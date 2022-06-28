using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dihweb.Models
{
    public class Orders
    {
        public int OrdersId { get; set; }

        [Required]
        public string Name { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public bool isActive { get; set; }
        public DateTime OrderDate { get; set; }

        [Display(Name = "Order Status")]
        public int OrderStatusId { get; set; }
        public OrderStatus OrderStatus { get; set; }

        public List<OrderProcess> OrderProcess { get; set; }
        public List<MachineOrderStatus> MachineOrderStatus { get; set; }
    }
}
