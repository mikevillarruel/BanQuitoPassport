//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BanQuitoPassport.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class OPCIONES
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OPCIONES()
        {
            this.ROL = new HashSet<ROL>();
        }
    
        public int ID_OPCIONES { get; set; }
        public int ID_APLICACION { get; set; }
        public string NOMBREOP { get; set; }
    
        public virtual APLICACION APLICACION { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ROL> ROL { get; set; }
    }
}
