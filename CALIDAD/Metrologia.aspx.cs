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
    public partial class MetrologiaCalibraciones : System.Web.UI.Page
    {

        private static DataTable ds_EquiposMedicion = new DataTable();
        private static DataTable ds_Calibraciones = new DataTable();
        private static DataTable ds_Referencias = new DataTable();
        private static DataTable ds_Planificacion_Calibraciones = new DataTable();

        private static DataTable DTPlanicalibCALIB = new DataTable();
      

        private static int vencenpront = 0;
        private static int vencidos = 0;
        private static int SinCalibrar = 0;

        private static int OrdenEquipo = 0;
        private static int OrdenDescripcion = 0;
        private static int OrdenTipoEquipo = 0;
        private static int OrdenPendCalibrar = 0;
        private static int OrdenPendMSA = 0;
        private static int OrdenUltimaCalib = 0;
        private static string orderby = " ORDER BY NumEquipo DESC";
        private static string ETIFiltroEquipo = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargaListasFiltro();
                Rellenar_grid(null, null);
            }
        }

        public void CargaListasFiltro()
        {
            try
            {
                Conexion_SMARTH SHconexion = new Conexion_SMARTH();

                DataTable ListaEquipos = SHconexion.Devuelve_Listado_Equipos_Metrologia_SEPARADOR();                
                {
                    for (int i = 0; i <= ListaEquipos.Rows.Count - 1; i++)
                    {
                        DatalistEquipos.InnerHtml = DatalistEquipos.InnerHtml + System.Environment.NewLine +
                            String.Format("<option value='{0}'>", ListaEquipos.Rows[i][0]);
                    }
                }

                DataTable ListaUbicaciones = SHconexion.Devuelve_Listado_Ubicaciones_Metrologia();
                {
                    for (int i = 0; i <= ListaUbicaciones.Rows.Count - 1; i++)
                    {
                        DatalistUbicacion.InnerHtml = DatalistUbicacion.InnerHtml + System.Environment.NewLine +
                            String.Format("<option value='{0}'>", ListaUbicaciones.Rows[i][0]);
                    }
                }

                DataTable Auxiliares = SHconexion.Devuelve_Auxiliares_Metrologia();
                    Auxiliares.DefaultView.RowFilter = "Tipo_equipo IS NOT NULL";
                    Auxiliares.DefaultView.Sort = "Tipo_equipo asc";
                    DataTable TipoEquipo = Auxiliares.DefaultView.ToTable();
                    Drop_tipos_equipos_filtro.Items.Clear();
                    foreach (DataRow row in TipoEquipo.Rows)
                    {
                    Drop_tipos_equipos_filtro.Items.Add(row["Tipo_equipo"].ToString());
                    DropTipoEquipo.Items.Add(row["Tipo_equipo"].ToString());
                    FiltroTipoPendCalibracion.Items.Add(row["Tipo_equipo"].ToString());
                    DropNuevoTipoEquipo.Items.Add(row["Tipo_equipo"].ToString());
                }

                    Auxiliares.DefaultView.RowFilter = "Departamento IS NOT NULL";
                    Auxiliares.DefaultView.Sort = "Departamento asc";
                    DataTable Departamento = Auxiliares.DefaultView.ToTable();
                    DropDepartamento.Items.Clear();
                    foreach (DataRow row in Departamento.Rows)
                    {
                    DropDepartamento.Items.Add(row["Departamento"].ToString());
                }

                DataTable Propietarios = SHconexion.Devuelve_Listado_Propietarios_METROLOGIA();
                {
                    for (int i = 0; i <= Propietarios.Rows.Count - 1; i++)
                    {
                        DatalistPropietario.InnerHtml = DatalistPropietario.InnerHtml + System.Environment.NewLine +
                            String.Format("<option value='{0}'>", Propietarios.Rows[i][0]);
                    }
                }

                DataTable Fabricante = SHconexion.Devuelve_Listado_Fabricantes_METROLOGIA();
                {
                    for (int i = 0; i <= Fabricante.Rows.Count - 1; i++)
                    {
                        DatalistFabricante.InnerHtml = DatalistFabricante.InnerHtml + System.Environment.NewLine +
                            String.Format("<option value='{0}'>", Fabricante.Rows[i][0]);
                    }
                }






                DataTable clientes = SHconexion.Devuelve_listado_clientes();
                        /*
                        lista_clientes.Items.Add("-");
                        foreach (DataRow row in clientes.Rows) { lista_clientes.Items.Add(row["Cliente"].ToString()); }
                        lista_clientes.ClearSelection();
                        lista_clientes.SelectedValue = "";
                        */

                DataSet Personal = SHconexion.Devuelve_mandos_intermedios_SMARTH();
                //incluir filtro  Responsable.Tables[0].DefaultView.RowFilter = "Departamento = 'INGENIERIA'  OR Departamento = '-'";
                //DataTable DTValidadores = Personal.Tables[0].DefaultView.ToTable();
                DropAprobadoMSA.Items.Clear();
                foreach (DataRow row in Personal.Tables[0].Rows)
                    { 
                    DropAprobadoMSA.Items.Add(row["Nombre"].ToString());
                    DropAprobadoCalibracion.Items.Add(row["Nombre"].ToString());
                    dropAprobadoDOCAUX.Items.Add(row["Nombre"].ToString());
                }
                
                DataTable ListaProductos = SHconexion.Devuelve_listado_PRODUCTOS();                       
                {
                    for (int i = 0; i <= ListaProductos.Rows.Count - 1; i++)
                    {
                            DatalistProductos.InnerHtml = DatalistProductos.InnerHtml + System.Environment.NewLine +
                            String.Format("<option value='{0}'>", ListaProductos.Rows[i][0]);

                        DatalistReferencias.InnerHtml = DatalistReferencias.InnerHtml + System.Environment.NewLine +
                           String.Format("<option value='{0}'>", ListaProductos.Rows[i][0]);
                    }
                }
                      
               
                DataSet Responsable = Personal; //AbiertoPorV2
                /*
                    Responsable.Tables[0].DefaultView.RowFilter = "Departamento = 'INGENIERIA'  OR Departamento = '-'";
                    DataTable DTIngenieria = Responsable.Tables[0].DefaultView.ToTable();
                    lista_responsable.Items.Clear();
                    foreach (DataRow row in DTIngenieria.Rows)
                    {
                        lista_responsable.Items.Add(row["Nombre"].ToString());
                    }
                */

            }
            catch (Exception ex)
            {

            }
        }

        public void Ordenar_lineas(object sender, EventArgs e)
        {
            HtmlButton button = (HtmlButton)sender;
            string name = button.ID;
            switch (name)
            {
                case "BTNOrdenaEquipo":
                    if (OrdenEquipo == 0)
                    {
                        orderby = " ORDER BY NumEquipo ASC";
                        OrdenEquipo = 1;
                    }
                    else
                    {
                        
                        orderby = " ORDER BY NumEquipo DESC";
                        OrdenEquipo = 0;
                    }
                    break;

                case "BTNOrdenaDescripcion":
                    if (OrdenDescripcion == 0)
                    {
                        orderby = " ORDER BY Nombre ASC";
                        OrdenDescripcion = 1;
                    }
                    else
                    {

                        orderby = " ORDER BY Nombre DESC";
                        OrdenDescripcion = 0;
                    }
                    break;

                    

               case "BTNOrdenaTipoEquipo":
                    if (OrdenTipoEquipo == 0)
                    {
                        orderby = " ORDER BY Clase ASC";
                        OrdenTipoEquipo = 1;
                    }
                    else
                    {

                        orderby = " ORDER BY Clase DESC";
                        OrdenTipoEquipo = 0;
                    }
                    break;

                case "BTNOrdenaVence":
                    if (OrdenPendCalibrar == 0)
                    {
                        orderby = " ORDER BY VenceCalibracion ASC";
                        OrdenPendCalibrar = 1;
                    }
                    else
                    {

                        orderby = " ORDER BY VenceCalibracion DESC";
                        OrdenPendCalibrar = 0;
                    }
                    break;
                case "BTNOrdenaCalibracion":
                    if (OrdenUltimaCalib == 0)
                    {
                        orderby = " ORDER BY FechaDoc ASC";
                        OrdenUltimaCalib = 1;
                    }
                    else
                    {

                        orderby = " ORDER BY FechaDoc DESC";
                        OrdenUltimaCalib = 0;
                    }
                    break;
                case "BTNOrdenaMSA":
                    if (OrdenPendCalibrar == 0)
                    {
                        orderby = " ORDER BY EstadoMSA ASC";
                        OrdenPendMSA = 1;
                    }
                    else
                    {

                        orderby = " ORDER BY EstadoMSA DESC";
                        OrdenPendMSA = 0;
                    }
                    break;       
            }
            Rellenar_grid(null, null);
        }
        //ACTUALIZAR EQUIPO

        public void Actualizar_equipo(object sender, EventArgs e)
        {
            Conexion_SMARTH conexion = new Conexion_SMARTH();
            conexion.Actualizar_equipo_Metrologia(lblNumEquipo.InnerText, InputNombreEquipo.Value, DropTipoEquipo.SelectedValue.ToString(), InputNumSerie.Value, InputRango.Value, InputEscala.Value, DropDepartamento.SelectedValue.ToString(), InputPropietario.Value, InputFabricante.Value, InputFechaAlta.Value, Convert.ToInt32(DropFrecuenciaCalibracion.SelectedValue.ToString()), DropTipoCalibracion.SelectedValue, InputMedioCalibracion.Value, Convert.ToInt32(DropCriterioAceptacion.SelectedIndex), DropMSA.SelectedValue.ToString(), "", "", UbicacionEquipo.Value, Convert.ToInt32(DropEstado.SelectedValue), InputNotasEquipo.Value, tbEquipoAlternativo.Value, tbNotasAlternativo.Value);
            Rellenar_grid(null, null);
        }

        //CARGA GRIDVIEWS
        public void Rellenar_grid(object sender, EventArgs e)
        {
            try
            {              
                Conexion_SMARTH SHconexion = new Conexion_SMARTH();
               
                        //EVALUO FILTROS
                string referencia = "";
                string referenciaestado = "";
                if (selectReferencia.Value != "")
                {
                    string[] RecorteReferencia = selectReferencia.Value.Split(new char[] { '¬' });
                    referencia = " AND Referencia like '" + RecorteReferencia[0].Trim() + "%'";
                    referenciaestado = " AND P.Referencia like '" + RecorteReferencia[0].Trim() + "%'";
                    //CheckEnRevision.Checked = false;
                }

                string equipo = "";
                if (selectEquipo.Value != "")
                {
                    string[] RecorteEquipo = selectEquipo.Value.Split(new char[] { '¬' });
                    equipo = " AND NumEquipo like '" + RecorteEquipo[0].Trim() + "%'";
                    
                    //CheckEnRevision.Checked = false;
                }

                string tipoequipo = "";
                if (Drop_tipos_equipos_filtro.SelectedValue != "-")
                {
                    //string[] RecorteEquipo = selectEquipo.Value.Split(new char[] { '¬' });
                    tipoequipo = " AND Clase like '" + Drop_tipos_equipos_filtro.SelectedValue + "%'";

                    //CheckEnRevision.Checked = false;
                }

                string estadoequipo = " AND EstadoEquipo < 2";
                if (!SwitchActivas.Checked)
                {
                    estadoequipo = "";
                }

               
                //ENSAMBLADO GRIDVIEWS
                ds_EquiposMedicion = SHconexion.Devuelve_Listado_Equipos_Metrologia(equipo,tipoequipo,"",estadoequipo,orderby);
                dgv_Listado_Equipos.DataSource = ds_EquiposMedicion;
                dgv_Listado_Equipos.DataBind();

                DataTable ds_EquiposMedicionTareas = SHconexion.Devuelve_Listado_Equipos_Metrologia("", "", "", "", orderby);

                ds_EquiposMedicionTareas.DefaultView.RowFilter = "Imagen = '../SMARTH_docs/METROLOGIA/sin_imagen.jpg' AND EstadoEquipo< 2";
                GridEquiposSinImagen.DataSource = ds_EquiposMedicionTareas.DefaultView.ToTable();
                BadgePendImagen.InnerText = ds_EquiposMedicionTareas.DefaultView.ToTable().Rows.Count.ToString();
                GridEquiposSinImagen.DataBind();

                ds_EquiposMedicionTareas.DefaultView.RowFilter = "(Ubicacion = '' or Ubicacion is null) AND EstadoEquipo< 2";
                GridSinUbicar.DataSource = ds_EquiposMedicionTareas.DefaultView.ToTable();
                BadgePendUbicar.InnerText = ds_EquiposMedicionTareas.DefaultView.ToTable().Rows.Count.ToString();
                GridSinUbicar.DataBind();


                ds_EquiposMedicionTareas.DefaultView.RowFilter = "EstadoMSA = 'Pendiente' AND EstadoEquipo< 2";
                GridPendientesMSA.DataSource = ds_EquiposMedicionTareas.DefaultView.ToTable();
                BadgePendMSA.InnerText = ds_EquiposMedicionTareas.DefaultView.ToTable().Rows.Count.ToString();
                GridPendientesMSA.DataBind();

                ds_EquiposMedicionTareas.DefaultView.RowFilter = "(Alternativo = '' or Alternativo is null) AND EstadoEquipo< 2";
                GridSinEquiposAlternativos.DataSource = ds_EquiposMedicionTareas.DefaultView.ToTable();
                BadgePendAlternativos.InnerText = ds_EquiposMedicionTareas.DefaultView.ToTable().Rows.Count.ToString();
                GridSinEquiposAlternativos.DataBind();



                string tipoequipocalib = "";
                if (FiltroTipoPendCalibracion.SelectedValue != "-")
                {
                    tipoequipocalib = " AND Clase like '" + FiltroTipoPendCalibracion.SelectedValue + "%'";
                }
                
                if (FiltroTipoEquipoAgrupado.SelectedValue != "0")
                {
                    tipoequipocalib = tipoequipocalib + " AND MSA = 2";
                }

                ds_Planificacion_Calibraciones = SHconexion.Devuelve_Pendientes_Metrologia_Calibraciones(tipoequipocalib);

                string FitroVencido = "";
                //string FiltVecenPronto = "VenceCalibracion > '"+DateTime.Now+"' AND VenceCalibracion < '" + DateTime.Now.AddDays(+45) + "'";
                //string FiltSinCalibrar = "VenceCalibracion = '01/01/2000'";
                //string FiltVencido = "VenceCalibracion > '01/01/2000' AND VenceCalibracion < '" + DateTime.Now + "'";
                switch (FiltroEstadoPendCalibracion.SelectedValue)
                {
                    case "0":
                        FitroVencido = "VenceCalibracion < '" + DateTime.Now.AddDays(+45) + "' AND EstadoEquipo <> 0";
                    break;
                    case "1":
                        FitroVencido = "VenceCalibracion > '" + DateTime.Now + "' AND VenceCalibracion < '" + DateTime.Now.AddDays(+45) + "'  AND EstadoEquipo <> 0";
                        break;
                    case "2":
                        FitroVencido = "VenceCalibracion > '01/01/2000' AND VenceCalibracion < '" + DateTime.Now + "'  AND EstadoEquipo <> 0";
                        break;
                    case "3":
                        FitroVencido = "VenceCalibracion = '01/01/2000'  AND EstadoEquipo <> 0";
                        break;
                }
                ds_Planificacion_Calibraciones.DefaultView.RowFilter = FitroVencido;
                DTPlanicalibCALIB = ds_Planificacion_Calibraciones.DefaultView.ToTable();
                GridPendientesCalibracion.DataSource = DTPlanicalibCALIB;
                GridPendientesCalibracion.DataBind();

                DataTable KPIEquiposMedicion = SHconexion.Devuelve_Listado_Equipos_Metrologia("", "", "", "", orderby);
                //Total equipos
                string a0 = KPIEquiposMedicion.Rows.Count.ToString();
                //Activos
                    KPIEquiposMedicion.DefaultView.RowFilter = "Eliminar<> 'true' and EstadoEquipo = 1";
                    lblKPIEquipoActivo.Text = KPIEquiposMedicion.DefaultView.ToTable().Rows.Count.ToString();
                //Retirados
                    KPIEquiposMedicion.DefaultView.RowFilter = "Eliminar<> 'true' and EstadoEquipo = 2";
                    string a2 = KPIEquiposMedicion.DefaultView.ToTable().Rows.Count.ToString();
                //Inactivos
                    KPIEquiposMedicion.DefaultView.RowFilter = "Eliminar<> 'true' and EstadoEquipo = 0";
                    string a3 = KPIEquiposMedicion.DefaultView.ToTable().Rows.Count.ToString();
                //Vencenpronto
                    KPIEquiposMedicion.DefaultView.RowFilter = "VenceCalibracion > '" + DateTime.Now + "' AND VenceCalibracion < '" + DateTime.Now.AddDays(+45) + "'  AND Eliminar<> 'true' and EstadoEquipo = 1 and TipoCalibracion <> 'N/A'";
                    lblKPIVencenPronto.Text = KPIEquiposMedicion.DefaultView.ToTable().Rows.Count.ToString();
                //Vencidos
                    KPIEquiposMedicion.DefaultView.RowFilter = "VenceCalibracion > '01/01/2000' AND VenceCalibracion < '" + DateTime.Now + "' AND Eliminar<> 'true' and EstadoEquipo = 1 and TipoCalibracion <> 'N/A'";
                    lblKPIVencidos.Text = KPIEquiposMedicion.DefaultView.ToTable().Rows.Count.ToString();
                //Sin Calibrar
                    KPIEquiposMedicion.DefaultView.RowFilter = "VenceCalibracion = '01/01/2000' AND Eliminar<> 'true' and EstadoEquipo = 1 and TipoCalibracion <> 'N/A'";
                    lblKPISinCalibrar.Text = KPIEquiposMedicion.DefaultView.ToTable().Rows.Count.ToString();
                  

                HtmlButton button = (HtmlButton)sender;

                if (button.ID == "BTNFiltroCalib")
                {
                    lkb_Sort_Click("BTNFiltroCalib");
                }
                else
                {
                    lkb_Sort_Click("ListadoEquipos");
                }
 

            }
            catch (Exception ex)
            {

            }
        }

        protected void GridView_DataBoundHist(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label EstadoEquipo = (Label)e.Row.FindControl("lblEstadoEquipo");
                Label AlertaEstadoEquipoInactivo = (Label)e.Row.FindControl("lblAlertaInactivo");
                Label AlertaEstadoEquipoObsoleto = (Label)e.Row.FindControl("lblAlertaObsoleto");
                Label FrecuenciaCalibracion = (Label)e.Row.FindControl("lblFrecuencia");
                Label lblTipoCalibracion = (Label)e.Row.FindControl("lblTipoCalibracion");
                Label lblVencido45dias = (Label)e.Row.FindControl("lblFechaProxima");
                Label lblFechaVENC = (Label)e.Row.FindControl("lblFechaVencida");
                Label lblProxVencido = (Label)e.Row.FindControl("lblBoundCalibracion");
                Label lblNACAlibracion = (Label)e.Row.FindControl("lblNACAlibracion");
                Label lblUltimaCalibracion = (Label)e.Row.FindControl("lblUltimaCalibracion");
                Label lblProximaCalibracion = (Label)e.Row.FindControl("lblProximaCalibracion");



                if (lblProxVencido.Text != "")
                {
                    if (Convert.ToDateTime(lblProxVencido.Text) < DateTime.Now)
                    {
                        lblFechaVENC.Visible = true;
                    }

                    if (Convert.ToDateTime(lblProxVencido.Text) < DateTime.Now.AddDays(+45) && lblFechaVENC.Visible == false)
                    {
                        lblVencido45dias.Visible = true;
                    }
                }
                if (lblTipoCalibracion.Text != "N/A" && EstadoEquipo.Text == "0")
                {
                    AlertaEstadoEquipoInactivo.Visible = true;
                    FrecuenciaCalibracion.Visible = false;
                    lblVencido45dias.Visible = false;
                    lblFechaVENC.Visible = false;
                    lblNACAlibracion.Visible = false;
                    lblUltimaCalibracion.Visible = false;
                    lblProximaCalibracion.Visible = false;
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#e7defa");
                }
                if (lblTipoCalibracion.Text != "N/A" && EstadoEquipo.Text == "2")
                {
                    AlertaEstadoEquipoObsoleto.Visible = true;
                    FrecuenciaCalibracion.Visible = false;
                    lblVencido45dias.Visible = false;
                    lblFechaVENC.Visible = false;
                    lblNACAlibracion.Visible = false;
                    lblUltimaCalibracion.Visible = false;
                    lblProximaCalibracion.Visible = false;
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#fcca97");
                }
                if (lblTipoCalibracion.Text == "N/A")
                {
                    AlertaEstadoEquipoObsoleto.Visible = false;
                    FrecuenciaCalibracion.Visible = false;
                    lblVencido45dias.Visible = false;
                    lblFechaVENC.Visible = false;
                    lblNACAlibracion.Visible = true;
                    lblUltimaCalibracion.Visible = false;
                    lblProximaCalibracion.Visible = false;
                }

            }

                
            /*
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
        */
        }

        protected void GridView_DataBoundCalib(object sender, GridViewRowEventArgs e)
        {
            

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label EstadoEquipo = (Label)e.Row.FindControl("lblEstadoEquipo");
                Label AlertaEstadoEquipoInactivo = (Label)e.Row.FindControl("lblAlertaInactivo");
                Label AlertaEstadoEquipoObsoleto = (Label)e.Row.FindControl("lblAlertaObsoleto");
                Label lblTipoCalibracion = (Label)e.Row.FindControl("lblTipoCalibracion");
                Label lblVencido45dias = (Label)e.Row.FindControl("lblFechaProxima");
                Label lblFechaVENC = (Label)e.Row.FindControl("lblFechaVencida");
                Label lblProxVencido = (Label)e.Row.FindControl("lblBoundCalibracion");
                Label lblNACAlibracion = (Label)e.Row.FindControl("lblNACAlibracion");
                Label lblUltimaCalibracion = (Label)e.Row.FindControl("lblUltimaCalibracion");
                Label lblProximaCalibracion = (Label)e.Row.FindControl("lblProximaCalibracion");



                if (lblProxVencido.Text != "")
                {
                    if (Convert.ToDateTime(lblProxVencido.Text) < DateTime.Now && Convert.ToDateTime(lblProxVencido.Text) > Convert.ToDateTime("01/01/2000"))
                    {
                        lblFechaVENC.Visible = true;
                       // vencidos += 1;
                    }

                    if (Convert.ToDateTime(lblProxVencido.Text) < DateTime.Now.AddDays(+45) && lblFechaVENC.Visible == false && Convert.ToDateTime(lblProxVencido.Text) > Convert.ToDateTime("01/01/2000"))
                    {
                        lblVencido45dias.Visible = true;
                        //vencenpront += 1;

                    }
                }
                if (lblTipoCalibracion.Text != "N/A" && EstadoEquipo.Text == "0")
                {
                    AlertaEstadoEquipoInactivo.Visible = true;
                   // vencidos -= 1;
                    lblVencido45dias.Visible = false;
                    lblFechaVENC.Visible = false;
                    lblNACAlibracion.Visible = false;
                    lblUltimaCalibracion.Visible = false;
                    lblProximaCalibracion.Visible = false;
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#e7defa");
                }
                if (lblTipoCalibracion.Text != "N/A" && EstadoEquipo.Text == "2")
                {
                    AlertaEstadoEquipoObsoleto.Visible = true;
                    lblVencido45dias.Visible = false;
                    lblFechaVENC.Visible = false;
                    lblNACAlibracion.Visible = false;
                    lblUltimaCalibracion.Visible = false;
                    lblProximaCalibracion.Visible = false;
                    
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#fcca97");
                }
                if (lblTipoCalibracion.Text == "N/A")
                {
                    AlertaEstadoEquipoObsoleto.Visible = false;
                    lblVencido45dias.Visible = false;
                    lblFechaVENC.Visible = false;
                    lblNACAlibracion.Visible = true;
                    lblUltimaCalibracion.Visible = false;
                    lblProximaCalibracion.Visible = false;
                }

            }
            /*
            lblKPIVencenPronto.Text = vencenpront.ToString();
            lblKPIVencidos.Text = vencidos.ToString();
            SinCalibrar++;
            lblKPISinCalibrar.Text = (SinCalibrar-(vencenpront+vencidos)).ToString();
            */

        }
        // carga la lista utilizando un filtro

        public void GridViewCommandEventHandler(Object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                if (e.CommandName == "FichaCalibracion")
                {
                    DIVFechaBaja.Visible = false;
                    BotonAuxiliarMSA.Visible = false;
                    BotonAuxiliarCalibracion.Visible = false;
                    BotonAuxiliarAgregarProducto.Visible = false;
                    AUXAÑADIRDOCAUX.Visible = false;

                    lblNumEquipo.InnerText = "";
                    lblNombreEquipo.InnerText = "";
                    AImagenEquipo.HRef = "";
                    ImagenEquipo.Src = "";
                    InputNombreEquipo.Value = "";
                    InputNumSerie.Value = "";
                    DropTipoEquipo.SelectedValue = "-";
                    InputRango.Value = "";
                    InputEscala.Value = "";
                    InputFabricante.Value = "";
                    DropDepartamento.SelectedValue = "-";
                    InputPropietario.Value = "";
                    InputFechaAlta.Value = "";
                    InputFechaBaja.Value = "";
                    DropTipoCalibracion.SelectedValue = "-";
                    DropFrecuenciaCalibracion.SelectedValue = "-";
                    InputMedioCalibracion.Value = "";
                    InputUltimaCalibracion.Value = "";
                    InputProximaCalibracion.Value = "";
                    InputNotasEquipo.Value = "";
                    tbEquipoAlternativo.Value = "";
                    tbNotasAlternativo.Value = "";
                    AImagenEquipo.HRef = "../SMARTH_docs/METROLOGIA/sin_imagen.jpg";
                    ImagenEquipo.Src = "../SMARTH_docs/METROLOGIA/sin_imagen.jpg";


                    DataTable ModalEquipo = SHConexion.Devuelve_Listado_Equipos_Metrologia(" AND EQU.ID = " + e.CommandArgument.ToString(), "", "", "", "");
                    lblNumEquipo.InnerText = ModalEquipo.Rows[0]["NumEquipo"].ToString();
                    lblNombreEquipo.InnerText = ModalEquipo.Rows[0]["Nombre"].ToString();
                    AImagenEquipo.HRef = ModalEquipo.Rows[0]["Imagen"].ToString();
                    ImagenEquipo.Src = ModalEquipo.Rows[0]["Imagen"].ToString();
                    InputNombreEquipo.Value = ModalEquipo.Rows[0]["Nombre"].ToString();
                    InputNumSerie.Value = ModalEquipo.Rows[0]["Nserie"].ToString();
                    DropTipoEquipo.SelectedValue = ModalEquipo.Rows[0]["Clase"].ToString();
                    InputRango.Value = ModalEquipo.Rows[0]["Rango"].ToString();
                    InputEscala.Value = ModalEquipo.Rows[0]["DivisionEscala"].ToString();
                    DropEstado.SelectedValue = ModalEquipo.Rows[0]["EstadoEquipo"].ToString();
                    if (DropEstado.SelectedValue == "2")
                    {
                        DIVFechaBaja.Visible = true;
                    }

                    DropDepartamento.SelectedValue = ModalEquipo.Rows[0]["Departamento"].ToString();
                    InputPropietario.Value = ModalEquipo.Rows[0]["Propietario"].ToString();
                    UbicacionEquipo.Value = ModalEquipo.Rows[0]["Ubicacion"].ToString();
                    InputFechaAlta.Value = ModalEquipo.Rows[0]["FechaAlta"].ToString();
                    InputFechaBaja.Value = ModalEquipo.Rows[0]["FechaBaja"].ToString();
                    InputFabricante.Value = ModalEquipo.Rows[0]["Fabricante"].ToString();
                    DropTipoCalibracion.SelectedValue = ModalEquipo.Rows[0]["TipoCalibracion"].ToString();
                    DropFrecuenciaCalibracion.SelectedValue = ModalEquipo.Rows[0]["FrecuenciaCalibracion"].ToString();
                    InputMedioCalibracion.Value = ModalEquipo.Rows[0]["MedioCalibracion"].ToString();
                    InputUltimaCalibracion.Value = ModalEquipo.Rows[0]["UltimaCalibracion"].ToString();
                    InputProximaCalibracion.Value = ModalEquipo.Rows[0]["VenceCalibracion"].ToString();
                    AImagenEquipo.HRef = ModalEquipo.Rows[0]["Imagen"].ToString();
                    ImagenEquipo.Src = ModalEquipo.Rows[0]["Imagen"].ToString();
                    InputNotasEquipo.Value = ModalEquipo.Rows[0]["NotasEquipo"].ToString();
                    if (ModalEquipo.Rows[0]["FrecuenciaCalibracion"].ToString() != "")
                    {
                        DropCriterioAceptacion.SelectedValue = ModalEquipo.Rows[0]["CriterioAceptacion"].ToString();
                    }
                    tbEquipoAlternativo.Value = ModalEquipo.Rows[0]["Alternativo"].ToString();
                    tbNotasAlternativo.Value = ModalEquipo.Rows[0]["AlternativoInstruccion"].ToString();

                    //Desactivo alertas
                    AlertaActivo.Visible = false;
                    AlertaCalibrado.Visible = false;
                    AlertaProxCalibracion.Visible = false;
                    AlertaProxCalibracion.Visible = false;
                    //Alerta de estado de equipo
                    if (ModalEquipo.Rows[0]["EstadoEquipo"].ToString() == "0")
                    {
                        AlertaActivo.Visible = true;
                        AlertaActivo.InnerText = "- El equipo está pendiente de activar.";
                    }
                    else if (ModalEquipo.Rows[0]["EstadoEquipo"].ToString() == "2")
                    {
                        AlertaActivo.Visible = true;
                        AlertaActivo.InnerText = "- El equipo está fuera de uso.";

                    }

                    //Alerta de calibración vencida
                    if (Convert.ToDateTime(ModalEquipo.Rows[0]["VenceCalibracion"].ToString()) < DateTime.Now)
                    {
                        AlertaCalibrado.Visible = true;
                        AlertaCalibrado.InnerText = "- La calibración del equipo ha vencido.";
                    }
                    //Alerta de calibración próxima
                    if (Convert.ToDateTime(ModalEquipo.Rows[0]["VenceCalibracion"].ToString()) < DateTime.Now.AddDays(+45) && AlertaCalibrado.Visible == false)
                    {
                        AlertaProxCalibracion.Visible = true;
                        AlertaProxCalibracion.InnerText = "- La calibración del equipo vencerá el " + Convert.ToDateTime(ModalEquipo.Rows[0]["VenceCalibracion"]).ToString("dd/MM/yyyy") + ".";

                    }

                    //BLOQUE MSA
                    GridMSA.DataSource = null;
                    DropMSA.SelectedIndex = Convert.ToInt32(ModalEquipo.Rows[0]["Msa"].ToString());
                    if (DropMSA.SelectedValue == "1")
                    {
                        DataTable ListaMSA = SHConexion.Devuelve_Listado_MSA(" AND Equipo = " + lblNumEquipo.InnerText);
                        GridMSA.DataSource = ListaMSA;
                        if (ListaMSA.Rows.Count == 0)
                        {
                            BotonAuxiliarMSA.Visible = true;
                        }
                    }
                    else if (DropMSA.SelectedValue == "2")
                    {
                        DataTable ListaMSA = SHConexion.Devuelve_Listado_MSA(" AND CLASE = '" + DropTipoEquipo.SelectedValue + "'");
                        GridMSA.DataSource = ListaMSA;
                        if (ListaMSA.Rows.Count == 0)
                        {
                            BotonAuxiliarMSA.Visible = true;
                        }
                    }
                    GridMSA.DataBind();

                    //BLOQUE CALIBRACION
                    GridCalibracion.DataSource = null;
                    DataTable ListaCalibracion = SHConexion.Devuelve_Listado_Calibracion(lblNumEquipo.InnerText);
                    GridCalibracion.DataSource = ListaCalibracion;
                    if (ListaCalibracion.Rows.Count == 0)
                    {
                        BotonAuxiliarCalibracion.Visible = true;
                    }

                    GridCalibracion.DataBind();

                    GridProductos.DataSource = null;
                    DataTable ListaEquiposXProductos = SHConexion.Devuelve_Listado_EquipoXProducto(lblNumEquipo.InnerText);
                    GridProductos.DataSource = ListaEquiposXProductos;
                    if (ListaEquiposXProductos.Rows.Count == 0)
                    {
                        BotonAuxiliarAgregarProducto.Visible = true;
                    }
                    GridProductos.DataBind();

                    GridDOCAUX.DataSource = null;
                    DataTable ListaDOCAUX = SHConexion.Devuelve_Listado_DOCAUX_Metrologia(" AND Equipo = " + lblNumEquipo.InnerText);
                    if (ListaDOCAUX.Rows.Count == 0)
                    {
                        AUXAÑADIRDOCAUX.Visible = true;
                    }

                    GridDOCAUX.DataSource = ListaDOCAUX;
                    GridDOCAUX.DataBind();



                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);

                }
                if (e.CommandName == "CargaDetalle")
                {

                    lkb_Sort_Click("REVISIONES");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                }
                if (e.CommandName == "Redirect")
                {
                    Response.Redirect("GP12HistoricoReferencia.aspx?REFERENCIA=" + e.CommandArgument);
                }
                string path = "";
                string file_name = "";
                if (e.CommandName == "EditarCalibracion")
                {
                    //LIMPIO ITEMS
                    DropCalibracionDocTIPO.SelectedValue = "0";
                    inputCalibracionDescripcion.Value = "";
                    DropInputINTEXT.SelectedValue = "INTERNO";
                    inputEntidadCalibracion.Value = "";
                    inputFechaCalibracion.Value = "";
                    DropCalibracionCriterio.SelectedValue = "0";
                    inputCalibracionValor.Value = "0";
                    DropAprobadoCalibracion.SelectedValue = "-";
                    
                    AUXCalibracionURL.Value = "";
                    BTNActualizarCalibracion.Visible = true;

                    RowCalibracionAdjunto.Visible = false;

                    //CARGO VALORES
                    DataTable Calibracion = SHConexion.Devuelve_Detalle_Calibracion(e.CommandArgument.ToString());
                    DropCalibracionDocTIPO.SelectedValue = Calibracion.Rows[0]["TipoCalibracion"].ToString();
                    inputCalibracionDescripcion.Value = Calibracion.Rows[0]["Descripcion"].ToString();
                    DropInputINTEXT.SelectedValue = Calibracion.Rows[0]["INT_EXT"].ToString();
                    inputEntidadCalibracion.Value = Calibracion.Rows[0]["EntidadCertificadora"].ToString();
                    inputFechaCalibracion.Value = Calibracion.Rows[0]["FechaDoc"].ToString();
                    DropCalibracionCriterio.SelectedValue = Calibracion.Rows[0]["CriterioCalibracion"].ToString();
                    inputCalibracionValor.Value = Convert.ToDouble(Calibracion.Rows[0]["ResultadoObtenido"]).ToString().Replace(",",".");
                    DropAprobadoCalibracion.SelectedValue = Calibracion.Rows[0]["Nombre"].ToString();                    
                    AUXCalibracionURL.Value = Calibracion.Rows[0]["URLDocumento"].ToString();
                    H1Calibracion.InnerText = "Editar Calibración: " + lblNumEquipo.InnerText + " " + lblNombreEquipo.InnerText;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupCalibracion();", true);                
                }
                if (e.CommandName == "EditarMSA")
                {
                    //LIMPIO ITEMS
                    DropTipoMSA.SelectedValue = "0";
                    InputDescripcionMSA.Value = "";
                    InputFechaMSA.Value = "";

                    DropResultadoMSA.SelectedValue = "-";
                    DropAprobadoMSA.SelectedValue = "-";
                    BTNActualizarMSA.Visible = true;
                    RowAdjuntoMSA.Visible = false;
                    AUXMSAURL.Value = "";

                    //CARGO VALORES

                    DataTable MSA = SHConexion.Devuelve_Detalle_MSA(e.CommandArgument.ToString());
                    DropTipoMSA.SelectedValue = MSA.Rows[0]["TipoMSA"].ToString();
                    InputDescripcionMSA.Value = MSA.Rows[0]["Descripcion"].ToString();
                    InputFechaMSA.Value = MSA.Rows[0]["FechaDoc"].ToString();

                    DropResultadoMSA.SelectedValue = MSA.Rows[0]["Resultado"].ToString();
                    DropAprobadoMSA.SelectedValue = MSA.Rows[0]["Nombre"].ToString();
                    AUXMSAURL.Value = MSA.Rows[0]["URLDocumento"].ToString();
                    H1MSA.InnerText = "Editar MSA: " + lblNumEquipo.InnerText + " " + lblNombreEquipo.InnerText;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupMSA();", true);
                    
                }
                if (e.CommandName == "EditarDOCAUX")
                {
                    //LIMPIO ITEMS
                    DropTipoDOCAUX.SelectedValue = "0";
                    tbDescripcionDOCAUX.Value = "";
                    tbFechaDOCAUX.Value = "";
                    dropAprobadoDOCAUX.SelectedValue = "-";

                    BTNActualizarDOCAUX.Visible = true;
                    RowAdjuntoDOCAUX.Visible = false;
                    AUXDOCAUXURL.Value = "";

                    //CARGO VALORES

                    DataTable DOCAUX = SHConexion.Devuelve_Detalle_DOCAUX_Metrologia(e.CommandArgument.ToString());
                    DropTipoDOCAUX.SelectedValue = DOCAUX.Rows[0]["TipoDOC"].ToString();
                    tbDescripcionDOCAUX.Value = DOCAUX.Rows[0]["Descripcion"].ToString();
                    tbFechaDOCAUX.Value = DOCAUX.Rows[0]["FechaDoc"].ToString();

                    dropAprobadoDOCAUX.SelectedValue = DOCAUX.Rows[0]["Nombre"].ToString();
                    AUXDOCAUXURL.Value = DOCAUX.Rows[0]["URLDocumento"].ToString();
                    H1DOCAUX.InnerText = "Editar documento: " + lblNumEquipo.InnerText + " " + lblNombreEquipo.InnerText;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupDOCAUX();", true);

                }
                if (e.CommandName == "EliminarMSA")
                {
                    //string file_name = DropDownList1.SelectedItem.Text;
                    //string path = Server.MapPath("files//" + file_name);
                    path = Server.MapPath(e.CommandArgument.ToString());

                    file_name = Path.GetFileName(path);

                    FileInfo file = new FileInfo(path);
                    if (file.Exists)//check file exsit or not  
                    {
                        try
                        {
                            file.Delete();
                            SHConexion.Eliminar_MSA(file_name);
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "Borrado_MSA_OK();", true);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "Borrado_MSA_NoExiste();", true);
                        SHConexion.Eliminar_MSA(file_name);
                    }


                }
                if (e.CommandName == "EliminarCalibracion")
                    {
                        //string file_name = DropDownList1.SelectedItem.Text;
                        //string path = Server.MapPath("files//" + file_name);
                        path = Server.MapPath(e.CommandArgument.ToString());

                        file_name = Path.GetFileName(path);

                        FileInfo file2 = new FileInfo(path);
                        if (file2.Exists)//check file exsit or not  
                        {
                            try
                            {
                                file2.Delete();
                                SHConexion.Eliminar_Calibracion(file_name);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "Borrado_MSA_OK();", true);
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "Borrado_MSA_NoExiste();", true);
                            SHConexion.Eliminar_Calibracion(file_name);
                        }
                    }
                if (e.CommandName == "EliminarDOCAUX")
                {
                    //string file_name = DropDownList1.SelectedItem.Text;
                    //string path = Server.MapPath("files//" + file_name);
                    path = Server.MapPath(e.CommandArgument.ToString());

                    file_name = Path.GetFileName(path);

                    FileInfo file2 = new FileInfo(path);
                    if (file2.Exists)//check file exsit or not  
                    {
                        try
                        {
                            file2.Delete();
                            SHConexion.Eliminar_DOCAUX_Metrologia(file_name);
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "Borrado_MSA_OK();", true);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "Borrado_MSA_NoExiste();", true);
                        SHConexion.Eliminar_DOCAUX_Metrologia(file_name);
                    }
                }
                if (e.CommandName == "EliminarProductoXEquipo")
                {

                    SHConexion.Eliminar_Metrologia_ProductoXEquipo(e.CommandArgument.ToString());
                    
                }
                if (e.CommandName == "NuevoEquipo")
                {
                    txtNuevoEquipo.Value = SHConexion.Devuelve_MAX_Metrologia();
                    txtNuevoEquipoAlternativo.Value = "";
                    txtNuevaDescripcion.Value = ""; 
                    DropNuevoTipoEquipo.SelectedValue = "-";
                    txtNuevoFrecuenciaCal.Value = "1";
                    DropNuevoTipoCalibracion.SelectedValue = "-";
                    txtNuevoMedioCalibracion.Value = "";
                    DropNuevoCriterioCalibracion.SelectedValue = "-";
                    DropNuevoTipoMSA.SelectedValue = "-";
                    txtNuevoUbicacion.Value = "";
                    txtNuevoNotas.Value = "";
                    DropNuevoEquipoEstado.SelectedValue = "Pendiente de entrada";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupMODALNuevoEquipo();", true);                    
                }
            }
            catch (Exception ex)
            {
                mandar_mail2(ex.Message + ex.StackTrace, "Error en la aplicación de metrologia");
            }

        }
  
        protected void Cargar_todas(object sender, EventArgs e)
        {
            selectEquipo.Value = "";
            Drop_tipos_equipos_filtro.SelectedValue = "-";
            orderby = " ORDER BY NumEquipo DESC";

            selectReferencia.Value = "";

            Rellenar_grid(null, null);
        }
      
        //ESTADO DE REFERENCIAS

        // guarda una fila

        // cancela la modificación de una fila
      
        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
                Conexion_SMARTH SHconexion = new Conexion_SMARTH();
                //if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                if (!e.Row.RowState.HasFlag(DataControlRowState.Edit))
                { 
                    Label lblVencido30dias = (Label)e.Row.FindControl("lblFechaProxima"); 
                    Label lblFechaVENC = (Label)e.Row.FindControl("lblFechaVencida");
                    Label lblProxVencido = (Label)e.Row.FindControl("lblFechaPrevVencidatxt");
                    if (lblProxVencido.Text != "")
                    { 
                    if(Convert.ToDateTime(lblProxVencido.Text) < DateTime.Now )
                    {
                        lblFechaVENC.Visible = true;
                    }

                    if (Convert.ToDateTime(lblProxVencido.Text) < DateTime.Now.AddDays(+30) && lblFechaVENC.Visible == false)
                    {
                        lblVencido30dias.Visible = true;
                    }
                    }

                }

                if (e.Row.RowState.HasFlag(DataControlRowState.Edit))
                {
                    //drops de estado de referencia
                    DropDownList txtEstadoActual = (DropDownList)e.Row.FindControl("txtEstadoActual");                  
                    DataTable dt = SHconexion.Devuelve_Lista_Estados_Referencias();
                    txtEstadoActual.DataSource = dt;
                    txtEstadoActual.DataTextField = "Razon";
                    txtEstadoActual.DataValueField = "Razon";
                    txtEstadoActual.DataBind();
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    txtEstadoActual.SelectedValue = dr["EstadoActual"].ToString();


                    //drops de responsables
                    DataSet Operarios = SHconexion.Devuelve_mandos_intermedios_SMARTH();
                    Operarios.Tables[0].DefaultView.RowFilter = "Departamento = 'INGENIERIA' OR Departamento = '-'";
                    DataTable DTIngenieria = (Operarios.Tables[0].DefaultView).ToTable();
                    DropDownList txtResponsable = (DropDownList)e.Row.FindControl("txtResponsable");
                    txtResponsable.DataSource = DTIngenieria;
                    txtResponsable.DataTextField = "Nombre";
                    txtResponsable.DataValueField = "Nombre";
                    txtResponsable.DataBind();
                    DataRowView dr2 = e.Row.DataItem as DataRowView;
                    txtResponsable.SelectedValue = dr2["Responsable"].ToString();
                }
            }
        }

        //Redirecciones 
        
        protected void lkb_Sort_Click(string e)
        {
            //Bind the data

            //Manage the selected tab.
            this.ManageTabsPostBack(BTN_ESTADO_PRODUCTOS, BTN_ULTIMAS_REVISIONES, pills_historico, pills_plancalib, e);

        }

        private void ManageTabsPostBack(HtmlButton BTN_ESTADO_PRODUCTOS, HtmlButton BTN_ULTIMAS_REVISIONES,
                                        HtmlGenericControl pills_historico, HtmlGenericControl pills_plancalib, string grid)
        {
            // desactivte all tabs and panes tab-pane fade
            BTN_ESTADO_PRODUCTOS.Attributes.Add("class", "nav-link");
            BTN_ULTIMAS_REVISIONES.Attributes.Add("class", "nav-link");
            pills_historico.Attributes.Add("class", "tab-pane fade");
            pills_plancalib.Attributes.Add("class", "tab-pane fade");

            //activate the the tab and pane the user was viewing

            switch (grid)
            {
                //NAVE 3
                case "BTNFiltroCalib":
                    BTN_ESTADO_PRODUCTOS.Attributes.Add("class", "nav-link active");
                    pills_plancalib.Attributes.Add("class", "tab-pane fade show active");    
                    break;
                case "ListadoEquipos":
                    BTN_ULTIMAS_REVISIONES.Attributes.Add("class", "nav-link active");
                    pills_historico.Attributes.Add("class", "tab-pane fade show active");
                    break;

            }
        }

        
        //INSERTAR NUEVO EQUIPO
        public void InsertarEquipo(object sender, EventArgs e)
        {
            try
            {
                int Equipo = Convert.ToInt32(txtNuevoEquipo.Value);
                Conexion_SMARTH SHconexion = new Conexion_SMARTH();

                //Verifica disponiblidad del numero

                if (Equipo > 55500000 && Equipo < 55599999)
                {
                    if (SHconexion.Existe_Equipo_Metrologia(Equipo.ToString()))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "EquipoExistente();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupMODALNuevoEquipo();", true);
                    }


                    else
                    {
                        SHconexion.Insertar_Equipo_Metrologia(Equipo, txtNuevaDescripcion.Value, DropNuevoTipoEquipo.SelectedValue, Convert.ToInt32(txtNuevoFrecuenciaCal.Value), DropNuevoTipoCalibracion.SelectedValue, txtNuevoMedioCalibracion.Value, Convert.ToInt32(DropNuevoCriterioCalibracion.SelectedValue), Convert.ToInt32(DropNuevoTipoMSA.SelectedValue), txtNuevoUbicacion.Value, Convert.ToInt32(DropNuevoEquipoEstado.SelectedValue), txtNuevoNotas.Value);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "Equipo_Fuera_RangoNUM();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupMODALNuevoEquipo();", true);
                }
            }

            catch (Exception ex)
            { 
            }
        }


        //BLOQUE ACTUACION DOCUMENTOS AUXILIARES
        public void AbreDOCAUX(object sender, EventArgs e)
        {
            DropTipoDOCAUX.SelectedValue = "0";
            tbDescripcionDOCAUX.Value = "";
            tbFechaDOCAUX.Value = "";
            dropAprobadoDOCAUX.SelectedValue = "-";

            AUXDOCAUXURL.Value = "";

            BTNActualizarDOCAUX.Visible = false;
            RowAdjuntoDOCAUX.Visible = true;
            H1DOCAUX.InnerText = "Agregar documento auxiliar: " + lblNumEquipo.InnerText + " " + lblNombreEquipo.InnerText;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupDOCAUX();", true);
        }

        public void Actualizar_DOCAUX(object sender, EventArgs e)
        {
            Conexion_SMARTH SHconexion = new Conexion_SMARTH();
            SHconexion.Actualizar_DOCAUX_Metrologia_BD(AUXDOCAUXURL.Value, Convert.ToInt32(DropTipoDOCAUX.SelectedValue), SHconexion.Devuelve_ID_Piloto_SMARTH(dropAprobadoDOCAUX.SelectedValue), tbFechaDOCAUX.Value, tbDescripcionDOCAUX.Value);
            Rellenar_grid(null, null);
        }


        //BLOQUE ACTUACION MSA
        public void AbreMSA(object sender, EventArgs e)
        {
            DropTipoMSA.SelectedValue = "0";
            InputDescripcionMSA.Value = "";
            InputFechaMSA.Value = "";
            DropResultadoMSA.SelectedValue = "-";
            DropAprobadoMSA.SelectedValue = "-";
            AUXMSAURL.Value = "";

            BTNActualizarMSA.Visible = false;
            RowAdjuntoMSA.Visible = true;
            H1MSA.InnerText = "Agregar documento MSA: " + lblNumEquipo.InnerText + " " + lblNombreEquipo.InnerText;         
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupMSA();", true);
        }

        public void Actualizar_MSA(object sender, EventArgs e)
        {
            Conexion_SMARTH SHconexion = new Conexion_SMARTH();
            SHconexion.Actualizar_MSA_BD(AUXMSAURL.Value, Convert.ToInt32(DropTipoMSA.SelectedValue), SHconexion.Devuelve_ID_Piloto_SMARTH(DropAprobadoMSA.SelectedValue), InputFechaMSA.Value, InputDescripcionMSA.Value, DropResultadoMSA.SelectedValue);
            Rellenar_grid(null, null);
        }

        //BLOQUE ACTUACION CALIBRACION
        public void AbreCalibracion(object sender, EventArgs e)
        {
            DropCalibracionDocTIPO.SelectedValue = "0";
            inputCalibracionDescripcion.Value = "";
            DropInputINTEXT.SelectedValue = "INTERNO";
            inputEntidadCalibracion.Value = "";
            inputFechaCalibracion.Value = "";
            DropCalibracionCriterio.SelectedValue = "0";
            inputCalibracionValor.Value = "0";
            DropAprobadoCalibracion.SelectedValue = "-";
            AUXCalibracionURL.Value = "";

            BTNActualizarCalibracion.Visible = false;
            RowCalibracionAdjunto.Visible = true;
            H1Calibracion.InnerText = "Agregar Calibración: " + lblNumEquipo.InnerText + " " + lblNombreEquipo.InnerText;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupCalibracion();", true);
        }

        public void Actualizar_Calibracion(object sender, EventArgs e)
        {
            Conexion_SMARTH SHconexion = new Conexion_SMARTH();
            string ValorCalib = "0";
            if (DropCalibracionCriterio.SelectedValue == "1")
            {
                ValorCalib = inputCalibracionValor.Value;
            }
            SHconexion.Actualizar_Calibracion_BD(AUXCalibracionURL.Value, Convert.ToInt32(DropCalibracionCriterio.SelectedValue), SHconexion.Devuelve_ID_Piloto_SMARTH(DropAprobadoCalibracion.SelectedValue), inputFechaCalibracion.Value, inputCalibracionDescripcion.Value, ValorCalib, "APTO", Convert.ToInt32(DropCalibracionDocTIPO.SelectedValue),DropInputINTEXT.SelectedValue, inputEntidadCalibracion.Value);
            Rellenar_grid(null,null);

        }
        //AGREGAR PRODUCTOS
        public void AbreAgregarProductos(object sender, EventArgs e)
        {

            InputAgregarProducto.Value = "";
            H1AgregarDOCS.InnerText = "Agregar producto: " + lblNumEquipo.InnerText + " " + lblNombreEquipo.InnerText;
            
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupDOCProductos();", true);
        }

        public void AgregarProductos(object sender, EventArgs e)
        {
            Conexion_SMARTH SHconexion = new Conexion_SMARTH();
            string[] RecorteAgregarProducto = InputAgregarProducto.Value.Split(new char[] { '¬' });
            string AgregarProducto = RecorteAgregarProducto[0].Trim();
            SHconexion.Insertar_Metrologia_ProductoXEquipo(lblNumEquipo.InnerText, AgregarProducto);
            Rellenar_grid(null, null);

        }

        //FECHAS PARA CALIBRACION
        public void GridView_RowUpdating(Object sender, GridViewUpdateEventArgs e)
        {
           
            try
            {
                Conexion_SMARTH SHconexion = new Conexion_SMARTH();
                Label Equipo = (Label)GridPendientesCalibracion.Rows[e.RowIndex].FindControl("lblEDITNumEquipo");
                TextBox FechaCalib = (TextBox)GridPendientesCalibracion.Rows[e.RowIndex].FindControl("TXTEditaFecha");

                string FechaCalibracion = "";
                try
                {
                    Convert.ToDateTime(FechaCalib.Text);
                    FechaCalibracion = FechaCalib.Text;

                }
                catch (Exception ex)
                {
                    FechaCalibracion = "";
                }
                SHconexion.Actualizar_Prevision_Calibracion(Equipo.Text, FechaCalibracion);

                //DESCOMENTAR AL TERMINAR


                GridPendientesCalibracion.EditIndex = -1;
                Rellenar_grid(null, null);

                
            }
            catch (Exception ex)
            {

            }
       
        }

        // cancela la modificación de una fila
        protected void gridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridPendientesCalibracion.EditIndex = -1;
            GridPendientesCalibracion.DataSource = DTPlanicalibCALIB;
            GridPendientesCalibracion.DataBind();
            lkb_Sort_Click("BTNFiltroCalib");

        }

        // modifica una fila
        public void GridView_RowEditing(Object sender, GridViewEditEventArgs e)
        {

            GridPendientesCalibracion.EditIndex = e.NewEditIndex;
            GridPendientesCalibracion.DataSource = DTPlanicalibCALIB;
            GridPendientesCalibracion.DataBind();
            lkb_Sort_Click("BTNFiltroCalib");


        }

        //CONSULTA EQUIPO X PRODUCTO

        public void Devuelve_EquiposXProducto(object sender, EventArgs e)
        {
            if (selectReferencia.Value != "")
            {
            Conexion_SMARTH SHconexion = new Conexion_SMARTH();
            string[] RecorteSelectProducto = selectReferencia.Value.Split(new char[] { '¬' });
            string CheckEquipo = RecorteSelectProducto[0].Trim();
            DataTable EquiposXReferencias = SHconexion.Devuelve_EquipoXProducto(CheckEquipo);
               
                HEquiposXProductos.InnerText = "Listado de equipos de: " + selectReferencia.Value;
                GridEquiposXProductos.DataSource = EquiposXReferencias;
                GridEquiposXProductos.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupEquiposXProductos();", true);
                
            }

        }


        //SUBIDA DE DOCUMENTOS
        public void Insertar_documento(Object sender, EventArgs e)
        {
            try
            {
                HtmlButton button = (HtmlButton)sender;
               
                string NombreBoton = button.ID;

                if (UploadMSA.HasFile && NombreBoton == "BTNUploadMSA")
                { 
                    SaveFile(UploadMSA.PostedFile, 1);
                }
                if (UploadCalibracion.HasFile && NombreBoton == "BTNUploadCalibracion")
                {
                    SaveFile(UploadCalibracion.PostedFile, 2);
                }
                if (UploadDOCAUX.HasFile && NombreBoton == "BTNUploadDOCAUX")
                {
                    SaveFile(UploadDOCAUX.PostedFile, 3);
                }
                if (UploadImagen.HasFile && NombreBoton == "BTNUploadImagen")
                {
                    SaveFile(UploadImagen.PostedFile, 4);
                }

            }
            catch (Exception ex)
            {
                mandar_mail2(ex.Message + ex.StackTrace, "Error en la aplicación de metrologia");
            }
        }

        private void SaveFile(HttpPostedFile file, int num_img)
        {
            try
            {
                // Specify the path to save the uploaded file to.
                //string savePath = "C:\\inetpub_thermoweb\\SMARTH_docs\\METROLOGIA\\";

                //Busca la ruta y si no está crea la carpeta
                //string savePath = "C:\\inetpub_thermoweb\\SMARTH_docs\\METROLOGIA\\"+lblNumEquipo.InnerText+"\\"; // Your code goes here

                

                //bool exists = System.IO.Directory.Exists(Server.MapPath(savePath));

                //if (!exists)
                  //  System.IO.Directory.CreateDirectory(Server.MapPath(savePath));

                string savePath = @"C:\inetpub_thermoweb\SMARTH_docs\METROLOGIA\" + lblNumEquipo.InnerText + "\\"; // Your code goes here
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }

                // Get the name of the file to upload.
                //string fileName = FileUpload1.FileName;
                string fileName = "";
                string extension = "";
                switch (num_img)
                {
                    case 1:
                        extension = System.IO.Path.GetExtension(UploadMSA.PostedFile.FileName);
                        fileName = "MSA_"+ lblNumEquipo.InnerText + "_" + DateTime.Now.ToString("yyyyy-MM-dd_HHmmss") + extension;
                        break;
                    case 2:
                        extension = System.IO.Path.GetExtension(UploadCalibracion.PostedFile.FileName);
                        fileName = "Calibracion_" + lblNumEquipo.InnerText + "_" + DateTime.Now.ToString("yyyyy-MM-dd_HHmmss") + extension;
                        break;
                    case 3:
                        extension = System.IO.Path.GetExtension(UploadDOCAUX.PostedFile.FileName);
                        fileName = "DOC_Auxiliares_" + lblNumEquipo.InnerText + "_" + DateTime.Now.ToString("yyyyy-MM-dd_HHmmss") + extension;
                        break;
                    case 4:
                        extension = System.IO.Path.GetExtension(UploadImagen.PostedFile.FileName);
                        fileName = "Imagen_" + lblNumEquipo.InnerText + "_" + DateTime.Now.ToString("yyyyy-MM-dd_HHmmss") + extension;
                        break;
                    default:break;
                }

           
                savePath += fileName;


                // Call the SaveAs method to save the uploaded
                // file to the specified directory.
                string savePathVirtual = "";
                switch (num_img)
                {
                    case 1:                       
                        UploadMSA.SaveAs(savePath);
                        savePathVirtual = "..\\SMARTH_docs\\METROLOGIA\\" + lblNumEquipo.InnerText + "\\";
                        savePathVirtual += fileName;
                        InsertarDocumentosBD(savePathVirtual, 1);
                        break;
                    case 2:
                        UploadCalibracion.SaveAs(savePath);
                        savePathVirtual = "..\\SMARTH_docs\\METROLOGIA\\" + lblNumEquipo.InnerText + "\\";
                        savePathVirtual += fileName;
                        InsertarDocumentosBD(savePathVirtual, 2);
                        break;
                    case 3:
                        UploadDOCAUX.SaveAs(savePath);
                        savePathVirtual = "..\\SMARTH_docs\\METROLOGIA\\" + lblNumEquipo.InnerText + "\\";
                        savePathVirtual += fileName;
                        InsertarDocumentosBD(savePathVirtual, 3);
                        break;
                    case 4:
                        UploadImagen.SaveAs(savePath);
                        savePathVirtual = "..\\SMARTH_docs\\METROLOGIA\\" + lblNumEquipo.InnerText + "\\";
                        savePathVirtual += fileName;                       
                        InsertarDocumentosBD(savePathVirtual, 4);
                        break;
                    default: break;
                }
            }
            catch (Exception ex)
            {
                mandar_mail2(ex.Message + ex.StackTrace, "Error en la aplicación de metrologia");
            }
            
        }

        private void InsertarDocumentosBD(string savepath, int tipodoc)
        {
            try
            {
                Conexion_SMARTH SHconexion = new Conexion_SMARTH();
                switch (tipodoc)
                {
                    case 1: //DOCUMENTO MSA
                        SHconexion.Insertar_MSA(lblNumEquipo.InnerText, Convert.ToInt32(DropTipoMSA.SelectedValue), SHconexion.Devuelve_ID_Piloto_SMARTH(DropAprobadoMSA.SelectedValue), InputFechaMSA.Value, DateTime.Now.ToString("dd/MM/yyyy"), savepath, InputDescripcionMSA.Value, DropResultadoMSA.SelectedValue);
                        break;
                    case 2: //CALIBRACIÓN
                        string ValorCalib = "0";
                        if (DropCalibracionCriterio.SelectedValue == "1")
                        {
                            ValorCalib = inputCalibracionValor.Value;
                        }
                        SHconexion.Insertar_Calibracion(lblNumEquipo.InnerText, Convert.ToInt32(DropCalibracionCriterio.SelectedValue), SHconexion.Devuelve_ID_Piloto_SMARTH(DropAprobadoCalibracion.SelectedValue), inputFechaCalibracion.Value, DateTime.Now.ToString("dd/MM/yyyy"), savepath, inputCalibracionDescripcion.Value, ValorCalib, "APTO", Convert.ToInt32(DropCalibracionDocTIPO.SelectedValue),DropInputINTEXT.SelectedValue,inputEntidadCalibracion.Value);
                        SHconexion.Actualizar_Prevision_Calibracion(lblNumEquipo.InnerText, "");
                        break;
                    case 3: //AUXILIARES
                        SHconexion.Insertar_DOCAUX_Metrologia(lblNumEquipo.InnerText, Convert.ToInt32(DropTipoDOCAUX.SelectedValue), SHconexion.Devuelve_ID_Piloto_SMARTH(dropAprobadoDOCAUX.SelectedValue), tbFechaDOCAUX.Value, DateTime.Now.ToString("dd/MM/yyyy"), savepath, tbDescripcionDOCAUX.Value);
                        break;
                    case 4: //IMAGEN
                        SHconexion.Insertar_Imagen_Equipo_Metrologia(lblNumEquipo.InnerText, savepath);
                        break;
                    default: break;
                }
                Rellenar_grid(null, null);
            }
            catch (Exception ex)
            {
                mandar_mail2(ex.Message + ex.StackTrace, "Error en la aplicación de metrologia");
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
           
        }

        //ETIQUETAS DE CALIBRACIÓN 

        public void Imprimir_etiquetas(Object sender, EventArgs e)
        {
            try
            {
                string filepath = @"\\FACTS4-SRV\FPOCS\FPOC-10-03.xlsx";
                FileInfo fileInfo = new FileInfo(filepath);
                ExcelPackage p = new ExcelPackage(fileInfo);
                ExcelWorksheet xlWorkSheet = p.Workbook.Worksheets["ETIQUETAS1"];

                GenerarEtiquetasFiltros();

                
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                DataTable EtiquetaDAT = SHConexion.Devuelve_Listado_Equipos_MetrologiaETIQUETAS(ETIFiltroEquipo);
                
                if (EtiquetaDAT.Rows.Count == 0)
                { 
                    //alarma
                
                }
                else
                {
                    //añado las lineas que faltan
                    for (int i = EtiquetaDAT.Rows.Count; i < 10; i++)
                    {
                        EtiquetaDAT.Rows.Add();
                    }

                    //LIMPIO ETIQUETAS
                    xlWorkSheet.Cells["B2:J63"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    xlWorkSheet.Cells["B2:J63"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Transparent);
                    // ETIQUETAS PEQUEÑAS Y MEDIANAS
                    // ETI1 P
                    xlWorkSheet.Cells[2, 3].Value = EtiquetaDAT.Rows[0]["NumEquipo"].ToString();  //EQUIPO
                    xlWorkSheet.Cells[4, 2].Value = EtiquetaDAT.Rows[0]["Nombre"].ToString();  //NOMBRE
                    xlWorkSheet.Cells[5, 2].Value = EtiquetaDAT.Rows[0]["EstadoEquipoTEXT"].ToString();  //ESTADO
                    xlWorkSheet.Cells[7, 2].Value = EtiquetaDAT.Rows[0]["UltCalib"].ToString();  //ULTCAL
                    xlWorkSheet.Cells[7, 3].Value = EtiquetaDAT.Rows[0]["VenceCalibracion"].ToString();  //PROXCAL

                    //ETI1 M
                    xlWorkSheet.Cells[2, 8].Value = EtiquetaDAT.Rows[0]["NumEquipo"].ToString();  //EQUIPO
                    xlWorkSheet.Cells[4, 5].Value = EtiquetaDAT.Rows[0]["Nombre"].ToString();  //NOMBRE
                    xlWorkSheet.Cells[6, 5].Value = EtiquetaDAT.Rows[0]["EstadoEquipoTEXT"].ToString();  //ESTADO
                    xlWorkSheet.Cells[7, 8].Value = EtiquetaDAT.Rows[0]["UltCalib"].ToString();  //ULTCAL
                    xlWorkSheet.Cells[7, 9].Value = EtiquetaDAT.Rows[0]["VenceCalibracion"].ToString();  //PROXCAL

                    switch (EtiquetaDAT.Rows[0]["EstadoEquipoTEXT"].ToString())
                    {
                        case "CALIBRADO":
                            xlWorkSheet.Cells["B2:J7"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            xlWorkSheet.Cells["B2:J7"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Transparent);
                            break;
                        case "PEND. VALIDAR":
                            xlWorkSheet.Cells["B2:J7"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            xlWorkSheet.Cells["B2:J7"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGoldenrodYellow);
                            break;
                        case "FUERA DE USO":
                            xlWorkSheet.Cells["B2:J7"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            xlWorkSheet.Cells["B2:J7"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.OrangeRed);
                            break;
                    }


                    // ETI2 P
                    xlWorkSheet.Cells[9, 3].Value = EtiquetaDAT.Rows[1]["NumEquipo"].ToString();  //EQUIPO
                    xlWorkSheet.Cells[11, 2].Value = EtiquetaDAT.Rows[1]["Nombre"].ToString();  //NOMBRE
                    xlWorkSheet.Cells[12, 2].Value = EtiquetaDAT.Rows[1]["EstadoEquipoTEXT"].ToString();  //ESTADO
                    xlWorkSheet.Cells[14, 2].Value = EtiquetaDAT.Rows[1]["UltCalib"].ToString();  //ULTCAL
                    xlWorkSheet.Cells[14, 3].Value = EtiquetaDAT.Rows[1]["VenceCalibracion"].ToString();  //PROXCAL

                    //ETI2 M
                    xlWorkSheet.Cells[9, 8].Value = EtiquetaDAT.Rows[1]["NumEquipo"].ToString();  //EQUIPO
                    xlWorkSheet.Cells[11, 5].Value = EtiquetaDAT.Rows[1]["Nombre"].ToString();  //NOMBRE
                    xlWorkSheet.Cells[13, 5].Value = EtiquetaDAT.Rows[1]["EstadoEquipoTEXT"].ToString();  //ESTADO
                    xlWorkSheet.Cells[14, 8].Value = EtiquetaDAT.Rows[1]["UltCalib"].ToString();  //ULTCAL
                    xlWorkSheet.Cells[14, 9].Value = EtiquetaDAT.Rows[1]["VenceCalibracion"].ToString();  //PROXCAL

                    switch (EtiquetaDAT.Rows[1]["EstadoEquipoTEXT"].ToString())
                    {
                        case "CALIBRADO":
                            xlWorkSheet.Cells["B9:J14"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            xlWorkSheet.Cells["B9:J14"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Transparent);
                            break;
                        case "PEND. VALIDAR":
                            xlWorkSheet.Cells["B9:J14"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            xlWorkSheet.Cells["B9:J14"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGoldenrodYellow);
                            break;
                        case "FUERA DE USO":
                            xlWorkSheet.Cells["B9:J14"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            xlWorkSheet.Cells["B9:J14"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red);
                            break;
                    }


                    // ETI3 P
                    xlWorkSheet.Cells[16, 3].Value = EtiquetaDAT.Rows[2]["NumEquipo"].ToString();  //EQUIPO
                    xlWorkSheet.Cells[18, 2].Value = EtiquetaDAT.Rows[2]["Nombre"].ToString();  //NOMBRE
                    xlWorkSheet.Cells[19, 2].Value = EtiquetaDAT.Rows[2]["EstadoEquipoTEXT"].ToString();  //ESTADO
                    xlWorkSheet.Cells[21, 2].Value = EtiquetaDAT.Rows[2]["UltCalib"].ToString();  //ULTCAL
                    xlWorkSheet.Cells[21, 3].Value = EtiquetaDAT.Rows[2]["VenceCalibracion"].ToString();  //PROXCAL

                    //ETI3 M
                    xlWorkSheet.Cells[16, 8].Value = EtiquetaDAT.Rows[2]["NumEquipo"].ToString();  //EQUIPO
                    xlWorkSheet.Cells[18, 5].Value = EtiquetaDAT.Rows[2]["Nombre"].ToString();  //NOMBRE
                    xlWorkSheet.Cells[20, 5].Value = EtiquetaDAT.Rows[2]["EstadoEquipoTEXT"].ToString();  //ESTADO
                    xlWorkSheet.Cells[21, 8].Value = EtiquetaDAT.Rows[2]["UltCalib"].ToString();  //ULTCAL
                    xlWorkSheet.Cells[21, 9].Value = EtiquetaDAT.Rows[2]["VenceCalibracion"].ToString();  //PROXCAL

                    switch (EtiquetaDAT.Rows[2]["EstadoEquipoTEXT"].ToString())
                    {
                        case "CALIBRADO":
                            xlWorkSheet.Cells["B16:J21"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            xlWorkSheet.Cells["B16:J21"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Transparent);
                            break;
                        case "PEND. VALIDAR":
                            xlWorkSheet.Cells["B16:J21"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            xlWorkSheet.Cells["B16:J21"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGoldenrodYellow);
                            break;
                        case "FUERA DE USO":
                            xlWorkSheet.Cells["B16:J21"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            xlWorkSheet.Cells["B16:J21"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red);
                            break;
                    }

                    // ETI4 P
                    xlWorkSheet.Cells[23, 3].Value = EtiquetaDAT.Rows[3]["NumEquipo"].ToString();  //EQUIPO
                    xlWorkSheet.Cells[25, 2].Value = EtiquetaDAT.Rows[3]["Nombre"].ToString();  //NOMBRE
                    xlWorkSheet.Cells[26, 2].Value = EtiquetaDAT.Rows[3]["EstadoEquipoTEXT"].ToString();  //ESTADO
                    xlWorkSheet.Cells[28, 2].Value = EtiquetaDAT.Rows[3]["UltCalib"].ToString();  //ULTCAL
                    xlWorkSheet.Cells[28, 3].Value = EtiquetaDAT.Rows[3]["VenceCalibracion"].ToString();  //PROXCAL
                                                                                                          //ETI4 M
                    xlWorkSheet.Cells[23, 8].Value = EtiquetaDAT.Rows[3]["NumEquipo"].ToString();  //EQUIPO
                    xlWorkSheet.Cells[25, 5].Value = EtiquetaDAT.Rows[3]["Nombre"].ToString();  //NOMBRE
                    xlWorkSheet.Cells[27, 5].Value = EtiquetaDAT.Rows[3]["EstadoEquipoTEXT"].ToString();  //ESTADO
                    xlWorkSheet.Cells[28, 8].Value = EtiquetaDAT.Rows[3]["UltCalib"].ToString();  //ULTCAL
                    xlWorkSheet.Cells[28, 9].Value = EtiquetaDAT.Rows[3]["VenceCalibracion"].ToString();  //PROXCAL

                    switch (EtiquetaDAT.Rows[3]["EstadoEquipoTEXT"].ToString())
                    {
                        case "CALIBRADO":
                            xlWorkSheet.Cells["B23:J28"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            xlWorkSheet.Cells["B23:J28"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Transparent);
                            break;
                        case "PEND. VALIDAR":
                            xlWorkSheet.Cells["B23:J28"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            xlWorkSheet.Cells["B23:J28"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGoldenrodYellow);
                            break;
                        case "FUERA DE USO":
                            xlWorkSheet.Cells["B23:J28"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            xlWorkSheet.Cells["B23:J28"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red);
                            break;
                    }


                    // ETI5 P
                    xlWorkSheet.Cells[30, 3].Value = EtiquetaDAT.Rows[4]["NumEquipo"].ToString();  //EQUIPO
                    xlWorkSheet.Cells[32, 2].Value = EtiquetaDAT.Rows[4]["Nombre"].ToString();  //NOMBRE
                    xlWorkSheet.Cells[33, 2].Value = EtiquetaDAT.Rows[4]["EstadoEquipoTEXT"].ToString();  //ESTADO
                    xlWorkSheet.Cells[35, 2].Value = EtiquetaDAT.Rows[4]["UltCalib"].ToString();  //ULTCAL
                    xlWorkSheet.Cells[35, 3].Value = EtiquetaDAT.Rows[4]["VenceCalibracion"].ToString();  //PROXCAL
                                                                                                          //ETI5 M
                    xlWorkSheet.Cells[30, 8].Value = EtiquetaDAT.Rows[4]["NumEquipo"].ToString();  //EQUIPO
                    xlWorkSheet.Cells[32, 5].Value = EtiquetaDAT.Rows[4]["Nombre"].ToString();  //NOMBRE
                    xlWorkSheet.Cells[34, 5].Value = EtiquetaDAT.Rows[4]["EstadoEquipoTEXT"].ToString();  //ESTADO
                    xlWorkSheet.Cells[35, 8].Value = EtiquetaDAT.Rows[4]["UltCalib"].ToString();  //ULTCAL
                    xlWorkSheet.Cells[35, 9].Value = EtiquetaDAT.Rows[4]["VenceCalibracion"].ToString();  //PROXCAL

                    switch (EtiquetaDAT.Rows[4]["EstadoEquipoTEXT"].ToString())
                    {
                        case "CALIBRADO":
                            xlWorkSheet.Cells["B30:J35"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            xlWorkSheet.Cells["B30:J35"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Transparent);
                            break;
                        case "PEND. VALIDAR":
                            xlWorkSheet.Cells["B30:J35"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            xlWorkSheet.Cells["B30:J35"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGoldenrodYellow);
                            break;
                        case "FUERA DE USO":
                            xlWorkSheet.Cells["B30:J35"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            xlWorkSheet.Cells["B30:J35"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red);
                            break;
                    }

                    // ETI6 P
                    xlWorkSheet.Cells[37, 3].Value = EtiquetaDAT.Rows[5]["NumEquipo"].ToString();  //EQUIPO
                    xlWorkSheet.Cells[39, 2].Value = EtiquetaDAT.Rows[5]["Nombre"].ToString();  //NOMBRE
                    xlWorkSheet.Cells[40, 2].Value = EtiquetaDAT.Rows[5]["EstadoEquipoTEXT"].ToString();  //ESTADO
                    xlWorkSheet.Cells[42, 2].Value = EtiquetaDAT.Rows[5]["UltCalib"].ToString();  //ULTCAL
                    xlWorkSheet.Cells[42, 3].Value = EtiquetaDAT.Rows[5]["VenceCalibracion"].ToString();  //PROXCAL
                                                                                                          //ETI6 M
                    xlWorkSheet.Cells[37, 8].Value = EtiquetaDAT.Rows[5]["NumEquipo"].ToString();  //EQUIPO
                    xlWorkSheet.Cells[39, 5].Value = EtiquetaDAT.Rows[5]["Nombre"].ToString();  //NOMBRE
                    xlWorkSheet.Cells[41, 5].Value = EtiquetaDAT.Rows[5]["EstadoEquipoTEXT"].ToString();  //ESTADO
                    xlWorkSheet.Cells[42, 8].Value = EtiquetaDAT.Rows[5]["UltCalib"].ToString();  //ULTCAL
                    xlWorkSheet.Cells[42, 9].Value = EtiquetaDAT.Rows[5]["VenceCalibracion"].ToString();  //PROXCAL

                    switch (EtiquetaDAT.Rows[5]["EstadoEquipoTEXT"].ToString())
                    {
                        case "CALIBRADO":
                            xlWorkSheet.Cells["B37:J42"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            xlWorkSheet.Cells["B37:J42"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Transparent);
                            break;
                        case "PEND. VALIDAR":
                            xlWorkSheet.Cells["B37:J42"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            xlWorkSheet.Cells["B37:J42"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGoldenrodYellow);
                            break;
                        case "FUERA DE USO":
                            xlWorkSheet.Cells["B37:J42"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            xlWorkSheet.Cells["B37:J42"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red);
                            break;
                    }


                    // ETI7 P
                    xlWorkSheet.Cells[44, 3].Value = EtiquetaDAT.Rows[6]["NumEquipo"].ToString();  //EQUIPO
                    xlWorkSheet.Cells[46, 2].Value = EtiquetaDAT.Rows[6]["Nombre"].ToString();  //NOMBRE
                    xlWorkSheet.Cells[47, 2].Value = EtiquetaDAT.Rows[6]["EstadoEquipoTEXT"].ToString();  //ESTADO
                    xlWorkSheet.Cells[49, 2].Value = EtiquetaDAT.Rows[6]["UltCalib"].ToString();  //ULTCAL
                    xlWorkSheet.Cells[49, 3].Value = EtiquetaDAT.Rows[6]["VenceCalibracion"].ToString();  //PROXCAL
                                                                                                          //ETI7 M
                    xlWorkSheet.Cells[44, 8].Value = EtiquetaDAT.Rows[6]["NumEquipo"].ToString();  //EQUIPO
                    xlWorkSheet.Cells[46, 5].Value = EtiquetaDAT.Rows[6]["Nombre"].ToString();  //NOMBRE
                    xlWorkSheet.Cells[48, 5].Value = EtiquetaDAT.Rows[6]["EstadoEquipoTEXT"].ToString();  //ESTADO
                    xlWorkSheet.Cells[49, 8].Value = EtiquetaDAT.Rows[6]["UltCalib"].ToString();  //ULTCAL
                    xlWorkSheet.Cells[49, 9].Value = EtiquetaDAT.Rows[6]["VenceCalibracion"].ToString();  //PROXCAL

                    switch (EtiquetaDAT.Rows[6]["EstadoEquipoTEXT"].ToString())
                    {
                        case "CALIBRADO":
                            xlWorkSheet.Cells["B44:J49"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            xlWorkSheet.Cells["B44:J49"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Transparent);
                            break;
                        case "PEND. VALIDAR":
                            xlWorkSheet.Cells["B44:J49"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            xlWorkSheet.Cells["B44:J49"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGoldenrodYellow);
                            break;
                        case "FUERA DE USO":
                            xlWorkSheet.Cells["B44:J49"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            xlWorkSheet.Cells["B44:J49"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red);
                            break;
                    }

                    // ETI8 P
                    xlWorkSheet.Cells[51, 3].Value = EtiquetaDAT.Rows[7]["NumEquipo"].ToString();  //EQUIPO
                    xlWorkSheet.Cells[53, 2].Value = EtiquetaDAT.Rows[7]["Nombre"].ToString();  //NOMBRE
                    xlWorkSheet.Cells[54, 2].Value = EtiquetaDAT.Rows[7]["EstadoEquipoTEXT"].ToString();  //ESTADO
                    xlWorkSheet.Cells[56, 2].Value = EtiquetaDAT.Rows[7]["UltCalib"].ToString();  //ULTCAL
                    xlWorkSheet.Cells[56, 3].Value = EtiquetaDAT.Rows[7]["VenceCalibracion"].ToString();  //PROXCAL
                                                                                                          //ETI8 M
                    xlWorkSheet.Cells[51, 8].Value = EtiquetaDAT.Rows[7]["NumEquipo"].ToString();  //EQUIPO
                    xlWorkSheet.Cells[53, 5].Value = EtiquetaDAT.Rows[7]["Nombre"].ToString();  //NOMBRE
                    xlWorkSheet.Cells[55, 5].Value = EtiquetaDAT.Rows[7]["EstadoEquipoTEXT"].ToString();  //ESTADO
                    xlWorkSheet.Cells[56, 8].Value = EtiquetaDAT.Rows[7]["UltCalib"].ToString();  //ULTCAL
                    xlWorkSheet.Cells[56, 9].Value = EtiquetaDAT.Rows[7]["VenceCalibracion"].ToString();  //PROXCAL

                    switch (EtiquetaDAT.Rows[7]["EstadoEquipoTEXT"].ToString())
                    {
                        case "CALIBRADO":
                            xlWorkSheet.Cells["B51:J56"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            xlWorkSheet.Cells["B51:J56"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Transparent);
                            break;
                        case "PEND. VALIDAR":
                            xlWorkSheet.Cells["B51:J56"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            xlWorkSheet.Cells["B51:J56"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGoldenrodYellow);
                            break;
                        case "FUERA DE USO":
                            xlWorkSheet.Cells["B51:J56"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            xlWorkSheet.Cells["B51:J56"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red);
                            break;
                    }


                    // ETI9 P
                    xlWorkSheet.Cells[58, 3].Value = EtiquetaDAT.Rows[8]["NumEquipo"].ToString();  //EQUIPO
                    xlWorkSheet.Cells[60, 2].Value = EtiquetaDAT.Rows[8]["Nombre"].ToString();  //NOMBRE
                    xlWorkSheet.Cells[61, 2].Value = EtiquetaDAT.Rows[8]["EstadoEquipoTEXT"].ToString();  //ESTADO
                    xlWorkSheet.Cells[63, 2].Value = EtiquetaDAT.Rows[8]["UltCalib"].ToString();  //ULTCAL
                    xlWorkSheet.Cells[63, 3].Value = EtiquetaDAT.Rows[8]["VenceCalibracion"].ToString();  //PROXCAL
                                                                                                          //ETI9 M
                    xlWorkSheet.Cells[58, 8].Value = EtiquetaDAT.Rows[8]["NumEquipo"].ToString();  //EQUIPO
                    xlWorkSheet.Cells[60, 5].Value = EtiquetaDAT.Rows[8]["Nombre"].ToString();  //NOMBRE
                    xlWorkSheet.Cells[62, 5].Value = EtiquetaDAT.Rows[8]["EstadoEquipoTEXT"].ToString();  //ESTADO
                    xlWorkSheet.Cells[63, 8].Value = EtiquetaDAT.Rows[8]["UltCalib"].ToString();  //ULTCAL
                    xlWorkSheet.Cells[63, 9].Value = EtiquetaDAT.Rows[8]["VenceCalibracion"].ToString();  //PROXCAL

                    switch (EtiquetaDAT.Rows[8]["EstadoEquipoTEXT"].ToString())
                    {
                        case "CALIBRADO":
                            xlWorkSheet.Cells["B58:J63"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            xlWorkSheet.Cells["B58:J63"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Transparent);
                            break;
                        case "PEND. VALIDAR":
                            xlWorkSheet.Cells["B58:J63"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            xlWorkSheet.Cells["B58:J63"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGoldenrodYellow);
                            break;
                        case "FUERA DE USO":
                            xlWorkSheet.Cells["B58:J63"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            xlWorkSheet.Cells["B58:J63"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red);
                            break;
                    }

                    xlWorkSheet.Cells["D1:D71"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    xlWorkSheet.Cells["D1:D71"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Transparent);
                    p.Save();

                    // Limpiamos la salida
                    Response.Clear();
                    // Con esto le decimos al browser que la salida sera descargable
                    Response.ContentType = "application/octet-stream";
                    // esta linea es opcional, en donde podemos cambiar el nombre del fichero a descargar (para que sea diferente al original)
                    Response.AddHeader("Content-Disposition", "attachment; filename=Etiquetas_Calibracion_"+DateTime.Now+".xlsx");
                    // Escribimos el fichero a enviar 
                    Response.WriteFile(@"\\FACTS4-SRV\FPOCS\FPOC-10-03.xlsx");
                    // volcamos el stream 
                    Response.Flush();
                    // Enviamos todo el encabezado ahora
                    Response.End();
                }
                
            }
            catch (Exception ex)
            {
                mandar_mail2(ex.Message + ex.StackTrace, "Error en la aplicación de metrologia");
            }
        }

        protected void GenerarEtiquetasFiltros()
        {

            //Comprueba todas las lineas del gridview y verifica si está marcado el cajetín

            try
            {
                ETIFiltroEquipo = " WHERE NumEquipo IS NULL";
                for (int i = 0; i < dgv_Listado_Equipos.Rows.Count; i++)
                {
                    CheckBox chkUpdate = (CheckBox)dgv_Listado_Equipos.Rows[i].Cells[8].FindControl("CheckPrinting");


                    if (chkUpdate != null)
                    {
                        if (chkUpdate.Checked)
                        {
                            // Get the values of textboxes using findControl
                            string Equipo = ((Label)dgv_Listado_Equipos.Rows[i].FindControl("lblNumEquipo")).Text;
                            //edit the data to database:
                            ETIFiltroEquipo = ETIFiltroEquipo + " OR NumEquipo like '" + Equipo + "%'";
                            chkUpdate.Checked = false;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                string errorMsg = "Error al actualizar";
                errorMsg += ex.Message;
                throw new Exception(errorMsg);
            }
        }

        public void mandar_mail2(string mensaje, string subject)
        {
        //Create MailMessage
        MailMessage email = new MailMessage();
        email.To.Add(new MailAddress("pedro@thermolympic.es"));
        email.From = new MailAddress("bms@thermolympic.es");
        email.Subject = subject + DateTime.Now.ToString("dd/MMM/yyy hh:mm:ss");
        email.Body = mensaje;
        email.IsBodyHtml = true;
        email.Priority = MailPriority.Normal;

        //Definir objeto SmtpClient
        SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtp.thermolympic.es";
        smtp.Port = 25;
        smtp.EnableSsl = false;
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = new NetworkCredential("bms@thermolympic.es", "010477Bms");

        //Enviar email
        try
        {
            smtp.Send(email);
            email.Dispose();
        }
        catch (Exception ex)
        {
        }
    }

    }

}