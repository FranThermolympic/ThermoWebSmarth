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

namespace ThermoWeb.LIBERACIONES
{
    public partial class HistoricoLiberacion : System.Web.UI.Page
    {

        private static DataSet ds_Liberaciones = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string maquina = Convert.ToString(lista_clientes.SelectedValue);
                if (maquina == "-") { maquina = ""; }
                string cliente = Convert.ToString(lista_clientes.SelectedValue);
                if (cliente == "-") { cliente = ""; }
                Conexion_LIBERACIONES conexion = new Conexion_LIBERACIONES();
                ds_Liberaciones = conexion.Devuelve_Historico_Liberaciones(selectOrden.Text, selectReferencia.Text, maquina, cliente, selectMolde.Text, Camblib.SelectedValue.ToString(), Prodlib.SelectedValue.ToString(), Callib.SelectedValue.ToString(),"","","","");
                CargaListasFiltro();
                Rellenar_grid();
            }

        }

        private void Rellenar_grid()
        {
            try
            {
                dgv_AreaRechazo.DataSource = ds_Liberaciones;
                dgv_AreaRechazo.DataBind();
            }
            catch (Exception)
            {
            }
        }
        // carga la lista utilizando un filtro
        public void CargaListasFiltro()
        {
            try
            {
                Conexion_LIBERACIONES conexion = new Conexion_LIBERACIONES();

                DataSet clientes = new DataSet();
                clientes = conexion.devuelve_lista_clientes();
                foreach (DataRow row in clientes.Tables[0].Rows) { lista_clientes.Items.Add(row["Cliente"].ToString()); }
                lista_clientes.ClearSelection();
                lista_clientes.SelectedValue = "";

                DataSet maquinas = new DataSet();
                maquinas = conexion.Devuelve_Maquinas_bms();
                foreach (DataRow row in maquinas.Tables[0].Rows) { lista_maquinas.Items.Add(row["C_ID"].ToString()); }
                lista_maquinas.ClearSelection();
                lista_maquinas.SelectedValue = "";

                DataSet Operarios = conexion.Devuelve_mandos_intermedios_SMARTH();
                Operarios.Tables[0].DefaultView.RowFilter = "Departamento = 'CAMBIADOR' OR Departamento = '-'";
                DataTable DTCambiador = (Operarios.Tables[0].DefaultView).ToTable();
                foreach (DataRow row in DTCambiador.Rows) { CamblibNom.Items.Add(row["Nombre"].ToString()); }
                CamblibNom.ClearSelection();
                CamblibNom.SelectedValue = "-";

                Operarios.Tables[0].DefaultView.RowFilter = "Departamento = 'PRODUCCION' OR Departamento = '-'";
                DataTable DTProduccion = (Operarios.Tables[0].DefaultView).ToTable();
                foreach (DataRow row in DTProduccion.Rows) { ProdlibNom.Items.Add(row["Nombre"].ToString()); }
                ProdlibNom.ClearSelection();
                ProdlibNom.SelectedValue = "-";

                Operarios.Tables[0].DefaultView.RowFilter = "Departamento = 'CALIDAD' OR Departamento = '-'";
                DataTable DTCalidad = (Operarios.Tables[0].DefaultView).ToTable();
                foreach (DataRow row in DTCalidad.Rows) { CallibNom.Items.Add(row["Nombre"].ToString()); }
                CallibNom.ClearSelection();
                CallibNom.SelectedValue = "-";





            }
            catch (Exception)
            {

            }
        }

        protected void Cargar_todas(object sender, EventArgs e)
        {

            Conexion_LIBERACIONES conexion = new Conexion_LIBERACIONES();
            //ds_Referencias = conexion.devuelve_ultimas_revisiones_periodo(Convert.ToString(TablaSeleccion.SelectedValue));
            string orden = "";
            string referencia = "";
            string molde = "";
            string maquina = "";
            string cliente = "";
            string camlib = "";
            string prodlib = "";
            string callib = "";
            ds_Liberaciones = conexion.Devuelve_Historico_Liberaciones(orden, referencia, maquina, cliente, molde, camlib, prodlib, callib,"","","","");
            Rellenar_grid();
        }

        protected void Cargar_Filtrados(object sender, EventArgs e)
        {
            Conexion_LIBERACIONES conexion = new Conexion_LIBERACIONES();

            string cliente = Convert.ToString(lista_clientes.SelectedValue);
            if (cliente == "-") { cliente = ""; }

            string maquina = Convert.ToString(lista_maquinas.SelectedValue);
            if (maquina == "-") { maquina = ""; }

            string cambiador = CamblibNom.SelectedValue.ToString();
                if (cambiador == "-")
                    { cambiador = ""; }
                else
                    { cambiador = " AND L.Cambiador = "+ conexion.devuelve_mandos_intermedios_SMARTH(CamblibNom.SelectedValue.ToString()) + ""; }

            string produccion = ProdlibNom.SelectedValue.ToString();
                if (produccion == "-")
                    { produccion = ""; }
                else
                    { produccion = " AND L.Encargado = " + conexion.devuelve_mandos_intermedios_SMARTH(ProdlibNom.SelectedValue.ToString()) + ""; }
            string calidad = CallibNom.SelectedValue.ToString();
                if (calidad == "-")
                    { calidad = ""; }
                else
                    { calidad = " AND L.Calidad = " + conexion.devuelve_mandos_intermedios_SMARTH(CallibNom.SelectedValue.ToString()) + ""; }
            string liberadaNOK = "";
            if (Callib.SelectedValue == "3" || Prodlib.SelectedValue == "3" || Camblib.SelectedValue == "3")
            {
                string liberadoNOK = " AND [AccionLiberado] = 4";
            }
            ds_Liberaciones = conexion.Devuelve_Historico_Liberaciones(selectOrden.Text, selectReferencia.Text, maquina, cliente, selectMolde.Text, Camblib.SelectedValue.ToString(), Prodlib.SelectedValue.ToString(), Callib.SelectedValue.ToString(), cambiador, produccion, calidad, liberadaNOK);
            CargaListasFiltro();
            Rellenar_grid();

        }

        public void GridViewCommandEventHandler(Object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Redirect")
            {
                Response.Redirect("LiberacionSerie.aspx?ORDEN=" + e.CommandArgument.ToString());
            }

        }

       
        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            for (int i = 0; i <= dgv_AreaRechazo.Rows.Count - 1; i++)
            {
                Label lblparent = (Label)dgv_AreaRechazo.Rows[i].FindControl("lblCambio");
                if (lblparent.Text == "Pendiente")
                {
                    //dgv_Liberaciones.Rows[i].Cells[6].BackColor = System.Drawing.Color.Yellow;
                    lblparent.ForeColor = System.Drawing.Color.Red;
                }

                if (lblparent.Text == "OK Condicionada")
                {
                    lblparent.ForeColor = System.Drawing.Color.Orange;
                }

                if (lblparent.Text == "Liberada OK")
                {
                    lblparent.ForeColor = System.Drawing.Color.Green;
                }

                Label lblparent2 = (Label)dgv_AreaRechazo.Rows[i].FindControl("lblProduccion");
                if (lblparent2.Text == "Pendiente")
                {
                    lblparent2.ForeColor = System.Drawing.Color.Red;
                }

                if (lblparent2.Text == "OK Condicionada")
                {
                    lblparent2.ForeColor = System.Drawing.Color.Orange;
                }

                if (lblparent2.Text == "Liberada OK")
                {
                    lblparent2.ForeColor = System.Drawing.Color.Green;
                }

                Label lblparent3 = (Label)dgv_AreaRechazo.Rows[i].FindControl("lblCalidad");
                if (lblparent3.Text == "Pendiente")
                {
                    //dgv_Liberaciones.Rows[i].Cells[8].BackColor = System.Drawing.Color.Yellow;
                    lblparent3.ForeColor = System.Drawing.Color.Red;
                }

                if (lblparent3.Text == "OK Condicionada")
                {
              
                    lblparent3.ForeColor = System.Drawing.Color.Orange;
                }
                if (lblparent3.Text == "Liberada OK")
                {
                   
                    lblparent3.ForeColor = System.Drawing.Color.Green;
                }

                Label Accion = (Label)dgv_AreaRechazo.Rows[i].FindControl("lblAccionLiberado");
                if (Accion.Text == "4")
                {
                    dgv_AreaRechazo.Rows[i].BackColor = System.Drawing.Color.Red;
                   
                }
            }

        }
        /*
       public void CargaHistorico(Object sender, EventArgs e)
       {
           Response.Redirect("GP12HistoricoReferencia.aspx?REFERENCIA=" + detalleReferencia.Text);
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

