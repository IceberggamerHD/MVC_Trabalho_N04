﻿using MVC_12H_N04.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC_12H_N04.Controllers
{
    public class LoginController : Controller
    {
        LoginBd _bd = new LoginBd();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel dados)
        {
            if (ModelState.IsValid)
            {
                UtilizadoresModel utilizador = _bd.ValidarLogin(dados);
                if (utilizador == null)
                {
                    ModelState.AddModelError("", "Login falhou. Tente novamente.");
                    return View(dados);
                }
                else
                {
                    Session["perfil"] = utilizador.Perfil;
                    Session["username"] = utilizador.Username;
                    FormsAuthentication.SetAuthCookie(utilizador.Username, false);

                    if (Request.QueryString["ReturnUrl"] == null)
                        return RedirectToAction("Index", "Home");
                    else
                        return Redirect(Request.QueryString["ReturnUrl"].ToString());
                }
            }
            return View(dados);
        }
    }
}