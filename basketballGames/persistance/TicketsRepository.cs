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
    public class TicketsRepository : IRepositoryTickets
    {
        private static readonly ILog log = LogManager.GetLogger("TicketRepository");


        IDictionary<String, string> props;
        public TicketsRepository(IDictionary<String, string> props)
        {
            log.Info("Creating TicketRepository");
            this.props = props;
        }
        public void Delete(int id)
        {
            IDbConnection con = DBUtils.getConnection(props);
            log.InfoFormat("Deleting ticket with id {0}", id);
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "delete from tickets where id=@id";
                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                comm.Parameters.Add(paramId);
                var dataR = comm.ExecuteNonQuery();
                log.InfoFormat("Deleted count {0}", dataR);
            }
        }

        public IList<Ticket> FindAll()
        {
            IDbConnection con = DBUtils.getConnection(props);
            IList<Ticket> tickets = new List<Ticket>();
            log.InfoFormat("Finding all tickets");
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select id, client_name, nr_seats, game_id from tickets";

                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        int idTicket = dataR.GetInt32(0);
                        string name = dataR.GetString(1);
                        int seats = dataR.GetInt32(2);
                        int gameId = dataR.GetInt32(3);
                        Ticket ticket = new Ticket(idTicket, gameId, name, seats);
                        tickets.Add(ticket);
                    }
                }
            }
            return tickets;
        }

        public Ticket FindOne(int id)
        {
            log.InfoFormat("Entering FindOne with value {0}", id);
            IDbConnection con = DBUtils.getConnection(props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select id, client_name, nr_seats, game_id from tickets where id=@id";
                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                comm.Parameters.Add(paramId);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        int idTicket = dataR.GetInt32(0);
                        string name = dataR.GetString(1);
                        int seats = dataR.GetInt32(2);
                        int gameId = dataR.GetInt32(3);
                        Ticket ticket = new Ticket(idTicket, gameId, name, seats);
                        log.InfoFormat("Exiting FindOne with value {0}", ticket);
                        return ticket;
                    }
                }
            }
            log.InfoFormat("Exiting FindOne with value {0}", null);
            return null;
        }

        public void Save(Ticket ticket)
        {
            var con = DBUtils.getConnection(props);
            log.InfoFormat("Saving ticket {0}", ticket);
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "insert into tickets(client_name, nr_seats, game_id) values ( @client_name, @nr_seats, @game_id)";

                var paramName = comm.CreateParameter();
                paramName.ParameterName = "@client_name";
                paramName.Value = ticket.ClientName;
                comm.Parameters.Add(paramName);

                var paramSeats = comm.CreateParameter();
                paramSeats.ParameterName = "@nr_seats";
                paramSeats.Value = ticket.NrSeats;
                comm.Parameters.Add(paramSeats);

                var paramGameId = comm.CreateParameter();
                paramGameId.ParameterName = "@game_id";
                paramGameId.Value = ticket.IdGame;
                comm.Parameters.Add(paramGameId);

                var result = comm.ExecuteNonQuery();
                log.InfoFormat("Saved count {0}", result);
            }
        }

        public void Update(Ticket ticket)
        {
            var con = DBUtils.getConnection(props);
            log.InfoFormat("Updating ticket with id  {0} with {1}", ticket.Id, ticket);
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "update tickets set client_name=@client_name, nr_seats=@seats, game_id=@game where id=@id";

                var paramName = comm.CreateParameter();
                paramName.ParameterName = "@client_name";
                paramName.Value = ticket.ClientName;
                comm.Parameters.Add(paramName);

                var paramUsername = comm.CreateParameter();
                paramUsername.ParameterName = "@seats";
                paramUsername.Value = ticket.NrSeats;
                comm.Parameters.Add(paramUsername);

                var paramPassword = comm.CreateParameter();
                paramPassword.ParameterName = "@game";
                paramPassword.Value = ticket.IdGame;
                comm.Parameters.Add(paramPassword);

                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ticket.Id;
                comm.Parameters.Add(paramId);

                var result = comm.ExecuteNonQuery();
                log.InfoFormat("Updated count {0}", result);
            }
        }
    }
}
