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

namespace ThermoWeb.AREA_RECHAZO
{
    public partial class Area_Rechazo : System.Web.UI.Page
    {

        private static DataTable DT_area = new DataTable();
        private static DataTable DT_ListaProducto = new DataTable();
        private static DataSet Personal = new DataSet();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Conexion_SMARTH SHconexion = new Conexion_SMARTH();
                DT_area = SHconexion.Devuelve_Area_Rechazo(" AND FechaSalida IS NULL");
                DT_ListaProducto = SHconexion.Devuelve_Productos_NAV("");
                Auxiliares_Carga();
                Rellenar_grid();
            }
        }

        private void Auxiliares_Carga()
        {
            Conexion_SMARTH SHconexion = new Conexion_SMARTH();
            Personal = SHconexion.Devuelve_mandos_intermedios_SMARTH();
            foreach (DataRow row in Personal.Tables[0].Rows)
            {
                DropNuevoResponsableEntrada.Items.Add(row["Nombre"].ToString());
                DropNuevoResponsableSalida.Items.Add(row["Nombre"].ToString());
            }

            DataTable DT_ListaProductoSeparador = SHconexion.Devuelve_Productos_NAV_SEPARADOR();
            {
                for (int i = 0; i <= DT_ListaProductoSeparador.Rows.Count - 1; i++)
                {
                    DatalistProductos.InnerHtml = DatalistProductos.InnerHtml + System.Environment.NewLine +
                        String.Format("<option value='{0}'>", DT_ListaProductoSeparador.Rows[i][0]);
                }
            }


        }

        private void Rellenar_grid()
        {
            try
            {
                var JoinResult = (from p in DT_area.AsEnumerable()
                                  join t in DT_ListaProducto.AsEnumerable()
                                  on p.Field<string>("Referencia") equals t.Field<string>("PRODUCTO") into tempJoin
                                  from leftJoin in tempJoin.DefaultIfEmpty()
                                  select new
                                  {
                                      Id = p.Field<Int32>("Id"),
                                      Referencia = p.Field<string>("Referencia"),
                                      Descripcion = leftJoin == null ? "-" : leftJoin.Field<string>("DESCRIPCION"),
                                      MOTIVO = p.Field<string>("Motivo"),
                                      RESPONSABLEENTRADA = p.Field<string>("ResponsableEntrada"),
                                      CANTIDAD = p.Field<Int32>("Cantidad"),
                                      FECHAENTRADA = p.Field<DateTime>("FechaEntrada"),
                                      FECHASALIDA = p.Field<DateTime>("FechaSalida"),
                                      DEBESALIR = p.Field<DateTime>("DebeSalir"),
                                      DECISION = p.Field<string>("Decision"),
                                      RESPONSABLESALIDA = p.Field<string>("ResponsableSalida"),
                                      OBSERVACIONES = p.Field<string>("OBSERVACIONES"),
                                  }).ToList();
                dgv_AreaRechazo.DataSource = JoinResult;
                dgv_AreaRechazo.DataBind(); 
            }
            catch (Exception ex)
            { 
                
            }
        }

        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if ((e.Row.RowState == DataControlRowState.Normal) || (e.Row.RowState == DataControlRowState.Alternate))
                    {
                        Label FechaSalida = (Label)e.Row.FindControl("lblFechaSalida");
                        

                        if (FechaSalida.Text == "01/01/1900")
                        {
                           
                            FechaSalida.Visible = false;
                        }
                        else
                        {
                            
                            FechaSalida.Visible = true;
                        }

                    }
                    if (e.Row.RowState.HasFlag(DataControlRowState.Edit))
                    {
                        LinkButton BTNSALIDA = (LinkButton)e.Row.FindControl("btnSalida");

                        DropDownList ResponsableEntrada = (DropDownList)e.Row.FindControl("txtResponsableEntrada");
                        DropDownList ResponsableSalida = (DropDownList)e.Row.FindControl("txtResponsableSalida");
                        Label FechaSalida = (Label)e.Row.FindControl("AUXFechaSalida");
                        LinkButton BtnRecuperar = (LinkButton)e.Row.FindControl("btnRecuperar");

                        if (FechaSalida.Text == "01/01/1900" || FechaSalida.Text == "")
                        {
                            BTNSALIDA.Visible = true;
                            BtnRecuperar.Visible = false;
                        }
                        else
                        {
                            BTNSALIDA.Visible = false;
                            BtnRecuperar.Visible = true;
                        }

                        ResponsableEntrada.DataSource = Personal;
                        ResponsableEntrada.DataTextField = "Nombre";
                        ResponsableEntrada.DataValueField = "Nombre";
                        ResponsableEntrada.DataBind();

                        ResponsableSalida.DataSource = Personal;
                        ResponsableSalida.DataTextField = "Nombre";
                        ResponsableSalida.DataValueField = "Nombre";
                        ResponsableSalida.DataBind();

                        //DataRowView dr = e.Row.DataItem as DataRowView;
                        ResponsableEntrada.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ResponsableEntrada"));
                        ResponsableSalida.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ResponsableSalida"));             

                    }
                }

            }
            catch (Exception ex)
            {
            }
        }

        public void Agregar_Producto(object sender, EventArgs e)
        {
            Conexion_SMARTH SHconexion = new Conexion_SMARTH();

            if (DropNuevoResponsableEntrada.SelectedValue != "-" && DropNuevoResponsableSalida.SelectedValue != "-")
            {
                int Cantidad = 0;
                try
                {
                    Cantidad = Math.Abs((Convert.ToInt32(InputNuevoCantidad.Value)));
                }
                catch (Exception ex)
                { 
                }
                string FechaSalida = DateTime.Now.AddDays(21).ToString("dd/MM/yyyy");
                try
                {
                    if (InputNuevaFechaSalida.Value != "")
                    {
                        FechaSalida = Convert.ToDateTime(InputNuevaFechaSalida.Value).ToString("dd/MM/yyyy");

                    }
                }
                catch (Exception ex)
                {
                }

                string[] RecorteReferencia = InputNuevoProducto.Value.Split(new char[] { '¬' });

                SHconexion.Insertar_area_rechazo(RecorteReferencia[0].ToString(), InputNuevoMotivo.Value, SHconexion.Devuelve_ID_Piloto_SMARTH(DropNuevoResponsableEntrada.SelectedValue), Cantidad, DateTime.Now.ToString("dd/MM/yyyy"), FechaSalida, SHconexion.Devuelve_ID_Piloto_SMARTH(DropNuevoResponsableSalida.SelectedValue), InputNuevoObservaciones.Value);

                if (CheckHistorico.Checked)
                {
                    DT_area = SHconexion.Devuelve_Area_Rechazo(" AND FechaSalida IS NULL");
                }
                else
                {
                    DT_area = SHconexion.Devuelve_Area_Rechazo(" ");
                }
                Rellenar_grid();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "LogErrorAsignacion()", true);
            }
        }

        public void GridView_RowUpdating(Object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Conexion_SMARTH SHconexion = new Conexion_SMARTH();                
                Label id = (Label)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("lblID");
                TextBox referencia = (TextBox)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("txtReferencia");
                TextBox cantidad = (TextBox)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("txtCantidad");
                DropDownList ResponsableEntrada = (DropDownList)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("txtResponsableEntrada");
                TextBox fechaEntrada = (TextBox)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("txtFechaEntrada");
                TextBox motivo = (TextBox)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("txtMotivo");
                DropDownList ResponsableSalida = (DropDownList)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("txtResponsableSalida");
                TextBox debeSalir = (TextBox)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("txtDebeSalir");
                TextBox decision = (TextBox)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("txtDecision");
                TextBox observaciones = (TextBox)dgv_AreaRechazo.Rows[e.RowIndex].FindControl("txtObservaciones");

                string LOGERRORES = "";

                //COMPRUEBO FECHA ENTRADA
                
                    if (fechaEntrada.Text == "")
                    {
                        LOGERRORES = LOGERRORES + "Falta fecha de entrada.";
                    }
                    else
                    {
                    try
                    {
                        Convert.ToDateTime(fechaEntrada.Text);
                    }
                    catch (Exception ex)
                    {
                        LOGERRORES = LOGERRORES + "La fecha entrada no es válida.";
                    }
                }
                
                //COMPRUEBO DEBE SALIR
       
                    if (debeSalir.Text == "")
                        {
                            LOGERRORES = LOGERRORES + "Falta definir una fecha de salida.";
                        }
                    else
                        {
                            try
                            {
                                Convert.ToDateTime(debeSalir.Text);
                            }
                            catch(Exception ex)
                            {
                                LOGERRORES = LOGERRORES + "La fecha prevista de salida no es válida.";
                            }
                        }
               
                //COMPRUEBO RESPONSABLE ENTRADA
                if (ResponsableEntrada.SelectedValue == "-")
                {
                    LOGERRORES = LOGERRORES + "Falta responsable entrada.";
                }
               
                //COMPRUEBO RESPONSABLE SALIDA
                if (ResponsableSalida.SelectedValue == "-")
                {
                    LOGERRORES = LOGERRORES + "Falta responsable salida.";
                }
                            
                if (LOGERRORES == "")
                {

                    SHconexion.Actualizar_Area_Rechazo(Convert.ToInt32(id.Text), referencia.Text, motivo.Text, SHconexion.Devuelve_ID_Piloto_SMARTH(ResponsableEntrada.SelectedValue),
                        Convert.ToInt32(cantidad.Text), fechaEntrada.Text, debeSalir.Text, decision.Text, SHconexion.Devuelve_ID_Piloto_SMARTH(ResponsableSalida.SelectedValue), observaciones.Text);

                    dgv_AreaRechazo.EditIndex = -1;
                    if (CheckHistorico.Checked)
                    {
                        DT_area = SHconexion.Devuelve_Area_Rechazo(" AND FechaSalida IS NULL");
                    }
                    else
                    {
                        DT_area = SHconexion.Devuelve_Area_Rechazo(" ");
                    }
                    Rellenar_grid();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "LogErrores()", true);

                }
            }
            catch (Exception ex)
            {
                string LOGERRORES = "Error inesperado";
              //  Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "LogErrores(" + LOGERRORES.ToString() + ");", true);
            }              
        }

        protected void GridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            dgv_AreaRechazo.EditIndex = -1;
            Rellenar_grid();
        }

        public void GridView_RowEditing(Object sender, GridViewEditEventArgs e)
        {
            dgv_AreaRechazo.EditIndex = e.NewEditIndex;
            Rellenar_grid();
        }

        // Acciones de comandos
        public void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Conexion_SMARTH SHconexion = new Conexion_SMARTH();
            if (e.CommandName.Equals("Retirar"))
            {
                try
                {
                    int Id = Convert.ToInt16(e.CommandArgument);
                    

                    //MODIFICAR
                    SHconexion.Liberar_Producto_Area_Rechazo(Id, 1);
                    if (CheckHistorico.Checked)
                    {
                        DT_area = SHconexion.Devuelve_Area_Rechazo(" AND FechaSalida IS NULL");
                    }
                    else
                    {
                        DT_area = SHconexion.Devuelve_Area_Rechazo(" ");
                    }
                    
                    Rellenar_grid();

                }
                catch (Exception)
                {

                }
            }
            if (e.CommandName.Equals("Recuperar"))
            {
                try
                {
                    int Id = Convert.ToInt16(e.CommandArgument);
                   
                    //MODIFICAR
                    SHconexion.Liberar_Producto_Area_Rechazo(Id, 0);
                    if (CheckHistorico.Checked)
                    {
                        DT_area = SHconexion.Devuelve_Area_Rechazo(" AND FechaSalida IS NULL");
                    }
                    else
                    {

                        DT_area = SHconexion.Devuelve_Area_Rechazo(" ");
                    }
                    dgv_AreaRechazo.EditIndex = -1;
                    Rellenar_grid();

                }
                catch (Exception)
                {

                }
            }
            if (e.CommandName.Equals("Eliminar"))
            {
                try
                {
                    int Id = Convert.ToInt16(e.CommandArgument);
                    
                    //MODIFICAR
                    SHconexion.Eliminar_Area_Rechazo(Id);
                    if (CheckHistorico.Checked)
                    {
                        DT_area = SHconexion.Devuelve_Area_Rechazo(" AND FechaSalida IS NULL");
                    }
                    else
                    {
                        DT_area = SHconexion.Devuelve_Area_Rechazo(" ");
                    }
                    dgv_AreaRechazo.EditIndex = -1;
                    Rellenar_grid();
                }
                catch (Exception)
                {

                }
            }
            if (e.CommandName.Equals("NuevaLinea"))
            {

                InputNuevoProducto.Value = "";
                DropNuevoResponsableEntrada.SelectedValue = "-";
                InputNuevoCantidad.Value = "0";

                InputNuevoMotivo.Value = "";
                DropNuevoResponsableSalida.SelectedValue = "-";
                InputNuevaFechaSalida.Value = "";
                InputNuevoObservaciones.Value = "";


                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupMODALNuevoEquipo();", true);

            }
        }

       
        // carga la lista utilizando un filtro
        protected void Cargar_filtro(object sender, EventArgs e)
        {
            
            Conexion_SMARTH SHconexion = new Conexion_SMARTH();
            if (CheckHistorico.Checked)
            {
                DT_area = SHconexion.Devuelve_Area_Rechazo(" AND FechaSalida IS NULL");
            }
            else
            {
                DT_area = SHconexion.Devuelve_Area_Rechazo(" ");
            }
            Rellenar_grid();
        }

           }

}