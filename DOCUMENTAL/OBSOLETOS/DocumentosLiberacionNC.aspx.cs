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

namespace ThermoWeb.DOCUMENTAL
{
    public partial class DocumentosLiberacionNC : System.Web.UI.Page
    {

        private static DataSet ds_DocumentosGP12 = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            {

            }
            if (!IsPostBack)
            {
                if (Request.QueryString["REFERENCIA"] != null)
                {
                    cargarframes();                 

                }

            }
        }

        public void cargarframes()
        {
            try
            {
                Conexion_DOCUMENTAL conexion = new Conexion_DOCUMENTAL();
                ds_DocumentosGP12 = conexion.devuelve_dataset_filtroreferencias(Request.QueryString["REFERENCIA"]);
                
                NOCONFORMIDAD.Attributes.Add("src", ds_DocumentosGP12.Tables[0].Rows[0]["NoConformidades"].ToString() + "#zoom=125");         
            }
            catch (Exception ex)
            { }
        }
                
    }

}