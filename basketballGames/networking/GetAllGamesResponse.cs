using models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace networking
{
    [Serializable]
    public class GetAllGamesResponse: IResponse
    {
        public IList<Game> Games { get; set; }
        public GetAllGamesResponse(IList<Game> games)
        {
            this.Games = games;
        }
    }
}
