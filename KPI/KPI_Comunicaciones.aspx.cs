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
    public partial class KPI_Comunicaciones : System.Web.UI.Page
    {
        private static DataSet ds_Detecciones_Mensual = new DataSet();
        private static DataSet ds_Detecciones_Anual = new DataSet();

        private static DataSet ds_Ranking_Informadores = new DataSet();
        private static DataSet ds_Ranking_Informadores_Mensual = new DataSet();

        private static DataSet ds_Ranking_Operarios = new DataSet();
        private static DataSet ds_Ranking_Operarios_Mensual = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {           
            if (!IsPostBack)
            {

                Selecaño.SelectedValue = Convert.ToString(DateTime.Now.Year);
                SelecMes.SelectedValue = Convert.ToString(DateTime.Now.Month);

                Cargar_tablas(null, null);
                /*
                 string MESOLD = (Convert.ToInt32(SelecMes.SelectedValue) - 1).ToString();
                 string AÑOOLD = Selecaño.SelectedValue.ToString();
                 if (MESOLD == "0")
                    { MESOLD = "12";
                      AÑOOLD = (Convert.ToInt32(Selecaño.SelectedValue) - 1).ToString();
                    }
                */
               
            }

        }
        public void Cargar_tablas(object sender, EventArgs e)
        {
            try
            {

                string año = Convert.ToString(Selecaño.SelectedValue);
                string filtromes = " AND MONTH(C.FechaInicio) = '" + SelecMes.SelectedValue + "'";
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();

                ds_Detecciones_Anual = SHConexion.Devuelve_KPI_ComunicacionesPDTExINFORMADOS_Totales(Selecaño.SelectedValue, "");
                ds_Detecciones_Mensual = SHConexion.Devuelve_KPI_ComunicacionesPDTExINFORMADOS_Totales(Selecaño.SelectedValue, filtromes);

                ds_Ranking_Informadores_Mensual = SHConexion.Devuelve_KPI_ComunicacionesRankingxINFORMADORES_MES(Selecaño.SelectedValue, filtromes);
                ds_Ranking_Informadores = SHConexion.Devuelve_KPI_ComunicacionesRankingxINFORMADORES(Selecaño.SelectedValue);

                ds_Ranking_Operarios_Mensual = SHConexion.Devuelve_KPI_ComunicacionesRankingOPERARIOS_INFORMADOS_MES(Selecaño.SelectedValue, filtromes);
                ds_Ranking_Operarios = SHConexion.Devuelve_KPI_ComunicacionesRankingOPERARIOS_INFORMADOS(Selecaño.SelectedValue);


                /*
                string MESOLD = (Convert.ToInt32(SelecMes.SelectedValue) - 1).ToString();
                string AÑOOLD = Selecaño.SelectedValue.ToString();
                if (MESOLD == "0")
                {
                    MESOLD = "12";
                    AÑOOLD = (Convert.ToInt32(Selecaño.SelectedValue) - 1).ToString();
                }
                */

                Rellenar_grid();
            }
            catch (Exception)
            {

            }
        }

        private void Rellenar_grid()
        {
            try
            {
                GridOperariosAño.DataSource = ds_Ranking_Operarios;
                GridOperariosAño.DataBind();

                GridInformadoresAño.DataSource = ds_Ranking_Informadores;
                GridInformadoresAño.DataBind();

                
                GridOperarios.DataSource = ds_Ranking_Operarios_Mensual;
                GridOperarios.DataBind();

                GridInformadores.DataSource = ds_Ranking_Informadores_Mensual;
                GridInformadores.DataBind();

                KPIDeteccionesMES.InnerText = ds_Detecciones_Mensual.Tables[0].Rows[0]["DETECCIONES"].ToString();
                KPIInformadosMES.InnerText = ds_Detecciones_Mensual.Tables[0].Rows[0]["INFORMADO"].ToString();
                KPINoInformadosMES.InnerText = ds_Detecciones_Mensual.Tables[0].Rows[0]["PENDIENTE"].ToString();

                KPIDeteccionesAÑO.InnerText = ds_Detecciones_Anual.Tables[0].Rows[0]["DETECCIONES"].ToString();
                KPIInformadosAÑO.InnerText = ds_Detecciones_Anual.Tables[0].Rows[0]["INFORMADO"].ToString();
                KPINoInformadosAÑO.InnerText = ds_Detecciones_Anual.Tables[0].Rows[0]["PENDIENTE"].ToString();
                //dgv_KPI_Mensual.DataSource = ds_KPI_Mensual;
                //dgv_KPI_Mensual.DataBind();
                // KPICosteTotalAÑO.InnerText = ds_KPI_Totales_Año.Tables[0].Rows[0]["COSTETOTAL"].ToString() + "€";
                /*
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
                 */
            }
            catch (Exception)
            {
            }
        }
       
        public void ContactsGridView_RowCommand(Object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Redirect")
            {
                Response.Redirect("../GP12/GP12RegistroComunicaciones.aspx?OPERARIO=" + e.CommandArgument.ToString() + "_"+ Selecaño.SelectedValue);
            }
        }
    }
}
