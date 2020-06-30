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
        public ActionResult Index()
        {
            return View();
        }

        [AuthorizeUser(opcion: "read", aplicacion: "Depositos")]
        public ActionResult Depositos()
        {
            ViewBag.Message = "Bienvenido al Modulo de depositos";

            return View();
        }

        [AuthorizeUser(opcion: "read", aplicacion: "Saldo")]
        public ActionResult Saldo()
        {
            ViewBag.Message = "Bienvenido al Modulo de saldo";

            return View();
        }

        [AuthorizeUser(opcion: "read", aplicacion: "Transacciones")]
        public ActionResult Transacciones()
        {
            ViewBag.Message = "Bienvenido al Modulo de transacciones";

            return View();
        }
    }
}