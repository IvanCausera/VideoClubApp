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
    /// Lógica de interacción para UCVentas.xaml
    /// </summary>
    public partial class UCVentas : UserControl {

        private videoclubEntities vidEnt;
        private MVVenta mVenta;

        public UCVentas(videoclubEntities vidEnt) {
            InitializeComponent();

            this.vidEnt = vidEnt;
            mVenta = new MVVenta(vidEnt);
            DataContext = mVenta;
        }

        private void menuEditar_Click(object sender, RoutedEventArgs e) {
            dMVVMAddVenta diag = new dMVVMAddVenta(vidEnt, (ventas)dgVenta.SelectedItem);
            diag.ShowDialog();
            if ((bool)diag.DialogResult) {
                dgVenta.Items.Refresh();
            }
        }

        private void menuBorrar_Click(object sender, RoutedEventArgs e) {
            mVenta.ventaSelected = (ventas)dgVenta.SelectedItem;
            if (mVenta.borrar()) {
                dgVenta.Items.Refresh();
            }
        }
    }
}
