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
                conexion.LimpiarTablaPrevisiones();
                conexion.leer_previsiones();
                ds_Prevision = conexion.lee_grid_prevision();
                rellenar_grid();

                ds_PrevisionAtrasados = conexion.lee_grid_prevision_atrasadas();
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
                    ds_Prevision = conexion.lee_grid_prevision();
                    dgv_PrevisionGP12.DataSource = ds_Prevision;
                    dgv_PrevisionGP12.DataBind();
                }
                else
                {
                    Conexion_GP12 conexion = new Conexion_GP12();
                    ds_Prevision = conexion.lee_grid_prevision_filtrado(txtFechaprevsalida.Text);
                    rellenar_grid();
                }

            }
            catch (Exception ex)
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
            catch (Exception ex)
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
            catch (Exception ex)
            {

            }
        }      
        // elimina una fila
        /*public void GridView_RowDeleting(Object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int num_fila = Convert.ToInt16(e.RowIndex);
                int i = 0;
                foreach (DataRow row in ds_area.Tables[0].Rows)
                {
                    if (i == num_fila)
                    {
                        Conexion_Area_Rechazo conexion = new Conexion_Area_Rechazo();
                        conexion.eliminar_area_rechazo(Convert.ToInt32(row["Id"]));
                        ds_area = conexion.leer_area_rechazo();
                        rellenar_grid();
                        break;
                    }
                    i++;
                }
            }
            catch (Exception ex)
            {
                
            }
        }
        */
        // guarda una fila

        // cancela la modificación de una fila
        
        // añade nueva fila
        /*public void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("AddNew"))
            {
                TextBox referencia = (TextBox)dgv_AreaRechazo.FooterRow.FindControl("footReferencia");
                TextBox motivo = (TextBox)dgv_AreaRechazo.FooterRow.FindControl("footMotivo");
                TextBox responsableEntrada = (TextBox)dgv_AreaRechazo.FooterRow.FindControl("footResponsableEntrada");
                TextBox cantidad = (TextBox)dgv_AreaRechazo.FooterRow.FindControl("footCantidad");
                TextBox fechaEntrada = (TextBox)dgv_AreaRechazo.FooterRow.FindControl("footFechaEntrada");
                TextBox fechaSalida = (TextBox)dgv_AreaRechazo.FooterRow.FindControl("footFechaSalida");
                TextBox debeSalir = (TextBox)dgv_AreaRechazo.FooterRow.FindControl("footDebeSalir");
                TextBox decision = (TextBox)dgv_AreaRechazo.FooterRow.FindControl("footDecision");
                TextBox responsable_salida = (TextBox)dgv_AreaRechazo.FooterRow.FindControl("footResponsableSalida");
                TextBox observaciones = (TextBox)dgv_AreaRechazo.FooterRow.FindControl("footObservaciones");
                Conexion_Area_Rechazo conexion = new Conexion_Area_Rechazo();
                string fecha_entrada = "";
                string fecha_salida = "";
                string debe_salir = "";

                if (fechaEntrada.Text == "")
                    fecha_entrada = null;
                else
                    fecha_entrada = fechaEntrada.Text;

                if (fechaSalida.Text == "")
                    fecha_salida = null;
                else
                    fecha_salida = fechaSalida.Text;

                if (debeSalir.Text == "")
                    debe_salir = null;
                else
                    debe_salir = debeSalir.Text;

                conexion.insertar_area_rechazo(referencia.Text, motivo.Text, responsableEntrada.Text, Convert.ToInt32(cantidad.Text), fecha_entrada, fecha_salida, debe_salir, decision.Text, responsable_salida.Text, observaciones.Text);                
                ds_area = conexion.leer_area_rechazo();
                dgv_AreaRechazo.DataSource = ds_area;
                dgv_AreaRechazo.DataBind(); 
            }
        }
        */
        // carga la lista utilizando un filtro
        
    }

}