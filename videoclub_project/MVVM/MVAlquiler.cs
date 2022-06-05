using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using videoclub_project.Backend.Modelo;
using videoclub_project.Backend.Servicios;

namespace videoclub_project.MVVM {
    public class MVAlquiler : MVBaseCRUD<alquileres> {
        // Variables privadas ****************************************************************************************
        private videoclubEntities vidEnt;

        private alquileres alqSel;
        private productos_alquiler prodAlqSel;

        private ServicioAlquiler servAql;

        private ListCollectionView listView;

        // Constructor ***********************************************************************************************
        public MVAlquiler(videoclubEntities vidEnt) {
            this.vidEnt = vidEnt;

            servAql = new ServicioAlquiler(vidEnt);
            servicio = servAql;

            alqSel = new alquileres();
            listView = new ListCollectionView(servAql.getAll().ToList());
        }

        public MVAlquiler(videoclubEntities vidEnt, cliente client) {
            this.vidEnt = vidEnt;

            servAql = new ServicioAlquiler(vidEnt);
            servicio = servAql;

            alqSel = new alquileres();
            listView = new ListCollectionView(servAql.getAllFromClient(client).ToList());
        }

        // List ******************************************************************************************************

        public ListCollectionView listAlquileres {
            get { return listView; }
        }

        public List<cliente> listClientes {
            get { return new ServicioGenerico<cliente>(vidEnt).getAll().ToList(); }
        }

        public List<tipo_alquiler> listTipoAlquiler {
            get { return new ServicioGenerico<tipo_alquiler>(vidEnt).getAll().ToList(); }
        }

        public List<formatos_peliculas> listPelicula {
            get { return new ServicioGenerico<formatos_peliculas>(vidEnt).getAll().ToList(); }
        }

        public List<plataformas_videojuegos> listVideojuego {
            get { return new ServicioGenerico<plataformas_videojuegos>(vidEnt).getAll().ToList(); }
        }

        public List<productos_alquiler> listProductosAlquiler {
            get { return new ServicioGenerico<productos_alquiler>(vidEnt).getAll().ToList(); }
        }

        // Getters and Setters ***************************************************************************************

        public alquileres alqSelected {
            get { return alqSel; }
            set { alqSel = value; NotifyPropertyChanged(nameof(alqSelected)); }
        }

        public productos_alquiler prodAlqSelected {
            get { return prodAlqSel; }
            set { prodAlqSel = value; NotifyPropertyChanged(nameof(prodAlqSelected)); }
        }


        // Methods ***************************************************************************************************

        public bool deleteProductoAlquiler() {
            return alqSelected.productos_alquiler.Remove(prodAlqSelected);
        }

        public bool guardar() {
            foreach (productos_alquiler alqProduct in alqSelected.productos_alquiler) {
                if (alqProduct.item.formatos_peliculas != null) {
                    alqProduct.item.formatos_peliculas.stock -= 1;
                } else {
                    alqProduct.item.plataformas_videojuegos.stock -= 1;
                }
            }

            return add(alqSelected);
        }

        public bool borrar() {
            if(!delete(alqSelected)) return false;

            listAlquileres.Remove(alqSelected);

            return true;
        }

        public bool editar() {
            return update(alqSelected);
        }
    }
}
