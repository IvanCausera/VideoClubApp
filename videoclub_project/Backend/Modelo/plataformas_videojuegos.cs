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
    
    public partial class plataformas_videojuegos
    {
        public int id { get; set; }
        public int id_plataforma { get; set; }
        public int id_videojuego { get; set; }
        public int stock { get; set; }
        public float precio { get; set; }
    
        public virtual item item { get; set; }
        public virtual plataformas plataformas { get; set; }
        public virtual videojuegos videojuegos { get; set; }

        public override string ToString() {
            return videojuegos.productos.titulo + " | " + plataformas.plataforma + " [" + stock + ", " + precio + "€]";
        }

        public string ToStringTitulo() {
            return videojuegos.productos.titulo;
        }
    }
}
