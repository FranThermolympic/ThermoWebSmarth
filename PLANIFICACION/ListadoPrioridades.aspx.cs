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
        private static DataTable dt_Planificacion = new DataTable();

        private static int OrdenaMaquina = 0;
        private static int OrdenaPrioridad = 0;
        private static int OrdenaOrden = 0;
        private static int OrdenaProducto = 0;
        private static string orderby = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "setTimeout(myFunction, 600000);", true);
            if (!IsPostBack)
            {
                
                Conexion_PLANIFICACION conexion = new Conexion_PLANIFICACION();
                Conexion_SMARTH SHconexion = new Conexion_SMARTH();

                    OrdenaMaquina = 0;
                    OrdenaPrioridad = 0;
                    OrdenaOrden = 0;
                    OrdenaProducto = 0;

                orderby = "ORDER BY PRIORIDADDEC ASC, SEQNR ASC";
                SHconexion.Leer_PlanificacionPrioridades();
                dt_Planificacion = conexion.Devuelve_Planificacion_SMARTH(orderby, 4);
                GridPrioridades.DataSource = dt_Planificacion;
                GridPrioridades.DataBind();


                dt_Planificacion.DefaultView.RowFilter = "IdNoConformidad IS NOT NULL";
                DataTable NCsAbiertas = (dt_Planificacion.DefaultView).ToTable();
                
                GridNoConformidades.DataSource = NCsAbiertas;
                GridNoConformidades.DataBind();
            }

        }

        public void Rellenar_grid(object sender, EventArgs e)
        {
            try
            {
                Conexion_PLANIFICACION conexion = new Conexion_PLANIFICACION();
                Conexion_SMARTH SHconexion = new Conexion_SMARTH();
                SHconexion.Leer_PlanificacionPrioridades();
                dt_Planificacion = conexion.Devuelve_Planificacion_SMARTH(orderby, Convert.ToInt32(DropNumSEQNR.SelectedValue));
                GridPrioridades.DataSource = dt_Planificacion;
                GridPrioridades.DataBind();

                dt_Planificacion.DefaultView.RowFilter = "IdNoConformidad IS NOT NULL";
                DataTable NCsAbiertas = (dt_Planificacion.DefaultView).ToTable();

                GridNoConformidades.DataSource = NCsAbiertas;
                GridNoConformidades.DataBind();
            }
            catch (Exception)
            { 
                
            }
        }
        //SMARTH_Planificacion
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
           
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                if (!e.Row.RowState.HasFlag(DataControlRowState.Edit))

                {
                    try
                    {
                        //ESTADO MANQUINA

                        Label lblSEQNR = (Label)e.Row.FindControl("LBLSEQNR");
                        Label lblMAQPLAY = (Label)e.Row.FindControl("lblplay");
                        Label lblMAQPAUSE = (Label)e.Row.FindControl("lblpause");
                        if (lblSEQNR.Text == "0")
                        {
                            lblMAQPLAY.Visible = true;
                        }
                        else
                        {
                            lblMAQPAUSE.Visible = true;
                        }

                        Label lblPrioridad = (Label)e.Row.FindControl("lblPrioridad");
                        if (lblPrioridad.Text == "100")
                        {
                            lblPrioridad.Text = "-";
                        }
                        
                        //AVISOS DE BMS
                        Label lblAccionOrden = (Label)e.Row.FindControl("lblAccionOrden");
                        TextBox txtAccionProducto = (TextBox)e.Row.FindControl("txtAccionProducto");
                        TextBox txtAccionReceta = (TextBox)e.Row.FindControl("txtAccionReceta");

                        if (lblAccionOrden.Text != "<br />")
                        {
                            lblAccionOrden.Visible = true;
                        }
                        if (txtAccionProducto.Text != "")
                        {
                            txtAccionProducto.Visible = true;
                        }
                        if (txtAccionReceta.Text != "Notas: ")
                        {
                            txtAccionReceta.Visible = true;
                        }

                        //PARTES DE MAQUINA/ROBOT
                        Button BtnParteMaq = (Button)e.Row.FindControl("btnParteMaq");
                        Label lblMAQAveria = (Label)e.Row.FindControl("lblMAQAveria");
                        Label lblMAQRepara = (Label)e.Row.FindControl("lblMAQRepara");
                        
                        Button btnParteROB = (Button)e.Row.FindControl("btnParteROB");
                        Label lblROBAveria = (Label)e.Row.FindControl("lblROBAveria");
                        Label lblROBRepara = (Label)e.Row.FindControl("lblROBRepara");

                        if (BtnParteMaq.Text == "")
                        { 
                            BtnParteMaq.Text = "0"; 
                        }
                        if (btnParteROB.Text == "")
                        { 
                            btnParteROB.Text = "0"; 
                        }
                        
                        if (Convert.ToInt32(BtnParteMaq.Text) > Convert.ToInt32(btnParteROB.Text))
                        {
                            BtnParteMaq.Visible = true;
                            lblMAQAveria.Visible = true;
                            lblMAQRepara.Visible = true;
                        }
                        else if (Convert.ToInt32(BtnParteMaq.Text) < Convert.ToInt32(btnParteROB.Text))
                        {
                            btnParteROB.Visible = true;
                            lblROBAveria.Visible = true;
                            lblROBRepara.Visible = true;
                        }
                        else
                        { }

                        //PARTES DE MOLDE
                        Button btnParteMOL = (Button)e.Row.FindControl("btnParteMOL");
                        Label lblMOLAveria = (Label)e.Row.FindControl("lblMOLAveria");
                        Label lblMOLRepara = (Label)e.Row.FindControl("lblMOLRepara");

                        if (btnParteMOL.Text != "")
                        {
                            btnParteMOL.Visible = true;
                            lblMOLAveria.Visible = true;
                            if (lblMOLRepara.Text != "<strong>Rep.: </strong>")
                            {
                                lblMOLRepara.Visible = true;
                            }
                        }

                        //NO CONFORMIDADES
                        Label lblD3Estado = (Label)e.Row.FindControl("lblD3Estado");
                        Label lblD6Estado = (Label)e.Row.FindControl("lblD6Estado");
                        Label lblD8Estado = (Label)e.Row.FindControl("lblD8Estado");

                        if (lblD3Estado.Text != " <br />")
                        {
                            lblD3Estado.Visible = true;
                        }
                        if (lblD6Estado.Text != " <br />")
                        {
                            lblD6Estado.Visible = true;
                        }
                        if (lblD8Estado.Text != " <br />")
                        {
                            lblD8Estado.Visible = true;
                        }
                        Button btnIDNOConformidad = (Button)e.Row.FindControl("btnIDNOConformidad");
                        if (btnIDNOConformidad.Text != "")
                        { 
                            btnIDNOConformidad.Visible = true;
                        }

                        //GP12
                        Button btnGP12 = (Button)e.Row.FindControl("btnGP12");
                        if (btnGP12.Text != "")
                        {
                            btnGP12.Visible = true;
                        }
                    }
                    catch (Exception ex)
                    { }

                }
            }
          
        }

        // modifica una fila        

        protected void Acciones_Cabecera(object sender, EventArgs e)
        {
            HtmlButton button = (HtmlButton)sender;
            string name = button.ID;
            Conexion_PLANIFICACION conexion = new Conexion_PLANIFICACION();
            string orderby = "";
            switch (name)
            {
                case "OrdenarMaq":
                    if (OrdenaMaquina == 0)
                    {
                        orderby = " ORDER BY Maquina ASC, PRIORIDADDEC ASC";
                        OrdenaMaquina = 1;
                    }
                    else
                    {
                        orderby = " ORDER BY Maquina DESC, PRIORIDADDEC ASC";
                        OrdenaMaquina = 0;
                    }
                    break;

                case "OrdenarOrden":
                    if (OrdenaOrden == 0)
                    {
                        orderby = " ORDER BY SEQNR ASC, TiempoMINUTOS ASC";
                        OrdenaOrden = 1;
                    }
                    else
                    {
                        orderby = " ORDER BY SEQNR ASC, TiempoMINUTOS DESC";
                        OrdenaOrden = 0;
                    }
                    break;

                case "OrdenarPrior":
                    if (OrdenaPrioridad == 0)
                    {
                        orderby = " ORDER BY PRIORIDADDEC ASC, SEQNR ASC";
                        OrdenaPrioridad = 1;
                    }
                    else
                    {

                        orderby = " ORDER BY PRIORIDADDEC DESC, SEQNR ASC";
                        OrdenaPrioridad = 0;
                    }
                    break;

                case "OrdenarProducto":
                    if (OrdenaProducto == 0)
                    {
                        orderby = " ORDER BY Producto ASC, PRIORIDADDEC ASC";
                        OrdenaProducto = 1;
                    }
                    else
                    {

                        orderby = " ORDER BY Producto DESC, PRIORIDADDEC ASC";
                        OrdenaProducto = 0;
                    }
                    break;

                case "btnParteMolHeader":
                    Response.Redirect("../MANTENIMIENTO/EstadoReparacionesMoldes.aspx");
                    break;

                case "btnParteMaqHeader":
                    Response.Redirect("../MANTENIMIENTO/EstadoReparacionesMaquina.aspx");
                    break;

                case "btnNoConformidadesHeader":
                    Response.Redirect("../CALIDAD/ListaAlertasCalidad.aspx");
                    break;

                    

            }

            dt_Planificacion = conexion.Devuelve_Planificacion_SMARTH(orderby, Convert.ToInt32(DropNumSEQNR.SelectedValue));
            GridPrioridades.DataSource = dt_Planificacion;
            GridPrioridades.DataBind();
        }

        public void ContactsGridView_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "RedirectMAQ")
            {
                Response.Redirect("../MANTENIMIENTO/ReparacionMaquinas.aspx?PARTE=" + e.CommandArgument.ToString());
            }
           
            if (e.CommandName == "RedirectMOL")
            {
                Response.Redirect("../MANTENIMIENTO/ReparacionMoldes.aspx?PARTE=" + e.CommandArgument.ToString());          
            }
            if (e.CommandName == "RedirectNC")
            {
                Response.Redirect("../CALIDAD/Alertas_Calidad.aspx?NOCONFORMIDAD=" + e.CommandArgument.ToString());
            }
            if (e.CommandName == "RedirectGP12")
            {
                Response.Redirect("../GP12/GP12HistoricoReferencia.aspx?REFERENCIA=" + e.CommandArgument.ToString());
            }
            if (e.CommandName == "EditarOrden")
            {
                Conexion_PLANIFICACION conexion = new Conexion_PLANIFICACION();
                DataTable ModalTable = conexion.Devuelve_Linea_Planificacion(e.CommandArgument.ToString());
                ModalOrden.InnerText = ModalTable.Rows[0]["Orden"].ToString();
                ModalProducto.InnerText = ModalTable.Rows[0]["Producto"].ToString();
                ModalDescripcion.InnerText = ModalTable.Rows[0]["ProdDescript"].ToString();
                ModalMolde.InnerText = ModalTable.Rows[0]["Molde"].ToString();

                ModalMaquina.InnerText = ModalTable.Rows[0]["Maquina"].ToString();
                ModalPrioridad.InnerText = ModalTable.Rows[0]["Prioridaddec"].ToString();
                ModalTiempRestante.InnerText = ModalTable.Rows[0]["Tiempo"].ToString();

                ModalNotasOrden.Text = ModalTable.Rows[0]["REMARKORDEN"].ToString();
                ModalNotasProductos.Text = ModalTable.Rows[0]["REMARKPRODUCTO"].ToString();
                ModalNotasMaterial.Text = ModalTable.Rows[0]["REMARKRECETA"].ToString();

                DataTable ReparacionesMAQ = conexion.Devuelve_lista_reparaciones_maquina(ModalTable.Rows[0]["Maquina"].ToString());
                GridPartesMaquina.DataSource = ReparacionesMAQ;
                GridPartesMaquina.DataBind();

                DataTable ReparacionesMOL = conexion.Devuelve_lista_reparaciones_molde(Convert.ToInt32(ModalTable.Rows[0]["Molde"].ToString()));
                GridPartesMolde.DataSource = ReparacionesMOL;
                GridPartesMolde.DataBind();

                ClientScript.RegisterStartupScript(this.GetType(), "Pop", "ShowPopup();", true);
            }

        }

        public void ContactsGridView_RowCommand_Partes(Object sender, GridViewCommandEventArgs e)
        {
            Conexion_PLANIFICACION conexion = new Conexion_PLANIFICACION();
            if (e.CommandName == "MOLParteOK")
            {
                string parte = e.CommandArgument.ToString();
                if (parte != "" && Convert.ToInt32(parte) != 0)
                {
                    
                    conexion.Actualizar_Parte_Molde(parte, 1, 1, 0);
                    int molde = conexion.Devuelve_Molde_X_Parte(Convert.ToInt32(parte));
                    conexion.eliminar_accion_pendienteBMSMOLDE(molde); 
                    Rellenar_grid(null, e);
                    DataTable ReparacionesMAQ = conexion.Devuelve_lista_reparaciones_maquina(ModalMaquina.InnerText);
                    GridPartesMaquina.DataSource = ReparacionesMAQ;
                    GridPartesMaquina.DataBind();

                    DataTable ReparacionesMOL = conexion.Devuelve_lista_reparaciones_molde(Convert.ToInt32(ModalMolde.InnerText));
                    GridPartesMolde.DataSource = ReparacionesMOL;
                    GridPartesMolde.DataBind();
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "ShowPopup();", true);
                }
            }
            if (e.CommandName == "MOLParteNOK")
            {
                string parte = e.CommandArgument.ToString();
                if (parte != "" && Convert.ToInt32(parte) != 0)
                {
                    
                    conexion.Actualizar_Parte_Molde(parte, 0, 0, 0);
                    DataTable ReparacionesMAQ = conexion.Devuelve_lista_reparaciones_maquina(ModalMaquina.InnerText);
                    GridPartesMaquina.DataSource = ReparacionesMAQ;
                    GridPartesMaquina.DataBind();

                    DataTable ReparacionesMOL = conexion.Devuelve_lista_reparaciones_molde(Convert.ToInt32(ModalMolde.InnerText));
                    GridPartesMolde.DataSource = ReparacionesMOL;
                    GridPartesMolde.DataBind();

                    Rellenar_grid(null, e);
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "ShowPopup();", true);
                    //envía aviso reparacion abierta 
                }

            }
            if (e.CommandName == "MaqParteOK")
            {
                string parte = e.CommandArgument.ToString();
                if (parte != "" && Convert.ToInt32(parte) != 0)
                {
                    
                    conexion.Actualizar_Parte_Maquina(parte, 1, 1, 0);
                    //string maquina = conexion.Devuelve_Maquina_X_Parte(Convert.ToInt32(parte));
                    //conexion.eliminar_accion_pendienteBMSMAQUINA(maquina);
                    DataTable ReparacionesMAQ = conexion.Devuelve_lista_reparaciones_maquina(ModalMaquina.InnerText);
                    GridPartesMaquina.DataSource = ReparacionesMAQ;
                    GridPartesMaquina.DataBind();

                    DataTable ReparacionesMOL = conexion.Devuelve_lista_reparaciones_molde(Convert.ToInt32(ModalMolde.InnerText));
                    GridPartesMolde.DataSource = ReparacionesMOL;
                    GridPartesMolde.DataBind();

                    Rellenar_grid(null, e);
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "ShowPopup();", true);

                }

            }
            if (e.CommandName == "MaqParteNOK")
            {
                string parte = e.CommandArgument.ToString();
                if (parte != "" && Convert.ToInt32(parte) != 0)
                {
                    DataTable ReparacionesMAQ = conexion.Devuelve_lista_reparaciones_maquina(ModalMaquina.InnerText);
                    GridPartesMaquina.DataSource = ReparacionesMAQ;
                    GridPartesMaquina.DataBind();

                    DataTable ReparacionesMOL = conexion.Devuelve_lista_reparaciones_molde(Convert.ToInt32(ModalMolde.InnerText));
                    GridPartesMolde.DataSource = ReparacionesMOL;
                    GridPartesMolde.DataBind();

                    conexion.Actualizar_Parte_Maquina(parte, 0, 0, 0);
                    Rellenar_grid(null, e);
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "ShowPopup();", true);
                    //envía aviso reparacion abierta 
                }
            }
            if (e.CommandName == "MaqVerParte")
            {
                Response.Redirect("../MANTENIMIENTO/ReparacionMaquinas.aspx?PARTE=" + e.CommandArgument.ToString());
            }
            if (e.CommandName == "MOLVerParte")
            {
                Response.Redirect("../MANTENIMIENTO/ReparacionMoldes.aspx?PARTE=" + e.CommandArgument.ToString());
            }
        }

        public void ActualizarOrden(Object sender, EventArgs e)
        {
            Conexion_PLANIFICACION conexion = new Conexion_PLANIFICACION();
            conexion.Actualizar_Remarks_ProductoBMS(ModalProducto.InnerText, ModalNotasProductos.Text); 
            conexion.Actualizar_Remarks_OrdenesBMS(Convert.ToInt32(ModalPrioridad.InnerText), ModalOrden.InnerText, ModalNotasOrden.Text);
            Rellenar_grid(null, e);
            DataTable ReparacionesMAQ = conexion.Devuelve_lista_reparaciones_maquina(ModalMaquina.InnerText);
            GridPartesMaquina.DataSource = ReparacionesMAQ;
            GridPartesMaquina.DataBind();

            DataTable ReparacionesMOL = conexion.Devuelve_lista_reparaciones_molde(Convert.ToInt32(ModalMolde.InnerText));
            GridPartesMolde.DataSource = ReparacionesMOL;
            GridPartesMolde.DataBind();
            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "ShowPopup();", true);

        }
    }
}