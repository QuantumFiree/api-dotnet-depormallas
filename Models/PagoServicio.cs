using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace api_depormallas.Models
{
    public class PagoServicio
    {
        public int Id { get; set; }
        public int Idservicio { get; set; }
        public int Idarchivo { get; set; }
        public int Valorpago { get; set; }
        public DateTime Fechapago { get; set; }

        public PagoServicio() // Constructor por defecto requerido para Dapper
        {
            Id = 0;
            Idservicio = 0;
            Idarchivo = 0;
            Valorpago = 0;
            Fechapago = DateTime.Now;
        }
        public PagoServicio(int id, int idservicio, int idarchivo, int valorpago, DateTime fechapago)
        {
            Id = id;
            Idservicio = idservicio;
            Idarchivo = idarchivo;
            Valorpago = valorpago;
            Fechapago = fechapago;
        }
    }
}
