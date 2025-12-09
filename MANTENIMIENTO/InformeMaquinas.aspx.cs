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

namespace ThermoWeb.MANTENIMIENTO
{
    public partial class InformesMaquinas : System.Web.UI.Page
    {

        private static DataSet ds_listadomaquinas = new DataSet();
        private static DataSet ds_listapreventivomaquinas = new DataSet();
        private static DataSet ds_listahistoricomaquinas = new DataSet();
        private static DataSet ds_listahistoricomaquinaspreventivo = new DataSet();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();

                DataTable ListaMaquinas = SHConexion.Devuelve_listado_MAQUINAS();
                for (int i = 0; i <= ListaMaquinas.Rows.Count - 1; i++)
                {
                    FiltroMaquina.InnerHtml = FiltroMaquina.InnerHtml + System.Environment.NewLine + String.Format("<option value='{0}'>", ListaMaquinas.Rows[i][1]);
                }

                if (Request.QueryString["MAQUINA"] != null)
                {
                    BuscarMaquinaInformeRedirect();
                }
                else {
                ds_listadomaquinas = conexion.Devuelve_lista_maquinas_numReparaciones("");
                ds_listapreventivomaquinas = conexion.Leer_contador_mantenimientomaquina("");
                ds_listahistoricomaquinas = conexion.Devuelve_historico_maquinas("");
                ds_listahistoricomaquinaspreventivo = conexion.Devuelve_historico_maquinas_preventivo("");
                Rellenar_grid();
                }
            }

        }

        private void Rellenar_grid()
        {
            try
            {
                dgv_Listadomaquinas.DataSource = ds_listadomaquinas;
                dgv_Listadomaquinas.DataBind();
                dgv_ListadoPreventivomaquinas.DataSource = ds_listapreventivomaquinas;
                dgv_ListadoPreventivomaquinas.DataBind();
                dgv_ListadoHistoricomaquinas.DataSource = ds_listahistoricomaquinas;
                dgv_ListadoHistoricomaquinas.DataBind();
                dgv_historico_preventivo.DataSource = ds_listahistoricomaquinaspreventivo;
                dgv_historico_preventivo.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        protected void GridViews_RowCommandReset(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Reset")
            {
                //REVISAR
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                int maquina = Convert.ToInt32(commandArgs[0]);
                int threshold = Convert.ToInt32(commandArgs[1]);
                //REINICIA EL CONTADOR Y SE GUARDA UN REGISTRO DE CONTROL
                Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();
                conexion.Reiniciar_contador_MoldeMaq(maquina, threshold);

                DataSet AuxiliaresMaquina = conexion.Comprobar_contadores_molde_Seleccionado(maquina, threshold);
                conexion.insertar_preventivoV2(maquina.ToString(), Convert.ToInt32(AuxiliaresMaquina.Tables[0].Rows[0]["C_MAINTCOUNTER_ID"].ToString()), AuxiliaresMaquina.Tables[0].Rows[0]["C_TIMELASTMAINTENANCE"].ToString());

                //CREA UN PARTE AUTOMÁTICO CERRADO
                conexion.insertar_parte_maquina(conexion.max_idParte_maquina() + 1, Convert.ToInt32(conexion.Devuelve_IDmaquinaXCHAR(maquina.ToString())), 0, 4, 23, 0, 0, 0, 0, "Realizar mantenimiento preventivo de máquina. Tipo de mantenimiento: " + AuxiliaresMaquina.Tables[0].Rows[0]["C_LONG_DESCR"].ToString() + ". (Parte creado automáticamente)", DateTime.Now.ToString("dd'/'MM'/'yyyy"), 
                                                "", 1, "", "", "", "", DateTime.Now.ToString("dd'/'MM'/'yyyy"), "Mantenimiento preventivo realizado.", "", 23, 1.0, 1.0, DateTime.Now.ToString("dd'/'MM'/'yyyy"), 23, "", 1, 0, 0.0, 21.50, "63 2 2", "63 2 2", 1, 1.0, 0.0, 0.0, 21.50, Convert.ToInt32(AuxiliaresMaquina.Tables[0].Rows[0]["C_MAINTCOUNTER_ID"].ToString()), conexion.devuelve_ArrayPregunta(Convert.ToInt32(AuxiliaresMaquina.Tables[0].Rows[0]["C_MAINTCOUNTER_ID"].ToString())), "1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1");
                BuscarMaquinainforme(null, null);
            }
        }

        private void BuscarMaquinaInformeRedirect()
        {
            try
            {
                
                    Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();
                    Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                    ds_listadomaquinas = conexion.Devuelve_lista_maquinas_numReparaciones(" AND IdMaquinaCHAR = '" + Request.QueryString["MAQUINA"] + "'"); //REVISAR ENTRADA
                    ds_listapreventivomaquinas = conexion.Leer_contador_mantenimientomaquina(" AND R.C_ID = '" + Request.QueryString["MAQUINA"] + "'"); //REVISAR ENTRADA
                    ds_listahistoricomaquinas = conexion.Devuelve_historico_maquinas(" AND C.IdMaquinaCHAR = '" + Request.QueryString["MAQUINA"] + "'"); //REVISAR ENTRADA
                    ds_listahistoricomaquinaspreventivo = conexion.Devuelve_historico_maquinas_preventivo(" AND C.IdMaquinaCHAR = '" + Request.QueryString["MAQUINA"] + "'");
                    Rellenar_grid();
               
            }
            catch (Exception ex)
            {

            }
        }
        public void BuscarMaquinainforme(Object sender, EventArgs e)
        {
            try
            {
                if (tbBuscarMaquina.Value == "")
                {
                    {
                        Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();
                        ds_listadomaquinas = conexion.Devuelve_lista_maquinas_numReparaciones("");
                        ds_listapreventivomaquinas = conexion.Leer_contador_mantenimientomaquina("");
                        ds_listahistoricomaquinas = conexion.Devuelve_historico_maquinas("");
                        ds_listahistoricomaquinaspreventivo = conexion.Devuelve_historico_maquinas_preventivo("");
                        Rellenar_grid();
                    }
                }
               else
                {
                    Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();
                    Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                    ds_listadomaquinas = conexion.Devuelve_lista_maquinas_numReparaciones(" AND IdMaquinaCHAR = '" + SHConexion.Devuelve_ID_MAQUINAS(Convert.ToString(tbBuscarMaquina.Value)) + "'"); //REVISAR ENTRADA
                    ds_listapreventivomaquinas = conexion.Leer_contador_mantenimientomaquina(" AND R.C_ID = '" + SHConexion.Devuelve_ID_MAQUINAS(Convert.ToString(tbBuscarMaquina.Value)) + "'"); //REVISAR ENTRADA
                    ds_listahistoricomaquinas = conexion.Devuelve_historico_maquinas(" AND C.IdMaquinaCHAR = '" + SHConexion.Devuelve_ID_MAQUINAS(Convert.ToString(tbBuscarMaquina.Value)) + "'"); //REVISAR ENTRADA
                    ds_listahistoricomaquinaspreventivo = conexion.Devuelve_historico_maquinas_preventivo(" AND C.IdMaquinaCHAR = '" + SHConexion.Devuelve_ID_MAQUINAS(Convert.ToString(tbBuscarMaquina.Value)) + "'");
                    Rellenar_grid();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void ContactsGridView_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName=="Redirect")
        {
            Response.Redirect("../MANTENIMIENTO/ReparacionMaquinas.aspx?PARTE=" + e.CommandArgument.ToString());
        }
        }
        
    }
}
