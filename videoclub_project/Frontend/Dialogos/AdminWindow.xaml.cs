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
    }
}
