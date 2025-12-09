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

namespace ThermoWeb.LIBERACIONES
{
    public partial class ConsultaNivelOperario : System.Web.UI.Page
    {

        private static DataSet ds_Liberaciones = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Conexion_LIBERACIONES conexion = new Conexion_LIBERACIONES();
                ds_Liberaciones = conexion.devuelve_operarios_NIVELI_trabajando();
                Rellenar_grid();
                FECHAACT.Text = DateTime.Now.ToString("dd/MM/yyy HH:mm");
            }

        }

        private void Rellenar_grid()
        {
            try
            {
                dgv_AreaRechazo.DataSource = ds_Liberaciones;
                dgv_AreaRechazo.DataBind();
            }
            catch (Exception)
            {
            }
        }
        // carga la lista utilizando un filtro       
    }
}

