namespace docker_exporter

open FSharp.Data
open docker_exporter.DockerSamples

module Docker =
    type Containers = JsonProvider<containersSample>
    type Container = JsonProvider<containerSample>

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
    let exportContainer(container: Container.Root) =

        let cpu_usage_template:Printf.StringFormat<_> = """
# HELP cpu_percentage CPU usage, Unix format.
# UNIT percent
# TYPE cpu_percentage gauge
cpu_percentage{name="%s"} %f
# HELP cpu_usage CPU usage, Unix format.
# TYPE cpu_usage gauge
cpu_usage{name="%s"} %f
"""
        let cpu_clock_template:Printf.StringFormat<_> = """
# HELP cpu_clock CPU clock for this container.
# TYPE cpu_clock counter
cpu_clock{name="%s"} %i
# HELP cpu_host_clock Host's CPU clock.
# TYPE cpu_host_clock counter
cpu_host_clock{name="%s"} %i
# HELP cpu_clock_total CPU clock for this container.
# TYPE cpu_clock_total counter
cpu_clock_total{name="%s"} %i
# HELP cpu_host_clock_total Host's CPU clock.
# TYPE cpu_host_clock_total counter
cpu_host_clock_total{name="%s"} %i
        """ 
        
        let total_usage = container.CpuStats.CpuUsage.TotalUsage
        let pre_total_usage = container.PrecpuStats.CpuUsage.TotalUsage
        let system_usage = container.CpuStats.SystemCpuUsage
        let pre_system_usage = container.PrecpuStats.SystemCpuUsage
        let cpu_usage =
            ((total_usage - pre_total_usage) |> float) /
            ((system_usage - pre_system_usage) |> float)
            * (container.CpuStats.OnlineCpus |> float)
        let cpu_percentage = cpu_usage * 100.0
        
        let cpu_usage_string = sprintf cpu_usage_template container.Name cpu_percentage container.Name cpu_usage
        let cpu_clock_string = sprintf cpu_clock_template container.Name total_usage container.Name system_usage container.Name total_usage container.Name system_usage

        let memory_template:Printf.StringFormat<_> = """# HELP memory Memory usage, in bytes.
# TYPE memory gauge
memory{name="%s"} %i
        """
        let memory: int =
            container.MemoryStats.JsonValue.TryGetProperty("usage")
            |> (fun memory ->
                match memory with
                | Some(memory) -> memory.AsInteger()
                | None -> -1)
        let memory_string = sprintf (memory_template) container.Name memory

        cpu_usage_string + cpu_clock_string + memory_string
