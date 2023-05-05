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
    public class GamesRepository : IRepositoryGames
    {
        private static readonly ILog log = LogManager.GetLogger("GamesRepository");


        IDictionary<String, string> props;
        public GamesRepository(IDictionary<String, string> props)
        {
            log.Info("Creating GamesRepository");
            this.props = props;
        }
        public void Delete(int id)
        {
            IDbConnection con = DBUtils.getConnection(props);
            log.InfoFormat("Deleting game wit id {0}", id);
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "delete from games where id=@id";
                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                comm.Parameters.Add(paramId);
                var dataR = comm.ExecuteNonQuery();
                log.InfoFormat("Deleted count {0}", id);
            }
        }

        public IList<Game> FindAll()
        {
            IDbConnection con = DBUtils.getConnection(props);
            IList<Game> games = new List<Game>();
            log.InfoFormat("Finding all games");
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select id, team1, team2, game_type, available_seats, price_per_ticket, game_date from games";

                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        int idTicket = dataR.GetInt32(0);
                        string team1 = dataR.GetString(1);
                        string team2 = dataR.GetString(2);
                        string gameType = dataR.GetString(3);
                        string seats = dataR.GetInt32(4).ToString();
                        if (seats.Equals("0"))
                            seats = "SOLD OUT";
                        double price = dataR.GetDouble(5);
                        DateTime date = dataR.GetDateTime(6);

                        Game game = new Game(idTicket, seats, date, gameType, team1, team2, price);
                        games.Add(game);
                    }
                }
            }
            return games;
        }

        public Game FindOne(int id)
        {
            log.InfoFormat("Entering FindOne with value {0}", id);
            IDbConnection con = DBUtils.getConnection(props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select id, team1, team2, game_type, available_seats, price_per_ticket, game_date from games where id=@id";
                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                comm.Parameters.Add(paramId);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        int idTicket = dataR.GetInt32(0);
                        string team1 = dataR.GetString(1);
                        string team2 = dataR.GetString(2);
                        string gameType = dataR.GetString(3);
                        string seats = dataR.GetInt32(4).ToString();
                        if (seats.Equals("0"))
                            seats = "SOLD OUT";
                        double price = dataR.GetDouble(5);
                        DateTime date = dataR.GetDateTime(6);

                        Game game = new Game(idTicket, seats, date, gameType, team1, team2, price);
                        log.InfoFormat("Exiting FindOne with value {0}", game);
                        return game;
                    }
                }
            }
            log.InfoFormat("Exiting FindOne with value {0}", null);
            return null;
        }

        public IList<Game> getNonSoldOutGames()
        {
            var con = DBUtils.getConnection(props);
            log.InfoFormat("Getting non sold out games");
            List<Game> games = new List<Game>();
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select id, team1, team2, game_type, available_seats, price_per_ticket, game_date from games where available_seats != 0 order by available_seats desc";
                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        int idTicket = dataR.GetInt32(0);
                        string team1 = dataR.GetString(1);
                        string team2 = dataR.GetString(2);
                        string gameType = dataR.GetString(3);
                        string seats = dataR.GetInt32(4).ToString();
                        if (seats.Equals("0"))
                            seats = "SOLD OUT";
                        double price = dataR.GetDouble(5);
                        DateTime date = dataR.GetDateTime(6);

                        Game game = new Game(idTicket, seats, date, gameType, team1, team2, price);
                        games.Add(game);
                    }
                }
            }
            return games;

        }

        public void Save(Game game)
        {
            var con = DBUtils.getConnection(props);
            log.InfoFormat("Saving game {0}", game);
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "insert into games(team1, team2, game_type, available_seats, price_per_ticket, game_date) values (@team1, @team2, @game_type, @available_seats, @price_per_ticket, @game_date)";

                var paramTeam1 = comm.CreateParameter();
                paramTeam1.ParameterName = "@team1";
                paramTeam1.Value = game.Team1;
                comm.Parameters.Add(paramTeam1);

                var paramTeam2 = comm.CreateParameter();
                paramTeam2.ParameterName = "@team2";
                paramTeam2.Value = game.Team2;
                comm.Parameters.Add(paramTeam2);

                var paramGameType = comm.CreateParameter();
                paramGameType.ParameterName = "@game_type";
                paramGameType.Value = game.GameType;
                comm.Parameters.Add(paramGameType);

                var paramSeats = comm.CreateParameter();
                paramSeats.ParameterName = "@available_seats";
                if (game.NrSeats.Equals("SOLD OUT"))
                    paramSeats.Value = 0;
                else paramSeats.Value = Int32.Parse(game.NrSeats);
                comm.Parameters.Add(paramSeats);

                var paramPrice = comm.CreateParameter();
                paramPrice.ParameterName = "@price_per_ticket";
                paramPrice.Value = game.Price;
                comm.Parameters.Add(paramPrice);

                var paramDate = comm.CreateParameter();
                paramDate.ParameterName = "@game_date";
                paramDate.Value = game.Date;
                comm.Parameters.Add(paramDate);



                var result = comm.ExecuteNonQuery();
                log.InfoFormat("Saved count {0}", result);
            }
        }

        public void Update(Game game)
        {
            var con = DBUtils.getConnection(props);
            log.InfoFormat("Updating game with id {0} with {1}", game.Id, game);
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "update games set team1=@team1, team2=@team2, game_type=@game_type, available_seats=@available_seats, price_per_ticket=@price_per_ticket, game_date=@game_date where id=@id";

                var paramTeam1 = comm.CreateParameter();
                paramTeam1.ParameterName = "@team1";
                paramTeam1.Value = game.Team1;
                comm.Parameters.Add(paramTeam1);

                var paramTeam2 = comm.CreateParameter();
                paramTeam2.ParameterName = "@team2";
                paramTeam2.Value = game.Team2;
                comm.Parameters.Add(paramTeam2);

                var paramGameType = comm.CreateParameter();
                paramGameType.ParameterName = "@game_type";
                paramGameType.Value = game.GameType;
                comm.Parameters.Add(paramGameType);

                var paramSeats = comm.CreateParameter();
                paramSeats.ParameterName = "@available_seats";
                if (game.NrSeats.Equals("SOLD OUT"))
                {
                    paramSeats.Value = 0;
                }
                else paramSeats.Value = Int32.Parse(game.NrSeats);
                comm.Parameters.Add(paramSeats);

                var paramPrice = comm.CreateParameter();
                paramPrice.ParameterName = "@price_per_ticket";
                paramPrice.Value = game.Price;
                comm.Parameters.Add(paramPrice);

                var paramDate = comm.CreateParameter();
                paramDate.ParameterName = "@game_date";
                paramDate.Value = game.Date;
                comm.Parameters.Add(paramDate);

                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = game.Id;
                comm.Parameters.Add(paramId);

                var result = comm.ExecuteNonQuery();
                log.InfoFormat("Updated count {0}", result);

            }
        }
    }
}
