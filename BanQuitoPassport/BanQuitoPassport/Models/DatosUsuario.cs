using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanQuitoPassport.Models
{
    public class DatosUsuario
    {
        public USUARIO us { get; set; }
        public EMPLEADO emp { get; set; }
        public ESTADO est { get; set; }
        public DatosUsuario(USUARIO us, EMPLEADO emp, ESTADO est)
        {
            this.us = us;
            this.emp = emp;
            this.est = est;
        }
        public DatosUsuario() { }
    }
}