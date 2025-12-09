using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Services;
using System.Net.Mail;
using System.Net;

namespace ThermoWeb.DOCUMENTAL
{
    public partial class DashboardVisitas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            //ClientScript.RegisterStartupScript(GetType(), "SetInterval", "setInterval(LanzaVideo(), 5000);", true);
            Conexion_SMARTH SHConexion = new Conexion_SMARTH();

            DataTable AuxVIDEO = SHConexion.Devuelve_Auxiliares_AudiovisualAPP("VISITAS", " AND Disponible = 1 ");
            try
            {
                Random r = new Random();
                int valorlinea = r.Next(0, AuxVIDEO.Rows.Count);
                IMGVisita.ImageUrl = AuxVIDEO.Rows[valorlinea]["URL"].ToString();
            }
            catch (Exception ex)
            {
                IMGVisita.ImageUrl = "../SMARTH_docs/AUDIOVISUAL/VISITALOGOTHERMO.png";
            }


        }



    }

}