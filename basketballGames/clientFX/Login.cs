using models;
using services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace clientFX
{
    public partial class Login : Form
    {
        private IGamesService service;
        private Games form;
        public Login()
        {
            InitializeComponent();
        }
        public Login(IGamesService service)
        {
            InitializeComponent();
            this.service = service;
            form = new Games();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String username = usernameTxtBox.Text;
            String password = passwordTxtBox.Text;
            try
            {
                if (username != null & password != null)
                {
                    this.service.Login(new Employee(username, password), form);
                    this.usernameTxtBox.Clear();
                    this.passwordTxtBox.Clear();
                    form.SetService(service);
                    form.SetEmployee(new Employee(username, password));
                    form.SetModel();
                    form.InnitGrid();
                    this.Hide();
                    form.Show();
                }
                else
                    MessageBox.Show("Complete all fields!");
            }catch(GamesException ge)
            {
                MessageBox.Show(ge.Message);
            }
        }
    }
}
