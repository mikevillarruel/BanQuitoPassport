using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanQuitoPassport.Filters;
using BanQuitoPassport.Models;

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
            UsuariosController usuarioC = new UsuariosController();
            Pass = usuarioC.Encriptar(Pass);
            AUDITORIA auditoria = new AUDITORIA();
            try
            {
                using (Models.MiSistemaEntities db = new Models.MiSistemaEntities())
                {
                    var oUser = (from us in db.USUARIO
                                 where us.IDENTIFICADOR == User && us.CONTRASENA == Pass
                                 select us).FirstOrDefault();
                    var oUserId = (from us in db.USUARIO
                                 where us.IDENTIFICADOR == User
                                 select us).FirstOrDefault();
                    auditoria = (from aud in db.AUDITORIA
                                 join us in db.USUARIO on aud.ID_AUDITORIA equals oUserId.ID_AUDITORIA
                                 select aud).FirstOrDefault();

                    if (oUser == null)
                    {
                        if (oUserId != null)
                        {
                            //Inicio fallido

                            auditoria.FALLIDOS = auditoria.FALLIDOS + 1;
                            db.Entry(auditoria).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();

                        }
                        ViewBag.Error = "Usuario o contraseña invalida";
                        return View();
                    }
                    //Inicio exitoso
                    auditoria.EXITOSOS = auditoria.EXITOSOS + 1;
                    db.Entry(auditoria).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
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