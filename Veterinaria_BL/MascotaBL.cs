using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinaria_BE;
using Veterinaria_ADO;

namespace Veterinaria_BL
{
    public class MascotaBL
    {
        public MascotasBE Datos_de_Mascota(String ID_mascotas)
        {
            try
            {
                MascotasADO objMascotaADO = new MascotasADO();
                return objMascotaADO.Datos_de_Mascota(ID_mascotas);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar la mascota: " + ex.Message);
            }
        }
    }
}
