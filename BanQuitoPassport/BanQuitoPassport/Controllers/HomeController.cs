using BanQuitoPassport.Filters;
using BanQuitoPassport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace BanQuitoPassport.Controllers
{
    public class HomeController : Controller
    {
        [AuthorizeUser(opcion:"Leer",aplicacion:"Banco")]
        public ActionResult Index()
        {
            return View();
        }

        [AuthorizeUser(opcion: "Leer", aplicacion: "Banco")]
        public ActionResult GestAplicacion()
        {
            ViewBag.Message = "Bienvenido al modulo para gestionar las aplicaciones del sistema";

            return View();
        }

        [AuthorizeUser(opcion: "Leer", aplicacion: "Banco")]
        public ActionResult GestUsuarios()
        {
            ViewBag.Message = "Bienvenido al modulo para gestionar los usuarios del sistema";

            return View();
        }

        [AuthorizeUser(opcion: "Leer", aplicacion: "Banco")]
        public ActionResult Depositos()
        {
            ViewBag.Message = "Bienvenido al Modulo de depositos";

            return View();
        }

        [AuthorizeUser(opcion: "Leer", aplicacion: "Banco")]
        public ActionResult Saldo()
        {
            ViewBag.Message = "Bienvenido al Modulo de saldo";

            return View();
        }

        [AuthorizeUser(opcion: "Leer", aplicacion: "Banco")]
        public ActionResult Transacciones()
        {
            ViewBag.Message = "Bienvenido al Modulo de transacciones";

            return View();
        }
    }
}