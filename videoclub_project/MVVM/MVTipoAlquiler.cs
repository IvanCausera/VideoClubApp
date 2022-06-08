using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using videoclub_project.Backend.Modelo;
using videoclub_project.Backend.Servicios;

namespace videoclub_project.MVVM {
    internal class MVTipoAlquiler : MVBaseCRUD<tipo_alquiler> {
        // Variables privadas ****************************************************************************************
        private videoclubEntities vidEnt;

        private tipo_alquiler tipoSel;

        private ServicioTipoAlquiler servTipo;

        private ListCollectionView listView;

        // Constructor ***********************************************************************************************
        public MVTipoAlquiler(videoclubEntities vidEnt) { 
            this.vidEnt = vidEnt;

            servTipo = new ServicioTipoAlquiler(vidEnt);
            servicio = servTipo;

            tipoSel = new tipo_alquiler();
            listView = new ListCollectionView(servTipo.getAll().ToList());
        }

        // List ******************************************************************************************************

        public ListCollectionView listTipos {
            get { return listView; }
        }

        // Getters and Setters ***************************************************************************************

        public tipo_alquiler tipoSelected {
            get { return tipoSel; }
            set { tipoSel = value; NotifyPropertyChanged(nameof(tipoSelected)); }
        }

        // Methods ***************************************************************************************************

        public bool guardar() {
            return add(tipoSelected);
        }

        public bool editar() {
            return update(tipoSelected);
        }

        public bool borrar() {
            if(!delete(tipoSelected)) return false;

            listTipos.Remove(tipoSelected);

            return true;
        }

    }
}
