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
    /// Lógica de interacción para UCInformeVentas.xaml
    /// </summary>
    public partial class UCInformeVentas : UserControl {

        private videoclubEntities vidEnt;
        private MVVenta mVenta;

        private string SQL_QUERY;
        private List<MySqlParameter> parametros;

        public UCInformeVentas(videoclubEntities ent) {
            InitializeComponent();
            vidEnt = ent;
            mVenta = new MVVenta(vidEnt);
            DataContext = mVenta;
            CargarInforme();
        }

        public void CargarInforme() {
            // Creamos un nuevo objeto Documento
            ReportDocument rd = new ReportDocument();
            // Indicamos la ruta del informe
            rd.Load("../../Informes/CPVenta.rpt");
            // Obtenemos el servicio asociado
            ServicioSQL sqlServ = new ServicioSQL();

            cargaParametros();

            // Rellenamos la fuente de datos del informe con los datos
            // que obtenemos del servicio SQL mediante el método getDatos
            // al cual le pasamos la sentencia SQL
            rd.SetDataSource(sqlServ.getDatos(SQL_QUERY, parametros));
            // Rellenamos los datos del informe
            crvInformeVentas.ViewerCore.ReportSource = rd;
        }

        private void cargaParametros() {
            SQL_QUERY = "select CONCAT(nombre, ' ', apellido1, ' ', ifnull(apellido2, '')) AS nombreCompleto, vent.fecha, cli.numero_tarjeta, prod.titulo, ifnull(plataformas.plataforma, formatos.formato) AS formato from ventas vent" +
                " inner join cliente cli on vent.id_cliente = cli.idCliente" +
                " inner join usuarios usu on usu.idUsuarios = cli.idCliente" +
                " inner join ventas_productos ventpro on vent.idVentas = ventpro.id_venta" +
                " inner join item on ventpro.id_producto = item.idItem" +
                " left join plataformas_videojuegos plat on item.idItem = plat.id" +
                " left join plataformas on plataformas.idPlataformas = plat.id_plataforma" +
                " left join formatos_peliculas form on item.idItem = form.id" +
                " left join formatos on formatos.idFormatos = form.id_formato" +
                " left join videojuegos vid on plat.id_videojuego = vid.idVideojuegos" +
                " left join peliculas peli on form.id_pelicula = peli.idPeliculas" +
                " left join productos prod on prod.idProductos = vid.idVideojuegos or prod.idProductos = peli.idPeliculas" +
                " WHERE 1 = 1";
            /*
            parametros = new List<MySqlParameter>();

           if (mUser.usuarioNuevo != null) {
               MySqlParameter paramUsu = new MySqlParameter();
               paramUsu.ParameterName = "usu";

               paramUsu.Value = ((usuario)mvUsu.usuarioNuevo).idusuario;
               SQL_QUERY += " AND u.idusuario = @usu";
               parametros.Add(paramUsu);
           }

           if (mvUsu.mesSeleccionado != 0) {
               MySqlParameter paramMes = new MySqlParameter();
               paramMes.ParameterName = "mes";

               paramMes.Value = mvUsu.mesSeleccionado;
               SQL_QUERY += " AND MONTH(s.fechasalida) = @mes";
               parametros.Add(paramMes);
           }
           */
        }
    }
}
