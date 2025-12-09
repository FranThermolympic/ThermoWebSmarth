using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using System.Web.Services;

namespace ThermoWeb.MATERIALES
{
    public partial class GestionMolinosLITE : System.Web.UI.Page
    {

        private static DataTable DatosMolino = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                Conexion_MATERIALES conexion = new Conexion_MATERIALES();
        
                Rellenar_grid();
               //conexion.Generar_Linea_Molido();
            }
         
        }

        public void ReiniciaCajas()
        {
            MueleM1.Visible = false;
            MueleM2.Visible = false;
            MueleM3.Visible = false;
            MueleM5.Visible = false;
            MueleM8.Visible = false;
            MueleM10.Visible = false;
            MueleM17.Visible = false;
            MueleM7.Visible = false;
            MueleM6.Visible = false;
            MueleM4.Visible = false;
            MueleM33.Visible = false;
            MueleM14.Visible = false;
            MueleM50.Visible = false;

            BorraM1.Visible = false;
            BorraM2.Visible = false;
            BorraM3.Visible = false;
            BorraM5.Visible = false;
            BorraM8.Visible = false;
            BorraM10.Visible = false;
            BorraM17.Visible = false;
            BorraM7.Visible = false;
            BorraM6.Visible = false;
            BorraM4.Visible = false;
            BorraM33.Visible = false;
            BorraM14.Visible = false;
            BorraM50.Visible = false;

            lblM1MAT.InnerText = "";
            lblM2MAT.InnerText = "";
            lblM3MAT.InnerText = "";
            lblM5MAT.InnerText = "";
            lblM8MAT.InnerText = "";
            lblM10MAT.InnerText = "";
            lblM17MAT.InnerText = "";
            lblM7MAT.InnerText = "";
            lblM6MAT.InnerText = "";
            lblM4MAT.InnerText = "";
            lblM33MAT.InnerText = "";
            lblM14MAT.InnerText = "";
            lblM50MAT.InnerText = "";


        }

        public void Rellenar_grid()
        {
            Conexion_SMARTH SHconexion = new Conexion_SMARTH();
            Conexion_MATERIALES conexion = new Conexion_MATERIALES();
            ReiniciaCajas();

            DatosMolino = conexion.Devuelve_DatosMolino();
           
            if (DatosMolino.Rows[0]["Referencia"].ToString() != "")
            {
                lblM1MAT.InnerText = DatosMolino.Rows[0]["Referencia"].ToString() + " " + DatosMolino.Rows[0]["Descripcion"].ToString();
                MueleM1.Visible = true;
                BorraM1.Visible = true;
            }
            if (DatosMolino.Rows[1]["Referencia"].ToString() != "")
            {
                lblM2MAT.InnerText = DatosMolino.Rows[1]["Referencia"].ToString() + " " + DatosMolino.Rows[1]["Descripcion"].ToString();
                MueleM2.Visible = true;
                BorraM2.Visible = true;
            }
            if (DatosMolino.Rows[2]["Referencia"].ToString() != "")
            {
                lblM3MAT.InnerText = DatosMolino.Rows[2]["Referencia"].ToString() + " " + DatosMolino.Rows[2]["Descripcion"].ToString();
                MueleM3.Visible = true;
                BorraM3.Visible = true;
            }
            if (DatosMolino.Rows[3]["Referencia"].ToString() != "")
            {
                lblM17MAT.InnerText = DatosMolino.Rows[3]["Referencia"].ToString() + " " + DatosMolino.Rows[3]["Descripcion"].ToString();
                MueleM17.Visible = true;
                BorraM17.Visible = true;
            }
            if (DatosMolino.Rows[4]["Referencia"].ToString() != "")
            {
                lblM5MAT.InnerText = DatosMolino.Rows[4]["Referencia"].ToString() + " " + DatosMolino.Rows[4]["Descripcion"].ToString();
                MueleM5.Visible = true;
                BorraM5.Visible = true;
            }
            if (DatosMolino.Rows[5]["Referencia"].ToString() != "")
            {
                lblM7MAT.InnerText = DatosMolino.Rows[5]["Referencia"].ToString() + " " + DatosMolino.Rows[5]["Descripcion"].ToString();
                MueleM7.Visible = true;
                BorraM7.Visible = true;
            }
            if (DatosMolino.Rows[6]["Referencia"].ToString() != "")
            {
                lblM6MAT.InnerText = DatosMolino.Rows[6]["Referencia"].ToString() + " " + DatosMolino.Rows[6]["Descripcion"].ToString();
                MueleM6.Visible = true;
                BorraM6.Visible = true;
            }
            if (DatosMolino.Rows[7]["Referencia"].ToString() != "")
            {
                lblM8MAT.InnerText = DatosMolino.Rows[7]["Referencia"].ToString() + " " + DatosMolino.Rows[7]["Descripcion"].ToString();
                MueleM8.Visible = true;
                BorraM8.Visible = true;
            }
            if (DatosMolino.Rows[8]["Referencia"].ToString() != "")
            {
                lblM4MAT.InnerText = DatosMolino.Rows[8]["Referencia"].ToString() + " " + DatosMolino.Rows[8]["Descripcion"].ToString();
                MueleM4.Visible = true;
            }
            if (DatosMolino.Rows[9]["Referencia"].ToString() != "")
            {
                lblM10MAT.InnerText = DatosMolino.Rows[9]["Referencia"].ToString() + " " + DatosMolino.Rows[9]["Descripcion"].ToString();
                MueleM10.Visible = true;
                BorraM10.Visible = true;
            }
            if (DatosMolino.Rows[10]["Referencia"].ToString() != "")
            {
                lblM33MAT.InnerText = DatosMolino.Rows[10]["Referencia"].ToString() + " " + DatosMolino.Rows[10]["Descripcion"].ToString();
                MueleM33.Visible = true;
                BorraM33.Visible = true;
            }
            if (DatosMolino.Rows[11]["Referencia"].ToString() != "")
            {
                lblM14MAT.InnerText = DatosMolino.Rows[11]["Referencia"].ToString() + " " + DatosMolino.Rows[11]["Descripcion"].ToString();
                MueleM14.Visible = true;
                BorraM14.Visible = true;
            }
            if (DatosMolino.Rows[12]["Referencia"].ToString() != "")
            {
                lblM50MAT.InnerText = DatosMolino.Rows[12]["Referencia"].ToString() + " " + DatosMolino.Rows[12]["Descripcion"].ToString();
                MueleM50.Visible = true;
                BorraM50.Visible = true;
            }


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
                                  NAVE = p.Field<string>("NAVE"),
                                  MATERIAL = p.Field<string>("MATERIAL"),
                                  DESCRIPCION = p.Field<string>("DESCRIPCION"),
                                  MOLINO = leftJoin == null ? "" : leftJoin.Field<string>("Molino"),
                                  UBICACION = leftJoin == null ? "" : "(" + leftJoin.Field<string>("Ubicacion") + ")",
                              }).ToList();
            dgv_Materiales.DataSource = JoinResult;
            dgv_Materiales.DataBind();
        }

        //Gestion de molinos
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
                    //EDITAR MOLINO
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
                        lkb_Sort_Click("MATERIALES");
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
                        lkb_Sort_Click("MATERIALES");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                        break;
                    case "EditaM3":
                        DatosMolino.DefaultView.RowFilter = "Id = 3";
                        Auxmolino = DatosMolino.DefaultView.ToTable();
                        IDlabelMolino.InnerText = "3";
                        labelMolino.InnerText = Auxmolino.Rows[0]["Molino"].ToString();
                        lblMaterialActual.InnerText = Auxmolino.Rows[0]["Referencia"].ToString() + " " + Auxmolino.Rows[0]["Descripcion"].ToString();
                        lkb_Sort_Click("NAVE 3");
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
                        lkb_Sort_Click("NAVE 3");
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
                        lkb_Sort_Click("NAVE 5");
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
                        lkb_Sort_Click("NAVE 4");
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
                        lkb_Sort_Click("NAVE 4");
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
                        lkb_Sort_Click("NAVE 4");
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
                        lkb_Sort_Click("NAVE 4");
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
                        lkb_Sort_Click("NAVE 4");
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
                        lkb_Sort_Click("NAVE 5");
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
                        lkb_Sort_Click("NAVE 5");
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
                        lkb_Sort_Click("NAVE 4");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup1();", true);
                        break;

                    //ELIMINAR MOLINOS
                    case "BorraM1":
                        conexion.Actualiza_Material_Activo("1", "0");
                        Rellenar_grid();
                        lkb_Sort_Click("MATERIALES");
                        break;
                    case "BorraM2":
                        conexion.Actualiza_Material_Activo("2", "0");
                        Rellenar_grid();
                        lkb_Sort_Click("MATERIALES");
                        break;
                    case "BorraM3":
                        conexion.Actualiza_Material_Activo("3", "0");
                        Rellenar_grid();
                        lkb_Sort_Click("NAVE 3");
                        break;
                    case "BorraM5":
                        conexion.Actualiza_Material_Activo("5", "0");
                        Rellenar_grid();
                        lkb_Sort_Click("NAVE 3");
                        break;
                    case "BorraM8":
                        conexion.Actualiza_Material_Activo("8", "0");
                        Rellenar_grid();
                        lkb_Sort_Click("NAVE 5");
                        break;
                    case "BorraM10":
                        conexion.Actualiza_Material_Activo("10", "0");
                        Rellenar_grid();
                        lkb_Sort_Click("NAVE 4");
                        break;
                    case "BorraM17":
                        conexion.Actualiza_Material_Activo("4", "0");
                        Rellenar_grid();
                        lkb_Sort_Click("NAVE 4");
                        break;
                    case "BorraM7":
                        conexion.Actualiza_Material_Activo("6", "0");
                        Rellenar_grid();
                        lkb_Sort_Click("NAVE 4");
                        break;
                    case "BorraM6":
                        conexion.Actualiza_Material_Activo("7", "0");
                        Rellenar_grid();
                        lkb_Sort_Click("NAVE 4");
                        break;
                    case "BorraM4":
                        conexion.Actualiza_Material_Activo("9", "0");
                        Rellenar_grid();
                        lkb_Sort_Click("NAVE 4");
                        break;
                    case "BorraM33":
                        conexion.Actualiza_Material_Activo("11", "0");
                        Rellenar_grid();
                        lkb_Sort_Click("NAVE 5");
                        break;
                    case "BorraM14":
                        conexion.Actualiza_Material_Activo("12", "0");
                        Rellenar_grid();
                        lkb_Sort_Click("NAVE 5");
                        break;
                    case "BorraM50":
                        conexion.Actualiza_Material_Activo("13", "0");
                        Rellenar_grid();
                        lkb_Sort_Click("NAVE 4");
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
               
            }
            /*
            if(TipoAccion == "BtnBorrarMaterial")
            {
                conexion.Actualiza_Material_Activo(IDlabelMolino.InnerText, "0");
            }
            */
            Rellenar_grid();
            
            switch (IDlabelMolino.InnerText)
            {
                case "Molino Nº8":
                    lkb_Sort_Click("NAVE 5");
                    break;
                case "Molino Nº14":
                    lkb_Sort_Click("NAVE 5");
                    break;
                case "Molino Nº50":
                    lkb_Sort_Click("NAVE 4");
                    break;
                case "Molino Nº33":
                    lkb_Sort_Click("NAVE 5");
                    break;
                case "Molino Nº17":
                    lkb_Sort_Click("NAVE 4");
                    break;
                case "Molino Nº10":
                    lkb_Sort_Click("NAVE 4");
                    break;
                case "Molino Nº7":
                    lkb_Sort_Click("NAVE 4");
                    break;
                case "Molino Nº6":
                    lkb_Sort_Click("NAVE 4");
                    break;
                case "Molino Nº4":
                    lkb_Sort_Click("NAVE 4");
                    break;
                case "Molino Nº3":
                    lkb_Sort_Click("NAVE 3");
                    break;
                case "Molino Nº5":
                    lkb_Sort_Click("NAVE 3");
                    break;
                case "Molino Nº1":
                    lkb_Sort_Click("MATERIALES");
                    break;
                case "Molino Nº2":
                    lkb_Sort_Click("MATERIALES");
                    break;

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
                    lkb_Sort_Click("MATERIALES");
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
                    lkb_Sort_Click("MATERIALES");
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
                    lkb_Sort_Click("NAVE 3");
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
                    lkb_Sort_Click("NAVE 3");
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
                    lkb_Sort_Click("NAVE 5");
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
                    lkb_Sort_Click("NAVE 4");
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
                    lkb_Sort_Click("NAVE 4");
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
                    lkb_Sort_Click("NAVE 4");
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
                    lkb_Sort_Click("NAVE 4");
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
                    lkb_Sort_Click("NAVE 4");
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
                    lkb_Sort_Click("NAVE 5");
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
                    lkb_Sort_Click("NAVE 5");
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
                    lkb_Sort_Click("NAVE 4");
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ImprimeEtiquetasNEW('ETIFINAL');", true);
            Rellenar_grid();
            switch (IDlabelMolino.InnerText)
            {
                case "Molino Nº8":
                    lkb_Sort_Click("NAVE 5");
                    break;
                case "Molino Nº14":
                    lkb_Sort_Click("NAVE 5");
                    break;
                case "Molino Nº50":
                    lkb_Sort_Click("NAVE 4");
                    break;
                case "Molino Nº33":
                    lkb_Sort_Click("NAVE 5");
                    break;
                case "Molino Nº17":
                    lkb_Sort_Click("NAVE 4");
                    break;
                case "Molino Nº10":
                    lkb_Sort_Click("NAVE 4");
                    break;
                case "Molino Nº7":
                    lkb_Sort_Click("NAVE 4");
                    break;
                case "Molino Nº6":
                    lkb_Sort_Click("NAVE 4");
                    break;
                case "Molino Nº4":
                    lkb_Sort_Click("NAVE 4");
                    break;
                case "Molino Nº3":
                    lkb_Sort_Click("NAVE 3");
                    break;
                case "Molino Nº5":
                    lkb_Sort_Click("NAVE 3");
                    break;
                case "Molino Nº1":
                    lkb_Sort_Click("MATERIALES");
                    break;
                case "Molino Nº2":
                    lkb_Sort_Click("MATERIALES");
                    break;

            }

        }

        
        //Gestion de pestañas
        protected void lkb_Sort_Click(string e)
        {
            //Bind the data

            //Manage the selected tab.
            this.ManageTabsPostBack(TAB_EST_0, TAB_EST_1, TAB_EST_2, TAB_EST_3, TAB_EST_4, PILL_EST0, PILL_EST1, PILL_EST2, PILL_EST3, PILL_EST4, e);

        }

        private void ManageTabsPostBack(HtmlGenericControl TAB_EST_0, HtmlGenericControl TAB_EST_1, HtmlGenericControl TAB_EST_2, HtmlGenericControl TAB_EST_3, HtmlGenericControl TAB_EST_4, HtmlButton PILL_EST0, HtmlButton PILL_EST1, HtmlButton PILL_EST2, HtmlButton PILL_EST3, HtmlButton PILL_EST4, string grid)
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


            //activate the the tab and pane the user was viewing
            switch (grid)
            {
                case "S0":
                    PILL_EST0.Attributes.Add("class", "nav-link shadow  border border-dark  pe-0 ps-0 active");
                    TAB_EST_0.Attributes.Add("class", "tab-pane fade show active");
                    break;
                case "NAVE 5":
                    PILL_EST1.Attributes.Add("class", "nav-link shadow  border border-dark pe-0 ps-0 active");
                    TAB_EST_1.Attributes.Add("class", "tab-pane fade show active");
                    break;
                case "NAVE 4":
                    PILL_EST2.Attributes.Add("class", "nav-link shadow  border border-dark pe-0 ps-0 active");
                    TAB_EST_2.Attributes.Add("class", "tab-pane fade show active");
                    break;
                case "NAVE 3":
                    PILL_EST3.Attributes.Add("class", "nav-link shadow  border border-dark pe-0 ps-0 active");
                    TAB_EST_3.Attributes.Add("class", "tab-pane fade show active");
                    break;
                case "MATERIALES":
                    PILL_EST4.Attributes.Add("class", "nav-link shadow  border border-dark pe-0 ps-0 active");
                    TAB_EST_4.Attributes.Add("class", "tab-pane fade show active");
                    break;
            }
        }


        [WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static bool ImprimirEtiquetasAuxiliares(string MATERIAL, string INPUTOPERARIO, string MOLINO, string INPUTCANTIDAD, string TIPO)
        {
            Conexion_MATERIALES conexion = new Conexion_MATERIALES();
            // Printer IP Address and communication port
            string ipAddress = "10.0.0.180"; //MATERIALES
            //string ipAddress = "10.0.0.164"; //THERMOBMS
            int port = 9100;
            string producto = conexion.Devuelve_Material_Moler(MATERIAL.Trim());
            string descripcion = conexion.Devuelve_MateriaPrima(producto);
            string nmolino = conexion.Devuelve_Descripcion_Molino(MOLINO).Replace("º","o");
            string operario = INPUTOPERARIO;
            string fechayhora = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            string lote = DateTime.Now.ToString("yy")+ DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd");
            string cantidad = INPUTCANTIDAD.Trim();
            string medida = "";
            if (cantidad != "0" && cantidad != "")
            {
                medida = "Kgs.";
            }
            string PIETIQUETA = "MATERIAL RECICLADO - THERMOLYMPIC S.L.";
            if (TIPO == "PROVISIONAL")
            {
                PIETIQUETA = "ETIQUETA PROVISIONAL PARA MATERIAL RECICLADO";
            }

            string ZPLString = "^[L,2^FS^XZ" +
                 "^XA^MMT^LS0^LT0^FWR^FPH^MTD^MNM^LH2,10^FS^PW815^ML2480" +
                 "^FX Lineas verticales" +
                 "^FO15,25^GB785,1,3^FS" +
                 "^FO600,1025^GB200,1,3^FS" +
                 "^FO55,580^GB145,1,3^FS" +
                 "^FO55,1000^GB145,1,3^FS" +
                 "^FO200,1150^GB400,1,3^FS" +
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
                 "^FO155,600^A0R35,35^FDOPERARIO:^FS" +
                 "^FO155,1020^A0R35,35^FDMOLINO:^FS" +
                 "^FO17,50^FR^FB1600,1,0,C,0^A0R35,35^FD"+PIETIQUETA+"^FS" +
                 "^FX Campos variables" +
                 "^FO600,105^A0R130,130^FD"+producto+"^FS" +
                 "^FO600,1100^A0R130,130^FD"+lote+"^FS" +
                 "^FO400,50^FB1625,2,0,J,0^A0R,130,130^" +
                 "^FO0,45^A0R70,70^FD"+fechayhora+"^FS" +
                 "^FO70,600^A0R70,70^FD"+operario+"^FS" +
                 "^FO70,1020^A0R70,70^FD"+ nmolino + "^FS" +
                 "^FO240,60^FB1100,3,0,C,0^A0R,90,90^FD"+descripcion+"^FS" +
                 "^FO160,350^FB1100,3,0,R,0^A0R,130,130^FD"+cantidad+"^FS" +
                 "^FO280,990^FB1100,3,0,C,0^A0R,80,80^FD"+medida+"^FS" +
                 "^FX Codigos de barra" +
                 "^FO615,680^BCR,170,N,N,N,N^FD>:30S"+producto+"^FS" +
                 "^FO200,1260^GFA,6272,6272,28,,:::gJ07FE,gI03IF8,gI07IFEL06,gH01KF8K0F,gH07KFEK0F8,gG01MFJ01F8,gG03MFCI01F8,gG0NFEI03F8,g03OF8007F8,g0PFC007F8,Y01QF00FF8,Y07QFC1FF8,X01RFE1FF8,X07SFBFF8,X0WF8,W03WF8,W0XF8,V03XF8,V07XF8,U01YF8,U07YF8,T01gF8,T03gF8,T0gGF8,S03gGF8,S0gHF8,R01gHFC,R07gHFC,Q01gIFC,Q07gIFC,Q0gJFC,P03gJFC,P0gKFCK08,O03F8gIFCK0E,S03gGFCJ01F8,T07gFCJ03FC,T01gFCJ03FF,U07YFCJ07FF8,U01YFCJ0IFE,V07XFCJ0JF8,V03XFCI01JFC,O0IFCJ0XFCI03KF,N0KFCI07WFCI03KF8,M03LFI01WFCI07KFE,M0MFCI0WFCI0MF8,L01NFI03VFCI0MFC,L03NFC001VFC001NF,L07OFI07UFC003NF8,L0PF8001UFC003NFE,L0PFEI0UFC007OF8,L0QFI03TFC00PFC,K01QFC001TFC00QF,K01QFEI07SFC01QF8,K01RF8001SF803QFE,K01RFCI0SF003RF8,K01SFI03QFC007RFC,K01SF8001QFI0TF,K01SF8I07OFCI0TF8,K01SF8I03OFI01TFE,K01SF8J0NFCI03UF,K01SF8J03MFJ03UF,K01SF8J01LFCJ07UF8,K01SF8K07KF8J0VF8,K01SF8K03JFEK0VFC,K01SF8K03JF8J01VFC,K01SF8K07IFEK03VFC,K01SF8K0JF8K03VFC,K01SF8K0IFEL07VFC,K01SF8J01IF8L0WFE,K01SF8J01FFEM0WFE,K01SF8J03FFCL01WFE,K01SF8J07FFM03WFE,K01SF8J07FCM03WFE,K01SF8J0FFN07WFC,K01SF8J07CN0XFC,K01SF8J06O0XFC,K01SF8T0XFC,K01SF8T03WF8,K01SF8T01WF8,K01SF8U07VF01,K01SF8U03VF01,K01SF8V0UFE01,K01SF8V07TFC03,K01SF8V01TFC03,K01SF8W0TF807,K01SF8W03SF007,K01SF8W01RFE00F,K01SF8X07QFC00F,K01SF8X01QF801F,K01SF8Y0QF001F,K01SF8Y03OFC003F,K01SF8Y01OF8007F,K01SF8g07NFI0FF,K01SF8g03MFC001FF,K01SF8gG0MFI03FF,K01SF8gG07KFEI07FF,K01SF8gG01KF8001IF,K01SF8gH07IFEI03IF,K01SF8gH03IF8I07IF,K01SF8gI0FFEI01JF,K01SF8gI07F8I03JF,K01SF8gI01EJ0KF,K01SF8gJ08I01KF,K01SF8gN07KF,K01SF8gM01LF,K01SFgN07LF,hL01MF,hL07MF,hK01NF,hK07NF,hJ01OF,gW078K07OF,gW07CJ01PF,gW07CJ07PF,gW07EI01QF,gW07EI07QF,Q01CgJ07F003RF,Q07FgJ07F00SF,Q0FF8gI0FF83SF,P03FFEgI0FF8TF,P07IF8gH0WF,O01JFEgH0WF,O03KFgH0WF,O0LFCgG0WF,N03MFgG0WF,N07MFCg0WF,M01NFEg0WF,M03OF8Y0WF,M0PFEY0WF,L01QF8X0WF,L07QFCX0WF,L0SFW01WF,K03SFCV01WF,K07TFV01WF,J01UF8U01WF,J03UFEU01WF,J0WF8T01WF,I01WFCT01WF,I07XFT01WF,001YFCS01WF,003YFCS01WF,007YFCS01WF,00gFCS01WF,00WFER0CI03WF,00WFCQ03EI03WF,00WFCQ0FEI03VFE,007VFCP01FFI03VFE,K01SFCP07FFI03VFE,K01SFCO01IF8003VFC,K01SFCO03IFC003VFC,K01SFCO0JFC003VFC,K01SFCN03JFE003VF8,K01SFCN07JFE003VF,K01SFCM01LF003VF,K01SFCM07LF803UFE,K01SFCM0MF803UFC,K01SFCL03MFC07UF8,K01SFCL0NFC07UF,K01SFCK01NFE07TFE,K01SFCK07NFE07TF8,K01SFCK0PF07SFE,K01SFCJ03PF81SF8,K01SFCJ0QF807QFE,K01SFCI03QFC03QFC,K01SFCI07QFC00QF,K01SFC001RFE003OFC,K01SFC003SFI0NFE,K01SFC00TFI07MFC,K01SFC03TF8001MF,K01SFC03TF8I07KF8,K01SFC03TFCI03KF8,K01SFC03TFCJ0KFC,K01SFC03TFEJ03JFC,K01SFC03UFJ01JFC,L0SFC03UFK07IFE,L0SFC03UF8J01IFE,L0SFC03UF8K0JF,L07RFC03UFCK03IF,L03RFC03UFEL0IF8,L03RFC03UFEL07FF8,L01RFC03VFL01FFC,M0RFC03VFM07FC,M03QFC03VF8L03FC,N0QFC03VF8M0FC,N07PFC03VFC,N01PFC03VFE,O07OFC03VFE,O01OFC03WF,P07NFC03VFE,P03NFC03VF8,Q0NFC03VF,Q03MFE03UFC,R0MFE03UF8,R03LFE03TFE,S0LFE03TFC,S07KFE03TF,S01KFE01SFC,T07KF01SF8,T01KF01RFE,U07JF00RFC,U01JF00RF,V0JF807PFE,V03IF807PF8,W0IFC03PF,W03FFC01OFC,X0FFE01OF8,X07FE00NFE,X01FF007MFC,Y07F003MF,Y01F801LFE,g07C00LF8,g01E007KF,gG0F003JFC,gG03001JF,gH0C007FFE,gK01FF8,,:::^FS" +
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