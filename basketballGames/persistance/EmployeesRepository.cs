using log4net;
using models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace persistance
{
    public class EmployeesRepository : IRepositoryEmployees
    {
        private static readonly ILog log = LogManager.GetLogger("EmployeesRepository");


        IDictionary<String, string> props;
        public EmployeesRepository(IDictionary<String, string> props)
        {
            log.Info("Creating EmployeesRepository");
            this.props = props;
        }


        public void Save(Employee employee)
        {
            var con = DBUtils.getConnection(props);
            log.InfoFormat("Saving employee {}", employee);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "insert into employees(employee_name, username, password) values (@employee_name, @username, @password)";

                var paramName = comm.CreateParameter();
                paramName.ParameterName = "@employee_name";
                paramName.Value = employee.EmployeeName;
                comm.Parameters.Add(paramName);

                var paramUsername = comm.CreateParameter();
                paramUsername.ParameterName = "@username";
                paramUsername.Value = employee.EmployeeUsername;
                comm.Parameters.Add(paramUsername);

                var paramPassword = comm.CreateParameter();
                paramPassword.ParameterName = "@password";
                paramPassword.Value = employee.Password;
                comm.Parameters.Add(paramPassword);

                var result = comm.ExecuteNonQuery();
                log.InfoFormat("Saved count {0}", result);
            }
        }

        public void Delete(int id)
        {
            IDbConnection con = DBUtils.getConnection(props);
            log.InfoFormat("Deleting employee with id {0}", id);
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "delete from employees where id=@id";
                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                comm.Parameters.Add(paramId);
                var dataR = comm.ExecuteNonQuery();
                log.InfoFormat("Deleted count {0}", dataR);
            }
        }

        public IList<Employee> FindAll()
        {
            IDbConnection con = DBUtils.getConnection(props);
            IList<Employee> employees = new List<Employee>();
            log.InfoFormat("Finding all employees");
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select id, employee_name, username, password from employees";

                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        int idEmployee = dataR.GetInt32(0);
                        string name = dataR.GetString(1);
                        string username = dataR.GetString(2);
                        string password = dataR.GetString(3);
                        Employee employee = new Employee(idEmployee, name, username, password);
                        employees.Add(employee);
                    }
                }
            }
            return employees;
        }

        public Employee FindByUsername(string username)
        {
            log.InfoFormat("Entering FindByUsername with value {0}", username);
            IDbConnection con = DBUtils.getConnection(props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select id, employee_name, username, password from employees where username=@username";
                var paramUsername = comm.CreateParameter();
                paramUsername.ParameterName = "@username";
                paramUsername.Value = username;
                comm.Parameters.Add(paramUsername);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        int idEmployee = dataR.GetInt32(0);
                        string name = dataR.GetString(1);
                        string usernameEmployee = dataR.GetString(2);
                        string password = dataR.GetString(3);
                        Employee employee = new Employee(idEmployee, name, usernameEmployee, password);
                        log.InfoFormat("Exiting FindByUsername with value {0}", employee);
                        return employee;
                    }
                }
            }
            log.InfoFormat("Exiting FindByUsername with value {0}", null);
            return null;
        }

        public Employee FindOne(int id)
        {
            log.InfoFormat("Entering FindOne with value {0}", id);
            IDbConnection con = DBUtils.getConnection(props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select id, employee_name, username, password from employees where id=@id";
                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                comm.Parameters.Add(paramId);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        int idEmployee = dataR.GetInt32(0);
                        string name = dataR.GetString(1);
                        string username = dataR.GetString(2);
                        string password = dataR.GetString(3);
                        Employee employee = new Employee(idEmployee, name, username, password);
                        log.InfoFormat("Exiting FindOne with value {0}", employee);
                        return employee;
                    }
                }
            }
            log.InfoFormat("Exiting FindOne with value {0}", null);
            return null;
        }

        public void Update(Employee employee)
        {
            var con = DBUtils.getConnection(props);
            log.InfoFormat("Updates employee with id {0} to {1}", employee.Id, employee);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "update employees set employee_name=@employee_name, username=@username, password=@password where id=@id";

                var paramName = comm.CreateParameter();
                paramName.ParameterName = "@employee_name";
                paramName.Value = employee.EmployeeName;
                comm.Parameters.Add(paramName);

                var paramUsername = comm.CreateParameter();
                paramUsername.ParameterName = "@username";
                paramUsername.Value = employee.EmployeeUsername;
                comm.Parameters.Add(paramUsername);

                var paramPassword = comm.CreateParameter();
                paramPassword.ParameterName = "@password";
                paramPassword.Value = employee.EmployeeUsername;
                comm.Parameters.Add(paramPassword);

                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = employee.Id;
                comm.Parameters.Add(paramId);

                var result = comm.ExecuteNonQuery();
                log.InfoFormat("Updated count {0}", result);
            }
        }
    }
}
