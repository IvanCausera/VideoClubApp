using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using videoclub_project.Backend.Modelo;
using videoclub_project.Backend.Servicios;

namespace videoclub_project.MVVM {
    public class MVVenta : MVBaseCRUD<ventas> {
        // Variables privadas ****************************************************************************************
        private videoclubEntities vidEnt;

        private ventas ventaSel;
        private ventas_productos ventProdSel;

        private ServicioGenerico<ventas> servVenta;

        private ListCollectionView listView;

        // Constructor ***********************************************************************************************
        public MVVenta(videoclubEntities vidEnt) {
            this.vidEnt = vidEnt;

            servVenta = new ServicioGenerico<ventas>(vidEnt);
            servicio = servVenta;

            ventaSel = new ventas();
            listView = new ListCollectionView(servVenta.getAll().ToList());
        }

        // List ******************************************************************************************************

        public ListCollectionView listVentas {
            get { return listView; }
        }

        // Getters and Setters ***************************************************************************************

        public ventas ventaSelected {
            get { return ventaSel; }
            set { ventaSel = value; NotifyPropertyChanged(nameof(ventaSelected)); }
        }

        public ventas_productos ventProdSelected {
            get { return ventProdSel; }
            set { ventProdSel = value; NotifyPropertyChanged(nameof(ventProdSelected)); }
        }

        public List<cliente> listClientes {
            get { return new ServicioGenerico<cliente>(vidEnt).getAll().ToList(); }
        }

        public List<formatos_peliculas> listPelicula {
            get { return new ServicioGenerico<formatos_peliculas>(vidEnt).getAll().ToList(); }
        }

        public List<plataformas_videojuegos> listVideojuego {
            get { return new ServicioGenerico<plataformas_videojuegos>(vidEnt).getAll().ToList(); }
        }

        // Methods ***************************************************************************************************

        public bool guardar() {
            return add(ventaSelected);
        }

        public bool borrar() {
            return delete(ventaSelected);
        }

        public bool editar() {
            return update(ventaSelected);
        }
    }
}
