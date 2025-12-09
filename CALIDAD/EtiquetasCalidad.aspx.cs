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

namespace ThermoWeb.CALIDAD
{
    public partial class EtiquetasCalidad : System.Web.UI.Page
    {

        private static DataSet ds_Liberaciones = new DataSet();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //LanzaEtisLocal();
                Conexion_SMARTH SHconexion = new Conexion_SMARTH();
                DataTable DTProductos = SHconexion.Devuelve_productos_SEPARADOR();
                RellenarGrid();
                for (int i = 0; i <= DTProductos.Rows.Count - 1; i++)
                {
                    DatalistProducto.InnerHtml = DatalistProducto.InnerHtml + System.Environment.NewLine +
                        String.Format("<option value='{0}'>", DTProductos.Rows[i][0]);
                }
                
                //ImprimirEtiquetasV2(1, "REVISION 100%", "61752125", "1049242S02 N01 SPACER PIPE 5500104929", "86148", "Piezas mal identificadas y cantida No O.K. Se detecta en el almacén soportes mal identificados y cantidades que no corresponden con el albarán. Teóricamente, según albarán llegaron 400uds de la ref. 5081770. pero en realidad llegaron: 80 uds 5081771 120 uds 5081770", 100, 95, 246);
            }
            if (IsPostBack)
            {
                RellenarGrid();
            }
                       
        }

        public void RellenarGrid()
        {
            Conexion_SMARTH SHconexion = new Conexion_SMARTH();
            DataTable HistoricoEtiquetas = SHconexion.Devuelve_Etiquetas_Auxiliares("");
            dgv_Historico_Etiquetas.DataSource = HistoricoEtiquetas;
            dgv_Historico_Etiquetas.DataBind();
        }

        public void GridViewCommandEventHandler(Object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "CargaDetalle")
                {
                    Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                    DataTable AUXEtiqueta = SHConexion.Devuelve_Etiquetas_Auxiliares(" WHERE ID = " + e.CommandArgument.ToString() + "");
                    NUMProducto.Value = AUXEtiqueta.Rows[0]["Referencia"].ToString().Trim() + "¬" + AUXEtiqueta.Rows[0]["Descripcion"].ToString().Trim();
                    InputAlbaran.Text = AUXEtiqueta.Rows[0]["Lote"].ToString().Trim();
                    InputCantidad.Text = AUXEtiqueta.Rows[0]["CantidadORI"].ToString().Trim();
                    InputCantidadCaja.Text = AUXEtiqueta.Rows[0]["CantidadREAL"].ToString().Trim();
                    InputObservaciones.Text = AUXEtiqueta.Rows[0]["Observaciones"].ToString().Trim();
                    InputOperario.Text = AUXEtiqueta.Rows[0]["Operario"].ToString().Trim();
                    NumEtiquetas.Value = "1";

                    switch (AUXEtiqueta.Rows[0]["Etiqueta"].ToString().Trim())
                    {
                        case "FPOC1101":
                            IdTipoEtiqueta.Value = "FPOC1101";
                            LblTipoRevision.Text = AUXEtiqueta.Rows[0]["Tarea"].ToString().Trim();
                            AUXColCantCaja.Attributes.Add("class", "col-lg-6 invisible");
                            lblCantRet.InnerText = "Cantidad retenida:";
                            break;
                        case "FPOC1110":
                            IdTipoEtiqueta.Value = "FPOC1110";
                            LblTipoRevision.Text = AUXEtiqueta.Rows[0]["Tarea"].ToString().Trim();
                            AUXColCantCaja.Attributes.Add("class", "col-lg-6");
                            lblCantRet.InnerText = "Cantidad estándar:";
                            break;
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupImprimirEtiquetas();", true);
                }
            }
            catch (Exception ex)
            {
            }

        }

        public void SelectEtiqueta(object sender, EventArgs e)
        {

            HtmlButton button = (HtmlButton)sender;
            string TipoEtiqueta = button.ID;

            NUMProducto.Value = "";
            InputAlbaran.Text = "";
            InputCantidad.Text = "0";
            InputCantidadCaja.Text = "0";
            InputObservaciones.Text = "";
            InputOperario.Text = "0";
            NumEtiquetas.Value = "1";
            SelecImpresora.SelectedValue = "0";

            switch (TipoEtiqueta)
            {
                case "BTNImprPENDINSP":
                    if (DropDownPENDINSP.SelectedValue.ToString() != "---")
                    {
                        IdTipoEtiqueta.Value = "FPOC1101";
                        LblTipoRevision.Text = DropDownPENDINSP.SelectedValue.ToString();
                        AUXColCantCaja.Attributes.Add("class", "col-lg-6 invisible");
                        lblCantRet.InnerText = "Cantidad retenida:";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupImprimirEtiquetas();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "LogErrores();", true);
                    }
                    break;
                case "BTNImprPENDPROC":
                    if (DropDownPENDPROC.SelectedValue.ToString() != "---")
                    {
                        IdTipoEtiqueta.Value = "FPOC1110";
                        LblTipoRevision.Text = DropDownPENDPROC.SelectedValue.ToString();
                        AUXColCantCaja.Attributes.Add("class", "col-lg-6");
                        lblCantRet.InnerText = "Cantidad estándar:";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupImprimirEtiquetas();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "LogErrores();", true);
                    }
                    break;
            }
            
        }

        [WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
       
        public static bool ImprimirEtiquetasV2(string ETIQUETA, string TAREA,string REFERENCIA, string LOTE, string OBSERVACIONES, string CANTIDADORI, string CANTIDADREAL, string OPERARIO, int NUMETIQUETAS, int IMPRESORA)
        {

            string[] RecorteReferencia = REFERENCIA.Split(new char[] { '¬' });
            string REFER = RecorteReferencia[0].Trim();
            string DESCRIP = RecorteReferencia[1].Trim();
            // Printer IP Address and communication port
            //string ipAddress = "10.0.0.180"; //MATERIALES
            string ipAddress = ""; //THERMOBMS
            //NAVE 3 - IMPRESORA AMARILLA: ipAddress = "10.0.0.185";
            //NAVE 3 - IMPRESORA AZUL: ipAddress = "10.0.0.186";
            //NAVE 4 - IMPRESORA AMARILLA: ipAddress = "10.0.0.187";
            //NAVE 4 - IMPRESORA AZUL: ipAddress = "10.0.0.188";
            //GP12 - IMPRESORA AMARILLA:
            //GP12 - IMPRESORA AZUL:
            //PICKING - IMPRESORA AMARILLA:
            //PICKING - IMPRESORA AZUL:

            int port = 9100;

            string ZPLString = "";
            switch (ETIQUETA)
            {
                case "FPOC1101":
                    switch(IMPRESORA)
                    {
                        case 1:
                            ipAddress = "10.0.0.185";  //IPIMPRESORAAMARILLA RETENIDO
                            break;
                        case 2:
                            ipAddress = "10.0.0.187";  //IPIMPRESORAAMARILLA RETENIDO
                            break;
                        case 3:
                            ipAddress = "10.0.0.189";  //IPIMPRESORAAMARILLA RETENIDO
                            break;
                        case 4:
                            ipAddress = "172.16.0.165";  //IPIMPRESORAAMARILLA RETENIDO
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
                                "^PW815^ML2480" +
                                "^FX Lineas verticales" +
                                "^FO65,15^GB735,1,3^FS" +
                                "^FO65,820^GB735,1,3^FS" +
                                "^FX Lineas horizontales" +
                                "^FO65,15^GB1,805,3^FS" +
                                "^FO800,15^GB1,808,3^FS" +
                                "^FO695,15^GB100,805,105^FS" +
                                "^FX Descripciones de campo" +
                                "^FO540,35^A0R35,35^FDREFERENCIA:^FS" +
                                "^FO435,35^A0R35,35^FDALBARAN/ORDEN:^FS" +
                                "^FO375,35^A0R35,35^FDCANTIDAD:^FS" +
                                "^FO295,35^A0R35,35^FDOBSERVACIONES:^FS" +
                                "^FO95,25^A0R30,30^FDOperario:^FS" +
                                "^FO90,615^A0R30,30^FDTHERMOLYMPIC^FS" +
                                "^FO65,630^FR^FB1600,1,0,,0^A0R25,25^FDFPOC-11.01 Ed.2^FS" +
                                "^FX Campos variables" +
                                "^FO635,120^FR^FB650,2,0,C,0^A0R,70,70^FDPRODUCTO RETENIDO^FS" +
                                "^FO535,130^FB650,2,0,C,0^A0R,70,70^FD"+TAREA+"^FS" +
                                "^FO520,240^FB635,1,0,,0^A0R,55,55^FD"+REFER+"^FS" +
                                "^FO490,245^FB635,1,0,,0^A0R,30,30^FD"+DESCRIP+"^FS" +
                                "^FO435,310^FB635,1,0,,0^A0R,30,30^FD"+LOTE+"^FS" +
                                "^FO345,220^A0R65,65^FD"+CANTIDADORI.ToString()+"^FS" +
                                "^FO95,40^FB780,6,0,C,0^A0R,32,25^FD"+OBSERVACIONES+"^FS" +
                                "^FO65,25^A0R30,30^FD"+DateTime.Now.ToString("dd/MM/yyyy HH:mm")+"^FS" +
                                "^FO95,150^A0R30,30^FD"+OPERARIO.ToString()+"^FS" +
                                "^XZ";

                    break;
                case "FPOC1110":
                   
                    switch (IMPRESORA)
                    {
                        case 1:
                            ipAddress = "10.0.0.186";  //IPIMPRESORAMORADA PROCESAR
                            break;
                        case 2:
                            ipAddress = "10.0.0.188";  //IPIMPRESORAAMORADA PROCESAR
                            break;
                        case 3:
                            ipAddress = "10.0.0.190";  //IPIMPRESORAAMORADA PROCESAR
                            break;
                        case 4:
                            ipAddress = "172.16.0.164";  //IPIMPRESORAAMORADA PROCESAR
                            break;

                    }

                    ZPLString = "^XA " +
                                "^[L,2^FS^XZ " +
                                "^XA " +
                                "^MMT " +
                                "^LS0^LT0 " +
                                "^FWR^FPH " +
                                "^MTD " +
                                "^MNA " +
                                "^LH2,10^FS" +
                                "^PW815^ML2480 " +
                                "^FX Lineas verticales " +
                                "^FO65,15^GB735,1,3^FS" +
                                "^FO65,820^GB735,1,3^FS" +
                                "^FX Lineas horizontales " +
                                "^FO65,15^GB1,805,3^FS" +
                                "^FO800,15^GB1,808,3^FS" +
                                "^FO695,15^GB100,805,105^FS" +
                                "^FX Descripciones de campo" +
                                "^FO540,35^A0R35,35^FDREFERENCIA:^FS" +
                                "^FO435,35^A0R35,35^FDORDEN:^FS" +
                                "^FO375,125^A0R35,35^FDCANTIDAD^FS" +
                                "^FO345,125^A0R35,35^FDESTANDAR:^FS" +
                                "^FO375,475^A0R35,35^FDCANTIDAD^FS" +
                                "^FO345,475^A0R35,35^FDREAL:^FS" +
                                "^FO275,35^A0R35,35^FDOBSERVACIONES:^FS" +
                                "^FO95,25^A0R30,30^FDOperario:^FS" +
                                "^FO90,615^A0R30,30^FDTHERMOLYMPIC^FS" +
                                "^FO65,630^FR^FB1600,1,0,,0^A0R25,25^FDFPOC-11.10 Ed.2^FS" +
                                "^FX Campos variables" +
                                "^FO635,90^FR^FB700,2,0,C,0^A0R,70,70^FDPENDIENTE PROCESAR^FS" +
                                "^FO535,130^FB650,2,0,C,0^A0R,70,70^FD" + TAREA + "^FS" +
                                "^FO520,240^FB635,1,0,,0^A0R,55,55^FD" + REFER + "^FS" +
                                "^FO490,245^FB635,1,0,,0^A0R,30,30^FD" + DESCRIP + "^FS" +
                                "^FO435,180^FB635,1,0,,0^A0R,30,30^FD" + LOTE + "^FS" +
                                "^FO345,310^A0R65,65^FD" + CANTIDADORI.ToString() + "^FS" +
                                "^FO345,650^A0R65,65^FD" + CANTIDADREAL.ToString() + "^FS" +
                                "^FO75,40^FB780,6,0,C,0^A0R,32,25^FD" + OBSERVACIONES+"^FS" +
                                "^FO65,25^A0R30,30^FD" + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "^FS" +
                                "^FO95,150^A0R30,30^FD" + OPERARIO.ToString() + "^FS" +
                                "^XZ";

                    break;
            }

            string ZPLStringOUTPUT = "";
            for (int i = 0; i <= NUMETIQUETAS - 1; i++)
            {
                ZPLStringOUTPUT = ZPLStringOUTPUT + ZPLString;
            }
            
            try
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
                Conexion_SMARTH SHconexion = new Conexion_SMARTH();
                SHconexion.Insertar_Etiqueta_Auxiliar(ETIQUETA, TAREA, REFER, DESCRIP, LOTE, OBSERVACIONES, Convert.ToInt32(CANTIDADORI), Convert.ToInt32(CANTIDADREAL), Convert.ToInt32(OPERARIO), NUMETIQUETAS);
                
                return true;
                
            }
            catch (Exception ex)
            {
                return false;

                // Catch Exception
            }
        }

        
        /// AuxiliarEtiquetasLocalesMateriales
        public void LanzaEtisLocal()
        {
            Conexion_SMARTH SHConexion = new Conexion_SMARTH();
            DataTable Test = SHConexion.Devuelve_AUX_ETISFERNANDO();
            foreach (DataRow row in Test.Rows)
            {
                ImpresoraRecepcionProducto("RECEPCION_THERMO", Convert.ToInt32(row["Etiquetas_necesarias"].ToString()), row["Producto"].ToString(), row["Descripcion"].ToString(), row["Cantidad_Por_caja"].ToString(), row["Lote"].ToString(), row["Lote_de_Cliente"].ToString(), row["NumProveedor"].ToString(), row["NumPedido"].ToString());
            }

        }

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
                            ipAddress = "10.0.0.159";  //AÑADIR IP IMPRESORA   
                            break;
                        case "RECEPCION_OFICINA":
                            ipAddress = "10.0.0.158";  //AÑADIR IP IMPRESORA   
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

                if (impresora == "RECEPCION_THERMO")
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

                }
                // Crear y agregar más filas si es necesario


                /*

                //Conexion_SMARTH SHconexion = new Conexion_SMARTH();
               
                // return true;
                */


            }
            catch (Exception ex)
            {
                //return false;
            }

        }



    }

}