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
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
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
                DataTable StocksDisponibles = conexion.Devuelve_Datos_Material_NAV(ARTICULO);
                DataTable MatXUBI = conexion.Devuelve_Lista_MaterialesXUbicacion(ARTICULO);
                if (MatXUBI != null)
                { 
                
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
                }
                if (StocksDisponibles.Rows.Count > 0)
                {
                    lblCantidadDisponible.Text = StocksDisponibles.Rows[0]["CANTALM"].ToString();
                   /*
                    Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                    DataTable StockProducto = SHConexion.Devuelve_Stock_ProductoXAlmacen_SERVNAV(ARTICULO, "", "");
                    decimal stock = 0;
                    foreach (DataRow row in StockProducto.Rows)
                    {
                        stock += Convert.ToDecimal(row["stock"]);                        
                    }
                    lblCantidadDisponible.Text = stock.ToString();
                    */
                }
           
                try
                {
                    lblProducto.Text = MatXUBI.Rows[0]["Articulo"].ToString() + " " + MatXUBI.Rows[0]["Descripcion"].ToString();
                }
                catch (Exception ex)
                {
                    lblProducto.Text = RecorteMAT[0].ToString().Trim() + " " + DESCRIPCION; 
                }
                //CARGO CAMPOS ETIQUETA

                DivContenidoEtiqueta.Visible = false;
                lblPedido.Text = "";
                lblLote.Text = "";
                lblCantidadUd.Text = "";
                lblProveedor.Text = "";

                if (hiddenCodEtiq.Value != "")
                {
                    try
                    {
                        DivContenidoEtiqueta.Visible = true;
                        string[] Codetiqueta = hiddenCodEtiq.Value.Split(new char[] { '_' }); ;
                        string codetiped = Codetiqueta[6].ToString().Remove(0, 1);
                        lblPedido.Text = codetiped.Remove(codetiped.Length - 5, 5);
                        lblLote.Text = Codetiqueta[3].ToString().Remove(0, 1) + " | " + Codetiqueta[4].ToString().Remove(0, 2);
                        lblCantidadUd.Text = Codetiqueta[2].ToString().Remove(0, 1) + " uds.";
                        string proveedor = conexion.Devuelve_Datos_Proveedor_NAV(Codetiqueta[5].ToString().Remove(0, 1));
                        if (proveedor == "")
                        {
                            lblProveedor.Text = Codetiqueta[5].ToString().Remove(0, 1);
                        }
                        else 
                        { 
                            lblProveedor.Text = proveedor;
                        }
                        
                        hiddenCodEtiq.Value = "";
                    }
                    catch (Exception ex)
                    {
                        DivContenidoEtiqueta.Visible = true;
                        lblPedido.Text = "";
                        lblLote.Text = "";
                        lblCantidadUd.Text = "";
                        lblProveedor.Text = "";
                    }
                }

                NUMMaterial.Value = "";
               
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "ShowPopDocVinculados();", true);
                
            }
        }

        public void GridCommandEventHandler(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Response.Redirect("UbicacionMateriasPrimasLite.aspx?UBI=" + e.CommandArgument);
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