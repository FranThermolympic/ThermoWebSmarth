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
    public partial class InformesMoldes : System.Web.UI.Page
    {

        private static DataSet ds_listadomoldes = new DataSet();
        private static DataSet ds_listapreventivo = new DataSet();
        private static DataSet ds_listahistorico = new DataSet();
        private static int OrdenaMolde = 0;
        private static int OrdenaReparaciones = 0;
        private static int OrdenaPreventivo = 0;
        private static int OrdenaUbicacion = 0;
        private static int OrdenaMano = 0;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();
                Conexion_SMARTH SHconexion = new Conexion_SMARTH();

                //Datalist lista de moldes PDCA
                DataTable ListaMoldes = SHconexion.Devuelve_listado_MOLDES_V2();
                {
                    for (int i = 0; i <= ListaMoldes.Rows.Count - 1; i++)
                    {
                        FiltroMolde.InnerHtml = FiltroMolde.InnerHtml + System.Environment.NewLine +
                            String.Format("<option value='{0}'>", ListaMoldes.Rows[i][0]);
                    }
                }

                if (Request.QueryString["MOLDE"] != null)
                {
                    tbBuscarMolde.Value = Request.QueryString["MOLDE"];                    
                }              
                Rellenar_grid(null,null); 
            }

        }
        public void Rellenar_grid(object sender, EventArgs e)
        {
            try
            {
                Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();
                if (tbBuscarMolde.Value == "")
                {
                    {
                        ds_listadomoldes = conexion.Devuelve_lista_moldes_numReparaciones("");
                        ds_listapreventivo = conexion.Leer_contador_mantenimiento_Molde("");
                        ds_listahistorico = conexion.Devuelve_historico_Molde("");
                    }
                }
                else
                {
                    string[] DataListMOLDE = tbBuscarMolde.Value.Split(new char[] { '¬' });
                    string Molde = DataListMOLDE[0];
                    ds_listadomoldes = conexion.Devuelve_lista_moldes_numReparaciones(" AND ReferenciaMolde = '" + Molde + "'");
                    ds_listapreventivo = conexion.Leer_contador_mantenimiento_Molde(" AND R.C_ID = '" + Molde + "'");
                    ds_listahistorico = conexion.Devuelve_historico_Molde(" AND IDMoldes = '" + Molde + "'");
                }
                DataTable TipoMant = conexion.Devuelve_Molde_Tipos_Mantenimiento_Asignados();
                var JoinResult = (from p in ds_listadomoldes.Tables[0].AsEnumerable()
                                  join t in TipoMant.AsEnumerable()
                                  on p.Field<string>("MOLJOIN") equals t.Field<string>("MOLJOIN") into tempJoin
                                  from leftJoin in tempJoin.DefaultIfEmpty()
                                  select new
                                  {
                                      ReferenciaMolde = p.Field<Int32>("ReferenciaMolde"),
                                      Descripcion = p.Field<string>("Descripcion"),
                                      NumReparaciones = p.Field<Int32>("NumReparaciones"),
                                      Ubicacion = p.Field<string>("Ubicacion"),
                                      Zona = p.Field<string>("Zona"),
                                      MANO = p.Field<string>("MANO"),
                                      MANUBICACION = p.Field<string>("MANUBICACION"),
                                      TIPOMANT = leftJoin == null ? "Sin asignar" : leftJoin.Field<string>("C_LONG_DESCR"),
                                  });
                string columnaParaOrdenar = "";

                try 
                {
                    HtmlButton button = (HtmlButton)sender;
                    columnaParaOrdenar = button.ID;
                }
                catch (Exception ex)
                { }
                if (columnaParaOrdenar != "")
                {
                    switch (columnaParaOrdenar)
                    {
                        case "BTNOrdenaMolde":
                            if (OrdenaMolde == 0)
                            {
                                columnaParaOrdenar = "ReferenciaMolde";
                                OrdenaMolde = 1;
                                JoinResult = JoinResult.OrderBy(item => item.GetType().GetProperty(columnaParaOrdenar).GetValue(item, null)).ToList();
                            }
                            else
                            {
                                columnaParaOrdenar = "ReferenciaMolde";
                                OrdenaMolde = 0;
                                JoinResult = JoinResult.OrderByDescending(item => item.GetType().GetProperty(columnaParaOrdenar).GetValue(item, null)).ToList();
                            }
                            
                            break;
                        case "BTNOrdenaNumReparaciones":
                            if (OrdenaReparaciones == 0)
                            {
                                columnaParaOrdenar = "NumReparaciones";
                                OrdenaReparaciones = 1;
                                JoinResult = JoinResult.OrderBy(item => item.GetType().GetProperty(columnaParaOrdenar).GetValue(item, null)).ToList();
                            }
                            else
                            {
                                columnaParaOrdenar = "NumReparaciones";
                                OrdenaReparaciones = 0;
                                JoinResult = JoinResult.OrderByDescending(item => item.GetType().GetProperty(columnaParaOrdenar).GetValue(item, null)).ToList();
                            }
                            //CodAlmacen = "CLNT";
                            break;
                        case "BTNOrdenaPreventivo":
                            if (OrdenaPreventivo == 0)
                            {
                                columnaParaOrdenar = "TIPOMANT";
                                OrdenaPreventivo = 1;
                                JoinResult = JoinResult.OrderBy(item => item.GetType().GetProperty(columnaParaOrdenar).GetValue(item, null)).ToList();
                            }
                            else
                            {
                                columnaParaOrdenar = "TIPOMANT";
                                OrdenaPreventivo = 0;
                                JoinResult = JoinResult.OrderByDescending(item => item.GetType().GetProperty(columnaParaOrdenar).GetValue(item, null)).ToList();
                            }
                            
                            break;
                        case "BTNOrdenaUbicacion":
                            if (OrdenaUbicacion == 0)
                            {
                                columnaParaOrdenar = "Ubicacion";
                                OrdenaUbicacion = 1;
                                JoinResult = JoinResult.OrderBy(item => item.GetType().GetProperty(columnaParaOrdenar).GetValue(item, null)).ToList();
                            }
                            else
                            {
                                columnaParaOrdenar = "Ubicacion";
                                OrdenaUbicacion = 0;
                                JoinResult = JoinResult.OrderByDescending(item => item.GetType().GetProperty(columnaParaOrdenar).GetValue(item, null)).ToList();
                            }
                           
                            break;
                        case "BTNOrdenaMano":
                            if (OrdenaMano == 0)
                            {
                                columnaParaOrdenar = "MANO";
                                OrdenaMano = 1;
                                JoinResult = JoinResult.OrderBy(item => item.GetType().GetProperty(columnaParaOrdenar).GetValue(item, null)).ToList();
                            }
                            else
                            {
                                columnaParaOrdenar = "MANO";
                                OrdenaMano = 0;
                                JoinResult = JoinResult.OrderByDescending(item => item.GetType().GetProperty(columnaParaOrdenar).GetValue(item, null)).ToList();
                            }

                            break;
                        default: break;
                    }
                    /*
                    
                       
                        OrdenaUbicacion
                        OrdenaMano
                    */
                }
                
               
                

                        
        //Selecciona criterio de ordenacion con CASE
        /*
        string columnaParaOrdenar = "NumReparaciones"; // Puedes cambiar esto al nombre de la columna que desees

        if ()
            { }
        else
        
        */

        JoinResult.ToList();                
                    //JoinListadoMoldes = JoinListadoMoldes.Where(item => item.PREPARADOR == "" || item.PREPARADOR == null).ToList();
              




                dgv_ListadoMoldes.DataSource = JoinResult;
                dgv_ListadoMoldes.DataBind();
                dgv_ListadoPreventivo.DataSource = ds_listapreventivo;
                dgv_ListadoPreventivo.DataBind();
                dgv_ListadoHistorico.DataSource = ds_listahistorico;
                dgv_ListadoHistorico.DataBind();
            }
            catch (Exception ex)
            {

            }
        }
        protected void GridViews_RowCommandReset(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Reset")
            {
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                int molde = Convert.ToInt32(commandArgs[0]);
                int threshold = Convert.ToInt32(commandArgs[1]);
                //Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                Conexion_MANTENIMIENTO conexion = new Conexion_MANTENIMIENTO();
                //REINICIA EL CONTADOR Y SE GUARDA UN REGISTRO DE CONTROL
                conexion.Reiniciar_contador_MoldeMaq(molde, threshold);
                DataSet AuxiliaresMolde = conexion.Comprobar_contadores_molde_Seleccionado(molde, threshold);
                conexion.insertar_preventivoV2(molde.ToString(), Convert.ToInt32(AuxiliaresMolde.Tables[0].Rows[0]["C_MAINTCOUNTER_ID"].ToString()), AuxiliaresMolde.Tables[0].Rows[0]["C_TIMELASTMAINTENANCE"].ToString());
                //CREA UN PARTE AUTOMÁTICO CERRADO
                conexion.insertar_parte(conexion.max_idParte() + 1, Convert.ToInt32(molde), 4, 0, 1, 0, 1, "Realizar mantenimiento preventivo del molde. Tipo de mantenimiento: " + AuxiliaresMolde.Tables[0].Rows[0]["C_LONG_DESCR"].ToString() + ". (Parte creado automáticamente)", DateTime.Now.ToString("dd'/'MM'/'yyyy"), 1,
                                                   "", "", "", DateTime.Now.ToString("dd'/'MM'/'yyyy"), DateTime.Now.ToString("dd'/'MM'/'yyyy"),9,"Mantenimiento preventivo del molde","",1, 1.0, 1.0,"", 2, "",1,0, 1, 0.0, 21.5, 21.5, "28 2 2", "28 2 2", 0,1.0, 0.0, 0.0, Convert.ToInt32(AuxiliaresMolde.Tables[0].Rows[0]["C_MAINTCOUNTER_ID"].ToString()), conexion.devuelve_ArrayPreguntaMolde(Convert.ToInt32(AuxiliaresMolde.Tables[0].Rows[0]["C_MAINTCOUNTER_ID"].ToString())), "1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1");
                Rellenar_grid(null,null);
            }
        }

        public void ContactsGridView_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Redirect")
                {
                    Response.Redirect("http://facts4-srv/thermogestion/MANTENIMIENTO/ReparacionMoldes.aspx?PARTE=" + e.CommandArgument.ToString());
                }
                if (e.CommandName == "EditUbicacion")
                {

                    Conexion_SMARTH SHConexion = new Conexion_SMARTH();

                    DataTable Molde = SHConexion.Devuelve_Molde(e.CommandArgument.ToString());
                    UbicaMolde.InnerText = e.CommandArgument.ToString();
                    UbicaMoldeNombre.InnerText = Molde.Rows[0]["Descripcion"].ToString();
                    UbicacionMolde.Text = Molde.Rows[0]["Ubicacion"].ToString();
                    LblModificado.Text = "Modificado: " + Molde.Rows[0]["FechaModificaUbicacion"].ToString();
                    flexCheckDefault.Checked = Convert.ToBoolean(Molde.Rows[0]["Activo"]);
                    ImgUbicacion.Src = Molde.Rows[0]["ImagenUbi"].ToString();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "ShowPopup();", true);
                }
            }
            catch (Exception ex)
            { }
            

            
        }
        
    }
}