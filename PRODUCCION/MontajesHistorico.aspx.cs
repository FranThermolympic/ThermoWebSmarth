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
    public partial class MontajesHistorico : System.Web.UI.Page
    {

        private static DataTable ds_Referencias = new DataTable();
        private static DataTable ds_Estado_Referencias = new DataTable();
        private static DataTable ds_Muro_Calidad = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Conexion_SMARTH SHconexion = new Conexion_SMARTH();
                DataTable DT_ListaProductoSeparador = SHconexion.Devuelve_Productos_NAV_SEPARADOR();
                {
                    for (int i = 0; i <= DT_ListaProductoSeparador.Rows.Count - 1; i++)
                    {
                        DatalistReferencias.InnerHtml = DatalistReferencias.InnerHtml + System.Environment.NewLine +
                            String.Format("<option value='{0}'>", DT_ListaProductoSeparador.Rows[i][0]);
                    }
                }
              
                Rellenar_grid(null, null);
            }
        }

        //CARGA GRIDVIEWS
        public void Rellenar_grid(object sender, EventArgs e)
        {
            try
            {              
                Conexion_SMARTH SHconexion = new Conexion_SMARTH();

                //EVALUO FILTROS
                string referencia = "";
                
                if (selectReferencia.Value != "")
                {
                    string[] RecorteReferencia = selectReferencia.Value.Split(new char[] { '¬' });
                    referencia = " AND Referencia like '" + RecorteReferencia[0].Trim() + "%'";
                    
                }

                string fechainicio = "";
                if (InputFechaDesde.Value != "")
                { 
                 fechainicio = " AND FechaInicio >= '"+InputFechaDesde.Value+"'";
                }
                string fechafin = "";
                if (InputFechaHasta.Value != "")
                {
                    fechafin = " AND FechaInicio <= '"+InputFechaHasta.Value+"'";
                }

                if (InputFechaDesde.Value != "" && InputFechaHasta.Value != "")
                {
                    fechainicio = " AND FechaInicio BETWEEN '"+InputFechaDesde.Value+ "' AND '" + InputFechaHasta.Value + "'";
                    fechafin = "";
                }

                //ENSAMBLADO GRIDVIEWS
                ds_Muro_Calidad = SHconexion.Devuelve_Historico_Montajes(referencia, fechainicio, fechafin);
                dgv_Montaje_Historico.DataSource = ds_Muro_Calidad;
                dgv_Montaje_Historico.DataBind();
       
            }
            catch (Exception ex)
            {

            }
        }
        /*
        protected void GridView_DataBoundHist(object sender, EventArgs e)
        {
            for (int i = 0; i <= dgv_GP12_Historico.Rows.Count - 1; i++)
            {

                Label lblObservaciones = (Label)dgv_GP12_Historico.Rows[i].FindControl("lblObservaciones");
                Label lblNotas = (Label)dgv_GP12_Historico.Rows[i].FindControl("lblNotas");

                Label lblscrap = (Label)dgv_GP12_Historico.Rows[i].FindControl("lblMalas");


                Label lblretrabajo = (Label)dgv_GP12_Historico.Rows[i].FindControl("lblRetrabajadas");

                Label FakeMode = (Label)dgv_GP12_Historico.Rows[i].FindControl("lblFAKE");
                if (FakeMode.Text == "1")
                {
                    lblscrap.Text = "0";
                    lblretrabajo.Text = "0";
                }
                else
                {
                    if (lblscrap.Text != "0")
                    {
                        dgv_GP12_Historico.Rows[i].Cells[7].BackColor = System.Drawing.Color.Red;
                        lblscrap.ForeColor = System.Drawing.Color.White;
                        lblscrap.Font.Bold = true;
                    }
                    if (lblretrabajo.Text != "0")
                    {
                        dgv_GP12_Historico.Rows[i].Cells[6].BackColor = System.Drawing.Color.Orange;
                        lblretrabajo.ForeColor = System.Drawing.Color.Black;
                        lblretrabajo.Font.Bold = true;
                    }
                    if (lblObservaciones.Text != "<br />")
                    {
                        lblObservaciones.Visible = true;
                    }
                    if (lblNotas.Text != "")
                    {
                        lblNotas.Visible = true;
                    }
                }

            }
        }
        */       

    }

}