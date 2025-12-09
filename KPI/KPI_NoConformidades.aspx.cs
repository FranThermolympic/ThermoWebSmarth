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
    public partial class KPI_NoConformidades : System.Web.UI.Page
    {

        private static DataSet ds_KPI_Mensual = new DataSet();
        private static DataSet ds_KPI_CostesMensual = new DataSet();
        /*
        private static DataSet ds_KPI_Operarios = new DataSet();
        private static DataSet ds_KPI_Top7_Scrap = new DataSet();
        private static DataSet ds_KPI_Top7_Retrabajo = new DataSet();
        private static DataSet ds_KPI_Top7_Coste = new DataSet();
        private static DataSet ds_KPI_Top7_Horas = new DataSet();
        private static DataSet ds_KPI_Top_Operarios_Año = new DataSet();
        private static DataSet ds_KPI_Top7_Scrap_Año = new DataSet();
        private static DataSet ds_KPI_Top7_Retrabajo_Año = new DataSet();
        private static DataSet ds_KPI_Top7_Coste_Año = new DataSet();
        private static DataSet ds_KPI_Top7_Horas_Año = new DataSet();*/
        /*protected GridView dgv_ListaFichasParam;*/

        protected void Page_Load(object sender, EventArgs e)
        {



            if (!IsPostBack)
            {

                Selecaño.SelectedValue = Convert.ToString(DateTime.Now.Year);

                string año = Convert.ToString(Selecaño.SelectedValue);

                Conexion_SMARTH SHConexion = new Conexion_SMARTH();

                //Conexion_CALIDAD conexion = new Conexion_CALIDAD();
                //SHConexion.LimpiarTablaPiezasEnviadas();

                ds_KPI_Mensual = SHConexion.Devuelve_kpi_Calidad(año,"", " AND [TipoNoConformidad] <> 1");
                ds_KPI_CostesMensual = SHConexion.Devuelve_kpi_Costes_Calidad(año,"");
                rellenar_grid();
            }

        }

        private void rellenar_grid()
        {
            try
            {
                // KPI MENSUAL
                dgv_KPI_Mensual.DataSource = ds_KPI_Mensual;
                dgv_KPI_Mensual.DataBind();

                int LineasDataSet = ds_KPI_Mensual.Tables[0].Rows.Count;

                double totalOficiales = 0;
                foreach (DataRow dr in ds_KPI_Mensual.Tables[0].Rows)
                {
                    totalOficiales += Convert.ToInt32(dr["NCOFICIALES"]);
                }
                Label lbloficiales = (Label)dgv_KPI_Mensual.FooterRow.FindControl("AVGOficiales");
                lbloficiales.Text = (totalOficiales / LineasDataSet).ToString("0.##");
                Label lbloficialestotal = (Label)dgv_KPI_Mensual.FooterRow.FindControl("TotalOficiales");
                lbloficialestotal.Text = totalOficiales.ToString();


                double TotalQInfo = 0;
                foreach (DataRow dr in ds_KPI_Mensual.Tables[0].Rows)
                {
                    TotalQInfo += Convert.ToInt32(dr["QINFO"]);
                }
                Label lblqinfo = (Label)dgv_KPI_Mensual.FooterRow.FindControl("AVGQInfo");
                lblqinfo.Text = (TotalQInfo / LineasDataSet).ToString("0.##");
                Label lblquinfototal = (Label)dgv_KPI_Mensual.FooterRow.FindControl("SumaQInfo");
                lblquinfototal.Text = TotalQInfo.ToString();


                double TotalProveedor = 0;
                foreach (DataRow dr in ds_KPI_Mensual.Tables[0].Rows)
                {
                    TotalProveedor += Convert.ToInt32(dr["PROVEEDOR"]);
                }
                Label lblproveedor = (Label)dgv_KPI_Mensual.FooterRow.FindControl("AVGOProveedor");
                lblproveedor.Text = (TotalProveedor / LineasDataSet).ToString("0.##");
                Label lblproveedortotal = (Label)dgv_KPI_Mensual.FooterRow.FindControl("SumaProveedor");
                lblproveedortotal.Text = TotalProveedor.ToString();

                int TotalEnviadas = 0;
                foreach (DataRow dr in ds_KPI_Mensual.Tables[0].Rows)
                {
                    TotalEnviadas += Convert.ToInt32(dr["CantidadEnviada"]);
                }
                Label lblenviada = (Label)dgv_KPI_Mensual.FooterRow.FindControl("AVGEnviada");
                lblenviada.Text = (TotalEnviadas / LineasDataSet).ToString("0,0.##");
                Label lblenviadatotal = (Label)dgv_KPI_Mensual.FooterRow.FindControl("SUMEnviada");
                lblenviadatotal.Text = TotalEnviadas.ToString("0,0.##");

                double TotalRechazadas = 0;
                foreach (DataRow dr in ds_KPI_Mensual.Tables[0].Rows)
                {
                    TotalRechazadas += Convert.ToInt32(dr["PIEZASNOK"]);
                }
                Label lblrechazadas = (Label)dgv_KPI_Mensual.FooterRow.FindControl("AVGNOK");
                lblrechazadas.Text = (TotalRechazadas / LineasDataSet).ToString("0,0");
                Label lblrechazadastotal = (Label)dgv_KPI_Mensual.FooterRow.FindControl("SUMNOK");
                lblrechazadastotal.Text = TotalRechazadas.ToString("0,0.##");

                Label lblPPM = (Label)dgv_KPI_Mensual.FooterRow.FindControl("AVGPPM");
                lblPPM.Text = ((TotalRechazadas * 1000000) / TotalEnviadas).ToString("0,0.##");

                //KPI Costes
                dgv_CostesNoCalidad.DataSource = ds_KPI_CostesMensual;
                dgv_CostesNoCalidad.DataBind();
                int LineasDataSetCostes = ds_KPI_CostesMensual.Tables[0].Rows.Count;

                double totalNCSelExternas = 0;
                foreach (DataRow dr in ds_KPI_CostesMensual.Tables[0].Rows)
                {
                    totalNCSelExternas += Convert.ToInt32(dr["SELECCIONEXT"]);
                }
                Label lblNCSelExternasAVG = (Label)dgv_CostesNoCalidad.FooterRow.FindControl("AVGNCSelExternas");
                lblNCSelExternasAVG.Text = (totalNCSelExternas / LineasDataSetCostes).ToString("0,0.## €");
                Label lblNCSelExternasTOTAL = (Label)dgv_CostesNoCalidad.FooterRow.FindControl("TotalNCSelExternas");
                lblNCSelExternasTOTAL.Text = totalNCSelExternas.ToString("0,0.## €");

                double totalNCChatarras = 0;
                foreach (DataRow dr in ds_KPI_CostesMensual.Tables[0].Rows)
                {
                    totalNCChatarras += Convert.ToInt32(dr["CHATARRAEXT"]);
                }
                Label lblNCChatarrasAVG = (Label)dgv_CostesNoCalidad.FooterRow.FindControl("AVGNCChatarras");
                lblNCChatarrasAVG.Text = (totalNCChatarras / LineasDataSetCostes).ToString("0,0.## €");
                Label lblNCChatarrasTOTAL = (Label)dgv_CostesNoCalidad.FooterRow.FindControl("TotalNCChatarras");
                lblNCChatarrasTOTAL.Text = totalNCChatarras.ToString("0,0.## €");

                double totalNCCargos = 0;
                foreach (DataRow dr in ds_KPI_CostesMensual.Tables[0].Rows)
                {
                    totalNCCargos += Convert.ToInt32(dr["CARGOSEXT"]);
                }
                Label lblNCCargosAVG = (Label)dgv_CostesNoCalidad.FooterRow.FindControl("AVGNCCargos");
                lblNCCargosAVG.Text = (totalNCCargos / LineasDataSetCostes).ToString("0,0.## €");
                Label lblNCCargosTOTAL = (Label)dgv_CostesNoCalidad.FooterRow.FindControl("TotalNCCargos");
                lblNCCargosTOTAL.Text = totalNCCargos.ToString("0,0.## €");

                double totalNCCostAdmon = 0;
                foreach (DataRow dr in ds_KPI_CostesMensual.Tables[0].Rows)
                {
                    totalNCCostAdmon += Convert.ToInt32(dr["ADMONEXT"]);
                }
                Label lblNCCostAdmonAVG = (Label)dgv_CostesNoCalidad.FooterRow.FindControl("AVGNCCostAdmon");
                lblNCCostAdmonAVG.Text = (totalNCCostAdmon / LineasDataSetCostes).ToString("0,0.## €");
                Label lblNCCostAdmonTOTAL = (Label)dgv_CostesNoCalidad.FooterRow.FindControl("TotalNCCostAdmon");
                lblNCCostAdmonTOTAL.Text = totalNCCostAdmon.ToString("0,0.## €");


                double totalNCCostOtros = 0;
                foreach (DataRow dr in ds_KPI_CostesMensual.Tables[0].Rows)
                {
                    totalNCCostOtros += Convert.ToInt32(dr["OTROSINT"]);
                }
                Label lblNCCostOtrosAVG = (Label)dgv_CostesNoCalidad.FooterRow.FindControl("AVGNCOtros");
                lblNCCostOtrosAVG.Text = (totalNCCostOtros / LineasDataSetCostes).ToString("0,0.## €");
                Label lblNCCostOtrosTOTAL = (Label)dgv_CostesNoCalidad.FooterRow.FindControl("TotalNCOtros");
                lblNCCostOtrosTOTAL.Text = totalNCCostOtros.ToString("0,0.## €");

                double totalGP12 = 0;
                foreach (DataRow dr in ds_KPI_CostesMensual.Tables[0].Rows)
                {
                    totalGP12 += Convert.ToInt32(dr["GP12"]);
                }
                Label lblGP12AVG = (Label)dgv_CostesNoCalidad.FooterRow.FindControl("AVGCosteGP12");
                lblGP12AVG.Text = (totalGP12 / LineasDataSetCostes).ToString("0,0.## €");
                Label lblGP12TOTAL = (Label)dgv_CostesNoCalidad.FooterRow.FindControl("TotalCosteGP12");
                lblGP12TOTAL.Text = totalGP12.ToString("0,0.## €");

                double totalChatarra = 0;
                foreach (DataRow dr in ds_KPI_CostesMensual.Tables[0].Rows)
                {
                    totalChatarra += Convert.ToInt32(dr["CHATARRA"]);
                }
                Label lblCosteChatarraAVG = (Label)dgv_CostesNoCalidad.FooterRow.FindControl("AVGCosteChatarra");
                lblCosteChatarraAVG.Text = (totalChatarra / LineasDataSetCostes).ToString("0,0.## €");
                Label lblCosteChatarraTOTAL = (Label)dgv_CostesNoCalidad.FooterRow.FindControl("TotalCosteChatarra");
                lblCosteChatarraTOTAL.Text = totalChatarra.ToString("0,0.## €");

                double totalCosteNoCalidad = 0;
                foreach (DataRow dr in ds_KPI_CostesMensual.Tables[0].Rows)
                {
                    totalCosteNoCalidad += Convert.ToInt32(dr["CosteNoCalidad"]);
                }
                Label lblCosteNoCalidadAVG = (Label)dgv_CostesNoCalidad.FooterRow.FindControl("AVGCosteNoCalidad");
                lblCosteNoCalidadAVG.Text = (totalCosteNoCalidad / LineasDataSetCostes).ToString("0,0.## €");
                Label lblCosteNoCalidadTOTAL = (Label)dgv_CostesNoCalidad.FooterRow.FindControl("TotalCosteNoCalidad");
                lblCosteNoCalidadTOTAL.Text = totalCosteNoCalidad.ToString("0,0.## €");

                double totalCosteArranques = 0;
                foreach (DataRow dr in ds_KPI_CostesMensual.Tables[0].Rows)
                {
                    totalCosteArranques += Convert.ToInt32(dr["ARRANQUE"]);
                }
                Label lblCosteArranquesAVG = (Label)dgv_CostesNoCalidad.FooterRow.FindControl("AVGCosteArranques");
                lblCosteArranquesAVG.Text = (totalCosteArranques / LineasDataSetCostes).ToString("0,0.## €");
                Label lblCosteArranquesTOTAL = (Label)dgv_CostesNoCalidad.FooterRow.FindControl("TotalCosteArranques");
                lblCosteArranquesTOTAL.Text = totalCosteArranques.ToString("0,0.## €");

                double totalCosteTotal = 0;
                foreach (DataRow dr in ds_KPI_CostesMensual.Tables[0].Rows)
                {
                    totalCosteTotal += Convert.ToInt32(dr["CosteTotal"]);
                }
                Label lblCosteTotalAVG = (Label)dgv_CostesNoCalidad.FooterRow.FindControl("AVGCosteCosteTotal");
                lblCosteTotalAVG.Text = (totalCosteTotal / LineasDataSetCostes).ToString("0,0.## €");
                Label lblCosteTotalTOTAL = (Label)dgv_CostesNoCalidad.FooterRow.FindControl("TotalCosteTotal");
                lblCosteTotalTOTAL.Text = totalCosteTotal.ToString("0,0.## €");

            }
            catch (Exception)
            {
            }
        }
        public void cargar_tablas(object sender, EventArgs e)
        {
            try
            {
                string año = Convert.ToString(Selecaño.SelectedValue);
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                //Conexion_CALIDAD conexion = new Conexion_CALIDAD();
                string SECTOR = "";
                switch (SelecSector.SelectedValue)
                {
                    case "-":
                        break;
                    case "Automoción":
                        SECTOR = " AND PRO.Sector LIKE 'AUTOMOCION%'";
                        break;
                    case "Línea Blanca":
                        SECTOR = " AND PRO.Sector LIKE 'LINEA BLANCA%'";
                        break;
                    case "Menaje":
                        SECTOR = " AND PRO.Sector LIKE 'MENAJE%'";
                        break;
                    case "Otros":
                        SECTOR = " AND (PRO.Sector LIKE 'OTROS%' or PRO.Sector = '')";
                        //SECTOR = " AND PRO.Sector 'OTROS%'";
                        break;
                }

                string TIPO = " AND[TipoNoConformidad] <> 1 ";
                if (TipoAlerta.SelectedValue != "0")
                {
                    TIPO = " AND [TipoNoConformidad] = " + TipoAlerta.SelectedValue;
                }
                ds_KPI_Mensual = SHConexion.Devuelve_kpi_Calidad(año, SECTOR, TIPO);
                ds_KPI_CostesMensual = SHConexion.Devuelve_kpi_Costes_Calidad(año,"");
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
                Response.Redirect("GP12HistoricoReferencia.aspx?REFERENCIA=" + e.CommandArgument.ToString());
            }
            if (e.CommandName == "Redirect2")
            {
                Response.Redirect("GP12HistoricoOperario.aspx?OPERARIO=" + e.CommandArgument.ToString());
            }
        }
    }
}
