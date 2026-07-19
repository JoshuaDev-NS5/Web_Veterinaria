using System;
using System.Collections.Generic;
using System.Linq;
using Veterinaria_BE;

namespace Veterinaria_ADO
{
    public class PersonalADO
    {
        public List<PersonalBE> ListarPersonal()
        {
            try
            {
                using (SISTEM_VETERINARIAEntities db = new SISTEM_VETERINARIAEntities())
                {
                    return db.usp_Listar_PERSONAL()
                        .Select(p => new PersonalBE
                        {
                            ID_Empleado = p.ID_de_empleado,
                            NombreCompleto = p.NombreCompleto,
                            DNI = p.DNI,
                            Telefono = p.Telefono,
                            Email = p.Email,
                            Cargo = p.Cargo,
                            HoraEntrada = p.HoraEntrada,
                            HoraSalida = p.HoraSalida
                        })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar el personal: " + ex.Message, ex);
            }
        }
    }
}
