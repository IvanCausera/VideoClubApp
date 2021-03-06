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

    public partial class peliculas : PropertyChangedDataError {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public peliculas()
        {
            this.actores_peliculas = new HashSet<actores_peliculas>();
            this.formatos_peliculas = new HashSet<formatos_peliculas>();
        }
    
        public int idPeliculas { get; set; }
        public string titulo_original { get; set; }

        public string pais { get; set; }

        [Required(ErrorMessage = "Campo Duracion obligatorio")]
        public int duracion { get; set; }
        public string sinopsis { get; set; }

        public string director { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<actores_peliculas> actores_peliculas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<formatos_peliculas> formatos_peliculas { get; set; }
        public virtual productos productos { get; set; }

        public override string ToString() {
            return titulo_original + " (" + productos.titulo + ")";
        }
    }
}
