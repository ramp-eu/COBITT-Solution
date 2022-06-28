using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dihweb.Models
{
    public class DeviceStatus
    {
        public int DeviceStatusId { get; set; }
        [Display(Name = "Status")]
        public string Status { get; set; }

        public List<DevicesLog> DevicesLog { get; set; }
        public List<OrderProcess> OrderProcess { get; set; }
    }
}
