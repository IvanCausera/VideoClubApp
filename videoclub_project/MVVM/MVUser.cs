using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using videoclub_project.Backend.Modelo;
using videoclub_project.Backend.Servicios;

namespace videoclub_project.MVVM {
    class MVUser: MVBaseCRUD<usuarios> {
        // Variables privadas ***************************************************************************************
        private videoclubEntities vidEnt;

        private roles rolSel;
        private usuarios usuSel;

        private ServicioUsuario servUsu;

        private ListCollectionView listView;

        // Constructor ***************************************************************************************
        public MVUser(videoclubEntities vidEnt) {
            this.vidEnt = vidEnt;

            servUsu = new ServicioUsuario(vidEnt);

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

        // Metodos ***************************************************************************************
    }
}
