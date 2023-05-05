using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace networking
{
    [Serializable]
    public class ErrorResponse: IResponse
    {
        public string Message { set; get; }
        public ErrorResponse(string message) {
            this.Message = message;
        }
    }
}
