using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using videoclub_project.Backend.Modelo;

namespace videoclub_project.Backend.Servicios {
    internal class ServicioAlquiler : ServicioGenerico<alquileres> {
        private static Logger log = LogManager.GetCurrentClassLogger();
        private DbContext context;

        public ServicioAlquiler(DbContext context) : base(context) {
            this.context = context;
        }

        public IEnumerable<alquileres> getAllFromClient(cliente client) {
            return context.Set<alquileres>().Where(a => a.id_cliente == client.idCliente).ToList();
        }
    }
}
