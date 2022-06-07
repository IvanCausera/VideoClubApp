using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for UCClientHome.xaml
    /// </summary>
    public partial class UCClientHome : UserControl {

        private videoclubEntities vidEnt;
        private MVAlquiler mAlquiler;

        public UCClientHome(videoclubEntities vidEnt, usuarios user) {
            InitializeComponent();

            this.vidEnt = vidEnt;
            mAlquiler = new MVAlquiler(vidEnt, user.cliente, true);
            DataContext = mAlquiler;
            txtClientName.Text = "Bienvenido: " + user.nombre + " " + user.apellido1;
            if (mAlquiler.listAlquileres.Count == 0) {
                dockAlquileres.Visibility = Visibility.Collapsed;
            }
        }

        public UCClientHome() {
            InitializeComponent();
            txtClientName.Text = "Bienvenido";
            dockAlquileres.Visibility = Visibility.Collapsed;
        }

        private void menuDevolver_Click(object sender, RoutedEventArgs e) {
            mAlquiler.alqSelected = (alquileres)dgAlquileres.SelectedItem;
            mAlquiler.alqSelected.fecha_devolucion = DateTime.Now;
            if (mAlquiler.editar()) {
                mAlquiler.listAlquileres.Remove(mAlquiler.alqSelected);
                if (mAlquiler.listAlquileres.Count == 0) {
                    dockAlquileres.Visibility = Visibility.Collapsed;
                } else
                    dgAlquileres.Items.Refresh();
            }
        }

    }

    public class TimeLeftConverter : IMultiValueConverter {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            DateTime fecha = ((DateTime)values[0]).AddDays((int)values[1]);

            int days = (fecha - DateTime.Now.Date).Days;

            if (days >= 0) {
                return days + " Días";
            } else
                return "Plazo atrasado";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    public class RecargoConverter : IMultiValueConverter {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            DateTime fecha = ((DateTime)values[0]).AddDays((int)values[1]);

            int days = (DateTime.Now.Date - fecha).Days;
            double recargo = 0;
            if (days > 0) {
                recargo = days * ((float)values[2]);
            }

            return recargo.ToString() + " €";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
