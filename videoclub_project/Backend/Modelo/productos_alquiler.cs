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
    
    public partial class productos_alquiler
    {
        public int id { get; set; }
        public int id_alquiler { get; set; }
        public int id_producto { get; set; }
    
        public virtual alquileres alquileres { get; set; }
        public virtual item item { get; set; }
    }
}
