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
    public partial class KPI_Mantenimiento : System.Web.UI.Page
    {
        private static DataSet ds_KPI_Resultados_Mantenimiento_MAQ = new DataSet();
        private static DataSet ds_KPI_Totales_Mantenimiento_MAQ = new DataSet();

        private static DataSet ds_KPI_Resultados_Mantenimiento_MOL = new DataSet();
        private static DataSet ds_KPI_Totales_Mantenimiento_MOL = new DataSet();

        private static DataSet ds_KPI_Ranking_Apertura_MOL = new DataSet();
        private static DataSet ds_KPI_Ranking_Apertura_MAQ = new DataSet();
        private static DataSet ds_KPI_Ranking_Apertura_MAQAÑO = new DataSet();
        private static DataSet ds_KPI_Ranking_Apertura_MOLAÑO = new DataSet();

        private static DataSet ds_KPI_Ranking_MAQxParte = new DataSet();
        private static DataSet ds_KPI_Ranking_MAQxParteMTBF = new DataSet();
        private static DataSet ds_KPI_Ranking_MAQxParteAÑO = new DataSet();
        private static DataSet ds_KPI_Ranking_MAQxParteAÑOMTBF = new DataSet();

        private static DataSet ds_KPI_Ranking_MOLxParte = new DataSet();
        private static DataSet ds_KPI_Ranking_MOLxParteMTBF = new DataSet();

        private static DataSet ds_KPI_Ranking_MOLxParteAÑO = new DataSet();
        private static DataSet ds_KPI_Ranking_MOLxParteAÑOMTBF = new DataSet();

        private static DataSet ds_KPI_TiposMantAÑO = new DataSet();




        protected void Page_Load(object sender, EventArgs e)
        {           
            if (!IsPostBack)
            {

                Selecaño.SelectedValue = Convert.ToString(DateTime.Now.Year);
                SelecMes.SelectedValue = Convert.ToString(DateTime.Now.Month);
                SELECMES2.SelectedValue = Convert.ToString(DateTime.Now.Month);
                Cargar_Cajas_Filtros();
                string año = Convert.ToString(Selecaño.SelectedValue);
                
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                /*
                SHConexion.Leer_HorasProductivasFabrica();
                SHConexion.Leer_HorasProductivasMaquina();
                SHConexion.Leer_HorasProductivasMolde();
                */
                ds_KPI_Resultados_Mantenimiento_MAQ = SHConexion.Devuelve_Resultados_Mantenimiento_Maquinas(Convert.ToInt32(año));
                ds_KPI_Resultados_Mantenimiento_MOL = SHConexion.Devuelve_Resultados_Mantenimiento_Moldes(Convert.ToInt32(año));

                ds_KPI_Totales_Mantenimiento_MAQ = SHConexion.Devuelve_TOTAL_Resultados_Mantenimiento_Maquinas(Convert.ToInt32(año));
                ds_KPI_Totales_Mantenimiento_MOL = SHConexion.Devuelve_TOTAL_Resultados_Mantenimiento_Moldes(Convert.ToInt32(año));

                ds_KPI_Ranking_Apertura_MOL = SHConexion.Devuelve_Ranking_AperturaPartes_Moldes(Convert.ToInt32(año), "", "");
                ds_KPI_Ranking_Apertura_MAQ = SHConexion.Devuelve_Ranking_AperturaPartes_Maquinas(Convert.ToInt32(año), "", "");

                ds_KPI_Ranking_Apertura_MAQAÑO = SHConexion.Devuelve_Ranking_AperturaPartes_MaquinasAÑO(Convert.ToInt32(año), "", "");
                ds_KPI_Ranking_Apertura_MOLAÑO = SHConexion.Devuelve_Ranking_AperturaPartes_MOLAÑO(Convert.ToInt32(año), "", "");

                ds_KPI_Ranking_MAQxParte = SHConexion.Devuelve_Ranking_Maquinas_PartesAbiertos(Convert.ToInt32(año), "", "");
                ds_KPI_Ranking_MAQxParteMTBF = SHConexion.Devuelve_Ranking_Maquinas_PartesAbiertosMTBF(Convert.ToInt32(año), "", "");

                ds_KPI_Ranking_MAQxParteAÑO = SHConexion.Devuelve_Ranking_Maquinas_PartesAbiertosAÑO(Convert.ToInt32(año), "", "");
                ds_KPI_Ranking_MAQxParteAÑOMTBF = SHConexion.Devuelve_Ranking_Maquinas_PartesAbiertosAÑOMTBF(Convert.ToInt32(año), "", "");

                ds_KPI_Ranking_MOLxParte = SHConexion.Devuelve_Ranking_Moldes_PartesAbiertos(Convert.ToInt32(año), "", "");
                ds_KPI_Ranking_MOLxParteMTBF = SHConexion.Devuelve_Ranking_Moldes_PartesAbiertosMTBF(Convert.ToInt32(año), "", "");

                ds_KPI_Ranking_MOLxParteAÑO = SHConexion.Devuelve_Ranking_Moldes_PartesAbiertosAÑO(Convert.ToInt32(año), "", "");
                ds_KPI_Ranking_MOLxParteAÑOMTBF = SHConexion.Devuelve_Ranking_Moldes_PartesAbiertosAÑOMTBF(Convert.ToInt32(año), "", "");

                ds_KPI_TiposMantAÑO = SHConexion.Devuelve_Resultados_Detallados_TipoMantenimiento_MOLDES(Convert.ToInt32(año), "");
                lblAñoMaquina.InnerText = DateTime.Now.Year.ToString();
                lblAñoMolde.InnerText = DateTime.Now.Year.ToString();
                rellenar_grid();
            }

        }

        private void Cargar_Cajas_Filtros()
        {
            Conexion_SMARTH SHConexion = new Conexion_SMARTH();
            lista_trabajos.Items.Clear();
            DataTable AuxiliaresMoldes = SHConexion.Devuelve_Auxiliares_Mantenimiento(); //Lista de dropdowns

            AuxiliaresMoldes.DefaultView.RowFilter = "TipoMantenimientoMolde is not null";
            DataTable tipo_trabajo = AuxiliaresMoldes.DefaultView.ToTable();
            foreach (DataRow row in tipo_trabajo.Rows)
            {
                lista_trabajos.Items.Add(row["TipoMantenimientoMolde"].ToString());
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

                GridRankingMAQ.DataSource = ds_KPI_Ranking_Apertura_MAQ;
                GridRankingMAQ.DataBind();

                GridRankingMAQAÑO.DataSource = ds_KPI_Ranking_Apertura_MAQAÑO;
                GridRankingMAQAÑO.DataBind();

                GridRankingMAQxParte.DataSource = ds_KPI_Ranking_MAQxParte;
                GridRankingMAQxParte.DataBind();

                GridRankingMAQxParteMTBF.DataSource = ds_KPI_Ranking_MAQxParteMTBF;
                GridRankingMAQxParteMTBF.DataBind();

                GridRankingMAQxParteAÑOMTBF.DataSource = ds_KPI_Ranking_MAQxParteAÑOMTBF;
                GridRankingMAQxParteAÑOMTBF.DataBind();


                GridRankingMAQxParteAÑO.DataSource = ds_KPI_Ranking_MAQxParteAÑO;
                GridRankingMAQxParteAÑO.DataBind();

                //MOLDES
                GridResultadosMolde.DataSource = ds_KPI_Resultados_Mantenimiento_MOL;
                GridResultadosMolde.DataBind();

                GridDetallesMolCORR.DataSource = ds_KPI_Resultados_Mantenimiento_MOL;
                GridDetallesMolCORR.DataBind();

                GridDetallesMolPREV.DataSource = ds_KPI_Resultados_Mantenimiento_MOL;
                GridDetallesMolPREV.DataBind();

                GridRankingMOL.DataSource = ds_KPI_Ranking_Apertura_MOL;
                GridRankingMOL.DataBind();

                GridRankingMOLAÑO.DataSource = ds_KPI_Ranking_Apertura_MOLAÑO;
                GridRankingMOLAÑO.DataBind();

                GridRankingMOLxParte.DataSource = ds_KPI_Ranking_MOLxParte;
                GridRankingMOLxParte.DataBind();

                GridRankingMOLxParteMTBF.DataSource = ds_KPI_Ranking_MOLxParteMTBF;
                GridRankingMOLxParteMTBF.DataBind();

                GridRankingMOLxParteAÑO.DataSource = ds_KPI_Ranking_MOLxParteAÑO;
                GridRankingMOLxParteAÑO.DataBind();

                GridRankingMOLxParteAÑOMTBF.DataSource = ds_KPI_Ranking_MOLxParteAÑOMTBF;
                GridRankingMOLxParteAÑOMTBF.DataBind();
                

                GridTipoMantenmientoAño.DataSource = ds_KPI_TiposMantAÑO;
                GridTipoMantenmientoAño.DataBind();

                //TOTALES
                KPICosteTotalMAQ.InnerText = ds_KPI_Totales_Mantenimiento_MAQ.Tables[0].Rows[0]["COSTETOTALES"].ToString();
                KPIHorasMAQCORR.InnerText = ds_KPI_Totales_Mantenimiento_MAQ.Tables[0].Rows[0]["REALESMAQ"].ToString();
                KPIHorasMAQPREV.InnerText = ds_KPI_Totales_Mantenimiento_MAQ.Tables[0].Rows[0]["REALESPREV"].ToString();
                KPIPartesMAQ.InnerText = ds_KPI_Totales_Mantenimiento_MAQ.Tables[0].Rows[0]["PARTES"].ToString();

                KPICosteTotalMOL.InnerText = ds_KPI_Totales_Mantenimiento_MOL.Tables[0].Rows[0]["COSTETOTALES"].ToString();
                KPIHorasMOLCORR.InnerText = ds_KPI_Totales_Mantenimiento_MOL.Tables[0].Rows[0]["REALESMAQ"].ToString();
                KPIHorasMOLPREV.InnerText = ds_KPI_Totales_Mantenimiento_MOL.Tables[0].Rows[0]["REALESPREV"].ToString();
                KPIPartesMOL.InnerText = ds_KPI_Totales_Mantenimiento_MOL.Tables[0].Rows[0]["PARTES"].ToString();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "CargarCharts('" + Selecaño.SelectedValue + "', '" + SelecMes.SelectedValue + "');", true);

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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "CargarCharts('" + Selecaño.SelectedValue + "', '" + SelecMes.SelectedValue + "');", true);
            }
        }
        public void cargar_tablas(object sender, EventArgs e)
        {
            try
            {
                string año = Convert.ToString(Selecaño.SelectedValue);              
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();

                ds_KPI_Resultados_Mantenimiento_MAQ = SHConexion.Devuelve_Resultados_Mantenimiento_Maquinas(Convert.ToInt32(año));
                ds_KPI_Resultados_Mantenimiento_MOL = SHConexion.Devuelve_Resultados_Mantenimiento_Moldes(Convert.ToInt32(año));
                ds_KPI_Ranking_Apertura_MOL = SHConexion.Devuelve_Ranking_AperturaPartes_Moldes(Convert.ToInt32(año), " AND MES = '" + Convert.ToString(SELECMES2.SelectedValue) + "'", "");
                ds_KPI_Ranking_Apertura_MAQ = SHConexion.Devuelve_Ranking_AperturaPartes_Maquinas(Convert.ToInt32(año), " AND MES = '" + Convert.ToString(SelecMes.SelectedValue) + "'", "");
                ds_KPI_Totales_Mantenimiento_MAQ = SHConexion.Devuelve_TOTAL_Resultados_Mantenimiento_Maquinas(Convert.ToInt32(año));
                ds_KPI_Totales_Mantenimiento_MOL = SHConexion.Devuelve_TOTAL_Resultados_Mantenimiento_Moldes(Convert.ToInt32(año));

                ds_KPI_Ranking_MAQxParte = SHConexion.Devuelve_Ranking_Maquinas_PartesAbiertos(Convert.ToInt32(año), " AND MES = '" + Convert.ToString(SelecMes.SelectedValue) + "'", "");
                ds_KPI_Ranking_MAQxParteMTBF = SHConexion.Devuelve_Ranking_Maquinas_PartesAbiertosMTBF(Convert.ToInt32(año), " AND NUM.MES = '" + Convert.ToString(SelecMes.SelectedValue) + "'", "");

                ds_KPI_Ranking_Apertura_MAQAÑO = SHConexion.Devuelve_Ranking_AperturaPartes_MaquinasAÑO(Convert.ToInt32(año), "", "");
                ds_KPI_Ranking_MAQxParteAÑO = SHConexion.Devuelve_Ranking_Maquinas_PartesAbiertosAÑO(Convert.ToInt32(año), "", "");
                ds_KPI_Ranking_MAQxParteAÑOMTBF = SHConexion.Devuelve_Ranking_Maquinas_PartesAbiertosAÑOMTBF(Convert.ToInt32(año), "", "");

                ds_KPI_Ranking_MOLxParte = SHConexion.Devuelve_Ranking_Moldes_PartesAbiertos(Convert.ToInt32(año), " AND MES = '" + Convert.ToString(SELECMES2.SelectedValue) + "'", "");
                ds_KPI_Ranking_MOLxParteMTBF = SHConexion.Devuelve_Ranking_Moldes_PartesAbiertosMTBF(Convert.ToInt32(año), " AND NUM.MES = '" + Convert.ToString(SELECMES2.SelectedValue) + "'", "");

                ds_KPI_Ranking_Apertura_MOLAÑO = SHConexion.Devuelve_Ranking_AperturaPartes_MOLAÑO(Convert.ToInt32(año), "", "");
                ds_KPI_Ranking_MOLxParteAÑO = SHConexion.Devuelve_Ranking_Moldes_PartesAbiertosAÑO(Convert.ToInt32(año), "", "");
                ds_KPI_Ranking_MOLxParteAÑOMTBF = SHConexion.Devuelve_Ranking_Moldes_PartesAbiertosAÑOMTBF(Convert.ToInt32(año), "", "");
                // string MESOLD = (Convert.ToInt32(SelecMes.SelectedValue) - 1).ToString();

                ds_KPI_TiposMantAÑO = SHConexion.Devuelve_Resultados_Detallados_TipoMantenimiento_MOLDES(Convert.ToInt32(año), " and IdTipoRevision = " + SHConexion.Devuelve_IDTrabajo(lista_trabajos.SelectedValue.ToString()));
                lblAñoMaquina.InnerText = año;
                lblAñoMolde.InnerText = año;

                rellenar_grid();
            }
            catch (Exception ex)
            {

            }
        }

        protected void GridView_DataBoundHist(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label PORCCORRECTIVO = (Label)e.Row.FindControl("lblPorCorrectivo");
                Label PORCPREVENTIVO = (Label)e.Row.FindControl("lblPorPreventivo");
                HiddenField KPICORRECTIVO = (HiddenField)e.Row.FindControl("lblKPICorrectivo");
                HiddenField KPIPREVENTIVO = (HiddenField)e.Row.FindControl("lblKPIPreventivo");

                double PORCORREC = Convert.ToDouble(PORCCORRECTIVO.Text.Remove(PORCCORRECTIVO.Text.Length - 1, 1));
                double PORCPREV = Convert.ToDouble(PORCPREVENTIVO.Text.Remove(PORCPREVENTIVO.Text.Length - 1, 1));
                double KPICOR = Convert.ToDouble(KPICORRECTIVO.Value);
                double KPIPREV = Convert.ToDouble(KPIPREVENTIVO.Value);

                if (PORCORREC > KPICOR)
                {
                    PORCCORRECTIVO.ForeColor = System.Drawing.Color.Red;
                    PORCCORRECTIVO.Font.Bold = true;
                }
                if (PORCPREV < KPIPREV)
                {
                    PORCPREVENTIVO.ForeColor = System.Drawing.Color.Red;
                    PORCPREVENTIVO.Font.Bold = true;
                }
                /*
                Label EstadoEquipo = (Label)e.Row.FindControl("lblEstadoEquipo");
               



                if (lblProxVencido.Text != "")
                {
                    if (Convert.ToDateTime(lblProxVencido.Text) < DateTime.Now)
                    {
                        lblFechaVENC.Visible = true;
                    }

                    if (Convert.ToDateTime(lblProxVencido.Text) < DateTime.Now.AddDays(+45) && lblFechaVENC.Visible == false)
                    {
                        lblVencido45dias.Visible = true;
                    }
                }
                if (lblTipoCalibracion.Text != "N/A" && EstadoEquipo.Text == "0")
                {
                    AlertaEstadoEquipoInactivo.Visible = true;
                    FrecuenciaCalibracion.Visible = false;
                    lblVencido45dias.Visible = false;
                    lblFechaVENC.Visible = false;
                    lblNACAlibracion.Visible = false;
                    lblUltimaCalibracion.Visible = false;
                    lblProximaCalibracion.Visible = false;
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#e7defa");
                }
                if (lblTipoCalibracion.Text != "N/A" && EstadoEquipo.Text == "2")
                {
                    AlertaEstadoEquipoObsoleto.Visible = true;
                    FrecuenciaCalibracion.Visible = false;
                    lblVencido45dias.Visible = false;
                    lblFechaVENC.Visible = false;
                    lblNACAlibracion.Visible = false;
                    lblUltimaCalibracion.Visible = false;
                    lblProximaCalibracion.Visible = false;
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#fcca97");
                }
                if (lblTipoCalibracion.Text == "N/A")
                {
                    AlertaEstadoEquipoObsoleto.Visible = false;
                    FrecuenciaCalibracion.Visible = false;
                    lblVencido45dias.Visible = false;
                    lblFechaVENC.Visible = false;
                    lblNACAlibracion.Visible = true;
                    lblUltimaCalibracion.Visible = false;
                    lblProximaCalibracion.Visible = false;
                }
                */
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


    }
}
