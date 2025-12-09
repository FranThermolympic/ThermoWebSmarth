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

namespace ThermoWeb
{
    public partial class Listado_Manos_Robot : System.Web.UI.Page
    {

        private static DataTable ds_Manos = new DataTable();
        private static DataSet ds_listadomoldesCOMP = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Conexion_SMARTH conexion = new Conexion_SMARTH();
                //Consulta Manos
                ds_Manos = conexion.Devuelve_listado_MANOS_SMARTH(" AND AREA <> 1", " ORDER BY MANO");

                //Consulta Moldes
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
                ds_listadomoldesCOMP = conexion.Devuelve_lista_moldes_numReparaciones(whereMoldes);

                //Rellenar
                RellenarDesplegables();
                Rellenar_grid();

            }
        }
        private void RellenarDesplegables()
        {
            Conexion_SMARTH conexion = new Conexion_SMARTH();
            try
            {            
                //Cargo Manos               
                for (int i = 0; i <= ds_Manos.Rows.Count - 1; i++)
                {
                    FiltroManos.InnerHtml = FiltroManos.InnerHtml + System.Environment.NewLine +
                    String.Format("<option value='{0}'>", ds_Manos.Rows[i][0]);

                    ListaManosAsignacion.InnerHtml = ListaManosAsignacion.InnerHtml + System.Environment.NewLine +
                    String.Format("<option value='{0}'>", ds_Manos.Rows[i][0]);
                    
                }

                //Cargo Ubicaciones
                DataTable Ubicaciones = conexion.Devuelve_listado_UBICACIONES_MANOS();
                for (int i = 0; i <= Ubicaciones.Rows.Count - 1; i++)
                {
                    FiltroUbicacion.InnerHtml = FiltroUbicacion.InnerHtml + System.Environment.NewLine +
                    String.Format("<option value='{0}'>", Ubicaciones.Rows[i][0]);
                }

                //Cargo Ubicaciones Molde
                DataTable ListaUbicaciones = conexion.Devuelve_Listado_Ubicaciones_Molde_Dropdown();
                UbicacionMolde.Items.Clear();
                UbicacionMolde.Items.Add("Sin ubicacion");
                UbicacionMolde.Items.Add("Externo");
                foreach (DataRow row in ListaUbicaciones.Rows) { UbicacionMolde.Items.Add(row["Ubicacion"].ToString().Trim()); }
                UbicacionMolde.ClearSelection();

                //Cargo Lista Moldes
                DataTable ListaMoldes = conexion.Devuelve_listado_MOLDES_V2();
                {
                    for (int i = 0; i <= ListaMoldes.Rows.Count - 1; i++)
                    {
                        FiltroMolde.InnerHtml = FiltroMolde.InnerHtml + System.Environment.NewLine +
                            String.Format("<option value='{0}'>", ListaMoldes.Rows[i][0]);
                    }
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
                dgv_ListadoManos.DataSource = ds_Manos;
                dgv_ListadoManos.DataBind();

                dgv_Listado_MoldesComp.DataSource = ds_listadomoldesCOMP;
                dgv_Listado_MoldesComp.DataBind();
            }
            catch (Exception ex)
            { 
                
            }
        }

        //GESTION DE PESTAÑAS
        protected void lkb_Sort_Click(string e)
        {
            //Bind the data

            //Manage the selected tab.
            this.ManageTabsPostBack(vpillstab1, vpillstab3, PILLPREVISION, PILLLISTAMOLDES, e);

        }

        private void ManageTabsPostBack(HtmlGenericControl vpillstab1, HtmlGenericControl vpillstab3,
                                        HtmlButton PILLPREVISION, HtmlButton PILLLISTAMOLDES, string grid)
        {
            // desactivte all tabs and panes tab-pane fade


            PILLPREVISION.Attributes.Add("class", "nav-link shadow");
            vpillstab1.Attributes.Add("class", "tab-pane fade");

            PILLLISTAMOLDES.Attributes.Add("class", "nav-link shadow");
            vpillstab3.Attributes.Add("class", "tab-pane fade");

            //activate the the tab and pane the user was viewing
            switch (grid)
            {
                case "LISTAMANOS":
                    PILLPREVISION.Attributes.Add("class", "nav-link active  shadow");
                    vpillstab1.Attributes.Add("class", "tab-pane fade show active");
                    break;

                case "LISTAMOLDES":
                    PILLLISTAMOLDES.Attributes.Add("class", "nav-link active  shadow");
                    vpillstab3.Attributes.Add("class", "tab-pane fade show active");
                    break;

            }
        }

        //FILTROS
        protected void Cargar_filtro(object sender, EventArgs e)
        {
            Conexion_SMARTH conexion = new Conexion_SMARTH();
            string MANO = "";
            if (InputFiltroManos.Value.ToString() != "")
            {
                string[] RecorteMano = InputFiltroManos.Value.Split(new char[] { '_' });
                MANO = " AND MAN.MANO = '" + RecorteMano[0] + "'";
            }

            string UBICACION = "";
            if (InputFiltroUbicacion.Value.ToString() != "")
            {
                UBICACION = " AND UBICACION LIKE '" + InputFiltroUbicacion.Value.ToString() + "%'";
            }

            string OBSOLETOS = "";
            if (SwitchActivas.Checked == false)
            {
                OBSOLETOS = " AND AREA <> 1";
            }

            string AREA = "";
            if (InputFiltroArea.Value.ToString() != "0")
            {
                AREA = " AND AREA = '" + InputFiltroArea.Value.ToString() + "'";
            }

            string ORDERBY = " ORDER BY MANO";
            if (selecorderby.SelectedIndex != 0)
            {
                switch (selecorderby.SelectedIndex.ToString())
                {
                    case "1":
                        ORDERBY = " ORDER BY AREA";
                        break;
                    case "2":
                        ORDERBY = " ORDER BY UBICACION";
                        break;
                }
            }
            string FILTRO = MANO + UBICACION + AREA + OBSOLETOS;
            ds_Manos = conexion.Devuelve_listado_MANOS_SMARTH(FILTRO, ORDERBY);
            Rellenar_grid();
            lkb_Sort_Click("LISTAMANOS");
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
                        string whereMoldes = whereObsoletos + whereRecientes + whereActivas + whereThermo;
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

        //EDICION DE MANOS
        protected void Añadir_Mano(object sender, EventArgs e)
        {
            Conexion_SMARTH conexion = new Conexion_SMARTH();
            try
            {
                int numestandar = Convert.ToInt32(NuevoNumMano.Value);
                if (!conexion.Existe_Mano(NuevoNumMano.Value))
                {
                    conexion.Insertar_Mano(NuevoNumMano.Value, NuevoNomMano.Value, NuevoUbicacion.Value, Convert.ToInt32(NuevoAreaSelect.SelectedValue), NuevoNotas.Value);
                    ds_Manos = conexion.Devuelve_listado_MANOS_SMARTH(" AND AREA <> 1", " ORDER BY MANO");

                    FiltroManos.InnerHtml = "";
                    ListaManosAsignacion.InnerHtml = "";

                    for (int i = 0; i <= ds_Manos.Rows.Count - 1; i++)
                    {
                        FiltroManos.InnerHtml = FiltroManos.InnerHtml + System.Environment.NewLine +
                        String.Format("<option value='{0}'>", ds_Manos.Rows[i][0]);

                        ListaManosAsignacion.InnerHtml = ListaManosAsignacion.InnerHtml + System.Environment.NewLine +
                        String.Format("<option value='{0}'>", ds_Manos.Rows[i][0]);

                    }

                    Rellenar_grid();
                    lkb_Sort_Click("LISTAMANOS");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "ExisteMano();", true);
                    lkb_Sort_Click("LISTAMANOS");
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "GuardadoNok();", true);
                lkb_Sort_Click("LISTAMANOS");
            }
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    if (!e.Row.RowState.HasFlag(DataControlRowState.Edit))
                    {
                        Label lblAlmacenb = (Label)e.Row.FindControl("lblAlmacenUB");
                        switch (lblAlmacenb.Text)
                        {
                            case "1":
                                lblAlmacenb.Text = "(Obsoleto)";
                                break;
                            case "2":
                                lblAlmacenb.Text = "(Cuarto de manos)";
                                break;
                            case "3":
                                lblAlmacenb.Text = "(Máquina 34)";
                                break;
                            case "4":
                                lblAlmacenb.Text = "(Cubetas estantería)";
                                break;
                            case "5":
                                lblAlmacenb.Text = "(Máquina 32)";
                                break;
                            case "6":
                                lblAlmacenb.Text = "(Máquina 48)";
                                break;
                            case "7":
                                lblAlmacenb.Text = "(Máquina 43)";
                                break;
                            case "8":
                                lblAlmacenb.Text = "(Junto a molde)";
                                break;
                        }
                        //lblAlmacenb.DataBind();

                        
                        LinkButton BTNVerMasMold = (LinkButton)e.Row.FindControl("BTNVerMasMold");
                        Label lblNuMoldes = (Label)e.Row.FindControl("lblMUMOL");
                        if (Convert.ToInt32(lblNuMoldes.Text) > 1)
                            {
                            BTNVerMasMold.Visible = true;
                            }
                        
                    }

                    if (e.Row.RowState.HasFlag(DataControlRowState.Edit))
                    {
                        DropDownList Area = (DropDownList)e.Row.FindControl("listAREA");
                        DataRowView dr2 = e.Row.DataItem as DataRowView;
                        Area.SelectedValue= dr2["AREA"].ToString();
                    }
                }
                catch (Exception ex)
                {
                }
            }


            
        }
        
        protected void GridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            dgv_ListadoManos.EditIndex = -1;
            dgv_ListadoManos.DataSource = ds_Manos;
            dgv_ListadoManos.DataBind();
            lkb_Sort_Click("LISTAMANOS");
        }

        public void GridView_RowEditing(Object sender, GridViewEditEventArgs e)
        {
            dgv_ListadoManos.EditIndex = e.NewEditIndex;
            dgv_ListadoManos.DataSource = ds_Manos;
            dgv_ListadoManos.DataBind();
            lkb_Sort_Click("LISTAMANOS");
        }

        public void GridView_RowUpdating(Object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Conexion_SMARTH conexion = new Conexion_SMARTH();
                Label Mano = (Label)dgv_ListadoManos.Rows[e.RowIndex].FindControl("txtMano");
                TextBox Descripcion = (TextBox)dgv_ListadoManos.Rows[e.RowIndex].FindControl("txtManoDescripcion");
                TextBox Ubicacion = (TextBox)dgv_ListadoManos.Rows[e.RowIndex].FindControl("txtUbicacion");
                DropDownList Area = (DropDownList)dgv_ListadoManos.Rows[e.RowIndex].FindControl("listAREA");
                TextBox Notas = (TextBox)dgv_ListadoManos.Rows[e.RowIndex].FindControl("txtNotas");
                //conexion
                conexion.Actualizar_Mano(Mano.Text, Descripcion.Text, Ubicacion.Text, Convert.ToInt32(Area.SelectedValue), Notas.Text);
                dgv_ListadoManos.EditIndex = -1;
                Cargar_filtro(null,null);
                lkb_Sort_Click("LISTAMANOS");
            }
            catch (Exception)
            {

            }
        }

        public void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Conexion_SMARTH conexion = new Conexion_SMARTH();
            if (e.CommandName == "Borrar")
            {
                conexion.Obsoletar_Mano(e.CommandArgument.ToString());
                Cargar_filtro(null, null);
                lkb_Sort_Click("LISTAMANOS");
            }
            if (e.CommandName == "AddNew")
            {
                NuevoNumMano.Value = conexion.Devuelve_Max_Mano();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopDocVinculados();", true);
                lkb_Sort_Click("LISTAMANOS");
            }
            if (e.CommandName == "ListaMoldes")
            {
                DataTable ListaMoldes = conexion.Devuelve_listado_Moldes_X_MANO(e.CommandArgument.ToString());
                H5TituloListaMolde.InnerText = " " + ListaMoldes.Rows[0]["Mano"].ToString() + " " + ListaMoldes.Rows[0]["MANDescripcion"].ToString();
                DgvListaMoldes.DataSource = ListaMoldes;
                DgvListaMoldes.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopListaMoldes();", true);
                lkb_Sort_Click("LISTAMANOS");
            }
        }

        public void Actualizar_Mano(object sender, EventArgs e)
        {
            try
            {
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();

                //AsignaMano.InnerText
                string[] RecorteMano = InputAsignaNuevaMano.Value.Split(new char[] { '_' });
                int mano = 0;
                try
                {
                    mano = Convert.ToInt32(RecorteMano[0].ToString());
                }
                catch (Exception)
                {
                    mano = 0;
                }
                RecorteMano[0].ToString();
                SHConexion.ActualizarMoldeXMano(AsignaMano.InnerText, mano);

                ds_listadomoldesCOMP = SHConexion.Devuelve_lista_moldes_numReparaciones(" AND MOL.Activo = 1");
                Rellenar_grid();
                lkb_Sort_Click("LISTAMOLDES");

            }
            catch (Exception ex)
            { }
        }



        //EDICION DE MOLDES
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

                    Label lblAlmacenb = (Label)e.Row.FindControl("lblManoArea");
                    switch (lblAlmacenb.Text)
                    {
                        case "1":
                            lblAlmacenb.Text = "(Obsoleto) - Pos. ";
                            break;
                        case "2":
                            lblAlmacenb.Text = "(Cuarto de manos) - Pos.: ";
                            break;
                        case "3":
                            lblAlmacenb.Text = "(Máquina 34) - Pos. ";
                            break;
                        case "4":
                            lblAlmacenb.Text = "(Cubetas estantería) - Pos. ";
                            break;
                        case "5":
                            lblAlmacenb.Text = "(Máquina 32) - Pos. ";
                            break;
                        case "6":
                            lblAlmacenb.Text = "(Máquina 48) - Pos. ";
                            break;
                        case "7":
                            lblAlmacenb.Text = "(Máquina 43) - Pos. ";
                            break;
                    }

                }
            }
            catch (Exception ex)
            {
            }

        }

        protected void ContactsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
    
              if (e.CommandName == "EditUbicacion")
            {

                Conexion_SMARTH SHConexion = new Conexion_SMARTH();              
                DataTable Molde = SHConexion.Devuelve_Molde(e.CommandArgument.ToString());
                UbicaMolde.InnerText = e.CommandArgument.ToString();
                UbicaMoldeNombre.InnerText = Molde.Rows[0]["Descripcion"].ToString();
                UbicacionMolde.SelectedValue = Molde.Rows[0]["Ubicacion"].ToString();
                LblModificado.Text = "Modificado: " + Molde.Rows[0]["FechaModificaUbicacion"].ToString();
                flexCheckDefault.Checked = Convert.ToBoolean(Molde.Rows[0]["Activo"]);
                ImgUbicacion.Src = Molde.Rows[0]["ImagenUbi"].ToString();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "ShowPopup();", true);
                lkb_Sort_Click("LISTAMOLDES");
            }

            if (e.CommandName == "EditMano")
            {

                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                DataTable Molde = SHConexion.Devuelve_listado_Manos_X_Molde(e.CommandArgument.ToString());
                AsignaMano.InnerText = e.CommandArgument.ToString();
                AsignaManoNombre.InnerText = Molde.Rows[0]["Descripcion"].ToString();
                
                string ubicacion = Molde.Rows[0]["AREAMANO"].ToString();
                switch (ubicacion)
                {
                    case "1":
                        ubicacion = "(Obsoleto - Pos: "+ Molde.Rows[0]["AREAMANO"].ToString() + ")";
                        break;
                    case "2":
                        ubicacion = "(Cuarto de manos - Pos: " + Molde.Rows[0]["AREAMANO"].ToString() + ")";
                        break;
                    case "3":
                        ubicacion = "(Máquina 34 - Pos: " + Molde.Rows[0]["AREAMANO"].ToString() + ")";
                        break;
                    case "4":
                        ubicacion = "(Cubetas estantería - Pos: " + Molde.Rows[0]["AREAMANO"].ToString() + ")";
                        break;
                    case "5":
                        ubicacion = "(Máquina 32 - Pos: " + Molde.Rows[0]["AREAMANO"].ToString() + ")";
                        break;
                    case "6":
                        ubicacion = "(Máquina 48 - Pos: " + Molde.Rows[0]["AREAMANO"].ToString() + ")";
                        break;
                    case "7":
                        ubicacion = "(Máquina 43 - Pos: " + Molde.Rows[0]["AREAMANO"].ToString() + ")"; ;
                        break;
                }

                InputManoActual.Value = Molde.Rows[0]["Mano"].ToString() + " - " + Molde.Rows[0]["MANDESCRIPCION"].ToString() + " " + ubicacion;



                lkb_Sort_Click("LISTAMOLDES");
                
                
                
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "ShowPopupAsignaMano();", true);
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

                SHConexion.ActualizarUbicacionMolde(UbicaMolde.InnerText, UbicacionMolde.SelectedValue, MoldeActivo);
                SHConexion.Leer_PlanificacionPrevisionBMS();
               
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

    }

}