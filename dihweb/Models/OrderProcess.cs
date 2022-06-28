using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dihweb.Models
{
    public class OrderProcess
    {
        public int OrderProcessId { get; set; }

        public DateTime Timestamp { get; set; }

        public int OrdersId { get; set; }
        public Orders Orders { get; set; }

        public int DevicesId { get; set; }
        public Devices Devices { get; set; }

        public int DeviceStatusId { get; set; }
        public DeviceStatus DeviceStatus { get; set; }
    }
}
