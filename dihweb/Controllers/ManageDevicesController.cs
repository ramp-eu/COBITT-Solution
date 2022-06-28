using dihweb.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dihweb.Controllers
{
    public class ManageDevicesController : Controller
    {
        private ApplicationDbContext _db;

        public ManageDevicesController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var devices = _db.Devices.ToList();
            return View(devices);
        }

        public ActionResult Delete(int ID)
        {
            var deviceLog = _db.DevicesLog.Where(x => x.DevicesId == ID).ToList();
            foreach (var item in deviceLog)
            {
                _db.DevicesLog.Remove(item);
            }

            var machineOrderStatus = _db.MachineOrderStatus.Where(x => x.DevicesId == ID).ToList();
            foreach (var item in machineOrderStatus)
            {
                _db.MachineOrderStatus.Remove(item);
            }

            var orderProcess = _db.OrderProcess.Where(x => x.DevicesId == ID).ToList();
            foreach (var item in orderProcess)
            {
                _db.OrderProcess.Remove(item);
            }

            var device = _db.Devices.Find(ID);
            _db.Devices.Remove(device);

            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
