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
    
    public partial class alquileres
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public alquileres()
        {
            this.productos_alquiler = new HashSet<productos_alquiler>();
        }
    
        public int idAlquileres { get; set; }
        public int id_cliente { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public Nullable<System.DateTime> fecha_devolucion { get; set; }
        public int id_tipo_alquiler { get; set; }
        public Nullable<int> id_reserva { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<productos_alquiler> productos_alquiler { get; set; }
        public virtual cliente cliente { get; set; }
        public virtual reservas reservas { get; set; }
        public virtual tipo_alquiler tipo_alquiler { get; set; }

        public override string ToString() {
            return idAlquileres + " ["+fecha.ToString()+"]";
        }
    }
}
