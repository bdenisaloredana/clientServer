using models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace networking
{
    [Serializable]
    public class NrSeatsChangedResponse: UpdateResponse
    {
        public IList<Game> UpdatedGames { get; set; }
        public NrSeatsChangedResponse(IList<Game> updatedGames) {
            this.UpdatedGames = updatedGames;
        }
    }
}
