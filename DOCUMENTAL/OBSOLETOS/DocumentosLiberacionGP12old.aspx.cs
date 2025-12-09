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
    public partial class DocumentosLiberacionGP12_ : System.Web.UI.Page
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
                    DataSet imagenes = new DataSet();
                    Conexion_DOCUMENTAL conexion = new Conexion_DOCUMENTAL();
                    imagenes = conexion.DatasetDetectadosGP12(Convert.ToString(Request.QueryString["REFERENCIA"]));
                    foreach (DataRow row in imagenes.Tables[0].Rows)
                    {
                        HtmlGenericControl divItem = new HtmlGenericControl("div");
                        divItem.Attributes["class"] = "item";
                        //divItem.ID = "prueba";
                        Image img = new Image();
                        img.ImageUrl = row["ImagenDefecto1"].ToString();
                        img.Visible = true;
                        divItem.Controls.Add(img);
                        img.Width = new Unit("100%");
                        ACTIVOS.Controls.Add(divItem);
                    }

                }

            }
        }

        public void cargarframes()
        {
            try
            {
                Conexion_DOCUMENTAL conexion = new Conexion_DOCUMENTAL();
                ds_DocumentosGP12 = conexion.devuelve_dataset_filtroreferencias(Request.QueryString["REFERENCIA"]);
                DEFECTOS.Attributes.Add("src", ds_DocumentosGP12.Tables[0].Rows[0]["Defoteca"].ToString() + "#zoom=125");
            }
            catch (Exception)
            { }
        }

    }

}