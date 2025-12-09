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
    public partial class GP12KPICAL : System.Web.UI.Page
    {
        private static DataTable ds_KPI_Operarios = new DataTable();
        private static DataTable ds_KPI_OperariosNuevo = new DataTable();
        private static DataSet ds_KPI_Top_Operarios_Año = new DataSet();

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
                //DataSet ds_KPI_Mensual = conexion.devuelve_KPI_GP12_Mensual(año);
                ds_KPI_Top_Operarios_Año = conexion.devuelve_detecciones_operarios_Año(año);

                ds_KPI_Operarios = conexion.devuelve_detecciones_operarios_PRIMAS(año, mes).Tables[0];
                ds_KPI_OperariosNuevo = conexion.Devuelve_declaraciones_operarios_PRIMAS(año, mes).Tables[0];
                rellenar_grid();
            }

        }
       
        private void rellenar_grid()
        {
            try
            {

                dgv_KPI_Operarios.DataSource = ds_KPI_Operarios;
                dgv_KPI_Operarios.DataBind();

                TopDeteccionesAño.DataSource = ds_KPI_Top_Operarios_Año;
                TopDeteccionesAño.DataBind();

                {
                    try
                    {
                        //Conexion_SMARTH SHconexion = new Conexion_SMARTH();
                        //Conexion_MATERIALES conexion = new Conexion_MATERIALES();
                        //DataTable PrevisionSecado = conexion.Devuelve_Prevision_SecadoV3();
                        //DataTable StocksDisponibles = conexion.Devuelve_Stock_MaterialV2();

                        //ds_ListaEntregas = SHconexion.Devuelve_listado_Recursos_Entregados(" AND [Employee No_] = " + lblNumOperario.Text + "");
                        //ds_firmas = SHconexion.Devuelve_EPIS_ListaEntrega("");
                        var JoinResult = (from p in ds_KPI_OperariosNuevo.AsEnumerable()
                                          join t in ds_KPI_Operarios.AsEnumerable()
                                          on p.Field<string>("IDE") equals t.Field<string>("IDE") into tempJoin
                                          from leftJoin in tempJoin.DefaultIfEmpty()
                                          select new
                                          {
                                              IDE = p.Field<string>("IDE"),
                                              OPERARIO = p.Field<string>("C_OPERATORNAME"),
                                              DETECCIONES = p.Field<decimal>("PIEZASDETECTADAS"),
                                              PRODUCCIONES = p.Field<decimal>("PRODUCCIONES"),
                                              GP12DETECCIONES = leftJoin == null ? "0" : leftJoin.Field<string>("PIEZASNOK"),
                                              GP12REVISIONES = leftJoin == null ? "0" : leftJoin.Field<string>("SUMA"),
                                          }).ToList();
                        NuevoGridOperarios.DataSource = JoinResult;
                        NuevoGridOperarios.DataBind();
                    }
                    catch (Exception ex)
                    { }
                }

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
                string mes = Convert.ToString(SelecMes.SelectedValue);
                Conexion_GP12 conexion = new Conexion_GP12();
                DataSet ds_KPI_Mensual = conexion.devuelve_KPI_GP12_Mensual(año);
                ds_KPI_Operarios = conexion.devuelve_detecciones_operarios_PRIMAS(año, mes).Tables[0];
                ds_KPI_Top_Operarios_Año = conexion.devuelve_detecciones_operarios_Año(año);
                ds_KPI_OperariosNuevo = conexion.Devuelve_declaraciones_operarios_PRIMAS(año, mes).Tables[0];
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
        }
    }
}
