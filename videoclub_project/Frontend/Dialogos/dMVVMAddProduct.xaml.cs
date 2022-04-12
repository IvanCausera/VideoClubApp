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

        public dMVVMAddProduct(videoclubEntities vidEnt) {
            InitializeComponent();

            this.vidEnt = vidEnt;

            inicializa();
        }

        private void inicializa() {
            mProduct = new MVProduct(vidEnt);
            DataContext = mProduct;

            mProduct.prodSelected.videojuegos = new videojuegos();
            mProduct.prodSelected.peliculas = new peliculas();

            showVideojuegos(false);
            showPelicula(true);
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

        private async void btnGuardar_Click(object sender, RoutedEventArgs e) {
            string portadaNombre = txtTitulo.Text + ".png";
            saveImage(portada, "../../Recursos/img/productos/" + portadaNombre);

            mProduct.prodSelected.portada = portadaNombre;

            if (actualLayout == PELICULA) mProduct.prodSelected.videojuegos = new videojuegos();
            else mProduct.prodSelected.peliculas = new peliculas();

            bool result;
            result = mProduct.guardar();

            if (result) {
                await this.ShowMessageAsync("GESTIÓN USUARIOS",
                                   "TODO CORRECTO!!! Objeto guardado correctamente");
                DialogResult = true;
            } else {
                await this.ShowMessageAsync("GESTIÓN USUARIOS",
                                   "ERROR!!! No se puede guardar el objeto");
            }
        }

        private void btnTipoProducto_Click(object sender, RoutedEventArgs e) {
            if (actualLayout == PELICULA) {
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

        private void btnAddActor_Click(object sender, RoutedEventArgs e) {
            uActors.addActor(new actores_peliculas { nombre = txtActor.Text });
        }

        private void btnAddFormato_Click(object sender, RoutedEventArgs e) {
            try {
                if (actualLayout == PELICULA) {
                    uFormatos.addFormato(new formatos_peliculas {
                        formatos = (formatos)comboFormato.SelectedItem,
                        stock = int.Parse(txtStock.Text),
                        precio = float.Parse(txtPrecio.Text)
                    });
                } else {
                    uPlataformas.addPlataforma(new plataformas_videojuegos {
                        plataformas = (plataformas)comboPlataforma.SelectedItem,
                        stock = int.Parse(txtStock.Text),
                        precio = float.Parse(txtPrecio.Text)
                    });
                }
            } catch (Exception) {
                //TODO excepcion
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

                uPlataformas = new UCPlataformas(mProduct);
                gridLeft.Children.Clear();
                gridLeft.Children.Add(uPlataformas);

                txtDistribuidora.Visibility = Visibility.Visible;
                txtDesarrolladora.Visibility = Visibility.Visible;
                comboPlataforma.Visibility = Visibility.Visible;
            } else {
                txtDistribuidora.Visibility = Visibility.Hidden;
                txtDesarrolladora.Visibility = Visibility.Hidden;
                comboPlataforma.Visibility = Visibility.Hidden;
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
                //TODO excepcion
            }
        }
    }
}
