using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio1;
using negocio;

namespace pokedex_web_nivel3
{
    public partial class MiPerfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Trainee user = (Trainee)Session["trainee"];
            if (!IsPostBack)
            {
                if (Seguridad.sesionActiva(Session["trainee"]))
                {
                    if (user.Nombre != null)
                        txtNombre.Text = user.Nombre.ToString();
                    if(user.Apellido != null)
                        txtApellido.Text = user.Apellido.ToString();
                    if(user.ImagenPerfil != null)
                        imgNuevoPerfil.ImageUrl = "~/Images/" + user.ImagenPerfil;
                    if(user.FechaNacimiento != null)
                        txtFechaNacimiento.Text = user.FechaNacimiento.ToString("yyyy-MM-dd");
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {              
                //Escribir IMG

                TraineeNegocio negocio = new TraineeNegocio();
                string ruta = Server.MapPath("./Images/");
                Trainee user = (Trainee)Session["Trainee"];
                txtImagen.PostedFile.SaveAs(ruta + "perfil-" + user.Id + ".jpg");

                user.ImagenPerfil = "perfil-" + user.Id + ".jpg";
                user.Nombre = txtNombre.Text;
                user.Apellido = txtApellido.Text;
                user.FechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text);

                negocio.Actualizar(user);

                //Leer IMG

                Image img = (Image)Master.FindControl("imgAvatar");
                img.ImageUrl = "~/Images/" + user.ImagenPerfil;
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("/Error.aspx");
            }
        }
    }
}