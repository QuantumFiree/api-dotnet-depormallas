using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace api_depormallas.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Esmetroslineales { get; set; }
        public bool Esmetroscuadrados { get; set; }
        public bool Esgramos { get; set; }
        public bool Eskilogramos { get; set; }
        public bool Estoneladas { get; set; }
        public decimal Valorporunidad { get; set; }

        public Producto() // Constructor por defecto requerido para Dapper
        {
            Id = 0;
            Nombre = string.Empty;
            Descripcion = string.Empty;
            Esmetroslineales = false;
            Esmetroscuadrados = false;
            Esgramos = false;
            Eskilogramos = false;
            Estoneladas = false;
            Valorporunidad = 0.0m;            
        }
        public Producto(int id, string nombre, string descripcion, bool esmetroslineales, bool esmetroscuadrados,
            bool esgramos, bool eskilogramos, bool estoneladas, int valorporunidad)
        {
            Id = id;
            Nombre = nombre;
            Descripcion = descripcion;
            Esmetroslineales = esmetroslineales;
            Esmetroscuadrados = esmetroscuadrados;
            Esgramos = esgramos;
            Eskilogramos = eskilogramos;
            Estoneladas = estoneladas;
            Valorporunidad = valorporunidad;
        }
    }
}
