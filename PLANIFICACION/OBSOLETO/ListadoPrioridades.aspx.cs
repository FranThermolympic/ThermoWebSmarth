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
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using System.Net.Mail;
using System.Net;
using DocumentFormat.OpenXml.Packaging;


namespace ThermoWeb.PLANIFICACION
{
    public partial class ListadoPrioridades : System.Web.UI.Page
    {

        private static DataSet ds_Referencias = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Conexion_PLANIFICACION conexion = new Conexion_PLANIFICACION();
                ds_Referencias = conexion.cargar_acciones_abiertas();
                //CargaListasFiltro();
                Rellenar_grid();
            }

        }

        private void Rellenar_grid()
        {
            try
            {
                dgv_AccionesAbiertas.DataSource = ds_Referencias;
                dgv_AccionesAbiertas.DataBind(); 
            }
            catch (Exception)
            { 
                
            }
        }
       
        // guarda una fila
        public void GridView_RowUpdating(Object sender, GridViewUpdateEventArgs e)
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
                Cargar_todas(null, e);
            }
            catch (Exception)
            {
               
            }              
        }

        // cancela la modificación de una fila
        protected void GridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            dgv_AccionesAbiertas.EditIndex = -1;
            dgv_AccionesAbiertas.DataSource = ds_Referencias;
            dgv_AccionesAbiertas.DataBind();
        }

        // modifica una fila
        public void GridView_RowEditing(Object sender, GridViewEditEventArgs e)
            {
            try
            {
                dgv_AccionesAbiertas.EditIndex = e.NewEditIndex;
                dgv_AccionesAbiertas.DataSource = ds_Referencias;
                dgv_AccionesAbiertas.DataBind();
            }
            catch (Exception)
            {
            }

            }
        // carga la lista utilizando un filtro

        public void BorrarPrioridades(Object sender, EventArgs e)
            {
            try 
            {
                Conexion_PLANIFICACION conexion = new Conexion_PLANIFICACION();
                conexion.Borrar_Prioridades_OrdenesBMS();
                ds_Referencias = conexion.cargar_acciones_abiertas();
                Rellenar_grid();
            }
            catch (Exception)
            { }
            }
        protected void Cargar_todas(object sender, EventArgs e)
        {

            Conexion_PLANIFICACION conexion = new Conexion_PLANIFICACION();
            ds_Referencias = conexion.cargar_acciones_abiertas();
            dgv_AccionesAbiertas.DataSource = ds_Referencias;
            dgv_AccionesAbiertas.DataBind();
            //VerTodas.Visible = false;
            //VerRevision.Visible = true;
        }
        protected void Cargar_EnRevision(object sender, EventArgs e)
        {

            Conexion_PLANIFICACION conexion = new Conexion_PLANIFICACION();
            ds_Referencias = conexion.leer_ReferenciaEstadosActivas();
            dgv_AccionesAbiertas.DataSource = ds_Referencias;
            dgv_AccionesAbiertas.DataBind();
            VerTodas.Visible = true;
            VerRevision.Visible = false;
        }

        protected void Cargar_Filtrados(object sender, EventArgs e)
        {

            string estado = Convert.ToString(lista_estado.SelectedValue);
            string cliente = Convert.ToString(lista_clientes.SelectedValue);
            if (cliente == "-") {cliente = "";}
            string responsable = Convert.ToString(lista_responsable.SelectedValue);
            if (responsable == "-") { responsable = ""; }
            Conexion_PLANIFICACION conexion = new Conexion_PLANIFICACION();
            ds_Referencias = conexion.leer_ReferenciaEstadosFiltros(estado, cliente, responsable, Convert.ToString(selectReferencia.Text), Convert.ToString(selectMolde.Text));
            dgv_AccionesAbiertas.DataSource = ds_Referencias;
            dgv_AccionesAbiertas.DataBind();
            VerTodas.Visible = true;
            VerRevision.Visible = false;
        }

        protected void Cargar_Ordenados(object sender, EventArgs e)
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
                    dgv_AccionesAbiertas.DataSource = ds_Referencias;
                    dgv_AccionesAbiertas.DataBind();
                    break;

                case "OrdenarPriorASC":
                    orderby = " ORDER BY Prioridad ASC, j1.C_SEQNR ASC";
                    ds_Referencias = conexion.cargar_acciones_abiertas_ordenadas(orderby, Convert.ToInt32(TipoAlerta.SelectedValue));
                    dgv_AccionesAbiertas.DataSource = ds_Referencias;
                    dgv_AccionesAbiertas.DataBind();
                    OrdenarPriorASC.Visible = false;
                    OrdenarPriorDESC.Visible = true;
                    break;

                case "OrdenarPriorDESC":
                    orderby = " ORDER BY Prioridad DESC, j1.C_SEQNR ASC";
                    ds_Referencias = conexion.cargar_acciones_abiertas_ordenadas(orderby, Convert.ToInt32(TipoAlerta.SelectedValue));
                    dgv_AccionesAbiertas.DataSource = ds_Referencias;
                    dgv_AccionesAbiertas.DataBind();
                    OrdenarPriorASC.Visible = true;
                    OrdenarPriorDESC.Visible = false;
                    break;

                case "OrdenarMaqASC":
                    orderby = " ORDER BY Maquina ASC, j1.C_SEQNR ASC";
                    ds_Referencias = conexion.cargar_acciones_abiertas_ordenadas(orderby, Convert.ToInt32(TipoAlerta.SelectedValue));
                    dgv_AccionesAbiertas.DataSource = ds_Referencias;
                    dgv_AccionesAbiertas.DataBind();
                    OrdenarMaqASC.Visible = false;
                    OrdenarMaqDESC.Visible = true;
                    break;

                case "OrdenarMaqDESC":
                    orderby = " ORDER BY Maquina DESC, j1.C_SEQNR ASC";
                    ds_Referencias = conexion.cargar_acciones_abiertas_ordenadas(orderby, Convert.ToInt32(TipoAlerta.SelectedValue));
                    dgv_AccionesAbiertas.DataSource = ds_Referencias;
                    dgv_AccionesAbiertas.DataBind();
                    OrdenarMaqASC.Visible = true;
                    OrdenarMaqDESC.Visible = false;
                    break;

                case "OrdenarOrdenASC":
                    orderby = " ORDER BY Orden ASC, j1.C_SEQNR ASC";
                    ds_Referencias = conexion.cargar_acciones_abiertas_ordenadas(orderby, Convert.ToInt32(TipoAlerta.SelectedValue));
                    dgv_AccionesAbiertas.DataSource = ds_Referencias;
                    dgv_AccionesAbiertas.DataBind();
                    OrdenarOrdenASC.Visible = false;
                    OrdenarOrdenDESC.Visible = true;
                    break;

                case "OrdenarOrdenDESC":
                    orderby = " ORDER BY Orden DESC, j1.C_SEQNR ASC";
                    ds_Referencias = conexion.cargar_acciones_abiertas_ordenadas(orderby, Convert.ToInt32(TipoAlerta.SelectedValue));
                    dgv_AccionesAbiertas.DataSource = ds_Referencias;
                    dgv_AccionesAbiertas.DataBind();
                    OrdenarOrdenASC.Visible = true;
                    OrdenarOrdenDESC.Visible = false;
                    break;

                case "OrdenarReferenciaASC":
                    orderby = " ORDER BY REF ASC, j1.C_SEQNR ASC";
                    ds_Referencias = conexion.cargar_acciones_abiertas_ordenadas(orderby, Convert.ToInt32(TipoAlerta.SelectedValue));
                    dgv_AccionesAbiertas.DataSource = ds_Referencias;
                    dgv_AccionesAbiertas.DataBind();
                    OrdenarReferenciaASC.Visible = false;
                    OrdenarReferenciaDESC.Visible = true;
                    break;

                case "OrdenarReferenciaDESC":
                    orderby = " ORDER BY REF DESC, j1.C_SEQNR ASC";
                    ds_Referencias = conexion.cargar_acciones_abiertas_ordenadas(orderby, Convert.ToInt32(TipoAlerta.SelectedValue));
                    dgv_AccionesAbiertas.DataSource = ds_Referencias;
                    dgv_AccionesAbiertas.DataBind();
                    OrdenarReferenciaASC.Visible = true;
                    OrdenarReferenciaDESC.Visible = false;
                    break;
            }
        }

        public void ContactsGridView_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "RedirectMOL")
            {
                string URL = "http://facts4-srv/thermogestion/MANTENIMIENTO/ReparacionMoldes.aspx?PARTE=" + e.CommandArgument.ToString();
                Response.Write("<script language='javascript'> window.open('" + URL + "', 'window','HEIGHT=600,WIDTH=820,top=50,left=50,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
                dgv_AccionesAbiertas.EditIndex = -1;
                Cargar_todas(null, e);
            }
            if (e.CommandName == "RedirectMAQ")
            {
                string URL= "http://facts4-srv/oftecnica/ReparacionMaquinas.aspx?PARTE=" + e.CommandArgument.ToString();
                Response.Write("<script language='javascript'> window.open('" + URL + "', 'window','HEIGHT=600,WIDTH=820,top=50,left=50,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
                dgv_AccionesAbiertas.EditIndex = -1;
                Cargar_todas(null, e);
            }
            if (e.CommandName == "OKMOL")
            {
                string parte = e.CommandArgument.ToString();
                if (parte != "" && Convert.ToInt32(parte) != 0)
                {
                    Conexion_PLANIFICACION conexion = new Conexion_PLANIFICACION();
                    conexion.Actualizar_Parte_Molde(parte, 1, 1, 0);
                    int molde = conexion.Devuelve_Molde_X_Parte(Convert.ToInt32(parte));
                    conexion.eliminar_accion_pendienteBMSMOLDE(molde);
                    dgv_AccionesAbiertas.EditIndex = -1;
                    Cargar_todas(null, e);
                }
            }
            if (e.CommandName == "NOKMOL")
            {
                string parte = e.CommandArgument.ToString();
                if (parte != "" && Convert.ToInt32(parte) != 0)
                {
                    Conexion_PLANIFICACION conexion = new Conexion_PLANIFICACION();
                    conexion.Actualizar_Parte_Molde(parte, 0, 0, 0);
                    dgv_AccionesAbiertas.EditIndex = -1;
                    Cargar_todas(null, e);
                    //envía aviso reparacion abierta 
                }

            }
            if (e.CommandName == "OKMAQ")
            {
                string parte = e.CommandArgument.ToString();
                if (parte != "" && Convert.ToInt32(parte) != 0)
                {
                    Conexion_PLANIFICACION conexion = new Conexion_PLANIFICACION();
                    conexion.Actualizar_Parte_Maquina(parte, 1, 1, 0);
                    string maquina = conexion.Devuelve_Maquina_X_Parte(Convert.ToInt32(parte));
                    conexion.eliminar_accion_pendienteBMSMAQUINA(maquina);
                    dgv_AccionesAbiertas.EditIndex = -1;
                    Cargar_todas(null, e);

                }

            }
            if (e.CommandName == "NOKMAQ")
            {
                string parte = e.CommandArgument.ToString();
                if (parte != "" && Convert.ToInt32(parte) != 0)
                {
                    Conexion_PLANIFICACION conexion = new Conexion_PLANIFICACION();
                    conexion.Actualizar_Parte_Maquina(parte, 0, 0, 0);
                    dgv_AccionesAbiertas.EditIndex = -1;
                    Cargar_todas(null, e);
                    //envía aviso reparacion abierta 
                }
            }

        }

        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                if (e.Row.RowState.HasFlag(DataControlRowState.Edit))

                {
                    DropDownList Prioridad = (DropDownList)e.Row.FindControl("Selecprioridad");
                    //Conexion_PLANIFICACION conexion = new Conexion_PLANIFICACION();
                    //DataTable dt = conexion.leer_tablalistaestados();
                    //Prioridad.DataSource = dt;
                    //Prioridad.DataTextField = "Razon";
                    //Prioridad.DataValueField = "Razon";
                    //Prioridad.DataBind();
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    Prioridad.SelectedValue = dr["Prioridad"].ToString();

                    DropDownList Responsable = (DropDownList)e.Row.FindControl("SelecResponsable");
                    Conexion_PLANIFICACION conexion = new Conexion_PLANIFICACION();
                    DataSet Operarios = conexion.Devuelve_mandos_intermedios();

                    Operarios.Tables[0].DefaultView.RowFilter = "Departamento = 'PRODUCCION' OR Departamento = '-' or Departamento = 'INGENIERIA'";
                    DataTable DTPRODUCCION = (Operarios.Tables[0].DefaultView).ToTable();
                    foreach (DataRow row in DTPRODUCCION.Rows) { Responsable.Items.Add(row["PListado"].ToString()); }
                    Responsable.ClearSelection();
                    Responsable.SelectedValue = "-";


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

        public void Mandar_mailtesting(string mensaje, string subject)
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

        /*public void imprimir_ficha(Object sender, EventArgs e)
        {
            try
            {
                //specify the file name where its actually exist   
                //string filepath = @"\\FACTS4-SRV\Fichas parametros\FichaReferencia_vacia.xlsx";  

                ////open the excel using openxml sdk  
                //using(SpreadsheetDocument doc = SpreadsheetDocument.Open(filepath, false))  
                //{    
                //    //create the object for workbook part  
                //    WorkbookPart wbPart = doc.WorkbookPart;  

                //    //statement to get the count of the worksheet  
                //    int worksheetcount = doc.WorkbookPart.Workbook.Sheets.Count();  

                //    //statement to get the sheet object  
                //    Sheet mysheet = (Sheet) doc.WorkbookPart.Workbook.Sheets.ChildElements.GetItem(0);  

                //    //statement to get the worksheet object by using the sheet id  
                //    DocumentFormat.OpenXml.Spreadsheet.Worksheet Worksheet = ((WorksheetPart) wbPart.GetPartById(mysheet.Id)).Worksheet;  

                //    //Note: worksheet has 8 children and the first child[1] = sheetviewdimension,....child[4]=sheetdata  
                //    int wkschildno = 4;  


                //    //statement to get the sheetdata which contains the rows and cell in table  
                //SheetData Rows = (SheetData)Worksheet.ChildElements.GetItem(wkschildno);


                //    //getting the row as per the specified index of getitem method  
                //    Row currentrow = (Row) Rows.ChildElements.GetItem(13);  

                //    //getting the cell as per the specified index of getitem method  
                //    Cell currentcell = (Cell) currentrow.ChildElements.GetItem(1);
                //    currentcell.CellValue = new CellValue("HOLA");
                //    currentrow.Append(currentcell);
                //    Rows.Append(currentrow);
                //    //statement to take the integer value  
                //    string currentcellvalue = currentcell.InnerText;







                //using (SpreadsheetDocument xl = SpreadsheetDocument.Open(filepath, true))
                //{
                //    WorkbookPart wbp = xl.WorkbookPart;

                //    Sheet mysheet = (Sheet)xl.WorkbookPart.Workbook.Sheets.ChildElements.GetItem(0);

                //    //statement to get the worksheet object by using the sheet id  
                //    DocumentFormat.OpenXml.Spreadsheet.Worksheet Worksheet = ((WorksheetPart)wbp.GetPartById(mysheet.Id)).Worksheet;
                //    DocumentFormat.OpenXml.Spreadsheet.Workbook wb = new DocumentFormat.OpenXml.Spreadsheet.Workbook();
                //    FileVersion fv = new FileVersion();
                //    fv.ApplicationName = "Microsoft Office Excel";


                //   //Note: worksheet has 8 children and the first child[1] = sheetviewdimension,....child[4]=sheetdata  
                //    int wkschildno = 4;  


                //    //statement to get the sheetdata which contains the rows and cell in table  
                //    SheetData Rows = (SheetData)Worksheet.ChildElements.GetItem(wkschildno);
                //    Row currentrow = (Row)Rows.ChildElements.GetItem(13);


                //    Cell currentcell = (Cell) currentrow.ChildElements.GetItem(1);
                //    currentcell.CellValue = new CellValue("HOLA");



                //    ////third cell
                //    //Row r2 = new Row() { RowIndex = (UInt32Value)2u };
                //    //Cell c3 = new Cell();
                //    //c3.DataType = CellValues.String;
                //    //c3.CellValue = new CellValue("some string");
                //    //r2.Append(c3);
                //    //Rows.Append(r2);

                //    //Worksheet.Append(Rows);

                //    DocumentFormat.OpenXml.Spreadsheet.Sheets sheets = new DocumentFormat.OpenXml.Spreadsheet.Sheets();
                //    Sheet sheet = new Sheet();
                //    sheet.Name = "first sheet";
                //    sheets.Append(sheet);
                //    wb.Append(fv);
                //    wb.Append(sheets);

                //    xl.WorkbookPart.Workbook = wb;
                //    xl.WorkbookPart.Workbook.Save();
                //    xl.Close();



                //}

                string filepath = @"\\FACTS4-SRV\Fichas parametros\PRIORIDADES.xlsx";
                FileInfo fileInfo = new FileInfo(filepath);
                ExcelPackage p = new ExcelPackage(fileInfo);
                ExcelWorksheet xlWorkSheet = p.Workbook.Worksheets["FICHA"];

                // información principal
                
                p.Save();

                // Limpiamos la salida
                Response.Clear();
                // Con esto le decimos al browser que la salida sera descargable
                Response.ContentType = "application/octet-stream";
                // esta linea es opcional, en donde podemos cambiar el nombre del fichero a descargar (para que sea diferente al original)
                Response.AddHeader("Content-Disposition", "attachment; filename=Ficha_Parametros_" + Convert.ToString(DateTime.Now) + ".xlsx");
                // Escribimos el fichero a enviar 
                Response.WriteFile(@"\\FACTS4-SRV\Fichas parametros\FichaReferencia_vacia.xlsx");
                // volcamos el stream 
                Response.Flush();
                // Enviamos todo el encabezado ahora
                Response.End();
            }
            catch (Exception)
            {
            }
        }*/

        protected void MandarMail(object sender, EventArgs e)
        {
            try
            {
                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                    {
                        dgv_AccionesAbiertas.RenderControl(hw);
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
                        mm.Body = "Se han realizado modificaciones en el listado de prioridades.<br><a href='http://facts4-srv/thermogestion/PLANIFICACION/ListadoPrioridades.aspx'>Accede a las prioridades en tiempo real a través de este link.</a><br><br>Listado de prioridades actual (" + Convert.ToString(DateTime.Now)+"):<hr />" + sw.ToString(); ;
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