using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Veterinaria
{
    public partial class Veterinaria : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            //Protege todas las paginas que usen este master page contra el boton
            //Navegar hacia atras del naveador

            Response.Cache.SetCacheability(HttpCacheability.NoCache);//Evitan que la pagina se almacene en la cache del navegador
            Response.Cache.SetNoStore();
            Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            Response.AppendHeader("Pragma", "no-cache");

            
            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/Login.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            lblUsuario.Text = "Bienvenido " + Session["Usuario"].ToString();
        }
    }
}
