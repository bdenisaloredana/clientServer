using models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace networking
{
    [Serializable]
    public class GetNonSoldOutGamesResponse: IResponse
    {
        public IList<Game> Games { get; set; }
        public GetNonSoldOutGamesResponse(IList<Game> games)
        {
            Games = games;
        }
    }
}
