using System;
using System.Collections.Generic;
using Veterinaria_ADO;
using Veterinaria_BE;

namespace Veterinaria_BL
{
    public class CitaBL
    {
        private readonly CitaADO citaADO = new CitaADO();

        public List<CitaBE> Citas_Por_mascotas(string idMascota)
        {
            return citaADO.Citas_por_Mascota(idMascota);
        }

        public List<CitaBE> Citas_Por_Fechas(string idMascota, DateTime fechaInicio, DateTime fechaFin)
        {
            if (fechaInicio.Date > fechaFin.Date)
                throw new ArgumentException("La fecha inicial no puede ser posterior a la fecha final.");

            return citaADO.Citas_Filtrado_Por_Fecha(idMascota, fechaInicio, fechaFin);
        }

        public List<OpcionBE> ListarEmpleados() { return citaADO.ListarEmpleados(); }
        public List<OpcionBE> ListarVeterinarios() { return citaADO.ListarVeterinarios(); }
        public List<OpcionBE> ListarClientes() { return citaADO.ListarClientes(); }
        public List<OpcionBE> ListarTiposCita() { return citaADO.ListarTiposCita(); }
        public List<OpcionBE> ListarMascotasPorCliente(string idCliente) { return citaADO.ListarMascotasPorCliente(idCliente); }
        public List<OpcionBE> ListarMedicamentos() { return citaADO.ListarMedicamentos(); }
        public List<OpcionBE> ListarVacunas() { return citaADO.ListarVacunas(); }

        public void RegistrarCita(RegistroCitaBE cita, TratamientoBE tratamiento)
        {
            if (cita == null)
                throw new ArgumentNullException("cita");
            if (tratamiento == null)
                throw new ArgumentNullException("tratamiento");

            citaADO.RegistrarCitaConTratamiento(cita, tratamiento);
        }
    }
}
