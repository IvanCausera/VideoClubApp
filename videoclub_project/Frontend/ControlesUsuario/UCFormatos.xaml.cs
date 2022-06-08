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
using System.Windows.Navigation;
using System.Windows.Shapes;
using videoclub_project.Backend.Modelo;
using videoclub_project.MVVM;

namespace videoclub_project.Frontend.ControlesUsuario {
    /// <summary>
    /// Lógica de interacción para UCFormatos.xaml
    /// </summary>
    public partial class UCFormatos : UserControl {
        private MVProduct mProduct;

        public UCFormatos(MVProduct mProduct) {
            InitializeComponent();

            this.mProduct = mProduct;
            DataContext = mProduct;

            if (MVUser.loginUsuer.id_rol != roles.EMPLEADO && MVUser.loginUsuer.id_rol != roles.ADMINISTRADOR) {
                menuBorrar.Visibility = Visibility.Collapsed;
            }
        }

        public void addFormato(formatos_peliculas formato) {
            mProduct.prodSelected.peliculas.formatos_peliculas.Add(formato);
            dgFormato.Items.Refresh();
        }
        private void menuBorrar_Click(object sender, RoutedEventArgs e) {
            if (mProduct.deleteFormato()) {
                dgFormato.Items.Refresh();
            }
        }
    }
}
