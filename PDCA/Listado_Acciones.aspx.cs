using System;
using System.Collections.Generic;
using System.Collections;
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
    public partial class Listado_Acciones : System.Web.UI.Page
    {

        //private static DataSet ds_DocumentosPlanta = new DataSet();

        private static DataTable APPAccionSeleccionada = new DataTable();
             
        private static string FILTRO = "";
        //private int PDCAINT = 2;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                CargaDropDowns();
                RellenarGrids(null, null);

            }
        }
        public void RellenarGrids(object sender, EventArgs e)
        {
            try
            {
                Conexion_PDCA conexion = new Conexion_PDCA();
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                
                string MAQUINA = "";
                if (FiltroMaquina.SelectedValue.ToString() != "-")
                {
                    MAQUINA = " AND AccionMaquina = '" + FiltroMaquina.SelectedValue.ToString().Trim() + "'";
                }
                string MOLDE = "";
                if (FiltroMolde.SelectedValue.ToString() != "-")
                {
                    MOLDE = " AND Molde = '" + FiltroMolde.SelectedValue.ToString().Trim() + "'";
                }
                string ORIGEN = "";
                if (FiltroOrigen.SelectedValue.ToString() != "0")
                {
                    ORIGEN = " AND OrigenCausa = " + FiltroOrigen.SelectedValue.ToString().Trim() + "";
                }
                string TIPO = "";
                if (FiltroTipo.SelectedValue.ToString() != "0")
                {
                    TIPO = " AND Tipo = " + FiltroTipo.SelectedValue.ToString().Trim() + "";
                }
                string CLIENTE = "";
                if (FiltroCliente.SelectedValue.ToString() != "-")
                {
                    CLIENTE = " AND Cliente = '" + FiltroCliente.SelectedValue.ToString().Trim() + "'";
                }
                string MATERIAL = "";
                if (FiltroMaterial.SelectedValue.ToString() != "-")
                {
                    MATERIAL = " AND Material = '" + FiltroMaterial.SelectedValue.ToString().Trim() + "'";
                }
                string LESSONLEARNT = "";
                if (FiltroLeccionAprendida.SelectedValue.ToString() != "0")
                {
                    LESSONLEARNT = " AND LeccionAprendida = '" + FiltroLeccionAprendida.SelectedValue.ToString().Trim() + "'";
                }
                string FILTRO = MAQUINA + MOLDE + CLIENTE + MATERIAL;
                string FILTRO2 = ORIGEN + TIPO + LESSONLEARNT;
                gvOrders.DataSource = SHConexion.Devuelve_Lecciones_Aprendidas(FILTRO, FILTRO2);
                gvOrders.DataBind();
           
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
                  
                }
                PilotoModal.ClearSelection();
                PilotoModal.SelectedValue = "-";

                DataTable LeccionesAprendidas = SHConexion.Devuelve_Lecciones_Aprendidas_DROPDOWNS(" DISTINCT AccionMaquina ");
                //FILTRO DE MAQUINA
               
                foreach (DataRow row in LeccionesAprendidas.DefaultView.ToTable().Rows)
                {
                    FiltroMaquina.Items.Add(row["AccionMaquina"].ToString());
                }

                //FILTRO DE MOLDE
                LeccionesAprendidas = SHConexion.Devuelve_Lecciones_Aprendidas_DROPDOWNS(" DISTINCT Molde ");               
                foreach (DataRow row in LeccionesAprendidas.DefaultView.ToTable().Rows)
                {
                    FiltroMolde.Items.Add(row["Molde"].ToString());
                }

                //FILTRO DE CLIENTE
                LeccionesAprendidas = SHConexion.Devuelve_Lecciones_Aprendidas_DROPDOWNS(" DISTINCT Cliente ");            
                foreach (DataRow row in LeccionesAprendidas.DefaultView.ToTable().Rows)
                {
                    FiltroCliente.Items.Add(row["Cliente"].ToString());
                }

                //FILTRO DE MATERIAL
        
                LeccionesAprendidas = SHConexion.Devuelve_Lecciones_Aprendidas_DROPDOWNS(" DISTINCT Material ");             
                foreach (DataRow row in LeccionesAprendidas.DefaultView.ToTable().Rows)
                {
                    FiltroMaterial.Items.Add(row["Material"].ToString());
                }


            }
            catch (Exception)
            {

            }
        }

        //eliminar    

        protected void OnRowDataBoundAUX(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label LBLLECCIONHIDDEN = (Label)e.Row.FindControl("lblLeccionHidden");
                    Label LBLLECCION = (Label)e.Row.FindControl("LblLeccion");

                    if (LBLLECCIONHIDDEN.Text == "2")
                    {
                        LBLLECCION.Visible = true;
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
                    DropMaquina.Text = "0";
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
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "ShowPopup();", true);
                }
                if (e.CommandName == "EditAccion")
                {
                    try
                    {
                        HeaderTipoGuardado.Text = "EDITAR";
                        string[] idaccion = e.CommandArgument.ToString().Split(new char[] { '¬' });
                        string id = idaccion[0];
                        string desviacion = idaccion[1];
                        staticBackdropLabel.InnerText = "PLAN DE ACCIÓN: " + desviacion;
                        DropAccionEstado.SelectedValue = "0";
                        DropAccionPrioridad.SelectedValue = "0";
                        DropTipoaccion.SelectedIndex = 0;
                        DropMaquina.Text= "0";
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
                        { DropMaquina.Text= "0"; }
                        else
                        {
                            DropMaquina.Text = SHConexion.Devuelve_MAQUINAS_SELECCIONADA(DetallesACCION.Rows[0]["AccionMaquina"].ToString());
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
                        ICONOAPP.Src = "http://facts4-srv/thermogestion/imagenes/" + DetallesACCION.Rows[0]["APPVinculadaIMG"].ToString() + "";

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
                                linkICONOAPP.HRef = Page.ResolveUrl("~/CALIDAD/Alertas_Calidad.aspx?NOCONFORMIDAD=" + APPAccionSeleccionada.Rows[0]["ID"].ToString() + "");
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
                    catch (Exception ex)
                    { 
                    }
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
            DropMaquina.Text= "0";
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
            { DropMaquina.Text= "0"; }
            else
            {
                DropMaquina.Text = SHConexion.Devuelve_MAQUINAS_SELECCIONADA(DetallesACCION.Rows[0]["AccionMaquina"].ToString());
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

