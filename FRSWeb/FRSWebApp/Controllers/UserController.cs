using FRS.BLL.Interfaces;
using FRS.Entities;
using FRSWebApp.Models.Login;
using System;
using System.Web.Mvc;
using System.Web.Security;

namespace FRSWebApp.Controllers
{
    public class UserController : Controller
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: User
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(SignInLoginVM signInLoginVM)
        {
            if (ModelState.IsValid)
            {
                HashedLoginVM hashedLoginVM = new HashedLoginVM()
                {
                    Username = signInLoginVM.Username,
                    Password = Utils.HashUtils.ComputeHash(signInLoginVM.Password)
                };

                if (_userService.Login(new User()
                {
                    Username = hashedLoginVM.Username,
                    HashPassword = hashedLoginVM.Password
                }))
                {
                    FormsAuthentication.SetAuthCookie(signInLoginVM.Username, true);

                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(CreateLoginVM createLoginVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    HashedLoginVM hashedLoginVM = new HashedLoginVM()
                    {
                        Username = createLoginVM.Username,
                        Password = Utils.HashUtils.ComputeHash(createLoginVM.Password)
                    };

                    _userService.AddUser(new User() { Username = hashedLoginVM.Username, HashPassword = hashedLoginVM.Password });
                    return RedirectToAction("Login", "Account");
                }
                catch (Exception ex)
                {
                    //Logger.Error(ex.Message);
                    return View();
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "PrintEdition");
        }

        [AllowAnonymous]
        public ActionResult AccesIsDenied()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult ShowLinksForAuthUser()
        {
            return PartialView("~/Views/PartialViews/_DisplayLinks.cshtml");
        }

        [Authorize]
        public ActionResult Subscription()
        {
            return View();
        }

        [Authorize]
        public ActionResult Subscription(int userId)
        {
            _userService.AddSubInfo(userId);
            return RedirectToAction("SubInfo", userId);
        }

        [Authorize]
        public ActionResult SubInfo(int userId)
        {
            return View(_userService.GetSubInfo(userId));
        }
    }
}