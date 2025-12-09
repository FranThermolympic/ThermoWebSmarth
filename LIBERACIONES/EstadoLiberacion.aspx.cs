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

namespace ThermoWeb.LIBERACIONES
{
    public partial class EstadoLiberacion : System.Web.UI.Page
    {

        private static DataSet ds_Liberaciones = new DataSet();
        private static string SELECNAVE = "Máq.";
        private static string SELECESTADO = "0";
        private static string SELECORDERBY = "0";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SELECNAVE = "Máq.";
                SELECESTADO = "0";
                SELECORDERBY = "0";
                Rellenar_Grid(null, null);
                
            }
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
           
            Conexion_SMARTH SHConexion = new Conexion_SMARTH();
            SHConexion.LimpiarOrdenesProduciendoBMS();
            SHConexion.leer_OrdenesProduciendoBMS();

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

            //ds_Liberaciones = conexion.devuelve_OrdenesProduciendoLOCAL(Nave3);


            DataSet ds_Desviaciones = SHConexion.Devuelve_OrdenesProduciendoDESVIADAS();
            GridDesviaciones.DataSource = ds_Desviaciones;
            GridDesviaciones.DataBind();

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

        protected void GridView0_DataBound(object sender, EventArgs e)
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

        protected void GridViewDESV_DataBound(object sender, EventArgs e)
        {
            for (int i = 0; i <= GridDesviaciones.Rows.Count - 1; i++)
            {
                Label Q1E = (Label)GridDesviaciones.Rows[i].FindControl("Desv1Prod");
                if (Q1E.Text != "1")
                {
                    Label auxQ1E = (Label)GridDesviaciones.Rows[i].FindControl("Q1PROD");
                    auxQ1E.Visible = false;
                }
                
                Label Q2E = (Label)GridDesviaciones.Rows[i].FindControl("Desv2Prod");
                if (Q2E.Text != "1")
                {
                    Label auxQ2E = (Label)GridDesviaciones.Rows[i].FindControl("Q2PROD");
                    auxQ2E.Visible = false;
                }
               
                Label Q3E = (Label)GridDesviaciones.Rows[i].FindControl("Desv3Prod");
                if (Q3E.Text != "1")
                {
                    Label auxQ3E = (Label)GridDesviaciones.Rows[i].FindControl("Q3PROD");
                    auxQ3E.Visible = false;
                }
               
                Label Q4E = (Label)GridDesviaciones.Rows[i].FindControl("Desv4Prod");
                if (Q4E.Text != "1")
                {
                    Label auxQ4E = (Label)GridDesviaciones.Rows[i].FindControl("Q4PROD");
                    auxQ4E.Visible = false;
                }
              
                Label Q5E = (Label)GridDesviaciones.Rows[i].FindControl("Desv5Prod");
                if (Q5E.Text != "1")
                {
                    //dgv_Liberaciones5.Rows[i].Cells[6].BackColor = System.Drawing.Color.Yellow;
                    Label auxQ5E = (Label)GridDesviaciones.Rows[i].FindControl("Q5PROD");
                    auxQ5E.ForeColor = System.Drawing.Color.Transparent;
                    auxQ5E.Visible = false;
                }
                
                Label Q6E = (Label)GridDesviaciones.Rows[i].FindControl("Desv6Prod");
                if (Q6E.Text != "1")
                {
                    Label auxQ6E = (Label)GridDesviaciones.Rows[i].FindControl("Q6PROD");
                    auxQ6E.Visible = false;
                }
                Label Q6C = (Label)GridDesviaciones.Rows[i].FindControl("Desv6Cal");
                if (Q6C.Text != "1")
                {
                    Label auxQ6C = (Label)GridDesviaciones.Rows[i].FindControl("Q6CAL");
                    auxQ6C.Visible = false;
                }

                Label Q7E = (Label)GridDesviaciones.Rows[i].FindControl("Desv7Prod");
                if (Q7E.Text != "1")
                {
                    Label auxQ7E = (Label)GridDesviaciones.Rows[i].FindControl("Q7PROD");
                    auxQ7E.Visible = false;
                }
                Label Q7C = (Label)GridDesviaciones.Rows[i].FindControl("Desv7Cal");
                if (Q7C.Text != "1")
                {
                    Label auxQ7C = (Label)GridDesviaciones.Rows[i].FindControl("Q7CAL");
                    auxQ7C.Visible = false;
                }

                Label Q8E = (Label)GridDesviaciones.Rows[i].FindControl("Desv8Prod");
                if (Q8E.Text != "1")
                {
                    Label auxQ8E = (Label)GridDesviaciones.Rows[i].FindControl("Q8PROD");
                    auxQ8E.Visible = false;
                }
                Label Q8C = (Label)GridDesviaciones.Rows[i].FindControl("Desv8Cal");
                if (Q8C.Text != "1")
                {
                    Label auxQ8C = (Label)GridDesviaciones.Rows[i].FindControl("Q8CAL");
                    auxQ8C.Visible = false;
                }

                Label Q9E = (Label)GridDesviaciones.Rows[i].FindControl("Desv9Prod");
                if (Q9E.Text != "1")
                {
                    Label auxQ9E = (Label)GridDesviaciones.Rows[i].FindControl("Q9PROD");
                    auxQ9E.Visible = false;
                }
                Label Q9C = (Label)GridDesviaciones.Rows[i].FindControl("Desv9Cal");
                if (Q9C.Text != "1")
                {
                    Label auxQ9C = (Label)GridDesviaciones.Rows[i].FindControl("Q9CAL");
                    auxQ9C.Visible = false;
                }

                Label Q10C = (Label)GridDesviaciones.Rows[i].FindControl("Desv10Cal");
                if (Q10C.Text != "1")
                {
                    Label auxQ10C = (Label)GridDesviaciones.Rows[i].FindControl("Q10CAL");
                    auxQ10C.Visible = false;
                }

                Label LibPROD = (Label)GridDesviaciones.Rows[i].FindControl("LiberadoPROD");
                Label NCPROD = (Label)GridDesviaciones.Rows[i].FindControl("DesvNCPROD");
                if (Convert.ToInt32(LibPROD.Text) > 0 && NCPROD.Text == "False")
                {
                    Label auxNCPROD = (Label)GridDesviaciones.Rows[i].FindControl("DesvNCPRODText");
                    auxNCPROD.Visible = true;
                }

                
                Label GP12PROD = (Label)GridDesviaciones.Rows[i].FindControl("DesvGP12PROD");
                if (Convert.ToInt32(LibPROD.Text) > 0 && GP12PROD.Text == "False")
                {
                    Label auxGP12PROD = (Label)GridDesviaciones.Rows[i].FindControl("DesvGP12PRODText");
                    auxGP12PROD.Visible = true;
                }

                Label LibCAL = (Label)GridDesviaciones.Rows[i].FindControl("LiberadoCal");
                Label NCCAL = (Label)GridDesviaciones.Rows[i].FindControl("DesvNCCAL");
                if (Convert.ToInt32(LibCAL.Text) > 0 && NCCAL.Text == "False")
                {
                    Label auxNCCAL = (Label)GridDesviaciones.Rows[i].FindControl("DesvNCCALText");
                    auxNCCAL.Visible = true;
                }

                Label GP12CAL = (Label)GridDesviaciones.Rows[i].FindControl("DesvGP12CAL");
                if (Convert.ToInt32(LibCAL.Text) > 0 && GP12CAL.Text == "False")
                {
                    Label auxGP12CAL = (Label)GridDesviaciones.Rows[i].FindControl("DesvGP12CALText");
                    auxGP12CAL.Visible = true;
                }

                Label REsultadoLOTES = (Label)GridDesviaciones.Rows[i].FindControl("ResultadoLOT");
                if (REsultadoLOTES.Text == "1")
                {
                    Label auxREsultadoLOTES = (Label)GridDesviaciones.Rows[i].FindControl("ResultadoLOTTEXT");
                    auxREsultadoLOTES.Visible = true;
                }

                Label REsultadoPARAM = (Label)GridDesviaciones.Rows[i].FindControl("ResultadoPARAM");
                if (REsultadoPARAM.Text == "1")
                {
                    Label auxREsultadoPARAM = (Label)GridDesviaciones.Rows[i].FindControl("ResultadoPARAMTEXT");
                    auxREsultadoPARAM.Visible = true;
                }

                Label EstadoActual = (Label)GridDesviaciones.Rows[i].FindControl("lblEstadoActual");
                if (EstadoActual.Text == "En marcha")
                {
                    //dgv_Liberaciones.Rows[i].Cells[8].BackColor = System.Drawing.Color.Yellow;
                    EstadoActual.ForeColor = System.Drawing.Color.Black;
                    GridDesviaciones.Rows[i].Cells[0].BackColor = System.Drawing.Color.LightGreen;
     
                }

                else if (EstadoActual.Text == "")
                {
                    EstadoActual.ForeColor = System.Drawing.Color.Black;
                    GridDesviaciones.Rows[i].Cells[0].BackColor = System.Drawing.Color.LightPink;

                }
                else
                {
                    EstadoActual.ForeColor = System.Drawing.Color.Black;
                    GridDesviaciones.Rows[i].Cells[0].BackColor = System.Drawing.Color.LightGoldenrodYellow;
        
                }
                /*
                Label Accion = (Label)GridDesviaciones.Rows[i].FindControl("lblAccionLiberado");
                if (Accion.Text == "4")
                {
                    GridDesviaciones.Rows[i].BackColor = System.Drawing.Color.Red;
                    Accion.ForeColor = System.Drawing.Color.White;
                    Accion.Visible = true;
                    Accion.Text = "Liberación NOK. Producción retenida";
                }
                */
            }
        }


        // carga la lista utilizando un filtro
        public void ContactsGridView_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Redirect")
            {
                Response.Redirect("LiberacionSerie.aspx?ORDEN=" + e.CommandArgument.ToString());
            }
        }
       
        
    }

}