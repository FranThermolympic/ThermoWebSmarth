using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
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

namespace ThermoWeb.PDCA
{
    public partial class PlandeAccionesDetalles : System.Web.UI.Page
    {

        //private static DataSet ds_DocumentosPlanta = new DataSet();

        private static DataTable PDCATable = new DataTable();
        private static DataTable ListaParteMoldes = new DataTable();
        private static DataTable ListaParteMaquinas = new DataTable();
        private static DataTable ListaAlertasCalidad = new DataTable();
        private static DataTable ListaProductos = new DataTable();
        private static DataTable APPAccionSeleccionada = new DataTable();
        private static DataTable LineaProductosMoldes = new DataTable();
        private static DataTable ListaMaquinas = new DataTable();
        
        private static string FILTRO = "";
        //private int PDCAINT = 2;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                SHConexion.LimpiarOrdenesProduciendoBMS();
                SHConexion.leer_OrdenesProduciendoBMS();
                try
                {
                    
                    if (Request.QueryString["IDPDCA"] != null)
                    {
                        //PDCAINT = Convert.ToInt32(Request.QueryString["IDPDCA"]);
                        LBLNUMPDCA.InnerText = Request.QueryString["IDPDCA"];
                        FILTRO = "AND P.[IdPDCA] = " + LBLNUMPDCA.InnerText + "";
                    }

                    if (Request.QueryString["NC"] != null)

                    {
                        FILTRO = "AND P.[IdPDCA] = " + LBLNUMPDCA.InnerText + " AND NAlertaCal = " + Request.QueryString["NC"] + "";
                    }
                    
                    
                    RellenarGrids(null,null);
                }
                catch (Exception ex)
                { }
                CargaDropDowns();
                //TestPurpose.
            }
        }
        public void RellenarGrids(object sender, EventArgs e)
        {
            try
            {
                Conexion_PDCA conexion = new Conexion_PDCA();
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                PDCATable = conexion.Devuelve_datatable_PDCA(FILTRO);
                dgvListadoPDCA.DataSource = PDCATable;
                dgvListadoPDCA.DataBind();
                string CLIENTE = "";
                string PILOTO = "";
                string MAQUINA = "";
                string MOLDE = "";
                string PRODUCTO = "";
                string ELIMINADOS = " AND A.Eliminar = 0";
                string PENDIENTES = " AND [FechaCierreReal] IS NULL";
                string VENCIDAS = "";
                //string ORDERBY = " order by a.[IdReferencial]";
                //string ORDERBY = " order by a.[FechaCierrePrev] ASC";
                string ORDERBY = " order by [SEQNR] ASC, a.[AccionPrioridad] DESC, a.[IdReferencial] ASC";

                LBLNUMPDCA.InnerText = PDCATable.Rows[0]["IdPDCA"].ToString();
                LBLTITULOPDCA.InnerText = PDCATable.Rows[0]["Desviacion"].ToString();
                LBLTITULOPDCAACCIONES.InnerText = PDCATable.Rows[0]["ACCIONES"].ToString() + " acciones cerradas";
                if (SwitchActivas.Checked == false)
                    { PENDIENTES = ""; }
                if (SwitchVencidas.Checked == true)
                    { VENCIDAS = " AND ([FechaCierreReal] IS NULL AND [FechaCierrePrev] < '" + DateTime.Now.ToString() + "')"; }
                if (selecorderby.SelectedIndex != 0)
                    {switch (selecorderby.SelectedIndex.ToString())
                        {
                        case "1":
                            ORDERBY = " order by a.[IdReferencial] ASC";
                            break;
                        case "2":
                            ORDERBY = " order by a.[FechaApertura] ASC";
                            break;
                        case "3":
                            ORDERBY = " order by a.[FechaCierrePrev] ASC";
                            break;
                        case "4":
                            ORDERBY = " order by a.[AccionPrioridad] DESC, a.[IdReferencial] ASC";
                            break;
                        case "5":
                            ORDERBY = " order by [SEQNR] ASC, a.[AccionPrioridad] DESC, a.[IdReferencial] ASC";
                            break;
                    }
                }

                if (InputFiltroCliente.Value.ToString() != "")
                { CLIENTE = " AND PR.CLIENTE LIKE '" + InputFiltroCliente.Value.ToString() + "%'"; }

                if (FiltroOperario.Value.ToString() != "-")
                { PILOTO = " AND OP.Nombre LIKE '" + FiltroOperario.Value.ToString() + "%'"; }

                if (InputFiltroMaquina.Value.ToString() != "")
                { MAQUINA = " AND a.AccionMaquina LIKE " + SHConexion.Devuelve_ID_MAQUINAS(InputFiltroMaquina.Value.ToString()) + "";

                }

                if (InputFiltroMoldes.Value.ToString() != "")
                {
                    string[] RecorteMolde = InputFiltroMoldes.Value.Split(new char[] { '¬' });
                    MOLDE = " AND CAST(PR.Molde as varchar) = '" + RecorteMolde[0] + "'";
                }

                if (InputFiltroProductos.Value.ToString() != "")
                {
                    string[] RecorteProducto = InputFiltroProductos.Value.Split(new char[] { '¬' });
                    PRODUCTO = " AND RTRIM(LTRIM(a.AccionProducto)) = '" + RecorteProducto[0] + "'";
                }
                
                string FILTRO_2 = " WHERE A.IdPDCA = " + LBLNUMPDCA.InnerText + "" + ELIMINADOS + "" + PENDIENTES + ""+CLIENTE+"" + VENCIDAS + "" + PILOTO + "" + MAQUINA + "" + MOLDE + "" + PRODUCTO + "";
                
                gvOrders.DataSource = conexion.Devuelve_datatable_PDCAXListaAcciones(FILTRO_2, ORDERBY);
                gvOrders.DataBind();

                DataTable AuxGVORDERS = gvOrders.DataSource as DataTable;
                int MAXIMOAUX = 0;
                foreach (DataRow dr in AuxGVORDERS.Rows)
                {
                    if (Convert.ToInt32(dr["IdReferencial"]) > MAXIMOAUX)
                    {
                        MAXIMOAUX = Convert.ToInt32(dr["IdReferencial"]);
                    }
                }
                //Label labelIDPDCA = (Label)gvOrders.FooterRow.FindControl("AUXIDPDCA");
                //labelIDPDCA.Text = Convert.ToInt32(AuxGVORDERS.Rows[0]["IdPDCA"]).ToString();

                //Label labelIDREFERENCIAL = (Label)gvOrders.FooterRow.FindControl("AUXIDREFERENCIAL");
                //labelIDREFERENCIAL.Text = (MAXIMOAUX + 1).ToString();

                LinkButton LINKBUT = (LinkButton)gvOrders.FooterRow.FindControl("NuevaAccion");
                LINKBUT.CommandArgument = Convert.ToInt32(AuxGVORDERS.Rows[0]["IdPDCA"]).ToString() + "¬" + AuxGVORDERS.Rows[0]["Desviacion"].ToString();
            }
            catch (Exception ex)
            { }
        }
        private void CargaDropDowns()
        {
            try
            {

                //cabecera de detalles

                Conexion_PDCA conexion = new Conexion_PDCA();
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();

                DataSet Operarios = SHConexion.Devuelve_mandos_intermedios_SMARTH();
                //Operarios.Tables[0].DefaultView.RowFilter = "Departamento = 'PRODUCCION' OR Departamento = '-'";
                DataTable DTPRODUCCION = (Operarios.Tables[0].DefaultView).ToTable();
                foreach (DataRow row in DTPRODUCCION.Rows)
                {
                    PilotoModal.Items.Add(row["Nombre"].ToString());
                    NuevoDropPiloto.Items.Add(row["Nombre"].ToString());
                    FiltroOperario.Items.Add(row["Nombre"].ToString());
                }
                PilotoModal.ClearSelection();
                PilotoModal.SelectedValue = "-";
              
                FiltroOperario.Value = "-";

                NuevoDropPiloto.ClearSelection();
                NuevoDropPiloto.SelectedValue = "-";

                //Datalist de moldes
                ListaParteMoldes = conexion.Devuelve_TOP100_Partes_Moldes();
                {
                    for (int i = 0; i <= ListaParteMoldes.Rows.Count - 1; i++)
                    {
                        DLSelectMolde.InnerHtml = DLSelectMolde.InnerHtml + System.Environment.NewLine +
                            String.Format("<option value='{0}'>", ListaParteMoldes.Rows[i][0]);
                    }
                }

                //Datalist de maquinas
                ListaParteMaquinas = conexion.Devuelve_TOP100_Partes_Maquina();
                {
                    for (int i = 0; i <= ListaParteMaquinas.Rows.Count - 1; i++)
                    {
                        DLSelectMaquina.InnerHtml = DLSelectMaquina.InnerHtml + System.Environment.NewLine +
                            String.Format("<option value='{0}'>", ListaParteMaquinas.Rows[i][0]);
                    }
                }

                //Datalist alertas calidad
                ListaAlertasCalidad = conexion.Devuelve_TOP100_NoConformidades();
                {
                    for (int i = 0; i <= ListaAlertasCalidad.Rows.Count - 1; i++)
                    {
                        DLSelectAlerta.InnerHtml = DLSelectAlerta.InnerHtml + System.Environment.NewLine +
                            String.Format("<option value='{0}'>", ListaAlertasCalidad.Rows[i][0]);
                    }
                }

                ListaProductos = conexion.Devuelve_Lista_Productos_NUEVO_PDCA();
                {
                    for (int i = 0; i <= ListaProductos.Rows.Count - 1; i++)
                    {
                        NuevaReferencia.InnerHtml = NuevaReferencia.InnerHtml + System.Environment.NewLine +
                            String.Format("<option value='{0}'>", ListaProductos.Rows[i][0]);
                    }
                }

                LineaProductosMoldes = SHConexion.Devuelve_listado_PRODUCTOS_SEPARADOR();
                {
                    for (int i = 0; i <= LineaProductosMoldes.Rows.Count - 1; i++)
                    {
                        DLProdMoldes.InnerHtml = DLProdMoldes.InnerHtml + System.Environment.NewLine +
                            String.Format("<option value='{0}'>", LineaProductosMoldes.Rows[i][0]);
                    }
                }

                ListaMaquinas = SHConexion.Devuelve_listado_MAQUINAS();
                    foreach (DataRow row in ListaMaquinas.Rows)
                    {
                        DropMaquina.Items.Add(row["Maquina"].ToString());
                    }
                    for (int i = 0; i <= ListaMaquinas.Rows.Count - 1; i++)
                    {
                        FiltroMaquina.InnerHtml = FiltroMaquina.InnerHtml + System.Environment.NewLine + String.Format("<option value='{0}'>", ListaMaquinas.Rows[i][1]);
                    }

                DataTable DTFiltroCliente = SHConexion.Devuelve_listado_clientes();
                    for (int i = 0; i <= DTFiltroCliente.Rows.Count - 1; i++)
                    {
                        FiltroCliente.InnerHtml = FiltroCliente.InnerHtml + System.Environment.NewLine + String.Format("<option value='{0}'>", DTFiltroCliente.Rows[i][0]);
                    }

                DataTable DTFiltroProducto = SHConexion.Devuelve_listado_PRODUCTOS();
                for (int i = 0; i <= DTFiltroProducto.Rows.Count - 1; i++)
                {
                    FiltroProductos.InnerHtml = FiltroProductos.InnerHtml + System.Environment.NewLine + String.Format("<option value='{0}'>", DTFiltroProducto.Rows[i][0]);
                }

                DataTable DTFiltroMolde = SHConexion.Devuelve_listado_MOLDES();
                for (int i = 0; i <= DTFiltroProducto.Rows.Count - 1; i++)
                {
                    FiltroMoldes.InnerHtml = FiltroMoldes.InnerHtml + System.Environment.NewLine + String.Format("<option value='{0}'>", DTFiltroMolde.Rows[i][0]);
                }
                DropMaquina.ClearSelection();
                DropMaquina.SelectedValue = "-";

            }
            catch (Exception)
            {

            }
        }

        //eliminar    
        public void GuardarAccion(object sender, EventArgs e)
            {
            try
            {
                Conexion_PDCA conexion = new Conexion_PDCA();
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                int APPVinculada = 0;
                int APPIdVinculada = 0;
                string EVIDENCIA1 = LINKevidencia1.Attributes["href"];
                string EVIDENCIA2 = LINKevidencia1.Attributes["href"];
                string EVIDENCIA3 = LINKevidencia1.Attributes["href"];
                string EVIDENCIA4 = LINKevidencia1.Attributes["href"];

                string TipoGuardado = HeaderTipoGuardado.Text;
                switch (TipoGuardado)
                {
                    case "---":
                        break;
                    case "NUEVO":
                        int IDReferencial = conexion.Devuelve_IDReferencial_Asignado(Convert.ToInt32(HeaderIdPDCA.Text));
                        
                        APPVinculada = Convert.ToInt32(AUXNUMDLPARTESELECT.Value);
                        APPIdVinculada = Convert.ToInt32(DLPARTESELECTHIDDEN.Value);
                        string Producto = SelectProdMoldes.Value.Trim();
                        string FechaLimite = "NULL";
                        if (InputFechaLimite.Value != "")
                        {
                            FechaLimite = "'" + InputFechaLimite.Value + "'";
                        }
                        try
                            {
                                string[] RecorteProducto = SelectProdMoldes.Value.Split(new char[] { '¬' });
                                Producto = RecorteProducto[1].Trim();
                            }
                            catch (Exception ex)
                                {
                                }

                         EVIDENCIA1 = LINKevidencia1.Attributes["href"];
                         EVIDENCIA2 = LINKevidencia1.Attributes["href"];
                         EVIDENCIA3 = LINKevidencia1.Attributes["href"];
                         EVIDENCIA4 = LINKevidencia1.Attributes["href"];

                        if (IDReferencial != 0)
                            {
                            conexion.Insertar_A_ListaAcciones(Convert.ToInt32(HeaderIdPDCA.Text), IDReferencial, DateTime.Now.ToString("dd/MM/yyyy"), FechaLimite, InputFechaCierre.Value, Convert.ToInt32(DropTipoaccion.SelectedValue), DescripcionProblema.Value, InputCausaRaiz.Value, InputAccion.Value, SHConexion.Devuelve_ID_Piloto_SMARTH(PilotoModal.SelectedValue.ToString()), 0, 0, APPVinculada, APPIdVinculada, 0, Producto, Convert.ToInt32(DropAccionPrioridad.SelectedValue), Convert.ToInt32(DropAccionEstado.SelectedValue), SHConexion.Devuelve_ID_MAQUINAS(DropMaquina.SelectedValue.ToString()), EVIDENCIA1, EVIDENCIA2, EVIDENCIA3, EVIDENCIA4, Convert.ToInt32(DropDownContencion.SelectedValue), Convert.ToInt32(DropDownISHIKAWA.SelectedValue),Convert.ToInt32(DropLeccionAprendida.SelectedValue));
                            }
                        RellenarGrids(null, null);
                        break;

                    case "EDITAR":
                        string FechaCierre = "";
                        FechaLimite = "";
                        APPVinculada = Convert.ToInt32(AUXNUMDLPARTESELECT.Value);
                        APPIdVinculada = Convert.ToInt32(DLPARTESELECTHIDDEN.Value);
                        if (InputFechaCierre.Value != "")
                        {
                            FechaCierre = ", FechaCierreReal = '" + InputFechaCierre.Value + "'";
                        }
                        else
                        {
                            FechaCierre = ", FechaCierreReal = NULL";
                        }
                        if (InputFechaLimite.Value != "")
                        {
                            FechaLimite = ", [FechaCierrePrev] = '" + InputFechaLimite.Value + "'";
                        }
                        else
                        {
                            FechaLimite = ", [FechaCierrePrev] = NULL";
                        }
                        //pasar a conector
                        Producto = SelectProdMoldes.Value;
                            try
                            {
                                string[] RecorteProducto = SelectProdMoldes.Value.Split(new char[] { '¬' });
                                Producto = RecorteProducto[1];
                            }
                            catch (Exception ex)
                            {
                            }
                         EVIDENCIA1 = LINKevidencia1.Attributes["href"];
                         EVIDENCIA2 = LINKevidencia2.Attributes["href"];
                         EVIDENCIA3 = LINKevidencia3.Attributes["href"];
                         EVIDENCIA4 = LINKevidencia4.Attributes["href"];
                            string url1 = "@" + EVIDENCIA1;
                            string ext1 = System.IO.Path.GetExtension(url1);
                            if (ext1 == ".jpg" || ext1 == ".png" || ext1 == ".jpeg")
                            { IMGevidencia1.Attributes.Add("src", EVIDENCIA1); }
                            else
                            { IMGevidencia1.Attributes.Add("src", "http://facts4-srv/thermogestion/SMARTH_fonts/INTERNOS/ICONODOCGENERICO.png"); }
                          

                            string url2 = "@" + EVIDENCIA2;
                            string ext2 = System.IO.Path.GetExtension(url2);
                            if (ext2 == ".jpg" || ext2 == ".png" || ext2 == ".jpeg")
                            { IMGevidencia2.Attributes.Add("src", EVIDENCIA2); }
                            else
                            { IMGevidencia2.Attributes.Add("src", "http://facts4-srv/thermogestion/SMARTH_fonts/INTERNOS/ICONODOCGENERICO.png"); }
                          

                            string url3 = "@" + EVIDENCIA3;
                            string ext3 = System.IO.Path.GetExtension(url3);
                            if (ext3 == ".jpg" || ext3 == ".png" || ext3 == ".jpeg")
                            { IMGevidencia3.Attributes.Add("src", EVIDENCIA3); }
                            else
                            { IMGevidencia3.Attributes.Add("src", "http://facts4-srv/thermogestion/SMARTH_fonts/INTERNOS/ICONODOCGENERICO.png"); }
                          

                            string url4 = "@" + EVIDENCIA4;
                            string ext4 = System.IO.Path.GetExtension(url4);
                            if (ext4 == ".jpg" || ext4 == ".png" || ext4 == ".jpeg")
                            { IMGevidencia4.Attributes.Add("src", EVIDENCIA4); }
                            else
                            { IMGevidencia4.Attributes.Add("src", "http://facts4-srv/thermogestion/SMARTH_fonts/INTERNOS/ICONODOCGENERICO.png"); }
                           

                        conexion.Actualizar_A_ListaAcciones(Convert.ToInt32(HeaderIdPDCAXListaacciones.Text), FechaLimite, Convert.ToInt32(DropTipoaccion.SelectedValue), DescripcionProblema.Value, InputCausaRaiz.Value, InputAccion.Value, SHConexion.Devuelve_ID_Piloto_SMARTH(PilotoModal.SelectedValue.ToString()), APPVinculada, APPIdVinculada, FechaCierre, Producto, Convert.ToInt32(DropAccionPrioridad.SelectedValue), Convert.ToInt32(DropAccionEstado.SelectedValue), SHConexion.Devuelve_ID_MAQUINAS(DropMaquina.SelectedValue.ToString()), EVIDENCIA1, EVIDENCIA2, EVIDENCIA3, EVIDENCIA4, Convert.ToInt32(DropDownContencion.SelectedValue), Convert.ToInt32(DropDownISHIKAWA.SelectedValue), Convert.ToInt32(DropLeccionAprendida.SelectedValue));
                        if (InputRevision.Value != "")
                            { 
                            InsertarRevision();
                            }
                        RellenarGrids(null,null);
                        
                        break;
                }
                

            }
            catch (Exception ex)
            { }
            }

        private void InsertarRevision()
        {
            Conexion_PDCA conexion = new Conexion_PDCA();
            try 
            { 
                conexion.Insertar_A_AccionRevision(Convert.ToInt32(HeaderIdPDCAXListaacciones.Text), InputRevision.Value);
            }
            catch (Exception ex)
            { }
            
        }

        protected void EliminarRevision(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Conexion_PDCA conexion = new Conexion_PDCA();
                string ID = e.CommandArgument.ToString();
                conexion.Marcar_Borrado_Revision(ID);
                string headertipoguardado = HeaderTipoGuardado.Text;
                string headeridpdcaxlistaacciones = HeaderIdPDCAXListaacciones.Text;
                string staticbackdropLabel = staticBackdropLabel.InnerText;
                RecargarAccion(headertipoguardado, headeridpdcaxlistaacciones, staticbackdropLabel);
    

            }
            catch (Exception ex)
            { }
        }
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                try
                {
                    Conexion_PDCA conexion = new Conexion_PDCA();
                    Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                  
                    if (e.Row.RowState.HasFlag(DataControlRowState.Edit))
                    {
                        DropDownList DropTipo = (DropDownList)e.Row.FindControl("dropTipo");
                        DropDownList DropPrioridad = (DropDownList)e.Row.FindControl("dropPrioridad");
                        DropDownList DropPiloto = (DropDownList)e.Row.FindControl("dropPiloto");
                        DropDownList DropEstado = (DropDownList)e.Row.FindControl("dropEstado");

                        DataSet Operarios = SHConexion.Devuelve_mandos_intermedios_SMARTH();
                        foreach (DataRow row in Operarios.Tables[0].Rows) { DropPiloto.Items.Add(row["Nombre"].ToString()); }
                   
                        DataRowView dr = e.Row.DataItem as DataRowView;
                        DropPiloto.SelectedValue = dr["Nombre"].ToString();
                        DropTipo.SelectedValue = dr["TipoNUM"].ToString();
                        DropPrioridad.SelectedValue = dr["PRIORIDADNUM"].ToString();
                        DropEstado.SelectedValue = dr["ESTADONUM"].ToString();

                        DropTipo.DataBind();
                        DropPrioridad.DataBind();
                        DropEstado.DataBind();
                        DropPiloto.DataBind();

                    }
                   
                        Label AccVencidas = (Label)e.Row.FindControl("lblVencidas");
                        
                       
                        if (AccVencidas.Text == "1")
                        {
                            AccVencidas.Text = "¡Plazos vencidos!<br />";
                            AccVencidas.Visible = true;
                            //e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffff89")
                        }
                    
                    DataRowView dr2 = e.Row.DataItem as DataRowView;
                    LinkButton BTNEDITAR = (LinkButton)e.Row.FindControl("btnEdit");
                    if (dr2["IdPDCA"].ToString() == "1" || dr2["IdPDCA"].ToString() == "2")
                        {
                        BTNEDITAR.Visible = false;
                        }
                }
                catch (Exception ex)
                { }
            }
        }

        protected void OnRowDataBoundAUX(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label FECHAREV = (Label)e.Row.FindControl("lblRevision");
                    if (FECHAREV.Text != "")
                    {
                        string auxfecharev = FECHAREV.Text;
                        FECHAREV.Text = "<strong >Rev: </strong >" + auxfecharev;
                    }

                    Label APPVinculada = (Label)e.Row.FindControl("lblIdAPP");
                    if (APPVinculada.Text != "0")
                    {
                        LinkButton LINKAPP = (LinkButton)e.Row.FindControl("VERAPP");
                        LINKAPP.Visible = true;
                    }

                    Label AccionVencida = (Label)e.Row.FindControl("lblAccionVencida");
                    Label FechaCierre = (Label)e.Row.FindControl("lblCierreReal");
                    if (AccionVencida.Text == "1" && FechaCierre.Text == "")
                    {
                        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffff89");
                    }

                    DataRowView dr2 = e.Row.DataItem as DataRowView;

                    Image IMGAccionEstado = (Image)e.Row.FindControl("IMGAccionEstado");
                    switch (dr2["AccionEstado"].ToString())
                    {
                        case "0":
                            IMGAccionEstado.ImageUrl = "~/SMARTH_fonts/INTERNOS/Circulo0b.png";
                            break;
                        case "1":
                            IMGAccionEstado.ImageUrl = "~/SMARTH_fonts/INTERNOS/Ciculo25b.png";
                            break;
                        case "2":
                            IMGAccionEstado.ImageUrl = "~/SMARTH_fonts/INTERNOS/Ciculo50b.png";
                            break;
                        case "3":
                            IMGAccionEstado.ImageUrl = "~/SMARTH_fonts/INTERNOS/Ciculo75b.png";
                            break;
                        case "4":
                            IMGAccionEstado.ImageUrl = "~/SMARTH_fonts/INTERNOS/Ciculo100b.png";
                            break;
                    }

                    Image IMGAccionPrioridad = (Image)e.Row.FindControl("IMGAccionPrioridad");
                    switch (dr2["AccionPrioridad"].ToString())
                    {
                        case "0":
                            IMGAccionPrioridad.ImageUrl = "~/SMARTH_fonts/INTERNOS/PRIORIDAD1.png";
                            break;
                        case "1":
                            IMGAccionPrioridad.ImageUrl = "~/SMARTH_fonts/INTERNOS/PRIORIDAD2.png";
                            break;
                        case "2":
                            IMGAccionPrioridad.ImageUrl = "~/SMARTH_fonts/INTERNOS/PRIORIDAD3.png";
                            break;
                        case "3":
                            IMGAccionPrioridad.ImageUrl = "~/SMARTH_fonts/INTERNOS/PRIORIDAD4.png";
                            break;
                    }

                    HtmlButton EstadoRunning = (HtmlButton)e.Row.FindControl("EstadoRunning");
                    HtmlButton EstadoEncola = (HtmlButton)e.Row.FindControl("EstadoEncola");
                    if (dr2["SEQNR"].ToString() == "100")
                    { }
                    else if (dr2["SEQNR"].ToString() == "0")
                    {
                        EstadoRunning.Style.Add("visibility", "visible");
                    }
                    else
                    {
                        EstadoEncola.Style.Add("visibility", "visible");                        
                    }


                    switch (dr2["AccionPrioridad"].ToString())
                    {
                        case "0":
                            IMGAccionPrioridad.ImageUrl = "~/SMARTH_fonts/INTERNOS/PRIORIDAD1.png";
                            break;
                        case "1":
                            IMGAccionPrioridad.ImageUrl = "~/SMARTH_fonts/INTERNOS/PRIORIDAD2.png";
                            break;
                        case "2":
                            IMGAccionPrioridad.ImageUrl = "~/SMARTH_fonts/INTERNOS/PRIORIDAD3.png";
                            break;
                        case "3":
                            IMGAccionPrioridad.ImageUrl = "~/SMARTH_fonts/INTERNOS/PRIORIDAD4.png";
                            break;
                    }


                    //LinkButton BTNBORRAR = (LinkButton)e.Row.FindControl("BORRARACCION");
                    //LinkButton BTNEDITAR = (LinkButton)e.Row.FindControl("EDITAACCION");
                    //Label NUMERO = (Label)e.Row.FindControl("lblNºRef");
                    //Label PILOTO = (Label)e.Row.FindControl("lblAPPPiloto");

                    //INCLUIR resto de linea para no mostrar
                    if (dr2["IdReferencial"].ToString() == "0")
                    {
                        e.Row.Style.Add("visibility", "hidden");
                        e.Row.Style.Add("display", "none");

                    }

                  
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void RedireccionaAPP(object sender, EventArgs e)
        {
            try
            {
                string link = "";
                HtmlButton button = (HtmlButton)sender;
                string APP = button.ID;
               
                switch (APP)
                {
                    case "PDCACreaParteMaquina":                 
                        link = "../MANTENIMIENTO/ReparacionMaquinas.aspx";
                        break;
                    case "PDCACreaParteMolde":
                        link = "http://facts4-srv/thermogestion/MANTENIMIENTO/ReparacionMoldes.aspx";
                        break;
                    case "PDCACreaNoConformidad":   
                        link = Page.ResolveUrl("~/CALIDAD/Alertas_Calidad.aspx");
                        break;

                }
                GuardarAccion(null, null);
                Response.Redirect(link);
            }
            catch (Exception ex)
            {
            }
        }

        public void AbrirScriptDocumentacion(Object sender, EventArgs e)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "VerificarLectura();", true);
               
            }

        public void GridView_RowEditing_PDCA(object sender, GridViewEditEventArgs e)
        {
            try
            {
                dgvListadoPDCA.EditIndex = e.NewEditIndex;
                dgvListadoPDCA.DataSource = PDCATable;
                dgvListadoPDCA.DataBind();
            }
            catch (Exception ex)
            { 
            }


        }
      
        protected void gridView_RowCancelingEdit_PDCA(object sender, GridViewCancelEditEventArgs e)
        {
            dgvListadoPDCA.EditIndex = -1;         
            dgvListadoPDCA.DataSource = PDCATable;
            dgvListadoPDCA.DataBind();
          
        }

        public void GridViewCommandEventHandler(Object sender, GridViewCommandEventArgs e)
        {
            try 
            {
                Conexion_PDCA conexion = new Conexion_PDCA();
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                //Comandos de Plan de acciones

                //Comandos de acciones
                if (e.CommandName == "NuevaAccion")
                {
                    HeaderTipoGuardado.Text = "NUEVO";
                    SelectProdMoldes.Value = "";
                    string[] IDSCONTROL = e.CommandArgument.ToString().Split(new char[] { '¬' });
                    HeaderIdPDCA.Text = IDSCONTROL[0].ToString();
                    staticBackdropLabel.InnerText = "NUEVA ACCIÓN PARA: " + IDSCONTROL[1].ToString(); ;
                    DropMaquina.SelectedIndex = 0;
                    DropTipoaccion.SelectedIndex = 0;
                    DropAccionEstado.SelectedValue = "0";
                    DropAccionPrioridad.SelectedValue = "0";
                    DescripcionProblema.Value = "";
                    InputFechaApertura.Value = DateTime.Now.ToString("dd/MM/yyyy");
                    InputFechaLimite.Value = "";
                    InputFechaLimiteOriginal.Value = "";
                    InputFechaCierre.Value = "";
                    InputCausaRaiz.Value = "";
                    InputAccion.Value = "";
                    DATOSAPP.Style.Value = "display:none";
                    DLPARTESELECT.Text = "";
                    DLPARTESELECT2.Text = "";
                    DLPARTESELECT3.InnerText = "";
                    ICONOAPP.Src = "http://facts4-srv/thermogestion/imagenes/NULL.png";
                    linkICONOAPP.HRef = "#";
                    APPLABEL.Text = "";
                    PilotoModal.SelectedValue = "-";
                    lblUltimaRev.Text = "";
                    InputRevision.Value = "";
                    DropDownISHIKAWA.SelectedValue = "0";
                    DropDownContencion.SelectedValue = "0";
                    DropLeccionAprendida.SelectedValue = "0";
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "ShowPopup();", true);
                }
                if (e.CommandName == "EditAccion")
                {
                    HeaderTipoGuardado.Text = "EDITAR";
                    string [] idaccion = e.CommandArgument.ToString().Split(new char[] {'¬'});
                    string id = idaccion[0];
                    string desviacion = idaccion[1];
                    staticBackdropLabel.InnerText = "PLAN DE ACCIÓN: " + desviacion;
                    DropAccionEstado.SelectedValue = "0";
                    DropAccionPrioridad.SelectedValue = "0";
                    DropTipoaccion.SelectedIndex = 0;
                    DropMaquina.SelectedIndex = 0;
                    DescripcionProblema.Value = "";
                    InputFechaLimite.Value = "";
                    InputFechaLimiteOriginal.Value = "";
                    InputFechaCierre.Value = "";
                    InputCausaRaiz.Value = "";
                    InputAccion.Value = "";

                    lblUltimaRev.Text = "";
                    InputRevision.Value = "";
                    DropDownContencion.SelectedValue = "0";
                    DropDownISHIKAWA.SelectedValue = "0";
                    DropLeccionAprendida.SelectedValue = "0";

                    DataList.Value = "";
                    DataList2.Value = "";
                    DataList3.Value = "";
                    DATOSAPP.Style.Value = "display:none";
                    DLPARTESELECT.Text = "";
                    DLPARTESELECT2.Text = "";
                    DLPARTESELECT3.InnerText = "";
                    ICONOAPP.Src = "http://facts4-srv/thermogestion/imagenes/NULL.png";
                    linkICONOAPP.HRef = "#";
                    APPLABEL.Text = "";

                    PilotoModal.SelectedValue = "-";

                    DataTable DetallesACCION = conexion.Devuelve_datatable_DetallesAcciones(id);
                    string AUXSelectProdMoldes = SHConexion.Devuelve_linea_PRODUCTOS_SEPARADOR(DetallesACCION.Rows[0]["ProdMOLDE"].ToString());
                    if (AUXSelectProdMoldes == "")
                    { SelectProdMoldes.Value = DetallesACCION.Rows[0]["ProdMOLDE"].ToString(); }
                    else
                    { SelectProdMoldes.Value = AUXSelectProdMoldes; }
                    string AUXDropMaquina = SHConexion.Devuelve_MAQUINAS_SELECCIONADA(DetallesACCION.Rows[0]["AccionMaquina"].ToString());
                    if (AUXDropMaquina == "")
                    { DropMaquina.SelectedIndex = 0; }
                    else
                    {
                        DropMaquina.SelectedValue = SHConexion.Devuelve_MAQUINAS_SELECCIONADA(DetallesACCION.Rows[0]["AccionMaquina"].ToString());
                    }

                    HeaderIdPDCAXListaacciones.Text = DetallesACCION.Rows[0]["Id"].ToString();
                    DropTipoaccion.SelectedValue = DetallesACCION.Rows[0]["TipoINT"].ToString();
                    DescripcionProblema.Value = DetallesACCION.Rows[0]["DesviacionEncontrada"].ToString();
                    InputCausaRaiz.Value = DetallesACCION.Rows[0]["CausaRaiz"].ToString();
                    InputAccion.Value = DetallesACCION.Rows[0]["Accion"].ToString();
                    InputFechaApertura.Value = Convert.ToDateTime(DetallesACCION.Rows[0]["FechaApertura"]).ToString("dd/MM/yyyy");
                    
                    DropAccionEstado.SelectedValue = DetallesACCION.Rows[0]["AccionEstado"].ToString();
                    DropAccionPrioridad.SelectedValue = DetallesACCION.Rows[0]["AccionPrioridad"].ToString();
                    DropDownContencion.SelectedValue = DetallesACCION.Rows[0]["EstadoContencion"].ToString();
                    DropDownISHIKAWA.SelectedValue = DetallesACCION.Rows[0]["OrigenCausa"].ToString();
                    DropLeccionAprendida.SelectedValue = DetallesACCION.Rows[0]["LeccionAprendida"].ToString();

                    if (DetallesACCION.Rows[0]["FechaCierrePrev"].ToString() != "")
                    {
                        InputFechaLimite.Value = Convert.ToDateTime(DetallesACCION.Rows[0]["FechaCierrePrev"]).ToString("dd/MM/yyyy");
                        if (DetallesACCION.Rows[0]["FechaCierrePrevback"].ToString() != "")
                        {
                            InputFechaLimiteOriginal.Value = Convert.ToDateTime(DetallesACCION.Rows[0]["FechaCierrePrevback"]).ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            InputFechaLimiteOriginal.Value = Convert.ToDateTime(DetallesACCION.Rows[0]["FechaCierrePrev"]).ToString("dd/MM/yyyy");
                        }
                    }
                    if (DetallesACCION.Rows[0]["FechaCierreReal"].ToString() != "")
                    {
                        InputFechaCierre.Value = Convert.ToDateTime(DetallesACCION.Rows[0]["FechaCierreReal"]).ToString("dd/MM/yyyy");
                    }
                    PilotoModal.SelectedValue = DetallesACCION.Rows[0]["APPPiloto"].ToString();
                    ICONOAPP.Src = "http://facts4-srv/thermogestion/imagenes/"+ DetallesACCION.Rows[0]["APPVinculadaIMG"].ToString() + "" ;

                    switch (DetallesACCION.Rows[0]["APPVinculada"].ToString())
                    {
                        case "0": //Vacío
                                  //APPLABEL.Text = "ALERTA DE CALIDAD VINCULADA";
                            break;
                        case "1": //ALERTA CALIDAD
                            DATOSAPP.Style.Clear();
                            ICONOAPP.Src = "ICONOS/ICONOCALIDAD.png";
                            APPLABEL.Text = "ALERTA DE CALIDAD VINCULADA";
                            AUXNUMDLPARTESELECT.Value = DetallesACCION.Rows[0]["APPVinculada"].ToString();
                            DLPARTESELECTHIDDEN.Value = DetallesACCION.Rows[0]["APPIdVinculada"].ToString();
                            APPAccionSeleccionada = conexion.Devuelve_NoConformidad_Seleccionado(DetallesACCION.Rows[0]["APPIdVinculada"].ToString());
                            DLPARTESELECT.Text = APPAccionSeleccionada.Rows[0]["ID"].ToString();
                            DLPARTESELECT2.Text = APPAccionSeleccionada.Rows[0]["PRODUCTO"].ToString();
                            DLPARTESELECT3.InnerText = APPAccionSeleccionada.Rows[0]["MOTIVO"].ToString();
                            linkICONOAPP.HRef = Page.ResolveUrl("~/CALIDAD/Alertas_Calidad.aspx?NOCONFORMIDAD=" + APPAccionSeleccionada.Rows[0]["ID"].ToString() + "");
                            break;
                        case "2": //PARTE DE MÁQUINA VINCULADO
                            DATOSAPP.Style.Clear();
                            ICONOAPP.Src = "ICONOS/ICONOMAQUINAS.png";
                            APPLABEL.Text = "PARTE DE MÁQUINA VINCULADO";
                            AUXNUMDLPARTESELECT.Value = DetallesACCION.Rows[0]["APPVinculada"].ToString();
                            DLPARTESELECTHIDDEN.Value = DetallesACCION.Rows[0]["APPIdVinculada"].ToString();
                            APPAccionSeleccionada = conexion.Devuelve_Parte_Maquina_Seleccionado(DetallesACCION.Rows[0]["APPIdVinculada"].ToString());
                            DLPARTESELECT.Text = APPAccionSeleccionada.Rows[0]["ID"].ToString();
                            DLPARTESELECT2.Text = APPAccionSeleccionada.Rows[0]["PRODUCTO"].ToString();
                            DLPARTESELECT3.InnerText = APPAccionSeleccionada.Rows[0]["MOTIVO"].ToString();
                            linkICONOAPP.HRef = "../MANTENIMIENTO/ReparacionMaquinas.aspx?PARTE=" + APPAccionSeleccionada.Rows[0]["ID"].ToString();

                            break;
                        case "3": //PARTE DE MOLDE
                            DATOSAPP.Style.Clear();
                            ICONOAPP.Src = "ICONOS/ICONOMOLDES.png";
                            APPLABEL.Text = "PARTE DE MOLDE VINCULADO";
                            AUXNUMDLPARTESELECT.Value = DetallesACCION.Rows[0]["APPVinculada"].ToString();
                            DLPARTESELECTHIDDEN.Value = DetallesACCION.Rows[0]["APPIdVinculada"].ToString();
                            APPAccionSeleccionada = conexion.Devuelve_Parte_Molde_Seleccionado(DetallesACCION.Rows[0]["APPIdVinculada"].ToString());
                            DLPARTESELECT.Text = APPAccionSeleccionada.Rows[0]["ID"].ToString();
                            DLPARTESELECT2.Text = APPAccionSeleccionada.Rows[0]["PRODUCTO"].ToString();
                            DLPARTESELECT3.InnerText = APPAccionSeleccionada.Rows[0]["MOTIVO"].ToString();
                            linkICONOAPP.HRef = "http://facts4-srv/thermogestion/MANTENIMIENTO/ReparacionMoldes.aspx?PARTE=" + APPAccionSeleccionada.Rows[0]["ID"].ToString();
                            break;
                        case "4": //PARÁMETROS
                            DATOSAPP.Style.Clear();
                            ICONOAPP.Src = "ICONOS/ICONOPARAMETROS.png";
                            APPLABEL.Text = "FICHA DE PARÁMETROS";
                            break;

                    }

                    //esto no va aquí
                    switch (DetallesACCION.Rows[0]["APPVinculada"].ToString())
                    {
                        case "0": //Vacío
                            //APPLABEL.Text = "ALERTA DE CALIDAD VINCULADA";
                            break;
                        case "1": //ALERTA CALIDAD
                            DATOSAPP.Style.Clear();
                            ICONOAPP.Src = "ICONOS/ICONOCALIDAD.png";
                            APPLABEL.Text = "ALERTA DE CALIDAD VINCULADA";
                            APPAccionSeleccionada = conexion.Devuelve_NoConformidad_Seleccionado(DetallesACCION.Rows[0]["APPIdVinculada"].ToString());
                                DLPARTESELECT.Text = APPAccionSeleccionada.Rows[0]["ID"].ToString();
                                DLPARTESELECT2.Text = APPAccionSeleccionada.Rows[0]["PRODUCTO"].ToString();
                                DLPARTESELECT3.InnerText = APPAccionSeleccionada.Rows[0]["MOTIVO"].ToString();
                                linkICONOAPP.HRef = Page.ResolveUrl("~/CALIDAD/Alertas_Calidad.aspx?NOCONFORMIDAD="+ APPAccionSeleccionada.Rows[0]["ID"].ToString() +"");
                            break;
                        case "2": //PARTE DE MÁQUINA VINCULADO
                            DATOSAPP.Style.Clear();
                            ICONOAPP.Src = "ICONOS/ICONOMAQUINAS.png";
                            APPLABEL.Text = "PARTE DE MÁQUINA VINCULADO";
                            APPAccionSeleccionada = conexion.Devuelve_Parte_Maquina_Seleccionado(DetallesACCION.Rows[0]["APPIdVinculada"].ToString());
                                DLPARTESELECT.Text = APPAccionSeleccionada.Rows[0]["ID"].ToString();
                                DLPARTESELECT2.Text = APPAccionSeleccionada.Rows[0]["PRODUCTO"].ToString();
                                DLPARTESELECT3.InnerText = APPAccionSeleccionada.Rows[0]["MOTIVO"].ToString();
                                linkICONOAPP.HRef = "../MANTENIMIENTO/ReparacionMaquinas.aspx?PARTE=" + APPAccionSeleccionada.Rows[0]["ID"].ToString();

                            break;
                        case "3": //PARTE DE MOLDE
                            DATOSAPP.Style.Clear();
                            ICONOAPP.Src = "ICONOS/ICONOMOLDES.png";
                            APPLABEL.Text = "PARTE DE MOLDE VINCULADO";
                            APPAccionSeleccionada = conexion.Devuelve_Parte_Molde_Seleccionado(DetallesACCION.Rows[0]["APPIdVinculada"].ToString());
                                DLPARTESELECT.Text = APPAccionSeleccionada.Rows[0]["ID"].ToString();
                                DLPARTESELECT2.Text = APPAccionSeleccionada.Rows[0]["PRODUCTO"].ToString();
                                DLPARTESELECT3.InnerText = APPAccionSeleccionada.Rows[0]["MOTIVO"].ToString();
                                linkICONOAPP.HRef = "http://facts4-srv/thermogestion/MANTENIMIENTO/ReparacionMoldes.aspx?PARTE=" + APPAccionSeleccionada.Rows[0]["ID"].ToString();
                            break;
                        case "4": //PARÁMETROS
                            DATOSAPP.Style.Clear();
                            ICONOAPP.Src = "ICONOS/ICONOPARAMETROS.png";
                            APPLABEL.Text = "FICHA DE PARÁMETROS";
                            break;

                    }
                    string EVIDENCIA1 = DetallesACCION.Rows[0]["AdjuntoEVIDENCIA1"].ToString();
                    string EVIDENCIA2 = DetallesACCION.Rows[0]["AdjuntoEVIDENCIA2"].ToString();
                    string EVIDENCIA3 = DetallesACCION.Rows[0]["AdjuntoEVIDENCIA3"].ToString();
                    string EVIDENCIA4 = DetallesACCION.Rows[0]["AdjuntoEVIDENCIA4"].ToString();

                    string url1 = "@"+EVIDENCIA1;
                    string ext1 = System.IO.Path.GetExtension(url1);
                    if (ext1 == ".jpg" || ext1 == ".png" || ext1 == ".jpeg")
                        { IMGevidencia1.Attributes.Add("src", EVIDENCIA1); }
                    else
                        { IMGevidencia1.Attributes.Add("src", "http://facts4-srv/thermogestion/SMARTH_fonts/INTERNOS/ICONODOCGENERICO.png"); }
                        LINKevidencia1.Attributes.Add("href", EVIDENCIA1);

                    string url2 = "@" + EVIDENCIA2;
                    string ext2 = System.IO.Path.GetExtension(url2);
                    if (ext2 == ".jpg" || ext2 == ".png" || ext2 == ".jpeg")
                        { IMGevidencia2.Attributes.Add("src", EVIDENCIA2); }
                    else
                        { IMGevidencia2.Attributes.Add("src", "http://facts4-srv/thermogestion/SMARTH_fonts/INTERNOS/ICONODOCGENERICO.png"); }
                        LINKevidencia2.Attributes.Add("href", EVIDENCIA2);

                    string url3 = "@" + EVIDENCIA3;
                    string ext3 = System.IO.Path.GetExtension(url3);
                    if (ext3 == ".jpg" || ext3 == ".png" || ext3 == ".jpeg")
                        { IMGevidencia3.Attributes.Add("src", EVIDENCIA3); }
                    else
                        { IMGevidencia3.Attributes.Add("src", "http://facts4-srv/thermogestion/SMARTH_fonts/INTERNOS/ICONODOCGENERICO.png"); }
                        LINKevidencia3.Attributes.Add("href", EVIDENCIA3);

                    string url4 = "@" + EVIDENCIA4;
                    string ext4 = System.IO.Path.GetExtension(url4);
                    if (ext4 == ".jpg" || ext4 == ".png" || ext4 == ".jpeg")
                        { IMGevidencia4.Attributes.Add("src", EVIDENCIA4); }
                    else
                        { IMGevidencia4.Attributes.Add("src", "http://facts4-srv/thermogestion/SMARTH_fonts/INTERNOS/ICONODOCGENERICO.png"); }
                        LINKevidencia4.Attributes.Add("href", EVIDENCIA4);

                    DataTable RevisionAcciones = conexion.Devuelve_datatable_RevisionAcciones(HeaderIdPDCAXListaacciones.Text);
                    try
                    {
                        if (RevisionAcciones.Rows.Count > 0)
                        {
                            lblUltimaRev.Text = Convert.ToDateTime(RevisionAcciones.Rows[0]["FechaApertura"]).ToString("dd/MM/yyyy") + " - " + RevisionAcciones.Rows[0]["Revision"].ToString();
                            DgvListaRevs.DataSource = RevisionAcciones;
                            DgvListaRevs.DataBind();
                        }                           
                        else
                        {
                            DgvListaRevs.DataSource = null;
                            DgvListaRevs.DataBind();
                        }
                       
                    }
                    catch (Exception ex)
                    {
                        DgvListaRevs.DataSource = null;
                        DgvListaRevs.DataBind();
                    }

                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "ShowPopup();", true);

                }  
                if (e.CommandName == "BorrarAccion")
                {
                    string ID = e.CommandArgument.ToString();
                    conexion.Marcar_Accion_Borrado_PDCA(ID);
                    RellenarGrids(null,null);
                }
                if (e.CommandName == "IrApp")
                {
                    string[] idAPP = e.CommandArgument.ToString().Split(new char[] { '¬' });
                    string APP = idAPP[0].ToString();
                    string IDParte = idAPP[1].ToString();

                    switch (APP)
                    {
                        case "1": //ALERTA CALIDAD
                            Response.Redirect(url: "http://facts4-srv/thermogestion/CALIDAD/Alertas_Calidad.aspx?NOCONFORMIDAD=" + IDParte + "", true);
                            break;
                        case "2": //PARTE MAQUINA
                            Response.Redirect(url: "../MANTENIMIENTO/ReparacionMaquinas.aspx?PARTE=" + IDParte + "", true);
                            break;
                        case "3": //PARTE MOLDE
                            Response.Redirect(url: "http://facts4-srv/thermogestion/MANTENIMIENTO/ReparacionMoldes.aspx?PARTE=" + IDParte + "", true);
                            break;
                        case "4": //LIBERACIÓN DE SERIE
                            Response.Redirect(url: "http://facts4-srv/thermogestion/LIBERACIONES/LiberacionSerie.aspx?ORDEN=" + IDParte + "", true);
                            break;
                        case "5": //FICHA DE PARAMETROS
                            Response.Redirect(url: "http://facts4-srv/thermogestion/ListaFichasParametros.aspx", true);
                            break;
                        case "6": //HISTORICO REVISIÓN
                            Response.Redirect(url: "http://facts4-srv/thermogestion/GP12/GP12HistoricoReferencia.aspx?REFERENCIA=" + IDParte + "",true);
                            break;
                    }

                    }
            }
            catch (Exception ex)
            {
                Mail_LOGERROR(ex.Message);
            }
        }

        public void RecargarAccion(string headertipo, string headeridpdcaXlista, string staticbackdrop)
        {
            Conexion_PDCA conexion = new Conexion_PDCA();
            Conexion_SMARTH SHConexion = new Conexion_SMARTH();
            HeaderTipoGuardado.Text = headertipo;
            staticBackdropLabel.InnerText = staticbackdrop;
            //string[] idaccion = e.CommandArgument.ToString().Split(new char[] { '¬' });
            //string id = idaccion[0];
            //string desviacion = idaccion[1];
            //staticBackdropLabel.InnerText = "PLAN DE ACCIÓN: " + desviacion;
            DropAccionEstado.SelectedValue = "0";
            DropAccionPrioridad.SelectedValue = "0";
            DropTipoaccion.SelectedIndex = 0;
            DropMaquina.SelectedIndex = 0;
            DescripcionProblema.Value = "";
            InputFechaLimite.Value = "";
            InputFechaLimiteOriginal.Value = "";
            InputFechaCierre.Value = "";
            InputCausaRaiz.Value = "";
            InputAccion.Value = "";
            lblUltimaRev.Text = "";
            DATOSAPP.Style.Value = "display:none";
            DLPARTESELECT.Text = "";
            DLPARTESELECT2.Text = "";
            DLPARTESELECT3.InnerText = "";
            ICONOAPP.Src = "http://facts4-srv/thermogestion/imagenes/NULL.png";
            linkICONOAPP.HRef = "#";
            APPLABEL.Text = "";
            InputRevision.Value = "";
            DropDownContencion.SelectedValue = "0";
            DropDownISHIKAWA.SelectedValue = "0";
            DropLeccionAprendida.SelectedValue = "0";

            DataList.Value = "";
            DataList2.Value = "";
            DataList3.Value = "";
            PilotoModal.SelectedValue = "-";

            DataTable DetallesACCION = conexion.Devuelve_datatable_DetallesAcciones(headeridpdcaXlista);
            string AUXSelectProdMoldes = SHConexion.Devuelve_linea_PRODUCTOS_SEPARADOR(DetallesACCION.Rows[0]["ProdMOLDE"].ToString());
            if (AUXSelectProdMoldes == "")
            { SelectProdMoldes.Value = DetallesACCION.Rows[0]["ProdMOLDE"].ToString(); }
            else
            { SelectProdMoldes.Value = AUXSelectProdMoldes; }
            string AUXDropMaquina = SHConexion.Devuelve_MAQUINAS_SELECCIONADA(DetallesACCION.Rows[0]["AccionMaquina"].ToString());
            if (AUXDropMaquina == "")
            { DropMaquina.SelectedIndex = 0; }
            else
            {
                DropMaquina.SelectedValue = SHConexion.Devuelve_MAQUINAS_SELECCIONADA(DetallesACCION.Rows[0]["AccionMaquina"].ToString());
            }

            HeaderIdPDCAXListaacciones.Text = DetallesACCION.Rows[0]["Id"].ToString();
            DropTipoaccion.SelectedValue = DetallesACCION.Rows[0]["TipoINT"].ToString();
            DescripcionProblema.Value = DetallesACCION.Rows[0]["DesviacionEncontrada"].ToString();
            InputCausaRaiz.Value = DetallesACCION.Rows[0]["CausaRaiz"].ToString();
            InputAccion.Value = DetallesACCION.Rows[0]["Accion"].ToString();
            InputFechaApertura.Value = Convert.ToDateTime(DetallesACCION.Rows[0]["FechaApertura"]).ToString("dd/MM/yyyy");

            DropAccionEstado.SelectedValue = DetallesACCION.Rows[0]["AccionEstado"].ToString();
            DropAccionPrioridad.SelectedValue = DetallesACCION.Rows[0]["AccionPrioridad"].ToString();

            DropDownContencion.SelectedValue = DetallesACCION.Rows[0]["EstadoContencion"].ToString();
            DropDownISHIKAWA.SelectedValue = DetallesACCION.Rows[0]["OrigenCausa"].ToString();
            DropLeccionAprendida.SelectedValue = DetallesACCION.Rows[0]["LeccionAprendida"].ToString();

            if (DetallesACCION.Rows[0]["FechaCierrePrev"].ToString() != "")
            {
                InputFechaLimite.Value = Convert.ToDateTime(DetallesACCION.Rows[0]["FechaCierrePrev"]).ToString("dd/MM/yyyy");
                InputFechaLimiteOriginal.Value = Convert.ToDateTime(DetallesACCION.Rows[0]["FechaCierrePrevback"]).ToString("dd/MM/yyyy");
            }
            if (DetallesACCION.Rows[0]["FechaCierreReal"].ToString() != "")
            {
                InputFechaCierre.Value = Convert.ToDateTime(DetallesACCION.Rows[0]["FechaCierreReal"]).ToString("dd/MM/yyyy");
            }
            PilotoModal.SelectedValue = DetallesACCION.Rows[0]["APPPiloto"].ToString();
            ICONOAPP.Src = "http://facts4-srv/thermogestion/imagenes/" + DetallesACCION.Rows[0]["APPVinculadaIMG"].ToString() + "";

            //esto no va aquí
            switch (DetallesACCION.Rows[0]["APPVinculada"].ToString())
            {
                case "0": //Vacío
                          //APPLABEL.Text = "ALERTA DE CALIDAD VINCULADA";
                    break;
                case "1": //ALERTA CALIDAD
                    DATOSAPP.Style.Clear();
                    ICONOAPP.Src = "ICONOS/ICONOCALIDAD.png";
                    APPLABEL.Text = "ALERTA DE CALIDAD VINCULADA";
                    AUXNUMDLPARTESELECT.Value = DetallesACCION.Rows[0]["APPVinculada"].ToString();
                    DLPARTESELECTHIDDEN.Value = DetallesACCION.Rows[0]["APPIdVinculada"].ToString();
                    APPAccionSeleccionada = conexion.Devuelve_NoConformidad_Seleccionado(DetallesACCION.Rows[0]["APPIdVinculada"].ToString());
                    DLPARTESELECT.Text = APPAccionSeleccionada.Rows[0]["ID"].ToString();
                    DLPARTESELECT2.Text = APPAccionSeleccionada.Rows[0]["PRODUCTO"].ToString();
                    DLPARTESELECT3.InnerText = APPAccionSeleccionada.Rows[0]["MOTIVO"].ToString();
                    linkICONOAPP.HRef = Page.ResolveUrl("~/CALIDAD/Alertas_Calidad.aspx?NOCONFORMIDAD=" + APPAccionSeleccionada.Rows[0]["ID"].ToString() + "");
                    break;
                case "2": //PARTE DE MÁQUINA VINCULADO
                    DATOSAPP.Style.Clear();
                    ICONOAPP.Src = "ICONOS/ICONOMAQUINAS.png";
                    APPLABEL.Text = "PARTE DE MÁQUINA VINCULADO";
                    AUXNUMDLPARTESELECT.Value = DetallesACCION.Rows[0]["APPVinculada"].ToString();
                    DLPARTESELECTHIDDEN.Value = DetallesACCION.Rows[0]["APPIdVinculada"].ToString();
                    APPAccionSeleccionada = conexion.Devuelve_Parte_Maquina_Seleccionado(DetallesACCION.Rows[0]["APPIdVinculada"].ToString());
                    DLPARTESELECT.Text = APPAccionSeleccionada.Rows[0]["ID"].ToString();
                    DLPARTESELECT2.Text = APPAccionSeleccionada.Rows[0]["PRODUCTO"].ToString();
                    DLPARTESELECT3.InnerText = APPAccionSeleccionada.Rows[0]["MOTIVO"].ToString();
                    linkICONOAPP.HRef = "../MANTENIMIENTO/ReparacionMaquinas.aspx?PARTE=" + APPAccionSeleccionada.Rows[0]["ID"].ToString();

                    break;
                case "3": //PARTE DE MOLDE
                    DATOSAPP.Style.Clear();
                    ICONOAPP.Src = "ICONOS/ICONOMOLDES.png";
                    APPLABEL.Text = "PARTE DE MOLDE VINCULADO";
                    AUXNUMDLPARTESELECT.Value = DetallesACCION.Rows[0]["APPVinculada"].ToString();
                    DLPARTESELECTHIDDEN.Value = DetallesACCION.Rows[0]["APPIdVinculada"].ToString();
                    APPAccionSeleccionada = conexion.Devuelve_Parte_Molde_Seleccionado(DetallesACCION.Rows[0]["APPIdVinculada"].ToString());
                    DLPARTESELECT.Text = APPAccionSeleccionada.Rows[0]["ID"].ToString();
                    DLPARTESELECT2.Text = APPAccionSeleccionada.Rows[0]["PRODUCTO"].ToString();
                    DLPARTESELECT3.InnerText = APPAccionSeleccionada.Rows[0]["MOTIVO"].ToString();
                    linkICONOAPP.HRef = "http://facts4-srv/thermogestion/MANTENIMIENTO/ReparacionMoldes.aspx?PARTE=" + APPAccionSeleccionada.Rows[0]["ID"].ToString();
                    break;
                case "4": //PARÁMETROS
                    DATOSAPP.Style.Clear();
                    ICONOAPP.Src = "ICONOS/ICONOPARAMETROS.png";
                    APPLABEL.Text = "FICHA DE PARÁMETROS";
                    break;

            }

            string EVIDENCIA1 = DetallesACCION.Rows[0]["AdjuntoEVIDENCIA1"].ToString();
            string EVIDENCIA2 = DetallesACCION.Rows[0]["AdjuntoEVIDENCIA2"].ToString();
            string EVIDENCIA3 = DetallesACCION.Rows[0]["AdjuntoEVIDENCIA3"].ToString();
            string EVIDENCIA4 = DetallesACCION.Rows[0]["AdjuntoEVIDENCIA4"].ToString();

            string url1 = "@" + EVIDENCIA1;
            string ext1 = System.IO.Path.GetExtension(url1);
            if (ext1 == ".jpg" || ext1 == ".png" || ext1 == ".jpeg")
            { IMGevidencia1.Attributes.Add("src", EVIDENCIA1); }
            else
            { IMGevidencia1.Attributes.Add("src", "http://facts4-srv/thermogestion/SMARTH_fonts/INTERNOS/ICONODOCGENERICO.png"); }
            LINKevidencia1.Attributes.Add("href", EVIDENCIA1);

            string url2 = "@" + EVIDENCIA2;
            string ext2 = System.IO.Path.GetExtension(url2);
            if (ext2 == ".jpg" || ext2 == ".png" || ext2 == ".jpeg")
            { IMGevidencia2.Attributes.Add("src", EVIDENCIA2); }
            else
            { IMGevidencia2.Attributes.Add("src", "http://facts4-srv/thermogestion/SMARTH_fonts/INTERNOS/ICONODOCGENERICO.png"); }
            LINKevidencia2.Attributes.Add("href", EVIDENCIA2);

            string url3 = "@" + EVIDENCIA3;
            string ext3 = System.IO.Path.GetExtension(url3);
            if (ext3 == ".jpg" || ext3 == ".png" || ext3 == ".jpeg")
            { IMGevidencia3.Attributes.Add("src", EVIDENCIA3); }
            else
            { IMGevidencia3.Attributes.Add("src", "http://facts4-srv/thermogestion/SMARTH_fonts/INTERNOS/ICONODOCGENERICO.png"); }
            LINKevidencia3.Attributes.Add("href", EVIDENCIA3);

            string url4 = "@" + EVIDENCIA4;
            string ext4 = System.IO.Path.GetExtension(url4);
            if (ext4 == ".jpg" || ext4 == ".png" || ext4 == ".jpeg")
            { IMGevidencia4.Attributes.Add("src", EVIDENCIA4); }
            else
            { IMGevidencia4.Attributes.Add("src", "http://facts4-srv/thermogestion/SMARTH_fonts/INTERNOS/ICONODOCGENERICO.png"); }
            LINKevidencia4.Attributes.Add("href", EVIDENCIA4);

            DataTable RevisionAcciones = conexion.Devuelve_datatable_RevisionAcciones(HeaderIdPDCAXListaacciones.Text);
             try
            {
                lblUltimaRev.Text = Convert.ToDateTime(RevisionAcciones.Rows[0]["FechaApertura"]).ToString("dd/MM/yyyy") + " - " + RevisionAcciones.Rows[0]["Revision"].ToString();
                DgvListaRevs.DataSource = RevisionAcciones;
                DgvListaRevs.DataBind();
            }
            catch (Exception ex)
            { }

            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "ShowPopup();", true);


        }

        //Gestión de documentos insertados.
        public void Insertar_documento(Object sender, EventArgs e)
        {
            try
            {
                int SELECTDOC = 0;
                if(LINKevidencia1.HRef == "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg")
                { SELECTDOC = 1; }
                else if (LINKevidencia2.HRef == "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg")
                { SELECTDOC = 2; }
                else if (LINKevidencia3.HRef == "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg")
                { SELECTDOC = 3; }
                else if (LINKevidencia4.HRef == "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg")
                { SELECTDOC = 4; }
                else
                { SELECTDOC = 0; }


                if (FileUpload1.HasFile)
                { SaveFile(FileUpload1.PostedFile, SELECTDOC); }
                    
             
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
              
                string savePath = "C:\\inetpub_thermoweb\\PDCA\\PDCADOCS\\";

                // Get the name of the file to upload.
                //string fileName = FileUpload1.FileName;
                string fileName = "";
                switch (num_img)
                {
                    case 1:
                        string extension = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
                        fileName = HeaderIdPDCAXListaacciones.Text + "_" + "EVIDENCIA1" + extension;
                        break;
                    case 2:
                        string extension2 = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
                        fileName = HeaderIdPDCAXListaacciones.Text + "_" + "EVIDENCIA2" + extension2;
                        break;
                    case 3:
                        string extension3 = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
                        fileName = HeaderIdPDCAXListaacciones.Text + "_" + "EVIDENCIA3" + extension3;
                        break;
                    case 4:
                        string extension4 = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
                        fileName = HeaderIdPDCAXListaacciones.Text + "_" + "EVIDENCIA4" + extension4;
                        break;

                }

                savePath += fileName;

                // Call the SaveAs method to save the uploaded
                // file to the specified directory.
                switch (num_img)
                {
                    case 1:
                        FileUpload1.SaveAs(savePath);
                        IMGevidencia1.Attributes.Add("src", "http://facts4-srv/thermogestion/PDCA/PDCADOCS/" + fileName);
                        LINKevidencia1.Attributes.Add("href", "http://facts4-srv/thermogestion/PDCA/PDCADOCS/" + fileName);
                        break;
                    case 2:
                        FileUpload1.SaveAs(savePath);
                        IMGevidencia2.Attributes.Add("src", "http://facts4-srv/thermogestion/PDCA/PDCADOCS/" + fileName);
                        LINKevidencia2.Attributes.Add("href", "http://facts4-srv/thermogestion/PDCA/PDCADOCS/" + fileName);
                        break;
                    case 3:
                        FileUpload1.SaveAs(savePath);
                        IMGevidencia3.Attributes.Add("src", "http://facts4-srv/thermogestion/PDCA/PDCADOCS/" + fileName);
                        LINKevidencia3.Attributes.Add("href", "http://facts4-srv/thermogestion/PDCA/PDCADOCS/" + fileName);
                        break;
                    case 4:
                        FileUpload1.SaveAs(savePath);
                        IMGevidencia4.Attributes.Add("src", "http://facts4-srv/thermogestion/PDCA/PDCADOCS/" + fileName);
                        LINKevidencia4.Attributes.Add("href", "http://facts4-srv/thermogestion/PDCA/PDCADOCS/" + fileName);
                        break;
                }
            }
            catch (Exception EX)
            {
            }
            string headertipoguardado = HeaderTipoGuardado.Text;
            string headeridpdcaxlistaacciones = HeaderIdPDCAXListaacciones.Text;
            string staticbackdropLabel = staticBackdropLabel.InnerText;

            GuardarAccion(null, null);
            RecargarAccion(headertipoguardado, headeridpdcaxlistaacciones, staticbackdropLabel);

        }

        public void BorrarDocumento(object sender, EventArgs e)
        {
            HtmlButton button = (HtmlButton)sender;
            string name = button.ID;
            switch (name)
                {
                case "BTNBorrarEvidencia1":
                    IMGevidencia1.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                    LINKevidencia1.Attributes.Add("href", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                    break;
                case "BTNBorrarEvidencia2":
                    IMGevidencia2.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                    LINKevidencia2.Attributes.Add("href", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                    break;
                case "BTNBorrarEvidencia3":
                    IMGevidencia3.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                    LINKevidencia3.Attributes.Add("href", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                    break;
                case "BTNBorrarEvidencia4":
                    IMGevidencia4.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                    LINKevidencia4.Attributes.Add("href", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                    break;
                }
            string headertipoguardado = HeaderTipoGuardado.Text;
            string headeridpdcaxlistaacciones = HeaderIdPDCAXListaacciones.Text;
            string staticbackdropLabel = staticBackdropLabel.InnerText;

            GuardarAccion(null, null);
            RecargarAccion(headertipoguardado, headeridpdcaxlistaacciones, staticbackdropLabel);
        }

        public void Mail_LOGERROR(string mensaje)
        {
            string subject = "ERROR en aplicación PDCA - ";
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
            catch (Exception)
            {
            }
        }

        //OLD
      

    }
}

