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
    /// Lógica de interacción para UCAlquileres.xaml
    /// </summary>
    public partial class UCAlquileres : UserControl {

        private videoclubEntities vidEnt;
        private MVAlquiler mAlquiler;

        private Predicate<object> predAlquiler;

        public UCAlquileres(videoclubEntities vidEnt) {
            InitializeComponent();

            this.vidEnt = vidEnt;
            mAlquiler = new MVAlquiler(vidEnt);
            inicializa();
        }

        public UCAlquileres(videoclubEntities vidEnt, cliente client) {
            InitializeComponent();

            this.vidEnt = vidEnt;
            mAlquiler = new MVAlquiler(vidEnt, client);

            txtFiltroCliente.Visibility = Visibility.Collapsed;
            comboFiltroCliente.Visibility = Visibility.Collapsed;

            inicializa();

        }

        private void inicializa() {
            DataContext = mAlquiler;
            
            if (MVUser.loginUsuer.id_rol != roles.EMPLEADO && MVUser.loginUsuer.id_rol != roles.ADMINISTRADOR) {
                menuBorrar.Visibility = Visibility.Collapsed;
                menuEditar.Visibility = Visibility.Collapsed;
            }

            predAlquiler = new Predicate<object>(mAlquiler.filtroCriterios);
        }


        private void menuEditar_Click(object sender, RoutedEventArgs e) {
            dMVVMAddAlquiler diag = new dMVVMAddAlquiler(vidEnt, (alquileres)dgAlquiler.SelectedItem);
            diag.ShowDialog();
            if ((bool)diag.DialogResult) {
                dgAlquiler.Items.Refresh();
            }
        }

        private void menuBorrar_Click(object sender, RoutedEventArgs e) {
            mAlquiler.alqSelected = (alquileres)dgAlquiler.SelectedItem;
            if (mAlquiler.borrar()) {
                dgAlquiler.Items.Refresh();
            }
        }

        private void menuVer_Click(object sender, RoutedEventArgs e) {
            dMVVMAddAlquiler diag = new dMVVMAddAlquiler(vidEnt, (alquileres)dgAlquiler.SelectedItem, true);
            diag.ShowDialog();
        }

        private void comboFiltroCliente_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            filtrar();
        }

        private void comboFiltroArticulo_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            filtrar();
        }

        private void btnClearFilter_Click(object sender, RoutedEventArgs e) {
            mAlquiler.borrarCriterios();

            comboFiltroArticulo.SelectedIndex = -1;
            comboFiltroCliente.SelectedIndex = -1;
            comboFiltroArticulo.Text = "Seleciona un Articulo";
            comboFiltroCliente.Text = "Seleciona un Cliente";

            mAlquiler.listAlquileres.Filter = predAlquiler;
        }

        private void filtrar() {
            mAlquiler.addCriterios();
            mAlquiler.listAlquileres.Filter = predAlquiler;
        }
    }
}
