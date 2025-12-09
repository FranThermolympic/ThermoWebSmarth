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

using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using OfficeOpenXml;



namespace ThermoWeb.CALIDAD
{
    public partial class ALERTASCALIDAD : System.Web.UI.Page
    {
        public string DefectoCargado = "";
        private static DataSet ds_operarios = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Conexion_CALIDAD conexion = new Conexion_CALIDAD();
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
               // SHConexion.LimpiarTablaPiezasEnviadas();
                CargaAuxiliares();                  
                    if (Request.QueryString["NOCONFORMIDAD"] != null)
                    {
                        CargarDatosNoConformidad(Request.QueryString["NOCONFORMIDAD"].ToString());
                    }
            }          
        }

        private void CargaAuxiliares()
        {
            try
            {
                hyperlink1.NavigateUrl = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";
                hyperlink1.ImageUrl = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";
                hyperlink2.NavigateUrl = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";
                hyperlink2.ImageUrl = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";
                hyperlink3.NavigateUrl = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";
                hyperlink3.ImageUrl = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";
                hyperlink4.NavigateUrl = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";
                hyperlink4.ImageUrl = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";
                hyperlink5.NavigateUrl = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";
                hyperlink5.ImageUrl = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";
                hyperlink6.NavigateUrl = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";
                hyperlink6.ImageUrl = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";

                //cabecera de detalles

                Conexion_CALIDAD conexion = new Conexion_CALIDAD();
                DataSet Operarios = conexion.Devuelve_mandos_intermedios_SMARTH();
                Operarios.Tables[0].DefaultView.RowFilter = "Departamento = 'PRODUCCION' OR Departamento = '-'";
                DataTable DTPRODUCCION = (Operarios.Tables[0].DefaultView).ToTable();
                foreach (DataRow row in DTPRODUCCION.Rows) { DropProduccion.Items.Add(row["Nombre"].ToString()); }
                DropProduccion.ClearSelection();
                DropProduccion.SelectedValue = "-";

                Operarios.Tables[0].DefaultView.RowFilter = "Departamento = 'CALIDAD' OR Departamento = '-'";
                DataTable DTCalidad = (Operarios.Tables[0].DefaultView).ToTable();
                foreach (DataRow row in DTCalidad.Rows) { DropCalidad.Items.Add(row["Nombre"].ToString()); }
                DropCalidad.ClearSelection();
                DropCalidad.SelectedValue = "-";

                Operarios.Tables[0].DefaultView.RowFilter = "Departamento = 'INGENIERIA' OR Departamento = '-'";
                DataTable DTIngenieria = (Operarios.Tables[0].DefaultView).ToTable();
                foreach (DataRow row in DTIngenieria.Rows) { DropIngenieria.Items.Add(row["Nombre"].ToString()); }
                DropIngenieria.ClearSelection();
                DropIngenieria.SelectedValue = "-";

                DataTable Almacenes = conexion.Devuelve_Listado_AlmacenesNAV();
                foreach (DataRow row in Almacenes.Rows)
                { DropAlmacenContencion.Items.Add(row["Name"].ToString()); }
                DropAlmacenContencion.ClearSelection();
                DropAlmacenContencion.Items.Add("En tránsito");
                DropAlmacenContencion.Items.Add("En cliente");
                DropAlmacenContencion.SelectedValue = "-";
            }
            catch (Exception)
            {

            }
        }

        //CARGA GENERAL
        private void CargarDatosNoConformidad(string NOCONFORMIDAD)
        {
            try
            {
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();

                Conexion_CALIDAD conexion = new Conexion_CALIDAD();
                DataSet NC = conexion.Devuelve_datos_NOCONFORMIDAD(NOCONFORMIDAD);
                    tbReferenciaCarga.Text = NC.Tables[0].Rows[0]["Referencia"].ToString();
                    tbNoConformidad.Text = NC.Tables[0].Rows[0]["IdNoConformidad"].ToString();
                    tbNoConformidadTEXT.Text = "NC-" + NC.Tables[0].Rows[0]["IdNoConformidad"].ToString();
                    tbFechaOriginal.Text = NC.Tables[0].Rows[0]["FechaOriginal"].ToString();
                    NivelAlerta.SelectedValue = NC.Tables[0].Rows[0]["EscaladoNoConformidad"].ToString();
                    TipoAlerta.SelectedValue = NC.Tables[0].Rows[0]["TipoNoConformidad"].ToString();
                    NivelAlerta.SelectedValue = NC.Tables[0].Rows[0]["EscaladoNoConformidad"].ToString();
                    tbPPM.Text = NC.Tables[0].Rows[0]["CantidadPPM"].ToString();
                    tbCantidad.Text = NC.Tables[0].Rows[0]["Cantidad"].ToString();
                    tbCantidadStock.Text = NC.Tables[0].Rows[0]["StockBloquear"].ToString();
                    tbProcesoAfectado.Text = NC.Tables[0].Rows[0]["ProcesoAfectado"].ToString();
                    tbProblemaNC.Text = NC.Tables[0].Rows[0]["DescripcionProblema"].ToString();
                    tbContramedidaPROD.Text = NC.Tables[0].Rows[0]["ContramedidaProduccion"].ToString();
                    tbContramedidaCAL.Text = NC.Tables[0].Rows[0]["ContramedidaCalidad"].ToString();
                    tbContramedidaING.Text = NC.Tables[0].Rows[0]["ContramedidaIngenieria"].ToString();
                    tbObservacionesNC.Text = NC.Tables[0].Rows[0]["Observaciones"].ToString();
                    divsector.Visible = true;
                    tbSector.Text = NC.Tables[0].Rows[0]["Sector"].ToString();
                    tbNoConformidadCliente.Text = NC.Tables[0].Rows[0]["IdNoConformidadCliente"].ToString();
                    Check_D6.Checked = Convert.ToBoolean(NC.Tables[0].Rows[0]["CheckD6"].ToString());
                    Check_D8.Checked = Convert.ToBoolean(NC.Tables[0].Rows[0]["CheckD8"].ToString());

                    if (NC.Tables[0].Rows[0]["D3"].ToString() != "" && NC.Tables[0].Rows[0]["D3"].ToString() != "N/A")
                    { 
                        D3Prevision.Value = NC.Tables[0].Rows[0]["D3"].ToString().Substring(0,10);
                    }
                    if (NC.Tables[0].Rows[0]["D6"].ToString() != "" && NC.Tables[0].Rows[0]["D6"].ToString() != "N/A" && Convert.ToBoolean(NC.Tables[0].Rows[0]["CheckD6"].ToString()) == true)
                    {
                        D6Prevision.Value = NC.Tables[0].Rows[0]["D6"].ToString().Substring(0, 10);
                    }
                    if (NC.Tables[0].Rows[0]["D8"].ToString() != "" && NC.Tables[0].Rows[0]["D8"].ToString() != "N/A" && Convert.ToBoolean(NC.Tables[0].Rows[0]["CheckD8"].ToString()) == true)
                    {
                        D8Prevision.Value = NC.Tables[0].Rows[0]["D8"].ToString().Substring(0, 10);
                    }

                    if (NC.Tables[0].Rows[0]["D3CIERRE"].ToString() != "" && NC.Tables[0].Rows[0]["D3CIERRE"].ToString() != "N/A")
                    {
                        D3FechaReal.Value = NC.Tables[0].Rows[0]["D3CIERRE"].ToString().Substring(0, 10);
                    }
                    if (NC.Tables[0].Rows[0]["D6CIERRE"].ToString() != "" && NC.Tables[0].Rows[0]["D6CIERRE"].ToString() != "N/A" && Convert.ToBoolean(NC.Tables[0].Rows[0]["CheckD6"].ToString()) == true)
                    {
                        D6FechaReal.Value = NC.Tables[0].Rows[0]["D6CIERRE"].ToString().Substring(0, 10);
                    }
                    if (NC.Tables[0].Rows[0]["D8CIERRE"].ToString() != "" && NC.Tables[0].Rows[0]["D8CIERRE"].ToString() != "N/A" && Convert.ToBoolean(NC.Tables[0].Rows[0]["CheckD8"].ToString()) == true)
                    {
                        D8FechaReal.Value = NC.Tables[0].Rows[0]["D8CIERRE"].ToString().Substring(0, 10);
                    }
                    TbSeleccionExterno.Value = NC.Tables[0].Rows[0]["CosteSeleccionEXT"].ToString();
                        if (TbSeleccionExterno.Value == ""){ TbSeleccionExterno.Value = "0"; }
                    TbPiezasNOKExterno.Value = NC.Tables[0].Rows[0]["CostePiezasNOKEXT"].ToString();
                        if (TbPiezasNOKExterno.Value == "") { TbPiezasNOKExterno.Value = "0"; }
                    TbCargosExterno.Value = NC.Tables[0].Rows[0]["CosteCargosEXT"].ToString();
                        if (TbCargosExterno.Value == "") { TbCargosExterno.Value = "0"; }
                    TbAdministrativosExterno.Value = NC.Tables[0].Rows[0]["CosteAdmonEXT"].ToString();
                        if (TbAdministrativosExterno.Value == "") { TbAdministrativosExterno.Value = "0"; }
                    tbSeleccionInterno.Value = NC.Tables[0].Rows[0]["CosteSeleccionINT"].ToString();
                        if (tbSeleccionInterno.Value == "") { tbSeleccionInterno.Value = "0"; }
                    tbOtrosInterno.Value = NC.Tables[0].Rows[0]["CosteOtrosINT"].ToString();
                        if (tbOtrosInterno.Value == "") { tbOtrosInterno.Value = "0"; }


                tbRepetitivoSN.Checked = false;
                if (Convert.ToInt32(NC.Tables[0].Rows[0]["RepiteDefecto10"]) == 1)
                {
                    tbRepetitivoSN.Checked = true;
                }
                tbProdRepetitivo.Visible = false;
                if (Convert.ToInt32(NC.Tables[0].Rows[0]["RepiteReferencia10"]) == 1)
                {
                    tbProdRepetitivo.Visible = true;
                }

                FRAMERESULTADOS.Src = "../GP12/GP12HistoricoReferenciaLite.aspx?REFERENCIA="+ tbReferenciaCarga.Text+"&FECHA="+ tbFechaOriginal.Text;
                FRAMEPDCA.Src = "../PDCA/PDCALite.aspx?NC=" + tbNoConformidad.Text;

                //PILOTO PRODUCCION
                if (NC.Tables[0].Rows[0]["PilotoProduccion"].ToString() != "")
                        { DropProduccion.SelectedValue = conexion.devuelve_Pilotos_NoConformidad_SMARTH(Convert.ToInt16(NC.Tables[0].Rows[0]["PilotoProduccion"].ToString())); }
                    else { DropProduccion.SelectedValue = "-"; }
                    //PILOTO CALIDAD
                    if (NC.Tables[0].Rows[0]["PilotoCalidad"].ToString() != "")
                        { DropCalidad.SelectedValue = conexion.devuelve_Pilotos_NoConformidad_SMARTH(Convert.ToInt16(NC.Tables[0].Rows[0]["PilotoCalidad"].ToString())); }
                    else { DropCalidad.SelectedValue = "-"; }
                    //PILOTO INGENIERIA
                    if (NC.Tables[0].Rows[0]["PilotoIngenieria"].ToString() != "")
                        { DropIngenieria.SelectedValue = conexion.devuelve_Pilotos_NoConformidad_SMARTH(Convert.ToInt16(NC.Tables[0].Rows[0]["PilotoIngenieria"].ToString())); }
                    else { DropIngenieria.SelectedValue = "-"; }

                    hyperlink1.NavigateUrl = NC.Tables[0].Rows[0]["ImagenNODefecto"].ToString();
                    hyperlink1.ImageUrl = NC.Tables[0].Rows[0]["ImagenNODefecto"].ToString();
                    hyperlink2.NavigateUrl = NC.Tables[0].Rows[0]["ImagenDefecto1"].ToString();
                    hyperlink2.ImageUrl = NC.Tables[0].Rows[0]["ImagenDefecto1"].ToString();
                    hyperlink3.NavigateUrl = NC.Tables[0].Rows[0]["ImagenDefecto2"].ToString();
                    hyperlink3.ImageUrl = NC.Tables[0].Rows[0]["ImagenDefecto2"].ToString();
                    hyperlink4.NavigateUrl = NC.Tables[0].Rows[0]["ImagenTrazabilidad1"].ToString();
                    hyperlink4.ImageUrl = NC.Tables[0].Rows[0]["ImagenTrazabilidad1"].ToString();
                    hyperlink5.NavigateUrl = NC.Tables[0].Rows[0]["ImagenTrazabilidad2"].ToString();
                    hyperlink5.ImageUrl = NC.Tables[0].Rows[0]["ImagenTrazabilidad2"].ToString();

                    tbFechaRevision.Text = NC.Tables[0].Rows[0]["FechaRevision"].ToString();


                DataSet ds = conexion.Devuelve_datos_referencia(tbReferenciaCarga.Text);
                    tbDescripcionCarga.Text = ds.Tables[0].Rows[0]["Descripcion"].ToString();
                    tbClienteCarga.Text = ds.Tables[0].Rows[0]["Nomproveedor"].ToString();
                    tbNumProveedor.Text = ds.Tables[0].Rows[0]["Proveedor"].ToString();


                //RELLENAR EL RESTO
                if (Convert.ToInt32(tbReferenciaCarga.Text) < 20000000 || Convert.ToInt32(tbReferenciaCarga.Text) > 30000000)
                {
                    ds = conexion.Devuelve_datos_producto_BMS(tbReferenciaCarga.Text);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                    tbMoldeCarga.Text = ds.Tables[0].Rows[0]["Molde"].ToString();
                    tbClienteCarga.Text = ds.Tables[0].Rows[0]["Cliente"].ToString();
                    tbNumProveedor.Text = "0";
                    hyperlink6.ImageUrl = ds.Tables[0].Rows[0]["ImagenPieza"].ToString();
                    hyperlink6.NavigateUrl = ds.Tables[0].Rows[0]["ImagenPieza"].ToString();
                    }
                //RELLENAR CAJAS
                CargaDatosCajasBMS();
                if (NC.Tables[0].Rows[0]["Caja1"].ToString() != "")
                    { DropCaja1.SelectedValue = NC.Tables[0].Rows[0]["Caja1"].ToString(); }
                else { DropCaja1.SelectedValue = "---"; }

                if (NC.Tables[0].Rows[0]["Caja2"].ToString() != "")
                    { DropCaja2.SelectedValue = NC.Tables[0].Rows[0]["Caja2"].ToString(); }
                else { DropCaja2.SelectedValue = "---"; }

                if (NC.Tables[0].Rows[0]["Caja3"].ToString() != "")
                    { DropCaja3.SelectedValue = NC.Tables[0].Rows[0]["Caja3"].ToString(); }
                else { DropCaja3.SelectedValue = "---"; }

                if (NC.Tables[0].Rows[0]["Caja4"].ToString() != "")
                    { DropCaja4.SelectedValue = NC.Tables[0].Rows[0]["Caja4"].ToString(); }
                else { DropCaja4.SelectedValue = "---"; }

                    DataSet ordenes = conexion.devuelve_lista_ordenes(tbReferenciaCarga.Text);
                    if (ordenes.Tables[0].Rows.Count == 1)
                    {

                        tbLote1text.Visible = true;
                        if (NC.Tables[0].Rows[0]["Lote1"].ToString() != "")
                        {
                            tbLote1text.Text = NC.Tables[0].Rows[0]["Lote1"].ToString();
                        }
                        tbLote2text.Visible = true;
                        if (NC.Tables[0].Rows[0]["Lote2"].ToString() != "")
                        {
                            tbLote2text.Text = NC.Tables[0].Rows[0]["Lote2"].ToString();
                        }
                        tbLote3text.Visible = true;
                        if (NC.Tables[0].Rows[0]["Lote3"].ToString() != "")
                        {
                            tbLote3text.Text = NC.Tables[0].Rows[0]["Lote3"].ToString();
                        }
                        tbLote4text.Visible = true;
                        if (NC.Tables[0].Rows[0]["Lote4"].ToString() != "")
                        {
                            tbLote4text.Text = NC.Tables[0].Rows[0]["Lote4"].ToString();
                        }

                        tbLote1.Visible = false;
                        tbLote2.Visible = false;
                        tbLote3.Visible = false;
                        tbLote4.Visible = false;
                    }
                    else
                    {
                        tbLote1.Items.Clear();
                        foreach (DataRow row in ordenes.Tables[0].Rows) { tbLote1.Items.Add(row["ORDEN"].ToString().Trim()); }
                        tbLote1.ClearSelection();
                        tbLote1.SelectedValue = "";
                            if (NC.Tables[0].Rows[0]["Lote1"].ToString() != "")
                                { tbLote1.SelectedValue = NC.Tables[0].Rows[0]["Lote1"].ToString(); }
                       

                        tbLote2.Items.Clear();
                        foreach (DataRow row in ordenes.Tables[0].Rows) { tbLote2.Items.Add(row["ORDEN"].ToString().Trim()); }
                        tbLote2.ClearSelection();
                        tbLote2.SelectedValue = "";
                        if (NC.Tables[0].Rows[0]["Lote2"].ToString() != "")
                            { tbLote2.SelectedValue = NC.Tables[0].Rows[0]["Lote2"].ToString(); }

                        tbLote3.Items.Clear();
                        foreach (DataRow row in ordenes.Tables[0].Rows) { tbLote3.Items.Add(row["ORDEN"].ToString().Trim()); }
                        tbLote3.ClearSelection();
                        tbLote3.SelectedValue = "";
                        if (NC.Tables[0].Rows[0]["Lote3"].ToString() != "")
                        { tbLote3.SelectedValue = NC.Tables[0].Rows[0]["Lote3"].ToString(); }

                        tbLote4.Items.Clear();
                        foreach (DataRow row in ordenes.Tables[0].Rows) { tbLote4.Items.Add(row["ORDEN"].ToString().Trim()); }
                        tbLote4.ClearSelection();
                        tbLote4.SelectedValue = "";
                        if (NC.Tables[0].Rows[0]["Lote4"].ToString() != "")
                        { tbLote4.SelectedValue = NC.Tables[0].Rows[0]["Lote4"].ToString(); }
                    }
                RellenaGridsProductosCliente();
                RellenaGridsOperariosVinculados(Convert.ToInt32(NOCONFORMIDAD));
                }
                else
                {

                    RellenaGridsProductosProveedor();
                }

                DataTable LineasContencion = conexion.Devuelve_Lineas_Contencion_NC(Convert.ToInt32(tbNoConformidad.Text), tbFechaOriginal.Text);
                DgvContencion.DataSource = LineasContencion;
                DgvContencion.DataBind();
               

                DataTable LineasEvidencias = conexion.Devuelve_evidencias_documentoNC(Convert.ToInt32(tbNoConformidad.Text));
                dgvEvidencias.DataSource = LineasEvidencias;
                dgvEvidencias.DataBind();

                DataTable EstadoProducto = SHConexion.Devuelve_estado_referencia_NC(tbReferenciaCarga.Text);
                if (EstadoProducto.Rows.Count > 0)
                {
                    lblEstadoGP12.InnerText = EstadoProducto.Rows[0]["Razon"].ToString();
                    lblEstadoGP12Desde.InnerText = "Desde: " + EstadoProducto.Rows[0]["DESDE"].ToString().Substring(0, 10);
                    if (lblEstadoGP12.InnerText != "Sin Revisión")
                    {
                        lblEstadoGP12Hasta.InnerText = "Hasta: " + EstadoProducto.Rows[0]["HASTA"].ToString().Substring(0, 10);
                    }
                }

                if (LineasEvidencias.Rows.Count == 0)
                { AUXMODALMSA.Visible = true; }
   
            }
            catch (Exception ex) 
            { 
            }

        }

        //CARGAS DE BLOQUES INDIVIDUALES
        public void CargaDatosReferencia(object sender, EventArgs e)
        {
            try 
            {
                hyperlink6.ImageUrl = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";
                hyperlink6.NavigateUrl = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";
                tbCantidadStock.Text = "";


                Conexion_CALIDAD conexion = new Conexion_CALIDAD();
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                DataSet ds = conexion.Devuelve_datos_referencia(tbReferenciaCarga.Text);
                tbDescripcionCarga.Text = ds.Tables[0].Rows[0]["Descripcion"].ToString();
                tbClienteCarga.Text = ds.Tables[0].Rows[0]["Nomproveedor"].ToString();
                tbNumProveedor.Text = ds.Tables[0].Rows[0]["Proveedor"].ToString();
                
                if (Convert.ToInt32(tbReferenciaCarga.Text) < 20000000 || Convert.ToInt32(tbReferenciaCarga.Text) > 30000000)
                {
                    ds = conexion.Devuelve_datos_producto_BMS(tbReferenciaCarga.Text);
                    tbMoldeCarga.Text = ds.Tables[0].Rows[0]["Molde"].ToString();
                    tbClienteCarga.Text = ds.Tables[0].Rows[0]["Cliente"].ToString();
                    tbNumProveedor.Text = "0";
                    hyperlink6.ImageUrl = ds.Tables[0].Rows[0]["ImagenPieza"].ToString();
                    hyperlink6.NavigateUrl = ds.Tables[0].Rows[0]["ImagenPieza"].ToString();

                    DataSet ordenes = conexion.devuelve_lista_ordenes(tbReferenciaCarga.Text);
                        if (ordenes.Tables[0].Rows.Count == 1)
                        {
                        //TbLoteManual.Visible = true;
                        //TbLote.Visible = false;
                        }
                        else
                        {
                        tbLote1.Items.Clear();
                        foreach (DataRow row in ordenes.Tables[0].Rows) { tbLote1.Items.Add(row["ORDEN"].ToString()); }
                        tbLote1.ClearSelection();
                        tbLote1.SelectedValue = "";

                        tbLote2.Items.Clear();
                        foreach (DataRow row in ordenes.Tables[0].Rows) { tbLote2.Items.Add(row["ORDEN"].ToString()); }
                        tbLote2.ClearSelection();
                        tbLote2.SelectedValue = "";

                        tbLote3.Items.Clear();
                        foreach (DataRow row in ordenes.Tables[0].Rows) { tbLote3.Items.Add(row["ORDEN"].ToString()); }
                        tbLote3.ClearSelection();
                        tbLote3.SelectedValue = "";

                        tbLote4.Items.Clear();
                        foreach (DataRow row in ordenes.Tables[0].Rows) { tbLote4.Items.Add(row["ORDEN"].ToString()); }
                        tbLote4.ClearSelection();
                        tbLote4.SelectedValue = "";
                    }
                    RellenaGridsProductosCliente();
                    CargaDatosCajasBMS();
                }
                else
                {
                    RellenaGridsProductosProveedor();
                }
                if (tbCantidadStock.Text == "")
                {
                    DataSet DTSTOCK = SHConexion.Devuelve_stock_cuarentena(tbReferenciaCarga.Text);
                    int SumaStock = 0;
                    foreach (DataRow row in DTSTOCK.Tables[0].Rows)
                    {
                        SumaStock += Convert.ToInt32(row["CANTALM"]);

                    }

                    //tbCantidadStock.Text = (Convert.ToInt32(ds.Tables[0].Rows[0]["CANTALM"]).ToString());
                    tbCantidadStock.Text = SumaStock.ToString();
                }

                tbFechaOriginal.Text = DateTime.Now.ToString("dd/MM/yyy HH:mm");

            }
            catch (Exception)
            { }
         }

        private void CargaDatosCajasBMS()
        {
            try
            {
                if (tbMoldeCarga.Text != "")
                {
                    Conexion_CALIDAD conexion = new Conexion_CALIDAD();
                    DataSet cajas = conexion.Devuelve_Cajas_AfectadasXMolde(tbMoldeCarga.Text);

                    DropCaja1.Items.Clear();
                    DropCaja1.Items.Add("---");
                    foreach (DataRow row in cajas.Tables[0].Rows) { DropCaja1.Items.Add(row["C_SHORT_DESCR"].ToString()); }
                    DropCaja1.ClearSelection();
                    DropCaja1.SelectedValue = "---";

                    DropCaja2.Items.Clear();
                    DropCaja2.Items.Add("---");
                    foreach (DataRow row in cajas.Tables[0].Rows) { DropCaja2.Items.Add(row["C_SHORT_DESCR"].ToString()); }
                    DropCaja2.ClearSelection();
                    DropCaja2.SelectedValue = "---";

                    DropCaja3.Items.Clear();
                    DropCaja3.Items.Add("---");
                    foreach (DataRow row in cajas.Tables[0].Rows) { DropCaja3.Items.Add(row["C_SHORT_DESCR"].ToString()); }
                    DropCaja3.ClearSelection();
                    DropCaja3.SelectedValue = "---";

                    DropCaja4.Items.Clear();
                    DropCaja4.Items.Add("---");
                    foreach (DataRow row in cajas.Tables[0].Rows) { DropCaja4.Items.Add(row["C_SHORT_DESCR"].ToString()); }
                    DropCaja4.ClearSelection();
                    DropCaja4.SelectedValue = "---";
                }
            }
            catch (Exception)
            { }
        }
        private void RellenaGridsProductosCliente()
        {
            try
            {
                Conexion_CALIDAD conexion = new Conexion_CALIDAD();
                DataSet ds_afectadas = conexion.Devuelve_moldesXreferencia(tbMoldeCarga.Text);
                    dgv_afectadas.DataSource = ds_afectadas;
                    dgv_afectadas.DataBind();

                DataSet ds_operarios = conexion.Cargar_operarios_produccion(tbMoldeCarga.Text);
                dgv_operarios_informados.DataSource = ds_operarios;
                dgv_operarios_informados.DataBind();
            }
            catch (Exception)
            { }
        }
        
        private void RellenaGridsProductosProveedor()
        {
            try
            {
                Conexion_CALIDAD conexion = new Conexion_CALIDAD();
                DataSet ds_afectadas = conexion.Devuelve_Afectados_MaterialesXreferencia(tbReferenciaCarga.Text);
                dgv_afectadas.DataSource = ds_afectadas;
                dgv_afectadas.DataBind();

            }
            catch (Exception)
            { }
        }

        private void RellenaGridsOperariosVinculados(int nonformidad)
        {
            try
            {
                Conexion_CALIDAD conexion = new Conexion_CALIDAD();
                ds_operarios = conexion.Devuelve_operarios_vinculados_NC_SMARTH(nonformidad);
                dgv_operarios_informados.DataSource = ds_operarios;
                dgv_operarios_informados.DataBind();
            }
            catch (Exception)
            { }
        }

        //GUARDAR DATOS
        public void GuardarAlerta(Object sender, EventArgs e)
        {
            try
            {
                if (tbDescripcionCarga.Text == "")
                {
                    CargaDatosReferencia(null, null);
                }
                string Lote1 = "";

                if (tbLote1text.Visible == true)
                { Lote1 = tbLote1text.Text; }
                else
                { Lote1 = tbLote1.SelectedValue.TrimEnd(); }

                string Lote2 = "";
                if (tbLote2text.Visible == true)
                { Lote2 = tbLote2text.Text; }
                else
                { Lote2 = tbLote2.SelectedValue.TrimEnd(); }

                string Lote3 = "";
                if (tbLote3text.Visible == true)
                { Lote3 = tbLote3text.Text; }
                else
                { Lote3 = tbLote3.SelectedValue.TrimEnd(); }

                string Lote4 = "";
                if (tbLote4text.Visible == true)
                { Lote4 = tbLote4text.Text; }
                else
                { Lote4 = tbLote4.SelectedValue.TrimEnd(); }

                int CantidadStock = 0;
                int CantidadPPM = 0;
                int CantidadReclamada = 0;
                try
                {
                    CantidadStock = Convert.ToInt32(tbCantidadStock.Text);
                }
                catch (Exception)
                {
                }
                try
                {
                    CantidadPPM = Convert.ToInt32(tbPPM.Text);
                }
                catch (Exception)
                {
                }
                try
                {
                    CantidadReclamada = Convert.ToInt32(tbCantidad.Text);
                }
                catch (Exception)
                {
                }
                if (tbFechaOriginal.Text == "")
                {
                    tbFechaOriginal.Text = DateTime.Now.ToString("dd/MM/yyy HH:mm");
                }
                else
                {
                    tbFechaRevision.Text = DateTime.Now.ToString("dd/MM/yyy HH:mm");
                }

                Conexion_CALIDAD conexion = new Conexion_CALIDAD();
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();

                if (!conexion.existe_alerta(tbNoConformidad.Text)) //ALERTA NUEVA
                {
                    if (tbReferenciaCarga.Text != "")
                    {
                        //CREAR ALERTA DE CALIDAD
                        int numalerta = conexion.Devuelve_Ultima_AlertaCalidad();
                        numalerta = ++numalerta;

                        int DEFRepetitivo = 0;
                        if (tbRepetitivoSN.Checked == true)
                        {
                            DEFRepetitivo = 1;
                        }
                        int REFRepetitiva = 0;
                        var myDate = DateTime.Now;
                        var newDate = myDate.AddYears(-1);
                        if (conexion.Devuelve_Recurrencia_Producto(tbReferenciaCarga.Text, newDate.ToString()) > 0)
                        {
                            REFRepetitiva = 1;
                        }

                        conexion.InsertarAlertaCalidad(numalerta, TipoAlerta.SelectedIndex, NivelAlerta.SelectedIndex, CantidadStock, CantidadReclamada, CantidadPPM, Lote1, Lote2, Lote3, Lote4, tbProblemaNC.Text, tbContramedidaPROD.Text, tbContramedidaCAL.Text, tbContramedidaING.Text, tbObservacionesNC.Text,
                                                       Convert.ToInt32(tbReferenciaCarga.Text), conexion.devuelve_ID_Piloto_SMARTH(DropProduccion.SelectedValue), conexion.devuelve_ID_Piloto_SMARTH(DropCalidad.SelectedValue), conexion.devuelve_ID_Piloto_SMARTH(DropIngenieria.SelectedValue), tbFechaOriginal.Text, tbFechaRevision.Text, hyperlink1.ImageUrl, hyperlink2.ImageUrl, hyperlink3.ImageUrl, hyperlink4.ImageUrl, hyperlink5.ImageUrl, tbProcesoAfectado.Text, tbNotas.Text, tbNoConformidadCliente.Text, DropCaja1.SelectedValue.ToString(), DropCaja2.SelectedValue.ToString(), DropCaja3.SelectedValue.ToString(), DropCaja4.SelectedValue.ToString(), DEFRepetitivo, REFRepetitiva);
                        tbNoConformidad.Text = numalerta.ToString();
                                              
                        DateTime D3 = DateTime.Parse(tbFechaOriginal.Text);
                        //Revisar con tipos de NC 
                        int CheckCorte = 1;
                        int CheckD3 = 1;
                        int CheckD6 = 1;
                        int CheckD8 = 1;

                        if (TipoAlerta.SelectedValue == "3")
                        {
                            CheckCorte = 0;
                            CheckD3 = 1;
                            CheckD6 = 1;
                            CheckD8 = 1;
                        }

                        //CREAR HITOS 8D
                        conexion.InsertarNoConformidad(numalerta, tbFechaOriginal.Text, D3.AddDays(1).ToString("dd/MM/yyyy"), D3.AddDays(10).ToString("dd/MM/yyyy"), D3.AddDays(60).ToString("dd/MM/yyyy"), CheckCorte, CheckD3, CheckD6, CheckD8);
                        
                        //INSERTAR DISTRIBUCIÓN
                        DataSet ds_operarios = conexion.DevuelveLEE_operarios_vinculados_NCBMS_SMARTH(tbMoldeCarga.Text, Convert.ToInt32(tbNoConformidad.Text));
                        dgv_operarios_informados.DataSource = ds_operarios;
                        dgv_operarios_informados.DataBind();

                        //CREAR CONTENCIÓN
                        DataSet Cuarentena = SHConexion.Devuelve_stock_cuarentena(tbReferenciaCarga.Text);
                        if (Cuarentena.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in Cuarentena.Tables[0].Rows)
                            {
                                conexion.Insertar_Cuarentena_NC(Convert.ToInt32(tbReferenciaCarga.Text), row["Location Code"].ToString(), row["Name"].ToString(), Convert.ToInt32(row["CANTALM"]), Convert.ToInt32(tbNoConformidad.Text), conexion.devuelve_ID_Piloto_SMARTH(DropCalidad.SelectedValue), tbFechaOriginal.Text);
                            }
                        }

                        //CAMBIAR PRODUCTO A GP12 y MODIFICAR ETIQUETA
                        DataTable ProductosEstados = conexion.Devuelve_estadoProductosGP12(tbReferenciaCarga.Text);
                        conexion.Actualiza_Estado_Producto_NCGP12(tbReferenciaCarga.Text, numalerta.ToString(),conexion.Devuelve_RazonRevisionGP12(ProductosEstados.Rows[0]["EstadoActual"].ToString()), ProductosEstados.Rows[0]["Fecharev"].ToString());
                        conexion.Actualizar_productosBMS(Convert.ToInt32(tbReferenciaCarga.Text), 1);

                        //CREAR PLAN DE ACCIÓN
                        int idPDCA = SHConexion.Devuelve_MAXID_PDCA_Principal_NC();
                        int Modo = 1;
                        string Referencia = tbReferenciaCarga.Text;
                        DataTable RefSeleccionada = SHConexion.Devuelve_Producto_Seleccionado_NUEVO_PDCA_NC(Referencia);
                        string DescripcionProducto = RefSeleccionada.Rows[0]["Descripcion"].ToString();
                        string Cliente = RefSeleccionada.Rows[0]["Cliente"].ToString();
                        string Molde = RefSeleccionada.Rows[0]["Molde"].ToString();
                        SHConexion.Insertar_Nuevo_PlanAcciones_NC(idPDCA, 5, Referencia, DescripcionProducto, Molde, Modo, "No conformidad "+ numalerta + ":"+ tbProblemaNC.Text + "", Cliente, 3, conexion.devuelve_ID_Piloto_SMARTH(DropIngenieria.SelectedValue), DateTime.Now.ToString("dd/MM/yyyy"), 2, 0, numalerta);

                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "falta_referencia();", true);
                    }
                }
                else
                {
                    int DEFRepetitivo = 0;
                    if (tbRepetitivoSN.Checked == true)
                    {
                        DEFRepetitivo = 1;
                    }
                    int REFRepetitiva = 0;
                    if (tbProdRepetitivo.Visible == true)
                    {
                        REFRepetitiva = 1;
                    }

                    conexion.GuardaAlertaCalidad(Convert.ToInt32(tbNoConformidad.Text), TipoAlerta.SelectedIndex, NivelAlerta.SelectedIndex, CantidadStock, CantidadReclamada, CantidadPPM, Lote1, Lote2, Lote3, Lote4, tbProblemaNC.Text, tbContramedidaPROD.Text, tbContramedidaCAL.Text, tbContramedidaING.Text, tbObservacionesNC.Text,
                                 Convert.ToInt32(tbReferenciaCarga.Text), conexion.devuelve_ID_Piloto_SMARTH(DropProduccion.SelectedValue), conexion.devuelve_ID_Piloto_SMARTH(DropCalidad.SelectedValue), conexion.devuelve_ID_Piloto_SMARTH(DropIngenieria.SelectedValue), tbFechaOriginal.Text, tbFechaRevision.Text, hyperlink1.ImageUrl, hyperlink2.ImageUrl, hyperlink3.ImageUrl, hyperlink4.ImageUrl, hyperlink5.ImageUrl, tbProcesoAfectado.Text, tbNotas.Text, tbNoConformidadCliente.Text, DropCaja1.SelectedValue.ToString(), DropCaja2.SelectedValue.ToString(), DropCaja3.SelectedValue.ToString(), DropCaja4.SelectedValue.ToString(), DEFRepetitivo, REFRepetitiva);

                    if (!conexion.existe_alertaOP(tbNoConformidad.Text))
                    {
                        conexion.DevuelveLEE_operarios_vinculados_NCBMS_SMARTH(tbMoldeCarga.Text, Convert.ToInt32(tbNoConformidad.Text));
                    }

                    //ACTUALIZAR HITOS ALERTA DE CALIDAD

                    string SET_D6 = "";
                    string SET_D8 = "";

                    if (Check_D6.Checked)
                    {
                        try
                        {
                            SET_D6 = ", D6 = '" + Convert.ToDateTime(D6Prevision.Value).ToString("dd/MM/yyyy") + "', CheckD6 = 1";
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    else
                    {
                        SET_D6 = ", D6 = '', CheckD6 = 0";
                    }
                    if (Check_D8.Checked)
                    {
                        try
                        {
                            SET_D8 = ", D8 = '" + Convert.ToDateTime(D8Prevision.Value).ToString("dd/MM/yyyy") + "', CheckD8 = 1";
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    else
                    {
                        SET_D8 = ", D8 = '', CheckD8 = 0";
                    }
                    try
                    {

                        Convert.ToDouble(TbSeleccionExterno.Value.Replace(',', '.'));
                        Convert.ToDouble(TbPiezasNOKExterno.Value.Replace(',', '.'));
                        Convert.ToDouble(TbCargosExterno.Value.Replace(',', '.'));
                        Convert.ToDouble(TbAdministrativosExterno.Value.Replace(',', '.'));
                        Convert.ToDouble(tbSeleccionInterno.Value.Replace(',', '.'));
                        Convert.ToDouble(tbOtrosInterno.Value.Replace(',', '.'));
                        string SQL = " [CosteSeleccionEXT] = " + TbSeleccionExterno.Value.Replace(",", ".") + " ,[CostePiezasNOKEXT] = " + TbPiezasNOKExterno.Value.Replace(",", ".") + ",[CosteCargosEXT] = " + TbCargosExterno.Value.Replace(",", ".") + ",[CosteAdmonEXT] = " + TbAdministrativosExterno.Value.Replace(",", ".") + ",[CosteSeleccionINT]= " + tbSeleccionInterno.Value.Replace(",", ".") + ",[CosteOtrosINT]= " + tbOtrosInterno.Value.Replace(",", ".") + "" + SET_D6 + SET_D8;
                        conexion.Actualiza_D3D6D8_NoConformidades(Convert.ToInt32(tbNoConformidad.Text), SQL);
                        
                    }
                    catch (Exception ex)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "error_cargos();", true);

                    }

                    //ACTUALIZAR FECHAORIGINAL DE CONTENCION
                    conexion.Actualiza_Fecha_Inicial_Contencion(Convert.ToInt32(tbNoConformidad.Text), tbFechaOriginal.Text);
                }
               
                CargarDatosNoConformidad(tbNoConformidad.Text);
                ScriptManager.RegisterStartupScript(this, typeof(string), "confirm", "DistribuirMOD();", true);
               
            }
            catch (Exception)
            {
            }
        }

        /*
        public void AUXInsertarAlerta(Object sender, EventArgs e)
        {
            try
            {
                if (tbDescripcionCarga.Text == "")
                {
                    CargaDatosReferencia(null, null);
                }
                string Lote1 = "";

                if (tbLote1text.Visible == true)
                { Lote1 = tbLote1text.Text; }
                else
                { Lote1 = tbLote1.SelectedValue.TrimEnd(); }

                string Lote2 = "";
                if (tbLote2text.Visible == true)
                { Lote2 = tbLote2text.Text; }
                else
                { Lote2 = tbLote2.SelectedValue.TrimEnd(); }

                string Lote3 = "";
                if (tbLote3text.Visible == true)
                { Lote3 = tbLote3text.Text; }
                else
                { Lote3 = tbLote3.SelectedValue.TrimEnd(); }

                string Lote4 = "";
                if (tbLote4text.Visible == true)
                { Lote4 = tbLote4text.Text; }
                else
                { Lote4 = tbLote4.SelectedValue.TrimEnd(); }

                int CantidadStock = 0;
                int CantidadPPM = 0;
                int CantidadReclamada = 0;
                try
                {
                    CantidadStock = Convert.ToInt32(tbCantidadStock.Text);
                }
                catch (Exception)
                {
                }
                try
                {
                    CantidadPPM = Convert.ToInt32(tbPPM.Text);
                }
                catch (Exception)
                {
                }
                try
                {
                    CantidadReclamada = Convert.ToInt32(tbCantidad.Text);
                }
                catch (Exception)
                {
                }
                if (tbFechaOriginal.Text == "")
                {
                    tbFechaOriginal.Text = DateTime.Now.ToString("dd/MM/yyy HH:mm");
                }
                else
                {
                    tbFechaRevision.Text = DateTime.Now.ToString("dd/MM/yyy HH:mm");
                }

                Conexion_CALIDAD conexion = new Conexion_CALIDAD();

                if (!conexion.existe_alerta(tbNoConformidad.Text))
                {
                    if (tbReferenciaCarga.Text != "")
                    {
                        int numalerta = conexion.Devuelve_Ultima_AlertaCalidad();

                        int DEFRepetitivo = 0;
                        if (tbRepetitivoSN.Checked == true)
                        {
                            DEFRepetitivo = 1;
                        }
                        int REFRepetitiva = 0;
                        var myDate = DateTime.Now;
                        var newDate = myDate.AddYears(-1);
                        if (conexion.Devuelve_Recurrencia_Producto(tbReferenciaCarga.Text,newDate.ToString()) > 0)
                        {
                            REFRepetitiva = 1;
                        }

                        numalerta = ++numalerta;
                        
                        conexion.InsertarAlertaCalidad(numalerta, TipoAlerta.SelectedIndex, NivelAlerta.SelectedIndex, CantidadStock, CantidadReclamada, CantidadPPM, Lote1, Lote2, Lote3, Lote4, tbProblemaNC.Text, tbContramedidaPROD.Text, tbContramedidaCAL.Text, tbContramedidaING.Text, tbObservacionesNC.Text,
                                                       Convert.ToInt32(tbReferenciaCarga.Text), conexion.devuelve_ID_Piloto_SMARTH(DropProduccion.SelectedValue), conexion.devuelve_ID_Piloto_SMARTH(DropCalidad.SelectedValue), conexion.devuelve_ID_Piloto_SMARTH(DropIngenieria.SelectedValue), tbFechaOriginal.Text, tbFechaRevision.Text, hyperlink1.ImageUrl, hyperlink2.ImageUrl, hyperlink3.ImageUrl, hyperlink4.ImageUrl, hyperlink5.ImageUrl, tbProcesoAfectado.Text, tbNotas.Text, tbNoConformidadCliente.Text, DropCaja1.SelectedValue.ToString(), DropCaja2.SelectedValue.ToString(), DropCaja3.SelectedValue.ToString(), DropCaja4.SelectedValue.ToString(),DEFRepetitivo, REFRepetitiva);
                        tbNoConformidad.Text = numalerta.ToString();
                        //conexion.leer_operarios_vinculados_NCBMS(tbMoldeCarga.Text, Convert.ToInt32(tbNoConformidad.Text));

                        DataSet ds_operarios = conexion.DevuelveLEE_operarios_vinculados_NCBMS_SMARTH(tbMoldeCarga.Text, Convert.ToInt32(tbNoConformidad.Text));
                        dgv_operarios_informados.DataSource = ds_operarios;
                        dgv_operarios_informados.DataBind();

                        DateTime D3 = DateTime.Parse(tbFechaOriginal.Text);
                        //Revisar con tipos de NC 
                        int CheckCorte = 1;
                        int CheckD3 = 1;
                        int CheckD6 = 1;
                        int CheckD8 = 1;

                        if (TipoAlerta.SelectedValue == "3")
                        {
                            CheckCorte = 0;
                            CheckD3 = 1;
                            CheckD6 = 1;
                            CheckD8 = 1;
                        }

                        conexion.InsertarNoConformidad(numalerta, tbFechaOriginal.Text, D3.AddDays(1).ToString("dd/MM/yyyy"), D3.AddDays(10).ToString("dd/MM/yyyy"), D3.AddDays(60).ToString("dd/MM/yyyy"), CheckCorte, CheckD3, CheckD6, CheckD8);
                        
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "falta_referencia();", true);
                    }
                }
                CargarDatosNoConformidad(tbNoConformidad.Text);

            }
            catch (Exception)
            {
            }
        }

       */

        public void InsertarStockCuarentena(object sender, EventArgs e)
        {
            try
            {
                Conexion_CALIDAD conexion = new Conexion_CALIDAD();
                string CodAlmacen = conexion.Devuelve_Codigo_Almacen(DropAlmacenContencion.SelectedValue);
                switch (DropAlmacenContencion.SelectedValue)
                {
                    case "En tránsito":
                        CodAlmacen = "TRANS";
                        break;
                    case "En cliente":
                        CodAlmacen = "CLNT";
                        break;
                    default:break;
                }
                conexion.Insertar_Cuarentena_NC(Convert.ToInt32(tbReferenciaCarga.Text), CodAlmacen, DropAlmacenContencion.SelectedValue, Convert.ToInt32(tbCantidadContencion.Value), Convert.ToInt32(tbNoConformidad.Text), conexion.devuelve_ID_Piloto_SMARTH(DropCalidad.SelectedValue), tbFechaOriginal.Text);
            }
            catch (Exception ex)
            { 
            }
        }

        public void AuxiliarCuarentena(object sender, EventArgs e)
        {
            Conexion_CALIDAD conexion = new Conexion_CALIDAD();
            Conexion_SMARTH SHConexion = new Conexion_SMARTH();
            DataSet Cuarentena = SHConexion.Devuelve_stock_cuarentena(tbReferenciaCarga.Text);
            if (Cuarentena.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in Cuarentena.Tables[0].Rows)
                {
                    conexion.Insertar_Cuarentena_NC(Convert.ToInt32(tbReferenciaCarga.Text), row["Location Code"].ToString(), row["Name"].ToString(), Convert.ToInt32(row["CANTALM"]), Convert.ToInt32(tbNoConformidad.Text), conexion.devuelve_ID_Piloto_SMARTH(DropCalidad.SelectedValue), tbFechaOriginal.Text);
                }
            }
            else
            {
                conexion.Insertar_Cuarentena_NC(Convert.ToInt32(tbReferenciaCarga.Text), "1", "Thermolympic", 0, Convert.ToInt32(tbNoConformidad.Text), conexion.devuelve_ID_Piloto_SMARTH(DropCalidad.SelectedValue), tbFechaOriginal.Text);
            }
        }

        public void AuxiliarGP12(object sender, EventArgs e)
        {
            try
            {
                Conexion_CALIDAD conexion = new Conexion_CALIDAD();
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                //CAMBIAR PRODUCTO A GP12 y MODIFICAR ETIQUETA
                DataTable ProductosEstados = conexion.Devuelve_estadoProductosGP12(tbReferenciaCarga.Text);
                conexion.Actualiza_Estado_Producto_NCGP12(tbReferenciaCarga.Text, tbNoConformidad.Text, conexion.Devuelve_RazonRevisionGP12(ProductosEstados.Rows[0]["EstadoActual"].ToString()), ProductosEstados.Rows[0]["Fecharev"].ToString());
                conexion.Actualizar_productosBMS(Convert.ToInt32(tbReferenciaCarga.Text), 1);
            }
            catch (Exception ex)
            { }
        }





        //INFORMADORES
        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Conexion_CALIDAD conexion = new Conexion_CALIDAD();
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                DropDownList newOperario = (DropDownList)e.Row.FindControl("newOperario");
                newOperario.Items.Clear();
                newOperario.Items.Add("");
                DataSet dsOP = conexion.devuelve_Set_Operarios_NAV();
                foreach (DataRow row in dsOP.Tables[0].Rows) { newOperario.Items.Add(row["Search Name"].ToString()); }
                newOperario.ClearSelection();
                newOperario.SelectedValue = "";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                              
                //if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                if (e.Row.RowState.HasFlag(DataControlRowState.Edit))

                {

                    DropDownList txtFormador = (DropDownList)e.Row.FindControl("txtFormador");
                    DataTable dt2 = conexion.devuelve_setlista_informadores();
                    txtFormador.DataSource = dt2;
                    txtFormador.DataTextField = "PInformadores";
                    txtFormador.DataValueField = "PInformadores";
                    txtFormador.DataBind();
                    DataRowView dr2 = e.Row.DataItem as DataRowView;
                    txtFormador.SelectedValue = dr2["FORMNOMBRE"].ToString();

                    /*for (int i = 0; i <= dgv_AreaRechazo.Rows.Count - 1; i++)
                    {
                        Label lblparent = (Label)dgv_AreaRechazo.Rows[i].FindControl("lblMalas");

                        if (Convert.ToDateTime(lblparent.Text)
                            
                            )
                        {
                            dgv_AreaRechazo.Rows[i].Cells[9].BackColor = System.Drawing.Color.Red;
                            lblparent.ForeColor = System.Drawing.Color.White;
                        }

                        Label lblparent2 = (Label)dgv_AreaRechazo.Rows[i].FindControl("lblRetrabajadas");

                    }*/

                }
            }
        }
     
        public void GridView_RowEditing(Object sender, GridViewEditEventArgs e)
        {
            dgv_operarios_informados.EditIndex = e.NewEditIndex;
            dgv_operarios_informados.DataSource = ds_operarios;
            dgv_operarios_informados.DataBind();
            Lkb_Sort_Click("FORMACION");
        }
        public void GridView_RowUpdating(Object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                //string Firma = signatureJSON.Value.ToString();
                Label operario = (Label)dgv_operarios_informados.Rows[e.RowIndex].FindControl("txtNOperario");
                DropDownList formador = (DropDownList)dgv_operarios_informados.Rows[e.RowIndex].FindControl("txtFormador");
                Conexion_CALIDAD conexion = new Conexion_CALIDAD();
                conexion.GuardaFormacionOperario(Convert.ToInt32(tbNoConformidad.Text), Convert.ToInt32(operario.Text), conexion.devuelve_IDlista_Formadores_SMARTH(Convert.ToString(formador.SelectedValue)), DateTime.Now.ToString("dd/MM/yyy HH:mm"), signatureJSON.Value.ToString());
                dgv_operarios_informados.EditIndex = -1;
                RellenaGridsOperariosVinculados(Convert.ToInt32(tbNoConformidad.Text));

            }
            catch (Exception)
            {

            }
        }
        protected void GridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            dgv_operarios_informados.EditIndex = -1;
            dgv_operarios_informados.DataSource = ds_operarios;
            dgv_operarios_informados.DataBind();
            Lkb_Sort_Click("FORMACION");
        }

        public void ForzarOperarios(object sender, EventArgs e)
        {
            try
            {
                Conexion_CALIDAD conexion = new Conexion_CALIDAD();
                conexion.DevuelveLEE_operarios_vinculados_NCBMS_SMARTH(tbMoldeCarga.Text, Convert.ToInt32(tbNoConformidad.Text));
            }
            catch (Exception ex)
            { }
        }
        //COMANDOS DE GRIDVIEW
        public void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Conexion_CALIDAD conexion = new Conexion_CALIDAD();
            if (e.CommandName.Equals("AddNew"))
            {


                DropDownList newOperario = (DropDownList)dgv_operarios_informados.FooterRow.FindControl("newOperario");
                if (!conexion.existeNCXOperario(Convert.ToInt32(tbNoConformidad.Text), conexion.devuelve_IDOperario_NAV(newOperario.SelectedValue.ToString())))
                {

                    conexion.InsertarOperarioNoConformidad(Convert.ToInt32(tbNoConformidad.Text), conexion.devuelve_IDOperario_NAV(newOperario.SelectedValue.ToString()));
                    CargarDatosNoConformidad(tbNoConformidad.Text);
                }
            }
            if (e.CommandName.Equals("GuardarPTLimpio"))
            {
                // int indice = Convert.ToInt32(e.CommandArgument);

                GridViewRow gvrow = (GridViewRow)((System.Web.UI.Control)e.CommandSource).NamingContainer;

                HtmlInputCheckBox CheckPuntoLimpio = (HtmlInputCheckBox)gvrow.FindControl("CheckPtLimpio");
                TextBox NotasCorte = (TextBox)gvrow.FindControl("txtNotas");
                Label Id = (Label)gvrow.FindControl("IdRev");
                conexion.Actualiza_Lineas_Contencion_NC(Convert.ToInt32(Id.Text), Convert.ToInt32(CheckPuntoLimpio.Checked), NotasCorte.Text);
                DataTable LineasContencion = conexion.Devuelve_Lineas_Contencion_NC(Convert.ToInt32(tbNoConformidad.Text), tbFechaOriginal.Text);
                DgvContencion.DataSource = LineasContencion;
                DgvContencion.DataBind();

                DataTable LineasEvidencias = conexion.Devuelve_evidencias_documentoNC(Convert.ToInt32(tbNoConformidad.Text));
                dgvEvidencias.DataSource = LineasEvidencias;
                dgvEvidencias.DataBind();
            }
            if (e.CommandName.Equals("NuevoEvidencia"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "AbreEvidencia();", true);
            }
            if (e.CommandName.Equals("Bajar_evidencia"))
            {
                Response.Redirect(e.CommandArgument.ToString());
            }
            if (e.CommandName.Equals("Eliminar_evidencia"))
            {
                conexion.Eliminar_EvidenciasNCBD(e.CommandArgument.ToString());
                CargarDatosNoConformidad(tbNoConformidad.Text);
            }
            if (e.CommandName.Equals("NuevoStock"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "AbreModalContencion();", true);
            }
        }
       
        //GESTION DE PESTAÑAS
        protected void Lkb_Sort_Click(string e)
        {
            //Bind the data

            //Manage the selected tab.
            this.ManageTabsPostBack(TABALERTA, TABDISTRIBUCION, TABCONTENCION, BTNALERTA, BTNDISTRIBUCION, BTNCONTENCION, e);

        }

        private void ManageTabsPostBack(HtmlGenericControl TABALERTA, HtmlGenericControl TABDISTRIBUCION, HtmlGenericControl TABCONTENCION,
                                        HtmlButton BTNALERTA, HtmlButton BTNDISTRIBUCION, HtmlButton BTNCONTENCION, string grid)
        {
            // desactivte all tabs and panes tab-pane fade

            BTNALERTA.Attributes.Add("class", "nav-link border border-secondary shadow");
            TABALERTA.Attributes.Add("class", "tab-pane fade");

            BTNDISTRIBUCION.Attributes.Add("class", "nav-link border border-secondary shadow ms-1 me-1");
            TABDISTRIBUCION.Attributes.Add("class", "tab-pane fade");

            BTNCONTENCION.Attributes.Add("class", "nav-link border border-secondary shadow ms-1 me-1");
            TABCONTENCION.Attributes.Add("class", "tab-pane fade");

            //activate the the tab and pane the user was viewing
            switch (grid)
            {
                case "ALERTA":
                    BTNALERTA.Attributes.Add("class", "nav-link active border border-secondary shadow");
                    TABALERTA.Attributes.Add("class", "tab-pane fade show active");
                    break;

                case "CONTENCION":
                    BTNCONTENCION.Attributes.Add("class", "nav-link active border border-secondary shadow ms-1 me-1");
                    TABCONTENCION.Attributes.Add("class", "tab-pane fade show active");
                    break;

                case "FORMACION":
                    BTNDISTRIBUCION.Attributes.Add("class", "nav-link active border border-secondary shadow ms-1 me-1");
                    TABDISTRIBUCION.Attributes.Add("class", "tab-pane fade show active");
                    break;
            }
        }

        //SUBIR DOCUMENTACIÓN
        public void Insertar_documento(Object sender, EventArgs e)
        {
            try
            {
               //REVISAR ESTA LINEA
                if (tbNoConformidad.Text == "")
                {
                    //AUXInsertarAlerta(null, null);
                }
                else
                { 
                if (UploadEvidencia.HasFile)
                {
                    NewSaveFile(UploadEvidencia.PostedFile, 0);
                }
                if (FileUpload1.HasFile)
                    NewSaveFile(FileUpload1.PostedFile, 1);
                if (FileUpload2.HasFile)
                    NewSaveFile(FileUpload2.PostedFile, 2);
                if (FileUpload3.HasFile)
                    NewSaveFile(FileUpload3.PostedFile, 3);
                if (FileUpload4.HasFile)
                    NewSaveFile(FileUpload4.PostedFile, 4);
                if (FileUpload5.HasFile)
                    NewSaveFile(FileUpload5.PostedFile, 5);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void NewSaveFile(HttpPostedFile file, int numdoc)
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

                string savePath = @"C:\inetpub_thermoweb\SMARTH_docs\NOCONFORMIDADES\" + tbNoConformidad.Text + "\\"; // Your code goes here
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }

                // Get the name of the file to upload.
                
                string fileName = "";
                string extension = "";
                switch (numdoc)
                {
                    case 0:
                        extension = System.IO.Path.GetExtension(UploadEvidencia.PostedFile.FileName);
                        fileName = UploadEvidencia.PostedFile.FileName + "_" + DateTime.Now.ToString("yyyyy-MM-dd_HHmmss") + extension;
                        break;
                    case 1:
                        //fileName = FileUpload1.FileName;
                        extension = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
                        fileName = tbNoConformidad.Text + "_" + DateTime.Now.ToString("yyyyy-MM-dd_HHmmss") + "_OK" + extension;
                        break;
                    case 2:
                        //fileName = FileUpload2.FileName;
                        extension = System.IO.Path.GetExtension(FileUpload2.PostedFile.FileName);
                        fileName = tbNoConformidad.Text + "_" + DateTime.Now.ToString("yyyyy-MM-dd_HHmmss") + "_NOK1" + extension;
                        break;
                    case 3:
                        //fileName = FileUpload3.FileName;
                        extension = System.IO.Path.GetExtension(FileUpload3.PostedFile.FileName);
                        fileName = tbNoConformidad.Text + "_" + DateTime.Now.ToString("yyyyy-MM-dd_HHmmss") + "_NOK2" + extension;
                        break;
                    case 4:
                        //fileName = FileUpload4.FileName;
                        extension = System.IO.Path.GetExtension(FileUpload4.PostedFile.FileName);
                        fileName = tbNoConformidad.Text + "_" + DateTime.Now.ToString("yyyyy-MM-dd_HHmmss") + "_TRAZA1" + extension;
                        break;
                    case 5:
                        //fileName = FileUpload5.FileName;
                        extension = System.IO.Path.GetExtension(FileUpload5.PostedFile.FileName);
                        fileName = tbNoConformidad.Text + "_" + DateTime.Now.ToString("yyyyy-MM-dd_HHmmss") + "_TRAZA2" + extension;
                        break;
                }

                savePath += fileName;

                // Call the SaveAs method to save the uploaded file to the specified directory.
                string savePathVirtual = "";
                switch (numdoc)
                {
                    case 0:
                        UploadEvidencia.SaveAs(savePath);
                        savePathVirtual = "..\\SMARTH_docs\\NOCONFORMIDADES\\" + tbNoConformidad.Text + "\\";
                        savePathVirtual += fileName;
                        InsertarDocumentosBD(savePathVirtual, 0);
                        CargarDatosNoConformidad(tbNoConformidad.Text);
                        break;
                    case 1:
                        FileUpload1.SaveAs(savePath);
                        hyperlink1.ImageUrl = "../SMARTH_docs/NOCONFORMIDADES/" + tbNoConformidad.Text + "/" + fileName;
                        hyperlink1.NavigateUrl = "../SMARTH_docs/NOCONFORMIDADES/" + tbNoConformidad.Text + "/" + fileName;

                        break;
                    case 2:
                        FileUpload2.SaveAs(savePath);
                        hyperlink2.ImageUrl = "../SMARTH_docs/NOCONFORMIDADES/" + tbNoConformidad.Text + "/" + fileName;
                        hyperlink2.NavigateUrl = "../SMARTH_docs/NOCONFORMIDADES/" + tbNoConformidad.Text + "/" + fileName;
                        break;
                    case 3:
                        FileUpload3.SaveAs(savePath);
                        hyperlink3.ImageUrl = "../SMARTH_docs/NOCONFORMIDADES/" + tbNoConformidad.Text + "/" + fileName;
                        hyperlink3.NavigateUrl = "../SMARTH_docs/NOCONFORMIDADES/" + tbNoConformidad.Text + "/" + fileName;
                        break;
                    case 4:
                        FileUpload4.SaveAs(savePath);
                        hyperlink4.ImageUrl = "../SMARTH_docs/NOCONFORMIDADES/" + tbNoConformidad.Text + "/" + fileName;
                        hyperlink4.NavigateUrl = "../SMARTH_docs/NOCONFORMIDADES/" + tbNoConformidad.Text + "/" + fileName;
                        break;
                    case 5:
                        FileUpload5.SaveAs(savePath);
                        hyperlink5.ImageUrl = "../SMARTH_docs/NOCONFORMIDADES/" + tbNoConformidad.Text + "/" + fileName;
                        hyperlink5.NavigateUrl = "../SMARTH_docs/NOCONFORMIDADES/" + tbNoConformidad.Text + "/" + fileName;
                        break;
                    default: break;
                }
                
            }
           
            catch (Exception ex)
            {
            }

        }

        private void InsertarDocumentosBD(string savepath, int tipodoc)
        {
            try
            {
                Conexion_CALIDAD conexion = new Conexion_CALIDAD();
                
                switch (tipodoc)
                {

                    case 0: //SUBIDA DESDE LA PANTALLA DE EVIDENCIAS                        
                        DataTable DOCSESTADO = new DataTable();
                        conexion.Insertar_EvidenciasNCBD(tbNoConformidad.Text, Convert.ToInt32(DropTipoEvidencia.SelectedValue), InputFechaEvidencia.Value, DateTime.Now.ToString("dd/MM/yyyy"), savepath, InputDescripcionEvidencia.Value);
                     
                        //ACTUALIZO CELDA DE NC EN LA ALERTA SI CORRESPONDE
                            switch (Convert.ToInt32(DropTipoEvidencia.SelectedValue))
                            {
                                case 0:

                                    break;
                                case 1://PUNTO DE CORTE                       
                                    conexion.Actualiza_D3D6D8_NoConformidades(Convert.ToInt32(tbNoConformidad.Text), "DOCPuntoCorte = '" + savepath + "'");
                                    break;
                                case 2://D3
                                    DOCSESTADO = conexion.Devuelve_estado_documentoNC(Convert.ToInt32(tbNoConformidad.Text));
                                    if (DOCSESTADO.Rows[0]["DOCD3"].ToString() == "" || DOCSESTADO.Rows[0]["DOCD3"].ToString() == null)
                                    {
                                        conexion.Actualiza_D3D6D8_NoConformidades(Convert.ToInt32(tbNoConformidad.Text), "D3CIERRE = '"+ DateTime.Now.ToString("dd/MM/yyyy") + "', [DOCD3] = '" + savepath + "'");
                                    }
                                    else
                                    {
                                        conexion.Actualiza_D3D6D8_NoConformidades(Convert.ToInt32(tbNoConformidad.Text), "[DOCD3] = '" + savepath + "'");
                                    }
                                    break;
                                case 3://D6
                                    DOCSESTADO = conexion.Devuelve_estado_documentoNC(Convert.ToInt32(tbNoConformidad.Text));
                                    if (DOCSESTADO.Rows[0]["DOCD6"].ToString() == "" || DOCSESTADO.Rows[0]["DOCD6"].ToString() == null)
                                    {
                                        conexion.Actualiza_D3D6D8_NoConformidades(Convert.ToInt32(tbNoConformidad.Text), "D6CIERRE = '" + DateTime.Now.ToString("dd/MM/yyyy") + "', [DOCD6] = '" + savepath + "'");
                                    }
                                    else
                                    {
                                        conexion.Actualiza_D3D6D8_NoConformidades(Convert.ToInt32(tbNoConformidad.Text), "[DOCD6] = '" + savepath + "'");
                                    }
                                    break;
                                case 4://D8
                                    DOCSESTADO = conexion.Devuelve_estado_documentoNC(Convert.ToInt32(tbNoConformidad.Text));
                                    if (DOCSESTADO.Rows[0]["DOCD8"].ToString() == "" || DOCSESTADO.Rows[0]["DOCD8"].ToString() == null)
                                    {
                                        conexion.Actualiza_D3D6D8_NoConformidades(Convert.ToInt32(tbNoConformidad.Text), "D8CIERRE = '" + DateTime.Now.ToString("dd/MM/yyyy") + "', [DOCD8] = '" + savepath + "'");
                                    }
                                    else
                                    {
                                        conexion.Actualiza_D3D6D8_NoConformidades(Convert.ToInt32(tbNoConformidad.Text), "[DOCD8] = '" + savepath + "'");
                                    }
                                    break;
                                case 5:
                                    break;
                                default: break;
                            }
                        break;
                    case 1: //OTROS
                        break;
                    default: break;
                }
                CargarDatosNoConformidad(tbNoConformidad.Text);
            }
            catch (Exception ex)
            {
            }
        }

        //DISTRIBUCIÓN

        public void Imprimir_alerta(Object sender, EventArgs e)
        {
            try
            {
                string filepath = @"\\FACTS4-SRV\Fichas parametros\FPOC-12-10.xlsx";
                FileInfo fileInfo = new FileInfo(filepath);
                ExcelPackage p = new ExcelPackage(fileInfo);
                ExcelWorksheet xlWorkSheet = p.Workbook.Worksheets["ALERTA"];

                // información principal
                if (tbNoConformidad.Text == "") //numero no conformidad
                    { xlWorkSheet.Cells[5, 2].Value = ""; }
                else
                    {   xlWorkSheet.Cells[5, 2].Value = tbNoConformidad.Text;  }

                xlWorkSheet.Cells[5, 5].Value = TipoAlerta.SelectedItem.ToString() +" (" + NivelAlerta.SelectedItem.ToString()+")"; //de proveedor/de cliente

                if (tbDescripcionCarga.Text == "") //descripción referencia  
                    { xlWorkSheet.Cells[6, 2].Value = ""; }
                else
                    { xlWorkSheet.Cells[6, 2].Value = tbDescripcionCarga.Text; }

                if (tbDescripcionCarga.Text == "") //proceso afectado
                    { xlWorkSheet.Cells[6, 5].Value = ""; }
                else
                    { xlWorkSheet.Cells[6, 5].Value = tbProcesoAfectado.Text; }

                if (tbReferenciaCarga.Text == "") //referencia
                    { xlWorkSheet.Cells[7, 2].Value = ""; }
                else
                    { xlWorkSheet.Cells[7, 2].Value = tbReferenciaCarga.Text; }

                if (tbFechaOriginal.Text == "")  //fecha de apertura
                    { xlWorkSheet.Cells[7, 5].Value = ""; }
                else
                    { xlWorkSheet.Cells[7, 5].Value = tbFechaOriginal.Text; }

                if (tbClienteCarga.Text == "")  //cliente
                    { xlWorkSheet.Cells[8, 2].Value = ""; }
                else
                    { xlWorkSheet.Cells[8, 2].Value = tbClienteCarga.Text; }

                if (tbNumProveedor.Text == "" && tbNumProveedor.Enabled == true)  //proveedor
                    { xlWorkSheet.Cells[8, 2].Value = ""; }
                else if (tbNumProveedor.Text != "" && tbNumProveedor.Enabled == true)
                    { xlWorkSheet.Cells[8, 2].Value = tbNumProveedor.Text; }

                if (tbFechaRevision.Text == "")  //fecha de revisión
                    { xlWorkSheet.Cells[8, 5].Value = ""; }
                else
                    { xlWorkSheet.Cells[8, 5].Value = tbFechaRevision.Text; }

                if (tbProblemaNC.Text == "")  //problema
                    { xlWorkSheet.Cells[11, 1].Value = ""; }
                else
                    { xlWorkSheet.Cells[11, 1].Value = tbProblemaNC.Text; }

                if (tbContramedidaPROD.Text == "")  //contramedida produccion
                    { xlWorkSheet.Cells[14, 2].Value = ""; }
                else
                    { xlWorkSheet.Cells[14, 2].Value = tbContramedidaPROD.Text; }

                if (tbContramedidaCAL.Text == "")  //contramedida calidad  
                    { xlWorkSheet.Cells[15, 2].Value = ""; }
                else
                    { xlWorkSheet.Cells[15, 2].Value = tbContramedidaCAL.Text; }

                if (tbContramedidaING.Text == "")  //contramedida ingenieria 
                { xlWorkSheet.Cells[16, 2].Value = ""; }
                else
                { xlWorkSheet.Cells[16, 2].Value = tbContramedidaING.Text; }

                if (tbObservacionesNC.Text == "")   //observaciones no conformidad
                    { xlWorkSheet.Cells[18, 1].Value = ""; }
                else
                    { xlWorkSheet.Cells[18, 1].Value = tbObservacionesNC.Text; }  
                
                //incluir trabajadores vinculado s
                //incluir imagenes
                p.Save();

                // Limpiamos la salida
                Response.Clear();
                // Con esto le decimos al browser que la salida sera descargable
                Response.ContentType = "application/octet-stream";
                // esta linea es opcional, en donde podemos cambiar el nombre del fichero a descargar (para que sea diferente al original)
                Response.AddHeader("Content-Disposition", "attachment; filename=Alerta_de_calidad_NC_" + tbNoConformidad.Text + ".xlsx");
                // Escribimos el fichero a enviar 
                Response.WriteFile(@"\\FACTS4-SRV\Fichas parametros\FPOC-12-10.xlsx");
                // volcamos el stream 
                Response.Flush();
                // Enviamos todo el encabezado ahora
                Response.End();
            }
            catch (Exception)
            {
            }
        }

        protected void MandarMail(object sender, EventArgs e)
        {
            try
            {
                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                    {
                        string URL = "http://facts4-srv/thermogestion/CALIDAD/Alertas_Calidad.aspx?NOCONFORMIDAD=" + tbNoConformidad.Text;
                        //contenedor1.RenderControl(hw);
                        StringReader sr = new StringReader(sw.ToString());
                        //MailMessage mm = new MailMessage("bms@thermolympic.es", "pedro@thermolympic.es");
                        MailMessage mm = new MailMessage();
                        Conexion_CALIDAD conexion = new Conexion_CALIDAD();
                        DataSet ds_correos = conexion.leer_correosCALIDAD();
                        foreach (DataRow row in ds_correos.Tables[0].Rows)
                        {
                            mm.To.Add(new MailAddress(row["Correo"].ToString()));
                        }

                        //var inlineLogo = new LinkedResource(Server.MapPath(""+hyperlink2.ImageUrl.ToString()+""), "image/png");
                        string filename1 = Path.GetFileName(new Uri(Path.GetFullPath(hyperlink2.ImageUrl.ToString())).AbsolutePath);
                        string extension1 = Path.GetExtension(filename1).Substring(1);
                        var inlineLogo = new LinkedResource(Server.MapPath("../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg"));
                        if (filename1 != "sin_imagen.jpg")
                        {
                            inlineLogo = new LinkedResource(Server.MapPath("../SMARTH_docs/NOCONFORMIDADES/" + tbNoConformidad.Text + "/" + filename1 + ""), "image/" + extension1 + "");
                        }
                        inlineLogo.ContentId = Guid.NewGuid().ToString();

                        string filename2 = Path.GetFileName(new Uri(Path.GetFullPath(hyperlink3.ImageUrl.ToString())).AbsolutePath);
                        string extension2 = Path.GetExtension(filename2).Substring(1);
                        var inlineLogo2 = new LinkedResource(Server.MapPath("../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg"));
                        if (filename2 != "sin_imagen.jpg")
                        { 

                            inlineLogo2 = new LinkedResource(Server.MapPath("../SMARTH_docs/NOCONFORMIDADES/" + tbNoConformidad.Text + "/" + filename2 + ""), "image/" + extension2 + "");
                        }
                        inlineLogo2.ContentId = Guid.NewGuid().ToString();
                        
                        
                        //ORIGINAL
                        /*
                        var inlineLogo2 = new LinkedResource(Server.MapPath("~/Imagenes/GP12/21032_NOK2.png"), "image/png");
                        inlineLogo2.ContentId = Guid.NewGuid().ToString();*/


                        //mm.To.Add(new MailAddress("pedro@thermolympic.es"));
                        mm.From = new MailAddress("calidadplanta@thermolympic.es");
                        //mm.To.Add(new MailAddress("pedro@thermolympic.es"));
                        mm.Subject = "NO CONFORMIDAD "+TipoAlerta.SelectedItem.ToString().ToLower()+ " (NC-" + tbNoConformidad.Text + " / "+ tbClienteCarga.Text.TrimEnd()+")";
                        mm.Body = string.Format("<strong>Referencia:</strong> <br />&nbsp " + tbReferenciaCarga.Text + " " + tbDescripcionCarga.Text + "<br>" +
                                  "<strong>Defecto denunciado:</strong> <br />&nbsp " + tbProblemaNC.Text + " <br />" +
                                  "<strong>Cantidad en stock:</strong><br />&nbsp " + tbCantidadStock.Text + " piezas <br /> <br />" +
                                  "<strong>CONTRAMEDIDAS</strong><br />" +
                                  "<strong>Producción:</strong> <br />&nbsp " + tbContramedidaPROD.Text + "<br>" +
                                  "<strong>Calidad:</strong><br />&nbsp " + tbContramedidaCAL.Text + "<br>" +
                                  "<strong>Ingeniería:</strong> <br />&nbsp " + tbContramedidaING.Text + "<br>"+
                                  "<a href =" + URL + "  > Accede a la aplicación para ver el detalle.</a>", inlineLogo.ContentId);
                        var view = AlternateView.CreateAlternateViewFromString(mm.Body, null, "text/html");
                        if (filename1 != "sin_imagen.jpg")
                            {
                            view.LinkedResources.Add(inlineLogo);
                            }
                        if (filename2 != "sin_imagen.jpg")
                            {
                            view.LinkedResources.Add(inlineLogo2);
                            }
                        mm.AlternateViews.Add(view);

                        mm.IsBodyHtml = true;
                        mm.Priority = MailPriority.High;
                        SmtpClient smtp = new SmtpClient { Host = "smtp.thermolympic.es", Port = 25, EnableSsl = false, UseDefaultCredentials = false, Credentials = new NetworkCredential("calidadplanta@thermolympic.es", "010477Cp") };

                        smtp.Send(mm);
                    }
                }
            }

            catch (Exception ex)
            {
            }
        }

    }

}