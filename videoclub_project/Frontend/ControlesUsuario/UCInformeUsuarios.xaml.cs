using CrystalDecisions.CrystalReports.Engine;
using MySql.Data.MySqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using videoclub_project.Backend.Modelo;
using videoclub_project.Backend.Servicios;
using videoclub_project.MVVM;

namespace videoclub_project.Frontend.ControlesUsuario {
    /// <summary>
    /// Interaction logic for UCInformeUsuarios.xaml
    /// </summary>
    public partial class UCInformeUsuarios : UserControl {

        private videoclubEntities vidEnt;
        private MVUser mUser;

        private string SQL_QUERY;

        public UCInformeUsuarios(videoclubEntities ent) {
            InitializeComponent();
            vidEnt = ent;
            mUser = new MVUser(vidEnt);
            DataContext = mUser;
            CargarInforme();
        }

        public void CargarInforme() {
            // Creamos un nuevo objeto Documento
            ReportDocument rd = new ReportDocument();
            // Indicamos la ruta del informe
            rd.Load("../../Informes/CPUsuario.rpt");
            // Obtenemos el servicio asociado
            ServicioSQL sqlServ = new ServicioSQL();

            SQL_QUERY = "SELECT CONCAT(nombre, ' ', apellido1, ' ', ifnull(apellido2, '')) AS nombreCompleto, direccion, mail, telefono, fecha_nacimiento, user, rol.rol " +
                "FROM usuarios usu " +
                "inner join roles rol on usu.id_rol=rol.idRoles " +
                "WHERE 1 = 1";

            // Rellenamos la fuente de datos del informe con los datos
            // que obtenemos del servicio SQL mediante el método getDatos
            // al cual le pasamos la sentencia SQL
            rd.SetDataSource(sqlServ.getDatos(SQL_QUERY));
            // Rellenamos los datos del informe
            crvInformeUsuarios.ViewerCore.ReportSource = rd;
        }
    }
}
