using models;
using Npgsql.Internal;
using persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace services
{
    public class GamesService : IGamesService
    {
        IRepositoryEmployees EmployeesRepository;
        IRepositoryGames GamesRepository;
        IRepositoryTickets TicketsRepository;
        IDictionary<string, IObserver> LoggedClients;

        public GamesService(IRepositoryEmployees employeesRepository, IRepositoryGames gamesRepository, IRepositoryTickets ticketsRepository)
        {
            EmployeesRepository = employeesRepository;
            GamesRepository = gamesRepository;
            TicketsRepository = ticketsRepository;
            LoggedClients = new Dictionary<string, IObserver>();
        }
        public void BuyTicket(Game game, int nrSeats, string clientName)
        {
            this.TicketsRepository.Save(new Ticket(1, game.Id, clientName, nrSeats));
            game.NrSeats = (Int32.Parse(game.NrSeats) - nrSeats).ToString();
            this.GamesRepository.Update(game);
            IList<Game> updatedGames = this.GamesRepository.FindAll();
            foreach(IObserver observer in LoggedClients.Values)
            {
                Task.Run(() => observer.Update(updatedGames));
            }

        }

        public IList<Game> FindAll()
        {
            return this.GamesRepository.FindAll();
        }

        public IList<Game> GetNonSoldOutGames()
        {
            return this.GamesRepository.getNonSoldOutGames();
        }
        private Employee GetByUsername(string username) { return this.EmployeesRepository.FindByUsername(username); }


        public void Login(Employee employee, IObserver observer)
        {
            Employee check = GetByUsername(employee.EmployeeUsername);
            if (check != null)
            {
                if (this.LoggedClients.ContainsKey(employee.EmployeeUsername))
                {
                    throw new GamesException("Employee already loged in!");
                }
                else
                {
                    if (employee.Password == check.Password)
                    {
                        this.LoggedClients.Add(employee.EmployeeUsername, observer);
                    }
                    else throw new GamesException("Wrong password!");
                }

            }
            else throw new GamesException("Invalid username!");
        }

        public void Logout(Employee employee, IObserver observer)
        {
            bool localClient  = this.LoggedClients.Remove(employee.EmployeeUsername);
            if(!localClient)
            {
                throw new GamesException("Employee " + employee.EmployeeUsername + " is not logged in!");
            }
        }
    }
}
