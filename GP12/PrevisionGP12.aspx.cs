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

namespace ThermoWeb.GP12
{
    public partial class PrevisionGP12 : System.Web.UI.Page
    {

        private static DataTable NewPrevisionTotal = new DataTable();
        private static DataTable NewPrevisionRetrasadas = new DataTable();
        private static DataTable StocksGP12 = new DataTable();
        private static DataTable ProdXHoras = new DataTable();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Conexion_GP12 conexion = new Conexion_GP12();
                conexion.LimpiarTablaPrevisionesNAV();
                conexion.leer_previsionesNAV();    
                NewPrevisionTotal = conexion.Lee_totales_grid_previsionNAV_SMARTH("", " and FechaEntrega >= '"+DateTime.Now.ToString("dd/MM/yyyy")+"'");
                NewPrevisionRetrasadas = conexion.Lee_grid_previsionNAV_SMARTH("", " and FechaEntrega < '" + DateTime.Now.ToString("dd/MM/yyyy") + "'");
                //ds_Prevision = conexion.lee_grid_previsionNAV();

                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                StocksGP12 = SHConexion.Devuelve_StockAlmacen_SERVNAV("CA", "Y", "");
                ProdXHoras = conexion.Devuelve_horasGP12_X_Producto();
                //ds_PrevisionAtrasados = conexion.lee_grid_prevision_atrasadasNAV();
                Rellenar_grid();

            }

        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    string revision = "";
                    if (SwitchOcultarRevision.Checked)
                    {
                        revision = " AND (Id <> 0 OR RequiereMontaje = 1)";
                    }
                    Conexion_GP12 conexion = new Conexion_GP12();
                    string FILTRO_1 = dgv_PedidosClientes.DataKeys[e.Row.RowIndex].Value.ToString();
                    GridView dgv_PrevisionGP12 = e.Row.FindControl("dgv_PrevisionGP12") as GridView;
                    dgv_PrevisionGP12.DataSource = conexion.Lee_grid_previsionNAV_SMARTH(revision, " and FechaEntrega = '"+FILTRO_1+"'");
                    dgv_PrevisionGP12.DataBind();
                }
            
                catch (Exception ex)
                { 
                }
            }

        }
        
        public void CargarSwitch(object sender, EventArgs e)
        {
            Conexion_GP12 conexion = new Conexion_GP12();
            string revision = "";
            if (SwitchOcultarRevision.Checked)
            {
                revision = " AND Id <> 0 ";
            }
            NewPrevisionTotal = conexion.Lee_totales_grid_previsionNAV_SMARTH(revision, " and FechaEntrega >= '" + DateTime.Now.ToString("dd/MM/yyyy") + "'");
            Rellenar_grid();
        }
       
        private void Rellenar_grid()
        {
            try
            {

                dgv_PedidosClientes.DataSource = NewPrevisionTotal;
                dgv_PedidosClientes.DataBind();
                GridRetrasadas.DataSource = NewPrevisionRetrasadas;
                GridRetrasadas.DataBind();

                

                var query = from t1 in StocksGP12.AsEnumerable()
                            join t2 in ProdXHoras.AsEnumerable() on t1.Field<string>("producto") equals t2.Field<string>("Referencia")
                            select new
                            {
                                Referencia = t1.Field<string>("producto"),
                                Descripcion = t1.Field<string>("Descripcion"),
                                Piezas = Convert.ToInt32(t1.Field<string>("stock")),
                                PZHoras = t2.Field<decimal>("PZHORA")
                            };

                DataTable NecesidadesTotales = new DataTable();
                NecesidadesTotales.Columns.Add("Referencia", typeof(string));
                NecesidadesTotales.Columns.Add("Descripcion", typeof(string));
                NecesidadesTotales.Columns.Add("Piezas", typeof(int));
                NecesidadesTotales.Columns.Add("PZHoras", typeof(decimal));
                NecesidadesTotales.Columns.Add("HorasRev", typeof(decimal));

                foreach (var row in query)
                {
                    NecesidadesTotales.Rows.Add(row.Referencia, row.Descripcion, row.Piezas, row.PZHoras, row.Piezas / row.PZHoras);
                }

                dgv_Almacenes.DataSource = NecesidadesTotales;
                dgv_Almacenes.DataBind();


                // Calcular el sumatorio de todas las líneas de "Piezas/Horas"
                decimal totalPiezasHoras = Math.Round(NecesidadesTotales.AsEnumerable().Sum(row => row.Field<decimal>("HorasRev")),2);
                
                int totalPiezas = NecesidadesTotales.AsEnumerable().Sum(row => row.Field<int>("Piezas"));

                lblHoras.Text =  totalPiezas.ToString() + "piezas (" + totalPiezasHoras.ToString() + " horas de revisión)";
                //dgv_atrasados.DataSource = ds_PrevisionAtrasados;
                //dgv_atrasados.DataBind();
            }
            catch (Exception ex)
            {

            }
        }
        
    }

}