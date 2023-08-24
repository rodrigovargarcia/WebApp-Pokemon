using dominio1;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace pokedex_web_nivel3
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            Trainee trainee = new Trainee();
            TraineeNegocio negocio = new TraineeNegocio();
            try
            {
                trainee.Email = txtEmail.Text;
                trainee.Pass = txtPassword.Text;
                if (negocio.Login(trainee))
                {
                    Session.Add("trainee", trainee);
                    Response.Redirect("/MiPerfil.aspx", false);
                }
                else
                {
                    Session.Add("error", "user o pass incorrectos");
                    Response.Redirect("/Error.aspx");
                }

            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("/Error.aspx");
            }
        }
    }
}