using Blinky.Requests;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blinky
{
    public partial class ClaymoreClient
    {
        public string ClaymoreRequest(string method)
        {
            //"miner_getstat1"
            //"miner_reboot"
            var requestClaymore = new ClaymoreRequest()
            {
                Method = method
            };

            return JsonConvert.SerializeObject(requestClaymore);
        }
    }
}
