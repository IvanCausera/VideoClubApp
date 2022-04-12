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
    /// Lógica de interacción para UCPlataformas.xaml
    /// </summary>
    public partial class UCPlataformas : UserControl {
        private MVProduct mProduct;

        public UCPlataformas(MVProduct mProduct) {
            InitializeComponent();

            this.mProduct = mProduct;
            DataContext = mProduct;
        }

        public void addPlataforma(plataformas_videojuegos plataforma) {
            mProduct.prodSelected.videojuegos.plataformas_videojuegos.Add(plataforma);
            dgPlataforma.Items.Refresh();
        }

        private void menuBorrar_Click(object sender, RoutedEventArgs e) {
            if (mProduct.deletePlataforma()) {
                dgPlataforma.Items.Refresh();
            }
        }
    }
}
