using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using videoclub_project.Backend.Modelo;

namespace videoclub_project.Backend.Servicios {
    internal class ServicioTipoAlquiler : ServicioGenerico<tipo_alquiler> {
        private static Logger log = LogManager.GetCurrentClassLogger();
        private DbContext context;

        public ServicioTipoAlquiler(DbContext context) : base(context) {
            this.context = context;
        }
    }
}
