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
    public partial class PrevisionSecado : System.Web.UI.Page
    {

        //private static DataSet ds_Liberaciones = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "setTimeout(myFunction, 600000);", true);
            Conexion_MATERIALES conexion = new Conexion_MATERIALES();
            //DataSet ds_Prevision = conexion.Devuelve_Prevision_Secado();
            //dgv_secado.DataSource = ds_Prevision;
            //dgv_secado.DataBind();
            Rellenar_grid();
            FECHAACT.Text = DateTime.Now.ToString("dd/MM/yyy HH:mm");
            
            if (!IsPostBack)
            {
                //conexion.LimpiarOrdenesProduciendoBMS();
                //conexion.leer_OrdenesProduciendoBMS();

                //ds_Liberaciones = conexion.devuelve_OrdenesProduciendoLOCALTOT();
                //dgv_Liberaciones.DataSource = ds_Liberaciones;
                //dgv_Liberaciones.DataBind();

                DataSet ds_Materiales = conexion.devuelve_lista_materiales("I.[No_] LIKE '2%' ");
                dgv_Materiales.DataSource = ds_Materiales;
                dgv_Materiales.DataBind();
            }
         
        }
        public void FiltraMaterial(object sender, EventArgs e)
        {

            try
            {

                Conexion_MATERIALES conexion = new Conexion_MATERIALES();
                DataSet ds_Materiales = conexion.devuelve_linea_material(NUMMaterial.Text);
                dgv_Materiales.DataSource = ds_Materiales;
                dgv_Materiales.DataBind();
                lkb_Sort_Click("TABLAMATERIALES");

            }
            catch (Exception)
            {

            }
        }

        public void cargar_todas(object sender, EventArgs e)
        {

            try
            {

                Conexion_MATERIALES conexion = new Conexion_MATERIALES();
                DataSet ds_Materiales = conexion.devuelve_lista_materiales("I.[No_] LIKE '2%'");
                dgv_Materiales.DataSource = ds_Materiales;
                dgv_Materiales.DataBind();
                lkb_Sort_Click("TABLAMATERIALES");

            }
            catch (Exception)
            {

            }
        }

        // carga la lista utilizando un filtro

        protected void lkb_Sort_Click(string e)
        {
            //Bind the data

            //Manage the selected tab.
            this.ManageTabsPostBack(SECADO, TABLAMATERIALES, tab0button, tab1button, e);

        }

        private void ManageTabsPostBack(HtmlGenericControl SECADO, HtmlGenericControl TABLAMATERIALES, HtmlGenericControl tab0button, HtmlGenericControl tab1button, string grid)
        {
            // desactivte all tabs and panes
            tab0button.Attributes.Add("class", "");
            SECADO.Attributes.Add("class", "tab-pane");
            tab1button.Attributes.Add("class", "");
            TABLAMATERIALES.Attributes.Add("class", "tab-pane");

            //activate the the tab and pane the user was viewing
            switch (grid)
            {
                case "SECADO":
                    tab0button.Attributes.Add("class", "active");
                    SECADO.Attributes.Add("class", "tab-pane active");
                    break;
                case "TABLAMATERIALES":
                    tab1button.Attributes.Add("class", "active");
                    TABLAMATERIALES.Attributes.Add("class", "tab-pane active");
                    break;
            }
        }

        public void ContactsGridView_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Redirect")
            {
                Response.Redirect("LiberacionSerie.aspx?ORDEN=" + e.CommandArgument.ToString());
            }
        }

        public void Rellenar_grid()
        {
            try
            {
                Conexion_SMARTH SHconexion = new Conexion_SMARTH();
                Conexion_MATERIALES conexion = new Conexion_MATERIALES();
                DataTable PrevisionSecado = conexion.Devuelve_Prevision_SecadoV3();
                DataTable StocksDisponibles = conexion.Devuelve_Stock_MaterialV2();

                //ds_ListaEntregas = SHconexion.Devuelve_listado_Recursos_Entregados(" AND [Employee No_] = " + lblNumOperario.Text + "");
                //ds_firmas = SHconexion.Devuelve_EPIS_ListaEntrega("");
                var JoinResult = (from p in PrevisionSecado.AsEnumerable()
                                  join t in StocksDisponibles.AsEnumerable()
                                  on p.Field<string>("MATERIAL") equals t.Field<string>("MATERIAL") into tempJoin
                                  from leftJoin in tempJoin.DefaultIfEmpty()
                                  select new
                                  {
                                      MAQ = p.Field<string>("MAQ"),
                                      FECHA = p.Field<string>("FECHA"),
                                      INICIARSECADO = p.Field<string>("INICIARSECADO"),
                                      ORDEN = p.Field<string>("ORDEN"),
                                      MATERIAL = p.Field<string>("MATERIAL"),
                                      DESCRIPCION = p.Field<string>("DESCRIPCION"),
                                      DISPONIBLE = leftJoin.Field<Decimal>("CANTALM"),
                                      PREPARAR = p.Field<string>("PREPARAR"),
                                      UBICACION = p.Field<string>("UBICACION"),
                                      NOTAS = p.Field<string>("NOTAS"),
                                      REPETICIONES = p.Field<Decimal>("REPETICIONES"),
                                      SUMAMATS = p.Field<Decimal>("SUMAMATS"),
                                  }).ToList();
                dgv_secado.DataSource = JoinResult;
                dgv_secado.DataBind();
            }
            catch (Exception ex)
            { }
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label REPETECIONES = (Label)e.Row.FindControl("lblRepiticiones");
                    Label CONSUMO = (Label)e.Row.FindControl("lblConsumo");
                    
                    
                    if (REPETECIONES.Text != "1")
                    {
                        CONSUMO.Visible = true;
                    }
                    

                }
            }
            catch (Exception ex)
            {
            }
        }


    }

}