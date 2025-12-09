using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
 using System.Data.SqlClient;
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

namespace ThermoWeb
{
    public partial class FichasParametros : System.Web.UI.Page
    {
        private string selectedValueMaquina = "";
       // private int maquina = 0;
        private string referenciaprevia = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Ajustar_tamaño_campos();
                Desactivar_campos();
                if (Request.QueryString["REFERENCIA"] != null)
                    {
                    referenciaprevia = Request.QueryString["REFERENCIA"];
                    tbFicha.Value = Request.QueryString["REFERENCIA"].ToString();
                    CargarFicha_function();
                    Desactivar_campos();
                    btnModificar.Visible = true;
                    btnGuardar.Visible = false;
                    btnCancelar.Visible = false;
                    }

                /*//tbFicha.Value = Request.QueryString["REFERENCIA"].ToString();
                if (referenciaprevia != "")
                {
                    tbFicha.Value = Request.QueryString["REFERENCIA"].ToString();
                    cargarFicha_function();
                    desactivar_campos();
                    //btnImportarMaquina.Visible = false;
                    btnModificar.Visible = true;
                    btnGuardar.Visible = false;
                    btnCancelar.Visible = false;
                }*/

            }

        }

        public void CargarFicha(Object sender, EventArgs e)
        {
            CargarFicha_function();
            Desactivar_campos();
            //btnImportarMaquina.Visible = false;
            btnModificar.Visible = true;
            btnGuardar.Visible = false;
            btnCancelar.Visible = false;
        }

        private void Ajustar_tamaño_campos()
        {
            try
            {
                //defino sources de imagenes--reposicionar
                //img1.Src = "http://facts4-srv/thermogestion/Imagenes/directorioparam/sin_imagen.jpg";
                //img5.Src = "http://facts4-srv/thermogestion/Imagenes/directorioparam/sin_imagen.jpg";
                hyperlink5.NavigateUrl = "http://facts4-srv/thermogestion/Imagenes/directorioparam/sin_imagen.jpg";
                hyperlink5.ImageUrl = "http://facts4-srv/thermogestion/Imagenes/directorioparam/sin_imagen.jpg"; 
                hyperlink1.NavigateUrl = "http://facts4-srv/thermogestion/Imagenes/directorioparam/sin_imagen.jpg";
                hyperlink1.ImageUrl = "http://facts4-srv/thermogestion/Imagenes/directorioparam/sin_imagen.jpg"; 
                hyperlink2.NavigateUrl = "http://facts4-srv/thermogestion/Imagenes/directorioparam/sin_imagen.jpg";
                hyperlink2.ImageUrl = "http://facts4-srv/thermogestion/Imagenes/directorioparam/sin_imagen.jpg"; 
                hyperlink3.NavigateUrl = "http://facts4-srv/thermogestion/Imagenes/directorioparam/sin_imagen.jpg";
                hyperlink3.ImageUrl = "http://facts4-srv/thermogestion/Imagenes/directorioparam/sin_imagen.jpg"; 
                hyperlink4.NavigateUrl = "http://facts4-srv/thermogestion/Imagenes/directorioparam/sin_imagen.jpg";
                hyperlink4.ImageUrl = "http://facts4-srv/thermogestion/Imagenes/directorioparam/sin_imagen.jpg";
                hyperlink6.NavigateUrl = "http://facts4-srv/thermogestion/Imagenes/directorioparam/sin_imagen.jpg";
                hyperlink6.ImageUrl = "http://facts4-srv/thermogestion/Imagenes/directorioparam/sin_imagen.jpg";
                hyperlink7.NavigateUrl = "http://facts4-srv/thermogestion/Imagenes/directorioparam/sin_imagen.jpg";
                hyperlink7.ImageUrl = "http://facts4-srv/thermogestion/Imagenes/directorioparam/sin_imagen.jpg";
                hyperlink8.NavigateUrl = "http://facts4-srv/thermogestion/Imagenes/directorioparam/sin_imagen.jpg";
                hyperlink8.ImageUrl = "http://facts4-srv/thermogestion/Imagenes/directorioparam/sin_imagen.jpg";
                
                //principal
                tbReferenciaTitulo.Width = 260;
                tbReferencia.Width = 260;
                tbNombreTitulo.Width = 260;
                tbNombre.Width = 260;
                tbClienteTitulo.Width = 260;
                tbCliente.Width = 260;
                tbCodigoMoldeTitulo.Width = 260;
                tbCodigoMolde.Width = 260;
                tbMaquinaTitulo.Width = 260;
                tbMaquina.Width = 260;
                tbAutomaticoTitulo.Width = 260;
                tbAutomatico.Width = 260;
                tbManualTitulo.Width = 260;
                tbManual.Width = 260;
                tbPersonalTitulo.Width = 260;
                tbPersonal.Width = 260;


                tbProgramaMoldeTitulo.Width = 260;
                tbProgramaMolde.Width = 260;
                tbProgramaRobotTitulo.Width = 260;
                tbProgramaRobot.Width = 260;
                tbManoRobotTitulo.Width = 260;
                tbManoRobot.Width = 260;
                tbAperturaMaquinaTitulo.Width = 260;
                tbAperturaMaquina.Width = 260;
                tbCavidadesTitulo.Width = 260;
                tbCavidades.Width = 260;
                tbPesoPiezaTitulo.Width = 260;
                tbPesoPieza.Width = 260;
                tbPesoColadaTitulo.Width = 260;
                tbPesoColada.Width = 260;
                tbPesoTotalTitulo.Width = 260;
                tbPesoTotal.Width = 260;
                tbFuerzaCierreTitulo.Width = 260;
                tbFuerzaCierre.Width = 260;

                //material
                /*tbCodMaterial.Width = 160;
                tbMaterial.Width = 160;
                tbCodColorante.Width = 160;
                tbColorante.Width = 160;
                tbColor.Width = 160;
                tbTempSecado.Width = 160;
                tbTiempoSecado.Width = 145;

                thCodMaterial.Width = 160;
                thMaterial.Width = 160;
                thCodColorante.Width = 160;
                thColorante.Width = 160;
                thColor.Width = 160;
                thTempSecado.Width = 160;
                thTiempoSecado.Width = 145;
                thCodMaterial2.Width = 160;
                thMaterial2.Width = 160;
                thCodColorante2.Width = 160;
                thColorante2.Width = 160;
                thColor2.Width = 160;
                thTempSecado2.Width = 160;
                thTiempoSecado2.Width = 145;*/

                //cilindro
                tbBoq.Width = 47;
                tbT1.Width = 47;
                tbT2.Width = 47;
                tbT3.Width = 47;
                tbT4.Width = 47;
                tbT5.Width = 47;
                tbT6.Width = 47;
                tbT7.Width = 47;
                tbT8.Width = 47;
                tbT9.Width = 47;
                tbT10.Width = 47;

                thBoq.Width = 47;
                thT1.Width = 47;
                thT2.Width = 47;
                thT3.Width = 47;
                thT4.Width = 47;
                thT5.Width = 47;
                thT6.Width = 47;
                thT7.Width = 47;
                thT8.Width = 47;
                thT9.Width = 47;
                thT10.Width = 47;

                //camara caliente
                tbZ1.Width = 52;
                tbZ2.Width = 52;
                tbZ3.Width = 52;
                tbZ4.Width = 52;
                tbZ5.Width = 52;
                tbZ6.Width = 52;
                tbZ7.Width = 52;
                tbZ8.Width = 52;
                tbZ9.Width = 52;
                tbZ10.Width = 52;
                tbZ11.Width = 52;
                tbZ12.Width = 52;
                tbZ13.Width = 52;
                tbZ14.Width = 52;
                tbZ15.Width = 52;
                tbZ16.Width = 52;
                tbZ17.Width = 52;
                tbZ18.Width = 52;
                tbZ19.Width = 52;
                tbZ20.Width = 52;

                thZ1.Width = 52;
                thZ2.Width = 52;
                thZ3.Width = 52;
                thZ4.Width = 52;
                thZ5.Width = 52;
                thZ6.Width = 52;
                thZ7.Width = 52;
                thZ8.Width = 52;
                thZ9.Width = 52;
                thZ10.Width = 52;
                thZ11.Width = 52;
                thZ12.Width = 52;
                thZ13.Width = 52;
                thZ14.Width = 52;
                thZ15.Width = 52;
                thZ16.Width = 52;
                thZ17.Width = 52;
                thZ18.Width = 52;
                thZ19.Width = 52;
                thZ20.Width = 52;

                //aguas
                //fija
                /*tbRefrig.Width = 200;
                tbAtemp.Width = 320;

                tbNcircuitoRefrig.Width = 200;
                tbNcircuitoAtemp.Width = 160;
                tbTemperatura.Width = 160;

                tbFijaRefrigeracion.Width = 200;
                tbFijaAtempCircuito.Width = 160;
                tbAtempTemperatura.Width = 160;

                //movil
                tbRefrigMovil.Width = 200;
                tbAtempMovil.Width = 320;

                tbNcircuitoRefrigMovil.Width = 200;
                tbFijaAtempCircuitoMovil.Width = 160;
                tbAtempTemperaturaMovil.Width = 160;

                tbMovilRefrigeracion.Width = 200;
                tbMovilAtempCircuito.Width = 160;
                tbMovilAtempTemperatura.Width = 160;*/

                //ATEMPERADO
                AtempTipoF.Width = 980;
                ThCircuitoF.Width = 140;
                ThCircuitoF.Height = 33;
                TbCircuitoF1.Width = 140;
                TbCircuitoF2.Width = 140;
                TbCircuitoF3.Width = 140;
                TbCircuitoF4.Width = 140;
                TbCircuitoF5.Width = 140;
                TbCircuitoF6.Width = 140;
                ThCaudalF.Width = 140;
                TbCaudalF1.Width = 140;
                TbCaudalF2.Width = 140;
                TbCaudalF3.Width = 140;
                TbCaudalF4.Width = 140;
                TbCaudalF5.Width = 140;
                TbCaudalF6.Width = 140;
                ThTemperaturaF.Width = 140;
                TbTemperaturaF1.Width = 140;
                TbTemperaturaF2.Width = 140;
                TbTemperaturaF3.Width = 140;
                TbTemperaturaF4.Width = 140;
                TbTemperaturaF5.Width = 140;
                TbTemperaturaF6.Width = 140;
                ThEntradaF.Width = 140;
                ThEntradaF.Height = 33;
                TbEntradaF1.Width = 140;
                TbEntradaF2.Width = 140;
                TbEntradaF3.Width = 140;
                TbEntradaF4.Width = 140;
                TbEntradaF5.Width = 140;
                TbEntradaF6.Width = 140;
                //ThSalidaF.Width = 120;
                AtempTipoM.Width = 980;
                ThCircuitoM.Width = 140;
                ThCircuitoM.Height = 33;
                TbCircuitoM1.Width = 140;
                TbCircuitoM2.Width = 140;
                TbCircuitoM3.Width = 140;
                TbCircuitoM4.Width = 140;
                TbCircuitoM5.Width = 140;
                TbCircuitoM6.Width = 140;
                ThCaudalM.Width = 140;
                TbCaudalM1.Width = 140;
                TbCaudalM2.Width = 140;
                TbCaudalM3.Width = 140;
                TbCaudalM4.Width = 140;
                TbCaudalM5.Width = 140;
                TbCaudalM6.Width = 140;
                ThTemperaturaM.Width = 140;
                TbTemperaturaM1.Width = 140;
                TbTemperaturaM2.Width = 140;
                TbTemperaturaM3.Width = 140;
                TbTemperaturaM4.Width = 140;
                TbTemperaturaM5.Width = 140;
                TbTemperaturaM6.Width = 140;
                ThEntradaM.Width = 140;
                ThEntradaM.Height = 33;
                TbEntradaM1.Width = 140;
                TbEntradaM2.Width = 140;
                TbEntradaM3.Width = 140;
                TbEntradaM4.Width = 140;
                TbEntradaM5.Width = 140;
                TbEntradaM6.Width = 140; 
             

                //inyeccion
                tbPaso.Width = 120;
                tbVelocidad.Width = 120;
                tbCarrera.Width = 120;
                tb1.Width = 50;
                tb2.Width = 50;
                tb3.Width = 50;
                tb4.Width = 50;
                tb5.Width = 50;
                tb6.Width = 50;
                tb7.Width = 50;
                tb8.Width = 50;
                tb9.Width = 50;
                tb10.Width = 50;
                tb11.Width = 50;
                tb12.Width = 50;
                thV1.Width = 50;
                thV2.Width = 50;
                thV3.Width = 50;
                thV4.Width = 50;
                thV5.Width = 50;
                thV6.Width = 50;
                thV7.Width = 50;
                thV8.Width = 50;
                thV9.Width = 50;
                thV10.Width = 50;
                thV11.Width = 50;
                thV12.Width = 50;
                thC1.Width = 50;
                thC2.Width = 50;
                thC3.Width = 50;
                thC4.Width = 50;
                thC5.Width = 50;
                thC6.Width = 50;
                thC7.Width = 50;
                thC8.Width = 50;
                thC9.Width = 50;
                thC10.Width = 50;
                thC11.Width = 50;
                thC12.Width = 50;
                tbTiempoInyeccionTitulo.Width = 170;
                tbLimitePresionTitulo.Width = 170;
                tbTiempoInyeccion.Width = 170;
                tbLimitePresion.Width = 170;
                tbConmutacionTitulo.Width = 170;
                tbTiempoPresionTitulo.Width = 170;
                tbConmutacion.Width = 170;
                tbTiempoPresion.Width = 170;

                tbTiempoInyeccionN.Width = 42;
                tbTiempoInyeccionNVal.Width = 42;
                tbTiempoInyeccionM.Width = 42;
                tbTiempoInyeccionMVal.Width = 42;
                tbLimitePresionN.Width = 42;
                tbLimitePresionNVal.Width = 42;
                tbLimitePresionM.Width = 42;
                tbLimitePresionMval.Width = 42;

                 thConmuntaciontolN.Width = 42;
                 thConmuntaciontolNVal.Width = 42;
                 thConmuntaciontolM.Width = 42;
                 thConmuntaciontolMVal.Width = 42;
                 tbTiempoPresiontolN.Width = 42;
                 tbTiempoPresiontolNVal.Width = 42;
                 tbTiempoPresiontolM.Width = 42;
                 tbTiempoPresiontolMVal.Width = 42;

                //inyección-secuencial
                tituloporcota.Width = 570;
                tituloBoquilla.Width = 90;
                titulosecuencial1.Width = 47;
                titulosecuencial2.Width = 47;
                titulosecuencial3.Width = 47;
                titulosecuencial4.Width = 47;
                titulosecuencial5.Width = 47;
                titulosecuencial6.Width = 47;
                titulosecuencial7.Width = 47;
                titulosecuencial8.Width = 47;
                titulosecuencial9.Width = 47;
                titulosecuencial10.Width = 47;
                lineabrir1secu.Width = 90;
                seqAbrir1_1.Width = 47;
                seqAbrir1_2.Width = 47;
                seqAbrir1_3.Width = 47;
                seqAbrir1_4.Width = 47;
                seqAbrir1_5.Width = 47;
                seqAbrir1_6.Width = 47;
                seqAbrir1_7.Width = 47;
                seqAbrir1_8.Width = 47;
                seqAbrir1_9.Width = 47;
                seqAbrir1_10.Width = 47;
                linecerrar1secu.Width = 90;
                seqCerrar1_1.Width = 47;
                seqCerrar1_2.Width = 47;
                seqCerrar1_3.Width = 47;
                seqCerrar1_4.Width = 47;
                seqCerrar1_5.Width = 47;
                seqCerrar1_6.Width = 47;
                seqCerrar1_7.Width = 47;
                seqCerrar1_8.Width = 47;
                seqCerrar1_9.Width = 47;
                seqCerrar1_10.Width = 47;
                lineabrir2secu.Width = 90;
                seqAbrir2_1.Width = 47;
                seqAbrir2_2.Width = 47;
                seqAbrir2_3.Width = 47;
                seqAbrir2_4.Width = 47;
                seqAbrir2_5.Width = 47;
                seqAbrir2_6.Width = 47;
                seqAbrir2_7.Width = 47;
                seqAbrir2_8.Width = 47;
                seqAbrir2_9.Width = 47;
                seqAbrir2_10.Width = 47;
                linecerrar2secu.Width = 90;
                seqCerrar2_1.Width = 47;
                seqCerrar2_2.Width = 47;
                seqCerrar2_3.Width = 47;
                seqCerrar2_4.Width = 47;
                seqCerrar2_5.Width = 47;
                seqCerrar2_6.Width = 47;
                seqCerrar2_7.Width = 47;
                seqCerrar2_8.Width = 47;
                seqCerrar2_9.Width = 47;
                seqCerrar2_10.Width = 47;
                lineTPresPost.Width = 90;
                seqTPresPost1.Width = 47;
                seqTPresPost2.Width = 47;
                seqTPresPost3.Width = 47;
                seqTPresPost4.Width = 47;
                seqTPresPost5.Width = 47;
                seqTPresPost6.Width = 47;
                seqTPresPost7.Width = 47;
                seqTPresPost8.Width = 47;
                seqTPresPost9.Width = 47;
                seqTPresPost10.Width = 47;

                tituloporTiempo.Width = 570;
                tituloBoquillaTiempo.Width = 90;
                titulosecuencialTiempo1.Width = 47;
                titulosecuencialTiempo2.Width = 47;
                titulosecuencialTiempo3.Width = 47;
                titulosecuencialTiempo4.Width = 47;
                titulosecuencialTiempo5.Width = 47;
                titulosecuencialTiempo6.Width = 47;
                titulosecuencialTiempo7.Width = 47;
                titulosecuencialTiempo8.Width = 47;
                titulosecuencialTiempo9.Width = 47;
                titulosecuencialTiempo10.Width = 47;
                lineTiempoRetardo.Width = 90;
                seqTiempoRetardo_1.Width = 47;
                seqTiempoRetardo_2.Width = 47;
                seqTiempoRetardo_3.Width = 47;
                seqTiempoRetardo_4.Width = 47;
                seqTiempoRetardo_5.Width = 47;
                seqTiempoRetardo_6.Width = 47;
                seqTiempoRetardo_7.Width = 47;
                seqTiempoRetardo_8.Width = 47;
                seqTiempoRetardo_9.Width = 47;
                seqTiempoRetardo_10.Width = 47;
                seqAnotacionesTitulo.Width = 90;
                seqAnotaciones.Width = 480;

                //secuencial tiempos de inyección

                //tbTiempoSecuencialTitulo.Width = 170;
                //tbPresionSecuencialTitulo.Width = 170;
                //tbTiempoSecuencial.Width = 170;
                //tbPresionSecuencial.Width = 170;
                //tbTiempoSecuencialN.Width = 42;
                //tbTiempoSecuencialNVal.Width = 42;
                //tbTiempoSecuencialM.Width = 42;
                //tbTiempoSecuencialMVal.Width = 42;
                //tbPresionSecuencialN.Width = 42;
                //tbPresionSecuencialNVal.Width = 42;
                //tbPresionSecuencialM.Width = 42;
                //tbPresionSecuencialMVal.Width = 42;


                //post-presion
                tbPasoPresion.Width = 120;
                thPresion.Width = 120;
                thTPtiempo.Width = 120;
                tbP1.Width = 50;
                tbP2.Width = 50;
                tbP3.Width = 50;
                tbP4.Width = 50;
                tbP5.Width = 50;
                tbP6.Width = 50;
                tbP7.Width = 50;
                tbP8.Width = 50;
                tbP9.Width = 50;
                tbP10.Width = 50;
                thP1.Width = 50;
                thP2.Width = 50;
                thP3.Width = 50;
                thP4.Width = 50;
                thP5.Width = 50;
                thP6.Width = 50;
                thP7.Width = 50;
                thP8.Width = 50;
                thP9.Width = 50;
                thP10.Width = 50;
                thTP1.Width = 50;
                thTP2.Width = 50;
                thTP3.Width = 50;
                thTP4.Width = 50;
                thTP5.Width = 50;
                thTP6.Width = 50;
                thTP7.Width = 50;
                thTP8.Width = 50;
                thTP9.Width = 50;
                thTP10.Width = 50;

                //dosificación
                tbVCarga.Width = 160;
                tbCarga.Width = 160;
                tbDescom.Width = 160;
                tbContra.Width = 160;
                tbTiempoDos.Width = 160;
                tbEnfriamiento.Width = 160;
                tbCiclo.Width = 160;
                tbCojin.Width = 160;

                thVCarga.Width = 160;
                thCarga.Width = 160;
                thDescomp.Width = 160;
                thContrapr.Width = 160;
                thTiempo.Width = 160;
                thEnfriamiento.Width = 160;
                thCiclo.Width = 160;
                thCojin.Width = 160;


                TMvcarga.Width = 40;
                TMvcargaval.Width = 40;
                TNvcarga.Width = 40;
                TNvcargaval.Width = 40;
                TMcarga.Width = 40;
                TMcargaval.Width = 40;
                TNcarga.Width = 40;
                TNcargaval.Width = 40;
                TMdescom.Width = 40;
                TMdescomval.Width = 40;
                TNdescom.Width = 40;
                TNdescomval.Width = 40;
                TMcontrap.Width = 40;
                TMcontrapval.Width = 40;
                TNcontrap.Width = 40;
                TNcontrapval.Width = 40;
                TMTiempdos.Width = 40;
                TMTiempdosval.Width = 40;
                TNTiempdos.Width = 40;
                TNTiempdosval.Width = 40;
                TMEnfriam.Width = 40;
                TMEnfriamval.Width = 40;
                TNEnfriam.Width = 40;
                TNEnfriamval.Width = 40;
                TMCiclo.Width = 40;
                TMCicloval.Width = 40;
                TNCiclo.Width = 40;
                TNCicloval.Width = 40;
                TNCojin.Width = 40;
                TNCojinval.Width = 40;
                TMCojin.Width = 40;
                TMCojinval.Width = 40;
                //Operaciones
                ThOperacionNum.Width = 160;
                ThOperacionTitulo.Width = 800;
                TbOperacionN1.Width = 160;
                TbOperacionText1.Width = 800;
                TbOperacionN2.Width = 160;
                TbOperacionText2.Width = 800;
                TbOperacionN3.Width = 160;
                TbOperacionText3.Width = 800;
                TbOperacionN4.Width = 160;
                TbOperacionText4.Width = 800;
                TbOperacionN5.Width = 160;
                TbOperacionText5.Width = 800;
                //MarcasOtrosTitulo.Width = 160;
                MarcasOtrosText.Width = 800;
                //datos de la ficha
                cbEdicionTitulo.Width = 220;
                cbFechaTitulo.Width = 220;
                cbElaboradoPorTitulo.Width = 220;
                cbRevisadoPorTitulo.Width = 220;
                cbAprobadoPorTitulo.Width = 220;
                cbEdicion.Width = 220;
                cbEdicion.Height = 33;
                cbFecha.Width = 220;
                cbFecha.Height = 33;
                cbElaboradoPor.Width = 220;
                cbRevisadoPor.Width = 220;
                cbAprobadoPor.Width = 220;
                tbObservacionesTitulo.Width = 660;
                tbObservaciones.Width = 660;
                tbRazonesTitulo.Width = 440;
                tbRazones.Width = 440;

            }
            catch (Exception)
            {

            }
        }

        private void CargarFicha_function()
        {
            try
            {
                Conexion conexion = new Conexion();
                selectedValueMaquina = Request.Form[lista_maquinas.UniqueID];                
                Cargar_maquinas(Convert.ToInt32(tbFicha.Value.ToString()));
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

                Cargar_historico_modificaciones(Convert.ToInt32(tbFicha.Value.ToString()), Convert.ToInt32(selectedValueMaquina));
                Cargar_estructura(Convert.ToInt32(tbFicha.Value.ToString()));
                DataSet ds = conexion.leerFicha(Convert.ToInt32(tbFicha.Value), Convert.ToInt32(selectedValueMaquina), version);
                if (ds.Tables[0].Rows.Count > 0)
                {                    
                    tbReferencia.Text = ds.Tables[0].Rows[0]["Referencia"].ToString();
                    tbNombre.Text = ds.Tables[0].Rows[0]["Descripcion"].ToString();
                    tbCliente.Text = ds.Tables[0].Rows[0]["Cliente"].ToString();
                    tbCodigoMolde.Text = ds.Tables[0].Rows[0]["CodMolde"].ToString();
                    tbMaquina.Text = ds.Tables[0].Rows[0]["NMaquina"].ToString();
                    tbAutomatico.Text = ds.Tables[0].Rows[0]["Automatico"].ToString();
                    tbManual.Text = ds.Tables[0].Rows[0]["Manual"].ToString();
                    tbPersonal.Text = ds.Tables[0].Rows[0]["PersonalAsignado"].ToString();
                    tbProgramaMolde.Text = ds.Tables[0].Rows[0]["ProgramaMolde"].ToString();
                    tbProgramaRobot.Text = ds.Tables[0].Rows[0]["NProgramaRobot"].ToString();
                    //bManoRobot.Text = ds.Tables[0].Rows[0]["NManoRobot"].ToString();
                    tbManoRobot.Text = conexion.Devuelve_ManoVinculada(tbCodigoMolde.Text);
                    tbAperturaMaquina.Text = ds.Tables[0].Rows[0]["AperturaMaquina"].ToString();
                    tbCavidades.Text = ds.Tables[0].Rows[0]["NCavidades"].ToString();
                    tbPesoPieza.Text = ds.Tables[0].Rows[0]["PesoPieza"].ToString();
                    tbPesoColada.Text = ds.Tables[0].Rows[0]["PesoColada"].ToString();
                    tbPesoTotal.Text = ds.Tables[0].Rows[0]["PesoTotal"].ToString();
                    thVCarga.Text = ds.Tables[0].Rows[0]["VelocidadCarga"].ToString();
                    thCarga.Text = ds.Tables[0].Rows[0]["Carga"].ToString();
                    thDescomp.Text = ds.Tables[0].Rows[0]["Descompresion"].ToString();
                    thContrapr.Text = ds.Tables[0].Rows[0]["Contrapresion"].ToString();
                    thTiempo.Text = ds.Tables[0].Rows[0]["Tiempo"].ToString();
                    thEnfriamiento.Text = ds.Tables[0].Rows[0]["Enfriamiento"].ToString();
                    thCiclo.Text = ds.Tables[0].Rows[0]["Ciclo"].ToString();
                    thCojin.Text = ds.Tables[0].Rows[0]["Cojin"].ToString();
                    tbRazones.Text = ds.Tables[0].Rows[0]["Razones"].ToString();
                    cbEdicion.Text = ds.Tables[0].Rows[0]["Version"].ToString();
                    cbFecha.Text = ds.Tables[0].Rows[0]["Fecha"].ToString();
                    tbFuerzaCierre.Text = ds.Tables[0].Rows[0]["FuerzaCierre"].ToString();
                    //cbElaboradoPor.Text = ds.Tables[0].Rows[0]["Elaborado"].ToString(); //listar
                    //cbRevisadoPor.Text = ds.Tables[0].Rows[0]["Revisado"].ToString(); //listar
                    //cbAprobadoPor.Text = ds.Tables[0].Rows[0]["Aprobado"].ToString(); //listar
                    tbObservaciones.Text = ds.Tables[0].Rows[0]["Observaciones"].ToString();

                    //CARGARESPONSABLES
                    Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                    DataSet Operarios = SHConexion.Devuelve_mandos_intermedios_SMARTH();
                   
                    //ELABORADO
                    Operarios.Tables[0].DefaultView.RowFilter = "Departamento = 'PRODUCCION' OR Departamento = '-'";
                    DataTable DTPRODUCCION = (Operarios.Tables[0].DefaultView).ToTable();
                    cbElaboradoPor.Items.Clear();
                    foreach (DataRow row in DTPRODUCCION.Rows)
                    { cbElaboradoPor.Items.Add(row["Nombre"].ToString()); }
                    cbElaboradoPor.ClearSelection();
                    if (ds.Tables[0].Rows[0]["INTElaborado"].ToString() != "")
                    {
                        try
                        {
                            cbElaboradoPor.SelectedValue = SHConexion.Devuelve_Pilotos_SMARTH(Convert.ToInt16(ds.Tables[0].Rows[0]["INTElaborado"].ToString()));
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
                    if (ds.Tables[0].Rows[0]["INTRevisado"].ToString() != "")
                    {
                        try
                        {
                            cbRevisadoPor.SelectedValue = SHConexion.Devuelve_Pilotos_SMARTH(Convert.ToInt16(ds.Tables[0].Rows[0]["INTRevisado"].ToString()));
                        }
                        catch (Exception ex)
                        {
                            cbRevisadoPor.SelectedValue = "-";
                        }

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
                    if (ds.Tables[0].Rows[0]["INTAprobado"].ToString() != "")
                        {
                        try
                        {
                            cbAprobadoPor.SelectedValue = SHConexion.Devuelve_Pilotos_SMARTH(Convert.ToInt16(ds.Tables[0].Rows[0]["INTAprobado"].ToString()));
                        }
                        catch (Exception ex)
                        {
                            cbAprobadoPor.SelectedValue = "-";
                        }
                    }
                    else 
                        { 
                            cbAprobadoPor.SelectedValue = "-";
                        }


                }


                

                ds = conexion.leerTempCilindro(Convert.ToInt32(tbFicha.Value), Convert.ToInt32(selectedValueMaquina), version);
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
                    hyperlink1.NavigateUrl = "http://FACTS4-SRV/oftecnica/imagenes2/directorio/sin_imagen.jpg";
                    hyperlink1.ImageUrl = "http://FACTS4-SRV/oftecnica/imagenes2/directorio/sin_imagen.jpg";
                    //img1.Src = "http://facts4-srv/thermogestion/Imagenes/directorioparam/sin_imagen.jpg";
                }
                if (imagen2 != "")
                {
                    hyperlink2.NavigateUrl = imagen2;
                    hyperlink2.ImageUrl = imagen2;
                    //img2.Src = imagen2;
                }
                else
                {
                    hyperlink2.NavigateUrl = "http://facts4-srv/thermogestion/Imagenes/directorioparam/sin_imagen.jpg";
                    hyperlink2.ImageUrl = "http://facts4-srv/thermogestion/Imagenes/directorioparam/sin_imagen.jpg";
                    //img2.Src = "http://FACTS4-SRV/oftecnica/imagenes2/directorio/sin_imagen.jpg";
                }
                if (imagen3 != "")
                {
                    hyperlink3.NavigateUrl = imagen3;
                    hyperlink3.ImageUrl = imagen3;
                    //img3.Src = imagen3;
                }
                else
                {
                    hyperlink3.NavigateUrl = "http://facts4-srv/thermogestion/Imagenes/directorioparam/sin_imagen.jpg";
                    hyperlink3.ImageUrl = "http://facts4-srv/thermogestion/Imagenes/directorioparam/sin_imagen.jpg";
                    //img3.Src = "http://FACTS4-SRV/oftecnica/imagenes2/directorio/sin_imagen.jpg";
                }
                if (imagen4 != "")
                {
                    hyperlink4.NavigateUrl = imagen4;
                    hyperlink4.ImageUrl = imagen4;
                    //img4.Src = imagen4;
                }
                else
                {
                    hyperlink4.NavigateUrl = "http://facts4-srv/thermogestion/Imagenes/directorioparam/sin_imagen.jpg";
                    hyperlink4.ImageUrl = "http://facts4-srv/thermogestion/Imagenes/directorioparam/sin_imagen.jpg";
                    //img4.Src = "http://FACTS4-SRV/oftecnica/imagenes2/directorio/sin_imagen.jpg";
                }
                if (imagen5 != "")
                {
                    hyperlink5.NavigateUrl = imagen5;
                    hyperlink5.ImageUrl = imagen5;
                    //img5.Src = imagen5;
                }
                else
                {
                    hyperlink5.NavigateUrl = "http://FACTS4-SRV/oftecnica/imagenes2/directorio/sin_imagen.jpg";
                    hyperlink5.ImageUrl = "http://FACTS4-SRV/oftecnica/imagenes2/directorio/sin_imagen.jpg";
                    //img5.Src = "http://facts4-srv/thermogestion/Imagenes/directorioparam/sin_imagen.jpg";
                }
                if (imagen6 != "")
                {
                    hyperlink6.NavigateUrl = imagen6;
                    hyperlink6.ImageUrl = imagen6;
                    //img6.Src = imagen6;
                }
                else
                {
                    hyperlink6.NavigateUrl = "http://facts4-srv/thermogestion/Imagenes/directorioparam/sin_imagen.jpg";
                    hyperlink6.ImageUrl = "http://facts4-srv/thermogestion/Imagenes/directorioparam/sin_imagen.jpg";
                    //img6.Src = "http://FACTS4-SRV/oftecnica/imagenes2/directorio/sin_imagen.jpg";
                }
                if (imagen7 != "")
                {
                    hyperlink7.NavigateUrl = imagen7;
                    hyperlink7.ImageUrl = imagen7;
                    //img7.Src = imagen7;
                }
                else
                {
                    hyperlink7.NavigateUrl = "http://facts4-srv/thermogestion/Imagenes/directorioparam/sin_imagen.jpg";
                    hyperlink7.ImageUrl = "http://facts4-srv/thermogestion/Imagenes/directorioparam/sin_imagen.jpg";
                    //img7.Src = "http://FACTS4-SRV/oftecnica/imagenes2/directorio/sin_imagen.jpg";
                }
                if (imagen8 != "")
                {
                    hyperlink8.NavigateUrl = imagen8;
                    hyperlink8.ImageUrl = imagen8;
                    //img8.Src = imagen8;
                }
                else
                {
                    hyperlink8.NavigateUrl = "http://facts4-srv/thermogestion/Imagenes/directorioparam/sin_imagen.jpg";
                    hyperlink8.ImageUrl = "http://facts4-srv/thermogestion/Imagenes/directorioparam/sin_imagen.jpg";
                    //img8.Src = "http://FACTS4-SRV/oftecnica/imagenes2/directorio/sin_imagen.jpg";
                }
            }
            catch (Exception)
            {
            }
        }

        public void Recalcular_Limites(Object sender, EventArgs e)
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
                        }
                        catch (Exception)
                        {
                            tbLimitePresion.BackColor = System.Drawing.Color.Red;
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


                        GuardarFicha(null,null);
                        break;
                }

            }
            catch { }
        }

        public void Validar_ficha(Object sender, EventArgs e)
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
        private void CargarListaFichas(DataSet ds)
        {
            try
            {
                //lista_fichas.Items.Clear();
                //foreach (DataRow row in ds.Tables[0].Rows)
                //{
                //    lista_fichas.Items.Add(row["Referencia"].ToString());                    
                //}                               
            }
            catch (Exception)
            {

            }
        }

        public void GuardarFicha(Object sender, EventArgs e)
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
                
                string selectedValueMaquina = Request.Form[lista_maquinas.UniqueID];
                int version = conexion.leer_maxima_version(Convert.ToInt32(tbReferencia.Text), Convert.ToInt32(selectedValueMaquina));
                if (conexion.existeFicha(Convert.ToInt32(tbReferencia.Text), Convert.ToInt32(selectedValueMaquina), version))
                //if (conexion.existeFicha(Convert.ToInt32(tbReferencia.Text), Convert.ToInt32(tbMaquina.Text), version))
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
                    conexion.insertarFicha(Convert.ToInt32(tbReferencia.Text), Convert.ToInt32(selectedValueMaquina), version, tbNombre.Text, tbCliente.Text,
                    //conexion.insertarFicha(Convert.ToInt32(tbReferencia.Text), Convert.ToInt32(tbMaquina.Text), version, tbNombre.Text, tbCliente.Text,
                    tbCodigoMolde.Text, tbMaquina.Text, tbAutomatico.Text, tbManual.Text,
                    tbPersonal.Text, tbProgramaMolde.Text, tbProgramaRobot.Text, tbManoRobot.Text,
                    tbAperturaMaquina.Text, tbCavidades.Text, tbPesoPieza.Text, tbPesoColada.Text, tbPesoTotal.Text, 
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
                    tbTiempoInyeccion.Text, tbLimitePresion.Text,
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
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "guardado_OK();", true);

                }               
            }
            catch (Exception)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "guardado_NOK();", true);
            }
        }

        public void Insertar_foto(Object sender, EventArgs e)
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
                string savePath = "C:\\inetpub_thermoweb\\Imagenes\\";
                

                // Get the name of the file to upload.
                //string fileName = FileUpload1.FileName;
                string fileName = "";
                switch (num_img)
                {
                    case 1:
                        fileName = FileUpload1.FileName;
                        break;
                    case 2:
                        fileName = FileUpload2.FileName;
                        break;
                    case 3:
                        fileName = FileUpload3.FileName;
                        break;
                    case 4:
                        fileName = FileUpload4.FileName;
                        break;
                    case 5:
                        fileName = FileUpload5.FileName;
                        break;
                    case 6:
                        fileName = FileUpload6.FileName;
                        break;
                    case 7:
                        fileName = FileUpload7.FileName;
                        break;
                    case 8:
                        fileName = FileUpload8.FileName;
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
                        //img1.Src = "http://facts4-srv/thermogestion/Imagenes/directorioparam/" + fileName;
                        hyperlink1.ImageUrl = "http://facts4-srv/thermogestion/Imagenes/directorioparam/" + fileName;
                        break;
                    case 2:
                        FileUpload2.SaveAs(savePath);
                        hyperlink2.ImageUrl = "http://facts4-srv/thermogestion/Imagenes/directorioparam/" + fileName;
                        break;
                    case 3:
                        FileUpload3.SaveAs(savePath);
                        hyperlink3.ImageUrl = "http://facts4-srv/thermogestion/Imagenes/directorioparam/" + fileName;
                        break;
                    case 4:
                        FileUpload4.SaveAs(savePath);
                        hyperlink4.ImageUrl = "http://facts4-srv/thermogestion/Imagenes/directorioparam/" + fileName;
                        break;
                    case 5:
                        FileUpload5.SaveAs(savePath);
                        hyperlink5.ImageUrl = "http://facts4-srv/thermogestion/Imagenes/directorioparam/" + fileName;
                        //img5.Src = "http://facts4-srv/thermogestion/Imagenes/directorioparam/" + fileName;
                        break;
                    case 6:
                        FileUpload6.SaveAs(savePath);
                        hyperlink6.ImageUrl = "http://facts4-srv/thermogestion/Imagenes/directorioparam/" + fileName;
                        break;
                    case 7:
                        FileUpload7.SaveAs(savePath);
                        hyperlink7.ImageUrl = "http://facts4-srv/thermogestion/Imagenes/directorioparam/" + fileName;
                        break;
                    case 8:
                        FileUpload8.SaveAs(savePath);
                        hyperlink8.ImageUrl = "http://facts4-srv/thermogestion/Imagenes/directorioparam/" + fileName;
                        break;
                    default: break;
                }
            }
            catch (Exception)
            {
            }

        }     



        public void ImportarExcel(Object sender, EventArgs e)
        {
            //try
            //{
            //    FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            //    if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            //    {
            //        dir = folderBrowserDialog1.SelectedPath;
            //        rellenar_grid();
            //    }            
            //}
            //catch(Exception)
            //{
            //}
        }

        public void ImportarMaquina(Object sender, EventArgs e)
        {
            try
            {
                Conexion conexion = new Conexion();
                conexion.comprobarMaquina(Convert.ToInt32(tbReferencia.Text));            
            }
            catch(Exception)
            {
            }
        }


        public void Imprimir_ficha(Object sender, EventArgs e)
        {
            try
            {
                 //specify the file name where its actually exist   
                //string filepath = @"\\FACTS4-SRV\Fichas parametros\FichaReferencia_vacia.xlsx";  

                ////open the excel using openxml sdk  
                //using(SpreadsheetDocument doc = SpreadsheetDocument.Open(filepath, false))  
                //{    
                //    //create the object for workbook part  
                //    WorkbookPart wbPart = doc.WorkbookPart;  

                //    //statement to get the count of the worksheet  
                //    int worksheetcount = doc.WorkbookPart.Workbook.Sheets.Count();  

                //    //statement to get the sheet object  
                //    Sheet mysheet = (Sheet) doc.WorkbookPart.Workbook.Sheets.ChildElements.GetItem(0);  

                //    //statement to get the worksheet object by using the sheet id  
                //    DocumentFormat.OpenXml.Spreadsheet.Worksheet Worksheet = ((WorksheetPart) wbPart.GetPartById(mysheet.Id)).Worksheet;  

                //    //Note: worksheet has 8 children and the first child[1] = sheetviewdimension,....child[4]=sheetdata  
                //    int wkschildno = 4;  


                //    //statement to get the sheetdata which contains the rows and cell in table  
                //SheetData Rows = (SheetData)Worksheet.ChildElements.GetItem(wkschildno);


                //    //getting the row as per the specified index of getitem method  
                //    Row currentrow = (Row) Rows.ChildElements.GetItem(13);  

                //    //getting the cell as per the specified index of getitem method  
                //    Cell currentcell = (Cell) currentrow.ChildElements.GetItem(1);
                //    currentcell.CellValue = new CellValue("HOLA");
                //    currentrow.Append(currentcell);
                //    Rows.Append(currentrow);
                //    //statement to take the integer value  
                //    string currentcellvalue = currentcell.InnerText;





                

                //using (SpreadsheetDocument xl = SpreadsheetDocument.Open(filepath, true))
                //{
                //    WorkbookPart wbp = xl.WorkbookPart;

                //    Sheet mysheet = (Sheet)xl.WorkbookPart.Workbook.Sheets.ChildElements.GetItem(0);

                //    //statement to get the worksheet object by using the sheet id  
                //    DocumentFormat.OpenXml.Spreadsheet.Worksheet Worksheet = ((WorksheetPart)wbp.GetPartById(mysheet.Id)).Worksheet;
                //    DocumentFormat.OpenXml.Spreadsheet.Workbook wb = new DocumentFormat.OpenXml.Spreadsheet.Workbook();
                //    FileVersion fv = new FileVersion();
                //    fv.ApplicationName = "Microsoft Office Excel";


                //   //Note: worksheet has 8 children and the first child[1] = sheetviewdimension,....child[4]=sheetdata  
                //    int wkschildno = 4;  


                //    //statement to get the sheetdata which contains the rows and cell in table  
                //    SheetData Rows = (SheetData)Worksheet.ChildElements.GetItem(wkschildno);
                //    Row currentrow = (Row)Rows.ChildElements.GetItem(13);

                    
                //    Cell currentcell = (Cell) currentrow.ChildElements.GetItem(1);
                //    currentcell.CellValue = new CellValue("HOLA");
                   


                //    ////third cell
                //    //Row r2 = new Row() { RowIndex = (UInt32Value)2u };
                //    //Cell c3 = new Cell();
                //    //c3.DataType = CellValues.String;
                //    //c3.CellValue = new CellValue("some string");
                //    //r2.Append(c3);
                //    //Rows.Append(r2);

                //    //Worksheet.Append(Rows);

                //    DocumentFormat.OpenXml.Spreadsheet.Sheets sheets = new DocumentFormat.OpenXml.Spreadsheet.Sheets();
                //    Sheet sheet = new Sheet();
                //    sheet.Name = "first sheet";
                //    sheets.Append(sheet);
                //    wb.Append(fv);
                //    wb.Append(sheets);

                //    xl.WorkbookPart.Workbook = wb;
                //    xl.WorkbookPart.Workbook.Save();
                //    xl.Close();



                //}

                string filepath = @"\\FACTS4-SRV\Fichas parametros\FichaReferencia_vacia.xlsx";
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
                xlWorkSheet.Cells[28, 16].Value = thV8.Text;
                xlWorkSheet.Cells[29, 16].Value = thC8.Text;
                xlWorkSheet.Cells[28, 17].Value = thV9.Text;
                xlWorkSheet.Cells[29, 17].Value = thC9.Text;
                xlWorkSheet.Cells[28, 18].Value = thV10.Text;
                xlWorkSheet.Cells[29, 18].Value = thC10.Text;
                xlWorkSheet.Cells[28, 20].Value = thV11.Text;
                xlWorkSheet.Cells[29, 20].Value = thC11.Text;
                xlWorkSheet.Cells[28, 21].Value = thV12.Text;
                xlWorkSheet.Cells[29, 21].Value = thC12.Text;
                xlWorkSheet.Cells[28, 23].Value = tbTiempoInyeccion.Text;
                xlWorkSheet.Cells[28, 28].Value = tbLimitePresion.Text;

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
                xlWorkSheet.Cells[41, 16].Value = thP7.Text;
                xlWorkSheet.Cells[42, 16].Value = thTP7.Text;
                xlWorkSheet.Cells[41, 17].Value = thP8.Text;
                xlWorkSheet.Cells[42, 17].Value = thTP8.Text;
                xlWorkSheet.Cells[41, 18].Value = thP9.Text;
                xlWorkSheet.Cells[42, 18].Value = thTP9.Text;
                xlWorkSheet.Cells[41, 20].Value = thP10.Text;
                xlWorkSheet.Cells[42, 20].Value = thTP10.Text;
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
                
                xlWorkSheet.Cells[28, 26].Value = tbTiempoInyeccionMVal.Text; //tiempoInyeccionMIN
                xlWorkSheet.Cells[29, 26].Value = tbTiempoInyeccionNVal.Text; //tiempoInyeccionMIN
                xlWorkSheet.Cells[28, 31].Value = tbLimitePresionMval.Text; //limitePresionMIN
                xlWorkSheet.Cells[29, 31].Value = tbLimitePresionNVal.Text; //limitePresionMIN
                
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
                Response.WriteFile(@"\\FACTS4-SRV\Fichas parametros\FichaReferencia_vacia.xlsx");
                // volcamos el stream 
                Response.Flush();
                // Enviamos todo el encabezado ahora
                Response.End();
            }
            catch (Exception)
            {
            }
        }

        public static void UpdateCell(string docName, string text,
            uint rowIndex, string columnName)
        {
            // Open the document for editing.
            using (SpreadsheetDocument spreadSheet = 
                     SpreadsheetDocument.Open(docName, true))
            {
                WorksheetPart worksheetPart = 
                      GetWorksheetPartByName(spreadSheet, "Sheet1");

                if (worksheetPart != null)
                {
                    Cell cell = GetCell(worksheetPart.Worksheet, 
                                             columnName, rowIndex);

                    cell.CellValue = new CellValue(text);
                    cell.DataType = 
                        new EnumValue<CellValues>(CellValues.Number);

                    // Save the worksheet.
                    worksheetPart.Worksheet.Save();
                }
            }
        }

        private static WorksheetPart GetWorksheetPartByName(SpreadsheetDocument document, string sheetName)
        {
            IEnumerable<Sheet> sheets = document.WorkbookPart.Workbook.GetFirstChild<DocumentFormat.OpenXml.Spreadsheet.Sheets>().Elements<Sheet>().Where(s => s.Name == sheetName);

            if (sheets.Count() == 0)
            {
                // The specified worksheet does not exist.

                return null;
            }

            string relationshipId = sheets.First().Id.Value;
            WorksheetPart worksheetPart = (WorksheetPart)
                 document.WorkbookPart.GetPartById(relationshipId);
            return worksheetPart;

        }

        // Given a worksheet, a column name, and a row index, 
        // gets the cell at the specified column and 
        private static Cell GetCell(DocumentFormat.OpenXml.Spreadsheet.Worksheet worksheet, 
                  string columnName, uint rowIndex)
        {
            Row row = GetRow(worksheet, rowIndex);

            if (row == null)
                return null;

            return row.Elements<Cell>().Where(c => string.Compare
                   (c.CellReference.Value, columnName + 
                   rowIndex, true) == 0).First();
        }


        // Given a worksheet and a row index, return the row.
        private static Row GetRow(DocumentFormat.OpenXml.Spreadsheet.Worksheet worksheet, uint rowIndex)
        {
            return worksheet.GetFirstChild<SheetData>().
              Elements<Row>().Where(r => r.RowIndex == rowIndex).First();
        } 
    
        // crea una Excel con los datos de la ficha de fabricación
        //public void imprimir_ficha(Object sender, EventArgs e)
        //{
        //    try
        //    {                
        //        Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
        //        Excel.Workbook xlWorkBook;
        //        Excel.Worksheet xlWorkSheet;

        //        xlApp = new Microsoft.Office.Interop.Excel.Application();
        //        xlWorkSheet = new Excel.Worksheet();                

        //        //brand new temporary file
        //        string tempPath = System.IO.Path.GetTempFileName();
        //        //to manage de temp file life
        //        FileInfo tempFile = new FileInfo(tempPath);
        //        //copy the structure and data of the template .xls
        //        System.IO.File.WriteAllBytes(tempPath, Properties.Resources.FichaReferencia_vacia);
        //        xlWorkBook = xlApp.Workbooks.Open(tempPath, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
        //        xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
              

        //        // información principal
        //        xlWorkSheet.Cells[3, 8] = tbReferencia.Text;  //referencia
        //        xlWorkSheet.Cells[4, 8] = tbNombre.Text; //nombre
        //        xlWorkSheet.Cells[5, 8] = tbCliente.Text; //cliente
        //        xlWorkSheet.Cells[6, 8] = tbCodigoMolde.Text; //codigoMolde
        //        xlWorkSheet.Cells[7, 8] = tbMaquina.Text; //maquina
        //        xlWorkSheet.Cells[8, 8] = tbAutomatico.Text; //auto
        //        xlWorkSheet.Cells[9, 8] = tbManual.Text; //manual
        //        xlWorkSheet.Cells[10, 8] = tbPersonal.Text; //personal

        //        xlWorkSheet.Cells[3, 22] = tbProgramaMolde.Text; //programaMolde
        //        xlWorkSheet.Cells[4, 22] = tbProgramaRobot.Text; //programaRobot
        //        xlWorkSheet.Cells[5, 22] = tbManoRobot.Text; //manoRobot
        //        xlWorkSheet.Cells[6, 22] = tbAperturaMaquina.Text; //aperturaMaquina
        //        xlWorkSheet.Cells[7, 22] = tbCavidades.Text; //cavidades
        //        xlWorkSheet.Cells[8, 22] = tbPesoPieza.Text; //pesoPieza
        //        xlWorkSheet.Cells[9, 22] = tbPesoColada.Text; //pesoColada
        //        xlWorkSheet.Cells[10, 22] = tbPesoTotal.Text; //pesoTotal
                

        //        // material
        //        xlWorkSheet.Cells[13, 1] = thCodMaterial.Text;  //codMaterial
        //        xlWorkSheet.Cells[13, 5] = thMaterial.Text; // material
        //        xlWorkSheet.Cells[13, 12] = thCodColorante.Text; //codColorante
        //        xlWorkSheet.Cells[13, 16] = thColorante.Text;  //colorante
        //        xlWorkSheet.Cells[13, 21] = thColor.Text; //color
        //        xlWorkSheet.Cells[13, 25] = thTempSecado.Text; //tempSecado
        //        xlWorkSheet.Cells[13, 29] = thTiempoSecado.Text; //tiempoSecado

        //        // temperaturas husillo
        //        xlWorkSheet.Cells[18, 1] = thBoq.Text; //boquilla                
        //        xlWorkSheet.Cells[18, 3] = thT1.Text;
        //        xlWorkSheet.Cells[18, 4] = thT2.Text;
        //        xlWorkSheet.Cells[18, 5] = thT3.Text;
        //        xlWorkSheet.Cells[18, 6] = thT4.Text;
        //        xlWorkSheet.Cells[24, 1] = tbFijaRefrigeracion.Text; //fijaRefriCircuito
        //        xlWorkSheet.Cells[24, 7] = tbFijaAtempCircuito.Text; // fijaAtempCircuito
        //        xlWorkSheet.Cells[24, 12] = tbAtempTemperatura.Text; //fijaAtempTemp

        //        // temperaturas cámara caliente
        //        xlWorkSheet.Cells[18, 16] = thZ1.Text;
        //        xlWorkSheet.Cells[18, 17] = thZ2.Text;
        //        xlWorkSheet.Cells[18, 18] = thZ3.Text;
        //        xlWorkSheet.Cells[18, 20] = thZ4.Text;
        //        xlWorkSheet.Cells[18, 21] = thZ5.Text;
        //        xlWorkSheet.Cells[18, 23] = thZ6.Text;
        //        xlWorkSheet.Cells[18, 25] = thZ7.Text;
        //        xlWorkSheet.Cells[18, 27] = thZ8.Text;
        //        xlWorkSheet.Cells[18, 29] = thZ9.Text;
        //        xlWorkSheet.Cells[18, 31] = thZ10.Text;
        //        xlWorkSheet.Cells[20, 16] = thZ11.Text;
        //        xlWorkSheet.Cells[20, 17] = thZ12.Text;
        //        xlWorkSheet.Cells[20, 18] = thZ13.Text;
        //        xlWorkSheet.Cells[20, 20] = thZ14.Text;
        //        xlWorkSheet.Cells[20, 21] = thZ15.Text;
        //        xlWorkSheet.Cells[20, 23] = thZ16.Text;
        //        xlWorkSheet.Cells[20, 25] = thZ17.Text;
        //        xlWorkSheet.Cells[20, 27] = thZ18.Text;
        //        xlWorkSheet.Cells[20, 29] = thZ19.Text;
        //        xlWorkSheet.Cells[20, 31] = thZ20.Text;
        //        xlWorkSheet.Cells[24, 16] = tbMovilRefrigeracion.Text;
        //        xlWorkSheet.Cells[24, 21] = tbMovilAtempCircuito.Text;
        //        xlWorkSheet.Cells[24, 26] = tbMovilAtempTemperatura.Text;

        //        // inyección 
        //        xlWorkSheet.Cells[27, 6] = thV1.Text;
        //        xlWorkSheet.Cells[28, 6] = thC1.Text;
        //        xlWorkSheet.Cells[27, 8] = thV2.Text;
        //        xlWorkSheet.Cells[28, 8] = thC2.Text;
        //        xlWorkSheet.Cells[27, 9] = thV3.Text;
        //        xlWorkSheet.Cells[28, 9] = thC3.Text;
        //        xlWorkSheet.Cells[27, 10] = thV4.Text;
        //        xlWorkSheet.Cells[28, 10] = thC4.Text;
        //        xlWorkSheet.Cells[27, 12] = thV5.Text;
        //        xlWorkSheet.Cells[28, 12] = thC5.Text;
        //        xlWorkSheet.Cells[27, 13] = thV6.Text;
        //        xlWorkSheet.Cells[28, 13] = thC6.Text;
        //        xlWorkSheet.Cells[27, 14] = thV7.Text;
        //        xlWorkSheet.Cells[28, 14] = thC7.Text;
        //        xlWorkSheet.Cells[27, 16] = thV8.Text;
        //        xlWorkSheet.Cells[28, 16] = thC8.Text;
        //        xlWorkSheet.Cells[27, 17] = thV9.Text;
        //        xlWorkSheet.Cells[28, 17] = thC9.Text;
        //        xlWorkSheet.Cells[27, 18] = thV10.Text;
        //        xlWorkSheet.Cells[28, 18] = thC10.Text;
        //        xlWorkSheet.Cells[27, 20] = thV11.Text;
        //        xlWorkSheet.Cells[28, 20] = thC11.Text;
        //        xlWorkSheet.Cells[27, 21] = thV12.Text;
        //        xlWorkSheet.Cells[28, 21] = thC12.Text;
        //        xlWorkSheet.Cells[27, 23] = tbTiempoInyeccion.Text;
        //        xlWorkSheet.Cells[27, 29] = tbLimitePresion.Text;

        //        // inyección secuencial

        //        //xlWorkSheet.Cells[32, 5].value());
        //        //xlWorkSheet.Cells[32, 6].value());
        //        //xlWorkSheet.Cells[32, 7].value());
        //        //xlWorkSheet.Cells[32, 8].value());
        //        //xlWorkSheet.Cells[32, 9].value());
        //        //xlWorkSheet.Cells[32, 10].value());
        //        //xlWorkSheet.Cells[32, 11].value());
        //        //xlWorkSheet.Cells[32, 12].value());
        //        //xlWorkSheet.Cells[32, 13].value());
        //        //xlWorkSheet.Cells[32, 14].value());

        //        //xlWorkSheet.Cells[33, 5].value());
        //        //xlWorkSheet.Cells[33, 6].value());
        //        //xlWorkSheet.Cells[33, 7].value());
        //        //xlWorkSheet.Cells[33, 8].value());
        //        //xlWorkSheet.Cells[33, 9].value());
        //        //xlWorkSheet.Cells[33, 10].value());
        //        //xlWorkSheet.Cells[33, 11].value());
        //        //xlWorkSheet.Cells[33, 12].value());
        //        //xlWorkSheet.Cells[33, 13].value());
        //        //xlWorkSheet.Cells[33, 14].value());

        //        //xlWorkSheet.Cells[34, 5].value());
        //        //xlWorkSheet.Cells[34, 6].value());
        //        //xlWorkSheet.Cells[34, 7].value());
        //        //xlWorkSheet.Cells[34, 8].value());
        //        //xlWorkSheet.Cells[34, 9].value());
        //        //xlWorkSheet.Cells[34, 10].value());
        //        //xlWorkSheet.Cells[34, 11].value());
        //        //xlWorkSheet.Cells[34, 12].value());
        //        //xlWorkSheet.Cells[34, 13].value());
        //        //xlWorkSheet.Cells[34, 14].value());

        //        //xlWorkSheet.Cells[35, 5].value());
        //        //xlWorkSheet.Cells[35, 6].value());
        //        //xlWorkSheet.Cells[35, 7].value());
        //        //xlWorkSheet.Cells[35, 8].value());
        //        //xlWorkSheet.Cells[35, 9].value());
        //        //xlWorkSheet.Cells[35, 10].value());
        //        //xlWorkSheet.Cells[35, 11].value());
        //        //xlWorkSheet.Cells[35, 12].value());
        //        //xlWorkSheet.Cells[35, 13].value());
        //        //xlWorkSheet.Cells[35, 14].value());

        //        //xlWorkSheet.Cells[36, 5].value());
        //        //xlWorkSheet.Cells[36, 6].value());
        //        //xlWorkSheet.Cells[36, 7].value());
        //        //xlWorkSheet.Cells[36, 8].value());
        //        //xlWorkSheet.Cells[36, 9].value());
        //        //xlWorkSheet.Cells[36, 10].value());
        //        //xlWorkSheet.Cells[36, 11].value());
        //        //xlWorkSheet.Cells[36, 12].value());
        //        //xlWorkSheet.Cells[36, 13].value());
        //        //xlWorkSheet.Cells[36, 14].value());

        //        //xlWorkSheet.Cells[32, 22].value());
        //        //xlWorkSheet.Cells[32, 23].value());
        //        //xlWorkSheet.Cells[32, 24].value());
        //        //xlWorkSheet.Cells[32, 25].value());
        //        //xlWorkSheet.Cells[32, 26].value());
        //        //xlWorkSheet.Cells[32, 27].value());
        //        //xlWorkSheet.Cells[32, 28].value());
        //        //xlWorkSheet.Cells[32, 29].value());
        //        //xlWorkSheet.Cells[32, 30].value());
        //        //xlWorkSheet.Cells[32, 31].value());
        //        // postpresion

        //        xlWorkSheet.Cells[40, 8] = thP1.Text;
        //        xlWorkSheet.Cells[41, 8] = thTP1.Text;
        //        xlWorkSheet.Cells[40, 9] = thP2.Text;
        //        xlWorkSheet.Cells[41, 9] = thTP2.Text;
        //        xlWorkSheet.Cells[40, 10] = thP3.Text;
        //        xlWorkSheet.Cells[41, 10] = thTP3.Text;
        //        xlWorkSheet.Cells[40, 12] = thP4.Text;
        //        xlWorkSheet.Cells[41, 12] = thTP4.Text;
        //        xlWorkSheet.Cells[40, 13] = thP5.Text;
        //        xlWorkSheet.Cells[41, 13] = thTP5.Text;
        //        xlWorkSheet.Cells[40, 14] = thP6.Text;
        //        xlWorkSheet.Cells[41, 14] = thTP6.Text;
        //        xlWorkSheet.Cells[40, 16] = thP7.Text;
        //        xlWorkSheet.Cells[41, 16] = thTP7.Text;
        //        xlWorkSheet.Cells[40, 17] = thP8.Text;
        //        xlWorkSheet.Cells[41, 17] = thTP8.Text;
        //        xlWorkSheet.Cells[40, 18] = thP9.Text;
        //        xlWorkSheet.Cells[41, 18] = thTP9.Text;
        //        xlWorkSheet.Cells[40, 20] = thP10.Text;
        //        xlWorkSheet.Cells[41, 20] = thTP10.Text;
        //        xlWorkSheet.Cells[40, 27] = tbConmutacion.Text;
        //        xlWorkSheet.Cells[40, 1] = tbTiempoPresion.Text;

        //        // dosificación

        //         xlWorkSheet.Cells[44, 1] = thVCarga.Text; // velocidadCarga
        //         xlWorkSheet.Cells[44, 5] = thCarga.Text; // carga
        //         xlWorkSheet.Cells[44, 9] = thDescomp.Text; // descompresion
        //         xlWorkSheet.Cells[44, 13] = thContrapr.Text; // contrapresion
        //         xlWorkSheet.Cells[44, 17] = thTiempo.Text; // tiempoDosificacion
        //         xlWorkSheet.Cells[44, 21] = thEnfriamiento.Text; // tiempoEnfriamiento
        //         xlWorkSheet.Cells[44, 27] = thCiclo.Text; // tiempoCiclo


        //        // marcad con cruz
        //         if (cbNoyos.Checked) 
        //             xlWorkSheet.Cells[47, 11] = "X"; //noyos
        //         if (cbHembra.Checked)
        //             xlWorkSheet.Cells[48, 11] = "X"; //hembra
        //         if (cbMacho.Checked)
        //             xlWorkSheet.Cells[49, 11] = "X"; //macho
        //         if (cbAntesExpul.Checked)
        //             xlWorkSheet.Cells[50, 11] = "X"; //antesExpuls
        //         if (cbAntesApert.Checked)
        //             xlWorkSheet.Cells[51, 11] = "X"; //antesApert
        //         if (cbAntesCierre.Checked)
        //             xlWorkSheet.Cells[52, 11] = "X"; //antesCierre
        //         if (cbDespuesCierre.Checked)
        //             xlWorkSheet.Cells[53, 11] = "X"; //despuesCierre
        //         if (cbOtros1.Checked)
        //             xlWorkSheet.Cells[54, 11] = "X"; //otros
        //         if (cbBoquilla.Checked)
        //             xlWorkSheet.Cells[47, 20] = "X"; //boquillaCruz
        //         if (cbCono.Checked)
        //             xlWorkSheet.Cells[48, 20] = "X"; //cono
        //         if (cbRadioLarga.Checked)
        //             xlWorkSheet.Cells[49, 20] = "X"; //radioLarga
        //         if (cbLibre.Checked)
        //             xlWorkSheet.Cells[50, 20] = "X"; //libre 
        //         if (cbValvula.Checked)
        //             xlWorkSheet.Cells[51, 20] = "X"; //valvula
        //         if (cbResistencia.Checked)
        //             xlWorkSheet.Cells[52, 20] = "X"; //resistencia
        //         if (cbOtros2.Checked)
        //             xlWorkSheet.Cells[53, 20] = "X"; //otros2
        //         if (cbExpulsion.Checked)
        //             xlWorkSheet.Cells[47, 30] = "X"; //expulsion
        //         if (cbHidraulica.Checked)
        //             xlWorkSheet.Cells[48, 30] = "X"; //hidraulica
        //         if (cbNeumatica.Checked)
        //             xlWorkSheet.Cells[49, 30] = "X"; //neumatica
        //         if (cbNormal.Checked)
        //             xlWorkSheet.Cells[50, 30] = "X"; //normal
        //         if (cbArandela125.Checked)
        //             xlWorkSheet.Cells[51, 30] = "X"; //arandela125
        //         if (cbArandela160.Checked)
        //             xlWorkSheet.Cells[52, 30] = "X"; //arandela160
        //         if (cbArandela200.Checked)
        //             xlWorkSheet.Cells[53, 30] = "X"; //arandela200 
        //         if (cbArandela250.Checked)
        //             xlWorkSheet.Cells[54, 30] = "X"; //arandela250

        //        tempFile.Delete();
        //        string path = @"\\FACTS4-SRV\\Fichas parametros\\Ficha_" + tbReferencia.Text + "_" + tbMaquina.Text + ".xls";
        //        xlWorkBook.SaveAs(path);
               
        //        xlWorkBook.Close(false, null, null);
        //        xlApp.Quit();

        //        Marshal.ReleaseComObject(xlWorkSheet);
        //        Marshal.ReleaseComObject(xlWorkBook);
        //        Marshal.ReleaseComObject(xlApp);

        //        // Limpiamos la salida
        //        //Response.Clear();
        //        //// Con esto le decimos al browser que la salida sera descargable
        //        //Response.ContentType = "application/octet-stream";
        //        //// esta linea es opcional, en donde podemos cambiar el nombre del fichero a descargar (para que sea diferente al original)
        //        //Response.AddHeader("Content-Disposition", "attachment; filename=Ficha_" + tbReferencia.Text + "_" + tbMaquina.Text + ".xls");
        //        //// Escribimos el fichero a enviar 
        //        //Response.WriteFile(@"\\FACTS4-SRV\Fichas parametros\Ficha_" + tbReferencia.Text + "_" + tbMaquina.Text + ".xls");
        //        //// volcamos el stream 
        //        //Response.Flush();
        //        //// Enviamos todo el encabezado ahora
        //        //Response.End();
        //        //System.Diagnostics.Process.Start(@"\\FACTS4-SRV\Fichas parametros\Ficha_" + tbReferencia.Text + "_" + tbMaquina.Text + ".xls");                             
                    
        //    }
        //    catch (Exception)
        //    {
        //        mandar_mail(ex.Message.ToString());
        //    }
        //}

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

        // ir a modificar ficha
        public void Modificar_ficha(Object sender, EventArgs e)
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
                    btnGuardar.Visible = true;
                    btnCancelar.Visible = true;
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
                btnModificar.Visible = true;
                btnGuardar.Visible = false;
                btnCancelar.Visible = false;
                CargarFicha_function();
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

                tbReferencia.Enabled = true;
                tbNombre.Enabled = true;
                tbCliente.Enabled = true;
                tbCodigoMolde.Enabled = true;
                tbMaquina.Enabled = true;
                tbAutomatico.Enabled = true;
                tbManual.Enabled = true;
                tbPersonal.Enabled = true;
                tbProgramaMolde.Enabled = true;
                tbProgramaRobot.Enabled = true;
                //tbManoRobot.Enabled = true;
                tbAperturaMaquina.Enabled = true;
                tbCavidades.Enabled = true;
                tbPesoPieza.Enabled = true;
                tbPesoColada.Enabled = true;
                tbPesoTotal.Enabled = true;
                thVCarga.Enabled = true;
                thCarga.Enabled = true;
                thDescomp.Enabled = true;
                thContrapr.Enabled = true;
                thTiempo.Enabled = true;
                thEnfriamiento.Enabled = true;
                thCiclo.Enabled = true;
                thCojin.Enabled = true;
                cbEdicion.Enabled = false;
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
                thT1.Enabled = true;
                thT2.Enabled = true;
                thT3.Enabled = true;
                thT4.Enabled = true;
                thT5.Enabled = true;
                thT6.Enabled = true;
                thT7.Enabled = true;
                thT8.Enabled = true;
                thT9.Enabled = true;
                thT10.Enabled = true;
                //tbFijaRefrigeracion.Enabled = true;
                //tbFijaAtempCircuito.Enabled = true;
                //tbAtempTemperatura.Enabled = true;
               
                thZ1.Enabled = true;
                thZ2.Enabled = true;
                thZ3.Enabled = true;
                thZ4.Enabled = true;
                thZ5.Enabled = true;
                thZ6.Enabled = true;
                thZ7.Enabled = true;
                thZ8.Enabled = true;
                thZ9.Enabled = true;
                thZ10.Enabled = true;
                thZ11.Enabled = true;
                thZ12.Enabled = true;
                thZ13.Enabled = true;
                thZ14.Enabled = true;
                thZ15.Enabled = true;
                thZ16.Enabled = true;
                thZ17.Enabled = true;
                thZ18.Enabled = true;
                thZ19.Enabled = true;
                thZ20.Enabled = true;
                //tbMovilRefrigeracion.Enabled = true;
                //tbMovilAtempCircuito.Enabled = true;
                //tbMovilAtempTemperatura.Enabled = true;

                thV1.Enabled = true;
                thV2.Enabled = true;
                thV3.Enabled = true;
                thV4.Enabled = true;
                thV5.Enabled = true;
                thV6.Enabled = true;
                thV7.Enabled = true;
                thV8.Enabled = true;
                thV9.Enabled = true;
                thV10.Enabled = true;
                thV11.Enabled = true;
                thV12.Enabled = true;
                thC1.Enabled = true;
                thC2.Enabled = true;
                thC3.Enabled = true;
                thC4.Enabled = true;
                thC5.Enabled = true;
                thC6.Enabled = true;
                thC7.Enabled = true;
                thC8.Enabled = true;
                thC9.Enabled = true;
                thC10.Enabled = true;
                thC11.Enabled = true;
                thC12.Enabled = true;
                tbTiempoInyeccion.Enabled = true;
                tbLimitePresion.Enabled = true;
                tbTiempoInyeccionN.Enabled = false;
                tbTiempoInyeccionNVal.Enabled = true;
                tbTiempoInyeccionM.Enabled = false;
                tbTiempoInyeccionMVal.Enabled = true;
                tbLimitePresionN.Enabled = false;
                tbLimitePresionNVal.Enabled = true;
                tbLimitePresionM.Enabled = false;
                tbLimitePresionMval.Enabled = true;

                thP1.Enabled = true;
                thP2.Enabled = true;
                thP3.Enabled = true;
                thP4.Enabled = true;
                thP5.Enabled = true;
                thP6.Enabled = true;
                thP7.Enabled = true;
                thP8.Enabled = true;
                thP9.Enabled = true;
                thP10.Enabled = true;
                thTP1.Enabled = true;
                thTP2.Enabled = true;
                thTP3.Enabled = true;
                thTP4.Enabled = true;
                thTP5.Enabled = true;
                thTP6.Enabled = true;
                thTP7.Enabled = true;
                thTP8.Enabled = true;
                thTP9.Enabled = true;
                thTP10.Enabled = true;
                tbConmutacion.Enabled = true;
                tbTiempoPresion.Enabled = true;

                thConmuntaciontolN.Enabled = false;
                thConmuntaciontolNVal.Enabled = true;
                thConmuntaciontolM.Enabled = false;
                thConmuntaciontolMVal.Enabled = true;
                tbTiempoPresiontolN.Enabled = false;
                tbTiempoPresiontolNVal.Enabled = true;
                tbTiempoPresiontolM.Enabled = false;
                tbTiempoPresiontolMVal.Enabled = true;
                
                seqAbrir1_1.Enabled = true;
                seqAbrir1_2.Enabled = true;
                seqAbrir1_3.Enabled = true;
                seqAbrir1_4.Enabled = true;
                seqAbrir1_5.Enabled = true;
                seqAbrir1_6.Enabled = true;
                seqAbrir1_7.Enabled = true;
                seqAbrir1_8.Enabled = true;
                seqAbrir1_9.Enabled = true;
                seqAbrir1_10.Enabled = true;
                seqCerrar1_1.Enabled = true;
                seqCerrar1_2.Enabled = true;
                seqCerrar1_3.Enabled = true;
                seqCerrar1_4.Enabled = true;
                seqCerrar1_5.Enabled = true;
                seqCerrar1_6.Enabled = true;
                seqCerrar1_7.Enabled = true;
                seqCerrar1_8.Enabled = true;
                seqCerrar1_9.Enabled = true;
                seqCerrar1_10.Enabled = true;
                seqAbrir2_1.Enabled = true;
                seqAbrir2_2.Enabled = true;
                seqAbrir2_3.Enabled = true;
                seqAbrir2_4.Enabled = true;
                seqAbrir2_5.Enabled = true;
                seqAbrir2_6.Enabled = true;
                seqAbrir2_7.Enabled = true;
                seqAbrir2_8.Enabled = true;
                seqAbrir2_9.Enabled = true;
                seqAbrir2_10.Enabled = true;
                seqCerrar2_1.Enabled = true;
                seqCerrar2_2.Enabled = true;
                seqCerrar2_3.Enabled = true;
                seqCerrar2_4.Enabled = true;
                seqCerrar2_5.Enabled = true;
                seqCerrar2_6.Enabled = true;
                seqCerrar2_7.Enabled = true;
                seqCerrar2_8.Enabled = true;
                seqCerrar2_9.Enabled = true;
                seqCerrar2_10.Enabled = true;
                seqTPresPost1.Enabled = true;
                seqTPresPost2.Enabled = true;
                seqTPresPost3.Enabled = true;
                seqTPresPost4.Enabled = true;
                seqTPresPost5.Enabled = true;
                seqTPresPost6.Enabled = true;
                seqTPresPost7.Enabled = true;
                seqTPresPost8.Enabled = true;
                seqTPresPost9.Enabled = true;
                seqTPresPost10.Enabled = true;

                seqTiempoRetardo_1.Enabled = true;
                seqTiempoRetardo_2.Enabled = true;
                seqTiempoRetardo_3.Enabled = true;
                seqTiempoRetardo_4.Enabled = true;
                seqTiempoRetardo_5.Enabled = true;
                seqTiempoRetardo_6.Enabled = true;
                seqTiempoRetardo_7.Enabled = true;
                seqTiempoRetardo_8.Enabled = true;
                seqTiempoRetardo_9.Enabled = true;
                seqTiempoRetardo_10.Enabled = true;
                seqAnotaciones.Enabled = true;



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
                TbCircuitoF1.Enabled = true;
                TbCircuitoF2.Enabled = true;
                TbCircuitoF3.Enabled = true;
                TbCircuitoF4.Enabled = true;
                TbCircuitoF5.Enabled = true;
                TbCircuitoF6.Enabled = true;
                TbCaudalF1.Enabled = true;
                TbCaudalF2.Enabled = true;
                TbCaudalF3.Enabled = true;
                TbCaudalF4.Enabled = true;
                TbCaudalF5.Enabled = true;
                TbCaudalF6.Enabled = true;
                TbTemperaturaF1.Enabled = true;
                TbTemperaturaF2.Enabled = true;
                TbTemperaturaF3.Enabled = true;
                TbTemperaturaF4.Enabled = true;
                TbTemperaturaF5.Enabled = true;
                TbTemperaturaF6.Enabled = true;
                TbEntradaF1.Enabled = true;
                TbEntradaF2.Enabled = true;
                TbEntradaF3.Enabled = true;
                TbEntradaF4.Enabled = true;
                TbEntradaF5.Enabled = true;
                TbEntradaF6.Enabled = true;
                AtempTipoM.Enabled = true;
                TbCircuitoM1.Enabled = true;
                TbCircuitoM2.Enabled = true;
                TbCircuitoM3.Enabled = true;
                TbCircuitoM4.Enabled = true;
                TbCircuitoM5.Enabled = true;
                TbCircuitoM6.Enabled = true;
                TbCaudalM1.Enabled = true;
                TbCaudalM2.Enabled = true;
                TbCaudalM3.Enabled = true;
                TbCaudalM4.Enabled = true;
                TbCaudalM5.Enabled = true;
                TbCaudalM6.Enabled = true;
                TbTemperaturaM1.Enabled = true;
                TbTemperaturaM2.Enabled = true;
                TbTemperaturaM3.Enabled = true;
                TbTemperaturaM4.Enabled = true;
                TbTemperaturaM5.Enabled = true;
                TbTemperaturaM6.Enabled = true;
                TbEntradaM1.Enabled = true;
                TbEntradaM2.Enabled = true;
                TbEntradaM3.Enabled = true;
                TbEntradaM4.Enabled = true;
                TbEntradaM5.Enabled = true;
                TbEntradaM6.Enabled = true;


                TMvcarga.Enabled = false;
                TMvcargaval.Enabled = true;
                TNvcarga.Enabled = false;
                TNvcargaval.Enabled = true;
                TMcarga.Enabled = false;
                TMcargaval.Enabled = true;
                TNcarga.Enabled = false;
                TNcargaval.Enabled = true;
                TMdescom.Enabled = false;
                TMdescomval.Enabled = true;
                TNdescom.Enabled = false;
                TNdescomval.Enabled = true;
                TMcontrap.Enabled = false;
                TMcontrapval.Enabled = true;
                TNcontrap.Enabled = false;
                TNcontrapval.Enabled = true;
                TMTiempdos.Enabled = false;
                TMTiempdosval.Enabled = true;
                TNTiempdos.Enabled = false;
                TNTiempdosval.Enabled = true;
                TMEnfriam.Enabled = false;
                TMEnfriamval.Enabled = true;
                TNEnfriam.Enabled = false;
                TNEnfriamval.Enabled = true;
                TMCiclo.Enabled = false;
                TMCicloval.Enabled = true;
                TNCiclo.Enabled = false;
                TNCicloval.Enabled = true;
                TNCojin.Enabled = false;
                TMCojin.Enabled = false;
                TMCojinval.Enabled = true;
                TNCojinval.Enabled = true;

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
            catch(Exception)
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
                tbNombre.Enabled = false;
                tbCliente.Enabled = false;
                tbCodigoMolde.Enabled = false;
                tbMaquina.Enabled = false;
                tbAutomatico.Enabled = false;
                tbManual.Enabled = false;
                tbPersonal.Enabled = false;
                tbProgramaMolde.Enabled = false;
                tbProgramaRobot.Enabled = false;
                tbManoRobot.Enabled = false;
                tbAperturaMaquina.Enabled = false;
                tbCavidades.Enabled = false;
                tbPesoPieza.Enabled = false;
                tbPesoColada.Enabled = false;
                tbPesoTotal.Enabled = false;
                thVCarga.Enabled = false;
                thCarga.Enabled = false;
                thDescomp.Enabled = false;
                thContrapr.Enabled = false;
                thTiempo.Enabled = false;
                thEnfriamiento.Enabled = false;
                thCiclo.Enabled = false;
                thCojin.Enabled = false;
                cbEdicion.Enabled = false;
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
                thT1.Enabled = false;
                thT2.Enabled = false;
                thT3.Enabled = false;
                thT4.Enabled = false;
                thT5.Enabled = false;
                thT6.Enabled = false;
                thT7.Enabled = false;
                thT8.Enabled = false;
                thT9.Enabled = false;
                thT10.Enabled = false;
                //tbFijaRefrigeracion.Enabled = false;
                //tbFijaAtempCircuito.Enabled = false;
                //tbAtempTemperatura.Enabled = false;
               
                thZ1.Enabled = false;
                thZ2.Enabled = false;
                thZ3.Enabled = false;
                thZ4.Enabled = false;
                thZ5.Enabled = false;
                thZ6.Enabled = false;
                thZ7.Enabled = false;
                thZ8.Enabled = false;
                thZ9.Enabled = false;
                thZ10.Enabled = false;
                thZ11.Enabled = false;
                thZ12.Enabled = false;
                thZ13.Enabled = false;
                thZ14.Enabled = false;
                thZ15.Enabled = false;
                thZ16.Enabled = false;
                thZ17.Enabled = false;
                thZ18.Enabled = false;
                thZ19.Enabled = false;
                thZ20.Enabled = false;
                //tbMovilRefrigeracion.Enabled = false;
                //tbMovilAtempCircuito.Enabled = false;
                //tbMovilAtempTemperatura.Enabled = false;

                thV1.Enabled = false;
                thV2.Enabled = false;
                thV3.Enabled = false;
                thV4.Enabled = false;
                thV5.Enabled = false;
                thV6.Enabled = false;
                thV7.Enabled = false;
                thV8.Enabled = false;
                thV9.Enabled = false;
                thV10.Enabled = false;
                thV11.Enabled = false;
                thV12.Enabled = false;
                thC1.Enabled = false;
                thC2.Enabled = false;
                thC3.Enabled = false;
                thC4.Enabled = false;
                thC5.Enabled = false;
                thC6.Enabled = false;
                thC7.Enabled = false;
                thC8.Enabled = false;
                thC9.Enabled = false;
                thC10.Enabled = false;
                thC11.Enabled = false;
                thC12.Enabled = false;
                tbTiempoInyeccion.Enabled = false;
                tbLimitePresion.Enabled = false;
                tbTiempoInyeccionN.Enabled = false;
                tbTiempoInyeccionNVal.Enabled = false;
                tbTiempoInyeccionM.Enabled = false;
                tbTiempoInyeccionMVal.Enabled = false;
                tbLimitePresionN.Enabled = false;
                tbLimitePresionNVal.Enabled = false;
                tbLimitePresionM.Enabled = false;
                tbLimitePresionMval.Enabled = false;

                thP1.Enabled = false;
                thP2.Enabled = false;
                thP3.Enabled = false;
                thP4.Enabled = false;
                thP5.Enabled = false;
                thP6.Enabled = false;
                thP7.Enabled = false;
                thP8.Enabled = false;
                thP9.Enabled = false;
                thP10.Enabled = false;
                thTP1.Enabled = false;
                thTP2.Enabled = false;
                thTP3.Enabled = false;
                thTP4.Enabled = false;
                thTP5.Enabled = false;
                thTP6.Enabled = false;
                thTP7.Enabled = false;
                thTP8.Enabled = false;
                thTP9.Enabled = false;
                thTP10.Enabled = false;
                tbConmutacion.Enabled = false;
                tbTiempoPresion.Enabled = false;

                thConmuntaciontolN.Enabled = false;
                thConmuntaciontolNVal.Enabled = false;
                thConmuntaciontolM.Enabled = false;
                thConmuntaciontolMVal.Enabled = false;
                tbTiempoPresiontolN.Enabled = false;
                tbTiempoPresiontolNVal.Enabled = false;
                tbTiempoPresiontolM.Enabled = false;
                tbTiempoPresiontolMVal.Enabled = false;

                //secuencialescontrol
                seqAbrir1_1.Enabled = false;
                seqAbrir1_2.Enabled = false;
                seqAbrir1_3.Enabled = false;
                seqAbrir1_4.Enabled = false;
                seqAbrir1_5.Enabled = false;
                seqAbrir1_6.Enabled = false;
                seqAbrir1_7.Enabled = false;
                seqAbrir1_8.Enabled = false;
                seqAbrir1_9.Enabled = false;
                seqAbrir1_10.Enabled = false;
                seqCerrar1_1.Enabled = false;
                seqCerrar1_2.Enabled = false;
                seqCerrar1_3.Enabled = false;
                seqCerrar1_4.Enabled = false;
                seqCerrar1_5.Enabled = false;
                seqCerrar1_6.Enabled = false;
                seqCerrar1_7.Enabled = false;
                seqCerrar1_8.Enabled = false;
                seqCerrar1_9.Enabled = false;
                seqCerrar1_10.Enabled = false;
                seqAbrir2_1.Enabled = false;
                seqAbrir2_2.Enabled = false;
                seqAbrir2_3.Enabled = false;
                seqAbrir2_4.Enabled = false;
                seqAbrir2_5.Enabled = false;
                seqAbrir2_6.Enabled = false;
                seqAbrir2_7.Enabled = false;
                seqAbrir2_8.Enabled = false;
                seqAbrir2_9.Enabled = false;
                seqAbrir2_10.Enabled = false;
                seqCerrar2_1.Enabled = false;
                seqCerrar2_2.Enabled = false;
                seqCerrar2_3.Enabled = false;
                seqCerrar2_4.Enabled = false;
                seqCerrar2_5.Enabled = false;
                seqCerrar2_6.Enabled = false;
                seqCerrar2_7.Enabled = false;
                seqCerrar2_8.Enabled = false;
                seqCerrar2_9.Enabled = false;
                seqCerrar2_10.Enabled = false;
                seqTPresPost1.Enabled = false;
                seqTPresPost2.Enabled = false;
                seqTPresPost3.Enabled = false;
                seqTPresPost4.Enabled = false;
                seqTPresPost5.Enabled = false;
                seqTPresPost6.Enabled = false;
                seqTPresPost7.Enabled = false;
                seqTPresPost8.Enabled = false;
                seqTPresPost9.Enabled = false;
                seqTPresPost10.Enabled = false;

                seqTiempoRetardo_1.Enabled = false;
                seqTiempoRetardo_2.Enabled = false;
                seqTiempoRetardo_3.Enabled = false;
                seqTiempoRetardo_4.Enabled = false;
                seqTiempoRetardo_5.Enabled = false;
                seqTiempoRetardo_6.Enabled = false;
                seqTiempoRetardo_7.Enabled = false;
                seqTiempoRetardo_8.Enabled = false;
                seqTiempoRetardo_9.Enabled = false;
                seqTiempoRetardo_10.Enabled = false;
                seqAnotaciones.Enabled = false;
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
                AtempTipoF.Enabled = false;
                TbCircuitoF1.Enabled = false;
                TbCircuitoF2.Enabled = false;
                TbCircuitoF3.Enabled = false;
                TbCircuitoF4.Enabled = false;
                TbCircuitoF5.Enabled = false;
                TbCircuitoF6.Enabled = false;
                TbCaudalF1.Enabled = false;
                TbCaudalF2.Enabled = false;
                TbCaudalF3.Enabled = false;
                TbCaudalF4.Enabled = false;
                TbCaudalF5.Enabled = false;
                TbCaudalF6.Enabled = false;
                TbTemperaturaF1.Enabled = false;
                TbTemperaturaF2.Enabled = false;
                TbTemperaturaF3.Enabled = false;
                TbTemperaturaF4.Enabled = false;
                TbTemperaturaF5.Enabled = false;
                TbTemperaturaF6.Enabled = false;
                TbEntradaF1.Enabled = false;
                TbEntradaF2.Enabled = false;
                TbEntradaF3.Enabled = false;
                TbEntradaF4.Enabled = false;
                TbEntradaF5.Enabled = false;
                TbEntradaF6.Enabled = false;
                AtempTipoM.Enabled = false;
                TbCircuitoM1.Enabled = false;
                TbCircuitoM2.Enabled = false;
                TbCircuitoM3.Enabled = false;
                TbCircuitoM4.Enabled = false;
                TbCircuitoM5.Enabled = false;
                TbCircuitoM6.Enabled = false;
                TbCaudalM1.Enabled = false;
                TbCaudalM2.Enabled = false;
                TbCaudalM3.Enabled = false;
                TbCaudalM4.Enabled = false;
                TbCaudalM5.Enabled = false;
                TbCaudalM6.Enabled = false;
                TbTemperaturaM1.Enabled = false;
                TbTemperaturaM2.Enabled = false;
                TbTemperaturaM3.Enabled = false;
                TbTemperaturaM4.Enabled = false;
                TbTemperaturaM5.Enabled = false;
                TbTemperaturaM6.Enabled = false;
                TbEntradaM1.Enabled = false;
                TbEntradaM2.Enabled = false;
                TbEntradaM3.Enabled = false;
                TbEntradaM4.Enabled = false;
                TbEntradaM5.Enabled = false;
                TbEntradaM6.Enabled = false;
                
                TMvcarga.Enabled = false;
                TMvcargaval.Enabled = false;
                TNvcarga.Enabled = false;
                TNvcargaval.Enabled = false;
                TMcarga.Enabled = false;
                TMcargaval.Enabled = false;
                TNcarga.Enabled = false;
                TNcargaval.Enabled = false;
                TMdescom.Enabled = false;
                TMdescomval.Enabled = false;
                TNdescom.Enabled = false;
                TNdescomval.Enabled = false;
                TMcontrap.Enabled = false;
                TMcontrapval.Enabled = false;
                TNcontrap.Enabled = false;
                TNcontrapval.Enabled = false;
                TMTiempdos.Enabled = false;
                TMTiempdosval.Enabled = false;
                TNTiempdos.Enabled = false;
                TNTiempdosval.Enabled = false;
                TMEnfriam.Enabled = false;
                TMEnfriamval.Enabled = false;
                TNEnfriam.Enabled = false;
                TNEnfriamval.Enabled = false;
                TMCiclo.Enabled = false;
                TMCicloval.Enabled = false;
                TNCiclo.Enabled = false;
                TNCicloval.Enabled = false;
                TNCojin.Enabled = false;
                TMCojin.Enabled = false;
                TMCojinval.Enabled = false;
                TNCojinval.Enabled = false;

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
            }
            catch(Exception)
            {
            }
        }

        public void Mandar_mail(string mensaje)
        {
            //Create MailMessage
            MailMessage email = new MailMessage();
            email.To.Add(new MailAddress("pedro@thermolympic.es"));
            email.From = new MailAddress("pedro@thermolympic.es");
            email.Subject = "Error importación QMaster (" + DateTime.Now.ToString("dd/MMM/yyy hh:mm:ss") + ")";
            email.Body = mensaje;
            email.IsBodyHtml = true;
            email.Priority = MailPriority.Normal;

            //Definir objeto SmtpClient
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.thermolympic.es";
            smtp.Port = 25;
            smtp.EnableSsl = false;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("r.quilez@thermolympic.es", "010477Rq");

            //Enviar email
            try
            {
                smtp.Send(email);
                email.Dispose();
            }
            catch (Exception)
            {
            }
        }

        public void Cargar_estructura(int referencia)
        {
            try
            {
                Conexion conexion = new Conexion();
                DataSet ds = conexion.cargar_datos_estructura(referencia);
                Estructura.Controls.Clear();
                var tr1E = new HtmlTableRow();
                var th1E = new HtmlTableCell("th");
                th1E.InnerText = "Material";
                var th2E = new HtmlTableCell("th");
                th2E.InnerText = "Descripción";
                var th3E = new HtmlTableCell("th");
                th3E.InnerText = "Ubicación";
                var th4E = new HtmlTableCell("th");
                th4E.InnerText = "Secado";
                var th5E = new HtmlTableCell("th");
                th5E.InnerText = "Notas";
                //var th6E = new HtmlTableCell("th");
                //th6E.InnerText = "Notas3";
                //var th7E = new HtmlTableCell("th");
                //th7E.InnerText = "Receta";
                tr1E.Controls.Add(th1E);
                tr1E.Controls.Add(th2E);
                tr1E.Controls.Add(th3E);
                tr1E.Controls.Add(th4E);
                tr1E.Controls.Add(th5E);
                //tr1E.Controls.Add(th6E);
                //tr1E.Controls.Add(th7E);
                Estructura.Controls.Add(tr1E);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var tr2E = new HtmlTableRow();
                    var td1E = new HtmlTableCell("td");
                    var td2E = new HtmlTableCell("td");
                    var td3E = new HtmlTableCell("td");
                    var td4E = new HtmlTableCell("td");
                    var td5E = new HtmlTableCell("td");
                    //var td6E = new HtmlTableCell("td");
                    //var td7E = new HtmlTableCell("td");
                    td1E.InnerText = row["MATERIAL"].ToString();
                    td2E.InnerText = row["DESCRIPCION"].ToString();
                    td3E.InnerText = row["UBICACION"].ToString();
                    td4E.InnerText = row["SECADO"].ToString();
                    td5E.InnerText = row["NOTAS"].ToString();
                    //td6E.InnerText = row["F"].ToString();
                    //td7E.InnerText = row["G"].ToString();
                    tr2E.Controls.Add(td1E);
                    tr2E.Controls.Add(td2E);
                    tr2E.Controls.Add(td3E);
                    tr2E.Controls.Add(td4E);
                    tr2E.Controls.Add(td5E);
                    //tr2E.Controls.Add(td6E);
                    //tr2E.Controls.Add(td7E);
                    Estructura.Controls.Add(tr2E);
                }
            }
            catch (Exception)
            {
            }
        }

        public void Cargar_historico_modificaciones(int referencia, int maquina)
        {
            try
            {
                Conexion conexion = new Conexion();
                DataSet ds = conexion.leerHistorico(referencia, maquina);
                HistoricoModificaciones.Controls.Clear();
                var tr1 = new HtmlTableRow();
                var th1 = new HtmlTableCell("th");
                th1.InnerText = "Molde";
                var th2 = new HtmlTableCell("th");
                th2.InnerText = "Máquina";
                var th3 = new HtmlTableCell("th");
                th3.InnerText = "Revisión";
                var th4 = new HtmlTableCell("th");
                th4.InnerText = "Fecha";
                var th5 = new HtmlTableCell("th");
                th5.InnerText = "Cambio";
                var th6 = new HtmlTableCell("th");
                th6.InnerText = "Razón";
                var th7 = new HtmlTableCell("th");
                th7.InnerText = "Autor";
                tr1.Controls.Add(th1);
                tr1.Controls.Add(th2);
                tr1.Controls.Add(th3);
                tr1.Controls.Add(th4);
                tr1.Controls.Add(th5);
                tr1.Controls.Add(th6);
                tr1.Controls.Add(th7);
                HistoricoModificaciones.Controls.Add(tr1);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var tr2 = new HtmlTableRow();
                    var td1 = new HtmlTableCell("td");
                    var td2 = new HtmlTableCell("td");
                    var td3 = new HtmlTableCell("td");
                    var td4 = new HtmlTableCell("td");
                    var td5 = new HtmlTableCell("td");
                    var td6 = new HtmlTableCell("td");
                    var td7 = new HtmlTableCell("td");
                    td1.InnerText = row["CodMolde"].ToString();
                    td2.InnerText = row["Maquina"].ToString();
                    td3.InnerText = row["Version"].ToString();
                    td4.InnerText = row["Fecha"].ToString();
                    td5.InnerText = row["Observaciones"].ToString();
                    td6.InnerText = row["Razones"].ToString();
                    td7.InnerText = row["Elaborado"].ToString();
                    tr2.Controls.Add(td1);
                    tr2.Controls.Add(td2);
                    tr2.Controls.Add(td3);
                    tr2.Controls.Add(td4);
                    tr2.Controls.Add(td5);
                    tr2.Controls.Add(td6);
                    tr2.Controls.Add(td7);
                    HistoricoModificaciones.Controls.Add(tr2);
                }
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

        protected void thP1_TextChanged(object sender, EventArgs e)
        {

        }


    }
    
}