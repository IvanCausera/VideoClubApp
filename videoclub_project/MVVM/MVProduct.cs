using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using videoclub_project.Backend.Modelo;
using videoclub_project.Backend.Servicios;
using videoclub_project.Frontend.Dialogos;

namespace videoclub_project.MVVM {
    public class MVProduct : MVBaseCRUD<productos> {
        // Variables privadas ****************************************************************************************
        private videoclubEntities vidEnt;

        private productos prodSel;
        private actores_peliculas actorSel;
        private formatos_peliculas formatoSel;
        private plataformas_videojuegos plataformaSel;

        private DateTime fechaIniFilt;
        private DateTime fechaFinFilt;
        private generos generoSel;
        private string txtTitulo;
        private string txtActor;
        private string txtDirector;
        private string txtDistribuidora;
        private string txtDesarrolladora;
        private bool chkMultijugador;

        private sbyte byteMultijugador;

        private ServicioProducto servProd;

        private ListCollectionView listView;

        // Definicion criterios filtro *************************************
        private List<Predicate<productos>> criterios;
        private Predicate<productos> criterioGenero;
        private Predicate<productos> criterioTitulo;
        private Predicate<productos> criterioFecha;

        private Predicate<productos> criterioActor;
        private Predicate<productos> criterioDirector;

        private Predicate<productos> criterioDistribuidora;
        private Predicate<productos> criterioDesarrolladora;
        private Predicate<productos> criterioMultijugador;

        // Constructor ***********************************************************************************************
        public MVProduct(videoclubEntities vidEnt) {
            this.vidEnt = vidEnt;

            servProd = new ServicioProducto(vidEnt);
            servicio = servProd;

            prodSel = new productos();

            listView = new ListCollectionView(servProd.getAll().ToList());

            inicializa();
        }

        public MVProduct(videoclubEntities vidEnt, int productType) {
            this.vidEnt = vidEnt;

            servProd = new ServicioProducto(vidEnt);
            servicio = servProd;

            prodSel = new productos();

            if (productType == productos.JUEGO) {
                listView = new ListCollectionView(servProd.getAllVideojuegos().ToList());
            } else listView = new ListCollectionView(servProd.getAllPeliculas().ToList());

            inicializa();
        }

        private void inicializa() {
            fechaFiltro = servProd.getFirstDate();
            fechaFinFiltro = servProd.getLastDate();

            criterios = new List<Predicate<productos>>();
            criterioGenero = new Predicate<productos>(p => p.id_genero == generoSel.idGeneros);
            criterioTitulo = new Predicate<productos>(p => p.titulo.ToUpper().StartsWith(txtTitulo.ToUpper()));
            criterioFecha = new Predicate<productos>(p => p.fecha >= fechaFiltro && p.fecha <= fechaFinFiltro);

            criterioActor = new Predicate<productos>(p => eachActor(p.peliculas.actores_peliculas));
            criterioDirector = new Predicate<productos>(p => p.peliculas.director.ToUpper().StartsWith(txtFiltroDirector.ToUpper()));

            criterioDistribuidora = new Predicate<productos>(p => p.videojuegos.distribuidora.ToUpper().StartsWith(txtFiltroDistribuidora.ToUpper()));
            criterioDesarrolladora = new Predicate<productos>(p => p.videojuegos.desarrolladora.ToUpper().StartsWith(txtFiltroDesarrolladora.ToUpper()));
            criterioMultijugador = new Predicate<productos>(p => p.videojuegos.multijugador == byteMultijugador);
        }

        private bool eachActor(ICollection<actores_peliculas> act) {
            foreach (actores_peliculas actor in act) {
                if (actor.nombre.ToUpper().StartsWith(txtFiltroActor.ToUpper())) return true;
            }
            return false;
        }

        // List ******************************************************************************************************

        public ListCollectionView listProductos {
            get { return listView; }
        }

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

        public generos generoSelected {
            get { return generoSel; }
            set { generoSel = value; NotifyPropertyChanged(nameof(generoSelected)); }
        }

        public DateTime fechaFiltro {
            get { return fechaIniFilt; }
            set { fechaIniFilt = value; NotifyPropertyChanged(nameof(fechaFiltro)); }
        }

        public DateTime fechaFinFiltro {
            get { return fechaFinFilt; }
            set { fechaFinFilt = value; NotifyPropertyChanged(nameof(fechaFiltro)); }
        }

        public string txtFiltroTitulo {
            get { return txtTitulo; }
            set { txtTitulo = value; NotifyPropertyChanged(nameof(txtFiltroTitulo)); }
        }

        public string txtFiltroActor {
            get { return txtActor; }
            set { txtActor = value; NotifyPropertyChanged(nameof(txtFiltroActor)); }
        }

        public string txtFiltroDirector {
            get { return txtDirector; }
            set { txtDirector = value; NotifyPropertyChanged(nameof(txtFiltroDirector)); }
        }

        public string txtFiltroDistribuidora {
            get { return txtDistribuidora; }
            set { txtDistribuidora = value; NotifyPropertyChanged(nameof(txtFiltroDistribuidora)); }
        }

        public string txtFiltroDesarrolladora {
            get { return txtDesarrolladora; }
            set { txtDesarrolladora = value; NotifyPropertyChanged(nameof(txtFiltroDesarrolladora)); }
        }

        public bool chkFiltroMultijugador {
            get { return chkMultijugador; }
            set {
                chkMultijugador = value;
                if (chkMultijugador) byteMultijugador = 1;
                else byteMultijugador = 0;
                NotifyPropertyChanged(nameof(chkFiltroMultijugador));
            }
        }

        // Methods ***************************************************************************************************

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

            if (prodSelected.videojuegos != null) {
                videojuegos game = prodSelected.videojuegos;
                prodSelected.videojuegos = null;
                if (add(prodSelected)) {
                    game.idVideojuegos = prodSelected.idProductos;
                    ICollection<plataformas_videojuegos> gamePlataforms = game.plataformas_videojuegos;
                    game.plataformas_videojuegos = null;
                    if (new MVBaseCRUD<videojuegos>(new ServicioGenerico<videojuegos>(vidEnt)).add(game)) {

                        foreach (plataformas_videojuegos platGame in gamePlataforms) {
                            item gameItem = new item ();
                            if (!new MVBaseCRUD<item>(new ServicioGenerico<item>(vidEnt)).add(gameItem)) return false; // item not inserted
                            platGame.id_videojuego = game.idVideojuegos;
                            platGame.id = gameItem.idItem;
                            platGame.item = gameItem;
                            if (!new MVBaseCRUD<plataformas_videojuegos>(new ServicioGenerico<plataformas_videojuegos>(vidEnt)).add(platGame)) return false; // product, inserted, game inserted, platform not.
                            
                        }
                    } else {
                        delete(prodSelected);
                        return false; // product inserted, game not
                    }
                } else return false; // product not inserted
            } else {
                peliculas movie = prodSelected.peliculas;
                prodSelected.peliculas = null;
                if (add(prodSelected)) {

                    movie.idPeliculas = prodSelected.idProductos;
                    ICollection<formatos_peliculas> movieFormats = movie.formatos_peliculas;
                    movie.formatos_peliculas = null;

                    if (new MVBaseCRUD<peliculas>(new ServicioGenerico<peliculas>(vidEnt)).add(movie)) {
                        foreach (formatos_peliculas movieFormat in movieFormats) {
                            item movieItem = new item();
                            if (!new MVBaseCRUD<item>(new ServicioGenerico<item>(vidEnt)).add(movieItem)) return false; // item not inserted
                            movieFormat.id_pelicula = movie.idPeliculas;
                            movieFormat.id = movieItem.idItem;
                            movieFormat.item = movieItem;
                            if (!new MVBaseCRUD<formatos_peliculas>(new ServicioGenerico<formatos_peliculas>(vidEnt)).add(movieFormat)) return false; // product, inserted, movie inserted, format not.
                        }
                    } else {
                        delete(prodSelected);
                        return false; // product inserted, movie not
                    }

                } else return false; // product not inserted
            }

            return true;
        }

        public bool borrar() {
            if (!delete(prodSelected)) return false;

            listProductos.Remove(prodSelected);

            return true;
        }

        public bool editar() {
            return update(prodSelected);
        }

        public bool filtroCriterios(object obj) {
            if (obj == null) return false;
            bool correct = true;

            productos prod = (productos)obj;
            if (criterios.Count() != 0) {
                correct = criterios.TrueForAll(x => x(prod));
            }

            return correct;
        }

        public void addCriterios() {
            criterios.Clear();
            if (generoSelected != null) {
                criterios.Add(criterioGenero);
            }
            if (!string.IsNullOrEmpty(txtFiltroTitulo)) {
                criterios.Add(criterioTitulo);
            }
            if (fechaFinFiltro >= fechaFiltro) {
                criterios.Add(criterioFecha);
            }
            if (!string.IsNullOrEmpty(txtFiltroActor)) {
                criterios.Add(criterioActor);
            }
            if (!string.IsNullOrEmpty(txtFiltroDirector)) {
                criterios.Add(criterioDirector);
            }
            if (!string.IsNullOrEmpty(txtFiltroDistribuidora)) {
                criterios.Add(criterioDistribuidora);
            }
            if (!string.IsNullOrEmpty(txtFiltroDesarrolladora)) {
                criterios.Add(criterioDesarrolladora);
            }
            if (chkFiltroMultijugador) {
                criterios.Add(criterioMultijugador);
            }

        }

        public void borrarCriterios() {
            generoSelected = null;
            txtFiltroTitulo = null;

            txtFiltroActor = null;
            txtFiltroDirector = null;

            txtFiltroDistribuidora = null;
            txtFiltroDesarrolladora = null;
            chkFiltroMultijugador = false;

            fechaFiltro = servProd.getFirstDate();
            fechaFinFiltro = servProd.getLastDate();

            criterios.Clear();
        }
    }
}
