namespace docker_exporter

open FSharp.Data

module Docker =
    [<Literal>]
    let containersSample =
        """
    [
        {
            "Id": "2d1ab9546a4fe61e23ac62ed422dfa7ac3e410c68b2f3262471c6e40ca68045c",
            "Names": [
                "/devbox_dns"
            ],
            "Image": "ghcr.io/darthmikke/docker-dnsmasq:1.2.1",
            "ImageID": "sha256:8dc73ee8e8bcfcf0f283d89e09c8bc41a987efa3e839ec6d684a36ab414b88b4",
            "Command": "/bin/sh -c /entrypoint.sh",
            "Created": 1714168357,
            "Ports": [
                {
                    "IP": "0.0.0.0",
                    "PrivatePort": 53,
                    "PublicPort": 53,
                    "Type": "tcp"
                },
                {
                    "IP": "0.0.0.0",
                    "PrivatePort": 53,
                    "PublicPort": 53,
                    "Type": "udp"
                }
            ],
            "Labels": {
                "com.docker.compose.config-hash": "27876b6f7b8e27c8063a64795dcc67250c853692a474c2d15228bab949fc85f7",
                "com.docker.compose.container-number": "1",
                "com.docker.compose.depends_on": "",
                "com.docker.compose.image": "sha256:8dc73ee8e8bcfcf0f283d89e09c8bc41a987efa3e839ec6d684a36ab414b88b4",
                "com.docker.compose.oneoff": "False",
                "com.docker.compose.project": "millim-dev",
                "com.docker.compose.project.config_files": "/Users/mjw/Documents/Koding/millim-dev/docker-compose.yml",
                "com.docker.compose.project.working_dir": "/Users/mjw/Documents/Koding/millim-dev",
                "com.docker.compose.replace": "de1358280e077ced7f2a6450fd73a3e98caebdc2a3d981193c38ada71fd88f27",
                "com.docker.compose.service": "dns",
                "com.docker.compose.version": "2.24.5",
                "desktop.docker.io/binds/0/Source": "/Users/mjw/Documents/Koding/millim-dev/dnsmasq",
                "desktop.docker.io/binds/0/SourceKind": "hostFile",
                "desktop.docker.io/binds/0/Target": "/srv"
            },
            "State": "running",
            "Status": "Up 2 weeks",
            "HostConfig": {
                "NetworkMode": "millim-dev_default"
            },
            "NetworkSettings": {
                "Networks": {
                    "millim-dev_default": {
                    "IPAMConfig": null,
                    "Links": null,
                    "Aliases": null,
                    "MacAddress": "02:42:ac:12:00:03",
                    "NetworkID": "a91109e8b9b2748bd54269e97947f9b72508256daeca8e733a7934a93fd252a6",
                    "EndpointID": "90df3dd75da5864e89a3ff47902dc06fc88d6ff43e29b6b0bd212768991055ec",
                    "Gateway": "172.18.0.1",
                    "IPAddress": "172.18.0.3",
                    "IPPrefixLen": 16,
                    "IPv6Gateway": "",
                    "GlobalIPv6Address": "",
                    "GlobalIPv6PrefixLen": 0,
                    "DriverOpts": null,
                    "DNSNames": null
                    }
                }
            },
            "Mounts": [
                {
                    "Type": "bind",
                    "Source": "/host_mnt/Users/mjw/Documents/Koding/millim-dev/dnsmasq",
                    "Destination": "/srv",
                    "Mode": "rw",
                    "RW": true,
                    "Propagation": "rprivate"
                }
            ]
        },
        {
            "Id": "41833a61eecb28f0d1cc8abab484f496b0afcb7634c87cbaf6316c9982d17475",
            "Names": [
                "/devbox"
            ],
            "Image": "gitea.ts.millim.no/millim/webhost:2.2.0-a7",
            "ImageID": "sha256:aafbf94686a3c8a9c041b3d78f2c3f72dbd35c7e6940f9ac86ddab2eba47807a",
            "Command": "/bin/sh -c /scripts/run.sh",
            "Created": 1710265067,
            "Ports": [
                {
                    "IP": "0.0.0.0",
                    "PrivatePort": 80,
                    "PublicPort": 80,
                    "Type": "tcp"
                },
                {
                    "IP": "0.0.0.0",
                    "PrivatePort": 443,
                    "PublicPort": 443,
                    "Type": "tcp"
                }
            ],
            "Labels": {
                "com.docker.compose.config-hash": "9821b9a5296336cf730a667d07b5b2e1edf38eeee4b7362dcdde362bab2bfff4",
                "com.docker.compose.container-number": "1",
                "com.docker.compose.depends_on": "",
                "com.docker.compose.image": "sha256:aafbf94686a3c8a9c041b3d78f2c3f72dbd35c7e6940f9ac86ddab2eba47807a",
                "com.docker.compose.oneoff": "False",
                "com.docker.compose.project": "millim-dev",
                "com.docker.compose.project.config_files": "/Users/mjw/Documents/Koding/millim-dev/docker-compose.yml",
                "com.docker.compose.project.working_dir": "/Users/mjw/Documents/Koding/millim-dev",
                "com.docker.compose.service": "devbox",
                "com.docker.compose.version": "2.24.5",
                "desktop.docker.io/binds/0/Source": "/Users/mjw/Documents/Koding/millim-config",
                "desktop.docker.io/binds/0/SourceKind": "hostFile",
                "desktop.docker.io/binds/0/Target": "/srv/apache",
                "desktop.docker.io/binds/1/Source": "/Users/mjw/Documents/Koding/millim",
                "desktop.docker.io/binds/1/SourceKind": "hostFile",
                "desktop.docker.io/binds/1/Target": "/srv/portfolio",
                "desktop.docker.io/binds/2/Source": "/Users/mjw/Documents/Koding/millim-dev/srv/certs",
                "desktop.docker.io/binds/2/SourceKind": "hostFile",
                "desktop.docker.io/binds/2/Target": "/srv/certs",
                "desktop.docker.io/binds/3/Source": "/Users/mjw/Documents/Koding/millim-dev/srv/ssh",
                "desktop.docker.io/binds/3/SourceKind": "hostFile",
                "desktop.docker.io/binds/3/Target": "/ssh"
            },
            "State": "running",
            "Status": "Up 2 weeks",
            "HostConfig": {
                "NetworkMode": "millim-dev_default"
            },
            "NetworkSettings": {
                "Networks": {
                    "millim-dev_default": {
                    "IPAMConfig": null,
                    "Links": null,
                    "Aliases": null,
                    "MacAddress": "02:42:ac:12:00:02",
                    "NetworkID": "a91109e8b9b2748bd54269e97947f9b72508256daeca8e733a7934a93fd252a6",
                    "EndpointID": "bec1a842aea1865790235b14a0560fd0ae724520ec43aad6f939512ee66c953c",
                    "Gateway": "172.18.0.1",
                    "IPAddress": "172.18.0.2",
                    "IPPrefixLen": 16,
                    "IPv6Gateway": "",
                    "GlobalIPv6Address": "",
                    "GlobalIPv6PrefixLen": 0,
                    "DriverOpts": null,
                    "DNSNames": null
                    }
                }
            },
            "Mounts": [
                {
                    "Type": "bind",
                    "Source": "/host_mnt/Users/mjw/Documents/Koding/millim",
                    "Destination": "/srv/portfolio",
                    "Mode": "rw",
                    "RW": true,
                    "Propagation": "rprivate"
                },
                {
                    "Type": "bind",
                    "Source": "/host_mnt/Users/mjw/Documents/Koding/millim-dev/srv/ssh",
                    "Destination": "/ssh",
                    "Mode": "rw",
                    "RW": true,
                    "Propagation": "rprivate"
                },
                {
                    "Type": "volume",
                    "Name": "6d954026587d416eaca9b741ea675635eddbbb21e087b5619eeb25b9eb30e863",
                    "Source": "",
                    "Destination": "/srv",
                    "Driver": "local",
                    "Mode": "",
                    "RW": true,
                    "Propagation": ""
                },
                {
                    "Type": "bind",
                    "Source": "/host_mnt/Users/mjw/Documents/Koding/millim-config",
                    "Destination": "/srv/apache",
                    "Mode": "rw",
                    "RW": true,
                    "Propagation": "rprivate"
                },
                {
                    "Type": "bind",
                    "Source": "/host_mnt/Users/mjw/Documents/Koding/millim-dev/srv/certs",
                    "Destination": "/srv/certs",
                    "Mode": "rw",
                    "RW": true,
                    "Propagation": "rprivate"
                }
            ]
        }
    ]
    """

    [<Literal>]
    let containerSample =
        """
        {
            "read": "2024-05-13T20:13:17.864869888Z",
            "preread": "2024-05-13T20:13:16.857814138Z",
            "pids_stats": {
                "current": 237,
                "limit": 18446744073709551615
            },
            "blkio_stats": {
                "io_service_bytes_recursive": [
                    {
                        "major": 254,
                        "minor": 0,
                        "op": "read",
                        "value": 40759296
                    },
                    {
                        "major": 254,
                        "minor": 0,
                        "op": "write",
                        "value": 3485696
                    },
                    {
                        "major": 254,
                        "minor": 32,
                        "op": "read",
                        "value": 114688
                    },
                    {
                        "major": 254,
                        "minor": 32,
                        "op": "write",
                        "value": 0
                    }
                ],
                "io_serviced_recursive": null,
                "io_queue_recursive": null,
                "io_service_time_recursive": null,
                "io_wait_time_recursive": null,
                "io_merged_recursive": null,
                "io_time_recursive": null,
                "sectors_recursive": null
            },
            "num_procs": 0,
            "storage_stats": {},
            "cpu_stats": {
                "cpu_usage": {
                    "total_usage": 105675152000,
                    "usage_in_kernelmode": 35337648000,
                    "usage_in_usermode": 70337504000
                },
                "system_cpu_usage": 964268510000000,
                "online_cpus": 8,
                "throttling_data": {
                    "periods": 0,
                    "throttled_periods": 0,
                    "throttled_time": 0
                }
            },
            "precpu_stats": {
                "cpu_usage": {
                "total_usage": 105674517000,
                "usage_in_kernelmode": 35337435000,
                "usage_in_usermode": 70337081000
                },
                "system_cpu_usage": 964260490000000,
                "online_cpus": 8,
                "throttling_data": {
                "periods": 0,
                "throttled_periods": 0,
                "throttled_time": 0
                }
            },
            "memory_stats": {
                "usage": 175489024,
                "stats": {
                    "active_anon": 47828992,
                    "active_file": 23195648,
                    "anon": 47476736,
                    "anon_thp": 0,
                    "file": 98709504,
                    "file_dirty": 0,
                    "file_mapped": 13332480,
                    "file_writeback": 0,
                    "inactive_anon": 0,
                    "inactive_file": 75161600,
                    "kernel_stack": 3866624,
                    "pgactivate": 0,
                    "pgdeactivate": 0,
                    "pgfault": 1179672,
                    "pglazyfree": 0,
                    "pglazyfreed": 0,
                    "pgmajfault": 223,
                    "pgrefill": 0,
                    "pgscan": 0,
                    "pgsteal": 0,
                    "shmem": 352256,
                    "slab": 24164768,
                    "slab_reclaimable": 21701616,
                    "slab_unreclaimable": 2463152,
                    "sock": 0,
                    "thp_collapse_alloc": 0,
                    "thp_fault_alloc": 2,
                    "unevictable": 0,
                    "workingset_activate": 0,
                    "workingset_nodereclaim": 0,
                    "workingset_refault": 0
                },
                "limit": 4113825792
            },
            "name": "/devbox",
            "id": "41833a61eecb28f0d1cc8abab484f496b0afcb7634c87cbaf6316c9982d17475",
            "networks": {
                "eth0": {
                    "rx_bytes": 240518,
                    "rx_packets": 2141,
                    "rx_errors": 0,
                    "rx_dropped": 0,
                    "tx_bytes": 6461791,
                    "tx_packets": 900,
                    "tx_errors": 0,
                    "tx_dropped": 0
                }
            }
        }
            """

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
