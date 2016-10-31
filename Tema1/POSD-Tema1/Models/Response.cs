using POSD_Tema1.Services;
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
        public object data { get; set; }
        public List<object> allData { get; set; }

        private ResourceService _resourceService;

        public Response() {
            _resourceService = new ResourceService();

            requestCode = 200;
            message = "OK";
            fullMessage = "";
            data = new object { };
            allData = _resourceService.GetAllResources();
        }

        public void SetResponse(int requestCode, string message, string fullMessage, object data) {
            this.requestCode = requestCode;
            this.message = message;
            this.fullMessage = fullMessage;
            this.data = data;
            this.allData = _resourceService.GetAllResources();
        }
    }
}
