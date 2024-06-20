using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace api_depormallas.Models
{
    public class TipoDeEstado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public TipoDeEstado() // Constructor por defecto requerido para Dapper
        {
            Id = 0;
            Descripcion = string.Empty;
            Nombre = string.Empty;
        }
        public TipoDeEstado(int id, string descripcion, string nombre)
        {
            Id = id;
            Descripcion = descripcion;
            Nombre = nombre;
        }
    }
}
