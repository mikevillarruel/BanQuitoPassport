using BanQuitoPassport.Filters;
using BanQuitoPassport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanQuitoPassport.Controllers
{
    public class RolesController : Controller
    {
        // GET: Roles
        [AuthorizeUser(opcion: "Leer", aplicacion: "Banco")]
        public ActionResult GestRoles()
        {
            ViewBag.Message = "Bienvenido al Modulo para gestionar los roles del sistema";
            List<ROL> lst;
            using (MiSistemaEntities db = new MiSistemaEntities())
            {
                lst = (from d in db.ROL select d).ToList();
            }

            return View(lst);
        }

        [AuthorizeUser(opcion: "Leer", aplicacion: "Banco")]
        public ActionResult NuevoRol()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NuevoRol(ROL model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (MiSistemaEntities db = new MiSistemaEntities())
                    {
                        db.ROL.Add(model);
                        db.SaveChanges();
                    }
                    return RedirectToAction("GestRoles");
                }
                return View();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [AuthorizeUser(opcion: "Leer", aplicacion: "Banco")]
        public ActionResult EditarRol(int id)
        {
            ROL model = new ROL();
            using (MiSistemaEntities db = new MiSistemaEntities())
            {
                model = db.ROL.Find(id);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult EditarRol(ROL model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (MiSistemaEntities db = new MiSistemaEntities())
                    {
                        db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }

                    return RedirectToAction("GestRoles");
                }
                return View();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [AuthorizeUser(opcion: "Leer", aplicacion: "Banco")]
        [HttpGet]
        public ActionResult EliminarRol(int id)
        {
            using (MiSistemaEntities db = new MiSistemaEntities())
            {
                var tabla = db.ROL.Find(id);
                db.ROL.Remove(tabla);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception) {
                    return RedirectToAction("GestRoles");
                }
            }
            return RedirectToAction("GestRoles");
        }
    }
}