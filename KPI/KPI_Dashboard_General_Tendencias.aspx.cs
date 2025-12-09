using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Web.Services;
using System.Data.OleDb;


namespace ThermoWeb.KPI
{
    public partial class KPI_Dashboard_General_Tendencias : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "SetInterval", "setInterval(TestSalida, 240000);", true);
            Conexion_SMARTH SHConexion = new Conexion_SMARTH();
            DataTable AuxVIDEO = SHConexion.Devuelve_Auxiliares_AudiovisualAPP("KPIPLANTA"," AND Disponible = 1 ");
            try
            {
                Random r = new Random();
                int valorlinea = r.Next(0, AuxVIDEO.Rows.Count);
                SRCVideo.Src = AuxVIDEO.Rows[valorlinea]["URL"].ToString();
            }
            catch (Exception ex)
            { }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "CargarChartsOEE();", true);
        }

        protected void btnStartVideo_Click(object sender, EventArgs e)
        {
            // Llamada al método JavaScript para iniciar el video.
            ClientScript.RegisterStartupScript(GetType(), "PlayVideo", "playVideo();", true);
        }

        protected void btnEndVideo_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "Pop", "Cierre();", true);
            // Aquí se puede añadir lógica adicional al finalizar el video.
            // Por ejemplo, registrar la acción en la base de datos.
        }

        [WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static List<object> BARCHART_OEECAL_OEE()
        {
            //Recupero el dato y lo filtro por la peticion
            Conexion_SMARTH SHConexion = new Conexion_SMARTH();
            DataTable KPI_CAL_OEE = SHConexion.Devuelve_DashboardOEECALPLantaTendencia();
            //Defino el datable Inicial y lo reordeno
            KPI_CAL_OEE.DefaultView.Sort = "Año ASC, Mes ASC";
            DataTable KPI_CAL_Reorder = KPI_CAL_OEE.DefaultView.ToTable();

            List<object> chartData = new List<object>();
            //Defino listado de labels (meses) y lo añado a ChartData [Serie 0]

            List<string> labels = new List<string>();
            foreach (DataRow row in KPI_CAL_Reorder.Rows)
            {
                labels.Add(row["EtiquetaGraph"].ToString());
            }
            chartData.Add(labels);


            //Datos Objetivo OEEGeneral del año [Serie 1]
            List<double> series1 = new List<double>();
            foreach (DataRow row in KPI_CAL_Reorder.Rows)
            {
                try
                {
                    series1.Add(Math.Round(Convert.ToDouble(row["OEEPlantaOBJ"]), 4));
                }
                catch
                {
                    series1.Add(0.0);
                }
            }
            chartData.Add(series1);

            //Datos OEEGeneral del año [Serie 2]
            List<double> series2 = new List<double>();
            foreach (DataRow row in KPI_CAL_Reorder.Rows)
            {
                try
                {
                    series2.Add(Math.Round(Convert.ToDouble(row["OEEPlanta"]), 4));
                }
                catch
                {
                    series2.Add(0.0);
                }
            }
            chartData.Add(series2);


            ////////////****OEE Calidad**********\\\\\\\\\\
            //Objetivos OEE Calidad [Serie 3]
            List<double> series3 = new List<double>();
            foreach (DataRow row in KPI_CAL_Reorder.Rows)
            {
                try
                {
                    series3.Add(Math.Round(Convert.ToDouble(row["OEECalidadOBJ"]), 4));
                }
                catch
                {
                    series3.Add(0.0);
                }
            }
            chartData.Add(series3);

            //Datos OEE Calidad [Serie 4]
            List<double> series4 = new List<double>();
            foreach (DataRow row in KPI_CAL_Reorder.Rows)
            {
                try
                {
                    series4.Add(Math.Round(Convert.ToDouble(row["OEECalidad"]), 4));
                }
                catch
                {
                    series4.Add(0.0);
                }
            }
            chartData.Add(series4);

            /*
            KPI_CAL_OEE.DefaultView.RowFilter = "Año = " + (Convert.ToInt32(DateTime.Now.ToString("yyyy")) - 1) + "";
            DataTable AÑOANTERIOR = KPI_CAL_OEE.DefaultView.ToTable();
            KPI_CAL_OEE.DefaultView.RowFilter = "Año = " + Convert.ToInt32(DateTime.Now.ToString("yyyy")) + "";
            DataTable AÑOACTUAL = KPI_CAL_OEE.DefaultView.ToTable();
            */

            //Comparo cumplimiento de objetivos en mes y año OEE
            //string mediaactualOEE = ((double)KPI_CAL_Reorder.Compute("AVG([OEEPlanta])", "")).ToString(); //Valor medio
            string objActualOEE = KPI_CAL_Reorder.Rows[0]["OEEPlantaOBJ"].ToString(); //Objetivo  
            double media = 0;double suma = 0;int count = 0;
            foreach (DataRow row in KPI_CAL_Reorder.Rows)
            {
                string valorStr = row["OEEPlanta"].ToString();
                double valor;
                if (double.TryParse(valorStr, out valor))
                {
                    suma += valor;
                    count++;
                }
            }
            if (count > 0)
            {              
                media = Math.Round(suma / count, 2);
            }
            string mediaactualOEE = media.ToString();

            string ultimovalorOEE = KPI_CAL_Reorder.Rows[KPI_CAL_Reorder.Rows.Count - 1]["OEEPlanta"].ToString();

            string ObjetivoAnualOEE = "INCUMPLIDO";
            if (Convert.ToDouble(objActualOEE) < Convert.ToDouble(mediaactualOEE))
            {
                ObjetivoAnualOEE = "CUMPLIDO";
            }
            string ObjetivoMensualOEE = "INCUMPLIDO";
            if (Convert.ToDouble(objActualOEE) < Convert.ToDouble(ultimovalorOEE))
            {
                ObjetivoMensualOEE = "CUMPLIDO";
            }
            List<string> series5 = new List<string>();
            series5.Add(ObjetivoAnualOEE);
            series5.Add(ObjetivoMensualOEE);
            series5.Add(mediaactualOEE.Replace(",", "."));
            chartData.Add(series5);

            //Comparo cumplimiento de objetivos en mes y año OEE
            string objActualOEECalidad = KPI_CAL_Reorder.Rows[0]["OEECalidadOBJ"].ToString(); //Objetivo

            //string mediaactualOEECalidad = ((double)KPI_CAL_Reorder.Compute("AVG([OEECalidad])", "")).ToString(); //Valor medio           
            double mediaCal = 0; double sumaCal = 0; int countCal = 0;
            foreach (DataRow row in KPI_CAL_Reorder.Rows)
            {
                string valorStrCAL = row["OEECalidad"].ToString();
                double valorCAL;
                if (double.TryParse(valorStrCAL, out valorCAL))
                {
                    sumaCal += valorCAL;
                    countCal++;
                }
            }
            if (countCal > 0)
            {
               
                mediaCal = Math.Round(sumaCal / countCal, 2);
            }
            string mediaactualOEECalidad = mediaCal.ToString();



            //string mediaactualOEECalidad = KPI_CAL_Reorder.AsEnumerable().Average(row => row.Field<double?>("OEECalidad")).ToString();
            //AÑOACTUAL.Rows[AÑOACTUAL.Rows.Count - 1]["OEECalidad"].ToString(); //Valor medio
            string ultimovalorOEECalidad = KPI_CAL_Reorder.Rows[KPI_CAL_Reorder.Rows.Count - 1]["OEECalidad"].ToString();

            string ObjetivoAnualOEECalidad = "INCUMPLIDO";
            if (Convert.ToDouble(objActualOEECalidad) < Convert.ToDouble(mediaactualOEECalidad))
            {
                ObjetivoAnualOEECalidad = "CUMPLIDO";
            }
            string ObjetivoMensualOEECalidad = "INCUMPLIDO";
            if (Convert.ToDouble(objActualOEECalidad) < Convert.ToDouble(ultimovalorOEECalidad))
            {
                ObjetivoMensualOEECalidad = "CUMPLIDO";
            }
            List<string> series6 = new List<string>();

            //Calculo el máximo y el mínimo para agregar los limites
            float maxValue = KPI_CAL_Reorder.AsEnumerable().Max(row => row.Field<float>("OEECalidad")) + 1;
            float minValue = KPI_CAL_Reorder.AsEnumerable().Min(row => row.Field<float>("OEECalidad")) - 1;

            series6.Add(ObjetivoAnualOEECalidad);
            series6.Add(ObjetivoMensualOEECalidad);
            series6.Add(mediaactualOEECalidad.Replace(",", "."));
            series6.Add(minValue.ToString());
            series6.Add(maxValue.ToString());
            chartData.Add(series6);

            return chartData;
        }

        [WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static List<object> BARCHART_NOCONFORMIDADES()
        {
            //Recupero el dato y lo filtro por la peticion
            Conexion_SMARTH SHConexion = new Conexion_SMARTH();
            DataTable KPI_NoConformidades = SHConexion.Devuelve_DashboardNoConformidadesTendencia();
            KPI_NoConformidades.DefaultView.Sort = "AÑO ASC, MESNUM ASC";
            DataTable KPI_NoConformidades_Reorder = KPI_NoConformidades.DefaultView.ToTable();


            //Defino el listado Inicial 
            List<object> chartData = new List<object>();
            //Defino listado de labels (meses) y lo añado a ChartData [Serie 0]
            List<string> labels = new List<string>();
            foreach (DataRow row in KPI_NoConformidades_Reorder.Rows)
            {
                labels.Add(row["EtiquetaGraph"].ToString());
            }
            chartData.Add(labels);

            //Defino ojetivo y lo añado a ChartDada [Serie 1]
            //Objetivos NoConformidades
            List<double> series1 = new List<double>();
            foreach (DataRow row in KPI_NoConformidades_Reorder.Rows)
            {
                try
                {
                    series1.Add(Math.Round(Convert.ToDouble(row["NCOBJETIVO"]), 4));
                }
                catch
                {
                    series1.Add(0.0);
                }
            }
            chartData.Add(series1);
            //Datos NoConformidades Año 
            List<double> series2 = new List<double>();
            foreach (DataRow row in KPI_NoConformidades_Reorder.Rows)
            {
                try
                {
                    series2.Add(Math.Round(Convert.ToDouble(row["NOCONFORMIDADES"]), 4));
                }
                catch
                {
                    series2.Add(0.0);
                }
            }
            chartData.Add(series2);
            
            //ENDING
            //Comparo cumplimiento de objetivos en mes y año Calidad
            //string mediaactualOEE = ((double)KPI_CAL_Reorder.Compute("AVG([OEEPlanta])", "")).ToString(); //Valor medio
            string objActualNOCONFORMIDADES = KPI_NoConformidades_Reorder.Rows[0]["NCOBJETIVO"].ToString(); //Objetivo  
            double media = 0; double suma = 0; int count = 0;
            foreach (DataRow row in KPI_NoConformidades_Reorder.Rows)
            {
                string valorStr = row["NOCONFORMIDADES"].ToString();
                double valor;
                if (double.TryParse(valorStr, out valor))
                {
                    suma += valor;
                    count++;
                }
            }
            if (count > 0)
            {
                media = Math.Round(suma / count, 2);
            }
            string mediaactualNOCONFORMIDADES = media.ToString();
            string ultimovalorNOCONFORMIDADES = KPI_NoConformidades_Reorder.Rows[KPI_NoConformidades_Reorder.Rows.Count - 1]["NOCONFORMIDADES"].ToString();
            string ObjetivoAnualNOCONFORMIDADES = "INCUMPLIDO";
            if (Convert.ToDouble(objActualNOCONFORMIDADES) > Convert.ToDouble(mediaactualNOCONFORMIDADES))
            {
                ObjetivoAnualNOCONFORMIDADES = "CUMPLIDO";
            }
            string ObjetivoMensualNOCONFORMIDADES = "INCUMPLIDO";
            if (Convert.ToDouble(objActualNOCONFORMIDADES) > Convert.ToDouble(ultimovalorNOCONFORMIDADES))
            {
                ObjetivoMensualNOCONFORMIDADES = "CUMPLIDO";
            }
            List<string> series3 = new List<string>();
            series3.Add(ObjetivoAnualNOCONFORMIDADES);
            series3.Add(ObjetivoMensualNOCONFORMIDADES);
            series3.Add(mediaactualNOCONFORMIDADES.Replace(",", "."));
            chartData.Add(series3);

            return chartData;
        }

        [WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static List<object> BARCHART_APROVECHAMIENTO()
        {
            //Recupero el dato y lo filtro por la peticion
            Conexion_SMARTH SHConexion = new Conexion_SMARTH();
            DataTable KPI_Aprovechamiento = SHConexion.Devuelve_DashboardAprovechamientoOPSTendencias();
            //Defino el datable Inicial y lo reordeno
            KPI_Aprovechamiento.DefaultView.Sort = "Año ASC, MESNUM ASC";
            DataTable KPI_Aprovechamiento_Reorder = KPI_Aprovechamiento.DefaultView.ToTable();

            //Defino el listado Inicial 
            List<object> chartData = new List<object>();

            //Defino listado de labels (meses) y lo añado a ChartData [Serie 0]
            List<string> labels = new List<string>();
            foreach (DataRow row in KPI_Aprovechamiento_Reorder.Rows)
            {
                labels.Add(row["EtiquetaGraph"].ToString());
            }
            chartData.Add(labels);

            //Defino ojetivo y lo añado a ChartDada [Serie 1]
            //Objetivos NoConformidades
            List<double> series1 = new List<double>();
            foreach (DataRow row in KPI_Aprovechamiento_Reorder.Rows)
            {
                try
                {
                    series1.Add(Math.Round(Convert.ToDouble(row["OBJETIVO"]), 4));
                }
                catch
                {
                    series1.Add(0.0);
                }
            }
            chartData.Add(series1);

            //Datos Aprovechamiento [Serie 2]
            List<double> series2 = new List<double>();
            foreach (DataRow row in KPI_Aprovechamiento_Reorder.Rows)
            {
                try
                {
                    series2.Add(Math.Round(Convert.ToDouble(row["OPLIBRES"]), 4));
                }
                catch
                {
                    series2.Add(0.0);
                }
            }
            chartData.Add(series2);

            string objActualAprovechamiento = KPI_Aprovechamiento.Rows[0]["OBJETIVO"].ToString(); //Objetivo  
            double media = 0; double suma = 0; int count = 0;
            foreach (DataRow row in KPI_Aprovechamiento.Rows)
            {
                string valorStr = row["OPLIBRES"].ToString();
                double valor;
                if (double.TryParse(valorStr, out valor))
                {
                    suma += valor;
                    count++;
                }
            }
            if (count > 0)
            {
                media = Math.Round(suma / count, 2);
            }
            string mediaactualAprovechamiento = media.ToString();

            string ultimovalorAprovechamiento = KPI_Aprovechamiento_Reorder.Rows[KPI_Aprovechamiento_Reorder.Rows.Count - 1]["OPLIBRES"].ToString();

            string ObjetivoAnualAprovechamiento = "INCUMPLIDO";
            if (Convert.ToDouble(objActualAprovechamiento) > Convert.ToDouble(mediaactualAprovechamiento))
            {
                ObjetivoAnualAprovechamiento = "CUMPLIDO";
            }
            string ObjetivoMensualAprovechamiento = "INCUMPLIDO";
            if (Convert.ToDouble(objActualAprovechamiento) > Convert.ToDouble(ultimovalorAprovechamiento))
            {
                ObjetivoMensualAprovechamiento = "CUMPLIDO";
            }

            List<string> series3 = new List<string>();
            series3.Add(ObjetivoAnualAprovechamiento);
            series3.Add(ObjetivoMensualAprovechamiento);
            series3.Add(mediaactualAprovechamiento.Replace(",", "."));
            chartData.Add(series3);
            return chartData;
        }

        [WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static List<object> BARCHART_ABSENTISMO()
        {
            //Recupero el dato y lo filtro por la peticion
            Conexion_SMARTH SHConexion = new Conexion_SMARTH();
            DataTable KPI_Absentismo = SHConexion.Devuelve_DashboardAbsentismoTendencias();

            KPI_Absentismo.DefaultView.Sort = "YEAR ASC, MES ASC";
            DataTable KPI_Absentismo_Reorder = KPI_Absentismo.DefaultView.ToTable();


            //Defino el listado Inicial 
            List<object> chartData = new List<object>();

            //Defino listado de labels (meses) y lo añado a ChartData [Serie 0]
            List<string> labels = new List<string>();
            foreach (DataRow row in KPI_Absentismo_Reorder.Rows)
            {
                labels.Add(row["EtiquetaGraph"].ToString());
            }
            chartData.Add(labels);

            //Defino ojetivo y lo añado a ChartDada [Serie 1]
            //Objetivos NoConformidades
            List<double> series1 = new List<double>();
            foreach (DataRow row in KPI_Absentismo_Reorder.Rows)
            {
                try
                {
                    series1.Add(Math.Round(Convert.ToDouble(row["OBJETIVO"]), 4));
                }
                catch
                {
                    series1.Add(0.0);
                }
            }
            chartData.Add(series1);
            //Datos OEEGeneral Año anterior [Serie 2]
          
            //Datos OEEGEneral Año actual [Serie 3]
            List<double?> series2 = new List<double?>();
            foreach (DataRow row in KPI_Absentismo_Reorder.Rows)
            {
                try
                {
                    series2.Add(Math.Round(Convert.ToDouble(row["PORCABS"]), 4));
                }
                catch
                {
                    series2.Add(null);
                }
            }
            chartData.Add(series2);

            //Comparo cumplimiento de objetivos en mes y año OEE
            string objActualAbsentismo = KPI_Absentismo_Reorder.Rows[0]["OBJETIVO"].ToString(); //Objetivo  
            double media = 0; double suma = 0; int count = 0;
            foreach (DataRow row in KPI_Absentismo_Reorder.Rows)
            {
                string valorStr = row["PORCABS"].ToString();
                double valor;
                if (double.TryParse(valorStr, out valor))
                {
                    suma += valor;
                    count++;
                }
            }
            if (count > 0)
            {
                media = Math.Round(suma / count,2);
            }
            string mediaactualAbsentismo = media.ToString();

            string ultimovalorAbsentismo = KPI_Absentismo_Reorder.Rows[KPI_Absentismo_Reorder.Rows.Count - 1]["PORCABS"].ToString();

            string ObjetivoAnualAbsentismo = "INCUMPLIDO";
            if (Convert.ToDouble(objActualAbsentismo) > Convert.ToDouble(mediaactualAbsentismo))
            {
                ObjetivoAnualAbsentismo = "CUMPLIDO";
            }
            string ObjetivoMensualAbsentismo = "INCUMPLIDO";
            if (Convert.ToDouble(objActualAbsentismo) > Convert.ToDouble(ultimovalorAbsentismo))
            {
                ObjetivoMensualAbsentismo = "CUMPLIDO";
            }

            double maxValue = Convert.ToDouble(KPI_Absentismo_Reorder.AsEnumerable().Max(row => row.Field<double>("PORCABS"))) + 1;
            double minValue = Convert.ToDouble(KPI_Absentismo_Reorder.AsEnumerable().Min(row => row.Field<double>("PORCABS"))) - 1;

            List<string> series3 = new List<string>();
            series3.Add(ObjetivoAnualAbsentismo); //serie 0
            series3.Add(ObjetivoMensualAbsentismo); //serie 1
            series3.Add(mediaactualAbsentismo.Replace(",", ".")); //serie 2
            series3.Add(minValue.ToString().Replace(",", ".")); //serie 3
            series3.Add(maxValue.ToString().Replace(",", ".")); //serie 4
            chartData.Add(series3);
            return chartData;
        }

    }
}
