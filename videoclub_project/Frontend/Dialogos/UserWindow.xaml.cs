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
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : MetroWindow {
        private videoclubEntities vidEnt;
        private usuarios user;

        public UserWindow(videoclubEntities vidEnt, usuarios usuLogin) {
            InitializeComponent();
            this.vidEnt = vidEnt;

            this.user = usuLogin;

            if (usuLogin == null) {
                tbUserLogin.Text = "sin registrar";
                menuAlquiler.IsEnabled = false;
                menuAlquiler.IsVisible = false;

                menuPedidos.IsEnabled = false;
                menuPedidos.IsVisible = false;
            } else tbUserLogin.Text = "Usuario: " + usuLogin.ToString();
        }

        private void hamMenuPrincipal_ItemClick(object sender, ItemClickEventArgs args) {
            if (hamMenuPrincipal.SelectedItem == menuHome) {
                
            } else if (hamMenuPrincipal.SelectedItem == menuPelicula) {
                UCProduct uc = new UCProduct(vidEnt, productos.PELICULA);
                mainGrid.Children.Clear();
                mainGrid.Children.Add(uc);
            } else if (hamMenuPrincipal.SelectedItem == menuVideojuego) {
                UCProduct uc = new UCProduct(vidEnt, productos.JUEGO);
                mainGrid.Children.Clear();
                mainGrid.Children.Add(uc);
            } else if (hamMenuPrincipal.SelectedItem == menuAlquiler) {
                UCAlquileres uc = new UCAlquileres(vidEnt, user.cliente);
                mainGrid.Children.Clear();
                mainGrid.Children.Add(uc);
            } else if (hamMenuPrincipal.SelectedItem == menuPedidos) {
                UCVentas uc = new UCVentas(vidEnt, user.cliente);
                mainGrid.Children.Clear();
                mainGrid.Children.Add(uc);
            }
        }

        private void Salir_Click(object sender, RoutedEventArgs e) {
            new Login().Show();
            this.Close();
        }
    }
}
