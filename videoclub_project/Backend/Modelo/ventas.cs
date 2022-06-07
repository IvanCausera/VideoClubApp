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

    public partial class ventas : PropertyChangedDataError {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ventas()
        {
            this.ventas_productos = new HashSet<ventas_productos>();
        }
    
        public int idVentas { get; set; }
        public int id_cliente { get; set; }

        [Required(ErrorMessage = "Fecha obligatorio")]
        public Nullable<System.DateTime> fecha { get; set; }
        public Nullable<System.DateTime> reserva { get; set; }

        [Required(ErrorMessage = "Cliente obligatorio")]
        public virtual cliente cliente { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ventas_productos> ventas_productos { get; set; }

        public override string ToString() {
            string str = "";
            foreach (ventas_productos product in ventas_productos) {
                str += product.ToString() + "\n";
            }
            return str;
        }
    }
}
