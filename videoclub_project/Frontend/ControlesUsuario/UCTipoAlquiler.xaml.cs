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
    /// Interaction logic for UCTipoAlquiler.xaml
    /// </summary>
    public partial class UCTipoAlquiler : UserControl {

        private videoclubEntities vidEnt;
        private MVTipoAlquiler mTipo;

        public UCTipoAlquiler(videoclubEntities vidEnt) {
            InitializeComponent();

            this.vidEnt = vidEnt;
            mTipo = new MVTipoAlquiler(vidEnt);
            DataContext = mTipo;
        }

        private void menuEditar_Click(object sender, RoutedEventArgs e) {
            DMVVMTipoAlquiler diag = new DMVVMTipoAlquiler(vidEnt, (tipo_alquiler)dgTipoAlquiler.SelectedItem);
            diag.ShowDialog();
            if ((bool)diag.DialogResult) {
                dgTipoAlquiler.Items.Refresh();
            }
        }

        private void menuBorrar_Click(object sender, RoutedEventArgs e) {
            mTipo.tipoSelected = (tipo_alquiler)dgTipoAlquiler.SelectedItem;
            if (mTipo.borrar()) {
                dgTipoAlquiler.Items.Refresh();
            }
        }
    }
}
