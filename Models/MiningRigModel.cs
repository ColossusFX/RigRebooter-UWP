using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blinky.Models
{
    public class MiningRigModel : ObservableCollection<MiningRigModel>
    {
        public MiningRigModel()
        {

        }
    }

    public class MiningRigs
    {
        public MiningRigs(string ip, int pin)
        {
            this.IP = ip;
            this.PinNumber = pin;
        }

        public string IP { get; set; }

        public int PinNumber { get; set; }
    }
}
