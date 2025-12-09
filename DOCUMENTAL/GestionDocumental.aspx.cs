using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Web.Script.Services;

namespace ThermoWeb.DOCUMENTAL
{
    public partial class GestionDocumental : System.Web.UI.Page
    {

        private static DataSet ds_Documentos = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Conexion_DOCUMENTAL conexion = new Conexion_DOCUMENTAL();
                ds_Documentos = conexion.devuelve_dataset_referenciasYN();
                rellenar_grid();
            }

        }

        private void rellenar_grid()
        {
            try
            {
                dgv_AreaRechazo.DataSource = ds_Documentos;
                dgv_AreaRechazo.DataBind(); 
            }
            catch (Exception)
            { 
                
            }
        }

        protected void cargar_todas(object sender, EventArgs e)
            {

            Conexion_DOCUMENTAL conexion = new Conexion_DOCUMENTAL();
            ds_Documentos = conexion.devuelve_dataset_referenciasYN();
            dgv_AreaRechazo.DataSource = ds_Documentos;
            dgv_AreaRechazo.DataBind();
            }

        protected void cargar_Filtrados(object sender, EventArgs e)
            {
            Conexion_DOCUMENTAL conexion = new Conexion_DOCUMENTAL();
            ds_Documentos = conexion.devuelve_dataset_filtroreferenciasYN(selectReferencia.Text, selectMolde.Text);
            dgv_AreaRechazo.DataSource = ds_Documentos;
            dgv_AreaRechazo.DataBind();
            VerTodas.Visible = true;
            }

        public void ContactsGridView_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Redirect")
            {
                Response.Redirect("FichaReferencia.aspx?REFERENCIA=" + e.CommandArgument.ToString());
            }
        }
    }

}