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

namespace ThermoWeb.RRHH
{
    public partial class RRHH_Dashboard : System.Web.UI.Page
    {

        private static DataTable dt_NAV = new DataTable();
        private static DataTable ds_firmas = new DataTable();
        private static DataTable ds_ListaEntregas = new DataTable();
        private static DataTable ds_esfirmado = new DataTable();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Conexion_SMARTH SHconexion = new Conexion_SMARTH();
                DataTable ListaOperarios = SHconexion.Devuelve_dias_sin_accidente();
                lblSinNumAccidentes.InnerText = ListaOperarios.Rows[0]["DIASINACC"].ToString();


            }
        }

        public void GuardarFecha(object sender, EventArgs e)
        {
            Conexion_SMARTH SHconexion = new Conexion_SMARTH();
            SHconexion.Actualizar_Fecha_Accidente(InputFechaAlta.Value);
            InputFechaAlta.Value = "";
            DataTable ListaOperarios = SHconexion.Devuelve_dias_sin_accidente();
            lblSinNumAccidentes.InnerText = ListaOperarios.Rows[0]["DIASINACC"].ToString();

        }
    }

}