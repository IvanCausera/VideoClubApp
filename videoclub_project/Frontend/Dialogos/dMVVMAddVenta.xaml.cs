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

        public dMVVMAddVenta(videoclubEntities vidEnt, usuarios user, item item = null) {
            InitializeComponent();

            this.vidEnt = vidEnt;

            inicializa();

            if (user.cliente != null) {
                mVenta.ventaSelected.cliente = user.cliente;
                comboCliente.IsEnabled = false;
                dateReserva.Visibility = Visibility.Collapsed;
                dateVenta.IsEnabled = false;
                btnGuardar.Content = "Comprar";
            }

            if (item != null) {
                mVenta.ventaSelected.ventas_productos.Add(new ventas_productos {
                    ventas = mVenta.ventaSelected,
                    item = item
                });
                uVentasProductos.update();
                gridAddJuego.Visibility = Visibility.Collapsed;
                gridAddPelicula.Visibility = Visibility.Collapsed;
            }

            mVenta.ventaSelected.fecha = DateTime.Now;

            this.editar = false;
        }

        public dMVVMAddVenta(videoclubEntities vidEnt, ventas venta, bool ver = false) {
            InitializeComponent();

            this.vidEnt = vidEnt;

            inicializa();

            this.editar = true;
            btnGuardar.Content = "Editar";
            mVenta.ventaSelected = venta;

            if (ver) {
                gridVenta.IsEnabled = false;
                gridProductos.IsEnabled = false;

                gridAddJuego.Visibility = Visibility.Collapsed;
                gridAddPelicula.Visibility = Visibility.Collapsed;
                gridTitulo.Visibility = Visibility.Collapsed;

                btnCancelar.Visibility = Visibility.Collapsed;
                btnGuardar.Visibility = Visibility.Collapsed;
            }
        }

        private void inicializa() {
            mVenta = new MVVenta(vidEnt);
            DataContext = mVenta;

            uVentasProductos = new UCVentasProductos(mVenta);
            gridItems.Children.Add(uVentasProductos);

            this.AddHandler(Validation.ErrorEvent, new RoutedEventHandler(mVenta.OnErrorEvent));
            mVenta.btnGuardar = btnGuardar;
        }

        private void btnAddPelicula_Click(object sender, RoutedEventArgs e) {
            try {
                mVenta.ventaSelected.ventas_productos.Add(new ventas_productos {
                    ventas = mVenta.ventaSelected,
                    item = ((formatos_peliculas)comboPelicula.SelectedItem).item
                });
                uVentasProductos.update();
            } catch (Exception) {
                msgThrow("ERROR!! No es posible añadir la pelicula", false, false);
            }
        }

        private void btnAddVideojuego_Click(object sender, RoutedEventArgs e) {
            try {
                mVenta.ventaSelected.ventas_productos.Add(new ventas_productos {
                    ventas = mVenta.ventaSelected,
                    item = ((plataformas_videojuegos)comboVideojuego.SelectedItem).item
                });
                uVentasProductos.update();
            } catch (Exception) {
                msgThrow("ERROR!! No es posible añadir el videojuego", false, false);
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e) {
            foreach(ventas_productos venProd in mVenta.ventaSelected.ventas_productos) {
                if (venProd.item.plataformas_videojuegos != null) {
                    if (venProd.item.plataformas_videojuegos.stock <= 0) {
                        msgThrow("ERROR!! Se está intentando comprar un artículo sin stock", false, false);
                        return;
                    }
                    break;
                }

                if (venProd.item.formatos_peliculas != null) {
                    if (venProd.item.formatos_peliculas.stock <= 0) {
                        msgThrow("ERROR!! Se está intentando comprar un artículo sin stock", false, false);
                        return;
                    }
                    break;
                }

                msgThrow("ERROR!! Se está intentando comprar un artículo que no existe", false, false);
                return;
            }

            if (!mVenta.IsValid(this)) {
                msgThrow("ERROR!!! Hay campos obligatorios sin completar", false, false);
                return;
            }

            bool result;
            if (editar) {
                result = mVenta.editar();
            } else {
                result = mVenta.guardar();
            }

            if (result) {
                msgThrow("TODO CORRECTO!!! Objeto guardado correctamente", true, true);
            } else {
                msgThrow("ERROR!!! No se puede guardar el objeto", true, false);
            }
        }

        private async void msgThrow(string msg, bool close, bool result) {
            await this.ShowMessageAsync("GESTIÓN VENTAS",
                                   msg);
            if (close) {
                DialogResult = result;
            }
        }
    }
}
