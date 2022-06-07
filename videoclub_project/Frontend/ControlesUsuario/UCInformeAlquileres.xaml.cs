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
    /// Lógica de interacción para UCInformeAlquileres.xaml
    /// </summary>
    public partial class UCInformeAlquileres : UserControl {

        private videoclubEntities vidEnt;
        private MVAlquiler mAlquiler;

        private string SQL_QUERY;

        public UCInformeAlquileres(videoclubEntities ent) {
            InitializeComponent();

            vidEnt = ent;
            mAlquiler = new MVAlquiler(vidEnt);
            DataContext = mAlquiler;
            CargarInforme();
        }

        public void CargarInforme() {
            // Creamos un nuevo objeto Documento
            ReportDocument rd = new ReportDocument();
            // Indicamos la ruta del informe
            rd.Load("../../Informes/CPAlquiler.rpt");
            // Obtenemos el servicio asociado
            ServicioSQL sqlServ = new ServicioSQL();

            SQL_QUERY = "select CONCAT(usu.nombre, ' ', usu.apellido1, ' ', ifnull(usu.apellido2, '')) AS nombreCompleto, alq.fecha, alq.fecha_devolucion, tipo.nombre AS tipoAlquiler, tipo.duracion, tipo.precio, tipo.recargo, prod.titulo, ifnull(plataformas.plataforma, formatos.formato) AS formato from alquileres alq " +
                "inner join tipo_alquiler tipo on alq.id_tipo_alquiler = tipo.idTipo_alquiler" +
                " inner join cliente cli on alq.id_cliente = cli.idCliente" +
                " inner join usuarios usu on usu.idUsuarios = cli.idCliente" +
                " inner join productos_alquiler prodAlq on alq.idAlquileres = prodAlq.id_alquiler" +
                " inner join item on prodAlq.id_producto = item.idItem" +
                " left join plataformas_videojuegos plat on item.idItem = plat.id" +
                " left join plataformas on plataformas.idPlataformas = plat.id_plataforma" +
                " left join formatos_peliculas form on item.idItem = form.id" +
                " left join formatos on formatos.idFormatos = form.id_formato" +
                " left join videojuegos vid on plat.id_videojuego = vid.idVideojuegos" +
                " left join peliculas peli on form.id_pelicula = peli.idPeliculas" +
                " left join productos prod on prod.idProductos = vid.idVideojuegos or prod.idProductos = peli.idPeliculas " +
                " WHERE 1 = 1";

            // Rellenamos la fuente de datos del informe con los datos
            // que obtenemos del servicio SQL mediante el método getDatos
            // al cual le pasamos la sentencia SQL
            rd.SetDataSource(sqlServ.getDatos(SQL_QUERY));
            // Rellenamos los datos del informe
            crvInformeAlquileres.ViewerCore.ReportSource = rd;
        }
    }
}
