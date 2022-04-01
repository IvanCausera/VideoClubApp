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
    }
}
