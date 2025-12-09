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
    public partial class InformeUltimasRevisiones : System.Web.UI.Page
    {

        private static DataSet ds_Referencias = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                Conexion_GP12 conexion = new Conexion_GP12();
                ds_Referencias = conexion.devuelve_ultimas_revisiones();
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
            catch (Exception ex)
            { 
                
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

            }
            catch (Exception ex)
            {

            }
        }

        protected void cargar_todas(object sender, EventArgs e)
        {

            Conexion_GP12 conexion = new Conexion_GP12();
            ds_Referencias = conexion.devuelve_ultimas_revisiones();
            dgv_AreaRechazo.DataSource = ds_Referencias;
            dgv_AreaRechazo.DataBind();
        }

        protected void cargar_Filtrados(object sender, EventArgs e)
        {

            string estado = Convert.ToString(lista_estado.SelectedValue);
            if (estado == "-") { estado = "";}
            string cliente = Convert.ToString(lista_clientes.SelectedValue);
            if (cliente == "-") {cliente = "";}

            /*int Referenciaseleccionada;
            int Moldeseleccionado;
            int Loteseleccionado;

            if (selectReferencia.Text == "")
                Referenciaseleccionada = null
                Replace('', null), out Referenciaseleccionada))
                 Referenciaseleccionada = Convert.ToDouble(selectReferencia.Text.Replace('', null));                
                else
                 Referenciaseleccionada = null;*/
            Conexion_GP12 conexion = new Conexion_GP12();
            ds_Referencias = conexion.devuelve_ultimas_revisiones_filtradas(selectReferencia.Text, selectMolde.Text, selectLote.Text,cliente,estado, selectFecha.Text);
            dgv_AreaRechazo.DataSource = ds_Referencias;
            dgv_AreaRechazo.DataBind();
            VerTodas.Visible = true;
        }

        public void GridViewCommandEventHandler(Object sender, GridViewCommandEventArgs e)
        {
           if (e.CommandName == "CargaDetalle")
            {
                Conexion_GP12 conexion = new Conexion_GP12();
                DataSet Modal = conexion.modal_detalles_ultimas_revisiones(e.CommandArgument.ToString());
                TbRevisadas.Text = Modal.Tables[0].Rows[0]["PiezasRevisadas"].ToString();
                TbBuenas.Text = Modal.Tables[0].Rows[0]["PiezasOK"].ToString();
                TbRetrabajadas.Text = Modal.Tables[0].Rows[0]["Retrabajadas"].ToString();
                TbMalas.Text = Modal.Tables[0].Rows[0]["PiezasNOK"].ToString();
                detalleReferencia.Text = Modal.Tables[0].Rows[0]["Referencia"].ToString();
                detalleReferenciaNombre.Text = Modal.Tables[0].Rows[0]["Nombre"].ToString();
                TbOperarioRevision.Text = Modal.Tables[0].Rows[0]["OperarioRevision"].ToString();
                TbEmpresaRevision.Text = Modal.Tables[0].Rows[0]["Proveedor"].ToString();
                Operario1.Text = Modal.Tables[0].Rows[0]["OPERARIO1"].ToString();
                Operario2.Text = Modal.Tables[0].Rows[0]["OPERARIO2"].ToString();
                Operario3.Text = Modal.Tables[0].Rows[0]["OPERARIO3"].ToString();
                Operario4.Text = Modal.Tables[0].Rows[0]["OPERARIO4"].ToString();
                CosteHoras.Text = Modal.Tables[0].Rows[0]["HORAS"].ToString();
                CosteChatarra.Text = Modal.Tables[0].Rows[0]["CosteScrapRevision"].ToString();
                CosteOperario.Text = Modal.Tables[0].Rows[0]["CostePiezaRevision"].ToString();
                CosteTotal.Text = Modal.Tables[0].Rows[0]["CosteRevision"].ToString();
                TDDefecto1.Text = Modal.Tables[0].Rows[0]["Def1"].ToString();
                ColorPorDefecto();
                if (TDDefecto1.Text != "0")
                {
                    THDefecto1.BackColor = System.Drawing.Color.Red;
                    TDDefecto1.BackColor = System.Drawing.Color.Red;
                    THDefecto1.ForeColor = System.Drawing.Color.White;
                    TDDefecto1.ForeColor = System.Drawing.Color.White;
                }
                TDDefecto2.Text = Modal.Tables[0].Rows[0]["Def2"].ToString();
                if (TDDefecto2.Text != "0")
                {
                    THDefecto2.BackColor = System.Drawing.Color.Red;
                    TDDefecto2.BackColor = System.Drawing.Color.Red;
                    THDefecto2.ForeColor = System.Drawing.Color.White;
                    TDDefecto2.ForeColor = System.Drawing.Color.White;
                }
                TDDefecto3.Text = Modal.Tables[0].Rows[0]["Def3"].ToString();
                if (TDDefecto3.Text != "0")
                {
                    THDefecto3.BackColor = System.Drawing.Color.Red;
                    TDDefecto3.BackColor = System.Drawing.Color.Red;
                    THDefecto3.ForeColor = System.Drawing.Color.White;
                    TDDefecto3.ForeColor = System.Drawing.Color.White;
                }
                TDDefecto4.Text = Modal.Tables[0].Rows[0]["Def4"].ToString();
                if (TDDefecto4.Text != "0")
                {
                    THDefecto4.BackColor = System.Drawing.Color.Red;
                    TDDefecto4.BackColor = System.Drawing.Color.Red;
                    THDefecto4.ForeColor = System.Drawing.Color.White;
                    TDDefecto4.ForeColor = System.Drawing.Color.White;
                }
                TDDefecto5.Text = Modal.Tables[0].Rows[0]["Def5"].ToString();
                if (TDDefecto5.Text != "0")
                {
                    THDefecto5.BackColor = System.Drawing.Color.Red;
                    TDDefecto5.BackColor = System.Drawing.Color.Red;
                    THDefecto5.ForeColor = System.Drawing.Color.White;
                    TDDefecto5.ForeColor = System.Drawing.Color.White;
                }
                TDDefecto6.Text = Modal.Tables[0].Rows[0]["Def6"].ToString();
                if (TDDefecto6.Text != "0")
                {
                    THDefecto6.BackColor = System.Drawing.Color.Red;
                    TDDefecto6.BackColor = System.Drawing.Color.Red;
                    THDefecto6.ForeColor = System.Drawing.Color.White;
                    TDDefecto6.ForeColor = System.Drawing.Color.White;
                }
                TDDefecto7.Text = Modal.Tables[0].Rows[0]["Def7"].ToString();
                if (TDDefecto7.Text != "0")
                {
                    THDefecto7.BackColor = System.Drawing.Color.Red;
                    TDDefecto7.BackColor = System.Drawing.Color.Red;
                    THDefecto7.ForeColor = System.Drawing.Color.White;
                    TDDefecto7.ForeColor = System.Drawing.Color.White;
                }
                TDDefecto8.Text = Modal.Tables[0].Rows[0]["Def8"].ToString();
                if (TDDefecto8.Text != "0")
                {
                    THDefecto8.BackColor = System.Drawing.Color.Red;
                    TDDefecto8.BackColor = System.Drawing.Color.Red;
                    THDefecto8.ForeColor = System.Drawing.Color.White;
                    TDDefecto8.ForeColor = System.Drawing.Color.White;
                }
                TDDefecto9.Text = Modal.Tables[0].Rows[0]["Def9"].ToString();
                if (TDDefecto9.Text != "0")
                {
                    THDefecto9.BackColor = System.Drawing.Color.Red;
                    TDDefecto9.BackColor = System.Drawing.Color.Red;
                    THDefecto9.ForeColor = System.Drawing.Color.White;
                    TDDefecto9.ForeColor = System.Drawing.Color.White;
                }
                TDDefecto10.Text = Modal.Tables[0].Rows[0]["Def10"].ToString();
                if (TDDefecto10.Text != "0")
                {
                    THDefecto10.BackColor = System.Drawing.Color.Red;
                    TDDefecto10.BackColor = System.Drawing.Color.Red;
                    THDefecto10.ForeColor = System.Drawing.Color.White;
                    TDDefecto10.ForeColor = System.Drawing.Color.White;
                }
                TDDefecto11.Text = Modal.Tables[0].Rows[0]["Def11"].ToString();
                if (TDDefecto11.Text != "0")
                {
                    THDefecto11.BackColor = System.Drawing.Color.Red;
                    TDDefecto11.BackColor = System.Drawing.Color.Red;
                    THDefecto11.ForeColor = System.Drawing.Color.White;
                    TDDefecto11.ForeColor = System.Drawing.Color.White;
                }
                TDDefecto12.Text = Modal.Tables[0].Rows[0]["Def12"].ToString();
                if (TDDefecto12.Text != "0")
                {
                    THDefecto12.BackColor = System.Drawing.Color.Red;
                    TDDefecto12.BackColor = System.Drawing.Color.Red;
                    THDefecto12.ForeColor = System.Drawing.Color.White;
                    TDDefecto12.ForeColor = System.Drawing.Color.White;
                }
                TDDefecto13.Text = Modal.Tables[0].Rows[0]["Def13"].ToString();
                if (TDDefecto13.Text != "0")
                {
                    THDefecto13.BackColor = System.Drawing.Color.Red;
                    TDDefecto13.BackColor = System.Drawing.Color.Red;
                    THDefecto13.ForeColor = System.Drawing.Color.White;
                    TDDefecto13.ForeColor = System.Drawing.Color.White;
                }
                TDDefecto14.Text = Modal.Tables[0].Rows[0]["Def14"].ToString();
                if (TDDefecto14.Text != "0")
                {
                    THDefecto14.BackColor = System.Drawing.Color.Red;
                    TDDefecto14.BackColor = System.Drawing.Color.Red;
                    THDefecto14.ForeColor = System.Drawing.Color.White;
                    TDDefecto14.ForeColor = System.Drawing.Color.White;
                }
                TDDefecto15.Text = Modal.Tables[0].Rows[0]["Def15"].ToString();
                if (TDDefecto15.Text != "0")
                {
                    THDefecto15.BackColor = System.Drawing.Color.Red;
                    TDDefecto15.BackColor = System.Drawing.Color.Red;
                    THDefecto15.ForeColor = System.Drawing.Color.White;
                    TDDefecto15.ForeColor = System.Drawing.Color.White;
                }
                TDDefecto16.Text = Modal.Tables[0].Rows[0]["Def16"].ToString();
                if (TDDefecto16.Text != "0")
                {
                    THDefecto16.BackColor = System.Drawing.Color.Red;
                    TDDefecto16.BackColor = System.Drawing.Color.Red;
                    THDefecto16.ForeColor = System.Drawing.Color.White;
                    TDDefecto16.ForeColor = System.Drawing.Color.White;
                }
                TDDefecto17.Text = Modal.Tables[0].Rows[0]["Def17"].ToString();
                if (TDDefecto17.Text != "0")
                {
                    THDefecto17.BackColor = System.Drawing.Color.Red;
                    TDDefecto17.BackColor = System.Drawing.Color.Red;
                    THDefecto17.ForeColor = System.Drawing.Color.White;
                    TDDefecto17.ForeColor = System.Drawing.Color.White;
                }
                TDDefecto18.Text = Modal.Tables[0].Rows[0]["Def18"].ToString();
                if (TDDefecto18.Text != "0")
                {
                    THDefecto18.BackColor = System.Drawing.Color.Red;
                    TDDefecto18.BackColor = System.Drawing.Color.Red;
                    THDefecto18.ForeColor = System.Drawing.Color.White;
                    TDDefecto18.ForeColor = System.Drawing.Color.White;
                }
                TDDefecto19.Text = Modal.Tables[0].Rows[0]["Def19"].ToString();
                if (TDDefecto19.Text != "0")
                {
                    THDefecto19.BackColor = System.Drawing.Color.Red;
                    TDDefecto19.BackColor = System.Drawing.Color.Red;
                    THDefecto19.ForeColor = System.Drawing.Color.White;
                    TDDefecto19.ForeColor = System.Drawing.Color.White;
                }
                TDDefecto20.Text = Modal.Tables[0].Rows[0]["Def20"].ToString();
                if (TDDefecto20.Text != "0")
                {
                    THDefecto20.BackColor = System.Drawing.Color.Red;
                    TDDefecto20.BackColor = System.Drawing.Color.Red;
                    THDefecto20.ForeColor = System.Drawing.Color.White;
                    TDDefecto20.ForeColor = System.Drawing.Color.White;
                }
                TDDefecto21.Text = Modal.Tables[0].Rows[0]["Def21"].ToString();
                if (TDDefecto21.Text != "0")
                {
                    THDefecto21.BackColor = System.Drawing.Color.Red;
                    TDDefecto21.BackColor = System.Drawing.Color.Red;
                    THDefecto21.ForeColor = System.Drawing.Color.White;
                    TDDefecto21.ForeColor = System.Drawing.Color.White;
                }
                TDDefecto22.Text = Modal.Tables[0].Rows[0]["Def22"].ToString();
                if (TDDefecto22.Text != "0")
                {
                    THDefecto22.BackColor = System.Drawing.Color.Red;
                    TDDefecto22.BackColor = System.Drawing.Color.Red;
                    THDefecto22.ForeColor = System.Drawing.Color.White;
                    TDDefecto22.ForeColor = System.Drawing.Color.White;
                }
                TDDefecto23.Text = Modal.Tables[0].Rows[0]["Def23"].ToString();
                if (TDDefecto23.Text != "0")
                {
                    THDefecto23.BackColor = System.Drawing.Color.Red;
                    TDDefecto23.BackColor = System.Drawing.Color.Red;
                    THDefecto23.ForeColor = System.Drawing.Color.White;
                    TDDefecto23.ForeColor = System.Drawing.Color.White;
                }
                TDDefecto24.Text = Modal.Tables[0].Rows[0]["Def24"].ToString();
                if (TDDefecto24.Text != "0")
                {
                    THDefecto24.BackColor = System.Drawing.Color.Red;
                    TDDefecto24.BackColor = System.Drawing.Color.Red;
                    THDefecto24.ForeColor = System.Drawing.Color.White;
                    TDDefecto24.ForeColor = System.Drawing.Color.White;
                }
                TDDefecto25.Text = Modal.Tables[0].Rows[0]["Def25"].ToString();
                if (TDDefecto25.Text != "0")
                {
                    THDefecto25.BackColor = System.Drawing.Color.Red;
                    TDDefecto25.BackColor = System.Drawing.Color.Red;
                    THDefecto25.ForeColor = System.Drawing.Color.White;
                    TDDefecto25.ForeColor = System.Drawing.Color.White;
                }
                TDDefecto26.Text = Modal.Tables[0].Rows[0]["Def26"].ToString();
                if (TDDefecto26.Text != "0")
                {
                    THDefecto26.BackColor = System.Drawing.Color.Red;
                    TDDefecto26.BackColor = System.Drawing.Color.Red;
                    THDefecto26.ForeColor = System.Drawing.Color.White;
                    TDDefecto26.ForeColor = System.Drawing.Color.White;
                }
                TDDefecto27.Text = Modal.Tables[0].Rows[0]["Def27"].ToString();
                if (TDDefecto27.Text != "0")
                {
                    THDefecto27.BackColor = System.Drawing.Color.Red;
                    TDDefecto27.BackColor = System.Drawing.Color.Red;
                    THDefecto27.ForeColor = System.Drawing.Color.White;
                    TDDefecto27.ForeColor = System.Drawing.Color.White;
                }
                TDDefecto28.Text = Modal.Tables[0].Rows[0]["Def28"].ToString();
                if (TDDefecto28.Text != "0")
                {
                    THDefecto28.BackColor = System.Drawing.Color.Red;
                    TDDefecto28.BackColor = System.Drawing.Color.Red;
                    THDefecto28.ForeColor = System.Drawing.Color.White;
                    TDDefecto28.ForeColor = System.Drawing.Color.White;
                }
                string imagen1 = Modal.Tables[0].Rows[0]["ImagenDefecto1"].ToString();
                    linkimagen1.NavigateUrl = imagen1;
                    linkimagen1.ImageUrl = imagen1;
                string imagen2 = Modal.Tables[0].Rows[0]["ImagenDefecto2"].ToString();
                    linkimagen2.NavigateUrl = imagen2;
                    linkimagen2.ImageUrl = imagen2;
                string imagen3 = Modal.Tables[0].Rows[0]["ImagenDefecto3"].ToString();
                    linkimagen3.NavigateUrl = imagen3;
                    linkimagen3.ImageUrl = imagen3;   
               lbAbrir_Modal(null, EventArgs.Empty);
                //Button_Click(Button3, EventArgs.Empty);
                //Button3.Attributes["data-toggle"] = "modal";
                //document.getElementById('<%= btnDelete.ClientID %>').click();
                //Response.Redirect("FichasParametros.aspx?REFERENCIA=" + e.CommandArgument.ToString());
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#mymodal').modal('show');</script>", false);
                //ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopup();", true);

            }
        }

        protected void lbAbrir_Modal(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        protected void ColorPorDefecto()
        {
            THDefecto1.BackColor = TbRevisadas.BackColor;
            THDefecto1.ForeColor = TbRevisadas.ForeColor;
            TDDefecto1.BackColor = TbRevisadas.BackColor;
            TDDefecto1.ForeColor = TbRevisadas.ForeColor;

            THDefecto2.BackColor = TbRevisadas.BackColor;
            THDefecto2.ForeColor = TbRevisadas.ForeColor;
            TDDefecto2.BackColor = TbRevisadas.BackColor;
            TDDefecto2.ForeColor = TbRevisadas.ForeColor;

            THDefecto3.BackColor = TbRevisadas.BackColor;
            THDefecto3.ForeColor = TbRevisadas.ForeColor;
            TDDefecto3.BackColor = TbRevisadas.BackColor;
            TDDefecto3.ForeColor = TbRevisadas.ForeColor;

            THDefecto4.BackColor = TbRevisadas.BackColor;
            THDefecto4.ForeColor = TbRevisadas.ForeColor;
            TDDefecto4.BackColor = TbRevisadas.BackColor;
            TDDefecto4.ForeColor = TbRevisadas.ForeColor;

            THDefecto5.BackColor = TbRevisadas.BackColor;
            THDefecto5.ForeColor = TbRevisadas.ForeColor;
            TDDefecto5.BackColor = TbRevisadas.BackColor;
            TDDefecto5.ForeColor = TbRevisadas.ForeColor;

            THDefecto6.BackColor = TbRevisadas.BackColor;
            THDefecto6.ForeColor = TbRevisadas.ForeColor;
            TDDefecto6.BackColor = TbRevisadas.BackColor;
            TDDefecto6.ForeColor = TbRevisadas.ForeColor;

            THDefecto7.BackColor = TbRevisadas.BackColor;
            THDefecto7.ForeColor = TbRevisadas.ForeColor;
            TDDefecto7.BackColor = TbRevisadas.BackColor;
            TDDefecto7.ForeColor = TbRevisadas.ForeColor;

            THDefecto8.BackColor = TbRevisadas.BackColor;
            THDefecto8.ForeColor = TbRevisadas.ForeColor;
            TDDefecto8.BackColor = TbRevisadas.BackColor;
            TDDefecto8.ForeColor = TbRevisadas.ForeColor;

            THDefecto9.BackColor = TbRevisadas.BackColor;
            THDefecto9.ForeColor = TbRevisadas.ForeColor;
            TDDefecto9.BackColor = TbRevisadas.BackColor;
            TDDefecto9.ForeColor = TbRevisadas.ForeColor;

            THDefecto10.BackColor = TbRevisadas.BackColor;
            THDefecto10.ForeColor = TbRevisadas.ForeColor;
            TDDefecto10.BackColor = TbRevisadas.BackColor;
            TDDefecto10.ForeColor = TbRevisadas.ForeColor;

            THDefecto11.BackColor = TbRevisadas.BackColor;
            THDefecto11.ForeColor = TbRevisadas.ForeColor;
            TDDefecto11.BackColor = TbRevisadas.BackColor;
            TDDefecto11.ForeColor = TbRevisadas.ForeColor;

            THDefecto12.BackColor = TbRevisadas.BackColor;
            THDefecto12.ForeColor = TbRevisadas.ForeColor;
            TDDefecto12.BackColor = TbRevisadas.BackColor;
            TDDefecto12.ForeColor = TbRevisadas.ForeColor;

            THDefecto13.BackColor = TbRevisadas.BackColor;
            THDefecto13.ForeColor = TbRevisadas.ForeColor;
            TDDefecto13.BackColor = TbRevisadas.BackColor;
            TDDefecto13.ForeColor = TbRevisadas.ForeColor;

            THDefecto14.BackColor = TbRevisadas.BackColor;
            THDefecto14.ForeColor = TbRevisadas.ForeColor;
            TDDefecto14.BackColor = TbRevisadas.BackColor;
            TDDefecto14.ForeColor = TbRevisadas.ForeColor;

            THDefecto15.BackColor = TbRevisadas.BackColor;
            THDefecto15.ForeColor = TbRevisadas.ForeColor;
            TDDefecto15.BackColor = TbRevisadas.BackColor;
            TDDefecto15.ForeColor = TbRevisadas.ForeColor;

            THDefecto16.BackColor = TbRevisadas.BackColor;
            THDefecto16.ForeColor = TbRevisadas.ForeColor;
            TDDefecto16.BackColor = TbRevisadas.BackColor;
            TDDefecto16.ForeColor = TbRevisadas.ForeColor;

            THDefecto17.BackColor = TbRevisadas.BackColor;
            THDefecto17.ForeColor = TbRevisadas.ForeColor;
            TDDefecto17.BackColor = TbRevisadas.BackColor;
            TDDefecto17.ForeColor = TbRevisadas.ForeColor;

            THDefecto18.BackColor = TbRevisadas.BackColor;
            THDefecto18.ForeColor = TbRevisadas.ForeColor;
            TDDefecto18.BackColor = TbRevisadas.BackColor;
            TDDefecto18.ForeColor = TbRevisadas.ForeColor;

            THDefecto19.BackColor = TbRevisadas.BackColor;
            THDefecto19.ForeColor = TbRevisadas.ForeColor;
            TDDefecto19.BackColor = TbRevisadas.BackColor;
            TDDefecto19.ForeColor = TbRevisadas.ForeColor;

            THDefecto20.BackColor = TbRevisadas.BackColor;
            THDefecto20.ForeColor = TbRevisadas.ForeColor;
            TDDefecto20.BackColor = TbRevisadas.BackColor;
            TDDefecto20.ForeColor = TbRevisadas.ForeColor;

            THDefecto21.BackColor = TbRevisadas.BackColor;
            THDefecto21.ForeColor = TbRevisadas.ForeColor;
            TDDefecto21.BackColor = TbRevisadas.BackColor;
            TDDefecto21.ForeColor = TbRevisadas.ForeColor;

            THDefecto22.BackColor = TbRevisadas.BackColor;
            THDefecto22.ForeColor = TbRevisadas.ForeColor;
            TDDefecto22.BackColor = TbRevisadas.BackColor;
            TDDefecto22.ForeColor = TbRevisadas.ForeColor;

            THDefecto23.BackColor = TbRevisadas.BackColor;
            THDefecto23.ForeColor = TbRevisadas.ForeColor;
            TDDefecto23.BackColor = TbRevisadas.BackColor;
            TDDefecto23.ForeColor = TbRevisadas.ForeColor;

            THDefecto24.BackColor = TbRevisadas.BackColor;
            THDefecto24.ForeColor = TbRevisadas.ForeColor;
            TDDefecto24.BackColor = TbRevisadas.BackColor;
            TDDefecto24.ForeColor = TbRevisadas.ForeColor;

            THDefecto25.BackColor = TbRevisadas.BackColor;
            THDefecto25.ForeColor = TbRevisadas.ForeColor;
            TDDefecto25.BackColor = TbRevisadas.BackColor;
            TDDefecto25.ForeColor = TbRevisadas.ForeColor;

            THDefecto26.BackColor = TbRevisadas.BackColor;
            THDefecto26.ForeColor = TbRevisadas.ForeColor;
            TDDefecto26.BackColor = TbRevisadas.BackColor;
            TDDefecto26.ForeColor = TbRevisadas.ForeColor;

            THDefecto27.BackColor = TbRevisadas.BackColor;
            THDefecto27.ForeColor = TbRevisadas.ForeColor;
            TDDefecto27.BackColor = TbRevisadas.BackColor;
            TDDefecto27.ForeColor = TbRevisadas.ForeColor;

            THDefecto28.BackColor = TbRevisadas.BackColor;
            THDefecto28.ForeColor = TbRevisadas.ForeColor;
            TDDefecto28.BackColor = TbRevisadas.BackColor;
            TDDefecto28.ForeColor = TbRevisadas.ForeColor;
        }
        /*public void GridView_RowDeleting(Object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                            Conexion_GP12 conexion = new Conexion_GP12();
            ds_Referencias = conexion.devuelve_ultimas_revisiones();
            dgv_AreaRechazo.DataSource = ds_Referencias;
            dgv_AreaRechazo.DataBind();
                
                }
            
            catch (Exception ex)
            {

            }
        }
         */

        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            for (int i = 0; i <= dgv_AreaRechazo.Rows.Count - 1; i++)
            {
                Label lblparent = (Label)dgv_AreaRechazo.Rows[i].FindControl("lblMalas");

                if (lblparent.Text != "0")
                {
                    dgv_AreaRechazo.Rows[i].Cells[9].BackColor = System.Drawing.Color.Red;
                    lblparent.ForeColor = System.Drawing.Color.White;
                }

                Label lblparent2 = (Label)dgv_AreaRechazo.Rows[i].FindControl("lblRetrabajadas");

                if (lblparent2.Text != "0")
                {
                    dgv_AreaRechazo.Rows[i].Cells[8].BackColor = System.Drawing.Color.Yellow;
                    lblparent2.ForeColor = System.Drawing.Color.Black;
                }

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