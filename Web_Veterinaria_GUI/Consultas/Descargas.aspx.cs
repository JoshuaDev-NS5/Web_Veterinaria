using System;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Veterinaria.Helpers;

namespace Veterinaria.Consultas
{
    public partial class Desacargas : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarArchivos();
        }

        private void CargarArchivos()
        {
            try
            {
                DirectoryInfo directorio = new DirectoryInfo(ArchivoDescargaHelper.ObtenerDirectorio(Server));// Este método obtiene la ruta del directorio de descargas en el servidor web.
                var archivos = directorio.GetFiles()
                    .OrderByDescending(archivo => archivo.LastWriteTime)
                    .Select(archivo => new
                    {
                        Archivo = archivo.Name,
                        Tipo = archivo.Extension.TrimStart('.').ToUpperInvariant(),
                        Tamano = FormatearTamano(archivo.Length),
                        Fecha = archivo.LastWriteTime
                    })
                    .ToList();

                GridDescargas.DataSource = archivos;
                GridDescargas.DataBind();
                lblConteo.Text = archivos.Count.ToString();
                lblMensaje.Text = string.Empty;
            }
            catch (Exception ex)
            {
                lblConteo.Text = "0";
                lblMensaje.Text = "No se pudieron cargar las descargas: " + ex.Message;
            }
        }

        protected void GridDescargas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!string.Equals(e.CommandName, "Descargar", StringComparison.OrdinalIgnoreCase))
                return;

            try
            {
                int indice;
                if (!int.TryParse(Convert.ToString(e.CommandArgument), out indice) || indice < 0 || indice >= GridDescargas.Rows.Count)
                    throw new InvalidOperationException("No se pudo identificar el archivo seleccionado.");

                string nombre = Convert.ToString(GridDescargas.DataKeys[indice].Value);
                string directorio = ArchivoDescargaHelper.ObtenerDirectorio(Server);
                string ruta = Path.Combine(directorio, Path.GetFileName(nombre));

                ArchivoDescargaHelper.EnviarArchivo(this, ruta, ObtenerTipoContenido(Path.GetExtension(ruta)));
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "No se pudo descargar el archivo: " + ex.Message;
            }
        }

        protected void btnLimpiarDescargas_Click(object sender, EventArgs e)
        {
            try
            {
                string directorio = ArchivoDescargaHelper.ObtenerDirectorio(Server);
                int eliminados = 0;
                int noEliminados = 0;

                foreach (FileInfo archivo in new DirectoryInfo(directorio).GetFiles())
                {
                    try
                    {
                        archivo.Delete();
                        eliminados++;
                    }
                    catch (IOException)
                    {
                        noEliminados++;
                    }
                    catch (UnauthorizedAccessException)
                    {
                        noEliminados++;
                    }
                }

                CargarArchivos();
                lblMensaje.Text = noEliminados == 0
                    ? "Se eliminaron " + eliminados + " descarga(s)."
                    : "Se eliminaron " + eliminados + " descarga(s); " + noEliminados + " no se pudieron eliminar porque están en uso.";
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "No se pudieron limpiar las descargas: " + ex.Message;
            }
        }

        private static string FormatearTamano(long bytes)
        {
            if (bytes >= 1024 * 1024)
                return (bytes / (1024d * 1024d)).ToString("0.00") + " MB";
            if (bytes >= 1024)
                return (bytes / 1024d).ToString("0.00") + " KB";
            return bytes + " B";
        }

        private static string ObtenerTipoContenido(string extension)
        {
            switch ((extension ?? string.Empty).ToLowerInvariant())
            {
                case ".xlsx":
                    return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                case ".pdf":
                    return "application/pdf";
                case ".docx":
                    return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                case ".csv":
                    return "text/csv";
                default:
                    return "application/octet-stream";
            }
        }
    }
}
