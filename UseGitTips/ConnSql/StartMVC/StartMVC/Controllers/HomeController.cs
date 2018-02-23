using StartMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace StartMVC.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            NFineBaseEntities db = new NFineBaseEntities();
            List<UserEntity> userList = new List<UserEntity>();
            foreach (var item in db.Sys_User)
            {
                UserEntity entity = new UserEntity();
                entity.F_RealName = item.F_RealName;
                userList.Add(entity);
            }
            return View(userList);
        }

    }
}
