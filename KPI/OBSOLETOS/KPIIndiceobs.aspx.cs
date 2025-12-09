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

namespace ThermoWeb.KPI
{
    public partial class KPIIndice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }

        }

        public void ImportardeBMS(object sender, EventArgs e)
        {

            try
            {
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                SHConexion.Leer_productosBMS();
                SHConexion.Leer_operariosNAV();
                SHConexion.InsertaProductosReferenciaEstados();
                SHConexion.InsertaProductosTablaDocumentos();
                SHConexion.Leer_actualizar_ubicaciones_moldes();
                SHConexion.Leer_moldes();


            }
            catch (Exception)
            {

            }


        }

    }

}