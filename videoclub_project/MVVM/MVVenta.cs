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

        private cliente clienteSel;
        private item itemSel;

        private ServicioVenta servVenta;

        private ListCollectionView listView;

        // Definicion criterios filtro *************************************
        private List<Predicate<ventas>> criterios;
        private Predicate<ventas> criterioCliente;
        private Predicate<ventas> criterioItem;

        // Constructor ***********************************************************************************************
        public MVVenta(videoclubEntities vidEnt) {
            this.vidEnt = vidEnt;

            servVenta = new ServicioVenta(vidEnt);
            servicio = servVenta;

            ventaSel = new ventas();
            listView = new ListCollectionView(servVenta.getAll().ToList());

            inicializa();
        }

        public MVVenta(videoclubEntities vidEnt, cliente client) {
            this.vidEnt = vidEnt;

            servVenta = new ServicioVenta(vidEnt);
            servicio = servVenta;

            ventaSel = new ventas();
            listView = new ListCollectionView(servVenta.getAllFromClient(client).ToList());

            inicializa();
        }

        private void inicializa() {
            criterios = new List<Predicate<ventas>>();
            criterioCliente = new Predicate<ventas>(v => v.id_cliente == clienteSelected.idCliente);
            criterioItem = new Predicate<ventas>(v => eachItem(v.ventas_productos));
        }

        private bool eachItem(IEnumerable<ventas_productos> p) {
            foreach(ventas_productos item in p) {
                if (item.id_producto == itemSelected.idItem) return true;
            }
            return false;
        }

        // List ******************************************************************************************************

        public ListCollectionView listVentas {
            get { return listView; }
        }

        public List<cliente> listClientes {
            get { return new ServicioGenerico<cliente>(vidEnt).getAll().ToList(); }
        }

        public List<item> listItems {
            get { return new ServicioItem(vidEnt).getAllNotNull().ToList(); }
        }

        public List<formatos_peliculas> listPelicula {
            get { return new ServicioGenerico<formatos_peliculas>(vidEnt).getAll().ToList(); }
        }

        public List<plataformas_videojuegos> listVideojuego {
            get { return new ServicioGenerico<plataformas_videojuegos>(vidEnt).getAll().ToList(); }
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

        public cliente clienteSelected {
            get { return clienteSel; }
            set { clienteSel = value; NotifyPropertyChanged(nameof(clienteSel)); }
        }

        public item itemSelected {
            get { return itemSel; }
            set { itemSel = value; NotifyPropertyChanged(nameof(itemSel)); }
        }

        // Methods ***************************************************************************************************

        public bool delteVentaProduct() {
            ventaSelected.ventas_productos.Remove(ventProdSelected);
            if (!new MVBaseCRUD<ventas_productos>(new ServicioGenerico<ventas_productos>(vidEnt)).delete(ventProdSelected)) return false;

            return true;
        }

        public bool guardar() {
            foreach (ventas_productos ventProduct in ventaSelected.ventas_productos) {
                if (ventProduct.item.formatos_peliculas != null) {
                    ventProduct.item.formatos_peliculas.stock -= 1;
                } else {
                    ventProduct.item.plataformas_videojuegos.stock -= 1;
                }
            }

            return add(ventaSelected);
        }

        public bool borrar() {
            if (!delete(ventaSelected)) return false;

            listVentas.Remove(ventaSelected);

            return true;
        }

        public bool editar() {
            return update(ventaSelected);
        }

        public bool filtroCriterios(object obj) {
            if (obj == null) return false;
            bool correct = true;

            ventas vent = (ventas)obj;
            if (criterios.Count() != 0) {
                correct = criterios.TrueForAll(x => x(vent));
            }

            return correct;
        }

        public void addCriterios() {
            criterios.Clear();
            if (itemSelected != null) {
                criterios.Add(criterioItem);
            }
            if (clienteSelected != null) {
                criterios.Add(criterioCliente);
            }
        }

        public void borrarCriterios() {
            itemSelected = null;
            clienteSelected = null;
            criterios.Clear();
        }
    }
}
