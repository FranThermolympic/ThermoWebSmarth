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

namespace ThermoWeb.MANTENIMIENTO
{
    public partial class InformePerifericos : System.Web.UI.Page
    {

        private static DataSet ds_listadoperifericos = new DataSet();
        private static DataSet ds_listahistoricoperifericos = new DataSet();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                DataTable ListaPerifericos = SHConexion.Devuelve_listado_PERIFERICOS();
                for (int i = 0; i <= ListaPerifericos.Rows.Count - 1; i++)
                {
                    FiltroPeriferico.InnerHtml = FiltroPeriferico.InnerHtml + System.Environment.NewLine + String.Format("<option value='{0}'>", ListaPerifericos.Rows[i][0]);
                }

                ds_listadoperifericos = conexion.Devuelve_lista_perifericos_numReparaciones("");               
                ds_listahistoricoperifericos = conexion.Devuelve_historico_perifericos("");
                rellenar_grid();
            }

        }

        private void rellenar_grid()
        {
            try
            {
                dgv_Listadoperifericos.DataSource = ds_listadoperifericos;
                dgv_Listadoperifericos.DataBind();
                
                dgv_ListadoHistoricoperifericos.DataSource = ds_listahistoricoperifericos;
                dgv_ListadoHistoricoperifericos.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        public void BuscarMaquinainforme(Object sender, EventArgs e)
        {
            try
            {
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();
                if (tbBuscarMaquina.Value == "")
                {
                    {
                       
                        ds_listadoperifericos = conexion.Devuelve_lista_perifericos_numReparaciones("");                       
                        ds_listahistoricoperifericos = conexion.Devuelve_historico_perifericos("");
                        rellenar_grid();
                    }
                }
               else
                {
                    
                    ds_listadoperifericos = conexion.Devuelve_lista_perifericos_numReparaciones(" AND P.ID = " + SHConexion.Devuelve_ID_PERIFERICOS(Convert.ToString(tbBuscarMaquina.Value))); //REVISAR  PARA ID               
                    ds_listahistoricoperifericos = conexion.Devuelve_historico_perifericos(" AND ID = " + SHConexion.Devuelve_ID_PERIFERICOS(Convert.ToString(tbBuscarMaquina.Value))); //REVISAR  PARA ID 
                    rellenar_grid();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void ContactsGridView_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName=="Redirect")
        {
            Response.Redirect("../MANTENIMIENTO/ReparacionMaquinas.aspx?PARTE=" + e.CommandArgument.ToString());
        }
        }
        
    }
}
