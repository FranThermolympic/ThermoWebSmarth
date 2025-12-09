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

namespace ThermoWeb.KPI
{
    public partial class KPIPDCA : System.Web.UI.Page
    {

        //private static DataSet ds_DocumentosPlanta = new DataSet();
        private static DataTable Resultados_KPIPDCA = new DataTable();
        private static DataTable ListaAccionesPendientes_KPIPDCA = new DataTable();
        private static DataTable ListaAccionesPendientesXOP_KPIPDCA = new DataTable();
        private static DataTable ListaAccionesCerradosXOP_KPIPDCA = new DataTable();
        private static DataTable ListaAccionesVencidosXOP_KPIPDCA = new DataTable();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Selecaño.SelectedValue = Convert.ToString(DateTime.Now.Year);
                rellenargrids(null, null);
            }
            

        }

        public void rellenargrids(object sender, EventArgs e)
        {
            Conexion_SMARTH SHConexion = new Conexion_SMARTH();
            //RESULTADOS
            Resultados_KPIPDCA = SHConexion.Devuelve_Resultados_MES_KPIPDCA(Selecaño.SelectedValue);
            dgvResultadosMES.DataSource = Resultados_KPIPDCA;
            dgvResultadosMES.DataBind();

            //LISTADO
            ListaAccionesPendientes_KPIPDCA = SHConexion.Devuelve_lista_acciones_pendientes_KPIPDCA("");
            dgv_Acciones_Pendientes.DataSource = ListaAccionesPendientes_KPIPDCA;
            dgv_Acciones_Pendientes.DataBind();

            //VENCIDOS
            ListaAccionesVencidosXOP_KPIPDCA = SHConexion.Devuelve_total_acciones_XOp_KPIPDCA(" AND A.FechaCierreReal IS NULL AND A.FechaCierrePrev < SYSDATETIME() AND YEAR([FechaApertura]) = "+ Selecaño.SelectedValue + "");
            dgv_Acciones_VencidosXOP.DataSource = ListaAccionesVencidosXOP_KPIPDCA;
            dgv_Acciones_VencidosXOP.DataBind();
            //PENDIENTES
            ListaAccionesPendientesXOP_KPIPDCA = SHConexion.Devuelve_total_acciones_XOp_KPIPDCA(" AND A.FechaCierreReal IS NULL  AND YEAR([FechaApertura]) = " + Selecaño.SelectedValue + "");
            dgv_Acciones_PendientesXOP.DataSource = ListaAccionesPendientesXOP_KPIPDCA;
            dgv_Acciones_PendientesXOP.DataBind();

            //CERRADOS
            ListaAccionesCerradosXOP_KPIPDCA = SHConexion.Devuelve_total_acciones_XOp_KPIPDCA(" AND A.FechaCierreReal IS NOT NULL  AND YEAR([FechaApertura]) = " + Selecaño.SelectedValue + "");
            dgv_Acciones_CerradosXOP.DataSource = ListaAccionesCerradosXOP_KPIPDCA;
            dgv_Acciones_CerradosXOP.DataBind();

            //ACTUALES

            KPIAccionesCerradas.InnerText = SHConexion.Devuelve_PDCACerrados_KPIPDCA();
            KPIAccionesVencidas.InnerText = SHConexion.Devuelve_PDCAVencidos_KPIPDCA();
            KPIAccionesPendientes.InnerText = SHConexion.Devuelve_PDCAPendientes_KPIPDCA();
        }
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                try
                {


                  
                    Conexion_SMARTH SHConexion = new Conexion_SMARTH();

                    Label lblFechaCierrePrev = (Label)e.Row.FindControl("lblFechaCierrePrev");
                    if (Convert.ToDateTime(lblFechaCierrePrev.Text) < DateTime.Now)
                    {
                        Label lblVencimiento = (Label)e.Row.FindControl("lblVencimiento");
                        lblVencimiento.Text = "¡Vencido!";
                        lblVencimiento.DataBind();
                    }

                    Label DropTipo = (Label)e.Row.FindControl("lblEjecutor");
                    Label AUXDropTipo = (Label)e.Row.FindControl("AUXlblEjecutor");


                    GridViewRow prevrow = dgv_Acciones_Pendientes.Rows[e.Row.RowIndex - 1];
                    Label DropTipo2 = (Label)prevrow.FindControl("AUXlblEjecutor");

                    if (AUXDropTipo.Text == DropTipo2.Text)
                    {
                        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
                        DropTipo.Text = "";
                        DropTipo.DataBind();
                        
                        //e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#F1F1F1");

                    }

                    


                    /*

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
                    }*/
                }
                catch (Exception ex)
                { }
            }
        }
        /*
        public void CargaOperario(object sender, EventArgs e)
        {
            Conexion_PDCA conexion = new Conexion_PDCA();
            Conexion_SMARTH SHConexion = new Conexion_SMARTH();
            try
            {
                COLPlanAccion.Visible = false;
                COLNoConformidades.Visible = false;
                COLMaqReparar.Visible = false;
                COLMaqValidar.Visible = false;
                COLMolValidar.Visible = false;
                COLMolReparar.Visible = false;

                int trabajador = SHConexion.Devuelve_ID_Piloto_SMARTH(SelectPersonal.SelectedValue.ToString());

                ListaPendientesReparacionMolde = conexion.Devuelve_MiPDCA_Pendientes_Reparacion_Molde(trabajador);
                int NUMPendientesReparacionMolde = ListaPendientesReparacionMolde.Rows.Count;

                ListaPendientesValidacionMolde = conexion.Devuelve_MiPDCA_Pendientes_Validacion_Molde(trabajador);
                int NUMPendientesValidacionMolde = ListaPendientesValidacionMolde.Rows.Count;

                ListaPendientesReparacionMaquina = conexion.Devuelve_MiPDCA_Pendientes_Reparacion_Maquina(trabajador);
                int NUMPendientesReparacionMaquina = ListaPendientesReparacionMaquina.Rows.Count;

                ListaPendientesValidacionMaquina = conexion.Devuelve_MiPDCA_Pendientes_Validacion_Maquina(trabajador);
                int NUMListaPendientesValidacionMaquina = ListaPendientesValidacionMaquina.Rows.Count;

                ListaPendientesNoConformidades = conexion.Devuelve_MiPDCA_Pendientes_No_Conformidades(trabajador);
                int NUMListaPendientesNoConformidades = ListaPendientesNoConformidades.Rows.Count;

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
                dgvListaPendientesPDCA.DataSource = ListaPendientesPDCA;
                dgvListaPendientesPDCA.DataBind();

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

            }
            catch (Exception ex)
            { }



        }

        public void CargaOperarioSession()
        {
            
            Conexion_SMARTH SHConexion = new Conexion_SMARTH();
            try
            {
                COLPlanAccion.Visible = false;
                COLNoConformidades.Visible = false;
                COLMaqReparar.Visible = false;
                COLMaqValidar.Visible = false;
                COLMolValidar.Visible = false;
                COLMolReparar.Visible = false;

                int trabajador = SHConexion.Devuelve_ID_Piloto_SMARTH(Session["Nombre"].ToString());

               

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
                dgvListaPendientesPDCA.DataSource = ListaPendientesPDCA;
                dgvListaPendientesPDCA.DataBind();

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

            }
            catch (Exception ex)
            { }



        }*/

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

