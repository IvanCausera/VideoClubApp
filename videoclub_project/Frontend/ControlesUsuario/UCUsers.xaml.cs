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
    /// Lógica de interacción para UCUsers.xaml
    /// </summary>
    public partial class UCUsers : UserControl {

        private videoclubEntities vidEnt;
        private MVUser mUser;

        private Predicate<object> predUsuario;

        public UCUsers(videoclubEntities vidEnt) {
            InitializeComponent();

            this.vidEnt = vidEnt;
            mUser = new MVUser(vidEnt);
            DataContext = mUser;

            if (MVUser.loginUsuer.id_rol != roles.ADMINISTRADOR) {
                menuBorrar.Visibility = Visibility.Collapsed;
                menuEditar.Visibility = Visibility.Collapsed;
            }

            predUsuario = new Predicate<object>(mUser.filtroCriterios);
        }

        private void menuBorrar_Click(object sender, RoutedEventArgs e) {
            if (mUser.borrar()) {
                dgUser.Items.Refresh();
            }
        }

        private void menuEditar_Click(object sender, RoutedEventArgs e) {
            dMVVMAddUser diag = new dMVVMAddUser(vidEnt, (usuarios)dgUser.SelectedItem);
            diag.ShowDialog();
            if ((bool)diag.DialogResult) {
                dgUser.Items.Refresh();
            }
        }

        private void btnClearFilter_Click(object sender, RoutedEventArgs e) {
            mUser.borrarCriterios();

            comboFiltroRol.SelectedIndex = -1;
            comboFiltroRol.Text = "Seleciona un Rol";

            mUser.listUsuarios.Filter = predUsuario;
        }

        private void txtFiltroNombre_TextChanged(object sender, TextChangedEventArgs e) {
            filtrar();
        }

        private void txtFiltroUsuario_TextChanged(object sender, TextChangedEventArgs e) {
            filtrar();
        }

        private void comboFiltroRol_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            filtrar();
        }

        private void filtrar() {
            mUser.addCriterios();
            mUser.listUsuarios.Filter = predUsuario;
        }
    }
}
