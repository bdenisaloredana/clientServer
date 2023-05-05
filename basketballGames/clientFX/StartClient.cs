using networking;
using services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientFX
{
    public class StartClient
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            IGamesService server = new GamesServicesProxy("127.0.0.1", 55556);
            Login win = new Login(server);
            Application.Run(win);
        }
    }
}
