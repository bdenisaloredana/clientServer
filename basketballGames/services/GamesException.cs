using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace services
{
    public class GamesException: Exception
    {
        public GamesException() : base() { }

        public GamesException(String msg) : base(msg) { }

        public GamesException(String msg, Exception ex) : base(msg, ex) { }
    }
}
