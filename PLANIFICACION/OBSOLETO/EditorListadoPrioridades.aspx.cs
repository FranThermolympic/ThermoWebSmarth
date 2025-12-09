using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
//using System.Data.OracleClient;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Web.Services;
using System.Web.Script.Services;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using System.Net.Mail;
using System.Net;
using DocumentFormat.OpenXml.Packaging;



namespace ThermoWeb.PLANIFICACION
{
    public partial class EditorPrioridades : System.Web.UI.Page
    {

        private static DataSet ds_Referencias = new DataSet();
        private static DataSet ds_Referencias_seq_0 = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //Conexion_PLANIFICACION conexion = new Conexion_PLANIFICACION();
                //ds_Referencias = conexion.cargar_acciones_abiertas();
                //CargaListasFiltro();
                cargar_todas();
                rellenar_grid();
            }

        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkTest = (CheckBox)sender;
            GridViewRow grdRow = (GridViewRow)chkTest.NamingContainer;
            //TextBox FirstName = (TextBox)grdRow.FindControl("TextBox2");
            //TextBox LastName = (TextBox)grdRow.FindControl("TextBox3");

            DropDownList Prioridad = (DropDownList)grdRow.FindControl("Selecprioridad");
            TextBox AccionOrden = (TextBox)grdRow.FindControl("txtAccionOrden");

            if (chkTest.Checked)
            {
                Prioridad.Enabled = false;
                AccionOrden.ReadOnly = true;
                //FirstName.ReadOnly = false;
                //LastName.ReadOnly = false;
                //FirstName.ForeColor = System.Drawing.Color.Black;
                //LastName.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                Prioridad.Enabled = true;
                AccionOrden.ReadOnly = false;
                //FirstName.ReadOnly = true;
                //LastName.ReadOnly = true;
                //FirstName.ForeColor = System.Drawing.Color.Blue;
                //LastName.ForeColor = System.Drawing.Color.Blue;
            }
        }

        private static readonly string strConnection = ConfigurationManager.ConnectionStrings["BMS"].ConnectionString;

        protected void ActualizarOrdenesSecuencial(object sender, EventArgs e)
        {

            OracleConnection con = new OracleConnection(strConnection);
            OracleCommand cmd = new OracleCommand();

            //Comprueba todas las lineas del gridview y verifica si está marcado el cajetín
            try
            {
                cmd.Connection = con;
                con.Open();
                for (int i = 0; i < dgv_AccionesAbiertas.Rows.Count; i++)
                {
                    CheckBox chkUpdate = (CheckBox)dgv_AccionesAbiertas.Rows[i].Cells[0].FindControl("CheckBox1");

                    if (chkUpdate != null)
                    {
                        if (chkUpdate.Checked)
                        {
                            // Get the values of textboxes using findControl
                            string orden = ((Label)dgv_AccionesAbiertas.Rows[i].FindControl("lblOrden")).Text;
                            string Prioridad = ((DropDownList)dgv_AccionesAbiertas.Rows[i].FindControl("Selecprioridad")).SelectedValue;
                            string AccionOrden = ((TextBox)dgv_AccionesAbiertas.Rows[i].FindControl("txtAccionOrden")).Text;

                            //edit the data to database:
                            string strUpdate = " UPDATE PCMS.T_JOBS SET C_PRIORITY = " + Prioridad + ", C_REMARKS = '" + AccionOrden + "' WHERE C_ID = '" + orden + "'";
                                cmd.CommandType = CommandType.Text;
                                cmd.CommandText = strUpdate.ToString();
                                cmd.ExecuteNonQuery();
                        }
                    }
                }
                con.Close();
            }
            catch (SqlException ex)
            {
                string errorMsg = "Error al actualizar";
                errorMsg += ex.Message;
                throw new Exception(errorMsg);
            }
            finally
            {
                con.Close();
                cargar_todas();
                rellenar_grid();
            }


        }

        private void rellenar_grid()
        {
            try
            {
                dgv_AccionesAbiertas.DataSource = ds_Referencias;
                dgv_AccionesAbiertas.DataBind();
                GridView2.DataSource = ds_Referencias_seq_0;
                GridView2.DataBind();
            }
            catch (Exception)
            { 
                
            }
        }
        // guarda una fila
        /*public void GridView_RowUpdating(Object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                //Ëdición directa BMS
               
                Label orden = (Label)dgv_AccionesAbiertas.Rows[e.RowIndex].FindControl("txtOrden");
                Label orden2 = (Label)dgv_AccionesAbiertas.Rows[e.RowIndex].FindControl("lblOrden");
                Label referencia = (Label)dgv_AccionesAbiertas.Rows[e.RowIndex].FindControl("txtReferenciaREF");
                TextBox AccionOrden = (TextBox)dgv_AccionesAbiertas.Rows[e.RowIndex].FindControl("txtAccionOrden");
                TextBox AccionProducto = (TextBox)dgv_AccionesAbiertas.Rows[e.RowIndex].FindControl("txtAccionProducto");
                DropDownList Prioridad = (DropDownList)dgv_AccionesAbiertas.Rows[e.RowIndex].FindControl("Selecprioridad");

                Conexion_PLANIFICACION conexion = new Conexion_PLANIFICACION();
                conexion.Actualizar_Remarks_ProductoBMS(referencia.Text, AccionProducto.Text);
                conexion.Actualizar_Remarks_OrdenesBMS(Convert.ToInt32(Prioridad.SelectedValue), orden.Text, AccionOrden.Text);
                dgv_AccionesAbiertas.EditIndex = -1;
                //cargar_todas(null, e);
            }
            catch (Exception)
            {
               
            }              
        }
        */
        public void BorrarPrioridades(Object sender, EventArgs e)
            {
            try 
            {
                Conexion_PLANIFICACION conexion = new Conexion_PLANIFICACION();
                conexion.Borrar_Prioridades_OrdenesBMS();
                cargar_todas();
                rellenar_grid();
            }
            catch (Exception)
            { }
            }
        protected void cargar_todas()
        {

            Conexion_PLANIFICACION conexion = new Conexion_PLANIFICACION();
            //ds_Referencias = conexion.cargar_acciones_abiertas();
            string orderby = " ORDER BY Prioridad ASC, j1.C_SEQNR ASC";
            ds_Referencias = conexion.cargar_acciones_abiertas_ordenadas(orderby, Convert.ToInt32(TipoAlerta.SelectedValue));
            ds_Referencias_seq_0 = conexion.cargar_acciones_abiertas_ordenadasSEQ0(orderby);
            rellenar_grid();
        }

        protected void cargar_Ordenados(object sender, EventArgs e)
        {
            HtmlButton button = (HtmlButton)sender;
            //LinkButton button = (LinkButton)sender;
            string name = button.ID;
            Conexion_PLANIFICACION conexion = new Conexion_PLANIFICACION();
            string orderby = "";
            switch (name)
            {
                case "OrdenarProdASC":
                    orderby = " ORDER BY j1.C_SEQNR, Tiempo_restante";
                    ds_Referencias = conexion.cargar_acciones_abiertas_ordenadas(orderby, Convert.ToInt32(TipoAlerta.SelectedValue));
                    rellenar_grid();
                    break;

                case "OrdenarPriorASC":
                    orderby = " ORDER BY Prioridad ASC, j1.C_SEQNR ASC";
                    ds_Referencias = conexion.cargar_acciones_abiertas_ordenadas(orderby, Convert.ToInt32(TipoAlerta.SelectedValue));
                    dgv_AccionesAbiertas.DataSource = ds_Referencias;
                    dgv_AccionesAbiertas.DataBind();
                    rellenar_grid();
                    break;

                case "OrdenarPriorDESC":
                    orderby = " ORDER BY Prioridad DESC, j1.C_SEQNR ASC";
                    ds_Referencias = conexion.cargar_acciones_abiertas_ordenadas(orderby, Convert.ToInt32(TipoAlerta.SelectedValue));
                    dgv_AccionesAbiertas.DataSource = ds_Referencias;
                    dgv_AccionesAbiertas.DataBind();
                    rellenar_grid();
                    break;

                case "OrdenarMaqASC":
                    orderby = " ORDER BY Maquina ASC, j1.C_SEQNR ASC";
                    ds_Referencias = conexion.cargar_acciones_abiertas_ordenadas(orderby, Convert.ToInt32(TipoAlerta.SelectedValue));
                    rellenar_grid();
                    OrdenarMaqASC.Visible = false;
                    OrdenarMaqDESC.Visible = true;
                    break;

                case "OrdenarMaqDESC":
                    orderby = " ORDER BY Maquina DESC, j1.C_SEQNR ASC";
                    ds_Referencias = conexion.cargar_acciones_abiertas_ordenadas(orderby, Convert.ToInt32(TipoAlerta.SelectedValue));
                    rellenar_grid();
                    OrdenarMaqASC.Visible = true;
                    OrdenarMaqDESC.Visible = false;
                    break;

                case "OrdenarOrdenASC":
                    orderby = " ORDER BY Orden ASC, j1.C_SEQNR ASC";
                    ds_Referencias = conexion.cargar_acciones_abiertas_ordenadas(orderby, Convert.ToInt32(TipoAlerta.SelectedValue));
                    rellenar_grid();
                    OrdenarOrdenASC.Visible = false;
                    OrdenarOrdenDESC.Visible = true;
                    break;

                case "OrdenarOrdenDESC":
                    orderby = " ORDER BY Orden DESC, j1.C_SEQNR ASC";
                    ds_Referencias = conexion.cargar_acciones_abiertas_ordenadas(orderby, Convert.ToInt32(TipoAlerta.SelectedValue));
                    rellenar_grid();
                    OrdenarOrdenASC.Visible = true;
                    OrdenarOrdenDESC.Visible = false;
                    break;

                case "OrdenarReferenciaASC":
                    orderby = " ORDER BY REF ASC, j1.C_SEQNR ASC";
                    ds_Referencias = conexion.cargar_acciones_abiertas_ordenadas(orderby, Convert.ToInt32(TipoAlerta.SelectedValue));
                    rellenar_grid();
                    OrdenarReferenciaASC.Visible = false;
                    OrdenarReferenciaDESC.Visible = true;
                    break;

                case "OrdenarReferenciaDESC":
                    orderby = " ORDER BY REF DESC, j1.C_SEQNR ASC";
                    ds_Referencias = conexion.cargar_acciones_abiertas_ordenadas(orderby, Convert.ToInt32(TipoAlerta.SelectedValue));
                    rellenar_grid();
                    OrdenarReferenciaASC.Visible = true;
                    OrdenarReferenciaDESC.Visible = false;
                    break;
            }
        }

        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList Prioridad = (DropDownList)e.Row.FindControl("Selecprioridad");
                DataRowView dr = e.Row.DataItem as DataRowView;
                Prioridad.SelectedValue = dr["Prioridad"].ToString();               
            }
        }

        public void mandar_mailtesting(string mensaje, string subject)
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
                        GridView2.RenderControl(hw);
                        StringReader sr = new StringReader(sw.ToString());
                        //MailMessage mm = new MailMessage("bms@thermolympic.es", "pedro@thermolympic.es");
                        MailMessage mm = new MailMessage();
                        Conexion_PLANIFICACION conexion = new Conexion_PLANIFICACION();
                        DataSet ds_correos = conexion.leer_correosPrioridades();
                        foreach (DataRow row in ds_correos.Tables[0].Rows)
                        {
                            mm.To.Add(new MailAddress(row["MAIL"].ToString()));
                        }
                        mm.From = new MailAddress("bms@thermolympic.es");
                        //mm.To.Add(new MailAddress("pedro@thermolympic.es"));
                        mm.Subject = "PRODUCCIÓN - Nuevas prioridades disponibles para su consulta.";
                        mm.Body = "Se han realizado modificaciones en el listado de prioridades.<br><a href='http://facts4-srv/thermogestion/PLANIFICACION/ListadoPrioridades.aspx'>Consulta el listado completo en tiempo real a través de este link.</a><br><br>Listado de prioridades actual (" + Convert.ToString(DateTime.Now)+"):<hr />" + sw.ToString(); ;
                        mm.IsBodyHtml = true;
                        mm.Priority = MailPriority.High;
                   
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.thermolympic.es";
                        smtp.Port = 25;
                        smtp.EnableSsl = false;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential("bms@thermolympic.es", "010477Bms");
                        smtp.Send(mm);
                    }
                }
            }

            catch (Exception) 
            { 
            }
        }

        protected void MandarMailinternotest(object sender, EventArgs e)
        {
            try
            {
                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                    {
                        GridView2.RenderControl(hw);
                        StringReader sr = new StringReader(sw.ToString());
                        //MailMessage mm = new MailMessage("bms@thermolympic.es", "pedro@thermolympic.es");
                        MailMessage mm = new MailMessage();
                        Conexion_PLANIFICACION conexion = new Conexion_PLANIFICACION();
                        DataSet ds_correos = conexion.leer_correosPrioridades();
                        mm.To.Add(new MailAddress("pedro@thermolympic.es"));
                        mm.From = new MailAddress("bms@thermolympic.es");
                        //mm.To.Add(new MailAddress("pedro@thermolympic.es"));
                        mm.Subject = "PRODUCCIÓN - Nuevas prioridades disponibles para su consulta.";
                        mm.Body = "Se han realizado modificaciones en el listado de prioridades.<br><a href='http://facts4-srv/thermogestion/PLANIFICACION/ListadoPrioridades.aspx'>Accede a las prioridades en tiempo real a través de este link.</a><br><br>Listado de prioridades actual (" + Convert.ToString(DateTime.Now) + "):<hr />" + sw.ToString(); ;
                        mm.IsBodyHtml = true;
                        mm.Priority = MailPriority.High;

                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.thermolympic.es";
                        smtp.Port = 25;
                        smtp.EnableSsl = false;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential("bms@thermolympic.es", "010477Bms");
                        smtp.Send(mm);
                    }
                }
            }

            catch (Exception)
            {
            }
        }
        public override void VerifyRenderingInServerForm(Control control) { }

    }

}