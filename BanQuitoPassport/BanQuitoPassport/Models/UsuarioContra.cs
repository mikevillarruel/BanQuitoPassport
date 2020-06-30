using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BanQuitoPassport.Models
{
    public class UsuarioContra
    {
        public USUARIO us { get; set; }

        [Display (Name="Contrasena Actual")]
        public String contraActual { get; set; }

        public UsuarioContra(USUARIO us, String contraActual)
        {
            this.us = us;
            this.contraActual = contraActual;
        }
        public UsuarioContra() { }
    }
}