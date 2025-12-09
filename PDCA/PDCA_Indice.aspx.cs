using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ThermoWeb.PDCA
{
    public partial class Indice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                Conexion_PDCA conexion = new Conexion_PDCA();
                BadgeGOP.InnerText = conexion.Devuelve_INTAbiertas("2").ToString();
                BadgeAuditorias.InnerText = conexion.Devuelve_INTAbiertas("6").ToString();
                BadgeProcesosnormativa.InnerText = conexion.Devuelve_INTAbiertas("3").ToString();

            }
        }
    }
}