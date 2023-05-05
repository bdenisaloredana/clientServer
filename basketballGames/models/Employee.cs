using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models
{
    [Serializable]
    public class Employee : Entity<int>
    {
        public string EmployeeName { get; set; }
        public string EmployeeUsername { get; set; }
        public string Password { get; set; }

        public Employee(int id, string name, string username, string password) : base(id)
        {
            this.EmployeeName = name;
            this.EmployeeUsername = username;
            this.Password = password;
        }
        public Employee(string username, string password):base(1)
        {
            this.EmployeeUsername = username;
            this.Password = password;
        }

        public override bool Equals(object? obj)
        {
            return obj is Employee && ((Employee)obj).Id == Id && ((Employee)obj).EmployeeName == EmployeeName && ((Employee)obj).EmployeeUsername == EmployeeUsername && ((Employee)obj).Password == Password;
        }
        public override int GetHashCode()
        {
            return EmployeeUsername.GetHashCode();
        }


    }
}
