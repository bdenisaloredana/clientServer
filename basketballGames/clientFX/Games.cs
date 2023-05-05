using models;
using services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace clientFX
{
    public partial class Games : Form, IObserver
    {
        public IList<Game> ModelGames;
        private IGamesService gameService;
        private Employee employee;
        
        public Games(IGamesService gamesService, Employee employee)
        {
            this.gameService = gamesService;
            this.employee = employee;
            InitializeComponent();
            InnitGrid();
        }
        public Games()
        {
            InitializeComponent();

        }

        public void SetService(IGamesService gameService)
        {
            this.gameService = gameService;
        }
        public void SetEmployee(Employee employee) { 
            
            this.employee = employee;
        }
        private void ChatWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(e.CloseReason == CloseReason.UserClosing)
            {
                
                Application.Exit();
            }
        }

        public void UpdateGames(IList<Game> updatedGames)
        {
            ModelGames.Clear();
            foreach (Game game in updatedGames)
            {
                ModelGames.Add(game);
            }
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = ModelGames;
        }
        public void SetModel()
        {
            ModelGames = new List<Game>();
            IList<Game> games = this.gameService.FindAll();
            foreach (Game game in games)
            {
                ModelGames.Add(game);
            }

        }
        private void UpdateModel(IList<Game> updatedGames)
        {
            ModelGames.Clear();
            foreach (Game game in updatedGames)
            {
                ModelGames.Add(game);
            }
        }
        public void InnitGrid()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Id",
                DataPropertyName = "Id",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                Visible = false

            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Team1",
                DataPropertyName = "Team1",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Team2",
                DataPropertyName = "Team2",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Type",
                DataPropertyName = "GameType",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Price",
                DataPropertyName = "Price",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Available Seats",
                DataPropertyName = "NrSeats",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            dataGridView1.DataSource = this.ModelGames;
        }
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e != null && e.Value != null && e.Value.Equals("SOLD OUT"))
                e.CellStyle.ForeColor = Color.Red;

        }
        private void buyBttn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                Game game = (Game)this.dataGridView1.SelectedRows[0].DataBoundItem;
                if (game != null && !game.NrSeats.Equals("SOLD OUT"))
                {
                    string clientName = clientNameTxtBox.Text;
                    int nrSeats = Int32.Parse(nrSeatsTxtBox.Text);
                    int currentNrSeats = Int32.Parse(game.NrSeats);
                    if (Int32.Parse(game.NrSeats) - nrSeats >= 0)
                    {
                        if (clientName != null & nrSeats != 0)
                        {
                            this.gameService.BuyTicket(game, nrSeats, clientName);
                            MessageBox.Show("Sucessfuly bought!");
                        }
                    }

                }
                else MessageBox.Show("Game is sold out!");
            }
        }

        private void logoutBttn_Click(object sender, EventArgs e)
        {
            gameService.Logout(employee, this);
            Application.Exit();

        }

        private void availableBttn_Click(object sender, EventArgs e)
        {
            IList<Game> nonSoldOutGames = this.gameService.GetNonSoldOutGames();
            UpdateModel(nonSoldOutGames);
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
            dataGridView1.DataSource = ModelGames;

        }

        private void refreshBttn_Click(object sender, EventArgs e)
        {
            IList<Game> games = gameService.FindAll();
            UpdateModel(games);
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
            dataGridView1.DataSource = ModelGames;
        }

        public void Update(IList<Game> updatedGames)
        {
            dataGridView1.BeginInvoke(() =>
            {
                UpdateGames(updatedGames);
            });
        }
    }
}
