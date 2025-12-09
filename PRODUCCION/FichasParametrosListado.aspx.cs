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

namespace ThermoWeb.PRODUCCION
{
    public partial class FichasParametrosListado : System.Web.UI.Page
    {
        private static int OrdenFichaFECHA = 0;
        private static int OrdenFichaReferencia = 0;

        private static string orderby = " ORDER BY Fecha DESC";
        private static DataSet ds_listficha = new DataSet();
        /*protected GridView dgv_ListaFichasParam;*/

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Conexion conexion = new Conexion();
                Conexion_SMARTH SHconexion = new Conexion_SMARTH();
                DataTable ListaProductos = SHconexion.Devuelve_listado_FICHAS_PRODUCTOS_SEPARADOR();
                {
                    for (int i = 0; i <= ListaProductos.Rows.Count - 1; i++)
                    {
                        DatalistReferencias.InnerHtml = DatalistReferencias.InnerHtml + System.Environment.NewLine +
                            String.Format("<option value='{0}'>", ListaProductos.Rows[i][1]);
                    }
                }

                DataTable ListaMoldes = SHconexion.Devuelve_listado_FICHAS_MOLDES_SEPARADOR();
                {
                    for (int i = 0; i <= ListaMoldes.Rows.Count - 1; i++)
                    {
                        DatalistMoldes.InnerHtml = DatalistMoldes.InnerHtml + System.Environment.NewLine +
                            String.Format("<option value='{0}'>", ListaMoldes.Rows[i][1]);
                    }
                }

                ds_listficha = conexion.Lista_fichas_parametros_V2("","", orderby);
                Rellenar_grid();
            }

        }

        private void Rellenar_grid()
        {
            try
            {
                dgv_ListadoFichas.DataSource = ds_listficha;
                dgv_ListadoFichas.DataBind();
            }
            catch (Exception)
            {

            }
        }

        public void Ordenar_lineas(object sender, EventArgs e)
        {
            HtmlButton button = (HtmlButton)sender;
            string name = button.ID;
            switch (name)
            {
                case "BTNOrdenaFecha":
                    if (OrdenFichaFECHA == 0)
                    {
                        orderby = " ORDER BY Fecha DESC";
                        OrdenFichaFECHA = 1;
                    }
                    else
                    {

                        orderby = " ORDER BY Fecha ASC";
                        OrdenFichaFECHA = 0;
                    }
                    break;
                case "BTNOrdenaReferencia":
                    if (OrdenFichaReferencia == 0)
                    {
                        orderby = " ORDER BY Referencia DESC";
                        OrdenFichaReferencia = 1;
                    }
                    else
                    {

                        orderby = " ORDER BY Referencia ASC";
                        OrdenFichaReferencia = 0;
                    }
                    break;
            }
            BuscarFicha(null, null);
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
                        ds_listficha = conexion.Lista_fichas_parametros_V2("", "", orderby);
                        Rellenar_grid();
                        break;
                    }
                    i++;
                }
            }
            catch (Exception)
            {

            }
        }
        public void ContactsGridView_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CargaDetalle")
            {
                string[] RecorteFicha = e.CommandArgument.ToString().Split(new char[] { '¬' });

                Response.Redirect("FichasParametros.aspx?REFERENCIA=" + RecorteFicha[0].ToString() + "&MAQUINA=" + RecorteFicha[1].ToString() + "&VERSION="+ RecorteFicha[2].ToString());
            }
        }


        public void BuscarFicha(object sender, EventArgs e)
        {
            try
            {
                Conexion conexion = new Conexion();
                string queryreferencia = "";
                string querymolde = "";
                if (tbReferencia.Value != "")
                {
                    string[] RecorteReferencia = tbReferencia.Value.Split(new char[] { '¬' });
                    queryreferencia = " AND FM.Referencia = " + RecorteReferencia[0].ToString();
                }
                if (tbMolde.Value != "")
                {
                    string[] RecorteMolde = tbMolde.Value.Split(new char[] { '¬' });
                    querymolde = " AND FIC.CodMolde = " + RecorteMolde[0].ToString();
                }
                ds_listficha = conexion.Lista_fichas_parametros_V2(queryreferencia, querymolde, orderby);
                Rellenar_grid();
            }
            catch (Exception)
            {

            }
        }


       

    }
}
