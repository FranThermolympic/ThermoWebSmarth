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
    public partial class KPI_Dashboard_General : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {           
            if (!IsPostBack)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "CargarChartsOEE();", true);

            }

        }


        [WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static List<object> Resultados_Completos_Mantenimiento_MAQ(string año)
        {
            //Recupero el dato y lo filtro por la peticion
            Conexion_SMARTH SHConexion = new Conexion_SMARTH();
            DataSet Mantenimiento = SHConexion.Devuelve_Resultados_Mantenimiento_Maquinas(Convert.ToInt32(año));
           
            //Defino el listado Inicial 
            List<object> chartData = new List<object>();

            //Defino listado de labels y lo añado a ChartData
            List<string> labels = new List<string>();
            foreach (DataRow row in Mantenimiento.Tables[0].Rows)
            {
                labels.Add(row["MESTEXTO"].ToString());
            }
            chartData.Add(labels);


            //Defino limite objetivo CorrectivoMaq
            List<double> series1 = new List<double>();
            foreach (DataRow row in Mantenimiento.Tables[0].Rows)
            {
                try
                {
                    series1.Add(Math.Round(Convert.ToDouble(row["PORCCORRECTIVO"]), 2));
                }
                catch
                {
                    series1.Add(0.0);
                }
            }
            chartData.Add(series1);

            //Defino listado serie2 de salidas Hidraulicas
            List<double> series2 = new List<double>();
            foreach (DataRow row in Mantenimiento.Tables[0].Rows)
            {
                try
                {
                    series2.Add(Math.Round(Convert.ToDouble(row["KPICOR"]), 2));
                }
                catch
                {
                    series2.Add(0.0);
                }
            }
            chartData.Add(series2);

            List<double> series3 = new List<double>();
            foreach (DataRow row in Mantenimiento.Tables[0].Rows)
            {
                try
                {
                    series3.Add(Math.Round(Convert.ToDouble(row["PORCPREVENTIVO"]), 2));
                }
                catch
                {
                    series3.Add(0.0);
                }
            }
            chartData.Add(series3);

            //Defino listado serie2 de salidas Hidraulicas
            List<double> series4 = new List<double>();
            foreach (DataRow row in Mantenimiento.Tables[0].Rows)
            {
                try
                {
                    series4.Add(Math.Round(Convert.ToDouble(row["KPIPREV"]), 2));
                }
                catch
                {
                    series4.Add(0.0);
                }
            }
            chartData.Add(series4);
            return chartData;
        }

        [WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static List<object> PIE_Ranking_Maquinas_HORASMES(string año, string mes)
        {
            //Recupero el dato y lo filtro por la peticion
            Conexion_SMARTH SHConexion = new Conexion_SMARTH();
            DataSet ds_KPI_Ranking_MAQxParte = SHConexion.Devuelve_Ranking_Maquinas_PartesAbiertos(Convert.ToInt32(año), " AND MES = '" + mes + "'", "");
            //Defino el listado Inicial 
            List<object> chartData = new List<object>();

            //Defino listado de labels y lo añado a ChartData
            List<string> labels = new List<string>();
            foreach (DataRow row in ds_KPI_Ranking_MAQxParte.Tables[0].Rows)
            {
                labels.Add(row["IdMaquinaCHAR"].ToString());
            }
            chartData.Add(labels);


            //Defino limite objetivo CorrectivoMaq
            List<double> series1 = new List<double>();
            foreach (DataRow row in ds_KPI_Ranking_MAQxParte.Tables[0].Rows)
            {
                try
                {
                    series1.Add(Math.Round(Convert.ToDouble(row["MTTR"]), 2));
                }
                catch
                {
                    series1.Add(0.0);
                }
            }
            chartData.Add(series1);

            //Defino listado serie2 de salidas Hidraulicas

            return chartData;
        }

        [WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static List<object> PIE_Ranking_Maquinas_HORASMESMTBF(string año, string mes)
        {
            //Recupero el dato y lo filtro por la peticion
            Conexion_SMARTH SHConexion = new Conexion_SMARTH();
            DataSet ds_KPI_Ranking_MAQxParte = SHConexion.Devuelve_Ranking_Maquinas_PartesAbiertosMTBF(Convert.ToInt32(año), " AND NUM.MES = '" + mes + "'", "");
            //Defino el listado Inicial 
            List<object> chartData = new List<object>();

            //Defino listado de labels y lo añado a ChartData
            List<string> labels = new List<string>();
            foreach (DataRow row in ds_KPI_Ranking_MAQxParte.Tables[0].Rows)
            {
                labels.Add(row["IdMaquinaCHAR"].ToString());
            }
            chartData.Add(labels);


            //Defino limite objetivo CorrectivoMaq
            List<double> series1 = new List<double>();
            foreach (DataRow row in ds_KPI_Ranking_MAQxParte.Tables[0].Rows)
            {
                try
                {
                    series1.Add(Math.Round(Convert.ToDouble(row["MTBFINVERSO"]), 2));
                }
                catch
                {
                    series1.Add(0.0);
                }
            }
            chartData.Add(series1);

            //Defino listado serie2 de salidas Hidraulicas

            return chartData;
        }

        [WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static List<object> PIE_Ranking_Maquinas_HORASAÑO(string año)
        {
            //Recupero el dato y lo filtro por la peticion
            Conexion_SMARTH SHConexion = new Conexion_SMARTH();
            DataSet ds_KPI_Ranking_MAQxParte = SHConexion.Devuelve_Ranking_Maquinas_PartesAbiertosAÑO(Convert.ToInt32(año),"","");
            //Defino el listado Inicial 
            List<object> chartData = new List<object>();

            //Defino listado de labels y lo añado a ChartData
            List<string> labels = new List<string>();
            foreach (DataRow row in ds_KPI_Ranking_MAQxParte.Tables[0].Rows)
            {
                labels.Add(row["IdMaquinaCHAR"].ToString());
            }
            chartData.Add(labels);


            //Defino limite objetivo CorrectivoMaq
            List<double> series1 = new List<double>();
            foreach (DataRow row in ds_KPI_Ranking_MAQxParte.Tables[0].Rows)
            {
                try
                {
                    series1.Add(Math.Round(Convert.ToDouble(row["MTTR"]), 2));
                }
                catch
                {
                    series1.Add(0.0);
                }
            }
            chartData.Add(series1);

            //Defino listado serie2 de salidas Hidraulicas

            return chartData;
        }

        [WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static List<object> PIE_Ranking_Maquinas_HORASAÑOMTBF(string año)
        {
            //Recupero el dato y lo filtro por la peticion
            Conexion_SMARTH SHConexion = new Conexion_SMARTH();
            DataSet ds_KPI_Ranking_MAQxParte = SHConexion.Devuelve_Ranking_Maquinas_PartesAbiertosAÑOMTBF(Convert.ToInt32(año), "", "");
            //Defino el listado Inicial 
            List<object> chartData = new List<object>();

            //Defino listado de labels y lo añado a ChartData
            List<string> labels = new List<string>();
            foreach (DataRow row in ds_KPI_Ranking_MAQxParte.Tables[0].Rows)
            {
                labels.Add(row["IdMaquinaCHAR"].ToString());
            }
            chartData.Add(labels);


            //Defino limite objetivo CorrectivoMaq
            List<double> series1 = new List<double>();
            foreach (DataRow row in ds_KPI_Ranking_MAQxParte.Tables[0].Rows)
            {
                try
                {
                    series1.Add(Math.Round(Convert.ToDouble(row["MTBFINVERSO"]), 4));
                }
                catch
                {
                    series1.Add(0.0);
                }
            }
            chartData.Add(series1);

            //Defino listado serie2 de salidas Hidraulicas

            return chartData;
        }

        ////////////////MOLDES
        [WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static List<object> Resultados_Completos_Mantenimiento_MOL(string año)
        {
            //Recupero el dato y lo filtro por la peticion
            Conexion_SMARTH SHConexion = new Conexion_SMARTH();
            DataSet Mantenimiento = SHConexion.Devuelve_Resultados_Mantenimiento_Moldes(Convert.ToInt32(año));

            //Defino el listado Inicial 
            List<object> chartData = new List<object>();

            //Defino listado de labels y lo añado a ChartData
            List<string> labels = new List<string>();
            foreach (DataRow row in Mantenimiento.Tables[0].Rows)
            {
                labels.Add(row["MESTEXTO"].ToString());
            }
            chartData.Add(labels);


            //Defino limite objetivo CorrectivoMaq
            List<double> series1 = new List<double>();
            foreach (DataRow row in Mantenimiento.Tables[0].Rows)
            {
                try
                {
                    series1.Add(Math.Round(Convert.ToDouble(row["PORCCORRECTIVO"]), 2));
                }
                catch
                {
                    series1.Add(0.0);
                }
            }
            chartData.Add(series1);

            //Defino listado serie2 de salidas Hidraulicas
            List<double> series2 = new List<double>();
            foreach (DataRow row in Mantenimiento.Tables[0].Rows)
            {
                try
                {
                    series2.Add(Math.Round(Convert.ToDouble(row["KPICOR"]), 2));
                }
                catch
                {
                    series2.Add(0.0);
                }
            }
            chartData.Add(series2);

            List<double> series3 = new List<double>();
            foreach (DataRow row in Mantenimiento.Tables[0].Rows)
            {
                try
                {
                    series3.Add(Math.Round(Convert.ToDouble(row["PORCPREVENTIVO"]), 2));
                }
                catch
                {
                    series3.Add(0.0);
                }
            }
            chartData.Add(series3);

            //Defino listado serie2 de salidas Hidraulicas
            List<double> series4 = new List<double>();
            foreach (DataRow row in Mantenimiento.Tables[0].Rows)
            {
                try
                {
                    series4.Add(Math.Round(Convert.ToDouble(row["KPIPREV"]), 2));
                }
                catch
                {
                    series4.Add(0.0);
                }
            }
            chartData.Add(series4);
            return chartData;
        }

        [WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static List<object> PIE_Ranking_Molde_HORASMES(string año, string mes)
        {
            //Recupero el dato y lo filtro por la peticion
            Conexion_SMARTH SHConexion = new Conexion_SMARTH();
            DataSet ds_KPI_Ranking_MAQxParte = SHConexion.Devuelve_Ranking_Moldes_PartesAbiertos(Convert.ToInt32(año), " AND MES = '" + mes + "'", "");
            //Defino el listado Inicial 
            List<object> chartData = new List<object>();

            //Defino listado de labels y lo añado a ChartData
            List<string> labels = new List<string>();
            foreach (DataRow row in ds_KPI_Ranking_MAQxParte.Tables[0].Rows)
            {
                labels.Add(row["MOL"].ToString());
            }
            chartData.Add(labels);


            //Defino limite objetivo CorrectivoMaq
            List<double> series1 = new List<double>();
            foreach (DataRow row in ds_KPI_Ranking_MAQxParte.Tables[0].Rows)
            {
                try
                {
                    series1.Add(Math.Round(Convert.ToDouble(row["MTTR"]), 2));
                }
                catch
                {
                    series1.Add(0.0);
                }
            }
            chartData.Add(series1);

            //Defino listado serie2 de salidas Hidraulicas

            return chartData;
        }

        [WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static List<object> PIE_Ranking_Molde_HORASAÑO(string año)
        {
            //Recupero el dato y lo filtro por la peticion
            Conexion_SMARTH SHConexion = new Conexion_SMARTH();
            DataSet ds_KPI_Ranking_MAQxParte = SHConexion.Devuelve_Ranking_Moldes_PartesAbiertosAÑO(Convert.ToInt32(año), "", "");
            //Defino el listado Inicial 
            List<object> chartData = new List<object>();

            //Defino listado de labels y lo añado a ChartData
            List<string> labels = new List<string>();
            foreach (DataRow row in ds_KPI_Ranking_MAQxParte.Tables[0].Rows)
            {
                labels.Add(row["MOL"].ToString());
            }
            chartData.Add(labels);


            //Defino limite objetivo CorrectivoMaq
            List<double> series1 = new List<double>();
            foreach (DataRow row in ds_KPI_Ranking_MAQxParte.Tables[0].Rows)
            {
                try
                {
                    series1.Add(Math.Round(Convert.ToDouble(row["MTTR"]), 2));
                }
                catch
                {
                    series1.Add(0.0);
                }
            }
            chartData.Add(series1);

            //Defino listado serie2 de salidas Hidraulicas

            return chartData;
        }

        [WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static List<object> PIE_Ranking_Molde_HORASMESMTBF(string año, string mes)
        {
            //Recupero el dato y lo filtro por la peticion
            Conexion_SMARTH SHConexion = new Conexion_SMARTH();
            DataSet ds_KPI_Ranking_MAQxParte = SHConexion.Devuelve_Ranking_Moldes_PartesAbiertosMTBF(Convert.ToInt32(año), " AND NUM.MES = '" + mes + "'", "");
            //Defino el listado Inicial 
            List<object> chartData = new List<object>();

            //Defino listado de labels y lo añado a ChartData
            List<string> labels = new List<string>();
            foreach (DataRow row in ds_KPI_Ranking_MAQxParte.Tables[0].Rows)
            {
                labels.Add(row["MOL"].ToString());
            }
            chartData.Add(labels);


            //Defino limite objetivo CorrectivoMaq
            List<double> series1 = new List<double>();
            foreach (DataRow row in ds_KPI_Ranking_MAQxParte.Tables[0].Rows)
            {
                try
                {
                    series1.Add(Math.Round(Convert.ToDouble(row["MTBFINVERSO"]), 2));
                }
                catch
                {
                    series1.Add(0.0);
                }
            }
            chartData.Add(series1);

            //Defino listado serie2 de salidas Hidraulicas

            return chartData;
        }

        [WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static List<object> PIE_Ranking_Molde_HORASAÑOMTBF(string año)
        {
            //Recupero el dato y lo filtro por la peticion
            Conexion_SMARTH SHConexion = new Conexion_SMARTH();
            DataSet ds_KPI_Ranking_MAQxParte = SHConexion.Devuelve_Ranking_Moldes_PartesAbiertosAÑOMTBF(Convert.ToInt32(año), "", "");
            //Defino el listado Inicial 
            List<object> chartData = new List<object>();

            //Defino listado de labels y lo añado a ChartData
            List<string> labels = new List<string>();
            foreach (DataRow row in ds_KPI_Ranking_MAQxParte.Tables[0].Rows)
            {
                labels.Add(row["MOL"].ToString());
            }
            chartData.Add(labels);


            //Defino limite objetivo CorrectivoMaq
            List<double> series1 = new List<double>();
            foreach (DataRow row in ds_KPI_Ranking_MAQxParte.Tables[0].Rows)
            {
                try
                {
                    series1.Add(Math.Round(Convert.ToDouble(row["MTBFINVERSO"]), 4));
                }
                catch
                {
                    series1.Add(0.0);
                }
            }
            chartData.Add(series1);

            //Defino listado serie2 de salidas Hidraulicas

            return chartData;
        }

       

        [WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static List<object> BARCHART_OEECAL_OEE()
        {
            //Recupero el dato y lo filtro por la peticion
            Conexion_SMARTH SHConexion = new Conexion_SMARTH();
            DataTable KPI_CAL_OEE = SHConexion.Devuelve_DashboardOEECALPLanta();
            //DataTable NC = SHConexion.Devuelve_DashboardNoConformidades();

            KPI_CAL_OEE.DefaultView.RowFilter = "Año = " + (Convert.ToInt32(DateTime.Now.ToString("yyyy")) - 1) + "";
            DataTable AÑOANTERIOR = KPI_CAL_OEE.DefaultView.ToTable();
            KPI_CAL_OEE.DefaultView.RowFilter = "Año = " + Convert.ToInt32(DateTime.Now.ToString("yyyy")) + "";
            DataTable AÑOACTUAL = KPI_CAL_OEE.DefaultView.ToTable();

            //Defino el listado Inicial 
            List<object> chartData = new List<object>();

            //Defino listado de labels (meses) y lo añado a ChartData [Serie 0]
            List<string> labels = new List<string>();
            foreach (DataRow row in AÑOANTERIOR.Rows)
            {
                labels.Add(row["EtiquetaMES"].ToString());
            }
            chartData.Add(labels);

            ////////////****OEE Planta**********\\\\\\\\\\
            //Defino ojetivo y lo añado a ChartDada [Serie 1]
            //Objetivos OEE General
            List<double> series1 = new List<double>();
            foreach (DataRow row in AÑOANTERIOR.Rows)
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
            //Datos OEEGeneral Año anterior [Serie 2]
            List<double> series2 = new List<double>();
            foreach (DataRow row in AÑOANTERIOR.Rows)
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
            //Datos OEEGEneral Año actual [Serie 3]
            List<double?> series3 = new List<double?>();
            foreach (DataRow row in AÑOACTUAL.Rows)
            {
                try
                {
                    if (Convert.ToDouble(row["OEEPlanta"]) == 0)
                    {
                        series3.Add(null);
                    }
                    else
                    {
                    series3.Add(Math.Round(Convert.ToDouble(row["OEEPlanta"]), 4));
                    }
                }
                catch
                {
                    series3.Add(null);
                }
            }
            chartData.Add(series3);


            ////////////****OEE Calidad**********\\\\\\\\\\
            //Objetivos OEE Calidad [Serie 4]
            List<double> series4 = new List<double>();
            foreach (DataRow row in AÑOANTERIOR.Rows)
            {
                try
                {
                    series4.Add(Math.Round(Convert.ToDouble(row["OEECalidadOBJ"]), 4));
                }
                catch
                {
                    series4.Add(0.0);
                }
            }
            chartData.Add(series4);

            //Datos OEE Calidad [Serie 5]
            List<double> series5 = new List<double>();
            foreach (DataRow row in AÑOANTERIOR.Rows)
            {
                try
                {
                    series5.Add(Math.Round(Convert.ToDouble(row["OEECalidad"]), 4));
                }
                catch
                {
                    series5.Add(0.0);
                }
            }
            chartData.Add(series5);

            //Datos OEE Calidad [Serie 6]
            List<double?> series6 = new List<double?>();
            foreach (DataRow row in AÑOACTUAL.Rows)
            {
                try
                {
                    if (Convert.ToDouble(row["OEECalidad"]) == 0)
                    {
                        series6.Add(null);
                    }
                    else
                    { 
                    series6.Add(Math.Round(Convert.ToDouble(row["OEECalidad"]), 4));
                    }
                }
                catch
                {
                    series6.Add(null);
                }
            }
            chartData.Add(series6);

            //Comparo cumplimiento de objetivos en mes y año OEE
            string objActualOEE = AÑOACTUAL.Rows[0]["OEEPlantaOBJ"].ToString(); //Objetivo

            string mediaactualOEE = AÑOACTUAL.Rows[AÑOACTUAL.Rows.Count - 1]["OEEPlanta"].ToString(); //Valor medio
            string ultimovalorOEE = AÑOACTUAL.Rows[Convert.ToInt32(DateTime.Now.ToString("MM")) - 1]["OEEPlanta"].ToString();

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
            List<string> series7 = new List<string>();
            series7.Add(ObjetivoAnualOEE);
            series7.Add(ObjetivoMensualOEE);
            chartData.Add(series7);

            //Comparo cumplimiento de objetivos en mes y año OEE
            string objActualOEECalidad = AÑOACTUAL.Rows[0]["OEECalidadOBJ"].ToString(); //Objetivo

            string mediaactualOEECalidad = AÑOACTUAL.Rows[AÑOACTUAL.Rows.Count - 1]["OEECalidad"].ToString(); //Valor medio
            string ultimovalorOEECalidad = AÑOACTUAL.Rows[Convert.ToInt32(DateTime.Now.ToString("MM")) - 1]["OEECalidad"].ToString();

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
            List<string> series8 = new List<string>();
            series8.Add(ObjetivoAnualOEECalidad);
            series8.Add(ObjetivoMensualOEECalidad);
            chartData.Add(series8);

            return chartData;
        }

        [WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static List<object> BARCHART_NOCONFORMIDADES()
        {
            //Recupero el dato y lo filtro por la peticion
            Conexion_SMARTH SHConexion = new Conexion_SMARTH();
            DataTable KPI_NoConformidades = SHConexion.Devuelve_DashboardNoConformidades();

            KPI_NoConformidades.DefaultView.RowFilter = "AÑO = " + (Convert.ToInt32(DateTime.Now.ToString("yyyy")) - 1) + "";
            DataTable AÑOANTERIOR = KPI_NoConformidades.DefaultView.ToTable();

            KPI_NoConformidades.DefaultView.RowFilter = "AÑO = " + Convert.ToInt32(DateTime.Now.ToString("yyyy")) + "";
            DataTable AÑOACTUAL = KPI_NoConformidades.DefaultView.ToTable();

            //Defino el listado Inicial 
            List<object> chartData = new List<object>();

            //Defino listado de labels (meses) y lo añado a ChartData [Serie 0]
            List<string> labels = new List<string>();
            foreach (DataRow row in AÑOANTERIOR.Rows)
            {
                labels.Add(row["EtiquetaMES"].ToString());
            }
            chartData.Add(labels);

            //Defino ojetivo y lo añado a ChartDada [Serie 1]
            //Objetivos NoConformidades
            List<double> series1 = new List<double>();
            foreach (DataRow row in AÑOANTERIOR.Rows)
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
            //Datos OEEGeneral Año anterior [Serie 2]
            List<double> series2 = new List<double>();
            foreach (DataRow row in AÑOANTERIOR.Rows)
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
            //Datos OEEGEneral Año actual [Serie 3]
            List<double> series3 = new List<double>();
            foreach (DataRow row in AÑOACTUAL.Rows)
            {
                try
                {
                    series3.Add(Math.Round(Convert.ToDouble(row["NOCONFORMIDADES"]), 4));
                }
                catch
                {
                    series3.Add(0.0);
                }
            }
            chartData.Add(series3);

            //Comparo cumplimiento de objetivos en mes y año OEE
            string objActualNOCONFORMIDADES = AÑOACTUAL.Rows[0]["NCOBJETIVO"].ToString(); //Objetivo

            string mediaactualNOCONFORMIDADES = AÑOACTUAL.Rows[AÑOACTUAL.Rows.Count - 1]["NOCONFORMIDADES"].ToString(); //Valor medio
            string ultimovalorNOCONFORMIDADES = AÑOACTUAL.Rows[Convert.ToInt32(DateTime.Now.ToString("MM")) - 1]["NOCONFORMIDADES"].ToString();

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
            List<string> series4 = new List<string>();
            series4.Add(ObjetivoAnualNOCONFORMIDADES);
            series4.Add(ObjetivoMensualNOCONFORMIDADES);
            chartData.Add(series4);
            return chartData;
        }

        [WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static List<object> BARCHART_APROVECHAMIENTO()
        {
            //Recupero el dato y lo filtro por la peticion
            Conexion_SMARTH SHConexion = new Conexion_SMARTH();
            DataTable KPI_NoConformidades = SHConexion.Devuelve_DashboardAprovechamientoOPS();

            KPI_NoConformidades.DefaultView.RowFilter = "AÑO = " + (Convert.ToInt32(DateTime.Now.ToString("yyyy")) - 1) + "";
            DataTable AÑOANTERIOR = KPI_NoConformidades.DefaultView.ToTable();

            KPI_NoConformidades.DefaultView.RowFilter = "AÑO = " + Convert.ToInt32(DateTime.Now.ToString("yyyy")) + "";
            DataTable AÑOACTUAL = KPI_NoConformidades.DefaultView.ToTable();

            //Defino el listado Inicial 
            List<object> chartData = new List<object>();

            //Defino listado de labels (meses) y lo añado a ChartData [Serie 0]
            List<string> labels = new List<string>();
            foreach (DataRow row in AÑOANTERIOR.Rows)
            {
                labels.Add(row["EtiquetaMES"].ToString());
            }
            chartData.Add(labels);

            //Defino ojetivo y lo añado a ChartDada [Serie 1]
            //Objetivos NoConformidades
            List<double> series1 = new List<double>();
            foreach (DataRow row in AÑOANTERIOR.Rows)
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
            List<double> series2 = new List<double>();
            foreach (DataRow row in AÑOANTERIOR.Rows)
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
            //Datos OEEGEneral Año actual [Serie 3]
            List<double?> series3 = new List<double?>();
            foreach (DataRow row in AÑOACTUAL.Rows)
            {
                try
                {
                    series3.Add(Math.Round(Convert.ToDouble(row["OPLIBRES"]), 4));
                }
                catch
                {
                    series3.Add(null);
                }
            }
            chartData.Add(series3);

            //Comparo cumplimiento de objetivos en mes y año OEE
            string objActualNOCONFORMIDADES = AÑOACTUAL.Rows[0]["OBJETIVO"].ToString(); //Objetivo

            string mediaactualNOCONFORMIDADES = AÑOACTUAL.Rows[AÑOACTUAL.Rows.Count - 1]["OPLIBRES"].ToString(); //Valor medio
            string ultimovalorNOCONFORMIDADES = AÑOACTUAL.Rows[Convert.ToInt32(DateTime.Now.ToString("MM")) - 1]["OPLIBRES"].ToString();

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
            List<string> series4 = new List<string>();
            series4.Add(ObjetivoAnualNOCONFORMIDADES);
            series4.Add(ObjetivoMensualNOCONFORMIDADES);
            chartData.Add(series4);
            return chartData;
        }

        [WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static List<object> BARCHART_ABSENTISMO()
        {
            //Recupero el dato y lo filtro por la peticion
            Conexion_SMARTH SHConexion = new Conexion_SMARTH();
            DataTable KPI_NoConformidades = SHConexion.Devuelve_DashboardAbsentismo();

            KPI_NoConformidades.DefaultView.RowFilter = "AÑO = " + (Convert.ToInt32(DateTime.Now.ToString("yyyy")) - 1) + "";
            DataTable AÑOANTERIOR = KPI_NoConformidades.DefaultView.ToTable();

            KPI_NoConformidades.DefaultView.RowFilter = "AÑO = " + Convert.ToInt32(DateTime.Now.ToString("yyyy")) + "";
            DataTable AÑOACTUAL = KPI_NoConformidades.DefaultView.ToTable();

            //Defino el listado Inicial 
            List<object> chartData = new List<object>();

            //Defino listado de labels (meses) y lo añado a ChartData [Serie 0]
            List<string> labels = new List<string>();
            foreach (DataRow row in AÑOANTERIOR.Rows)
            {
                labels.Add(row["EtiquetaMES"].ToString());
            }
            chartData.Add(labels);

            //Defino ojetivo y lo añado a ChartDada [Serie 1]
            //Objetivos NoConformidades
            List<double> series1 = new List<double>();
            foreach (DataRow row in AÑOANTERIOR.Rows)
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
            List<double> series2 = new List<double>();
            foreach (DataRow row in AÑOANTERIOR.Rows)
            {
                try
                {
                    series2.Add(Math.Round(Convert.ToDouble(row["PORCABS"]), 4));
                }
                catch
                {
                    series2.Add(0.0);
                }
            }
            chartData.Add(series2);
            //Datos OEEGEneral Año actual [Serie 3]
            List<double?> series3 = new List<double?>();
            foreach (DataRow row in AÑOACTUAL.Rows)
            {
                try
                {
                    series3.Add(Math.Round(Convert.ToDouble(row["PORCABS"]), 4));
                }
                catch
                {
                    series3.Add(null);
                }
            }
            chartData.Add(series3);

            //Comparo cumplimiento de objetivos en mes y año OEE
            string objActualNOCONFORMIDADES = AÑOACTUAL.Rows[0]["OBJETIVO"].ToString(); //Objetivo

            string mediaactualNOCONFORMIDADES = AÑOACTUAL.Rows[AÑOACTUAL.Rows.Count - 1]["PORCABS"].ToString(); //Valor medio
            string ultimovalorNOCONFORMIDADES = AÑOACTUAL.Rows[Convert.ToInt32(DateTime.Now.ToString("MM")) - 1]["PORCABS"].ToString();

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
            List<string> series4 = new List<string>();
            series4.Add(ObjetivoAnualNOCONFORMIDADES);
            series4.Add(ObjetivoMensualNOCONFORMIDADES);
            chartData.Add(series4);
            return chartData;
        }

    }
}
