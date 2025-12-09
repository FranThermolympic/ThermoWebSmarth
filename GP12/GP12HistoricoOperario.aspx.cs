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

namespace ThermoWeb.GP12
{
    public partial class GP12HistoricoOperario : System.Web.UI.Page
    {

        private static DataSet ds_Referencias = new DataSet();
        private static DataSet ds_listaoperarios = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["OPERARIO"] != null)
                {   
                    Conexion_GP12 conexion = new Conexion_GP12();
                    //DATASET
                    ds_Referencias = conexion.devuelve_detalle_revisiones_referencia_operario(Convert.ToInt32(Request.QueryString["OPERARIO"]));
                    ds_listaoperarios = conexion.cargar_operarios_bms(Convert.ToInt32(Request.QueryString["OPERARIO"]));
                    tbNumOperario.Text = ds_listaoperarios.Tables[0].Rows[0]["NUMOPERARIO"].ToString();
                    tbNombreOperario.Text = ds_listaoperarios.Tables[0].Rows[0]["NOMBRE"].ToString();
                    rellenar_grid();
                }
            }
        }

        private void rellenar_grid()
        {
            try
            {
                //rellenar cabecera
                dgv_AreaRechazo.DataSource = ds_Referencias;

                double totalHoras = 0;
                foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                {
                    totalHoras += Convert.ToDouble(dr["DoubleHoras"]);
                }
                totalHoras = Math.Truncate(totalHoras * 100) / 100;
                dgv_AreaRechazo.Columns[3].FooterText = totalHoras.ToString();

                if (ds_Referencias.Tables[0].Rows[0]["FakeMode"].ToString() != "1")
                {
                    //rellenar grid
          
                    //sumo defectos
                    int Defecto1 = 0;
                    foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                    {
                        Defecto1 += Convert.ToInt32(dr["Def1"]);
                    }
                    dgv_AreaRechazo.Columns[14].FooterText = Defecto1.ToString();

                    int Defecto2 = 0;
                    foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                    {
                        Defecto2 += Convert.ToInt32(dr["Def2"]);
                    }
                    dgv_AreaRechazo.Columns[15].FooterText = Defecto2.ToString();

                    int Defecto3 = 0;
                    foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                    {
                        Defecto3 += Convert.ToInt32(dr["Def3"]);
                    }
                    dgv_AreaRechazo.Columns[16].FooterText = Defecto3.ToString();

                    int Defecto4 = 0;
                    foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                    {
                        Defecto4 += Convert.ToInt32(dr["Def4"]);
                    }
                    dgv_AreaRechazo.Columns[17].FooterText = Defecto4.ToString();

                    int Defecto5 = 0;
                    foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                    {
                        Defecto5 += Convert.ToInt32(dr["Def5"]);
                    }
                    dgv_AreaRechazo.Columns[18].FooterText = Defecto5.ToString();

                    int Defecto6 = 0;
                    foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                    {
                        Defecto6 += Convert.ToInt32(dr["Def6"]);
                    }
                    dgv_AreaRechazo.Columns[19].FooterText = Defecto6.ToString();

                    int Defecto7 = 0;
                    foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                    {
                        Defecto7 += Convert.ToInt32(dr["Def7"]);
                    }
                    dgv_AreaRechazo.Columns[20].FooterText = Defecto7.ToString();

                    int Defecto8 = 0;
                    foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                    {
                        Defecto8 += Convert.ToInt32(dr["Def8"]);
                    }
                    dgv_AreaRechazo.Columns[21].FooterText = Defecto8.ToString();

                    int Defecto9 = 0;
                    foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                    {
                        Defecto9 += Convert.ToInt32(dr["Def9"]);
                    }
                    dgv_AreaRechazo.Columns[22].FooterText = Defecto9.ToString();

                    int Defecto10 = 0;
                    foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                    {
                        Defecto10 += Convert.ToInt32(dr["Def10"]);
                    }
                    dgv_AreaRechazo.Columns[23].FooterText = Defecto10.ToString();

                    int Defecto11 = 0;
                    foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                    {
                        Defecto11 += Convert.ToInt32(dr["Def11"]);
                    }
                    dgv_AreaRechazo.Columns[24].FooterText = Defecto11.ToString();

                    int Defecto12 = 0;
                    foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                    {
                        Defecto12 += Convert.ToInt32(dr["Def12"]);
                    }
                    dgv_AreaRechazo.Columns[25].FooterText = Defecto12.ToString();

                    int Defecto13 = 0;
                    foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                    {
                        Defecto13 += Convert.ToInt32(dr["Def13"]);
                    }
                    dgv_AreaRechazo.Columns[26].FooterText = Defecto13.ToString();

                    int Defecto14 = 0;
                    foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                    {
                        Defecto14 += Convert.ToInt32(dr["Def14"]);
                    }
                    dgv_AreaRechazo.Columns[27].FooterText = Defecto14.ToString();

                    int Defecto15 = 0;
                    foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                    {
                        Defecto15 += Convert.ToInt32(dr["Def15"]);
                    }
                    dgv_AreaRechazo.Columns[28].FooterText = Defecto15.ToString();

                    int Defecto16 = 0;
                    foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                    {
                        Defecto16 += Convert.ToInt32(dr["Def16"]);
                    }
                    dgv_AreaRechazo.Columns[29].FooterText = Defecto16.ToString();

                    int Defecto17 = 0;
                    foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                    {
                        Defecto17 += Convert.ToInt32(dr["Def17"]);
                    }
                    dgv_AreaRechazo.Columns[30].FooterText = Defecto17.ToString();

                    int Defecto18 = 0;
                    foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                    {
                        Defecto18 += Convert.ToInt32(dr["Def18"]);
                    }
                    dgv_AreaRechazo.Columns[31].FooterText = Defecto18.ToString();

                    int Defecto19 = 0;
                    foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                    {
                        Defecto19 += Convert.ToInt32(dr["Def19"]);
                    }
                    dgv_AreaRechazo.Columns[32].FooterText = Defecto19.ToString();

                    int Defecto20 = 0;
                    foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                    {
                        Defecto20 += Convert.ToInt32(dr["Def20"]);
                    }
                    dgv_AreaRechazo.Columns[33].FooterText = Defecto20.ToString();

                    int Defecto21 = 0;
                    foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                    {
                        Defecto21 += Convert.ToInt32(dr["Def21"]);
                    }
                    dgv_AreaRechazo.Columns[34].FooterText = Defecto21.ToString();

                    int Defecto22 = 0;
                    foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                    {
                        Defecto22 += Convert.ToInt32(dr["Def22"]);
                    }
                    dgv_AreaRechazo.Columns[35].FooterText = Defecto22.ToString();

                    int Defecto23 = 0;
                    foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                    {
                        Defecto23 += Convert.ToInt32(dr["Def23"]);
                    }
                    dgv_AreaRechazo.Columns[36].FooterText = Defecto23.ToString();

                    int Defecto24 = 0;
                    foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                    {
                        Defecto24 += Convert.ToInt32(dr["Def24"]);
                    }
                    dgv_AreaRechazo.Columns[37].FooterText = Defecto24.ToString();

                    int Defecto25 = 0;
                    foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                    {
                        Defecto25 += Convert.ToInt32(dr["Def25"]);
                    }
                    dgv_AreaRechazo.Columns[38].FooterText = Defecto25.ToString();

                    int Defecto26 = 0;
                    foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                    {
                        Defecto26 += Convert.ToInt32(dr["Def26"]);
                    }
                    dgv_AreaRechazo.Columns[39].FooterText = Defecto26.ToString();

                    int Defecto27 = 0;
                    foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                    {
                        Defecto27 += Convert.ToInt32(dr["Def27"]);
                    }
                    dgv_AreaRechazo.Columns[40].FooterText = Defecto27.ToString();

                    int Defecto28 = 0;
                    foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                    {
                        Defecto28 += Convert.ToInt32(dr["Def28"]);
                    }
                    dgv_AreaRechazo.Columns[41].FooterText = Defecto28.ToString();

                    int Defecto29 = 0;
                    foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                    {
                        Defecto29 += Convert.ToInt32(dr["Def29"]);
                    }
                    dgv_AreaRechazo.Columns[42].FooterText = Defecto29.ToString();

                    int Defecto30 = 0;
                    foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                    {
                        Defecto30 += Convert.ToInt32(dr["Def30"]);
                    }
                    dgv_AreaRechazo.Columns[43].FooterText = Defecto30.ToString();
                }
                dgv_AreaRechazo.DataBind();
            }
            catch (Exception ex)
            {
                dgv_AreaRechazo.DataSource = null;
                dgv_AreaRechazo.DataBind();
            }

            /*try
            {
                //rellenar cabecera
                dgv_AreaRechazo.DataSource = ds_Referencias;


                //rellenar grid
                double totalBuenas = 0;
                foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                {
                    totalBuenas += Convert.ToDouble(dr["PiezasOK"]);
                }
                dgv_AreaRechazo.Columns[5].FooterText = totalBuenas.ToString();

                double totalRetrabajadas = 0;
                foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                {
                    totalRetrabajadas += Convert.ToDouble(dr["Retrabajadas"]);
                }
                dgv_AreaRechazo.Columns[6].FooterText = totalRetrabajadas.ToString();


                double totalMalas = 0;
                foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                {
                    totalMalas += Convert.ToDouble(dr["PiezasNOK"]);
                }
                dgv_AreaRechazo.Columns[7].FooterText = totalMalas.ToString();


                double totalHoras = 0;
                foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                {
                    totalHoras += Convert.ToDouble(dr["DoubleHoras"]);
                }
                totalHoras = Math.Truncate(totalHoras * 100) / 100;
                dgv_AreaRechazo.Columns[3].FooterText = totalHoras.ToString();


                double totalchatarra = 0;
                foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                {
                    totalchatarra += Convert.ToDouble(dr["CosteScrapRevision"]);
                }
                totalchatarra = Math.Truncate(totalchatarra * 100) / 100;
                dgv_AreaRechazo.Columns[9].FooterText = totalchatarra.ToString("C");
        

                double totalpersrevision = 0;
                foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                {
                    totalpersrevision += Convert.ToDouble(dr["CostePiezaRevision"]);
                }
                totalpersrevision = Math.Truncate(totalpersrevision * 100) / 100;
                dgv_AreaRechazo.Columns[10].FooterText = totalpersrevision.ToString("C");
               

                double totalrevision = 0;
                foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                {
                    totalrevision += Convert.ToDouble(dr["CosteRevision"]);
                }
                totalrevision = Math.Truncate(totalrevision * 100) / 100;
                dgv_AreaRechazo.Columns[11].FooterText = totalrevision.ToString("C");
            
                //sumo defectos
                int Defecto1 = 0;
                foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                {
                    Defecto1 += Convert.ToInt32(dr["Def1"]);
                }
                dgv_AreaRechazo.Columns[14].FooterText = Defecto1.ToString();

                int Defecto2 = 0;
                foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                {
                    Defecto2 += Convert.ToInt32(dr["Def2"]);
                }
                dgv_AreaRechazo.Columns[15].FooterText = Defecto2.ToString();

                int Defecto3 = 0;
                foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                {
                    Defecto3 += Convert.ToInt32(dr["Def3"]);
                }
                dgv_AreaRechazo.Columns[16].FooterText = Defecto3.ToString();

                int Defecto4 = 0;
                foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                {
                    Defecto4 += Convert.ToInt32(dr["Def4"]);
                }
                dgv_AreaRechazo.Columns[17].FooterText = Defecto4.ToString();

                int Defecto5 = 0;
                foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                {
                    Defecto5 += Convert.ToInt32(dr["Def5"]);
                }
                dgv_AreaRechazo.Columns[18].FooterText = Defecto5.ToString();

                int Defecto6 = 0;
                foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                {
                    Defecto6 += Convert.ToInt32(dr["Def6"]);
                }
                dgv_AreaRechazo.Columns[19].FooterText = Defecto6.ToString();

                int Defecto7 = 0;
                foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                {
                    Defecto7 += Convert.ToInt32(dr["Def7"]);
                }
                dgv_AreaRechazo.Columns[20].FooterText = Defecto7.ToString();

                int Defecto8 = 0;
                foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                {
                    Defecto8 += Convert.ToInt32(dr["Def8"]);
                }
                dgv_AreaRechazo.Columns[21].FooterText = Defecto8.ToString();

                int Defecto9 = 0;
                foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                {
                    Defecto9 += Convert.ToInt32(dr["Def9"]);
                }
                dgv_AreaRechazo.Columns[22].FooterText = Defecto9.ToString();

                int Defecto10 = 0;
                foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                {
                    Defecto10 += Convert.ToInt32(dr["Def10"]);
                }
                dgv_AreaRechazo.Columns[23].FooterText = Defecto10.ToString();

                int Defecto11 = 0;
                foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                {
                    Defecto11 += Convert.ToInt32(dr["Def11"]);
                }
                dgv_AreaRechazo.Columns[24].FooterText = Defecto11.ToString();

                int Defecto12 = 0;
                foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                {
                    Defecto12 += Convert.ToInt32(dr["Def12"]);
                }
                dgv_AreaRechazo.Columns[25].FooterText = Defecto12.ToString();

                int Defecto13 = 0;
                foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                {
                    Defecto13 += Convert.ToInt32(dr["Def13"]);
                }
                dgv_AreaRechazo.Columns[26].FooterText = Defecto13.ToString();

                int Defecto14 = 0;
                foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                {
                    Defecto14 += Convert.ToInt32(dr["Def14"]);
                }
                dgv_AreaRechazo.Columns[27].FooterText = Defecto14.ToString();

                int Defecto15 = 0;
                foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                {
                    Defecto15 += Convert.ToInt32(dr["Def15"]);
                }
                dgv_AreaRechazo.Columns[28].FooterText = Defecto15.ToString();

                int Defecto16 = 0;
                foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                {
                    Defecto16 += Convert.ToInt32(dr["Def16"]);
                }
                dgv_AreaRechazo.Columns[29].FooterText = Defecto16.ToString();

                int Defecto17 = 0;
                foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                {
                    Defecto17 += Convert.ToInt32(dr["Def17"]);
                }
                dgv_AreaRechazo.Columns[30].FooterText = Defecto17.ToString();

                int Defecto18 = 0;
                foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                {
                    Defecto18 += Convert.ToInt32(dr["Def18"]);
                }
                dgv_AreaRechazo.Columns[31].FooterText = Defecto18.ToString();

                int Defecto19 = 0;
                foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                {
                    Defecto19 += Convert.ToInt32(dr["Def19"]);
                }
                dgv_AreaRechazo.Columns[32].FooterText = Defecto19.ToString();

                int Defecto20 = 0;
                foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                {
                    Defecto20 += Convert.ToInt32(dr["Def20"]);
                }
                dgv_AreaRechazo.Columns[33].FooterText = Defecto20.ToString();

                int Defecto21 = 0;
                foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                {
                    Defecto21 += Convert.ToInt32(dr["Def21"]);
                }
                dgv_AreaRechazo.Columns[34].FooterText = Defecto21.ToString();

                int Defecto22 = 0;
                foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                {
                    Defecto22 += Convert.ToInt32(dr["Def22"]);
                }
                dgv_AreaRechazo.Columns[35].FooterText = Defecto22.ToString();

                int Defecto23 = 0;
                foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                {
                    Defecto23 += Convert.ToInt32(dr["Def23"]);
                }
                dgv_AreaRechazo.Columns[36].FooterText = Defecto23.ToString();

                int Defecto24 = 0;
                foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                {
                    Defecto24 += Convert.ToInt32(dr["Def24"]);
                }
                dgv_AreaRechazo.Columns[37].FooterText = Defecto24.ToString();

                int Defecto25 = 0;
                foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                {
                    Defecto25 += Convert.ToInt32(dr["Def25"]);
                }
                dgv_AreaRechazo.Columns[38].FooterText = Defecto25.ToString();

                int Defecto26 = 0;
                foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                {
                    Defecto26 += Convert.ToInt32(dr["Def26"]);
                }
                dgv_AreaRechazo.Columns[39].FooterText = Defecto26.ToString();

                int Defecto27 = 0;
                foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                {
                    Defecto27 += Convert.ToInt32(dr["Def27"]);
                }
                dgv_AreaRechazo.Columns[40].FooterText = Defecto27.ToString();

                int Defecto28 = 0;
                foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                {
                    Defecto28 += Convert.ToInt32(dr["Def28"]);
                }
                dgv_AreaRechazo.Columns[41].FooterText = Defecto28.ToString();

                int Defecto29 = 0;
                foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                {
                    Defecto29 += Convert.ToInt32(dr["Def29"]);
                }
                dgv_AreaRechazo.Columns[42].FooterText = Defecto29.ToString();

                int Defecto30 = 0;
                foreach (DataRow dr in ds_Referencias.Tables[0].Rows)
                {
                    Defecto30 += Convert.ToInt32(dr["Def30"]);
                }
                dgv_AreaRechazo.Columns[43].FooterText = Defecto30.ToString();
                dgv_AreaRechazo.DataBind();

               
            }
            catch (Exception)
            {
                dgv_AreaRechazo.DataSource = null;
                dgv_AreaRechazo.DataBind();
            }*/
        }     
        // carga la lista utilizando un filtro
        
        protected void cargar_Filtrados(object sender, EventArgs e)
        {
            try
            {
                Conexion_GP12 conexion = new Conexion_GP12();
                
                if (Convert.ToString(selectoperario.Text) == "")
                {

                    ds_Referencias = conexion.devuelve_detalle_revisiones_referencia_operario(Convert.ToInt32(tbNumOperario.Text));
                    ds_listaoperarios = conexion.cargar_operarios_bms(Convert.ToInt32(tbNumOperario.Text));
                    tbNumOperario.Text = ds_listaoperarios.Tables[0].Rows[0]["NUMOPERARIO"].ToString();
                    tbNombreOperario.Text = ds_listaoperarios.Tables[0].Rows[0]["NOMBRE"].ToString();
                }
                else
                {
                    ds_Referencias = conexion.devuelve_detalle_revisiones_referencia_operario(Convert.ToInt32(selectoperario.Text));
                    ds_listaoperarios = conexion.cargar_operarios_bms(Convert.ToInt32(selectoperario.Text));
                    tbNumOperario.Text = ds_listaoperarios.Tables[0].Rows[0]["NUMOPERARIO"].ToString();
                    tbNombreOperario.Text = ds_listaoperarios.Tables[0].Rows[0]["NOMBRE"].ToString();
                }
                //ds_Referencias = conexion.devuelve_detalle_revisiones_referencia_filtradas(selectReferencia.Text, selectLote.Text, estado, selectFecha.Text);
                dgv_AreaRechazo.DataSource = ds_Referencias;
                rellenar_grid();
               
            }
            catch (Exception)
            {

            }
        }
                   
        public void GridViewCommandEventHandler(Object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CargaDetalle")
            {
                Conexion_GP12 conexion = new Conexion_GP12();
                DataSet Modal = conexion.modal_detalles_ultimas_revisiones_referencia(e.CommandArgument.ToString());
                ColorPorDefecto();
                if (Modal.Tables[0].Rows[0]["FakeMode"].ToString() == "1") //FAKEMODE ON
                {
                    TbRevisadas.Text = Modal.Tables[0].Rows[0]["PiezasOK"].ToString();
                    TbBuenas.Text = Modal.Tables[0].Rows[0]["PiezasOK"].ToString();
                    detalleReferencia.Text = Modal.Tables[0].Rows[0]["Referencia"].ToString();
                    detalleReferenciaNombre.Text = Modal.Tables[0].Rows[0]["Nombre"].ToString();
                    TbOperarioRevision.Text = Modal.Tables[0].Rows[0]["OperarioRevision"].ToString();
                    TbEmpresaRevision.Text = Modal.Tables[0].Rows[0]["Proveedor"].ToString();

                    linkimagen1.NavigateUrl = "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg";
                    linkimagen1.ImageUrl = "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg";
                    linkimagen2.NavigateUrl = "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg";
                    linkimagen2.ImageUrl = "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg";
                    linkimagen3.NavigateUrl = "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg";
                    linkimagen3.ImageUrl = "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg";

                    CosteHoras.Text = Modal.Tables[0].Rows[0]["HORAS"].ToString();
                    CosteChatarra.Text = "0 €";
                    CosteOperario.Text = Modal.Tables[0].Rows[0]["CostePiezaRevision"].ToString();
                    CosteTotal.Text = Modal.Tables[0].Rows[0]["CostePiezaRevision"].ToString();


                }
                else
                {

                    TbRevisadas.Text = Modal.Tables[0].Rows[0]["PiezasRevisadas"].ToString();
                    TbBuenas.Text = Modal.Tables[0].Rows[0]["PiezasOK"].ToString();
                    TbRetrabajadas.Text = Modal.Tables[0].Rows[0]["Retrabajadas"].ToString();
                    TbMalas.Text = Modal.Tables[0].Rows[0]["PiezasNOK"].ToString();
                    detalleReferencia.Text = Modal.Tables[0].Rows[0]["Referencia"].ToString();
                    detalleReferenciaNombre.Text = Modal.Tables[0].Rows[0]["Nombre"].ToString();
                    TbOperarioRevision.Text = Modal.Tables[0].Rows[0]["OperarioRevision"].ToString();
                    TbEmpresaRevision.Text = Modal.Tables[0].Rows[0]["Proveedor"].ToString();

                    //ACTIVO CELDAS DE OPERARIO Y LAS COMPARO

                    Operario1.Visible = true;


                    Operario2.Visible = true;


                    Operario3.Visible = true;


                    Operario4.Visible = true;


                    Operario1.Text = Modal.Tables[0].Rows[0]["OPERARIO1"].ToString();

                    if (Operario1.Text == "")
                    {

                        Operario1.Visible = false;

                    }
                    Operario2.Text = Modal.Tables[0].Rows[0]["OPERARIO2"].ToString();

                    if (Operario2.Text == "")
                    {

                        Operario2.Visible = false;

                    }
                    Operario3.Text = Modal.Tables[0].Rows[0]["OPERARIO3"].ToString();

                    if (Operario3.Text == "")
                    {

                        Operario3.Visible = false;

                    }
                    Operario4.Text = Modal.Tables[0].Rows[0]["OPERARIO4"].ToString();
                    if (Operario4.Text == "")
                    {

                        Operario4.Visible = false;

                    }

                    CosteHoras.Text = Modal.Tables[0].Rows[0]["HORAS"].ToString();
                    CosteChatarra.Text = Modal.Tables[0].Rows[0]["CosteScrapRevision"].ToString();
                    CosteOperario.Text = Modal.Tables[0].Rows[0]["CostePiezaRevision"].ToString();
                    CosteTotal.Text = Modal.Tables[0].Rows[0]["CosteRevision"].ToString();
                    TDDefecto1.Text = Modal.Tables[0].Rows[0]["Def1"].ToString();
                    TDNotas.Text = Modal.Tables[0].Rows[0]["Notas"].ToString();
                    ColorPorDefecto();
                    if (TDDefecto1.Text != "0")
                    {
                        THDefecto1.BackColor = System.Drawing.Color.Red;
                        TDDefecto1.BackColor = System.Drawing.Color.Red;
                        THDefecto1.ForeColor = System.Drawing.Color.White;
                        TDDefecto1.ForeColor = System.Drawing.Color.White;
                    }
                    TDDefecto2.Text = Modal.Tables[0].Rows[0]["Def2"].ToString();
                    if (TDDefecto2.Text != "0")
                    {
                        THDefecto2.BackColor = System.Drawing.Color.Red;
                        TDDefecto2.BackColor = System.Drawing.Color.Red;
                        THDefecto2.ForeColor = System.Drawing.Color.White;
                        TDDefecto2.ForeColor = System.Drawing.Color.White;
                    }
                    TDDefecto3.Text = Modal.Tables[0].Rows[0]["Def3"].ToString();
                    if (TDDefecto3.Text != "0")
                    {
                        THDefecto3.BackColor = System.Drawing.Color.Red;
                        TDDefecto3.BackColor = System.Drawing.Color.Red;
                        THDefecto3.ForeColor = System.Drawing.Color.White;
                        TDDefecto3.ForeColor = System.Drawing.Color.White;
                    }
                    TDDefecto4.Text = Modal.Tables[0].Rows[0]["Def4"].ToString();
                    if (TDDefecto4.Text != "0")
                    {
                        THDefecto4.BackColor = System.Drawing.Color.Red;
                        TDDefecto4.BackColor = System.Drawing.Color.Red;
                        THDefecto4.ForeColor = System.Drawing.Color.White;
                        TDDefecto4.ForeColor = System.Drawing.Color.White;
                    }
                    TDDefecto5.Text = Modal.Tables[0].Rows[0]["Def5"].ToString();
                    if (TDDefecto5.Text != "0")
                    {
                        THDefecto5.BackColor = System.Drawing.Color.Red;
                        TDDefecto5.BackColor = System.Drawing.Color.Red;
                        THDefecto5.ForeColor = System.Drawing.Color.White;
                        TDDefecto5.ForeColor = System.Drawing.Color.White;
                    }
                    TDDefecto6.Text = Modal.Tables[0].Rows[0]["Def6"].ToString();
                    if (TDDefecto6.Text != "0")
                    {
                        THDefecto6.BackColor = System.Drawing.Color.Red;
                        TDDefecto6.BackColor = System.Drawing.Color.Red;
                        THDefecto6.ForeColor = System.Drawing.Color.White;
                        TDDefecto6.ForeColor = System.Drawing.Color.White;
                    }
                    TDDefecto7.Text = Modal.Tables[0].Rows[0]["Def7"].ToString();
                    if (TDDefecto7.Text != "0")
                    {
                        THDefecto7.BackColor = System.Drawing.Color.Red;
                        TDDefecto7.BackColor = System.Drawing.Color.Red;
                        THDefecto7.ForeColor = System.Drawing.Color.White;
                        TDDefecto7.ForeColor = System.Drawing.Color.White;
                    }
                    TDDefecto8.Text = Modal.Tables[0].Rows[0]["Def8"].ToString();
                    if (TDDefecto8.Text != "0")
                    {
                        THDefecto8.BackColor = System.Drawing.Color.Red;
                        TDDefecto8.BackColor = System.Drawing.Color.Red;
                        THDefecto8.ForeColor = System.Drawing.Color.White;
                        TDDefecto8.ForeColor = System.Drawing.Color.White;
                    }
                    TDDefecto9.Text = Modal.Tables[0].Rows[0]["Def9"].ToString();
                    if (TDDefecto9.Text != "0")
                    {
                        THDefecto9.BackColor = System.Drawing.Color.Red;
                        TDDefecto9.BackColor = System.Drawing.Color.Red;
                        THDefecto9.ForeColor = System.Drawing.Color.White;
                        TDDefecto9.ForeColor = System.Drawing.Color.White;
                    }
                    TDDefecto10.Text = Modal.Tables[0].Rows[0]["Def10"].ToString();
                    if (TDDefecto10.Text != "0")
                    {
                        THDefecto10.BackColor = System.Drawing.Color.Red;
                        TDDefecto10.BackColor = System.Drawing.Color.Red;
                        THDefecto10.ForeColor = System.Drawing.Color.White;
                        TDDefecto10.ForeColor = System.Drawing.Color.White;
                    }
                    TDDefecto11.Text = Modal.Tables[0].Rows[0]["Def11"].ToString();
                    if (TDDefecto11.Text != "0")
                    {
                        THDefecto11.BackColor = System.Drawing.Color.Red;
                        TDDefecto11.BackColor = System.Drawing.Color.Red;
                        THDefecto11.ForeColor = System.Drawing.Color.White;
                        TDDefecto11.ForeColor = System.Drawing.Color.White;
                    }
                    TDDefecto12.Text = Modal.Tables[0].Rows[0]["Def12"].ToString();
                    if (TDDefecto12.Text != "0")
                    {
                        THDefecto12.BackColor = System.Drawing.Color.Red;
                        TDDefecto12.BackColor = System.Drawing.Color.Red;
                        THDefecto12.ForeColor = System.Drawing.Color.White;
                        TDDefecto12.ForeColor = System.Drawing.Color.White;
                    }
                    TDDefecto13.Text = Modal.Tables[0].Rows[0]["Def13"].ToString();
                    if (TDDefecto13.Text != "0")
                    {
                        THDefecto13.BackColor = System.Drawing.Color.Red;
                        TDDefecto13.BackColor = System.Drawing.Color.Red;
                        THDefecto13.ForeColor = System.Drawing.Color.White;
                        TDDefecto13.ForeColor = System.Drawing.Color.White;
                    }
                    TDDefecto14.Text = Modal.Tables[0].Rows[0]["Def14"].ToString();
                    if (TDDefecto14.Text != "0")
                    {
                        THDefecto14.BackColor = System.Drawing.Color.Red;
                        TDDefecto14.BackColor = System.Drawing.Color.Red;
                        THDefecto14.ForeColor = System.Drawing.Color.White;
                        TDDefecto14.ForeColor = System.Drawing.Color.White;
                    }
                    TDDefecto15.Text = Modal.Tables[0].Rows[0]["Def15"].ToString();
                    if (TDDefecto15.Text != "0")
                    {
                        THDefecto15.BackColor = System.Drawing.Color.Red;
                        TDDefecto15.BackColor = System.Drawing.Color.Red;
                        THDefecto15.ForeColor = System.Drawing.Color.White;
                        TDDefecto15.ForeColor = System.Drawing.Color.White;
                    }
                    TDDefecto16.Text = Modal.Tables[0].Rows[0]["Def16"].ToString();
                    if (TDDefecto16.Text != "0")
                    {
                        THDefecto16.BackColor = System.Drawing.Color.Red;
                        TDDefecto16.BackColor = System.Drawing.Color.Red;
                        THDefecto16.ForeColor = System.Drawing.Color.White;
                        TDDefecto16.ForeColor = System.Drawing.Color.White;
                    }
                    TDDefecto17.Text = Modal.Tables[0].Rows[0]["Def17"].ToString();
                    if (TDDefecto17.Text != "0")
                    {
                        THDefecto17.BackColor = System.Drawing.Color.Red;
                        TDDefecto17.BackColor = System.Drawing.Color.Red;
                        THDefecto17.ForeColor = System.Drawing.Color.White;
                        TDDefecto17.ForeColor = System.Drawing.Color.White;
                    }
                    TDDefecto18.Text = Modal.Tables[0].Rows[0]["Def18"].ToString();
                    if (TDDefecto18.Text != "0")
                    {
                        THDefecto18.BackColor = System.Drawing.Color.Red;
                        TDDefecto18.BackColor = System.Drawing.Color.Red;
                        THDefecto18.ForeColor = System.Drawing.Color.White;
                        TDDefecto18.ForeColor = System.Drawing.Color.White;
                    }
                    TDDefecto19.Text = Modal.Tables[0].Rows[0]["Def19"].ToString();
                    if (TDDefecto19.Text != "0")
                    {
                        THDefecto19.BackColor = System.Drawing.Color.Red;
                        TDDefecto19.BackColor = System.Drawing.Color.Red;
                        THDefecto19.ForeColor = System.Drawing.Color.White;
                        TDDefecto19.ForeColor = System.Drawing.Color.White;
                    }
                    TDDefecto20.Text = Modal.Tables[0].Rows[0]["Def20"].ToString();
                    if (TDDefecto20.Text != "0")
                    {
                        THDefecto20.BackColor = System.Drawing.Color.Red;
                        TDDefecto20.BackColor = System.Drawing.Color.Red;
                        THDefecto20.ForeColor = System.Drawing.Color.White;
                        TDDefecto20.ForeColor = System.Drawing.Color.White;
                    }
                    TDDefecto21.Text = Modal.Tables[0].Rows[0]["Def21"].ToString();
                    if (TDDefecto21.Text != "0")
                    {
                        THDefecto21.BackColor = System.Drawing.Color.Red;
                        TDDefecto21.BackColor = System.Drawing.Color.Red;
                        THDefecto21.ForeColor = System.Drawing.Color.White;
                        TDDefecto21.ForeColor = System.Drawing.Color.White;
                    }
                    TDDefecto22.Text = Modal.Tables[0].Rows[0]["Def22"].ToString();
                    if (TDDefecto22.Text != "0")
                    {
                        THDefecto22.BackColor = System.Drawing.Color.Red;
                        TDDefecto22.BackColor = System.Drawing.Color.Red;
                        THDefecto22.ForeColor = System.Drawing.Color.White;
                        TDDefecto22.ForeColor = System.Drawing.Color.White;
                    }
                    TDDefecto23.Text = Modal.Tables[0].Rows[0]["Def23"].ToString();
                    if (TDDefecto23.Text != "0")
                    {
                        THDefecto23.BackColor = System.Drawing.Color.Red;
                        TDDefecto23.BackColor = System.Drawing.Color.Red;
                        THDefecto23.ForeColor = System.Drawing.Color.White;
                        TDDefecto23.ForeColor = System.Drawing.Color.White;
                    }
                    TDDefecto24.Text = Modal.Tables[0].Rows[0]["Def24"].ToString();
                    if (TDDefecto24.Text != "0")
                    {
                        THDefecto24.BackColor = System.Drawing.Color.Red;
                        TDDefecto24.BackColor = System.Drawing.Color.Red;
                        THDefecto24.ForeColor = System.Drawing.Color.White;
                        TDDefecto24.ForeColor = System.Drawing.Color.White;
                    }
                    TDDefecto25.Text = Modal.Tables[0].Rows[0]["Def25"].ToString();
                    if (TDDefecto25.Text != "0")
                    {
                        THDefecto25.BackColor = System.Drawing.Color.Red;
                        TDDefecto25.BackColor = System.Drawing.Color.Red;
                        THDefecto25.ForeColor = System.Drawing.Color.White;
                        TDDefecto25.ForeColor = System.Drawing.Color.White;
                    }
                    TDDefecto26.Text = Modal.Tables[0].Rows[0]["Def26"].ToString();
                    if (TDDefecto26.Text != "0")
                    {
                        THDefecto26.BackColor = System.Drawing.Color.Red;
                        TDDefecto26.BackColor = System.Drawing.Color.Red;
                        THDefecto26.ForeColor = System.Drawing.Color.White;
                        TDDefecto26.ForeColor = System.Drawing.Color.White;
                    }
                    TDDefecto27.Text = Modal.Tables[0].Rows[0]["Def27"].ToString();
                    if (TDDefecto27.Text != "0")
                    {
                        THDefecto27.BackColor = System.Drawing.Color.Red;
                        TDDefecto27.BackColor = System.Drawing.Color.Red;
                        THDefecto27.ForeColor = System.Drawing.Color.White;
                        TDDefecto27.ForeColor = System.Drawing.Color.White;
                    }
                    TDDefecto28.Text = Modal.Tables[0].Rows[0]["Def28"].ToString();
                    if (TDDefecto28.Text != "0")
                    {
                        THDefecto28.BackColor = System.Drawing.Color.Red;
                        TDDefecto28.BackColor = System.Drawing.Color.Red;
                        THDefecto28.ForeColor = System.Drawing.Color.White;
                        TDDefecto28.ForeColor = System.Drawing.Color.White;
                    }
                    string imagen1 = Modal.Tables[0].Rows[0]["ImagenDefecto1"].ToString();
                    linkimagen1.NavigateUrl = imagen1;
                    linkimagen1.ImageUrl = imagen1;
                    string imagen2 = Modal.Tables[0].Rows[0]["ImagenDefecto2"].ToString();
                    linkimagen2.NavigateUrl = imagen2;
                    linkimagen2.ImageUrl = imagen2;
                    string imagen3 = Modal.Tables[0].Rows[0]["ImagenDefecto3"].ToString();
                    linkimagen3.NavigateUrl = imagen3;
                    linkimagen3.ImageUrl = imagen3;
                }

                lbAbrir_Modal(null, EventArgs.Empty);
            }
        }

        protected void lbAbrir_Modal(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        protected void ColorPorDefecto()
        {
            THDefecto1.BackColor = TbRevisadas.BackColor;
            THDefecto1.ForeColor = TbRevisadas.ForeColor;
            TDDefecto1.BackColor = TbRevisadas.BackColor;
            TDDefecto1.ForeColor = TbRevisadas.ForeColor;

            THDefecto2.BackColor = TbRevisadas.BackColor;
            THDefecto2.ForeColor = TbRevisadas.ForeColor;
            TDDefecto2.BackColor = TbRevisadas.BackColor;
            TDDefecto2.ForeColor = TbRevisadas.ForeColor;

            THDefecto3.BackColor = TbRevisadas.BackColor;
            THDefecto3.ForeColor = TbRevisadas.ForeColor;
            TDDefecto3.BackColor = TbRevisadas.BackColor;
            TDDefecto3.ForeColor = TbRevisadas.ForeColor;

            THDefecto4.BackColor = TbRevisadas.BackColor;
            THDefecto4.ForeColor = TbRevisadas.ForeColor;
            TDDefecto4.BackColor = TbRevisadas.BackColor;
            TDDefecto4.ForeColor = TbRevisadas.ForeColor;

            THDefecto5.BackColor = TbRevisadas.BackColor;
            THDefecto5.ForeColor = TbRevisadas.ForeColor;
            TDDefecto5.BackColor = TbRevisadas.BackColor;
            TDDefecto5.ForeColor = TbRevisadas.ForeColor;

            THDefecto6.BackColor = TbRevisadas.BackColor;
            THDefecto6.ForeColor = TbRevisadas.ForeColor;
            TDDefecto6.BackColor = TbRevisadas.BackColor;
            TDDefecto6.ForeColor = TbRevisadas.ForeColor;

            THDefecto7.BackColor = TbRevisadas.BackColor;
            THDefecto7.ForeColor = TbRevisadas.ForeColor;
            TDDefecto7.BackColor = TbRevisadas.BackColor;
            TDDefecto7.ForeColor = TbRevisadas.ForeColor;

            THDefecto8.BackColor = TbRevisadas.BackColor;
            THDefecto8.ForeColor = TbRevisadas.ForeColor;
            TDDefecto8.BackColor = TbRevisadas.BackColor;
            TDDefecto8.ForeColor = TbRevisadas.ForeColor;

            THDefecto9.BackColor = TbRevisadas.BackColor;
            THDefecto9.ForeColor = TbRevisadas.ForeColor;
            TDDefecto9.BackColor = TbRevisadas.BackColor;
            TDDefecto9.ForeColor = TbRevisadas.ForeColor;

            THDefecto10.BackColor = TbRevisadas.BackColor;
            THDefecto10.ForeColor = TbRevisadas.ForeColor;
            TDDefecto10.BackColor = TbRevisadas.BackColor;
            TDDefecto10.ForeColor = TbRevisadas.ForeColor;

            THDefecto11.BackColor = TbRevisadas.BackColor;
            THDefecto11.ForeColor = TbRevisadas.ForeColor;
            TDDefecto11.BackColor = TbRevisadas.BackColor;
            TDDefecto11.ForeColor = TbRevisadas.ForeColor;

            THDefecto12.BackColor = TbRevisadas.BackColor;
            THDefecto12.ForeColor = TbRevisadas.ForeColor;
            TDDefecto12.BackColor = TbRevisadas.BackColor;
            TDDefecto12.ForeColor = TbRevisadas.ForeColor;

            THDefecto13.BackColor = TbRevisadas.BackColor;
            THDefecto13.ForeColor = TbRevisadas.ForeColor;
            TDDefecto13.BackColor = TbRevisadas.BackColor;
            TDDefecto13.ForeColor = TbRevisadas.ForeColor;

            THDefecto14.BackColor = TbRevisadas.BackColor;
            THDefecto14.ForeColor = TbRevisadas.ForeColor;
            TDDefecto14.BackColor = TbRevisadas.BackColor;
            TDDefecto14.ForeColor = TbRevisadas.ForeColor;

            THDefecto15.BackColor = TbRevisadas.BackColor;
            THDefecto15.ForeColor = TbRevisadas.ForeColor;
            TDDefecto15.BackColor = TbRevisadas.BackColor;
            TDDefecto15.ForeColor = TbRevisadas.ForeColor;

            THDefecto16.BackColor = TbRevisadas.BackColor;
            THDefecto16.ForeColor = TbRevisadas.ForeColor;
            TDDefecto16.BackColor = TbRevisadas.BackColor;
            TDDefecto16.ForeColor = TbRevisadas.ForeColor;

            THDefecto17.BackColor = TbRevisadas.BackColor;
            THDefecto17.ForeColor = TbRevisadas.ForeColor;
            TDDefecto17.BackColor = TbRevisadas.BackColor;
            TDDefecto17.ForeColor = TbRevisadas.ForeColor;

            THDefecto18.BackColor = TbRevisadas.BackColor;
            THDefecto18.ForeColor = TbRevisadas.ForeColor;
            TDDefecto18.BackColor = TbRevisadas.BackColor;
            TDDefecto18.ForeColor = TbRevisadas.ForeColor;

            THDefecto19.BackColor = TbRevisadas.BackColor;
            THDefecto19.ForeColor = TbRevisadas.ForeColor;
            TDDefecto19.BackColor = TbRevisadas.BackColor;
            TDDefecto19.ForeColor = TbRevisadas.ForeColor;

            THDefecto20.BackColor = TbRevisadas.BackColor;
            THDefecto20.ForeColor = TbRevisadas.ForeColor;
            TDDefecto20.BackColor = TbRevisadas.BackColor;
            TDDefecto20.ForeColor = TbRevisadas.ForeColor;

            THDefecto21.BackColor = TbRevisadas.BackColor;
            THDefecto21.ForeColor = TbRevisadas.ForeColor;
            TDDefecto21.BackColor = TbRevisadas.BackColor;
            TDDefecto21.ForeColor = TbRevisadas.ForeColor;

            THDefecto22.BackColor = TbRevisadas.BackColor;
            THDefecto22.ForeColor = TbRevisadas.ForeColor;
            TDDefecto22.BackColor = TbRevisadas.BackColor;
            TDDefecto22.ForeColor = TbRevisadas.ForeColor;

            THDefecto23.BackColor = TbRevisadas.BackColor;
            THDefecto23.ForeColor = TbRevisadas.ForeColor;
            TDDefecto23.BackColor = TbRevisadas.BackColor;
            TDDefecto23.ForeColor = TbRevisadas.ForeColor;

            THDefecto24.BackColor = TbRevisadas.BackColor;
            THDefecto24.ForeColor = TbRevisadas.ForeColor;
            TDDefecto24.BackColor = TbRevisadas.BackColor;
            TDDefecto24.ForeColor = TbRevisadas.ForeColor;

            THDefecto25.BackColor = TbRevisadas.BackColor;
            THDefecto25.ForeColor = TbRevisadas.ForeColor;
            TDDefecto25.BackColor = TbRevisadas.BackColor;
            TDDefecto25.ForeColor = TbRevisadas.ForeColor;

            THDefecto26.BackColor = TbRevisadas.BackColor;
            THDefecto26.ForeColor = TbRevisadas.ForeColor;
            TDDefecto26.BackColor = TbRevisadas.BackColor;
            TDDefecto26.ForeColor = TbRevisadas.ForeColor;

            THDefecto27.BackColor = TbRevisadas.BackColor;
            THDefecto27.ForeColor = TbRevisadas.ForeColor;
            TDDefecto27.BackColor = TbRevisadas.BackColor;
            TDDefecto27.ForeColor = TbRevisadas.ForeColor;

            THDefecto28.BackColor = TbRevisadas.BackColor;
            THDefecto28.ForeColor = TbRevisadas.ForeColor;
            TDDefecto28.BackColor = TbRevisadas.BackColor;
            TDDefecto28.ForeColor = TbRevisadas.ForeColor;
        }

        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i <= dgv_AreaRechazo.Rows.Count - 1; i++)
                {
                    Label FakeMode = (Label)dgv_AreaRechazo.Rows[i].FindControl("lblFAKE");
                    Label lblparent = (Label)dgv_AreaRechazo.Rows[i].FindControl("lblMalas");
                    Label lblparent2 = (Label)dgv_AreaRechazo.Rows[i].FindControl("lblRetrabajadas");
                    Label lblparent3 = (Label)dgv_AreaRechazo.Rows[i].FindControl("lblDefecto1");
                    Label lblparent4 = (Label)dgv_AreaRechazo.Rows[i].FindControl("lblDefecto2");
                    Label lblparent5 = (Label)dgv_AreaRechazo.Rows[i].FindControl("lblDefecto3");
                    Label lblparent6 = (Label)dgv_AreaRechazo.Rows[i].FindControl("lblDefecto4");
                    Label lblparent7 = (Label)dgv_AreaRechazo.Rows[i].FindControl("lblDefecto5");
                    Label lblparent8 = (Label)dgv_AreaRechazo.Rows[i].FindControl("lblDefecto6");
                    Label lblparent9 = (Label)dgv_AreaRechazo.Rows[i].FindControl("lblDefecto7");
                    Label lblparent10 = (Label)dgv_AreaRechazo.Rows[i].FindControl("lblDefecto8");
                    Label lblparent11 = (Label)dgv_AreaRechazo.Rows[i].FindControl("lblDefecto9");
                    Label lblparent12 = (Label)dgv_AreaRechazo.Rows[i].FindControl("lblDefecto10");
                    Label lblparent13 = (Label)dgv_AreaRechazo.Rows[i].FindControl("lblDefecto11");
                    Label lblparent14 = (Label)dgv_AreaRechazo.Rows[i].FindControl("lblDefecto12");
                    Label lblparent15 = (Label)dgv_AreaRechazo.Rows[i].FindControl("lblDefecto13");
                    Label lblparent16 = (Label)dgv_AreaRechazo.Rows[i].FindControl("lblDefecto14");
                    Label lblparent17 = (Label)dgv_AreaRechazo.Rows[i].FindControl("lblDefecto15");
                    Label lblparent18 = (Label)dgv_AreaRechazo.Rows[i].FindControl("lblDefecto16");
                    Label lblparent19 = (Label)dgv_AreaRechazo.Rows[i].FindControl("lblDefecto17");
                    Label lblparent20 = (Label)dgv_AreaRechazo.Rows[i].FindControl("lblDefecto18");
                    Label lblparent21 = (Label)dgv_AreaRechazo.Rows[i].FindControl("lblDefecto19");
                    Label lblparent22 = (Label)dgv_AreaRechazo.Rows[i].FindControl("lblDefecto20");
                    Label lblparent23 = (Label)dgv_AreaRechazo.Rows[i].FindControl("lblDefecto21");
                    Label lblparent24 = (Label)dgv_AreaRechazo.Rows[i].FindControl("lblDefecto22");
                    Label lblparent25 = (Label)dgv_AreaRechazo.Rows[i].FindControl("lblDefecto23");
                    Label lblparent26 = (Label)dgv_AreaRechazo.Rows[i].FindControl("lblDefecto24");
                    Label lblparent27 = (Label)dgv_AreaRechazo.Rows[i].FindControl("lblDefecto25");
                    Label lblparent28 = (Label)dgv_AreaRechazo.Rows[i].FindControl("lblDefecto26");
                    Label lblparent29 = (Label)dgv_AreaRechazo.Rows[i].FindControl("lblDefecto27");
                    Label lblparent30 = (Label)dgv_AreaRechazo.Rows[i].FindControl("lblDefecto28");
                    Label lblparent31 = (Label)dgv_AreaRechazo.Rows[i].FindControl("lblDefecto29");
                    Label lblparent32 = (Label)dgv_AreaRechazo.Rows[i].FindControl("lblDefecto30");

                    if (Convert.ToInt32(FakeMode.Text) == 1)
                    {
                        lblparent.Text = "0";
                        lblparent2.Text = "0";
                        lblparent3.Text = "0";
                        lblparent4.Text = "0";
                        lblparent5.Text = "0";
                        lblparent6.Text = "0";
                        lblparent7.Text = "0";
                        lblparent8.Text = "0";
                        lblparent9.Text = "0";
                        lblparent10.Text = "0";
                        lblparent11.Text = "0";
                        lblparent12.Text = "0";
                        lblparent13.Text = "0";
                        lblparent14.Text = "0";
                        lblparent15.Text = "0";
                        lblparent16.Text = "0";
                        lblparent17.Text = "0";
                        lblparent18.Text = "0";
                        lblparent19.Text = "0";
                        lblparent20.Text = "0";
                        lblparent21.Text = "0";
                        lblparent22.Text = "0";
                        lblparent23.Text = "0";
                        lblparent24.Text = "0";
                        lblparent25.Text = "0";
                        lblparent26.Text = "0";
                        lblparent27.Text = "0";
                        lblparent28.Text = "0";
                        lblparent29.Text = "0";
                        lblparent30.Text = "0";
                        lblparent31.Text = "0";
                        lblparent32.Text = "0";
                    }
                    else
                    {
                        //colorear celdas de defectos
                        if (lblparent.Text != "0")
                        {
                            dgv_AreaRechazo.Rows[i].Cells[7].BackColor = System.Drawing.Color.Red;
                            lblparent.ForeColor = System.Drawing.Color.White;
                        }

                        if (lblparent2.Text != "0")
                        {
                            dgv_AreaRechazo.Rows[i].Cells[6].BackColor = System.Drawing.Color.Yellow;
                            lblparent2.ForeColor = System.Drawing.Color.Black;
                        }

                        if (lblparent3.Text != "0")
                        {
                            dgv_AreaRechazo.Rows[i].Cells[14].BackColor = System.Drawing.Color.Red;
                            lblparent3.ForeColor = System.Drawing.Color.White;
                        }

                        if (lblparent4.Text != "0")
                        {
                            dgv_AreaRechazo.Rows[i].Cells[15].BackColor = System.Drawing.Color.Red;
                            lblparent4.ForeColor = System.Drawing.Color.White;
                        }

                        if (lblparent5.Text != "0")
                        {
                            dgv_AreaRechazo.Rows[i].Cells[16].BackColor = System.Drawing.Color.Red;
                            lblparent5.ForeColor = System.Drawing.Color.White;
                        }

                        if (lblparent6.Text != "0")
                        {
                            dgv_AreaRechazo.Rows[i].Cells[17].BackColor = System.Drawing.Color.Red;
                            lblparent6.ForeColor = System.Drawing.Color.White;
                        }

                        if (lblparent7.Text != "0")
                        {
                            dgv_AreaRechazo.Rows[i].Cells[18].BackColor = System.Drawing.Color.Red;
                            lblparent7.ForeColor = System.Drawing.Color.White;
                        }

                        if (lblparent8.Text != "0")
                        {
                            dgv_AreaRechazo.Rows[i].Cells[19].BackColor = System.Drawing.Color.Red;
                            lblparent8.ForeColor = System.Drawing.Color.White;
                        }

                        if (lblparent9.Text != "0")
                        {
                            dgv_AreaRechazo.Rows[i].Cells[20].BackColor = System.Drawing.Color.Red;
                            lblparent9.ForeColor = System.Drawing.Color.White;
                        }

                        if (lblparent10.Text != "0")
                        {
                            dgv_AreaRechazo.Rows[i].Cells[21].BackColor = System.Drawing.Color.Red;
                            lblparent10.ForeColor = System.Drawing.Color.White;
                        }

                        if (lblparent11.Text != "0")
                        {
                            dgv_AreaRechazo.Rows[i].Cells[22].BackColor = System.Drawing.Color.Red;
                            lblparent11.ForeColor = System.Drawing.Color.White;
                        }

                        if (lblparent12.Text != "0")
                        {
                            dgv_AreaRechazo.Rows[i].Cells[23].BackColor = System.Drawing.Color.Red;
                            lblparent12.ForeColor = System.Drawing.Color.White;
                        }

                        if (lblparent13.Text != "0")
                        {
                            dgv_AreaRechazo.Rows[i].Cells[24].BackColor = System.Drawing.Color.Red;
                            lblparent13.ForeColor = System.Drawing.Color.White;
                        }

                        if (lblparent14.Text != "0")
                        {
                            dgv_AreaRechazo.Rows[i].Cells[25].BackColor = System.Drawing.Color.Red;
                            lblparent14.ForeColor = System.Drawing.Color.White;
                        }

                        if (lblparent15.Text != "0")
                        {
                            dgv_AreaRechazo.Rows[i].Cells[26].BackColor = System.Drawing.Color.Red;
                            lblparent15.ForeColor = System.Drawing.Color.White;
                        }

                        if (lblparent16.Text != "0")
                        {
                            dgv_AreaRechazo.Rows[i].Cells[27].BackColor = System.Drawing.Color.Red;
                            lblparent16.ForeColor = System.Drawing.Color.White;
                        }

                        if (lblparent17.Text != "0")
                        {
                            dgv_AreaRechazo.Rows[i].Cells[28].BackColor = System.Drawing.Color.Red;
                            lblparent17.ForeColor = System.Drawing.Color.White;
                        }

                        if (lblparent18.Text != "0")
                        {
                            dgv_AreaRechazo.Rows[i].Cells[29].BackColor = System.Drawing.Color.Red;
                            lblparent18.ForeColor = System.Drawing.Color.White;
                        }

                        if (lblparent19.Text != "0")
                        {
                            dgv_AreaRechazo.Rows[i].Cells[30].BackColor = System.Drawing.Color.Red;
                            lblparent19.ForeColor = System.Drawing.Color.White;
                        }

                        if (lblparent20.Text != "0")
                        {
                            dgv_AreaRechazo.Rows[i].Cells[31].BackColor = System.Drawing.Color.Red;
                            lblparent20.ForeColor = System.Drawing.Color.White;
                        }

                        if (lblparent21.Text != "0")
                        {
                            dgv_AreaRechazo.Rows[i].Cells[32].BackColor = System.Drawing.Color.Red;
                            lblparent21.ForeColor = System.Drawing.Color.White;
                        }

                        if (lblparent22.Text != "0")
                        {
                            dgv_AreaRechazo.Rows[i].Cells[33].BackColor = System.Drawing.Color.Red;
                            lblparent22.ForeColor = System.Drawing.Color.White;
                        }

                        if (lblparent23.Text != "0")
                        {
                            dgv_AreaRechazo.Rows[i].Cells[34].BackColor = System.Drawing.Color.Red;
                            lblparent23.ForeColor = System.Drawing.Color.White;
                        }

                        if (lblparent24.Text != "0")
                        {
                            dgv_AreaRechazo.Rows[i].Cells[35].BackColor = System.Drawing.Color.Red;
                            lblparent24.ForeColor = System.Drawing.Color.White;
                        }

                        if (lblparent25.Text != "0")
                        {
                            dgv_AreaRechazo.Rows[i].Cells[36].BackColor = System.Drawing.Color.Red;
                            lblparent25.ForeColor = System.Drawing.Color.White;
                        }

                        if (lblparent26.Text != "0")
                        {
                            dgv_AreaRechazo.Rows[i].Cells[37].BackColor = System.Drawing.Color.Red;
                            lblparent26.ForeColor = System.Drawing.Color.White;
                        }

                        if (lblparent27.Text != "0")
                        {
                            dgv_AreaRechazo.Rows[i].Cells[38].BackColor = System.Drawing.Color.Red;
                            lblparent27.ForeColor = System.Drawing.Color.White;
                        }

                        if (lblparent28.Text != "0")
                        {
                            dgv_AreaRechazo.Rows[i].Cells[39].BackColor = System.Drawing.Color.Red;
                            lblparent28.ForeColor = System.Drawing.Color.White;
                        }

                        if (lblparent29.Text != "0")
                        {
                            dgv_AreaRechazo.Rows[i].Cells[40].BackColor = System.Drawing.Color.Red;
                            lblparent29.ForeColor = System.Drawing.Color.White;
                        }

                        if (lblparent30.Text != "0")
                        {
                            dgv_AreaRechazo.Rows[i].Cells[41].BackColor = System.Drawing.Color.Red;
                            lblparent30.ForeColor = System.Drawing.Color.White;
                        }

                        if (lblparent31.Text != "0")
                        {
                            dgv_AreaRechazo.Rows[i].Cells[42].BackColor = System.Drawing.Color.Red;
                            lblparent31.ForeColor = System.Drawing.Color.White;
                        }

                        if (lblparent32.Text != "0")
                        {
                            dgv_AreaRechazo.Rows[i].Cells[43].BackColor = System.Drawing.Color.Red;
                            lblparent32.ForeColor = System.Drawing.Color.White;
                        }
                    }
                }
            }
            catch (Exception ex)
            { }

        }

    }

}