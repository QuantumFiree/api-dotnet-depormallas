using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace api_depormallas.Models
{
    public class ConcpetoGastoServicio
    {
        public int Id { get; set; }
        public int Idservicio { get; set; }
        public int Idarchivo { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fechagasto { get; set; }
        public int Valorgasto { get; set; }

        public ConcpetoGastoServicio() // Constructor por defecto requerido para Dapper
        {
            Id = 0;
            Idservicio = 0;
            Idarchivo = 0;
            Descripcion = string.Empty;
            Fechagasto = DateTime.MinValue;
            Valorgasto = 0;
        }
        public ConcpetoGastoServicio(int id, int idservicio, int idarchivo, string descripcion, DateTime fechagasto, int valorgasto)
        {
            Id = id;
            Idservicio = idservicio;
            Idarchivo = idarchivo;
            Descripcion = string.Empty;
            Fechagasto = fechagasto;
            Valorgasto = valorgasto;
        }
    }
}
