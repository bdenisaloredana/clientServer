using networking;
using services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    public class SerialGamesServer : ConcurrentServer
    {
        private IGamesService server;
        private GamesClientWorker worker;
        public SerialGamesServer(string host, int port, IGamesService server) : base(host, port)
        {
            this.server = server;
        }

        protected override Thread CreateWorker(TcpClient client)
        {
            worker = new GamesClientWorker(server, client);
            return new Thread(new ThreadStart(worker.Run));
        }
    }
}
