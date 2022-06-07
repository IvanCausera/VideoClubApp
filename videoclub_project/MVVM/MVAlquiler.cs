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

        private cliente clienteSel;
        private item itemSel;
        private bool chkPorDevolver;

        private ServicioAlquiler servAql;

        private ListCollectionView listView;

        // Definicion criterios filtro *************************************
        private List<Predicate<alquileres>> criterios;
        private Predicate<alquileres> criterioCliente;
        private Predicate<alquileres> criterioItem;
        private Predicate<alquileres> criterioPorDevolver;

        // Constructor ***********************************************************************************************
        public MVAlquiler(videoclubEntities vidEnt) {
            this.vidEnt = vidEnt;

            servAql = new ServicioAlquiler(vidEnt);
            servicio = servAql;

            alqSel = new alquileres();
            listView = new ListCollectionView(servAql.getAll().ToList());

            inicializa();
        }

        public MVAlquiler(videoclubEntities vidEnt, cliente client, bool enCurso = false) {
            this.vidEnt = vidEnt;

            servAql = new ServicioAlquiler(vidEnt);
            servicio = servAql;

            alqSel = new alquileres();
            if (enCurso) {
                listView = new ListCollectionView(servAql.getAllFromClientInProcess(client).ToList());
            } else listView = new ListCollectionView(servAql.getAllFromClient(client).ToList());

            inicializa();
        }

        private void inicializa() {
            criterios = new List<Predicate<alquileres>>();
            criterioCliente = new Predicate<alquileres>(a => a.id_cliente == clienteSelected.idCliente);
            criterioItem = new Predicate<alquileres>(v => eachItem(v.productos_alquiler));
            criterioPorDevolver = new Predicate<alquileres>(v => v.fecha_devolucion == null);
        }

        private bool eachItem(IEnumerable<productos_alquiler> p) {
            foreach (productos_alquiler item in p) {
                if (item.id_producto == itemSelected.idItem) return true;
            }
            return false;
        }

        // List ******************************************************************************************************

        public ListCollectionView listAlquileres {
            get { return listView; }
        }

        public List<cliente> listClientes {
            get { return new ServicioGenerico<cliente>(vidEnt).getAll().ToList(); }
        }

        public List<item> listItems {
            get { return new ServicioItem(vidEnt).getAllNotNull().ToList(); }
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

        public cliente clienteSelected {
            get { return clienteSel; }
            set { clienteSel = value; NotifyPropertyChanged(nameof(clienteSel)); }
        }

        public item itemSelected {
            get { return itemSel; }
            set { itemSel = value; NotifyPropertyChanged(nameof(itemSel)); }
        }

        public bool chkFiltroPorDevolver {
            get { return chkPorDevolver; }
            set { chkPorDevolver = value; NotifyPropertyChanged(nameof(chkFiltroPorDevolver)); }
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

        public bool filtroCriterios(object obj) {
            if (obj == null) return false;
            bool correct = true;

            alquileres alq = (alquileres)obj;
            if (criterios.Count() != 0) {
                correct = criterios.TrueForAll(x => x(alq));
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
            if (chkFiltroPorDevolver) {
                criterios.Add(criterioPorDevolver);
            }
        }

        public void borrarCriterios() {
            itemSelected = null;
            clienteSelected = null;
            chkFiltroPorDevolver = false;

            criterios.Clear();
        }
    }
}
