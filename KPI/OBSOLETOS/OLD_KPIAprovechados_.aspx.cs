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

namespace ThermoWeb.KPI
{
    public partial class KPIAprovechados_ : System.Web.UI.Page
    {

        private static DataSet ds_listarefencias = new DataSet();
        private static DataSet ds_listareferenciasmedias = new DataSet();
        private static DataSet ds_listarefenciassemana = new DataSet();
        private static DataSet ds_listarefenciasmes = new DataSet();
        private static DataSet ds_listarefenciasaño = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Conexion_KPI conexion = new Conexion_KPI();
                ds_listarefencias = conexion.Aprovechamiento_Operario_4H_NEW();
                    tbEncargado1.Text = ds_listarefencias.Tables[0].Rows[0]["ENC1"].ToString();
                    tbEncargado2.Text = ds_listarefencias.Tables[0].Rows[0]["ENC2"].ToString();
                    tbCalidad.Text = ds_listarefencias.Tables[0].Rows[0]["CAL"].ToString();
                ds_listareferenciasmedias = conexion.AprovechamientoOperarioMedias4H();
                    tbLogueados.Text = Convert.ToDouble(ds_listareferenciasmedias.Tables[0].Rows[0]["OPLOGUEADOS"]).ToString("N2");
                    tbAprovechados.Text = Convert.ToDouble(ds_listareferenciasmedias.Tables[0].Rows[0]["OPAPROVECHADOS"]).ToString("N2");
                    tbAsignados.Text = Convert.ToDouble(ds_listareferenciasmedias.Tables[0].Rows[0]["OPASIGNADOS"]).ToString("N2");
                    tbLibres.Text = Convert.ToDouble(ds_listareferenciasmedias.Tables[0].Rows[0]["OPLIBRES"]).ToString("N2");

                //CargaListasFiltro();
                Cargar_anuales(null, null);
                Lkb_Sort_Click("TURNO");
                Rellenar_grid();
            }

        }

        private void Rellenar_grid()
        {
            try
            {

                dgv_Aprovechamiento.DataSource = ds_listarefencias;
                dgv_Aprovechamiento.DataBind(); 
            }
            catch (Exception)
            { 
                
            }
        }     
        // carga la lista utilizando un filtro

        protected void Cargar_todas(object sender, EventArgs e)
        {

            Conexion_KPI conexion = new Conexion_KPI();
            ds_listarefencias = conexion.Aprovechamiento_Operario_4H_NEW();
            ds_listareferenciasmedias = conexion.AprovechamientoOperarioMedias4H();
            Rellenar_grid();
        }

        protected void Cargar_anuales(object sender, EventArgs e)
        {
            try {
                tbLogueadosAÑO.Text = "";
                tbAprovechadosAÑO.Text = "";
                tbAsignadosAÑO.Text = "";
                tbLibresAÑO.Text = "";

                Conexion_KPI conexion = new Conexion_KPI();
                ds_listarefenciassemana = conexion.Aprovechamiento_Operario_Semanas_NEW(Selecaño.SelectedValue.ToString());
                    dgv_AprovechamientoSEMANA.DataSource = ds_listarefenciassemana;
                    dgv_AprovechamientoSEMANA.DataBind();
                ds_listarefenciasmes = conexion.AprovechamientoOperarioMes(Selecaño.SelectedValue.ToString());
                    dgv_AprovechamientoMES.DataSource = ds_listarefenciasmes;
                    dgv_AprovechamientoMES.DataBind();
                ds_listarefenciasaño = conexion.AprovechamientoOperarioMediaAño(Selecaño.SelectedValue.ToString());

                tbLogueadosAÑO.Text = Convert.ToDouble(ds_listarefenciasaño.Tables[0].Rows[0]["OPLOGUEADOS"]).ToString("N2");
                tbAprovechadosAÑO.Text = Convert.ToDouble(ds_listarefenciasaño.Tables[0].Rows[0]["OPAPROVECHADOS"]).ToString("N2");
                tbAsignadosAÑO.Text = Convert.ToDouble(ds_listarefenciasaño.Tables[0].Rows[0]["OPASIGNADOS"]).ToString("N2");
                tbLibresAÑO.Text = Convert.ToDouble(ds_listarefenciasaño.Tables[0].Rows[0]["OPLIBRES"]).ToString("N2");
                Lkb_Sort_Click("ANUAL");
            }
            catch (Exception) { }
        }
        protected void Cargar_Filtrados(object sender, EventArgs e)
        {
            //string selectFecha1 = selectFecha.Text;
            //string selectHora1 = "";
            //string selectFecha2 = "";
            //string selectHora2 = "";
            try
            {
                if (selectFecha.Text != "")
                {
                    Conexion_KPI conexion = new Conexion_KPI();
                    int selectTurno = 0;
                    if (INTMañana.Checked == true)
                    { selectTurno = 1; }
                    else if (INTTarde.Checked == true)
                    { selectTurno = 2; }
                    else if (INTNoche.Checked == true)
                    { selectTurno = 3; }

                    switch (selectTurno)
                    {
                        case 0:
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "valorNOK();", true);
                            break;
                        case 1:
                            //Muestra un cartel de necesidad de turno
                            string selectFecha1 = "" + selectFecha.Text + " 06:30";
                            string selectFecha2 = "" + selectFecha.Text + " 14:00";
                            ds_listarefencias = conexion.AprovechamientoOperario(selectFecha1, selectFecha2);
                            tbEncargado1.Text = ds_listarefencias.Tables[0].Rows[0]["ENC1"].ToString();
                            tbEncargado2.Text = ds_listarefencias.Tables[0].Rows[0]["ENC2"].ToString();
                            tbCalidad.Text = ds_listarefencias.Tables[0].Rows[0]["CAL"].ToString();
                            dgv_Aprovechamiento.DataSource = ds_listarefencias;
                            dgv_Aprovechamiento.DataBind();
                            ds_listareferenciasmedias = conexion.AprovechamientoOperarioMedias(selectFecha1, selectFecha2);
                            tbLogueados.Text = Convert.ToDouble(ds_listareferenciasmedias.Tables[0].Rows[0]["OPLOGUEADOS"]).ToString("N2");
                            tbAprovechados.Text = Convert.ToDouble(ds_listareferenciasmedias.Tables[0].Rows[0]["OPAPROVECHADOS"]).ToString("N2");
                            tbAsignados.Text = Convert.ToDouble(ds_listareferenciasmedias.Tables[0].Rows[0]["OPASIGNADOS"]).ToString("N2");
                            tbLibres.Text = Convert.ToDouble(ds_listareferenciasmedias.Tables[0].Rows[0]["OPLIBRES"]).ToString("N2");
                            break;

                        case 2:
                            //Muestra un cartel de necesidad de turno
                            selectFecha1 = "" + selectFecha.Text + " 14:30";
                            selectFecha2 = "" + selectFecha.Text + " 22:00";
                            ds_listarefencias = conexion.AprovechamientoOperario(selectFecha1, selectFecha2);
                            tbEncargado1.Text = ds_listarefencias.Tables[0].Rows[0]["ENC1"].ToString();
                            tbEncargado2.Text = ds_listarefencias.Tables[0].Rows[0]["ENC2"].ToString();
                            tbCalidad.Text = ds_listarefencias.Tables[0].Rows[0]["CAL"].ToString();
                            dgv_Aprovechamiento.DataSource = ds_listarefencias;
                            dgv_Aprovechamiento.DataBind();
                            ds_listareferenciasmedias = conexion.AprovechamientoOperarioMedias(selectFecha1, selectFecha2);
                            tbLogueados.Text = Convert.ToDouble(ds_listareferenciasmedias.Tables[0].Rows[0]["OPLOGUEADOS"]).ToString("N2");
                            tbAprovechados.Text = Convert.ToDouble(ds_listareferenciasmedias.Tables[0].Rows[0]["OPAPROVECHADOS"]).ToString("N2");
                            tbAsignados.Text = Convert.ToDouble(ds_listareferenciasmedias.Tables[0].Rows[0]["OPASIGNADOS"]).ToString("N2");
                            tbLibres.Text = Convert.ToDouble(ds_listareferenciasmedias.Tables[0].Rows[0]["OPLIBRES"]).ToString("N2");
                            break;

                        case 3:
                            //Muestra un cartel de necesidad de turno
                            selectFecha1 = "" + selectFecha.Text + " 22:30";
                            selectFecha2 = "" + selectFecha.Text + " 6:00";
                            DateTime Fecha2 = Convert.ToDateTime(selectFecha2);

                            string selectFecha3 = Fecha2.AddDays(1).ToString("yyyy-MM-dd HH:mm");

                            //string selectFecha3 = "" + selectFecha2 + " 06:00";
                            ds_listarefencias = conexion.AprovechamientoOperario(selectFecha1, selectFecha3);
                            dgv_Aprovechamiento.DataSource = ds_listarefencias;
                            tbEncargado1.Text = ds_listarefencias.Tables[0].Rows[0]["ENC1"].ToString();
                            tbEncargado2.Text = ds_listarefencias.Tables[0].Rows[0]["ENC2"].ToString();
                            tbCalidad.Text = ds_listarefencias.Tables[0].Rows[0]["CAL"].ToString();
                            dgv_Aprovechamiento.DataBind();
                            ds_listareferenciasmedias = conexion.AprovechamientoOperarioMedias(selectFecha1, selectFecha3);
                            tbLogueados.Text = Convert.ToDouble(ds_listareferenciasmedias.Tables[0].Rows[0]["OPLOGUEADOS"]).ToString("N2");
                            tbAprovechados.Text = Convert.ToDouble(ds_listareferenciasmedias.Tables[0].Rows[0]["OPAPROVECHADOS"]).ToString("N2");
                            tbAsignados.Text = Convert.ToDouble(ds_listareferenciasmedias.Tables[0].Rows[0]["OPASIGNADOS"]).ToString("N2");
                            tbLibres.Text = Convert.ToDouble(ds_listareferenciasmedias.Tables[0].Rows[0]["OPLIBRES"]).ToString("N2");
                            break;
                    }

                    Lkb_Sort_Click("TURNO");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "valorNOK();", true);
                }
            }
            catch (Exception)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "valorVACIO();", true);
            }
        }

        protected void Lkb_Sort_Click( string e)
        {
            //Bind the data

            //Manage the selected tab.
            this.ManageTabsPostBack(TURNO, ANUAL, tab0button, tab1button, e);

        }


        private void ManageTabsPostBack(HtmlGenericControl TURNO, HtmlGenericControl ANUAL,
                    HtmlGenericControl tab0button, HtmlGenericControl tab1button, string grid)
        {
            // desactivte all tabs and panes
            tab0button.Attributes.Add("class", "");
            TURNO.Attributes.Add("class", "tab-pane");
            tab1button.Attributes.Add("class", "");
            ANUAL.Attributes.Add("class", "tab-pane");

            //activate the the tab and pane the user was viewing
            switch (grid)
            {
                case "TURNO":
                    tab0button.Attributes.Add("class", "active");
                    TURNO.Attributes.Add("class", "tab-pane active");
                    break;
                case "ANUAL":
                    tab1button.Attributes.Add("class", "active");
                    ANUAL.Attributes.Add("class", "tab-pane active");
                    break;

            }
        }
    }

}