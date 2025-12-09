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


namespace ThermoWeb.KPI
{
    public partial class EditorKPI : System.Web.UI.Page
    {
       
        private static DataTable DT_EditorKPI = new DataTable();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RellenarGrid(null, null);
            }

        }

        public void RellenarGrid(object sender, EventArgs e)
        {
            try
            {
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                DT_EditorKPI = SHConexion.Devuelve_Objetivos_KPI_Editor(Drop_KPISelect.SelectedValue);
                dgv_AudiovisualKPI.DataSource = DT_EditorKPI;
                dgv_AudiovisualKPI.DataBind();
            }
            catch (Exception)
            {

            }
        }

        public void GridView_RowUpdating(Object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Label txtAño = (Label)dgv_AudiovisualKPI.Rows[e.RowIndex].FindControl("txtAño");
                TextBox txtSimbolo = (TextBox)dgv_AudiovisualKPI.Rows[e.RowIndex].FindControl("txtSimbolo");
                TextBox txtKPI = (TextBox)dgv_AudiovisualKPI.Rows[e.RowIndex].FindControl("txtKPIValor");
                string KPI = Drop_KPISelect.SelectedValue;
                string año = txtAño.Text;
                string simbolo = txtSimbolo.Text;
                string valorkpi = txtKPI.Text;
                string a = "";
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                SHConexion.Actualiza_Objetivos_KPI_Editor(Drop_KPISelect.SelectedValue, Convert.ToInt32(txtAño.Text), txtSimbolo.Text, txtKPI.Text.Replace(",", "."));
               
                dgv_AudiovisualKPI.EditIndex = -1;
                RellenarGrid(null, null);
                /*
                Label referencia = (Label)dgv_EstadoReferencias.Rows[e.RowIndex].FindControl("txtReferencia");
                DropDownList estadoactual = (DropDownList)dgv_EstadoReferencias.Rows[e.RowIndex].FindControl("txtEstadoActual");
                DropDownList responsable = (DropDownList)dgv_EstadoReferencias.Rows[e.RowIndex].FindControl("txtResponsable");
                //Label fechrevision = (Label)dgv_EstadoReferencias.Rows[e.RowIndex].FindControl("txtFecharev");
                TextBox fechprevsal = (TextBox)dgv_EstadoReferencias.Rows[e.RowIndex].FindControl("txtFechaprevsalida");
                Label estadoanterior = (Label)dgv_EstadoReferencias.Rows[e.RowIndex].FindControl("txtEstadoAnterior");
                Label Fechaestanterior = (Label)dgv_EstadoReferencias.Rows[e.RowIndex].FindControl("txtFechaestanterior");
                TextBox Observaciones = (TextBox)dgv_EstadoReferencias.Rows[e.RowIndex].FindControl("txtObservaciones");

                Conexion_GP12 conexion = new Conexion_GP12();
                //DESCOMENTAR AL TERMINAR
                conexion.actualizar_estado(Convert.ToInt32(referencia.Text),
                                            conexion.devuelve_IDlista_razonesrevision(Convert.ToString(estadoactual.SelectedValue)),
                                            conexion.Devuelve_IDlista_responsablesSMARTH(Convert.ToString(responsable.SelectedValue)),
                                            DateTime.Now.ToString("dd/MM/yyyy"),
                                            fechprevsal.Text,
                                            estadoanterior.Text,
                                            Fechaestanterior.Text,
                                            Observaciones.Text);
                conexion.actualizar_productosBMS(Convert.ToInt32(referencia.Text), conexion.devuelve_IDlista_razonesrevision(Convert.ToString(estadoactual.SelectedValue)));
                dgv_EstadoReferencias.EditIndex = -1;
                Rellenar_grid(null, null);
                */
                //ds_Referencias = conexion.leer_referenciaestados();
                //dgv_EstadoReferencias.DataSource = ds_Referencias;
                //dgv_EstadoReferencias.DataBind();
                //cargar_Filtrados(null, e);
            }
            catch (Exception ex)
            {

            }
        }

        // cancela la modificación de una fila
        protected void gridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            dgv_AudiovisualKPI.EditIndex = -1;
            dgv_AudiovisualKPI.DataSource = DT_EditorKPI;
            dgv_AudiovisualKPI.DataBind();

        }

        // modifica una fila
        public void GridView_RowEditing(Object sender, GridViewEditEventArgs e)
        {

            dgv_AudiovisualKPI.EditIndex = e.NewEditIndex;
            dgv_AudiovisualKPI.DataSource = DT_EditorKPI;
            dgv_AudiovisualKPI.DataBind();

        }





        public void ActualizaDocumento(object sender, EventArgs e)
        {
            Conexion_SMARTH SHConexion = new Conexion_SMARTH();
            SHConexion.Actualiza_Auxiliares_AudiovisualAPP(AUXRecursoID.Value, DropEstadoDocumento.SelectedValue, InputDescripcion.Value);


            // Conexion_DOCUMENTAL conexion = new Conexion_DOCUMENTAL();           
            RellenarGrid(null,null);
        }

        public void VerDocumento(object sender, EventArgs e)
        {
            Response.Redirect(AUXRecursoURL.Value);

        }

        public void EliminaDocumento(object sender, EventArgs e)
        {
            if (AUXRecursoTipo.Value == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "AlertaFormato('Este documento está protegido y no se puede eliminar.');", true);
            }     
            else
            { 
            Conexion_SMARTH SHConexion = new Conexion_SMARTH();
            SHConexion.Elimina_Auxiliares_AudiovisualAPP(AUXRecursoID.Value);
           
           
            // Conexion_DOCUMENTAL conexion = new Conexion_DOCUMENTAL();           
            RellenarGrid(null,null);
            }

        }

        public void GridViewCommandEventHandler(Object sender, GridViewCommandEventArgs e)
        {
            try
            {
                
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                if (e.CommandName == "ActualizaDOC")
                {
                    string[] RecorteComand = e.CommandArgument.ToString().Split(new char[] { '¬' });
                    string Id = RecorteComand[0].Trim();
                    string Estado = RecorteComand[1].Trim();


                }
                if (e.CommandName == "NuevoRecurso")
                {
                    //Limpia los códigos                  

                    InputNuevaAPP.Value = e.CommandArgument.ToString();
                    InputNuevaDescripcion.Value = "";
                    DropDownNuevoEstado.SelectedValue = "0";
                       
                    //InputAPP.Value = e.CommandArgument.ToString();
                    //InputDescripcion.Value = Recurso.Rows[0]["Descripcion"].ToString();
                    //DropEstadoDocumento.SelectedValue = Recurso.Rows[0]["Disponible"].ToString();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupNuevo();", true);
                }                
                if (e.CommandName == "EditarRecurso")
                {

                    AUXRecursoID.Value = "";
                    AUXRecursoURL.Value = "";
                    AUXRecursoTipo.Value = "";
                    InputDescripcion.Value = "";
                    InputAPP.Value = "";
                    DropEstadoDocumento.SelectedValue = "0";

                    DataTable Recurso = SHConexion.Devuelve_Auxiliares_AudiovisualAPPxID(e.CommandArgument.ToString());
                    AUXRecursoID.Value = Recurso.Rows[0]["Id"].ToString();
                    AUXRecursoTipo.Value = Recurso.Rows[0]["Tipo"].ToString();
                    AUXRecursoURL.Value = Recurso.Rows[0]["URL"].ToString();
                    InputAPP.Value = Recurso.Rows[0]["APP"].ToString();
                    InputDescripcion.Value = Recurso.Rows[0]["Descripcion"].ToString();
                    DropEstadoDocumento.SelectedValue = Recurso.Rows[0]["Disponible"].ToString();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupEdicion();", true);
                }



                RellenarGrid(null,null);

            }
            catch (Exception ex)
            {
            }

        }

        //Subida de documentos
        public void Insertar_documento(Object sender, EventArgs e)
        {
            try
            {
                if (UploadRecurso.HasFile && InputNuevaAPP.Value == "KPIPLANTA")
                {
                    string extension = System.IO.Path.GetExtension(UploadRecurso.PostedFile.FileName);
                    //Checkea extensión
                    //mpeg or mp4
                    if (extension == ".mpeg" || extension == ".mp4")
                    {
                        SaveFile(UploadRecurso.PostedFile);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "AlertaFormato('¡Formato no compatible! Debes usar archivos .mp4 o .mpeg.');", true);
                    }
                }
                if (UploadRecurso.HasFile && InputNuevaAPP.Value == "VISITAS")
                {
                    string extension = System.IO.Path.GetExtension(UploadRecurso.PostedFile.FileName);
                    if (extension == ".bmp" || extension == ".jpg" || extension == ".png")
                    {                        
                        //Checkea extensión
                        SaveFile(UploadRecurso.PostedFile);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "AlertaFormato('¡Formato no compatible! Debes usar archivos .jpg, .png, .bmp.');", true);

                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        private void SaveFile(HttpPostedFile file)
        {
            try
            {
                // Specify the path to save the uploaded file to.


                string savePath = @"C:\inetpub_thermoweb\SMARTH_docs\AUDIOVISUAL\"; // Your code goes here
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }

                // Get the name of the file to upload.
                //string fileName = FileUpload1.FileName;
                string fileName = "";
                string extension = "";               
                extension = System.IO.Path.GetExtension(UploadRecurso.PostedFile.FileName);
                fileName = InputNuevaAPP.Value + "_" + DateTime.Now.ToString("yyyyy-MM-dd_HHmmss") + extension;
                savePath += fileName;

                // Call the SaveAs method to save the uploaded
                // file to the specified directory.
                string savePathVirtual = "";
                UploadRecurso.SaveAs(savePath);
                savePathVirtual = "..\\SMARTH_docs\\AUDIOVISUAL\\";
                savePathVirtual += fileName;
                InsertarDocumentosBD(savePathVirtual);             
                
            }
            catch (Exception ex)
            {
              
            }

        }

        private void InsertarDocumentosBD(string savepath)
        {
            try
            {
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                SHConexion.Inserta_Auxiliares_AudiovisualAPP(savepath, InputNuevaAPP.Value, DropDownNuevoEstado.SelectedValue, InputNuevaDescripcion.Value);


                RellenarGrid(null,null);
            }
            catch (Exception ex)
            {
               
            }
        }


    }

}