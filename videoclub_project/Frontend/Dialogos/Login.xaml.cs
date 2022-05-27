using videoclub_project.Backend.Modelo;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using NLog;
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
using videoclub_project.Backend.Servicios;
using videoclub_project;
using videoclub_project.MVVM;

namespace videoclub_project.Frontend.Dialogos {
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : MetroWindow{
        private static Logger log = LogManager.GetCurrentClassLogger();

        private bool empleado;

        private videoclubEntities vidEnt;
        private ServicioUsuario usuServ;

        public Login() {
            InitializeComponent();
            if (!conectar()) {
                MessageBox.Show("ERROR!!! NO SE PUEDE CONECTAR CON LA BASE DE DATOS",
                "CONEXION BASE DE DATOS", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
            usuServ = new ServicioUsuario(vidEnt);
            empleado = false;
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e) {
            if (String.IsNullOrEmpty(txtUserName.Text) || String.IsNullOrEmpty(passPassword.Password)) {
                MessageBox.Show("Es necesario establecer el usuario y la contraseña",
                "FALTAN CAMPOS", MessageBoxButton.OK, MessageBoxImage.Error);
            } else if (usuServ.login(txtUserName.Text, passPassword.Password)) {
                if (empleado) {
                    if (usuServ.usuLogin.empleado != null) {
                        AdminWindow adminWindow = new AdminWindow(vidEnt, usuServ.usuLogin);
                        MVUser.loginUsuer = usuServ.usuLogin;
                        adminWindow.Show();
                        this.Close();
                    } else {
                        await this.ShowMessageAsync("USUARIO INCORRECTOS",
                                   "El usuario no es un empleado o administrador");
                    }
                } else {
                    if (usuServ.usuLogin.cliente != null) {
                        UserWindow userWindow = new UserWindow(vidEnt, usuServ.usuLogin);
                        MVUser.loginUsuer = usuServ.usuLogin;
                        userWindow.Show();
                        this.Close();
                    } else {
                        await this.ShowMessageAsync("USUARIO INCORRECTOS",
                                   "El usuario no es un cliente");
                    }
                }
            } else {
                await this.ShowMessageAsync("CAMPOS INCORRECTOS",
                                   "Usuario o contraseña incorrectos");
            }
        }

        private Boolean conectar() {
            Boolean conectar = true;
            vidEnt = new videoclubEntities();
            try {
                vidEnt.Database.Connection.Open();
            } catch (Exception ex) { 
                conectar = false;
                log.Info("Conexion con base de datos fallida");
                log.Error(ex.InnerException);
                log.Error(ex.StackTrace);
            }
            return conectar;
        }

        private void btnLoginWithoutUser_Click(object sender, RoutedEventArgs e) {
            UserWindow userWindow = new UserWindow(vidEnt, null);
            MVUser.loginUsuer = new usuarios { id_rol = roles.CLIENTE };
            userWindow.Show();
            this.Close();
        }

        private void btnChangeUser_Click(object sender, RoutedEventArgs e) {
            if (empleado) {
                txtLogin.Text = "L O G I N";
                btnChangeUser.Content = "Portal Empleado";
                btnLoginWithoutUser.Visibility = Visibility.Visible;
                empleado = false;
            } else {
                txtLogin.Text = "PORTAL EMPLEADO";
                btnChangeUser.Content = "Portal Cliente";
                btnLoginWithoutUser.Visibility = Visibility.Hidden;
                empleado = true;
            }
        }
    }
}