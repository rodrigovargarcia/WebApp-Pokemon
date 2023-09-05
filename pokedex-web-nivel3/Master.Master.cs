using dominio1;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.WebRequestMethods;

namespace pokedex_web_nivel3
{
    public partial class Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            imgAvatar.ImageUrl = "https://us.123rf.com/450wm/thesomeday123/thesomeday1231709/thesomeday123170900021/85622928-icono-de-perfil-de-avatar-predeterminado-marcador-de-posici%C3%B3n-de-foto-gris-vectores-de.jpg";
            if (Seguridad.sesionActiva(Session["trainee"]))
                imgAvatar.ImageUrl = "~/Images/" + ((Trainee)Session["trainee"]).ImagenPerfil;
            if(!(Page is Login || Page is Default || Page is Registro || Page is Error))
            {
                if (!Seguridad.sesionActiva(Session["trainee"]))
                    Response.Redirect("Login.aspx", false);
                else 
                    {
                        Trainee user = (Trainee)Session["trainee"];
                        lblUser.Text = user.Email;
                        if(!string.IsNullOrEmpty(user.ImagenPerfil))
                            imgAvatar.ImageUrl = "~/Images/" + user.ImagenPerfil;
                    }
            }
            
        }

        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Registro.aspx");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Login.aspx");
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("/Default.aspx");
        }
    }
}