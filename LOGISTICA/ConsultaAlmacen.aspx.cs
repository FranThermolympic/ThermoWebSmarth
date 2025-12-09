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
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;
using System.IO;


namespace ThermoWeb.LOGISTICA
{
    public partial class ConsultaAlmacen : System.Web.UI.Page
    {

        private static DataTable ds_Referencias = new DataTable();
        private static DataTable ds_Estado_Referencias = new DataTable();
        private static DataTable ds_Muro_Calidad = new DataTable();
        private static int INTEnRevision = 0;
        private static int INTVencidas = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {             
                LeerStockAlmacen(null,null);                
            }
        }

        
        public void LeerStockAlmacen(object sender, EventArgs e)
        {
            try
            {

                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                DataTable StockAlmacen = SHConexion.Devuelve_StockAlmacen_SERVNAV(DropSelect.SelectedValue, "Y","");

                if (StockAlmacen != null)
                {
                    //DataTable AuxiliaresNAV = SHConexion.Devuelve_Lista_Materiales_LEFTJOIN();
                    var JoinResult2 = (from PSev in StockAlmacen.AsEnumerable() //Defino tabla maestra
                                                                                //join Stock in AuxiliaresNAV.AsEnumerable() on PSev.Field<string>("producto") equals Stock.Field<string>("No_") into tempJoin
                                                                                //from tempRow in tempJoin.DefaultIfEmpty() //JOIN A TABLA DE STOCKS
                                       select new
                                       {
                                           PRODUCTO = PSev.Field<string>("producto"),
                                           DESCRIPCION = PSev.Field<string>("descripcion"),
                                           ALMACEN = PSev.Field<string>("almacen"),
                                           STOCK = Convert.ToDouble(PSev.Field<string>("stock")),
                                       });


                    if (InputReferencias.Value != "")
                    {
                        string[] RecorteReferencia = InputReferencias.Value.Split(new char[] { '¬' });
                        StockAlmacen.DefaultView.RowFilter = "producto = '" + RecorteReferencia[0].ToString() + "'";
                        DataTable DTAlmacenFiltrado = StockAlmacen.DefaultView.ToTable();
                        var JoinResult = (from PSev in DTAlmacenFiltrado.AsEnumerable() //Defino tabla maestra                                                                           
                                          select new
                                          {
                                              PRODUCTO = PSev.Field<string>("producto"),
                                              DESCRIPCION = PSev.Field<string>("descripcion"),
                                              ALMACEN = PSev.Field<string>("almacen"),
                                              STOCK = Convert.ToDouble(PSev.Field<string>("stock")),
                                          });
                        dgv_Almacenes.DataSource = JoinResult;
                        dgv_Almacenes.DataBind();
                    }
                    else
                    {
                        dgv_Almacenes.DataSource = JoinResult2;
                        dgv_Almacenes.DataBind();
                    }
                }
                else
                {
                    dgv_Almacenes.DataSource = StockAlmacen;
                    dgv_Almacenes.DataBind();
                }
                //MOVIDO AL SERVICIO PARA LAS TABLETAS
                /*
                DatalistReferencias.InnerHtml = "";
                for (int i = 0; i <= StockAlmacen.Rows.Count - 1; i++)
                {
                    DatalistReferencias.InnerHtml = DatalistReferencias.InnerHtml + System.Environment.NewLine +
                        String.Format("<option value='{0}'>", StockAlmacen.Rows[i][0] + "¬" + StockAlmacen.Rows[i][1]);
                }
                */

            }
            catch (Exception ex)
            {
               // ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "AlertaFormato('" + ex.Message + "');", true);
            }

        }

        public void LeerStockProducto(string PRODUCTO)
        {
            try
            {
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                DataTable StockProducto = SHConexion.Devuelve_Stock_ProductoXAlmacen_SERVNAV(PRODUCTO, "", "");
                DataTable Aux_Almacenes_NAV = SHConexion.Devuelve_lista_almacenes_NAV();

                var JoinResult = (from p in StockProducto.AsEnumerable()
                                  join t in Aux_Almacenes_NAV.AsEnumerable()
                                  on p.Field<string>("almacen") equals t.Field<string>("Code") into tempJoin
                                  from leftJoin in tempJoin.DefaultIfEmpty()
                                  select new
                                  {
                                      producto = p.Field<string>("producto"),
                                      descripcion = p.Field<string>("descripcion"),
                                      almacenNUM = p.Field<string>("almacen"),
                                      //stock = Convert.ToDecimal(p.Field<string>("stock")),
                                      stock = Convert.ToDouble(p.Field<string>("stock")),
                                      almacen = leftJoin == null ? "-" : leftJoin.Field<string>("Name"),
                                  }).ToList();

                dgv_StockProducto.DataSource = JoinResult;
                dgv_StockProducto.DataBind();               
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "AlertaFormato('" + ex.Message + "');", true);

            }

        }

        public void MoverEntreAlmacenes(object sender, EventArgs e)
        {        
            var SNAV = new Serv_NAVISION.WSFunctionsAPP_PortClient();//MODO DEBUG
            SNAV.ClientCredentials.Windows.ClientCredential = new System.Net.NetworkCredential("Administrador", "eXtint0r");
            string response = SNAV.ChangeLocationItem(AUXReferenciaMovimiento.Value, DropAlmOrigen.SelectedValue, DropAlmFinal.SelectedValue, Convert.ToInt32(CantidadMovimiento.Value));
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "AlertaResultadoMovimiento('" + response + "');", true);
            LeerStockAlmacen(null, null);
        }

        //CARGA GRIDVIEWS
       
        // carga la lista utilizando un filtro
               
        public void GridViewCommandEventHandler(Object sender, GridViewCommandEventArgs e)
        {
            try
            {
                
                if (e.CommandName == "CambioAlmacen")
                {
                    Conexion_SMARTH SHConexion = new Conexion_SMARTH();

                    //LIMPIA CELDAS
                    CantidadMovimiento.Value = "";
                    DropAlmFinal.SelectedValue = "0";
                    //CARGA DATOS
                    string[] RecorteComando = e.CommandArgument.ToString().Split(new char[] { '¬' });

                    double cantidad = Convert.ToDouble(RecorteComando[2].Replace(",","."));
                    

                    detalleReferencia.InnerText = RecorteComando[0] + " " + RecorteComando[1];
                    AUXReferenciaMovimiento.Value = RecorteComando[0];
                    DropAlmOrigen.SelectedValue = RecorteComando[3];
                    LeerStockProducto(RecorteComando[0]);

                    AuxPZcajas.InnerText= "x " + SHConexion.Devuelve_CantidadCaja_PorDefecto(RecorteComando[0].ToString()) + " piezas por caja.";
                    HiddenPZCajas.Value = SHConexion.Devuelve_CantidadCaja_PorDefecto(RecorteComando[0].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                    /*
                    DataTable Modal = SHConexion.Modal_Detalles_Revision(e.CommandArgument.ToString(), Convert.ToString(Selecaño.SelectedValue));
                    IDINSPECCION.Text = e.CommandArgument.ToString();

                    detalleReferencia.InnerText = Modal.Rows[0]["Referencia"].ToString() + " - " + Modal.Rows[0]["Nombre"].ToString();
                    //detalleReferenciaNombre.Text = ;

                    //ACTIVO CELDAS DE OPERARIO Y LAS COMPARO
                    //OPERARIO 1



                    //lkb_Sort_Click("REVISIONES");
                    */

                }

                if (e.CommandName == "Redirect")
                {
                    Response.Redirect("GP12HistoricoReferencia.aspx?REFERENCIA=" + e.CommandArgument);
                }
            }
            catch (Exception ex)
            { 
            }

        }

        
      
        //ESTADO DE REFERENCIAS

        // guarda una fila

        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label CueentaLinea = (Label)e.Row.FindControl("lblEstadoActual");
                Conexion_SMARTH SHconexion = new Conexion_SMARTH();
                //if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                if (!e.Row.RowState.HasFlag(DataControlRowState.Edit))
                { 
                    Label lblVencido30dias = (Label)e.Row.FindControl("lblFechaProxima"); 
                    Label lblFechaVENC = (Label)e.Row.FindControl("lblFechaVencida");
                    Label lblProxVencido = (Label)e.Row.FindControl("lblFechaPrevVencidatxt");
                    if (lblProxVencido.Text != "")
                    {
                    if (Convert.ToDateTime(lblProxVencido.Text) < DateTime.Now && CueentaLinea.Text != "Sin Revisión" && CueentaLinea.Text != "Proy. terminado / Recambio")
                    {
                            lblFechaVENC.Visible = true;
                    }

                    if (Convert.ToDateTime(lblProxVencido.Text) < DateTime.Now.AddDays(+30) && lblFechaVENC.Visible == false)
                    {
                        lblVencido30dias.Visible = true;
                    }
                    }

                }

                if (e.Row.RowState.HasFlag(DataControlRowState.Edit))
                {
                    //drops de estado de referencia
                    DropDownList txtEstadoActual = (DropDownList)e.Row.FindControl("txtEstadoActual");                  
                    DataTable dt = SHconexion.Devuelve_Lista_Estados_Referencias();
                    txtEstadoActual.DataSource = dt;
                    txtEstadoActual.DataTextField = "Razon";
                    txtEstadoActual.DataValueField = "Razon";
                    txtEstadoActual.DataBind();
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    txtEstadoActual.SelectedValue = dr["EstadoActual"].ToString();


                    //drops de responsables
                    DataSet Operarios = SHconexion.Devuelve_mandos_intermedios_SMARTH();
                    Operarios.Tables[0].DefaultView.RowFilter = "Departamento = 'INGENIERIA' OR Departamento = '-'";
                    DataTable DTIngenieria = (Operarios.Tables[0].DefaultView).ToTable();
                    DropDownList txtResponsable = (DropDownList)e.Row.FindControl("txtResponsable");
                    txtResponsable.DataSource = DTIngenieria;
                    txtResponsable.DataTextField = "Nombre";
                    txtResponsable.DataValueField = "Nombre";
                    txtResponsable.DataBind();
                    DataRowView dr2 = e.Row.DataItem as DataRowView;
                    txtResponsable.SelectedValue = dr2["Responsable"].ToString();
                }
            }
        }

        //Redirecciones 
        public void CargaHistorico(Object sender, EventArgs e)
        {
            string[] RecorteRef = detalleReferencia.InnerText.Split(new char[] { '-' });

            Response.Redirect("GP12HistoricoReferencia.aspx?REFERENCIA=" + RecorteRef[0].Trim());
        }

       /*
        protected void lkb_Sort_Click(string e)
        {
            //Bind the data

            //Manage the selected tab.
            this.ManageTabsPostBack(BTN_ESTADO_PRODUCTOS, BTN_ULTIMAS_REVISIONES, pills_historico, pills_estados, e);

        }

        private void ManageTabsPostBack(HtmlButton BTN_ESTADO_PRODUCTOS, HtmlButton BTN_ULTIMAS_REVISIONES,
                                        HtmlGenericControl pills_historico, HtmlGenericControl pills_estados, string grid)
        {
            // desactivte all tabs and panes tab-pane fade
            BTN_ESTADO_PRODUCTOS.Attributes.Add("class", "nav-link");
            BTN_ULTIMAS_REVISIONES.Attributes.Add("class", "nav-link");
            pills_historico.Attributes.Add("class", "tab-pane fade");
            pills_estados.Attributes.Add("class", "tab-pane fade");

            //activate the the tab and pane the user was viewing

            switch (grid)
            {
                //NAVE 3
                case "ESTADO":
                    BTN_ESTADO_PRODUCTOS.Attributes.Add("class", "nav-link active");
                    pills_estados.Attributes.Add("class", "tab-pane fade show active");    
                    break;
                case "REVISIONES":
                    BTN_ULTIMAS_REVISIONES.Attributes.Add("class", "nav-link active");
                    pills_historico.Attributes.Add("class", "tab-pane fade show active");
                    break;

            }
        }

        */
    }

}