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
    public partial class GP12ReferenciasEstado : System.Web.UI.Page
    {

        private static DataSet ds_Referencias = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Conexion_GP12 conexion = new Conexion_GP12();
                ds_Referencias = conexion.leer_ReferenciaEstadosActivas();
                CargaListasFiltro();
                rellenar_grid();
            }

        }

        private void rellenar_grid()
        {
            try
            {
                dgv_AreaRechazo.DataSource = ds_Referencias;
                dgv_AreaRechazo.DataBind(); 
            }
            catch (Exception)
            { 
                
            }
        }
        public void ImportardeBMS(object sender, EventArgs e)
        {
        
            try
            {
                Conexion_GP12 conexion = new Conexion_GP12();
                conexion.leer_productosBMS();
                conexion.leer_operariosBMS();
                conexion.InsertaProductosReferenciaEstados();
                conexion.InsertaProductosTablaDocumentos();
                ds_Referencias = conexion.leer_ReferenciaEstadosActivas();
                rellenar_grid();
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
                Label referencia = (Label)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("txtReferencia");
                DropDownList estadoactual = (DropDownList)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("txtEstadoActual");
                DropDownList responsable = (DropDownList)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("txtResponsable");
                Label fechrevision = (Label)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("txtFecharev");
                TextBox fechprevsal = (TextBox)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("txtFechaprevsalida");
                Label estadoanterior = (Label)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("txtEstadoAnterior");
                Label Fechaestanterior = (Label)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("txtFechaestanterior");
                TextBox Observaciones = (TextBox)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("txtObservaciones");
                
                Conexion_GP12 conexion = new Conexion_GP12();
                 //DESCOMENTAR AL TERMINAR
                conexion.actualizar_estado(Convert.ToInt32(referencia.Text),
                                            conexion.devuelve_IDlista_razonesrevision(Convert.ToString(estadoactual.SelectedValue)),
                                            conexion.Devuelve_IDlista_responsablesSMARTH(Convert.ToString(responsable.SelectedValue)),
                                            fechrevision.Text,
                                            fechprevsal.Text,
                                            estadoanterior.Text,
                                            Fechaestanterior.Text,
                                            Observaciones.Text);
                conexion.actualizar_productosBMS(Convert.ToInt32(referencia.Text), conexion.devuelve_IDlista_razonesrevision(Convert.ToString(estadoactual.SelectedValue)));
                dgv_AreaRechazo.EditIndex = -1;
                //ds_Referencias = conexion.leer_referenciaestados();
                //dgv_AreaRechazo.DataSource = ds_Referencias;
                //dgv_AreaRechazo.DataBind();
                cargar_Filtrados(null, e);
            }
            catch (Exception)
            {
               
            }              
        }

        // cancela la modificación de una fila
        protected void gridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            dgv_AreaRechazo.EditIndex = -1;
            dgv_AreaRechazo.DataSource = ds_Referencias;
            dgv_AreaRechazo.DataBind();
        }

        // modifica una fila
        public void GridView_RowEditing(Object sender, GridViewEditEventArgs e)
        {
            dgv_AreaRechazo.EditIndex = e.NewEditIndex;
            dgv_AreaRechazo.DataSource = ds_Referencias;
            dgv_AreaRechazo.DataBind();
             }

        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                    if (e.Row.RowState.HasFlag(DataControlRowState.Edit))

                {
                    DropDownList txtEstadoActual = (DropDownList)e.Row.FindControl("txtEstadoActual");
                    Conexion_GP12 conexion = new Conexion_GP12();
                    DataTable dt = conexion.leer_tablalistaestados();
                    txtEstadoActual.DataSource = dt;
                    txtEstadoActual.DataTextField = "Razon";
                    txtEstadoActual.DataValueField = "Razon";
                    txtEstadoActual.DataBind();

                    DataRowView dr = e.Row.DataItem as DataRowView;
                    txtEstadoActual.SelectedValue = dr["EstadoActual"].ToString();

                    DropDownList txtResponsable = (DropDownList)e.Row.FindControl("txtResponsable");
                    DataTable dt2 = conexion.devuelve_lista_responsables();
                    txtResponsable.DataSource = dt2;
                    txtResponsable.DataTextField = "PAprobado";
                    txtResponsable.DataValueField = "PAprobado";
                    txtResponsable.DataBind();
                    DataRowView dr2 = e.Row.DataItem as DataRowView;
                    txtResponsable.SelectedValue = dr2["Responsable"].ToString();


                    /*for (int i = 0; i <= dgv_AreaRechazo.Rows.Count - 1; i++)
                    {
                        Label lblparent = (Label)dgv_AreaRechazo.Rows[i].FindControl("lblMalas");

                        if (Convert.ToDateTime(lblparent.Text)
                            
                            )
                        {
                            dgv_AreaRechazo.Rows[i].Cells[9].BackColor = System.Drawing.Color.Red;
                            lblparent.ForeColor = System.Drawing.Color.White;
                        }

                        Label lblparent2 = (Label)dgv_AreaRechazo.Rows[i].FindControl("lblRetrabajadas");

                    }*/

                }
            }
        }
        
        // carga la lista utilizando un filtro
        public void CargaListasFiltro()
        {
            try
            {
                Conexion_GP12 conexion = new Conexion_GP12();
                DataSet estado = new DataSet();
                estado = conexion.devuelve_lista_razonesrevision();
                foreach (DataRow row in estado.Tables[0].Rows) { lista_estado.Items.Add(row["Razon"].ToString()); }
                lista_estado.ClearSelection();
                lista_estado.SelectedValue = "";

                DataSet clientes = new DataSet();
                clientes = conexion.devuelve_lista_clientes();
                foreach (DataRow row in clientes.Tables[0].Rows) { lista_clientes.Items.Add(row["Cliente"].ToString()); }
                lista_clientes.ClearSelection();
                lista_clientes.SelectedValue = "";


                DataSet responsable = new DataSet();
                responsable = conexion.Devuelve_setlista_responsablesSMARTH();
                foreach (DataRow row in responsable.Tables[0].Rows) { lista_responsable.Items.Add(row["PAprobado"].ToString()); }
                lista_responsable.ClearSelection();
                lista_responsable.SelectedValue = "";


            }
            catch (Exception)
            {

            }
        }

        protected void cargar_todas(object sender, EventArgs e)
        {

            Conexion_GP12 conexion = new Conexion_GP12();
            ds_Referencias = conexion.leer_referenciaestados();
            dgv_AreaRechazo.DataSource = ds_Referencias;
            dgv_AreaRechazo.DataBind();
            VerTodas.Visible = false;
            VerRevision.Visible = true;
        }
        protected void cargar_EnRevision(object sender, EventArgs e)
        {

            Conexion_GP12 conexion = new Conexion_GP12();
            ds_Referencias = conexion.leer_ReferenciaEstadosActivas();
            dgv_AreaRechazo.DataSource = ds_Referencias;
            dgv_AreaRechazo.DataBind();
            VerTodas.Visible = true;
            VerRevision.Visible = false;
        }

        protected void cargar_Filtrados(object sender, EventArgs e)
        {

            string estado = Convert.ToString(lista_estado.SelectedValue);
            string cliente = Convert.ToString(lista_clientes.SelectedValue);
            if (cliente == "-") {cliente = "";}
            string responsable = Convert.ToString(lista_responsable.SelectedValue);
            if (responsable == "-") { responsable = ""; }
            Conexion_GP12 conexion = new Conexion_GP12();
            ds_Referencias = conexion.leer_ReferenciaEstadosFiltros(estado, cliente, responsable, Convert.ToString(selectReferencia.Text), Convert.ToString(selectMolde.Text));
            dgv_AreaRechazo.DataSource = ds_Referencias;
            dgv_AreaRechazo.DataBind();
            VerTodas.Visible = true;
            VerRevision.Visible = false;
        }

        public void ContactsGridView_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Redirect")
            {
                Response.Redirect("GP12HistoricoReferencia.aspx?REFERENCIA=" + e.CommandArgument.ToString());
            }
        }
        /*protected void cargar_filtro(object sender, EventArgs e)
        {
            string lista_filtros = Request.Form[cbFiltro.UniqueID];
            cbFiltro.Items.FindByValue(lista_filtros).Selected = true;
            string filtro = cbFiltro.Items.FindByValue(lista_filtros).Text;
            Conexion_GP12 conexion = new Conexion_GP12();

            if (filtro == "En revisión")
            {
                ds_Referencias = conexion.leer_ReferenciaEstadosActivas();
            }
            else
            {
                ds_Referencias = conexion.leer_referenciaestados();
            }
            dgv_AreaRechazo.DataSource = ds_Referencias;
            dgv_AreaRechazo.DataBind(); 
        }
        */
        
    }

}