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
    public class AplicacionesController : Controller
    {
        // GET: Aplicaciones
        [AuthorizeUser(opcion: "read", aplicacion: "Gestionar Aplicaciones")]
        public ActionResult GestAplicaciones()
        {
            ViewBag.Message = "Bienvenido al Modulo para gestionar las aplicaciones del sistema";
            List<APLICACION> lst;
            using (MiSistemaEntities db = new MiSistemaEntities())
            {
                lst = (from d in db.APLICACION select d).ToList();
            }

            return View(lst);
        }

        [AuthorizeUser(opcion: "create", aplicacion: "Gestionar Aplicaciones")]
        public ActionResult NuevaAplicacion()
        {
            return View();
        }

        [AuthorizeUser(opcion: "create", aplicacion: "Gestionar Aplicaciones")]
        [HttpPost]
        public ActionResult NuevaAplicacion(APLICACION model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (MiSistemaEntities db = new MiSistemaEntities())
                    {
                        var aplicacion = new APLICACION();
                        aplicacion.NOMBREA = model.NOMBREA;
                        aplicacion.DESCRIPCIONA = model.DESCRIPCIONA;
                        db.APLICACION.Add(aplicacion);
                        db.SaveChanges();
                    }
                    return RedirectToAction("GestAplicaciones");
                }
                return View();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [AuthorizeUser(opcion: "update", aplicacion: "Gestionar Aplicaciones")]
        public ActionResult EditarAplicacion(int id)
        {
            APLICACION model = new APLICACION();
            using (MiSistemaEntities db = new MiSistemaEntities())
            {
                var tabla = db.APLICACION.Find(id);
                model.ID_APLICACION = tabla.ID_APLICACION;
                model.NOMBREA = tabla.NOMBREA;
                model.DESCRIPCIONA = tabla.DESCRIPCIONA;
            }
            return View(model);
        }

        [AuthorizeUser(opcion: "update", aplicacion: "Gestionar Aplicaciones")]
        [HttpPost]
        public ActionResult EditarAplicacion(APLICACION model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (MiSistemaEntities db = new MiSistemaEntities())
                    {
                        var aplicacion = db.APLICACION.Find(model.ID_APLICACION);
                        aplicacion.NOMBREA = model.NOMBREA;
                        aplicacion.DESCRIPCIONA = model.DESCRIPCIONA;
                        db.Entry(aplicacion).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }

                    return RedirectToAction("GestAplicaciones");
                }
                return View();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [AuthorizeUser(opcion: "delete", aplicacion: "Gestionar Aplicaciones")]
        [HttpGet]
        public ActionResult EliminarAplicacion(int id)
        {
            ROL model = new ROL();
            using (MiSistemaEntities db = new MiSistemaEntities())
            {
                var tabla = db.APLICACION.Find(id);
                db.APLICACION.Remove(tabla);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return RedirectToAction("GestAplicaciones");
                }
            }
            return RedirectToAction("GestAplicaciones");
        }
    }
}