using System;

namespace Veterinaria_BE
{
    public class PersonalBE
    {
        public string ID_Empleado { get; set; }
        public string NombreCompleto { get; set; }
        public string DNI { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Cargo { get; set; }
        public string HoraEntrada { get; set; }
        public string HoraSalida { get; set; }

        public bool EsTurnoManana
        {
            get
            {
                TimeSpan hora;
                return TimeSpan.TryParse(HoraEntrada, out hora) && hora.Hours < 12;
            }
        }
    }
}
