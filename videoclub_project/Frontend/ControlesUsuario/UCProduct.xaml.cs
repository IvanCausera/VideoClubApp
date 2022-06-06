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
        private int productType;
        private Predicate<object> predProduct;

        public UCProduct(videoclubEntities vidEnt) {
            InitializeComponent();

            this.vidEnt = vidEnt;
            mProduct = new MVProduct(vidEnt);
            DataContext = mProduct;

            inicializa();
        }

        public UCProduct(videoclubEntities vidEnt, int productType) {
            InitializeComponent();

            this.vidEnt = vidEnt;
            this.productType = productType;
            mProduct = new MVProduct(vidEnt, productType);
            DataContext = mProduct;

            inicializa();
        }

        private void inicializa() {
            if (MVUser.loginUsuer.id_rol != roles.EMPLEADO && MVUser.loginUsuer.id_rol != roles.ADMINISTRADOR) {
                menuBorrar.Visibility = Visibility.Collapsed;
                menuEditar.Visibility = Visibility.Collapsed;
                if (MVUser.loginUsuer.cliente == null) {
                    menuAlquilar.Visibility = Visibility.Collapsed;
                    menuComprar.Visibility = Visibility.Collapsed;
                }
            }

            predProduct = new Predicate<object>(mProduct.filtroCriterios);
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

        private void menuVer_Click(object sender, RoutedEventArgs e) {
            dMVVMAddProduct diag = new dMVVMAddProduct(vidEnt, (productos)dgProduct.SelectedItem, true);
            diag.ShowDialog();
        }

        private void btnCierraSeleccion_Click(object sender, RoutedEventArgs e) {
            dgProduct.SelectedIndex = -1;
        }

        public class StringNullOrEmptyToVisibilityConverter : System.Windows.Markup.MarkupExtension, IValueConverter {
            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
                return string.IsNullOrEmpty(value as string)
                    ? Visibility.Collapsed : Visibility.Visible;
            }
            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
                return null;
            }
            public override object ProvideValue(IServiceProvider serviceProvider) {
                return this;
            }
        }

        private void menuAlquilar_Click(object sender, RoutedEventArgs e) {
            dMVVMAddAlquiler diag;

            if (productType == productos.PELICULA) {
                diag = new dMVVMAddAlquiler(vidEnt, MVUser.loginUsuer, mProduct.formatoSelected.item);
            } else {
                diag = new dMVVMAddAlquiler(vidEnt, MVUser.loginUsuer, mProduct.plataformaSelected.item);
            }
            
            diag.ShowDialog();

            if ((bool)diag.DialogResult) {

                dgArticulos.Items.Refresh();
            }
        }

        private void menuComprar_Click(object sender, RoutedEventArgs e) {
            dMVVMAddVenta diag;
            
            if (productType == productos.PELICULA) {
                diag = new dMVVMAddVenta(vidEnt, MVUser.loginUsuer, mProduct.formatoSelected.item);
            } else {
                diag = new dMVVMAddVenta(vidEnt, MVUser.loginUsuer, mProduct.plataformaSelected.item);
            }

            diag.ShowDialog();

            if ((bool)diag.DialogResult) {
                if (productType == productos.JUEGO) {
                }
                dgArticulos.Items.Refresh();
            }
        }

        private void dgProduct_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            mProduct.prodSelected = (productos)dgProduct.SelectedItem;

            if (mProduct.prodSelected != null) {
                if (mProduct.prodSelected.peliculas != null) {
                    productType = productos.PELICULA;
                    dgArticulos.SetBinding(DataGrid.ItemsSourceProperty, new Binding("prodSelected.peliculas.formatos_peliculas"));
                    dgArticulos.SetBinding(DataGrid.SelectedItemProperty, new Binding("formatoSelected"));
                    txtHeaderArticulo.Header = "Formato";
                    txtHeaderArticulo.Binding = new Binding("formatos");
                } else {
                    productType = productos.JUEGO;
                    dgArticulos.SetBinding(DataGrid.ItemsSourceProperty, new Binding("prodSelected.videojuegos.plataformas_videojuegos"));
                    dgArticulos.SetBinding(DataGrid.SelectedItemProperty, new Binding("plataformaSelected"));
                    txtHeaderArticulo.Header = "Plataforma";
                    txtHeaderArticulo.Binding = new Binding("plataformas");
                }
            }
        }

        private void dateFiltrar_SelectedDateChanged(object sender, SelectionChangedEventArgs e) {
            filtrar();
        }

        private void txtFiltroTitulo_TextChanged(object sender, TextChangedEventArgs e) {
            filtrar();
        }

        private void comboFiltroGenero_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            filtrar();
        }

        private void filtrar() {
            mProduct.addCriterios();
            mProduct.listProductos.Filter = predProduct;
        }

        private void btnClearFilter_Click(object sender, RoutedEventArgs e) {
            mProduct.borrarCriterios();

            comboFiltroGenero.SelectedIndex = -1;
            comboFiltroGenero.Text = "Seleciona un Genero";

            mProduct.listProductos.Filter = predProduct;
        }
    }
}
