using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using videoclub_project.Backend.Modelo;
using videoclub_project.Backend.Servicios;

namespace videoclub_project.MVVM {
    public class MVProduct : MVBaseCRUD<productos> {
        // Variables privadas ****************************************************************************************
        private videoclubEntities vidEnt;

        private productos prodSel;
        private actores_peliculas actorSel;
        private formatos_peliculas formatoSel;
        private plataformas_videojuegos plataformaSel;

        private ServicioProducto servProd;

        private ListCollectionView listView;

        // Constructor ***********************************************************************************************
        public MVProduct(videoclubEntities vidEnt) {
            this.vidEnt = vidEnt;

            servProd = new ServicioProducto(vidEnt);
            servicio = servProd;

            prodSel = new productos();

            listView = new ListCollectionView(servProd.getAll().ToList());
        }

        public MVProduct(videoclubEntities vidEnt, int productType) {
            this.vidEnt = vidEnt;

            servProd = new ServicioProducto(vidEnt);
            servicio = servProd;

            prodSel = new productos();

            if (productType == productos.JUEGO) {
                listView = new ListCollectionView(servProd.getAllVideojuegos().ToList());
            } else listView = new ListCollectionView(servProd.getAllPeliculas().ToList());
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
    }
}
