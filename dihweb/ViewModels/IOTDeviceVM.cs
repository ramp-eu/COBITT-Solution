using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dihweb.ViewModels
{
    public class IOTDeviceVM
    {
        [Required]
        [Display(Name= "Device ID")]
        public string DeviceID { get; set; }

        [Display(Name = "Entity Name")]
        public string EntityName { get; set; }

        [Display(Name = "Entity Type")]
        public string EntityType { get; set; }

        [Display(Name = "Endpoint")]
        public string Endpoint { get; set; }

        [Display(Name = "Device Type")]
        public int DeviceTypeId { get; set; }
    }
}
