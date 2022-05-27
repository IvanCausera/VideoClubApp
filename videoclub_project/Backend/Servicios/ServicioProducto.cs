using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using videoclub_project.Backend.Modelo;

namespace videoclub_project.Backend.Servicios {
    public class ServicioProducto : ServicioGenerico<productos> {
        private static Logger log = LogManager.GetCurrentClassLogger();
        private DbContext context;

        public ServicioProducto(DbContext context) : base(context) {
            this.context = context;
        }

        public IEnumerable<productos> getAllVideojuegos() {
            return context.Set<productos>().Where(u => u.videojuegos != null && u.peliculas == null).ToList();
        }

        public IEnumerable<productos> getAllPeliculas() {
            return context.Set<productos>().Where(u => u.videojuegos == null && u.peliculas != null).ToList();
        }

        public List<actores_peliculas> getActores(int idPelicula) {
            return context.Set<actores_peliculas>().Where(u => u.id_pelicula == idPelicula).ToList();
        }

        public List<formatos_peliculas> getFormatos(int idPelicula) {
            return context.Set<formatos_peliculas>().Where(x => x.id_pelicula == idPelicula).ToList();
        }
    }
}
