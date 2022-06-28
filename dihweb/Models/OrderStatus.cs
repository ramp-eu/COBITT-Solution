using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dihweb.Models
{
    public class OrderStatus
    {
        public int OrderStatusId { get; set; }
        public string Status { get; set; }

        public List<Orders> Orders { get; set; }
    }
}
