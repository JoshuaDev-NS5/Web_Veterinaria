using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Veterinaria_BE;
using Veterinaria_BL;

namespace Web_Veterinaria_GUI
{
    public partial class Cita : Page
    {
        private readonly CitaBL citaBL = new CitaBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    CargarCombos();
                    txtFecha.Text = DateTime.Today.ToString("yyyy-MM-dd");
                }
                catch (Exception ex)
                {
                    lblMensajeCita.Text = "No se pudieron cargar los datos del formulario: " + ex.Message;
                }
            }
        }

        private void CargarCombos()
        {
            CargarCombo(DL_Empleado, citaBL.ListarEmpleados(), "Seleccione un empleado");
            CargarCombo(DL_Veterinario, citaBL.ListarVeterinarios(), "Seleccione un veterinario");
            CargarCombo(DL_Cliente, citaBL.ListarClientes(), "Seleccione un cliente");
            CargarCombo(DL_TipoCita, citaBL.ListarTiposCita(), "Seleccione un tipo de cita");
            CargarCombo(DL_Medicamento, citaBL.ListarMedicamentos(), "Seleccione un medicamento");
            CargarCombo(DL_Vacuna, citaBL.ListarVacunas(), "Sin vacuna");
            CargarCombo(DL_Mascota, new List<OpcionBE>(), "Seleccione primero un cliente");
        }

        private static void CargarCombo(DropDownList combo, List<OpcionBE> opciones, string textoInicial)
        {
            combo.Items.Clear();
            combo.Items.Add(new ListItem(textoInicial, string.Empty));
            combo.AppendDataBoundItems = true;
            combo.DataSource = opciones;
            combo.DataTextField = "Descripcion";
            combo.DataValueField = "Id";
            combo.DataBind();
        }

        protected void DL_Cliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CargarCombo(
                    DL_Mascota,
                    citaBL.ListarMascotasPorCliente(DL_Cliente.SelectedValue),
                    string.IsNullOrWhiteSpace(DL_Cliente.SelectedValue)
                        ? "Seleccione primero un cliente"
                        : "Seleccione una mascota");
                lblMensajeCita.Text = string.Empty;
            }
            catch (Exception ex)
            {
                lblMensajeCita.Text = "No se pudieron cargar las mascotas: " + ex.Message;
            }
        }

        protected void btnGuardarTratamiento_Click(object sender, EventArgs e)
        {
            try
            {
                int idMedicamento;
                if (!int.TryParse(DL_Medicamento.SelectedValue, out idMedicamento))
                    throw new InvalidOperationException("Seleccione un medicamento.");
                if (string.IsNullOrWhiteSpace(txtDosis.Text))
                    throw new InvalidOperationException("Ingrese la dosis del tratamiento.");
                if (string.IsNullOrWhiteSpace(txtFrecuencia.Text))
                    throw new InvalidOperationException("Ingrese la frecuencia del tratamiento.");
                if (!string.IsNullOrWhiteSpace(DL_Vacuna.SelectedValue) && string.IsNullOrWhiteSpace(txtMiligramosVacuna.Text))
                    throw new InvalidOperationException("Ingrese la cantidad de la vacuna seleccionada.");

                ViewState["TratamientoMedicamento"] = idMedicamento;
                ViewState["TratamientoDosis"] = txtDosis.Text.Trim();
                ViewState["TratamientoFrecuencia"] = txtFrecuencia.Text.Trim();
                ViewState["TratamientoVacuna"] = DL_Vacuna.SelectedValue;
                ViewState["TratamientoMiligramos"] = txtMiligramosVacuna.Text.Trim();

                lblMensajeTratamiento.Text = string.Empty;
                lblMensajeCita.CssClass = "labelExito";
                lblMensajeCita.Text = "Tratamiento preparado. Ya puede guardar la cita.";
                mpeTratamiento.Hide();
            }
            catch (Exception ex)
            {
                lblMensajeTratamiento.Text = ex.Message;
                mpeTratamiento.Show();
            }
        }

        protected void btnGuardarCita_Click(object sender, EventArgs e)
        {
            try
            {
                ValidarSeleccion(DL_Empleado, "empleado");
                ValidarSeleccion(DL_Veterinario, "veterinario");
                ValidarSeleccion(DL_Cliente, "cliente");
                ValidarSeleccion(DL_TipoCita, "tipo de cita");
                ValidarSeleccion(DL_Mascota, "mascota");

                DateTime fecha;
                if (!DateTime.TryParseExact(txtFecha.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out fecha))
                    throw new InvalidOperationException("Ingrese una fecha válida.");
                if (fecha.Date < DateTime.Today)
                    throw new InvalidOperationException("La fecha de la cita no puede estar en el pasado.");

                TimeSpan hora;
                if (!TimeSpan.TryParse(txtHora.Text, CultureInfo.InvariantCulture, out hora))
                    throw new InvalidOperationException("Ingrese una hora válida.");

                decimal precio;
                if (!decimal.TryParse(txtPrecio.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out precio) &&
                    !decimal.TryParse(txtPrecio.Text, NumberStyles.Number, CultureInfo.CurrentCulture, out precio))
                    throw new InvalidOperationException("Ingrese un precio válido.");
                if (precio < 0)
                    throw new InvalidOperationException("El precio no puede ser negativo.");
                if (string.IsNullOrWhiteSpace(txtMotivo.Text))
                    throw new InvalidOperationException("Ingrese el motivo de la cita.");

                TratamientoBE tratamiento = ObtenerTratamientoPreparado();
                if (tratamiento == null)
                    throw new InvalidOperationException("Prepare el tratamiento antes de guardar la cita.");

                RegistroCitaBE cita = new RegistroCitaBE
                {
                    ID_Empleado = DL_Empleado.SelectedValue,
                    ID_Veterinario = DL_Veterinario.SelectedValue,
                    ID_Cliente = DL_Cliente.SelectedValue,
                    ID_TipoCita = DL_TipoCita.SelectedValue,
                    ID_Mascota = DL_Mascota.SelectedValue,
                    Fecha = fecha.Date,
                    Hora = fecha.Date.Add(hora),
                    Precio = precio,
                    Motivo = txtMotivo.Text.Trim(),
                    Observaciones = txtObservaciones.Text.Trim()
                };

                
                LimpiarFormulario();
                citaBL.RegistrarCita(cita, tratamiento);
                lblMensajeCita.CssClass = "labelExito";
                lblMensajeCita.Text = "Cita registrada correctamente.";
                mpeExito.Show();
            }
            catch (Exception ex)
            {
                lblMensajeCita.CssClass = "labelErrores";
                lblMensajeCita.Text = ex.Message;
            }
        }

        private TratamientoBE ObtenerTratamientoPreparado()
        {
            if (ViewState["TratamientoMedicamento"] == null)
                return null;

            return new TratamientoBE
            {
                ID_Medicamento = Convert.ToInt32(ViewState["TratamientoMedicamento"]),
                Dosis = Convert.ToString(ViewState["TratamientoDosis"]),
                Frecuencia = Convert.ToString(ViewState["TratamientoFrecuencia"]),
                ID_Vacuna = Convert.ToString(ViewState["TratamientoVacuna"]),
                MiligramosVacuna = Convert.ToString(ViewState["TratamientoMiligramos"])
            };
        }

        private static void ValidarSeleccion(DropDownList combo, string campo)
        {
            if (string.IsNullOrWhiteSpace(combo.SelectedValue))
                throw new InvalidOperationException("Seleccione un " + campo + ".");
        }

        private void LimpiarFormulario()
        {
            DL_Empleado.SelectedIndex = 0;
            DL_Veterinario.SelectedIndex = 0;
            DL_Cliente.SelectedIndex = 0;
            DL_TipoCita.SelectedIndex = 0;
            CargarCombo(DL_Mascota, new List<OpcionBE>(), "Seleccione primero un cliente");
            DL_Medicamento.SelectedIndex = 0;
            DL_Vacuna.SelectedIndex = 0;
            txtFecha.Text = DateTime.Today.ToString("yyyy-MM-dd");
            txtHora.Text = string.Empty;
            txtPrecio.Text = string.Empty;
            txtMotivo.Text = string.Empty;
            txtObservaciones.Text = string.Empty;
            txtDosis.Text = string.Empty;
            txtFrecuencia.Text = string.Empty;
            txtMiligramosVacuna.Text = string.Empty;
            ViewState.Remove("TratamientoMedicamento");
            ViewState.Remove("TratamientoDosis");
            ViewState.Remove("TratamientoFrecuencia");
            ViewState.Remove("TratamientoVacuna");
            ViewState.Remove("TratamientoMiligramos");
        }
    }
}
