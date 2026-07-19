using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using Veterinaria_BE;

namespace Veterinaria_ADO
{
    public class CitaADO
    {
        public List<CitaBE> Citas_por_Mascota(string idMascota)
        {
            try
            {
                using (SISTEM_VETERINARIAEntities db = new SISTEM_VETERINARIAEntities())
                {
                    return db.usp_Consultar_CITA_por_Mascota(idMascota)
                        .Select(cita => new CitaBE
                        {
                            ID_cita = cita.ID_cita,
                            fecha = cita.Fecha ?? DateTime.MinValue,
                            tipo_cita = cita.Tipo_de_cita ?? string.Empty,
                            motivo = cita.Motivo ?? string.Empty,
                            medicamento = cita.Medicamento ?? string.Empty,
                            Cliente = cita.Cliente ?? string.Empty,
                            Mascota = cita.Mascota ?? string.Empty,
                            Veterinario = cita.Veterinario ?? string.Empty,
                            Empleado = cita.Empleado ?? string.Empty
                        })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar las citas de la mascota: " + ex.Message, ex);
            }
        }

        public List<CitaBE> Citas_Filtrado_Por_Fecha(string idMascota, DateTime fechaInicio, DateTime fechaFin)
        {
            return Citas_por_Mascota(idMascota)
                .Where(cita => cita.fecha.Date >= fechaInicio.Date && cita.fecha.Date <= fechaFin.Date)
                .ToList();
        }

        public List<OpcionBE> ListarEmpleados()
        {
            using (SISTEM_VETERINARIAEntities db = new SISTEM_VETERINARIAEntities())
            {
                return db.usp_Listar_PERSONAL()
                    .Select(x => new OpcionBE { Id = x.ID_de_empleado, Descripcion = x.NombreCompleto })
                    .ToList();
            }
        }

        public List<OpcionBE> ListarVeterinarios()
        {
            using (SISTEM_VETERINARIAEntities db = new SISTEM_VETERINARIAEntities())
            {
                return db.usp_Listar_VETERINARIOS()
                    .Where(x => string.Equals(x.Estado_Veterinario, "Activo", StringComparison.OrdinalIgnoreCase))
                    .Select(x => new OpcionBE { Id = x.ID_vet, Descripcion = x.NombreCompleto + " - " + x.especialidad })
                    .ToList();
            }
        }

        public List<OpcionBE> ListarClientes()
        {
            using (SISTEM_VETERINARIAEntities db = new SISTEM_VETERINARIAEntities())
            {
                return db.usp_Listar_CLIENTES()
                    .Select(x => new OpcionBE { Id = x.ID_cliente, Descripcion = x.NombreCompleto })
                    .ToList();
            }
        }

        public List<OpcionBE> ListarTiposCita()
        {
            using (SISTEM_VETERINARIAEntities db = new SISTEM_VETERINARIAEntities())
            {
                return db.usp_Listar_TIPOS_CITA()
                    .Select(x => new OpcionBE { Id = x.ID_tipo_cita, Descripcion = x.tipo_cita })
                    .ToList();
            }
        }

        public List<OpcionBE> ListarMascotasPorCliente(string idCliente)
        {
            if (string.IsNullOrWhiteSpace(idCliente))
                return new List<OpcionBE>();

            using (SISTEM_VETERINARIAEntities db = new SISTEM_VETERINARIAEntities())
            {
                return db.usp_Listar_MASCOTAS_POR_CLIENTE(idCliente)
                    .Select(x => new OpcionBE { Id = x.ID_mascota, Descripcion = x.nomMascota + " - " + x.tipo_mascota })
                    .ToList();
            }
        }

        public List<OpcionBE> ListarMedicamentos()
        {
            using (SISTEM_VETERINARIAEntities db = new SISTEM_VETERINARIAEntities())
            {
                return db.usp_Listar_MEDICAMENTOS()
                    .Where(x => x.Stock_medicamento > 0)
                    .Select(x => new OpcionBE
                    {
                        Id = x.Id_medicamento.ToString(),
                        Descripcion = x.nombre_medicamento + " (stock: " + x.Stock_medicamento + ")"
                    })
                    .ToList();
            }
        }

        public List<OpcionBE> ListarVacunas()
        {
            using (SISTEM_VETERINARIAEntities db = new SISTEM_VETERINARIAEntities())
            {
                return db.usp_Listar_VACUNAS()
                    .Where(x => x.Stock_vacunas > 0)
                    .Select(x => new OpcionBE
                    {
                        Id = x.ID_vacuna,
                        Descripcion = x.vacuna + " (stock: " + x.Stock_vacunas + ")"
                    })
                    .ToList();
            }
        }

        public void RegistrarCitaConTratamiento(RegistroCitaBE cita, TratamientoBE tratamiento)
        {
            using (SISTEM_VETERINARIAEntities db = new SISTEM_VETERINARIAEntities())
            using (var transaccion = db.Database.BeginTransaction())
            {
                try
                {
                    //tratamiento.ID_Tratamiento = GenerarIdUnico(
                    //    "TR",
                    //    id => db.TB_TRAT_CITA.Any(x => x.ID_trat_cita == id));

                    //cita.ID_Cita = GenerarIdUnico(
                    //    "CI",
                    //    id => db.TB_CITA.Any(x => x.ID_cita == id));
                    //cita.ID_Tratamiento = tratamiento.ID_Tratamiento;

                    
                    var idTratParameter = new ObjectParameter("ID_trat_cita", typeof(string));
                    db.usp_Insertar_TRAT_CITA(
                        tratamiento.ID_Medicamento,
                        tratamiento.Dosis,
                        tratamiento.Frecuencia,
                        string.IsNullOrWhiteSpace(tratamiento.ID_Vacuna) ? null : tratamiento.ID_Vacuna,
                        tratamiento.MiligramosVacuna,
                        idTratParameter);
                    cita.ID_Tratamiento = idTratParameter.Value?.ToString();

                    db.usp_Insertar_CITA(
                        
                        cita.ID_Empleado,
                        cita.ID_Veterinario,
                        cita.ID_Cliente,
                        cita.ID_TipoCita,
                        cita.ID_Mascota,
                        cita.ID_Tratamiento,
                        cita.Fecha,
                        cita.Hora,
                        cita.Motivo,
                        cita.Precio,
                        cita.Observaciones);

                    transaccion.Commit();
                   
                }
                catch (Exception ex)
                {
                    transaccion.Rollback();
                    throw new Exception("No se pudo registrar la cita: " + ex.Message, ex);
                }
            }
        }

        private static string GenerarIdUnico(string prefijo, Func<string, bool> existe)
        {
            string id;
            do
            {
                id = prefijo + Guid.NewGuid().ToString("N").Substring(0, 8).ToUpperInvariant();
            }
            while (existe(id));

            return id;
        }
    }
}
