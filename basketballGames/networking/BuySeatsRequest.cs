using models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace networking
{
    [Serializable]
    public class BuySeatsRequest: IRequest
    {
        public Game Game { get; set; }
        public int NrSeats { get; set; }
        public string ClientName { get; set; }
        public BuySeatsRequest(Game game, int nrSeats, string clientName) { 
            this.ClientName = clientName;
            this.NrSeats = nrSeats;
            this.Game = game;
        }

    }
}
