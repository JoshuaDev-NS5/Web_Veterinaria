using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinaria_BE;

namespace Veterinaria_ADO
{
    public class UsuarioADO
    {   
        //Para verificar el usuario y contraseña y traer los datos cuando se necesiten
        public UsuarioBE ConsultarUsuario(string strLogin, string strPassword)
        {
            try
            {
                //Creamos una instacia de la clase UsuarioBE para almacenar los datos del usuario
                UsuarioBE objUsuarioBE=new UsuarioBE();

                //Instanciamos la clase de la base de datos para poder consultar los datos del usuario
                using (SISTEM_VETERINARIAEntities objVeterinaria = new SISTEM_VETERINARIAEntities())
                {   //Usamos un linq para consultar el usuario y contraseña en la base de datos y traer los datos del usuario
                    Tb_Usuario objUsuario = objVeterinaria.Tb_Usuario.FirstOrDefault(miUsuario => miUsuario.Login_Usuario == strLogin && miUsuario.Pass_Usuario == strPassword);

                    if (objUsuario == null)//Si no se encuentra el usuario, se retorna null en la variable BE
                    {
                        objUsuarioBE = new UsuarioBE();
                    }
                    else
                    {
                        objUsuarioBE.Login_Usuario = objUsuario.Login_Usuario;
                        objUsuarioBE.Niv_Usuario = Convert.ToInt32(objUsuario.Niv_Usuario);

                    }
                    return objUsuarioBE;
                }

            }
            catch (Exception ex) {
            
                    throw new Exception("Error al consultar el usuario: " + ex.Message);
            }
        }
    }
}
