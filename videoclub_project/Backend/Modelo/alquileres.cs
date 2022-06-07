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

    public partial class alquileres : PropertyChangedDataError {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public alquileres()
        {
            this.productos_alquiler = new HashSet<productos_alquiler>();
        }
    
        public int idAlquileres { get; set; }
        public int id_cliente { get; set; }

        [Required(ErrorMessage = "Fecha obligatorio")]
        public Nullable<System.DateTime> fecha { get; set; }
        public Nullable<System.DateTime> fecha_devolucion { get; set; }
        public int id_tipo_alquiler { get; set; }
        public Nullable<System.DateTime> reserva { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<productos_alquiler> productos_alquiler { get; set; }

        [Required(ErrorMessage = "Cliente obligatorio")]
        public virtual cliente cliente { get; set; }

        [Required(ErrorMessage = "Tipo Alquiler obligatorio")]
        public virtual tipo_alquiler tipo_alquiler { get; set; }

        public override string ToString() {
            string str = "";
            foreach (productos_alquiler product in productos_alquiler) {
                str += product.ToString() + "\n";
            }
            return str;
        }

        public string ToStringID() {
            return idAlquileres + " [" + fecha.ToString() + "]";
        }
    }
}
