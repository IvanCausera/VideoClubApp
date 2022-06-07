using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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
    /// Interaction logic for dMVVMTipoAlquiler.xaml
    /// </summary>
    public partial class DMVVMTipoAlquiler : MetroWindow {

        private videoclubEntities vidEnt;
        private MVTipoAlquiler mTipo;

        private bool editar;

        public DMVVMTipoAlquiler(videoclubEntities vidEnt) {
            InitializeComponent();

            this.vidEnt = vidEnt;

            inicializa();

            editar = false;
        }

        public DMVVMTipoAlquiler(videoclubEntities vidEnt, tipo_alquiler tipo) {
            InitializeComponent();

            this.vidEnt = vidEnt;

            inicializa();

            editar = true;
            btnGuardar.Content = "Editar";
            mTipo.tipoSelected = tipo;
        }

        private void inicializa() {
            mTipo = new MVTipoAlquiler(vidEnt);
            DataContext = mTipo;

            this.AddHandler(Validation.ErrorEvent, new RoutedEventHandler(mTipo.OnErrorEvent));
            mTipo.btnGuardar = btnGuardar;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e) {
            if (!mTipo.IsValid(this)) {
                msgThrow("ERROR!!! Hay campos obligatorios sin completar", false, false);
                return;
            }

            bool result;
            if (editar) {
                result = mTipo.editar();
            } else {
                result = mTipo.guardar();
            }

            if (result) {
                msgThrow("TODO CORRECTO!!! Objeto guardado correctamente", true, true);
            } else {
                msgThrow("ERROR!!! No se puede guardar el objeto", true, false);
            }

        }

        private async void msgThrow(string msg, bool close, bool result) {
            await this.ShowMessageAsync("GESTIÓN TIPO ALQUILERES",
                                   msg);
            if (close) {
                DialogResult = result;
            }
        }
    }
}
