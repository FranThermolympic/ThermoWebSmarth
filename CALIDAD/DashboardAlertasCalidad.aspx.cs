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

namespace ThermoWeb.CALIDAD
{
    public partial class DashboardAlertasCalidad : System.Web.UI.Page
    {

        private static DataSet ds_DocumentosGP12 = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            {
                /*if (Request.QueryString["REFERENCIA"] != null)
                {
                    DataSet imagenes = new DataSet();
                    Conexion_DOCUMENTAL conexion = new Conexion_DOCUMENTAL();
                    imagenes = conexion.DatasetDetectadosGP12(Request.QueryString["REFERENCIA"]);
                    foreach (DataRow row in imagenes.Tables[0].Rows)
                    {
                        HtmlGenericControl divItem = new HtmlGenericControl("div");
                        divItem.Attributes["class"] = "item";
                        //divItem.ID = "prueba";
                        Image img = new Image();
                        img.ImageUrl = row["ImagenDefecto1"].ToString();
                        img.Visible = true;
                        divItem.Controls.Add(img);
                        //img.Width = 100;
                        ACTIVOS.Controls.Add(divItem);
                    }
                }*/

            }
            if (!IsPostBack)
            {
               
                 HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate");
                 HttpContext.Current.Response.AddHeader("Pragma", "no-cache");
                 HttpContext.Current.Response.AddHeader("Expires", "0");
                 //cargarframes();
                    DataSet NC = new DataSet();
                    Conexion_CALIDAD conexion = new Conexion_CALIDAD();
                    NC = conexion.Devuelve_Listado_NoConformidadesSMARTH(DateTime.Now.Year.ToString());
                    //NoConformidad 1

                    string NC1imagen1 = NC.Tables[0].Rows[0]["ImagenNODefecto"].ToString();
                    string NC1imagen2 = NC.Tables[0].Rows[0]["ImagenDefecto1"].ToString();
                    string NC1imagen3 = NC.Tables[0].Rows[0]["ImagenDefecto2"].ToString();
                    string NC1imagen4 = NC.Tables[0].Rows[0]["ImagenTrazabilidad1"].ToString();
                    string NC1imagen5 = NC.Tables[0].Rows[0]["ImagenTrazabilidad2"].ToString();
                    
                    NC1_Cliente.Text = NC.Tables[0].Rows[0]["Cliente"].ToString();
                    NC1_TIPO.Text = NC.Tables[0].Rows[0]["NCTEXT"].ToString();
                    //NC1_ID.Text = "NC-" + NC.Tables[0].Rows[0]["IdNoConformidad"].ToString();
                    NC1_ID.Text = "NC-" + NC.Tables[0].Rows[0]["IdNoConformidad"].ToString() + "  (" + Convert.ToDateTime(NC.Tables[0].Rows[0]["FechaOriginal"]).ToString("dd/MM/yyyy") + ")";
                    NC1DefectoReclamado.Text = NC.Tables[0].Rows[0]["DescripcionProblema"].ToString();
                    NC1Referencia.Text = NC.Tables[0].Rows[0]["Referencia"].ToString() + " - " + NC.Tables[0].Rows[0]["Descripcion"].ToString();

                if (NC1imagen1 != "")
                    {
                        NC1_IMG1.Src = NC1imagen1;
                    }
                else
                    {
                        NC1_IMG1.Src = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";
                    }
                if (NC1imagen2 != "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg")
                    {
                        NC1_IMG2.Src = NC1imagen2;
                    }
                else
                    {
                        NC1_IMG2.Src = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";
                    }
                if (NC1imagen3 != "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg")
                    {
                        NC1_IMG3.Src = NC1imagen3;
                    }
                else
                    {
                        CAR11.Visible = false;
                        CAR11A.Visible = false;
                    }

                if (NC1imagen4 != "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg")
                    {
                        NC1_IMG4.Src = NC1imagen4;
                    }
                else
                    {
                        CAR12.Visible = false;
                        CAR12A.Visible = false;
                    }
                if (NC1imagen5 != "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg")
                    {
                        NC1_IMG5.Src = NC1imagen5;
                    }
                else
                    {
                        CAR13.Visible = false;
                        CAR13A.Visible = false;
                    }


                //NC2
                if (NC.Tables[0].Rows.Count > 1)
                {
                    NC2.Visible = true;
                    string NC2imagen1 = NC.Tables[0].Rows[1]["ImagenNODefecto"].ToString();
                    string NC2imagen2 = NC.Tables[0].Rows[1]["ImagenDefecto1"].ToString();
                    string NC2imagen3 = NC.Tables[0].Rows[1]["ImagenDefecto2"].ToString();
                    string NC2imagen4 = NC.Tables[0].Rows[1]["ImagenTrazabilidad1"].ToString();
                    string NC2imagen5 = NC.Tables[0].Rows[1]["ImagenTrazabilidad2"].ToString();

                    NC2_Cliente.Text = NC.Tables[0].Rows[1]["Cliente"].ToString();
                    NC2_TIPO.Text = NC.Tables[0].Rows[1]["NCTEXT"].ToString();
                    NC2_ID.Text = "NC-" + NC.Tables[0].Rows[1]["IdNoConformidad"].ToString() + "  (" + Convert.ToDateTime(NC.Tables[0].Rows[1]["FechaOriginal"]).ToString("dd/MM/yyyy") + ")";
                    NC2DefectoReclamado.Text = NC.Tables[0].Rows[1]["DescripcionProblema"].ToString();
                    NC2Referencia.Text = NC.Tables[0].Rows[1]["Referencia"].ToString() + " - " + NC.Tables[0].Rows[1]["Descripcion"].ToString();


                    if (NC2imagen1 != "")
                        {
                            NC2_IMG1.Src = NC2imagen1;
                        }
                    else
                        {
                            NC2_IMG1.Src = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";
                        }
                    if (NC2imagen2 != "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg")
                        {
                            NC2_IMG2.Src = NC2imagen2;
                        }
                    else
                        {
                            NC2_IMG2.Src = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";
                        }
                    if (NC2imagen3 != "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg")
                        {
                            NC2_IMG3.Src = NC2imagen3;
                        }
                    else
                        {
                            CAR21.Visible = false;
                            CAR21A.Visible = false;
                        }

                    if (NC2imagen4 != "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg")
                        {
                            NC2_IMG4.Src = NC2imagen4;
                        }
                        else
                        {
                            CAR22.Visible = false;
                            CAR22A.Visible = false;
                        }
                    if (NC2imagen5 != "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg")
                        {
                            NC2_IMG5.Src = NC2imagen5;
                        }
                    else
                        {
                            CAR23.Visible = false;
                            CAR23A.Visible = false;
                        }
                }
                //NC3
                if (NC.Tables[0].Rows.Count > 2)
                {
                    NC3.Visible = true;
                    string NC3imagen1 = NC.Tables[0].Rows[2]["ImagenNODefecto"].ToString();
                    string NC3imagen2 = NC.Tables[0].Rows[2]["ImagenDefecto1"].ToString();
                    string NC3imagen3 = NC.Tables[0].Rows[2]["ImagenDefecto2"].ToString();
                    string NC3imagen4 = NC.Tables[0].Rows[2]["ImagenTrazabilidad1"].ToString();
                    string NC3imagen5 = NC.Tables[0].Rows[2]["ImagenTrazabilidad2"].ToString();

                    NC3_Cliente.Text = NC.Tables[0].Rows[2]["Cliente"].ToString();
                    NC3_TIPO.Text = NC.Tables[0].Rows[2]["NCTEXT"].ToString();
                    //NC3_ID.Text = "NC-" + NC.Tables[0].Rows[2]["IdNoConformidad"].ToString();
                    NC3_ID.Text = "NC-" + NC.Tables[0].Rows[2]["IdNoConformidad"].ToString() + "  (" + Convert.ToDateTime(NC.Tables[0].Rows[2]["FechaOriginal"]).ToString("dd/MM/yyyy") + ")";
                    NC3DefectoReclamado.Text = NC.Tables[0].Rows[2]["DescripcionProblema"].ToString();
                    NC3Referencia.Text = NC.Tables[0].Rows[2]["Referencia"].ToString() + " - " + NC.Tables[0].Rows[2]["Descripcion"].ToString();

                    if (NC3imagen1 != "")
                    {
                        NC3_IMG1.Src = NC3imagen1;
                    }
                    else
                    {
                        NC3_IMG1.Src = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";
                    }
                    if (NC3imagen2 != "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg")
                    {
                        NC3_IMG2.Src = NC3imagen2;
                    }
                    else
                    {
                        NC3_IMG2.Src = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";
                    }
                    if (NC3imagen3 != "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg")
                    {
                        NC3_IMG3.Src = NC3imagen3;
                    }
                    else
                    {
                        CAR31.Visible = false;
                        CAR31A.Visible = false;
                    }

                    if (NC3imagen4 != "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg")
                    {
                        NC3_IMG4.Src = NC3imagen4;
                    }
                    else
                    {
                        CAR32.Visible = false;
                        CAR32A.Visible = false;
                    }
                    if (NC3imagen5 != "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg")
                    {
                        NC3_IMG5.Src = NC3imagen5;
                    }
                    else
                    {
                        CAR33.Visible = false;
                        CAR33A.Visible = false;
                    }
                }
                //NC4
                if (NC.Tables[0].Rows.Count > 3)
                {
                    NC4.Visible = true;
                    string NC4imagen1 = NC.Tables[0].Rows[3]["ImagenNODefecto"].ToString();
                    string NC4imagen2 = NC.Tables[0].Rows[3]["ImagenDefecto1"].ToString();
                    string NC4imagen3 = NC.Tables[0].Rows[3]["ImagenDefecto2"].ToString();
                    string NC4imagen4 = NC.Tables[0].Rows[3]["ImagenTrazabilidad1"].ToString();
                    string NC4imagen5 = NC.Tables[0].Rows[3]["ImagenTrazabilidad2"].ToString();

                    NC4_Cliente.Text = NC.Tables[0].Rows[3]["Cliente"].ToString();
                    NC4_TIPO.Text = NC.Tables[0].Rows[3]["NCTEXT"].ToString();
                    //NC4_ID.Text = "NC-" + NC.Tables[0].Rows[3]["IdNoConformidad"].ToString();
                    NC4_ID.Text = "NC-" + NC.Tables[0].Rows[3]["IdNoConformidad"].ToString() + "  (" + Convert.ToDateTime(NC.Tables[0].Rows[3]["FechaOriginal"]).ToString("dd/MM/yyyy") + ")";
                    NC4DefectoReclamado.Text = NC.Tables[0].Rows[3]["DescripcionProblema"].ToString();
                    NC4Referencia.Text = NC.Tables[0].Rows[3]["Referencia"].ToString() + " - " + NC.Tables[0].Rows[3]["Descripcion"].ToString();

                    if (NC4imagen1 != "")
                    {
                        NC4_IMG1.Src = NC4imagen1;
                    }
                    else
                    {
                        NC4_IMG1.Src = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";
                    }
                    if (NC4imagen2 != "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg")
                    {
                        NC4_IMG2.Src = NC4imagen2;
                    }
                    else
                    {
                        NC4_IMG2.Src = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";
                    }
                    if (NC4imagen3 != "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg")
                    {
                        NC4_IMG3.Src = NC4imagen3;
                    }
                    else
                    {
                        CAR41.Visible = false;
                        CAR41A.Visible = false;
                    }

                    if (NC4imagen4 != "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg")
                    {
                        NC4_IMG4.Src = NC4imagen4;
                    }
                    else
                    {
                        CAR42.Visible = false;
                        CAR42A.Visible = false;
                    }
                    if (NC4imagen5 != "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg")
                    {
                        NC4_IMG5.Src = NC4imagen5;
                    }
                    else
                    {
                        CAR43.Visible = false;
                        CAR43A.Visible = false;
                    }
                }
                //NC5
                if (NC.Tables[0].Rows.Count > 4)
                {
                    NC5.Visible = true;
                    string NC5imagen1 = NC.Tables[0].Rows[4]["ImagenNODefecto"].ToString();
                    string NC5imagen2 = NC.Tables[0].Rows[4]["ImagenDefecto1"].ToString();
                    string NC5imagen3 = NC.Tables[0].Rows[4]["ImagenDefecto2"].ToString();
                    string NC5imagen4 = NC.Tables[0].Rows[4]["ImagenTrazabilidad1"].ToString();
                    string NC5imagen5 = NC.Tables[0].Rows[4]["ImagenTrazabilidad2"].ToString();

                    NC5_Cliente.Text = NC.Tables[0].Rows[4]["Cliente"].ToString();
                    NC5_TIPO.Text = NC.Tables[0].Rows[4]["NCTEXT"].ToString();
                    //NC5_ID.Text = "NC-" + NC.Tables[0].Rows[4]["IdNoConformidad"].ToString();
                    NC5_ID.Text = "NC-" + NC.Tables[0].Rows[4]["IdNoConformidad"].ToString() + "  (" + Convert.ToDateTime(NC.Tables[0].Rows[4]["FechaOriginal"]).ToString("dd/MM/yyyy") + ")";
                    NC5DefectoReclamado.Text = NC.Tables[0].Rows[4]["DescripcionProblema"].ToString();
                    NC5Referencia.Text = NC.Tables[0].Rows[4]["Referencia"].ToString() + " - " + NC.Tables[0].Rows[4]["Descripcion"].ToString();


                    if (NC5imagen1 != "")
                    {
                        NC5_IMG1.Src = NC5imagen1;
                    }
                    else
                    {
                        NC5_IMG1.Src = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";
                    }
                    if (NC5imagen2 != "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg")
                    {
                        NC5_IMG2.Src = NC5imagen2;
                    }
                    else
                    {
                        NC5_IMG2.Src = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";
                    }
                    if (NC5imagen3 != "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg")
                    {
                        NC5_IMG3.Src = NC5imagen3;
                    }
                    else
                    {
                        CAR51.Visible = false;
                        CAR51A.Visible = false;
                    }

                    if (NC5imagen4 != "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg")
                    {
                        NC5_IMG4.Src = NC5imagen4;
                    }
                    else
                    {
                        CAR52.Visible = false;
                        CAR52A.Visible = false;
                    }
                    if (NC5imagen5 != "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg")
                    {
                        NC5_IMG5.Src = NC5imagen5;
                    }
                    else
                    {
                        CAR53.Visible = false;
                        CAR53A.Visible = false;
                    }
                }
                //NC6
                if (NC.Tables[0].Rows.Count > 5)
                {
                    NC6.Visible = true;
                    string NC6imagen1 = NC.Tables[0].Rows[5]["ImagenNODefecto"].ToString();
                    string NC6imagen2 = NC.Tables[0].Rows[5]["ImagenDefecto1"].ToString();
                    string NC6imagen3 = NC.Tables[0].Rows[5]["ImagenDefecto2"].ToString();
                    string NC6imagen4 = NC.Tables[0].Rows[5]["ImagenTrazabilidad1"].ToString();
                    string NC6imagen5 = NC.Tables[0].Rows[5]["ImagenTrazabilidad2"].ToString();

                    NC6_Cliente.Text = NC.Tables[0].Rows[5]["Cliente"].ToString();
                    NC6_TIPO.Text = NC.Tables[0].Rows[5]["NCTEXT"].ToString();
                    //NC6_ID.Text = "NC-" + NC.Tables[0].Rows[5]["IdNoConformidad"].ToString();
                    NC6_ID.Text = "NC-" + NC.Tables[0].Rows[5]["IdNoConformidad"].ToString() + "  (" + Convert.ToDateTime(NC.Tables[0].Rows[5]["FechaOriginal"]).ToString("dd/MM/yyyy") + ")";
                    NC6DefectoReclamado.Text = NC.Tables[0].Rows[5]["DescripcionProblema"].ToString();
                    NC6Referencia.Text = NC.Tables[0].Rows[5]["Referencia"].ToString() + " - " + NC.Tables[0].Rows[5]["Descripcion"].ToString();


                    if (NC6imagen1 != "")
                    {
                        NC6_IMG1.Src = NC6imagen1;
                    }
                    else
                    {
                        NC6_IMG1.Src = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";
                    }
                    if (NC6imagen2 != "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg")
                    {
                        NC6_IMG2.Src = NC6imagen2;
                    }
                    else
                    {
                        NC6_IMG2.Src = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";
                    }
                    if (NC6imagen3 != "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg")
                    {
                        NC6_IMG3.Src = NC6imagen3;
                    }
                    else
                    {
                        CAR61.Visible = false;
                        CAR61A.Visible = false;
                    }

                    if (NC6imagen4 != "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg")
                    {
                        NC6_IMG4.Src = NC6imagen4;
                    }
                    else
                    {
                        CAR62.Visible = false;
                        CAR62A.Visible = false;
                    }
                    if (NC6imagen5 != "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg")
                    {
                        NC6_IMG5.Src = NC6imagen5;
                    }
                    else
                    {
                        CAR63.Visible = false;
                        CAR63A.Visible = false;
                    }
                }
                //NC7
                if (NC.Tables[0].Rows.Count > 6)
                {
                    NC7.Visible = true;
                    string NC7imagen1 = NC.Tables[0].Rows[6]["ImagenNODefecto"].ToString();
                    string NC7imagen2 = NC.Tables[0].Rows[6]["ImagenDefecto1"].ToString();
                    string NC7imagen3 = NC.Tables[0].Rows[6]["ImagenDefecto2"].ToString();
                    string NC7imagen4 = NC.Tables[0].Rows[6]["ImagenTrazabilidad1"].ToString();
                    string NC7imagen5 = NC.Tables[0].Rows[6]["ImagenTrazabilidad2"].ToString();

                    NC7_Cliente.Text = NC.Tables[0].Rows[6]["Cliente"].ToString();
                    NC7_TIPO.Text = NC.Tables[0].Rows[6]["NCTEXT"].ToString();
                    //NC7_ID.Text = "NC-" + NC.Tables[0].Rows[6]["IdNoConformidad"].ToString();
                    NC7_ID.Text = "NC-" + NC.Tables[0].Rows[6]["IdNoConformidad"].ToString() + "  (" + Convert.ToDateTime(NC.Tables[0].Rows[6]["FechaOriginal"]).ToString("dd/MM/yyyy") + ")";
                    NC7DefectoReclamado.Text = NC.Tables[0].Rows[6]["DescripcionProblema"].ToString();
                    NC7Referencia.Text = NC.Tables[0].Rows[6]["Referencia"].ToString() + " - " + NC.Tables[0].Rows[6]["Descripcion"].ToString();


                    if (NC7imagen1 != "")
                    {
                        NC7_IMG1.Src = NC7imagen1;
                    }
                    else
                    {
                        NC7_IMG1.Src = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";
                    }
                    if (NC7imagen2 != "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg")
                    {
                        NC7_IMG2.Src = NC7imagen2;
                    }
                    else
                    {
                        NC7_IMG2.Src = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";
                    }
                    if (NC7imagen3 != "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg")
                    {
                        NC7_IMG3.Src = NC7imagen3;
                    }
                    else
                    {
                        CAR71.Visible = false;
                        CAR71A.Visible = false;
                    }

                    if (NC7imagen4 != "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg")
                    {
                        NC7_IMG4.Src = NC7imagen4;
                    }
                    else
                    {
                        CAR72.Visible = false;
                        CAR72A.Visible = false;
                    }
                    if (NC7imagen5 != "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg")
                    {
                        NC7_IMG5.Src = NC7imagen5;
                    }
                    else
                    {
                        CAR73.Visible = false;
                        CAR73A.Visible = false;
                    }
                }
                //NC8
                if (NC.Tables[0].Rows.Count > 7)
                {
                    NC8.Visible = true;
                    string NC8imagen1 = NC.Tables[0].Rows[7]["ImagenNODefecto"].ToString();
                    string NC8imagen2 = NC.Tables[0].Rows[7]["ImagenDefecto1"].ToString();
                    string NC8imagen3 = NC.Tables[0].Rows[7]["ImagenDefecto2"].ToString();
                    string NC8imagen4 = NC.Tables[0].Rows[7]["ImagenTrazabilidad1"].ToString();
                    string NC8imagen5 = NC.Tables[0].Rows[7]["ImagenTrazabilidad2"].ToString();

                    NC8_Cliente.Text = NC.Tables[0].Rows[7]["Cliente"].ToString();
                    NC8_TIPO.Text = NC.Tables[0].Rows[7]["NCTEXT"].ToString();
                    //NC8_ID.Text = "NC-" + NC.Tables[0].Rows[7]["IdNoConformidad"].ToString();
                    NC8_ID.Text = "NC-" + NC.Tables[0].Rows[7]["IdNoConformidad"].ToString() + "  (" + Convert.ToDateTime(NC.Tables[0].Rows[7]["FechaOriginal"]).ToString("dd/MM/yyyy") + ")";
                    NC8DefectoReclamado.Text = NC.Tables[0].Rows[7]["DescripcionProblema"].ToString();
                    NC8Referencia.Text = NC.Tables[0].Rows[7]["Referencia"].ToString() + " - " + NC.Tables[0].Rows[7]["Descripcion"].ToString();


                    if (NC8imagen1 != "")
                    {
                        NC8_IMG1.Src = NC8imagen1;
                    }
                    else
                    {
                        NC8_IMG1.Src = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";
                    }
                    if (NC8imagen2 != "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg")
                    {
                        NC8_IMG2.Src = NC8imagen2;
                    }
                    else
                    {
                        NC8_IMG2.Src = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";
                    }
                    if (NC8imagen3 != "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg")
                    {
                        NC8_IMG3.Src = NC8imagen3;
                    }
                    else
                    {
                        CAR81.Visible = false;
                        CAR81A.Visible = false;
                    }

                    if (NC8imagen4 != "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg")
                    {
                        NC8_IMG4.Src = NC8imagen4;
                    }
                    else
                    {
                        CAR82.Visible = false;
                        CAR82A.Visible = false;
                    }
                    if (NC8imagen5 != "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg")
                    {
                        NC8_IMG5.Src = NC8imagen5;
                    }
                    else
                    {
                        CAR83.Visible = false;
                        CAR83A.Visible = false;
                    }
                }
                //NC9
                if (NC.Tables[0].Rows.Count > 8)
                {
                    NC9.Visible = true;
                    string NC9imagen1 = NC.Tables[0].Rows[8]["ImagenNODefecto"].ToString();
                    string NC9imagen2 = NC.Tables[0].Rows[8]["ImagenDefecto1"].ToString();
                    string NC9imagen3 = NC.Tables[0].Rows[8]["ImagenDefecto2"].ToString();
                    string NC9imagen4 = NC.Tables[0].Rows[8]["ImagenTrazabilidad1"].ToString();
                    string NC9imagen5 = NC.Tables[0].Rows[8]["ImagenTrazabilidad2"].ToString();

                    NC9_Cliente.Text = NC.Tables[0].Rows[8]["Cliente"].ToString();
                    NC9_TIPO.Text = NC.Tables[0].Rows[8]["NCTEXT"].ToString();
                    //NC9_ID.Text = "NC-" + NC.Tables[0].Rows[8]["IdNoConformidad"].ToString();
                    NC9_ID.Text = "NC-" + NC.Tables[0].Rows[8]["IdNoConformidad"].ToString() + "  (" + Convert.ToDateTime(NC.Tables[0].Rows[8]["FechaOriginal"]).ToString("dd/MM/yyyy") + ")";
                    NC9DefectoReclamado.Text = NC.Tables[0].Rows[8]["DescripcionProblema"].ToString();
                    NC9Referencia.Text = NC.Tables[0].Rows[8]["Referencia"].ToString() + " - " + NC.Tables[0].Rows[8]["Descripcion"].ToString();


                    if (NC9imagen1 != "")
                    {
                        NC9_IMG1.Src = NC9imagen1;
                    }
                    else
                    {
                        NC9_IMG1.Src = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";
                    }
                    if (NC9imagen2 != "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg")
                    {
                        NC9_IMG2.Src = NC9imagen2;
                    }
                    else
                    {
                        NC9_IMG2.Src = "../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg";
                    }
                    if (NC9imagen3 != "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg")
                    {
                        NC9_IMG3.Src = NC9imagen3;
                    }
                    else
                    {
                        CAR91.Visible = false;
                        CAR91A.Visible = false;
                    }

                    if (NC9imagen4 != "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg")
                    {
                        NC9_IMG4.Src = NC9imagen4;
                    }
                    else
                    {
                        CAR92.Visible = false;
                        CAR92A.Visible = false;
                    }
                    if (NC9imagen5 != "http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg")
                    {
                        NC9_IMG5.Src = NC9imagen5;
                    }
                    else
                    {
                        CAR93.Visible = false;
                        CAR93A.Visible = false;
                    }
                }
            }

            }
        

        
                
    }

}