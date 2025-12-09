using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Web.UI.HtmlControls;
using System.IO;

namespace ThermoWeb.MANTENIMIENTO
{
    public partial class ReparacionMoldes : System.Web.UI.Page
    {
        SqlConnection cnnPROG = new SqlConnection();
        private static DataSet ds = new DataSet();
        private int pag_actual = 1;
        private static int reparado_ant = 0;
        private static int revisado_ant = 0;
        private static int revisadoNOK_ant = 0;
        private int reparado_act = 1;
        private int revisado_act = 1;
        private int revisadoNOK_act = 1;

        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
            {
                //CargarDropdowns();
                
                if (Request.QueryString["PARTE"] != null)
                {
                    tbBuscar.Value = Request.QueryString["PARTE"].ToString();
                    BuscarParte(null, null);
                }
                else
                { 
                    Cargar_datos(pag_actual);
                }
            }
            cargar_pendientes();
           
           
            //Conexion conexion = new Conexion();
            //conexion.leer_moldes();
            //conexion.importacion();
            //conexion.insertar_nuevos_partes();
        }

        //GESTIÓN DE PARTES
        public void CrearNuevo(Object sender, EventArgs e)
        {
            try
            {
                Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();
                Conexion_SMARTH SHconexion = new Conexion_SMARTH();


                SHconexion.Leer_moldes(); //ACTUALIZA LISTADO DE MOLDES CON bms
                RellenarDropDowns();

                //REGISTRO 

                lista_partes.Value = Convert.ToString(conexion.max_idParte() + 1);
                HiddenNuevo.Value = "1";
                prioridad.ClearSelection();

                
                tbMoldeNew.Value = "";


                lista_trabajos.ClearSelection();
                averia.Value = "";

               // img1.Src = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";
                //img2.Src = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";

                img1.ImageUrl = "../SMARTH_docs/MANTENIMIENTO/sin_imagen.jpg";
                img2.ImageUrl = "../SMARTH_docs/MANTENIMIENTO/sin_imagen.jpg";



                datetimepicker.Value = "";
                encargado.ClearSelection();
                //turno.ClearSelection();
                ubicacion.ClearSelection();

                //REPARACION
                BloquePreventivo.Visible = false;
                LabelTipoPreventivo.Visible = false;

                lista_realizadoPorNEW.ClearSelection();
                datetime_ini2.Value = "";
                datetime_rep2.Value = "";
                reparado.Checked = false; //cambiado
                //terminado.Attributes.Add("class", "btn btn2 btn-success2 active");
                reparado.Attributes.Add("class", "form-check-input ms-2 bg-danger"); //cambiado


                //lista_realizadoPor.ClearSelection();
                lista_reparacion.ClearSelection();
                reparacion.Value = "";
                observaciones.Value = "";

                DropEstado.ClearSelection();
                horas1.Value = "";
                horas2.Value = "";
                horas3.Value = "";
                Horasprevistas.Value = "";
                TbCostes.Value = "0.0";
                TbCostesTotales.Value = "0.0";
                Asignado1.Value = "Daniel Ferrer";
                Asignado2.Value = "-";
                Asignado3.Value = "-";
                ReparadoPor1.Value = "-";
                ReparadoPor2.Value = "-";
                ReparadoPor3.Value = "-";


                //REVISION
                //revisado_label.Attributes.Add("class", "btn btn2 btn-success2 active");
                revisado.Attributes.Add("class", "form-check-input ms-2 bg-danger");


                revisado.Checked = false;
                //revisadoNOK_label.Attributes.Add("class", "btn btn2 btn-success2 active");
                revisadoNOK.Attributes.Add("class", "form-check-input ms-2 bg-danger");

                revisadoNOK.Checked = false;
                date_revision2.Value = "";
                revisado_por.ClearSelection();
                observaciones_revision.Value = "";

            }
            catch (Exception ex)
            {
                mandar_mail2(ex.Message+ex.StackTrace, "Error en la aplicación de mantenimiento");
            }
        }

        public void Cargar_datos(int pag_actual)
        {
            try
            {
                Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();

                ds = conexion.TablaReparacionMoldes();
                RellenarDropDowns();

                //CARGA Y RELLENA DESPLEGABLES
                DataSet moldes = conexion.Devuelve_lista_moldes();
                DataSet molde = conexion.Devuelve_molde(Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["IDMoldes"].ToString()));


                //BARRA DE ESTADO
                int estadoreparado = Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["Terminado"]);
                int estadovalidado = Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["Revisado"]);
                int estadovalidadonok = Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["RevisadoNOK"]);
                if (estadoreparado == 0 && estadovalidado == 0 && estadovalidadonok == 0 && DropEstado.SelectedValue == "0") //pendiente
                {
                    progressERROR.Visible = false;
                    progressABIERTO.Visible = true;
                    progressABIERTOINI.Visible = false;
                    progressPENDIENTE.Visible = false;
                    progressNOCONFORME.Visible = false;
                    progressREPARADO.Visible = false;
                }
                if (estadoreparado == 0 && estadovalidado == 0 && estadovalidadonok == 0 && DropEstado.SelectedValue == "1") //pendiente iniciado
                {
                    progressERROR.Visible = false;
                    progressABIERTO.Visible = false;
                    progressABIERTOINI.Visible = true;
                    progressPENDIENTE.Visible = false;
                    progressNOCONFORME.Visible = false;
                    progressREPARADO.Visible = false;
                }
                if (estadoreparado == 0 && (estadovalidado == 1 || estadovalidadonok == 1)) //error
                {
                    progressERROR.Visible = true;
                    progressABIERTO.Visible = false;
                    progressABIERTOINI.Visible = false;
                    progressPENDIENTE.Visible = false;
                    progressNOCONFORME.Visible = false;
                    progressREPARADO.Visible = false;
                }
                if (estadoreparado == 1 && estadovalidado == 1 && estadovalidadonok == 1) //error
                {
                    progressERROR.Visible = true;
                    progressABIERTO.Visible = false;
                    progressABIERTOINI.Visible = false;
                    progressPENDIENTE.Visible = false;
                    progressNOCONFORME.Visible = false;
                    progressREPARADO.Visible = false;
                }
                if (estadoreparado == 1 && estadovalidado == 0 && estadovalidadonok == 0) //pendiente
                {
                    progressERROR.Visible = false;
                    progressABIERTO.Visible = false;
                    progressPENDIENTE.Visible = true;
                    progressNOCONFORME.Visible = false;
                    progressREPARADO.Visible = false;
                }
                if (estadoreparado == 1 && estadovalidado == 1 && estadovalidadonok == 0) //reparado
                {
                    progressERROR.Visible = false;
                    progressABIERTO.Visible = false;
                    progressABIERTOINI.Visible = false;
                    progressPENDIENTE.Visible = false;
                    progressNOCONFORME.Visible = false;
                    progressREPARADO.Visible = true;
                }
                if (estadoreparado == 1 && estadovalidado == 0 && estadovalidadonok == 1)//noconforme
                {
                    progressERROR.Visible = false;
                    progressABIERTO.Visible = false;
                    progressABIERTOINI.Visible = false;
                    progressPENDIENTE.Visible = false;
                    progressNOCONFORME.Visible = true;
                    progressREPARADO.Visible = false;
                }
                //estado cierre
                if ((bool)ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["Terminado"])
                {
                    //terminado.Attributes.Add("class", "btn btn2 btn-success2 active");
                    reparado.Attributes.Add("class", "form-check-input ms-2 bg-success");
                    reparado.Checked = true;

                    reparado_ant = 1;
                }
                else
                {
                    //terminado.Attributes.Add("class", "btn btn2 btn-success2");
                    //class="form-check-input ms-2 bg-danger"
                    reparado.Attributes.Add("class", "form-check-input ms-2 bg-danger");
                    reparado.Checked = false;
                    reparado_ant = 0;
                }


                //REGISTRO
                HiddenNuevo.Value = "0";
                tbMoldeNew.Value = SHConexion.Devuelve_linea_MOLDES(molde.Tables[0].Rows[0]["ReferenciaMolde"].ToString()).Rows[0]["MOLDE"].ToString();


                lista_partes.Value = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["IdReparacionMolde"].ToString();
                prioridad.ClearSelection();
                prioridad.SelectedValue = SHConexion.Devuelve_prioridad_parte(Convert.ToInt16(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["IdPrioridad"].ToString()));

                if (tbMoldeNew.Value != "")
                {
                    DataSet ds_listahistorico = conexion.Devuelve_historico_Molde(" AND IDMoldes = '" + tbMoldeNew.Value.Split('¬')[0].ToString() + "'");
                    dgv_ListadoHistorico.DataSource = ds_listahistorico;
                    dgv_ListadoHistorico.DataBind();
                }

                DataSet ProximaProduccion = conexion.Devuelve_Proxima_Produccion_Molde(tbMoldeNew.Value.Split('¬')[0].ToString());
                if (ProximaProduccion.Tables[0].Rows.Count == 0)
                {
                    TbProxProduccion.Value = "No planificado";
                }
                else if (ProximaProduccion.Tables[0].Rows[0]["C_SEQNR"].ToString() == "0")
                {
                    TbProxProduccion.Value = "Produciendo";
                }
                else
                {
                    TbProxProduccion.Value = ProximaProduccion.Tables[0].Rows[0]["C_CALCSTARTDATE"].ToString();
                }

               
               

                lista_trabajos.ClearSelection();
                lista_trabajos.SelectedValue = SHConexion.Devuelve_TipoMantenimiento(Convert.ToInt16(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["IdTipoRevision"].ToString()));
                averia.Value = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["MotivoAveria"].ToString();

                string imagen1 = conexion.Devuelve_imagenes(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["IdReparacionMolde"].ToString(), 1);
                string imagen2 = conexion.Devuelve_imagenes(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["IdReparacionMolde"].ToString(), 2);
                string imagen3 = conexion.Devuelve_imagenes(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["IdReparacionMolde"].ToString(), 3);
                if (imagen1 != "")
                {
                    img1.ImageUrl = imagen1;
                }
                else
                {
                    //img1.Src = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";
                    img1.ImageUrl = "../SMARTH_docs/MANTENIMIENTO/sin_imagen.jpg";
                    
                }
                if (imagen2 != "")
                {
                    img2.ImageUrl = imagen2;
                }
                else
                {
                    img2.ImageUrl = "../SMARTH_docs/MANTENIMIENTO/sin_imagen.jpg";
                    //img2.Src = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";
                }
               

                
                datetimepicker.Value = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["FechaAveria"].ToString();
                
                encargado.ClearSelection();
                string EncargadoSelect = SHConexion.Devuelve_Pilotos_SMARTH(Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["IdEncargado"].ToString()));
                if (EncargadoSelect != "")
                {
                    encargado.SelectedValue = EncargadoSelect;
                }
                ubicacion.ClearSelection();
                ubicacion.SelectedValue = SHConexion.Devuelve_ubicacion(Convert.ToInt16(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["IdUbicacion"].ToString()));
                    //OBSOLETOS
                    /*
                        turno.ClearSelection();
                        turno.SelectedValue = conexion.devuelve_turno(Convert.ToInt16(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["IdTurno"].ToString()));
                    */
                //BlOQUE SELECCION PREVENTIVO (CARGA DE DATASET Y DROPDOWN)
                BloquePreventivo.Visible = false;
                LabelTipoPreventivo.Visible = false;
                DropTipoPreventivo.Items.Clear();
                Q1DROP.ClearSelection();
                Q1Text.Text = "";
                Q1INT.Text = "1";
                Q2DROP.ClearSelection();
                Q2Text.Text = "";
                Q2INT.Text = "1";
                Q3DROP.ClearSelection();
                Q3Text.Text = "";
                Q3INT.Text = "1";
                Q4DROP.ClearSelection();
                Q4Text.Text = "";
                Q4INT.Text = "1";
                Q5DROP.ClearSelection();
                Q5Text.Text = "";
                Q5INT.Text = "1";
                Q6DROP.ClearSelection();
                Q6Text.Text = "";
                Q6INT.Text = "1";
                Q7DROP.ClearSelection();
                Q7Text.Text = "";
                Q7INT.Text = "1";
                Q8DROP.ClearSelection();
                Q8Text.Text = "";
                Q8INT.Text = "1";
                Q9DROP.ClearSelection();
                Q9Text.Text = "";
                Q9INT.Text = "1";
                Q10DROP.ClearSelection();
                Q10Text.Text = "";
                Q10INT.Text = "1";
                Q11DROP.ClearSelection();
                Q11Text.Text = "";
                Q11INT.Text = "1";
                Q12DROP.ClearSelection();
                Q12Text.Text = "";
                Q12INT.Text = "1";
                Q13DROP.ClearSelection();
                Q13Text.Text = "";
                Q13INT.Text = "1";
                Q14DROP.ClearSelection();
                Q14Text.Text = "";
                Q14INT.Text = "1";
                Q15DROP.ClearSelection();
                Q15Text.Text = "";
                Q15INT.Text = "1";
                Q16DROP.ClearSelection();
                Q16Text.Text = "";
                Q16INT.Text = "1";
                Q17DROP.ClearSelection();
                Q17Text.Text = "";
                Q17INT.Text = "1";
                Q18DROP.ClearSelection();
                Q18Text.Text = "";
                Q18INT.Text = "1";
                Q19DROP.ClearSelection();
                Q19Text.Text = "";
                Q19INT.Text = "1";
                Q20DROP.ClearSelection();
                Q20Text.Text = "";
                Q20INT.Text = "1";
                if (lista_trabajos.SelectedValue.ToString() == "Mant. Preventivo") // == "Mant. Preventivo"
                {
                    BloquePreventivo.Visible = true;
                    LabelTipoPreventivo.Visible = true;
                    int IDTipoMantPreventivo = 0;
                  
                       
                    if (molde.Tables[0].Rows[0]["ReferenciaMolde"].ToString() == "0")
                    {
                        IDTipoMantPreventivo = 10;
                    }
                    else
                    {
                        string IDMaquinaCHAR = molde.Tables[0].Rows[0]["ReferenciaMolde"].ToString();
                        IDTipoMantPreventivo = conexion.Devuelve_IDMantenimiento_BMSMOLDE(IDMaquinaCHAR);
                    }

                    if (IDTipoMantPreventivo > 0)
                    {
                        DataSet DropTiposPreventivos = conexion.Devuelve_Listado_PreventivosXMolde(IDTipoMantPreventivo);
                        DropTipoPreventivo.Items.Clear();
                        DropTipoPreventivo.Items.Add("");
                        foreach (DataRow row in DropTiposPreventivos.Tables[0].Rows)
                        {
                            DropTipoPreventivo.Items.Add(row["TipoFrecuencia"].ToString());
                        }
                        DropTipoPreventivo.SelectedValue = conexion.Devuelve_Tipo_Mantenimiento_Seleccionado_PREVMOLDE_DS(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["IDTipoMantPreventivo"].ToString());
                    }
                    else
                    {


                    }


                    DataSet ListaPreguntas = conexion.Devuelve_Listado_Preguntas_PREVMAQ();
                    DataSet CuestionPreventiva = conexion.Devuelve_Tipo_Mantenimiento_Seleccionado_PREVMolde(DropTipoPreventivo.SelectedValue.ToString());
                    TextTipoPreventivo.Text = "MANTENIMIENTO PREVENTIVO: " + DropTipoPreventivo.SelectedValue.ToString();
                    
                                 
                    Q1INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoAcciones"].ToString().Split(' ')[0];

                    Q1DROP.ClearSelection();
                    Q1DROP.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoEstado"].ToString().Split(' ')[0]);
                    ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q1INT.Text + "";
                    DataTable ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                    Q1Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();

                    Q2INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoAcciones"].ToString().Split(' ')[1];
                    if (Q2INT.Text != "1")
                    {
                        Q2LINEA.Visible = true;
                        Q2DROP.ClearSelection();
                        Q2DROP.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoEstado"].ToString().Split(' ')[1]);
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q2INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q2Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }


                    Q3INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoAcciones"].ToString().Split(' ')[2];
                    if (Q3INT.Text != "1")
                    {
                        Q3LINEA.Visible = true;
                        Q3DROP.ClearSelection();
                        Q3DROP.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoEstado"].ToString().Split(' ')[2]);
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q3INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q3Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }


                    Q4INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoAcciones"].ToString().Split(' ')[3];
                    if (Q4INT.Text != "1")
                    {
                        Q4LINEA.Visible = true;
                        Q4DROP.ClearSelection();
                        Q4DROP.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoEstado"].ToString().Split(' ')[3]);
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q4INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q4Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }


                    Q5INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoAcciones"].ToString().Split(' ')[4];
                    if (Q5INT.Text != "1")
                    {
                        Q5LINEA.Visible = true;
                        Q5DROP.ClearSelection();
                        Q5DROP.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoEstado"].ToString().Split(' ')[4]);

                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q5INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q5Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }


                    Q6INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoAcciones"].ToString().Split(' ')[5];
                    if (Q6INT.Text != "1")
                    {
                        Q6LINEA.Visible = true;
                        Q6DROP.ClearSelection();
                        Q6DROP.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoEstado"].ToString().Split(' ')[5]);

                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q6INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q6Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }


                    Q7INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoAcciones"].ToString().Split(' ')[6];
                    if (Q7INT.Text != "1")
                    {
                        Q7LINEA.Visible = true;
                        Q7DROP.ClearSelection();
                        Q7DROP.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoEstado"].ToString().Split(' ')[6]);

                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q7INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q7Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }


                    Q8INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoAcciones"].ToString().Split(' ')[7];
                    if (Q8INT.Text != "1")
                    {
                        Q8LINEA.Visible = true;
                        Q8DROP.ClearSelection();
                        Q8DROP.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoEstado"].ToString().Split(' ')[7]);

                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q8INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q8Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }


                    Q9INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoAcciones"].ToString().Split(' ')[8];
                    if (Q9INT.Text != "1")
                    {
                        Q9LINEA.Visible = true;
                        Q9DROP.ClearSelection();
                        Q9DROP.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoEstado"].ToString().Split(' ')[8]);

                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q9INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q9Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }


                    Q10INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoAcciones"].ToString().Split(' ')[9];
                    if (Q10INT.Text != "1")
                    {
                        Q10LINEA.Visible = true;
                        Q10DROP.ClearSelection();
                        Q10DROP.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoEstado"].ToString().Split(' ')[9]);

                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q10INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q10Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }


                    Q11INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoAcciones"].ToString().Split(' ')[10];
                    if (Q11INT.Text != "1")
                    {
                        Q11LINEA.Visible = true;
                        Q11DROP.ClearSelection();
                        Q11DROP.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoEstado"].ToString().Split(' ')[10]);

                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q11INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q11Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }

                    Q12INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoAcciones"].ToString().Split(' ')[11];
                    if (Q12INT.Text != "1")
                    {
                        Q12LINEA.Visible = true;
                        Q12DROP.ClearSelection();
                        Q12DROP.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoEstado"].ToString().Split(' ')[11]);

                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q12INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q12Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }


                    Q13INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoAcciones"].ToString().Split(' ')[12];
                    if (Q13INT.Text != "1")
                    {
                        Q13LINEA.Visible = true;
                        Q13DROP.ClearSelection();
                        Q13DROP.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoEstado"].ToString().Split(' ')[12]);

                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q13INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q13Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }

                    Q14INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoAcciones"].ToString().Split(' ')[13];
                    if (Q14INT.Text != "1")
                    {
                        Q14LINEA.Visible = true;
                        Q14DROP.ClearSelection();
                        Q14DROP.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoEstado"].ToString().Split(' ')[13]);

                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q14INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q14Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }


                    Q15INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoAcciones"].ToString().Split(' ')[14];
                    if (Q15INT.Text != "1")
                    {
                        Q15LINEA.Visible = true;
                        Q15DROP.ClearSelection();
                        Q15DROP.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoEstado"].ToString().Split(' ')[14]);

                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q15INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q15Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }


                    Q16INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoAcciones"].ToString().Split(' ')[15];
                    if (Q16INT.Text != "1")
                    {
                        Q16LINEA.Visible = true;
                        Q16DROP.ClearSelection();
                        Q16DROP.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoEstado"].ToString().Split(' ')[15]);

                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q16INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q16Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }


                    Q17INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoAcciones"].ToString().Split(' ')[16];
                    if (Q17INT.Text != "1")
                    {
                        Q17LINEA.Visible = true;
                        Q17DROP.ClearSelection();
                        Q17DROP.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoEstado"].ToString().Split(' ')[16]);

                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q17INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q17Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }


                    Q18INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoAcciones"].ToString().Split(' ')[17];
                    if (Q18INT.Text != "1")
                    {
                        Q18LINEA.Visible = true;
                        Q18DROP.ClearSelection();
                        Q18DROP.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoEstado"].ToString().Split(' ')[17]);

                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q18INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q18Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }


                    Q19INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoAcciones"].ToString().Split(' ')[18];
                    if (Q19INT.Text != "1")
                    {
                        Q19LINEA.Visible = true;
                        Q19DROP.ClearSelection();
                        Q19DROP.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoEstado"].ToString().Split(' ')[18]);

                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q19INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q19Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }


                    Q20INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoAcciones"].ToString().Split(' ')[19];
                    if (Q20INT.Text != "1")
                    {
                        Q20LINEA.Visible = true;
                        Q20DROP.ClearSelection();
                        Q20DROP.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoEstado"].ToString().Split(' ')[19]);

                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q20INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q20Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }
                } //lista trabajos = "Mant.Preventivo"



                //REPARACION

                Horasprevistas.Value = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["HorasEstimadasReparacion"].ToString();
                datetime_ini2.Value = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["FechaInicioReparacion"].ToString();
                lista_reparacion.ClearSelection();
                lista_reparacion.SelectedValue = SHConexion.Devuelve_TipoReparacion(Convert.ToInt16(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["IdTiposReparacion"].ToString()));
                ColAsignado2.Visible = false;
                ColAsignado3.Visible = false;
                Asignado1.Value = SHConexion.Devuelve_Pilotos_SMARTH(Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["AsignadoA"].ToString().Split(' ')[0]));
                Asignado2.Value = SHConexion.Devuelve_Pilotos_SMARTH(Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["AsignadoA"].ToString().Split(' ')[1]));
                if (Asignado2.Value != "-") { ColAsignado2.Visible = true; }
                Asignado3.Value = SHConexion.Devuelve_Pilotos_SMARTH(Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["AsignadoA"].ToString().Split(' ')[2]));
                if (Asignado3.Value != "-") { ColAsignado3.Visible = true; }

                reparacion.Value = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["Reparacion"].ToString();
                observaciones.Value = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["Observaciones"].ToString();

                BorrarReparadoPor2.Visible = false;
                ReparadoPor2.Visible = false;
                horas2.Visible = false;
                BorrarReparadoPor3.Visible = false;
                ReparadoPor3.Visible = false;
                horas3.Visible = false;

                ReparadoPor1.Value = SHConexion.Devuelve_Pilotos_SMARTH(Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["ReparadoPorOP"].ToString().Split(' ')[0]));
                ReparadoPor2.Value = SHConexion.Devuelve_Pilotos_SMARTH(Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["ReparadoPorOP"].ToString().Split(' ')[1]));
                if (ReparadoPor2.Value != "-") { BorrarReparadoPor2.Visible = true; ReparadoPor2.Visible = true; horas2.Visible = true; }
                ReparadoPor3.Value = SHConexion.Devuelve_Pilotos_SMARTH(Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["ReparadoPorOP"].ToString().Split(' ')[2]));
                if (ReparadoPor3.Value != "-") { BorrarReparadoPor3.Visible = true; ReparadoPor3.Visible = true; horas3.Visible = true; }
                horas1.Value = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["HorasRealesRepOP1"].ToString();
                horas2.Value = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["HorasRealesRepOP2"].ToString();
                horas3.Value = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["HorasRealesRepOP3"].ToString();

                TbCostes.Value = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["ImporteEmpresa1"].ToString();
                TbCostesTotales.Value = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["ImporteEmpreza3"].ToString();


                //lista_realizadoPor.ClearSelection();
                //lista_realizadoPor.SelectedValue = conexion.devuelve_opMantenimiento(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["ReparadoPorOP"].ToString());
                datetime_rep2.Value = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["FechaFinalizacionReparacion"].ToString();
                date_revision2.Value = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["FechaRevision"].ToString();


                //REVISION

                if ((bool)ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["Revisado"])
                {
                    //revisado_label.Attributes.Add("class", "btn btn2 btn-success2");
                    revisado.Attributes.Add("class", "form-check-input ms-2 bg-success");
                    revisado.Checked = true;
                    revisado_ant = 1;
                }
                else
                {
                    //revisado_label.Attributes.Add("class", "btn btn2 btn-success2 active");
                    revisado.Attributes.Add("class", "form-check-input ms-2 bg-danger");
                    revisado.Checked = false;
                    revisado_ant = 0;
                }
                if ((bool)ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["RevisadoNOK"])
                {
                    revisadoNOK.Attributes.Add("class", "form-check-input ms-2 bg-success");
                    //revisadoNOK_label.Attributes.Add("class", "btn btn2 btn-success2");
                    revisadoNOK.Checked = true;
                    revisadoNOK_ant = 1;
                }
                else
                {
                    revisadoNOK.Attributes.Add("class", "form-check-input ms-2 bg-danger");
                    //revisadoNOK_label.Attributes.Add("class", "btn btn2 btn-success2 active");
                    revisadoNOK.Checked = false;
                    revisadoNOK_ant = 0;
                }
                
                string RevisadoPor = SHConexion.Devuelve_Pilotos_SMARTH(Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["RevisadoPor"].ToString()));
                if (RevisadoPor != "")
                {
                    revisado_por.SelectedValue = RevisadoPor;
                }
                observaciones_revision.Value = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["ObservacionesRevision"].ToString();
       
            }
            catch (Exception ex)
            {
                mandar_mail2(ex.Message+ex.StackTrace, "Error en la aplicación de mantenimiento");
            }
        }

        public void Cargar_parte(DataSet ds)
        {
            try
            {
               
                Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();
                Conexion_SMARTH SHconexion = new Conexion_SMARTH();


                //CARGA Y RELLENA DESPLEGABLES
                DataSet moldes = conexion.Devuelve_lista_moldes();
                DataSet molde = conexion.Devuelve_molde(Convert.ToInt32(ds.Tables[0].Rows[0]["IDMoldes"].ToString()));
                RellenarDropDowns();

                //BARRA DE ESTADO
                int estadoreparado = Convert.ToInt32(ds.Tables[0].Rows[0]["Terminado"]);
                int estadovalidado = Convert.ToInt32(ds.Tables[0].Rows[0]["Revisado"]);
                int estadovalidadonok = Convert.ToInt32(ds.Tables[0].Rows[0]["RevisadoNOK"]);
                if (estadoreparado == 0 && estadovalidado == 0 && estadovalidadonok == 0 && DropEstado.SelectedValue == "0") //pendiente
                {
                    progressERROR.Visible = false;
                    progressABIERTO.Visible = true;
                    progressABIERTOINI.Visible = false;
                    progressPENDIENTE.Visible = false;
                    progressNOCONFORME.Visible = false;
                    progressREPARADO.Visible = false;
                }
                if (estadoreparado == 0 && estadovalidado == 0 && estadovalidadonok == 0 && DropEstado.SelectedValue == "1") //pendiente iniciado
                {
                    progressERROR.Visible = false;
                    progressABIERTO.Visible = false;
                    progressABIERTOINI.Visible = true;
                    progressPENDIENTE.Visible = false;
                    progressNOCONFORME.Visible = false;
                    progressREPARADO.Visible = false;
                }
                if (estadoreparado == 0 && (estadovalidado == 1 || estadovalidadonok == 1)) //error
                {
                    progressERROR.Visible = true;
                    progressABIERTO.Visible = false;
                    progressABIERTOINI.Visible = false;
                    progressPENDIENTE.Visible = false;
                    progressNOCONFORME.Visible = false;
                    progressREPARADO.Visible = false;
                }
                if (estadoreparado == 1 && estadovalidado == 1 && estadovalidadonok == 1) //error
                {
                    progressERROR.Visible = true;
                    progressABIERTO.Visible = false;
                    progressABIERTOINI.Visible = false;
                    progressPENDIENTE.Visible = false;
                    progressNOCONFORME.Visible = false;
                    progressREPARADO.Visible = false;
                }
                if (estadoreparado == 1 && estadovalidado == 0 && estadovalidadonok == 0) //pendiente
                {
                    progressERROR.Visible = false;
                    progressABIERTO.Visible = false;
                    progressPENDIENTE.Visible = true;
                    progressNOCONFORME.Visible = false;
                    progressREPARADO.Visible = false;
                }
                if (estadoreparado == 1 && estadovalidado == 1 && estadovalidadonok == 0) //reparado
                {
                    progressERROR.Visible = false;
                    progressABIERTO.Visible = false;
                    progressABIERTOINI.Visible = false;
                    progressPENDIENTE.Visible = false;
                    progressNOCONFORME.Visible = false;
                    progressREPARADO.Visible = true;
                }
                if (estadoreparado == 1 && estadovalidado == 0 && estadovalidadonok == 1)//noconforme
                {
                    progressERROR.Visible = false;
                    progressABIERTO.Visible = false;
                    progressABIERTOINI.Visible = false;
                    progressPENDIENTE.Visible = false;
                    progressNOCONFORME.Visible = true;
                    progressREPARADO.Visible = false;
                }


                //REGISTRO
                HiddenNuevo.Value = "0";
                lista_partes.Value = ds.Tables[0].Rows[0]["IdReparacionMolde"].ToString();
                prioridad.ClearSelection();
                prioridad.SelectedValue = SHconexion.Devuelve_prioridad_parte(Convert.ToInt16(ds.Tables[0].Rows[0]["IdPrioridad"].ToString()));

                //si es 0
                tbMoldeNew.Value = SHconexion.Devuelve_linea_MOLDES(molde.Tables[0].Rows[0]["ReferenciaMolde"].ToString()).Rows[0]["MOLDE"].ToString();
                DataSet ProximaProduccion = conexion.Devuelve_Proxima_Produccion_Molde(tbMoldeNew.Value.Split('¬')[0].ToString());
                if (ProximaProduccion.Tables[0].Rows.Count == 0)
                {
                    TbProxProduccion.Value = "No planificado";
                }
                else if (ProximaProduccion.Tables[0].Rows[0]["C_SEQNR"].ToString() == "0")
                {
                    TbProxProduccion.Value = "Produciendo";
                }
                else
                { 
                    TbProxProduccion.Value = ProximaProduccion.Tables[0].Rows[0]["C_CALCSTARTDATE"].ToString();
                }

                if (tbMoldeNew.Value != "")
                {
                    DataSet ds_listahistorico = conexion.Devuelve_historico_Molde(" AND IDMoldes = '" + tbMoldeNew.Value.Split('¬')[0].ToString() + "'");
                    dgv_ListadoHistorico.DataSource = ds_listahistorico;
                    dgv_ListadoHistorico.DataBind();
                }

                lista_trabajos.ClearSelection();
                lista_trabajos.SelectedValue = SHconexion.Devuelve_TipoMantenimiento(Convert.ToInt16(ds.Tables[0].Rows[0]["IdTipoRevision"].ToString()));
                averia.Value = ds.Tables[0].Rows[0]["MotivoAveria"].ToString();

                string imagen1 = conexion.Devuelve_imagenes(ds.Tables[0].Rows[0]["IdReparacionMolde"].ToString(), 1);
                string imagen2 = conexion.Devuelve_imagenes(ds.Tables[0].Rows[0]["IdReparacionMolde"].ToString(), 2);
                string imagen3 = conexion.Devuelve_imagenes(ds.Tables[0].Rows[0]["IdReparacionMolde"].ToString(), 3);
                    if (imagen1 != "")
                    {
                        img1.ImageUrl = imagen1;
                    }
                    else
                    {
                        img1.ImageUrl = "../SMARTH_docs/MANTENIMIENTO/sin_imagen.jpg";
                
                    }
                    if (imagen2 != "")
                    {
                        img2.ImageUrl = imagen2;
                    }
                    else
                    {
                        img2.ImageUrl = "../SMARTH_docs/MANTENIMIENTO/sin_imagen.jpg";
                    }
                    

                datetimepicker.Value = ds.Tables[0].Rows[0]["FechaAveria"].ToString();
                encargado.ClearSelection();
                string EncargadoSelect = SHconexion.Devuelve_Pilotos_SMARTH(Convert.ToInt32(ds.Tables[0].Rows[0]["IdEncargado"].ToString()));
                if (EncargadoSelect != "")
                {
                    encargado.SelectedValue = EncargadoSelect;                   
                }
                ubicacion.ClearSelection();
                ubicacion.SelectedValue = SHconexion.Devuelve_ubicacion(Convert.ToInt16(ds.Tables[0].Rows[0]["IdUbicacion"].ToString()));

                    
                //BlOQUE SELECCION PREVENTIVO (CARGA DE DATASET Y DROPDOWN)

                BloquePreventivo.Visible = false;
                LabelTipoPreventivo.Visible = false;
                DropTipoPreventivo.Items.Clear();
                Q1DROP.ClearSelection();
                Q1Text.Text = "";
                Q1INT.Text = "1";
                Q2DROP.ClearSelection();
                Q2Text.Text = "";
                Q2INT.Text = "1";
                Q3DROP.ClearSelection();
                Q3Text.Text = "";
                Q3INT.Text = "1";
                Q4DROP.ClearSelection();
                Q4Text.Text = "";
                Q4INT.Text = "1";
                Q5DROP.ClearSelection();
                Q5Text.Text = "";
                Q5INT.Text = "1";
                Q6DROP.ClearSelection();
                Q6Text.Text = "";
                Q6INT.Text = "1";
                Q7DROP.ClearSelection();
                Q7Text.Text = "";
                Q7INT.Text = "1";
                Q8DROP.ClearSelection();
                Q8Text.Text = "";
                Q8INT.Text = "1";
                Q9DROP.ClearSelection();
                Q9Text.Text = "";
                Q9INT.Text = "1";
                Q10DROP.ClearSelection();
                Q10Text.Text = "";
                Q10INT.Text = "1";
                Q11DROP.ClearSelection();
                Q11Text.Text = "";
                Q11INT.Text = "1";
                Q12DROP.ClearSelection();
                Q12Text.Text = "";
                Q12INT.Text = "1";
                Q13DROP.ClearSelection();
                Q13Text.Text = "";
                Q13INT.Text = "1";
                Q14DROP.ClearSelection();
                Q14Text.Text = "";
                Q14INT.Text = "1";
                Q15DROP.ClearSelection();
                Q15Text.Text = "";
                Q15INT.Text = "1";
                Q16DROP.ClearSelection();
                Q16Text.Text = "";
                Q16INT.Text = "1";
                Q17DROP.ClearSelection();
                Q17Text.Text = "";
                Q17INT.Text = "1";
                Q18DROP.ClearSelection();
                Q18Text.Text = "";
                Q18INT.Text = "1";
                Q19DROP.ClearSelection();
                Q19Text.Text = "";
                Q19INT.Text = "1";
                Q20DROP.ClearSelection();
                Q20Text.Text = "";
                Q20INT.Text = "1";
                if (lista_trabajos.SelectedValue.ToString() == "Mant. Preventivo")
                {
                    LabelTipoPreventivo.Visible = true;
                    BloquePreventivo.Visible = true;
                    int IDTipoMantPreventivo = 0;
                    if (molde.Tables[0].Rows[0]["ReferenciaMolde"].ToString() == "0")
                    {
                        IDTipoMantPreventivo = 10;
                    }
                    else
                    {
                        string IDMaquinaCHAR = molde.Tables[0].Rows[0]["ReferenciaMolde"].ToString();
                        IDTipoMantPreventivo = conexion.Devuelve_IDMantenimiento_BMSMOLDE(IDMaquinaCHAR);
                    }
                    //string IDMaquinaCHAR = conexion.devuelve_IDmaquinaCHAR(lista_maquinas.SelectedValue.ToString());
                    //int IDTipoMantPreventivo = conexion.Devuelve_IDMantenimiento_BMS(IDMaquinaCHAR);
                    if (IDTipoMantPreventivo > 0)
                    {
                        DataSet DropTiposPreventivos = conexion.Devuelve_Listado_PreventivosXMolde(IDTipoMantPreventivo);
                        DropTipoPreventivo.Items.Clear();
                        DropTipoPreventivo.Items.Add("");
                        foreach (DataRow row in DropTiposPreventivos.Tables[0].Rows)
                        {
                            DropTipoPreventivo.Items.Add(row["TipoFrecuencia"].ToString());
                        }
                        DropTipoPreventivo.SelectedValue = conexion.Devuelve_Tipo_Mantenimiento_Seleccionado_PREVMOLDE_DS(ds.Tables[0].Rows[0]["IDTipoMantPreventivo"].ToString());
                    }
                    else
                    {


                    }


                    DataSet ListaPreguntas = conexion.Devuelve_Listado_Preguntas_PREVMAQ();
                    DataSet CuestionPreventiva = conexion.Devuelve_Tipo_Mantenimiento_Seleccionado_PREVMolde(DropTipoPreventivo.SelectedValue.ToString());
                    TextTipoPreventivo.Text = "MANTENIMIENTO PREVENTIVO: " + DropTipoPreventivo.SelectedValue.ToString();

                    //string AuxCuestion = ds.Tables[0].Rows[0]["PreventivoAcciones"].ToString().Split(' ')[0];
                    Q1INT.Text = ds.Tables[0].Rows[0]["PreventivoAcciones"].ToString().Split(' ')[0];
                    Q1DROP.ClearSelection();
                    Q1DROP.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[0]["PreventivoEstado"].ToString().Split(' ')[0]);
                    ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q1INT.Text + "";
                    DataTable ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                    Q1Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();


                    Q2INT.Text = ds.Tables[0].Rows[0]["PreventivoAcciones"].ToString().Split(' ')[1];

                    if (Q2INT.Text != "1")
                    {
                        Q2LINEA.Visible = true;
                        Q2DROP.ClearSelection();
                        Q2DROP.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[0]["PreventivoEstado"].ToString().Split(' ')[1]);
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q2INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q2Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }

                    Q3INT.Text = ds.Tables[0].Rows[0]["PreventivoAcciones"].ToString().Split(' ')[2];
                    if (Q3INT.Text != "1")
                    {
                        Q3LINEA.Visible = true;
                        Q3DROP.ClearSelection();
                        Q3DROP.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[0]["PreventivoEstado"].ToString().Split(' ')[2]);
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q3INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q3Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }

                    Q4INT.Text = ds.Tables[0].Rows[0]["PreventivoAcciones"].ToString().Split(' ')[3];
                    if (Q4INT.Text != "1")
                    {
                        Q4LINEA.Visible = true;
                        Q4DROP.ClearSelection();
                        Q4DROP.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[0]["PreventivoEstado"].ToString().Split(' ')[3]);
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q4INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q4Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }

                    Q5INT.Text = ds.Tables[0].Rows[0]["PreventivoAcciones"].ToString().Split(' ')[4];
                    if (Q5INT.Text != "1")
                    {
                        Q5LINEA.Visible = true;
                        Q5DROP.ClearSelection();
                        Q5DROP.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[0]["PreventivoEstado"].ToString().Split(' ')[4]);
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q5INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q5Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }

                    Q6INT.Text = ds.Tables[0].Rows[0]["PreventivoAcciones"].ToString().Split(' ')[5];
                    if (Q6INT.Text != "1")
                    {
                        Q6LINEA.Visible = true;
                        Q6DROP.ClearSelection();
                        Q6DROP.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[0]["PreventivoEstado"].ToString().Split(' ')[5]);
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q6INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q6Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }


                    Q7INT.Text = ds.Tables[0].Rows[0]["PreventivoAcciones"].ToString().Split(' ')[6];
                    if (Q7INT.Text != "1")
                    {
                        Q7LINEA.Visible = true;
                        Q7DROP.ClearSelection();
                        Q7DROP.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[0]["PreventivoEstado"].ToString().Split(' ')[6]);
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q7INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q7Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }


                    Q8INT.Text = ds.Tables[0].Rows[0]["PreventivoAcciones"].ToString().Split(' ')[7];
                    if (Q8INT.Text != "1")
                    {
                        Q8LINEA.Visible = true;
                        Q8DROP.ClearSelection();
                        Q8DROP.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[0]["PreventivoEstado"].ToString().Split(' ')[7]);
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q8INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q8Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }


                    Q9INT.Text = ds.Tables[0].Rows[0]["PreventivoAcciones"].ToString().Split(' ')[8];
                    if (Q9INT.Text != "1")
                    {
                        Q9LINEA.Visible = true;
                        Q9DROP.ClearSelection();
                        Q9DROP.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[0]["PreventivoEstado"].ToString().Split(' ')[8]);
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q9INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q9Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }


                    Q10INT.Text = ds.Tables[0].Rows[0]["PreventivoAcciones"].ToString().Split(' ')[9];
                    if (Q10INT.Text != "1")
                    {
                        Q10LINEA.Visible = true;
                        Q10DROP.ClearSelection();
                        Q10DROP.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[0]["PreventivoEstado"].ToString().Split(' ')[9]);
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q10INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q10Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }


                    Q11INT.Text = ds.Tables[0].Rows[0]["PreventivoAcciones"].ToString().Split(' ')[10];
                    if (Q11INT.Text != "1")
                    {
                        Q11LINEA.Visible = true;
                        Q11DROP.ClearSelection();
                        Q11DROP.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[0]["PreventivoEstado"].ToString().Split(' ')[10]);
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q11INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q11Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }


                    Q12INT.Text = ds.Tables[0].Rows[0]["PreventivoAcciones"].ToString().Split(' ')[11];
                    if (Q12INT.Text != "1")
                    {
                        Q12LINEA.Visible = true;
                        Q12DROP.ClearSelection();
                        Q12DROP.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[0]["PreventivoEstado"].ToString().Split(' ')[11]);
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q12INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q12Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }


                    Q13INT.Text = ds.Tables[0].Rows[0]["PreventivoAcciones"].ToString().Split(' ')[12];
                    if (Q13INT.Text != "1")
                    {
                        Q13LINEA.Visible = true;
                        Q13DROP.ClearSelection();
                        Q13DROP.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[0]["PreventivoEstado"].ToString().Split(' ')[12]);
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q13INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q13Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }


                    Q14INT.Text = ds.Tables[0].Rows[0]["PreventivoAcciones"].ToString().Split(' ')[13];
                    if (Q14INT.Text != "1")
                    {
                        Q14LINEA.Visible = true;
                        Q14DROP.ClearSelection();
                        Q14DROP.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[0]["PreventivoEstado"].ToString().Split(' ')[13]);
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q14INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q14Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }


                    Q15INT.Text = ds.Tables[0].Rows[0]["PreventivoAcciones"].ToString().Split(' ')[14];
                    if (Q15INT.Text != "1")
                    {
                        Q15LINEA.Visible = true;
                        Q15DROP.ClearSelection();
                        Q15DROP.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[0]["PreventivoEstado"].ToString().Split(' ')[14]);
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q15INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q15Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }


                    Q16INT.Text = ds.Tables[0].Rows[0]["PreventivoAcciones"].ToString().Split(' ')[15];
                    if (Q16INT.Text != "1")
                    {
                        Q16LINEA.Visible = true;
                        Q16DROP.ClearSelection();
                        Q16DROP.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[0]["PreventivoEstado"].ToString().Split(' ')[15]);
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q16INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q16Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }


                    Q17INT.Text = ds.Tables[0].Rows[0]["PreventivoAcciones"].ToString().Split(' ')[16];
                    if (Q17INT.Text != "1")
                    {
                        Q17LINEA.Visible = true;
                        Q17DROP.ClearSelection();
                        Q17DROP.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[0]["PreventivoEstado"].ToString().Split(' ')[16]);
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q17INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q17Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }


                    Q18INT.Text = ds.Tables[0].Rows[0]["PreventivoAcciones"].ToString().Split(' ')[17];
                    if (Q18INT.Text != "1")
                    {
                        Q18LINEA.Visible = true;
                        Q18DROP.ClearSelection();
                        Q18DROP.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[0]["PreventivoEstado"].ToString().Split(' ')[17]);
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q18INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q18Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }


                    Q19INT.Text = ds.Tables[0].Rows[0]["PreventivoAcciones"].ToString().Split(' ')[18];
                    if (Q19INT.Text != "1")
                    {
                        Q19LINEA.Visible = true;
                        Q19DROP.ClearSelection();
                        Q19DROP.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[0]["PreventivoEstado"].ToString().Split(' ')[18]);
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q19INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q19Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }

                    Q20INT.Text = ds.Tables[0].Rows[0]["PreventivoAcciones"].ToString().Split(' ')[19];
                    if (Q20INT.Text != "1")
                    {
                        Q20LINEA.Visible = true;
                        Q20DROP.ClearSelection();
                        Q20DROP.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[0]["PreventivoEstado"].ToString().Split(' ')[19]);
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q20INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q20Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }
                } //lista trabajos = "Mant.Preventivo"


                //REPARACION
                Horasprevistas.Value = ds.Tables[0].Rows[0]["HorasEstimadasReparacion"].ToString();
                datetime_ini2.Value = ds.Tables[0].Rows[0]["FechaInicioReparacion"].ToString();
                lista_reparacion.ClearSelection();
                lista_reparacion.SelectedValue = SHconexion.Devuelve_TipoReparacion(Convert.ToInt16(ds.Tables[0].Rows[0]["IdTiposReparacion"].ToString()));
                ColAsignado2.Visible = false;
                ColAsignado3.Visible = false;
                Asignado1.Value = SHconexion.Devuelve_Pilotos_SMARTH(Convert.ToInt32(ds.Tables[0].Rows[0]["AsignadoA"].ToString().Split(' ')[0]));
                Asignado2.Value = SHconexion.Devuelve_Pilotos_SMARTH(Convert.ToInt32(ds.Tables[0].Rows[0]["AsignadoA"].ToString().Split(' ')[1]));
                if (Asignado2.Value != "-") { ColAsignado2.Visible = true; }
                Asignado3.Value = SHconexion.Devuelve_Pilotos_SMARTH(Convert.ToInt32(ds.Tables[0].Rows[0]["AsignadoA"].ToString().Split(' ')[2]));
                if (Asignado3.Value != "-") { ColAsignado3.Visible = true; }



                reparacion.Value = ds.Tables[0].Rows[0]["Reparacion"].ToString();
                observaciones.Value = ds.Tables[0].Rows[0]["Observaciones"].ToString();

                BorrarReparadoPor2.Visible = false;
                ReparadoPor2.Visible = false;
                horas2.Visible = false;
                BorrarReparadoPor3.Visible = false;
                ReparadoPor3.Visible = false;
                horas3.Visible = false;

                ReparadoPor1.Value = SHconexion.Devuelve_Pilotos_SMARTH(Convert.ToInt32(ds.Tables[0].Rows[0]["ReparadoPorOP"].ToString().Split(' ')[0]));
                ReparadoPor2.Value = SHconexion.Devuelve_Pilotos_SMARTH(Convert.ToInt32(ds.Tables[0].Rows[0]["ReparadoPorOP"].ToString().Split(' ')[1]));
                if (ReparadoPor2.Value != "-") { BorrarReparadoPor2.Visible = true; ReparadoPor2.Visible = true; horas2.Visible = true; }
                ReparadoPor3.Value = SHconexion.Devuelve_Pilotos_SMARTH(Convert.ToInt32(ds.Tables[0].Rows[0]["ReparadoPorOP"].ToString().Split(' ')[2]));
                if (ReparadoPor3.Value != "-") { BorrarReparadoPor3.Visible = true; ReparadoPor3.Visible = true; horas3.Visible = true; }
                horas1.Value = ds.Tables[0].Rows[0]["HorasRealesRepOP1"].ToString();
                horas2.Value = ds.Tables[0].Rows[0]["HorasRealesRepOP2"].ToString();
                horas3.Value = ds.Tables[0].Rows[0]["HorasRealesRepOP3"].ToString();

                TbCostes.Value = ds.Tables[0].Rows[0]["ImporteEmpresa1"].ToString();
                TbCostesTotales.Value = ds.Tables[0].Rows[0]["ImporteEmpreza3"].ToString();

                //lista_realizadoPor.ClearSelection();
                //lista_realizadoPor.SelectedValue = conexion.devuelve_opMantenimiento(ds.Tables[0].Rows[0]["IdRealizadoPor"].ToString());
                datetime_rep2.Value = ds.Tables[0].Rows[0]["FechaFinalizacionReparacion"].ToString();
               

                if ((bool)ds.Tables[0].Rows[0]["Terminado"])
                {
                    //terminado.Attributes.Add("class", "btn btn2 btn-success2");
                    reparado.Attributes.Add("class", "form-check-input ms-2 bg-success");
                    reparado.Checked = true;
                    reparado_ant = 1;
                }
                else
                {
                    //terminado.Attributes.Add("class", "btn btn2 btn-success2 active");
                    
                    reparado.Attributes.Add("class", "form-check-input ms-2 bg-danger");
                    reparado.Checked = false;
                    reparado_ant = 0;
                }


                //REVISION
                if ((bool)ds.Tables[0].Rows[0]["Revisado"])
                {
                    //revisado_label.Attributes.Add("class", "btn btn2 btn-success2");
                    revisado.Attributes.Add("class", "form-check-input ms-2 bg-success");
                    revisado.Checked = true;
                    revisado_ant = 1;
                }
                else
                {
                    //revisado_label.Attributes.Add("class", "btn btn2 btn-success2 active");
                    revisado.Attributes.Add("class", "form-check-input ms-2 bg-danger");
                    revisado.Checked = false;
                    revisado_ant = 0;
                }
                if ((bool)ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["RevisadoNOK"])
                {
                    //revisadoNOK_label.Attributes.Add("class", "btn btn2 btn-success2");
                    revisadoNOK.Attributes.Add("class", "form-check-input ms-2 bg-success");
                    revisadoNOK.Checked = true;
                    revisadoNOK_ant = 1;
                }
                else
                {
                    //revisadoNOK_label.Attributes.Add("class", "btn btn2 btn-success2 active");
                    revisadoNOK.Attributes.Add("class", "form-check-input ms-2 bg-danger");
                    revisadoNOK.Checked = false;
                    revisadoNOK_ant = 0;
                }                            
                string RevisadoPor = SHconexion.Devuelve_Pilotos_SMARTH(Convert.ToInt32(ds.Tables[0].Rows[0]["RevisadoPor"].ToString()));
                if (RevisadoPor != "")
                {
                    revisado_por.SelectedValue = RevisadoPor;
                }
              
                date_revision2.Value = ds.Tables[0].Rows[0]["FechaRevision"].ToString();
                observaciones_revision.Value = ds.Tables[0].Rows[0]["ObservacionesRevision"].ToString();
        

                      
            }
            catch (Exception ex)
            {
                mandar_mail2(ex.Message+ex.StackTrace, "Error en la aplicación de mantenimiento");
            }
        }
   
        public void GuardarParte(Object sender, EventArgs e)
        {
            try
            {
                
                Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();
                Conexion_SMARTH SHconexion = new Conexion_SMARTH();
                string fecha_aux = datetimepicker.Value;
                string fecha_aux2 = datetime_ini2.Value;
                string fecha_aux3 = datetime_rep2.Value;
                string fecha_aux4 = date_revision2.Value;
                string[] fecha_apertura = fecha_aux.Split(' ');
                    if (fecha_apertura[0] == "")
                    {
                        fecha_apertura[0] = DateTime.Now.ToString("dd/MM/yyy");
                    }
                string[] fecha_ini = fecha_aux2.Split(' ');
                string[] fecha_rep = fecha_aux3.Split(' ');
                string[] fecha_rev = fecha_aux4.Split(' ');

               
                double horas_previstas = 0.0;
                if (Horasprevistas.Value.ToString() != "")
                {
                    horas_previstas = Convert.ToDouble(Horasprevistas.Value.ToString());
                }

                double horas_ok1 = 0.0;
                double horas_ok2 = 0.0;
                double horas_ok3 = 0.0;

                if (horas1.Value.ToString() != "")
                {
                    horas_ok1 = Convert.ToDouble(horas1.Value.ToString());
                }
                if (horas2.Value.ToString() != "")
                {
                    horas_ok2 = Convert.ToDouble(horas2.Value.ToString());
                }
                if (horas3.Value.ToString() != "")
                {
                    horas_ok3 = Convert.ToDouble(horas3.Value.ToString());
                }
                double horas_reales = horas_ok1 + horas_ok2 + horas_ok3;

                int chek_reparado = 0;
                int chek_revisado = 0;
                int chek_revisadoNOK = 0;

                double CosteRepuestos = 0.0;
                if (TbCostes.Value.ToString() != "")
                {
                    CosteRepuestos = Convert.ToDouble(TbCostes.Value.ToString());
                }

                double coste_horas = 0.0;
                if (horas1.Value.ToString() != "")
                {
                    coste_horas = (horas_reales * 21.5);
                }

                double CosteTotales = CosteRepuestos + coste_horas;

                string AsignadoA = "2 2 2";
                AsignadoA = SHconexion.Devuelve_ID_Piloto_SMARTH(Asignado1.Value).ToString() + " " + SHconexion.Devuelve_ID_Piloto_SMARTH(Asignado2.Value).ToString() + " " + SHconexion.Devuelve_ID_Piloto_SMARTH(Asignado3.Value).ToString();
                string ReparadoPor = "2 2 2";
                ReparadoPor = SHconexion.Devuelve_ID_Piloto_SMARTH(ReparadoPor1.Value).ToString() + " " + SHconexion.Devuelve_ID_Piloto_SMARTH(ReparadoPor2.Value).ToString() + " " + SHconexion.Devuelve_ID_Piloto_SMARTH(ReparadoPor3.Value).ToString();
                int EstadoReparacion = Convert.ToInt32(DropEstado.SelectedValue);

                int IDTipoPreventivo = conexion.Devuelve_ID_Tipo_Mantenimiento_Seleccionado_PREVMOLDE_DS(DropTipoPreventivo.SelectedValue.ToString());
                string PreventivoAcciones = Q1INT.Text + " " + Q2INT.Text + " " + Q3INT.Text + " " + Q4INT.Text + " " + Q5INT.Text + " " + Q6INT.Text + " " + Q7INT.Text + " " + Q8INT.Text + " " + Q9INT.Text + " " + Q10INT.Text + " " + Q11INT.Text + " " + Q12INT.Text + " " + Q13INT.Text + " " + Q14INT.Text + " " + Q15INT.Text + " " + Q16INT.Text + " " + Q17INT.Text + " " + Q18INT.Text + " " + Q19INT.Text + " " + Q20INT.Text;
                string PreventivoEstado = Q1DROP.SelectedValue + " " + Q2DROP.SelectedValue + " " + Q3DROP.SelectedValue + " " + Q4DROP.SelectedValue + " " + Q5DROP.SelectedValue + " " + Q6DROP.SelectedValue + " " + Q7DROP.SelectedValue + " " + Q8DROP.SelectedValue + " " + Q9DROP.SelectedValue + " " + Q10DROP.SelectedValue + " " + Q11DROP.SelectedValue + " " + Q12DROP.SelectedValue + " " + Q13DROP.SelectedValue + " " + Q14DROP.SelectedValue + " " + Q15DROP.SelectedValue + " " + Q16DROP.SelectedValue + " " + Q17DROP.SelectedValue + " " + Q18DROP.SelectedValue + " " + Q19DROP.SelectedValue + " " + Q20DROP.SelectedValue;
                
                //Parcheo que se hayan creado partes nuevos mientras tanto
                if (HiddenNuevo.Value == "1")
                {
                    lista_partes.Value = Convert.ToString(conexion.max_idParte() + 1);
                    HiddenNuevo.Value = "0";
                }

                if (!conexion.Existe_parte(lista_partes.Value.ToString()))
                {
                    conexion.insertar_parte(Convert.ToInt16(lista_partes.Value), conexion.Devuelve_IDMolde(tbMoldeNew.Value.Split('¬')[0].ToString()), SHconexion.Devuelve_IDTrabajo(lista_trabajos.SelectedValue.ToString()),
                                        SHconexion.Devuelve_ID_Piloto_SMARTH(encargado.SelectedValue.ToString()), conexion.Devuelve_IDUbicacion(ubicacion.SelectedValue.ToString()), SHconexion.Devuelve_IDPrioridad(prioridad.SelectedValue.ToString()),
                                        0, averia.Value.ToString(), fecha_apertura[0], 0, img1.ImageUrl, img2.ImageUrl, "",
                                        fecha_ini[0], fecha_rep[0], conexion.Devuelve_IDtipo_reparacion(lista_reparacion.SelectedValue.ToString()), reparacion.Value.ToString(), observaciones.Value.ToString(),
                                        SHconexion.Devuelve_ID_Piloto_SMARTH(lista_realizadoPor.SelectedValue), horas_previstas, horas_reales, fecha_rev[0], SHconexion.Devuelve_ID_Piloto_SMARTH(revisado_por.SelectedValue.ToString()), observaciones_revision.Value.ToString(),0,0,0,CosteRepuestos,coste_horas,CosteTotales, AsignadoA, ReparadoPor, EstadoReparacion, horas_ok1,horas_ok2,horas_ok3,0, "1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1", "0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0");
                    //DataSet ds_molde = conexion.devuelve_molde(conexion.devuelve_IDMolde(tbMolde.Value.ToString()));
                    MandarMailV2("Nuevo");
                }
                else
                {
                    DataSet ds = conexion.Devuelve_parte_Molde(lista_partes.Value.ToString());
                    DataSet ds_molde = conexion.Devuelve_molde(conexion.Devuelve_IDMolde(tbMoldeNew.Value.Split('¬')[0].ToString()));
                    string TipoMail = "";
                    int reparado_anta = Convert.ToInt32(ds.Tables[0].Rows[0]["Terminado"]);
                    int revisado_anta = Convert.ToInt32(ds.Tables[0].Rows[0]["Revisado"]);
                    int revisadoNOK_anta = Convert.ToInt32(ds.Tables[0].Rows[0]["RevisadoNOK"]);

                    //compruebo botones
                    if (reparado.Checked && lista_trabajos.SelectedValue.ToString() == "Mant. Preventivo") //si es preventivo y está reparado, lo valido en automático
                    {
                        chek_reparado = 1;
                        revisado.Checked = true;
                        chek_revisado = 1;
                    }
                    else if (reparado.Checked)
                    {
                        chek_reparado = 1;
                    }
                    else
                    {
                        chek_reparado = 0;
                    }

                    if (revisado.Checked)
                    {
                        chek_revisado = 1;
                    }              
                    else
                    {
                        chek_revisado = 0;
                    }

                    if (revisadoNOK.Checked)
                    {
                        chek_revisadoNOK = 1;
                    }
                    else
                    {
                        chek_revisadoNOK = 0;
                    }
                  
                    //inicia acciones de aviso
                    

                    if (chek_revisado == 1 && chek_revisadoNOK == 1)
                    {
                        chek_revisado = 0;
                        chek_revisadoNOK = 0;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "ValidadoDoble();", true);

                    }

                    if (chek_reparado == 0 && (chek_revisadoNOK == 1 || chek_revisado == 1))
                    {
                        chek_revisado = 0;
                        chek_revisadoNOK = 0;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "ValidadoNREP();", true);

                    }
                    
                    else if (chek_reparado == 1 && chek_revisado == 0 && chek_revisadoNOK == 0)
                    {
                          if (chek_reparado != reparado_anta)
                        {
                            conexion.insertar_accion_pendiente(Convert.ToInt32(ds_molde.Tables[0].Rows[0]["ReferenciaMolde"]), "Parte: " + lista_partes.Value + ". Avería: " + averia.Value.ToString() + ". Acción pendiente de validar: " + reparacion.Value.ToString() + "", Convert.ToInt16(lista_partes.Value));
                            TipoMail = "Reparado";
                        }

                    }

                    else if (chek_reparado == 1 && chek_revisado == 0 && chek_revisadoNOK == 1)
                    {
                        chek_reparado = 0;
                        
                        if (chek_revisadoNOK != revisadoNOK_anta)
                        {
                            conexion.insertar_accion_pendiente(Convert.ToInt32(ds_molde.Tables[0].Rows[0]["ReferenciaMolde"]), "Parte: " + lista_partes.Value + ". Acción rechazada: " + reparacion.Value.ToString() + ". Avería: " + averia.Value.ToString() + "", Convert.ToInt16(lista_partes.Value));
                            TipoMail = "ValidadoNC";
                        }

                    }

                    else if (chek_reparado == 1 && chek_revisado == 1 && chek_revisadoNOK == 0)
                    {
                        if (chek_revisado != revisado_anta)

                        {
                            conexion.eliminar_accion_pendiente(Convert.ToInt32(ds_molde.Tables[0].Rows[0]["ReferenciaMolde"]));
                            TipoMail = "Validado";
                        }

                    }
                    
                    conexion.Modificar_parte(Convert.ToInt16(lista_partes.Value), conexion.Devuelve_IDMolde(tbMoldeNew.Value.Split('¬')[0].ToString()), SHconexion.Devuelve_IDTrabajo(lista_trabajos.SelectedValue.ToString()),
                                        SHconexion.Devuelve_ID_Piloto_SMARTH(encargado.SelectedValue.ToString()), conexion.Devuelve_IDUbicacion(ubicacion.SelectedValue.ToString()), SHconexion.Devuelve_IDPrioridad(prioridad.SelectedValue.ToString()), 
                                        0, averia.Value.ToString(), fecha_apertura[0], chek_reparado, img1.ImageUrl, img2.ImageUrl, "",
                                        fecha_ini[0], fecha_rep[0], conexion.Devuelve_IDtipo_reparacion(lista_reparacion.SelectedValue.ToString()), reparacion.Value.ToString(), observaciones.Value.ToString(),
                                        SHconexion.Devuelve_ID_Piloto_SMARTH(lista_realizadoPor.SelectedValue.ToString()), horas_previstas, horas_reales, fecha_rev[0], SHconexion.Devuelve_ID_Piloto_SMARTH(revisado_por.SelectedValue.ToString()), observaciones_revision.Value.ToString(), chek_revisado, chek_revisadoNOK, CosteRepuestos.ToString(), coste_horas, CosteTotales, AsignadoA, ReparadoPor, EstadoReparacion, horas_ok1, horas_ok2, horas_ok3, IDTipoPreventivo, PreventivoAcciones, PreventivoEstado);
                    if (TipoMail != "")
                    {
                        MandarMailV2(TipoMail);
                    }
                    if (lista_trabajos.SelectedValue.ToString() == "Mant. Preventivo" && chek_reparado == 1) //Si es preventivo y está reparado reinicio contador
                    {
                      
                        conexion.Reiniciar_contador_MoldeMaq(Convert.ToInt32(conexion.Devuelve_IDMolde(tbMoldeNew.Value.Split('¬')[0].ToString())), Convert.ToInt32(conexion.Devuelve_Frecuencia_Tipo_Mantenimiento_MOLDE(DropTipoPreventivo.SelectedValue.ToString())));

                    }
                }
                //conexion.Añadir_operacion_molde(Convert.ToInt16(lista_partes.Value), conexion.Devuelve_IDMolde(tbMoldeNew.Value.Split('¬')[0].ToString()), chek_reparado, chek_revisado, chek_revisadoNOK);
                cargar_pendientes();              
                DataSet recargads = conexion.Devuelve_parte_Molde(lista_partes.Value);
                Cargar_parte(recargads);
            }
            catch (Exception ex)
            {
                mandar_mail2(ex.Message+ex.StackTrace, "Error en la aplicación de metrologia");
            }
        }
  
        //DESPLEGABLES Y ACCIONES
        public void SeleccionPreventivo(Object sender, EventArgs e)
        {
            try
            {
                Q2LINEA.Visible = false;
                Q3LINEA.Visible = false;
                Q4LINEA.Visible = false;
                Q5LINEA.Visible = false;
                Q6LINEA.Visible = false;
                Q7LINEA.Visible = false;
                Q8LINEA.Visible = false;
                Q9LINEA.Visible = false;
                Q10LINEA.Visible = false;
                Q11LINEA.Visible = false;
                Q12LINEA.Visible = false;
                Q13LINEA.Visible = false;
                Q14LINEA.Visible = false;
                Q15LINEA.Visible = false;
                Q16LINEA.Visible = false;
                Q17LINEA.Visible = false;
                Q18LINEA.Visible = false;
                Q19LINEA.Visible = false;
                Q20LINEA.Visible = false;
                Q1DROP.ClearSelection();
                Q1Text.Text = "";
                Q1INT.Text = "1";
                Q2DROP.ClearSelection();
                Q2Text.Text = "";
                Q2INT.Text = "1";
                Q3DROP.ClearSelection();
                Q3Text.Text = "";
                Q3INT.Text = "1";
                Q4DROP.ClearSelection();
                Q4Text.Text = "";
                Q4INT.Text = "1";
                Q5DROP.ClearSelection();
                Q5Text.Text = "";
                Q5INT.Text = "1";
                Q6DROP.ClearSelection();
                Q6Text.Text = "";
                Q6INT.Text = "1";
                Q7DROP.ClearSelection();
                Q7Text.Text = "";
                Q7INT.Text = "1";
                Q8DROP.ClearSelection();
                Q8Text.Text = "";
                Q8INT.Text = "1";
                Q9DROP.ClearSelection();
                Q9Text.Text = "";
                Q9INT.Text = "1";
                Q10DROP.ClearSelection();
                Q10Text.Text = "";
                Q10INT.Text = "1";
                Q11DROP.ClearSelection();
                Q11Text.Text = "";
                Q11INT.Text = "1";
                Q12DROP.ClearSelection();
                Q12Text.Text = "";
                Q12INT.Text = "1";
                Q13DROP.ClearSelection();
                Q13Text.Text = "";
                Q13INT.Text = "1";
                Q14DROP.ClearSelection();
                Q14Text.Text = "";
                Q14INT.Text = "1";
                Q15DROP.ClearSelection();
                Q15Text.Text = "";
                Q15INT.Text = "1";
                Q16DROP.ClearSelection();
                Q16Text.Text = "";
                Q16INT.Text = "1";
                Q17DROP.ClearSelection();
                Q17Text.Text = "";
                Q17INT.Text = "1";
                Q18DROP.ClearSelection();
                Q18Text.Text = "";
                Q18INT.Text = "1";
                Q19DROP.ClearSelection();
                Q19Text.Text = "";
                Q19INT.Text = "1";
                Q20DROP.ClearSelection();
                Q20Text.Text = "";
                Q20INT.Text = "1";
                TextTipoPreventivo.Text = "MANTENIMIENTO PREVENTIVO: " + DropTipoPreventivo.SelectedValue.ToString();
                //BlOQUE SELECCION PREVENTIVO (CARGA DE DATASET Y DROPDOWN)

                Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();
                Conexion_SMARTH SHconexion = new Conexion_SMARTH();
                if (lista_trabajos.SelectedValue.ToString() == "Mant. Preventivo")
                {
                    /*
                    string IDMaquinaCHAR = conexion.devuelve_IDmaquinaCHAR(lista_maquinas.SelectedValue.ToString());

                    int IDTipoMantPreventivo = conexion.Devuelve_IDMantenimiento_BMS(IDMaquinaCHAR);
                    if (IDTipoMantPreventivo > 0)
                    {
                        DataSet DropTiposPreventivos = conexion.Devuelve_Listado_PreventivosXMaquina(IDTipoMantPreventivo);
                        DropTipoPreventivo.Items.Clear();
                        //DropTipoPreventivo.Items.Add(""); Recuperar
                        foreach (DataRow row in DropTiposPreventivos.Tables[0].Rows)
                        {
                            DropTipoPreventivo.Items.Add(row["TipoFrecuencia"].ToString());
                        }
                    }
                    */

                    DataSet ListaPreguntas = conexion.Devuelve_Listado_Preguntas_PREVMAQ();
                    DataSet CuestionPreventiva = conexion.Devuelve_Tipo_Mantenimiento_Seleccionado_PREVMolde(DropTipoPreventivo.SelectedValue.ToString());


                    Q1INT.Text = CuestionPreventiva.Tables[0].Rows[0]["ArrayPreguntas"].ToString().Split(' ')[0];

                    ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q1INT.Text + "";
                    DataTable ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                    Q1Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();



                    Q2INT.Text = CuestionPreventiva.Tables[0].Rows[0]["ArrayPreguntas"].ToString().Split(' ')[1];
                    if (Q2INT.Text != "1")
                    {
                        Q2LINEA.Visible = true;

                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q2INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q2Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }

                    Q3INT.Text = CuestionPreventiva.Tables[0].Rows[0]["ArrayPreguntas"].ToString().Split(' ')[2];
                    if (Q3INT.Text != "1")
                    {
                        Q3LINEA.Visible = true;

                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q3INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q3Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }

                    Q4INT.Text = CuestionPreventiva.Tables[0].Rows[0]["ArrayPreguntas"].ToString().Split(' ')[3];
                    if (Q4INT.Text != "1")
                    {
                        Q4LINEA.Visible = true;

                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q4INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q4Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }

                    Q5INT.Text = CuestionPreventiva.Tables[0].Rows[0]["ArrayPreguntas"].ToString().Split(' ')[4];
                    if (Q5INT.Text != "1")
                    {
                        Q5LINEA.Visible = true;

                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q5INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q5Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }

                    Q6INT.Text = CuestionPreventiva.Tables[0].Rows[0]["ArrayPreguntas"].ToString().Split(' ')[5];
                    if (Q6INT.Text != "1")
                    {
                        Q6LINEA.Visible = true;

                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q6INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q6Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }

                    Q7INT.Text = CuestionPreventiva.Tables[0].Rows[0]["ArrayPreguntas"].ToString().Split(' ')[6];
                    if (Q7INT.Text != "1")
                    {
                        Q7LINEA.Visible = true;

                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q7INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q7Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }

                    Q8INT.Text = CuestionPreventiva.Tables[0].Rows[0]["ArrayPreguntas"].ToString().Split(' ')[7];
                    if (Q8INT.Text != "1")
                    {
                        Q8LINEA.Visible = true;

                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q8INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q8Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }

                    Q9INT.Text = CuestionPreventiva.Tables[0].Rows[0]["ArrayPreguntas"].ToString().Split(' ')[8];
                    if (Q9INT.Text != "1")
                    {
                        Q9LINEA.Visible = true;

                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q9INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q9Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }

                    Q10INT.Text = CuestionPreventiva.Tables[0].Rows[0]["ArrayPreguntas"].ToString().Split(' ')[9];
                    if (Q10INT.Text != "1")
                    {
                        Q10LINEA.Visible = true;

                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q10INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q10Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }

                    Q11INT.Text = CuestionPreventiva.Tables[0].Rows[0]["ArrayPreguntas"].ToString().Split(' ')[10];
                    if (Q11INT.Text != "1")
                    {
                        Q11LINEA.Visible = true;

                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q11INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q11Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }

                    Q12INT.Text = CuestionPreventiva.Tables[0].Rows[0]["ArrayPreguntas"].ToString().Split(' ')[11];
                    if (Q12INT.Text != "1")
                    {
                        Q12LINEA.Visible = true;

                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q12INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q12Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }

                    Q13INT.Text = CuestionPreventiva.Tables[0].Rows[0]["ArrayPreguntas"].ToString().Split(' ')[12];
                    if (Q13INT.Text != "1")
                    {
                        Q13LINEA.Visible = true;

                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q13INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q13Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }

                    Q14INT.Text = CuestionPreventiva.Tables[0].Rows[0]["ArrayPreguntas"].ToString().Split(' ')[13];

                    if (Q14INT.Text != "1")
                    {
                        Q14LINEA.Visible = true;

                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q14INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q14Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }

                    Q15INT.Text = CuestionPreventiva.Tables[0].Rows[0]["ArrayPreguntas"].ToString().Split(' ')[14];
                    if (Q15INT.Text != "1")
                    {
                        Q15LINEA.Visible = true;

                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q15INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q15Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }

                    Q16INT.Text = CuestionPreventiva.Tables[0].Rows[0]["ArrayPreguntas"].ToString().Split(' ')[15];
                    if (Q16INT.Text != "1")
                    {
                        Q16LINEA.Visible = true;

                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q16INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q16Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }

                    Q17INT.Text = CuestionPreventiva.Tables[0].Rows[0]["ArrayPreguntas"].ToString().Split(' ')[16];
                    if (Q17INT.Text != "1")
                    {
                        Q17LINEA.Visible = true;

                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q17INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q17Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }

                    Q18INT.Text = CuestionPreventiva.Tables[0].Rows[0]["ArrayPreguntas"].ToString().Split(' ')[17];
                    if (Q18INT.Text != "1")
                    {
                        Q18LINEA.Visible = true;

                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q18INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q18Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }

                    Q19INT.Text = CuestionPreventiva.Tables[0].Rows[0]["ArrayPreguntas"].ToString().Split(' ')[18];
                    if (Q19INT.Text != "1")
                    {
                        Q19LINEA.Visible = true;

                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q19INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q19Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }

                    Q20INT.Text = CuestionPreventiva.Tables[0].Rows[0]["ArrayPreguntas"].ToString().Split(' ')[19];
                    if (Q20INT.Text != "1")
                    {
                        Q20LINEA.Visible = true;

                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q20INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q20Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }
                } //lista trabajos = "Mant.Preventivo"

            }
            catch (Exception ex) 
            {
                mandar_mail2(ex.Message+ex.StackTrace, "Error en la aplicación de mantenimiento");
            }
        }

        public void Gestionar_trabajadores(Object sender, EventArgs e)
        {
            try
            {
                HtmlButton anchor = (HtmlButton)sender;
                string name = anchor.ID;
                switch (name)
                {

                    //BLOQUE ASIGNADOS
                    case "AgregarAsignado":
                        if (Asignado1.Value == "-")
                        {
                            Asignado1.Value = lista_realizadoPorNEW.SelectedValue.ToString();
                        }
                        else if (Asignado1.Value != "-" && Asignado2.Value == "-")
                        {
                            ColAsignado2.Visible = true;
                            Asignado2.Value = lista_realizadoPorNEW.SelectedValue.ToString();
                        }
                        else if (Asignado1.Value != "-" && Asignado2.Value != "" && Asignado3.Value == "-")
                        {
                            ColAsignado3.Visible = true;
                            Asignado3.Value = lista_realizadoPorNEW.SelectedValue.ToString();
                        }
                        else
                        { }
                        break;

                    case "BorrarAsignado1":
                        if (ColAsignado3.Visible == true)
                        {
                            Asignado1.Value = Asignado2.Value;
                            Asignado2.Value = Asignado3.Value;
                            ColAsignado3.Visible = false;
                            Asignado3.Value = "-";
                        }

                        else if (ColAsignado2.Visible == true)
                        {
                            Asignado1.Value = Asignado2.Value;
                            ColAsignado2.Visible = false;
                            Asignado2.Value = "-";
                        }
                        else
                        {
                            Asignado1.Value = "-";
                        }
                        break;

                    case "BorrarAsignado2":
                        if (ColAsignado3.Visible == true)
                        {
                            Asignado2.Value = Asignado3.Value;
                            ColAsignado3.Visible = false;
                            Asignado3.Value = "-";
                        }
                        else
                        {
                            Asignado2.Value = "-";
                            ColAsignado2.Visible = false;
                        }
                        break;

                    case "BorrarAsignado3":

                        ColAsignado3.Visible = false;
                        Asignado3.Value = "-";
                        break;


                    //BLOQUE REPARADOS
                    case "AgregarReparado":
                        if (ReparadoPor1.Value == "-")
                        {
                            ReparadoPor1.Value = lista_realizadoPorNEW.SelectedValue.ToString();
                        }
                        else if (ReparadoPor1.Value != "-" && ReparadoPor2.Value == "-")
                        {
                            ReparadoPor2.Visible = true;
                            horas2.Visible = true;
                            BorrarReparadoPor2.Visible = true;
                            ReparadoPor2.Value = lista_realizadoPorNEW.SelectedValue.ToString();
                        }
                        else if (ReparadoPor1.Value != "-" && ReparadoPor2.Value != "-" && ReparadoPor3.Value == "-")
                        {
                            BorrarReparadoPor3.Visible = true;
                            horas3.Visible = true;
                            ReparadoPor3.Visible = true;
                            ReparadoPor3.Value = lista_realizadoPorNEW.SelectedValue.ToString();
                        }
                        else
                        { }
                        break;

                    case "BorrarReparadoPor1":
                        if (ReparadoPor3.Visible == true)
                        {

                            ReparadoPor1.Value = ReparadoPor2.Value;
                            horas1.Value = horas2.Value;

                            ReparadoPor2.Value = ReparadoPor3.Value;
                            horas2.Value = horas3.Value;

                            BorrarReparadoPor3.Visible = false;
                            horas3.Visible = false;
                            ReparadoPor3.Visible = false;

                            horas3.Value = "0";
                            ReparadoPor3.Value = "-";
                        }
                        else if (ReparadoPor2.Visible == true)
                        {
                            ReparadoPor1.Value = ReparadoPor2.Value;
                            horas1.Value = horas2.Value;
                            BorrarReparadoPor2.Visible = false;
                            horas2.Visible = false;
                            ReparadoPor2.Visible = false;

                            horas2.Value = "0";
                            ReparadoPor2.Value = "-";
                        }
                        else
                        {
                            horas1.Value = "0";
                            ReparadoPor1.Value = "-";
                        }
                        break;
                    case "BorrarReparadoPor2":
                        if (ReparadoPor3.Visible == true)
                        {

                            ReparadoPor2.Value = ReparadoPor3.Value;
                            horas2.Value = horas3.Value;
                            BorrarReparadoPor3.Visible = false;
                            horas3.Visible = false;
                            ReparadoPor3.Visible = false;
                            horas3.Value = "0";
                            ReparadoPor3.Value = "-";
                        }
                        else
                        {
                            horas2.Value = "0";
                            ReparadoPor2.Value = "-";
                            BorrarReparadoPor2.Visible = false;
                            horas2.Visible = false;
                            ReparadoPor2.Visible = false;
                        }
                        break;
                    case "BorrarReparadoPor3":
                        BorrarReparadoPor3.Visible = false;
                        horas3.Visible = false;
                        ReparadoPor3.Visible = false;
                        horas3.Value = "0";
                        ReparadoPor3.Value = "-";
                        break;

                }
            }
            catch (Exception ex)
            {
                mandar_mail2(ex.Message+ex.StackTrace, "Error en la aplicación de mantenimiento");
            }
        }

        public void RellenarDropDowns()
        {
            try
            {
                Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                //DATALISTA LISTAMOLDES
                tbMoldeListaMoldes.InnerHtml = "";
                DataTable ListaMoldes = SHConexion.Devuelve_listado_MOLDES_V2();
                {
                    for (int i = 0; i <= ListaMoldes.Rows.Count - 1; i++)
                    {
                        tbMoldeListaMoldes.InnerHtml = tbMoldeListaMoldes.InnerHtml + System.Environment.NewLine +
                            String.Format("<option value='{0}'>", ListaMoldes.Rows[i][0]);
                    }
                }


                //Conexion conexion = new Conexion();
                DataTable AuxiliaresMoldes = SHConexion.Devuelve_Auxiliares_Mantenimiento(); //Lista de dropdowns

                //TIPO MANTENIMIENTO / SMARTH
                lista_trabajos.Items.Clear();
                AuxiliaresMoldes.DefaultView.RowFilter = "TipoMantenimientoMolde is not null";
                DataTable tipo_trabajo = AuxiliaresMoldes.DefaultView.ToTable();
                foreach (DataRow row in tipo_trabajo.Rows)
                {
                    lista_trabajos.Items.Add(row["TipoMantenimientoMolde"].ToString());
                }
                //PRIORIDAD / SMARTH
                prioridad.Items.Clear();
                AuxiliaresMoldes.DefaultView.RowFilter = "Prioridad is not null";
                DataTable prioridades = AuxiliaresMoldes.DefaultView.ToTable();
                foreach (DataRow row in prioridades.Rows)
                {
                    prioridad.Items.Add(row["Prioridad"].ToString());
                }

                //UBICACIONES / SMARTH
                ubicacion.Items.Clear();
                AuxiliaresMoldes.DefaultView.RowFilter = "UbicacionMolde is not null";
                DataTable ubicaciones = AuxiliaresMoldes.DefaultView.ToTable();
                foreach (DataRow row in ubicaciones.Rows)
                {
                    ubicacion.Items.Add(row["UbicacionMolde"].ToString());
                }

                lista_reparacion.Items.Clear();
                AuxiliaresMoldes.DefaultView.RowFilter = "TipoReparación is not null";
                DataTable tipo_reparacion = AuxiliaresMoldes.DefaultView.ToTable();
                foreach (DataRow row in tipo_reparacion.Rows)
                {
                    lista_reparacion.Items.Add(row["TipoReparación"].ToString());
                }


                DataSet Personal = SHConexion.Devuelve_mandos_intermedios_SMARTH();
                DataSet operario_mantenimiento = Personal;
                operario_mantenimiento.Tables[0].DefaultView.RowFilter = "Departamento = 'MANTENIMIENTO' OR Departamento = '-'  OR Departamento = 'Z'";
                DataTable DToperario_mantenimiento = (operario_mantenimiento.Tables[0].DefaultView).ToTable();
                lista_realizadoPorNEW.Items.Clear();
                foreach (DataRow row in DToperario_mantenimiento.Rows)
                {
                    lista_realizadoPorNEW.Items.Add(row["Nombre"].ToString());
                }

                DataSet EncargadosAbiertoPor = Personal;
                EncargadosAbiertoPor.Tables[0].DefaultView.RowFilter = "";
                //"Departamento <> 'ADMINISTRADOR' AND Departamento <> 'OFICINAS' AND Departamento <> 'CALIDAD'";
                DataTable DTEncargadosAbiertoPor = (EncargadosAbiertoPor.Tables[0].DefaultView).ToTable();
                encargado.Items.Clear();
                revisado_por.Items.Clear();
                foreach (DataRow row in DTEncargadosAbiertoPor.Rows)
                {
                    encargado.Items.Add(row["Nombre"].ToString());
                    revisado_por.Items.Add(row["Nombre"].ToString());
                }

                //lista partes moldes
                FiltroParte.InnerHtml = "";
                DataTable ListaParteMoldes = conexion.Devuelve_TOP300_Partes_Moldes();
                {
                    for (int i = 0; i <= ListaParteMoldes.Rows.Count - 1; i++)
                    {
                        FiltroParte.InnerHtml = FiltroParte.InnerHtml + System.Environment.NewLine +
                            String.Format("<option value='{0}'>", ListaParteMoldes.Rows[i][0]);
                    }
                }

                //REVISA E INCLUYE 0
                /*
                DataSet turnos = conexion.devuelve_lista_turnos();
                turno.Items.Clear();
                foreach (DataRow row in turnos.Tables[0].Rows)
                {
                    turno.Items.Add(row["Turno"].ToString());
                }*/
                //conexion.modificacion_BD();
            }
            catch (Exception ex)
            {
                mandar_mail2(ex.Message+ex.StackTrace, "Error en la aplicación de mantenimiento");
            }
        }

        public void cargar_pendientes()
        {
            try
            {
                Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();
                Conexion_SMARTH SHconexion = new Conexion_SMARTH();
                DataSet ds = conexion.TablaReparacionMoldes_pendientes();
                listado_partes.Controls.Clear();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    {
                        //declaro variables
                        HtmlAnchor a_enclose = new HtmlAnchor();
                        //var a_enclose = new HtmlGenericControl("a");
                        var div_flex = new HtmlGenericControl("div");
                        var h3_header_molde = new HtmlGenericControl("h3");
                        var h5_header_descripcion = new HtmlGenericControl("h5");
                        var p_text = new HtmlGenericControl("p");
                        var div_flex2 = new HtmlGenericControl("div");
                        var small_prioridad = new HtmlGenericControl("small");
                        var small_dias = new HtmlGenericControl("small");

                        //asigno atributos
                        a_enclose.Attributes["class"] = "list-group-item list-group-item-action shadow";
                        div_flex.Attributes["class"] = "d-flex w-100  shadow-sm";
                        h5_header_descripcion.Attributes["class"] = "mt-2 ms-2";
                        p_text.Attributes["class"] = "mb-1 ms-2 me-2";
                        div_flex2.Attributes["class"] = "d-flex w-100 justify-content-between";
                        small_dias.Attributes["class"] = "mt-1 ms-1";

                        DataSet molde = conexion.Devuelve_molde(Convert.ToInt32(row["IDMoldes"]));
                        if (molde.Tables[0].Rows.Count > 0)
                        {
                            //relleno datos
                            h3_header_molde.InnerText = molde.Tables[0].Rows[0]["ReferenciaMolde"].ToString();
                            h5_header_descripcion.InnerText = molde.Tables[0].Rows[0]["Descripcion"].ToString();
                            p_text.InnerText = row["MotivoAveria"].ToString();
                            small_dias.InnerText = row["FechaAveria"].ToString();

                            //asigno criticidad
                            //string prioridad = SHconexion.Devuelve_prioridad_parte(Convert.ToInt16(row["IdPrioridad"].ToString()));
                            switch (Convert.ToInt16(row["idPrioridad"]))
                            {

                                case 1:
                                    small_prioridad.InnerHtml = "&nbsp Prioridad Crítica &nbsp";
                                    small_prioridad.Attributes["class"] = "bg-danger rounded-2 shadow-sm ms-2 text-white";
                                    break;
                                case 2:
                                    small_prioridad.InnerHtml = "&nbsp Prioridad Alta &nbsp";
                                    small_prioridad.Attributes["class"] = "bg-warning rounded-2 shadow-sm ms-2 text-white";
                                    break;
                                case 3:
                                    small_prioridad.InnerHtml = "&nbsp Prioridad Media &nbsp";
                                    small_prioridad.Attributes["class"] = "bg-primary rounded-2 shadow-sm ms-2 text-white";
                                    break;
                                case 4:
                                    small_prioridad.InnerHtml = "&nbsp Prioridad Baja &nbsp";
                                    small_prioridad.Attributes["class"] = "bg-info rounded-2 shadow-sm ms-2";
                                    break;
                                case 5:
                                    small_prioridad.InnerHtml = "&nbsp Aplazable &nbsp";
                                    small_prioridad.Attributes["class"] = "bg-secondary  rounded-2 shadow-sm ms-2";
                                    break;
                                default:
                                    small_prioridad.InnerHtml = "&nbsp Sin prioridad &nbsp";
                                    small_prioridad.Attributes["class"] = "bg-light  rounded-2 shadow-sm ms-2";
                                    break;
                            }

                            //ensamblo htmls
                            div_flex.Controls.Add(h3_header_molde);
                            div_flex.Controls.Add(h5_header_descripcion);

                            div_flex2.Controls.Add(small_prioridad);
                            div_flex2.Controls.Add(small_dias);

                            a_enclose.ID = "id_" + Convert.ToInt16(row["IdReparacionMolde"]);
                            a_enclose.ServerClick += new EventHandler(IrPendiente);  //Hookup event to method Linkme(object sender, System.EventArgs e)
                            a_enclose.HRef = "#";
                            a_enclose.Controls.Add(div_flex);
                            a_enclose.Controls.Add(p_text);
                            a_enclose.Controls.Add(div_flex2);

                            listado_partes.Controls.Add(a_enclose);

                        }
                    }
                }


            }
            catch (Exception ex)
            {
                mandar_mail2(ex.Message+ex.StackTrace, "Error en la aplicación de mantenimiento");
            }

        }

        void IrPendiente(Object sender, EventArgs e)
        {
            HtmlAnchor a = (HtmlAnchor)sender;
            string id_aux = a.ID;
            string[] id = id_aux.Split('_');

            Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();
            DataSet ds = conexion.Devuelve_parte_Molde(id[1]);
            Cargar_parte(ds);
        }

        //ADJUNTAR IMÁGENES
        public void insertar_foto(Object sender, EventArgs e)
        {
            try
            {
                if (FileUpload1.HasFile)
                    SaveFile(FileUpload1.PostedFile, 1);

                if (FileUpload2.HasFile)
                    SaveFile(FileUpload2.PostedFile, 2);

            }
            catch (Exception ex)
            {
                mandar_mail2(ex.Message+ex.StackTrace, "Error en la aplicación de mantenimiento");
            }
        }

        private void SaveFile(HttpPostedFile file, int num_img)
        {
            try
            {
                // Specify the path to save the uploaded file to.
                //string savePath = "C:\\inetpub_mantenimiento\\imagenes\\";
                string savePath = "C:\\inetpub_thermoweb\\SMARTH_docs\\MANTENIMIENTO\\";

                // Get the name of the file to upload.
                //string fileName = FileUpload1.FileName;
                string fileName = "";
                string extension = "";
                switch (num_img)
                {
                    case 1:
                        //fileName = FileUpload1.FileName;
                        extension = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
                        fileName = lista_partes.Value + "_" + DateTime.Now.ToString("yyyyy-MM-dd_HHmmss") + "_MUESTRA1" + extension;
                        break;
                    case 2:
                        extension = System.IO.Path.GetExtension(FileUpload2.PostedFile.FileName);
                        fileName = lista_partes.Value + "_" + DateTime.Now.ToString("yyyyy-MM-dd_HHmmss") + "_MUESTRA2" + extension;
                        break;
                    
                    default: break;
                }
                 

                // Create the path and file name to check for duplicates.
                string pathToCheck = savePath + fileName;

                // Create a temporary file name to use for checking duplicates.
                string tempfileName = "";

                // Check to see if a file already exists with the
                // same name as the file to upload.        
                if (System.IO.File.Exists(pathToCheck))
                {
                    int counter = 2;
                    while (System.IO.File.Exists(pathToCheck))
                    {
                        // if a file with this name already exists,
                        // prefix the filename with a number.
                        tempfileName = counter.ToString() + fileName;
                        pathToCheck = savePath + tempfileName;
                        counter++;
                    }

                    fileName = tempfileName;

                    // Notify the user that the file name was changed.
                    UploadStatusLabel.Text = "Imágenes cargadas correctamente.";
                }
                else
                {
                    // Notify the user that the file was saved successfully.
                    UploadStatusLabel.Text = "Imágenes cargadas correctamente.";
                }

                // Append the name of the file to upload to the path.
                savePath += fileName;

                // Call the SaveAs method to save the uploaded
                // file to the specified directory.
                switch (num_img)
                {
                    case 1:
                        FileUpload1.SaveAs(savePath);
                        //img1.Src = "http://FACTS4-SRV/thermogestion/SMARTH_docs/MANTENIMIENTO/" + fileName;
                        img1.ImageUrl = "../SMARTH_docs/MANTENIMIENTO/" + fileName;
                        break;
                    case 2:
                        FileUpload2.SaveAs(savePath);
                        //img2.Src = "http://FACTS4-SRV/thermogestion/SMARTH_docs/MANTENIMIENTO/" + fileName;
                        img2.ImageUrl = "../SMARTH_docs/MANTENIMIENTO/" + fileName;
                        break;
              
                    default: break;
                }                
            }
            catch (Exception ex)
            {
                mandar_mail2(ex.Message+ex.StackTrace, "Error en la aplicación de mantenimiento");
            }
        }
 
        // LLAMADAS DE BOTONES DE BÚSQUEDA Y CARGA
        public void BuscarParte(Object sender, EventArgs e)
        {
            try
            {
                Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();
                DataSet ds = conexion.Devuelve_parte_Molde(tbBuscar.Value.Split('|')[0].ToString());
                Cargar_parte(ds);
                tbBuscar.Value = "";
             
            }
            catch (Exception ex)
            {
                mandar_mail2(ex.Message+ex.StackTrace, "Error en la aplicación de mantenimiento");

            }
        }
        public void IrAnteriorSiguiente(Object sender, EventArgs e)
        {
            try
            {
                Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();

                HtmlButton anchor = (HtmlButton)sender;
                string name = anchor.ID;
                switch (name)
                {
                    case "btnAnterior":
                        int parte = Convert.ToInt32(lista_partes.Value) - 1;

                        DataSet ds = conexion.Devuelve_parte_Molde(parte.ToString());
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            Cargar_parte(ds);
                        }
                        else
                        {
                        }
                        break;
                    case "btnSiguiente":
                        parte = Convert.ToInt32(lista_partes.Value) + 1;

                        ds = conexion.Devuelve_parte_Molde(parte.ToString());
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            Cargar_parte(ds);
                        }
                        else
                        {
                        }
                        break;
                }
            }
            catch (Exception ex)
            { }
        }
        public void ContactsGridView_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Redirect")
            {
                Response.Redirect("http://facts4-srv/thermogestion/MANTENIMIENTO/ReparacionMoldes.aspx?PARTE=" + e.CommandArgument.ToString());
            }
        }

        //LANZADORES DE EMAILS
        protected void MandarMailV2(string TipoParte)
        {
            
            try
            {
                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                    {
                        string URL = "http://facts4-srv/thermogestion/MANTENIMIENTO/ReparacionMoldes.aspx?PARTE=" + lista_partes.Value.ToString();
                        //contenedor1.RenderControl(hw);
                        StringReader sr = new StringReader(sw.ToString());
                        //MailMessage mm = new MailMessage("bms@thermolympic.es", "pedro@thermolympic.es");
                        MailMessage mm = new MailMessage();

                        Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();
                        DataSet ds_correos = conexion.Leer_correosMANTMOL();
                        //DataSet ds_correos = conexion.leer_correosADMIN(); //cambiar a MANTMAQ

                        foreach (DataRow row in ds_correos.Tables[0].Rows)
                        {
                            mm.To.Add(new MailAddress(row["Correo"].ToString()));
                        }

                        //var inlineLogo = new LinkedResource(Server.MapPath(""+hyperlink2.ImageUrl.ToString()+""), "image/png");
                        //string filename1 = Path.GetFileName(new Uri(img1.Src.ToString()).AbsolutePath);
                        string filename1 = Path.GetFileName(new Uri(Path.GetFullPath(img1.ImageUrl.ToString())).AbsolutePath);
                        string extension1 = Path.GetExtension(filename1).Substring(1);
                        //var inlineLogo = new LinkedResource(Server.MapPath("../SMARTH_docs/MANTENIMIENTO/" + filename1 + ""), "image/" + extension1 + "");
                        //inlineLogo.ContentId = Guid.NewGuid().ToString();
                        var inlineLogo = new LinkedResource(Server.MapPath("../SMARTH_docs/MANTENIMIENTO/sin_imagen.jpg"));
                        if (filename1 != "sin_imagen.jpg")
                        {
                            inlineLogo = new LinkedResource(Server.MapPath("../SMARTH_docs/MANTENIMIENTO/" + filename1 + ""), "image/" + extension1 + "");
                        }
                        inlineLogo.ContentId = Guid.NewGuid().ToString();

                        //string filename2 = Path.GetFileName(new Uri(img2.Src.ToString()).AbsolutePath);
                        string filename2 = Path.GetFileName(new Uri(Path.GetFullPath(img2.ImageUrl.ToString())).AbsolutePath);
                        string extension2 = Path.GetExtension(filename2).Substring(1);
                        var inlineLogo2 = new LinkedResource(Server.MapPath("../SMARTH_docs/MANTENIMIENTO/sin_imagen.jpg"));
                        if (filename2 != "sin_imagen.jpg")
                        {
                            inlineLogo2 = new LinkedResource(Server.MapPath("../SMARTH_docs/MANTENIMIENTO/" + filename2 + ""), "image/" + extension2 + "");
                        }
                        inlineLogo2.ContentId = Guid.NewGuid().ToString();

                        mm.From = new MailAddress("mantenimiento@thermolympic.es");
                        //mm.To.Add(new MailAddress("pedro@thermolympic.es"));
                        string subject = "";
                        string Body = "";
                        DataSet ds = conexion.Devuelve_parte_Molde(lista_partes.Value.ToString());
                        DataSet ds_molde = conexion.Devuelve_molde(conexion.Devuelve_IDMolde(tbMoldeNew.Value.Split('¬')[0].ToString()));
                        switch (TipoParte)
                        {
                            case "Nuevo":
                                subject = "Nuevo parte " + Convert.ToInt16(lista_partes.Value) + " de reparación de molde.";
                                Body = "<strong>Molde a reparar: </strong>" + Convert.ToInt32(ds_molde.Tables[0].Rows[0]["ReferenciaMolde"]) + " " + ds_molde.Tables[0].Rows[0]["Descripcion"].ToString() + "<br><strong>Avería denunciada: </strong>" + averia.Value.ToString() + "<br>Abierto por: " + encargado.SelectedValue.ToString() + "<br><a href='http://facts4-srv/thermogestion/MANTENIMIENTO/ReparacionMoldes.aspx?PARTE=" + Convert.ToInt16(lista_partes.Value) + "'>Accede al parte a través de este link.</a>";
                                break;

                            case "Reparado":
                                string REPOP = "";
                                if (ReparadoPor3.Value != "-")
                                { REPOP = "por " + ReparadoPor1.Value + ", " + ReparadoPor2.Value + " y " + ReparadoPor3.Value; }
                                else if (ReparadoPor2.Value != "-")
                                { REPOP = "por " + ReparadoPor1.Value + " y " + ReparadoPor2.Value; }
                                else if (ReparadoPor1.Value != "-")
                                { REPOP = "por " + ReparadoPor1.Value; }
                                else { }
                                string REPOP2 = "";
                                if (encargado.SelectedValue.ToString() != "-")
                                { REPOP2 = "por " + encargado.SelectedValue.ToString(); }
                                subject = "Reparación realizada pendiente de validar " + REPOP2 + ". Parte " + Convert.ToInt16(lista_partes.Value) + " de molde.";
                                Body = "<strong>Reparación realizada " + REPOP + " pendiente de validación " + REPOP2 + ".</strong><br>Parte " + Convert.ToInt16(lista_partes.Value) + ".<br><strong>Molde:</strong> " + Convert.ToInt32(ds_molde.Tables[0].Rows[0]["ReferenciaMolde"]) + " " + ds_molde.Tables[0].Rows[0]["Descripcion"].ToString() + "<br><strong>Avería denunciada: </strong>" + averia.Value.ToString() + "<br><strong>Acción pendiente de validar: </strong>" + reparacion.Value.ToString() + " <br><a href='http://facts4-srv/thermogestion/MANTENIMIENTO/ReparacionMoldes.aspx?PARTE=" + Convert.ToInt16(lista_partes.Value) + "'>Accede al parte a través de este link.</a>";
                                break;

                            case "ValidadoNC":
                                REPOP = "";
                                if (revisado_por.SelectedValue.ToString() != "-")
                                { REPOP = "por " + revisado_por.SelectedValue.ToString(); }
                                subject = "Revisión NOK. Parte " + Convert.ToInt16(lista_partes.Value) + " de reparación de molde.";
                                Body = "<strong>Reparación validada como no conforme " + REPOP + ".</strong><br>Parte " + Convert.ToInt16(lista_partes.Value) + ".<br><strong>Molde:</strong> " + Convert.ToInt32(ds_molde.Tables[0].Rows[0]["ReferenciaMolde"]) + " " + ds_molde.Tables[0].Rows[0]["Descripcion"].ToString() + "<br><strong>Avería denunciada: </strong>" + averia.Value.ToString() + "<br><strong>Reparación no conforme: </strong>" + reparacion.Value.ToString() + " <br><a href='http://facts4-srv/thermogestion/MANTENIMIENTO/ReparacionMoldes.aspx?PARTE=" + Convert.ToInt16(lista_partes.Value) + "'>Accede al parte a través de este link.</a>";
                                break;

                            case "Validado":
                                REPOP = "";
                                if (revisado_por.SelectedValue.ToString() != "-")
                                { REPOP = "por " + revisado_por.SelectedValue.ToString(); }
                                subject = "Reparación completada, parte " + Convert.ToInt16(lista_partes.Value) + " de reparación de molde.";
                                Body = "<strong>Reparación validada como correcta " + REPOP + ".</strong><br>Parte " + Convert.ToInt16(lista_partes.Value) + ".<br><strong>Molde:</strong> " + Convert.ToInt32(ds_molde.Tables[0].Rows[0]["ReferenciaMolde"]) + " " + ds_molde.Tables[0].Rows[0]["Descripcion"].ToString() + "<br><strong>Avería denunciada: </strong>" + averia.Value.ToString() + "<br><strong>Acción validada: </strong>" + reparacion.Value.ToString() + "<br><a href='http://facts4-srv/thermogestion/MANTENIMIENTO/ReparacionMoldes.aspx?PARTE=" + Convert.ToInt16(lista_partes.Value) + "'>Accede al parte a través de este link.</a>";
                                break;
                        }


                        mm.Subject = subject;
                        mm.Body = string.Format(Body, inlineLogo.ContentId);

                        var view = AlternateView.CreateAlternateViewFromString(mm.Body, null, "text/html");
                        if (filename1 != "sin_imagen.jpg")
                        {
                            view.LinkedResources.Add(inlineLogo);
                        }
                        if (filename2 != "sin_imagen.jpg")
                        {
                            view.LinkedResources.Add(inlineLogo2);
                        }
                        mm.AlternateViews.Add(view);

                        mm.IsBodyHtml = true;
                        mm.Priority = MailPriority.High;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.thermolympic.es";
                        smtp.Port = 25;
                        smtp.EnableSsl = false;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential("mantenimiento@thermolympic.es", "010477Mto");
                        smtp.Send(mm);
                    }
                }
            }

            catch (Exception ex)
            {
                mandar_mail2(ex.Message + ex.StackTrace, "Error en la aplicación de mantenimiento");
            }
        
        }

        protected void MandarMailTESTING(object sender, EventArgs e)
        {

            try
            {
                string TipoParte = "Nuevo";
                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                    {
                        string URL = "http://facts4-srv/thermogestion/MANTENIMIENTO/ReparacionMoldes.aspx?PARTE=" + lista_partes.Value.ToString();
                        //contenedor1.RenderControl(hw);
                        StringReader sr = new StringReader(sw.ToString());
                        //MailMessage mm = new MailMessage("bms@thermolympic.es", "pedro@thermolympic.es");
                        MailMessage mm = new MailMessage();

                        Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();
                        DataSet ds_correos = conexion.Leer_correosMANTMOL();
                        //DataSet ds_correos = conexion.leer_correosADMIN(); //cambiar a MANTMAQ

                        foreach (DataRow row in ds_correos.Tables[0].Rows)
                        {
                            mm.To.Add(new MailAddress(row["Correo"].ToString()));
                        }

                        //var inlineLogo = new LinkedResource(Server.MapPath(""+hyperlink2.ImageUrl.ToString()+""), "image/png");
                        //string filename1 = Path.GetFileName(new Uri(img1.Src.ToString()).AbsolutePath);
                        string filename1 = Path.GetFileName(new Uri(Path.GetFullPath(img1.ImageUrl.ToString())).AbsolutePath);
                        string extension1 = Path.GetExtension(filename1).Substring(1);
                        //var inlineLogo = new LinkedResource(Server.MapPath("../SMARTH_docs/MANTENIMIENTO/" + filename1 + ""), "image/" + extension1 + "");
                        //inlineLogo.ContentId = Guid.NewGuid().ToString();
                        var inlineLogo = new LinkedResource(Server.MapPath("../SMARTH_docs/MANTENIMIENTO/sin_imagen.jpg"));
                        if (filename1 != "sin_imagen.jpg")
                        {
                            inlineLogo = new LinkedResource(Server.MapPath("../SMARTH_docs/MANTENIMIENTO/" + filename1 + ""), "image/" + extension1 + "");
                        }
                        inlineLogo.ContentId = Guid.NewGuid().ToString();

                        //string filename2 = Path.GetFileName(new Uri(img2.Src.ToString()).AbsolutePath);
                        string filename2 = Path.GetFileName(new Uri(Path.GetFullPath(img2.ImageUrl.ToString())).AbsolutePath);
                        string extension2 = Path.GetExtension(filename2).Substring(1);
                        var inlineLogo2 = new LinkedResource(Server.MapPath("../SMARTH_docs/MANTENIMIENTO/sin_imagen.jpg"));
                        if (filename2 != "sin_imagen.jpg")
                        {
                            inlineLogo2 = new LinkedResource(Server.MapPath("../SMARTH_docs/MANTENIMIENTO/" + filename2 + ""), "image/" + extension2 + "");
                        }
                        inlineLogo2.ContentId = Guid.NewGuid().ToString();

                        mm.From = new MailAddress("mantenimiento@thermolympic.es");
                        //mm.To.Add(new MailAddress("pedro@thermolympic.es"));
                        string subject = "";
                        string Body = "";
                        DataSet ds = conexion.Devuelve_parte_Molde(lista_partes.Value.ToString());
                        DataSet ds_molde = conexion.Devuelve_molde(conexion.Devuelve_IDMolde(tbMoldeNew.Value.Split('¬')[0].ToString()));
                        switch (TipoParte)
                        {
                            case "Nuevo":
                                subject = "Nuevo parte " + Convert.ToInt16(lista_partes.Value) + " de reparación de molde.";
                                Body = "<strong>Molde a reparar: </strong>" + Convert.ToInt32(ds_molde.Tables[0].Rows[0]["ReferenciaMolde"]) + " " + ds_molde.Tables[0].Rows[0]["Descripcion"].ToString() + "<br><strong>Avería denunciada: </strong>" + averia.Value.ToString() + "<br>Abierto por: " + encargado.SelectedValue.ToString() + "<br><a href='http://facts4-srv/thermogestion/MANTENIMIENTO/ReparacionMoldes.aspx?PARTE=" + Convert.ToInt16(lista_partes.Value) + "'>Accede al parte a través de este link.</a>";
                                break;

                            case "Reparado":
                                string REPOP = "";
                                if (ReparadoPor3.Value != "-")
                                { REPOP = "por " + ReparadoPor1.Value + ", " + ReparadoPor2.Value + " y " + ReparadoPor3.Value; }
                                else if (ReparadoPor2.Value != "-")
                                { REPOP = "por " + ReparadoPor1.Value + " y " + ReparadoPor2.Value; }
                                else if (ReparadoPor1.Value != "-")
                                { REPOP = "por " + ReparadoPor1.Value; }
                                else { }
                                string REPOP2 = "";
                                if (encargado.SelectedValue.ToString() != "-")
                                { REPOP2 = "por " + encargado.SelectedValue.ToString(); }
                                subject = "Reparación realizada pendiente de validar " + REPOP2 + ". Parte " + Convert.ToInt16(lista_partes.Value) + " de molde.";
                                Body = "<strong>Reparación realizada " + REPOP + " pendiente de validación " + REPOP2 + ".</strong><br>Parte " + Convert.ToInt16(lista_partes.Value) + ".<br><strong>Molde:</strong> " + Convert.ToInt32(ds_molde.Tables[0].Rows[0]["ReferenciaMolde"]) + " " + ds_molde.Tables[0].Rows[0]["Descripcion"].ToString() + "<br><strong>Avería denunciada: </strong>" + averia.Value.ToString() + "<br><strong>Acción pendiente de validar: </strong>" + reparacion.Value.ToString() + " <br><a href='http://facts4-srv/thermogestion/MANTENIMIENTO/ReparacionMoldes.aspx?PARTE=" + Convert.ToInt16(lista_partes.Value) + "'>Accede al parte a través de este link.</a>";
                                break;

                            case "ValidadoNC":
                                REPOP = "";
                                if (revisado_por.SelectedValue.ToString() != "-")
                                { REPOP = "por " + revisado_por.SelectedValue.ToString(); }
                                subject = "Revisión NOK. Parte " + Convert.ToInt16(lista_partes.Value) + " de reparación de molde.";
                                Body = "<strong>Reparación validada como no conforme " + REPOP + ".</strong><br>Parte " + Convert.ToInt16(lista_partes.Value) + ".<br><strong>Molde:</strong> " + Convert.ToInt32(ds_molde.Tables[0].Rows[0]["ReferenciaMolde"]) + " " + ds_molde.Tables[0].Rows[0]["Descripcion"].ToString() + "<br><strong>Avería denunciada: </strong>" + averia.Value.ToString() + "<br><strong>Reparación no conforme: </strong>" + reparacion.Value.ToString() + " <br><a href='http://facts4-srv/thermogestion/MANTENIMIENTO/ReparacionMoldes.aspx?PARTE=" + Convert.ToInt16(lista_partes.Value) + "'>Accede al parte a través de este link.</a>";
                                break;

                            case "Validado":
                                REPOP = "";
                                if (revisado_por.SelectedValue.ToString() != "-")
                                { REPOP = "por " + revisado_por.SelectedValue.ToString(); }
                                subject = "Reparación completada, parte " + Convert.ToInt16(lista_partes.Value) + " de reparación de molde.";
                                Body = "<strong>Reparación validada como correcta " + REPOP + ".</strong><br>Parte " + Convert.ToInt16(lista_partes.Value) + ".<br><strong>Molde:</strong> " + Convert.ToInt32(ds_molde.Tables[0].Rows[0]["ReferenciaMolde"]) + " " + ds_molde.Tables[0].Rows[0]["Descripcion"].ToString() + "<br><strong>Avería denunciada: </strong>" + averia.Value.ToString() + "<br><strong>Acción validada: </strong>" + reparacion.Value.ToString() + "<br><a href='http://facts4-srv/thermogestion/MANTENIMIENTO/ReparacionMoldes.aspx?PARTE=" + Convert.ToInt16(lista_partes.Value) + "'>Accede al parte a través de este link.</a>";
                                break;
                        }


                        mm.Subject = subject;
                        mm.Body = string.Format(Body, inlineLogo.ContentId);

                        var view = AlternateView.CreateAlternateViewFromString(mm.Body, null, "text/html");
                        if (filename1 != "sin_imagen.jpg")
                        {
                            view.LinkedResources.Add(inlineLogo);
                        }
                        if (filename2 != "sin_imagen.jpg")
                        {
                            view.LinkedResources.Add(inlineLogo2);
                        }
                        mm.AlternateViews.Add(view);

                        mm.IsBodyHtml = true;
                        mm.Priority = MailPriority.High;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.thermolympic.es";
                        smtp.Port = 25;
                        smtp.EnableSsl = false;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential("mantenimiento@thermolympic.es", "010477Mto");
                        //smtp.Send(mm);
                    }
                }
            }

            catch (Exception ex)
            {
                mandar_mail2(ex.Message + ex.StackTrace, "Error en la aplicación de mantenimiento");
            }

        }

        public void mandar_mail2(string mensaje, string subject)
        {
            //Create MailMessage
            MailMessage email = new MailMessage();
            email.To.Add(new MailAddress("pedro@thermolympic.es"));
            email.From = new MailAddress("bms@thermolympic.es");
            email.Subject = subject + DateTime.Now.ToString("dd/MMM/yyy hh:mm:ss");
            email.Body = mensaje;
            email.IsBodyHtml = true;
            email.Priority = MailPriority.Normal;

            //Definir objeto SmtpClient
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.thermolympic.es";
            smtp.Port = 25;
            smtp.EnableSsl = false;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("bms@thermolympic.es", "010477Bms");

            //Enviar email
            try
            {
                smtp.Send(email);
                email.Dispose();
            }
            catch (Exception ex)
            {
            }
        }
     
    }
}