using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veterinaria_BE
{
    public class MascotasBE
    {
        public String ID_mascota { get; set; }
        public String id_estado { get; set; }

        public String estado { get; set; }
        public String id_raza { get; set; }
        public String id_tipo_mascota { get; set; }
        public String nomMascota { get; set; }
        public String tipo_mascota { get; set; }
        public DateTime fecha_de_nacimiento { get; set; }
        public Decimal peso { get; set; }
        public String ID_cliente { get; set; }
        public String Cliente { get; set; }

        public Int32 edad { get; set; }

        public String raza { get; set; }
        public String genero { get; set; }
        public byte[] Foto { get; set; }
        public DateTime Fec_Registro { get; set; }
        public String Usu_Registro { get; set; }
        public DateTime Fec_Ult_Mod { get; set; }
        public String Usu_Ult_Mod { get; set; }

        public string telefono { get; set; }
    }
}
