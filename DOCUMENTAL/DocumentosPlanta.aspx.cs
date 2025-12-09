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

namespace ThermoWeb.DOCUMENTAL
{
    public partial class DocumentosPlanta : System.Web.UI.Page
    {
        private static DataSet datosorden = new DataSet();
        private static DataSet ds_DocumentosPlanta = new DataSet();
        private static DataSet Experiencia = new DataSet();
        private static DataTable Trabajadores2 = new DataTable();
        private static string Maquina = "";
        private static int AvisoSinLog = 0;
        private string NumMolde = "";


        protected void Page_Load(object sender, EventArgs e)
        {

            if (IsPostBack)
            {
                HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate");
                HttpContext.Current.Response.AddHeader("Pragma", "no-cache");
                HttpContext.Current.Response.AddHeader("Expires", "0");
                Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(1));
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetNoStore();
                Carga_Datos();
            }
            if (!IsPostBack)
            {
                AvisoSinLog = 0;
                HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate");
                HttpContext.Current.Response.AddHeader("Pragma", "no-cache");
                HttpContext.Current.Response.AddHeader("Expires", "0");
                Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(1));
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetNoStore();
                Carga_Datos();
            }
        }

        public void Carga_Datos()
        {
            string referenciactiva1 = "";
            string referenciactiva2 = "";
            string referenciactiva3 = "";
            string referenciactiva4 = "";
            string referenciactiva5 = "";
            try
            {
                if (Request.QueryString["MAQUINA"] != null)
                {
                    LabelNumMaquina.InnerText = "Máquina " + Request.QueryString["MAQUINA"].ToString();
                    Maquina = Request.QueryString["MAQUINA"].ToString();
                    Conexion_DOCUMENTAL conexion = new Conexion_DOCUMENTAL();
                    datosorden = conexion.DevuelveOrdenProducciendo(Convert.ToString(Request.QueryString["MAQUINA"]));

                    dgv_Ordenes.DataSource = datosorden;
                    dgv_Ordenes.DataBind();
                    
                    //ALMACENO FILTROS PARA LAS CONSULTAS
                    if (datosorden.Tables[0].Rows.Count > 0)
                    {
                        referenciactiva1 = datosorden.Tables[0].Rows[0]["C_PRODUCT_ID"].ToString();
                    }
                    if (datosorden.Tables[0].Rows.Count > 1)
                    {
                        referenciactiva2 = " OR P.Referencia = '" + datosorden.Tables[0].Rows[1]["C_PRODUCT_ID"].ToString() + "'";
                    }
                    if (datosorden.Tables[0].Rows.Count > 2)
                    {
                        referenciactiva3 = " OR P.Referencia = '" + datosorden.Tables[0].Rows[2]["C_PRODUCT_ID"].ToString() + "'";
                    }
                    if (datosorden.Tables[0].Rows.Count > 3)
                    {
                        referenciactiva4 = " OR P.Referencia = '" + datosorden.Tables[0].Rows[3]["C_PRODUCT_ID"].ToString() + "'";
                    }
                    if (datosorden.Tables[0].Rows.Count > 4)
                    {
                        referenciactiva5 = " OR P.Referencia = '" +datosorden.Tables[0].Rows[4]["C_PRODUCT_ID"].ToString() + "'";
                    }

                    NumMolde = datosorden.Tables[0].Rows[0]["C_TOOL_ID"].ToString();
                    NumeroMolde.Value = datosorden.Tables[0].Rows[0]["C_TOOL_ID"].ToString();
                    CargarFramesDU11();

                    CargarFramesMaquina(referenciactiva1, referenciactiva2, referenciactiva3, referenciactiva4, referenciactiva5);
                    CargarCarruselGP12(referenciactiva1, referenciactiva2, referenciactiva3, referenciactiva4, referenciactiva5);
                    CargarTrabajadores();
                }
                else
                {
                    referenciactiva1 = Request.QueryString["REFERENCIA"].ToString();
                    NUM1.Visible = false;
                    navbarrecarga.Visible = false;
                    CargarFramesDU11();
                    CargarFramesMaquina(referenciactiva1, referenciactiva2, referenciactiva3, referenciactiva4, referenciactiva5);
                    CargarCarruselGP12(referenciactiva1, referenciactiva2, referenciactiva3, referenciactiva4, referenciactiva5);
                    CargarTrabajadores();
                }
            }
            catch (Exception)
            { }
        }
   
        public void CargarFramesMaquina(string ref1, string ref2, string ref3, string ref4, string ref5)
        {
            try
            {

                Conexion_DOCUMENTAL conexion = new Conexion_DOCUMENTAL();

                if (ref1 != "")
                {
                    ref1labtext.InnerText = ref1;
                    ds_DocumentosPlanta = conexion.Devuelve_dataset_filtroreferenciasSMARTH(ref1, ref2, ref3, ref4, ref5);


                    //REFERENCIA 1

                    ds_DocumentosPlanta.Tables[0].DefaultView.RowFilter = "REF = '"+datosorden.Tables[0].Rows[0]["C_PRODUCT_ID"].ToString()+"'";
                    DataTable DTDOCUMENTAL = (ds_DocumentosPlanta.Tables[0].DefaultView).ToTable();
                    
                    if (DTDOCUMENTAL.Rows[0]["PautaControl"].ToString() != "")
                    {
                        PautaControl_1.Attributes.Add("src", DTDOCUMENTAL.Rows[0]["PautaControl"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                    }
                    else
                    {
                        PautaControl_1.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                    }

                    if (DTDOCUMENTAL.Rows[0]["Defoteca"].ToString() != "")
                    {
                        DEFECTOS_1.Attributes.Add("src", DTDOCUMENTAL.Rows[0]["Defoteca"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                    }
                    else
                    {
                        DEFECTOS_1.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                    }

                    if (DTDOCUMENTAL.Rows[0]["OperacionEstandar"].ToString() != "")
                    {
                        GP12_1.Attributes.Add("src", DTDOCUMENTAL.Rows[0]["OperacionEstandar"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                    }
                    else
                    {
                        GP12_1.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                    }

                    if (DTDOCUMENTAL.Rows[0]["Embalaje"].ToString() != "")
                    {
                        PAUTAEMBALAJE_1.Attributes.Add("src", DTDOCUMENTAL.Rows[0]["Embalaje"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                    }
                    else
                    {
                        PAUTAEMBALAJE_1.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                    }

                    if (DTDOCUMENTAL.Rows[0]["PautaRecepcion2"].ToString() != "")
                    {
                        PAUTAEMBALAJEALTERNATIVO_1.Attributes.Add("src", DTDOCUMENTAL.Rows[0]["PautaRecepcion2"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                    }
                    else
                    {
                        PAUTAEMBALAJEALTERNATIVO_1.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                    }




                    if (DTDOCUMENTAL.Rows[0]["Logotipo"].ToString() != "")
                    {
                        CLIENTE.ImageUrl = DTDOCUMENTAL.Rows[0]["Logotipo"].ToString();
                    }
                    else
                    {
                        CLIENTE.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                    }

                    //DOCUMENTOS VINCULADOS
                    DataTable Vinculados = conexion.Devuelve_Documentos_Vinculados_SMARTH(datosorden.Tables[0].Rows[0]["C_PRODUCT_ID"].ToString(), datosorden.Tables[0].Rows[0]["C_TOOL_ID"].ToString());
                    GridVinculados.DataSource = Vinculados;
                    GridVinculados.DataBind();


                    //COMPRUEBO ESTADOS DE GP12 - FALTA INCLUIR RESTO DE REFERENCIAS
                    DataSet estadoGP12 = conexion.devuelve_estado_GP12(ref1);
                    if (Convert.ToInt32(estadoGP12.Tables[0].Rows[0]["EstadoActual"].ToString()) > 0)
                    {
                        tbAlertaCalidad1.Visible = true;
                        INAlertaCalidad1.InnerText = " PRODUCTO PENDIENTE DE MURO DE CALIDAD - " + estadoGP12.Tables[0].Rows[0]["Razon"].ToString();

                    }

                    DataSet ModDOCU = conexion.devuelve_fechaModDocumentos(NumMolde);
                    tbfechaMod.Text = ModDOCU.Tables[0].Rows[0]["FechaModificacion"].ToString();
                    RazonMod.Text = ModDOCU.Tables[0].Rows[0]["RazonModificacion"].ToString();

                    //VERIFICO MODO DE TRABAJO Y OCULTO PESTAÑAS
                    if (DTDOCUMENTAL.Rows[0]["CLINO"].ToString().TrimEnd() == "ARAVEN S.L.")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ModoAraven();", true);
                    }

                }
                if (ref2 != "")
                {
                    ref2labtext.InnerText = datosorden.Tables[0].Rows[1]["C_PRODUCT_ID"].ToString();
                    ref2lab.Visible = true;

                    ds_DocumentosPlanta.Tables[0].DefaultView.RowFilter = "REF = '" + datosorden.Tables[0].Rows[1]["C_PRODUCT_ID"].ToString() + "'";
                    DataTable DTDOCUMENTAL = (ds_DocumentosPlanta.Tables[0].DefaultView).ToTable();


                    if (DTDOCUMENTAL.Rows[0]["PautaControl"].ToString() != "")
                    {
                        PautaControl_2.Attributes.Add("src", DTDOCUMENTAL.Rows[0]["PautaControl"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                    }
                    else
                    {
                        PautaControl_2.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                    }
                    if (DTDOCUMENTAL.Rows[0]["Defoteca"].ToString() != "")
                    {
                        DEFECTOS_2.Attributes.Add("src", DTDOCUMENTAL.Rows[0]["Defoteca"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                    }
                    else
                    {
                        DEFECTOS_2.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                    }
                    if (DTDOCUMENTAL.Rows[0]["OperacionEstandar"].ToString() != "")
                    {
                        GP12_2.Attributes.Add("src", DTDOCUMENTAL.Rows[0]["OperacionEstandar"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                    }
                    else
                    {
                        GP12_2.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                    }
                    if (DTDOCUMENTAL.Rows[0]["Embalaje"].ToString() != "")
                    {
                        PAUTAEMBALAJE_2.Attributes.Add("src", DTDOCUMENTAL.Rows[0]["Embalaje"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                    }
                    else
                    {
                        PAUTAEMBALAJE_2.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                    }
                    if (DTDOCUMENTAL.Rows[0]["PautaRecepcion2"].ToString() != "")
                    {
                        PAUTAEMBALAJEALTERNATIVO_2.Attributes.Add("src", DTDOCUMENTAL.Rows[0]["PautaRecepcion2"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                    }
                    else
                    {
                        PAUTAEMBALAJEALTERNATIVO_2.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                    }

                    //DOCUMENTOS VINCULADOS2
                    DataTable Vinculados = conexion.Devuelve_Documentos_Vinculados_SMARTH(datosorden.Tables[0].Rows[1]["C_PRODUCT_ID"].ToString(), datosorden.Tables[0].Rows[1]["C_TOOL_ID"].ToString());
                    GridVinculados2.DataSource = Vinculados;
                    GridVinculados2.DataBind();
                }
                if (ref3 != "")
                {
                    ref3lab.Visible = true;
                    ref3labtext.InnerText = datosorden.Tables[0].Rows[2]["C_PRODUCT_ID"].ToString();

                    ds_DocumentosPlanta.Tables[0].DefaultView.RowFilter = "REF = '" + datosorden.Tables[0].Rows[2]["C_PRODUCT_ID"].ToString() + "'";
                    DataTable DTDOCUMENTAL = (ds_DocumentosPlanta.Tables[0].DefaultView).ToTable();

                    if (DTDOCUMENTAL.Rows[0]["PautaControl"].ToString() != "")
                    {
                        PautaControl_3.Attributes.Add("src", DTDOCUMENTAL.Rows[0]["PautaControl"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                    }
                    else
                    {
                        PautaControl_3.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                    }
                    if (DTDOCUMENTAL.Rows[0]["Defoteca"].ToString() != "")
                    {
                        DEFECTOS_3.Attributes.Add("src", DTDOCUMENTAL.Rows[0]["Defoteca"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                    }
                    else
                    {
                        DEFECTOS_3.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                    }
                    if (DTDOCUMENTAL.Rows[0]["OperacionEstandar"].ToString() != "")
                    {
                        GP12_3.Attributes.Add("src", DTDOCUMENTAL.Rows[0]["OperacionEstandar"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                    }
                    else
                    {
                        GP12_3.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                    }
                    if (DTDOCUMENTAL.Rows[0]["Embalaje"].ToString() != "")
                    {
                        PAUTAEMBALAJE_3.Attributes.Add("src", DTDOCUMENTAL.Rows[0]["Embalaje"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                    }
                    else
                    {
                        PAUTAEMBALAJE_3.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                    }
                    if (DTDOCUMENTAL.Rows[0]["PautaRecepcion2"].ToString() != "")
                    {
                        PAUTAEMBALAJEALTERNATIVO_3.Attributes.Add("src", DTDOCUMENTAL.Rows[0]["PautaRecepcion2"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                    }
                    else
                    {
                        PAUTAEMBALAJEALTERNATIVO_3.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                    }
                    DataTable Vinculados = conexion.Devuelve_Documentos_Vinculados_SMARTH(datosorden.Tables[0].Rows[2]["C_PRODUCT_ID"].ToString(), datosorden.Tables[0].Rows[2]["C_TOOL_ID"].ToString());
                    GridVinculados3.DataSource = Vinculados;
                    GridVinculados3.DataBind();
                }              
                if (ref4 != "")
                {
                    ref4lab.Visible = true;
                    ref4labtext.InnerText = datosorden.Tables[0].Rows[3]["C_PRODUCT_ID"].ToString();

                    ds_DocumentosPlanta.Tables[0].DefaultView.RowFilter = "REF = '" + datosorden.Tables[0].Rows[3]["C_PRODUCT_ID"].ToString() + "'";
                    DataTable DTDOCUMENTAL = (ds_DocumentosPlanta.Tables[0].DefaultView).ToTable();

                    if (DTDOCUMENTAL.Rows[0]["PautaControl"].ToString() != "")
                    {
                        PautaControl_4.Attributes.Add("src", DTDOCUMENTAL.Rows[0]["PautaControl"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                    }
                    else
                    {
                        PautaControl_4.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                    }
                    if (DTDOCUMENTAL.Rows[0]["Defoteca"].ToString() != "")
                    {
                        DEFECTOS_4.Attributes.Add("src", DTDOCUMENTAL.Rows[0]["Defoteca"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                    }
                    else
                    {
                        DEFECTOS_4.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                    }
                    if (DTDOCUMENTAL.Rows[0]["OperacionEstandar"].ToString() != "")
                    {
                        GP12_4.Attributes.Add("src", DTDOCUMENTAL.Rows[0]["OperacionEstandar"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                    }
                    else
                    {
                        GP12_4.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                    }
                    if (DTDOCUMENTAL.Rows[0]["Embalaje"].ToString() != "")
                    {
                        PAUTAEMBALAJE_4.Attributes.Add("src", DTDOCUMENTAL.Rows[0]["Embalaje"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                    }
                    else
                    {
                        PAUTAEMBALAJE_4.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                    }
                    if (DTDOCUMENTAL.Rows[0]["PautaRecepcion2"].ToString() != "")
                    {
                        PAUTAEMBALAJEALTERNATIVO_4.Attributes.Add("src", DTDOCUMENTAL.Rows[0]["PautaRecepcion2"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                    }
                    else
                    {
                        PAUTAEMBALAJEALTERNATIVO_4.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                    }
                    DataTable Vinculados = conexion.Devuelve_Documentos_Vinculados_SMARTH(datosorden.Tables[0].Rows[3]["C_PRODUCT_ID"].ToString(), datosorden.Tables[0].Rows[3]["C_TOOL_ID"].ToString());
                    GridVinculados4.DataSource = Vinculados;
                    GridVinculados4.DataBind();

                    
                }
                
            }
            catch (Exception EX)
            { }
        }

        protected void OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DocVinculado.Attributes.Add("src", e.CommandArgument.ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopDocVinculados();", true);

            }
            catch (Exception ex)
            { }
            
        }
        public void CargarCarruselGP12(string ref1, string ref2, string ref3, string ref4, string ref5)
        {
            try
            {
                DataSet imagenes = new DataSet();
                Conexion_DOCUMENTAL conexion = new Conexion_DOCUMENTAL();

                if (ref1 != "")
                {
                    imagenes = conexion.DatasetDetectadosGP12(datosorden.Tables[0].Rows[0]["C_PRODUCT_ID"].ToString());
                    foreach (DataRow row in imagenes.Tables[0].Rows)
                    {
                        HtmlGenericControl divItem = new HtmlGenericControl("div");
                        divItem.Attributes["class"] = "carousel-item";
                        //divItem.ID = "prueba";
                        Image img = new Image();
                        img.ImageUrl = row["ImagenDefecto1"].ToString();
                        img.Visible = true;
                        img.Attributes["class"] = "d-block w-100";
                        divItem.Controls.Add(img);
                        //img.Width = new Unit("100%");
                        img.Height = new Unit("600px");
                        ACTIVOS.Controls.Add(divItem);
                    }
                }
                if (ref2 != "")
                {
                    imagenes = conexion.DatasetDetectadosGP12(datosorden.Tables[0].Rows[1]["C_PRODUCT_ID"].ToString());
                    foreach (DataRow row in imagenes.Tables[0].Rows)
                    {
                        HtmlGenericControl divItem = new HtmlGenericControl("div");
                        divItem.Attributes["class"] = "carousel-item";
                        Image img = new Image();
                        img.ImageUrl = row["ImagenDefecto1"].ToString();
                        img.Visible = true;
                        img.Attributes["class"] = "d-block w-100";
                        divItem.Controls.Add(img);
                        //img.Width = new Unit("100%");
                        img.Height = new Unit("600px");
                        ACTIVOS2.Controls.Add(divItem);
                    }
                }
                if (ref3 != "")
                {
                    imagenes = conexion.DatasetDetectadosGP12(datosorden.Tables[0].Rows[2]["C_PRODUCT_ID"].ToString());
                    foreach (DataRow row in imagenes.Tables[0].Rows)
                    {
                        HtmlGenericControl divItem = new HtmlGenericControl("div");
                        divItem.Attributes["class"] = "carousel-item";
                        Image img = new Image();
                        img.ImageUrl = row["ImagenDefecto1"].ToString();
                        img.Visible = true;
                        img.Attributes["class"] = "d-block w-100";
                        divItem.Controls.Add(img);
                        //img.Width = new Unit("100%");
                        img.Height = new Unit("600px");
                        ACTIVOS3.Controls.Add(divItem);
                    }
                }
                if (ref4 != "")
                {
                    imagenes = conexion.DatasetDetectadosGP12(datosorden.Tables[0].Rows[3]["C_PRODUCT_ID"].ToString());
                    foreach (DataRow row in imagenes.Tables[0].Rows)
                    {
                        HtmlGenericControl divItem = new HtmlGenericControl("div");
                        divItem.Attributes["class"] = "carousel-item";
                         Image img = new Image();
                        img.ImageUrl = row["ImagenDefecto1"].ToString();
                        img.Visible = true;
                        img.Attributes["class"] = "d-block w-100";
                        divItem.Controls.Add(img);
                        //img.Width = new Unit("100%");
                        img.Height = new Unit("600px");
                        ACTIVOS4.Controls.Add(divItem);
                    }
                }

            }
            catch (Exception)
            { }
        }
  
        public void CargarTrabajadores()
        {
            try
            {
                //CONSULTA LA VALIDACION DE DOCUMENTACION POR PARTE DEL OPERARIO 1

                Conexion_DOCUMENTAL conexion = new Conexion_DOCUMENTAL();

                DataSet Trabajadores = conexion.Devuelve_LogueadoXMaquina(Request.QueryString["MAQUINA"].ToString());
                Experiencia = conexion.Devuelve_HorasXReferenciaSMARTH(" AND C_TOOL = '"+NumMolde+"'");

                GridPolivalencia.DataSource = Experiencia;
                GridPolivalencia.DataBind();

                Trabajadores2 = Trabajadores.Tables[0];
                DataTable Experiencia2 = Experiencia.Tables[0];

                var JoinResult = (from p in Trabajadores2.AsEnumerable()
                                  join t in Experiencia2.AsEnumerable()

                                  on p.Field<string>("C_CLOCKNO") equals t.Field<string>("C_CLOCKNUMBER") into tempJoin
                                  from leftJoin in tempJoin.DefaultIfEmpty()
                                  select new
                                  {
                                      NUMOP = p.Field<string>("C_CLOCKNO"),
                                      NOMBREOP = p.Field<string>("C_NAME"),
                                      TIPOOPE = p.Field<string>("C_OPERATORTYPE"),
                                      TIEMPOHORAS = leftJoin == null ? 0 : leftJoin.Field<Decimal>("TIEMPOHORAS"),
                                      NIVELOPE = leftJoin == null ? "I" : leftJoin.Field<string>("NIVEL"),
                                  }).ToList();
                dgv_Personal.DataSource = JoinResult;
                dgv_Personal.DataBind();

                Trabajadores2.DefaultView.RowFilter = "C_OPERATORTYPE = 'OPERARIO'";
                Trabajadores2 = Trabajadores2.DefaultView.ToTable();

                //Compruebo validaciones
                if (Trabajadores2.Rows.Count == 0)
                {
                    
                    AlertaDOCTEXT.Text = "Ningún operario logueado. Por favor, logueate en BMS y pulsa 'Recargar con orden actual'";
                    if (AvisoSinLog == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupDocumentacion();", true);
                        AvisoSinLog = 1;
                    }

                    }
                else if (!conexion.existe_validacion_operario(Trabajadores2.Rows[0]["C_CLOCKNO"].ToString(), NumMolde))
                {
                    conexion.insertar_validacion_operario(Trabajadores2.Rows[0]["C_CLOCKNO"].ToString(), NumMolde);
                    string ALoperario = Trabajadores2.Rows[0]["C_NAME"].ToString();
                    string AlCambios = RazonMod.Text + " (" + tbfechaMod.Text + ")";
                    
                    AlertaDOCTEXT.Text = ALoperario + " hay documentación nueva disponible para su consulta. Por favor, revísala antes de continuar con la producción.";
                    AlertaDOCTEXTCAMBIOS.Text = "<strong>Documentos modificados:</strong> " + AlCambios + "";
                    if(AvisoSinLog == 0)
                    { 
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupDocumentacion();", true);
                    AvisoSinLog = 1;
                    }

                }
                else
                {
                    if (conexion.existe_validacion_operario_enfecha(Trabajadores2.Rows[0]["C_CLOCKNO"].ToString(), NumMolde, tbfechaMod.Text))
                    {
                        conexion.resetea_alarma_validacion_operario(Trabajadores2.Rows[0]["C_CLOCKNO"].ToString(), NumMolde);
                        string ALoperario = Trabajadores2.Rows[0]["C_NAME"].ToString();
                        string AlCambios = RazonMod.Text + " (" + tbfechaMod.Text + ")";
                     
                        AlertaDOCTEXT.Text = ALoperario + " hay documentación nueva disponible para su consulta. Por favor, revísala antes de continuar con la producción.";
                        AlertaDOCTEXTCAMBIOS.Text = "<strong>Documentos modificados:</strong> " + AlCambios + "";
                        if (AvisoSinLog == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupDocumentacion();", true);
                            AvisoSinLog = 1;
                        }

                    }
                    else
                    {
                        //AlertaDOC1.Visible = false;
                    }
                }

                //Asigno HIDDENS
                if (Trabajadores2.Rows.Count == 1)
                {
                    Operario1Numero.Value = Trabajadores2.Rows[0]["C_CLOCKNO"].ToString();
                }
                else 
                {
                    Operario1Numero.Value = "---";
                }
                if (Trabajadores2.Rows.Count == 2)
                {
                    Operario2Numero.Value = Trabajadores2.Rows[1]["C_CLOCKNO"].ToString();
                }
                else
                {
                    Operario2Numero.Value = "---";
                }

            }
            catch (Exception EX)
            { }

        }

        public void CargarFramesDU11()
        {
            string MAQ = Request.QueryString["MAQUINA"].ToString();
            
            switch (MAQ)
            {
                case "12":
                    IframeDU11_1.Src = "http://10.0.2.1/";
                    btn_DU11_1.InnerText = "MÁQUINA 12";
                    btn_DU11_2.Visible = false;
                    btn_DU11_3.Visible = false;
                    break;
                case "15":
                    IframeDU11_1.Src = "http://10.0.0.170/";
                    btn_DU11_1.InnerText = "MÁQUINA 15";
                    btn_DU11_2.Visible = false;
                    btn_DU11_3.Visible = false;
                    break;
                case "16":
                    IframeDU11_1.Src = "http://10.0.0.176/";
                    btn_DU11_1.InnerText = "MÁQUINA 16";
                    btn_DU11_2.Visible = false;
                    btn_DU11_3.Visible = false;
                    break;
                case "22":
                    IframeDU11_1.Src = "http://10.0.2.30/";
                    btn_DU11_1.InnerText = "MÁQUINA 22";
                    btn_DU11_2.Visible = false;
                    btn_DU11_3.Visible = false;
                    break;
                case "23":
                    IframeDU11_1.Src = "http://10.0.2.40/";
                    btn_DU11_1.InnerText = "MÁQUINA 23";
                    btn_DU11_2.Visible = false;
                    btn_DU11_3.Visible = false;
                    break;
                case "24":
                    IframeDU11_1.Src = "http://10.0.2.50/";
                    btn_DU11_1.InnerText = "MÁQUINA 24";
                    btn_DU11_2.Visible = false;
                    btn_DU11_3.Visible = false;
                    break;
                case "25":
                    IframeDU11_1.Src = "../SMARTH_fonts/INTERNOS/NODU11s.PNG";
                    btn_DU11_1.InnerText = "MÁQUINA 25";
                    btn_DU11_2.Visible = false;
                    btn_DU11_3.Visible = false;
                    break;
                case "26":
                    IframeDU11_1.Src = "http://10.0.2.65/";
                    btn_DU11_1.InnerText = "MÁQUINA 26";
                    btn_DU11_2.Visible = false;
                    btn_DU11_3.Visible = false;
                    break;
                case "28":
                    IframeDU11_1.Src = "http://10.0.2.70/";
                    btn_DU11_1.InnerText = "MÁQUINA 28";
                    btn_DU11_2.Visible = false;
                    btn_DU11_3.Visible = false;
                    break;
                case "29":
                    IframeDU11_1.Src = "http://10.0.2.92/";
                    btn_DU11_1.InnerText = "MÁQUINA 29";
                    btn_DU11_2.Visible = false;
                    btn_DU11_3.Visible = false;
                    break;
                case "30":
                    IframeDU11_1.Src = "http://10.0.2.97/";
                    btn_DU11_1.InnerText = "MÁQUINA 30";
                    btn_DU11_2.Visible = false;
                    btn_DU11_3.Visible = false;
                    break;
                case "31":
                    IframeDU11_1.Src = "../SMARTH_fonts/INTERNOS/NODU11s.PNG";
                    btn_DU11_1.InnerText = "MÁQUINA 31";
                    btn_DU11_2.Visible = false;
                    btn_DU11_3.Visible = false;
                    break;
                case "32":
                    IframeDU11_1.Src = "http://10.0.2.107/";
                    btn_DU11_1.InnerText = "MÁQUINA 32";
                    btn_DU11_2.Visible = false;
                    btn_DU11_3.Visible = false;
                    break;
                case "33":
                    IframeDU11_1.Src = "../SMARTH_fonts/INTERNOS/NODU11s.PNG";
                    btn_DU11_1.InnerText = "MÁQUINA 33";
                    btn_DU11_2.Visible = false;
                    btn_DU11_3.Visible = false;
                    break;
                case "34":
                    IframeDU11_1.Src = "../SMARTH_fonts/INTERNOS/NODU11s.PNG";
                    btn_DU11_1.InnerText = "MÁQUINA 34";
                    btn_DU11_2.Visible = false;
                    btn_DU11_3.Visible = false;
                    break;
                case "35":
                    IframeDU11_1.Src = "../SMARTH_fonts/INTERNOS/NODU11s.PNG";
                    btn_DU11_1.InnerText = "MÁQUINA 35";
                    btn_DU11_2.Visible = false;
                    btn_DU11_3.Visible = false;
                    break;
                case "36":
                    IframeDU11_1.Src = "http://10.0.2.130/";
                    btn_DU11_1.InnerText = "MÁQUINA 36";
                    btn_DU11_2.Visible = false;
                    btn_DU11_3.Visible = false;
                    break;
                case "37":
                    IframeDU11_1.Src = "http://10.0.2.140/";
                    btn_DU11_1.InnerText = "MÁQUINA 37";
                    btn_DU11_2.Visible = false;
                    btn_DU11_3.Visible = false;
                    break;
                case "38":
                    IframeDU11_1.Src = "http://10.0.2.146/";
                    btn_DU11_1.InnerText = "MÁQUINA 38";
                    btn_DU11_2.Visible = false;
                    btn_DU11_3.Visible = false;
                    break;
                case "39":
                    IframeDU11_1.Src = "http://10.0.2.145/";
                   
                    btn_DU11_1.InnerText = "MÁQUINA 39";
                    btn_DU11_2.Visible = false;
                    btn_DU11_3.Visible = false;
                    break;
                case "40":
                    IframeDU11_1.Src = "http://10.0.2.150/";
                    btn_DU11_1.InnerText = "MÁQUINA 40";
                    btn_DU11_2.Visible = false;
                    btn_DU11_3.Visible = false;
                    break;
                case "41":
                    IframeDU11_1.Src = "http://10.0.2.155/";
                    btn_DU11_1.InnerText = "MÁQUINA 41";
                    btn_DU11_2.Visible = false;
                    btn_DU11_3.Visible = false;
                    break;
                case "42":
                    IframeDU11_1.Src = "../SMARTH_fonts/INTERNOS/NODU11s.PNG";
                    btn_DU11_1.InnerText = "MÁQUINA 42";
                    btn_DU11_2.Visible = false;
                    btn_DU11_3.Visible = false;
                    break;
                case "43":
                    IframeDU11_1.Src = "../SMARTH_fonts/INTERNOS/NODU11s.PNG";
                    btn_DU11_1.InnerText = "MÁQUINA 43";
                    btn_DU11_2.Visible = false;
                    btn_DU11_3.Visible = false;
                    break;
                case "44":
                    IframeDU11_1.Src = "http://10.0.2.170/";
                    btn_DU11_1.InnerText = "MÁQUINA 44";
                    btn_DU11_2.Visible = false;
                    btn_DU11_3.Visible = false;
                    break;
                case "45":
                    IframeDU11_1.Src = "http://10.0.2.206/";
                    btn_DU11_1.InnerText = "MÁQUINA 45";
                    btn_DU11_2.Visible = false;
                    btn_DU11_3.Visible = false;
                    break;
                case "46":
                    IframeDU11_1.Src = "http://10.0.2.160/";
                    btn_DU11_1.InnerText = "MÁQUINA 46";
                    btn_DU11_2.Visible = false;
                    btn_DU11_3.Visible = false;
                    break;
                case "47":
                    IframeDU11_1.Src = "http://10.0.2.165/";
                    btn_DU11_1.InnerText = "MÁQUINA 47";
                    btn_DU11_2.Visible = false;
                    btn_DU11_3.Visible = false;
                    break;
                case "48":
                    IframeDU11_1.Src = "../SMARTH_fonts/INTERNOS/NODU11s.PNG";
                    btn_DU11_1.InnerText = "MÁQUINA 48";
                    btn_DU11_2.Visible = false;
                    btn_DU11_3.Visible = false;
                    break;
                case "49":
                    IframeDU11_1.Src = "http://10.0.2.185/home.html";
                    btn_DU11_1.InnerText = "MÁQUINA 49";
                    btn_DU11_2.Visible = false;
                    btn_DU11_3.Visible = false;
                    break;
                case "50":
                    IframeDU11_1.Src = "http://10.0.2.190";
                    IframeDU11_2.Src = "http://10.0.2.195";
                    btn_DU11_1.InnerText = "MÁQUINA 50";
                    btn_DU11_2.InnerText = "MONT. WORKTOP";
                    btn_DU11_2.Visible = true;
                    btn_DU11_3.Visible = false;
                    break;
                case "HOUS":
                    IframeDU11_1.Src = "../SMARTH_fonts/INTERNOS/NODU11s.PNG";
                    btn_DU11_1.InnerText = "MONT. HOUSING";
                    btn_DU11_2.Visible = false;
                    btn_DU11_3.Visible = false;
                    break;
                case "FOAM":
                    IframeDU11_1.Src = "../SMARTH_fonts/INTERNOS/NODU11s.PNG";
                    btn_DU11_1.InnerText = "FOAMIZADO";
                    btn_DU11_2.Visible = false;
                    btn_DU11_3.Visible = false;
                    break;
                case "BSH":
                    IframeDU11_1.Src = "http://10.0.2.200/";
                    IframeDU11_2.Src = "http://10.0.2.97/";
                    IframeDU11_3.Src = "http://10.0.2.145/";
                    btn_DU11_1.InnerText = "MONTAJE CUBETA";
                    btn_DU11_2.InnerText = "MÁQUINA 30";
                    btn_DU11_3.InnerText = "MÁQUINA 39";
                    btn_DU11_2.Visible = true;
                    btn_DU11_3.Visible = true;
                    break;
                case "WTOP":
                    IframeDU11_1.Src = "http://10.0.2.195/";
                    IframeDU11_2.Src = "http://10.0.2.190/";
                    btn_DU11_1.InnerText = "MONTAJE WORKTOP";
                    btn_DU11_2.InnerText = "MÁQUINA 50";
                    btn_DU11_2.Visible = true;
                    break;
                default:
                    IframeDU11_1.Src = "../SMARTH_fonts/INTERNOS/NODU11s.PNG";
                    btn_DU11_1.InnerText = "--";
                    btn_DU11_2.Visible = false;
                    btn_DU11_3.Visible = false;
                    break;

            }
        }

        //letura documentación
        /*
        public void AbrirScriptDocumentacion(Object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "VerificarLectura();", true);
        }
        */
        public void CerrarAvisoDocumentacion(Object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ClosePopupDocumentacion();", true);
                Conexion_DOCUMENTAL conexion = new Conexion_DOCUMENTAL();
                if (Trabajadores2.Rows.Count == 0)
                {
                    AvisoSinLog = 1;
                }

                if(Trabajadores2.Rows.Count > 0)
                { 
                conexion.actualizar_validacion_operario(Trabajadores2.Rows[0]["C_CLOCKNO"].ToString(), NumMolde);
                    AvisoSinLog = 0;
                }
                

                //AlertaDOC1.Visible = false;
                
            }
            catch (Exception)
            { }
        }
        public void RedireccionaDocumento(Object sender, EventArgs e)
        {
            try
            {
                Conexion_DOCUMENTAL conexion = new Conexion_DOCUMENTAL();
                HtmlButton button = (HtmlButton)sender;              
                string name = button.ID;
                switch (name)
                {
                    case "DOC1":
                        IframeIncrustados.Src = "http://facts4-srv/thermogestion/DOCUMENTAL/FichaReferencia.aspx?REFERENCIA=" + datosorden.Tables[0].Rows[0]["C_PRODUCT_ID"].ToString();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupIncrustados();", true);
                        break;
                    case "LIB1":
                        IframeIncrustados.Src = "http://facts4-srv/thermogestion/LIBERACIONES/LiberacionSerie.aspx?ORDEN=" + datosorden.Tables[0].Rows[0]["C_ID"].ToString();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupIncrustados();", true);
                        break;
                    case "MUR1":
                        IframeIncrustados.Src = "http://facts4-srv/thermogestion/GP12/GP12HistoricoReferencia.aspx?REFERENCIA=" + datosorden.Tables[0].Rows[0]["C_PRODUCT_ID"].ToString();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupIncrustados();", true);
                        break;
                    case "FAB1":
                        IframeIncrustados.Src = "http://facts4-srv/thermogestion/FichasParametros.aspx?REFERENCIA=" + datosorden.Tables[0].Rows[0]["C_PRODUCT_ID"].ToString() + "&MAQUINA=" + datosorden.Tables[0].Rows[0]["C_MACHINE_ID"].ToString(); ;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupIncrustados();", true);
                        break;

                    case "EST1":
                        DataSet Estructura = conexion.DevuelveEstructuraMaquina(datosorden.Tables[0].Rows[0]["C_MACHINE_ID"].ToString());
                        DataSet EstructuraAgrupada = conexion.DevuelveEstructuraMaquinaAgrupada(datosorden.Tables[0].Rows[0]["C_MACHINE_ID"].ToString());
                        Estructura.Tables[0].DefaultView.RowFilter = "PRODUCTO = '" + datosorden.Tables[0].Rows[0]["C_PRODUCT_ID"].ToString() + "'";
                            DataTable DTEstructura = (Estructura.Tables[0].DefaultView).ToTable();
                            lblEstructuraProducto.InnerText = DTEstructura.Rows[0]["PRODUCTO"].ToString() + " " + DTEstructura.Rows[0]["C_PRODLONGDESCR"].ToString();
                            GridEstructura.DataSource = DTEstructura;
                            GridEstructura.DataBind();
                            GridEstructuraOrden.DataSource = EstructuraAgrupada;
                            GridEstructuraOrden.DataBind();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupEstructura();", true);
                        break;
                    case "POL1":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupExperiencia();", true);
                        break;
                        //PESTAÑA2
                    case "DOC2":
                        IframeIncrustados.Src = "http://facts4-srv/thermogestion/DOCUMENTAL/FichaReferencia.aspx?REFERENCIA=" + datosorden.Tables[0].Rows[1]["C_PRODUCT_ID"].ToString();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupIncrustados();", true);
                        break;
                    case "LIB2":
                        IframeIncrustados.Src = "http://facts4-srv/thermogestion/LIBERACIONES/LiberacionSerie.aspx?ORDEN=" + datosorden.Tables[0].Rows[1]["C_ID"].ToString();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupIncrustados();", true);
                        break;
                    case "MUR2":
                        IframeIncrustados.Src = "http://facts4-srv/thermogestion/GP12/GP12HistoricoReferencia.aspx?REFERENCIA=" + datosorden.Tables[0].Rows[1]["C_PRODUCT_ID"].ToString();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupIncrustados();", true);
                        break;
                    case "FAB2":
                        IframeIncrustados.Src = "http://facts4-srv/thermogestion/FichasParametros.aspx?REFERENCIA=" + datosorden.Tables[0].Rows[1]["C_PRODUCT_ID"].ToString() + "&MAQUINA=" + datosorden.Tables[0].Rows[1]["C_MACHINE_ID"].ToString(); ;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupIncrustados();", true);
                        break;

                    case "EST2":
                        Estructura = conexion.DevuelveEstructuraMaquina(datosorden.Tables[0].Rows[1]["C_MACHINE_ID"].ToString());
                        EstructuraAgrupada = conexion.DevuelveEstructuraMaquinaAgrupada(datosorden.Tables[0].Rows[1]["C_MACHINE_ID"].ToString());
                        Estructura.Tables[0].DefaultView.RowFilter = "PRODUCTO = '" + datosorden.Tables[0].Rows[1]["C_PRODUCT_ID"].ToString() + "'";
                        DTEstructura = (Estructura.Tables[0].DefaultView).ToTable();
                        lblEstructuraProducto.InnerText = DTEstructura.Rows[1]["PRODUCTO"].ToString() + " " + DTEstructura.Rows[1]["C_PRODLONGDESCR"].ToString();
                        GridEstructura.DataSource = DTEstructura;
                        GridEstructura.DataBind();
                        GridEstructuraOrden.DataSource = EstructuraAgrupada;
                        GridEstructuraOrden.DataBind();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupEstructura();", true);
                        break;
                    case "POL2":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupExperiencia();", true);
                        break;

                        //PESTAÑA3
                    case "DOC3":
                        IframeIncrustados.Src = "http://facts4-srv/thermogestion/DOCUMENTAL/FichaReferencia.aspx?REFERENCIA=" + datosorden.Tables[0].Rows[2]["C_PRODUCT_ID"].ToString();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupIncrustados();", true);
                        break;
                    case "LIB3":
                        IframeIncrustados.Src = "http://facts4-srv/thermogestion/LIBERACIONES/LiberacionSerie.aspx?ORDEN=" + datosorden.Tables[0].Rows[2]["C_ID"].ToString();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupIncrustados();", true);
                        break;
                    case "MUR3":
                        IframeIncrustados.Src = "http://facts4-srv/thermogestion/GP12/GP12HistoricoReferencia.aspx?REFERENCIA=" + datosorden.Tables[0].Rows[2]["C_PRODUCT_ID"].ToString();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupIncrustados();", true);
                        break;
                    case "FAB3":
                        IframeIncrustados.Src = "http://facts4-srv/thermogestion/FichasParametros.aspx?REFERENCIA=" + datosorden.Tables[0].Rows[2]["C_PRODUCT_ID"].ToString() + "&MAQUINA=" + datosorden.Tables[0].Rows[2]["C_MACHINE_ID"].ToString(); ;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupIncrustados();", true);
                        break;

                    case "EST3":
                        Estructura = conexion.DevuelveEstructuraMaquina(datosorden.Tables[0].Rows[2]["C_MACHINE_ID"].ToString());
                        EstructuraAgrupada = conexion.DevuelveEstructuraMaquinaAgrupada(datosorden.Tables[0].Rows[2]["C_MACHINE_ID"].ToString());
                        Estructura.Tables[0].DefaultView.RowFilter = "PRODUCTO = '" + datosorden.Tables[0].Rows[2]["C_PRODUCT_ID"].ToString() + "'";
                        DTEstructura = (Estructura.Tables[0].DefaultView).ToTable();
                        lblEstructuraProducto.InnerText = DTEstructura.Rows[2]["PRODUCTO"].ToString() + " " + DTEstructura.Rows[2]["C_PRODLONGDESCR"].ToString();
                        GridEstructura.DataSource = DTEstructura;
                        GridEstructura.DataBind();
                        GridEstructuraOrden.DataSource = EstructuraAgrupada;
                        GridEstructuraOrden.DataBind();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupEstructura();", true);
                        break;
                    case "POL3":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupExperiencia();", true);
                        break;

                    //PESTAÑA3
                    case "DOC4":
                        IframeIncrustados.Src = "http://facts4-srv/thermogestion/DOCUMENTAL/FichaReferencia.aspx?REFERENCIA=" + datosorden.Tables[0].Rows[3]["C_PRODUCT_ID"].ToString();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupIncrustados();", true);
                        break;
                    case "LIB4":
                        IframeIncrustados.Src = "http://facts4-srv/thermogestion/LIBERACIONES/LiberacionSerie.aspx?ORDEN=" + datosorden.Tables[0].Rows[3]["C_ID"].ToString();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupIncrustados();", true);
                        break;
                    case "MUR4":
                        IframeIncrustados.Src = "http://facts4-srv/thermogestion/GP12/GP12HistoricoReferencia.aspx?REFERENCIA=" + datosorden.Tables[0].Rows[3]["C_PRODUCT_ID"].ToString();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupIncrustados();", true);
                        break;
                    case "FAB4":
                        IframeIncrustados.Src = "http://facts4-srv/thermogestion/FichasParametros.aspx?REFERENCIA=" + datosorden.Tables[0].Rows[3]["C_PRODUCT_ID"].ToString() + "&MAQUINA=" + datosorden.Tables[0].Rows[3]["C_MACHINE_ID"].ToString(); ;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupIncrustados();", true);
                        break;

                    case "EST4":
                        Estructura = conexion.DevuelveEstructuraMaquina(datosorden.Tables[0].Rows[3]["C_MACHINE_ID"].ToString());
                        EstructuraAgrupada = conexion.DevuelveEstructuraMaquinaAgrupada(datosorden.Tables[0].Rows[3]["C_MACHINE_ID"].ToString());
                        Estructura.Tables[0].DefaultView.RowFilter = "PRODUCTO = '" + datosorden.Tables[0].Rows[3]["C_PRODUCT_ID"].ToString() + "'";
                        DTEstructura = (Estructura.Tables[0].DefaultView).ToTable();
                        lblEstructuraProducto.InnerText = DTEstructura.Rows[3]["PRODUCTO"].ToString() + " " + DTEstructura.Rows[3]["C_PRODLONGDESCR"].ToString();
                        GridEstructura.DataSource = DTEstructura;
                        GridEstructura.DataBind();
                        GridEstructuraOrden.DataSource = EstructuraAgrupada;
                        GridEstructuraOrden.DataBind();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupEstructura();", true);
                        break;
                    case "POL4":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupExperiencia();", true);
                        break;

                }

            }
            catch (Exception)
            {
            }
        }

        //feedback documental 
        //Pasar a AJAX
        public void InsertarFeedbackDocumental(Object sender, EventArgs e)
        {
            try
            {
                
                /*
                if (tbDenunciaError.Text != "" && NumMolde != "")
                {
                    string OP1 = "0";
                    string OP2 = "0";
                    if (Operario1Numero.Text != "---")
                    {
                        OP1 = Operario1Numero.Text;
                    }
                    if (Operario2Numero.Text != "---")
                    {
                        OP2 = Operario2Numero.Text;
                    }

                    Conexion_DOCUMENTAL conexion = new Conexion_DOCUMENTAL();
                    conexion.insertar_feedback_operario(OP1, OP2, NumMolde, tbDenunciaError.Text);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "guardado_feedback_OK();", true);
                }
            */
            }
            catch (Exception)
            { }
        }


        protected void lanzaPostback(object sender, EventArgs e)
        {
            Response.Redirect(url: "http://facts4-srv/thermogestion/DOCUMENTAL/DocumentosPlanta.aspx?MAQUINA=" + Maquina);
        }


        [WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static bool CerrarAvisoDocumentacionWeb(string Avisolog)
        {
            try
            {
                Avisolog = "";
                Conexion_DOCUMENTAL conexion = new Conexion_DOCUMENTAL();
                if (Trabajadores2.Rows.Count == 0)
                {
                    AvisoSinLog = 1;
                }

                if (Trabajadores2.Rows.Count > 0)
                {
                    conexion.actualizar_validacion_operario(Trabajadores2.Rows[0]["C_CLOCKNO"].ToString(), datosorden.Tables[0].Rows[0]["C_TOOL_ID"].ToString());
                    AvisoSinLog = 0;
                }

                return true;


                //AlertaDOC1.Visible = false;

            }
            catch (Exception)
            {
                return false;
            }
        }

        [WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static bool InsertarFeedbackDocumentalWeb(string operario1, string operario2, string Referencia, string Molde, string InputFeedback)
        {
            try
            {                
                  if (InputFeedback != "" && Referencia != "---")
                  {
                      string OP1 = "0";
                      string OP2 = "0";
                      if (operario1 != "---")
                      {
                          OP1 = operario1;
                      }
                      if (operario2 != "---")
                      {
                          OP2 = operario2;
                      }

                      Conexion_DOCUMENTAL conexion = new Conexion_DOCUMENTAL();
                      conexion.Insertar_feedback_operario_V2(OP1, OP2, Molde, Referencia, InputFeedback);
                  }
                
                return true;

            }
            catch (Exception ex)
            {
               
                return false;
            }
        }

    }

}
