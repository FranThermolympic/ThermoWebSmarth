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
using System.IO;

namespace ThermoWeb.MATERIALES
{
    public partial class PrevisionSecado : System.Web.UI.Page
    {

        private static DataSet ds_Liberaciones = new DataSet();
        private static string SELECNAVE = "Máq.";
        private static string SELECESTADO = "0";
        private static string SELECORDERBY = "0";
        DataSet ds_Materiales = new DataSet();
        DataTable DT_Materiales = new DataTable();
        DataTable DT_MaterialesJoin = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SELECNAVE = "Máq.";
                SELECESTADO = "0";
                SELECORDERBY = "0";


                Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                Conexion_MATERIALES conexion = new Conexion_MATERIALES();

                DataTable TiposMaterial = SHConexion.Devuelve_listado_Tipos_Material();
                DropTipoMaterial.Items.Add("-");
                foreach (DataRow row in TiposMaterial.Rows) { DropTipoMaterial.Items.Add(row["TipoMaterial"].ToString()); }
                DropTipoMaterial.ClearSelection();
                DropTipoMaterial.SelectedValue = "-";


                DropTipoFiltro.Items.Add("Sin definir");
                foreach (DataRow row in TiposMaterial.Rows) { DropTipoFiltro.Items.Add(row["TipoMaterial"].ToString().Trim()); }
                DropTipoFiltro.ClearSelection();
                DropTipoFiltro.SelectedValue = "-";

                DataTable TiposFamilia = conexion.Devuelve_Familias_Productos_NAV();
                DropFamiliaFiltro.Items.Add("-");
                foreach (DataRow row in TiposFamilia.Rows) { DropFamiliaFiltro.Items.Add(row["Description"].ToString()); }
                DropFamiliaFiltro.ClearSelection();
                DropFamiliaFiltro.SelectedValue = "-";


                Rellenar_Grid(null, null);
                
                for (int i = 0; i <= DT_Materiales.Rows.Count - 1; i++)
                {
                    DatalistNUMMaterial.InnerHtml = DatalistNUMMaterial.InnerHtml + System.Environment.NewLine +
                        String.Format("<option value='{0}'>", DT_Materiales.Rows[i][0]);
                }
                


            }
            if (IsPostBack)
            {
                Rellenar_Grid(null, null);
            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "setTimeout(myFunction, 600000);", true);
            FECHAACT.Text = DateTime.Now.ToString("dd/MM/yyy HH:mm");

            DropDownList DropSelecMaq = dgv_Liberaciones.HeaderRow.FindControl("DropSelecMaq") as DropDownList;
            DropSelecMaq.AutoPostBack = true;
            DropSelecMaq.SelectedIndexChanged += new EventHandler(Rellenar_Grid);
            

            DropDownList DropSelecEstado = dgv_Liberaciones.HeaderRow.FindControl("DropSelecEstado") as DropDownList;
            DropSelecEstado.AutoPostBack = true;
            DropSelecEstado.SelectedIndexChanged += new EventHandler(Rellenar_Grid);

            DropDownList DropOrderByPrior = dgv_Liberaciones.HeaderRow.FindControl("DropPrioridad") as DropDownList;
            DropOrderByPrior.AutoPostBack = true;
            DropOrderByPrior.SelectedIndexChanged += new EventHandler(Rellenar_Grid);

        }

        protected void Rellenar_Grid(object sender, EventArgs e)
        {
            Conexion_MATERIALES conexion = new Conexion_MATERIALES();
            Conexion_SMARTH SHConexion = new Conexion_SMARTH();
            SHConexion.LimpiarOrdenesProduciendoBMS();
            SHConexion.leer_OrdenesProduciendoBMS();
            /////////////////////////////********LIBERACIONES********\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            string FILTRO = "";
            string ORDERBY = " ORDER BY p.Maquina";

            if (dgv_Liberaciones.Rows.Count>0)
            {
                // Retrieve header

                DropDownList DropSelecMaqCAST = dgv_Liberaciones.HeaderRow.FindControl("DropSelecMaq") as DropDownList;
                DropDownList DropSelecEstadoCAST = dgv_Liberaciones.HeaderRow.FindControl("DropSelecEstado") as DropDownList;
                DropDownList DropOrderByPriorCAST = dgv_Liberaciones.HeaderRow.FindControl("DropPrioridad") as DropDownList;

                switch (DropSelecMaqCAST.SelectedValue)
                {
                    case "Máq.":
                        FILTRO = FILTRO + "";
                        break;
                    default:
                        FILTRO = FILTRO + " AND M.Ubicación = '" + DropSelecMaqCAST.SelectedValue + "'";
                        break;
                }
                SELECNAVE = DropSelecMaqCAST.SelectedValue.ToString();

                switch (DropSelecEstadoCAST.SelectedValue)
                {
                    case "1":
                        FILTRO = FILTRO + " AND EstadoMaquina = 'En marcha'";
                        break;
                    case "2":
                        FILTRO = FILTRO + " AND EstadoMaquina <> 'En marcha'";
                        break;
                    default:
                        break;
                }
                SELECESTADO = DropSelecEstadoCAST.SelectedValue.ToString();

                switch (DropOrderByPriorCAST.SelectedValue)
                {
                    case "1":
                        ORDERBY = " ORDER BY PRIORIDAD ASC";
                        break;
                    case "2":
                        ORDERBY = " ORDER BY PRIORIDAD DESC";
                        break;
                    default:
                        break;
                }
                SELECORDERBY = DropOrderByPriorCAST.SelectedValue.ToString();


            }
            ds_Liberaciones = SHConexion.Devuelve_Panel_Liberaciones(FILTRO, ORDERBY);
            dgv_Liberaciones.DataSource = ds_Liberaciones;
            dgv_Liberaciones.DataBind();

            /////////////////////////////********LISTADO MATERIALES********\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            string[] RecorteMAT = NUMMaterialFiltro.Value.ToString().Split(new char[] { '¬' });
           
            string filtroNAV = "I.[No_] LIKE '2%'";
            string filtroSMARTH = "";

            if (NUMMaterialFiltro.Value != "") //NUMERO MATERIAL
            {
                filtroNAV = filtroNAV + "AND I.[No_] LIKE '" + RecorteMAT[0].Trim().ToString() + "%'";
                filtroSMARTH = filtroSMARTH + " AND Referencia LIKE '" + RecorteMAT[0].Trim().ToString() + "%'";
            }
            if (DropSecadoFiltro.SelectedValue != "-") //SECADO SI/NO
            {
                filtroSMARTH = filtroSMARTH + " AND [Secado] LIKE '" + DropSecadoFiltro.SelectedValue + "%'";
            }
            if (DropDocumentosFiltro.SelectedValue != "-") //DOCUMENTOS ADJUNTOS
            {
                switch (DropDocumentosFiltro.SelectedValue)
                {
                    case "0":
                        filtroSMARTH = filtroSMARTH + " AND ([FichaMaterial] IS NULL OR [FichaMaterial] = '')";
                        break;
                    case "1":
                        filtroSMARTH = filtroSMARTH + " AND ([FichaSeguridad] IS NULL OR [FichaSeguridad] = '')";
                        break;
                    case "2":
                        filtroSMARTH = filtroSMARTH + " AND ([FichaSeguridad] IS NULL OR [FichaSeguridad] = '') AND ([FichaMaterial] IS NULL OR [FichaMaterial] = '')";
                        break;
                }



            }
            if (DropTipoFiltro.SelectedValue != "-") //TIPO
            {
                if (DropTipoFiltro.SelectedValue == "Sin definir")
                {
                    filtroSMARTH = filtroSMARTH + " AND ([TipoMaterial] IS NULL OR [TipoMaterial] = '')";
                }
                else
                {
                    filtroSMARTH = filtroSMARTH + " AND [TipoMaterial] LIKE '" + DropTipoFiltro.SelectedValue + "%'";
                }
            }
            if (DropRecicladoFiltro.SelectedValue != "-") //RECICLADO
            {
                filtroSMARTH = filtroSMARTH + " AND ([ReferenciaReciclado] <> '0')";
            }
            //FAMILIA
            if (DropFamiliaFiltro.SelectedValue != "-") //RECICLADO
            {
                filtroNAV = filtroNAV + " AND I.[Item Category Code] = '" + conexion.Devuelve_ID_Familias_Productos_NAV(DropFamiliaFiltro.SelectedValue) + "'";
            }
            DT_Materiales = conexion.Devuelve_lista_materiales(filtroNAV);
            DT_MaterialesJoin = conexion.Devuelve_Lista_Material_SMARTH_LEFTJOIN_V1(filtroSMARTH);
            try
            {

                var JoinResultado = (from p in DT_Materiales.AsEnumerable()
                                  join t in DT_MaterialesJoin.AsEnumerable()
                                  on p.Field<string>("MATERIAL") equals t.Field<string>("Referencia")
                                  //into tempJoin from leftJoin in tempJoin.DefaultIfEmpty()
                                  select new
                                  {
                                      //NAV
                                      MATERIAL = p.Field<string>("MATERIAL"),
                                      DESCRIPCION = p.Field<string>("LONG_DESCRIPTION"),
                                      DISPONIBLE = p.Field<decimal>("CANTALM"),
                                      PREVISION = p.Field<string>("FECHA"),
                                      QUANTITY = p == null ? 0 : p.Field<decimal>("QUANTITY"),
                                      //SMARTH
                                      TIPOMATERIAL = t.Field<string>("TipoMaterial"),
                                      REFERENCIARECICLADO = t.Field<string>("ReferenciaReciclado"),
                                      SECADOHABILITADO = t.Field<string>("Secado"),
                                      SecadoTemp = t.Field<string>("SecadoTemp"),
                                      SECADOTIEMP = t.Field<string>("SECADOTIEMP"),
                                      FichaMaterial = t.Field<string>("FichaMaterial"),
                                      FichaSeguridad = t.Field<string>("FichaSeguridad"),
                                      UBICACION = t.Field<string>("UBICACION"),

                                  }).ToList();
                dgv_Materiales.DataSource = JoinResultado;
                dgv_Materiales.DataBind();
            }
            catch (Exception EX)
            { }

            /////////////////////////////********ENTRADAS PREVISTAS********\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            DataTable EntradasPrevistas = SHConexion.Devuelve_Entradas_Previstas_Materiales();
            dgv_EntradasPrevistas.DataSource = EntradasPrevistas;
            dgv_EntradasPrevistas.DataBind();


            /////////////////////////////********PREVISION DE SECADO********\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            DataTable PrevisionSecado = conexion.Devuelve_Prevision_SecadoV3();
            DataTable StocksDisponibles = conexion.Devuelve_Stock_MaterialV2();
            DataTable SMARTHMATERIALES = conexion.Devuelve_Lista_Material_SMARTH_LEFTJOIN_V1("");
            DataTable EstadoSecado = conexion.Devuelve_Entradas_Secado();

            //ds_ListaEntregas = SHconexion.Devuelve_listado_Recursos_Entregados(" AND [Employee No_] = " + lblNumOperario.Text + "");
            //ds_firmas = SHconexion.Devuelve_EPIS_ListaEntrega("");
            var JoinResult = (from PSev in PrevisionSecado.AsEnumerable() //Defino tabla maestra

                              join Stock in StocksDisponibles.AsEnumerable() on PSev.Field<string>("MATERIAL") equals Stock.Field<string>("MATERIAL") into tempJoin
                              from tempRow in tempJoin.DefaultIfEmpty() //JOIN A TABLA DE STOCKS

                              join STHMat in SMARTHMATERIALES.AsEnumerable() on PSev.Field<string>("MATERIAL") equals STHMat.Field<string>("Referencia") into tempJoin2
                              from tempRow2 in tempJoin2.DefaultIfEmpty() //JOIN A TABLA DE UBICACIONES Y SECADO

                              join EstSec in EstadoSecado.AsEnumerable() on new
                              {
                                  ORDEN = PSev.Field<string>("ORDEN").ToString(),
                                  ORDENVER = PSev.Field<string>("JOBVER").ToString(),
                                  MATJOIN = PSev.Field<string>("MATJOIN").ToString()
                              } equals new
                              {
                                  ORDEN = EstSec.Field<string>("ORDEN").ToString(),
                                  ORDENVER = EstSec.Field<string>("JOBVER").ToString(),
                                  MATJOIN = EstSec.Field<string>("MATJOIN").ToString()
                              } into tempJoin3
                              from tempRow3 in tempJoin3.DefaultIfEmpty()
                              select new
                              {
                                  SEQNR = PSev.Field<decimal>("SEQNR"),
                                  MAQ = PSev.Field<string>("MAQ"),
                                  FECHA = PSev.Field<string>("FECHA"),
                                  INICIARSECADO = PSev.Field<string>("INICIARSECADO"),
                                  ORDEN = PSev.Field<string>("ORDEN"),
                                  ORDENVER = PSev.Field<string>("JOBVER"),
                                  MATERIAL = PSev.Field<string>("MATERIAL"),
                                  DESCRIPCION = PSev.Field<string>("DESCRIPCION"),
                                  DESCRIPCIONLONG = PSev.Field<string>("DESCRIPCIONLONG"),
                                  DISPONIBLE = tempRow?.Field<decimal>("CANTALM"),
                                  PREVISION = tempRow?.Field<string>("FECHA"),
                                  INICIOSECADO = tempRow3?.Field<DateTime>("FECHA"),
                                  PREPARADOR = tempRow3?.Field<string>("NOMBRE"),
                                  PREPARAR = PSev.Field<string>("PREPARAR"),
                                  UBICACION = tempRow2?.Field<string>("UBICACION"),
                                  LOTE = tempRow2?.Field<string>("LOTE"),
                                  //LOTE = tempRow2?.Field<decimal>("LOTE"),
                                  TEMPSECADO = tempRow2?.Field<string>("SECADOTEMPSHORT"),
                                  TIEMPOSECADO = tempRow2?.Field<string>("SECADOTIEMPSHORT"),
                                  SECARSN = tempRow2?.Field<string>("SECADO"),
                                  NOTAS = PSev.Field<string>("NOTAS"),
                                  REPETICIONES = PSev.Field<Decimal>("REPETICIONES"),
                                  SUMAMATS = PSev.Field<Decimal>("SUMAMATS"),

                                  //REPEUBIS = (leftJoin2 != null) ? leftJoin2.Field<Decimal>("UBIACTIVAS") : 0,
                                  //REPEUBIS = leftJoin2.Field<Decimal>("UBIACTIVAS"),
                                  //FECHA = leftJoin.Field<string>("FECHA"),
                              });
            if (CheckMostrarPreparados.Checked == true)
            {
                JoinResult.ToList();
            }
            else
            {
                JoinResult = JoinResult.Where(item => item.PREPARADOR == "" || item.PREPARADOR == null).ToList();                
            }
                
            dgv_secado.DataSource = JoinResult;
            dgv_secado.DataBind();

            //ESTADO DE MÁQUINAS V2
            DataTable EstadoMaquina = conexion.Devuelve_Prevision_Secado_Pendientes_V2();
            var JoinResult2 = (from PSev in EstadoMaquina.AsEnumerable() //Defino tabla maestra
                              join Stock in StocksDisponibles.AsEnumerable() on PSev.Field<string>("MATERIAL") equals Stock.Field<string>("MATERIAL") into tempJoin
                              from tempRow in tempJoin.DefaultIfEmpty() //JOIN A TABLA DE STOCKS
                              join STHMat in SMARTHMATERIALES.AsEnumerable() on PSev.Field<string>("MATERIAL") equals STHMat.Field<string>("Referencia") into tempJoin2
                              from tempRow2 in tempJoin2.DefaultIfEmpty() //JOIN A TABLA DE UBICACIONES Y SECADO
                              join EstSec in EstadoSecado.AsEnumerable() on new
                              {
                                  ORDEN = PSev.Field<string>("ORDEN").ToString(),
                                  ORDENVER = PSev.Field<string>("JOBVER").ToString(),
                                  MATJOIN = PSev.Field<string>("MATJOIN").ToString()
                              } equals new
                              {
                                  ORDEN = EstSec.Field<string>("ORDEN").ToString(),
                                  ORDENVER = EstSec.Field<string>("JOBVER").ToString(),
                                  MATJOIN = EstSec.Field<string>("MATJOIN").ToString()
                              } into tempJoin3
                              from tempRow3 in tempJoin3.DefaultIfEmpty()
                              select new
                               {                                 
                                   MAQ = PSev.Field<string>("MAQ"),
                                   MATERIAL = PSev.Field<string>("MATERIAL"),
                                   DESCRIPCION = PSev.Field<string>("DESCRIPCION"),
                                   DESCRIPCIONLONG = PSev.Field<string>("DESCRIPCIONLONG"),
                                   UBICACION = tempRow2?.Field<string>("UBICACION"),
                                   NOTAS = PSev.Field<string>("NOTAS"),
                                   DISPONIBLE = tempRow?.Field<decimal>("CANTALM"),
                                   KGS_RESTANTE = PSev.Field<decimal>("KGS_RESTANTE"),
                                   ORDENES = PSev.Field<decimal>("ORDENES"),
                                   ORDEN = PSev.Field<string>("ORDEN"),
                                   ORDENVER = PSev.Field<string>("JOBVER"),
                                   PREVISION = tempRow?.Field<string>("FECHA"),
                                   INICIOSECADO = tempRow3?.Field<DateTime>("FECHA"),
                                   PREPARADOR = tempRow3?.Field<string>("NOMBRE"),
                                   SECADOHABILITADO = tempRow2?.Field<string>("Secado"),
                                   SecadoTemp = tempRow2?.Field<string>("SecadoTemp"),
                                   SECADOTIEMP = tempRow2?.Field<string>("SECADOTIEMP"),
                              });

            if (CheckMostrarPreparados.Checked == true)
            {
                JoinResult2.ToList();
            }
            else
            {
                JoinResult2 = JoinResult2.Where(item => item.PREPARADOR == "" || item.PREPARADOR == null).ToList();
            }

            //ESTADO DE MÁQUINAS

            dgv_estado_maquina.DataSource = JoinResult2;
            dgv_estado_maquina.DataBind();

            //RECARGAR CABECERAS
            DropDownList DropSelecMaq = dgv_Liberaciones.HeaderRow.FindControl("DropSelecMaq") as DropDownList;
            DropSelecMaq.AutoPostBack = true;
            DropSelecMaq.SelectedIndexChanged += new EventHandler(Rellenar_Grid);
            

            DropDownList DropSelecEstado = dgv_Liberaciones.HeaderRow.FindControl("DropSelecEstado") as DropDownList;
            DropSelecEstado.AutoPostBack = true;
            DropSelecEstado.SelectedIndexChanged += new EventHandler(Rellenar_Grid);

            DropDownList DropOrderByPrior = dgv_Liberaciones.HeaderRow.FindControl("DropPrioridad") as DropDownList;
            DropOrderByPrior.AutoPostBack = true;
            DropOrderByPrior.SelectedIndexChanged += new EventHandler(Rellenar_Grid);

            try
            {
                HtmlButton Boton = (HtmlButton)sender;
                if (Boton.ID == "BTNFiltraMateriales")
                {
                    lkb_Sort_Click("TABLAMATERIALES");
                }
            }
            catch (Exception)
            { }

        }

        protected void GridView_DataBound_Liberaciones(object sender, EventArgs e)
        {
           

            for (int i = 0; i <= dgv_Liberaciones.Rows.Count - 1; i++)
            {
                DropDownList DropSelecEstado = dgv_Liberaciones.HeaderRow.FindControl("DropSelecEstado") as DropDownList;
                DropSelecEstado.SelectedValue = SELECESTADO;
                DropDownList DropSelecMaq = dgv_Liberaciones.HeaderRow.FindControl("DropSelecMaq") as DropDownList;
                DropSelecMaq.SelectedValue = SELECNAVE;

                Label lblparent = (Label)dgv_Liberaciones.Rows[i].FindControl("lblEstadoCambio");
                if (lblparent.Text == "Pendiente")
                {
                    //dgv_Liberaciones.Rows[i].Cells[6].BackColor = System.Drawing.Color.Yellow;
                    lblparent.ForeColor = System.Drawing.Color.Red;
                }

                if (lblparent.Text == "OK Condicionada")
                {
                    dgv_Liberaciones.Rows[i].Cells[5].BackColor = System.Drawing.Color.Orange;
                    lblparent.ForeColor = System.Drawing.Color.Black;
                    Label auxparent1 = (Label)dgv_Liberaciones.Rows[i].FindControl("lblfechaCambiador");
                    auxparent1.ForeColor = System.Drawing.Color.Black;
                }

                if (lblparent.Text == "Liberada OK")
                {
                    dgv_Liberaciones.Rows[i].Cells[5].BackColor = System.Drawing.Color.Green;
                    lblparent.ForeColor = System.Drawing.Color.White;
                    Label auxparent1 = (Label)dgv_Liberaciones.Rows[i].FindControl("lblfechaCambiador");
                    auxparent1.ForeColor = System.Drawing.Color.White;
                }

                Label lblparent2 = (Label)dgv_Liberaciones.Rows[i].FindControl("lblEstadoProduccion");
                if (lblparent2.Text == "Pendiente")
                {
                    //dgv_Liberaciones.Rows[i].Cells[7].BackColor = System.Drawing.Color.Yellow;
                    lblparent2.ForeColor = System.Drawing.Color.Red;
                }

                if (lblparent2.Text == "OK Condicionada")
                {
                    dgv_Liberaciones.Rows[i].Cells[6].BackColor = System.Drawing.Color.Orange;
                    lblparent2.ForeColor = System.Drawing.Color.Black;
                    Label auxparent2 = (Label)dgv_Liberaciones.Rows[i].FindControl("lblfechaproduccion");
                    auxparent2.ForeColor = System.Drawing.Color.Black;
                }

                if (lblparent2.Text == "Liberada OK")
                {
                    dgv_Liberaciones.Rows[i].Cells[6].BackColor = System.Drawing.Color.Green;
                    lblparent2.ForeColor = System.Drawing.Color.White;
                    Label auxparent2 = (Label)dgv_Liberaciones.Rows[i].FindControl("lblfechaproduccion");
                    auxparent2.ForeColor = System.Drawing.Color.White;
                }

                Label lblparent3 = (Label)dgv_Liberaciones.Rows[i].FindControl("lblEstadoCalidad");
                if (lblparent3.Text == "Pendiente")
                {
                    //dgv_Liberaciones.Rows[i].Cells[8].BackColor = System.Drawing.Color.Yellow;
                    lblparent3.ForeColor = System.Drawing.Color.Red;
                }

                if (lblparent3.Text == "OK Condicionada")
                {
                    dgv_Liberaciones.Rows[i].Cells[7].BackColor = System.Drawing.Color.Orange;
                    lblparent3.ForeColor = System.Drawing.Color.Black;
                    Label auxparent3 = (Label)dgv_Liberaciones.Rows[i].FindControl("lblfechaCalidad");
                    auxparent3.ForeColor = System.Drawing.Color.Black;

                }
                if (lblparent3.Text == "Liberada OK")
                {
                    dgv_Liberaciones.Rows[i].Cells[7].BackColor = System.Drawing.Color.Green;
                    lblparent3.ForeColor = System.Drawing.Color.White;
                    Label auxparent3 = (Label)dgv_Liberaciones.Rows[i].FindControl("lblfechaCalidad");
                    auxparent3.ForeColor = System.Drawing.Color.White;
                }

                Label lblparent4 = (Label)dgv_Liberaciones.Rows[i].FindControl("lblEstadoActual");
                if (lblparent4.Text == "En marcha")
                {
                    //dgv_Liberaciones.Rows[i].Cells[8].BackColor = System.Drawing.Color.Yellow;
                    lblparent4.ForeColor = System.Drawing.Color.Black;
                    dgv_Liberaciones.Rows[i].Cells[0].BackColor = System.Drawing.Color.LightGreen;
                }

                else if (lblparent4.Text == "")
                {
                    lblparent4.ForeColor = System.Drawing.Color.Black;
                    dgv_Liberaciones.Rows[i].Cells[0].BackColor = System.Drawing.Color.LightPink;
                }
                else
                {
                    lblparent4.ForeColor = System.Drawing.Color.Black;
                    dgv_Liberaciones.Rows[i].Cells[0].BackColor = System.Drawing.Color.LightGoldenrodYellow;
                }

                Label Accion = (Label)dgv_Liberaciones.Rows[i].FindControl("lblAccionLiberado");
                if (Accion.Text == "4")
                {
                    dgv_Liberaciones.Rows[i].BackColor = System.Drawing.Color.Red;
                    Accion.ForeColor = System.Drawing.Color.White;
                    Accion.Visible = true;
                    Accion.Text = "Liberación NOK. Producción retenida";
                }
               
            }
          

        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label REPETECIONES = (Label)e.Row.FindControl("lblRepiticiones");
                    Label CONSUMO = (Label)e.Row.FindControl("lblConsumo");
                    HiddenField SECADO = (HiddenField)e.Row.FindControl("HiddenInicioSecado");
                    Label LABELSECADO = (Label)e.Row.FindControl("lblFechasecado");
                    LinkButton MATERIALPREP = (LinkButton)e.Row.FindControl("BTNMaterialPrep");
                    
                    HiddenField TEMPSECADO = (HiddenField)e.Row.FindControl("HiddenTempSecado");
                    HiddenField HiddenSecadoSN = (HiddenField)e.Row.FindControl("HiddenSecadoSN");
                    Label LABELDETALLESECADO = (Label)e.Row.FindControl("lblSecaDetalle");

                    

                    if (SECADO.Value != "")
                    {
                        LABELSECADO.Visible = true;
                        MATERIALPREP.Visible = false;
                    }

                    if (TEMPSECADO.Value == "Temp.: 0 °C")
                    {
                        LABELDETALLESECADO.Visible = false;
                    }

                    if (TEMPSECADO.Value != "Temp.: 0 °C" && HiddenSecadoSN.Value == "NO")
                    {
                        LABELDETALLESECADO.Text = "[NS]" + LABELDETALLESECADO.Text;
                    }


                    if (REPETECIONES.Text != "1")
                    {
                        CONSUMO.Visible = true;
                    }


                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void OnRowDataBound2(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                   Label lblRestantes = (Label)e.Row.FindControl("lblRestantes");
                    Label lblNecesidad = (Label)e.Row.FindControl("lblNecesidad");
                    if (lblRestantes.Text.Substring(0, 1) == "-")// || lblRestantes.Text.Substring(0, 1) == "0")
                    {
                        lblRestantes.Text = "¡Producción sobrepasada!";
                        lblNecesidad.Visible = false;
                    }

                    HiddenField SECADO = (HiddenField)e.Row.FindControl("HiddenInicioSecado");
                    Label LABELSECADO = (Label)e.Row.FindControl("lblFechasecado");
                    LinkButton MATERIALPREP = (LinkButton)e.Row.FindControl("BTNMaterialPrep");
                    if (SECADO.Value != "")
                    {
                        LABELSECADO.Visible = true;
                        MATERIALPREP.Visible = false;
                    }

                    Label lblSecadoHabilitado = (Label)e.Row.FindControl("lblSecadoHabilitado");
                    Label lblSecTemp = (Label)e.Row.FindControl("lblSecTemp");
                    Label lblSecTiemp = (Label)e.Row.FindControl("lblSecTiemp");
                    if (lblSecadoHabilitado.Text == "SI")
                    {
                        lblSecadoHabilitado.Visible = false;
                        if (lblSecTemp.Text == "Temperatura: 0 °C")
                        {
                            lblSecTemp.Visible = false;
                            lblSecTiemp.Visible = false;
                        }
                    }
                    else
                    {
                        lblSecadoHabilitado.Text = "-";
                        lblSecTemp.Visible = false;
                        lblSecTiemp.Visible = false;
                    }
                    HiddenField TEMPSECADO = (HiddenField)e.Row.FindControl("HiddenTempSecado");
                    HiddenField HiddenSecadoSN = (HiddenField)e.Row.FindControl("HiddenSecadoSN");
                    Label LABELDETALLESECADO = (Label)e.Row.FindControl("lblSecaDetalle");

                    if (TEMPSECADO.Value != "Temperatura: 0 °C" && HiddenSecadoSN.Value == "NO")
                    {
                        lblSecadoHabilitado.Visible = false;
                        lblSecTemp.Visible = true;
                        lblSecTiemp.Visible = true;
                        lblSecTemp.Text = "[NS]" + lblSecTemp.Text;
                    }
                    // Label CONSUMO = (Label)e.Row.FindControl("lblConsumo");


                    //if (REPETECIONES.Text != "1")
                    // {
                    //     CONSUMO.Visible = true;
                    //}


                }
            }
            catch (Exception ex)
            {
            }
        }
        protected void OnRowDataBound3(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblSecadoHabilitado = (Label)e.Row.FindControl("lblSecadoHabilitado");
                    Label lblSecTemp = (Label)e.Row.FindControl("lblSecTemp");
                    Label lblSecTiemp = (Label)e.Row.FindControl("lblSecTiemp");
                    Label lblPrevision = (Label)e.Row.FindControl("lblPrevision");
                    Label lblRecicla = (Label)e.Row.FindControl("lblRecicla");
                    Label lblUbicacionAUX = (Label)e.Row.FindControl("lblUbicacionAUX");
                    Label lblUbicacion = (Label)e.Row.FindControl("lblUbicacion");


                    HiddenField LBLICONOTEC = (HiddenField)e.Row.FindControl("LBLICONOTEC");
                    HiddenField LBLICONOSEC = (HiddenField)e.Row.FindControl("LBLICONOSEC");
                    LinkButton IMGICONOTEC = (LinkButton)e.Row.FindControl("IMGICONOTEC");
                    LinkButton IMGICONOSEC = (LinkButton)e.Row.FindControl("IMGICONOSEC");

                    if (lblUbicacion.Text != "")
                    {
                        lblUbicacionAUX.Visible = true;
                    }

                    if (lblSecadoHabilitado.Text == "SI")
                    {
                        lblSecadoHabilitado.Visible = false;
                        if (lblSecTemp.Text == "Temperatura: 0 °C")
                        {
                            lblSecTemp.Visible = false;
                            lblSecTiemp.Visible = false;
                        }
                    }
                    else
                    {
                        lblSecTemp.Visible = false;
                        lblSecTiemp.Visible = false;
                    }

                    if (LBLICONOTEC.Value != "")
                    {
                        IMGICONOTEC.Visible = true;
                    }
                    if (LBLICONOSEC.Value != "")
                    {
                        IMGICONOSEC.Visible = true;
                    }
                    if (lblRecicla.Text == " - Recicla en: 0" || lblRecicla.Text == " - Recicla en: -")
                    {
                        lblRecicla.Text = "";
                    }
                    if (lblPrevision.Text == "Previsión:  - 0 uds.")
                    {
                        lblPrevision.Text = "Sin previsión.";
                    }


                    /*
                    Label REPETECIONES = (Label)e.Row.FindControl("lblRepiticiones");
                    Label CONSUMO = (Label)e.Row.FindControl("lblConsumo");


                    if (REPETECIONES.Text != "1")
                    {
                        CONSUMO.Visible = true;
                    }
                    */

                }
            }
            catch (Exception ex)
            {
            }
        }

        public void GridViewCommandEventHandler(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "ImprimirEtiqueta")
                {
                    string[] RecorteMAT = e.CommandArgument.ToString().Split(new char[] { '¬' });
                    AUX_MATERIAL.Value = RecorteMAT[0].Trim();
                    AUX_DESCRIPCION.Value = RecorteMAT[1].Trim();
                    LblImpDESCMAT.Text = RecorteMAT[0].Trim() + " " + RecorteMAT[1].Trim();
                    InputOperario.SelectedValue = "0";
                    InputLote.Text = "";
                    lkb_Sort_Click(RecorteMAT[2].Trim());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupImprimirEtiquetas();", true);
                }
                if (e.CommandName == "ImprimirEtiquetaEST")
                {
                    string[] RecorteMAT = e.CommandArgument.ToString().Split(new char[] { '¬' });
                    AUX_MATERIAL.Value = RecorteMAT[0].Trim();
                    AUX_DESCRIPCION.Value = RecorteMAT[1].Trim();
                    LblImpDESCMAT.Text = RecorteMAT[0].Trim() + " " + RecorteMAT[1].Trim();
                    InputOperario.SelectedValue = "0";
                    InputLote.Text = "";
                    InputCantidad.Text = "";
                    lkb_Sort_Click(RecorteMAT[2].Trim());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupImprimirEtiquetas();", true);
                }
                if (e.CommandName == "Estructura")
                {
                    
                    Conexion_SMARTH conexion = new Conexion_SMARTH();
                    DataSet Estructura = conexion.DevuelveEstructuraMaquina(e.CommandArgument.ToString());
                    DataSet EstructuraAgrupada = conexion.DevuelveEstructuraMaquinaAgrupada(e.CommandArgument.ToString());
                    Estructura.Tables[0].DefaultView.RowFilter = "PRODUCTO = '"+Estructura.Tables[0].Rows[0]["PRODUCTO"].ToString() + "'";
                    DataTable DTEstructura = (Estructura.Tables[0].DefaultView).ToTable();
                    lblEstructuraProducto.InnerText = DTEstructura.Rows[0]["PRODUCTO"].ToString() + " " + DTEstructura.Rows[0]["C_PRODLONGDESCR"].ToString();
                    GridEstructura.DataSource = DTEstructura;
                    GridEstructura.DataBind();
                    GridEstructuraOrden.DataSource = EstructuraAgrupada;
                    GridEstructuraOrden.DataBind();
                    lkb_Sort_Click("TABLAESTADO");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupEstructura();", true);
                }
                if (e.CommandName == "EstructuraMSEQ")
                {
                    string[] Recorte = e.CommandArgument.ToString().Split(new char[] { '¬' });
                    string maquina = Recorte[0].Trim();
                    string seq = Recorte[1].Trim();
                    string orden = Recorte[2].Trim();
                    Conexion_SMARTH conexion = new Conexion_SMARTH();
                    DataSet Estructura = conexion.DevuelveEstructuraOrden(orden);
                    DataSet EstructuraAgrupada = conexion.DevuelveEstructuraOrdenAgrupada(seq, maquina);
                    Estructura.Tables[0].DefaultView.RowFilter = "PRODUCTO = '" + Estructura.Tables[0].Rows[0]["PRODUCTO"].ToString() + "'";
                    DataTable DTEstructura = (Estructura.Tables[0].DefaultView).ToTable();
                    lblEstructuraProducto.InnerText = DTEstructura.Rows[0]["PRODUCTO"].ToString() + " " + DTEstructura.Rows[0]["C_PRODLONGDESCR"].ToString();
                    GridEstructura.DataSource = DTEstructura;
                    GridEstructura.DataBind();
                    GridEstructuraOrden.DataSource = EstructuraAgrupada;
                    GridEstructuraOrden.DataBind();
                    lkb_Sort_Click("SECADO"); 
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupEstructura();", true);
                }
                if (e.CommandName == "Redirect")
                {
                    Response.Redirect("UbicacionMateriasPrimas.aspx?UBI=" + e.CommandArgument);
                    e.CommandArgument.ToString();
                }
                if (e.CommandName == "EditarMaterial")
                {
                    TxtTiempMIN.Value = "";
                    TxtTiempMAX.Value = "";
                    TxtTempMIN.Value = "";
                    TxtTempMAX.Value = "";
                    hyperlink1.NavigateUrl = "";
                    hyperlink1.ImageUrl = "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg";
                    hyperlink2.NavigateUrl = "";
                    hyperlink2.ImageUrl = "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg";
                    HiddenMaterial.Value = "";
                    DropTipoMaterial.SelectedValue = "-";
                    DropUploadSelect.SelectedValue = "-";


                    Conexion_MATERIALES conexion = new Conexion_MATERIALES();

                    HiddenMaterial.Value = e.CommandArgument.ToString();
                    DataTable DT_MaterialesAux = conexion.Devuelve_Lista_Material_SMARTH_LEFTJOIN_V1(" AND Referencia LIKE '" + e.CommandArgument.ToString() + "%'");
                    ModalNombreMaterial.InnerText = "Editar: " + DT_MaterialesAux.Rows[0]["REFERENCIA"].ToString() + " " + DT_MaterialesAux.Rows[0]["Descripcion"].ToString();
                    if (DT_MaterialesAux.Rows[0]["SECADO"].ToString() == "SI")
                    {
                        successoutlined2.Checked = true;
                        dangeroutlined2.Checked = false;

                    }
                    else
                    {
                        successoutlined2.Checked = false;
                        dangeroutlined2.Checked = true;

                    }
                    if (DT_MaterialesAux.Rows[0]["ReferenciaReciclado"].ToString() == "0")
                    {
                        //RecicladoPill.Visible = false;
                        //RecicladoInput.Visible = true;
                    }
                    else
                    {
                        DataTable AUXREC = conexion.Devuelve_Material_Reciclado_Insertar(DT_MaterialesAux.Rows[0]["ReferenciaReciclado"].ToString());
                        lblMatReciclado.Text = AUXREC.Rows[0]["MATDESC"].ToString();
                        //RecicladoPill.Visible = true;
                        //RecicladoInput.Visible = false;
                    }
                    if (DT_MaterialesAux.Rows[0]["TipoMaterial"].ToString() != "")
                    {
                        DropTipoMaterial.SelectedValue = DT_MaterialesAux.Rows[0]["TipoMaterial"].ToString();
                    }

                    TxtTiempMIN.Value = DT_MaterialesAux.Rows[0]["SecadoTiempoMIN"].ToString();
                    TxtTiempMAX.Value = DT_MaterialesAux.Rows[0]["SecadoTiempoMAX"].ToString();
                    TxtTempMIN.Value = DT_MaterialesAux.Rows[0]["SecadoTempMIN"].ToString();
                    TxtTempMAX.Value = DT_MaterialesAux.Rows[0]["SecadoTempMAX"].ToString();
                    if (DT_MaterialesAux.Rows[0]["FichaMaterial"].ToString() != "")
                    {
                        hyperlink1.NavigateUrl = DT_MaterialesAux.Rows[0]["FichaMaterial"].ToString();
                        hyperlink1.ImageUrl = "http://facts4-srv/thermogestion/SMARTH_fonts/INTERNOS/ICONODOCGENERICO.png";
                    }

                    if (DT_MaterialesAux.Rows[0]["FichaSeguridad"].ToString() != "")
                    {
                        hyperlink2.NavigateUrl = DT_MaterialesAux.Rows[0]["FichaSeguridad"].ToString();
                        hyperlink2.ImageUrl = "http://facts4-srv/thermogestion/SMARTH_fonts/INTERNOS/ICONODOCGENERICO.png";
                    }

                    DataTable MatXUBI = conexion.Devuelve_Lista_MaterialesXUbicacion(e.CommandArgument.ToString());
                    DataTable StocksDisponibles = conexion.Devuelve_Datos_Material_NAV(e.CommandArgument.ToString());
                    GridUbicacion.DataSource = MatXUBI;
                    GridUbicacion.DataBind();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupEditaDetalles();", true);

                }
                if (e.CommandName == "RedirectFicha")
                {
                    Response.Redirect(e.CommandArgument.ToString());
                }
                if (e.CommandName == "MaterialPreparado")
                {
                    string[] DECLARARSECADO = e.CommandArgument.ToString().Split(new char[] { '¬' });
                    try
                    {
                        
                        Conexion_MATERIALES conexion = new Conexion_MATERIALES();
                        Conexion_SMARTH SHConexion = new Conexion_SMARTH();
                        conexion.Insertar_Preparacion_Materiales(DECLARARSECADO[0].Trim(), Convert.ToInt32(DECLARARSECADO[1].Remove(0, 1)), DECLARARSECADO[2].Trim(), SHConexion.Devuelve_ID_Piloto_SMARTH(Session["Nombre"].ToString()));
                        Rellenar_Grid(null, null);
                        lkb_Sort_Click(DECLARARSECADO[3]);
                    }
                   catch(Exception ex)
                    {
                        lkb_Sort_Click(DECLARARSECADO[3]);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "AlertaSinLoguear();", true);
                    }
                }
            }
            catch (Exception ex)
            { }
        }
   
        protected void lkb_Sort_Click(string e)
        {
            //Bind the data

            //Manage the selected tab.
            this.ManageTabsPostBack(pill_secados, pills_listamateriales, pills_liberaciones, pills_ConsumoRestante, pills_entradamateriales, BTN_ESTADO_SECADO, BTN_LISTADO_PRODUCTOS, BTN_CONSUMO_MAQUINAS, BTN_LIBERACIONES, BTN_ENTRADAMATS, e);

        }

        private void ManageTabsPostBack(HtmlGenericControl pill_secados, HtmlGenericControl pills_listamateriales, HtmlGenericControl pills_liberaciones, HtmlGenericControl pills_ConsumoRestante, HtmlGenericControl pills_entradamateriales, HtmlButton BTN_ESTADO_SECADO, HtmlButton BTN_LISTADO_PRODUCTOS, HtmlButton BTN_CONSUMO_MAQUINAS, HtmlButton BTN_LIBERACIONES, HtmlButton BTN_ENTRADAMATS, string grid)
        {
            // desactivte all tabs and panes
            pill_secados.Attributes.Add("class", "tab-pane fade");
            BTN_ESTADO_SECADO.Attributes.Add("class", "nav-link");

            pills_listamateriales.Attributes.Add("class", "tab-pane fade");
            BTN_LISTADO_PRODUCTOS.Attributes.Add("class", "nav-link");

            pills_liberaciones.Attributes.Add("class", "tab-pane fade");
            BTN_LIBERACIONES.Attributes.Add("class", "nav-link");

            pills_ConsumoRestante.Attributes.Add("class", "tab-pane fade");
            BTN_CONSUMO_MAQUINAS.Attributes.Add("class", "nav-link");

            pills_entradamateriales.Attributes.Add("class", "tab-pane fade");
            BTN_ENTRADAMATS.Attributes.Add("class", "nav-link");


            //activate the the tab and pane the user was viewing
            switch (grid)
            {
                case "SECADO":
                    BTN_ESTADO_SECADO.Attributes.Add("class", "nav-link active");
                    pill_secados.Attributes.Add("class", "tab-pane fade show active");
                    break;
                case "TABLAMATERIALES":
                    BTN_LISTADO_PRODUCTOS.Attributes.Add("class", "nav-link active");
                    pills_listamateriales.Attributes.Add("class", "tab-pane fade show active");
                    break;
                case "LIBERACIONES":
                    BTN_LIBERACIONES.Attributes.Add("class", "nav-link active");
                    pills_liberaciones.Attributes.Add("class", "tab-pane fade show active");
                    break;
                case "TABLAESTADO":
                    BTN_CONSUMO_MAQUINAS.Attributes.Add("class", "nav-link active");
                    pills_ConsumoRestante.Attributes.Add("class", "tab-pane fade show active");
                    break;
                case "TABLAENTRADAS":
                    BTN_ENTRADAMATS.Attributes.Add("class", "nav-link active");
                    pills_entradamateriales.Attributes.Add("class", "tab-pane fade show active");
                    break;
            }
        }

        // carga la lista utilizando un filtro
        public void ContactsGridView_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Redirect")
            {
                Response.Redirect("../LIBERACIONES/LiberacionSerie.aspx?ORDEN=" + e.CommandArgument.ToString());
            }
        }

        //Relacionados Gestión Materiales.aspx
        public void ActualizarMaterial(object sender, EventArgs e)
        {
            Conexion_MATERIALES conexion = new Conexion_MATERIALES();

            double TiempMIN = 0;
            if (Double.TryParse(TxtTiempMIN.Value.Replace('.', ','), out TiempMIN))
                TiempMIN = Convert.ToDouble(TxtTiempMIN.Value.Replace('.', ','));
            double TiempMAX = 0;
            if (Double.TryParse(TxtTiempMAX.Value.Replace('.', ','), out TiempMAX))
                TiempMAX = Convert.ToDouble(TxtTiempMAX.Value.Replace('.', ','));
            double TempMIN = 0;
            if (Double.TryParse(TxtTempMIN.Value.Replace('.', ','), out TempMIN))
                TempMIN = Convert.ToDouble(TxtTempMIN.Value.Replace('.', ','));
            double TempMAX = 0;
            if (Double.TryParse(TxtTempMAX.Value.Replace('.', ','), out TempMAX))
                TempMAX = Convert.ToDouble(TxtTempMAX.Value.Replace('.', ','));

            bool secado = false;
            if (successoutlined2.Checked == true)
            { secado = true; }

            Insertar_documento(null, null);
            conexion.Actualiza_Material(HiddenMaterial.Value, Convert.ToInt32(TempMIN), Convert.ToInt32(TempMAX), Convert.ToInt32(TiempMIN), Convert.ToInt32(TiempMAX), Convert.ToInt32(secado), DropTipoMaterial.SelectedValue);
            //SI TIENE 



        }
        public void Insertar_documento(object sender, EventArgs e)
        {
            try
            {

                if (FileUpload1.HasFile && DropUploadSelect.SelectedValue == "1")
                {
                    GuardarArchivo(FileUpload1.PostedFile, 1);
                }
                if (FileUpload1.HasFile && DropUploadSelect.SelectedValue == "2")
                {
                    GuardarArchivo(FileUpload1.PostedFile, 2);
                }
                else
                { }

            }
            catch (Exception ex)
            {

            }
        }
        public void EliminarDocumento(object sender, EventArgs e)
        {
            string query = "";

            HtmlButton Boton = (HtmlButton)sender;
            if (Boton.ID == "BtnEliminaFTEC")
            {
                query = "[FichaMaterial]";
            }
            if (Boton.ID == "BtnEliminaFSEC")
            {
                query = "[FichaSeguridad]";
            }

            Conexion_MATERIALES conexion = new Conexion_MATERIALES();
            conexion.Edita_documento_Material(HiddenMaterial.Value, query, "");
            Rellenar_Grid(null, null);

        }
        private void GuardarArchivo(HttpPostedFile file, int numdoc)
        {
            try
            {
                // Specify the path to save the uploaded file to.
                Conexion_MATERIALES conexion = new Conexion_MATERIALES();

                string savePath = @"C:\inetpub_thermoweb\SMARTH_docs\MATERIALES\"; // Your code goes here
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }

                // Get the name of the file to upload.

                string fileName = "";
                string extension = "";
                switch (numdoc)
                {
                    case 1:
                        extension = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
                        fileName = FileUpload1.PostedFile.FileName + "_" + DateTime.Now.ToString("yyyy-MM-dd_HHmmss") + extension;
                        break;
                    case 2:
                        //fileName = FileUpload1.FileName;
                        extension = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
                        fileName = FileUpload1.PostedFile.FileName + "_" + DateTime.Now.ToString("yyyy-MM-dd_HHmmss") + extension;
                        break;
                    default: break;
                }

                savePath += fileName;

                // Call the SaveAs method to save the uploaded file to the specified directory.
                string savePathVirtual = "";
                switch (numdoc)
                {
                    case 1:
                        FileUpload1.SaveAs(savePath);
                        savePathVirtual = "..\\SMARTH_docs\\MATERIALES\\";
                        savePathVirtual += fileName;
                        hyperlink1.ImageUrl = "../SMARTH_docs/MATERIALES/" + fileName;
                        hyperlink1.NavigateUrl = "../SMARTH_docs/MATERIALES/" + fileName;
                        conexion.Edita_documento_Material(HiddenMaterial.Value, "[FichaMaterial]", savePathVirtual);
                        break;
                    case 2:
                        FileUpload1.SaveAs(savePath);
                        savePathVirtual = "..\\SMARTH_docs\\MATERIALES\\";
                        savePathVirtual += fileName;
                        hyperlink2.ImageUrl = "../SMARTH_docs/MATERIALES/" + fileName;
                        hyperlink2.NavigateUrl = "../SMARTH_docs/MATERIALES/" + fileName;
                        conexion.Edita_documento_Material(HiddenMaterial.Value, "[FichaSeguridad]", savePathVirtual);
                        break;
                    default: break;
                }
                Rellenar_Grid(null, null);
            }

            catch (Exception ex)
            {
            }

        }

        [WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]       
        public static bool ImprimirEtiquetasV2(string MATERIAL, string DESCRIPCION, string INPUTOPERARIO, string LOTE, string TIPO, string CANTIDAD)
        {

            // Printer IP Address and communication port
            string ipAddress = "10.0.0.180"; //MATERIALES
            //string ipAddress = "10.0.0.164"; //THERMOBMS
            int port = 9100;
            //string producto = AUX_MATERIAL.Value.ToString();
            string producto = MATERIAL;
            string cantidadp = CANTIDAD;
            //string descripcion = AUX_DESCRIPCION.Value.ToString();
            string descripcion = DESCRIPCION;
            //string operario = InputOperario.SelectedValue.ToString();
            string operario = INPUTOPERARIO;
            string fechayhora = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            //string lote = InputLote.Text.ToString();
            string lote = LOTE;

            string ZPLEstufa = "^XA^[L,2^FS^XZ" +
                                "^XA" +
                                "^MMT" +
                                "^LS0^LT0" +
                                "^FWR^FPH" +
                                "^MTD" +
                                "^MNA" +
                                "^LH2,10^FS" +
                                "^PW815^ML2480" +
                                "^FX Lineas verticales" +
                                "^FO15,25^GB785,1,3^FS" +
                                "^FO600,1025^GB200,1,3^FS" +
                                "^FO55,1025^GB145,1,3^FS" +
                                "^FO15,1625^GB785,1,3^FS" +
                                "^FX Lineas horizontales" +
                                "^FO800,25^GB1,1600,3^FS" +
                                "^FO600,25^GB1,1600,3^FS" +
                                "^FO200,25^GB1,1600,3^FS" +
                                "^FO15,25^GB1,1600,3^FS" +
                                "^FO55,25^GB1,1600,3^FS" +
                                "^FO15,25^GB30,1600,50,20,0^FS" +
                                "^FX Descripciones de campo" +
                                "^FO755,35^A0R35,35^FDPRODUCTO:^FS" +
                                "^FO555,35^A0R35,35^FDDESCRIPCION:^FS" +
                                "^FO755,1040^A0R35,35^FDLOTE:^FS" +
                                "^FO155,35^A0R35,35^FDFECHA:^FS" +
                                "^FO155,1040^A0R35,35^FDOPERARIO:^FS" +
                                "^FO17,50^FR^FB1600,1,0,C,0^A0R35,35^FDETIQUETA PARA IDENTIFICAR LA ESTUFA^FS" +
                                "^ FX Campos variables" +
                                "^FO600,105^A0R130,130^FD" + producto + "^FS" +
                                "^FO600,1100^A0R130,130^FD" + lote + "^FS" +
                                "^FO400,50^FB1625,2,0,J,0^A0R,130,130^" +
                                "^FO0,45^A0R70,70^FD" + fechayhora + "^FS" +
                                "^FO70,1145^A0R70,70^FD" + operario + "^FS" +
                                "^FS" +
                                "^FO160,50^FB1600,3,0,C,0^A0R,110,110^FD" + descripcion + "^FS" +
                                "^FX Codigos de barra" +
                               // "^FO615,680^BCR,170,N,N,N,N^FD>:30S" + producto + "^FS" +
                                "^FO605,680^BXR,4,200,48,48^FD[]>E2*06_P" + producto + "_Q" + 1 + "_T" + lote + "_1T" + 0 + "_V" + 0 + "_K" + 0 + "E2*11^FS"+
                                "^XZ";

            string ZPLMaterial= "^XA^[L,2^FS^XZ" +
                                "^XA" +
                                "^MMT" +
                                "^LS0^LT0" +
                                "^FWR^FPH" +
                                "^MTD" +
                                "^MNA" +
                                "^LH2,10^FS" +
                                "^PW815^MNM^ML2480" +
                                "^FX Lineas verticales" +
                                "^FO15,25^GB785,1,3^FS" +
                                "^FO600,1025^GB200,1,3^FS" +
                                "^FO55,1025^GB145,1,3^FS" +
                                "^FO15,1625^GB785,1,3^FS" +
                                "^FX Lineas horizontales" +
                                "^FO800,25^GB1,1600,3^FS" +
                                "^FO600,25^GB1,1600,3^FS" +
                                "^FO200,25^GB1,1600,3^FS" +
                                "^FO15,25^GB1,1600,3^FS" +
                                "^FO55,25^GB1,1600,3^FS" +
                                "^FO15,25^GB30,1600,50,20,0^FS" +
                                "^FX Descripciones de campo" +
                                "^FO755,35^A0R35,35^FDPRODUCTO:^FS" +
                                "^FO555,35^A0R35,35^FDDESCRIPCION:^FS" +
                                "^FO755,1040^A0R35,35^FDLOTE:^FS" +
                                "^FO155,35^A0R35,35^FDFECHA:^FS" +
                                "^FO155,1040^A0R35,35^FDOPERARIO:^FS" +
                                "^FO17,50^FR^FB1600,1,0,C,0^A0R35,35^FDETIQUETA PARA IDENTIFICAR EL PALET/OCTAVIN^FS" +
                                "^ FX Campos variables" +
                                "^FO600,105^A0R130,130^FD" + producto + "^FS" +
                                "^FO600,1100^A0R130,130^FD" + lote + "^FS" +
                                "^FO400,50^FB1625,2,0,J,0^A0R,130,130^" +
                                "^FO0,45^A0R70,70^FD" + fechayhora + "^FS" +
                                "^FO70,1145^A0R70,70^FD" + operario + "^FS" +
                                "^FS" +
                                "^FO160,50^FB1600,3,0,C,0^A0R,110,110^FD" + descripcion + "^FS" +
                                "^FX Codigos de barra" +
                                //"^FO615,680^BCR,170,N,N,N,N^FD>:30S" + producto + "^FS" +
                                "^FO605,680^BXR,4,200,48,48^FD[]>E2*06_P" + producto + "_Q" + 1 + "_T" + lote + "_1T" + 0 + "_V" + 0 + "_K" + 0 + "E2*11^FS" +
                                "^XZ";

            string ZPLAuxiliar = "^XA" +
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
                                 "^FO15,600^FB280,1,0,C,0^A0R,90,90^FD"+ cantidadp + "^FS" +
                                 "^FO580,620^BXR,4,200,48,48^FD[]>E2*06_P" + producto + "_Q1"+ cantidadp + "_T" + lote + "_1T-_V-_K-E2*11^FS" +
                                 "^XZ";


            string ZPLString = "";
            switch (TIPO)
            {
                case "0":
                    ZPLString = ZPLMaterial + ZPLEstufa; ;
                    break;
                case "1":
                    ZPLString = ZPLEstufa; ;
                    break;
                case "2":
                    ZPLString = ZPLMaterial;
                    break;
                case "3":
                    ZPLString = ZPLAuxiliar;
                    break;
                default:
                    ZPLString = ZPLMaterial + ZPLEstufa;
                    break;
            }
            // ZPL Command(s)
            try
            {
                
                // Open connection
                System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient();
                client.Connect(ipAddress, port);

                // Write ZPL String to connection
                System.IO.StreamWriter writer = new System.IO.StreamWriter(client.GetStream());
                writer.Write(ZPLString);
                writer.Flush();

                // Close Connection
                writer.Close();
                client.Close();
                
                return true;
                
            }
            catch (Exception ex)
            {
                return false;

                // Catch Exception
            }
        }

        [WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static string Inserta_Material_Reciclado(string MATERIAL, string RECICLADO)
        {
            //Recupero el dato y lo filtro por la peticion
            Conexion_SMARTH SHConexion = new Conexion_SMARTH();
            Conexion_MATERIALES conexion = new Conexion_MATERIALES();
            //Compruebo que el material existe
            string[] RECORTERECICLADO = RECICLADO.Split(new char[] { '¬' });
            DataTable AuxMat = conexion.Devuelve_Material_Reciclado_Insertar(RECORTERECICLADO[0]);
            //Actualizo material y genero el texto para devolver a la etiqueta.
            string RECMAT = "0";
            try
            {
                if (AuxMat.Rows.Count > 0)
                {
                    conexion.Vincula_Material_Reciclado_a_Material(MATERIAL, AuxMat.Rows[0]["Referencia"].ToString());
                    RECMAT = AuxMat.Rows[0]["MATDESC"].ToString();
                }
            }
            catch (Exception ex)
            { }
            //Defino el listado Inicial

            return RECMAT;
        }

        [WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static string Elimina_Material_Reciclado(string MATERIAL)
        {

            Conexion_MATERIALES conexion = new Conexion_MATERIALES();
            //Compruebo que el material existe
            //Actualizo material y genero el texto para devolver a la etiqueta.
            string RECMAT = "0";
            try
            {
                {
                    conexion.Vincula_Material_Reciclado_a_Material(MATERIAL, "0");
                }
            }
            catch (Exception ex)
            { }
            //Defino el listado Inicial

            return RECMAT;
        }

    }

}