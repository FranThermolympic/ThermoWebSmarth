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
using System.Net.Mail;
using System.Net;
using System.Data.SqlClient;
using System.Text;
using System.IO;

namespace ThermoWeb.PRODUCCION
{
    public partial class MontajesExternos : System.Web.UI.Page
    {
        private static DataSet ds_RefXMolde = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Conexion_SMARTH SHconexion = new Conexion_SMARTH();
                FormatoCeldas();
                LimpiarCampos();

                DataTable DT_ListaProductoSeparador = SHconexion.Devuelve_Productos_NAV_SEPARADOR();
                {
                    for (int i = 0; i <= DT_ListaProductoSeparador.Rows.Count - 1; i++)
                    {
                        DatalistReferencias.InnerHtml = DatalistReferencias.InnerHtml + System.Environment.NewLine +
                            String.Format("<option value='{0}'>", DT_ListaProductoSeparador.Rows[i][0]);
                    }
                }
               
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupDOCAUX();", true);
                //GestionBotones(0);
                //CargarFiltros();
                //tbObservacionesCarga.Enabled = false;
                //tbObservacionesCargaBMS.Enabled = false;
                if (Request.QueryString["REFERENCIA"] != null)
                {
                   
                    //tbReferencia.Value = Request.QueryString["REFERENCIA"];
                    //CargarDatos(null, e);
                }

            }

        }
        //Auxiliares
        private void FormatoCeldas()
        {
            tbReferencia.CssClass = "form-control bg-white border border-secondary";
            tbOperario.CssClass = "form-control bg-white border border-secondary";
            tbHoraInicio.CssClass = "form-control bg-white border border-secondary";
            tbPiezasFabricadas.CssClass = "form-control bg-white border border-secondary";
            tbLote.CssClass = "form-control bg-white border border-secondary";
            tbNotas.CssClass = "form-control bg-white border border-secondary";
            tbPiezasMalas.CssClass = "form-control bg-white border border-secondary";
        }

        public void LimpiarCampos()
        {
            AUXOperarioEmpresa.Value = "";   
            AUXOperarioINT.Value = "";
            AUXOperario.Value = "";
            CosteOperario.Value = "";
            AUXReferencia.Value = "";
            AuxReferenciaNombre.Value = "";
            tbHoraInicio.Text = "";
            tbLote.Text = "";
            tbPiezasFabricadas.Text = "";
            tbNotas.Text = "";
            tbPiezasMalas.Text = "";
        }
        
       //Cargas secundarias
        public bool CargarOperario()
        {
            Conexion_SMARTH conexion = new Conexion_SMARTH();
            try
            {
                DataTable ope = conexion.Devuelve_operarios_bms(Convert.ToInt32(tbOperarioCarga.Value));
                if (ope.Rows.Count == 0)
                {                   
                    CosteOperario.Value = "12.75";
                    tbOperario.Text = "NUEVO_OPERARIO";

                    AUXOperarioINT.Value = tbOperarioCarga.Value;
                    AUXOperario.Value = "NUEVO_OPERARIO";
                    AUXOperarioEmpresa.Value = "NUEVO_OPERARIO";
                }
                else
                {
                    tbOperario.Text = ope.Rows[0]["NUMOPERARIO"].ToString() + "-" + ope.Rows[0]["NOMBRE"].ToString() + " (" + ope.Rows[0]["EMPRESA"].ToString() + ")";
                    
                    AUXOperarioINT.Value = tbOperarioCarga.Value;
                    AUXOperario.Value = ope.Rows[0]["NOMBRE"].ToString();
                    AUXOperarioEmpresa.Value = ope.Rows[0]["EMPRESA"].ToString();

                    // tbOperarioCarga.Text = ope.Rows[0]["NUMOPERARIO"].ToString();
                    // tbOpeNombreCarga.Text = ope.Rows[0]["NOMBRE"].ToString();
                    // tbEmpresaCarga.Text = ope.Rows[0]["EMPRESA"].ToString();
                    if (ope.Rows[0]["COSTE"].ToString() != "")
                    {
                        CosteOperario.Value = ope.Rows[0]["COSTE"].ToString();
                    }
                    else
                    {
                        CosteOperario.Value = "12.75";
                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool CargarFramesMaquina(string ref1)
        {
                Conexion_SMARTH conexion = new Conexion_SMARTH();
                if (ref1 != "")
                {
                DataTable PRODNAV = conexion.Devuelve_Productos_NAV(" AND No_ = '" + ref1 + "'");
                    if (PRODNAV.Rows.Count > 0)
                    {
                    tbReferencia.Text = PRODNAV.Rows[0]["PRODUCTO"].ToString() + " " + PRODNAV.Rows[0]["DESCRIPCION"].ToString();
                    AUXReferencia.Value = PRODNAV.Rows[0]["PRODUCTO"].ToString();
                    AuxReferenciaNombre.Value = PRODNAV.Rows[0]["DESCRIPCION"].ToString();
                    DataSet ds_DocumentosPlanta = conexion.Devuelve_dataset_filtroreferenciasSMARTH(ref1);
                    ds_DocumentosPlanta.Tables[0].DefaultView.RowFilter = "REF = '" + ref1 + "'";
                    if (ds_DocumentosPlanta.Tables[0].Rows.Count > 0)
                    {
                    
                        DataTable DTDOCUMENTAL = (ds_DocumentosPlanta.Tables[0].DefaultView).ToTable();
                        

                        if (DTDOCUMENTAL.Rows[0]["PautaControl"].ToString() != "")
                        {
                            PautaControl_1.Attributes.Add("src", DTDOCUMENTAL.Rows[0]["PautaControl"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                        }
                        else
                        {
                            PautaControl_1.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                        }

                        if (DTDOCUMENTAL.Rows[0]["Defoteca"].ToString() != "")
                        {
                            DEFECTOS_1.Attributes.Add("src", DTDOCUMENTAL.Rows[0]["Defoteca"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                        }
                        else
                        {
                            DEFECTOS_1.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                        }

                        if (DTDOCUMENTAL.Rows[0]["OperacionEstandar"].ToString() != "")
                        {
                            GP12_1.Attributes.Add("src", DTDOCUMENTAL.Rows[0]["OperacionEstandar"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                        }
                        else
                        {
                            GP12_1.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                        }

                        if (DTDOCUMENTAL.Rows[0]["Embalaje"].ToString() != "")
                        {
                            PAUTAEMBALAJE_1.Attributes.Add("src", DTDOCUMENTAL.Rows[0]["Embalaje"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                        }
                        else
                        {
                            PAUTAEMBALAJE_1.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                        }

                        if (DTDOCUMENTAL.Rows[0]["PautaRecepcion2"].ToString() != "")
                        {
                            PAUTAEMBALAJEALTERNATIVO_1.Attributes.Add("src", DTDOCUMENTAL.Rows[0]["PautaRecepcion2"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                        }
                        else
                        {
                            PAUTAEMBALAJEALTERNATIVO_1.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                        }

                        if (DTDOCUMENTAL.Rows[0]["Logotipo"].ToString() != "")
                        {
                            IMGCliente.ImageUrl = DTDOCUMENTAL.Rows[0]["Logotipo"].ToString();
                        }
                        else
                        {
                            IMGCliente.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                        }

                        if (DTDOCUMENTAL.Rows[0]["ImagenPieza"].ToString() != "")
                        {
                            IMGPieza.ImageUrl = DTDOCUMENTAL.Rows[0]["ImagenPieza"].ToString();
                        }
                        else
                        {
                            IMGPieza.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                        }
                    }                   
                    return true;
                    }
                    else
                    {
                        return false;
                    }
                  
                    
                    //COMPRUEBO ESTADOS DE GP12 - FALTA INCLUIR RESTO DE REFERENCIAS

                }
                else
                {
                    return false;
                }
        }

        public void CargarCarruselGP12(string ref1)
        {
            try
            {
                DataSet imagenes = new DataSet();              
                Conexion_SMARTH conexion = new Conexion_SMARTH();

                if (ref1 != "")
                {
                    imagenes = conexion.DatasetDetectadosGP12(ref1);
                    foreach (DataRow row in imagenes.Tables[0].Rows)
                    {
                        HtmlGenericControl divItem = new HtmlGenericControl("div");
                        divItem.Attributes["class"] = "carousel-item";
                        //divItem.ID = "prueba";
                        Image img = new Image();
                        img.ImageUrl = row["ImagenDefecto1"].ToString();
                        img.Visible = true;
                        img.Attributes["class"] = "d-block w-100";
                        divItem.Controls.Add(img);
                        //img.Width = new Unit("100%");
                        img.Height = new Unit("600px");
                        ACTIVOS.Controls.Add(divItem);
                    }
                }
              

            }
            catch (Exception)
            { }
        }

        //Montaje
        public void IniciarMontaje(object sender, EventArgs e)
        {
            string[] RecorteReferencia = tbReferenciaCarga.Value.Split(new char[] { '¬' });

            bool OPER = CargarOperario();
            bool MAQ = CargarFramesMaquina(RecorteReferencia[0].Trim().ToString());
            if (OPER == true && MAQ == true)
            {
                CargarCarruselGP12(AUXReferencia.Value);
                tbHoraInicio.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                tbLote.Enabled = true;
                tbPiezasFabricadas.Enabled = true;
                tbNotas.Enabled = true;
                tbPiezasMalas.Enabled = true;
                BtnEnviar.Disabled = false;
            }
            else
            {
                LimpiarCampos();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupDOCAUX();", true);
            }

          

        }
        /*
        public void TerminarMontaje(object sender, EventArgs e)
        {
            try
            {
                Conexion_SMARTH conexion = new Conexion_SMARTH();
                //CALCULOS DE TIEMPO Y COSTE
               
                if (tbLote.Text == "" || tbPiezasFabricadas.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "Guardado_NOK();", true);
                }
                else
                {
                double tiemporevision = Math.Round(Convert.ToDouble((DateTime.Now - DateTime.Parse(tbHoraInicio.Text)).TotalHours), 2);
                double costeoperario = double.Parse(CosteOperario.Value) * tiemporevision;
                //GENERA ENTRADA
                conexion.Insertar_Montaje(tbHoraInicio.Text, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), tiemporevision, conexion.devuelve_IDproveedorrevision(AUXOperarioEmpresa.Value), Convert.ToInt32(AUXOperarioINT.Value), AUXOperario.Value, Convert.ToInt32(AUXReferencia.Value), AuxReferenciaNombre.Value, Convert.ToInt32(tbLote.Text), tbLote.Text + "/000", Convert.ToInt32(tbPiezasFabricadas.Text), Math.Round(tiemporevision * costeoperario,3), tbNotas.Text);
                //tbReferencia.Text;
                //tbLote.Text;

                ////GENERA FICHERO Y EXPORTAR FICHERO PARA NAV
                StringBuilder exportar = new StringBuilder();
                string CadenaTexto = ";" + AUXReferencia.Value + ";" + tbLote.Text + ";" + tbLote.Text + "/000;" + tbPiezasFabricadas.Text + ";;" + tbHoraInicio.Text + ";;0";
                string folderPath = @"\\thermonav\ExportNAV\";
                File.WriteAllText(folderPath + "_BMSEXPORT_" + DateTime.Now.ToString("yy-MM-dd_HHmmss") + "MONT.csv", CadenaTexto.ToString());
                
                //RECARGA LA PÁGINA
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "Guardado_OK();", true);
                Response.Redirect(url: "MontajesExternos.aspx");
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "Guardado_NOK();", true);
            }

        }
        */


        [WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]

        public static bool TerminarMontajeAJAX(string LOTE, string PIEZASFABRICADAS, string HORAINICIO, string COSTEOPERARIO, string AUXOPERARIOEMPRESA, string AUXOPERARIOINT, string AUXOPERARIO,  string NOTAS, string CANTIDADMONTADA, string REFERENCIA, string REFERENCIATEXT, string CANTIDADMALA)
        {

            try
            {
                Conexion_SMARTH conexion = new Conexion_SMARTH();
                //CALCULOS DE TIEMPO Y COSTE

                if (LOTE == "" || PIEZASFABRICADAS == "")
                {
                    return false;
                }
                else
                {
                    double tiemporevision = Math.Round(Convert.ToDouble((DateTime.Now - DateTime.Parse(HORAINICIO)).TotalHours), 2);
                    double costeoperario = double.Parse(COSTEOPERARIO) * tiemporevision;
                    //GENERA ENTRADA
                    //PREPARAR SI EL CAMPO PIEZAS MALAS ES VACIO 0 SI NO CAMPO 

                    conexion.Insertar_Montaje(HORAINICIO, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), tiemporevision, conexion.Devuelve_IDproveedorrevision(AUXOPERARIOEMPRESA), Convert.ToInt32(AUXOPERARIOINT), AUXOPERARIO, Convert.ToInt32(REFERENCIA), REFERENCIATEXT, Convert.ToInt32(LOTE), LOTE + "/000", Convert.ToInt32(CANTIDADMONTADA), Math.Round(tiemporevision * costeoperario, 3), NOTAS, Convert.ToInt32(CANTIDADMALA));
                   
                    ////GENERA FICHERO Y EXPORTAR FICHERO PARA NAV
                    StringBuilder exportar = new StringBuilder();
                    string CadenaTexto = ";" + REFERENCIA + ";" + LOTE + ";" + LOTE + "/000;" + PIEZASFABRICADAS + ";"+ CANTIDADMALA + ";" + HORAINICIO + ";;0";
                    string folderPath = @"\\thermonav\ExportNAV\";
                    File.WriteAllText(folderPath + "BMSEXPORT_" + DateTime.Now.ToString("yy-MM-dd_HHmmss") + "MONT.csv", CadenaTexto.ToString());

                    //RECARGA LA PÁGINA
                    return true;
                    
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

    }

}
