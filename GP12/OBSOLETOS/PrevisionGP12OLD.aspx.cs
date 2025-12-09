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
    public partial class GP12PrevisionGP12 : System.Web.UI.Page
    {

        private static DataSet ds_Prevision = new DataSet();
        private static DataSet ds_PrevisionAtrasados = new DataSet();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Conexion_GP12 conexion = new Conexion_GP12();
                //conexion.LimpiarTablaPrevisiones();
                //conexion.leer_previsiones();
                conexion.LimpiarTablaPrevisionesNAV();
                conexion.leer_previsionesNAV();

                ds_Prevision = conexion.lee_grid_previsionNAV();
                rellenar_grid();

                ds_PrevisionAtrasados = conexion.lee_grid_prevision_atrasadasNAV();
                rellenar_grid2();
            }

        }

        public void filtrodia(object sender, EventArgs e)
        {
            try
            {
                if (txtFechaprevsalida.Text == "")
                {
                    Conexion_GP12 conexion = new Conexion_GP12();
                    ds_Prevision = conexion.lee_grid_previsionNAV();
                    dgv_PrevisionGP12.DataSource = ds_Prevision;
                    dgv_PrevisionGP12.DataBind();
                }
                else
                {
                    Conexion_GP12 conexion = new Conexion_GP12();
                    ds_Prevision = conexion.lee_grid_prevision_filtradoNAV(txtFechaprevsalida.Text);
                    rellenar_grid();
                }

            }
            catch (Exception)
            {

            }
        }   

        private void rellenar_grid()
        {
            try
            {
                dgv_PrevisionGP12.DataSource = ds_Prevision;
                dgv_PrevisionGP12.DataBind(); 
            }
            catch (Exception)
            { 
                
            }
        }
        private void rellenar_grid2()
        {
            try
            {
                dgv_atrasados.DataSource = ds_PrevisionAtrasados;
                dgv_atrasados.DataBind();
            }
            catch (Exception)
            {

            }
        }      
        
        
    }

}