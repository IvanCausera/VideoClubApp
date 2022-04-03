﻿using MahApps.Metro.Controls;
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

        public dMVVMAddUser(videoclubEntities vidEnt) {
            InitializeComponent();

            this.vidEnt = vidEnt;

            inicializa();
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
            if(mUser.rolSelected.idRoles < 3) {
                txtSalario.Visibility = Visibility.Visible;
                txtBanco.Visibility = Visibility.Visible;

                txtNTarjeta.Visibility = Visibility.Hidden;
            } else {
                txtSalario.Visibility = Visibility.Hidden;
                txtBanco.Visibility = Visibility.Hidden;

                txtNTarjeta.Visibility = Visibility.Visible;
            }
        }
    }
}