using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models
{
    [Serializable]
    public class Ticket : Entity<int>
    {
        public long IdGame { get; set; }
        public string ClientName { get; set; }
        public int NrSeats { get; set; }

        public Ticket(int id, int idGame, string name, int nrSeats) : base(id)
        {

            this.IdGame = idGame;
            this.ClientName = name;
            this.NrSeats = nrSeats;
        }

        public override bool Equals(object? obj)
        {
            return obj is Ticket && Id == ((Ticket)obj).Id && IdGame == ((Ticket)obj).IdGame && ClientName == ((Ticket)obj).ClientName && NrSeats == ((Ticket)obj).NrSeats;
        }
    }
}
