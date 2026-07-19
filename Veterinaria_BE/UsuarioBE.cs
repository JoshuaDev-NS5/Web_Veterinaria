using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veterinaria_BE
{
    public class UsuarioBE
    {
        public string Login_Usuario { get; set; }

        public string Pass_Usuario { get; set; }

        public Int32 Niv_Usuario { get; set; }

        public Int32 Est_Usuario { get; set; }

        public DateTime Fec_Registro { get; set; }

        public string Usu_Registro { get; set; }
    }
}
