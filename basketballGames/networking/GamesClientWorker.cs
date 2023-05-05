using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using services;
using models;

namespace networking
{
    public class GamesClientWorker: IObserver
    {
        private IGamesService server;
        private TcpClient connection;

        private NetworkStream stream;
        private IFormatter formatter;
        private volatile bool connected;
        public GamesClientWorker(IGamesService server, TcpClient connection)
        {
            this.server = server;
            this.connection = connection;
            try
            {

                stream = connection.GetStream();
                formatter = new BinaryFormatter();
                connected = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public virtual void Run()
        {
            while (connected)
            {
                try
                {
                    object request = formatter.Deserialize(stream);
                    object response = handleRequest((IRequest)request);
                    if (response != null)
                    {
                        SendResponse((IResponse)response);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }

                try
                {
                    Thread.Sleep(1000);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
            try
            {
                stream.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e);
            }
        }

        public void Update(IList<Game> updatedGames)
        {
            SendResponse(new NrSeatsChangedResponse(updatedGames));
           
        }

        private IResponse handleRequest(IRequest request)
        {
            IResponse response = null;
            if (request is LoginRequest)
            {
                Console.WriteLine("Login request ...");
                LoginRequest logReq = (LoginRequest)request;
                Employee employee = logReq.Employee;
                try
                {
                    lock (server)
                    {
                        server.Login(employee, this);
                    }
                    return new OkResponse();
                }
                catch (GamesException e)
                {
                    connected = false;
                    return new ErrorResponse(e.Message);
                }
            }
            if (request is LogoutRequest)
            {
                Console.WriteLine("Logout request");
                LogoutRequest logReq = (LogoutRequest)request;
                Employee employee = logReq.Employee;
                try
                {
                    lock (server)
                    {

                        server.Logout(employee, this);
                    }
                    connected = false;
                    return new OkResponse();

                }
                catch (GamesException e)
                {
                    return new ErrorResponse(e.Message);
                }
            }
            if (request is GetAllGamesRequest)
            {
                Console.WriteLine("Get all games request");
                IList<Game> games;
                try
                {
                    lock (server)
                    {
                        games = this.server.FindAll();
                    }
                    return new GetAllGamesResponse(games);
                }
                catch (GamesException e)
                {
                    return new ErrorResponse(e.Message);
                }
            }
            if (request is GetNonSoldOutGamesRequest)
            {
                Console.WriteLine("Get non sold out games request");
                IList<Game> games;
                try
                {
                    lock (server)
                    {
                        games = this.server.GetNonSoldOutGames();
                    }
                    return new GetNonSoldOutGamesResponse(games);
                }
                catch (GamesException e)
                {
                    return new ErrorResponse(e.Message);
                }
            }
            if (request is BuySeatsRequest)
            {
                Console.WriteLine("Buy seats request");
                BuySeatsRequest buySeatsRequest = (BuySeatsRequest)request;
                try
                {
                    lock (server)
                    {
                        this.server.BuyTicket(buySeatsRequest.Game, buySeatsRequest.NrSeats, buySeatsRequest.ClientName);
                    }
                    return new OkResponse();
                }
                catch (GamesException e)
                {
                    return new ErrorResponse(e.Message);
                }
                
            }
            return response;
        }

        private void SendResponse(IResponse response)
        {
            Console.WriteLine("sending response " + response);
            formatter.Serialize(stream, response);
            stream.Flush();

        }
    }
}
