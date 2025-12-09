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

namespace ThermoWeb.PRODUCCION
{
    public partial class FichasParametros_nuevo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                CargaAuxiliares();
            }

        }
        //cargar datos de atemperado
        private void CargaAuxiliares()
        {
            try
            {
                //defino sources de imagenes--reposicionar
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

                //NUEVOS CCAMPOS
                double LimitePresionRealDouble = 0.0;
                double LimitePresionNRealValDouble = 0.0;
                double LimitePresionMRealValDouble = 0.0;
                double TOLCarInyeccionDouble = 10.0;
                double TOLCamCalienteDouble = 10.0;
                double TOLCilindroDouble = 10.0;
                double TOLPostPresionDouble = 10.0;
                double TOLSecCotaDouble = 10.0;
                double TOLSecTiempoDouble = 10.0;

                if (Double.TryParse(tbLimitePresionReal.Text.Replace('.', ','), out LimitePresionRealDouble))
                    LimitePresionRealDouble = Convert.ToDouble(tbLimitePresionReal.Text.Replace('.', ','));
                else
                    LimitePresionRealDouble = 0.0;

                if (Double.TryParse(tbLimitePresionReal.Text.Replace('.', ','), out LimitePresionNRealValDouble))
                    LimitePresionNRealValDouble = Convert.ToDouble(tbLimitePresionReal.Text.Replace('.', ','))*0.9;
                else
                    LimitePresionNRealValDouble = 0.0;

                if (Double.TryParse(tbLimitePresionReal.Text.Replace('.', ','), out LimitePresionMRealValDouble))
                    LimitePresionMRealValDouble = Convert.ToDouble(tbLimitePresionReal.Text.Replace('.', ','))*1.1;
                else
                    LimitePresionMRealValDouble = 0.0;

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

                if (!conexion.existeFicha(Convert.ToInt32(tbFicha.Value), Convert.ToInt32(tbMaquina.Text), 1))
                {

                    DataSet ds = conexion.cargar_datos_bms(Convert.ToInt32(tbFicha.Value));
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        tbNombre.Text = ds.Tables[0].Rows[0]["Descripcion"].ToString();
                        tbCliente.Text = ds.Tables[0].Rows[0]["Cliente"].ToString();
                        tbCodigoMolde.Text = ds.Tables[0].Rows[0]["Molde"].ToString();
                        tbPersonal.Text = ds.Tables[0].Rows[0]["Operarios"].ToString();
                        tbCavidades.Text = ds.Tables[0].Rows[0]["Cavidades"].ToString();
                    }
                    conexion.InsertarFichaV2(Convert.ToInt32(tbFicha.Value), Convert.ToInt32(tbMaquina.Text), 1, tbNombre.Text, tbCliente.Text,
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
                    "1", DateTime.Now.ToString(), SHConexion.Devuelve_ID_Piloto_SMARTH(cbElaboradoPor.SelectedValue.ToString())/*cbElaboradoPor.Text*/, SHConexion.Devuelve_ID_Piloto_SMARTH(cbRevisadoPor.SelectedValue.ToString())/*cbRevisadoPor.Text*/, SHConexion.Devuelve_ID_Piloto_SMARTH(cbAprobadoPor.SelectedValue.ToString())/*cbAprobadoPor.Text*/, tbObservaciones.Text, tbFuerzaCierre.Text);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "guardado_OK();", true);
                    //Response.Redirect("FichasParametros.aspx?REFERENCIA=" + tbFicha.Value + "&MAQUINA=" + tbMaquina.Text + "&VERSION=1");
                }
                else
                { Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "guardado_NOKEXISTE();", true); }
            }
            catch (Exception)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "guardado_NOK();", true);
            }
        }
        /*
        public void importarMaquina(object sender, EventArgs e)
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
        */
        public void CargarDatosBms (Object sender, EventArgs e)
        {
            try
            {
                Conexion conexion = new Conexion();
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                //Recupero y cargo los datos iniciales de BMS
                DataSet ds = conexion.cargar_datos_bms(Convert.ToInt32(tbFicha.Value));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    tbNombre.Text = ds.Tables[0].Rows[0]["Descripcion"].ToString();
                    tbCliente.Text = ds.Tables[0].Rows[0]["Cliente"].ToString();
                    tbCodigoMolde.Text = ds.Tables[0].Rows[0]["Molde"].ToString();
                    tbPersonal.Text = ds.Tables[0].Rows[0]["Operarios"].ToString();
                    tbCavidades.Text = ds.Tables[0].Rows[0]["Cavidades"].ToString();
                }
                //Relleno la ubicación del molde
                DataTable Molde = SHConexion.Devuelve_Molde(tbCodigoMolde.Text);
                UbicaMolde.InnerText = tbCodigoMolde.Text;
                UbicaMoldeNombre.InnerText = Molde.Rows[0]["Descripcion"].ToString();
                UbicacionMolde.Text = Molde.Rows[0]["Ubicacion"].ToString();
                LblModificado.Text = "Modificado: " + Molde.Rows[0]["FechaModificaUbicacion"].ToString();
                flexCheckDefault.Checked = Convert.ToBoolean(Molde.Rows[0]["Activo"]);
                ImgUbicacion.Src = Molde.Rows[0]["ImagenUbi"].ToString();
                BTNModalUbicacion.Disabled = false;
                tbUbicacionMolde.Text = Molde.Rows[0]["Ubicacion"].ToString();

                //Relleno los campos de la mano
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

                tbManoRobot.Text = MoldexMano.Rows[0]["Mano"].ToString();
                tbUbicacionMano.Text = ubicacion;
                BTNManoUbi.Disabled = false;


            }
            catch (Exception ex)
            {
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
            }
            catch (Exception ex)
            { }
        }

    }

}