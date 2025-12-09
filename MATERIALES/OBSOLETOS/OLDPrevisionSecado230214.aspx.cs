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
    public partial class PrevisionSecadoV3 : System.Web.UI.Page
    {

        private static DataSet ds_Liberaciones = new DataSet();
        private static string SELECNAVE = "Máq.";
        private static string SELECESTADO = "0";
        private static string SELECORDERBY = "0";
        DataSet ds_Materiales = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SELECNAVE = "Máq.";
                SELECESTADO = "0";
                SELECORDERBY = "0";
                Rellenar_Grid(null, null);
                for (int i = 0; i <= ds_Materiales.Tables[0].Rows.Count - 1; i++)
                {
                    DatalistNUMMaterial.InnerHtml = DatalistNUMMaterial.InnerHtml + System.Environment.NewLine +
                        String.Format("<option value='{0}'>", ds_Materiales.Tables[0].Rows[i][0]);
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
            //LIBERACIONES
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

            //LISTADO MATERIALES
            //ds_Materiales = conexion.devuelve_lista_materiales("I.[No_] LIKE '2%'");
            //dgv_Materiales.DataSource = ds_Materiales;
            //dgv_Materiales.DataBind();

            string[] RecorteMAT = NUMMaterial.Value.ToString().Split(new char[] { '¬' });

            //Conexion_MATERIALES conexion = new Conexion_MATERIALES();

            if (NUMMaterial.Value != "")
            {
                ds_Materiales = conexion.devuelve_lista_materiales("I.[No_] LIKE '" + RecorteMAT[0].Trim().ToString() + "%'");
            }
            else
            {
                ds_Materiales = conexion.devuelve_lista_materiales("I.[No_] LIKE '2%'");
            }
            dgv_Materiales.DataSource = ds_Materiales;
            dgv_Materiales.DataBind();

            //ENTRADAS PREVISTAS
            DataTable EntradasPrevistas = SHConexion.Devuelve_Entradas_Previstas_Materiales();
            dgv_EntradasPrevistas.DataSource = EntradasPrevistas;
            dgv_EntradasPrevistas.DataBind();


            //PREVISION DE SECADO
            DataTable PrevisionSecado = conexion.Devuelve_Prevision_SecadoV3();
            DataTable StocksDisponibles = conexion.Devuelve_Stock_MaterialV2();
            DataTable Ubicaciones = conexion.Devuelve_Ubicacion_Material_LEFTJOIN();
            //ds_ListaEntregas = SHconexion.Devuelve_listado_Recursos_Entregados(" AND [Employee No_] = " + lblNumOperario.Text + "");
            //ds_firmas = SHconexion.Devuelve_EPIS_ListaEntrega("");
            var JoinResult = (from p in PrevisionSecado.AsEnumerable()
                              join t in StocksDisponibles.AsEnumerable()
                              on p.Field<string>("MATERIAL") equals t.Field<string>("MATERIAL") into tempJoin
                              from leftJoin in tempJoin.DefaultIfEmpty()
                              join u in Ubicaciones.AsEnumerable() on p.Field<string>("MATERIAL") equals u.Field<string>("Articulo") into tempJoin2 from leftJoin2 in tempJoin2.DefaultIfEmpty()
                     
                              select new
                              {
                                  SEQNR = p.Field<decimal>("SEQNR"),
                                  MAQ = p.Field<string>("MAQ"),
                                  FECHA = p.Field<string>("FECHA"),
                                  INICIARSECADO = p.Field<string>("INICIARSECADO"),
                                  ORDEN = p.Field<string>("ORDEN"),
                                  MATERIAL = p.Field<string>("MATERIAL"),
                                  DESCRIPCION = p.Field<string>("DESCRIPCION"),
                                  DESCRIPCIONLONG = p.Field<string>("DESCRIPCIONLONG"),
                                  DISPONIBLE = leftJoin.Field<Decimal>("CANTALM"),
                                  PREVISION = leftJoin.Field<string>("FECHA"),
                                  PREPARAR = p.Field<string>("PREPARAR"),
                                  UBICACION = p.Field<string>("UBICACION"),
                                  NOTAS = p.Field<string>("NOTAS"),
                                  REPETICIONES = p.Field<Decimal>("REPETICIONES"),
                                  SUMAMATS = p.Field<Decimal>("SUMAMATS"),
                                  //REPEUBIS = (leftJoin2 != null) ? leftJoin2.Field<Decimal>("UBIACTIVAS") : 0,
                                  //REPEUBIS = leftJoin2.Field<Decimal>("UBIACTIVAS"),
                                  // FECHA = leftJoin.Field<string>("FECHA"),
                              }).ToList();
            dgv_secado.DataSource = JoinResult;
            dgv_secado.DataBind();

            //ESTADO DE MÁQUINAS
            DataTable EstadoMaquina = conexion.Devuelve_Prevision_Secado_Pendientes();
            var JoinResult2 = (from p in EstadoMaquina.AsEnumerable()
                              join t in StocksDisponibles.AsEnumerable()
                              on p.Field<string>("MATERIAL") equals t.Field<string>("MATERIAL") into tempJoin
                              from leftJoin in tempJoin.DefaultIfEmpty()
                              select new
                              {
                                  MAQ = p.Field<string>("MAQ"),
                                 
                                  
                                  MATERIAL = p.Field<string>("MATERIAL"),
                                  DESCRIPCION = p.Field<string>("DESCRIPCION"),
                                  DESCRIPCIONLONG = p.Field<string>("DESCRIPCIONLONG"),
                                  UBICACION = p.Field<string>("UBICACION"),
                                  NOTAS = p.Field<string>("NOTAS"),
                                  DISPONIBLE = leftJoin.Field<decimal>("CANTALM"),
                                  KGS_RESTANTE = p.Field<decimal>("KGS_RESTANTE"),
                                  ORDENES = p.Field<decimal>("ORDENES"),
                                  
                                  
                              }).ToList();


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
            }
            catch (Exception ex)
            { }
        }
        /*
        public void ImprimirLabel(object sender, EventArgs e)
        {

            // Printer IP Address and communication port
            string ipAddress = "10.0.0.180";
            int port = 9100;
            string producto = AUX_MATERIAL.Value.ToString();
            string descripcion = AUX_DESCRIPCION.Value.ToString();
            string operario = InputOperario.SelectedValue.ToString();
            string fechayhora = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            string lote = InputLote.Text.ToString();

            // ZPL Command(s)
            string ZPLString = "^XA^[L,2^FS^XZ" +
                                "^XA" +
                                "^MMT" +
                                "^LS0^LT0" +
                                "^FWR^FPH" +
                                "^MTD" +
                                "^MNM" +
                                "^LH2,10^FS" +
                                "^PW815^MNM^ML1817" +
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
                                "^FO600,105^A0R130,130^FD"+producto+"^FS" +
                                "^FO600,1100^A0R130,130^FD"+lote+"^FS" +
                                "^FO400,50^FB1625,2,0,J,0^A0R,130,130^" +
                                "^FO0,45^A0R70,70^FD"+fechayhora+ "^FS" +
                                "^FO70,1145^A0R70,70^FD"+operario+"^FS" +
                                "^FS" +
                                "^FO160,50^FB1600,3,0,C,0^A0R,110,110^FD"+descripcion+"^FS" +
                                "^FX Codigos de barra" +
                                "^FO615,680^BCR,170,N,N,N,N^FD>:30S"+producto+"^FS" +
                                "^XZ"+
                                "^XA^[L,2^FS^XZ" +
                                "^XA" +
                                "^MMT" +
                                "^LS0^LT0" +
                                "^FWR^FPH" +
                                "^MTD" +
                                "^MNM" +
                                "^LH2,10^FS" +
                                "^PW815^MNM^ML1817" +
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
                                "^FO615,680^BCR,170,N,N,N,N^FD>:30S" + producto + "^FS" +
                                "^XZ";
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
            }
            catch (Exception ex)
            {

                // Catch Exception
            }
        }
        */

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

        public void FiltraMaterial(object sender, EventArgs e)
        {

            try
            {
                string[] RecorteMAT = NUMMaterial.Value.ToString().Split(new char[] { '¬' });

                Conexion_MATERIALES conexion = new Conexion_MATERIALES();
                
                if (NUMMaterial.Value != "")
                {
                    ds_Materiales = conexion.devuelve_lista_materiales("I.[No_] LIKE '" + RecorteMAT[0].Trim().ToString() + "%'");
                }
                else
                {
                    ds_Materiales = conexion.devuelve_lista_materiales("I.[No_] LIKE '2%'");
                }                
                dgv_Materiales.DataSource = ds_Materiales;
                dgv_Materiales.DataBind();
                lkb_Sort_Click("TABLAMATERIALES");
                //NUMMaterial.Value = "";

            }
            catch (Exception)
            {

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


        [WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
       
        public static bool ImprimirEtiquetasV2(string MATERIAL, string DESCRIPCION, string INPUTOPERARIO, string LOTE, string TIPO)
        {

            // Printer IP Address and communication port
            string ipAddress = "10.0.0.180"; //MATERIALES
            //string ipAddress = "10.0.0.164"; //THERMOBMS
            int port = 9100;
            //string producto = AUX_MATERIAL.Value.ToString();
            string producto = MATERIAL;

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
                                "^MNM" +
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
                                "^FO615,680^BCR,170,N,N,N,N^FD>:30S" + producto + "^FS" +
                                "^XZ";

            string ZPLMaterial= "^XA^[L,2^FS^XZ" +
                                "^XA" +
                                "^MMT" +
                                "^LS0^LT0" +
                                "^FWR^FPH" +
                                "^MTD" +
                                "^MNM" +
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
                                "^FO615,680^BCR,170,N,N,N,N^FD>:30S" + producto + "^FS" +
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
                default:
                    ZPLString = ZPLMaterial + ZPLEstufa;
                    break;
            }
            // ZPL Command(s)
            /*
            string ZPLString = "^XA^[L,2^FS^XZ" +
                                "^XA" +
                                "^MMT" +
                                "^LS0^LT0" +
                                "^FWR^FPH" +
                                "^MTD" +
                                "^MNM" +
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
                                "^FO615,680^BCR,170,N,N,N,N^FD>:30S" + producto + "^FS" +
                                "^XZ" +
                                "^XA^[L,2^FS^XZ" +
                                "^XA" +
                                "^MMT" +
                                "^LS0^LT0" +
                                "^FWR^FPH" +
                                "^MTD" +
                                "^MNM" +
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
                                "^FO615,680^BCR,170,N,N,N,N^FD>:30S" + producto + "^FS" +
                                "^XZ";
            */

            

            

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
    }

}