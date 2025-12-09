using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;

namespace ThermoWeb.MATERIALES
{
    public partial class GestionMolinos : System.Web.UI.Page
    {

        private static DataTable DatosMolino = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                Conexion_MATERIALES conexion = new Conexion_MATERIALES();
        
                DataTable Materiales = conexion.Devuelve_Lista_Materiales_SEPARADOR();
                {
                    for (int i = 0; i <= Materiales.Rows.Count - 1; i++)
                    {
                        DatalistNuevoMat.InnerHtml = DatalistNuevoMat.InnerHtml + System.Environment.NewLine +
                            String.Format("<option value='{0}'>", Materiales.Rows[i][0]);

                        DatalistFiltroMat.InnerHtml = DatalistFiltroMat.InnerHtml + System.Environment.NewLine +
                            String.Format("<option value='{0}'>", Materiales.Rows[i][0]);
                    }
                }
                Rellenar_grid();
               // conexion.Generar_Linea_Molido();
            }
         
        }

        public void Rellenar_grid()
        {
            Conexion_SMARTH SHconexion = new Conexion_SMARTH();
            Conexion_MATERIALES conexion = new Conexion_MATERIALES();

            DatosMolino = conexion.Devuelve_DatosMolino();
            lblM1MAT.InnerText = DatosMolino.Rows[0]["Referencia"].ToString() + " " + DatosMolino.Rows[0]["Descripcion"].ToString();
            lblM2MAT.InnerText = DatosMolino.Rows[1]["Referencia"].ToString() + " " + DatosMolino.Rows[1]["Descripcion"].ToString();
            lblM3MAT.InnerText = DatosMolino.Rows[2]["Referencia"].ToString() + " " + DatosMolino.Rows[2]["Descripcion"].ToString();
            lblM17MAT.InnerText = DatosMolino.Rows[3]["Referencia"].ToString() + " " + DatosMolino.Rows[3]["Descripcion"].ToString();
            lblM5MAT.InnerText = DatosMolino.Rows[4]["Referencia"].ToString() + " " + DatosMolino.Rows[4]["Descripcion"].ToString();
            lblM7MAT.InnerText = DatosMolino.Rows[5]["Referencia"].ToString() + " " + DatosMolino.Rows[5]["Descripcion"].ToString();
            lblM6MAT.InnerText = DatosMolino.Rows[6]["Referencia"].ToString() + " " + DatosMolino.Rows[6]["Descripcion"].ToString();
            lblM8MAT.InnerText = DatosMolino.Rows[7]["Referencia"].ToString() + " " + DatosMolino.Rows[7]["Descripcion"].ToString();
            lblM4MAT.InnerText = DatosMolino.Rows[8]["Referencia"].ToString() + " " + DatosMolino.Rows[8]["Descripcion"].ToString();
            lblM10MAT.InnerText = DatosMolino.Rows[9]["Referencia"].ToString() + " " + DatosMolino.Rows[9]["Descripcion"].ToString();
            lblM33MAT.InnerText = DatosMolino.Rows[10]["Referencia"].ToString() + " " + DatosMolino.Rows[10]["Descripcion"].ToString();
            lblM14MAT.InnerText = DatosMolino.Rows[11]["Referencia"].ToString() + " " + DatosMolino.Rows[11]["Descripcion"].ToString();
            lblM50MAT.InnerText = DatosMolino.Rows[12]["Referencia"].ToString() + " " + DatosMolino.Rows[12]["Descripcion"].ToString();

            DataTable HistoricoMolino = conexion.Devuelve_Historico_Molidos();
            dgv_HistoricoMolidos.DataSource = HistoricoMolino;
            dgv_HistoricoMolidos.DataBind();
             /*
            DataTable HistoricoMES = conexion.Devuelve_ResumenMolidosMES("");
                GridKPIporMES.DataSource = HistoricoMES;
            GridKPIporMES.DataBind();
           */

            DataTable MatPRODUCIENDO = conexion.Devuelve_Materiales_Produciendo();

            DataTable ReferenciaMolino = conexion.Devuelve_ReferenciaXMolino();

            var JoinResult = (from p in MatPRODUCIENDO.AsEnumerable()
                              join t in ReferenciaMolino.AsEnumerable()
                              on p.Field<string>("MATERIAL") equals t.Field<string>("ReferenciaMOL") into tempJoin
                              from leftJoin in tempJoin.DefaultIfEmpty()
                              select new
                              {
                                  MAQ = p.Field<string>("MAQ"),
                                  MATERIAL = p.Field<string>("MATERIAL"),
                                  DESCRIPCION = p.Field<string>("DESCRIPCION"),
                                  MOLINO = leftJoin == null ? "-" : leftJoin.Field<string>("Molino"),
                              }).ToList();
            dgv_Materiales.DataSource = JoinResult;
            dgv_Materiales.DataBind();
        }

        public void Editar_Molino(object sender, EventArgs e)
        {
            try
            {
                //limpio cuadros
                DIVMaterialAlternativo.Visible = false;
                lblMaterialAlternativo.InnerText = "";
                IDlabelMolino.InnerText = "0";
                inputNuevoMaterial.Value = "";

                Conexion_MATERIALES conexion = new Conexion_MATERIALES();
                HtmlButton button = (HtmlButton)sender;
                string Molino = button.ClientID.ToString();
                DataTable Auxmolino;
                switch (Molino)
                {
                    //EDICIONES
                    case "EditaM1":
                        DatosMolino.DefaultView.RowFilter = "Id = 1";
                        Auxmolino = DatosMolino.DefaultView.ToTable();
                        IDlabelMolino.InnerText = "1";
                        labelMolino.InnerText = Auxmolino.Rows[0]["Molino"].ToString();
                        lblMaterialActual.InnerText = Auxmolino.Rows[0]["Referencia"].ToString() + " " + Auxmolino.Rows[0]["Descripcion"].ToString();
                        if (Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "0" && Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "")
                        {
                            DIVMaterialAlternativo.Visible = true;
                            lblMaterialAlternativo.InnerText = Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() + " " + conexion.Devuelve_MateriaPrima(Auxmolino.Rows[0]["ReferenciaReciclado"].ToString());
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                        break;
                    case "EditaM2":
                        DatosMolino.DefaultView.RowFilter = "Id = 2";
                        Auxmolino = DatosMolino.DefaultView.ToTable();
                        IDlabelMolino.InnerText = "2";
                        labelMolino.InnerText = Auxmolino.Rows[0]["Molino"].ToString();
                        lblMaterialActual.InnerText = Auxmolino.Rows[0]["Referencia"].ToString() + " " + Auxmolino.Rows[0]["Descripcion"].ToString();
                        if (Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "0" && Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "")
                        {
                            DIVMaterialAlternativo.Visible = true;
                            lblMaterialAlternativo.InnerText = Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() + " " + conexion.Devuelve_MateriaPrima(Auxmolino.Rows[0]["ReferenciaReciclado"].ToString());
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                        break;
                    case "EditaM3":
                        DatosMolino.DefaultView.RowFilter = "Id = 3";
                        Auxmolino = DatosMolino.DefaultView.ToTable();
                        IDlabelMolino.InnerText = "3";
                        labelMolino.InnerText = Auxmolino.Rows[0]["Molino"].ToString();
                        lblMaterialActual.InnerText = Auxmolino.Rows[0]["Referencia"].ToString() + " " + Auxmolino.Rows[0]["Descripcion"].ToString();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                        break;
                    case "EditaM5":
                        DatosMolino.DefaultView.RowFilter = "Id = 5";
                        Auxmolino = DatosMolino.DefaultView.ToTable();
                        IDlabelMolino.InnerText = "5";
                        labelMolino.InnerText = Auxmolino.Rows[0]["Molino"].ToString();
                        lblMaterialActual.InnerText = Auxmolino.Rows[0]["Referencia"].ToString() + " " + Auxmolino.Rows[0]["Descripcion"].ToString();
                        if (Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "0" && Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "")
                        {
                            DIVMaterialAlternativo.Visible = true;
                            lblMaterialAlternativo.InnerText = Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() + " " + conexion.Devuelve_MateriaPrima(Auxmolino.Rows[0]["ReferenciaReciclado"].ToString());
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                        break;
                    case "EditaM8":
                        DatosMolino.DefaultView.RowFilter = "Id = 8";
                        Auxmolino = DatosMolino.DefaultView.ToTable();
                        IDlabelMolino.InnerText = "8";
                        labelMolino.InnerText = Auxmolino.Rows[0]["Molino"].ToString();
                        lblMaterialActual.InnerText = Auxmolino.Rows[0]["Referencia"].ToString() + " " + Auxmolino.Rows[0]["Descripcion"].ToString();
                        if (Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "0" && Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "")
                        {
                            DIVMaterialAlternativo.Visible = true;
                            lblMaterialAlternativo.InnerText = Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() + " " + conexion.Devuelve_MateriaPrima(Auxmolino.Rows[0]["ReferenciaReciclado"].ToString());
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                        break;
                    case "EditaM10":
                        DatosMolino.DefaultView.RowFilter = "Id = 10";
                        Auxmolino = DatosMolino.DefaultView.ToTable();
                        IDlabelMolino.InnerText = "10";
                        labelMolino.InnerText = Auxmolino.Rows[0]["Molino"].ToString();
                        lblMaterialActual.InnerText = Auxmolino.Rows[0]["Referencia"].ToString() + " " + Auxmolino.Rows[0]["Descripcion"].ToString();
                        if (Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "0" && Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "")
                        {
                            DIVMaterialAlternativo.Visible = true;
                            lblMaterialAlternativo.InnerText = Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() + " " + conexion.Devuelve_MateriaPrima(Auxmolino.Rows[0]["ReferenciaReciclado"].ToString());
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                        break;

                    case "EditaM17":
                        DatosMolino.DefaultView.RowFilter = "Id = 4";
                        Auxmolino = DatosMolino.DefaultView.ToTable();
                        IDlabelMolino.InnerText = "4";
                        labelMolino.InnerText = Auxmolino.Rows[0]["Molino"].ToString();
                        lblMaterialActual.InnerText = Auxmolino.Rows[0]["Referencia"].ToString() + " " + Auxmolino.Rows[0]["Descripcion"].ToString();
                        if (Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "0" && Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "")
                        {
                            DIVMaterialAlternativo.Visible = true;
                            lblMaterialAlternativo.InnerText = Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() + " " + conexion.Devuelve_MateriaPrima(Auxmolino.Rows[0]["ReferenciaReciclado"].ToString());
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                        break;
                    case "EditaM7":
                        DatosMolino.DefaultView.RowFilter = "Id = 6";
                        Auxmolino = DatosMolino.DefaultView.ToTable();
                        IDlabelMolino.InnerText = "6";
                        labelMolino.InnerText = Auxmolino.Rows[0]["Molino"].ToString();
                        lblMaterialActual.InnerText = Auxmolino.Rows[0]["Referencia"].ToString() + " " + Auxmolino.Rows[0]["Descripcion"].ToString();
                        if (Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "0" && Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "")
                        {
                            DIVMaterialAlternativo.Visible = true;
                            lblMaterialAlternativo.InnerText = Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() + " " + conexion.Devuelve_MateriaPrima(Auxmolino.Rows[0]["ReferenciaReciclado"].ToString());
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                        break;
                    case "EditaM6":
                        DatosMolino.DefaultView.RowFilter = "Id = 7";
                        Auxmolino = DatosMolino.DefaultView.ToTable();
                        IDlabelMolino.InnerText = "7";
                        labelMolino.InnerText = Auxmolino.Rows[0]["Molino"].ToString();
                        lblMaterialActual.InnerText = Auxmolino.Rows[0]["Referencia"].ToString() + " " + Auxmolino.Rows[0]["Descripcion"].ToString();
                        if (Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "0" && Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "")
                        {
                            DIVMaterialAlternativo.Visible = true;
                            lblMaterialAlternativo.InnerText = Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() + " " + conexion.Devuelve_MateriaPrima(Auxmolino.Rows[0]["ReferenciaReciclado"].ToString());
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                        break;
                    case "EditaM4":
                        DatosMolino.DefaultView.RowFilter = "Id = 9";
                        Auxmolino = DatosMolino.DefaultView.ToTable();
                        IDlabelMolino.InnerText = "9";
                        labelMolino.InnerText = Auxmolino.Rows[0]["Molino"].ToString();
                        lblMaterialActual.InnerText = Auxmolino.Rows[0]["Referencia"].ToString() + " " + Auxmolino.Rows[0]["Descripcion"].ToString();
                        if (Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "0" && Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "")
                        {
                            DIVMaterialAlternativo.Visible = true;
                            lblMaterialAlternativo.InnerText = Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() + " " + conexion.Devuelve_MateriaPrima(Auxmolino.Rows[0]["ReferenciaReciclado"].ToString());
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                        break;
                    case "EditaM33":
                        DatosMolino.DefaultView.RowFilter = "Id = 11";
                        Auxmolino = DatosMolino.DefaultView.ToTable();
                        IDlabelMolino.InnerText = "11";
                        labelMolino.InnerText = Auxmolino.Rows[0]["Molino"].ToString();
                        lblMaterialActual.InnerText = Auxmolino.Rows[0]["Referencia"].ToString() + " " + Auxmolino.Rows[0]["Descripcion"].ToString();
                        if (Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "0" && Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "")
                        {
                            DIVMaterialAlternativo.Visible = true;
                            lblMaterialAlternativo.InnerText = Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() + " " + conexion.Devuelve_MateriaPrima(Auxmolino.Rows[0]["ReferenciaReciclado"].ToString());
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                        break;
                    case "EditaM14":
                        DatosMolino.DefaultView.RowFilter = "Id = 12";
                        Auxmolino = DatosMolino.DefaultView.ToTable();
                        IDlabelMolino.InnerText = "12";
                        labelMolino.InnerText = Auxmolino.Rows[0]["Molino"].ToString();
                        lblMaterialActual.InnerText = Auxmolino.Rows[0]["Referencia"].ToString() + " " + Auxmolino.Rows[0]["Descripcion"].ToString();
                        if (Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "0" && Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "")
                        {
                            DIVMaterialAlternativo.Visible = true;
                            lblMaterialAlternativo.InnerText = Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() + " " + conexion.Devuelve_MateriaPrima(Auxmolino.Rows[0]["ReferenciaReciclado"].ToString());
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                        break;
                    case "EditaM50":
                        DatosMolino.DefaultView.RowFilter = "Id = 13";
                        Auxmolino = DatosMolino.DefaultView.ToTable();
                        IDlabelMolino.InnerText = "13";
                        labelMolino.InnerText = Auxmolino.Rows[0]["Molino"].ToString();
                        lblMaterialActual.InnerText = Auxmolino.Rows[0]["Referencia"].ToString() + " " + Auxmolino.Rows[0]["Descripcion"].ToString();
                        if (Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "0" && Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "")
                        {
                            DIVMaterialAlternativo.Visible = true;
                            lblMaterialAlternativo.InnerText = Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() + " " + conexion.Devuelve_MateriaPrima(Auxmolino.Rows[0]["ReferenciaReciclado"].ToString());
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                        break;

                    
                }
                
            }
            catch (Exception ex)
            { }
        }

        public void Guardar_Molino(object sender, EventArgs e)
        {
            Conexion_MATERIALES conexion = new Conexion_MATERIALES();
            HtmlButton button = (HtmlButton)sender;
            string TipoAccion = button.ClientID.ToString();
            if (inputNuevoMaterial.Value != "" && TipoAccion == "BtnCambiaMaterial")
            {
               
                string[] RecorteMaterial = inputNuevoMaterial.Value.Split(new char[] { '¬' }); 
                conexion.Actualiza_Material_Activo(IDlabelMolino.InnerText, conexion.Devuelve_Valida_MateriaPrima(RecorteMaterial[0].Trim().ToString()));
                Rellenar_grid();
            }

            if(TipoAccion == "BtnBorrarMaterial")
            {
                conexion.Actualiza_Material_Activo(IDlabelMolino.InnerText, "0");
                Rellenar_grid();
            }
                

        }

        public void AbrirMolidoMaterial(object sender, EventArgs e)
        {
            rowMolidoAlternativo.Visible = false;
            lblMaterialMolido.InnerText = "";
            lblDescripcionMolido.InnerText = "";
            lblMaterialAlternativoAviso.InnerText = "";
            lblMaterialAlternativoAvisoReferencia.InnerText = "";
            lblMolinoAsignado.InnerText = "0";
            inputMolidoKgs.Value = "";

            Conexion_MATERIALES conexion = new Conexion_MATERIALES();
            HtmlButton button = (HtmlButton)sender;
            string Molino = button.ClientID.ToString();
            DataTable Auxmolino;
            switch (Molino)
            {
                case "MueleM1":
                    if (lblM1MAT.InnerText != " ")
                    {
                        DatosMolino.DefaultView.RowFilter = "Id = 1";
                        Auxmolino = DatosMolino.DefaultView.ToTable();
                        lblMolinoAsignado.InnerText = "1";
                        lblMaterialMolido.InnerText = Auxmolino.Rows[0]["Referencia"].ToString();
                        lblDescripcionMolido.InnerText = Auxmolino.Rows[0]["Descripcion"].ToString();
                        if (Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "0" && Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "")
                        {
                            rowMolidoAlternativo.Visible = true;
                            lblMaterialAlternativoAviso.InnerText = "*Se generará una entrada de material para el siguiente producto:";
                            lblMaterialAlternativoAvisoReferencia.InnerText = Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() + " " + conexion.Devuelve_MateriaPrima(Auxmolino.Rows[0]["ReferenciaReciclado"].ToString()) + ".";
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupMolido();", true);

                    }
                    break;
                case "MueleM2":
                    if (lblM2MAT.InnerText != " ")
                    {
                        DatosMolino.DefaultView.RowFilter = "Id = 2";
                        Auxmolino = DatosMolino.DefaultView.ToTable();
                        lblMolinoAsignado.InnerText = "2";
                        lblMaterialMolido.InnerText = Auxmolino.Rows[0]["Referencia"].ToString();
                        lblDescripcionMolido.InnerText = Auxmolino.Rows[0]["Descripcion"].ToString();
                        if (Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "0" && Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "")
                        {
                            rowMolidoAlternativo.Visible = true;
                            lblMaterialAlternativoAviso.InnerText = "*Se generará una entrada de material para el siguiente producto:";
                            lblMaterialAlternativoAvisoReferencia.InnerText = Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() + " " + conexion.Devuelve_MateriaPrima(Auxmolino.Rows[0]["ReferenciaReciclado"].ToString()) + ".";
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupMolido();", true);

                    }
                    break;
                case "MueleM3":
                    if (lblM3MAT.InnerText != " ")
                    {
                        DatosMolino.DefaultView.RowFilter = "Id = 3";
                        Auxmolino = DatosMolino.DefaultView.ToTable();
                        lblMolinoAsignado.InnerText = "3";
                        lblMaterialMolido.InnerText = Auxmolino.Rows[0]["Referencia"].ToString();
                        lblDescripcionMolido.InnerText = Auxmolino.Rows[0]["Descripcion"].ToString();
                        if (Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "0" && Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "")
                        {
                            rowMolidoAlternativo.Visible = true;
                            lblMaterialAlternativoAviso.InnerText = "*Se generará una entrada de material para el siguiente producto:";
                            lblMaterialAlternativoAvisoReferencia.InnerText = Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() + " " + conexion.Devuelve_MateriaPrima(Auxmolino.Rows[0]["ReferenciaReciclado"].ToString()) + ".";
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupMolido();", true);

                    }
                    break;
                case "MueleM5":
                    if (lblM5MAT.InnerText != " ")
                    {
                        DatosMolino.DefaultView.RowFilter = "Id = 5";
                        Auxmolino = DatosMolino.DefaultView.ToTable();
                        lblMolinoAsignado.InnerText = "5";
                        lblMaterialMolido.InnerText = Auxmolino.Rows[0]["Referencia"].ToString();
                        lblDescripcionMolido.InnerText = Auxmolino.Rows[0]["Descripcion"].ToString();
                        if (Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "0" && Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "")
                        {
                            rowMolidoAlternativo.Visible = true;
                            lblMaterialAlternativoAviso.InnerText = "*Se generará una entrada de material para el siguiente producto:";
                            lblMaterialAlternativoAvisoReferencia.InnerText = Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() + " " + conexion.Devuelve_MateriaPrima(Auxmolino.Rows[0]["ReferenciaReciclado"].ToString()) + ".";
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupMolido();", true);
                    }
                    break;
                case "MueleM8":
                    if (lblM8MAT.InnerText != " ")
                    {
                        DatosMolino.DefaultView.RowFilter = "Id = 8";
                        Auxmolino = DatosMolino.DefaultView.ToTable();
                        lblMolinoAsignado.InnerText = "8";
                        lblMaterialMolido.InnerText = Auxmolino.Rows[0]["Referencia"].ToString();
                        lblDescripcionMolido.InnerText = Auxmolino.Rows[0]["Descripcion"].ToString();
                        if (Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "0" && Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "")
                        {
                            rowMolidoAlternativo.Visible = true;
                            lblMaterialAlternativoAviso.InnerText = "*Se generará una entrada de material para el siguiente producto:";
                            lblMaterialAlternativoAvisoReferencia.InnerText = Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() + " " + conexion.Devuelve_MateriaPrima(Auxmolino.Rows[0]["ReferenciaReciclado"].ToString()) + ".";
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupMolido();", true);
                    }
                    break;
                case "MueleM10":
                    if (lblM10MAT.InnerText != " ")
                    {
                        DatosMolino.DefaultView.RowFilter = "Id = 10";
                        Auxmolino = DatosMolino.DefaultView.ToTable();
                        lblMolinoAsignado.InnerText = "10";
                        lblMaterialMolido.InnerText = Auxmolino.Rows[0]["Referencia"].ToString();
                        lblDescripcionMolido.InnerText = Auxmolino.Rows[0]["Descripcion"].ToString();
                        if (Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "0" && Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "")
                        {
                            rowMolidoAlternativo.Visible = true;
                            lblMaterialAlternativoAviso.InnerText = "*Se generará una entrada de material para el siguiente producto:";
                            lblMaterialAlternativoAvisoReferencia.InnerText = Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() + " " + conexion.Devuelve_MateriaPrima(Auxmolino.Rows[0]["ReferenciaReciclado"].ToString()) + ".";
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupMolido();", true);
                    }
                    break;

                case "MueleM17":
                    if (lblM17MAT.InnerText != " ")
                    {
                        DatosMolino.DefaultView.RowFilter = "Id = 4";
                        Auxmolino = DatosMolino.DefaultView.ToTable();
                        lblMolinoAsignado.InnerText = "4";
                        lblMaterialMolido.InnerText = Auxmolino.Rows[0]["Referencia"].ToString();
                        lblDescripcionMolido.InnerText = Auxmolino.Rows[0]["Descripcion"].ToString();
                        if (Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "0" && Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "")
                        {
                            rowMolidoAlternativo.Visible = true;
                            lblMaterialAlternativoAviso.InnerText = "*Se generará una entrada de material para el siguiente producto:";
                            lblMaterialAlternativoAvisoReferencia.InnerText = Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() + " " + conexion.Devuelve_MateriaPrima(Auxmolino.Rows[0]["ReferenciaReciclado"].ToString()) + ".";
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupMolido();", true);
                    }
                    break;
                case "MueleM7":
                    if (lblM7MAT.InnerText != " ")
                    {
                        DatosMolino.DefaultView.RowFilter = "Id = 6";
                        Auxmolino = DatosMolino.DefaultView.ToTable();
                        lblMolinoAsignado.InnerText = "6";
                        lblMaterialMolido.InnerText = Auxmolino.Rows[0]["Referencia"].ToString();
                        lblDescripcionMolido.InnerText = Auxmolino.Rows[0]["Descripcion"].ToString();
                        if (Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "0" && Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "")
                        {
                            rowMolidoAlternativo.Visible = true;
                            lblMaterialAlternativoAviso.InnerText = "*Se generará una entrada de material para el siguiente producto:";
                            lblMaterialAlternativoAvisoReferencia.InnerText = Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() + " " + conexion.Devuelve_MateriaPrima(Auxmolino.Rows[0]["ReferenciaReciclado"].ToString()) + ".";
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupMolido();", true);
                    }
                    break;
                case "MueleM6":
                    if (lblM6MAT.InnerText != " ")
                    {
                        DatosMolino.DefaultView.RowFilter = "Id = 7";
                        Auxmolino = DatosMolino.DefaultView.ToTable();
                        lblMolinoAsignado.InnerText = "7";
                        lblMaterialMolido.InnerText = Auxmolino.Rows[0]["Referencia"].ToString();
                        lblDescripcionMolido.InnerText = Auxmolino.Rows[0]["Descripcion"].ToString();
                        if (Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "0" && Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "")
                        {
                            rowMolidoAlternativo.Visible = true;
                            lblMaterialAlternativoAviso.InnerText = "*Se generará una entrada de material para el siguiente producto:";
                            lblMaterialAlternativoAvisoReferencia.InnerText = Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() + " " + conexion.Devuelve_MateriaPrima(Auxmolino.Rows[0]["ReferenciaReciclado"].ToString()) + ".";
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupMolido();", true);
                    }
                    break;
                case "MueleM4":
                    if (lblM4MAT.InnerText != " ")
                    {
                        DatosMolino.DefaultView.RowFilter = "Id = 9";
                        Auxmolino = DatosMolino.DefaultView.ToTable();
                        lblMolinoAsignado.InnerText = "9";
                        lblMaterialMolido.InnerText = Auxmolino.Rows[0]["Referencia"].ToString();
                        lblDescripcionMolido.InnerText = Auxmolino.Rows[0]["Descripcion"].ToString();
                        if (Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "0" && Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "")
                        {
                            rowMolidoAlternativo.Visible = true;
                            lblMaterialAlternativoAviso.InnerText = "*Se generará una entrada de material para el siguiente producto:";
                            lblMaterialAlternativoAvisoReferencia.InnerText = Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() + " " + conexion.Devuelve_MateriaPrima(Auxmolino.Rows[0]["ReferenciaReciclado"].ToString()) + ".";
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupMolido();", true);
                    }
                    break;
                case "MueleM33":
                    if (lblM33MAT.InnerText != " ")
                    {
                        DatosMolino.DefaultView.RowFilter = "Id = 11";
                        Auxmolino = DatosMolino.DefaultView.ToTable();
                        lblMolinoAsignado.InnerText = "11";
                        lblMaterialMolido.InnerText = Auxmolino.Rows[0]["Referencia"].ToString();
                        lblDescripcionMolido.InnerText = Auxmolino.Rows[0]["Descripcion"].ToString();
                        if (Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "0" && Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "")
                        {
                            rowMolidoAlternativo.Visible = true;
                            lblMaterialAlternativoAviso.InnerText = "*Se generará una entrada de material para el siguiente producto:";
                            lblMaterialAlternativoAvisoReferencia.InnerText = Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() + " " + conexion.Devuelve_MateriaPrima(Auxmolino.Rows[0]["ReferenciaReciclado"].ToString()) + ".";
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupMolido();", true);
                    }
                    break;
                case "MueleM14":
                    if (lblM14MAT.InnerText != " ")
                    {
                        DatosMolino.DefaultView.RowFilter = "Id = 12";
                        Auxmolino = DatosMolino.DefaultView.ToTable();
                        lblMolinoAsignado.InnerText = "12";
                        lblMaterialMolido.InnerText = Auxmolino.Rows[0]["Referencia"].ToString();
                        lblDescripcionMolido.InnerText = Auxmolino.Rows[0]["Descripcion"].ToString();
                        if (Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "0" && Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "")
                        {
                            rowMolidoAlternativo.Visible = true;
                            lblMaterialAlternativoAviso.InnerText = "*Se generará una entrada de material para el siguiente producto:";
                            lblMaterialAlternativoAvisoReferencia.InnerText = Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() + " " + conexion.Devuelve_MateriaPrima(Auxmolino.Rows[0]["ReferenciaReciclado"].ToString()) + ".";
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupMolido();", true);
                    }
                    break;
                case "MueleM50":
                    if (lblM50MAT.InnerText != " ")
                    {
                        DatosMolino.DefaultView.RowFilter = "Id = 13";
                        Auxmolino = DatosMolino.DefaultView.ToTable();
                        lblMolinoAsignado.InnerText = "13";
                        lblMaterialMolido.InnerText = Auxmolino.Rows[0]["Referencia"].ToString();
                        lblDescripcionMolido.InnerText = Auxmolino.Rows[0]["Descripcion"].ToString();
                        if (Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "0" && Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() != "")
                        {
                            rowMolidoAlternativo.Visible = true;
                            lblMaterialAlternativoAviso.InnerText = "*Se generará una entrada de material para el siguiente producto:";
                            lblMaterialAlternativoAvisoReferencia.InnerText = Auxmolino.Rows[0]["ReferenciaReciclado"].ToString() + " " + conexion.Devuelve_MateriaPrima(Auxmolino.Rows[0]["ReferenciaReciclado"].ToString()) + ".";
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopupMolido();", true);
                    }
                    break;
            }
        }

        public void Moler_Material(object sender, EventArgs e)
        {
            Conexion_MATERIALES conexion = new Conexion_MATERIALES();
            string MaterialMoler = conexion.Devuelve_Material_Moler(lblMaterialMolido.InnerText.Trim());
            if (MaterialMoler != "0")
            {
                conexion.Insertar_Linea_Molido(Convert.ToInt32(lblMolinoAsignado.InnerText), MaterialMoler, inputMolidoKgs.Value.Replace(",", "."), DateTime.Now.ToString());

                StringBuilder exportar = new StringBuilder();
                string CadenaTexto = "" + ";" + MaterialMoler + ";40004;40004/01;" + inputMolidoKgs.Value.Replace(",", ".") + ";0;" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ";;1";             
                string folderPath = @"X:\";
                //ACTIVA EXPORTACIÓN
                //File.WriteAllText(folderPath + "BMSEXPORT_"+ DateTime.Now.ToString("yy-MM-dd_HHmmss")+"RC.csv", CadenaTexto.ToString());
            }
            Rellenar_grid();

        }

        public void Etiqueta(object sender, EventArgs e)
        {
            string descripcion = "";
            string producto = "";
            string cantidad = "";
            string fecha = "";
            string lote = "";
            string molino = "";
            string etiqueta = "^[L,2^FS^XZ" +
                                "^XA" +
                                "^MMT " +
                                "^LS0^LT0" +
                                "^FWR^FPH " +
                                "^MTD " +
                                "^MNM " +
                                "^LH2,10^FS" +
                                "^PW815^ML2480" +
                                "^FX Lineas verticales" +
                                "^FO15,25^GB785,1,3^FS " +
                                "^FO600,1025^GB200,1,3^FS " +
                                "^FO55,480^GB145,1,3^FS " +
                                "^FO55,1025^GB145,1,3^FS " +
                                "^FO15,1625^GB785,1,3^FS " +
                                "^FX Lineas horizontales " +
                                "^FO800,25^GB1,1600,3^FS " +
                                "^FO600,25^GB1,1600,3^FS " +
                                "^FO200,25^GB1,1600,3^FS " +
                                "^FO15,25^GB1,1600,3^FS " +
                                "^FO55,25^GB1,1600,3^FS " +
                                "^FO15,25^GB30,1600,50,20,0^FS" +
                                "^FX Descripciones de campo " +
                                "^FO755,35^A0R35,35^FDPRODUCTO:^FS" +
                                "^FO555,35^A0R35,35^FDDESCRIPCION:^FS" +
                                "^FO755,1040^A0R35,35^FDLOTE:^FS " +
                                "^FO155,35^A0R35,35^FDFECHA:^FS " +
                                "^FO155,500^A0R35,35^FDMOLINO:^FS" +
                                "^FO155,1040^A0R35,35^FDCANTIDAD:^FS " +
                                "^FO17,50^FR^FB1600,1,0,C,0^A0R35,35^FDMATERIAL RECICLADO^FS " +
                                "^FX Campos variables " +
                                "^FO600,105^A0R130,130^FD"+producto+"^FS " +
                                "^FO600,1100^A0R130,130^FD"+lote+"^FS" +
                                "^FO400,50^FB1625,2,0,J,0^A0R,130,130^ " +
                                "^FO0,45^A0R70,70^FD"+fecha+"^FS" +
                                "^FO70,605^A0R70,70^FD"+molino+"^FS" +
                                "^FO60,1145^A0R100,100^FD" + cantidad+" Kgs.^FS" +
                                "^FS" +
                                "^FO180,50^FB1300,3,0,C,0^A0R,110,110^FD"+descripcion+"^FS" +
                                "^FX Codigos de barra " +
                                "^FO615,680^BCR,170,N,N,N,N^FD>:30S"+producto+"^FS" +
                                "^FO270,1350^GFA,7350,7350,30,,gI07FFCP08,gH01JF8N018,gH07JFEN01C,gG01LF8M03C,gG07LFEM03C,gG0NFM07E,g03NFCL07E,g0PFL0FE,Y03PF8K0FE,Y07PFEJ01FF,X01RF8I01FF,X07RFEI03FF,X0TFI07FF8,W03TFC007FF8,W0VF00IF8,V03VFC0IFC,V07VFE1IFC,U01XF9IFC,U07gHFE,T01gIFE,T03gIFE,T0gKF,S03gKF,S0gLF,R01gLF8,R07gLF8,Q01gMF8,Q03gMFC,Q0gNFC,P03gNFC,P04gNFE,Q01gMFE,R07gLFE,O02003gMF,N03FE00gMF,N0IF803gLF,M03IFE00gLF8,M07JF007gKF8,M0KFC01gKF8,L01LF007gJF8,L03LFC01gJFC,L03LFE00gJFC,L07MF803gIFC,L0NFE00gIFE,L0OF803gHFE,K01OFC01gHFE,K01PF007gHF,K01PFC01gHF,K03QF00gHFL0E,K03QF803gGF8J01F,K03QFE00gGF8J01FC,K03RF803gF8J03FF,K03RFC01gFCJ07FFC,K03SF007YFCJ07FFE,K03SFC01YFCJ0JF8,K03SFE00YFEJ0JFE,K03SFE003XFEI01KF8,K03SFEI0XFEI03KFC,K03SFEI03XFI03LF,K03SFEI01XFI07LFC,K03SFEJ07WFI0NF,K03SFEJ01WF800NF8,K03SFEK07VF801NFE,K03SFEK03VF803OF8,K03SFEL0VFC03OFC,K03SFEL03UFC07PF,K03SFEM0UF80QFC,K03SFEM07SFC00RF,K03SFEM01RFE001RF8,K03SFEN0RFI03RFE,K03SFEN0QF8I03SF8,K03SFEM01PFCJ07SFC,K03SFEM01OFEK0TFE,K03SFEM03OFL0UF,K03SFEM03NFL01UF8,K03SFEM07MF8L01UFC,K03SFEM07LFCM03UFE,K03SFEM0LFEN07UFE,K03SFEM0LFO07VF,K03SFEL01KF8O0WF,K03SFEL01JFCO01WF8K03SFEL03IFEP01WF8K03SFEL03IFQ03WFCK03SFEL07FF8Q07WFCK03SFEL07FCR07WFCK03SFEL0FES0XFEK03SFEL0FS01XFEK03SFEK018S01XFEK03SFEg03XFEK03SFEg07XFE:K03SFEg0YFCK03SFEY01YFC:K03SFEY03YF8:K03SFEY01YF,K03SFEg07WFE,K03SFEg01WFC,M01QFEgG07VF02R07LFEgG03UFC06W07FEgH0UF80EhH03SFE01EhI0SF803EhI07QFE00FEhI01QFC03FEQ01gR07PF007FEQ038gQ01OFC01FFEQ07CgR0OF007FFEQ0FEgR03MFE01IFEP01FFgS0MF803IFEP03FF8gR07KFE00JFEP07FFEgR01KF803JFEP0JFgS07JF007JFEP0JF8gR01IFC01KFEO01JFCgS0IF007KFEO03JFEgS03FE01LFEO07KFgT0F803LFEO0LF8gS0200MFEN01LFCgU03MFEN03MFgU0NFEN07MF8gS01NFEN0NFCgS07NFEM01NFEgR01OFEM03OFgH04O07OFEM03OF8gG06O0PFEM07OFCgG07N03PFEM0PFEgG07N0QFEL01QFgG078L01QFEL03QFCg0FCL07QFEL07QFEg0FCK01RFEL0SFg0FEK07RFEK01SF8Y0FFK0SFEK03SFCX01FFJ03SFEK07SFEX01FF8I0TFEK07TFX01FF8003TFEK0UF8W01FFC007TFEJ01UFCW01FFE01UFEJ03VFW03FFE07UFEJ07VF8V03IF0VFEJ0WFCV03IFBVFEI01WFEV03gFEI03XFV03gFEI07XF8U07gFEI0YFCU07gFE001YFEU07gFE001gFU07gFE003gFCT07gFE007gFET0gGFE00gHFT0gGFE01gHF8S0gGFE03gHFCS0gGFE07gHFER01gGFE0gHFT01gGFE1XFEW01gGFE3XFEW01gGFE:7IF03SFEW03gGFCK03SFEW03gGFC::K03SFEW03gGF8K03SFEW07gGF8K03SFEW07gGF,:K03SFEW07gFE,:K03SFEP01L0gGFC,K03SFEP078K0gGF8,K03SFEP0F8K0gGF,K03SFEO03FCK0gFE,K03SFEO0FFCJ01gFC,K03SFEN03FFEJ01gF8,K03SFEN07FFEJ01YFE,K03SFEM01JFJ01YF8,K03SFEM07JFJ01YF,K03SFEM0KF8I03XFC,K03SFEL03KF8I03XF,K03SFEL0LFCI03WFC,K03SFEK03LFCI03WF8,K03SFEK07LFEI03VFE,K03SFEJ01MFEI07VF8,K03SFEJ07NFI07UFE,K03SFEI01OFI07UFC,K03SFEI03OF8007UF,K03SFEI0PF8007TFC,K03SFE003PFC00UF8,K03SFE00QFC00TFE,K03SFE01QFE00TF8,K03SFE07QFE00TF,K03SFE1SF00TF8,K03SFE1SF01TFC,K03SFE1SF81TFC,K03SFE1SF800SFE,K03SFE1SFCI07RF,K01SFE1SFCJ07QF,K01SFE1SFEK03PF8,K01SFE1TFL01OFC,L0SFE1TFM01NFC,L0SFE1TFO0MFE,L07RFE1TF8O07LF,L03RFE1TFCP07KF,L03RFE1TFCQ03JF8,L01RFE1TFER03IFC,M0RFE1TFES01FFC,M07QFE1UFU0FE,M03QFE1UFV07,N0QFE1UF8,N07PFE1UF8,N01PFE1UFC,O07OFE1UFC,O01OFE1UFE,P0OFE1UFE,P03NFE1VF,Q0NFE1VF,Q03MFE1VF8,Q01MFE1VF8,R07LFE1VFC,R01LFE1VF8,S0LFE1UFE,S03KFE1UFC,T0KFE1UF,T03JFE1TFC,T01JFE1TF8,U07IFE1SFE,U01IFE1SF8,V07FFE1RFE,V03FFE1RFC,W0FFE1RF,W03FE1QFC,W01FE1QF,X07E1PFE,X01E1PF8,Y070OFE,Y030OF8,g08OF,gG07MFC,gG03MF,gG03LFE,gH0LF8,gH07JFE,gH01JF8,gI07FFC,,^FS" +
                                "^XZ";



        }


    }

}