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

namespace ThermoWeb.PLANIFICACION
{
    public partial class Tareas_cambiador : System.Web.UI.Page
    {

        private static DataSet ds_Cambios = new DataSet();
        private static DataSet ds_listadomoldes = new DataSet();
        private static DataSet ds_listadomoldesCOMP = new DataSet();

     
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();

                //Cargar Datalist Filtromolde
                DataTable ListaMoldes = SHConexion.Devuelve_listado_MOLDES_V2();
                {
                    for (int i = 0; i <= ListaMoldes.Rows.Count - 1; i++)
                    {
                        FiltroMolde.InnerHtml = FiltroMolde.InnerHtml + System.Environment.NewLine +
                            String.Format("<option value='{0}'>", ListaMoldes.Rows[i][0]);
                    }
                }

                //Cargar Dropdown Ubicaciones
                DataTable ListaUbicaciones = SHConexion.Devuelve_Listado_Ubicaciones_Molde_Dropdown();
                UbicacionMolde.Items.Clear();
                UbicacionMolde.Items.Add("Sin ubicacion");
                UbicacionMolde.Items.Add("Externo");
                foreach (DataRow row in ListaUbicaciones.Rows) { UbicacionMolde.Items.Add(row["Ubicacion"].ToString().Trim()); }
                UbicacionMolde.ClearSelection();
               
                SHConexion.Leer_PlanificacionPrevisionBMS();
                ds_Cambios = SHConexion.PrevisionCambiosOrdenXMolde("","");
                ds_listadomoldes = SHConexion.Moldes_Pendientes_Taller();

                

                string whereObsoletos = "";
                if (SwitchObsoletos.Checked == true)
                {
                    whereObsoletos = " AND MOL.Activo = 1";
                }
                string whereRecientes = "";
                if (SwitchRecientes.Checked == true)
                {
                    whereRecientes = " AND NOT((FechaUltimaProduccion is null or FechaUltimaProduccion < DATEADD(day, -730, sysdatetime())) and UbicacionExterna <> 'castro')"; 

                }
                string whereActivas = "";
                if (SwitchActivas.Checked == true)
                {
                    whereActivas = " AND (MOL.Activo = 0 OR MOL.FechaUltimaProduccion IS NULL)";
                }
                string whereMoldes = whereObsoletos + whereRecientes + whereActivas;
                    ds_listadomoldesCOMP = SHConexion.Devuelve_lista_moldes_numReparaciones(whereMoldes);
                Rellenar_grid();
                
                if (Request.QueryString["TAB"] == "TALLER")
                {
                    lkb_Sort_Click("TALLER");
                }
                if (Request.QueryString["TAB"] == "LISTAMOLDES")
                {
                    lkb_Sort_Click("LISTAMOLDES");
                }
            }
        }

        protected void OnRowDataBoundLISMOL(object sender, GridViewRowEventArgs e)
        {

            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {


                    HtmlButton Vencido = (HtmlButton)e.Row.FindControl("btnObsoleto");
                    HtmlButton Retirado = (HtmlButton)e.Row.FindControl("btnRetirado");

                    DataRowView dr2 = e.Row.DataItem as DataRowView;

                    if (dr2["FechaUltimaProduccion"].ToString() == "" && dr2["MOLUBICACION"].ToString() != "CASTRO" && dr2["ReferenciaMolde"].ToString() != "")
                    {
                        //e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFF4C1");
                        Vencido.Visible = true;
                    }

                    else if (dr2["FechaUltimaProduccion"].ToString() != "" && (DateTime.Now - Convert.ToDateTime(dr2["FechaUltimaProduccion"].ToString())).TotalDays > 730)
                    {
                        //e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFF4C1");
                        Vencido.Visible = true;
                    }


                    if (dr2["ReferenciaMolde"].ToString() != "" && dr2["Activo"].ToString() == "False")
                    {
                        Retirado.Visible = true;
                    }





                }
            }
            catch (Exception ex)
            {
            }

        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

           try
                {
                Label lblRestante = (Label)e.Row.FindControl("lblRestante");
                Label lblFinCalculado = (Label)e.Row.FindControl("lblfincal");
                
                Label CANTPENDIENTE = (Label)e.Row.FindControl("lblCANTPENDIENTE");
                if (Convert.ToInt32(CANTPENDIENTE.Text) <= 0)
                {
                    lblFinCalculado.Text = "<strong>¡PRODUCCIÓN COMPLETADA!</strong>";
                    lblRestante.Visible = false;
                    lblFinCalculado.DataBind();
                    
                }
                else
                {

                   
                    var timeSpan = TimeSpan.FromDays(Convert.ToDouble(lblRestante.Text));
                        string dd = "";
                            if (timeSpan.Days.ToString("0") != "0")
                            {
                                dd = timeSpan.Days.ToString("0") + "d ";
                            }
                        string hh = timeSpan.Hours.ToString("00");
                        string mm = timeSpan.Minutes.ToString("00");
                    lblRestante.Text = "<strong>Restante: </strong>"+dd + hh +":"+ mm;
                }
                lblRestante.DataBind();
                }
                catch (Exception ex)
                { }
            
        }

        public void BuscarMoldeinforme(Object sender, EventArgs e)
        {
            try
            {
                string whereActivas = "";
                string whereRecientes = "";
                string whereObsoletos = "";
                string whereThermo = "";
                if (tbBuscarMolde.Value == "")
                {
                    {
                        //Conexion conexion = new Conexion();
                        
                        Conexion_SMARTH SHConexion = new Conexion_SMARTH();

                       
                        if (SwitchObsoletos.Checked == true)
                        {
                            whereObsoletos = " AND MOL.Activo = 1";
                        }
                        
                        if (SwitchRecientes.Checked == true)
                        {
                            whereRecientes = " AND NOT((FechaUltimaProduccion is null or FechaUltimaProduccion < DATEADD(day, -730, sysdatetime())) and UbicacionExterna <> 'castro')";

                        }
                        
                        if (SwitchActivas.Checked == true)
                        {
                            whereActivas = " AND (MOL.Activo = 0 OR MOL.FechaUltimaProduccion IS NULL)";
                        }
                        if (SwitchThermo.Checked == true)                       
                        {
                            whereThermo = " AND (UBI.Nave Like 'NAVE%')";
                        }
                        string whereMoldes = whereObsoletos + whereRecientes + whereActivas+whereThermo;
                        ds_listadomoldesCOMP = SHConexion.Devuelve_lista_moldes_numReparaciones(whereMoldes);

                        Rellenar_grid();
                        lkb_Sort_Click("LISTAMOLDES");
                    }
                }
                else
                {

                    Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                    string[] DataListMOLDE = tbBuscarMolde.Value.Split(new char[] { '¬' });
                    string Molde = DataListMOLDE[0];
                    ds_listadomoldesCOMP = SHConexion.Devuelve_lista_moldes_numReparaciones(" AND ReferenciaMolde = '" + Molde + "'");                   
                    Rellenar_grid();
                    lkb_Sort_Click("LISTAMOLDES");
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void Rellenar_grid()
        {
            try
            {
                CambiosPREV.Text = ds_Cambios.Tables[0].Rows.Count.ToString();

                //Filtro Mañana
                ds_Cambios.Tables[0].DefaultView.RowFilter = "HORA >= 6 AND HORA <14";
                    DataTable DTCambiosMañana = ds_Cambios.Tables[0].DefaultView.ToTable();
                    lblCambiosMañana.Text = DTCambiosMañana.Rows.Count.ToString();

                //Filtro Tarde
                ds_Cambios.Tables[0].DefaultView.RowFilter = "HORA >= 14 AND HORA <22";
                    DataTable DTCambiosTarde = ds_Cambios.Tables[0].DefaultView.ToTable();                
                    lblCambiosTarde.Text = DTCambiosTarde.Rows.Count.ToString();

                //Filtro Noche
                ds_Cambios.Tables[0].DefaultView.RowFilter = "HORA < 6 OR HORA >= 22";
                    DataTable DTCambiosNoche = ds_Cambios.Tables[0].DefaultView.ToTable();
                    lblCambiosNoche.Text = DTCambiosNoche.Rows.Count.ToString();

                gvOrders.DataSource = ds_Cambios;
                gvOrders.DataBind();

                dgv_ListadoMoldes.DataSource = ds_listadomoldes;
                dgv_ListadoMoldes.DataBind();

                dgv_Listado_MoldesComp.DataSource = ds_listadomoldesCOMP;
                dgv_Listado_MoldesComp.DataBind();

            }
            catch (Exception)
            {

            }
        }       
       
        public void GridViewCommandEventHandler(object sender, GridViewCommandEventArgs e)
        {
            /*
           if (e.CommandName.Equals("AddNew"))
           {
               TextBox referencia = (TextBox)dgv_AreaRechazo.FooterRow.FindControl("footReferencia");
               TextBox motivo = (TextBox)dgv_AreaRechazo.FooterRow.FindControl("footMotivo");
               TextBox responsableEntrada = (TextBox)dgv_AreaRechazo.FooterRow.FindControl("footResponsableEntrada");
               TextBox cantidad = (TextBox)dgv_AreaRechazo.FooterRow.FindControl("footCantidad");
               TextBox fechaEntrada = (TextBox)dgv_AreaRechazo.FooterRow.FindControl("footFechaEntrada");
               TextBox fechaSalida = (TextBox)dgv_AreaRechazo.FooterRow.FindControl("footFechaSalida");
               TextBox debeSalir = (TextBox)dgv_AreaRechazo.FooterRow.FindControl("footDebeSalir");
               TextBox decision = (TextBox)dgv_AreaRechazo.FooterRow.FindControl("footDecision");
               TextBox responsable_salida = (TextBox)dgv_AreaRechazo.FooterRow.FindControl("footResponsableSalida");
               TextBox observaciones = (TextBox)dgv_AreaRechazo.FooterRow.FindControl("footObservaciones");
               Conexion_PLANIFICACION conexion = new Conexion_PLANIFICACION();
               string fecha_entrada;
               string fecha_salida;
               string debe_salir;

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

               conexion.Insertar_area_rechazo(referencia.Text, motivo.Text, responsableEntrada.Text, Convert.ToInt32(cantidad.Text), fecha_entrada, fecha_salida, debe_salir, decision.Text, responsable_salida.Text, observaciones.Text);                
               ds_area = conexion.Leer_area_rechazo();
               dgv_AreaRechazo.DataSource = ds_area;
               dgv_AreaRechazo.DataBind(); 
           }
            */
        }

        // carga la lista utilizando un filtro

        public void Actualizar_Ubicacion(object sender, EventArgs e)
        {
            try
            {
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();

                int activo = 1;
                if (flexCheckDefault.Checked == false)
                    { 
                    activo = 0; 
                    }

                string MoldeActivo = " ,Activo = " + activo + "";

                SHConexion.ActualizarUbicacionMolde(UbicaMolde.InnerText, UbicacionMolde.SelectedValue, MoldeActivo) ;
                SHConexion.Leer_PlanificacionPrevisionBMS();
                ds_Cambios = SHConexion.PrevisionCambiosOrdenXMolde("", "");
                ds_listadomoldes = SHConexion.Moldes_Pendientes_Taller();
                string whereActivas = "";
                if (SwitchActivas.Checked == true)
                {
                    whereActivas = " AND MOL.Activo = 1";
                }
                ds_listadomoldesCOMP = SHConexion.Devuelve_lista_moldes_numReparaciones(whereActivas);
                Rellenar_grid();
                lkb_Sort_Click("LISTAMOLDES");

            }
            catch (Exception ex)
            { }
        }

        protected void Cargar_filtro(object sender, EventArgs e)
        {
            string HoraInicial = "";
            if (HoraInicio.Value != "")
                {
                
                HoraInicial = " AND cast([FINCALCULADO] as datetime) > cast('" + HoraInicio.Value + "' as datetime)";
            }
            string HoraCierre = "";
            if (HoraFin.Value != "")
                { 
               
                HoraCierre = " AND cast([FINCALCULADO] as datetime) < cast('" + HoraFin.Value + "' as datetime)";
            }
            Conexion_SMARTH SHConexion = new Conexion_SMARTH();
            ds_Cambios = SHConexion.PrevisionCambiosOrdenXMolde(HoraInicial, HoraCierre);
            Rellenar_grid();
            lkb_Sort_Click("PREVISIONCAMBIO");
        }

        protected void ContactsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Reubicar")
            {
                Conexion_SMARTH conexion = new Conexion_SMARTH();
                string PARTE = e.CommandArgument.ToString();
                conexion.Actualizar_Molde_Al_Taller(PARTE);

                ds_listadomoldes = conexion.Moldes_Pendientes_Taller();
                lkb_Sort_Click("TALLER");
                Rellenar_grid();
                
            }
            if (e.CommandName == "Redirect")
            {
                Response.Redirect("http://facts4-srv/thermogestion/MANTENIMIENTO/ReparacionMoldes.aspx?PARTE=" + e.CommandArgument.ToString());
            }
            if (e.CommandName == "EditUbicacion")
            {
                
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                lkb_Sort_Click("LISTAMOLDES");
                DataTable Molde = SHConexion.Devuelve_Molde(e.CommandArgument.ToString());
                UbicaMolde.InnerText = e.CommandArgument.ToString();
                UbicaMoldeNombre.InnerText = Molde.Rows[0]["Descripcion"].ToString();
                UbicacionMolde.SelectedValue = Molde.Rows[0]["Ubicacion"].ToString();
                LblModificado.Text = "Modificado: " + Molde.Rows[0]["FechaModificaUbicacion"].ToString();
                flexCheckDefault.Checked = Convert.ToBoolean(Molde.Rows[0]["Activo"]);
                ImgUbicacion.Src = Molde.Rows[0]["ImagenUbi"].ToString();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "ShowPopup();", true);
            }

            if (e.CommandName == "EditUbicacion2")
            {

                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                lkb_Sort_Click("PREVISIONCAMBIO");
                DataTable Molde = SHConexion.Devuelve_Molde(e.CommandArgument.ToString());
                UbicaMolde.InnerText = e.CommandArgument.ToString();
                UbicaMoldeNombre.InnerText = Molde.Rows[0]["Descripcion"].ToString();
                UbicacionMolde.SelectedValue = Molde.Rows[0]["Ubicacion"].ToString();
                LblModificado.Text = "Modificado: " + Molde.Rows[0]["FechaModificaUbicacion"].ToString();
                flexCheckDefault.Checked = Convert.ToBoolean(Molde.Rows[0]["Activo"]);
                ImgUbicacion.Src = Molde.Rows[0]["ImagenUbi"].ToString();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "ShowPopup();", true);
            }

        }

        protected void lkb_Sort_Click(string e)
        {
            //Bind the data

            //Manage the selected tab.
            this.ManageTabsPostBack(vpillstab1,vpillstab2,vpillstab3, PILLPREVISION, PILLTALLER, PILLLISTAMOLDES, e);

        }

        private void ManageTabsPostBack(HtmlGenericControl vpillstab1, HtmlGenericControl vpillstab2, HtmlGenericControl vpillstab3, 
                                        HtmlButton PILLPREVISION, HtmlButton PILLTALLER, HtmlButton PILLLISTAMOLDES, string grid)
        {
            // desactivte all tabs and panes tab-pane fade


            PILLPREVISION.Attributes.Add("class", "nav-link");
            vpillstab1.Attributes.Add("class", "tab-pane fade");

            PILLTALLER.Attributes.Add("class", "nav-link");
            vpillstab2.Attributes.Add("class", "tab-pane fade");

            PILLLISTAMOLDES.Attributes.Add("class", "nav-link");
            vpillstab3.Attributes.Add("class", "tab-pane fade");

            //activate the the tab and pane the user was viewing
            switch (grid)
            {
                case "PREVISIONCAMBIO":
                    PILLPREVISION.Attributes.Add("class", "nav-link active");
                    vpillstab1.Attributes.Add("class", "tab-pane fade show active");
                    break;
                case "TALLER":
                    PILLTALLER.Attributes.Add("class", "nav-link active");
                    vpillstab2.Attributes.Add("class", "tab-pane fade show active");           
                    break;
                case "LISTAMOLDES":
                    PILLLISTAMOLDES.Attributes.Add("class", "nav-link active");
                    vpillstab3.Attributes.Add("class", "tab-pane fade show active");                
                    break;
                
            }
        }


    }

}