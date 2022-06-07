using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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
using System.Windows.Shapes;
using videoclub_project.Backend.Modelo;
using videoclub_project.Frontend.ControlesUsuario;
using videoclub_project.MVVM;

namespace videoclub_project.Frontend.Dialogos {
    /// <summary>
    /// Lógica de interacción para dMVVMAddAlquiler.xaml
    /// </summary>
    public partial class dMVVMAddAlquiler : MetroWindow {
        private videoclubEntities vidEnt;
        private MVAlquiler mAlquiler;

        private UCProductosAlquiler uProductosAlquiler;

        private bool editar;

        public dMVVMAddAlquiler(videoclubEntities vidEnt) {
            InitializeComponent();

            this.vidEnt = vidEnt;

            inicializa();

            this.editar = false;
        }

        public dMVVMAddAlquiler(videoclubEntities vidEnt, usuarios user, item item = null) {
            InitializeComponent();

            this.vidEnt = vidEnt;

            inicializa();

            if (user.cliente != null) {
                mAlquiler.alqSelected.cliente = user.cliente;
                comboCliente.IsEnabled = false;
                dateReserva.Visibility = Visibility.Collapsed;
                dateDevolucion.Visibility = Visibility.Collapsed;
                dateAlquiler.IsEnabled = false;
                btnGuardar.Content = "Alquilar";
            }

            if (item != null) {
                mAlquiler.alqSelected.productos_alquiler.Add(new productos_alquiler {
                    alquileres = mAlquiler.alqSelected,
                    item = item
                });
                uProductosAlquiler.update();
                gridAddJuego.Visibility = Visibility.Collapsed;
                gridAddPelicula.Visibility = Visibility.Collapsed;
            }

            mAlquiler.alqSelected.fecha = DateTime.Now;

            this.editar = false;
        }

        public dMVVMAddAlquiler(videoclubEntities vidEnt, alquileres alq, bool ver = false) {
            InitializeComponent();

            this.vidEnt = vidEnt;

            inicializa();

            this.editar = true;
            btnGuardar.Content = "Editar";
            mAlquiler.alqSelected = alq;

            if (ver) {
                gridAlquiler.IsEnabled = false;
                gridProductos.IsEnabled = false;

                gridTitulo.Visibility = Visibility.Collapsed;
                gridAddPelicula.Visibility = Visibility.Collapsed;
                gridAddJuego.Visibility = Visibility.Collapsed;
                btnCancelar.Visibility = Visibility.Collapsed;
                btnGuardar.Visibility = Visibility.Collapsed;
            }
        }

        private void inicializa() {
            mAlquiler = new MVAlquiler(vidEnt);
            DataContext = mAlquiler;

            uProductosAlquiler = new UCProductosAlquiler(mAlquiler);
            gridItems.Children.Add(uProductosAlquiler);

            this.AddHandler(Validation.ErrorEvent, new RoutedEventHandler(mAlquiler.OnErrorEvent));
            mAlquiler.btnGuardar = btnGuardar;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e) {
            if (!mAlquiler.IsValid(this)) {
                msgThrow("ERROR!!! Hay campos obligatorios sin completar", false, false);
                return;
            }

            bool result;
            if (editar) {
                result = mAlquiler.editar();
            } else {
                result = mAlquiler.guardar();
            }

            if (result) {
                msgThrow("TODO CORRECTO!!! Objeto guardado correctamente", true, true);
            } else {
                msgThrow("ERROR!!! No se puede guardar el objeto", true, false);
            }
        }

        private void btnAddPelicula_Click(object sender, RoutedEventArgs e) {
            try {
                mAlquiler.alqSelected.productos_alquiler.Add(new productos_alquiler {
                    alquileres = mAlquiler.alqSelected,
                    item = ((formatos_peliculas)comboPelicula.SelectedItem).item
                });
                uProductosAlquiler.update();
            } catch (Exception) {
                msgThrow("ERROR!! No se puede cargar la pelicula", false, false);
            }
        }

        private void btnAddVideojuego_Click(object sender, RoutedEventArgs e) {
            try {
                mAlquiler.alqSelected.productos_alquiler.Add(new productos_alquiler {
                    alquileres = mAlquiler.alqSelected,
                    item = ((plataformas_videojuegos)comboVideojuego.SelectedItem).item
                });
                uProductosAlquiler.update();
            } catch (Exception) {
                msgThrow("ERROR!! No se puede cargar el videojuego", false, false);
            }
        }

        private async void msgThrow(string msg, bool close, bool result) {
            await this.ShowMessageAsync("GESTIÓN ALQUILER",
                                   msg);
            if (close) {
                DialogResult = result;
            }
        }
    }
}
