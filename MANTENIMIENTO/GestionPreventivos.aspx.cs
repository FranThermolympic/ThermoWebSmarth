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

namespace ThermoWeb.MANTENIMIENTO
{
    public partial class GestionPreventivos : System.Web.UI.Page
    {

        private static DataSet ListaPreguntas = new DataSet();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Nombre"] != null)
                {
                    if (Session["Nombre"].ToString() == "Pedro Amoraga")
                    {
                            SinAcceso.Visible = false;
                            AccesoOK.Visible = true;
                        Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();
                        DataSet ListaTiposMantenimiento = conexion.Devuelve_Listado_TiposMantenimiento();
                        TipoMantCarga.Items.Clear();
                        foreach (DataRow row in ListaTiposMantenimiento.Tables[0].Rows)
                        {
                            TipoMantCarga.Items.Add(row["TipoFrecuencia"].ToString());
                        }
                        CargaDatosInicial(null, null);
                    }
                }
            }

        }

        public void CargaDatosInicial(object sender, EventArgs e)
        {
            try
            {
                Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();

                /*
                DataSet operario_mantenimiento = Personal;
                operario_mantenimiento.Tables[0].DefaultView.RowFilter = "Departamento = 'MANTENIMIENTO' OR Departamento = '-' OR Departamento = 'Z'";
                DataTable DToperario_mantenimiento = (operario_mantenimiento.Tables[0].DefaultView).ToTable();
                lista_realizadoPorNEW.Items.Clear();
                foreach (DataRow row in DToperario_mantenimiento.Rows)
                {
                    lista_realizadoPorNEW.Items.Add(row["Nombre"].ToString());
                }
                prioridad.SelectedValue = conexion.devuelve_prioridad(Convert.ToInt16(ds.Tables[0].Rows[0]["IdPrioridad"].ToString()));
                */
                ListaPreguntas = conexion.Devuelve_Listado_Preguntas_PREVMAQ();
                Q1.Items.Clear();
                Q2.Items.Clear();
                Q3.Items.Clear();
                Q4.Items.Clear();
                Q5.Items.Clear();
                Q6.Items.Clear();
                Q7.Items.Clear();
                Q8.Items.Clear();
                Q9.Items.Clear();
                Q10.Items.Clear();
                Q11.Items.Clear();
                Q12.Items.Clear();
                Q13.Items.Clear();
                Q14.Items.Clear();
                Q15.Items.Clear();
                Q16.Items.Clear();
                Q17.Items.Clear();
                Q18.Items.Clear();
                Q19.Items.Clear();
                Q20.Items.Clear();

                foreach (DataRow row in ListaPreguntas.Tables[0].Rows)
                {
                    Q1.Items.Add(row["Accion"].ToString());
                    Q2.Items.Add(row["Accion"].ToString());
                    Q3.Items.Add(row["Accion"].ToString());
                    Q4.Items.Add(row["Accion"].ToString());
                    Q5.Items.Add(row["Accion"].ToString());
                    Q6.Items.Add(row["Accion"].ToString());
                    Q7.Items.Add(row["Accion"].ToString());
                    Q8.Items.Add(row["Accion"].ToString());
                    Q9.Items.Add(row["Accion"].ToString());
                    Q10.Items.Add(row["Accion"].ToString());
                    Q11.Items.Add(row["Accion"].ToString());
                    Q12.Items.Add(row["Accion"].ToString());
                    Q13.Items.Add(row["Accion"].ToString());
                    Q14.Items.Add(row["Accion"].ToString());
                    Q15.Items.Add(row["Accion"].ToString());
                    Q16.Items.Add(row["Accion"].ToString());
                    Q17.Items.Add(row["Accion"].ToString());
                    Q18.Items.Add(row["Accion"].ToString());
                    Q19.Items.Add(row["Accion"].ToString());
                    Q20.Items.Add(row["Accion"].ToString());
                }


            }
            catch (Exception ex)
            {
            }
        }

        public void CargaDatos(object sender, EventArgs e)
        {
            try
            {
                Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();
                tbTipoRecurso.Text = "";
                tbIdMantenimiento.Text = "";
                ListaPreguntas = conexion.Devuelve_Listado_Preguntas_PREVMAQ();
                DataSet TipoMantCargado = conexion.Devuelve_Tipo_Mantenimiento_Seleccionado_PREVMAQ(TipoMantCarga.SelectedValue.ToString());
                if (TipoMantCargado.Tables[0].Rows.Count > 0)
                {
                    btnGuardar.Visible = true;
                    tbTipoRecurso.Text = TipoMantCargado.Tables[0].Rows[0]["IdMantenimiento"].ToString();
                    tbIdMantenimiento.Text = TipoMantCargado.Tables[0].Rows[0]["IdTipoMantenimiento"].ToString();
                    tbTipoFrecuencia.Visible = true;
                    tbTipoFrecuencia.Text = TipoMantCargado.Tables[0].Rows[0]["TipoFrecuencia"].ToString() + " " + TipoMantCargado.Tables[0].Rows[0]["Frecuencia"].ToString();

                    DataSet ds_ListaMaq = conexion.Devuelve_Listado_MAQ_TIPO_PREVMAQ(tbIdMantenimiento.Text);
                    dgv_ListadoMaquinas.DataSource = ds_ListaMaq;
                    dgv_ListadoMaquinas.DataBind();

                    TablaAcciones.Visible = true;
                    string AuxCuestion = TipoMantCargado.Tables[0].Rows[0]["ArrayPreguntas"].ToString().Split(' ')[0];
                    ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + AuxCuestion + "";
                    DataTable ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                    Q1.SelectedValue = ListaPreguntasSelec.Rows[0]["Accion"].ToString();

                    //REVISAR - Convertir a loop

                    AuxCuestion = TipoMantCargado.Tables[0].Rows[0]["ArrayPreguntas"].ToString().Split(' ')[1];
                    ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + AuxCuestion + "";
                    ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                    Q2.SelectedValue = ListaPreguntasSelec.Rows[0]["Accion"].ToString();

                    AuxCuestion = TipoMantCargado.Tables[0].Rows[0]["ArrayPreguntas"].ToString().Split(' ')[2];
                    ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + AuxCuestion + "";
                    ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                    Q3.SelectedValue = ListaPreguntasSelec.Rows[0]["Accion"].ToString();

                    AuxCuestion = TipoMantCargado.Tables[0].Rows[0]["ArrayPreguntas"].ToString().Split(' ')[3];
                    ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + AuxCuestion + "";
                    ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                    Q4.SelectedValue = ListaPreguntasSelec.Rows[0]["Accion"].ToString();

                    AuxCuestion = TipoMantCargado.Tables[0].Rows[0]["ArrayPreguntas"].ToString().Split(' ')[4];
                    ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + AuxCuestion + "";
                    ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                    Q5.SelectedValue = ListaPreguntasSelec.Rows[0]["Accion"].ToString();

                    AuxCuestion = TipoMantCargado.Tables[0].Rows[0]["ArrayPreguntas"].ToString().Split(' ')[5];
                    ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + AuxCuestion + "";
                    ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                    Q6.SelectedValue = ListaPreguntasSelec.Rows[0]["Accion"].ToString();

                    AuxCuestion = TipoMantCargado.Tables[0].Rows[0]["ArrayPreguntas"].ToString().Split(' ')[6];
                    ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + AuxCuestion + "";
                    ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                    Q7.SelectedValue = ListaPreguntasSelec.Rows[0]["Accion"].ToString();

                    AuxCuestion = TipoMantCargado.Tables[0].Rows[0]["ArrayPreguntas"].ToString().Split(' ')[7];
                    ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + AuxCuestion + "";
                    ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                    Q8.SelectedValue = ListaPreguntasSelec.Rows[0]["Accion"].ToString();

                    AuxCuestion = TipoMantCargado.Tables[0].Rows[0]["ArrayPreguntas"].ToString().Split(' ')[8];
                    ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + AuxCuestion + "";
                    ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                    Q9.SelectedValue = ListaPreguntasSelec.Rows[0]["Accion"].ToString();

                    AuxCuestion = TipoMantCargado.Tables[0].Rows[0]["ArrayPreguntas"].ToString().Split(' ')[9];
                    ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + AuxCuestion + "";
                    ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                    Q10.SelectedValue = ListaPreguntasSelec.Rows[0]["Accion"].ToString();

                    AuxCuestion = TipoMantCargado.Tables[0].Rows[0]["ArrayPreguntas"].ToString().Split(' ')[10];
                    ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + AuxCuestion + "";
                    ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                    Q11.SelectedValue = ListaPreguntasSelec.Rows[0]["Accion"].ToString();

                    AuxCuestion = TipoMantCargado.Tables[0].Rows[0]["ArrayPreguntas"].ToString().Split(' ')[11];
                    ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + AuxCuestion + "";
                    ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                    Q12.SelectedValue = ListaPreguntasSelec.Rows[0]["Accion"].ToString();

                    AuxCuestion = TipoMantCargado.Tables[0].Rows[0]["ArrayPreguntas"].ToString().Split(' ')[12];
                    ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + AuxCuestion + "";
                    ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                    Q13.SelectedValue = ListaPreguntasSelec.Rows[0]["Accion"].ToString();

                    AuxCuestion = TipoMantCargado.Tables[0].Rows[0]["ArrayPreguntas"].ToString().Split(' ')[13];
                    ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + AuxCuestion + "";
                    ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                    Q14.SelectedValue = ListaPreguntasSelec.Rows[0]["Accion"].ToString();

                    AuxCuestion = TipoMantCargado.Tables[0].Rows[0]["ArrayPreguntas"].ToString().Split(' ')[14];
                    ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + AuxCuestion + "";
                    ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                    Q15.SelectedValue = ListaPreguntasSelec.Rows[0]["Accion"].ToString();

                    AuxCuestion = TipoMantCargado.Tables[0].Rows[0]["ArrayPreguntas"].ToString().Split(' ')[15];
                    ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + AuxCuestion + "";
                    ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                    Q16.SelectedValue = ListaPreguntasSelec.Rows[0]["Accion"].ToString();

                    AuxCuestion = TipoMantCargado.Tables[0].Rows[0]["ArrayPreguntas"].ToString().Split(' ')[16];
                    ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + AuxCuestion + "";
                    ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                    Q17.SelectedValue = ListaPreguntasSelec.Rows[0]["Accion"].ToString();

                    AuxCuestion = TipoMantCargado.Tables[0].Rows[0]["ArrayPreguntas"].ToString().Split(' ')[17];
                    ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + AuxCuestion + "";
                    ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                    Q18.SelectedValue = ListaPreguntasSelec.Rows[0]["Accion"].ToString();

                    AuxCuestion = TipoMantCargado.Tables[0].Rows[0]["ArrayPreguntas"].ToString().Split(' ')[18];
                    ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + AuxCuestion + "";
                    ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                    Q19.SelectedValue = ListaPreguntasSelec.Rows[0]["Accion"].ToString();

                    AuxCuestion = TipoMantCargado.Tables[0].Rows[0]["ArrayPreguntas"].ToString().Split(' ')[19];
                    ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + AuxCuestion + "";
                    ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                    Q20.SelectedValue = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void Guardar_PlanMantenimiento(object sender, EventArgs e)
        {
            try
            {
                if (tbTipoRecurso.ToString() != "")
                {
                    Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();
                    DataTable ListaPreguntasSelec = new DataTable();
                    DataSet ListaPreguntas = conexion.Devuelve_Listado_Preguntas_PREVMAQ();
                    string Q1SEL = "1";
                    string Q2SEL = "1";
                    string Q3SEL = "1";
                    string Q4SEL = "1";
                    string Q5SEL = "1";
                    string Q6SEL = "1";
                    string Q7SEL = "1";
                    string Q8SEL = "1";
                    string Q9SEL = "1";
                    string Q10SEL = "1";
                    string Q11SEL = "1";
                    string Q12SEL = "1";
                    string Q13SEL = "1";
                    string Q14SEL = "1";
                    string Q15SEL = "1";
                    string Q16SEL = "1";
                    string Q17SEL = "1";
                    string Q18SEL = "1";
                    string Q19SEL = "1";
                    string Q20SEL = "1";

                    if (Q1.SelectedValue.ToString() != "")
                    {
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "Accion = '" + Q1.SelectedValue.ToString() + "'";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q1SEL = ListaPreguntasSelec.Rows[0]["IdCuestionario"].ToString();
                    }
                    if (Q2.SelectedValue.ToString() != "")
                    {
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "Accion = '" + Q2.SelectedValue.ToString() + "'";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q2SEL = ListaPreguntasSelec.Rows[0]["IdCuestionario"].ToString();
                    }
                    if (Q3.SelectedValue.ToString() != "")
                    {
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "Accion = '" + Q3.SelectedValue.ToString() + "'";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q3SEL = ListaPreguntasSelec.Rows[0]["IdCuestionario"].ToString();
                    }
                    if (Q4.SelectedValue.ToString() != "")
                    {
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "Accion = '" + Q4.SelectedValue.ToString() + "'";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q4SEL = ListaPreguntasSelec.Rows[0]["IdCuestionario"].ToString();

                    }
                    if (Q5.SelectedValue.ToString() != "")
                    {
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "Accion = '" + Q5.SelectedValue.ToString() + "'";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q5SEL = ListaPreguntasSelec.Rows[0]["IdCuestionario"].ToString();

                    }
                    if (Q6.SelectedValue.ToString() != "")
                    {
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "Accion = '" + Q6.SelectedValue.ToString() + "'";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q6SEL = ListaPreguntasSelec.Rows[0]["IdCuestionario"].ToString();

                    }
                    if (Q7.SelectedValue.ToString() != "")
                    {
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "Accion = '" + Q7.SelectedValue.ToString() + "'";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q7SEL = ListaPreguntasSelec.Rows[0]["IdCuestionario"].ToString();

                    }
                    if (Q8.SelectedValue.ToString() != "")
                    {
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "Accion = '" + Q8.SelectedValue.ToString() + "'";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q8SEL = ListaPreguntasSelec.Rows[0]["IdCuestionario"].ToString();

                    }
                    if (Q9.SelectedValue.ToString() != "")
                    {
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "Accion = '" + Q9.SelectedValue.ToString() + "'";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q9SEL = ListaPreguntasSelec.Rows[0]["IdCuestionario"].ToString();

                    }
                    if (Q10.SelectedValue.ToString() != "")
                    {
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "Accion = '" + Q10.SelectedValue.ToString() + "'";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q10SEL = ListaPreguntasSelec.Rows[0]["IdCuestionario"].ToString();

                    }
                    if (Q11.SelectedValue.ToString() != "")
                    {
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "Accion = '" + Q11.SelectedValue.ToString() + "'";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q11SEL = ListaPreguntasSelec.Rows[0]["IdCuestionario"].ToString();

                    }
                    if (Q12.SelectedValue.ToString() != "")
                    {
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "Accion = '" + Q12.SelectedValue.ToString() + "'";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q12SEL = ListaPreguntasSelec.Rows[0]["IdCuestionario"].ToString();

                    }
                    if (Q13.SelectedValue.ToString() != "")
                    {
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "Accion = '" + Q13.SelectedValue.ToString() + "'";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q13SEL = ListaPreguntasSelec.Rows[0]["IdCuestionario"].ToString();

                    }
                    if (Q14.SelectedValue.ToString() != "")
                    {
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "Accion = '" + Q14.SelectedValue.ToString() + "'";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q14SEL = ListaPreguntasSelec.Rows[0]["IdCuestionario"].ToString();

                    }
                    if (Q15.SelectedValue.ToString() != "")
                    {
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "Accion = '" + Q15.SelectedValue.ToString() + "'";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q15SEL = ListaPreguntasSelec.Rows[0]["IdCuestionario"].ToString();

                    }
                    if (Q16.SelectedValue.ToString() != "")
                    {
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "Accion = '" + Q16.SelectedValue.ToString() + "'";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q16SEL = ListaPreguntasSelec.Rows[0]["IdCuestionario"].ToString();

                    }
                    if (Q17.SelectedValue.ToString() != "")
                    {
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "Accion = '" + Q17.SelectedValue.ToString() + "'";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q17SEL = ListaPreguntasSelec.Rows[0]["IdCuestionario"].ToString();
                    }
                    if (Q18.SelectedValue.ToString() != "")
                    {
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "Accion = '" + Q18.SelectedValue.ToString() + "'";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q18SEL = ListaPreguntasSelec.Rows[0]["IdCuestionario"].ToString();

                    }
                    if (Q19.SelectedValue.ToString() != "")
                    {
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "Accion = '" + Q19.SelectedValue.ToString() + "'";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q19SEL = ListaPreguntasSelec.Rows[0]["IdCuestionario"].ToString();

                    }
                    if (Q20.SelectedValue.ToString() != "")
                    {
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "Accion = '" + Q20.SelectedValue.ToString() + "'";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q20SEL = ListaPreguntasSelec.Rows[0]["IdCuestionario"].ToString();

                    }

                    string ArrayPreguntas = Q1SEL + " " + Q2SEL + " " + Q3SEL + " " + Q4SEL + " " + Q5SEL + " " + Q6SEL + " " + Q7SEL + " " + Q8SEL + " " + Q9SEL + " " + Q10SEL + " " + Q11SEL + " " + Q12SEL + " " + Q13SEL + " " + Q14SEL + " " + Q15SEL + " " + Q16SEL + " " + Q17SEL + " " + Q18SEL + " " + Q19SEL + " " + Q20SEL;
                    conexion.Actualizar_Preguntas_PREVMAQ(tbTipoRecurso.Text, ArrayPreguntas);
                    CargaDatos(null, null);
                }

                else { }

            }
            catch (Exception ex)
            {
            }
        }


    }
}
