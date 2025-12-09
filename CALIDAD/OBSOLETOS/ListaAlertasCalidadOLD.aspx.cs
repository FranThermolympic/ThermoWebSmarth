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

namespace ThermoWeb.CALIDAD
{
    public partial class ListaAlertasCalidadOLD : System.Web.UI.Page
    {

        private static DataSet ds_Referencias = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Conexion_CALIDAD conexion = new Conexion_CALIDAD();
                
                ds_Referencias = conexion.Devuelve_Listado_NoConformidadesSMARTH(selecaño.SelectedValue);
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

        public void CargaListasFiltro()
        {
            try
            {
                Conexion_CALIDAD conexion = new Conexion_CALIDAD();
            
                DataSet clientes = new DataSet();
                clientes = conexion.devuelve_lista_clientes();
                lista_clientes.Items.Add("-");
                foreach (DataRow row in clientes.Tables[0].Rows) { lista_clientes.Items.Add(row["Cliente"].ToString()); }
                lista_clientes.ClearSelection();
                lista_clientes.SelectedValue = "-";


                DataSet responsable = new DataSet();
                responsable = conexion.Devuelve_setlista_responsablesSMARTH();
                foreach (DataRow row in responsable.Tables[0].Rows) { lista_responsable.Items.Add(row["PAprobado"].ToString()); }
                lista_responsable.ClearSelection();
                lista_responsable.SelectedValue = "-";


            }
            catch (Exception)
            {

            }
        }
        public void ImportardeBMS(object sender, EventArgs e)
        {

            try
            {
                Conexion_CALIDAD conexion = new Conexion_CALIDAD();
                conexion.leer_productosBMS();
                conexion.leer_operariosNAV();
                conexion.InsertaProductosReferenciaEstados();
                conexion.InsertaProductosTablaDocumentos();
                ds_Referencias = conexion.leer_ReferenciaEstadosActivas();
                rellenar_grid();
            }
            catch (Exception)
            {

            }


        }

        public void CargarFiltrados(object sender, EventArgs e)
        {
            Conexion_CALIDAD conexion = new Conexion_CALIDAD();
            string estado = "";
                if (lista_estado.SelectedValue == "1") //Cerrados
                {
                    estado = " AND ((D3CIERRE IS NOT NULL AND D3CIERRE <> '') AND(D6CIERRE IS NOT NULL AND D6CIERRE <> '') AND(D8CIERRE IS NOT NULL AND D8CIERRE <> ''))";
                }
            else if (lista_estado.SelectedValue == "2") //En curso
                {
                    estado = " AND((D3CIERRE IS NULL OR D3CIERRE = '') OR(D6CIERRE IS NULL OR D6CIERRE = '') OR(D8CIERRE IS NULL OR D8CIERRE = '')) AND (CHECKD6 = 1 OR CHECKD8 = 1)";
                }
            else if (lista_estado.SelectedValue == "3") // Vencidos
                {
                    estado = " AND ((D3 < SYSDATETIME() AND (D3CIERRE IS NULL OR D3CIERRE = ''))" +
                             " OR(CASE WHEN D6 = 'N/A' THEN '01/01/2900 0:00:00' WHEN D6 = '' THEN '01/01/1900 0:00:00' ELSE cast(d6 as datetime) end < SYSDATETIME() AND(D6CIERRE IS NULL OR D6CIERRE = '') AND CHECKD6 = 1)" +
                             " OR(CASE WHEN D8 = 'N/A' THEN '01/01/2900 0:00:00' WHEN D8 = '' THEN '01/01/1900 0:00:00' ELSE cast(d8 as datetime) end < SYSDATETIME() AND(D8CIERRE IS NULL OR D8CIERRE = '')) AND CHECKD8 = 1)";				
                }
                else
                {
                    estado = "";            
                }

            string cliente = "";
                if (lista_clientes.SelectedValue.ToString() != "-")
                {
                    cliente = " AND CLIENTE LIKE '" + lista_clientes.SelectedValue.ToString() + "%'";
                }
                else
                {
                    cliente = "";
                }

            string piloto = "";
                if (lista_responsable.SelectedValue.ToString() != "-")
                {
                    piloto = " AND PilotoIngenieria LIKE '" + conexion.Devuelve_IDlista_responsablesSMARTH(lista_responsable.SelectedValue.ToString()) + "'";
                }
                else
                {
                    piloto = "";
                }

            string escalado = "";
                if (NivelAlerta.SelectedValue != "0")
                    {
                    escalado = " AND EscaladoNoConformidad = " + NivelAlerta.SelectedValue + "";
                    }
                else
                    {
                    escalado = "";
                    }

            string tipoNC = "";
                if (TipoAlerta.SelectedValue != "0")
                {
                    tipoNC = " AND TipoNoConformidad = " + TipoAlerta.SelectedValue + "";
                }
                else
                {
                    tipoNC = "";
                }

                ds_Referencias = conexion.Devuelve_Listado_NoConformidadesFiltradosSMARTH(estado, cliente, piloto, escalado, tipoNC, selecaño.SelectedValue);
                rellenar_grid();




        }

        public void CargarTodas(object sender, EventArgs e)
        {
            Conexion_CALIDAD conexion = new Conexion_CALIDAD();
                lista_responsable.SelectedValue = "-";
                NivelAlerta.SelectedValue = "-";
                TipoAlerta.SelectedValue = "-";
                lista_estado.SelectedValue = "-";
                lista_clientes.SelectedValue = "-";

            string estado = "";
                string cliente = "";
                string escalado = "";
                string tipoNC = "";
                string piloto = "";
            ds_Referencias = conexion.Devuelve_Listado_NoConformidadesFiltradosSMARTH(estado, cliente, piloto, escalado, tipoNC, selecaño.SelectedValue);
            rellenar_grid();

        }
        // guarda una fila
        public void GridView_RowUpdating(Object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                CheckBox D6Checked = (CheckBox)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("TXTD6TRANS");
                CheckBox D8Checked = (CheckBox)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("TXTD8TRANS");
                Label NConformdidad = (Label)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("txtNC");
                Label FechaOriginal = (Label)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("txtFecha");
                TextBox FechaD3PREV = (TextBox)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("TXTFechaD3");
                TextBox FechaD6PREV = (TextBox)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("TXTFechaD6");
                TextBox FechaD8PREV = (TextBox)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("TXTFechaD8");
                TextBox FechaD3Cierre = (TextBox)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("txtFechaD3ent");
                TextBox FechaD6Cierre = (TextBox)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("txtFechaD6ent");
                TextBox FechaD8Cierre = (TextBox)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("txtFechaD8ent");
                DropDownList escalado = (DropDownList)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("NivelAlerta");

                TextBox NCObservaciones = (TextBox)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("lblNCObservaciones");
                string D3Prevision = FechaD3PREV.Text;
                string D3Cierre = FechaD3Cierre.Text;
                string D6Prevision = FechaD6PREV.Text;
                string D6Cierre = FechaD6Cierre.Text;
                string D8Prevision = FechaD8PREV.Text;
                string D8Cierre = FechaD8Cierre.Text;


                //EVALUO ESCENARIOS D3PREVISTO
                if (FechaD3PREV.Text == "")
                {
                    DateTime D3Prev = DateTime.Parse(FechaOriginal.Text);
                    D3Prevision = D3Prev.AddDays(1).ToString("dd/MM/yyyy");
                }
                //else if (FechaD3PREV.Text == "N/A")
                //    {
                //    D3Prevision = "N/A";
                //    }
                else
                {
                    D3Prevision = Convert.ToString(DateTime.Parse(FechaD3PREV.Text));
                }

                //EVALUO ESCENARIOS D3REAL
                if (FechaD3Cierre.Text == "")
                {
                    D3Cierre = "";
                }
                else
                {
                    //D3Cierre = FechaD3Cierre.ToString();
                    D3Cierre = Convert.ToString(DateTime.Parse(FechaD3Cierre.Text));
                }


                /*
                CÓDIGO ORIGINAL
                //EVALUO ESCENARIOS D6PREVISION
                if (FechaD6PREV.Text == "" && D6Checked.Checked == true)
                {
                    DateTime D6Prev = DateTime.Parse(FechaOriginal.Text);
                    D6Prevision = D6Prev.AddDays(10).ToString("dd/MM/yyyy");
                }
                else if (FechaD6PREV.Text == "N/A" || D6Checked.Checked == false)
                {
                    D6Prevision = "N/A";
                }
                else
                {
                    D6Prevision = Convert.ToString(DateTime.Parse(FechaD6PREV.Text));
                }
                //EVALUO ESCENARIOS D6REALES
                if (FechaD6Cierre.Text == "" && D6Checked.Checked == true)
                {
                    D6Cierre = "";
                }
                else if (FechaD6Cierre.Text == "N/A" || D6Checked.Checked == false)
                {
                    D6Cierre = "N/A";
                }
                else
                {
                    D6Cierre = Convert.ToString(DateTime.Parse(FechaD6Cierre.Text));
                }

                //EVALUO ESCENARIOS D8PREVISION
                if (FechaD8PREV.Text == "" && D8Checked.Checked == true)
                {
                    DateTime D8Prev = DateTime.Parse(FechaOriginal.Text);
                    D8Prevision = D8Prev.AddDays(60).ToString("dd/MM/yyyy");
                }
                else if (FechaD8PREV.Text == "N/A" || D8Checked.Checked == false)
                {
                    D8Prevision = "N/A";
                }
                else
                {
                    D8Prevision = Convert.ToString(DateTime.Parse(FechaD8PREV.Text));
                }
                //EVALUO ESCENARIOS D8REALES
                if (FechaD8Cierre.Text == "" && D8Checked.Checked == true)
                {
                    D8Cierre = "";
                }
                else if (FechaD8Cierre.Text == "N/A" || D8Checked.Checked == false)
                {
                    D8Cierre = "N/A";
                }
                else
                {
                    D8Cierre = Convert.ToString(DateTime.Parse(FechaD8Cierre.Text));
                }
                */
                if ((FechaD6PREV.Text == "" || FechaD6PREV.Text == "N/A") && D6Checked.Checked == true)
                {
                    DateTime D6Prev = DateTime.Parse(FechaOriginal.Text);
                    D6Prevision = D6Prev.AddDays(10).ToString("dd/MM/yyyy");
                }
                else if ((FechaD6PREV.Text == "" || FechaD6PREV.Text == "N/A") && D6Checked.Checked == false)
                {
                    D6Prevision = "";
                }
                else
                {
                    D6Prevision = Convert.ToString(DateTime.Parse(FechaD6PREV.Text));
                }
                //EVALUO ESCENARIOS D6REALES
                if ((FechaD6Cierre.Text == "" || FechaD6Cierre.Text == "N/A") && D6Checked.Checked == true)
                {
                    D6Cierre = "";
                }
                else if ((FechaD6Cierre.Text == "" || FechaD6Cierre.Text == "N/A") && D6Checked.Checked == false)
                {
                    D6Cierre = "";
                }
                else
                {
                    D6Cierre = Convert.ToString(DateTime.Parse(FechaD6Cierre.Text));
                }

                //EVALUO ESCENARIOS D8PREVISION
                if ((FechaD8PREV.Text == "" || FechaD8PREV.Text == "N/A") && D8Checked.Checked == true)
                {
                    DateTime D8Prev = DateTime.Parse(FechaOriginal.Text);
                    D8Prevision = D8Prev.AddDays(60).ToString("dd/MM/yyyy");
                }
                else if ((FechaD8PREV.Text == "" || FechaD8PREV.Text == "N/A") && D8Checked.Checked == false)
                {
                    D8Prevision = "";
                }
                else
                {
                    D8Prevision = Convert.ToString(DateTime.Parse(FechaD8PREV.Text));
                }
                //EVALUO ESCENARIOS D8REALES
                if ((FechaD8Cierre.Text == "" || FechaD8Cierre.Text == "N/A") && D8Checked.Checked == true)
                {
                    D8Cierre = "";
                }
                else if ((FechaD8Cierre.Text == "" || FechaD8Cierre.Text == "N/A") && D8Checked.Checked == false)
                {
                    D8Cierre = "";
                }
                else
                {
                    D8Cierre = Convert.ToString(DateTime.Parse(FechaD8Cierre.Text));
                }

                int CheckCorte = 1;
                int CheckD3 = 1;
                int CheckD6 = 1;
                    if (D6Checked.Checked == false)
                        {
                            CheckD6 = 0;
                        }
                int CheckD8 = 1;
                    if (D8Checked.Checked == false)
                    {
                        CheckD8 = 0;
                    }


                Conexion_CALIDAD conexion = new Conexion_CALIDAD();
                
                conexion.Actualiza_Listado_NoConformidades(Convert.ToInt32(NConformdidad.Text), Convert.ToInt32(escalado.SelectedIndex), D3Prevision, D3Cierre, D6Prevision, D6Cierre, D8Prevision, D8Cierre, NCObservaciones.Text, CheckCorte, CheckD3,CheckD6,CheckD8);
                dgv_AreaRechazo.EditIndex = -1;
                ds_Referencias = conexion.Devuelve_Listado_NoConformidadesSMARTH(selecaño.SelectedValue);
                dgv_AreaRechazo.DataSource = ds_Referencias;
                dgv_AreaRechazo.DataBind();
             
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

        public void GridView_RowDeleting(Object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Label NConformdidad = (Label)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("txtNC");
                Conexion_CALIDAD conexion = new Conexion_CALIDAD();
                dgv_AreaRechazo.EditIndex = -1;
                conexion.elimina_alerta_NoConformidad(NConformdidad.Text);

                ds_Referencias = conexion.Devuelve_Listado_NoConformidadesSMARTH(selecaño.SelectedValue);
                dgv_AreaRechazo.DataSource = ds_Referencias;
                dgv_AreaRechazo.DataBind();
                /*
                int num_fila = Convert.ToInt16(e.RowIndex);
                int i = 0;
                foreach (DataRow row in ds_area.Tables[0].Rows)
                {
                    if (i == num_fila)
                    {
                        Conexion_CALIDAD conexion = new Conexion_CALIDAD();
                        conexion.eliminar_area_rechazo(Convert.ToInt32(row["Id"]));
                        ds_area = conexion.leer_area_rechazo();
                        rellenar_grid();
                        break;
                    }
                    i++;
                    
                }*/

            }
            catch (Exception)
            {

            }
        }
        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if ((e.Row.RowState == DataControlRowState.Normal) || (e.Row.RowState == DataControlRowState.Alternate))
                    {
                        Label DEFRepetitivo = (Label)e.Row.FindControl("lblReiteraDefecto");
                        Label ProdRepetitivo = (Label)e.Row.FindControl("lblReiteraProducto");

                        if (DEFRepetitivo.Text == "1")
                        {
                            
                            DEFRepetitivo.Text = " Def. recurrente";
                            DEFRepetitivo.Visible = true;
                        }
                        if (ProdRepetitivo.Text == "1")
                        {
                            ProdRepetitivo.Text = " Prod. recurrente";
                            ProdRepetitivo.Visible = true;
                        }



                        Label PrevD3 = (Label)e.Row.FindControl("lblFechaD3");
                        Label RealD3 = (Label)e.Row.FindControl("lblFechaD3ent");
                        Label ColorD3 = (Label)e.Row.FindControl("D3COLOR");

                        if (RealD3.Text == "" && DateTime.Now > DateTime.Parse(PrevD3.Text)) //CASO NOK ROJO
                        {
                            ColorD3.ForeColor = System.Drawing.Color.White;
                            ColorD3.BackColor = System.Drawing.Color.Red;
                            ColorD3.BorderColor = System.Drawing.Color.Red;
                        }
                        else if (RealD3.Text == "" && DateTime.Now < DateTime.Parse(PrevD3.Text)) //CASO A TIEMPO GRIS
                        {
                            ColorD3.ForeColor = System.Drawing.Color.White;
                            ColorD3.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                            ColorD3.BorderColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        }
                        else //CASO OK
                        {
                            ColorD3.ForeColor = System.Drawing.Color.White;
                            ColorD3.BackColor = System.Drawing.ColorTranslator.FromHtml("#33cc33");
                            ColorD3.BorderColor = System.Drawing.ColorTranslator.FromHtml("#33cc33");
                        }

                        Label PrevD6 = (Label)e.Row.FindControl("lblFechaD6");
                        Label RealD6 = (Label)e.Row.FindControl("lblFechaD6ent");
                        Label ColorD6 = (Label)e.Row.FindControl("D6COLOR");


                        if (RealD6.Text == "" && DateTime.Now > DateTime.Parse(PrevD6.Text)) //CASO NOK ROJO
                        {
                            ColorD6.ForeColor = System.Drawing.Color.White;
                            ColorD6.BackColor = System.Drawing.Color.Red;
                            ColorD6.BorderColor = System.Drawing.Color.Red;
                        }
                        else if (RealD6.Text == "" && DateTime.Now < DateTime.Parse(PrevD6.Text)) //CASO A TIEMPO GRIS
                        {
                            ColorD6.ForeColor = System.Drawing.Color.White;
                            ColorD6.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                            ColorD6.BorderColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        }
                        else //CASO OK
                        {
                            ColorD6.ForeColor = System.Drawing.Color.White;
                            ColorD6.BackColor = System.Drawing.ColorTranslator.FromHtml("#33cc33");
                            ColorD6.BorderColor = System.Drawing.ColorTranslator.FromHtml("#33cc33");
                        }

                        Label PrevD8 = (Label)e.Row.FindControl("lblFechaD8");
                        Label RealD8 = (Label)e.Row.FindControl("lblFechaD8ent");
                        Label ColorD8 = (Label)e.Row.FindControl("D8COLOR");


                        if (RealD8.Text == "" && DateTime.Now > DateTime.Parse(PrevD8.Text)) //CASO NOK ROJO
                        {
                            ColorD8.ForeColor = System.Drawing.Color.White;
                            ColorD8.BackColor = System.Drawing.Color.Red;
                            ColorD8.BorderColor = System.Drawing.Color.Red;
                        }
                        else if (RealD8.Text == "" && DateTime.Now < DateTime.Parse(PrevD8.Text)) //CASO A TIEMPO GRIS
                        {
                            ColorD8.ForeColor = System.Drawing.Color.White;
                            ColorD8.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                            ColorD8.BorderColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        }
                        else //CASO OK
                        {
                            ColorD8.ForeColor = System.Drawing.Color.White;
                            ColorD8.BackColor = System.Drawing.ColorTranslator.FromHtml("#33cc33");
                            ColorD8.BorderColor = System.Drawing.ColorTranslator.FromHtml("#33cc33");
                        }




                    }

                    if (e.Row.RowState.HasFlag(DataControlRowState.Edit))

                    {
                        DropDownList escalado = (DropDownList)e.Row.FindControl("NivelAlerta");
                        DataRowView dr = e.Row.DataItem as DataRowView;
                        escalado.SelectedIndex = Convert.ToInt32(dr["EscaladoNoConformidad"]);

                        TextBox EditD6 = (TextBox)e.Row.FindControl("TXTFechaD6");
                        CheckBox D6Checkbox = (CheckBox)e.Row.FindControl("TXTD6TRANS");
                            if (EditD6.Text == "N/A")
                            {
                            D6Checkbox.Checked = false;   
                            }

                        TextBox EditD8 = (TextBox)e.Row.FindControl("TXTFechaD8");
                        CheckBox D8Checkbox = (CheckBox)e.Row.FindControl("TXTD8TRANS");
                        if (EditD8.Text == "N/A")
                        {
                            D8Checkbox.Checked = false;
                        }

                    }
                }
                
            }
            catch (Exception)
            { 
            }
        }

        public void GridViewCommandEventHandler(Object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "CargaDetalle")
                {
                    
                    Conexion_CALIDAD conexion = new Conexion_CALIDAD();
                        string IDNoconformidad = e.CommandArgument.ToString();
                        DataSet Modal = conexion.Devuelve_Detalle_NoConformidad(IDNoconformidad);
                            IDNoConformidad.Text = "NC-" + Modal.Tables[0].Rows[0]["IdNoConformidad"].ToString();
                            IDNoConformidadSM.Text = Modal.Tables[0].Rows[0]["IdNoConformidad"].ToString();
                            tbEXTSeleccion.Text = Modal.Tables[0].Rows[0]["CosteSeleccionEXT"].ToString();
                            tbEXTScrap.Text = Modal.Tables[0].Rows[0]["CostePiezasNOKEXT"].ToString();
                            tbEXTCargos.Text = Modal.Tables[0].Rows[0]["CosteCargosEXT"].ToString();
                            tbEXTAdmon.Text = Modal.Tables[0].Rows[0]["CosteAdmonEXT"].ToString();
                            tbINTSeleccion.Text = Modal.Tables[0].Rows[0]["CosteSeleccionINT"].ToString();
                            tbINTOtros.Text = Modal.Tables[0].Rows[0]["CosteOtrosINT"].ToString();
                            EvidenciaCorte.Text = Modal.Tables[0].Rows[0]["DOCPuntoCorte"].ToString();
                            tbD3Text.Text = Modal.Tables[0].Rows[0]["DOCD3"].ToString();
                            tbD6Text.Text = Modal.Tables[0].Rows[0]["DOCD6"].ToString();
                            tbD8Text.Text = Modal.Tables[0].Rows[0]["DOCD8"].ToString();
                        D3FECHASUBIDA.Text = Modal.Tables[0].Rows[0]["D3CIERRE"].ToString();
                        D6FECHASUBIDA.Text = Modal.Tables[0].Rows[0]["D6CIERRE"].ToString();
                        D8FECHASUBIDA.Text = Modal.Tables[0].Rows[0]["D8CIERRE"].ToString();
                    D6CheckboxEvidencias.Checked = Convert.ToBoolean(Modal.Tables[0].Rows[0]["CheckD6"]);
                    D8CheckboxEvidencias.Checked = Convert.ToBoolean(Modal.Tables[0].Rows[0]["CheckD8"]);
                    /*
                        if (D6FECHASUBIDA.Text != "N/A")
                        { D6CheckboxEvidencias.Checked = true; }
                        else
                        { D6CheckboxEvidencias.Checked = false; }
                        if (D8FECHASUBIDA.Text != "N/A")
                        { D8CheckboxEvidencias.Checked = true; }
                        else
                        { D8CheckboxEvidencias.Checked = false; }
                    */

                    lbAbrir_Modal(null, EventArgs.Empty);

                    //Button_Click(Button3, EventArgs.Empty);
                    //Button3.Attributes["data-toggle"] = "modal";
                    //document.getElementById('<%= btnDelete.ClientID %>').click();
                    //Response.Redirect("FichasParametros.aspx?REFERENCIA=" + e.CommandArgument.ToString());
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#mymodal').modal('show');</script>", false);
                    //ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopup();", true);

                }

                if (e.CommandName == "Redirect")
                {
                    Response.Redirect("Alertas_Calidad.aspx?NOCONFORMIDAD=" + e.CommandArgument.ToString());
                }
            }
            catch (Exception)
            { }

        }

        protected void lbAbrir_Modal(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        //**\\EVIDENCIAS**\\
        public void insertar_documento(Object sender, EventArgs e)
        {
            try
            {

                if (FileUpload1.HasFile)
                    SaveFile(FileUpload1.PostedFile, 1);
                if (FileUpload2.HasFile)
                    SaveFile(FileUpload2.PostedFile, 2);
                if (FileUpload3.HasFile)
                    SaveFile(FileUpload3.PostedFile, 3);
                if (FileUpload4.HasFile)
                    SaveFile(FileUpload4.PostedFile, 4);
                if (FileUpload5.HasFile)
                    SaveFile(FileUpload5.PostedFile, 5);

            }
            catch (Exception)
            {

            }
        }

        private void SaveFile(HttpPostedFile file, int num_img)
        {
            try
            {
                // Specify the path to save the uploaded file to.
                //string savePath = "C:\\inetpub_thermoweb\\DOCUMENTAL\\DOCUMENTOS\\";
                string savePath = "C:\\inetpub_thermoweb\\DOCUMENTAL\\DOCUMENTOS\\";
               

                // Get the name of the file to upload.
                //string fileName = FileUpload1.FileName;
                string fileName = "";
                switch (num_img)
                {
                    case 1:
                        string extension = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
                        fileName = "NC-" + IDNoConformidadSM.Text + "_PUNTO_DE_CORTE" + extension;
                        break;
                    case 2:
                        string extension2 = System.IO.Path.GetExtension(FileUpload2.PostedFile.FileName);
                        fileName = "NC-" + IDNoConformidadSM.Text + "_D3" + extension2;
                        break;
                    case 3:
                        string extension3 = System.IO.Path.GetExtension(FileUpload3.PostedFile.FileName);
                        fileName = "NC-" + IDNoConformidadSM.Text + "_D6" + extension3;
                        break;
                    case 4:
                        string extension4 = System.IO.Path.GetExtension(FileUpload4.PostedFile.FileName);
                        fileName = "NC-" + IDNoConformidadSM.Text + "_D8" + extension4;
                        break;
                    case 5:
                        string extension5 = System.IO.Path.GetExtension(FileUpload5.PostedFile.FileName);
                        fileName = "NC-" + IDNoConformidadSM.Text + "_EVIDENCIAS" + extension5;
                        break;
                }

                /*
              // Create the path and file name to check for duplicates.
              string pathToCheck = savePath + fileName;
              
                              // Create a temporary file name to use for checking duplicates.
                              string tempfileName = "";

                              // Check to see if a file already exists with the
                              // same name as the file to upload. 
                
                              if (System.IO.File.Exists(pathToCheck))
                              {
                                  int counter = 2;
                                  while (System.IO.File.Exists(pathToCheck))
                                  {
                                      // if a file with this name already exists,
                                      // prefix the filename with a number.
                                      tempfileName = counter.ToString() + fileName;
                                      pathToCheck = savePath + tempfileName;
                                      counter++;
                                  }

                                  fileName = tempfileName;

                                  // Notify the user that the file name was changed.
                                  //UploadStatusLabel.Text = "Imágenes subidas correctamente.";

                              }
                              else
                              {
                                  // Notify the user that the file was saved successfully.
                                  //UploadStatusLabel.Text = "Imágenes cargadas correctamente.";

                              }

                              // Append the name of the file to upload to the path.*/
                savePath += fileName;

                // Call the SaveAs method to save the uploaded
                // file to the specified directory.
                switch (num_img)
                {
                    case 1:
                        FileUpload1.SaveAs(savePath);
                        EvidenciaCorte.Text = "http://facts4-srv/thermogestion/DOCUMENTAL/DOCUMENTOS/" + fileName;
                        if(D3FECHASUBIDA.Text == "")
                            {
                            D3FECHASUBIDA.Text = DateTime.Now.ToString();
                            }
                        break;
                    case 2:
                        FileUpload2.SaveAs(savePath);
                        tbD3Text.Text = "http://facts4-srv/thermogestion/DOCUMENTAL/DOCUMENTOS/" + fileName;
                        if (D3FECHASUBIDA.Text == "")
                            {
                                D3FECHASUBIDA.Text = DateTime.Now.ToString();
                            }
                        break;
                    case 3:
                        FileUpload3.SaveAs(savePath);
                        tbD6Text.Text = "http://facts4-srv/thermogestion/DOCUMENTAL/DOCUMENTOS/" + fileName;
                        if ((D6FECHASUBIDA.Text == "" || D6FECHASUBIDA.Text == "N/A") && D6CheckboxEvidencias.Checked == true)
                            {
                                D6FECHASUBIDA.Text = DateTime.Now.ToString();
                            }
                        break;
                    case 4:
                        FileUpload4.SaveAs(savePath);
                        tbD8Text.Text = "http://facts4-srv/thermogestion/DOCUMENTAL/DOCUMENTOS/" + fileName;
                        if ((D8FECHASUBIDA.Text == "" || D8FECHASUBIDA.Text == "N/A") && D8CheckboxEvidencias.Checked == true)
                            {
                                D8FECHASUBIDA.Text = DateTime.Now.ToString();
                            }
                        break;
                    case 5:
                        FileUpload5.SaveAs(savePath);
                        tbEvidenciaCargosText.Text = "http://facts4-srv/thermogestion/DOCUMENTAL/DOCUMENTOS/" + fileName;
                        break;
                    default: break;
                }
                guarda_evidencias(null, null);
                
            }
            catch (Exception)
            {
            }
        }

        public void guarda_evidencias(Object sender, EventArgs e)
        {
            double CosteSeleccionEXT; 
            double CostePiezasNOKEXT;
            double CosteCargosEXT;
            double CosteAdmonEXT;
            double CosteSeleccionINT;
            double CosteOtrosINT;

            if (Double.TryParse(tbEXTSeleccion.Text.Replace('.', ','), out CosteSeleccionEXT))
                CosteSeleccionEXT = Convert.ToDouble(tbEXTSeleccion.Text.Replace('.', ','));
            else
                CosteSeleccionEXT = 0.0;

            if (Double.TryParse(tbEXTScrap.Text.Replace('.', ','), out CostePiezasNOKEXT))
                CostePiezasNOKEXT = Convert.ToDouble(tbEXTScrap.Text.Replace('.', ','));
            else
                CostePiezasNOKEXT = 0.0;

            if (Double.TryParse(tbEXTCargos.Text.Replace('.', ','), out CosteCargosEXT))
                CosteCargosEXT = Convert.ToDouble(tbEXTCargos.Text.Replace('.', ','));
            else
                CosteCargosEXT = 0.0;

            if (Double.TryParse(tbEXTAdmon.Text.Replace('.', ','), out CosteAdmonEXT))
                CosteAdmonEXT = Convert.ToDouble(tbEXTAdmon.Text.Replace('.', ','));
            else
                CosteAdmonEXT = 0.0;

            if (Double.TryParse(tbINTSeleccion.Text.Replace('.', ','), out CosteSeleccionINT))
                CosteSeleccionINT = Convert.ToDouble(tbINTSeleccion.Text.Replace('.', ','));
            else
                CosteSeleccionINT = 0.0;

            if (Double.TryParse(tbINTOtros.Text.Replace('.', ','), out CosteOtrosINT))
                CosteOtrosINT = Convert.ToDouble(tbINTOtros.Text.Replace('.', ','));
            else
                CosteOtrosINT = 0.0;

            int CheckCorte = 1;
            int CheckD3 = 1;
            int CheckD6 = 1;
            int CheckD8 = 1;

            string D6Cierre = "";
            if (D6CheckboxEvidencias.Checked == false)
                { D6Cierre = "";
                  CheckD6 = 0; 
                }//VALOR ANTIGUO N/A
            else if (D6CheckboxEvidencias.Checked == true && D6FECHASUBIDA.Text == "N/A")
                { D6Cierre = ""; }
            else
                { D6Cierre = D6FECHASUBIDA.Text;
                }

            string D8Cierre = "";
            if (D8CheckboxEvidencias.Checked == false)
                { D8Cierre = "";
                  CheckD8 = 0;
                } //VALOR ANTIGUO N/A
            else if (D8CheckboxEvidencias.Checked == true && D8FECHASUBIDA.Text == "N/A")
                { D8Cierre = ""; }
            else
                { D8Cierre = D8FECHASUBIDA.Text; }

            Conexion_CALIDAD conexion = new Conexion_CALIDAD();
            conexion.Actualiza_Evidencias_NoConformidades(Convert.ToInt32(IDNoConformidadSM.Text), EvidenciaCorte.Text, tbD3Text.Text, tbD6Text.Text, tbD8Text.Text, "", tbEvidenciaCargosText.Text, "", CosteSeleccionEXT, CostePiezasNOKEXT, CosteCargosEXT, CosteAdmonEXT, CosteSeleccionINT, CosteOtrosINT, D3FECHASUBIDA.Text, D6Cierre, D8Cierre, CheckCorte, CheckD3, CheckD6,CheckD8);
            CargarTodas(null,null);
            carga_detalle(IDNoConformidadSM.Text);
            //lbAbrir_Modal(null,null);
        }

        public void carga_detalle(string NCNUM)
        {
            Conexion_CALIDAD conexion = new Conexion_CALIDAD();
            DataSet Modal = conexion.Devuelve_Detalle_NoConformidad(NCNUM);
            IDNoConformidad.Text = "NC-" + Modal.Tables[0].Rows[0]["IdNoConformidad"].ToString();
            IDNoConformidadSM.Text = Modal.Tables[0].Rows[0]["IdNoConformidad"].ToString();
            tbEXTSeleccion.Text = Modal.Tables[0].Rows[0]["CosteSeleccionEXT"].ToString();
            tbEXTScrap.Text = Modal.Tables[0].Rows[0]["CostePiezasNOKEXT"].ToString();
            tbEXTCargos.Text = Modal.Tables[0].Rows[0]["CosteCargosEXT"].ToString();
            tbEXTAdmon.Text = Modal.Tables[0].Rows[0]["CosteAdmonEXT"].ToString();
            tbINTSeleccion.Text = Modal.Tables[0].Rows[0]["CosteSeleccionINT"].ToString();
            tbINTOtros.Text = Modal.Tables[0].Rows[0]["CosteOtrosINT"].ToString();
            EvidenciaCorte.Text = Modal.Tables[0].Rows[0]["DOCPuntoCorte"].ToString();
            tbD3Text.Text = Modal.Tables[0].Rows[0]["DOCD3"].ToString();
            tbD6Text.Text = Modal.Tables[0].Rows[0]["DOCD6"].ToString();
            tbD8Text.Text = Modal.Tables[0].Rows[0]["DOCD8"].ToString();
            D3FECHASUBIDA.Text = Modal.Tables[0].Rows[0]["D3CIERRE"].ToString();
            D6FECHASUBIDA.Text = Modal.Tables[0].Rows[0]["D6CIERRE"].ToString();
            D8FECHASUBIDA.Text = Modal.Tables[0].Rows[0]["D8CIERRE"].ToString();
            D6CheckboxEvidencias.Checked = Convert.ToBoolean(Modal.Tables[0].Rows[0]["CheckD6"]);
            D8CheckboxEvidencias.Checked = Convert.ToBoolean(Modal.Tables[0].Rows[0]["CheckD8"]);
            /*
                if (D6FECHASUBIDA.Text != "N/A")
                { D6CheckboxEvidencias.Checked = true; }
                else
                { D6CheckboxEvidencias.Checked = false; }
                if (D8FECHASUBIDA.Text != "N/A")
                { D8CheckboxEvidencias.Checked = true; }
                else
                { D8CheckboxEvidencias.Checked = false; }
            */

            lbAbrir_Modal(null, EventArgs.Empty);
        }

        public void redireccionadocumento(Object sender, EventArgs e)
        {
            try
            {
                string URL = "";
                HtmlButton button = (HtmlButton)sender;

                string selector = button.ClientID.ToString();
                
                switch (selector)
                {
                    case "BTNVERInfoCorte":
                        URL = EvidenciaCorte.Text;
                        Response.Redirect(URL);
                        break;
                    case "BTNVERD3":
                        URL = tbD3Text.Text;
                        Response.Redirect(URL);
                        break;
                    case "BTNVERD6":
                        URL = tbD6Text.Text;
                        Response.Redirect(URL);
                        break;
                    case "BTNVERD8":
                        URL = tbD8Text.Text;
                        Response.Redirect(URL);
                        break;
                    case "BTNVERCargos":
                        URL = tbEvidenciaCargosText.Text;
                        Response.Redirect(URL);
                        break;
                }

              

            }
            catch (Exception)
            { }
        }

    }

}