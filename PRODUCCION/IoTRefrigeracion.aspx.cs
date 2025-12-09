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

namespace ThermoWeb.PRODUCCION
{
    public partial class IoTRefrigeracion : System.Web.UI.Page
    {

        private static DataTable DatosMolino = new DataTable();
        private static string[] ChartGeneral = new string[2];
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                Conexion_SMARTH SHconexion = new Conexion_SMARTH();
                DataTable EstadoMaquina = SHconexion.Devuelve_Estado_Maquina_BMS();
                DataTable AuxEstadoMaquina = new DataTable();

                DataTable DatosRefrigeracion = SHconexion.Devuelve_Lecturas_Refrigeracion();
                DataTable AuxRefrigeracion = new DataTable();

                //MAQUINA 33
                DatosRefrigeracion.DefaultView.RowFilter = "Maquina = '33'";
                AuxRefrigeracion = DatosRefrigeracion.DefaultView.ToTable();

                EstadoMaquina.DefaultView.RowFilter = "C_MACHINE_ID = '33'";
                AuxEstadoMaquina = EstadoMaquina.DefaultView.ToTable();
                if (AuxEstadoMaquina.Rows.Count > 0)
                {
                    M33ESTADOMAQ.InnerText = AuxEstadoMaquina.Rows[0]["ESTADO2"].ToString();
                }
                else
                {
                    M33ESTADOMAQ.InnerText = "Sin orden";
                }

                try
                {
                    //EstadoMaquina.Rows[0]["ESTADO2"].ToString();

                    M33AMBIENTAL.InnerText = AuxRefrigeracion.Rows[0]["TempAmbiental"].ToString() + "ºC / " + AuxRefrigeracion.Rows[0]["HumAmbiental"].ToString() + "%";
                    M33MOLTEMPENT.InnerText = AuxRefrigeracion.Rows[0]["MOLTempEntrada"].ToString() + "ºC";
                    M33MOLTEMSAL.InnerText = AuxRefrigeracion.Rows[0]["MOLTempSalida"].ToString() + "ºC";
                    M33MOLCAUDAL.InnerText = AuxRefrigeracion.Rows[0]["MOLCaudal"].ToString() + "m3/h";
                    M33HIDTEMPENT.InnerText = AuxRefrigeracion.Rows[0]["HIDTempEntrada"].ToString() + "ºC";
                    M33HIDTEMSAL.InnerText = AuxRefrigeracion.Rows[0]["HIDTempSalida"].ToString() + "ºC";
                    M33HIDCAUDAL.InnerText = AuxRefrigeracion.Rows[0]["HIDCaudal"].ToString() + "m3/h";

                    M33TIMESTAMP.InnerText = Convert.ToDateTime(AuxRefrigeracion.Rows[0]["FechaMedida"]).ToString("dd/MM/yyyy HH:mm");
                }
                catch (Exception ex)
                { }

                //MAQUINA 39
                DatosRefrigeracion.DefaultView.RowFilter = "Maquina = '39'";
                AuxRefrigeracion = DatosRefrigeracion.DefaultView.ToTable();

                EstadoMaquina.DefaultView.RowFilter = "C_MACHINE_ID = '39'";
                AuxEstadoMaquina = EstadoMaquina.DefaultView.ToTable();
                if (AuxEstadoMaquina.Rows.Count > 0)
                {
                    M39ESTADOMAQ.InnerText = AuxEstadoMaquina.Rows[0]["ESTADO2"].ToString();
                }
                else
                {
                    M39ESTADOMAQ.InnerText = "Sin orden";
                }
                try
                {
                    M39AMBIENTAL.InnerText = AuxRefrigeracion.Rows[0]["TempAmbiental"].ToString() + "ºC / " + AuxRefrigeracion.Rows[0]["HumAmbiental"].ToString() + "%";
                    M39MOLTEMPENT.InnerText = AuxRefrigeracion.Rows[0]["MOLTempEntrada"].ToString() + "ºC";
                    M39MOLTEMSAL.InnerText = AuxRefrigeracion.Rows[0]["MOLTempSalida"].ToString() + "ºC";
                    M39MOLCAUDAL.InnerText = AuxRefrigeracion.Rows[0]["MOLCaudal"].ToString() + "m3/h";
                    M39HIDTEMPENT.InnerText = AuxRefrigeracion.Rows[0]["HIDTempEntrada"].ToString() + "ºC";
                    M39HIDTEMSAL.InnerText = AuxRefrigeracion.Rows[0]["HIDTempSalida"].ToString() + "ºC";
                    M39HIDCAUDAL.InnerText = AuxRefrigeracion.Rows[0]["HIDCaudal"].ToString() + "m3/h";

                    M39TIMESTAMP.InnerText = Convert.ToDateTime(AuxRefrigeracion.Rows[0]["FechaMedida"]).ToString("dd/MM/yyyy HH:mm");
                }
                catch (Exception ex)
                { }

                //MAQUINA 30
                DatosRefrigeracion.DefaultView.RowFilter = "Maquina = '30'";
                AuxRefrigeracion = DatosRefrigeracion.DefaultView.ToTable();

                EstadoMaquina.DefaultView.RowFilter = "C_MACHINE_ID = '30'";
                AuxEstadoMaquina = EstadoMaquina.DefaultView.ToTable();
                if (AuxEstadoMaquina.Rows.Count > 0)
                {
                    M30ESTADOMAQ.InnerText = AuxEstadoMaquina.Rows[0]["ESTADO2"].ToString();
                }
                else
                {
                    M30ESTADOMAQ.InnerText = "Sin orden";
                }
                try
                {
                    M30AMBIENTAL.InnerText = AuxRefrigeracion.Rows[0]["TempAmbiental"].ToString() + "ºC / " + AuxRefrigeracion.Rows[0]["HumAmbiental"].ToString() + "%";
                    M30MOLTEMPENT.InnerText = AuxRefrigeracion.Rows[0]["MOLTempEntrada"].ToString() + "ºC";
                    M30MOLTEMSAL.InnerText = AuxRefrigeracion.Rows[0]["MOLTempSalida"].ToString() + "ºC";
                    M30MOLCAUDAL.InnerText = AuxRefrigeracion.Rows[0]["MOLCaudal"].ToString() + "m3/h";
                    M30HIDTEMPENT.InnerText = AuxRefrigeracion.Rows[0]["HIDTempEntrada"].ToString() + "ºC";
                    M30HIDTEMSAL.InnerText = AuxRefrigeracion.Rows[0]["HIDTempSalida"].ToString() + "ºC";
                    M30HIDCAUDAL.InnerText = AuxRefrigeracion.Rows[0]["HIDCaudal"].ToString() + "m3/h";

                    M30TIMESTAMP.InnerText = Convert.ToDateTime(AuxRefrigeracion.Rows[0]["FechaMedida"]).ToString("dd/MM/yyyy HH:mm");
                }
                catch (Exception ex)
                { }

                //MAQUINA 16
                DatosRefrigeracion.DefaultView.RowFilter = "Maquina = '16'";
                AuxRefrigeracion = DatosRefrigeracion.DefaultView.ToTable();

                EstadoMaquina.DefaultView.RowFilter = "C_MACHINE_ID = '16'";
                AuxEstadoMaquina = EstadoMaquina.DefaultView.ToTable();
                if (AuxEstadoMaquina.Rows.Count > 0)
                {
                    M16ESTADOMAQ.InnerText = AuxEstadoMaquina.Rows[0]["ESTADO2"].ToString();
                }
                else
                {
                    M16ESTADOMAQ.InnerText = "Sin orden";

                }
                try
                {
                    M16AMBIENTAL.InnerText = AuxRefrigeracion.Rows[0]["TempAmbiental"].ToString() + "ºC / " + AuxRefrigeracion.Rows[0]["HumAmbiental"].ToString() + "%";
                    M16MOLTEMPENT.InnerText = AuxRefrigeracion.Rows[0]["MOLTempEntrada"].ToString() + "ºC";
                    M16MOLTEMSAL.InnerText = AuxRefrigeracion.Rows[0]["MOLTempSalida"].ToString() + "ºC";
                    M16MOLCAUDAL.InnerText = AuxRefrigeracion.Rows[0]["MOLCaudal"].ToString() + "m3/h";
                    M16HIDTEMPENT.InnerText = AuxRefrigeracion.Rows[0]["HIDTempEntrada"].ToString() + "ºC";
                    M16HIDTEMSAL.InnerText = AuxRefrigeracion.Rows[0]["HIDTempSalida"].ToString() + "ºC";
                    M16HIDCAUDAL.InnerText = AuxRefrigeracion.Rows[0]["HIDCaudal"].ToString() + "m3/h";

                    M16TIMESTAMP.InnerText = Convert.ToDateTime(AuxRefrigeracion.Rows[0]["FechaMedida"]).ToString("dd/MM/yyyy HH:mm");
                }
                catch (Exception ex)
                { }
                //ENFRIADORA Y TORRE
                DatosRefrigeracion.DefaultView.RowFilter = "Maquina = 'ENF_TOR'";
                AuxRefrigeracion = DatosRefrigeracion.DefaultView.ToTable();     
                try
                {
                    ENFRIADORATEMPENT.InnerText = AuxRefrigeracion.Rows[0]["MOLTempEntrada"].ToString() + "ºC";
                    ENFRIADORATEMPSAL.InnerText = AuxRefrigeracion.Rows[0]["MOLTempSalida"].ToString() + "ºC";
                    ENFRIADORACAUDAL.InnerText = AuxRefrigeracion.Rows[0]["MOLCaudal"].ToString() + "m3/h";
                    TORRETEMPENT.InnerText = AuxRefrigeracion.Rows[0]["HIDTempEntrada"].ToString() + "ºC";
                    TORRETEMPSAL.InnerText = AuxRefrigeracion.Rows[0]["HIDTempSalida"].ToString() + "ºC";
                    TORRECAUDAL.InnerText = AuxRefrigeracion.Rows[0]["HIDCaudal"].ToString() + "m3/h";
                    TORRETIMESTAMP.InnerText = Convert.ToDateTime(AuxRefrigeracion.Rows[0]["FechaMedida"]).ToString("dd/MM/yyyy HH:mm");
                    ENFRIADORATIMESTAMP.InnerText = Convert.ToDateTime(AuxRefrigeracion.Rows[0]["FechaMedida"]).ToString("dd/MM/yyyy HH:mm");
                }


                catch (Exception ex)
                { }

                
            }
        }
 
        public void AbrirGraficas(object sender, EventArgs e)
        {

            HtmlButton button = (HtmlButton)sender;
            if (button.ID.Substring(5) == "ENF" || button.ID.Substring(5) == "TOR")
                {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup2('ENF_TOR');", true);
                }
            else
                { 
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1("+ button.ID.Substring(5) + ");", true);
                }
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "CargarCharts();", true);

            //genera grafica
            //abre popup
        }


        [WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
       
        public static List<object> GetChart(string maquina)
        {
            //Recupero el dato y lo filtro por la peticion
            Conexion_SMARTH SHConexion = new Conexion_SMARTH();
            DataTable DTRefrigera = SHConexion.Devuelve_Lecturas_Refrigeracion();
            DTRefrigera.DefaultView.RowFilter = "Maquina = '"+maquina+"'";
            DTRefrigera.DefaultView.Sort = "FechaMedida ASC";
            DataTable DTGraficaRefrigeracion = DTRefrigera.DefaultView.ToTable();



            //Defino el listado Inicial 
            List<object> chartData = new List<object>();
          
            //Defino listado de labels y lo añado a ChartData
            List<string> labels = new List<string>();
            foreach (DataRow row in DTGraficaRefrigeracion.Rows)
            {
                labels.Add(Convert.ToDateTime(row["FechaMedida"]).ToString("dd/MM/yy HH:mm"));
            }
            chartData.Add(labels);

           
            //Defino listado serie de entradas Hidraulicas
            List<double> series1 = new List<double>();
            foreach (DataRow row in DTGraficaRefrigeracion.Rows)
            {
                try
                {
                    series1.Add(Math.Round(Convert.ToDouble(row["HIDTempEntrada"]),2));
                }
                catch
                {
                    series1.Add(0.0);
                }
            }
            chartData.Add(series1);

            //Defino listado serie2 de salidas Hidraulicas
            List<double> series2 = new List<double>();
            foreach (DataRow row in DTGraficaRefrigeracion.Rows)
            {
                try
                {
                    series2.Add(Math.Round(Convert.ToDouble(row["HIDTempSalida"]), 2));
                }
                catch
                {
                    series2.Add(0.0);
                }
            }
            chartData.Add(series2);

            //Defino listado de caudales Hidraulicas
            List<double> series3 = new List<double>();
            foreach (DataRow row in DTGraficaRefrigeracion.Rows)
            {
                try
                {
                    series3.Add(Math.Round(Convert.ToDouble(row["HIDCaudal"]), 2));
                }
                catch
                {
                    series3.Add(0.0);
                }
            }
            chartData.Add(series3);

            //Defino listado serie de entradas Moldes
            List<double> series4 = new List<double>();
            foreach (DataRow row in DTGraficaRefrigeracion.Rows)
            {
                try
                {
                    series4.Add(Math.Round(Convert.ToDouble(row["MOLTempEntrada"]), 2));
                }
                catch
                {
                    series4.Add(0.0);
                }
            }
            chartData.Add(series4);

            //Defino listado serie2 de salidas Moldes
            List<double> series5 = new List<double>();
            foreach (DataRow row in DTGraficaRefrigeracion.Rows)
            {
                try
                {
                    series5.Add(Math.Round(Convert.ToDouble(row["MOLTempSalida"]),2));
                }
                catch
                {
                    series5.Add(0.0);
                }
            }
            chartData.Add(series5);

            //Defino listado serie2 de salidas Moldes
            List<double> series6 = new List<double>();
            foreach (DataRow row in DTGraficaRefrigeracion.Rows)
            {
                try
                {
                    series6.Add(Math.Round(Convert.ToDouble(row["MOLCaudal"]),2));
                }
                catch
                {
                    series6.Add(0.0);
                }
            }
            chartData.Add(series6);

            return chartData;
        }

        
    }

}