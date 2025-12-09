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
    public partial class KPI_GP12 : System.Web.UI.Page
    {

        private static DataSet ds_KPI_Mensual = new DataSet();
        private static DataSet ds_KPI_Operarios = new DataSet();
        private static DataSet ds_KPI_Top7_Scrap = new DataSet();
        private static DataSet ds_KPI_Top7_Retrabajo = new DataSet();
        private static DataSet ds_KPI_Top7_Coste = new DataSet();
        private static DataSet ds_KPI_Top7_Horas = new DataSet();
        private static DataSet ds_KPI_Top_Operarios_Año = new DataSet();
        private static DataSet ds_KPI_Top7_Scrap_Año = new DataSet();
        private static DataSet ds_KPI_Top7_Retrabajo_Año = new DataSet();
        private static DataSet ds_KPI_Top7_Coste_Año = new DataSet();
        private static DataSet ds_KPI_Top7_Horas_Año = new DataSet();

        private static DataSet ds_KPI_Totales_Año = new DataSet();
        private static DataSet ds_KPI_Totales_Mes = new DataSet();

        private static DataSet ds_Tipología_Revision_Año = new DataSet();
        private static DataSet ds_Tipología_Revision_Mes = new DataSet();

        private static DataSet ds_Incidencia_Revision_Año = new DataSet();
        private static DataSet ds_Incidencia_Revision_Mes = new DataSet();

        private static DataSet ds_Resultados_Cliente_Año = new DataSet();
        private static DataSet ds_Resultados_Cliente_Mes = new DataSet();

        private static string[] ChartGeneral = new string [2];
        /*protected GridView dgv_ListaFichasParam;*/

        protected void Page_Load(object sender, EventArgs e)
        {           
            if (!IsPostBack)
            {
                //testJSON();
                Selecaño.SelectedValue = Convert.ToString(DateTime.Now.Year);
                SelecMes.SelectedValue = Convert.ToString(DateTime.Now.Month);
                

                string db = "2021";
                if (Convert.ToInt32(Selecaño.SelectedValue) < 2022)
                {
                    db = Convert.ToString(Selecaño.SelectedValue);
                }
                string año = Convert.ToString(Selecaño.SelectedValue);
                
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();

                //string mes = Convert.ToString(SelecMes.SelectedValue);
                //string MESELECT = " AND month ([FechaInicio]) = '" + Convert.ToString(SelecMes.SelectedValue) + "'";
                //Conexion_GP12 conexion = new Conexion_GP12();
                //conexion.lee_produccionhistorica_BMS();
                //ds_KPI_Mensual = conexion.devuelve_KPI_GP12_Mensual(año);
                //ds_KPI_Operarios = conexion.devuelve_detecciones_operarios(año, mes);
                //ds_KPI_Top7_Retrabajo = conexion.devuelve_TOP_PiezasRetrabajadas(año, mes);
                //ds_KPI_Top7_Scrap = conexion.devuelve_TOP_PiezasNOK(año, mes);
                //ds_KPI_Top7_Coste = conexion.devuelve_TOP_Costes(año, mes);
                //ds_KPI_Top7_Horas = conexion.devuelve_TOP_PiezasHoras(año, mes);
                //ds_KPI_Top_Operarios_Año = conexion.devuelve_detecciones_operarios_Año(año);
                //ds_KPI_Top7_Retrabajo_Año = conexion.devuelve_TOP_PiezasRetrabajadas_Año(año);
                //ds_KPI_Top7_Scrap_Año = conexion.devuelve_TOP_PiezasNOK_Año(año);
                //ds_KPI_Top7_Coste_Año = conexion.devuelve_TOP_Costes_Año(año);
                //ds_KPI_Top7_Horas_Año = conexion.devuelve_TOP_PiezasHoras_Año(año);
                
                SHConexion.LimpiarTablaHistoria();
                SHConexion.Lee_produccionhistorica_BMS();


                  ds_KPI_Mensual = SHConexion.Devuelve_KPI_GP12_Mensual(año, db);
                  ds_KPI_Operarios = SHConexion.Devuelve_detecciones_operarios(db, " AND month ([FechaInicio]) = '" + Convert.ToString(SelecMes.SelectedValue) + "'", " AND year([FechaInicio]) = '" + Convert.ToString(Selecaño.SelectedValue) + "'");
                  ds_KPI_Top7_Retrabajo = SHConexion.Devuelve_TOP_PiezasRetrabajadas(db, " AND month ([FechaInicio]) = '" + Convert.ToString(SelecMes.SelectedValue) + "'", " AND year([FechaInicio]) = '" + Convert.ToString(Selecaño.SelectedValue) + "'");
                ds_KPI_Top7_Scrap = SHConexion.Devuelve_TOP_PiezasNOK(db, " AND month ([FechaInicio]) = '" + Convert.ToString(SelecMes.SelectedValue) + "'", " AND year([FechaInicio]) = '" + Convert.ToString(Selecaño.SelectedValue) + "'");
                ds_KPI_Top7_Coste = SHConexion.Devuelve_TOP_Costes(db, " AND month ([FechaInicio]) = '" + Convert.ToString(SelecMes.SelectedValue) + "'", " AND year([FechaInicio]) = '" + Convert.ToString(Selecaño.SelectedValue) + "'");
                  ds_KPI_Top7_Horas = SHConexion.Devuelve_TOP_PiezasHoras(db, " AND month ([FechaInicio]) = '" + Convert.ToString(SelecMes.SelectedValue) + "'", " AND year([FechaInicio]) = '" + Convert.ToString(Selecaño.SelectedValue) + "'");

                   ds_KPI_Top_Operarios_Año = SHConexion.Devuelve_detecciones_operarios(db, "", " AND year([FechaInicio]) = '" + Convert.ToString(Selecaño.SelectedValue) + "'");
                   ds_KPI_Top7_Retrabajo_Año = SHConexion.Devuelve_TOP_PiezasRetrabajadas(db, "", " AND year([FechaInicio]) = '" + Convert.ToString(Selecaño.SelectedValue) + "'");
                ds_KPI_Top7_Scrap_Año = SHConexion.Devuelve_TOP_PiezasNOK(db, "", " AND year([FechaInicio]) = '" + Convert.ToString(Selecaño.SelectedValue) + "'");
                   ds_KPI_Top7_Coste_Año = SHConexion.Devuelve_TOP_Costes(db, "", " AND year([FechaInicio]) = '" + Convert.ToString(Selecaño.SelectedValue) + "'"); ;
                   ds_KPI_Top7_Horas_Año = SHConexion.Devuelve_TOP_PiezasHoras(db, "", " AND year([FechaInicio]) = '" + Convert.ToString(Selecaño.SelectedValue) + "'");

                ds_KPI_Totales_Año = SHConexion.Devuelve_totales_KPIGP12(db, " AND year([FechaInicio]) = '" + Convert.ToString(Selecaño.SelectedValue) + "'", "","","");
                string MESOLD = (Convert.ToInt32(SelecMes.SelectedValue) - 1).ToString();
                string AÑOOLD = Selecaño.SelectedValue.ToString();
                if (MESOLD == "0")
                    { MESOLD = "12";
                      AÑOOLD = (Convert.ToInt32(Selecaño.SelectedValue) - 1).ToString();
                    }
                ds_KPI_Totales_Mes = SHConexion.Devuelve_totales_KPIGP12(db, " AND year([FechaInicio]) = '" + Convert.ToString(Selecaño.SelectedValue) + "'", " AND month ([FechaInicio]) = '" + Convert.ToString(SelecMes.SelectedValue) + "'", " AND month ([FechaInicio]) = '" + MESOLD + "'", " AND year([FechaInicio]) = '" + AÑOOLD + "'");

                ds_Tipología_Revision_Mes = SHConexion.Devuelve_KPI_GP12_RazonRevision(Convert.ToString(Selecaño.SelectedValue), " AND month ([FechaInicio]) = '" + Convert.ToString(SelecMes.SelectedValue) + "'");
                ds_Tipología_Revision_Año = SHConexion.Devuelve_KPI_GP12_RazonRevisionAÑO(Convert.ToString(Selecaño.SelectedValue));

                ds_Incidencia_Revision_Mes = SHConexion.Devuelve_KPI_Incidencias_GP12(Convert.ToString(Selecaño.SelectedValue), " AND month ([FechaInicio]) = '" + Convert.ToString(SelecMes.SelectedValue) + "'");
                ds_Incidencia_Revision_Año = SHConexion.Devuelve_KPI_Incidencias_GP12AÑO(Convert.ToString(Selecaño.SelectedValue));

                ds_Resultados_Cliente_Mes = SHConexion.Devuelve_KPI_GP12xCliente(Convert.ToString(Selecaño.SelectedValue), " AND month ([FechaInicio]) = '" + Convert.ToString(SelecMes.SelectedValue) + "'");
                ds_Resultados_Cliente_Año = SHConexion.Devuelve_KPI_GP12xClienteAÑO(Convert.ToString(Selecaño.SelectedValue));


                rellenar_grid();
            }

        }
       
        private void rellenar_grid()
        {
            try
            {
                dgv_KPI_Mensual.DataSource = ds_KPI_Mensual;
                dgv_KPI_Mensual.DataBind();

                dgv_KPI_Operarios.DataSource = ds_KPI_Operarios;
                dgv_KPI_Operarios.DataBind();

                GridNOK.DataSource = ds_KPI_Top7_Scrap;
                GridNOK.DataBind();

                GridRetrabajos.DataSource = ds_KPI_Top7_Retrabajo;
                GridRetrabajos.DataBind();

                GridCoste.DataSource = ds_KPI_Top7_Coste;
                GridCoste.DataBind();

                GridHoras.DataSource = ds_KPI_Top7_Horas;
                GridHoras.DataBind();

                TopDeteccionesAño.DataSource = ds_KPI_Top_Operarios_Año;
                TopDeteccionesAño.DataBind();

                TopChatarraAño.DataSource = ds_KPI_Top7_Scrap_Año;
                TopChatarraAño.DataBind();

                TopRetrabajoAño.DataSource = ds_KPI_Top7_Retrabajo_Año;
                TopRetrabajoAño.DataBind();

                TopHorasAño.DataSource = ds_KPI_Top7_Horas_Año;
                TopHorasAño.DataBind();

                TopCostesAño.DataSource = ds_KPI_Top7_Coste_Año;
                TopCostesAño.DataBind();

                KPICosteTotalAÑO.InnerText = ds_KPI_Totales_Año.Tables[0].Rows[0]["COSTETOTAL"].ToString() + "€";
                KPIHorasRevAÑO.InnerText = ds_KPI_Totales_Año.Tables[0].Rows[0]["HORAS"].ToString();
                KPIScrapAÑO.InnerText = ds_KPI_Totales_Año.Tables[0].Rows[0]["PIEZASNOK"].ToString();
                KPIRetrabajadasAÑO.InnerText = ds_KPI_Totales_Año.Tables[0].Rows[0]["RETRABAJADAS"].ToString();


                ViewState["TotalCosteScrapAÑOSUMAHoras"] = null;
                ViewState["TotalRazonesConteoAÑOHoras"] = null;
                ViewState["TotalRevisionesConteoAÑO"] = null;
                ViewState["TotalCosteTotalAÑOSUMA"] = null;
                GridRazonesAÑO.DataSource = ds_Tipología_Revision_Año;
                GridRazonesAÑO.DataBind();

                
                ViewState["TotalCosteScrapMESSUMAHoras"] = null;
                ViewState["TotalRazonesConteoMESHoras"] = null;
                ViewState["TotalRevisionesConteoMES"] = null;
                ViewState["TotalCosteTotalMESSUMA"] = null;
                GridRazonesMES.DataSource = ds_Tipología_Revision_Mes;
                GridRazonesMES.DataBind();

                GridIncidenciasMES.DataSource = ds_Incidencia_Revision_Mes;
                GridIncidenciasMES.DataBind();

                GridIncidenciasAÑO.DataSource = ds_Incidencia_Revision_Año;
                GridIncidenciasAÑO.DataBind();

                dgv_Resultados_Cliente_Mes.DataSource = ds_Resultados_Cliente_Mes;
                dgv_Resultados_Cliente_Mes.DataBind();

                dgv_Resultados_Cliente_Año.DataSource = ds_Resultados_Cliente_Año;
                dgv_Resultados_Cliente_Año.DataBind();


                KPICosteTotalMES.InnerText = ds_KPI_Totales_Mes.Tables[0].Rows[0]["COSTETOTAL"].ToString()+"€";
                if (Convert.ToInt32(ds_KPI_Totales_Mes.Tables[0].Rows[0]["COSTETOTAL"].ToString()) > Convert.ToInt32(ds_KPI_Totales_Mes.Tables[0].Rows[1]["COSTETOTAL"].ToString()))
                {
                    FootMesCosteTotal.Style.Add("color", "red");
                    FootMesCosteTotal.InnerText = ds_KPI_Totales_Mes.Tables[0].Rows[1]["COSTETOTAL"].ToString() + "€ el mes anterior";
                }
                else
                {
                    FootMesCosteTotal.Style.Add("color", "green");
                    FootMesCosteTotal.InnerText = ds_KPI_Totales_Mes.Tables[0].Rows[1]["COSTETOTAL"].ToString() + "€ el mes anterior";
                }

                KPIHorasRevMES.InnerText = ds_KPI_Totales_Mes.Tables[0].Rows[0]["HORAS"].ToString();
                if (Convert.ToDouble(ds_KPI_Totales_Mes.Tables[0].Rows[0]["HORAS"].ToString()) > Convert.ToDouble(ds_KPI_Totales_Mes.Tables[0].Rows[1]["HORAS"].ToString()))
                {
                    FootMesHoras.Style.Add("color", "red");
                    FootMesHoras.InnerText = ds_KPI_Totales_Mes.Tables[0].Rows[1]["HORAS"].ToString() + " horas el mes anterior";
                }
                else
                {
                    FootMesHoras.Style.Add("color", "green");
                    FootMesHoras.InnerText = ds_KPI_Totales_Mes.Tables[0].Rows[1]["HORAS"].ToString() + " horas el mes anterior";
                }

                KPIScrapMES.InnerText = ds_KPI_Totales_Mes.Tables[0].Rows[0]["PIEZASNOK"].ToString();
                if (Convert.ToDouble(ds_KPI_Totales_Mes.Tables[0].Rows[0]["PIEZASNOK"].ToString()) > Convert.ToDouble(ds_KPI_Totales_Mes.Tables[0].Rows[1]["PIEZASNOK"].ToString()))
                {
                    FootMesMalas.Style.Add("color", "red");
                    FootMesMalas.InnerText = ds_KPI_Totales_Mes.Tables[0].Rows[1]["PIEZASNOK"].ToString() + " malas el mes anterior";
                }
                else
                {
                    FootMesMalas.Style.Add("color", "green");
                    FootMesMalas.InnerText = ds_KPI_Totales_Mes.Tables[0].Rows[1]["PIEZASNOK"].ToString() + " malas el mes anterior";
                }
                
                KPIRetrabajadasMES.InnerText = ds_KPI_Totales_Mes.Tables[0].Rows[0]["RETRABAJADAS"].ToString();
                if (Convert.ToDouble(ds_KPI_Totales_Mes.Tables[0].Rows[0]["RETRABAJADAS"].ToString()) > Convert.ToDouble(ds_KPI_Totales_Mes.Tables[0].Rows[1]["RETRABAJADAS"].ToString()))
                {
                    FootMesRetrabajadas.Style.Add("color", "red");
                    FootMesRetrabajadas.InnerText = ds_KPI_Totales_Mes.Tables[0].Rows[1]["RETRABAJADAS"].ToString() + " retrabajadas el mes anterior";
                }
                else
                {
                    FootMesRetrabajadas.Style.Add("color", "green");
                    FootMesRetrabajadas.InnerText = ds_KPI_Totales_Mes.Tables[0].Rows[1]["RETRABAJADAS"].ToString() + " retrabajadas el mes anterior";
                }

                
                  
                   
            }
            catch (Exception EX)
            {
            }
        }
        public void cargar_tablas(object sender, EventArgs e)
        {
            try
            {
                string db = "2021";
                if (Convert.ToInt32(Selecaño.SelectedValue) < 2022)
                {
                    db = Convert.ToString(Selecaño.SelectedValue);
                }
                string año = Convert.ToString(Selecaño.SelectedValue);              
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                  ds_KPI_Mensual = SHConexion.Devuelve_KPI_GP12_Mensual(año, db);
                  ds_KPI_Operarios = SHConexion.Devuelve_detecciones_operarios(db, " AND month ([FechaInicio]) = '" + Convert.ToString(SelecMes.SelectedValue) + "'", " AND year([FechaInicio]) = '" + Convert.ToString(Selecaño.SelectedValue) + "'");
                  ds_KPI_Top7_Retrabajo = SHConexion.Devuelve_TOP_PiezasRetrabajadas(db, " AND month ([FechaInicio]) = '" + Convert.ToString(SelecMes.SelectedValue) + "'", " AND year([FechaInicio]) = '" + Convert.ToString(Selecaño.SelectedValue) + "'");
                  ds_KPI_Top7_Scrap = SHConexion.Devuelve_TOP_PiezasNOK(db, " AND month ([FechaInicio]) = '" + Convert.ToString(SelecMes.SelectedValue) + "'", " AND year([FechaInicio]) = '" + Convert.ToString(Selecaño.SelectedValue) + "'");
                  ds_KPI_Top7_Coste = SHConexion.Devuelve_TOP_Costes(db, " AND month ([FechaInicio]) = '" + Convert.ToString(SelecMes.SelectedValue) + "'", " AND year([FechaInicio]) = '" + Convert.ToString(Selecaño.SelectedValue) + "'");
                  ds_KPI_Top7_Horas = SHConexion.Devuelve_TOP_PiezasHoras(db, " AND month ([FechaInicio]) = '" + Convert.ToString(SelecMes.SelectedValue) + "'", " AND year([FechaInicio]) = '" + Convert.ToString(Selecaño.SelectedValue) + "'");

                  ds_KPI_Top_Operarios_Año = SHConexion.Devuelve_detecciones_operarios(db, "", " AND year([FechaInicio]) = '" + Convert.ToString(Selecaño.SelectedValue) + "'");
                  ds_KPI_Top7_Retrabajo_Año = SHConexion.Devuelve_TOP_PiezasRetrabajadas(db, "", " AND year([FechaInicio]) = '" + Convert.ToString(Selecaño.SelectedValue) + "'");
                  ds_KPI_Top7_Scrap_Año = SHConexion.Devuelve_TOP_PiezasNOK(db, "", " AND year([FechaInicio]) = '" + Convert.ToString(Selecaño.SelectedValue) + "'");
                  ds_KPI_Top7_Coste_Año = SHConexion.Devuelve_TOP_Costes(db, "", " AND year([FechaInicio]) = '" + Convert.ToString(Selecaño.SelectedValue) + "'"); ;
                  ds_KPI_Top7_Horas_Año = SHConexion.Devuelve_TOP_PiezasHoras(db, "", " AND year([FechaInicio]) = '" + Convert.ToString(Selecaño.SelectedValue) + "'");

                ds_Tipología_Revision_Mes = SHConexion.Devuelve_KPI_GP12_RazonRevision(Convert.ToString(Selecaño.SelectedValue), " AND month ([FechaInicio]) = '" + Convert.ToString(SelecMes.SelectedValue) + "'");
                ds_Tipología_Revision_Año = SHConexion.Devuelve_KPI_GP12_RazonRevisionAÑO(Convert.ToString(Selecaño.SelectedValue));

                ds_Incidencia_Revision_Mes = SHConexion.Devuelve_KPI_Incidencias_GP12(Convert.ToString(Selecaño.SelectedValue), " AND month ([FechaInicio]) = '" + Convert.ToString(SelecMes.SelectedValue) + "'");
                ds_Incidencia_Revision_Año = SHConexion.Devuelve_KPI_Incidencias_GP12AÑO(Convert.ToString(Selecaño.SelectedValue));

                ds_Resultados_Cliente_Mes = SHConexion.Devuelve_KPI_GP12xCliente(Convert.ToString(Selecaño.SelectedValue), " AND month ([FechaInicio]) = '" + Convert.ToString(SelecMes.SelectedValue) + "'");
                ds_Resultados_Cliente_Año = SHConexion.Devuelve_KPI_GP12xClienteAÑO(Convert.ToString(Selecaño.SelectedValue));

                ds_KPI_Totales_Año = SHConexion.Devuelve_totales_KPIGP12(db, " AND year([FechaInicio]) = '" + Convert.ToString(Selecaño.SelectedValue) + "'", "", "","");
                string MESOLD = (Convert.ToInt32(SelecMes.SelectedValue) - 1).ToString();
                string AÑOOLD = Selecaño.SelectedValue.ToString();
                if (MESOLD == "0")
                {
                    MESOLD = "12";
                    AÑOOLD = (Convert.ToInt32(Selecaño.SelectedValue) - 1).ToString();
                }
                ds_KPI_Totales_Mes = SHConexion.Devuelve_totales_KPIGP12(db, " AND year([FechaInicio]) = '" + Convert.ToString(Selecaño.SelectedValue) + "'", " AND month ([FechaInicio]) = '" + Convert.ToString(SelecMes.SelectedValue) + "'", " AND month ([FechaInicio]) = '" + MESOLD + "'", " AND year([FechaInicio]) = '" + AÑOOLD + "'");

                
                rellenar_grid();
            }
            catch (Exception)
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
        }

        protected void OnRowDataBoundSECOND(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    Conexion_SMARTH SHConexion = new Conexion_SMARTH();

                    string FILTRO_1 = GridIncidenciasAÑO.DataKeys[e.Row.RowIndex].Value.ToString();
                    //Label AccionConteo = (Label)e.Row.FindControl("lblAcciones");
                    //
                    GridView pnlOrders2 = e.Row.FindControl("GridIncidenciasAÑODetalle") as GridView;
                    pnlOrders2.DataSource = SHConexion.Devuelve_KPI_Incidencias_GP12AÑO_DETALLE(Selecaño.SelectedValue, FILTRO_1);
                    pnlOrders2.DataBind();
                    //DataTable AuxGVORDERS = gvOrders.DataSource as DataTable;
                }
                catch (Exception ex)
                { }
            }
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                try
                {
                   
                    Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                    string FILTRO_2 = GridIncidenciasMES.DataKeys[e.Row.RowIndex].Value.ToString();
                    //Label AccionConteo = (Label)e.Row.FindControl("lblAcciones");
                    //
                    GridView gvOrders = e.Row.FindControl("GridIncidenciasMESDetalle") as GridView;
                    gvOrders.DataSource = SHConexion.Devuelve_KPI_Incidencias_GP12_DETALLE(Selecaño.SelectedValue, SelecMes.SelectedValue, FILTRO_2);
                    gvOrders.DataBind();
                    //DataTable AuxGVORDERS = gvOrders.DataSource as DataTable;

                }
                catch (Exception ex)
                { }
            }
        }

        protected void OnRowDataBoundTHIRD(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                   
                    Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                    string FILTRO_2 = GridRazonesMES.DataKeys[e.Row.RowIndex].Value.ToString();
                    GridView gvOrders3 = e.Row.FindControl("GridRazonesMESDetalle") as GridView;
                    gvOrders3.DataSource = SHConexion.Devuelve_KPI_GP12_RazonRevision_DETALLE(Selecaño.SelectedValue, SelecMes.SelectedValue, FILTRO_2);
                    gvOrders3.DataBind();

                    //SUMA FOOTERS

                    //SUMAHORAS
                    
                    Label lblCantidadHORAS = (Label)e.Row.FindControl("lblRazonesConteoMESHoras");
                    int cantidad = Convert.ToInt32(lblCantidadHORAS.Text);
                    if (ViewState["TotalRazonesConteoMESHoras"] != null)
                    {
                        int totalHoras = (int)ViewState["TotalRazonesConteoMESHoras"];
                        totalHoras += cantidad;
                        ViewState["TotalRazonesConteoMESHoras"] = totalHoras;
                    }
                    else
                    {
                        ViewState["TotalRazonesConteoMESHoras"] = cantidad;
                    }
                    
                    //SUMAREVISIONES

                    Label lblNUMRevisiones = (Label)e.Row.FindControl("lblRevisionesConteoMES");
                    int cantidadRevisiones = Convert.ToInt32(lblNUMRevisiones.Text);
                    if (ViewState["TotalRevisionesConteoMES"] != null)
                    {
                        int totalReviones = (int)ViewState["TotalRevisionesConteoMES"];
                        totalReviones += cantidadRevisiones;
                        ViewState["TotalRevisionesConteoMES"] = totalReviones;
                    }
                    else
                    {
                        ViewState["TotalRevisionesConteoMES"] = cantidadRevisiones;
                    }

                   
                    //SUMACOSTESCRAP
                    Label lblCosteScrapMESSUMAHoras = (Label)e.Row.FindControl("lblCosteScrapMES");
                    double cantidadSCRAP = Convert.ToDouble(lblCosteScrapMESSUMAHoras.Text.Remove(lblCosteScrapMESSUMAHoras.Text.Length - 1, 1));                 
                    if (ViewState["TotalCosteScrapMESSUMAHoras"] != null)
                    {
                        double totalSCRAP = (double)ViewState["TotalCosteScrapMESSUMAHoras"];
                        totalSCRAP += cantidadSCRAP;
                        ViewState["TotalCosteScrapMESSUMAHoras"] = totalSCRAP;
                    }
                    else
                    {
                        ViewState["TotalCosteScrapMESSUMAHoras"] = cantidadSCRAP;
                    }

                    //SUMACOSTETOTALES
                    Label lblCosteTotalMESSUMA = (Label)e.Row.FindControl("lblCosteTotalMES");
                    double cantidadTotalRevision = Convert.ToDouble(lblCosteTotalMESSUMA.Text.Remove(lblCosteTotalMESSUMA.Text.Length - 1, 1));
                    if (ViewState["TotalCosteTotalMESSUMA"] != null)
                    {
                        double CosteTotalRevision = (double)ViewState["TotalCosteTotalMESSUMA"];
                        CosteTotalRevision += cantidadTotalRevision;
                        ViewState["TotalCosteTotalMESSUMA"] = CosteTotalRevision;
                    }
                    else
                    {
                        ViewState["TotalCosteTotalMESSUMA"] = cantidadTotalRevision;
                    }

                }
                catch (Exception ex)
                { }
            }
            
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                // Mostrar el total en el footer

                //SUMAHORAS
                Label lblTotalHoras = (Label)e.Row.FindControl("lblRazonesConteoMESSUMAHoras");
                if (ViewState["TotalRazonesConteoMESHoras"] != null)
                {
                    int totalHoras = (int)ViewState["TotalRazonesConteoMESHoras"];
                    lblTotalHoras.Text = totalHoras.ToString();
                }

                //SUMACANTIDADREVISIONES
                Label lblTotalCantidadRevisiones = (Label)e.Row.FindControl("lblRevisionesConteoMESSUMAHoras");
                if (ViewState["TotalRevisionesConteoMES"] != null)
                {
                    int totalCantidadRevisiones = (int)ViewState["TotalRevisionesConteoMES"];
                    lblTotalCantidadRevisiones.Text = totalCantidadRevisiones.ToString();
                }

                //SUMACOSTESCRAP
                Label lblTotalScrap = (Label)e.Row.FindControl("lblCosteScrapMESSUMAHoras");
                if (ViewState["TotalCosteScrapMESSUMAHoras"] != null)
                {
                    double totalScrap = (double)ViewState["TotalCosteScrapMESSUMAHoras"];
                    lblTotalScrap.Text = totalScrap.ToString() + "€";
                }

                //SUMATOTALREVISION
                Label lblTotalRevision = (Label)e.Row.FindControl("lblCosteTotalMESSUMA");
                if (ViewState["TotalCosteTotalMESSUMA"] != null)
                {
                    double totalCoste = (double)ViewState["TotalCosteTotalMESSUMA"];
                    lblTotalRevision.Text = totalCoste.ToString() + "€";
                }
            }
        }

        protected void OnRowDataBoundFOURTH(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    Conexion_SMARTH SHConexion = new Conexion_SMARTH();

                    string FILTRO_1 = GridRazonesAÑO.DataKeys[e.Row.RowIndex].Value.ToString();
                    //Label AccionConteo = (Label)e.Row.FindControl("lblAcciones");
                    //
                    GridView pnlOrders4 = e.Row.FindControl("GridRazonesAÑODetalle") as GridView;
                    pnlOrders4.DataSource = SHConexion.Devuelve_KPI_GP12_RazonRevisionAÑO_DETALLE(Selecaño.SelectedValue, FILTRO_1);
                    pnlOrders4.DataBind();
                    //DataTable AuxGVORDERS = gvOrders.DataSource as DataTable;

                    
                   

                    //SUMA FOOTERS

                    //SUMAHORAS

                    Label lblCantidadHORAS = (Label)e.Row.FindControl("lblRazonesConteoAÑOHoras");
                    int cantidad = Convert.ToInt32(lblCantidadHORAS.Text);
                    if (ViewState["TotalRazonesConteoAÑOHoras"] != null)
                    {
                        int totalHoras = (int)ViewState["TotalRazonesConteoAÑOHoras"];
                        totalHoras += cantidad;
                        ViewState["TotalRazonesConteoAÑOHoras"] = totalHoras;
                    }
                    else
                    {
                        ViewState["TotalRazonesConteoAÑOHoras"] = cantidad;
                    }

                    //SUMAREVISIONES

                    Label lblNUMRevisiones = (Label)e.Row.FindControl("lblRazonesConteoAño");
                    int cantidadRevisiones = Convert.ToInt32(lblNUMRevisiones.Text);
                    if (ViewState["TotalRevisionesConteoAÑO"] != null)
                    {
                        int totalReviones = (int)ViewState["TotalRevisionesConteoAÑO"];
                        totalReviones += cantidadRevisiones;
                        ViewState["TotalRevisionesConteoAÑO"] = totalReviones;
                    }
                    else
                    {
                        ViewState["TotalRevisionesConteoAÑO"] = cantidadRevisiones;
                    }


                    //SUMACOSTESCRAP
                    Label lblCosteScrapMESSUMAHoras = (Label)e.Row.FindControl("lblCosteScrapAÑO");
                    double cantidadSCRAP = Convert.ToDouble(lblCosteScrapMESSUMAHoras.Text.Remove(lblCosteScrapMESSUMAHoras.Text.Length - 1, 1));
                    if (ViewState["TotalCosteScrapAÑOSUMAHoras"] != null)
                    {
                        double totalSCRAP = (double)ViewState["TotalCosteScrapAÑOSUMAHoras"];
                        totalSCRAP += cantidadSCRAP;
                        ViewState["TotalCosteScrapAÑOSUMAHoras"] = totalSCRAP;
                    }
                    else
                    {
                        ViewState["TotalCosteScrapAÑOSUMAHoras"] = cantidadSCRAP;
                    }

                    //SUMACOSTETOTALES
                    Label lblCosteTotalMESSUMA = (Label)e.Row.FindControl("lblCosteTotalAÑO");
                    double cantidadTotalRevision = Convert.ToDouble(lblCosteTotalMESSUMA.Text.Remove(lblCosteTotalMESSUMA.Text.Length - 1, 1));
                    if (ViewState["TotalCosteTotalAÑOSUMA"] != null)
                    {
                        double CosteTotalRevision = (double)ViewState["TotalCosteTotalAÑOSUMA"];
                        CosteTotalRevision += cantidadTotalRevision;
                        ViewState["TotalCosteTotalAÑOSUMA"] = CosteTotalRevision;
                    }
                    else
                    {
                        ViewState["TotalCosteTotalAÑOSUMA"] = cantidadTotalRevision;
                    }
                }
                catch (Exception ex)
                { }
            }

            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                // Mostrar el total en el footer

                //SUMAHORAS
                Label lblTotalHoras = (Label)e.Row.FindControl("lblRazonesConteoAÑOSUMAHoras");
                if (ViewState["TotalRazonesConteoAÑOHoras"] != null)
                {
                    int totalHoras = (int)ViewState["TotalRazonesConteoAÑOHoras"];
                    lblTotalHoras.Text = totalHoras.ToString();
                }

                //SUMACANTIDADREVISIONES
                Label lblTotalCantidadRevisiones = (Label)e.Row.FindControl("lblRevisionesConteoAÑOSUMAHoras");
                if (ViewState["TotalRevisionesConteoAÑO"] != null)
                {
                    int totalCantidadRevisiones = (int)ViewState["TotalRevisionesConteoAÑO"];
                    lblTotalCantidadRevisiones.Text = totalCantidadRevisiones.ToString();
                }

                //SUMACOSTESCRAP
                Label lblTotalScrap = (Label)e.Row.FindControl("lblCosteScrapAÑOSUMAHoras");
                if (ViewState["TotalCosteScrapAÑOSUMAHoras"] != null)
                {
                    double totalScrap = (double)ViewState["TotalCosteScrapAÑOSUMAHoras"];
                    lblTotalScrap.Text = totalScrap.ToString() + "€";
                }

                //SUMATOTALREVISION
                Label lblTotalRevision = (Label)e.Row.FindControl("lblCosteTotalAÑOSUMA");
                if (ViewState["TotalCosteTotalAÑOSUMA"] != null)
                {
                    double totalCoste = (double)ViewState["TotalCosteTotalAÑOSUMA"];
                    lblTotalRevision.Text = totalCoste.ToString() + "€";
                }
            }
        }

       
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


       
       
    }
}

