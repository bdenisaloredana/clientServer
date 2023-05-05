using models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace networking
{
    [Serializable]
    public class LoginRequest: IRequest
    {
        public Employee Employee { get; set; }
        public LoginRequest(Employee employee) {
            this.Employee = employee;
        }

    }
}
