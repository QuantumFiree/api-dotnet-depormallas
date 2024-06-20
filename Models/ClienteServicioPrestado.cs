using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_depormallas.Models
{
    public class ClienteServicioPrestado
    {
        public int Id { get; set; }
        public string Nombrecompleto { get; set; }
        public int Telefono { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }

        public ClienteServicioPrestado() // Constructor por defecto requerido para Dapper
        {
            Id = 0;
            Nombrecompleto = "";
            Telefono = 0;
            Correo = "";
            Direccion = "";
        }
        public ClienteServicioPrestado(int id, string nombrecompleto, int telefono, string correo, string direccion)
        {
            Id = id;
            Nombrecompleto = nombrecompleto;
            Telefono = telefono;
            Correo = correo;
            Direccion = direccion;
        }
    }
}
