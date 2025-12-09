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
using System.Globalization;

namespace ThermoWeb.CALIDAD
{
    public partial class ILUO_Formularios : System.Web.UI.Page
    {

        private static DataTable ds_Muro_Calidad = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["ID"] != null)
                {
                }
                    /*
                    CargaListasFiltro();
                    if (Request.QueryString["REFERENCIA"] != null)
                    {   
                         conexion = new Conexion_GP12();
                        //ds_Muro_Calidad = conexion.devuelve_detalle_revisiones_referencia(Convert.ToString(Request.QueryString["REFERENCIA"]));
                        //rellenar_grid();
                        selectReferencia.Value = Convert.ToString(Request.QueryString["REFERENCIA"]);
                        InputFechaDesde.Value = Convert.ToString(Request.QueryString["FECHA"]);
                        Rellenar_grid(null, null);
                    }
                */
                }
        }
       
       // carga la lista utilizando un filtro
       

    }

}