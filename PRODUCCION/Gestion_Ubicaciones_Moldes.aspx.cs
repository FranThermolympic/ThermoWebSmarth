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
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Extensions;
using OfficeOpenXml;

namespace ThermoWeb.PRODUCCION
{
    public partial class Gestion_Ubicaciones_Moldes : System.Web.UI.Page
    {
        private static DataTable ds_Listado_Ubicaciones_Gral = new DataTable();
       

        protected void Page_Load(object sender, EventArgs e)
        {           
            if (!IsPostBack)
            {
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                DataTable CajaMolde = SHConexion.Devuelve_listado_MoldesLineaUbicacion();
                
                
                {
                    for (int i = 0; i <= CajaMolde.Rows.Count - 1; i++)
                    {
                        selectmolde.InnerHtml = selectmolde.InnerHtml + System.Environment.NewLine +
                            String.Format("<option value='{0}'>", CajaMolde.Rows[i][0]);
                    }
                }
                rellenar_grid();

              
                if (Request.QueryString["TAB"] != null)
                {
                    Selector_Label();
                }
               


            }

        }
       
        private void rellenar_grid()
        {
            try
            {
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                ds_Listado_Ubicaciones_Gral = SHConexion.Devuelve_listado_Ubicaciones_Moldes();
                //NAVE 3
                    //UBICACIÓN C
                    ds_Listado_Ubicaciones_Gral.DefaultView.RowFilter = "zona = 'ALTILLO' AND Ubicacion LIKE 'C%'";
                    DataTable ListaUbica = (ds_Listado_Ubicaciones_Gral.DefaultView).ToTable();
                    dgv_Listado1.DataSource = ListaUbica;
                    dgv_Listado1.DataBind();

                    //UBICACIÓN B
                    ds_Listado_Ubicaciones_Gral.DefaultView.RowFilter = "zona = 'ALTILLO' AND Ubicacion LIKE 'B%'";
                    ListaUbica = (ds_Listado_Ubicaciones_Gral.DefaultView).ToTable();
                    dgv_Listado2.DataSource = ListaUbica;
                    dgv_Listado2.DataBind();

                    //UBICACIÓN A
                    ds_Listado_Ubicaciones_Gral.DefaultView.RowFilter = "zona = 'ALTILLO' AND Ubicacion LIKE 'A%'";
                    ListaUbica = (ds_Listado_Ubicaciones_Gral.DefaultView).ToTable();
                    dgv_Listado3.DataSource = ListaUbica;
                    dgv_Listado3.DataBind();

                    //UBICACIÓN D
                    ds_Listado_Ubicaciones_Gral.DefaultView.RowFilter = "zona = 'ALTILLO' AND Ubicacion LIKE 'D%'";
                    ListaUbica = (ds_Listado_Ubicaciones_Gral.DefaultView).ToTable();
                    dgv_Listado4.DataSource = ListaUbica;
                    dgv_Listado4.DataBind();

                    //UBICACIÓN E
                    ds_Listado_Ubicaciones_Gral.DefaultView.RowFilter = "zona = 'ALTILLO' AND Ubicacion LIKE 'E%'";
                    ListaUbica = (ds_Listado_Ubicaciones_Gral.DefaultView).ToTable();
                    dgv_Listado5.DataSource = ListaUbica;
                    dgv_Listado5.DataBind();

                    //UBICACIÓN F
                    ds_Listado_Ubicaciones_Gral.DefaultView.RowFilter = "zona = 'ALTILLO' AND Ubicacion LIKE 'F%'";
                    ListaUbica = (ds_Listado_Ubicaciones_Gral.DefaultView).ToTable();
                    dgv_Listado6.DataSource = ListaUbica;
                    dgv_Listado6.DataBind();

                    //UBICACIÓN G
                    ds_Listado_Ubicaciones_Gral.DefaultView.RowFilter = "zona = 'ALTILLO' AND Ubicacion LIKE 'G%'";
                    ListaUbica = (ds_Listado_Ubicaciones_Gral.DefaultView).ToTable();
                    dgv_Listado7.DataSource = ListaUbica;
                    dgv_Listado7.DataBind();

                    //UBICACIÓN H
                    ds_Listado_Ubicaciones_Gral.DefaultView.RowFilter = "zona = 'ALTILLO' AND Ubicacion LIKE 'H%'";
                    ListaUbica = (ds_Listado_Ubicaciones_Gral.DefaultView).ToTable();
                    dgv_Listado8.DataSource = ListaUbica;
                    dgv_Listado8.DataBind();

                    //UBICACIÓN I
                    ds_Listado_Ubicaciones_Gral.DefaultView.RowFilter = "zona = 'ALTILLO' AND Ubicacion LIKE 'I%'";
                    ListaUbica = (ds_Listado_Ubicaciones_Gral.DefaultView).ToTable();
                    dgv_Listado9.DataSource = ListaUbica;
                    dgv_Listado9.DataBind();

                    //UBICACIÓN J
                    ds_Listado_Ubicaciones_Gral.DefaultView.RowFilter = "zona = 'ALTILLO' AND Ubicacion LIKE 'J%'";
                    ListaUbica = (ds_Listado_Ubicaciones_Gral.DefaultView).ToTable();
                    dgv_Listado10.DataSource = ListaUbica;
                    dgv_Listado10.DataBind();

                    //UBICACIÓN K
                    ds_Listado_Ubicaciones_Gral.DefaultView.RowFilter = "zona = 'ALTILLO' AND Ubicacion LIKE 'K%'";
                    ListaUbica = (ds_Listado_Ubicaciones_Gral.DefaultView).ToTable();
                    dgv_Listado11.DataSource = ListaUbica;
                    dgv_Listado11.DataBind();

                    //UBICACIÓN L
                    ds_Listado_Ubicaciones_Gral.DefaultView.RowFilter = "zona = 'ALTILLO' AND Ubicacion LIKE 'L%'";
                    ListaUbica = (ds_Listado_Ubicaciones_Gral.DefaultView).ToTable();
                    dgv_Listado12.DataSource = ListaUbica;
                    dgv_Listado12.DataBind();

                    //UBICACIÓN M
                    ds_Listado_Ubicaciones_Gral.DefaultView.RowFilter = "zona = 'ALTILLO' AND Ubicacion LIKE 'M%'";
                    ListaUbica = (ds_Listado_Ubicaciones_Gral.DefaultView).ToTable();
                    dgv_Listado13.DataSource = ListaUbica;
                    dgv_Listado13.DataBind();

                    //UBICACIÓN X0
                    ds_Listado_Ubicaciones_Gral.DefaultView.RowFilter = "zona = 'ALTILLO' AND Ubicacion LIKE 'X0%'";
                    ListaUbica = (ds_Listado_Ubicaciones_Gral.DefaultView).ToTable();
                    dgv_Listado14.DataSource = ListaUbica;
                    dgv_Listado14.DataBind();

                    //UBICACIÓN X1
                    ds_Listado_Ubicaciones_Gral.DefaultView.RowFilter = "zona = 'ALTILLO' AND Ubicacion LIKE 'X1%'";
                    ListaUbica = (ds_Listado_Ubicaciones_Gral.DefaultView).ToTable();
                    dgv_Listado15.DataSource = ListaUbica;
                    dgv_Listado15.DataBind();

                    //UBICACIÓN X2
                    ds_Listado_Ubicaciones_Gral.DefaultView.RowFilter = "zona = 'ALTILLO' AND (Ubicacion LIKE 'X2%' OR Ubicacion LIKE 'X3%')";
                    ListaUbica = (ds_Listado_Ubicaciones_Gral.DefaultView).ToTable();
                    dgv_Listado16.DataSource = ListaUbica;
                    dgv_Listado16.DataBind();

                    //UBICACIÓN P
                    ds_Listado_Ubicaciones_Gral.DefaultView.RowFilter = "zona = 'ALTILLO' AND Ubicacion LIKE 'P%'";
                    ListaUbica = (ds_Listado_Ubicaciones_Gral.DefaultView).ToTable();
                    dgv_Listado17.DataSource = ListaUbica;
                    dgv_Listado17.DataBind();

                    //UBICACIÓN O
                    ds_Listado_Ubicaciones_Gral.DefaultView.RowFilter = "zona = 'ALTILLO' AND Ubicacion LIKE 'O%'";
                    ListaUbica = (ds_Listado_Ubicaciones_Gral.DefaultView).ToTable();
                    dgv_Listado18.DataSource = ListaUbica;
                    dgv_Listado18.DataBind();

                    //UBICACIÓN N
                    ds_Listado_Ubicaciones_Gral.DefaultView.RowFilter = "zona = 'ALTILLO' AND Ubicacion LIKE 'N%'";
                    ListaUbica = (ds_Listado_Ubicaciones_Gral.DefaultView).ToTable();
                    dgv_Listado19.DataSource = ListaUbica;
                    dgv_Listado19.DataBind();

                    //UBICACIÓN Y
                    ds_Listado_Ubicaciones_Gral.DefaultView.RowFilter = "zona = 'ALTILLO SUELO' AND Ubicacion LIKE 'Y%'";
                    ListaUbica = (ds_Listado_Ubicaciones_Gral.DefaultView).ToTable();
                    dgv_Listado20.DataSource = ListaUbica;
                    dgv_Listado20.DataBind();

                    //UBICACIÓN Z
                    ds_Listado_Ubicaciones_Gral.DefaultView.RowFilter = "zona = 'ALTILLO SUELO' AND Ubicacion LIKE 'Z%'";
                    ListaUbica = (ds_Listado_Ubicaciones_Gral.DefaultView).ToTable();
                    dgv_Listado21.DataSource = ListaUbica;
                    dgv_Listado21.DataBind();

                    //UBICACIÓN AA
                    ds_Listado_Ubicaciones_Gral.DefaultView.RowFilter = "zona = 'ALTILLO SUELO' AND Ubicacion LIKE 'AA%'";
                    ListaUbica = (ds_Listado_Ubicaciones_Gral.DefaultView).ToTable();
                    dgv_Listado22.DataSource = ListaUbica;
                    dgv_Listado22.DataBind();

                    //UBICACIÓN AC
                    ds_Listado_Ubicaciones_Gral.DefaultView.RowFilter = "zona = 'ALTILLO SUELO' AND Ubicacion LIKE 'AC%'";
                    ListaUbica = (ds_Listado_Ubicaciones_Gral.DefaultView).ToTable();
                    dgv_Listado23.DataSource = ListaUbica;
                    dgv_Listado23.DataBind();

                    //UBICACIÓN AB
                    ds_Listado_Ubicaciones_Gral.DefaultView.RowFilter = "zona = 'BAJO ALTILLO' AND Ubicacion LIKE 'AB%'";
                    ListaUbica = (ds_Listado_Ubicaciones_Gral.DefaultView).ToTable();
                    dgv_Listado24.DataSource = ListaUbica;
                    dgv_Listado24.DataBind();

                    //UBICACIÓN AK
                    ds_Listado_Ubicaciones_Gral.DefaultView.RowFilter = "zona = 'BAJO ALTILLO' AND Ubicacion LIKE 'AK%'";
                    ListaUbica = (ds_Listado_Ubicaciones_Gral.DefaultView).ToTable();
                    dgv_Listado25.DataSource = ListaUbica;
                    dgv_Listado25.DataBind();

                    //UBICACIÓN AE
                    ds_Listado_Ubicaciones_Gral.DefaultView.RowFilter = "zona = 'BAJO ALTILLO' AND Ubicacion LIKE 'AE%'";
                    ListaUbica = (ds_Listado_Ubicaciones_Gral.DefaultView).ToTable();
                    dgv_Listado26.DataSource = ListaUbica;
                    dgv_Listado26.DataBind();

                    //UBICACIÓN AE
                    ds_Listado_Ubicaciones_Gral.DefaultView.RowFilter = "Ubicacion LIKE 'BAJO ALTILLO' OR Ubicacion LIKE 'AF%'";
                    ListaUbica = (ds_Listado_Ubicaciones_Gral.DefaultView).ToTable();
                    dgv_Listado44.DataSource = ListaUbica;
                    dgv_Listado44.DataBind();

                //UBICACIÓN MAQ. 46
                ds_Listado_Ubicaciones_Gral.DefaultView.RowFilter = "Ubicacion LIKE 'AL%'";
                    ListaUbica = (ds_Listado_Ubicaciones_Gral.DefaultView).ToTable();
                    dgv_Listado27.DataSource = ListaUbica;
                    dgv_Listado27.DataBind();


                //NAVE 4
                    //UBICACION MAQ.25
                    ds_Listado_Ubicaciones_Gral.DefaultView.RowFilter = "Ubicacion LIKE 'AJ%'";
                    ListaUbica = (ds_Listado_Ubicaciones_Gral.DefaultView).ToTable();
                    dgv_Listado28.DataSource = ListaUbica;
                    dgv_Listado28.DataBind();

                //UBICACION MAQ.29
                ds_Listado_Ubicaciones_Gral.DefaultView.RowFilter = "Ubicacion LIKE 'MAQ. 29%'";
                ListaUbica = (ds_Listado_Ubicaciones_Gral.DefaultView).ToTable();
                dgv_Listado29.DataSource = ListaUbica;
                dgv_Listado29.DataBind();

                //UBICACION MAQ.31
                ds_Listado_Ubicaciones_Gral.DefaultView.RowFilter = "Ubicacion LIKE 'MAQ. 31%'";
                ListaUbica = (ds_Listado_Ubicaciones_Gral.DefaultView).ToTable();
                dgv_Listado30.DataSource = ListaUbica;
                dgv_Listado30.DataBind();

                //UBICACION MAQ.32
                ds_Listado_Ubicaciones_Gral.DefaultView.RowFilter = "Ubicacion LIKE 'MAQ. 32%'";
                ListaUbica = (ds_Listado_Ubicaciones_Gral.DefaultView).ToTable();
                dgv_Listado31.DataSource = ListaUbica;
                dgv_Listado31.DataBind();

                ds_Listado_Ubicaciones_Gral.DefaultView.RowFilter = "Ubicacion LIKE 'AD%'";
                ListaUbica = (ds_Listado_Ubicaciones_Gral.DefaultView).ToTable();
                dgv_Listado45.DataSource = ListaUbica;
                dgv_Listado45.DataBind();

                //UBICACION MAQ.38
                ds_Listado_Ubicaciones_Gral.DefaultView.RowFilter = "Ubicacion LIKE 'MAQ. 38%'";
                ListaUbica = (ds_Listado_Ubicaciones_Gral.DefaultView).ToTable();
                dgv_Listado32.DataSource = ListaUbica;
                dgv_Listado32.DataBind();

                //NAVE 5

                //UBICACION AG
                ds_Listado_Ubicaciones_Gral.DefaultView.RowFilter = "Ubicacion LIKE 'AG%'";
                ListaUbica = (ds_Listado_Ubicaciones_Gral.DefaultView).ToTable();
                dgv_Listado33.DataSource = ListaUbica;
                dgv_Listado33.DataBind();

                //UBICACION MAQ.33
                ds_Listado_Ubicaciones_Gral.DefaultView.RowFilter = "Ubicacion LIKE 'MAQ. 33%'";
                ListaUbica = (ds_Listado_Ubicaciones_Gral.DefaultView).ToTable();
                dgv_Listado34.DataSource = ListaUbica;
                dgv_Listado34.DataBind();

                //UBICACION MAQ.39
                ds_Listado_Ubicaciones_Gral.DefaultView.RowFilter = "Ubicacion LIKE 'MAQ. 39%'";
                ListaUbica = (ds_Listado_Ubicaciones_Gral.DefaultView).ToTable();
                dgv_Listado35.DataSource = ListaUbica;
                dgv_Listado35.DataBind();

                //UBICACION MAQ.42
                ds_Listado_Ubicaciones_Gral.DefaultView.RowFilter = "Ubicacion LIKE 'MAQ. 42%'";
                ListaUbica = (ds_Listado_Ubicaciones_Gral.DefaultView).ToTable();
                dgv_Listado36.DataSource = ListaUbica;
                dgv_Listado36.DataBind();

                //UBICACION MAQ.43
                ds_Listado_Ubicaciones_Gral.DefaultView.RowFilter = "Ubicacion LIKE 'MAQ. 43%'";
                ListaUbica = (ds_Listado_Ubicaciones_Gral.DefaultView).ToTable();
                dgv_Listado37.DataSource = ListaUbica;
                dgv_Listado37.DataBind();

                //UBICACION MAQ.48
                ds_Listado_Ubicaciones_Gral.DefaultView.RowFilter = "Ubicacion LIKE 'MAQ. 48%'";
                ListaUbica = (ds_Listado_Ubicaciones_Gral.DefaultView).ToTable();
                dgv_Listado38.DataSource = ListaUbica;
                dgv_Listado38.DataBind();

                //EXTERNOS
                ds_Listado_Ubicaciones_Gral.DefaultView.RowFilter = "Ubicacion LIKE 'NAVE 68%'";
                ListaUbica = (ds_Listado_Ubicaciones_Gral.DefaultView).ToTable();
                dgv_Listado39.DataSource = ListaUbica;
                dgv_Listado39.DataBind();

                ds_Listado_Ubicaciones_Gral.DefaultView.RowFilter = "Ubicacion LIKE 'NAVE 69%'";
                ListaUbica = (ds_Listado_Ubicaciones_Gral.DefaultView).ToTable();
                dgv_Listado40.DataSource = ListaUbica;
                dgv_Listado40.DataBind();

                ds_Listado_Ubicaciones_Gral.DefaultView.RowFilter = "Ubicacion LIKE 'OGX%'";
                ListaUbica = (ds_Listado_Ubicaciones_Gral.DefaultView).ToTable();
                dgv_Listado41.DataSource = ListaUbica;
                dgv_Listado41.DataBind();

                ds_Listado_Ubicaciones_Gral.DefaultView.RowFilter = "Ubicacion LIKE 'CASTRO%'";
                ListaUbica = (ds_Listado_Ubicaciones_Gral.DefaultView).ToTable();
                dgv_Listado42.DataSource = ListaUbica;
                dgv_Listado42.DataBind();

                ds_Listado_Ubicaciones_Gral.DefaultView.RowFilter = "Ubicacion LIKE 'Sin ubicacion%' OR Ubicacion LIKE 'Externo%'";
                ListaUbica = (ds_Listado_Ubicaciones_Gral.DefaultView).ToTable();
                dgv_Listado43.DataSource = ListaUbica;
                dgv_Listado43.DataBind();




            }
            catch (Exception ex)
            {
                
            }
        }
             
        public void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                lblNombreUbicacion.Text = e.CommandArgument.ToString();
                DataTable ListaUbicacionesXMolde = SHConexion.Devuelve_listado_Moldes_X_Ubicacion(e.CommandArgument.ToString());
                DgvListaMoldesUbicados.DataSource = ListaUbicacionesXMolde;
                DgvListaMoldesUbicados.DataBind();
                InputFiltroMoldes.Value = "";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "ShowPopup();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {


                    HtmlButton Vencido = (HtmlButton)e.Row.FindControl("btnObsoleto");
                    HtmlButton Retirado = (HtmlButton)e.Row.FindControl("btnRetirado");

                    DataRowView dr2 = e.Row.DataItem as DataRowView;

                    if (dr2["FechaUltimaProduccion"].ToString() == "" && dr2["Nave"].ToString() != "EXTERNO" && dr2["ReferenciaMolde"].ToString() != "")
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

        //Eliminar molde de ubicación
        public void GridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
               
                    Label referencia = (Label)DgvListaMoldesUbicados.Rows[e.RowIndex].FindControl("lblMolnum");
                    SHConexion.ActualizarUbicacionMolde(referencia.Text,"Sin ubicacion", "");
                    rellenar_grid();
                    DataTable ListaUbicacionesXMolde = SHConexion.Devuelve_listado_Moldes_X_Ubicacion(lblNombreUbicacion.Text);
                    DgvListaMoldesUbicados.DataSource = ListaUbicacionesXMolde;
                    DgvListaMoldesUbicados.DataBind();
                    InputFiltroMoldes.Value = "";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "ShowPopup();", true);

            }
            catch (Exception ex)
            {
            }
        }

        public void Agregar_Molde(object sender, EventArgs e)
        {
            Conexion_SMARTH SHConexion = new Conexion_SMARTH();
            if (InputFiltroMoldes.Value != "")
            {
            string[] DataListVALOR = InputFiltroMoldes.Value.Split(new char[] { '¬' });
            SHConexion.ActualizarUbicacionMolde(DataListVALOR[0], lblNombreUbicacion.Text, "") ;
            rellenar_grid();
            DataTable ListaUbicacionesXMolde = SHConexion.Devuelve_listado_Moldes_X_Ubicacion(lblNombreUbicacion.Text);
            DgvListaMoldesUbicados.DataSource = ListaUbicacionesXMolde;
            DgvListaMoldesUbicados.DataBind();
            InputFiltroMoldes.Value = "";
            
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "ShowPopup();", true);
            }
          
        }

        //GESTOR DE PESTAÑAS
        private void Selector_Label()
        {
            string selector = Request.QueryString["TAB"].ToString();

            switch (selector)
            {
                case "ABC":
                    lkb_Sort_Click("BTN_ALTILLO_ABC");
                    break;
                case "DEF":
                    lkb_Sort_Click("BTN_ALTILLO_DEF");
                    break;
                case "G":
                    lkb_Sort_Click("BTN_ALTILLO_G");
                    break;
                case "HIJ":
                    lkb_Sort_Click("BTN_ALTILLO_HIJ");
                    break;
                case "MLK":
                    lkb_Sort_Click("BTN_ALTILLO_MLK");
                    break;
                case "PON":
                    lkb_Sort_Click("BTN_ALTILLO_NOP");
                    break;
                case "XX":
                    lkb_Sort_Click("BTN_ALTILLO_XX");
                    break;
                case "YZ":
                    lkb_Sort_Click("BTN_ALTILLO_YZ");
                    break;
                case "AAAC":
                    lkb_Sort_Click("BTN_ALTILLO_AAAC");
                    break;
                case "ABAK":
                    lkb_Sort_Click("BTN_ALTILLO_ABAK");
                    break;
                case "AE":
                    lkb_Sort_Click("BTN_ALTILLO_AE");
                    break;
                case "MAQ46":
                    lkb_Sort_Click("BTN_ALTILLO_MAQ46");
                    break;
                case "MAQ25":
                    lkb_Sort_Click("BTN_ALTILLO_MAQ25");
                    break;
                case "MAQ29":
                    lkb_Sort_Click("BTN_ALTILLO_MAQ29");
                    break;
                case "MAQ31":
                    lkb_Sort_Click("BTN_ALTILLO_MAQ31");
                    break;
                case "MAQ32":
                    lkb_Sort_Click("BTN_ALTILLO_MAQ32");
                    break;
                case "MAQ38":
                    lkb_Sort_Click("BTN_ALTILLO_MAQ38");
                    break;
                case "AG":
                    lkb_Sort_Click("BTN_ALTILLO_AG");
                    break;
                case "MAQ33":
                    lkb_Sort_Click("BTN_ALTILLO_MAQ33");
                    break;
                case "MAQ39":
                    lkb_Sort_Click("BTN_ALTILLO_MAQ39");
                    break;
                case "MAQ42":
                    lkb_Sort_Click("BTN_ALTILLO_MAQ42");
                    break;
                case "MAQ43":
                    lkb_Sort_Click("BTN_ALTILLO_MAQ43");
                    break;
                case "MAQ48":
                    lkb_Sort_Click("BTN_ALTILLO_MAQ48");
                    break;
                case "NAVE68":
                    lkb_Sort_Click("BTN_ALTILLO_NAVE68");
                    break;
                case "OGX":
                    lkb_Sort_Click("BTN_ALTILLO_OGX");
                    break;
                case "CASTRO":
                    lkb_Sort_Click("BTN_ALTILLO_CASTRO");
                    break;
                case "SINUBICAR":
                    lkb_Sort_Click("BTN_ALTILLO_SINUBICAR");
                    break;
            }               
        }

        protected void lkb_Sort_Click(string e)
        {
            //Bind the data

            //Manage the selected tab.
            this.ManageTabsPostBack(TAB_NAVE3, TAB_NAVE4, TAB_NAVE5, TAB_EXTERNO,
                                    BTN_ALTILLO_ABC, BTN_ALTILLO_DEF, BTN_ALTILLO_G, BTN_ALTILLO_HIJ, BTN_ALTILLO_MLK, BTN_ALTILLO_XX,
                                    BTN_ALTILLO_NOP, BTN_ALTILLO_YZ, BTN_ALTILLO_AAAC, BTN_ALTILLO_ABAK, BTN_ALTILLO_AE, BTN_ALTILLO_MAQ46,
                                    BTN_ALTILLO_MAQ25, BTN_ALTILLO_MAQ29, BTN_ALTILLO_MAQ31, BTN_ALTILLO_MAQ32, BTN_ALTILLO_MAQ38,
                                    BTN_ALTILLO_AG, BTN_ALTILLO_MAQ33, BTN_ALTILLO_MAQ39, BTN_ALTILLO_MAQ42, BTN_ALTILLO_MAQ43, BTN_ALTILLO_MAQ48,
                                    BTN_ALTILLO_NAVE68, BTN_ALTILLO_OGX, BTN_ALTILLO_CASTRO, BTN_ALTILLO_SINUBICAR,
                                    pills_TAB_NAVE3, pills_TAB_NAVE4, pills_TAB_NAVE5, pills_TAB_EXTERNO,
                                    ALTILLO_ABC, ALTILLO_DEF, ALTILLO_G, ALTILLO_HIJ, ALTILLO_MLK, ALTILLO_XX,
                                    ALTILLO_NOP, ALTILLO_YZ, ALTILLO_AAAC, ALTILLO_ABAK, ALTILLO_AE, ALTILLO_MAQ46,
                                    ALTILLO_MAQ25, ALTILLO_MAQ29, ALTILLO_MAQ31, ALTILLO_MAQ32, ALTILLO_MAQ38,
                                    ALTILLO_AG, ALTILLO_MAQ33, ALTILLO_MAQ39, ALTILLO_MAQ42, ALTILLO_MAQ43, ALTILLO_MAQ48,
                                    ALTILLO_NAVE68, ALTILLO_OGX, ALTILLO_CASTRO, ALTILLO_SINUBICAR, e);

        }

        private void ManageTabsPostBack(HtmlButton TAB_NAVE3, HtmlButton TAB_NAVE4, HtmlButton TAB_NAVE5, HtmlButton TAB_EXTERNO,
                                        HtmlButton BTN_ALTILLO_ABC, HtmlButton BTN_ALTILLO_DEF, HtmlButton BTN_ALTILLO_G, HtmlButton BTN_ALTILLO_HIJ, HtmlButton BTN_ALTILLO_MLK, HtmlButton BTN_ALTILLO_XX,
                                        HtmlButton BTN_ALTILLO_NOP, HtmlButton BTN_ALTILLO_YZ, HtmlButton BTN_ALTILLO_AAAC, HtmlButton BTN_ALTILLO_ABAK, HtmlButton BTN_ALTILLO_AE, HtmlButton BTN_ALTILLO_MAQ46,
                                        HtmlButton BTN_ALTILLO_MAQ25, HtmlButton BTN_ALTILLO_MAQ29, HtmlButton BTN_ALTILLO_MAQ31, HtmlButton BTN_ALTILLO_MAQ32, HtmlButton BTN_ALTILLO_MAQ38,
                                        HtmlButton BTN_ALTILLO_AG, HtmlButton BTN_ALTILLO_MAQ33, HtmlButton BTN_ALTILLO_MAQ39, HtmlButton BTN_ALTILLO_MAQ42, HtmlButton BTN_ALTILLO_MAQ43, HtmlButton BTN_ALTILLO_MAQ48,
                                        HtmlButton BTN_ALTILLO_NAVE68, HtmlButton BTN_ALTILLO_OGX, HtmlButton BTN_ALTILLO_CASTRO, HtmlButton BTN_ALTILLO_SINUBICAR,
                                        HtmlGenericControl pills_TAB_NAVE3, HtmlGenericControl pills_TAB_NAVE4, HtmlGenericControl pills_TAB_NAVE5, HtmlGenericControl pills_TAB_EXTERNO,
                                        HtmlGenericControl ALTILLO_ABC, HtmlGenericControl ALTILLO_DEF, HtmlGenericControl ALTILLO_G, HtmlGenericControl ALTILLO_HIJ, HtmlGenericControl ALTILLO_MLK, HtmlGenericControl ALTILLO_XX,
                                        HtmlGenericControl ALTILLO_NOP, HtmlGenericControl ALTILLO_YZ, HtmlGenericControl ALTILLO_AAAC, HtmlGenericControl ALTILLO_ABAK, HtmlGenericControl ALTILLO_AE, HtmlGenericControl ALTILLO_MAQ46,
                                        HtmlGenericControl ALTILLO_MAQ25, HtmlGenericControl ALTILLO_MAQ29, HtmlGenericControl ALTILLO_MAQ31, HtmlGenericControl ALTILLO_MAQ32, HtmlGenericControl ALTILLO_MAQ38,
                                        HtmlGenericControl ALTILLO_AG, HtmlGenericControl ALTILLO_MAQ33, HtmlGenericControl ALTILLO_MAQ39, HtmlGenericControl ALTILLO_MAQ42, HtmlGenericControl ALTILLO_MAQ43, HtmlGenericControl ALTILLO_MAQ48,
                                        HtmlGenericControl ALTILLO_NAVE68, HtmlGenericControl ALTILLO_OGX, HtmlGenericControl ALTILLO_CASTRO, HtmlGenericControl ALTILLO_SINUBICAR, string grid)
                                        {
            // desactivte all tabs and panes tab-pane fade
            TAB_NAVE3.Attributes.Add("class", "nav-link");
            TAB_NAVE4.Attributes.Add("class", "nav-link");
            TAB_NAVE5.Attributes.Add("class", "nav-link");
            TAB_EXTERNO.Attributes.Add("class", "nav-link");
            BTN_ALTILLO_ABC.Attributes.Add("class", "nav-link");
            BTN_ALTILLO_DEF.Attributes.Add("class", "nav-link");
            BTN_ALTILLO_G.Attributes.Add("class", "nav-link");
            BTN_ALTILLO_HIJ.Attributes.Add("class", "nav-link");
            BTN_ALTILLO_MLK.Attributes.Add("class", "nav-link");
            BTN_ALTILLO_XX.Attributes.Add("class", "nav-link");
            BTN_ALTILLO_NOP.Attributes.Add("class", "nav-link");
            BTN_ALTILLO_YZ.Attributes.Add("class", "nav-link");
            BTN_ALTILLO_AAAC.Attributes.Add("class", "nav-link");
            BTN_ALTILLO_ABAK.Attributes.Add("class", "nav-link");
            BTN_ALTILLO_AE.Attributes.Add("class", "nav-link");
            BTN_ALTILLO_MAQ46.Attributes.Add("class", "nav-link");
            BTN_ALTILLO_MAQ25.Attributes.Add("class", "nav-link");
            BTN_ALTILLO_MAQ29.Attributes.Add("class", "nav-link");
            BTN_ALTILLO_MAQ31.Attributes.Add("class", "nav-link");
            BTN_ALTILLO_MAQ32.Attributes.Add("class", "nav-link");
            BTN_ALTILLO_MAQ38.Attributes.Add("class", "nav-link");
            BTN_ALTILLO_AG.Attributes.Add("class", "nav-link");
            BTN_ALTILLO_MAQ33.Attributes.Add("class", "nav-link");
            BTN_ALTILLO_MAQ39.Attributes.Add("class", "nav-link");
            BTN_ALTILLO_MAQ42.Attributes.Add("class", "nav-link");
            BTN_ALTILLO_MAQ43.Attributes.Add("class", "nav-link");
            BTN_ALTILLO_MAQ48.Attributes.Add("class", "nav-link");
            BTN_ALTILLO_NAVE68.Attributes.Add("class", "nav-link");
            BTN_ALTILLO_OGX.Attributes.Add("class", "nav-link");
            BTN_ALTILLO_CASTRO.Attributes.Add("class", "nav-link");
            BTN_ALTILLO_SINUBICAR.Attributes.Add("class", "nav-link");

            pills_TAB_NAVE3.Attributes.Add("class", "tab-pane fade");
            pills_TAB_NAVE4.Attributes.Add("class", "tab-pane fade");
            pills_TAB_NAVE5.Attributes.Add("class", "tab-pane fade");
            pills_TAB_EXTERNO.Attributes.Add("class", "tab-pane fade");
            ALTILLO_ABC.Attributes.Add("class", "tab-pane fade");
            ALTILLO_DEF.Attributes.Add("class", "tab-pane fade");
            ALTILLO_G.Attributes.Add("class", "tab-pane fade");
            ALTILLO_HIJ.Attributes.Add("class", "tab-pane fade");
            ALTILLO_MLK.Attributes.Add("class", "tab-pane fade");
            ALTILLO_XX.Attributes.Add("class", "tab-pane fade");
            ALTILLO_NOP.Attributes.Add("class", "tab-pane fade");
            ALTILLO_YZ.Attributes.Add("class", "tab-pane fade");
            ALTILLO_AAAC.Attributes.Add("class", "tab-pane fade");
            ALTILLO_ABAK.Attributes.Add("class", "tab-pane fade");
            ALTILLO_AE.Attributes.Add("class", "tab-pane fade");
            ALTILLO_MAQ46.Attributes.Add("class", "tab-pane fade");
            ALTILLO_MAQ25.Attributes.Add("class", "tab-pane fade");
            ALTILLO_MAQ29.Attributes.Add("class", "tab-pane fade");
            ALTILLO_MAQ31.Attributes.Add("class", "tab-pane fade");
            ALTILLO_MAQ32.Attributes.Add("class", "tab-pane fade");
            ALTILLO_MAQ38.Attributes.Add("class", "tab-pane fade");
            ALTILLO_AG.Attributes.Add("class", "tab-pane fade");
            ALTILLO_MAQ33.Attributes.Add("class", "tab-pane fade");
            ALTILLO_MAQ39.Attributes.Add("class", "tab-pane fade");
            ALTILLO_MAQ42.Attributes.Add("class", "tab-pane fade");
            ALTILLO_MAQ43.Attributes.Add("class", "tab-pane fade");
            ALTILLO_MAQ48.Attributes.Add("class", "tab-pane fade");
            ALTILLO_NAVE68.Attributes.Add("class", "tab-pane fade");
            ALTILLO_OGX.Attributes.Add("class", "tab-pane fade");
            ALTILLO_CASTRO.Attributes.Add("class", "tab-pane fade");
            ALTILLO_SINUBICAR.Attributes.Add("class", "tab-pane fade");

            //activate the the tab and pane the user was viewing
            
                                   
            switch (grid)
            {
                //NAVE 3
                case "BTN_ALTILLO_ABC":
                    TAB_NAVE3.Attributes.Add("class", "nav-link active");
                    pills_TAB_NAVE3.Attributes.Add("class", "tab-pane fade show active");
                    BTN_ALTILLO_ABC.Attributes.Add("class", "nav-link active");
                    ALTILLO_ABC.Attributes.Add("class", "tab-pane fade show active");
                    break;
                case "BTN_ALTILLO_DEF":
                    TAB_NAVE3.Attributes.Add("class", "nav-link active");
                    pills_TAB_NAVE3.Attributes.Add("class", "tab-pane fade show active");
                    BTN_ALTILLO_DEF.Attributes.Add("class", "nav-link active");
                    ALTILLO_DEF.Attributes.Add("class", "tab-pane fade show active");
                    break;
                case "BTN_ALTILLO_G":
                    TAB_NAVE3.Attributes.Add("class", "nav-link active");
                    pills_TAB_NAVE3.Attributes.Add("class", "tab-pane fade show active");
                    BTN_ALTILLO_G.Attributes.Add("class", "nav-link active");
                    ALTILLO_G.Attributes.Add("class", "tab-pane fade show active");
                    break;
                case "BTN_ALTILLO_HIJ":
                    TAB_NAVE3.Attributes.Add("class", "nav-link active");
                    pills_TAB_NAVE3.Attributes.Add("class", "tab-pane fade show active");
                    BTN_ALTILLO_HIJ.Attributes.Add("class", "nav-link active");
                    ALTILLO_HIJ.Attributes.Add("class", "tab-pane fade show active");
                    break;
                case "BTN_ALTILLO_MLK":
                    TAB_NAVE3.Attributes.Add("class", "nav-link active");
                    pills_TAB_NAVE3.Attributes.Add("class", "tab-pane fade show active");
                    BTN_ALTILLO_MLK.Attributes.Add("class", "nav-link active");
                    ALTILLO_MLK.Attributes.Add("class", "tab-pane fade show active");
                    break;
                case "BTN_ALTILLO_XX":
                    TAB_NAVE3.Attributes.Add("class", "nav-link active");
                    pills_TAB_NAVE3.Attributes.Add("class", "tab-pane fade show active");
                    BTN_ALTILLO_XX.Attributes.Add("class", "nav-link active");
                    ALTILLO_XX.Attributes.Add("class", "tab-pane fade show active");
                    break;
                case "BTN_ALTILLO_NOP":
                    TAB_NAVE3.Attributes.Add("class", "nav-link active");
                    pills_TAB_NAVE3.Attributes.Add("class", "tab-pane fade show active");
                    BTN_ALTILLO_NOP.Attributes.Add("class", "nav-link active");
                    ALTILLO_NOP.Attributes.Add("class", "tab-pane fade show active");
                    break;
                case "BTN_ALTILLO_YZ":
                    TAB_NAVE3.Attributes.Add("class", "nav-link active");
                    pills_TAB_NAVE3.Attributes.Add("class", "tab-pane fade show active");
                    BTN_ALTILLO_YZ.Attributes.Add("class", "nav-link active");
                    ALTILLO_YZ.Attributes.Add("class", "tab-pane fade show active");
                    break;
                case "BTN_ALTILLO_AAAC":
                    TAB_NAVE3.Attributes.Add("class", "nav-link active");
                    pills_TAB_NAVE3.Attributes.Add("class", "tab-pane fade show active");
                    BTN_ALTILLO_AAAC.Attributes.Add("class", "nav-link active");
                    ALTILLO_AAAC.Attributes.Add("class", "tab-pane fade show active");
                    break;
                case "BTN_ALTILLO_ABAK":
                    TAB_NAVE3.Attributes.Add("class", "nav-link active");
                    pills_TAB_NAVE3.Attributes.Add("class", "tab-pane fade show active");
                    BTN_ALTILLO_ABAK.Attributes.Add("class", "nav-link active");
                    ALTILLO_ABAK.Attributes.Add("class", "tab-pane fade show active");
                    break;
                case "BTN_ALTILLO_AE":
                    TAB_NAVE3.Attributes.Add("class", "nav-link active");
                    pills_TAB_NAVE3.Attributes.Add("class", "tab-pane fade show active");
                    BTN_ALTILLO_AE.Attributes.Add("class", "nav-link active");
                    ALTILLO_AE.Attributes.Add("class", "tab-pane fade show active");
                    break;
                case "BTN_ALTILLO_MAQ46":
                    TAB_NAVE3.Attributes.Add("class", "nav-link active");
                    pills_TAB_NAVE3.Attributes.Add("class", "tab-pane fade show active");
                    BTN_ALTILLO_MAQ46.Attributes.Add("class", "nav-link active");
                    ALTILLO_MAQ46.Attributes.Add("class", "tab-pane fade show active");
                    break;

                //NAVE 4                   
                case "BTN_ALTILLO_MAQ25":
                    TAB_NAVE4.Attributes.Add("class", "nav-link active");
                    pills_TAB_NAVE4.Attributes.Add("class", "tab-pane fade show active");
                    BTN_ALTILLO_MAQ25.Attributes.Add("class", "nav-link active");
                    ALTILLO_MAQ25.Attributes.Add("class", "tab-pane fade show active");
                    break;
                case "BTN_ALTILLO_MAQ29":
                    TAB_NAVE4.Attributes.Add("class", "nav-link active");
                    pills_TAB_NAVE4.Attributes.Add("class", "tab-pane fade show active");
                    BTN_ALTILLO_MAQ29.Attributes.Add("class", "nav-link active");
                    ALTILLO_MAQ29.Attributes.Add("class", "tab-pane fade show active");
                    break;
                case "BTN_ALTILLO_MAQ31":
                    TAB_NAVE4.Attributes.Add("class", "nav-link active");
                    pills_TAB_NAVE4.Attributes.Add("class", "tab-pane fade show active");
                    BTN_ALTILLO_MAQ31.Attributes.Add("class", "nav-link active");
                    ALTILLO_MAQ31.Attributes.Add("class", "tab-pane fade show active");
                    break;
                case "BTN_ALTILLO_MAQ32":
                    TAB_NAVE4.Attributes.Add("class", "nav-link active");
                    pills_TAB_NAVE4.Attributes.Add("class", "tab-pane fade show active");
                    BTN_ALTILLO_MAQ32.Attributes.Add("class", "nav-link active");
                    ALTILLO_MAQ32.Attributes.Add("class", "tab-pane fade show active");
                    break;
                case "BTN_ALTILLO_MAQ38":
                    TAB_NAVE4.Attributes.Add("class", "nav-link active");
                    pills_TAB_NAVE4.Attributes.Add("class", "tab-pane fade show active");
                    BTN_ALTILLO_MAQ38.Attributes.Add("class", "nav-link active");
                    ALTILLO_MAQ38.Attributes.Add("class", "tab-pane fade show active");
                    break;

                //NAVE 5                                   
                case "BTN_ALTILLO_AG":
                    TAB_NAVE5.Attributes.Add("class", "nav-link active");
                    pills_TAB_NAVE5.Attributes.Add("class", "tab-pane fade show active");
                    BTN_ALTILLO_AG.Attributes.Add("class", "nav-link active");
                    ALTILLO_AG.Attributes.Add("class", "tab-pane fade show active");
                    break;
                case "BTN_ALTILLO_MAQ33":
                    TAB_NAVE5.Attributes.Add("class", "nav-link active");
                    pills_TAB_NAVE5.Attributes.Add("class", "tab-pane fade show active");
                    BTN_ALTILLO_MAQ33.Attributes.Add("class", "nav-link active");
                    ALTILLO_MAQ33.Attributes.Add("class", "tab-pane fade show active");
                    break;
                case "BTN_ALTILLO_MAQ39":
                    TAB_NAVE5.Attributes.Add("class", "nav-link active");
                    pills_TAB_NAVE5.Attributes.Add("class", "tab-pane fade show active");
                    BTN_ALTILLO_MAQ39.Attributes.Add("class", "nav-link active");
                    ALTILLO_MAQ39.Attributes.Add("class", "tab-pane fade show active");
                    break;
                case "BTN_ALTILLO_MAQ42":
                    TAB_NAVE5.Attributes.Add("class", "nav-link active");
                    pills_TAB_NAVE5.Attributes.Add("class", "tab-pane fade show active");
                    BTN_ALTILLO_MAQ42.Attributes.Add("class", "nav-link active");
                    ALTILLO_MAQ42.Attributes.Add("class", "tab-pane fade show active");
                    break;
                case "BTN_ALTILLO_MAQ43":
                    TAB_NAVE5.Attributes.Add("class", "nav-link active");
                    pills_TAB_NAVE5.Attributes.Add("class", "tab-pane fade show active");
                    BTN_ALTILLO_MAQ43.Attributes.Add("class", "nav-link active");
                    ALTILLO_MAQ43.Attributes.Add("class", "tab-pane fade show active");
                    break;
                case "BTN_ALTILLO_MAQ48":
                    TAB_NAVE5.Attributes.Add("class", "nav-link active");
                    pills_TAB_NAVE5.Attributes.Add("class", "tab-pane fade show active");
                    BTN_ALTILLO_MAQ48.Attributes.Add("class", "nav-link active");
                    ALTILLO_MAQ48.Attributes.Add("class", "tab-pane fade show active");
                    break;

               //EXTERNO
                case "BTN_ALTILLO_NAVE68":
                    TAB_EXTERNO.Attributes.Add("class", "nav-link active");
                    pills_TAB_EXTERNO.Attributes.Add("class", "tab-pane fade show active");
                    BTN_ALTILLO_NAVE68.Attributes.Add("class", "nav-link active");
                    ALTILLO_NAVE68.Attributes.Add("class", "tab-pane fade show active");
                    break;
                case "BTN_ALTILLO_OGX":
                    TAB_EXTERNO.Attributes.Add("class", "nav-link active");
                    pills_TAB_EXTERNO.Attributes.Add("class", "tab-pane fade show active");
                    BTN_ALTILLO_OGX.Attributes.Add("class", "nav-link active");
                    ALTILLO_OGX.Attributes.Add("class", "tab-pane fade show active");
                    break;
                case "BTN_ALTILLO_CASTRO":
                    TAB_EXTERNO.Attributes.Add("class", "nav-link active");
                    pills_TAB_EXTERNO.Attributes.Add("class", "tab-pane fade show active");
                    BTN_ALTILLO_CASTRO.Attributes.Add("class", "nav-link active");
                    ALTILLO_CASTRO.Attributes.Add("class", "tab-pane fade show active");
                    break;
                case "BTN_ALTILLO_SINUBICAR":
                    TAB_EXTERNO.Attributes.Add("class", "nav-link active");
                    pills_TAB_EXTERNO.Attributes.Add("class", "tab-pane fade show active");
                    BTN_ALTILLO_SINUBICAR.Attributes.Add("class", "nav-link active");
                    ALTILLO_SINUBICAR.Attributes.Add("class", "tab-pane fade show active");
                    break;
            }
        }


    }
}
