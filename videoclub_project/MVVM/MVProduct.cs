using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using videoclub_project.Backend.Modelo;
using videoclub_project.Backend.Servicios;

namespace videoclub_project.MVVM {
    public class MVProduct : MVBaseCRUD<productos> {
        // Variables privadas ***************************************************************************************
        private videoclubEntities vidEnt;

        private productos prodSel;
        private actores_peliculas actorSel;
        private formatos_peliculas formatoSel;
        private plataformas_videojuegos plataformaSel;

        private ServicioProducto servProd;

        // Constructor ***************************************************************************************
        public MVProduct(videoclubEntities vidEnt) {
            this.vidEnt = vidEnt;

            servProd = new ServicioProducto(vidEnt);
            servicio = servProd;

            prodSel = new productos();
        }

        // List ***************************************************************************************

        public List<formatos> listFormatos {
            get { return new ServicioGenerico<formatos>(vidEnt).getAll().ToList(); }
        }

        public List<plataformas> listPlataformas {
            get { return new ServicioGenerico<plataformas>(vidEnt).getAll().ToList(); }
        }

        public List<generos> listGeneros {
            get { return new ServicioGenerico<generos>(vidEnt).getAll().ToList(); }
        }

        public List<idiomas> listIdiomas {
            get { return new ServicioGenerico<idiomas>(vidEnt).getAll().ToList(); }
        }

        // Getters and Setters ***************************************************************************************

        public productos prodSelected {
            get { return prodSel; }
            set { prodSel = value; NotifyPropertyChanged(nameof(prodSelected)); }
        }

        public actores_peliculas actorSelected {
            get { return actorSel; }
            set { actorSel = value; NotifyPropertyChanged(nameof(actorSelected)); }
        }

        public formatos_peliculas formatoSelected {
            get { return formatoSel; }
            set { formatoSel = value; NotifyPropertyChanged(nameof(formatoSelected)); }
        }

        public plataformas_videojuegos plataformaSelected {
            get { return plataformaSel; }
            set { plataformaSel = value; NotifyPropertyChanged(nameof(plataformaSelected)); }
        }


        // Methods **************************************************************************************

        public bool deleteActor() {
            return prodSel.peliculas.actores_peliculas.Remove(actorSelected);
        }

        public bool deleteFormato() {
            return prodSel.peliculas.formatos_peliculas.Remove(formatoSelected);
        }

        public bool deletePlataforma() {
            return prodSel.videojuegos.plataformas_videojuegos.Remove(plataformaSelected);
        }

        public bool guardar() {
            return add(prodSelected);
        }
    }
}
