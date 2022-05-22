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
    /// Lógica de interacción para dMVVMAddUser.xaml
    /// </summary>
    public partial class dMVVMAddUser : MetroWindow {
        private videoclubEntities vidEnt;
        private MVUser mUser;
        private bool editar;
        private bool empleado;

        public dMVVMAddUser(videoclubEntities vidEnt) {
            InitializeComponent();

            this.vidEnt = vidEnt;

            inicializa();

            this.editar = false;
            this.empleado = false;
        }

        public dMVVMAddUser(videoclubEntities vidEnt, usuarios user) {
            InitializeComponent();

            this.vidEnt = vidEnt;

            inicializa();

            comboRol.IsEnabled = false;

            this.editar = true;
            btnGuardar.Content = "Editar";
            mUser.usuSelected = user;
            passPassword.Password = user.password;
            string dir = user.direccion;
            txtDomicilio.Text = dir.Substring(0, dir.IndexOf("-"));
            txtPoblacion.Text = dir.Substring(dir.IndexOf("-") + 1, (dir.LastIndexOf("-") - (dir.IndexOf("-") + 1)));
            txtCP.Text = dir.Substring(dir.LastIndexOf("-") + 1);

            if (user.cliente != null) {
                mUser.cliSelected = user.cliente;
            } else mUser.emplSelected = user.empleado;

        }

        private void inicializa() {
            mUser = new MVUser(vidEnt);
            DataContext = mUser;

            dateNacimiento.DisplayDateEnd = DateTime.Today.AddDays(-1);
            txtSalario.Visibility = Visibility.Hidden;
            txtBanco.Visibility = Visibility.Hidden;
            txtNTarjeta.Visibility = Visibility.Hidden;
        }

        private void comboRol_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if(mUser.usuSelected.roles.idRoles < 3) {
                txtSalario.Visibility = Visibility.Visible;
                txtBanco.Visibility = Visibility.Visible;
                empleado = true;

                txtNTarjeta.Visibility = Visibility.Hidden;
            } else {
                txtSalario.Visibility = Visibility.Hidden;
                txtBanco.Visibility = Visibility.Hidden;
                empleado = false;

                txtNTarjeta.Visibility = Visibility.Visible;
            }
        }

        private async void btnGuardar_Click(object sender, RoutedEventArgs e) {
            if (!setPassword()) {
                await this.ShowMessageAsync("GESTIÓN MODELO ARTÍCULO",
                           "ERROR!!! La contraseña tiene que contener: Nº, letra, alfanumérico");
            } else {
                // -----------------
                setDireccion();
                bool result;
                if (empleado) {
                    mUser.cliSelected = null;
                } else mUser.emplSelected = null;

                if (editar) {
                    result = mUser.editar();
                } else {
                    result = mUser.guardar();
                }

                if (result) {
                    await this.ShowMessageAsync("GESTIÓN USUARIOS",
                                       "TODO CORRECTO!!! Objeto guardado correctamente");
                    DialogResult = true;
                } else {
                    await this.ShowMessageAsync("GESTIÓN USUARIOS",
                                       "ERROR!!! No se puede guardar el objeto");
                }
            }
        }

        private void setDireccion() {
            mUser.usuSelected.direccion = txtDomicilio.Text + "-" + txtPoblacion.Text + "-" + txtCP.Text;
        }
        private bool setPassword() {
            bool result = mUser.validarPassword(passPassword.Password);

            if (result) {
                mUser.usuSelected.password = passPassword.Password;
            } else {
                passPassword.Focus();
            }

            return result;
        }
    }
}
