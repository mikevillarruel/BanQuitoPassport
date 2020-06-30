using BanQuitoPassport.Controllers;
using BanQuitoPassport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanQuitoPassport.Filters
{
    public class VerificaSession : ActionFilterAttribute
    {
        private USUARIO oUsuario;
        public bool Disable { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                base.OnActionExecuting(filterContext);

                oUsuario = (USUARIO)HttpContext.Current.Session["User"];
                if (Disable == true) {
                    return;
                }
                if (oUsuario == null)
                {
                   
                        if (filterContext.Controller is AccesoController == false)
                        {
                            filterContext.HttpContext.Response.Redirect("/Acceso/Login");
                        }
                }

            }
            catch (Exception)
            {
                filterContext.Result = new RedirectResult("~/Acceso/Login");
            }

        }
    }
}