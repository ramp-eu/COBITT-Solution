using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dihweb.Models
{
    public class MachineOrderStatus
    {
        public int MachineOrderStatusId { get; set; }
        public bool Status { get; set; }

        public int OrdersId { get; set; }
        public Orders Orders { get; set; }

        public int DevicesId { get; set; }
        public Devices Devices { get; set; }
    }
}
