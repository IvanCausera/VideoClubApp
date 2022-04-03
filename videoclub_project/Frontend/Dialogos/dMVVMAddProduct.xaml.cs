using MahApps.Metro.Controls;
using Microsoft.Win32;
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
using videoclub_project.MVVM;

namespace videoclub_project.Frontend.Dialogos {
    /// <summary>
    /// Lógica de interacción para dMVVMAddProduct.xaml
    /// </summary>
    public partial class dMVVMAddProduct : MetroWindow {
        private videoclubEntities vidEnt;
        private MVProduct mProduct;

        public dMVVMAddProduct(videoclubEntities vidEnt) {
            InitializeComponent();

            this.vidEnt = vidEnt;

            inicializa();
        }

        private void inicializa() {
            mProduct = new MVProduct(vidEnt);
            DataContext = mProduct;


        }

        private void btnPortada_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true) {
                imgPortada.Source = new BitmapImage(new Uri(op.FileName));
            }
        }
    }
}
