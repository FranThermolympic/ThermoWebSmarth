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

namespace ThermoWeb.MATERIALES
{
    public partial class FOTOVOLTAICA : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            Conexion_MATERIALES conexion = new Conexion_MATERIALES();
            DataSet ds_Materiales = conexion.devuelve_lista_materiales("(I.[No_] LIKE '2%' or I.[No_] LIKE '3%' or I.[No_] LIKE '1%')");
            for (int i = 0; i <= ds_Materiales.Tables[0].Rows.Count - 1; i++)
            {
                DatalistNUMMaterial.InnerHtml = DatalistNUMMaterial.InnerHtml + System.Environment.NewLine +
                    String.Format("<option value='{0}'>", ds_Materiales.Tables[0].Rows[i][0]);
            }
            */
        }

    }
}