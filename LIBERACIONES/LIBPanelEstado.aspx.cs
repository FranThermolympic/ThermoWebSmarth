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

namespace ThermoWeb.LIBERACIONES
{
    public partial class PANELLIBERACIONES : System.Web.UI.Page
    {
        public string DefectoCargado = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ajustar_tamaño_campos();
            }

        }

        protected void Redireccionaraiz(object sender, EventArgs e)
        {
                Response.Redirect(url: "GP12.aspx");
        }

        public void CargarReferenciaOperario(Object sender, EventArgs e)
        {
            try
            {
                alertafuera.Visible = false;
                alertasinfecha.Visible = false;

                Conexion_LIBERACIONES conexion = new Conexion_LIBERACIONES();
                DataSet ds = conexion.cargar_operarios_bms(Convert.ToInt32(tbOperarioRevision.Value.ToString()));
                if (ds.Tables[0].Rows.Count == 0)
                {
                    tbOperarioCarga.Text = Convert.ToString(0);
                    tbOpeNombreCarga.Text = "NUEVO_OPERARIO";
                    tbEmpresaCarga.Text = "NO DEFINIDO";
                    tbCosteOPE.Text = "12.5";
                }
                else
                {
                    tbOperarioCarga.Text = ds.Tables[0].Rows[0]["NUMOPERARIO"].ToString();
                    tbOpeNombreCarga.Text = ds.Tables[0].Rows[0]["NOMBRE"].ToString();
                    tbEmpresaCarga.Text = ds.Tables[0].Rows[0]["EMPRESA"].ToString();
                    tbCosteOPE.Text = ds.Tables[0].Rows[0]["COSTE"].ToString();
                }

                ds = conexion.cargar_producto(tbReferencia.Value.ToString());
                tbReferenciaCarga.Text = ds.Tables[0].Rows[0]["PRODUCTO"].ToString();
                tbDescripcionCarga.Text = ds.Tables[0].Rows[0]["DESCRIPCION"].ToString();
                tbClienteCarga.Text = ds.Tables[0].Rows[0]["CLIENTE"].ToString();
                tbMolde.Text = ds.Tables[0].Rows[0]["MOLDE"].ToString();
                tbCostePIEZA.Text = ds.Tables[0].Rows[0]["COSTEPIEZA"].ToString();
                

                ds = conexion.devuelve_EstadosFiltros_Referencia(tbReferenciaCarga.Text);
                tbResponsable.Text = ds.Tables[0].Rows[0]["PAprobado"].ToString();
                string estadorevision = ds.Tables[0].Rows[0]["EstadoActual"].ToString();
                int razonderevision = Convert.ToInt32(ds.Tables[0].Rows[0]["IDEstactual"].ToString());
                string fecharevisionestado = ds.Tables[0].Rows[0]["Fechaprevsalida"].ToString();
                double fechacontrol = 0;
                if (fecharevisionestado != "")
                {
                    fechacontrol = Convert.ToDouble((DateTime.Now.Date - Convert.ToDateTime(fecharevisionestado)).TotalHours);  
                }
                if (razonderevision != 0 && razonderevision != 7 && fecharevisionestado=="")
                {
                    alertasinfecha.Visible = true;
                }
                if (razonderevision != 0 && razonderevision != 7 && fecharevisionestado != "" && fechacontrol > 0)
                {
                    alertafuera.Visible = true;
                }
                if (razonderevision == 0) // || razonderevision == 7
                {
                    alertasinrevision.Visible = true;
                }

                string observacionesresp = ds.Tables[0].Rows[0]["Observaciones"].ToString();
                ObservacionesRevision.Text = observacionesresp;

                DataSet razonesrevision = new DataSet();
                razonesrevision = conexion.devuelve_lista_razonesrevision();
                RazonRevision.Items.Clear();
                foreach (DataRow row in razonesrevision.Tables[0].Rows) { RazonRevision.Items.Add(row["Razon"].ToString()); }
                RazonRevision.ClearSelection();
                RazonRevision.SelectedValue = estadorevision;
                BtnInicioRevision.Disabled = false;

            }
            catch (Exception ex)
            { 
            }
        }

        public void IniciarRevision(Object sender, EventArgs e)
        {
            try
            {
                HoraInicio.Text = DateTime.Now.ToString("dd/MM/yyy HH:mm");
                TotalRevisado.Text = Convert.ToString(0);
                TotalOK.Text = Convert.ToString(0);
                TotalNOK.Text = Convert.ToString(0);
                TotalRetrabajado.Text = Convert.ToString(0);
                ContadorDefecto1.Text = Convert.ToString(0);
                ContadorDefecto2.Text = Convert.ToString(0);
                ContadorDefecto3.Text = Convert.ToString(0);
                ContadorDefecto4.Text = Convert.ToString(0);
                ContadorDefecto5.Text = Convert.ToString(0);
                ContadorDefecto6.Text = Convert.ToString(0);
                ContadorDefecto7.Text = Convert.ToString(0);
                ContadorDefecto8.Text = Convert.ToString(0);
                ContadorDefecto9.Text = Convert.ToString(0);
                ContadorDefecto10.Text = Convert.ToString(0);
                ContadorDefecto11.Text = Convert.ToString(0);
                ContadorDefecto12.Text = Convert.ToString(0);
                ContadorDefecto13.Text = Convert.ToString(0);
                ContadorDefecto14.Text = Convert.ToString(0);
                ContadorDefecto15.Text = Convert.ToString(0);
                ContadorDefecto16.Text = Convert.ToString(0);
                ContadorDefecto17.Text = Convert.ToString(0);
                ContadorDefecto18.Text = Convert.ToString(0);
                ContadorDefecto19.Text = Convert.ToString(0);
                ContadorDefecto20.Text = Convert.ToString(0);
                ContadorDefecto21.Text = Convert.ToString(0);
                ContadorDefecto22.Text = Convert.ToString(0);
                ContadorDefecto23.Text = Convert.ToString(0);
                ContadorDefecto24.Text = Convert.ToString(0);
                ContadorDefecto25.Text = Convert.ToString(0);
                ContadorDefecto26.Text = Convert.ToString(0);
                ContadorDefecto27.Text = Convert.ToString(0);
                ContadorDefecto28.Text = Convert.ToString(0);
                ContadorDefecto29.Text = Convert.ToString(0);
                ContadorDefecto30.Text = Convert.ToString(0);
                btnCargar.Disabled = true;
                BtnInicioRevision.Disabled = true;
                BtnTerminarRevision.Disabled = false;
                BtnResta.Disabled = false;
                BtnSuma.Disabled = false;
                UploadButton.Disabled = false;
                CantidadNOK.Disabled = false;
                TbLote.Enabled = true;
                TbOP1.Enabled = true;
                TbOP2.Enabled = true;
                TbOP3.Enabled = true;
                TbOP4.Enabled = true;
                TbObservaciones.Enabled = true;
                TbIncidencias.Enabled = true;
                TbLote.Enabled = true;
                RazonRevision.Enabled = true;

                Conexion_LIBERACIONES conexion = new Conexion_LIBERACIONES();
                DataSet ordenes = new DataSet();
                ordenes = conexion.devuelve_lista_ordenes(Convert.ToInt32(tbReferenciaCarga.Text));

                DataSet ds = new DataSet();
                ds = conexion.devuelve_EstadosFiltros_Referencia(tbReferenciaCarga.Text);
                string estadorevision = ds.Tables[0].Rows[0]["EstadoActual"].ToString();
                int razonderevision = Convert.ToInt32(ds.Tables[0].Rows[0]["IDEstactual"].ToString());
                string fecharevisionestado = ds.Tables[0].Rows[0]["Fechaprevsalida"].ToString();
                double fechacontrol = 0;
                if (fecharevisionestado != "")
                {
                    fechacontrol = Convert.ToDouble((DateTime.Now.Date - Convert.ToDateTime(fecharevisionestado)).TotalHours);
                }
                if (razonderevision != 0 && razonderevision != 7 && fecharevisionestado == "")
                {

                    //mandar_mail("Se ha iniciado la revisión de una pieza que no tiene una fecha de control asignada:<br>-" + tbReferenciaCarga.Text + " " + tbDescripcionCarga.Text + "<br><a href='http://FACTS4-SRV/oftecnica/GP12DetallesReferencia.aspx?REFERENCIA=" + tbReferenciaCarga.Text + "'>Accede a los detalles para revisar su estado parte a través de este link.</a>", "ALERTA DE GP12: Referencia en muro de calidad sin fecha de control asignada.  " + tbReferenciaCarga.Text + " " + tbDescripcionCarga.Text + "");
                    mandar_mail("Se ha iniciado la revisión de una pieza que no tiene una fecha de control asignada:<br><strong>Referencia:</strong> " + tbReferenciaCarga.Text + " " + tbDescripcionCarga.Text + "<br><strong>Responsable asignado:</strong> " + tbResponsable.Text + "", "ALERTA DE GP12: Referencia en muro de calidad sin fecha de control asignada.  " + tbReferenciaCarga.Text + " " + tbDescripcionCarga.Text + "");
                
                }
                if (razonderevision != 0 && razonderevision != 7 && fecharevisionestado != "" && fechacontrol > 0)
                {

                    mandar_mail("Se ha iniciado la revisión de una pieza cuya fecha de control está vencida:<br><strong>Referencia:</strong> " + tbReferenciaCarga.Text + " " + tbDescripcionCarga.Text + "<br><strong>Responsable asignado:</strong> " + tbResponsable.Text + "", "ALERTA DE GP12: Referencia en muro de calidad con fecha de control vencida.  " + tbReferenciaCarga.Text + " " + tbDescripcionCarga.Text + "");
                }
                if (razonderevision == 0) // || razonderevision == 7
                {
                    mandar_mail("Se ha iniciado la revisión de una pieza cuyo estado es 'Sin Revision':<br><strong>Referencia:</strong> " + tbReferenciaCarga.Text + " " + tbDescripcionCarga.Text + "<br><br>Por favor, en caso de ser necesario, inicia la contención y cambia el estado de la referencia en el muro de calidad.", "ALERTA DE GP12: Referencia sin revisión en el muro de calidad.  " + tbReferenciaCarga.Text + " " + tbDescripcionCarga.Text + "");
                
                }
                if (ordenes.Tables[0].Rows.Count == 1)
                {
                    TbLoteManual.Visible = true;
                    TbLote.Visible = false;
                }
                else
                {
                    TbLote.Items.Clear();
                    //if (ordenes.Tables[0].Rows.Count == 1) { TbLote.Items.Add(""); TbLote.Items.Add("00000"); }// Add(new ListItem("00000", "00000")); }
                    //else { foreach (DataRow row in ordenes.Tables[0].Rows) { TbLote.Items.Add(row["ORDEN"].ToString()); } } 
                    foreach (DataRow row in ordenes.Tables[0].Rows) { TbLote.Items.Add(row["ORDEN"].ToString()); }
                    TbLote.ClearSelection();
                    TbLote.SelectedValue = "";
                }


                /*DataSet razonesrevision = new DataSet();
                razonesrevision = conexion.devuelve_lista_razonesrevision();
                RazonRevision.Items.Clear();
                foreach (DataRow row in razonesrevision.Tables[0].Rows) { RazonRevision.Items.Add(row["Razon"].ToString()); }
                RazonRevision.ClearSelection();
                RazonRevision.SelectedValue = "";*/
                //Conectar cuando estén los productos en la base de datos
                //if (dsordenes.Tables[0].Rows[0]["EntradaF1"].ToString() != "")
                //{ TbLote.SelectedValue = conexion.devuelve_tipo_entradasAtemp(Convert.ToInt16(dsordenes.Tables[0].Rows[0]["EntradaF1"].ToString())); }
                //else { TbLote.SelectedValue = ""; }

            }
            catch (Exception ex)
            {
            }
        }

        public void TerminarRevision(Object sender, EventArgs e)
        {
            try
            {
                //PDTE. AVISO: ¿VAS A CERRAR LA ORDEN, SEGURO?
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "guardado_OK();", true);
                //onclick="return confirm('La orden de revisión se va a cerrar, ¿estás seguro?');"
                //RELLENO LOS CAMPOS FALTANTES
                HoraFinal.Text = DateTime.Now.ToString("dd/MM/yyy HH:mm");
                double tiemporevision = Convert.ToDouble(((DateTime.Parse(HoraFinal.Text) - DateTime.Parse(HoraInicio.Text)).TotalHours));
                    tbTiempoRevision.Text = Convert.ToString(Math.Round(tiemporevision*100)/100);
                    //tbTiempoRevision.Text = Convert.ToString(((DateTime.Parse(HoraFinal.Text) - DateTime.Parse(HoraInicio.Text)).TotalHours));
                CosteRevision.Text = Convert.ToString((double.Parse(tbCostePIEZA.Text) * int.Parse(TotalNOK.Text)) + (double.Parse(tbCosteOPE.Text) * double.Parse(tbTiempoRevision.Text)));
                double costescrap = Convert.ToDouble((double.Parse(tbCostePIEZA.Text) * int.Parse(TotalNOK.Text)));
                double costeoperario = double.Parse(tbCosteOPE.Text) * double.Parse(tbTiempoRevision.Text);


                if (Convert.ToString(TbOP1.Text) == "")
                    TbOP1.Text = Convert.ToString(0);
                if (Convert.ToString(TbOP2.Text) == "")
                    TbOP2.Text = Convert.ToString(0);
                if (Convert.ToString(TbOP3.Text) == "")
                    TbOP3.Text = Convert.ToString(0);
                if (Convert.ToString(TbOP4.Text) == "")
                    TbOP4.Text = Convert.ToString(0);

                int OP1 = 0;
                int OP2 = 0;
                int OP3 = 0;
                int OP4 = 0;
                if (Convert.ToInt32(TbOP1.Text) > 0)
                    OP1 = Convert.ToInt32(TbOP1.Text);
                if (Convert.ToInt32(TbOP2.Text) > 0)
                    OP2 = Convert.ToInt32(TbOP2.Text);
                if (Convert.ToInt32(TbOP3.Text) > 0)
                    OP3 = Convert.ToInt32(TbOP3.Text);
                if (Convert.ToInt32(TbOP4.Text) > 0)
                    OP4 = Convert.ToInt32(TbOP4.Text);

                string numlote = "";
                if (TbLoteManual.Visible == true)
                { numlote = TbLoteManual.Text; }
                else
                { numlote = TbLote.Text; }

                Conexion_LIBERACIONES conexion = new Conexion_LIBERACIONES();
                conexion.insertarRevision(HoraInicio.Text, HoraFinal.Text, Convert.ToDouble(tbTiempoRevision.Text), conexion.devuelve_IDproveedorrevision(tbEmpresaCarga.Text), Convert.ToInt32(tbOperarioCarga.Text), tbOpeNombreCarga.Text, Convert.ToInt32(tbReferenciaCarga.Text), tbDescripcionCarga.Text, Convert.ToInt32(tbMolde.Text), Convert.ToInt32(numlote), numlote, Convert.ToInt32(TotalRevisado.Text), Convert.ToInt32(TotalOK.Text), Convert.ToInt32(TotalRetrabajado.Text), Convert.ToInt32(TotalNOK.Text),
                                          Convert.ToInt32(ContadorDefecto1.Text), Convert.ToInt32(ContadorDefecto2.Text), Convert.ToInt32(ContadorDefecto3.Text), Convert.ToInt32(ContadorDefecto4.Text), Convert.ToInt32(ContadorDefecto5.Text), Convert.ToInt32(ContadorDefecto6.Text), Convert.ToInt32(ContadorDefecto7.Text), Convert.ToInt32(ContadorDefecto8.Text), Convert.ToInt32(ContadorDefecto9.Text), Convert.ToInt32(ContadorDefecto10.Text),
                                          Convert.ToInt32(ContadorDefecto11.Text), Convert.ToInt32(ContadorDefecto12.Text), Convert.ToInt32(ContadorDefecto13.Text), Convert.ToInt32(ContadorDefecto14.Text), Convert.ToInt32(ContadorDefecto15.Text), Convert.ToInt32(ContadorDefecto16.Text), Convert.ToInt32(ContadorDefecto17.Text), Convert.ToInt32(ContadorDefecto18.Text), Convert.ToInt32(ContadorDefecto19.Text), Convert.ToInt32(ContadorDefecto20.Text),
                                          Convert.ToInt32(ContadorDefecto21.Text), Convert.ToInt32(ContadorDefecto22.Text), Convert.ToInt32(ContadorDefecto23.Text), Convert.ToInt32(ContadorDefecto24.Text), Convert.ToInt32(ContadorDefecto25.Text), Convert.ToInt32(ContadorDefecto26.Text), Convert.ToInt32(ContadorDefecto27.Text), Convert.ToInt32(ContadorDefecto28.Text), Convert.ToInt32(ContadorDefecto29.Text), Convert.ToInt32(ContadorDefecto30.Text),
                                          TbIncidencias.Text, hyperlink1.ImageUrl, hyperlink2.ImageUrl, hyperlink3.ImageUrl, TbObservaciones.Text, OP1, OP2, OP3, OP4, conexion.devuelve_IDlista_razonesrevision(RazonRevision.Text), Convert.ToDouble(CosteRevision.Text), costescrap, costeoperario);
                btnCargar.Disabled = true;
                BtnInicioRevision.Disabled = true;
                BtnTerminarRevision.Disabled = true;
                BtnResta.Disabled = true;
                BtnSuma.Disabled = true;
                UploadButton.Disabled = true;
                CantidadNOK.Disabled = true;
                TbLote.Enabled = false;
                TbOP1.Enabled = false;
                TbOP2.Enabled = false;
                TbOP3.Enabled = false;
                TbOP4.Enabled = false;
                TbObservaciones.Enabled = false;
                TbIncidencias.Enabled = false;
                TbLote.Enabled = false;
                RazonRevision.Enabled = false;
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "guardado_NOK();", true);
            }
        }
        private void ajustar_tamaño_campos()
        {
            try
            {
                hyperlink1.NavigateUrl = "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg";
                hyperlink1.ImageUrl = "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg";
                hyperlink2.NavigateUrl = "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg";
                hyperlink2.ImageUrl = "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg";
                hyperlink3.NavigateUrl = "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg";
                hyperlink3.ImageUrl = "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg";
                //cabecera de detalles
                tituloReferenciaCarga.Width = 125;
                tituloDescripcionCarga.Width = 350;
                tituloClienteCarga.Width = 200;
                tbReferenciaCarga.Width = 125;
                tbDescripcionCarga.Width = 350;
                tbClienteCarga.Width = 200;
                tituloOperarioCarga.Width = 125;
                tituloNombreCarga.Width = 350;
                tituloEmpresaCarga.Width = 200;
                tbOperarioCarga.Width = 125;
                tbOpeNombreCarga.Width = 350;
                tbEmpresaCarga.Width = 200;
                
            }
            catch (Exception ex)
            {

            }
        }

        public void CargaDefecto(Object sender, EventArgs e)
        {
            try
            {
                //Conexion conexion = new Conexion();
                HtmlButton button = (HtmlButton)sender;
                //LinkButton button = (LinkButton)sender;
                string name = button.ID;
                switch (name)
                {
                    case "Buenas":
                        DefectoDeclarado.Text = "Buenas";
                        DefectoCarga.Text = "Buenas";
                        break;
                    case "Retrabajadas":
                       DefectoDeclarado.Text = "Retrabajadas";
                       DefectoCarga.Text = "Retrabajadas";
                       break;
                   case "Defecto01":
                       DefectoDeclarado.Text = "Falta llenado";
                       DefectoCarga.Text = "Defecto01";
                       //return DefectoCargado;
                       break;
                 case "Defecto02":
                       DefectoDeclarado.Text = "Ráfagas";
                       DefectoCarga.Text = "Defecto02";
                       break;
                   case "Defecto03":
                       DefectoDeclarado.Text = "Roturas";
                       DefectoCarga.Text = "Defecto03";
                       break;
                   case "Defecto04":
                       DefectoDeclarado.Text = "Montaje NOK";
                       DefectoCarga.Text = "Defecto04";
                       break;
                   case "Defecto05":
                       DefectoDeclarado.Text = "Deformaciones";
                       DefectoCarga.Text = "Defecto05";
                       break;
                   case "Defecto06":
                       DefectoDeclarado.Text = "Etiqueta NOK";
                       DefectoCarga.Text = "Defecto06";
                       break;
                   case "Defecto07":
                       DefectoDeclarado.Text = "Burbujas";
                       DefectoCarga.Text = "Defecto07";
                       break;
                   case "Defecto08":
                       DefectoDeclarado.Text = "Arrastre material";
                       DefectoCarga.Text = "Defecto08";
                       break;
                   case "Defecto09":
                       DefectoDeclarado.Text = "Marca expulsor";
                       DefectoCarga.Text = "Defecto09";
                       break;
                   case "Defecto10":
                       DefectoDeclarado.Text = "Quemazos";
                       DefectoCarga.Text = "Defecto10";
                       break;
                   case "Defecto11":
                       DefectoDeclarado.Text = "Brillos";
                       DefectoCarga.Text = "Defecto11";
                       break;
                   case "Defecto12":
                       DefectoDeclarado.Text = "M. contaminado";
                       DefectoCarga.Text = "Defecto12";
                       break;
                   case "Defecto13":
                       DefectoDeclarado.Text = "Rechupes";
                       DefectoCarga.Text = "Defecto13";
                       break;
                   case "Defecto14":
                       DefectoDeclarado.Text = "Color NOK";
                       DefectoCarga.Text = "Defecto14";
                       break;
                   case "Defecto15":
                       DefectoDeclarado.Text = "Manchas";
                       DefectoCarga.Text = "Defecto15";
                       break;
                   case "Defecto16":
                       DefectoDeclarado.Text = "Rebabas";
                       DefectoCarga.Text = "Defecto16";
                       break;
                   case "Defecto17":
                       DefectoDeclarado.Text = "Sólo plástico";
                       DefectoCarga.Text = "Defecto17";
                       break;
                   case "Defecto18":
                       DefectoDeclarado.Text = "Sólo goma";
                       DefectoCarga.Text = "Defecto18";
                       break;
                   case "Defecto19":
                       DefectoDeclarado.Text = "Otros";
                       DefectoCarga.Text = "Defecto19";
                       break;
                   case "Defecto20":
                       DefectoDeclarado.Text = "Electroválvula";
                       DefectoCarga.Text = "Defecto20";
                       break;
                   case "Defecto21":
                       DefectoDeclarado.Text = "Grapa: Posición";
                       DefectoCarga.Text = "Defecto21";
                       break;
                   case "Defecto22":
                       DefectoDeclarado.Text = "Grapa: Altura";
                       DefectoCarga.Text = "Defecto22";
                       break;
                   case "Defecto23":
                       DefectoDeclarado.Text = "Tubo: Deformado";
                       DefectoCarga.Text = "Defecto23";
                       break;
                   case "Defecto24":
                       DefectoDeclarado.Text = "Tubo: Mal puesto";
                       DefectoCarga.Text = "Defecto24";
                       break;
                   case "Defecto25":
                       DefectoDeclarado.Text = "Mal clipado";
                       DefectoCarga.Text = "Defecto25";
                       break;
                   case "Defecto26":
                       DefectoDeclarado.Text = "Suciedad";
                       DefectoCarga.Text = "Defecto26";
                       break;
                   case "Defecto27":
                       DefectoDeclarado.Text = "Punzonado NOK";
                       DefectoCarga.Text = "Defecto27";
                       break;
                   case "Defecto28":
                       DefectoDeclarado.Text = "Láser ilegible";
                       DefectoCarga.Text = "Defecto28";
                       break;
                }
                
            }
            catch (Exception ex)
            {
            }
        }

        /*public void SumarPieza(Object sender, EventArgs e)
        {
            try
            {
                //string SumaP = DefectoDeclarado.Text;
                string SumaP = DefectoCarga.Text;
                switch (SumaP)
                {
                    case "Buenas":
                        TotalOK.Text = Convert.ToString(int.Parse(TotalOK.Text) + int.Parse(CantidadNOK.Value));

                        break;
                    case "Retrabajadas":
                        TotalRetrabajado.Text = Convert.ToString(int.Parse(TotalRetrabajado.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Falta llenado":
                        ContadorDefecto1.Text = Convert.ToString(int.Parse(ContadorDefecto1.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Ráfagas":
                        ContadorDefecto2.Text = Convert.ToString(int.Parse(ContadorDefecto2.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Roturas":
                        ContadorDefecto3.Text = Convert.ToString(int.Parse(ContadorDefecto3.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Montaje NOK":
                        ContadorDefecto4.Text = Convert.ToString(int.Parse(ContadorDefecto4.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Deformaciones":
                        ContadorDefecto5.Text = Convert.ToString(int.Parse(ContadorDefecto5.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Etiqueta NOK":
                        ContadorDefecto6.Text = Convert.ToString(int.Parse(ContadorDefecto6.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Burbujas":
                        ContadorDefecto7.Text = Convert.ToString(int.Parse(ContadorDefecto7.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Chapa visible":
                        ContadorDefecto8.Text = Convert.ToString(int.Parse(ContadorDefecto8.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Marca expulsor":
                        ContadorDefecto9.Text = Convert.ToString(int.Parse(ContadorDefecto9.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Quemazos":
                        ContadorDefecto10.Text = Convert.ToString(int.Parse(ContadorDefecto10.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Brillos":
                        ContadorDefecto11.Text = Convert.ToString(int.Parse(ContadorDefecto11.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "M. contaminado":
                        ContadorDefecto12.Text = Convert.ToString(int.Parse(ContadorDefecto12.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Rechupes":
                        ContadorDefecto13.Text = Convert.ToString(int.Parse(ContadorDefecto13.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Color NOK":
                        ContadorDefecto14.Text = Convert.ToString(int.Parse(ContadorDefecto14.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Manchas":
                        ContadorDefecto15.Text = Convert.ToString(int.Parse(ContadorDefecto15.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Rebabas":
                        ContadorDefecto16.Text = Convert.ToString(int.Parse(ContadorDefecto16.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Sólo plástico":
                        ContadorDefecto17.Text = Convert.ToString(int.Parse(ContadorDefecto17.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Sólo goma":
                        ContadorDefecto18.Text = Convert.ToString(int.Parse(ContadorDefecto18.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Otros":
                        ContadorDefecto19.Text = Convert.ToString(int.Parse(ContadorDefecto19.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                }
                TotalRevisado.Text = Convert.ToString(int.Parse(TotalOK.Text) + int.Parse(TotalRetrabajado.Text) + int.Parse(TotalNOK.Text));
            }
            catch (Exception ex) { }
    
        }
        */
        public void SumarPieza(Object sender, EventArgs e)
        {
            try
            {
                //string SumaP = DefectoDeclarado.Text;
                string SumaP = DefectoCarga.Text;
                switch (SumaP)
                {
                    case "Buenas":
                        TotalOK.Text = Convert.ToString(int.Parse(TotalOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Retrabajadas":
                        TotalRetrabajado.Text = Convert.ToString(int.Parse(TotalRetrabajado.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto01":
                        ContadorDefecto1.Text = Convert.ToString(int.Parse(ContadorDefecto1.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto02":
                        ContadorDefecto2.Text = Convert.ToString(int.Parse(ContadorDefecto2.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto03":
                        ContadorDefecto3.Text = Convert.ToString(int.Parse(ContadorDefecto3.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto04":
                        ContadorDefecto4.Text = Convert.ToString(int.Parse(ContadorDefecto4.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto05":
                        ContadorDefecto5.Text = Convert.ToString(int.Parse(ContadorDefecto5.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto06":
                        ContadorDefecto6.Text = Convert.ToString(int.Parse(ContadorDefecto6.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto07":
                        ContadorDefecto7.Text = Convert.ToString(int.Parse(ContadorDefecto7.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto08":
                        ContadorDefecto8.Text = Convert.ToString(int.Parse(ContadorDefecto8.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto09":
                        ContadorDefecto9.Text = Convert.ToString(int.Parse(ContadorDefecto9.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto10":
                        ContadorDefecto10.Text = Convert.ToString(int.Parse(ContadorDefecto10.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto11":
                        ContadorDefecto11.Text = Convert.ToString(int.Parse(ContadorDefecto11.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto12":
                        ContadorDefecto12.Text = Convert.ToString(int.Parse(ContadorDefecto12.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto13":
                        ContadorDefecto13.Text = Convert.ToString(int.Parse(ContadorDefecto13.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto14":
                        ContadorDefecto14.Text = Convert.ToString(int.Parse(ContadorDefecto14.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto15":
                        ContadorDefecto15.Text = Convert.ToString(int.Parse(ContadorDefecto15.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto16":
                        ContadorDefecto16.Text = Convert.ToString(int.Parse(ContadorDefecto16.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto17":
                        ContadorDefecto17.Text = Convert.ToString(int.Parse(ContadorDefecto17.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto18":
                        ContadorDefecto18.Text = Convert.ToString(int.Parse(ContadorDefecto18.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto19":
                        ContadorDefecto19.Text = Convert.ToString(int.Parse(ContadorDefecto19.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto20":
                        ContadorDefecto20.Text = Convert.ToString(int.Parse(ContadorDefecto20.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto21":
                        ContadorDefecto21.Text = Convert.ToString(int.Parse(ContadorDefecto21.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto22":
                        ContadorDefecto22.Text = Convert.ToString(int.Parse(ContadorDefecto22.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto23":
                        ContadorDefecto23.Text = Convert.ToString(int.Parse(ContadorDefecto23.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto24":
                        ContadorDefecto24.Text = Convert.ToString(int.Parse(ContadorDefecto24.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto25":
                        ContadorDefecto25.Text = Convert.ToString(int.Parse(ContadorDefecto25.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto26":
                        ContadorDefecto26.Text = Convert.ToString(int.Parse(ContadorDefecto26.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto27":
                        ContadorDefecto27.Text = Convert.ToString(int.Parse(ContadorDefecto27.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto28":
                        ContadorDefecto19.Text = Convert.ToString(int.Parse(ContadorDefecto28.Text) + int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) + int.Parse(CantidadNOK.Value));
                        break;
                }
                TotalRevisado.Text = Convert.ToString(int.Parse(TotalOK.Text) + int.Parse(TotalRetrabajado.Text) + int.Parse(TotalNOK.Text));
            }
            catch (Exception ex) { }

        }
        /*
        public void RestarPieza(Object sender, EventArgs e)
        {
            try
            {
                string RestaP = DefectoDeclarado.Text;
                switch (RestaP)
                {
                    case "Buenas":
                        TotalOK.Text = Convert.ToString(int.Parse(TotalOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Retrabajadas":
                        TotalRetrabajado.Text = Convert.ToString(int.Parse(TotalRetrabajado.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Falta llenado":
                        ContadorDefecto1.Text = Convert.ToString(int.Parse(ContadorDefecto1.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Ráfagas":
                        ContadorDefecto2.Text = Convert.ToString(int.Parse(ContadorDefecto2.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Roturas":
                        ContadorDefecto3.Text = Convert.ToString(int.Parse(ContadorDefecto3.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Montaje NOK":
                        ContadorDefecto4.Text = Convert.ToString(int.Parse(ContadorDefecto4.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Deformaciones":
                        ContadorDefecto5.Text = Convert.ToString(int.Parse(ContadorDefecto5.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Etiqueta NOK":
                        ContadorDefecto6.Text = Convert.ToString(int.Parse(ContadorDefecto6.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Burbujas":
                        ContadorDefecto7.Text = Convert.ToString(int.Parse(ContadorDefecto7.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Chapa visible":
                        ContadorDefecto8.Text = Convert.ToString(int.Parse(ContadorDefecto8.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Marca expulsor":
                        ContadorDefecto9.Text = Convert.ToString(int.Parse(ContadorDefecto9.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Quemazos":
                        ContadorDefecto10.Text = Convert.ToString(int.Parse(ContadorDefecto10.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Brillos":
                        ContadorDefecto11.Text = Convert.ToString(int.Parse(ContadorDefecto11.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "M. contaminado":
                        ContadorDefecto12.Text = Convert.ToString(int.Parse(ContadorDefecto12.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Rechupes":
                        ContadorDefecto13.Text = Convert.ToString(int.Parse(ContadorDefecto13.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Color NOK":
                        ContadorDefecto14.Text = Convert.ToString(int.Parse(ContadorDefecto14.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Manchas":
                        ContadorDefecto15.Text = Convert.ToString(int.Parse(ContadorDefecto15.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Rebabas":
                        ContadorDefecto16.Text = Convert.ToString(int.Parse(ContadorDefecto16.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Sólo plástico":
                        ContadorDefecto17.Text = Convert.ToString(int.Parse(ContadorDefecto17.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Sólo goma":
                        ContadorDefecto18.Text = Convert.ToString(int.Parse(ContadorDefecto18.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Otros":
                        ContadorDefecto19.Text = Convert.ToString(int.Parse(ContadorDefecto19.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                }
                TotalRevisado.Text = Convert.ToString(int.Parse(TotalOK.Text) + int.Parse(TotalRetrabajado.Text) + int.Parse(TotalNOK.Text));
            }
            catch (Exception ex) { }

        }
        */
        public void RestarPieza(Object sender, EventArgs e)
        {
            try
            {
                //string SumaP = DefectoDeclarado.Text;
                string RestaP = DefectoCarga.Text;
                switch (RestaP)
                {
                    case "Buenas":
                        TotalOK.Text = Convert.ToString(int.Parse(TotalOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Retrabajadas":
                        TotalRetrabajado.Text = Convert.ToString(int.Parse(TotalRetrabajado.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto01":
                        ContadorDefecto1.Text = Convert.ToString(int.Parse(ContadorDefecto1.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto02":
                        ContadorDefecto2.Text = Convert.ToString(int.Parse(ContadorDefecto2.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto03":
                        ContadorDefecto3.Text = Convert.ToString(int.Parse(ContadorDefecto3.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto04":
                        ContadorDefecto4.Text = Convert.ToString(int.Parse(ContadorDefecto4.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto05":
                        ContadorDefecto5.Text = Convert.ToString(int.Parse(ContadorDefecto5.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto06":
                        ContadorDefecto6.Text = Convert.ToString(int.Parse(ContadorDefecto6.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto07":
                        ContadorDefecto7.Text = Convert.ToString(int.Parse(ContadorDefecto7.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto08":
                        ContadorDefecto8.Text = Convert.ToString(int.Parse(ContadorDefecto8.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto09":
                        ContadorDefecto9.Text = Convert.ToString(int.Parse(ContadorDefecto9.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto10":
                        ContadorDefecto10.Text = Convert.ToString(int.Parse(ContadorDefecto10.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto11":
                        ContadorDefecto11.Text = Convert.ToString(int.Parse(ContadorDefecto11.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto12":
                        ContadorDefecto12.Text = Convert.ToString(int.Parse(ContadorDefecto12.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto13":
                        ContadorDefecto13.Text = Convert.ToString(int.Parse(ContadorDefecto13.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto14":
                        ContadorDefecto14.Text = Convert.ToString(int.Parse(ContadorDefecto14.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto15":
                        ContadorDefecto15.Text = Convert.ToString(int.Parse(ContadorDefecto15.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto16":
                        ContadorDefecto16.Text = Convert.ToString(int.Parse(ContadorDefecto16.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto17":
                        ContadorDefecto17.Text = Convert.ToString(int.Parse(ContadorDefecto17.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto18":
                        ContadorDefecto18.Text = Convert.ToString(int.Parse(ContadorDefecto18.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto19":
                        ContadorDefecto19.Text = Convert.ToString(int.Parse(ContadorDefecto19.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto20":
                        ContadorDefecto20.Text = Convert.ToString(int.Parse(ContadorDefecto20.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto21":
                        ContadorDefecto21.Text = Convert.ToString(int.Parse(ContadorDefecto21.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto22":
                        ContadorDefecto22.Text = Convert.ToString(int.Parse(ContadorDefecto22.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto23":
                        ContadorDefecto23.Text = Convert.ToString(int.Parse(ContadorDefecto23.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto24":
                        ContadorDefecto24.Text = Convert.ToString(int.Parse(ContadorDefecto24.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto25":
                        ContadorDefecto25.Text = Convert.ToString(int.Parse(ContadorDefecto25.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto26":
                        ContadorDefecto26.Text = Convert.ToString(int.Parse(ContadorDefecto26.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto27":
                        ContadorDefecto27.Text = Convert.ToString(int.Parse(ContadorDefecto27.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                    case "Defecto28":
                        ContadorDefecto19.Text = Convert.ToString(int.Parse(ContadorDefecto28.Text) - int.Parse(CantidadNOK.Value));
                        TotalNOK.Text = Convert.ToString(int.Parse(TotalNOK.Text) - int.Parse(CantidadNOK.Value));
                        break;
                }
                TotalRevisado.Text = Convert.ToString(int.Parse(TotalOK.Text) + int.Parse(TotalRetrabajado.Text) + int.Parse(TotalNOK.Text));
            }
            catch (Exception ex) { }

        }
        public void insertar_foto(Object sender, EventArgs e)
        {
            try
            {
                if (FileUpload1.HasFile)
                    SaveFile(FileUpload1.PostedFile, 1);
                if (FileUpload2.HasFile)
                    SaveFile(FileUpload2.PostedFile, 2);
                if (FileUpload3.HasFile)
                    SaveFile(FileUpload3.PostedFile, 3);
            }
            catch (Exception ex)
            {

            }
        }

        private void SaveFile(HttpPostedFile file, int num_img)
        {
            try
            {
                // Specify the path to save the uploaded file to.
                string savePath = "C:\\inetpub_thermoweb\\Imagenes\\GP12\\";


                // Get the name of the file to upload.
                //string fileName = FileUpload1.FileName;
                string fileName = "";
                switch (num_img)
                {
                    case 1:
                        fileName = FileUpload1.FileName;
                        break;
                    case 2:
                        fileName = FileUpload2.FileName;
                        break;
                    case 3:
                        fileName = FileUpload3.FileName;
                        break;
                    default: break;
                }


                // Create the path and file name to check for duplicates.
                string pathToCheck = savePath + fileName;

                // Create a temporary file name to use for checking duplicates.
                string tempfileName = "";

                // Check to see if a file already exists with the
                // same name as the file to upload.        
                if (System.IO.File.Exists(pathToCheck))
                {
                    int counter = 2;
                    while (System.IO.File.Exists(pathToCheck))
                    {
                        // if a file with this name already exists,
                        // prefix the filename with a number.
                        tempfileName = counter.ToString() + fileName;
                        pathToCheck = savePath + tempfileName;
                        counter++;
                    }

                    fileName = tempfileName;

                    // Notify the user that the file name was changed.
                    UploadStatusLabel.Text = "Imágenes subidas correctamente.";
                    
                }
                else
                {
                    // Notify the user that the file was saved successfully.
                    UploadStatusLabel.Text = "Imágenes cargadas correctamente.";
                   
                }

                // Append the name of the file to upload to the path.
                savePath += fileName;

                // Call the SaveAs method to save the uploaded
                // file to the specified directory.
                switch (num_img)
                {
                    case 1:
                        FileUpload1.SaveAs(savePath);
                        hyperlink1.ImageUrl = "http://facts4-srv/thermogestion/Imagenes/GP12/" + fileName;
                        hyperlink1.NavigateUrl = "http://facts4-srv/thermogestion/Imagenes/GP12/" + fileName;

                        break;
                    case 2:
                        FileUpload2.SaveAs(savePath);
                        hyperlink2.ImageUrl = "http://facts4-srv/thermogestion/Imagenes/GP12/" + fileName;
                        hyperlink2.NavigateUrl = "http://facts4-srv/thermogestion/Imagenes/GP12/" + fileName;
                        break;
                    case 3:
                        FileUpload3.SaveAs(savePath);
                        hyperlink3.ImageUrl = "http://facts4-srv/thermogestion/Imagenes/GP12/" + fileName;
                        hyperlink3.NavigateUrl = "http://facts4-srv/thermogestion/Imagenes/GP12/" + fileName;
                        break;
                    default: break;
                }
            }
            catch (Exception ex)
            {
            }

        }

        public void mandar_mail(string mensaje, string subject)
        {
            //Create MailMessage
            MailMessage email = new MailMessage();
            Conexion_LIBERACIONES conexion = new Conexion_LIBERACIONES();
            DataSet ds_correos = conexion.leer_correos();
            foreach (DataRow row in ds_correos.Tables[0].Rows)
            {
                if (row["CorreoGP12"].ToString() != "")
                {
                    email.To.Add(new MailAddress(row["CorreoGP12"].ToString()));
                }
            }
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
            catch (Exception ex)
            {
            }
        }

    }

}