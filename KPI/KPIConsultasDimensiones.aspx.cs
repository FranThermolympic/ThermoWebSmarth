using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.IO;
using System.Drawing;
using System.Net.Mail;
using System.Net;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Extensions;
using OfficeOpenXml;
using System.ComponentModel;
//using Excel = Microsoft.Office.Interop.Excel;


namespace ThermoWeb.KPI
{
    public partial class KPIConsultasDimensiones : System.Web.UI.Page
    {
        private static DataTable DATATEST = new DataTable();
        private static DataSet ds_KPI_Lista_OperarioXCliente = new DataSet();
        private static DataTable DT_KPI_Lista_Operario_NAV = new DataTable();
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

                InputFechaDesde.Value = DateTime.Now.AddDays(-30).ToString("dd/MM/yyyy");
                InputFechaHasta.Value = DateTime.Now.ToString("dd/MM/yyyy");

                ds_KPI_Lista_OperarioXCliente = conexion.Devuelve_OperarioXCliente(DateTime.Now.AddDays(-30).ToString("dd/MM/yyyy"), DateTime.Now.ToString("dd/MM/yyyy"));
                DT_KPI_Lista_Operario_NAV = conexion.Devuelve_OperarioXEmpresa();
                ds_KPI_Uso_Operario = conexion.KPI_Mensual_Uso_Operario(año, "", cliente);
                ds_KPI_Uso_Cambiador = conexion.KPI_Mensual_Uso_Cambiador(año, "", cliente);

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
                
                DataTable DT_KPI_Lista_OperarioXCliente = ds_KPI_Lista_OperarioXCliente.Tables[0];
                var JoinResult = (from p in DT_KPI_Lista_OperarioXCliente.AsEnumerable()
                                  join t in DT_KPI_Lista_Operario_NAV.AsEnumerable()
                                  on p.Field<string>("OPERARIO") equals t.Field<string>("NUM") into tempJoin
                                  //where t.Field<string>("Cantidad") equals ""
                                  from leftJoin in tempJoin.DefaultIfEmpty()
                                  select new
                                  {
                                      CLIENTE = p.Field<string>("C_CUSTOMER"),
                                      NUMOPERARIO = p.Field<string>("OPERARIO"),
                                      OPERARIO = p.Field<string>("C_OPERATORNAME"),
                                      TIEMPODISP = p.Field<decimal>("TIEMPODISP"),
                                      TIEMPOFUNC = p.Field<decimal>("TIEMPOFUNC"),
                                      PUESTO = leftJoin == null ? "" :  leftJoin.Field<string>("PUESTO"),
                                      EMPRESA = leftJoin == null ? "" : leftJoin.Field<string>("DESCRIPCION"),
                                  }
                  ).ToList();
                dgv_KPI_ClienteXPersona.DataSource = JoinResult;
                dgv_KPI_ClienteXPersona.DataBind();
                
                




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

                /*
                dgv_KPI_Uso_Maquina.DataSource = ds_KPI_Lista_Operario;
                dgv_KPI_Uso_Maquina.DataBind();
                    double HorasMaquina = 0;
                    foreach (DataRow dr in ds_KPI_Lista_Operario.Tables[0].Rows)
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

               
                */
            }
            catch (Exception ex)
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
              
                ds_KPI_Uso_Operario = conexion.KPI_Mensual_Uso_Operario(año, mes, cliente);
                ds_KPI_Uso_Cambiador = conexion.KPI_Mensual_Uso_Cambiador(año, mes, cliente);       
                
                ds_KPI_Lista_OperarioXCliente = conexion.Devuelve_OperarioXCliente(InputFechaDesde.Value, InputFechaHasta.Value);
                DT_KPI_Lista_Operario_NAV = conexion.Devuelve_OperarioXEmpresa();

                Rellenar_grid();

            }
            catch (Exception)
            {

            }
        }
        /*
        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
        */
        protected void ExportToExcel(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                dgv_KPI_ClienteXPersona.AllowPaging = false;
                this.Cargar_tablas(null,null);

                dgv_KPI_ClienteXPersona.HeaderRow.BackColor = System.Drawing.Color.White;
                foreach (TableCell cell in dgv_KPI_ClienteXPersona.HeaderRow.Cells)
                {
                    cell.BackColor = dgv_KPI_ClienteXPersona.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in dgv_KPI_ClienteXPersona.Rows)
                {
                    row.BackColor = System.Drawing.Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = dgv_KPI_ClienteXPersona.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = dgv_KPI_ClienteXPersona.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                dgv_KPI_ClienteXPersona.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }


        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {
            /* Verifies that the control is rendered */
        }


    }
}