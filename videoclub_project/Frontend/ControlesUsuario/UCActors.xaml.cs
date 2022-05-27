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
    /// Lógica de interacción para UCActors.xaml
    /// </summary>
    public partial class UCActors : UserControl {
        private MVProduct mProduct;

        public UCActors(MVProduct mProduct) {
            InitializeComponent();

            dgActor.SetBinding(DataGrid.ItemsSourceProperty, new Binding("prodSelected.peliculas.actores_peliculas"));

            this.mProduct = mProduct;
            DataContext = mProduct;

            if (MVUser.loginUsuer.id_rol != roles.EMPLEADO && MVUser.loginUsuer.id_rol != roles.ADMINISTRADOR) {
                menuBorrar.Visibility = Visibility.Collapsed;
            }
        }

        public void addActor(actores_peliculas actor) {
            mProduct.prodSelected.peliculas.actores_peliculas.Add(actor);
            dgActor.Items.Refresh();
        }

        private void menuBorrar_Click(object sender, RoutedEventArgs e) {
            if (mProduct.deleteActor()) {
                dgActor.Items.Refresh();
            }
        }
    }
}
