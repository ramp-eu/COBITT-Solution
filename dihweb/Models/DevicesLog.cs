using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dihweb.Models
{
    public class DevicesLog
    {
        public int DevicesLogId { get; set; }
        public DateTime Time { get; set; }

        public int DeviceStatusId { get; set; }
        public DeviceStatus DeviceStatus { get; set; }

        public int DevicesId { get; set; }
        public Devices Devices { get; set; }
    }
}
