namespace docker_exporter

open System.Net.Sockets
open System.Net.Http
open System
open System.Threading.Tasks

module Http =
    let socketHttpClient (path: string) =
        (**
        * https://stackoverflow.com/questions/53547152/how-do-i-nicely-send-http-over-a-unix-domain-socket-in-net-core
        *)
        let unixConnectCallback (context: SocketsHttpConnectionContext) (token: Threading.CancellationToken) =
            let socket = new Socket(AddressFamily.Unix, SocketType.Stream, ProtocolType.IP)
            let endpoint = new UnixDomainSocketEndPoint(path)
            socket.ConnectAsync(endpoint)
                |> Async.AwaitTask
                |> Async.RunSynchronously
                |> ignore
            new NetworkStream(socket, true)
                |> ValueTask<IO.Stream>
        
        let socketHandler = new SocketsHttpHandler()
        socketHandler.ConnectCallback <- new Func<SocketsHttpConnectionContext,Threading.CancellationToken,ValueTask<IO.Stream>>(unixConnectCallback)

        new HttpClient(socketHandler)
