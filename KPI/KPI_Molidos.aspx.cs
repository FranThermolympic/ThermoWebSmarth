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
    public partial class KPI_Molidos : System.Web.UI.Page
        
    {
        protected void Page_Load(object sender, EventArgs e)
        {           
            if (!IsPostBack)
            {
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                DataTable Materiales = SHConexion.Devuelve_Lista_Materiales_SEPARADOR();
                {
                    for (int i = 0; i <= Materiales.Rows.Count - 1; i++)
                    {
                        DatalistFiltroMat.InnerHtml = DatalistFiltroMat.InnerHtml + System.Environment.NewLine +
                            String.Format("<option value='{0}'>", Materiales.Rows[i][0]);
                    }
                }

                rellenar_grid(null,null);
            }

        }
       
        public void rellenar_grid(object sender, EventArgs e)
        {
            try
            {
                //Filtros
                string where = "";
                string where2 = "";
                string filtromat = "";
                string filtromes = "";
                string filtromat2 = "";
                string filtromes2 = "";
                string where3 = "";
                string filtromat3 = "";
                string filtromes3 = "";
                string where4 = "";
                string filtromes4 = "";
                string filtromat4 = "";
                if (InputFiltroMaterial.Value != "")
                {
                    string[] RecorteMaterial = InputFiltroMaterial.Value.Split(new char[] { '¬' });
                    filtromat = " AND HIS.Referencia = '" + RecorteMaterial[0].Trim().ToString() + "'";
                    filtromat2 = " AND MAT.Referencia = '" + RecorteMaterial[0].Trim().ToString() + "'";
                    filtromat3 = " AND Referencia = '" + RecorteMaterial[0].Trim().ToString() + "'";
                    filtromat4 = " AND MAT.C_ID = '" + RecorteMaterial[0].Trim().ToString() + "'";
                       
                }
                if (SeleMes.SelectedValue != "0")
                {
                    filtromes = " AND MESNUM = " + SeleMes.SelectedValue;
                    filtromes2 = " AND MONTH(HIS.[Fecha]) = " + SeleMes.SelectedValue;
                    filtromes3 = " and MONTH(FECHA) = " + SeleMes.SelectedValue;
                    filtromes4 = " AND EXTRACT(MONTH FROM C_STARTTIME) = " + SeleMes.SelectedValue;

                }
                
               string filtroaño = " AND YEAR = " + Selecaño.SelectedValue;
               string filtroaño2 = " AND YEAR(HIS.[Fecha]) = " + Selecaño.SelectedValue;
               string filtroaño3 = " and YEAR(FECHA) = " + Selecaño.SelectedValue;
               string filtroaño4 = " AND EXTRACT(YEAR FROM C_STARTTIME) = " + Selecaño.SelectedValue;

                where = filtromat + filtromes + filtroaño;
                where2 = filtromat2 + filtromes2 + filtroaño2;
                where3 = filtromat3 + filtromes3 + filtroaño3;
                where4 = filtromat4 + filtromes4 + filtroaño4;


                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                DataTable HistoricoMolino = SHConexion.Devuelve_Historico_Molidos(where2);
                dgv_HistoricoMolidos.DataSource = HistoricoMolino;
                dgv_HistoricoMolidos.DataBind();


                DataTable HistoricoMES = SHConexion.Devuelve_ResumenMolidosMES(where);
                GridKPIporMES.DataSource = HistoricoMES;
                GridKPIporMES.DataBind();

                DataTable HistoricoMolinos = SHConexion.Devuelve_ResumenMolinos(where3);
                GridKPIporMOLINOMES.DataSource = HistoricoMolinos;
                GridKPIporMOLINOMES.DataBind();

                KPILineasTotalMOL.InnerText = HistoricoMolino.Rows.Count.ToString();

                double sumakilos = 0.0;
                for (int i = 0; i <= HistoricoMolino.Rows.Count - 1; i++)
                {
                    
                    sumakilos = sumakilos+ Convert.ToDouble(HistoricoMolino.Rows[i][4]);
                }
                KPIKGSMOL.InnerText = sumakilos.ToString("#,##");



                DataSet ds_KPI_KGTransformados = SHConexion.KPI_Mensual_KGTransformadosMOLIDO(Selecaño.SelectedValue, where4);

                double sumatransformados = 0.0;
                for (int i = 0; i <= ds_KPI_KGTransformados.Tables[0].Rows.Count - 1; i++)
                {

                    sumatransformados = sumatransformados + Convert.ToDouble(ds_KPI_KGTransformados.Tables[0].Rows[i][2]);
                }
                KPIKgstransformados.InnerText = sumatransformados.ToString("#,##");

                double porcentaje = (sumakilos) / sumatransformados;
                FootMesReciclados.InnerText = "Reciclado el " + porcentaje.ToString("0.##%") + " del material producido";

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
