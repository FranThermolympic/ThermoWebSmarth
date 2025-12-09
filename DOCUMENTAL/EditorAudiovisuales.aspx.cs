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


namespace ThermoWeb.DOCUMENTAL
{
    public partial class EditorAudiovisuales : System.Web.UI.Page
    {

       

        private static DataTable DT_AudivisualKPI = new DataTable();
        private static DataTable DT_AudivisualVISITAS = new DataTable();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();

                DT_AudivisualKPI = SHConexion.Devuelve_Auxiliares_AudiovisualAPP("KPIPLANTA", "");
                DT_AudivisualVISITAS = SHConexion.Devuelve_Auxiliares_AudiovisualAPP("VISITAS", "");
                // Conexion_DOCUMENTAL conexion = new Conexion_DOCUMENTAL();           
                Rellenar_grid();
            }

        }

        private void Rellenar_grid()
        {
            try
            {
                dgv_AudiovisualKPI.DataSource = DT_AudivisualKPI;
                dgv_AudiovisualKPI.DataBind();

                dgv_AudiovisualVISITAS.DataSource = DT_AudivisualVISITAS;
                dgv_AudiovisualVISITAS.DataBind();
            }
            catch (Exception)
            {

            }
        }

        public void ActualizaDocumento(object sender, EventArgs e)
        {
            Conexion_SMARTH SHConexion = new Conexion_SMARTH();
            SHConexion.Actualiza_Auxiliares_AudiovisualAPP(AUXRecursoID.Value, DropEstadoDocumento.SelectedValue, InputDescripcion.Value);
            DT_AudivisualKPI = SHConexion.Devuelve_Auxiliares_AudiovisualAPP("KPIPLANTA", "");
            DT_AudivisualVISITAS = SHConexion.Devuelve_Auxiliares_AudiovisualAPP("VISITAS", "");
            // Conexion_DOCUMENTAL conexion = new Conexion_DOCUMENTAL();           
            Rellenar_grid();
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
            DT_AudivisualKPI = SHConexion.Devuelve_Auxiliares_AudiovisualAPP("KPIPLANTA", "");
            DT_AudivisualVISITAS = SHConexion.Devuelve_Auxiliares_AudiovisualAPP("VISITAS", "");
            // Conexion_DOCUMENTAL conexion = new Conexion_DOCUMENTAL();           
            Rellenar_grid();
            }

        }

        public void OnRowDataBoud(object sender, GridViewRowEventArgs e)
        {
            if (!e.Row.RowState.HasFlag(DataControlRowState.Edit))
            {
                try
            {
                
                    Label DISPONIBILIDAD = (Label)e.Row.FindControl("lblEstado");

                    if (DISPONIBILIDAD.Text == "0")
                    {
                        DISPONIBILIDAD.Text = "Oculto";
                        DISPONIBILIDAD.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        DISPONIBILIDAD.Text = "Disponible";
                    }
                }
            catch (Exception ex)
            {
            }
        }
        }

        public void GridViewCommandEventHandler(Object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Conexion_DOCUMENTAL conexion = new Conexion_DOCUMENTAL();
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                if (e.CommandName == "ActualizaDOC")
                {
                    string[] RecorteComand = e.CommandArgument.ToString().Split(new char[] { '¬' });
                    string Id = RecorteComand[0].Trim();
                    string Estado = RecorteComand[1].Trim();

                    if (Estado == "False")
                    {
                        conexion.Actualizar_Operarios_Feedback(Id, "1");
                    }
                    else
                    {
                        conexion.Actualizar_Operarios_Feedback(Id, "0");
                    }
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
               
                DT_AudivisualKPI = SHConexion.Devuelve_Auxiliares_AudiovisualAPP("KPIPLANTA", "");
                DT_AudivisualVISITAS = SHConexion.Devuelve_Auxiliares_AudiovisualAPP("VISITAS", "");
                Rellenar_grid();

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
                DT_AudivisualKPI = SHConexion.Devuelve_Auxiliares_AudiovisualAPP("KPIPLANTA", "");
                DT_AudivisualVISITAS = SHConexion.Devuelve_Auxiliares_AudiovisualAPP("VISITAS", "");
                Rellenar_grid();
            }
            catch (Exception ex)
            {
               
            }
        }


    }

}