using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinaria_BE;
namespace Veterinaria_ADO
{
    public class MascotasADO
    {
        public MascotasBE Datos_de_Mascota(String ID_mascotas)
        {
            try
            {
                using (SISTEM_VETERINARIAEntities Veterinaria = new SISTEM_VETERINARIAEntities())
                {
                    //Usamos un proceso almacenado para obtener los datos de la mascota
                    var query = Veterinaria.usp_Consultar_MASCOTA(ID_mascotas).FirstOrDefault();

                    if (query == null)
                    {
                        return null;
                    }

                    MascotasBE objMascotaBE = new MascotasBE();
                    objMascotaBE.ID_mascota = query.ID_mascota;
                    objMascotaBE.nomMascota = query.nomMascota;
                    objMascotaBE.Cliente = query.Cliente;
                    objMascotaBE.telefono = query.telefono;
                    objMascotaBE.tipo_mascota = query.tipo_mascota;
                    objMascotaBE.raza = query.nombre_raza;
                    objMascotaBE.edad = Convert.ToInt32(query.Edad ?? 0);
                    objMascotaBE.peso = Convert.ToDecimal(query.peso ?? 0);
                    objMascotaBE.genero = query.Genero;
                    objMascotaBE.Foto = query.Foto;

                    return objMascotaBE;
                }

                
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar la mascota: " + ex.Message);
            }

        }
    }
}
