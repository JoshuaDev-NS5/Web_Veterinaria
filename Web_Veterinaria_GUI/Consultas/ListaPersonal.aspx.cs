using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Veterinaria.Helpers;
using Veterinaria_BE;
using Veterinaria_BL;

namespace Veterinaria.Consultas
{
    public partial class ListaPersonal : Page
    {
        private readonly PersonalBL personalBL = new PersonalBL();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnListarPersonal_Click(object sender, EventArgs e)
        {
            try
            {
                MostrarPersonal(personalBL.ListarPersonal());
            }
            catch (Exception ex)
            {
                LimpiarResultados();
                lblMensaje.Text = "Error al consultar el personal: " + ex.Message;
            }
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                List<PersonalBE> lista = personalBL.ListarPersonal();
                if (lista.Count == 0)
                {
                    lblMensaje.Text = "No hay registros de personal para exportar.";
                    return;
                }

                MostrarPersonal(lista);
                ExcelPackage.License.SetNonCommercialPersonal("INUPETS");

                using (ExcelPackage paquete = new ExcelPackage())
                {
                    ExcelWorksheet hoja = paquete.Workbook.Worksheets.Add("Personal");
                    string[] encabezados = { "ID", "Nombre completo", "DNI", "Teléfono", "Email", "Cargo", "Hora de entrada", "Hora de salida" };

                    for (int columna = 0; columna < encabezados.Length; columna++)
                        hoja.Cells[1, columna + 1].Value = encabezados[columna];

                    using (ExcelRange encabezado = hoja.Cells[1, 1, 1, encabezados.Length])
                    {
                        encabezado.Style.Font.Bold = true;
                        encabezado.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        encabezado.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(4, 114, 72));
                        encabezado.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    }

                    for (int fila = 0; fila < lista.Count; fila++)
                    {
                        PersonalBE personal = lista[fila];
                        hoja.Cells[fila + 2, 1].Value = personal.ID_Empleado;
                        hoja.Cells[fila + 2, 2].Value = personal.NombreCompleto;
                        hoja.Cells[fila + 2, 3].Value = personal.DNI;
                        hoja.Cells[fila + 2, 4].Value = personal.Telefono;
                        hoja.Cells[fila + 2, 5].Value = personal.Email;
                        hoja.Cells[fila + 2, 6].Value = personal.Cargo;
                        hoja.Cells[fila + 2, 7].Value = personal.HoraEntrada;
                        hoja.Cells[fila + 2, 8].Value = personal.HoraSalida;
                    }

                    hoja.Cells[hoja.Dimension.Address].AutoFitColumns();
                    string nombre = "Lista_Personal_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                    ArchivoDescargaHelper.GuardarYEnviar(
                        this,
                        nombre,
                        paquete.GetAsByteArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al exportar el personal: " + ex.Message;
            }
        }

        private void MostrarPersonal(List<PersonalBE> lista)
        {
            GridListarPersonal.DataSource = lista;
            GridListarPersonal.DataBind();
            int manana = lista.Count(p => p.EsTurnoManana);
            lblTotal.Text = lista.Count.ToString();
            lblTurnoManana.Text = manana.ToString();
            lblTurnoTarde.Text = (lista.Count - manana).ToString();
            lblConteo.Text = lista.Count.ToString();
            lblMensaje.Text = lista.Count == 0 ? "No se encontraron registros de personal." : string.Empty;
        }

        private void LimpiarResultados()
        {
            GridListarPersonal.DataSource = null;
            GridListarPersonal.DataBind();
            lblTotal.Text = "0";
            lblTurnoManana.Text = "0";
            lblTurnoTarde.Text = "0";
            lblConteo.Text = "0";
        }
    }
}
