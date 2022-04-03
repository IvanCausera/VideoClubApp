using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using videoclub_project.Backend.Modelo;

namespace videoclub_project.MVVM {
    class MVProduct : MVBaseCRUD<productos> {
        // Variables privadas ***************************************************************************************
        private videoclubEntities vidEnt;

        // Constructor ***************************************************************************************
        public MVProduct(videoclubEntities vidEnt) {
            this.vidEnt = vidEnt;
        }
    }
}
