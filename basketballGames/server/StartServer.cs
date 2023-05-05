using networking;
using persistance;
using services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    public class StartServer
    {
        static void Main(string[] args)
        {
            IDictionary<String, string> props = new SortedList<String, String>();
            props.Add("ConnectionString", GetConnectionStringByName("postgres"));
            IRepositoryEmployees repositoryEmployees = new EmployeesRepository(props);
            IRepositoryTickets repositoryTickets = new TicketsRepository(props);
            IRepositoryGames repositoryGames = new GamesRepository(props);
            IGamesService gamesService = new GamesService(repositoryEmployees, repositoryGames, repositoryTickets);
            SerialGamesServer server = new SerialGamesServer("127.0.0.1", 55556, gamesService);
            server.Start();
            Console.WriteLine("Server started ...");
            Console.ReadLine();

        }
        static string GetConnectionStringByName(string name)
        {
            string returnValue = null;
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];

            if (settings != null)
                returnValue = settings.ConnectionString;

            return returnValue;
        }
    }

}

