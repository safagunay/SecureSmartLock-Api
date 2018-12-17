using LockerApi.Admin;
using LockerApi.Models;
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

        //POST api/Device/DeviceList
        [Route("DeviceList")]
        public IEnumerable<string> DeviceList([FromBody] string password)
        {
            var list = new List<string>();
            if (!string.IsNullOrEmpty(password) && password == _password)
            {
                using (ApplicationDbContext dbContext = new ApplicationDbContext())
                {

                    foreach (var item in dbContext.Devices)
                    {
                        var email = "null";
                        if (item.User_Id != null)
                            email = UserManager.FindById(item.User_Id).Email;

                        list.Add(string.Format("id={0}, Name={1}, Code={2}, Email={3}",
                            item.Id, item.Name, item.Code, email));
                    }
                }
            }
            return list;
        }
    }
}
