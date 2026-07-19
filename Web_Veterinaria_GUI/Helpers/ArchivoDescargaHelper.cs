using System;
using System.IO;
using System.Web;
using System.Web.UI;

namespace Veterinaria.Helpers
{
    public static class ArchivoDescargaHelper
    {
        public static string ObtenerDirectorio(HttpServerUtility servidor)
        {
            string directorio = servidor.MapPath("~/App_Data/Descargas");
            Directory.CreateDirectory(directorio);
            return directorio;
        }

        public static string GuardarArchivo(HttpServerUtility servidor, string nombreArchivo, byte[] contenido)
        {
            string nombreSeguro = Path.GetFileName(nombreArchivo);
            if (string.IsNullOrWhiteSpace(nombreSeguro))
                throw new ArgumentException("El nombre del archivo no es válido.", "nombreArchivo");

            string ruta = Path.Combine(ObtenerDirectorio(servidor), nombreSeguro);
            File.WriteAllBytes(ruta, contenido);
            return ruta;
        }

        public static void EnviarArchivo(Page pagina, string ruta, string tipoContenido)
        {
            FileInfo archivo = new FileInfo(ruta);
            if (!archivo.Exists)
                throw new FileNotFoundException("El archivo solicitado ya no existe.", ruta);

            PrepararDescargaEnNavegador(
                pagina,
                archivo.Name,
                File.ReadAllBytes(archivo.FullName),
                tipoContenido);
        }

        public static void GuardarYEnviar(Page pagina, string nombreArchivo, byte[] contenido, string tipoContenido)
        {
            string ruta = GuardarArchivo(pagina.Server, nombreArchivo, contenido);
            EnviarArchivo(pagina, ruta, tipoContenido);
        }

        private static void PrepararDescargaEnNavegador(Page pagina, string nombreArchivo, byte[] contenido, string tipoContenido)
        {
            string base64 = Convert.ToBase64String(contenido);
            string nombreSeguro = HttpUtility.JavaScriptStringEncode(Path.GetFileName(nombreArchivo));
            string tipoSeguro = HttpUtility.JavaScriptStringEncode(tipoContenido ?? "application/octet-stream");

            string script =
                "(function(){" +
                "var binario=window.atob('" + base64 + "');" +
                "var bytes=new Uint8Array(binario.length);" +
                "for(var i=0;i<binario.length;i++){bytes[i]=binario.charCodeAt(i);}" +
                "var blob=new Blob([bytes],{type:'" + tipoSeguro + "'});" +
                "var url=window.URL.createObjectURL(blob);" +
                "var enlace=document.createElement('a');" +
                "enlace.style.display='none';" +
                "enlace.href=url;" +
                "enlace.download='" + nombreSeguro + "';" +
                "document.body.appendChild(enlace);" +
                "enlace.click();" +
                "window.setTimeout(function(){document.body.removeChild(enlace);window.URL.revokeObjectURL(url);},1000);" +
                "})();";

            pagina.ClientScript.RegisterStartupScript(
                pagina.GetType(),
                "descarga_" + Guid.NewGuid().ToString("N"),
                script,
                true);
        }
    }
}
