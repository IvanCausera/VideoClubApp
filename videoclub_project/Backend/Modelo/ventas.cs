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
    
    public partial class ventas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ventas()
        {
            this.ventas_productos = new HashSet<ventas_productos>();
        }
    
        public int idVentas { get; set; }
        public int id_cliente { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public Nullable<int> id_reserva { get; set; }
    
        public virtual cliente cliente { get; set; }
        public virtual reservas reservas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ventas_productos> ventas_productos { get; set; }
    }
}