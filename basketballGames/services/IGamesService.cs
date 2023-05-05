using models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace services
{
    public interface IGamesService
    {
        IList<Game> FindAll();
        IList<Game> GetNonSoldOutGames();
        void BuyTicket(Game game, int nrSeats, string clientName);
        void Login(Employee employee, IObserver observer);
        void Logout(Employee employee, IObserver observer);
    }
}
