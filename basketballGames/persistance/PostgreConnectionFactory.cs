using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace persistance
{
    internal class PostgreConnectionFactory : ConnectionFactory
    {
        public override IDbConnection createConnection(IDictionary<string, string> props)
        {
            String connectionString = props["ConnectionString"];
            Console.WriteLine("PostgreSQL ---Se deschide o conexiune la  ... {0}", connectionString);
            return new NpgsqlConnection(connectionString);

        }
    }
}
