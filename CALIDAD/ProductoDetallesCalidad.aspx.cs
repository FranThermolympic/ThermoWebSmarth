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
using System.Globalization;

namespace ThermoWeb.CALIDAD
{
    public partial class ProductoDetallesCalidad : System.Web.UI.Page
    {

        private static DataTable OrdenaTabla = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDatos();
            }
        }

        public void CargarDatos()
        {
            try
            {
                Conexion_SMARTH SHconexion = new Conexion_SMARTH();

                DataTable ListaProductos = SHconexion.Devuelve_listado_PRODUCTOS();
                {
                    for (int i = 0; i <= ListaProductos.Rows.Count - 1; i++)
                    {
                        DatalistReferencias.InnerHtml = DatalistReferencias.InnerHtml + System.Environment.NewLine +
                            String.Format("<option value='{0}'>", ListaProductos.Rows[i][0]);
                    }
                }

                DataTable ListaMoldes = SHconexion.Devuelve_listado_MOLDES();
                {
                    for (int i = 0; i <= ListaMoldes.Rows.Count - 1; i++)
                    {
                        DatalistMolde.InnerHtml = DatalistMolde.InnerHtml + System.Environment.NewLine +
                            String.Format("<option value='{0}'>", ListaMoldes.Rows[i][0]);
                    }
                }

            }
            catch (Exception)
            {

            }
        }

        public void RellenarGrids(object sender, EventArgs e)
        {
            //string referencia = "61752224";
            if (selectReferencia.Value != "" || selectMolde.Value != "")
            {
            string[] RecorteReferencia = selectReferencia.Value.Split(new char[] { '¬' });
            string referencia = RecorteReferencia[0].Trim();

            string[] RecorteMolde = selectMolde.Value.Split(new char[] { '¬' });
            string molde = RecorteMolde[0].Trim();

            string fechainicio = "01/01/2023";
            if (InputFechaDesde.Value != "")
            {
                try
                {
                    fechainicio = InputFechaDesde.Value;
                }
                catch (Exception ex)
                { 

                }
            }

            string fechafin = "";
            if (InputFechaHasta.Value != "")
            {
                try
                {
                    fechafin = InputFechaHasta.Value;
                }
                catch (Exception ex)
                {

                }
            }

            Conexion_SMARTH SHconexion = new Conexion_SMARTH();
            DataTable DT_BMS = SHconexion.Devuelve_Datos_Productivos_BMS(referencia, molde, fechainicio, fechafin);
            DataTable DT_GP12 = SHconexion.Devuelve_Datos_Productivos_SMARTH(referencia,molde, fechainicio, fechafin);
            DataTable AUXPROD = SHconexion.Devuelve_Datos_Producto_Seleccionado(referencia,molde);
            // Paso 2: Combinar los DataTables
            DataTable combinedDataTable = new DataTable();
            foreach (DataColumn col in DT_BMS.Columns)
            {
                combinedDataTable.Columns.Add(col.ColumnName, col.DataType);
            }

            foreach (DataRow row in DT_BMS.Rows)
            {
                combinedDataTable.Rows.Add(row.ItemArray);
            }

            foreach (DataRow row in DT_GP12.Rows)
            {
                combinedDataTable.Rows.Add(row.ItemArray);
            }



            // Paso 3: Calcular la suma de los valores INT para cada columna
            var sums = new int[35];
            for (int i = 5; i < 40; i++)
            {
                foreach (DataRow row in combinedDataTable.Rows)
                {
                    sums[i - 5] += Convert.ToInt32(row[i]);
                }
            }

            // Filtrar columnas con suma 0
            var filteredColumns = sums.Select((sum, index) => new { Sum = sum, Index = index + 5 })
                                      .Where(item => item.Sum != 0)
                                      .OrderByDescending(item => item.Sum)
                                      .Select(item => combinedDataTable.Columns[item.Index].ColumnName)
                                      .ToArray();



            // Paso 4: Reordenar las columnas según la suma calculada
            DataTable finalDataTable = new DataTable();


            // Agregar las 6 primeras columnas al principio
            foreach (DataColumn col in combinedDataTable.Columns)
            {
                if (col.Ordinal < 5)
                {
                    finalDataTable.Columns.Add(col.ColumnName, col.DataType);
                }
            }

            foreach (string colName in filteredColumns)
            {
                finalDataTable.Columns.Add(colName, combinedDataTable.Columns[colName].DataType);
            }

            foreach (DataRow row in combinedDataTable.Rows)
            {
                finalDataTable.Rows.Add(row.ItemArray.Take(5).Concat(filteredColumns.Select(colName => row[colName])).ToArray());
            }

            DataTable DT_Etiquetas = SHconexion.Devuelve_AUX_TablaDefectosProduccion();
            foreach (DataRow row in DT_Etiquetas.Rows)
            {
                try
                {
                    string oldColumnName = row["IdDefecto"].ToString();
                    string newColumnName = row["DescripcionCOMDEF"].ToString();

                    if (finalDataTable.Columns.Contains(oldColumnName))
                    {
                        DataColumn column = finalDataTable.Columns[oldColumnName];
                        column.ColumnName = newColumnName;
                    }
                }
                catch (Exception ex)
                { }

            }

            dgv_GP12_Historico.DataSource = finalDataTable;
            dgv_GP12_Historico.DataBind();

            dgv_NoConformidades.DataSource = SHconexion.Devuelve_Datos_NC_SMARTH(referencia,molde, fechainicio, fechafin);
            dgv_NoConformidades.DataBind();

                if (molde != "")
                {
                    LABELGRIDVIEW.InnerText = "Detalles del molde: " + AUXPROD.Rows[0]["Molde"].ToString() + " - " + AUXPROD.Rows[0]["Descripcion"].ToString();
                    LABELGRIDVIEW.Visible = true;
                    LABELNC.Visible = true;
                }
                else
                {
                    LABELGRIDVIEW.InnerText = "Detalles del producto: " + AUXPROD.Rows[0]["Referencia"].ToString() + " - " + AUXPROD.Rows[0]["Descripcion"].ToString();
                    LABELGRIDVIEW.Visible = true;
                    LABELNC.Visible = true;
                }
           
            //LABELMANT.Visible = true;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "CargarCharts('"+referencia+ "','" + molde + "','" + fechainicio+"','"+fechafin+"');", true);
            }

        }

        protected void GridView_DataBoundHist(object sender, EventArgs e)
        {
            for (int i = 0; i <= dgv_GP12_Historico.Rows.Count - 1; i++)
            {
                dgv_GP12_Historico.Rows[i].Cells[1].BackColor = System.Drawing.Color.Gainsboro;
                dgv_GP12_Historico.Rows[i].Cells[1].ForeColor = System.Drawing.Color.Black;
                dgv_GP12_Historico.Rows[i].Cells[1].Font.Bold = true;

                dgv_GP12_Historico.Rows[i].Cells[2].BackColor = System.Drawing.Color.Gainsboro;
                dgv_GP12_Historico.Rows[i].Cells[2].ForeColor = System.Drawing.Color.Black;
                dgv_GP12_Historico.Rows[i].Cells[2].Font.Bold = true;
                dgv_GP12_Historico.Rows[i].Cells[2].Font.Italic = true;

                dgv_GP12_Historico.Rows[i].Cells[3].BackColor = System.Drawing.Color.Gainsboro;
                dgv_GP12_Historico.Rows[i].Cells[3].ForeColor = System.Drawing.Color.Black;
                dgv_GP12_Historico.Rows[i].Cells[3].Font.Bold = true;
                dgv_GP12_Historico.Rows[i].Cells[3].Font.Italic = true;

                dgv_GP12_Historico.Rows[i].Cells[4].BackColor = System.Drawing.Color.Gainsboro;
                dgv_GP12_Historico.Rows[i].Cells[4].ForeColor = System.Drawing.Color.Black;
                dgv_GP12_Historico.Rows[i].Cells[4].Font.Bold = true;
                dgv_GP12_Historico.Rows[i].Cells[4].Font.Italic = true;

                dgv_GP12_Historico.Rows[i].Cells[5].BackColor = System.Drawing.Color.Gainsboro;
                dgv_GP12_Historico.Rows[i].Cells[5].ForeColor = System.Drawing.Color.Black;
                dgv_GP12_Historico.Rows[i].Cells[5].Font.Bold = true;
                dgv_GP12_Historico.Rows[i].Cells[5].Font.Italic = true;
            }
                //dgv_GP12_Historico.Rows[i].Cells[7].BackColor = System.Drawing.Color.Red;
                //lblparent.ForeColor = System.Drawing.Color.White;
                //lblparent.Font.Bold = true;
            }

        [WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static List<object> GetChart(string producto, string molde, string fechainicio, string fechafin)
        {
            //Recupero el dato y lo filtro por la peticion
            Conexion_SMARTH SHconexion = new Conexion_SMARTH();
            DataTable DT_BMS = SHconexion.Devuelve_Datos_Productivos_BMS(producto,molde, fechainicio, fechafin);
            DataTable DT_GP12 = SHconexion.Devuelve_Datos_Productivos_SMARTH(producto,molde, fechainicio, fechafin);
            DataTable DT_Etiquetas = SHconexion.Devuelve_AUX_TablaDefectosProduccion();

            // Paso 2: Combinar los DataTables
            DataTable combinedDataTable = new DataTable();
            foreach (DataColumn col in DT_BMS.Columns)
            {
                combinedDataTable.Columns.Add(col.ColumnName, col.DataType);
            }

            foreach (DataRow row in DT_BMS.Rows)
            {
                combinedDataTable.Rows.Add(row.ItemArray);
            }

            foreach (DataRow row in DT_GP12.Rows)
            {
                combinedDataTable.Rows.Add(row.ItemArray);
            }

            // Paso 3: Calcular la suma de los valores INT para cada columna
            var sums = new int[35];
            for (int i = 5; i < 40; i++)
            {
                foreach (DataRow row in combinedDataTable.Rows)
                {
                    sums[i - 5] += Convert.ToInt32(row[i]);
                }
            }

            // Filtrar columnas con suma 0 y ordenarlas
            var filteredColumns = sums.Select((sum, index) => new { Sum = sum, Index = index + 5 })
                                      .Where(item => item.Sum != 0)
                                      .OrderByDescending(item => item.Sum)
                                      .Select(item => combinedDataTable.Columns[item.Index].ColumnName)
                                      .ToArray();

            // Crear un nuevo DataTable con columnas reordenadas
            DataTable reorderedDataTable = new DataTable();

            // Agregar las columnas filtradas
            foreach (string colName in filteredColumns)
            {
                reorderedDataTable.Columns.Add(colName, combinedDataTable.Columns[colName].DataType);
            }

            foreach (DataRow row in combinedDataTable.Rows)
            {
                reorderedDataTable.Rows.Add(filteredColumns.Select(colName => row[colName]).ToArray());
            }

            //Actualizo las cabeceras con los nombres de defecto comunes
            foreach (DataRow row in DT_Etiquetas.Rows)
            {
                string oldColumnName = row["IdDefecto"].ToString();
                string newColumnName = row["DescripcionCOMDEF"].ToString();

                if (reorderedDataTable.Columns.Contains(oldColumnName))
                {
                    DataColumn column = reorderedDataTable.Columns[oldColumnName];
                    column.ColumnName = newColumnName;
                }
            }


            //INICIAR LISTADOS

            //Defino el listado Inicial 
            List<object> chartData = new List<object>();
            if (reorderedDataTable.Rows.Count > 0)
                {
                    //Defino listado de labels y lo añado a ChartData
                    List<string> labels = new List<string>();
                DataRow row0 = reorderedDataTable.Rows[0];
                {
                    foreach (DataColumn dc0 in reorderedDataTable.Columns)
                    {
                        try
                        {
                            labels.Add(dc0.ColumnName.ToString());
                        }
                        catch
                        {
                            labels.Add("VAC");
                        }
                    }
                }
                chartData.Add(labels);

            
                //Defino listado serie de entradas PRODUCCCION
                List<int> series1 = new List<int>();
                DataRow row1 = reorderedDataTable.Rows[0];
                //DataRow row1 = finalDataTable.Rows[0];
                {
                    foreach (DataColumn dc in reorderedDataTable.Columns)
                    {
                        try
                        {
                            series1.Add(Convert.ToInt32(row1[dc]));
                        }
                        catch
                        {
                            series1.Add(0);
                        }
                    }
                }
                chartData.Add(series1);
            }
            
            
            if(reorderedDataTable.Rows.Count > 1)
            {
                //Defino listado serie de entradas GP12
                List<int> series2 = new List<int>();
                DataRow row2 = reorderedDataTable.Rows[1];
                {
                    foreach (DataColumn dc in reorderedDataTable.Columns)
                    {
                        try
                        {
                            series2.Add(Convert.ToInt32(row2[dc]));
                        }
                        catch
                        {
                            series2.Add(0);
                        }
                    }
                }
            chartData.Add(series2);
            }
            return chartData;
        }

        [WebMethod]
        public static List<object> GetChartAnual(string producto, string molde, string fechainicio, string fechafin)
        {
            //Recupero el dato y lo filtro por la peticion
            Conexion_SMARTH SHConexion = new Conexion_SMARTH();
            DataTable PRODUCCION = SHConexion.Devuelve_Datos_Productivos_BMS_MES(producto,molde, fechainicio, fechafin);
            DataTable GP12 = SHConexion.Devuelve_Datos_Productivos_SMARTH_MES(producto,molde, fechainicio, fechafin);
            DataTable NC = SHConexion.Devuelve_Datos_NC_SMARTH_MES(producto, molde,fechainicio, fechafin);

            DataTable resultadoTabla = new DataTable();
            resultadoTabla.Columns.Add("MESNOM", typeof(string));
            resultadoTabla.Columns.Add("PRODREV", typeof(decimal));
            resultadoTabla.Columns.Add("MALASPROD", typeof(decimal));
            resultadoTabla.Columns.Add("MALASGP12", typeof(decimal));
            resultadoTabla.Columns.Add("COSTEPROD", typeof(decimal));
            resultadoTabla.Columns.Add("COSTEGP12HORAS", typeof(decimal));
            resultadoTabla.Columns.Add("COSTEGP12PIEZAS", typeof(decimal));
            resultadoTabla.Columns.Add("COSTENC", typeof(decimal));
            //resultadoTabla.Columns.Add("CARGOS", typeof(decimal));
            //resultadoTabla.Columns.Add("RECHAZO", typeof(decimal));
            //resultadoTabla.Columns.Add("SELECCION_EXT", typeof(decimal));
            //resultadoTabla.Columns.Add("SELECCION_INT", typeof(decimal));
            //resultadoTabla.Columns.Add("OTROS", typeof(decimal));

            foreach (DataRow row1 in PRODUCCION.Rows)
            {
                string año = (string)row1["YEARINICIO"];
                string mes = (string)row1["MONTHINICIO"];
                string mesnom = (string)row1["FECHAINICIO"];
                decimal MALASPROD = (decimal)row1["Malas"];
                decimal ProdRev = (decimal)row1["ProdRev"];
                decimal COSTEPROD = (decimal)row1["COSTE"];
               

                DataRow matchingRow = null;
                foreach (DataRow row2 in GP12.Rows)
                {
                    if ((string)row2["YEARINICIO"] == año && (string)row2["MONTHINICIO"] == mes)
                    {
                        matchingRow = row2;
                        break;
                    }
                }
                int MALASGP12 = (matchingRow != null) ? (int)matchingRow["Malas"] : 0;
                decimal COSTEGP12HORAS = (matchingRow != null) ? (decimal)matchingRow["COSTEHORAS"] : 0;
                decimal COSTEGP12PIEZAS = (matchingRow != null) ? (decimal)matchingRow["COSTESCRAP"] : 0;

                DataRow matchingRow2 = null;
                foreach (DataRow row3 in NC.Rows)
                {
                    if ((string)row3["YEARINICIO"] == año && (string)row3["MONTHINICIO"] == mes)
                    {
                        matchingRow2 = row3;
                        break;
                    }
                }
                decimal COSTENC = (matchingRow2 != null) ? (decimal)matchingRow2["COSTENC"] : 0;
                //decimal COSTENCCARGOS = (matchingRow2 != null) ? (decimal)matchingRow2["CARGOS"] : 0;
                //decimal COSTENCRECHAZO = (matchingRow2 != null) ? (decimal)matchingRow2["RECHAZO"] : 0;
                //decimal COSTENCSELECCION_EXT = (matchingRow2 != null) ? (decimal)matchingRow2["SELECCION_EXT"] : 0;
                //decimal COSTENCSELECCION_INT = (matchingRow2 != null) ? (decimal)matchingRow2["SELECCION_INT"] : 0;
                //decimal COSTENCOTROS = (matchingRow2 != null) ? (decimal)matchingRow2["OTROS"] : 0;
                resultadoTabla.Rows.Add(mesnom, ProdRev, MALASPROD, MALASGP12,COSTEPROD, COSTEGP12HORAS, COSTEGP12PIEZAS, COSTENC);
            }

            //Defino el listado Inicial 
            List<object> chartData = new List<object>();

            //Defino listado de labels y lo añado a ChartData
            List<string> labels = new List<string>();
            foreach (DataRow row in resultadoTabla.Rows)
            {
                labels.Add(row["mesnom"].ToString());
            }
            chartData.Add(labels);


            //Defino listado serie de entradas de produccion
            //MALASPRODUCCION
            List<double> series1 = new List<double>();
            foreach (DataRow row in resultadoTabla.Rows)
            {
                try
                {
                    if (Convert.ToInt32(row["PRODREV"]) > 0)
                    {
                        series1.Add(Math.Round((Convert.ToDouble(row["MALASPROD"]) * 100) / Convert.ToDouble(row["PRODREV"]),2));
                    }
                    else
                    {
                        series1.Add(0.0);
                    }
                }
                catch
                {
                    series1.Add(0.0);
                }
            }
            chartData.Add(series1);

            //MALASGP12
            List<double> series2 = new List<double>();
            foreach (DataRow row in resultadoTabla.Rows)
            {
                try
                {
                    if (Convert.ToInt32(row["PRODREV"]) > 0)
                    {
                        series2.Add(Math.Round((Convert.ToDouble(row["MALASGP12"]) * 100) / Convert.ToDouble(row["PRODREV"]),2));
                    }
                    else
                    {
                        series2.Add(0.0);
                    }
                    //series2.Add(Convert.ToInt32(row["MALASGP12"]));
                }
                catch
                {
                    series2.Add(0.0);
                }
            }
            chartData.Add(series2);

            //COSTEPRODUCCION
            List<double> series3 = new List<double>();
            foreach (DataRow row in resultadoTabla.Rows)
            {
                try
                {
                    series3.Add(Convert.ToDouble(row["COSTEPROD"]));
                }
                catch
                {
                    series3.Add(0.0);
                }
            }
            chartData.Add(series3);

            //COSTEGP12
            List<double> series4 = new List<double>();
            foreach (DataRow row in resultadoTabla.Rows)
            {
                try
                {
                    series4.Add(Convert.ToDouble(row["COSTEGP12HORAS"]));
                }
                catch
                {
                    series4.Add(0.0);
                }
            }
            chartData.Add(series4);

            List<double> series5 = new List<double>();
            foreach (DataRow row in resultadoTabla.Rows)
            {
                try
                {
                    series5.Add(Convert.ToDouble(row["COSTEGP12PIEZAS"]));
                }
                catch
                {
                    series5.Add(0.0);
                }
            }
            chartData.Add(series5);

            //NO CONFORMIDADES
            List<double> series6 = new List<double>();
            foreach (DataRow row in resultadoTabla.Rows)
            {
                try
                {
                    series6.Add(Convert.ToDouble(row["COSTENC"]));
                }
                catch
                {
                    series6.Add(0.0);
                }
            }
            chartData.Add(series6);

            return chartData;
        }

       
    }

}