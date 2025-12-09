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


namespace ThermoWeb.PRODUCCION
{
    public partial class FichasParametros : System.Web.UI.Page
    {
        private string selectedValueMaquina = "";
        private string referenciaprevia = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Ajustar_tamaño_campos();
                Desactivar_campos();

                //Cargo el listado de manos en el desplegable
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                DataTable ds_Manos = SHConexion.Devuelve_listado_MANOS_SMARTH(" AND AREA <> 1", " ORDER BY MANO");
                for (int i = 0; i <= ds_Manos.Rows.Count - 1; i++)
                {
                    ListaManosAsignacion.InnerHtml = ListaManosAsignacion.InnerHtml + System.Environment.NewLine +
                    String.Format("<option value='{0}'>", ds_Manos.Rows[i][0]);
                }

                //Cargo datos desde URL
                if (Request.QueryString["REFERENCIA"] != null)
                {
                    CancelarModificacion(null, null);
                    tbFicha.Value = Request.QueryString["REFERENCIA"].ToString();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "AuxCargaFichaModificada(" + Request.QueryString["REFERENCIA"].ToString() + "," + Request.QueryString["MAQUINA"].ToString() + ");", true);
                }

            }

        }

        public void CargarFicha(object sender, EventArgs e)
        {
            CargarFicha_function();
            Desactivar_campos();
            //btnImportarMaquina.Visible = false;
            btnModificar.Visible = true;
            btnGuardar.Visible = false;
            btnCancelar.Visible = false;
            btnImprimir.Visible = true;
        }


        //TAMAÑOS, ACTIVACIONES Y FORMATOS
        private void Ajustar_tamaño_campos()
        {
            try
            {
                //defino sources de imagenes--reposicionar
                //img1.Src = "../SMARTH_docs/PARAMETROS/sin_imagen.jpg";
                //img5.Src = "../SMARTH_docs/PARAMETROS/sin_imagen.jpg";
                hyperlink5.NavigateUrl = "../SMARTH_docs/PARAMETROS/sin_imagen.jpg";
                hyperlink5.ImageUrl = "../SMARTH_docs/PARAMETROS/sin_imagen.jpg";
                hyperlink1.NavigateUrl = "../SMARTH_docs/PARAMETROS/sin_imagen.jpg";
                hyperlink1.ImageUrl = "../SMARTH_docs/PARAMETROS/sin_imagen.jpg";
                hyperlink2.NavigateUrl = "../SMARTH_docs/PARAMETROS/sin_imagen.jpg";
                hyperlink2.ImageUrl = "../SMARTH_docs/PARAMETROS/sin_imagen.jpg";
                hyperlink3.NavigateUrl = "../SMARTH_docs/PARAMETROS/sin_imagen.jpg";
                hyperlink3.ImageUrl = "../SMARTH_docs/PARAMETROS/sin_imagen.jpg";
                hyperlink4.NavigateUrl = "../SMARTH_docs/PARAMETROS/sin_imagen.jpg";
                hyperlink4.ImageUrl = "../SMARTH_docs/PARAMETROS/sin_imagen.jpg";
                hyperlink6.NavigateUrl = "../SMARTH_docs/PARAMETROS/sin_imagen.jpg";
                hyperlink6.ImageUrl = "../SMARTH_docs/PARAMETROS/sin_imagen.jpg";
                hyperlink7.NavigateUrl = "../SMARTH_docs/PARAMETROS/sin_imagen.jpg";
                hyperlink7.ImageUrl = "../SMARTH_docs/PARAMETROS/sin_imagen.jpg";
                hyperlink8.NavigateUrl = "../SMARTH_docs/PARAMETROS/sin_imagen.jpg";
                hyperlink8.ImageUrl = "../SMARTH_docs/PARAMETROS/sin_imagen.jpg";

                //principal


            }
            catch (Exception)
            {

            }
        }

        private void Desactivar_campos()
        {
            try
            {
                FileUpload1.Visible = false;
                FileUpload2.Visible = false;
                FileUpload3.Visible = false;
                FileUpload4.Visible = false;
                FileUpload5.Visible = false;
                FileUpload6.Visible = false;
                FileUpload7.Visible = false;
                FileUpload8.Visible = false;
                Botonsubida1.Visible = false;
                Botonsubida2.Visible = false;
                Botonsubida3.Visible = false;
                Botonsubida4.Visible = false;
                Botonsubida5.Visible = false;
                Botonsubida6.Visible = false;
                Botonsubida7.Visible = false;
                Botonsubida8.Visible = false;
                tbFuerzaCierre.Enabled = false;

                tbReferencia.Enabled = false;
                tbReferencia.Visible = false;
                tbFicha.Visible = true;

                tbNombre.Enabled = false;
                tbCliente.Enabled = false;
                tbCodigoMolde.Enabled = false;
                tbMaquina.Enabled = false;
                tbMaquina.Visible = false;
                lista_maquinas.Visible = true;//test
               
                tbAutomatico.Enabled = false;
                tbManual.Enabled = false;
                tbPersonal.Enabled = false;
                DropModoSelect.Enabled = false;
                tbProgramaMolde.Enabled = false;
                tbProgramaRobot.Enabled = false;
                tbManoRobot.Enabled = false;
                tbAperturaMaquina.Enabled = false;
                tbCavidades.Enabled = false;
                tbPesoPieza.Enabled = false;
                tbPesoColada.Enabled = false;
                tbPesoTotal.Enabled = false;

                cbEdicion.Enabled = false;
                cbEdicion.Visible = false;
                lista_versiones.Visible = true;

                cbFecha.Enabled = false;
                cbElaboradoPor.Enabled = false;
                //cbRevisadoPor.Enabled = false;
                //cbAprobadoPor.Enabled = false;
                tbObservaciones.Enabled = false;
                tbRazones.Enabled = false;

                recalclimthCarga.Visible = false;
                recalclimthCiclo.Visible = false;
                recalclimthCojin.Visible = false;
                recalclimthContrapr.Visible = false;
                recalclimthDescomp.Visible = false;
                recalclimthTiempo.Visible = false;
                recalclimthVCarga.Visible = false;
                recalcthEnfriamiento.Visible = false;
                recalfull.Visible = false;
                reclimLimitePresion.Visible = false;
                reclimtbConmutacion.Visible = false;
                reclimtbTiempoPresion.Visible = false;
                reclimTiempoInyeccion.Visible = false;
                /*thCodMaterial.Enabled = false;
                thMaterial.Enabled = false;
                thCodColorante.Enabled = false;
                thColorante.Enabled = false;
                thColor.Enabled = false;
                thTempSecado.Enabled = false;
                thTiempoSecado.Enabled = false;
                thCodMaterial2.Enabled = false;
                thMaterial2.Enabled = false;
                thCodColorante2.Enabled = false;
                thColorante2.Enabled = false;
                thColor2.Enabled = false;
                thTempSecado2.Enabled = false;
                thTiempoSecado2.Enabled = false;*/

                thBoq.Enabled = false;
                thBoq.CssClass = "cuerpo";
                //thBoq.CssClass = "border border-0 bg-transparent form-control text-dark";
                thT1.Enabled = false;
                thT1.CssClass = "cuerpo";
                thT2.Enabled = false;
                thT2.CssClass = "cuerpo";
                thT3.Enabled = false;
                thT3.CssClass = "cuerpo";
                thT4.Enabled = false;
                thT4.CssClass = "cuerpo";
                thT5.Enabled = false;
                thT5.CssClass = "cuerpo";
                thT6.Enabled = false;
                thT6.CssClass = "cuerpo";
                thT7.Enabled = false;
                thT7.CssClass = "cuerpo";
                thT8.Enabled = false;
                thT8.CssClass = "cuerpo";
                thT9.Enabled = false;
                thT9.CssClass = "cuerpo";
                thT10.Enabled = false;
                thT10.CssClass = "cuerpo";


                TbTOLCilindro.Enabled = false;
                TbTOLCilindro.CssClass = "border border-dark border-start-0";
                tbTOLCamCaliente.Enabled = false;
                tbTOLCamCaliente.CssClass = "border border-dark border-start-0";
                tbTOLCarInyeccion.Enabled = false;
                tbTOLCarInyeccion.CssClass = "border border-dark border-start-0";
                tbTOLSecCota.Enabled = false;
                tbTOLSecCota.CssClass = "border border-dark border-start-0";
                tbTOLSecTiempo.Enabled = false;
                tbTOLSecTiempo.CssClass = "border border-dark border-start-0";
                tbTOLPostPresion.Enabled = false;
                tbTOLPostPresion.CssClass = "border border-dark border-start-0";
                //tbFijaRefrigeracion.Enabled = false;
                //tbFijaAtempCircuito.Enabled = false;
                //tbAtempTemperatura.Enabled = false;

                thZ1.Enabled = false;
                thZ1.CssClass = "cuerpo";
                thZ2.Enabled = false;
                thZ2.CssClass = "cuerpo";
                thZ3.Enabled = false;
                thZ3.CssClass = "cuerpo";
                thZ4.Enabled = false;
                thZ4.CssClass = "cuerpo";
                thZ5.Enabled = false;
                thZ5.CssClass = "cuerpo";
                thZ6.Enabled = false;
                thZ6.CssClass = "cuerpo";
                thZ7.Enabled = false;
                thZ7.CssClass = "cuerpo";
                thZ8.Enabled = false;
                thZ8.CssClass = "cuerpo";
                thZ9.Enabled = false;
                thZ9.CssClass = "cuerpo";
                thZ10.Enabled = false;
                thZ10.CssClass = "cuerpo";

                thZ11.Enabled = false;
                thZ11.CssClass = "cuerpo";
                thZ12.Enabled = false;
                thZ12.CssClass = "cuerpo";
                thZ13.Enabled = false;
                thZ13.CssClass = "cuerpo";
                thZ14.Enabled = false;
                thZ14.CssClass = "cuerpo";
                thZ15.Enabled = false;
                thZ15.CssClass = "cuerpo";
                thZ16.Enabled = false;
                thZ16.CssClass = "cuerpo";
                thZ17.Enabled = false;
                thZ17.CssClass = "cuerpo";
                thZ18.Enabled = false;
                thZ18.CssClass = "cuerpo";
                thZ19.Enabled = false;
                thZ19.CssClass = "cuerpo";
                thZ20.Enabled = false;
                thZ20.CssClass = "cuerpo";
                
               //tbMovilRefrigeracion.Enabled = false;
               //tbMovilAtempCircuito.Enabled = false;
               //tbMovilAtempTemperatura.Enabled = false;

                thV1.Enabled = false;
                thV1.CssClass = "cuerpo";
                thV2.Enabled = false;
                thV2.CssClass = "cuerpo";
                thV3.Enabled = false;
                thV3.CssClass = "cuerpo";
                thV4.Enabled = false;
                thV4.CssClass = "cuerpo";
                thV5.Enabled = false;
                thV5.CssClass = "cuerpo";
                thV6.Enabled = false;
                thV6.CssClass = "cuerpo";
                thV7.Enabled = false;
                thV7.CssClass = "cuerpo";
                thV8.Enabled = false;
                thV8.CssClass = "cuerpo";
                thV9.Enabled = false;
                thV9.CssClass = "cuerpo";
                thV10.Enabled = false;
                thV10.CssClass = "cuerpo";
                thV11.Enabled = false;
                thV11.CssClass = "cuerpo";
                thV12.Enabled = false;
                thV12.CssClass = "cuerpo";

                //tbVelocidad.CssClass = "form-control form-control-sm bg-secondary text-white border border-dark border-start-0  border-bottom-0";
                //tbVelUNIT.CssClass = "bg-secondary text-white border border-dark border-end-0 border-bottom-0";

                thC1.Enabled = false;
                thC1.CssClass = "cuerpo";
                thC2.Enabled = false;
                thC2.CssClass = "cuerpo";
                thC3.Enabled = false;
                thC3.CssClass = "cuerpo";
                thC4.Enabled = false;
                thC4.CssClass = "cuerpo";
                thC5.Enabled = false;
                thC5.CssClass = "cuerpo";
                thC6.Enabled = false;
                thC6.CssClass = "cuerpo";
                thC7.Enabled = false;
                thC7.CssClass = "cuerpo";
                thC8.Enabled = false;
                thC8.CssClass = "cuerpo";
                thC9.Enabled = false;
                thC9.CssClass = "cuerpo";
                thC10.Enabled = false;
                thC10.CssClass = "cuerpo";
                thC11.Enabled = false;
                thC11.CssClass = "cuerpo";
                thC12.Enabled = false;
                thC12.CssClass = "cuerpo";
                
                


                tbTiempoInyeccion.Enabled = false;
                tbTiempoInyeccion.CssClass = "cuerpo";
                tbTiempoInyeccionNVal.Enabled = false;
                tbTiempoInyeccionNVal.CssClass = "cuerpo";
                tbTiempoInyeccionMVal.Enabled = false;
                tbTiempoInyeccionMVal.CssClass = "cuerpo";


                tbLimitePresion.Enabled = false;
                tbLimitePresion.CssClass = "cuerpo";
                tbLimitePresionNVal.Enabled = false;
                tbLimitePresionNVal.CssClass = "cuerpo";
                tbLimitePresionMval.Enabled = false;
                tbLimitePresionMval.CssClass = "cuerpo";

                tbLimitePresionReal.Enabled = false;
                tbLimitePresionReal.CssClass = "cuerpo";
                tbLimitePresionNRealVal.Enabled = false;
                tbLimitePresionNRealVal.CssClass = "cuerpo";
                tbLimitePresionMRealVal.Enabled = false;
                tbLimitePresionMRealVal.CssClass = "cuerpo";

                thP1.Enabled = false;
                thP1.CssClass = "cuerpo";
                thP2.Enabled = false;
                thP2.CssClass = "cuerpo";
                thP3.Enabled = false;
                thP3.CssClass = "cuerpo";
                thP4.Enabled = false;
                thP4.CssClass = "cuerpo";
                thP5.Enabled = false;
                thP5.CssClass = "cuerpo";
                thP6.Enabled = false;
                thP6.CssClass = "cuerpo";
                thP7.Enabled = false;
                thP7.CssClass = "cuerpo";
                thP8.Enabled = false;
                thP8.CssClass = "cuerpo";
                thP9.Enabled = false;
                thP9.CssClass = "cuerpo";
                thP10.Enabled = false;
                thP10.CssClass = "cuerpo";

                thTP1.Enabled = false;
                thTP1.CssClass = "cuerpo";
                thTP2.Enabled = false;
                thTP2.CssClass = "cuerpo";
                thTP3.Enabled = false;
                thTP3.CssClass = "cuerpo";
                thTP4.Enabled = false;
                thTP4.CssClass = "cuerpo";
                thTP5.Enabled = false;
                thTP5.CssClass = "cuerpo";
                thTP6.Enabled = false;
                thTP6.CssClass = "cuerpo";
                thTP7.Enabled = false;
                thTP7.CssClass = "cuerpo";
                thTP8.Enabled = false;
                thTP8.CssClass = "cuerpo";
                thTP9.Enabled = false;
                thTP9.CssClass = "cuerpo";
                thTP10.Enabled = false;
                thTP10.CssClass = "cuerpo";
                

                tbConmutacion.Enabled = false;
                tbConmutacion.CssClass = "cuerpo";
                thConmuntaciontolNVal.Enabled = false;
                thConmuntaciontolNVal.CssClass = "cuerpo";
                thConmuntaciontolMVal.Enabled = false;
                thConmuntaciontolMVal.CssClass = "cuerpo";

                tbTiempoPresion.Enabled = false;
                tbTiempoPresion.CssClass = "cuerpo";
                tbTiempoPresiontolNVal.Enabled = false;
                tbTiempoPresiontolNVal.CssClass = "cuerpo";
                tbTiempoPresiontolMVal.Enabled = false;
                tbTiempoPresiontolMVal.CssClass = "cuerpo";

                thVCarga.Enabled = false;
                thVCarga.CssClass = "cuerpo";
                TNvcargaval.Enabled = false;
                TNvcargaval.CssClass = "cuerpo";
                TMvcargaval.Enabled = false;
                TMvcargaval.CssClass = "cuerpo";


                thCarga.Enabled = false;
                thCarga.CssClass = "cuerpo";
                TNcargaval.Enabled = false;
                TNcargaval.CssClass = "cuerpo";
                TMcargaval.Enabled = false;
                TMcargaval.CssClass = "cuerpo";

                thDescomp.Enabled = false;
                thDescomp.CssClass = "cuerpo";
                TNdescomval.Enabled = false;
                TNdescomval.CssClass = "cuerpo";
                TMdescomval.Enabled = false;
                TMdescomval.CssClass = "cuerpo";

                thContrapr.Enabled = false;
                thContrapr.CssClass = "cuerpo";
                TNcontrapval.Enabled = false;
                TNcontrapval.CssClass = "cuerpo";
                TMcontrapval.Enabled = false;
                TMcontrapval.CssClass = "cuerpo";

                thCojin.Enabled = false;
                thCojin.CssClass = "cuerpo p-0";
                TNCojinval.Enabled = false;
                TNCojinval.CssClass = "cuerpo p-0";
                TMCojinval.Enabled = false;
                TMCojinval.CssClass = "cuerpo p-0";

                thTiempo.Enabled = false;
                thTiempo.CssClass = "cuerpo";
                TNTiempdosval.Enabled = false;
                TNTiempdosval.CssClass = "cuerpo";
                TMTiempdosval.Enabled = false;
                TMTiempdosval.CssClass = "cuerpo";

                thEnfriamiento.Enabled = false;
                thEnfriamiento.CssClass = "cuerpo";
                TNEnfriamval.Enabled = false;
                TNEnfriamval.CssClass = "cuerpo";
                TMEnfriamval.Enabled = false;
                TMEnfriamval.CssClass = "cuerpo";

                thCiclo.Enabled = false;
                thCiclo.CssClass = "cuerpo";
                TNCicloval.Enabled = false;
                TNCicloval.CssClass = "cuerpo";
                TMCicloval.Enabled = false;
                TMCicloval.CssClass = "cuerpo";

                //Secuenciales control

                seqAbrir1_1.Enabled = false;
                seqAbrir1_1.CssClass = "cuerpo";
                seqAbrir1_2.Enabled = false;
                seqAbrir1_2.CssClass = "cuerpo";
                seqAbrir1_3.Enabled = false;
                seqAbrir1_3.CssClass = "cuerpo";
                seqAbrir1_4.Enabled = false;
                seqAbrir1_4.CssClass = "cuerpo";
                seqAbrir1_5.Enabled = false;
                seqAbrir1_5.CssClass = "cuerpo";
                seqAbrir1_6.Enabled = false;
                seqAbrir1_6.CssClass = "cuerpo";
                seqAbrir1_7.Enabled = false;
                seqAbrir1_7.CssClass = "cuerpo";
                seqAbrir1_8.Enabled = false;
                seqAbrir1_8.CssClass = "cuerpo";
                seqAbrir1_9.Enabled = false;
                seqAbrir1_9.CssClass = "cuerpo";
                seqAbrir1_10.Enabled = false;
                seqAbrir1_10.CssClass = "cuerpo";
                seqCerrar1_1.Enabled = false;
                seqCerrar1_1.CssClass = "cuerpo";
                seqCerrar1_2.Enabled = false;
                seqCerrar1_2.CssClass = "cuerpo";
                seqCerrar1_3.Enabled = false;
                seqCerrar1_3.CssClass = "cuerpo";
                seqCerrar1_4.Enabled = false;
                seqCerrar1_4.CssClass = "cuerpo";
                seqCerrar1_5.Enabled = false;
                seqCerrar1_5.CssClass = "cuerpo";
                seqCerrar1_6.Enabled = false;
                seqCerrar1_6.CssClass = "cuerpo";
                seqCerrar1_7.Enabled = false;
                seqCerrar1_7.CssClass = "cuerpo";
                seqCerrar1_8.Enabled = false;
                seqCerrar1_8.CssClass = "cuerpo";
                seqCerrar1_9.Enabled = false;
                seqCerrar1_9.CssClass = "cuerpo";
                seqCerrar1_10.Enabled = false;
                seqCerrar1_10.CssClass = "cuerpo";
                seqAbrir2_1.Enabled = false;
                seqAbrir2_1.CssClass = "cuerpo";
                seqAbrir2_2.Enabled = false;
                seqAbrir2_2.CssClass = "cuerpo";
                seqAbrir2_3.Enabled = false;
                seqAbrir2_3.CssClass = "cuerpo";
                seqAbrir2_4.Enabled = false;
                seqAbrir2_4.CssClass = "cuerpo";
                seqAbrir2_5.Enabled = false;
                seqAbrir2_5.CssClass = "cuerpo";
                seqAbrir2_6.Enabled = false;
                seqAbrir2_6.CssClass = "cuerpo";
                seqAbrir2_7.Enabled = false;
                seqAbrir2_7.CssClass = "cuerpo";
                seqAbrir2_8.Enabled = false;
                seqAbrir2_8.CssClass = "cuerpo";
                seqAbrir2_9.Enabled = false;
                seqAbrir2_9.CssClass = "cuerpo";
                seqAbrir2_10.Enabled = false;
                seqAbrir2_10.CssClass = "cuerpo";
                seqCerrar2_1.Enabled = false;
                seqCerrar2_1.CssClass = "cuerpo";
                seqCerrar2_2.Enabled = false;
                seqCerrar2_2.CssClass = "cuerpo";
                seqCerrar2_3.Enabled = false;
                seqCerrar2_3.CssClass = "cuerpo";
                seqCerrar2_4.Enabled = false;
                seqCerrar2_4.CssClass = "cuerpo";
                seqCerrar2_5.Enabled = false;
                seqCerrar2_5.CssClass = "cuerpo";
                seqCerrar2_6.Enabled = false;
                seqCerrar2_6.CssClass = "cuerpo";
                seqCerrar2_7.Enabled = false;
                seqCerrar2_7.CssClass = "cuerpo";
                seqCerrar2_8.Enabled = false;
                seqCerrar2_8.CssClass = "cuerpo";
                seqCerrar2_9.Enabled = false;
                seqCerrar2_9.CssClass = "cuerpo";
                seqCerrar2_10.Enabled = false;
                seqCerrar2_10.CssClass = "cuerpo";
                seqTPresPost1.Enabled = false;
                seqTPresPost1.CssClass = "cuerpo";
                seqTPresPost2.Enabled = false;
                seqTPresPost2.CssClass = "cuerpo";
                seqTPresPost3.Enabled = false;
                seqTPresPost3.CssClass = "cuerpo";
                seqTPresPost4.Enabled = false;
                seqTPresPost4.CssClass = "cuerpo";
                seqTPresPost5.Enabled = false;
                seqTPresPost5.CssClass = "cuerpo";
                seqTPresPost6.Enabled = false;
                seqTPresPost6.CssClass = "cuerpo";
                seqTPresPost7.Enabled = false;
                seqTPresPost7.CssClass = "cuerpo";
                seqTPresPost8.Enabled = false;
                seqTPresPost8.CssClass = "cuerpo";
                seqTPresPost9.Enabled = false;
                seqTPresPost9.CssClass = "cuerpo";
                seqTPresPost10.Enabled = false;
                seqTPresPost10.CssClass = "cuerpo";
                

                seqTiempoRetardo_1.Enabled = false;
                seqTiempoRetardo_1.CssClass = "cuerpo";
                seqTiempoRetardo_2.Enabled = false;
                seqTiempoRetardo_2.CssClass = "cuerpo";
                seqTiempoRetardo_3.Enabled = false;
                seqTiempoRetardo_3.CssClass = "cuerpo";
                seqTiempoRetardo_4.Enabled = false;
                seqTiempoRetardo_4.CssClass = "cuerpo";
                seqTiempoRetardo_5.Enabled = false;
                seqTiempoRetardo_5.CssClass = "cuerpo";
                seqTiempoRetardo_6.Enabled = false;
                seqTiempoRetardo_6.CssClass = "cuerpo";
                seqTiempoRetardo_7.Enabled = false;
                seqTiempoRetardo_7.CssClass = "cuerpo";
                seqTiempoRetardo_8.Enabled = false;
                seqTiempoRetardo_8.CssClass = "cuerpo";
                seqTiempoRetardo_9.Enabled = false;
                seqTiempoRetardo_9.CssClass = "cuerpo";
                seqTiempoRetardo_10.Enabled = false;
                seqTiempoRetardo_10.CssClass = "cuerpo";
                

                seqAnotaciones.Enabled = false;
                seqAnotaciones.CssClass = "form-control border border-secondary shadow";
                //secuenciales control
                //tbTiempoSecuencial.Enabled = false;
                //tbPresionSecuencial.Enabled = false;
                //tbTiempoSecuencialN.Enabled = false;
                //tbTiempoSecuencialNVal.Enabled = false;
                //tbTiempoSecuencialM.Enabled = false;
                //tbTiempoSecuencialMVal.Enabled = false;
                //tbPresionSecuencialN.Enabled = false;
                //tbPresionSecuencialNVal.Enabled = false;
                //tbPresionSecuencialM.Enabled = false;
                //tbPresionSecuencialMVal.Enabled = false;


                //ATEMPERADO
                //CIRCUITO FIJO
                AtempTipoF.Enabled = false;
                TbCircuitoF1.Enabled = false;
                TbCircuitoF1.CssClass = "cuerpo";
                TbCircuitoF2.Enabled = false;
                TbCircuitoF2.CssClass = "cuerpo";
                TbCircuitoF3.Enabled = false;
                TbCircuitoF3.CssClass = "cuerpo";
                TbCircuitoF4.Enabled = false;
                TbCircuitoF4.CssClass = "cuerpo";
                TbCircuitoF5.Enabled = false;
                TbCircuitoF5.CssClass = "cuerpo";
                TbCircuitoF6.Enabled = false;
                TbCircuitoF6.CssClass = "cuerpo";
                TbCaudalF1.Enabled = false;
                TbCaudalF1.CssClass = "cuerpo";
                TbCaudalF2.Enabled = false;
                TbCaudalF2.CssClass = "cuerpo";
                TbCaudalF3.Enabled = false;
                TbCaudalF3.CssClass = "cuerpo";
                TbCaudalF4.Enabled = false;
                TbCaudalF4.CssClass = "cuerpo";
                TbCaudalF5.Enabled = false;
                TbCaudalF5.CssClass = "cuerpo";
                TbCaudalF6.Enabled = false;
                TbCaudalF6.CssClass = "cuerpo";
                TbTemperaturaF1.Enabled = false;
                TbTemperaturaF1.CssClass = "cuerpo";
                TbTemperaturaF2.Enabled = false;
                TbTemperaturaF2.CssClass = "cuerpo";
                TbTemperaturaF3.Enabled = false;
                TbTemperaturaF3.CssClass = "cuerpo";
                TbTemperaturaF4.Enabled = false;
                TbTemperaturaF4.CssClass = "cuerpo";
                TbTemperaturaF5.Enabled = false;
                TbTemperaturaF5.CssClass = "cuerpo";
                TbTemperaturaF6.Enabled = false;
                TbTemperaturaF6.CssClass = "cuerpo";
                TbEntradaF1.Enabled = false;
                TbEntradaF1.CssClass = "cuerpo";
                TbEntradaF2.Enabled = false;
                TbEntradaF2.CssClass = "cuerpo";
                TbEntradaF3.Enabled = false;
                TbEntradaF3.CssClass = "cuerpo";
                TbEntradaF4.Enabled = false;
                TbEntradaF4.CssClass = "cuerpo";
                TbEntradaF5.Enabled = false;
                TbEntradaF5.CssClass = "cuerpo";
                TbEntradaF6.Enabled = false;
                TbEntradaF6.CssClass = "cuerpo";

                //CIRCUITO MÓVIL
                AtempTipoM.Enabled = false;
                
                TbCircuitoM1.Enabled = false;
                TbCircuitoM1.CssClass = "cuerpo";
                TbCircuitoM2.Enabled = false;
                TbCircuitoM2.CssClass = "cuerpo";
                TbCircuitoM3.Enabled = false;
                TbCircuitoM3.CssClass = "cuerpo";
                TbCircuitoM4.Enabled = false;
                TbCircuitoM4.CssClass = "cuerpo";
                TbCircuitoM5.Enabled = false;
                TbCircuitoM5.CssClass = "cuerpo";
                TbCircuitoM6.Enabled = false;
                TbCircuitoM6.CssClass = "cuerpo";
                TbCaudalM1.Enabled = false;
                TbCaudalM1.CssClass = "cuerpo";
                TbCaudalM2.Enabled = false;
                TbCaudalM2.CssClass = "cuerpo";
                TbCaudalM3.Enabled = false;
                TbCaudalM3.CssClass = "cuerpo";
                TbCaudalM4.Enabled = false;
                TbCaudalM4.CssClass = "cuerpo";
                TbCaudalM5.Enabled = false;
                TbCaudalM5.CssClass = "cuerpo";
                TbCaudalM6.Enabled = false;
                TbCaudalM6.CssClass = "cuerpo";
                TbTemperaturaM1.Enabled = false;
                TbTemperaturaM1.CssClass = "cuerpo";
                TbTemperaturaM2.Enabled = false;
                TbTemperaturaM2.CssClass = "cuerpo";
                TbTemperaturaM3.Enabled = false;
                TbTemperaturaM3.CssClass = "cuerpo";
                TbTemperaturaM4.Enabled = false;
                TbTemperaturaM4.CssClass = "cuerpo";
                TbTemperaturaM5.Enabled = false;
                TbTemperaturaM5.CssClass = "cuerpo";
                TbTemperaturaM6.Enabled = false;
                TbTemperaturaM6.CssClass = "cuerpo";
                TbEntradaM1.Enabled = false;
                TbEntradaM1.CssClass = "cuerpo";
                TbEntradaM2.Enabled = false;
                TbEntradaM2.CssClass = "cuerpo";
                TbEntradaM3.Enabled = false;
                TbEntradaM3.CssClass = "cuerpo";
                TbEntradaM4.Enabled = false;
                TbEntradaM4.CssClass = "cuerpo";
                TbEntradaM5.Enabled = false;
                TbEntradaM5.CssClass = "cuerpo";
                TbEntradaM6.Enabled = false;
                TbEntradaM6.CssClass = "cuerpo";


                //Desactivo Nuevas tolerancias
                TbTOLCilindro.Enabled = false;
                tbTOLCamCaliente.Enabled = false;
                tbTOLCarInyeccion.Enabled = false;
                tbTOLPostPresion.Enabled = false;
                tbTOLSecCota.Enabled = false;
                tbTOLSecTiempo.Enabled = false;

                //instrucciones de arranque

                TbOperacionText1.Enabled = false;
                TbOperacionText2.Enabled = false;
                TbOperacionText3.Enabled = false;
                TbOperacionText4.Enabled = false;
                TbOperacionText5.Enabled = false;

                cbNoyos.Disabled = true;
                cbHembra.Disabled = true;
                cbMacho.Disabled = true;
                cbAntesExpul.Disabled = true;
                cbAntesApert.Disabled = true;
                cbAntesCierre.Disabled = true;
                cbDespuesCierre.Disabled = true;
                cbOtros1.Disabled = true;
                cbBoquilla.Disabled = true;
                cbCono.Disabled = true;
                cbRadioLarga.Disabled = true;
                cbLibre.Disabled = true;
                cbValvula.Disabled = true;
                cbResistencia.Disabled = true;
                cbOtros2.Disabled = true;
                cbExpulsion.Disabled = true;
                cbHidraulica.Disabled = true;
                cbNeumatica.Disabled = true;
                cbNormal.Disabled = true;
                cbArandela125.Disabled = true;
                cbArandela160.Disabled = true;
                cbArandela200.Disabled = true;
                cbArandela250.Disabled = true;
                MarcasOtrosText.Enabled = false;
                MarcasOtrosText.CssClass = "form-control border border-secondary shadow";

                BTNManoUbi.Disabled = true;
               
            }
            catch (Exception)
            {
            }
        }

        private void Activar_campos()
        {
            try
            {
                FileUpload1.Visible = true;
                FileUpload2.Visible = true;
                FileUpload3.Visible = true;
                FileUpload4.Visible = true;
                FileUpload5.Visible = true;
                FileUpload6.Visible = true;
                FileUpload7.Visible = true;
                FileUpload8.Visible = true;
                Botonsubida1.Visible = true;
                Botonsubida2.Visible = true;
                Botonsubida3.Visible = true;
                Botonsubida4.Visible = true;
                Botonsubida5.Visible = true;
                Botonsubida6.Visible = true;
                Botonsubida7.Visible = true;
                Botonsubida8.Visible = true;

                BTNManoUbi.Disabled = false;

                tbReferencia.Enabled = false;
                tbReferencia.Visible = true;
                tbFicha.Visible = false;
                //tbNombre.Enabled = true;
                tbCliente.Enabled = true;
                tbCodigoMolde.Enabled = true;
                tbMaquina.Enabled = false;
                tbMaquina.Visible = true;
                lista_maquinas.Visible = false;
                //lista_maquinas.Attributes.Add("style", "visibility:collapse");
               
                tbAutomatico.Enabled = true;
                tbManual.Enabled = true;
                tbPersonal.Enabled = true;
                DropModoSelect.Enabled = true;
                tbProgramaMolde.Enabled = true;
                tbProgramaRobot.Enabled = true;
                //tbManoRobot.Enabled = true;
                tbAperturaMaquina.Enabled = true;
                tbCavidades.Enabled = true;
                tbPesoPieza.Enabled = true;
                tbPesoColada.Enabled = true;
                tbPesoTotal.Enabled = true;

                thVCarga.Enabled = true;
                thVCarga.CssClass = "cuerpo-editable";
                thCarga.Enabled = true;
                thCarga.CssClass = "cuerpo-editable";
                thDescomp.Enabled = true;
                thDescomp.CssClass = "cuerpo-editable";
                thContrapr.Enabled = true;
                thContrapr.CssClass = "cuerpo-editable";
                thTiempo.Enabled = true;
                thTiempo.CssClass = "cuerpo-editable";
                thEnfriamiento.Enabled = true;
                thEnfriamiento.CssClass = "cuerpo-editable";
                thCiclo.Enabled = true;
                thCiclo.CssClass = "cuerpo-editable";
                thCojin.Enabled = true;
                thCojin.CssClass = "cuerpo-editable";

                cbEdicion.Enabled = false;
                cbEdicion.Visible = true;
                lista_versiones.Visible = false;
                cbFecha.Enabled = false;
                cbElaboradoPor.Enabled = true;
                cbRevisadoPor.Enabled = true;
                cbAprobadoPor.Enabled = true;
                tbObservaciones.Enabled = true;
                tbRazones.Enabled = true;

                /*thCodMaterial.Enabled = true;
                thMaterial.Enabled = true;
                thCodColorante.Enabled = true;
                thColorante.Enabled = true;
                thColor.Enabled = true;
                thTempSecado.Enabled = true;
                thTiempoSecado.Enabled = true;
                thCodMaterial2.Enabled = true;
                thMaterial2.Enabled = true;
                thCodColorante2.Enabled = true;
                thColorante2.Enabled = true;
                thColor2.Enabled = true;
                thTempSecado2.Enabled = true;
                thTiempoSecado2.Enabled = true;*/

                thBoq.Enabled = true;
                thBoq.CssClass = "cuerpo-editable";
                thT1.Enabled = true;
                thT1.CssClass = "cuerpo-editable";
                thT2.Enabled = true;
                thT2.CssClass = "cuerpo-editable"; 
                thT3.Enabled = true;
                thT3.CssClass = "cuerpo-editable"; 
                thT4.Enabled = true;
                thT4.CssClass = "cuerpo-editable"; 
                thT5.Enabled = true;
                thT5.CssClass = "cuerpo-editable"; 
                thT6.Enabled = true;
                thT6.CssClass = "cuerpo-editable"; 
                thT7.Enabled = true;
                thT7.CssClass = "cuerpo-editable"; 
                thT8.Enabled = true;
                thT8.CssClass = "cuerpo-editable"; 
                thT9.Enabled = true;
                thT9.CssClass = "cuerpo-editable"; 
                thT10.Enabled = true;
                thT10.CssClass = "cuerpo-editable"; 

                //tbFijaRefrigeracion.Enabled = true;
                //tbFijaAtempCircuito.Enabled = true;
                //tbAtempTemperatura.Enabled = true;

                thZ1.Enabled = true;
                thZ1.CssClass = "cuerpo-editable";
                thZ2.Enabled = true;
                thZ2.CssClass = "cuerpo-editable";
                thZ3.Enabled = true;
                thZ3.CssClass = "cuerpo-editable";
                thZ4.Enabled = true;
                thZ4.CssClass = "cuerpo-editable";
                thZ5.Enabled = true;
                thZ5.CssClass = "cuerpo-editable";
                thZ6.Enabled = true;
                thZ6.CssClass = "cuerpo-editable";
                thZ7.Enabled = true;
                thZ7.CssClass = "cuerpo-editable";
                thZ8.Enabled = true;
                thZ8.CssClass = "cuerpo-editable";
                thZ9.Enabled = true;
                thZ9.CssClass = "cuerpo-editable";
                thZ10.Enabled = true;
                thZ10.CssClass = "cuerpo-editable";
                thZ11.Enabled = true;
                thZ11.CssClass = "cuerpo-editable";
                thZ12.Enabled = true;
                thZ12.CssClass = "cuerpo-editable";
                thZ13.Enabled = true;
                thZ13.CssClass = "cuerpo-editable";
                thZ14.Enabled = true;
                thZ14.CssClass = "cuerpo-editable";
                thZ15.Enabled = true;
                thZ15.CssClass = "cuerpo-editable";
                thZ16.Enabled = true;
                thZ16.CssClass = "cuerpo-editable";
                thZ17.Enabled = true;
                thZ17.CssClass = "cuerpo-editable";
                thZ18.Enabled = true;
                thZ18.CssClass = "cuerpo-editable";
                thZ19.Enabled = true;
                thZ19.CssClass = "cuerpo-editable";
                thZ20.Enabled = true;
                thZ20.CssClass = "cuerpo-editable";
                //tbMovilRefrigeracion.Enabled = true;
                //tbMovilAtempCircuito.Enabled = true;
                //tbMovilAtempTemperatura.Enabled = true;

                thV1.Enabled = true;
                thV1.CssClass = "cuerpo-editable";
                thV2.Enabled = true;
                thV2.CssClass = "cuerpo-editable";
                thV3.Enabled = true;
                thV3.CssClass = "cuerpo-editable";
                thV4.Enabled = true;
                thV4.CssClass = "cuerpo-editable";
                thV5.Enabled = true;
                thV5.CssClass = "cuerpo-editable";
                thV6.Enabled = true;
                thV6.CssClass = "cuerpo-editable";
                thV7.Enabled = true;
                thV7.CssClass = "cuerpo-editable";
                thV8.Enabled = true;
                thV8.CssClass = "cuerpo-editable";
                thV9.Enabled = true;
                thV9.CssClass = "cuerpo-editable";
                thV10.Enabled = true;
                thV10.CssClass = "cuerpo-editable";
                thV11.Enabled = true;
                thV11.CssClass = "cuerpo-editable";
                thV12.Enabled = true;
                thV12.CssClass = "cuerpo-editable";
                thC1.Enabled = true;
                thC1.CssClass = "cuerpo-editable";
                thC2.Enabled = true;
                thC2.CssClass = "cuerpo-editable";
                thC3.Enabled = true;
                thC3.CssClass = "cuerpo-editable";
                thC4.Enabled = true;
                thC4.CssClass = "cuerpo-editable";
                thC5.Enabled = true;
                thC5.CssClass = "cuerpo-editable";
                thC6.Enabled = true;
                thC6.CssClass = "cuerpo-editable";
                thC7.Enabled = true;
                thC7.CssClass = "cuerpo-editable";
                thC8.Enabled = true;
                thC8.CssClass = "cuerpo-editable";
                thC9.Enabled = true;
                thC9.CssClass = "cuerpo-editable";
                thC10.Enabled = true;
                thC10.CssClass = "cuerpo-editable";
                thC11.Enabled = true;
                thC11.CssClass = "cuerpo-editable";
                thC12.Enabled = true;
                thC12.CssClass = "cuerpo-editable";
                tbTiempoInyeccion.Enabled = true;
                tbTiempoInyeccion.CssClass = "cuerpo-editable";
                tbLimitePresion.Enabled = true;
                tbLimitePresion.CssClass = "cuerpo-editable";

                tbTiempoInyeccionNVal.Enabled = true;
                tbTiempoInyeccionNVal.CssClass = "cuerpo-editable";
                tbTiempoInyeccionMVal.Enabled = true;
                tbTiempoInyeccionMVal.CssClass = "cuerpo-editable";
                tbLimitePresionNVal.Enabled = true;
                tbLimitePresionNVal.CssClass = "cuerpo-editable";
                tbLimitePresionMval.Enabled = true;
                tbLimitePresionMval.CssClass = "cuerpo-editable";

                tbLimitePresionReal.Enabled = true;
                tbLimitePresionReal.CssClass = "cuerpo-editable";
                tbLimitePresionNRealVal.Enabled = true;
                tbLimitePresionNRealVal.CssClass = "cuerpo-editable";
                tbLimitePresionMRealVal.Enabled = true;
                tbLimitePresionMRealVal.CssClass = "cuerpo-editable";

                thP1.Enabled = true;
                thP1.CssClass = "cuerpo-editable";
                thP2.Enabled = true;
                thP2.CssClass = "cuerpo-editable";
                thP3.Enabled = true;
                thP3.CssClass = "cuerpo-editable";
                thP4.Enabled = true;
                thP4.CssClass = "cuerpo-editable";
                thP5.Enabled = true;
                thP5.CssClass = "cuerpo-editable";
                thP6.Enabled = true;
                thP6.CssClass = "cuerpo-editable";
                thP7.Enabled = true;
                thP7.CssClass = "cuerpo-editable";
                thP8.Enabled = true;
                thP8.CssClass = "cuerpo-editable";
                thP9.Enabled = true;
                thP9.CssClass = "cuerpo-editable";
                thP10.Enabled = true;
                thP10.CssClass = "cuerpo-editable";

                thTP1.Enabled = true;
                thTP1.CssClass = "cuerpo-editable";
                thTP2.Enabled = true;
                thTP2.CssClass = "cuerpo-editable";
                thTP3.Enabled = true;
                thTP3.CssClass = "cuerpo-editable";
                thTP4.Enabled = true;
                thTP4.CssClass = "cuerpo-editable";
                thTP5.Enabled = true;
                thTP5.CssClass = "cuerpo-editable";
                thTP6.Enabled = true;
                thTP6.CssClass = "cuerpo-editable";
                thTP7.Enabled = true;
                thTP7.CssClass = "cuerpo-editable";
                thTP8.Enabled = true;
                thTP8.CssClass = "cuerpo-editable";
                thTP9.Enabled = true;
                thTP9.CssClass = "cuerpo-editable";
                thTP10.Enabled = true;
                thTP10.CssClass = "cuerpo-editable";
                tbConmutacion.Enabled = true;
                tbConmutacion.CssClass = "cuerpo-editable";
                tbTiempoPresion.Enabled = true;
                tbTiempoPresion.CssClass = "cuerpo-editable";


                thConmuntaciontolNVal.Enabled = true;
                thConmuntaciontolNVal.CssClass = "cuerpo-editable";
                thConmuntaciontolMVal.Enabled = true;
                thConmuntaciontolMVal.CssClass = "cuerpo-editable";
                tbTiempoPresiontolNVal.Enabled = true;
                tbTiempoPresiontolNVal.CssClass = "cuerpo-editable";
                tbTiempoPresiontolMVal.Enabled = true;
                tbTiempoPresiontolMVal.CssClass = "cuerpo-editable";

                seqAbrir1_1.Enabled = true;
                seqAbrir1_1.CssClass = "cuerpo-editable";
                seqAbrir1_2.Enabled = true;
                seqAbrir1_2.CssClass = "cuerpo-editable";
                seqAbrir1_3.Enabled = true;
                seqAbrir1_3.CssClass = "cuerpo-editable";
                seqAbrir1_4.Enabled = true;
                seqAbrir1_4.CssClass = "cuerpo-editable";
                seqAbrir1_5.Enabled = true;
                seqAbrir1_5.CssClass = "cuerpo-editable";
                seqAbrir1_6.Enabled = true;
                seqAbrir1_6.CssClass = "cuerpo-editable";
                seqAbrir1_7.Enabled = true;
                seqAbrir1_7.CssClass = "cuerpo-editable";
                seqAbrir1_8.Enabled = true;
                seqAbrir1_8.CssClass = "cuerpo-editable";
                seqAbrir1_9.Enabled = true;
                seqAbrir1_9.CssClass = "cuerpo-editable";
                seqAbrir1_10.Enabled = true;
                seqAbrir1_10.CssClass = "cuerpo-editable";
                seqCerrar1_1.Enabled = true;
                seqCerrar1_1.CssClass = "cuerpo-editable";
                seqCerrar1_2.Enabled = true;
                seqCerrar1_2.CssClass = "cuerpo-editable";
                seqCerrar1_3.Enabled = true;
                seqCerrar1_3.CssClass = "cuerpo-editable";
                seqCerrar1_4.Enabled = true;
                seqCerrar1_4.CssClass = "cuerpo-editable";
                seqCerrar1_5.Enabled = true;
                seqCerrar1_5.CssClass = "cuerpo-editable";
                seqCerrar1_6.Enabled = true;
                seqCerrar1_6.CssClass = "cuerpo-editable";
                seqCerrar1_7.Enabled = true;
                seqCerrar1_7.CssClass = "cuerpo-editable";
                seqCerrar1_8.Enabled = true;
                seqCerrar1_8.CssClass = "cuerpo-editable";
                seqCerrar1_9.Enabled = true;
                seqCerrar1_9.CssClass = "cuerpo-editable";
                seqCerrar1_10.Enabled = true;
                seqCerrar1_10.CssClass = "cuerpo-editable";


                seqAbrir2_1.Enabled = true;
                seqAbrir2_1.CssClass = "cuerpo-editable";
                seqAbrir2_2.Enabled = true;
                seqAbrir2_2.CssClass = "cuerpo-editable";
                seqAbrir2_3.Enabled = true;
                seqAbrir2_3.CssClass = "cuerpo-editable";
                seqAbrir2_4.Enabled = true;
                seqAbrir2_4.CssClass = "cuerpo-editable";
                seqAbrir2_5.Enabled = true;
                seqAbrir2_5.CssClass = "cuerpo-editable";
                seqAbrir2_6.Enabled = true;
                seqAbrir2_6.CssClass = "cuerpo-editable";
                seqAbrir2_7.Enabled = true;
                seqAbrir2_7.CssClass = "cuerpo-editable";
                seqAbrir2_8.Enabled = true;
                seqAbrir2_8.CssClass = "cuerpo-editable";
                seqAbrir2_9.Enabled = true;
                seqAbrir2_9.CssClass = "cuerpo-editable";
                seqAbrir2_10.Enabled = true;
                seqAbrir2_10.CssClass = "cuerpo-editable";

                seqCerrar2_1.Enabled = true;
                seqCerrar2_1.CssClass = "cuerpo-editable";
                seqCerrar2_2.Enabled = true;
                seqCerrar2_2.CssClass = "cuerpo-editable";
                seqCerrar2_3.Enabled = true;
                seqCerrar2_3.CssClass = "cuerpo-editable";
                seqCerrar2_4.Enabled = true;
                seqCerrar2_4.CssClass = "cuerpo-editable";
                seqCerrar2_5.Enabled = true;
                seqCerrar2_5.CssClass = "cuerpo-editable";
                seqCerrar2_6.Enabled = true;
                seqCerrar2_6.CssClass = "cuerpo-editable";
                seqCerrar2_7.Enabled = true;
                seqCerrar2_7.CssClass = "cuerpo-editable";
                seqCerrar2_8.Enabled = true;
                seqCerrar2_8.CssClass = "cuerpo-editable";
                seqCerrar2_9.Enabled = true;
                seqCerrar2_9.CssClass = "cuerpo-editable";
                seqCerrar2_10.Enabled = true;
                seqCerrar2_10.CssClass = "cuerpo-editable";

                seqTPresPost1.Enabled = true;
                seqTPresPost1.CssClass = "cuerpo-editable";
                seqTPresPost2.Enabled = true;
                seqTPresPost2.CssClass = "cuerpo-editable";
                seqTPresPost3.Enabled = true;
                seqTPresPost3.CssClass = "cuerpo-editable";
                seqTPresPost4.Enabled = true;
                seqTPresPost4.CssClass = "cuerpo-editable";
                seqTPresPost5.Enabled = true;
                seqTPresPost5.CssClass = "cuerpo-editable";
                seqTPresPost6.Enabled = true;
                seqTPresPost6.CssClass = "cuerpo-editable";
                seqTPresPost7.Enabled = true;
                seqTPresPost7.CssClass = "cuerpo-editable";
                seqTPresPost8.Enabled = true;
                seqTPresPost8.CssClass = "cuerpo-editable";
                seqTPresPost9.Enabled = true;
                seqTPresPost9.CssClass = "cuerpo-editable";
                seqTPresPost10.Enabled = true;
                seqTPresPost10.CssClass = "cuerpo-editable"; ;

                seqTiempoRetardo_1.Enabled = true;
                seqTiempoRetardo_1.CssClass = "cuerpo-editable";
                seqTiempoRetardo_2.Enabled = true;
                seqTiempoRetardo_2.CssClass = "cuerpo-editable";
                seqTiempoRetardo_3.Enabled = true;
                seqTiempoRetardo_3.CssClass = "cuerpo-editable";
                seqTiempoRetardo_4.Enabled = true;
                seqTiempoRetardo_4.CssClass = "cuerpo-editable";
                seqTiempoRetardo_5.Enabled = true;
                seqTiempoRetardo_5.CssClass = "cuerpo-editable";
                seqTiempoRetardo_6.Enabled = true;
                seqTiempoRetardo_6.CssClass = "cuerpo-editable";
                seqTiempoRetardo_7.Enabled = true;
                seqTiempoRetardo_7.CssClass = "cuerpo-editable";
                seqTiempoRetardo_8.Enabled = true;
                seqTiempoRetardo_8.CssClass = "cuerpo-editable";
                seqTiempoRetardo_9.Enabled = true;
                seqTiempoRetardo_9.CssClass = "cuerpo-editable";
                seqTiempoRetardo_10.Enabled = true;
                seqTiempoRetardo_10.CssClass = "cuerpo-editable";
                seqAnotaciones.Enabled = true;
                seqAnotaciones.CssClass = "border border-2 rounded rounded-1 border-dark";



                //tbTiempoSecuencial.Enabled = true;
                //tbPresionSecuencial.Enabled = true;
                //tbTiempoSecuencialN.Enabled = false;
                //tbTiempoSecuencialNVal.Enabled = true;
                //tbTiempoSecuencialM.Enabled = false;
                //tbTiempoSecuencialMVal.Enabled = true;
                //tbPresionSecuencialN.Enabled = false;
                //tbPresionSecuencialNVal.Enabled = true;
                //tbPresionSecuencialM.Enabled = false;
                //tbPresionSecuencialMVal.Enabled = true;

                //ATEMPERADO
                AtempTipoF.Enabled = true;
                //AtempTipoF.CssClass = "cuerpo-editable";
                TbCircuitoF1.Enabled = true;
                TbCircuitoF1.CssClass = "cuerpo-editable";
                TbCircuitoF2.Enabled = true;
                TbCircuitoF2.CssClass = "cuerpo-editable";
                TbCircuitoF3.Enabled = true;
                TbCircuitoF3.CssClass = "cuerpo-editable";
                TbCircuitoF4.Enabled = true;
                TbCircuitoF4.CssClass = "cuerpo-editable";
                TbCircuitoF5.Enabled = true;
                TbCircuitoF5.CssClass = "cuerpo-editable";
                TbCircuitoF6.Enabled = true;
                TbCircuitoF6.CssClass = "cuerpo-editable";

                TbCaudalF1.Enabled = true;
                TbCaudalF1.CssClass = "cuerpo-editable";
                TbCaudalF2.Enabled = true;
                TbCaudalF2.CssClass = "cuerpo-editable";
                TbCaudalF3.Enabled = true;
                TbCaudalF3.CssClass = "cuerpo-editable";
                TbCaudalF4.Enabled = true;
                TbCaudalF4.CssClass = "cuerpo-editable";
                TbCaudalF5.Enabled = true;
                TbCaudalF5.CssClass = "cuerpo-editable";
                TbCaudalF6.Enabled = true;
                TbCaudalF6.CssClass = "cuerpo-editable";

                TbTemperaturaF1.Enabled = true;
                TbTemperaturaF1.CssClass = "cuerpo-editable";
                TbTemperaturaF2.Enabled = true;
                TbTemperaturaF2.CssClass = "cuerpo-editable";
                TbTemperaturaF3.Enabled = true;
                TbTemperaturaF3.CssClass = "cuerpo-editable";
                TbTemperaturaF4.Enabled = true;
                TbTemperaturaF4.CssClass = "cuerpo-editable";
                TbTemperaturaF5.Enabled = true;
                TbTemperaturaF5.CssClass = "cuerpo-editable";
                TbTemperaturaF6.Enabled = true;
                TbTemperaturaF6.CssClass = "cuerpo-editable";

                TbEntradaF1.Enabled = true;
                TbEntradaF1.CssClass = "cuerpo-editable";
                TbEntradaF2.Enabled = true;
                TbEntradaF2.CssClass = "cuerpo-editable";
                TbEntradaF3.Enabled = true;
                TbEntradaF3.CssClass = "cuerpo-editable";
                TbEntradaF4.Enabled = true;
                TbEntradaF4.CssClass = "cuerpo-editable";
                TbEntradaF5.Enabled = true;
                TbEntradaF5.CssClass = "cuerpo-editable";
                TbEntradaF6.Enabled = true;
                TbEntradaF6.CssClass = "cuerpo-editable";


                AtempTipoM.Enabled = true;
                //AtempTipoM.CssClass = "cuerpo-editable";
                TbCircuitoM1.Enabled = true;
                TbCircuitoM1.CssClass = "cuerpo-editable";
                TbCircuitoM2.Enabled = true;
                TbCircuitoM2.CssClass = "cuerpo-editable";
                TbCircuitoM3.Enabled = true;
                TbCircuitoM3.CssClass = "cuerpo-editable";
                TbCircuitoM4.Enabled = true;
                TbCircuitoM4.CssClass = "cuerpo-editable";
                TbCircuitoM5.Enabled = true;
                TbCircuitoM5.CssClass = "cuerpo-editable";
                TbCircuitoM6.Enabled = true;
                TbCircuitoM6.CssClass = "cuerpo-editable";

                TbCaudalM1.Enabled = true;
                TbCaudalM1.CssClass = "cuerpo-editable";
                TbCaudalM2.Enabled = true;
                TbCaudalM2.CssClass = "cuerpo-editable";
                TbCaudalM3.Enabled = true;
                TbCaudalM3.CssClass = "cuerpo-editable";
                TbCaudalM4.Enabled = true;
                TbCaudalM4.CssClass = "cuerpo-editable";
                TbCaudalM5.Enabled = true;
                TbCaudalM5.CssClass = "cuerpo-editable";
                TbCaudalM6.Enabled = true;
                TbCaudalM6.CssClass = "cuerpo-editable";

                TbTemperaturaM1.Enabled = true;
                TbTemperaturaM1.CssClass = "cuerpo-editable";
                TbTemperaturaM2.Enabled = true;
                TbTemperaturaM2.CssClass = "cuerpo-editable";
                TbTemperaturaM3.Enabled = true;
                TbTemperaturaM3.CssClass = "cuerpo-editable";
                TbTemperaturaM4.Enabled = true;
                TbTemperaturaM4.CssClass = "cuerpo-editable";
                TbTemperaturaM5.Enabled = true;
                TbTemperaturaM5.CssClass = "cuerpo-editable";
                TbTemperaturaM6.Enabled = true;
                TbTemperaturaM6.CssClass = "cuerpo-editable";

                TbEntradaM1.Enabled = true;
                TbEntradaM1.CssClass = "cuerpo-editable";
                TbEntradaM2.Enabled = true;
                TbEntradaM2.CssClass = "cuerpo-editable";
                TbEntradaM3.Enabled = true;
                TbEntradaM3.CssClass = "cuerpo-editable";
                TbEntradaM4.Enabled = true;
                TbEntradaM4.CssClass = "cuerpo-editable";
                TbEntradaM5.Enabled = true;
                TbEntradaM5.CssClass = "cuerpo-editable";
                TbEntradaM6.Enabled = true;
                TbEntradaM6.CssClass = "cuerpo-editable";

                TMvcargaval.Enabled = true;
                TMvcargaval.CssClass = "cuerpo-editable";
                TNvcargaval.Enabled = true;
                TNvcargaval.CssClass = "cuerpo-editable";
                TMcargaval.Enabled = true;
                TMcargaval.CssClass = "cuerpo-editable";
                TNcargaval.Enabled = true;
                TNcargaval.CssClass = "cuerpo-editable";
                TMdescomval.Enabled = true;
                TMdescomval.CssClass = "cuerpo-editable";
                TNdescomval.Enabled = true;
                TNdescomval.CssClass = "cuerpo-editable";
                TMcontrapval.Enabled = true;
                TMcontrapval.CssClass = "cuerpo-editable";
                TNcontrapval.Enabled = true;
                TNcontrapval.CssClass = "cuerpo-editable";
                TMTiempdosval.Enabled = true;
                TMTiempdosval.CssClass = "cuerpo-editable";
                TNTiempdosval.Enabled = true;
                TNTiempdosval.CssClass = "cuerpo-editable";
                TMEnfriamval.Enabled = true;
                TMEnfriamval.CssClass = "cuerpo-editable";
                TNEnfriamval.Enabled = true;
                TNEnfriamval.CssClass = "cuerpo-editable";
                TMCicloval.Enabled = true;
                TMCicloval.CssClass = "cuerpo-editable";
                TNCicloval.Enabled = true;
                TNCicloval.CssClass = "cuerpo-editable";
                TMCojinval.Enabled = true;
                TMCojinval.CssClass = "cuerpo-editable";
                TNCojinval.Enabled = true;
                TNCojinval.CssClass = "cuerpo-editable";

                //Activo Nuevas tolerancias
                TbTOLCilindro.Enabled = true;
                TbTOLCilindro.CssClass = "cuerpo-editable";
                tbTOLCamCaliente.Enabled = true;
                tbTOLCamCaliente.CssClass = "cuerpo-editable";
                tbTOLCarInyeccion.Enabled = true;
                tbTOLCarInyeccion.CssClass = "cuerpo-editable";
                tbTOLPostPresion.Enabled = true;
                tbTOLPostPresion.CssClass = "cuerpo-editable";
                tbTOLSecCota.Enabled = true;
                tbTOLSecCota.CssClass = "cuerpo-editable";
                tbTOLSecTiempo.Enabled = true;
                tbTOLSecTiempo.CssClass = "cuerpo-editable";


                //instrucciones de arranque
                TbOperacionText1.Enabled = true;
                TbOperacionText2.Enabled = true;
                TbOperacionText3.Enabled = true;
                TbOperacionText4.Enabled = true;
                TbOperacionText5.Enabled = true;

                cbNoyos.Disabled = false;
                cbHembra.Disabled = false;
                cbMacho.Disabled = false;
                cbAntesExpul.Disabled = false;
                cbAntesApert.Disabled = false;
                cbAntesCierre.Disabled = false;
                cbDespuesCierre.Disabled = false;
                cbOtros1.Disabled = false;
                cbBoquilla.Disabled = false;
                cbCono.Disabled = false;
                cbRadioLarga.Disabled = false;
                cbLibre.Disabled = false;
                cbValvula.Disabled = false;
                cbResistencia.Disabled = false;
                cbOtros2.Disabled = false;
                cbExpulsion.Disabled = false;
                cbHidraulica.Disabled = false;
                cbNeumatica.Disabled = false;
                cbNormal.Disabled = false;
                cbArandela125.Disabled = false;
                cbArandela160.Disabled = false;
                cbArandela200.Disabled = false;
                cbArandela250.Disabled = false;
                MarcasOtrosText.Enabled = true;
                MarcasOtrosText.CssClass = "border border-2 rounded rounded-1 border-dark";
                tbFuerzaCierre.Enabled = true;



                recalclimthCarga.Visible = true;
                recalclimthCiclo.Visible = true;
                recalclimthCojin.Visible = true;
                recalclimthContrapr.Visible = true;
                recalclimthDescomp.Visible = true;
                recalclimthTiempo.Visible = true;
                recalclimthVCarga.Visible = true;
                recalcthEnfriamiento.Visible = true;
                recalfull.Visible = true;
                reclimLimitePresion.Visible = true;
                reclimtbConmutacion.Visible = true;
                reclimtbTiempoPresion.Visible = true;
                reclimTiempoInyeccion.Visible = true;
            }
            catch (Exception)
            {
            }
        }

        //CARGA DE FICHAS
        private void CargarFicha_function()
        {
            try
            {
                Conexion conexion = new Conexion();
                
                selectedValueMaquina = Request.Form[lista_maquinas.UniqueID];
                Cargar_maquinas(Convert.ToInt32(tbFicha.Value.ToString())); //JAVA
                lista_maquinas.Items.FindByValue(selectedValueMaquina).Selected = true;
                int maquina_actual = 0;
                //llamada a referencia heredada
                if (referenciaprevia != "")
                {
                    maquina_actual = Convert.ToInt32(Request.QueryString["MAQUINA"]);

                }
                else
                //fin llamada a referencia heredada
                {
                    maquina_actual = Convert.ToInt32(lista_maquinas.Items.FindByValue(selectedValueMaquina).Text); //ori
                }

                Cargar_versiones(Convert.ToInt32(tbFicha.Value.ToString()), Convert.ToInt32(selectedValueMaquina));

                int version = 0;
                string version_string = Request.Form[lista_versiones.UniqueID];
                if (version_string == null)
                {
                    version = conexion.leer_maxima_version(Convert.ToInt32(tbFicha.Value), Convert.ToInt32(selectedValueMaquina));
                }
                else
                {
                    version = Convert.ToInt32(version_string);
                }
                lista_versiones.Items.FindByValue(version.ToString()).Selected = true;

                

                //CARGO ESTRUCTURA
                //Conexion conexion = new Conexion();
                // Cargar_historico_modificaciones(Convert.ToInt32(tbFicha.Value.ToString()), Convert.ToInt32(selectedValueMaquina));
                //Cargar_estructura(Convert.ToInt32(tbFicha.Value.ToString()), Convert.ToInt32(selectedValueMaquina));

                DataSet GRIDS = conexion.cargar_datos_estructura(Convert.ToInt32(tbFicha.Value.ToString()));
                dgv_Materiales.DataSource = GRIDS;
                dgv_Materiales.DataBind();

                GRIDS = conexion.LeerHistoricoFichasFabricacionV2(Convert.ToInt32(tbFicha.Value.ToString()));
                dgvHistoricoModificaciones.DataSource = GRIDS;
                dgvHistoricoModificaciones.DataBind();

                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                
                DataTable FichaParam = SHConexion.LeerFichaParametrosV2(Convert.ToInt32(tbFicha.Value), Convert.ToInt32(selectedValueMaquina), version);
                if (FichaParam.Rows.Count > 0)
                {
                    tbReferencia.Text = FichaParam.Rows[0]["Referencia"].ToString();
                    tbNombre.Text = FichaParam.Rows[0]["Descripcion"].ToString();
                    tbCliente.Text = FichaParam.Rows[0]["Cliente"].ToString();
                    tbCodigoMolde.Text = FichaParam.Rows[0]["CodMolde"].ToString();
                    tbUbicacionMolde.Text = FichaParam.Rows[0]["UBIMOLDE"].ToString();
                    tbMaquina.Text = FichaParam.Rows[0]["NMaquina"].ToString();
                    //tbAutomatico.Text = FichaParam.Rows[0]["Automatico"].ToString();
                    //tbManual.Text = FichaParam.Rows[0]["Manual"].ToString();
                    tbPersonal.Text = FichaParam.Rows[0]["PersonalAsignado"].ToString();
                    tbProgramaMolde.Text = FichaParam.Rows[0]["ProgramaMolde"].ToString();
                    tbProgramaRobot.Text = FichaParam.Rows[0]["NProgramaRobot"].ToString();
                    //bManoRobot.Text = ds.Tables[0].Rows[0]["NManoRobot"].ToString();
                    //tbManoRobot.Text = conexion.Devuelve_ManoVinculada(tbCodigoMolde.Text);
                    tbManoRobot.Text = FichaParam.Rows[0]["Mano"].ToString();
                    tbUbicacionMano.Text = FichaParam.Rows[0]["UBIMANO"].ToString();
                    tbAperturaMaquina.Text = FichaParam.Rows[0]["AperturaMaquina"].ToString();
                    tbCavidades.Text = FichaParam.Rows[0]["NCavidades"].ToString();
                    tbPesoPieza.Text = FichaParam.Rows[0]["PesoPiezas"].ToString();
                    tbPesoColada.Text = FichaParam.Rows[0]["PesoColadas"].ToString();
                    tbPesoTotal.Text = FichaParam.Rows[0]["PesoTotales"].ToString();
                    thVCarga.Text = FichaParam.Rows[0]["VelocidadCarga"].ToString();
                    thCarga.Text = FichaParam.Rows[0]["Carga"].ToString();
                    thDescomp.Text = FichaParam.Rows[0]["Descompresion"].ToString();
                    thContrapr.Text = FichaParam.Rows[0]["Contrapresion"].ToString();
                    thTiempo.Text = FichaParam.Rows[0]["Tiempo"].ToString();
                    thEnfriamiento.Text = FichaParam.Rows[0]["Enfriamiento"].ToString();
                    thCiclo.Text = FichaParam.Rows[0]["Ciclo"].ToString();
                    thCojin.Text = FichaParam.Rows[0]["Cojin"].ToString();
                    tbRazones.Text = FichaParam.Rows[0]["Razones"].ToString();
                    cbEdicion.Text = FichaParam.Rows[0]["Version"].ToString();

                    //NUEVOS
                    DropModoSelect.SelectedValue = FichaParam.Rows[0]["ModoAutoMan"].ToString();
                    IMGliente.ImageUrl = FichaParam.Rows[0]["LogotipoSM"].ToString();
                    if(FichaParam.Rows[0]["ImagenPieza"].ToString() != "")
                    { 
                    ImgPieza.ImageUrl = FichaParam.Rows[0]["ImagenPieza"].ToString();
                    }
                    try
                        {
                        cbFecha.Text = Convert.ToDateTime(FichaParam.Rows[0]["Fecha"]).ToString("dd/MM/yyyy HH:mm");
                        }
                    catch(Exception ex)
                        {
                        cbFecha.Text = FichaParam.Rows[0]["Fecha"].ToString();
                        }
                    
                    tbFuerzaCierre.Text = FichaParam.Rows[0]["FuerzaCierre"].ToString();
                    //cbElaboradoPor.Text = ds.Tables[0].Rows[0]["Elaborado"].ToString(); //listar
                    //cbRevisadoPor.Text = ds.Tables[0].Rows[0]["Revisado"].ToString(); //listar
                    //cbAprobadoPor.Text = ds.Tables[0].Rows[0]["Aprobado"].ToString(); //listar
                    tbObservaciones.Text = FichaParam.Rows[0]["Observaciones"].ToString();

                    //CARGARESPONSABLES
                    
                    DataSet Operarios = SHConexion.Devuelve_mandos_intermedios_SMARTH();

                    //CARGA AUTORES
                    Operarios.Tables[0].DefaultView.RowFilter = "Departamento = 'PRODUCCION' OR Departamento = '-'";
                    DataTable DTPRODUCCION = (Operarios.Tables[0].DefaultView).ToTable();

                    //ELABORADO                  
                    cbElaboradoPor.Items.Clear();
                    foreach (DataRow row in DTPRODUCCION.Rows)
                    { cbElaboradoPor.Items.Add(row["Nombre"].ToString()); }
                    cbElaboradoPor.ClearSelection();
                    if (FichaParam.Rows[0]["INTElaborado"].ToString() != "")
                    {
                        try
                        {
                            cbElaboradoPor.SelectedValue = SHConexion.Devuelve_Pilotos_SMARTH(Convert.ToInt16(FichaParam.Rows[0]["INTElaborado"].ToString()));
                        }
                        catch (Exception ex)
                        {
                            cbElaboradoPor.SelectedValue = "-";
                        }
                    }
                    else
                    {
                        cbElaboradoPor.SelectedValue = "-";
                    }

                    //REVISADO
                    cbRevisadoPor.Items.Clear();
                    foreach (DataRow row in DTPRODUCCION.Rows)
                    { cbRevisadoPor.Items.Add(row["Nombre"].ToString()); }
                    cbRevisadoPor.ClearSelection();
                    if (FichaParam.Rows[0]["INTRevisado"].ToString() != "")
                    {
                        cbRevisadoPor.SelectedValue = SHConexion.Devuelve_Pilotos_SMARTH(Convert.ToInt16(FichaParam.Rows[0]["INTRevisado"].ToString()));
                    }
                    else
                    {
                        cbRevisadoPor.SelectedValue = "-";
                    }

                    //APROBADO
                    Operarios.Tables[0].DefaultView.RowFilter = "Departamento = 'INGENIERIA' OR Departamento = '-' OR Departamento = 'CALIDAD'";
                    DTPRODUCCION = (Operarios.Tables[0].DefaultView).ToTable();
                    cbAprobadoPor.Items.Clear();
                    foreach (DataRow row in DTPRODUCCION.Rows)
                    { cbAprobadoPor.Items.Add(row["Nombre"].ToString()); }
                    cbAprobadoPor.ClearSelection();
                    if (FichaParam.Rows[0]["INTAprobado"].ToString() != "")
                    {
                        cbAprobadoPor.SelectedValue = SHConexion.Devuelve_Pilotos_SMARTH(Convert.ToInt16(FichaParam.Rows[0]["INTAprobado"].ToString()));
                    }
                    else
                    {
                        cbAprobadoPor.SelectedValue = "-";
                    }



                }


                DataSet ds = conexion.leerTempCilindro(Convert.ToInt32(tbFicha.Value), Convert.ToInt32(selectedValueMaquina), version);
                thBoq.Text = ds.Tables[0].Rows[0]["Boq"].ToString();
                thT1.Text = ds.Tables[0].Rows[0]["T1"].ToString();
                thT2.Text = ds.Tables[0].Rows[0]["T2"].ToString();
                thT3.Text = ds.Tables[0].Rows[0]["T3"].ToString();
                thT4.Text = ds.Tables[0].Rows[0]["T4"].ToString();
                thT5.Text = ds.Tables[0].Rows[0]["T5"].ToString();
                thT6.Text = ds.Tables[0].Rows[0]["T6"].ToString();
                thT7.Text = ds.Tables[0].Rows[0]["T7"].ToString();
                thT8.Text = ds.Tables[0].Rows[0]["T8"].ToString();
                thT9.Text = ds.Tables[0].Rows[0]["T9"].ToString();
                thT10.Text = ds.Tables[0].Rows[0]["T10"].ToString();
                //tbFijaRefrigeracion.Text = ds.Tables[0].Rows[0]["RefrigeracionCircuito"].ToString();
                //tbFijaAtempCircuito.Text = ds.Tables[0].Rows[0]["AtempCircuito"].ToString();
                //tbAtempTemperatura.Text = ds.Tables[0].Rows[0]["AtempTemperatura"].ToString();

                ds = conexion.leerTempCamCaliente(Convert.ToInt32(tbFicha.Value), Convert.ToInt32(selectedValueMaquina), version);
                thZ1.Text = ds.Tables[0].Rows[0]["Z1"].ToString();
                thZ2.Text = ds.Tables[0].Rows[0]["Z2"].ToString();
                thZ3.Text = ds.Tables[0].Rows[0]["Z3"].ToString();
                thZ4.Text = ds.Tables[0].Rows[0]["Z4"].ToString();
                thZ5.Text = ds.Tables[0].Rows[0]["Z5"].ToString();
                thZ6.Text = ds.Tables[0].Rows[0]["Z6"].ToString();
                thZ7.Text = ds.Tables[0].Rows[0]["Z7"].ToString();
                thZ8.Text = ds.Tables[0].Rows[0]["Z8"].ToString();
                thZ9.Text = ds.Tables[0].Rows[0]["Z9"].ToString();
                thZ10.Text = ds.Tables[0].Rows[0]["Z10"].ToString();
                thZ11.Text = ds.Tables[0].Rows[0]["Z11"].ToString();
                thZ12.Text = ds.Tables[0].Rows[0]["Z12"].ToString();
                thZ13.Text = ds.Tables[0].Rows[0]["Z13"].ToString();
                thZ14.Text = ds.Tables[0].Rows[0]["Z14"].ToString();
                thZ15.Text = ds.Tables[0].Rows[0]["Z15"].ToString();
                thZ16.Text = ds.Tables[0].Rows[0]["Z16"].ToString();
                thZ17.Text = ds.Tables[0].Rows[0]["Z17"].ToString();
                thZ18.Text = ds.Tables[0].Rows[0]["Z18"].ToString();
                thZ19.Text = ds.Tables[0].Rows[0]["Z19"].ToString();
                thZ20.Text = ds.Tables[0].Rows[0]["Z20"].ToString();
                //tbMovilRefrigeracion.Text = ds.Tables[0].Rows[0]["RefrigeracionCircuito"].ToString();
                //tbMovilAtempCircuito.Text = ds.Tables[0].Rows[0]["AtempCircuito"].ToString();
                //tbMovilAtempTemperatura.Text = ds.Tables[0].Rows[0]["AtempTemperatura"].ToString();

                ds = conexion.leerInyeccion(Convert.ToInt32(tbFicha.Value), Convert.ToInt32(selectedValueMaquina), version);
                thV1.Text = ds.Tables[0].Rows[0]["V1"].ToString();
                thV2.Text = ds.Tables[0].Rows[0]["V2"].ToString();
                thV3.Text = ds.Tables[0].Rows[0]["V3"].ToString();
                thV4.Text = ds.Tables[0].Rows[0]["V4"].ToString();
                thV5.Text = ds.Tables[0].Rows[0]["V5"].ToString();
                thV6.Text = ds.Tables[0].Rows[0]["V6"].ToString();
                thV7.Text = ds.Tables[0].Rows[0]["V7"].ToString();
                thV8.Text = ds.Tables[0].Rows[0]["V8"].ToString();
                thV9.Text = ds.Tables[0].Rows[0]["V9"].ToString();
                thV10.Text = ds.Tables[0].Rows[0]["V10"].ToString();
                thV11.Text = ds.Tables[0].Rows[0]["V11"].ToString();
                thV12.Text = ds.Tables[0].Rows[0]["V12"].ToString();
                thC1.Text = ds.Tables[0].Rows[0]["C1"].ToString();
                thC2.Text = ds.Tables[0].Rows[0]["C2"].ToString();
                thC3.Text = ds.Tables[0].Rows[0]["C3"].ToString();
                thC4.Text = ds.Tables[0].Rows[0]["C4"].ToString();
                thC5.Text = ds.Tables[0].Rows[0]["C5"].ToString();
                thC6.Text = ds.Tables[0].Rows[0]["C6"].ToString();
                thC7.Text = ds.Tables[0].Rows[0]["C7"].ToString();
                thC8.Text = ds.Tables[0].Rows[0]["C8"].ToString();
                thC9.Text = ds.Tables[0].Rows[0]["C9"].ToString();
                thC10.Text = ds.Tables[0].Rows[0]["C10"].ToString();
                thC11.Text = ds.Tables[0].Rows[0]["C11"].ToString();
                thC12.Text = ds.Tables[0].Rows[0]["C12"].ToString();
                tbTiempoInyeccion.Text = ds.Tables[0].Rows[0]["Tiempo"].ToString();
                tbLimitePresion.Text = ds.Tables[0].Rows[0]["LimitePresion"].ToString();
                tbLimitePresionReal.Text = ds.Tables[0].Rows[0]["LimitePresionReal"].ToString();

                ds = conexion.leerPostpresion(Convert.ToInt32(tbFicha.Value), Convert.ToInt32(selectedValueMaquina), version);
                thP1.Text = ds.Tables[0].Rows[0]["P1"].ToString();
                thP2.Text = ds.Tables[0].Rows[0]["P2"].ToString();
                thP3.Text = ds.Tables[0].Rows[0]["P3"].ToString();
                thP4.Text = ds.Tables[0].Rows[0]["P4"].ToString();
                thP5.Text = ds.Tables[0].Rows[0]["P5"].ToString();
                thP6.Text = ds.Tables[0].Rows[0]["P6"].ToString();
                thP7.Text = ds.Tables[0].Rows[0]["P7"].ToString();
                thP8.Text = ds.Tables[0].Rows[0]["P8"].ToString();
                thP9.Text = ds.Tables[0].Rows[0]["P9"].ToString();
                thP10.Text = ds.Tables[0].Rows[0]["P10"].ToString();
                thTP1.Text = ds.Tables[0].Rows[0]["T1"].ToString();
                thTP2.Text = ds.Tables[0].Rows[0]["T2"].ToString();
                thTP3.Text = ds.Tables[0].Rows[0]["T3"].ToString();
                thTP4.Text = ds.Tables[0].Rows[0]["T4"].ToString();
                thTP5.Text = ds.Tables[0].Rows[0]["T5"].ToString();
                thTP6.Text = ds.Tables[0].Rows[0]["T6"].ToString();
                thTP7.Text = ds.Tables[0].Rows[0]["T7"].ToString();
                thTP8.Text = ds.Tables[0].Rows[0]["T8"].ToString();
                thTP9.Text = ds.Tables[0].Rows[0]["T9"].ToString();
                thTP10.Text = ds.Tables[0].Rows[0]["T10"].ToString();
                tbConmutacion.Text = ds.Tables[0].Rows[0]["Conmutacion"].ToString();
                tbTiempoPresion.Text = ds.Tables[0].Rows[0]["TiempoPresion"].ToString();

                //BLOQUE ATEMPERADO NUEVO
                ds = conexion.leerAtemperado(Convert.ToInt32(tbFicha.Value), Convert.ToInt32(selectedValueMaquina), version);
                //TIPO DE ATEMPERADO
                DataSet AtempTipo = new DataSet();
                AtempTipo = conexion.devuelve_lista_tipo_atemperado();
                //PARTE FIJA
                AtempTipoF.Items.Clear();
                foreach (DataRow row in AtempTipo.Tables[0].Rows)
                { AtempTipoF.Items.Add(row["TipoAgua"].ToString()); }
                AtempTipoF.ClearSelection();
                if (ds.Tables[0].Rows[0]["AtempTipoF"].ToString() != "")
                { AtempTipoF.SelectedValue = conexion.devuelve_tipo_atemperado(Convert.ToInt16(ds.Tables[0].Rows[0]["AtempTipoF"].ToString())); }
                else { AtempTipoF.SelectedValue = ""; }

                //PARTE MOVIL
                AtempTipoM.Items.Clear();
                foreach (DataRow row in AtempTipo.Tables[0].Rows)
                { AtempTipoM.Items.Add(row["TipoAgua"].ToString()); }
                AtempTipoM.ClearSelection();
                if (ds.Tables[0].Rows[0]["AtempTipoM"].ToString() != "")
                { AtempTipoM.SelectedValue = conexion.devuelve_tipo_atemperado(Convert.ToInt16(ds.Tables[0].Rows[0]["AtempTipoM"].ToString())); }
                else { AtempTipoM.SelectedValue = ""; }

                //CIRCUITOS
                DataSet Circuitos = new DataSet();
                Circuitos = conexion.devuelve_lista_circuitos();
                //PARTE FIJA
                TbCircuitoF1.Items.Clear();
                foreach (DataRow row in Circuitos.Tables[0].Rows) { TbCircuitoF1.Items.Add(row["Circuitos"].ToString()); }
                TbCircuitoF1.ClearSelection();
                if (ds.Tables[0].Rows[0]["CircuitoF1"].ToString() != "")
                { TbCircuitoF1.SelectedValue = conexion.devuelve_tipo_circuito(Convert.ToInt16(ds.Tables[0].Rows[0]["CircuitoF1"].ToString())); }
                else { TbCircuitoF1.SelectedValue = ""; }

                TbCircuitoF2.Items.Clear();
                foreach (DataRow row in Circuitos.Tables[0].Rows) { TbCircuitoF2.Items.Add(row["Circuitos"].ToString()); }
                TbCircuitoF2.ClearSelection();
                if (ds.Tables[0].Rows[0]["CircuitoF2"].ToString() != "")
                { TbCircuitoF2.SelectedValue = conexion.devuelve_tipo_circuito(Convert.ToInt16(ds.Tables[0].Rows[0]["CircuitoF2"].ToString())); }
                else { TbCircuitoF2.SelectedValue = ""; }

                TbCircuitoF3.Items.Clear();
                foreach (DataRow row in Circuitos.Tables[0].Rows) { TbCircuitoF3.Items.Add(row["Circuitos"].ToString()); }
                TbCircuitoF3.ClearSelection();
                if (ds.Tables[0].Rows[0]["CircuitoF3"].ToString() != "")
                { TbCircuitoF3.SelectedValue = conexion.devuelve_tipo_circuito(Convert.ToInt16(ds.Tables[0].Rows[0]["CircuitoF3"].ToString())); }
                else { TbCircuitoF3.SelectedValue = ""; }

                TbCircuitoF4.Items.Clear();
                foreach (DataRow row in Circuitos.Tables[0].Rows) { TbCircuitoF4.Items.Add(row["Circuitos"].ToString()); }
                TbCircuitoF4.ClearSelection();
                if (ds.Tables[0].Rows[0]["CircuitoF4"].ToString() != "")
                { TbCircuitoF4.SelectedValue = conexion.devuelve_tipo_circuito(Convert.ToInt16(ds.Tables[0].Rows[0]["CircuitoF4"].ToString())); }
                else { TbCircuitoF4.SelectedValue = ""; }

                TbCircuitoF5.Items.Clear();
                foreach (DataRow row in Circuitos.Tables[0].Rows) { TbCircuitoF5.Items.Add(row["Circuitos"].ToString()); }
                TbCircuitoF5.ClearSelection();
                if (ds.Tables[0].Rows[0]["CircuitoF5"].ToString() != "")
                { TbCircuitoF5.SelectedValue = conexion.devuelve_tipo_circuito(Convert.ToInt16(ds.Tables[0].Rows[0]["CircuitoF5"].ToString())); }
                else { TbCircuitoF5.SelectedValue = ""; }

                TbCircuitoF6.Items.Clear();
                foreach (DataRow row in Circuitos.Tables[0].Rows) { TbCircuitoF6.Items.Add(row["Circuitos"].ToString()); }
                TbCircuitoF6.ClearSelection();
                if (ds.Tables[0].Rows[0]["CircuitoF6"].ToString() != "")
                { TbCircuitoF6.SelectedValue = conexion.devuelve_tipo_circuito(Convert.ToInt16(ds.Tables[0].Rows[0]["CircuitoF6"].ToString())); }
                else { TbCircuitoF6.SelectedValue = ""; }

                //PARTE MOVIL
                TbCircuitoM1.Items.Clear();
                foreach (DataRow row in Circuitos.Tables[0].Rows) { TbCircuitoM1.Items.Add(row["Circuitos"].ToString()); }
                TbCircuitoM1.ClearSelection();
                if (ds.Tables[0].Rows[0]["CircuitoM1"].ToString() != "")
                { TbCircuitoM1.SelectedValue = conexion.devuelve_tipo_circuito(Convert.ToInt16(ds.Tables[0].Rows[0]["CircuitoM1"].ToString())); }
                else { TbCircuitoM1.SelectedValue = ""; }

                TbCircuitoM2.Items.Clear();
                foreach (DataRow row in Circuitos.Tables[0].Rows) { TbCircuitoM2.Items.Add(row["Circuitos"].ToString()); }
                TbCircuitoM2.ClearSelection();
                if (ds.Tables[0].Rows[0]["CircuitoM2"].ToString() != "")
                { TbCircuitoM2.SelectedValue = conexion.devuelve_tipo_circuito(Convert.ToInt16(ds.Tables[0].Rows[0]["CircuitoM2"].ToString())); }
                else { TbCircuitoM2.SelectedValue = ""; }

                TbCircuitoM3.Items.Clear();
                foreach (DataRow row in Circuitos.Tables[0].Rows) { TbCircuitoM3.Items.Add(row["Circuitos"].ToString()); }
                TbCircuitoM3.ClearSelection();
                if (ds.Tables[0].Rows[0]["CircuitoM3"].ToString() != "")
                { TbCircuitoM3.SelectedValue = conexion.devuelve_tipo_circuito(Convert.ToInt16(ds.Tables[0].Rows[0]["CircuitoM3"].ToString())); }
                else { TbCircuitoM3.SelectedValue = ""; }

                TbCircuitoM4.Items.Clear();
                foreach (DataRow row in Circuitos.Tables[0].Rows) { TbCircuitoM4.Items.Add(row["Circuitos"].ToString()); }
                TbCircuitoM4.ClearSelection();
                if (ds.Tables[0].Rows[0]["CircuitoM4"].ToString() != "")
                { TbCircuitoM4.SelectedValue = conexion.devuelve_tipo_circuito(Convert.ToInt16(ds.Tables[0].Rows[0]["CircuitoM4"].ToString())); }
                else { TbCircuitoM4.SelectedValue = ""; }

                TbCircuitoM5.Items.Clear();
                foreach (DataRow row in Circuitos.Tables[0].Rows) { TbCircuitoM5.Items.Add(row["Circuitos"].ToString()); }
                TbCircuitoM5.ClearSelection();
                if (ds.Tables[0].Rows[0]["CircuitoM5"].ToString() != "")
                { TbCircuitoM5.SelectedValue = conexion.devuelve_tipo_circuito(Convert.ToInt16(ds.Tables[0].Rows[0]["CircuitoM5"].ToString())); }
                else { TbCircuitoM5.SelectedValue = ""; }

                TbCircuitoM6.Items.Clear();
                foreach (DataRow row in Circuitos.Tables[0].Rows) { TbCircuitoM6.Items.Add(row["Circuitos"].ToString()); }
                TbCircuitoM6.ClearSelection();
                if (ds.Tables[0].Rows[0]["CircuitoM6"].ToString() != "")
                { TbCircuitoM6.SelectedValue = conexion.devuelve_tipo_circuito(Convert.ToInt16(ds.Tables[0].Rows[0]["CircuitoM6"].ToString())); }
                else { TbCircuitoM6.SelectedValue = ""; }

                //CAUDALES
                //PARTE FIJA
                TbCaudalF1.Text = ds.Tables[0].Rows[0]["CaudalF1"].ToString();
                TbCaudalF2.Text = ds.Tables[0].Rows[0]["CaudalF2"].ToString();
                TbCaudalF3.Text = ds.Tables[0].Rows[0]["CaudalF3"].ToString();
                TbCaudalF4.Text = ds.Tables[0].Rows[0]["CaudalF4"].ToString();
                TbCaudalF5.Text = ds.Tables[0].Rows[0]["CaudalF5"].ToString();
                TbCaudalF6.Text = ds.Tables[0].Rows[0]["CaudalF6"].ToString();
                //PARTE MOVIL
                TbCaudalM1.Text = ds.Tables[0].Rows[0]["CaudalM1"].ToString();
                TbCaudalM2.Text = ds.Tables[0].Rows[0]["CaudalM2"].ToString();
                TbCaudalM3.Text = ds.Tables[0].Rows[0]["CaudalM3"].ToString();
                TbCaudalM4.Text = ds.Tables[0].Rows[0]["CaudalM4"].ToString();
                TbCaudalM5.Text = ds.Tables[0].Rows[0]["CaudalM5"].ToString();
                TbCaudalM6.Text = ds.Tables[0].Rows[0]["CaudalM6"].ToString();

                //TEMPERATURAS
                //PARTE FIJA
                TbTemperaturaF1.Text = ds.Tables[0].Rows[0]["TemperaturaF1"].ToString();
                TbTemperaturaF2.Text = ds.Tables[0].Rows[0]["TemperaturaF2"].ToString();
                TbTemperaturaF3.Text = ds.Tables[0].Rows[0]["TemperaturaF3"].ToString();
                TbTemperaturaF4.Text = ds.Tables[0].Rows[0]["TemperaturaF4"].ToString();
                TbTemperaturaF5.Text = ds.Tables[0].Rows[0]["TemperaturaF5"].ToString();
                TbTemperaturaF6.Text = ds.Tables[0].Rows[0]["TemperaturaF6"].ToString();
                //PARTE MOVIL
                TbTemperaturaM1.Text = ds.Tables[0].Rows[0]["TemperaturaM1"].ToString();
                TbTemperaturaM2.Text = ds.Tables[0].Rows[0]["TemperaturaM2"].ToString();
                TbTemperaturaM3.Text = ds.Tables[0].Rows[0]["TemperaturaM3"].ToString();
                TbTemperaturaM4.Text = ds.Tables[0].Rows[0]["TemperaturaM4"].ToString();
                TbTemperaturaM5.Text = ds.Tables[0].Rows[0]["TemperaturaM5"].ToString();
                TbTemperaturaM6.Text = ds.Tables[0].Rows[0]["TemperaturaM6"].ToString();

                //ENTRADAS ATEMPERADO
                DataSet entradasAtemperado = new DataSet();
                entradasAtemperado = conexion.devuelve_lista_entradasAtemp();
                //PARTE FIJA
                TbEntradaF1.Items.Clear();
                foreach (DataRow row in entradasAtemperado.Tables[0].Rows) { TbEntradaF1.Items.Add(row["AtempIN"].ToString()); }
                TbEntradaF1.ClearSelection();
                if (ds.Tables[0].Rows[0]["EntradaF1"].ToString() != "")
                { TbEntradaF1.SelectedValue = conexion.devuelve_tipo_entradasAtemp(Convert.ToInt16(ds.Tables[0].Rows[0]["EntradaF1"].ToString())); }
                else { TbEntradaF1.SelectedValue = ""; }

                TbEntradaF2.Items.Clear();
                foreach (DataRow row in entradasAtemperado.Tables[0].Rows) { TbEntradaF2.Items.Add(row["AtempIN"].ToString()); }
                TbEntradaF2.ClearSelection();
                if (ds.Tables[0].Rows[0]["EntradaF1"].ToString() != "")
                { TbEntradaF2.SelectedValue = conexion.devuelve_tipo_entradasAtemp(Convert.ToInt16(ds.Tables[0].Rows[0]["EntradaF2"].ToString())); }
                else { TbEntradaF2.SelectedValue = ""; }

                TbEntradaF3.Items.Clear();
                foreach (DataRow row in entradasAtemperado.Tables[0].Rows) { TbEntradaF3.Items.Add(row["AtempIN"].ToString()); }
                TbEntradaF3.ClearSelection();
                if (ds.Tables[0].Rows[0]["EntradaF3"].ToString() != "")
                { TbEntradaF3.SelectedValue = conexion.devuelve_tipo_entradasAtemp(Convert.ToInt16(ds.Tables[0].Rows[0]["EntradaF3"].ToString())); }
                else { TbEntradaF3.SelectedValue = ""; }

                TbEntradaF4.Items.Clear();
                foreach (DataRow row in entradasAtemperado.Tables[0].Rows) { TbEntradaF4.Items.Add(row["AtempIN"].ToString()); }
                TbEntradaF4.ClearSelection();
                if (ds.Tables[0].Rows[0]["EntradaF4"].ToString() != "")
                { TbEntradaF4.SelectedValue = conexion.devuelve_tipo_entradasAtemp(Convert.ToInt16(ds.Tables[0].Rows[0]["EntradaF4"].ToString())); }
                else { TbEntradaF4.SelectedValue = ""; }

                TbEntradaF5.Items.Clear();
                foreach (DataRow row in entradasAtemperado.Tables[0].Rows) { TbEntradaF5.Items.Add(row["AtempIN"].ToString()); }
                TbEntradaF5.ClearSelection();
                if (ds.Tables[0].Rows[0]["EntradaF5"].ToString() != "")
                { TbEntradaF5.SelectedValue = conexion.devuelve_tipo_entradasAtemp(Convert.ToInt16(ds.Tables[0].Rows[0]["EntradaF5"].ToString())); }
                else { TbEntradaF5.SelectedValue = ""; }

                TbEntradaF6.Items.Clear();
                foreach (DataRow row in entradasAtemperado.Tables[0].Rows) { TbEntradaF6.Items.Add(row["AtempIN"].ToString()); }
                TbEntradaF6.ClearSelection();
                if (ds.Tables[0].Rows[0]["EntradaF6"].ToString() != "")
                { TbEntradaF6.SelectedValue = conexion.devuelve_tipo_entradasAtemp(Convert.ToInt16(ds.Tables[0].Rows[0]["EntradaF6"].ToString())); }
                else { TbEntradaF6.SelectedValue = ""; }

                //PARTE MOVIL
                TbEntradaM1.Items.Clear();
                foreach (DataRow row in entradasAtemperado.Tables[0].Rows) { TbEntradaM1.Items.Add(row["AtempIN"].ToString()); }
                TbEntradaM1.ClearSelection();
                if (ds.Tables[0].Rows[0]["EntradaM1"].ToString() != "")
                { TbEntradaM1.SelectedValue = conexion.devuelve_tipo_entradasAtemp(Convert.ToInt16(ds.Tables[0].Rows[0]["EntradaM1"].ToString())); }
                else { TbEntradaM1.SelectedValue = ""; }

                TbEntradaM2.Items.Clear();
                foreach (DataRow row in entradasAtemperado.Tables[0].Rows) { TbEntradaM2.Items.Add(row["AtempIN"].ToString()); }
                TbEntradaM2.ClearSelection();
                if (ds.Tables[0].Rows[0]["EntradaM1"].ToString() != "")
                { TbEntradaM2.SelectedValue = conexion.devuelve_tipo_entradasAtemp(Convert.ToInt16(ds.Tables[0].Rows[0]["EntradaM2"].ToString())); }
                else { TbEntradaM2.SelectedValue = ""; }

                TbEntradaM3.Items.Clear();
                foreach (DataRow row in entradasAtemperado.Tables[0].Rows) { TbEntradaM3.Items.Add(row["AtempIN"].ToString()); }
                TbEntradaM3.ClearSelection();
                if (ds.Tables[0].Rows[0]["EntradaM3"].ToString() != "")
                { TbEntradaM3.SelectedValue = conexion.devuelve_tipo_entradasAtemp(Convert.ToInt16(ds.Tables[0].Rows[0]["EntradaM3"].ToString())); }
                else { TbEntradaM3.SelectedValue = ""; }

                TbEntradaM4.Items.Clear();
                foreach (DataRow row in entradasAtemperado.Tables[0].Rows) { TbEntradaM4.Items.Add(row["AtempIN"].ToString()); }
                TbEntradaM4.ClearSelection();
                if (ds.Tables[0].Rows[0]["EntradaM4"].ToString() != "")
                { TbEntradaM4.SelectedValue = conexion.devuelve_tipo_entradasAtemp(Convert.ToInt16(ds.Tables[0].Rows[0]["EntradaM4"].ToString())); }
                else { TbEntradaM4.SelectedValue = ""; }

                TbEntradaM5.Items.Clear();
                foreach (DataRow row in entradasAtemperado.Tables[0].Rows) { TbEntradaM5.Items.Add(row["AtempIN"].ToString()); }
                TbEntradaM5.ClearSelection();
                if (ds.Tables[0].Rows[0]["EntradaM5"].ToString() != "")
                { TbEntradaM5.SelectedValue = conexion.devuelve_tipo_entradasAtemp(Convert.ToInt16(ds.Tables[0].Rows[0]["EntradaM5"].ToString())); }
                else { TbEntradaM5.SelectedValue = ""; }

                TbEntradaM6.Items.Clear();
                foreach (DataRow row in entradasAtemperado.Tables[0].Rows) { TbEntradaM6.Items.Add(row["AtempIN"].ToString()); }
                TbEntradaM6.ClearSelection();
                if (ds.Tables[0].Rows[0]["EntradaM6"].ToString() != "")
                { TbEntradaM6.SelectedValue = conexion.devuelve_tipo_entradasAtemp(Convert.ToInt16(ds.Tables[0].Rows[0]["EntradaM6"].ToString())); }
                else { TbEntradaM6.SelectedValue = ""; }



                //leer secuencial
                ds = conexion.leerSecuencial(Convert.ToInt32(tbFicha.Value), Convert.ToInt32(selectedValueMaquina), version);
                seqAbrir1_1.Text = ds.Tables[0].Rows[0]["Abrir1_1"].ToString();
                seqAbrir1_2.Text = ds.Tables[0].Rows[0]["Abrir1_2"].ToString();
                seqAbrir1_3.Text = ds.Tables[0].Rows[0]["Abrir1_3"].ToString();
                seqAbrir1_4.Text = ds.Tables[0].Rows[0]["Abrir1_4"].ToString();
                seqAbrir1_5.Text = ds.Tables[0].Rows[0]["Abrir1_5"].ToString();
                seqAbrir1_6.Text = ds.Tables[0].Rows[0]["Abrir1_6"].ToString();
                seqAbrir1_7.Text = ds.Tables[0].Rows[0]["Abrir1_7"].ToString();
                seqAbrir1_8.Text = ds.Tables[0].Rows[0]["Abrir1_8"].ToString();
                seqAbrir1_9.Text = ds.Tables[0].Rows[0]["Abrir1_9"].ToString();
                seqAbrir1_10.Text = ds.Tables[0].Rows[0]["Abrir1_10"].ToString();
                seqCerrar1_1.Text = ds.Tables[0].Rows[0]["Cerrar1_1"].ToString();
                seqCerrar1_2.Text = ds.Tables[0].Rows[0]["Cerrar1_2"].ToString();
                seqCerrar1_3.Text = ds.Tables[0].Rows[0]["Cerrar1_3"].ToString();
                seqCerrar1_4.Text = ds.Tables[0].Rows[0]["Cerrar1_4"].ToString();
                seqCerrar1_5.Text = ds.Tables[0].Rows[0]["Cerrar1_5"].ToString();
                seqCerrar1_6.Text = ds.Tables[0].Rows[0]["Cerrar1_6"].ToString();
                seqCerrar1_7.Text = ds.Tables[0].Rows[0]["Cerrar1_7"].ToString();
                seqCerrar1_8.Text = ds.Tables[0].Rows[0]["Cerrar1_8"].ToString();
                seqCerrar1_9.Text = ds.Tables[0].Rows[0]["Cerrar1_9"].ToString();
                seqCerrar1_10.Text = ds.Tables[0].Rows[0]["Cerrar1_10"].ToString();
                seqAbrir2_1.Text = ds.Tables[0].Rows[0]["Abrir2_1"].ToString();
                seqAbrir2_2.Text = ds.Tables[0].Rows[0]["Abrir2_2"].ToString();
                seqAbrir2_3.Text = ds.Tables[0].Rows[0]["Abrir2_3"].ToString();
                seqAbrir2_4.Text = ds.Tables[0].Rows[0]["Abrir2_4"].ToString();
                seqAbrir2_5.Text = ds.Tables[0].Rows[0]["Abrir2_5"].ToString();
                seqAbrir2_6.Text = ds.Tables[0].Rows[0]["Abrir2_6"].ToString();
                seqAbrir2_7.Text = ds.Tables[0].Rows[0]["Abrir2_7"].ToString();
                seqAbrir2_8.Text = ds.Tables[0].Rows[0]["Abrir2_8"].ToString();
                seqAbrir2_9.Text = ds.Tables[0].Rows[0]["Abrir2_9"].ToString();
                seqAbrir2_10.Text = ds.Tables[0].Rows[0]["Abrir2_10"].ToString();
                seqCerrar2_1.Text = ds.Tables[0].Rows[0]["Cerrar2_1"].ToString();
                seqCerrar2_2.Text = ds.Tables[0].Rows[0]["Cerrar2_2"].ToString();
                seqCerrar2_3.Text = ds.Tables[0].Rows[0]["Cerrar2_3"].ToString();
                seqCerrar2_4.Text = ds.Tables[0].Rows[0]["Cerrar2_4"].ToString();
                seqCerrar2_5.Text = ds.Tables[0].Rows[0]["Cerrar2_5"].ToString();
                seqCerrar2_6.Text = ds.Tables[0].Rows[0]["Cerrar2_6"].ToString();
                seqCerrar2_7.Text = ds.Tables[0].Rows[0]["Cerrar2_7"].ToString();
                seqCerrar2_8.Text = ds.Tables[0].Rows[0]["Cerrar2_8"].ToString();
                seqCerrar2_9.Text = ds.Tables[0].Rows[0]["Cerrar2_9"].ToString();
                seqCerrar2_10.Text = ds.Tables[0].Rows[0]["Cerrar2_10"].ToString();
                seqTPresPost1.Text = ds.Tables[0].Rows[0]["TprestPost_1"].ToString();
                seqTPresPost2.Text = ds.Tables[0].Rows[0]["TprestPost_2"].ToString();
                seqTPresPost3.Text = ds.Tables[0].Rows[0]["TprestPost_3"].ToString();
                seqTPresPost4.Text = ds.Tables[0].Rows[0]["TprestPost_4"].ToString();
                seqTPresPost5.Text = ds.Tables[0].Rows[0]["TprestPost_5"].ToString();
                seqTPresPost6.Text = ds.Tables[0].Rows[0]["TprestPost_6"].ToString();
                seqTPresPost7.Text = ds.Tables[0].Rows[0]["TprestPost_7"].ToString();
                seqTPresPost8.Text = ds.Tables[0].Rows[0]["TprestPost_8"].ToString();
                seqTPresPost9.Text = ds.Tables[0].Rows[0]["TprestPost_9"].ToString();
                seqTPresPost10.Text = ds.Tables[0].Rows[0]["TprestPost_10"].ToString();
                seqTiempoRetardo_1.Text = ds.Tables[0].Rows[0]["TiempoRetardo_1"].ToString();
                seqTiempoRetardo_2.Text = ds.Tables[0].Rows[0]["TiempoRetardo_2"].ToString();
                seqTiempoRetardo_3.Text = ds.Tables[0].Rows[0]["TiempoRetardo_3"].ToString();
                seqTiempoRetardo_4.Text = ds.Tables[0].Rows[0]["TiempoRetardo_4"].ToString();
                seqTiempoRetardo_5.Text = ds.Tables[0].Rows[0]["TiempoRetardo_5"].ToString();
                seqTiempoRetardo_6.Text = ds.Tables[0].Rows[0]["TiempoRetardo_6"].ToString();
                seqTiempoRetardo_7.Text = ds.Tables[0].Rows[0]["TiempoRetardo_7"].ToString();
                seqTiempoRetardo_8.Text = ds.Tables[0].Rows[0]["TiempoRetardo_8"].ToString();
                seqTiempoRetardo_9.Text = ds.Tables[0].Rows[0]["TiempoRetardo_9"].ToString();
                seqTiempoRetardo_10.Text = ds.Tables[0].Rows[0]["TiempoRetardo_10"].ToString();
                seqAnotaciones.Text = ds.Tables[0].Rows[0]["Anotaciones"].ToString();

                //LEER TOLERANCIAS
                ds = conexion.leerTolerancias(Convert.ToInt32(tbFicha.Value), Convert.ToInt32(selectedValueMaquina), version);
                tbTiempoInyeccionNVal.Text = ds.Tables[0].Rows[0]["TiempoInyeccionNVal"].ToString();
                tbTiempoInyeccionMVal.Text = ds.Tables[0].Rows[0]["TiempoInyeccionMVal"].ToString();
                tbLimitePresionNVal.Text = ds.Tables[0].Rows[0]["LimitePresionNVal"].ToString();
                tbLimitePresionMval.Text = ds.Tables[0].Rows[0]["LimitePresionMVal"].ToString();
                thConmuntaciontolNVal.Text = ds.Tables[0].Rows[0]["ConmuntaciontolNVal"].ToString();
                thConmuntaciontolMVal.Text = ds.Tables[0].Rows[0]["ConmuntaciontolMVal"].ToString();
                tbTiempoPresiontolNVal.Text = ds.Tables[0].Rows[0]["TiempoPresiontolNVal"].ToString();
                tbTiempoPresiontolMVal.Text = ds.Tables[0].Rows[0]["TiempoPresiontolMVal"].ToString();
                TNvcargaval.Text = ds.Tables[0].Rows[0]["TNvcargaval"].ToString();
                TMvcargaval.Text = ds.Tables[0].Rows[0]["TMvcargaval"].ToString();
                TNcargaval.Text = ds.Tables[0].Rows[0]["TNcargaval"].ToString();
                TMcargaval.Text = ds.Tables[0].Rows[0]["TMcargaval"].ToString();
                TNdescomval.Text = ds.Tables[0].Rows[0]["TNdescomval"].ToString();
                TMdescomval.Text = ds.Tables[0].Rows[0]["TMdescomval"].ToString();
                TNcontrapval.Text = ds.Tables[0].Rows[0]["TNcontrapval"].ToString();
                TMcontrapval.Text = ds.Tables[0].Rows[0]["TMcontrapval"].ToString();
                TNTiempdosval.Text = ds.Tables[0].Rows[0]["TNTiempdosval"].ToString();
                TMTiempdosval.Text = ds.Tables[0].Rows[0]["TMTiempdosval"].ToString();
                TNEnfriamval.Text = ds.Tables[0].Rows[0]["TNEnfriamval"].ToString();
                TMEnfriamval.Text = ds.Tables[0].Rows[0]["TMEnfriamval"].ToString();
                TNCicloval.Text = ds.Tables[0].Rows[0]["TNCicloval"].ToString();
                TMCicloval.Text = ds.Tables[0].Rows[0]["TMCicloval"].ToString();
                TNCojinval.Text = ds.Tables[0].Rows[0]["TNCojinval"].ToString();
                TMCojinval.Text = ds.Tables[0].Rows[0]["TMCojinval"].ToString();
                //leer notas de arranque
                TbOperacionText1.Text = ds.Tables[0].Rows[0]["ArranqueLIN1"].ToString();
                TbOperacionText2.Text = ds.Tables[0].Rows[0]["ArranqueLIN2"].ToString();
                TbOperacionText3.Text = ds.Tables[0].Rows[0]["ArranqueLIN3"].ToString();
                TbOperacionText4.Text = ds.Tables[0].Rows[0]["ArranqueLIN4"].ToString();
                TbOperacionText5.Text = ds.Tables[0].Rows[0]["ArranqueLIN5"].ToString();

                //leer nuevas tolerancias
                TbTOLCilindro.Text = ds.Tables[0].Rows[0]["TOLCilindro"].ToString();
                tbTOLCamCaliente.Text = ds.Tables[0].Rows[0]["TOLCamCaliente"].ToString();
                tbTOLCarInyeccion.Text = ds.Tables[0].Rows[0]["TOLCarInyeccion"].ToString();
                tbTOLPostPresion.Text = ds.Tables[0].Rows[0]["TOLPostPresion"].ToString();
                tbTOLSecCota.Text = ds.Tables[0].Rows[0]["TOLSecCota"].ToString();
                tbTOLSecTiempo.Text = ds.Tables[0].Rows[0]["TOLSecTiempo"].ToString();
                tbLimitePresionNRealVal.Text = ds.Tables[0].Rows[0]["LimitePresionRealNVal"].ToString();
                tbLimitePresionMRealVal.Text = ds.Tables[0].Rows[0]["LimitePresionRealMVal"].ToString();
                //leer cruces
                ds = conexion.leerMarcaCruz(Convert.ToInt32(tbFicha.Value), Convert.ToInt32(selectedValueMaquina), version);

                cbNoyos.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Noyos"]);
                cbHembra.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Hembra"]);
                cbMacho.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Macho"]);
                cbAntesExpul.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["AntesExpuls"]);
                cbAntesApert.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["AntesApert"]);
                cbAntesCierre.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["AntesCierre"]);
                cbDespuesCierre.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["DespuesCierre"]);
                cbOtros1.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Otros"]);
                cbBoquilla.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Boquilla"]);
                cbCono.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Cono"]);
                cbRadioLarga.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["RadioLarga"]);
                cbLibre.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Libre"]);
                cbValvula.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Valvula"]);
                cbResistencia.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Resistencia"]);
                cbOtros2.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Otros2"]);
                cbExpulsion.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Expulsion"]);
                cbHidraulica.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Hidraulica"]);
                cbNeumatica.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Neumatica"]);
                cbNormal.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Normal"]);
                cbArandela125.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Arandela125"]);
                cbArandela160.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Arandela160"]);
                cbArandela200.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Arandela200"]);
                cbArandela250.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Arandela250"]);
                MarcasOtrosText.Text = ds.Tables[0].Rows[0]["NotasMarcaCruz"].ToString();

                //CARGA IMAGENES
                string imagen1 = conexion.leerImagenes(Convert.ToInt32(tbFicha.Value), Convert.ToInt32(selectedValueMaquina), version, 1);
                string imagen2 = conexion.leerImagenes(Convert.ToInt32(tbFicha.Value), Convert.ToInt32(selectedValueMaquina), version, 2);
                string imagen3 = conexion.leerImagenes(Convert.ToInt32(tbFicha.Value), Convert.ToInt32(selectedValueMaquina), version, 3);
                string imagen4 = conexion.leerImagenes(Convert.ToInt32(tbFicha.Value), Convert.ToInt32(selectedValueMaquina), version, 4);
                string imagen5 = conexion.leerImagenes(Convert.ToInt32(tbFicha.Value), Convert.ToInt32(selectedValueMaquina), version, 5);
                string imagen6 = conexion.leerImagenes(Convert.ToInt32(tbFicha.Value), Convert.ToInt32(selectedValueMaquina), version, 6);
                string imagen7 = conexion.leerImagenes(Convert.ToInt32(tbFicha.Value), Convert.ToInt32(selectedValueMaquina), version, 7);
                string imagen8 = conexion.leerImagenes(Convert.ToInt32(tbFicha.Value), Convert.ToInt32(selectedValueMaquina), version, 8);
                if (imagen1 != "")
                {
                    hyperlink1.NavigateUrl = imagen1;
                    hyperlink1.ImageUrl = imagen1;
                    //img1.Src = imagen1;
                }
                else
                {
                    hyperlink1.NavigateUrl = "../SMARTH_docs/PARAMETROS/sin_imagen.jpg";
                    hyperlink1.ImageUrl = "../SMARTH_docs/PARAMETROS/sin_imagen.jpg"; 
                    //img1.Src = "../SMARTH_docs/PARAMETROS/sin_imagen.jpg";
                }
                if (imagen2 != "")
                {
                    hyperlink2.NavigateUrl = imagen2;
                    hyperlink2.ImageUrl = imagen2;
                    //img2.Src = imagen2;
                }
                else
                {
                    hyperlink2.NavigateUrl = "../SMARTH_docs/PARAMETROS/sin_imagen.jpg";
                    hyperlink2.ImageUrl = "../SMARTH_docs/PARAMETROS/sin_imagen.jpg";
                    //img2.Src = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";
                }
                if (imagen3 != "")
                {
                    hyperlink3.NavigateUrl = imagen3;
                    hyperlink3.ImageUrl = imagen3;
                    //img3.Src = imagen3;
                }
                else
                {
                    hyperlink3.NavigateUrl = "../SMARTH_docs/PARAMETROS/sin_imagen.jpg";
                    hyperlink3.ImageUrl = "../SMARTH_docs/PARAMETROS/sin_imagen.jpg";
                    //img3.Src = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";
                }
                if (imagen4 != "")
                {
                    hyperlink4.NavigateUrl = imagen4;
                    hyperlink4.ImageUrl = imagen4;
                    //img4.Src = imagen4;
                }
                else
                {
                    hyperlink4.NavigateUrl = "../SMARTH_docs/PARAMETROS/sin_imagen.jpg";
                    hyperlink4.ImageUrl = "../SMARTH_docs/PARAMETROS/sin_imagen.jpg";
                    //img4.Src = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";
                }
                if (imagen5 != "")
                {
                    hyperlink5.NavigateUrl = imagen5;
                    hyperlink5.ImageUrl = imagen5;
                    //img5.Src = imagen5;
                }
                else
                {
                    hyperlink5.NavigateUrl = "../SMARTH_docs/PARAMETROS/sin_imagen.jpg";
                    hyperlink5.ImageUrl = "../SMARTH_docs/PARAMETROS/sin_imagen.jpg";
                    //img5.Src = "../SMARTH_docs/PARAMETROS/sin_imagen.jpg";
                }
                if (imagen6 != "")
                {
                    hyperlink6.NavigateUrl = imagen6;
                    hyperlink6.ImageUrl = imagen6;
                    //img6.Src = imagen6;
                }
                else
                {
                    hyperlink6.NavigateUrl = "../SMARTH_docs/PARAMETROS/sin_imagen.jpg";
                    hyperlink6.ImageUrl = "../SMARTH_docs/PARAMETROS/sin_imagen.jpg";
                    //img6.Src = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";
                }
                if (imagen7 != "")
                {
                    hyperlink7.NavigateUrl = imagen7;
                    hyperlink7.ImageUrl = imagen7;
                    //img7.Src = imagen7;
                }
                else
                {
                    hyperlink7.NavigateUrl = "../SMARTH_docs/PARAMETROS/sin_imagen.jpg";
                    hyperlink7.ImageUrl = "../SMARTH_docs/PARAMETROS/sin_imagen.jpg";
                    //img7.Src = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";
                }
                if (imagen8 != "")
                {
                    hyperlink8.NavigateUrl = imagen8;
                    hyperlink8.ImageUrl = imagen8;
                    //img8.Src = imagen8;
                }
                else
                {
                    hyperlink8.NavigateUrl = "../SMARTH_docs/PARAMETROS/sin_imagen.jpg";
                    hyperlink8.ImageUrl = "../SMARTH_docs/PARAMETROS/sin_imagen.jpg";
                    //img8.Src = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";
                }

                //UBICACIÓN DE MOLDE
                DataTable Molde = SHConexion.Devuelve_Molde(FichaParam.Rows[0]["CodMolde"].ToString());
                UbicaMolde.InnerText = FichaParam.Rows[0]["CodMolde"].ToString();
                UbicaMoldeNombre.InnerText = Molde.Rows[0]["Descripcion"].ToString();
                UbicacionMolde.Text = Molde.Rows[0]["Ubicacion"].ToString();
                LblModificado.Text = "Modificado: " + Molde.Rows[0]["FechaModificaUbicacion"].ToString();
                flexCheckDefault.Checked = Convert.ToBoolean(Molde.Rows[0]["Activo"]);
                ImgUbicacion.Src = Molde.Rows[0]["ImagenUbi"].ToString();
                BTNModalUbicacion.Disabled = false;

                //ASIGNACION MANO
                
                DataTable MoldexMano = SHConexion.Devuelve_listado_Manos_X_Molde(tbCodigoMolde.Text);
                AsignaMano.InnerText = tbCodigoMolde.Text;
                AsignaManoNombre.InnerText = MoldexMano.Rows[0]["Descripcion"].ToString();
                string ubicacion = MoldexMano.Rows[0]["AREAMANO"].ToString();
                switch (ubicacion)
                {
                    case "1":
                        ubicacion = "(Obsoleto - Pos: " + MoldexMano.Rows[0]["AREAMANO"].ToString() + ")";
                        break;
                    case "2":
                        ubicacion = "(Cuarto de manos - Pos: " + MoldexMano.Rows[0]["AREAMANO"].ToString() + ")";
                        break;
                    case "3":
                        ubicacion = "(Máquina 34 - Pos: " + MoldexMano.Rows[0]["AREAMANO"].ToString() + ")";
                        break;
                    case "4":
                        ubicacion = "(Cubetas estantería - Pos: " + MoldexMano.Rows[0]["AREAMANO"].ToString() + ")";
                        break;
                    case "5":
                        ubicacion = "(Máquina 32 - Pos: " + MoldexMano.Rows[0]["AREAMANO"].ToString() + ")";
                        break;
                    case "6":
                        ubicacion = "(Máquina 48 - Pos: " + MoldexMano.Rows[0]["AREAMANO"].ToString() + ")";
                        break;
                    case "7":
                        ubicacion = "(Máquina 43 - Pos: " + MoldexMano.Rows[0]["AREAMANO"].ToString() + ")"; ;
                        break;
                }
                InputManoActual.Value = MoldexMano.Rows[0]["Mano"].ToString() + " - " + MoldexMano.Rows[0]["MANDESCRIPCION"].ToString() + " " + ubicacion;

            }
            catch (Exception EX)
            {
            }
        }


        //ACTUALIZACIÓN DE FICHAS
    
        public void Actualizar_Mano(object sender, EventArgs e)
        {
            try
            {
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();

                //AsignaMano.InnerText
                string[] RecorteMano = InputAsignaNuevaMano.Value.Split(new char[] { '_' });
                int mano = 0;
                try
                {
                    mano = Convert.ToInt32(RecorteMano[0].ToString());
                }
                catch (Exception)
                {
                    mano = 0;
                }
                RecorteMano[0].ToString();
                SHConexion.ActualizarMoldeXMano(AsignaMano.InnerText, mano);

                DataTable MoldexMano = SHConexion.Devuelve_listado_Manos_X_Molde(tbCodigoMolde.Text);
                string ubicacion = MoldexMano.Rows[0]["AREAMANO"].ToString();
                switch (ubicacion)
                {
                    case "1":
                        ubicacion = "(Obsoleto - Pos: " + MoldexMano.Rows[0]["AREAMANO"].ToString() + ")";
                        break;
                    case "2":
                        ubicacion = "(Cuarto de manos - Pos: " + MoldexMano.Rows[0]["AREAMANO"].ToString() + ")";
                        break;
                    case "3":
                        ubicacion = "(Máquina 34 - Pos: " + MoldexMano.Rows[0]["AREAMANO"].ToString() + ")";
                        break;
                    case "4":
                        ubicacion = "(Cubetas estantería - Pos: " + MoldexMano.Rows[0]["AREAMANO"].ToString() + ")";
                        break;
                    case "5":
                        ubicacion = "(Máquina 32 - Pos: " + MoldexMano.Rows[0]["AREAMANO"].ToString() + ")";
                        break;
                    case "6":
                        ubicacion = "(Máquina 48 - Pos: " + MoldexMano.Rows[0]["AREAMANO"].ToString() + ")";
                        break;
                    case "7":
                        ubicacion = "(Máquina 43 - Pos: " + MoldexMano.Rows[0]["AREAMANO"].ToString() + ")"; ;
                        break;
                }

                tbManoRobot.Text = MoldexMano.Rows[0]["Mano"].ToString();
                tbUbicacionMano.Text = ubicacion;
                

                CancelarModificacion(null,null);
                CargarFicha_function();
                //ds_listadomoldesCOMP = SHConexion.Devuelve_lista_moldes_numReparaciones(" AND MOL.Activo = 1");
                //Rellenar_grid();
                //lkb_Sort_Click("LISTAMOLDES");

            }
            catch (Exception ex)
            { }
        }

        public void Recalcular_Limites(object sender, EventArgs e)
        {
            try
            {
                HtmlButton button = (HtmlButton)sender;
                string name = button.ID;
                switch (name)
                {
                    case "reclimTiempoInyeccion":
                        double TiempoInyeccion = Convert.ToDouble(tbTiempoInyeccion.Text.Replace('.', ','));
                        tbTiempoInyeccionNVal.Text = (TiempoInyeccion * 0.9).ToString();
                        tbTiempoInyeccionMVal.Text = (TiempoInyeccion * 1.1).ToString();
                        break;

                    case "reclimLimitePresion":
                        double LimitePresion = Convert.ToDouble(tbLimitePresion.Text.Replace('.', ','));
                        tbLimitePresionNVal.Text = (LimitePresion * 0.9).ToString();
                        tbLimitePresionMval.Text = (LimitePresion * 1.1).ToString();
                        double LimitePresionREAL = Convert.ToDouble(tbLimitePresionReal.Text.Replace('.', ','));
                        tbLimitePresionNRealVal.Text = (LimitePresionREAL * 0.9).ToString();
                        tbLimitePresionMRealVal.Text = (LimitePresionREAL * 1.1).ToString();
                        break;

                    case "reclimtbConmutacion":
                        double LimiteConmutacion = Convert.ToDouble(tbConmutacion.Text.Replace('.', ','));
                        thConmuntaciontolNVal.Text = (LimiteConmutacion * 0.9).ToString();
                        thConmuntaciontolMVal.Text = (LimiteConmutacion * 1.1).ToString();
                        break;

                    case "reclimtbTiempoPresion":
                        double limiteTiempoPresion = Convert.ToDouble(tbTiempoPresion.Text.Replace('.', ','));
                        tbTiempoPresiontolNVal.Text = (limiteTiempoPresion * 0.9).ToString();
                        tbTiempoPresiontolMVal.Text = (limiteTiempoPresion * 1.1).ToString();
                        break;

                    case "recalclimthVCarga":
                        double limiteVCarga = Convert.ToDouble(thVCarga.Text.Replace('.', ','));
                        TNvcargaval.Text = (limiteVCarga * 0.9).ToString();
                        TMvcargaval.Text = (limiteVCarga * 1.1).ToString();
                        break;

                    case "recalclimthCarga":
                        double limiteCarga = Convert.ToDouble(thCarga.Text.Replace('.', ','));
                        TNcargaval.Text = (limiteCarga * 0.9).ToString();
                        TMcargaval.Text = (limiteCarga * 1.1).ToString();
                        break;

                    case "recalclimthDescomp":
                        double limDescomp = Convert.ToDouble(thDescomp.Text.Replace('.', ','));
                        TNdescomval.Text = (limDescomp * 0.9).ToString();
                        TMdescomval.Text = (limDescomp * 1.1).ToString();
                        break;

                    case "recalclimthContrapr":
                        double limcontrapresion = Convert.ToDouble(thContrapr.Text.Replace('.', ','));
                        TNcontrapval.Text = (limcontrapresion * 0.9).ToString();
                        TMcontrapval.Text = (limcontrapresion * 1.1).ToString();
                        break;

                    case "recalclimthTiempo":
                        double limTiempo = Convert.ToDouble(thTiempo.Text.Replace('.', ','));
                        TNTiempdosval.Text = (limTiempo * 0.9).ToString();
                        TMTiempdosval.Text = (limTiempo * 1.1).ToString();
                        break;

                    case "recalcthEnfriamiento":
                        double limEnfriamiento = Convert.ToDouble(thEnfriamiento.Text.Replace('.', ','));
                        TNEnfriamval.Text = (limEnfriamiento * 0.9).ToString();
                        TMEnfriamval.Text = (limEnfriamiento * 1.1).ToString();
                        break;

                    case "recalclimthCiclo":
                        double limCiclo = Convert.ToDouble(thCiclo.Text.Replace('.', ','));
                        TNCicloval.Text = (limCiclo * 0.9).ToString();
                        TMCicloval.Text = (limCiclo * 1.1).ToString();
                        break;

                    case "recalclimthCojin":
                        double limCojin = Convert.ToDouble(thCojin.Text.Replace('.', ','));
                        TNCojinval.Text = (limCojin * 0.19).ToString();
                        TMCojinval.Text = (limCojin * 1.1).ToString();
                        break;

                    case "recalfull":

                        try
                        {
                            tbTiempoInyeccion.BackColor = System.Drawing.Color.Transparent;
                            TiempoInyeccion = Convert.ToDouble(tbTiempoInyeccion.Text.Replace('.', ','));
                            tbTiempoInyeccionNVal.Text = (TiempoInyeccion * 0.9).ToString();
                            tbTiempoInyeccionMVal.Text = (TiempoInyeccion * 1.1).ToString();
                        }
                        catch (Exception)
                        {
                            tbTiempoInyeccion.BackColor = System.Drawing.Color.Red;
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "ErrorRecalculando();", true);
                            break;
                        }

                        try
                        {
                            tbLimitePresion.BackColor = System.Drawing.Color.Transparent;
                            LimitePresion = Convert.ToDouble(tbLimitePresion.Text.Replace('.', ','));
                            tbLimitePresionNVal.Text = (LimitePresion * 0.9).ToString();
                            tbLimitePresionMval.Text = (LimitePresion * 1.1).ToString();

                            LimitePresionREAL = Convert.ToDouble(tbLimitePresionReal.Text.Replace('.', ','));
                            tbLimitePresionNRealVal.Text = (LimitePresionREAL * 0.9).ToString();
                            tbLimitePresionMRealVal.Text = (LimitePresionREAL * 1.1).ToString();
                        }
                        catch (Exception)
                        {
                            tbLimitePresion.BackColor = System.Drawing.Color.Red;
                            tbLimitePresionReal.BackColor = System.Drawing.Color.Red;
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "ErrorRecalculando();", true);
                            break;
                        }

                        try
                        {
                            tbConmutacion.BackColor = System.Drawing.Color.Transparent;
                            LimiteConmutacion = Convert.ToDouble(tbConmutacion.Text.Replace('.', ','));
                            thConmuntaciontolNVal.Text = (LimiteConmutacion * 0.9).ToString();
                            thConmuntaciontolMVal.Text = (LimiteConmutacion * 1.1).ToString();
                        }
                        catch (Exception)
                        {
                            tbConmutacion.BackColor = System.Drawing.Color.Red;
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "ErrorRecalculando();", true);
                            break;
                        }

                        try
                        {
                            tbTiempoPresion.BackColor = System.Drawing.Color.Transparent;
                            limiteTiempoPresion = Convert.ToDouble(tbTiempoPresion.Text.Replace('.', ','));
                            tbTiempoPresiontolNVal.Text = (limiteTiempoPresion * 0.9).ToString();
                            tbTiempoPresiontolMVal.Text = (limiteTiempoPresion * 1.1).ToString();
                        }
                        catch (Exception)
                        {
                            tbTiempoPresion.BackColor = System.Drawing.Color.Red;
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "ErrorRecalculando();", true);
                            break;
                        }

                        try
                        {
                            thVCarga.BackColor = System.Drawing.Color.Transparent;
                            limiteVCarga = Convert.ToDouble(thVCarga.Text.Replace('.', ','));
                            TNvcargaval.Text = (limiteVCarga * 0.9).ToString();
                            TMvcargaval.Text = (limiteVCarga * 1.1).ToString();
                        }
                        catch (Exception)
                        {
                            thVCarga.BackColor = System.Drawing.Color.Red;
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "ErrorRecalculando();", true);
                            break;
                        }

                        try
                        {
                            thDescomp.BackColor = System.Drawing.Color.Transparent;
                            limDescomp = Convert.ToDouble(thDescomp.Text.Replace('.', ','));
                            TNdescomval.Text = (limDescomp * 0.9).ToString();
                            TMdescomval.Text = (limDescomp * 1.1).ToString();
                        }
                        catch (Exception)
                        {
                            thDescomp.BackColor = System.Drawing.Color.Red;
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "ErrorRecalculando();", true);
                            break;
                        }

                        try
                        {
                            thCarga.BackColor = System.Drawing.Color.Transparent;
                            limiteCarga = Convert.ToDouble(thCarga.Text.Replace('.', ','));
                            TNcargaval.Text = (limiteCarga * 0.9).ToString();
                            TMcargaval.Text = (limiteCarga * 1.1).ToString();
                        }
                        catch (Exception)
                        {
                            thCarga.BackColor = System.Drawing.Color.Red;
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "ErrorRecalculando();", true);
                            break;
                        }

                        try
                        {
                            thContrapr.BackColor = System.Drawing.Color.Transparent;
                            limcontrapresion = Convert.ToDouble(thContrapr.Text.Replace('.', ','));
                            TNcontrapval.Text = (limcontrapresion * 0.9).ToString();
                            TMcontrapval.Text = (limcontrapresion * 1.1).ToString();
                        }
                        catch (Exception)
                        {
                            thContrapr.BackColor = System.Drawing.Color.Red;
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "ErrorRecalculando();", true);
                            break;
                        }

                        try
                        {
                            thEnfriamiento.BackColor = System.Drawing.Color.Transparent;
                            limEnfriamiento = Convert.ToDouble(thEnfriamiento.Text.Replace('.', ','));
                            TNEnfriamval.Text = (limEnfriamiento * 0.9).ToString();
                            TMEnfriamval.Text = (limEnfriamiento * 1.1).ToString();
                        }
                        catch (Exception)
                        {
                            thEnfriamiento.BackColor = System.Drawing.Color.Red;
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "ErrorRecalculando();", true);
                            break;
                        }

                        try
                        {
                            thCiclo.BackColor = System.Drawing.Color.Transparent;
                            limCiclo = Convert.ToDouble(thCiclo.Text.Replace('.', ','));
                            TNCicloval.Text = (limCiclo * 0.9).ToString();
                            TMCicloval.Text = (limCiclo * 1.1).ToString();
                        }
                        catch (Exception)
                        {
                            thCiclo.BackColor = System.Drawing.Color.Red;
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "ErrorRecalculando();", true);
                            break;
                        }

                        try
                        {
                            thCojin.BackColor = System.Drawing.Color.Transparent;
                            limCojin = Convert.ToDouble(thCojin.Text.Replace('.', ','));
                            TNCojinval.Text = (limCojin * 0.9).ToString();
                            TMCojinval.Text = (limCojin * 1.1).ToString();
                        }
                        catch (Exception)
                        {
                            thCojin.BackColor = System.Drawing.Color.Red;
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "ErrorRecalculando();", true);
                            break;
                        }

                        try
                        {
                            thTiempo.BackColor = System.Drawing.Color.Transparent;
                            limTiempo = Convert.ToDouble(thTiempo.Text.Replace('.', ','));
                            TNTiempdosval.Text = (limTiempo * 0.9).ToString();
                            TMTiempdosval.Text = (limTiempo * 1.1).ToString();

                        }
                        catch (Exception)
                        {
                            thTiempo.BackColor = System.Drawing.Color.Red;
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "ErrorRecalculando();", true);
                            break;
                        }


                        GuardarFicha(null, null);
                        break;
                }

            }
            catch { }
        }

        public void Validar_ficha(object sender, EventArgs e)
        {
            try
            {
                if (tbReferencia.Text != "" && tbMaquina.Text != "")
                {
                    Conexion conexion = new Conexion();
                    Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                    conexion.ValidarFicha(Convert.ToInt32(tbReferencia.Text), Convert.ToInt32(tbMaquina.Text), Convert.ToInt32(cbEdicion.Text), tbObservaciones.Text, tbRazones.Text, SHConexion.Devuelve_ID_Piloto_SMARTH(cbElaboradoPor.SelectedValue.ToString()), SHConexion.Devuelve_ID_Piloto_SMARTH(cbRevisadoPor.SelectedValue.ToString()), SHConexion.Devuelve_ID_Piloto_SMARTH(cbAprobadoPor.SelectedValue.ToString()));
                }
            }
            catch (Exception)
            {
            }

        }
       
        public void GuardarFicha(object sender, EventArgs e)
        {
            try
            {
                Conexion conexion = new Conexion();
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                int noyos = 0;
                int hembra = 0;
                int macho = 0;
                int antesExpul = 0;
                int antesApert = 0;
                int antesCierre = 0;
                int despuesCierre = 0;
                int otros = 0;
                int boquilla = 0;
                int cono = 0;
                int radioLarga = 0;
                int libre = 0;
                int valvula = 0;
                int resistencia = 0;
                int otros2 = 0;
                int expulsion = 0;
                int hidraulica = 0;
                int neumatica = 0;
                int normal = 0;
                int arandela125 = 0;
                int arandela160 = 0;
                int arandela200 = 0;
                int arandela250 = 0;
                double thBoq_double = 0;
                double thT1_double = 0;
                double thT2_double = 0;
                double thT3_double = 0;
                double thT4_double = 0;
                double thT5_double = 0;
                double thT6_double = 0;
                double thT7_double = 0;
                double thT8_double = 0;
                double thT9_double = 0;
                double thT10_double = 0;
                double thZ1_double = 0;
                double thZ2_double = 0;
                double thZ3_double = 0;
                double thZ4_double = 0;
                double thZ5_double = 0;
                double thZ6_double = 0;
                double thZ7_double = 0;
                double thZ8_double = 0;
                double thZ9_double = 0;
                double thZ10_double = 0;
                double thZ11_double = 0;
                double thZ12_double = 0;
                double thZ13_double = 0;
                double thZ14_double = 0;
                double thZ15_double = 0;
                double thZ16_double = 0;
                double thZ17_double = 0;
                double thZ18_double = 0;
                double thZ19_double = 0;
                double thZ20_double = 0;
                double thV1_double = 0;
                double thV2_double = 0;
                double thV3_double = 0;
                double thV4_double = 0;
                double thV5_double = 0;
                double thV6_double = 0;
                double thV7_double = 0;
                double thV8_double = 0;
                double thV9_double = 0;
                double thV10_double = 0;
                double thV11_double = 0;
                double thV12_double = 0;
                double thC1_double = 0;
                double thC2_double = 0;
                double thC3_double = 0;
                double thC4_double = 0;
                double thC5_double = 0;
                double thC6_double = 0;
                double thC7_double = 0;
                double thC8_double = 0;
                double thC9_double = 0;
                double thC10_double = 0;
                double thC11_double = 0;
                double thC12_double = 0;
                double thP1_double = 0;
                double thP2_double = 0;
                double thP3_double = 0;
                double thP4_double = 0;
                double thP5_double = 0;
                double thP6_double = 0;
                double thP7_double = 0;
                double thP8_double = 0;
                double thP9_double = 0;
                double thP10_double = 0;
                double thTP1_double = 0;
                double thTP2_double = 0;
                double thTP3_double = 0;
                double thTP4_double = 0;
                double thTP5_double = 0;
                double thTP6_double = 0;
                double thTP7_double = 0;
                double thTP8_double = 0;
                double thTP9_double = 0;
                double thTP10_double = 0;
                //int thCodMaterial_int = 0;

                if (Double.TryParse(thBoq.Text.Replace('.', ','), out thBoq_double))
                    thBoq_double = Convert.ToDouble(thBoq.Text.Replace('.', ','));
                else
                    thBoq_double = 0.0;
                if (Double.TryParse(thT1.Text.Replace('.', ','), out thT1_double))
                    thT1_double = Convert.ToDouble(thT1.Text.Replace('.', ','));
                else
                    thT1_double = 0.0;
                if (Double.TryParse(thT2.Text.Replace('.', ','), out thT2_double))
                    thT2_double = Convert.ToDouble(thT2.Text.Replace('.', ','));
                else
                    thT2_double = 0.0;
                if (Double.TryParse(thT3.Text.Replace('.', ','), out thT3_double))
                    thT3_double = Convert.ToDouble(thT3.Text.Replace('.', ','));
                else
                    thT3_double = 0.0;
                if (Double.TryParse(thT4.Text.Replace('.', ','), out thT4_double))
                    thT4_double = Convert.ToDouble(thT4.Text.Replace('.', ','));
                else
                    thT4_double = 0.0;
                if (Double.TryParse(thT5.Text.Replace('.', ','), out thT5_double))
                    thT5_double = Convert.ToDouble(thT5.Text.Replace('.', ','));
                else
                    thT5_double = 0.0;
                if (Double.TryParse(thT6.Text.Replace('.', ','), out thT6_double))
                    thT6_double = Convert.ToDouble(thT6.Text.Replace('.', ','));
                else
                    thT6_double = 0.0;
                if (Double.TryParse(thT7.Text.Replace('.', ','), out thT7_double))
                    thT7_double = Convert.ToDouble(thT7.Text.Replace('.', ','));
                else
                    thT7_double = 0.0;
                if (Double.TryParse(thT8.Text.Replace('.', ','), out thT8_double))
                    thT8_double = Convert.ToDouble(thT8.Text.Replace('.', ','));
                else
                    thT8_double = 0.0;
                if (Double.TryParse(thT9.Text.Replace('.', ','), out thT9_double))
                    thT9_double = Convert.ToDouble(thT9.Text.Replace('.', ','));
                else
                    thT9_double = 0.0;
                if (Double.TryParse(thT10.Text.Replace('.', ','), out thT10_double))
                    thT10_double = Convert.ToDouble(thT10.Text.Replace('.', ','));
                else
                    thT10_double = 0.0;
                if (Double.TryParse(thZ1.Text.Replace('.', ','), out thZ1_double))
                    thZ1_double = Convert.ToDouble(thZ1.Text.Replace('.', ','));
                else
                    thZ1_double = 0.0;
                if (Double.TryParse(thZ2.Text.Replace('.', ','), out thZ2_double))
                    thZ2_double = Convert.ToDouble(thZ2.Text.Replace('.', ','));
                else
                    thZ2_double = 0.0;
                if (Double.TryParse(thZ3.Text.Replace('.', ','), out thZ3_double))
                    thZ3_double = Convert.ToDouble(thZ3.Text.Replace('.', ','));
                else
                    thZ3_double = 0.0;
                if (Double.TryParse(thZ4.Text.Replace('.', ','), out thZ4_double))
                    thZ4_double = Convert.ToDouble(thZ4.Text.Replace('.', ','));
                else
                    thZ4_double = 0.0;
                if (Double.TryParse(thZ5.Text.Replace('.', ','), out thZ5_double))
                    thZ5_double = Convert.ToDouble(thZ5.Text.Replace('.', ','));
                else
                    thZ5_double = 0.0;
                if (Double.TryParse(thZ6.Text.Replace('.', ','), out thZ6_double))
                    thZ6_double = Convert.ToDouble(thZ6.Text.Replace('.', ','));
                else
                    thZ6_double = 0.0;
                if (Double.TryParse(thZ7.Text.Replace('.', ','), out thZ7_double))
                    thZ7_double = Convert.ToDouble(thZ7.Text.Replace('.', ','));
                else
                    thZ7_double = 0.0;
                if (Double.TryParse(thZ8.Text.Replace('.', ','), out thZ8_double))
                    thZ8_double = Convert.ToDouble(thZ8.Text.Replace('.', ','));
                else
                    thZ8_double = 0.0;
                if (Double.TryParse(thZ9.Text.Replace('.', ','), out thZ9_double))
                    thZ9_double = Convert.ToDouble(thZ9.Text.Replace('.', ','));
                else
                    thZ9_double = 0.0;
                if (Double.TryParse(thZ10.Text.Replace('.', ','), out thZ10_double))
                    thZ10_double = Convert.ToDouble(thZ10.Text.Replace('.', ','));
                else
                    thZ10_double = 0.0;
                if (Double.TryParse(thZ11.Text.Replace('.', ','), out thZ11_double))
                    thZ11_double = Convert.ToDouble(thZ11.Text.Replace('.', ','));
                else
                    thZ11_double = 0.0;
                if (Double.TryParse(thZ12.Text.Replace('.', ','), out thZ12_double))
                    thZ12_double = Convert.ToDouble(thZ12.Text.Replace('.', ','));
                else
                    thZ12_double = 0.0;
                if (Double.TryParse(thZ13.Text.Replace('.', ','), out thZ13_double))
                    thZ13_double = Convert.ToDouble(thZ13.Text.Replace('.', ','));
                else
                    thZ13_double = 0.0;
                if (Double.TryParse(thZ14.Text.Replace('.', ','), out thZ14_double))
                    thZ14_double = Convert.ToDouble(thZ14.Text.Replace('.', ','));
                else
                    thZ14_double = 0.0;
                if (Double.TryParse(thZ15.Text.Replace('.', ','), out thZ15_double))
                    thZ15_double = Convert.ToDouble(thZ15.Text.Replace('.', ','));
                else
                    thZ15_double = 0.0;
                if (Double.TryParse(thZ16.Text.Replace('.', ','), out thZ16_double))
                    thZ16_double = Convert.ToDouble(thZ16.Text.Replace('.', ','));
                else
                    thZ16_double = 0.0;
                if (Double.TryParse(thZ17.Text.Replace('.', ','), out thZ17_double))
                    thZ17_double = Convert.ToDouble(thZ17.Text.Replace('.', ','));
                else
                    thZ17_double = 0.0;
                if (Double.TryParse(thZ18.Text.Replace('.', ','), out thZ18_double))
                    thZ18_double = Convert.ToDouble(thZ18.Text.Replace('.', ','));
                else
                    thZ18_double = 0.0;
                if (Double.TryParse(thZ19.Text.Replace('.', ','), out thZ19_double))
                    thZ19_double = Convert.ToDouble(thZ19.Text.Replace('.', ','));
                else
                    thZ19_double = 0.0;
                if (Double.TryParse(thZ20.Text.Replace('.', ','), out thZ20_double))
                    thZ20_double = Convert.ToDouble(thZ20.Text.Replace('.', ','));
                else
                    thZ20_double = 0.0;
                if (Double.TryParse(thV1.Text.Replace('.', ','), out thV1_double))
                    thV1_double = Convert.ToDouble(thV1.Text.Replace('.', ','));
                else
                    thV1_double = 0.0;
                if (Double.TryParse(thV2.Text.Replace('.', ','), out thV2_double))
                    thV2_double = Convert.ToDouble(thV2.Text.Replace('.', ','));
                else
                    thV2_double = 0.0;
                if (Double.TryParse(thV3.Text.Replace('.', ','), out thV3_double))
                    thV3_double = Convert.ToDouble(thV3.Text.Replace('.', ','));
                else
                    thV3_double = 0.0;
                if (Double.TryParse(thV4.Text.Replace('.', ','), out thV4_double))
                    thV4_double = Convert.ToDouble(thV4.Text.Replace('.', ','));
                else
                    thV4_double = 0.0;
                if (Double.TryParse(thV5.Text.Replace('.', ','), out thV5_double))
                    thV5_double = Convert.ToDouble(thV5.Text.Replace('.', ','));
                else
                    thV5_double = 0.0;
                if (Double.TryParse(thV6.Text.Replace('.', ','), out thV6_double))
                    thV6_double = Convert.ToDouble(thV6.Text.Replace('.', ','));
                else
                    thV6_double = 0.0;
                if (Double.TryParse(thV7.Text.Replace('.', ','), out thV7_double))
                    thV7_double = Convert.ToDouble(thV7.Text.Replace('.', ','));
                else
                    thV7_double = 0.0;
                if (Double.TryParse(thV8.Text.Replace('.', ','), out thV8_double))
                    thV8_double = Convert.ToDouble(thV8.Text.Replace('.', ','));
                else
                    thV8_double = 0.0;
                if (Double.TryParse(thV9.Text.Replace('.', ','), out thV9_double))
                    thV9_double = Convert.ToDouble(thV9.Text.Replace('.', ','));
                else
                    thV9_double = 0.0;
                if (Double.TryParse(thV10.Text.Replace('.', ','), out thV10_double))
                    thV10_double = Convert.ToDouble(thV10.Text.Replace('.', ','));
                else
                    thV10_double = 0.0;
                if (Double.TryParse(thV11.Text.Replace('.', ','), out thV11_double))
                    thV11_double = Convert.ToDouble(thV11.Text.Replace('.', ','));
                else
                    thV11_double = 0.0;
                if (Double.TryParse(thV12.Text.Replace('.', ','), out thV12_double))
                    thV12_double = Convert.ToDouble(thV12.Text.Replace('.', ','));
                else
                    thV12_double = 0.0;
                if (Double.TryParse(thC1.Text.Replace('.', ','), out thC1_double))
                    thC1_double = Convert.ToDouble(thC1.Text.Replace('.', ','));
                else
                    thC1_double = 0.0;
                if (Double.TryParse(thC2.Text.Replace('.', ','), out thC2_double))
                    thC2_double = Convert.ToDouble(thC2.Text.Replace('.', ','));
                else
                    thC2_double = 0.0;
                if (Double.TryParse(thC3.Text.Replace('.', ','), out thC3_double))
                    thC3_double = Convert.ToDouble(thC3.Text.Replace('.', ','));
                else
                    thC3_double = 0.0;
                if (Double.TryParse(thC4.Text.Replace('.', ','), out thC4_double))
                    thC4_double = Convert.ToDouble(thC4.Text.Replace('.', ','));
                else
                    thC4_double = 0.0;
                if (Double.TryParse(thC5.Text.Replace('.', ','), out thC5_double))
                    thC5_double = Convert.ToDouble(thC5.Text.Replace('.', ','));
                else
                    thC5_double = 0.0;
                if (Double.TryParse(thC6.Text.Replace('.', ','), out thC6_double))
                    thC6_double = Convert.ToDouble(thC6.Text.Replace('.', ','));
                else
                    thC6_double = 0.0;
                if (Double.TryParse(thC7.Text.Replace('.', ','), out thC7_double))
                    thC7_double = Convert.ToDouble(thC7.Text.Replace('.', ','));
                else
                    thC7_double = 0.0;
                if (Double.TryParse(thC8.Text.Replace('.', ','), out thC8_double))
                    thC8_double = Convert.ToDouble(thC8.Text.Replace('.', ','));
                else
                    thC8_double = 0.0;
                if (Double.TryParse(thC9.Text.Replace('.', ','), out thC9_double))
                    thC9_double = Convert.ToDouble(thC9.Text.Replace('.', ','));
                else
                    thC9_double = 0.0;
                if (Double.TryParse(thC10.Text.Replace('.', ','), out thC10_double))
                    thC10_double = Convert.ToDouble(thC10.Text.Replace('.', ','));
                else
                    thC10_double = 0.0;
                if (Double.TryParse(thC11.Text.Replace('.', ','), out thC11_double))
                    thC11_double = Convert.ToDouble(thC11.Text.Replace('.', ','));
                else
                    thC11_double = 0.0;
                if (Double.TryParse(thC12.Text.Replace('.', ','), out thC12_double))
                    thC12_double = Convert.ToDouble(thC12.Text.Replace('.', ','));
                else
                    thC12_double = 0.0;
                if (Double.TryParse((thP1.Text).Replace('.', ','), out thP1_double))
                    thP1_double = Convert.ToDouble(thP1.Text.Replace('.', ','));
                else
                    thP1_double = 0.0;
                if (Double.TryParse((thP2.Text).Replace('.', ','), out thP2_double))
                    thP2_double = Convert.ToDouble(thP2.Text.Replace('.', ','));
                else
                    thP2_double = 0.0;
                if (Double.TryParse(thP3.Text.Replace('.', ','), out thP3_double))
                    thP3_double = Convert.ToDouble(thP3.Text.Replace('.', ','));
                else
                    thP3_double = 0.0;
                if (Double.TryParse(thP4.Text.Replace('.', ','), out thP4_double))
                    thP4_double = Convert.ToDouble(thP4.Text.Replace('.', ','));
                else
                    thP4_double = 0.0;
                if (Double.TryParse(thP5.Text.Replace('.', ','), out thP5_double))
                    thP5_double = Convert.ToDouble(thP5.Text.Replace('.', ','));
                else
                    thP5_double = 0.0;
                if (Double.TryParse(thP6.Text.Replace('.', ','), out thP6_double))
                    thP6_double = Convert.ToDouble(thP6.Text.Replace('.', ','));
                else
                    thP6_double = 0.0;
                if (Double.TryParse(thP7.Text.Replace('.', ','), out thP7_double))
                    thP7_double = Convert.ToDouble(thP7.Text.Replace('.', ','));
                else
                    thP7_double = 0.0;
                if (Double.TryParse(thP8.Text.Replace('.', ','), out thP8_double))
                    thP8_double = Convert.ToDouble(thP8.Text.Replace('.', ','));
                else
                    thP8_double = 0.0;
                if (Double.TryParse(thP9.Text.Replace('.', ','), out thP9_double))
                    thP9_double = Convert.ToDouble(thP9.Text.Replace('.', ','));
                else
                    thP9_double = 0.0;
                if (Double.TryParse(thP10.Text.Replace('.', ','), out thP10_double))
                    thP10_double = Convert.ToDouble(thP10.Text.Replace('.', ','));
                else
                    thP10_double = 0.0;
                if (Double.TryParse(thTP1.Text.Replace('.', ','), out thTP1_double))
                    thTP1_double = Convert.ToDouble(thTP1.Text.Replace('.', ','));
                else
                    thTP1_double = 0.0;
                if (Double.TryParse(thTP2.Text.Replace('.', ','), out thTP2_double))
                    thTP2_double = Convert.ToDouble(thTP2.Text.Replace('.', ','));
                else
                    thTP2_double = 0.0;
                if (Double.TryParse(thTP3.Text.Replace('.', ','), out thTP3_double))
                    thTP3_double = Convert.ToDouble(thTP3.Text.Replace('.', ','));
                else
                    thTP3_double = 0.0;
                if (Double.TryParse(thTP4.Text.Replace('.', ','), out thTP4_double))
                    thTP4_double = Convert.ToDouble(thTP4.Text.Replace('.', ','));
                else
                    thTP4_double = 0.0;
                if (Double.TryParse(thTP5.Text.Replace('.', ','), out thTP5_double))
                    thTP5_double = Convert.ToDouble(thTP5.Text.Replace('.', ','));
                else
                    thTP5_double = 0.0;
                if (Double.TryParse(thTP6.Text.Replace('.', ','), out thTP6_double))
                    thTP6_double = Convert.ToDouble(thTP6.Text.Replace('.', ','));
                else
                    thTP6_double = 0.0;
                if (Double.TryParse(thTP7.Text.Replace('.', ','), out thTP7_double))
                    thTP7_double = Convert.ToDouble(thTP7.Text.Replace('.', ','));
                else
                    thTP7_double = 0.0;
                if (Double.TryParse(thTP8.Text.Replace('.', ','), out thTP8_double))
                    thTP8_double = Convert.ToDouble(thTP8.Text.Replace('.', ','));
                else
                    thTP8_double = 0.0;
                if (Double.TryParse(thTP9.Text.Replace('.', ','), out thTP9_double))
                    thTP9_double = Convert.ToDouble(thTP9.Text.Replace('.', ','));
                else
                    thTP9_double = 0.0;
                if (Double.TryParse(thTP10.Text.Replace('.', ','), out thTP10_double))
                    thTP10_double = Convert.ToDouble(thTP10.Text.Replace('.', ','));
                else
                    thTP10_double = 0.0;

                /*if (int.TryParse(thCodMaterial.Text, out thCodMaterial_int))
                    thCodMaterial_int = Convert.ToInt32(thCodMaterial.Text);
                else
                    thCodMaterial_int = 0;*/
                //declaro secuenciales y parcheo puntos y comas
                double seqAbrir1_1_double = 0;
                double seqAbrir1_2_double = 0;
                double seqAbrir1_3_double = 0;
                double seqAbrir1_4_double = 0;
                double seqAbrir1_5_double = 0;
                double seqAbrir1_6_double = 0;
                double seqAbrir1_7_double = 0;
                double seqAbrir1_8_double = 0;
                double seqAbrir1_9_double = 0;
                double seqAbrir1_10_double = 0;
                double seqCerrar1_1_double = 0;
                double seqCerrar1_2_double = 0;
                double seqCerrar1_3_double = 0;
                double seqCerrar1_4_double = 0;
                double seqCerrar1_5_double = 0;
                double seqCerrar1_6_double = 0;
                double seqCerrar1_7_double = 0;
                double seqCerrar1_8_double = 0;
                double seqCerrar1_9_double = 0;
                double seqCerrar1_10_double = 0;
                double seqAbrir2_1_double = 0;
                double seqAbrir2_2_double = 0;
                double seqAbrir2_3_double = 0;
                double seqAbrir2_4_double = 0;
                double seqAbrir2_5_double = 0;
                double seqAbrir2_6_double = 0;
                double seqAbrir2_7_double = 0;
                double seqAbrir2_8_double = 0;
                double seqAbrir2_9_double = 0;
                double seqAbrir2_10_double = 0;
                double seqCerrar2_1_double = 0;
                double seqCerrar2_2_double = 0;
                double seqCerrar2_3_double = 0;
                double seqCerrar2_4_double = 0;
                double seqCerrar2_5_double = 0;
                double seqCerrar2_6_double = 0;
                double seqCerrar2_7_double = 0;
                double seqCerrar2_8_double = 0;
                double seqCerrar2_9_double = 0;
                double seqCerrar2_10_double = 0;
                double seqTPresPost1_double = 0;
                double seqTPresPost2_double = 0;
                double seqTPresPost3_double = 0;
                double seqTPresPost4_double = 0;
                double seqTPresPost5_double = 0;
                double seqTPresPost6_double = 0;
                double seqTPresPost7_double = 0;
                double seqTPresPost8_double = 0;
                double seqTPresPost9_double = 0;
                double seqTPresPost10_double = 0;
                double seqTiempoRetardo_1_double = 0;
                double seqTiempoRetardo_2_double = 0;
                double seqTiempoRetardo_3_double = 0;
                double seqTiempoRetardo_4_double = 0;
                double seqTiempoRetardo_5_double = 0;
                double seqTiempoRetardo_6_double = 0;
                double seqTiempoRetardo_7_double = 0;
                double seqTiempoRetardo_8_double = 0;
                double seqTiempoRetardo_9_double = 0;
                double seqTiempoRetardo_10_double = 0;
                if (Double.TryParse(seqAbrir1_1.Text.Replace('.', ','), out seqAbrir1_1_double))
                    seqAbrir1_1_double = Convert.ToDouble(seqAbrir1_1.Text.Replace('.', ','));
                else
                    seqAbrir1_1_double = 0.0;
                if (Double.TryParse(seqAbrir1_2.Text.Replace('.', ','), out seqAbrir1_2_double))
                    seqAbrir1_2_double = Convert.ToDouble(seqAbrir1_2.Text.Replace('.', ','));
                else
                    seqAbrir1_2_double = 0.0;
                if (Double.TryParse(seqAbrir1_3.Text.Replace('.', ','), out seqAbrir1_3_double))
                    seqAbrir1_3_double = Convert.ToDouble(seqAbrir1_3.Text.Replace('.', ','));
                else
                    seqAbrir1_3_double = 0.0;
                if (Double.TryParse(seqAbrir1_4.Text.Replace('.', ','), out seqAbrir1_4_double))
                    seqAbrir1_4_double = Convert.ToDouble(seqAbrir1_4.Text.Replace('.', ','));
                else
                    seqAbrir1_4_double = 0.0;
                if (Double.TryParse(seqAbrir1_5.Text.Replace('.', ','), out seqAbrir1_5_double))
                    seqAbrir1_5_double = Convert.ToDouble(seqAbrir1_5.Text.Replace('.', ','));
                else
                    seqAbrir1_5_double = 0.0;
                if (Double.TryParse(seqAbrir1_6.Text.Replace('.', ','), out seqAbrir1_6_double))
                    seqAbrir1_6_double = Convert.ToDouble(seqAbrir1_6.Text.Replace('.', ','));
                else
                    seqAbrir1_6_double = 0.0;
                if (Double.TryParse(seqAbrir1_7.Text.Replace('.', ','), out seqAbrir1_7_double))
                    seqAbrir1_7_double = Convert.ToDouble(seqAbrir1_7.Text.Replace('.', ','));
                else
                    seqAbrir1_7_double = 0.0;
                if (Double.TryParse(seqAbrir1_8.Text.Replace('.', ','), out seqAbrir1_8_double))
                    seqAbrir1_8_double = Convert.ToDouble(seqAbrir1_8.Text.Replace('.', ','));
                else
                    seqAbrir1_8_double = 0.0;
                if (Double.TryParse(seqAbrir1_9.Text.Replace('.', ','), out seqAbrir1_9_double))
                    seqAbrir1_9_double = Convert.ToDouble(seqAbrir1_9.Text.Replace('.', ','));
                else
                    seqAbrir1_9_double = 0.0;
                if (Double.TryParse(seqAbrir1_10.Text.Replace('.', ','), out seqAbrir1_10_double))
                    seqAbrir1_10_double = Convert.ToDouble(seqAbrir1_10.Text.Replace('.', ','));
                else
                    seqAbrir1_10_double = 0.0;
                if (Double.TryParse(seqCerrar1_1.Text.Replace('.', ','), out seqCerrar1_1_double))
                    seqCerrar1_1_double = Convert.ToDouble(seqCerrar1_1.Text.Replace('.', ','));
                else
                    seqCerrar1_1_double = 0.0;
                if (Double.TryParse(seqCerrar1_2.Text.Replace('.', ','), out seqCerrar1_2_double))
                    seqCerrar1_2_double = Convert.ToDouble(seqCerrar1_2.Text.Replace('.', ','));
                else
                    seqCerrar1_2_double = 0.0;
                if (Double.TryParse(seqCerrar1_3.Text.Replace('.', ','), out seqCerrar1_3_double))
                    seqCerrar1_3_double = Convert.ToDouble(seqCerrar1_3.Text.Replace('.', ','));
                else
                    seqCerrar1_3_double = 0.0;
                if (Double.TryParse(seqCerrar1_4.Text.Replace('.', ','), out seqCerrar1_4_double))
                    seqCerrar1_4_double = Convert.ToDouble(seqCerrar1_4.Text.Replace('.', ','));
                else
                    seqCerrar1_4_double = 0.0;
                if (Double.TryParse(seqCerrar1_5.Text.Replace('.', ','), out seqCerrar1_5_double))
                    seqCerrar1_5_double = Convert.ToDouble(seqCerrar1_5.Text.Replace('.', ','));
                else
                    seqCerrar1_5_double = 0.0;
                if (Double.TryParse(seqCerrar1_6.Text.Replace('.', ','), out seqCerrar1_6_double))
                    seqCerrar1_6_double = Convert.ToDouble(seqCerrar1_6.Text.Replace('.', ','));
                else
                    seqCerrar1_6_double = 0.0;
                if (Double.TryParse(seqCerrar1_7.Text.Replace('.', ','), out seqCerrar1_7_double))
                    seqCerrar1_7_double = Convert.ToDouble(seqCerrar1_7.Text.Replace('.', ','));
                else
                    seqCerrar1_7_double = 0.0;
                if (Double.TryParse(seqCerrar1_8.Text.Replace('.', ','), out seqCerrar1_8_double))
                    seqCerrar1_8_double = Convert.ToDouble(seqCerrar1_8.Text.Replace('.', ','));
                else
                    seqCerrar1_8_double = 0.0;
                if (Double.TryParse(seqCerrar1_9.Text.Replace('.', ','), out seqCerrar1_9_double))
                    seqCerrar1_9_double = Convert.ToDouble(seqCerrar1_9.Text.Replace('.', ','));
                else
                    seqCerrar1_9_double = 0.0;
                if (Double.TryParse(seqCerrar1_10.Text.Replace('.', ','), out seqCerrar1_10_double))
                    seqCerrar1_10_double = Convert.ToDouble(seqCerrar1_10.Text.Replace('.', ','));
                else
                    seqCerrar1_10_double = 0.0;
                if (Double.TryParse(seqAbrir2_1.Text.Replace('.', ','), out seqAbrir2_1_double))
                    seqAbrir2_1_double = Convert.ToDouble(seqAbrir2_1.Text.Replace('.', ','));
                else
                    seqAbrir2_1_double = 0.0;
                if (Double.TryParse(seqAbrir2_2.Text.Replace('.', ','), out seqAbrir2_2_double))
                    seqAbrir2_2_double = Convert.ToDouble(seqAbrir2_2.Text.Replace('.', ','));
                else
                    seqAbrir2_2_double = 0.0;
                if (Double.TryParse(seqAbrir2_3.Text.Replace('.', ','), out seqAbrir2_3_double))
                    seqAbrir2_3_double = Convert.ToDouble(seqAbrir2_3.Text.Replace('.', ','));
                else
                    seqAbrir2_3_double = 0.0;
                if (Double.TryParse(seqAbrir2_4.Text.Replace('.', ','), out seqAbrir2_4_double))
                    seqAbrir2_4_double = Convert.ToDouble(seqAbrir2_4.Text.Replace('.', ','));
                else
                    seqAbrir2_4_double = 0.0;
                if (Double.TryParse(seqAbrir2_5.Text.Replace('.', ','), out seqAbrir2_5_double))
                    seqAbrir2_5_double = Convert.ToDouble(seqAbrir2_5.Text.Replace('.', ','));
                else
                    seqAbrir2_5_double = 0.0;
                if (Double.TryParse(seqAbrir2_6.Text.Replace('.', ','), out seqAbrir2_6_double))
                    seqAbrir2_6_double = Convert.ToDouble(seqAbrir2_6.Text.Replace('.', ','));
                else
                    seqAbrir2_6_double = 0.0;
                if (Double.TryParse(seqAbrir2_7.Text.Replace('.', ','), out seqAbrir2_7_double))
                    seqAbrir2_7_double = Convert.ToDouble(seqAbrir2_7.Text.Replace('.', ','));
                else
                    seqAbrir2_7_double = 0.0;
                if (Double.TryParse(seqAbrir2_8.Text.Replace('.', ','), out seqAbrir2_8_double))
                    seqAbrir2_8_double = Convert.ToDouble(seqAbrir2_8.Text.Replace('.', ','));
                else
                    seqAbrir2_8_double = 0.0;
                if (Double.TryParse(seqAbrir2_9.Text.Replace('.', ','), out seqAbrir2_9_double))
                    seqAbrir2_9_double = Convert.ToDouble(seqAbrir2_9.Text.Replace('.', ','));
                else
                    seqAbrir2_9_double = 0.0;
                if (Double.TryParse(seqAbrir2_10.Text.Replace('.', ','), out seqAbrir2_10_double))
                    seqAbrir2_10_double = Convert.ToDouble(seqAbrir2_10.Text.Replace('.', ','));
                else
                    seqAbrir2_10_double = 0.0;
                if (Double.TryParse(seqCerrar2_1.Text.Replace('.', ','), out seqCerrar2_1_double))
                    seqCerrar2_1_double = Convert.ToDouble(seqCerrar2_1.Text.Replace('.', ','));
                else
                    seqCerrar2_1_double = 0.0;
                if (Double.TryParse(seqCerrar2_2.Text.Replace('.', ','), out seqCerrar2_2_double))
                    seqCerrar2_2_double = Convert.ToDouble(seqCerrar2_2.Text.Replace('.', ','));
                else
                    seqCerrar2_2_double = 0.0;
                if (Double.TryParse(seqCerrar2_3.Text.Replace('.', ','), out seqCerrar2_3_double))
                    seqCerrar2_3_double = Convert.ToDouble(seqCerrar2_3.Text.Replace('.', ','));
                else
                    seqCerrar2_3_double = 0.0;
                if (Double.TryParse(seqCerrar2_4.Text.Replace('.', ','), out seqCerrar2_4_double))
                    seqCerrar2_4_double = Convert.ToDouble(seqCerrar2_4.Text.Replace('.', ','));
                else
                    seqCerrar2_4_double = 0.0;
                if (Double.TryParse(seqCerrar2_5.Text.Replace('.', ','), out seqCerrar2_5_double))
                    seqCerrar2_5_double = Convert.ToDouble(seqCerrar2_5.Text.Replace('.', ','));
                else
                    seqCerrar2_5_double = 0.0;
                if (Double.TryParse(seqCerrar2_6.Text.Replace('.', ','), out seqCerrar2_6_double))
                    seqCerrar2_6_double = Convert.ToDouble(seqCerrar2_6.Text.Replace('.', ','));
                else
                    seqCerrar2_6_double = 0.0;
                if (Double.TryParse(seqCerrar2_7.Text.Replace('.', ','), out seqCerrar2_7_double))
                    seqCerrar2_7_double = Convert.ToDouble(seqCerrar2_7.Text.Replace('.', ','));
                else
                    seqCerrar2_7_double = 0.0;
                if (Double.TryParse(seqCerrar2_8.Text.Replace('.', ','), out seqCerrar2_8_double))
                    seqCerrar2_8_double = Convert.ToDouble(seqCerrar2_8.Text.Replace('.', ','));
                else
                    seqCerrar2_8_double = 0.0;
                if (Double.TryParse(seqCerrar2_9.Text.Replace('.', ','), out seqCerrar2_9_double))
                    seqCerrar2_9_double = Convert.ToDouble(seqCerrar2_9.Text.Replace('.', ','));
                else
                    seqCerrar2_9_double = 0.0;
                if (Double.TryParse(seqCerrar2_10.Text.Replace('.', ','), out seqCerrar2_10_double))
                    seqCerrar2_10_double = Convert.ToDouble(seqCerrar2_10.Text.Replace('.', ','));
                else
                    seqCerrar2_10_double = 0.0;

                if (Double.TryParse(seqTPresPost1.Text.Replace('.', ','), out seqTPresPost1_double))
                    seqTPresPost1_double = Convert.ToDouble(seqTPresPost1.Text.Replace('.', ','));
                else
                    seqTPresPost1_double = 0.0;
                if (Double.TryParse(seqTPresPost2.Text.Replace('.', ','), out seqTPresPost2_double))
                    seqTPresPost2_double = Convert.ToDouble(seqTPresPost2.Text.Replace('.', ','));
                else
                    seqTPresPost2_double = 0.0;
                if (Double.TryParse(seqTPresPost3.Text.Replace('.', ','), out seqTPresPost3_double))
                    seqTPresPost3_double = Convert.ToDouble(seqTPresPost3.Text.Replace('.', ','));
                else
                    seqTPresPost3_double = 0.0;
                if (Double.TryParse(seqTPresPost4.Text.Replace('.', ','), out seqTPresPost4_double))
                    seqTPresPost4_double = Convert.ToDouble(seqTPresPost4.Text.Replace('.', ','));
                else
                    seqTPresPost4_double = 0.0;
                if (Double.TryParse(seqTPresPost5.Text.Replace('.', ','), out seqTPresPost5_double))
                    seqTPresPost5_double = Convert.ToDouble(seqTPresPost5.Text.Replace('.', ','));
                else
                    seqTPresPost5_double = 0.0;
                if (Double.TryParse(seqTPresPost6.Text.Replace('.', ','), out seqTPresPost6_double))
                    seqTPresPost6_double = Convert.ToDouble(seqTPresPost6.Text.Replace('.', ','));
                else
                    seqTPresPost6_double = 0.0;
                if (Double.TryParse(seqTPresPost7.Text.Replace('.', ','), out seqTPresPost7_double))
                    seqTPresPost7_double = Convert.ToDouble(seqTPresPost7.Text.Replace('.', ','));
                else
                    seqTPresPost7_double = 0.0;
                if (Double.TryParse(seqTPresPost8.Text.Replace('.', ','), out seqTPresPost8_double))
                    seqTPresPost8_double = Convert.ToDouble(seqTPresPost8.Text.Replace('.', ','));
                else
                    seqTPresPost8_double = 0.0;
                if (Double.TryParse(seqTPresPost9.Text.Replace('.', ','), out seqTPresPost9_double))
                    seqTPresPost9_double = Convert.ToDouble(seqTPresPost9.Text.Replace('.', ','));
                else
                    seqTPresPost9_double = 0.0;
                if (Double.TryParse(seqTPresPost10.Text.Replace('.', ','), out seqTPresPost10_double))
                    seqTPresPost10_double = Convert.ToDouble(seqTPresPost10.Text.Replace('.', ','));
                else
                    seqTPresPost10_double = 0.0;
                if (Double.TryParse(seqTiempoRetardo_1.Text.Replace('.', ','), out seqTiempoRetardo_1_double))
                    seqTiempoRetardo_1_double = Convert.ToDouble(seqTiempoRetardo_1.Text.Replace('.', ','));
                else
                    seqTiempoRetardo_1_double = 0.0;
                if (Double.TryParse(seqTiempoRetardo_2.Text.Replace('.', ','), out seqTiempoRetardo_2_double))
                    seqTiempoRetardo_2_double = Convert.ToDouble(seqTiempoRetardo_2.Text.Replace('.', ','));
                else
                    seqTiempoRetardo_2_double = 0.0;
                if (Double.TryParse(seqTiempoRetardo_3.Text.Replace('.', ','), out seqTiempoRetardo_3_double))
                    seqTiempoRetardo_3_double = Convert.ToDouble(seqTiempoRetardo_3.Text.Replace('.', ','));
                else
                    seqTiempoRetardo_3_double = 0.0;
                if (Double.TryParse(seqTiempoRetardo_4.Text.Replace('.', ','), out seqTiempoRetardo_4_double))
                    seqTiempoRetardo_4_double = Convert.ToDouble(seqTiempoRetardo_4.Text.Replace('.', ','));
                else
                    seqTiempoRetardo_4_double = 0.0;
                if (Double.TryParse(seqTiempoRetardo_5.Text.Replace('.', ','), out seqTiempoRetardo_5_double))
                    seqTiempoRetardo_5_double = Convert.ToDouble(seqTiempoRetardo_5.Text.Replace('.', ','));
                else
                    seqTiempoRetardo_5_double = 0.0;
                if (Double.TryParse(seqTiempoRetardo_6.Text.Replace('.', ','), out seqTiempoRetardo_6_double))
                    seqTiempoRetardo_6_double = Convert.ToDouble(seqTiempoRetardo_6.Text.Replace('.', ','));
                else
                    seqTiempoRetardo_6_double = 0.0;
                if (Double.TryParse(seqTiempoRetardo_7.Text.Replace('.', ','), out seqTiempoRetardo_7_double))
                    seqTiempoRetardo_7_double = Convert.ToDouble(seqTiempoRetardo_7.Text.Replace('.', ','));
                else
                    seqTiempoRetardo_7_double = 0.0;
                if (Double.TryParse(seqTiempoRetardo_8.Text.Replace('.', ','), out seqTiempoRetardo_8_double))
                    seqTiempoRetardo_8_double = Convert.ToDouble(seqTiempoRetardo_8.Text.Replace('.', ','));
                else
                    seqTiempoRetardo_8_double = 0.0;
                if (Double.TryParse(seqTiempoRetardo_9.Text.Replace('.', ','), out seqTiempoRetardo_9_double))
                    seqTiempoRetardo_9_double = Convert.ToDouble(seqTiempoRetardo_9.Text.Replace('.', ','));
                else
                    seqTiempoRetardo_9_double = 0.0;
                if (Double.TryParse(seqTiempoRetardo_10.Text.Replace('.', ','), out seqTiempoRetardo_10_double))
                    seqTiempoRetardo_10_double = Convert.ToDouble(seqTiempoRetardo_10.Text.Replace('.', ','));
                else
                    seqTiempoRetardo_10_double = 0.0;
                //termino de declarar y parchear puntos y comas
                //TOLERANCIAS
                double TiempoInyeccionNValDouble = 0;
                double TiempoInyeccionMValDouble = 0;
                double LimitePresionNValDouble = 0;
                double LimitePresionMValDouble = 0;
                double ConmuntaciontolNValDouble = 0;
                double ConmuntaciontolMValDouble = 0;
                double TiempoPresiontolNValDouble = 0;
                double TiempoPresiontolMValDouble = 0;
                double TNvcargavalDouble = 0;
                double TMvcargavalDouble = 0;
                double TNcargavalDouble = 0;
                double TMcargavalDouble = 0;
                double TNdescomvalDouble = 0;
                double TMdescomvalDouble = 0;
                double TNcontrapvalDouble = 0;
                double TMcontrapvalDouble = 0;
                double TNTiempdosvalDouble = 0;
                double TMTiempdosvalDouble = 0;
                double TNEnfriamvalDouble = 0;
                double TMEnfriamvalDouble = 0;
                double TNCiclovalDouble = 0;
                double TMCiclovalDouble = 0;
                double TNCojinvalDouble = 0;
                double TMCojinvalDouble = 0;

                if (Double.TryParse(tbTiempoInyeccionNVal.Text.Replace('.', ','), out TiempoInyeccionNValDouble))
                    TiempoInyeccionNValDouble = Convert.ToDouble(tbTiempoInyeccionNVal.Text.Replace('.', ','));
                else
                    TiempoInyeccionNValDouble = 0.0;
                if (Double.TryParse(tbTiempoInyeccionMVal.Text.Replace('.', ','), out TiempoInyeccionMValDouble))
                    TiempoInyeccionMValDouble = Convert.ToDouble(tbTiempoInyeccionMVal.Text.Replace('.', ','));
                else
                    TiempoInyeccionMValDouble = 0.0;
                if (Double.TryParse(tbLimitePresionNVal.Text.Replace('.', ','), out LimitePresionNValDouble))
                    LimitePresionNValDouble = Convert.ToDouble(tbLimitePresionNVal.Text.Replace('.', ','));
                else
                    LimitePresionNValDouble = 0.0;
                if (Double.TryParse(tbLimitePresionMval.Text.Replace('.', ','), out LimitePresionMValDouble))
                    LimitePresionMValDouble = Convert.ToDouble(tbLimitePresionMval.Text.Replace('.', ','));
                else
                    LimitePresionNValDouble = 0.0;
                if (Double.TryParse(thConmuntaciontolNVal.Text.Replace('.', ','), out ConmuntaciontolNValDouble))
                    ConmuntaciontolNValDouble = Convert.ToDouble(thConmuntaciontolNVal.Text.Replace('.', ','));
                else
                    ConmuntaciontolNValDouble = 0.0;
                if (Double.TryParse(thConmuntaciontolMVal.Text.Replace('.', ','), out ConmuntaciontolMValDouble))
                    ConmuntaciontolMValDouble = Convert.ToDouble(thConmuntaciontolMVal.Text.Replace('.', ','));
                else
                    ConmuntaciontolMValDouble = 0.0;
                if (Double.TryParse(tbTiempoPresiontolNVal.Text.Replace('.', ','), out TiempoPresiontolNValDouble))
                    TiempoPresiontolNValDouble = Convert.ToDouble(tbTiempoPresiontolNVal.Text.Replace('.', ','));
                else
                    TiempoPresiontolNValDouble = 0.0;
                if (Double.TryParse(tbTiempoPresiontolMVal.Text.Replace('.', ','), out TiempoPresiontolMValDouble))
                    TiempoPresiontolMValDouble = Convert.ToDouble(tbTiempoPresiontolMVal.Text.Replace('.', ','));
                else
                    TiempoPresiontolMValDouble = 0.0;
                if (Double.TryParse(TNvcargaval.Text.Replace('.', ','), out TNvcargavalDouble))
                    TNvcargavalDouble = Convert.ToDouble(TNvcargaval.Text.Replace('.', ','));
                else
                    TNvcargavalDouble = 0.0;
                if (Double.TryParse(TMvcargaval.Text.Replace('.', ','), out TMvcargavalDouble))
                    TMvcargavalDouble = Convert.ToDouble(TMvcargaval.Text.Replace('.', ','));
                else
                    TMvcargavalDouble = 0.0;
                if (Double.TryParse(TNcargaval.Text.Replace('.', ','), out TNcargavalDouble))
                    TNcargavalDouble = Convert.ToDouble(TNcargaval.Text.Replace('.', ','));
                else
                    TNcargavalDouble = 0.0;
                if (Double.TryParse(TMcargaval.Text.Replace('.', ','), out TMcargavalDouble))
                    TMcargavalDouble = Convert.ToDouble(TMcargaval.Text.Replace('.', ','));
                else
                    TMcargavalDouble = 0.0;

                if (Double.TryParse(TNdescomval.Text.Replace('.', ','), out TNdescomvalDouble))
                    TNdescomvalDouble = Convert.ToDouble(TNdescomval.Text.Replace('.', ','));
                else
                    TNdescomvalDouble = 0.0;
                if (Double.TryParse(TMdescomval.Text.Replace('.', ','), out TMdescomvalDouble))
                    TMdescomvalDouble = Convert.ToDouble(TMdescomval.Text.Replace('.', ','));
                else
                    TMdescomvalDouble = 0.0;
                if (Double.TryParse(TNcontrapval.Text.Replace('.', ','), out TNcontrapvalDouble))
                    TNcontrapvalDouble = Convert.ToDouble(TNcontrapval.Text.Replace('.', ','));
                else
                    TNcontrapvalDouble = 0.0;
                if (Double.TryParse(TMcontrapval.Text.Replace('.', ','), out TMcontrapvalDouble))
                    TMcontrapvalDouble = Convert.ToDouble(TMcontrapval.Text.Replace('.', ','));
                else
                    TMcontrapvalDouble = 0.0;
                if (Double.TryParse(TNTiempdosval.Text.Replace('.', ','), out TNTiempdosvalDouble))
                    TNTiempdosvalDouble = Convert.ToDouble(TNTiempdosval.Text.Replace('.', ','));
                else
                    TNTiempdosvalDouble = 0.0;
                if (Double.TryParse(TMTiempdosval.Text.Replace('.', ','), out TMTiempdosvalDouble))
                    TMTiempdosvalDouble = Convert.ToDouble(TMTiempdosval.Text.Replace('.', ','));
                else
                    TMTiempdosvalDouble = 0.0;
                if (Double.TryParse(TNEnfriamval.Text.Replace('.', ','), out TNEnfriamvalDouble))
                    TNEnfriamvalDouble = Convert.ToDouble(TNEnfriamval.Text.Replace('.', ','));
                else
                    TNEnfriamvalDouble = 0.0;
                if (Double.TryParse(TMEnfriamval.Text.Replace('.', ','), out TMEnfriamvalDouble))
                    TMEnfriamvalDouble = Convert.ToDouble(TMEnfriamval.Text.Replace('.', ','));
                else
                    TMEnfriamvalDouble = 0.0;

                if (Double.TryParse(TNCicloval.Text.Replace('.', ','), out TNCiclovalDouble))
                    TNCiclovalDouble = Convert.ToDouble(TNCicloval.Text.Replace('.', ','));
                else
                    TNCiclovalDouble = 0.0;
                if (Double.TryParse(TMCicloval.Text.Replace('.', ','), out TMCiclovalDouble))
                    TMCiclovalDouble = Convert.ToDouble(TMCicloval.Text.Replace('.', ','));
                else
                    TMCiclovalDouble = 0.0;

                if (Double.TryParse(TNCojinval.Text.Replace('.', ','), out TNCojinvalDouble))
                    TNCojinvalDouble = Convert.ToDouble(TNCojinval.Text.Replace('.', ','));
                else
                    TNCojinvalDouble = 0.0;
                if (Double.TryParse(TMCojinval.Text.Replace('.', ','), out TMCojinvalDouble))
                    TMCojinvalDouble = Convert.ToDouble(TMCojinval.Text.Replace('.', ','));
                else
                    TMCojinvalDouble = 0.0;


                //Nuevas tolerancias y valores 07/09/2023

                double LimitePresionRealDouble = 0.0;
                double LimitePresionNRealValDouble = 0.0;
                double LimitePresionMRealValDouble = 0.0;
                double TOLCarInyeccionDouble = 0.0;
                double TOLCamCalienteDouble = 0.0;
                double TOLCilindroDouble = 0.0;
                double TOLPostPresionDouble = 0.0;
                double TOLSecCotaDouble = 0.0;
                double TOLSecTiempoDouble = 0.0;

                if (Double.TryParse(tbLimitePresionReal.Text.Replace('.', ','), out LimitePresionRealDouble))
                    LimitePresionRealDouble = Convert.ToDouble(tbLimitePresionReal.Text.Replace('.', ','));
                else
                    LimitePresionRealDouble = 0.0;

                if (Double.TryParse(tbLimitePresionNRealVal.Text.Replace('.', ','), out LimitePresionNRealValDouble))
                    LimitePresionNRealValDouble = Convert.ToDouble(tbLimitePresionNRealVal.Text.Replace('.', ','));
                else
                    LimitePresionNRealValDouble = 0.0;

                if (Double.TryParse(tbLimitePresionMRealVal.Text.Replace('.', ','), out LimitePresionMRealValDouble))
                    LimitePresionMRealValDouble = Convert.ToDouble(tbLimitePresionMRealVal.Text.Replace('.', ','));
                else
                    LimitePresionMRealValDouble = 0.0;

                if (Double.TryParse(tbTOLCarInyeccion.Text.Replace('.', ','), out TOLCarInyeccionDouble))
                    TOLCarInyeccionDouble = Convert.ToDouble(tbTOLCarInyeccion.Text.Replace('.', ','));
                else
                    TOLCarInyeccionDouble = 0.0;

                if (Double.TryParse(tbTOLCamCaliente.Text.Replace('.', ','), out TOLCamCalienteDouble))
                    TOLCamCalienteDouble = Convert.ToDouble(tbTOLCamCaliente.Text.Replace('.', ','));
                else
                    TOLCamCalienteDouble = 0.0;

                if (Double.TryParse(TbTOLCilindro.Text.Replace('.', ','), out TOLCilindroDouble))
                    TOLCilindroDouble = Convert.ToDouble(TbTOLCilindro.Text.Replace('.', ','));
                else
                    TOLCilindroDouble = 0.0;

                if (Double.TryParse(tbTOLPostPresion.Text.Replace('.', ','), out TOLPostPresionDouble))
                    TOLPostPresionDouble = Convert.ToDouble(tbTOLPostPresion.Text.Replace('.', ','));
                else
                    TOLPostPresionDouble = 0.0;


                if (Double.TryParse(tbTOLSecCota.Text.Replace('.', ','), out TOLSecCotaDouble))
                    TOLSecCotaDouble = Convert.ToDouble(tbTOLSecCota.Text.Replace('.', ','));
                else
                    TOLSecCotaDouble = 0.0;

                if (Double.TryParse(tbTOLSecTiempo.Text.Replace('.', ','), out TOLSecTiempoDouble))
                    TOLSecTiempoDouble = Convert.ToDouble(tbTOLSecTiempo.Text.Replace('.', ','));
                else
                    TOLSecTiempoDouble = 0.0;

                //FIN TOLERANCIAS
                //MARCAS INFERIOR
                if (cbNoyos.Checked)
                    noyos = 1;
                if (cbHembra.Checked)
                    hembra = 1;
                if (cbMacho.Checked)
                    macho = 1;
                if (cbAntesExpul.Checked)
                    antesExpul = 1;
                if (cbAntesApert.Checked)
                    antesApert = 1;
                if (cbAntesCierre.Checked)
                    antesCierre = 1;
                if (cbDespuesCierre.Checked)
                    despuesCierre = 1;
                if (cbOtros1.Checked)
                    otros = 1;
                if (cbBoquilla.Checked)
                    boquilla = 1;
                if (cbCono.Checked)
                    cono = 1;
                if (cbRadioLarga.Checked)
                    radioLarga = 1;
                if (cbLibre.Checked)
                    libre = 1;
                if (cbValvula.Checked)
                    valvula = 1;
                if (cbResistencia.Checked)
                    resistencia = 1;
                if (cbOtros2.Checked)
                    otros2 = 1;
                if (cbExpulsion.Checked)
                    expulsion = 1;
                if (cbHidraulica.Checked)
                    hidraulica = 1;
                if (cbNeumatica.Checked)
                    neumatica = 1;
                if (cbNormal.Checked)
                    normal = 1;
                if (cbArandela125.Checked)
                    arandela125 = 1;
                if (cbArandela160.Checked)
                    arandela160 = 1;
                if (cbArandela200.Checked)
                    arandela200 = 1;
                if (cbArandela250.Checked)
                    arandela250 = 1;
                //FIN MARCAS INFERIOR
                
                double PesoPieza = 0.0;
                if (Double.TryParse(tbPesoPieza.Text.Replace('.', ','), out PesoPieza))
                    PesoPieza = Convert.ToDouble(tbPesoPieza.Text.Replace('.', ','));
                else
                    PesoPieza = 0.0;

                double PesoColadas = 0.0;
                if (Double.TryParse(tbPesoColada.Text.Replace('.', ','), out PesoColadas))
                    PesoColadas = Convert.ToDouble(tbPesoColada.Text.Replace('.', ','));
                else
                    PesoColadas = 0.0;

                double PesoTotales = 0.0;
                if (Double.TryParse(tbPesoTotal.Text.Replace('.', ','), out PesoTotales))
                    PesoTotales = Convert.ToDouble(tbPesoTotal.Text.Replace('.', ','));
                else
                    PesoTotales = 0.0;

                //string selectedValueMaquina = Request.Form[lista_maquinas.UniqueID];
                string selectedValueMaquina = tbMaquina.Text;
                int version = conexion.leer_maxima_version(Convert.ToInt32(tbReferencia.Text), Convert.ToInt32(selectedValueMaquina));
                if (conexion.existeFicha(Convert.ToInt32(tbReferencia.Text), Convert.ToInt32(selectedValueMaquina), version))
                {
                    DataSet ds = conexion.cargar_datos_bms(Convert.ToInt32(tbReferencia.Text));
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        tbNombre.Text = ds.Tables[0].Rows[0]["Descripcion"].ToString();
                        tbCliente.Text = ds.Tables[0].Rows[0]["Cliente"].ToString();
                        tbCodigoMolde.Text = ds.Tables[0].Rows[0]["Molde"].ToString();
                        tbPersonal.Text = ds.Tables[0].Rows[0]["Operarios"].ToString();
                        tbCavidades.Text = ds.Tables[0].Rows[0]["Cavidades"].ToString();
                    }
                    version++;
                    string jsreferencia = tbReferencia.Text;
                    string jsmaquina = selectedValueMaquina;
                    conexion.InsertarFichaV2(Convert.ToInt32(tbReferencia.Text), Convert.ToInt32(selectedValueMaquina), version, tbNombre.Text, tbCliente.Text,
                    //conexion.insertarFicha(Convert.ToInt32(tbReferencia.Text), Convert.ToInt32(tbMaquina.Text), version, tbNombre.Text, tbCliente.Text,
                    tbCodigoMolde.Text, tbMaquina.Text, Convert.ToInt32(DropModoSelect.SelectedValue), tbManual.Text,
                    tbPersonal.Text, tbProgramaMolde.Text, tbProgramaRobot.Text, tbManoRobot.Text,
                    tbAperturaMaquina.Text, tbCavidades.Text, PesoPieza, PesoColadas, PesoTotales,
                    thVCarga.Text, thCarga.Text, thDescomp.Text, thContrapr.Text, thTiempo.Text, thEnfriamiento.Text,
                    thCiclo.Text, thCojin.Text, tbRazones.Text, /*thCodMaterial_int, thMaterial.Text, thCodColorante.Text, thColorante.Text, thColor.Text, thTempSecado.Text,
                    thTiempoSecado.Text,*/ thBoq_double, thT1_double, thT2_double,
                    thT3_double, thT4_double, thT5_double, thT6_double, thT7_double,
                    thT8_double, thT9_double, thT10_double,
                    //tbFijaRefrigeracion.Text, tbFijaAtempCircuito.Text, tbAtempTemperatura.Text,
                    thZ1_double, thZ2_double, thZ3_double, thZ4_double, thZ5_double,
                    thZ6_double, thZ7_double, thZ8_double, thZ9_double,
                    thZ10_double, thZ11_double, thZ12_double, thZ13_double,
                    thZ14_double, thZ15_double, thZ16_double, thZ17_double,
                    thZ18_double, thZ19_double, thZ20_double,
                    //tbMovilRefrigeracion.Text, tbMovilAtempCircuito.Text, tbMovilAtempTemperatura.Text,
                    thV1_double, thC1_double, thV2_double, thC2_double, thV3_double, thC3_double, thV4_double, thC4_double, thV5_double, thC5_double, thV6_double, thC6_double,
                    thV7_double, thC7_double, thV8_double, thC8_double, thV9_double, thC9_double, thV10_double, thC10_double, thV11_double, thC11_double, thV12_double, thC12_double,
                    tbTiempoInyeccion.Text, tbLimitePresion.Text, LimitePresionRealDouble.ToString(),
                    thP1_double, thTP1_double, thP2_double, thTP2_double, thP3_double, thTP3_double, thP4_double, thTP4_double,
                    thP5_double, thTP5_double, thP6_double, thTP6_double, thP7_double, thTP7_double, thP8_double, thTP8_double,
                    thP9_double, thTP9_double, thP10_double, thTP10_double, tbConmutacion.Text, tbTiempoPresion.Text,
                    //declaro secuencial_doubles
                    seqAbrir1_1_double, seqAbrir1_2_double, seqAbrir1_3_double, seqAbrir1_4_double, seqAbrir1_5_double, seqAbrir1_6_double, seqAbrir1_7_double, seqAbrir1_8_double, seqAbrir1_9_double, seqAbrir1_10_double,
                    seqCerrar1_1_double, seqCerrar1_2_double, seqCerrar1_3_double, seqCerrar1_4_double, seqCerrar1_5_double, seqCerrar1_6_double, seqCerrar1_7_double, seqCerrar1_8_double, seqCerrar1_9_double, seqCerrar1_10_double,
                    seqAbrir2_1_double, seqAbrir2_2_double, seqAbrir2_3_double, seqAbrir2_4_double, seqAbrir2_5_double, seqAbrir2_6_double, seqAbrir2_7_double, seqAbrir2_8_double, seqAbrir2_9_double, seqAbrir2_10_double,
                    seqCerrar2_1_double, seqCerrar2_2_double, seqCerrar2_3_double, seqCerrar2_4_double, seqCerrar2_5_double, seqCerrar2_6_double, seqCerrar2_7_double, seqCerrar2_8_double, seqCerrar2_9_double, seqCerrar2_10_double,
                    seqTPresPost1_double, seqTPresPost2_double, seqTPresPost3_double, seqTPresPost4_double, seqTPresPost5_double, seqTPresPost6_double, seqTPresPost7_double, seqTPresPost8_double, seqTPresPost9_double, seqTPresPost10_double,
                    seqTiempoRetardo_1_double, seqTiempoRetardo_2_double, seqTiempoRetardo_3_double, seqTiempoRetardo_4_double, seqTiempoRetardo_5_double, seqTiempoRetardo_6_double, seqTiempoRetardo_7_double, seqTiempoRetardo_8_double, seqTiempoRetardo_9_double, seqTiempoRetardo_10_double, seqAnotaciones.Text,
                    //fin declarar secuencial
                    //declaro tolerancias
                    TiempoInyeccionNValDouble, TiempoInyeccionMValDouble, LimitePresionNValDouble, LimitePresionMValDouble, ConmuntaciontolNValDouble, ConmuntaciontolMValDouble, TiempoPresiontolNValDouble,
                    TiempoPresiontolMValDouble, TNvcargavalDouble, TMvcargavalDouble, TNcargavalDouble, TMcargavalDouble, TNdescomvalDouble, TMdescomvalDouble, TNcontrapvalDouble,
                    TMcontrapvalDouble, TNTiempdosvalDouble, TMTiempdosvalDouble, TNEnfriamvalDouble, TMEnfriamvalDouble, TNCiclovalDouble, TMCiclovalDouble, TNCojinvalDouble, TMCojinvalDouble,
                    //Tolerancias nuevas
                    TOLCarInyeccionDouble, TOLCamCalienteDouble, TOLCilindroDouble, TOLPostPresionDouble, TOLSecCotaDouble, TOLSecTiempoDouble, LimitePresionNRealValDouble, LimitePresionMRealValDouble,
                    //Tolerancias nuevas fin
                    TbOperacionText1.Text, TbOperacionText2.Text, TbOperacionText3.Text, TbOperacionText4.Text, TbOperacionText5.Text,
                    //fin declarar tolerancias
                    //declaro atemperado
                    conexion.devuelve_IDtipo_atemperado(AtempTipoF.SelectedValue.ToString()), conexion.devuelve_IDtipo_circuito(TbCircuitoF1.SelectedValue.ToString()), conexion.devuelve_IDtipo_circuito(TbCircuitoF2.SelectedValue.ToString()), conexion.devuelve_IDtipo_circuito(TbCircuitoF3.SelectedValue.ToString()), conexion.devuelve_IDtipo_circuito(TbCircuitoF4.SelectedValue.ToString()), conexion.devuelve_IDtipo_circuito(TbCircuitoF5.SelectedValue.ToString()), conexion.devuelve_IDtipo_circuito(TbCircuitoF6.SelectedValue.ToString()),
                    TbCaudalF1.Text, TbCaudalF2.Text, TbCaudalF3.Text, TbCaudalF4.Text, TbCaudalF5.Text, TbCaudalF6.Text,
                    TbTemperaturaF1.Text, TbTemperaturaF2.Text, TbTemperaturaF3.Text, TbTemperaturaF4.Text, TbTemperaturaF5.Text, TbTemperaturaF6.Text,
                    conexion.devuelve_IDtipo_entradasAtemp(TbEntradaF1.SelectedValue.ToString()), conexion.devuelve_IDtipo_entradasAtemp(TbEntradaF2.SelectedValue.ToString()), conexion.devuelve_IDtipo_entradasAtemp(TbEntradaF3.SelectedValue.ToString()), conexion.devuelve_IDtipo_entradasAtemp(TbEntradaF4.SelectedValue.ToString()), conexion.devuelve_IDtipo_entradasAtemp(TbEntradaF5.SelectedValue.ToString()), conexion.devuelve_IDtipo_entradasAtemp(TbEntradaF6.SelectedValue.ToString()),
                    conexion.devuelve_IDtipo_atemperado(AtempTipoM.SelectedValue.ToString()), conexion.devuelve_IDtipo_circuito(TbCircuitoM1.SelectedValue.ToString()), conexion.devuelve_IDtipo_circuito(TbCircuitoM2.SelectedValue.ToString()), conexion.devuelve_IDtipo_circuito(TbCircuitoM3.SelectedValue.ToString()), conexion.devuelve_IDtipo_circuito(TbCircuitoM4.SelectedValue.ToString()), conexion.devuelve_IDtipo_circuito(TbCircuitoM5.SelectedValue.ToString()), conexion.devuelve_IDtipo_circuito(TbCircuitoM6.SelectedValue.ToString()),
                    TbCaudalM1.Text, TbCaudalM2.Text, TbCaudalM3.Text, TbCaudalM4.Text, TbCaudalM5.Text, TbCaudalM6.Text,
                    TbTemperaturaM1.Text, TbTemperaturaM2.Text, TbTemperaturaM3.Text, TbTemperaturaM4.Text, TbTemperaturaM5.Text, TbTemperaturaM6.Text,
                    conexion.devuelve_IDtipo_entradasAtemp(TbEntradaM1.SelectedValue.ToString()), conexion.devuelve_IDtipo_entradasAtemp(TbEntradaM2.SelectedValue.ToString()), conexion.devuelve_IDtipo_entradasAtemp(TbEntradaM3.SelectedValue.ToString()), conexion.devuelve_IDtipo_entradasAtemp(TbEntradaM4.SelectedValue.ToString()), conexion.devuelve_IDtipo_entradasAtemp(TbEntradaM5.SelectedValue.ToString()), conexion.devuelve_IDtipo_entradasAtemp(TbEntradaM6.SelectedValue.ToString()),
                    //fin declarar atemperado
                    //inicio declarar imagenes
                    hyperlink1.ImageUrl, hyperlink2.ImageUrl, hyperlink3.ImageUrl, hyperlink4.ImageUrl, hyperlink5.ImageUrl, hyperlink6.ImageUrl, hyperlink7.ImageUrl, hyperlink8.ImageUrl,
                    //fin declarar imagenes
                    noyos, hembra, macho, antesExpul, antesApert, antesCierre, despuesCierre, otros,
                    boquilla, cono, radioLarga, libre, valvula, resistencia, otros2, expulsion, hidraulica, neumatica, normal,
                    arandela125, arandela160, arandela200, arandela250, MarcasOtrosText.Text,
                    cbEdicion.Text, cbFecha.Text, SHConexion.Devuelve_ID_Piloto_SMARTH(cbElaboradoPor.SelectedValue.ToString())/*cbElaboradoPor.Text*/, SHConexion.Devuelve_ID_Piloto_SMARTH(cbRevisadoPor.SelectedValue.ToString())/*cbRevisadoPor.Text*/, SHConexion.Devuelve_ID_Piloto_SMARTH(cbAprobadoPor.SelectedValue.ToString())/*cbAprobadoPor.Text*/, tbObservaciones.Text, tbFuerzaCierre.Text);
                    CancelarModificacion(null,null);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "guardado_OK("+jsreferencia+","+ jsmaquina + ");", true);
                    //volver a cargar el resultado
                    //tbReferencia.Text = tbReferencia.Text + " ";
                    //CargarFicha(null, null);



                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "guardado_NOK();", true);
            }
        }

        public void Insertar_foto(object sender, EventArgs e)
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

                //if (pag_actual != 1)
                //    b1.Attributes.Clear();
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
                //string savePath = "C:\\inetpub_thermoweb\\Imagenes\\";
                string savePath = @"C:\inetpub_thermoweb\SMARTH_docs\PARAMETROS\";


                // Get the name of the file to upload.
                //string fileName = FileUpload1.FileName;
                string fileName = "";
                string extension = "";

                switch (num_img)
                {
                    
                    case 1:
                        extension = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
                        fileName = tbFicha.Value + "_PFIJA_GRAL_" + DateTime.Now.ToString("yyyyy-MM-dd_HHmmss") + extension;
                        break;
                    case 2:
                        extension = System.IO.Path.GetExtension(FileUpload2.PostedFile.FileName);
                        fileName = tbFicha.Value + "_PFIJA_GRAL2_" + DateTime.Now.ToString("yyyyy-MM-dd_HHmmss") + extension;
                        break;
                    case 3:
                        extension = System.IO.Path.GetExtension(FileUpload3.PostedFile.FileName);
                        fileName = tbFicha.Value + "_PFIJA_LOPE_" + DateTime.Now.ToString("yyyyy-MM-dd_HHmmss") + extension;
                        break;
                    case 4:
                        extension = System.IO.Path.GetExtension(FileUpload4.PostedFile.FileName);
                        fileName = tbFicha.Value + "_PFIJA_LOPU_" + DateTime.Now.ToString("yyyyy-MM-dd_HHmmss") + extension;
                        break;
                    case 5:
                        extension = System.IO.Path.GetExtension(FileUpload5.PostedFile.FileName);
                        fileName = tbFicha.Value + "_PFIJA_GRAL_" + DateTime.Now.ToString("yyyyy-MM-dd_HHmmss") + extension;
                        break;
                    case 6:
                        extension = System.IO.Path.GetExtension(FileUpload6.PostedFile.FileName);
                        fileName = tbFicha.Value + "_PFIJA_GRAL2_" + DateTime.Now.ToString("yyyyy-MM-dd_HHmmss") + extension;
                        break;
                    case 7:
                        extension = System.IO.Path.GetExtension(FileUpload7.PostedFile.FileName);
                        fileName = tbFicha.Value + "_PFIJA_LOPE_" + DateTime.Now.ToString("yyyyy-MM-dd_HHmmss") + extension;
                        break;
                    case 8:
                        extension = System.IO.Path.GetExtension(FileUpload8.PostedFile.FileName);
                        fileName = tbFicha.Value + "_PFIJA_LOPU_" + DateTime.Now.ToString("yyyyy-MM-dd_HHmmss") + extension;
                        break;
                    default: break;
                }


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
                    UploadStatusLabel.Text = "Imágenes subidas correctamente.";
                    UploadStatusLabel2.Text = "Imágenes subidas correctamente.";
                }
                else
                {
                    // Notify the user that the file was saved successfully.
                    UploadStatusLabel.Text = "Imágenes cargadas correctamente.";
                    UploadStatusLabel2.Text = "Imágenes subidas correctamente.";
                }

                // Append the name of the file to upload to the path.
                savePath += fileName;

                // Call the SaveAs method to save the uploaded
                // file to the specified directory.
                switch (num_img)
                {
                    case 1:
                        FileUpload1.SaveAs(savePath);
                        hyperlink1.ImageUrl = "../SMARTH_docs/PARAMETROS/" + fileName;
                        break;
                    case 2:
                        FileUpload2.SaveAs(savePath);
                        hyperlink2.ImageUrl = "../SMARTH_docs/PARAMETROS/" + fileName;
                        break;
                    case 3:
                        FileUpload3.SaveAs(savePath);
                        hyperlink3.ImageUrl = "../SMARTH_docs/PARAMETROS/" + fileName;
                        break;
                    case 4:
                        FileUpload4.SaveAs(savePath);
                        hyperlink4.ImageUrl = "../SMARTH_docs/PARAMETROS/" + fileName;
                        break;
                    case 5:
                        FileUpload5.SaveAs(savePath);
                        hyperlink5.ImageUrl = "../SMARTH_docs/PARAMETROS/" + fileName;
                        break;
                    case 6:
                        FileUpload6.SaveAs(savePath);
                        hyperlink6.ImageUrl = "../SMARTH_docs/PARAMETROS/" + fileName;
                        break;
                    case 7:
                        FileUpload7.SaveAs(savePath);
                        hyperlink7.ImageUrl = "../SMARTH_docs/PARAMETROS/" + fileName;
                        break;
                    case 8:
                        FileUpload8.SaveAs(savePath);
                        hyperlink8.ImageUrl = "../SMARTH_docs/PARAMETROS/" + fileName;
                        break;
                    default: break;
                }
            }
            catch (Exception)
            {
            }

        }

        private void Cargar_versiones(int referencia, int maquina)
        {
            try
            {
                Conexion conexion = new Conexion();
                DataSet ds_versiones = conexion.leer_versiones(referencia, maquina);
                lista_versiones.Items.Clear();
                foreach (DataRow row in ds_versiones.Tables[0].Rows)
                {
                    lista_versiones.Items.Add(row["Version"].ToString());
                }
            }
            catch (Exception)
            {
            }
        }

        private void Cargar_maquinas(int referencia)
        {
            try
            {
                Conexion conexion = new Conexion();
                DataSet ds_maquinas = conexion.leer_maquinas_byRef(referencia);
                lista_maquinas.Items.Clear();
                foreach (DataRow row in ds_maquinas.Tables[0].Rows)
                {
                    lista_maquinas.Items.Add(row["Maquina"].ToString());
                }
            }
            catch (Exception)
            {
            }
        }

        // ACCIONES DE BOTONES
        public void Modificar_ficha(object sender, EventArgs e)
        {
            try
            {
                Conexion conexion = new Conexion();
                string selectedValueMaquina = Request.Form[lista_maquinas.UniqueID];
                if (conexion.existeFicha(Convert.ToInt32(tbFicha.Value), Convert.ToInt32(selectedValueMaquina), Convert.ToInt16(lista_versiones.SelectedValue)))
                {
                    Activar_campos();
                    //btnImportarMaquina.Visible = false;
                    btnModificar.Visible = false;
                    btnCargar.Visible = false;
                    btnNuevo.Visible = false;
                    btnGuardar.Visible = true;
                    btnCancelar.Visible = true;
                    btnImprimir.Visible = false;
                    CargarFicha_function();

                }
            }
            catch (Exception)
            {
            }
        }

        public void CancelarModificacion(Object sender, EventArgs e)
        {
            try
            {
                Desactivar_campos();
                //btnImportarMaquina.Visible = false;
                btnNuevo.Visible = true;
                
                btnCargar.Visible = true;
                btnModificar.Visible = true;
                btnGuardar.Visible = false;
                btnCancelar.Visible = false;
                btnImprimir.Visible = true;
                CargarFicha_function();
            }
            catch (Exception)
            {
            }
        }

        public void Nueva_Ficha(object sender, EventArgs e)
        {
            Response.Redirect("../PRODUCCION/FichasParametros_nuevo.aspx");
        }

        //ACCIONES CON EXCEL

        //EXPORTAR
        public void Imprimir_ficha(Object sender, EventArgs e)
        {
            try
            {

                string filepath = @"\\FACTS4-SRV\Fichas parametros\FichaReferencia_vacia_V2.xlsx";
                FileInfo fileInfo = new FileInfo(filepath);
                ExcelPackage p = new ExcelPackage(fileInfo);
                ExcelWorksheet xlWorkSheet = p.Workbook.Worksheets["FICHA"];

                // información principal
                xlWorkSheet.Cells[2, 8].Value = tbReferencia.Text;  //referencia
                xlWorkSheet.Cells[3, 8].Value = tbNombre.Text; //nombre
                xlWorkSheet.Cells[4, 8].Value = tbCliente.Text; //cliente
                xlWorkSheet.Cells[5, 8].Value = tbCodigoMolde.Text; //codigoMolde
                xlWorkSheet.Cells[6, 8].Value = tbMaquina.Text; //maquina
                xlWorkSheet.Cells[7, 8].Value = tbAutomatico.Text; //auto
                xlWorkSheet.Cells[8, 8].Value = tbManual.Text; //manual
                xlWorkSheet.Cells[9, 8].Value = tbPersonal.Text; //personal

                xlWorkSheet.Cells[2, 22].Value = tbProgramaMolde.Text; //programaMolde
                xlWorkSheet.Cells[3, 22].Value = tbProgramaRobot.Text; //programaRobot
                xlWorkSheet.Cells[4, 22].Value = tbManoRobot.Text; //manoRobot
                xlWorkSheet.Cells[5, 22].Value = tbAperturaMaquina.Text; //aperturaMaquina
                xlWorkSheet.Cells[5, 27].Value = tbFuerzaCierre.Text; //aperturaMaquina
                xlWorkSheet.Cells[6, 22].Value = tbCavidades.Text; //cavidades
                xlWorkSheet.Cells[7, 22].Value = tbPesoPieza.Text; //pesoPieza
                xlWorkSheet.Cells[8, 22].Value = tbPesoColada.Text; //pesoColada
                xlWorkSheet.Cells[9, 22].Value = tbPesoTotal.Text; //pesoTotal


                // material
                /*xlWorkSheet.Cells[13, 1].Value = thCodMaterial.Text;  //codMaterial
                xlWorkSheet.Cells[13, 5].Value = thMaterial.Text; // material
                xlWorkSheet.Cells[13, 12].Value = thCodColorante.Text; //codColorante
                xlWorkSheet.Cells[13, 16].Value = thColorante.Text;  //colorante
                xlWorkSheet.Cells[13, 21].Value = thColor.Text; //color
                xlWorkSheet.Cells[13, 25].Value = thTempSecado.Text; //tempSecado
                xlWorkSheet.Cells[13, 29].Value = thTiempoSecado.Text; //tiempoSecado*/

                // temperaturas husillo
                xlWorkSheet.Cells[18, 1].Value = thBoq.Text; //boquilla                
                xlWorkSheet.Cells[18, 3].Value = thT1.Text;
                xlWorkSheet.Cells[18, 4].Value = thT2.Text;
                xlWorkSheet.Cells[18, 5].Value = thT3.Text;
                xlWorkSheet.Cells[18, 6].Value = thT4.Text;
                xlWorkSheet.Cells[18, 8].Value = thT5.Text;
                xlWorkSheet.Cells[18, 9].Value = thT6.Text;
                xlWorkSheet.Cells[18, 10].Value = thT7.Text;
                xlWorkSheet.Cells[18, 12].Value = thT8.Text;
                xlWorkSheet.Cells[18, 13].Value = thT9.Text;
                xlWorkSheet.Cells[18, 14].Value = thT10.Text;
                //xlWorkSheet.Cells[24, 1].Value = tbFijaRefrigeracion.Text; //fijaRefriCircuito
                //xlWorkSheet.Cells[24, 7].Value = tbFijaAtempCircuito.Text; // fijaAtempCircuito
                //xlWorkSheet.Cells[24, 12].Value = tbAtempTemperatura.Text; //fijaAtempTemp

                // temperaturas cámara caliente
                xlWorkSheet.Cells[18, 16].Value = thZ1.Text;
                xlWorkSheet.Cells[18, 17].Value = thZ2.Text;
                xlWorkSheet.Cells[18, 18].Value = thZ3.Text;
                xlWorkSheet.Cells[18, 20].Value = thZ4.Text;
                xlWorkSheet.Cells[18, 21].Value = thZ5.Text;
                xlWorkSheet.Cells[18, 23].Value = thZ6.Text;
                xlWorkSheet.Cells[18, 25].Value = thZ7.Text;
                xlWorkSheet.Cells[18, 27].Value = thZ8.Text;
                xlWorkSheet.Cells[18, 29].Value = thZ9.Text;
                xlWorkSheet.Cells[18, 31].Value = thZ10.Text;
                xlWorkSheet.Cells[20, 16].Value = thZ11.Text;
                xlWorkSheet.Cells[20, 17].Value = thZ12.Text;
                xlWorkSheet.Cells[20, 18].Value = thZ13.Text;
                xlWorkSheet.Cells[20, 20].Value = thZ14.Text;
                xlWorkSheet.Cells[20, 21].Value = thZ15.Text;
                xlWorkSheet.Cells[20, 23].Value = thZ16.Text;
                xlWorkSheet.Cells[20, 25].Value = thZ17.Text;
                xlWorkSheet.Cells[20, 27].Value = thZ18.Text;
                xlWorkSheet.Cells[20, 29].Value = thZ19.Text;
                xlWorkSheet.Cells[20, 31].Value = thZ20.Text;
                //xlWorkSheet.Cells[24, 16].Value = tbMovilRefrigeracion.Text;
                //xlWorkSheet.Cells[24, 21].Value = tbMovilAtempCircuito.Text;
                //xlWorkSheet.Cells[24, 26].Value = tbMovilAtempTemperatura.Text;

                //MATERIAL INYECCION
                Conexion conexion = new Conexion();
                DataSet ds = new DataSet();
                ds = conexion.cargar_datos_materialinyeccion(Convert.ToInt32(tbFicha.Value));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string CodigoMaterial = ds.Tables[0].Rows[0]["MATERIAL"].ToString();
                    string ReferenciaMaterial = ds.Tables[0].Rows[0]["DESCRIPCION"].ToString();
                    string UbicacionMaterial = ds.Tables[0].Rows[0]["UBICACION"].ToString();
                    string SecadoMaterial = ds.Tables[0].Rows[0]["SECADO"].ToString();
                    xlWorkSheet.Cells[12, 5].Value = CodigoMaterial;
                    xlWorkSheet.Cells[12, 10].Value = ReferenciaMaterial;
                    xlWorkSheet.Cells[12, 19].Value = UbicacionMaterial;
                    xlWorkSheet.Cells[12, 25].Value = SecadoMaterial;
                }
                else
                {
                    xlWorkSheet.Cells[12, 5].Value = "";
                    xlWorkSheet.Cells[12, 10].Value = "";
                    xlWorkSheet.Cells[12, 19].Value = "";
                    xlWorkSheet.Cells[12, 25].Value = "";
                }

                if (ds.Tables[0].Rows.Count > 1)
                {
                    string CodigoMaterial2 = ds.Tables[0].Rows[1]["MATERIAL"].ToString();
                    string ReferenciaMaterial2 = ds.Tables[0].Rows[1]["DESCRIPCION"].ToString();
                    string UbicacionMaterial2 = ds.Tables[0].Rows[1]["UBICACION"].ToString();
                    string SecadoMaterial2 = ds.Tables[0].Rows[1]["SECADO"].ToString();
                    xlWorkSheet.Cells[13, 5].Value = CodigoMaterial2;
                    xlWorkSheet.Cells[13, 10].Value = ReferenciaMaterial2;
                    xlWorkSheet.Cells[13, 19].Value = UbicacionMaterial2;
                    xlWorkSheet.Cells[13, 25].Value = SecadoMaterial2;
                }
                else
                {
                    xlWorkSheet.Cells[13, 5].Value = "";
                    xlWorkSheet.Cells[13, 10].Value = "";
                    xlWorkSheet.Cells[13, 19].Value = "";
                    xlWorkSheet.Cells[13, 25].Value = "";
                }

                DataSet dsc = new DataSet();
                dsc = conexion.cargar_datos_materialconcentrado(Convert.ToInt32(tbFicha.Value));
                if (dsc.Tables[0].Rows.Count > 0)
                {
                    string CodigoConcentrado = dsc.Tables[0].Rows[0]["MATERIAL"].ToString();
                    string ReferenciaConcentrado = dsc.Tables[0].Rows[0]["DESCRIPCION"].ToString();
                    string UbicacionConcentrado = dsc.Tables[0].Rows[0]["UBICACION"].ToString();
                    string SecadoConcentrado = dsc.Tables[0].Rows[0]["SECADO"].ToString();
                    xlWorkSheet.Cells[14, 5].Value = CodigoConcentrado;
                    xlWorkSheet.Cells[14, 10].Value = ReferenciaConcentrado;
                    xlWorkSheet.Cells[14, 19].Value = UbicacionConcentrado;
                    xlWorkSheet.Cells[14, 25].Value = SecadoConcentrado;
                }
                else
                {
                    xlWorkSheet.Cells[14, 5].Value = "";
                    xlWorkSheet.Cells[14, 10].Value = "";
                    xlWorkSheet.Cells[14, 19].Value = "";
                    xlWorkSheet.Cells[14, 25].Value = "";
                }

                // ATEMPERADO
                xlWorkSheet.Cells[21, 8].Value = AtempTipoF.Text;
                //circuitofijo
                xlWorkSheet.Cells[22, 1].Value = TbCircuitoF1.Text;
                xlWorkSheet.Cells[22, 4].Value = TbCircuitoF2.Text;
                xlWorkSheet.Cells[22, 7].Value = TbCircuitoF3.Text;
                xlWorkSheet.Cells[22, 10].Value = TbCircuitoF4.Text;
                xlWorkSheet.Cells[22, 13].Value = TbCircuitoF5.Text;
                //caudalfijo
                xlWorkSheet.Cells[23, 1].Value = TbCaudalF1.Text;
                xlWorkSheet.Cells[23, 4].Value = TbCaudalF2.Text;
                xlWorkSheet.Cells[23, 7].Value = TbCaudalF3.Text;
                xlWorkSheet.Cells[23, 10].Value = TbCaudalF4.Text;
                xlWorkSheet.Cells[23, 13].Value = TbCaudalF5.Text;
                //temperaturafijo
                xlWorkSheet.Cells[24, 1].Value = TbTemperaturaF1.Text;
                xlWorkSheet.Cells[24, 4].Value = TbTemperaturaF2.Text;
                xlWorkSheet.Cells[24, 7].Value = TbTemperaturaF3.Text;
                xlWorkSheet.Cells[24, 10].Value = TbTemperaturaF4.Text;
                xlWorkSheet.Cells[24, 13].Value = TbTemperaturaF5.Text;
                //entradafijo
                xlWorkSheet.Cells[25, 1].Value = TbEntradaF1.Text;
                xlWorkSheet.Cells[25, 4].Value = TbEntradaF2.Text;
                xlWorkSheet.Cells[25, 7].Value = TbEntradaF3.Text;
                xlWorkSheet.Cells[25, 10].Value = TbEntradaF4.Text;
                xlWorkSheet.Cells[25, 13].Value = TbEntradaF5.Text;

                //CIRCUITOMOVIL
                xlWorkSheet.Cells[21, 24].Value = AtempTipoM.Text;
                //circuitos
                xlWorkSheet.Cells[22, 18].Value = TbCircuitoM1.Text;
                xlWorkSheet.Cells[22, 21].Value = TbCircuitoM2.Text;
                xlWorkSheet.Cells[22, 24].Value = TbCircuitoM3.Text;
                xlWorkSheet.Cells[22, 27].Value = TbCircuitoM4.Text;
                xlWorkSheet.Cells[22, 30].Value = TbCircuitoM5.Text;
                //caudalfijo
                xlWorkSheet.Cells[23, 18].Value = TbCaudalM1.Text;
                xlWorkSheet.Cells[23, 21].Value = TbCaudalM2.Text;
                xlWorkSheet.Cells[23, 24].Value = TbCaudalM3.Text;
                xlWorkSheet.Cells[23, 27].Value = TbCaudalM4.Text;
                xlWorkSheet.Cells[23, 30].Value = TbCaudalM5.Text;
                //temperaturafijo
                xlWorkSheet.Cells[24, 18].Value = TbTemperaturaM1.Text;
                xlWorkSheet.Cells[24, 21].Value = TbTemperaturaM2.Text;
                xlWorkSheet.Cells[24, 24].Value = TbTemperaturaM3.Text;
                xlWorkSheet.Cells[24, 27].Value = TbTemperaturaM4.Text;
                xlWorkSheet.Cells[24, 30].Value = TbTemperaturaM5.Text;
                //entradafijo
                xlWorkSheet.Cells[25, 18].Value = TbEntradaM1.Text;
                xlWorkSheet.Cells[25, 21].Value = TbEntradaM2.Text;
                xlWorkSheet.Cells[25, 24].Value = TbEntradaM3.Text;
                xlWorkSheet.Cells[25, 27].Value = TbEntradaM4.Text;
                xlWorkSheet.Cells[25, 30].Value = TbEntradaM5.Text;


                // inyección 
                xlWorkSheet.Cells[28, 6].Value = thV1.Text;
                xlWorkSheet.Cells[29, 6].Value = thC1.Text;
                xlWorkSheet.Cells[28, 8].Value = thV2.Text;
                xlWorkSheet.Cells[29, 8].Value = thC2.Text;
                xlWorkSheet.Cells[28, 9].Value = thV3.Text;
                xlWorkSheet.Cells[29, 9].Value = thC3.Text;
                xlWorkSheet.Cells[28, 10].Value = thV4.Text;
                xlWorkSheet.Cells[29, 10].Value = thC4.Text;
                xlWorkSheet.Cells[28, 12].Value = thV5.Text;
                xlWorkSheet.Cells[29, 12].Value = thC5.Text;
                xlWorkSheet.Cells[28, 13].Value = thV6.Text;
                xlWorkSheet.Cells[29, 13].Value = thC6.Text;
                xlWorkSheet.Cells[28, 14].Value = thV7.Text;
                xlWorkSheet.Cells[29, 14].Value = thC7.Text;

                xlWorkSheet.Cells[28, 15].Value = thV8.Text;
                xlWorkSheet.Cells[29, 15].Value = thC8.Text;
                xlWorkSheet.Cells[28, 16].Value = thV9.Text;
                xlWorkSheet.Cells[29, 16].Value = thC9.Text;
                xlWorkSheet.Cells[28, 17].Value = thV10.Text;
                xlWorkSheet.Cells[29, 17].Value = thC10.Text;
                xlWorkSheet.Cells[28, 19].Value = thV11.Text;
                xlWorkSheet.Cells[29, 19].Value = thC11.Text;
                xlWorkSheet.Cells[28, 20].Value = thV12.Text;
                xlWorkSheet.Cells[29, 20].Value = thC12.Text;
                xlWorkSheet.Cells[28, 22].Value = tbTiempoInyeccion.Text;
                xlWorkSheet.Cells[28, 26].Value = tbLimitePresion.Text;
                xlWorkSheet.Cells[28, 28].Value = tbLimitePresionReal.Text;

                // inyección secuencial
                //abrir1
                xlWorkSheet.Cells[33, 5].Value = seqAbrir1_1.Text;
                xlWorkSheet.Cells[33, 6].Value = seqAbrir1_2.Text;
                xlWorkSheet.Cells[33, 7].Value = seqAbrir1_3.Text;
                xlWorkSheet.Cells[33, 8].Value = seqAbrir1_4.Text;
                xlWorkSheet.Cells[33, 9].Value = seqAbrir1_5.Text;
                xlWorkSheet.Cells[33, 10].Value = seqAbrir1_6.Text;
                xlWorkSheet.Cells[33, 11].Value = seqAbrir1_7.Text;
                xlWorkSheet.Cells[33, 12].Value = seqAbrir1_8.Text;
                xlWorkSheet.Cells[33, 13].Value = seqAbrir1_9.Text;
                xlWorkSheet.Cells[33, 14].Value = seqAbrir1_10.Text;
                //cerrar1
                xlWorkSheet.Cells[34, 5].Value = seqCerrar1_1.Text;
                xlWorkSheet.Cells[34, 6].Value = seqCerrar1_2.Text;
                xlWorkSheet.Cells[34, 7].Value = seqCerrar1_3.Text;
                xlWorkSheet.Cells[34, 8].Value = seqCerrar1_4.Text;
                xlWorkSheet.Cells[34, 9].Value = seqCerrar1_5.Text;
                xlWorkSheet.Cells[34, 10].Value = seqCerrar1_6.Text;
                xlWorkSheet.Cells[34, 11].Value = seqCerrar1_7.Text;
                xlWorkSheet.Cells[34, 12].Value = seqCerrar1_8.Text;
                xlWorkSheet.Cells[34, 13].Value = seqCerrar1_9.Text;
                xlWorkSheet.Cells[34, 14].Value = seqCerrar1_10.Text;
                //abrir2
                xlWorkSheet.Cells[35, 5].Value = seqAbrir2_1.Text;
                xlWorkSheet.Cells[35, 6].Value = seqAbrir2_2.Text;
                xlWorkSheet.Cells[35, 7].Value = seqAbrir2_3.Text;
                xlWorkSheet.Cells[35, 8].Value = seqAbrir2_4.Text;
                xlWorkSheet.Cells[35, 9].Value = seqAbrir2_5.Text;
                xlWorkSheet.Cells[35, 10].Value = seqAbrir2_6.Text;
                xlWorkSheet.Cells[35, 11].Value = seqAbrir2_7.Text;
                xlWorkSheet.Cells[35, 12].Value = seqAbrir2_8.Text;
                xlWorkSheet.Cells[35, 13].Value = seqAbrir2_9.Text;
                xlWorkSheet.Cells[35, 14].Value = seqAbrir2_10.Text;
                //cerrar2
                xlWorkSheet.Cells[36, 5].Value = seqCerrar2_1.Text;
                xlWorkSheet.Cells[36, 6].Value = seqCerrar2_2.Text;
                xlWorkSheet.Cells[36, 7].Value = seqCerrar2_3.Text;
                xlWorkSheet.Cells[36, 8].Value = seqCerrar2_4.Text;
                xlWorkSheet.Cells[36, 9].Value = seqCerrar2_5.Text;
                xlWorkSheet.Cells[36, 10].Value = seqCerrar2_6.Text;
                xlWorkSheet.Cells[36, 11].Value = seqCerrar2_7.Text;
                xlWorkSheet.Cells[36, 12].Value = seqCerrar2_8.Text;
                xlWorkSheet.Cells[36, 13].Value = seqCerrar2_9.Text;
                xlWorkSheet.Cells[36, 14].Value = seqCerrar2_10.Text;
                //tprestpost
                xlWorkSheet.Cells[37, 5].Value = seqTPresPost1.Text;
                xlWorkSheet.Cells[37, 6].Value = seqTPresPost2.Text;
                xlWorkSheet.Cells[37, 7].Value = seqTPresPost3.Text;
                xlWorkSheet.Cells[37, 8].Value = seqTPresPost4.Text;
                xlWorkSheet.Cells[37, 9].Value = seqTPresPost5.Text;
                xlWorkSheet.Cells[37, 10].Value = seqTPresPost6.Text;
                xlWorkSheet.Cells[37, 11].Value = seqTPresPost7.Text;
                xlWorkSheet.Cells[37, 12].Value = seqTPresPost8.Text;
                xlWorkSheet.Cells[37, 13].Value = seqTPresPost9.Text;
                xlWorkSheet.Cells[37, 14].Value = seqTPresPost10.Text;
                //portiempo
                xlWorkSheet.Cells[33, 22].Value = seqTiempoRetardo_1.Text;
                xlWorkSheet.Cells[33, 23].Value = seqTiempoRetardo_2.Text;
                xlWorkSheet.Cells[33, 24].Value = seqTiempoRetardo_3.Text;
                xlWorkSheet.Cells[33, 25].Value = seqTiempoRetardo_4.Text;
                xlWorkSheet.Cells[33, 26].Value = seqTiempoRetardo_5.Text;
                xlWorkSheet.Cells[33, 27].Value = seqTiempoRetardo_6.Text;
                xlWorkSheet.Cells[33, 28].Value = seqTiempoRetardo_7.Text;
                xlWorkSheet.Cells[33, 29].Value = seqTiempoRetardo_8.Text;
                xlWorkSheet.Cells[33, 30].Value = seqTiempoRetardo_9.Text;
                xlWorkSheet.Cells[33, 31].Value = seqTiempoRetardo_10.Text;
                xlWorkSheet.Cells[35, 15].Value = seqAnotaciones.Text;


                // postpresion
                xlWorkSheet.Cells[41, 8].Value = thP1.Text;
                xlWorkSheet.Cells[42, 8].Value = thTP1.Text;
                xlWorkSheet.Cells[41, 9].Value = thP2.Text;
                xlWorkSheet.Cells[42, 9].Value = thTP2.Text;
                xlWorkSheet.Cells[41, 10].Value = thP3.Text;
                xlWorkSheet.Cells[42, 10].Value = thTP3.Text;
                xlWorkSheet.Cells[41, 12].Value = thP4.Text;
                xlWorkSheet.Cells[42, 12].Value = thTP4.Text;
                xlWorkSheet.Cells[41, 13].Value = thP5.Text;
                xlWorkSheet.Cells[42, 13].Value = thTP5.Text;
                xlWorkSheet.Cells[41, 14].Value = thP6.Text;
                xlWorkSheet.Cells[42, 14].Value = thTP6.Text;
                xlWorkSheet.Cells[41, 15].Value = thP7.Text;
                xlWorkSheet.Cells[42, 15].Value = thTP7.Text;
                xlWorkSheet.Cells[41, 16].Value = thP8.Text;
                xlWorkSheet.Cells[42, 16].Value = thTP8.Text;
                xlWorkSheet.Cells[41, 17].Value = thP9.Text;
                xlWorkSheet.Cells[42, 17].Value = thTP9.Text;
                xlWorkSheet.Cells[41, 18].Value = thP10.Text;
                xlWorkSheet.Cells[42, 18].Value = thTP10.Text;
                xlWorkSheet.Cells[41, 27].Value = tbTiempoPresion.Text;
                xlWorkSheet.Cells[41, 1].Value = tbConmutacion.Text;

                // dosificación

                xlWorkSheet.Cells[45, 1].Value = thVCarga.Text; // velocidadCarga
                xlWorkSheet.Cells[45, 5].Value = thCarga.Text; // carga
                xlWorkSheet.Cells[45, 9].Value = thDescomp.Text; // descompresion
                xlWorkSheet.Cells[45, 13].Value = thContrapr.Text; // contrapresion
                xlWorkSheet.Cells[45, 17].Value = thTiempo.Text; // tiempoDosificacion
                xlWorkSheet.Cells[45, 21].Value = thEnfriamiento.Text; // tiempoEnfriamiento
                xlWorkSheet.Cells[45, 25].Value = thCiclo.Text; // tiempoCiclo
                xlWorkSheet.Cells[45, 29].Value = thCojin.Text; // tiempoCojin

                // Tolerancias

                xlWorkSheet.Cells[28, 25].Value = tbTiempoInyeccionMVal.Text; //tiempoInyeccionMIN
                xlWorkSheet.Cells[29, 25].Value = tbTiempoInyeccionNVal.Text; //tiempoInyeccionMIN
               // xlWorkSheet.Cells[28, 31].Value = tbLimitePresionMval.Text; //limitePresionMIN
                //xlWorkSheet.Cells[29, 31].Value = tbLimitePresionNVal.Text; //limitePresionMIN

                xlWorkSheet.Cells[41, 31].Value = tbTiempoPresiontolMVal.Text; //tiempoPresionMIN
                xlWorkSheet.Cells[42, 31].Value = tbTiempoPresiontolNVal.Text; //tiempoPresionMAX

                xlWorkSheet.Cells[41, 4].Value = thConmuntaciontolMVal.Text; //conmutacionMIN
                xlWorkSheet.Cells[42, 4].Value = thConmuntaciontolNVal.Text; //conmutacionMAX

                xlWorkSheet.Cells[45, 4].Value = TMvcargaval.Text; //velCargaMIN
                xlWorkSheet.Cells[46, 4].Value = TNvcargaval.Text; //velCargaMAX
                xlWorkSheet.Cells[45, 8].Value = TMcargaval.Text; //CargaMIN
                xlWorkSheet.Cells[46, 8].Value = TNcargaval.Text; //CargaMAX
                xlWorkSheet.Cells[45, 12].Value = TMdescomval.Text; //descompresionMIN
                xlWorkSheet.Cells[46, 12].Value = TNdescomval.Text; //descompresionMAX
                xlWorkSheet.Cells[45, 16].Value = TMcontrapval.Text; //contrapresionMIN
                xlWorkSheet.Cells[46, 16].Value = TNcontrapval.Text; //contrapresionMAX
                xlWorkSheet.Cells[45, 20].Value = TMTiempdosval.Text; //tiempodosifMIN
                xlWorkSheet.Cells[46, 20].Value = TNTiempdosval.Text; //tiempodosifMAX
                xlWorkSheet.Cells[45, 24].Value = TMEnfriamval.Text; //enfriamientoMIN
                xlWorkSheet.Cells[46, 24].Value = TNEnfriamval.Text; //enfriuamientoMAX
                xlWorkSheet.Cells[45, 28].Value = TMCicloval.Text; //cicloMIN
                xlWorkSheet.Cells[46, 28].Value = TNCicloval.Text; //cicloMAX
                xlWorkSheet.Cells[45, 31].Value = TMCojinval.Text; //cojinMIN
                xlWorkSheet.Cells[46, 31].Value = TNCojinval.Text; //cojinMAX

                //NUEVAS TOLERANCIAS
                xlWorkSheet.Cells[28, 31].Value = tbLimitePresionMRealVal.Text; //limitePresionMAX
                xlWorkSheet.Cells[29, 31].Value = tbLimitePresionNRealVal.Text; //limitePresionMIN

                xlWorkSheet.Cells[16, 13].Value = "± " + TbTOLCilindro.Text + "%"; //Tolerancia cilindro
                xlWorkSheet.Cells[16, 29].Value = "± " + tbTOLCamCaliente.Text + "%";  //Tolerancia cámara caliente
                xlWorkSheet.Cells[28, 21].Value = "± " + tbTOLCarInyeccion.Text + "%";  //Tolerancia carrera de inyección
                xlWorkSheet.Cells[31, 13].Value = "± " + tbTOLSecCota.Text + "%";  //Tolerancia secuencial por cota
                xlWorkSheet.Cells[31, 30].Value = "± " + tbTOLSecTiempo.Text + "%";  //Tolerancia secuencial por tiempo
                xlWorkSheet.Cells[41, 20].Value = "± " + tbTOLPostPresion.Text + "%";  //Tolerancia post presión

                
                //Notas de arranque
                xlWorkSheet.Cells[58, 4].Value = TbOperacionText1.Text; //NotaOperacion1
                xlWorkSheet.Cells[59, 4].Value = TbOperacionText2.Text; //NotaOperacion2
                xlWorkSheet.Cells[60, 4].Value = TbOperacionText3.Text; //NotaOperacion3
                xlWorkSheet.Cells[61, 4].Value = TbOperacionText4.Text; //NotaOperacion4
                xlWorkSheet.Cells[62, 4].Value = TbOperacionText5.Text; //NotaOperacion5

                //Pie de página
                xlWorkSheet.Cells[63, 4].Value = cbEdicion.Text;
                xlWorkSheet.Cells[64, 4].Value = cbFecha.Text;
                xlWorkSheet.Cells[64, 7].Value = cbElaboradoPor.Text;
                xlWorkSheet.Cells[64, 14].Value = cbRevisadoPor.Text;
                xlWorkSheet.Cells[64, 23].Value = cbAprobadoPor.Text;

                // marcad con cruz
                if (cbNoyos.Checked)
                    xlWorkSheet.Cells[48, 11].Value = "X"; //noyos
                else
                    xlWorkSheet.Cells[48, 11].Value = ""; //noyos)
                if (cbHembra.Checked)
                    xlWorkSheet.Cells[49, 11].Value = "X"; //hembra
                else
                    xlWorkSheet.Cells[49, 11].Value = ""; //hembra
                if (cbMacho.Checked)
                    xlWorkSheet.Cells[50, 11].Value = "X"; //macho
                else
                    xlWorkSheet.Cells[50, 11].Value = ""; //macho
                if (cbAntesExpul.Checked)
                    xlWorkSheet.Cells[51, 11].Value = "X"; //antesExpuls
                else
                    xlWorkSheet.Cells[51, 11].Value = ""; //antesExpuls
                if (cbAntesApert.Checked)
                    xlWorkSheet.Cells[52, 11].Value = "X"; //antesApert
                else
                    xlWorkSheet.Cells[52, 11].Value = ""; //antesApert
                if (cbAntesCierre.Checked)
                    xlWorkSheet.Cells[53, 11].Value = "X"; //antesCierre
                else
                    xlWorkSheet.Cells[53, 11].Value = ""; //antesCierre
                if (cbDespuesCierre.Checked)
                    xlWorkSheet.Cells[54, 11].Value = "X"; //despuesCierre
                else
                    xlWorkSheet.Cells[54, 11].Value = ""; //despuesCierre
                if (cbOtros1.Checked)
                    xlWorkSheet.Cells[55, 11].Value = "X"; //otros
                else
                    xlWorkSheet.Cells[55, 11].Value = ""; //otros
                if (cbBoquilla.Checked)
                    xlWorkSheet.Cells[48, 20].Value = "X"; //boquillaCruz
                else
                    xlWorkSheet.Cells[48, 20].Value = ""; //boquillaCruz
                if (cbCono.Checked)
                    xlWorkSheet.Cells[49, 20].Value = "X"; //cono
                else
                    xlWorkSheet.Cells[49, 20].Value = ""; //cono
                if (cbRadioLarga.Checked)
                    xlWorkSheet.Cells[50, 20].Value = "X"; //radioLarga
                else
                    xlWorkSheet.Cells[50, 20].Value = ""; //radioLarga
                if (cbLibre.Checked)
                    xlWorkSheet.Cells[51, 20].Value = "X"; //libre 
                else
                    xlWorkSheet.Cells[51, 20].Value = ""; //libre 
                if (cbValvula.Checked)
                    xlWorkSheet.Cells[52, 20].Value = "X"; //valvula
                else
                    xlWorkSheet.Cells[52, 20].Value = ""; //valvula
                if (cbResistencia.Checked)
                    xlWorkSheet.Cells[53, 20].Value = "X"; //resistencia
                else
                    xlWorkSheet.Cells[53, 20].Value = ""; //resistencia
                if (cbOtros2.Checked)
                    xlWorkSheet.Cells[54, 20].Value = "X"; //otros2
                else
                    xlWorkSheet.Cells[54, 20].Value = ""; //otros2
                if (cbExpulsion.Checked)
                    xlWorkSheet.Cells[48, 30].Value = "X"; //expulsion
                else
                    xlWorkSheet.Cells[48, 30].Value = ""; //expulsion
                if (cbHidraulica.Checked)
                    xlWorkSheet.Cells[49, 30].Value = "X"; //hidraulica
                else
                    xlWorkSheet.Cells[49, 30].Value = ""; //hidraulica
                if (cbNeumatica.Checked)
                    xlWorkSheet.Cells[50, 30].Value = "X"; //neumatica
                else
                    xlWorkSheet.Cells[50, 30].Value = ""; //neumatica
                if (cbNormal.Checked)
                    xlWorkSheet.Cells[51, 30].Value = "X"; //normal
                else
                    xlWorkSheet.Cells[51, 30].Value = ""; //normal
                if (cbArandela125.Checked)
                    xlWorkSheet.Cells[52, 30].Value = "X"; //arandela125
                else
                    xlWorkSheet.Cells[52, 30].Value = ""; //arandela125
                if (cbArandela160.Checked)
                    xlWorkSheet.Cells[53, 30].Value = "X"; //arandela160
                else
                    xlWorkSheet.Cells[53, 30].Value = ""; //arandela160
                if (cbArandela200.Checked)
                    xlWorkSheet.Cells[54, 30].Value = "X"; //arandela200 
                else
                    xlWorkSheet.Cells[54, 30].Value = ""; //arandela200 
                if (cbArandela250.Checked)
                    xlWorkSheet.Cells[55, 30].Value = "X"; //arandela250
                else
                    xlWorkSheet.Cells[55, 30].Value = ""; //arandela250
                xlWorkSheet.Cells[55, 4].Value = MarcasOtrosText.Text;
                p.Save();

                // Limpiamos la salida
                Response.Clear();
                // Con esto le decimos al browser que la salida sera descargable
                Response.ContentType = "application/octet-stream";
                // esta linea es opcional, en donde podemos cambiar el nombre del fichero a descargar (para que sea diferente al original)
                Response.AddHeader("Content-Disposition", "attachment; filename=Ficha_Parametros_" + tbReferencia.Text + "_" + tbMaquina.Text + ".xlsx");
                // Escribimos el fichero a enviar 
                Response.WriteFile(@"\\FACTS4-SRV\Fichas parametros\FichaReferencia_vacia_V2.xlsx");
                // volcamos el stream 
                Response.Flush();
                // Enviamos todo el encabezado ahora
                Response.End();
            }
            catch (Exception)
            {
            }
        }

        [WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static List<int> Cargar_maquinas_web(string referencia)
        {
            Conexion conexion = new Conexion();
            List<int> listado_maquinas = new List<int>();
            DataSet ds_maquinas = conexion.leer_maquinas_byRef(Convert.ToInt32(referencia));
            foreach (DataRow row in ds_maquinas.Tables[0].Rows)
            {
                listado_maquinas.Add(Convert.ToInt16(row["Maquina"]));
            }
            return listado_maquinas;
        }

    }

}