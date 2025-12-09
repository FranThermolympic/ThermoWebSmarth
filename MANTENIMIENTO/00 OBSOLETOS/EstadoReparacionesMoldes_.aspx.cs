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
    public partial class EstadoReparacionesMoldes_ : System.Web.UI.Page
    {

        private static DataSet ds_listapendientesMOL = new DataSet();
        private static DataSet ds_listaPendientesValidarMOL = new DataSet();
        /*protected GridView dgv_ListaFichasParam;*/

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Conexion conexion = new Conexion();
                Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();
                Conexion_SMARTH SHconexion = new Conexion_SMARTH();
                ds_listapendientesMOL = conexion.Devuelve_pendientes_Moldes("", "", "");
                ds_listaPendientesValidarMOL = conexion.Devuelve_pendientes_validar_Moldes("", "", "", "");
                rellenar_grid();

                //Conexion conexion = new Conexion();
                DataTable AuxiliaresMoldes = SHconexion.Devuelve_Auxiliares_Mantenimiento(); //Lista de dropdowns

                //TIPO MANTENIMIENTO
                SelectTipoMant.Items.Clear();
                AuxiliaresMoldes.DefaultView.RowFilter = "TipoMantenimientoMolde is not null";
                DataTable tipo_trabajo = AuxiliaresMoldes.DefaultView.ToTable();
                foreach (DataRow row in tipo_trabajo.Rows)
                {
                    SelectTipoMant.Items.Add(row["TipoMantenimientoMolde"].ToString());
                }

                DataTable ListaMoldes = SHconexion.Devuelve_listado_MOLDES_V2();
                {
                    for (int i = 0; i <= ListaMoldes.Rows.Count - 1; i++)
                    {
                        FiltroMolde.InnerHtml = FiltroMolde.InnerHtml + System.Environment.NewLine +
                            String.Format("<option value='{0}'>", ListaMoldes.Rows[i][0]);
                    }
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
                dgv_ListaPendientesMOLDE.DataSource = ds_listapendientesMOL;
                dgv_ListaPendientesMOLDE.DataBind();

                dgv_ListaPendientesValidarMAQ.DataSource = ds_listaPendientesValidarMOL;
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
                Response.Redirect("http://facts4-srv/thermogestion/MANTENIMIENTO/ReparacionMoldes.aspx?PARTE=" + e.CommandArgument.ToString());
            }
        }

        public void GridView_RowEditing(Object sender, GridViewEditEventArgs e)
        {
            dgv_ListaPendientesMOLDE.EditIndex = e.NewEditIndex;
            dgv_ListaPendientesMOLDE.DataSource = ds_listapendientesMOL;
            dgv_ListaPendientesMOLDE.DataBind();
        }

        protected void gridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            dgv_ListaPendientesMOLDE.EditIndex = -1;
            dgv_ListaPendientesMOLDE.DataSource = ds_listapendientesMOL;
            dgv_ListaPendientesMOLDE.DataBind();
        }
        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                if (e.Row.RowState.HasFlag(DataControlRowState.Edit))

                {
                    /*
                    DropDownList dropdownAsignado = (DropDownList)e.Row.FindControl("dropdownAsignado");
                    Conexion conexion = new Conexion();
                    DataTable dt = conexion.devuelve_lista_responsables();
                    dropdownAsignado.DataSource = dt;
                    dropdownAsignado.DataTextField = "Nombre";
                    dropdownAsignado.DataValueField = "Nombre";
                    dropdownAsignado.DataBind();
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    dropdownAsignado.SelectedValue = dr["AREPARAR"].ToString();
                    */

                }
            }
        }

        public void CargarFiltrados(object sender, EventArgs e)
        {
            Conexion_SMARTH SHconexion = new Conexion_SMARTH();
            Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();
            string molde = "";

            string tipomantenimiento = "";
            string PreventivoVisible = "";
            string AbiertoPor = "";

            if (tbBuscarMolde.Value != "")
            {
                string[] DataListMOLDE = tbBuscarMolde.Value.Split(new char[] { '¬' });
                string MoldeSELECT = DataListMOLDE[0];
                molde = " AND R.IDMoldes = " + MoldeSELECT;
            }

            if (SelectTipoMant.SelectedValue.ToString() != "")
            {
                tipomantenimiento = " AND R.IdTipoRevision = " + SHconexion.Devuelve_IDTrabajo(SelectTipoMant.SelectedValue.ToString());
            }


            if (SwitchMostraPreventivos.Checked == false)
            {
                PreventivoVisible = " AND R.IdTipoRevision <> 4";
            }


            if (SelectPersonal.SelectedValue.ToString() != "-")
            {
                AbiertoPor = " AND IdEncargado = " + SHconexion.Devuelve_ID_Piloto_SMARTH(SelectPersonal.SelectedValue.ToString());
            }
            ds_listapendientesMOL = conexion.Devuelve_pendientes_Moldes(molde, tipomantenimiento, PreventivoVisible);
            dgv_ListaPendientesMOLDE.DataSource = ds_listapendientesMOL;
            dgv_ListaPendientesMOLDE.DataBind();

            ds_listaPendientesValidarMOL = conexion.Devuelve_pendientes_validar_Moldes(molde, tipomantenimiento, PreventivoVisible, AbiertoPor);
            dgv_ListaPendientesValidarMAQ.DataSource = ds_listaPendientesValidarMOL;
            dgv_ListaPendientesValidarMAQ.DataBind();
        }

        public void VerTodo(object sender, EventArgs e)
        {

            Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();
            ds_listapendientesMOL = conexion.Devuelve_pendientes_Moldes("", "", "");
            dgv_ListaPendientesMOLDE.DataSource = ds_listapendientesMOL;
            dgv_ListaPendientesMOLDE.DataBind();

            ds_listaPendientesValidarMOL = conexion.Devuelve_pendientes_validar_Moldes("", "", "", "");
            dgv_ListaPendientesValidarMAQ.DataSource = ds_listaPendientesValidarMOL;
            dgv_ListaPendientesValidarMAQ.DataBind();



        }
    }
}
