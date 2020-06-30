using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanQuitoPassport.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string User, string Pass)
        {
            try
            {
                using (Models.MiSistemaEntities db = new Models.MiSistemaEntities())
                {
                    var oUser = (from us in db.USUARIO
                                 where us.IDENTIFICADOR == User && us.CONTRASENA == Pass
                                 select us).FirstOrDefault();
                    if (oUser == null)
                    {
                        ViewBag.Error = "Usuario o contraseña invalida";
                        return View();
                    }

                    Session["User"] = oUser;

                }

                return RedirectToAction("Index", "Home");

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }

        }

        public ActionResult CerrarSesion(string User, string Pass)
        {
            Session["User"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}