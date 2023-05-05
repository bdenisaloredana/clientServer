using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models
{
    [Serializable]
    public class Game : Entity<int>
    {
        public string NrSeats { get; set; }
        public DateTime Date { get; set; }
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public string GameType { get; set; }
        public double Price { get; set; }

        public Game(int id, string nrSeats, DateTime date, string type, string team1, string team2, double price) : base(id)
        {
            this.NrSeats = nrSeats;
            this.Date = date;
            this.Team1 = team1;
            this.Team2 = team2;
            this.Price = price;
            this.GameType = type;
        }


        public override bool Equals(object? obj)
        {
            return obj is Game && Id == ((Game)obj).Id && NrSeats == ((Game)obj).NrSeats && Date == ((Game)obj).Date && Team1 == ((Game)obj).Team1
                && Team2 == ((Game)obj).Team2 && GameType == ((Game)obj).GameType && Price == ((Game)obj).Price;
        }


    }
}
