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
    
    public partial class permisos_roles
    {
        public int idPermisos_roles { get; set; }
        public int id_permiso { get; set; }
        public int id_rol { get; set; }
    
        public virtual permisos permisos { get; set; }
        public virtual roles roles { get; set; }
    }
}