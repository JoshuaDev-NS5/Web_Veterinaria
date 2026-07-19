using System;
using System.Collections.Generic;
using Veterinaria_BE;
using Veterinaria_ADO;

namespace Veterinaria_BL
{
    public class VeterinarioBL
    {
        public List<VeterinarioBE> Listar_Veterinarios(string especialidad = "", string estado = "")
        {
            try
            {
                VeterinarioADO objADO = new VeterinarioADO();
                return objADO.Listar_Veterinarios(especialidad, estado);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en VeterinarioBL: " + ex.Message);
            }
        }

        public List<VeterinarioBE> Listar_Veterinarios_por_Filtros(string especialidad = "", string estado = "")
        {
            try
            {
                VeterinarioADO objADO = new VeterinarioADO();
                return objADO.Listar_Veterinarios_Por_Filtros(especialidad, estado);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en VeterinarioBL: " + ex.Message);
            }
        }
    }
}