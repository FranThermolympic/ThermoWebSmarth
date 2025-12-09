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
    public partial class GP12RegistroComunicaciones : System.Web.UI.Page
    {

        private static DataTable ds_Referencias = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargaListasFiltro();
                if (Request.QueryString["OPERARIO"] != null)
                {
                    string[] RecorteOperario = Request.QueryString["OPERARIO"].Split(new char[] { '_' });
                        lista_operario.Value = RecorteOperario[0];
                        Selecaño.SelectedValue = RecorteOperario[1];               
                }
                Rellenar_grid(null, null);
            }
        }

        public void Rellenar_grid(object sender, EventArgs e)
        {
            try
            {              
                Conexion_SMARTH SHconexion = new Conexion_SMARTH();

                //EVALUO FILTROS
                string referencia = "";
                if (selectReferencia.Value != "")
                {
                    string[] RecorteReferencia = selectReferencia.Value.Split(new char[] { '¬' });
                    referencia = " AND Referencia like '" + RecorteReferencia[0].Trim() + "%'";
                }

                string molde = "";
                if (selectMolde.Value != "")
                {
                    string[] RecorteMolde = selectMolde.Value.Split(new char[] { '¬' });
                    molde = " AND Molde like '" + RecorteMolde[0].Trim() + "%'";
                }

                string lote = "";
                if (selectLote.Value != "")
                {
                    lote = " AND Nlote like '" + selectLote.Value.Trim() + "%'";
                }

                string cliente = "";
                if (Convert.ToString(lista_clientes.SelectedValue).Trim() != "-")
                {
                    cliente = "";
                }

                string responsable = "";
                if (Convert.ToString(lista_responsable.SelectedValue).Trim() != "-")
                {
                    responsable = " AND J.Nombre like '" + Convert.ToString(lista_responsable.SelectedValue).Trim() + "%'";
                }

                string operario = "";
                string operarios = "";
                if (lista_operario.Value != "")
                {
                    string[] RecorteOperario = lista_operario.Value.Split(new char[] { '¬' });
                    operario = " AND NOp like '" + RecorteOperario[0].Trim() + "%'";
                    operarios = " AND (NOp1 like '" + RecorteOperario[0].Trim() + "%' OR NOp2 like '" + RecorteOperario[0].Trim() + "%' OR NOp3 like '" + RecorteOperario[0].Trim() + "%' OR NOp4 like '" + RecorteOperario[0].Trim() + "%')";
                }

                string firmado = "";
                string firmados = "";
                if (SwitchActivas.Checked)
                { 
                firmado = " AND INFO = 'Pdte. informar'";
                firmados = " AND (INFO1 = 'Pdte. informar' OR INFO2 = 'Pdte. informar' OR INFO3 = 'Pdte. informar' OR INFO4 = 'Pdte. informar')";
                }
                //CARGO DATASET
                ds_Referencias = SHconexion.Devuelve_Registro_COMUNICACIONES_ORDEN(Convert.ToInt32(Selecaño.SelectedValue), " AND YEAR([FechaInicio]) = " + Convert.ToString(Selecaño.SelectedValue) + "", referencia, molde, lote, cliente, responsable, operarios, firmados);
                DataTable Ordenes = SHconexion.Devuelve_Registro_COMUNICACIONES_OPERARIO(Convert.ToInt32(Selecaño.SelectedValue), " AND YEAR([FechaInicio]) = " + Convert.ToString(Selecaño.SelectedValue) + "", referencia, molde, lote, cliente, responsable, operario, firmado);
                dgv_RegistroOrdenes.DataSource = ds_Referencias;
                dgv_RegistroOrdenes.DataBind();

                dgv_RegistroOperarios.DataSource = Ordenes;
                dgv_RegistroOperarios.DataBind();
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
                Conexion_SMARTH SHconexion = new Conexion_SMARTH();
                Conexion_GP12 conexion = new Conexion_GP12();
                DataTable clientes = SHconexion.Devuelve_listado_clientes();
                lista_clientes.Items.Add("-");
                foreach (DataRow row in clientes.Rows) { lista_clientes.Items.Add(row["Cliente"].ToString()); }
                lista_clientes.ClearSelection();
                lista_clientes.SelectedValue = "";


                DataSet Personal = SHconexion.Devuelve_mandos_intermedios_SMARTH();

                DataTable ListaProductos = SHconexion.Devuelve_listado_PRODUCTOS();
                {
                    for (int i = 0; i <= ListaProductos.Rows.Count - 1; i++)
                    {
                        DatalistReferencias.InnerHtml = DatalistReferencias.InnerHtml + System.Environment.NewLine +
                            String.Format("<option value='{0}'>", ListaProductos.Rows[i][0]);
                    }
                }

                DataTable ListaMoldes = SHconexion.Devuelve_listado_MOLDES();
                {
                    for (int i = 0; i <= ListaMoldes.Rows.Count - 1; i++)
                    {
                        DatalistMoldes.InnerHtml = DatalistMoldes.InnerHtml + System.Environment.NewLine +
                            String.Format("<option value='{0}'>", ListaMoldes.Rows[i][0]);
                    }
                }

                DataTable ListaOperarios = SHconexion.Devuelve_listado_OPERARIOS_ACTIVOS();
                {
                    for (int i = 0; i <= ListaOperarios.Rows.Count - 1; i++)
                    {
                        DatalistOperario.InnerHtml = DatalistOperario.InnerHtml + System.Environment.NewLine +
                            String.Format("<option value='{0}'>", ListaOperarios.Rows[i][0]);
                    }
                }

                DataSet Responsable = Personal; //AbiertoPorV2
                Responsable.Tables[0].DefaultView.RowFilter = "Departamento = 'INGENIERIA'  OR Departamento = '-'";
                DataTable DTIngenieria = Responsable.Tables[0].DefaultView.ToTable();
                lista_responsable.Items.Clear();
                foreach (DataRow row in DTIngenieria.Rows)
                {
                    lista_responsable.Items.Add(row["Nombre"].ToString());
                }

                DataSet DSInformador = Personal; //AbiertoPorV2
                DSInformador.Tables[0].DefaultView.RowFilter = "Departamento = 'CALIDAD' OR Departamento = 'PRODUCCION' OR Departamento = '-'";
                DataTable DTListainformador = Responsable.Tables[0].DefaultView.ToTable();
                Informador.Items.Clear();
                foreach (DataRow row in DTListainformador.Rows)
                {
                    Informador.Items.Add(row["Nombre"].ToString());
                }

                


            }
            catch (Exception)
            {

            }
        }

        protected void Cargar_todas(object sender, EventArgs e)
        {
            selectReferencia.Value = "";
            selectMolde.Value = "";
            selectLote.Value = "";
            lista_clientes.SelectedValue = "-";
            lista_responsable.SelectedValue = "-";
            Rellenar_grid(null, null);
        }

        public void GridViewCommandEventHandler(Object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "CargaDetalle")
                {                   
                    Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                    DataTable Modal = SHConexion.Modal_Detalles_Revision(e.CommandArgument.ToString(), Convert.ToString(Selecaño.SelectedValue));
                    IDINSPECCION.Text = e.CommandArgument.ToString();
                    TbRevisadas.InnerText = Modal.Rows[0]["PiezasRevisadas"].ToString();
                    TbBuenas.InnerText = Modal.Rows[0]["PiezasOK"].ToString();
                    TbRetrabajadas.InnerText = Modal.Rows[0]["Retrabajadas"].ToString();
                    TbMalas.InnerText = Modal.Rows[0]["PiezasNOK"].ToString();
                    detalleReferencia.InnerText = Modal.Rows[0]["Referencia"].ToString() + " - " + Modal.Rows[0]["Nombre"].ToString();
                    //detalleReferenciaNombre.Text = ;
                    TbOperarioRevision.InnerText = Modal.Rows[0]["OperarioRevision"].ToString() + " (" + Modal.Rows[0]["Proveedor"].ToString() + ")";
                

                    
                    
                    
                    
                    //ACTIVO CELDAS DE OPERARIO Y LAS COMPARO
                    //OPERARIO 1
                    Operario1.InnerText = Modal.Rows[0]["OPERARIO1"].ToString();
                        NOperario1.Value = Modal.Rows[0]["NOp1"].ToString();
                        NFirmaOperario1.Value = Modal.Rows[0]["FirmaNOp1"].ToString();
                        NInformadoPor1.Value = Modal.Rows[0]["Informador1"].ToString();
                        lblFeedbackOp1.InnerText = Modal.Rows[0]["FeedbackOPERARIOS"].ToString();
                    if (Operario1.InnerText == "")
                        {
                            BOXOP1.Visible = false;
                        }
                        else
                        {
                            BOXOP1.Visible = true;
                            if (Modal.Rows[0]["FechaInfo1"].ToString() == "")
                            {
                                FechaINFO1.InnerText = "(Pdte. informar)";
                            }
                            else
                            {
                                FechaINFO1.InnerText = "(Comunica " + Modal.Rows[0]["Informador1"].ToString() + " en " + Convert.ToDateTime(Modal.Rows[0]["FechaInfo1"]).ToString("dd/MM/yyy") + ")";
                            }
                        }

                        //OPERARIO 2
                        Operario2.InnerText = Modal.Rows[0]["OPERARIO2"].ToString();
                    NOperario2.Value = Modal.Rows[0]["NOp2"].ToString();
                    NFirmaOperario2.Value = Modal.Rows[0]["FirmaNOp2"].ToString();
                    NInformadoPor2.Value = Modal.Rows[0]["Informador2"].ToString();
                    lblFeedbackOp2.InnerText = Modal.Rows[0]["FeedbackOPERARIOS2"].ToString();
                    if (Operario2.InnerText == "")
                        {
                            BOXOP2.Visible = false;
                        }
                        else 
                        {
                            BOXOP2.Visible = true;                      
                            if (Modal.Rows[0]["FechaInfo2"].ToString() == "")
                            {
                                FechaINFO2.InnerText = "(Pdte. informar)";
                            }
                            else
                            {
                                FechaINFO2.InnerText = "(Comunica " + Modal.Rows[0]["Informador2"].ToString() + " en " + Convert.ToDateTime(Modal.Rows[0]["FechaInfo2"]).ToString("dd/MM/yyy") + ")";
                            }
                        }

                        //OPERARIO 3
                        Operario3.InnerText = Modal.Rows[0]["OPERARIO3"].ToString();
                    NOperario3.Value = Modal.Rows[0]["NOp3"].ToString();
                    NFirmaOperario3.Value = Modal.Rows[0]["FirmaNOp3"].ToString();
                    NInformadoPor3.Value = Modal.Rows[0]["Informador3"].ToString();
                    lblFeedbackOp3.InnerText = Modal.Rows[0]["FeedbackOPERARIOS3"].ToString();
                    if (Operario3.InnerText == "")
                        {
                            BOXOP3.Visible = false;
                        }
                        else
                        {
                            BOXOP3.Visible = true;                        
                            if (Modal.Rows[0]["FechaInfo3"].ToString() == "")
                            {
                                FechaINFO3.InnerText = "(Pdte. informar)";
                            }
                            else
                            {
                                FechaINFO3.InnerText = "(Comunica " + Modal.Rows[0]["Informador3"].ToString() + " en " + Convert.ToDateTime(Modal.Rows[0]["FechaInfo3"]).ToString("dd/MM/yyy") + ")";
                            }
                        }
                    

                        //OPERARIO 4
                        Operario4.InnerText = Modal.Rows[0]["OPERARIO4"].ToString();
                    NOperario4.Value = Modal.Rows[0]["NOp4"].ToString();
                    NFirmaOperario4.Value = Modal.Rows[0]["FirmaNOp4"].ToString();
                    NInformadoPor4.Value = Modal.Rows[0]["Informador4"].ToString();
                    lblFeedbackOp4.InnerText = Modal.Rows[0]["FeedbackOPERARIOS4"].ToString();
                    if (Operario4.InnerText == "")
                        {
                            BOXOP4.Visible = false;
                        }
                        else
                        {
                            BOXOP4.Visible = true;
                            if (Modal.Rows[0]["FechaInfo4"].ToString() == "")
                            {
                                FechaINFO4.InnerText = "(Pdte. informar)";
                            }
                            else
                            {
                                FechaINFO4.InnerText = "(Comunica " + Modal.Rows[0]["Informador4"].ToString() + " en " + Convert.ToDateTime(Modal.Rows[0]["FechaInfo4"]).ToString("dd/MM/yyy") + ")";
                            }
                        }
                    
                    CosteHoras.InnerText = "en " + Modal.Rows[0]["HORAS"].ToString() + " horas.";
                    CosteChatarra.InnerText = Modal.Rows[0]["CosteScrapRevision"].ToString();
                    CosteOperario.InnerText = Modal.Rows[0]["CostePiezaRevision"].ToString();
                    CosteTotal.InnerText = Modal.Rows[0]["CosteRevision"].ToString();
                    TDDefecto1.InnerText = Modal.Rows[0]["Def1"].ToString();
                    TDNotas.Value = Modal.Rows[0]["Notas"].ToString();
                    CeldasPorDefecto();
                    if (TDDefecto1.InnerText != "0")
                    {
                        THDefecto1.Visible = true;
                    }
                    TDDefecto2.InnerText = Modal.Rows[0]["Def2"].ToString();
                    if (TDDefecto2.InnerText != "0")
                    {
                        THDefecto2.Visible = true;       
                    }
                    TDDefecto3.InnerText = Modal.Rows[0]["Def3"].ToString();
                    if (TDDefecto3.InnerText != "0")
                    {
                        THDefecto3.Visible = true;
                    }
                    TDDefecto4.InnerText = Modal.Rows[0]["Def4"].ToString();
                    if (TDDefecto4.InnerText != "0")
                    {
                        THDefecto4.Visible = true;
                    }
                    TDDefecto5.InnerText = Modal.Rows[0]["Def5"].ToString();
                    if (TDDefecto5.InnerText != "0")
                    {
                        THDefecto5.Visible = true;
                    }
                    TDDefecto6.InnerText = Modal.Rows[0]["Def6"].ToString();
                    if (TDDefecto6.InnerText != "0")
                    {
                        THDefecto6.Visible = true;
                    }
                    TDDefecto7.InnerText = Modal.Rows[0]["Def7"].ToString();
                    if (TDDefecto7.InnerText != "0")
                    {
                        THDefecto7.Visible = true;
                    }
                    TDDefecto8.InnerText = Modal.Rows[0]["Def8"].ToString();
                    if (TDDefecto8.InnerText != "0")
                    {
                        THDefecto8.Visible = true;
                    }
                    TDDefecto9.InnerText = Modal.Rows[0]["Def9"].ToString();
                    if (TDDefecto9.InnerText != "0")
                    {
                        THDefecto9.Visible = true;
                    }
                    TDDefecto10.InnerText = Modal.Rows[0]["Def10"].ToString();
                    if (TDDefecto10.InnerText != "0")
                    {
                        THDefecto10.Visible = true;
                    }
                    TDDefecto11.InnerText = Modal.Rows[0]["Def11"].ToString();
                    if (TDDefecto11.InnerText != "0")
                    {
                        THDefecto11.Visible = true;
                    }
                    TDDefecto12.InnerText = Modal.Rows[0]["Def12"].ToString();
                    if (TDDefecto12.InnerText != "0")
                    {
                        THDefecto12.Visible = true;
                    }
                    TDDefecto13.InnerText = Modal.Rows[0]["Def13"].ToString();
                    if (TDDefecto13.InnerText != "0")
                    {
                        THDefecto13.Visible = true;
                    }
                    TDDefecto14.InnerText = Modal.Rows[0]["Def14"].ToString();
                    if (TDDefecto14.InnerText != "0")
                    {
                        THDefecto14.Visible = true;
                    }
                    TDDefecto15.InnerText = Modal.Rows[0]["Def15"].ToString();
                    if (TDDefecto15.InnerText != "0")
                    {
                        THDefecto15.Visible = true;
                    }
                    TDDefecto16.InnerText = Modal.Rows[0]["Def16"].ToString();
                    if (TDDefecto16.InnerText != "0")
                    {
                        THDefecto16.Visible = true;
                    }
                    TDDefecto17.InnerText = Modal.Rows[0]["Def17"].ToString();
                    if (TDDefecto17.InnerText != "0")
                    {
                        THDefecto17.Visible = true;
                    }
                    TDDefecto18.InnerText = Modal.Rows[0]["Def18"].ToString();
                    if (TDDefecto18.InnerText != "0")
                    {
                        THDefecto18.Visible = true;
                    }
                    TDDefecto19.InnerText = Modal.Rows[0]["Def19"].ToString();
                    if (TDDefecto19.InnerText != "0")
                    {
                        THDefecto19.Visible = true;
                    }
                    TDDefecto20.InnerText = Modal.Rows[0]["Def20"].ToString();
                    if (TDDefecto20.InnerText != "0")
                    {
                        THDefecto20.Visible = true;
                    }
                    TDDefecto21.InnerText = Modal.Rows[0]["Def21"].ToString();
                    if (TDDefecto21.InnerText != "0")
                    {
                        THDefecto21.Visible = true;
                    }
                    TDDefecto22.InnerText = Modal.Rows[0]["Def22"].ToString();
                    if (TDDefecto22.InnerText != "0")
                    {
                        THDefecto22.Visible = true;
                    }
                    TDDefecto23.InnerText = Modal.Rows[0]["Def23"].ToString();
                    if (TDDefecto23.InnerText != "0")
                    {
                        THDefecto23.Visible = true;
                    }
                    TDDefecto24.InnerText = Modal.Rows[0]["Def24"].ToString();
                    if (TDDefecto24.InnerText != "0")
                    {
                        THDefecto24.Visible = true;
                    }
                    TDDefecto25.InnerText = Modal.Rows[0]["Def25"].ToString();
                    if (TDDefecto25.InnerText != "0")
                    {
                        THDefecto25.Visible = true;
                    }
                    TDDefecto26.InnerText = Modal.Rows[0]["Def26"].ToString();
                    if (TDDefecto26.InnerText != "0")
                    {
                        THDefecto26.Visible = true;
                    }
                    TDDefecto27.InnerText = Modal.Rows[0]["Def27"].ToString();
                    if (TDDefecto27.InnerText != "0")
                    {
                        THDefecto27.Visible = true;
                    }
                    TDDefecto28.InnerText = Modal.Rows[0]["Def28"].ToString();
                    if (TDDefecto28.InnerText != "0")
                    {
                        THDefecto28.Visible = true;
                    }
                    string imagenpordefecto = "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg";

                    string imagen1 = Modal.Rows[0]["ImagenDefecto1"].ToString();
                    if (imagen1 != "")
                    {
                        linkimagen1.HRef = imagen1;
                        IMGlinkimagen1.Src = imagen1;
                    }
                    else
                    {
                        linkimagen1.HRef = imagenpordefecto;
                        IMGlinkimagen1.Src = imagenpordefecto;
                    }

                    string imagen2 = Modal.Rows[0]["ImagenDefecto2"].ToString();
                    if (imagen2 != "")
                    {
                        linkimagen2.HRef = imagen2;
                        IMGlinkimagen2.Src = imagen2;
                    }
                    else
                    {
                        linkimagen2.HRef = imagenpordefecto;
                        IMGlinkimagen2.Src = imagenpordefecto;
                    }

                    string imagen3 = Modal.Rows[0]["ImagenDefecto3"].ToString();
                    if (imagen3 != "")
                    {
                        linkimagen3.HRef = imagen3;
                        IMGlinkimagen3.Src = imagen3;
                    }
                    else
                    {
                        linkimagen3.HRef = imagenpordefecto;
                        IMGlinkimagen3.Src = imagenpordefecto;
                    }


                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                }
            }
            catch (Exception ex)
            { 
            }

        }

        protected void CeldasPorDefecto()
        {
            THDefecto1.Visible = false;
            THDefecto2.Visible = false;
            THDefecto3.Visible = false;
            THDefecto4.Visible = false;
            THDefecto5.Visible = false;
            THDefecto6.Visible = false;
            THDefecto7.Visible = false;
            THDefecto8.Visible = false;
            THDefecto9.Visible = false;
            THDefecto10.Visible = false;
            THDefecto11.Visible = false;
            THDefecto12.Visible = false;
            THDefecto13.Visible = false;
            THDefecto14.Visible = false;
            THDefecto15.Visible = false;
            THDefecto16.Visible = false;
            THDefecto17.Visible = false;
            THDefecto18.Visible = false;
            THDefecto19.Visible = false;
            THDefecto20.Visible = false;
            THDefecto21.Visible = false;
            THDefecto22.Visible = false;
            THDefecto23.Visible = false;
            THDefecto24.Visible = false;
            THDefecto25.Visible = false;
            THDefecto26.Visible = false;
            THDefecto27.Visible = false;
            THDefecto28.Visible = false;

        }

        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            for (int i = 0; i <= dgv_RegistroOperarios.Rows.Count - 1; i++)
            {
                Label lblscrap = (Label)dgv_RegistroOperarios.Rows[i].FindControl("lblMalas");

                if (lblscrap.Text != "0")
                {
                    lblscrap.ForeColor = System.Drawing.Color.Red;
                    lblscrap.Font.Bold = true;
                }
                else
                {
                    lblscrap.Text = "";
                }

                Label lblretrabajo = (Label)dgv_RegistroOperarios.Rows[i].FindControl("lblRetrabajadas");

                if (lblretrabajo.Text != "0")
                {
                    lblretrabajo.ForeColor = System.Drawing.Color.DarkOrange;
                    lblretrabajo.Font.Bold = true;
                }
                else
                {
                    lblretrabajo.Text = "";
                }
            }
        }

        protected void GridView2_DataBound(object sender, EventArgs e)
        {
            for (int i = 0; i <= dgv_RegistroOrdenes.Rows.Count - 1; i++)
            {
                Label lblscrap = (Label)dgv_RegistroOrdenes.Rows[i].FindControl("lblMalas");

                if (lblscrap.Text != "0")
                {
                    lblscrap.ForeColor = System.Drawing.Color.Red;
                    lblscrap.Font.Bold = true;
                }
                else
                {
                    lblscrap.Text = "";
                }

                Label lblretrabajo = (Label)dgv_RegistroOrdenes.Rows[i].FindControl("lblRetrabajadas");

                if (lblretrabajo.Text != "0")
                {
                    lblretrabajo.ForeColor = System.Drawing.Color.DarkOrange;
                    lblretrabajo.Font.Bold = true;
                }
                else
                {
                    lblretrabajo.Text = "";
                }
            }
        }

        public void CargaHistorico(Object sender, EventArgs e)
        {
            string[] RecorteRef = detalleReferencia.InnerText.Split(new char[] { '-' });

            Response.Redirect("GP12HistoricoReferencia.aspx?REFERENCIA=" + RecorteRef[0].Trim());
        }

        public void CargaHistoricoOperario(Object sender, EventArgs e)
        {
            string Numoperario = "";
            HtmlButton button = (HtmlButton)sender;
            string indice = button.ClientID.ToString();

            switch (indice)
            {
                case "BtnDetalleOP1":
                    Numoperario = NOperario1.Value;
                    break;
                case "BtnDetalleOP2":
                    Numoperario = NOperario2.Value;
                    break;
                case "BtnDetalleOP3":
                    Numoperario = NOperario3.Value;
                    break;
                case "BtnDetalleOP4":
                    Numoperario = NOperario4.Value;
                    break;
            }

            Response.Redirect("GP12HistoricoOperario.aspx?OPERARIO=" + Numoperario.Trim());
        }

        public void ActualizaInformador(object sender, EventArgs e)
        {
            try
            {
                //HtmlButton button = (HtmlButton)sender;
                //string indice = button.ClientID.ToString();               
                Conexion_SMARTH SHconexion = new Conexion_SMARTH();
                string query = "";
                string fechainfo = DateTime.Now.ToString("dd/MM/yyy HH:mm");
                switch (NUMOP.Text)
                {
                    case "1":
                        FechaINFO1.InnerText = DateTime.Now.ToString("dd/MM/yyy HH:mm");
                        query = " SET FechaInfo1 = '" + fechainfo + "', Informador1 = '" + Informador.SelectedValue.ToString() + "', FeedbackOPERARIOS = '" + FeedbackOPERARIOS.Value + "', FirmaNOp1 = '"+ signatureJSON.Value + "'";                        
                        break;
                    case "2":
                        FechaINFO2.InnerText = DateTime.Now.ToString("dd/MM/yyy HH:mm");
                        query = " SET FechaInfo2 = '" + fechainfo + "', Informador2 = '" + Informador.SelectedValue.ToString() + "', FeedbackOPERARIOS2 = '" + FeedbackOPERARIOS.Value + "', FirmaNOp2 = '" + signatureJSON.Value + "'";
                        break;
                    case "3":
                        FechaINFO3.InnerText = DateTime.Now.ToString("dd/MM/yyy HH:mm");
                        query = " SET FechaInfo3 = '" + fechainfo + "', Informador3 = '" + Informador.SelectedValue.ToString() + "', FeedbackOPERARIOS3 = '" + FeedbackOPERARIOS.Value + "', FirmaNOp3 = '" + signatureJSON.Value + "'";
                        break;
                    case "4":
                        FechaINFO4.InnerText = DateTime.Now.ToString("dd/MM/yyy HH:mm");
                        query = " SET FechaInfo4 = '" + fechainfo + "', Informador4 = '" + Informador.SelectedValue.ToString() + "', FeedbackOPERARIOS4 = '" + FeedbackOPERARIOS.Value + "', FirmaNOp4 = '" + signatureJSON.Value + "'";
                        break;                        
                }
                SHconexion.Actualizar_GP12_OPERARIOREVISION(Convert.ToInt32(IDINSPECCION.Text), query);
                Cargar_todas(null, null);

            }
            catch (Exception)
            {
            }

        }

        public void CargarInformador(object sender, EventArgs e)
        {
            try
            {
                
                HtmlButton button = (HtmlButton)sender;
                string indice = button.ClientID.ToString();
                IDREVISION.Text = IDINSPECCION.Text;
                switch (indice)
                {
                    case "InformarOp1":
                        NUMOP.Text = "1";
                        LblInformadoOP.InnerText = Operario1.InnerText;
                        if (NFirmaOperario1.Value == "")
                        {
                            FirmadoOp.Visible = false;
                            SinFirmarOp.Visible = true;
                        }
                        else 
                        {
                            FirmadoOp.Visible = true;
                            SinFirmarOp.Visible = false;
                            IMGFirma.ImageUrl = NFirmaOperario1.Value;
                            lblInformadoPor.Text = FechaINFO1.InnerText;
                            FeedbackOPERARIOS.Value = lblFeedbackOp1.InnerText;
                        }

                        break;

                    case "InformarOp2":
                        NUMOP.Text = "2";
                        LblInformadoOP.InnerText = Operario2.InnerText;
                        if (NFirmaOperario2.Value == "")
                        {
                            FirmadoOp.Visible = false;
                            SinFirmarOp.Visible = true;
                        }
                        else
                        {
                            FirmadoOp.Visible = true;
                            SinFirmarOp.Visible = false;
                            IMGFirma.ImageUrl = NFirmaOperario2.Value;
                            lblInformadoPor.Text = FechaINFO2.InnerText;
                            FeedbackOPERARIOS.Value = lblFeedbackOp2.InnerText;
                        }
                        break;

                    case "InformarOp3":
                        NUMOP.Text = "3";
                        LblInformadoOP.InnerText = Operario3.InnerText;
                        if (NFirmaOperario3.Value == "")
                        {
                            FirmadoOp.Visible = false;
                            SinFirmarOp.Visible = true;
                        }
                        else
                        {
                            FirmadoOp.Visible = true;
                            SinFirmarOp.Visible = false;
                            IMGFirma.ImageUrl = NFirmaOperario3.Value;
                            lblInformadoPor.Text = FechaINFO3.InnerText;
                            FeedbackOPERARIOS.Value = lblFeedbackOp3.InnerText;
                        }
                        break;
                    case "InformarOp4":
                        NUMOP.Text = "4";
                        LblInformadoOP.InnerText = Operario4.InnerText;
                        if (NFirmaOperario4.Value == "")
                        {
                            FirmadoOp.Visible = false;
                            SinFirmarOp.Visible = true;
                        }
                        else
                        {
                            FirmadoOp.Visible = true;
                            SinFirmarOp.Visible = false;
                            IMGFirma.ImageUrl = NFirmaOperario4.Value;
                            lblInformadoPor.Text = FechaINFO4.InnerText;
                            FeedbackOPERARIOS.Value = lblFeedbackOp4.InnerText;
                        }
                        break;
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupFirma();", true);

            }
            catch (Exception)
            {
            }

        }

    }

}