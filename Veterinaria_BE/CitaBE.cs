using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veterinaria_BE
{   //Clase cita para el uso del procedimineto almacenado de listar citas_por_mascotas
    public class CitaBE
    {
        public String ID_cita {  get; set; }

        public DateTime fecha { get; set; }

        public String tipo_cita { get; set; }

        public String motivo { get; set; }

        public String medicamento { get; set; }

        public String Cliente {  get; set; }

        public String Mascota   { get; set; }

        public String Veterinario { get; set; }
        
        public String Empleado { get; set; }
            

    }
}
