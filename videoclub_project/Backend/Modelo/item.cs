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
    
    public partial class item
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public item()
        {
            this.productos_alquiler = new HashSet<productos_alquiler>();
            this.ventas_productos = new HashSet<ventas_productos>();
        }
    
        public int idItem { get; set; }
    
        public virtual formatos_peliculas formatos_peliculas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<productos_alquiler> productos_alquiler { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ventas_productos> ventas_productos { get; set; }
        public virtual plataformas_videojuegos plataformas_videojuegos { get; set; }

        public override string ToString() {
            string str;

            if (formatos_peliculas == null && plataformas_videojuegos == null) {
                return "Articulo no disponible";
            }

            if (formatos_peliculas != null) {
                str = formatos_peliculas.ToString();
            } else {
                str = plataformas_videojuegos.ToString();
            }

            return str;
        }

        public string ToStringTitulo() {
            string str;

            if (formatos_peliculas == null && plataformas_videojuegos == null) {
                return "Articulo no disponible";
            }

            if (formatos_peliculas != null) {
                str = formatos_peliculas.ToStringTitulo();
            } else {
                str = plataformas_videojuegos.ToStringTitulo();
            }

            return str;
        }
    }
}
