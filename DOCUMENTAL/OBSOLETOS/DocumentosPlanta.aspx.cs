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

namespace ThermoWeb.DOCUMENTAL
{
    public partial class DocumentosPlanta : System.Web.UI.Page
    {

        //private static DataSet ds_DocumentosPlanta = new DataSet();


        protected void Page_Load(object sender, EventArgs e)
        {

            if (IsPostBack)
            {
                HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate");
                HttpContext.Current.Response.AddHeader("Pragma", "no-cache");
                HttpContext.Current.Response.AddHeader("Expires", "0");
                Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(1));
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetNoStore();
                cargadatos();

            }
            if (!IsPostBack)
            {
                HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate");
                HttpContext.Current.Response.AddHeader("Pragma", "no-cache");
                HttpContext.Current.Response.AddHeader("Expires", "0");
                Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(1));
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetNoStore();
                cargadatos();

            }
        }

        public void cargadatos()
        {
            string referenciactiva1 = "";
            string referenciactiva2 = "";
            string referenciactiva3 = "";
            string referenciactiva4 = "";
            try
            {

                if (Request.QueryString["MAQUINA"] != null)
                {

                    Conexion_DOCUMENTAL conexion = new Conexion_DOCUMENTAL();
                    DataSet datosorden = conexion.DevuelveOrdenProducciendo(Convert.ToString(Request.QueryString["MAQUINA"]));
                    dgv_Ordenes.DataSource = datosorden;
                    dgv_Ordenes.DataBind();

                    if (datosorden.Tables[0].Rows.Count > 0)
                    {

                        //SELECT C_ID, C_PRODUCT_ID, C_PRODLONGDESCR, C_CUSTOMER, C_MACHINE_ID
                        referenciactiva1 = datosorden.Tables[0].Rows[0]["C_PRODUCT_ID"].ToString();
                        tbOrden1.Text = datosorden.Tables[0].Rows[0]["C_ID"].ToString();
                        tbReferencia1.Text = datosorden.Tables[0].Rows[0]["C_PRODUCT_ID"].ToString();
                        tbNombre1.Text = datosorden.Tables[0].Rows[0]["C_PRODLONGDESCR"].ToString();
                        tbMaquina1.Text = datosorden.Tables[0].Rows[0]["C_MACHINE_ID"].ToString();
                        tbMolde1.Text = datosorden.Tables[0].Rows[0]["C_TOOL_ID"].ToString();

                    }
                    if (datosorden.Tables[0].Rows.Count > 1)
                    {
                        referenciactiva2 = datosorden.Tables[0].Rows[1]["C_PRODUCT_ID"].ToString();
                        tbOrden2.Text = datosorden.Tables[0].Rows[1]["C_ID"].ToString();
                        tbReferencia2.Text = datosorden.Tables[0].Rows[1]["C_PRODUCT_ID"].ToString();
                        tbNombre2.Text = datosorden.Tables[0].Rows[1]["C_PRODLONGDESCR"].ToString();
                        tbMaquina2.Text = datosorden.Tables[0].Rows[1]["C_MACHINE_ID"].ToString();
                        tbMolde2.Text = datosorden.Tables[0].Rows[1]["C_TOOL_ID"].ToString();
                    }
                    if (datosorden.Tables[0].Rows.Count > 2)
                    {
                        referenciactiva3 = datosorden.Tables[0].Rows[2]["C_PRODUCT_ID"].ToString();
                        tbOrden3.Text = datosorden.Tables[0].Rows[2]["C_ID"].ToString();
                        tbReferencia3.Text = datosorden.Tables[0].Rows[2]["C_PRODUCT_ID"].ToString();
                        tbNombre3.Text = datosorden.Tables[0].Rows[2]["C_PRODLONGDESCR"].ToString();
                        tbMaquina3.Text = datosorden.Tables[0].Rows[2]["C_MACHINE_ID"].ToString();
                        tbMolde3.Text = datosorden.Tables[0].Rows[2]["C_TOOL_ID"].ToString();
                    }
                    if (datosorden.Tables[0].Rows.Count > 3)
                    {
                        referenciactiva4 = datosorden.Tables[0].Rows[3]["C_PRODUCT_ID"].ToString();
                        tbOrden4.Text = datosorden.Tables[0].Rows[3]["C_ID"].ToString();
                        tbReferencia4.Text = datosorden.Tables[0].Rows[3]["C_PRODUCT_ID"].ToString();
                        tbNombre4.Text = datosorden.Tables[0].Rows[3]["C_PRODLONGDESCR"].ToString();
                        tbMaquina4.Text = datosorden.Tables[0].Rows[3]["C_MACHINE_ID"].ToString();
                        tbMolde4.Text = datosorden.Tables[0].Rows[3]["C_TOOL_ID"].ToString();
                    }
                    cargarframesmaquina(referenciactiva1, referenciactiva2, referenciactiva3, referenciactiva4);
                    cargarcarruselGP12(referenciactiva1, referenciactiva2, referenciactiva3, referenciactiva4);
                    CargarTrabajadores();
                }
                else
                {
                    referenciactiva1 = Request.QueryString["REFERENCIA"].ToString();
                    NUM1.Visible = false;
                    navbarrecarga.Visible = false;
                    cargarframesmaquina(referenciactiva1, referenciactiva2, referenciactiva3, referenciactiva4);
                    cargarcarruselGP12(referenciactiva1, referenciactiva2, referenciactiva3, referenciactiva4);
                    CargarTrabajadores();
                }
            }
            catch (Exception)
            { }
        }

        public void pruebazoom(Object sender, EventArgs e)
        {
            PautaControl_1.Attributes.Add("style","overflow:hidden; height: 80%; width: 100%") ;
        }
        public void cargarframesmaquina(string ref1, string ref2, string ref3, string ref4)
        {
            try
            {

                Conexion_DOCUMENTAL conexion = new Conexion_DOCUMENTAL();

                if (ref1 != "")
                {
                    ref1labtext.InnerText = ref1;

                    DataSet ds_DocumentosPlanta = conexion.devuelve_dataset_filtroreferencias(ref1);
                    if (ds_DocumentosPlanta.Tables[0].Rows[0]["PautaControl"].ToString() != "")
                    {
                        PautaControl_1.Attributes.Add("src", ds_DocumentosPlanta.Tables[0].Rows[0]["PautaControl"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                    }
                    else
                    {
                        PautaControl_1.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                    }
                    if (ds_DocumentosPlanta.Tables[0].Rows[0]["Defoteca"].ToString() != "")
                    {
                        DEFECTOS_1.Attributes.Add("src", ds_DocumentosPlanta.Tables[0].Rows[0]["Defoteca"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                    }

                    else
                    {
                        DEFECTOS_1.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                    }
                    if (ds_DocumentosPlanta.Tables[0].Rows[0]["OperacionEstandar"].ToString() != "")
                    {
                        GP12_1.Attributes.Add("src", ds_DocumentosPlanta.Tables[0].Rows[0]["OperacionEstandar"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                    }
                    else
                    {
                        GP12_1.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                    }

                    if (ds_DocumentosPlanta.Tables[0].Rows[0]["Embalaje"].ToString() != "")
                    {
                        PAUTAEMBALAJE_1.Attributes.Add("src", ds_DocumentosPlanta.Tables[0].Rows[0]["Embalaje"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                    }
                    else
                    {
                        PAUTAEMBALAJE_1.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                    }

                    if (ds_DocumentosPlanta.Tables[0].Rows[0]["Logotipo"].ToString() != "")
                    {
                        CLIENTE.ImageUrl = ds_DocumentosPlanta.Tables[0].Rows[0]["Logotipo"].ToString();
                    }
                    else
                    {
                        CLIENTE.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                    }


                    DataSet estadoGP12 = conexion.devuelve_estado_GP12(ref1);
                    if (Convert.ToInt32(estadoGP12.Tables[0].Rows[0]["EstadoActual"].ToString()) > 0)
                    {
                        tbAlertaCalidad1.Visible = true;
                        tbAlertaCalidad1.Text = "PRODUCTO PENDIENTE DE MURO DE CALIDAD - " + estadoGP12.Tables[0].Rows[0]["Razon"].ToString();
                    }

                    DataSet ModDOCU = conexion.devuelve_fechaModDocumentos(tbMolde1.Text);
                    tbfechaMod.Text = ModDOCU.Tables[0].Rows[0]["FechaModificacion"].ToString();
                    RazonMod.Text = ModDOCU.Tables[0].Rows[0]["RazonModificacion"].ToString();

                }
                if (ref2 != "")
                {
                    ref2labtext.InnerText = ref2;
                    ref2lab.Visible = true;
                    DataSet ds_DocumentosPlanta = conexion.devuelve_dataset_filtroreferencias(ref2);

                    if (ds_DocumentosPlanta.Tables[0].Rows[0]["PautaControl"].ToString() != "")
                    {
                        PautaControl_2.Attributes.Add("src", ds_DocumentosPlanta.Tables[0].Rows[0]["PautaControl"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                    }
                    else
                    {
                        PautaControl_2.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                    }
                    if (ds_DocumentosPlanta.Tables[0].Rows[0]["Defoteca"].ToString() != "")
                    {
                        DEFECTOS_2.Attributes.Add("src", ds_DocumentosPlanta.Tables[0].Rows[0]["Defoteca"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                    }
                    else
                    {
                        DEFECTOS_2.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                    }
                    if (ds_DocumentosPlanta.Tables[0].Rows[0]["OperacionEstandar"].ToString() != "")
                    {
                        GP12_2.Attributes.Add("src", ds_DocumentosPlanta.Tables[0].Rows[0]["OperacionEstandar"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                    }
                    else
                    {
                        GP12_2.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                    }
                    if (ds_DocumentosPlanta.Tables[0].Rows[0]["Embalaje"].ToString() != "")
                    {
                        PAUTAEMBALAJE_2.Attributes.Add("src", ds_DocumentosPlanta.Tables[0].Rows[0]["Embalaje"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                    }
                    else
                    {
                        PAUTAEMBALAJE_2.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                    }
                    DataSet estadoGP12 = conexion.devuelve_estado_GP12(ref2);
                    if (Convert.ToInt32(estadoGP12.Tables[0].Rows[0]["EstadoActual"].ToString()) > 0)
                    {
                        tbAlertaCalidad2.Visible = true;
                        tbAlertaCalidad2.Text = "PRODUCTO PENDIENTE DE MURO DE CALIDAD - " + estadoGP12.Tables[0].Rows[0]["Razon"].ToString();
                    }
                }
                if (ref3 != "")
                {
                    ref3lab.Visible = true;
                    ref3labtext.InnerText = ref3;
                    DataSet ds_DocumentosPlanta = conexion.devuelve_dataset_filtroreferencias(ref3);
                    /*PautaControl_3.Attributes.Add("src", ds_DocumentosPlanta.Tables[0].Rows[0]["PautaControl"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                    DEFECTOS_3.Attributes.Add("src", ds_DocumentosPlanta.Tables[0].Rows[0]["Defoteca"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                    GP12_3.Attributes.Add("src", ds_DocumentosPlanta.Tables[0].Rows[0]["OperacionEstandar"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                    PAUTAEMBALAJE_3.Attributes.Add("src", ds_DocumentosPlanta.Tables[0].Rows[0]["Embalaje"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");*/
                    if (ds_DocumentosPlanta.Tables[0].Rows[0]["PautaControl"].ToString() != "")
                    {
                        PautaControl_3.Attributes.Add("src", ds_DocumentosPlanta.Tables[0].Rows[0]["PautaControl"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                    }
                    else
                    {
                        PautaControl_3.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                    }
                    if (ds_DocumentosPlanta.Tables[0].Rows[0]["Defoteca"].ToString() != "")
                    {
                        DEFECTOS_3.Attributes.Add("src", ds_DocumentosPlanta.Tables[0].Rows[0]["Defoteca"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                    }
                    else
                    {
                        DEFECTOS_3.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                    }
                    if (ds_DocumentosPlanta.Tables[0].Rows[0]["OperacionEstandar"].ToString() != "")
                    {
                        GP12_3.Attributes.Add("src", ds_DocumentosPlanta.Tables[0].Rows[0]["OperacionEstandar"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                    }
                    else
                    {
                        GP12_3.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                    }
                    if (ds_DocumentosPlanta.Tables[0].Rows[0]["Embalaje"].ToString() != "")
                    {
                        PAUTAEMBALAJE_3.Attributes.Add("src", ds_DocumentosPlanta.Tables[0].Rows[0]["Embalaje"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                    }
                    else
                    {
                        PAUTAEMBALAJE_3.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                    }


                    DataSet estadoGP12 = conexion.devuelve_estado_GP12(ref3);
                    if (Convert.ToInt32(estadoGP12.Tables[0].Rows[0]["EstadoActual"].ToString()) > 0)
                    {
                        tbAlertaCalidad3.Visible = true;
                        tbAlertaCalidad3.Text = "PRODUCTO PENDIENTE DE MURO DE CALIDAD - " + estadoGP12.Tables[0].Rows[0]["Razon"].ToString();
                    }
                }
                if (ref4 != "")
                {
                    ref4lab.Visible = true;
                    ref4labtext.InnerText = ref4;
                    DataSet ds_DocumentosPlanta = conexion.devuelve_dataset_filtroreferencias(ref4);
                    /*PautaControl_4.Attributes.Add("src", ds_DocumentosPlanta.Tables[0].Rows[0]["PautaControl"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                    DEFECTOS_4.Attributes.Add("src", ds_DocumentosPlanta.Tables[0].Rows[0]["Defoteca"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                    GP12_4.Attributes.Add("src", ds_DocumentosPlanta.Tables[0].Rows[0]["OperacionEstandar"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                    PAUTAEMBALAJE_4.Attributes.Add("src", ds_DocumentosPlanta.Tables[0].Rows[0]["Embalaje"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");*/
                    if (ds_DocumentosPlanta.Tables[0].Rows[0]["PautaControl"].ToString() != "")
                    {
                        PautaControl_4.Attributes.Add("src", ds_DocumentosPlanta.Tables[0].Rows[0]["PautaControl"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                    }
                    else
                    {
                        PautaControl_4.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                    }
                    if (ds_DocumentosPlanta.Tables[0].Rows[0]["Defoteca"].ToString() != "")
                    {
                        DEFECTOS_4.Attributes.Add("src", ds_DocumentosPlanta.Tables[0].Rows[0]["Defoteca"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                    }
                    else
                    {
                        DEFECTOS_4.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                    }
                    if (ds_DocumentosPlanta.Tables[0].Rows[0]["OperacionEstandar"].ToString() != "")
                    {
                        GP12_4.Attributes.Add("src", ds_DocumentosPlanta.Tables[0].Rows[0]["OperacionEstandar"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                    }
                    else
                    {
                        GP12_4.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                    }
                    if (ds_DocumentosPlanta.Tables[0].Rows[0]["Embalaje"].ToString() != "")
                    {
                        PAUTAEMBALAJE_4.Attributes.Add("src", ds_DocumentosPlanta.Tables[0].Rows[0]["Embalaje"].ToString() + "#toolbar=0&navpanes=0&scrollbar=0&zoom=125");
                    }
                    else
                    {
                        PAUTAEMBALAJE_4.Attributes.Add("src", "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg");
                    }
                    DataSet estadoGP12 = conexion.devuelve_estado_GP12(ref4);
                    if (Convert.ToInt32(estadoGP12.Tables[0].Rows[0]["EstadoActual"].ToString()) > 0)
                    {
                        tbAlertaCalidad4.Visible = true;
                        tbAlertaCalidad4.Text = "PRODUCTO PENDIENTE DE MURO DE CALIDAD - " + estadoGP12.Tables[0].Rows[0]["Razon"].ToString();
                    }
                }
            }
            catch (Exception)
            { }
        }

        public void cargarcarruselGP12(string ref1, string ref2, string ref3, string ref4)
        {
            try
            {
                DataSet imagenes = new DataSet();
                Conexion_DOCUMENTAL conexion = new Conexion_DOCUMENTAL();

                if (ref1 != "")
                {
                    imagenes = conexion.DatasetDetectadosGP12(ref1);
                    foreach (DataRow row in imagenes.Tables[0].Rows)
                    {
                        HtmlGenericControl divItem = new HtmlGenericControl("div");
                        divItem.Attributes["class"] = "item";
                        //divItem.ID = "prueba";
                        Image img = new Image();
                        img.ImageUrl = row["ImagenDefecto1"].ToString();
                        img.Visible = true;
                        divItem.Controls.Add(img);
                        img.Width = new Unit("100%");
                        ACTIVOS.Controls.Add(divItem);
                    }
                }
                if (ref2 != "")
                {
                    imagenes = conexion.DatasetDetectadosGP12(ref2);
                    foreach (DataRow row in imagenes.Tables[0].Rows)
                    {
                        HtmlGenericControl divItem = new HtmlGenericControl("div");
                        divItem.Attributes["class"] = "item";
                        //divItem.ID = "prueba";
                        Image img = new Image();
                        img.ImageUrl = row["ImagenDefecto1"].ToString();
                        img.Visible = true;
                        divItem.Controls.Add(img);
                        img.Width = new Unit("100%");
                        ACTIVOS2.Controls.Add(divItem);
                    }
                }
                if (ref3 != "")
                {
                    imagenes = conexion.DatasetDetectadosGP12(ref3);
                    foreach (DataRow row in imagenes.Tables[0].Rows)
                    {
                        HtmlGenericControl divItem = new HtmlGenericControl("div");
                        divItem.Attributes["class"] = "item";
                        //divItem.ID = "prueba";
                        Image img = new Image();
                        img.ImageUrl = row["ImagenDefecto1"].ToString();
                        img.Visible = true;
                        divItem.Controls.Add(img);
                        img.Width = new Unit("100%");
                        ACTIVOS3.Controls.Add(divItem);
                    }
                }
                if (ref4 != "")
                {
                    imagenes = conexion.DatasetDetectadosGP12(ref4);
                    foreach (DataRow row in imagenes.Tables[0].Rows)
                    {
                        HtmlGenericControl divItem = new HtmlGenericControl("div");
                        divItem.Attributes["class"] = "item";
                        //divItem.ID = "prueba";
                        Image img = new Image();
                        img.ImageUrl = row["ImagenDefecto1"].ToString();
                        img.Visible = true;
                        divItem.Controls.Add(img);
                        img.Width = new Unit("100%");
                        ACTIVOS4.Controls.Add(divItem);
                    }
                }

            }
            catch (Exception)
            { }
        }

        public void CargarTrabajadores()
        {
            try
            {
                Conexion_DOCUMENTAL conexion = new Conexion_DOCUMENTAL();
                DataSet calidadplanta = conexion.devuelve_calidadplanta_logueadoXMaquina(tbMaquina1.Text);
                if (calidadplanta.Tables[0].Rows.Count > 0)
                {
                    CalidadNumero.Text = calidadplanta.Tables[0].Rows[0]["C_CLOCKNO"].ToString();
                    CalidadNombre.Text = calidadplanta.Tables[0].Rows[0]["C_NAME"].ToString();
                    calidadplanta = conexion.devuelve_horasXReferenciaXOperario(Convert.ToInt32(tbMolde1.Text), Convert.ToInt32(CalidadNumero.Text));
                    if (calidadplanta.Tables[0].Rows.Count > 0)
                    { CalidadHoras.Text = calidadplanta.Tables[0].Rows[0]["TIEMPOHORAS"].ToString(); }
                    else
                    { CalidadHoras.Text = "0"; }
                }

                DataSet encargado = conexion.devuelve_encargado_logueadoXMaquina(tbMaquina1.Text);
                if (encargado.Tables[0].Rows.Count > 0)
                {
                    EncargadoNumero.Text = encargado.Tables[0].Rows[0]["C_CLOCKNO"].ToString();
                    EncargadoNombre.Text = encargado.Tables[0].Rows[0]["C_NAME"].ToString();
                    encargado = conexion.devuelve_horasXReferenciaXOperario(Convert.ToInt32(tbMolde1.Text), Convert.ToInt32(EncargadoNumero.Text));
                    if (encargado.Tables[0].Rows.Count > 0)
                    { EncargadoHoras.Text = encargado.Tables[0].Rows[0]["TIEMPOHORAS"].ToString(); }
                    else
                    { EncargadoHoras.Text = "0"; }
                }

                DataSet operario = conexion.devuelve_operario_logueadoXMaquina(tbMaquina1.Text);
                if (operario.Tables[0].Rows.Count > 0)
                {
                    Operario1Numero.Text = operario.Tables[0].Rows[0]["C_CLOCKNO"].ToString();
                    Operario1Nombre.Text = operario.Tables[0].Rows[0]["C_NAME"].ToString();
                    if (operario.Tables[0].Rows.Count > 1)
                    {
                        Operario2Numero.Text = operario.Tables[0].Rows[1]["C_CLOCKNO"].ToString();
                        Operario2Nombre.Text = operario.Tables[0].Rows[1]["C_NAME"].ToString();
                        Operario2Posicion.Visible = true;
                        Operario2Nivel.Visible = true;
                        Operario2Horas.Visible = true;
                        Operario2Nombre.Visible = true;

                        Operario2Numero.Visible = true;

                    }

                    operario = conexion.devuelve_horasXReferenciaXOperario(Convert.ToInt32(tbMolde1.Text), Convert.ToInt32(Operario1Numero.Text));
                    if (operario.Tables[0].Rows.Count > 0)
                    {
                        Operario1Horas.Text = operario.Tables[0].Rows[0]["TIEMPOHORAS"].ToString();
                        //compruebo horas y asigno valor (revincular a aplicación)
                        double DoubleOperarioHoras = Convert.ToDouble(Operario1Horas.Text);
                        if (DoubleOperarioHoras < 10)
                        {
                            Operario1Nivel.Text = "I";

                        }
                        if (DoubleOperarioHoras > 10 && DoubleOperarioHoras < 80)
                        { Operario1Nivel.Text = "L"; }
                        if (DoubleOperarioHoras > 80)
                        { Operario1Nivel.Text = "U"; }
                    }
                    else
                    {
                        Operario1Horas.Text = "0";

                    }

                    if (Operario2Posicion.Visible == true)
                    {
                        DataSet Operario2 = conexion.devuelve_horasXReferenciaXOperario(Convert.ToInt32(tbMolde1.Text), Convert.ToInt32(Operario2Numero.Text));
                        if (Operario2.Tables[0].Rows.Count > 0)
                        {
                            Operario2Horas.Text = Operario2.Tables[0].Rows[0]["TIEMPOHORAS"].ToString();
                            double DoubleOperario2Horas = Convert.ToDouble(Operario2Horas.Text);
                            if (DoubleOperario2Horas < 10)
                            {
                                Operario2Nivel.Text = "I";

                            }
                            if (DoubleOperario2Horas > 10 && DoubleOperario2Horas < 80)
                            { Operario2Nivel.Text = "L"; }
                            if (DoubleOperario2Horas > 80)
                            { Operario2Nivel.Text = "U"; }
                        }
                        else
                        { Operario2Horas.Text = "0"; }
                    }

                    //CONSULTA LA VALIDACION DE DOCUMENTACION POR PARTE DEL OPERARIO 1
                    if (Operario1Numero.Text == "---")
                    {
                        AlertaDOC1.Visible = true;
                        AlertaDOCTEXT1.Text = "Ningún operario logueado. Por favor, logueate en BMS y pulsa 'Recargar con orden actual'";
                    }
                    else if (!conexion.existe_validacion_operario(Operario1Numero.Text, tbMolde1.Text) && Operario1Numero.Text != "---")
                    {
                        conexion.insertar_validacion_operario(Operario1Numero.Text, tbMolde1.Text);
                        string ALoperario = Operario1Nombre.Text;
                        string AlCambios = RazonMod.Text + " (" + tbfechaMod.Text + ")";
                        AlertaDOC1.Visible = true;
                        AlertaDOCTEXT1.Text = ALoperario + " hay documentación nueva disponible para su consulta. Por favor, revísala antes de continuar con la producción. CAMBIOS: " + AlCambios + "";
                       }
                    else
                    {
                        if (conexion.existe_validacion_operario_enfecha(Operario1Numero.Text, tbMolde1.Text, tbfechaMod.Text))
                        {
                            conexion.resetea_alarma_validacion_operario(Operario1Numero.Text, tbMolde1.Text);
                            string ALoperario = Operario1Nombre.Text;
                            string AlCambios = RazonMod.Text + " (" + tbfechaMod.Text + ")";
                            AlertaDOC1.Visible = true;

                            AlertaDOCTEXT1.Text = ALoperario + " hay documentación nueva disponible para su consulta. Por favor, revísala antes de continuar con la producción. CAMBIOS: " + AlCambios + "";
}
                        else
                        {
                            AlertaDOC1.Visible = false;


                        }
                    }

                }

            }
            catch (Exception)
            { }

        }

        public void AbrirScriptDocumentacion(Object sender, EventArgs e)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "VerificarLectura();", true);
            }
        public void CerrarAvisoDocumentacion(Object sender, EventArgs e)
        {
            try
            {
                Conexion_DOCUMENTAL conexion = new Conexion_DOCUMENTAL();
                conexion.actualizar_validacion_operario(Operario1Numero.Text, tbMolde1.Text);
                AlertaDOC1.Visible = false;

            }
            catch (Exception)
            { }
        }

        public void RedireccionaDocumento(Object sender, EventArgs e)
        {
            try
            {
                HtmlButton button = (HtmlButton)sender;
                string URL = "";
                string name = button.ID;
                switch (name)
                {
                    case "DOC1":
                        URL = "http://facts4-srv/thermogestion/DOCUMENTAL/FichaReferencia.aspx?REFERENCIA=" + tbReferencia1.Text;
                        Response.Write("<script language='javascript'> window.open('" + URL + "', 'window','HEIGHT=600,WIDTH=820,top=50,left=50,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
                        break;
                    case "LIB1":
                        URL = "http://facts4-srv/thermogestion/LIBERACIONES/LiberacionSerie.aspx?ORDEN=" + tbOrden1.Text;
                        Response.Write("<script language='javascript'> window.open('" + URL + "', 'window','HEIGHT=600,WIDTH=820,top=50,left=50,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
                        break;
                    case "MUR1":
                        URL = "http://facts4-srv/thermogestion/GP12/GP12HistoricoReferencia.aspx?REFERENCIA=" + tbReferencia1.Text;
                        Response.Write("<script language='javascript'> window.open('" + URL + "', 'window','HEIGHT=600,WIDTH=820,top=50,left=50,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
                        break;
                    case "FAB1":
                        URL = "http://facts4-srv/thermogestion/FichasParametros.aspx?REFERENCIA="+ tbReferencia1.Text + "&MAQUINA=" + tbMaquina1.Text;
                        Response.Write("<script language='javascript'> window.open('" + URL + "', 'window','HEIGHT=600,WIDTH=820,top=50,left=50,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
                        break;
                    case "DOC2":
                        URL = "http://facts4-srv/thermogestion/DOCUMENTAL/FichaReferencia.aspx?REFERENCIA=" + tbReferencia2.Text;
                        Response.Write("<script language='javascript'> window.open('" + URL + "', 'window','HEIGHT=600,WIDTH=820,top=50,left=50,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
                        break;
                    case "LIB2":
                        URL = "http://facts4-srv/thermogestion/LIBERACIONES/LiberacionSerie.aspx?ORDEN=" + tbOrden2.Text;
                        Response.Write("<script language='javascript'> window.open('" + URL + "', 'window','HEIGHT=600,WIDTH=820,top=50,left=50,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
                        break;
                    case "MUR2":
                        URL = "http://facts4-srv/thermogestion/GP12/GP12HistoricoReferencia.aspx?REFERENCIA=" + tbReferencia2.Text;
                        Response.Write("<script language='javascript'> window.open('" + URL + "', 'window','HEIGHT=600,WIDTH=820,top=50,left=50,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
                        break;
                    case "FAB2":
                        URL = "http://facts4-srv/thermogestion/FichasParametros.aspx?REFERENCIA=" + tbReferencia2.Text + "&MAQUINA=" + tbMaquina2.Text;
                        Response.Write("<script language='javascript'> window.open('" + URL + "', 'window','HEIGHT=600,WIDTH=820,top=50,left=50,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
                        break;

                    case "DOC3":
                        URL = "http://facts4-srv/thermogestion/DOCUMENTAL/FichaReferencia.aspx?REFERENCIA=" + tbReferencia3.Text;
                        Response.Write("<script language='javascript'> window.open('" + URL + "', 'window','HEIGHT=600,WIDTH=820,top=50,left=50,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
                        break;
                    case "LIB3":
                        URL = "http://facts4-srv/thermogestion/LIBERACIONES/LiberacionSerie.aspx?ORDEN=" + tbOrden3.Text;
                        Response.Write("<script language='javascript'> window.open('" + URL + "', 'window','HEIGHT=600,WIDTH=820,top=50,left=50,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
                        break;
                    case "MUR3":
                        URL = "http://facts4-srv/thermogestion/GP12/GP12HistoricoReferencia.aspx?REFERENCIA=" + tbReferencia3.Text;
                        Response.Write("<script language='javascript'> window.open('" + URL + "', 'window','HEIGHT=600,WIDTH=820,top=50,left=50,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
                        break;
                    case "FAB3":
                        URL = "http://facts4-srv/thermogestion/FichasParametros.aspx?REFERENCIA=" + tbReferencia3.Text + "&MAQUINA=" + tbMaquina3.Text;
                        Response.Write("<script language='javascript'> window.open('" + URL + "', 'window','HEIGHT=600,WIDTH=820,top=50,left=50,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
                        break;

                    case "DOC4":
                        URL = "http://facts4-srv/thermogestion/DOCUMENTAL/FichaReferencia.aspx?REFERENCIA=" + tbReferencia4.Text;
                        Response.Write("<script language='javascript'> window.open('" + URL + "', 'window','HEIGHT=600,WIDTH=820,top=50,left=50,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
                        break;
                    case "LIB4":
                        URL = "http://facts4-srv/thermogestion/LIBERACIONES/LiberacionSerie.aspx?ORDEN=" + tbOrden4.Text;
                        Response.Write("<script language='javascript'> window.open('" + URL + "', 'window','HEIGHT=600,WIDTH=820,top=50,left=50,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
                        break;
                    case "MUR4":
                        URL = "http://facts4-srv/thermogestion/GP12/GP12HistoricoReferencia.aspx?REFERENCIA=" + tbReferencia4.Text;
                        Response.Write("<script language='javascript'> window.open('" + URL + "', 'window','HEIGHT=600,WIDTH=820,top=50,left=50,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
                        break;
                    case "FAB4":
                        URL = "http://facts4-srv/thermogestion/FichasParametros.aspx?REFERENCIA=" + tbReferencia4.Text + "&MAQUINA=" + tbMaquina4.Text;
                        Response.Write("<script language='javascript'> window.open('" + URL + "', 'window','HEIGHT=600,WIDTH=820,top=50,left=50,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
                        break;
                }
            }
            catch (Exception)
            {
            }
        }

        public void InsertarFeedbackDocumental(Object sender, EventArgs e)
        {
            try
            {
                if (tbDenunciaError.Text != "" && tbMolde1.Text != "")
                {
                    string OP1 = "0";
                    string OP2 = "0";
                    if (Operario1Numero.Text != "---")
                        {
                        OP1 = Operario1Numero.Text;
                    }
                    if (Operario2Numero.Text != "---")
                    {
                        OP2 = Operario2Numero.Text;
                    }

                    Conexion_DOCUMENTAL conexion = new Conexion_DOCUMENTAL();
                    conexion.insertar_feedback_operario(OP1, OP2, tbMolde1.Text, tbDenunciaError.Text);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "guardado_feedback_OK();", true);
                }
            }
            catch (Exception)
            { }
        }


        protected void lanzaPostback(object sender, EventArgs e)
        {
            Response.Redirect(url: "http://facts4-srv/thermogestion/DOCUMENTAL/DocumentosPlanta.aspx?MAQUINA=" + tbMaquina1.Text);
        }
    
    }

}