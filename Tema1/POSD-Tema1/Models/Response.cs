using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSD_Tema1.Models
{
    public class Response
    {
        public int requestCode { get; set; }
        public string message { get; set; }
        public string fullMessage { get; set; }

        public Response() {
            requestCode = 200;
            message = "OK";
            fullMessage = "";
        }

        public void SetResponse(int requestCode, string message, string fullMessage) {
            this.requestCode = requestCode;
            this.message = message;
            this.fullMessage = fullMessage;
        }
    }
}
