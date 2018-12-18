using LockerApi.Admin;
using LockerApi.Models;
using LockerApi.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace LockerApi.Controllers
{
    [RoutePrefix("api/Admin")]
    public class AdminController : ApiController
    {
        private const string _password = AdminSettings.PASSWORD;
        private ApplicationUserManager UserManager
        {
            get
            {
                return Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        //POST api/Device/AddDevice
        [Route("AddDevice")]
        public IHttpActionResult AddDevice(AddDeviceBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model.Password == _password)
            {
                using (ApplicationDbContext dbContext = new ApplicationDbContext())
                {
                    var device = new Device()
                    {
                        CodeHash = HashService.HashDeviceCode(model.DeviceCode),
                        SecretKeyHash = HashService.HashDeviceSecret(model.DeviceSecret),
                        CreatedOnUTC = DateService.getCurrentUTC(),
                        IsDeleted = false

                    };
                    dbContext.Devices.Add(device);
                    try
                    {
                        dbContext.SaveChanges();
                    }
                    catch
                    {
                        ModelState.AddModelError("DbInsert", "DeviceCode and SecretCode must be unique.");
                        return BadRequest(ModelState);
                    }

                    return Ok(device.Id);
                }
            }
            ModelState.AddModelError("Password", "Incorrect password.");
            return BadRequest(ModelState);
        }

        //POST api/Device/Device
        [Route("Device")]
        public IHttpActionResult Device(GetDeviceBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model.Password == _password)
            {
                using (ApplicationDbContext dbContext = new ApplicationDbContext())
                {
                    var device = dbContext.Devices.Find(model.DeviceId);
                    if (device == null)
                    {
                        return Ok(string.Format("No device was found with the given id({0}).", model.DeviceId));
                    }
                    string defaultVal = "null";
                    var email = defaultVal;
                    if (device.User_Id != null)
                        email = UserManager.FindById(device.User_Id).Email;
                    return Ok(string.Format("id={0,-10}, Name={1,-20}, Code={2,-20}, Email={3,-20}",
                            device.Id, device.Name ?? defaultVal, device.Code ?? defaultVal, email));
                }
            }
            ModelState.AddModelError("Password", "Incorrect password.");
            return BadRequest(ModelState);
        }

        //POST api/Device/DeviceList
        [Route("DeviceList")]
        public IEnumerable<string> DeviceList(DeviceListBindingModel model)
        {
            var list = new List<string>();
            if (!ModelState.IsValid)
            {
                list.Add("Password is required.");
                return list;
            }
            if (model.Password == _password)
            {
                using (ApplicationDbContext dbContext = new ApplicationDbContext())
                {

                    foreach (var item in dbContext.Devices)
                    {
                        string defaultVal = "null";
                        var email = defaultVal;
                        if (item.User_Id != null)
                            email = UserManager.FindById(item.User_Id).Email;

                        list.Add(string.Format("id={0,-20}, Name={1,-20}, Code={2,-20}, Email={3,-20}",
                            item.Id, item.Name ?? defaultVal, item.Code ?? defaultVal, email));
                    }
                }
            }
            else
                list.Add("Incorrect password");
            return list;
        }
    }
}
