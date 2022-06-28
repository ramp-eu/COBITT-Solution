using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dihweb.ViewModels
{
    public class SubscriptionsVM
    {
        [Required]
        public string Description { get; set; }
    }
}
