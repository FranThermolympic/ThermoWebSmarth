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
    public partial class MiPDCA : System.Web.UI.Page
    {

        //private static DataSet ds_DocumentosPlanta = new DataSet();

        private static DataTable ListaPendientesReparacionMolde = new DataTable();
        private static DataTable ListaPendientesValidacionMolde = new DataTable();
        private static DataTable ListaPendientesReparacionMaquina = new DataTable();
        private static DataTable ListaPendientesValidacionMaquina = new DataTable();
        private static DataTable ListaPendientesNoConformidades = new DataTable();
        private static DataTable ListaVencidasNoConformidades = new DataTable();
        private static DataTable ListaPendientesPDCA = new DataTable();
        private static DataTable ListaJaulaRechazo = new DataTable();
        private static DataTable ListaMuroCalidad = new DataTable();


        private static int NUMPendientesReparacionMolde = 0;
        private static int NUMPendientesValidacionMolde = 0;
        private static int NUMPendientesReparacionMaquina = 0;
        private static int NUMListaPendientesValidacionMaquina = 0;
        private static int NUMListaPendientesNoConformidades = 0;
        private static int NUMListaVencidasNoConformidades = 0;
        private static int NUMListaPendientesPDCA = 0;
        private static int NUMJaulaRechazo = 0;
        private static int NUMGP12Vencidas = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
                if (Session["Nombre"] != null)
                {
                    CargaOperarioSession();
                    selectpersonalrow.Visible = false;
                    if (Request.QueryString["ACCESO"] != null)
                    {
                        string SECCION = Request.QueryString["ACCESO"];
                        switch (SECCION)
                        {
                            case "PLANACCION":
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "VerAcciones();", true);
                                break;
                            case "NOCONFORMIDAD":
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "VerNOCONFORMIDADES();", true);
                                break;
                            case "REPARACIONMOLDES":
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "VerMOLREPARAR();", true);
                                break;
                            case "VALIDACIONMOLDES":
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "VerMOLVALIDAR();", true);
                                break;
                            case "REPARACIONMAQUINAS":
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "VerMAQREPARAR();", true);
                                break;
                            case "VALIDACIONMAQUINAS":
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "VerMAQVALIDAR();", true);
                                break;
                        }

                    }

                }

                else
                {
                    Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                    DataSet Personal = SHConexion.Devuelve_mandos_intermedios_SMARTH();
                    SelectPersonal.Items.Clear();
                    foreach (DataRow row in Personal.Tables[0].Rows)
                    {
                        SelectPersonal.Items.Add(row["Nombre"].ToString());

                    }
                    }
            
        }

        protected void GestionaVistaDept()
        {
            //Botones off
            PILLMURODECALIDAD.Visible = false;
            PILLACCIONES.Visible = false;
            PILLNOCONFORMIDADES.Visible = false;
            PILLMOLREPARAR.Visible = false;
            PILLMAQREPARAR.Visible = false;

            COLPlanAccion.Visible = false;
            COLNoConformidades.Visible = false;
            COLMaqReparar.Visible = false;
            COLMaqValidar.Visible = false;
            COLMolValidar.Visible = false;
            COLMolReparar.Visible = false;


            if (Session["Departamento"].ToString() == "-" || Session["Departamento"].ToString() == "ADMINISTRADOR" || Session["Departamento"].ToString() == "INGENIERIA")
            {
                PILLMURODECALIDAD.Visible = true;
                PILLACCIONES.Visible = true;
                PILLNOCONFORMIDADES.Visible = true;
                PILLMOLREPARAR.Visible = true;
                PILLMAQREPARAR.Visible = true;

                if (NUMJaulaRechazo > 0)
                {
                    COLJaulaRechazo.Visible = true;
                    BADGEJAULA.InnerText = NUMJaulaRechazo.ToString();
                }
                if (NUMListaPendientesPDCA > 0)
                {
                    COLPlanAccion.Visible = true;
                    BADGEACCION.InnerText = NUMListaPendientesPDCA.ToString();
                }
                if (NUMListaPendientesNoConformidades > 0)
                {
                    COLNoConformidades.Visible = true;
                    BADGENC.InnerText = NUMListaPendientesNoConformidades.ToString();
                }
                if (NUMListaVencidasNoConformidades > 0)
                {
                    COLNoConformidadesVENC.Visible = true;
                    BADGENCVEN.InnerText = NUMListaVencidasNoConformidades.ToString();
                }
                if (NUMPendientesReparacionMolde > 0)
                {
                    COLMolReparar.Visible = true;
                    BADGEMOLDREP.InnerText = NUMPendientesReparacionMolde.ToString();
                }
                if (NUMPendientesValidacionMolde > 0)
                {
                    COLMolValidar.Visible = true;
                    BADGEMOLDVAL.InnerText = NUMPendientesValidacionMolde.ToString();
                }
                if (NUMPendientesReparacionMaquina > 0)
                {
                    COLMaqReparar.Visible = true;
                    BADGEMAQREP.InnerText = NUMPendientesReparacionMaquina.ToString();
                }
                if (NUMListaPendientesValidacionMaquina > 0)
                {
                    COLMaqValidar.Visible = true;
                    BADGEMAQVAL.InnerText = NUMListaPendientesValidacionMaquina.ToString();
                }
                if (NUMGP12Vencidas > 0)
                {
                    COLGP12.Visible = true;
                    BADGEGP12.InnerText = NUMGP12Vencidas.ToString();
                }
            }
            else
            {
               
                PILLACCIONES.Visible = true;
                PILLMOLREPARAR.Visible = true;
                PILLMAQREPARAR.Visible = true;
                if (NUMListaPendientesPDCA > 0)
                {
                    COLPlanAccion.Visible = true;
                    BADGEACCION.InnerText = NUMListaPendientesPDCA.ToString();
                }   
                if (NUMPendientesReparacionMolde > 0)
                {
                    COLMolReparar.Visible = true;
                    BADGEMOLDREP.InnerText = NUMPendientesReparacionMolde.ToString();
                }
                if (NUMPendientesValidacionMolde > 0)
                {
                    COLMolValidar.Visible = true;
                    BADGEMOLDVAL.InnerText = NUMPendientesValidacionMolde.ToString();
                }
                if (NUMPendientesReparacionMaquina > 0)
                {
                    COLMaqReparar.Visible = true;
                    BADGEMAQREP.InnerText = NUMPendientesReparacionMaquina.ToString();
                }
                if (NUMListaPendientesValidacionMaquina > 0)
                {
                    COLMaqValidar.Visible = true;
                    BADGEMAQVAL.InnerText = NUMListaPendientesValidacionMaquina.ToString();
                }
            }
           

            //Selección según departamentos
           


        }
        public void CargaOperario(object sender, EventArgs e)
        {
            
            Conexion_PDCA conexion = new Conexion_PDCA();
            Conexion_SMARTH SHConexion = new Conexion_SMARTH();
            try
            {

                int trabajador = SHConexion.Devuelve_ID_Piloto_SMARTH(SelectPersonal.SelectedValue.ToString());

                ListaPendientesReparacionMolde = conexion.Devuelve_MiPDCA_Pendientes_Reparacion_Molde(trabajador);
                int NUMPendientesReparacionMolde = ListaPendientesReparacionMolde.Rows.Count;

                ListaPendientesValidacionMolde = conexion.Devuelve_MiPDCA_Pendientes_Validacion_Molde(trabajador);
                int NUMPendientesValidacionMolde = ListaPendientesValidacionMolde.Rows.Count;

                ListaPendientesReparacionMaquina = conexion.Devuelve_MiPDCA_Pendientes_Reparacion_Maquina(trabajador);
                int NUMPendientesReparacionMaquina = ListaPendientesReparacionMaquina.Rows.Count;

                ListaPendientesValidacionMaquina = conexion.Devuelve_MiPDCA_Pendientes_Validacion_Maquina(trabajador);
                int NUMListaPendientesValidacionMaquina = ListaPendientesValidacionMaquina.Rows.Count;

                ListaPendientesNoConformidades = conexion.Devuelve_MiPDCA_Pendientes_No_ConformidadesEnCurso(trabajador);
                int NUMListaPendientesNoConformidades = ListaPendientesNoConformidades.Rows.Count;

                ListaVencidasNoConformidades= conexion.Devuelve_MiPDCA_Pendientes_No_ConformidadesVencidas(trabajador);
                int NUMListaVencidasNoConformidades = ListaVencidasNoConformidades.Rows.Count;

                ListaPendientesPDCA = conexion.Devuelve_MiPDCA_Pendientes_Acciones(trabajador);
                int NUMListaPendientesPDCA = ListaPendientesPDCA.Rows.Count;

                dgvListaPendientesReparacionMolde.DataSource = ListaPendientesReparacionMolde;
                dgvListaPendientesReparacionMolde.DataBind();
                dgvListaPendientesValidacionMolde.DataSource = ListaPendientesValidacionMolde;
                dgvListaPendientesValidacionMolde.DataBind();
                dgvListaPendientesReparacionMaquina.DataSource = ListaPendientesReparacionMaquina;
                dgvListaPendientesReparacionMaquina.DataBind();
                dgvListaPendientesValidacionMaquina.DataSource = ListaPendientesValidacionMaquina;
                dgvListaPendientesValidacionMaquina.DataBind();
                dgvListaPendientesNoConformidades.DataSource = ListaPendientesNoConformidades;
                dgvListaPendientesNoConformidades.DataBind();

                //ACCIONES QUE DIRIJO
                ListaPendientesPDCA.DefaultView.RowFilter = "JEFE = '"+ Session["Nombre"].ToString() + "'";
                DataTable DTPDCA = (ListaPendientesPDCA.DefaultView).ToTable();
                dgvListaPlanesPendientesPDCA.DataSource = DTPDCA;
                dgvListaPlanesPendientesPDCA.DataBind();

                //ACCIONES QUE EJECUTO
                ListaPendientesPDCA.DefaultView.RowFilter = "EJECUTA = '" + Session["Nombre"].ToString() + "'";
                DTPDCA = (ListaPendientesPDCA.DefaultView).ToTable();
                GridAccionesPendientes.DataSource = DTPDCA;
                GridAccionesPendientes.DataBind();

                if (NUMListaPendientesPDCA > 0)
                {
                    COLPlanAccion.Visible = true;
                    BADGEACCION.InnerText = NUMListaPendientesPDCA.ToString();
                }
                if (NUMListaPendientesNoConformidades > 0)
                {
                    COLNoConformidades.Visible = true;
                    BADGENC.InnerText = NUMListaPendientesNoConformidades.ToString();
                }
                if (NUMPendientesReparacionMolde > 0)
                {
                    COLMolReparar.Visible = true;
                    BADGEMOLDREP.InnerText = NUMPendientesReparacionMolde.ToString();
                }
                if (NUMPendientesValidacionMolde > 0)
                {
                    COLMolValidar.Visible = true;
                    BADGEMOLDVAL.InnerText = NUMPendientesValidacionMolde.ToString();
                }
                if (NUMPendientesReparacionMaquina > 0)
                {
                    COLMaqReparar.Visible = true;
                    BADGEMAQREP.InnerText = NUMPendientesReparacionMaquina.ToString();

                }
                if (NUMListaPendientesValidacionMaquina > 0)
                {
                    COLMaqValidar.Visible = true;
                    BADGEMAQVAL.InnerText = NUMListaPendientesValidacionMaquina.ToString();
                }
                GestionaVistaDept();
            }
            catch (Exception ex)
            { }

           

        }

        public void CargaOperarioSession()
        {
            Conexion_PDCA conexion = new Conexion_PDCA();
            Conexion_SMARTH SHConexion = new Conexion_SMARTH();
            try
            {
               

                int trabajador = SHConexion.Devuelve_ID_Piloto_SMARTH(Session["Nombre"].ToString());

                ListaPendientesReparacionMolde = conexion.Devuelve_MiPDCA_Pendientes_Reparacion_Molde(trabajador);
                NUMPendientesReparacionMolde = ListaPendientesReparacionMolde.Rows.Count;

                ListaPendientesValidacionMolde = conexion.Devuelve_MiPDCA_Pendientes_Validacion_Molde(trabajador);
                NUMPendientesValidacionMolde = ListaPendientesValidacionMolde.Rows.Count;

                ListaPendientesReparacionMaquina = conexion.Devuelve_MiPDCA_Pendientes_Reparacion_Maquina(trabajador);
                NUMPendientesReparacionMaquina = ListaPendientesReparacionMaquina.Rows.Count;

                ListaPendientesValidacionMaquina = conexion.Devuelve_MiPDCA_Pendientes_Validacion_Maquina(trabajador);
                NUMListaPendientesValidacionMaquina = ListaPendientesValidacionMaquina.Rows.Count;

                ListaPendientesNoConformidades = conexion.Devuelve_MiPDCA_Pendientes_No_ConformidadesEnCurso(trabajador);
                NUMListaPendientesNoConformidades = ListaPendientesNoConformidades.Rows.Count;

                ListaVencidasNoConformidades = conexion.Devuelve_MiPDCA_Pendientes_No_ConformidadesVencidas(trabajador);
                NUMListaVencidasNoConformidades = ListaVencidasNoConformidades.Rows.Count;

                ListaPendientesPDCA = conexion.Devuelve_MiPDCA_Pendientes_Acciones(trabajador);
                NUMListaPendientesPDCA = ListaPendientesPDCA.Rows.Count;

                ListaJaulaRechazo = SHConexion.Devuelve_Area_Rechazo(" and [ResponsableSalida] = " + trabajador + " AND FechaSalida is null");
                NUMJaulaRechazo = ListaJaulaRechazo.Rows.Count;

                ListaMuroCalidad = SHConexion.Devuelve_Estado_Referencias("", "", "", "", " AND D.Nombre like '" + Session["Nombre"].ToString().Trim() + "%'", "AND(Fechaprevsalida IS NOT NULL AND Fechaprevsalida < SYSDATETIME() AND EstadoActual <> 0 AND EstadoActual <> 7)");
                NUMGP12Vencidas = ListaMuroCalidad.Rows.Count;

                dgvListaPendientesReparacionMolde.DataSource = ListaPendientesReparacionMolde;
                dgvListaPendientesReparacionMolde.DataBind();
                dgvListaPendientesValidacionMolde.DataSource = ListaPendientesValidacionMolde;
                dgvListaPendientesValidacionMolde.DataBind();
                dgvListaPendientesReparacionMaquina.DataSource = ListaPendientesReparacionMaquina;
                dgvListaPendientesReparacionMaquina.DataBind();
                dgvListaPendientesValidacionMaquina.DataSource = ListaPendientesValidacionMaquina;
                dgvListaPendientesValidacionMaquina.DataBind();
                dgvListaPendientesNoConformidades.DataSource = ListaPendientesNoConformidades;
                dgvListaPendientesNoConformidades.DataBind();
                dgvListaVencidasNoConformidades.DataSource = ListaVencidasNoConformidades;
                dgvListaVencidasNoConformidades.DataBind();

                dgvJaulaRechazo.DataSource = ListaJaulaRechazo;
                dgvJaulaRechazo.DataBind();

                dgvMuroCalidad.DataSource = ListaMuroCalidad;
                dgvMuroCalidad.DataBind();

                //ACCIONES QUE DIRIJO
                ListaPendientesPDCA.DefaultView.RowFilter = "JEFE = '" + Session["Nombre"].ToString() + "'";
                DataTable DTPDCA = (ListaPendientesPDCA.DefaultView).ToTable();
                dgvListaPlanesPendientesPDCA.DataSource = DTPDCA;
                dgvListaPlanesPendientesPDCA.DataBind();

                //ACCIONES QUE EJECUTO
                ListaPendientesPDCA.DefaultView.RowFilter = "EJECUTA = '" + Session["Nombre"].ToString() + "'";
                DTPDCA = (ListaPendientesPDCA.DefaultView).ToTable();
                GridAccionesPendientes.DataSource = DTPDCA;
                GridAccionesPendientes.DataBind();
                GestionaVistaDept();


            }
            catch (Exception ex)
            { }



        }

        public void RedireccionaAPP(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToString())
            {
                case "RepMoldes":
                    Response.Redirect("http://facts4-srv/thermogestion/MANTENIMIENTO/ReparacionMoldes.aspx?PARTE=" + e.CommandArgument.ToString());
                    break;
                case "ValMoldes":
                    Response.Redirect("http://facts4-srv/thermogestion/MANTENIMIENTO/ReparacionMoldes.aspx?PARTE=" + e.CommandArgument.ToString());
                    break;
                case "RepMaquinas":
                    Response.Redirect("../MANTENIMIENTO/ReparacionMaquinas.aspx?PARTE=" + e.CommandArgument.ToString());
                    break;
                case "ValMaquinas":
                    Response.Redirect("../MANTENIMIENTO/ReparacionMaquinas.aspx?PARTE=" + e.CommandArgument.ToString());
                    break;
                case "NoConformidades":
                    Response.Redirect("~/CALIDAD/Alertas_Calidad.aspx?NOCONFORMIDAD=" + e.CommandArgument.ToString());
                    break;
                case "PlanAccion":
                    Response.Redirect("~/PDCA/PDCA_DETALLE.aspx?IDPDCA=" + e.CommandArgument.ToString());
                    break;
                case "RedirectJaula":
                    Response.Redirect("~/AREA_RECHAZO/Area_Rechazo.aspx");
                    break;
                case "RedMuroCalidad":
                    Response.Redirect("~/GP12/GP12HistoricoReferencia.aspx?REFERENCIA=" + e.CommandArgument.ToString());
                    break;

            }
            
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

    }
}

