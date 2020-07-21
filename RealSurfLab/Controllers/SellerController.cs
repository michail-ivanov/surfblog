﻿using RealSurfLab.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealSurfLab.Controllers
{
    public class SellerController : Controller
    {
        SurfDbContext dbContext = new SurfDbContext();
        // GET: Seller
        public ActionResult Index()
        {
            var user = dbContext.Users.FirstOrDefault();
            
            return View(user);
        }
    }
}