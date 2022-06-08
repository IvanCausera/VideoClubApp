//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace videoclub_project.Backend.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using videoclub_project.MVVM;

    public partial class usuarios : PropertyChangedDataError {
        public int idUsuarios { get; set; }

        [Required(ErrorMessage = "Nombre obligatorio")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "Apellido obligatorio")]
        public string apellido1 { get; set; }
        public string apellido2 { get; set; }
        public string direccion { get; set; }
        public string mail { get; set; }
        public string telefono { get; set; }
        public Nullable<System.DateTime> fecha_nacimiento { get; set; }

        [Required(ErrorMessage = "Username obligatorio")]
        public string user { get; set; }

        [Required(ErrorMessage = "Password obligatorio")]
        public string password { get; set; }
        public int id_rol { get; set; }
        public string DNI { get; set; }
    
        public virtual cliente cliente { get; set; }
        public virtual empleado empleado { get; set; }

        [Required(ErrorMessage = "Rol obligatorio")]
        public virtual roles roles { get; set; }

        public override string ToString() {
            return nombre + " " + apellido1 + " " + apellido2;
        }
    }
}
