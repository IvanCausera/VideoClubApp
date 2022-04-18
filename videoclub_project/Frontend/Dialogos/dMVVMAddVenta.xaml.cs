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
    /// Lógica de interacción para dMVVMAddVenta.xaml
    /// </summary>
    public partial class dMVVMAddVenta : MetroWindow {

        private videoclubEntities vidEnt;
        private MVVenta mVenta;

        private UCVentasProductos uVentasProductos;

        private bool editar;

        public dMVVMAddVenta(videoclubEntities vidEnt) {
            InitializeComponent();

            this.vidEnt = vidEnt;

            inicializa();

            this.editar = false;

        }

        private void inicializa() {
            mVenta = new MVVenta(vidEnt);
            DataContext = mVenta;

            uVentasProductos = new UCVentasProductos(mVenta);
            gridItems.Children.Add(uVentasProductos);
        }

        private void btnAddPelicula_Click(object sender, RoutedEventArgs e) {
            mVenta.ventaSelected.ventas_productos.Add(new ventas_productos {
                ventas = mVenta.ventaSelected,
                item = ((formatos_peliculas)comboPelicula.SelectedItem).item
            });
            uVentasProductos.update();
        }

        private void btnAddVideojuego_Click(object sender, RoutedEventArgs e) {
            mVenta.ventaSelected.ventas_productos.Add(new ventas_productos {
                ventas = mVenta.ventaSelected,
                item = ((plataformas_videojuegos)comboVideojuego.SelectedItem).item
            });
            uVentasProductos.update();
        }

        private async void btnGuardar_Click(object sender, RoutedEventArgs e) {
            bool result;
            if (editar) {
                result = mVenta.editar();
            } else {
                result = mVenta.guardar();
            }

            if (result) {
                await this.ShowMessageAsync("GESTIÓN VENTAS",
                                   "TODO CORRECTO!!! Objeto guardado correctamente");
                DialogResult = true;
            } else {
                await this.ShowMessageAsync("GESTIÓN VENTAS",
                                   "ERROR!!! No se puede guardar el objeto");
                DialogResult = false;
            }
        }
    }
}
