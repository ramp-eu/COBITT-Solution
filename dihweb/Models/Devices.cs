using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dihweb.Models
{
    public class Devices
    {
        public int ID { get; set; }
        [Display(Name = "Device Name")]
        public string DeviceName { get; set; }
        public int? OrderIndex { get; set; }

        public List<DevicesLog> DevicesLog { get; set; }


        public int DeviceTypeID { get; set; }
        public DeviceType DeviceType { get; set; }

        public List<OrderProcess> OrderProcess { get; set; }
        public List<MachineOrderStatus> MachineOrderStatus { get; set; }
    }
}
