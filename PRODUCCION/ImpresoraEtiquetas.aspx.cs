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

namespace ThermoWeb.PRODUCCION
{
    public partial class ImpresoraEtiquetas : System.Web.UI.Page
    {
        private static DataSet ds_KPI_Resultados_Mantenimiento_MAQ = new DataSet();
        private static DataSet ds_KPI_Totales_Mantenimiento_MAQ = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {           
            if (!IsPostBack)
            {

                Selecaño.SelectedValue = Convert.ToString(DateTime.Now.Year);
                SelecMes.SelectedValue = Convert.ToString(DateTime.Now.Month);
                SELECMES2.SelectedValue = Convert.ToString(DateTime.Now.Month);

                string año = Convert.ToString(Selecaño.SelectedValue);
                
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                DataTable TESTING = SHConexion.TESTARAVEN("");

                ds_KPI_Resultados_Mantenimiento_MAQ = SHConexion.Devuelve_Resultados_Mantenimiento_Maquinas(Convert.ToInt32(año));
                
                ds_KPI_Totales_Mantenimiento_MAQ = SHConexion.Devuelve_TOTAL_Resultados_Mantenimiento_Maquinas(Convert.ToInt32(año));

                rellenar_grid();
            }

        }
       
        private void rellenar_grid()
        {
            try
            {
                //MAQUINAS
                GridResultadosMaq.DataSource = ds_KPI_Resultados_Mantenimiento_MAQ;
                GridResultadosMaq.DataBind();

                GridDetallesMaqCORR.DataSource = ds_KPI_Resultados_Mantenimiento_MAQ;
                GridDetallesMaqCORR.DataBind();

                GridDetallesMaqPREV.DataSource = ds_KPI_Resultados_Mantenimiento_MAQ;
                GridDetallesMaqPREV.DataBind();

  

                //MOLDES
                

                //TOTALES
                KPICosteTotalMAQ.InnerText = ds_KPI_Totales_Mantenimiento_MAQ.Tables[0].Rows[0]["COSTETOTALES"].ToString();
                KPIHorasMAQCORR.InnerText = ds_KPI_Totales_Mantenimiento_MAQ.Tables[0].Rows[0]["REALESMAQ"].ToString();
                KPIHorasMAQPREV.InnerText = ds_KPI_Totales_Mantenimiento_MAQ.Tables[0].Rows[0]["REALESPREV"].ToString();
                KPIPartesMAQ.InnerText = ds_KPI_Totales_Mantenimiento_MAQ.Tables[0].Rows[0]["PARTES"].ToString();

             
            }
            catch (Exception ex)
            {
                KPICosteTotalMAQ.InnerText = "-";
                KPIHorasMAQCORR.InnerText = "-";
                KPIHorasMAQPREV.InnerText = "-";
                KPIPartesMAQ.InnerText = "-";
                KPICosteTotalMOL.InnerText = "-";
                KPIHorasMOLCORR.InnerText = "-";
                KPIHorasMOLPREV.InnerText = "-";
                KPIPartesMOL.InnerText = "-";
            }
        }

        public void ImprimirLabel(object sender, EventArgs e)
        {
            // Printer IP Address and communication port
            string ipAddress = "10.0.0.161";
            int port = 9100;
            
            // ZPL Command(s)
            string ZPLString =
                /*
             "^XA" +
             "^FO50,50" +
             "^A0N50,50" +
             "^FDPrueba de concepto^FS" +
             "^FO100,100"+
             "^BXN,10,200"+
             "^FDA4476894101Q01Z001-230111" +
             "^FS" +
             "^XZ"+
             "^XA" +
             "^FO50,50" +
             "^A0N50,50" +
             "^FDPrueba de concepto^FS" +
             "^FO100,100" +
             "^BXN,10,200" +
             "^FDA4476894101Q01Z001-230111" +
             "^FS" +
             "^XZ";
                */
                
                "^XA~TA000~JSN^LT0^MNW^MTT^PON^PMN^LH0,0^JMA^PR8,8~SD15^JUS^LRN^CI0^XZ" +
                "^XA" +
                "^MMT" +
                "^PW344" +
                "^LL0240" +
                "^LS0" +
                "^FO224,0^GFA,00768,00768,00008,:Z64:eJxjYBghgDUUBBgY+P+DwAEGeSgtAZRjYzjAIAOlcfIdHAzA/AcPCvg4gPzCD3/YaoD86p1/2GyAfPv598F8Of5+Ngsgn4+dHaSeg4UJXX8FnwWSfmsM/e1sEkA+D0S/BLJ+ATz65YH6baD6GRD6ORiYMPzDh8V/sPCAhE8DPLyGOQAAmsJY5w==:8BCA" +
                "^FO160,0^GFA,00768,00768,00008,:Z64:eJzlkDEOwjAMRe2mULZwAKRwEERB4gDcgIsgJYiD0d6kIyPdMlT92M7CyojIkKcnxd+xif7mLE6F25WBj2ysKCVlza9zeZDtXvO0+fTDBUtlaudKyxlw6g64SwA3QN9JnAduGhgAFqcWM4vXUWieJ2Udx2weu6F43zl1JDJGokY5SwutvxavhF77CYP22Umk/uNKDOWe3ExWHyZlmx42QOhh7gcMymZEZ3vIsPkpoozrn99u9ofPGzQwRsY=:A036" +
                "^FT76,42^A0N,20,16^FH\\^FDfdes^FS" +
                "^FT76,66^A0N,20,16^FH\\^FDREF: 00471                  ^FS" +
                "^FT76,90^A0N,20,16^FH\\^FD19L                        ^FS" +
                "^FT76,114^A0N,20,16^FH\\^FD354x325xh.200mm.^FS" +
                "^FT14,225^A0N,8,14^FH\\^FDARAVEN G. P.I. San Miguel^FS" +
                "^FT14,235^A0N,8,14^FH\\^FDCalle Rio Martin,n\\A76 50830 Villanueva de Gallego^FS" +
                "^FT40,207^A0B,14,14^FH\\^FDMADE IN SPAIN^FS" +
                "^FT62,200^A0B,17,16^FH\\^FD26/02/2021^FS" +
                "^BY2,2,66^FT102,192^BEN,,Y,N" +
                "^FD8411777004719^FS" +
                "^FT223,101^A0N,28,28^FH\\^FDGN 2/3^FS" +
                "^PQ1,0,1,Y^XZ";

            try
            {
                // Open connection
                System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient();
                client.Connect(ipAddress, port);

                // Write ZPL String to connection
                System.IO.StreamWriter writer =
                new System.IO.StreamWriter(client.GetStream());
                writer.Write(ZPLString);
                writer.Flush();

                // Close Connection
                writer.Close();
                client.Close();
            }
            catch (Exception ex)
            {
                // Catch Exception
            }
        }
        public void cargar_tablas(object sender, EventArgs e)
        {
            try
            {
                string año = Convert.ToString(Selecaño.SelectedValue);              
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();

                ds_KPI_Resultados_Mantenimiento_MAQ = SHConexion.Devuelve_Resultados_Mantenimiento_Maquinas(Convert.ToInt32(año));
                ds_KPI_Totales_Mantenimiento_MAQ = SHConexion.Devuelve_TOTAL_Resultados_Mantenimiento_Maquinas(Convert.ToInt32(año));
                
                rellenar_grid();
            }
            catch (Exception ex)
            {

            }
        }

        public void ContactsGridView_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Redirect")
            {
                Response.Redirect("../GP12/GP12HistoricoReferencia.aspx?REFERENCIA=" + e.CommandArgument.ToString());
            }
            if (e.CommandName == "Redirect2")
            {
                Response.Redirect("../GP12/GP12HistoricoOperario.aspx?OPERARIO=" + e.CommandArgument.ToString());
            }
            if (e.CommandName == "RedirectMAQ")
            {
                Response.Redirect("../MANTENIMIENTO/InformeMaquinas.aspx?MAQUINA=" + e.CommandArgument.ToString());
            }
            if (e.CommandName == "RedirectMOL")
            {
                string[] RecorteMolde = e.CommandArgument.ToString().Split(new char[] { ' ' });
                Response.Redirect("../MANTENIMIENTO/InformeMoldes.aspx?MOLDE=" + RecorteMolde[0].ToString());
            }
        }
    }
}
