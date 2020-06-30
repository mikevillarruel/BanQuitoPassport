﻿using BanQuitoPassport.Filters;
using BanQuitoPassport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanQuitoPassport.Controllers
{
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        [AuthorizeUser(opcion: "Leer", aplicacion: "Banco")]
        public ActionResult GestUsuarios()
        {
            ViewBag.Message = "Bienvenido al Modulo para gestionar los usuarios del sistema";
            List<USUARIO> lst;
            using (MiSistemaEntities db = new MiSistemaEntities())
            {
                lst = db.USUARIO.ToList();
            }

            return View(lst);
        }

        [AuthorizeUser(opcion: "Leer", aplicacion: "Banco")]
        public ActionResult NuevoUsuario()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NuevoUsuario(USUARIO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (MiSistemaEntities db = new MiSistemaEntities())
                    {
                        db.USUARIO.Add(model);
                        db.SaveChanges();
                    }
                    return RedirectToAction("GestUsuarios");
                }
                return View();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [AuthorizeUser(opcion: "Leer", aplicacion: "Banco")]
        public ActionResult EditarUsuario(int id)
        {
            USUARIO model = new USUARIO();
            using (MiSistemaEntities db = new MiSistemaEntities())
            {
                model = (from d in db.USUARIO where d.ID_USUARIO == id select d).FirstOrDefault();
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult EditarUsuario(USUARIO model)
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

                    return RedirectToAction("GestUsuarios");
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
        public ActionResult EliminarUsuario(int id)
        {
            using (MiSistemaEntities db = new MiSistemaEntities())
            {
                var usuario = (from d in db.USUARIO where d.ID_USUARIO == id select d).FirstOrDefault();
                db.USUARIO.Remove(usuario);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return RedirectToAction("GestUsuarios");
                }
            }
            return RedirectToAction("GestUsuarios");
        }
    }
}