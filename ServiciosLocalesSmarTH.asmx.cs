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
    /// <summary>
    /// Descripción breve de ServiciosLocalesSmarTH
    /// </summary>
    [WebService(Namespace = "http://www.thermolympic.es/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    //[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    //[System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class ServiciosLocalesSmarTH : System.Web.Services.WebService
    {
        //Conectores del reloj
        [WebMethod]

        public void Importar_Absentismo_Reloj()
        {            
           //ABSENTISMO
            try
            {
                string rutaArchivo = @"\\dcthermo\datosred\SMARTH\EXPORTACIONES\ABSENTISMOTHERMO.csv";
                using (StreamReader sr = new StreamReader(rutaArchivo))
                {

                    string linea;
                    while ((linea = sr.ReadLine()) != null)
                    {
                        string[] columnas = linea.Split(';');
                        //CAPTURO LAS COLUMNAS
                        string NUMOPERARIO = columnas[0];
                        string NOMBREOPERARIO = columnas[1];
                        string FECHA = columnas[2];
                        int MINTEORICO = Convert.ToInt32(columnas[3]);
                        int SININCIDENCIA = Convert.ToInt32(columnas[4]);
                        int ENFERMEDADLARGA = Convert.ToInt32(columnas[5]);
                        int ACCIDENTELAB = Convert.ToInt32(columnas[6]);
                        int BAJALAB = Convert.ToInt32(columnas[7]);
                        int ENFERMEDAD = Convert.ToInt32(columnas[8]);
                        int LACTANCIA30MIN = Convert.ToInt32(columnas[9]);
                        int LACTANCIA60MIN = Convert.ToInt32(columnas[10]);
                        int VISITAMEDICO = Convert.ToInt32(columnas[11]);
                        int ENFERMEDADFAMILIAR = Convert.ToInt32(columnas[12]);
                        int EXAMENES = Convert.ToInt32(columnas[13]);
                        int DEBERINEXCUSABLE = Convert.ToInt32(columnas[14]);
                        int NACIMIENTOHIJO = Convert.ToInt32(columnas[15]);
                        int MATRIMONIOS = Convert.ToInt32(columnas[16]);
                        int MATERNIDAD = Convert.ToInt32(columnas[17]);
                        int LICENCIASVARIAS = Convert.ToInt32(columnas[18]);
                        int PATERNIDAD = Convert.ToInt32(columnas[19]);
                        int LACTANCIACOMPACTACION = Convert.ToInt32(columnas[20]);
                        int FALLECIMIENTOFAM = Convert.ToInt32(columnas[21]);
                        int CAMBIODOMICILIO = Convert.ToInt32(columnas[22]);
                        int LICENCIABODA = Convert.ToInt32(columnas[23]);
                        int PERMISORETRIBUIDO = Convert.ToInt32(columnas[24]);
                        int PERMISONORETRIBUIDO = Convert.ToInt32(columnas[25]);
                        int RETRASOAPROBADO = Convert.ToInt32(columnas[26]);
                        int HORASSINDICALES = Convert.ToInt32(columnas[27]);
                        // ...

                        // Insertar en la base de datos
                        Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                        SHConexion.Insertar_absentismo(NUMOPERARIO, NOMBREOPERARIO, FECHA, MINTEORICO, SININCIDENCIA, ENFERMEDADLARGA, ACCIDENTELAB, BAJALAB, ENFERMEDAD, LACTANCIA30MIN, LACTANCIA60MIN, VISITAMEDICO, ENFERMEDADFAMILIAR, EXAMENES,
                                                       DEBERINEXCUSABLE, NACIMIENTOHIJO, MATRIMONIOS, MATERNIDAD, LICENCIASVARIAS, PATERNIDAD, LACTANCIACOMPACTACION, FALLECIMIENTOFAM, CAMBIODOMICILIO, LICENCIABODA, PERMISORETRIBUIDO, PERMISONORETRIBUIDO, RETRASOAPROBADO, HORASSINDICALES);

                    }
                    
                }
                File.Delete(rutaArchivo);

            }
            catch (Exception ex)
            {
                mandar_mail(ex.Message.ToString(),"Error en la importación del absentismo");
            }
            //OEE
            try
            {
                string rutaArchivo2 = @"\\dcthermo\datosred\SMARTH\EXPORTACIONES\KPI_OEE.csv";
                using (StreamReader sr = new StreamReader(rutaArchivo2))
                {
                    string linea;
                    while ((linea = sr.ReadLine()) != null)
                    {
                        string[] columnas = linea.Split(';');
                        //CAPTURO LAS COLUMNAS
                        int AÑO = Convert.ToInt32(DateTime.Now.ToString("yyyy"));
                        int MES = Convert.ToInt32(columnas[0]);
                        string OEEGral = columnas[1];
                        string OEECal = columnas[4];

                        if (MES == 12 && Convert.ToInt32(DateTime.Now.ToString("MM")) == 1)
                        {
                            AÑO = AÑO - 1;
                        }
                        // Insertar en la base de datos
                        Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                        SHConexion.Insertar_OEE(AÑO, MES, OEEGral, OEECal);

                    }

                }
                File.Delete(rutaArchivo2);

            }
            catch (Exception ex)
            {
                mandar_mail(ex.Message.ToString(), "Error en la importación del OEE CSV");
            }
        }


        public void Importar_OEE_CSV()
        {
            

            
        }

        //Servicio de impresiones
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ImpresoraRecepcionProducto(string impresora, int numetiquetas, string producto, string descripcion, string cantidad, string lote, string lotecliente, string proveedor, string numpedido)
        {
            // Dirección de impresora y puerto
            //NAVE 3 - IMPRESORA AMARILLA: ipAddress = "10.0.0.185";
            string ipAddress = ""; //AÑADIR IP IMPRESORA           
            int port = 9100;
            if (descripcion.Length > 35)
            {
                descripcion = descripcion.Substring(0, 34);
            }
            //parchear descripcion si mayor de 38

            string ZPLString = "";
            string ETIQUETA = "RECEPCION";
            switch (ETIQUETA)
            {
                case "RECEPCION":
                    switch (impresora)
                    {
                        case "RECEPCION_THERMO":
                            ipAddress = "10.0.0.156";  //AÑADIR IP IMPRESORA   
                            break;
                        case "RECEPCION_OFICINA":
                            ipAddress = "10.0.0.157";  //AÑADIR IP IMPRESORA   
                            break;
                    }
                    ZPLString = "^XA" +
                                 "^[L,2^FS^XZ" +
                                 "^XA" +
                                 "^MMT" +
                                 "^LS0^LT0" +
                                 "^FWR^FPH" +
                                 "^MTD" +
                                 "^MNA" +
                                 "^LH2,10^FS" +
                                 "^FX Lineas verticales" +
                                 "^FO15,15^GB765,1,3^FS" +
                                 "^FO15,870^GB765,1,3^FS" +
                                 "^FO570,540^GB210,1,3^FS" +
                                 "^FO15,590^GB145,1,3^FS" +
                                 "^FX Lineas horizontales" +
                                 "^FO15,15^GB1,855,3^FS" +
                                 "^FO160,15^GB1,855,3^FS" +
                                 "^FO570,15^GB1,855,3^FS" +
                                 "^FO780,15^GB1,855,3^FS" +
                                 "^FX Descripciones de campo" +
                                 "^FO125,30^A0R30,30^FDNo. LOTE:^FS" +
                                 "^FO125,600^A0R30,30^FDCANTIDAD:^FS" +
                                 "^FO745,30^A0R30,30^FDREFERENCIA:^FS" +
                                 "^FO530,35^A0R35,35^FDDESCRIPCION PIEZA:^FS" +
                                 "^FX Campos variables" +
                                 "^FO465,30^FB480,2,0,C,0^A0R,120,120^FD" + producto + "^FS" +
                                 "^FO165,35^FB850,3,0,L,0^A0R,110,110^FD" + descripcion + "^FS" +
                                 "^FO0,30^FB580,1,0,C,0^A0R,120,120^FD" + lote + "^FS" +
                                 "^FO15,600^FB280,1,0,C,0^A0R,90,90^FD" + cantidad + "^FS" +
                                 "^FO580,620^BXR,4,200,48,48^FD[]>E2*06_P" + producto + "_Q" + cantidad + "_T" + lote + "_1T" + lotecliente + "_V" + proveedor + "_K" + numpedido + "E2*11^FS" +
                                 "^XZ";
                    
                    break;
            }

            string ZPLStringOUTPUT = "";
            for (int i = 0; i <= numetiquetas - 1; i++)
            {
                ZPLStringOUTPUT = ZPLStringOUTPUT + ZPLString;
            }

            try
            {
                             
                // Agregar columnas al DataTable

                if (impresora == "RECEPCION_THERMO" || impresora == "RECEPCION_OFICINA")
                {
                    // Open connection
                    System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient();
                    client.Connect(ipAddress, port);

                    // Write ZPL String to connection
                    System.IO.StreamWriter writer = new System.IO.StreamWriter(client.GetStream());
                    writer.Write(ZPLStringOUTPUT);
                    writer.Flush();

                    // Close Connection
                    writer.Close();
                    client.Close();
                }
                else
                {
                    DataTable LogTable = new DataTable();
                    LogTable.Columns.Add("impresora", typeof(string));
                    LogTable.Columns.Add("numetiquetas", typeof(int));
                    LogTable.Columns.Add("producto", typeof(string));
                    LogTable.Columns.Add("descripcion", typeof(string));
                    LogTable.Columns.Add("cantidad", typeof(string));
                    LogTable.Columns.Add("lote", typeof(string));
                    LogTable.Columns.Add("lotecliente", typeof(string));
                    LogTable.Columns.Add("proveedor", typeof(string));
                    LogTable.Columns.Add("numpedido", typeof(string));

                    DataRow row1 = LogTable.NewRow();
                    row1["impresora"] = impresora;
                    row1["numetiquetas"] = numetiquetas;
                    row1["producto"] = producto;
                    row1["descripcion"] = descripcion;
                    row1["cantidad"] = cantidad;
                    row1["lote"] = lote;
                    row1["lotecliente"] = lotecliente;
                    row1["proveedor"] = proveedor;
                    row1["numpedido"] = numpedido;

                    LogTable.Rows.Add(row1);
                    ExportarCSVLOG(LogTable);
                }
                // Crear y agregar más filas si es necesario
              

                /*

                //Conexion_SMARTH SHconexion = new Conexion_SMARTH();
               
                // return true;
                */


            }
            catch (Exception ex)
            {
                mandar_mail(ex.ToString(), "Error impresora");
                //return false;
            }

        }

        private bool ExportarCSVLOG(DataTable dataTable)
        {
            try
            {
                string filePath = @"\\thermonav.thermolympic.local\ExportNAV\LOGIMPRESORA_" + DateTime.Now.ToString("yy-MM-dd_HHmmss") + "SH.CSV";

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

    }
}
