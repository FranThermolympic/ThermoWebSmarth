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

namespace ThermoWeb
{
    public partial class KPIFichasParametros : System.Web.UI.Page
    {

        private static DataSet ds_listficha = new DataSet();
        private static DataSet ds_listamissing = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                SHConexion.LimpiarOrdenesProduciendoBMS();
                SHConexion.leer_OrdenesProduciendoBMS();

                Conexion conexion = new Conexion();
                ds_listficha = conexion.devuelveKPIEncargadoXFicha();
                //conexion.LimpiarOrdenesProduciendoBMS();
                //conexion.leer_OrdenesProduciendoBMS();
                ds_listamissing = conexion.devuelveKPIFichasFaltantes();
                Rellenar_grid();
            }

        }

        protected void Cargarlistaordenadareferencia(object sender, EventArgs e)
        {
            try
            {
                Conexion conexion = new Conexion();
                ds_listficha = conexion.devuelveKPIEncargadoXFicha();
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
                dgv_ListaEncargados.DataSource = ds_listficha;
                dgv_ListaEncargados.DataBind();

                KPIListaMissing.DataSource = ds_listamissing;
                KPIListaMissing.DataBind();

            }
            catch (Exception)
            {
            }
        }
        public void GridView_RowDeleting(Object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int num_fila = Convert.ToInt16(e.RowIndex);
                int i = 0;
                foreach (DataRow row in ds_listficha.Tables[0].Rows)
                {
                    if (i == num_fila)
                    {
                        Conexion conexion = new Conexion();
                        conexion.eliminar_ficha_lista_V2(Convert.ToInt32(row["Referencia"]), Convert.ToInt32(row["Maquina"]), Convert.ToInt32(row["Version"]));
                        ds_listficha = conexion.lista_fichas_parametros();
                        Rellenar_grid();
                        break;
                    }
                    i++;
                }
            }
            catch (Exception)
            {

            }
        }

        public void ContactsGridView_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName=="Redirect")
        {
            Response.Redirect("FichasParametros.aspx?REFERENCIA=" + e.CommandArgument.ToString());
        }
        }
        
    }
}

