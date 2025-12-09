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

namespace ThermoWeb
{
    public partial class FichasParametros_nuevo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ajustar_tamaño_campos();
                cargardatasets();
            }

        }
        //cargar datos de atemperado
         private void cargardatasets()
        {
            try
            {
                //defino sources de imagenes--reposicionar
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

                Conexion conexion = new Conexion();
                //TIPO DE ATEMPERADO
                DataSet AtempTipo = new DataSet();
                AtempTipo = conexion.devuelve_lista_tipo_atemperado();
                    //PARTE FIJA
                    AtempTipoF.Items.Clear();
                    foreach (DataRow row in AtempTipo.Tables[0].Rows)
                    { AtempTipoF.Items.Add(row["TipoAgua"].ToString()); }

                    //PARTE MOVIL
                    AtempTipoM.Items.Clear();
                    foreach (DataRow row in AtempTipo.Tables[0].Rows)
                    { AtempTipoM.Items.Add(row["TipoAgua"].ToString()); }

                //CIRCUITOS
                DataSet Circuitos = new DataSet();
                Circuitos = conexion.devuelve_lista_circuitos();
                    //PARTE FIJA
                    TbCircuitoF1.Items.Clear();
                    foreach (DataRow row in Circuitos.Tables[0].Rows) { TbCircuitoF1.Items.Add(row["Circuitos"].ToString()); }

                    TbCircuitoF2.Items.Clear();
                    foreach (DataRow row in Circuitos.Tables[0].Rows) { TbCircuitoF2.Items.Add(row["Circuitos"].ToString()); }

                    TbCircuitoF3.Items.Clear();
                    foreach (DataRow row in Circuitos.Tables[0].Rows) { TbCircuitoF3.Items.Add(row["Circuitos"].ToString()); }

                    TbCircuitoF4.Items.Clear();
                    foreach (DataRow row in Circuitos.Tables[0].Rows) { TbCircuitoF4.Items.Add(row["Circuitos"].ToString()); }

                    TbCircuitoF5.Items.Clear();
                    foreach (DataRow row in Circuitos.Tables[0].Rows) { TbCircuitoF5.Items.Add(row["Circuitos"].ToString()); }

                    TbCircuitoF6.Items.Clear();
                    foreach (DataRow row in Circuitos.Tables[0].Rows) { TbCircuitoF6.Items.Add(row["Circuitos"].ToString()); }

                    //PARTE MOVIL
                    TbCircuitoM1.Items.Clear();
                    foreach (DataRow row in Circuitos.Tables[0].Rows) { TbCircuitoM1.Items.Add(row["Circuitos"].ToString()); }

                    TbCircuitoM2.Items.Clear();
                    foreach (DataRow row in Circuitos.Tables[0].Rows) { TbCircuitoM2.Items.Add(row["Circuitos"].ToString()); }

                    TbCircuitoM3.Items.Clear();
                    foreach (DataRow row in Circuitos.Tables[0].Rows) { TbCircuitoM3.Items.Add(row["Circuitos"].ToString()); }

                    TbCircuitoM4.Items.Clear();
                    foreach (DataRow row in Circuitos.Tables[0].Rows) { TbCircuitoM4.Items.Add(row["Circuitos"].ToString()); }

                    TbCircuitoM5.Items.Clear();
                    foreach (DataRow row in Circuitos.Tables[0].Rows) { TbCircuitoM5.Items.Add(row["Circuitos"].ToString()); }

                    TbCircuitoM6.Items.Clear();
                    foreach (DataRow row in Circuitos.Tables[0].Rows) { TbCircuitoM6.Items.Add(row["Circuitos"].ToString()); }

                //ENTRADAS ATEMPERADO
                DataSet entradasAtemperado = new DataSet();
                entradasAtemperado = conexion.devuelve_lista_entradasAtemp();
                //PARTE FIJA
                TbEntradaF1.Items.Clear();
                foreach (DataRow row in entradasAtemperado.Tables[0].Rows) { TbEntradaF1.Items.Add(row["AtempIN"].ToString()); }

                TbEntradaF2.Items.Clear();
                foreach (DataRow row in entradasAtemperado.Tables[0].Rows) { TbEntradaF2.Items.Add(row["AtempIN"].ToString()); }

                TbEntradaF3.Items.Clear();
                foreach (DataRow row in entradasAtemperado.Tables[0].Rows) { TbEntradaF3.Items.Add(row["AtempIN"].ToString()); }

                TbEntradaF4.Items.Clear();
                foreach (DataRow row in entradasAtemperado.Tables[0].Rows) { TbEntradaF4.Items.Add(row["AtempIN"].ToString()); }

                TbEntradaF5.Items.Clear();
                foreach (DataRow row in entradasAtemperado.Tables[0].Rows) { TbEntradaF5.Items.Add(row["AtempIN"].ToString()); }

                TbEntradaF6.Items.Clear();
                foreach (DataRow row in entradasAtemperado.Tables[0].Rows) { TbEntradaF6.Items.Add(row["AtempIN"].ToString()); }

                //PARTE MOVIL
                TbEntradaM1.Items.Clear();
                foreach (DataRow row in entradasAtemperado.Tables[0].Rows) { TbEntradaM1.Items.Add(row["AtempIN"].ToString()); }

                TbEntradaM2.Items.Clear();
                foreach (DataRow row in entradasAtemperado.Tables[0].Rows) { TbEntradaM2.Items.Add(row["AtempIN"].ToString()); }

                TbEntradaM3.Items.Clear();
                foreach (DataRow row in entradasAtemperado.Tables[0].Rows) { TbEntradaM3.Items.Add(row["AtempIN"].ToString()); }

                TbEntradaM4.Items.Clear();
                foreach (DataRow row in entradasAtemperado.Tables[0].Rows) { TbEntradaM4.Items.Add(row["AtempIN"].ToString()); }

                TbEntradaM5.Items.Clear();
                foreach (DataRow row in entradasAtemperado.Tables[0].Rows) { TbEntradaM5.Items.Add(row["AtempIN"].ToString()); }

                TbEntradaM6.Items.Clear();
                foreach (DataRow row in entradasAtemperado.Tables[0].Rows) { TbEntradaM6.Items.Add(row["AtempIN"].ToString()); }

                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                DataSet Operarios = SHConexion.Devuelve_mandos_intermedios_SMARTH();
              
                  
                //ELABORADO
                Operarios.Tables[0].DefaultView.RowFilter = "Departamento = 'PRODUCCION' OR Departamento = '-'";
                DataTable DTPRODUCCION = (Operarios.Tables[0].DefaultView).ToTable();
                cbElaboradoPor.Items.Clear();
                foreach (DataRow row in DTPRODUCCION.Rows)
                { cbElaboradoPor.Items.Add(row["Nombre"].ToString()); }

                //REVISADO
                cbRevisadoPor.Items.Clear();
                foreach (DataRow row in DTPRODUCCION.Rows)
                { cbRevisadoPor.Items.Add(row["Nombre"].ToString()); }
                

   
                //APROBADO
                Operarios.Tables[0].DefaultView.RowFilter = "Departamento = 'INGENIERIA' OR Departamento = '-' OR Departamento = 'CALIDAD'";
                DTPRODUCCION = (Operarios.Tables[0].DefaultView).ToTable();
                cbAprobadoPor.Items.Clear();
                foreach (DataRow row in DTPRODUCCION.Rows)
                { cbAprobadoPor.Items.Add(row["Nombre"].ToString()); }
                

            }
            catch (Exception)
            {
            }
        }


        // ajusta el tamaño de los textbox
        private void ajustar_tamaño_campos()
        {
            try
            {


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
                TextBox1.Width = 260;
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

                //aguas antigua
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

                tbMovilRefrigeracion.Width =200;
                tbMovilAtempCircuito.Width = 160;
                tbMovilAtempTemperatura.Width = 160;*/

                //ATEMPERADO
                AtempTipoF.Width = 980;
                ThCircuitoF.Width = 140;
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
                TbEntradaF1.Width = 140;
                TbEntradaF2.Width = 140;
                TbEntradaF3.Width = 140;
                TbEntradaF4.Width = 140;
                TbEntradaF5.Width = 140;
                TbEntradaF6.Width = 140;
                //ThSalidaF.Width = 120;
                AtempTipoM.Width = 980;
                ThCircuitoM.Width = 140;
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
                tbTiempoInyeccionN.Enabled = false;
                tbTiempoInyeccionNVal.Enabled = false;
                tbTiempoInyeccionM.Enabled = false;
                tbTiempoInyeccionMVal.Enabled = false;
                tbLimitePresionN.Enabled = false;
                tbLimitePresionNVal.Enabled = false;
                tbLimitePresionM.Enabled = false;
                tbLimitePresionMval.Enabled = false;
                
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

                thConmuntaciontolN.Width = 42;
                thConmuntaciontolNVal.Width = 42;
                thConmuntaciontolM.Width = 42;
                thConmuntaciontolMVal.Width = 42;
                tbTiempoPresiontolN.Width = 42;
                tbTiempoPresiontolNVal.Width = 42;
                tbTiempoPresiontolM.Width = 42;
                tbTiempoPresiontolMVal.Width = 42;
                thConmuntaciontolN.Enabled = false;
                thConmuntaciontolNVal.Enabled = false;
                thConmuntaciontolM.Enabled = false;
                thConmuntaciontolMVal.Enabled = false;
                tbTiempoPresiontolN.Enabled = false;
                tbTiempoPresiontolNVal.Enabled = false;
                tbTiempoPresiontolM.Enabled = false;
                tbTiempoPresiontolMVal.Enabled = false;

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
                TNCojinval.Enabled = false;
                TMCojin.Enabled = false;
                TMCojinval.Enabled = false;
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


                //secuencial tiempos de inyección
                //datos de la ficha
                cbEdicionTitulo.Width = 220;
                cbFechaTitulo.Width = 220;
                cbElaboradoPorTitulo.Width = 220;
                cbRevisadoPorTitulo.Width = 220;
                cbAprobadoPorTitulo.Width = 220;
                cbEdicion.Width = 220;
                cbEdicion.Height = 33;
                cbEdicion.Enabled = false;
                cbFecha.Width = 220;
                cbFecha.Height = 33;
                cbFecha.Enabled = false;
                cbElaboradoPor.Width = 220;
                cbRevisadoPor.Width = 220;
                cbAprobadoPor.Width = 220;
                tbObservacionesTitulo.Width = 660;
                tbObservaciones.Width = 660;
                tbObservaciones.Enabled = false;
                tbRazonesTitulo.Width = 440;
                tbRazones.Width = 440;
                tbRazones.Enabled = false;
            }
            catch (Exception)
            { 
                
            }
        }            

        public void guardarFicha(Object sender, EventArgs e)
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
                if (Double.TryParse(thP1.Text.Replace('.', ','), out thP1_double))
                    thP1_double = Convert.ToDouble(thP1.Text.Replace('.', ','));
                else
                    thP1_double = 0.0;
                if (Double.TryParse(thP2.Text.Replace('.', ','), out thP2_double))
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

                if (Double.TryParse(tbTiempoInyeccion.Text.Replace('.', ','), out TiempoInyeccionNValDouble))
                    TiempoInyeccionNValDouble = Convert.ToDouble(tbTiempoInyeccion.Text.Replace('.', ',')) * 0.9;
                else
                    TiempoInyeccionNValDouble = 0.0;
                if (Double.TryParse(tbTiempoInyeccion.Text.Replace('.', ','), out TiempoInyeccionMValDouble))
                    TiempoInyeccionMValDouble = Convert.ToDouble(tbTiempoInyeccion.Text.Replace('.', ',')) * 1.1;
                else
                    TiempoInyeccionMValDouble = 0.0;
                if (Double.TryParse(tbLimitePresion.Text.Replace('.', ','), out LimitePresionNValDouble))
                    LimitePresionNValDouble = Convert.ToDouble(tbLimitePresion.Text.Replace('.', ',')) * 0.9;
                else
                    LimitePresionNValDouble = 0.0;
                if (Double.TryParse(tbLimitePresion.Text.Replace('.', ','), out LimitePresionMValDouble))
                    LimitePresionMValDouble = Convert.ToDouble(tbLimitePresion.Text.Replace('.', ',')) * 1.1;
                else
                    LimitePresionNValDouble = 0.0;
                if (Double.TryParse(tbConmutacion.Text.Replace('.', ','), out ConmuntaciontolNValDouble))
                    ConmuntaciontolNValDouble = Convert.ToDouble(tbConmutacion.Text.Replace('.', ',')) * 0.9;
                else
                    ConmuntaciontolNValDouble = 0.0;
                if (Double.TryParse(tbConmutacion.Text.Replace('.', ','), out ConmuntaciontolMValDouble))
                    ConmuntaciontolMValDouble = Convert.ToDouble(tbConmutacion.Text.Replace('.', ',')) * 1.1;
                else
                    ConmuntaciontolMValDouble = 0.0;
                if (Double.TryParse(tbTiempoPresion.Text.Replace('.', ','), out TiempoPresiontolNValDouble))
                    TiempoPresiontolNValDouble = Convert.ToDouble(tbTiempoPresion.Text.Replace('.', ',')) * 0.9;
                else
                    TiempoPresiontolNValDouble = 0.0;
                if (Double.TryParse(tbTiempoPresion.Text.Replace('.', ','), out TiempoPresiontolMValDouble))
                    TiempoPresiontolMValDouble = Convert.ToDouble(tbTiempoPresion.Text.Replace('.', ',')) * 1.1;
                else
                    TiempoPresiontolMValDouble = 0.0;
                if (Double.TryParse(thVCarga.Text.Replace('.', ','), out TNvcargavalDouble))
                    TNvcargavalDouble = Convert.ToDouble(thVCarga.Text.Replace('.', ',')) * 0.9;
                else
                    TNvcargavalDouble = 0.0;
                if (Double.TryParse(thVCarga.Text.Replace('.', ','), out TMvcargavalDouble))
                    TMvcargavalDouble = Convert.ToDouble(thVCarga.Text.Replace('.', ',')) * 1.1;
                else
                    TMvcargavalDouble = 0.0;
                if (Double.TryParse(thCarga.Text.Replace('.', ','), out TNcargavalDouble))
                    TNcargavalDouble = Convert.ToDouble(thCarga.Text.Replace('.', ',')) * 0.9;
                else
                    TNcargavalDouble = 0.0;
                if (Double.TryParse(thCarga.Text.Replace('.', ','), out TMcargavalDouble))
                    TMcargavalDouble = Convert.ToDouble(thCarga.Text.Replace('.', ',')) * 1.1;
                else
                    TMcargavalDouble = 0.0;
                if (Double.TryParse(thDescomp.Text.Replace('.', ','), out TNdescomvalDouble))
                    TNdescomvalDouble = Convert.ToDouble(thDescomp.Text.Replace('.', ',')) * 0.9;
                else
                    TNdescomvalDouble = 0.0;
                if (Double.TryParse(thDescomp.Text.Replace('.', ','), out TMdescomvalDouble))
                    TMdescomvalDouble = Convert.ToDouble(thDescomp.Text.Replace('.', ',')) * 1.1;
                else
                    TMdescomvalDouble = 0.0;
                if (Double.TryParse(thContrapr.Text.Replace('.', ','), out TNcontrapvalDouble))
                    TNcontrapvalDouble = Convert.ToDouble(thContrapr.Text.Replace('.', ',')) * 0.9;
                else
                    TNcontrapvalDouble = 0.0;
                if (Double.TryParse(thContrapr.Text.Replace('.', ','), out TMcontrapvalDouble))
                    TMcontrapvalDouble = Convert.ToDouble(thContrapr.Text.Replace('.', ',')) * 1.1;
                else
                    TMcontrapvalDouble = 0.0;
                if (Double.TryParse(thTiempo.Text.Replace('.', ','), out TNTiempdosvalDouble))
                    TNTiempdosvalDouble = Convert.ToDouble(thTiempo.Text.Replace('.', ',')) * 0.9;
                else
                    TNTiempdosvalDouble = 0.0;
                if (Double.TryParse(thTiempo.Text.Replace('.', ','), out TMTiempdosvalDouble))
                    TMTiempdosvalDouble = Convert.ToDouble(thTiempo.Text.Replace('.', ',')) * 1.1;
                else
                    TMTiempdosvalDouble = 0.0;
                if (Double.TryParse(thEnfriamiento.Text.Replace('.', ','), out TNEnfriamvalDouble))
                    TNEnfriamvalDouble = Convert.ToDouble(thEnfriamiento.Text.Replace('.', ',')) * 0.9;
                else
                    TNEnfriamvalDouble = 0.0;
                if (Double.TryParse(thEnfriamiento.Text.Replace('.', ','), out TMEnfriamvalDouble))
                    TMEnfriamvalDouble = Convert.ToDouble(thEnfriamiento.Text.Replace('.', ',')) * 1.1;
                else
                    TMEnfriamvalDouble = 0.0;
                if (Double.TryParse(thCiclo.Text.Replace('.', ','), out TNCiclovalDouble))
                    TNCiclovalDouble = Convert.ToDouble(thCiclo.Text.Replace('.', ',')) * 0.9;
                else
                    TNCiclovalDouble = 0.0;
                if (Double.TryParse(thCiclo.Text.Replace('.', ','), out TMCiclovalDouble))
                    TMCiclovalDouble = Convert.ToDouble(thCiclo.Text.Replace('.', ',')) * 1.1;
                else
                    TMCiclovalDouble = 0.0;
                if (Double.TryParse(thCojin.Text.Replace('.', ','), out TNCojinvalDouble))
                    TNCojinvalDouble = Convert.ToDouble(thCojin.Text.Replace('.', ',')) * 0.9;
                else
                    TNCojinvalDouble = 0.0;
                if (Double.TryParse(thCojin.Text.Replace('.', ','), out TMCojinvalDouble))
                    TMCojinvalDouble = Convert.ToDouble(thCojin.Text.Replace('.', ',')) * 1.1;
                else
                    TMCojinvalDouble = 0.0;
                //FIN TOLERANCIAS

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


                if (!conexion.existeFicha(Convert.ToInt32(tbReferencia.Text), Convert.ToInt32(tbMaquina.Text), 1))
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
                    conexion.insertarFicha(Convert.ToInt32(tbReferencia.Text), Convert.ToInt32(tbMaquina.Text), 1, tbNombre.Text, tbCliente.Text,
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
                else
                { Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "guardado_NOKEXISTE();", true); }
            }
            catch (Exception)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "guardado_NOK();", true);
            }
        }

        public void importarMaquina(Object sender, EventArgs e)
        {
            try
            {
                Conexion conexion = new Conexion();
                conexion.comprobarMaquina(Convert.ToInt32(tbReferencia.Text));
            }
            catch (Exception)
            {
            }
        }
        public void cargardatosbms (Object sender, EventArgs e)
        {
            try
            {
                Conexion conexion = new Conexion();
                DataSet ds = conexion.cargar_datos_bms(Convert.ToInt32(tbReferencia.Text));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    tbNombre.Text = ds.Tables[0].Rows[0]["Descripcion"].ToString();
                    tbCliente.Text = ds.Tables[0].Rows[0]["Cliente"].ToString();
                    tbCodigoMolde.Text = ds.Tables[0].Rows[0]["Molde"].ToString();
                    tbPersonal.Text = ds.Tables[0].Rows[0]["Operarios"].ToString();
                    tbCavidades.Text = ds.Tables[0].Rows[0]["Cavidades"].ToString();
                }

            }
            catch (Exception)
            {
            }
        }

        public void insertar_foto(Object sender, EventArgs e)
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


        
    }

}