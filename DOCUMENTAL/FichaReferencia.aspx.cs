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
using System.Net.Mail;
using System.Net;
using System.Data.SqlClient;
using System.Text;
using System.IO;

namespace ThermoWeb.DOCUMENTAL
{
    public partial class FichaReferencia : System.Web.UI.Page
    {
        private static DataSet ds_RefXMolde = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GestionBotones(0);
                CargarFiltros();
                tbObservacionesCarga.Enabled = false;
                tbObservacionesCargaBMS.Enabled = false;
                if (Request.QueryString["REFERENCIA"] != null)
                {
                    tbReferencia.Value = Request.QueryString["REFERENCIA"];
                    CargarDatos(null, e);
                }

            }

        }

        protected void CargarFiltros()
        {
            try
            {
                Conexion_SMARTH SHconexion = new Conexion_SMARTH();
                Conexion_DOCUMENTAL conexion = new Conexion_DOCUMENTAL();

                DataTable ListaProductos = SHconexion.Devuelve_listado_PRODUCTOS();
                {
                    for (int i = 0; i <= ListaProductos.Rows.Count - 1; i++)
                    {
                        DatalistReferencias.InnerHtml = DatalistReferencias.InnerHtml + System.Environment.NewLine +
                            String.Format("<option value='{0}'>", ListaProductos.Rows[i][0]);
                    }
                }

                //Relleno tipo de documentos
                DataTable TipoDocs = conexion.Devuelve_Tipos_Documentos_SMARTH();
                DropTipoDOCAUX.Items.Add("-");
                foreach (DataRow row in TipoDocs.Rows) { DropTipoDOCAUX.Items.Add(row["TipoDocumento"].ToString()); }
                DropTipoDOCAUX.ClearSelection();
                DropTipoDOCAUX.SelectedValue = "-";


                //Relleno responsables
                DataSet Personal = SHconexion.Devuelve_mandos_intermedios_SMARTH();
                dropAprobadoDOCAUX.Items.Clear();
                foreach (DataRow row in Personal.Tables[0].Rows)
                {
                    dropAprobadoDOCAUX.Items.Add(row["Nombre"].ToString());
                }

            }
            catch (Exception ex)
            { }
        }

        public void CargarDatos(object sender, EventArgs e)
        {
            try
            {
                ResetearCajasCheck();
                ResetearFrames();

                btnMaquinaVista.Disabled = false;
                btnGP12Vista.Disabled = false;
                btnmolde.Disabled = false;
                borrarmolde.Visible = true;
                borrarref.Visible = true;
                btnVerEstructura.Visible = true;

                string[] RecorteReferencia = tbReferencia.Value.Split(new char[] { '¬' });

                Conexion_DOCUMENTAL conexion = new Conexion_DOCUMENTAL();
                DataSet ds = conexion.Devuelve_dataset_filtroreferenciasSMARTH(RecorteReferencia[0].Trim(), "", "", "", "");
                tbReferenciaCarga.Text = ds.Tables[0].Rows[0]["REF"].ToString();
                tbDescripcionCarga.Text = ds.Tables[0].Rows[0]["Descripcion"].ToString();
                tbMolde.InnerText = ds.Tables[0].Rows[0]["Molde"].ToString();
                tbObservacionesCarga.Text = ds.Tables[0].Rows[0]["Observaciones"].ToString();
                tbObservacionesCarga.Enabled = true;


                DataSet BMS = conexion.DevuelveRemarkProducto(RecorteReferencia[0].Trim());
                tbObservacionesCargaBMS.Text = BMS.Tables[0].Rows[0]["C_REMARKS"].ToString();
                tbObservacionesCargaBMS.Enabled = true;

                DataTable Vinculados = conexion.Devuelve_Documentos_Vinculados_SMARTH(ds.Tables[0].Rows[0]["REF"].ToString(), ds.Tables[0].Rows[0]["Molde"].ToString());
                GridVinculados.DataSource = Vinculados;
                GridVinculados.DataBind();

                
                string PautaControl = ds.Tables[0].Rows[0]["PautaControl"].ToString();
                framepautadecontrol.Attributes.Add("src", PautaControl);
                string EmbalajeAlternativo = ds.Tables[0].Rows[0]["PautaRecepcion2"].ToString();
                frameEmbalajeAlternativo.Attributes.Add("src", EmbalajeAlternativo);
                string OperacionEstandar = ds.Tables[0].Rows[0]["OperacionEstandar"].ToString();
                frameoperacionestandar.Attributes.Add("src", OperacionEstandar);
                
                string Defoteca = ds.Tables[0].Rows[0]["Defoteca"].ToString();
                framedefoteca.Attributes.Add("src", Defoteca);
                string Embalaje = ds.Tables[0].Rows[0]["Embalaje"].ToString();
                frameembalaje.Attributes.Add("src", Embalaje);
                string Gp12 = ds.Tables[0].Rows[0]["Gp12"].ToString();
                framemurodecalidad.Attributes.Add("src", Gp12);


                string ImagenPieza = ds.Tables[0].Rows[0]["ImagenPieza"].ToString();
                if (ImagenPieza != "") { hyperlink9.ImageUrl = ImagenPieza; }
                else { hyperlink9.ImageUrl = "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg"; }

                ds_RefXMolde = conexion.devuelve_moldesXreferencia(tbMolde.InnerText.ToString());
                dgv_RefXMolde.DataSource = ds_RefXMolde;
                dgv_RefXMolde.DataBind();
                GestionBotones(1);
                

            }
            catch (Exception)
            {
                GridVinculados.DataSource = null;
                GridVinculados.DataBind();

                dgv_RefXMolde.DataSource = null;
                dgv_RefXMolde.DataBind();

                hyperlink9.ImageUrl = "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg";
                btnMaquinaVista.Disabled = true;
                btnGP12Vista.Disabled = true;
                btnmolde.Disabled = true;
                borrarmolde.Visible = false;
                borrarref.Visible = false;
                ResetearCajasCheck();
                ResetearFrames();
                GestionBotones(0);
            }
        }

        //Gestionar estado y visibilidad de botones y checks
        public void ResetearCajasCheck()
        {
            try
            {
                CHECKIMG.Checked = false;             
                CHECKPREVPAC.Checked = false;
                CHECKPREVEMBALT.Checked = false;
                CHECKPREVHOS.Checked = false;
                CHECKPREVGP12.Checked = false;          
                CHECKPREVDEF.Checked = false;
                CHECKPREVEMB.Checked = false;
            }
            catch (Exception) { }
        }

        public void ResetearFrames()
        {
            try
            {
                

                framepautadecontrol.Attributes.Add("src", "");                              
                frameEmbalajeAlternativo.Attributes.Add("src", "");
                frameoperacionestandar.Attributes.Add("src", "");
                
                framedefoteca.Attributes.Add("src", "");
                frameembalaje.Attributes.Add("src", "");
                framemurodecalidad.Attributes.Add("src", "");

            }
            catch (Exception) { }
        }

        public void GestionBotones(int accion)
        {
            if (accion == 0)
            {

                btnVerEstructura.Visible = false;
                btnOtrosDocs.Visible = false;
                btnsubir0.Visible = false;
                btnsubir5.Visible = false;
                btnsubir7.Visible = false;
                btnsubir9.Visible = false;
                btnsubir11.Visible = false;
                btnsubir15.Visible = false;
                btnsubir17.Visible = false;

                btnver5.Visible = false;
                btnver7.Visible = false;
                btnver9.Visible = false;
                btnver11.Visible = false;
                btnver15.Visible = false;
                btnver17.Visible = false;
     
                btnborrar5.Visible = false;
                btnborrar7.Visible = false;
                btnborrar9.Visible = false;
                btnborrar11.Visible = false;      
                btnborrar15.Visible = false;
                btnborrar17.Visible = false; 
                FileUpload0.Visible = false;
                FileUpload4.Visible = false;
                FileUpload6.Visible = false;
                FileUpload8.Visible = false;
                FileUpload10.Visible = false;
                FileUpload14.Visible = false;
                FileUpload16.Visible = false;

            }
            else
            {
                btnOtrosDocs.Visible = true;
                btnsubir0.Visible = true;
                btnsubir5.Visible = true;
                btnsubir7.Visible = true;
                btnsubir9.Visible = true;
                btnsubir11.Visible = true;
                btnsubir15.Visible = true;
                btnsubir17.Visible = true;

                btnver5.Visible = true;
                btnver7.Visible = true;
                btnver9.Visible = true;
                btnver11.Visible = true;
                btnver15.Visible = true;
                btnver17.Visible = true;


                btnborrar5.Visible = true;
                btnborrar7.Visible = true;
                btnborrar9.Visible = true;
                btnborrar11.Visible = true;
                btnborrar15.Visible = true;
                btnborrar17.Visible = true;

                FileUpload0.Visible = true;
                FileUpload4.Visible = true;
                FileUpload6.Visible = true;
                FileUpload8.Visible = true;
                FileUpload10.Visible = true;
                FileUpload14.Visible = true;
                FileUpload16.Visible = true;

            }

        }

        public void VaciaFramesSeleccionado()
        {
            try
            {
                if (CHECKPREVPAC.Checked == true)
                {
                    framepautadecontrol.Attributes.Add("src", "");
                }
                if (CHECKPREVEMBALT.Checked == true)
                {
                    frameEmbalajeAlternativo.Attributes.Add("src", "");
                }
                if (CHECKPREVHOS.Checked == true)
                {
                    frameoperacionestandar.Attributes.Add("src", "");
                }
                if (CHECKPREVGP12.Checked == true)
                {
                    framemurodecalidad.Attributes.Add("src", "");
                }
                if (CHECKPREVDEF.Checked == true)
                {
                    framedefoteca.Attributes.Add("src", "");
                }
                if (CHECKPREVEMB.Checked == true)
                {
                    frameembalaje.Attributes.Add("src", "");
                }
                

            }
            catch (Exception)
            { }
        }

        //VISUALIZADOR DE DOCUMENTOS
        protected void OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Redirect")
                {
                    DocVinculado.Attributes.Add("src", e.CommandArgument.ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopDocVinculados();", true);
                }
                if (e.CommandName == "Eliminar")

                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "BorraDOC();", true);
                    Conexion_DOCUMENTAL conexion = new Conexion_DOCUMENTAL();
                    conexion.Eliminar_documentos_V3(e.CommandArgument.ToString());
                    DataTable Vinculados = conexion.Devuelve_Documentos_Vinculados_SMARTH(tbReferenciaCarga.Text, tbMolde.InnerText);
                    GridVinculados.DataSource = Vinculados;
                    GridVinculados.DataBind();
                }
            }
            catch (Exception ex)
            { }

        }

        protected void OnRowCommandRedi(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                tbReferencia.Value = e.CommandArgument.ToString();
                CargarDatos(null, null);
            }
            catch (Exception ex)
            { }

        }

        public void Redirecciones(object sender, EventArgs e)
        {
            try
            {
                HtmlButton button = (HtmlButton)sender;
                string NombreBoton = button.ID.ToString();
                string TipoBoton = NombreBoton.Substring(0, 6);
                string urlframe = "";

                switch (TipoBoton)
                {
                    case "btnver":
                        switch (NombreBoton)
                        {
                            case "btnver5":
                                urlframe = framepautadecontrol.Attributes["src"];
                                if (urlframe != "") { Response.Redirect(url: urlframe); }
                                break;
                            case "btnver7":
                                urlframe = frameEmbalajeAlternativo.Attributes["src"];
                                if (urlframe != "") { Response.Redirect(url: urlframe); }
                                break;
                            case "btnver9":
                                urlframe = frameoperacionestandar.Attributes["src"];
                                if (urlframe != "") { Response.Redirect(url: urlframe); }
                                break;
                            case "btnver11":
                                urlframe = framemurodecalidad.Attributes["src"];
                                if (urlframe != "") { Response.Redirect(url: urlframe); }
                                break;

                            case "btnver15":
                                urlframe = framedefoteca.Attributes["src"];
                                if (urlframe != "") { Response.Redirect(url: urlframe); }
                                break;
                            case "btnver17":
                                urlframe = frameembalaje.Attributes["src"];
                                if (urlframe != "") { Response.Redirect(url: urlframe); }
                                break;
                        }
                        break;
                    case "btnGP1":
                        Response.Redirect(url: "DocumentosGP12.aspx?REFERENCIA=" + tbReferenciaCarga.Text);
                        break;
                    case "btnMaq":
                        Response.Redirect(url: "DocumentosPlanta.aspx?REFERENCIA=" + tbReferenciaCarga.Text);
                        break;
                }

            }
            catch (Exception ex)
            { }

        }

        //Experimental
        public void Imprimiretiqueta(object sender, EventArgs e)

        {
            string cantidad = "250";
            string referencia = "62095008";
            string descripcion = "TIP/TEMP-ABCPART";
            string lote = "65432";
            string caja = "65432001";
            string pedido = "170123";
            string refcliente = "ASM815108-1";
            string fecha = "20210113";
            string hora = "13:09";
            string cliente = "PLASTICOS ABC";
            string unloadingpoint = "1752";
            string gp12 = "1752";
            string plano = "007";

            byte[] zplABC = Encoding.UTF8.GetBytes("^XA^[L,2^FS^XZ^XA~TA000~JSN^MUD^PON^PMN^LH0,0^JMA^PR8,8~SD15^JUS^LRN^CI0^MMT^LS0^LT0^FWR^FPH^MTD^MNM^LH2,25^FS^PW815^MNM^ML1817" +
                    "^FXLineasverticales^FO600,515^GB195,1,3^FS^FO600,915^GB195,1,3^FS^FO455,555^GB145,1,3^FS^FO455,1065^GB145,1,3^FS^FO310,730^GB145,1,3^FS^FO25,1130^GB290,1,3^FS" +
                    "^FXLineashorizontales^FO690,515^GB1,400,3^FS^FO600,25^GB1,1600,3^FS^FO455,25^GB1,1600,3^FS^FO310,25^GB1,1600,3^FS^FO200,1130^GB1,490,3^FS^FO100,1130^GB1,490,3^FS" +
                    "^FXDescripcionesdecampo^FO760,35^A0R30,30^FDFROM:^FS^FO760,525^A0R30,30^FDTO:^FS^FO655,525^A0R30,30^FDDLOC:^FS^FO565,35^A0R30,30^FDCUSTOMERP.O.(K):^FS^FO565,565^A0R30,30^FDSUPPLIERPART:^FS" +
                    "^FO565,1075^A0R30,30^FDQUANTITY^FS^FO420,35^A0R30,30^FDCUSTOMERPART:^FS^FO420,740^A0R30,30^FDDESCRIPTION:^FS^FO275,35^A0R30,30^FDLICENSEPLATE(1J):^FS^FO275,1140^A0R30,30^FDBATCHNO:^FS" +
                    "^FO275,1335^A0R30,30^FDBOXNO:^FS^FO165,1140^A0R30,30^FDDATE:^FS^FO165,1400^A0R30,30^FDHOUR:^FS^FO65,1140^A0R30,30^FDOP:^FS^FXCamposfijos^FO715,40^A0R40,40^FDTHERMOLYMPICS.L.^FS" +
                    "^FO680,45^A0R35,35^FDCalleAlemaniaS/N^FS^FO645,45^A0R35,35^FD50180,Utebo^FS^FO610,45^A0R35,35^FDZaragoza^FS" +
                    "^FXCamposvariables^FO715,515^A0R35,35^FB400,1,,C^FD" + cliente + "^FS" +
                    "^FO600,515^A0R65,65^FB400,1,,C^FD^FS^FO455,1120^A0R100,100^FD" + cantidad + "^FS^FO465,585^A0R80,80^FD" + referencia + "^FS^FO455,310^A0R45,45^FD" + pedido + "^FS^FO330,55^A0R65,65^FD" + refcliente + "^FS" +
                    "^FO330,760^A0R65,65^FD" + descripcion + "^FS^FO55,55^A0R65,65^FDUN476925631" + caja + "^FS^FO205,1160^A0R65,65^FD" + lote + "^FS^FO215,1370^A0R50,50^FD" + caja + "^FS^FO110,1160^A0R50,50^FDD" + fecha + "^FS" +
                    "^FO110,1410^A0R50,50^FD" + hora + "^FS^FO15,1160^A0R50,50^FD^FS^FO10,1250^A0R65,65^FB400,1,,C^FD.^FS^FXCodigosdebarra^FO140,55^BCR,130,N,N,N,N^FD>:1JUN476925631" + caja + "^FS^FO510,310^BCR,80,N,N,N,N^FD>:K" + pedido + "^FS" +
                    "^FO615,1000^B7R,4,8,0,N,N^FD[)>E2*06|SUN476925631" + caja + "|P" + refcliente + "|N" + lote + "|Q" + cantidad + "E2*11^FS^XZ");

            //byte[] zpl = Encoding.UTF8.GetBytes("^xa^cfa,50^fo100,100^fdHello World^fs^xz");
            byte[] zpl = Encoding.UTF8.GetBytes("^XA^[L,0^FS^XZ^XA~TA000~JSN^MUD^PON^PMN^LH0,0^JMA^PR8,8~SD15^JUS^LRN^CI0^MMT^LS0^LT0^FWR^FPH^MTD^PW559^MNM^ML1817^BY2,3^FXHorizontalLines^FS^FXHorizontalLine^FS^FO507,0^GB0,1615,2^FS^FO408,0^GB0,1615,2^FS^FO234,0^GB0,1615,2^FS" +
                    "^FXHorizontalLine^FS^FO348,961^GB0,254,2^FS^FO291,961^GB0,254,2^FS^FO174,807^GB0,807,2^FS^FO117,807^GB0,807,2^FS^FO60,807^GB0,807,2^FS^FXVerticalLines^FS^FXVerticalLine^FS^FO507,538^GB53,0,2^FS^FO408,807^GB98,0,2^FS" +
                    "^FO0,807^GB234,0,2^FS^FO174,961^GB234,0,2^FS^FO507,999^GB53,0,2^FS^FO60,1215^GB57,0,2^FS^FO234,1215^GB174,0,2^FS^FXVerticalLine^FS^FXFixedTexts^FS^FO539,12^AMR19,19^FB523,1,,L^FDSHIPTO^FS^FO467,12^AMR19,19^FB784,2,,L^FDDOCUMENTNO^FS" +
                    "^FO368,12^AMR19,19^FB938,2,,L^FDPARTNUMBER(P)^FS^FO213,12^AMR19,19^FB784,1,,L^FDLABELNO(S)^FS^FO520,542^AMR19,19^FB461,2,,L^FDUNLOADINGPOINT^FS^FO467,811^AMR19,19^FB784,2,,L^FDDESTINATIONPOINT^FS^FO213,811^AMR19,19^FB231,1,,L^FDNOOFBOXES^FS" +
                    "^FO153,811^AMR19,19^FB784,1,,L^FDLOGISTICREFERENCE(30S)^FS^FO96,811^AMR19,19^FB308,1,,L^FDBATCHNO(H)^FS^FO4,811^AMR38,38^FB77,1,,C^FDOP:^FS^FO539,1003^AMR19,19^FB600,1,,L^FDSHIPFROM^FS^FO387,965^AMR19,19^FB246,1,,L^FDNETWEIGHT^FS" +
                    "^FO327,965^AMR19,19^FB246,1,,L^FDGROSSWEIGHT^FS^FO270,965^AMR19,19^FB246,1,,L^FDDATE^FS^FO213,965^AMR19,19^FB630,1,,L^FDDESCRIPTION^FS^FO77,1219^AMR19,19^FB477,2,,L^FDREV.NO^FS^FO387,1219^AMR19,19^FB384,1,,L^FDQUANTITY(Q)^FS" +
                    "^FO7,1538^AMR15,15^FB46,1,,C^FDETI9^FS" +
                    "^FXVariabletexts^FS^FO503,12^AMR38,38^FB523,1,,C^FD" + cliente + "^FS^FO503,542^AMR38,38^FB454,1,,C^FD" + unloadingpoint + "^FS^FO503,1003^AMR38,38^FB592,1,,C^FDTHERMOLYMPICSL^FS^FO434,12^AMR46,46^FB784,1,,R^FD.^FS" +
                    "^FO434,811^AMR46,46^FB784,1,,C^FD" + gp12 + "^FS^FO267,49^AMR46,46^BCR,68,N,N,N,N^FD>:P" + refcliente + "^FS^FO206,412^AMR168,168^FB392,1,,C^FD.^FS^FO351,984^AMR38,38^FB231,1,,C^FD0.0KGM^FS^FO291,984^AMR38,38^FB231,1,,C^FD0.0KGM^FS^FO233,984^AMR46,46^FB231,1,,C^FDD" + fecha + "^FS" +
                    "^FO293,1215^AMR91,91^FB400,1,,C^FD" + cantidad + "^FS^FO242,1269^BCR,45,N,N,N,N^FD>:Q" + cantidad + "^FS^FO45,115^AMR38,38^BCR,98,N,N,N,N^FD>:S" + caja + "^FS^FO177,807^AMR38,38^FB154,1,,C^FD1^FS^FO169,961^AMR46,46^FB650,1,,C^FD" + descripcion + "^FS^FO117,807^AMR38,38^FB308,1,,C^FD" + referencia + "^FS" +
                    "^FO128,1134^BCR,38,N,N,N,N^FD>:30S" + referencia + "^FS^FO60,811^AMR38,38^FB115,1,,C^FD" + lote + "^FS^FO60,1222^AMR38,38^FB396,1,,C^FD" + plano + "^FS^FO4,884^AMR38,38^FB246,1,,C^FD^FS^FO4,1246^AMR38,38^FB154,1,,C^FD" + hora + "^FS^FO19,577^BXR,4,200,48,48^FD[]>_1E06_1D12PGTL3_1D9K01_1D3LTHERMOLYMPICSL_1D4LSPAIN_1D8V._1D2L1752_1DV0062056630_1DQ250_1D3QPC_1D2Q0_1DP" + referencia + "_1D1JUN00" + lote + "" + caja + "_1DB._1D2P" + plano + "_1E_04^FS" +
                    "^FO4,1130^AMR38,38^FB154,1,,C^FDHOUR:^FS^FO350,123^AMR46,46^FB477,1,,L^FD" + refcliente + "^FS^FO159,115^AMR38,38^FB692,1,,L^FD" + caja + "^FS^FO72,930^BCR,38,N,N,N,N^FD>:H" + lote + "^FS^XZ");

            // adjust print density (8dpmm), label width (4 inches), label height (6 inches), and label index (0) as necessary
            var request = (HttpWebRequest)WebRequest.Create("http://api.labelary.com/v1/printers/8dpmm/labels/4.9x8.3/0/");
            request.Method = "POST";
            //request.Accept = "application/pdf"; // omit this line to get PNG images back
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = zpl.Length;

            var requestStream = request.GetRequestStream();
            requestStream.Write(zpl, 0, zpl.Length);
            requestStream.Close();

            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                var responseStream = response.GetResponseStream();
                //var fileStream = File.Create(@"\\FACTS4-SRV\Fichas parametros\\label.png"); // change file name for PNG images
                var fileStream = File.Create(@"\\FACTS4-SRV\\inetpub_thermoweb\\Imagenes\\GP12\\label.png");

                responseStream.CopyTo(fileStream);
                responseStream.Close();
                fileStream.Close();
                string url = "http://facts4-srv/thermogestion/Imagenes/GP12/label.png";
                Response.Write("<script language='javascript'> window.open('" + url + "', 'window','HEIGHT=600,WIDTH=820,top=50,left=50,toolbar=yes,scrollbars=yes,resizable=yes');</script>");

            }
            catch (WebException ex)
            {
                Console.WriteLine("Error: {0}", ex.Status);
            }
        }

        //Gestión de documentos

        public void AbrirModalSubirDocs(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupDOCAUX();", true);
        }

        public void Insertar_documento(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload4.HasFile)
                    SaveFile(FileUpload4.PostedFile, 4);
                if (FileUpload6.HasFile)
                    SaveFile(FileUpload6.PostedFile, 6);
                if (FileUpload8.HasFile)
                    SaveFile(FileUpload8.PostedFile, 8);
                if (FileUpload10.HasFile)
                    SaveFile(FileUpload10.PostedFile, 10);
                if (FileUpload14.HasFile)
                    SaveFile(FileUpload14.PostedFile, 14);
                if (FileUpload16.HasFile)
                    SaveFile(FileUpload16.PostedFile, 16);
                if (FileUpload0.HasFile)
                    SaveFile(FileUpload0.PostedFile, 100);
            }
            catch (Exception)
            {

            }
        }

        public void BorrarDocumento(object sender, EventArgs e)
        {
            try
            {
                
                string test = sender.GetType().ToString();
                string name = "";
                if (test == "System.Web.UI.WebControls.Button")
                {
                    Button button = (Button)sender;
                    name = button.ID;
                }
                else
                {
                    HtmlButton button = (HtmlButton)sender;
                    name = button.ID;
                }

                
                if (name != "borrarmolde" && name != "borrarref")
                {
                    switch (name)
                    {
                        case "btnborrar5":
                            framepautadecontrol.Attributes.Add("src", "");
                            break;
                        case "btnborrar7":
                            frameEmbalajeAlternativo.Attributes.Add("src", "");
                            break;
                        case "btnborrar9":
                            frameoperacionestandar.Attributes.Add("src", "");
                            break;
                        case "btnborrar11":
                            framemurodecalidad.Attributes.Add("src", "");
                            break;
                        case "btnborrar15":
                            framedefoteca.Attributes.Add("src", "");
                            break;
                        case "btnborrar17":
                            frameembalaje.Attributes.Add("src", "");

                            break;
                        default:break;

                    }
                    InsertarDocumentosReferenciaMoldeBD(null, null);
                }
                else
                {
                    switch (name)
                    {
                        case "borrarmolde":
                            VaciaFramesSeleccionado();
                            InsertarDocumentosMolde(null, null);
                            break;

                        case "borrarref":
                            VaciaFramesSeleccionado();
                            InsertarDocumentosReferenciaMoldeBD(null, null);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
  
        private void SaveFile(HttpPostedFile file, int num_img)
        {
            try
            {
                // Specify the path to save the uploaded file to.
               
                string savePath = @"C:\inetpub_thermoweb\SMARTH_docs\DOCUMENTAL\" + tbMolde.InnerText + "\\"; // Your code goes here
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }
                // Get the name of the file to upload.
                //string fileName = FileUpload1.FileName;
                string fileName = "";
                switch (num_img)
                {

                    case 4:
                        string extension4 = System.IO.Path.GetExtension(FileUpload4.PostedFile.FileName);
                        fileName = tbMolde.InnerText + "_" + tbReferenciaCarga.Text + "_" + "PAUTACONTROL" + DateTime.Now.ToString("yyyy-MM-dd_HHmmss") + extension4;
                        break;                  
                    case 6:
                        string extension6 = System.IO.Path.GetExtension(FileUpload6.PostedFile.FileName);
                        fileName = tbMolde.InnerText + "_" + tbReferenciaCarga.Text + "_" + "EMBALTERNATIVO" + DateTime.Now.ToString("yyyy-MM-dd_HHmmss") + extension6;
                        break;                 
                    case 8:
                        string extension8 = System.IO.Path.GetExtension(FileUpload8.PostedFile.FileName);
                        fileName = tbMolde.InnerText + "_" + tbReferenciaCarga.Text + "_" + "HOS" + DateTime.Now.ToString("yyyy-MM-dd_HHmmss") + extension8;
                        break;                    
                    case 10:
                        string extension10 = System.IO.Path.GetExtension(FileUpload10.PostedFile.FileName);
                        fileName = tbMolde.InnerText + "_" + tbReferenciaCarga.Text + "_" + "GP12" + DateTime.Now.ToString("yyyy-MM-dd_HHmmss") + extension10;
                        break;
                    case 14:
                        string extension14 = System.IO.Path.GetExtension(FileUpload14.PostedFile.FileName);
                        fileName = tbMolde.InnerText + "_" + tbReferenciaCarga.Text + "_" + "DEF" + DateTime.Now.ToString("yyyy-MM-dd_HHmmss") + extension14;
                        break;                  
                    case 16:
                        string extension16 = System.IO.Path.GetExtension(FileUpload16.PostedFile.FileName);
                        fileName = tbMolde.InnerText + "_" + tbReferenciaCarga.Text + "_" + "EMBALAJE" + DateTime.Now.ToString("yyyy-MM-dd_HHmmss") + extension16;
                        break;
                    case 100:
                        string extension100 = System.IO.Path.GetExtension(FileUpload0.PostedFile.FileName);
                        fileName = tbMolde.InnerText + "_" + tbReferenciaCarga.Text + "_" + "IMG" + DateTime.Now.ToString("yyyy-MM-dd_HHmmss") + extension100;
                        break;
                }

                savePath += fileName;

                // Call the SaveAs method to save the uploaded
                // file to the specified directory.
                switch (num_img)
                {                   
                    case 4:
                        FileUpload4.SaveAs(savePath);
                        framepautadecontrol.Attributes.Add("src", "..\\SMARTH_docs\\DOCUMENTAL\\" + tbMolde.InnerText + "\\" + fileName);
                        txtPautaControl.Text = ", Pauta de Control";
                        break;
                   
                    case 6:
                        FileUpload6.SaveAs(savePath);
                        frameEmbalajeAlternativo.Attributes.Add("src", "..\\SMARTH_docs\\DOCUMENTAL\\" + tbMolde.InnerText + "\\" + fileName);
                        PreviewEmbalajeAlternativo.Visible = true;
                      
                        break;

                    case 8:
                        FileUpload8.SaveAs(savePath);
                        frameoperacionestandar.Attributes.Add("src", "..\\SMARTH_docs\\DOCUMENTAL\\" + tbMolde.InnerText + "\\" + fileName);
                        txtOperacionEstandar.Text = ", Hoja de Operación Estándar";
                        PreviewOperacionEstandar.Visible = true;
                  
                        break;

                    case 10:
                        FileUpload10.SaveAs(savePath);
                        framemurodecalidad.Attributes.Add("src", "..\\SMARTH_docs\\DOCUMENTAL\\" + tbMolde.InnerText + "\\" + fileName);
                        PreviewGP12.Visible = true;
                        break;                    
                    case 14:
                        FileUpload14.SaveAs(savePath);
                        framedefoteca.Attributes.Add("src", "..\\SMARTH_docs\\DOCUMENTAL\\" + tbMolde.InnerText + "\\" + fileName);
                        txtDefoteca.Text = ", Defoteca";
                        PreviewDefoteca.Visible = true;
                        break;
                   
                    case 16:
                        FileUpload16.SaveAs(savePath);
                        frameembalaje.Attributes.Add("src", "..\\SMARTH_docs\\DOCUMENTAL\\" + tbMolde.InnerText + "\\" + fileName);
                        txtEmbalaje.Text = ", Pauta de embalaje";
                        PreviewEmbalaje.Visible = true;                        
                        break;
                                    
                    case 100:
                        FileUpload0.SaveAs(savePath);
                        hyperlink9.ImageUrl = "..\\SMARTH_docs\\DOCUMENTAL\\" + tbMolde.InnerText + "\\" + fileName;
                        break;
                    default: break;
                }
            }
            catch (Exception)
            {
            }
            InsertarDocumentosReferenciaMoldeBD(null, null);
        }

        public void InsertarDocumentosReferenciaMoldeBD(object sender, EventArgs e)
        {
            try
            {

                string LINKPAUTACONTROL = "";
                if (framepautadecontrol.Attributes["src"] != "")
                {
                    LINKPAUTACONTROL = framepautadecontrol.Attributes["src"];
                }
                string LINKEMBALTERNATIVO = "";
                if (frameEmbalajeAlternativo.Attributes["src"] != "")
                {
                    LINKEMBALTERNATIVO = frameEmbalajeAlternativo.Attributes["src"];
                }
                string LINKHOS = "";
                if (frameoperacionestandar.Attributes["src"] != "")
                {
                    LINKHOS = frameoperacionestandar.Attributes["src"];
                }
                string LINKGP12 = "";
                if (framemurodecalidad.Attributes["src"] != "")
                {
                    LINKGP12 = framemurodecalidad.Attributes["src"];
                }
                string LINKDEF = "";
                if (framedefoteca.Attributes["src"] != "")
                {
                    LINKDEF = framedefoteca.Attributes["src"];
                }
                string LINKEMB = "";
                if (frameembalaje.Attributes["src"] != "")
                {
                    LINKEMB = frameembalaje.Attributes["src"];
                }
                string LINKIMG = hyperlink9.ImageUrl;

                string cambios = "Se ha modificado" + txtPlanControl.Text + txtPautaControl.Text + txtOperacionEstandar.Text + txtDefoteca.Text + txtEmbalaje.Text + ".";
                string observaciones = "'" + tbObservacionesCarga.Text + "',";
                Conexion_DOCUMENTAL conexion = new Conexion_DOCUMENTAL();
                conexion.Insertar_documentos_referencia_moldeV2(tbReferenciaCarga.Text, tbMolde.InnerText, LINKPAUTACONTROL, LINKEMBALTERNATIVO, LINKHOS, LINKGP12, LINKDEF, LINKEMB, LINKIMG, tbObservacionesCarga.Text, DateTime.Now.ToString(), cambios);
                conexion.Actualizar_NotaProductosBMSxFicha(Convert.ToInt32(tbReferenciaCarga.Text), tbObservacionesCargaBMS.Text);
                ResetearCajasCheck();
            }
            catch (Exception)
            {
            }
        }

        public void InsertarDocumentosMolde(object sender, EventArgs e)
        {
            try
            {

                string LINKPAUTACONTROL = "";
                string SETPAUTACONTROL = "";
                if ((CHECKPREVPAC.Checked == true))
                {
                    LINKPAUTACONTROL = framepautadecontrol.Attributes["src"];
                    SETPAUTACONTROL = "[PautaControl] = '" + LINKPAUTACONTROL + "',";
                }

                string LINKEMBALTERNATIVO = "";
                string SETEMBALTERNATIVO = "";
                if ((CHECKPREVEMBALT.Checked == true))
                {
                    LINKEMBALTERNATIVO = frameEmbalajeAlternativo.Attributes["src"];
                    SETEMBALTERNATIVO = "[PautaRecepcion2] = '" + LINKEMBALTERNATIVO + "',";
                }


                string LINKHOS = "";
                string SETHOS = "";
                if ((CHECKPREVHOS.Checked == true))
                {
                    LINKHOS = frameoperacionestandar.Attributes["src"];
                    SETHOS = "[OperacionEstandar] = '" + LINKHOS + "',";
                }

                string LINKGP12 = "";
                string SETGP12 = "";
                if ((CHECKPREVGP12.Checked == true))
                {
                    LINKGP12 = framemurodecalidad.Attributes["src"];
                    SETGP12 = "[Gp12] = '" + LINKGP12 + "',";
                }

                string LINKDEF = "";
                string SETDEF = "";
                if ((CHECKPREVDEF.Checked == true))
                {
                    LINKDEF = framedefoteca.Attributes["src"];
                    SETDEF = "[Defoteca] = '" + LINKDEF + "',";
                }

                string LINKEMB = "";
                string SETEMB = "";
                if ((CHECKPREVEMB.Checked == true))
                {
                    LINKEMB = frameembalaje.Attributes["src"];
                    SETEMB = "[Embalaje] = '" + LINKEMB + "',";
                }

                string LINKIMG = hyperlink9.ImageUrl;
                string SETIMG = "";
                if (CHECKIMG.Checked == true)
                {
                    SETIMG = "[ImagenPieza] = '" + LINKIMG + "',";
                }

                string cambios = "[RazonModificacion] = 'Se ha modificado" + txtPlanControl.Text + txtPautaControl.Text + txtOperacionEstandar.Text + txtDefoteca.Text + txtEmbalaje.Text + ".',";
                string observaciones = "[Observaciones] = '" + tbObservacionesCarga.Text + "',";
                Conexion_DOCUMENTAL conexion = new Conexion_DOCUMENTAL();
                conexion.Insertar_documentos_moldeBOTONV2(tbMolde.InnerText, SETPAUTACONTROL, SETEMBALTERNATIVO, SETHOS, SETGP12, SETDEF, SETEMB, observaciones, SETIMG, DateTime.Now.ToString(), cambios);
                ResetearCajasCheck();

            }
            catch (Exception)
            {
            }
        }
        
        public void NewSaveFile(object sender, EventArgs e)
        {
            if (UploadDOCAUX.HasFile)
            { 
                try
                {
                    // Specify the path to save the uploaded file to.

                    string savePath = @"C:\inetpub_thermoweb\SMARTH_docs\DOCUMENTAL\"+ tbMolde.InnerText + "\\"; //MOLDE
                    if (!Directory.Exists(savePath))
                    {
                        Directory.CreateDirectory(savePath);
                    }

                    // Get the name of the file to upload.
                    //string fileName = FileUpload1.FileName;
                    string fileName = "";
                    string extension = Path.GetExtension(UploadDOCAUX.PostedFile.FileName);
                           fileName = tbMolde.InnerText + "_" + tbReferenciaCarga.Text + "_" + DropTipoDOCAUX.SelectedValue + DateTime.Now.ToString("yyyy-MM-dd_HHmmss") + extension;
              
                    savePath += fileName;
                    // Call the SaveAs method to save the uploaded
                    // file to the specified directory.
               
                    UploadDOCAUX.SaveAs(savePath);

                    Conexion_SMARTH SHconexion = new Conexion_SMARTH();
                    Conexion_DOCUMENTAL conexion = new Conexion_DOCUMENTAL();
                    conexion.Insertar_documentos_V3(tbReferenciaCarga.Text, tbMolde.InnerText, conexion.Devuelve_ID_TipoDocumento_SMARTH(DropTipoDOCAUX.SelectedValue), tbDescripcionDOCAUX.Value, "..\\SMARTH_docs\\DOCUMENTAL\\" + tbMolde.InnerText + "\\" + fileName, tbFechaDOCAUX.Value, Convert.ToInt32(TbNumEdicion.Value), SHconexion.Devuelve_ID_Piloto_SMARTH(dropAprobadoDOCAUX.SelectedValue), Convert.ToInt32(DropSelectMOLREF.SelectedValue));

                    DropSelectMOLREF.ClearSelection();
                    DropTipoDOCAUX.ClearSelection();
                    tbDescripcionDOCAUX.Value = "";
                    tbFechaDOCAUX.Value = "";
                    TbNumEdicion.Value = "0";
                    dropAprobadoDOCAUX.ClearSelection();
                    tbReferencia.Value = tbReferenciaCarga.Text;
                    CargarDatos(null,null);


                }
                catch (Exception ex)
                {
                }
            }
        }

        //Ver estructura

        public void VerEstructura(object sender, EventArgs e)
        {
            Conexion_DOCUMENTAL conexion = new Conexion_DOCUMENTAL();
            DataSet Estructura = conexion.DevuelveEstructuraProducto(tbReferenciaCarga.Text);
            GridEstructura.DataSource = Estructura;
            GridEstructura.DataBind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupEstructura();", true);
        }

        
     
                            
                            
        }

}
