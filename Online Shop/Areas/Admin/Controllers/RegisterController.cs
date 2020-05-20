using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Shop.Areas.Admin.Controllers
{
    public class RegisterController : BaseLoginController
    {
        // GET: Admin/Register
        public ActionResult Index()
        {
            return View();
        }
    }
}