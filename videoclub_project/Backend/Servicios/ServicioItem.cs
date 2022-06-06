using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using videoclub_project.Backend.Modelo;

namespace videoclub_project.Backend.Servicios {
    internal class ServicioItem : ServicioGenerico<item> {
        private static Logger log = LogManager.GetCurrentClassLogger();
        private DbContext context;

        public ServicioItem(DbContext context) : base(context) {
            this.context = context;
        }

        public IEnumerable<item> getAllNotNull() {
            return context.Set<item>().Where(i => i.plataformas_videojuegos != null || i.formatos_peliculas != null).ToList();
        }
    }
}
