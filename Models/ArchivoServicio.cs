using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace api_depormallas.Models
{
    public class ArchivoServicio
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Url { get; set; }
        public DateTime Fechacargue{ get; set; }

        public ArchivoServicio() // Constructor por defecto requerido para Dapper
        {
            Id = 0;
            Descripcion = string.Empty;
            Url = string.Empty;
            Fechacargue = DateTime.MinValue;
        }
        public ArchivoServicio(int id, string descripcion, string url, DateTime fechacargue)
        {
            Id = id;
            Descripcion = descripcion;
            Url = url;
            Fechacargue = fechacargue;
        }
    }
}
