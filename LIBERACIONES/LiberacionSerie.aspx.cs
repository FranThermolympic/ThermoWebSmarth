using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Web.Script.Services;
using Excel = Microsoft.Office.Interop.Excel;

using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using System.Net.Mail;
using System.Net;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Extensions;
using OfficeOpenXml;



namespace ThermoWeb.LIBERACIONES
{
    public partial class LiberacionSerie : System.Web.UI.Page
    {
        //private string selectedValueMaquina = "";
        //private int maquina = 0;
        //private string orden = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDrops();
                if (Request.QueryString["ORDEN"] != null)
                {
                   
                    CargarCabecera();
                    Conexion_LIBERACIONES conexion = new Conexion_LIBERACIONES();
                    DataSet existeorden = conexion.Consulta_existencia_liberacion(tbOrden.Text);
                    if (existeorden.Tables[0].Rows.Count > 0)
                    {
                        CargarFichaLiberacion();
                        CargarParametrosLiberacion();
                        CargarCuestionariosLiberacion();
                        CargarMateriasPrimasLiberacion();
                        PintarCeldasDevacion();
                        CargarResultados();
                        CargarMantenimiento();
                    }
                    else
                    {
                        CargarParametros();
                        CargarMateriasPrimas();
                        //CargarTrabajadores(); prueba
                        CargarMantenimiento();
                        CrearLiberacion();
                    }
                }
            }

        }
        public void CargarDrops() //revisar y retirar
        {
            Conexion_LIBERACIONES conexion = new Conexion_LIBERACIONES();
            
            DataSet responsable = conexion.Devuelve_setlista_responsablesSMARTH();
            foreach (DataRow row in responsable.Tables[0].Rows) { DropValidadJefeProyecto.Items.Add(row["PAprobado"].ToString()); }
            DropValidadJefeProyecto.ClearSelection();
            DropValidadJefeProyecto.SelectedValue = "";

        }

        //PRIMERA CARGA Y ESCRITURA EN BASE DE DATOS
        public void CargarCabecera()
        {
            Conexion_LIBERACIONES conexion = new Conexion_LIBERACIONES();
          

            try
            {
                
                DataSet ds = conexion.devuelve_detalles_orden(Request.QueryString["ORDEN"]);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    tbMaquina.Text = ds.Tables[0].Rows[0]["C_MACHINE_ID"].ToString();
                    tbReferencia.Text = ds.Tables[0].Rows[0]["C_PRODUCT_ID"].ToString();
                    tbNombre.Text = ds.Tables[0].Rows[0]["C_PRODLONGDESCR"].ToString();
                    tbOrden.Text = ds.Tables[0].Rows[0]["C_ID"].ToString();
                    tbMolde.Text = ds.Tables[0].Rows[0]["C_TOOL_ID"].ToString();
                    tbFechaCambio.Text = ds.Tables[0].Rows[0]["C_ACTSTARTDATE"].ToString();
                

                DataSet Hijos = conexion.devuelve_hijos_orden(Request.QueryString["ORDEN"]);
                    if (Hijos.Tables[0].Rows.Count > 0)
                    {
                        tbOrden2.Text = Hijos.Tables[0].Rows[0]["C_ID"].ToString();
                        tbReferencia2.Text = Hijos.Tables[0].Rows[0]["C_PRODUCT_ID"].ToString();
                        tbNombre2.Text = Hijos.Tables[0].Rows[0]["C_LONG_DESCR"].ToString();
                        tbOrden2.Visible = true;
                        tbReferencia2.Visible = true;
                        tbNombre2.Visible = true;
                        tbOrdenTitulo2.Visible = true;
                        tbReferenciaTitulo2.Visible = true;

                    }
                    if (Hijos.Tables[0].Rows.Count > 1)
                    {
                        tbOrden3.Text = Hijos.Tables[0].Rows[1]["C_ID"].ToString();
                        tbReferencia3.Text = Hijos.Tables[0].Rows[1]["C_PRODUCT_ID"].ToString();
                        tbNombre3.Text = Hijos.Tables[0].Rows[1]["C_LONG_DESCR"].ToString();
                        tbOrden3.Visible = true;
                        tbReferencia3.Visible = true;
                        tbNombre3.Visible = true;
                        tbOrdenTitulo3.Visible = true;
                        tbReferenciaTitulo3.Visible = true;

                    }
                    if (Hijos.Tables[0].Rows.Count > 2)
                    {

                        tbOrden4.Text = Hijos.Tables[0].Rows[2]["C_ID"].ToString();
                        tbReferencia4.Text = Hijos.Tables[0].Rows[2]["C_PRODUCT_ID"].ToString();
                        tbNombre4.Text = Hijos.Tables[0].Rows[2]["C_LONG_DESCR"].ToString();
                        tbOrden4.Visible = true;
                        tbReferencia4.Visible = true;
                        tbNombre4.Visible = true;
                        tbOrdenTitulo4.Visible = true;
                        tbReferenciaTitulo4.Visible = true;

                    }
                }
                else
                { 
                    ds = conexion.devuelve_detalles_orden_HIST(Request.QueryString["ORDEN"]);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                    tbMaquina.Text = ds.Tables[0].Rows[0]["Maquina"].ToString();
                    tbReferencia.Text = ds.Tables[0].Rows[0]["Referencia"].ToString();
                    tbNombre.Text = ds.Tables[0].Rows[0]["Descripcion"].ToString();
                    tbOrden.Text = ds.Tables[0].Rows[0]["Orden"].ToString();
                    tbMolde.Text = ds.Tables[0].Rows[0]["CodMolde"].ToString();
                    tbFechaCambio.Text = ds.Tables[0].Rows[0]["FechaApertura"].ToString();
                    }

                    DataSet Hijos = conexion.devuelve_hijos_orden(Request.QueryString["ORDEN"]);
                    if (Hijos.Tables[0].Rows.Count > 0)
                    {
                        tbOrden2.Text = Hijos.Tables[0].Rows[0]["C_ID"].ToString();
                        tbReferencia2.Text = Hijos.Tables[0].Rows[0]["C_PRODUCT_ID"].ToString();
                        tbNombre2.Text = Hijos.Tables[0].Rows[0]["C_LONG_DESCR"].ToString();
                        tbOrden2.Visible = true;
                        tbReferencia2.Visible = true;
                        tbNombre2.Visible = true;
                        tbOrdenTitulo2.Visible = true;
                        tbReferenciaTitulo2.Visible = true;
                    }
                    if (Hijos.Tables[0].Rows.Count > 1)
                    {
                        tbOrden3.Text = Hijos.Tables[0].Rows[1]["C_ID"].ToString();
                        tbReferencia3.Text = Hijos.Tables[0].Rows[1]["C_PRODUCT_ID"].ToString();
                        tbNombre3.Text = Hijos.Tables[0].Rows[1]["C_LONG_DESCR"].ToString();
                        tbOrden3.Visible = true;
                        tbReferencia3.Visible = true;
                        tbNombre3.Visible = true;
                        tbOrdenTitulo3.Visible = true;
                        tbReferenciaTitulo3.Visible = true;
                    }
                    if (Hijos.Tables[0].Rows.Count > 2)
                    {

                        tbOrden4.Text = Hijos.Tables[0].Rows[2]["C_ID"].ToString();
                        tbReferencia4.Text = Hijos.Tables[0].Rows[2]["C_PRODUCT_ID"].ToString();
                        tbNombre4.Text = Hijos.Tables[0].Rows[2]["C_LONG_DESCR"].ToString();
                        tbOrden4.Visible = true;
                        tbReferencia4.Visible = true;
                        tbNombre4.Visible = true;
                        tbOrdenTitulo4.Visible = true;
                        tbReferenciaTitulo4.Visible = true;

                    }
                }

                //CambiadorHoras.Text = cambiador.Tables[0].Rows[0]["C_MACHINE_ID"].ToString(); 

            }
            catch (Exception)
            { }
        }
        public void CargarTrabajadores()
        {
            try
            {
                Conexion_LIBERACIONES conexion = new Conexion_LIBERACIONES();
                DataSet calidadplanta = conexion.devuelve_calidadplanta_logueadoXMaquina(tbMaquina.Text);
                if (calidadplanta.Tables[0].Rows.Count > 0)
                {
                    CalidadNumero.Text = calidadplanta.Tables[0].Rows[0]["C_CLOCKNO"].ToString();
                    CalidadNombre.Text = calidadplanta.Tables[0].Rows[0]["C_NAME"].ToString();
                    calidadplanta = conexion.devuelve_horasXReferenciaXOperario(Convert.ToInt32(tbMolde.Text), Convert.ToInt32(CalidadNumero.Text));
                    if (calidadplanta.Tables[0].Rows.Count > 0)
                    { CalidadHoras.Text = calidadplanta.Tables[0].Rows[0]["TIEMPOHORAS"].ToString(); }
                    else
                    { CalidadHoras.Text = "0"; }
                }

                DataSet encargado = conexion.devuelve_encargado_logueadoXMaquina(tbMaquina.Text);
                if (encargado.Tables[0].Rows.Count > 0)
                {
                    EncargadoNumero.Text = encargado.Tables[0].Rows[0]["C_CLOCKNO"].ToString();
                    EncargadoNombre.Text = encargado.Tables[0].Rows[0]["C_NAME"].ToString();
                    encargado = conexion.devuelve_horasXReferenciaXOperario(Convert.ToInt32(tbMolde.Text), Convert.ToInt32(EncargadoNumero.Text));
                    if (encargado.Tables[0].Rows.Count > 0)
                    { EncargadoHoras.Text = encargado.Tables[0].Rows[0]["TIEMPOHORAS"].ToString(); }
                    else
                    { EncargadoHoras.Text = "0"; }
                }

                DataSet operario = conexion.devuelve_operario_logueadoXMaquina(tbMaquina.Text);
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
                        Operario2UltRevision.Visible = true;
                        Operario2Numero.Visible = true;
                        Operario2Notas.Visible = true;
                    }

                    operario = conexion.devuelve_horasXReferenciaXOperario(Convert.ToInt32(tbMolde.Text), Convert.ToInt32(Operario1Numero.Text));
                    if (operario.Tables[0].Rows.Count > 0)
                    { Operario1Horas.Text = operario.Tables[0].Rows[0]["TIEMPOHORAS"].ToString();
                        //compruebo horas y asigno valor (revincular a aplicación)
                        double DoubleOperarioHoras = Convert.ToDouble(Operario1Horas.Text);
                        if (DoubleOperarioHoras < 10)
                        { Operario1Nivel.SelectedValue = "I";
                            alertaoperario.Visible = true; }
                        if (DoubleOperarioHoras > 10 && DoubleOperarioHoras < 80)
                        { Operario1Nivel.SelectedValue = "L"; }
                        if (DoubleOperarioHoras > 80)
                        { Operario1Nivel.SelectedValue = "U"; }
                    }
                    else
                    {
                        Operario1Horas.Text = "0";
                        alertaoperario.Visible = true; }

                    if (Operario2Posicion.Visible == true)
                    {
                        DataSet Operario2 = conexion.devuelve_horasXReferenciaXOperario(Convert.ToInt32(tbMolde.Text), Convert.ToInt32(Operario2Numero.Text));
                        if (Operario2.Tables[0].Rows.Count > 0)
                        {
                            Operario2Horas.Text = Operario2.Tables[0].Rows[0]["TIEMPOHORAS"].ToString();
                            double DoubleOperario2Horas = Convert.ToDouble(Operario2Horas.Text);
                            if (DoubleOperario2Horas < 10)
                            {
                                Operario2Nivel.SelectedValue = "I";
                                alertaoperario.Visible = true;
                            }
                            if (DoubleOperario2Horas > 10 && DoubleOperario2Horas < 80)
                            { Operario2Nivel.SelectedValue = "L"; }
                            if (DoubleOperario2Horas > 80)
                            { Operario2Nivel.SelectedValue = "U"; }
                        }
                        else
                        { Operario2Horas.Text = "0"; }
                    }
                }

                DataSet cambiador = conexion.devuelve_cambiador_logueadoXMaquina(tbMaquina.Text);
                if (cambiador.Tables[0].Rows.Count > 0)
                {
                    CambiadorNumero.Text = cambiador.Tables[0].Rows[0]["C_CLOCKNO"].ToString();
                    CambiadorNombre.Text = cambiador.Tables[0].Rows[0]["C_NAME"].ToString();
                    cambiador = conexion.devuelve_horasXReferenciaXOperario(Convert.ToInt32(tbMolde.Text), Convert.ToInt32(CambiadorNumero.Text));
                    if (cambiador.Tables[0].Rows.Count > 0)
                    { CambiadorHoras.Text = cambiador.Tables[0].Rows[0]["TIEMPOHORAS"].ToString(); }
                    else
                    { CambiadorHoras.Text = "0"; }
                }
            }
            catch (Exception)
            { }

        }
        public void CargarParametros()
        {
            try
            {
                Conexion_LIBERACIONES conexion = new Conexion_LIBERACIONES();


                DataSet ds = conexion.leerFicha(Convert.ToInt32(tbReferencia.Text), Convert.ToInt32(tbMaquina.Text));

                if (ds.Tables[0].Rows.Count > 0) //COMPRUEBO SI EXISTE ALGUNA FICHA CON EL VALOR DE REFERENCIA, SI EXISTE LA CARGO
                {
                    EXISTEFICHA.Text = "0";
                    thVCarga.Text = ds.Tables[0].Rows[0]["VelocidadCarga"].ToString();
                    thCarga.Text = ds.Tables[0].Rows[0]["Carga"].ToString();
                    thDescomp.Text = ds.Tables[0].Rows[0]["Descompresion"].ToString();
                    thContrapr.Text = ds.Tables[0].Rows[0]["Contrapresion"].ToString();
                    thTiempo.Text = ds.Tables[0].Rows[0]["Tiempo"].ToString();
                    thEnfriamiento.Text = ds.Tables[0].Rows[0]["Enfriamiento"].ToString();
                    thCiclo.Text = ds.Tables[0].Rows[0]["Ciclo"].ToString();
                    thCojin.Text = ds.Tables[0].Rows[0]["Cojin"].ToString();
                    //cbEdicion.Text = ds.Tables[0].Rows[0]["Version"].ToString();
                    //cbFecha.Text = ds.Tables[0].Rows[0]["Fecha"].ToString();

                    ds = conexion.leerTempCilindro(Convert.ToInt32(tbReferencia.Text), Convert.ToInt32(tbMaquina.Text));
                    thBoq.Text = ds.Tables[0].Rows[0]["Boq"].ToString();
                    thT1.Text = ds.Tables[0].Rows[0]["T1"].ToString();
                    thT2.Text = ds.Tables[0].Rows[0]["T2"].ToString();
                    thT3.Text = ds.Tables[0].Rows[0]["T3"].ToString();
                    thT4.Text = ds.Tables[0].Rows[0]["T4"].ToString();
                    thT5.Text = ds.Tables[0].Rows[0]["T5"].ToString();
                    thT6.Text = ds.Tables[0].Rows[0]["T6"].ToString();
                    thT7.Text = ds.Tables[0].Rows[0]["T7"].ToString();
                    thT8.Text = ds.Tables[0].Rows[0]["T8"].ToString();
                    thT9.Text = ds.Tables[0].Rows[0]["T9"].ToString();
                    thT10.Text = ds.Tables[0].Rows[0]["T10"].ToString();

                    ds = conexion.leerTempCamCaliente(Convert.ToInt32(tbReferencia.Text), Convert.ToInt32(tbMaquina.Text));
                    thZ1.Text = ds.Tables[0].Rows[0]["Z1"].ToString();
                    thZ2.Text = ds.Tables[0].Rows[0]["Z2"].ToString();
                    thZ3.Text = ds.Tables[0].Rows[0]["Z3"].ToString();
                    thZ4.Text = ds.Tables[0].Rows[0]["Z4"].ToString();
                    thZ5.Text = ds.Tables[0].Rows[0]["Z5"].ToString();
                    thZ6.Text = ds.Tables[0].Rows[0]["Z6"].ToString();
                    thZ7.Text = ds.Tables[0].Rows[0]["Z7"].ToString();
                    thZ8.Text = ds.Tables[0].Rows[0]["Z8"].ToString();
                    thZ9.Text = ds.Tables[0].Rows[0]["Z9"].ToString();
                    thZ10.Text = ds.Tables[0].Rows[0]["Z10"].ToString();
                    thZ11.Text = ds.Tables[0].Rows[0]["Z11"].ToString();
                    thZ12.Text = ds.Tables[0].Rows[0]["Z12"].ToString();
                    thZ13.Text = ds.Tables[0].Rows[0]["Z13"].ToString();
                    thZ14.Text = ds.Tables[0].Rows[0]["Z14"].ToString();
                    thZ15.Text = ds.Tables[0].Rows[0]["Z15"].ToString();
                    thZ16.Text = ds.Tables[0].Rows[0]["Z16"].ToString();
                    thZ17.Text = ds.Tables[0].Rows[0]["Z17"].ToString();
                    thZ18.Text = ds.Tables[0].Rows[0]["Z18"].ToString();
                    thZ19.Text = ds.Tables[0].Rows[0]["Z19"].ToString();
                    thZ20.Text = ds.Tables[0].Rows[0]["Z20"].ToString();

                    ds = conexion.leerInyeccion(Convert.ToInt32(tbReferencia.Text), Convert.ToInt32(tbMaquina.Text));
                    tbTiempoInyeccion.Text = ds.Tables[0].Rows[0]["Tiempo"].ToString();
                    tbLimitePresion.Text = ds.Tables[0].Rows[0]["LimitePresion"].ToString();

                    ds = conexion.leerPostpresion(Convert.ToInt32(tbReferencia.Text), Convert.ToInt32(tbMaquina.Text));
                    thP1.Text = ds.Tables[0].Rows[0]["P1"].ToString();
                    thP2.Text = ds.Tables[0].Rows[0]["P2"].ToString();
                    thP3.Text = ds.Tables[0].Rows[0]["P3"].ToString();
                    thP4.Text = ds.Tables[0].Rows[0]["P4"].ToString();
                    thP5.Text = ds.Tables[0].Rows[0]["P5"].ToString();
                    thP6.Text = ds.Tables[0].Rows[0]["P6"].ToString();
                    thP7.Text = ds.Tables[0].Rows[0]["P7"].ToString();
                    thP8.Text = ds.Tables[0].Rows[0]["P8"].ToString();
                    thP9.Text = ds.Tables[0].Rows[0]["P9"].ToString();
                    thP10.Text = ds.Tables[0].Rows[0]["P10"].ToString();
                    thTP1.Text = ds.Tables[0].Rows[0]["T1"].ToString();
                    thTP2.Text = ds.Tables[0].Rows[0]["T2"].ToString();
                    thTP3.Text = ds.Tables[0].Rows[0]["T3"].ToString();
                    thTP4.Text = ds.Tables[0].Rows[0]["T4"].ToString();
                    thTP5.Text = ds.Tables[0].Rows[0]["T5"].ToString();
                    thTP6.Text = ds.Tables[0].Rows[0]["T6"].ToString();
                    thTP7.Text = ds.Tables[0].Rows[0]["T7"].ToString();
                    thTP8.Text = ds.Tables[0].Rows[0]["T8"].ToString();
                    thTP9.Text = ds.Tables[0].Rows[0]["T9"].ToString();
                    thTP10.Text = ds.Tables[0].Rows[0]["T10"].ToString();
                    tbConmutacion.Text = ds.Tables[0].Rows[0]["Conmutacion"].ToString();
                    tbTiempoPresion.Text = ds.Tables[0].Rows[0]["TiempoPresion"].ToString();

                    //CONSULTA ATEMPERADO

                    ds = conexion.leerAtemperado(Convert.ToInt32(tbReferencia.Text), Convert.ToInt32(tbMaquina.Text));

                    TbCircuitoM1.Text = ds.Tables[0].Rows[0]["CircuitoM1"].ToString();
                    TbCircuitoM2.Text = ds.Tables[0].Rows[0]["CircuitoM2"].ToString();
                    TbCircuitoM3.Text = ds.Tables[0].Rows[0]["CircuitoM3"].ToString();
                    TbCircuitoM4.Text = ds.Tables[0].Rows[0]["CircuitoM4"].ToString();
                    TbCircuitoM5.Text = ds.Tables[0].Rows[0]["CircuitoM5"].ToString();
                    TbCircuitoM6.Text = ds.Tables[0].Rows[0]["CircuitoM6"].ToString();

                    //Convertir valores a string Movil
                    if (TbCircuitoM1.Text != "1")
                    { TbCircuitoM1.Text = conexion.devuelve_tipo_atemperado(Convert.ToInt16(TbCircuitoM1.Text)).ToString(); }
                    else { TbCircuitoM1.Text = ""; }

                    if (TbCircuitoM2.Text != "1")
                    { TbCircuitoM2.Text = conexion.devuelve_tipo_atemperado(Convert.ToInt16(TbCircuitoM2.Text)).ToString(); }
                    else { TbCircuitoM2.Text = ""; }

                    if (TbCircuitoM3.Text != "1")
                    { TbCircuitoM3.Text = conexion.devuelve_tipo_atemperado(Convert.ToInt16(TbCircuitoM3.Text)).ToString(); }
                    else { TbCircuitoM3.Text = ""; }

                    if (TbCircuitoM4.Text != "1")
                    { TbCircuitoM4.Text = conexion.devuelve_tipo_atemperado(Convert.ToInt16(TbCircuitoM4.Text)).ToString(); }
                    else { TbCircuitoM4.Text = ""; }

                    if (TbCircuitoM5.Text != "1")
                    { TbCircuitoM5.Text = conexion.devuelve_tipo_atemperado(Convert.ToInt16(TbCircuitoM5.Text)).ToString(); }
                    else { TbCircuitoM5.Text = ""; }

                    if (TbCircuitoM6.Text != "1")
                    { TbCircuitoM6.Text = conexion.devuelve_tipo_atemperado(Convert.ToInt16(TbCircuitoM6.Text)).ToString(); }
                    else { TbCircuitoM6.Text = ""; }


                    TbCircuitoF1.Text = ds.Tables[0].Rows[0]["CircuitoF1"].ToString();
                    TbCircuitoF2.Text = ds.Tables[0].Rows[0]["CircuitoF2"].ToString();
                    TbCircuitoF3.Text = ds.Tables[0].Rows[0]["CircuitoF3"].ToString();
                    TbCircuitoF4.Text = ds.Tables[0].Rows[0]["CircuitoF4"].ToString();
                    TbCircuitoF5.Text = ds.Tables[0].Rows[0]["CircuitoF5"].ToString();
                    TbCircuitoF6.Text = ds.Tables[0].Rows[0]["CircuitoF6"].ToString();

                    //Convertir valores a string fijo
                    if (TbCircuitoF1.Text != "1")
                    { TbCircuitoF1.Text = conexion.devuelve_tipo_atemperado(Convert.ToInt16(TbCircuitoF1.Text)).ToString(); }
                    else { TbCircuitoF1.Text = ""; }

                    if (TbCircuitoF2.Text != "1")
                    { TbCircuitoF2.Text = conexion.devuelve_tipo_atemperado(Convert.ToInt16(TbCircuitoF2.Text)).ToString(); }
                    else { TbCircuitoF2.Text = ""; }

                    if (TbCircuitoF3.Text != "1")
                    { TbCircuitoF3.Text = conexion.devuelve_tipo_atemperado(Convert.ToInt16(TbCircuitoF3.Text)).ToString(); }
                    else { TbCircuitoF3.Text = ""; }

                    if (TbCircuitoF4.Text != "1")
                    { TbCircuitoF4.Text = conexion.devuelve_tipo_atemperado(Convert.ToInt16(TbCircuitoF4.Text)).ToString(); }
                    else { TbCircuitoF4.Text = ""; }

                    if (TbCircuitoF5.Text != "1")
                    { TbCircuitoF5.Text = conexion.devuelve_tipo_atemperado(Convert.ToInt16(TbCircuitoF5.Text)).ToString(); }
                    else { TbCircuitoF5.Text = ""; }

                    if (TbCircuitoF6.Text != "1")
                    { TbCircuitoF6.Text = conexion.devuelve_tipo_atemperado(Convert.ToInt16(TbCircuitoF6.Text)).ToString(); }
                    else { TbCircuitoF6.Text = ""; }
                    //CAUDALES
                    //PARTE FIJA
                    TbCaudalF1.Text = ds.Tables[0].Rows[0]["CaudalF1"].ToString();
                    TbCaudalF2.Text = ds.Tables[0].Rows[0]["CaudalF2"].ToString();
                    TbCaudalF3.Text = ds.Tables[0].Rows[0]["CaudalF3"].ToString();
                    TbCaudalF4.Text = ds.Tables[0].Rows[0]["CaudalF4"].ToString();
                    TbCaudalF5.Text = ds.Tables[0].Rows[0]["CaudalF5"].ToString();
                    TbCaudalF6.Text = ds.Tables[0].Rows[0]["CaudalF6"].ToString();

                    //PARTE MOVIL
                    TbCaudalM1.Text = ds.Tables[0].Rows[0]["CaudalM1"].ToString();
                    TbCaudalM2.Text = ds.Tables[0].Rows[0]["CaudalM2"].ToString();
                    TbCaudalM3.Text = ds.Tables[0].Rows[0]["CaudalM3"].ToString();
                    TbCaudalM4.Text = ds.Tables[0].Rows[0]["CaudalM4"].ToString();
                    TbCaudalM5.Text = ds.Tables[0].Rows[0]["CaudalM5"].ToString();
                    TbCaudalM6.Text = ds.Tables[0].Rows[0]["CaudalM6"].ToString();

                    //TEMPERATURAS
                    //PARTE FIJA
                    TbTemperaturaF1.Text = ds.Tables[0].Rows[0]["TemperaturaF1"].ToString();
                    TbTemperaturaF2.Text = ds.Tables[0].Rows[0]["TemperaturaF2"].ToString();
                    TbTemperaturaF3.Text = ds.Tables[0].Rows[0]["TemperaturaF3"].ToString();
                    TbTemperaturaF4.Text = ds.Tables[0].Rows[0]["TemperaturaF4"].ToString();
                    TbTemperaturaF5.Text = ds.Tables[0].Rows[0]["TemperaturaF5"].ToString();
                    TbTemperaturaF6.Text = ds.Tables[0].Rows[0]["TemperaturaF6"].ToString();
                    //PARTE MOVIL
                    TbTemperaturaM1.Text = ds.Tables[0].Rows[0]["TemperaturaM1"].ToString();
                    TbTemperaturaM2.Text = ds.Tables[0].Rows[0]["TemperaturaM2"].ToString();
                    TbTemperaturaM3.Text = ds.Tables[0].Rows[0]["TemperaturaM3"].ToString();
                    TbTemperaturaM4.Text = ds.Tables[0].Rows[0]["TemperaturaM4"].ToString();
                    TbTemperaturaM5.Text = ds.Tables[0].Rows[0]["TemperaturaM5"].ToString();
                    TbTemperaturaM6.Text = ds.Tables[0].Rows[0]["TemperaturaM6"].ToString();

                    //LEER TOLERANCIAS
                    ds = conexion.leerTolerancias(Convert.ToInt32(tbReferencia.Text), Convert.ToInt32(tbMaquina.Text));
                    tbTiempoInyeccionNVal.Text = ds.Tables[0].Rows[0]["TiempoInyeccionNVal"].ToString();
                    tbTiempoInyeccionMVal.Text = ds.Tables[0].Rows[0]["TiempoInyeccionMVal"].ToString();
                    tbLimitePresionNVal.Text = ds.Tables[0].Rows[0]["LimitePresionNVal"].ToString();
                    tbLimitePresionMVal.Text = ds.Tables[0].Rows[0]["LimitePresionMVal"].ToString();
                    thConmuntaciontolNVal.Text = ds.Tables[0].Rows[0]["ConmuntaciontolNVal"].ToString();
                    thConmuntaciontolMVal.Text = ds.Tables[0].Rows[0]["ConmuntaciontolMVal"].ToString();
                    tbTiempoPresiontolNVal.Text = ds.Tables[0].Rows[0]["TiempoPresiontolNVal"].ToString();
                    tbTiempoPresiontolMVal.Text = ds.Tables[0].Rows[0]["TiempoPresiontolMVal"].ToString();
                    TNvcargaval.Text = ds.Tables[0].Rows[0]["TNvcargaval"].ToString();
                    TMvcargaval.Text = ds.Tables[0].Rows[0]["TMvcargaval"].ToString();
                    TNcargaval.Text = ds.Tables[0].Rows[0]["TNcargaval"].ToString();
                    TMcargaval.Text = ds.Tables[0].Rows[0]["TMcargaval"].ToString();
                    TNdescomval.Text = ds.Tables[0].Rows[0]["TNdescomval"].ToString();
                    TMdescomval.Text = ds.Tables[0].Rows[0]["TMdescomval"].ToString();
                    TNcontrapval.Text = ds.Tables[0].Rows[0]["TNcontrapval"].ToString();
                    TMcontrapval.Text = ds.Tables[0].Rows[0]["TMcontrapval"].ToString();
                    TNTiempdosval.Text = ds.Tables[0].Rows[0]["TNTiempdosval"].ToString();
                    TMTiempdosval.Text = ds.Tables[0].Rows[0]["TMTiempdosval"].ToString();
                    TNEnfriamval.Text = ds.Tables[0].Rows[0]["TNEnfriamval"].ToString();
                    TMEnfriamval.Text = ds.Tables[0].Rows[0]["TMEnfriamval"].ToString();
                    TNCicloval.Text = ds.Tables[0].Rows[0]["TNCicloval"].ToString();
                    TMCicloval.Text = ds.Tables[0].Rows[0]["TMCicloval"].ToString();
                    TNCojinval.Text = ds.Tables[0].Rows[0]["TNCojinval"].ToString();
                    TMCojinval.Text = ds.Tables[0].Rows[0]["TMCojinval"].ToString();
                }
                else //SI NO EXISTE FICHA CON REFERENCIA, CARGO GEMELO A TRAVÉS DE MOLDE
                {
                    EXISTEFICHA.Text = "0";
                    ds = conexion.leerFichaMOLDE(Convert.ToInt32(tbMolde.Text), Convert.ToInt32(tbMaquina.Text));
                    //tbCliente.Text = ds.Tables[0].Rows[0]["Cliente"].ToString();
                    thVCarga.Text = ds.Tables[0].Rows[0]["VelocidadCarga"].ToString();
                    thCarga.Text = ds.Tables[0].Rows[0]["Carga"].ToString();
                    thDescomp.Text = ds.Tables[0].Rows[0]["Descompresion"].ToString();
                    thContrapr.Text = ds.Tables[0].Rows[0]["Contrapresion"].ToString();
                    thTiempo.Text = ds.Tables[0].Rows[0]["Tiempo"].ToString();
                    thEnfriamiento.Text = ds.Tables[0].Rows[0]["Enfriamiento"].ToString();
                    thCiclo.Text = ds.Tables[0].Rows[0]["Ciclo"].ToString();
                    thCojin.Text = ds.Tables[0].Rows[0]["Cojin"].ToString();
                    //cbEdicion.Text = ds.Tables[0].Rows[0]["Version"].ToString();
                    //cbFecha.Text = ds.Tables[0].Rows[0]["Fecha"].ToString();

                    ds = conexion.leerTempCilindroMOLDE(Convert.ToInt32(tbMolde.Text), Convert.ToInt32(tbMaquina.Text));
                    thBoq.Text = ds.Tables[0].Rows[0]["Boq"].ToString();
                    thT1.Text = ds.Tables[0].Rows[0]["T1"].ToString();
                    thT2.Text = ds.Tables[0].Rows[0]["T2"].ToString();
                    thT3.Text = ds.Tables[0].Rows[0]["T3"].ToString();
                    thT4.Text = ds.Tables[0].Rows[0]["T4"].ToString();
                    thT5.Text = ds.Tables[0].Rows[0]["T5"].ToString();
                    thT6.Text = ds.Tables[0].Rows[0]["T6"].ToString();
                    thT7.Text = ds.Tables[0].Rows[0]["T7"].ToString();
                    thT8.Text = ds.Tables[0].Rows[0]["T8"].ToString();
                    thT9.Text = ds.Tables[0].Rows[0]["T9"].ToString();
                    thT10.Text = ds.Tables[0].Rows[0]["T10"].ToString();

                    ds = conexion.leerTempCamCalienteMOLDE(Convert.ToInt32(tbMolde.Text), Convert.ToInt32(tbMaquina.Text));
                    thZ1.Text = ds.Tables[0].Rows[0]["Z1"].ToString();
                    thZ2.Text = ds.Tables[0].Rows[0]["Z2"].ToString();
                    thZ3.Text = ds.Tables[0].Rows[0]["Z3"].ToString();
                    thZ4.Text = ds.Tables[0].Rows[0]["Z4"].ToString();
                    thZ5.Text = ds.Tables[0].Rows[0]["Z5"].ToString();
                    thZ6.Text = ds.Tables[0].Rows[0]["Z6"].ToString();
                    thZ7.Text = ds.Tables[0].Rows[0]["Z7"].ToString();
                    thZ8.Text = ds.Tables[0].Rows[0]["Z8"].ToString();
                    thZ9.Text = ds.Tables[0].Rows[0]["Z9"].ToString();
                    thZ10.Text = ds.Tables[0].Rows[0]["Z10"].ToString();
                    thZ11.Text = ds.Tables[0].Rows[0]["Z11"].ToString();
                    thZ12.Text = ds.Tables[0].Rows[0]["Z12"].ToString();
                    thZ13.Text = ds.Tables[0].Rows[0]["Z13"].ToString();
                    thZ14.Text = ds.Tables[0].Rows[0]["Z14"].ToString();
                    thZ15.Text = ds.Tables[0].Rows[0]["Z15"].ToString();
                    thZ16.Text = ds.Tables[0].Rows[0]["Z16"].ToString();
                    thZ17.Text = ds.Tables[0].Rows[0]["Z17"].ToString();
                    thZ18.Text = ds.Tables[0].Rows[0]["Z18"].ToString();
                    thZ19.Text = ds.Tables[0].Rows[0]["Z19"].ToString();
                    thZ20.Text = ds.Tables[0].Rows[0]["Z20"].ToString();

                    ds = conexion.leerInyeccionMOLDE(Convert.ToInt32(tbMolde.Text), Convert.ToInt32(tbMaquina.Text));
                    tbTiempoInyeccion.Text = ds.Tables[0].Rows[0]["Tiempo"].ToString();
                    tbLimitePresion.Text = ds.Tables[0].Rows[0]["LimitePresion"].ToString();

                    ds = conexion.leerPostpresionMOLDE(Convert.ToInt32(tbMolde.Text), Convert.ToInt32(tbMaquina.Text));
                    thP1.Text = ds.Tables[0].Rows[0]["P1"].ToString();
                    thP2.Text = ds.Tables[0].Rows[0]["P2"].ToString();
                    thP3.Text = ds.Tables[0].Rows[0]["P3"].ToString();
                    thP4.Text = ds.Tables[0].Rows[0]["P4"].ToString();
                    thP5.Text = ds.Tables[0].Rows[0]["P5"].ToString();
                    thP6.Text = ds.Tables[0].Rows[0]["P6"].ToString();
                    thP7.Text = ds.Tables[0].Rows[0]["P7"].ToString();
                    thP8.Text = ds.Tables[0].Rows[0]["P8"].ToString();
                    thP9.Text = ds.Tables[0].Rows[0]["P9"].ToString();
                    thP10.Text = ds.Tables[0].Rows[0]["P10"].ToString();
                    thTP1.Text = ds.Tables[0].Rows[0]["T1"].ToString();
                    thTP2.Text = ds.Tables[0].Rows[0]["T2"].ToString();
                    thTP3.Text = ds.Tables[0].Rows[0]["T3"].ToString();
                    thTP4.Text = ds.Tables[0].Rows[0]["T4"].ToString();
                    thTP5.Text = ds.Tables[0].Rows[0]["T5"].ToString();
                    thTP6.Text = ds.Tables[0].Rows[0]["T6"].ToString();
                    thTP7.Text = ds.Tables[0].Rows[0]["T7"].ToString();
                    thTP8.Text = ds.Tables[0].Rows[0]["T8"].ToString();
                    thTP9.Text = ds.Tables[0].Rows[0]["T9"].ToString();
                    thTP10.Text = ds.Tables[0].Rows[0]["T10"].ToString();
                    tbConmutacion.Text = ds.Tables[0].Rows[0]["Conmutacion"].ToString();
                    tbTiempoPresion.Text = ds.Tables[0].Rows[0]["TiempoPresion"].ToString();

                    //PREPARAR CONSULTA ATEMPERADO AQUÍ

                    ds = conexion.leerAtemperadoMOLDE(Convert.ToInt32(tbMolde.Text), Convert.ToInt32(tbMaquina.Text));


                    TbCircuitoM1.Text = ds.Tables[0].Rows[0]["CircuitoM1"].ToString();
                    TbCircuitoM2.Text = ds.Tables[0].Rows[0]["CircuitoM2"].ToString();
                    TbCircuitoM3.Text = ds.Tables[0].Rows[0]["CircuitoM3"].ToString();
                    TbCircuitoM4.Text = ds.Tables[0].Rows[0]["CircuitoM4"].ToString();
                    TbCircuitoM5.Text = ds.Tables[0].Rows[0]["CircuitoM5"].ToString();
                    TbCircuitoM6.Text = ds.Tables[0].Rows[0]["CircuitoM6"].ToString();

                    //Convertir valores a string Movil
                    if (TbCircuitoM1.Text != "1")
                    { TbCircuitoM1.Text = conexion.devuelve_tipo_atemperado(Convert.ToInt16(TbCircuitoM1.Text)).ToString(); }
                    else { TbCircuitoM1.Text = ""; }

                    if (TbCircuitoM2.Text != "1")
                    { TbCircuitoM2.Text = conexion.devuelve_tipo_atemperado(Convert.ToInt16(TbCircuitoM2.Text)).ToString(); }
                    else { TbCircuitoM2.Text = ""; }

                    if (TbCircuitoM3.Text != "1")
                    { TbCircuitoM3.Text = conexion.devuelve_tipo_atemperado(Convert.ToInt16(TbCircuitoM3.Text)).ToString(); }
                    else { TbCircuitoM3.Text = ""; }

                    if (TbCircuitoM4.Text != "1")
                    { TbCircuitoM4.Text = conexion.devuelve_tipo_atemperado(Convert.ToInt16(TbCircuitoM4.Text)).ToString(); }
                    else { TbCircuitoM4.Text = ""; }

                    if (TbCircuitoM5.Text != "1")
                    { TbCircuitoM5.Text = conexion.devuelve_tipo_atemperado(Convert.ToInt16(TbCircuitoM5.Text)).ToString(); }
                    else { TbCircuitoM5.Text = ""; }

                    if (TbCircuitoM6.Text != "1")
                    { TbCircuitoM6.Text = conexion.devuelve_tipo_atemperado(Convert.ToInt16(TbCircuitoM6.Text)).ToString(); }
                    else { TbCircuitoM6.Text = ""; }


                    TbCircuitoF1.Text = ds.Tables[0].Rows[0]["CircuitoF1"].ToString();
                    TbCircuitoF2.Text = ds.Tables[0].Rows[0]["CircuitoF2"].ToString();
                    TbCircuitoF3.Text = ds.Tables[0].Rows[0]["CircuitoF3"].ToString();
                    TbCircuitoF4.Text = ds.Tables[0].Rows[0]["CircuitoF4"].ToString();
                    TbCircuitoF5.Text = ds.Tables[0].Rows[0]["CircuitoF5"].ToString();
                    TbCircuitoF6.Text = ds.Tables[0].Rows[0]["CircuitoF6"].ToString();

                    //Convertir valores a string fijo
                    if (TbCircuitoF1.Text != "1")
                    { TbCircuitoF1.Text = conexion.devuelve_tipo_atemperado(Convert.ToInt16(TbCircuitoF1.Text)).ToString(); }
                    else { TbCircuitoF1.Text = ""; }

                    if (TbCircuitoF2.Text != "1")
                    { TbCircuitoF2.Text = conexion.devuelve_tipo_atemperado(Convert.ToInt16(TbCircuitoF2.Text)).ToString(); }
                    else { TbCircuitoF2.Text = ""; }

                    if (TbCircuitoF3.Text != "1")
                    { TbCircuitoF3.Text = conexion.devuelve_tipo_atemperado(Convert.ToInt16(TbCircuitoF3.Text)).ToString(); }
                    else { TbCircuitoF3.Text = ""; }

                    if (TbCircuitoF4.Text != "1")
                    { TbCircuitoF4.Text = conexion.devuelve_tipo_atemperado(Convert.ToInt16(TbCircuitoF4.Text)).ToString(); }
                    else { TbCircuitoF4.Text = ""; }

                    if (TbCircuitoF5.Text != "1")
                    { TbCircuitoF5.Text = conexion.devuelve_tipo_atemperado(Convert.ToInt16(TbCircuitoF5.Text)).ToString(); }
                    else { TbCircuitoF5.Text = ""; }

                    if (TbCircuitoF6.Text != "1")
                    { TbCircuitoF6.Text = conexion.devuelve_tipo_atemperado(Convert.ToInt16(TbCircuitoF6.Text)).ToString(); }
                    else { TbCircuitoF6.Text = ""; }
                    //CAUDALES
                    //PARTE FIJA
                    TbCaudalF1.Text = ds.Tables[0].Rows[0]["CaudalF1"].ToString();
                    TbCaudalF2.Text = ds.Tables[0].Rows[0]["CaudalF2"].ToString();
                    TbCaudalF3.Text = ds.Tables[0].Rows[0]["CaudalF3"].ToString();
                    TbCaudalF4.Text = ds.Tables[0].Rows[0]["CaudalF4"].ToString();
                    TbCaudalF5.Text = ds.Tables[0].Rows[0]["CaudalF5"].ToString();
                    TbCaudalF6.Text = ds.Tables[0].Rows[0]["CaudalF6"].ToString();

                    //PARTE MOVIL
                    TbCaudalM1.Text = ds.Tables[0].Rows[0]["CaudalM1"].ToString();
                    TbCaudalM2.Text = ds.Tables[0].Rows[0]["CaudalM2"].ToString();
                    TbCaudalM3.Text = ds.Tables[0].Rows[0]["CaudalM3"].ToString();
                    TbCaudalM4.Text = ds.Tables[0].Rows[0]["CaudalM4"].ToString();
                    TbCaudalM5.Text = ds.Tables[0].Rows[0]["CaudalM5"].ToString();
                    TbCaudalM6.Text = ds.Tables[0].Rows[0]["CaudalM6"].ToString();

                    //TEMPERATURAS
                    //PARTE FIJA
                    TbTemperaturaF1.Text = ds.Tables[0].Rows[0]["TemperaturaF1"].ToString();
                    TbTemperaturaF2.Text = ds.Tables[0].Rows[0]["TemperaturaF2"].ToString();
                    TbTemperaturaF3.Text = ds.Tables[0].Rows[0]["TemperaturaF3"].ToString();
                    TbTemperaturaF4.Text = ds.Tables[0].Rows[0]["TemperaturaF4"].ToString();
                    TbTemperaturaF5.Text = ds.Tables[0].Rows[0]["TemperaturaF5"].ToString();
                    TbTemperaturaF6.Text = ds.Tables[0].Rows[0]["TemperaturaF6"].ToString();
                    //PARTE MOVIL
                    TbTemperaturaM1.Text = ds.Tables[0].Rows[0]["TemperaturaM1"].ToString();
                    TbTemperaturaM2.Text = ds.Tables[0].Rows[0]["TemperaturaM2"].ToString();
                    TbTemperaturaM3.Text = ds.Tables[0].Rows[0]["TemperaturaM3"].ToString();
                    TbTemperaturaM4.Text = ds.Tables[0].Rows[0]["TemperaturaM4"].ToString();
                    TbTemperaturaM5.Text = ds.Tables[0].Rows[0]["TemperaturaM5"].ToString();
                    TbTemperaturaM6.Text = ds.Tables[0].Rows[0]["TemperaturaM6"].ToString();

                    //LEER TOLERANCIAS
                    ds = conexion.leerToleranciasMOLDE(Convert.ToInt32(tbMolde.Text), Convert.ToInt32(tbMaquina.Text));
                    tbTiempoInyeccionNVal.Text = ds.Tables[0].Rows[0]["TiempoInyeccionNVal"].ToString();
                    tbTiempoInyeccionMVal.Text = ds.Tables[0].Rows[0]["TiempoInyeccionMVal"].ToString();
                    tbLimitePresionNVal.Text = ds.Tables[0].Rows[0]["LimitePresionNVal"].ToString();
                    tbLimitePresionMVal.Text = ds.Tables[0].Rows[0]["LimitePresionMVal"].ToString();
                    thConmuntaciontolNVal.Text = ds.Tables[0].Rows[0]["ConmuntaciontolNVal"].ToString();
                    thConmuntaciontolMVal.Text = ds.Tables[0].Rows[0]["ConmuntaciontolMVal"].ToString();
                    tbTiempoPresiontolNVal.Text = ds.Tables[0].Rows[0]["TiempoPresiontolNVal"].ToString();
                    tbTiempoPresiontolMVal.Text = ds.Tables[0].Rows[0]["TiempoPresiontolMVal"].ToString();
                    TNvcargaval.Text = ds.Tables[0].Rows[0]["TNvcargaval"].ToString();
                    TMvcargaval.Text = ds.Tables[0].Rows[0]["TMvcargaval"].ToString();
                    TNcargaval.Text = ds.Tables[0].Rows[0]["TNcargaval"].ToString();
                    TMcargaval.Text = ds.Tables[0].Rows[0]["TMcargaval"].ToString();
                    TNdescomval.Text = ds.Tables[0].Rows[0]["TNdescomval"].ToString();
                    TMdescomval.Text = ds.Tables[0].Rows[0]["TMdescomval"].ToString();
                    TNcontrapval.Text = ds.Tables[0].Rows[0]["TNcontrapval"].ToString();
                    TMcontrapval.Text = ds.Tables[0].Rows[0]["TMcontrapval"].ToString();
                    TNTiempdosval.Text = ds.Tables[0].Rows[0]["TNTiempdosval"].ToString();
                    TMTiempdosval.Text = ds.Tables[0].Rows[0]["TMTiempdosval"].ToString();
                    TNEnfriamval.Text = ds.Tables[0].Rows[0]["TNEnfriamval"].ToString();
                    TMEnfriamval.Text = ds.Tables[0].Rows[0]["TMEnfriamval"].ToString();
                    TNCicloval.Text = ds.Tables[0].Rows[0]["TNCicloval"].ToString();
                    TMCicloval.Text = ds.Tables[0].Rows[0]["TMCicloval"].ToString();
                    TNCojinval.Text = ds.Tables[0].Rows[0]["TNCojinval"].ToString();
                    TMCojinval.Text = ds.Tables[0].Rows[0]["TMCojinval"].ToString();
                }
            }
            catch (Exception)
            {
                alertafichafabricacion.Visible = true;
                EXISTEFICHA.Text = "1";

            }
        }
        public void CargarMateriasPrimas()
        {
            try
            {
                Conexion_LIBERACIONES conexion = new Conexion_LIBERACIONES();
                DataSet materiales = conexion.devuelve_materiasprimasXReferencias(tbReferencia.Text);
                if (materiales.Tables[0].Rows.Count > 0)
                {
                    //MAT1STOCK.Visible = true;
                    MATSAVE1.Visible = true;
                    MAT1LOT.Visible = true;
                    MAT1LOT2.Visible = true;
                    MAT1NOM.Visible = true;
                    MAT1REF.Visible = true;
                    MAT1TEMP.Visible = true;
                    MAT1TEMPREAL.Visible = true;
                    MAT1TIEMP.Visible = true;
                    MAT1TIEMPREAL.Visible = true;
                    MAT1REF.Text = materiales.Tables[0].Rows[0]["C_ID"].ToString();
                    MAT1NOM.Text = materiales.Tables[0].Rows[0]["C_LONG_DESCR"].ToString();
                    MAT1TIEMP.Text = materiales.Tables[0].Rows[0]["C_USERVALUE1"].ToString();
                    //MAT1TEMP.Text = materiales.Tables[0].Rows[0]["C_REMARKS"].ToString().Split('|')[0];
                    //MAT1TIEMP.Text = materiales.Tables[0].Rows[0]["C_REMARKS"].ToString().Split('|')[1];
                    //RETURN
                }
                if (materiales.Tables[0].Rows.Count > 1)
                {
                    //MAT2STOCK.Visible = true;
                    MATSAVE2.Visible = true;
                    MAT2LOT.Visible = true;
                    MAT2LOT2.Visible = true;
                    MAT2NOM.Visible = true;
                    MAT2REF.Visible = true;
                    MAT2TEMP.Visible = true;
                    MAT2TEMPREAL.Visible = true;
                    MAT2TIEMP.Visible = true;
                    MAT2TIEMPREAL.Visible = true;
                    MAT2REF.Text = materiales.Tables[0].Rows[1]["C_ID"].ToString();
                    MAT2NOM.Text = materiales.Tables[0].Rows[1]["C_LONG_DESCR"].ToString();
                    MAT2TIEMP.Text = materiales.Tables[0].Rows[1]["C_USERVALUE1"].ToString();
                }
                if (materiales.Tables[0].Rows.Count > 2)
                {
                    //MAT3STOCK.Visible = true;
                    MATSAVE3.Visible = true;
                    MAT3LOT.Visible = true;
                    MAT3LOT2.Visible = true;
                    MAT3NOM.Visible = true;
                    MAT3REF.Visible = true;
                    MAT3TEMP.Visible = true;
                    MAT3TEMPREAL.Visible = true;
                    MAT3TIEMP.Visible = true;
                    MAT3TIEMPREAL.Visible = true;
                    MAT3REF.Text = materiales.Tables[0].Rows[2]["C_ID"].ToString();
                    MAT3NOM.Text = materiales.Tables[0].Rows[2]["C_LONG_DESCR"].ToString();
                    MAT3TIEMP.Text = materiales.Tables[0].Rows[2]["C_USERVALUE1"].ToString();
                }
                DataSet Componentes = conexion.devuelve_componentesXReferencias(tbReferencia.Text);
                if (Componentes.Tables[0].Rows.Count > 0)
                {
                    COMPSAVE1.Visible = true;
                    //COMP1STOCK.Visible = true;
                    COMP1REF.Visible = true;
                    COMP1NOM.Visible = true;
                    COMP1LOT.Visible = true;
                    COMP1LOT2.Visible = true;
                    COMP1REF.Text = Componentes.Tables[0].Rows[0]["C_ID"].ToString();
                    COMP1NOM.Text = Componentes.Tables[0].Rows[0]["C_LONG_DESCR"].ToString();
                }
                if (Componentes.Tables[0].Rows.Count > 1)
                {
                    COMPSAVE2.Visible = true;
                    //COMP2STOCK.Visible = true;
                    COMP2REF.Visible = true;
                    COMP2NOM.Visible = true;
                    COMP2LOT.Visible = true;
                    COMP2LOT2.Visible = true;
                    COMP2REF.Text = Componentes.Tables[0].Rows[1]["C_ID"].ToString();
                    COMP2NOM.Text = Componentes.Tables[0].Rows[1]["C_LONG_DESCR"].ToString();
                }
                if (Componentes.Tables[0].Rows.Count > 2)
                {
                    COMPSAVE3.Visible = true;
                    //COMP3STOCK.Visible = true;
                    COMP3REF.Visible = true;
                    COMP3NOM.Visible = true;
                    COMP3LOT.Visible = true;
                    COMP3LOT2.Visible = true;
                    COMP3REF.Text = Componentes.Tables[0].Rows[2]["C_ID"].ToString();
                    COMP3NOM.Text = Componentes.Tables[0].Rows[2]["C_LONG_DESCR"].ToString();
                }
                if (Componentes.Tables[0].Rows.Count > 3)
                {
                    COMPSAVE4.Visible = true;
                    //COMP4STOCK.Visible = true;
                    COMP4REF.Visible = true;
                    COMP4NOM.Visible = true;
                    COMP4LOT.Visible = true;
                    COMP4LOT2.Visible = true;
                    COMP4REF.Text = Componentes.Tables[0].Rows[3]["C_ID"].ToString();
                    COMP4NOM.Text = Componentes.Tables[0].Rows[3]["C_LONG_DESCR"].ToString();
                }
                if (Componentes.Tables[0].Rows.Count > 4)
                {
                    COMPSAVE5.Visible = true;
                    //COMP5STOCK.Visible = true;
                    COMP5REF.Visible = true;
                    COMP5NOM.Visible = true;
                    COMP5LOT.Visible = true;
                    COMP5LOT2.Visible = true;
                    COMP5REF.Text = Componentes.Tables[0].Rows[3]["C_ID"].ToString();
                    COMP5NOM.Text = Componentes.Tables[0].Rows[3]["C_LONG_DESCR"].ToString();
                }

            }
            catch (Exception)
            { }

        }
        public void CargarMantenimiento()
        {
            try
            {
                Conexion_LIBERACIONES conexion = new Conexion_LIBERACIONES();
                //molde
                DataSet mantmolde = conexion.Devuelve_reparaciones_molde(Convert.ToInt32(tbMolde.Text));

                if (mantmolde.Tables[0].Rows.Count > 0)
                {
                    tbParteMolde.Text = mantmolde.Tables[0].Rows[0]["IdReparacionMolde"].ToString();
                    TbMantMolde.Text = mantmolde.Tables[0].Rows[0]["MotivoAveria"].ToString();
                    TbRepaMolde.Text = mantmolde.Tables[0].Rows[0]["Reparacion"].ToString();
                    TbEstadoRepMolde.Text = mantmolde.Tables[0].Rows[0]["Texto"].ToString();
                    dgv_mantmolde.DataSource = mantmolde;
                    dgv_mantmolde.DataBind();
                }


                //maquina
                DataSet mantmaquina = conexion.Devuelve_reparaciones_maquina(tbMaquina.Text);

                if (mantmaquina.Tables[0].Rows.Count > 0)
                {
                    tbParteMaq.Text = mantmaquina.Tables[0].Rows[0]["IdMantenimiento"].ToString();
                    TbMantMaq.Text = mantmaquina.Tables[0].Rows[0]["MotivoAveria"].ToString();
                    TbRepaMaq.Text = mantmaquina.Tables[0].Rows[0]["Reparacion"].ToString();
                    TbEstadoRepMaq.Text = mantmaquina.Tables[0].Rows[0]["Texto"].ToString();
                    dgv_mantmaq.DataSource = mantmaquina;
                    dgv_mantmaq.DataBind();
                }



            }
            catch (Exception)
            { }

        }
        public void CrearLiberacion()
        {
            try
            {
                //VALORES DE FICHA
                int version = 0;

                int operario1numero = 0;
                if (Operario1Numero.Text != "")
                { operario1numero = Convert.ToInt32(Operario1Numero.Text); }

                double operario1horas = 0;
                if (Operario1Horas.Text != "")
                { operario1horas = Convert.ToDouble(Operario1Horas.Text); }

                int operario2numero = 0;
                if (Operario2Numero.Text != "")
                { operario2numero = Convert.ToInt32(Operario2Numero.Text); }
                double operario2horas = 0;
                if (Operario2Horas.Text != "")
                { operario2horas = Convert.ToDouble(Operario2Horas.Text); }

                int encargadonumero = 0;
                if (EncargadoNumero.Text != "")
                { encargadonumero = Convert.ToInt32(EncargadoNumero.Text); }
                double encargadohoras = 0;
                if (EncargadoHoras.Text != "")
                { encargadohoras = Convert.ToDouble(EncargadoHoras.Text); }

                int cambiadornumero = 0;
                if (CambiadorNumero.Text != "")
                { cambiadornumero = Convert.ToInt32(CambiadorNumero.Text); }
                double cambiadorhoras = 0;
                if (CambiadorHoras.Text != "")
                { cambiadorhoras = Convert.ToDouble(CambiadorHoras.Text); }

                int calidadnumero = 0;
                if (CalidadNumero.Text != "")
                { calidadnumero = Convert.ToInt32(CalidadNumero.Text); }
                double calidadhoras = 0;
                if (CalidadHoras.Text != "")
                { calidadhoras = Convert.ToDouble(CalidadHoras.Text); }

                int CambiadorLiberado = 0;
                int ProduccionLiberado = 0;
                int CalidadLiberado = 0;
                int ResultadoLiberacion = 0;
                int Reliberacion = 0;
                int NCEncargado = 0;
                int GP12Encargado = 0;
                int NCCalidad = 0;
                int GP12Calidad = 0;

                //VALORES DE POSTPRESION
                double thP1_double = 0;
                double thP2_double = 0;
                double thP3_double = 0;
                double thP4_double = 0;
                double thP5_double = 0;
                double thP6_double = 0;
                double thP7_double = 0;
                double thP8_double = 0;
                double thP9_double = 0;
                double thP10_double = 0;
                double thTP1_double = 0;
                double thTP2_double = 0;
                double thTP3_double = 0;
                double thTP4_double = 0;
                double thTP5_double = 0;
                double thTP6_double = 0;
                double thTP7_double = 0;
                double thTP8_double = 0;
                double thTP9_double = 0;
                double thTP10_double = 0;
                if (Double.TryParse((thP1.Text).Replace('.', ','), out thP1_double))
                    thP1_double = Convert.ToDouble(thP1.Text.Replace('.', ','));
                else
                    thP1_double = 0.0;
                if (Double.TryParse((thP2.Text).Replace('.', ','), out thP2_double))
                    thP2_double = Convert.ToDouble(thP2.Text.Replace('.', ','));
                else
                    thP2_double = 0.0;
                if (Double.TryParse(thP3.Text.Replace('.', ','), out thP3_double))
                    thP3_double = Convert.ToDouble(thP3.Text.Replace('.', ','));
                else
                    thP3_double = 0.0;
                if (Double.TryParse(thP4.Text.Replace('.', ','), out thP4_double))
                    thP4_double = Convert.ToDouble(thP4.Text.Replace('.', ','));
                else
                    thP4_double = 0.0;
                if (Double.TryParse(thP5.Text.Replace('.', ','), out thP5_double))
                    thP5_double = Convert.ToDouble(thP5.Text.Replace('.', ','));
                else
                    thP5_double = 0.0;
                if (Double.TryParse(thP6.Text.Replace('.', ','), out thP6_double))
                    thP6_double = Convert.ToDouble(thP6.Text.Replace('.', ','));
                else
                    thP6_double = 0.0;
                if (Double.TryParse(thP7.Text.Replace('.', ','), out thP7_double))
                    thP7_double = Convert.ToDouble(thP7.Text.Replace('.', ','));
                else
                    thP7_double = 0.0;
                if (Double.TryParse(thP8.Text.Replace('.', ','), out thP8_double))
                    thP8_double = Convert.ToDouble(thP8.Text.Replace('.', ','));
                else
                    thP8_double = 0.0;
                if (Double.TryParse(thP9.Text.Replace('.', ','), out thP9_double))
                    thP9_double = Convert.ToDouble(thP9.Text.Replace('.', ','));
                else
                    thP9_double = 0.0;
                if (Double.TryParse(thP10.Text.Replace('.', ','), out thP10_double))
                    thP10_double = Convert.ToDouble(thP10.Text.Replace('.', ','));
                else
                    thP10_double = 0.0;
                if (Double.TryParse(thTP1.Text.Replace('.', ','), out thTP1_double))
                    thTP1_double = Convert.ToDouble(thTP1.Text.Replace('.', ','));
                else
                    thTP1_double = 0.0;
                if (Double.TryParse(thTP2.Text.Replace('.', ','), out thTP2_double))
                    thTP2_double = Convert.ToDouble(thTP2.Text.Replace('.', ','));
                else
                    thTP2_double = 0.0;
                if (Double.TryParse(thTP3.Text.Replace('.', ','), out thTP3_double))
                    thTP3_double = Convert.ToDouble(thTP3.Text.Replace('.', ','));
                else
                    thTP3_double = 0.0;
                if (Double.TryParse(thTP4.Text.Replace('.', ','), out thTP4_double))
                    thTP4_double = Convert.ToDouble(thTP4.Text.Replace('.', ','));
                else
                    thTP4_double = 0.0;
                if (Double.TryParse(thTP5.Text.Replace('.', ','), out thTP5_double))
                    thTP5_double = Convert.ToDouble(thTP5.Text.Replace('.', ','));
                else
                    thTP5_double = 0.0;
                if (Double.TryParse(thTP6.Text.Replace('.', ','), out thTP6_double))
                    thTP6_double = Convert.ToDouble(thTP6.Text.Replace('.', ','));
                else
                    thTP6_double = 0.0;
                if (Double.TryParse(thTP7.Text.Replace('.', ','), out thTP7_double))
                    thTP7_double = Convert.ToDouble(thTP7.Text.Replace('.', ','));
                else
                    thTP7_double = 0.0;
                if (Double.TryParse(thTP8.Text.Replace('.', ','), out thTP8_double))
                    thTP8_double = Convert.ToDouble(thTP8.Text.Replace('.', ','));
                else
                    thTP8_double = 0.0;
                if (Double.TryParse(thTP9.Text.Replace('.', ','), out thTP9_double))
                    thTP9_double = Convert.ToDouble(thTP9.Text.Replace('.', ','));
                else
                    thTP9_double = 0.0;
                if (Double.TryParse(thTP10.Text.Replace('.', ','), out thTP10_double))
                    thTP10_double = Convert.ToDouble(thTP10.Text.Replace('.', ','));
                else
                    thTP10_double = 0.0;

                //VALORES CAMARA CALIENTE
                double thZ1_double = 0;
                double thZ2_double = 0;
                double thZ3_double = 0;
                double thZ4_double = 0;
                double thZ5_double = 0;
                double thZ6_double = 0;
                double thZ7_double = 0;
                double thZ8_double = 0;
                double thZ9_double = 0;
                double thZ10_double = 0;
                double thZ11_double = 0;
                double thZ12_double = 0;
                double thZ13_double = 0;
                double thZ14_double = 0;
                double thZ15_double = 0;
                double thZ16_double = 0;
                double thZ17_double = 0;
                double thZ18_double = 0;
                double thZ19_double = 0;
                double thZ20_double = 0;

                if (Double.TryParse(thZ1.Text.Replace('.', ','), out thZ1_double))
                    thZ1_double = Convert.ToDouble(thZ1.Text.Replace('.', ','));
                else
                    thZ1_double = 0.0;
                if (Double.TryParse(thZ2.Text.Replace('.', ','), out thZ2_double))
                    thZ2_double = Convert.ToDouble(thZ2.Text.Replace('.', ','));
                else
                    thZ2_double = 0.0;
                if (Double.TryParse(thZ3.Text.Replace('.', ','), out thZ3_double))
                    thZ3_double = Convert.ToDouble(thZ3.Text.Replace('.', ','));
                else
                    thZ3_double = 0.0;
                if (Double.TryParse(thZ4.Text.Replace('.', ','), out thZ4_double))
                    thZ4_double = Convert.ToDouble(thZ4.Text.Replace('.', ','));
                else
                    thZ4_double = 0.0;
                if (Double.TryParse(thZ5.Text.Replace('.', ','), out thZ5_double))
                    thZ5_double = Convert.ToDouble(thZ5.Text.Replace('.', ','));
                else
                    thZ5_double = 0.0;
                if (Double.TryParse(thZ6.Text.Replace('.', ','), out thZ6_double))
                    thZ6_double = Convert.ToDouble(thZ6.Text.Replace('.', ','));
                else
                    thZ6_double = 0.0;
                if (Double.TryParse(thZ7.Text.Replace('.', ','), out thZ7_double))
                    thZ7_double = Convert.ToDouble(thZ7.Text.Replace('.', ','));
                else
                    thZ7_double = 0.0;
                if (Double.TryParse(thZ8.Text.Replace('.', ','), out thZ8_double))
                    thZ8_double = Convert.ToDouble(thZ8.Text.Replace('.', ','));
                else
                    thZ8_double = 0.0;
                if (Double.TryParse(thZ9.Text.Replace('.', ','), out thZ9_double))
                    thZ9_double = Convert.ToDouble(thZ9.Text.Replace('.', ','));
                else
                    thZ9_double = 0.0;
                if (Double.TryParse(thZ10.Text.Replace('.', ','), out thZ10_double))
                    thZ10_double = Convert.ToDouble(thZ10.Text.Replace('.', ','));
                else
                    thZ10_double = 0.0;
                if (Double.TryParse(thZ11.Text.Replace('.', ','), out thZ11_double))
                    thZ11_double = Convert.ToDouble(thZ11.Text.Replace('.', ','));
                else
                    thZ11_double = 0.0;
                if (Double.TryParse(thZ12.Text.Replace('.', ','), out thZ12_double))
                    thZ12_double = Convert.ToDouble(thZ12.Text.Replace('.', ','));
                else
                    thZ12_double = 0.0;
                if (Double.TryParse(thZ13.Text.Replace('.', ','), out thZ13_double))
                    thZ13_double = Convert.ToDouble(thZ13.Text.Replace('.', ','));
                else
                    thZ13_double = 0.0;
                if (Double.TryParse(thZ14.Text.Replace('.', ','), out thZ14_double))
                    thZ14_double = Convert.ToDouble(thZ14.Text.Replace('.', ','));
                else
                    thZ14_double = 0.0;
                if (Double.TryParse(thZ15.Text.Replace('.', ','), out thZ15_double))
                    thZ15_double = Convert.ToDouble(thZ15.Text.Replace('.', ','));
                else
                    thZ15_double = 0.0;
                if (Double.TryParse(thZ16.Text.Replace('.', ','), out thZ16_double))
                    thZ16_double = Convert.ToDouble(thZ16.Text.Replace('.', ','));
                else
                    thZ16_double = 0.0;
                if (Double.TryParse(thZ17.Text.Replace('.', ','), out thZ17_double))
                    thZ17_double = Convert.ToDouble(thZ17.Text.Replace('.', ','));
                else
                    thZ17_double = 0.0;
                if (Double.TryParse(thZ18.Text.Replace('.', ','), out thZ18_double))
                    thZ18_double = Convert.ToDouble(thZ18.Text.Replace('.', ','));
                else
                    thZ18_double = 0.0;
                if (Double.TryParse(thZ19.Text.Replace('.', ','), out thZ19_double))
                    thZ19_double = Convert.ToDouble(thZ19.Text.Replace('.', ','));
                else
                    thZ19_double = 0.0;
                if (Double.TryParse(thZ20.Text.Replace('.', ','), out thZ20_double))
                    thZ20_double = Convert.ToDouble(thZ20.Text.Replace('.', ','));
                else
                    thZ20_double = 0.0;

                //VALORES TEMPERATURA CILINDRO
                double thBoq_double = 0;
                double thT1_double = 0;
                double thT2_double = 0;
                double thT3_double = 0;
                double thT4_double = 0;
                double thT5_double = 0;
                double thT6_double = 0;
                double thT7_double = 0;
                double thT8_double = 0;
                double thT9_double = 0;
                double thT10_double = 0;

                if (Double.TryParse(thBoq.Text.Replace('.', ','), out thBoq_double))
                    thBoq_double = Convert.ToDouble(thBoq.Text.Replace('.', ','));
                else
                    thBoq_double = 0.0;
                if (Double.TryParse(thT1.Text.Replace('.', ','), out thT1_double))
                    thT1_double = Convert.ToDouble(thT1.Text.Replace('.', ','));
                else
                    thT1_double = 0.0;
                if (Double.TryParse(thT2.Text.Replace('.', ','), out thT2_double))
                    thT2_double = Convert.ToDouble(thT2.Text.Replace('.', ','));
                else
                    thT2_double = 0.0;
                if (Double.TryParse(thT3.Text.Replace('.', ','), out thT3_double))
                    thT3_double = Convert.ToDouble(thT3.Text.Replace('.', ','));
                else
                    thT3_double = 0.0;
                if (Double.TryParse(thT4.Text.Replace('.', ','), out thT4_double))
                    thT4_double = Convert.ToDouble(thT4.Text.Replace('.', ','));
                else
                    thT4_double = 0.0;
                if (Double.TryParse(thT5.Text.Replace('.', ','), out thT5_double))
                    thT5_double = Convert.ToDouble(thT5.Text.Replace('.', ','));
                else
                    thT5_double = 0.0;
                if (Double.TryParse(thT6.Text.Replace('.', ','), out thT6_double))
                    thT6_double = Convert.ToDouble(thT6.Text.Replace('.', ','));
                else
                    thT6_double = 0.0;
                if (Double.TryParse(thT7.Text.Replace('.', ','), out thT7_double))
                    thT7_double = Convert.ToDouble(thT7.Text.Replace('.', ','));
                else
                    thT7_double = 0.0;
                if (Double.TryParse(thT8.Text.Replace('.', ','), out thT8_double))
                    thT8_double = Convert.ToDouble(thT8.Text.Replace('.', ','));
                else
                    thT8_double = 0.0;
                if (Double.TryParse(thT9.Text.Replace('.', ','), out thT9_double))
                    thT9_double = Convert.ToDouble(thT9.Text.Replace('.', ','));
                else
                    thT9_double = 0.0;
                if (Double.TryParse(thT10.Text.Replace('.', ','), out thT10_double))
                    thT10_double = Convert.ToDouble(thT10.Text.Replace('.', ','));
                else
                    thT10_double = 0.0;

                //VALORES DE TOLERANCIAS
                double TiempoInyeccionNValDouble = 0;
                double TiempoInyeccionMValDouble = 0;
                double LimitePresionNValDouble = 0;
                double LimitePresionMValDouble = 0;
                double ConmuntaciontolNValDouble = 0;
                double ConmuntaciontolMValDouble = 0;
                double TiempoPresiontolNValDouble = 0;
                double TiempoPresiontolMValDouble = 0;
                double TNvcargavalDouble = 0;
                double TMvcargavalDouble = 0;
                double TNcargavalDouble = 0;
                double TMcargavalDouble = 0;
                double TNdescomvalDouble = 0;
                double TMdescomvalDouble = 0;
                double TNcontrapvalDouble = 0;
                double TMcontrapvalDouble = 0;
                double TNTiempdosvalDouble = 0;
                double TMTiempdosvalDouble = 0;
                double TNEnfriamvalDouble = 0;
                double TMEnfriamvalDouble = 0;
                double TNCiclovalDouble = 0;
                double TMCiclovalDouble = 0;
                double TNCojinvalDouble = 0;
                double TMCojinvalDouble = 0;

                double tbTiempoInyeccion_double = 0;
                double tbLimitePresion_double = 0;
                double thVCarga_double = 0;
                double thCarga_double = 0;
                double thDescomp_double = 0;
                double thContrapr_double = 0;
                double thTiempo_double = 0;
                double thEnfriamiento_double = 0;
                double thCiclo_double = 0;
                double thCojin_double = 0;

                if (Double.TryParse(tbTiempoInyeccion.Text.Replace('.', ','), out tbTiempoInyeccion_double))
                    tbTiempoInyeccion_double = Convert.ToDouble(tbTiempoInyeccion.Text.Replace('.', ','));
                else tbTiempoInyeccion_double = 0.0;

                if (Double.TryParse(tbLimitePresion.Text.Replace('.', ','), out tbLimitePresion_double))
                    tbLimitePresion_double = Convert.ToDouble(tbLimitePresion.Text.Replace('.', ','));
                else tbLimitePresion_double = 0.0;

                if (Double.TryParse(thVCarga.Text.Replace('.', ','), out thVCarga_double))
                    thVCarga_double = Convert.ToDouble(thVCarga.Text.Replace('.', ','));
                else thVCarga_double = 0.0;

                if (Double.TryParse(thCarga.Text.Replace('.', ','), out thCarga_double))
                    thCarga_double = Convert.ToDouble(thCarga.Text.Replace('.', ','));
                else thCarga_double = 0.0;

                if (Double.TryParse(thDescomp.Text.Replace('.', ','), out thDescomp_double))
                    thDescomp_double = Convert.ToDouble(thDescomp.Text.Replace('.', ','));
                else thDescomp_double = 0.0;

                if (Double.TryParse(thContrapr.Text.Replace('.', ','), out thContrapr_double))
                    thContrapr_double = Convert.ToDouble(thContrapr.Text.Replace('.', ','));
                else thContrapr_double = 0.0;

                if (Double.TryParse(thTiempo.Text.Replace('.', ','), out thTiempo_double))
                    thTiempo_double = Convert.ToDouble(thTiempo.Text.Replace('.', ','));
                else thTiempo_double = 0.0;

                if (Double.TryParse(thEnfriamiento.Text.Replace('.', ','), out thEnfriamiento_double))
                    thEnfriamiento_double = Convert.ToDouble(thEnfriamiento.Text.Replace('.', ','));
                else thEnfriamiento_double = 0.0;

                if (Double.TryParse(thCiclo.Text.Replace('.', ','), out thCiclo_double))
                    thCiclo_double = Convert.ToDouble(thCiclo.Text.Replace('.', ','));
                else thCiclo_double = 0.0;

                if (Double.TryParse(thCojin.Text.Replace('.', ','), out thCojin_double))
                    thCojin_double = Convert.ToDouble(thCojin.Text.Replace('.', ','));
                else thCojin_double = 0.0;

                if (Double.TryParse(tbTiempoInyeccionNVal.Text.Replace('.', ','), out TiempoInyeccionNValDouble))
                    TiempoInyeccionNValDouble = Convert.ToDouble(tbTiempoInyeccionNVal.Text.Replace('.', ','));
                else
                    TiempoInyeccionNValDouble = 0.0;
                if (Double.TryParse(tbTiempoInyeccionMVal.Text.Replace('.', ','), out TiempoInyeccionMValDouble))
                    TiempoInyeccionMValDouble = Convert.ToDouble(tbTiempoInyeccionMVal.Text.Replace('.', ','));
                else
                    TiempoInyeccionMValDouble = 0.0;
                if (Double.TryParse(tbLimitePresionNVal.Text.Replace('.', ','), out LimitePresionNValDouble))
                    LimitePresionNValDouble = Convert.ToDouble(tbLimitePresionNVal.Text.Replace('.', ','));
                else
                    LimitePresionNValDouble = 0.0;
                if (Double.TryParse(tbLimitePresionMVal.Text.Replace('.', ','), out LimitePresionMValDouble))
                    LimitePresionMValDouble = Convert.ToDouble(tbLimitePresionMVal.Text.Replace('.', ','));
                else
                    LimitePresionNValDouble = 0.0;
                if (Double.TryParse(thConmuntaciontolNVal.Text.Replace('.', ','), out ConmuntaciontolNValDouble))
                    ConmuntaciontolNValDouble = Convert.ToDouble(thConmuntaciontolNVal.Text.Replace('.', ','));
                else
                    ConmuntaciontolNValDouble = 0.0;
                if (Double.TryParse(thConmuntaciontolMVal.Text.Replace('.', ','), out ConmuntaciontolMValDouble))
                    ConmuntaciontolMValDouble = Convert.ToDouble(thConmuntaciontolMVal.Text.Replace('.', ','));
                else
                    ConmuntaciontolMValDouble = 0.0;
                if (Double.TryParse(tbTiempoPresiontolNVal.Text.Replace('.', ','), out TiempoPresiontolNValDouble))
                    TiempoPresiontolNValDouble = Convert.ToDouble(tbTiempoPresiontolNVal.Text.Replace('.', ','));
                else
                    TiempoPresiontolNValDouble = 0.0;
                if (Double.TryParse(tbTiempoPresiontolMVal.Text.Replace('.', ','), out TiempoPresiontolMValDouble))
                    TiempoPresiontolMValDouble = Convert.ToDouble(tbTiempoPresiontolMVal.Text.Replace('.', ','));
                else
                    TiempoPresiontolMValDouble = 0.0;
                if (Double.TryParse(TNvcargaval.Text.Replace('.', ','), out TNvcargavalDouble))
                    TNvcargavalDouble = Convert.ToDouble(TNvcargaval.Text.Replace('.', ','));
                else
                    TNvcargavalDouble = 0.0;
                if (Double.TryParse(TMvcargaval.Text.Replace('.', ','), out TMvcargavalDouble))
                    TMvcargavalDouble = Convert.ToDouble(TMvcargaval.Text.Replace('.', ','));
                else
                    TMvcargavalDouble = 0.0;
                if (Double.TryParse(TNcargaval.Text.Replace('.', ','), out TNcargavalDouble))
                    TNcargavalDouble = Convert.ToDouble(TNcargaval.Text.Replace('.', ','));
                else
                    TNcargavalDouble = 0.0;
                if (Double.TryParse(TMcargaval.Text.Replace('.', ','), out TMcargavalDouble))
                    TMcargavalDouble = Convert.ToDouble(TMcargaval.Text.Replace('.', ','));
                else
                    TMcargavalDouble = 0.0;

                if (Double.TryParse(TNdescomval.Text.Replace('.', ','), out TNdescomvalDouble))
                    TNdescomvalDouble = Convert.ToDouble(TNdescomval.Text.Replace('.', ','));
                else
                    TNdescomvalDouble = 0.0;
                if (Double.TryParse(TMdescomval.Text.Replace('.', ','), out TMdescomvalDouble))
                    TMdescomvalDouble = Convert.ToDouble(TMdescomval.Text.Replace('.', ','));
                else
                    TMdescomvalDouble = 0.0;
                if (Double.TryParse(TNcontrapval.Text.Replace('.', ','), out TNcontrapvalDouble))
                    TNcontrapvalDouble = Convert.ToDouble(TNcontrapval.Text.Replace('.', ','));
                else
                    TNcontrapvalDouble = 0.0;
                if (Double.TryParse(TMcontrapval.Text.Replace('.', ','), out TMcontrapvalDouble))
                    TMcontrapvalDouble = Convert.ToDouble(TMcontrapval.Text.Replace('.', ','));
                else
                    TMcontrapvalDouble = 0.0;
                if (Double.TryParse(TNTiempdosval.Text.Replace('.', ','), out TNTiempdosvalDouble))
                    TNTiempdosvalDouble = Convert.ToDouble(TNTiempdosval.Text.Replace('.', ','));
                else
                    TNTiempdosvalDouble = 0.0;
                if (Double.TryParse(TMTiempdosval.Text.Replace('.', ','), out TMTiempdosvalDouble))
                    TMTiempdosvalDouble = Convert.ToDouble(TMTiempdosval.Text.Replace('.', ','));
                else
                    TMTiempdosvalDouble = 0.0;
                if (Double.TryParse(TNEnfriamval.Text.Replace('.', ','), out TNEnfriamvalDouble))
                    TNEnfriamvalDouble = Convert.ToDouble(TNEnfriamval.Text.Replace('.', ','));
                else
                    TNEnfriamvalDouble = 0.0;
                if (Double.TryParse(TMEnfriamval.Text.Replace('.', ','), out TMEnfriamvalDouble))
                    TMEnfriamvalDouble = Convert.ToDouble(TMEnfriamval.Text.Replace('.', ','));
                else
                    TMEnfriamvalDouble = 0.0;

                if (Double.TryParse(TNCicloval.Text.Replace('.', ','), out TNCiclovalDouble))
                    TNCiclovalDouble = Convert.ToDouble(TNCicloval.Text.Replace('.', ','));
                else
                    TNCiclovalDouble = 0.0;
                if (Double.TryParse(TMCicloval.Text.Replace('.', ','), out TMCiclovalDouble))
                    TMCiclovalDouble = Convert.ToDouble(TMCicloval.Text.Replace('.', ','));
                else
                    TMCiclovalDouble = 0.0;

                if (Double.TryParse(TNCojinval.Text.Replace('.', ','), out TNCojinvalDouble))
                    TNCojinvalDouble = Convert.ToDouble(TNCojinval.Text.Replace('.', ','));
                else
                    TNCojinvalDouble = 0.0;
                if (Double.TryParse(TMCojinval.Text.Replace('.', ','), out TMCojinvalDouble))
                    TMCojinvalDouble = Convert.ToDouble(TMCojinval.Text.Replace('.', ','));
                else
                    TMCojinvalDouble = 0.0;

                //MATERIALES
                double MAT1TIEMP_double = 0;
                if (Double.TryParse(MAT1TIEMP.Text.Replace('.', ','), out MAT1TIEMP_double))
                    MAT1TIEMP_double = Convert.ToDouble(MAT1TIEMP.Text.Replace('.', ','));
                else
                    MAT1TIEMP_double = 0.0;


                double MAT1TIEMPREAL_double = 0;
                if (Double.TryParse(MAT1TIEMPREAL.Text.Replace('.', ','), out MAT1TIEMPREAL_double))
                    MAT1TIEMPREAL_double = Convert.ToDouble(MAT1TIEMPREAL.Text.Replace('.', ','));
                else
                    MAT1TIEMPREAL_double = 0.0;
                double MAT1TEMP_double = 0;
                if (Double.TryParse(MAT1TEMP.Text.Replace('.', ','), out MAT1TEMP_double))
                    MAT1TEMP_double = Convert.ToDouble(MAT1TEMP.Text.Replace('.', ','));
                else
                    MAT1TEMP_double = 0.0;
                double MAT1TEMPREAL_double = 0;
                if (Double.TryParse(MAT1TEMPREAL.Text.Replace('.', ','), out MAT1TEMPREAL_double))
                    MAT1TEMPREAL_double = Convert.ToDouble(MAT1TEMPREAL.Text.Replace('.', ','));
                else
                    MAT1TEMPREAL_double = 0.0;
                double MAT2TIEMP_double = 0;
                if (Double.TryParse(MAT2TIEMP.Text.Replace('.', ','), out MAT2TIEMP_double))
                    MAT2TIEMP_double = Convert.ToDouble(MAT2TIEMP.Text.Replace('.', ','));
                else
                    MAT2TIEMP_double = 0.0;
                double MAT2TIEMPREAL_double = 0;
                if (Double.TryParse(MAT2TIEMPREAL.Text.Replace('.', ','), out MAT2TIEMPREAL_double))
                    MAT2TIEMPREAL_double = Convert.ToDouble(MAT2TIEMPREAL.Text.Replace('.', ','));
                else
                    MAT2TIEMPREAL_double = 0.0;
                double MAT2TEMP_double = 0;
                if (Double.TryParse(MAT2TEMP.Text.Replace('.', ','), out MAT2TEMP_double))
                    MAT2TEMP_double = Convert.ToDouble(MAT2TEMP.Text.Replace('.', ','));
                else
                    MAT2TEMP_double = 0.0;

                double MAT2TEMPREAL_double = 0;
                if (Double.TryParse(MAT2TEMPREAL.Text.Replace('.', ','), out MAT2TEMPREAL_double))
                    MAT2TEMPREAL_double = Convert.ToDouble(MAT2TEMPREAL.Text.Replace('.', ','));
                else
                    MAT2TEMPREAL_double = 0.0;

                double MAT3TIEMP_double = 0;
                if (Double.TryParse(MAT3TIEMP.Text.Replace('.', ','), out MAT3TIEMP_double))
                    MAT3TIEMP_double = Convert.ToDouble(MAT3TIEMP.Text.Replace('.', ','));
                else
                    MAT3TIEMP_double = 0.0;
                double MAT3TIEMPREAL_double = 0;
                if (Double.TryParse(MAT3TIEMPREAL.Text.Replace('.', ','), out MAT3TIEMPREAL_double))
                    MAT3TIEMPREAL_double = Convert.ToDouble(MAT3TIEMPREAL.Text.Replace('.', ','));
                else
                    MAT3TIEMPREAL_double = 0.0;
                double MAT3TEMP_double = 0;
                if (Double.TryParse(MAT3TEMP.Text.Replace('.', ','), out MAT3TEMP_double))
                    MAT3TEMP_double = Convert.ToDouble(MAT3TEMP.Text.Replace('.', ','));
                else
                    MAT3TEMP_double = 0.0;
                double MAT3TEMPREAL_double = 0;
                if (Double.TryParse(MAT3TEMPREAL.Text.Replace('.', ','), out MAT3TEMPREAL_double))
                    MAT3TEMPREAL_double = Convert.ToDouble(MAT3TEMPREAL.Text.Replace('.', ','));
                else
                    MAT3TEMPREAL_double = 0.0;

                //AUDITORIA
                int Q1E = 0;
                int Q2E = 0;
                int Q3E = 0;
                int Q4E = 0;
                int Q5E = 0;
                int Q6E = 0;
                int Q7E = 0;
                int Q8E = 0;
                int Q9E = 0;
                //int Q10E = 0;
                //int Q1C = 0;
                //int Q2C = 0;
                //int Q3C = 0;
                //int Q4C = 0;
                //int Q5C = 0;
                int Q6C = 0;
                int Q7C = 0;
                int Q8C = 0;
                int Q9C = 0;
                int Q10C = 0;
                //CONECTOR ESCRITURA

                Conexion_LIBERACIONES conexion = new Conexion_LIBERACIONES();
                conexion.Crear_ficha_liberacion(tbOrden.Text, Convert.ToInt32(tbReferencia.Text), tbMaquina.Text, version, Convert.ToInt32(tbMolde.Text), operario1numero, operario1horas, Convert.ToString(Operario1Nivel.SelectedValue), Operario1UltRevision.Text, Operario1Notas.Text, operario2numero, operario2horas, Convert.ToString(Operario2Nivel.SelectedValue),
                    Operario2UltRevision.Text, Operario2Notas.Text, encargadonumero, encargadohoras, cambiadornumero, cambiadorhoras, calidadnumero, calidadhoras, CambiadorLiberado, LiberacionCambiadorHora.Text, ProduccionLiberado, LiberacionEncargadoHora.Text, CalidadLiberado, LiberacionCalidadHora.Text, ResultadoLiberacion, Convert.ToInt32(LiberacionCondicionada.SelectedValue), NotaLiberacionCondicionada.Text, Reliberacion, NCEncargado, GP12Encargado, NCCalidad, GP12Calidad, Convert.ToDateTime(tbFechaCambio.Text).ToString("dd/MM/yyy HH:mm"),
                    //Valores POSTPRESION
                    thP1_double, thTP1_double, thP2_double, thTP2_double, thP3_double, thTP3_double, thP4_double, thTP4_double, thP5_double, thTP5_double, thP6_double, thTP6_double, thP7_double, thTP7_double, thP8_double, thTP8_double, thP9_double, thTP9_double, thP10_double, thTP10_double, tbConmutacion.Text, tbTiempoPresion.Text,
                    //VALORES CAMARA CALIENTE
                    thZ1_double, thZ2_double, thZ3_double, thZ4_double, thZ5_double, thZ6_double, thZ7_double, thZ8_double, thZ9_double, thZ10_double, thZ11_double, thZ12_double, thZ13_double, thZ14_double, thZ15_double, thZ16_double, thZ17_double, thZ18_double, thZ19_double, thZ20_double,
                    //VALORES TEMPERATURA CILINDRO
                    thBoq_double, thT1_double, thT2_double, thT3_double, thT4_double, thT5_double, thT6_double, thT7_double, thT8_double, thT9_double, thT10_double, EXISTEFICHA.Text,
                    //VALORES TOLERANCIAS 
                    tbTiempoInyeccion_double, tbLimitePresion_double, thVCarga_double, thCarga_double, thDescomp_double, thContrapr_double, thTiempo_double, thEnfriamiento_double, thCiclo_double, thCojin_double,
                    TiempoInyeccionNValDouble, TiempoInyeccionMValDouble, LimitePresionNValDouble, LimitePresionMValDouble, ConmuntaciontolNValDouble, ConmuntaciontolMValDouble, TiempoPresiontolNValDouble,
                    TiempoPresiontolMValDouble, TNvcargavalDouble, TMvcargavalDouble, TNcargavalDouble, TMcargavalDouble, TNdescomvalDouble, TMdescomvalDouble, TNcontrapvalDouble,
                    TMcontrapvalDouble, TNTiempdosvalDouble, TMTiempdosvalDouble, TNEnfriamvalDouble, TMEnfriamvalDouble, TNCiclovalDouble, TMCiclovalDouble, TNCojinvalDouble, TMCojinvalDouble,
                    //VALORES MATERIALES
                    MAT1REF.Text, MAT1NOM.Text, MAT1LOT.Text, MAT1TIEMP_double, MAT1TIEMPREAL_double, MAT1TEMP_double, MAT1TEMPREAL_double, MAT2REF.Text, MAT2NOM.Text, MAT2LOT.Text, MAT2TIEMP_double, MAT2TIEMPREAL_double, MAT2TEMP_double, MAT2TEMPREAL_double, MAT3REF.Text, MAT3NOM.Text, MAT3LOT.Text, MAT3TIEMP_double, MAT3TIEMPREAL_double, MAT3TEMP_double, MAT3TEMPREAL_double,
                    COMP1REF.Text, COMP1NOM.Text, COMP1LOT.Text, COMP2REF.Text, COMP2NOM.Text, COMP2LOT.Text, COMP3REF.Text, COMP3NOM.Text, COMP3LOT.Text, COMP4REF.Text, COMP4NOM.Text, COMP4LOT.Text, COMP5REF.Text, COMP5NOM.Text, COMP5LOT.Text, COMP6REF.Text, COMP6NOM.Text, COMP6LOT.Text, COMP7REF.Text, COMP7NOM.Text, COMP7LOT.Text,
                    //VALORES ATEMPERADO
                    TbCircuitoF1.Text, TbCircuitoF2.Text, TbCircuitoF3.Text, TbCircuitoF4.Text, TbCircuitoF5.Text, TbCircuitoF6.Text,
                    TbCaudalF1.Text, TbCaudalF2.Text, TbCaudalF3.Text, TbCaudalF4.Text, TbCaudalF5.Text, TbCaudalF6.Text,
                    TbTemperaturaF1.Text, TbTemperaturaF2.Text, TbTemperaturaF3.Text, TbTemperaturaF4.Text, TbTemperaturaF5.Text, TbTemperaturaF6.Text,
                    TbCircuitoM1.Text, TbCircuitoM2.Text, TbCircuitoM3.Text, TbCircuitoM4.Text, TbCircuitoM5.Text, TbCircuitoM6.Text,
                    TbCaudalM1.Text, TbCaudalM2.Text, TbCaudalM3.Text, TbCaudalM4.Text, TbCaudalM5.Text, TbCaudalM6.Text,
                    TbTemperaturaM1.Text, TbTemperaturaM2.Text, TbTemperaturaM3.Text, TbTemperaturaM4.Text, TbTemperaturaM5.Text, TbTemperaturaM6.Text,
                    //VALORES AUDITORIA
                    Q1E, TbQ1ENC.Text, Q2E, TbQ2ENC.Text, Q3E, TbQ3ENC.Text, Q4E, TbQ4ENC.Text, Q5E, TbQ5ENC.Text, Q6E, Q6C, TbQ6ENC.Text, TbQ6CAL.Text,
                    Q7E, Q7C, TbQ7ENC.Text, TbQ7CAL.Text, Q8E, Q8C, TbQ8ENC.Text, TbQ8CAL.Text, Q9E, Q9C, TbQ9ENC.Text, TbQ9CAL.Text, Q10C, TbQ10CAL.Text, QXFeedbackCambiador.Text, QXFeedbackProduccion.Text
                    ); 

            }
            catch (Exception)
            {
            }
        }

        //CARGA DESDE LIBERACIÓN CREADA      
        public void CargarFichaLiberacion()
        {
            try
            {
                alertaoperario.Visible = false;
                Conexion_LIBERACIONES conexion = new Conexion_LIBERACIONES();
                DataSet ds = conexion.CargaLiberacionFicha(tbOrden.Text);

                CalidadNumero.Text = ds.Tables[0].Rows[0]["Calidad"].ToString();
                CalidadNombre.Text = conexion.Devuelve_Nombre_Operario(ds.Tables[0].Rows[0]["Calidad"].ToString());
                CalidadHoras.Text = ds.Tables[0].Rows[0]["CalidadHoras"].ToString();

                EncargadoNumero.Text = ds.Tables[0].Rows[0]["Encargado"].ToString();
                EncargadoNombre.Text = conexion.Devuelve_Nombre_Operario(ds.Tables[0].Rows[0]["Encargado"].ToString());
                EncargadoHoras.Text = ds.Tables[0].Rows[0]["EncargadoHoras"].ToString();

                CambiadorNumero.Text = ds.Tables[0].Rows[0]["Cambiador"].ToString();
                CambiadorHoras.Text = ds.Tables[0].Rows[0]["CambiadorHoras"].ToString();
                CambiadorNombre.Text = conexion.Devuelve_Nombre_Operario(ds.Tables[0].Rows[0]["Cambiador"].ToString());

                Operario1Numero.Text = ds.Tables[0].Rows[0]["Operario1"].ToString();
                Operario1Nombre.Text = conexion.Devuelve_Nombre_Operario(ds.Tables[0].Rows[0]["Operario1"].ToString());
                Operario1Horas.Text = ds.Tables[0].Rows[0]["Operario1Horas"].ToString();
                Operario1Nivel.SelectedValue = ds.Tables[0].Rows[0]["Operario1Nivel"].ToString();
                Operario1Notas.Text = ds.Tables[0].Rows[0]["Operario1Notas"].ToString();
                if (Operario1Nivel.SelectedValue.ToString() == "I")
                {
                    alertaoperario.Visible = true;
                }

                Operario2Numero.Text = ds.Tables[0].Rows[0]["Operario2"].ToString();
                Operario2Nombre.Text = conexion.Devuelve_Nombre_Operario(ds.Tables[0].Rows[0]["Operario2"].ToString());
                Operario2Horas.Text = ds.Tables[0].Rows[0]["Operario2Horas"].ToString();
                Operario2Nivel.SelectedValue = ds.Tables[0].Rows[0]["Operario2Nivel"].ToString();
                Operario2Notas.Text = ds.Tables[0].Rows[0]["Operario2Notas"].ToString();
                if (Operario2Numero.Text != "0")
                {
                    Operario2Posicion.Visible = true;
                    Operario2Nivel.Visible = true;
                    Operario2Horas.Visible = true;
                    Operario2Nombre.Visible = true;
                    Operario2UltRevision.Visible = true;
                    Operario2Numero.Visible = true;
                    Operario2Notas.Visible = true;
                    if (Operario2Nivel.SelectedValue.ToString() == "I")
                    {
                        alertaoperario.Visible = true;
                    }
                }

                LiberacionCondicionada.SelectedValue = ds.Tables[0].Rows[0]["AccionLiberado"].ToString();
                NotaLiberacionCondicionada.Text = ds.Tables[0].Rows[0]["NotasLiberado"].ToString();

                int liberacioncambiador = Convert.ToInt32(ds.Tables[0].Rows[0]["CambiadorLiberado"]);
                LiberacionCambiadorHoraORI.Text = ds.Tables[0].Rows[0]["ORIFechaCambiadorLiberado"].ToString(); //LIBNUEVO
                if (ds.Tables[0].Rows[0]["ORIFechaCambiadorLiberado"].ToString() == ds.Tables[0].Rows[0]["FechaCambiadorLiberado"].ToString())
                    {
                    LiberacionCambiadorHora.Text = "";
                    }
                else
                    {
                    LiberacionCambiadorHora.Text = ds.Tables[0].Rows[0]["FechaCambiadorLiberado"].ToString();
                    }

                
                //LiberacionCambiadorNotas.Text
                switch (liberacioncambiador)
                {
                    case 0:
                        LiberacionCambiador.Text = "Sin liberar";
                        estadocambiadorSN.Visible = true;
                        estadocambiadorCOND.Visible = false;
                        estadocambiadorLIBOK.Visible = false;
                        break;
                    case 1:
                        LiberacionCambiador.Text = "Liberada OK condicionada";
                        estadocambiadorSN.Visible = false;
                        estadocambiadorCOND.Visible = true;
                        estadocambiadorLIBOK.Visible = false;
                        break;
                    case 2:
                        LiberacionCambiador.Text = "Liberada OK";
                        estadocambiadorSN.Visible = false;
                        estadocambiadorCOND.Visible = false;
                        estadocambiadorLIBOK.Visible = true;
                        break;
                }

                int liberacionencargado = Convert.ToInt32(ds.Tables[0].Rows[0]["ProduccionLiberado"]);
                LiberacionEncargadoHoraORI.Text = ds.Tables[0].Rows[0]["ORIFechaProduccionLiberado"].ToString(); //LIBNUEVO

                if (ds.Tables[0].Rows[0]["ORIFechaProduccionLiberado"].ToString() == ds.Tables[0].Rows[0]["FechaProduccionLiberado"].ToString())
                    {
                    LiberacionEncargadoHora.Text = "";
                    }
                else
                    {
                    LiberacionEncargadoHora.Text = ds.Tables[0].Rows[0]["FechaProduccionLiberado"].ToString();
                    }
                
                //LiberacionEncargadoNotas.Text
                switch (liberacionencargado)
                {
                    case 0:
                        LiberacionEncargado.Text = "Sin liberar";
                        estadoencargadoSN.Visible = true;
                        estadoencargadoCOND.Visible = false;
                        estadoencargadoLIBOK.Visible = false;

                        break;
                    case 1:
                        LiberacionEncargado.Text = "Liberada OK condicionada";
                        estadoencargadoSN.Visible = false;
                        estadoencargadoCOND.Visible = true;
                        estadoencargadoLIBOK.Visible = false;
                        break;
                    case 2:
                        LiberacionEncargado.Text = "Liberada OK";
                        estadoencargadoSN.Visible = false;
                        estadoencargadoCOND.Visible = false;
                        estadoencargadoLIBOK.Visible = true;
                        break;
                }

                int liberacionauditoria = Convert.ToInt32(ds.Tables[0].Rows[0]["CalidadLiberado"]);

                LiberacionCalidadHoraORI.Text = ds.Tables[0].Rows[0]["ORIFechaCalidadLiberado"].ToString(); //LIBNUEVO
                
                if (ds.Tables[0].Rows[0]["ORIFechaCalidadLiberado"].ToString() == ds.Tables[0].Rows[0]["FechaCalidadLiberado"].ToString())
                    {
                    LiberacionCalidadHora.Text = "";
                    }
                else
                    {
                    LiberacionCalidadHora.Text = ds.Tables[0].Rows[0]["FechaCalidadLiberado"].ToString();
                    }
                //LiberaciónCalidadNotas.Text
                switch (liberacionauditoria)
                {
                    case 0:
                        LiberacionCalidad.Text = "Sin liberar";
                        estadocalidadSN.Visible = true;
                        estadocalidadCOND.Visible = false;
                        estadocalidadLIBOK.Visible = false;
                        break;
                    case 1:
                        LiberacionCalidad.Text = "Liberada OK condicionada";
                        estadocalidadSN.Visible = false;
                        estadocalidadCOND.Visible = true;
                        estadocalidadLIBOK.Visible = false;
                        break;
                    case 2:
                        LiberacionCalidad.Text = "Liberada OK";
                        estadocalidadSN.Visible = false;
                        estadocalidadCOND.Visible = false;
                        estadocalidadLIBOK.Visible = true;
                        break;
                }

                int ENCNoconformidad = Convert.ToInt32(ds.Tables[0].Rows[0]["ENCNoconformidad"]);
                if (ENCNoconformidad == 1)
                {
                    A3.Visible = false;
                    A3OK.Visible = true;
                }
                int ENCDefectos = Convert.ToInt32(ds.Tables[0].Rows[0]["ENCDefectos"]);
                if (ENCDefectos == 1)
                {
                    A5.Visible = false;
                    A5OK.Visible = true;
                }
                int CALNoconformidad = Convert.ToInt32(ds.Tables[0].Rows[0]["CALNoconformidad"]);
                if (CALNoconformidad == 1)
                {
                    A7.Visible = false;
                    A7OK.Visible = true;
                }
                int CALDefectos = Convert.ToInt32(ds.Tables[0].Rows[0]["CALDefectos"]);
                if (CALDefectos == 1)
                {
                    A8.Visible = false;
                    A8OK.Visible = true;
                }

                int ValidadoPOR = Convert.ToInt32(ds.Tables[0].Rows[0]["ValidadoING"].ToString());
                if (ValidadoPOR > 0)
                {
                    Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                    DropValidadJefeProyecto.SelectedValue = SHConexion.Devuelve_Pilotos_SMARTH(ValidadoPOR);
                }

            }

            catch (Exception)
            { }

        }
        public void CargarParametrosLiberacion()
        {
            try
            {
                Conexion_LIBERACIONES conexion = new Conexion_LIBERACIONES();
                DataSet ds = conexion.LeerTempCilindroLIBERACION(Convert.ToInt32(tbOrden.Text));
                thBoq.Text = ds.Tables[0].Rows[0]["Boq"].ToString();
                thT1.Text = ds.Tables[0].Rows[0]["T1"].ToString();
                thT2.Text = ds.Tables[0].Rows[0]["T2"].ToString();
                thT3.Text = ds.Tables[0].Rows[0]["T3"].ToString();
                thT4.Text = ds.Tables[0].Rows[0]["T4"].ToString();
                thT5.Text = ds.Tables[0].Rows[0]["T5"].ToString();
                thT6.Text = ds.Tables[0].Rows[0]["T6"].ToString();
                thT7.Text = ds.Tables[0].Rows[0]["T7"].ToString();
                thT8.Text = ds.Tables[0].Rows[0]["T8"].ToString();
                thT9.Text = ds.Tables[0].Rows[0]["T9"].ToString();
                thT10.Text = ds.Tables[0].Rows[0]["T10"].ToString();
                thBoqREAL.Text = ds.Tables[0].Rows[0]["LIBBoq"].ToString();
                thT1REAL.Text = ds.Tables[0].Rows[0]["LIBT1"].ToString();
                thT2REAL.Text = ds.Tables[0].Rows[0]["LIBT2"].ToString();
                thT3REAL.Text = ds.Tables[0].Rows[0]["LIBT3"].ToString();
                thT4REAL.Text = ds.Tables[0].Rows[0]["LIBT4"].ToString();
                thT5REAL.Text = ds.Tables[0].Rows[0]["LIBT5"].ToString();
                thT6REAL.Text = ds.Tables[0].Rows[0]["LIBT6"].ToString();
                thT7REAL.Text = ds.Tables[0].Rows[0]["LIBT7"].ToString();
                thT8REAL.Text = ds.Tables[0].Rows[0]["LIBT8"].ToString();
                thT9REAL.Text = ds.Tables[0].Rows[0]["LIBT9"].ToString();
                thT10REAL.Text = ds.Tables[0].Rows[0]["LIBT10"].ToString();
                EXISTEFICHA.Text = ds.Tables[0].Rows[0]["EXISTEFICHA"].ToString();
                if (EXISTEFICHA.Text == "1")
                    { alertafichafabricacion.Visible = true; }

                ds = conexion.LeerTempCamCalienteLIBERACION(Convert.ToInt32(tbOrden.Text));
                thZ1.Text = ds.Tables[0].Rows[0]["Z1"].ToString();
                thZ2.Text = ds.Tables[0].Rows[0]["Z2"].ToString();
                thZ3.Text = ds.Tables[0].Rows[0]["Z3"].ToString();
                thZ4.Text = ds.Tables[0].Rows[0]["Z4"].ToString();
                thZ5.Text = ds.Tables[0].Rows[0]["Z5"].ToString();
                thZ6.Text = ds.Tables[0].Rows[0]["Z6"].ToString();
                thZ7.Text = ds.Tables[0].Rows[0]["Z7"].ToString();
                thZ8.Text = ds.Tables[0].Rows[0]["Z8"].ToString();
                thZ9.Text = ds.Tables[0].Rows[0]["Z9"].ToString();
                thZ10.Text = ds.Tables[0].Rows[0]["Z10"].ToString();
                thZ11.Text = ds.Tables[0].Rows[0]["Z11"].ToString();
                thZ12.Text = ds.Tables[0].Rows[0]["Z12"].ToString();
                thZ13.Text = ds.Tables[0].Rows[0]["Z13"].ToString();
                thZ14.Text = ds.Tables[0].Rows[0]["Z14"].ToString();
                thZ15.Text = ds.Tables[0].Rows[0]["Z15"].ToString();
                thZ16.Text = ds.Tables[0].Rows[0]["Z16"].ToString();
                thZ17.Text = ds.Tables[0].Rows[0]["Z17"].ToString();
                thZ18.Text = ds.Tables[0].Rows[0]["Z18"].ToString();
                thZ19.Text = ds.Tables[0].Rows[0]["Z19"].ToString();
                thZ20.Text = ds.Tables[0].Rows[0]["Z20"].ToString();
                thZ1REAL.Text = ds.Tables[0].Rows[0]["LIBZ1"].ToString();
                thZ2REAL.Text = ds.Tables[0].Rows[0]["LIBZ2"].ToString();
                thZ3REAL.Text = ds.Tables[0].Rows[0]["LIBZ3"].ToString();
                thZ4REAL.Text = ds.Tables[0].Rows[0]["LIBZ4"].ToString();
                thZ5REAL.Text = ds.Tables[0].Rows[0]["LIBZ5"].ToString();
                thZ6REAL.Text = ds.Tables[0].Rows[0]["LIBZ6"].ToString();
                thZ7REAL.Text = ds.Tables[0].Rows[0]["LIBZ7"].ToString();
                thZ8REAL.Text = ds.Tables[0].Rows[0]["LIBZ8"].ToString();
                thZ9REAL.Text = ds.Tables[0].Rows[0]["LIBZ9"].ToString();
                thZ10REAL.Text = ds.Tables[0].Rows[0]["LIBZ10"].ToString();
                thZ11REAL.Text = ds.Tables[0].Rows[0]["LIBZ11"].ToString();
                thZ12REAL.Text = ds.Tables[0].Rows[0]["LIBZ12"].ToString();
                thZ13REAL.Text = ds.Tables[0].Rows[0]["LIBZ13"].ToString();
                thZ14REAL.Text = ds.Tables[0].Rows[0]["LIBZ14"].ToString();
                thZ15REAL.Text = ds.Tables[0].Rows[0]["LIBZ15"].ToString();
                thZ16REAL.Text = ds.Tables[0].Rows[0]["LIBZ16"].ToString();
                thZ17REAL.Text = ds.Tables[0].Rows[0]["LIBZ17"].ToString();
                thZ18REAL.Text = ds.Tables[0].Rows[0]["LIBZ18"].ToString();
                thZ19REAL.Text = ds.Tables[0].Rows[0]["LIBZ19"].ToString();
                thZ20REAL.Text = ds.Tables[0].Rows[0]["LIBZ20"].ToString();

                ds = conexion.LeerPostpresionLIBERACION(Convert.ToInt32(tbOrden.Text));
                thP1.Text = ds.Tables[0].Rows[0]["P1"].ToString();
                thP2.Text = ds.Tables[0].Rows[0]["P2"].ToString();
                thP3.Text = ds.Tables[0].Rows[0]["P3"].ToString();
                thP4.Text = ds.Tables[0].Rows[0]["P4"].ToString();
                thP5.Text = ds.Tables[0].Rows[0]["P5"].ToString();
                thP6.Text = ds.Tables[0].Rows[0]["P6"].ToString();
                thP7.Text = ds.Tables[0].Rows[0]["P7"].ToString();
                thP8.Text = ds.Tables[0].Rows[0]["P8"].ToString();
                thP9.Text = ds.Tables[0].Rows[0]["P9"].ToString();
                thP10.Text = ds.Tables[0].Rows[0]["P10"].ToString();
                thTP1.Text = ds.Tables[0].Rows[0]["T1"].ToString();
                thTP2.Text = ds.Tables[0].Rows[0]["T2"].ToString();
                thTP3.Text = ds.Tables[0].Rows[0]["T3"].ToString();
                thTP4.Text = ds.Tables[0].Rows[0]["T4"].ToString();
                thTP5.Text = ds.Tables[0].Rows[0]["T5"].ToString();
                thTP6.Text = ds.Tables[0].Rows[0]["T6"].ToString();
                thTP7.Text = ds.Tables[0].Rows[0]["T7"].ToString();
                thTP8.Text = ds.Tables[0].Rows[0]["T8"].ToString();
                thTP9.Text = ds.Tables[0].Rows[0]["T9"].ToString();
                thTP10.Text = ds.Tables[0].Rows[0]["T10"].ToString();
                tbConmutacion.Text = ds.Tables[0].Rows[0]["Conmutacion"].ToString();
                tbTiempoPresion.Text = ds.Tables[0].Rows[0]["TiempoPresion"].ToString();
                thP1R.Text = ds.Tables[0].Rows[0]["LIBP1"].ToString();
                thP2R.Text = ds.Tables[0].Rows[0]["LIBP2"].ToString();
                thP3R.Text = ds.Tables[0].Rows[0]["LIBP3"].ToString();
                thP4R.Text = ds.Tables[0].Rows[0]["LIBP4"].ToString();
                thP5R.Text = ds.Tables[0].Rows[0]["LIBP5"].ToString();
                thP6R.Text = ds.Tables[0].Rows[0]["LIBP6"].ToString();
                thP7R.Text = ds.Tables[0].Rows[0]["LIBP7"].ToString();
                thP8R.Text = ds.Tables[0].Rows[0]["LIBP8"].ToString();
                thP9R.Text = ds.Tables[0].Rows[0]["LIBP9"].ToString();
                thP10R.Text = ds.Tables[0].Rows[0]["LIBP10"].ToString();
                thTP1R.Text = ds.Tables[0].Rows[0]["LIBT1"].ToString();
                thTP2R.Text = ds.Tables[0].Rows[0]["LIBT2"].ToString();
                thTP3R.Text = ds.Tables[0].Rows[0]["LIBT3"].ToString();
                thTP4R.Text = ds.Tables[0].Rows[0]["LIBT4"].ToString();
                thTP5R.Text = ds.Tables[0].Rows[0]["LIBT5"].ToString();
                thTP6R.Text = ds.Tables[0].Rows[0]["LIBT6"].ToString();
                thTP7R.Text = ds.Tables[0].Rows[0]["LIBT7"].ToString();
                thTP8R.Text = ds.Tables[0].Rows[0]["LIBT8"].ToString();
                thTP9R.Text = ds.Tables[0].Rows[0]["LIBT9"].ToString();
                thTP10R.Text = ds.Tables[0].Rows[0]["LIBT10"].ToString();
                tbConmutacionREAL.Text = ds.Tables[0].Rows[0]["LIBConmutacion"].ToString();
                tbTiempoPresionREAL.Text = ds.Tables[0].Rows[0]["LIBTiempoPresion"].ToString();

                ds = conexion.LeerAtemperadoLIBERACION(Convert.ToInt32(tbOrden.Text));
                TbCircuitoM1.Text = ds.Tables[0].Rows[0]["CircuitoM1"].ToString();
                TbCircuitoM2.Text = ds.Tables[0].Rows[0]["CircuitoM2"].ToString();
                TbCircuitoM3.Text = ds.Tables[0].Rows[0]["CircuitoM3"].ToString();
                TbCircuitoM4.Text = ds.Tables[0].Rows[0]["CircuitoM4"].ToString();
                TbCircuitoM5.Text = ds.Tables[0].Rows[0]["CircuitoM5"].ToString();
                TbCircuitoM6.Text = ds.Tables[0].Rows[0]["CircuitoM6"].ToString();
                TbCircuitoF1.Text = ds.Tables[0].Rows[0]["CircuitoF1"].ToString();
                TbCircuitoF2.Text = ds.Tables[0].Rows[0]["CircuitoF2"].ToString();
                TbCircuitoF3.Text = ds.Tables[0].Rows[0]["CircuitoF3"].ToString();
                TbCircuitoF4.Text = ds.Tables[0].Rows[0]["CircuitoF4"].ToString();
                TbCircuitoF5.Text = ds.Tables[0].Rows[0]["CircuitoF5"].ToString();
                TbCircuitoF6.Text = ds.Tables[0].Rows[0]["CircuitoF6"].ToString();
                //CAUDALES
                //PARTE FIJA
                TbCaudalF1.Text = ds.Tables[0].Rows[0]["CaudalF1"].ToString();
                TbCaudalF2.Text = ds.Tables[0].Rows[0]["CaudalF2"].ToString();
                TbCaudalF3.Text = ds.Tables[0].Rows[0]["CaudalF3"].ToString();
                TbCaudalF4.Text = ds.Tables[0].Rows[0]["CaudalF4"].ToString();
                TbCaudalF5.Text = ds.Tables[0].Rows[0]["CaudalF5"].ToString();
                TbCaudalF6.Text = ds.Tables[0].Rows[0]["CaudalF6"].ToString();
                //PARTE MOVIL
                TbCaudalM1.Text = ds.Tables[0].Rows[0]["CaudalM1"].ToString();
                TbCaudalM2.Text = ds.Tables[0].Rows[0]["CaudalM2"].ToString();
                TbCaudalM3.Text = ds.Tables[0].Rows[0]["CaudalM3"].ToString();
                TbCaudalM4.Text = ds.Tables[0].Rows[0]["CaudalM4"].ToString();
                TbCaudalM5.Text = ds.Tables[0].Rows[0]["CaudalM5"].ToString();
                TbCaudalM6.Text = ds.Tables[0].Rows[0]["CaudalM6"].ToString();
                //TEMPERATURAS
                //PARTE FIJA
                TbTemperaturaF1.Text = ds.Tables[0].Rows[0]["TemperaturaF1"].ToString();
                TbTemperaturaF2.Text = ds.Tables[0].Rows[0]["TemperaturaF2"].ToString();
                TbTemperaturaF3.Text = ds.Tables[0].Rows[0]["TemperaturaF3"].ToString();
                TbTemperaturaF4.Text = ds.Tables[0].Rows[0]["TemperaturaF4"].ToString();
                TbTemperaturaF5.Text = ds.Tables[0].Rows[0]["TemperaturaF5"].ToString();
                TbTemperaturaF6.Text = ds.Tables[0].Rows[0]["TemperaturaF6"].ToString();
                //PARTE MOVIL
                TbTemperaturaM1.Text = ds.Tables[0].Rows[0]["TemperaturaM1"].ToString();
                TbTemperaturaM2.Text = ds.Tables[0].Rows[0]["TemperaturaM2"].ToString();
                TbTemperaturaM3.Text = ds.Tables[0].Rows[0]["TemperaturaM3"].ToString();
                TbTemperaturaM4.Text = ds.Tables[0].Rows[0]["TemperaturaM4"].ToString();
                TbTemperaturaM5.Text = ds.Tables[0].Rows[0]["TemperaturaM5"].ToString();
                TbTemperaturaM6.Text = ds.Tables[0].Rows[0]["TemperaturaM6"].ToString();
                //CAUDALES LIBERADOS
                //PARTE FIJA LIBERADOS
                TbCaudalF1REAL.Text = ds.Tables[0].Rows[0]["LIBCaudalF1"].ToString();
                TbCaudalF2REAL.Text = ds.Tables[0].Rows[0]["LIBCaudalF2"].ToString();
                TbCaudalF3REAL.Text = ds.Tables[0].Rows[0]["LIBCaudalF3"].ToString();
                TbCaudalF4REAL.Text = ds.Tables[0].Rows[0]["LIBCaudalF4"].ToString();
                TbCaudalF5REAL.Text = ds.Tables[0].Rows[0]["LIBCaudalF5"].ToString();
                TbCaudalF6REAL.Text = ds.Tables[0].Rows[0]["LIBCaudalF6"].ToString();
                //PARTE MOVIL LIBERADOS
                TbCaudalM1REAL.Text = ds.Tables[0].Rows[0]["LIBCaudalM1"].ToString();
                TbCaudalM2REAL.Text = ds.Tables[0].Rows[0]["LIBCaudalM2"].ToString();
                TbCaudalM3REAL.Text = ds.Tables[0].Rows[0]["LIBCaudalM3"].ToString();
                TbCaudalM4REAL.Text = ds.Tables[0].Rows[0]["LIBCaudalM4"].ToString();
                TbCaudalM5REAL.Text = ds.Tables[0].Rows[0]["LIBCaudalM5"].ToString();
                TbCaudalM6REAL.Text = ds.Tables[0].Rows[0]["LIBCaudalM6"].ToString();

                //TEMPERATURAS LIBERADOS
                //PARTE FIJA LIBERADOS
                TbTemperaturaF1REAL.Text = ds.Tables[0].Rows[0]["LIBTemperaturaF1"].ToString();
                TbTemperaturaF2REAL.Text = ds.Tables[0].Rows[0]["LIBTemperaturaF2"].ToString();
                TbTemperaturaF3REAL.Text = ds.Tables[0].Rows[0]["LIBTemperaturaF3"].ToString();
                TbTemperaturaF4REAL.Text = ds.Tables[0].Rows[0]["LIBTemperaturaF4"].ToString();
                TbTemperaturaF5REAL.Text = ds.Tables[0].Rows[0]["LIBTemperaturaF5"].ToString();
                TbTemperaturaF6REAL.Text = ds.Tables[0].Rows[0]["LIBTemperaturaF6"].ToString();
                //PARTE MOVIL LREALIBERADOS
                TbTemperaturaM1REAL.Text = ds.Tables[0].Rows[0]["LIBTemperaturaM1"].ToString();
                TbTemperaturaM2REAL.Text = ds.Tables[0].Rows[0]["LIBTemperaturaM2"].ToString();
                TbTemperaturaM3REAL.Text = ds.Tables[0].Rows[0]["LIBTemperaturaM3"].ToString();
                TbTemperaturaM4REAL.Text = ds.Tables[0].Rows[0]["LIBTemperaturaM4"].ToString();
                TbTemperaturaM5REAL.Text = ds.Tables[0].Rows[0]["LIBTemperaturaM5"].ToString();
                TbTemperaturaM6REAL.Text = ds.Tables[0].Rows[0]["LIBTemperaturaM6"].ToString();

                //LEER TOLERANCIAS
                ds = conexion.LeerToleranciasLIBERACION(Convert.ToInt32(tbOrden.Text));
                tbTiempoInyeccionNVal.Text = ds.Tables[0].Rows[0]["TiempoInyeccionNVal"].ToString();
                tbTiempoInyeccionMVal.Text = ds.Tables[0].Rows[0]["TiempoInyeccionMVal"].ToString();
                tbLimitePresionNVal.Text = ds.Tables[0].Rows[0]["LimitePresionNVal"].ToString();
                tbLimitePresionMVal.Text = ds.Tables[0].Rows[0]["LimitePresionMVal"].ToString();
                thConmuntaciontolNVal.Text = ds.Tables[0].Rows[0]["ConmuntaciontolNVal"].ToString();
                thConmuntaciontolMVal.Text = ds.Tables[0].Rows[0]["ConmuntaciontolMVal"].ToString();
                tbTiempoPresiontolNVal.Text = ds.Tables[0].Rows[0]["TiempoPresiontolNVal"].ToString();
                tbTiempoPresiontolMVal.Text = ds.Tables[0].Rows[0]["TiempoPresiontolMVal"].ToString();
                TNvcargaval.Text = ds.Tables[0].Rows[0]["TNvcargaval"].ToString();
                TMvcargaval.Text = ds.Tables[0].Rows[0]["TMvcargaval"].ToString();
                TNcargaval.Text = ds.Tables[0].Rows[0]["TNcargaval"].ToString();
                TMcargaval.Text = ds.Tables[0].Rows[0]["TMcargaval"].ToString();
                TNdescomval.Text = ds.Tables[0].Rows[0]["TNdescomval"].ToString();
                TMdescomval.Text = ds.Tables[0].Rows[0]["TMdescomval"].ToString();
                TNcontrapval.Text = ds.Tables[0].Rows[0]["TNcontrapval"].ToString();
                TMcontrapval.Text = ds.Tables[0].Rows[0]["TMcontrapval"].ToString();
                TNTiempdosval.Text = ds.Tables[0].Rows[0]["TNTiempdosval"].ToString();
                TMTiempdosval.Text = ds.Tables[0].Rows[0]["TMTiempdosval"].ToString();
                TNEnfriamval.Text = ds.Tables[0].Rows[0]["TNEnfriamval"].ToString();
                TMEnfriamval.Text = ds.Tables[0].Rows[0]["TMEnfriamval"].ToString();
                TNCicloval.Text = ds.Tables[0].Rows[0]["TNCicloval"].ToString();
                TMCicloval.Text = ds.Tables[0].Rows[0]["TMCicloval"].ToString();
                TNCojinval.Text = ds.Tables[0].Rows[0]["TNCojinval"].ToString();
                TMCojinval.Text = ds.Tables[0].Rows[0]["TMCojinval"].ToString();

                tbTiempoInyeccion.Text = ds.Tables[0].Rows[0]["TiempoInyeccion"].ToString();
                tbLimitePresion.Text = ds.Tables[0].Rows[0]["LimitePresion"].ToString();
                thVCarga.Text = ds.Tables[0].Rows[0]["VelocidadCarga"].ToString();
                thCarga.Text = ds.Tables[0].Rows[0]["Carga"].ToString();
                thDescomp.Text = ds.Tables[0].Rows[0]["Descompresion"].ToString();
                thContrapr.Text = ds.Tables[0].Rows[0]["Contrapresion"].ToString();
                thTiempo.Text = ds.Tables[0].Rows[0]["Tiempo"].ToString();
                thEnfriamiento.Text = ds.Tables[0].Rows[0]["Enfriamiento"].ToString();
                thCiclo.Text = ds.Tables[0].Rows[0]["Ciclo"].ToString();
                thCojin.Text = ds.Tables[0].Rows[0]["Cojin"].ToString();

                tbTiempoInyeccionREAL.Text = ds.Tables[0].Rows[0]["LIBTiempoInyeccion"].ToString();
                tbLimitePresionREAL.Text = ds.Tables[0].Rows[0]["LIBLimitePresion"].ToString();
                thVCargaREAL.Text = ds.Tables[0].Rows[0]["LIBVelocidadCarga"].ToString();
                thCargaREAL.Text = ds.Tables[0].Rows[0]["LIBCarga"].ToString();
                thDescompREAL.Text = ds.Tables[0].Rows[0]["LIBDescompresion"].ToString();
                thContraprREAL.Text = ds.Tables[0].Rows[0]["LIBContrapresion"].ToString();
                thTiempoREAL.Text = ds.Tables[0].Rows[0]["LIBTiempo"].ToString();
                thEnfriamientoREAL.Text = ds.Tables[0].Rows[0]["LIBEnfriamiento"].ToString();
                thCicloREAL.Text = ds.Tables[0].Rows[0]["LIBCiclo"].ToString();
                thCojinREAL.Text = ds.Tables[0].Rows[0]["LIBCojin"].ToString();

            }
            catch (Exception)
            {
            }
        }
        public void CargarCuestionariosLiberacion()
        { 
            try
            {
                Conexion_LIBERACIONES conexion = new Conexion_LIBERACIONES();
                DataSet ds = conexion.LeerAuditoriaLIBERACION(Convert.ToInt32(tbOrden.Text));
                QXFeedbackCambiador.Text = ds.Tables[0].Rows[0]["QXFeedbackCambiador"].ToString();
                LiberacionCambiadorNotas.Text = ds.Tables[0].Rows[0]["QXFeedbackCambiador"].ToString();
                QXFeedbackProduccion.Text = ds.Tables[0].Rows[0]["QXFeedbackProduccion"].ToString();
                LiberacionEncargadoNotas.Text = ds.Tables[0].Rows[0]["QXFeedbackProduccion"].ToString();
                QXFeedbackCalidad.Text = ds.Tables[0].Rows[0]["QXFeedbackCalidad"].ToString();
                LiberacionCalidadNotas.Text = ds.Tables[0].Rows[0]["QXFeedbackCalidad"].ToString();

                int Q1E = Convert.ToInt32(ds.Tables[0].Rows[0]["Q1E"]);
                TbQ1ENC.Text = ds.Tables[0].Rows[0]["Q1ENC"].ToString();
                switch (Q1E)
                {
                    case 0:
                        Q1_OK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q1_OKOPT.Checked = false;
                        Q1_NOK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q1_NOKOPT.Checked = false;
                        Q1_NA.Attributes.Add("Class", "btn btn-lg btn-primary active");
                        Q1_NAOPT.Checked = true;
                        break;
                    case 1:
                        Q1_OK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q1_OKOPT.Checked = false;
                        Q1_NOK.Attributes.Add("Class", "btn btn-lg btn-primary active");
                            Q1_NOK.Attributes.Add("style", "background-color:red"); 
                        Q1_NOKOPT.Checked = true;
                        Q1_NA.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q1_NAOPT.Checked = false;
                        break;
                    case 2:
                        Q1_OK.Attributes.Add("Class", "btn btn-lg btn-primary active");
                            Q1_OK.Attributes.Add("style", "background-color:forestgreen");
                        Q1_OKOPT.Checked = true;
                        Q1_NOK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q1_NOKOPT.Checked = false;
                        Q1_NA.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q1_NAOPT.Checked = false;
                        break;
                }

                int Q2E = Convert.ToInt32(ds.Tables[0].Rows[0]["Q2E"]);
                TbQ2ENC.Text = ds.Tables[0].Rows[0]["Q2ENC"].ToString();
                switch (Q2E)
                {
                    case 0:
                        Q2_OK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q2_OKOPT.Checked = false;
                        Q2_NOK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q2_NOKOPT.Checked = false;
                        Q2_NA.Attributes.Add("Class", "btn btn-lg btn-primary active");
                        Q2_NAOPT.Checked = true;
                        break;
                    case 1:
                        Q2_OK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q2_OKOPT.Checked = false;
                        Q2_NOK.Attributes.Add("Class", "btn btn-lg btn-primary active");
                            Q2_NOK.Attributes.Add("style", "background-color:red");
                        Q2_NOKOPT.Checked = true;
                        Q2_NA.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q2_NAOPT.Checked = false;
                        break;
                    case 2:
                        Q2_OK.Attributes.Add("Class", "btn btn-lg btn-primary active");
                            Q2_OK.Attributes.Add("style", "background-color:forestgreen");
                        Q2_OKOPT.Checked = true;
                        Q2_NOK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q2_NOKOPT.Checked = false;
                        Q2_NA.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q2_NAOPT.Checked = false;
                        break;
                }
                int Q3E = Convert.ToInt32(ds.Tables[0].Rows[0]["Q3E"]);
                TbQ3ENC.Text = ds.Tables[0].Rows[0]["Q3ENC"].ToString();
                switch (Q3E)
                {
                    case 0:
                        Q3_OK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q3_OKOPT.Checked = false;
                        Q3_NOK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q3_NOKOPT.Checked = false;
                        Q3_NA.Attributes.Add("Class", "btn btn-lg btn-primary active");
                        Q3_NAOPT.Checked = true;
                        break;
                    case 1:
                        Q3_OK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q3_OKOPT.Checked = false;
                        Q3_NOK.Attributes.Add("Class", "btn btn-lg btn-primary active");
                            Q3_NOK.Attributes.Add("style", "background-color:red");
                        Q3_NOKOPT.Checked = true;
                        Q3_NA.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q3_NAOPT.Checked = false;
                        break;
                    case 2:
                        Q3_OK.Attributes.Add("Class", "btn btn-lg btn-primary active");
                            Q3_OK.Attributes.Add("style", "background-color:forestgreen");
                        Q3_OKOPT.Checked = true;
                        Q3_NOK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q3_NOKOPT.Checked = false;
                        Q3_NA.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q3_NAOPT.Checked = false;
                        break;
                }
                int Q4E = Convert.ToInt32(ds.Tables[0].Rows[0]["Q4E"]);
                TbQ4ENC.Text = ds.Tables[0].Rows[0]["Q4ENC"].ToString();
                switch (Q4E)
                {
                    case 0:
                        Q4_OK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q4_OKOPT.Checked = false;
                        Q4_NOK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q4_NOKOPT.Checked = false;
                        Q4_NA.Attributes.Add("Class", "btn btn-lg btn-primary active");
                        Q4_NAOPT.Checked = true;
                        break;
                    case 1:
                        Q4_OK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q4_OKOPT.Checked = false;
                        Q4_NOK.Attributes.Add("Class", "btn btn-lg btn-primary active");
                            Q4_NOK.Attributes.Add("style", "background-color:red");
                        Q4_NOKOPT.Checked = true;
                        Q4_NA.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q4_NAOPT.Checked = false;
                        break;
                    case 2:
                        Q4_OK.Attributes.Add("Class", "btn btn-lg btn-primary active");
                            Q4_OK.Attributes.Add("style", "background-color:forestgreen");
                        Q4_OKOPT.Checked = true;
                        Q4_NOK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q4_NOKOPT.Checked = false;
                        Q4_NA.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q4_NAOPT.Checked = false;
                        break;
                }


                int Q5E = Convert.ToInt32(ds.Tables[0].Rows[0]["Q5E"]);
                TbQ5ENC.Text = ds.Tables[0].Rows[0]["Q5ENC"].ToString();
                switch (Q5E)
                {
                    case 0:
                        Q5_OK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q5_OKOPT.Checked = false;
                        Q5_NOK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q5_NOKOPT.Checked = false;
                        Q5_NA.Attributes.Add("Class", "btn btn-lg btn-primary active");
                        Q5_NAOPT.Checked = true;
                        break;
                    case 1:
                        Q5_OK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q5_OKOPT.Checked = false;
                        Q5_NOK.Attributes.Add("Class", "btn btn-lg btn-primary active");
                            Q5_NOK.Attributes.Add("style", "background-color:red");
                        Q5_NOKOPT.Checked = true;
                        Q5_NA.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q5_NAOPT.Checked = false;
                        break;
                    case 2:
                        Q5_OK.Attributes.Add("Class", "btn btn-lg btn-primary active");
                            Q5_OK.Attributes.Add("style", "background-color:forestgreen");
                        Q5_OKOPT.Checked = true;
                        Q5_NOK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q5_NOKOPT.Checked = false;
                        Q5_NA.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q5_NAOPT.Checked = false;
                        break;
                }

                int Q6E = Convert.ToInt32(ds.Tables[0].Rows[0]["Q6E"]);
                TbQ6ENC.Text = ds.Tables[0].Rows[0]["Q6ENC"].ToString();
                switch (Q6E)
                {
                    case 0:
                        Q6_OK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q6_OKOPT.Checked = false;
                        Q6_NOK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q6_NOKOPT.Checked = false;
                        Q6_NA.Attributes.Add("Class", "btn btn-lg btn-primary active");
                        Q6_NAOPT.Checked = true;
                        break;
                    case 1:
                        Q6_OK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q6_OKOPT.Checked = false;
                        Q6_NOK.Attributes.Add("Class", "btn btn-lg btn-primary active");
                            Q6_NOK.Attributes.Add("style", "background-color:red");
                        Q6_NOKOPT.Checked = true;
                        Q6_NA.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q6_NAOPT.Checked = false;
                        break;
                    case 2:
                        Q6_OK.Attributes.Add("Class", "btn btn-lg btn-primary active");
                            Q6_OK.Attributes.Add("style", "background-color:forestgreen");
                        Q6_OKOPT.Checked = true;
                        Q6_NOK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q6_NOKOPT.Checked = false;
                        Q6_NA.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q6_NAOPT.Checked = false;
                        break;
                }

                int Q6C = Convert.ToInt32(ds.Tables[0].Rows[0]["Q6C"]);
                TbQ6CAL.Text = ds.Tables[0].Rows[0]["Q6CAL"].ToString();
                switch (Q6C)
                {
                    case 0:
                        Q6C_OK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q6C_OKOPT.Checked = false;
                        Q6C_NOK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q6C_NOKOPT.Checked = false;
                        Q6C_NA.Attributes.Add("Class", "btn btn-lg btn-primary active");
                        Q6C_NAOPT.Checked = true;
                        break;
                    case 1:
                        Q6C_OK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q6C_OKOPT.Checked = false;
                        Q6C_NOK.Attributes.Add("Class", "btn btn-lg btn-primary active");
                            Q6C_NOK.Attributes.Add("style", "background-color:red");
                        Q6C_NOKOPT.Checked = true;
                        Q6C_NA.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q6C_NAOPT.Checked = false;
                        break;
                    case 2:
                        Q6C_OK.Attributes.Add("Class", "btn btn-lg btn-primary active");
                            Q6C_OK.Attributes.Add("style", "background-color:forestgreen");
                        Q6C_OKOPT.Checked = true;
                        Q6C_NOK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q6C_NOKOPT.Checked = false;
                        Q6C_NA.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q6C_NAOPT.Checked = false;
                        break;
                }


                int Q7E = Convert.ToInt32(ds.Tables[0].Rows[0]["Q7E"]);
                TbQ7ENC.Text = ds.Tables[0].Rows[0]["Q7ENC"].ToString();
                switch (Q7E)
                {
                    case 0:
                        Q7_OK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q7_OKOPT.Checked = false;
                        Q7_NOK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q7_NOKOPT.Checked = false;
                        Q7_NA.Attributes.Add("Class", "btn btn-lg btn-primary active");
                        Q7_NAOPT.Checked = true;
                        break;
                    case 1:
                        Q7_OK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q7_OKOPT.Checked = false;
                        Q7_NOK.Attributes.Add("Class", "btn btn-lg btn-primary active");
                            Q7_NOK.Attributes.Add("style", "background-color:red");
                        Q7_NOKOPT.Checked = true;
                        Q7_NA.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q7_NAOPT.Checked = false;
                        break;
                    case 2:
                        Q7_OK.Attributes.Add("Class", "btn btn-lg btn-primary active");
                            Q7_OK.Attributes.Add("style", "background-color:forestgreen");
                        Q7_OKOPT.Checked = true;
                        Q7_NOK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q7_NOKOPT.Checked = false;
                        Q7_NA.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q7_NAOPT.Checked = false;
                        break;
                }

                int Q7C = Convert.ToInt32(ds.Tables[0].Rows[0]["Q7C"]);
                TbQ7CAL.Text = ds.Tables[0].Rows[0]["Q7CAL"].ToString();
                switch (Q7C)
                {
                    case 0:
                        Q7C_OK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q7C_OKOPT.Checked = false;
                        Q7C_NOK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q7C_NOKOPT.Checked = false;
                        Q7C_NA.Attributes.Add("Class", "btn btn-lg btn-primary active");
                        Q7C_NAOPT.Checked = true;
                        break;
                    case 1:
                        Q7C_OK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q7C_OKOPT.Checked = false;
                        Q7C_NOK.Attributes.Add("Class", "btn btn-lg btn-primary active");
                            Q7C_NOK.Attributes.Add("style", "background-color:red");
                        Q7C_NOKOPT.Checked = true;
                        Q7C_NA.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q7C_NAOPT.Checked = false;
                        break;
                    case 2:
                        Q7C_OK.Attributes.Add("Class", "btn btn-lg btn-primary active");
                            Q7C_OK.Attributes.Add("style", "background-color:forestgreen");
                        Q7C_OKOPT.Checked = true;
                        Q7C_NOK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q7C_NOKOPT.Checked = false;
                        Q7C_NA.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q7C_NAOPT.Checked = false;
                        break;
                }

                int Q8E = Convert.ToInt32(ds.Tables[0].Rows[0]["Q8E"]);
                TbQ8ENC.Text = ds.Tables[0].Rows[0]["Q8ENC"].ToString();
                switch (Q8E)
                {
                    case 0:
                        Q8_OK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q8_OKOPT.Checked = false;
                        Q8_NOK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q8_NOKOPT.Checked = false;
                        Q8_NA.Attributes.Add("Class", "btn btn-lg btn-primary active");
                        Q8_NAOPT.Checked = true;
                        break;
                    case 1:
                        Q8_OK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q8_OKOPT.Checked = false;
                        Q8_NOK.Attributes.Add("Class", "btn btn-lg btn-primary active");
                            Q8_NOK.Attributes.Add("style", "background-color:red");
                        Q8_NOKOPT.Checked = true;
                        Q8_NA.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q8_NAOPT.Checked = false;
                        break;
                    case 2:
                        Q8_OK.Attributes.Add("Class", "btn btn-lg btn-primary active");
                            Q8_OK.Attributes.Add("style", "background-color:forestgreen");
                        Q8_OKOPT.Checked = true;
                        Q8_NOK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q8_NOKOPT.Checked = false;
                        Q8_NA.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q8_NAOPT.Checked = false;
                        break;
                }

                int Q8C = Convert.ToInt32(ds.Tables[0].Rows[0]["Q8C"]);
                TbQ8CAL.Text = ds.Tables[0].Rows[0]["Q8CAL"].ToString();
                switch (Q8C)
                {
                    case 0:
                        Q8C_OK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q8C_OKOPT.Checked = false;
                        Q8C_NOK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q8C_NOKOPT.Checked = false;
                        Q8C_NA.Attributes.Add("Class", "btn btn-lg btn-primary active");
                        Q8C_NAOPT.Checked = true;
                        break;
                    case 1:
                        Q8C_OK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q8C_OKOPT.Checked = false;
                        Q8C_NOK.Attributes.Add("Class", "btn btn-lg btn-primary active");
                            Q8C_NOK.Attributes.Add("style", "background-color:red");
                        Q8C_NOKOPT.Checked = true;
                        Q8C_NA.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q8C_NAOPT.Checked = false;
                        break;
                    case 2:
                        Q8C_OK.Attributes.Add("Class", "btn btn-lg btn-primary active");
                            Q8C_OK.Attributes.Add("style", "background-color:forestgreen");
                        Q8C_OKOPT.Checked = true;
                        Q8C_NOK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q8C_NOKOPT.Checked = false;
                        Q8C_NA.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q8C_NAOPT.Checked = false;
                        break;
                }

                int Q9E = Convert.ToInt32(ds.Tables[0].Rows[0]["Q9E"]);
                TbQ9ENC.Text = ds.Tables[0].Rows[0]["Q9ENC"].ToString();
                switch (Q9E)
                {
                    case 0:
                        Q9_OK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q9_OKOPT.Checked = false;
                        Q9_NOK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q9_NOKOPT.Checked = false;
                        Q9_NA.Attributes.Add("Class", "btn btn-lg btn-primary active");
                        Q9_NAOPT.Checked = true;
                        break;
                    case 1:
                        Q9_OK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q9_OKOPT.Checked = false;
                        Q9_NOK.Attributes.Add("Class", "btn btn-lg btn-primary active");
                            Q9_NOK.Attributes.Add("style", "background-color:red");
                        Q9_NOKOPT.Checked = true;
                        Q9_NA.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q9_NAOPT.Checked = false;
                        break;
                    case 2:
                        Q9_OK.Attributes.Add("Class", "btn btn-lg btn-primary active");
                            Q9_OK.Attributes.Add("style", "background-color:forestgreen");
                        Q9_OKOPT.Checked = true;
                        Q9_NOK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q9_NOKOPT.Checked = false;
                        Q9_NA.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q9_NAOPT.Checked = false;
                        break;
                }

                int Q9C = Convert.ToInt32(ds.Tables[0].Rows[0]["Q9C"]);
                TbQ9CAL.Text = ds.Tables[0].Rows[0]["Q9CAL"].ToString();
                switch (Q9C)
                {
                    case 0:
                        Q9C_OK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q9C_OKOPT.Checked = false;
                        Q9C_NOK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q9C_NOKOPT.Checked = false;
                        Q9C_NA.Attributes.Add("Class", "btn btn-lg btn-primary active");
                        Q9C_NAOPT.Checked = true;
                        break;
                    case 1:
                        Q9C_OK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q9C_OKOPT.Checked = false;
                        Q9C_NOK.Attributes.Add("Class", "btn btn-lg btn-primary active");
                            Q9C_NOK.Attributes.Add("style", "background-color:red");
                        Q9C_NOKOPT.Checked = true;
                        Q9C_NA.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q9C_NAOPT.Checked = false;
                        break;
                    case 2:
                        Q9C_OK.Attributes.Add("Class", "btn btn-lg btn-primary active");
                            Q9C_OK.Attributes.Add("style", "background-color:forestgreen");
                        Q9C_OKOPT.Checked = true;
                        Q9C_NOK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q9C_NOKOPT.Checked = false;
                        Q9C_NA.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q9C_NAOPT.Checked = false;
                        break;
                }

                int Q10C = Convert.ToInt32(ds.Tables[0].Rows[0]["Q10C"]);
                TbQ10CAL.Text = ds.Tables[0].Rows[0]["Q10CAL"].ToString();
                switch (Q10C)
                {
                    case 0:
                        Q10C_OK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q10C_OKOPT.Checked = false;
                        Q10C_NOK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q10C_NOKOPT.Checked = false;
                        Q10C_NA.Attributes.Add("Class", "btn btn-lg btn-primary active");
                        Q10C_NAOPT.Checked = true;
                        break;
                    case 1:
                        Q10C_OK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q10C_OKOPT.Checked = false;
                        Q10C_NOK.Attributes.Add("Class", "btn btn-lg btn-primary active");
                            Q10C_NOK.Attributes.Add("style", "background-color:red");
                        Q10C_NOKOPT.Checked = true;
                        Q10C_NA.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q10C_NAOPT.Checked = false;
                        break;
                    case 2:
                        Q10C_OK.Attributes.Add("Class", "btn btn-lg btn-primary active");
                            Q10C_OK.Attributes.Add("style", "background-color:forestgreen");
                        Q10C_OKOPT.Checked = true;
                        Q10C_NOK.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q10C_NOKOPT.Checked = false;
                        Q10C_NA.Attributes.Add("Class", "btn btn-lg btn-primary");
                        Q10C_NAOPT.Checked = false;
                        break;
                }

            }
            catch (Exception) {}
        }
        public void CargarMateriasPrimasLiberacion()
        {
            try
            {
                Conexion_LIBERACIONES conexion = new Conexion_LIBERACIONES();
                DataSet materiales = conexion.LeerEstructuraLIBERACION(Convert.ToInt32(tbOrden.Text));
                string MAT1REFERENCIA = materiales.Tables[0].Rows[0]["MAT1REF"].ToString();
                if (MAT1REFERENCIA != "")
                {
                    //MAT1STOCK.Visible = true;
                    MATSAVE1.Visible = true;
                    MAT1LOT.Visible = true;
                    MAT1LOT2.Visible = true;
                    MAT1NOM.Visible = true;
                    MAT1REF.Visible = true;
                    MAT1REMARK2.Visible = true;
                    //MAT1TEMP.Visible = true;
                    MAT1TEMPREAL.Visible = true;
                    //MAT1TIEMP.Visible = true;
                    MAT1TIEMPREAL.Visible = true;
                    MAT1REF.Text = materiales.Tables[0].Rows[0]["MAT1REF"].ToString();
                    MAT1NOM.Text = materiales.Tables[0].Rows[0]["MAT1NOM"].ToString();
                    MAT1LOT.Text = materiales.Tables[0].Rows[0]["MAT1LOT"].ToString().Split('|')[0];
                    try
                    {
                        MAT1LOT2.Text = materiales.Tables[0].Rows[0]["MAT1LOT"].ToString().Split('|')[1];
                    }
                    catch (Exception) { }

                    MAT1TIEMP.Text = materiales.Tables[0].Rows[0]["MAT1TIEMP"].ToString();
                    MAT1TIEMPREAL.Text = materiales.Tables[0].Rows[0]["MAT1TIEMPREAL"].ToString();
                    MAT1TEMP.Text = materiales.Tables[0].Rows[0]["MAT1TEMP"].ToString();
                    MAT1TEMPREAL.Text = materiales.Tables[0].Rows[0]["MAT1TEMPREAL"].ToString();
                    MAT1REMARK2.Text = conexion.devuelve_remark_materiasprimasBMS(MAT1REF.Text);
                }
                string MAT2REFERENCIA = materiales.Tables[0].Rows[0]["MAT2REF"].ToString();
                if (MAT2REFERENCIA != "")
                {
                    //MAT2STOCK.Visible = true;
                    MATSAVE2.Visible = true;
                    MAT2LOT.Visible = true;
                    MAT2LOT2.Visible = true;
                    MAT2NOM.Visible = true;
                    MAT2REF.Visible = true;
                    MAT2REMARK2.Visible = true;
                    //MAT2TEMP.Visible = true;
                    MAT2TEMPREAL.Visible = true;
                    //MAT2TIEMP.Visible = true;
                    MAT2TIEMPREAL.Visible = true;
                    MAT2REF.Text = materiales.Tables[0].Rows[0]["MAT2REF"].ToString();
                    MAT2NOM.Text = materiales.Tables[0].Rows[0]["MAT2NOM"].ToString();
                    MAT2LOT.Text = materiales.Tables[0].Rows[0]["MAT2LOT"].ToString().Split('|')[0];
                    try
                    {
                        MAT2LOT2.Text = materiales.Tables[0].Rows[0]["MAT2LOT"].ToString().Split('|')[1];
                    }
                    catch (Exception) { }
                    
                    MAT2TIEMP.Text = materiales.Tables[0].Rows[0]["MAT2TIEMP"].ToString();
                    MAT2TIEMPREAL.Text = materiales.Tables[0].Rows[0]["MAT2TIEMPREAL"].ToString();
                    MAT2TEMP.Text = materiales.Tables[0].Rows[0]["MAT2TEMP"].ToString();
                    MAT2TEMPREAL.Text = materiales.Tables[0].Rows[0]["MAT2TEMPREAL"].ToString();
                    MAT2REMARK2.Text = conexion.devuelve_remark_materiasprimasBMS(MAT2REF.Text);
                }
                string MAT3REFERENCIA = materiales.Tables[0].Rows[0]["MAT3REF"].ToString();
                if (MAT3REFERENCIA != "")
                {
                    MATSAVE3.Visible = true;
                    //MAT3STOCK.Visible = true;
                    MAT3LOT.Visible = true;
                    MAT3LOT2.Visible = true;
                    MAT3NOM.Visible = true;
                    MAT3REF.Visible = true;
                    MAT3REMARK2.Visible = true;
                    //MAT3TEMP.Visible = true;
                    MAT3TEMPREAL.Visible = true;
                    //MAT3TIEMP.Visible = true;
                    MAT3TIEMPREAL.Visible = true;
                    MAT3REF.Text = materiales.Tables[0].Rows[0]["MAT3REF"].ToString();
                    MAT3NOM.Text = materiales.Tables[0].Rows[0]["MAT3NOM"].ToString();
                    MAT3LOT.Text = materiales.Tables[0].Rows[0]["MAT3LOT"].ToString().Split('|')[0];
                    try
                    {
                        MAT3LOT2.Text = materiales.Tables[0].Rows[0]["MAT3LOT"].ToString().Split('|')[1];
                    }
                    catch (Exception) { }
                    MAT3TIEMP.Text = materiales.Tables[0].Rows[0]["MAT3TIEMP"].ToString();
                    MAT3TIEMPREAL.Text = materiales.Tables[0].Rows[0]["MAT3TIEMPREAL"].ToString();
                    MAT3TEMP.Text = materiales.Tables[0].Rows[0]["MAT3TEMP"].ToString();
                    MAT3TEMPREAL.Text = materiales.Tables[0].Rows[0]["MAT3TEMPREAL"].ToString();
                    MAT3REMARK2.Text = conexion.devuelve_remark_materiasprimasBMS(MAT3REF.Text);
                }

                string COMP1REFERENCIA = materiales.Tables[0].Rows[0]["COMP1REF"].ToString();
                if (COMP1REFERENCIA != "")
                {
                    COMPSAVE1.Visible = true;
                    //COMP1STOCK.Visible = true;
                    COMP1REF.Visible = true;
                    COMP1NOM.Visible = true;
                    COMP1LOT.Visible = true;
                    COMP1LOT2.Visible = true;
                    COMP1REF.Text = materiales.Tables[0].Rows[0]["COMP1REF"].ToString();
                    COMP1NOM.Text = materiales.Tables[0].Rows[0]["COMP1NOM"].ToString();
                    //COMP1LOT.Text = materiales.Tables[0].Rows[0]["COMP1LOT"].ToString();
                    COMP1LOT.Text = materiales.Tables[0].Rows[0]["COMP1LOT"].ToString().Split('|')[0];
                    try
                    {
                        COMP1LOT2.Text = materiales.Tables[0].Rows[0]["COMP1LOT"].ToString().Split('|')[1];
                    }
                    catch (Exception) { }

                }

                string COMP2REFERENCIA = materiales.Tables[0].Rows[0]["COMP2REF"].ToString();
                if (COMP2REFERENCIA != "")
                {
                    COMPSAVE2.Visible = true;
                    //COMP2STOCK.Visible = true;
                    COMP2REF.Visible = true;
                    COMP2NOM.Visible = true;
                    COMP2LOT.Visible = true;
                    COMP2LOT2.Visible = true;
                    COMP2REF.Text = materiales.Tables[0].Rows[0]["COMP2REF"].ToString();
                    COMP2NOM.Text = materiales.Tables[0].Rows[0]["COMP2NOM"].ToString();
                    COMP2LOT.Text = materiales.Tables[0].Rows[0]["COMP2LOT"].ToString().Split('|')[0];
                    try
                    {
                        COMP2LOT2.Text = materiales.Tables[0].Rows[0]["COMP2LOT"].ToString().Split('|')[1];
                    }
                    catch (Exception) { }
                }

                string COMP3REFERENCIA = materiales.Tables[0].Rows[0]["COMP3REF"].ToString();
                if (COMP3REFERENCIA != "")
                {
                    COMPSAVE3.Visible = true;
                    //COMP3STOCK.Visible = true;
                    COMP3REF.Visible = true;
                    COMP3NOM.Visible = true;
                    COMP3LOT.Visible = true;
                    COMP3LOT2.Visible = true;
                    COMP3REF.Text = materiales.Tables[0].Rows[0]["COMP3REF"].ToString();
                    COMP3NOM.Text = materiales.Tables[0].Rows[0]["COMP3NOM"].ToString();
                    COMP3LOT.Text = materiales.Tables[0].Rows[0]["COMP3LOT"].ToString().Split('|')[0];
                    try
                    {
                        COMP3LOT2.Text = materiales.Tables[0].Rows[0]["COMP3LOT"].ToString().Split('|')[1];
                    }
                    catch (Exception) { }
                }

                string COMP4REFERENCIA = materiales.Tables[0].Rows[0]["COMP4REF"].ToString();
                if (COMP4REFERENCIA != "")
                {
                    COMPSAVE4.Visible = true;
                    //COMP4STOCK.Visible = true;
                    COMP4REF.Visible = true;
                    COMP4NOM.Visible = true;
                    COMP4LOT.Visible = true;
                    COMP4LOT2.Visible = true;
                    COMP4REF.Text = materiales.Tables[0].Rows[0]["COMP4REF"].ToString();
                    COMP4NOM.Text = materiales.Tables[0].Rows[0]["COMP4NOM"].ToString();
                    COMP4LOT.Text = materiales.Tables[0].Rows[0]["COMP4LOT"].ToString().Split('|')[0];
                    try
                    {
                        COMP4LOT2.Text = materiales.Tables[0].Rows[0]["COMP4LOT"].ToString().Split('|')[1];
                    }
                    catch (Exception) { }
                }

                string COMP5REFERENCIA = materiales.Tables[0].Rows[0]["COMP5REF"].ToString();
                if (COMP5REFERENCIA != "")
                {
                    COMPSAVE5.Visible = true;
                    //COMP5STOCK.Visible = true;
                    COMP5REF.Visible = true;
                    COMP5NOM.Visible = true;
                    COMP5LOT.Visible = true;
                    COMP5LOT2.Visible = true;
                    COMP5REF.Text = materiales.Tables[0].Rows[0]["COMP5REF"].ToString();
                    COMP5NOM.Text = materiales.Tables[0].Rows[0]["COMP5NOM"].ToString();
                    COMP5LOT.Text = materiales.Tables[0].Rows[0]["COMP5LOT"].ToString().Split('|')[0];
                    try
                    {
                        COMP5LOT2.Text = materiales.Tables[0].Rows[0]["COMP5LOT"].ToString().Split('|')[1];
                    }
                    catch (Exception) { }
                }

                string COMP6REFERENCIA = materiales.Tables[0].Rows[0]["COMP6REF"].ToString();
                if (COMP6REFERENCIA != "")
                {
                    COMPSAVE6.Visible = true;
                    //COMP6STOCK.Visible = true;
                    COMP6REF.Visible = true;
                    COMP6NOM.Visible = true;
                    COMP6LOT.Visible = true;
                    COMP6LOT2.Visible = true;
                    COMP6REF.Text = materiales.Tables[0].Rows[0]["COMP6REF"].ToString();
                    COMP6NOM.Text = materiales.Tables[0].Rows[0]["COMP6NOM"].ToString();
                    COMP6LOT.Text = materiales.Tables[0].Rows[0]["COMP6LOT"].ToString().Split('|')[0];
                    try
                    {
                        COMP6LOT2.Text = materiales.Tables[0].Rows[0]["COMP6LOT"].ToString().Split('|')[1];
                    }
                    catch (Exception) { }

                }

                string COMP7REFERENCIA = materiales.Tables[0].Rows[0]["COMP7REF"].ToString();
                if (COMP7REFERENCIA != "")
                {
                    COMPSAVE7.Visible = true;
                    //COMP7STOCK.Visible = true;
                    COMP7REF.Visible = true;
                    COMP7NOM.Visible = true;
                    COMP7LOT.Visible = true;
                    COMP7LOT2.Visible = true;
                    COMP7REF.Text = materiales.Tables[0].Rows[0]["COMP7REF"].ToString();
                    COMP7NOM.Text = materiales.Tables[0].Rows[0]["COMP7NOM"].ToString();
                    COMP7LOT.Text = materiales.Tables[0].Rows[0]["COMP7LOT"].ToString().Split('|')[0];
                    try
                    {
                        COMP7LOT2.Text = materiales.Tables[0].Rows[0]["COMP1LOT"].ToString().Split('|')[1];
                    }
                    catch (Exception) { }
                }
            }
            catch (Exception)
            { }

        }
        public void PintarCeldasDevacion()
        {
            //COMPARA Y PINTA
            try
            {
                ResumenPROD1.Visible = false;
                ResumenPROD2.Visible = false;
                ResumenPROD3.Visible = false;
                matparamNC.Visible = false;
                //CILINDRO
                double thBoq_double = Convert.ToDouble(thBoq.Text.Replace('.', ','));
                double thBoq_REAL_double = Convert.ToDouble(thBoqREAL.Text.Replace('.', ','));
                if (thBoq_REAL_double > thBoq_double * 1.1 || thBoq_REAL_double < thBoq_double * 0.9)
                {
                    thBoqREAL.BackColor = System.Drawing.Color.Red;
                    thBoqREAL.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;

                }
                else
                {
                    thBoqREAL.BackColor = System.Drawing.Color.Empty;
                    thBoqREAL.ForeColor = System.Drawing.Color.Black;

                }

                double thT1_double = Convert.ToDouble(thT1.Text.Replace('.', ','));
                double thT1_REAL_double = Convert.ToDouble(thT1REAL.Text.Replace('.', ','));
                if (thT1_REAL_double > thT1_double * 1.1 || thT1_REAL_double < thT1_double * 0.9)
                {
                    thT1REAL.BackColor = System.Drawing.Color.Red;
                    thT1REAL.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;

                }
                else
                {
                    thT1REAL.BackColor = System.Drawing.Color.Empty;
                    thT1REAL.ForeColor = System.Drawing.Color.Black;

                }

                double thT2_double = Convert.ToDouble(thT2.Text.Replace('.', ','));
                double thT2_REAL_double = Convert.ToDouble(thT2REAL.Text.Replace('.', ','));
                if (thT2_REAL_double > thT2_double * 1.1 || thT2_REAL_double < thT2_double * 0.9)
                {
                    thT2REAL.BackColor = System.Drawing.Color.Red;
                    thT2REAL.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thT2REAL.BackColor = System.Drawing.Color.Empty;
                    thT2REAL.ForeColor = System.Drawing.Color.Black;
                }

                double thT3_double = Convert.ToDouble(thT3.Text.Replace('.', ','));
                double thT3_REAL_double = Convert.ToDouble(thT3REAL.Text.Replace('.', ','));
                if (thT3_REAL_double > thT3_double * 1.1 || thT3_REAL_double < thT3_double * 0.9)
                {
                    thT3REAL.BackColor = System.Drawing.Color.Red;
                    thT3REAL.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thT3REAL.BackColor = System.Drawing.Color.Empty;
                    thT3REAL.ForeColor = System.Drawing.Color.Black;
                }

                double thT4_double = Convert.ToDouble(thT4.Text.Replace('.', ','));
                double thT4_REAL_double = Convert.ToDouble(thT4REAL.Text.Replace('.', ','));
                if (thT4_REAL_double > thT4_double * 1.1 || thT4_REAL_double < thT4_double * 0.9)
                {
                    thT4REAL.BackColor = System.Drawing.Color.Red;
                    thT4REAL.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thT4REAL.BackColor = System.Drawing.Color.Empty;
                    thT4REAL.ForeColor = System.Drawing.Color.Black;
                }

                double thT5_double = Convert.ToDouble(thT5.Text.Replace('.', ','));
                double thT5_REAL_double = Convert.ToDouble(thT5REAL.Text.Replace('.', ','));
                if (thT5_REAL_double > thT5_double * 1.1 || thT5_REAL_double < thT5_double * 0.9)
                {
                    thT5REAL.BackColor = System.Drawing.Color.Red;
                    thT5REAL.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thT5REAL.BackColor = System.Drawing.Color.Empty;
                    thT5REAL.ForeColor = System.Drawing.Color.Black;
                }

                double thT6_double = Convert.ToDouble(thT6.Text.Replace('.', ','));
                double thT6_REAL_double = Convert.ToDouble(thT6REAL.Text.Replace('.', ','));
                if (thT6_REAL_double > thT6_double * 1.1 || thT6_REAL_double < thT6_double * 0.9)
                {
                    thT6REAL.BackColor = System.Drawing.Color.Red;
                    thT6REAL.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thT6REAL.BackColor = System.Drawing.Color.Empty;
                    thT6REAL.ForeColor = System.Drawing.Color.Black;
                }

                double thT7_double = Convert.ToDouble(thT7.Text.Replace('.', ','));
                double thT7_REAL_double = Convert.ToDouble(thT7REAL.Text.Replace('.', ','));
                if (thT7_REAL_double > thT7_double * 1.1 || thT7_REAL_double < thT7_double * 0.9)
                {
                    thT7REAL.BackColor = System.Drawing.Color.Red;
                    thT7REAL.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thT7REAL.BackColor = System.Drawing.Color.Empty;
                    thT7REAL.ForeColor = System.Drawing.Color.Black;
                }

                double thT8_double = Convert.ToDouble(thT8.Text.Replace('.', ','));
                double thT8_REAL_double = Convert.ToDouble(thT8REAL.Text.Replace('.', ','));
                if (thT8_REAL_double > thT8_double * 1.1 || thT8_REAL_double < thT8_double * 0.9)
                {
                    thT8REAL.BackColor = System.Drawing.Color.Red;
                    thT8REAL.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thT8REAL.BackColor = System.Drawing.Color.Empty;
                    thT8REAL.ForeColor = System.Drawing.Color.Black;
                }

                double thT9_double = Convert.ToDouble(thT9.Text.Replace('.', ','));
                double thT9_REAL_double = Convert.ToDouble(thT9REAL.Text.Replace('.', ','));
                if (thT9_REAL_double > thT9_double * 1.1 || thT9_REAL_double < thT9_double * 0.9)
                {
                    thT9REAL.BackColor = System.Drawing.Color.Red;
                    thT9REAL.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thT9REAL.BackColor = System.Drawing.Color.Empty;
                    thT9REAL.ForeColor = System.Drawing.Color.Black;
                }
                double thT10_double = Convert.ToDouble(thT10.Text.Replace('.', ','));
                double thT10_REAL_double = Convert.ToDouble(thT10REAL.Text.Replace('.', ','));
                if (thT10_REAL_double > thT10_double * 1.1 || thT10_REAL_double < thT10_double * 0.9)
                {
                    thT10REAL.BackColor = System.Drawing.Color.Red;
                    thT10REAL.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thT10REAL.BackColor = System.Drawing.Color.Empty;
                    thT10REAL.ForeColor = System.Drawing.Color.Black;
                }

                //CÁMARA CALIENTE

                double thZ1_double = Convert.ToDouble(thZ1.Text.Replace('.', ','));
                double thZ1_REAL_double = Convert.ToDouble(thZ1REAL.Text.Replace('.', ','));
                if (thZ1_REAL_double > thZ1_double * 1.1 || thZ1_REAL_double < thZ1_double * 0.9)
                {
                    thZ1REAL.BackColor = System.Drawing.Color.Red;
                    thZ1REAL.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thZ1REAL.BackColor = System.Drawing.Color.Empty;
                    thZ1REAL.ForeColor = System.Drawing.Color.Black;
                }

                double thZ2_double = Convert.ToDouble(thZ2.Text.Replace('.', ','));
                double thZ2_REAL_double = Convert.ToDouble(thZ2REAL.Text.Replace('.', ','));
                if (thZ2_REAL_double > thZ2_double * 1.1 || thZ2_REAL_double < thZ2_double * 0.9)
                {
                    thZ2REAL.BackColor = System.Drawing.Color.Red;
                    thZ2REAL.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thZ2REAL.BackColor = System.Drawing.Color.Empty;
                    thZ2REAL.ForeColor = System.Drawing.Color.Black;
                }

                double thZ3_double = Convert.ToDouble(thZ3.Text.Replace('.', ','));
                double thZ3_REAL_double = Convert.ToDouble(thZ3REAL.Text.Replace('.', ','));
                if (thZ3_REAL_double > thZ3_double * 1.1 || thZ3_REAL_double < thZ3_double * 0.9)
                {
                    thZ3REAL.BackColor = System.Drawing.Color.Red;
                    thZ3REAL.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thZ3REAL.BackColor = System.Drawing.Color.Empty;
                    thZ3REAL.ForeColor = System.Drawing.Color.Black;
                }

                double thZ4_double = Convert.ToDouble(thZ4.Text.Replace('.', ','));
                double thZ4_REAL_double = Convert.ToDouble(thZ4REAL.Text.Replace('.', ','));
                if (thZ4_REAL_double > thZ4_double * 1.1 || thZ4_REAL_double < thZ4_double * 0.9)
                {
                    thZ4REAL.BackColor = System.Drawing.Color.Red;
                    thZ4REAL.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thZ4REAL.BackColor = System.Drawing.Color.Empty;
                    thZ4REAL.ForeColor = System.Drawing.Color.Black;
                }

                double thZ5_double = Convert.ToDouble(thZ5.Text.Replace('.', ','));
                double thZ5_REAL_double = Convert.ToDouble(thZ5REAL.Text.Replace('.', ','));
                if (thZ5_REAL_double > thZ5_double * 1.1 || thZ5_REAL_double < thZ5_double * 0.9)
                {
                    thZ5REAL.BackColor = System.Drawing.Color.Red;
                    thZ5REAL.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thZ5REAL.BackColor = System.Drawing.Color.Empty;
                    thZ5REAL.ForeColor = System.Drawing.Color.Black;
                }

                double thZ6_double = Convert.ToDouble(thZ6.Text.Replace('.', ','));
                double thZ6_REAL_double = Convert.ToDouble(thZ6REAL.Text.Replace('.', ','));
                if (thZ6_REAL_double > thZ6_double * 1.1 || thZ6_REAL_double < thZ6_double * 0.9)
                {
                    thZ6REAL.BackColor = System.Drawing.Color.Red;
                    thZ6REAL.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thZ6REAL.BackColor = System.Drawing.Color.Empty;
                    thZ6REAL.ForeColor = System.Drawing.Color.Black;
                }

                double thZ7_double = Convert.ToDouble(thZ7.Text.Replace('.', ','));
                double thZ7_REAL_double = Convert.ToDouble(thZ7REAL.Text.Replace('.', ','));
                if (thZ7_REAL_double > thZ7_double * 1.1 || thZ7_REAL_double < thZ7_double * 0.9)
                {
                    thZ7REAL.BackColor = System.Drawing.Color.Red;
                    thZ7REAL.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thZ7REAL.BackColor = System.Drawing.Color.Empty;
                    thZ7REAL.ForeColor = System.Drawing.Color.Black;
                }

                double thZ8_double = Convert.ToDouble(thZ8.Text.Replace('.', ','));
                double thZ8_REAL_double = Convert.ToDouble(thZ8REAL.Text.Replace('.', ','));
                if (thZ8_REAL_double > thZ8_double * 1.1 || thZ8_REAL_double < thZ8_double * 0.9)
                {
                    thZ8REAL.BackColor = System.Drawing.Color.Red;
                    thZ8REAL.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thZ8REAL.BackColor = System.Drawing.Color.Empty;
                    thZ8REAL.ForeColor = System.Drawing.Color.Black;
                }

                double thZ9_double = Convert.ToDouble(thZ9.Text.Replace('.', ','));
                double thZ9_REAL_double = Convert.ToDouble(thZ9REAL.Text.Replace('.', ','));
                if (thZ9_REAL_double > thZ9_double * 1.1 || thZ9_REAL_double < thZ9_double * 0.9)
                {
                    thZ9REAL.BackColor = System.Drawing.Color.Red;
                    thZ9REAL.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thZ9REAL.BackColor = System.Drawing.Color.Empty;
                    thZ9REAL.ForeColor = System.Drawing.Color.Black;
                }

                double thZ10_double = Convert.ToDouble(thZ10.Text.Replace('.', ','));
                double thZ10_REAL_double = Convert.ToDouble(thZ10REAL.Text.Replace('.', ','));
                if (thZ10_REAL_double > thZ10_double * 1.1 || thZ10_REAL_double < thZ10_double * 0.9)
                {
                    thZ10REAL.BackColor = System.Drawing.Color.Red;
                    thZ10REAL.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thZ10REAL.BackColor = System.Drawing.Color.Empty;
                    thZ10REAL.ForeColor = System.Drawing.Color.Black;
                }

                double thZ11_double = Convert.ToDouble(thZ11.Text.Replace('.', ','));
                double thZ11_REAL_double = Convert.ToDouble(thZ11REAL.Text.Replace('.', ','));
                if (thZ11_REAL_double > thZ11_double * 1.1 || thZ11_REAL_double < thZ11_double * 0.9)
                {
                    thZ11REAL.BackColor = System.Drawing.Color.Red;
                    thZ11REAL.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thZ11REAL.BackColor = System.Drawing.Color.Empty;
                    thZ11REAL.ForeColor = System.Drawing.Color.Black;
                }

                double thZ12_double = Convert.ToDouble(thZ12.Text.Replace('.', ','));
                double thZ12_REAL_double = Convert.ToDouble(thZ12REAL.Text.Replace('.', ','));
                if (thZ12_REAL_double > thZ12_double * 1.1 || thZ12_REAL_double < thZ12_double * 0.9)
                {
                    thZ12REAL.BackColor = System.Drawing.Color.Red;
                    thZ12REAL.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thZ12REAL.BackColor = System.Drawing.Color.Empty;
                    thZ12REAL.ForeColor = System.Drawing.Color.Black;
                }

                double thZ13_double = Convert.ToDouble(thZ13.Text.Replace('.', ','));
                double thZ13_REAL_double = Convert.ToDouble(thZ13REAL.Text.Replace('.', ','));
                if (thZ13_REAL_double > thZ13_double * 1.1 || thZ13_REAL_double < thZ13_double * 0.9)
                {
                    thZ13REAL.BackColor = System.Drawing.Color.Red;
                    thZ13REAL.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thZ13REAL.BackColor = System.Drawing.Color.Empty;
                    thZ13REAL.ForeColor = System.Drawing.Color.Black;
                }

                double thZ14_double = Convert.ToDouble(thZ14.Text.Replace('.', ','));
                double thZ14_REAL_double = Convert.ToDouble(thZ14REAL.Text.Replace('.', ','));
                if (thZ14_REAL_double > thZ14_double * 1.1 || thZ14_REAL_double < thZ14_double * 0.9)
                {
                    thZ14REAL.BackColor = System.Drawing.Color.Red;
                    thZ14REAL.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thZ14REAL.BackColor = System.Drawing.Color.Empty;
                    thZ14REAL.ForeColor = System.Drawing.Color.Black;
                }

                double thZ15_double = Convert.ToDouble(thZ15.Text.Replace('.', ','));
                double thZ15_REAL_double = Convert.ToDouble(thZ15REAL.Text.Replace('.', ','));
                if (thZ15_REAL_double > thZ15_double * 1.1 || thZ15_REAL_double < thZ15_double * 0.9)
                {
                    thZ15REAL.BackColor = System.Drawing.Color.Red;
                    thZ15REAL.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thZ15REAL.BackColor = System.Drawing.Color.Empty;
                    thZ15REAL.ForeColor = System.Drawing.Color.Black;
                }

                double thZ16_double = Convert.ToDouble(thZ16.Text.Replace('.', ','));
                double thZ16_REAL_double = Convert.ToDouble(thZ16REAL.Text.Replace('.', ','));
                if (thZ16_REAL_double > thZ16_double * 1.1 || thZ16_REAL_double < thZ16_double * 0.9)
                {
                    thZ16REAL.BackColor = System.Drawing.Color.Red;
                    thZ16REAL.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thZ16REAL.BackColor = System.Drawing.Color.Empty;
                    thZ16REAL.ForeColor = System.Drawing.Color.Black;
                }

                double thZ17_double = Convert.ToDouble(thZ17.Text.Replace('.', ','));
                double thZ17_REAL_double = Convert.ToDouble(thZ17REAL.Text.Replace('.', ','));
                if (thZ17_REAL_double > thZ17_double * 1.1 || thZ17_REAL_double < thZ17_double * 0.9)
                {
                    thZ17REAL.BackColor = System.Drawing.Color.Red;
                    thZ17REAL.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thZ17REAL.BackColor = System.Drawing.Color.Empty;
                    thZ17REAL.ForeColor = System.Drawing.Color.Black;
                }

                double thZ18_double = Convert.ToDouble(thZ18.Text.Replace('.', ','));
                double thZ18_REAL_double = Convert.ToDouble(thZ18REAL.Text.Replace('.', ','));
                if (thZ18_REAL_double > thZ18_double * 1.1 || thZ18_REAL_double < thZ18_double * 0.9)
                {
                    thZ18REAL.BackColor = System.Drawing.Color.Red;
                    thZ18REAL.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thZ18REAL.BackColor = System.Drawing.Color.Empty;
                    thZ18REAL.ForeColor = System.Drawing.Color.Black;
                }

                double thZ19_double = Convert.ToDouble(thZ19.Text.Replace('.', ','));
                double thZ19_REAL_double = Convert.ToDouble(thZ19REAL.Text.Replace('.', ','));
                if (thZ19_REAL_double > thZ19_double * 1.1 || thZ19_REAL_double < thZ19_double * 0.9)
                {
                    thZ19REAL.BackColor = System.Drawing.Color.Red;
                    thZ19REAL.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thZ19REAL.BackColor = System.Drawing.Color.Empty;
                    thZ19REAL.ForeColor = System.Drawing.Color.Black;
                }

                double thZ20_double = Convert.ToDouble(thZ20.Text.Replace('.', ','));
                double thZ20_REAL_double = Convert.ToDouble(thZ20REAL.Text.Replace('.', ','));
                if (thZ20_REAL_double > thZ20_double * 1.1 || thZ20_REAL_double < thZ20_double * 0.9)
                {
                    thZ20REAL.BackColor = System.Drawing.Color.Red;
                    thZ20REAL.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thZ20REAL.BackColor = System.Drawing.Color.Empty;
                    thZ20REAL.ForeColor = System.Drawing.Color.Black;
                }

                //POSTPRESION
                double thP1_double = Convert.ToDouble(thP1.Text.Replace('.', ','));
                double thP1_REAL_double = Convert.ToDouble(thP1R.Text.Replace('.', ','));
                if (thP1_REAL_double > thP1_double * 1.1 || thP1_REAL_double < thP1_double * 0.9)
                {
                    thP1R.BackColor = System.Drawing.Color.Red;
                    thP1R.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thP1R.BackColor = System.Drawing.Color.Empty;
                    thP1R.ForeColor = System.Drawing.Color.Black;
                }

                double thP2_double = Convert.ToDouble(thP2.Text.Replace('.', ','));
                double thP2_REAL_double = Convert.ToDouble(thP2R.Text.Replace('.', ','));
                if (thP2_REAL_double > thP2_double * 1.1 || thP2_REAL_double < thP2_double * 0.9)
                {
                    thP2R.BackColor = System.Drawing.Color.Red;
                    thP2R.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thP2R.BackColor = System.Drawing.Color.Empty;
                    thP2R.ForeColor = System.Drawing.Color.Black;
                }

                double thP3_double = Convert.ToDouble(thP3.Text.Replace('.', ','));
                double thP3_REAL_double = Convert.ToDouble(thP3R.Text.Replace('.', ','));
                if (thP3_REAL_double > thP3_double * 1.1 || thP3_REAL_double < thP3_double * 0.9)
                {
                    thP3R.BackColor = System.Drawing.Color.Red;
                    thP3R.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thP3R.BackColor = System.Drawing.Color.Empty;
                    thP3R.ForeColor = System.Drawing.Color.Black;
                }

                double thP4_double = Convert.ToDouble(thP4.Text.Replace('.', ','));
                double thP4_REAL_double = Convert.ToDouble(thP4R.Text.Replace('.', ','));
                if (thP4_REAL_double > thP4_double * 1.1 || thP4_REAL_double < thP4_double * 0.9)
                {
                    thP4R.BackColor = System.Drawing.Color.Red;
                    thP4R.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thP4R.BackColor = System.Drawing.Color.Empty;
                    thP4R.ForeColor = System.Drawing.Color.Black;
                }

                double thP5_double = Convert.ToDouble(thP5.Text.Replace('.', ','));
                double thP5_REAL_double = Convert.ToDouble(thP5R.Text.Replace('.', ','));
                if (thP5_REAL_double > thP5_double * 1.1 || thP5_REAL_double < thP5_double * 0.9)
                {
                    thP5R.BackColor = System.Drawing.Color.Red;
                    thP5R.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thP5R.BackColor = System.Drawing.Color.Empty;
                    thP5R.ForeColor = System.Drawing.Color.Black;
                }

                double thP6_double = Convert.ToDouble(thP6.Text.Replace('.', ','));
                double thP6_REAL_double = Convert.ToDouble(thP6R.Text.Replace('.', ','));
                if (thP6_REAL_double > thP6_double * 1.1 || thP6_REAL_double < thP6_double * 0.9)
                {
                    thP6R.BackColor = System.Drawing.Color.Red;
                    thP6R.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thP6R.BackColor = System.Drawing.Color.Empty;
                    thP6R.ForeColor = System.Drawing.Color.Black;
                }

                double thP7_double = Convert.ToDouble(thP7.Text.Replace('.', ','));
                double thP7_REAL_double = Convert.ToDouble(thP7R.Text.Replace('.', ','));
                if (thP7_REAL_double > thP7_double * 1.1 || thP7_REAL_double < thP7_double * 0.9)
                {
                    thP7R.BackColor = System.Drawing.Color.Red;
                    thP7R.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thP7R.BackColor = System.Drawing.Color.Empty;
                    thP7R.ForeColor = System.Drawing.Color.Black;
                }

                double thP8_double = Convert.ToDouble(thP8.Text.Replace('.', ','));
                double thP8_REAL_double = Convert.ToDouble(thP8R.Text.Replace('.', ','));
                if (thP8_REAL_double > thP8_double * 1.1 || thP8_REAL_double < thP8_double * 0.9)
                {
                    thP8R.BackColor = System.Drawing.Color.Red;
                    thP8R.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thP8R.BackColor = System.Drawing.Color.Empty;
                    thP8R.ForeColor = System.Drawing.Color.Black;
                }
                double thP9_double = Convert.ToDouble(thP9.Text.Replace('.', ','));
                double thP9_REAL_double = Convert.ToDouble(thP9R.Text.Replace('.', ','));
                if (thP9_REAL_double > thP9_double * 1.1 || thP9_REAL_double < thP9_double * 0.9)
                {
                    thP9R.BackColor = System.Drawing.Color.Red;
                    thP9R.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thP9R.BackColor = System.Drawing.Color.Empty;
                    thP9R.ForeColor = System.Drawing.Color.Black;
                }
                double thP10_double = Convert.ToDouble(thP10.Text.Replace('.', ','));
                double thP10_REAL_double = Convert.ToDouble(thP10R.Text.Replace('.', ','));
                if (thP10_REAL_double > thP10_double * 1.1 || thP10_REAL_double < thP10_double * 0.9)
                {
                    thP10R.BackColor = System.Drawing.Color.Red;
                    thP10R.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thP10R.BackColor = System.Drawing.Color.Empty;
                    thP10R.ForeColor = System.Drawing.Color.Black;
                }
                double thTP1_double = Convert.ToDouble(thTP1.Text.Replace('.', ','));
                double thTP1_REAL_double = Convert.ToDouble(thTP1R.Text.Replace('.', ','));
                if (thTP1_REAL_double > thTP1_double * 1.1 || thTP1_REAL_double < thTP1_double * 0.9)
                {
                    thTP1R.BackColor = System.Drawing.Color.Red;
                    thTP1R.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thTP1R.BackColor = System.Drawing.Color.Empty;
                    thTP1R.ForeColor = System.Drawing.Color.Black;
                }

                double thTP2_double = Convert.ToDouble(thTP2.Text.Replace('.', ','));
                double thTP2_REAL_double = Convert.ToDouble(thTP2R.Text.Replace('.', ','));
                if (thTP2_REAL_double > thTP2_double * 1.1 || thTP2_REAL_double < thTP2_double * 0.9)
                {
                    thTP2R.BackColor = System.Drawing.Color.Red;
                    thTP2R.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thTP2R.BackColor = System.Drawing.Color.Empty;
                    thTP2R.ForeColor = System.Drawing.Color.Black;
                }

                double thTP3_double = Convert.ToDouble(thTP3.Text.Replace('.', ','));
                double thTP3_REAL_double = Convert.ToDouble(thTP3R.Text.Replace('.', ','));
                if (thTP3_REAL_double > thTP3_double * 1.1 || thTP3_REAL_double < thTP3_double * 0.9)
                {
                    thTP3R.BackColor = System.Drawing.Color.Red;
                    thTP3R.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thTP3R.BackColor = System.Drawing.Color.Empty;
                    thTP3R.ForeColor = System.Drawing.Color.Black;
                }

                double thTP4_double = Convert.ToDouble(thTP4.Text.Replace('.', ','));
                double thTP4_REAL_double = Convert.ToDouble(thTP4R.Text.Replace('.', ','));
                if (thTP4_REAL_double > thTP4_double * 1.1 || thTP4_REAL_double < thTP4_double * 0.9)
                {
                    thTP4R.BackColor = System.Drawing.Color.Red;
                    thTP4R.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thTP4R.BackColor = System.Drawing.Color.Empty;
                    thTP4R.ForeColor = System.Drawing.Color.Black;
                }

                double thTP5_double = Convert.ToDouble(thTP5.Text.Replace('.', ','));
                double thTP5_REAL_double = Convert.ToDouble(thTP5R.Text.Replace('.', ','));
                if (thTP5_REAL_double > thTP5_double * 1.1 || thTP5_REAL_double < thTP5_double * 0.9)
                {
                    thTP5R.BackColor = System.Drawing.Color.Red;
                    thTP5R.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thTP5R.BackColor = System.Drawing.Color.Empty;
                    thTP5R.ForeColor = System.Drawing.Color.Black;
                }

                double thTP6_double = Convert.ToDouble(thTP6.Text.Replace('.', ','));
                double thTP6_REAL_double = Convert.ToDouble(thTP6R.Text.Replace('.', ','));
                if (thTP6_REAL_double > thTP6_double * 1.1 || thTP6_REAL_double < thTP6_double * 0.9)
                {
                    thTP6R.BackColor = System.Drawing.Color.Red;
                    thTP6R.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thTP6R.BackColor = System.Drawing.Color.Empty;
                    thTP6R.ForeColor = System.Drawing.Color.Black;
                }

                double thTP7_double = Convert.ToDouble(thTP7.Text.Replace('.', ','));
                double thTP7_REAL_double = Convert.ToDouble(thTP7R.Text.Replace('.', ','));
                if (thTP7_REAL_double > thTP7_double * 1.1 || thTP7_REAL_double < thTP7_double * 0.9)
                {
                    thTP7R.BackColor = System.Drawing.Color.Red;
                    thTP7R.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thTP7R.BackColor = System.Drawing.Color.Empty;
                    thTP7R.ForeColor = System.Drawing.Color.Black;
                }

                double thTP8_double = Convert.ToDouble(thTP8.Text.Replace('.', ','));
                double thTP8_REAL_double = Convert.ToDouble(thTP8R.Text.Replace('.', ','));
                if (thTP8_REAL_double > thTP8_double * 1.1 || thTP8_REAL_double < thTP8_double * 0.9)
                {
                    thTP8R.BackColor = System.Drawing.Color.Red;
                    thTP8R.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thTP8R.BackColor = System.Drawing.Color.Empty;
                    thTP8R.ForeColor = System.Drawing.Color.Black;
                }
                double thTP9_double = Convert.ToDouble(thTP9.Text.Replace('.', ','));
                double thTP9_REAL_double = Convert.ToDouble(thTP9R.Text.Replace('.', ','));
                if (thTP9_REAL_double > thTP9_double * 1.1 || thTP9_REAL_double < thTP9_double * 0.9)
                {
                    thTP9R.BackColor = System.Drawing.Color.Red;
                    thTP9R.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thTP9R.BackColor = System.Drawing.Color.Empty;
                    thTP9R.ForeColor = System.Drawing.Color.Black;
                }
                double thTP10_double = Convert.ToDouble(thTP10.Text.Replace('.', ','));
                double thTP10_REAL_double = Convert.ToDouble(thTP10R.Text.Replace('.', ','));
                if (thTP10_REAL_double > thTP10_double * 1.1 || thTP10_REAL_double < thTP10_double * 0.9)
                {
                    thTP10R.BackColor = System.Drawing.Color.Red;
                    thTP10R.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thTP10R.BackColor = System.Drawing.Color.Empty;
                    thTP10R.ForeColor = System.Drawing.Color.Black;
                }

                //LIMITES
                double tbConmutacionREAL_double = Convert.ToDouble(tbConmutacionREAL.Text.Replace('.', ','));
                double thConmuntaciontolNVal_double = Convert.ToDouble(thConmuntaciontolNVal.Text.Replace('.', ','));
                double thConmuntaciontolMVal_double = Convert.ToDouble(thConmuntaciontolMVal.Text.Replace('.', ','));

                if (tbConmutacionREAL_double > thConmuntaciontolMVal_double || tbConmutacionREAL_double < thConmuntaciontolNVal_double)
                {
                    tbConmutacionREAL.BackColor = System.Drawing.Color.Red;
                    tbConmutacionREAL.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    tbConmutacionREAL.BackColor = System.Drawing.Color.Empty;
                    tbConmutacionREAL.ForeColor = System.Drawing.Color.Black;
                }

                double tbTiempoPresionREAL_double = Convert.ToDouble(tbTiempoPresionREAL.Text.Replace('.', ','));
                double tbTiempoPresiontolNVal_double = Convert.ToDouble(tbTiempoPresiontolNVal.Text.Replace('.', ','));
                double tbTiempoPresiontolMVal_double = Convert.ToDouble(tbTiempoPresiontolMVal.Text.Replace('.', ','));

                if (tbTiempoPresionREAL_double > tbTiempoPresiontolMVal_double || tbTiempoPresionREAL_double < tbTiempoPresiontolNVal_double)
                {
                    tbTiempoPresionREAL.BackColor = System.Drawing.Color.Red;
                    tbTiempoPresionREAL.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    tbTiempoPresionREAL.BackColor = System.Drawing.Color.Empty;
                    tbTiempoPresionREAL.ForeColor = System.Drawing.Color.Black;
                }


                double tbTiempoInyeccionREAL_double = Convert.ToDouble(tbTiempoInyeccionREAL.Text.Replace('.', ','));
                double tbTiempoInyeccionNVal_double = Convert.ToDouble(tbTiempoInyeccionNVal.Text.Replace('.', ','));
                double tbTiempoInyeccionMVal_double = Convert.ToDouble(tbTiempoInyeccionMVal.Text.Replace('.', ','));

                if (tbTiempoInyeccionREAL_double > tbTiempoInyeccionMVal_double || tbTiempoInyeccionREAL_double < tbTiempoInyeccionNVal_double)
                {
                    tbTiempoInyeccionREAL.BackColor = System.Drawing.Color.Red;
                    tbTiempoInyeccionREAL.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    tbTiempoInyeccionREAL.BackColor = System.Drawing.Color.Empty;
                    tbTiempoInyeccionREAL.ForeColor = System.Drawing.Color.Black;
                }

                double tbLimitePresionREAL_double = Convert.ToDouble(tbLimitePresionREAL.Text.Replace('.', ','));
                double tbLimitePresionNVal_double = Convert.ToDouble(tbLimitePresionNVal.Text.Replace('.', ','));
                double tbLimitePresionMVal_double = Convert.ToDouble(tbLimitePresionMVal.Text.Replace('.', ','));

                if (tbLimitePresionREAL_double > tbLimitePresionMVal_double || tbLimitePresionREAL_double < tbLimitePresionNVal_double)
                {
                    tbLimitePresionREAL.BackColor = System.Drawing.Color.Red;
                    tbLimitePresionREAL.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    tbLimitePresionREAL.BackColor = System.Drawing.Color.Empty;
                    tbLimitePresionREAL.ForeColor = System.Drawing.Color.Black;
                }

                double thVCargaREAL_double = Convert.ToDouble(thVCargaREAL.Text.Replace('.', ','));
                double TNvcargaval_double = Convert.ToDouble(TNvcargaval.Text.Replace('.', ','));
                double TMvcargaval_double = Convert.ToDouble(TMvcargaval.Text.Replace('.', ','));

                if (thVCargaREAL_double > TMvcargaval_double || thVCargaREAL_double < TNvcargaval_double)
                {
                    thVCargaREAL.BackColor = System.Drawing.Color.Red;
                    thVCargaREAL.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thVCargaREAL.BackColor = System.Drawing.Color.Empty;
                    thVCargaREAL.ForeColor = System.Drawing.Color.Black;
                }

                double thCargaREAL_double = Convert.ToDouble(thCargaREAL.Text.Replace('.', ','));
                double TNcargaval_double = Convert.ToDouble(TNcargaval.Text.Replace('.', ','));
                double TMcargaval_double = Convert.ToDouble(TMcargaval.Text.Replace('.', ','));
                if (thCargaREAL_double > TMcargaval_double || thCargaREAL_double < TNcargaval_double)
                {
                    thCargaREAL.BackColor = System.Drawing.Color.Red;
                    thCargaREAL.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thCargaREAL.BackColor = System.Drawing.Color.Empty;
                    thCargaREAL.ForeColor = System.Drawing.Color.Black;
                }

                double thDescompREAL_double = Convert.ToDouble(thDescompREAL.Text.Replace('.', ','));
                double TNdescomval_double = Convert.ToDouble(TNdescomval.Text.Replace('.', ','));
                double TMdescomval_double = Convert.ToDouble(TMdescomval.Text.Replace('.', ','));
                if (thDescompREAL_double > TMdescomval_double || thDescompREAL_double < TNdescomval_double)
                {
                    thDescompREAL.BackColor = System.Drawing.Color.Red;
                    thDescompREAL.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thDescompREAL.BackColor = System.Drawing.Color.Empty;
                    thDescompREAL.ForeColor = System.Drawing.Color.Black;
                }

                double thContraprREAL_double = Convert.ToDouble(thContraprREAL.Text.Replace('.', ','));
                double TNcontrapval_double = Convert.ToDouble(TNcontrapval.Text.Replace('.', ','));
                double TMcontrapval_double = Convert.ToDouble(TMcontrapval.Text.Replace('.', ','));
                if (thContraprREAL_double > TMcontrapval_double || thContraprREAL_double < TNcontrapval_double)
                {
                    thContraprREAL.BackColor = System.Drawing.Color.Red;
                    thContraprREAL.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thContraprREAL.BackColor = System.Drawing.Color.Empty;
                    thContraprREAL.ForeColor = System.Drawing.Color.Black;
                }

                double thTiempoREAL_double = Convert.ToDouble(thTiempoREAL.Text.Replace('.', ','));
                double TNTiempdosval_double = Convert.ToDouble(TNTiempdosval.Text.Replace('.', ','));
                double TMTiempdosval_double = Convert.ToDouble(TMTiempdosval.Text.Replace('.', ','));
                if (thTiempoREAL_double > TMTiempdosval_double || thTiempoREAL_double < TNTiempdosval_double)
                {
                    thTiempoREAL.BackColor = System.Drawing.Color.Red;
                    thTiempoREAL.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thTiempoREAL.BackColor = System.Drawing.Color.Empty;
                    thTiempoREAL.ForeColor = System.Drawing.Color.Black;
                }

                double thEnfriamientoREAL_double = Convert.ToDouble(thEnfriamientoREAL.Text.Replace('.', ','));
                double TNEnfriamval_double = Convert.ToDouble(TNEnfriamval.Text.Replace('.', ','));
                double TMEnfriamval_double = Convert.ToDouble(TMEnfriamval.Text.Replace('.', ','));
                if (thEnfriamientoREAL_double > TMEnfriamval_double || thEnfriamientoREAL_double < TNEnfriamval_double)
                {
                    thEnfriamientoREAL.BackColor = System.Drawing.Color.Red;
                    thEnfriamientoREAL.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thEnfriamientoREAL.BackColor = System.Drawing.Color.Empty;
                    thEnfriamientoREAL.ForeColor = System.Drawing.Color.Black;
                }

                double thCicloREAL_double = Convert.ToDouble(thCicloREAL.Text.Replace('.', ','));
                double TNCicloval_double = Convert.ToDouble(TNCicloval.Text.Replace('.', ','));
                double TMCicloval_double = Convert.ToDouble(TMCicloval.Text.Replace('.', ','));
                if (thCicloREAL_double > TMCicloval_double || thCicloREAL_double < TNCicloval_double)
                {
                    thCicloREAL.BackColor = System.Drawing.Color.Red;
                    thCicloREAL.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thCicloREAL.BackColor = System.Drawing.Color.Empty;
                    thCicloREAL.ForeColor = System.Drawing.Color.Black;
                }

                double thCojinREAL_double = Convert.ToDouble(thCojinREAL.Text.Replace('.', ','));
                double TNCojinval_double = Convert.ToDouble(TNCojinval.Text.Replace('.', ','));
                double TMCojinval_double = Convert.ToDouble(TMCojinval.Text.Replace('.', ','));
                if (thCojinREAL_double > TMCojinval_double || thCojinREAL_double < TNCojinval_double)
                {
                    thCojinREAL.BackColor = System.Drawing.Color.Red;
                    thCojinREAL.ForeColor = System.Drawing.Color.White; ResumenPROD1.Visible = true; ResumenPROD2.Visible = true; if (MotivoCambioParam.Text != "") { ResumenPROD2.Text = MotivoCambioParam.Text; }
                    matparamNC.Visible = true;
                }
                else
                {
                    thCojinREAL.BackColor = System.Drawing.Color.Empty;
                    thCojinREAL.ForeColor = System.Drawing.Color.Black;
                }
            }
            catch (Exception) { }
        }
        public void EvaluaParam()
        {
            //COMPARA Y asigna desviacion
            try
            {
                PARAMSESTADO.Text = "";
                //CILINDRO
                double thBoq_double = Convert.ToDouble(thBoq.Text.Replace('.', ','));
                double thBoq_REAL_double = Convert.ToDouble(thBoqREAL.Text.Replace('.', ','));
                if (thBoq_REAL_double > thBoq_double * 1.1 || thBoq_REAL_double < thBoq_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }

                double thT1_double = Convert.ToDouble(thT1.Text.Replace('.', ','));
                double thT1_REAL_double = Convert.ToDouble(thT1REAL.Text.Replace('.', ','));
                if (thT1_REAL_double > thT1_double * 1.1 || thT1_REAL_double < thT1_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }

                double thT2_double = Convert.ToDouble(thT2.Text.Replace('.', ','));
                double thT2_REAL_double = Convert.ToDouble(thT2REAL.Text.Replace('.', ','));
                if (thT2_REAL_double > thT2_double * 1.1 || thT2_REAL_double < thT2_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thT3_double = Convert.ToDouble(thT3.Text.Replace('.', ','));
                double thT3_REAL_double = Convert.ToDouble(thT3REAL.Text.Replace('.', ','));
                if (thT3_REAL_double > thT3_double * 1.1 || thT3_REAL_double < thT3_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thT4_double = Convert.ToDouble(thT4.Text.Replace('.', ','));
                double thT4_REAL_double = Convert.ToDouble(thT4REAL.Text.Replace('.', ','));
                if (thT4_REAL_double > thT4_double * 1.1 || thT4_REAL_double < thT4_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thT5_double = Convert.ToDouble(thT5.Text.Replace('.', ','));
                double thT5_REAL_double = Convert.ToDouble(thT5REAL.Text.Replace('.', ','));
                if (thT5_REAL_double > thT5_double * 1.1 || thT5_REAL_double < thT5_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thT6_double = Convert.ToDouble(thT6.Text.Replace('.', ','));
                double thT6_REAL_double = Convert.ToDouble(thT6REAL.Text.Replace('.', ','));
                if (thT6_REAL_double > thT6_double * 1.1 || thT6_REAL_double < thT6_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thT7_double = Convert.ToDouble(thT7.Text.Replace('.', ','));
                double thT7_REAL_double = Convert.ToDouble(thT7REAL.Text.Replace('.', ','));
                if (thT7_REAL_double > thT7_double * 1.1 || thT7_REAL_double < thT7_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thT8_double = Convert.ToDouble(thT8.Text.Replace('.', ','));
                double thT8_REAL_double = Convert.ToDouble(thT8REAL.Text.Replace('.', ','));
                if (thT8_REAL_double > thT8_double * 1.1 || thT8_REAL_double < thT8_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thT9_double = Convert.ToDouble(thT9.Text.Replace('.', ','));
                double thT9_REAL_double = Convert.ToDouble(thT9REAL.Text.Replace('.', ','));
                if (thT9_REAL_double > thT9_double * 1.1 || thT9_REAL_double < thT9_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thT10_double = Convert.ToDouble(thT10.Text.Replace('.', ','));
                double thT10_REAL_double = Convert.ToDouble(thT10REAL.Text.Replace('.', ','));
                if (thT10_REAL_double > thT10_double * 1.1 || thT10_REAL_double < thT10_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                //CÁMARA CALIENTE

                double thZ1_double = Convert.ToDouble(thZ1.Text.Replace('.', ','));
                double thZ1_REAL_double = Convert.ToDouble(thZ1REAL.Text.Replace('.', ','));
                if (thZ1_REAL_double > thZ1_double * 1.1 || thZ1_REAL_double < thZ1_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thZ2_double = Convert.ToDouble(thZ2.Text.Replace('.', ','));
                double thZ2_REAL_double = Convert.ToDouble(thZ2REAL.Text.Replace('.', ','));
                if (thZ2_REAL_double > thZ2_double * 1.1 || thZ2_REAL_double < thZ2_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thZ3_double = Convert.ToDouble(thZ3.Text.Replace('.', ','));
                double thZ3_REAL_double = Convert.ToDouble(thZ3REAL.Text.Replace('.', ','));
                if (thZ3_REAL_double > thZ3_double * 1.1 || thZ3_REAL_double < thZ3_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thZ4_double = Convert.ToDouble(thZ4.Text.Replace('.', ','));
                double thZ4_REAL_double = Convert.ToDouble(thZ4REAL.Text.Replace('.', ','));
                if (thZ4_REAL_double > thZ4_double * 1.1 || thZ4_REAL_double < thZ4_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thZ5_double = Convert.ToDouble(thZ5.Text.Replace('.', ','));
                double thZ5_REAL_double = Convert.ToDouble(thZ5REAL.Text.Replace('.', ','));
                if (thZ5_REAL_double > thZ5_double * 1.1 || thZ5_REAL_double < thZ5_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thZ6_double = Convert.ToDouble(thZ6.Text.Replace('.', ','));
                double thZ6_REAL_double = Convert.ToDouble(thZ6REAL.Text.Replace('.', ','));
                if (thZ6_REAL_double > thZ6_double * 1.1 || thZ6_REAL_double < thZ6_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thZ7_double = Convert.ToDouble(thZ7.Text.Replace('.', ','));
                double thZ7_REAL_double = Convert.ToDouble(thZ7REAL.Text.Replace('.', ','));
                if (thZ7_REAL_double > thZ7_double * 1.1 || thZ7_REAL_double < thZ7_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thZ8_double = Convert.ToDouble(thZ8.Text.Replace('.', ','));
                double thZ8_REAL_double = Convert.ToDouble(thZ8REAL.Text.Replace('.', ','));
                if (thZ8_REAL_double > thZ8_double * 1.1 || thZ8_REAL_double < thZ8_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thZ9_double = Convert.ToDouble(thZ9.Text.Replace('.', ','));
                double thZ9_REAL_double = Convert.ToDouble(thZ9REAL.Text.Replace('.', ','));
                if (thZ9_REAL_double > thZ9_double * 1.1 || thZ9_REAL_double < thZ9_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thZ10_double = Convert.ToDouble(thZ10.Text.Replace('.', ','));
                double thZ10_REAL_double = Convert.ToDouble(thZ10REAL.Text.Replace('.', ','));
                if (thZ10_REAL_double > thZ10_double * 1.1 || thZ10_REAL_double < thZ10_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thZ11_double = Convert.ToDouble(thZ11.Text.Replace('.', ','));
                double thZ11_REAL_double = Convert.ToDouble(thZ11REAL.Text.Replace('.', ','));
                if (thZ11_REAL_double > thZ11_double * 1.1 || thZ11_REAL_double < thZ11_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thZ12_double = Convert.ToDouble(thZ12.Text.Replace('.', ','));
                double thZ12_REAL_double = Convert.ToDouble(thZ12REAL.Text.Replace('.', ','));
                if (thZ12_REAL_double > thZ12_double * 1.1 || thZ12_REAL_double < thZ12_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thZ13_double = Convert.ToDouble(thZ13.Text.Replace('.', ','));
                double thZ13_REAL_double = Convert.ToDouble(thZ13REAL.Text.Replace('.', ','));
                if (thZ13_REAL_double > thZ13_double * 1.1 || thZ13_REAL_double < thZ13_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thZ14_double = Convert.ToDouble(thZ14.Text.Replace('.', ','));
                double thZ14_REAL_double = Convert.ToDouble(thZ14REAL.Text.Replace('.', ','));
                if (thZ14_REAL_double > thZ14_double * 1.1 || thZ14_REAL_double < thZ14_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thZ15_double = Convert.ToDouble(thZ15.Text.Replace('.', ','));
                double thZ15_REAL_double = Convert.ToDouble(thZ15REAL.Text.Replace('.', ','));
                if (thZ15_REAL_double > thZ15_double * 1.1 || thZ15_REAL_double < thZ15_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thZ16_double = Convert.ToDouble(thZ16.Text.Replace('.', ','));
                double thZ16_REAL_double = Convert.ToDouble(thZ16REAL.Text.Replace('.', ','));
                if (thZ16_REAL_double > thZ16_double * 1.1 || thZ16_REAL_double < thZ16_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thZ17_double = Convert.ToDouble(thZ17.Text.Replace('.', ','));
                double thZ17_REAL_double = Convert.ToDouble(thZ17REAL.Text.Replace('.', ','));
                if (thZ17_REAL_double > thZ17_double * 1.1 || thZ17_REAL_double < thZ17_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thZ18_double = Convert.ToDouble(thZ18.Text.Replace('.', ','));
                double thZ18_REAL_double = Convert.ToDouble(thZ18REAL.Text.Replace('.', ','));
                if (thZ18_REAL_double > thZ18_double * 1.1 || thZ18_REAL_double < thZ18_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thZ19_double = Convert.ToDouble(thZ19.Text.Replace('.', ','));
                double thZ19_REAL_double = Convert.ToDouble(thZ19REAL.Text.Replace('.', ','));
                if (thZ19_REAL_double > thZ19_double * 1.1 || thZ19_REAL_double < thZ19_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thZ20_double = Convert.ToDouble(thZ20.Text.Replace('.', ','));
                double thZ20_REAL_double = Convert.ToDouble(thZ20REAL.Text.Replace('.', ','));
                if (thZ20_REAL_double > thZ20_double * 1.1 || thZ20_REAL_double < thZ20_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                //POSTPRESION
                double thP1_double = Convert.ToDouble(thP1.Text.Replace('.', ','));
                double thP1_REAL_double = Convert.ToDouble(thP1R.Text.Replace('.', ','));
                if (thP1_REAL_double > thP1_double * 1.1 || thP1_REAL_double < thP1_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thP2_double = Convert.ToDouble(thP2.Text.Replace('.', ','));
                double thP2_REAL_double = Convert.ToDouble(thP2R.Text.Replace('.', ','));
                if (thP2_REAL_double > thP2_double * 1.1 || thP2_REAL_double < thP2_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thP3_double = Convert.ToDouble(thP3.Text.Replace('.', ','));
                double thP3_REAL_double = Convert.ToDouble(thP3R.Text.Replace('.', ','));
                if (thP3_REAL_double > thP3_double * 1.1 || thP3_REAL_double < thP3_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thP4_double = Convert.ToDouble(thP4.Text.Replace('.', ','));
                double thP4_REAL_double = Convert.ToDouble(thP4R.Text.Replace('.', ','));
                if (thP4_REAL_double > thP4_double * 1.1 || thP4_REAL_double < thP4_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thP5_double = Convert.ToDouble(thP5.Text.Replace('.', ','));
                double thP5_REAL_double = Convert.ToDouble(thP5R.Text.Replace('.', ','));
                if (thP5_REAL_double > thP5_double * 1.1 || thP5_REAL_double < thP5_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thP6_double = Convert.ToDouble(thP6.Text.Replace('.', ','));
                double thP6_REAL_double = Convert.ToDouble(thP6R.Text.Replace('.', ','));
                if (thP6_REAL_double > thP6_double * 1.1 || thP6_REAL_double < thP6_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thP7_double = Convert.ToDouble(thP7.Text.Replace('.', ','));
                double thP7_REAL_double = Convert.ToDouble(thP7R.Text.Replace('.', ','));
                if (thP7_REAL_double > thP7_double * 1.1 || thP7_REAL_double < thP7_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thP8_double = Convert.ToDouble(thP8.Text.Replace('.', ','));
                double thP8_REAL_double = Convert.ToDouble(thP8R.Text.Replace('.', ','));
                if (thP8_REAL_double > thP8_double * 1.1 || thP8_REAL_double < thP8_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thP9_double = Convert.ToDouble(thP9.Text.Replace('.', ','));
                double thP9_REAL_double = Convert.ToDouble(thP9R.Text.Replace('.', ','));
                if (thP9_REAL_double > thP9_double * 1.1 || thP9_REAL_double < thP9_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thP10_double = Convert.ToDouble(thP10.Text.Replace('.', ','));
                double thP10_REAL_double = Convert.ToDouble(thP10R.Text.Replace('.', ','));
                if (thP10_REAL_double > thP10_double * 1.1 || thP10_REAL_double < thP10_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thTP1_double = Convert.ToDouble(thTP1.Text.Replace('.', ','));
                double thTP1_REAL_double = Convert.ToDouble(thTP1R.Text.Replace('.', ','));
                if (thTP1_REAL_double > thTP1_double * 1.1 || thTP1_REAL_double < thTP1_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thTP2_double = Convert.ToDouble(thTP2.Text.Replace('.', ','));
                double thTP2_REAL_double = Convert.ToDouble(thTP2R.Text.Replace('.', ','));
                if (thTP2_REAL_double > thTP2_double * 1.1 || thTP2_REAL_double < thTP2_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thTP3_double = Convert.ToDouble(thTP3.Text.Replace('.', ','));
                double thTP3_REAL_double = Convert.ToDouble(thTP3R.Text.Replace('.', ','));
                if (thTP3_REAL_double > thTP3_double * 1.1 || thTP3_REAL_double < thTP3_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thTP4_double = Convert.ToDouble(thTP4.Text.Replace('.', ','));
                double thTP4_REAL_double = Convert.ToDouble(thTP4R.Text.Replace('.', ','));
                if (thTP4_REAL_double > thTP4_double * 1.1 || thTP4_REAL_double < thTP4_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thTP5_double = Convert.ToDouble(thTP5.Text.Replace('.', ','));
                double thTP5_REAL_double = Convert.ToDouble(thTP5R.Text.Replace('.', ','));
                if (thTP5_REAL_double > thTP5_double * 1.1 || thTP5_REAL_double < thTP5_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thTP6_double = Convert.ToDouble(thTP6.Text.Replace('.', ','));
                double thTP6_REAL_double = Convert.ToDouble(thTP6R.Text.Replace('.', ','));
                if (thTP6_REAL_double > thTP6_double * 1.1 || thTP6_REAL_double < thTP6_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thTP7_double = Convert.ToDouble(thTP7.Text.Replace('.', ','));
                double thTP7_REAL_double = Convert.ToDouble(thTP7R.Text.Replace('.', ','));
                if (thTP7_REAL_double > thTP7_double * 1.1 || thTP7_REAL_double < thTP7_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thTP8_double = Convert.ToDouble(thTP8.Text.Replace('.', ','));
                double thTP8_REAL_double = Convert.ToDouble(thTP8R.Text.Replace('.', ','));
                if (thTP8_REAL_double > thTP8_double * 1.1 || thTP8_REAL_double < thTP8_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thTP9_double = Convert.ToDouble(thTP9.Text.Replace('.', ','));
                double thTP9_REAL_double = Convert.ToDouble(thTP9R.Text.Replace('.', ','));
                if (thTP9_REAL_double > thTP9_double * 1.1 || thTP9_REAL_double < thTP9_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thTP10_double = Convert.ToDouble(thTP10.Text.Replace('.', ','));
                double thTP10_REAL_double = Convert.ToDouble(thTP10R.Text.Replace('.', ','));
                if (thTP10_REAL_double > thTP10_double * 1.1 || thTP10_REAL_double < thTP10_double * 0.9)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                //LIMITES
                double tbConmutacionREAL_double = Convert.ToDouble(tbConmutacionREAL.Text.Replace('.', ','));
                double thConmuntaciontolNVal_double = Convert.ToDouble(thConmuntaciontolNVal.Text.Replace('.', ','));
                double thConmuntaciontolMVal_double = Convert.ToDouble(thConmuntaciontolMVal.Text.Replace('.', ','));

                if (tbConmutacionREAL_double > thConmuntaciontolMVal_double || tbConmutacionREAL_double < thConmuntaciontolNVal_double)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double tbTiempoPresionREAL_double = Convert.ToDouble(tbTiempoPresionREAL.Text.Replace('.', ','));
                double tbTiempoPresiontolNVal_double = Convert.ToDouble(tbTiempoPresiontolNVal.Text.Replace('.', ','));
                double tbTiempoPresiontolMVal_double = Convert.ToDouble(tbTiempoPresiontolMVal.Text.Replace('.', ','));

                if (tbTiempoPresionREAL_double > tbTiempoPresiontolMVal_double || tbTiempoPresionREAL_double < tbTiempoPresiontolNVal_double)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                


                double tbTiempoInyeccionREAL_double = Convert.ToDouble(tbTiempoInyeccionREAL.Text.Replace('.', ','));
                double tbTiempoInyeccionNVal_double = Convert.ToDouble(tbTiempoInyeccionNVal.Text.Replace('.', ','));
                double tbTiempoInyeccionMVal_double = Convert.ToDouble(tbTiempoInyeccionMVal.Text.Replace('.', ','));

                if (tbTiempoInyeccionREAL_double > tbTiempoInyeccionMVal_double || tbTiempoInyeccionREAL_double < tbTiempoInyeccionNVal_double)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double tbLimitePresionREAL_double = Convert.ToDouble(tbLimitePresionREAL.Text.Replace('.', ','));
                double tbLimitePresionNVal_double = Convert.ToDouble(tbLimitePresionNVal.Text.Replace('.', ','));
                double tbLimitePresionMVal_double = Convert.ToDouble(tbLimitePresionMVal.Text.Replace('.', ','));

                if (tbLimitePresionREAL_double > tbLimitePresionMVal_double || tbLimitePresionREAL_double < tbLimitePresionNVal_double)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thVCargaREAL_double = Convert.ToDouble(thVCargaREAL.Text.Replace('.', ','));
                double TNvcargaval_double = Convert.ToDouble(TNvcargaval.Text.Replace('.', ','));
                double TMvcargaval_double = Convert.ToDouble(TMvcargaval.Text.Replace('.', ','));

                if (thVCargaREAL_double > TMvcargaval_double || thVCargaREAL_double < TNvcargaval_double)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thCargaREAL_double = Convert.ToDouble(thCargaREAL.Text.Replace('.', ','));
                double TNcargaval_double = Convert.ToDouble(TNcargaval.Text.Replace('.', ','));
                double TMcargaval_double = Convert.ToDouble(TMcargaval.Text.Replace('.', ','));
                if (thCargaREAL_double > TMcargaval_double || thCargaREAL_double < TNcargaval_double)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thDescompREAL_double = Convert.ToDouble(thDescompREAL.Text.Replace('.', ','));
                double TNdescomval_double = Convert.ToDouble(TNdescomval.Text.Replace('.', ','));
                double TMdescomval_double = Convert.ToDouble(TMdescomval.Text.Replace('.', ','));
                if (thDescompREAL_double > TMdescomval_double || thDescompREAL_double < TNdescomval_double)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thContraprREAL_double = Convert.ToDouble(thContraprREAL.Text.Replace('.', ','));
                double TNcontrapval_double = Convert.ToDouble(TNcontrapval.Text.Replace('.', ','));
                double TMcontrapval_double = Convert.ToDouble(TMcontrapval.Text.Replace('.', ','));
                if (thContraprREAL_double > TMcontrapval_double || thContraprREAL_double < TNcontrapval_double)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thTiempoREAL_double = Convert.ToDouble(thTiempoREAL.Text.Replace('.', ','));
                double TNTiempdosval_double = Convert.ToDouble(TNTiempdosval.Text.Replace('.', ','));
                double TMTiempdosval_double = Convert.ToDouble(TMTiempdosval.Text.Replace('.', ','));
                if (thTiempoREAL_double > TMTiempdosval_double || thTiempoREAL_double < TNTiempdosval_double)
                {
                    PARAMSESTADO.Text = "1";
                }
               
                

                double thEnfriamientoREAL_double = Convert.ToDouble(thEnfriamientoREAL.Text.Replace('.', ','));
                double TNEnfriamval_double = Convert.ToDouble(TNEnfriamval.Text.Replace('.', ','));
                double TMEnfriamval_double = Convert.ToDouble(TMEnfriamval.Text.Replace('.', ','));
                if (thEnfriamientoREAL_double > TMEnfriamval_double || thEnfriamientoREAL_double < TNEnfriamval_double)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thCicloREAL_double = Convert.ToDouble(thCicloREAL.Text.Replace('.', ','));
                double TNCicloval_double = Convert.ToDouble(TNCicloval.Text.Replace('.', ','));
                double TMCicloval_double = Convert.ToDouble(TMCicloval.Text.Replace('.', ','));
                if (thCicloREAL_double > TMCicloval_double || thCicloREAL_double < TNCicloval_double)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

                double thCojinREAL_double = Convert.ToDouble(thCojinREAL.Text.Replace('.', ','));
                double TNCojinval_double = Convert.ToDouble(TNCojinval.Text.Replace('.', ','));
                double TMCojinval_double = Convert.ToDouble(TMCojinval.Text.Replace('.', ','));
                if (thCojinREAL_double > TMCojinval_double || thCojinREAL_double < TNCojinval_double)
                {
                    PARAMSESTADO.Text = "1";
                }
                
                

            }
            catch (Exception) 
            {
            }
        }
        public void RellenarVacíos()
        {
            try
            {
                //RELLENO TEMPERATURAS DE CILINDRO
                if (thBoqREAL.Text == "")
                { thBoqREAL.Text = thBoq.Text; }
                if (thT1REAL.Text == "")
                { thT1REAL.Text = thT1.Text; }
                if (thT2REAL.Text == "")
                { thT2REAL.Text = thT2.Text; }
                if (thT3REAL.Text == "")
                { thT3REAL.Text = thT3.Text; }
                if (thT4REAL.Text == "")
                { thT4REAL.Text = thT4.Text; }
                if (thT5REAL.Text == "")
                { thT5REAL.Text = thT5.Text; }
                if (thT6REAL.Text == "")
                { thT6REAL.Text = thT6.Text; }
                if (thT7REAL.Text == "")
                { thT7REAL.Text = thT7.Text; }
                if (thT8REAL.Text == "")
                { thT8REAL.Text = thT8.Text; }
                if (thT9REAL.Text == "")
                { thT9REAL.Text = thT9.Text; }
                if (thT10REAL.Text == "")
                { thT10REAL.Text = thT10.Text; }

                //RELLENO CAMARA CALIENTE
                if (thZ1REAL.Text == "")
                { thZ1REAL.Text = thZ1.Text; }
                if (thZ2REAL.Text == "")
                { thZ2REAL.Text = thZ2.Text; }
                if (thZ3REAL.Text == "")
                { thZ3REAL.Text = thZ3.Text; }
                if (thZ4REAL.Text == "")
                { thZ4REAL.Text = thZ4.Text; }
                if (thZ5REAL.Text == "")
                { thZ5REAL.Text = thZ5.Text; }
                if (thZ6REAL.Text == "")
                { thZ6REAL.Text = thZ6.Text; }
                if (thZ7REAL.Text == "")
                { thZ7REAL.Text = thZ7.Text; }
                if (thZ8REAL.Text == "")
                { thZ8REAL.Text = thZ8.Text; }
                if (thZ9REAL.Text == "")
                { thZ9REAL.Text = thZ9.Text; }
                if (thZ10REAL.Text == "")
                { thZ10REAL.Text = thZ10.Text; }
                if (thZ11REAL.Text == "")
                { thZ11REAL.Text = thZ11.Text; }
                if (thZ12REAL.Text == "")
                { thZ12REAL.Text = thZ12.Text; }
                if (thZ13REAL.Text == "")
                { thZ13REAL.Text = thZ13.Text; }
                if (thZ14REAL.Text == "")
                { thZ14REAL.Text = thZ14.Text; }
                if (thZ15REAL.Text == "")
                { thZ15REAL.Text = thZ15.Text; }
                if (thZ16REAL.Text == "")
                { thZ16REAL.Text = thZ16.Text; }
                if (thZ17REAL.Text == "")
                { thZ17REAL.Text = thZ17.Text; }
                if (thZ18REAL.Text == "")
                { thZ18REAL.Text = thZ18.Text; }
                if (thZ19REAL.Text == "")
                { thZ19REAL.Text = thZ19.Text; }
                if (thZ20REAL.Text == "")
                { thZ20REAL.Text = thZ20.Text; }

                //RELLENO POSTPRESION
                if (thP1R.Text == "")
                { thP1R.Text = thP1.Text; }
                if (thP2R.Text == "")
                { thP2R.Text = thP2.Text; }
                if (thP3R.Text == "")
                { thP3R.Text = thP3.Text; }
                if (thP4R.Text == "")
                { thP4R.Text = thP4.Text; }
                if (thP5R.Text == "")
                { thP5R.Text = thP5.Text; }
                if (thP6R.Text == "")
                { thP6R.Text = thP6.Text; }
                if (thP7R.Text == "")
                { thP7R.Text = thP7.Text; }
                if (thP8R.Text == "")
                { thP8R.Text = thP8.Text; }
                if (thP9R.Text == "")
                { thP9R.Text = thP9.Text; }
                if (thP10R.Text == "")
                { thP10R.Text = thP10.Text; }
                if (thTP1R.Text == "")
                { thTP1R.Text = thTP1.Text; }
                if (thTP2R.Text == "")
                { thTP2R.Text = thTP2.Text; }
                if (thTP3R.Text == "")
                { thTP3R.Text = thTP3.Text; }
                if (thTP4R.Text == "")
                { thTP4R.Text = thTP4.Text; }
                if (thTP5R.Text == "")
                { thTP5R.Text = thTP5.Text; }
                if (thTP6R.Text == "")
                { thTP6R.Text = thTP6.Text; }
                if (thTP7R.Text == "")
                { thTP7R.Text = thTP7.Text; }
                if (thTP8R.Text == "")
                { thTP8R.Text = thTP8.Text; }
                if (thTP9R.Text == "")
                { thTP9R.Text = thTP9.Text; }
                if (thTP10R.Text == "")
                { thTP10R.Text = thTP10.Text; }

                //RELLENO ATEMPERADO
                if (TbCaudalF1REAL.Text == "")
                { TbCaudalF1REAL.Text = TbCaudalF1.Text; }
                if (TbCaudalF2REAL.Text == "")
                { TbCaudalF2REAL.Text = TbCaudalF2.Text; }
                if (TbCaudalF3REAL.Text == "")
                { TbCaudalF3REAL.Text = TbCaudalF3.Text; }
                if (TbCaudalF4REAL.Text == "")
                { TbCaudalF4REAL.Text = TbCaudalF4.Text; }
                if (TbCaudalF5REAL.Text == "")
                { TbCaudalF5REAL.Text = TbCaudalF5.Text; }
                if (TbCaudalF6REAL.Text == "")
                { TbCaudalF6REAL.Text = TbCaudalF6.Text; }

                if (TbCaudalM1REAL.Text == "")
                { TbCaudalM1REAL.Text = TbCaudalM1.Text; }
                if (TbCaudalM2REAL.Text == "")
                { TbCaudalM2REAL.Text = TbCaudalM2.Text; }
                if (TbCaudalM3REAL.Text == "")
                { TbCaudalM3REAL.Text = TbCaudalM3.Text; }
                if (TbCaudalM4REAL.Text == "")
                { TbCaudalM4REAL.Text = TbCaudalM4.Text; }
                if (TbCaudalM5REAL.Text == "")
                { TbCaudalM5REAL.Text = TbCaudalM5.Text; }
                if (TbCaudalM6REAL.Text == "")
                { TbCaudalM6REAL.Text = TbCaudalM6.Text; }

                if (TbTemperaturaF1REAL.Text == "")
                { TbTemperaturaF1REAL.Text = TbTemperaturaF1.Text; }
                if (TbTemperaturaF2REAL.Text == "")
                { TbTemperaturaF2REAL.Text = TbTemperaturaF2.Text; }
                if (TbTemperaturaF3REAL.Text == "")
                { TbTemperaturaF3REAL.Text = TbTemperaturaF3.Text; }
                if (TbTemperaturaF4REAL.Text == "")
                { TbTemperaturaF4REAL.Text = TbTemperaturaF4.Text; }
                if (TbTemperaturaF5REAL.Text == "")
                { TbTemperaturaF5REAL.Text = TbTemperaturaF5.Text; }
                if (TbTemperaturaF6REAL.Text == "")
                { TbTemperaturaF6REAL.Text = TbTemperaturaF6.Text; }

                if (TbTemperaturaM1REAL.Text == "")
                { TbTemperaturaM1REAL.Text = TbTemperaturaM1.Text; }
                if (TbTemperaturaM2REAL.Text == "")
                { TbTemperaturaM2REAL.Text = TbTemperaturaM2.Text; }
                if (TbTemperaturaM3REAL.Text == "")
                { TbTemperaturaM3REAL.Text = TbTemperaturaM3.Text; }
                if (TbTemperaturaM4REAL.Text == "")
                { TbTemperaturaM4REAL.Text = TbTemperaturaM4.Text; }
                if (TbTemperaturaM5REAL.Text == "")
                { TbTemperaturaM5REAL.Text = TbTemperaturaM5.Text; }
                if (TbTemperaturaM6REAL.Text == "")
                { TbTemperaturaM6REAL.Text = TbTemperaturaM6.Text; }

                //LIMITES
                if (tbConmutacionREAL.Text == "")
                { tbConmutacionREAL.Text = tbConmutacion.Text; }
                if (tbTiempoPresionREAL.Text == "")
                { tbTiempoPresionREAL.Text = tbTiempoPresion.Text; }
                if (tbTiempoInyeccionREAL.Text == "")
                { tbTiempoInyeccionREAL.Text = tbTiempoInyeccion.Text; }
                if (tbLimitePresionREAL.Text == "")
                { tbLimitePresionREAL.Text = tbLimitePresion.Text; }
                if (thVCargaREAL.Text == "")
                { thVCargaREAL.Text = thVCarga.Text; }
                if (thCargaREAL.Text == "")
                { thCargaREAL.Text = thCarga.Text; }
                if (thDescompREAL.Text == "")
                { thDescompREAL.Text = thDescomp.Text; }
                if (thContraprREAL.Text == "")
                { thContraprREAL.Text = thContrapr.Text; }
                if (thTiempoREAL.Text == "")
                { thTiempoREAL.Text = thTiempo.Text; }
                if (thEnfriamientoREAL.Text == "")
                { thEnfriamientoREAL.Text = thEnfriamiento.Text; }
                if (thCicloREAL.Text == "")
                { thCicloREAL.Text = thCiclo.Text; }
                if (thCojinREAL.Text == "")
                { thCojinREAL.Text = thCojin.Text; }
            }
            catch (Exception)
            { }

        }
        public void CargarResultados()
        {
            try
            {
                //REOCULTO CELDAS PARA EVALUARLAS
                formaNC.Visible = false;
                audinc.Visible = false;
                SINDESVIACIONES.Visible = true;


                ResumenFORM1.Visible = false;
                ResumenFORM2.Visible = false;
                ResumenFORM3.Visible = false;
                ResumenFORM4.Visible = false;
                ResumenFORM4ILUO.Visible = false;

                ResumenMAT1.Visible = false;
                ResumenGRAL1.Visible = false;
                ResumenGRAL2.Visible = false;
                ResumenGRAL3.Visible = false;
                Resumen1.Visible = false;
                Resumen1ENC.Visible = false;
                Resumen2.Visible = false;
                Resumen2ENC.Visible = false;
                Resumen3.Visible = false;
                Resumen3ENC.Visible = false;
                Resumen4.Visible = false;
                Resumen4ENC.Visible = false;
                Resumen5.Visible = false;
                Resumen5ENC.Visible = false;
                Resumen6.Visible = false;
                Resumen6ENC.Visible = false;
                Resumen6CAL.Visible = false;
                Resumen7.Visible = false;
                Resumen7ENC.Visible = false;
                Resumen7CAL.Visible = false;
                Resumen8.Visible = false;
                Resumen8ENC.Visible = false;
                Resumen8CAL.Visible = false;
                Resumen9.Visible = false;
                Resumen9ENC.Visible = false;
                Resumen9CAL.Visible = false;
                Resumen10.Visible = false;
                Resumen10CAL.Visible = false;

                //EVALUO Y ACTIVO LAS DESVIACIONES
                if (EXISTEFICHA.Text == "1")
                    { ResumenPROD3.Visible = true;
                      matparamNC.Visible = true;
                      SINDESVIACIONES.Visible = false;
                }

                //if (LiberacionEncargadoHora.Text != "" || LiberacionCalidadHora.Text != "") 
                if (LiberacionEncargadoHoraORI.Text != "" || LiberacionCalidadHoraORI.Text != "") //LIBNUEVO
                    { 
                if (A3.Visible == true) ///no confor 3nc
                    { ResumenFORM1.Visible = true;
                      formaNC.Visible = true;
                      SINDESVIACIONES.Visible = false;
                    }
                if (A5.Visible == true) ///gp12 3nc
                    { ResumenFORM2.Visible = true;
                        formaNC.Visible = true;
                        SINDESVIACIONES.Visible = false;
                    }
                if (A7.Visible == true) ///no confor cal
                    { ResumenFORM3.Visible = true;
                        formaNC.Visible = true;
                        SINDESVIACIONES.Visible = false;
                    }
                if (A8.Visible == true) ///gp12 cal
                    { ResumenFORM4.Visible = true;
                        formaNC.Visible = true;
                        SINDESVIACIONES.Visible = false;
                    }
                if (Operario1Nivel.SelectedValue.ToString() == "I")
                    { ResumenFORM4ILUO.Visible = true;
                        formaNC.Visible = true;
                        SINDESVIACIONES.Visible = false;
                    }
                if ((Operario2Nivel.Visible == true) && (Operario2Nivel.SelectedValue.ToString() == "I"))
                    { ResumenFORM4ILUO.Visible = true;
                        formaNC.Visible = true;
                        SINDESVIACIONES.Visible = false;
                    }

                 if ((MAT1REF.Visible == true && MAT1LOT.Text == "") || (MAT2REF.Visible == true && MAT2LOT.Text == "") || (MAT3REF.Visible == true && MAT3LOT.Text == "") || (COMP1REF.Visible == true && COMP1LOT.Text == "") || (COMP2REF.Visible == true && COMP2LOT.Text == "") || (COMP3REF.Visible == true && COMP3LOT.Text == "") || (COMP4REF.Visible == true && COMP4LOT.Text == "") || (COMP5REF.Visible == true && COMP5LOT.Text == "") || (COMP6REF.Visible == true && COMP6LOT.Text == "") || (COMP7REF.Visible == true && COMP7LOT.Text == ""))
                    { ResumenMAT1.Visible = true;
                      matparamNC.Visible = true;
                      
                    }

                    //PREPARAR CONDICIONAL DE PARAMETROS AQUÍ

                    if (Q1_NOKOPT.Checked)
                    {
                        ResumenGRAL1.Visible = true;
                        ResumenGRAL2.Visible = true;
                        ResumenGRAL3.Visible = true;

                        Resumen1.Visible = true;
                        Resumen1ENC.Visible = true;
                        Resumen1ENC.Text = TbQ1ENC.Text;
                        audinc.Visible = true;
                    }
                    if (Q2_NOKOPT.Checked)
                    {
                        ResumenGRAL1.Visible = true;
                        ResumenGRAL2.Visible = true;
                        ResumenGRAL3.Visible = true;
                        Resumen2.Visible = true;
                        Resumen2ENC.Visible = true;
                        Resumen2ENC.Text = TbQ2ENC.Text;
                        audinc.Visible = true;
                    }
                    if (Q3_NOKOPT.Checked)
                    {
                        ResumenGRAL1.Visible = true;
                        ResumenGRAL2.Visible = true;
                        ResumenGRAL3.Visible = true;
                        Resumen3.Visible = true;
                        Resumen3ENC.Visible = true;
                        Resumen3ENC.Text = TbQ3ENC.Text;
                        audinc.Visible = true;
                    }
                    if (Q4_NOKOPT.Checked)
                    {
                        ResumenGRAL1.Visible = true;
                        ResumenGRAL2.Visible = true;
                        ResumenGRAL3.Visible = true;
                        Resumen4.Visible = true;
                        Resumen4ENC.Visible = true;
                        Resumen4ENC.Text = TbQ4ENC.Text;
                        audinc.Visible = true;
                    }
                    if (Q5_NOKOPT.Checked)
                    {
                        ResumenGRAL1.Visible = true;
                        ResumenGRAL2.Visible = true;
                        ResumenGRAL3.Visible = true;
                        Resumen5.Visible = true;
                        Resumen5ENC.Visible = true;
                        Resumen5ENC.Text = TbQ5ENC.Text;
                        audinc.Visible = true;
                    }
                    if (Q6_NOKOPT.Checked || Q6C_NOKOPT.Checked)
                    {
                        ResumenGRAL1.Visible = true;
                        ResumenGRAL2.Visible = true;
                        ResumenGRAL3.Visible = true;
                        Resumen6.Visible = true;
                        Resumen6ENC.Visible = true;
                        Resumen6CAL.Visible = true;
                        Resumen6ENC.Text = TbQ6ENC.Text;
                        Resumen6CAL.Text = TbQ6CAL.Text;
                        audinc.Visible = true;
                    }

                    if (Q7_NOKOPT.Checked || Q7C_NOKOPT.Checked)
                    {
                        ResumenGRAL1.Visible = true;
                        ResumenGRAL2.Visible = true;
                        ResumenGRAL3.Visible = true;
                        Resumen7.Visible = true;
                        Resumen7ENC.Visible = true;
                        Resumen7CAL.Visible = true;
                        Resumen7ENC.Text = TbQ7ENC.Text;
                        Resumen7CAL.Text = TbQ7CAL.Text;
                        audinc.Visible = true;
                    }

                    if (Q8_NOKOPT.Checked || Q8C_NOKOPT.Checked)
                    {
                        ResumenGRAL1.Visible = true;
                        ResumenGRAL2.Visible = true;
                        ResumenGRAL3.Visible = true;
                        Resumen8.Visible = true;
                        Resumen8ENC.Visible = true;
                        Resumen8CAL.Visible = true;
                        Resumen8ENC.Text = TbQ8ENC.Text;
                        Resumen8CAL.Text = TbQ8CAL.Text;
                        audinc.Visible = true;
                    }

                    if (Q9_NOKOPT.Checked || Q9C_NOKOPT.Checked)
                    {
                        ResumenGRAL1.Visible = true;
                        ResumenGRAL2.Visible = true;
                        ResumenGRAL3.Visible = true;
                        Resumen9.Visible = true;
                        Resumen9ENC.Visible = true;
                        Resumen9CAL.Visible = true;
                        Resumen9ENC.Text = TbQ9ENC.Text;
                        Resumen9CAL.Text = TbQ9CAL.Text;
                        audinc.Visible = true;
                    }

                    if (Q10C_NOKOPT.Checked)
                    {
                        ResumenGRAL1.Visible = true;
                        ResumenGRAL2.Visible = true;
                        ResumenGRAL3.Visible = true;
                        Resumen10.Visible = true;
                        Resumen10ENC.Visible = true;
                        Resumen10CAL.Visible = true;
                        Resumen10CAL.Text = TbQ10CAL.Text;
                        audinc.Visible = true;
                    }

                    if (Convert.ToInt32(LiberacionCondicionada.SelectedValue) == 4 || ResumenPROD1.Visible || ResumenPROD3.Visible)
                    {
                        lblValidadPORING.Visible = true;
                        DropValidadJefeProyecto.Visible = true;
                        btnValidarING.Visible = true;
                        Conexion_LIBERACIONES conexion = new Conexion_LIBERACIONES();
                        if (conexion.Existe_Desviacion_Sin_Notificar(tbOrden.Text.Trim()))
                        {
                            string producto = tbReferencia.Text.Trim() + " " + tbNombre.Text.Trim();
                            string parametros = "";
                            if (ResumenPROD1.Visible)
                            { 
                                parametros = "<br> <strong>-</strong> Parámetros fuera de especificación.";
                            }
                            string retenido = "";
                            if (Convert.ToInt32(LiberacionCondicionada.SelectedValue) == 4)
                            { 
                                retenido = "<br> <strong>-</strong> Producción retenida por liberación NoK.";
                            }
                            mandar_mail(tbOrden.Text.Trim(), producto, parametros, retenido);
                            conexion.Marca_Mail_Enviado_Alerta_Desviacion(tbOrden.Text);
                        }                       
                    }   
                }
            }
            catch (Exception)
            { }
        }

        public void ValidarIngenieria(Object sender, EventArgs e)
        {
            Conexion_SMARTH SHConexion = new Conexion_SMARTH();
            Conexion_LIBERACIONES conexion = new Conexion_LIBERACIONES();

            conexion.Validar_Ingenieria(tbOrden.Text.Trim(), SHConexion.Devuelve_ID_Piloto_SMARTH(DropValidadJefeProyecto.SelectedValue.ToString()));

        }
        public void LiberarCambiador(Object sender, EventArgs e)
        {
            try {
                string QUERYLiberarCambiadorHoraORI = "";
                    if (LiberacionCambiadorHoraORI.Text == "")
                    {
                        QUERYLiberarCambiadorHoraORI = ", ORIFechaCambiadorLiberado = '" + DateTime.Now.ToString("dd/MM/yyy HH:mm") + "'";
                    }
                LiberacionCambiadorHoraORI.Text = DateTime.Now.ToString("dd/MM/yyy HH:mm");
                LiberacionCambiadorHora.Text = DateTime.Now.ToString("dd/MM/yyy HH:mm");
                if (CambiadorNombre.Text == "")
                    {
                    ReCargarCambiador(null, null);
                    }
                LiberarCambiadorFunction(QUERYLiberarCambiadorHoraORI);
                CargarFichaLiberacion();
                CargarParametrosLiberacion();
                CargarCuestionariosLiberacion();
                CargarMateriasPrimasLiberacion();
                CargarResultados();
                lkb_Sort_Click("CAMBIO");
            }
            catch (Exception)
            {
            }
        }
        public void LiberarEncargado(Object sender, EventArgs e)
        {
            try
            {
                if (Q4_NAOPT.Checked == true && Q5_NAOPT.Checked == true && Q6_NAOPT.Checked == true && LiberacionEncargadoHoraORI.Text == "")
                {
                    lkb_Sort_Click("PARAMETROS");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "liberar_encargado_NOK();", true);
                    
                }
                else
                {
                string QUERYLiberarEncargadoHoraORI = "";
                    if (LiberacionEncargadoHoraORI.Text == "")
                        {
                            QUERYLiberarEncargadoHoraORI = ", ORIFechaProduccionLiberado = '" + DateTime.Now.ToString("dd/MM/yyy HH:mm") + "'";
                        }
                LiberacionEncargadoHoraORI.Text = DateTime.Now.ToString("dd/MM/yyy HH:mm");
                LiberacionEncargadoHora.Text = DateTime.Now.ToString("dd/MM/yyy HH:mm");
                    if (EncargadoNombre.Text == "")
                    {
                        ReCargarEncargado(null, null);
                    }
                    if (Operario1Nombre.Text == "")
                    {
                        ReCargarOperarios(null, null);
                    }
                    RellenarVacíos();
                EvaluaParam();
                LiberarFunction("", QUERYLiberarEncargadoHoraORI, "");
                CargarFichaLiberacion();
                CargarParametrosLiberacion();
                CargarCuestionariosLiberacion();
                CargarMateriasPrimasLiberacion();
                PintarCeldasDevacion();
                CargarResultados();
                lkb_Sort_Click("PROCESO");
                }


            }
            catch (Exception) { }
        }
        public void LiberarAUDCalidad(Object sender, EventArgs e)
        {
            try
            {
                string QUERYLiberacionCalidadHoraORI = "";
                    if (LiberacionCalidadHoraORI.Text == "")
                    {
                        QUERYLiberacionCalidadHoraORI = ", ORIFechaCalidadLiberado = '" + DateTime.Now.ToString("dd/MM/yyy HH:mm") + "'";
                    }
                LiberacionCalidadHoraORI.Text = DateTime.Now.ToString("dd/MM/yyy HH:mm");
                LiberacionCalidadHora.Text = DateTime.Now.ToString("dd/MM/yyy HH:mm");
                if (EncargadoNombre.Text == "")
                {
                    ReCargarEncargado(null, null);
                }
                if (Operario1Nombre.Text == "")
                {
                    ReCargarOperarios(null, null);
                }
                if (CalidadNombre.Text == "")
                {
                    ReCargarCalidad(null, null);
                }

                EvaluaParam();
                //LiberarFunction("", "",QUERYLiberacionCalidadHoraORI);
                LiberarCalidadFunction(QUERYLiberacionCalidadHoraORI);
                CargarFichaLiberacion();
                CargarParametrosLiberacion();
                CargarCuestionariosLiberacion();
                CargarMateriasPrimasLiberacion();
                PintarCeldasDevacion();
                CargarResultados();
                lkb_Sort_Click("CALIDAD");
            }
            catch (Exception) { }
        } 
        public void LiberarParametros(Object sender, EventArgs e)
            {
            try
            {
                //RELLENAR CAMPOS VACÍOS
                try
                {
                    //RELLENO TEMPERATURAS DE CILINDRO
                    if (thBoqREAL.Text == "")
                    { thBoqREAL.Text = thBoq.Text; }
                    if (thT1REAL.Text == "")
                    { thT1REAL.Text = thT1.Text; }
                    if (thT2REAL.Text == "")
                    { thT2REAL.Text = thT2.Text; }
                    if (thT3REAL.Text == "")
                    { thT3REAL.Text = thT3.Text; }
                    if (thT4REAL.Text == "")
                    { thT4REAL.Text = thT4.Text; }
                    if (thT5REAL.Text == "")
                    { thT5REAL.Text = thT5.Text; }
                    if (thT6REAL.Text == "")
                    { thT6REAL.Text = thT6.Text; }
                    if (thT7REAL.Text == "")
                    { thT7REAL.Text = thT7.Text; }
                    if (thT8REAL.Text == "")
                    { thT8REAL.Text = thT8.Text; }
                    if (thT9REAL.Text == "")
                    { thT9REAL.Text = thT9.Text; }
                    if (thT10REAL.Text == "")
                    { thT10REAL.Text = thT10.Text; }

                    //RELLENO CAMARA CALIENTE
                    if (thZ1REAL.Text == "")
                    { thZ1REAL.Text = thZ1.Text; }
                    if (thZ2REAL.Text == "")
                    { thZ2REAL.Text = thZ2.Text; }
                    if (thZ3REAL.Text == "")
                    { thZ3REAL.Text = thZ3.Text; }
                    if (thZ4REAL.Text == "")
                    { thZ4REAL.Text = thZ4.Text; }
                    if (thZ5REAL.Text == "")
                    { thZ5REAL.Text = thZ5.Text; }
                    if (thZ6REAL.Text == "")
                    { thZ6REAL.Text = thZ6.Text; }
                    if (thZ7REAL.Text == "")
                    { thZ7REAL.Text = thZ7.Text; }
                    if (thZ8REAL.Text == "")
                    { thZ8REAL.Text = thZ8.Text; }
                    if (thZ9REAL.Text == "")
                    { thZ9REAL.Text = thZ9.Text; }
                    if (thZ10REAL.Text == "")
                    { thZ10REAL.Text = thZ10.Text; }
                    if (thZ11REAL.Text == "")
                    { thZ11REAL.Text = thZ11.Text; }
                    if (thZ12REAL.Text == "")
                    { thZ12REAL.Text = thZ12.Text; }
                    if (thZ13REAL.Text == "")
                    { thZ13REAL.Text = thZ13.Text; }
                    if (thZ14REAL.Text == "")
                    { thZ14REAL.Text = thZ14.Text; }
                    if (thZ15REAL.Text == "")
                    { thZ15REAL.Text = thZ15.Text; }
                    if (thZ16REAL.Text == "")
                    { thZ16REAL.Text = thZ16.Text; }
                    if (thZ17REAL.Text == "")
                    { thZ17REAL.Text = thZ17.Text; }
                    if (thZ18REAL.Text == "")
                    { thZ18REAL.Text = thZ18.Text; }
                    if (thZ19REAL.Text == "")
                    { thZ19REAL.Text = thZ19.Text; }
                    if (thZ20REAL.Text == "")
                    { thZ20REAL.Text = thZ20.Text; }

                    //RELLENO POSTPRESION
                    if (thP1R.Text == "")
                    { thP1R.Text = thP1.Text; }
                    if (thP2R.Text == "")
                    { thP2R.Text = thP2.Text; }
                    if (thP3R.Text == "")
                    { thP3R.Text = thP3.Text; }
                    if (thP4R.Text == "")
                    { thP4R.Text = thP4.Text; }
                    if (thP5R.Text == "")
                    { thP5R.Text = thP5.Text; }
                    if (thP6R.Text == "")
                    { thP6R.Text = thP6.Text; }
                    if (thP7R.Text == "")
                    { thP7R.Text = thP7.Text; }
                    if (thP8R.Text == "")
                    { thP8R.Text = thP8.Text; }
                    if (thP9R.Text == "")
                    { thP9R.Text = thP9.Text; }
                    if (thP10R.Text == "")
                    { thP10R.Text = thP10.Text; }
                    if (thTP1R.Text == "")
                    { thTP1R.Text = thTP1.Text; }
                    if (thTP2R.Text == "")
                    { thTP2R.Text = thTP2.Text; }
                    if (thTP3R.Text == "")
                    { thTP3R.Text = thTP3.Text; }
                    if (thTP4R.Text == "")
                    { thTP4R.Text = thTP4.Text; }
                    if (thTP5R.Text == "")
                    { thTP5R.Text = thTP5.Text; }
                    if (thTP6R.Text == "")
                    { thTP6R.Text = thTP6.Text; }
                    if (thTP7R.Text == "")
                    { thTP7R.Text = thTP7.Text; }
                    if (thTP8R.Text == "")
                    { thTP8R.Text = thTP8.Text; }
                    if (thTP9R.Text == "")
                    { thTP9R.Text = thTP9.Text; }
                    if (thTP10R.Text == "")
                    { thTP10R.Text = thTP10.Text; }

                    //RELLENO ATEMPERADO
                    if (TbCaudalF1REAL.Text == "")
                    { TbCaudalF1REAL.Text = TbCaudalF1.Text; }
                    if (TbCaudalF2REAL.Text == "")
                    { TbCaudalF2REAL.Text = TbCaudalF2.Text; }
                    if (TbCaudalF3REAL.Text == "")
                    { TbCaudalF3REAL.Text = TbCaudalF3.Text; }
                    if (TbCaudalF4REAL.Text == "")
                    { TbCaudalF4REAL.Text = TbCaudalF4.Text; }
                    if (TbCaudalF5REAL.Text == "")
                    { TbCaudalF5REAL.Text = TbCaudalF5.Text; }
                    if (TbCaudalF6REAL.Text == "")
                    { TbCaudalF6REAL.Text = TbCaudalF6.Text; }

                    if (TbCaudalM1REAL.Text == "")
                    { TbCaudalM1REAL.Text = TbCaudalM1.Text; }
                    if (TbCaudalM2REAL.Text == "")
                    { TbCaudalM2REAL.Text = TbCaudalM2.Text; }
                    if (TbCaudalM3REAL.Text == "")
                    { TbCaudalM3REAL.Text = TbCaudalM3.Text; }
                    if (TbCaudalM4REAL.Text == "")
                    { TbCaudalM4REAL.Text = TbCaudalM4.Text; }
                    if (TbCaudalM5REAL.Text == "")
                    { TbCaudalM5REAL.Text = TbCaudalM5.Text; }
                    if (TbCaudalM6REAL.Text == "")
                    { TbCaudalM6REAL.Text = TbCaudalM6.Text; }

                    if (TbTemperaturaF1REAL.Text == "")
                    { TbTemperaturaF1REAL.Text = TbTemperaturaF1.Text; }
                    if (TbTemperaturaF2REAL.Text == "")
                    { TbTemperaturaF2REAL.Text = TbTemperaturaF2.Text; }
                    if (TbTemperaturaF3REAL.Text == "")
                    { TbTemperaturaF3REAL.Text = TbTemperaturaF3.Text; }
                    if (TbTemperaturaF4REAL.Text == "")
                    { TbTemperaturaF4REAL.Text = TbTemperaturaF4.Text; }
                    if (TbTemperaturaF5REAL.Text == "")
                    { TbTemperaturaF5REAL.Text = TbTemperaturaF5.Text; }
                    if (TbTemperaturaF6REAL.Text == "")
                    { TbTemperaturaF6REAL.Text = TbTemperaturaF6.Text; }

                    if (TbTemperaturaM1REAL.Text == "")
                    { TbTemperaturaM1REAL.Text = TbTemperaturaM1.Text; }
                    if (TbTemperaturaM2REAL.Text == "")
                    { TbTemperaturaM2REAL.Text = TbTemperaturaM2.Text; }
                    if (TbTemperaturaM3REAL.Text == "")
                    { TbTemperaturaM3REAL.Text = TbTemperaturaM3.Text; }
                    if (TbTemperaturaM4REAL.Text == "")
                    { TbTemperaturaM4REAL.Text = TbTemperaturaM4.Text; }
                    if (TbTemperaturaM5REAL.Text == "")
                    { TbTemperaturaM5REAL.Text = TbTemperaturaM5.Text; }
                    if (TbTemperaturaM6REAL.Text == "")
                    { TbTemperaturaM6REAL.Text = TbTemperaturaM6.Text; }

                    //LIMITES
                    if (tbConmutacionREAL.Text == "")
                    { tbConmutacionREAL.Text = tbConmutacion.Text; }
                    if (tbTiempoPresionREAL.Text == "")
                    { tbTiempoPresionREAL.Text = tbTiempoPresion.Text; }
                    if (tbTiempoInyeccionREAL.Text == "")
                    { tbTiempoInyeccionREAL.Text = tbTiempoInyeccion.Text; }
                    if (tbLimitePresionREAL.Text == "")
                    { tbLimitePresionREAL.Text = tbLimitePresion.Text; }
                    if (thVCargaREAL.Text == "")
                    { thVCargaREAL.Text = thVCarga.Text; }
                    if (thCargaREAL.Text == "")
                    { thCargaREAL.Text = thCarga.Text; }
                    if (thDescompREAL.Text == "")
                    { thDescompREAL.Text = thDescomp.Text; }
                    if (thContraprREAL.Text == "")
                    { thContraprREAL.Text = thContrapr.Text; }
                    if (thTiempoREAL.Text == "")
                    { thTiempoREAL.Text = thTiempo.Text; }
                    if (thEnfriamientoREAL.Text == "")
                    { thEnfriamientoREAL.Text = thEnfriamiento.Text; }
                    if (thCicloREAL.Text == "")
                    { thCicloREAL.Text = thCiclo.Text; }
                    if (thCojinREAL.Text == "")
                    { thCojinREAL.Text = thCojin.Text; }
                }
                catch (Exception) { }
                EvaluaParam();
                LiberarFunction("","","");
                CargarFichaLiberacion();
                CargarParametrosLiberacion();
                CargarCuestionariosLiberacion();
                CargarMateriasPrimasLiberacion();
                PintarCeldasDevacion();
                CargarResultados();
                lkb_Sort_Click("PARAMETROS");
            }
            catch (Exception)
            {}

            }
        public void LiberarFunction(string QUERYLiberarCambiadorHoraORI, string QUERYLiberarEncargadoHoraORI, string QUERYLiberacionCalidadHoraORI)
        {
            try {
                //FICHA DE LIBERACION
                int version = 0;

                int operario1numero = 0;
                if (Operario1Numero.Text != "")
                { operario1numero = Convert.ToInt32(Operario1Numero.Text); }

                double operario1horas = 0;
                if (Operario1Horas.Text != "")
                { operario1horas = Convert.ToDouble(Operario1Horas.Text); }

                int operario2numero = 0;
                if (Operario2Numero.Text != "")
                { operario2numero = Convert.ToInt32(Operario2Numero.Text); }
                double operario2horas = 0;
                if (Operario2Horas.Text != "")
                { operario2horas = Convert.ToDouble(Operario2Horas.Text); }

                int encargadonumero = 0;
                if (EncargadoNumero.Text != "")
                { encargadonumero = Convert.ToInt32(EncargadoNumero.Text); }
                double encargadohoras = 0;
                if (EncargadoHoras.Text != "")
                { encargadohoras = Convert.ToDouble(EncargadoHoras.Text); }

                int cambiadornumero = 0;
                if (CambiadorNumero.Text != "")
                { cambiadornumero = Convert.ToInt32(CambiadorNumero.Text); }
                double cambiadorhoras = 0;
                if (CambiadorHoras.Text != "")
                { cambiadorhoras = Convert.ToDouble(CambiadorHoras.Text); }

                int calidadnumero = 0;
                if (CalidadNumero.Text != "")
                { calidadnumero = Convert.ToInt32(CalidadNumero.Text); }

                double calidadhoras = 0;
                if (CalidadHoras.Text != "")
                { calidadhoras = Convert.ToDouble(CalidadHoras.Text); }

                alertaoperario.Visible = false;
                    if (Operario1Nivel.SelectedValue.ToString() == "I")
                    {
                        alertaoperario.Visible = true;
                    }
                    if (Operario2Numero.Text != "0" && Operario2Nivel.SelectedValue.ToString() == "I")
                    {
                        alertaoperario.Visible = true;
                    }

                int CambiadorLiberado = 0;
                    
                    if (LiberacionCambiadorHoraORI.Text != "" && (Q1_NOKOPT.Checked == true || Q2_NOKOPT.Checked == true || Q3_NOKOPT.Checked == true || (MAT1REF.Visible == true && MAT1LOT.Text == "") || (MAT2REF.Visible == true && MAT2LOT.Text == "") || (MAT3REF.Visible == true && MAT3LOT.Text == "") || (COMP1REF.Visible == true && COMP1LOT.Text == "") || (COMP2REF.Visible == true && COMP2LOT.Text == "") || (COMP3REF.Visible == true && COMP3LOT.Text == "") || (COMP4REF.Visible == true && COMP4LOT.Text == "") || (COMP5REF.Visible == true && COMP5LOT.Text == "") || (COMP6REF.Visible == true && COMP6LOT.Text == "") || (COMP7REF.Visible == true && COMP7LOT.Text == "")))
                        {
                        CambiadorLiberado = 1;
                        }
                    else if (LiberacionCambiadorHoraORI.Text != "" && (Q1_NOKOPT.Checked == false && Q2_NOKOPT.Checked == false && Q3_NOKOPT.Checked == false))
                        {
                        CambiadorLiberado = 2;
                        }
                int ProduccionLiberado = 0;
                    if (LiberacionEncargadoHoraORI.Text != "" && (Q4_NOKOPT.Checked == true || Q5_NOKOPT.Checked == true || Q6_NOKOPT.Checked == true || Q7_NOKOPT.Checked == true || Q8_NOKOPT.Checked == true || Q9_NOKOPT.Checked == true || A3.Visible == true || A5.Visible == true || PARAMSESTADO.Text != "" || (alertaoperario.Visible == true) || (Convert.ToInt32(EXISTEFICHA.Text) == 1)))
                    {
                        ProduccionLiberado = 1;
                    }
                    else if (LiberacionEncargadoHoraORI.Text != "" && (Q4_NOKOPT.Checked == false && Q5_NOKOPT.Checked == false && Q6_NOKOPT.Checked == false && Q7_NOKOPT.Checked == false && Q8_NOKOPT.Checked == false && Q9_NOKOPT.Checked == false ))
                    { 
                        ProduccionLiberado = 2;
                    }
                int CalidadLiberado = 0;
                    if (LiberacionCalidadHoraORI.Text != "" && (Q10C_NOKOPT.Checked == true || Q6C_NOKOPT.Checked == true || Q7C_NOKOPT.Checked == true || Q8C_NOKOPT.Checked == true || Q9C_NOKOPT.Checked == true || A7.Visible == true || A7.Visible == true || (alertaoperario.Visible == true) || (Convert.ToInt32(EXISTEFICHA.Text) == 1)))
                    {
                        CalidadLiberado = 1;
                    }
                    else if (LiberacionCalidadHoraORI.Text != "" && (Q10C_NOKOPT.Checked == false && Q6C_NOKOPT.Checked == false && Q7C_NOKOPT.Checked == false && Q8C_NOKOPT.Checked == false && Q9C_NOKOPT.Checked == false))
                    {
                        CalidadLiberado = 2;
                    }

                int ResultadoLiberacion = 0;
                int Reliberacion = 0;


                int NCEncargado = 0;
                    if (A3OK.Visible == true)
                        { NCEncargado = 1; }
                int GP12Encargado = 0;
                    if (A5OK.Visible == true)
                        { GP12Encargado = 1; }
                int NCCalidad = 0;
                    if (A7OK.Visible == true)
                        { NCCalidad = 1; }
                int GP12Calidad = 0;
                    if (A8OK.Visible == true)
                        { GP12Calidad = 1; }

                //VALORES DE POSTPRESION
                double thP1_double = 0;
                double thP2_double = 0;
                double thP3_double = 0;
                double thP4_double = 0;
                double thP5_double = 0;
                double thP6_double = 0;
                double thP7_double = 0;
                double thP8_double = 0;
                double thP9_double = 0;
                double thP10_double = 0;
                double thTP1_double = 0;
                double thTP2_double = 0;
                double thTP3_double = 0;
                double thTP4_double = 0;
                double thTP5_double = 0;
                double thTP6_double = 0;
                double thTP7_double = 0;
                double thTP8_double = 0;
                double thTP9_double = 0;
                double thTP10_double = 0;
                if (Double.TryParse((thP1.Text).Replace('.', ','), out thP1_double))
                    thP1_double = Convert.ToDouble(thP1.Text.Replace('.', ','));
                else
                    thP1_double = 0.0;
                if (Double.TryParse((thP2.Text).Replace('.', ','), out thP2_double))
                    thP2_double = Convert.ToDouble(thP2.Text.Replace('.', ','));
                else
                    thP2_double = 0.0;
                if (Double.TryParse(thP3.Text.Replace('.', ','), out thP3_double))
                    thP3_double = Convert.ToDouble(thP3.Text.Replace('.', ','));
                else
                    thP3_double = 0.0;
                if (Double.TryParse(thP4.Text.Replace('.', ','), out thP4_double))
                    thP4_double = Convert.ToDouble(thP4.Text.Replace('.', ','));
                else
                    thP4_double = 0.0;
                if (Double.TryParse(thP5.Text.Replace('.', ','), out thP5_double))
                    thP5_double = Convert.ToDouble(thP5.Text.Replace('.', ','));
                else
                    thP5_double = 0.0;
                if (Double.TryParse(thP6.Text.Replace('.', ','), out thP6_double))
                    thP6_double = Convert.ToDouble(thP6.Text.Replace('.', ','));
                else
                    thP6_double = 0.0;
                if (Double.TryParse(thP7.Text.Replace('.', ','), out thP7_double))
                    thP7_double = Convert.ToDouble(thP7.Text.Replace('.', ','));
                else
                    thP7_double = 0.0;
                if (Double.TryParse(thP8.Text.Replace('.', ','), out thP8_double))
                    thP8_double = Convert.ToDouble(thP8.Text.Replace('.', ','));
                else
                    thP8_double = 0.0;
                if (Double.TryParse(thP9.Text.Replace('.', ','), out thP9_double))
                    thP9_double = Convert.ToDouble(thP9.Text.Replace('.', ','));
                else
                    thP9_double = 0.0;
                if (Double.TryParse(thP10.Text.Replace('.', ','), out thP10_double))
                    thP10_double = Convert.ToDouble(thP10.Text.Replace('.', ','));
                else
                    thP10_double = 0.0;
                if (Double.TryParse(thTP1.Text.Replace('.', ','), out thTP1_double))
                    thTP1_double = Convert.ToDouble(thTP1.Text.Replace('.', ','));
                else
                    thTP1_double = 0.0;
                if (Double.TryParse(thTP2.Text.Replace('.', ','), out thTP2_double))
                    thTP2_double = Convert.ToDouble(thTP2.Text.Replace('.', ','));
                else
                    thTP2_double = 0.0;
                if (Double.TryParse(thTP3.Text.Replace('.', ','), out thTP3_double))
                    thTP3_double = Convert.ToDouble(thTP3.Text.Replace('.', ','));
                else
                    thTP3_double = 0.0;
                if (Double.TryParse(thTP4.Text.Replace('.', ','), out thTP4_double))
                    thTP4_double = Convert.ToDouble(thTP4.Text.Replace('.', ','));
                else
                    thTP4_double = 0.0;
                if (Double.TryParse(thTP5.Text.Replace('.', ','), out thTP5_double))
                    thTP5_double = Convert.ToDouble(thTP5.Text.Replace('.', ','));
                else
                    thTP5_double = 0.0;
                if (Double.TryParse(thTP6.Text.Replace('.', ','), out thTP6_double))
                    thTP6_double = Convert.ToDouble(thTP6.Text.Replace('.', ','));
                else
                    thTP6_double = 0.0;
                if (Double.TryParse(thTP7.Text.Replace('.', ','), out thTP7_double))
                    thTP7_double = Convert.ToDouble(thTP7.Text.Replace('.', ','));
                else
                    thTP7_double = 0.0;
                if (Double.TryParse(thTP8.Text.Replace('.', ','), out thTP8_double))
                    thTP8_double = Convert.ToDouble(thTP8.Text.Replace('.', ','));
                else
                    thTP8_double = 0.0;
                if (Double.TryParse(thTP9.Text.Replace('.', ','), out thTP9_double))
                    thTP9_double = Convert.ToDouble(thTP9.Text.Replace('.', ','));
                else
                    thTP9_double = 0.0;
                if (Double.TryParse(thTP10.Text.Replace('.', ','), out thTP10_double))
                    thTP10_double = Convert.ToDouble(thTP10.Text.Replace('.', ','));
                else
                    thTP10_double = 0.0;

                //VALORES DE POSTPRESION REALES
                double thP1_REAL_double = 0;
                double thP2_REAL_double = 0;
                double thP3_REAL_double = 0;
                double thP4_REAL_double = 0;
                double thP5_REAL_double = 0;
                double thP6_REAL_double = 0;
                double thP7_REAL_double = 0;
                double thP8_REAL_double = 0;
                double thP9_REAL_double = 0;
                double thP10_REAL_double = 0;
                double thTP1_REAL_double = 0;
                double thTP2_REAL_double = 0;
                double thTP3_REAL_double = 0;
                double thTP4_REAL_double = 0;
                double thTP5_REAL_double = 0;
                double thTP6_REAL_double = 0;
                double thTP7_REAL_double = 0;
                double thTP8_REAL_double = 0;
                double thTP9_REAL_double = 0;
                double thTP10_REAL_double = 0;
                if (Double.TryParse((thP1R.Text).Replace('.', ','), out thP1_REAL_double))
                    thP1_REAL_double = Convert.ToDouble(thP1R.Text.Replace('.', ','));
                else
                    thP1_REAL_double = 0.0;
                if (Double.TryParse((thP2R.Text).Replace('.', ','), out thP2_REAL_double))
                    thP2_REAL_double = Convert.ToDouble(thP2R.Text.Replace('.', ','));
                else
                    thP2_REAL_double = 0.0;
                if (Double.TryParse(thP3R.Text.Replace('.', ','), out thP3_REAL_double))
                    thP3_REAL_double = Convert.ToDouble(thP3R.Text.Replace('.', ','));
                else
                    thP3_REAL_double = 0.0;
                if (Double.TryParse(thP4R.Text.Replace('.', ','), out thP4_REAL_double))
                    thP4_REAL_double = Convert.ToDouble(thP4R.Text.Replace('.', ','));
                else
                    thP4_REAL_double = 0.0;
                if (Double.TryParse(thP5R.Text.Replace('.', ','), out thP5_REAL_double))
                    thP5_REAL_double = Convert.ToDouble(thP5R.Text.Replace('.', ','));
                else
                    thP5_REAL_double = 0.0;
                if (Double.TryParse(thP6R.Text.Replace('.', ','), out thP6_REAL_double))
                    thP6_REAL_double = Convert.ToDouble(thP6R.Text.Replace('.', ','));
                else
                    thP6_REAL_double = 0.0;
                if (Double.TryParse(thP7R.Text.Replace('.', ','), out thP7_REAL_double))
                    thP7_REAL_double = Convert.ToDouble(thP7R.Text.Replace('.', ','));
                else
                    thP7_REAL_double = 0.0;
                if (Double.TryParse(thP8R.Text.Replace('.', ','), out thP8_REAL_double))
                    thP8_REAL_double = Convert.ToDouble(thP8R.Text.Replace('.', ','));
                else
                    thP8_REAL_double = 0.0;
                if (Double.TryParse(thP9R.Text.Replace('.', ','), out thP9_REAL_double))
                    thP9_REAL_double = Convert.ToDouble(thP9R.Text.Replace('.', ','));
                else
                    thP9_REAL_double = 0.0;
                if (Double.TryParse(thP10R.Text.Replace('.', ','), out thP10_REAL_double))
                    thP10_REAL_double = Convert.ToDouble(thP10R.Text.Replace('.', ','));
                else
                    thP10_REAL_double = 0.0;
                if (Double.TryParse(thTP1R.Text.Replace('.', ','), out thTP1_REAL_double))
                    thTP1_REAL_double = Convert.ToDouble(thTP1R.Text.Replace('.', ','));
                else
                    thTP1_REAL_double = 0.0;
                if (Double.TryParse(thTP2R.Text.Replace('.', ','), out thTP2_REAL_double))
                    thTP2_REAL_double = Convert.ToDouble(thTP2R.Text.Replace('.', ','));
                else
                    thTP2_REAL_double = 0.0;
                if (Double.TryParse(thTP3R.Text.Replace('.', ','), out thTP3_REAL_double))
                    thTP3_REAL_double = Convert.ToDouble(thTP3R.Text.Replace('.', ','));
                else
                    thTP3_REAL_double = 0.0;
                if (Double.TryParse(thTP4R.Text.Replace('.', ','), out thTP4_REAL_double))
                    thTP4_REAL_double = Convert.ToDouble(thTP4R.Text.Replace('.', ','));
                else
                    thTP4_REAL_double = 0.0;
                if (Double.TryParse(thTP5R.Text.Replace('.', ','), out thTP5_REAL_double))
                    thTP5_REAL_double = Convert.ToDouble(thTP5R.Text.Replace('.', ','));
                else
                    thTP5_REAL_double = 0.0;
                if (Double.TryParse(thTP6R.Text.Replace('.', ','), out thTP6_REAL_double))
                    thTP6_REAL_double = Convert.ToDouble(thTP6R.Text.Replace('.', ','));
                else
                    thTP6_REAL_double = 0.0;
                if (Double.TryParse(thTP7R.Text.Replace('.', ','), out thTP7_REAL_double))
                    thTP7_REAL_double = Convert.ToDouble(thTP7R.Text.Replace('.', ','));
                else
                    thTP7_REAL_double = 0.0;
                if (Double.TryParse(thTP8R.Text.Replace('.', ','), out thTP8_REAL_double))
                    thTP8_REAL_double = Convert.ToDouble(thTP8R.Text.Replace('.', ','));
                else
                    thTP8_REAL_double = 0.0;
                if (Double.TryParse(thTP9R.Text.Replace('.', ','), out thTP9_REAL_double))
                    thTP9_REAL_double = Convert.ToDouble(thTP9R.Text.Replace('.', ','));
                else
                    thTP9_REAL_double = 0.0;
                if (Double.TryParse(thTP10R.Text.Replace('.', ','), out thTP10_REAL_double))
                    thTP10_REAL_double = Convert.ToDouble(thTP10R.Text.Replace('.', ','));
                else
                    thTP10_REAL_double = 0.0;

                //VALORES CAMARA CALIENTE
                double thZ1_double = 0;
                double thZ2_double = 0;
                double thZ3_double = 0;
                double thZ4_double = 0;
                double thZ5_double = 0;
                double thZ6_double = 0;
                double thZ7_double = 0;
                double thZ8_double = 0;
                double thZ9_double = 0;
                double thZ10_double = 0;
                double thZ11_double = 0;
                double thZ12_double = 0;
                double thZ13_double = 0;
                double thZ14_double = 0;
                double thZ15_double = 0;
                double thZ16_double = 0;
                double thZ17_double = 0;
                double thZ18_double = 0;
                double thZ19_double = 0;
                double thZ20_double = 0;

                if (Double.TryParse(thZ1.Text.Replace('.', ','), out thZ1_double))
                    thZ1_double = Convert.ToDouble(thZ1.Text.Replace('.', ','));
                else
                    thZ1_double = 0.0;
                if (Double.TryParse(thZ2.Text.Replace('.', ','), out thZ2_double))
                    thZ2_double = Convert.ToDouble(thZ2.Text.Replace('.', ','));
                else
                    thZ2_double = 0.0;
                if (Double.TryParse(thZ3.Text.Replace('.', ','), out thZ3_double))
                    thZ3_double = Convert.ToDouble(thZ3.Text.Replace('.', ','));
                else
                    thZ3_double = 0.0;
                if (Double.TryParse(thZ4.Text.Replace('.', ','), out thZ4_double))
                    thZ4_double = Convert.ToDouble(thZ4.Text.Replace('.', ','));
                else
                    thZ4_double = 0.0;
                if (Double.TryParse(thZ5.Text.Replace('.', ','), out thZ5_double))
                    thZ5_double = Convert.ToDouble(thZ5.Text.Replace('.', ','));
                else
                    thZ5_double = 0.0;
                if (Double.TryParse(thZ6.Text.Replace('.', ','), out thZ6_double))
                    thZ6_double = Convert.ToDouble(thZ6.Text.Replace('.', ','));
                else
                    thZ6_double = 0.0;
                if (Double.TryParse(thZ7.Text.Replace('.', ','), out thZ7_double))
                    thZ7_double = Convert.ToDouble(thZ7.Text.Replace('.', ','));
                else
                    thZ7_double = 0.0;
                if (Double.TryParse(thZ8.Text.Replace('.', ','), out thZ8_double))
                    thZ8_double = Convert.ToDouble(thZ8.Text.Replace('.', ','));
                else
                    thZ8_double = 0.0;
                if (Double.TryParse(thZ9.Text.Replace('.', ','), out thZ9_double))
                    thZ9_double = Convert.ToDouble(thZ9.Text.Replace('.', ','));
                else
                    thZ9_double = 0.0;
                if (Double.TryParse(thZ10.Text.Replace('.', ','), out thZ10_double))
                    thZ10_double = Convert.ToDouble(thZ10.Text.Replace('.', ','));
                else
                    thZ10_double = 0.0;
                if (Double.TryParse(thZ11.Text.Replace('.', ','), out thZ11_double))
                    thZ11_double = Convert.ToDouble(thZ11.Text.Replace('.', ','));
                else
                    thZ11_double = 0.0;
                if (Double.TryParse(thZ12.Text.Replace('.', ','), out thZ12_double))
                    thZ12_double = Convert.ToDouble(thZ12.Text.Replace('.', ','));
                else
                    thZ12_double = 0.0;
                if (Double.TryParse(thZ13.Text.Replace('.', ','), out thZ13_double))
                    thZ13_double = Convert.ToDouble(thZ13.Text.Replace('.', ','));
                else
                    thZ13_double = 0.0;
                if (Double.TryParse(thZ14.Text.Replace('.', ','), out thZ14_double))
                    thZ14_double = Convert.ToDouble(thZ14.Text.Replace('.', ','));
                else
                    thZ14_double = 0.0;
                if (Double.TryParse(thZ15.Text.Replace('.', ','), out thZ15_double))
                    thZ15_double = Convert.ToDouble(thZ15.Text.Replace('.', ','));
                else
                    thZ15_double = 0.0;
                if (Double.TryParse(thZ16.Text.Replace('.', ','), out thZ16_double))
                    thZ16_double = Convert.ToDouble(thZ16.Text.Replace('.', ','));
                else
                    thZ16_double = 0.0;
                if (Double.TryParse(thZ17.Text.Replace('.', ','), out thZ17_double))
                    thZ17_double = Convert.ToDouble(thZ17.Text.Replace('.', ','));
                else
                    thZ17_double = 0.0;
                if (Double.TryParse(thZ18.Text.Replace('.', ','), out thZ18_double))
                    thZ18_double = Convert.ToDouble(thZ18.Text.Replace('.', ','));
                else
                    thZ18_double = 0.0;
                if (Double.TryParse(thZ19.Text.Replace('.', ','), out thZ19_double))
                    thZ19_double = Convert.ToDouble(thZ19.Text.Replace('.', ','));
                else
                    thZ19_double = 0.0;
                if (Double.TryParse(thZ20.Text.Replace('.', ','), out thZ20_double))
                    thZ20_double = Convert.ToDouble(thZ20.Text.Replace('.', ','));
                else
                    thZ20_double = 0.0;

                //VALORES CAMARA CALIENTE REALES
                double thZ1_REAL_double = 0;
                double thZ2_REAL_double = 0;
                double thZ3_REAL_double = 0;
                double thZ4_REAL_double = 0;
                double thZ5_REAL_double = 0;
                double thZ6_REAL_double = 0;
                double thZ7_REAL_double = 0;
                double thZ8_REAL_double = 0;
                double thZ9_REAL_double = 0;
                double thZ10_REAL_double = 0;
                double thZ11_REAL_double = 0;
                double thZ12_REAL_double = 0;
                double thZ13_REAL_double = 0;
                double thZ14_REAL_double = 0;
                double thZ15_REAL_double = 0;
                double thZ16_REAL_double = 0;
                double thZ17_REAL_double = 0;
                double thZ18_REAL_double = 0;
                double thZ19_REAL_double = 0;
                double thZ20_REAL_double = 0;

                if (Double.TryParse(thZ1REAL.Text.Replace('.', ','), out thZ1_REAL_double))
                    thZ1_REAL_double = Convert.ToDouble(thZ1REAL.Text.Replace('.', ','));
                else
                    thZ1_REAL_double = 0.0;
                if (Double.TryParse(thZ2REAL.Text.Replace('.', ','), out thZ2_REAL_double))
                    thZ2_REAL_double = Convert.ToDouble(thZ2REAL.Text.Replace('.', ','));
                else
                    thZ2_REAL_double = 0.0;
                if (Double.TryParse(thZ3REAL.Text.Replace('.', ','), out thZ3_REAL_double))
                    thZ3_REAL_double = Convert.ToDouble(thZ3REAL.Text.Replace('.', ','));
                else
                    thZ3_REAL_double = 0.0;
                if (Double.TryParse(thZ4REAL.Text.Replace('.', ','), out thZ4_REAL_double))
                    thZ4_REAL_double = Convert.ToDouble(thZ4REAL.Text.Replace('.', ','));
                else
                    thZ4_REAL_double = 0.0;
                if (Double.TryParse(thZ5REAL.Text.Replace('.', ','), out thZ5_REAL_double))
                    thZ5_REAL_double = Convert.ToDouble(thZ5REAL.Text.Replace('.', ','));
                else
                    thZ5_REAL_double = 0.0;
                if (Double.TryParse(thZ6REAL.Text.Replace('.', ','), out thZ6_REAL_double))
                    thZ6_REAL_double = Convert.ToDouble(thZ6REAL.Text.Replace('.', ','));
                else
                    thZ6_REAL_double = 0.0;
                if (Double.TryParse(thZ7REAL.Text.Replace('.', ','), out thZ7_REAL_double))
                    thZ7_REAL_double = Convert.ToDouble(thZ7REAL.Text.Replace('.', ','));
                else
                    thZ7_REAL_double = 0.0;
                if (Double.TryParse(thZ8REAL.Text.Replace('.', ','), out thZ8_REAL_double))
                    thZ8_REAL_double = Convert.ToDouble(thZ8REAL.Text.Replace('.', ','));
                else
                    thZ8_REAL_double = 0.0;
                if (Double.TryParse(thZ9REAL.Text.Replace('.', ','), out thZ9_REAL_double))
                    thZ9_REAL_double = Convert.ToDouble(thZ9REAL.Text.Replace('.', ','));
                else
                    thZ9_REAL_double = 0.0;
                if (Double.TryParse(thZ10REAL.Text.Replace('.', ','), out thZ10_REAL_double))
                    thZ10_REAL_double = Convert.ToDouble(thZ10REAL.Text.Replace('.', ','));
                else
                    thZ10_REAL_double = 0.0;
                if (Double.TryParse(thZ11REAL.Text.Replace('.', ','), out thZ11_REAL_double))
                    thZ11_REAL_double = Convert.ToDouble(thZ11REAL.Text.Replace('.', ','));
                else
                    thZ11_REAL_double = 0.0;
                if (Double.TryParse(thZ12REAL.Text.Replace('.', ','), out thZ12_REAL_double))
                    thZ12_REAL_double = Convert.ToDouble(thZ12REAL.Text.Replace('.', ','));
                else
                    thZ12_REAL_double = 0.0;
                if (Double.TryParse(thZ13REAL.Text.Replace('.', ','), out thZ13_REAL_double))
                    thZ13_REAL_double = Convert.ToDouble(thZ13REAL.Text.Replace('.', ','));
                else
                    thZ13_REAL_double = 0.0;
                if (Double.TryParse(thZ14REAL.Text.Replace('.', ','), out thZ14_REAL_double))
                    thZ14_REAL_double = Convert.ToDouble(thZ14REAL.Text.Replace('.', ','));
                else
                    thZ14_REAL_double = 0.0;
                if (Double.TryParse(thZ15REAL.Text.Replace('.', ','), out thZ15_REAL_double))
                    thZ15_REAL_double = Convert.ToDouble(thZ15REAL.Text.Replace('.', ','));
                else
                    thZ15_REAL_double = 0.0;
                if (Double.TryParse(thZ16REAL.Text.Replace('.', ','), out thZ16_REAL_double))
                    thZ16_REAL_double = Convert.ToDouble(thZ16REAL.Text.Replace('.', ','));
                else
                    thZ16_double = 0.0;
                if (Double.TryParse(thZ17REAL.Text.Replace('.', ','), out thZ17_REAL_double))
                    thZ17_REAL_double = Convert.ToDouble(thZ17REAL.Text.Replace('.', ','));
                else
                    thZ17_REAL_double = 0.0;
                if (Double.TryParse(thZ18REAL.Text.Replace('.', ','), out thZ18_REAL_double))
                    thZ18_REAL_double = Convert.ToDouble(thZ18REAL.Text.Replace('.', ','));
                else
                    thZ18_REAL_double = 0.0;
                if (Double.TryParse(thZ19REAL.Text.Replace('.', ','), out thZ19_REAL_double))
                    thZ19_REAL_double = Convert.ToDouble(thZ19REAL.Text.Replace('.', ','));
                else
                    thZ19_REAL_double = 0.0;
                if (Double.TryParse(thZ20REAL.Text.Replace('.', ','), out thZ20_REAL_double))
                    thZ20_REAL_double = Convert.ToDouble(thZ20REAL.Text.Replace('.', ','));
                else
                    thZ20_REAL_double = 0.0;


                //VALORES TEMPERATURA CILINDRO
                double thBoq_double = 0.0;
                double thT1_double = 0.0;
                double thT2_double = 0.0;
                double thT3_double = 0.0;
                double thT4_double = 0.0;
                double thT5_double = 0.0;
                double thT6_double = 0.0;
                double thT7_double = 0.0;
                double thT8_double = 0.0;
                double thT9_double = 0.0;
                double thT10_double = 0.0;

                if (Double.TryParse(thBoq.Text.Replace('.', ','), out thBoq_double))
                    thBoq_double = Convert.ToDouble(thBoq.Text.Replace('.', ','));
                else
                    thBoq_double = 0.0;
                if (Double.TryParse(thT1.Text.Replace('.', ','), out thT1_double))
                    thT1_double = Convert.ToDouble(thT1.Text.Replace('.', ','));
                else
                    thT1_double = 0.0;
                if (Double.TryParse(thT2.Text.Replace('.', ','), out thT2_double))
                    thT2_double = Convert.ToDouble(thT2.Text.Replace('.', ','));
                else
                    thT2_double = 0.0;
                if (Double.TryParse(thT3.Text.Replace('.', ','), out thT3_double))
                    thT3_double = Convert.ToDouble(thT3.Text.Replace('.', ','));
                else
                    thT3_double = 0.0;
                if (Double.TryParse(thT4.Text.Replace('.', ','), out thT4_double))
                    thT4_double = Convert.ToDouble(thT4.Text.Replace('.', ','));
                else
                    thT4_double = 0.0;
                if (Double.TryParse(thT5.Text.Replace('.', ','), out thT5_double))
                    thT5_double = Convert.ToDouble(thT5.Text.Replace('.', ','));
                else
                    thT5_double = 0.0;
                if (Double.TryParse(thT6.Text.Replace('.', ','), out thT6_double))
                    thT6_double = Convert.ToDouble(thT6.Text.Replace('.', ','));
                else
                    thT6_double = 0.0;
                if (Double.TryParse(thT7.Text.Replace('.', ','), out thT7_double))
                    thT7_double = Convert.ToDouble(thT7.Text.Replace('.', ','));
                else
                    thT7_double = 0.0;
                if (Double.TryParse(thT8.Text.Replace('.', ','), out thT8_double))
                    thT8_double = Convert.ToDouble(thT8.Text.Replace('.', ','));
                else
                    thT8_double = 0.0;
                if (Double.TryParse(thT9.Text.Replace('.', ','), out thT9_double))
                    thT9_double = Convert.ToDouble(thT9.Text.Replace('.', ','));
                else
                    thT9_double = 0.0;
                if (Double.TryParse(thT10.Text.Replace('.', ','), out thT10_double))
                    thT10_double = Convert.ToDouble(thT10.Text.Replace('.', ','));
                else
                    thT10_double = 0.0;

                //VALORES TEMPERATURA CILINDRO REALES
                double thBoq_REAL_double = 0;
                double thT1_REAL_double = 0;
                double thT2_REAL_double = 0;
                double thT3_REAL_double = 0;
                double thT4_REAL_double = 0;
                double thT5_REAL_double = 0;
                double thT6_REAL_double = 0;
                double thT7_REAL_double = 0;
                double thT8_REAL_double = 0;
                double thT9_REAL_double = 0;
                double thT10_REAL_double = 0;

                if (Double.TryParse(thBoqREAL.Text.Replace('.', ','), out thBoq_REAL_double))
                    thBoq_REAL_double = Convert.ToDouble(thBoqREAL.Text.Replace('.', ','));
                else
                    thBoq_REAL_double = 0.0;
                if (Double.TryParse(thT1REAL.Text.Replace('.', ','), out thT1_REAL_double))
                    thT1_REAL_double = Convert.ToDouble(thT1REAL.Text.Replace('.', ','));
                else
                    thT1_REAL_double = 0.0;
                if (Double.TryParse(thT2REAL.Text.Replace('.', ','), out thT2_REAL_double))
                    thT2_REAL_double = Convert.ToDouble(thT2REAL.Text.Replace('.', ','));
                else
                    thT2_REAL_double = 0.0;
                if (Double.TryParse(thT3REAL.Text.Replace('.', ','), out thT3_REAL_double))
                    thT3_REAL_double = Convert.ToDouble(thT3REAL.Text.Replace('.', ','));
                else
                    thT3_REAL_double = 0.0;
                if (Double.TryParse(thT4REAL.Text.Replace('.', ','), out thT4_REAL_double))
                    thT4_REAL_double = Convert.ToDouble(thT4REAL.Text.Replace('.', ','));
                else
                    thT4_REAL_double = 0.0;
                if (Double.TryParse(thT5REAL.Text.Replace('.', ','), out thT5_REAL_double))
                    thT5_REAL_double = Convert.ToDouble(thT5REAL.Text.Replace('.', ','));
                else
                    thT5_REAL_double = 0.0;
                if (Double.TryParse(thT6REAL.Text.Replace('.', ','), out thT6_REAL_double))
                    thT6_REAL_double = Convert.ToDouble(thT6REAL.Text.Replace('.', ','));
                else
                    thT6_REAL_double = 0.0;
                if (Double.TryParse(thT7REAL.Text.Replace('.', ','), out thT7_REAL_double))
                    thT7_REAL_double = Convert.ToDouble(thT7REAL.Text.Replace('.', ','));
                else
                    thT7_REAL_double = 0.0;
                if (Double.TryParse(thT8REAL.Text.Replace('.', ','), out thT8_REAL_double))
                    thT8_REAL_double = Convert.ToDouble(thT8REAL.Text.Replace('.', ','));
                else
                    thT8_REAL_double = 0.0;
                if (Double.TryParse(thT9REAL.Text.Replace('.', ','), out thT9_REAL_double))
                    thT9_REAL_double = Convert.ToDouble(thT9REAL.Text.Replace('.', ','));
                else
                    thT9_REAL_double = 0.0;
                if (Double.TryParse(thT10REAL.Text.Replace('.', ','), out thT10_REAL_double))
                    thT10_REAL_double = Convert.ToDouble(thT10REAL.Text.Replace('.', ','));
                else
                    thT10_REAL_double = 0.0;


                //VALORES DE TOLERANCIAS
                double TiempoInyeccionNValDouble = 0;
                double TiempoInyeccionMValDouble = 0;
                double LimitePresionNValDouble = 0;
                double LimitePresionMValDouble = 0;
                double ConmuntaciontolNValDouble = 0;
                double ConmuntaciontolMValDouble = 0;
                double TiempoPresiontolNValDouble = 0;
                double TiempoPresiontolMValDouble = 0;
                double TNvcargavalDouble = 0;
                double TMvcargavalDouble = 0;
                double TNcargavalDouble = 0;
                double TMcargavalDouble = 0;
                double TNdescomvalDouble = 0;
                double TMdescomvalDouble = 0;
                double TNcontrapvalDouble = 0;
                double TMcontrapvalDouble = 0;
                double TNTiempdosvalDouble = 0;
                double TMTiempdosvalDouble = 0;
                double TNEnfriamvalDouble = 0;
                double TMEnfriamvalDouble = 0;
                double TNCiclovalDouble = 0;
                double TMCiclovalDouble = 0;
                double TNCojinvalDouble = 0;
                double TMCojinvalDouble = 0;

                double tbTiempoInyeccion_double = 0;
                double tbLimitePresion_double = 0;
                double thVCarga_double = 0;
                double thCarga_double = 0;
                double thDescomp_double = 0;
                double thContrapr_double = 0;
                double thTiempo_double = 0;
                double thEnfriamiento_double = 0;
                double thCiclo_double = 0;
                double thCojin_double = 0;

                if (Double.TryParse(tbTiempoInyeccion.Text.Replace('.', ','), out tbTiempoInyeccion_double))
                    tbTiempoInyeccion_double = Convert.ToDouble(tbTiempoInyeccion.Text.Replace('.', ','));
                else tbTiempoInyeccion_double = 0.0;

                if (Double.TryParse(tbLimitePresion.Text.Replace('.', ','), out tbLimitePresion_double))
                    tbLimitePresion_double = Convert.ToDouble(tbLimitePresion.Text.Replace('.', ','));
                else tbLimitePresion_double = 0.0;

                if (Double.TryParse(thVCarga.Text.Replace('.', ','), out thVCarga_double))
                    thVCarga_double = Convert.ToDouble(thVCarga.Text.Replace('.', ','));
                else thVCarga_double = 0.0;

                if (Double.TryParse(thCarga.Text.Replace('.', ','), out thCarga_double))
                    thCarga_double = Convert.ToDouble(thCarga.Text.Replace('.', ','));
                else thCarga_double = 0.0;

                if (Double.TryParse(thDescomp.Text.Replace('.', ','), out thDescomp_double))
                    thDescomp_double = Convert.ToDouble(thDescomp.Text.Replace('.', ','));
                else thDescomp_double = 0.0;

                if (Double.TryParse(thContrapr.Text.Replace('.', ','), out thContrapr_double))
                    thContrapr_double = Convert.ToDouble(thContrapr.Text.Replace('.', ','));
                else thContrapr_double = 0.0;

                if (Double.TryParse(thTiempo.Text.Replace('.', ','), out thTiempo_double))
                    thTiempo_double = Convert.ToDouble(thTiempo.Text.Replace('.', ','));
                else thTiempo_double = 0.0;

                if (Double.TryParse(thEnfriamiento.Text.Replace('.', ','), out thEnfriamiento_double))
                    thEnfriamiento_double = Convert.ToDouble(thEnfriamiento.Text.Replace('.', ','));
                else thEnfriamiento_double = 0.0;

                if (Double.TryParse(thCiclo.Text.Replace('.', ','), out thCiclo_double))
                    thCiclo_double = Convert.ToDouble(thCiclo.Text.Replace('.', ','));
                else thCiclo_double = 0.0;

                if (Double.TryParse(thCojin.Text.Replace('.', ','), out thCojin_double))
                    thCojin_double = Convert.ToDouble(thCojin.Text.Replace('.', ','));
                else thCojin_double = 0.0;

                if (Double.TryParse(tbTiempoInyeccionNVal.Text.Replace('.', ','), out TiempoInyeccionNValDouble))
                    TiempoInyeccionNValDouble = Convert.ToDouble(tbTiempoInyeccionNVal.Text.Replace('.', ','));
                else
                    TiempoInyeccionNValDouble = 0.0;
                if (Double.TryParse(tbTiempoInyeccionMVal.Text.Replace('.', ','), out TiempoInyeccionMValDouble))
                    TiempoInyeccionMValDouble = Convert.ToDouble(tbTiempoInyeccionMVal.Text.Replace('.', ','));
                else
                    TiempoInyeccionMValDouble = 0.0;
                if (Double.TryParse(tbLimitePresionNVal.Text.Replace('.', ','), out LimitePresionNValDouble))
                    LimitePresionNValDouble = Convert.ToDouble(tbLimitePresionNVal.Text.Replace('.', ','));
                else
                    LimitePresionNValDouble = 0.0;
                if (Double.TryParse(tbLimitePresionMVal.Text.Replace('.', ','), out LimitePresionMValDouble))
                    LimitePresionMValDouble = Convert.ToDouble(tbLimitePresionMVal.Text.Replace('.', ','));
                else
                    LimitePresionNValDouble = 0.0;
                if (Double.TryParse(thConmuntaciontolNVal.Text.Replace('.', ','), out ConmuntaciontolNValDouble))
                    ConmuntaciontolNValDouble = Convert.ToDouble(thConmuntaciontolNVal.Text.Replace('.', ','));
                else
                    ConmuntaciontolNValDouble = 0.0;
                if (Double.TryParse(thConmuntaciontolMVal.Text.Replace('.', ','), out ConmuntaciontolMValDouble))
                    ConmuntaciontolMValDouble = Convert.ToDouble(thConmuntaciontolMVal.Text.Replace('.', ','));
                else
                    ConmuntaciontolMValDouble = 0.0;
                if (Double.TryParse(tbTiempoPresiontolNVal.Text.Replace('.', ','), out TiempoPresiontolNValDouble))
                    TiempoPresiontolNValDouble = Convert.ToDouble(tbTiempoPresiontolNVal.Text.Replace('.', ','));
                else
                    TiempoPresiontolNValDouble = 0.0;
                if (Double.TryParse(tbTiempoPresiontolMVal.Text.Replace('.', ','), out TiempoPresiontolMValDouble))
                    TiempoPresiontolMValDouble = Convert.ToDouble(tbTiempoPresiontolMVal.Text.Replace('.', ','));
                else
                    TiempoPresiontolMValDouble = 0.0;
                if (Double.TryParse(TNvcargaval.Text.Replace('.', ','), out TNvcargavalDouble))
                    TNvcargavalDouble = Convert.ToDouble(TNvcargaval.Text.Replace('.', ','));
                else
                    TNvcargavalDouble = 0.0;
                if (Double.TryParse(TMvcargaval.Text.Replace('.', ','), out TMvcargavalDouble))
                    TMvcargavalDouble = Convert.ToDouble(TMvcargaval.Text.Replace('.', ','));
                else
                    TMvcargavalDouble = 0.0;
                if (Double.TryParse(TNcargaval.Text.Replace('.', ','), out TNcargavalDouble))
                    TNcargavalDouble = Convert.ToDouble(TNcargaval.Text.Replace('.', ','));
                else
                    TNcargavalDouble = 0.0;
                if (Double.TryParse(TMcargaval.Text.Replace('.', ','), out TMcargavalDouble))
                    TMcargavalDouble = Convert.ToDouble(TMcargaval.Text.Replace('.', ','));
                else
                    TMcargavalDouble = 0.0;

                if (Double.TryParse(TNdescomval.Text.Replace('.', ','), out TNdescomvalDouble))
                    TNdescomvalDouble = Convert.ToDouble(TNdescomval.Text.Replace('.', ','));
                else
                    TNdescomvalDouble = 0.0;
                if (Double.TryParse(TMdescomval.Text.Replace('.', ','), out TMdescomvalDouble))
                    TMdescomvalDouble = Convert.ToDouble(TMdescomval.Text.Replace('.', ','));
                else
                    TMdescomvalDouble = 0.0;
                if (Double.TryParse(TNcontrapval.Text.Replace('.', ','), out TNcontrapvalDouble))
                    TNcontrapvalDouble = Convert.ToDouble(TNcontrapval.Text.Replace('.', ','));
                else
                    TNcontrapvalDouble = 0.0;
                if (Double.TryParse(TMcontrapval.Text.Replace('.', ','), out TMcontrapvalDouble))
                    TMcontrapvalDouble = Convert.ToDouble(TMcontrapval.Text.Replace('.', ','));
                else
                    TMcontrapvalDouble = 0.0;
                if (Double.TryParse(TNTiempdosval.Text.Replace('.', ','), out TNTiempdosvalDouble))
                    TNTiempdosvalDouble = Convert.ToDouble(TNTiempdosval.Text.Replace('.', ','));
                else
                    TNTiempdosvalDouble = 0.0;
                if (Double.TryParse(TMTiempdosval.Text.Replace('.', ','), out TMTiempdosvalDouble))
                    TMTiempdosvalDouble = Convert.ToDouble(TMTiempdosval.Text.Replace('.', ','));
                else
                    TMTiempdosvalDouble = 0.0;
                if (Double.TryParse(TNEnfriamval.Text.Replace('.', ','), out TNEnfriamvalDouble))
                    TNEnfriamvalDouble = Convert.ToDouble(TNEnfriamval.Text.Replace('.', ','));
                else
                    TNEnfriamvalDouble = 0.0;
                if (Double.TryParse(TMEnfriamval.Text.Replace('.', ','), out TMEnfriamvalDouble))
                    TMEnfriamvalDouble = Convert.ToDouble(TMEnfriamval.Text.Replace('.', ','));
                else
                    TMEnfriamvalDouble = 0.0;

                if (Double.TryParse(TNCicloval.Text.Replace('.', ','), out TNCiclovalDouble))
                    TNCiclovalDouble = Convert.ToDouble(TNCicloval.Text.Replace('.', ','));
                else
                    TNCiclovalDouble = 0.0;
                if (Double.TryParse(TMCicloval.Text.Replace('.', ','), out TMCiclovalDouble))
                    TMCiclovalDouble = Convert.ToDouble(TMCicloval.Text.Replace('.', ','));
                else
                    TMCiclovalDouble = 0.0;

                if (Double.TryParse(TNCojinval.Text.Replace('.', ','), out TNCojinvalDouble))
                    TNCojinvalDouble = Convert.ToDouble(TNCojinval.Text.Replace('.', ','));
                else
                    TNCojinvalDouble = 0.0;
                if (Double.TryParse(TMCojinval.Text.Replace('.', ','), out TMCojinvalDouble))
                    TMCojinvalDouble = Convert.ToDouble(TMCojinval.Text.Replace('.', ','));
                else
                    TMCojinvalDouble = 0.0;

                //VALORES REALES DE TOLERANCIAS
                double tbTiempoInyeccion_double_REAL = 0;
                double tbLimitePresion_double_REAL = 0;
                double thVCarga_double_REAL = 0;
                double thCarga_double_REAL = 0;
                double thDescomp_double_REAL = 0;
                double thContrapr_double_REAL = 0;
                double thTiempo_double_REAL = 0;
                double thEnfriamiento_double_REAL = 0;
                double thCiclo_double_REAL = 0;
                double thCojin_double_REAL = 0;

                if (Double.TryParse(tbTiempoInyeccionREAL.Text.Replace('.', ','), out tbTiempoInyeccion_double_REAL))
                    tbTiempoInyeccion_double_REAL = Convert.ToDouble(tbTiempoInyeccionREAL.Text.Replace('.', ','));
                else tbTiempoInyeccion_double_REAL = 0.0;

                if (Double.TryParse(tbLimitePresionREAL.Text.Replace('.', ','), out tbLimitePresion_double_REAL))
                    tbLimitePresion_double_REAL = Convert.ToDouble(tbLimitePresionREAL.Text.Replace('.', ','));
                else tbLimitePresion_double_REAL = 0.0;

                if (Double.TryParse(thVCargaREAL.Text.Replace('.', ','), out thVCarga_double_REAL))
                    thVCarga_double_REAL = Convert.ToDouble(thVCargaREAL.Text.Replace('.', ','));
                else thVCarga_double_REAL = 0.0;

                if (Double.TryParse(thCargaREAL.Text.Replace('.', ','), out thCarga_double_REAL))
                    thCarga_double_REAL = Convert.ToDouble(thCargaREAL.Text.Replace('.', ','));
                else thCarga_double_REAL = 0.0;

                if (Double.TryParse(thDescompREAL.Text.Replace('.', ','), out thDescomp_double_REAL))
                    thDescomp_double_REAL = Convert.ToDouble(thDescompREAL.Text.Replace('.', ','));
                else thDescomp_double_REAL = 0.0;

                if (Double.TryParse(thContraprREAL.Text.Replace('.', ','), out thContrapr_double_REAL))
                    thContrapr_double_REAL = Convert.ToDouble(thContraprREAL.Text.Replace('.', ','));
                else thContrapr_double_REAL = 0.0;

                if (Double.TryParse(thTiempoREAL.Text.Replace('.', ','), out thTiempo_double_REAL))
                    thTiempo_double_REAL = Convert.ToDouble(thTiempoREAL.Text.Replace('.', ','));
                else thTiempo_double_REAL = 0.0;

                if (Double.TryParse(thEnfriamientoREAL.Text.Replace('.', ','), out thEnfriamiento_double_REAL))
                    thEnfriamiento_double_REAL = Convert.ToDouble(thEnfriamientoREAL.Text.Replace('.', ','));
                else thEnfriamiento_double_REAL = 0.0;

                if (Double.TryParse(thCicloREAL.Text.Replace('.', ','), out thCiclo_double_REAL))
                    thCiclo_double_REAL = Convert.ToDouble(thCicloREAL.Text.Replace('.', ','));
                else thCiclo_double_REAL = 0.0;

                if (Double.TryParse(thCojinREAL.Text.Replace('.', ','), out thCojin_double_REAL))
                    thCojin_double_REAL = Convert.ToDouble(thCojinREAL.Text.Replace('.', ','));
                else thCojin_double_REAL = 0.0;

                //MATERIALES
                double MAT1TIEMP_double = 0;
                if (Double.TryParse(MAT1TIEMP.Text.Replace('.', ','), out MAT1TIEMP_double))
                    MAT1TIEMP_double = Convert.ToDouble(MAT1TIEMP.Text.Replace('.', ','));
                else
                    MAT1TIEMP_double = 0.0;


                double MAT1TIEMPREAL_double = 0;
                if (Double.TryParse(MAT1TIEMPREAL.Text.Replace('.', ','), out MAT1TIEMPREAL_double))
                    MAT1TIEMPREAL_double = Convert.ToDouble(MAT1TIEMPREAL.Text.Replace('.', ','));
                else
                    MAT1TIEMPREAL_double = 0.0;
                double MAT1TEMP_double = 0;
                if (Double.TryParse(MAT1TEMP.Text.Replace('.', ','), out MAT1TEMP_double))
                    MAT1TEMP_double = Convert.ToDouble(MAT1TEMP.Text.Replace('.', ','));
                else
                    MAT1TEMP_double = 0.0;
                double MAT1TEMPREAL_double = 0;
                if (Double.TryParse(MAT1TEMPREAL.Text.Replace('.', ','), out MAT1TEMPREAL_double))
                    MAT1TEMPREAL_double = Convert.ToDouble(MAT1TEMPREAL.Text.Replace('.', ','));
                else
                    MAT1TEMPREAL_double = 0.0;
                double MAT2TIEMP_double = 0;
                if (Double.TryParse(MAT2TIEMP.Text.Replace('.', ','), out MAT2TIEMP_double))
                    MAT2TIEMP_double = Convert.ToDouble(MAT2TIEMP.Text.Replace('.', ','));
                else
                    MAT2TIEMP_double = 0.0;
                double MAT2TIEMPREAL_double = 0;
                if (Double.TryParse(MAT2TIEMPREAL.Text.Replace('.', ','), out MAT2TIEMPREAL_double))
                    MAT2TIEMPREAL_double = Convert.ToDouble(MAT2TIEMPREAL.Text.Replace('.', ','));
                else
                    MAT2TIEMPREAL_double = 0.0;
                double MAT2TEMP_double = 0;
                if (Double.TryParse(MAT2TEMP.Text.Replace('.', ','), out MAT2TEMP_double))
                    MAT2TEMP_double = Convert.ToDouble(MAT2TEMP.Text.Replace('.', ','));
                else
                    MAT2TEMP_double = 0.0;
                double MAT2TEMPREAL_double = 0;
                if (Double.TryParse(MAT2TEMPREAL.Text.Replace('.', ','), out MAT2TEMPREAL_double))
                    MAT2TEMPREAL_double = Convert.ToDouble(MAT2TEMPREAL.Text.Replace('.', ','));
                else
                    MAT2TEMPREAL_double = 0.0;

                double MAT3TIEMP_double = 0;
                if (Double.TryParse(MAT3TIEMP.Text.Replace('.', ','), out MAT3TIEMP_double))
                    MAT3TIEMP_double = Convert.ToDouble(MAT3TIEMP.Text.Replace('.', ','));
                else
                    MAT3TIEMP_double = 0.0;
                double MAT3TIEMPREAL_double = 0;
                if (Double.TryParse(MAT3TIEMPREAL.Text.Replace('.', ','), out MAT3TIEMPREAL_double))
                    MAT3TIEMPREAL_double = Convert.ToDouble(MAT3TIEMPREAL.Text.Replace('.', ','));
                else
                    MAT3TIEMPREAL_double = 0.0;
                double MAT3TEMP_double = 0;
                if (Double.TryParse(MAT3TEMP.Text.Replace('.', ','), out MAT3TEMP_double))
                    MAT3TEMP_double = Convert.ToDouble(MAT3TEMP.Text.Replace('.', ','));
                else
                    MAT3TEMP_double = 0.0;
                double MAT3TEMPREAL_double = 0;
                if (Double.TryParse(MAT3TEMPREAL.Text.Replace('.', ','), out MAT3TEMPREAL_double))
                    MAT3TEMPREAL_double = Convert.ToDouble(MAT3TEMPREAL.Text.Replace('.', ','));
                else
                    MAT3TEMPREAL_double = 0.0;

                //CUESTIONARIOS
                //AUDITORIA
                int Q1E = 0;
                    if (Q1_OKOPT.Checked == true)
                       { Q1E = 2; }
                    else if (Q1_NOKOPT.Checked == true)
                       { Q1E = 1; }
                    else
                       { Q1E = 0; }

                int Q2E = 0;
                    if (Q2_OKOPT.Checked == true)
                    { Q2E = 2; }
                    else if (Q2_NOKOPT.Checked == true)
                    { Q2E = 1; }
                    else
                    { Q2E = 0; }

                int Q3E = 0;
                    if (Q3_OKOPT.Checked == true)
                    { Q3E = 2; }
                    else if (Q3_NOKOPT.Checked == true)
                    { Q3E = 1; }
                    else
                    { Q3E = 0; }

                int Q4E = 0;
                    if (Q4_OKOPT.Checked == true)
                    { Q4E = 2; }
                    else if (Q4_NOKOPT.Checked == true)
                    { Q4E = 1; }
                    else
                    { Q4E = 0; }

                int Q5E = 0;
                    if (Q5_OKOPT.Checked == true)
                    { Q5E = 2; }
                    else if (Q5_NOKOPT.Checked == true)
                    { Q5E = 1; }
                    else
                    { Q5E = 0; }

                int Q6E = 0;
                    if (Q6_OKOPT.Checked == true)
                    { Q6E = 2; }
                    else if (Q6_NOKOPT.Checked == true)
                    { Q6E = 1; }
                    else
                    { Q6E = 0; }

                int Q7E = 0;
                    if (Q7_OKOPT.Checked == true)
                    { Q7E = 2; }
                    else if (Q7_NOKOPT.Checked == true)
                    { Q7E = 1; }
                    else
                    { Q7E = 0; }

                int Q8E = 0;
                    if (Q8_OKOPT.Checked == true)
                    { Q8E = 2; }
                    else if (Q8_NOKOPT.Checked == true)
                    { Q8E = 1; }
                    else
                    { Q8E = 0; }
                int Q9E = 0;
                    if (Q9_OKOPT.Checked == true)
                    { Q9E = 2; }
                    else if (Q9_NOKOPT.Checked == true)
                    { Q9E = 1; }
                    else
                    { Q9E = 0; }
               /* int Q10E = 0;
                int Q1C = 0;
                int Q2C = 0;
                int Q3C = 0;
                int Q4C = 0;
                int Q5C = 0;*/
                int Q6C = 0;
                    if (Q6C_OKOPT.Checked == true)
                    { Q6C = 2; }
                    else if (Q6C_NOKOPT.Checked == true)
                    { Q6C = 1; }
                    else
                    { Q6C = 0; }
                int Q7C = 0;
                    if (Q7C_OKOPT.Checked == true)
                    { Q7C = 2; }
                    else if (Q7C_NOKOPT.Checked == true)
                    { Q7C = 1; }
                    else
                    { Q7C = 0; }
                int Q8C = 0;
                    if (Q8C_OKOPT.Checked == true)
                    { Q8C = 2; }
                    else if (Q8C_NOKOPT.Checked == true)
                    { Q8C = 1; }
                    else
                    { Q8C = 0; }
                int Q9C = 0;
                    if (Q9C_OKOPT.Checked == true)
                    { Q9C = 2; }
                    else if (Q9C_NOKOPT.Checked == true)
                    { Q9C = 1; }
                    else
                    { Q9C = 0; }
                int Q10C = 0;
                    if (Q10C_OKOPT.Checked == true)
                    { Q10C = 2; }
                    else if (Q10C_NOKOPT.Checked == true)
                    { Q10C = 1; }
                    else
                    { Q10C = 0; }

                //EVALUO MATERIALES
                int ResultadoLOTES = 0;
                int ResultadoPARAM = 0;
                if ((MAT1REF.Visible == true && MAT1LOT.Text == "") || (MAT2REF.Visible == true && MAT2LOT.Text == "") || (MAT3REF.Visible == true && MAT3LOT.Text == "") || (COMP1REF.Visible == true && COMP1LOT.Text == "") || (COMP2REF.Visible == true && COMP2LOT.Text == "") || (COMP3REF.Visible == true && COMP3LOT.Text == "") || (COMP4REF.Visible == true && COMP4LOT.Text == "") || (COMP5REF.Visible == true && COMP5LOT.Text == "") || (COMP6REF.Visible == true && COMP6LOT.Text == "") || (COMP7REF.Visible == true && COMP7LOT.Text == ""))
                {
                    ResultadoLOTES = 1;
                }
                if (PARAMSESTADO.Text != "")
                {
                    ResultadoPARAM = 1;
                }

                String MAT1LOTE = MAT1LOT.Text + "|" + MAT1LOT2.Text;
                String MAT2LOTE = MAT2LOT.Text + "|" + MAT2LOT2.Text;
                String MAT3LOTE = MAT3LOT.Text + "|" + MAT3LOT2.Text;
                String COMP1LOTE = COMP1LOT.Text + "|" + COMP1LOT2.Text;
                String COMP2LOTE = COMP2LOT.Text + "|" + COMP2LOT2.Text;
                String COMP3LOTE = COMP3LOT.Text + "|" + COMP3LOT2.Text;
                String COMP4LOTE = COMP4LOT.Text + "|" + COMP4LOT2.Text;
                String COMP5LOTE = COMP5LOT.Text + "|" + COMP5LOT2.Text;
                String COMP6LOTE = COMP6LOT.Text + "|" + COMP6LOT2.Text;
                String COMP7LOTE = COMP7LOT.Text + "|" + COMP7LOT2.Text;

                Conexion_LIBERACIONES conexion = new Conexion_LIBERACIONES();
                conexion.ActualizarLiberacionFicha(tbOrden.Text, Convert.ToInt32(tbReferencia.Text), tbMaquina.Text, version, Convert.ToInt32(tbMolde.Text), operario1numero, operario1horas, Convert.ToString(Operario1Nivel.SelectedValue), Operario1UltRevision.Text, Operario1Notas.Text, operario2numero, operario2horas, Convert.ToString(Operario2Nivel.SelectedValue),
                       Operario2UltRevision.Text, Operario2Notas.Text, encargadonumero, encargadohoras, cambiadornumero, cambiadorhoras, calidadnumero, calidadhoras, CambiadorLiberado, LiberacionCambiadorHora.Text, ProduccionLiberado, LiberacionEncargadoHora.Text, CalidadLiberado, LiberacionCalidadHora.Text, ResultadoLiberacion, Convert.ToInt32(LiberacionCondicionada.SelectedValue), NotaLiberacionCondicionada.Text, Reliberacion, NCEncargado, GP12Encargado, NCCalidad, GP12Calidad, QUERYLiberarCambiadorHoraORI, QUERYLiberarEncargadoHoraORI, QUERYLiberacionCalidadHoraORI, ResultadoLOTES, ResultadoPARAM,
                                //Valores POSTPRESION
                       thP1_double, thTP1_double, thP2_double, thTP2_double, thP3_double, thTP3_double, thP4_double, thTP4_double, thP5_double, thTP5_double, thP6_double, thTP6_double, thP7_double, thTP7_double, thP8_double, thTP8_double, thP9_double, thTP9_double, thP10_double, thTP10_double, tbConmutacion.Text, tbTiempoPresion.Text,
                            //Valores POSTPRESION REALES
                       thP1_REAL_double, thTP1_REAL_double, thP2_REAL_double, thTP2_REAL_double, thP3_REAL_double, thTP3_REAL_double, thP4_REAL_double, thTP4_REAL_double, thP5_REAL_double, thTP5_REAL_double, thP6_REAL_double, thTP6_REAL_double, thP7_REAL_double, thTP7_REAL_double, thP8_REAL_double, thTP8_REAL_double, thP9_REAL_double, thTP9_REAL_double, thP10_REAL_double, thTP10_REAL_double, tbConmutacionREAL.Text, tbTiempoPresionREAL.Text,
                       //VALORES CAMARA CALIENTE
                       thZ1_double, thZ2_double, thZ3_double, thZ4_double, thZ5_double, thZ6_double, thZ7_double, thZ8_double, thZ9_double, thZ10_double, thZ11_double, thZ12_double, thZ13_double, thZ14_double, thZ15_double, thZ16_double, thZ17_double, thZ18_double, thZ19_double, thZ20_double,
                            //VALORES REALES CAMARA CALIENTE
                       thZ1_REAL_double, thZ2_REAL_double, thZ3_REAL_double, thZ4_REAL_double, thZ5_REAL_double, thZ6_REAL_double, thZ7_REAL_double, thZ8_REAL_double, thZ9_REAL_double, thZ10_REAL_double, thZ11_REAL_double, thZ12_REAL_double, thZ13_REAL_double, thZ14_REAL_double, thZ15_REAL_double, thZ16_REAL_double, thZ17_REAL_double, thZ18_REAL_double, thZ19_REAL_double, thZ20_REAL_double,
                       //VALORES TEMPERATURA CILINDRO
                       thBoq_double, thT1_double, thT2_double, thT3_double, thT4_double, thT5_double, thT6_double, thT7_double, thT8_double, thT9_double, thT10_double, 
                            //VALORES REALES TEMPERATURA CILINDRO
                       thBoq_REAL_double, thT1_REAL_double, thT2_REAL_double, thT3_REAL_double, thT4_REAL_double, thT5_REAL_double, thT6_REAL_double, thT7_REAL_double, thT8_REAL_double, thT9_REAL_double, thT10_REAL_double, EXISTEFICHA.Text,
                       //VALORES TOLERANCIAS 
                       tbTiempoInyeccion_double, tbLimitePresion_double, thVCarga_double, thCarga_double, thDescomp_double, thContrapr_double, thTiempo_double, thEnfriamiento_double, thCiclo_double, thCojin_double,
                       TiempoInyeccionNValDouble, TiempoInyeccionMValDouble, LimitePresionNValDouble, LimitePresionMValDouble, ConmuntaciontolNValDouble, ConmuntaciontolMValDouble, TiempoPresiontolNValDouble,
                       TiempoPresiontolMValDouble, TNvcargavalDouble, TMvcargavalDouble, TNcargavalDouble, TMcargavalDouble, TNdescomvalDouble, TMdescomvalDouble, TNcontrapvalDouble,
                       TMcontrapvalDouble, TNTiempdosvalDouble, TMTiempdosvalDouble, TNEnfriamvalDouble, TMEnfriamvalDouble, TNCiclovalDouble, TMCiclovalDouble, TNCojinvalDouble, TMCojinvalDouble,
                        //VALORES TOLERANCIAS REALES
                       tbTiempoInyeccion_double_REAL, tbLimitePresion_double_REAL, thVCarga_double_REAL, thCarga_double_REAL, thDescomp_double_REAL, thContrapr_double_REAL, thTiempo_double_REAL, thEnfriamiento_double_REAL, thCiclo_double_REAL, thCojin_double_REAL,
                       //VALORES MATERIALES
                       MAT1REF.Text, MAT1NOM.Text, MAT1LOTE, MAT1TIEMP_double, MAT1TIEMPREAL_double, MAT1TEMP_double, MAT1TEMPREAL_double, MAT2REF.Text, MAT2NOM.Text, MAT2LOTE, MAT2TIEMP_double, MAT2TIEMPREAL_double, MAT2TEMP_double, MAT2TEMPREAL_double, MAT3REF.Text, MAT3NOM.Text, MAT3LOTE, MAT3TIEMP_double, MAT3TIEMPREAL_double, MAT3TEMP_double, MAT3TEMPREAL_double,
                       COMP1REF.Text, COMP1NOM.Text, COMP1LOTE, COMP2REF.Text, COMP2NOM.Text, COMP2LOTE, COMP3REF.Text, COMP3NOM.Text, COMP3LOTE, COMP4REF.Text, COMP4NOM.Text, COMP4LOTE, COMP5REF.Text, COMP5NOM.Text, COMP5LOTE, COMP6REF.Text, COMP6NOM.Text, COMP6LOTE, COMP7REF.Text, COMP7NOM.Text, COMP7LOTE,
                       //VALORES ATEMPERADO
                       TbCircuitoF1.Text, TbCircuitoF2.Text, TbCircuitoF3.Text, TbCircuitoF4.Text, TbCircuitoF5.Text, TbCircuitoF6.Text,
                       TbCaudalF1.Text, TbCaudalF2.Text, TbCaudalF3.Text, TbCaudalF4.Text, TbCaudalF5.Text, TbCaudalF6.Text,
                       TbTemperaturaF1.Text, TbTemperaturaF2.Text, TbTemperaturaF3.Text, TbTemperaturaF4.Text, TbTemperaturaF5.Text, TbTemperaturaF6.Text,
                       TbCircuitoM1.Text, TbCircuitoM2.Text, TbCircuitoM3.Text, TbCircuitoM4.Text, TbCircuitoM5.Text, TbCircuitoM6.Text,
                       TbCaudalM1.Text, TbCaudalM2.Text, TbCaudalM3.Text, TbCaudalM4.Text, TbCaudalM5.Text, TbCaudalM6.Text,
                       TbTemperaturaM1.Text, TbTemperaturaM2.Text, TbTemperaturaM3.Text, TbTemperaturaM4.Text, TbTemperaturaM5.Text, TbTemperaturaM6.Text,
                       TbCaudalF1REAL.Text, TbCaudalF2REAL.Text, TbCaudalF3REAL.Text, TbCaudalF4REAL.Text, TbCaudalF5REAL.Text, TbCaudalF6REAL.Text,
                       TbTemperaturaF1REAL.Text, TbTemperaturaF2REAL.Text, TbTemperaturaF3REAL.Text, TbTemperaturaF4REAL.Text, TbTemperaturaF5REAL.Text, TbTemperaturaF6REAL.Text,
                       TbCaudalM1REAL.Text, TbCaudalM2REAL.Text, TbCaudalM3REAL.Text, TbCaudalM4REAL.Text, TbCaudalM5REAL.Text, TbCaudalM6REAL.Text,
                       TbTemperaturaM1REAL.Text, TbTemperaturaM2REAL.Text, TbTemperaturaM3REAL.Text, TbTemperaturaM4REAL.Text, TbTemperaturaM5REAL.Text, TbTemperaturaM6REAL.Text,
                       //VALORES AUDITORIA
                       Q1E, TbQ1ENC.Text, Q2E, TbQ2ENC.Text, Q3E, TbQ3ENC.Text, Q4E, TbQ4ENC.Text, Q5E, TbQ5ENC.Text, Q6E, Q6C, TbQ6ENC.Text, TbQ6CAL.Text,
                       Q7E, Q7C, TbQ7ENC.Text, TbQ7CAL.Text, Q8E, Q8C, TbQ8ENC.Text, TbQ8CAL.Text, Q9E, Q9C, TbQ9ENC.Text, TbQ9CAL.Text, Q10C, TbQ10CAL.Text, QXFeedbackCambiador.Text, QXFeedbackProduccion.Text, QXFeedbackCalidad.Text
                       );
            }
            catch (Exception) { }
        }
        public void LiberarCambiadorFunction(string QUERYLiberarCambiadorHoraORI)
        {
            try
            {
                //FICHA DE LIBERACION
                int version = 0;

                int operario1numero = 0;
                if (Operario1Numero.Text != "")
                { operario1numero = Convert.ToInt32(Operario1Numero.Text); }

                double operario1horas = 0;
                if (Operario1Horas.Text != "")
                { operario1horas = Convert.ToDouble(Operario1Horas.Text); }

                int operario2numero = 0;
                if (Operario2Numero.Text != "")
                { operario2numero = Convert.ToInt32(Operario2Numero.Text); }
                double operario2horas = 0;
                if (Operario2Horas.Text != "")
                { operario2horas = Convert.ToDouble(Operario2Horas.Text); }

                int encargadonumero = 0;
                if (EncargadoNumero.Text != "")
                { encargadonumero = Convert.ToInt32(EncargadoNumero.Text); }
                double encargadohoras = 0;
                if (EncargadoHoras.Text != "")
                { encargadohoras = Convert.ToDouble(EncargadoHoras.Text); }

                int cambiadornumero = 0;
                if (CambiadorNumero.Text != "")
                { cambiadornumero = Convert.ToInt32(CambiadorNumero.Text); }
                double cambiadorhoras = 0;
                if (CambiadorHoras.Text != "")
                { cambiadorhoras = Convert.ToDouble(CambiadorHoras.Text); }

                int calidadnumero = 0;
                if (CalidadNumero.Text != "")
                { calidadnumero = Convert.ToInt32(CalidadNumero.Text); }

                double calidadhoras = 0;
                if (CalidadHoras.Text != "")
                { calidadhoras = Convert.ToDouble(CalidadHoras.Text); }

                int CambiadorLiberado = 0;

                if (LiberacionCambiadorHoraORI.Text != "" && (Q1_NOKOPT.Checked == true || Q2_NOKOPT.Checked == true || Q3_NOKOPT.Checked == true || (MAT1REF.Visible == true && MAT1LOT.Text == "") || (MAT2REF.Visible == true && MAT2LOT.Text == "") || (MAT3REF.Visible == true && MAT3LOT.Text == "") || (COMP1REF.Visible == true && COMP1LOT.Text == "") || (COMP2REF.Visible == true && COMP2LOT.Text == "") || (COMP3REF.Visible == true && COMP3LOT.Text == "") || (COMP4REF.Visible == true && COMP4LOT.Text == "") || (COMP5REF.Visible == true && COMP5LOT.Text == "") || (COMP6REF.Visible == true && COMP6LOT.Text == "") || (COMP7REF.Visible == true && COMP7LOT.Text == "")))
                {
                    CambiadorLiberado = 1;
                }
                else if (LiberacionCambiadorHoraORI.Text != "" && (Q1_NOKOPT.Checked == false && Q2_NOKOPT.Checked == false && Q3_NOKOPT.Checked == false))
                {
                    CambiadorLiberado = 2;
                }

                int ProduccionLiberado = 0;
                //
                if (estadoencargadoCOND.Visible == true)
                {
                    ProduccionLiberado = 1;
                }
                else if (estadoencargadoLIBOK.Visible == true)
                {
                    ProduccionLiberado = 2;
                }
                    /*
                    if (LiberacionEncargadoHora.Text != "" && (Q4_NOKOPT.Checked == true || Q5_NOKOPT.Checked == true || Q6_NOKOPT.Checked == true || Q7_NOKOPT.Checked == true || Q8_NOKOPT.Checked == true || Q9_NOKOPT.Checked == true || A3.Visible == true || A5.Visible == true))
                    {
                        ProduccionLiberado = 1;
                    }
                    else if (LiberacionEncargadoHora.Text != "" && (Q4_NOKOPT.Checked == false && Q5_NOKOPT.Checked == false && Q6_NOKOPT.Checked == false && Q7_NOKOPT.Checked == false && Q8_NOKOPT.Checked == false && Q9_NOKOPT.Checked == false))
                    {
                        ProduccionLiberado = 2;
                    }
                    */
                int CalidadLiberado = 0;
                if (estadocalidadCOND.Visible == true)
                {
                    CalidadLiberado = 1;
                }
                else if (estadocalidadLIBOK.Visible == true)
                {
                    CalidadLiberado = 2;
                }
                    /*
                    if (LiberacionCalidadHora.Text != "" && (Q10C_NOKOPT.Checked == true || Q6C_NOKOPT.Checked == true || Q7C_NOKOPT.Checked == true || Q8C_NOKOPT.Checked == true || Q9C_NOKOPT.Checked == true || A7.Visible == true || A7.Visible == true))
                    {
                        CalidadLiberado = 1;
                    }
                    else if (LiberacionCalidadHora.Text != "" && (Q10C_NOKOPT.Checked == false && Q6C_NOKOPT.Checked == false && Q7C_NOKOPT.Checked == false && Q8C_NOKOPT.Checked == false && Q9C_NOKOPT.Checked == false))
                    {
                        CalidadLiberado = 2;
                    }
                    */
                int ResultadoLiberacion = 0;
                int Reliberacion = 0;


                int NCEncargado = 0;
                if (A3OK.Visible == true)
                { NCEncargado = 1; }
                int GP12Encargado = 0;
                if (A5OK.Visible == true)
                { GP12Encargado = 1; }
                int NCCalidad = 0;
                if (A7OK.Visible == true)
                { NCCalidad = 1; }
                int GP12Calidad = 0;
                if (A8OK.Visible == true)
                { GP12Calidad = 1; }

                //MATERIALES
                double MAT1TIEMP_double = 0;
                if (Double.TryParse(MAT1TIEMP.Text.Replace('.', ','), out MAT1TIEMP_double))
                    MAT1TIEMP_double = Convert.ToDouble(MAT1TIEMP.Text.Replace('.', ','));
                else
                    MAT1TIEMP_double = 0.0;


                double MAT1TIEMPREAL_double = 0;
                if (Double.TryParse(MAT1TIEMPREAL.Text.Replace('.', ','), out MAT1TIEMPREAL_double))
                    MAT1TIEMPREAL_double = Convert.ToDouble(MAT1TIEMPREAL.Text.Replace('.', ','));
                else
                    MAT1TIEMPREAL_double = 0.0;
                double MAT1TEMP_double = 0;
                if (Double.TryParse(MAT1TEMP.Text.Replace('.', ','), out MAT1TEMP_double))
                    MAT1TEMP_double = Convert.ToDouble(MAT1TEMP.Text.Replace('.', ','));
                else
                    MAT1TEMP_double = 0.0;
                double MAT1TEMPREAL_double = 0;
                if (Double.TryParse(MAT1TEMPREAL.Text.Replace('.', ','), out MAT1TEMPREAL_double))
                    MAT1TEMPREAL_double = Convert.ToDouble(MAT1TEMPREAL.Text.Replace('.', ','));
                else
                    MAT1TEMPREAL_double = 0.0;
                double MAT2TIEMP_double = 0;
                if (Double.TryParse(MAT2TIEMP.Text.Replace('.', ','), out MAT2TIEMP_double))
                    MAT2TIEMP_double = Convert.ToDouble(MAT2TIEMP.Text.Replace('.', ','));
                else
                    MAT2TIEMP_double = 0.0;
                double MAT2TIEMPREAL_double = 0;
                if (Double.TryParse(MAT2TIEMPREAL.Text.Replace('.', ','), out MAT2TIEMPREAL_double))
                    MAT2TIEMPREAL_double = Convert.ToDouble(MAT2TIEMPREAL.Text.Replace('.', ','));
                else
                    MAT2TIEMPREAL_double = 0.0;
                double MAT2TEMP_double = 0;
                if (Double.TryParse(MAT2TEMP.Text.Replace('.', ','), out MAT2TEMP_double))
                    MAT2TEMP_double = Convert.ToDouble(MAT2TEMP.Text.Replace('.', ','));
                else
                    MAT2TEMP_double = 0.0;
                double MAT2TEMPREAL_double = 0;
                if (Double.TryParse(MAT2TEMPREAL.Text.Replace('.', ','), out MAT2TEMPREAL_double))
                    MAT2TEMPREAL_double = Convert.ToDouble(MAT2TEMPREAL.Text.Replace('.', ','));
                else
                    MAT2TEMPREAL_double = 0.0;

                double MAT3TIEMP_double = 0;
                if (Double.TryParse(MAT3TIEMP.Text.Replace('.', ','), out MAT3TIEMP_double))
                    MAT3TIEMP_double = Convert.ToDouble(MAT3TIEMP.Text.Replace('.', ','));
                else
                    MAT3TIEMP_double = 0.0;
                double MAT3TIEMPREAL_double = 0;
                if (Double.TryParse(MAT3TIEMPREAL.Text.Replace('.', ','), out MAT3TIEMPREAL_double))
                    MAT3TIEMPREAL_double = Convert.ToDouble(MAT3TIEMPREAL.Text.Replace('.', ','));
                else
                    MAT3TIEMPREAL_double = 0.0;
                double MAT3TEMP_double = 0;
                if (Double.TryParse(MAT3TEMP.Text.Replace('.', ','), out MAT3TEMP_double))
                    MAT3TEMP_double = Convert.ToDouble(MAT3TEMP.Text.Replace('.', ','));
                else
                    MAT3TEMP_double = 0.0;
                double MAT3TEMPREAL_double = 0;
                if (Double.TryParse(MAT3TEMPREAL.Text.Replace('.', ','), out MAT3TEMPREAL_double))
                    MAT3TEMPREAL_double = Convert.ToDouble(MAT3TEMPREAL.Text.Replace('.', ','));
                else
                    MAT3TEMPREAL_double = 0.0;

                //CUESTIONARIOS
                //AUDITORIA
                int Q1E = 0;
                if (Q1_OKOPT.Checked == true)
                { Q1E = 2; }
                else if (Q1_NOKOPT.Checked == true)
                { Q1E = 1; }
                else
                { Q1E = 0; }

                int Q2E = 0;
                if (Q2_OKOPT.Checked == true)
                { Q2E = 2; }
                else if (Q2_NOKOPT.Checked == true)
                { Q2E = 1; }
                else
                { Q2E = 0; }

                int Q3E = 0;
                if (Q3_OKOPT.Checked == true)
                { Q3E = 2; }
                else if (Q3_NOKOPT.Checked == true)
                { Q3E = 1; }
                else
                { Q3E = 0; }

                int Q4E = 0;
                if (Q4_OKOPT.Checked == true)
                { Q4E = 2; }
                else if (Q4_NOKOPT.Checked == true)
                { Q4E = 1; }
                else
                { Q4E = 0; }

                int Q5E = 0;
                if (Q5_OKOPT.Checked == true)
                { Q5E = 2; }
                else if (Q5_NOKOPT.Checked == true)
                { Q5E = 1; }
                else
                { Q5E = 0; }

                int Q6E = 0;
                if (Q6_OKOPT.Checked == true)
                { Q6E = 2; }
                else if (Q6_NOKOPT.Checked == true)
                { Q6E = 1; }
                else
                { Q6E = 0; }

                int Q7E = 0;
                if (Q7_OKOPT.Checked == true)
                { Q7E = 2; }
                else if (Q7_NOKOPT.Checked == true)
                { Q7E = 1; }
                else
                { Q7E = 0; }

                int Q8E = 0;
                if (Q8_OKOPT.Checked == true)
                { Q8E = 2; }
                else if (Q8_NOKOPT.Checked == true)
                { Q8E = 1; }
                else
                { Q8E = 0; }
                int Q9E = 0;
                if (Q9_OKOPT.Checked == true)
                { Q9E = 2; }
                else if (Q9_NOKOPT.Checked == true)
                { Q9E = 1; }
                else
                { Q9E = 0; }
                /*int Q10E = 0;
                int Q1C = 0;
                int Q2C = 0;
                int Q3C = 0;
                int Q4C = 0;
                int Q5C = 0;*/
                int Q6C = 0;
                if (Q6C_OKOPT.Checked == true)
                { Q6C = 2; }
                else if (Q6C_NOKOPT.Checked == true)
                { Q6C = 1; }
                else
                { Q6C = 0; }
                int Q7C = 0;
                if (Q7C_OKOPT.Checked == true)
                { Q7C = 2; }
                else if (Q7C_NOKOPT.Checked == true)
                { Q7C = 1; }
                else
                { Q7C = 0; }
                int Q8C = 0;
                if (Q8C_OKOPT.Checked == true)
                { Q8C = 2; }
                else if (Q8C_NOKOPT.Checked == true)
                { Q8C = 1; }
                else
                { Q8C = 0; }
                int Q9C = 0;
                if (Q9C_OKOPT.Checked == true)
                { Q9C = 2; }
                else if (Q9C_NOKOPT.Checked == true)
                { Q9C = 1; }
                else
                { Q9C = 0; }
                int Q10C = 0;
                if (Q10C_OKOPT.Checked == true)
                { Q10C = 2; }
                else if (Q10C_NOKOPT.Checked == true)
                { Q10C = 1; }
                else
                { Q10C = 0; }

                int ResultadoLOTES = 0;
                int ResultadoPARAM = 0;
                if ((MAT1REF.Visible == true && MAT1LOT.Text == "") || (MAT2REF.Visible == true && MAT2LOT.Text == "") || (MAT3REF.Visible == true && MAT3LOT.Text == "") || (COMP1REF.Visible == true && COMP1LOT.Text == "") || (COMP2REF.Visible == true && COMP2LOT.Text == "") || (COMP3REF.Visible == true && COMP3LOT.Text == "") || (COMP4REF.Visible == true && COMP4LOT.Text == "") || (COMP5REF.Visible == true && COMP5LOT.Text == "") || (COMP6REF.Visible == true && COMP6LOT.Text == "") || (COMP7REF.Visible == true && COMP7LOT.Text == ""))
                {
                    ResultadoLOTES = 1;
                }
                //if (PARAMSESTADO.Text != "")
                //{
                //    ResultadoPARAM = 1;
                //}

                String MAT1LOTE = MAT1LOT.Text + "|" + MAT1LOT2.Text;
                String MAT2LOTE = MAT2LOT.Text + "|" + MAT2LOT2.Text;
                String MAT3LOTE = MAT3LOT.Text + "|" + MAT3LOT2.Text;
                String COMP1LOTE = COMP1LOT.Text + "|" + COMP1LOT2.Text;
                String COMP2LOTE = COMP2LOT.Text + "|" + COMP2LOT2.Text;
                String COMP3LOTE = COMP3LOT.Text + "|" + COMP3LOT2.Text;
                String COMP4LOTE = COMP4LOT.Text + "|" + COMP4LOT2.Text;
                String COMP5LOTE = COMP5LOT.Text + "|" + COMP5LOT2.Text;
                String COMP6LOTE = COMP6LOT.Text + "|" + COMP6LOT2.Text;
                String COMP7LOTE = COMP7LOT.Text + "|" + COMP7LOT2.Text;

                Conexion_LIBERACIONES conexion = new Conexion_LIBERACIONES();
                conexion.ActualizarLiberacionFichaCambiador(tbOrden.Text, Convert.ToInt32(tbReferencia.Text), tbMaquina.Text, version, Convert.ToInt32(tbMolde.Text), operario1numero, operario1horas, Convert.ToString(Operario1Nivel.SelectedValue), Operario1UltRevision.Text, Operario1Notas.Text, operario2numero, operario2horas, Convert.ToString(Operario2Nivel.SelectedValue),
                       Operario2UltRevision.Text, Operario2Notas.Text, encargadonumero, encargadohoras, cambiadornumero, cambiadorhoras, calidadnumero, calidadhoras, CambiadorLiberado, LiberacionCambiadorHora.Text, ProduccionLiberado, LiberacionEncargadoHora.Text, CalidadLiberado, LiberacionCalidadHora.Text, ResultadoLiberacion, Convert.ToInt32(LiberacionCondicionada.SelectedValue), NotaLiberacionCondicionada.Text, Reliberacion, NCEncargado, GP12Encargado, NCCalidad, GP12Calidad, QUERYLiberarCambiadorHoraORI, ResultadoLOTES, ResultadoPARAM,
                       //VALORES MATERIALES
                       MAT1REF.Text, MAT1NOM.Text, MAT1LOTE, MAT1TIEMP_double, MAT1TIEMPREAL_double, MAT1TEMP_double, MAT1TEMPREAL_double, MAT2REF.Text, MAT2NOM.Text, MAT2LOTE, MAT2TIEMP_double, MAT2TIEMPREAL_double, MAT2TEMP_double, MAT2TEMPREAL_double, MAT3REF.Text, MAT3NOM.Text, MAT3LOTE, MAT3TIEMP_double, MAT3TIEMPREAL_double, MAT3TEMP_double, MAT3TEMPREAL_double,
                       COMP1REF.Text, COMP1NOM.Text, COMP1LOTE, COMP2REF.Text, COMP2NOM.Text, COMP2LOTE, COMP3REF.Text, COMP3NOM.Text, COMP3LOTE, COMP4REF.Text, COMP4NOM.Text, COMP4LOTE, COMP5REF.Text, COMP5NOM.Text, COMP5LOTE, COMP6REF.Text, COMP6NOM.Text, COMP6LOTE, COMP7REF.Text, COMP7NOM.Text, COMP7LOTE,
                       //VALORES AUDITORIA
                       Q1E, TbQ1ENC.Text, Q2E, TbQ2ENC.Text, Q3E, TbQ3ENC.Text, Q4E, TbQ4ENC.Text, Q5E, TbQ5ENC.Text, Q6E, Q6C, TbQ6ENC.Text, TbQ6CAL.Text,
                       Q7E, Q7C, TbQ7ENC.Text, TbQ7CAL.Text, Q8E, Q8C, TbQ8ENC.Text, TbQ8CAL.Text, Q9E, Q9C, TbQ9ENC.Text, TbQ9CAL.Text, Q10C, TbQ10CAL.Text, QXFeedbackCambiador.Text, QXFeedbackProduccion.Text, QXFeedbackCalidad.Text
                       );

                
            }
            catch (Exception) { }
        }
        public void LiberarCalidadFunction(string QUERYLiberarCalidadHoraORI)
        {
            try
            {
                //FICHA DE LIBERACION
                int version = 0;

                int operario1numero = 0;
                if (Operario1Numero.Text != "")
                { operario1numero = Convert.ToInt32(Operario1Numero.Text); }

                double operario1horas = 0;
                if (Operario1Horas.Text != "")
                { operario1horas = Convert.ToDouble(Operario1Horas.Text); }

                int operario2numero = 0;
                if (Operario2Numero.Text != "")
                { operario2numero = Convert.ToInt32(Operario2Numero.Text); }
                double operario2horas = 0;
                if (Operario2Horas.Text != "")
                { operario2horas = Convert.ToDouble(Operario2Horas.Text); }

                int encargadonumero = 0;
                if (EncargadoNumero.Text != "")
                { encargadonumero = Convert.ToInt32(EncargadoNumero.Text); }
                double encargadohoras = 0;
                if (EncargadoHoras.Text != "")
                { encargadohoras = Convert.ToDouble(EncargadoHoras.Text); }

                int cambiadornumero = 0;
                if (CambiadorNumero.Text != "")
                { cambiadornumero = Convert.ToInt32(CambiadorNumero.Text); }
                double cambiadorhoras = 0;
                if (CambiadorHoras.Text != "")
                { cambiadorhoras = Convert.ToDouble(CambiadorHoras.Text); }

                int calidadnumero = 0;
                if (CalidadNumero.Text != "")
                { calidadnumero = Convert.ToInt32(CalidadNumero.Text); }

                double calidadhoras = 0;
                if (CalidadHoras.Text != "")
                { calidadhoras = Convert.ToDouble(CalidadHoras.Text); }

                int CambiadorLiberado = 0;

                if (LiberacionCambiadorHoraORI.Text != "" && (Q1_NOKOPT.Checked == true || Q2_NOKOPT.Checked == true || Q3_NOKOPT.Checked == true || (MAT1REF.Visible == true && MAT1LOT.Text == "") || (MAT2REF.Visible == true && MAT2LOT.Text == "") || (MAT3REF.Visible == true && MAT3LOT.Text == "") || (COMP1REF.Visible == true && COMP1LOT.Text == "") || (COMP2REF.Visible == true && COMP2LOT.Text == "") || (COMP3REF.Visible == true && COMP3LOT.Text == "") || (COMP4REF.Visible == true && COMP4LOT.Text == "") || (COMP5REF.Visible == true && COMP5LOT.Text == "") || (COMP6REF.Visible == true && COMP6LOT.Text == "") || (COMP7REF.Visible == true && COMP7LOT.Text == "")))
                {
                    CambiadorLiberado = 1;
                }
                else if (LiberacionCambiadorHoraORI.Text != "" && (Q1_NOKOPT.Checked == false && Q2_NOKOPT.Checked == false && Q3_NOKOPT.Checked == false))
                {
                    CambiadorLiberado = 2;
                }

                int ProduccionLiberado = 0;
                //
                if (estadoencargadoCOND.Visible == true)
                {
                    ProduccionLiberado = 1;
                }
                else if (estadoencargadoLIBOK.Visible == true)
                {
                    ProduccionLiberado = 2;
                }
                /*
                if (LiberacionEncargadoHora.Text != "" && (Q4_NOKOPT.Checked == true || Q5_NOKOPT.Checked == true || Q6_NOKOPT.Checked == true || Q7_NOKOPT.Checked == true || Q8_NOKOPT.Checked == true || Q9_NOKOPT.Checked == true || A3.Visible == true || A5.Visible == true))
                {
                    ProduccionLiberado = 1;
                }
                else if (LiberacionEncargadoHora.Text != "" && (Q4_NOKOPT.Checked == false && Q5_NOKOPT.Checked == false && Q6_NOKOPT.Checked == false && Q7_NOKOPT.Checked == false && Q8_NOKOPT.Checked == false && Q9_NOKOPT.Checked == false))
                {
                    ProduccionLiberado = 2;
                }
                */
                int CalidadLiberado = 0;
                if (LiberacionCalidadHoraORI.Text != "" && (Q10C_NOKOPT.Checked == true || Q6C_NOKOPT.Checked == true || Q7C_NOKOPT.Checked == true || Q8C_NOKOPT.Checked == true || Q9C_NOKOPT.Checked == true || A7.Visible == true || A7.Visible == true || (alertaoperario.Visible == true) || (Convert.ToInt32(EXISTEFICHA.Text) == 1)))
                {
                    CalidadLiberado = 1;
                }
                else if (LiberacionCalidadHoraORI.Text != "" && (Q10C_NOKOPT.Checked == false && Q6C_NOKOPT.Checked == false && Q7C_NOKOPT.Checked == false && Q8C_NOKOPT.Checked == false && Q9C_NOKOPT.Checked == false))
                {
                    CalidadLiberado = 2;
                }

            
                /*
                if (LiberacionCalidadHora.Text != "" && (Q10C_NOKOPT.Checked == true || Q6C_NOKOPT.Checked == true || Q7C_NOKOPT.Checked == true || Q8C_NOKOPT.Checked == true || Q9C_NOKOPT.Checked == true || A7.Visible == true || A7.Visible == true))
                {
                    CalidadLiberado = 1;
                }
                else if (LiberacionCalidadHora.Text != "" && (Q10C_NOKOPT.Checked == false && Q6C_NOKOPT.Checked == false && Q7C_NOKOPT.Checked == false && Q8C_NOKOPT.Checked == false && Q9C_NOKOPT.Checked == false))
                {
                    CalidadLiberado = 2;
                }
                */
                int ResultadoLiberacion = 0;
                int Reliberacion = 0;


                int NCEncargado = 0;
                if (A3OK.Visible == true)
                { NCEncargado = 1; }
                int GP12Encargado = 0;
                if (A5OK.Visible == true)
                { GP12Encargado = 1; }
                int NCCalidad = 0;
                if (A7OK.Visible == true)
                { NCCalidad = 1; }
                int GP12Calidad = 0;
                if (A8OK.Visible == true)
                { GP12Calidad = 1; }

                //MATERIALES
                double MAT1TIEMP_double = 0;
                if (Double.TryParse(MAT1TIEMP.Text.Replace('.', ','), out MAT1TIEMP_double))
                    MAT1TIEMP_double = Convert.ToDouble(MAT1TIEMP.Text.Replace('.', ','));
                else
                    MAT1TIEMP_double = 0.0;


                double MAT1TIEMPREAL_double = 0;
                if (Double.TryParse(MAT1TIEMPREAL.Text.Replace('.', ','), out MAT1TIEMPREAL_double))
                    MAT1TIEMPREAL_double = Convert.ToDouble(MAT1TIEMPREAL.Text.Replace('.', ','));
                else
                    MAT1TIEMPREAL_double = 0.0;
                double MAT1TEMP_double = 0;
                if (Double.TryParse(MAT1TEMP.Text.Replace('.', ','), out MAT1TEMP_double))
                    MAT1TEMP_double = Convert.ToDouble(MAT1TEMP.Text.Replace('.', ','));
                else
                    MAT1TEMP_double = 0.0;
                double MAT1TEMPREAL_double = 0;
                if (Double.TryParse(MAT1TEMPREAL.Text.Replace('.', ','), out MAT1TEMPREAL_double))
                    MAT1TEMPREAL_double = Convert.ToDouble(MAT1TEMPREAL.Text.Replace('.', ','));
                else
                    MAT1TEMPREAL_double = 0.0;
                double MAT2TIEMP_double = 0;
                if (Double.TryParse(MAT2TIEMP.Text.Replace('.', ','), out MAT2TIEMP_double))
                    MAT2TIEMP_double = Convert.ToDouble(MAT2TIEMP.Text.Replace('.', ','));
                else
                    MAT2TIEMP_double = 0.0;
                double MAT2TIEMPREAL_double = 0;
                if (Double.TryParse(MAT2TIEMPREAL.Text.Replace('.', ','), out MAT2TIEMPREAL_double))
                    MAT2TIEMPREAL_double = Convert.ToDouble(MAT2TIEMPREAL.Text.Replace('.', ','));
                else
                    MAT2TIEMPREAL_double = 0.0;
                double MAT2TEMP_double = 0;
                if (Double.TryParse(MAT2TEMP.Text.Replace('.', ','), out MAT2TEMP_double))
                    MAT2TEMP_double = Convert.ToDouble(MAT2TEMP.Text.Replace('.', ','));
                else
                    MAT2TEMP_double = 0.0;
                double MAT2TEMPREAL_double = 0;
                if (Double.TryParse(MAT2TEMPREAL.Text.Replace('.', ','), out MAT2TEMPREAL_double))
                    MAT2TEMPREAL_double = Convert.ToDouble(MAT2TEMPREAL.Text.Replace('.', ','));
                else
                    MAT2TEMPREAL_double = 0.0;

                double MAT3TIEMP_double = 0;
                if (Double.TryParse(MAT3TIEMP.Text.Replace('.', ','), out MAT3TIEMP_double))
                    MAT3TIEMP_double = Convert.ToDouble(MAT3TIEMP.Text.Replace('.', ','));
                else
                    MAT3TIEMP_double = 0.0;
                double MAT3TIEMPREAL_double = 0;
                if (Double.TryParse(MAT3TIEMPREAL.Text.Replace('.', ','), out MAT3TIEMPREAL_double))
                    MAT3TIEMPREAL_double = Convert.ToDouble(MAT3TIEMPREAL.Text.Replace('.', ','));
                else
                    MAT3TIEMPREAL_double = 0.0;
                double MAT3TEMP_double = 0;
                if (Double.TryParse(MAT3TEMP.Text.Replace('.', ','), out MAT3TEMP_double))
                    MAT3TEMP_double = Convert.ToDouble(MAT3TEMP.Text.Replace('.', ','));
                else
                    MAT3TEMP_double = 0.0;
                double MAT3TEMPREAL_double = 0;
                if (Double.TryParse(MAT3TEMPREAL.Text.Replace('.', ','), out MAT3TEMPREAL_double))
                    MAT3TEMPREAL_double = Convert.ToDouble(MAT3TEMPREAL.Text.Replace('.', ','));
                else
                    MAT3TEMPREAL_double = 0.0;

                //CUESTIONARIOS
                //AUDITORIA
                int Q1E = 0;
                if (Q1_OKOPT.Checked == true)
                { Q1E = 2; }
                else if (Q1_NOKOPT.Checked == true)
                { Q1E = 1; }
                else
                { Q1E = 0; }

                int Q2E = 0;
                if (Q2_OKOPT.Checked == true)
                { Q2E = 2; }
                else if (Q2_NOKOPT.Checked == true)
                { Q2E = 1; }
                else
                { Q2E = 0; }

                int Q3E = 0;
                if (Q3_OKOPT.Checked == true)
                { Q3E = 2; }
                else if (Q3_NOKOPT.Checked == true)
                { Q3E = 1; }
                else
                { Q3E = 0; }

                int Q4E = 0;
                if (Q4_OKOPT.Checked == true)
                { Q4E = 2; }
                else if (Q4_NOKOPT.Checked == true)
                { Q4E = 1; }
                else
                { Q4E = 0; }

                int Q5E = 0;
                if (Q5_OKOPT.Checked == true)
                { Q5E = 2; }
                else if (Q5_NOKOPT.Checked == true)
                { Q5E = 1; }
                else
                { Q5E = 0; }

                int Q6E = 0;
                if (Q6_OKOPT.Checked == true)
                { Q6E = 2; }
                else if (Q6_NOKOPT.Checked == true)
                { Q6E = 1; }
                else
                { Q6E = 0; }

                int Q7E = 0;
                if (Q7_OKOPT.Checked == true)
                { Q7E = 2; }
                else if (Q7_NOKOPT.Checked == true)
                { Q7E = 1; }
                else
                { Q7E = 0; }

                int Q8E = 0;
                if (Q8_OKOPT.Checked == true)
                { Q8E = 2; }
                else if (Q8_NOKOPT.Checked == true)
                { Q8E = 1; }
                else
                { Q8E = 0; }
                int Q9E = 0;
                if (Q9_OKOPT.Checked == true)
                { Q9E = 2; }
                else if (Q9_NOKOPT.Checked == true)
                { Q9E = 1; }
                else
                { Q9E = 0; }
                /*int Q10E = 0;
                int Q1C = 0;
                int Q2C = 0;
                int Q3C = 0;
                int Q4C = 0;
                int Q5C = 0;*/
                int Q6C = 0;
                if (Q6C_OKOPT.Checked == true)
                { Q6C = 2; }
                else if (Q6C_NOKOPT.Checked == true)
                { Q6C = 1; }
                else
                { Q6C = 0; }
                int Q7C = 0;
                if (Q7C_OKOPT.Checked == true)
                { Q7C = 2; }
                else if (Q7C_NOKOPT.Checked == true)
                { Q7C = 1; }
                else
                { Q7C = 0; }
                int Q8C = 0;
                if (Q8C_OKOPT.Checked == true)
                { Q8C = 2; }
                else if (Q8C_NOKOPT.Checked == true)
                { Q8C = 1; }
                else
                { Q8C = 0; }
                int Q9C = 0;
                if (Q9C_OKOPT.Checked == true)
                { Q9C = 2; }
                else if (Q9C_NOKOPT.Checked == true)
                { Q9C = 1; }
                else
                { Q9C = 0; }
                int Q10C = 0;
                if (Q10C_OKOPT.Checked == true)
                { Q10C = 2; }
                else if (Q10C_NOKOPT.Checked == true)
                { Q10C = 1; }
                else
                { Q10C = 0; }

                int ResultadoLOTES = 0;
                int ResultadoPARAM = 0;
                if ((MAT1REF.Visible == true && MAT1LOT.Text == "") || (MAT2REF.Visible == true && MAT2LOT.Text == "") || (MAT3REF.Visible == true && MAT3LOT.Text == "") || (COMP1REF.Visible == true && COMP1LOT.Text == "") || (COMP2REF.Visible == true && COMP2LOT.Text == "") || (COMP3REF.Visible == true && COMP3LOT.Text == "") || (COMP4REF.Visible == true && COMP4LOT.Text == "") || (COMP5REF.Visible == true && COMP5LOT.Text == "") || (COMP6REF.Visible == true && COMP6LOT.Text == "") || (COMP7REF.Visible == true && COMP7LOT.Text == ""))
                {
                    ResultadoLOTES = 1;
                }
                if (PARAMSESTADO.Text != "")
                {
                    ResultadoPARAM = 1;
                }

                //MONTAR STRING MATERIALES
                String MAT1LOTE = MAT1LOT.Text + "|" + MAT1LOT2.Text;
                String MAT2LOTE = MAT2LOT.Text + "|" + MAT2LOT2.Text;
                String MAT3LOTE = MAT3LOT.Text + "|" + MAT3LOT2.Text;
                String COMP1LOTE = COMP1LOT.Text + "|" + COMP1LOT2.Text;
                String COMP2LOTE = COMP2LOT.Text + "|" + COMP2LOT2.Text;
                String COMP3LOTE = COMP3LOT.Text + "|" + COMP3LOT2.Text;
                String COMP4LOTE = COMP4LOT.Text + "|" + COMP4LOT2.Text;
                String COMP5LOTE = COMP5LOT.Text + "|" + COMP5LOT2.Text;
                String COMP6LOTE = COMP6LOT.Text + "|" + COMP6LOT2.Text;
                String COMP7LOTE = COMP7LOT.Text + "|" + COMP7LOT2.Text;


                Conexion_LIBERACIONES conexion = new Conexion_LIBERACIONES();
                conexion.ActualizarLiberacionFichaCambiador(tbOrden.Text, Convert.ToInt32(tbReferencia.Text), tbMaquina.Text, version, Convert.ToInt32(tbMolde.Text), operario1numero, operario1horas, Convert.ToString(Operario1Nivel.SelectedValue), Operario1UltRevision.Text, Operario1Notas.Text, operario2numero, operario2horas, Convert.ToString(Operario2Nivel.SelectedValue),
                       Operario2UltRevision.Text, Operario2Notas.Text, encargadonumero, encargadohoras, cambiadornumero, cambiadorhoras, calidadnumero, calidadhoras, CambiadorLiberado, LiberacionCambiadorHora.Text, ProduccionLiberado, LiberacionEncargadoHora.Text, CalidadLiberado, LiberacionCalidadHora.Text, ResultadoLiberacion, Convert.ToInt32(LiberacionCondicionada.SelectedValue), NotaLiberacionCondicionada.Text, Reliberacion, NCEncargado, GP12Encargado, NCCalidad, GP12Calidad, QUERYLiberarCalidadHoraORI, ResultadoLOTES, ResultadoPARAM,
                       //VALORES MATERIALES
                       MAT1REF.Text, MAT1NOM.Text, MAT1LOTE, MAT1TIEMP_double, MAT1TIEMPREAL_double, MAT1TEMP_double, MAT1TEMPREAL_double, MAT2REF.Text, MAT2NOM.Text, MAT2LOTE, MAT2TIEMP_double, MAT2TIEMPREAL_double, MAT2TEMP_double, MAT2TEMPREAL_double, MAT3REF.Text, MAT3NOM.Text, MAT3LOTE, MAT3TIEMP_double, MAT3TIEMPREAL_double, MAT3TEMP_double, MAT3TEMPREAL_double,
                       COMP1REF.Text, COMP1NOM.Text, COMP1LOTE, COMP2REF.Text, COMP2NOM.Text, COMP2LOTE, COMP3REF.Text, COMP3NOM.Text, COMP3LOTE, COMP4REF.Text, COMP4NOM.Text, COMP4LOTE, COMP5REF.Text, COMP5NOM.Text, COMP5LOTE, COMP6REF.Text, COMP6NOM.Text, COMP6LOTE, COMP7REF.Text, COMP7NOM.Text, COMP7LOTE,
                       //VALORES AUDITORIA
                       Q1E, TbQ1ENC.Text, Q2E, TbQ2ENC.Text, Q3E, TbQ3ENC.Text, Q4E, TbQ4ENC.Text, Q5E, TbQ5ENC.Text, Q6E, Q6C, TbQ6ENC.Text, TbQ6CAL.Text,
                       Q7E, Q7C, TbQ7ENC.Text, TbQ7CAL.Text, Q8E, Q8C, TbQ8ENC.Text, TbQ8CAL.Text, Q9E, Q9C, TbQ9ENC.Text, TbQ9CAL.Text, Q10C, TbQ10CAL.Text, QXFeedbackCambiador.Text, QXFeedbackProduccion.Text, QXFeedbackCalidad.Text
                       );


            }
            catch (Exception) { }
        }
        public void GuardaMaterial(Object sender, EventArgs e)
        {
            try
            {
                //FICHA DE LIBERACION
                int version = 0;

                //MATERIALES
                double MAT1TIEMP_double = 0;
                if (Double.TryParse(MAT1TIEMP.Text.Replace('.', ','), out MAT1TIEMP_double))
                    MAT1TIEMP_double = Convert.ToDouble(MAT1TIEMP.Text.Replace('.', ','));
                else
                    MAT1TIEMP_double = 0.0;

                double MAT1TIEMPREAL_double = 0;
                if (Double.TryParse(MAT1TIEMPREAL.Text.Replace('.', ','), out MAT1TIEMPREAL_double))
                    MAT1TIEMPREAL_double = Convert.ToDouble(MAT1TIEMPREAL.Text.Replace('.', ','));
                else
                    MAT1TIEMPREAL_double = 0.0;
                double MAT1TEMP_double = 0;
                if (Double.TryParse(MAT1TEMP.Text.Replace('.', ','), out MAT1TEMP_double))
                    MAT1TEMP_double = Convert.ToDouble(MAT1TEMP.Text.Replace('.', ','));
                else
                    MAT1TEMP_double = 0.0;
                double MAT1TEMPREAL_double = 0;
                if (Double.TryParse(MAT1TEMPREAL.Text.Replace('.', ','), out MAT1TEMPREAL_double))
                    MAT1TEMPREAL_double = Convert.ToDouble(MAT1TEMPREAL.Text.Replace('.', ','));
                else
                    MAT1TEMPREAL_double = 0.0;
                double MAT2TIEMP_double = 0;
                if (Double.TryParse(MAT2TIEMP.Text.Replace('.', ','), out MAT2TIEMP_double))
                    MAT2TIEMP_double = Convert.ToDouble(MAT2TIEMP.Text.Replace('.', ','));
                else
                    MAT2TIEMP_double = 0.0;
                double MAT2TIEMPREAL_double = 0;
                if (Double.TryParse(MAT2TIEMPREAL.Text.Replace('.', ','), out MAT2TIEMPREAL_double))
                    MAT2TIEMPREAL_double = Convert.ToDouble(MAT2TIEMPREAL.Text.Replace('.', ','));
                else
                    MAT2TIEMPREAL_double = 0.0;
                double MAT2TEMP_double = 0;
                if (Double.TryParse(MAT2TEMP.Text.Replace('.', ','), out MAT2TEMP_double))
                    MAT2TEMP_double = Convert.ToDouble(MAT2TEMP.Text.Replace('.', ','));
                else
                    MAT2TEMP_double = 0.0;
                double MAT2TEMPREAL_double = 0;
                if (Double.TryParse(MAT2TEMPREAL.Text.Replace('.', ','), out MAT2TEMPREAL_double))
                    MAT2TEMPREAL_double = Convert.ToDouble(MAT2TEMPREAL.Text.Replace('.', ','));
                else
                    MAT2TEMPREAL_double = 0.0;

                double MAT3TIEMP_double = 0;
                if (Double.TryParse(MAT3TIEMP.Text.Replace('.', ','), out MAT3TIEMP_double))
                    MAT3TIEMP_double = Convert.ToDouble(MAT3TIEMP.Text.Replace('.', ','));
                else
                    MAT3TIEMP_double = 0.0;
                double MAT3TIEMPREAL_double = 0;
                if (Double.TryParse(MAT3TIEMPREAL.Text.Replace('.', ','), out MAT3TIEMPREAL_double))
                    MAT3TIEMPREAL_double = Convert.ToDouble(MAT3TIEMPREAL.Text.Replace('.', ','));
                else
                    MAT3TIEMPREAL_double = 0.0;
                double MAT3TEMP_double = 0;
                if (Double.TryParse(MAT3TEMP.Text.Replace('.', ','), out MAT3TEMP_double))
                    MAT3TEMP_double = Convert.ToDouble(MAT3TEMP.Text.Replace('.', ','));
                else
                    MAT3TEMP_double = 0.0;
                double MAT3TEMPREAL_double = 0;
                if (Double.TryParse(MAT3TEMPREAL.Text.Replace('.', ','), out MAT3TEMPREAL_double))
                    MAT3TEMPREAL_double = Convert.ToDouble(MAT3TEMPREAL.Text.Replace('.', ','));
                else
                    MAT3TEMPREAL_double = 0.0;

                String MAT1LOTE = MAT1LOT.Text + "|" + MAT1LOT2.Text;
                String MAT2LOTE = MAT2LOT.Text + "|" + MAT2LOT2.Text;
                String MAT3LOTE = MAT3LOT.Text + "|" + MAT3LOT2.Text;
                String COMP1LOTE = COMP1LOT.Text + "|" + COMP1LOT2.Text;
                String COMP2LOTE = COMP2LOT.Text + "|" + COMP2LOT2.Text;
                String COMP3LOTE = COMP3LOT.Text + "|" + COMP3LOT2.Text;
                String COMP4LOTE = COMP4LOT.Text + "|" + COMP4LOT2.Text;
                String COMP5LOTE = COMP5LOT.Text + "|" + COMP5LOT2.Text;
                String COMP6LOTE = COMP6LOT.Text + "|" + COMP6LOT2.Text;
                String COMP7LOTE = COMP7LOT.Text + "|" + COMP7LOT2.Text;

                Conexion_LIBERACIONES conexion = new Conexion_LIBERACIONES();
                conexion.Actualizar_ficha_Materiales(tbOrden.Text, Convert.ToInt32(tbReferencia.Text), tbMaquina.Text, version, MAT1REF.Text, MAT1NOM.Text, MAT1LOTE, MAT1TIEMP_double, MAT1TIEMPREAL_double, MAT1TEMP_double, MAT1TEMPREAL_double, MAT2REF.Text, MAT2NOM.Text, MAT2LOTE, MAT2TIEMP_double, MAT2TIEMPREAL_double, MAT2TEMP_double, MAT2TEMPREAL_double, MAT3REF.Text, MAT3NOM.Text, MAT3LOTE, MAT3TIEMP_double, MAT3TIEMPREAL_double, MAT3TEMP_double, MAT3TEMPREAL_double,
                       COMP1REF.Text, COMP1NOM.Text, COMP1LOTE, COMP2REF.Text, COMP2NOM.Text, COMP2LOTE, COMP3REF.Text, COMP3NOM.Text, COMP3LOTE, COMP4REF.Text, COMP4NOM.Text, COMP4LOTE, COMP5REF.Text, COMP5NOM.Text, COMP5LOTE, COMP6REF.Text, COMP6NOM.Text, COMP6LOTE, COMP7REF.Text, COMP7NOM.Text, COMP7LOTE);

                CargarFichaLiberacion();
                CargarParametrosLiberacion();
                CargarCuestionariosLiberacion();
                CargarMateriasPrimasLiberacion();
                CargarResultados();
                lkb_Sort_Click("CAMBIO");


            }
            catch (Exception) 
            { 
            }
        }

        //OPCIONES DE RECARGA DE DATOS ACTUALIZADOS

        public void RecargayLimpiaParametros(Object sender, EventArgs e)
        {
            try {
                //CILINDRO
                thBoqREAL.Text = "";
                thBoqREAL.BackColor = System.Drawing.Color.Empty;
                thBoqREAL.ForeColor = System.Drawing.Color.Black;
                thT1REAL.Text = "";
                thT1REAL.BackColor = System.Drawing.Color.Empty;
                thT1REAL.ForeColor = System.Drawing.Color.Black;
                thT2REAL.Text = "";
                thT2REAL.BackColor = System.Drawing.Color.Empty;
                thT2REAL.ForeColor = System.Drawing.Color.Black;
                thT3REAL.Text = "";
                thT3REAL.BackColor = System.Drawing.Color.Empty;
                thT3REAL.ForeColor = System.Drawing.Color.Black;
                thT4REAL.Text = "";
                thT4REAL.BackColor = System.Drawing.Color.Empty;
                thT4REAL.ForeColor = System.Drawing.Color.Black;
                thT5REAL.Text = "";
                thT5REAL.BackColor = System.Drawing.Color.Empty;
                thT5REAL.ForeColor = System.Drawing.Color.Black;
                thT6REAL.Text = "";
                thT6REAL.BackColor = System.Drawing.Color.Empty;
                thT6REAL.ForeColor = System.Drawing.Color.Black;
                thT7REAL.Text = "";
                thT7REAL.BackColor = System.Drawing.Color.Empty;
                thT7REAL.ForeColor = System.Drawing.Color.Black;
                thT8REAL.Text = "";
                thT8REAL.BackColor = System.Drawing.Color.Empty;
                thT8REAL.ForeColor = System.Drawing.Color.Black;
                thT9REAL.Text = "";
                thT9REAL.BackColor = System.Drawing.Color.Empty;
                thT9REAL.ForeColor = System.Drawing.Color.Black;
                thT10REAL.Text = "";
                thT10REAL.BackColor = System.Drawing.Color.Empty;
                thT10REAL.ForeColor = System.Drawing.Color.Black;

                //CAMARA CALIENTE
                thZ1REAL.Text = "";
                thZ1REAL.BackColor = System.Drawing.Color.Empty;
                thZ1REAL.ForeColor = System.Drawing.Color.Black;
                thZ2REAL.Text = "";
                thZ2REAL.BackColor = System.Drawing.Color.Empty;
                thZ2REAL.ForeColor = System.Drawing.Color.Black;
                thZ3REAL.Text = "";
                thZ3REAL.BackColor = System.Drawing.Color.Empty;
                thZ3REAL.ForeColor = System.Drawing.Color.Black;
                thZ4REAL.Text = "";
                thZ4REAL.BackColor = System.Drawing.Color.Empty;
                thZ4REAL.ForeColor = System.Drawing.Color.Black;
                thZ5REAL.Text = "";
                thZ5REAL.BackColor = System.Drawing.Color.Empty;
                thZ5REAL.ForeColor = System.Drawing.Color.Black;
                thZ6REAL.Text = "";
                thZ6REAL.BackColor = System.Drawing.Color.Empty;
                thZ6REAL.ForeColor = System.Drawing.Color.Black;
                thZ7REAL.Text = "";
                thZ7REAL.BackColor = System.Drawing.Color.Empty;
                thZ7REAL.ForeColor = System.Drawing.Color.Black;
                thZ8REAL.Text = "";
                thZ8REAL.BackColor = System.Drawing.Color.Empty;
                thZ8REAL.ForeColor = System.Drawing.Color.Black;
                thZ9REAL.Text = "";
                thZ9REAL.BackColor = System.Drawing.Color.Empty;
                thZ9REAL.ForeColor = System.Drawing.Color.Black;
                thZ10REAL.Text = "";
                thZ10REAL.BackColor = System.Drawing.Color.Empty;
                thZ10REAL.ForeColor = System.Drawing.Color.Black;
                thZ11REAL.Text = "";
                thZ11REAL.BackColor = System.Drawing.Color.Empty;
                thZ11REAL.ForeColor = System.Drawing.Color.Black;
                thZ12REAL.Text = "";
                thZ12REAL.BackColor = System.Drawing.Color.Empty;
                thZ12REAL.ForeColor = System.Drawing.Color.Black;
                thZ13REAL.Text = "";
                thZ13REAL.BackColor = System.Drawing.Color.Empty;
                thZ13REAL.ForeColor = System.Drawing.Color.Black;
                thZ14REAL.Text = "";
                thZ14REAL.BackColor = System.Drawing.Color.Empty;
                thZ14REAL.ForeColor = System.Drawing.Color.Black;
                thZ15REAL.Text = "";
                thZ15REAL.BackColor = System.Drawing.Color.Empty;
                thZ15REAL.ForeColor = System.Drawing.Color.Black;
                thZ16REAL.Text = "";
                thZ16REAL.BackColor = System.Drawing.Color.Empty;
                thZ16REAL.ForeColor = System.Drawing.Color.Black;
                thZ17REAL.Text = "";
                thZ17REAL.BackColor = System.Drawing.Color.Empty;
                thZ17REAL.ForeColor = System.Drawing.Color.Black;
                thZ18REAL.Text = "";
                thZ18REAL.BackColor = System.Drawing.Color.Empty;
                thZ18REAL.ForeColor = System.Drawing.Color.Black;
                thZ19REAL.Text = "";
                thZ19REAL.BackColor = System.Drawing.Color.Empty;
                thZ19REAL.ForeColor = System.Drawing.Color.Black;
                thZ20REAL.Text = "";
                thZ20REAL.BackColor = System.Drawing.Color.Empty;
                thZ20REAL.ForeColor = System.Drawing.Color.Black;


                thP1R.Text = "";
                thP1R.BackColor = System.Drawing.Color.Empty;
                thP1R.ForeColor = System.Drawing.Color.Black;
                thP2R.Text = "";
                thP2R.BackColor = System.Drawing.Color.Empty;
                thP2R.ForeColor = System.Drawing.Color.Black;
                thP3R.Text = "";
                thP3R.BackColor = System.Drawing.Color.Empty;
                thP3R.ForeColor = System.Drawing.Color.Black;
                thP4R.Text = "";
                thP4R.BackColor = System.Drawing.Color.Empty;
                thP4R.ForeColor = System.Drawing.Color.Black;
                thP5R.Text = "";
                thP5R.BackColor = System.Drawing.Color.Empty;
                thP5R.ForeColor = System.Drawing.Color.Black;
                thP6R.Text = "";
                thP6R.BackColor = System.Drawing.Color.Empty;
                thP6R.ForeColor = System.Drawing.Color.Black;
                thP7R.Text = "";
                thP7R.BackColor = System.Drawing.Color.Empty;
                thP7R.ForeColor = System.Drawing.Color.Black;
                thP8R.Text = "";
                thP8R.BackColor = System.Drawing.Color.Empty;
                thP8R.ForeColor = System.Drawing.Color.Black;
                thP9R.Text = "";
                thP9R.BackColor = System.Drawing.Color.Empty;
                thP9R.ForeColor = System.Drawing.Color.Black;
                thP10R.Text = "";
                thP10R.BackColor = System.Drawing.Color.Empty;
                thP10R.ForeColor = System.Drawing.Color.Black;
                thTP1R.Text = "";
                thTP1R.BackColor = System.Drawing.Color.Empty;
                thTP1R.ForeColor = System.Drawing.Color.Black;
                thTP2R.Text = "";
                thTP2R.BackColor = System.Drawing.Color.Empty;
                thTP2R.ForeColor = System.Drawing.Color.Black;
                thTP3R.Text = "";
                thTP3R.BackColor = System.Drawing.Color.Empty;
                thTP3R.ForeColor = System.Drawing.Color.Black;
                thTP4R.Text = "";
                thTP4R.BackColor = System.Drawing.Color.Empty;
                thTP4R.ForeColor = System.Drawing.Color.Black;
                thTP5R.Text = "";
                thTP5R.BackColor = System.Drawing.Color.Empty;
                thTP5R.ForeColor = System.Drawing.Color.Black;
                thTP6R.Text = "";
                thTP6R.BackColor = System.Drawing.Color.Empty;
                thTP6R.ForeColor = System.Drawing.Color.Black;
                thTP7R.Text = "";
                thTP7R.BackColor = System.Drawing.Color.Empty;
                thTP7R.ForeColor = System.Drawing.Color.Black;
                thTP8R.Text = "";
                thTP8R.BackColor = System.Drawing.Color.Empty;
                thTP8R.ForeColor = System.Drawing.Color.Black;
                thTP9R.Text = "";
                thTP9R.BackColor = System.Drawing.Color.Empty;
                thTP9R.ForeColor = System.Drawing.Color.Black;
                thTP10R.Text = "";
                thTP10R.BackColor = System.Drawing.Color.Empty;
                thTP10R.ForeColor = System.Drawing.Color.Black;
                tbConmutacionREAL.Text = "";
                tbConmutacionREAL.BackColor = System.Drawing.Color.Empty;
                tbConmutacionREAL.ForeColor = System.Drawing.Color.Black;
                tbTiempoPresionREAL.Text = "";
                tbTiempoPresionREAL.BackColor = System.Drawing.Color.Empty;
                tbTiempoPresionREAL.ForeColor = System.Drawing.Color.Black;

                TbCaudalF1REAL.Text = "";
                TbCaudalF2REAL.Text = "";
                TbCaudalF3REAL.Text = "";
                TbCaudalF4REAL.Text = "";
                TbCaudalF5REAL.Text = "";
                TbCaudalF6REAL.Text = "";
                //PARTE MOVIL LIBERADOS
                TbCaudalM1REAL.Text = "";
                TbCaudalM2REAL.Text = "";
                TbCaudalM3REAL.Text = "";
                TbCaudalM4REAL.Text = "";
                TbCaudalM5REAL.Text = "";
                TbCaudalM6REAL.Text = "";

                //TEMPERATURAS LIBERADOS
                //PARTE FIJA LIBERADOS
                TbTemperaturaF1REAL.Text = "";
                TbTemperaturaF2REAL.Text = "";
                TbTemperaturaF3REAL.Text = "";
                TbTemperaturaF4REAL.Text = "";
                TbTemperaturaF5REAL.Text = "";
                TbTemperaturaF6REAL.Text = "";
                //PARTE MOVIL LREALIBERADOS
                TbTemperaturaM1REAL.Text = "";
                TbTemperaturaM2REAL.Text = "";
                TbTemperaturaM3REAL.Text = "";
                TbTemperaturaM4REAL.Text = "";
                TbTemperaturaM5REAL.Text = "";
                TbTemperaturaM6REAL.Text = "";

                tbTiempoInyeccionREAL.Text = "";
                tbTiempoInyeccionREAL.BackColor = System.Drawing.Color.Empty;
                tbTiempoInyeccionREAL.ForeColor = System.Drawing.Color.Black;
                tbLimitePresionREAL.Text = "";
                tbLimitePresionREAL.BackColor = System.Drawing.Color.Empty;
                tbLimitePresionREAL.ForeColor = System.Drawing.Color.Black;
                thVCargaREAL.Text = "";
                thVCargaREAL.BackColor = System.Drawing.Color.Empty;
                thVCargaREAL.ForeColor = System.Drawing.Color.Black;
                thCargaREAL.Text = "";
                thCargaREAL.BackColor = System.Drawing.Color.Empty;
                thCargaREAL.ForeColor = System.Drawing.Color.Black;
                thDescompREAL.Text = "";
                thDescompREAL.BackColor = System.Drawing.Color.Empty;
                thDescompREAL.ForeColor = System.Drawing.Color.Black;
                thContraprREAL.Text = "";
                thContraprREAL.BackColor = System.Drawing.Color.Empty;
                thContraprREAL.ForeColor = System.Drawing.Color.Black;
                thTiempoREAL.Text = "";
                thTiempoREAL.BackColor = System.Drawing.Color.Empty;
                thTiempoREAL.ForeColor = System.Drawing.Color.Black;
                thEnfriamientoREAL.Text = "";
                thEnfriamientoREAL.BackColor = System.Drawing.Color.Empty;
                thEnfriamientoREAL.ForeColor = System.Drawing.Color.Black;
                thCicloREAL.Text = "";
                thCicloREAL.BackColor = System.Drawing.Color.Empty;
                thCicloREAL.ForeColor = System.Drawing.Color.Black;
                thCojinREAL.Text = "";
                thCojinREAL.BackColor = System.Drawing.Color.Empty;
                thCojinREAL.ForeColor = System.Drawing.Color.Black;
                CargarParametros();
                lkb_Sort_Click("PARAMETROS");
            }
            catch (Exception)
            { }
        }
        public void ReCargarCambiador(Object sender, EventArgs e)
        {
            try
            {
                Conexion_LIBERACIONES conexion = new Conexion_LIBERACIONES();
                DataSet cambiador = conexion.devuelve_cambiador_logueadoXMaquina(tbMaquina.Text);
                if (cambiador.Tables[0].Rows.Count > 0)
                {
                    CambiadorNumero.Text = cambiador.Tables[0].Rows[0]["C_CLOCKNO"].ToString();
                    CambiadorNombre.Text = cambiador.Tables[0].Rows[0]["C_NAME"].ToString();
                    cambiador = conexion.devuelve_horasXReferenciaXOperario(Convert.ToInt32(tbMolde.Text), Convert.ToInt32(CambiadorNumero.Text));
                    if (cambiador.Tables[0].Rows.Count > 0)
                    { CambiadorHoras.Text = cambiador.Tables[0].Rows[0]["TIEMPOHORAS"].ToString(); }
                    else
                    { CambiadorHoras.Text = "0"; }
                }
                lkb_Sort_Click("PORTADA");
            }
            catch (Exception)
            { }

        }
        public void ReCargarCalidad(Object sender, EventArgs e)
        {
            try
            {
                Conexion_LIBERACIONES conexion = new Conexion_LIBERACIONES();
                DataSet calidadplanta = conexion.devuelve_calidadplanta_logueadoXMaquina(tbMaquina.Text);
                if (calidadplanta.Tables[0].Rows.Count > 0)
                {
                    CalidadNumero.Text = calidadplanta.Tables[0].Rows[0]["C_CLOCKNO"].ToString();
                    CalidadNombre.Text = calidadplanta.Tables[0].Rows[0]["C_NAME"].ToString();
                    calidadplanta = conexion.devuelve_horasXReferenciaXOperario(Convert.ToInt32(tbMolde.Text), Convert.ToInt32(CalidadNumero.Text));
                    if (calidadplanta.Tables[0].Rows.Count > 0)
                    { CalidadHoras.Text = calidadplanta.Tables[0].Rows[0]["TIEMPOHORAS"].ToString(); }
                    else
                    { CalidadHoras.Text = "0"; }
                }
                lkb_Sort_Click("PORTADA");

            }
            catch (Exception)
            { }

        }
        public void ReCargarEncargado(Object sender, EventArgs e)
        {
            try
            {
                Conexion_LIBERACIONES conexion = new Conexion_LIBERACIONES();
                DataSet encargado = conexion.devuelve_encargado_logueadoXMaquina(tbMaquina.Text);
                if (encargado.Tables[0].Rows.Count > 0)
                {
                    EncargadoNumero.Text = encargado.Tables[0].Rows[0]["C_CLOCKNO"].ToString();
                    EncargadoNombre.Text = encargado.Tables[0].Rows[0]["C_NAME"].ToString();
                    encargado = conexion.devuelve_horasXReferenciaXOperario(Convert.ToInt32(tbMolde.Text), Convert.ToInt32(EncargadoNumero.Text));
                    if (encargado.Tables[0].Rows.Count > 0)
                    { EncargadoHoras.Text = encargado.Tables[0].Rows[0]["TIEMPOHORAS"].ToString(); }
                    else
                    { EncargadoHoras.Text = "0"; }
                }
                lkb_Sort_Click("PORTADA");
            }
            catch (Exception)
            { }

        }
        public void ReCargarOperarios(Object sender, EventArgs e)
        {
            try
            {
                alertaoperario.Visible = false;
                Operario2Posicion.Visible = false;
                Operario2Nivel.Visible = false;
                Operario2Horas.Visible = false;
                Operario2Nombre.Visible = false;
                Operario2UltRevision.Visible = false;
                Operario2Numero.Visible = false;
                Operario2Notas.Visible = false;
                Operario2Numero.Text = "";
                Operario2Nombre.Text = "";
                Operario2Horas.Text = "";


                Conexion_LIBERACIONES conexion = new Conexion_LIBERACIONES();
                DataSet operario = conexion.devuelve_operario_logueadoXMaquina(tbMaquina.Text);
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
                        Operario2UltRevision.Visible = true;
                        Operario2Numero.Visible = true;
                        Operario2Notas.Visible = true;
                    }

                    operario = conexion.devuelve_horasXReferenciaXOperario(Convert.ToInt32(tbMolde.Text), Convert.ToInt32(Operario1Numero.Text));
                    if (operario.Tables[0].Rows.Count > 0)
                    {
                        Operario1Horas.Text = operario.Tables[0].Rows[0]["TIEMPOHORAS"].ToString();
                        //compruebo horas y asigno valor (revincular a aplicación)
                        double DoubleOperarioHoras = Convert.ToDouble(Operario1Horas.Text);
                        if (DoubleOperarioHoras < 10)
                        {
                            Operario1Nivel.SelectedValue = "I";
                            alertaoperario.Visible = true;
                        }
                        if (DoubleOperarioHoras > 10 && DoubleOperarioHoras < 80)
                        { Operario1Nivel.SelectedValue = "L"; }
                        if (DoubleOperarioHoras > 80)
                        { Operario1Nivel.SelectedValue = "U"; }
                    }
                    else
                    {
                        Operario1Horas.Text = "0";
                        alertaoperario.Visible = true;
                    }

                    if (Operario2Posicion.Visible == true)
                    {
                        DataSet Operario2 = conexion.devuelve_horasXReferenciaXOperario(Convert.ToInt32(tbMolde.Text), Convert.ToInt32(Operario2Numero.Text));
                        if (Operario2.Tables[0].Rows.Count > 0)
                        {
                            Operario2Horas.Text = Operario2.Tables[0].Rows[0]["TIEMPOHORAS"].ToString();
                            double DoubleOperario2Horas = Convert.ToDouble(Operario2Horas.Text);
                            if (DoubleOperario2Horas < 10)
                            {
                                Operario2Nivel.SelectedValue = "I";
                                alertaoperario.Visible = true;
                            }
                            if (DoubleOperario2Horas > 10 && DoubleOperario2Horas < 80)
                            { Operario1Nivel.SelectedValue = "L"; }
                            if (DoubleOperario2Horas > 80)
                            { Operario1Nivel.SelectedValue = "U"; }
                        }
                        else
                        { Operario2Horas.Text = "0"; }
                    }
                    lkb_Sort_Click("PORTADA");
                }
            }
            catch (Exception)
            { }

        }

        //REDIRECCIONES DE BOTONES AUXILIARES
        public void redireccionadetalleGP12(Object sender, EventArgs e)
        {
            try
            {
                string tab = "";
                HtmlButton button = (HtmlButton)sender;

                button.Visible = false;
                string indice = button.ClientID.ToString();
                string URL = "http://facts4-srv/thermogestion/DOCUMENTAL/DocumentosLiberacionGP12.aspx?REFERENCIA=" + tbReferencia.Text;
                switch (indice)
                {
                    case "A3":
                        A3OK.Visible = true;
                        
                        tab = "PROCESO";
                        break;
                    case "A5":
                        A5OK.Visible = true;
                        tab = "PROCESO";
                        break;
                    case "A7":
                        A7OK.Visible = true;
                        
                        tab = "CALIDAD";
                        break;
                    case "A8":
                        A8OK.Visible = true;
                        tab = "CALIDAD";
                        break;
                }

                LiberarCambiadorFunction("");
                CargarFichaLiberacion();
                CargarParametrosLiberacion();
                CargarCuestionariosLiberacion();
                CargarMateriasPrimasLiberacion();
                PintarCeldasDevacion();
                CargarResultados();
                Response.Write("<script language='javascript'> window.open('" + URL + "', 'window','HEIGHT=600,WIDTH=820,top=50,left=50,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
                lkb_Sort_Click(tab);

            }
            catch (Exception)
            { }
        }
        public void redireccionadetalleNC(Object sender, EventArgs e)
        {
            try
            {
                string tab = "";
                HtmlButton button = (HtmlButton)sender;

                button.Visible = false;
                string indice = button.ClientID.ToString();
                string URL = "http://facts4-srv/thermogestion/calidad/Alertas_Calidad_LIBERACION.aspx?Molde=" + tbMolde.Text;
                switch (indice)
                {
                    case "A3":
                        A3OK.Visible = true;
                        tab = "PROCESO";
                        break;
                    case "A5":
                        A5OK.Visible = true;
                        tab = "PROCESO";
                        break;
                    case "A7":
                        A7OK.Visible = true;
                        tab = "CALIDAD";
                        break;
                    case "A8":
                        A8OK.Visible = true;
                        tab = "CALIDAD";
                        break;
                }

                LiberarCambiadorFunction("");
                CargarFichaLiberacion();
                CargarParametrosLiberacion();
                CargarCuestionariosLiberacion();
                CargarMateriasPrimasLiberacion();
                PintarCeldasDevacion();
                CargarResultados();
                Response.Write("<script language='javascript'> window.open('" + URL + "', 'window','HEIGHT=600,WIDTH=820,top=50,left=50,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
                lkb_Sort_Click(tab);
            }
            catch (Exception)
            { }
        }
        public void redireccionaGRAL(Object sender, EventArgs e)
        {
            try
            {
                string tab = "";
                HtmlButton button = (HtmlButton)sender;
                string indice = button.ClientID.ToString();
                string urlredireccion = "";
                switch (indice)
                {
                    case "BotonAbrirParte":
                        urlredireccion = "http://facts4-srv/thermogestion/MANTENIMIENTO/ReparacionMoldes.aspx?PARTE=" + tbParteMolde.Text;
                        Response.Write("<script>window.open('" + urlredireccion + "','_blank');</script>");
                        tab = "CAMBIO";
                        break;
                    case "BotonCrearParte":
                        Response.Write("<script>window.open('http://facts4-srv/thermogestion/MANTENIMIENTO/ReparacionMoldes.aspx','_blank');</script>");
                        tab = "CAMBIO";
                        break;
                    case "BotonAbrirParteMaq":
                        urlredireccion = "../MANTENIMIENTO/ReparacionMaquinas.aspx?PARTE=" + tbParteMaq.Text;
                        Response.Write("<script>window.open('" + urlredireccion + "','_blank');</script>");
                        tab = "PARAMETROS";
                        break;
                    case "BotonCrearParteMaq":
                        Response.Write("<script>window.open('../MANTENIMIENTO/ReparacionMaquinas.aspx','_blank');</script>");
                        tab = "PARAMETROS";
                        break;
                    case "A3OK":
                        //urlredireccion = "http://facts4-srv/thermogestion/DOCUMENTAL/DocumentosLiberacionNC.aspx?REFERENCIA="+tbReferencia.Text;
                        urlredireccion = "http://facts4-srv/thermogestion/calidad/Alertas_Calidad_LIBERACION.aspx?Molde=" + tbMolde.Text;
                        Response.Write("<script language='javascript'> window.open('" + urlredireccion + "', 'window','HEIGHT=600,WIDTH=820,top=50,left=50,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
                        tab = "PROCESO";
                        break;
                    case "A5OK":
                        urlredireccion = "http://facts4-srv/thermogestion/DOCUMENTAL/DocumentosLiberacionGP12.aspx?REFERENCIA=" + tbReferencia.Text;
                        Response.Write("<script language='javascript'> window.open('" + urlredireccion + "', 'window','HEIGHT=600,WIDTH=820,top=50,left=50,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
                        tab = "PROCESO";
                        break;
                    case "A7OK":
                        //urlredireccion = "http://facts4-srv/thermogestion/DOCUMENTAL/DocumentosLiberacionNC.aspx?REFERENCIA=" + tbReferencia.Text;
                        urlredireccion = "http://facts4-srv/thermogestion/calidad/Alertas_Calidad_LIBERACION.aspx?Molde=" + tbMolde.Text;
                        Response.Write("<script language='javascript'> window.open('" + urlredireccion + "', 'window','HEIGHT=600,WIDTH=820,top=50,left=50,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
                        tab = "CALIDAD";
                        break;
                    case "A8OK":
                        urlredireccion = "http://facts4-srv/thermogestion/DOCUMENTAL/DocumentosLiberacionGP12.aspx?REFERENCIA=" + tbReferencia.Text;
                        Response.Write("<script language='javascript'> window.open('" + urlredireccion + "', 'window','HEIGHT=600,WIDTH=820,top=50,left=50,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
                        tab = "CALIDAD";
                        break;
                }
                lkb_Sort_Click(tab);
            }
            catch (Exception)
            { }
        }
        public void RedireccionaGridView(Object sender, GridViewCommandEventArgs e)
        {
            string tab = "";
            string urlredireccion = "";
            if (e.CommandName == "RedirectMOLDE")
            {
                urlredireccion = "http://facts4-srv/thermogestion/MANTENIMIENTO/ReparacionMoldes.aspx?PARTE=" + e.CommandArgument.ToString();
                Response.Write("<script>window.open('" + urlredireccion + "','_blank');</script>");
                tab = "CAMBIO";
            }
            if (e.CommandName == "RedirectMAQ")
            {
                urlredireccion = "../MANTENIMIENTO/ReparacionMaquinas.aspx?PARTE=" + e.CommandArgument.ToString();
                Response.Write("<script>window.open('" + urlredireccion + "','_blank');</script>");
                tab = "PARAMETROS";
            }
            lkb_Sort_Click(tab);
        }

        //GESTIÓN DE RETORNO DE PESTAÑAS
        protected void lkb_Sort_Click(string e)
        {
            //Bind the data

            //Manage the selected tab.
            this.ManageTabsPostBack(PORTADA, CAMBIO, PARAMETROS, PROCESO, CALIDAD, tab0button, tab1button, tab2button, tab3button, tab4button, e);

        }

        private void ManageTabsPostBack(HtmlGenericControl PORTADA, HtmlGenericControl CAMBIO, HtmlGenericControl PARAMETROS, HtmlGenericControl PROCESO, HtmlGenericControl CALIDAD,
                    HtmlGenericControl tab0button, HtmlGenericControl tab1button, HtmlGenericControl tab2button, HtmlGenericControl tab3button, HtmlGenericControl tab4button, string grid)
        {
            // desactivte all tabs and panes
            tab0button.Attributes.Add("class", "");
            PORTADA.Attributes.Add("class", "tab-pane");
            tab1button.Attributes.Add("class", "");
            CAMBIO.Attributes.Add("class", "tab-pane");
            tab2button.Attributes.Add("class", "");
            PARAMETROS.Attributes.Add("class", "tab-pane");
            tab3button.Attributes.Add("class", "");
            PROCESO.Attributes.Add("class", "tab-pane");
            tab4button.Attributes.Add("class", "");
            CALIDAD.Attributes.Add("class", "tab-pane");

            //activate the the tab and pane the user was viewing
            switch (grid)
            {
                case "PORTADA":
                    tab0button.Attributes.Add("class", "active");
                    PORTADA.Attributes.Add("class", "tab-pane active");
                    break;
                case "CAMBIO":
                    tab1button.Attributes.Add("class", "active");
                    CAMBIO.Attributes.Add("class", "tab-pane active");
                    break;
                case "PARAMETROS":
                    tab2button.Attributes.Add("class", "active");
                    PARAMETROS.Attributes.Add("class", "tab-pane active");
                    break;
                case "PROCESO":
                    tab3button.Attributes.Add("class", "active");
                    PROCESO.Attributes.Add("class", "tab-pane active");
                    break;
                case "CALIDAD":
                    tab4button.Attributes.Add("class", "active");
                    CALIDAD.Attributes.Add("class", "tab-pane active");
                    break;
            }
        }

        public void mandar_mail(string orden, string producto, string parametros, string retenido)
        {
            //Create MailMessage
            MailMessage email = new MailMessage();
            email.To.Add(new MailAddress("ruben@thermolympic.es"));
            email.To.Add(new MailAddress("j.murillo@thermolympic.es"));
            email.To.Add(new MailAddress("paco@thermolympic.es"));
            email.To.Add(new MailAddress("jorge@thermolympic.es"));
            email.Bcc.Add(new MailAddress("pedro@thermolympic.es"));
            email.From = new MailAddress("calidadplanta@thermolympic.es");
            email.Subject = "Desviación crítica en liberación "+orden+" del producto "+producto+".";
            email.Body = "Se han detectado las siguientes desviaciones críticas durante el proceso de liberación:"+
                         ""+parametros+""+
                         ""+retenido+""+
                         "<br><a href='http://facts4-srv/thermogestion/LIBERACIONES/LiberacionSerie.aspx?ORDEN=" +orden+"'>Consulta la desviaciones a través de este link.</a>";
            email.IsBodyHtml = true;
            email.Priority = MailPriority.Normal;

            //Definir objeto SmtpClient
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.thermolympic.es";
            smtp.Port = 25;
            smtp.EnableSsl = false;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("calidadplanta@thermolympic.es", "010477Cp");
            

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



        /*
        //ANTIGUO
        public void importarMaquina(Object sender, EventArgs e)
        {
            try
            {
                Conexion conexion = new Conexion();
                conexion.comprobarMaquina(Convert.ToInt32(tbReferencia.Text));            
            }
            catch(Exception)
            {
            }
        }

        public void mandar_mail(string mensaje)
        {
            //Create MailMessage
            MailMessage email = new MailMessage();
            email.To.Add(new MailAddress("pedro@thermolympic.es"));
            email.From = new MailAddress("pedro@thermolympic.es");
            email.Subject = "Error importación QMaster (" + DateTime.Now.ToString("dd/MMM/yyy hh:mm:ss") + ")";
            email.Body = mensaje;
            email.IsBodyHtml = true;
            email.Priority = MailPriority.Normal;

            //Definir objeto SmtpClient
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.thermolympic.es";
            smtp.Port = 25;
            smtp.EnableSsl = false;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("r.quilez@thermolympic.es", "010477Rq");

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
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static List<int> cargar_maquinas_web(string referencia)
        {
            Conexion conexion = new Conexion();
            List<int> listado_maquinas = new List<int>();
            DataSet ds_maquinas = conexion.leer_maquinas_byRef(Convert.ToInt32(referencia));
            foreach (DataRow row in ds_maquinas.Tables[0].Rows)
            {
                listado_maquinas.Add(Convert.ToInt16(row["Maquina"]));
            }
            return listado_maquinas;
        }

        protected void thP1_TextChanged(object sender, EventArgs e)
        {

        }
        */

    }
    
}