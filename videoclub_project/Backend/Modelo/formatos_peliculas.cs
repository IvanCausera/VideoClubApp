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
    
    public partial class formatos_peliculas
    {
        public int id { get; set; }
        public int id_formato { get; set; }
        public int id_pelicula { get; set; }
        public int stock { get; set; }
        public float precio { get; set; }
    
        public virtual formatos formatos { get; set; }
        public virtual peliculas peliculas { get; set; }
        public virtual item item { get; set; }
        public override string ToString() {
            return peliculas.productos.titulo + " | " + formatos.formato + " [" + stock + ", " + precio + "€]";
        }
    }
}
