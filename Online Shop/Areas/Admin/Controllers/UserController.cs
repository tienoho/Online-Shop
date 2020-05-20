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
    public class UserController : BaseController
    {
        // GET: Admin/User
        //Phân trang bằng PagedList
        public ActionResult Index(string searchString, int page = 1, int pageSize = 2)
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
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                //nhập chuyển password sang mã hóa md5
                if (!string.IsNullOrEmpty(user.Password))
                {
                    var encrytedMd5Pass = Encryptor.MD5Hash(user.Password);
                    user.Password = encrytedMd5Pass;
                }
                var result = dao.Update(user);
                if (result)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhập user không thành công");
                }
            }
            return View(user);
        }
        public ActionResult Delete(long id)
        {
            new UserDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}