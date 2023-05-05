using Azure.Core;
using Azure;
using persistance;
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
using Microsoft.Identity.Client;

namespace networking
{
    public class GamesServicesProxy: IGamesService
    {
        private string host;
        private int port;

        private IObserver client;

        private NetworkStream stream;

        private IFormatter formatter;
        private TcpClient connection;

        private Queue<IResponse> responses;
        private volatile bool finished;
        private EventWaitHandle _waitHandle;
        public GamesServicesProxy(string host, int port)
        {
            this.host = host;
            this.port = port;
            responses = new Queue<IResponse>();
        }

        public virtual void Login(Employee employee, IObserver client)
        {
            InitializeConnection();
            SendRequest(new LoginRequest(employee));
            IResponse response = ReadResponse();
            if (response is OkResponse)
            {
                this.client = client;
                return;
            }
            if (response is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse)response;
                CloseConnection();
                throw new GamesException(err.Message);
            }
        }

        public virtual void Logout(Employee employee, IObserver client)
        {
            SendRequest(new LogoutRequest(employee));
            IResponse response = ReadResponse();
            CloseConnection();
            if (response is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse)response;
                throw new GamesException(err.Message);
            }
        }

        private void CloseConnection()
        {
            finished = true;
            try
            {
                stream.Close();

                connection.Close();
                _waitHandle.Close();
                client = null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

        }

        private void SendRequest(IRequest request)
        {
            try
            {
                formatter.Serialize(stream, request);
                stream.Flush();
            }
            catch (Exception e)
            {
                throw new GamesException("Error sending object " + e);
            }

        }

        private IResponse ReadResponse()
        {
            IResponse response = null;
            try
            {
                _waitHandle.WaitOne();
                lock (responses)
                {
                    response = responses.Dequeue();

                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return response;
        }
        private void InitializeConnection()
        {
            try
            {
                connection = new TcpClient(host, port);
                stream = connection.GetStream();
                formatter = new BinaryFormatter();
                finished = false;
                _waitHandle = new AutoResetEvent(false);
                StartReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
        private void StartReader()
        {
            Thread tw = new Thread(Run);
            tw.Start();
        }


        private void HandleUpdate(UpdateResponse update)
        {
            if (update is NrSeatsChangedResponse)
            {
                NrSeatsChangedResponse nrSeatsChangedResponse = (NrSeatsChangedResponse)update;
                try
                {
                    this.client.Update(nrSeatsChangedResponse.UpdatedGames);
                }
                catch (GamesException e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
        }
        public virtual void Run()
        {
            while (!finished)
            {
                try
                {
                    object response = formatter.Deserialize(stream);
                    Console.WriteLine("response received " + response);
                    if (response is UpdateResponse)
                    {
                        HandleUpdate((UpdateResponse)response);
                    }
                    else
                    {

                        lock (responses)
                        {


                            responses.Enqueue((IResponse)response);

                        }
                        _waitHandle.Set();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Reading error " + e);
                }

            }
        }

        public IList<Game> FindAll()
        {
            SendRequest(new GetAllGamesRequest());
            IResponse response = ReadResponse();
            if(response is ErrorResponse)
            {
                ErrorResponse errorResponse = (ErrorResponse)response;
                throw new GamesException(errorResponse.Message);
            }
            GetAllGamesResponse getAllGamesResponse = (GetAllGamesResponse)response;
            return getAllGamesResponse.Games;
        }

        public IList<Game> GetNonSoldOutGames()
        {
            SendRequest(new  GetNonSoldOutGamesRequest());
            IResponse response = ReadResponse();
            if (response is ErrorResponse)
            {
                ErrorResponse errorResponse = (ErrorResponse)response;
                throw new GamesException(errorResponse.Message);
            }
            GetNonSoldOutGamesResponse getNonSoldOutGamesResponse = (GetNonSoldOutGamesResponse)response;
            return getNonSoldOutGamesResponse.Games;
        }

        public void BuyTicket(Game game, int nrSeats, string clientName)
        {
            SendRequest(new BuySeatsRequest(game, nrSeats, clientName));
            IResponse response = ReadResponse();
            if(response is ErrorResponse)
            {
                ErrorResponse errorResponse = (ErrorResponse)response;
                throw new GamesException(errorResponse.Message);
            }
        }
       
    }

}

