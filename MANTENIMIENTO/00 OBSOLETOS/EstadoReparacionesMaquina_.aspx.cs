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
    public partial class EstadoReparacionesMaquina_ : System.Web.UI.Page
    {

        private static DataSet ds_listapendientesMAQ = new DataSet();
        private static DataSet ds_listaPendientesValidarMAQ = new DataSet();
        private static DataSet ds_listaNoConformeMAQ = new DataSet();
        /*protected GridView dgv_ListaFichasParam;*/

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();
                Conexion_SMARTH SHconexion = new Conexion_SMARTH();
                ds_listapendientesMAQ = conexion.Devuelve_pendientes_maquina("", "", "", "");
                ds_listaPendientesValidarMAQ = conexion.Devuelve_pendientes_validarmaquina("", "", "", "", "");
                
                rellenar_grid();

                DataSet maquinas = conexion.Devuelve_lista_maquinas();
                SelectMaquina.Items.Clear();
                SelectMaquina.Items.Add("");
                foreach (DataRow row in maquinas.Tables[0].Rows)
                {
                    SelectMaquina.Items.Add(row["Maquina"].ToString());
                }

                DataSet perifericos = conexion.Devuelve_lista_perifericos();
                SelectPeriferico.Items.Clear();
                foreach (DataRow row in perifericos.Tables[0].Rows)
                {
                    SelectPeriferico.Items.Add(row["Máquina"].ToString());
                }

                DataSet tipo_trabajo = conexion.Tipos_trabajo();
                SelectTipoMant.Items.Clear();
                foreach (DataRow row in tipo_trabajo.Tables[0].Rows)
                {
                    SelectTipoMant.Items.Add(row["TipoMantenimientoMolde"].ToString());
                }

                DataSet Personal = SHconexion.Devuelve_mandos_intermedios_SMARTH();

                DataSet EncargadosAbiertoPor = Personal; //AbiertoPorV2
                EncargadosAbiertoPor.Tables[0].DefaultView.RowFilter = "Departamento <> 'ADMINISTRADOR' AND Departamento <> 'OFICINAS' AND Departamento <> 'CALIDAD'";
                DataTable DTEncargadosAbiertoPor = EncargadosAbiertoPor.Tables[0].DefaultView.ToTable();
                SelectPersonal.Items.Clear();
                foreach (DataRow row in DTEncargadosAbiertoPor.Rows)
                {
                    SelectPersonal.Items.Add(row["Nombre"].ToString());

                }
            }

        }

        private void rellenar_grid()
        {
            try
            {
                dgv_ListaPendientesMAQ.DataSource = ds_listapendientesMAQ;
                dgv_ListaPendientesMAQ.DataBind();
                dgv_ListaPendientesValidarMAQ.DataSource = ds_listaPendientesValidarMAQ;
                dgv_ListaPendientesValidarMAQ.DataBind();
               

            }
            catch (Exception ex)
            {

            }
        }

        public void ContactsGridView_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Redirect")
            {
                Response.Redirect("../MANTENIMIENTO/ReparacionMaquinas.aspx?PARTE=" + e.CommandArgument.ToString());
            }
        }

        public void GridView_RowEditing(Object sender, GridViewEditEventArgs e)
        {
            dgv_ListaPendientesMAQ.EditIndex = e.NewEditIndex;
            dgv_ListaPendientesMAQ.DataSource = ds_listapendientesMAQ;
            dgv_ListaPendientesMAQ.DataBind();
        }

        protected void gridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            dgv_ListaPendientesMAQ.EditIndex = -1;
            dgv_ListaPendientesMAQ.DataSource = ds_listapendientesMAQ;
            dgv_ListaPendientesMAQ.DataBind();
        }
        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label AccVencidas = (Label)e.Row.FindControl("lblIdEstadoReparacion");
                if (AccVencidas.Text != "")
                { AccVencidas.Visible = true; }

                //IdEstadoReparacion
                if (e.Row.RowState.HasFlag(DataControlRowState.Edit))

                {
                    DropDownList dropdownAsignado = (DropDownList)e.Row.FindControl("dropdownAsignado");
                    Conexion_SMARTH ShConexion = new Conexion_SMARTH();
                    DataTable dt = ShConexion.devuelve_lista_responsables();
                    dropdownAsignado.DataSource = dt;
                    dropdownAsignado.DataTextField = "Nombre";
                    dropdownAsignado.DataValueField = "Nombre";
                    dropdownAsignado.DataBind();
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    dropdownAsignado.SelectedValue = dr["AREPARAR"].ToString();
                }
            }
        }

        public void CargarFiltrados(object sender, EventArgs e)
        {

            Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();
            Conexion_SMARTH SHConexion = new Conexion_SMARTH();
            string maquina = "";
            string periferico = "";
            string tipomantenimiento = "";
            string PreventivoVisible = "";
            string AbiertoPor = "";
            if (SelectMaquina.SelectedValue.ToString() != "")
            {
                maquina = " AND M.IDMaquina = " + conexion.Devuelve_IDmaquina(SelectMaquina.SelectedValue.ToString());
            }
            if (SelectPeriferico.SelectedValue.ToString() != "")
            {
                periferico = " AND M.IDPeriferico = " + conexion.Devuelve_IDperiferico(SelectPeriferico.SelectedValue.ToString());
            }
            if (SelectTipoMant.SelectedValue.ToString() != "")
            {
                tipomantenimiento = " AND IdTipoMantenimiento = " + conexion.Devuelve_IDTrabajo(SelectTipoMant.SelectedValue.ToString());
            }
            if (SwitchMostraPreventivos.Checked == false)
            {
                PreventivoVisible = " AND IdTipoMantenimiento <> 4";
            }
           // if (CheckPreventivo.Checked == true) {      }

            if (SelectPersonal.SelectedValue.ToString() != "-")
            {
                AbiertoPor = " AND IdEncargado = " + SHConexion.Devuelve_ID_Piloto_SMARTH(SelectPersonal.SelectedValue.ToString()); // OJO
            }
            ds_listapendientesMAQ = conexion.Devuelve_pendientes_maquina(maquina, periferico, tipomantenimiento, PreventivoVisible);
            dgv_ListaPendientesMAQ.DataSource = ds_listapendientesMAQ;
            dgv_ListaPendientesMAQ.DataBind();

            ds_listaPendientesValidarMAQ = conexion.Devuelve_pendientes_validarmaquina(maquina, periferico, tipomantenimiento, PreventivoVisible, AbiertoPor);
            dgv_ListaPendientesValidarMAQ.DataSource = ds_listaPendientesValidarMAQ;
            dgv_ListaPendientesValidarMAQ.DataBind();



        }

        public void VerTodo(object sender, EventArgs e)
        {

            Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();
            ds_listapendientesMAQ = conexion.Devuelve_pendientes_maquina("", "", "", "");
            dgv_ListaPendientesMAQ.DataSource = ds_listapendientesMAQ;
            dgv_ListaPendientesMAQ.DataBind();

            ds_listaPendientesValidarMAQ = conexion.Devuelve_pendientes_validarmaquina("", "", "", "", "");
            dgv_ListaPendientesValidarMAQ.DataSource = ds_listaPendientesValidarMAQ;
            dgv_ListaPendientesValidarMAQ.DataBind();



        }

        public void GridView_RowUpdating(Object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Label VALparte = (Label)dgv_ListaPendientesMAQ.Rows[e.RowIndex].FindControl("lblReferencia");
                DropDownList VALAsignado = (DropDownList)dgv_ListaPendientesMAQ.Rows[e.RowIndex].FindControl("dropdownAsignado");
                TextBox VALHorasEstimadas = (TextBox)dgv_ListaPendientesMAQ.Rows[e.RowIndex].FindControl("txtHorasEstimadas");
                TextBox VALFechaPrevista = (TextBox)dgv_ListaPendientesMAQ.Rows[e.RowIndex].FindControl("txtFechaprevsalida");

                int parte = Convert.ToInt32(VALparte.Text);
                string Asignado = Convert.ToString(VALAsignado.SelectedValue);
                string FechaPrevista = Convert.ToString(VALFechaPrevista.Text);
                string HorasEstimadas = Convert.ToString(VALHorasEstimadas.Text);


                Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                conexion.Modificar_Asignacion_Reparacion_Maquina(parte, FechaPrevista, SHConexion.Devuelve_ID_Piloto_SMARTH(Asignado), HorasEstimadas);
                dgv_ListaPendientesMAQ.EditIndex = -1;
                ds_listapendientesMAQ = conexion.Devuelve_pendientes_maquina("", "", "", "");
                dgv_ListaPendientesMAQ.DataSource = ds_listapendientesMAQ;
                dgv_ListaPendientesMAQ.DataBind();

            }
            catch (Exception ex)
            {

            }
        }


    }
}
