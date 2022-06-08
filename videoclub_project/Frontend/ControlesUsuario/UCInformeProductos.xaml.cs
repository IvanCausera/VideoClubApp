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
    /// Lógica de interacción para UCInformeProductos.xaml
    /// </summary>
    public partial class UCInformeProductos : UserControl {

        private videoclubEntities vidEnt;
        private MVProduct mProduct;

        private string SQL_QUERY;

        public UCInformeProductos(videoclubEntities ent) {
            InitializeComponent();

            vidEnt = ent;
            mProduct = new MVProduct(vidEnt);
            DataContext = mProduct;
            CargarInforme();
        }

        public void CargarInforme() {
            // Creamos un nuevo objeto Documento
            ReportDocument rd = new ReportDocument();
            // Indicamos la ruta del informe
            rd.Load("../../Informes/CPProduct.rpt");
            // Obtenemos el servicio asociado
            ServicioSQL sqlServ = new ServicioSQL();

            SQL_QUERY = "select titulo, fecha, nota, genero, idi.idioma, idi2.idioma AS doblado, edad_nota " +
                "from productos prod " +
                "inner join generos gen on prod.id_genero = gen.idGeneros " +
                "inner join idiomas idi on prod.id_idioma = idi.idIdiomas " +
                "inner join idiomas idi2 on prod.id_idiomaDoblado = idi2.IdIdiomas " +
                "WHERE 1 = 1";

            // Rellenamos la fuente de datos del informe con los datos
            // que obtenemos del servicio SQL mediante el método getDatos
            // al cual le pasamos la sentencia SQL
            rd.SetDataSource(sqlServ.getDatos(SQL_QUERY));
            // Rellenamos los datos del informe
            crvInformeProductos.ViewerCore.ReportSource = rd;
        }
    }
}
