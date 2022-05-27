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
using videoclub_project.Frontend.Dialogos;
using videoclub_project.MVVM;

namespace videoclub_project.Frontend.ControlesUsuario {
    /// <summary>
    /// Lógica de interacción para UCProduct.xaml
    /// </summary>
    public partial class UCProduct : UserControl {

        private videoclubEntities vidEnt;
        private MVProduct mProduct;

        public UCProduct(videoclubEntities vidEnt) {
            InitializeComponent();

            this.vidEnt = vidEnt;
            mProduct = new MVProduct(vidEnt);
            DataContext = mProduct;

            if (MVUser.loginUsuer.id_rol != roles.EMPLEADO && MVUser.loginUsuer.id_rol != roles.ADMINISTRADOR) {
                menuBorrar.Visibility = Visibility.Collapsed;
                menuEditar.Visibility = Visibility.Collapsed;
            }

        }

        public UCProduct(videoclubEntities vidEnt, int productType) {
            InitializeComponent();

            this.vidEnt = vidEnt;
            mProduct = new MVProduct(vidEnt, productType);
            DataContext = mProduct;

            if (MVUser.loginUsuer.id_rol != roles.EMPLEADO && MVUser.loginUsuer.id_rol != roles.ADMINISTRADOR) {
                menuBorrar.Visibility = Visibility.Collapsed;
                menuEditar.Visibility = Visibility.Collapsed;
            }

        }

        private void menuEditar_Click(object sender, RoutedEventArgs e) {
            dMVVMAddProduct diag = new dMVVMAddProduct(vidEnt, (productos)dgProduct.SelectedItem);
            diag.ShowDialog();
            if ((bool)diag.DialogResult) {
                dgProduct.Items.Refresh();
            }
        }

        private void menuBorrar_Click(object sender, RoutedEventArgs e) {
            if (mProduct.borrar()) {
                dgProduct.Items.Refresh();
            }
        }
    }
}
