using System; // Importa el espacio de nombres para las funcionalidades básicas de C#
using System.Collections.Generic; // Permite el uso de listas y colecciones genéricas
using System.Data.SqlClient; // Proporciona clases para la conexión y ejecución de comandos SQL
using System.Linq; // Permite consultas sobre colecciones
using System.Net.Http.Headers; // Maneja encabezados HTTP
using System.Text; // Proporciona clases para manipular cadenas
using System.Threading.Tasks; // Permite la programación asíncrona

namespace DatosLayer // Define el espacio de nombres para la capa de datos
{
    public class CustomerRepository // Clase que maneja operaciones sobre clientes
    {
        // Método para obtener todos los clientes
        public List<Customers> ObtenerTodos()
        {
            // Establece la conexión con la base de datos
            using (var conexion = DataBase.GetSqlConnection())
            {
                String selectFrom = ""; // Inicializa la consulta SQL

                // Construye la consulta SQL para seleccionar todos los campos de la tabla Customers
                selectFrom = selectFrom + "SELECT [CustomerID] " + "\n";
                selectFrom = selectFrom + " ,[CompanyName] " + "\n";
                selectFrom = selectFrom + " ,[ContactName] " + "\n";
                selectFrom = selectFrom + " ,[ContactTitle] " + "\n";
                selectFrom = selectFrom + " ,[Address] " + "\n";
                selectFrom = selectFrom + " ,[City] " + "\n";
                selectFrom = selectFrom + " ,[Region] " + "\n";
                selectFrom = selectFrom + " ,[PostalCode] " + "\n";
                selectFrom = selectFrom + " ,[Country] " + "\n";
                selectFrom = selectFrom + " ,[Phone] " + "\n";
                selectFrom = selectFrom + " ,[Fax] " + "\n";
                selectFrom = selectFrom + " FROM [dbo].[Customers]"; // Especifica la tabla de la que se selecciona

                using (SqlCommand comando = new SqlCommand(selectFrom, conexion))
                {
                    SqlDataReader reader = comando.ExecuteReader(); // Ejecuta la consulta y obtiene un lector de datos
                    List<Customers> Customers = new List<Customers>(); // Inicializa la lista de clientes

                    // Lee los datos devueltos por la consulta
                    while (reader.Read())
                    {
                        var customers = LeerDelDataReader(reader); // Llama a un método para mapear los datos a un objeto Customers
                        Customers.Add(customers); // Agrega el objeto a la lista
                    }

                    return Customers; // Devuelve la lista de clientes
                }
            }
        }

        // Método para obtener un cliente por su ID
        public Customers ObtenerPorID(string id)
        {
            using (var conexion = DataBase.GetSqlConnection())
            {
                String selectForID = ""; // Inicializa la consulta SQL

                // Construye la consulta SQL para seleccionar un cliente específico
                selectForID = selectForID + "SELECT [CustomerID] " + "\n";
                selectForID = selectForID + " ,[CompanyName] " + "\n";
                selectForID = selectForID + " ,[ContactName] " + "\n";
                selectForID = selectForID + " ,[ContactTitle] " + "\n";
                selectForID = selectForID + " ,[Address] " + "\n";
                selectForID = selectForID + " ,[City] " + "\n";
                selectForID = selectForID + " ,[Region] " + "\n";
                selectForID = selectForID + " ,[PostalCode] " + "\n";
                selectForID = selectForID + " ,[Country] " + "\n";
                selectForID = selectForID + " ,[Phone] " + "\n";
                selectForID = selectForID + " ,[Fax] " + "\n";
                selectForID = selectForID + " FROM [dbo].[Customers] " + "\n";
                selectForID = selectForID + $" Where CustomerID = @customerId"; // Filtra por CustomerID

                using (SqlCommand comando = new SqlCommand(selectForID, conexion))
                {
                    comando.Parameters.AddWithValue("customerId", id); // Agrega el parámetro de ID
                    var reader = comando.ExecuteReader(); // Ejecuta la consulta

                    Customers customers = null; // Inicializa el objeto de cliente

                    // Valida si se encontró un cliente
                    if (reader.Read())
                    {
                        customers = LeerDelDataReader(reader); // Mapea los datos a un objeto Customers
                    }

                    return customers; // Devuelve el cliente encontrado
                }
            }
        }

        // Método para leer datos del SqlDataReader y mapearlos a un objeto Customers
        public Customers LeerDelDataReader(SqlDataReader reader)
        {
            Customers customers = new Customers(); // Crea un nuevo objeto Customers

            // Asigna valores a las propiedades del objeto, manejando posibles valores nulos
            customers.CustomerID = reader["CustomerID"] == DBNull.Value ? " " : (String)reader["CustomerID"];
            customers.CompanyName = reader["CompanyName"] == DBNull.Value ? "" : (String)reader["CompanyName"];
            customers.ContactName = reader["ContactName"] == DBNull.Value ? "" : (String)reader["ContactName"];
            customers.ContactTitle = reader["ContactTitle"] == DBNull.Value ? "" : (String)reader["ContactTitle"];
            customers.Address = reader["Address"] == DBNull.Value ? "" : (String)reader["Address"];
            customers.City = reader["City"] == DBNull.Value ? "" : (String)reader["City"];
            customers.Region = reader["Region"] == DBNull.Value ? "" : (String)reader["Region"];
            customers.PostalCode = reader["PostalCode"] == DBNull.Value ? "" : (String)reader["PostalCode"];
            customers.Country = reader["Country"] == DBNull.Value ? "" : (String)reader["Country"];
            customers.Phone = reader["Phone"] == DBNull.Value ? "" : (String)reader["Phone"];
            customers.Fax = reader["Fax"] == DBNull.Value ? "" : (String)reader["Fax"];

            return customers; // Devuelve el objeto Customers lleno de datos
        }

        // Método para insertar un nuevo cliente
        public int InsertarCliente(Customers customer)
        {
            using (var conexion = DataBase.GetSqlConnection())
            {
                String insertInto = ""; // Inicializa la consulta SQL

                // Construye la consulta SQL para insertar un nuevo cliente
                insertInto = insertInto + "INSERT INTO [dbo].[Customers] " + "\n";
                insertInto = insertInto + " ([CustomerID] " + "\n";
                insertInto = insertInto + " ,[CompanyName] " + "\n";
                insertInto = insertInto + " ,[ContactName] " + "\n";
                insertInto = insertInto + " ,[ContactTitle] " + "\n";
                insertInto = insertInto + " ,[Address] " + "\n";
                insertInto = insertInto + " ,[City]) " + "\n";
                insertInto = insertInto + " VALUES " + "\n";
                insertInto = insertInto + " (@CustomerID " + "\n";
                insertInto = insertInto + " ,@CompanyName " + "\n";
                insertInto = insertInto + " ,@ContactName " + "\n";
                insertInto = insertInto + " ,@ContactTitle " + "\n";
                insertInto = insertInto + " ,@Address " + "\n";
                insertInto = insertInto + " ,@City)"; // Especifica los valores a insertar

                using (var comando = new SqlCommand(insertInto, conexion))
                {
                    int insertados = parametrosCliente(customer, comando); // Llama al método para agregar parámetros y ejecuta el comando
                    return insertados; // Devuelve el número de registros insertados
                }
            }
        }

        // Método para actualizar un cliente existente
        public int ActualizarCliente(Customers customer)
        {
            using (var conexion = DataBase.GetSqlConnection())
            {
                String ActualizarCustomerPorID = ""; // Inicializa la consulta SQL

                // Construye la consulta SQL para actualizar un cliente
                ActualizarCustomerPorID = ActualizarCustomerPorID + "UPDATE [dbo].[Customers] " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + " SET [CustomerID] = @CustomerID " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + " ,[CompanyName] = @CompanyName " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + " ,[ContactName] = @ContactName " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + " ,[ContactTitle] = @ContactTitle " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + " ,[Address] = @Address " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + " ,[City] = @City " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + " WHERE CustomerID= @CustomerID"; // Filtra por CustomerID

                using (var comando = new SqlCommand(ActualizarCustomerPorID, conexion))
                {
                    int actualizados = parametrosCliente(customer, comando); // Llama al método para agregar parámetros y ejecuta el comando
                    return actualizados; // Devuelve el número de registros actualizados
                }
            }
        }

        // Método para agregar parámetros a un comando SQL
        public int parametrosCliente(Customers customer, SqlCommand comando)
        {
            // Agrega los parámetros correspondientes del objeto Customers al comando SQL
            comando.Parameters.AddWithValue("CustomerID", customer.CustomerID);
            comando.Parameters.AddWithValue("CompanyName", customer.CompanyName);
            comando.Parameters.AddWithValue("ContactName", customer.ContactName);
            comando.Parameters.AddWithValue("ContactTitle", customer.ContactTitle);
            comando.Parameters.AddWithValue("Address", customer.Address);
            comando.Parameters.AddWithValue("City", customer.City);

            var insertados = comando.ExecuteNonQuery(); // Ejecuta el comando y devuelve el número de registros afectados
            return insertados; // Devuelve el número de registros insertados o actualizados
        }

        // Método para eliminar un cliente
        public int EliminarCliente(string id)
        {
            using (var conexion = DataBase.GetSqlConnection())
            {
                String EliminarCliente = ""; // Inicializa la consulta SQL

                // Construye la consulta SQL para eliminar un cliente
                EliminarCliente = EliminarCliente + "DELETE FROM [dbo].[Customers] " + "\n";
                EliminarCliente = EliminarCliente + " WHERE CustomerID = @CustomerID"; // Filtra por CustomerID

                using (SqlCommand comando = new SqlCommand(EliminarCliente, conexion))
                {
                    comando.Parameters.AddWithValue("@CustomerID", id); // Agrega el parámetro de ID
                    int elimindos = comando.ExecuteNonQuery(); // Ejecuta el comando y obtiene el número de registros eliminados
                    return elimindos; // Devuelve el número de registros eliminados
                }
            }
        }
    }
}