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
    public partial class ReparacionMaquinas : System.Web.UI.Page
    {
        SqlConnection cnnPROG = new SqlConnection();
        private static DataSet ds = new DataSet();
        private static int pag_actual = 1;
        private static int reparado_ant = 0;
        private static int revisado_ant = 0;
        private static int revisadoNOK_ant = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargar_datos(pag_actual);
                if (Request.QueryString["PARTE"] != null)
                {
                    tbBuscar.Value = Request.QueryString["PARTE"].ToString();
                    buscarParteCarga();
                }
            }
            cargar_pendientes();
            cargar_info();
        }

        public void cargar_datos(int pag_actual)
        {
            try
            {
                Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();
                DataSet estadoreparacion = new DataSet();


                //**\\CARGA DE DATASETS Y RELLENA DROPDOWNS//**\\
                //REGISTRO
                DataSet prioridades = conexion.devuelve_lista_prioridades();
                prioridad.Items.Clear();
                foreach (DataRow row in prioridades.Tables[0].Rows)
                {
                    prioridad.Items.Add(row["Prioridad"].ToString());
                }

                DataSet maquinas = conexion.devuelve_lista_maquinas();
                lista_maquinas.Items.Clear();
                foreach (DataRow row in maquinas.Tables[0].Rows)
                {
                    lista_maquinas.Items.Add(row["Maquina"].ToString());
                }

                DataSet perifericos = conexion.devuelve_lista_perifericos();
                lista_perifericos.Items.Clear();
                foreach (DataRow row in perifericos.Tables[0].Rows)
                {
                    lista_perifericos.Items.Add(row["Máquina"].ToString());
                }

                DataSet instalaciones = conexion.devuelve_lista_instalaciones();
                instalacion.Items.Clear();
                foreach (DataRow row in instalaciones.Tables[0].Rows)
                {
                    instalacion.Items.Add(row["Instalacion"].ToString());
                }

                DataSet tipo_trabajo = conexion.tipos_trabajo();
                lista_trabajos.Items.Clear();
                foreach (DataRow row in tipo_trabajo.Tables[0].Rows)
                {
                    lista_trabajos.Items.Add(row["Nombre"].ToString());
                }

                DataSet Personal = conexion.Devuelve_mandos_intermedios_SMARTH();

                DataSet EncargadosAbiertoPor = Personal; //AbiertoPorV2
                EncargadosAbiertoPor.Tables[0].DefaultView.RowFilter = "Departamento <> 'ADMINISTRADOR' AND Departamento <> 'OFICINAS' AND Departamento <> 'CALIDAD'";
                DataTable DTEncargadosAbiertoPor = EncargadosAbiertoPor.Tables[0].DefaultView.ToTable();
                encargado.Items.Clear();
                revisado_por.Items.Clear();
                foreach (DataRow row in DTEncargadosAbiertoPor.Rows)
                {
                    encargado.Items.Add(row["Nombre"].ToString());
                    revisado_por.Items.Add(row["Nombre"].ToString());
                }



                //REPARACION
                DataSet operario_mantenimiento = Personal;
                operario_mantenimiento.Tables[0].DefaultView.RowFilter = "Departamento = 'MANTENIMIENTO' OR Departamento = '-' OR Departamento = 'Z'";
                DataTable DToperario_mantenimiento = (operario_mantenimiento.Tables[0].DefaultView).ToTable();
                lista_realizadoPorNEW.Items.Clear();
                foreach (DataRow row in DToperario_mantenimiento.Rows)
                {
                    lista_realizadoPorNEW.Items.Add(row["Nombre"].ToString());
                }

                //PARCHEAR VALOR Y DESHABILITAR
                DataSet turnos = conexion.devuelve_lista_turnos();
                turno.Items.Clear();
                foreach (DataRow row in turnos.Tables[0].Rows)
                {
                    turno.Items.Add(row["Turno"].ToString());
                }

                DataSet op_mantenimiento = conexion.realizadoPor();
                reparar_por.Items.Clear();
                foreach (DataRow row in op_mantenimiento.Tables[0].Rows)
                {
                    reparar_por.Items.Add(row["Nombre"].ToString());
                }

                lista_realizadoPor.Items.Clear();
                foreach (DataRow row in op_mantenimiento.Tables[0].Rows)
                {
                    lista_realizadoPor.Items.Add(row["Nombre"].ToString());
                }

                DataSet tipo_reparacion = conexion.tipos_reparacion();

                //**\\RELLENAR PARTE//**\\
                //CARGA INICIAL

                ds = conexion.tablaReparacionMaquinas();

                //BARRA DE ESTADO

                int estadoreparado = Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["Terminado"]);
                int estadovalidado = Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["Revisado"]);
                int estadovalidadonok = Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["RevisadoNOK"]);
                DropEstado.SelectedValue = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["IdEstadoReparacion"].ToString();
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

                //BLOQUE REGISTRO
                lista_partes.Value = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["IdMantenimiento"].ToString();

                prioridad.ClearSelection();
                prioridad.SelectedValue = conexion.devuelve_prioridad(Convert.ToInt16(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["IdPrioridad"].ToString()));

                DataSet maquina = conexion.devuelve_maquina(Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["IDMaquina"].ToString()));
                lista_maquinas.ClearSelection();
                lista_maquinas.SelectedValue = maquina.Tables[0].Rows[0]["Maquina"].ToString();

                lista_perifericos.ClearSelection();
                lista_perifericos.SelectedValue = conexion.devuelve_periferico(Convert.ToInt16(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["IDPeriferico"].ToString()));

                instalacion.ClearSelection();
                instalacion.SelectedValue = conexion.devuelve_instalacion(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["IDInstalacion"].ToString());


                lista_trabajos.ClearSelection();
                lista_trabajos.SelectedValue = conexion.devuelve_tipo_trabajo(Convert.ToInt16(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["IdTipoMantenimiento"].ToString()));

                averia.Value = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["MotivoAveria"].ToString();



                string imagen1 = conexion.devuelve_imagenes_maquinas(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["IdMantenimiento"].ToString(), 1);
                string imagen2 = conexion.devuelve_imagenes_maquinas(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["IdMantenimiento"].ToString(), 2);
                string imagen3 = conexion.devuelve_imagenes_maquinas(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["IdMantenimiento"].ToString(), 3);
                if (imagen1 != "")
                {
                    img1.Src = imagen1;
                }
                else
                {
                    img1.Src = "http://FACTS4-SRV/oftecnica/imagenes2/directorio/sin_imagen.jpg";
                }
                if (imagen2 != "")
                {
                    img2.Src = imagen2;
                }
                else
                {
                    img2.Src = "http://FACTS4-SRV/oftecnica/imagenes2/directorio/sin_imagen.jpg";
                }
                if (imagen3 != "")
                {
                    img3.Src = imagen3;
                }
                else
                {
                    img3.Src = "http://FACTS4-SRV/oftecnica/imagenes2/directorio/sin_imagen.jpg";
                }



                date_apertura.Value = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["FechaAveria"].ToString();

                date_prox.Value = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["FechaProximaProduccion"].ToString();

                encargado.ClearSelection();
                encargado.SelectedValue = conexion.Devuelve_NombreMandoIntermedio(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["IdEncargado"].ToString());



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
                    if (lista_maquinas.SelectedValue.ToString() == "Sin máquina")
                    {
                        IDTipoMantPreventivo = 1;
                    }
                    else
                    {
                        string IDMaquinaCHAR = conexion.devuelve_IDmaquinaCHAR(lista_maquinas.SelectedValue.ToString());
                        IDTipoMantPreventivo = conexion.Devuelve_IDMantenimiento_BMS(IDMaquinaCHAR);
                    }

                    if (IDTipoMantPreventivo > 0)
                    {
                        DataSet DropTiposPreventivos = conexion.Devuelve_Listado_PreventivosXMaquina(IDTipoMantPreventivo);
                        DropTipoPreventivo.Items.Clear();
                        DropTipoPreventivo.Items.Add("");
                        foreach (DataRow row in DropTiposPreventivos.Tables[0].Rows)
                        {
                            DropTipoPreventivo.Items.Add(row["TipoFrecuencia"].ToString());
                        }
                        DropTipoPreventivo.SelectedValue = conexion.Devuelve_Tipo_Mantenimiento_Seleccionado_PREVMAQ_DS(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["IDTipoMantPreventivo"].ToString());
                    }
                    else
                    {


                    }


                    DataSet ListaPreguntas = conexion.Devuelve_Listado_Preguntas_PREVMAQ();
                    DataSet CuestionPreventiva = conexion.Devuelve_Tipo_Mantenimiento_Seleccionado_PREVMAQ(DropTipoPreventivo.SelectedValue.ToString());
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
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q3INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q3Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }


                    Q4INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoAcciones"].ToString().Split(' ')[3];
                    if (Q4INT.Text != "1")
                    {
                        Q4LINEA.Visible = true;
                        Q4DROP.ClearSelection();
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q4INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q4Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }


                    Q5INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoAcciones"].ToString().Split(' ')[4];
                    if (Q5INT.Text != "1")
                    {
                        Q5LINEA.Visible = true;
                        Q5DROP.ClearSelection();
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q5INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q5Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }


                    Q6INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoAcciones"].ToString().Split(' ')[5];
                    if (Q6INT.Text != "1")
                    {
                        Q6LINEA.Visible = true;
                        Q6DROP.ClearSelection();
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q6INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q6Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }


                    Q7INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoAcciones"].ToString().Split(' ')[6];
                    if (Q7INT.Text != "1")
                    {
                        Q7LINEA.Visible = true;
                        Q7DROP.ClearSelection();
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q7INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q7Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }


                    Q8INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoAcciones"].ToString().Split(' ')[7];
                    if (Q8INT.Text != "1")
                    {
                        Q8LINEA.Visible = true;
                        Q8DROP.ClearSelection();
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q8INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q8Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }


                    Q9INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoAcciones"].ToString().Split(' ')[8];
                    if (Q9INT.Text != "1")
                    {
                        Q9LINEA.Visible = true;
                        Q9DROP.ClearSelection();
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q9INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q9Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }


                    Q10INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoAcciones"].ToString().Split(' ')[9];
                    if (Q10INT.Text != "1")
                    {
                        Q10LINEA.Visible = true;
                        Q10DROP.ClearSelection();
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q10INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q10Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }


                    Q11INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoAcciones"].ToString().Split(' ')[10];
                    if (Q11INT.Text != "1")
                    {
                        Q11LINEA.Visible = true;
                        Q11DROP.ClearSelection();
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q11INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q11Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }

                    Q12INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoAcciones"].ToString().Split(' ')[11];
                    if (Q12INT.Text != "1")
                    {
                        Q12LINEA.Visible = true;
                        Q12DROP.ClearSelection();
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q12INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q12Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }


                    Q13INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoAcciones"].ToString().Split(' ')[12];
                    if (Q13INT.Text != "1")
                    {
                        Q13LINEA.Visible = true;
                        Q13DROP.ClearSelection();
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q13INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q13Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }

                    Q14INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoAcciones"].ToString().Split(' ')[13];
                    if (Q14INT.Text != "1")
                    {
                        Q14LINEA.Visible = true;
                        Q14DROP.ClearSelection();
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q14INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q14Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }


                    Q15INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoAcciones"].ToString().Split(' ')[14];
                    if (Q15INT.Text != "1")
                    {
                        Q15LINEA.Visible = true;
                        Q15DROP.ClearSelection();
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q15INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q15Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }


                    Q16INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoAcciones"].ToString().Split(' ')[15];
                    if (Q16INT.Text != "1")
                    {
                        Q16LINEA.Visible = true;
                        Q16DROP.ClearSelection();
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q16INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q16Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }


                    Q17INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoAcciones"].ToString().Split(' ')[16];
                    if (Q17INT.Text != "1")
                    {
                        Q17LINEA.Visible = true;
                        Q17DROP.ClearSelection();
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q17INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q17Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }


                    Q18INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoAcciones"].ToString().Split(' ')[17];
                    if (Q18INT.Text != "1")
                    {
                        Q18LINEA.Visible = true;
                        Q18DROP.ClearSelection();
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q18INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q18Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }


                    Q19INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoAcciones"].ToString().Split(' ')[18];
                    if (Q19INT.Text != "1")
                    {
                        Q19LINEA.Visible = true;
                        Q19DROP.ClearSelection();
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q19INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q19Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }


                    Q20INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["PreventivoAcciones"].ToString().Split(' ')[19];
                    if (Q20INT.Text != "1")
                    {
                        Q20LINEA.Visible = true;
                        Q20DROP.ClearSelection();
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q20INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q20Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }
                } //lista trabajos = "Mant.Preventivo"

                //DESHABILITAR
                turno.ClearSelection();
                turno.SelectedValue = conexion.devuelve_turno(Convert.ToInt16(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["IdTurno"].ToString()));

                reparar_por.ClearSelection();
                reparar_por.SelectedValue = conexion.devuelve_opMantenimiento(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["IdOperario"].ToString());

                lista_realizadoPor.ClearSelection();
                lista_realizadoPor.SelectedValue = conexion.devuelve_opMantenimiento(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["IdRealizadoPor"].ToString());//obsoletar al terminar


                //BLOQUE REPARACION

                datetime_ini2.Value = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["FechaInicioReparacion"].ToString();
                datetime_rep2.Value = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["FechaFinalizacionReparacion"].ToString();
                if ((bool)ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["Terminado"])
                {
                    terminado.Attributes.Add("class", "btn btn2 btn-success2");
                    reparado.Checked = false;
                    reparado_ant = 1;
                }
                else
                {
                    terminado.Attributes.Add("class", "btn btn2 btn-success2 active");
                    reparado.Checked = true;
                    reparado_ant = 0;
                }


                ColAsignado2.Visible = false;
                ColAsignado3.Visible = false;
                Asignado1.Text = conexion.Devuelve_NombreMandoIntermedio(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["AsignadoA"].ToString().Split(' ')[0]);
                Asignado2.Text = conexion.Devuelve_NombreMandoIntermedio(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["AsignadoA"].ToString().Split(' ')[1]);
                if (Asignado2.Text != "-") { ColAsignado2.Visible = true; }
                Asignado3.Text = conexion.Devuelve_NombreMandoIntermedio(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["AsignadoA"].ToString().Split(' ')[2]);
                if (Asignado3.Text != "-") { ColAsignado3.Visible = true; }
                Horasprevistas.Value = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["HorasEstimadasReparacion"].ToString();


                reparacion.Value = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["Reparacion"].ToString();
                observaciones.Value = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["Observaciones"].ToString();

                BorrarReparadoPor2.Visible = false;
                ReparadoPor2.Visible = false;
                horas2.Visible = false;
                BorrarReparadoPor3.Visible = false;
                ReparadoPor3.Visible = false;
                horas3.Visible = false;

                ReparadoPor1.Text = conexion.Devuelve_NombreMandoIntermedio(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["ReparadoPorOP"].ToString().Split(' ')[0]);
                ReparadoPor2.Text = conexion.Devuelve_NombreMandoIntermedio(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["ReparadoPorOP"].ToString().Split(' ')[1]);
                if (ReparadoPor2.Text != "-") { BorrarReparadoPor2.Visible = true; ReparadoPor2.Visible = true; horas2.Visible = true; }
                ReparadoPor3.Text = conexion.Devuelve_NombreMandoIntermedio(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["ReparadoPorOP"].ToString().Split(' ')[2]);
                if (ReparadoPor3.Text != "-") { BorrarReparadoPor3.Visible = true; ReparadoPor3.Visible = true; horas3.Visible = true; }
                horas1.Value = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["HorasRealesRepOP1"].ToString();
                horas2.Value = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["HorasRealesRepOP2"].ToString();
                horas3.Value = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["HorasRealesRepOP3"].ToString();


                TbCostes.Value = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["ImporteEmpresa1"].ToString();
                TbCostesTotales.Value = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["ImporteEmpreza3"].ToString();

                //BLOQUE REVISION

                if ((bool)ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["Revisado"])
                {
                    revisado_label.Attributes.Add("class", "btn btn2 btn-success2");
                    revisado.Checked = false;
                    revisado_ant = 1;
                }
                else
                {
                    revisado_label.Attributes.Add("class", "btn btn2 btn-success2 active");
                    revisado.Checked = true;
                    revisado_ant = 0;
                }
                if ((bool)ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["RevisadoNOK"])
                {
                    revisadoNOK_label.Attributes.Add("class", "btn btn2 btn-success2");
                    revisadoNOK.Checked = false;
                    revisadoNOK_ant = 1;
                }
                else
                {
                    revisadoNOK_label.Attributes.Add("class", "btn btn2 btn-success2 active");
                    revisadoNOK.Checked = true;
                    revisadoNOK_ant = 0;
                }

                revisado_por.SelectedValue = conexion.Devuelve_NombreMandoIntermedio(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["RevisadoPor"].ToString());
                date_revision2.Value = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["FechaRevision"].ToString();
                observaciones_revision.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["ObservacionesRevision"].ToString();


            }
            catch (Exception e)
            {
            }

        }

        public void cargar_parte(DataSet ds)
        {
            try
            {
                Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();

                //**\\CARGA DE DATASETS Y RELLENA DROPDOWNS//**\\
                //REGISTRO
                DataSet prioridades = conexion.devuelve_lista_prioridades();
                prioridad.Items.Clear();
                foreach (DataRow row in prioridades.Tables[0].Rows)
                {
                    prioridad.Items.Add(row["Prioridad"].ToString());
                }

                DataSet maquinas = conexion.devuelve_lista_maquinas();
                lista_maquinas.Items.Clear();
                foreach (DataRow row in maquinas.Tables[0].Rows)
                {
                    lista_maquinas.Items.Add(row["Maquina"].ToString());
                }

                DataSet perifericos = conexion.devuelve_lista_perifericos();
                lista_perifericos.Items.Clear();
                foreach (DataRow row in perifericos.Tables[0].Rows)
                {
                    lista_perifericos.Items.Add(row["Máquina"].ToString());
                }

                DataSet instalaciones = conexion.devuelve_lista_instalaciones();
                instalacion.Items.Clear();
                foreach (DataRow row in instalaciones.Tables[0].Rows)
                {
                    instalacion.Items.Add(row["Instalacion"].ToString());
                }

                DataSet tipo_trabajo = conexion.tipos_trabajo();
                lista_trabajos.Items.Clear();
                foreach (DataRow row in tipo_trabajo.Tables[0].Rows)
                {
                    lista_trabajos.Items.Add(row["Nombre"].ToString());
                }

                DataSet Personal = conexion.Devuelve_mandos_intermedios_SMARTH();
                DataSet operario_mantenimiento = Personal;
                operario_mantenimiento.Tables[0].DefaultView.RowFilter = "Departamento = 'MANTENIMIENTO' OR Departamento = '-'  OR Departamento = 'Z'";
                DataTable DToperario_mantenimiento = (operario_mantenimiento.Tables[0].DefaultView).ToTable();
                lista_realizadoPorNEW.Items.Clear();
                foreach (DataRow row in DToperario_mantenimiento.Rows)
                {
                    lista_realizadoPorNEW.Items.Add(row["Nombre"].ToString());
                }

                DataSet EncargadosAbiertoPor = Personal; //AbiertoPorV2
                EncargadosAbiertoPor.Tables[0].DefaultView.RowFilter = "Departamento <> 'ADMINISTRADOR' AND Departamento <> 'OFICINAS' AND Departamento <> 'CALIDAD'";
                DataTable DTEncargadosAbiertoPor = (EncargadosAbiertoPor.Tables[0].DefaultView).ToTable();
                encargado.Items.Clear();
                revisado_por.Items.Clear();
                foreach (DataRow row in DTEncargadosAbiertoPor.Rows)
                {
                    encargado.Items.Add(row["Nombre"].ToString());
                    revisado_por.Items.Add(row["Nombre"].ToString());
                }

                DataSet tipo_reparacion = conexion.tipos_reparacion();

                //DESHABILITAR

                DataSet turnos = conexion.devuelve_lista_turnos();
                DataSet ubicaciones = conexion.devuelve_lista_ubicaciones();
                turno.Items.Clear();
                foreach (DataRow row in turnos.Tables[0].Rows)
                {
                    turno.Items.Add(row["Turno"].ToString());
                }

                //BARRA DE ESTADO

                int estadoreparado = Convert.ToInt32(ds.Tables[0].Rows[0]["Terminado"]);
                int estadovalidado = Convert.ToInt32(ds.Tables[0].Rows[0]["Revisado"]);
                int estadovalidadonok = Convert.ToInt32(ds.Tables[0].Rows[0]["RevisadoNOK"]);
                DropEstado.SelectedValue = ds.Tables[0].Rows[0]["IdEstadoReparacion"].ToString();
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


                //BLOQUE DE REGISTRO
                lista_partes.Value = ds.Tables[0].Rows[0]["IdMantenimiento"].ToString();

                prioridad.ClearSelection();
                prioridad.SelectedValue = conexion.devuelve_prioridad(Convert.ToInt16(ds.Tables[0].Rows[0]["IdPrioridad"].ToString()));



                DataSet maquina = conexion.devuelve_maquina(Convert.ToInt32(ds.Tables[0].Rows[0]["IDMaquina"].ToString()));
                lista_maquinas.ClearSelection();
                lista_maquinas.SelectedValue = maquina.Tables[0].Rows[0]["Maquina"].ToString();

                lista_perifericos.ClearSelection();
                lista_perifericos.SelectedValue = conexion.devuelve_periferico(Convert.ToInt16(ds.Tables[0].Rows[0]["IDPeriferico"].ToString()));

                instalacion.ClearSelection();
                instalacion.SelectedValue = conexion.devuelve_instalacion(ds.Tables[0].Rows[0]["IDInstalacion"].ToString());



                lista_trabajos.ClearSelection();
                lista_trabajos.SelectedValue = conexion.devuelve_tipo_trabajo(Convert.ToInt16(ds.Tables[0].Rows[0]["IdTipoMantenimiento"].ToString()));

                averia.Value = ds.Tables[0].Rows[0]["MotivoAveria"].ToString();


                string imagen1 = conexion.devuelve_imagenes_maquinas(ds.Tables[0].Rows[0]["IdMantenimiento"].ToString(), 1);
                string imagen2 = conexion.devuelve_imagenes_maquinas(ds.Tables[0].Rows[0]["IdMantenimiento"].ToString(), 2);
                string imagen3 = conexion.devuelve_imagenes_maquinas(ds.Tables[0].Rows[0]["IdMantenimiento"].ToString(), 3);
                if (imagen1 != "")
                {
                    img1.Src = imagen1;
                }
                else
                {
                    img1.Src = "http://FACTS4-SRV/oftecnica/imagenes2/directorio/sin_imagen.jpg";
                }
                if (imagen2 != "")
                {
                    img2.Src = imagen2;
                }
                else
                {
                    img2.Src = "http://FACTS4-SRV/oftecnica/imagenes2/directorio/sin_imagen.jpg";
                }
                if (imagen3 != "")
                {
                    img3.Src = imagen3;
                }
                else
                {
                    img3.Src = "http://FACTS4-SRV/oftecnica/imagenes2/directorio/sin_imagen.jpg";
                }

                date_apertura.Value = ds.Tables[0].Rows[0]["FechaAveria"].ToString();
                date_prox.Value = ds.Tables[0].Rows[0]["FechaProximaProduccion"].ToString();


                encargado.ClearSelection();
                encargado.SelectedValue = conexion.Devuelve_NombreMandoIntermedio(ds.Tables[0].Rows[0]["IdEncargado"].ToString());

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
                    if (lista_maquinas.SelectedValue.ToString() == "Sin máquina")
                    {
                        IDTipoMantPreventivo = 1;
                    }
                    else
                    {
                        string IDMaquinaCHAR = conexion.devuelve_IDmaquinaCHAR(lista_maquinas.SelectedValue.ToString());
                        IDTipoMantPreventivo = conexion.Devuelve_IDMantenimiento_BMS(IDMaquinaCHAR);
                    }
                    //string IDMaquinaCHAR = conexion.devuelve_IDmaquinaCHAR(lista_maquinas.SelectedValue.ToString());
                    //int IDTipoMantPreventivo = conexion.Devuelve_IDMantenimiento_BMS(IDMaquinaCHAR);
                    if (IDTipoMantPreventivo > 0)
                    {
                        DataSet DropTiposPreventivos = conexion.Devuelve_Listado_PreventivosXMaquina(IDTipoMantPreventivo);
                        DropTipoPreventivo.Items.Clear();
                        DropTipoPreventivo.Items.Add("");
                        foreach (DataRow row in DropTiposPreventivos.Tables[0].Rows)
                        {
                            DropTipoPreventivo.Items.Add(row["TipoFrecuencia"].ToString());
                        }
                        DropTipoPreventivo.SelectedValue = conexion.Devuelve_Tipo_Mantenimiento_Seleccionado_PREVMAQ_DS(ds.Tables[0].Rows[0]["IDTipoMantPreventivo"].ToString());
                    }
                    else
                    {


                    }


                    DataSet ListaPreguntas = conexion.Devuelve_Listado_Preguntas_PREVMAQ();
                    DataSet CuestionPreventiva = conexion.Devuelve_Tipo_Mantenimiento_Seleccionado_PREVMAQ(DropTipoPreventivo.SelectedValue.ToString());
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

                //DESHABILITAR
                turno.ClearSelection();
                turno.SelectedValue = conexion.devuelve_turno(Convert.ToInt16(ds.Tables[0].Rows[0]["IdTurno"].ToString()));
                reparar_por.ClearSelection();
                reparar_por.SelectedValue = conexion.devuelve_opMantenimiento(ds.Tables[0].Rows[0]["IdOperario"].ToString());
                lista_realizadoPor.SelectedValue = conexion.devuelve_opMantenimiento(ds.Tables[0].Rows[0]["IdRealizadoPor"].ToString()); //obsoletar al terminar


                //REPARACION

                datetime_ini2.Value = ds.Tables[0].Rows[0]["FechaInicioReparacion"].ToString();
                datetime_rep2.Value = ds.Tables[0].Rows[0]["FechaFinalizacionReparacion"].ToString();
                if ((bool)ds.Tables[0].Rows[0]["Terminado"])
                {
                    terminado.Attributes.Add("class", "btn btn2 btn-success2");
                    reparado.Checked = false;
                    reparado_ant = 1;
                }
                else
                {
                    terminado.Attributes.Add("class", "btn btn2 btn-success2 active");
                    reparado.Checked = true;
                    reparado_ant = 0;
                }



                ColAsignado2.Visible = false;
                ColAsignado3.Visible = false;
                Asignado1.Text = conexion.Devuelve_NombreMandoIntermedio(ds.Tables[0].Rows[0]["AsignadoA"].ToString().Split(' ')[0]);
                Asignado2.Text = conexion.Devuelve_NombreMandoIntermedio(ds.Tables[0].Rows[0]["AsignadoA"].ToString().Split(' ')[1]);
                if (Asignado2.Text != "-") { ColAsignado2.Visible = true; }
                Asignado3.Text = conexion.Devuelve_NombreMandoIntermedio(ds.Tables[0].Rows[0]["AsignadoA"].ToString().Split(' ')[2]);
                if (Asignado3.Text != "-") { ColAsignado3.Visible = true; }
                Horasprevistas.Value = ds.Tables[0].Rows[0]["HorasEstimadasReparacion"].ToString();



                reparacion.Value = ds.Tables[0].Rows[0]["Reparacion"].ToString();
                observaciones.Value = ds.Tables[0].Rows[0]["Observaciones"].ToString();



                BorrarReparadoPor2.Visible = false;
                ReparadoPor2.Visible = false;
                horas2.Visible = false;
                BorrarReparadoPor3.Visible = false;
                ReparadoPor3.Visible = false;
                horas3.Visible = false;

                ReparadoPor1.Text = conexion.Devuelve_NombreMandoIntermedio(ds.Tables[0].Rows[0]["ReparadoPorOP"].ToString().Split(' ')[0]);
                ReparadoPor2.Text = conexion.Devuelve_NombreMandoIntermedio(ds.Tables[0].Rows[0]["ReparadoPorOP"].ToString().Split(' ')[1]);
                if (ReparadoPor2.Text != "-") { BorrarReparadoPor2.Visible = true; ReparadoPor2.Visible = true; horas2.Visible = true; }
                ReparadoPor3.Text = conexion.Devuelve_NombreMandoIntermedio(ds.Tables[0].Rows[0]["ReparadoPorOP"].ToString().Split(' ')[2]);
                if (ReparadoPor3.Text != "-") { BorrarReparadoPor3.Visible = true; ReparadoPor3.Visible = true; horas3.Visible = true; }

                horas1.Value = ds.Tables[0].Rows[0]["HorasRealesRepOP1"].ToString();
                horas2.Value = ds.Tables[0].Rows[0]["HorasRealesRepOP2"].ToString();
                horas3.Value = ds.Tables[0].Rows[0]["HorasRealesRepOP3"].ToString();


                TbCostes.Value = ds.Tables[0].Rows[0]["ImporteEmpresa1"].ToString();
                TbCostesTotales.Value = ds.Tables[0].Rows[0]["ImporteEmpreza3"].ToString();


                //REVISION

                if ((bool)ds.Tables[0].Rows[0]["Revisado"])
                {
                    revisado_label.Attributes.Add("class", "btn btn2 btn-success2");
                    revisado.Checked = false;
                    revisado_ant = 1;
                }
                else
                {
                    revisado_label.Attributes.Add("class", "btn btn2 btn-success2 active");
                    revisado.Checked = true;
                    revisado_ant = 0;
                }
                if ((bool)ds.Tables[0].Rows[0]["RevisadoNOK"])
                {
                    revisadoNOK_label.Attributes.Add("class", "btn btn2 btn-success2");
                    revisadoNOK.Checked = false;
                    revisadoNOK_ant = 1;
                }
                else
                {
                    revisadoNOK_label.Attributes.Add("class", "btn btn2 btn-success2 active");
                    revisadoNOK.Checked = true;
                    revisadoNOK_ant = 0;
                }

                date_revision2.Value = ds.Tables[0].Rows[0]["FechaRevision"].ToString();
                revisado_por.SelectedValue = conexion.Devuelve_NombreMandoIntermedio(ds.Tables[0].Rows[0]["RevisadoPor"].ToString());

                observaciones_revision.Text = ds.Tables[0].Rows[0]["ObservacionesRevision"].ToString();

                desmarcar_boton();
            }
            catch (Exception e)
            {
            }
        }

        public void irPagina(Object sender, EventArgs e)
        {
            try
            {
                Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();
       
                LinkButton button = (LinkButton)sender;
                desmarcar_boton();
                string name = button.ID;
                switch (name)
                {
                    case "btn1":
                        b1.Attributes.Add("class", "active");
                        break;
                    case "btn2":
                        b2.Attributes.Add("class", "active");
                        break;
                    case "btn3":
                        b3.Attributes.Add("class", "active");
                        break;
                    case "btn4":
                        b4.Attributes.Add("class", "active");
                        break;
                    case "btn5":
                        b5.Attributes.Add("class", "active");
                        break;
                    case "btn6":
                        b6.Attributes.Add("class", "active");
                        break;
                    case "btn7":
                        b7.Attributes.Add("class", "active");
                        break;
                    case "btn8":
                        b8.Attributes.Add("class", "active");
                        break;
                    case "btn9":
                        b9.Attributes.Add("class", "active");
                        break;
                    case "btn10":
                        b10.Attributes.Add("class", "active");
                        break;
                }

                int num_boton = Convert.ToInt16(button.Text);
                pag_actual = num_boton;

                ds = conexion.tablaReparacionMaquinas();


                //BARRAS DE ESTADO
                int estadoreparado = Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["Terminado"]);
                int estadovalidado = Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["Revisado"]);
                int estadovalidadonok = Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["RevisadoNOK"]);
                DropEstado.SelectedValue = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["IdEstadoReparacion"].ToString();

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

                lista_partes.Value = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["IdMantenimiento"].ToString();
                prioridad.ClearSelection();
                prioridad.SelectedValue = conexion.devuelve_prioridad(Convert.ToInt16(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["IdPrioridad"].ToString()));


                DataSet maquina = conexion.devuelve_maquina(Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["IDMaquina"].ToString()));

                lista_maquinas.ClearSelection();
                lista_maquinas.SelectedValue = maquina.Tables[0].Rows[0]["Maquina"].ToString();

                lista_perifericos.ClearSelection();
                lista_perifericos.SelectedValue = conexion.devuelve_periferico(Convert.ToInt16(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["IDPeriferico"].ToString()));

                instalacion.ClearSelection();
                instalacion.SelectedValue = conexion.devuelve_instalacion(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["IDInstalacion"].ToString());



                lista_trabajos.ClearSelection();
                lista_trabajos.SelectedValue = conexion.devuelve_tipo_trabajo(Convert.ToInt16(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["IdTipoMantenimiento"].ToString()));

                averia.Value = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["MotivoAveria"].ToString();



                string imagen1 = conexion.devuelve_imagenes_maquinas(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["IdMantenimiento"].ToString(), 1);
                string imagen2 = conexion.devuelve_imagenes_maquinas(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["IdMantenimiento"].ToString(), 2);
                string imagen3 = conexion.devuelve_imagenes_maquinas(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["IdMantenimiento"].ToString(), 3);
                if (imagen1 != "")
                {
                    img1.Src = imagen1;
                }
                else
                {
                    img1.Src = "http://FACTS4-SRV/oftecnica/imagenes2/directorio/sin_imagen.jpg";
                }
                if (imagen2 != "")
                {
                    img2.Src = imagen2;
                }
                else
                {
                    img2.Src = "http://FACTS4-SRV/oftecnica/imagenes2/directorio/sin_imagen.jpg";
                }
                if (imagen3 != "")
                {
                    img3.Src = imagen3;
                }
                else
                {
                    img3.Src = "http://FACTS4-SRV/oftecnica/imagenes2/directorio/sin_imagen.jpg";
                }


                date_apertura.Value = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["FechaAveria"].ToString();
                date_prox.Value = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["FechaProximaProduccion"].ToString();

                encargado.ClearSelection();
                encargado.SelectedValue = conexion.Devuelve_NombreMandoIntermedio(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["IdEncargado"].ToString());

                //BlOQUE SELECCION PREVENTIVO (CARGA DE DATASET Y DROPDOWN)
                LabelTipoPreventivo.Visible = false;
                BloquePreventivo.Visible = false;
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
                    BloquePreventivo.Visible = true;
                    LabelTipoPreventivo.Visible = true;
                    int IDTipoMantPreventivo = 0;
                    if (lista_maquinas.SelectedValue.ToString() == "Sin máquina")
                    {
                        IDTipoMantPreventivo = 1;
                    }
                    else
                    {
                        string IDMaquinaCHAR = conexion.devuelve_IDmaquinaCHAR(lista_maquinas.SelectedValue.ToString());
                        IDTipoMantPreventivo = conexion.Devuelve_IDMantenimiento_BMS(IDMaquinaCHAR);
                    }
                    //string IDMaquinaCHAR = conexion.devuelve_IDmaquinaCHAR(lista_maquinas.SelectedValue.ToString());
                    //int IDTipoMantPreventivo = conexion.Devuelve_IDMantenimiento_BMS(IDMaquinaCHAR);
                    if (IDTipoMantPreventivo > 0)
                    {
                        DataSet DropTiposPreventivos = conexion.Devuelve_Listado_PreventivosXMaquina(IDTipoMantPreventivo);
                        DropTipoPreventivo.Items.Clear();
                        DropTipoPreventivo.Items.Add("");
                        foreach (DataRow row in DropTiposPreventivos.Tables[0].Rows)
                        {
                            DropTipoPreventivo.Items.Add(row["TipoFrecuencia"].ToString());
                        }
                        DropTipoPreventivo.SelectedValue = conexion.Devuelve_Tipo_Mantenimiento_Seleccionado_PREVMAQ_DS(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["IDTipoMantPreventivo"].ToString());
                    }
                    else
                    {


                    }


                    DataSet ListaPreguntas = conexion.Devuelve_Listado_Preguntas_PREVMAQ();
                    DataSet CuestionPreventiva = conexion.Devuelve_Tipo_Mantenimiento_Seleccionado_PREVMAQ(DropTipoPreventivo.SelectedValue.ToString());
                    TextTipoPreventivo.Text = "MANTENIMIENTO PREVENTIVO: " + DropTipoPreventivo.SelectedValue.ToString();

                    Q1INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["PreventivoAcciones"].ToString().Split(' ')[0];
                    Q1DROP.ClearSelection();
                    ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q1INT.Text + "";
                    DataTable ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                    Q1Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();


                    Q2INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["PreventivoAcciones"].ToString().Split(' ')[1];
                    if (Q2INT.Text != "1")
                    {
                        Q2LINEA.Visible = true;
                        Q2DROP.ClearSelection();
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q2INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q2Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }

                    Q3INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["PreventivoAcciones"].ToString().Split(' ')[2];
                    if (Q3INT.Text != "1")
                    {
                        Q3LINEA.Visible = true;
                        Q4DROP.ClearSelection();
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q3INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q3Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }

                    Q4INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["PreventivoAcciones"].ToString().Split(' ')[3];
                    if (Q4INT.Text != "1")
                    {
                        Q4LINEA.Visible = true;
                        Q4DROP.ClearSelection();
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q4INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q4Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }

                    Q5INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["PreventivoAcciones"].ToString().Split(' ')[4];
                    if (Q5INT.Text != "1")
                    {
                        Q5LINEA.Visible = true;
                        Q5DROP.ClearSelection();
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q5INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q5Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }

                    Q6INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["PreventivoAcciones"].ToString().Split(' ')[5];
                    if (Q6INT.Text != "1")
                    {
                        Q6LINEA.Visible = true;
                        Q6DROP.ClearSelection();
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q6INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q6Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }

                    Q7INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["PreventivoAcciones"].ToString().Split(' ')[6];
                    if (Q7INT.Text != "1")
                    {
                        Q7LINEA.Visible = true;
                        Q7DROP.ClearSelection();
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q7INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q7Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }

                    Q8INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["PreventivoAcciones"].ToString().Split(' ')[7];
                    if (Q8INT.Text != "1")
                    {
                        Q8LINEA.Visible = true;
                        Q8DROP.ClearSelection();
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q8INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q8Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }

                    Q9INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["PreventivoAcciones"].ToString().Split(' ')[8];
                    if (Q9INT.Text != "1")
                    {
                        Q9LINEA.Visible = true;
                        Q9DROP.ClearSelection();
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q9INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q9Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }

                    Q10INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["PreventivoAcciones"].ToString().Split(' ')[9];
                    if (Q10INT.Text != "1")
                    {
                        Q10LINEA.Visible = true;
                        Q10DROP.ClearSelection();
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q10INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q10Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }

                    Q11INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["PreventivoAcciones"].ToString().Split(' ')[10];
                    if (Q11INT.Text != "1")
                    {
                        Q11LINEA.Visible = true;
                        Q11DROP.ClearSelection();
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q11INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q11Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }

                    Q12INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["PreventivoAcciones"].ToString().Split(' ')[11];
                    if (Q12INT.Text != "1")
                    {
                        Q12LINEA.Visible = true;
                        Q12DROP.ClearSelection();
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q12INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q12Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }

                    Q13INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["PreventivoAcciones"].ToString().Split(' ')[12];
                    if (Q13INT.Text != "1")
                    {
                        Q13LINEA.Visible = true;
                        Q13DROP.ClearSelection();
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q13INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q13Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }

                    Q14INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["PreventivoAcciones"].ToString().Split(' ')[13];
                    if (Q14INT.Text != "1")
                    {
                        Q14LINEA.Visible = true;
                        Q14DROP.ClearSelection();
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q14INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q14Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }

                    Q15INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["PreventivoAcciones"].ToString().Split(' ')[14];
                    if (Q15INT.Text != "1")
                    {
                        Q15LINEA.Visible = true;
                        Q15DROP.ClearSelection();
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q15INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q15Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }

                    Q16INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["PreventivoAcciones"].ToString().Split(' ')[15];
                    if (Q16INT.Text != "1")
                    {
                        Q16LINEA.Visible = true;
                        Q16DROP.ClearSelection();
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q16INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q16Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }

                    Q17INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["PreventivoAcciones"].ToString().Split(' ')[16];
                    if (Q17INT.Text != "1")
                    {
                        Q17LINEA.Visible = true;
                        Q17DROP.ClearSelection();
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q17INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q17Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }

                    Q18INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["PreventivoAcciones"].ToString().Split(' ')[17];
                    if (Q18INT.Text != "1")
                    {
                        Q18LINEA.Visible = true;
                        Q18DROP.ClearSelection();
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q18INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q18Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }

                    Q19INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["PreventivoAcciones"].ToString().Split(' ')[18];
                    if (Q19INT.Text != "1")
                    {
                        Q19LINEA.Visible = true;
                        Q19DROP.ClearSelection();
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q19INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q19Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }

                    Q20INT.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["PreventivoAcciones"].ToString().Split(' ')[19];
                    if (Q20INT.Text != "1")
                    {
                        Q20LINEA.Visible = true;
                        Q20DROP.ClearSelection();
                        ListaPreguntas.Tables[0].DefaultView.RowFilter = "IdCuestionario = " + Q20INT.Text + "";
                        ListaPreguntasSelec = (ListaPreguntas.Tables[0].DefaultView).ToTable();
                        Q20Text.Text = ListaPreguntasSelec.Rows[0]["Accion"].ToString();
                    }
                } //lista trabajos = "Mant.Preventivo"

                //DESHABILITAR
                turno.ClearSelection();
                turno.SelectedValue = conexion.devuelve_turno(Convert.ToInt16(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["IdTurno"].ToString()));

                reparar_por.ClearSelection();
                reparar_por.SelectedValue = conexion.devuelve_opMantenimiento(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["IdOperario"].ToString());

                lista_realizadoPor.ClearSelection();
                lista_realizadoPor.SelectedValue = conexion.devuelve_opMantenimiento(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["IdRealizadoPor"].ToString());
                //REPARACION

                if ((bool)ds.Tables[0].Rows[ds.Tables[0].Rows.Count - pag_actual]["Terminado"])
                {
                    terminado.Attributes.Add("class", "btn btn2 btn-success2");
                    reparado.Checked = false;
                }
                else
                {
                    terminado.Attributes.Add("class", "btn btn2 btn-success2 active");
                    reparado.Checked = true;
                }
                datetime_ini2.Value = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["FechaInicioReparacion"].ToString();
                datetime_rep2.Value = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["FechaFinalizacionReparacion"].ToString();
                //DataSet tipo_reparacion = conexion.tipos_reparacion();
                //DataSet op_mantenimiento = conexion.realizadoPor();


                ColAsignado2.Visible = false;
                ColAsignado3.Visible = false;
                Asignado1.Text = conexion.Devuelve_NombreMandoIntermedio(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["AsignadoA"].ToString().Split(' ')[0]);
                Asignado2.Text = conexion.Devuelve_NombreMandoIntermedio(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["AsignadoA"].ToString().Split(' ')[1]);
                if (Asignado2.Text != "-") { ColAsignado2.Visible = true; }
                Asignado3.Text = conexion.Devuelve_NombreMandoIntermedio(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["AsignadoA"].ToString().Split(' ')[2]);
                if (Asignado3.Text != "-") { ColAsignado3.Visible = true; }
                Horasprevistas.Value = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["HorasEstimadasReparacion"].ToString();


                reparacion.Value = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["Reparacion"].ToString();
                observaciones.Value = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["Observaciones"].ToString();

                horas1.Value = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["HorasRealesRepOP1"].ToString();
                horas2.Value = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["HorasRealesRepOP2"].ToString();
                horas3.Value = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["HorasRealesRepOP3"].ToString();

                TbCostes.Value = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["ImporteEmpresa1"].ToString();
                TbCostesTotales.Value = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["ImporteEmpreza3"].ToString();


                BorrarReparadoPor2.Visible = false;
                ReparadoPor2.Visible = false;
                horas2.Visible = false;
                BorrarReparadoPor3.Visible = false;
                ReparadoPor3.Visible = false;
                horas3.Visible = false;


                ReparadoPor1.Text = conexion.Devuelve_NombreMandoIntermedio(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["ReparadoPorOP"].ToString().Split(' ')[0]);
                ReparadoPor2.Text = conexion.Devuelve_NombreMandoIntermedio(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["ReparadoPorOP"].ToString().Split(' ')[1]);
                if (ReparadoPor2.Text != "-") { BorrarReparadoPor2.Visible = true; ReparadoPor2.Visible = true; horas2.Visible = true; }
                ReparadoPor3.Text = conexion.Devuelve_NombreMandoIntermedio(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["ReparadoPorOP"].ToString().Split(' ')[2]);
                if (ReparadoPor3.Text != "-") { BorrarReparadoPor3.Visible = true; ReparadoPor3.Visible = true; horas3.Visible = true; }


                //REVISION
                if ((bool)ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["Revisado"])
                {
                    revisado_label.Attributes.Add("class", "btn btn2 btn-success2");
                    revisado.Checked = false;
                }
                else
                {
                    revisado_label.Attributes.Add("class", "btn btn2 btn-success2 active");
                    revisado.Checked = true;
                }
                revisado_por.ClearSelection();
                revisado_por.SelectedValue = conexion.Devuelve_NombreMandoIntermedio(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["RevisadoPor"].ToString());
                date_revision2.Value = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["FechaRevision"].ToString();
                observaciones_revision.Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - num_boton]["ObservacionesRevision"].ToString();
            }
            catch (Exception ex)
            { }

        }

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
                    DataSet CuestionPreventiva = conexion.Devuelve_Tipo_Mantenimiento_Seleccionado_PREVMAQ(DropTipoPreventivo.SelectedValue.ToString());


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
                desmarcar_boton();
            }
            catch (Exception ex) { }
        }

        private void desmarcar_boton()
        {
            b1.Attributes.Clear();
            b2.Attributes.Clear();
            b3.Attributes.Clear();
            b4.Attributes.Clear();
            b5.Attributes.Clear();
            b6.Attributes.Clear();
            b7.Attributes.Clear();
            b8.Attributes.Clear();
            b9.Attributes.Clear();
            b10.Attributes.Clear();
        }

        void irPendiente(Object sender, EventArgs e)
        {
            HtmlAnchor a = (HtmlAnchor)sender;
            string id_aux = a.ID;
            string[] id = id_aux.Split('_');
            Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();
            DataSet ds = conexion.devuelve_parte_maquina(id[1]);
            cargar_parte(ds);
        }

        public void cargar_info()
        {
            try
            {
                info.Controls.Clear();
                var h4 = new HtmlGenericControl("h4");
                var p1 = new HtmlGenericControl("p");
                var p2 = new HtmlGenericControl("p");
                var p3 = new HtmlGenericControl("p");
                var p4 = new HtmlGenericControl("p");
                var i = new HtmlGenericControl("i");
                var li = new HtmlGenericControl("li");
                var span = new HtmlGenericControl("span");
                var div1 = new HtmlGenericControl("div");
                var div2 = new HtmlGenericControl("div");
                var a = new HtmlGenericControl("a");

                h4.Attributes["class"] = "media-heading";
                p1.InnerText = "THERMOLYMPIC S.L.";
                //i.Attributes["class"] = "fa fa-clock-o";                   
                p2.Attributes["class"] = "small text-muted";
                p2.InnerText = "Aplicación interna V.0.8.1";
                p3.Attributes["class"] = "small text-muted";
                p3.InnerText = "Email: pedro@thermolympic.es";

                div1.Attributes["class"] = "media-body";
                div1.Controls.Add(h4);
                div1.Controls.Add(p1);
                div1.Controls.Add(p2);
                div1.Controls.Add(p3);
                div2.Attributes["class"] = "media";
                div2.Controls.Add(div1);

                div2.Controls.Add(div1);

                HtmlAnchor ha = new HtmlAnchor();

                ha.HRef = "#";
                ha.Controls.Add(div2);

                li.Attributes["class"] = "message-preview";
                li.Controls.Add(ha);
                info.Controls.Add(li);
            }
            catch (Exception ex)
            {
            }
        }

        public void cargar_pendientes()
        {
            try
            {
                Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();
                DataSet ds = conexion.tablaReparacionMaquinas_pendientes();
                pendientes.Controls.Clear();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var strong = new HtmlGenericControl("strong");
                    var h4 = new HtmlGenericControl("h4");
                    var p2 = new HtmlGenericControl("p");
                    var p3 = new HtmlGenericControl("p");
                    var p4 = new HtmlGenericControl("p");
                    var i = new HtmlGenericControl("i");
                    var li = new HtmlGenericControl("li");
                    var span = new HtmlGenericControl("span");
                    var div1 = new HtmlGenericControl("div");
                    var div2 = new HtmlGenericControl("div");
                    var a = new HtmlGenericControl("a");

                    if (row["IdMaquina"].ToString() != "")
                    {
                        string ab = row["IdMaquina"].ToString();
                        DataSet maquina = conexion.devuelve_maquina(Convert.ToInt16(row["IdMaquina"]));
                        if (maquina.Tables[0].Rows.Count > 0)
                        {
                            strong.InnerText = maquina.Tables[0].Rows[0]["Maquina"].ToString();
                            h4.Attributes["class"] = "media-heading";
                            h4.Controls.Add(strong);

                            //i.Attributes["class"] = "fa fa-clock-o";                   
                            p2.Attributes["class"] = "small text-muted";
                            p2.InnerText = "Fecha avería: " + row["FechaAveria"].ToString();

                            //p2.Controls.Add(i);
                            string prioridad = conexion.devuelve_prioridad(Convert.ToInt16(row["IdPrioridad"].ToString()));
                            switch (Convert.ToInt16(row["idPrioridad"]))
                            {
                                case 1:
                                    span.InnerHtml = prioridad;
                                    span.Attributes["class"] = "label label-danger";
                                    break;
                                case 2:
                                    span.InnerHtml = prioridad;
                                    span.Attributes["class"] = "label label-warning";
                                    break;
                                case 3:
                                    span.InnerHtml = prioridad;
                                    span.Attributes["class"] = "label label-info";
                                    break;
                                case 4:
                                    span.InnerHtml = prioridad;
                                    span.Attributes["class"] = "label label-success";
                                    break;
                                case 5:
                                    span.InnerHtml = prioridad;
                                    span.Attributes["class"] = "label label-primary";
                                    break;
                                default:
                                    span.InnerHtml = "Sin prioridad";
                                    span.Attributes["class"] = "label label-primary";
                                    break;
                            }
                            div1.Attributes["class"] = "media-body";
                            div1.Controls.Add(h4);
                            div1.Controls.Add(p2);
                            div1.Controls.Add(span);
                            div1.Controls.Add(p3);
                            div2.Attributes["class"] = "media";
                            div2.Controls.Add(div1);

                            HtmlAnchor ha = new HtmlAnchor();
                            ha.ID = "id_" + Convert.ToInt16(row["IdMantenimiento"]);
                            ha.ServerClick += new EventHandler(irPendiente);  //Hookup event to method Linkme(object sender, System.EventArgs e)
                            ha.HRef = "#";
                            ha.Controls.Add(div2);

                            li.Attributes["class"] = "message-preview";
                            li.Controls.Add(ha);
                            pendientes.Controls.Add(li);
                        }
                    }


                }
            }
            catch (Exception ex)
            {
            }
        }

        public void crearNuevo(Object sender, EventArgs e)
        {
            try
            {
                Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();

                DataSet maquinas = conexion.devuelve_lista_maquinas();
                lista_maquinas.Items.Clear();
                lista_maquinas.Items.Add("");
                foreach (DataRow row in maquinas.Tables[0].Rows)
                {
                    lista_maquinas.Items.Add(row["Maquina"].ToString());
                }

                DataSet perifericos = conexion.devuelve_lista_perifericos();
                lista_perifericos.Items.Clear();
                foreach (DataRow row in perifericos.Tables[0].Rows)
                {
                    lista_perifericos.Items.Add(row["Máquina"].ToString());
                }

                DataSet instalaciones = conexion.devuelve_lista_instalaciones();
                instalacion.Items.Clear();
                foreach (DataRow row in instalaciones.Tables[0].Rows)
                {
                    instalacion.Items.Add(row["Instalacion"].ToString());
                }


                DataSet tipo_trabajo = conexion.tipos_trabajo();
                lista_trabajos.Items.Clear();
                foreach (DataRow row in tipo_trabajo.Tables[0].Rows)
                {
                    lista_trabajos.Items.Add(row["Nombre"].ToString());
                }

                DataSet prioridades = conexion.devuelve_lista_prioridades();
                prioridad.Items.Clear();
                foreach (DataRow row in prioridades.Tables[0].Rows)
                {
                    prioridad.Items.Add(row["Prioridad"].ToString());
                }

                DataSet Personal = conexion.Devuelve_mandos_intermedios_SMARTH();

                DataSet EncargadosAbiertoPor = Personal; //AbiertoPorV2
                EncargadosAbiertoPor.Tables[0].DefaultView.RowFilter = "Departamento <> 'ADMINISTRADOR' AND Departamento <> 'OFICINAS' AND Departamento <> 'CALIDAD'";
                DataTable DTEncargadosAbiertoPor = EncargadosAbiertoPor.Tables[0].DefaultView.ToTable();
                encargado.Items.Clear();
                revisado_por.Items.Clear();
                foreach (DataRow row in DTEncargadosAbiertoPor.Rows)
                {
                    encargado.Items.Add(row["Nombre"].ToString());
                    revisado_por.Items.Add(row["Nombre"].ToString());
                }
                DataSet operario_mantenimiento = Personal;
                operario_mantenimiento.Tables[0].DefaultView.RowFilter = "Departamento = 'MANTENIMIENTO' OR Departamento = '-' OR Departamento = 'Z'";
                DataTable DToperario_mantenimiento = (operario_mantenimiento.Tables[0].DefaultView).ToTable();
                lista_realizadoPorNEW.Items.Clear();
                foreach (DataRow row in DToperario_mantenimiento.Rows)
                {
                    lista_realizadoPorNEW.Items.Add(row["Nombre"].ToString());
                }


                //DESHABILITAR
                DataSet turnos = conexion.devuelve_lista_turnos();
                turno.Items.Clear();
                foreach (DataRow row in turnos.Tables[0].Rows)
                {
                    turno.Items.Add(row["Turno"].ToString());
                }

                DataSet op_mantenimiento = conexion.realizadoPor(); //obsoletar
                reparar_por.Items.Clear();
                foreach (DataRow row in op_mantenimiento.Tables[0].Rows)
                {
                    reparar_por.Items.Add(row["Nombre"].ToString());
                }

                DataSet tipo_reparacion = conexion.tipos_reparacion();
                //BARRAS ESTADO
                progressERROR.Visible = false;
                progressABIERTO.Visible = true;
                progressABIERTOINI.Visible = false;
                progressPENDIENTE.Visible = false;
                progressNOCONFORME.Visible = false;
                progressREPARADO.Visible = false;

                //REGISTRO 

                ds = conexion.tablaReparacionMaquinas();

                DataSet maquina = conexion.devuelve_maquina(Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["IDMaquina"].ToString()));

                lista_partes.Value = Convert.ToString(conexion.max_idParte_maquina() + 1);
                prioridad.ClearSelection();

                lista_maquinas.ClearSelection();
                instalacion.ClearSelection();
                lista_perifericos.ClearSelection();

                lista_trabajos.ClearSelection();
                averia.Value = "";

                img1.Src = "http://FACTS4-SRV/oftecnica/imagenes2/directorio/sin_imagen.jpg";
                img2.Src = "http://FACTS4-SRV/oftecnica/imagenes2/directorio/sin_imagen.jpg";
                img3.Src = "http://FACTS4-SRV/oftecnica/imagenes2/directorio/sin_imagen.jpg";

                date_apertura.Value = "";
                date_prox.Value = "";
                encargado.ClearSelection();



                //REPARACION

                datetime_ini2.Value = "";
                datetime_rep2.Value = "";
                lista_realizadoPorNEW.ClearSelection();

                terminado.Attributes.Add("class", "btn btn2 btn-success2 active");
                reparado.Checked = true;

                DropEstado.ClearSelection();
                ColAsignado2.Visible = false;
                ColAsignado3.Visible = false;
                Asignado1.Text = "-";
                Asignado2.Text = "-";
                Asignado3.Text = "-";
                Horasprevistas.Value = "";

                reparacion.Value = "";
                observaciones.Value = "";

                BorrarReparadoPor2.Visible = false;
                ReparadoPor2.Visible = false;
                horas2.Visible = false;
                BorrarReparadoPor3.Visible = false;
                ReparadoPor3.Visible = false;
                horas3.Visible = false;
                ReparadoPor1.Text = "-";
                ReparadoPor2.Text = "-";
                ReparadoPor3.Text = "-";
                horas1.Value = "";
                horas2.Value = "";
                horas3.Value = "";

                TbCostes.Value = "0";
                TbCostesTotales.Value = "0";


                //REVISION
                revisado_label.Attributes.Add("class", "btn btn2 btn-success2 active");
                revisado.Checked = true;
                revisadoNOK_label.Attributes.Add("class", "btn btn2 btn-success2 active");
                revisadoNOK.Checked = true;
                date_revision2.Value = "";
                revisado_por.ClearSelection();
                observaciones_revision.Text = "";


                //DESHABILITAR
                turno.ClearSelection();
                lista_realizadoPor.Items.Clear();
                lista_realizadoPor.ClearSelection();
                foreach (DataRow row in op_mantenimiento.Tables[0].Rows)
                {
                    lista_realizadoPor.Items.Add(row["Nombre"].ToString());
                }
                lista_realizadoPor.ClearSelection();

            }
            catch (Exception ex)
            {
            }
        }

        public void guardarParte(Object sender, EventArgs e)
        {
            try
            {
                string fecha_aux = date_apertura.Value;
                string fecha_aux2 = datetime_ini2.Value;
                string fecha_aux3 = datetime_rep2.Value;
                string fecha_aux4 = date_revision2.Value;
                string fecha_aux5 = date_prox.Value;
                string[] fecha_apertura = fecha_aux.Split(' ');
                if (fecha_apertura[0] == "")
                {
                    fecha_apertura[0] = DateTime.Now.ToString("dd/MM/yyy");
                }
                string[] fecha_ini = fecha_aux2.Split(' ');
                string[] fecha_rep = fecha_aux3.Split(' ');
                string[] fecha_rev = fecha_aux4.Split(' ');
                string[] fecha_prox = fecha_aux5.Split(' ');

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


                Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();

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
                AsignadoA = conexion.Devuelve_IdMandoIntermedio(Asignado1.Text).ToString() + " " + conexion.Devuelve_IdMandoIntermedio(Asignado2.Text).ToString() + " " + conexion.Devuelve_IdMandoIntermedio(Asignado3.Text).ToString();
                string ReparadoPor = "2 2 2";
                ReparadoPor = conexion.Devuelve_IdMandoIntermedio(ReparadoPor1.Text).ToString() + " " + conexion.Devuelve_IdMandoIntermedio(ReparadoPor2.Text).ToString() + " " + conexion.Devuelve_IdMandoIntermedio(ReparadoPor3.Text).ToString();
                int EstadoReparacion = Convert.ToInt32(DropEstado.SelectedValue);

                int IDTipoPreventivo = conexion.Devuelve_ID_Tipo_Mantenimiento_Seleccionado_PREVMAQ_DS(DropTipoPreventivo.SelectedValue.ToString());
                string PreventivoAcciones = Q1INT.Text + " " + Q2INT.Text + " " + Q3INT.Text + " " + Q4INT.Text + " " + Q5INT.Text + " " + Q6INT.Text + " " + Q7INT.Text + " " + Q8INT.Text + " " + Q9INT.Text + " " + Q10INT.Text + " " + Q11INT.Text + " " + Q12INT.Text + " " + Q13INT.Text + " " + Q14INT.Text + " " + Q15INT.Text + " " + Q16INT.Text + " " + Q17INT.Text + " " + Q18INT.Text + " " + Q19INT.Text + " " + Q20INT.Text;
                string PreventivoEstado = Q1DROP.SelectedValue + " " + Q2DROP.SelectedValue + " " + Q3DROP.SelectedValue + " " + Q4DROP.SelectedValue + " " + Q5DROP.SelectedValue + " " + Q6DROP.SelectedValue + " " + Q7DROP.SelectedValue + " " + Q8DROP.SelectedValue + " " + Q9DROP.SelectedValue + " " + Q10DROP.SelectedValue + " " + Q11DROP.SelectedValue + " " + Q12DROP.SelectedValue + " " + Q13DROP.SelectedValue + " " + Q14DROP.SelectedValue + " " + Q15DROP.SelectedValue + " " + Q16DROP.SelectedValue + " " + Q17DROP.SelectedValue + " " + Q18DROP.SelectedValue + " " + Q19DROP.SelectedValue + " " + Q20DROP.SelectedValue;

                if (!conexion.existe_parte_maquina(lista_partes.Value.ToString()))
                {
                    conexion.insertar_parte_maquina(Convert.ToInt16(lista_partes.Value), conexion.devuelve_IDmaquina(lista_maquinas.SelectedValue.ToString()), conexion.devuelve_IDperiferico(lista_perifericos.SelectedValue.ToString()), conexion.devuelve_IDTrabajo(lista_trabajos.SelectedValue.ToString()),
                                        conexion.Devuelve_IdMandoIntermedio(encargado.SelectedValue.ToString()), conexion.devuelve_IDPrioridad(prioridad.SelectedValue.ToString()), conexion.devuelve_IDinstalacion(instalacion.SelectedValue.ToString()), conexion.devuelve_IDopMantenimiento(reparar_por.SelectedValue.ToString()),
                                        conexion.devuelve_turno(turno.SelectedValue.ToString()), averia.Value.ToString(), fecha_apertura[0], fecha_prox[0], 0, img1.Src, img2.Src, img3.Src,
                                        fecha_ini[0], fecha_rep[0], reparacion.Value.ToString(), observaciones.Value.ToString(),
                                        conexion.devuelve_IDopMantenimiento(lista_realizadoPor.SelectedValue), horas_previstas, horas_reales, fecha_rev[0], conexion.Devuelve_IdMandoIntermedio(revisado_por.SelectedValue.ToString()), observaciones_revision.Text, 0, 0, CosteRepuestos, coste_horas, AsignadoA, ReparadoPor, EstadoReparacion, horas_ok1, horas_ok2, horas_ok3, CosteTotales, 0, "1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1", "0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0");

                    DataSet ds_maquina = conexion.devuelve_maquina(conexion.devuelve_IDmaquina(lista_maquinas.SelectedValue.ToString()));
                    string string_periferico = conexion.devuelve_periferico(conexion.devuelve_IDperiferico(lista_perifericos.SelectedValue.ToString()));

                    //mandar_mail("<strong>Máquina/periférico a reparar: </strong>" + Convert.ToString(ds_maquina.Tables[0].Rows[0]["Maquina"]) + " // "+string_periferico+"<br><strong>Avería denunciada: </strong>" + averia.Value.ToString() + "<br><a href='http://FACTS4-SRV/oftecnica/ReparacionMaquinas.aspx?PARTE=" + Convert.ToInt16(lista_partes.Value) + "'>Accede al parte a través de este link.</a>", "Nuevo parte " + Convert.ToInt16(lista_partes.Value) + " de reparación de máquina.");
                    MandarMailV2("Nuevo");
                }
                else
                {

                    DataSet ds = conexion.devuelve_parte_maquina(lista_partes.Value.ToString());
                    string TipoMail = "";
                    //reparado_ant = Convert.ToInt32(ds.Tables[0].Rows[0]["Terminado"].ToString());
                    //revisado_ant = Convert.ToInt32(ds.Tables[0].Rows[0]["Revisado"].ToString());
                    //revisadoNOK_ant = Convert.ToInt32(ds.Tables[0].Rows[0]["RevisadoNOK"].ToString());
                    int reparado_anta = Convert.ToInt32(ds.Tables[0].Rows[0]["Terminado"]);
                    int revisado_anta = Convert.ToInt32(ds.Tables[0].Rows[0]["Revisado"]);
                    int revisadoNOK_anta = Convert.ToInt32(ds.Tables[0].Rows[0]["RevisadoNOK"]);

                    //compruebo botones
                    if (reparado.Checked)
                    {
                        chek_reparado = 0;
                    }
                    else
                    {
                        chek_reparado = 1;
                    }

                    if (revisado.Checked)
                    {
                        chek_revisado = 0;
                    }
                    else
                    {
                        chek_revisado = 1;
                    }

                    if (revisadoNOK.Checked)
                    {
                        chek_revisadoNOK = 0;
                    }
                    else
                    {
                        chek_revisadoNOK = 1;
                    }
                    //probar y descomentar
                    /*
                    if (reparado_ant == 1 && revisado_ant == 0 && revisadoNOK_ant == 0 &&
                        chek_reparado == 1 && chek_revisado == 0 && chek_revisadoNOK == 1)
                        {
                        chek_reparado = 0;
                        chek_revisado = 0;
                        chek_revisadoNOK = 1;
                        }

                    else if (reparado_ant == 0 && revisado_ant == 0 && revisadoNOK_ant == 1 &&
                        chek_reparado == 1 && chek_revisado == 0 && chek_revisadoNOK == 1)
                        {
                        chek_reparado = 1;
                        chek_revisado = 0;
                        chek_revisadoNOK = 0;
                        }
                    */
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
                        //DataSet ds_maquina = conexion.devuelve_maquina(conexion.devuelve_IDmaquina(lista_maquinas.SelectedValue.ToString()));
                        //string string_periferico = conexion.devuelve_periferico(conexion.devuelve_IDperiferico(lista_perifericos.SelectedValue.ToString()));
                        //mandar_mail("<strong>Reparación realizada pendiente de validación.</strong><br>Parte " + Convert.ToInt16(lista_partes.Value) + ".<br><strong>Máquina/periférico:</strong> " + Convert.ToString(ds_maquina.Tables[0].Rows[0]["Maquina"]) + " // " + string_periferico + "<br><strong>Avería denunciada: </strong>" + averia.Value.ToString() + "<br><strong>Acción pendiente de validar: </strong>" + reparacion.Value.ToString() + " <br><a href='http://FACTS4-SRV/oftecnica/ReparacionMaquinas.aspx?PARTE=" + Convert.ToInt16(lista_partes.Value) + "'>Accede al parte a través de este link.</a>", "Reparación realizada pendiente de validar. Parte " + Convert.ToInt16(lista_partes.Value) + " de máquina/periférico.");
                        // MandarMailV2("Reparado");
                        if (chek_reparado != reparado_anta)
                        {
                            TipoMail = "Reparado";
                        }

                    }

                    else if (chek_reparado == 1 && chek_revisado == 0 && chek_revisadoNOK == 1)
                    {
                        chek_reparado = 0;
                        //DataSet ds_maquina = conexion.devuelve_maquina(conexion.devuelve_IDmaquina(lista_maquinas.SelectedValue.ToString()));
                        //string string_periferico = conexion.devuelve_periferico(conexion.devuelve_IDperiferico(lista_perifericos.SelectedValue.ToString()));
                        //mandar_mail("<strong>Reparación validada como no conforme.</strong><br>Parte " + Convert.ToInt16(lista_partes.Value) + ".<br><strong>Máquina/periférico:</strong> " + Convert.ToString(ds_maquina.Tables[0].Rows[0]["Maquina"]) + " // " + string_periferico + "<br><strong>Avería denunciada: </strong>" + averia.Value.ToString() + "<br><strong>Reparación no conforme: </strong>" + reparacion.Value.ToString() + " <br><a href='http://FACTS4-SRV/oftecnica/ReparacionMaquinas.aspx?PARTE=" + Convert.ToInt16(lista_partes.Value) + "'>Accede al parte a través de este link.</a>", "Revisión NOK. Parte " + Convert.ToInt16(lista_partes.Value) + " de reparación de máquina/periférico.");
                        //MandarMailV2("ValidadoNC");
                        if (chek_revisadoNOK != revisadoNOK_anta)
                        {
                            TipoMail = "ValidadoNC";
                        }

                    }

                    else if (chek_reparado == 1 && chek_revisado == 1 && chek_revisadoNOK == 0)
                    {
                        //DataSet ds_maquina = conexion.devuelve_maquina(conexion.devuelve_IDmaquina(lista_maquinas.SelectedValue.ToString()));
                        //string string_periferico = conexion.devuelve_periferico(conexion.devuelve_IDperiferico(lista_perifericos.SelectedValue.ToString()));
                        //mandar_mail("<strong>Reparación validada como correcta.</strong><br>Parte " + Convert.ToInt16(lista_partes.Value) + ".<br><strong>Máquina/periférico:</strong> " + Convert.ToString(ds_maquina.Tables[0].Rows[0]["Maquina"]) + " // " + string_periferico + "<br><strong>Avería denunciada: </strong>" + averia.Value.ToString() + "<br><strong>Acción validada: </strong>" + reparacion.Value.ToString() + " <br><a href='http://FACTS4-SRV/oftecnica/ReparacionMaquinas.aspx?PARTE=" + Convert.ToInt16(lista_partes.Value) + "'>Accede al parte a través de este link.</a>", "Reparación completada, parte " + Convert.ToInt16(lista_partes.Value) + " de reparación de máquina/periférico.");
                        //
                        if (chek_revisado != revisado_anta)
                        {
                            TipoMail = "Validado";
                        }

                    }
                    //corrijo errores

                    //guarda resultados

                    conexion.modificar_parte_maquina(Convert.ToInt16(lista_partes.Value), conexion.devuelve_IDmaquina(lista_maquinas.SelectedValue.ToString()), conexion.devuelve_IDperiferico(lista_perifericos.SelectedValue.ToString()), conexion.devuelve_IDTrabajo(lista_trabajos.SelectedValue.ToString()),
                                            conexion.Devuelve_IdMandoIntermedio(encargado.SelectedValue.ToString()), conexion.devuelve_IDPrioridad(prioridad.SelectedValue.ToString()), conexion.devuelve_IDinstalacion(instalacion.SelectedValue.ToString()), conexion.devuelve_IDopMantenimiento(reparar_por.SelectedValue.ToString()),
                                            conexion.devuelve_turno(turno.SelectedValue.ToString()), averia.Value.ToString(), fecha_apertura[0], fecha_prox[0], chek_reparado, img1.Src, img2.Src, img3.Src,
                                            fecha_ini[0], fecha_rep[0], reparacion.Value.ToString(), observaciones.Value.ToString(),
                                            conexion.devuelve_IDopMantenimiento(lista_realizadoPor.SelectedValue.ToString()), horas_previstas, horas_reales, fecha_rev[0], conexion.Devuelve_IdMandoIntermedio(revisado_por.SelectedValue.ToString()), observaciones_revision.Text, chek_revisado, chek_revisadoNOK, CosteRepuestos, coste_horas, AsignadoA, ReparadoPor, EstadoReparacion, horas_ok1, horas_ok2, horas_ok3, CosteTotales, IDTipoPreventivo, PreventivoAcciones, PreventivoEstado);
                    if (TipoMail != "")
                    {
                        MandarMailV2(TipoMail);
                    }
                    if (lista_trabajos.SelectedValue.ToString() == "Mant. Preventivo" && chek_reparado == 1 && chek_revisado == 1 && chek_revisadoNOK == 0)
                    {
                        conexion.reiniciar_contador(Convert.ToInt32(conexion.devuelve_IDmaquinaCHAR(lista_maquinas.SelectedValue.ToString())), Convert.ToInt32(conexion.Devuelve_Frecuencia_Tipo_Mantenimiento_MAQ(DropTipoPreventivo.SelectedValue.ToString())));
                    }
                }

                if (pag_actual != 1)
                    b1.Attributes.Clear();
                conexion.añadir_operacion_maquina(Convert.ToInt16(lista_partes.Value), conexion.devuelve_IDmaquina(lista_maquinas.SelectedValue.ToString()), chek_reparado, chek_revisado);
                cargar_pendientes();
                //cargar_datos(pag_actual);
                DataSet recargads = conexion.devuelve_parte_maquina(lista_partes.Value);
                cargar_parte(recargads);
                desmarcar_boton();
            }
            catch (Exception ex)
            {

            }
        }

        public void insertar_foto(Object sender, EventArgs e)
        {
            try
            {
                if (FileUpload1.HasFile)
                    SaveFile(FileUpload1.PostedFile, 1);

                if (FileUpload2.HasFile)
                    SaveFile(FileUpload2.PostedFile, 2);

                if (FileUpload3.HasFile)
                    SaveFile(FileUpload3.PostedFile, 3);

                if (pag_actual != 1)
                    b1.Attributes.Clear();
            }
            catch (Exception ex)
            {

            }
        }

        private void SaveFile(HttpPostedFile file, int num_img)
        {
            try
            {
                // Specify the path to save the uploaded file to.
                string savePath = "C:\\inetpub_mantenimiento\\imagenes\\";

                // Get the name of the file to upload.
                //string fileName = FileUpload1.FileName;
                string fileName = "";
                switch (num_img)
                {
                    case 1:
                        fileName = FileUpload1.FileName;
                        break;
                    case 2:
                        fileName = FileUpload2.FileName;
                        break;
                    case 3:
                        fileName = FileUpload3.FileName;
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
                        img1.Src = "http://FACTS4-SRV/oftecnica/imagenes2/directorio/" + fileName;
                        break;
                    case 2:
                        FileUpload2.SaveAs(savePath);
                        img2.Src = "http://FACTS4-SRV/oftecnica/imagenes2/directorio/" + fileName;
                        break;
                    case 3:
                        FileUpload3.SaveAs(savePath);
                        img3.Src = "http://FACTS4-SRV/oftecnica/imagenes2/directorio/" + fileName;
                        break;
                    default: break;
                }
            }
            catch (Exception ex)
            {
            }

        }



        // llamada del botón buscar. Muestra un modal para introducir el número
        // de parte a buscar
        public void buscarParte(Object sender, EventArgs e)
        {
            try
            {
                Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();
                DataSet ds = conexion.devuelve_parte_maquina(tbBuscar.Value);
                cargar_parte(ds);
                desmarcar_boton();
            }
            catch (Exception ex)
            {

            }
        }

        private void buscarParteCarga()
        {
            try
            {
                Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();
                DataSet ds = conexion.devuelve_parte_maquina(tbBuscar.Value);
                cargar_parte(ds);
                desmarcar_boton();
            }
            catch (Exception ex)
            {

            }
        }

        public void siguiente(Object sender, EventArgs e)
        {
            desmarcar_boton();
            int num = Convert.ToInt16(btn1.Text);
            if (num < ds.Tables[0].Rows.Count)
            {
                num = num + 10;
                btn1.Text = Convert.ToString(num);

                num = Convert.ToInt16(btn2.Text);
                num = num + 10;
                btn2.Text = Convert.ToString(num);

                num = Convert.ToInt16(btn3.Text);
                num = num + 10;
                btn3.Text = Convert.ToString(num);

                num = Convert.ToInt16(btn4.Text);
                num = num + 10;
                btn4.Text = Convert.ToString(num);

                num = Convert.ToInt16(btn5.Text);
                num = num + 10;
                btn5.Text = Convert.ToString(num);

                num = Convert.ToInt16(btn6.Text);
                num = num + 10;
                btn6.Text = Convert.ToString(num);

                num = Convert.ToInt16(btn7.Text);
                num = num + 10;
                btn7.Text = Convert.ToString(num);

                num = Convert.ToInt16(btn8.Text);
                num = num + 10;
                btn8.Text = Convert.ToString(num);

                num = Convert.ToInt16(btn9.Text);
                num = num + 10;
                btn9.Text = Convert.ToString(num);

                num = Convert.ToInt16(btn10.Text);
                num = num + 10;
                btn10.Text = Convert.ToString(num);
            }
        }

        public void anterior(Object sender, EventArgs e)
        {
            desmarcar_boton();
            int num = Convert.ToInt16(btn1.Text);
            if (num > 1)
            {
                num = num - 10;
                btn1.Text = Convert.ToString(num);

                num = Convert.ToInt16(btn2.Text);
                num = num - 10;
                btn2.Text = Convert.ToString(num);

                num = Convert.ToInt16(btn3.Text);
                num = num - 10;
                btn3.Text = Convert.ToString(num);

                num = Convert.ToInt16(btn4.Text);
                num = num - 10;
                btn4.Text = Convert.ToString(num);

                num = Convert.ToInt16(btn5.Text);
                num = num - 10;
                btn5.Text = Convert.ToString(num);

                num = Convert.ToInt16(btn6.Text);
                num = num - 10;
                btn6.Text = Convert.ToString(num);

                num = Convert.ToInt16(btn7.Text);
                num = num - 10;
                btn7.Text = Convert.ToString(num);

                num = Convert.ToInt16(btn8.Text);
                num = num - 10;
                btn8.Text = Convert.ToString(num);

                num = Convert.ToInt16(btn9.Text);
                num = num - 10;
                btn9.Text = Convert.ToString(num);

                num = Convert.ToInt16(btn10.Text);
                num = num - 10;
                btn10.Text = Convert.ToString(num);
            }
        }


        public void Asignar_trabajador(Object sender, EventArgs e)
        {
            try
            {
                if (Asignado1.Text == "-")
                {
                    Asignado1.Text = lista_realizadoPorNEW.SelectedValue.ToString();
                }
                else if (Asignado1.Text != "-" && Asignado2.Text == "-")
                {
                    ColAsignado2.Visible = true;
                    Asignado2.Text = lista_realizadoPorNEW.SelectedValue.ToString();
                }
                else if (Asignado1.Text != "-" && Asignado2.Text != "" && Asignado3.Text == "-")
                {
                    ColAsignado3.Visible = true;
                    Asignado3.Text = lista_realizadoPorNEW.SelectedValue.ToString();
                }
                else
                { }

            }
            catch (Exception ex)
            {
            }
        }
        public void Agregar_trabajador(Object sender, EventArgs e)
        {
            try
            {
                if (ReparadoPor1.Text == "-")
                {
                    ReparadoPor1.Text = lista_realizadoPorNEW.SelectedValue.ToString();
                }
                else if (ReparadoPor1.Text != "-" && ReparadoPor2.Text == "-")
                {
                    ReparadoPor2.Visible = true;
                    horas2.Visible = true;
                    BorrarReparadoPor2.Visible = true;
                    ReparadoPor2.Text = lista_realizadoPorNEW.SelectedValue.ToString();
                }
                else if (ReparadoPor1.Text != "-" && ReparadoPor2.Text != "-" && ReparadoPor3.Text == "-")
                {
                    BorrarReparadoPor3.Visible = true;
                    horas3.Visible = true;
                    ReparadoPor3.Visible = true;
                    ReparadoPor3.Text = lista_realizadoPorNEW.SelectedValue.ToString();
                }
                else
                { }

            }
            catch (Exception ex)
            {
            }
        }

        public void Eliminar_trabajador(Object sender, EventArgs e)
        {
            try
            {
                HtmlAnchor anchor = (HtmlAnchor)sender;
                string name = anchor.ID;
                switch (name)
                {
                    case "BorrarReparadoPor1":
                        if (ReparadoPor3.Visible == true)
                        {

                            ReparadoPor1.Text = ReparadoPor2.Text;
                            horas1.Value = horas2.Value;

                            ReparadoPor2.Text = ReparadoPor3.Text;
                            horas2.Value = horas3.Value;

                            BorrarReparadoPor3.Visible = false;
                            horas3.Visible = false;
                            ReparadoPor3.Visible = false;

                            horas3.Value = "0";
                            ReparadoPor3.Text = "-";
                        }
                        else if (ReparadoPor2.Visible == true)
                        {
                            ReparadoPor1.Text = ReparadoPor2.Text;
                            horas1.Value = horas2.Value;
                            BorrarReparadoPor2.Visible = false;
                            horas2.Visible = false;
                            ReparadoPor2.Visible = false;

                            horas2.Value = "0";
                            ReparadoPor2.Text = "-";
                        }
                        else
                        {
                            horas1.Value = "0";
                            ReparadoPor1.Text = "-";
                        }
                        break;
                    case "BorrarReparadoPor2":
                        if (ReparadoPor3.Visible == true)
                        {

                            ReparadoPor2.Text = ReparadoPor3.Text;
                            horas2.Value = horas3.Value;
                            BorrarReparadoPor3.Visible = false;
                            horas3.Visible = false;
                            ReparadoPor3.Visible = false;
                            horas3.Value = "0";
                            ReparadoPor3.Text = "-";
                        }
                        else
                        {
                            horas2.Value = "0";
                            ReparadoPor2.Text = "-";
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
                        ReparadoPor3.Text = "-";
                        break;


                    case "BorrarAsignado1":
                        if (ColAsignado3.Visible == true)
                        {
                            Asignado1.Text = Asignado2.Text;
                            Asignado2.Text = Asignado3.Text;
                            ColAsignado3.Visible = false;
                            Asignado3.Text = "-";
                        }

                        else if (ColAsignado2.Visible == true)
                        {
                            Asignado1.Text = Asignado2.Text;
                            ColAsignado2.Visible = false;
                            Asignado2.Text = "-";
                        }
                        else
                        {
                            Asignado1.Text = "-";
                        }
                        break;

                    case "BorrarAsignado2":
                        if (ColAsignado3.Visible == true)
                        {
                            Asignado2.Text = Asignado3.Text;
                            ColAsignado3.Visible = false;
                            Asignado3.Text = "-";
                        }
                        else
                        {
                            Asignado2.Text = "-";
                            ColAsignado2.Visible = false;
                        }
                        break;

                    case "BorrarAsignado3":

                        ColAsignado3.Visible = false;
                        Asignado3.Text = "-";
                        break;
                }
            }
            catch (Exception ex)
            { }
        }

        protected void MandarMailV2(string TipoParte)
        {
            try
            {
                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                    {
                        string URL = "http://facts4-srv/oftecnica/ReparacionMaquinas.aspx?PARTE=" + lista_partes.Value.ToString();
                        //contenedor1.RenderControl(hw);
                        StringReader sr = new StringReader(sw.ToString());
                        //MailMessage mm = new MailMessage("bms@thermolympic.es", "pedro@thermolympic.es");
                        MailMessage mm = new MailMessage();
                        Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();
                        DataSet ds_correos = conexion.leer_correosMANTMAQ();
                        //DataSet ds_correos = conexion.leer_correosADMIN(); //cambiar a MANTMAQ

                        foreach (DataRow row in ds_correos.Tables[0].Rows)
                        {
                            mm.To.Add(new MailAddress(row["Correo"].ToString()));
                        }

                        //var inlineLogo = new LinkedResource(Server.MapPath(""+hyperlink2.ImageUrl.ToString()+""), "image/png");
                        string filename1 = Path.GetFileName(new Uri(img1.Src.ToString()).AbsolutePath);
                        string extension1 = Path.GetExtension(filename1).Substring(1);
                        var inlineLogo = new LinkedResource(Server.MapPath("~/Imagenes/" + filename1 + ""), "image/" + extension1 + "");
                        inlineLogo.ContentId = Guid.NewGuid().ToString();

                        string filename2 = Path.GetFileName(new Uri(img2.Src.ToString()).AbsolutePath);
                        string extension2 = Path.GetExtension(filename2).Substring(1);
                        var inlineLogo2 = new LinkedResource(Server.MapPath("~/Imagenes/" + filename2 + ""), "image/" + extension2 + "");
                        inlineLogo.ContentId = Guid.NewGuid().ToString();


                        //ORIGINAL
                        /*
                        var inlineLogo2 = new LinkedResource(Server.MapPath("~/Imagenes/GP12/21032_NOK2.png"), "image/png");
                        inlineLogo2.ContentId = Guid.NewGuid().ToString();*/


                        //mm.To.Add(new MailAddress("pedro@thermolympic.es"));
                        mm.From = new MailAddress("mantenimiento@thermolympic.es");
                        //mm.To.Add(new MailAddress("pedro@thermolympic.es"));
                        string subject = "";
                        string Body = "";
                        switch (TipoParte)
                        {
                            case "Nuevo":
                                subject = "Nuevo parte " + Convert.ToInt16(lista_partes.Value) + " de reparación de máquina.";
                                Body = "<strong>Máquina/periférico a reparar: </strong>" + lista_maquinas.SelectedValue.ToString() + " // " + lista_perifericos.SelectedValue.ToString() + "<br><strong>Avería denunciada: </strong>" + averia.Value.ToString() + "<br>Abierto por: " + encargado.SelectedValue.ToString() + "<br><a href='http://FACTS4-SRV/oftecnica/ReparacionMaquinas.aspx?PARTE=" + Convert.ToInt16(lista_partes.Value) + "'>Accede al parte a través de este link.</a>";
                                break;

                            case "Reparado":
                                string REPOP = "";
                                if (ReparadoPor3.Text != "-")
                                { REPOP = "por " + ReparadoPor1.Text + ", " + ReparadoPor2.Text + " y " + ReparadoPor3.Text; }
                                else if (ReparadoPor2.Text != "-")
                                { REPOP = "por " + ReparadoPor1.Text + " y " + ReparadoPor2.Text; }
                                else if (ReparadoPor1.Text != "-")
                                { REPOP = "por " + ReparadoPor1.Text; }
                                else { }
                                string REPOP2 = "";
                                if (encargado.SelectedValue.ToString() != "-")
                                { REPOP2 = "por " + encargado.SelectedValue.ToString(); }
                                subject = "Reparación realizada pendiente de validar " + REPOP2 + ". Parte " + Convert.ToInt16(lista_partes.Value) + " de máquina/periférico.";
                                Body = "<strong>Reparación realizada " + REPOP + " pendiente de validación " + REPOP2 + ".</strong><br>Parte " + Convert.ToInt16(lista_partes.Value) + ".<br><strong>Máquina/periférico:</strong> " + lista_maquinas.SelectedValue.ToString() + " // " + lista_perifericos.SelectedValue.ToString() + "<br><strong>Avería denunciada: </strong>" + averia.Value.ToString() + "<br><strong>Acción pendiente de validar: </strong>" + reparacion.Value.ToString() + " <br><a href='http://FACTS4-SRV/oftecnica/ReparacionMaquinas.aspx?PARTE=" + Convert.ToInt16(lista_partes.Value) + "'>Accede al parte a través de este link.</a>";
                                break;

                            case "ValidadoNC":
                                REPOP = "";
                                if (revisado_por.SelectedValue.ToString() != "-")
                                { REPOP = "por " + revisado_por.SelectedValue.ToString(); }
                                subject = "Revisión NOK. Parte " + Convert.ToInt16(lista_partes.Value) + " de reparación de máquina/periférico.";
                                Body = "<strong>Reparación validada como no conforme " + REPOP + ".</strong><br>Parte " + Convert.ToInt16(lista_partes.Value) + ".<br><strong>Máquina/periférico:</strong> " + lista_maquinas.SelectedValue.ToString() + " // " + lista_perifericos.SelectedValue.ToString() + "<br><strong>Avería denunciada: </strong>" + averia.Value.ToString() + "<br><strong>Reparación no conforme: </strong>" + reparacion.Value.ToString() + " <br><a href='http://FACTS4-SRV/oftecnica/ReparacionMaquinas.aspx?PARTE=" + Convert.ToInt16(lista_partes.Value) + "'>Accede al parte a través de este link.</a>";
                                break;

                            case "Validado":
                                REPOP = "";
                                if (revisado_por.SelectedValue.ToString() != "-")
                                { REPOP = "por " + revisado_por.SelectedValue.ToString(); }
                                subject = "Reparación completada, parte " + Convert.ToInt16(lista_partes.Value) + " de reparación de máquina/periférico.";
                                Body = "<strong>Reparación validada como correcta " + REPOP + ".</strong><br>Parte " + Convert.ToInt16(lista_partes.Value) + ".<br><strong>Máquina/periférico:</strong> " + lista_maquinas.SelectedValue.ToString() + " // " + lista_perifericos.SelectedValue.ToString() + "<br><strong>Avería denunciada: </strong>" + averia.Value.ToString() + "<br><strong>Acción validada: </strong>" + reparacion.Value.ToString() + "<br><a href='http://FACTS4-SRV/oftecnica/ReparacionMaquinas.aspx?PARTE=" + Convert.ToInt16(lista_partes.Value) + "'>Accede al parte a través de este link.</a>";
                                break;
                        }
                        mm.Subject = subject;
                        mm.Body = string.Format(Body, inlineLogo.ContentId);
                        //ORIGINAL
                        /*
                        mm.Subject = "NO CONFORMIDAD " + TipoAlerta.SelectedItem.ToString().ToLower() + " (NC-" + tbNoConformidad.Text + " / " + tbClienteCarga.Text.TrimEnd() + ")";
                        mm.Body = string.Format("<strong>Referencia:</strong> <br />&nbsp " + tbReferenciaCarga.Text + " " + tbDescripcionCarga.Text + "<br>" +
                                  "<strong>Defecto denunciado:</strong> <br />&nbsp " + tbProblemaNC.Text + " <br />" +
                                  "<strong>Cantidad en stock:</strong><br />&nbsp " + tbCantidadStock.Text + " piezas <br /> <br />" +
                                  "<strong>CONTRAMEDIDAS</strong><br />" +
                                  "<strong>Producción:</strong> <br />&nbsp " + tbContramedidaPROD.Text + "<br>" +
                                  "<strong>Calidad:</strong><br />&nbsp " + tbContramedidaCAL.Text + "<br>" +
                                  "<strong>Ingeniería:</strong> <br />&nbsp " + tbContramedidaING.Text + "<br>" +
                                  "<a href =" + URL + "  > Accede a la aplicación para ver el detalle.</a>", inlineLogo.ContentId);*/
                        //
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
            }
        }

        //protected override void OnPreRender(EventArgs e)
        //{
        //    lista_moldes.SelectedIndex = Convert.ToInt32(ViewState["ReparacionMoldes"]);
        //    base.OnPreRender(e);
        //}
    }

}