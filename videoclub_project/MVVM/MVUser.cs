using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using videoclub_project.Backend.Modelo;
using videoclub_project.Backend.Servicios;

namespace videoclub_project.MVVM {
    class MVUser: MVBaseCRUD<usuarios> {
        // Variables privadas ***************************************************************************************
        private videoclubEntities vidEnt;
        private roles rolSel;

        // Constructor ***************************************************************************************
        public MVUser(videoclubEntities vidEnt) {
            this.vidEnt = vidEnt;
        }
        // Getters and Setters ***************************************************************************************
        public List<roles> listRoles {
            get { return new ServicioGenerico<roles>(vidEnt).getAll().ToList();}
        }
        public roles rolSelected {
            get { return rolSel; }
            set { rolSel = value; NotifyPropertyChanged(nameof(rolSelected)); }
        }

        // Metodos ***************************************************************************************
    }
}
