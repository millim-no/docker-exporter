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
            |> Array.map exportContainer
            |> String.concat "\n"
