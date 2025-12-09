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
    public partial class ALERTASCALIDADMINI : System.Web.UI.Page
    {
        public string DefectoCargado = "";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            Conexion_SMARTH SHConexion = new Conexion_SMARTH();
            if (!IsPostBack)
            {
               
                RestauraCampos();
                CargarDatosNoConformidad(SHConexion.Devuelve_ultimas_NC());   
            }
            if (IsPostBack)
            {
                RestauraCampos();
                CargarDatosNoConformidad(SHConexion.Devuelve_ultimas_NC());
            }
        }

        private void RestauraCampos()
        {
            try
            {
                IMGliente.ImageUrl = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";
                hyperlink2.NavigateUrl = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";
                hyperlink2.ImageUrl = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";
                hyperlink3.NavigateUrl = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";
                hyperlink3.ImageUrl = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";
                //cabecera de detalles

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
                    tbFechaOriginal.Text = "(" + Convert.ToDateTime(NC.Tables[0].Rows[0]["FechaOriginal"]).ToString("dd/MM/yyyy") + ")";
                    NivelAlerta.SelectedValue = NC.Tables[0].Rows[0]["EscaladoNoConformidad"].ToString();
                    
                    NivelAlerta.SelectedValue = NC.Tables[0].Rows[0]["EscaladoNoConformidad"].ToString();
                    
                    tbProblemaNC.Text = NC.Tables[0].Rows[0]["DescripcionProblema"].ToString().ToUpper();
                    hyperlink2.NavigateUrl = NC.Tables[0].Rows[0]["ImagenDefecto1"].ToString();
                    hyperlink2.ImageUrl = NC.Tables[0].Rows[0]["ImagenDefecto1"].ToString();
                    hyperlink3.NavigateUrl = NC.Tables[0].Rows[0]["ImagenDefecto2"].ToString();
                    hyperlink3.ImageUrl = NC.Tables[0].Rows[0]["ImagenDefecto2"].ToString();
                    DataSet ds = conexion.Devuelve_datos_producto_SMARTH(tbReferenciaCarga.Text);
                    tbDescripcionCarga.Text = ds.Tables[0].Rows[0]["Descripcion"].ToString().ToUpper();
                    IMGliente.ImageUrl = ds.Tables[0].Rows[0]["LogotipoSM"].ToString();
            }
            catch (Exception ex) 
            { 
            }

        }

        
    }

}