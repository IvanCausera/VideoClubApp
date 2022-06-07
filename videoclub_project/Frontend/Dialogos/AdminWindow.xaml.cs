using MahApps.Metro.Controls;
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

namespace videoclub_project.Frontend.Dialogos {
    /// <summary>
    /// Lógica de interacción para AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : MetroWindow {
        private videoclubEntities vidEnt;
        private usuarios usuLogin;

        public AdminWindow(videoclubEntities vidEnt, usuarios usuLogin) {
            InitializeComponent();
            this.vidEnt = vidEnt;
            this.usuLogin = usuLogin;
            tbUserLogin.Text = usuLogin.ToString();

            if (usuLogin.id_rol != roles.ADMINISTRADOR) {
                rtabAdministracion.Visibility = Visibility.Collapsed;
            }

        }

        private void Salir_Click(object sender, RoutedEventArgs e) {
            new Login().Show();
            this.Close();
        }

        private void rbtnAddUsuario_Click(object sender, RoutedEventArgs e) {
            dMVVMAddUser diag = new dMVVMAddUser(vidEnt);
            diag.ShowDialog();
        }

        private void rbtnAddProducto_Click(object sender, RoutedEventArgs e) {
            dMVVMAddProduct diag = new dMVVMAddProduct(vidEnt);
            diag.ShowDialog();
        }

        private void rbtnListaUsuario_Click(object sender, RoutedEventArgs e) {
            UCUsers uc = new UCUsers(vidEnt);
            mainGrid.Children.Clear();
            mainGrid.Children.Add(uc);
        }

        private void rbtnListaProductos_Click(object sender, RoutedEventArgs e) {
            UCProduct uc = new UCProduct(vidEnt);
            mainGrid.Children.Clear();
            mainGrid.Children.Add(uc);
        }

        private void rbtnAddAlquiler_Click(object sender, RoutedEventArgs e) {
            dMVVMAddAlquiler diag = new dMVVMAddAlquiler(vidEnt);
            diag.ShowDialog();
        }

        private void rbtnListaAlquileres_Click(object sender, RoutedEventArgs e) {
            UCAlquileres uc = new UCAlquileres(vidEnt);
            mainGrid.Children.Clear();
            mainGrid.Children.Add(uc);
        }

        private void rbtnAddVenta_Click(object sender, RoutedEventArgs e) {
            dMVVMAddVenta diag = new dMVVMAddVenta(vidEnt);
            diag.ShowDialog();
        }

        private void rbtnListaVentas_Click(object sender, RoutedEventArgs e) {
            UCVentas uc = new UCVentas(vidEnt);
            mainGrid.Children.Clear();
            mainGrid.Children.Add(uc);
        }

        private void rbtnInformeUsuario_Click(object sender, RoutedEventArgs e) {
            UCInformeUsuarios uc = new UCInformeUsuarios(vidEnt);
            mainGrid.Children.Clear();
            mainGrid.Children.Add(uc);
        }

        private void rbtnInformeVentas_Click(object sender, RoutedEventArgs e) {
            UCInformeVentas uc = new UCInformeVentas(vidEnt);
            mainGrid.Children.Clear();
            mainGrid.Children.Add(uc);
        }

        private void rbtnInformeAlquileres_Click(object sender, RoutedEventArgs e) {
            UCInformeAlquileres uc = new UCInformeAlquileres(vidEnt);
            mainGrid.Children.Clear();
            mainGrid.Children.Add(uc);
        }

        private void rbtnInformeProductos_Click(object sender, RoutedEventArgs e) {
            UCInformeProductos uc = new UCInformeProductos(vidEnt);
            mainGrid.Children.Clear();
            mainGrid.Children.Add(uc);
        }

        private void rbtnAddTipoAlquiler_Click(object sender, RoutedEventArgs e) {
            DMVVMTipoAlquiler diag = new DMVVMTipoAlquiler(vidEnt);
            diag.ShowDialog();
        }

        private void rbtnListaTipoAlquiler_Click(object sender, RoutedEventArgs e) {
            UCTipoAlquiler uc = new UCTipoAlquiler(vidEnt);
            mainGrid.Children.Clear();
            mainGrid.Children.Add(uc);
        }
    }
}
