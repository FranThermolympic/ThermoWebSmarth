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
using System.ComponentModel;


namespace ThermoWeb.KPI
{
    public partial class KPIConsultasConsumos : System.Web.UI.Page
    {

        private static DataSet ds_KPI_Uso_Maquina = new DataSet();
        private static DataSet ds_KPI_Uso_Operario = new DataSet();
        private static DataSet ds_KPI_Uso_Cambiador = new DataSet();
        private static DataSet ds_KPI_KGTransformados = new DataSet();

        /*protected GridView dgv_ListaFichasParam;*/

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Selecaño.SelectedValue = Convert.ToString(DateTime.Now.Year);
                //SelecMes.SelectedValue = Convert.ToString(DateTime.Now.Month);
                

                Conexion_KPI conexion = new Conexion_KPI();
                string año = Selecaño.SelectedValue;
                string mes = "";
                string cliente = "";

                ds_KPI_Uso_Maquina = conexion.KPI_Mensual_Uso_Maquina(año, "", cliente, "");
                ds_KPI_Uso_Operario = conexion.KPI_Mensual_Uso_Operario(año, "", cliente);
                ds_KPI_Uso_Cambiador = conexion.KPI_Mensual_Uso_Cambiador(año, "", cliente);
                ds_KPI_KGTransformados = conexion.KPI_Mensual_KGTransformados(año, "", cliente);
                Rellenar_grid();
                Cargar_filtros();
            }

        }

        private void Cargar_filtros()
        {
            try
            {
                Conexion_KPI conexion = new Conexion_KPI();
                DataSet clientes = new DataSet();
                clientes = conexion.devuelve_lista_clientes();

                foreach (DataRow row in clientes.Tables[0].Rows) { Lista_Clientes.Items.Add(row["Cliente"].ToString()); }
                Lista_Clientes.ClearSelection();
                Lista_Clientes.SelectedValue = "";

            }
            catch (Exception)
            { }
        }

        private void Rellenar_grid()
        {
            try
            {
                dgv_KPI_Uso_Maquina.DataSource = ds_KPI_Uso_Maquina;
                dgv_KPI_Uso_Maquina.DataBind();
                    double HorasMaquina = 0;
                    foreach (DataRow dr in ds_KPI_Uso_Maquina.Tables[0].Rows)
                    {
                        HorasMaquina += Convert.ToInt32(dr["TIEMPO"]);
                    }
                    KPIHorasTotalMAQ.InnerText = HorasMaquina.ToString();

                dgv_KPI_KGTransformados.DataSource = ds_KPI_KGTransformados;
                dgv_KPI_KGTransformados.DataBind();
                    double KGSTransform = 0;
                    foreach (DataRow dr in ds_KPI_KGTransformados.Tables[0].Rows)
                    {
                    KGSTransform += Convert.ToInt32(dr["KG"]);
                    }
                    KPIKgstransformados.InnerText = KGSTransform.ToString();

                dgv_KPI_Uso_Operario.DataSource = ds_KPI_Uso_Operario;
                dgv_KPI_Uso_Operario.DataBind();
                    double HorasOP = 0;
                    foreach (DataRow dr in ds_KPI_Uso_Operario.Tables[0].Rows)
                    {
                    HorasOP += Convert.ToInt32(dr["TIEMPOOP"]);
                    }
                    KPIHorasOperario.InnerText = HorasOP.ToString();

                dgv_KPI_Uso_Cambiador.DataSource = ds_KPI_Uso_Cambiador;
                dgv_KPI_Uso_Cambiador.DataBind();
                    double HorasCambio = 0;
                    foreach (DataRow dr in ds_KPI_Uso_Cambiador.Tables[0].Rows)
                    {
                    HorasCambio += Convert.ToInt32(dr["TIEMPOOP"]);
                    }
                    KPIHorasCambiador.InnerText = HorasCambio.ToString();
            }
            catch (Exception)
            {
            }
        }
        public void Cargar_tablas(object sender, EventArgs e)
        {
            try
            {
                
                    string año = Selecaño.SelectedValue;
                    string mes = "";
                    if (SeleMes.SelectedValue.ToString() != "0")
                        {
                        mes = " AND EXTRACT(MONTH FROM C_STARTTIME) = " + SeleMes.SelectedValue;
                        }
                    string cliente = "";
                    if (Lista_Clientes.SelectedValue.ToString() != "-")
                        {
                        cliente = Lista_Clientes.SelectedValue;
                        }

                    Conexion_KPI conexion = new Conexion_KPI();
                    ds_KPI_Uso_Maquina = conexion.KPI_Mensual_Uso_Maquina(año, mes, cliente, "");
                    ds_KPI_Uso_Operario = conexion.KPI_Mensual_Uso_Operario(año, mes, cliente);
                    ds_KPI_Uso_Cambiador = conexion.KPI_Mensual_Uso_Cambiador(año, mes, cliente);
                    ds_KPI_KGTransformados = conexion.KPI_Mensual_KGTransformados(año, mes, cliente);
                    Rellenar_grid();
                
            }
            catch (Exception)
            {

            }
        }

        
    }
}