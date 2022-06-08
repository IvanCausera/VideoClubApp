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

    public partial class tipo_alquiler : PropertyChangedDataError {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tipo_alquiler()
        {
            this.alquileres = new HashSet<alquileres>();
        }
    
        public int idTipo_alquiler { get; set; }

        [Required(ErrorMessage = "Precio obligatorio")]
        public float precio { get; set; }

        [Required(ErrorMessage = "Duracion obligatorio")]
        public int duracion { get; set; }

        [Required(ErrorMessage = "Recargo obligatorio")]
        public float recargo { get; set; }

        [Required(ErrorMessage = "Nombre obligatorio")]
        public string nombre { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<alquileres> alquileres { get; set; }

        public override string ToString() {
            return nombre + " [dias: " + duracion + " precio: " + precio + "€]";
        }
    }
}
