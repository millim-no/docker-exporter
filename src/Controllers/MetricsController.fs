namespace docker_exporter.Controllers

open System
open System.Net.Http
open System.Net.Sockets
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open docker_exporter.Http
open docker_exporter.Docker

type ConnectCallbackDelegate = delegate of SocketsHttpConnectionContext * Threading.CancellationToken -> Task<NetworkStream>

[<ApiController>]
[<Route("[controller]")>]
type MetricsController (logger : ILogger<MetricsController>) =
    inherit ControllerBase()

    /// To calculate the values shown by the stats command of the docker cli tool the following formulas can be used:
    /// 
    /// - used_memory = memory_stats.usage - memory_stats.stats.cache
    /// - available_memory = memory_stats.limit
    /// - Memory usage % = (used_memory / available_memory) * 100.0
    /// - cpu_delta = cpu_stats.cpu_usage.total_usage - precpu_stats.cpu_usage.total_usage
    /// - system_cpu_delta = cpu_stats.system_cpu_usage - precpu_stats.system_cpu_usage
    /// - number_cpus = lenght(cpu_stats.cpu_usage.percpu_usage) or cpu_stats.online_cpus
    /// - CPU usage % = (cpu_delta / system_cpu_delta) * number_cpus * 100.0
    /// 
    /// As per documentation:
    /// https://docs.docker.com/engine/api/v1.41/#tag/Container/operation/ContainerStats
    member _.exportContainer(container: Container.Root) =
        let cpu_usage =
            ((container.CpuStats.CpuUsage.TotalUsage - container.PrecpuStats.CpuUsage.TotalUsage) |> float)
            /
            ((container.CpuStats.SystemCpuUsage - container.PrecpuStats.SystemCpuUsage) |> float)
            * (container.CpuStats.OnlineCpus |> float)
        let cpu = cpu_usage * 100.0
        let memory = container.MemoryStats.Usage
        sprintf """
# HELP cpu_percentage CPU usage, Unix format.
# UNIT percent
# TYPE cpu_percentage gauge
cpu_percentage{name="%s"} %f
# HELP cpu_usage CPU usage, Unix format.
# TYPE cpu_usage gauge
cpu_usage{name="%s"} %f
# HELP cpu_clock CPU clock for this container.
# TYPE cpu_clock counter
cpu_clock{name="%s"} %i
# HELP cpu_host_clock Host's CPU clock.
# TYPE cpu_host_clock counter
cpu_host_clock{name="%s"} %i

# HELP memory Memory usage, in bytes.
# TYPE memory gauge
memory{name="%s"} %i
        """ container.Name cpu container.Name cpu_usage container.Name container.CpuStats.CpuUsage.TotalUsage container.Name container.CpuStats.SystemCpuUsage container.Name memory

    [<HttpGet>]
    member this.Get() =
        let socketPath = "/var/run/docker.sock"

        let httpClient = socketHttpClient socketPath

        let ids =
            httpClient.GetStringAsync "http://v1.41/containers/json"
            |> Async.AwaitTask
            |> Async.RunSynchronously
            |> Containers.Parse
            |> Array.map (fun container -> container.Id)

        // curl --unix-socket /Users/mjw/.docker/run/docker.sock "v1.41/containers/41833a61eecb28f0d1cc8abab484f496b0afcb7634c87cbaf6316c9982d17475/stats?stream=false" | jq "."
        let containers =
            ids
            |> Array.map (sprintf "http://v1.41/containers/%s/stats?stream=false")
            |> Array.map (fun url ->
                httpClient.GetStringAsync url
                |> Async.AwaitTask
            )
            |> Async.Parallel
            |> Async.RunSynchronously
            |> Array.map (fun data -> Container.Parse data)

        containers
            |> Array.map this.exportContainer
            |> String.concat "\n"
