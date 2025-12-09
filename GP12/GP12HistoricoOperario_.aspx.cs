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
using System.Globalization;

namespace ThermoWeb.GP12
{
    public partial class GP12HistoricoOperario_ : System.Web.UI.Page
    {

        private static DataTable ds_Muro_Calidad = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargaListasFiltro();
                if (Request.QueryString["OPERARIO"] != null)
                {   
                    

                    selectReferencia.Value = Convert.ToString(Request.QueryString["REFERENCIA"]);
                    Rellenar_grid(null, null);
                }
            }
        }
       
        public void Rellenar_grid(object sender, EventArgs e)
        {
            try
            {
                Conexion_SMARTH SHconexion = new Conexion_SMARTH();

                //EVALUO FILTROS
                string referencia = "";
                string referenciaestado = "";
                if (selectReferencia.Value != "")
                {
                    string[] RecorteReferencia = selectReferencia.Value.Split(new char[] { '¬' });
                    referencia = " AND Referencia like '" + RecorteReferencia[0].Trim() + "%'";
                    referenciaestado = " AND P.Referencia like '" + RecorteReferencia[0].Trim() + "%'";
                    
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
                string clienteestado = "";
                

                string responsable = "";
                string responsableestado = "";
                

                string defectuosas = "";

                string fechainicio = "";
                if (InputFechaDesde.Value != "")
                {
                    fechainicio = " AND FechaInicio >= '" + InputFechaDesde.Value + "'";
                   
                }
                string fechafin = "";
                if (InputFechaHasta.Value != "")
                {
                    fechafin = " AND FechaInicio <= '" + InputFechaHasta.Value + "'";
                    
                }

                if (SwitchActivas.Checked)
                {
                    defectuosas = " AND ([Retrabajadas] > 0 OR [PiezasNOK] > 0)";
                }
                //ENSAMBLADO GRIDVIEWS
                ds_Muro_Calidad = SHconexion.Devuelve_ultimas_revisiones_periodo(Convert.ToInt32(Selecaño.SelectedValue), " AND YEAR([FechaInicio]) = " + Convert.ToString(Selecaño.SelectedValue) + "", referencia, molde, lote, cliente, responsable, fechainicio, fechafin, defectuosas,"");
                dgv_GP12_Historico.DataSource = ds_Muro_Calidad;
                dgv_GP12_Historico.DataBind();

                //AGREGA FOOTER Y LO CALCULA
                double totalRetrabajadas = 0;
                double totalMalas = 0;

               
                double totalBuenas = 0;
                foreach (DataRow dr in ds_Muro_Calidad.Rows)
                {
                    totalBuenas += Convert.ToDouble(dr["PiezasOK"]);
                }
                dgv_GP12_Historico.Columns[5].FooterText = totalBuenas.ToString();
                tbRevisadasOK.Text = totalBuenas.ToString();

                double totalHoras = 0;
                foreach (DataRow dr in ds_Muro_Calidad.Rows)
                {
                    totalHoras += Convert.ToDouble(dr["DoubleHoras"]);
                }
                totalHoras = Math.Truncate(totalHoras * 100) / 100;
                dgv_GP12_Historico.Columns[3].FooterText = totalHoras.ToString();

                tbHorasRevision.Text = totalHoras.ToString();
                double totalpersrevision = 0;
                
                foreach (DataRow dr in ds_Muro_Calidad.Rows)
                {
                    totalpersrevision += Convert.ToDouble(dr["CostePiezaRevision"].ToString().Replace(" €", "").Replace(".", ","));
                }
                totalpersrevision = Math.Truncate(totalpersrevision * 100) / 100;
                dgv_GP12_Historico.Columns[9].FooterText = totalpersrevision.ToString("C");
                tbCostesInspeccion.Text = totalpersrevision.ToString("C");

                if (ds_Muro_Calidad.Rows[0]["FakeMode"].ToString() != "1")
                {
                    //rellenar grid
                    foreach (DataRow dr in ds_Muro_Calidad.Rows)
                    {
                        totalRetrabajadas += Convert.ToDouble(dr["Retrabajadas"]);
                    }
                    dgv_GP12_Historico.Columns[6].FooterText = totalRetrabajadas.ToString();
                    tbRecuperadas.Text = totalRetrabajadas.ToString();

                    foreach (DataRow dr in ds_Muro_Calidad.Rows)
                    {
                        totalMalas += Convert.ToDouble(dr["PiezasNOK"]);
                    }
                    dgv_GP12_Historico.Columns[7].FooterText = totalMalas.ToString();
                    tbRevisadasNOK.Text = totalMalas.ToString();


                    double TotalRevisadas = (totalBuenas + totalMalas + totalRetrabajadas);
                    tbPiezasRevisadas.Text = TotalRevisadas.ToString();

                    double totalchatarra = 0.000;
                    
                    foreach (DataRow dr in ds_Muro_Calidad.Rows)
                    {

                        totalchatarra += Convert.ToDouble(dr["CosteScrapRevision"].ToString().Replace(" €", "").Replace(".", ","));
                    }

                   /*
                   totalchatarra = Math.Truncate(totalchatarra * 100) / 100;
                   dgv_GP12_Historico.Columns[8].FooterText = totalchatarra.ToString("C");
                   MuroScrap.Text = totalchatarra.ToString("C");


                   double totalrevision = 0;
                   foreach (DataRow dr in ds_Muro_Calidad.Rows)
                   {
                       totalrevision += Convert.ToDouble(dr["CosteRevision"].ToString().Replace(" €", "").Replace(".", ","));
                   }

                   dgv_GP12_Historico.Columns[10].FooterText = totalrevision.ToString("C");
                   MuroTotal.Text = totalrevision.ToString("C");
                   */

                   //sumo defectos
                    int Defecto1 = 0;
                    foreach (DataRow dr in ds_Muro_Calidad.Rows)
                    {
                        Defecto1 += Convert.ToInt32(dr["Def1"]);
                    }
                    dgv_GP12_Historico.Columns[14].FooterText = Defecto1.ToString();

                    int Defecto2 = 0;
                    foreach (DataRow dr in ds_Muro_Calidad.Rows)
                    {
                        Defecto2 += Convert.ToInt32(dr["Def2"]);
                    }
                    dgv_GP12_Historico.Columns[15].FooterText = Defecto2.ToString();

                    int Defecto3 = 0;
                    foreach (DataRow dr in ds_Muro_Calidad.Rows)
                    {
                        Defecto3 += Convert.ToInt32(dr["Def3"]);
                    }
                    dgv_GP12_Historico.Columns[16].FooterText = Defecto3.ToString();

                    int Defecto4 = 0;
                    foreach (DataRow dr in ds_Muro_Calidad.Rows)
                    {
                        Defecto4 += Convert.ToInt32(dr["Def4"]);
                    }
                    dgv_GP12_Historico.Columns[17].FooterText = Defecto4.ToString();

                    int Defecto5 = 0;
                    foreach (DataRow dr in ds_Muro_Calidad.Rows)
                    {
                        Defecto5 += Convert.ToInt32(dr["Def5"]);
                    }
                    dgv_GP12_Historico.Columns[18].FooterText = Defecto5.ToString();

                    int Defecto6 = 0;
                    foreach (DataRow dr in ds_Muro_Calidad.Rows)
                    {
                        Defecto6 += Convert.ToInt32(dr["Def6"]);
                    }
                    dgv_GP12_Historico.Columns[19].FooterText = Defecto6.ToString();

                    int Defecto7 = 0;
                    foreach (DataRow dr in ds_Muro_Calidad.Rows)
                    {
                        Defecto7 += Convert.ToInt32(dr["Def7"]);
                    }
                    dgv_GP12_Historico.Columns[20].FooterText = Defecto7.ToString();

                    int Defecto8 = 0;
                    foreach (DataRow dr in ds_Muro_Calidad.Rows)
                    {
                        Defecto8 += Convert.ToInt32(dr["Def8"]);
                    }
                    dgv_GP12_Historico.Columns[21].FooterText = Defecto8.ToString();

                    int Defecto9 = 0;
                    foreach (DataRow dr in ds_Muro_Calidad.Rows)
                    {
                        Defecto9 += Convert.ToInt32(dr["Def9"]);
                    }
                    dgv_GP12_Historico.Columns[22].FooterText = Defecto9.ToString();

                    int Defecto10 = 0;
                    foreach (DataRow dr in ds_Muro_Calidad.Rows)
                    {
                        Defecto10 += Convert.ToInt32(dr["Def10"]);
                    }
                    dgv_GP12_Historico.Columns[23].FooterText = Defecto10.ToString();

                    int Defecto11 = 0;
                    foreach (DataRow dr in ds_Muro_Calidad.Rows)
                    {
                        Defecto11 += Convert.ToInt32(dr["Def11"]);
                    }
                    dgv_GP12_Historico.Columns[24].FooterText = Defecto11.ToString();

                    int Defecto12 = 0;
                    foreach (DataRow dr in ds_Muro_Calidad.Rows)
                    {
                        Defecto12 += Convert.ToInt32(dr["Def12"]);
                    }
                    dgv_GP12_Historico.Columns[25].FooterText = Defecto12.ToString();

                    int Defecto13 = 0;
                    foreach (DataRow dr in ds_Muro_Calidad.Rows)
                    {
                        Defecto13 += Convert.ToInt32(dr["Def13"]);
                    }
                    dgv_GP12_Historico.Columns[26].FooterText = Defecto13.ToString();

                    int Defecto14 = 0;
                    foreach (DataRow dr in ds_Muro_Calidad.Rows)
                    {
                        Defecto14 += Convert.ToInt32(dr["Def14"]);
                    }
                    dgv_GP12_Historico.Columns[27].FooterText = Defecto14.ToString();

                    int Defecto15 = 0;
                    foreach (DataRow dr in ds_Muro_Calidad.Rows)
                    {
                        Defecto15 += Convert.ToInt32(dr["Def15"]);
                    }
                    dgv_GP12_Historico.Columns[28].FooterText = Defecto15.ToString();

                    int Defecto16 = 0;
                    foreach (DataRow dr in ds_Muro_Calidad.Rows)
                    {
                        Defecto16 += Convert.ToInt32(dr["Def16"]);
                    }
                    dgv_GP12_Historico.Columns[29].FooterText = Defecto16.ToString();

                    int Defecto17 = 0;
                    foreach (DataRow dr in ds_Muro_Calidad.Rows)
                    {
                        Defecto17 += Convert.ToInt32(dr["Def17"]);
                    }
                    dgv_GP12_Historico.Columns[30].FooterText = Defecto17.ToString();

                    int Defecto18 = 0;
                    foreach (DataRow dr in ds_Muro_Calidad.Rows)
                    {
                        Defecto18 += Convert.ToInt32(dr["Def18"]);
                    }
                    dgv_GP12_Historico.Columns[31].FooterText = Defecto18.ToString();

                    int Defecto19 = 0;
                    foreach (DataRow dr in ds_Muro_Calidad.Rows)
                    {
                        Defecto19 += Convert.ToInt32(dr["Def19"]);
                    }
                    dgv_GP12_Historico.Columns[32].FooterText = Defecto19.ToString();

                    int Defecto20 = 0;
                    foreach (DataRow dr in ds_Muro_Calidad.Rows)
                    {
                        Defecto20 += Convert.ToInt32(dr["Def20"]);
                    }
                    dgv_GP12_Historico.Columns[33].FooterText = Defecto20.ToString();

                    int Defecto21 = 0;
                    foreach (DataRow dr in ds_Muro_Calidad.Rows)
                    {
                        Defecto21 += Convert.ToInt32(dr["Def21"]);
                    }
                    dgv_GP12_Historico.Columns[34].FooterText = Defecto21.ToString();

                    int Defecto22 = 0;
                    foreach (DataRow dr in ds_Muro_Calidad.Rows)
                    {
                        Defecto22 += Convert.ToInt32(dr["Def22"]);
                    }
                    dgv_GP12_Historico.Columns[35].FooterText = Defecto22.ToString();

                    int Defecto23 = 0;
                    foreach (DataRow dr in ds_Muro_Calidad.Rows)
                    {
                        Defecto23 += Convert.ToInt32(dr["Def23"]);
                    }
                    dgv_GP12_Historico.Columns[36].FooterText = Defecto23.ToString();

                    int Defecto24 = 0;
                    foreach (DataRow dr in ds_Muro_Calidad.Rows)
                    {
                        Defecto24 += Convert.ToInt32(dr["Def24"]);
                    }
                    dgv_GP12_Historico.Columns[37].FooterText = Defecto24.ToString();

                    int Defecto25 = 0;
                    foreach (DataRow dr in ds_Muro_Calidad.Rows)
                    {
                        Defecto25 += Convert.ToInt32(dr["Def25"]);
                    }
                    dgv_GP12_Historico.Columns[38].FooterText = Defecto25.ToString();

                    int Defecto26 = 0;
                    foreach (DataRow dr in ds_Muro_Calidad.Rows)
                    {
                        Defecto26 += Convert.ToInt32(dr["Def26"]);
                    }
                    dgv_GP12_Historico.Columns[39].FooterText = Defecto26.ToString();

                    int Defecto27 = 0;
                    foreach (DataRow dr in ds_Muro_Calidad.Rows)
                    {
                        Defecto27 += Convert.ToInt32(dr["Def27"]);
                    }
                    dgv_GP12_Historico.Columns[40].FooterText = Defecto27.ToString();

                    int Defecto28 = 0;
                    foreach (DataRow dr in ds_Muro_Calidad.Rows)
                    {
                        Defecto28 += Convert.ToInt32(dr["Def28"]);
                    }
                    dgv_GP12_Historico.Columns[41].FooterText = Defecto28.ToString();

                    int Defecto29 = 0;
                    foreach (DataRow dr in ds_Muro_Calidad.Rows)
                    {
                        Defecto29 += Convert.ToInt32(dr["Def29"]);
                    }
                    dgv_GP12_Historico.Columns[42].FooterText = Defecto29.ToString();

                    int Defecto30 = 0;
                    foreach (DataRow dr in ds_Muro_Calidad.Rows)
                    {
                        Defecto30 += Convert.ToInt32(dr["Def30"]);
                    }
                    dgv_GP12_Historico.Columns[43].FooterText = Defecto30.ToString();
                }
                dgv_GP12_Historico.DataBind();

                

      



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
                Conexion_SMARTH SHconexion = new Conexion_SMARTH();
                Conexion_GP12 conexion = new Conexion_GP12();
                
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

                DataTable EstadoRevision = SHconexion.Devuelve_Lista_Estados_Referencias();
                lista_estado_revision.Items.Add("-");
                foreach (DataRow row in EstadoRevision.Rows)
                {
                    lista_estado_revision.Items.Add(row["Razon"].ToString());
                }

                DataSet DSInformador = Personal; //AbiertoPorV2
                DSInformador.Tables[0].DefaultView.RowFilter = "Departamento = 'CALIDAD' OR Departamento = 'PRODUCCION' OR Departamento = '-'";
                DataTable DTListainformador = DSInformador.Tables[0].DefaultView.ToTable();
                Informador.Items.Clear();
                foreach (DataRow row in DTListainformador.Rows)
                {
                    Informador.Items.Add(row["Nombre"].ToString());
                }
            }
            catch (Exception ex)
            {

            }
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

                    //lkb_Sort_Click("REVISIONES");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                }

                if (e.CommandName == "Redirect")
                {
                    Response.Redirect("GP12HistoricoReferencia.aspx?REFERENCIA=" + e.CommandArgument);
                }
            }
            catch (Exception ex)
            {
            }

        }

        protected void lbAbrir_Modal(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
      
      
        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i <= dgv_GP12_Historico.Rows.Count - 1; i++)
                {
                    Label FakeMode = (Label)dgv_GP12_Historico.Rows[i].FindControl("lblFAKE");
                    Label lblparent = (Label)dgv_GP12_Historico.Rows[i].FindControl("lblMalas");
                    Label lblparent2 = (Label)dgv_GP12_Historico.Rows[i].FindControl("lblRetrabajadas");
                    Label lblparent3 = (Label)dgv_GP12_Historico.Rows[i].FindControl("lblDefecto1");
                    Label lblparent4 = (Label)dgv_GP12_Historico.Rows[i].FindControl("lblDefecto2");
                    Label lblparent5 = (Label)dgv_GP12_Historico.Rows[i].FindControl("lblDefecto3");
                    Label lblparent6 = (Label)dgv_GP12_Historico.Rows[i].FindControl("lblDefecto4");
                    Label lblparent7 = (Label)dgv_GP12_Historico.Rows[i].FindControl("lblDefecto5");
                    Label lblparent8 = (Label)dgv_GP12_Historico.Rows[i].FindControl("lblDefecto6");
                    Label lblparent9 = (Label)dgv_GP12_Historico.Rows[i].FindControl("lblDefecto7");
                    Label lblparent10 = (Label)dgv_GP12_Historico.Rows[i].FindControl("lblDefecto8");
                    Label lblparent11 = (Label)dgv_GP12_Historico.Rows[i].FindControl("lblDefecto9");
                    Label lblparent12 = (Label)dgv_GP12_Historico.Rows[i].FindControl("lblDefecto10");
                    Label lblparent13 = (Label)dgv_GP12_Historico.Rows[i].FindControl("lblDefecto11");
                    Label lblparent14 = (Label)dgv_GP12_Historico.Rows[i].FindControl("lblDefecto12");
                    Label lblparent15 = (Label)dgv_GP12_Historico.Rows[i].FindControl("lblDefecto13");
                    Label lblparent16 = (Label)dgv_GP12_Historico.Rows[i].FindControl("lblDefecto14");
                    Label lblparent17 = (Label)dgv_GP12_Historico.Rows[i].FindControl("lblDefecto15");
                    Label lblparent18 = (Label)dgv_GP12_Historico.Rows[i].FindControl("lblDefecto16");
                    Label lblparent19 = (Label)dgv_GP12_Historico.Rows[i].FindControl("lblDefecto17");
                    Label lblparent20 = (Label)dgv_GP12_Historico.Rows[i].FindControl("lblDefecto18");
                    Label lblparent21 = (Label)dgv_GP12_Historico.Rows[i].FindControl("lblDefecto19");
                    Label lblparent22 = (Label)dgv_GP12_Historico.Rows[i].FindControl("lblDefecto20");
                    Label lblparent23 = (Label)dgv_GP12_Historico.Rows[i].FindControl("lblDefecto21");
                    Label lblparent24 = (Label)dgv_GP12_Historico.Rows[i].FindControl("lblDefecto22");
                    Label lblparent25 = (Label)dgv_GP12_Historico.Rows[i].FindControl("lblDefecto23");
                    Label lblparent26 = (Label)dgv_GP12_Historico.Rows[i].FindControl("lblDefecto24");
                    Label lblparent27 = (Label)dgv_GP12_Historico.Rows[i].FindControl("lblDefecto25");
                    Label lblparent28 = (Label)dgv_GP12_Historico.Rows[i].FindControl("lblDefecto26");
                    Label lblparent29 = (Label)dgv_GP12_Historico.Rows[i].FindControl("lblDefecto27");
                    Label lblparent30 = (Label)dgv_GP12_Historico.Rows[i].FindControl("lblDefecto28");
                    Label lblparent31 = (Label)dgv_GP12_Historico.Rows[i].FindControl("lblDefecto29");
                    Label lblparent32 = (Label)dgv_GP12_Historico.Rows[i].FindControl("lblDefecto30");

                    if (Convert.ToInt32(FakeMode.Text) == 1)
                    {
                        lblparent.Text = "0";
                        lblparent2.Text = "0";
                        lblparent3.Text = "0";
                        lblparent4.Text = "0";
                        lblparent5.Text = "0";
                        lblparent6.Text = "0";
                        lblparent7.Text = "0";
                        lblparent8.Text = "0";
                        lblparent9.Text = "0";
                        lblparent10.Text = "0";
                        lblparent11.Text = "0";
                        lblparent12.Text = "0";
                        lblparent13.Text = "0";
                        lblparent14.Text = "0";
                        lblparent15.Text = "0";
                        lblparent16.Text = "0";
                        lblparent17.Text = "0";
                        lblparent18.Text = "0";
                        lblparent19.Text = "0";
                        lblparent20.Text = "0";
                        lblparent21.Text = "0";
                        lblparent22.Text = "0";
                        lblparent23.Text = "0";
                        lblparent24.Text = "0";
                        lblparent25.Text = "0";
                        lblparent26.Text = "0";
                        lblparent27.Text = "0";
                        lblparent28.Text = "0";
                        lblparent29.Text = "0";
                        lblparent30.Text = "0";
                        lblparent31.Text = "0";
                        lblparent32.Text = "0";
                    }
                    else
                    {
                        //colorear celdas de defectos
                        if (lblparent.Text != "0")
                        {
                            dgv_GP12_Historico.Rows[i].Cells[7].BackColor = System.Drawing.Color.Red;
                            lblparent.ForeColor = System.Drawing.Color.White;
                            lblparent.Font.Bold = true;
                        }

                        if (lblparent2.Text != "0")
                        {
                            dgv_GP12_Historico.Rows[i].Cells[6].BackColor = System.Drawing.Color.Yellow;
                            lblparent2.ForeColor = System.Drawing.Color.Black;
                            lblparent2.Font.Bold = true;
                        }

                        if (lblparent3.Text != "0")
                        {
                            dgv_GP12_Historico.Rows[i].Cells[14].BackColor = System.Drawing.Color.Red;
                            lblparent3.ForeColor = System.Drawing.Color.White;
                            lblparent3.Font.Bold = true;
                        }

                        if (lblparent4.Text != "0")
                        {
                            dgv_GP12_Historico.Rows[i].Cells[15].BackColor = System.Drawing.Color.Red;
                            lblparent4.ForeColor = System.Drawing.Color.White;
                            lblparent4.Font.Bold = true;
                        }

                        if (lblparent5.Text != "0")
                        {
                            dgv_GP12_Historico.Rows[i].Cells[16].BackColor = System.Drawing.Color.Red;
                            lblparent5.ForeColor = System.Drawing.Color.White;
                            lblparent5.Font.Bold = true;
                        }

                        if (lblparent6.Text != "0")
                        {
                            dgv_GP12_Historico.Rows[i].Cells[17].BackColor = System.Drawing.Color.Red;
                            lblparent6.ForeColor = System.Drawing.Color.White;
                            lblparent6.Font.Bold = true;
                        }

                        if (lblparent7.Text != "0")
                        {
                            dgv_GP12_Historico.Rows[i].Cells[18].BackColor = System.Drawing.Color.Red;
                            lblparent7.ForeColor = System.Drawing.Color.White;
                            lblparent7.Font.Bold = true;
                        }

                        if (lblparent8.Text != "0")
                        {
                            dgv_GP12_Historico.Rows[i].Cells[19].BackColor = System.Drawing.Color.Red;
                            lblparent8.ForeColor = System.Drawing.Color.White;
                            lblparent8.Font.Bold = true;
                        }

                        if (lblparent9.Text != "0")
                        {
                            dgv_GP12_Historico.Rows[i].Cells[20].BackColor = System.Drawing.Color.Red;
                            lblparent9.ForeColor = System.Drawing.Color.White;
                            lblparent9.Font.Bold = true;
                        }

                        if (lblparent10.Text != "0")
                        {
                            dgv_GP12_Historico.Rows[i].Cells[21].BackColor = System.Drawing.Color.Red;
                            lblparent10.ForeColor = System.Drawing.Color.White;
                            lblparent10.Font.Bold = true;
                        }

                        if (lblparent11.Text != "0")
                        {
                            dgv_GP12_Historico.Rows[i].Cells[22].BackColor = System.Drawing.Color.Red;
                            lblparent11.ForeColor = System.Drawing.Color.White;
                            lblparent11.Font.Bold = true;
                        }

                        if (lblparent12.Text != "0")
                        {
                            dgv_GP12_Historico.Rows[i].Cells[23].BackColor = System.Drawing.Color.Red;
                            lblparent12.ForeColor = System.Drawing.Color.White;
                            lblparent12.Font.Bold = true;
                        }

                        if (lblparent13.Text != "0")
                        {
                            dgv_GP12_Historico.Rows[i].Cells[24].BackColor = System.Drawing.Color.Red;
                            lblparent13.ForeColor = System.Drawing.Color.White;
                            lblparent13.Font.Bold = true;
                        }

                        if (lblparent14.Text != "0")
                        {
                            dgv_GP12_Historico.Rows[i].Cells[25].BackColor = System.Drawing.Color.Red;
                            lblparent14.ForeColor = System.Drawing.Color.White;
                            lblparent14.Font.Bold = true;
                        }

                        if (lblparent15.Text != "0")
                        {
                            dgv_GP12_Historico.Rows[i].Cells[26].BackColor = System.Drawing.Color.Red;
                            lblparent15.ForeColor = System.Drawing.Color.White;
                            lblparent15.Font.Bold = true;
                        }

                        if (lblparent16.Text != "0")
                        {
                            dgv_GP12_Historico.Rows[i].Cells[27].BackColor = System.Drawing.Color.Red;
                            lblparent16.ForeColor = System.Drawing.Color.White;
                            lblparent16.Font.Bold = true;
                        }

                        if (lblparent17.Text != "0")
                        {
                            dgv_GP12_Historico.Rows[i].Cells[28].BackColor = System.Drawing.Color.Red;
                            lblparent17.ForeColor = System.Drawing.Color.White;
                            lblparent17.Font.Bold = true;
                        }

                        if (lblparent18.Text != "0")
                        {
                            dgv_GP12_Historico.Rows[i].Cells[29].BackColor = System.Drawing.Color.Red;
                            lblparent18.ForeColor = System.Drawing.Color.White;
                            lblparent18.Font.Bold = true;
                        }

                        if (lblparent19.Text != "0")
                        {
                            dgv_GP12_Historico.Rows[i].Cells[30].BackColor = System.Drawing.Color.Red;
                            lblparent19.ForeColor = System.Drawing.Color.White;
                            lblparent19.Font.Bold = true;
                        }

                        if (lblparent20.Text != "0")
                        {
                            dgv_GP12_Historico.Rows[i].Cells[31].BackColor = System.Drawing.Color.Red;
                            lblparent20.ForeColor = System.Drawing.Color.White;
                            lblparent20.Font.Bold = true;
                        }

                        if (lblparent21.Text != "0")
                        {
                            dgv_GP12_Historico.Rows[i].Cells[32].BackColor = System.Drawing.Color.Red;
                            lblparent21.ForeColor = System.Drawing.Color.White;
                            lblparent21.Font.Bold = true;
                        }

                        if (lblparent22.Text != "0")
                        {
                            dgv_GP12_Historico.Rows[i].Cells[33].BackColor = System.Drawing.Color.Red;
                            lblparent22.ForeColor = System.Drawing.Color.White;
                            lblparent22.Font.Bold = true;
                        }

                        if (lblparent23.Text != "0")
                        {
                            dgv_GP12_Historico.Rows[i].Cells[34].BackColor = System.Drawing.Color.Red;
                            lblparent23.ForeColor = System.Drawing.Color.White;
                            lblparent23.Font.Bold = true;
                        }

                        if (lblparent24.Text != "0")
                        {
                            dgv_GP12_Historico.Rows[i].Cells[35].BackColor = System.Drawing.Color.Red;
                            lblparent24.ForeColor = System.Drawing.Color.White;
                            lblparent24.Font.Bold = true;
                        }

                        if (lblparent25.Text != "0")
                        {
                            dgv_GP12_Historico.Rows[i].Cells[36].BackColor = System.Drawing.Color.Red;
                            lblparent25.ForeColor = System.Drawing.Color.White;
                            lblparent25.Font.Bold = true;
                        }

                        if (lblparent26.Text != "0")
                        {
                            dgv_GP12_Historico.Rows[i].Cells[37].BackColor = System.Drawing.Color.Red;
                            lblparent26.ForeColor = System.Drawing.Color.White;
                            lblparent26.Font.Bold = true;
                        }

                        if (lblparent27.Text != "0")
                        {
                            dgv_GP12_Historico.Rows[i].Cells[38].BackColor = System.Drawing.Color.Red;
                            lblparent27.ForeColor = System.Drawing.Color.White;
                            lblparent27.Font.Bold = true;
                        }

                        if (lblparent28.Text != "0")
                        {
                            dgv_GP12_Historico.Rows[i].Cells[39].BackColor = System.Drawing.Color.Red;
                            lblparent28.ForeColor = System.Drawing.Color.White;
                            lblparent28.Font.Bold = true;
                        }

                        if (lblparent29.Text != "0")
                        {
                            dgv_GP12_Historico.Rows[i].Cells[40].BackColor = System.Drawing.Color.Red;
                            lblparent29.ForeColor = System.Drawing.Color.White;
                            lblparent29.Font.Bold = true;
                        }

                        if (lblparent30.Text != "0")
                        {
                            dgv_GP12_Historico.Rows[i].Cells[41].BackColor = System.Drawing.Color.Red;
                            lblparent30.ForeColor = System.Drawing.Color.White;
                            lblparent30.Font.Bold = true;
                        }

                        if (lblparent31.Text != "0")
                        {
                            dgv_GP12_Historico.Rows[i].Cells[42].BackColor = System.Drawing.Color.Red;
                            lblparent31.ForeColor = System.Drawing.Color.White;
                            lblparent31.Font.Bold = true;
                        }

                        if (lblparent32.Text != "0")
                        {
                            dgv_GP12_Historico.Rows[i].Cells[43].BackColor = System.Drawing.Color.Red;
                            lblparent32.ForeColor = System.Drawing.Color.White;
                            lblparent32.Font.Bold = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            { }


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

        protected void Cargar_todas(object sender, EventArgs e)
        {
            selectReferencia.Value = "";
            selectMolde.Value = "";
            selectLote.Value = "";
            Rellenar_grid(null, null);
        }

        //VALIDACIÓN DE OPERARIO
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
                        query = " SET FechaInfo1 = '" + fechainfo + "', Informador1 = '" + Informador.SelectedValue.ToString() + "', FeedbackOPERARIOS = '" + FeedbackOPERARIOS.Value + "', FirmaNOp1 = '" + signatureJSON.Value + "'";
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


    }

}