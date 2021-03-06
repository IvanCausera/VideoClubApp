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
        private Predicate<object> predVenta;

        public UCVentas(videoclubEntities vidEnt) {
            InitializeComponent();

            this.vidEnt = vidEnt;
            mVenta = new MVVenta(vidEnt);

            inicializa();
        }

        public UCVentas(videoclubEntities vidEnt, cliente client) {
            InitializeComponent();

            this.vidEnt = vidEnt;
            mVenta = new MVVenta(vidEnt, client);

            txtFiltroCliente.Visibility = Visibility.Collapsed;
            comboFiltroCliente.Visibility = Visibility.Collapsed;

            inicializa();
        }

        private void inicializa() {
            DataContext = mVenta;

            if (MVUser.loginUsuer.id_rol != roles.EMPLEADO && MVUser.loginUsuer.id_rol != roles.ADMINISTRADOR) {
                menuBorrar.Visibility = Visibility.Collapsed;
                menuEditar.Visibility = Visibility.Collapsed;
            }
            predVenta = new Predicate<object>(mVenta.filtroCriterios);
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

        private void menuVer_Click(object sender, RoutedEventArgs e) {
            dMVVMAddVenta diag = new dMVVMAddVenta(vidEnt, (ventas)dgVenta.SelectedItem, true);
            diag.ShowDialog();
        }

        private void btnClearFilter_Click(object sender, RoutedEventArgs e) {
            mVenta.borrarCriterios();

            comboFiltroArticulo.SelectedIndex = -1;
            comboFiltroCliente.SelectedIndex = -1;
            comboFiltroArticulo.Text = "Seleciona un Articulo";
            comboFiltroCliente.Text = "Seleciona un Cliente";

            mVenta.listVentas.Filter = predVenta;
        }

        private void comboFiltroCliente_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            filtrar();
        }

        private void comboFiltroArticulo_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            filtrar();
        }

        private void filtrar() {
            mVenta.addCriterios();
            mVenta.listVentas.Filter = predVenta;
        }
    }
}
