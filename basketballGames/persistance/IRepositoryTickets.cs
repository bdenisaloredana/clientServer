using models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace persistance
{
    public interface IRepositoryTickets : IRepository<int, Ticket>
    {
    }
}
