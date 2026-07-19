using System;
using System.Collections.Generic;
using System.Linq;
using Veterinaria_BE;

namespace Veterinaria_ADO
{
    public class VeterinarioADO
    {
        public List<VeterinarioBE> Listar_Veterinarios(string especialidad, string estado)
        {
            try
            {
                using (SISTEM_VETERINARIAEntities db = new SISTEM_VETERINARIAEntities())
                {
                    var query = db.VW_VistaVeterinarios.AsQueryable();//Obtener la vista de veterinarios

                    // Filtrar por especialidad y estado si se proporcionan
                    if (!string.IsNullOrEmpty(especialidad) && especialidad != "Todos")//Filtrar por especialidad si se proporciona
                        query = query.Where(v => v.especialidad == especialidad);
                    // Filtrar por estado si se proporciona
                    if (!string.IsNullOrEmpty(estado) && estado != "Todos")
                        query = query.Where(v => v.Estado_Veterinario == estado);

                    return query.Select(v => new VeterinarioBE
                    {
                        ID_vet = v.ID_vet,
                        nomVeterinario = v.nomVeterinario,
                        apellidoVeterinario = v.apellidoVeterinario,
                        NombreCompleto = v.NombreCompleto,
                        telefonoVeterinario = v.telefonoVeterinario,
                        especialidad = v.especialidad,
                        email = v.email,
                        DNI_Veterinario = Convert.ToString(v.DNI_Veterinario),
                        Estado_Veterinario = v.Estado_Veterinario,
                        direccion_Veterinario = v.direccion_Veterinario
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar veterinarios: " + ex.Message);
            }
        }

        public List<VeterinarioBE> Listar_Veterinarios_Por_Filtros(String especialidad, String estado)
        {
            try
            {
                List<VeterinarioBE> lista = new List<VeterinarioBE>();

                using (SISTEM_VETERINARIAEntities Veterinaria = new SISTEM_VETERINARIAEntities())
                {
                    var query = Veterinaria.TB_VETERINARIO.AsQueryable();

                    if (!string.IsNullOrEmpty(especialidad))
                        query = query.Where(v => v.especialidad == especialidad);

                    if (!string.IsNullOrEmpty(estado))
                        query = query.Where(v => v.Estado_Veterinario == estado);

                    foreach (var v in query.ToList())
                    {
                        VeterinarioBE obj = new VeterinarioBE();
                        obj.ID_vet = v.ID_vet;
                        obj.nomVeterinario = v.nomVeterinario;
                        obj.apellidoVeterinario = v.apellidoVeterinario;
                        obj.telefonoVeterinario = v.telefonoVeterinario;
                        obj.especialidad = v.especialidad;
                        obj.email = v.email;
                        obj.DNI_Veterinario = v.DNI_Veterinario;
                        obj.Estado_Veterinario = v.Estado_Veterinario;
                        obj.direccion_Veterinario = v.direccion_Veterinario;
                        lista.Add(obj);
                    }
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar veterinarios: " + ex.Message);
            }
        }


    }
}