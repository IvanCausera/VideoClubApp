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

        private UCItems uItems;

        private bool editar;

        public dMVVMAddAlquiler(videoclubEntities vidEnt) {
            InitializeComponent();

            this.vidEnt = vidEnt;

            inicializa();

            this.editar = false;
        }

        public dMVVMAddAlquiler(videoclubEntities vidEnt, alquileres alq) {
            InitializeComponent();

            this.vidEnt = vidEnt;

            inicializa();

            this.editar = true;
            btnGuardar.Content = "Editar";
            mAlquiler.alqSelected = alq;
        }

        private void inicializa() {
            mAlquiler = new MVAlquiler(vidEnt);
            DataContext = mAlquiler;

            uItems = new UCItems(mAlquiler);
            gridItems.Children.Add(uItems);
        }

        private async void btnGuardar_Click(object sender, RoutedEventArgs e) {
            bool result;
            if (editar) {
                result = mAlquiler.editar();
            } else {
                result = mAlquiler.guardar();
            }

            if (result) {
                await this.ShowMessageAsync("GESTIÓN USUARIOS",
                                   "TODO CORRECTO!!! Objeto guardado correctamente");
                DialogResult = true;
            } else {
                await this.ShowMessageAsync("GESTIÓN USUARIOS",
                                   "ERROR!!! No se puede guardar el objeto");
                DialogResult = false;
            }
        }

        private void btnAddPelicula_Click(object sender, RoutedEventArgs e) {
            mAlquiler.alqSelected.productos_alquiler.Add(new productos_alquiler {
                alquileres = mAlquiler.alqSelected,
                item = ((formatos_peliculas)comboPelicula.SelectedItem).item
            });
            uItems.update();
        }

        private void btnAddVideojuego_Click(object sender, RoutedEventArgs e) {
            mAlquiler.alqSelected.productos_alquiler.Add(new productos_alquiler {
                alquileres = mAlquiler.alqSelected,
                item = ((plataformas_videojuegos)comboVideojuego.SelectedItem).item
            });
            uItems.update();
        }
    }
}
