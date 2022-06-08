using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Windows.Shapes;
using videoclub_project.Backend.Modelo;
using videoclub_project.MVVM;
using System.IO;
using System.Drawing.Imaging;
using videoclub_project.Frontend.ControlesUsuario;
using MahApps.Metro.Controls.Dialogs;

namespace videoclub_project.Frontend.Dialogos {
    /// <summary>
    /// Lógica de interacción para dMVVMAddProduct.xaml
    /// </summary>
    public partial class dMVVMAddProduct : MetroWindow {

        private const int PELICULA = 0;
        private const int VIDEOJUEGO = 1;

        private int actualLayout;
        private videoclubEntities vidEnt;
        private MVProduct mProduct;
        private BitmapImage portada;

        private UCActors uActors;
        private UCFormatos uFormatos;
        private UCPlataformas uPlataformas;

        private bool editar;

        public dMVVMAddProduct(videoclubEntities vidEnt) {
            InitializeComponent();

            this.vidEnt = vidEnt;

            inicializa();

            this.editar = false;
        }

        public dMVVMAddProduct(videoclubEntities vidEnt, productos product, bool ver = false) {
            InitializeComponent();

            this.vidEnt = vidEnt;

            inicializa();

            this.editar = true;
            btnGuardar.Content = "Editar";
            btnPortada.IsEnabled = false;
            mProduct.prodSelected = product;

            if (!string.IsNullOrEmpty(product.portada)) {
                /*
                portada = new BitmapImage(new Uri("/Recursos/img/productos/" + product.portada, UriKind.Relative));
                imgPortada.Source = portada;
                */
                string dir = System.AppDomain.CurrentDomain.BaseDirectory;
                portada = new BitmapImage(new Uri(dir + "../../Recursos/img/productos/" + product.portada, UriKind.RelativeOrAbsolute));
                imgPortada.Source = portada;
            }

            if (mProduct.prodSelected.videojuegos == null) {
                changeLayout(PELICULA);
            } else {
                changeLayout(VIDEOJUEGO);
                if (mProduct.prodSelected.videojuegos.multijugador == 1) chkMultijugador.IsChecked = true;
            }

            if (ver) {
                gridGeneral.IsEnabled = false;
                gridInfoEspecifica.IsEnabled = false;

                gridAddActor.Visibility = Visibility.Collapsed;
                gridAddFormato.Visibility = Visibility.Collapsed;
                gridTitulo.Visibility = Visibility.Collapsed;
                btnCancelar.Visibility = Visibility.Collapsed;
                btnGuardar.Visibility = Visibility.Collapsed;
                btnTipoProducto.Visibility = Visibility.Collapsed;
            } else {
                btnTipoProducto.IsEnabled = false;
            }
        }

        private void inicializa() {
            mProduct = new MVProduct(vidEnt);
            DataContext = mProduct;

            mProduct.prodSelected.videojuegos = new videojuegos { productos = mProduct.prodSelected };
            mProduct.prodSelected.peliculas = new peliculas { productos = mProduct.prodSelected };

            showVideojuegos(false);
            showPelicula(true);

            this.AddHandler(Validation.ErrorEvent, new RoutedEventHandler(mProduct.OnErrorEvent));
            mProduct.btnGuardar = btnGuardar;
        }

        private void btnPortada_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true) {
                portada = new BitmapImage(new Uri(op.FileName));
                imgPortada.Source = portada;
            }
                
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e) {
            if (!mProduct.IsValid(this)) {
                msgThrow("ERROR!!! Hay campos obligatorios sin completar", false, false);
                return;
            }

            if (!editar) {
                if (portada != null) {
                    string portadaNombre = txtTitulo.Text + ".png";
                    saveImage(portada, "../../Recursos/img/productos/" + portadaNombre);
                    mProduct.prodSelected.portada = portadaNombre;
                }
            }

            if (actualLayout == PELICULA) {
                mProduct.prodSelected.videojuegos = null;
            } else {
                mProduct.prodSelected.peliculas = null;
            }

            bool result;
            if (editar) {
                result = mProduct.editar();
            } else {
                result = mProduct.guardar();
            }

            if (result) {
                msgThrow("TODO CORRECTO!!! Objeto guardado correctamente", true, true);
            } else {
                msgThrow("ERROR!!! No se puede guardar el objeto", true, false);
            }
        }

        private void btnTipoProducto_Click(object sender, RoutedEventArgs e) {
            if (actualLayout == VIDEOJUEGO) {
                changeLayout(PELICULA);
            } else {
                changeLayout(VIDEOJUEGO);
            }
            
        }

        private void btnAddActor_Click(object sender, RoutedEventArgs e) {
            uActors.addActor(new actores_peliculas { 
                nombre = txtActor.Text, 
                peliculas = mProduct.prodSelected.peliculas 
            });
        }

        private void btnAddFormato_Click(object sender, RoutedEventArgs e) {
            try {
                if (actualLayout == PELICULA) {
                    uFormatos.addFormato(new formatos_peliculas {
                        formatos = (formatos)comboFormato.SelectedItem,
                        stock = int.Parse(txtStock.Text),
                        precio = float.Parse(txtPrecio.Text),
                        peliculas = mProduct.prodSelected.peliculas
                    });
                } else {
                    uPlataformas.addPlataforma(new plataformas_videojuegos {
                        plataformas = (plataformas)comboPlataforma.SelectedItem,
                        stock = int.Parse(txtStock.Text),
                        precio = float.Parse(txtPrecio.Text),
                        videojuegos = mProduct.prodSelected.videojuegos
                    });
                }
            } catch (Exception) {
                if (actualLayout == PELICULA) msgThrow("ERROR!! No es posible añadir la pelicula", false, false);
                else msgThrow("ERROR!! No es posible añadir el videojuego", false, false);
            }
        }

        /// <summary>
        /// Show the layout of movies or hides it.
        /// </summary>
        /// <param name="show">If is going to show or hide</param>
        private void showPelicula(bool show) {
            if (show) {
                actualLayout = PELICULA;

                uActors = new UCActors(mProduct);
                gridRight.Children.Clear();
                gridRight.Children.Add(uActors);

                uFormatos = new UCFormatos(mProduct);
                gridLeft.Children.Clear();
                gridLeft.Children.Add(uFormatos);

                txtSinopsis.Visibility = Visibility.Visible;
                txtDirector.Visibility = Visibility.Visible;
                txtPais.Visibility = Visibility.Visible;
                comboFormato.Visibility = Visibility.Visible;
                txtActor.Visibility = Visibility.Visible;
                txtTituloOriginal.Visibility = Visibility.Visible;
                txtDuracion.Visibility = Visibility.Visible;
                btnAddActor.Visibility = Visibility.Visible;
            } else {
                txtSinopsis.Visibility = Visibility.Hidden;
                txtDirector.Visibility = Visibility.Hidden;
                txtPais.Visibility = Visibility.Hidden;
                comboFormato.Visibility = Visibility.Hidden;
                txtActor.Visibility = Visibility.Hidden;
                txtTituloOriginal.Visibility = Visibility.Hidden;
                txtDuracion.Visibility = Visibility.Hidden;
                btnAddActor.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Shows the layout of games or hides it.
        /// </summary>
        /// <param name="show">If is going to show or hide</param>
        private void showVideojuegos(bool show) {
            if (show) {
                actualLayout = VIDEOJUEGO;

                gridRight.Children.Clear();

                gridLeft.SetValue(Grid.ColumnSpanProperty, 4);
                uPlataformas = new UCPlataformas(mProduct);
                gridLeft.Children.Clear();
                gridLeft.Children.Add(uPlataformas);

                txtDistribuidora.Visibility = Visibility.Visible;
                txtDesarrolladora.Visibility = Visibility.Visible;
                comboPlataforma.Visibility = Visibility.Visible;
                chkMultijugador.Visibility = Visibility.Visible;
                txtMultijugador.Visibility = Visibility.Visible;
            } else {
                gridLeft.SetValue(Grid.ColumnSpanProperty, 2);
                txtDistribuidora.Visibility = Visibility.Hidden;
                txtDesarrolladora.Visibility = Visibility.Hidden;
                comboPlataforma.Visibility = Visibility.Hidden;
                chkMultijugador.Visibility = Visibility.Hidden;
                txtMultijugador.Visibility = Visibility.Hidden;
            }
        }
        
        /// <summary>
        /// Saves a bitmapImage on a specific path.
        /// </summary>
        private void saveImage(BitmapImage img, string path) {
            try {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(img));

                using (var fileStream = new FileStream(path, FileMode.Create)) {
                    encoder.Save(fileStream);
                }
            } catch (ArgumentNullException) {
                
            }
        }

        private void changeLayout(int layout) {
            if (layout == VIDEOJUEGO) {
                showPelicula(false);
                showVideojuegos(true);
                btnTipoProducto.Content = "Videojuegos";
                lableInfo.Text = "INFORMACIÓN VIDEOJUEGO";
            } else {
                showVideojuegos(false);
                showPelicula(true);
                btnTipoProducto.Content = "Películas";
                lableInfo.Text = "INFORMACIÓN PELÍCULA";
            }
        }

        private async void msgThrow(string msg, bool close, bool result) {
            await this.ShowMessageAsync("GESTIÓN PRODUCTOS",
                                   msg);
            if (close) {
                DialogResult = result;
            }
        }

        private void chkMultijugador_Checked(object sender, RoutedEventArgs e) {
            mProduct.prodSelected.videojuegos.multijugador = 1;
        }

        private void chkMultijugador_Unchecked(object sender, RoutedEventArgs e) {
            mProduct.prodSelected.videojuegos.multijugador = 0;
        }
    }
}
