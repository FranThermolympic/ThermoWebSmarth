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
using System.Text;
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


namespace ThermoWeb.AREA_RECHAZO
{
    public partial class GestionScrapGP12 : System.Web.UI.Page
    {

        private static DataTable ds_Referencias = new DataTable();
        private static DataTable ds_Estado_Referencias = new DataTable();
        private static DataTable ds_Muro_Calidad = new DataTable();

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
                DataSet Personal = SHconexion.Devuelve_mandos_intermedios_SMARTH();            
                DataSet DSInformador = Personal; //AbiertoPorV2
                DSInformador.Tables[0].DefaultView.RowFilter = "Departamento = 'CALIDAD' OR Departamento = 'PRODUCCION' OR Departamento = '-'";
                DataTable DTListainformador = DSInformador.Tables[0].DefaultView.ToTable();
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
                //ENSAMBLADO GRIDVIEWS
                ds_Muro_Calidad = SHconexion.Devuelve_ultimas_revisiones_periodo(Convert.ToInt32(Selecaño.SelectedValue), " AND YEAR([FechaInicio]) = " + Convert.ToString(Selecaño.SelectedValue) + " AND PiezasNOK > 0 AND ScrapAlmacen = 0 ", "", "", "", "", "", "", "", "", "");
                dgv_GP12_Historico.DataSource = ds_Muro_Calidad;
                dgv_GP12_Historico.DataBind();
 
            }
            catch (Exception ex)
            {

            }
        }

        protected void GridView_DataBoundHist(object sender, EventArgs e)
        {
            for (int i = 0; i <= dgv_GP12_Historico.Rows.Count - 1; i++)
            {

                Label lblObservaciones = (Label)dgv_GP12_Historico.Rows[i].FindControl("lblObservaciones");
                Label lblNotas = (Label)dgv_GP12_Historico.Rows[i].FindControl("lblNotas");

                Label lblscrap = (Label)dgv_GP12_Historico.Rows[i].FindControl("lblMalas");


                Label lblretrabajo = (Label)dgv_GP12_Historico.Rows[i].FindControl("lblRetrabajadas");

                Label FakeMode = (Label)dgv_GP12_Historico.Rows[i].FindControl("lblFAKE");
                if (FakeMode.Text == "1")
                {
                    lblscrap.Text = "0";
                    lblretrabajo.Text = "0";
                }
                else
                {
                    if (lblscrap.Text != "0")
                    {
                        dgv_GP12_Historico.Rows[i].Cells[7].BackColor = System.Drawing.Color.Red;
                        lblscrap.ForeColor = System.Drawing.Color.White;
                        lblscrap.Font.Bold = true;
                    }
                    if (lblretrabajo.Text != "0")
                    {
                        dgv_GP12_Historico.Rows[i].Cells[6].BackColor = System.Drawing.Color.Orange;
                        lblretrabajo.ForeColor = System.Drawing.Color.Black;
                        lblretrabajo.Font.Bold = true;
                    }
                    if (lblObservaciones.Text != "<br />")
                    {
                        lblObservaciones.Visible = true;
                    }
                    if (lblNotas.Text != "")
                    {
                        lblNotas.Visible = true;
                    }
                }

            }
        }
        // carga la lista utilizando un filtro
               
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

                if (e.CommandName == "Redirect")
                {
                    Response.Redirect("GP12HistoricoReferencia.aspx?REFERENCIA=" + e.CommandArgument);
                }

                if (e.CommandName == "MoverScrap")
                {
                    Conexion_SMARTH SHconexion = new Conexion_SMARTH();
                    //Consulta detalle de la linea
                    DataTable LineaGP12 = SHconexion.Devuelve_ultimas_revisiones_periodoID(Convert.ToInt32(Selecaño.SelectedValue), Convert.ToInt32(e.CommandArgument.ToString()));
                    
                    //Concatena la instrucción y lo crea en la ruta del conector de NAV
                    StringBuilder exportar = new StringBuilder();
                    string CadenaTexto = "" + ";" + LineaGP12.Rows[0]["Referencia"].ToString() + ";"+ LineaGP12.Rows[0]["Nlote"].ToString() + ";" + LineaGP12.Rows[0]["Nlote"].ToString() + "/00;-"+ LineaGP12.Rows[0]["PiezasNOK"].ToString().Replace(",", ".") + ";1;" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ";;0";
                    //string folderPath = @"X:\";
                    string folderPath = @"\\thermonav\ExportNAV\";
                    
                    //string folderPath = @"C:\TEST\";
                    File.WriteAllText(folderPath + "BMSEXPORT_"+ DateTime.Now.ToString("yy-MM-dd_HHmmss")+"GP12.csv", CadenaTexto.ToString());

                    //Actualiza la base de datos de GP12
                    SHconexion.Actualizar_SCRAP_GP1XNAV(Convert.ToInt32(Selecaño.SelectedValue), Convert.ToInt32(e.CommandArgument.ToString()));

                    Rellenar_grid(null, null);
                }

                if (e.CommandName == "BorrarScrap")
                {
                    Conexion_SMARTH SHconexion = new Conexion_SMARTH();
                    SHconexion.Actualizar_SCRAP_GP1XNAV(Convert.ToInt32(Selecaño.SelectedValue), Convert.ToInt32(e.CommandArgument.ToString()));
                    Rellenar_grid(null, null);
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
                    Rellenar_grid(null, null);

                }
                catch (Exception)
                {
                }

            }

       

    }

}