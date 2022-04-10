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

        private ServicioProducto servProd;

        // Constructor ***************************************************************************************
        public MVProduct(videoclubEntities vidEnt) {
            this.vidEnt = vidEnt;

            servProd = new ServicioProducto(vidEnt);
            prodSel = new productos();
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


        // Methods **************************************************************************************

        public bool deleteActor() {
            return prodSel.peliculas.actores_peliculas.Remove(actorSelected);
        }
    }
}
