using Model.Dao;
using Online_Shop.Areas.Admin.Models;
using Online_Shop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Shop.Areas.Admin.Controllers
{
    public class LoginController : BaseLoginController
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.PassWord));
                if (result == 1)
                {
                    //lấy ra id của user
                    var user = dao.GetById(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserID = user.ID;
                    //gán user vào session
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản của bạn không tồn tại.");
                }
                else if (result == 2)
                {
                    ModelState.AddModelError("", "Bạn đã nhập sai mật khẩu.");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản của bạn đang bị khóa.");
                }
            }
            return View("Index");
        }
        public ActionResult Logout()
        {
            return View();
        }
    }
}