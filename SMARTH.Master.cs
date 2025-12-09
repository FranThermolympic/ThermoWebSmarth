using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ThermoWeb
{
    public partial class SMARTH_MASTER : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    if (Session["Nombre"] != null)
                    {
                        Mantener_sesion();
                    }
                    else
                    {
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        protected void Iniciar_sesion(object sender, EventArgs e)
        {
            Conexion_SMARTH SHConexion = new Conexion_SMARTH();
            PDCA.Conexion_PDCA PDCAConexion = new PDCA.Conexion_PDCA();

            DataTable UsuarioLog = SHConexion.Devuelve_log_in(inputUsuario.Value, inputPassword.Value);
            if (UsuarioLog.Rows.Count > 0)
            {
                Session["Nombre"] = UsuarioLog.Rows[0]["Nombre"].ToString();
                Session["Departamento"] = UsuarioLog.Rows[0]["Departamento"].ToString();
                Session["Token"] = UsuarioLog.Rows[0]["IdToken"].ToString();

                IdNOMBRE.InnerText = "Hola, " + UsuarioLog.Rows[0]["Nombre"].ToString();
                OFCLabelNOMBRE.InnerText = UsuarioLog.Rows[0]["Nombre"].ToString() + " (" + UsuarioLog.Rows[0]["Departamento"].ToString() + ")";
                USUARIO.Value = UsuarioLog.Rows[0]["Nombre"].ToString();
                // Logeado.Attributes.Add("class", "d-flex");
                SinLogear.Attributes.Clear();
                SinLogear.Attributes.Add("class", "d-flex-none justify-content-end");
                SinLogear.Attributes.Add("style", "display:none");
                Logeado.Attributes.Clear();
                Logeado.Attributes.Add("class", "d-flex justify-content-end");

                try
                {
                    //Botones de Acciones pendientes
                    BtnPlanAccion.Visible = false;
                    BtnNoConformidades.Visible = false;
                    BtnMaqRep.Visible = false;
                    BtnMaqVal.Visible = false;
                    BtnMoldRep.Visible = false;
                    BtnMoldVal.Visible = false;
                    
                    int trabajador = SHConexion.Devuelve_ID_Piloto_SMARTH(USUARIO.Value.ToString());

                    int NUMPendientesReparacionMolde = PDCAConexion.Devuelve_MiPDCA_Pendientes_Reparacion_Molde(trabajador).Rows.Count;
                    int NUMPendientesValidacionMolde = PDCAConexion.Devuelve_MiPDCA_Pendientes_Validacion_Molde(trabajador).Rows.Count;
                    int NUMPendientesReparacionMaquina = PDCAConexion.Devuelve_MiPDCA_Pendientes_Reparacion_Maquina(trabajador).Rows.Count;    
                    int NUMListaPendientesValidacionMaquina = PDCAConexion.Devuelve_MiPDCA_Pendientes_Validacion_Maquina(trabajador).Rows.Count;
                    int NUMListaPendientesNoConformidades = PDCAConexion.Devuelve_MiPDCA_Pendientes_No_ConformidadesEnCurso(trabajador).Rows.Count;
                    int NUMListaPendientesPDCA = PDCAConexion.Devuelve_MiPDCA_Pendientes_Acciones(trabajador).Rows.Count;



                    if (NUMListaPendientesPDCA > 0)
                    {
                        BtnPlanAccion.Visible = true;
                        BADGEACCION.InnerText = NUMListaPendientesPDCA.ToString();
                        
                    }
                    if (NUMListaPendientesNoConformidades > 0)
                    {
                        BtnNoConformidades.Visible = true;
                        BADGENC.InnerText = NUMListaPendientesNoConformidades.ToString();
                    }
                    if (NUMPendientesReparacionMolde > 0)
                    {
                        BtnMoldRep.Visible = true;
                        BADGEMOLDREP.InnerText = NUMPendientesReparacionMolde.ToString();
                    }
                    if (NUMPendientesValidacionMolde > 0)
                    {
                        BtnMoldVal.Visible = true;
                        BADGEMOLDVAL.InnerText = NUMPendientesValidacionMolde.ToString();
                    }
                    if (NUMPendientesReparacionMaquina > 0)
                    {
                        BtnMaqRep.Visible = true;
                        BADGEMAQREP.InnerText = NUMPendientesReparacionMaquina.ToString();

                    }
                    if (NUMListaPendientesValidacionMaquina > 0)
                    {
                        BtnMaqVal.Visible = true;
                        BADGEMAQVAL.InnerText = NUMListaPendientesValidacionMaquina.ToString();
                    }
                    
                    BADGERESUMEN.InnerText = (NUMListaPendientesPDCA + NUMListaPendientesNoConformidades + NUMPendientesReparacionMolde + NUMPendientesValidacionMolde + NUMPendientesReparacionMaquina + NUMListaPendientesValidacionMaquina).ToString();

                    //Botones de informes
                    BtnVerPrioridades.Visible = false;
                    BtnUltimasRevisiones.Visible = false;
                    BtnInformePDTEMol.Visible = false;
                    BtnInformePDTEMaq.Visible = false;
                    BtnListaFichaParametros.Visible = false;
                    BtnPDTEFichaParam.Visible = false;
                    BtnAlertasCalidad.Visible = false;
                    BtnConsultaPDCA.Visible = false;
                    BtnGP12Estado.Visible = false;
                    BtnPrevGP12.Visible = false;
                    BtnMoldesATaller.Visible = false;
                    BtnMaterialPDTESecar.Visible = false;
                    BtnRegistroCom.Visible = false;


                    if (Session["Departamento"].ToString() == "PRODUCCION")
                    {
                        BtnVerPrioridades.Visible = true;
                        BtnListaFichaParametros.Visible = true;
                        BtnPDTEFichaParam.Visible = true;
                        BtnConsultaPDCA.Visible = true;
                    }

                    else if (Session["Departamento"].ToString() == "CALIDAD")
                    {
                        BtnVerPrioridades.Visible = true;
                        BtnUltimasRevisiones.Visible = true;
                        BtnAlertasCalidad.Visible = true;
                        BtnConsultaPDCA.Visible = true;
                        BtnGP12Estado.Visible = true;
                        BtnPrevGP12.Visible = true;
                        BtnRegistroCom.Visible = true;
                    }
                    else if (Session["Departamento"].ToString() == "MANTENIMIENTO")
                    {
                        BtnVerPrioridades.Visible = true;
                        BtnInformePDTEMol.Visible = true;
                        BtnInformePDTEMaq.Visible = true;
                        BtnConsultaPDCA.Visible = true;
                        BtnMoldesATaller.Visible = true;
                    }
                    else if (Session["Departamento"].ToString() == "CAMBIADOR")
                    {
                        BtnVerPrioridades.Visible = true;
                        BtnInformePDTEMol.Visible = true;
                        BtnListaFichaParametros.Visible = true;
                        BtnMoldesATaller.Visible = true;
                    }
                    else if (Session["Nombre"].ToString() == "Ruben Mateo")
                    {
                        BtnNuevaFichaParam.Visible = false;
                        BtnNuevaParteMaq.Visible = false;
                        BtnNuevaParteMol.Visible = false;
                        BtnNuevaNoConformidad.Visible = false;
                        BtnNuevaGP12.Visible = false;
                        BtnNuevaLiberacion.Visible = false;

                        BtnRegistroCom.Visible = true; //registro de comunicaciones
                        BtnEntregaRecurso.Visible = true; //lista epis entregados
                        BtnResultadoOPERARIO.Visible = true; //gp12 historico operario
                        BtnDeteccionesOPERARIO.Visible = true; //gp12 historico de operario mes
                        BtnKPIAprovechados.Visible = true;//operarios aprovechamiento
                        BtnConsultaNivelI.Visible = true; //operarios trabajando nivel I

                    }
                    else
                    {
                        BtnVerPrioridades.Visible = true;
                        BtnUltimasRevisiones.Visible = true;
                        BtnInformePDTEMol.Visible = true;
                        BtnInformePDTEMaq.Visible = true;
                        BtnListaFichaParametros.Visible = true;
                        BtnPDTEFichaParam.Visible = true;
                        BtnAlertasCalidad.Visible = true;
                        BtnConsultaPDCA.Visible = true;
                        BtnGP12Estado.Visible = true;
                        BtnPrevGP12.Visible = true;
                        BtnMoldesATaller.Visible = true;
                        BtnMaterialPDTESecar.Visible = true;
                    }

                }
                catch (Exception ex)
                { }

                //Session.
            }
            Response.Redirect(Request.RawUrl);


            //Response.Cookies["Usuario"].Expires = DateTime.Now.AddDays(15);
            //Response.Cookies["Pass"].Expires = DateTime.Now.AddDays(15);

            //Response.Cookies["Usuario"].Value = txtUserName.Text.Trim;
            //Response.Cookies["Pass"].Value = txtPassword.Text.Trim;


        }

        protected void Mantener_sesion()
        {
            Conexion_SMARTH SHConexion = new Conexion_SMARTH();
            PDCA.Conexion_PDCA PDCAConexion = new PDCA.Conexion_PDCA();

                IdNOMBRE.InnerText = "Hola, " + Session["Nombre"].ToString();
                OFCLabelNOMBRE.InnerText = Session["Nombre"].ToString() + " (" + Session["Departamento"].ToString() + ")";
                USUARIO.Value = Session["Nombre"].ToString();
                // Logeado.Attributes.Add("class", "d-flex");
                SinLogear.Attributes.Clear();
                SinLogear.Attributes.Add("class", "d-flex-none justify-content-end");
                SinLogear.Attributes.Add("style", "display:none");
                Logeado.Attributes.Clear();
                Logeado.Attributes.Add("class", "d-flex justify-content-end");
                try
                {
                BtnPlanAccion.Visible = false;
                BtnNoConformidades.Visible = false;
                BtnMaqRep.Visible = false;
                BtnMaqVal.Visible = false;
                BtnMoldRep.Visible = false;
                BtnMoldVal.Visible = false;
                

                int trabajador = SHConexion.Devuelve_ID_Piloto_SMARTH(USUARIO.Value.ToString());

                    int NUMPendientesReparacionMolde = PDCAConexion.Devuelve_MiPDCA_Pendientes_Reparacion_Molde(trabajador).Rows.Count;
                    int NUMPendientesValidacionMolde = PDCAConexion.Devuelve_MiPDCA_Pendientes_Validacion_Molde(trabajador).Rows.Count;
                    int NUMPendientesReparacionMaquina = PDCAConexion.Devuelve_MiPDCA_Pendientes_Reparacion_Maquina(trabajador).Rows.Count;
                    int NUMListaPendientesValidacionMaquina = PDCAConexion.Devuelve_MiPDCA_Pendientes_Validacion_Maquina(trabajador).Rows.Count;
                    int NUMListaPendientesNoConformidades = PDCAConexion.Devuelve_MiPDCA_Pendientes_No_ConformidadesEnCurso(trabajador).Rows.Count;
                    int NUMListaPendientesPDCA = PDCAConexion.Devuelve_MiPDCA_Pendientes_Acciones(trabajador).Rows.Count;

                    if (NUMListaPendientesPDCA > 0)
                    {
                        BtnPlanAccion.Visible = true;
                        BADGEACCION.InnerText = NUMListaPendientesPDCA.ToString();
     

                    }
                    if (NUMListaPendientesNoConformidades > 0)
                    {
                        BtnNoConformidades.Visible = true;
                        BADGENC.InnerText = NUMListaPendientesNoConformidades.ToString();
                    
                }
                    if (NUMPendientesReparacionMolde > 0)
                    {
                        BtnMoldRep.Visible = true;
                        BADGEMOLDREP.InnerText = NUMPendientesReparacionMolde.ToString();
                    
                }
                    if (NUMPendientesValidacionMolde > 0)
                    {
                        BtnMoldVal.Visible = true;
                        BADGEMOLDVAL.InnerText = NUMPendientesValidacionMolde.ToString();
                    
                }
                    if (NUMPendientesReparacionMaquina > 0)
                    {
                        BtnMaqRep.Visible = true;
                        BADGEMAQREP.InnerText = NUMPendientesReparacionMaquina.ToString();
                 
                }
                    if (NUMListaPendientesValidacionMaquina > 0)
                    {
                        BtnMaqVal.Visible = true;
                        BADGEMAQVAL.InnerText = NUMListaPendientesValidacionMaquina.ToString();
                    
                }
                BADGERESUMEN.InnerText = (NUMListaPendientesPDCA + NUMListaPendientesNoConformidades + NUMPendientesReparacionMolde + NUMPendientesValidacionMolde + NUMPendientesReparacionMaquina + NUMListaPendientesValidacionMaquina).ToString();
                //Botones de informes
                BtnVerPrioridades.Visible = false;
                BtnUltimasRevisiones.Visible = false;
                BtnInformePDTEMol.Visible = false;
                BtnInformePDTEMaq.Visible = false;
                BtnListaFichaParametros.Visible = false;
                BtnPDTEFichaParam.Visible = false;
                BtnAlertasCalidad.Visible = false;
                BtnConsultaPDCA.Visible = false;
                BtnGP12Estado.Visible = false;
                BtnPrevGP12.Visible = false;
                BtnMoldesATaller.Visible = false;
                BtnMaterialPDTESecar.Visible = false;
                BtnRegistroCom.Visible = false;


                if (Session["Departamento"].ToString() == "PRODUCCION")
                {
                    BtnVerPrioridades.Visible = true;
                    BtnListaFichaParametros.Visible = true;
                    BtnPDTEFichaParam.Visible = true;
                    BtnConsultaPDCA.Visible = true;
                }

                else if (Session["Departamento"].ToString() == "CALIDAD")
                {
                    BtnVerPrioridades.Visible = true;
                    BtnUltimasRevisiones.Visible = true;
                    BtnAlertasCalidad.Visible = true;
                    BtnConsultaPDCA.Visible = true;
                    BtnGP12Estado.Visible = true;
                    BtnPrevGP12.Visible = true;
                    BtnRegistroCom.Visible = true;
                }
                else if (Session["Departamento"].ToString() == "MANTENIMIENTO")
                {
                    BtnVerPrioridades.Visible = true;
                    BtnInformePDTEMol.Visible = true;
                    BtnInformePDTEMaq.Visible = true;
                    BtnConsultaPDCA.Visible = true;
                    BtnMoldesATaller.Visible = true;
                }
                else if (Session["Departamento"].ToString() == "CAMBIADOR")
                {
                    BtnVerPrioridades.Visible = true;
                    BtnInformePDTEMol.Visible = true;
                    BtnListaFichaParametros.Visible = true;
                    BtnMoldesATaller.Visible = true;
                }
                else if (Session["Nombre"].ToString() == "Ruben Mateo")
                {
                    BtnNuevaFichaParam.Visible = false;
                    BtnNuevaParteMaq.Visible = false;
                    BtnNuevaParteMol.Visible = false;
                    BtnNuevaNoConformidad.Visible = false;
                    BtnNuevaGP12.Visible = false;
                    BtnNuevaLiberacion.Visible = false;

                    BtnRegistroCom.Visible = true; //registro de comunicaciones
                    BtnEntregaRecurso.Visible = true; //lista epis entregados
                    BtnResultadoOPERARIO.Visible = true; //gp12 historico operario
                    BtnDeteccionesOPERARIO.Visible = true; //gp12 historico de operario mes
                    BtnKPIAprovechados.Visible = true;//operarios aprovechamiento
                    BtnConsultaNivelI.Visible = true; //operarios trabajando nivel I

                }
                else
                {
                    BtnVerPrioridades.Visible = true;
                    BtnUltimasRevisiones.Visible = true;
                    BtnInformePDTEMol.Visible = true;
                    BtnInformePDTEMaq.Visible = true;
                    BtnListaFichaParametros.Visible = true;
                    BtnPDTEFichaParam.Visible = true;
                    BtnAlertasCalidad.Visible = true;
                    BtnConsultaPDCA.Visible = true;
                    BtnGP12Estado.Visible = true;
                    BtnPrevGP12.Visible = true;
                    BtnMoldesATaller.Visible = true;
                    BtnMaterialPDTESecar.Visible = true;
                }
            }

            catch (Exception ex)
                { }

                //Session.
           



            //Response.Cookies["Usuario"].Expires = DateTime.Now.AddDays(15);
            //Response.Cookies["Pass"].Expires = DateTime.Now.AddDays(15);

            //Response.Cookies["Usuario"].Value = txtUserName.Text.Trim;
            //Response.Cookies["Pass"].Value = txtPassword.Text.Trim;


        }

        protected void Cerrar_sesion(object sender, EventArgs e)
        {
            Session.Clear();
            Logeado.Attributes.Clear();
            Logeado.Attributes.Add("class", "d-flex-none justify-content-end");
            Logeado.Attributes.Add("style", "display:none");
            inputUsuario.Value = null;
            SinLogear.Attributes.Clear();
            SinLogear.Attributes.Add("class", "d-flex justify-content-end");
            Response.Redirect(Request.RawUrl);

        }

        protected void Redireccionar(object sender, EventArgs e)
        {
            string link = Page.ResolveUrl("~/index.aspx");
            HtmlButton button = (HtmlButton)sender;
            string Boton = button.ID;
            switch (Boton)
            {

                case "BtnResumen":
                    link = Page.ResolveUrl("~/PDCA/MiPDCA.aspx");
                    break;
                case "BtnPlanAccion":
                    link = Page.ResolveUrl("~/PDCA/MiPDCA.aspx?ACCESO=PLANACCION");
                    break;
                case "BtnNoConformidades":
                        link = Page.ResolveUrl("~/PDCA/MiPDCA.aspx?ACCESO=NOCONFORMIDAD");
                    break;                
                case "BtnMoldRep":
                        link = Page.ResolveUrl("~/PDCA/MiPDCA.aspx?ACCESO=REPARACIONMOLDES");
                    break;                
                case "BtnMoldVal":
                        link = Page.ResolveUrl("~/PDCA/MiPDCA.aspx?ACCESO=VALIDACIONMOLDES");
                    break;                
                case "BtnMaqRep":
                        link = Page.ResolveUrl("~/PDCA/MiPDCA.aspx?ACCESO=REPARACIONMAQUINAS");
                    break;                
                case "BtnMaqVal":
                        link = Page.ResolveUrl("~/PDCA/MiPDCA.aspx?ACCESO=VALIDACIONMAQUINAS");
                    break;
                case "BtnNuevaFichaParam":
                    link = Page.ResolveUrl("~/FichasParametros_nuevo.aspx");
                    break;
                case "BtnNuevaParteMaq":
                    link = Page.ResolveUrl("~/MANTENIMIENTO/ReparacionMaquinas.aspx");
                    break;
                case "BtnNuevaParteMol":
                    link = Page.ResolveUrl("~/MANTENIMIENTO/ReparacionMoldes.aspx");                
                    break;
                case "BtnNuevaNoConformidad":
                    link = Page.ResolveUrl("~/CALIDAD/Alertas_Calidad.aspx");
                    break;
                case "BtnNuevaGP12":
                    link = Page.ResolveUrl("~/GP12/GP12.aspx");
                    break;
                case "BtnNuevaLiberacion":
                    link = Page.ResolveUrl("~/LIBERACIONES/EstadoLiberacion.aspx");
                    break;
                case "BtnAreaRechazo":
                    link = Page.ResolveUrl("~/AREA_RECHAZO/Area_Rechazo.aspx");
                    break;
                case "BtnVerPrioridades":
                    link = Page.ResolveUrl("~/PLANIFICACION/ListadoPrioridades.aspx");
                    break;
                case "BtnUltimasRevisiones":
                    link = Page.ResolveUrl("~/GP12/GP12Historico.aspx");
                    break;
                case "BtnInformePDTEMol":
                    link = Page.ResolveUrl("~/MANTENIMIENTO/EstadoReparacionesMoldes.aspx"); 
                    break;
                case "BtnInformePDTEMaq":
                    link = Page.ResolveUrl("~/MANTENIMIENTO/EstadoReparacionesMaquina.aspx");
                    break;
                case "BtnListaFichaParametros":
                    link = Page.ResolveUrl("~/ListaFichasParametros.aspx");
                    break;
                case "BtnPDTEFichaParam":
                    link = Page.ResolveUrl("~/KPIFichasParametros.aspx");
                    break;
                case "BtnAlertasCalidad":
                    link = Page.ResolveUrl("~/CALIDAD/ListaAlertasCalidad.aspx");
                    break;
                case "BtnConsultaPDCA":
                    link = Page.ResolveUrl("~/PDCA/PDCA.aspx");
                    break;
                case "BtnConsultaPDCAGOP":
                    link = Page.ResolveUrl("~/PDCA/PDCAGOP.aspx");
                    break;
                case "BtnGP12Estado":
                    link = Page.ResolveUrl("~/GP12/GP12ReferenciasEstado.aspx");
                    break;
                case "BtnRegistroCom":
                    link = Page.ResolveUrl("~/GP12/GP12RegistroComunicaciones.aspx");
                    break;
                case "BtnPrevGP12":
                    link = Page.ResolveUrl("~/GP12/PrevisionGP12.aspx");
                    break;
                case "BtnMoldesATaller":
                    link = "http://facts4-srv/oftecnica/UbicarMoldes.aspx";
                    break;        
                case "BtnMaterialPDTESecar":
                    link = Page.ResolveUrl("~/MATERIALES/PrevisionSecado.aspx");
                    break;
                case "BtnEntregaRecurso":
                    link = Page.ResolveUrl("~/RRHH/Lista_Operario_Recursos_Pendientes_Firmar.aspx");
                    break;
                case "BtnResultadoOPERARIO":
                    link = Page.ResolveUrl("~/GP12/GP12HistoricoOperario.aspx");
                    break;
                case "BtnDeteccionesOPERARIO":
                    link = Page.ResolveUrl("~/GP12/GP12KPICAL.aspx");
                    break;
                case "BtnKPIAprovechados":
                    link = Page.ResolveUrl("~/KPI/KPIAprovechados.aspx");
                    break;
                case "BtnConsultaNivelI":
                    link = Page.ResolveUrl("~/LIBERACIONES/ConsultaNivelOperario.aspx");
                    break;


            }
            Response.Redirect(link);

        }
    }
}
