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

namespace ThermoWeb.AREA_RECHAZO
{
    public partial class Area_Rechazo : System.Web.UI.Page
    {

        private static DataSet ds_area = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Conexion_Area_Rechazo conexion = new Conexion_Area_Rechazo();
                ds_area = conexion.Leer_area_rechazo();
                Rellenar_grid();
            }

        }

        private void Rellenar_grid()
        {
            try
            {               
                dgv_AreaRechazo.DataSource = ds_area;
                dgv_AreaRechazo.DataBind(); 
            }
            catch (Exception)
            { 
                
            }
        }       

        // elimina una fila
        public void GridView_RowDeleting(Object sender, GridViewDeleteEventArgs e)
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
                        conexion.Eliminar_area_rechazo(Convert.ToInt32(row["Id"]));
                        ds_area = conexion.Leer_area_rechazo();
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

        // guarda una fila
        public void GridView_RowUpdating(Object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                string id = dgv_AreaRechazo.DataKeys[e.RowIndex].Values["Id"].ToString();
                TextBox referencia = (TextBox)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("txtReferencia");
                TextBox motivo = (TextBox)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("txtMotivo");
                TextBox responsableEntrada = (TextBox)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("txtResponsableEntrada");
                TextBox cantidad = (TextBox)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("txtCantidad");
                TextBox fechaEntrada = (TextBox)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("txtFechaEntrada");
                TextBox fechaSalida = (TextBox)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("txtFechaSalida");
                TextBox debeSalir = (TextBox)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("txtDebeSalir");
                TextBox decision = (TextBox)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("txtDecision");
                TextBox responsable_salida = (TextBox)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("txtResponsableSalida");
                TextBox observaciones = (TextBox)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("txtObservaciones");
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

                conexion.Actualizar_area_rechazo(Convert.ToInt32(id), referencia.Text, motivo.Text, responsableEntrada.Text,
                    Convert.ToInt32(cantidad.Text), fecha_entrada, fecha_salida, debe_salir, decision.Text, responsable_salida.Text, observaciones.Text);

                dgv_AreaRechazo.EditIndex = -1;
                ds_area = conexion.Leer_area_rechazo();
                dgv_AreaRechazo.DataSource = ds_area;
                dgv_AreaRechazo.DataBind(); 
            }
            catch (Exception)
            {
               
            }              
        }

        // cancela la modificación de una fila
        protected void GridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            dgv_AreaRechazo.EditIndex = -1;
            dgv_AreaRechazo.DataSource = ds_area;
            dgv_AreaRechazo.DataBind(); 
        }

        // modifica una fila
        public void GridView_RowEditing(Object sender, GridViewEditEventArgs e)
        {
            dgv_AreaRechazo.EditIndex = e.NewEditIndex;
            dgv_AreaRechazo.DataSource = ds_area;
            dgv_AreaRechazo.DataBind();
        }

        // añade nueva fila
        public void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
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
                string fecha_entrada;
                string fecha_salida;
                string debe_salir;

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

                conexion.Insertar_area_rechazo(referencia.Text, motivo.Text, responsableEntrada.Text, Convert.ToInt32(cantidad.Text), fecha_entrada, fecha_salida, debe_salir, decision.Text, responsable_salida.Text, observaciones.Text);                
                ds_area = conexion.Leer_area_rechazo();
                dgv_AreaRechazo.DataSource = ds_area;
                dgv_AreaRechazo.DataBind(); 
            }
        }

        // carga la lista utilizando un filtro
        protected void Cargar_filtro(object sender, EventArgs e)
        {
            string lista_filtros = Request.Form[cbFiltro.UniqueID];
            cbFiltro.Items.FindByValue(lista_filtros).Selected = true;
            string filtro = cbFiltro.Items.FindByValue(lista_filtros).Text;
            Conexion_Area_Rechazo conexion = new Conexion_Area_Rechazo();
            
            if (filtro == "Activas")
            {
                ds_area = conexion.Leer_area_rechazo();
            }
            else
            {
                ds_area = conexion.Leer_area_rechazo_todas();
            }
            dgv_AreaRechazo.DataSource = ds_area;
            dgv_AreaRechazo.DataBind(); 
        }
    }

}