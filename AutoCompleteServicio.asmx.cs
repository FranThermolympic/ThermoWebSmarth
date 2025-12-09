using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.IO;

namespace ThermoWeb
{
    [WebService(Namespace = "http://www.thermolympic.es/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    public class AutoCompleteServicio : System.Web.Services.WebService
    {
        //Devuelve Materials
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string[] GetAutoCompleteListaMateriales(string term)
        {
            // Aquí obtienes los datos de autocompletado basados en el término de búsqueda.
            // Puedes consultar tu fuente de datos (base de datos, lista, etc.) y devolver un arreglo de cadenas.

            // Ejemplo de datos de retorno:
            //string[] data = new string[] { "Opción 1", "Opción 2", "Opción 3" };
            try
            {
                Conexion_SMARTH conexion = new Conexion_SMARTH();
                DataTable DT_Materiales = conexion.Devuelve_Lista_Materiales_SEPARADOR_NAV("(I.[No_] LIKE '2%' or I.[No_] LIKE '3%' or I.[No_] LIKE '1%')");

                int numRows = DT_Materiales.Rows.Count;

                // Crear un arreglo de strings del mismo tamaño que el número de filas
                string[] data = new string[numRows];

                // Iterar a través de las filas del DataTable y almacenar los valores en el arreglo
                for (int i = 0; i < numRows; i++)
                {
                    data[i] = DT_Materiales.Rows[i]["MATFILTRO"].ToString();
                }


                // Filtrar las opciones basadas en el término de búsqueda
                var filteredData = data.Where(item => item.ToLower().Contains(term.ToLower())).ToArray();

                return filteredData;
            }
            catch (Exception ex)
            {
                string[] data = new string[] { "Opción 1", "Opción 2", "Opción 3" };
                var filteredData = data.Where(item => item.ToLower().Contains(term.ToLower())).ToArray();
                return filteredData;

            }
        }

        //Devuelve Nums Operarios
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string[] GetAutoCompleteListaOperariosAnoniNUMS(string term)
        {
            // Aquí obtienes los datos de autocompletado basados en el término de búsqueda.
            // Puedes consultar tu fuente de datos (base de datos, lista, etc.) y devolver un arreglo de cadenas.

            // Ejemplo de datos de retorno:
            //string[] data = new string[] { "Opción 1", "Opción 2", "Opción 3" };
            try
            {
                Conexion_SMARTH conexion = new Conexion_SMARTH();
                DataTable DT_Operarios = conexion.Devuelve_listado_OPERARIOS_ACTIVOS();
                int numRows = DT_Operarios.Rows.Count;

                // Crear un arreglo de strings del mismo tamaño que el número de filas
                string[] data = new string[numRows];

                // Iterar a través de las filas del DataTable y almacenar los valores en el arreglo
                for (int i = 0; i < numRows; i++)
                {
                    string[] RecorteOP = DT_Operarios.Rows[i]["OPERARIO"].ToString().Split(new char[] { '¬' });
                    data[i] = RecorteOP[0].ToString().Trim();
                }


                // Filtrar las opciones basadas en el término de búsqueda
                var filteredData = data.Where(item => item.ToLower().Contains(term.ToLower())).ToArray();

                return filteredData;
            }
            catch (Exception ex)
            {
                string[] data = new string[] { "Opción 1", "Opción 2", "Opción 3" };
                var filteredData = data.Where(item => item.ToLower().Contains(term.ToLower())).ToArray();
                return filteredData;

            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string[] GetAutoCompleteListaMaterialesXAlmacen(string term, string almacen)
        {
            // Aquí obtienes los datos de autocompletado basados en el término de búsqueda.
            // Puedes consultar tu fuente de datos (base de datos, lista, etc.) y devolver un arreglo de cadenas.

            // Ejemplo de datos de retorno:
            //string[] data = new string[] { "Opción 1", "Opción 2", "Opción 3" };
            try
            {
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                DataTable StockAlmacen = SHConexion.Devuelve_StockAlmacen_SERVNAV(almacen, "Y", "");
              
                int numRows = StockAlmacen.Rows.Count;

                // Crear un arreglo de strings del mismo tamaño que el número de filas
                string[] data = new string[numRows];

                // Iterar a través de las filas del DataTable y almacenar los valores en el arreglo
                for (int i = 0; i < numRows; i++)
                {                  
                    data[i] = StockAlmacen.Rows[i][0] + "¬" + StockAlmacen.Rows[i][1];
                }

                // Filtrar las opciones basadas en el término de búsqueda
                var filteredData = data.Where(item => item.ToLower().Contains(term.ToLower())).ToArray();

                return filteredData;
            }
            catch (Exception ex)
            {
                string[] data = new string[] { "No hay productos en este almacén" };
                var filteredData = data.Where(item => item.ToLower().Contains(term.ToLower())).ToArray();
                return filteredData;

            }
        }

        //EXPORTACIONES BMS
        [WebMethod]
        public void ExportarCajasBMS()
        {
            try
            {
                Conexion_SMARTH conexion = new Conexion_SMARTH();

                //Exporto las cajas desde BMS a SMARTH Y recupero el datatable de SMARTH
                DataTable CajasGeneradas = conexion.Devuelve_Cajas_BMS_SMARTH();

                if (CajasGeneradas.Rows.Count > 0)
                { 
                    //Exporto el archivo al servidor, si la exportacions se completa, actualizo la salida en BD
                    if (ExportarCSV(CajasGeneradas))
                    {
                        conexion.Actualiza_Cajas_SMARTH_Exportadas();
                    }
                }

            }
            catch (Exception ex)
            {
                // Maneja las excepciones según tus necesidades
                throw new ApplicationException("Error al exportar datos a CSV", ex);
                //ENVIA UN CORREO PARA AVISAR DE UN ERROR AL EXPORTAR
                
            }
        }

        private bool ExportarCSV(DataTable dataTable)
        {
            try
            {
                string filePath = @"\\thermonav.thermolympic.local\ExportNAV\BMSEXPORT_" + DateTime.Now.ToString("yy-MM-dd_HHmmss") + "SH.CSV";

                // Abre un StreamWriter para escribir en el archivo CSV
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    /*
                    // Escribe las cabeceras (nombres de columnas)
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        sw.Write(column.ColumnName + ",");
                    }
                    sw.WriteLine();
                    */

                    // Escribo los datos
                    foreach (DataRow row in dataTable.Rows)
                    {
                        for (int i = 0; i < dataTable.Columns.Count; i++)
                        {
                            sw.Write(row[i].ToString());
                            if (i < dataTable.Columns.Count - 1)
                            {
                                sw.Write(";");
                            }
                        }
                        sw.WriteLine();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                mandar_mail("Error al exportar cajas a CSV", ex.Message.ToString());
                return false;
            }

        }

        //METODO DE NOTIFICACIONE DE ERRORES
        public void mandar_mail(string mensaje, string subject)
        {
            //Create MailMessage
            MailMessage email = new MailMessage();

            email.To.Add(new MailAddress("pedro@thermolympic.es"));              
            email.From = new MailAddress("bms@thermolympic.es");
            email.Subject = subject;
            email.Body = mensaje;
            email.IsBodyHtml = true;
            email.Priority = MailPriority.Normal;

            //Definir objeto SmtpClient
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.thermolympic.es";
            smtp.Port = 25;
            smtp.EnableSsl = false;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("bms@thermolympic.es", "010477Bms");

            //Enviar email
            try
            {
                smtp.Send(email);
                email.Dispose();
            }
            catch (Exception)
            {
            }
        }

        [WebMethod]
        public void Exportar_Datos_Mantenimiento()
        {
            try
            {
                Conexion_SMARTH conexion = new Conexion_SMARTH();

                //Exporto las cajas desde BMS a SMARTH Y recupero el datatable de SMARTH
                conexion.Leer_HorasProductivasFabrica();
                conexion.Leer_HorasProductivasMaquina();
                conexion.Leer_HorasProductivasMolde();
                //conexion.LimpiarTablaPiezasEnviadas();
                conexion.Leer_piezas_enviadas_NAV();
                conexion.Leer_coste_chatarras_BMS();

            }
            catch (Exception ex)
            {
                // Maneja las excepciones según tus necesidades
                mandar_mail("Error en el exportador de mantenimiento","Error al exportar");
                throw new ApplicationException("Error al exportar", ex);
                //ENVIA UN CORREO PARA AVISAR DE UN ERROR AL EXPORTAR

            }
        }
    }
}
