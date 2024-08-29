using System; // Importa el espacio de nombres para funcionalidades básicas de C#
using System.Collections.Generic; // Permite el uso de listas y colecciones genéricas
using System.Linq; // Permite realizar consultas sobre colecciones
using System.Text; // Proporciona clases para manipular cadenas
using System.Threading.Tasks; // Permite la programación asíncrona

namespace DatosLayer // Define el espacio de nombres para la capa de datos
{
    public class Customers // Clase que representa un cliente
    {
        // Propiedades del cliente
        public string CustomerID { get; set; } // ID del cliente
        public string CompanyName { get; set; } // Nombre de la empresa
        public string ContactName { get; set; } // Nombre del contacto
        public string ContactTitle { get; set; } // Título del contacto
        public string Address { get; set; } // Dirección del cliente
        public string City { get; set; } // Ciudad del cliente
        public string Region { get; set; } // Región del cliente
        public string PostalCode { get; set; } // Código postal del cliente
        public string Country { get; set; } // País del cliente
        public string Phone { get; set; } // Teléfono del cliente
        public string Fax { get; set; } // Fax del cliente
    }
}