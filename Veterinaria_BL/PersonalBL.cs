using System;
using System.Collections.Generic;
using Veterinaria_ADO;
using Veterinaria_BE;

namespace Veterinaria_BL
{
    public class PersonalBL
    {
        public List<PersonalBE> ListarPersonal()
        {
            try
            {
                return new PersonalADO().ListarPersonal();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en PersonalBL: " + ex.Message, ex);
            }
        }
    }
}
