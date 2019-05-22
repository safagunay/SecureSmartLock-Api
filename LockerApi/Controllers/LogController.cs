using LockerApi.Models;
using LockerApi.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LockerApi.Controllers
{
    public class LogController : Controller
    {
        public ApplicationUserManager UserManager
        {
            get
            {
                return Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }
        private readonly DeviceService _deviceService = new DeviceService();
        private readonly DeviceActivityLogService _deviceActivityLogService = new DeviceActivityLogService();

        // GET: ActivityLog
        [HttpGet]
        public async Task<ActionResult> Index(int id)
        {
            //prepare page model
            ActivityLogViewModel model = new ActivityLogViewModel();
            var dev = _deviceService.GetById(id);
            if (dev != null)
            {
                model.DeviceId = dev.Id;
                model.DeviceName = dev.Name;
                model.DevFound = true;
                model.DeviceOwnerEmail = UserManager.GetEmail(dev.User_Id);
                model.LogList = await _deviceActivityLogService.getLogs(dev.Id);
            }

            return View(model);
        }
    }
}