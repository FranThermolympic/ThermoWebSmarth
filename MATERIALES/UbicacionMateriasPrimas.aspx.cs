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

namespace ThermoWeb.MATERIALES
{
    public partial class UbicacionMateriales : System.Web.UI.Page
    {

        private static DataSet ds_Liberaciones = new DataSet();
        private static string modulo = "";
        private static string estanteria = "";

        DataSet ds_Materiales = new DataSet();
        DataTable UbiMateriales = new DataTable();
       
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["UBI"] != null)
                {
                    string[] UBIURL = Request.QueryString["UBI"].ToString().Split(new char[] { '.' }); ;
                    estanteria = UBIURL[0].ToString();
                    modulo = UBIURL[1].ToString();
                }
                else
                {
                estanteria = "E1";
                modulo = "M1";
                }
                /*
                Conexion_MATERIALES conexion = new Conexion_MATERIALES();

                DataSet ds_Materiales = conexion.devuelve_lista_materiales("I.[No_] LIKE '2%'");
                for (int i = 0; i <= ds_Materiales.Tables[0].Rows.Count - 1; i++)
                {
                    DatalistNUMMaterial.InnerHtml = DatalistNUMMaterial.InnerHtml + System.Environment.NewLine +
                        String.Format("<option value='{0}'>", ds_Materiales.Tables[0].Rows[i][0]);
                }
                */
                Rellenar_Grid(null, null);
            }
            if (IsPostBack)
            {
               
                //Rellenar_Grid(null, null);
            }
          
        }

        protected void Ajusta_cajas()
        {
            
            IMGVCA1.Visible = true;
            IMGVCA2.Visible = true;
            IMGVCA3.Visible = true;
            IMGVCB1.Visible = true;
            IMGVCB2.Visible = true;
            IMGVCB3.Visible = true;
            IMGVCC1.Visible = true;
            IMGVCC2.Visible = true;
            IMGVCC3.Visible = true;
            IMGVCD1.Visible = true;
            IMGVCD2.Visible = true;
            IMGVCD3.Visible = true;
            IMGVCE1.Visible = true;
            IMGVCE2.Visible = true;
            IMGVCE3.Visible = true;
            AltA.Visible = true;
            AltB.Visible = true;
            AltC.Visible = true;
            AltD.Visible = true;
            AltE.Visible = true;
            AltEnproduccion.Visible = true;
            GridSilos.Visible = true;
            COL_A1.Visible = true;
            COL_A2.Visible = true;
            COL_A3.Visible = true;
            COL_A4.Visible = true;
            COL_B1.Visible = true;
            COL_B2.Visible = true;
            COL_B3.Visible = true;
            COL_B4.Visible = true;
            COL_C1.Visible = true;
            COL_C2.Visible = true;
            COL_C3.Visible = true;
            COL_C4.Visible = true;
            COL_D1.Visible = true;
            COL_D2.Visible = true;
            COL_D3.Visible = true;
            COL_D4.Visible = true;
            COL_E1.Visible = true;
            COL_E2.Visible = true;
            COL_E3.Visible = true;
            COL_E4.Visible = true;
            COL_A1.Attributes.Add("class", "col-lg-3 border border-dark p-0");
            COL_A2.Attributes.Add("class", "col-lg-3 border border-dark p-0");
            COL_A3.Attributes.Add("class", "col-lg-3 border border-dark p-0");
            COL_A4.Attributes.Add("class", "col-lg-3 border border-dark p-0");
            COL_B1.Attributes.Add("class", "col-lg-3 border border-dark p-0");
            COL_B2.Attributes.Add("class", "col-lg-3 border border-dark p-0");
            COL_B3.Attributes.Add("class", "col-lg-3 border border-dark p-0");
            COL_B4.Attributes.Add("class", "col-lg-3 border border-dark p-0");
            COL_C1.Attributes.Add("class", "col-lg-3 border border-dark p-0");
            COL_C2.Attributes.Add("class", "col-lg-3 border border-dark p-0");
            COL_C3.Attributes.Add("class", "col-lg-3 border border-dark p-0");
            COL_C4.Attributes.Add("class", "col-lg-3 border border-dark p-0");
            COL_D1.Attributes.Add("class", "col-lg-3 border border-dark p-0");
            COL_D2.Attributes.Add("class", "col-lg-3 border border-dark p-0");
            COL_D3.Attributes.Add("class", "col-lg-3 border border-dark p-0");
            COL_D4.Attributes.Add("class", "col-lg-3 border border-dark p-0");
            COL_E1.Attributes.Add("class", "col-lg-3 border border-dark p-0");
            COL_E2.Attributes.Add("class", "col-lg-3 border border-dark p-0");
            COL_E3.Attributes.Add("class", "col-lg-3 border border-dark p-0");
            COL_E4.Attributes.Add("class", "col-lg-3 border border-dark p-0");

        }

        protected void Rellenar_Grid(object sender, EventArgs e)
        {
            try
            {
                Conexion_MATERIALES conexion = new Conexion_MATERIALES();
                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                //DEFINO CONDICION INICIAL
                Ajusta_cajas();

                //GENERO UN DATATABLE VACIO PARA USARLO DE FORMA AUXILIAR
                DataTable chartVacio = new DataTable();
                chartVacio.Columns.Add("FechaEntrada", typeof(System.DateTime));
                chartVacio.Columns.Add("Articulo", typeof(string));
                chartVacio.Columns.Add("Descripcion", typeof(string));
                chartVacio.Columns.Add("Ubicacion", typeof(string));
                chartVacio.Columns.Add("FechaAuditoria", typeof(string));
                chartVacio.Columns.Add("Eliminado", typeof(string));
                chartVacio.Columns.Add("Estantería", typeof(string));
                chartVacio.Columns.Add("Modulo", typeof(string));
                chartVacio.Columns.Add("Balda", typeof(string));
                chartVacio.Columns.Add("EnMaquina", typeof(Boolean));
                chartVacio.Rows.Add(Convert.ToDateTime("2000-01-01 00:00:00.000"), "-", "", "", "", "", "", "", "", false);
                chartVacio.Columns.Add("Lote", typeof(string));


                //CONSULTO LA LISTA DE UBICACIONES COMPLETA
                UbiMateriales = conexion.Devuelve_Lista_Materiales_Ubicados();
                DataTable AUXUBI = new DataTable();



                try
                {
                    HtmlButton button = (HtmlButton)sender;
                    string[] RecorteUBI = button.ClientID.ToString().Split(new char[] { '_' });
                    estanteria = RecorteUBI[1].ToString();
                    modulo = RecorteUBI[2].ToString();
                }
                catch (Exception ex)
                {
                }

                if (estanteria == "S0")
                {
                    try
                    {
                        HeaderEstanteria.InnerText = "SILOS THERMOLYMPIC";
                        IMGESTANTE.ImageUrl = "..\\SMARTH_fonts\\INTERNOS\\SILOSGRUPOS.png";
                        AltA.Visible = false;
                        AltB.Visible = false;
                        AltC.Visible = false;
                        AltD.Visible = false;
                        AltE.Visible = false;
                        AltSilos.Visible = true;
                        AltEnproduccion.Visible = false;
                        DataTable SILOS = conexion.Devuelve_Lista_Materiales_Ubicados_Silos();
                        GridSilos.DataSource = SILOS;
                        GridSilos.DataBind();
                        lkb_Sort_Click("S0");
                    }
                    catch (Exception ex)
                    { }
                }
                else if (estanteria == "PR0")
                {
                    HeaderEstanteria.InnerText = "MATERIALES BLOQUEADOS PARA PRODUCCION";
                    IMGESTANTE.ImageUrl = "..\\SMARTH_fonts\\INTERNOS\\SILOSGRUPOS.png";
                    AltA.Visible = false;
                    AltB.Visible = false;
                    AltC.Visible = false;
                    AltD.Visible = false;
                    AltE.Visible = false;
                    AltSilos.Visible = false;
                    AltEnproduccion.Visible = true;

                    UbiMateriales.DefaultView.RowFilter = "EnMaquina = 1";
                    AUXUBI = (UbiMateriales.DefaultView).ToTable();
                    GridEnProduccion.DataSource = AUXUBI;
                    GridEnProduccion.DataBind();
                    lkb_Sort_Click("PR0");
                }
                else
                {
                    AltEnproduccion.Visible = false;
                    AltSilos.Visible = false;
                    HeaderEstanteria.InnerText = "ESTANTERÍA " + estanteria.Substring(1, estanteria.Length - 1) + " - MÓDULO " + modulo.Substring(1, modulo.Length - 1);

                //DEFINO AUXILIARES PARA OCULTAR POSTERIORMENTE FILAS Y COLUMNAS
                int AUXHID_A1 = 0;
                int AUXHID_A2 = 0;
                int AUXHID_A3 = 0;
                int AUXHID_A4 = 0;
                int AUXHID_B1 = 0;
                int AUXHID_B2 = 0;
                int AUXHID_B3 = 0;
                int AUXHID_B4 = 0;
                int AUXHID_C1 = 0;
                int AUXHID_C2 = 0;
                int AUXHID_C3 = 0;
                int AUXHID_C4 = 0;
                int AUXHID_D1 = 0;
                int AUXHID_D2 = 0;
                int AUXHID_D3 = 0;
                int AUXHID_D4 = 0;
                int AUXHID_E1 = 0;
                int AUXHID_E2 = 0;
                int AUXHID_E3 = 0;
                int AUXHID_E4 = 0;

                    //CARGO LOS GRIDS

                    ///////////**LINEA_A**\\\\\\

                //A1
                UbiMateriales.DefaultView.RowFilter = "Estanteria = '" + estanteria + "' AND Modulo = '" + modulo + "' AND Balda = 'A1'";
                AUXUBI = (UbiMateriales.DefaultView).ToTable();
                if (AUXUBI.Rows.Count == 0)
                {
                    if (conexion.Existe_Ubicacion(estanteria, modulo, "A1"))
                    {
                        IMGVCA1.Visible = false;
                        GridA1.DataSource = chartVacio;
                    }
                    else
                    {
                        GridA1.DataSource = null;
                        AUXHID_A1 = 1;
                    }

                }
                else
                {
                    IMGVCA1.Visible = false;
                    GridA1.DataSource = AUXUBI;
                }
                GridA1.DataBind();
                //A2
                UbiMateriales.DefaultView.RowFilter = "Estanteria = '" + estanteria + "' AND Modulo = '" + modulo + "' AND Balda = 'A2'";
                AUXUBI = (UbiMateriales.DefaultView).ToTable();
                if (AUXUBI.Rows.Count == 0)
                {
                    if (conexion.Existe_Ubicacion(estanteria, modulo, "A2"))
                    {
                        IMGVCA2.Visible = false;
                        GridA2.DataSource = chartVacio;
                    }
                    else
                    {
                        GridA2.DataSource = null;
                        AUXHID_A2 = 1;
                    }
                }
                else
                {
                    IMGVCA2.Visible = false;
                    GridA2.DataSource = AUXUBI;
                }
                GridA2.DataBind();
                //A3
                UbiMateriales.DefaultView.RowFilter = "Estanteria = '" + estanteria + "' AND Modulo = '" + modulo + "' AND Balda = 'A3'";
                AUXUBI = (UbiMateriales.DefaultView).ToTable();
                if (AUXUBI.Rows.Count == 0)
                {
                    if (conexion.Existe_Ubicacion(estanteria, modulo, "A3"))
                    {
                        IMGVCA3.Visible = false;
                        GridA3.DataSource = chartVacio;
                    }
                    else
                    {
                        GridA3.DataSource = null;
                        AUXHID_A3 = 1;
                    }
                }
                else
                {
                    IMGVCA3.Visible = false;
                    GridA3.DataSource = AUXUBI;
                }
                GridA3.DataBind();

                //A4
                UbiMateriales.DefaultView.RowFilter = "Estanteria = '" + estanteria + "' AND Modulo = '" + modulo + "' AND Balda = 'A4'";
                AUXUBI = (UbiMateriales.DefaultView).ToTable();
                if (AUXUBI.Rows.Count == 0)
                {
                    if (conexion.Existe_Ubicacion(estanteria, modulo, "A4"))
                    {
                        IMGVCA4.Visible = false;
                        GridA4.DataSource = chartVacio;
                    }
                    else
                    {
                        GridA4.DataSource = null;
                        AUXHID_A4 = 1;
                    }
                }
                else
                {
                    IMGVCA4.Visible = false;
                    GridA4.DataSource = AUXUBI;
                }
                GridA4.DataBind();

                    ///////////**LINEA_B**\\\\\\
                    //B1
                    UbiMateriales.DefaultView.RowFilter = "Estanteria = '" + estanteria + "' AND Modulo = '" + modulo + "' AND Balda = 'B1'";
                AUXUBI = (UbiMateriales.DefaultView).ToTable();
                if (AUXUBI.Rows.Count == 0)
                {
                    if (conexion.Existe_Ubicacion(estanteria, modulo, "B1"))
                    {
                        IMGVCB1.Visible = false;
                        GridB1.DataSource = chartVacio;
                    }
                    else
                    {
                        GridB1.DataSource = null;
                        AUXHID_B1 = 1;
                    }

                }
                else
                {
                    IMGVCB1.Visible = false;
                    GridB1.DataSource = AUXUBI;
                }
                GridB1.DataBind();
                //B2
                UbiMateriales.DefaultView.RowFilter = "Estanteria = '" + estanteria + "' AND Modulo = '" + modulo + "' AND Balda = 'B2'";
                AUXUBI = (UbiMateriales.DefaultView).ToTable();
                if (AUXUBI.Rows.Count == 0)
                {
                    if (conexion.Existe_Ubicacion(estanteria, modulo, "B2"))
                    {
                        IMGVCB2.Visible = false;
                        GridB2.DataSource = chartVacio;
                    }
                    else
                    {
                        GridB2.DataSource = null;
                        AUXHID_B2 = 1;
                    }
                }
                else
                {
                    IMGVCB2.Visible = false;
                    GridB2.DataSource = AUXUBI;
                }
                GridB2.DataBind();
                //B3
                UbiMateriales.DefaultView.RowFilter = "Estanteria = '" + estanteria + "' AND Modulo = '" + modulo + "' AND Balda = 'B3'";
                AUXUBI = (UbiMateriales.DefaultView).ToTable();
                if (AUXUBI.Rows.Count == 0)
                {
                    if (conexion.Existe_Ubicacion(estanteria, modulo, "B3"))
                    {
                        IMGVCB3.Visible = false;
                        GridB3.DataSource = chartVacio;
                    }
                    else
                    {
                        GridB3.DataSource = null;
                        AUXHID_B3 = 1;
                    }
                }
                else
                {
                    IMGVCB3.Visible = false;
                    GridB3.DataSource = AUXUBI;
                }
                GridB3.DataBind();

                //B4
                UbiMateriales.DefaultView.RowFilter = "Estanteria = '" + estanteria + "' AND Modulo = '" + modulo + "' AND Balda = 'B4'";
                AUXUBI = (UbiMateriales.DefaultView).ToTable();
                if (AUXUBI.Rows.Count == 0)
                {
                    if (conexion.Existe_Ubicacion(estanteria, modulo, "B4"))
                    {
                        IMGVCB4.Visible = false;
                        GridB4.DataSource = chartVacio;
                    }
                    else
                    {
                        GridB4.DataSource = null;
                        AUXHID_B4 = 1;
                    }
                }
                else
                {
                    IMGVCB4.Visible = false;
                    GridB4.DataSource = AUXUBI;
                }
                GridB4.DataBind();

                    ///////////**LINEA_C**\\\\\\
                    //C1
                    UbiMateriales.DefaultView.RowFilter = "Estanteria = '" + estanteria + "' AND Modulo = '" + modulo + "' AND Balda = 'C1'";
                AUXUBI = (UbiMateriales.DefaultView).ToTable();
                if (AUXUBI.Rows.Count == 0)
                {
                    if (conexion.Existe_Ubicacion(estanteria, modulo, "C1"))
                    {
                        IMGVCC1.Visible = false;
                        GridC1.DataSource = chartVacio;
                    }
                    else
                    {
                        GridC1.DataSource = null;
                        AUXHID_C1 = 1;
                    }

                }
                else
                {
                    IMGVCC1.Visible = false;
                    GridC1.DataSource = AUXUBI;
                }
                GridC1.DataBind();
                //C2
                UbiMateriales.DefaultView.RowFilter = "Estanteria = '" + estanteria + "' AND Modulo = '" + modulo + "' AND Balda = 'C2'";
                AUXUBI = (UbiMateriales.DefaultView).ToTable();
                if (AUXUBI.Rows.Count == 0)
                {
                    if (conexion.Existe_Ubicacion(estanteria, modulo, "C2"))
                    {
                        IMGVCC2.Visible = false;
                        GridC2.DataSource = chartVacio;
                    }
                    else
                    {
                        GridC2.DataSource = null;
                        AUXHID_C2 = 1;
                    }
                }
                else
                {
                    IMGVCC2.Visible = false;
                    GridC2.DataSource = AUXUBI;
                }
                GridC2.DataBind();
                //C3
                UbiMateriales.DefaultView.RowFilter = "Estanteria = '" + estanteria + "' AND Modulo = '" + modulo + "' AND Balda = 'C3'";
                AUXUBI = (UbiMateriales.DefaultView).ToTable();
                if (AUXUBI.Rows.Count == 0)
                {
                    if (conexion.Existe_Ubicacion(estanteria, modulo, "C3"))
                    {
                        IMGVCC3.Visible = false;
                        GridC3.DataSource = chartVacio;
                    }
                    else
                    {
                        GridC3.DataSource = null;
                        AUXHID_C3 = 1;
                    }
                }
                else
                {
                    IMGVCC3.Visible = false;
                    GridC3.DataSource = AUXUBI;
                }
                GridC3.DataBind();

                //C4
                UbiMateriales.DefaultView.RowFilter = "Estanteria = '" + estanteria + "' AND Modulo = '" + modulo + "' AND Balda = 'C4'";
                AUXUBI = (UbiMateriales.DefaultView).ToTable();
                if (AUXUBI.Rows.Count == 0)
                {
                    if (conexion.Existe_Ubicacion(estanteria, modulo, "C4"))
                    {
                        IMGVCC4.Visible = false;
                        GridC4.DataSource = chartVacio;
                    }
                    else
                    {
                        GridC4.DataSource = null;
                        AUXHID_C4 = 1;
                    }
                }
                else
                {
                    IMGVCC4.Visible = false;
                    GridC4.DataSource = AUXUBI;
                }
                GridC4.DataBind();


                    ///////////**LINEA_E**\\\\\\


                    //E1
                    UbiMateriales.DefaultView.RowFilter = "Estanteria = '" + estanteria + "' AND Modulo = '" + modulo + "' AND Balda = 'E1'";
                AUXUBI = (UbiMateriales.DefaultView).ToTable();
                if (AUXUBI.Rows.Count == 0)
                {
                    if (conexion.Existe_Ubicacion(estanteria, modulo, "E1"))
                    {
                        IMGVCE1.Visible = false;
                        GridE1.DataSource = chartVacio;
                    }
                    else
                    {
                        GridE1.DataSource = null;
                        AUXHID_E1 = 1;
                    }

                }
                else
                {
                    IMGVCE1.Visible = false;
                    GridE1.DataSource = AUXUBI;
                }
                GridE1.DataBind();
                //E2
                UbiMateriales.DefaultView.RowFilter = "Estanteria = '" + estanteria + "' AND Modulo = '" + modulo + "' AND Balda = 'E2'";
                AUXUBI = (UbiMateriales.DefaultView).ToTable();
                if (AUXUBI.Rows.Count == 0)
                {
                    if (conexion.Existe_Ubicacion(estanteria, modulo, "E2"))
                    {
                        IMGVCE2.Visible = false;
                        GridE2.DataSource = chartVacio;
                    }
                    else
                    {
                        GridE2.DataSource = null;
                        AUXHID_E2 = 1;
                    }
                }
                else
                {
                    IMGVCE2.Visible = false;
                    GridE2.DataSource = AUXUBI;
                }
                GridE2.DataBind();
                //E3
                UbiMateriales.DefaultView.RowFilter = "Estanteria = '" + estanteria + "' AND Modulo = '" + modulo + "' AND Balda = 'E3'";
                AUXUBI = (UbiMateriales.DefaultView).ToTable();
                if (AUXUBI.Rows.Count == 0)
                {
                    if (conexion.Existe_Ubicacion(estanteria, modulo, "E3"))
                    {
                        IMGVCE3.Visible = false;
                        GridE3.DataSource = chartVacio;
                    }
                    else
                    {
                        GridE3.DataSource = null;
                        AUXHID_E3 = 1;
                    }
                }
                else
                {
                    IMGVCE3.Visible = false;
                    GridE3.DataSource = AUXUBI;
                }
                GridE3.DataBind();

                //E4
                UbiMateriales.DefaultView.RowFilter = "Estanteria = '" + estanteria + "' AND Modulo = '" + modulo + "' AND Balda = 'E4'";
                AUXUBI = (UbiMateriales.DefaultView).ToTable();
                if (AUXUBI.Rows.Count == 0)
                {
                    if (conexion.Existe_Ubicacion(estanteria, modulo, "E4"))
                    {
                        IMGVCE4.Visible = false;
                        GridE4.DataSource = chartVacio;
                    }
                    else
                    {
                        GridE4.DataSource = null;
                        AUXHID_E4 = 1;
                    }
                }
                else
                {
                    IMGVCE4.Visible = false;
                    GridE4.DataSource = AUXUBI;
                }
                GridE4.DataBind();

                    ///////////**LINEA_D**\\\\\\
                    //D1
                    UbiMateriales.DefaultView.RowFilter = "Estanteria = '" + estanteria + "' AND Modulo = '" + modulo + "' AND Balda = 'D1'";
                AUXUBI = (UbiMateriales.DefaultView).ToTable();
                if (AUXUBI.Rows.Count == 0)
                {
                    if (conexion.Existe_Ubicacion(estanteria, modulo, "D1"))
                    {
                        IMGVCD1.Visible = false;
                        GridD1.DataSource = chartVacio;
                    }
                    else
                    {
                        GridD1.DataSource = null;
                        AUXHID_D1 = 1;
                    }

                }
                else
                {
                    IMGVCD1.Visible = false;
                    GridD1.DataSource = AUXUBI;
                }
                GridD1.DataBind();
                //D2
                UbiMateriales.DefaultView.RowFilter = "Estanteria = '" + estanteria + "' AND Modulo = '" + modulo + "' AND Balda = 'D2'";
                AUXUBI = (UbiMateriales.DefaultView).ToTable();
                if (AUXUBI.Rows.Count == 0)
                {
                    if (conexion.Existe_Ubicacion(estanteria, modulo, "D2"))
                    {
                        IMGVCD2.Visible = false;
                        GridD2.DataSource = chartVacio;
                    }
                    else
                    {
                        GridD2.DataSource = null;
                        AUXHID_D2 = 1;
                    }
                }
                else
                {
                    IMGVCD2.Visible = false;
                    GridD2.DataSource = AUXUBI;
                }
                GridD2.DataBind();
                //D3
                UbiMateriales.DefaultView.RowFilter = "Estanteria = '" + estanteria + "' AND Modulo = '" + modulo + "' AND Balda = 'D3'";
                AUXUBI = (UbiMateriales.DefaultView).ToTable();
                if (AUXUBI.Rows.Count == 0)
                {
                    if (conexion.Existe_Ubicacion(estanteria, modulo, "D3"))
                    {
                        IMGVCD3.Visible = false;
                        GridD3.DataSource = chartVacio;
                    }
                    else
                    {
                        GridD3.DataSource = null;
                        AUXHID_D3 = 1;
                    }
                }
                else
                {
                    IMGVCD3.Visible = false;
                    GridD3.DataSource = AUXUBI;
                }
                GridD3.DataBind();

                //D4
                UbiMateriales.DefaultView.RowFilter = "Estanteria = '" + estanteria + "' AND Modulo = '" + modulo + "' AND Balda = 'D4'";
                AUXUBI = (UbiMateriales.DefaultView).ToTable();
                if (AUXUBI.Rows.Count == 0)
                {
                    if (conexion.Existe_Ubicacion(estanteria, modulo, "D4"))
                    {
                        IMGVCD4.Visible = false;
                        GridD4.DataSource = chartVacio;
                    }
                    else
                    {
                        GridD4.DataSource = null;
                        AUXHID_D4 = 1;
                    }
                }
                else
                {
                    IMGVCD4.Visible = false;
                    GridD4.DataSource = AUXUBI;
                }
                GridD4.DataBind();

                int VISTCOL1 = 1;
                int VISTCOL2 = 1;
                int VISTCOL3 = 1;
                int VISTCOL4 = 1;

                    //OCULTO COLUMNAS
                if (AUXHID_E4 == 1 && AUXHID_D4 == 1 && AUXHID_C4 == 1 && AUXHID_B4 == 1 && AUXHID_A4 == 1)
                {
                    COL_E4.Visible = false;
                    COL_D4.Visible = false;
                    COL_C4.Visible = false;
                    COL_B4.Visible = false;
                    COL_A4.Visible = false;
                    VISTCOL4 = 0;
                }
                if (AUXHID_E3 == 1 && AUXHID_D3 == 1 && AUXHID_C3 == 1 && AUXHID_B3 == 1 && AUXHID_A3 == 1)
                {
                    COL_E3.Visible = false;
                    COL_D3.Visible = false;
                    COL_C3.Visible = false;
                    COL_B3.Visible = false;
                    COL_A3.Visible = false;
                    VISTCOL3 = 0;
                }
                if (AUXHID_E2 == 1 && AUXHID_D2 == 1 && AUXHID_C2 == 1 && AUXHID_B2 == 1 && AUXHID_A2 == 1)
                {
                    COL_E2.Visible = false;
                    COL_D2.Visible = false;
                    COL_C2.Visible = false;
                    COL_B2.Visible = false;
                    COL_A2.Visible = false;
                    VISTCOL2 = 0;
                }
                if (AUXHID_E1 == 1 && AUXHID_D1 == 1 && AUXHID_C1 == 1 && AUXHID_B1 == 1 && AUXHID_A1 == 1)
                {
                    COL_E1.Visible = false;
                    COL_D1.Visible = false;
                    COL_C1.Visible = false;
                    COL_B1.Visible = false;
                    COL_A1.Visible = false;
                    VISTCOL1 = 0;
                }


                //OCULTO LINEAS
                if (AUXHID_E1 == 1 && AUXHID_E2 == 1 && AUXHID_E3 == 1 && AUXHID_E4 == 1)
                {
                    AltE.Visible = false;
                }
                if (AUXHID_D1 == 1 && AUXHID_D2 == 1 && AUXHID_D3 == 1 && AUXHID_D4 == 1)
                {
                    AltD.Visible = false;
                }
                if (AUXHID_C1 == 1 && AUXHID_C2 == 1 && AUXHID_C3 == 1 && AUXHID_C4 == 1)
                {
                    AltC.Visible = false;
                }
                if (AUXHID_B1 == 1 && AUXHID_B2 == 1 && AUXHID_B3 == 1 && AUXHID_B4 == 1)
                {
                    AltB.Visible = false;
                }

                //REDIMENSIONO COLUMNAS
                if (VISTCOL1 + VISTCOL2 + VISTCOL3 + VISTCOL4 == 1)
                {
                    COL_A1.Attributes.Add("class", "col-lg-12 border border-dark p-0");
                    COL_A2.Attributes.Add("class", "col-lg-12 border border-dark p-0");
                    COL_A3.Attributes.Add("class", "col-lg-12 border border-dark p-0");
                    COL_B1.Attributes.Add("class", "col-lg-12 border border-dark p-0");
                    COL_B2.Attributes.Add("class", "col-lg-12 border border-dark p-0");
                    COL_B3.Attributes.Add("class", "col-lg-12 border border-dark p-0");
                    COL_C1.Attributes.Add("class", "col-lg-12 border border-dark p-0");
                    COL_C2.Attributes.Add("class", "col-lg-12 border border-dark p-0");
                    COL_C3.Attributes.Add("class", "col-lg-12 border border-dark p-0");
                    COL_D1.Attributes.Add("class", "col-lg-12 border border-dark p-0");
                    COL_D2.Attributes.Add("class", "col-lg-12 border border-dark p-0");
                    COL_D3.Attributes.Add("class", "col-lg-12 border border-dark p-0");
                    COL_E1.Attributes.Add("class", "col-lg-12 border border-dark p-0");
                    COL_E2.Attributes.Add("class", "col-lg-12 border border-dark p-0");
                    COL_E3.Attributes.Add("class", "col-lg-12 border border-dark p-0");
                }
                if (VISTCOL1 + VISTCOL2 + VISTCOL3 + VISTCOL4 == 2)
                {
                    COL_A1.Attributes.Add("class", "col-lg-6 border border-dark p-0");
                    COL_A2.Attributes.Add("class", "col-lg-6 border border-dark p-0");
                    COL_A3.Attributes.Add("class", "col-lg-6 border border-dark p-0");
                    COL_B1.Attributes.Add("class", "col-lg-6 border border-dark p-0");
                    COL_B2.Attributes.Add("class", "col-lg-6 border border-dark p-0");
                    COL_B3.Attributes.Add("class", "col-lg-6 border border-dark p-0");
                    COL_C1.Attributes.Add("class", "col-lg-6 border border-dark p-0");
                    COL_C2.Attributes.Add("class", "col-lg-6 border border-dark p-0");
                    COL_C3.Attributes.Add("class", "col-lg-6 border border-dark p-0");
                    COL_D1.Attributes.Add("class", "col-lg-6 border border-dark p-0");
                    COL_D2.Attributes.Add("class", "col-lg-6 border border-dark p-0");
                    COL_D3.Attributes.Add("class", "col-lg-6 border border-dark p-0");
                    COL_E1.Attributes.Add("class", "col-lg-6 border border-dark p-0");
                    COL_E2.Attributes.Add("class", "col-lg-6 border border-dark p-0");
                    COL_E3.Attributes.Add("class", "col-lg-6 border border-dark p-0");
                }
                if (VISTCOL1 + VISTCOL2 + VISTCOL3 + VISTCOL4 == 3)
                {
                    COL_A1.Attributes.Add("class", "col-lg-4 border border-dark p-0");
                    COL_A2.Attributes.Add("class", "col-lg-4 border border-dark p-0");
                    COL_A3.Attributes.Add("class", "col-lg-4 border border-dark p-0");
                    COL_B1.Attributes.Add("class", "col-lg-4 border border-dark p-0");
                    COL_B2.Attributes.Add("class", "col-lg-4 border border-dark p-0");
                    COL_B3.Attributes.Add("class", "col-lg-4 border border-dark p-0");
                    COL_C1.Attributes.Add("class", "col-lg-4 border border-dark p-0");
                    COL_C2.Attributes.Add("class", "col-lg-4 border border-dark p-0");
                    COL_C3.Attributes.Add("class", "col-lg-4 border border-dark p-0");
                    COL_D1.Attributes.Add("class", "col-lg-4 border border-dark p-0");
                    COL_D2.Attributes.Add("class", "col-lg-4 border border-dark p-0");
                    COL_D3.Attributes.Add("class", "col-lg-4 border border-dark p-0");
                    COL_E1.Attributes.Add("class", "col-lg-4 border border-dark p-0");
                    COL_E2.Attributes.Add("class", "col-lg-4 border border-dark p-0");
                    COL_E3.Attributes.Add("class", "col-lg-4 border border-dark p-0");
                }
                if (VISTCOL1 + VISTCOL2 + VISTCOL3 + VISTCOL4 == 4)
                {
                    COL_A1.Attributes.Add("class", "col-lg-3 border border-dark p-0");
                    COL_A2.Attributes.Add("class", "col-lg-3 border border-dark p-0");
                    COL_A3.Attributes.Add("class", "col-lg-3 border border-dark p-0");
                    COL_B1.Attributes.Add("class", "col-lg-3 border border-dark p-0");
                    COL_B2.Attributes.Add("class", "col-lg-3 border border-dark p-0");
                    COL_B3.Attributes.Add("class", "col-lg-3 border border-dark p-0");
                    COL_C1.Attributes.Add("class", "col-lg-3 border border-dark p-0");
                    COL_C2.Attributes.Add("class", "col-lg-3 border border-dark p-0");
                    COL_C3.Attributes.Add("class", "col-lg-3 border border-dark p-0");
                    COL_D1.Attributes.Add("class", "col-lg-3 border border-dark p-0");
                    COL_D2.Attributes.Add("class", "col-lg-3 border border-dark p-0");
                    COL_D3.Attributes.Add("class", "col-lg-3 border border-dark p-0");
                    COL_E1.Attributes.Add("class", "col-lg-3 border border-dark p-0");
                    COL_E2.Attributes.Add("class", "col-lg-3 border border-dark p-0");
                    COL_E3.Attributes.Add("class", "col-lg-3 border border-dark p-0");
                }

                switch (estanteria)
                {
                    case "E1":
                        IMGESTANTE.ImageUrl = "..\\SMARTH_fonts\\INTERNOS\\ESTANTERIA1.png";
                        break;
                    case "E2":
                        IMGESTANTE.ImageUrl = "..\\SMARTH_fonts\\INTERNOS\\ESTANTERIA2.png";
                        break;
                    case "E3":
                        IMGESTANTE.ImageUrl = "..\\SMARTH_fonts\\INTERNOS\\ESTANTERIA3.png";
                        break;
                    case "E4":
                        IMGESTANTE.ImageUrl = "..\\SMARTH_fonts\\INTERNOS\\ESTANTERIA4.png";
                        break;
                    case "E5":
                        IMGESTANTE.ImageUrl = "..\\SMARTH_fonts\\INTERNOS\\ESTANTERIA5.png";
                        break;
                    case "E6":
                        IMGESTANTE.ImageUrl = "..\\SMARTH_fonts\\INTERNOS\\ESTANTERIA6.png";
                        break;
                    case "E7":
                        IMGESTANTE.ImageUrl = "..\\SMARTH_fonts\\INTERNOS\\ESTANTERIA7.png";
                        break;
                    case "E8":
                        IMGESTANTE.ImageUrl = "..\\SMARTH_fonts\\INTERNOS\\ESTANTERIA8.png";
                        break;
                    case "E9":
                        IMGESTANTE.ImageUrl = "..\\SMARTH_fonts\\INTERNOS\\ESTANTERIA9.png";
                        break;
                    case "E10":
                        IMGESTANTE.ImageUrl = "..\\SMARTH_fonts\\INTERNOS\\ESTANTERIA10.png";
                        break;

                }
                lkb_Sort_Click(estanteria);
            }
            }

            catch (Exception ex)
            { }


        }

        public void AgregarMaterialXUbicacion(object sender, EventArgs e)
        {
            
            string[] RecorteMAT = NUMMaterial.Value.Split(new char[] { '¬' });
            string MATERIAL = RecorteMAT[0].ToString();
            if (MATERIAL != "" && NUMMaterialLote.Value != "")
            {
                Conexion_MATERIALES conexion = new Conexion_MATERIALES();
                conexion.Insertar_MaterialxUbicacion(AuxTBUbicacion.InnerText, MATERIAL, "0", NUMMaterialLote.Value);

                string[] ReapuntaUBI = AuxTBUbicacion.InnerText.Split(new char[] { '.' });
                if (estanteria.Substring(0, 1) == "S")
                {
                    estanteria = "S0";
                    modulo = "M1";
                }
                else
                {
                    estanteria = ReapuntaUBI[0].ToString();
                    modulo = ReapuntaUBI[1].ToString();
                }
                Rellenar_Grid(null, null);
                lkb_Sort_Click(estanteria);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "AlertaFaltanDatos();", true);
            }
        }

        public void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    LinkButton BTNAUDITAUBICACION = (LinkButton)e.Row.FindControl("BTNAuditaUbicacion");

                    LinkButton BTNELIMINARTICULO = (LinkButton)e.Row.FindControl("BTNEliminaArticulo");
                    LinkButton BTNBLOQUEAPROD = (LinkButton)e.Row.FindControl("BTNBloquearProduccion");
                    Label LBLFECHAENTRADA = (Label)e.Row.FindControl("lblFechaEntrada");
                    Label LBLAUXFECHAENTRADA = (Label)e.Row.FindControl("lblAuxFechaEntrada");
                    HiddenField MatENPROD = (HiddenField)e.Row.FindControl("lblEnProduccion");
                    Image IMGEnMaquina = (Image)e.Row.FindControl("IMGEnMaquina");
                    Label LBLLOTE = (Label)e.Row.FindControl("lblLote");

                    if (LBLFECHAENTRADA.Text == "<br />(Desde el 01/01/2000)") //OCULTO LOS GENERADOS AUXILIARMENTE
                    {
                        LBLFECHAENTRADA.Visible = false;
                        BTNELIMINARTICULO.Visible = false;
                        BTNAUDITAUBICACION.Visible = false;
                        BTNBLOQUEAPROD.Visible = false;
                        LBLLOTE.Visible = false;
                    }
                    if (LBLAUXFECHAENTRADA.Text != null)
                    {
                        if (Convert.ToDateTime(LBLAUXFECHAENTRADA.Text) <= DateTime.Now.Date) //OCULTO AQUELLOS QUE ESTÁN DENTRO DE FECHA
                        {
                            BTNAUDITAUBICACION.Visible = false;
                        }
                    }
                    if (Convert.ToBoolean(MatENPROD.Value))
                    {
                        e.Row.Cells[1].BackColor = System.Drawing.Color.Orange;
                        e.Row.Cells[1].BorderWidth = 2;
                        //e.Row.Cells[1].BorderStyle = System.Web.UI.WebControls.BorderStyle.Dashed;
                        e.Row.Cells[1].BorderColor = System.Drawing.Color.Red;


                        IMGEnMaquina.Visible = true;
                    }
                }
                catch (Exception ex)
                { }



            }
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView gv = (GridView)sender;
                string NOMBRESTANTE = "";
                switch (gv.ID)
                {
                    case "GridA1":
                        NOMBRESTANTE = modulo + ".A1";
                        break;
                    case "GridA2":
                        NOMBRESTANTE = modulo + ".A2";
                        break;
                    case "GridA3":
                        NOMBRESTANTE = modulo + ".A3";
                        break;
                    case "GridA4":
                        NOMBRESTANTE = modulo + ".A4";
                        break;
                    case "GridB1":
                        NOMBRESTANTE = modulo + ".B1";
                        break;
                    case "GridB2":
                        NOMBRESTANTE = modulo + ".B2";
                        break;
                    case "GridB3":
                        NOMBRESTANTE = modulo + ".B3";
                        break;
                    case "GridB4":
                        NOMBRESTANTE = modulo + ".B4";
                        break;
                    case "GridD1":
                        NOMBRESTANTE = modulo + ".D1";
                        break;
                    case "GridD2":
                        NOMBRESTANTE = modulo + ".D2";
                        break;
                    case "GridD3":
                        NOMBRESTANTE = modulo + ".D3";
                        break;
                    case "GridD4":
                        NOMBRESTANTE = modulo + ".D4";
                        break;
                    case "GridC1":
                        NOMBRESTANTE = modulo + ".C1";
                        break;
                    case "GridC2":
                        NOMBRESTANTE = modulo + ".C2";
                        break;
                    case "GridC3":
                        NOMBRESTANTE = modulo + ".C3";
                        break;
                    case "GridC4":
                        NOMBRESTANTE = modulo + ".C4";
                        break;
                    case "GridE1":
                        NOMBRESTANTE = modulo + ".E1";
                        break;
                    case "GridE2":
                        NOMBRESTANTE = modulo + ".E2";
                        break;
                    case "GridE3":
                        NOMBRESTANTE = modulo + ".E3";
                        break;
                    case "GridE4":
                        NOMBRESTANTE = modulo + ".E4";
                        break;
                }
                e.Row.Cells[0].Text = "<i class=\"bi bi-arrow-down-square\">&nbsp "+NOMBRESTANTE+"</i>";
                e.Row.Cells[0].Style.Add("text-shadow", "1px 1px 1px black");
            }
        }

        public void GridCommandEventHandler(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EliminaArticulo")
                {
                    
                    Conexion_MATERIALES conexion = new Conexion_MATERIALES();

                    string[] RecorteCOMANDO = e.CommandArgument.ToString().Split(new char[] { '¬' });
                    string FECHA = RecorteCOMANDO[0].ToString();
                    string ARTICULO = RecorteCOMANDO[1].ToString();
                    string UBICACION = RecorteCOMANDO[2].ToString();
                    string LOTE = RecorteCOMANDO[3].ToString();

                    conexion.Elimina_MaterialxUbicacion(FECHA, ARTICULO, UBICACION, LOTE);
                    Rellenar_Grid(null, null);
                    lkb_Sort_Click(estanteria);

                }
                if (e.CommandName == "ValidaArticulo")
                {
                    string recepcion = e.CommandArgument.ToString();
                    lkb_Sort_Click(estanteria);
                    int a = 0;
                }
                if (e.CommandName == "AgregaArticulo")
                {
                    AuxTBUbicacion.InnerText = estanteria + "." + modulo + "." + e.CommandArgument.ToString();
                    NUMMaterial.Value = "";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "ShowPopDocVinculados();", true);
                    lkb_Sort_Click(estanteria);
                }
                if (e.CommandName == "AgregaArticuloSILO")
                {
                    AuxTBUbicacion.InnerText = e.CommandArgument.ToString();
                    NUMMaterial.Value = "";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "ShowPopDocVinculados();", true);
                    lkb_Sort_Click(estanteria);
                }
                if (e.CommandName == "BloqueaArticulo")
                {
                    Conexion_MATERIALES conexion = new Conexion_MATERIALES();
                    string[] RecorteCOMANDO = e.CommandArgument.ToString().Split(new char[] { '¬' });
                    string FECHA = RecorteCOMANDO[0].ToString();
                    string ARTICULO = RecorteCOMANDO[1].ToString();
                    string UBICACION = RecorteCOMANDO[2].ToString();
                    string ENMAQUINA = RecorteCOMANDO[3].ToString();
                    string LOTE = RecorteCOMANDO[4].ToString();

                    conexion.BloquearProd_MaterialxUbicacion(FECHA, ARTICULO, UBICACION, ENMAQUINA, LOTE);
                    Rellenar_Grid(null, null);
                    lkb_Sort_Click(estanteria);

                }
            }
            catch (Exception ex)
            {
                
            }
        }

        protected void lkb_Sort_Click(string e)
        {
            //Bind the data

            //Manage the selected tab.
            this.ManageTabsPostBack(TAB_EST_0, TAB_EST_1, TAB_EST_2, TAB_EST_3, TAB_EST_4, TAB_EST_5, TAB_EST_6, TAB_EST_7, TAB_EST_8, TAB_EST_9, TAB_EST_10, TAB_EST_PROD, PILL_EST0, PILL_EST1, PILL_EST2, PILL_EST3, PILL_EST4, PILL_EST5, PILL_EST6, PILL_EST7, PILL_EST8, PILL_EST9, PILL_EST10, PILL_ESTPROD, e);

        }

        private void ManageTabsPostBack(HtmlGenericControl TAB_EST_0, HtmlGenericControl TAB_EST_1, HtmlGenericControl TAB_EST_2, HtmlGenericControl TAB_EST_3, HtmlGenericControl TAB_EST_4, HtmlGenericControl TAB_EST_5, HtmlGenericControl TAB_EST_6, HtmlGenericControl TAB_EST_7, HtmlGenericControl TAB_EST_8, HtmlGenericControl TAB_EST_9, HtmlGenericControl TAB_EST_10, HtmlGenericControl TAB_EST_PROD, HtmlButton PILL_EST0, HtmlButton PILL_EST1, HtmlButton PILL_EST2, HtmlButton PILL_EST3, HtmlButton PILL_EST4, HtmlButton PILL_EST5, HtmlButton PILL_EST6, HtmlButton PILL_EST7, HtmlButton PILL_EST8, HtmlButton PILL_EST9, HtmlButton PILL_EST10, HtmlButton PILL_ESTPROD, string grid)
        {
            // DESACTIVO TODOS LOS TABS Y PILLS
            TAB_EST_0.Attributes.Add("class", "tab-pane fade");
            PILL_EST0.Attributes.Add("class", "nav-link shadow  border border-dark pe-0 ps-0");

            TAB_EST_1.Attributes.Add("class", "tab-pane fade");
            PILL_EST1.Attributes.Add("class", "nav-link shadow  border border-dark pe-0 ps-0");

            TAB_EST_2.Attributes.Add("class", "tab-pane fade");
            PILL_EST2.Attributes.Add("class", "nav-link shadow  border border-dark pe-0 ps-0");

            TAB_EST_3.Attributes.Add("class", "tab-pane fade");
            PILL_EST3.Attributes.Add("class", "nav-link shadow  border border-dark pe-0 ps-0");

            TAB_EST_4.Attributes.Add("class", "tab-pane fade");
            PILL_EST4.Attributes.Add("class", "nav-link shadow  border border-dark pe-0 ps-0");

            TAB_EST_5.Attributes.Add("class", "tab-pane fade");
            PILL_EST5.Attributes.Add("class", "nav-link shadow  border border-dark pe-0 ps-0");

            TAB_EST_6.Attributes.Add("class", "tab-pane fade");
            PILL_EST6.Attributes.Add("class", "nav-link shadow  border border-dark pe-0 ps-0");

            TAB_EST_7.Attributes.Add("class", "tab-pane fade");
            PILL_EST7.Attributes.Add("class", "nav-link shadow  border border-dark pe-0 ps-0");

            TAB_EST_8.Attributes.Add("class", "tab-pane fade");
            PILL_EST8.Attributes.Add("class", "nav-link shadow  border border-dark pe-0 ps-0");

            TAB_EST_9.Attributes.Add("class", "tab-pane fade");
            PILL_EST9.Attributes.Add("class", "nav-link shadow  border border-dark pe-0 ps-0");

            TAB_EST_10.Attributes.Add("class", "tab-pane fade");
            PILL_EST10.Attributes.Add("class", "nav-link shadow  border border-dark pe-0 ps-0");

            TAB_EST_PROD.Attributes.Add("class", "tab-pane fade");
            PILL_ESTPROD.Attributes.Add("class", "nav-link shadow  border border-dark pe-0 ps-0");

            //activate the the tab and pane the user was viewing
            switch (grid)
            {
                case "PR0":
                    PILL_ESTPROD.Attributes.Add("class", "nav-link shadow  border border-dark  pe-0 ps-0 active");
                    TAB_EST_PROD.Attributes.Add("class", "tab-pane fade show active");
                    break;
                case "S0":
                    PILL_EST0.Attributes.Add("class", "nav-link shadow  border border-dark  pe-0 ps-0 active");
                    TAB_EST_0.Attributes.Add("class", "tab-pane fade show active");
                    break;
                case "E1":
                    PILL_EST1.Attributes.Add("class", "nav-link shadow  border border-dark pe-0 ps-0 active");
                    TAB_EST_1.Attributes.Add("class", "tab-pane fade show active");
                    break;
                case "E2":
                    PILL_EST2.Attributes.Add("class", "nav-link shadow  border border-dark pe-0 ps-0 active");
                    TAB_EST_2.Attributes.Add("class", "tab-pane fade show active");
                    break;
                case "E3":
                    PILL_EST3.Attributes.Add("class", "nav-link shadow  border border-dark pe-0 ps-0 active");
                    TAB_EST_3.Attributes.Add("class", "tab-pane fade show active");
                    break;
                case "E4":
                    PILL_EST4.Attributes.Add("class", "nav-link shadow  border border-dark pe-0 ps-0 active");
                    TAB_EST_4.Attributes.Add("class", "tab-pane fade show active");
                    break;
                case "E5":
                    PILL_EST5.Attributes.Add("class", "nav-link shadow  border border-dark pe-0 ps-0 active");
                    TAB_EST_5.Attributes.Add("class", "tab-pane fade show active");
                    break;
                case "E6":
                    PILL_EST6.Attributes.Add("class", "nav-link shadow  border border-dark pe-0 ps-0 active");
                    TAB_EST_6.Attributes.Add("class", "tab-pane fade show active");
                    break;
                case "E7":
                    PILL_EST7.Attributes.Add("class", "nav-link shadow  border border-dark pe-0 ps-0 active");
                    TAB_EST_7.Attributes.Add("class", "tab-pane fade show active");
                    break;
                case "E8":
                    PILL_EST8.Attributes.Add("class", "nav-link shadow  border border-dark pe-0 ps-0 active");
                    TAB_EST_8.Attributes.Add("class", "tab-pane fade show active");
                    break;
                case "E9":
                    PILL_EST9.Attributes.Add("class", "nav-link shadow  border border-dark pe-0 ps-0 active");
                    TAB_EST_9.Attributes.Add("class", "tab-pane fade show active");
                    break;
                case "E10":
                    PILL_EST10.Attributes.Add("class", "nav-link shadow  border border-dark pe-0 ps-0 active");
                    TAB_EST_10.Attributes.Add("class", "tab-pane fade show active");
                    break;

            }
        }




    }

}