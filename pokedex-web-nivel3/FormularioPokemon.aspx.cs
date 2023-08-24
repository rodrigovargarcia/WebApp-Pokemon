using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using dominio1;

namespace pokedex_web_nivel3
{
    public partial class FormularioPokemon : System.Web.UI.Page
    {
        public bool ConfirmaEliminacion { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            ConfirmaEliminacion = false;
            txtId.Enabled = false;
            try
            {

                //configuracion inicial de la pantalla.
                if (!IsPostBack)
                {
                    ElementoNegocio negocio = new ElementoNegocio();
                    List<Elemento> lista = negocio.listar();

                    ddlTipo.DataSource = lista;
                    ddlTipo.DataValueField = "id";
                    ddlTipo.DataTextField = "Descripcion";
                    ddlTipo.DataBind();

                    ddlDebilidad.DataSource = lista;
                    ddlDebilidad.DataValueField = "id";
                    ddlDebilidad.DataTextField = "Descripcion";
                    ddlDebilidad.DataBind();
                }

                //configuracion si estamos modificando.
                string id = Request.QueryString["id"] != null ? Request.QueryString["id"] : id = "";
                if(id != "" && !IsPostBack)
                {
                    PokemonNegocio negocio = new PokemonNegocio();
                    //List<Pokemon> lista = negocio.listar(Request.QueryString["id"].ToString());
                    //Pokemon seleccionado = lista[0];
                    Pokemon seleccionado = (negocio.listar(id))[0];
                    Session.Add("pokemonSeleccionado", seleccionado);

                    //precargar los datos
                    txtId.Text = id;
                    txtNumero.Text = seleccionado.Numero.ToString();
                    txtNombre.Text = seleccionado.Nombre;
                    txtDescripcion.Text = seleccionado.Descripcion;
                    txtImagenUrl.Text = seleccionado.UrlImagen;
                    ddlDebilidad.SelectedItem.Text = seleccionado.Debilidad.ToString();
                    ddlTipo.SelectedItem.Text = seleccionado.Tipo.ToString();
                    txtImagenUrl_TextChanged(sender, e);

                    //configurar acciones
                    if (!seleccionado.Activo)
                        btnDesactivar.Text = "Reactivar";
                    
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
                //Aquì podriamos agregar una redireccion a una pagina de error por ej.
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {                
                Pokemon nuevo = new Pokemon();
                PokemonNegocio negocio = new PokemonNegocio();
                nuevo.Numero = int.Parse(txtNumero.Text);
                nuevo.Nombre = txtNombre.Text;
                nuevo.Descripcion = txtDescripcion.Text;
                nuevo.UrlImagen = txtImagenUrl.Text;

                nuevo.Tipo = new Elemento();
                nuevo.Tipo.id = int.Parse(ddlTipo.SelectedValue);
                nuevo.Debilidad = new Elemento();
                nuevo.Debilidad.id = int.Parse(ddlDebilidad.SelectedValue);

                if (Request.QueryString["id"] != null)
                {
                    nuevo.Id = int.Parse(Request.QueryString["id"]);
                    negocio.modificarConSP(nuevo);
                }
                else
                    negocio.agregarConSP(nuevo);

                Response.Redirect("PokemonLista.aspx", false);                
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
            }
        }

        protected void txtImagenUrl_TextChanged(object sender, EventArgs e)
        {
            imgPokemon.ImageUrl = txtImagenUrl.Text;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            ConfirmaEliminacion = true;
        }

        protected void btnConfirmaEliminacion_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkConfirmaEliminacion.Checked)
                {
                    PokemonNegocio negocio = new PokemonNegocio();
                    negocio.eliminar(int.Parse(txtId.Text));

                    Response.Redirect("PokemonLista.aspx", false);
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
            }
        }

        protected void btnDesactivar_Click(object sender, EventArgs e)
        {
            try
            {
                PokemonNegocio negocio = new PokemonNegocio();
                Pokemon seleccionado = (Pokemon)Session["pokemonSeleccionado"];

                negocio.eliminarLogico(seleccionado.Id, !seleccionado.Activo);
                Response.Redirect("PokemonLista.aspx");
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
            }
        }
    }
}