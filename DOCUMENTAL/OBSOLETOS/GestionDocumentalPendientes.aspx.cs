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
    public partial class GestionDocumentalPendientes : System.Web.UI.Page
    {

        private static DataTable ds_Documentos = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Conexion_DOCUMENTAL conexion = new Conexion_DOCUMENTAL();
                ds_Documentos = conexion.Devuelve_Produciendo_Pendientes_Documentos();
                rellenar_grid();
            }

        }

        private void rellenar_grid()
        {
            try
            {
                dgv_PendientesProduciendo.DataSource = ds_Documentos;
                dgv_PendientesProduciendo.DataBind(); 
            }
            catch (Exception)
            { 
                
            }
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