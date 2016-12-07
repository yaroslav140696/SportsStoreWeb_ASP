using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Infrastructure.Abstrtact;
using SportsStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class AccountController : Controller
    {
        IAuthProvider authProvider;
        IRepository<User> repository;

        public AccountController(IAuthProvider ap, IRepository<User> repos)
        {
            authProvider = ap;
            repository = repos;
        }
        public ViewResult Login()
        {
            Session.Abandon();
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl, CurrentUser user)
        {
            if (ModelState.IsValid)
            {
                if (authProvider.Authenticate(model.Username, model.Password))
                {
                    user.Data = repository.Items.FirstOrDefault(x => x.Email == "admin@gmail.com");
                    user.isAdmin = true;
                    return Redirect(returnUrl);
                }
                user.Data = repository.Items.FirstOrDefault(x => x.Email == model.Username);

                if (user.Data != null)
                {
                    if (user.Data.Password == model.Password)
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        user = null;
                    }
                }
            }
            ModelState.AddModelError("", "Неправильний логин или пароль");
            return View(model);
        }
        public PartialViewResult UserBar(CurrentUser user)
        {
            if (user.Data == null)
            {
                user.Data = repository.Items.FirstOrDefault(x => x.Email == "null@null");
                user.isAdmin = false;
            }
            return PartialView(user);
        }
        public ViewResult Registration()
        {
            Session.Abandon();
            return View(new User());
        }
        [HttpPost]
        public ActionResult Registration(User user, CurrentUser currentuser)
        {
            if (ModelState.IsValid)
            {
                var tmp = repository.Items.FirstOrDefault(x => x.Email == user.Email);
                if (tmp != null)
                {
                    ModelState.AddModelError("", "Пользователь с таким E-mail уже зарегестрирован");
                    return View(user);
                }
                else
                {
                    repository.SaveItem(user);
                    TempData["Message"] = string.Format("Уважаемый {0}, вы успешно зарегестрированы", user.FirtsName);
                    currentuser.Data = repository.Items.FirstOrDefault(x => x.Email == user.Email);
                    return RedirectToAction("List", "Product");
                }
            }
            else
            {
                return View(user);
            }
        }
        public ActionResult Logout(CurrentUser user)
        {
            Session.Abandon();
            user.Data = null;
            user.isAdmin = false;
            return RedirectToAction("List", "Product");
        }
    }
}