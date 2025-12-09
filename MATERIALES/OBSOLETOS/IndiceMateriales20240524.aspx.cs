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

namespace ThermoWeb.MATERIALES
{
    public partial class IndiceMateriales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            Conexion_MATERIALES conexion = new Conexion_MATERIALES();
            DataSet ds_Materiales = conexion.devuelve_lista_materiales("(I.[No_] LIKE '2%' or I.[No_] LIKE '3%' or I.[No_] LIKE '1%')");
            for (int i = 0; i <= ds_Materiales.Tables[0].Rows.Count - 1; i++)
            {
                DatalistNUMMaterial.InnerHtml = DatalistNUMMaterial.InnerHtml + System.Environment.NewLine +
                    String.Format("<option value='{0}'>", ds_Materiales.Tables[0].Rows[i][0]);
            }
            */
        }

        public void BuscarMaterial(object sender, EventArgs e)
        {
            if (NUMMaterial.Value != "")
            {
                //Condicion inicial
                LblProximaEntrada.Text = "No hay entradas previstas.";
                lblCantidadDisponible.Text = "0";

                //Parcheo lectura
                Conexion_MATERIALES conexion = new Conexion_MATERIALES();
                string[] RecorteMAT = NUMMaterial.Value.Split(new char[] { '¬' });
            
                string ARTICULO = RecorteMAT[0].ToString().Trim();
                string DESCRIPCION = "";
                try
                {
                    DESCRIPCION = RecorteMAT[1].ToString().Trim();
                }
                catch (Exception ex)
                {
                    
                }
           
                DataTable MatXUBI = conexion.Devuelve_Lista_MaterialesXUbicacion(ARTICULO);
                DataTable StocksDisponibles = conexion.Devuelve_Datos_Material_NAV(ARTICULO);
                GridUbicacion.DataSource = MatXUBI;
                GridUbicacion.DataBind();

                CardUltimasCon.Visible = false;
                if (MatXUBI.Rows.Count == 0)
                {
                    CardUltimasCon.Visible = true;
                    DataTable MatXUBIUltimas = conexion.Devuelve_Lista_MaterialesXUbicacionUltimas(ARTICULO);
                    GridUltimaUbicacion.DataSource = MatXUBIUltimas;
                    GridUltimaUbicacion.DataBind();
                }

                if (StocksDisponibles.Rows.Count > 0)
                {
                        if (StocksDisponibles.Rows[0]["FECHA"].ToString() != "")
                        {
                            LblProximaEntrada.Text = StocksDisponibles.Rows[0]["FECHA"].ToString();
                        }
                   lblCantidadDisponible.Text = StocksDisponibles.Rows[0]["CANTALM"].ToString();
                }
           
                try
                {
                    lblProducto.Text = MatXUBI.Rows[0]["Articulo"].ToString() + " " + MatXUBI.Rows[0]["Descripcion"].ToString();
                }
                catch (Exception ex)
                {
                    lblProducto.Text = RecorteMAT[0].ToString().Trim() + " " + DESCRIPCION; 
                }
                NUMMaterial.Value = "";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "ShowPopDocVinculados();", true);
            }
        }

        public void GridCommandEventHandler(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Response.Redirect("UbicacionMateriasPrimas.aspx?UBI=" + e.CommandArgument);
                e.CommandArgument.ToString();

            }
            catch (Exception ex)
            {

            }
        }

        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                {
                    Label lblTextoAuxiliar = (Label)e.Row.FindControl("lblFechaEntrada");
                    lblTextoAuxiliar.Text = "<br /><strong>¡Consumir primero!</strong>";
                    lblTextoAuxiliar.ForeColor = System.Drawing.Color.Red;
                }


            }
        }

    }
}