﻿using System;
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
using videoclub_project.MVVM;

namespace videoclub_project.Frontend.ControlesUsuario {
    /// <summary>
    /// Lógica de interacción para UCVentasProductos.xaml
    /// </summary>
    public partial class UCVentasProductos : UserControl {

        private MVVenta mVenta;

        public UCVentasProductos(MVVenta mVenta) {
            InitializeComponent();

            dgItems.SetBinding(DataGrid.ItemsSourceProperty, new Binding("{Binding ventaSelected.ventas_productos}"));

            this.mVenta = mVenta;
            DataContext = mVenta;
        }

        public void update() {
            dgItems.Items.Refresh();
        }

        private void menuBorrar_Click(object sender, RoutedEventArgs e) {
            //TODO
        }
    }
}