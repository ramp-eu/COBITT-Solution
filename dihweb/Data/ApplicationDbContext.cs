using dihweb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dihweb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Devices> Devices { get; set; }
        public DbSet<DeviceStatus> DeviceStatus { get; set; }
        public DbSet<DevicesLog> DevicesLog { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<DeviceType> DeviceType { get; set; }
        public DbSet<OrderProcess> OrderProcess { get; set; }
        public DbSet<MachineOrderStatus> MachineOrderStatus { get; set; }
    }
}
