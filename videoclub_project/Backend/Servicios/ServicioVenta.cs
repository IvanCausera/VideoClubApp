using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using videoclub_project.Backend.Modelo;

namespace videoclub_project.Backend.Servicios {
    internal class ServicioVenta : ServicioGenerico<ventas> {
        private static Logger log = LogManager.GetCurrentClassLogger();
        private DbContext context;

        public ServicioVenta(DbContext context) : base(context) {
            this.context = context;
        }

        public IEnumerable<ventas> getAllFromClient(cliente client) {
            return context.Set<ventas>().Where(v => v.id_cliente == client.idCliente).ToList();
        }

    }
}
