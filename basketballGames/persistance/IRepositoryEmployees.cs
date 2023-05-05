using models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace persistance
{
    public interface IRepositoryEmployees : IRepository<int, Employee>
    {
        public Employee FindByUsername(string username);
    }
}
