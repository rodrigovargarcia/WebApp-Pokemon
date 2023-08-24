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
    public partial class PokemonLista : System.Web.UI.Page
    {
        public bool FiltroAvanzado { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Seguridad.esAdmin(Session["trainee"])))
            {
                Session.Add("error", "Se requiere permisos de admin para acceder a esta pantalla");
                Response.Redirect("/Error.aspx");
            }

            FiltroAvanzado = false;
            PokemonNegocio negocio = new PokemonNegocio();
            Session.Add("listaPokemons", negocio.listarConSP());
            dgvPokemons.DataSource = Session["listaPokemons"];
            dgvPokemons.DataBind();
        }

        protected void dgvPokemons_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvPokemons.PageIndex = e.NewPageIndex;
            dgvPokemons.DataBind();
        }

        protected void dgvPokemons_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvPokemons.SelectedDataKey.Value.ToString();
            Response.Redirect("FormularioPokemon.aspx?id=" + id);
        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Pokemon> lista = (List<Pokemon>)Session["listaPokemons"];
            List<Pokemon> listaFiltrada = lista.FindAll(x => x.Nombre.ToUpper().Contains(txtFiltro.Text.ToUpper()));
            dgvPokemons.DataSource = listaFiltrada;
            dgvPokemons.DataBind();
        }

        protected void chkFiltroAvanzado_CheckedChanged(object sender, EventArgs e)
        {
            FiltroAvanzado = chkFiltroAvanzado.Checked;
            txtFiltro.Enabled = !FiltroAvanzado;
        }

        protected void ddlCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCriterio.Items.Clear();
            if (ddlCampo.SelectedItem.ToString() == "Número")
            {
                ddlCriterio.Items.Add("Mayor a");
                ddlCriterio.Items.Add("Menor a");
                ddlCriterio.Items.Add("Igual a");
            }
            else
            {
                ddlCriterio.Items.Add("Contiene");
                ddlCriterio.Items.Add("Comienza con");
                ddlCriterio.Items.Add("Termina con");
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                PokemonNegocio negocio = new PokemonNegocio();
                dgvPokemons.DataSource = negocio.filtrar(ddlCampo.SelectedItem.ToString(), ddlCriterio.Text, txtFiltro.Text, ddlEstado.Text);

            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);
            }
        }
    }
}