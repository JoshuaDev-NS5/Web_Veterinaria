using System;

namespace Veterinaria_BE
{
    public class OpcionBE
    {
        public string Id { get; set; }
        public string Descripcion { get; set; }
    }

    public class RegistroCitaBE
    {
        public string ID_Cita { get; set; }
        public string ID_Empleado { get; set; }
        public string ID_Veterinario { get; set; }
        public string ID_Cliente { get; set; }
        public string ID_TipoCita { get; set; }
        public string ID_Mascota { get; set; }
        public string ID_Tratamiento { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime Hora { get; set; }
        public string Motivo { get; set; }
        public decimal Precio { get; set; }
        public string Observaciones { get; set; }
    }

    public class TratamientoBE
    {
        public string ID_Tratamiento { get; set; }
        public int ID_Medicamento { get; set; }
        public string Dosis { get; set; }
        public string Frecuencia { get; set; }
        public string ID_Vacuna { get; set; }
        public string MiligramosVacuna { get; set; }
    }
}
