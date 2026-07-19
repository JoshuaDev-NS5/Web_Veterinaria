using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using Veterinaria_BE;
using Veterinaria_BL;

namespace Veterinaria
{
    public partial class WebForm1 : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Protege todas las paginas que usen este master page contra el boton
            //Navegar hacia atras del naveador

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                String LoginUsuario = txtUsuario.Text.Trim();
                String PasswordUsuario = txtClave.Text.Trim();
                UsuarioBL objUsuraioBL = new UsuarioBL();
                UsuarioBE objUsuarioBE= objUsuraioBL.ConsultarUsuario(LoginUsuario,PasswordUsuario);

                // Validación con acceso a BD
                if (objUsuarioBE.Login_Usuario != null)
                {
                    //Crea la cookie de autneticacion y lo redirige a la pagina que intentaba ver
                    FormsAuthentication.RedirectFromLoginPage(LoginUsuario, false);

                    Session["Usuario"] = objUsuarioBE.Login_Usuario;
                    Session["Nivel"] = objUsuarioBE.Niv_Usuario;
                    Response.Redirect("~/Inicio.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }
                else
                {
                    throw new Exception("Usuario o contraseña incorrectos");
                }
            }
            catch (Exception ex) {

                lblMensaje.Text = ex.Message;
                txtClave.Text = string.Empty;
            }


            
        }
    }
}

