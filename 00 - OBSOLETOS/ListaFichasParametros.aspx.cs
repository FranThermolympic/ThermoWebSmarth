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

namespace ThermoWeb
{
    public partial class Lista_Ficha : System.Web.UI.Page
    {

        private static DataSet ds_listficha = new DataSet();
        /*protected GridView dgv_ListaFichasParam;*/

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Conexion conexion = new Conexion();
                ds_listficha = conexion.lista_fichas_parametros();
                rellenar_grid();
            }

        }

        protected void cargarlistaordenadareferencia(object sender, EventArgs e)
        {
            try
            {
                Conexion conexion = new Conexion();
                ds_listficha = conexion.lista_fichas_parametros();
                rellenar_grid();
                OrdenarRef.Visible = false;
                OrdenaFecha.Visible = true;
            }
                catch (Exception)
            {

            }

        }

        protected void cargarlistaordenadafecha(object sender, EventArgs e)
        {
            try
            {
                Conexion conexion = new Conexion();
                ds_listficha = conexion.lista_fichas_parametros_fecha();
                rellenar_grid();
                OrdenaFecha.Visible = false;
                OrdenarRef.Visible = true;
                
            }
            catch (Exception)
            {

            }
        }

        private void rellenar_grid()
        {
            try
            {
                dgv_ListaFichasParam.DataSource = ds_listficha;
                dgv_ListaFichasParam.DataBind();
            }
            catch (Exception)
            {

            }
        }
        public void GridView_RowDeleting(Object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int num_fila = Convert.ToInt16(e.RowIndex);
                int i = 0;
                foreach (DataRow row in ds_listficha.Tables[0].Rows)
                {
                    if (i == num_fila)
                    {
                        Conexion conexion = new Conexion();
                        conexion.eliminar_ficha_lista_V2(Convert.ToInt32(row["Referencia"]), Convert.ToInt32(row["Maquina"]), Convert.ToInt32(row["Version"]));
                        ds_listficha = conexion.lista_fichas_parametros();
                        rellenar_grid();
                        break;
                    }
                    i++;
                }
            }
            catch (Exception)
            {

            }
        }

        public void buscarFicha(Object sender, EventArgs e)
        {
            try
            {
                if (tbBuscarFicha.Value == "")
                {
                    {
                        Conexion conexion = new Conexion();
                        ds_listficha = conexion.lista_fichas_parametros();
                        rellenar_grid();
                    }
                }
                else
                {
                    Conexion conexion = new Conexion();
                    ds_listficha = conexion.busca_fichas_parametros(Convert.ToInt32(tbBuscarFicha.Value));
                    rellenar_grid();
                }
            }
            catch (Exception)
            {

            }
        }

        public void buscarFichaMolde(Object sender, EventArgs e)
        {
            try
            {
                if (tbBuscarFichaMolde.Value == "")
                {
                    {
                        Conexion conexion = new Conexion();
                        ds_listficha = conexion.lista_fichas_parametros();
                        rellenar_grid();
                    }
                }
                else
                {
                    Conexion conexion = new Conexion();
                    ds_listficha = conexion.busca_fichas_parametrosMolde(Convert.ToInt32(tbBuscarFichaMolde.Value));
                    rellenar_grid();
                }
            }
            catch (Exception)
            {

            }
        }
        public void ContactsGridView_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName=="Redirect")
        {
            Response.Redirect("FichasParametros.aspx?REFERENCIA=" + e.CommandArgument.ToString());
        }
        }
        
    }
}
