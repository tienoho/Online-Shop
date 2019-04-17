using Model.Dao;
using Model.EF;
using Online_Shop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Online_Shop.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: Admin/User
        //Phân trang bằng PagedList
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            return View(model);
        }
        //phân trang bằng jquyery
        //public ActionResult Index()
        //{
        //    var dao = new UserDao();
        //    var model = dao.ListAll();
        //    return View(model);
        //}
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(User enitity)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var encryptedMd5Pas = Encryptor.MD5Hash(enitity.Password);
                enitity.Password = encryptedMd5Pas;
                enitity.CreatedDate = DateTime.Now;
                long id = dao.Insert(enitity);
                if (id > 0)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                   ModelState.AddModelError("", "Thêm người dùng không thành công.");
                }
            }
            return View(enitity);
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var user = new UserDao().ViewDetail(id);
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(User user)
        {
            return View();
        }
    }
}