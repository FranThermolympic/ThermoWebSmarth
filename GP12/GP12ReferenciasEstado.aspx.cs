using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Windows;
using System.Data.SqlClient;
using System.Xml;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Web.Script.Services;
using Excel = Microsoft.Office.Interop.Excel;

using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using System.Net.Mail;
using System.Net;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Extensions;
using OfficeOpenXml;

using System.Reflection;


namespace ThermoWeb.GP12
{
    public partial class GP12ReferenciasEstado : System.Web.UI.Page
    {

        
        private static DataTable ds_Estado_Referencias = new DataTable();
        private static int INTEnRevision = 0;
        private static int INTVencidas = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargaListasFiltro();
                Rellenar_grid(null, null);
            }
           
        }

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

                DataSet Responsable = Personal; //AbiertoPorV2
                Responsable.Tables[0].DefaultView.RowFilter = "Departamento = 'INGENIERIA'  OR Departamento = '-'";
                DataTable DTIngenieria = Responsable.Tables[0].DefaultView.ToTable();
                lista_responsable.Items.Clear();
                foreach (DataRow row in DTIngenieria.Rows)
                {
                    lista_responsable.Items.Add(row["Nombre"].ToString());
                }


                DataTable EstadoRevision = SHconexion.Devuelve_Lista_Estados_Referencias();
                lista_estado_revision.Items.Add("-");
                foreach (DataRow row in EstadoRevision.Rows)
                {
                    lista_estado_revision.Items.Add(row["Razon"].ToString());
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

        //CARGA GRIDVIEWS
        public void Rellenar_grid(object sender, EventArgs e)
        {
            try
            {
               
                Conexion_SMARTH SHconexion = new Conexion_SMARTH();

                INTEnRevision = 0;
                INTVencidas = 0;

                //EVALUO FILTROS
                string referencia = "";
                string referenciaestado = "";
                if (selectReferencia.Value != "")
                {
                    string[] RecorteReferencia = selectReferencia.Value.Split(new char[] { '¬' });
                    referencia = " AND Referencia like '" + RecorteReferencia[0].Trim() + "%'";
                    referenciaestado = " AND P.Referencia like '" + RecorteReferencia[0].Trim() + "%'";
                    //CheckEnRevision.Checked = false;
                }

                string molde = "";
                if (selectMolde.Value != "")
                {
                    string[] RecorteMolde = selectMolde.Value.Split(new char[] { '¬' });
                    molde = " AND Molde like '" + RecorteMolde[0].Trim() + "%'";
                    //CheckEnRevision.Checked = false;
                }

                string lote = "";
                if (selectLote.Value != "")
                {
                    lote = " AND Nlote like '" + selectLote.Value.Trim() + "%'";
                    //CheckEnRevision.Checked = false;
                }

                string cliente = "";
                string clienteestado = "";
                if (Convert.ToString(lista_clientes.SelectedValue).Trim() != "-")
                {
                    cliente = " AND Cliente LIKE '"+lista_clientes.SelectedValue.ToString()+"%'";
                    clienteestado = " AND P.Cliente LIKE '" + lista_clientes.SelectedValue.ToString() + "%'";
                    //CheckEnRevision.Checked = false;
                }

                string responsable = "";
                string responsableestado = "";
                if (Convert.ToString(lista_responsable.SelectedValue).Trim() != "-")
                {
                    responsable = " AND NombreRESP like '" + Convert.ToString(lista_responsable.SelectedValue).Trim() + "%'";
                    responsableestado = " AND D.Nombre like '" + Convert.ToString(lista_responsable.SelectedValue).Trim() + "%'";
                    //CheckEnRevision.Checked = false;
                }

                //ENSAMBLADO GRIDVIEWS
               

                string estadoREV = " and (E.EstadoActual > 0 and e.EstadoActual < 7)";
                if (!CheckEnRevision.Checked)
                {
                    estadoREV = "";
                }
                if (lista_estado_revision.SelectedValue != "-")
                {
                    estadoREV = estadoREV + " and E.EstadoActual = " + SHconexion.Devuelve_ID_Estados_Referencias(lista_estado_revision.SelectedValue);
                }




                string vencido = "";
                if (CheckVencidas.Checked)
                {
                    vencido = "AND(Fechaprevsalida IS NOT NULL AND Fechaprevsalida < SYSDATETIME() AND EstadoActual <> 0 AND EstadoActual <> 7)";
                }
                

                //referencia //molde //estado //vencido //cliente //responsable
                ds_Estado_Referencias = SHconexion.Devuelve_Estado_Referencias(referenciaestado, molde, estadoREV, clienteestado, responsableestado, vencido);
                dgv_EstadoReferencias.DataSource = ds_Estado_Referencias;
                dgv_EstadoReferencias.DataBind();

                lblEnPlazo.Text = (INTEnRevision - INTVencidas).ToString();
                lblEnRevision.Text = INTEnRevision.ToString();
                lblVencidas.Text = INTVencidas.ToString();

                lblVencidasPORC.Text = ((INTVencidas * 100) / INTEnRevision).ToString() + "% vencidas.";
                lblEnPlazoPORC.Text = (((INTEnRevision - INTVencidas) * 100) / INTEnRevision).ToString() + "% en plazo.";

                //CALCULO CABECERA
                /*
                if (ds_Estado_Referencias.Rows.Count > 0)
                {
                    DataTable EstadoREFS = ds_Estado_Referencias;

                    int TotalEnRevision = EstadoREFS.Rows.Count;
                    lblEnRevision.Text = TotalEnRevision.ToString();

                    EstadoREFS.DefaultView.RowFilter = "Fechaprevsalida is not null AND Fechaprevsalida < #"+DateTime.Now.ToString("dd/MM/yyyy")+"# AND EstadoActual <> '0' AND EstadoActual <> '7'";  
                    DataTable DTVencidas = EstadoREFS.DefaultView.ToTable();
                    int Totalvencidas = DTVencidas.Rows.Count;
                    lblVencidas.Text = Totalvencidas.ToString();

                    double porvencidas = (Convert.ToDouble(Totalvencidas) / Convert.ToDouble(TotalEnRevision))*100;
                    lblVencidasPORC.Text = porvencidas.ToString() + "% vencidas.";

                    int TotalEnPlazo = TotalEnRevision - Totalvencidas;
                    lblEnPlazo.Text = TotalEnPlazo.ToString();

                    double porvenplazo = (Convert.ToDouble(TotalEnPlazo) / Convert.ToDouble(TotalEnRevision)) * 100;
                    lblEnPlazoPORC.Text = porvenplazo.ToString() + "% enplazo.";

                    //double PORRETRABAJO = ((Convert.ToDouble(totalRetrabajadas) * 100) / Convert.ToDouble(totalRevisadas));
                    //lblRetrabajadasPORC.Text = PORRETRABAJO.ToString("0.0") + "% retrab.";

                    //double PORCBUENAS = ((Convert.ToDouble(totalBuenas) * 100) / Convert.ToDouble(totalRevisadas));
                    //lblBuenasPORC.Text = PORCBUENAS.ToString("0.0") + "% bueno.";
                }
                else
                {
                    lblEnRevision.Text = "0";
                    lblEnPlazo.Text = "0";
                    lblVencidas.Text = "0";
                   
                }
                */

            }
            catch (Exception ex)
            {

            }
        }

        // carga la lista utilizando un filtro
               
        public void GridViewCommandEventHandler(Object sender, GridViewCommandEventArgs e)
        {
            try
            {
               
                if (e.CommandName == "Redirect")
                {
                    Response.Redirect("GP12HistoricoReferencia.aspx?REFERENCIA=" + e.CommandArgument);
                }
            }
            catch (Exception ex)
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

        protected void Cargar_todas(object sender, EventArgs e)
        {
            selectReferencia.Value = "";
            selectMolde.Value = "";
            selectLote.Value = "";
            lista_clientes.SelectedValue = "-";
            lista_responsable.SelectedValue = "-";
            Rellenar_grid(null, null);
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

        //ESTADO DE REFERENCIAS

        // guarda una fila
        public void GridView_RowUpdating(Object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Label referencia = (Label)dgv_EstadoReferencias.Rows[e.RowIndex].FindControl("txtReferencia");
                DropDownList estadoactual = (DropDownList)dgv_EstadoReferencias.Rows[e.RowIndex].FindControl("txtEstadoActual");
                DropDownList responsable = (DropDownList)dgv_EstadoReferencias.Rows[e.RowIndex].FindControl("txtResponsable");
                //Label fechrevision = (Label)dgv_EstadoReferencias.Rows[e.RowIndex].FindControl("txtFecharev");
                TextBox fechprevsal = (TextBox)dgv_EstadoReferencias.Rows[e.RowIndex].FindControl("txtFechaprevsalida");
                Label estadoanterior = (Label)dgv_EstadoReferencias.Rows[e.RowIndex].FindControl("txtEstadoAnterior");
                Label Fechaestanterior = (Label)dgv_EstadoReferencias.Rows[e.RowIndex].FindControl("txtFechaestanterior");
                TextBox Observaciones = (TextBox)dgv_EstadoReferencias.Rows[e.RowIndex].FindControl("txtObservaciones");

                Conexion_GP12 conexion = new Conexion_GP12();
                //DESCOMENTAR AL TERMINAR
                conexion.actualizar_estado(Convert.ToInt32(referencia.Text),
                                            conexion.devuelve_IDlista_razonesrevision(Convert.ToString(estadoactual.SelectedValue)),
                                            conexion.Devuelve_IDlista_responsablesSMARTH(Convert.ToString(responsable.SelectedValue)),
                                            DateTime.Now.ToString("dd/MM/yyyy"),
                                            fechprevsal.Text,
                                            estadoanterior.Text,
                                            Fechaestanterior.Text,
                                            Observaciones.Text);
                conexion.actualizar_productosBMS(Convert.ToInt32(referencia.Text), conexion.devuelve_IDlista_razonesrevision(Convert.ToString(estadoactual.SelectedValue)));
                dgv_EstadoReferencias.EditIndex = -1;
                Rellenar_grid(null,null);
                
                //ds_Referencias = conexion.leer_referenciaestados();
                //dgv_EstadoReferencias.DataSource = ds_Referencias;
                //dgv_EstadoReferencias.DataBind();
                //cargar_Filtrados(null, e);
            }
            catch (Exception ex)
            {

            }
        }

        // cancela la modificación de una fila
        protected void gridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            dgv_EstadoReferencias.EditIndex = -1;
            dgv_EstadoReferencias.DataSource = ds_Estado_Referencias;
            dgv_EstadoReferencias.DataBind();
            
        }

        // modifica una fila
        public void GridView_RowEditing(Object sender, GridViewEditEventArgs e)
        {

            dgv_EstadoReferencias.EditIndex = e.NewEditIndex;
            dgv_EstadoReferencias.DataSource = ds_Estado_Referencias;
            dgv_EstadoReferencias.DataBind();
            
        }

        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label CueentaLinea = (Label)e.Row.FindControl("lblEstadoActual");
                Conexion_SMARTH SHconexion = new Conexion_SMARTH();
                //if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                if (!e.Row.RowState.HasFlag(DataControlRowState.Edit))
                { 
                    Label lblVencido30dias = (Label)e.Row.FindControl("lblFechaProxima"); 
                    Label lblFechaVENC = (Label)e.Row.FindControl("lblFechaVencida");
                    Label lblProxVencido = (Label)e.Row.FindControl("lblFechaPrevVencidatxt");
                    if (lblProxVencido.Text != "")
                    { 
                    if(Convert.ToDateTime(lblProxVencido.Text) < DateTime.Now && CueentaLinea.Text != "Sin Revisión" && CueentaLinea.Text != "Proy. terminado / Recambio")
                    {
                        lblFechaVENC.Visible = true;
                            INTVencidas++;
                    }

                    if (Convert.ToDateTime(lblProxVencido.Text) < DateTime.Now.AddDays(+30) && lblFechaVENC.Visible == false)
                    {
                        lblVencido30dias.Visible = true;
                    }
                    }

                    
                    if (CueentaLinea.Text != "Sin Revisión" && CueentaLinea.Text != "Proy. terminado / Recambio")
                    {
                        INTEnRevision ++;
                    }


                }

                if (e.Row.RowState.HasFlag(DataControlRowState.Edit))
                {
                    //drops de estado de referencia
                    DropDownList txtEstadoActual = (DropDownList)e.Row.FindControl("txtEstadoActual");                  
                    DataTable dt = SHconexion.Devuelve_Lista_Estados_Referencias();
                    txtEstadoActual.DataSource = dt;
                    txtEstadoActual.DataTextField = "Razon";
                    txtEstadoActual.DataValueField = "Razon";
                    txtEstadoActual.DataBind();
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    txtEstadoActual.SelectedValue = dr["EstadoActual"].ToString();


                    //drops de responsables
                    DataSet Operarios = SHconexion.Devuelve_mandos_intermedios_SMARTH();
                    Operarios.Tables[0].DefaultView.RowFilter = "Departamento = 'INGENIERIA' OR Departamento = '-'";
                    DataTable DTIngenieria = (Operarios.Tables[0].DefaultView).ToTable();
                    DropDownList txtResponsable = (DropDownList)e.Row.FindControl("txtResponsable");
                    txtResponsable.DataSource = DTIngenieria;
                    txtResponsable.DataTextField = "Nombre";
                    txtResponsable.DataValueField = "Nombre";
                    txtResponsable.DataBind();
                    DataRowView dr2 = e.Row.DataItem as DataRowView;
                    txtResponsable.SelectedValue = dr2["Responsable"].ToString();
                }
            }
        }

        //Redirecciones 
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

        
        

        //Acciones sobre operarios
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

       

    }

}