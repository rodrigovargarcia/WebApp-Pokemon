using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;

namespace pokedex_web_nivel3
{
    public partial class EnvioEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEnviarEmail_Click(object sender, EventArgs e)
        {
            EmailService emailService = new EmailService();
            emailService.armarCorreo(txtNombreEmail.Text, txtAsuntoEmail.Text, txtMensajeEmail.Text);
            try
            {
                emailService.enviarEmail();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}