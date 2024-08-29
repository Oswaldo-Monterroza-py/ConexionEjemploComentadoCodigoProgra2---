using System; // Importa el espacio de nombres para funcionalidades básicas de C#
using System.Collections.Generic; // Permite el uso de listas y colecciones genéricas
using System.Linq; // Permite realizar consultas sobre colecciones
using System.Text; // Proporciona clases para manipular cadenas
using System.Threading.Tasks; // Permite la programación asíncrona
using System.Configuration; // Proporciona acceso a la configuración de la aplicación
using System.Xml.Linq; // Permite trabajar con XML
using System.Data.SqlClient; // Proporciona clases para la conexión y ejecución de comandos SQL
using System.Runtime.CompilerServices; // Proporciona funcionalidades para la compilación

namespace DatosLayer // Define el espacio de nombres para la capa de datos
{
    public class DataBase // Clase que maneja la conexión a la base de datos
    {
        // Propiedad estática para obtener la cadena de conexión
        public static string ConnectionString
        {
            get
            {
                // Obtiene la cadena de conexión desde la configuración de la aplicación
                string CadenaConexion = ConfigurationManager
                    .ConnectionStrings["NWConnection"]
                    .ConnectionString;

                // Crea un objeto SqlConnectionStringBuilder para modificar la cadena de conexión
                SqlConnectionStringBuilder conexionBuilder =
                    new SqlConnectionStringBuilder(CadenaConexion);

                // Asigna el nombre de la aplicación si no está especificado
                conexionBuilder.ApplicationName =
                    ApplicationName ?? conexionBuilder.ApplicationName;

                // Establece el tiempo de espera de conexión si es mayor que 0
                conexionBuilder.ConnectTimeout = (ConnectionTimeout > 0)
                    ? ConnectionTimeout : conexionBuilder.ConnectTimeout;

                // Devuelve la cadena de conexión final
                return conexionBuilder.ToString();
            }
        }

        // Propiedad estática para establecer el tiempo de espera de conexión
        public static int ConnectionTimeout { get; set; }

        // Propiedad estática para establecer el nombre de la aplicación
        public static string ApplicationName { get; set; }

        // Método estático para obtener una conexión SQL
        public static SqlConnection GetSqlConnection()
        {
            // Crea una nueva conexión SQL usando la cadena de conexión
            SqlConnection conexion = new SqlConnection(ConnectionString);
            conexion.Open(); // Abre la conexión
            return conexion; // Devuelve la conexión abierta
        }
    }
}