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


namespace ThermoWeb.CALIDAD
{
    public partial class ListaAlertasCalidad : System.Web.UI.Page
    {

        private static DataSet ds_Referencias = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Conexion_CALIDAD conexion = new Conexion_CALIDAD();

                ds_Referencias = conexion.Devuelve_Listado_NoConformidadesSMARTH(selecaño.SelectedValue);
                CargaListasFiltro();
                rellenar_grid();
            }

        }

        private void rellenar_grid()
        {
            try
            {
                dgv_AreaRechazo.DataSource = ds_Referencias;
                dgv_AreaRechazo.DataBind();
                if (ds_Referencias.Tables[0].Rows.Count > 0)
                {
                    BTN_Aux_Nuevo.Visible = false;
                    dgv_AreaRechazo.Visible = true;
                }
                else
                {
                    dgv_AreaRechazo.Visible = false;
                    BTN_Aux_Nuevo.Visible = true;
                }
            }
            catch (Exception)
            {

            }
        }

        public void CargaListasFiltro()
        {
            try
            {
                Conexion_CALIDAD conexion = new Conexion_CALIDAD();

                DataSet clientes = new DataSet();
                clientes = conexion.devuelve_lista_clientes();
                lista_clientes.Items.Add("-");
                foreach (DataRow row in clientes.Tables[0].Rows) { lista_clientes.Items.Add(row["Cliente"].ToString()); }
                lista_clientes.ClearSelection();
                lista_clientes.SelectedValue = "-";


                DataSet responsable = new DataSet();
                responsable = conexion.Devuelve_setlista_responsablesSMARTH();
                foreach (DataRow row in responsable.Tables[0].Rows) { lista_responsable.Items.Add(row["PAprobado"].ToString()); }
                lista_responsable.ClearSelection();
                lista_responsable.SelectedValue = "-";

                Conexion_SMARTH SHconexion = new Conexion_SMARTH();

                DataTable ListaProductos = SHconexion.Devuelve_listado_PRODUCTOS();
                {
                    for (int i = 0; i <= ListaProductos.Rows.Count - 1; i++)
                    {
                        DatalistReferencias.InnerHtml = DatalistReferencias.InnerHtml + System.Environment.NewLine +
                            String.Format("<option value='{0}'>", ListaProductos.Rows[i][0]);
                    }
                }

                DataTable ListaMoldes = SHconexion.Devuelve_listado_MOLDES();
                {
                    for (int i = 0; i <= ListaMoldes.Rows.Count - 1; i++)
                    {
                        DatalistMoldes.InnerHtml = DatalistMoldes.InnerHtml + System.Environment.NewLine +
                            String.Format("<option value='{0}'>", ListaMoldes.Rows[i][0]);
                    }
                }



            }
            catch (Exception)
            {

            }
        }
        /*
        public void ImportardeBMS(object sender, EventArgs e)
        {

            try
            {
                Conexion_CALIDAD conexion = new Conexion_CALIDAD();
                conexion.leer_productosBMS();
                conexion.leer_operariosNAV();
                conexion.InsertaProductosReferenciaEstados();
                conexion.InsertaProductosTablaDocumentos();
                ds_Referencias = conexion.leer_ReferenciaEstadosActivas();
                rellenar_grid();
            }
            catch (Exception)
            {

            }


        }
        */
        public void CargarFiltrados(object sender, EventArgs e)
        {
            Conexion_CALIDAD conexion = new Conexion_CALIDAD();
            string estado = "";
            if (lista_estado.SelectedValue == "1") //Cerrados
            {
                estado = " AND ((D3CIERRE IS NOT NULL AND D3CIERRE <> '') AND(D6CIERRE IS NOT NULL AND D6CIERRE <> '') AND(D8CIERRE IS NOT NULL AND D8CIERRE <> ''))";
            }
            else if (lista_estado.SelectedValue == "2") //En curso
            {
                estado = " AND((D3CIERRE IS NULL OR D3CIERRE = '') OR ((D6CIERRE IS NULL OR D6CIERRE = '') AND CHECKD6 = 1) OR(D8CIERRE IS NULL OR D8CIERRE = '') AND CHECKD8 = 1)";
            }
            else if (lista_estado.SelectedValue == "3") // Vencidos
            {
                estado = " AND ((D3 < SYSDATETIME() AND (D3CIERRE IS NULL OR D3CIERRE = ''))" +
                         " OR(CASE WHEN D6 = 'N/A' THEN '01/01/2900 0:00:00' WHEN D6 = '' THEN '01/01/1900 0:00:00' ELSE cast(d6 as datetime) end < SYSDATETIME() AND(D6CIERRE IS NULL OR D6CIERRE = '') AND CHECKD6 = 1)" +
                         " OR(CASE WHEN D8 = 'N/A' THEN '01/01/2900 0:00:00' WHEN D8 = '' THEN '01/01/1900 0:00:00' ELSE cast(d8 as datetime) end < SYSDATETIME() AND(D8CIERRE IS NULL OR D8CIERRE = '')) AND CHECKD8 = 1)";
            }
            else
            {
                estado = "";
            }

            string cliente = "";
            if (lista_clientes.SelectedValue.ToString() != "-")
            {
                cliente = " AND P.CLIENTE LIKE '" + lista_clientes.SelectedValue.ToString() + "%'";
            }
            else
            {
                cliente = "";
            }

            string piloto = "";
            if (lista_responsable.SelectedValue.ToString() != "-")
            {
                piloto = " AND PilotoIngenieria LIKE '" + conexion.Devuelve_IDlista_responsablesSMARTH(lista_responsable.SelectedValue.ToString()) + "'";
            }
            else
            {
                piloto = "";
            }

            string escalado = "";
            if (NivelAlerta.SelectedValue != "0")
            {
                escalado = " AND EscaladoNoConformidad = " + NivelAlerta.SelectedValue + "";
            }
            else
            {
                escalado = "";
            }

            string tipoNC = "";
            if (TipoAlerta.SelectedValue != "0")
            {
                tipoNC = " AND TipoNoConformidad = " + TipoAlerta.SelectedValue + "";
            }
            else
            {
                tipoNC = "";
            }
            string sector = "";          
                switch (SelecSector.SelectedValue)
                {
                    case "-":
                        break;
                    case "Automoción":
                    sector = " AND Sector LIKE 'AUTOMOCION%'";
                        break;
                    case "Línea Blanca":
                    sector = " AND Sector LIKE 'LINEA BLANCA%'";
                        break;
                    case "Menaje":
                    sector = " AND Sector LIKE 'MENAJE%'";
                        break;
                    case "Otros":
                    sector = " AND Sector LIKE 'OTROS%'";
                        break;
                }

            string producto = "";
            if (selectReferencia.Value != "")
            {
                string[] RecorteReferencia = selectReferencia.Value.Split(new char[] { '¬' });
                producto = " AND P.Referencia like '" + RecorteReferencia[0].Trim() + "%'";
            }

            string molde = "";
            if (selectMolde.Value != "")
            {
                string[] RecorteMolde = selectMolde.Value.Split(new char[] { '¬' });
                producto = " AND P.Molde like '" + RecorteMolde[0].Trim() + "%'";
            }


            ds_Referencias = conexion.Devuelve_Listado_NoConformidadesFiltradosSMARTH(estado, cliente, piloto, escalado, tipoNC, selecaño.SelectedValue, sector, producto, molde);
            rellenar_grid();


        }

        public void CargarTodas(object sender, EventArgs e)
        {
            Conexion_CALIDAD conexion = new Conexion_CALIDAD();
            lista_responsable.SelectedValue = "-";
            NivelAlerta.SelectedValue = "-";
            TipoAlerta.SelectedValue = "-";
            lista_estado.SelectedValue = "-";
            lista_clientes.SelectedValue = "-";
            selectReferencia.Value = "";
            selectMolde.Value = "";

            string estado = "";
            string cliente = "";
            string escalado = "";
            string tipoNC = "";
            string piloto = "";
            string sector = "";
            string molde = "";
            string producto = "";
            ds_Referencias = conexion.Devuelve_Listado_NoConformidadesFiltradosSMARTH(estado, cliente, piloto, escalado, tipoNC, selecaño.SelectedValue,sector, producto, molde);
            rellenar_grid();

        }

        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if ((e.Row.RowState == DataControlRowState.Normal) || (e.Row.RowState == DataControlRowState.Alternate))
                    {
                        Label DEFRepetitivo = (Label)e.Row.FindControl("lblReiteraDefecto");
                        Label ProdRepetitivo = (Label)e.Row.FindControl("lblReiteraProducto");
                        HtmlButton BTNDEFRepetitivo = (HtmlButton)e.Row.FindControl("btnReiteraDefecto");
                        HtmlButton BTNProdRepetitivo = (HtmlButton)e.Row.FindControl("btnReiteraProducto");

                        if (Convert.ToInt32(DEFRepetitivo.Text) == 1)
                        {
                            BTNDEFRepetitivo.Visible = true;
                            //DEFRepetitivo.Text = " Def. recurrente";
                            //DEFRepetitivo.Visible = true;
                        }
                        if (Convert.ToInt32(ProdRepetitivo.Text) == 1)
                        {
                            BTNProdRepetitivo.Visible = true;
                            //ProdRepetitivo.Text = " Prod. recurrente";
                            //ProdRepetitivo.Visible = true;
                        }



                        Label PrevD3 = (Label)e.Row.FindControl("lblFechaD3");
                        Label RealD3 = (Label)e.Row.FindControl("lblFechaD3ent");
                        HtmlButton ColorD3 = (HtmlButton)e.Row.FindControl("D3COLOR");

                        if (RealD3.Text == "" && DateTime.Now > DateTime.Parse(PrevD3.Text)) //CASO NOK ROJO
                        {
                            ColorD3.Style.Clear();
                            ColorD3.Style.Add("color", "White");
                            ColorD3.Style.Add("background-color", "Red");
                            ColorD3.Style.Add("font-weight", "bold");
                            ColorD3.Style.Add("font-size", "small");
                        }
                        else if (RealD3.Text == "" && DateTime.Now < DateTime.Parse(PrevD3.Text)) //CASO A TIEMPO GRIS
                        {
                            ColorD3.Style.Clear();
                            ColorD3.Style.Add("color", "White");
                            ColorD3.Style.Add("background-color", "#999999");
                            ColorD3.Style.Add("font-weight", "bold");
                            ColorD3.Style.Add("font-size", "small");
                        }
                        else //CASO OK
                        {
                            ColorD3.Style.Clear();
                            ColorD3.Style.Add("color", "White");
                            ColorD3.Style.Add("background-color", "#33cc33");
                            ColorD3.Style.Add("font-weight", "bold");
                            ColorD3.Style.Add("font-size", "small");

                        }

                        Label PrevD6 = (Label)e.Row.FindControl("lblFechaD6");
                        Label RealD6 = (Label)e.Row.FindControl("lblFechaD6ent");
                        HtmlButton ColorD6 = (HtmlButton)e.Row.FindControl("D6COLOR");

                        if (RealD6.Text == "" && DateTime.Now > DateTime.Parse(PrevD6.Text)) //CASO NOK ROJO
                        {
                            ColorD6.Style.Clear();
                            ColorD6.Style.Add("color", "White");
                            ColorD6.Style.Add("background-color", "Red");
                            ColorD6.Style.Add("font-weight", "bold");
                            ColorD6.Style.Add("font-size", "small");
                            //ColorD6.ForeColor = System.Drawing.Color.White;
                            //ColorD6.BackColor = System.Drawing.Color.Red;
                        }
                        else if (RealD6.Text == "" && DateTime.Now < DateTime.Parse(PrevD6.Text)) //CASO A TIEMPO GRIS
                        {
                            ColorD6.Style.Clear();
                            ColorD6.Style.Add("color", "White");
                            ColorD6.Style.Add("background-color", "#999999");
                            ColorD6.Style.Add("font-weight", "bold");
                            ColorD6.Style.Add("font-size", "small");
                            //ColorD6.ForeColor = System.Drawing.Color.White;
                            //ColorD6.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                            // ColorD6.BorderColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        }
                        else //CASO OK
                        {
                            ColorD6.Style.Clear();
                            ColorD6.Style.Add("color", "White");
                            ColorD6.Style.Add("background-color", "#33cc33");
                            ColorD6.Style.Add("font-weight", "bold");
                            ColorD6.Style.Add("font-size", "small");

                            //ColorD6.ForeColor = System.Drawing.Color.White;
                            //ColorD6.BackColor = System.Drawing.ColorTranslator.FromHtml("#33cc33");
                            //ColorD6.BorderColor = System.Drawing.ColorTranslator.FromHtml("#33cc33");
                        }

                        Label PrevD8 = (Label)e.Row.FindControl("lblFechaD8");
                        Label RealD8 = (Label)e.Row.FindControl("lblFechaD8ent");
                        HtmlButton ColorD8 = (HtmlButton)e.Row.FindControl("D8COLOR");

                        if (RealD8.Text == "" && DateTime.Now > DateTime.Parse(PrevD8.Text)) //CASO NOK ROJO
                        {
                            ColorD8.Style.Clear();
                            ColorD8.Style.Add("color", "White");
                            ColorD8.Style.Add("background-color", "Red");
                            ColorD8.Style.Add("font-weight", "bold");
                            ColorD8.Style.Add("font-size", "small");
                            // ColorD8.BorderColor = System.Drawing.Color.Red;
                        }
                        else if (RealD8.Text == "" && DateTime.Now < DateTime.Parse(PrevD8.Text)) //CASO A TIEMPO GRIS
                        {
                            ColorD8.Style.Clear();
                            ColorD8.Style.Add("color", "White");
                            ColorD8.Style.Add("background-color", "#999999");
                            ColorD8.Style.Add("font-weight", "bold");
                            ColorD8.Style.Add("font-size", "small");
                        }
                        else //CASO OK
                        {
                            ColorD8.Style.Clear();
                            ColorD8.Style.Add("color", "White");
                            ColorD8.Style.Add("background-color", "#33cc33");
                            ColorD8.Style.Add("font-weight", "bold");
                            ColorD8.Style.Add("font-size", "small");
                        }




                    }

                    if (e.Row.RowState.HasFlag(DataControlRowState.Edit))

                    {
                        DropDownList escalado = (DropDownList)e.Row.FindControl("NivelAlerta");
                        DataRowView dr = e.Row.DataItem as DataRowView;
                        escalado.SelectedIndex = Convert.ToInt32(dr["EscaladoNoConformidad"]);

                        TextBox EditD6 = (TextBox)e.Row.FindControl("TXTFechaD6");
                        CheckBox D6Checkbox = (CheckBox)e.Row.FindControl("TXTD6TRANS");
                        if (EditD6.Text == "N/A")
                        {
                            D6Checkbox.Checked = false;
                        }

                        TextBox EditD8 = (TextBox)e.Row.FindControl("TXTFechaD8");
                        CheckBox D8Checkbox = (CheckBox)e.Row.FindControl("TXTD8TRANS");
                        if (EditD8.Text == "N/A")
                        {
                            D8Checkbox.Checked = false;
                        }

                    }
                }

            }
            catch (Exception)
            {
            }
        }

        public void GridViewCommandEventHandler(Object sender, GridViewCommandEventArgs e)
        {
            try
            {
              
                if (e.CommandName == "Redirect")
                {
                    Response.Redirect("Alertas_Calidad.aspx?NOCONFORMIDAD=" + e.CommandArgument.ToString());
                }
            }
            catch (Exception)
            { }

        }
        
    }

}