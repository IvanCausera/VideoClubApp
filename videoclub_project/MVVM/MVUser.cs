using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using videoclub_project.Backend.Modelo;
using videoclub_project.Backend.Servicios;

namespace videoclub_project.MVVM {
    public class MVUser: MVBaseCRUD<usuarios> {

        public static usuarios loginUsuer;

        // Variables privadas ***************************************************************************************
        private videoclubEntities vidEnt;

        private roles rolSel;
        private cliente cliSel;
        private empleado emplSel;
        private usuarios usuSel;

        private ServicioUsuario servUsu;

        private ListCollectionView listView;

        // Constructor ***************************************************************************************
        public MVUser(videoclubEntities vidEnt) {
            this.vidEnt = vidEnt;

            servUsu = new ServicioUsuario(vidEnt);
            servicio = servUsu;
            usuSel = new usuarios();
            cliSel = new cliente();
            emplSelected = new empleado();

            listView = new ListCollectionView(servUsu.getAll().ToList());

        }
        // Getters and Setters ***************************************************************************************
        public List<roles> listRoles {
            get { return new ServicioGenerico<roles>(vidEnt).getAll().ToList();}
        }

        public ListCollectionView listUsuarios {
            get { return listView; }
        }

        public roles rolSelected {
            get { return rolSel; }
            set { rolSel = value; NotifyPropertyChanged(nameof(rolSelected)); }
        }

        public usuarios usuSelected {
            get { return usuSel; }
            set { usuSel = value; NotifyPropertyChanged(nameof(usuSelected)); }
        }

        public cliente cliSelected {
            get { return cliSel; }
            set { cliSel = value; NotifyPropertyChanged(nameof(cliSelected)); }
        }

        public empleado emplSelected {
            get { return emplSel; }
            set { emplSel = value; NotifyPropertyChanged(nameof(emplSelected)); }
        }

        // Metodos ***************************************************************************************

        public bool guardar() {
            if (!add(usuSelected)) return false;

            if (emplSelected == null) {
                cliSelected.idCliente = usuSelected.idUsuarios;
                cliSelected.usuarios = usuSelected;
                if (!(new MVBaseCRUD<cliente>(new ServicioGenerico<cliente>(vidEnt)).add(cliSelected))) return false;
            } else {
                emplSelected.idEmpleado = usuSelected.idUsuarios;
                emplSelected.usuarios = usuSelected;
                if (!(new MVBaseCRUD<empleado>(new ServicioGenerico<empleado>(vidEnt)).add(emplSelected))) return false;
            }

            return true;
        }

        public bool borrar() {
            if (!delete(usuSelected)) return false;

            listUsuarios.Remove(usuSelected);

            return true;
        }

        public bool editar() {
            return update(usuSelected);
        }

        public Boolean validarPassword(String pass) {
            if (pass.Length < 8) return false;

            int validConditions = 0;
            foreach (char c in pass) {
                if (c >= 'a' && c <= 'z') {
                    validConditions++;
                    break;
                } else if (c >= 'A' && c <= 'Z') {
                    validConditions++;
                    break;
                }
            }
            if (validConditions == 0) return false;
            foreach (char c in pass) {
                if (c >= '0' && c <= '9') {
                    validConditions++;
                    break;
                }
            }
            if (validConditions == 1) return false;
            char[] special = { '@', '#', '$', '%', '^', '&', '+', '=', '*', '/', '(', ')' };
            if (pass.IndexOfAny(special) == -1) return false;
            else
                return true;
        }
    }
}
