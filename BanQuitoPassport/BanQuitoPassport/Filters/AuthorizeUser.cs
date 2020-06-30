using BanQuitoPassport.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanQuitoPassport.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AuthorizeUser : AuthorizeAttribute
    {
        private USUARIO usuario;
        private MiSistemaEntities db = new MiSistemaEntities();
        private String opcion, aplicacion;

        public AuthorizeUser(String opcion, String aplicacion)
        {
            this.opcion = opcion;
            this.aplicacion = aplicacion;
        }


        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                usuario = (USUARIO)HttpContext.Current.Session["User"];
                if (usuario != null)
                {                    
                    db = new MiSistemaEntities();
                    if (db.ROL.Find(usuario.ID_ROL).NOMBRER.Equals("Administrador"))
                    {
                        return;
                    }
                    foreach (OPCIONES app in db.ROL.Find(usuario.ID_ROL).OPCIONES)
                    {
                        if (getNombreDeOperacion(app.ID_OPCIONES).Equals(opcion) && aplicacion.Equals(getNombreAplicacion(app.ID_APLICACION)))
                        {
                            return;
                        }
                    }
                    filterContext.Result = new RedirectResult("~/Error/UnauthorizedOperation?operacion=" + opcion + "&modulo=" + aplicacion + "&msjeErrorExcepcion=");
                }
            }
            catch (Exception ex)
            {
                filterContext.Result = new RedirectResult("~/Error/UnauthorizedOperation?operacion=" + opcion + "&modulo=" + aplicacion + "&msjeErrorExcepcion=" + ex.Message);
            }
            

        }

        public string getNombreDeOperacion(int idOpcion)
        {
            var ope = from op in db.OPCIONES
                      where op.ID_OPCIONES == idOpcion
                      select op.NOMBREOP;
            String nombreOpcion;
            try
            {
                nombreOpcion = ope.First();
            }
            catch (Exception)
            {
                nombreOpcion = "";
            }
            return nombreOpcion;
        }

        public string getNombreAplicacion(int? idAplicacion)
        {
            var modulo = from m in db.APLICACION
                         where m.ID_APLICACION == idAplicacion
                         select m.NOMBREA;
            String nombreAplicacion;
            try
            {
                nombreAplicacion = modulo.First();
            }
            catch (Exception)
            {
                nombreAplicacion = "";
            }
            return nombreAplicacion;
        }
    }
}