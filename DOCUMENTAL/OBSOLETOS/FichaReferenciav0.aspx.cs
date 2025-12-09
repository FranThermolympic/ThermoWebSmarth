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
                btnsubir0.Visible = false;
                btnsubir1.Visible = false;
                btnsubir2.Visible = false;
                btnsubir4.Visible = false;
                btnsubir5.Visible = false;
                btnsubir6.Visible = false;
                btnsubir7.Visible = false;
                btnsubir8.Visible = false;
                btnsubir9.Visible = false;
                btnsubir10.Visible = false;
                btnsubir11.Visible = false;
                btnsubir12.Visible = false;
                btnsubir13.Visible = false;
                btnsubir14.Visible = false;
                btnsubir15.Visible = false;
                btnsubir16.Visible = false;
                btnsubir17.Visible = false;
                btnsubir18.Visible = false;
                btnsubir19.Visible = false;
                btnsubir20.Visible = false;
                btnsubir21.Visible = false;
                btnsubir22.Visible = false;
                btnsubir23.Visible = false;

                btnver2.Visible = false;
                btnver5.Visible = false;
                btnver7.Visible = false;
                btnver9.Visible = false;
                btnver11.Visible = false;
                btnver13.Visible = false;
                btnver15.Visible = false;
                btnver17.Visible = false;
                btnver19.Visible = false;
                btnver21.Visible = false;
                btnver23.Visible = false;

                FileUpload0.Visible = false;
                FileUpload1.Visible = false;
                FileUpload2.Visible = false;
                FileUpload3.Visible = false;
                FileUpload4.Visible = false;
                FileUpload5.Visible = false;
                FileUpload6.Visible = false;
                FileUpload7.Visible = false;
                FileUpload8.Visible = false;
                FileUpload9.Visible = false;
                FileUpload10.Visible = false;
                FileUpload11.Visible = false;
                FileUpload12.Visible = false;
                FileUpload13.Visible = false;
                FileUpload14.Visible = false;
                FileUpload15.Visible = false;
                FileUpload16.Visible = false;
                FileUpload17.Visible = false;
                FileUpload18.Visible = false;
                FileUpload19.Visible = false;
                FileUpload20.Visible = false;
                FileUpload21.Visible = false;
                FileUpload22.Visible = false;

                tbObservacionesCarga.Enabled = false;
                tbObservacionesCargaBMS.Enabled = false;
                if (Request.QueryString["REFERENCIA"] != null)
                {
                    tbReferencia.Value = Request.QueryString["REFERENCIA"];
                    CargarDatos(null, e);
                }

            }

        }

        protected void Redireccionaraiz(object sender, EventArgs e)
            
        {
            Response.Redirect(url: "GP12/GP12Documentacion.aspx");
        }

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
                    "^FXCamposvariables^FO715,515^A0R35,35^FB400,1,,C^FD"+cliente+"^FS" +
                    "^FO600,515^A0R65,65^FB400,1,,C^FD^FS^FO455,1120^A0R100,100^FD" + cantidad + "^FS^FO465,585^A0R80,80^FD" + referencia + "^FS^FO455,310^A0R45,45^FD" + pedido + "^FS^FO330,55^A0R65,65^FD" + refcliente + "^FS" +
                    "^FO330,760^A0R65,65^FD" + descripcion + "^FS^FO55,55^A0R65,65^FDUN476925631" + caja + "^FS^FO205,1160^A0R65,65^FD" + lote + "^FS^FO215,1370^A0R50,50^FD" + caja + "^FS^FO110,1160^A0R50,50^FDD"+fecha+"^FS" +
                    "^FO110,1410^A0R50,50^FD" + hora + "^FS^FO15,1160^A0R50,50^FD^FS^FO10,1250^A0R65,65^FB400,1,,C^FD.^FS^FXCodigosdebarra^FO140,55^BCR,130,N,N,N,N^FD>:1JUN476925631" + caja + "^FS^FO510,310^BCR,80,N,N,N,N^FD>:K" + pedido + "^FS" +
                    "^FO615,1000^B7R,4,8,0,N,N^FD[)>E2*06|SUN476925631" + caja + "|P" + refcliente + "|N" + lote + "|Q" + cantidad+"E2*11^FS^XZ");

            //byte[] zpl = Encoding.UTF8.GetBytes("^xa^cfa,50^fo100,100^fdHello World^fs^xz");
            byte[] zpl = Encoding.UTF8.GetBytes("^XA^[L,0^FS^XZ^XA~TA000~JSN^MUD^PON^PMN^LH0,0^JMA^PR8,8~SD15^JUS^LRN^CI0^MMT^LS0^LT0^FWR^FPH^MTD^PW559^MNM^ML1817^BY2,3^FXHorizontalLines^FS^FXHorizontalLine^FS^FO507,0^GB0,1615,2^FS^FO408,0^GB0,1615,2^FS^FO234,0^GB0,1615,2^FS" +
                    "^FXHorizontalLine^FS^FO348,961^GB0,254,2^FS^FO291,961^GB0,254,2^FS^FO174,807^GB0,807,2^FS^FO117,807^GB0,807,2^FS^FO60,807^GB0,807,2^FS^FXVerticalLines^FS^FXVerticalLine^FS^FO507,538^GB53,0,2^FS^FO408,807^GB98,0,2^FS" +
                    "^FO0,807^GB234,0,2^FS^FO174,961^GB234,0,2^FS^FO507,999^GB53,0,2^FS^FO60,1215^GB57,0,2^FS^FO234,1215^GB174,0,2^FS^FXVerticalLine^FS^FXFixedTexts^FS^FO539,12^AMR19,19^FB523,1,,L^FDSHIPTO^FS^FO467,12^AMR19,19^FB784,2,,L^FDDOCUMENTNO^FS" +
                    "^FO368,12^AMR19,19^FB938,2,,L^FDPARTNUMBER(P)^FS^FO213,12^AMR19,19^FB784,1,,L^FDLABELNO(S)^FS^FO520,542^AMR19,19^FB461,2,,L^FDUNLOADINGPOINT^FS^FO467,811^AMR19,19^FB784,2,,L^FDDESTINATIONPOINT^FS^FO213,811^AMR19,19^FB231,1,,L^FDNOOFBOXES^FS" +
                    "^FO153,811^AMR19,19^FB784,1,,L^FDLOGISTICREFERENCE(30S)^FS^FO96,811^AMR19,19^FB308,1,,L^FDBATCHNO(H)^FS^FO4,811^AMR38,38^FB77,1,,C^FDOP:^FS^FO539,1003^AMR19,19^FB600,1,,L^FDSHIPFROM^FS^FO387,965^AMR19,19^FB246,1,,L^FDNETWEIGHT^FS" +
                    "^FO327,965^AMR19,19^FB246,1,,L^FDGROSSWEIGHT^FS^FO270,965^AMR19,19^FB246,1,,L^FDDATE^FS^FO213,965^AMR19,19^FB630,1,,L^FDDESCRIPTION^FS^FO77,1219^AMR19,19^FB477,2,,L^FDREV.NO^FS^FO387,1219^AMR19,19^FB384,1,,L^FDQUANTITY(Q)^FS" +
                    "^FO7,1538^AMR15,15^FB46,1,,C^FDETI9^FS" +
                    "^FXVariabletexts^FS^FO503,12^AMR38,38^FB523,1,,C^FD" + cliente + "^FS^FO503,542^AMR38,38^FB454,1,,C^FD"+unloadingpoint+"^FS^FO503,1003^AMR38,38^FB592,1,,C^FDTHERMOLYMPICSL^FS^FO434,12^AMR46,46^FB784,1,,R^FD.^FS" +
                    "^FO434,811^AMR46,46^FB784,1,,C^FD"+gp12+"^FS^FO267,49^AMR46,46^BCR,68,N,N,N,N^FD>:P"+refcliente+"^FS^FO206,412^AMR168,168^FB392,1,,C^FD.^FS^FO351,984^AMR38,38^FB231,1,,C^FD0.0KGM^FS^FO291,984^AMR38,38^FB231,1,,C^FD0.0KGM^FS^FO233,984^AMR46,46^FB231,1,,C^FDD"+fecha+"^FS" +
                    "^FO293,1215^AMR91,91^FB400,1,,C^FD"+cantidad+"^FS^FO242,1269^BCR,45,N,N,N,N^FD>:Q"+cantidad+"^FS^FO45,115^AMR38,38^BCR,98,N,N,N,N^FD>:S"+caja+"^FS^FO177,807^AMR38,38^FB154,1,,C^FD1^FS^FO169,961^AMR46,46^FB650,1,,C^FD"+descripcion+"^FS^FO117,807^AMR38,38^FB308,1,,C^FD"+referencia+"^FS" +
                    "^FO128,1134^BCR,38,N,N,N,N^FD>:30S"+referencia+"^FS^FO60,811^AMR38,38^FB115,1,,C^FD"+lote+"^FS^FO60,1222^AMR38,38^FB396,1,,C^FD"+plano+"^FS^FO4,884^AMR38,38^FB246,1,,C^FD^FS^FO4,1246^AMR38,38^FB154,1,,C^FD"+hora+"^FS^FO19,577^BXR,4,200,48,48^FD[]>_1E06_1D12PGTL3_1D9K01_1D3LTHERMOLYMPICSL_1D4LSPAIN_1D8V._1D2L1752_1DV0062056630_1DQ250_1D3QPC_1D2Q0_1DP"+referencia+"_1D1JUN00"+lote+""+caja+"_1DB._1D2P"+plano+"_1E_04^FS" +
                    "^FO4,1130^AMR38,38^FB154,1,,C^FDHOUR:^FS^FO350,123^AMR46,46^FB477,1,,L^FD"+refcliente+"^FS^FO159,115^AMR38,38^FB692,1,,L^FD"+caja+"^FS^FO72,930^BCR,38,N,N,N,N^FD>:H"+lote+"^FS^XZ");
            
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
                Response.Write("<script language='javascript'> window.open('"+url+"', 'window','HEIGHT=600,WIDTH=820,top=50,left=50,toolbar=yes,scrollbars=yes,resizable=yes');</script>");

            }
            catch (WebException ex)
            {
                Console.WriteLine("Error: {0}", ex.Status);
            }
        }

        protected void RedireccionaGP12(object sender, EventArgs e)
        {
            Response.Redirect(url: "DocumentosGP12.aspx?REFERENCIA="+tbReferenciaCarga.Text);
        }

        protected void RedireccionaOTROS(object sender, EventArgs e)
        {
            Response.Redirect(url: "DocumentosPlanta.aspx?REFERENCIA=" + tbReferenciaCarga.Text);
            //Response.Redirect(url: "DocumentosPlanta.aspx?MAQUINA=36");
        }
        public void RedireccionaDocumento(Object sender, EventArgs e)
        {
            try
            {
                HtmlButton button = (HtmlButton)sender;
                string name = button.ID;
                
                switch (name)
                {
                    case "btnver2":
                        string urlframe = frameplandecontrol.Attributes["src"];
                        Response.Redirect(url: urlframe);
                        break;
                    case "btnver5":
                        string urlframe2 = framepautadecontrol.Attributes["src"];
                        Response.Redirect(url: urlframe2);
                        break;
                    case "btnver7":
                        string urlframe3 = framepautarecepcion.Attributes["src"];
                        Response.Redirect(url: urlframe3);
                        break;
                    case "btnver9":
                        string urlframe4 = frameoperacionestandar.Attributes["src"];
                        Response.Redirect(url: urlframe4);
                        break;
                    case "btnver11":
                        string urlframe5 = framemurodecalidad.Attributes["src"];
                        Response.Redirect(url: urlframe5);
                        break;
                    case "btnver13":
                        string urlframe6 = frameoperacionestandar2.Attributes["src"];
                        Response.Redirect(url: urlframe6);
                        break;
                    case "btnver15":
                        string urlframe7 = framedefoteca.Attributes["src"];
                        Response.Redirect(url: urlframe7);
                        break;
                    case "btnver17":
                        string urlframe8 = frameembalaje.Attributes["src"];
                        Response.Redirect(url: urlframe8);
                        break;
                    case "btnver19":
                        string urlframe9 = frameretrabajo.Attributes["src"];
                        Response.Redirect(url: urlframe9);
                        break;
                    case "btnver21":
                        string urlframe10 = framevideoaux.Attributes["src"];
                        Response.Redirect(url: urlframe10);
                        break;
                    case "btnver23":
                        string urlframe11 = frameNoConformidad.Attributes["src"];
                        Response.Redirect(url: urlframe11);
                        break;
                }
            }
            catch (Exception)
            {
            }
        }

        public void BorrarDocumento(Object sender, EventArgs e)
        {
            try
            {
                HtmlButton button = (HtmlButton)sender;
                string name = button.ID;

                switch (name)
                {
                    case "btnborrar2":
                        frameplandecontrol.Attributes.Add("src", "");        
                        DefaultPlanControl.Visible = true; 
                        PreviewPlanControl.Visible = false;

                        break;
                    case "btnborrar5":
                        framepautadecontrol.Attributes.Add("src", "");
                        DefaultPautaControl.Visible = true;
                        PreviewPautaControl.Visible = false;
               
                        break;
                    case "btnborrar7":
                        framepautarecepcion.Attributes.Add("src", "");
                        DefaultPautaRecepcion.Visible = true;
                        PreviewPautaRecepcion.Visible = false;

                        break;
                    case "btnborrar9":
                        frameoperacionestandar.Attributes.Add("src", "");
                        DefaultOperacionEstandar.Visible = true;
                        PreviewOperacionEstandar.Visible = false;
                        break;
                    case "btnborrar11":
                        framemurodecalidad.Attributes.Add("src", "");
                        DefaultGP12.Visible = true;
                        PreviewGP12.Visible = false;
                        break;
                    case "btnborrar13":
                        frameoperacionestandar2.Attributes.Add("src", "");
                        DefaultOperacionEstandar2.Visible = true;
                        PreviewOperacionEstandar2.Visible = false;
                        break;
                    case "btnborrar15":
                        framedefoteca.Attributes.Add("src", "");
                        DefaultDefoteca.Visible = true;
                        PreviewDefoteca.Visible = false;

                        break;
                    case "btnborrar17":
                        frameembalaje.Attributes.Add("src", "");
                        DefaultEmbalaje.Visible = true;
                        PreviewEmbalaje.Visible = false;
                        break;

                    case "btnborrar19":
                        frameretrabajo.Attributes.Add("src", "");
                        defaultRetrabajo.Visible = true;
                        previewRetrabajo.Visible = false;
                        break;

                    case "btnborrar21":
                        framevideoaux.Attributes.Add("src", "");
                        defaultVideo.Visible = true;
                        previewVideo.Visible = false;
                        break;

                    case "btnborrar23":
                        frameNoConformidad.Attributes.Add("src", "");
                        defaultNC.Visible = true;
                        previewNC.Visible = false;
                        break;

                }
                InsertarDocumentosReferenciaMolde(null, null);
            }
            catch (Exception)
            {
            }
        }

        public void BorrarDocumentoSeleccionados(Object sender, EventArgs e)
        {
            try
            {
                Button button = (Button)sender;
          
                string name = button.ID;
                switch (name)
                { 
                case "borrarmolde":
                        VaciaFrameSeleccionado();
                        InsertarDocumentosMolde(null, null);
                    break;

                case "borrarref":                       
                        VaciaFrameSeleccionado();
                        InsertarDocumentosReferenciaMolde(null,null);
                        break;
                }

            }
            catch (Exception)
            {
            }
        }

        public void VaciaFrameSeleccionado ()
        {
            try
            {
                if (CHECKPREVPC.Checked == true)
                {
                    frameplandecontrol.Attributes.Add("src", "");
                    DefaultPlanControl.Visible = true;
                    PreviewPlanControl.Visible = false;
                }
                if (CHECKPREVPAC.Checked == true)
                {
                    framepautadecontrol.Attributes.Add("src", "");
                    DefaultPautaControl.Visible = true;
                    PreviewPautaControl.Visible = false;
                }
                if (CHECKPREVREC.Checked == true)
                {
                    framepautarecepcion.Attributes.Add("src", "");
                    DefaultPautaRecepcion.Visible = true;
                    PreviewPautaRecepcion.Visible = false;

                }
                if (CHECKPREVHOS.Checked == true)
                {
                    frameoperacionestandar.Attributes.Add("src", "");
                    DefaultOperacionEstandar.Visible = true;
                    PreviewOperacionEstandar.Visible = false;
                }
                if (CHECKPREVGP12.Checked == true)
                {
                    framemurodecalidad.Attributes.Add("src", "");
                    DefaultGP12.Visible = true;
                    PreviewGP12.Visible = false;
                }
                if (CHECKPREVHOS2.Checked == true)
                {
                    frameoperacionestandar2.Attributes.Add("src", "");
                    DefaultOperacionEstandar2.Visible = true;
                    PreviewOperacionEstandar2.Visible = false;
                }
                if (CHECKPREVDEF.Checked == true)
                {
                    framedefoteca.Attributes.Add("src", "");
                    DefaultDefoteca.Visible = true;
                    PreviewDefoteca.Visible = false;
                }

                if (CHECKPREVEMB.Checked == true)
                {
                    frameembalaje.Attributes.Add("src", "");
                    DefaultEmbalaje.Visible = true;
                    PreviewEmbalaje.Visible = false;
                }
                if (CHECKPREVRTR.Checked == true)
                {
                    frameretrabajo.Attributes.Add("src", "");
                    defaultRetrabajo.Visible = true;
                    previewRetrabajo.Visible = false;
                }
                if (CHECKDEFVID.Checked == true)
                {
                    framevideoaux.Attributes.Add("src", "");
                    defaultVideo.Visible = true;
                    previewVideo.Visible = false;
                }
                if (CHECKDEFNC.Checked == true)
                {
                    frameNoConformidad.Attributes.Add("src", "");
                    defaultNC.Visible = true;
                    previewNC.Visible = false;

                }
            }
            catch (Exception)
            { }
        }

        public void CargarDatos(Object sender, EventArgs e)
        {
            try
            {
                resetearcajas();
                DefaultPlanControl.Visible = true;
                DefaultPautaControl.Visible = true;
                DefaultPautaRecepcion.Visible = true;
                DefaultOperacionEstandar.Visible = true;
                DefaultOperacionEstandar2.Visible = true;
                DefaultDefoteca.Visible = true;
                DefaultEmbalaje.Visible = true;
                DefaultGP12.Visible = true;
                defaultRetrabajo.Visible = true;
                defaultVideo.Visible = true;
                PreviewPlanControl.Visible = false;
                PreviewPautaControl.Visible = false;
                PreviewPautaRecepcion.Visible = false;
                PreviewOperacionEstandar.Visible = false;
                PreviewOperacionEstandar2.Visible = false;
                PreviewDefoteca.Visible = false;
                PreviewEmbalaje.Visible = false;
                PreviewGP12.Visible = false;
                previewRetrabajo.Visible = false;
                previewVideo.Visible = false;
                previewNC.Visible = false;
                btnVistaMaquina.Disabled = false;
                btnVistaGP12.Disabled = false;
                btnmolde.Disabled = false;
                borrarmolde.Visible = true;
                borrarref.Visible = true;

                Conexion_DOCUMENTAL conexion = new Conexion_DOCUMENTAL();
                DataSet ds = conexion.Devuelve_dataset_filtroreferenciasSMARTH(tbReferencia.Value.ToString(),"","","","") ;
                tbReferenciaCarga.Text = ds.Tables[0].Rows[0]["REF"].ToString();
                tbDescripcionCarga.Text = ds.Tables[0].Rows[0]["Descripcion"].ToString();
                tbMolde.Text = ds.Tables[0].Rows[0]["Molde"].ToString();
                tbObservacionesCarga.Text = ds.Tables[0].Rows[0]["Observaciones"].ToString();
                tbObservacionesCarga.Enabled = true;

               
                DataSet BMS = conexion.DevuelveRemarkProducto(tbReferencia.Value.ToString());
                tbObservacionesCargaBMS.Text = BMS.Tables[0].Rows[0]["C_REMARKS"].ToString();
                tbObservacionesCargaBMS.Enabled = true;


                string PlanControl = ds.Tables[0].Rows[0]["PlanControl"].ToString();
                     if (PlanControl != "") { PreviewPlanControl.Visible = true; DefaultPlanControl.Visible = false; }
                     frameplandecontrol.Attributes.Add("src", PlanControl);     
                string PautaControl = ds.Tables[0].Rows[0]["PautaControl"].ToString();
                     if (PautaControl != "") { PreviewPautaControl.Visible = true; DefaultPautaControl.Visible = false; }
                     framepautadecontrol.Attributes.Add("src", PautaControl);
                string PautaRecepcion1 = ds.Tables[0].Rows[0]["PautaRecepcion1"].ToString();  
                     if (PautaRecepcion1 != "") { PreviewPautaRecepcion.Visible = true; DefaultPautaRecepcion.Visible = false; }
                     framepautarecepcion.Attributes.Add("src", PautaRecepcion1);
                string OperacionEstandar = ds.Tables[0].Rows[0]["OperacionEstandar"].ToString();
                    if (OperacionEstandar != "") { PreviewOperacionEstandar.Visible = true; DefaultOperacionEstandar.Visible = false; }
                    frameoperacionestandar.Attributes.Add("src", OperacionEstandar);
                string OperacionEstandar2 = ds.Tables[0].Rows[0]["OperacionEstandar2"].ToString();
                    if (OperacionEstandar2 != "") { PreviewOperacionEstandar2.Visible = true; DefaultOperacionEstandar2.Visible = false; }
                    frameoperacionestandar2.Attributes.Add("src", OperacionEstandar2);
                string Defoteca = ds.Tables[0].Rows[0]["Defoteca"].ToString();
                    if (Defoteca != "") { PreviewDefoteca.Visible = true; DefaultDefoteca.Visible = false; }
                    framedefoteca.Attributes.Add("src", Defoteca);
                string Embalaje = ds.Tables[0].Rows[0]["Embalaje"].ToString();
                    if (Embalaje != "") { PreviewEmbalaje.Visible = true; DefaultEmbalaje.Visible = false; }
                    frameembalaje.Attributes.Add("src", Embalaje);
                string Gp12 = ds.Tables[0].Rows[0]["Gp12"].ToString();
                    if (Gp12 != "") { PreviewGP12.Visible = true; DefaultGP12.Visible = false; }
                    framemurodecalidad.Attributes.Add("src", Gp12);
                string Retrabajo = ds.Tables[0].Rows[0]["PautaRetrabajo"].ToString();
                    if (Retrabajo != "") { previewRetrabajo.Visible = true; defaultRetrabajo.Visible = false; }
                    frameretrabajo.Attributes.Add("src", Retrabajo);
                string VideoAux = ds.Tables[0].Rows[0]["VideoAuxiliar"].ToString();
                    if (VideoAux != "") { previewVideo.Visible = true; defaultVideo.Visible = false;}
                    framevideoaux.Attributes.Add("src",VideoAux);
                string NoConformidad = ds.Tables[0].Rows[0]["NoConformidades"].ToString();
                    if (NoConformidad != "") { previewNC.Visible = true; defaultNC.Visible = false; }
                    frameNoConformidad.Attributes.Add("src", NoConformidad);


                string ImagenPieza = ds.Tables[0].Rows[0]["ImagenPieza"].ToString();
                    if (ImagenPieza != "") { hyperlink9.ImageUrl = ImagenPieza; }
                    else { hyperlink9.ImageUrl = "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg"; }

                ds_RefXMolde = conexion.devuelve_moldesXreferencia(tbMolde.Text.ToString());
                dgv_RefXMolde.DataSource = ds_RefXMolde;
                dgv_RefXMolde.DataBind();

                btnsubir0.Visible = true;
                btnsubir1.Visible = true;
                btnsubir2.Visible = true;
                btnsubir4.Visible = true;
                btnsubir5.Visible = true;
                btnsubir6.Visible = true;
                btnsubir7.Visible = true;
                btnsubir8.Visible = true;
                btnsubir9.Visible = true;
                btnsubir10.Visible = true;
                btnsubir11.Visible = true;
                btnsubir12.Visible = true;
                btnsubir13.Visible = true;
                btnsubir14.Visible = true;
                btnsubir15.Visible = true;
                btnsubir16.Visible = true;
                btnsubir17.Visible = true;
                btnsubir18.Visible = true;
                btnsubir19.Visible = true;
                btnsubir20.Visible = true;
                btnsubir21.Visible = true;
                btnsubir22.Visible = true;
                btnsubir23.Visible = true;

                btnver2.Visible = true;
                btnver5.Visible = true;
                btnver7.Visible = true;
                btnver9.Visible = true;
                btnver11.Visible = true;
                btnver13.Visible = true;
                btnver15.Visible = true;
                btnver17.Visible = true;
                btnver19.Visible = true;
                btnver21.Visible = true;
                btnver23.Visible = true;

                FileUpload0.Visible = true;
                FileUpload1.Visible = true;
                FileUpload2.Visible = true;
                FileUpload3.Visible = true;
                FileUpload4.Visible = true;
                FileUpload5.Visible = true;
                FileUpload6.Visible = true;
                FileUpload7.Visible = true;
                FileUpload8.Visible = true;
                FileUpload9.Visible = true;
                FileUpload10.Visible = true;
                FileUpload11.Visible = true;
                FileUpload12.Visible = true;
                FileUpload13.Visible = true;
                FileUpload14.Visible = true;
                FileUpload15.Visible = true;
                FileUpload16.Visible = true;
                FileUpload17.Visible = true;
                FileUpload18.Visible = true;
                FileUpload19.Visible = true;
                FileUpload20.Visible = true;
                FileUpload21.Visible = true;
                FileUpload22.Visible = true;
                
            }
            catch (Exception)
            { 
            }
        }

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
                if (FileUpload6.HasFile)
                    SaveFile(FileUpload6.PostedFile, 6);
                if (FileUpload7.HasFile)
                    SaveFile(FileUpload7.PostedFile, 7);
                if (FileUpload8.HasFile)
                    SaveFile(FileUpload8.PostedFile, 8);
                if (FileUpload9.HasFile)
                    SaveFile(FileUpload9.PostedFile, 9);
                if (FileUpload10.HasFile)
                    SaveFile(FileUpload10.PostedFile, 10);
                if (FileUpload11.HasFile)
                    SaveFile(FileUpload11.PostedFile, 11);
                if (FileUpload12.HasFile)
                    SaveFile(FileUpload12.PostedFile, 12);
                if (FileUpload13.HasFile)
                    SaveFile(FileUpload13.PostedFile, 13);
                if (FileUpload13.HasFile)
                    SaveFile(FileUpload13.PostedFile, 13);
                if (FileUpload14.HasFile)
                    SaveFile(FileUpload14.PostedFile, 14);
                if (FileUpload15.HasFile)
                    SaveFile(FileUpload15.PostedFile, 15);
                if (FileUpload16.HasFile)
                    SaveFile(FileUpload16.PostedFile, 16);

                if (FileUpload17.HasFile)
                    SaveFile(FileUpload17.PostedFile, 17);
                if (FileUpload18.HasFile)
                    SaveFile(FileUpload18.PostedFile, 18);
                if (FileUpload19.HasFile)
                    SaveFile(FileUpload19.PostedFile, 19);
                if (FileUpload20.HasFile)
                    SaveFile(FileUpload20.PostedFile, 20);
                if (FileUpload21.HasFile)
                    SaveFile(FileUpload21.PostedFile, 21);
                if (FileUpload22.HasFile)
                    SaveFile(FileUpload22.PostedFile, 22);

                if (FileUpload0.HasFile)
                    SaveFile(FileUpload0.PostedFile, 100);
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
                                  //"C:\\inetpub_thermoweb\\DOCUMENTAL\\DOCUMENTOS\\";


                // Get the name of the file to upload.
                //string fileName = FileUpload1.FileName;
                string fileName = "";
                switch (num_img)
                {
                    case 1:
                        string extension = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
                        fileName = tbMolde.Text + "_" + tbReferenciaCarga.Text + "_" + "PLANCONTROL" + extension;
                        break;
                    case 2:
                        string extension2 = System.IO.Path.GetExtension(FileUpload2.PostedFile.FileName);
                        fileName = tbMolde.Text + "_" + tbReferenciaCarga.Text + "_" + "PLANCONTROL" + extension2;
                        break;
                    case 3:
                        string extension3 = System.IO.Path.GetExtension(FileUpload3.PostedFile.FileName);
                        fileName = tbMolde.Text + "_" + tbReferenciaCarga.Text + "_" + "PAUTACONTROL" + extension3;
                        break;
                    case 4:
                        string extension4 = System.IO.Path.GetExtension(FileUpload4.PostedFile.FileName);
                        fileName = tbMolde.Text + "_" + tbReferenciaCarga.Text + "_" + "PAUTACONTROL" + extension4;
                        break;
                    case 5:
                        string extension5 = System.IO.Path.GetExtension(FileUpload5.PostedFile.FileName);
                        fileName = tbMolde.Text + "_" + tbReferenciaCarga.Text + "_" + "RECEPCION" + extension5;
                        break;
                    case 6:
                        string extension6 = System.IO.Path.GetExtension(FileUpload6.PostedFile.FileName);
                        fileName = tbMolde.Text + "_" + tbReferenciaCarga.Text + "_" + "RECEPCION" + extension6;
                        break;
                    case 7:
                        string extension7 = System.IO.Path.GetExtension(FileUpload7.PostedFile.FileName);
                        fileName = tbMolde.Text + "_" + tbReferenciaCarga.Text + "_" + "HOS" + extension7;
                        break;
                    case 8:
                        string extension8 = System.IO.Path.GetExtension(FileUpload8.PostedFile.FileName);
                        fileName = tbMolde.Text + "_" + tbReferenciaCarga.Text + "_" + "HOS" + extension8;
                        break;
                    case 9:
                        string extension9 = System.IO.Path.GetExtension(FileUpload9.PostedFile.FileName);
                        fileName = tbMolde.Text + "_" + tbReferenciaCarga.Text + "_" + "GP12" + extension9;
                        break;
                    case 10:
                        string extension10 = System.IO.Path.GetExtension(FileUpload10.PostedFile.FileName);
                        fileName = tbMolde.Text + "_" + tbReferenciaCarga.Text + "_" + "GP12" + extension10;
                        break;
                    case 11:
                        string extension11 = System.IO.Path.GetExtension(FileUpload11.PostedFile.FileName);
                        fileName = tbMolde.Text + "_" + tbReferenciaCarga.Text + "_" + "HOS2" + extension11;
                        break;
                    case 12:
                        string extension12 = System.IO.Path.GetExtension(FileUpload12.PostedFile.FileName);
                        fileName = tbMolde.Text + "_" + tbReferenciaCarga.Text + "_" + "HOS2" + extension12;
                        break;
                    case 13:
                        string extension13 = System.IO.Path.GetExtension(FileUpload13.PostedFile.FileName);
                        fileName = tbMolde.Text + "_" + tbReferenciaCarga.Text + "_" + "DEF" + extension13;
                        break;
                    case 14:
                        string extension14 = System.IO.Path.GetExtension(FileUpload14.PostedFile.FileName);
                        fileName = tbMolde.Text + "_" + tbReferenciaCarga.Text + "_" + "DEF" + extension14;
                        break;
                    case 15:
                        string extension15 = System.IO.Path.GetExtension(FileUpload15.PostedFile.FileName);
                        fileName = tbMolde.Text + "_" + tbReferenciaCarga.Text + "_" + "EMBALAJE" + extension15;
                        break;
                    case 16:
                        string extension16 = System.IO.Path.GetExtension(FileUpload16.PostedFile.FileName);
                        fileName = tbMolde.Text + "_" + tbReferenciaCarga.Text + "_" + "EMBALAJE" + extension16;
                        break;


                    case 17:
                        string extension17 = System.IO.Path.GetExtension(FileUpload17.PostedFile.FileName);
                        fileName = tbMolde.Text + "_" + tbReferenciaCarga.Text + "_" + "RETRABAJO" + extension17;
                        break;
                    case 18:
                        string extension18 = System.IO.Path.GetExtension(FileUpload18.PostedFile.FileName);
                        fileName = tbMolde.Text + "_" + tbReferenciaCarga.Text + "_" + "RETRABAJO" + extension18;
                        break;
                    case 19:
                        string extension19 = System.IO.Path.GetExtension(FileUpload19.PostedFile.FileName);
                        fileName = tbMolde.Text + "_" + tbReferenciaCarga.Text + "_" + "VIDEOAUX" + extension19;
                        break;
                    case 20:
                        string extension20 = System.IO.Path.GetExtension(FileUpload20.PostedFile.FileName);
                        fileName = tbMolde.Text + "_" + tbReferenciaCarga.Text + "_" + "VIDEOAUX" + extension20;
                        break;
                    case 21:
                        string extension21 = System.IO.Path.GetExtension(FileUpload21.PostedFile.FileName);
                        fileName = tbMolde.Text + "_" + tbReferenciaCarga.Text + "_" + "NC" + extension21;
                        break;
                    case 22:
                        string extension22 = System.IO.Path.GetExtension(FileUpload22.PostedFile.FileName);
                        fileName = tbMolde.Text + "_" + tbReferenciaCarga.Text + "_" + "NC" + extension22;
                        break;

                    case 100:
                        string extension100 = System.IO.Path.GetExtension(FileUpload0.PostedFile.FileName);
                        fileName = tbMolde.Text + "_" + tbReferenciaCarga.Text + "_" + "IMG" + extension100;
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
                        //http://facts4-srv/thermogestion/DOCUMENTAL/DOCUMENTOS/GP12_60462006_33333284.pdf
                        FileUpload1.SaveAs(savePath);
                        frameplandecontrol.Attributes.Add("src", "http://facts4-srv/thermogestion/DOCUMENTAL/DOCUMENTOS/" + fileName);
                        txtPlanControl.Text = ", Plan de Control";
                        PreviewPlanControl.Visible = true;
                        DefaultPlanControl.Visible = false;
                        break;
                    case 2:
                        FileUpload2.SaveAs(savePath);
                        frameplandecontrol.Attributes.Add("src", "http://facts4-srv/thermogestion/DOCUMENTAL/DOCUMENTOS/" + fileName);
                        txtPlanControl.Text = ", Plan de Control";
                        PreviewPlanControl.Visible = true;
                        DefaultPlanControl.Visible = false;
                        break;
                    case 3:
                        FileUpload3.SaveAs(savePath);
                        framepautadecontrol.Attributes.Add("src", "http://facts4-srv/thermogestion/DOCUMENTAL/DOCUMENTOS/" + fileName);
                        txtPautaControl.Text = ", Pauta de Control";
                        PreviewPautaControl.Visible = true;
                        DefaultPautaControl.Visible = false;
                        break;
                    case 4:
                        FileUpload4.SaveAs(savePath);
                        framepautadecontrol.Attributes.Add("src", "http://facts4-srv/thermogestion/DOCUMENTAL/DOCUMENTOS/" + fileName);
                        txtPautaControl.Text = ", Pauta de Control";
                        PreviewPautaControl.Visible = true;
                        DefaultPautaControl.Visible = false;
                        break;
                    case 5:
                        FileUpload5.SaveAs(savePath);
                        framepautarecepcion.Attributes.Add("src", "http://facts4-srv/thermogestion/DOCUMENTAL/DOCUMENTOS/" + fileName);
                        PreviewPautaRecepcion.Visible = true;
                        DefaultPautaRecepcion.Visible = false;
                        break;
                    case 6:
                        FileUpload6.SaveAs(savePath);
                        framepautarecepcion.Attributes.Add("src", "http://facts4-srv/thermogestion/DOCUMENTAL/DOCUMENTOS/" + fileName);
                        PreviewPautaRecepcion.Visible = true;
                        DefaultPautaRecepcion.Visible = false;
                        break;
                    case 7:
                        FileUpload7.SaveAs(savePath);
                        frameoperacionestandar.Attributes.Add("src", "http://facts4-srv/thermogestion/DOCUMENTAL/DOCUMENTOS/" + fileName);
                        txtOperacionEstandar.Text = ", Hoja de Operación Estándar";
                        PreviewOperacionEstandar.Visible = true;
                        DefaultOperacionEstandar.Visible = false;
                        break;
                    case 8:
                        FileUpload8.SaveAs(savePath);
                        frameoperacionestandar.Attributes.Add("src", "http://facts4-srv/thermogestion/DOCUMENTAL/DOCUMENTOS/" + fileName);
                        txtOperacionEstandar.Text = ", Hoja de Operación Estándar";
                        PreviewOperacionEstandar.Visible = true;
                        DefaultOperacionEstandar.Visible = false;
                        break;
                    case 9:
                        FileUpload9.SaveAs(savePath);
                        framemurodecalidad.Attributes.Add("src", "http://facts4-srv/thermogestion/DOCUMENTAL/DOCUMENTOS/" + fileName);
                        PreviewGP12.Visible = true;
                        DefaultGP12.Visible = false;
                        break;
                    case 10:
                        FileUpload10.SaveAs(savePath);
                        framemurodecalidad.Attributes.Add("src", "http://facts4-srv/thermogestion/DOCUMENTAL/DOCUMENTOS/" + fileName);
                        PreviewGP12.Visible = true;
                        DefaultGP12.Visible = false;
                        //hyperlink1.ImageUrl = "http://facts4-srv/thermogestion/Imagenes/GP12/" + fileName;
                        //hyperlink1.NavigateUrl = "http://facts4-srv/thermogestion/Imagenes/GP12/" + fileName;
                        break;
                    case 11:
                        FileUpload11.SaveAs(savePath);
                        frameoperacionestandar2.Attributes.Add("src", "http://facts4-srv/thermogestion/DOCUMENTAL/DOCUMENTOS/" + fileName);
                        PreviewOperacionEstandar2.Visible = true;
                        DefaultOperacionEstandar2.Visible = false;
                        break;
                    case 12:
                        FileUpload12.SaveAs(savePath);
                        frameoperacionestandar2.Attributes.Add("src", "http://facts4-srv/thermogestion/DOCUMENTAL/DOCUMENTOS/" + fileName);
                        PreviewOperacionEstandar2.Visible = true;
                        DefaultOperacionEstandar2.Visible = false;
                        break;
                    case 13:
                        FileUpload13.SaveAs(savePath);
                        framedefoteca.Attributes.Add("src", "http://facts4-srv/thermogestion/DOCUMENTAL/DOCUMENTOS/" + fileName);
                        txtDefoteca.Text = ", Defoteca";
                        PreviewDefoteca.Visible = true;
                        DefaultDefoteca.Visible = false; 
                        //hyperlink1.ImageUrl = "http://facts4-srv/thermogestion/Imagenes/GP12/" + fileName;
                        //hyperlink1.NavigateUrl = "http://facts4-srv/thermogestion/Imagenes/GP12/" + fileName;
                        break;
                    case 14:
                        FileUpload14.SaveAs(savePath);
                        framedefoteca.Attributes.Add("src", "http://facts4-srv/thermogestion/DOCUMENTAL/DOCUMENTOS/" + fileName);
                        txtDefoteca.Text = ", Defoteca";
                        PreviewDefoteca.Visible = true;
                        DefaultDefoteca.Visible = false;
                        //hyperlink1.ImageUrl = "http://facts4-srv/thermogestion/Imagenes/GP12/" + fileName;
                        //hyperlink1.NavigateUrl = "http://facts4-srv/thermogestion/Imagenes/GP12/" + fileName;
                        break;
                    case 15:
                        FileUpload15.SaveAs(savePath);
                        frameembalaje.Attributes.Add("src", "http://facts4-srv/thermogestion/DOCUMENTAL/DOCUMENTOS/" + fileName);
                        txtEmbalaje.Text = ", Pauta de embalaje";
                        PreviewEmbalaje.Visible = true;
                        DefaultEmbalaje.Visible = false;
                        break;
                    case 16:
                        FileUpload16.SaveAs(savePath);
                        frameembalaje.Attributes.Add("src", "http://facts4-srv/thermogestion/DOCUMENTAL/DOCUMENTOS/" + fileName);
                        txtEmbalaje.Text = ", Pauta de embalaje";
                        PreviewEmbalaje.Visible = true;
                        DefaultEmbalaje.Visible = false;
                        break;

                    case 17:
                        FileUpload17.SaveAs(savePath);
                        frameretrabajo.Attributes.Add("src", "http://facts4-srv/thermogestion/DOCUMENTAL/DOCUMENTOS/" + fileName);
                        previewRetrabajo.Visible = true;
                        defaultRetrabajo.Visible = false;
                        break;
                    case 18:
                        FileUpload18.SaveAs(savePath);
                        frameretrabajo.Attributes.Add("src", "http://facts4-srv/thermogestion/DOCUMENTAL/DOCUMENTOS/" + fileName);
                        previewRetrabajo.Visible = true;
                        defaultRetrabajo.Visible = false;
                        break;

                    case 19:
                        FileUpload19.SaveAs(savePath);
                        framevideoaux.Attributes.Add("src", "http://facts4-srv/thermogestion/DOCUMENTAL/DOCUMENTOS/" + fileName);
                        previewVideo.Visible = true;
                        defaultVideo.Visible = false;
                        break;
                    case 20:
                        FileUpload20.SaveAs(savePath);
                        framevideoaux.Attributes.Add("src", "http://facts4-srv/thermogestion/DOCUMENTAL/DOCUMENTOS/" + fileName);
                        previewVideo.Visible = true;
                        defaultVideo.Visible = false;
                        break;
                    case 21:
                        FileUpload21.SaveAs(savePath);
                        frameNoConformidad.Attributes.Add("src", "http://facts4-srv/thermogestion/DOCUMENTAL/DOCUMENTOS/" + fileName);
                        previewNC.Visible = true;
                        defaultNC.Visible = false;
                        break;
                    case 22:
                        FileUpload22.SaveAs(savePath);
                        frameNoConformidad.Attributes.Add("src", "http://facts4-srv/thermogestion/DOCUMENTAL/DOCUMENTOS/" + fileName);
                        previewNC.Visible = true;
                        defaultNC.Visible = false;
                        break;

                    case 100:
                        FileUpload0.SaveAs(savePath);
                        hyperlink9.ImageUrl = "http://facts4-srv/thermogestion/DOCUMENTAL/DOCUMENTOS/" + fileName;
                        break;
                    default: break;
                }
            }
            catch (Exception)
            {
            }
            InsertarDocumentosReferenciaMolde(null, null);
        }

        public void InsertarDocumentosReferenciaMolde(Object sender, EventArgs e)
        { 
            try
            {
                string LINKPLANCONTROL = "";
                    if (frameplandecontrol.Attributes["src"] != "")
                    {
                        LINKPLANCONTROL = frameplandecontrol.Attributes["src"];
                    }
                string LINKPAUTACONTROL = "";
                    if (framepautadecontrol.Attributes["src"] != "")                   
                    {
                        LINKPAUTACONTROL = framepautadecontrol.Attributes["src"];
                    }
                string LINKRECEPCION = "";
                    if (framepautarecepcion.Attributes["src"] != "")
                    {
                        LINKRECEPCION = framepautarecepcion.Attributes["src"];
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
                string LINKHOS2 = "";
                    if (frameoperacionestandar2.Attributes["src"] != "")
                    {
                        LINKHOS2 = frameoperacionestandar2.Attributes["src"];
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

                string LINKRETRABAJO = "";
                    if (frameretrabajo.Attributes["src"] != "")
                    {
                        LINKRETRABAJO = frameretrabajo.Attributes["src"];
                    }

                string LINKVIDEOAUX = "";
                    if (framevideoaux.Attributes["src"] != "")
                    {
                        LINKVIDEOAUX = framevideoaux.Attributes["src"];
                    }

                string NOCONFORMIDAD = "";
                if (frameNoConformidad.Attributes["src"] != "")
                {
                    NOCONFORMIDAD = frameNoConformidad.Attributes["src"];
                }
                string cambios = "Se ha modificado" + txtPlanControl.Text + txtPautaControl.Text + txtOperacionEstandar.Text + txtDefoteca.Text + txtEmbalaje.Text + ".";
                string observaciones = "'" + tbObservacionesCarga.Text + "',";
                Conexion_DOCUMENTAL conexion = new Conexion_DOCUMENTAL();
                conexion.insertar_documentos_referencia_molde(tbReferenciaCarga.Text, tbMolde.Text, LINKPLANCONTROL, LINKPAUTACONTROL, LINKRECEPCION, LINKHOS, LINKGP12, LINKHOS2, LINKDEF, LINKEMB, LINKIMG, LINKRETRABAJO, LINKVIDEOAUX, tbObservacionesCarga.Text, NOCONFORMIDAD, DateTime.Now.ToString(),cambios);
                conexion.Actualizar_NotaProductosBMSxFicha(Convert.ToInt32(tbReferenciaCarga.Text), tbObservacionesCargaBMS.Text);
                resetearcajas();
            }
            catch (Exception)
            {
            }
        }

        public void InsertarDocumentosMolde(Object sender, EventArgs e)
        {
            try
            {
                string LINKPLANCONTROL = "";
                string SETPLANCONTROL = "";
                if ((CHECKDEFPC.Checked == true || CHECKPREVPC.Checked == true))
                    {
                    LINKPLANCONTROL = frameplandecontrol.Attributes["src"];
                        SETPLANCONTROL = "[PlanControl] = '" + LINKPLANCONTROL + "',";
                    }

                string LINKPAUTACONTROL = "";
                string SETPAUTACONTROL = "";
                if ((CHECKDEFPAC.Checked == true || CHECKPREVPAC.Checked == true))
                {
                    LINKPAUTACONTROL = framepautadecontrol.Attributes["src"];
                        SETPAUTACONTROL = "[PautaControl] = '" + LINKPAUTACONTROL + "',";
                }

                string LINKRECEPCION = "";
                string SETRECEPCION = "";
                if ((CHECKDEFPREC.Checked == true || CHECKPREVREC.Checked == true))
                {
                    LINKRECEPCION = framepautarecepcion.Attributes["src"];
                        SETRECEPCION = "[PautaRecepcion1] = '" + LINKRECEPCION + "',";
                }


                string LINKHOS = "";
                string SETHOS = "";
                if ((CHECKDEFHOS.Checked == true || CHECKPREVHOS.Checked == true))
                    {
                    LINKHOS = frameoperacionestandar.Attributes["src"];
                    SETHOS = "[OperacionEstandar] = '" + LINKHOS + "',";
                    }

                string LINKGP12 = "";
                string SETGP12 = "";
                if ((CHECKDEFGP12.Checked == true || CHECKPREVGP12.Checked == true))
                    {
                    LINKGP12 = framemurodecalidad.Attributes["src"];
                    SETGP12 = "[Gp12] = '" + LINKGP12 + "',"; 
                    }

                string LINKHOS2 = "";
                string SETHOS2 = "";
                if ((CHECKDEFHOS2.Checked == true || CHECKPREVHOS2.Checked == true))
                    {
                    LINKHOS2 = frameoperacionestandar2.Attributes["src"];
                    SETHOS2 = "[OperacionEstandar2] = '" + LINKHOS2 + "',";
                    }

                string LINKDEF = "";
                string SETDEF = "";
                if ((CHECKDEFDEF.Checked == true || CHECKPREVDEF.Checked == true))
                    {
                    LINKDEF = framedefoteca.Attributes["src"];
                    SETDEF = "[Defoteca] = '" + LINKDEF + "',";
                    }

                string LINKEMB = "";
                string SETEMB = "";
                if ((CHECKDEFEMB.Checked == true || CHECKPREVEMB.Checked == true))
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

                string LINKRETRABAJO = "";
                string SETRETRABAJO = "";
                if ((CHECKDEFRTR.Checked == true || CHECKPREVRTR.Checked == true))
                    {
                    LINKRETRABAJO = frameretrabajo.Attributes["src"];
                    SETRETRABAJO = "[PautaRetrabajo] = '" + LINKRETRABAJO + "',";
                    }

                string LINKVIDEOAUX = "";
                string SETVIDEOAUX = "";
                if ((CHECKDEFVID.Checked == true || CHECKPREVVID.Checked == true))
                    {
                    LINKVIDEOAUX = framevideoaux.Attributes["src"];
                    SETVIDEOAUX = "[VideoAuxiliar] = '" + LINKVIDEOAUX + "',";
                    }

                string NOCONFORMIDAD = "";
                string SETNOCONFORMIDAD = "";
                if ((CHECKDEFNC.Checked == true || CHECKPREVNC.Checked == true))
                    {
                    NOCONFORMIDAD = frameNoConformidad.Attributes["src"];
                    SETNOCONFORMIDAD = "[NoConformidades] = '" + NOCONFORMIDAD + "',";
                    }
                string cambios = "[RazonModificacion] = 'Se ha modificado" + txtPlanControl.Text + txtPautaControl.Text + txtOperacionEstandar.Text + txtDefoteca.Text + txtEmbalaje.Text + ".',";
                string observaciones = "[Observaciones] = '" + tbObservacionesCarga.Text + "',";
                Conexion_DOCUMENTAL conexion = new Conexion_DOCUMENTAL();
                //conexion.insertar_documentos_molde(tbMolde.Text, LINKPLANCONTROL, LINKPAUTACONTROL, LINKRECEPCION, LINKHOS, LINKGP12, LINKHOS2, LINKDEF, LINKEMB, LINKRETRABAJO, LINKVIDEOAUX, tbObservacionesCarga.Text, NOCONFORMIDAD);
                conexion.insertar_documentos_moldeBOTON(tbMolde.Text, SETPLANCONTROL, SETPAUTACONTROL, SETRECEPCION, SETHOS, SETGP12, SETHOS2, SETDEF, SETEMB, SETRETRABAJO, SETVIDEOAUX, observaciones, SETNOCONFORMIDAD, SETIMG, DateTime.Now.ToString(),cambios);
                resetearcajas();

            }
            catch (Exception)
            {
            }
        }

        public void resetearcajas()
        {
            try
            {
                CHECKIMG.Checked = false;
                CHECKDEFPC.Checked = false;
                CHECKDEFPAC.Checked = false;
                CHECKDEFPREC.Checked = false;
                CHECKDEFHOS.Checked = false;
                CHECKDEFGP12.Checked = false;
                CHECKDEFHOS2.Checked = false;
                CHECKDEFDEF.Checked = false;
                CHECKDEFEMB.Checked = false;
                CHECKDEFRTR.Checked = false;
                CHECKDEFVID.Checked = false;
                CHECKDEFNC.Checked = false;

                CHECKPREVPC.Checked = false;
                CHECKPREVPAC.Checked = false;
                CHECKPREVREC.Checked = false;
                CHECKPREVHOS.Checked = false;
                CHECKPREVGP12.Checked = false;
                CHECKPREVHOS2.Checked = false;
                CHECKPREVDEF.Checked = false;
                CHECKPREVEMB.Checked = false;
                CHECKPREVRTR.Checked = false;
                CHECKPREVVID.Checked = false;
                CHECKPREVNC.Checked = false;
            }
            catch (Exception) { }
        }
    }

}