using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Veterinaria_BE;
using Veterinaria_BL;
using OfficeOpenXml;
using System.IO;
using Veterinaria.Helpers;


namespace Veterinaria.Consultas
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        MascotaBL objMascotasBL = new MascotaBL();
        CitaBL objCitaBL = new CitaBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                lblMensaje.Text = "";
                string idMascota = txtIdMascota.Text.Trim();
                if (string.IsNullOrEmpty(idMascota))
                {
                    lblMensaje.Text = "Por favor, ingrese el ID de la mascota.";
                    LimpiarFicha();
                    grvHistorialMascotas.DataSource = null;
                    grvHistorialMascotas.DataBind();
                    NumRegistros.Text = "0";
                    return;
                }
                MascotasBE mascota = objMascotasBL.Datos_de_Mascota(idMascota);
                if (mascota == null)
                {
                    lblMensaje.Text = "No se encontró ninguna mascota con el ID: " + idMascota;
                    LimpiarFicha();
                    grvHistorialMascotas.DataSource = null;
                    grvHistorialMascotas.DataBind();
                    NumRegistros.Text = "0";
                    return;
                }

                Mostrar_Ficha(mascota);

                List<CitaBE> historial = objCitaBL.Citas_Por_mascotas(idMascota);
                grvHistorialMascotas.DataSource= historial;
                grvHistorialMascotas.DataBind();//Se actualizan los datos en el html
                NumRegistros.Text=historial.Count.ToString();

            } catch (Exception ex)
            {
                lblMensaje.Text = "Error al consultar la mascota: " + ex.Message; 

            }
        }

        public void Mostrar_Ficha(MascotasBE mascota)
        {
            HM_Dato_Nombre.Text = mascota.nomMascota;
            lblIdMascota.Text = mascota.ID_mascota;
            HM_Dato_Cliente.Text = mascota.Cliente;
            HM_Dato_Telefono.Text = mascota.telefono;
            HM_Dato_TipoMascota.Text = mascota.tipo_mascota;
            HM_Dato_Raza.Text = mascota.raza;
            HM_Dato_Edad.Text = Convert.ToString(mascota.edad);
            HM_Dato_Peso.Text = Convert.ToString(mascota.peso);
            HM_Dato_Genero.Text = mascota.genero;

            if (mascota.Foto != null && mascota.Foto.Length > 0)
            {
                Image1.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(mascota.Foto);
            }
            else
            {
                Image1.ImageUrl = "";
            }
        }

        private void LimpiarFicha()
        {
            HM_Dato_Nombre.Text = "-";
            lblIdMascota.Text = "-";
            HM_Dato_Cliente.Text = "-";
            HM_Dato_Telefono.Text = "-";
            HM_Dato_TipoMascota.Text = "-";
            HM_Dato_Raza.Text = "-";
            HM_Dato_Edad.Text = "-";
            HM_Dato_Peso.Text = "-";
            HM_Dato_Genero.Text = "-";
            Image1.ImageUrl = "";
        }

        protected void txtIdMascota_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            try
            {


                lblMensaje.Text = "";
                string idMascota = txtIdMascota.Text.Trim();
                if (string.IsNullOrEmpty(idMascota))
                {
                    lblMensaje.Text = "Por favor, ingrese el ID de la mascota.";
                    LimpiarFicha();
                    grvHistorialMascotas.DataSource = null;
                    grvHistorialMascotas.DataBind();
                    NumRegistros.Text = "0";
                    return;
                }
                MascotasBE mascota = objMascotasBL.Datos_de_Mascota(idMascota);
                if (mascota == null)
                {
                    lblMensaje.Text = "No se encontró ninguna mascota con el ID: " + idMascota;
                    LimpiarFicha();
                    grvHistorialMascotas.DataSource = null;
                    grvHistorialMascotas.DataBind();
                    NumRegistros.Text = "0";
                    return;
                }

                Mostrar_Ficha(mascota);



                if (string.IsNullOrEmpty(txtFechaInicio.Text) || string.IsNullOrEmpty(txtFechaFinal.Text))
                {
                    lblMensaje.Text = "Por favor, llenar las fechas.";
                    return; 
                }

                lblMensaje.Text = "";

                
                DateTime FecInicio = Convert.ToDateTime(txtFechaInicio.Text);
                DateTime FecFinal = Convert.ToDateTime(txtFechaFinal.Text);
                List<CitaBE> Citas_Por_Fecha = objCitaBL.Citas_Por_Fechas(idMascota, FecInicio, FecFinal);
                grvHistorialMascotas.DataSource = Citas_Por_Fecha;
                grvHistorialMascotas.DataBind();
                NumRegistros.Text = Citas_Por_Fecha.Count.ToString();


            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al filtrar datos por fecha: " + ex.Message;
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFicha();
            grvHistorialMascotas.DataSource = null;
            grvHistorialMascotas.DataBind();
            txtIdMascota.Text = "";
            txtFechaInicio.Text = "";
            txtFechaFinal.Text = "";
            NumRegistros.Text = "0";
            lblMensaje.Text = "";

        }

        protected void btnDescargar_Citas_Click(object sender, EventArgs e)
        {
            try
            {
                //Validamos que halla datos en grid
                if (grvHistorialMascotas.Rows.Count == 0) {
                    //Agregar un pop cuando terminemos la logica de todo
                    lblMensaje.Text = "No hay citas registradas para descargar.";
                    return;
                }

                string rutaarchivo = Server.MapPath("~/Documentos/ListarCitasDeMascotas.xlsx");

                ExcelPackage.License.SetNonCommercialPersonal("Anthony"); // Set the license context for EPPlus

                using (ExcelPackage paquete = new ExcelPackage(new FileInfo(rutaarchivo)))
                {
                    ExcelWorksheet hoja = paquete.Workbook.Worksheets[0]; //Seleccionamos la primera hoja del excel

                    //Encabezado del reporte
                    hoja.Cells["D2"].Value= HM_Dato_Nombre.Text;
                    hoja.Cells["F2"].Value= (!string.IsNullOrEmpty(txtFechaInicio.Text) && !string.IsNullOrEmpty(txtFechaFinal.Text))? txtFechaInicio.Text + " - " + txtFechaFinal.Text: "Todas";

                    int filaExcel = 5; //Los datos comienzan en la fila 5 del excel

                    //Recorremos el grid para llenar el excel
                    foreach(GridViewRow fila in grvHistorialMascotas.Rows)//Recorremos el datgrid
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

                    string nombreArchivo = "Historial_Citas_" + txtIdMascota.Text.Trim() + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                    ArchivoDescargaHelper.GuardarYEnviar(
                        this,
                        nombreArchivo,
                        paquete.GetAsByteArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                }

            }
            catch (Exception ex)    
            {
                lblMensaje.Text = "Error al descargar el archivo: " + ex.Message;
            }
        }
    }
}
