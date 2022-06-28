using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dihweb.Models
{
    public class DeviceType
    {
        public int DeviceTypeID { get; set; }
        public string Type { get; set; }

        [Display(Name = "Device Type")]
        public string IOTAgentType { get; set; }

        public List<Devices> Devices { get; set; }
    }
}
