﻿using RealSurfLab.DAL;
using RealSurfLab.Helpers;
using RealSurfLab.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RealSurfLab.Controllers
{
    public class RegisterController : Controller
    {

        SurfDbContext dbContext = new SurfDbContext();

        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register(User model, HttpPostedFileBase imageData)
        {
            if (ModelState.IsValid)
            {
                //регистрация
                if (model.Password != model.PasswordConfirm)
                {
                    ModelState.AddModelError(string.Empty, "Введенные пароли не совпадают!");
                    return View("Index", model);
                }

                var userInDb = dbContext.Users.FirstOrDefault(c => c.Nickname == model.Nickname);
                if (userInDb != null)
                {
                    ModelState.AddModelError(string.Empty, "Пользователь с таким псевдонимом уже существует!");
                    return View("Index", model);
                }

                if (imageData != null)
                {

                    model.Photo = ImageSaveHelper.SaveImage(imageData);

                }

                dbContext.Users.Add(model);
                dbContext.SaveChanges();

                FormsAuthentication.SetAuthCookie(model.Nickname, false);
                Session["UserId"] = model.Id.ToString();
                Session["Nickname"] = model.Nickname;
                Session["Photo"] = ImageUrlHelper.GetUrl(model.Photo);


                return RedirectToAction("Index", "Feed");
            }

            return View("Index",model);
        }
    }
}