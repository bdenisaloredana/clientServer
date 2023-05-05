using models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace persistance
{
    public interface IRepositoryGames : IRepository<int, Game>
    {
        public IList<Game> getNonSoldOutGames();
    }
}
