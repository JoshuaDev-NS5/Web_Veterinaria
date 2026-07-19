using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Veterinaria.Helpers;
using Veterinaria_BE;
using Veterinaria_BL;

namespace Veterinaria
{
    public partial class ConsVeterinario : Page
    {
        private readonly VeterinarioBL veterinarioBL = new VeterinarioBL();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        private List<VeterinarioBE> ObtenerLista()
        {
            return veterinarioBL.Listar_Veterinarios_por_Filtros(ddlEspecialidad.SelectedValue,ddlEstado.SelectedValue);//Devolver la lista de veterinarios según los filtros seleccionados
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                lblMensaje.Text = "";
                List<VeterinarioBE> lista = ObtenerLista();//Obtener la lista de veterinarios según los filtros seleccionados

                gvVeterinarios.DataSource = lista;//Asignar la lista de veterinarios al GridView
                gvVeterinarios.DataBind();//Actualizar el GridView con la lista de veterinarios

                lblTotal.Text = lista.Count.ToString();//Mostrar el total de veterinarios encontrados
                lblActivos.Text = lista.Count(v => string.Equals(v.Estado_Veterinario, "Activo", StringComparison.OrdinalIgnoreCase)).ToString();//Mostrar el total de veterinarios activos
                lblEspecialidades.Text = lista
                    .Where(v => !string.IsNullOrWhiteSpace(v.especialidad))
                    .Select(v => v.especialidad)
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .Count()
                    .ToString();
                lblConteo.Text = lista.Count.ToString();

                if (lista.Count == 0)
                    lblMensaje.Text = "No se encontraron veterinarios.";
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al consultar veterinarios: " + ex.Message;
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            ddlEspecialidad.SelectedIndex = 0;
            ddlEstado.SelectedIndex = 0;
            lblTotal.Text = "0";
            lblActivos.Text = "0";
            lblEspecialidades.Text = "0";
            lblConteo.Text = "0";
            lblMensaje.Text = string.Empty;
            gvVeterinarios.DataSource = null;
            gvVeterinarios.DataBind();
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    List<VeterinarioBE> lista = ObtenerLista();

            //    StringBuilder sb = new StringBuilder();
            //    sb.AppendLine("ID\tNombre\tApellido\tEspecialidad\tTeléfono\tEmail\tDNI\tEstado");

            //    foreach (VeterinarioBE v in lista)
            //    {
            //        sb.AppendLine(string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}",
            //            v.ID_vet,
            //            v.nomVeterinario,
            //            v.apellidoVeterinario,
            //            v.especialidad,
            //            v.telefonoVeterinario,
            //            v.email,
            //            v.DNI_Veterinario,
            //            v.Estado_Veterinario));
            //    }

            //    Response.Clear();
            //    Response.Buffer = true;
            //    Response.AddHeader("content-disposition", "attachment;filename=Veterinarios.xls");
            //    Response.Charset = "UTF-8";
            //    Response.ContentEncoding = Encoding.UTF8;
            //    Response.ContentType = "application/vnd.ms-excel";
            //    Response.Write(sb.ToString());
            //    Response.Flush();
            //    Response.End();
            //}
            //catch (Exception ex)
            //{
            //    lblMensaje.Text = "Error al exportar: " + ex.Message;
            //}


            try
            {
                if (gvVeterinarios.Rows.Count == 0)
                {
                    //Agregar un pop cuando terminemos la logica de todo
                    lblMensaje.Text = "No hay Veterinarios registrados para descargar.";
                    return;
                }

                string rutaarchivo = Server.MapPath("~/Documentos/ListarVeterinarios.xlsx");

                ExcelPackage.License.SetNonCommercialPersonal("Renzo"); // Set the license context for EPPlus

                using (ExcelPackage paquete = new ExcelPackage(new FileInfo(rutaarchivo)))//Abrimos el archivo excel
                {
                    ExcelWorksheet hoja = paquete.Workbook.Worksheets[0]; //Seleccionamos la primera hoja del excel


                    int filaExcel = 5; //Los datos comienzan en la fila 5 del excel

                    //Recorremos el grid para llenar el excel
                    foreach (GridViewRow fila in gvVeterinarios.Rows)//Recorremos el datgrid
                    {
                        if (fila.RowType == DataControlRowType.DataRow)//Validamos que sea una fila de datos
                        {
                            for (int col = 0; col < fila.Cells.Count; col++)//Recorremos las columnas de la fila
                            {
                                hoja.Cells[filaExcel, col + 1].Value = HttpUtility.HtmlDecode(fila.Cells[col].Text);
                            }
                            filaExcel++;//Pasamos a la siguiente fila del excel
                        }
                    }

                    string nombreArchivo = "Listado_Veterinarios " + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                    ArchivoDescargaHelper.GuardarYEnviar(
                        this,
                        nombreArchivo,
                        paquete.GetAsByteArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al exportar: " + ex.Message;
            }
        }
    }
}
