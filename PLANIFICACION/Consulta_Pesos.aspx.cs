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

namespace ThermoWeb.PLANIFICACION
{
    public partial class Consulta_Pesos : System.Web.UI.Page
    {
        private static DataTable ds_listadopesajes = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Conexion_SMARTH conexion = new Conexion_SMARTH();
                conexion.Leer_PESOSNAV();
                conexion.Leer_PESOSBMS();

                //Datalist lista de moldes PDCA
                DataSet ListaInspeccionados = conexion.Devuelve_Listado_Productos_pesados_QMASTER();
                {
                    for (int i = 0; i <= ListaInspeccionados.Tables[0].Rows.Count - 1; i++)
                    {
                        FiltroMolde.InnerHtml = FiltroMolde.InnerHtml + System.Environment.NewLine +
                            String.Format("<option value='{0}'>", ListaInspeccionados.Tables[0].Rows[i][1]);
                    }
                }
                ds_listadopesajes = conexion.Devuelve_Comparativa_Pesos("");
                Rellenar_grid();
            }
        }

        private void Rellenar_grid()
        {
            try
            {
                dgv_ListadoPesajes.DataSource = ds_listadopesajes;
                dgv_ListadoPesajes.DataBind();
                GridExport.DataSource = ds_listadopesajes;
                GridExport.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        public void BuscarMoldeinforme(Object sender, EventArgs e)
        {
            try
            {
                if (tbBuscarMolde.Value == "")
                {
                    {
                        Conexion_SMARTH conexion = new Conexion_SMARTH();
                        ds_listadopesajes = conexion.Devuelve_Comparativa_Pesos("");                       
                        Rellenar_grid();
                    }
                }
                else
                {

                    Conexion_SMARTH conexion = new Conexion_SMARTH();
                    string[] DataListMOLDE = tbBuscarMolde.Value.Split(new char[] { '¬' });
                    string Molde = DataListMOLDE[0];
                    ds_listadopesajes = conexion.Devuelve_Comparativa_Pesos(" AND BMS.[Producto] like '" + Molde + "%' ");                    
                    Rellenar_grid();
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
                Response.Redirect("http://facts4-srv/thermogestion/MANTENIMIENTO/ReparacionMoldes.aspx?PARTE=" + e.CommandArgument.ToString());
            }
        }        
    }
}
