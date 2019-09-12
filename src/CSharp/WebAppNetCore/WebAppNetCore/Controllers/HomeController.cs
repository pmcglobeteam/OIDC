﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebAppNetCore.Models;

namespace WebAppNetCore.Controllers
{
    public class HomeController : Controller
    {
        public IConfiguration Configuration
        {
            get;
            private set;
        }

        public HomeController(IConfiguration configuration)
        {
            Configuration = configuration ?? throw new ArgumentNullException("configuration");
        }

        public IActionResult Index()
        {
            ViewData["EditMyProfileUri"] = Configuration.EditMyProfileUri();
            ViewData[OpenIdConnectConstants.AccessToken] = HttpContext.GetTokenAsync(OpenIdConnectConstants.AccessToken).Result;

            ViewData["Origin"] = $"{Request.Scheme}://{Request.Host.Value}";
            ViewData["CheckSessionIframeUri"] = Configuration.CheckSessionIframeUri();

            var authorizationRequest = OpenIdConnectHelper.GenerateReauthenticateUri(HttpContext, Configuration);
            ViewData["Reauthenticate"] = authorizationRequest;

            return View();
        }
       
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }    
    }
}
