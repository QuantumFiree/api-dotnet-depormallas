using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_depormallas.Models
{
    public class ServicioPrestado
    {
        public int Id { get; set; }
        public int Idtiposervicio { get; set; }
        public int Idclienteservicio { get; set; }
        public int Valortotal { get; set; }
        public int Numerodepagos { get; set; }
        public int Valorpendiente { get; set; }
        public DateTime Fechainicio { get; set; }
        public DateTime Fechafin { get; set; }
        public DateTime Fechafinestimada { get; set; }
        public int Idestado { get; set; }


        public ServicioPrestado() // Constructor por defecto requerido para Dapper
        {
            Id = 0;
            Idtiposervicio = 0;
            Idclienteservicio = 0;
            Valortotal = 0;
            Numerodepagos = 0;
            Valorpendiente = 0;
            Fechainicio = DateTime.Now;
            Fechafin = DateTime.Now;
            Fechafinestimada = DateTime.Now;
            Idestado = 0;
        }
        public ServicioPrestado(int id, int idtiposervicio, int idclienteservicio, int valortotal, 
            int numerodepagos, int valorpendiente, DateTime fechainicio, DateTime fechafin, DateTime fechafinestimada, int idestado)
        {
            Id = id;
            Idtiposervicio = idtiposervicio;
            Idclienteservicio = idclienteservicio;
            Valortotal = valortotal;
            Numerodepagos = numerodepagos;
            Valorpendiente = valorpendiente;
            Fechainicio = fechainicio;
            Fechafin = fechafin;
            Fechafinestimada = fechafinestimada;
            Idestado = idestado;
        }
    }
}
