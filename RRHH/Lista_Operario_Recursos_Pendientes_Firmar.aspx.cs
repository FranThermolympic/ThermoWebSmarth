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

namespace ThermoWeb.RRHH
{
    public partial class Lista_Operario_Recursos_Pendientes_Firmar : System.Web.UI.Page
    {

        private static DataTable dt_NAV = new DataTable();
        private static DataTable ds_firmas = new DataTable();
        private static DataTable ds_ListaEntregas = new DataTable();
        private static DataTable ds_esfirmado = new DataTable();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Conexion_SMARTH SHconexion = new Conexion_SMARTH();
                DataTable ListaOperarios = SHconexion.Devuelve_listado_OPERARIOS_SEPARADOR();
                {
                    for (int i = 0; i <= ListaOperarios.Rows.Count - 1; i++)
                    {
                        FiltroOperario.InnerHtml = FiltroOperario.InnerHtml + System.Environment.NewLine +
                            String.Format("<option value='{0}'>", ListaOperarios.Rows[i][0]);
                    }
                }
                Rellenar_grid(null,null);         
            }
        }

        public void Rellenar_grid(object sender, EventArgs e)
        {
           
            string filtro_operario = "";
            if(InputFiltroOperario.Value != "")
            {
                string[] DataListOperario = InputFiltroOperario.Value.Split(new char[] { '¬' });
                try
                {
                    int OperarioSelect = Convert.ToInt32(DataListOperario[0]);
                    filtro_operario = " and [Employee No_] = " + OperarioSelect + "";
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "filtro_NOK();", true);
                }
                
            }

            Conexion_SMARTH SHconexion = new Conexion_SMARTH();
            ds_ListaEntregas = SHconexion.Devuelve_listado_Recursos_Entregados(filtro_operario);
            ds_firmas = SHconexion.Devuelve_EPIS_ListaEntrega("");
            var JoinResult = (from p in ds_ListaEntregas.AsEnumerable()
                              join t in ds_firmas.AsEnumerable()
                              on p.Field<int>("Line No_") equals t.Field<int>("IdNAV")
                              //where t.Field<string>("Cantidad") equals ""
                              into tempJoin
                              from leftJoin
                              in tempJoin.DefaultIfEmpty()                           
                              select new
                              {
                                  OPERARIO = p.Field<int>("Employee No_"),
                                  NOMBRE = p.Field<string>("Search Name"),
                                  //NOMBRE = leftJoin == null ? "" : leftJoin.Field<string>("NOMBRE"),
                                  LINEA = p.Field<int>("Line No_"),
                                  ARTICULO = p.Field<string>("Description"),
                                  NUMSERIE = p.Field<string>("Serial No_"),
                                  CANTIDAD = leftJoin == null ? "" : leftJoin.Field<string>("Cantidad"),
                                  FECHAENTREGA = leftJoin == null ? "" : leftJoin.Field<string>("FechaEntrega"),
                                  ENTREGADOPOR = leftJoin == null ? "" : leftJoin.Field<string>("EntregadoPOR"),
                                  FIRMA = leftJoin == null ? "" : leftJoin.Field<string>("Firma"),
                                  //Grade = leftJoin == null ? 0 : leftJoin.Field<int>("Grade")
                              }
                              ).ToList();
            GridView2.DataSource = JoinResult;
            GridView2.DataBind();
        }

        // elimina una fila
        public void GridView_RowDeleting(Object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                /*
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
                */
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
                                /*
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
                                */
            }
            catch (Exception)
            {
               
            }              
        }

        // cancela la modificación de una fila
        protected void GridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            /*
            dgv_AreaRechazo.EditIndex = -1;
            dgv_AreaRechazo.DataSource = ds_area;
            dgv_AreaRechazo.DataBind(); 
            */
        }

        // modifica una fila
        public void GridView_RowEditing(Object sender, GridViewEditEventArgs e)
        {
            /*
            dgv_AreaRechazo.EditIndex = e.NewEditIndex;
            dgv_AreaRechazo.DataSource = ds_area;
            dgv_AreaRechazo.DataBind();
            */
        }

        // añade nueva fila
        public void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Redirect")
            {
                Response.Redirect("Firma_Operario_Recursos_Entregados.aspx?ID=" + e.CommandArgument.ToString());
            }
        }

        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           
            
            if (e.Row.RowType == DataControlRowType.DataRow && CheckEntregados.Checked)
            { 
                
                Label Fecha = (Label)e.Row.FindControl("lblFecha");
                if (Fecha.Text != "")
                {
                   e.Row.Visible = false;
                }
            }   
           

        }
    }

}