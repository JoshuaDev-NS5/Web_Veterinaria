using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinaria_ADO;
using Veterinaria_BE;
namespace Veterinaria_BL
{
    public class UsuarioBL
    {
        UsuarioADO objUsuarioADO = new UsuarioADO();

        public UsuarioBE ConsultarUsuario(String Login, String Password)
        {
            
             return objUsuarioADO.ConsultarUsuario(Login, Password);
            
           
        }
    }
}
