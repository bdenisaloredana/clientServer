using models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace networking
{
    [Serializable]
    public class LogoutRequest: IRequest
    {
        public Employee Employee { set; get; }
        public LogoutRequest(Employee employee)
        {
            this.Employee = employee;
        }
    }
}
