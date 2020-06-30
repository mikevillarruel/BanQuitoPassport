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
                        APLICACION app = (from d in db.APLICACION where d.NOMBREA == aplicacion.NOMBREA select d).FirstOrDefault();
                        OPCIONES op = new OPCIONES();
                        List<String> opciones = new List<String>() { "read", "update", "create", "delete" };
                        foreach (String s in opciones)
                        {
                            op = new OPCIONES();
                            op.ID_APLICACION = app.ID_APLICACION;
                            op.NOMBREOP = s;
                            app.OPCIONES.Add(op);
                            db.SaveChanges();
                        }
                    }
                    return RedirectToAction("GestAplicaciones");
                }
                return View();
            }
            catch (Exception ex)
            {
                return View();
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
                var orders = (from d in db.OPCIONES where d.ID_APLICACION == tabla.ID_APLICACION select d).ToList();
                
                try
                {
                    foreach (OPCIONES o in orders)
                    {
                        db.OPCIONES.Remove(o);
                    }
                    db.SaveChanges();
                    db.APLICACION.Remove(tabla);
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