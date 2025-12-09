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


namespace ThermoWeb.DOCUMENTAL
{
    public partial class GestionDocumentalPendientes : System.Web.UI.Page
    {

        private static DataTable ds_Documentos = new DataTable();
        private static DataTable ds_Documentos_Notas = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Conexion_DOCUMENTAL conexion = new Conexion_DOCUMENTAL();
                ds_Documentos = conexion.Devuelve_Produciendo_Pendientes_Documentos();
                ds_Documentos_Notas = conexion.Devuelve_Notas_Documentales_Operarios("");
                rellenar_grid();
            }

        }

        private void rellenar_grid()
        {
            try
            {
                dgv_PendientesProduciendo.DataSource = ds_Documentos;
                dgv_PendientesProduciendo.DataBind();

                dgv_Notas_Operarios.DataSource = ds_Documentos_Notas;
                dgv_Notas_Operarios.DataBind();
            }
            catch (Exception)
            {

            }
        }

        public void GridViewCommandEventHandler(Object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Conexion_DOCUMENTAL conexion = new Conexion_DOCUMENTAL();
                if (e.CommandName == "ActualizaDOC")
                {
                    string[] RecorteComand = e.CommandArgument.ToString().Split(new char[] { '¬' });
                    string Id = RecorteComand[0].Trim();
                    string Estado = RecorteComand[1].Trim();

                    if (Estado == "False")
                    {
                        conexion.Actualizar_Operarios_Feedback(Id, "1");
                    }
                    else
                    {
                        conexion.Actualizar_Operarios_Feedback(Id, "0");
                    }
                }
                ds_Documentos_Notas = conexion.Devuelve_Notas_Documentales_Operarios("");
                rellenar_grid();

            }
            catch (Exception ex)
            {
            }

        }


    }

}