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
        [AuthorizeUser(opcion: "read", aplicacion: "Gestionar Roles")]
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

        [AuthorizeUser(opcion: "create", aplicacion: "Gestionar Roles")]
        public ActionResult NuevoRol()
        {
            return View();
        }

        [AuthorizeUser(opcion: "create", aplicacion: "Gestionar Roles")]
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

        [AuthorizeUser(opcion: "update", aplicacion: "Gestionar Roles")]
        public ActionResult EditarRol(int id)
        {
            ROL model = new ROL();
            using (MiSistemaEntities db = new MiSistemaEntities())
            {
                model = db.ROL.Find(id);
            }
            return View(model);
        }

        [AuthorizeUser(opcion: "update", aplicacion: "Gestionar Roles")]
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

        [AuthorizeUser(opcion: "delete", aplicacion: "Gestionar Roles")]
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

        [AuthorizeUser(opcion: "update", aplicacion: "Gestionar Roles")]
        public ActionResult VerOpciones(int id)
        {
            List<OPCIONES> opciones = new List<OPCIONES>();
            using (MiSistemaEntities db = new MiSistemaEntities())
            {
                opciones = db.OPCIONES.ToList();
                ViewBag.ListaOpciones = db.ROL.Find(id).OPCIONES.ToList();
            }
            ViewBag.ID = id;
            
            return View(opciones);
        }

        [AuthorizeUser(opcion: "update", aplicacion: "Gestionar Roles")]
        public ActionResult OpcionesRol(int idOpcion, int idRol)
        {
            OPCIONES opcion = new OPCIONES();
            ROL rol = new ROL();
            using (MiSistemaEntities db = new MiSistemaEntities())
            {
                opcion = db.OPCIONES.Find(idOpcion);
                rol = db.ROL.Find(idRol);
                rol.OPCIONES.Add(opcion);
                db.SaveChanges();
            }
            String ruta = "VerOpciones/"+idRol;
            return RedirectToAction(ruta);
        }

        [AuthorizeUser(opcion: "update", aplicacion: "Gestionar Roles")]
        public ActionResult EliminarOpcionesRol(int idOpcion, int idRol)
        {
            OPCIONES opcion = new OPCIONES();
            ROL rol = new ROL();
            using (MiSistemaEntities db = new MiSistemaEntities())
            {
                opcion = db.OPCIONES.Find(idOpcion);
                rol = db.ROL.Find(idRol);
                rol.OPCIONES.Remove(opcion);
                db.SaveChanges();
            }
            String ruta = "VerOpciones/" + idRol;
            return RedirectToAction(ruta);
        }
    }
}