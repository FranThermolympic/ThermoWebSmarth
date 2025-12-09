using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

namespace ThermoWeb.MANTENIMIENTO
{
    public partial class MantenimientoIndex : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                cargar_pagina();
            }
            Comprobar_contadores_molde();
            Comprobar_contadores_maquina();

            Cargar_operaciones_pendientes();
           
            /*
            Conexion conexion = new Conexion();
            DataSet ds_listapendientes = conexion.devuelve_pendientes_maquina_PREVENTIVO();
            dgv_PreventivoMaquinas.DataSource = ds_listapendientes;
            dgv_PreventivoMaquinas.DataBind();
            */
        }

        // comprueba si ha llegado algún contador al límite y crea un parte automático
        
        private void Comprobar_contadores_molde()
        {
            try
            {
                Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();
                
                DataSet ds = conexion.Comprobar_contadores_moldesV2();
                string fecha_hoy = DateTime.Now.ToString("dd'/'MM'/'yyyy");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                     
                        if (conexion.leer_preventivoV2(row["C_RESOURCE_ID"].ToString(), Convert.ToInt32(row["C_MAINTCOUNTER_ID"].ToString()), row["C_TIMELASTMAINTENANCE"].ToString()) == 0)
                        {
                            conexion.insertar_parte(conexion.max_idParte() + 1, Convert.ToInt32(row["C_RESOURCE_ID"].ToString()), 4, 0, 1, 0, 1, "Realizar mantenimiento preventivo del molde. Tipo de mantenimiento: " + row["C_LONG_DESCR"].ToString() + ". (Parte creado automáticamente)", fecha_hoy.ToString(), 0,
                                                   "", "", "", "", "", 0, "", "", 1, 0.0, 0.0, "",2,"", 0, 0, 1, 0.0,0.0,0.0,"28 2 2","2 2 2",1,0.0,0.0,0.0, Convert.ToInt32(row["C_MAINTCOUNTER_ID"].ToString()), conexion.devuelve_ArrayPreguntaMolde(Convert.ToInt32(row["C_MAINTCOUNTER_ID"].ToString())), "0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0");
                            //inserta linea en mantenimiento preventivo para evitar que se creen más partes automáticos
                            conexion.insertar_preventivoV2(row["C_RESOURCE_ID"].ToString(), Convert.ToInt32(row["C_MAINTCOUNTER_ID"].ToString()), row["C_TIMELASTMAINTENANCE"].ToString());
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void Comprobar_contadores_maquina()
        {
            try
            {
                Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();
                DataSet ds = conexion.Comprobar_contadores_maquinasV2();
               // DateTime fecha_hoy = DateTime.Today;
                string fecha_hoy = DateTime.Now.ToString("dd'/'MM'/'yyyy");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {

                        if (conexion.leer_preventivoV2(row["C_RESOURCE_ID"].ToString(), Convert.ToInt32(row["C_MAINTCOUNTER_ID"].ToString()), row["C_TIMELASTMAINTENANCE"].ToString()) == 0)
                        {
                            conexion.insertar_parte_maquina(conexion.max_idParte_maquina() + 1, Convert.ToInt32(conexion.Devuelve_IDmaquinaXCHAR(row["C_RESOURCE_ID"].ToString())), 0, 4, 23, 0, 0, 0, 0, "Realizar mantenimiento preventivo de máquina. Tipo de mantenimiento: " + row["C_LONG_DESCR"].ToString() + ". (Parte creado automáticamente)", fecha_hoy.ToString(), "", 0, "", "", "", "", "", "", "", 2, 0.0, 0.0, "", 2, "", 0, 0, 0.0, 0.0, "2 2 2", "2 2 2", 0, 0.0, 0.0, 0.0, 0.0, Convert.ToInt32(row["C_MAINTCOUNTER_ID"].ToString()), conexion.devuelve_ArrayPregunta(Convert.ToInt32(row["C_MAINTCOUNTER_ID"].ToString())), "0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0");

                            //inserta linea en mantenimiento preventivo para evitar que se creen más partes automáticos
                            conexion.insertar_preventivoV2(row["C_RESOURCE_ID"].ToString(), Convert.ToInt32(row["C_MAINTCOUNTER_ID"].ToString()), row["C_TIMELASTMAINTENANCE"].ToString());
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        public void cargar_pagina()
        {
            try
            {
                Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();
                DataSet ds_moldes = conexion.Avisos_moldes();
                avisosMoldes.InnerText = ds_moldes.Tables[0].Rows[0]["num_avisos_moldes"].ToString();
                DataSet ds_maquinas = conexion.Avisos_maquinas();
                avisosMaquinas.InnerText = ds_maquinas.Tables[0].Rows[0]["num_avisos_maquinas"].ToString();                
            }
            catch (Exception ex)
            { 
                
            }
        }

        // Carga la tabla de últimas operaciones en mantenimiento de moldes.
        public void Cargar_operaciones_pendientes()
        {
            try
            {
                Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();
                DataSet PendMold = conexion.Devuelve_TOP7_pendientes_moldes(" AND R.Automatico <> 1 ");
                dgvMoldPend.DataSource = PendMold;
                dgvMoldPend.DataBind();

                DataSet PendMoldPrev = conexion.Devuelve_TOP7_pendientes_moldes(" AND R.Automatico = 1 ");
                dgvMoldPendPrev.DataSource = PendMoldPrev;
                dgvMoldPendPrev.DataBind();

                DataSet PendMaq = conexion.Devuelve_TOP7_pendientes_maquina(" AND IdTipoMantenimiento <> 4 ");
                dgvMaqPend.DataSource = PendMaq;
                dgvMaqPend.DataBind();

                DataSet PendMaqPrev = conexion.Devuelve_TOP7_pendientes_maquina(" AND IdTipoMantenimiento = 4 ");
                dgvMaqPendPrev.DataSource = PendMaqPrev;
                dgvMaqPendPrev.DataBind();

            }
            catch (Exception ex)
            {
            }
        }

        // Carga la tabla de últimas operaciones en manteimiento máquinas.
        
        public void ContactsGridView_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "RedirectMAQ")
            {
                Response.Redirect("EstadoReparacionesMaquina.aspx?PARTE=" + e.CommandArgument.ToString());
            }
            if (e.CommandName == "IrAppMAQ")
            {
                Response.Redirect("../MANTENIMIENTO/ReparacionMaquinas.aspx?PARTE=" + e.CommandArgument.ToString());
            }
            if (e.CommandName == "RedirectMOL")
            {
                Response.Redirect("EstadoReparacionesMoldes.aspx?PARTE=" + e.CommandArgument.ToString());
            }
            if (e.CommandName == "IrAppMOL")
            {
                Response.Redirect("http://facts4-srv/thermogestion/MANTENIMIENTO/ReparacionMoldes.aspx?PARTE=" + e.CommandArgument.ToString());
            }
            
        }


       

    }
}