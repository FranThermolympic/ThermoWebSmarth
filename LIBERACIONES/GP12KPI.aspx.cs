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

namespace ThermoWeb.GP12
{
    public partial class GP12KPI : System.Web.UI.Page
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
        /*protected GridView dgv_ListaFichasParam;*/

        protected void Page_Load(object sender, EventArgs e)
        {


            
            if (!IsPostBack)
            {
                Selecaño.SelectedValue = Convert.ToString(DateTime.Now.Year);
                SelecMes.SelectedValue = Convert.ToString(DateTime.Now.Month);
                string año = Convert.ToString(Selecaño.SelectedValue);
                string mes = Convert.ToString(SelecMes.SelectedValue);
                Conexion_GP12 conexion = new Conexion_GP12();
                ds_KPI_Mensual = conexion.devuelve_KPI_GP12_Mensual(año);
                ds_KPI_Operarios = conexion.devuelve_detecciones_operarios(año, mes);
                ds_KPI_Top7_Retrabajo = conexion.devuelve_TOP_PiezasRetrabajadas(año, mes);
                ds_KPI_Top7_Scrap = conexion.devuelve_TOP_PiezasNOK(año, mes);
                ds_KPI_Top7_Coste = conexion.devuelve_TOP_Costes(año, mes);
                ds_KPI_Top7_Horas = conexion.devuelve_TOP_PiezasHoras(año, mes);

                ds_KPI_Top_Operarios_Año = conexion.devuelve_detecciones_operarios_Año(año);
                ds_KPI_Top7_Retrabajo_Año = conexion.devuelve_TOP_PiezasRetrabajadas_Año(año);
                ds_KPI_Top7_Scrap_Año = conexion.devuelve_TOP_PiezasNOK_Año(año);
                ds_KPI_Top7_Coste_Año = conexion.devuelve_TOP_Costes_Año(año);
                ds_KPI_Top7_Horas_Año = conexion.devuelve_TOP_PiezasHoras_Año(año);
                              
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

            }
            catch (Exception ex)
            {
            }
        }
        public void cargar_tablas(object sender, EventArgs e)
        {
            try
            {
                string año = Convert.ToString(Selecaño.SelectedValue);
                string mes = Convert.ToString(SelecMes.SelectedValue);
                Conexion_GP12 conexion = new Conexion_GP12();
                ds_KPI_Mensual = conexion.devuelve_KPI_GP12_Mensual(año);
                ds_KPI_Operarios = conexion.devuelve_detecciones_operarios(año, mes);
                ds_KPI_Top7_Retrabajo = conexion.devuelve_TOP_PiezasRetrabajadas(año, mes);
                ds_KPI_Top7_Scrap = conexion.devuelve_TOP_PiezasNOK(año, mes);
                ds_KPI_Top7_Coste = conexion.devuelve_TOP_Costes(año, mes);
                ds_KPI_Top7_Horas = conexion.devuelve_TOP_PiezasHoras(año, mes);
                ds_KPI_Top_Operarios_Año = conexion.devuelve_detecciones_operarios_Año(año);
                ds_KPI_Top7_Retrabajo_Año = conexion.devuelve_TOP_PiezasRetrabajadas_Año(año);
                ds_KPI_Top7_Scrap_Año = conexion.devuelve_TOP_PiezasNOK_Año(año);
                ds_KPI_Top7_Coste_Año = conexion.devuelve_TOP_Costes_Año(año);
                ds_KPI_Top7_Horas_Año = conexion.devuelve_TOP_PiezasHoras_Año(año);

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
                Response.Redirect("GP12HistoricoReferencia.aspx?REFERENCIA=" + e.CommandArgument.ToString());
            }
        }
    }
}
