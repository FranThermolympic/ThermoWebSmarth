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
    public partial class KPI_WTOP_Pegamento : System.Web.UI.Page
    {

        private static DataSet ds_Consumo_Bidon = new DataSet();
        
        /*protected GridView dgv_ListaFichasParam;*/

        protected void Page_Load(object sender, EventArgs e)
        {           
            if (!IsPostBack)
            {
                //testJSON();
                              
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                Conexion_KPI KPIConexion = new Conexion_KPI();
                ds_Consumo_Bidon = KPIConexion.KPI_TiempoEntreParo();
                dgv_KPI_Mensual.DataSource = ds_Consumo_Bidon;
                dgv_KPI_Mensual.DataBind();
                
            }

        }
      
      
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                try
                {
                    // Obtiene el índice de la fila actual
                    int rowIndex = e.Row.RowIndex;
                    //Miro la datatable vinculada
                    DataSet dt = ((DataSet)dgv_KPI_Mensual.DataSource);

                    if (rowIndex < dt.Tables[0].Rows.Count - 1)
                    {
                        // Obtiene el valor de la celda actual en la columna deseada
                        string valorActual = dt.Tables[0].Rows[rowIndex]["INICIO"].ToString(); // Cambia el índice de la celda según tus necesidades
                        // Obtiene el valor de la celda en la fila siguiente en la misma columna
                        string valorSiguiente = dt.Tables[0].Rows[rowIndex + 1]["HORAFIN"].ToString();

                        /*
                         *   Label InicioCambio = (Label)e.Row.FindControl("lblIniCam");
                    Label FinCambio = (Label)e.Row.FindControl("lblFinCam");
                    Label HorasBidon = (Label)e.Row.FindControl("lblHoras");
                    Label PiezasBidon = (Label)e.Row.FindControl("lblPiezas");
                         */

                        Conexion_KPI conexion = new Conexion_KPI();
                        DataSet AuxBidon = conexion.KPI_PiezasXTrend("WTOP", valorSiguiente, valorActual);
                        e.Row.Cells[3].Text = AuxBidon.Tables[0].Rows[0]["PRODUCIENDO"].ToString();
                        e.Row.Cells[4].Text = AuxBidon.Tables[0].Rows[0]["PIEZAS"].ToString();
                    }


                    /*
                   Conexion_SMARTH SHConexion = new Conexion_SMARTH();

                   string FILTRO_2 = GridIncidenciasMES.DataKeys[e.Row.RowIndex].Value.ToString();
                   //Label AccionConteo = (Label)e.Row.FindControl("lblAcciones");
                   //
                   GridView gvOrders = e.Row.FindControl("GridIncidenciasMESDetalle") as GridView;
                   gvOrders.DataSource = SHConexion.Devuelve_KPI_Incidencias_GP12_DETALLE(Selecaño.SelectedValue, SelecMes.SelectedValue, FILTRO_2);
                   gvOrders.DataBind();
                   //DataTable AuxGVORDERS = gvOrders.DataSource as DataTable;
                   */
                }
                catch (Exception ex)
                { }
            }
        }

       /*
        [WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static string GetChartGeneral(string periodo)
        {
            string db = "2021";
            if (Convert.ToInt32(periodo) < 2022)
            {
                db = Convert.ToString(periodo);
            }
            string año = Convert.ToString(periodo);

            Conexion_SMARTH SHConexion = new Conexion_SMARTH();
            //string ChartEjemplo = SHConexion.Devuelve_KPI_GP12_Mensual_CHART(año, db);
            string ChartEjemplo = SHConexion.Devuelve_KPI_GP12_Mensual_CHARTPERC(año, db);
            ChartGeneral = ChartEjemplo.Split(new char[] { '¬' });
            return ChartGeneral[0];

        }

        [WebMethod]
        public static string GetChartGeneralCostes(string periodo)
        {            
            return ChartGeneral[1];

        }



        public List<string> GetChartnew(string periodo)
        {
            string db = "2021";
            if (Convert.ToInt32(periodo) < 2022)
            {
                db = Convert.ToString(periodo);
            }
            string año = Convert.ToString(periodo);

            Conexion_SMARTH SHConexion = new Conexion_SMARTH();
            //string ChartEjemplo = SHConexion.Devuelve_KPI_GP12_Mensual_CHART2(año, db);

            //var Bloques = new List<string> {  };
            //Bloques = SHConexion.Devuelve_KPI_GP12_Mensual_CHART3(año, db);
            //Bloques.Add(ChartEjemplo);
            //List<string> iData = new List<string>();
            //iData.Add(SHConexion.Devuelve_KPI_GP12_Mensual_CHART3(año, db));
            List<string> iData = SHConexion.Devuelve_KPI_GP12_Mensual_CHART3(año, db);
            return iData;
        }

        */
       
       
    }
}

