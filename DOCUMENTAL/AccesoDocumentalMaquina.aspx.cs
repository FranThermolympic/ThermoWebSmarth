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
    public partial class AccesoDocumentalMaquina : System.Web.UI.Page
    {

        private static DataTable DatosMolino = new DataTable();
        private static string[] ChartGeneral = new string[2];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                
            }
        }
 
        public void AbrirMaquinas(object sender, EventArgs e)
        {

            HtmlButton button = (HtmlButton)sender;
            string name = button.ID;
            switch (name)
            {
                case "FOAM":
                    DocVinculado.Src = "../DOCUMENTAL/DocumentosPlanta.aspx?MAQUINA=FOAM";
                    lblMaqPOP.InnerText = "Foamizado";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                    break;
                case "HOUSING":
                    DocVinculado.Src = "../DOCUMENTAL/DocumentosPlanta.aspx?MAQUINA=HOUS";
                    lblMaqPOP.InnerText = "Montaje Housing";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                    break;
                case "WTOP":
                    DocVinculado.Src = "../DOCUMENTAL/DocumentosPlanta.aspx?MAQUINA=WTOP";
                    lblMaqPOP.InnerText = "Montaje Worktop";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                    break;
                case "BSH":
                    DocVinculado.Src = "../DOCUMENTAL/DocumentosPlanta.aspx?MAQUINA=BSH";
                    lblMaqPOP.InnerText = "Montaje Cubetas BSH";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                    break;
                case "MAQ12":
                    DocVinculado.Src = "../DOCUMENTAL/DocumentosPlanta.aspx?MAQUINA=12";
                    lblMaqPOP.InnerText = "Máquina 12";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                    break;
                case "MAQ15":
                    DocVinculado.Src = "../DOCUMENTAL/DocumentosPlanta.aspx?MAQUINA=15";
                    lblMaqPOP.InnerText = "Máquina 15";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                    break;
                case "MAQ16":
                    DocVinculado.Src = "../DOCUMENTAL/DocumentosPlanta.aspx?MAQUINA=16";
                    lblMaqPOP.InnerText = "Máquina 16";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                    break;
                case "MAQ22":
                    DocVinculado.Src = "../DOCUMENTAL/DocumentosPlanta.aspx?MAQUINA=22";
                    lblMaqPOP.InnerText = "Máquina 22";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                    break;
                case "MAQ23":
                    DocVinculado.Src = "../DOCUMENTAL/DocumentosPlanta.aspx?MAQUINA=23";
                    lblMaqPOP.InnerText = "Máquina 23";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                    break;
                case "MAQ24":
                    DocVinculado.Src = "../DOCUMENTAL/DocumentosPlanta.aspx?MAQUINA=24";
                    lblMaqPOP.InnerText = "Máquina 24";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                    break;
                case "MAQ25":
                    DocVinculado.Src = "../DOCUMENTAL/DocumentosPlanta.aspx?MAQUINA=25";
                    lblMaqPOP.InnerText = "Máquina 25";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                    break;
                case "MAQ26":
                    DocVinculado.Src = "../DOCUMENTAL/DocumentosPlanta.aspx?MAQUINA=26";
                    lblMaqPOP.InnerText = "Máquina 26";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                    break;
                case "MAQ28":
                    DocVinculado.Src = "../DOCUMENTAL/DocumentosPlanta.aspx?MAQUINA=28";
                    lblMaqPOP.InnerText = "Máquina 28";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                    break;
                case "MAQ29":
                    DocVinculado.Src = "../DOCUMENTAL/DocumentosPlanta.aspx?MAQUINA=29";
                    lblMaqPOP.InnerText = "Máquina 29";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                    break;
                case "MAQ30":
                    DocVinculado.Src = "../DOCUMENTAL/DocumentosPlanta.aspx?MAQUINA=30";
                    lblMaqPOP.InnerText = "Máquina 30";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                    break;
                case "MAQ31":
                    DocVinculado.Src = "../DOCUMENTAL/DocumentosPlanta.aspx?MAQUINA=31";
                    lblMaqPOP.InnerText = "Máquina 31";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                    break;
                case "MAQ32":
                    DocVinculado.Src = "../DOCUMENTAL/DocumentosPlanta.aspx?MAQUINA=32";
                    lblMaqPOP.InnerText = "Máquina 32";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                    break;
                case "MAQ33":
                    DocVinculado.Src = "../DOCUMENTAL/DocumentosPlanta.aspx?MAQUINA=33";
                    lblMaqPOP.InnerText = "Máquina 33";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                    break;
                case "MAQ34":
                    DocVinculado.Src = "../DOCUMENTAL/DocumentosPlanta.aspx?MAQUINA=34";
                    lblMaqPOP.InnerText = "Máquina 34";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                    break;
                case "MAQ35":
                    DocVinculado.Src = "../DOCUMENTAL/DocumentosPlanta.aspx?MAQUINA=35";
                    lblMaqPOP.InnerText = "Máquina 35";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                    break;
                case "MAQ37":
                    DocVinculado.Src = "../DOCUMENTAL/DocumentosPlanta.aspx?MAQUINA=37";
                    lblMaqPOP.InnerText = "Máquina 37";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                    break;
                case "MAQ38":
                    DocVinculado.Src = "../DOCUMENTAL/DocumentosPlanta.aspx?MAQUINA=38";
                    lblMaqPOP.InnerText = "Máquina 38";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                    break;
                case "MAQ36":
                    DocVinculado.Src = "../DOCUMENTAL/DocumentosPlanta.aspx?MAQUINA=36";
                    lblMaqPOP.InnerText = "Máquina 36";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                    break;
                case "MAQ39":
                    DocVinculado.Src = "../DOCUMENTAL/DocumentosPlanta.aspx?MAQUINA=39";
                    lblMaqPOP.InnerText = "Máquina 39";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                    break;
                case "MAQ40":
                    DocVinculado.Src = "../DOCUMENTAL/DocumentosPlanta.aspx?MAQUINA=40";
                    lblMaqPOP.InnerText = "Máquina 40";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                    break;
                case "MAQ41":
                    DocVinculado.Src = "../DOCUMENTAL/DocumentosPlanta.aspx?MAQUINA=41";
                    lblMaqPOP.InnerText = "Máquina 41";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                    break;
                case "MAQ42":
                    DocVinculado.Src = "../DOCUMENTAL/DocumentosPlanta.aspx?MAQUINA=42";
                    lblMaqPOP.InnerText = "Máquina 42";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                    break;
                case "MAQ43":
                    DocVinculado.Src = "../DOCUMENTAL/DocumentosPlanta.aspx?MAQUINA=43";
                    lblMaqPOP.InnerText = "Máquina 43";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                    break;
                case "MAQ44":
                    DocVinculado.Src = "../DOCUMENTAL/DocumentosPlanta.aspx?MAQUINA=44";
                    lblMaqPOP.InnerText = "Máquina 44";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                    break;
                case "MAQ45":
                    DocVinculado.Src = "../DOCUMENTAL/DocumentosPlanta.aspx?MAQUINA=45";
                    lblMaqPOP.InnerText = "Máquina 45";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                    break;
                case "MAQ46":
                    DocVinculado.Src = "../DOCUMENTAL/DocumentosPlanta.aspx?MAQUINA=46";
                    lblMaqPOP.InnerText = "Máquina 46";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                    break;
                case "MAQ47":
                    DocVinculado.Src = "../DOCUMENTAL/DocumentosPlanta.aspx?MAQUINA=47";
                    lblMaqPOP.InnerText = "Máquina 47";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                    break;
                case "MAQ48":
                    DocVinculado.Src = "../DOCUMENTAL/DocumentosPlanta.aspx?MAQUINA=48";
                    lblMaqPOP.InnerText = "Máquina 48";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                    break;
                case "MAQ49":
                    DocVinculado.Src = "../DOCUMENTAL/DocumentosPlanta.aspx?MAQUINA=49";
                    lblMaqPOP.InnerText = "Máquina 49";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                    break;
                case "MAQ50":
                    DocVinculado.Src = "../DOCUMENTAL/DocumentosPlanta.aspx?MAQUINA=50";
                    lblMaqPOP.InnerText = "Máquina 50";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                    break;
            }
/*

            

            

            


            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1("+ button.ID.Substring(5) + ");", true);
*/
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "CargarCharts();", true);

            //genera grafica
            //abre popup
        }   

        
    }

}