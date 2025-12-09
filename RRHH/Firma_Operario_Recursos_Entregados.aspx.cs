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

namespace ThermoWeb.RRHH
{
    public partial class Firma_Operario_Recursos_Entregados : System.Web.UI.Page
    {

        private static DataTable dt_NAV = new DataTable();
        private static DataTable ds_firmas = new DataTable();
        private static DataTable ds_ListaEntregas = new DataTable();
        private static DataTable ds_esfirmado = new DataTable();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Conexion_SMARTH SHconexion = new Conexion_SMARTH();

                //CONSULTA SESION Y DEVUELVE NOMBRE PARA FIRMA
                if (Session["Nombre"] != null)
                {
                    try
                    {
                        LabelEntrega.Text = Session["Nombre"].ToString();
                    }
                    catch (Exception ex)
                    {
                        LabelEntrega.Text = "ADMINISTRACION";
                    }
                }
                else
                {
                    LabelEntrega.Text = "ADMINISTRACION";
                }

                //CONSULTA ID EN CABECERA Y RELLENA ESTADO DE LA HOJA DE FIRMA SI APLICA
                if (Request.QueryString["ID"] != null)
                {
                    string WHERE = " AND [Line No_] = " + Request.QueryString["ID"].ToString();
                    dt_NAV = SHconexion.Devuelve_listado_Recursos_Entregados(WHERE);
                        valIdNAV.Value = dt_NAV.Rows[0]["Line No_"].ToString();
                        valCodigo.Value = dt_NAV.Rows[0]["Misc_ Article Code"].ToString();

                    lblNumOperario.Text = dt_NAV.Rows[0]["Employee No_"].ToString();
                    lblNom.Text = dt_NAV.Rows[0]["Search Name"].ToString();
                    //lblCantidad.Text = dt_NAV.Rows[0]["Employee No_"].ToString();
                    lblMaterial.Text = dt_NAV.Rows[0]["Description"].ToString();
                    lblNumserie.Text = dt_NAV.Rows[0]["Serial No_"].ToString();
                    numOperarioP2.Text = lblNumOperario.Text + " - " + lblNom.Text;
                    lblFecha.Text = DateTime.Now.ToString("dddd, dd 'de' MMMM 'de' yyyy");
                    ds_esfirmado = SHconexion.Devuelve_EPIS_ListaEntrega(" AND [IdNAV] = " + Request.QueryString["ID"].ToString());
                    if (ds_esfirmado.Rows.Count > 0)
                    {
                        txtCantidad.Visible = false;
                        SinFirmar.Visible = false;
                        Firmado.Visible = true;
                        lblNumserie.Text = "("+ds_esfirmado.Rows[0]["NumSerie"].ToString()+")";
                        lblCantidad.Visible = true;
                        lblCantidad.Text = ds_esfirmado.Rows[0]["Cantidad"].ToString();
                        IMGFirma.ImageUrl = ds_esfirmado.Rows[0]["Firma"].ToString();
                        lblFecha.Text = Convert.ToDateTime(ds_esfirmado.Rows[0]["FechaEntregaSHOW"]).ToString("dddd, dd 'de' MMMM 'de' yyyy");

                        lblEntregadoPor.Text = ds_esfirmado.Rows[0]["EntregadoPOR"].ToString();
                        if (lblEntregadoPor.Text == "")
                        { 
                            lblEntregadoPor.Text = "ADMINISTRACION";
                        }
                        }
                    else
                    {
                        btnFirmar.Visible = true;
                    }

                    Rellenar_grid();

                    /*
                    
                    
                    lblFecha
                    signatureJSON
                    LabelEntrega
                    */
                }
                else
                {
                    
                }
            }

        }
       
        public void InsertarLinea(object sender, EventArgs e)
        {
            Conexion_SMARTH SHconexion = new Conexion_SMARTH();
            
            int IdNav = Convert.ToInt32(valIdNAV.Value);

            int Operario = Convert.ToInt32(lblNumOperario.Text);
            string Codigo = valCodigo.Value;
            string Descripcion = lblMaterial.Text;
            string NumSerie = lblNumserie.Text;
            string Talla = "";
            int cantidad = 1;
            try
            {
                cantidad = Convert.ToInt32(txtCantidad.Value);
            }
            catch (Exception)
            {

            }
            string FechaEntrega = DateTime.Now.ToString();
            int EntregadoPor = 0;
            if (LabelEntrega.Text != "ADMINISTRACION")
            {
                EntregadoPor = SHconexion.Devuelve_ID_Piloto_SMARTH(LabelEntrega.Text);
            }

            string Firma = signatureJSON.Value;
            SHconexion.Insertar_EPI_Entregado(IdNav, Operario, Codigo, Descripcion, NumSerie, Talla, cantidad, FechaEntrega, EntregadoPor, Firma);
            Response.Redirect("Firma_Operario_Recursos_Entregados.aspx?ID=" + IdNav);


            //SHconexion.Insertar_EPI_Entregado()

        }

        public void Rellenar_grid()
        {
            Conexion_SMARTH SHconexion = new Conexion_SMARTH();
            ds_ListaEntregas = SHconexion.Devuelve_listado_Recursos_Entregados(" AND [Employee No_] = "+lblNumOperario.Text+"");
            ds_firmas = SHconexion.Devuelve_EPIS_ListaEntrega("");
            var JoinResult = (from p in ds_ListaEntregas.AsEnumerable()
                              join t in ds_firmas.AsEnumerable()
                              on p.Field<int>("Line No_") equals t.Field<int>("IdNAV") into tempJoin
                              from leftJoin in tempJoin.DefaultIfEmpty()
                              select new
                              {
                                  OPERARIO = p.Field<int>("Employee No_"),
                                  NOMBRE = p.Field<string>("Search Name"),
                                  //NOMBRE = leftJoin == null ? "" : leftJoin.Field<string>("NOMBRE"),
                                  LINEA = p.Field<int>("Line No_"),
                                  ARTICULO = p.Field<string>("Description"),
                                  NUMSERIE = p.Field<string>("Serial No_"),
                                  CANTIDAD = leftJoin == null ? "" : leftJoin.Field<string>("Cantidad"),
                                  FECHAENTREGA = leftJoin == null ? "" : leftJoin.Field<string>("FechaEntrega"),
                                  ENTREGADOPOR = leftJoin == null ? "" : leftJoin.Field<string>("EntregadoPOR"),
                                  FIRMA = leftJoin == null ? "" : leftJoin.Field<string>("Firma"),
                                  //Grade = leftJoin == null ? 0 : leftJoin.Field<int>("Grade")
                              }).ToList();
            GridView2.DataSource = JoinResult;
            GridView2.DataBind();
        }

        // elimina una fila
        public void GridView_RowDeleting(Object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                /*
                int num_fila = Convert.ToInt16(e.RowIndex);
                int i = 0;
                foreach (DataRow row in ds_area.Tables[0].Rows)
                {
                    if (i == num_fila)
                    {

                        Conexion_Area_Rechazo conexion = new Conexion_Area_Rechazo();
                        conexion.Eliminar_area_rechazo(Convert.ToInt32(row["Id"]));
                        ds_area = conexion.Leer_area_rechazo();
                        Rellenar_grid();
                        break;
                    }
                    i++;
                }
                */
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
                                /*
                string id = dgv_AreaRechazo.DataKeys[e.RowIndex].Values["Id"].ToString();
                TextBox referencia = (TextBox)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("txtReferencia");
                TextBox motivo = (TextBox)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("txtMotivo");
                TextBox responsableEntrada = (TextBox)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("txtResponsableEntrada");
                TextBox cantidad = (TextBox)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("txtCantidad");
                TextBox fechaEntrada = (TextBox)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("txtFechaEntrada");
                TextBox fechaSalida = (TextBox)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("txtFechaSalida");
                TextBox debeSalir = (TextBox)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("txtDebeSalir");
                TextBox decision = (TextBox)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("txtDecision");
                TextBox responsable_salida = (TextBox)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("txtResponsableSalida");
                TextBox observaciones = (TextBox)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("txtObservaciones");
                Conexion_Area_Rechazo conexion = new Conexion_Area_Rechazo();
                string fecha_entrada = "";
                string fecha_salida = "";
                string debe_salir = "";

                if (fechaEntrada.Text == "")
                    fecha_entrada = null;
                else
                    fecha_entrada = fechaEntrada.Text;

                if (fechaSalida.Text == "")
                    fecha_salida = null;
                else
                    fecha_salida = fechaSalida.Text;

                if (debeSalir.Text == "")
                    debe_salir = null;
                else
                    debe_salir = debeSalir.Text;

                conexion.Actualizar_area_rechazo(Convert.ToInt32(id), referencia.Text, motivo.Text, responsableEntrada.Text,
                    Convert.ToInt32(cantidad.Text), fecha_entrada, fecha_salida, debe_salir, decision.Text, responsable_salida.Text, observaciones.Text);

                dgv_AreaRechazo.EditIndex = -1;
                ds_area = conexion.Leer_area_rechazo();
                dgv_AreaRechazo.DataSource = ds_area;
                dgv_AreaRechazo.DataBind(); 
                                */
            }
            catch (Exception)
            {
               
            }              
        }

        // cancela la modificación de una fila
        protected void GridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            /*
            dgv_AreaRechazo.EditIndex = -1;
            dgv_AreaRechazo.DataSource = ds_area;
            dgv_AreaRechazo.DataBind(); 
            */
        }

        // modifica una fila
        public void GridView_RowEditing(Object sender, GridViewEditEventArgs e)
        {
            /*
            dgv_AreaRechazo.EditIndex = e.NewEditIndex;
            dgv_AreaRechazo.DataSource = ds_area;
            dgv_AreaRechazo.DataBind();
            */
        }

        // añade nueva fila
        public void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Redirect")
            {
                Response.Redirect("Firma_Operario_Recursos_Entregados.aspx?ID=" + e.CommandArgument.ToString());
            }
        }
        

        // carga la lista utilizando un filtro
        protected void Cargar_filtro(object sender, EventArgs e)
        {
            /*
            string lista_filtros = Request.Form[cbFiltro.UniqueID];
            cbFiltro.Items.FindByValue(lista_filtros).Selected = true;
            string filtro = cbFiltro.Items.FindByValue(lista_filtros).Text;
            Conexion_Area_Rechazo conexion = new Conexion_Area_Rechazo();
            
            if (filtro == "Activas")
            {
                ds_area = conexion.Leer_area_rechazo();
            }
            else
            {
                ds_area = conexion.Leer_area_rechazo_todas();
            }
            dgv_AreaRechazo.DataSource = ds_area;
            dgv_AreaRechazo.DataBind(); 
            */
        }
    }

}