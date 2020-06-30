using BanQuitoPassport.Filters;
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
        [AuthorizeUser(opcion: "read", aplicacion: "Gestionar Usuarios")]
        public ActionResult GestUsuarios()
        {
            ViewBag.Message = "Bienvenido al Modulo para gestionar los usuarios del sistema";
            List<USUARIO> lst;
            List<DatosUsuario> lista = new List<DatosUsuario>();
            using (MiSistemaEntities db = new MiSistemaEntities())
            {
                lst = db.USUARIO.ToList();
                foreach (USUARIO us in lst) {
                    DatosUsuario datosUsuario = new DatosUsuario(us, us.EMPLEADO, us.ESTADO);
                    lista.Add(datosUsuario);
                }
            }

            return View(lista);
        }

        [AuthorizeUser(opcion: "create", aplicacion: "Gestionar Usuarios")]
        public ActionResult NuevoUsuario()
        {
            return View();
        }

        [AuthorizeUser(opcion: "create", aplicacion: "Gestionar Usuarios")]
        [HttpPost]
        public ActionResult NuevoUsuario(DatosUsuario model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (MiSistemaEntities db = new MiSistemaEntities())
                    {
                        model.us.EMPLEADO = model.emp;
                        model.us.ESTADO = db.ESTADO.Find(1);
                        model.us.AUDITORIA = new AUDITORIA();
                        model.us.AUDITORIA.EXITOSOS = 0;
                        model.us.AUDITORIA.FALLIDOS = 0;
                        model.us.AUDITORIA.NOAUTORIZADOS = 0;
                        model.us.ID_ROL = 10;
                        db.USUARIO.Add(model.us);
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

        [AuthorizeUser(opcion: "udpate", aplicacion: "Gestionar Usuarios")]
        public ActionResult EditarUsuario(int id)
        {
            USUARIO model = new USUARIO();
            DatosUsuario us;
            using (MiSistemaEntities db = new MiSistemaEntities())
            {
                model = (from d in db.USUARIO where d.ID_USUARIO == id select d).FirstOrDefault();
                us = new DatosUsuario(model, model.EMPLEADO, model.ESTADO);
            }
            return View(us);
        }

        [AuthorizeUser(opcion: "udpate", aplicacion: "Gestionar Usuarios")]
        [HttpPost]
        public ActionResult EditarUsuario(DatosUsuario model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (MiSistemaEntities db = new MiSistemaEntities())
                    {
                        db.Entry(model.emp).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        db.Entry(model.us).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        db.Entry(model.est).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }

                    return RedirectToAction("GestUsuarios");
                }
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [AuthorizeUser(opcion: "delete", aplicacion: "Gestionar Usuarios")]
        [HttpGet]
        public ActionResult EliminarUsuario(int id)
        {
            using (MiSistemaEntities db = new MiSistemaEntities())
            {
                var usuario = (from d in db.USUARIO where d.ID_USUARIO == id select d).FirstOrDefault();
                usuario.ESTADO = db.ESTADO.Find(4);
                try
                {
                    db.Entry(usuario).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return RedirectToAction("GestUsuarios");
                }
            }
            return RedirectToAction("GestUsuarios");
        }

        [VerificaSession(Disable = true)]
        public ActionResult Registrar()
        {
            return View();
        }

        [VerificaSession(Disable = true)]
        [HttpPost]
        public ActionResult Registrar(DatosUsuario model)
        {
            String contrasena="";
            try
            {
                if (ModelState.IsValid)
                {
                    using (MiSistemaEntities db = new MiSistemaEntities())
                    {
                        model.us.EMPLEADO = model.emp;
                        model.us.ESTADO = db.ESTADO.Find(1);
                        model.us.AUDITORIA = new AUDITORIA();
                        model.us.AUDITORIA.EXITOSOS = 0;
                        model.us.AUDITORIA.FALLIDOS = 0;
                        model.us.AUDITORIA.NOAUTORIZADOS = 0;
                        model.us.ID_ROL = 10;
                        contrasena = GenerarContraseña();
                        model.us.CONTRASENA = contrasena;
                        db.USUARIO.Add(model.us);
                        db.SaveChanges();
                    }
                    return Content("<script>alert('Su contraseña: "+contrasena
                        +" fue enviada a su correo electronico');</script>");
                }
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public String GenerarContraseña() {
            Random rdn = new Random();
            string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890%$#@";
            int longitud = caracteres.Length;
            char letra;
            int longitudContrasenia = 15;
            string contraseniaAleatoria = string.Empty;
            for (int i = 0; i < longitudContrasenia; i++)
            {
                letra = caracteres[rdn.Next(longitud)];
                contraseniaAleatoria += letra.ToString();
            }
            return contraseniaAleatoria;
        }

    }
}