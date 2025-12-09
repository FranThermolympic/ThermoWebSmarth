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

namespace ThermoWeb.KPI
{
    public partial class KPILiberaciones : System.Web.UI.Page
    {

        private static DataSet ds_ListaLiberacionesWEEK = new DataSet();
        private static DataSet ds_ListaLiberacionesCAMBIADORTOTAL = new DataSet();
        private static DataSet ds_ListaLiberacionesENCARGADOTOTAL = new DataSet();
        private static DataSet ds_ListaLiberacionesCALIDADTOTAL = new DataSet();

        private static DataSet ds_ListaLiberacionesDEPT_CAMBIADORES = new DataSet();
        private static DataSet ds_ListaLiberacionesDEPT_PRODUCCION = new DataSet();
        private static DataSet ds_ListaLiberacionesDEPT_CALIDAD = new DataSet();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Conexion_KPI conexion = new Conexion_KPI();
                //CargaListasFiltro();
                Cargar_todas(null, null);
                //lkb_Sort_Click("ANUAL");
                Rellenar_grid();
            }

        }

        private void Rellenar_grid()
        {
            try
            {
                //dgv_liberaciones_gral.DataSource = ds_ListaLiberacionesWEEK;
                //dgv_liberaciones_gral.DataBind();

                dgv_CambiadorTOTAL.DataSource = ds_ListaLiberacionesCAMBIADORTOTAL;
                dgv_CambiadorTOTAL.DataBind();

                dgv_EncargadoTOTAL.DataSource = ds_ListaLiberacionesENCARGADOTOTAL;
                dgv_EncargadoTOTAL.DataBind();

                dgv_CalidadTOTAL.DataSource = ds_ListaLiberacionesCALIDADTOTAL;
                dgv_CalidadTOTAL.DataBind();

                GridResulDeptCambio.DataSource = ds_ListaLiberacionesDEPT_CAMBIADORES;
                GridResulDeptCambio.DataBind();

                GridResulDeptCalidad.DataSource = ds_ListaLiberacionesDEPT_CALIDAD;
                GridResulDeptCalidad.DataBind();

                GridResulDeptProduccion.DataSource = ds_ListaLiberacionesDEPT_PRODUCCION;
                GridResulDeptProduccion.DataBind();

                //PRODUCCION
                int ProduccionAbiertas = 0;
                int ProduccionConformes = 0;
                int ProduccionCondicionadas = 0;
                int ProduccionSinLiberar = 0;
                foreach (DataRow dr in ds_ListaLiberacionesDEPT_PRODUCCION.Tables[0].Rows)
                {
                    ProduccionAbiertas += Convert.ToInt32(dr["ABIERTAS"]);
                    ProduccionConformes += Convert.ToInt32(dr["OK"]);
                    ProduccionCondicionadas += Convert.ToInt32(dr["CONDICIONADAS"]);
                    ProduccionSinLiberar += Convert.ToInt32(dr["SINLIBERAR"]);
                }

                double PorcPRODConformes = (ProduccionConformes * 100) / ProduccionAbiertas;
                PORCKPIProduccionConformes.InnerText = PorcPRODConformes.ToString("0.#") + "% conformes";
                double PorcPRODCondicionadas = (ProduccionCondicionadas * 100) / ProduccionAbiertas;
                PORCKPIProduccionCondicionadas.InnerText = PorcPRODCondicionadas.ToString("0.#") + "% condicionadas";
                double PorcPRODSinLiberar = (ProduccionSinLiberar * 100) / ProduccionAbiertas;
                PORCKPIProduccionSinliberar.InnerText = PorcPRODSinLiberar.ToString("0.#") + "% sin liberar";

                KPIProduccionAbiertas.InnerText = ProduccionAbiertas.ToString();
                KPIProduccionConformes.InnerText = ProduccionConformes.ToString();
                KPIProduccionCondicionadas.InnerText = ProduccionCondicionadas.ToString();
                KPIProduccionSinliberar.InnerText = ProduccionSinLiberar.ToString();

                //CALIDAD
                double CalidadAbiertas = 0;
                double CalidadConformes = 0;
                double CalidadCondicionadas = 0;
                double CalidadSinLiberar = 0;
                foreach (DataRow dr in ds_ListaLiberacionesDEPT_CALIDAD.Tables[0].Rows)
                {
                    CalidadAbiertas += Convert.ToInt32(dr["ABIERTAS"]);
                    CalidadConformes += Convert.ToInt32(dr["OK"]);
                    CalidadCondicionadas += Convert.ToInt32(dr["CONDICIONADAS"]);
                    CalidadSinLiberar += Convert.ToInt32(dr["SINLIBERAR"]);
                }

                double PorcCALConformes = (CalidadConformes * 100) / CalidadAbiertas;
                PORCKPICalidadConformes.InnerText = PorcCALConformes.ToString("0.#") + "% conformes";
                double PorcCALCondicionadas = (CalidadCondicionadas * 100) / CalidadAbiertas;
                PORCKPICalidadCondicionadas.InnerText = PorcCALCondicionadas.ToString("0.#") + "% condicionadas";
                double PorcCALSinLiberar = (CalidadSinLiberar * 100) / CalidadAbiertas;
                PORCKPICalidadSinliberar.InnerText = PorcCALSinLiberar.ToString("0.#") + "% sin liberar";

                KPICalidadAbiertas.InnerText = CalidadAbiertas.ToString();
                KPICalidadConformes.InnerText = CalidadConformes.ToString();
                KPICalidadCondicionadas.InnerText = CalidadCondicionadas.ToString();
                KPICalidadSinliberar.InnerText = CalidadSinLiberar.ToString();


                //CAMBIADORES
                double CambiadoresAbiertas = 0;
                double CambiadoresConformes = 0;
                double CambiadoresCondicionadas = 0;
                double CambiadoresSinLiberar = 0;
                foreach (DataRow dr in ds_ListaLiberacionesDEPT_CAMBIADORES.Tables[0].Rows)
                {
                    CambiadoresAbiertas += Convert.ToInt32(dr["ABIERTAS"]);
                    CambiadoresConformes += Convert.ToInt32(dr["OK"]);
                    CambiadoresCondicionadas += Convert.ToInt32(dr["CONDICIONADAS"]);
                    CambiadoresSinLiberar += Convert.ToInt32(dr["SINLIBERAR"]);
                }

                double PorcCAMConformes = (CambiadoresConformes * 100) / CambiadoresAbiertas;
                PORCKPICambiadorConformes.InnerText = PorcCAMConformes.ToString("0.#") + "% conformes";
                double PorcCAMCondicionadas = (CambiadoresCondicionadas * 100) / CambiadoresAbiertas;
                PORCKPICambiadorCondicionadas.InnerText = PorcCAMCondicionadas.ToString("0.#") + "% condicionadas";
                double PorcCAMSinLiberar = (CambiadoresSinLiberar * 100) / CambiadoresAbiertas;
                PORCKPICambiadorSinliberar.InnerText = PorcCAMSinLiberar.ToString("0.#") + "% sin liberar";

                KPICambiadorAbiertas.InnerText = CambiadoresAbiertas.ToString();
                KPICambiadorConformes.InnerText = CambiadoresConformes.ToString();
                KPICambiadorCondicionadas.InnerText = CambiadoresCondicionadas.ToString();
                KPICambiadorSinliberar.InnerText = CambiadoresSinLiberar.ToString();



            }
            catch (Exception)
            { 
                
            }
        }     
        // carga la lista utilizando un filtro

        protected void Cargar_todas(object sender, EventArgs e)
        {
            
            Conexion_KPI conexion = new Conexion_KPI();
            ds_ListaLiberacionesWEEK = conexion.KPILiberacionesWEEK(DropSelectAño.SelectedValue);

            ds_ListaLiberacionesCAMBIADORTOTAL = conexion.KPILiberacionesCAMBIADORTOTALES(DropSelectAño.SelectedValue);
            ds_ListaLiberacionesENCARGADOTOTAL = conexion.KPILiberacionesPRODUCCIONTOTALES(DropSelectAño.SelectedValue);
            ds_ListaLiberacionesCALIDADTOTAL = conexion.KPILiberacionesCALIDADTOTALES(DropSelectAño.SelectedValue);

            ds_ListaLiberacionesDEPT_CAMBIADORES = conexion.KPILiberacionesDepartamentoCAMBIADOR(DropSelectAño.SelectedValue);
            ds_ListaLiberacionesDEPT_PRODUCCION = conexion.KPILiberacionesDepartamentoPRODUCCION(DropSelectAño.SelectedValue);
            ds_ListaLiberacionesDEPT_CALIDAD = conexion.KPILiberacionesDepartamentoCALIDAD(DropSelectAño.SelectedValue);


            Rellenar_grid();
        }

        protected void Filtrar_Preguntas(object sender, EventArgs e)
        {
            try
            {
              //cuestiones
                /*
                 * 1.Máquina y programas
                 * Q1E = 1 or Q1ENC <> '' 
                 * 2.Conexiones de agua
                 * Q2E = 1 or Q2ENC <> '' 
                 * 3.Periféricos y ajuste mecánico
                 * Q3E = 1 or Q3ENC <> ''
                 * 4.Condiciones iniciales
                 * Q4E = 1 or Q4ENC <> '' 
                 * 5.Primeras inyectadas
                 * Q5E = 1 or Q5ENC <> ''
                 * 6.Pokayokes, galgas de control y máquinas periféricas
                 * Q6E = 1 or Q6ENC <> '' or Q6C = 1 or Q6CAL <> ''
                 * 7.Puesto de trabajo
                 * Q7E = 1 or Q7ENC <> '' or Q7C = 1 or Q7CAL <> ''
                 * 8.Anti mezclas
                 * Q8E = 1 or Q8ENC <> '' or Q8C = 1 or Q8CAL <> ''
                 * 9.Elementos auxiliares
                 * Q9E = 1 or Q9ENC <> '' or Q9C = 1 or Q9CAL <> ''
                 * 10.Control de atributos
                 * Q10E = 1 or Q10ENC <> '' or Q10C = 1 or Q10CAL <> ''
                 */
            }
            catch (Exception ex)
            { }
        
        }





        /* protected void lkb_Sort_Click( string e)
         {
             //Bind the data

             //Manage the selected tab.
             this.ManageTabsPostBack(TURNO, ANUAL, tab0button, tab1button, e);

         }
        */

        /*private void ManageTabsPostBack(HtmlGenericControl TURNO, HtmlGenericControl ANUAL,
                    HtmlGenericControl tab0button, HtmlGenericControl tab1button, string grid)
        {
            // desactivte all tabs and panes
            tab0button.Attributes.Add("class", "");
            TURNO.Attributes.Add("class", "tab-pane");
            tab1button.Attributes.Add("class", "");
            ANUAL.Attributes.Add("class", "tab-pane");

            //activate the the tab and pane the user was viewing
            switch (grid)
            {
                case "TURNO":
                    tab0button.Attributes.Add("class", "active");
                    TURNO.Attributes.Add("class", "tab-pane active");
                    break;
                case "ANUAL":
                    tab1button.Attributes.Add("class", "active");
                    ANUAL.Attributes.Add("class", "tab-pane active");
                    break;

            }
        }*/
    }

}