using System; // Importa el espacio de nombres para funcionalidades básicas de C#
using System.Collections.Generic; // Permite el uso de listas y colecciones genéricas
using System.ComponentModel; // Proporciona funcionalidades para la creación de componentes
using System.Data; // Proporciona clases para trabajar con datos
using System.Drawing; // Permite el uso de gráficos y dibujos
using System.Linq; // Permite realizar consultas sobre colecciones
using System.Text; // Proporciona clases para manipular cadenas
using System.Threading.Tasks; // Permite la programación asíncrona
using System.Windows.Forms; // Proporciona clases para la creación de formularios Windows
using System.Data.SqlClient; // Proporciona clases para la conexión y ejecución de comandos SQL
using DatosLayer; // Importa clases de la capa de datos
using System.Net; // Proporciona funcionalidades para trabajar con redes
using System.Reflection; // Permite la introspección y manipulación de tipos

namespace ConexionEjemplo // Define el espacio de nombres para la aplicación
{
    public partial class Form1 : Form // Clase que representa el formulario principal
    {
        CustomerRepository customerRepository = new CustomerRepository(); // Instancia de la clase CustomerRepository

        public Form1() // Constructor del formulario
        {
            InitializeComponent(); // Inicializa los componentes del formulario
        }

        private void btnCargar_Click(object sender, EventArgs e) // Evento al hacer clic en el botón "Cargar"
        {
            var Customers = customerRepository.ObtenerTodos(); // Obtiene todos los clientes
            dataGrid.DataSource = Customers; // Asigna los clientes como fuente de datos del DataGrid
        }

        private void textBox1_TextChanged(object sender, EventArgs e) // Evento al cambiar el texto del TextBox
        {
            // var filtro = Customers.FindAll( X => X.CompanyName.StartsWith(tbFiltro.Text));
            // dataGrid.DataSource = filtro;
        }

        private void Form1_Load(object sender, EventArgs e) // Evento al cargar el formulario
        {
            // DatosLayer.DataBase.ApplicationName = "Programacion 2 ejemplo";
            // DatosLayer.DataBase.ConnectionTimeout = 30;
            // string cadenaConexion = DatosLayer.DataBase.ConnectionString;
            // var conxion = DatosLayer.DataBase.GetSqlConnection();
        }

        private void btnBuscar_Click(object sender, EventArgs e) // Evento al hacer clic en el botón "Buscar"
        {
            var cliente = customerRepository.ObtenerPorID(txtBuscar.Text); // Obtiene un cliente por su ID
            tboxCustomerID.Text = cliente.CustomerID; // Asigna el ID del cliente a un TextBox
            tboxCompanyName.Text = cliente.CompanyName; // Asigna el nombre de la empresa a un TextBox
            tboxContacName.Text = cliente.ContactName; // Asigna el nombre del contacto a un TextBox
            tboxContactTitle.Text = cliente.ContactTitle; // Asigna el título del contacto a un TextBox
            tboxAddress.Text = cliente.Address; // Asigna la dirección del cliente a un TextBox
            tboxCity.Text = cliente.City; // Asigna la ciudad del cliente a un TextBox
        }

        private void label4_Click(object sender, EventArgs e) // Evento al hacer clic en una etiqueta
        {
            // No hace nada
        }

        private void btnInsertar_Click(object sender, EventArgs e) // Evento al hacer clic en el botón "Insertar"
        {
            var resultado = 0; // Variable para almacenar el resultado de la inserción
            var nuevoCliente = ObtenerNuevoCliente(); // Obtiene un nuevo cliente a partir de los valores de los TextBox

            if (validarCampoNull(nuevoCliente) == false) // Valida si algún campo está vacío
            {
                resultado = customerRepository.InsertarCliente(nuevoCliente); // Inserta el nuevo cliente
                MessageBox.Show("Guardado" + "Filas modificadas = " + resultado); // Muestra un mensaje con el resultado
            }
            else
            {
                MessageBox.Show("Debe completar los campos por favor"); // Muestra un mensaje indicando que se deben completar los campos
            }
        }

        public Boolean validarCampoNull(Object objeto) // Método para validar si alguna propiedad de un objeto está vacía
        {
            foreach (PropertyInfo property in objeto.GetType().GetProperties()) // Itera sobre las propiedades del objeto
            {
                object value = property.GetValue(objeto, null); // Obtiene el valor de la propiedad
                if ((string)value == "") // Verifica si el valor es una cadena vacía
                {
                    return true; // Devuelve true si encuentra un campo vacío
                }
            }
            return false; // Devuelve false si no encuentra campos vacíos
        }

        private void label5_Click(object sender, EventArgs e) // Evento al hacer clic en una etiqueta
        {
            // No hace nada
        }

        private void btModificar_Click(object sender, EventArgs e) // Evento al hacer clic en el botón "Modificar"
        {
            var actualizarCliente = ObtenerNuevoCliente(); // Obtiene un cliente a partir de los valores de los TextBox
            int actualizadas = customerRepository.ActualizarCliente(actualizarCliente); // Actualiza el cliente
            MessageBox.Show($"Filas actualizadas = {actualizadas}"); // Muestra un mensaje con el número de filas actualizadas
        }

        private Customers ObtenerNuevoCliente() // Método para obtener un nuevo cliente a partir de los valores de los TextBox
        {
            var nuevoCliente = new Customers // Crea un nuevo objeto Customers
            {
                CustomerID = tboxCustomerID.Text, // Asigna el ID del cliente
                CompanyName = tboxCompanyName.Text, // Asigna el nombre de la empresa
                ContactName = tboxContacName.Text, // Asigna el nombre del contacto
                ContactTitle = tboxContactTitle.Text, // Asigna el título del contacto
                Address = tboxAddress.Text, // Asigna la dirección del cliente
                City = tboxCity.Text // Asigna la ciudad del cliente
            };

            return nuevoCliente; // Devuelve el nuevo cliente
        }

        private void btnEliminar_Click(object sender, EventArgs e) // Evento al hacer clic en el botón "Eliminar"
        {
            int elimindas = customerRepository.EliminarCliente(tboxCustomerID.Text); // Elimina un cliente por su ID
            MessageBox.Show("Filas eliminadas = " + elimindas); // Muestra un mensaje con el número de filas eliminadas
        }
    }
}