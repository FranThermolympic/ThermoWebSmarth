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
    public partial class KPIPrimasBSH : System.Web.UI.Page
    {

        private static DataSet ds_KPI_BSH = new DataSet();

        /*protected GridView dgv_ListaFichasParam;*/

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Selecaño.SelectedValue = Convert.ToString(DateTime.Now.Year);
                SelecMes.SelectedValue = Convert.ToString(DateTime.Now.Month);
                string año = Convert.ToString(Selecaño.SelectedValue);
                string mes = Convert.ToString(SelecMes.SelectedValue);
                Conexion_KPI conexion = new Conexion_KPI();

                ds_KPI_BSH = conexion.KPI_Mensual_BSH(año, mes);                              
                Rellenar_grid();
            }

        }
       
        private void Rellenar_grid()
        {
            try
            {
                dgv_KPI_Operarios.DataSource = ds_KPI_BSH;
                dgv_KPI_Operarios.DataBind();
            }
            catch (Exception)
            {
            }
        }
        public void Cargar_tablas(object sender, EventArgs e)
        {
            try
            {
                string año = Convert.ToString(Selecaño.SelectedValue);
                string mes = Convert.ToString(SelecMes.SelectedValue);
                Conexion_KPI conexion = new Conexion_KPI();
                ds_KPI_BSH = conexion.KPI_Mensual_BSH(año,mes);
                Rellenar_grid();
            }
            catch (Exception)
            {

            }
        }

    }
}
