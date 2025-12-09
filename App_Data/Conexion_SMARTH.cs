using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Web.Services;
using System.Data.OleDb;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;


namespace ThermoWeb
{
    public class Conexion_SMARTH
    {
        private readonly SqlConnection cnn_GP12 = new SqlConnection();//OBSOLETAR
        private readonly SqlConnection cnn_SMARTH = new SqlConnection();
        private readonly OracleConnection cnn_bms = new OracleConnection();
        private readonly SqlConnection cnn_NAV = new SqlConnection();
         private readonly OleDbConnection cnn_ARAVENHOS2 = new OleDbConnection();

        public Conexion_SMARTH()
        {
            cnn_GP12.ConnectionString = ConfigurationManager.ConnectionStrings["area_rechazo"].ToString(); //OBSOLETAR
            cnn_bms.ConnectionString = ConfigurationManager.ConnectionStrings["BMS"].ToString();
            cnn_SMARTH.ConnectionString = ConfigurationManager.ConnectionStrings["SMARTH"].ToString();
            cnn_NAV.ConnectionString = ConfigurationManager.ConnectionStrings["NAV"].ToString();
            cnn_ARAVENHOS2.ConnectionString = ConfigurationManager.ConnectionStrings["ARAVEN_HOSTELERIA2"].ToString();

        }
        //***************************************\\CALIDAD-PRODUCCION//**************************************\\
        //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\//\\
        public DataTable Devuelve_Datos_Cajas_BMS()
        {
            try
            {
                //CONSULTA A BASE DE DATOS
                cnn_bms.Open();
                string sql = "SELECT P.C_SHORT_DESCR, HP.C_JOB, HP.C_PRODUCT, HP.C_PRODUCTIONCOUNT02-HP.C_COUNTTOTAL AS BUENAS, HP.C_COUNTTOTAL AS MALAS, P.C_ACTSTARTDATE, P.C_ACTENDDATE, HP.C_CLOCKNUMBER" +
                                " FROM PCMS.T_JOB_PIECE P" +
                                " LEFT JOIN HIS.t_hispieces HP ON P.C_PIECENR = HP.C_PIECE" +
                                " WHERE P.C_STATUS = -1 AND ROWNUM <= 600" +
                                " ORDER BY C_ACTSTARTDATE DESC";
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataTable dsope = new DataTable();
                query.Fill(dsope);
                cnn_bms.Close();
                return dsope;
            }
            catch (Exception)
            {
                cnn_bms.Close();
                return null;
            }
        }


        public DataTable Devuelve_Cajas_BMS_SMARTH()
        {
            try
            {
                //Copio las cajas de BMS a SMARTH
                string sql = "SELECT P.C_PIECENR, P.C_SHORT_DESCR, HP.C_JOB, HP.C_PRODUCT, CAST(HP.C_PRODUCTIONCOUNT02-HP.C_COUNTTOTAL AS INT) AS BUENAS, CAST(HP.C_COUNTTOTAL AS INT) AS MALAS, P.C_ACTSTARTDATE, P.C_ACTENDDATE, HP.C_CLOCKNUMBER" +
                                " FROM PCMS.T_JOB_PIECE P" +
                                " LEFT JOIN HIS.t_hispieces HP ON P.C_PIECENR = HP.C_PIECE" +
                                " WHERE P.C_STATUS = -1 and C_ACTENDDATE > (sysdate-0.25)" +
                                //" WHERE P.C_STATUS = -1 and C_ACTENDDATE > (sysdate-5) and C_ACTENDDATE < (sysdate-4) and HP.C_JOB = '83096'" +
                                " ORDER BY C_ACTENDDATE DESC";
                OracleCommand cmd = new OracleCommand(sql, cnn_bms);
                cnn_bms.Open();

                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            InsertarCajasSMARTH(reader["C_PIECENR"].ToString().Trim(), reader["C_JOB"].ToString().Trim(), Convert.ToInt32(reader["C_PRODUCT"].ToString().Trim()), Convert.ToInt32(reader["BUENAS"].ToString()), Convert.ToInt32(reader["MALAS"].ToString()), reader["C_ACTSTARTDATE"].ToString().Trim(), reader["C_ACTENDDATE"].ToString().Trim(), reader["C_CLOCKNUMBER"].ToString().Trim(), reader["C_SHORT_DESCR"].ToString().Trim());
                        }
                        catch (Exception ex)
                        { }
                        }
                }
                cnn_bms.Close();
                               
                //Devuelvo las cajas pendientes de exportar de SMARTH
                cnn_SMARTH.Open();
                SqlCommand cmdSM = new SqlCommand();
                cmdSM.Connection = cnn_SMARTH;
                string sqlSM = "SELECT '' as CADENO, RTRIM(CAST([Referencia] AS varchar)) AS PRODUCTO, RTRIM(CAST([Orden] AS varchar)) AS TRABAJO, RTRIM(CAST([IdEtiqueta] AS varchar)) AS NOCAJA, RTRIM(CAST([Buenas] AS varchar)) AS PRODUCIDAS, RTRIM(CAST([Malas] AS varchar)) AS MALAS, CONVERT(VARCHAR(10), [FechaFin], 103) + ' '  + convert(VARCHAR(8), [FechaFin], 14) as FECHAFIN, '' as CADENO2, 0 AS MCDIVISOR" +
                                " FROM[SMARTH_DB].[dbo].[ERP_EXPORT_Lista_Cajas] WHERE Export = 0 order by FechaFin desc";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlSM, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_bms.Close();
                cnn_SMARTH.Close();
                return null;
            }
        }

        public bool Actualiza_Cajas_SMARTH_Exportadas()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "UPDATE [SMARTH_DB].[dbo].[ERP_EXPORT_Lista_Cajas] SET EXPORT = 1 WHERE Export = 0";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return false;
            }
        } //CALIDAD - CAMBIAR A NUEVA BASE DE DATOS DE OPERARIOS


        public bool InsertarCajasSMARTH(string IdCaja, string Orden, int referencia, int buenas, int malas, string fechainicio, string fechafin, string operario, string IdEtiqueta)
        {
            {
                try
                {
                    cnn_SMARTH.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cnn_SMARTH;
                    string sql = "";
                    sql = "INSERT INTO [SMARTH_DB].[dbo].[ERP_EXPORT_Lista_Cajas]" +
                              " ([IdCaja],[Orden],[Referencia],[Buenas],[Malas],[FechaInicio],[FechaFin],[Operario],[TipoCaja],[Export],[IdEtiqueta])" +
                              " VALUES('"+IdCaja+"', '"+Orden+"', "+referencia+", "+buenas+", "+malas+", '"+fechainicio+"', '"+fechafin+"', '"+operario+"',0,0,'"+IdEtiqueta+"')";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    cnn_SMARTH.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    cnn_SMARTH.Close();
                    return false;
                }
            }
        }



        public DataTable Devuelve_Datos_Productivos_BMS(string referencia, string molde, string fechainicio, string fechafin)
        {
            try
            {
                string queryfin = "";
                string agrupado = "C_PRODUCT";
                string where = "C_PRODUCT = '" + referencia + "'";
                
                //Criterio seleccion molde/producto
                if (molde != "")
                {
                    agrupado = "C_PRODUCTTOOL";
                    where = "C_PRODUCTTOOL = '"+molde.Trim()+"'";
                }

                //Criterio de fecha final
                if (fechafin != "")
                {
                    queryfin = " AND C_STARTTIME <= TO_DATE('" + fechafin + "', 'DD/MM/YYYY')";
                }

                //CONSULTA A BASE DE DATOS
                cnn_bms.Open();
                string sql = "SELECT 'PRODUCCION' AS APP," +
                                //" C_PRODUCT AS Referencia," +
                                " SUM(C_PRODUCTIONCOUNT02) AS ProdRev, 0 AS Retrabajo, SUM(C_COUNTTOTAL) as Malas, concat(ROUND(SUM(C_COUNTTOTAL)*MAX(C_CUSTOMVALUE2),2),'€') AS COSTE," +
                                " SUM(C_COUNT01) as Def1,SUM(C_COUNT02) as Def2,SUM(C_COUNT03) as Def3,SUM(C_COUNT04) as Def4,SUM(C_COUNT05) as Def5,SUM(C_COUNT06) as Def6,SUM(C_COUNT07) as Def7,SUM(C_COUNT08) as Def8,SUM(C_COUNT09) as Def9,SUM(C_COUNT10) as Def10," +
                                " SUM(C_COUNT11) as Def11,SUM(C_COUNT12) as Def12,SUM(C_COUNT13) as Def13,SUM(C_COUNT14) as Def14,SUM(C_COUNT15) as Def15,SUM(C_COUNT16) as Def16,SUM(C_COUNT17) as Def17,SUM(C_COUNT18) as Def18,SUM(C_COUNT19) as Def19,SUM(C_COUNT20) as Def20," +
                                " SUM(C_COUNT21) as Def21,SUM(C_COUNT22) as Def22,SUM(C_COUNT23) as Def23,SUM(C_COUNT24) as Def24,SUM(C_COUNT25) as Def25,SUM(C_COUNT26) as Def26,SUM(C_COUNT27) as Def27,SUM(C_COUNT28) as Def28,SUM(C_COUNT29) as Def29,SUM(C_COUNT30) as Def30,SUM(C_COUNT31) as Def31,SUM(C_COUNT32) as Def32,SUM(C_COUNT33) as Def33,SUM(C_COUNT34) as Def34,SUM(C_COUNT35) as Def35" +
                                " FROM HIS.T_HISSHIFTS" +
                                " WHERE "+where+" AND C_STARTTIME >= TO_DATE('"+ fechainicio + "', 'DD/MM/YYYY')"+queryfin+"" +
                                " GROUP BY "+agrupado+"";
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataTable dsope = new DataTable();
                query.Fill(dsope);
                cnn_bms.Close();
                return dsope;
            }
            catch (Exception)
            {
                cnn_bms.Close();
                return null;
            }
        }
        /*
        public DataTable Devuelve_Datos_Productivos_BMS_MES(string referencia, string fechainicio, string fechafin)
        {
            try
            {
                string queryfin = "";
                if (fechafin != "")
                {
                    queryfin = " AND DAT.C_STARTDATE <= TO_DATE('" + fechafin + "', 'DD/MM/YYYY')";
                }
                cnn_bms.Open();
                string sql = "SELECT CASE WHEN APP IS NULL THEN 'PRODUCCION' ELSE APP END AS APP,TRIM(to_char(to_date(C_STARTDATE, 'DD-MM-rrrr'), 'yyyy')) AS YEARINICIO, TRIM(to_char(EXTRACT(month FROM C_STARTDATE))) AS MONTHINICIO, to_char(to_date(C_STARTDATE, 'DD-MM-YYYY'), 'Month', 'NLS_DATE_LANGUAGE = spanish') AS FECHAINICIO,  Referencia, CASE WHEN PRODREV IS NULL THEN 0 ELSE PRODREV END AS PRODREV, CASE WHEN Retrabajo IS NULL THEN 0 ELSE Retrabajo END AS Retrabajo, CASE WHEN Malas IS NULL THEN 0 ELSE Malas END AS Malas, CASE WHEN COSTE IS NULL THEN 0.0 ELSE COSTE END AS COSTE" +
                             " FROM HIS.T_PERIODMONTHS DAT" +
                             " LEFT JOIN(SELECT 'PRODUCCION' AS APP, C_PRODUCT AS Referencia, to_char(to_date(C_STARTTIME, 'DD-MM-rrrr'), 'yyyy') AS YEA, to_char(to_date(C_STARTTIME, 'DD-MM-YYYY'), 'fmMM') AS MES, SUM(C_PRODUCTIONCOUNT02) AS ProdRev, 0 AS Retrabajo, SUM(C_COUNTTOTAL) as Malas, ROUND(SUM(C_COUNTTOTAL)*MAX(C_CUSTOMVALUE2),2) AS COSTE" +
                                                             " FROM HIS.T_HISSHIFTS" +
                                                             " WHERE C_PRODUCT = '"+ referencia + "'" +
                                                             " GROUP BY C_PRODUCT, to_char(to_date(C_STARTTIME, 'DD-MM-rrrr'), 'yyyy'), to_char(to_date(C_STARTTIME, 'DD-MM-YYYY'), 'fmMM')"+
                            " )COD ON to_char(to_date(DAT.C_STARTDATE, 'DD-MM-rrrr'), 'yyyy') = COD.YEA AND to_char(to_date(DAT.C_STARTDATE, 'DD-MM-YYYY'), 'fmMM') = COD.MES" +
                            " WHERE DAT.C_STARTDATE >= TO_DATE('" + fechainicio + "', 'DD/MM/YYYY')"+queryfin+"" +
                            " ORDER BY to_char(to_date(DAT.C_STARTDATE, 'DD-MM-rrrr'), 'yyyy'), to_number(to_char(to_date(DAT.C_STARTDATE, 'DD-MM-YYYY'), 'fmMM'))";
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataTable dsope = new DataTable();
                query.Fill(dsope);
                cnn_bms.Close();
                return dsope;
            }
            catch (Exception EX)
            {
                cnn_bms.Close();
                return null;
            }
        }
        */
        public DataTable Devuelve_Datos_Productivos_BMS_MES(string referencia, string molde, string fechainicio, string fechafin)
        {
            try
            {
                string queryfin = "";
                string queryfin2 = "";
                string agrupado = "C_PRODUCT";
                string where = "C_PRODUCT = '" + referencia + "'";
                string auxiliar = "C_PRODUCT AS Referencia";
                string auxiliar2 = "Referencia";
                //Criterio seleccion molde/producto
                if (molde != "")
                {
                    agrupado = "C_PRODUCTTOOL";
                    where = "C_PRODUCTTOOL = '" + molde.Trim() + "'";
                    auxiliar = "C_PRODUCTTOOL as C_PRODUCTTOOL";
                    auxiliar2 = "C_PRODUCTTOOL";

                }

                //Criterio de fecha final
              
                if (fechafin != "")
                {
                    queryfin = " AND DAT.FECHA <= TO_DATE('" + fechafin + "', 'DD/MM/YYYY')";
                    queryfin2 = "  AND C_STARTTIME <= TO_DATE('" + fechafin + "', 'DD/MM/YYYY')";
                }

                cnn_bms.Open();
                string sql = "SELECT CASE WHEN APP IS NULL THEN 'PRODUCCION' ELSE APP END AS APP,TRIM(to_char(to_date(FECHA, 'DD-MM-rrrr'), 'yyyy')) AS YEARINICIO, TRIM(to_char(EXTRACT(month FROM FECHA))) AS MONTHINICIO, TRIM(to_char(to_date(FECHA, 'DD-MM-YYYY'), 'Month', 'NLS_DATE_LANGUAGE = spanish'))  || ' *' || substr(to_char(to_date(FECHA, 'DD-MM-rrrr'), 'yyyy'),-2) AS FECHAINICIO,  "+auxiliar2+", CASE WHEN PRODREV IS NULL THEN 0 ELSE PRODREV END AS PRODREV, CASE WHEN Retrabajo IS NULL THEN 0 ELSE Retrabajo END AS Retrabajo, CASE WHEN Malas IS NULL THEN 0 ELSE Malas END AS Malas, CASE WHEN COSTE IS NULL THEN 0.0 ELSE COSTE END AS COSTE" +
                                    " FROM(SELECT to_date('01-' || to_char(to_date(C_STARTTIME, 'DD-MM-YYYY'), 'MM') || '-' || to_char(to_date(C_STARTTIME, 'DD-MM-rrrr'), 'yyyy'), 'dd/MM/yyyy') AS FECHA"+
                                            " FROM HIS.T_HISSHIFTS" +
                                            " GROUP BY to_date('01-' || to_char(to_date(C_STARTTIME, 'DD-MM-YYYY'), 'MM') || '-' || to_char(to_date(C_STARTTIME, 'DD-MM-rrrr'), 'yyyy'), 'dd/MM/yyyy')) DAT" +
                                    " LEFT JOIN(SELECT 'PRODUCCION' AS APP, "+auxiliar+", to_char(to_date(C_STARTTIME, 'DD-MM-rrrr'), 'yyyy') AS YEA, to_char(to_date(C_STARTTIME, 'DD-MM-YYYY'), 'fmMM') AS MES, SUM(C_PRODUCTIONCOUNT02) AS ProdRev, 0 AS Retrabajo, SUM(C_COUNTTOTAL) as Malas, ROUND(SUM(C_COUNTTOTAL) * MAX(C_CUSTOMVALUE2), 2) AS COSTE" +
                                                " FROM HIS.T_HISSHIFTS" +
                                                " WHERE "+where+ " AND C_STARTTIME >= TO_DATE('"+fechainicio+"', 'DD/MM/YYYY')"+ queryfin2 + "" +
                                                " GROUP BY "+agrupado+", to_char(to_date(C_STARTTIME, 'DD-MM-rrrr'), 'yyyy'), to_char(to_date(C_STARTTIME, 'DD-MM-YYYY'), 'fmMM')" +
                                                " )COD ON to_char(to_date(DAT.FECHA, 'DD-MM-rrrr'), 'yyyy') = COD.YEA AND to_char(to_date(DAT.FECHA, 'DD-MM-YYYY'), 'fmMM') = COD.MES" +
                                    " WHERE DAT.FECHA >= TO_DATE('"+fechainicio+"', 'DD/MM/YYYY')"+queryfin+"" +
                                    " ORDER BY to_char(to_date(DAT.FECHA, 'DD-MM-rrrr'), 'yyyy'), to_number(to_char(to_date(DAT.FECHA, 'DD-MM-YYYY'), 'fmMM'))";
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataTable dsope = new DataTable();
                query.Fill(dsope);
                cnn_bms.Close();
                return dsope;
            }
            catch (Exception EX)
            {
                cnn_bms.Close();
                return null;
            }
        }

        public DataTable Devuelve_Datos_Productivos_SMARTH(string referencia, string molde, string fechainicio, string fechafin)
        {
            try
            {
                string queryfin = "";
                string agrupado = "GP.[Referencia]";
                string where = "GP.Referencia = '" + referencia + "'";

                //Criterio seleccion molde/producto
                if (molde != "")
                {
                    agrupado = "PR.Molde";
                    where = "PR.Molde = '" + molde.Trim() + "'";
                }

                //Criterio de fecha final
                if (fechafin != "")
                {
                    queryfin = "  AND FechaInicio <= '" + fechafin + "'";
                }
                

                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT 'GP12' AS APP," +
                                //" [Referencia]," +
                                " SUM([PiezasRevisadas]) AS ProdRev, SUM([Retrabajadas]) AS Retrabajo, SUM([PiezasNOK]) AS Malas, CAST(ROUND(SUM([CosteRevision]),2) AS varchar) + '€' AS COSTE," +
                                " SUM([Def1]) as Def1,SUM([Def2]) as Def2,SUM([Def3]) as Def3,0 as Def4, SUM([Def4]) as Def5,SUM([Def5]) as Def6,SUM([Def6]) as Def7,SUM([Def7]) as Def8,SUM([Def8]) as Def9,0 as Def10, SUM([Def9]) as Def11,SUM([Def10]) as Def12" +
                                ",SUM([Def11]) as Def13,SUM([Def12]) as Def14,0 as Def15, 0 as Def16, SUM([Def13]) as Def17,SUM([Def14]) as Def18, 0 as Def19, SUM([Def15]) as Def20,SUM([Def16]) as Def21,SUM([Def17]) as Def22,SUM([Def18]) as Def23,SUM([Def19]) as Def24" +
                                ",SUM([Def25]) as Def25,SUM([Def26]) as Def26,SUM([Def27]) as Def27,SUM([Def28]) as Def28,SUM([Def29]) as Def29,SUM([Def30]) as Def30" +
                                ",0 as [Def31],0 as [Def32],0 as [Def33],0 as [Def34],0 as [Def35]" +
                                " FROM[SMARTH_DB].[dbo].[GP12_Historico_2021] GP" +
                                " left join[SMARTH_DB].[dbo].[AUX_TablaProductos] PR ON PR.Referencia = GP.Referencia"+
                                " WHERE "+where+" AND FechaInicio >= '" + fechainicio + "'"+queryfin+"" +
                                " GROUP BY "+agrupado+"";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_Datos_Productivos_SMARTH_MES(string referencia, string molde, string fechainicio, string fechafin)
        {
            try
            {
                //Criterio de fecha final
                string queryfin = "";
                string agrupado = "GP.[Referencia]";
                string where = "GP.Referencia = '" + referencia + "'";

                if (fechafin != "")
                {
                    queryfin = "  AND cast('01/' + CAST(Mes AS varchar) + '/' + CAST(DAT.Periodo AS varchar) AS datetime) <= '" + fechafin + "'";
                }
                
                //Criterio seleccion molde/producto
               
                if (molde != "")
                {
                    agrupado = "PR.Molde";
                    where = "PR.Molde = '" + molde.Trim() + "'";
                }
                
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT CASE WHEN DOC.APP IS NULL THEN 'GP12' ELSE DOC.APP END AS APP,RTRIM(DAT.Periodo) as YEARINICIO,RTRIM(DAT.Mes) as MONTHINICIO,CAST(RTRIM(datename(month,cast('01/' + CAST(Mes AS varchar) + '/' + CAST(DAT.Periodo AS varchar) AS datetime))) AS varchar) + ' *' + CAST(RIGHT(DAT.PERIODO,2) AS varchar) AS FECHAINICIO, CASE WHEN DOC.ProdRev IS NULL THEN 0 ELSE DOC.ProdRev END AS ProdRev," +
                                " CASE WHEN DOC.Retrabajo IS NULL THEN 0 ELSE DOC.Retrabajo END AS Retrabajo," +
                                " CAST(CASE WHEN DOC.Malas IS NULL THEN 0 ELSE DOC.Malas END as int) AS Malas," +
                                " CAST(CASE WHEN DOC.COSTEHORAS IS NULL THEN 0.0 ELSE ROUND(DOC.COSTEHORAS,2) END as DECIMAL) AS COSTEHORAS," +
                                " CAST(CASE WHEN DOC.COSTESCRAP IS NULL THEN 0.0 ELSE ROUND(DOC.COSTESCRAP,2) END as DECIMAL) AS COSTESCRAP" +
                                " FROM[SMARTH_DB].[dbo].[AUX_BMSHistoricoProducidas] DAT" +
                                " LEFT JOIN(SELECT 'GP12' AS APP, month([FechaInicio]) as MESNUM,YEAR([FechaInicio]) as PERIODO,datename(month, [FechaInicio]) as FECHAINICIO, SUM([PiezasRevisadas]) AS ProdRev, SUM([Retrabajadas]) AS Retrabajo, SUM([PiezasNOK]) AS Malas, ROUND(SUM([CostePiezaRevision]),2) AS COSTEHORAS, ROUND(SUM([CosteScrapRevision]),2) AS COSTESCRAP" +
                                " FROM[SMARTH_DB].[dbo].[GP12_Historico_2021] GP" +
                                " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] PR ON PR.Referencia = GP.Referencia"+
                                " WHERE "+where+"" +
                                " GROUP BY "+agrupado+", MONTH([FechaInicio]), datename(month, [FechaInicio]), YEAR([FechaInicio])) DOC ON DAT.Periodo = DOC.PERIODO AND DAT.Mes = DOC.MESNUM"+
                                " WHERE cast('01/' + CAST(Mes AS varchar) + '/' + CAST(DAT.Periodo AS varchar) AS datetime) >= '" + fechainicio + "'"+queryfin+"" +
                                " ORDER BY DAT.PERIODO,DAT.Mes";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }
        /*
        public DataTable Devuelve_Datos_NC_SMARTH_MES(string referencia, string fechainicio, string fechafin)
        {
            try
            {
                string queryfin = "";
                if (fechafin != "")
                {
                    queryfin = "  AND cast('01/' + CAST(Mes AS varchar) + '/' + CAST(DAT.Periodo AS varchar) AS datetime) <= '" + fechafin + "'";
                }
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = " SELECT CASE WHEN APP IS NULL THEN 'NO CONFORMIDADES' ELSE APP END AS APP, RTRIM(DAT.PERIODO) AS YEARINICIO, RTRIM(DAT.MES) AS MONTHINICIO,CAST(RTRIM(datename(month, cast('01/' + CAST(Mes AS varchar) + '/' + CAST(DAT.Periodo AS varchar) AS datetime))) AS varchar) AS FECHAINICIO," +
                                    " CAST(CASE WHEN ADMINISTRATIVOS IS NULL THEN 0.0 ELSE ADMINISTRATIVOS END AS DECIMAL) AS ADMINISTRATIVOS," +
                                    " CAST(CASE WHEN CARGOS IS NULL THEN 0.0 ELSE CARGOS END AS DECIMAL) AS CARGOS," +
                                    " CAST(CASE WHEN PIEZAS IS NULL THEN 0.0 ELSE PIEZAS END AS DECIMAL) AS RECHAZO," +
                                    " CAST(CASE WHEN SELECCION_EXT IS NULL THEN 0.0 ELSE SELECCION_EXT END AS DECIMAL) AS SELECCION_EXT," +
                                    " CAST(CASE WHEN SELECCION_INT IS NULL THEN 0.0 ELSE SELECCION_INT END AS DECIMAL) AS SELECCION_INT," +
                                    " CAST(CASE WHEN OTROS IS NULL THEN 0.0 ELSE OTROS END AS DECIMAL) AS OTROS," +

                                    " FROM[SMARTH_DB].[dbo].[AUX_BMSHistoricoProducidas] DAT" +
                                    " LEFT JOIN(SELECT 'NO CONFORMIDADES' AS APP, YEAR(NC.FechaOriginal) AS YEARINICIO, MONTH(NC.FechaOriginal) AS MONTHINICIO, datename(month, NC.FechaOriginal) AS FECHAINICIO,[Referencia] AS REFERENCIA, SUM(EST.CosteAdmonEXT) AS ADMINISTRATIVOS" +
                                                " ,SUM(EST.CosteCargosEXT) AS CARGOS, SUM(EST.CostePiezasNOKEXT) AS PIEZAS, SUM(EST.CosteSeleccionEXT) AS SELECCION_EXT, SUM(EST.CosteSeleccionINT) AS SELECCION_INT, SUM(EST.CosteOtrosINT) AS OTROS" +
                                                " FROM[SMARTH_DB].[dbo].[NC_Alerta_de_calidad] NC" +
                                                " LEFT JOIN[SMARTH_DB].[dbo].[NC_Estado_No_Conformidad] EST ON NC.IdNoConformidad = EST.IdNoConformidad" +
                                                " WHERE REFERENCIA = '"+referencia+ "' AND NC.FechaOriginal >= '" + fechainicio + "'" +
                                                 " GROUP BY Referencia, MONTH(NC.FechaOriginal), datename(month, NC.FechaOriginal), YEAR(NC.FechaOriginal)) DOC ON DAT.Periodo = DOC.YEARINICIO AND DAT.Mes = DOC.MONTHINICIO" +
                                    " WHERE cast('01/' + CAST(Mes AS varchar) + '/' + CAST(DAT.Periodo AS varchar) AS datetime) >= '"+fechainicio+"'"+queryfin+"";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }
        */
        public DataTable Devuelve_Datos_NC_SMARTH(string referencia, string molde, string fechainicio, string fechafin)
        {
            try
            {
                string queryfin = "";
                string agrupado = "CAL.[Referencia]";
                string where = "CAL.Referencia = '" + referencia + "'";
                //Criterio seleccion molde/producto

                if (molde != "")
                {
                    agrupado = "PR.Molde";
                    where = "PR.Molde = '" + molde.Trim() + "'";
                    
                }
                //Criterio seleccion fecha
                if (fechafin != "")
                {
                    queryfin = "  AND CAL.FechaOriginal <= '" + fechafin + "'";
                }

                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT 'APP' AS APP,"+agrupado+" AS PRODUCTO,COUNT(CAL.[IdNoConformidad]) AS RECLAMACIONES, SUM([CosteAdmonEXT]) AS ADMINISTRATIVOS, SUM([CosteCargosEXT]) AS CARGOS, SUM([CostePiezasNOKEXT]) AS RECHAZO, SUM([CosteSeleccionEXT]) AS SELECCION_EXT, SUM([CosteSeleccionINT]) AS SELECCION_INT, SUM([CosteOtrosINT]) AS OTROS"+
                              " FROM[SMARTH_DB].[dbo].[NC_Alerta_de_calidad]CAL" +
                              " LEFT JOIN[SMARTH_DB].[dbo].[NC_Estado_No_Conformidad] EST ON CAL.IdNoConformidad = EST.IdNoConformidad" +
                              " LEFT JOIN[SMARTH_DB].[dbo].[AUX_TablaProductos] PR ON PR.Referencia = CAL.Referencia"+
                              " WHERE "+where+" AND CAL.FechaOriginal >= '"+fechainicio+"'"+queryfin+"" +
                              " GROUP BY "+agrupado+"";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }
        public DataTable Devuelve_Datos_NC_SMARTH_MES(string referencia, string molde, string fechainicio, string fechafin)
        {
            try
            {
                string queryfin = "";
                string agrupado = "NC.REFERENCIA";
                string where = "NC.REFERENCIA = '" + referencia + "'";
                string auxiliar = "NC.[Referencia] AS REFERENCIA";
                //Criterio seleccion molde/producto

                if (molde != "")
                {
                    agrupado = "PR.Molde";
                    where = "PR.Molde = '" + molde.Trim() + "'";
                    auxiliar = "PR.[Molde] AS Molde";
                }
                //Criterio seleccion fecha
                
                if (fechafin != "")
                {
                    queryfin = "  AND cast('01/' + CAST(Mes AS varchar) + '/' + CAST(DAT.Periodo AS varchar) AS datetime) <= '" + fechafin + "'";
                }
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = " SELECT CASE WHEN APP IS NULL THEN 'NO CONFORMIDADES' ELSE APP END AS APP, RTRIM(DAT.PERIODO) AS YEARINICIO, RTRIM(DAT.MES) AS MONTHINICIO,CAST(RTRIM(datename(month, cast('01/' + CAST(Mes AS varchar) + '/' + CAST(DAT.Periodo AS varchar) AS datetime))) AS varchar) + ' *' + CAST(RIGHT(DAT.PERIODO,2) AS varchar) AS FECHAINICIO," +
                                    " CAST(CASE WHEN COSTENC IS NULL THEN 0 ELSE COSTENC END AS DECIMAL) AS COSTENC"+
                                    " FROM[SMARTH_DB].[dbo].[AUX_BMSHistoricoProducidas] DAT" +
                                    " LEFT JOIN(SELECT 'NO CONFORMIDADES' AS APP, YEAR(NC.FechaOriginal) AS YEARINICIO, MONTH(NC.FechaOriginal) AS MONTHINICIO, datename(month, NC.FechaOriginal) AS FECHAINICIO,"+auxiliar+"," +
                                    " SUM(EST.CosteAdmonEXT) + SUM(EST.CosteCargosEXT) + SUM(EST.CostePiezasNOKEXT) + SUM(EST.CosteSeleccionEXT) + SUM(EST.CosteSeleccionINT)+ SUM(EST.CosteOtrosINT) AS COSTENC" +
                                                " FROM[SMARTH_DB].[dbo].[NC_Alerta_de_calidad] NC" +
                                                " LEFT JOIN[SMARTH_DB].[dbo].[NC_Estado_No_Conformidad] EST ON NC.IdNoConformidad = EST.IdNoConformidad" +
                                                " LEFT JOIN[SMARTH_DB].[dbo].[AUX_TablaProductos] PR ON PR.Referencia = NC.Referencia"+
                                                " WHERE "+where+" AND NC.FechaOriginal >= '" + fechainicio + "'" +
                                                 " GROUP BY "+agrupado+", MONTH(NC.FechaOriginal), datename(month, NC.FechaOriginal), YEAR(NC.FechaOriginal)) DOC ON DAT.Periodo = DOC.YEARINICIO AND DAT.Mes = DOC.MONTHINICIO" +
                                    " WHERE cast('01/' + CAST(Mes AS varchar) + '/' + CAST(DAT.Periodo AS varchar) AS datetime) >= '" + fechainicio + "'" + queryfin + "";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_Datos_Producto_Seleccionado(string Referencia, string molde)
        {
            try
            {
                string where = "Referencia = " + Referencia + "";
                if (molde != "")
                {
                    where = "Molde = '" + molde.Trim() + "'";
                }

                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM [SMARTH_DB].[dbo].[AUX_TablaProductos] where "+where+"", cnn_SMARTH);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                cnn_SMARTH.Close();
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                return dt;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_AUX_TablaDefectosProduccion()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT * FROM[SMARTH_DB].[dbo].[AUX_TablaDefectos] order by IdDefecto asc";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_AUX_ETISFERNANDO()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT  [Etiquetas_necesarias],[Producto], MAT.Descripcion,[Cantidad_Por_caja],[Lote],[Lote_de_Cliente],[NumProveedor],[NumPedido]"+
                                         " FROM[SMARTH_DB].[dbo].[LOTES ANTIGUOS] LOT"+
                                         " left join SMARTH_DB.dbo.AUX_Lista_Materiales MAT ON LOT.Producto = MAT.Referencia"+
                                            " WHERE Etiquetas_necesarias IS NOT NULL";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        //***************************************\\IOT//**************************************\\
        //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\//\\
        public DataTable Devuelve_Estado_Maquina_BMS()
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT TRIM(J.C_MACHINE_ID) AS C_MACHINE_ID, J.C_ID, J.C_PARENT_ID, J.C_PARINDEX, J.C_TOOL_ID, J.C_PRODUCT_ID, J.C_ACTSTARTDATE, S.ESTADO2 FROM PCMS.T_JOBS J" +
                           " LEFT JOIN(select C_MACHINE_ID AS MAQUINA, CASE CAST(E.C_FINAL_RUNSTATE AS VARCHAR2(30)) WHEN '1' THEN 'En marcha' ELSE CAST(S.C_SHORT_DESCR AS VARCHAR2(30)) END AS ESTADO2, S.C_STOPGROUPS_ID from ELOG2.T_ELOG_STOPINFO E LEFT JOIN PCMS.T_STPTABLE_STPCODE S ON E.C_FINAL_STOPCODE = S.C_STOPCODE_NR AND C_STOPTABLE_ID = 2" +
                           " where C_NEXT_ID is null order by c_machine_id asc) S ON J.C_MACHINE_ID = S.MAQUINA WHERE C_SEQNR = 0 ORDER BY C_MACHINE_ID, C_PARINDEX";

                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataTable dsope = new DataTable();
                query.Fill(dsope);
                cnn_bms.Close();
                return dsope;
            }
            catch (Exception)
            {
                cnn_bms.Close();
                return null;
            }
        }

      
        public DataTable Devuelve_Lecturas_Refrigeracion()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT TOP(1000)* FROM[SMARTH_DB].[dbo].[IOT_Refrigeracion] where FechaMedida > DATEADD(day, -2, SYSDATETIME()) order by FechaMedida desc";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public string Devuelve_Lecturas_Refrigeracion_Graficas(string inicio, string final)
        {
            try
            {
                using (SqlConnection con = cnn_SMARTH)
                {
                    string query = string.Format("SELECT TOP(10) * FROM[SMARTH_DB].[dbo].[IOT_Refrigeracion]"+
                                                 " where HIDTempEntrada is not null and MOLTempEntrada is not null and FechaMedida between '"+inicio+"' and '"+final+"'");
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        con.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            //STRING LABELS
                            StringBuilder sbLabels = new StringBuilder();
                            sbLabels.Append("labels: [");

                            //STRING PRODUCIDAS
                            StringBuilder sbTempEnt = new StringBuilder();
                            //Linea 1
                            sbTempEnt.Append("datasets: [");
                            sbTempEnt.Append("{label: \u0022% Molde Temp.Entrada\u0022,");
                            sbTempEnt.Append("backgroundColor: [\u0022#6A0DAD\u0022],");
                            sbTempEnt.Append("borderColor: [\u0022#6A0DAD\u0022],");
                            sbTempEnt.Append("borderWidth: 3, ");
                            sbTempEnt.Append("data: [");

                            //Linea 2
                            StringBuilder sbTempSal = new StringBuilder();
                            sbTempSal.Append("{label: \u0022% Molde Temp.Salida\u0022,");
                            sbTempSal.Append("backgroundColor: [\u0022#0000FF\u0022],");
                            sbTempSal.Append("borderColor: [\u0022#0000FF\u0022],");
                            sbTempSal.Append("borderWidth: 3, ");
                            sbTempSal.Append("data: [");

                            string quote = "\u0022";
                            while (sdr.Read())
                            {
                                System.Threading.Thread.Sleep(50);
                                sbLabels.Append(string.Format("{0},", quote + sdr[1].ToString().Replace(":",".") + quote)); //FECHAS
                                sbTempEnt.Append(string.Format("{0},", sdr[3].ToString().Replace(',', '.'))); //HIDENTRADA
                                sbTempSal.Append(string.Format("{0},", sdr[4].ToString().Replace(',', '.'))); //HIDSALIDA

                            }

                            //Correcciones de final de linea
                            sbLabels = sbLabels.Remove(sbLabels.Length - 1, 1);
                            sbLabels.Append("],");


                            sbTempEnt = sbTempEnt.Remove(sbTempEnt.Length - 1, 1);
                            sbTempEnt.Append("]},");

                            sbTempSal = sbTempSal.Remove(sbTempSal.Length - 1, 1);
                            sbTempSal.Append("]}]");

                            sbLabels.Append(sbTempEnt);
                            sbLabels.Append(sbTempSal);

                            return sbLabels.ToString();

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return "";
            }

        }

        public DataTable TESTARAVEN(string referencia)
        {
            try
            {
                string query = "select * FROM [Datos_HOS] WHERE item = '01172' order by item desc";
                //OleDbConnection con = new OleDbConnection(cnn_ARAVENHOS2);
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cnn_ARAVENHOS2;
                cnn_ARAVENHOS2.Open();
                OleDbDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                cnn_ARAVENHOS2.Close();
                return dt;

                /*
                cnn_ARAVENHOS.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_ARAVENHOS;
                string sql = "SELECT *" +
                             " FROM [Datos_HOS]";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_ARAVENHOS);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_ARAVENHOS.Close();
                return dt;
                */
            }
            catch (Exception ex)
            {
                cnn_ARAVENHOS2.Close();
                return null;
            }
        }


        //***************************************\\SINCRONIZACIÓN DE BASES DE DATOS//**************************************\\
        //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\//\\
        public bool Insertar_absentismo(string NUMOPERARIO, string NOMBREOPERARIO, string Fecha, int MINTEORICO, int SININCIDENCIA, int ENFERMEDADLARGA, int ACCIDENTELAB, int BAJALAB, int ENFERMEDAD, int LACTANCIA30MIN, int LACTANCIA60MIN, int VISITAMEDICO, int ENFERMEDADFAMILIAR, int EXAMENES, int DEBERINEXCUSABLE, int NACIMIENTOHIJO, 
                                        int MATRIMONIOS, int MATERNIDAD, int LICENCIASVARIAS, int PATERNIDAD, int LACTANCIACOMPACTACION, int FALLECIMIENTOFAM, int CAMBIODOMICILIO, int LICENCIABODA, int PERMISORETRIBUIDO, int PERMISONORETRIBUIDO, int RETRASOAPROBADO, int HORASSINDICALES)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[KPI_Absentismo] ([NUMOPERARIO],[NOMBREOPERARIO],[Fecha],[MINTEORICO],[SININCIDENCIA],[ENFERMEDADLARGA],[ACCIDENTELAB],[BAJALAB],[ENFERMEDAD],[LACTANCIA30MIN],[LACTANCIA60MIN],[VISITAMEDICO],[ENFERMEDADFAMILIAR],[EXAMENES]," +
                                                    " [DEBERINEXCUSABLE],[NACIMIENTOHIJO],[MATRIMONIOS],[MATERNIDAD],[LICENCIASVARIAS],[PATERNIDAD],[LACTANCIACOMPACTACION],[FALLECIMIENTOFAM],[CAMBIODOMICILIO],[LICENCIABODA],[PERMISORETRIBUIDO],[PERMISONORETRIBUIDO],[RETRASOAPROBADO],[HORASSINDICALES])  " +
                                                    " VALUES ( '"+ NUMOPERARIO +"', '" + NOMBREOPERARIO + "','"+ Fecha + "', "+ MINTEORICO +", "+SININCIDENCIA + ", "+ ENFERMEDADLARGA +","+ ACCIDENTELAB +", "+ BAJALAB +","+ ENFERMEDAD +","+LACTANCIA30MIN+","+LACTANCIA60MIN+","+ VISITAMEDICO+","+ENFERMEDADFAMILIAR+""+
                                                    "," +EXAMENES+","+ DEBERINEXCUSABLE+","+ NACIMIENTOHIJO+","+MATRIMONIOS+","+MATERNIDAD+","+LICENCIASVARIAS+","+PATERNIDAD+","+LACTANCIACOMPACTACION+","+FALLECIMIENTOFAM+","+CAMBIODOMICILIO+","+LICENCIABODA+","+PERMISORETRIBUIDO+","+PERMISONORETRIBUIDO+","+RETRASOAPROBADO+","+HORASSINDICALES+")";               
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception ex)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cnn_SMARTH;
                    string sql2 = "UPDATE [SMARTH_DB].[dbo].[KPI_Absentismo]"+
                                   "SET [NUMOPERARIO]='"+ NUMOPERARIO + "',[NOMBREOPERARIO]='"+ NOMBREOPERARIO + "',[Fecha]='"+ Fecha + "',[MINTEORICO]="+ MINTEORICO + ",[SININCIDENCIA]="+ SININCIDENCIA + ",[ENFERMEDADLARGA]="+ ENFERMEDADLARGA + ",[ACCIDENTELAB]="+ ACCIDENTELAB + ",[BAJALAB]="+ BAJALAB + ",[ENFERMEDAD]="+ ENFERMEDAD + ",[LACTANCIA30MIN]="+ LACTANCIA30MIN + ",[LACTANCIA60MIN]="+ LACTANCIA60MIN + ",[VISITAMEDICO]="+ VISITAMEDICO + ",[ENFERMEDADFAMILIAR]="+ ENFERMEDADFAMILIAR + ",[EXAMENES]="+ EXAMENES + "," +
                                   " [DEBERINEXCUSABLE]="+ DEBERINEXCUSABLE + ",[NACIMIENTOHIJO]="+ NACIMIENTOHIJO + ",[MATRIMONIOS]="+ MATRIMONIOS + ",[MATERNIDAD]="+ MATERNIDAD + ",[LICENCIASVARIAS]="+ LICENCIASVARIAS + ",[PATERNIDAD]="+ PATERNIDAD + ",[LACTANCIACOMPACTACION]="+ LACTANCIACOMPACTACION + ",[FALLECIMIENTOFAM]="+ FALLECIMIENTOFAM + ",[CAMBIODOMICILIO]="+ CAMBIODOMICILIO + ",[LICENCIABODA]="+ LICENCIABODA + ",[PERMISORETRIBUIDO]="+ PERMISORETRIBUIDO + ",[PERMISONORETRIBUIDO]="+ PERMISONORETRIBUIDO + ",[RETRASOAPROBADO]="+ RETRASOAPROBADO + ",[HORASSINDICALES]="+ HORASSINDICALES + "" +
                                   " WHERE [NUMOPERARIO] = '" + NUMOPERARIO + "' AND [Fecha]='" + Fecha + "'";
                    cmd.CommandText = sql2;
                    cmd.ExecuteNonQuery();
                    cnn_SMARTH.Close();
                }
                catch (Exception ex2)
                {
                    cnn_SMARTH.Close();
                    return false;
                }
                cnn_SMARTH.Close();
                return false;
            }
        } //CALIDAD - CAMBIAR A NUEVA BASE DE DATOS DE OPERARIOS


        public bool Insertar_OEE(int AÑO, int MES, string OEE, string OEECal)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[KPI_Panel_General_Sandbox] ([Año],[Mes],[OEEPlanta],[OEECalidad])  " +
                                                    " VALUES ( " + AÑO + ", " + MES + "," + OEE.ToString().Replace(",",".") + ", " + OEECal.ToString().Replace(",", ".") + ")";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception ex)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cnn_SMARTH;
                    string sql2 = "UPDATE [SMARTH_DB].[dbo].[KPI_Panel_General_Sandbox]" +
                                   "SET [OEEPlanta]=" + OEE.ToString().Replace(",", ".") + ",[OEECalidad]=" + OEECal.ToString().Replace(",", ".") + ""+
                                   " WHERE [Año] = " + AÑO + " AND [Mes]=" + MES + "";
                    cmd.CommandText = sql2;
                    cmd.ExecuteNonQuery();
                    cnn_SMARTH.Close();
                }
                catch (Exception ex2)
                {
                    cnn_SMARTH.Close();
                    return false;
                }
                cnn_SMARTH.Close();
                return false;
            }
        } //CALIDAD - CAMBIAR A NUEVA BASE DE DATOS DE OPERARIOS


        public bool Leer_operariosNAV() //CALIDAD - CAMBIAR A NUEVA BASE DE DATOS PRODUCTOS
        {

            try
            {
                //string sql = "SELECT '' AS ID, C_LONG_DESCR AS DESCRIPCION, TRIM(C_ID) AS CODIGO, TRIM(C_ID) AS REFERENCIAMOLDE, TRIM(C_ORIGINALCAVITIES) AS CAVIDADES, C_CHARACTERISTICS AS UBICACION FROM PCMS.T_TOOLS WHERE C_ID LIKE '3333%' or C_ID LIKE '9999%'";
                cnn_SMARTH.Open();
                string sql1 = "UPDATE [SMARTH_DB].[dbo].[AUX_TablaOperarios] SET OpActivo = 1 WHERE LegacyOP IS NULL ";
                SqlCommand cmd = new SqlCommand(sql1, cnn_SMARTH);
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                string sql = "SELECT CAST(CAST (No_ as INT) AS VARCHAR) AS C_ID,[Search Name] AS C_NAME, case WHEN Status = 0 THEN 1 ELSE 0 END AS ACTIVO, [E-Mail] as EMAIL, [Job Title] as CARGO FROM[NAVDB].[dbo].[THERMO$Employee] where [Job Title] <> 'Administración' and No_<> 999 order by[C_ID]";
                SqlCommand cmd2 = new SqlCommand(sql, cnn_NAV);
                cnn_NAV.Open();

                using (SqlDataReader reader = cmd2.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Insertar_operario(Convert.ToInt32(reader["C_ID"]), reader["C_NAME"].ToString(), reader["ACTIVO"].ToString(), reader["EMAIL"].ToString(), reader["CARGO"].ToString());

                    }
                }
                cnn_NAV.Close();
                return true;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                cnn_NAV.Close();
                return false;
            }
        }

        public bool Insertar_operario(int numoperario, string operario, string estado, string email, string cargo)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "";
                if (estado == "0")
                {
                    sql = "UPDATE [SMARTH_DB].[dbo].[AUX_TablaOperarios] SET OpActivo = 0, [Mail] = '"+email+"' WHERE Id = " + numoperario + "";
                }
                else
                {
                    sql = "INSERT INTO [SMARTH_DB].[dbo].[AUX_TablaOperarios] (Id, Operario, OpActivo, [Mail], TipoOperario) VALUES " +
                                 "(" + numoperario + ",'" + operario + "', 1,'" + email + "','" + cargo + "' )";
                }
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception ex)
            {
                try 
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cnn_SMARTH;
                    string sql2 = "UPDATE [SMARTH_DB].[dbo].[AUX_TablaOperarios] SET [Mail] = '" + email + "' WHERE Id = " + numoperario + "";
                    cmd.CommandText = sql2;
                    cmd.ExecuteNonQuery();
                    cnn_SMARTH.Close();
                }
                catch (Exception ex2)
                {
                    cnn_SMARTH.Close();
                    return false;
                }
                cnn_SMARTH.Close();
                return false;
            }
        } //CALIDAD - CAMBIAR A NUEVA BASE DE DATOS DE OPERARIOS

        public bool Leer_MATERIALES_NAVV0() //CALIDAD - CAMBIAR A NUEVA BASE DE DATOS PRODUCTOS
        {

            try
            {

                string sql = "SELECT Cast(I.[No_] as int) as Referencia, [Description] AS Descripcion, 1 as Secado, R.[NEO Drying temperature] as SecadoTemperatura, R.[NEO Drying time] as SecadoTiempo, R.[NEO Shelf location] as Ubicacion FROM[NAVDB].[dbo].[THERMO$Item] I LEFT JOIN[NAVDB].[dbo].[THERMO$Item$e8af12d5-328b-47d8-b2d8-158ecbed1ec3] R ON I.No_ = R.No_ WHERE I.[No_] LIKE '2%' AND([Item Category Code] = 15 OR[Item Category Code] = 155  OR[Item Category Code] = 221  OR[Item Category Code] = 215) ORDER BY[Item Category Code] asc";
                SqlCommand cmd2 = new SqlCommand(sql, cnn_NAV);
                cnn_NAV.Open();

                using (SqlDataReader reader = cmd2.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Insertar_material(Convert.ToInt32(reader["Referencia"]), reader["Descripcion"].ToString(), reader["SecadoTemperatura"].ToString(), reader["SecadoTiempo"].ToString(), reader["Ubicacion"].ToString());

                    }
                }
                cnn_NAV.Close();
                return true;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                cnn_NAV.Close();
                return false;
            }
        }

        public bool Leer_MATERIALES_NAV() //CALIDAD - CAMBIAR A NUEVA BASE DE DATOS PRODUCTOS
        {

            try
            {

                string sql = "SELECT Cast(I.[No_] as int) as Referencia, [Description] AS Descripcion, 1 as Secado, R.[NEO Drying temperature] as SecadoTemperatura, R.[NEO Drying time] as SecadoTiempo, R.[NEO Shelf location] as Ubicacion " +
                    " FROM[NAVDB].[dbo].[THERMO$Item] I LEFT JOIN[NAVDB].[dbo].[THERMO$Item$e8af12d5-328b-47d8-b2d8-158ecbed1ec3] R ON I.No_ = R.No_ " +
                    //" WHERE I.[No_] LIKE '2%' AND([Item Category Code] = 10 OR [Item Category Code] = 12 OR [Item Category Code] = 15 OR [Item Category Code] = 120 OR[Item Category Code] = 150 OR[Item Category Code] = 155  OR[Item Category Code] = 221  OR[Item Category Code] = 215) " +
                    " WHERE (I.[No_] LIKE '2%' or I.[No_] LIKE '3%' or I.[No_] LIKE '1%') AND([Item Category Code] = 8 or [Item Category Code] = 10 OR [Item Category Code] = 12 OR [Item Category Code] = 15 OR [Item Category Code] = 120 OR[Item Category Code] = 150 OR[Item Category Code] = 155  OR[Item Category Code] = 221  OR[Item Category Code] = 215)"+
                     
                    " ORDER BY[Item Category Code] asc";
                SqlCommand cmd2 = new SqlCommand(sql, cnn_NAV);
                cnn_NAV.Open();

                using (SqlDataReader reader = cmd2.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Insertar_material(Convert.ToInt32(reader["Referencia"]), reader["Descripcion"].ToString(), reader["SecadoTemperatura"].ToString(), reader["SecadoTiempo"].ToString(), reader["Ubicacion"].ToString());

                    }
                }
                cnn_NAV.Close();
                return true;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                cnn_NAV.Close();
                return false;
            }
        }


        public bool Insertar_material(int referencia, string descripcion, string SecadoTemperatura, string SecadoTiempo, string Ubicacion)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "";                
                    sql = "INSERT INTO [SMARTH_DB].[dbo].[AUX_Lista_Materiales] ([Referencia],[Descripcion],[ReferenciaReciclado],[Secado],[SecadoTemperatura],[SecadoTiempo],[Ubicacion])" +
                            " VALUES (" + referencia + ",'" + descripcion + "','', 1,'" + SecadoTemperatura + "', '" + SecadoTiempo + "', '" + Ubicacion + "')";       
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception ex)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cnn_SMARTH;
                    string sql2 = "UPDATE [SMARTH_DB].[dbo].[AUX_Lista_Materiales] SET [Descripcion] = '" + descripcion + "', [Ubicacion] = '" + Ubicacion + "' WHERE Referencia = " + referencia + "";
                    cmd.CommandText = sql2;
                    cmd.ExecuteNonQuery();
                    cnn_SMARTH.Close();
                }
                catch (Exception ex2)
                {
                    cnn_SMARTH.Close();
                    return false;
                }
                cnn_SMARTH.Close();
                return false;
            }
        } //CALIDAD - CAMBIAR A NUEVA BASE DE DATOS DE OPERARIOS

        public bool Leer_Materiales_X_Productos()
        {
            try
            {
                string sql = "SELECT DISTINCT(C_RECIPE_ID), C_MATERIAL_ID" +
                              " FROM PCMS.T_RECIPE_X_MATERIAL R, PCMS.T_MATERIALS M" +
                              " WHERE R.C_MATERIAL_ID = M.C_ID AND((M.C_MATTYPE_ID = '15') OR(M.C_MATTYPE_ID = '215') OR(M.C_MATTYPE_ID = '221') OR(M.C_MATTYPE_ID = '155')) AND M.C_LONG_DESCR NOT LIKE '%conct%' AND M.C_LONG_DESCR NOT LIKE '%ADITIVO%'" +
                              " ORDER BY C_RECIPE_ID ASC";
;
                OracleCommand cmd = new OracleCommand(sql, cnn_bms);
                cnn_bms.Open();

                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Insertar_material_X_Producto(Convert.ToInt32(reader["C_RECIPE_ID"]), Convert.ToInt32(reader["C_MATERIAL_ID"]));
                        
                    }
                }
                cnn_bms.Close();
                return true;
            }
            catch (Exception)
            {
                cnn_bms.Close();
                return false;
            }
        }

        public bool Insertar_material_X_Producto(int referencia, int material)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "";
                sql = "INSERT INTO [SMARTH_DB].[dbo].[AUX_Lista_Materiales_X_Producto] ([Referencia],[Material])" +
                        " VALUES (" + referencia + "," + material + ")";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception ex)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cnn_SMARTH;
                    string sql2 = "UPDATE [SMARTH_DB].[dbo].[AUX_Lista_Materiales_X_Producto]  SET [Material] = " + material + " WHERE Referencia = " + referencia + "";
                    cmd.CommandText = sql2;
                    cmd.ExecuteNonQuery();
                    cnn_SMARTH.Close();
                }
                catch (Exception ex2)
                {
                    cnn_SMARTH.Close();
                    return false;
                }
                cnn_SMARTH.Close();
                return false;
            }
        } //CALIDAD - CAMBIAR A NUEVA BASE DE DATOS DE OPERARIOS

        public bool Leer_HorasProductivasFabrica()
        {
            try
            {
                string sql = "SELECT EXTRACT(YEAR FROM C_STARTTIME) AS YEAR, EXTRACT(MONTH FROM C_STARTTIME) AS MONTH, ROUND(SUM(C_AVAILABLETIME / 60), 2) AS DISPONIBLE, ROUND(SUM(C_RUNTIME / 60), 2)  AS FUNCIONANDO"+
                                " FROM HIS.T_HISSHIFTS"+
                                " WHERE C_MASTERJOB = 1"+
                                " GROUP BY EXTRACT(YEAR FROM C_STARTTIME), EXTRACT(MONTH FROM C_STARTTIME)"+
                                " ORDER BY YEAR, MONTH";
                OracleCommand cmd = new OracleCommand(sql, cnn_bms);
                cnn_bms.Open();

                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Insertar_Horas_Disponibles_Fabrica(Convert.ToInt32(reader["YEAR"]), Convert.ToInt32(reader["MONTH"]), Convert.ToDouble(reader["DISPONIBLE"].ToString()), Convert.ToDouble(reader["FUNCIONANDO"].ToString()));

                    }
                }
                cnn_bms.Close();
                return true;
            }
            catch (Exception)
            {
                cnn_bms.Close();
                return false;
            }
        }

        public bool Insertar_Horas_Disponibles_Fabrica(int año, int mes, double disponible, double funcionando)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "";
                sql = "INSERT INTO [SMARTH_DB].[dbo].[AUX_Horas_Disponibles_Fabrica] ([Año],[Mes],[HorasDisponibles],[HorasProduciendo])" +
                        " VALUES (" + año + "," + mes + "," + disponible.ToString().Replace(",", ".") + "," + funcionando.ToString().Replace(",",".") + ")";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception ex)
            {
                try
                {
                    if (Convert.ToInt32(DateTime.Now.ToString("yyyy")) == año  && (Convert.ToInt32(DateTime.Now.ToString("MM")) == mes || Convert.ToInt32(DateTime.Now.ToString("MM")) == (mes-1)))
                    { 
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cnn_SMARTH;
                    string sql2 = "UPDATE [SMARTH_DB].[dbo].[AUX_Horas_Disponibles_Fabrica]   SET [Año] = " + año + ", [Mes] = " + mes + ", [HorasDisponibles] = " + disponible.ToString().Replace(",", ".") + ", [HorasProduciendo] = " + funcionando.ToString().Replace(",",".") + " WHERE [Año] = " + año + " AND [Mes] = " + mes + "";
                    cmd.CommandText = sql2;
                    cmd.ExecuteNonQuery();
                    cnn_SMARTH.Close();
                    }
                }
                catch (Exception ex2)
                {
                    cnn_SMARTH.Close();
                    return false;
                }
                cnn_SMARTH.Close();
                return false;
            }
        }

        public bool Leer_HorasProductivasMaquina()
        {
            try
            {
                string sql = "SELECT EXTRACT(YEAR FROM C_STARTTIME) AS YEAR, EXTRACT(MONTH FROM C_STARTTIME) AS MONTH, C_MACHINE, ROUND(SUM(C_AVAILABLETIME / 60), 2) AS DISPONIBLE, ROUND(SUM(C_RUNTIME / 60), 2)  AS FUNCIONANDO" +
                                " FROM HIS.T_HISSHIFTS" +
                                " WHERE C_MASTERJOB = 1" +
                                " GROUP BY EXTRACT(YEAR FROM C_STARTTIME), EXTRACT(MONTH FROM C_STARTTIME), C_MACHINE " +
                                " ORDER BY YEAR, MONTH";
                OracleCommand cmd = new OracleCommand(sql, cnn_bms);
                cnn_bms.Open();

                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Insertar_Horas_Disponibles_Maquina(Convert.ToInt32(reader["YEAR"]), Convert.ToInt32(reader["MONTH"]), reader["C_MACHINE"].ToString(), Convert.ToDouble(reader["DISPONIBLE"].ToString()), Convert.ToDouble(reader["FUNCIONANDO"].ToString()));

                    }
                }
                cnn_bms.Close();
                return true;
            }
            catch (Exception)
            {
                cnn_bms.Close();
                return false;
            }
        }

        public bool Insertar_Horas_Disponibles_Maquina(int año, int mes, string maquina, double disponible, double funcionando)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "";
                sql = "INSERT INTO [SMARTH_DB].[dbo].[AUX_Horas_Disponibles_Maquina] ([Año],[Mes],[Maquina],[HorasDisponibles],[HorasProduciendo])" +
                        " VALUES (" + año + "," + mes + ",'" +maquina+ "', " + disponible.ToString().Replace(",", ".") + "," + funcionando.ToString().Replace(",", ".") + ")";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception ex)
            {
                try
                {
                    if (Convert.ToInt32(DateTime.Now.ToString("yyyy")) == año && (Convert.ToInt32(DateTime.Now.ToString("MM")) == mes || Convert.ToInt32(DateTime.Now.ToString("MM")) == (mes - 1)))
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = cnn_SMARTH;
                        string sql2 = "UPDATE [SMARTH_DB].[dbo].[AUX_Horas_Disponibles_Maquina]   SET [Año] = " + año + ", [Mes] = " + mes + ", [HorasDisponibles] = " + disponible.ToString().Replace(",", ".") + ", [HorasProduciendo] = " + funcionando.ToString().Replace(",", ".") + " " +
                            " WHERE [Año] = " + año + " AND [Mes] = " + mes + " AND [Maquina] = '"+maquina+"'";
                        cmd.CommandText = sql2;
                        cmd.ExecuteNonQuery();
                        cnn_SMARTH.Close();
                    }
                }
                catch (Exception ex2)
                {
                    cnn_SMARTH.Close();
                    return false;
                }
                cnn_SMARTH.Close();
                return false;
            }
        }

        public bool Leer_HorasProductivasMolde()
        {
            try
            {
                string sql = "SELECT EXTRACT(YEAR FROM C_STARTTIME) AS YEAR, EXTRACT(MONTH FROM C_STARTTIME) AS MONTH, C_TOOL, ROUND(SUM(C_AVAILABLETIME / 60), 2) AS DISPONIBLE, ROUND(SUM(C_RUNTIME / 60), 2)  AS FUNCIONANDO" +
                                " FROM HIS.T_HISSHIFTS" +
                                " WHERE C_MASTERJOB = 1" +
                                " GROUP BY EXTRACT(YEAR FROM C_STARTTIME), EXTRACT(MONTH FROM C_STARTTIME), C_TOOL " +
                                " ORDER BY YEAR, MONTH";
                OracleCommand cmd = new OracleCommand(sql, cnn_bms);
                cnn_bms.Open();

                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Insertar_Horas_Disponibles_Molde(Convert.ToInt32(reader["YEAR"]), Convert.ToInt32(reader["MONTH"]), reader["C_TOOL"].ToString(), Convert.ToDouble(reader["DISPONIBLE"].ToString()), Convert.ToDouble(reader["FUNCIONANDO"].ToString()));

                    }
                }
                cnn_bms.Close();
                return true;
            }
            catch (Exception)
            {
                cnn_bms.Close();
                return false;
            }
        }

        public bool Insertar_Horas_Disponibles_Molde(int año, int mes, string molde, double disponible, double funcionando)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "";
                sql = "INSERT INTO [SMARTH_DB].[dbo].[AUX_Horas_Disponibles_Molde] ([Año],[Mes],[Molde],[HorasDisponibles],[HorasProduciendo])" +
                        " VALUES (" + año + "," + mes + ",'" + molde + "', " + disponible.ToString().Replace(",", ".") + "," + funcionando.ToString().Replace(",", ".") + ")";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception ex)
            {
                try
                {
                    if (Convert.ToInt32(DateTime.Now.ToString("yyyy")) == año && (Convert.ToInt32(DateTime.Now.ToString("MM")) == mes || Convert.ToInt32(DateTime.Now.ToString("MM")) == (mes - 1)))
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = cnn_SMARTH;
                        string sql2 = "UPDATE [SMARTH_DB].[dbo].[AUX_Horas_Disponibles_Molde]   SET [Año] = " + año + ", [Mes] = " + mes + ", [HorasDisponibles] = " + disponible.ToString().Replace(",", ".") + ", [HorasProduciendo] = " + funcionando.ToString().Replace(",", ".") + "" +
                            " WHERE [Año] = " + año + " AND [Mes] = " + mes + " AND [Molde] = '"+molde+"'";
                        cmd.CommandText = sql2;
                        cmd.ExecuteNonQuery();
                        cnn_SMARTH.Close();
                    }
                }
                catch (Exception ex2)
                {
                    cnn_SMARTH.Close();
                    return false;
                }
                cnn_SMARTH.Close();
                return false;
            }
        }



        public bool Limpiar_PlanificacionPrioridades()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "DELETE FROM [SMARTH_DB].[dbo].[AUX_Planificacion_Prioridades]";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }

        public bool Leer_PlanificacionPrioridades()
        { 
            try
            {
                Limpiar_PlanificacionPrioridades();

                string sql = " SELECT Maquina, Orden, REF, Descripcion, trim(Molde) as Molde, CAST(Tiempo_restante AS INT) AS Tiempo_restante, CASE WHEN Posicion > 0 THEN 'En cola' WHEN(Tiempo_Restante like '-%' OR Tiempo_Restante = '0') AND Posicion = 0 THEN 'Completado' WHEN Posicion = 0 THEN TRIM(to_char(trunc(Tiempo_restante / 60), '999999') || 'H' || to_char(round(Tiempo_restante - (hh * 60)), '99') || 'Min') END as Tiempo, Posicion, RemarkProducto, RemarkOrden,RemarksReceta, Prioridad,Prioridaddec" +
                                    " FROM(SELECT distinct j1.c_machine_id AS Maquina, TRIM(j1.C_ID) AS Orden, j1.c_tool_id as Molde, j1.c_prodlongdescr AS Descripcion, TRIM(j1.c_product_id) as REF, CONCAT(TRIM(j1.c_product_id), ' ') AS Referencia, j1.C_CALCENDDATE, j1.C_SEQNR AS Posicion, j1.C_PRIORITY AS Prioridad, CAST(PCMS.fncCstrm_getnumData(m.c_id, m.c_rownr, 19935, 14) AS DECIMAL(10, 2)) AS Tiempo_restante, trunc((PCMS.fncCstrm_getnumData(m.c_id, m.c_rownr, 19935, 14)) / 60)  AS hh," +
                                    " PCMS.fncCstrm_getnumData(m.c_id, m.c_rownr, 19968, 14) AS Completo, p.c_remarks AS RemarkProducto, j1.C_REMARKS as RemarkOrden, CASE WHEN j1.C_PRIORITY IS NULL OR j1.C_PRIORITY = '' THEN 100 ELSE j1.C_PRIORITY END AS Prioridaddec, r.C_REMARKS as RemarksReceta" +
                                " FROM PCMS.T_JOBS j1, PCMS.t_machines m, PCMS.T_products p, PCMS.t_recipes R" +
                                " WHERE p.C_ID(+) = j1.C_PRODUCT_ID AND j1.c_seqnr >= 0 AND j1.c_seqnr <= 8 AND j1.c_machine_id = m.c_id AND p.C_ID = r.C_ID" +
                                " ORDER BY j1.C_SEQNR, Tiempo_restante)";
                OracleCommand cmd = new OracleCommand(sql, cnn_bms);
                cnn_bms.Open();

                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Insertar_PlanificacionPrioridadesBMS(reader["Maquina"].ToString(), reader["Orden"].ToString(), reader["REF"].ToString(), reader["Descripcion"].ToString(), reader["Molde"].ToString(), reader["Tiempo_restante"].ToString().Replace(",", "."), reader["tiempo"].ToString(), Convert.ToInt32(reader["Posicion"]), reader["RemarkProducto"].ToString(), reader["RemarkOrden"].ToString(), reader["RemarksReceta"].ToString(), reader["Prioridad"].ToString(), Convert.ToInt32(reader["Prioridaddec"]));

                        //Convert.ToInt32(reader["C_TOOL_ID"]), reader["C_LONG_DESCR"].ToString(), reader["C_CUSTOMER"].ToString(), reader["C_CUSTOMSTRING11"].ToString());

                    }
                }
                cnn_bms.Close();
                return true;
            }
            catch (Exception)
            {
                cnn_bms.Close();
                return false;
            }
        }

        public bool Insertar_PlanificacionPrioridadesBMS(string MAQUINA, string ORDEN, string PRODUCTO, string DESCRIPCION, string MOLDE, string TIEMPOMINUTOS, string TIEMPO, int SEQNR, string REMARKSPRODUCTO, string REMARKSORDEN, string REMARKSRECETA, string PRIORIDAD, int PRIORIDADDEC)
        {

            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[AUX_Planificacion_Prioridades] ([Maquina],[Orden],[Producto],[ProdDescript],[MOLDE],[TiempoMINUTOS],[Tiempo],[SEQNR],[REMARKPRODUCTO],[REMARKORDEN],[REMARKRECETA],[PRIORIDADDEC])" +
                                   " VALUES ('" + MAQUINA + "','" + ORDEN + "','" + PRODUCTO + "','" + DESCRIPCION + "','" + MOLDE + "'," + TIEMPOMINUTOS + ",'" + TIEMPO + "', " + SEQNR + ",'" + REMARKSPRODUCTO + "', '" + REMARKSORDEN + "', '" + REMARKSRECETA + "', "+PRIORIDADDEC+")";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return false;
            }
        } //CALIDAD - CAMBIAR A NUEVA BASE DE DATOS PRODUCTOS

        public bool Leer_productosBMS()
        {
            try
            {
                string sql = "SELECT P.C_ID, T.C_TOOL_ID, P.C_LONG_DESCR, P.C_CUSTOMER, P.C_CUSTOMSTRING11, P.C_CUSTOMSTRING12 FROM T_PRODUCTS P, T_TOOL_X_PRODUCT T WHERE P.C_ID = T.C_PRODUCT_ID AND REGEXP_LIKE (P.C_ID, '^[^a-z]+$', 'i') AND REGEXP_LIKE (T.C_TOOL_ID, '^[^a-z]+$', 'i') AND P.C_ID > '10000000' ORDER BY C_ID";
                //string sql = "SELECT P.C_ID, T.C_TOOL_ID, P.C_LONG_DESCR, P.C_CUSTOMER, P.C_CUSTOMSTRING11 FROM T_PRODUCTS P, T_TOOL_X_PRODUCT T WHERE P.C_ID = T.C_PRODUCT_ID AND REGEXP_LIKE (P.C_ID, '^[^a-z]+$', 'i') AND REGEXP_LIKE (T.C_TOOL_ID, '^[^a-z]+$', 'i') AND P.C_ID > '61752250' ORDER BY C_ID";
                OracleCommand cmd = new OracleCommand(sql, cnn_bms);
                cnn_bms.Open();

                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Insertar_producto(Convert.ToInt32(reader["C_ID"]), Convert.ToInt32(reader["C_TOOL_ID"]), reader["C_LONG_DESCR"].ToString(), reader["C_CUSTOMER"].ToString(), reader["C_CUSTOMSTRING11"].ToString(), reader["C_CUSTOMSTRING12"].ToString());
                    }
                }
                cnn_bms.Close();
                return true;
            }
            catch (Exception EX)
            {
                cnn_bms.Close();
                return false;
            }
        }
        public bool Insertar_producto(int referencia, int molde, string descripcion, string cliente, string sector, string material)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[AUX_TablaProductos] (Referencia, Molde, Descripcion, Cliente, Sector, Material, [FakeMode]) VALUES " +
                                 "(" + referencia + "," + molde + ",'" + descripcion + "','" + cliente + "','" + sector + "','" + material + "',0)";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception ex)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cnn_SMARTH;
                    string sql = "UPDATE [SMARTH_DB].[dbo].[AUX_TablaProductos] " +
                                    " SET Descripcion = '" + descripcion + "', Cliente = '" + cliente + "', Sector = '" + sector + "', Material = '" + material + "'" +
                                    " WHERE Referencia = " + referencia + "";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    cnn_SMARTH.Close();
                    return true;
                }
                catch (Exception ex2)
                {
                    cnn_SMARTH.Close();
                    return false;
                }
                
            }
        } //CALIDAD - CAMBIAR A NUEVA BASE DE DATOS PRODUCTOS

        public bool Leer_PlanificacionPrevisionBMS()
        {
            try
            {
                Limpiar_PlanificacionPrevisionBMS();

                string sql = "SELECT MAX(TO_NUMBER(TO_CHAR(j.c_calcenddate,'HH24'))) as hora, j.c_machine_id AS MAQUINA, j.c_id AS ACT_ORDEN, max(n.c_id) AS NEXT_ORDEN, max(j.c_product_id) AS ACT_PRODUCTO, max(j.c_productdescr) AS ACT_PRODDESCRIPT, max(n.c_product_id) AS NEXT_PRODUCTO, max(SUBSTR (n.c_prodlongdescr,0,35)) AS NEXT_PRODDESCRIPT, max(DECODE(j.c_seqnr, 0, PCMS.FncJobBalance(j.c_machine_id), j.c_qtyrequired)) AS ACT_CANTPENDIENTE, j.c_tool_id AS ACT_MOLDE, max(n.c_tool_id) AS NEXT_MOLDE, max(n.c_PlnDueDate) as NEXT_ENTREGAPLANIFICADO, max(n.c_plnstartdate) as NEXT_INICIOPLANIFICADO, max(n.c_recipe_id) AS NEXT_RECETA, j.c_seqnr AS SEQNR, max(j.c_calcenddate) as FINCALCULADO, max((n.c_PlnDueDate - (n.c_MountTime / 1440))) AS FECHACAMBIOMAXIMO, max(round((j.c_calcenddate-SYSDATE),2)) AS TIMETOGO,  max(TRIM(a.C_CHARACTERISTICS)) AS UBICACION, max (n.c_remarks) AS REMARKS, max(n.c_priority) as PRIORIDAD" +
                              " FROM(select m.c_id as c_machine_id, nvl(j.c_id, '-') as c_id, j.c_tool_id as c_tool_id, nvl(j.c_seqnr, 0) as c_seqnr, nvl(j.c_CalcStartDate, sysdate) as c_calcstartdate, nvl(j.c_CalcendDate, sysdate) as c_calcenddate, nvl(j.c_qtyrequired, 0) as c_qtyrequired, j.c_qtyrequired AS c_remaining, j.c_product_id as c_product_id, j.c_productdescr as c_productdescr, j.c_colour_id, j.c_remarks, j.c_priority from pcms.t_machines m, pcms.t_jobs j  where m.c_id = j.c_machine_id(+) and nvl(j.c_seqnr(+), 0) = 0 and nvl(j.c_parindex(+), 1) = 1" +
                                " union select j.c_machine_id, j.c_id, j.c_Tool_id, j.c_seqnr, j.c_CalcStartDate, j.c_CalcendDate, j.c_qtyrequired, j.c_qtyrequired, j.c_product_id, j.c_productdescr, j.c_colour_id, j.c_remarks, j.c_priority from pcms.t_jobs j where j.c_parindex = 1) j, pcms.T_JOBS n, pcms.T_MACHINES m, pcms.T_TOOLS a" +
                              " WHERE j.C_SEQNR >= 0 AND n.C_MACHINE_ID(+) = j.C_MACHINE_ID AND n.C_SEQNR(+) = j.c_seqnr + 1 AND n.C_PARINDEX = 1 AND j.C_MACHINE_ID = m.C_ID AND(j.C_ID is not null) AND(n.C_ID is not null) " +
                              " AND((C_PLNSTARTDATE <= (SYSDATE + 0.16)) or(c_plnstartdate is null)) " +
                              " AND a.C_ID(+) = n.c_tool_id AND j.C_MACHINE_ID <> 'MONT' AND j.C_MACHINE_ID <> 'HOUS' AND j.C_MACHINE_ID <> 'RTRB' AND j.C_MACHINE_ID <> 'FOAM' and j.c_tool_id<> n.c_tool_id" +
                              " GROUP BY j.C_MACHINE_ID, j.C_TOOL_ID, j.C_SEQNR, j.C_ID" +
                              " ORDER BY TIMETOGO";
                OracleCommand cmd = new OracleCommand(sql, cnn_bms);
                cnn_bms.Open();

                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Insertar_PlanificacionPrevisionBMS(Convert.ToInt32(reader["hora"]), reader["MAQUINA"].ToString(), reader["ACT_ORDEN"].ToString(), reader["NEXT_ORDEN"].ToString(), reader["ACT_PRODUCTO"].ToString(), reader["ACT_PRODDESCRIPT"].ToString(), reader["NEXT_PRODUCTO"].ToString(), reader["NEXT_PRODDESCRIPT"].ToString(), Convert.ToInt32(reader["ACT_CANTPENDIENTE"]), reader["ACT_MOLDE"].ToString(), reader["NEXT_MOLDE"].ToString(), reader["NEXT_ENTREGAPLANIFICADO"].ToString(), reader["NEXT_INICIOPLANIFICADO"].ToString(), reader["NEXT_RECETA"].ToString(), Convert.ToInt32(reader["SEQNR"]), reader["FINCALCULADO"].ToString(), reader["FECHACAMBIOMAXIMO"].ToString(), reader["TIMETOGO"].ToString().Replace(',', '.'), reader["REMARKS"].ToString(), reader["PRIORIDAD"].ToString());

                            //Convert.ToInt32(reader["C_TOOL_ID"]), reader["C_LONG_DESCR"].ToString(), reader["C_CUSTOMER"].ToString(), reader["C_CUSTOMSTRING11"].ToString());

                    }
                }
                cnn_bms.Close();
                return true;
            }
            catch (Exception)
            {
                cnn_bms.Close();
                return false;
            }
        }

        public bool Limpiar_PlanificacionPrevisionBMS()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "DELETE FROM [SMARTH_DB].[dbo].[AUX_Planificacion_Prevision]";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }

        public bool Insertar_PlanificacionPrevisionBMS(int hora, string MAQUINA, string ACT_ORDEN, string NEXT_ORDEN, string ACT_PRODUCTO, string ACT_PRODDESCRIPT, string NEXT_PRODUCTO, string NEXT_PRODDESCRIPT, int ACT_CANTPENDIENTE, string ACT_MOLDE, string NEXT_MOLDE, string NEXT_ENTREGAPLANIFICADO, string NEXT_INICIOPLANIFICADO, string NEXT_RECETA, int SEQNR, string FINCALCULADO, string FECHACAMBIOMAXIMO, string TIMETOGO, string REMARKS, string PRIORIDAD)
        {
            
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[AUX_Planificacion_Prevision] ([Hora],[Maquina],[ACT_Orden],[NEXT_Orden],[ACT_Producto],[ACT_ProdDescript],[NEXT_Producto],[NEXT_ProdDescript],[ACT_CantPendiente],[ACT_MOLDE],[NEXT_MOLDE],[NEXT_ENTREGAPLANIFICADO],[NEXT_INICIOPLANIFICADO],[NEXT_RECETA],[SEQNR],[FINCALCULADO],[FECHACAMBIOMAXIMO],[TIMETOGO],[REMARKS],[Prioridad])" +
                                                                                " VALUES (" + hora + ",'" + MAQUINA + "','" + ACT_ORDEN + "','" + NEXT_ORDEN + "','" + ACT_PRODUCTO + "','" + ACT_PRODDESCRIPT + "','" + NEXT_PRODUCTO + "', '" + NEXT_PRODDESCRIPT + "', " + ACT_CANTPENDIENTE + ", '" + ACT_MOLDE + "', '" + NEXT_MOLDE + "', '" + NEXT_ENTREGAPLANIFICADO + "', '" + NEXT_INICIOPLANIFICADO + "', '" + NEXT_RECETA + "', " + SEQNR+", '" + FINCALCULADO + "','" + FECHACAMBIOMAXIMO + "',"+TIMETOGO+",'" + REMARKS + "','"+PRIORIDAD+"')";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return false;
            }
        } //CALIDAD - CAMBIAR A NUEVA BASE DE DATOS PRODUCTOS
      
        public bool InsertaProductosReferenciaEstados() //CALIDAD - CAMBIAR A NUEVA BASE DE DATOS DE PRODUCTOS ESTADOS
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[GP12_ProductosEstados] SELECT P.Referencia, 0 as EstadoActual, 2 as Responsable, null  as Fecharev, null as Fechaprevsalida, null as EstadoAnterior, null as Fechaestanterior, null as Observaciones, null as IdNoConformidad, 0 as RequiereMontaje  FROM [SMARTH_DB].[dbo].[AUX_TablaProductos] P FULL JOIN [SMARTH_DB].[dbo].[GP12_ProductosEstados] E ON P.Referencia = E.Referencia WHERE E.Referencia IS NULL";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }

            catch (Exception)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }

        public bool InsertaProductosTablaDocumentos() //CALIDAD - CAMBIAR A NUEVA BASE DE DATOS DE DOCUMENTOSXREFERENCIAS
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[DOCUMENTAL_Base_DocumentosXReferencias] SELECT P.Referencia, P.Molde, null as ImagenPieza, null as PlanControl, null as PautaControl, null as PautaRecepcion1, null as PautaRecepcion2, null as OperacionEstandar, null as OperacionEstandar2, null as Defoteca, null as Embalaje, null as Gp12, null as [PautaRetrabajo], null as [VideoAuxiliar], null as [Observaciones], null as [NoConformidades], SYSDATETIME() as [FechaModificacion], 'Agregada referencia al sistema de documentación.' as [RazonModificacion]  FROM [SMARTH_DB].[dbo].[AUX_TablaProductos] P FULL JOIN [SMARTH_DB].[dbo].[DOCUMENTAL_Base_DocumentosXReferencias] E ON P.Referencia = E.Referencia WHERE E.Referencia IS NULL";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }

            catch (Exception)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }

        public bool Leer_moldes()
        {

            try
            {
             
                string sql = "SELECT '' AS ID, C_LONG_DESCR AS DESCRIPCION, TRIM(C_ID) AS CODIGO, TRIM(C_ID) AS REFERENCIAMOLDE, TRIM(C_ORIGINALCAVITIES) AS CAVIDADES, C_CHARACTERISTICS AS UBICACION FROM PCMS.T_TOOLS WHERE C_ID LIKE '3333%' or C_ID LIKE '9999%'";
                
                OracleCommand cmd = new OracleCommand(sql, cnn_bms);
                cnn_bms.Open();

                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //Insertar_molde_mant(reader["DESCRIPCION"].ToString(), Convert.ToInt32(reader["CODIGO"]), Convert.ToInt32(reader["REFERENCIAMOLDE"]), Convert.ToInt32(reader["CAVIDADES"]), reader["UBICACION"].ToString());
                        Insertar_Molde_Mant_SMARTH(reader["DESCRIPCION"].ToString(), Convert.ToInt32(reader["REFERENCIAMOLDE"]), Convert.ToInt32(reader["CAVIDADES"]), reader["UBICACION"].ToString());

                    }
                }

                cnn_bms.Close();
                return true;
            }
            catch (Exception ex)
            {
                //file.Close();
                cnn_bms.Close();
                //mandar_mail("Error al importar moldes. " + ex.Message.ToString());
                return false;
            }
        } //MANTENIMIENTO
        
        public bool Insertar_Molde_Mant_SMARTH(string descripcion, int referenciamolde, int cavidades, string ubicacion) // MANTENIMIENTO - NUEVA BASE DE DATOS: OBSOLETAR LA OTRA
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[AUX_Lista_Moldes] (Descripcion, ReferenciaMolde, Cavidades, Ubicacion, Activo)" +
                             " VALUES ('" + descripcion + "'," + referenciamolde + "," + cavidades + ", 'Sin ubicacion', 1)";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception ex)
            {
                try
                {
                   
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cnn_SMARTH;
                    string sql = "UPDATE [SMARTH_DB].[dbo].[AUX_Lista_Moldes] SET Descripcion = '" + descripcion + "' WHERE ReferenciaMolde = " + referenciamolde + " ";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    cnn_SMARTH.Close();
                }
                catch (Exception ex2)
                { 
                  cnn_SMARTH.Close();
                  return false;
                }
               
                return false;
            }
        }

        public bool Leer_moldes_ultima_produccion()
        {

            try
            {
                string sql = "SELECT TRIM(C_PRODUCTTOOL) AS MOLDEHIS, TO_CHAR(MAX(C_STARTTIME), 'DD/MM/YYYY') AS ULTIMAPROD FROM HIS.T_HISJOBS GROUP BY C_PRODUCTTOOL ORDER BY C_PRODUCTTOOL";
                OracleCommand cmd = new OracleCommand(sql, cnn_bms);
                cnn_bms.Open();

                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //Insertar_molde_mant(reader["DESCRIPCION"].ToString(), Convert.ToInt32(reader["CODIGO"]), Convert.ToInt32(reader["REFERENCIAMOLDE"]), Convert.ToInt32(reader["CAVIDADES"]), reader["UBICACION"].ToString());
                        Actualizar_Molde_Ultima_Produccion(Convert.ToInt32(reader["MOLDEHIS"]), reader["ULTIMAPROD"].ToString());

                    }
                }

                cnn_bms.Close();
                return true;
            }
            catch (Exception ex)
            {
                
                cnn_bms.Close();
               
                return false;
            }
        } 

        public bool Actualizar_Molde_Ultima_Produccion(int referenciamolde, string fecha) 
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "UPDATE [SMARTH_DB].[dbo].[AUX_Lista_Moldes] SET FechaUltimaProduccion = '" + fecha + "' WHERE ReferenciaMolde = " + referenciamolde + " ";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }

        public bool LimpiarOrdenesProduciendoBMS()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "DELETE FROM [SMARTH_DB].[dbo].[AUX_Planificacion]";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }
        public bool leer_OrdenesProduciendoBMS()
        {

            try
            {
               string sql = " SELECT J.C_MACHINE_ID, J.C_ID, J.C_PARENT_ID, J.C_PARINDEX, J.C_TOOL_ID, J.C_PRODUCT_ID, J.C_CALCSTARTDATE, S.ESTADO2, J.C_SEQNR FROM PCMS.T_JOBS J" +
                             " LEFT JOIN(select C_MACHINE_ID AS MAQUINA, CASE CAST(E.C_FINAL_RUNSTATE AS VARCHAR2(30)) WHEN '1' THEN 'En marcha' ELSE CAST(S.C_SHORT_DESCR AS VARCHAR2(30)) END AS ESTADO2, S.C_STOPGROUPS_ID from ELOG2.T_ELOG_STOPINFO E LEFT JOIN PCMS.T_STPTABLE_STPCODE S ON E.C_FINAL_STOPCODE = S.C_STOPCODE_NR AND C_STOPTABLE_ID = 2"+
                             " where C_NEXT_ID is null order by c_machine_id asc) S ON J.C_MACHINE_ID = S.MAQUINA WHERE C_SEQNR >= 0 ORDER BY C_MACHINE_ID, C_PARINDEX, C_SEQNR";
                OracleCommand cmd = new OracleCommand(sql, cnn_bms);
                cnn_bms.Open();
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        insertar_orden_produciendo(reader["C_MACHINE_ID"].ToString(), reader["C_ID"].ToString(), reader["C_PARENT_ID"].ToString(), Convert.ToInt32(reader["C_PARINDEX"]), reader["C_TOOL_ID"].ToString(), reader["C_PRODUCT_ID"].ToString(), reader["C_CALCSTARTDATE"].ToString(), reader["ESTADO2"].ToString(), reader["C_SEQNR"].ToString());
                    }
                }
                cnn_bms.Close();
                return true;
            }
            catch (Exception)
            {
                //file.Close();
                cnn_bms.Close();
                //mandar_mail("Error al importar moldes. " + ex.Message.ToString());
                return false;
            }
        }
        public bool insertar_orden_produciendo(string maquina, string orden, string ordenmaestra, int indice, string molde, string producto, string HoraCarga, string Estado, string seqnr)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[AUX_Planificacion] (Maquina,Orden,OrdenMaestra,Linea,Molde,Referencia,HoraCarga,EstadoMaquina, SEQNR) VALUES " +
                                 "('" + maquina + "','" + orden + "','" + ordenmaestra + "'," + indice + ",'" + molde + "','" + producto + "', '" + HoraCarga + "', '" + Estado + "', "+seqnr+")";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }

        public DataTable Devuelve_Lista_Materiales_SEPARADOR_NAV(string where)
        {
            try
            {
                cnn_NAV.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_NAV;
                string sql = "SELECT I.[No_] + ' ¬ ' + [Description] as MATFILTRO, I.[No_] AS MATERIAL, [Description] AS LONG_DESCRIPTION, R.[NEO Shelf location] AS SHORT_DESCRIPTION, CONCAT (R.[NEO Drying temperature], ' | ', R.[NEO Drying time]) AS REMARKS, CAN.CANTALM, PUR.FECHA, PUR.QUANTITY" +
                             " FROM[NAVDB].[dbo].[THERMO$Item] I " +
                             " LEFT JOIN[NAVDB].[dbo].[THERMO$Item$e8af12d5-328b-47d8-b2d8-158ecbed1ec3] R ON I.No_ = R.No_ " +
                             " LEFT JOIN(SELECT MAT.[No_] AS MATERIAL, CASE WHEN CANTALM IS NULL THEN 0.0 ELSE CANTALM END AS CANTALM" +
                                        " FROM[NAVDB].[dbo].[THERMO$Item] MAT" +
                                        " LEFT JOIN(SELECT RTRIM([Item No_]) as MATERIAL, sum([Item Ledger Entry Quantity]) AS CANTALM" +
                                        " FROM[NAVDB].[dbo].[THERMO$Value Entry] where[Item No_] LIKE '2%' group by[Item No_]) CAN ON MAT.[No_] = CAN.MATERIAL" +
                                        " WHERE MAT.[No_] LIKE '2%') CAN ON I.No_ = CAN.MATERIAL" +
                             " LEFT JOIN(SELECT DAT.No_, DAT.FECHA, CAN.Quantity " +
                                         " FROM(SELECT [No_], MIN([Expected Receipt Date]) AS FECHA" +
                                              " FROM[NAVDB].[dbo].[THERMO$Purchase Line]" +
                                              " WHERE[Document No_] LIKE 'PC23%' AND[Outstanding Amount] > 0 AND[Posting Group] = 'MAT. PRIMA' AND[Line No_] LIKE '1%' GROUP BY No_) DAT" +
                                         " LEFT JOIN[NAVDB].[dbo].[THERMO$Purchase Line] CAN ON DAT.No_ = CAN.No_ AND DAT.FECHA = CAN.[Expected Receipt Date]) PUR ON I.No_ = PUR.No_" +
                             " WHERE " + where + " " +
                             " AND(I.[Item Category Code] = 8 OR I.[Item Category Code] = 15 OR I.[Item Category Code] = 215 OR I.[Item Category Code] = 221 OR I.[Item Category Code] = 215 OR I.[Item Category Code] = 155" +
                                    " OR I.[Item Category Code] = 150 OR I.[Item Category Code] = 012 OR I.[Item Category Code] = 10 OR I.[Item Category Code] = 12 " +
                                    " OR I.[Item Category Code] = 120 OR I.[Item Category Code] = 20 OR I.[Item Category Code] = 200 OR I.[Item Category Code] = 220" +
                                    " OR I.[Item Category Code] = 221 OR I.[Item Category Code] = 222)" +
                              " ORDER BY MATERIAL asc";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_NAV);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_NAV.Close();
                return dt;
        
            }
            catch (Exception ex)
            {
                cnn_NAV.Close();
                return null;
            }
        }

        public DataTable Devuelve_Lista_Materiales_LEFTJOIN()
        {
            try
            {
                cnn_NAV.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_NAV;
                string sql = "SELECT * FROM [NAVDB].[dbo].[THERMO$Item]";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_NAV);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_NAV.Close();
                return dt;

            }
            catch (Exception ex)
            {
                cnn_NAV.Close();
                return null;
            }
        }

        //***************************************\\SINCRONIZACIÓN DE BASES DE DATOS//**************************************\\
        //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\//\\

        public DataTable LeerFichaParametros(int referencia, int maquina, int version)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT *" +
                             " FROM [SMARTH_DB].[dbo].[PARAMETROS_Ficha] F" +
                             " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] PRO ON F.Referencia = PRO.Referencia" +
                             " LEFT JOIN [SMARTH_DB].[dbo].[AUX_Lista_Clientes] CLI ON PRO.Cliente = CLI.Cliente" +
                             " LEFT JOIN [SMARTH_DB].[dbo].[DOCUMENTAL_Base_DocumentosXReferencias] DOC ON F.Referencia = DOC.Referencia"+
                             " WHERE F.Referencia = " + referencia + " AND F.Maquina = " + maquina + " AND F.Version = " + version;
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable LeerFichaParametrosV2(int referencia, int maquina, int version)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT F.[Referencia],[Maquina],[Version], PRO.[Descripcion],F.[Cliente],CLI.LogotipoSM,DOC.ImagenPieza,UMA.Ubicacion + ' (' + UMA.Zona + ' ' + UMA.Nave + ')' AS UBIMOLDE" +
                                ",CAST(MAN.UBICACION AS varchar) +' ' + CASE WHEN MAN.AREA = 1 THEN '(Obsoleto)' WHEN MAN.AREA = 2 THEN '(Cuarto de manos)' WHEN MAN.AREA = 3 THEN '(Máquina 34)' WHEN MAN.AREA = 4 THEN '(Cubetas estantería)' WHEN MAN.AREA = 5 THEN '(Máquina 32)' WHEN MAN.AREA = 6 THEN '(Máquina 48)' WHEN MAN.AREA = 7 THEN '(Máquina 43)' WHEN MAN.AREA = 8 THEN '(Junto a molde)' ELSE CAST(MAN.AREA AS varchar) END AS UBIMANO" +
                                ",MOL.Mano,[CodMolde],[NMaquina],[PersonalAsignado],[ProgramaMolde],[NProgramaRobot],[NManoRobot],[AperturaMaquina],[NCavidades],[PesoPieza],[PesoColada],[PesoTotal],[VelocidadCarga],[Carga],[Descompresion],[Contrapresion],[Tiempo],[Enfriamiento],[Ciclo],[Edicion],[Fecha],F.[Observaciones],[Cojin],[Razones],[INTElaborado],[INTRevisado],[INTAprobado],[FuerzaCierre],[ModoAutoMan],[PesoPiezas],[PesoColadas],[PesoTotales]" +
                             " FROM[SMARTH_DB].[dbo].[PARAMETROS_Ficha] F" +
                                " LEFT JOIN[SMARTH_DB].[dbo].[AUX_TablaProductos] PRO ON F.Referencia = PRO.Referencia" +
                                " LEFT JOIN[SMARTH_DB].[dbo].[AUX_Lista_Clientes] CLI ON PRO.Cliente = CLI.Cliente" +
                                " LEFT JOIN[SMARTH_DB].[dbo].[DOCUMENTAL_Base_DocumentosXReferencias] DOC ON F.Referencia = DOC.Referencia" +
                                " LEFT JOIN[SMARTH_DB].[dbo].AUX_Lista_Moldes MOL ON F.CodMolde = MOL.ReferenciaMolde" +
                                " LEFT JOIN[SMARTH_DB].[dbo].AUX_Lista_Moldes_Ubicaciones UMA ON MOL.Ubicacion = UMA.Ubicacion" +
                                " LEFT JOIN[SMARTH_DB].[dbo].AUX_Lista_Manos_Robot MAN ON MOL.Mano = MAN.MANO" +
                             " WHERE F.Referencia = "+referencia+" AND F.Maquina = "+maquina+" AND F.Version = "+version+"";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }



        //GP12
        public bool LimpiarTablaPrevisiones()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "DELETE FROM [SMARTH_DB].[dbo].[AUX_BMSHistoricoProducidas]";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }

        public bool LimpiarTablaHistoria()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "DELETE FROM [SMARTH_DB].[dbo].[AUX_BMSHistoricoProducidas]";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }

        public bool Lee_produccionhistorica_BMS()
        {

            try
            {
                //string sql = "SELECT '' AS ID, C_LONG_DESCR AS DESCRIPCION, TRIM(C_ID) AS CODIGO, TRIM(C_ID) AS REFERENCIAMOLDE, TRIM(C_ORIGINALCAVITIES) AS CAVIDADES, C_CHARACTERISTICS AS UBICACION FROM PCMS.T_TOOLS WHERE C_ID LIKE '3333%' or C_ID LIKE '9999%'";
                string sql = "SELECT to_char(to_date(C_STARTTIME, 'DD-MM-rrrr'), 'yyyy') AS YEA, to_char(to_date(C_STARTTIME, 'DD-MM-YYYY'), 'fmMM') AS MES, SUM (cast(C_PRODUCTIONCOUNT02 as integer)) AS CANTIDAD FROM HIS.T_HISJOBS" +
                             " GROUP BY to_char(to_date(C_STARTTIME, 'DD-MM-rrrr'), 'yyyy'), to_char(to_date(C_STARTTIME, 'DD-MM-YYYY'), 'fmMM') ORDER BY YEA, MES";

                OracleCommand cmd = new OracleCommand(sql, cnn_bms);
                cnn_bms.Open();

                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Insertar_historiaBMS(Convert.ToInt32(reader["YEA"]), Convert.ToInt32(reader["MES"]), Convert.ToDouble(reader["CANTIDAD"]));

                    }
                }
                cnn_bms.Close();
                return true;
            }
            catch (Exception)
            {             
                cnn_bms.Close();
                return false;
            }
        }

        public DataTable Devuelve_productos_SEPARADOR()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT CAST([Referencia] AS varchar) + '¬' + [Descripcion] AS PRODUCTO" +
                              " FROM[SMARTH_DB].[dbo].[AUX_Lista_Materiales]" +
                              " UNION ALL" +
                              " SELECT CAST([Referencia] AS varchar) +'¬' + [Descripcion] AS PRODUCTO" +
                              " FROM[SMARTH_DB].[dbo].[AUX_TablaProductos]" +
                              " ORDER BY PRODUCTO ASC";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }


        public bool Insertar_historiaBMS(int periodo, int mes, double cantidad)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[AUX_BMSHistoricoProducidas] (Periodo, Mes, Cantidad) VALUES " +
                                 "(" + periodo + "," + mes + "," + cantidad + ")";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }

        //NO CONFORMIDADES
        public bool LimpiarTablaPiezasEnviada()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "DELETE FROM [SMARTH_DB].[dbo].[AUX_Piezas_Pz_Enviadas_NAV]";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                sql = "DELETE FROM [SMARTH_DB].[dbo].[AUX_Piezas_Chatarras_Costes_BMS]";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();


                cnn_SMARTH.Close();
                Leer_piezas_enviadas_NAV();
                Leer_coste_chatarras_BMS();
                return true;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }
        public bool Leer_piezas_enviadas_NAV()
        {
            try
            {
                cnn_NAV.Open();
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;
                cmd.Connection = cnn_NAV;

                cmd.CommandText = "SELECT YEAR ([Posting Date]) AS YEAR,MONTH([Posting Date]) AS MES, CAST(ABS((SUM([Quantity]))) as int) AS Cantidad" +
                                  " FROM[NAVDB].[dbo].[THERMO$Item Ledger Entry] where[Entry Type] = 1 AND[Item No_] <> '66666666' AND YEAR([Posting Date]) >= 2021" +
                                  " group by  YEAR([Posting Date]),MONTH([Posting Date]) order by year, MES";

                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Insertar_piezas_enviadas_NAV(Convert.ToInt32(reader["YEAR"]), Convert.ToInt32(reader["MES"]), Convert.ToInt32(reader["Cantidad"]));
                }
                cnn_NAV.Close();
                return true;
            }
            catch (Exception)
            {

                cnn_NAV.Close();
                return false;
            }
        }
        private bool Insertar_piezas_enviadas_NAV(int AÑO, int MES, int cantidad)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql2 = "INSERT INTO [SMARTH_DB].[dbo].[AUX_Piezas_Pz_Enviadas_NAV] (Año, Mes, CantidadEnviada) VALUES " +
                                 "(" + AÑO + "," + MES + "," + cantidad + ")";
                cmd.CommandText = sql2;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception)
            {
                try
                {
                    if (Convert.ToInt32(DateTime.Now.ToString("yyyy")) == AÑO && (Convert.ToInt32(DateTime.Now.ToString("MM")) == MES || Convert.ToInt32(DateTime.Now.ToString("MM")) == (MES - 1)))
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = cnn_SMARTH;
                        string sql2 = "UPDATE [SMARTH_DB].[dbo].[AUX_Piezas_Pz_Enviadas_NAV] SET [Año] = " + AÑO + ", [Mes] = " + MES + ", [CantidadEnviada] = " + cantidad.ToString().Replace(",", ".") + "" +
                            " WHERE [Año] = " + AÑO + " AND [Mes] = " + MES + "";
                        cmd.CommandText = sql2;
                        cmd.ExecuteNonQuery();
                        cnn_SMARTH.Close();
                    }
                }
                catch (Exception ex2)
                {
                    cnn_SMARTH.Close();
                    return false;
                }
                cnn_SMARTH.Close();
                return false;
            }
        }
        public bool Leer_coste_chatarras_BMS()
        {

            try
            {
                string sql = "SELECT extract(YEAR from C_STARTTIME) AS YEAR, extract(month from C_STARTTIME) AS MES, SUM(C_COUNT27*C_CUSTOMVALUE2) AS COSTEARRANQUE, SUM((C_COUNTTOTAL-C_COUNT27)*C_CUSTOMVALUE2)  AS COSTESCRAP" +
                             " FROM HIS.T_HISSHIFTS WHERE extract(year from C_STARTTIME) >= 2021 GROUP BY extract(YEAR from C_STARTTIME), extract(month from C_STARTTIME) ORDER BY YEAR, MES";
                OracleCommand cmd = new OracleCommand(sql, cnn_bms);
                cnn_bms.Open();

                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Insertar_coste_scrap_BMS(Convert.ToInt32(reader["YEAR"]), Convert.ToInt32(reader["MES"]), Convert.ToDouble(reader["COSTEARRANQUE"]), Convert.ToDouble(reader["COSTESCRAP"]));
                    }
                }
                cnn_bms.Close();

                return true;
            }
            catch (Exception)
            {

                cnn_bms.Close();
                return false;
            }
        }
        private bool Insertar_coste_scrap_BMS(int AÑO, int MES, double costearranque, double costescrap)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql2 = "INSERT INTO [SMARTH_DB].[dbo].[AUX_Piezas_Chatarras_Costes_BMS] ([Año],[Mes],[CosteChatarra],[CosteArranque]) VALUES " +
                                 "(" + AÑO + "," + MES + "," + costescrap.ToString().Replace(',', '.') + "," + costearranque.ToString().Replace(',', '.') + ")";
                cmd.CommandText = sql2;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception)
            {
                try
                {
                    if (Convert.ToInt32(DateTime.Now.ToString("yyyy")) == AÑO && (Convert.ToInt32(DateTime.Now.ToString("MM")) == MES || Convert.ToInt32(DateTime.Now.ToString("MM")) == (MES - 1)))
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = cnn_SMARTH;
                        string sql2 = "UPDATE [SMARTH_DB].[dbo].[AUX_Piezas_Chatarras_Costes_BMS] SET [Año] = " + AÑO + ", [Mes] = " + MES + ", [CosteChatarra] = " + costescrap.ToString().Replace(",", ".") + ", [CosteArranque] = " + costearranque.ToString().Replace(",", ".") + "" +
                            " WHERE [Año] = " + AÑO + " AND [Mes] = " + MES + "";
                        cmd.CommandText = sql2;
                        cmd.ExecuteNonQuery();
                        cnn_SMARTH.Close();
                    }
                }
                catch (Exception ex2)
                {
                    cnn_SMARTH.Close();
                    return false;
                }
                cnn_SMARTH.Close();
                return false;
            }
        }

        public DataTable Devuelve_estado_referencia_NC(string referencia)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT AUS.Razon,[Fecharev] AS DESDE,[Fechaprevsalida] AS HASTA"+
                                  " FROM[SMARTH_DB].[dbo].[GP12_ProductosEstados] EST" +
                                  " left join [SMARTH_DB].[dbo].[AUX_GP12_RazonRevision] AUS on EST.EstadoActual = AUS.Id" +
                             " WHERE Referencia = '"+referencia+"'";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public String Devuelve_ultimas_NC()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT TOP (5)[IdNoConformidad] FROM[SMARTH_DB].[dbo].[NC_Alerta_de_calidad] where TipoNoConformidad = 2 order by IdNoConformidad desc";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                Random r = new Random();
                int Linea = r.Next(0, 4);
                cnn_SMARTH.Close();
                string NC = dt.Rows[Linea]["IdNoConformidad"].ToString(); 
                return NC;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return "0";
            }
        }


        //GESTION PDCA
        public int Devuelve_MAXID_PDCA_Principal_NC()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT MAX([IdPDCA])+1 AS MAXID FROM [SMARTH_DB].[dbo].[PDCA_Principal]";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return Convert.ToInt16(ds.Tables[0].Rows[0]["MAXID"].ToString());
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return 0;
            }
        }

        public DataTable Devuelve_Producto_Seleccionado_NUEVO_PDCA_NC(string Referencia)
        {
            try
            {

                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM [SMARTH_DB].[dbo].[AUX_TablaProductos] where Referencia = " + Referencia + "", cnn_SMARTH);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                cnn_SMARTH.Close();
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                return dt;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public bool Insertar_Nuevo_PlanAcciones_NC(int IdPDCA, int Tipo, string Referencia, string ReferenciaTEXT, string molde, int Modo, string objetivo, string Cliente, int Prioridad, int Piloto, string FechaApertura, int Accionprioridad, int Accionestado, int numalerta)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "INSERT INTO[SMARTH_DB].[dbo].[PDCA_Principal] " +
                             " ([IdPDCA],[Tipo],[Referencia],[ReferenciaTEXT],[Modo],[Desviacion],[Cliente],[Prioridad],[Molde],[Piloto],[Estado],[Apertura],[NAlertaCal] )" +
                             " VALUES(" + IdPDCA + ", " + Tipo + ",'" + Referencia + "', '" + ReferenciaTEXT + "'," + Modo + ", '" + objetivo + "', '" + Cliente + "'," + Prioridad + "," + molde + "," + Piloto + ", 1, '" + FechaApertura + "', "+numalerta+")";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                Insertar_A_ListaAcciones_NC(IdPDCA, 0, FechaApertura, "NULL", null, 0, "", "", "", 2, 0, 0, 0, 0, 0, "", 0, 0, "", "", "", "", "");
                return false;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }

        public bool Insertar_A_ListaAcciones_NC(int IdPDCA, int IdReferencial, string FechaApertura, string FechaCierrePrev, string FechaCierreReal, int Tipo, string Desviacion, string CausaRaiz, string Accion, int Piloto, int APPEstado, int Efectividad, int APPVinculada, int APPIdVinculada, int Eliminar, string producto, int Accionprioridad, int Accionestado, string AccionMaquina, string Evidencia1, string Evidencia2, string Evidencia3, string Evidencia4)
        {
            try
            {

                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "INSERT INTO[SMARTH_DB].[dbo].[PDCA_X_ListaAcciones]" +
                             " ([IdPDCA],[IdReferencial],[FechaApertura],[FechaCierrePrev],[FechaCierrePrevback],[FechaCierreReal],[Tipo],[DesviacionEncontrada],[CausaRaiz],[Accion],[Piloto],[APPEstado],[Efectividad],[APPVinculada],[APPIdVinculada],[Eliminar],[AccionProducto],[AccionPrioridad],[AccionEstado],[AccionMaquina],[AdjuntoEVIDENCIA1],[AdjuntoEVIDENCIA2],[AdjuntoEVIDENCIA3],[AdjuntoEVIDENCIA4],[EstadoContencion],[OrigenCausa],[LeccionAprendida])" +
                             " VALUES(" + IdPDCA + "," + IdReferencial + ",'" + FechaApertura + "'," + FechaCierrePrev + "," + FechaCierrePrev + ",NULL," + Tipo + ",'" + Desviacion + "','" + CausaRaiz + "','" + Accion + "'," + Piloto + "," + APPEstado + "," + Efectividad + "," + APPVinculada + "," + APPIdVinculada + "," + Eliminar + ",'" + producto + "', " + Accionprioridad + "," + Accionestado + ",'" + AccionMaquina + "','" + Evidencia1 + "','" + Evidencia2 + "','" + Evidencia3 + "','" + Evidencia4 + "',0,0,0)";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return false;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }

        public DataTable Devuelve_Lecciones_Aprendidas(string WHERE, string WHERE2)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * "+
                                " FROM(SELECT A.[Id], CASE WHEN A.[AccionMaquina] = '' THEN '-' ELSE A.[AccionMaquina] END AS[AccionMaquina],"+
                                " CASE WHEN A.OrigenCausa IS NULL OR A.OrigenCausa = 0 THEN 'Sin definir' WHEN A.OrigenCausa = 1 THEN 'Entorno' WHEN A.OrigenCausa = 2 THEN 'Máquina/Molde' WHEN A.OrigenCausa = 3 THEN 'Medida' WHEN A.OrigenCausa = 4 THEN 'Material' WHEN A.OrigenCausa = 5 THEN 'Método' WHEN A.OrigenCausa = 6 THEN 'Persona' END AS OrigenCausa,"+ 
                                " CASE WHEN[Tipo] = 0 THEN '-' WHEN[Tipo] = 1 THEN 'CONTENCIÓN:' WHEN[Tipo] = 2 THEN 'DETECCIÓN' WHEN Tipo = 3 THEN 'OCURRENCIA' END AS Tipo,"+
                                " CASE WHEN A.AccionProducto = '' THEN '-' ELSE LTRIM(A.AccionProducto) END as ProdMOLDE, DesviacionEncontrada, CausaRaiz, Accion," +
                                " CASE WHEN PRO.Cliente IS NULL THEN '-' ELSE PRO.Cliente END as Cliente," +
                                " CASE WHEN PRO.Molde IS NULL THEN '-' ELSE CAST(PRO.Molde AS varchar) END as Molde," +
                                " CASE WHEN PRO.Descripcion IS NULL THEN '-' ELSE PRO.Descripcion END as Descripcion," +
                                " CASE WHEN PRO.Sector IS NULL THEN '-' ELSE PRO.Sector END as Sector,"+
                                " CASE WHEN PRO.Material IS NULL or PRO.MATERIAL = '' THEN '-' ELSE PRO.Material END as Material," +
                                " A.LeccionAprendida" +
                                " FROM[SMARTH_DB].[dbo].[PDCA_X_ListaAcciones] A" +
                                " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] PRO ON LTRIM(A.AccionProducto) = CAST(PRO.Referencia AS varchar)" +
                                " WHERE A.Eliminar = 0 and DesviacionEncontrada <> ''"+WHERE2+") A" +
                                " WHERE Id > 0" +WHERE, cnn_SMARTH);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                cnn_SMARTH.Close();
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                return dt;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_Lecciones_Aprendidas_DROPDOWNS(string SELECT)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT "+SELECT+"" +
                                " FROM(SELECT A.[Id], CASE WHEN A.[AccionMaquina] = '' THEN '-' ELSE A.[AccionMaquina] END AS[AccionMaquina]," +
                                " CASE WHEN A.OrigenCausa IS NULL OR A.OrigenCausa = 0 THEN 'Sin definir' WHEN A.OrigenCausa = 1 THEN 'Entorno' WHEN A.OrigenCausa = 2 THEN 'Máquina/Molde' WHEN A.OrigenCausa = 3 THEN 'Medida' WHEN A.OrigenCausa = 4 THEN 'Material' WHEN A.OrigenCausa = 5 THEN 'Método' WHEN A.OrigenCausa = 6 THEN 'Persona' END AS OrigenCausa," +
                                " CASE WHEN[Tipo] = 0 THEN '-' WHEN[Tipo] = 1 THEN 'CONTENCIÓN:' WHEN[Tipo] = 2 THEN 'DETECCIÓN' WHEN Tipo = 3 THEN 'OCURRENCIA' END AS Tipo," +
                                " CASE WHEN A.AccionProducto = '' THEN '-' ELSE LTRIM(A.AccionProducto) END as ProdMOLDE, DesviacionEncontrada, CausaRaiz, Accion," +
                                " CASE WHEN PRO.Cliente IS NULL THEN '-' ELSE PRO.Cliente END as Cliente," +
                                " CASE WHEN PRO.Molde IS NULL THEN '-' ELSE CAST(PRO.Molde AS varchar) END as Molde," +
                                " CASE WHEN PRO.Descripcion IS NULL THEN '-' ELSE PRO.Descripcion END as Descripcion," +
                                " CASE WHEN PRO.Sector IS NULL THEN '-' ELSE PRO.Sector END as Sector," +
                                " CASE WHEN PRO.Material IS NULL or PRO.MATERIAL = '' THEN '-' ELSE PRO.Material END as Material," +
                                " A.LeccionAprendida" +
                                " FROM[SMARTH_DB].[dbo].[PDCA_X_ListaAcciones] A" +
                                " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] PRO ON LTRIM(A.AccionProducto) = CAST(PRO.Referencia AS varchar)" +
                                " WHERE A.Eliminar = 0 and DesviacionEncontrada <> '') A" +
                                " WHERE Id > 0", cnn_SMARTH);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                cnn_SMARTH.Close();
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                return dt;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }
        //**************************************\\CONSULTAS GESTION DOCUMENTAL//**************************************\\
        //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\//\\
        public DataSet DatasetDetectadosGP12(string referencia)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT [FechaInicio],[Referencia],[ImagenDefecto1] FROM [SMARTH_DB].[dbo].[GP12_Historico] where ImagenDefecto1 <> 'http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg' AND Referencia = " + referencia + "" +
                   " UNION ALL SELECT [FechaInicio],[Referencia],[ImagenDefecto2] FROM [SMARTH_DB].[dbo].[GP12_Historico] where ImagenDefecto2 <> 'http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg' AND Referencia = " + referencia + "" +
                   " UNION ALL SELECT [FechaInicio],[Referencia],[ImagenDefecto3] FROM [SMARTH_DB].[dbo].[GP12_Historico] where ImagenDefecto3 <> 'http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg' AND Referencia = " + referencia + "" +
                   " UNION ALL SELECT [FechaInicio],[Referencia],[ImagenDefecto1] FROM [SMARTH_DB].[dbo].[GP12_Historico_2021] where ImagenDefecto1 <> 'http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg' AND Referencia = " + referencia + "" +
                   " UNION ALL SELECT [FechaInicio],[Referencia],[ImagenDefecto2] FROM [SMARTH_DB].[dbo].[GP12_Historico_2021] where ImagenDefecto2 <> 'http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg' AND Referencia = " + referencia + "" +
                   " UNION ALL SELECT [FechaInicio],[Referencia],[ImagenDefecto3] FROM [SMARTH_DB].[dbo].[GP12_Historico_2021] where ImagenDefecto3 <> 'http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg' AND Referencia = " + referencia + "" +
                   " ORDER BY FechaInicio desc";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }  //CAMBIAR CONSULTA A HISTORICO

        public DataSet Devuelve_dataset_filtroreferenciasSMARTH(string referencia1)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT P.[Referencia] as REF,P.[Molde],[Descripcion],[PlanControl],[PautaControl],[PautaRecepcion1],[PautaRecepcion2],[OperacionEstandar],[OperacionEstandar2],[Defoteca],[Embalaje],[Gp12],ImagenPieza,[PautaRetrabajo],[VideoAuxiliar], [Observaciones],[NoConformidades],P.Cliente as CLINO, IMG.Logotipo,[FechaModificacion] FROM [SMARTH_DB].[dbo].[AUX_TablaProductos] P LEFT JOIN [SMARTH_DB].[dbo].[DOCUMENTAL_Base_DocumentosXReferencias] R ON P.Referencia = R.Referencia LEFT JOIN SMARTH_DB.DBO.[AUX_Lista_Clientes] IMG ON P.Cliente = IMG.Cliente WHERE P.Referencia = '" + referencia1 + "' ORDER BY P.Referencia", cnn_SMARTH);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                cnn_SMARTH.Close();
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_operarios_bms(int operario)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT TRIM(C_ID) as NUMOPERARIO, TRIM(C_NAME) AS NOMBRE, TRIM(C_DEPARTMENT) AS EMPRESA, C_USERVALUE1 AS COSTE FROM PCMS.T_EMPLOYEES WHERE C_ID = '" + operario + "'";
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                //DataSet dsope = new DataSet();
                DataTable dsope = new DataTable();
                query.Fill(dsope);
                cnn_bms.Close();
                return dsope;
            }
            catch (Exception)
            {
                cnn_bms.Close();
                return null;
            }
        }

        //**************************************\\CONSULTAS AUXILIARES SMARTH//**************************************\\
        //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\//\\

        //SELECTORES DE OPERARIOS EN DROPDOWNS
        public DataTable Devuelve_log_in(string usuario, string pass)
        {
            try
                {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT [Nombre], Departamento, [IdToken]  FROM[SMARTH_DB].[dbo].[AUX_Personal_Mandos] Where Usuario = '" + usuario+"' and Pass = '"+pass+"'";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
                {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataSet Devuelve_mandos_intermedios_SMARTH()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                //string sql = "SELECT [Referencia],[Descripcion] FROM [SMARTH_DB].[dbo].[AUX_TablaProductos] where Molde = " + molde + " order by Referencia";
                string sql = "SELECT * FROM[SMARTH_DB].[dbo].[AUX_Personal_Mandos] where OPActivo = 1 order by Nombre asc";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        } 

        public string Devuelve_Pilotos_SMARTH(int id)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT [Id],[Nombre] FROM[SMARTH_DB].[dbo].[AUX_Personal_Mandos] WHERE Id = " + id;
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                if (ds.Tables[0].Rows[0]["Nombre"].ToString() != "")
                {
                    return ds.Tables[0].Rows[0]["Nombre"].ToString();
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            { 
                cnn_SMARTH.Close();
                return "";
            }
        } 

        public int Devuelve_ID_Piloto_SMARTH(string nombre)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT Id FROM [SMARTH_DB].[dbo].[AUX_Personal_Mandos] where [Nombre] = '" + nombre + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return Convert.ToInt16(ds.Tables[0].Rows[0]["Id"].ToString());
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return 0;
            }
        }

        public string Devuelve_Pilotos_TOKEN_SMARTH(string token)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT [Id],[Nombre] FROM[SMARTH_DB].[dbo].[AUX_Personal_Mandos] WHERE [IdToken] = " + token;
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                if (ds.Tables[0].Rows[0]["Nombre"].ToString() != "")
                {
                    return ds.Tables[0].Rows[0]["Nombre"].ToString();
                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return "";
            }
        }

        //SELECTORES DE LISTAS DE MOLDES
        public DataTable Devuelve_listado_OPERARIOS_SEPARADOR()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT CAST([Id] AS VARCHAR) + ' ¬ ' + [Operario] AS OPERARIO FROM[SMARTH_DB].[dbo].[AUX_TablaOperarios]";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_listado_Tipos_Material()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT DISTINCT [TipoMaterial] FROM[SMARTH_DB].[dbo].[AUX_Lista_Materiales] WHERE TipoMaterial IS NOT NULL ORDER BY TipoMaterial";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_listado_OPERARIOS_ACTIVOS()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT CAST([Id] AS VARCHAR) + ' ¬ ' + [Operario] AS OPERARIO FROM[SMARTH_DB].[dbo].[AUX_TablaOperarios] WHERE [OpActivo] = 1";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_listado_PRODUCTOS_SEPARADOR()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT cast([Molde] as varchar) + ' ¬ ' + cast([Referencia] as varchar) + ' ¬ ' + [Descripcion] + ' ¬ ' + RTRIM([Cliente])  as Producto FROM[SMARTH_DB].[dbo].[AUX_TablaProductos]";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_listado_FICHAS_PRODUCTOS_SEPARADOR()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT DISTINCT FIC.[Referencia], cast(FIC.[Referencia] as varchar) + ' ¬ ' + cast(PRD.Descripcion as varchar) + ' ¬ ' + RTRIM(PRD.[Cliente]) AS PRODUCTO"+
                              " FROM[SMARTH_DB].[dbo].[PARAMETROS_Ficha] FIC" +
                              " LEFT JOIN[SMARTH_DB].[dbo].[AUX_TablaProductos] PRD ON FIC.Referencia = PRD.Referencia";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_listado_FICHAS_MOLDES_SEPARADOR()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT DISTINCT FIC.CodMolde, cast(FIC.CodMolde as varchar) + ' ¬ ' + cast(PRD.Descripcion as varchar) AS PRODUCTO" +
                              " FROM[SMARTH_DB].[dbo].[PARAMETROS_Ficha] FIC" +
                              " LEFT JOIN[SMARTH_DB].[dbo].[AUX_Lista_Moldes] PRD ON FIC.CodMolde = PRD.ReferenciaMolde" +
                              " WHERE FIC.CodMolde LIKE '3%' OR FIC.CodMolde LIKE '9%'";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public string Devuelve_linea_PRODUCTOS_SEPARADOR(string producto)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT cast([Molde] as varchar) + ' ¬ ' + cast([Referencia] as varchar) + ' ¬ ' + RTRIM([Descripcion]) + ' ¬ ' + RTRIM([Cliente])  as Producto FROM[SMARTH_DB].[dbo].[AUX_TablaProductos] where Referencia = " + producto+"";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                if (ds.Tables[0].Rows[0]["Producto"].ToString() != "")
                {
                    return ds.Tables[0].Rows[0]["Producto"].ToString();
                }
                else
                {
                    return "";
                }

            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return "";
            }
        }

        public DataTable Devuelve_listado_PRODUCTOS()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT cast([Referencia] as varchar) + ' ¬ ' + [Descripcion] FROM[SMARTH_DB].[dbo].[AUX_TablaProductos]";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_listado_MOLDES()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT cast([ReferenciaMolde] as varchar) + ' ¬ ' +  [Descripcion] as Molde FROM [SMARTH_DB].[dbo].[AUX_Lista_Moldes]";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_listado_MOLDES_V2()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT DISTINCT CAST([ReferenciaMolde] AS VARCHAR) + ' ¬ ' + MOL.[Descripcion] +' ¬ '+ CASE WHEN PR.Cliente is null then '' else RTRIM(Pr.Cliente) end AS MOLDE FROM [SMARTH_DB].[dbo].[AUX_Lista_Moldes] MOL LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] PR ON MOL.ReferenciaMolde = PR.Molde ORDER BY MOLDE ASC", cnn_SMARTH);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                cnn_SMARTH.Close();
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                return dt;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_linea_MOLDES(string molde)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT DISTINCT CAST([ReferenciaMolde] AS VARCHAR) + ' ¬ ' + MOL.[Descripcion] +' ¬ '+ CASE WHEN PR.Cliente is null then '' else RTRIM(Pr.Cliente) end AS MOLDE  FROM [SMARTH_DB].[dbo].[AUX_Lista_Moldes] MOL LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] PR ON MOL.ReferenciaMolde = PR.Molde WHERE ReferenciaMolde = '" + molde+"' ORDER BY MOLDE ASC", cnn_SMARTH);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                cnn_SMARTH.Close();
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                return dt;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_Molde(string molde)
        {
            try 
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM [SMARTH_DB].[dbo].[AUX_Lista_Moldes] A LEFT JOIN[SMARTH_DB].[dbo].AUX_Lista_Moldes_Ubicaciones B ON A.Ubicacion = B.Ubicacion WHERE ReferenciaMolde = '" + molde + "'", cnn_SMARTH);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                cnn_SMARTH.Close();
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        

        public DataTable Devuelve_listado_MAQUINAS()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT * FROM[SMARTH_DB].[dbo].[AUX_Lista_Máquinas] WHERE Familia <> 0 ORDER BY Observaciones ASC";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public string Devuelve_ID_MAQUINAS(string MAQUINA)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT IdMaquinaCHAR FROM[SMARTH_DB].[dbo].[AUX_Lista_Máquinas] WHERE Maquina = '"+MAQUINA+ "' ORDER BY Observaciones ASC";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                if (ds.Tables[0].Rows[0]["IdMaquinaCHAR"].ToString() != "")
                {
                    return ds.Tables[0].Rows[0]["IdMaquinaCHAR"].ToString();
                }
                else
                {
                    return "";
                }

            }
            catch (Exception EX)
            {
                cnn_SMARTH.Close();
                return "";
            }
        }

        public string Devuelve_MAQUINAS_SELECCIONADA(string MAQUINA)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT * FROM[SMARTH_DB].[dbo].[AUX_Lista_Máquinas] WHERE IdMaquinaCHAR = '" + MAQUINA + "' ORDER BY Observaciones ASC";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                if (ds.Tables[0].Rows[0]["Maquina"].ToString() != "")
                {
                    return ds.Tables[0].Rows[0]["Maquina"].ToString();
                }
                else
                {
                    return "";
                }

            }
            catch (Exception EX)
            {
                cnn_SMARTH.Close();
                return "";
            }
        }

        public DataTable Devuelve_listado_clientes()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT RTRIM(Cliente) AS Cliente FROM [SMARTH_DB].[dbo].[AUX_TablaProductos] WHERE CLIENTE <> '' AND ISNUMERIC(Cliente) = 0 GROUP BY Cliente ORDER BY Cliente";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;

            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_listado_PERIFERICOS()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT [Máquina]"+
                                " FROM[SMARTH_DB].[dbo].[AUX_Lista_MaquinasPeriferico]"+
                                " ORDER BY Familia, Id";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public string Devuelve_ID_PERIFERICOS(string MAQUINA)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT TOP (1000) [Id],[Máquina],[Datos1],[Familia],[Vinculado],[IdThermo] FROM[SMARTH_DB].[dbo].[AUX_Lista_MaquinasPeriferico] WHERE Máquina = '"+MAQUINA+"'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    return ds.Tables[0].Rows[0]["Id"].ToString();
                }
                else
                {
                    return "";
                }

            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return "";
            }
        }
        //***************************************\\APP AREA DE RECHAZO//***************************************\\
        //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\//\\
        public DataTable Devuelve_Area_Rechazo(string where)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = " SELECT REC.[Id],[Referencia],[Motivo],PE1.Nombre as [ResponsableEntrada],[Cantidad],[FechaEntrada],CASE WHEN [FechaSalida] IS NULL THEN '1900-01-01' ELSE [FechaSalida] END AS FechaSalida,[DebeSalir],[Decision],PE2.Nombre as [ResponsableSalida],[Observaciones],[Eliminar]" +
                                " FROM[SMARTH_DB].[dbo].[CALIDAD_Area_Rechazo] REC"+
                                " LEFT JOIN SMARTH_DB.DBO.AUX_Personal_Mandos PE1 ON REC.ResponsableEntrada = PE1.Id"+
                                " LEFT JOIN SMARTH_DB.DBO.AUX_Personal_Mandos PE2 ON REC.ResponsableSalida = PE2.Id"+
                                " WHERE Eliminar = 0 "+where+"";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception EX)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_Productos_NAV_SEPARADOR()
        {
            try
            {
                cnn_NAV.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_NAV;
                string sql = "SELECT No_ + '¬' + Description as PRODUCTO" +
                                " FROM[NAVDB].[dbo].[THERMO$Item]" +
                                " WHERE([No_] LIKE  '1%' OR [No_] LIKE  '2%' OR No_ LIKE '3%' OR No_ LIKE '6%') AND[Inventory Posting Group] <> ''" +
                                " order by no_ asc";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_NAV);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_NAV.Close();
                return dt;
            }
            catch (Exception EX)
            {
                cnn_NAV.Close();
                return null;
            }
        }

        public DataTable Devuelve_Productos_NAV(string where)
        {
            try
            {
                cnn_NAV.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_NAV;
                string sql = "SELECT No_ as PRODUCTO,  LEFT(Description,30) as DESCRIPCION" +
                                " FROM[NAVDB].[dbo].[THERMO$Item]" +
                                " WHERE([No_] LIKE  '1%' OR [No_] LIKE  '2%' OR No_ LIKE '3%' OR No_ LIKE '6%') AND[Inventory Posting Group] <> '' " + where+"" +
                                " order by no_ asc";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_NAV);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_NAV.Close();
                return dt;
            }
            catch (Exception EX)
            {
                cnn_NAV.Close();
                return null;
            }
        }

        public void Insertar_area_rechazo(string referencia, string motivo, int responsableEntrada, int cantidad, string fechaEntrada, string debeSalir, int responsableSalida, string observaciones)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand { Connection = cnn_SMARTH };
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[CALIDAD_Area_Rechazo] (Referencia,Motivo,ResponsableEntrada,Cantidad,FechaEntrada,DebeSalir,ResponsableSalida,Observaciones, Eliminar) " +
                                    " VALUES ('" + referencia + "','" + motivo + "'," + responsableEntrada + "," + cantidad + ",'" + fechaEntrada + "', '" + debeSalir + "'," + responsableSalida + ",'" + observaciones + "',0)";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
            }
        }

        public void Actualizar_Area_Rechazo(Int32 id, string referencia, string motivo, int responsableEntrada,
                                          Int32 cantidad, string fechaEntrada, string debeSalir,
                                          string decision, int responsableSalida, string observaciones)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand { Connection = cnn_SMARTH };
                string sql = "UPDATE [SMARTH_DB].[dbo].[CALIDAD_Area_Rechazo] SET Referencia = '" + referencia + "', Motivo = '" + motivo + "', ResponsableEntrada = " + responsableEntrada + ", Cantidad = " + cantidad + ", FechaEntrada = '" + fechaEntrada + "', DebeSalir = '" + debeSalir + "', Decision = '" + decision + "', ResponsableSalida = " + responsableSalida + ", Observaciones = '" + observaciones + "' WHERE Id = " + id;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
            }
        }

        public void Eliminar_Area_Rechazo(Int32 id)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand { Connection = cnn_SMARTH };
                string sql = "UPDATE [SMARTH_DB].[dbo].[CALIDAD_Area_Rechazo] SET Eliminar = 1 WHERE Id = " + id; 
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
            }
        }

        public void Liberar_Producto_Area_Rechazo(Int32 id, int tipo)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand { Connection = cnn_SMARTH };
                string sql = "";
                if (tipo == 1)
                {
                    sql = "UPDATE [SMARTH_DB].[dbo].[CALIDAD_Area_Rechazo] SET FechaSalida = SYSDATETIME() WHERE Id = " + id;
                }
                else
                {
                    sql = "UPDATE [SMARTH_DB].[dbo].[CALIDAD_Area_Rechazo] SET FechaSalida = null WHERE Id = " + id;
                }

                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
            }
        }


        //***************************************\\APP MONTAJES//***************************************\\
        //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\//\\
        public void Insertar_Montaje(string fechainicio, string fechafin, double HorasMontaje, int ProveedorMontaje, int OperarioINT, string OperarioNombre, int Referencia, string Nombre, int Nlote, string NCaja, int Cantidad, double Coste, string Notas, int PiezasMalas)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand { Connection = cnn_SMARTH };
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[PRODUCCION_Montajes] ([FechaInicio],[FechaFin],[HorasMontaje],[ProveedorMontaje],[OperarioMontajeINT],[OperarioMontaje],[Referencia],[Nombre],[Nlote],[Ncaja],[PiezasMontadas],[CosteMontaje],[Notas],[PiezasMalas]) " +
                                    " VALUES ('" + fechainicio + "','" + fechafin + "'," + HorasMontaje.ToString().Replace(",", ".") + "," + ProveedorMontaje + "," + OperarioINT + ", '" + OperarioNombre + "'," + Referencia + ",'" + Nombre + "',"+Nlote+",'"+NCaja+"',"+Cantidad+", "+Coste.ToString().Replace(",",".")+",'"+Notas+"',"+ PiezasMalas + ")";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
            }
        }

        public DataTable Devuelve_Historico_Montajes(string where, string fechainicio, string fechafin)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT MON.[Id],[FechaInicio],[FechaFin],[HorasMontaje],PiezasMontadas / CASE WHEN HorasMontaje = 0 THEN 0.01 ELSE HorasMontaje END as PZHORA,PRO.Proveedor,[ProveedorMontaje],[OperarioMontajeINT],[OperarioMontaje],[Referencia],[Nombre],[Nlote],[Ncaja],[PiezasMontadas],[CosteMontaje],[Notas]" +
                             " FROM[SMARTH_DB].[dbo].[PRODUCCION_Montajes] MON" +
                             " LEFT JOIN [SMARTH_DB].[dbo].[GP12_ProveedoresRevision] PRO ON MON.ProveedorMontaje = PRO.Id" +
                             " WHERE MON.ID > 0 " + where + ""+fechainicio+""+fechafin+""+
                             " order by [FechaInicio] desc";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_GP12);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_GP12.Close();
                return dt;
            }
            catch (Exception EX)
            {
                cnn_GP12.Close();
                return null;
            }
        }


        //***************************************\\APP GESTIÓN DE MOLINOS//***************************************\\
        //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\//\\
        public DataTable Devuelve_Historico_Molidos(string where)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT MOL.Molino,' (' + MOL.Ubicacion + ')' AS UBICACION, MAT.[Referencia],MAT.Descripcion,[Cantidad],HIS.[Fecha]" +
                                " FROM[SMARTH_DB].[dbo].[RECICLADO_Historico_Molidos] HIS" +
                                " left join[SMARTH_DB].[dbo].[RECICLADO_Lista_Molinos] MOL ON HIS.Molino = MOL.Id" +
                                " left join SMARTH_DB.dbo.AUX_Lista_Materiales MAT on HIS.Referencia = MAT.Referencia" +
                                " where Cantidad > 0 "+where+""+
                                " order by his.Fecha desc";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_GP12);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_GP12.Close();
                return dt;
            }
            catch (Exception EX)
            {
                cnn_GP12.Close();
                return null;
            }
        }

        public DataTable Devuelve_Lista_Materiales_SEPARADOR()
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT CAST([Referencia] AS varchar) + ' ¬ '+ [Descripcion] AS Material FROM [SMARTH_DB].[dbo].[AUX_Lista_Materiales]";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_GP12);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_GP12.Close();
                return dt;
            }
            catch (Exception EX)
            {
                cnn_GP12.Close();
                return null;
            }
        }

        public DataTable Devuelve_Materiales_Produciendo()
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT J.c_Machine_id AS MAQ, TRIM(M.C_ID) AS MATERIAL, RPAD(M.C_LONG_DESCR, 25) AS DESCRIPCION" +
                              " FROM PCMS.t_Jobs J, PCMS.T_RECIPE_X_MATERIAL R, PCMS.T_MATERIALS M" +
                              " WHERE J.C_SEQNR = 0 AND R.C_MATERIAL_ID = M.C_ID AND R.C_RECIPE_ID = J.c_Product_id AND((M.C_MATTYPE_ID = '15') OR(M.C_MATTYPE_ID = '215') OR(M.C_MATTYPE_ID = '155')) AND J.C_MACHINE_ID <> 'FOAM' AND J.C_MACHINE_ID <> 'MONT' AND J.C_MACHINE_ID <> 'RTRB'" +
                              " GROUP BY J.c_Machine_id, TRIM(M.C_ID), M.C_LONG_DESCR ORDER BY J.c_Machine_id ASC";
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataTable dsope = new DataTable();
                query.Fill(dsope);
                cnn_bms.Close();
                return dsope;
            }
            catch (Exception)
            {
                cnn_bms.Close();
                return null;
            }
        }

        public DataTable Devuelve_ReferenciaXMolino()
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT CASE WHEN [ReferenciaActiva] IS NULL THEN CAST('0' AS varchar) ELSE Cast([ReferenciaActiva] as varchar) END AS ReferenciaMOL ,[Molino] FROM[SMARTH_DB].[dbo].[RECICLADO_Lista_Molinos]";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_GP12);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_GP12.Close();
                return dt;
            }
            catch (Exception)
            {
                cnn_GP12.Close();
                return null;
            }
        }

        public DataTable Devuelve_DatosMolino()
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT * FROM [SMARTH_DB].[dbo].[RECICLADO_Lista_Molinos] MOL left join [SMARTH_DB].[dbo].AUX_Lista_Materiales MAT ON MOL.ReferenciaActiva = MAT.Referencia";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_GP12);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_GP12.Close();
                return dt;
            }
            catch (Exception EX)
            {
                cnn_GP12.Close();
                return null;
            }
        }

        public DataTable Devuelve_ResumenMolidosMES(string where)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT MES, HIS.Referencia, MAT.Descripcion, CANTIDAD" +
                                " FROM (SELECT year([Fecha]) AS YEAR, month([Fecha]) AS MESNUM, datename(month, [Fecha]) as MES,[Referencia],SUM(cast([Cantidad] as float)) AS CANTIDAD FROM[SMARTH_DB].[dbo].[RECICLADO_Historico_Molidos]" +
                                " GROUP BY year([Fecha]), month([Fecha]), datename(month, [Fecha]),[Referencia],Molino) HIS" +
                                " LEFT JOIN[SMARTH_DB].[dbo].Aux_Lista_Materiales MAT ON HIS.Referencia = MAT.Referencia" +
                                " where MESNUM > 0 " + where + " ORDER BY MESNUM, HIS.Referencia";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_GP12);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_GP12.Close();
                return dt;
            }
            catch (Exception EX)
            {
                cnn_GP12.Close();
                return null;
            }
        }

        public DataTable Devuelve_ResumenMolinos(string where)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT datename(MONTH, Fecha) AS MES, MONTH(FECHA) as MESNUM, Molino, sum(Cantidad) AS Cantidad"+
                             " FROM(SELECT MOL.Molino, ' (' + MOL.Ubicacion + ')' AS UBICACION, MAT.[Referencia], MAT.Descripcion,[Cantidad], HIS.[Fecha]" +
                                 " FROM[SMARTH_DB].[dbo].[RECICLADO_Historico_Molidos] HIS" +
                                 " left join[SMARTH_DB].[dbo].[RECICLADO_Lista_Molinos] MOL ON HIS.Molino = MOL.Id" +
                                 " left join SMARTH_DB.dbo.AUX_Lista_Materiales MAT on HIS.Referencia = MAT.Referencia)MOL" +
                                 " WHERE Cantidad > 0 "+where+"" +
                                 " GROUP BY datename(MONTH, Fecha), MONTH(FECHA), Molino" +
                                 " order by MONTH(FECHA) asc, Cantidad asc";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_GP12);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_GP12.Close();
                return dt;
            }
            catch (Exception EX)
            {
                cnn_GP12.Close();
                return null;
            }
        }

        public string Devuelve_MateriaPrima(string material)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT * FROM [SMARTH_DB].[dbo].[AUX_Lista_Materiales] where Referencia = '" + material + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_GP12.Close();
                if (ds.Tables[0].Rows[0]["Descripcion"].ToString() != "")
                {
                    return ds.Tables[0].Rows[0]["Descripcion"].ToString();
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                cnn_GP12.Close();
                return "";
            }
        }

        public string Devuelve_Valida_MateriaPrima(string material)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT CAST([Referencia] AS varchar) Material FROM[SMARTH_DB].[dbo].[AUX_Lista_Materiales] where[Referencia] = " + material + "";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_GP12.Close();
                if (ds.Tables[0].Rows[0]["Material"].ToString() != "")
                {
                    return ds.Tables[0].Rows[0]["Material"].ToString();
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                cnn_GP12.Close();
                return "0";
            }
        }

        public string Devuelve_Material_Moler(string material)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT CASE WHEN [ReferenciaReciclado] = 0 THEN Referencia ELSE [ReferenciaReciclado] END AS MOLER FROM[SMARTH_DB].[dbo].[AUX_Lista_Materiales] where Referencia = " + material + "";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_GP12.Close();
                if (ds.Tables[0].Rows[0]["MOLER"].ToString() != "")
                {
                    return ds.Tables[0].Rows[0]["MOLER"].ToString();
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                cnn_GP12.Close();
                return "0";
            }
        }

        public bool Insertar_Linea_Molido(int molino, string referencia, string cantidad, string fecha)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql2 = "INSERT INTO [SMARTH_DB].[dbo].[RECICLADO_Historico_Molidos] ([Molino],[Referencia],[Cantidad],[Fecha],[Secuencia],[Activo]) VALUES " +
                                                                           "('" + molino + "'," + referencia + "," + cantidad.ToString().Replace(',', '.') + ",'" + fecha + "', 0, 1)";
                cmd.CommandText = sql2;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();
                return true;
            }
            catch (Exception ex)
            {
                cnn_GP12.Close();
                return false;
            }
        }

        public bool Generar_Linea_Molido()
        {
            try
            {
                int numentradas = 200;
                int molino = 7;
                string referencia = "20880013";
                string cantidad = "15";
                string fecha = DateTime.Now.ToString();

                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;

                for (int i = 0; i <= numentradas; i++)
                {
                    string sql2 = "INSERT INTO [SMARTH_DB].[dbo].[RECICLADO_Historico_Molidos] ([Molino],[Referencia],[Cantidad],[Fecha],[Secuencia],[Activo]) VALUES " +
                                                                           "('" + molino + "'," + referencia + "," + cantidad.ToString().Replace(',', '.') + ",'" + fecha + "', 0, 1)";
                    cmd.CommandText = sql2;
                    cmd.ExecuteNonQuery();
                }
                cnn_GP12.Close();
                return true;
            }
            catch (Exception ex)
            {
                cnn_GP12.Close();
                return false;
            }
        }

        public void Actualiza_Material_Activo(string id, string referencia)

        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "  UPDATE [SMARTH_DB].[dbo].[RECICLADO_Lista_Molinos] SET ReferenciaActiva = " + referencia + " where Id = " + id + "";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();
            }
            catch (Exception ex)
            {
                cnn_GP12.Close();
            }

        }

        //***************************************\\APP MATERIALES//***************************************\\
        //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\//\\


        public DataSet DevuelveEstructuraMaquina(string maquina)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT J.c_Machine_id AS MAQ, J.C_PRODLONGDESCR, J.C_PRODUCT_ID AS PRODUCTO, J.c_Id AS ORDEN, TRIM(M.C_ID) AS MATERIAL, RPAD(M.C_LONG_DESCR, 25) AS DESCRIPCION, RPAD(M.C_SHORT_DESCR, 16) AS UBICACION, ROUND((((R.C_UNITS * J.C_QTYREQUIRED) * 1.05) + 5), 2)  AS CONSUMOORDEN, M.C_REMARKS AS NOTAS" +
                                " FROM PCMS.t_Jobs J, PCMS.T_RECIPE_X_MATERIAL R, PCMS.T_MATERIALS M " +
                                " WHERE J.C_SEQNR = 0 AND J.c_Machine_id = '" + maquina + "' AND R.C_MATERIAL_ID = M.C_ID AND R.C_RECIPE_ID = J.c_Product_id" +
                                " ORDER BY J.C_PRODUCT_ID ASC";
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataSet ds = new DataSet();
                query.Fill(ds);
                cnn_bms.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_bms.Close();
                return null;
            }

        }

        public DataSet DevuelveEstructuraMaquinaAgrupada(string maquina)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT TRIM(M.C_ID) AS MATERIAL, RPAD(M.C_LONG_DESCR, 25) AS DESCRIPCION, RPAD(M.C_SHORT_DESCR, 16) AS UBICACION, ROUND(SUM((((R.C_UNITS * J.C_QTYREQUIRED) * 1.05) + 5)), 2)  AS CONSUMOORDEN,  M.C_REMARKS AS NOTAS" +
                                " FROM PCMS.t_Jobs J, PCMS.T_RECIPE_X_MATERIAL R, PCMS.T_MATERIALS M" +
                                " WHERE J.C_SEQNR = 0 AND J.c_Machine_id = '" + maquina + "' AND R.C_MATERIAL_ID = M.C_ID AND R.C_RECIPE_ID = J.c_Product_id" +
                                " GROUP BY M.C_ID, M.C_LONG_DESCR, M.C_SHORT_DESCR, M.C_REMARKS ORDER BY M.C_ID ASC";
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataSet ds = new DataSet();
                query.Fill(ds);
                cnn_bms.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_bms.Close();
                return null;
            }

        }

        public DataSet DevuelveEstructuraProducto(string producto)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT TRIM(M.C_ID) AS MATERIAL, RPAD(M.C_LONG_DESCR, 25) AS DESCRIPCION, RPAD(M.C_SHORT_DESCR, 16) AS UBICACION, R.C_UNITS  AS CONSUMOUNIDAD, M.C_REMARKS AS NOTAS" +
                                 " FROM PCMS.T_RECIPE_X_MATERIAL R" +
                                 " LEFT JOIN PCMS.T_MATERIALS M ON R.C_MATERIAL_ID = M.C_ID" +
                                 " WHERE R.C_RECIPE_ID = '" + producto + "'" +
                                 " ORDER BY M.C_ID ASC";
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataSet ds = new DataSet();
                query.Fill(ds);
                cnn_bms.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_bms.Close();
                return null;
            }

        }

        public DataSet DevuelveEstructuraOrden(string orden)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT J.c_Machine_id AS MAQ, J.C_PRODLONGDESCR, J.C_PRODUCT_ID AS PRODUCTO, J.c_Id AS ORDEN, TRIM(M.C_ID) AS MATERIAL, RPAD(M.C_LONG_DESCR, 25) AS DESCRIPCION, RPAD(M.C_SHORT_DESCR, 16) AS UBICACION, ROUND((((R.C_UNITS * J.C_QTYREQUIRED) * 1.05) + 5), 2)  AS CONSUMOORDEN, M.C_REMARKS AS NOTAS" +
                                " FROM PCMS.t_Jobs J, PCMS.T_RECIPE_X_MATERIAL R, PCMS.T_MATERIALS M " +
                                " WHERE J.C_ID = '" + orden + "' AND R.C_MATERIAL_ID = M.C_ID AND R.C_RECIPE_ID = J.c_Product_id" +
                                " ORDER BY J.C_PRODUCT_ID ASC";
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataSet ds = new DataSet();
                query.Fill(ds);
                cnn_bms.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_bms.Close();
                return null;
            }

        }

        public DataSet DevuelveEstructuraOrdenAgrupada(string seqnr, string maquina)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT TRIM(M.C_ID) AS MATERIAL, RPAD(M.C_LONG_DESCR, 25) AS DESCRIPCION, RPAD(M.C_SHORT_DESCR, 16) AS UBICACION, ROUND(SUM((((R.C_UNITS * J.C_QTYREQUIRED) * 1.05) + 5)), 2)  AS CONSUMOORDEN,  M.C_REMARKS AS NOTAS" +
                                " FROM PCMS.t_Jobs J, PCMS.T_RECIPE_X_MATERIAL R, PCMS.T_MATERIALS M" +
                                " WHERE J.C_SEQNR = '"+seqnr+"' AND J.c_Machine_id = '" + maquina + "' AND R.C_MATERIAL_ID = M.C_ID AND R.C_RECIPE_ID = J.c_Product_id" +
                                " GROUP BY M.C_ID, M.C_LONG_DESCR, M.C_SHORT_DESCR, M.C_REMARKS ORDER BY M.C_ID ASC";
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataSet ds = new DataSet();
                query.Fill(ds);
                cnn_bms.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_bms.Close();
                return null;
            }

        }

        public DataTable Devuelve_Entradas_Previstas_Materiales()
        {
            try
            {
                cnn_NAV.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_NAV;
                string sql = "SELECT DAT.No_, DAT.FECHA,IT.Description, CAN.Quantity" +
                                " FROM(SELECT[No_], MIN([Expected Receipt Date]) AS FECHA" +
                                    " FROM[NAVDB].[dbo].[THERMO$Purchase Line]" +
                                    " WHERE[Document No_] LIKE 'PC23%' AND[Outstanding Amount] > 0 AND[Posting Group] = 'MAT. PRIMA' AND[Line No_] LIKE '1%' GROUP BY No_) DAT" +
                                " LEFT JOIN[NAVDB].[dbo].[THERMO$Purchase Line] CAN ON DAT.No_ = CAN.No_ AND DAT.FECHA = CAN.[Expected Receipt Date]" +
                                " LEFT JOIN[NAVDB].[dbo].[THERMO$Item] IT ON DAT.No_ = IT.No_" +
                                " order by FECHA asc";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_NAV);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_NAV.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_NAV.Close();
                return null;
            }
        }



        //***************************************\\APP UBICACIONES MOLDES//***************************************\\
        //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\//\\
        public string Devuelve_Ubicacion_Moldes(string molde)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT UBI.Ubicacion + ' (' + UBI.Zona + ' ' + UBI.Nave + ')' AS UBICACION" +
                              " FROM[SMARTH_DB].[dbo].[AUX_Lista_Moldes] MOL" +
                              " LEFT JOIN[SMARTH_DB].[dbo].[AUX_Lista_Moldes_Ubicaciones] UBI ON MOL.Ubicacion = UBI.Ubicacion" +
                              " WHERE MOL.ReferenciaMolde = "+molde+"";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt.Rows[0]["UBICACION"].ToString();

            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return "Sin ubicación";
            }
        }


        public DataTable Devuelve_listado_MoldesLineaUbicacion()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT CAST([ReferenciaMolde] AS varchar) +' ¬ ' + [Descripcion] + ' ¬ ' + [Ubicacion] AS MOLDE" +
                             " FROM[SMARTH_DB].[dbo].[AUX_Lista_Moldes] WHERE ReferenciaMolde <> '0' ORDER BY ReferenciaMolde asc";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;

            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_Listado_Ubicaciones_Molde_Dropdown()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM[SMARTH_DB].[dbo].[AUX_Lista_Moldes_Ubicaciones] where Activo = 1 and Ubicacion<>'Sin ubicacion' and Ubicacion <> 'Externo' Order by Ubicacion", cnn_SMARTH);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                cnn_SMARTH.Close();
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }
        public DataTable Devuelve_listado_Ubicaciones(string ubicacion, string letra)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT [Id], A.[Ubicacion],B.ReferenciaMolde, CAST(B.ReferenciaMolde AS varchar) + '_' + B.Descripcion AS MOLDE"+
                              " FROM[SMARTH_DB].[dbo].[AUX_Lista_Moldes_Ubicaciones] A LEFT JOIN[SMARTH_DB].[dbo].[AUX_Lista_Moldes] B ON A.Ubicacion = B.Ubicacion"+
                              " where zona = '"+ubicacion+"' AND A.Ubicacion LIKE '"+letra+"%' ORDER BY A.Ubicacion ASC";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;

            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_listado_Ubicaciones_Moldes()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT [Id], A.[Ubicacion],B.ReferenciaMolde, CAST(B.ReferenciaMolde AS varchar) + ' ' + B.Descripcion AS MOLDE, Zona, FechaUltimaProduccion, Nave, B.Activo " +
                              " FROM[SMARTH_DB].[dbo].[AUX_Lista_Moldes_Ubicaciones] A LEFT JOIN[SMARTH_DB].[dbo].[AUX_Lista_Moldes] B ON A.Ubicacion = B.Ubicacion" +
                              " ORDER BY A.Ubicacion ASC";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;

            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_listado_Moldes_X_Ubicacion(string ubicacion)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT[Id], A.[Ubicacion],B.ReferenciaMolde, CAST(B.ReferenciaMolde AS varchar) + ' ' + B.Descripcion AS MOLDE, B.ReferenciaMolde as Molnum,  Zona, FechaModificaUbicacion" +
                               " FROM[SMARTH_DB].[dbo].[AUX_Lista_Moldes_Ubicaciones] A LEFT JOIN[SMARTH_DB].[dbo].[AUX_Lista_Moldes] B ON A.Ubicacion = B.Ubicacion"+
                               " where A.Ubicacion like '"+ubicacion+"%' ORDER BY A.Ubicacion ASC";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;

            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public bool ActualizarUbicacionMolde(string molde, string ubicacion, string activo)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "";
                if (molde != "0")
                { 
                sql = "UPDATE [SMARTH_DB].[dbo].[AUX_Lista_Moldes] SET Ubicacion = '"+ubicacion+ "', FechaModificaUbicacion = GETDATE() " + activo+" where ReferenciaMolde = '"+molde+"'";
                }
    
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }

        public bool ActualizarMoldeXMano(string molde, int mano)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "";
                if (mano > 0)
                {
                    sql = "UPDATE [SMARTH_DB].[dbo].[AUX_Lista_Moldes] SET [Mano] = '" + mano + "' where ReferenciaMolde = '" + molde + "'";
                }
                else
                {
                    sql = "UPDATE [SMARTH_DB].[dbo].[AUX_Lista_Moldes] SET [Mano] = NULL where ReferenciaMolde = '" + molde + "'";
                }

                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }

        //***************************************\\APP MANOS//***************************************\\
        //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\//\\
        public DataTable Devuelve_listado_MANOSOLD(string where, string orderby)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT CAST([MANO] as varchar) + '_' + DESCRIPCION AS FILTRO, [AREA],MAN.[UBICACION],MAN.[MANO],MAN.Descripcion,[NOTA],[IMAGENES] FROM[SMARTH_DB].[dbo].[AUX_Lista_Manos_Robot] MAN WHERE MAN.MANO > 0 " + where+""+orderby+""; 
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;

            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_listado_MANOS_SMARTH(string where, string orderby)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT CAST(MAN.[MANO] as varchar) + '_' + DESCRIPCION AS FILTRO, [AREA],MAN.[UBICACION],MAN.[MANO],MAN.Descripcion,[NOTA],[IMAGENES], LOGOTIPO, Molde, CASE WHEN NUMOL IS NULL THEN 0 ELSE NUMOL END AS NUMOL" + 
                                " FROM[SMARTH_DB].[dbo].[AUX_Lista_Manos_Robot] MAN"+
                                " LEFT JOIN(SELECT DISTINCT[Molde], PR.[Cliente], MOL.Mano"+
                                    ", CASE WHEN CL.Logotipo IS NULL THEN 'http://facts4-srv/thermogestion/smarth_docs/clientes/sin_imagen.jpg' ELSE CL.Logotipo END AS LOGOTIPO, NUMOL" +
                                  " FROM[SMARTH_DB].[dbo].[AUX_TablaProductos] PR"+
                                  " LEFT JOIN[SMARTH_DB].[dbo].[AUX_Lista_Clientes] CL ON PR.Cliente = CL.Cliente" +
                                  " LEFT JOIN (SELECT MANO AS MANO, MIN (ReferenciaMolde) AS ReferenciaMolde, COUNT(ReferenciaMolde) as NUMOL FROM [SMARTH_DB].DBO.AUX_Lista_Moldes MOL WHERE MANO IS NOT NULL GROUP BY MANO) MOL ON PR.Molde = MOL.ReferenciaMolde WHERE Mano IS NOT NULL) MLO ON MAN.MANO = MLO.Mano" +
                                " WHERE ELIMINAR = 'FALSE'" + where + "" + orderby + "";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;

            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_listado_UBICACIONES_MANOS()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT DISTINCT [UBICACION] FROM[SMARTH_DB].[dbo].[AUX_Lista_Manos_Robot] ORDER BY UBICACION";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;

            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_listado_Moldes_X_MANO(string mano)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT TOP (1000) [ReferenciaMolde],MOL.[Descripcion],[Cavidades],MOL.[Ubicacion],MOL.[Mano],[Activo],[UbicacionExterna],[FechaUltimaProduccion],[FechaModificaUbicacion],Man.DESCRIPCION as MANDESCRIPCION" +
                              " FROM[SMARTH_DB].[dbo].[AUX_Lista_Moldes] MOL" +
                                " left join[SMARTH_DB].[dbo].[AUX_Lista_Manos_Robot] MAN ON MOL.Mano = MAN.MANO" +
                                " WHERE MOL.Mano = '"+mano+"'";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;

            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_listado_Manos_X_Molde(string molde)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT TOP (1000) [ReferenciaMolde],MOL.[Descripcion],[Cavidades],MOL.[Ubicacion],MOL.[Mano],[Activo],[UbicacionExterna],[FechaUltimaProduccion],[FechaModificaUbicacion],Man.DESCRIPCION as MANDESCRIPCION, man.UBICACION as UBIMANO, man.AREA as AREAMANO" +
                              " FROM[SMARTH_DB].[dbo].[AUX_Lista_Moldes] MOL" +
                                " left join[SMARTH_DB].[dbo].[AUX_Lista_Manos_Robot] MAN ON MOL.Mano = MAN.MANO" +
                                " WHERE [ReferenciaMolde] = '" + molde + "'";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;

            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public void Obsoletar_Mano(string Mano)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "UPDATE [SMARTH_DB].[dbo].[AUX_Lista_Manos_Robot] SET AREA = 1 WHERE [MANO] = "+Mano+" ";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();

            }
            catch (Exception)
            {

            }
        }

        public void Eliminar_Mano(string Mano)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "UPDATE [SMARTH_DB].[dbo].[AUX_Lista_Manos_Robot] SET ELIMINAR = 1 WHERE [MANO] = " + Mano + " ";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();

            }
            catch (Exception)
            {

            }
        }

        public void Insertar_Mano(string Mano, string descripcion, string ubicacion, int area, string nota)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[AUX_Lista_Manos_Robot]([AREA],[UBICACION],[MANO],[DESCRIPCION],[NOTA],[IMAGENES],[ELIMINAR])"+
                                                                          " VALUES("+area+",'"+ubicacion+"',"+Mano+",'"+descripcion+"','"+nota+"','',0)";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();

            }
            catch (Exception EX)
            {
                cnn_SMARTH.Close();
            }
        }

        public void Actualizar_Mano(string Mano, string descripcion, string ubicacion, int area, string nota)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "Update [SMARTH_DB].[dbo].[AUX_Lista_Manos_Robot] Set AREA = "+area+", DESCRIPCION = '"+descripcion+"', UBICACION = '"+ubicacion+"', NOTA = '"+nota+"' WHERE MANO = "+Mano+"";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();

            }
            catch (Exception)
            {

            }
        }

        public string Devuelve_Max_Mano()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT max([MANO])+1 as MANO FROM[SMARTH_DB].[dbo].[AUX_Lista_Manos_Robot]";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return ds.Tables[0].Rows[0]["MANO"].ToString();
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return "";
            }
        }

        public bool Existe_Mano(string mano)
        {
            try
            {
                cnn_SMARTH.Open();
                string sql = "SELECT MANO FROM[SMARTH_DB].[dbo].[AUX_Lista_Manos_Robot] where mano = '"+mano+"'";
                SqlCommand cmd = new SqlCommand(sql, cnn_SMARTH);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                if (dt.Rows.Count > 0)
                { return true; }
                else
                { return false; }
                
                
            }
            catch (Exception ex)
            {
                return true;
            }
            }

        //**********************************************\\APP PLANIFICACION//**********************************************\\
        //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\//\\
        ///////////\\\\\\\\\\\\\\\CONSULTA PESOS/ESTRUCTURA/////////////////\\\\\\\\\\\\\\\
        public bool Leer_PESOSNAV() 
        {
            try
            {
                cnn_SMARTH.Open();
                string sql1 = "DELETE FROM [SMARTH_DB].[dbo].[AUX_CONSULTA_PESOSNAV]";
                SqlCommand cmd = new SqlCommand(sql1, cnn_SMARTH);
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                cnn_NAV.Open();
                string sql = "SELECT M.[Parent Item No_] AS codsup, left(P.Description, 40) AS LONGDESC, CAV.[NEO Cavities],  SUM([Quantity per]) AS PESO"+
                                " FROM[NAVDB].[dbo].[THERMO$BOM Component] M" +
                                 " LEFT JOIN[NAVDB].[dbo].[THERMO$Item] P ON M.[Parent Item No_] = P.No_" +
                                 " LEFT JOIN[NAVDB].[dbo].[THERMO$Item$e8af12d5-328b-47d8-b2d8-158ecbed1ec3] REM ON M.[Parent Item No_] = REM.No_" +
                                 " LEFT JOIN[NAVDB].[dbo].[THERMO$Item] ITE ON M.[No_] = ITE.No_" +
                                 " LEFT JOIN[NAVDB].[dbo].[THERMO$Item$e8af12d5-328b-47d8-b2d8-158ecbed1ec3] R ON ITE.No_ = R.No_" +
                                 " LEFT JOIN[NAVDB].[dbo].[THERMO$BOM Component$e8af12d5-328b-47d8-b2d8-158ecbed1ec3] CAV ON M.[Parent Item No_] = CAV.[Parent Item No_] AND M.[Line No_] = CAV.[Line No_]" +
                                 " WHERE(ITE.[Item Category Code] = 15 OR ITE.[Item Category Code] = 150 OR ITE.[Item Category Code] = 215 OR ITE.[Item Category Code] = 155   OR ITE.[Item Category Code] = 216 OR ITE.[Item Category Code] = 23 OR ITE.[Item Category Code] = 221)" +
                                 " GROUP BY M.[Parent Item No_],  P.Description, CAV.[NEO Cavities] order by M.[Parent Item No_] desc";
                SqlCommand cmd2 = new SqlCommand(sql, cnn_NAV);

                using (SqlDataReader reader = cmd2.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Insertar_PESOSNAV(Convert.ToInt32(reader["codsup"]), reader["LONGDESC"].ToString(), reader["PESO"].ToString().Replace(',', '.'), reader["NEO Cavities"].ToString());
                    }
                }
                cnn_NAV.Close();
                return true;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                cnn_NAV.Close();
                return false;
            }
        }

        public bool Insertar_PESOSNAV(int producto, string descripcion, string cantidad, string cavidades)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[AUX_CONSULTA_PESOSNAV] ([Producto], [Descripcion], [Cantidad],[Cavidades] ) VALUES " +
                                 "(" + producto + ",'" + descripcion + "', " + cantidad + ", "+cavidades+")";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return false;
            }
        } 

        public bool Leer_PESOSBMS()
        {
            try
            {
                cnn_SMARTH.Open();
                string sql1 = "DELETE FROM [SMARTH_DB].[dbo].[AUX_CONSULTA_PESOSBMS]";
                SqlCommand cmd1 = new SqlCommand(sql1, cnn_SMARTH);
                cmd1.ExecuteNonQuery();
                cnn_SMARTH.Close();

                string sql = "SELECT PRD.C_NAME, LTRIM(PRD.C_DESCRIPTION, 40) AS C_DESCRIPTION, ROUND(AVG(SAM.C_XBAR), 5) AS PESO_G, ROUND(MAX(SAM.C_MAX), 5) AS MAX_PESO_G, ROUND(MIN(SAM.C_MIN), 5) AS MIN_PESO_G" +
                                " FROM SPC.T_INSPECTIONSAMPLES SAM" +
                                " LEFT JOIN SPC.T_INSPECTIONS INS ON SAM.C_INSPECTIONID = INS.C_ID" +
                                " LEFT JOIN SPC.T_INSPECTIONORDERS ORD ON INS.C_INSPECTIONORDERID = ORD.C_ID" +
                                " LEFT JOIN SPC.T_PRODUCTS PRD ON ORD.C_PRODUCTID = PRD.C_ID" +
                                " LEFT JOIN SPC.t_inspectioncharparams ICH ON SAM.C_INSPECTIONCHARPARAMID = ICH.C_ID" +
                                " LEFT JOIN SPC.t_characteristics CHA ON ICH.C_CHARACTERISTICID = CHA.C_ID" +
                                " WHERE C_ACTUALTIMESTAMP > SYSDATE - 120 AND C_MIN > 0  AND C_SIGMA IS NOT NULL AND C_UNITOFMEASURESYMBOL LIKE 'g%'" +
                                " GROUP BY ICH.C_CHARACTERISTICID, CHA.C_UNITOFMEASURESYMBOL, PRD.C_NAME, PRD.C_DESCRIPTION" +
                                " ORDER BY PRD.C_NAME ASC";

                OracleCommand cmd = new OracleCommand(sql, cnn_bms);
                cnn_bms.Open();

                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Insertar_PESOSBMS(Convert.ToInt32(reader["C_NAME"]), reader["C_DESCRIPTION"].ToString(), reader["PESO_G"].ToString().Replace(',', '.'), reader["MAX_PESO_G"].ToString().Replace(',', '.'),reader["MIN_PESO_G"].ToString().Replace(',', '.'));

                    }
                }
           
                cnn_bms.Close();
                return true;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                cnn_bms.Close();
                return false;
            }
        }

        public bool Insertar_PESOSBMS(int referencia, string descripcion, string peso, string pesomax, string pesomin)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[AUX_CONSULTA_PESOSBMS] ([Producto],[Descripcion],[Cantidad],[CantidadMIN],[CantidadMAX]) VALUES " +
                                 "(" + referencia + ",'" + descripcion + "'," + peso + "," + pesomin + "," + pesomax + ")";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return false;
            }
        } //CALIDAD - CAMBIAR A NUEVA BASE DE DATOS PRODUCTOS
        //DESDE SMARTH
        public DataTable Devuelve_Comparativa_Pesos(string WHERE)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT BMS.[Producto],BMS.[Descripcion],BMS.[Cantidad],BMS.[CantidadMIN],BMS.[CantidadMAX],NAV.Cantidad * 1000 AS CANTIDAD_NAV, COL.PESOCOLADA, NAV.Cavidades, round(((NAV.Cantidad*100000)/BMS.Cantidad)-100,2) AS PORCENTAJE" +
                              " FROM[SMARTH_DB].[dbo].[AUX_CONSULTA_PESOSBMS] BMS" +
                              " LEFT JOIN SMARTH_DB.DBO.AUX_CONSULTA_PESOSNAV NAV ON BMS.Producto = NAV.Producto" +
                              " LEFT JOIN[SMARTH_DB].[dbo].[AUX_TablaProductos] PRD ON BMS.Producto = PRD.Referencia" +
                              " LEFT JOIN(SELECT[CodMolde], max([PesoColada]) AS PESOCOLADA FROM [SMARTH_DB].[dbo].[PARAMETROS_Ficha] where PesoColada <> '' AND CodMolde<> '' group by CodMolde) COL ON PRD.Molde = COL.CodMolde" +
                              " WHERE BMS.DESCRIPCION <> ''" + WHERE+ " ORDER BY abs(round(((NAV.Cantidad*100000)/BMS.Cantidad)-100,2)) desc";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;

            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        //DESDE BMS
        public DataSet Devuelve_Pesos_QMASTER(string WHERE)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT PRD.C_NAME, PRD.C_DESCRIPTION, ROUND(AVG(SAM.C_XBAR), 2) AS PESO_G, ROUND(MAX(SAM.C_MAX), 2) AS MAX_PESO_G, ROUND(MIN(SAM.C_MIN), 2) AS MIN_PESO_G" +
                                " FROM SPC.T_INSPECTIONSAMPLES SAM" +
                                " LEFT JOIN SPC.T_INSPECTIONS INS ON SAM.C_INSPECTIONID = INS.C_ID" +
                                " LEFT JOIN SPC.T_INSPECTIONORDERS ORD ON INS.C_INSPECTIONORDERID = ORD.C_ID" +
                                " LEFT JOIN SPC.T_PRODUCTS PRD ON ORD.C_PRODUCTID = PRD.C_ID" +
                                " LEFT JOIN SPC.t_inspectioncharparams ICH ON SAM.C_INSPECTIONCHARPARAMID = ICH.C_ID" +
                                " LEFT JOIN SPC.t_characteristics CHA ON ICH.C_CHARACTERISTICID = CHA.C_ID" +
                                " WHERE C_ACTUALTIMESTAMP > SYSDATE - 120 AND C_MIN > 0  AND C_SIGMA IS NOT NULL AND C_UNITOFMEASURESYMBOL LIKE 'g%'"+ WHERE+"" +
                                " GROUP BY ICH.C_CHARACTERISTICID, CHA.C_UNITOFMEASURESYMBOL, PRD.C_NAME, PRD.C_DESCRIPTION"+
                                " ORDER BY PRD.C_NAME ASC";
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataSet dsprod = new DataSet();
                query.Fill(dsprod);
                cnn_bms.Close();
                return dsprod;
            }
            catch (Exception)
            {
                cnn_bms.Close();
                return null;
            }
        }

        public DataSet Devuelve_Listado_Productos_pesados_QMASTER()
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT PRD.C_NAME, MAX(PRD.C_NAME || '¬' || PRD.C_DESCRIPTION) AS PRODUCTO" +
                                " FROM SPC.T_INSPECTIONSAMPLES SAM" +
                                " LEFT JOIN SPC.T_INSPECTIONS INS ON SAM.C_INSPECTIONID = INS.C_ID" +
                                " LEFT JOIN SPC.T_INSPECTIONORDERS ORD ON INS.C_INSPECTIONORDERID = ORD.C_ID" +
                                " LEFT JOIN SPC.T_PRODUCTS PRD ON ORD.C_PRODUCTID = PRD.C_ID" +
                                " LEFT JOIN SPC.t_inspectioncharparams ICH ON SAM.C_INSPECTIONCHARPARAMID = ICH.C_ID" +
                                " LEFT JOIN SPC.t_characteristics CHA ON ICH.C_CHARACTERISTICID = CHA.C_ID" +
                                " WHERE C_ACTUALTIMESTAMP > SYSDATE - 120 AND C_SIGMA IS NOT NULL AND C_UNITOFMEASURESYMBOL LIKE 'g%'" +
                                " GROUP BY ICH.C_CHARACTERISTICID, CHA.C_UNITOFMEASURESYMBOL, PRD.C_NAME, PRD.C_DESCRIPTION" +
                                " ORDER BY PRD.C_NAME ASC";
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataSet dsprod = new DataSet();
                query.Fill(dsprod);
                cnn_bms.Close();
                return dsprod;
            }
            catch (Exception)
            {
                cnn_bms.Close();
                return null;
            }
        }

        ///////////\\\\\\\\\\\\\\\CAMBIOS DE MOLDE/////////////////\\\\\\\\\\\\\\\
        public DataSet PrevisionCambiosOrdenXMolde(string HORAINICIO, string HORAFIN)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT [Hora],[Maquina],[ACT_Orden],[NEXT_Orden],[ACT_Producto],[ACT_ProdDescript],[NEXT_Producto],[NEXT_ProdDescript],[ACT_CantPendiente],[ACT_MOLDE],[NEXT_MOLDE],[NEXT_ENTREGAPLANIFICADO],[NEXT_INICIOPLANIFICADO],[NEXT_RECETA],[SEQNR], CASE WHEN PRIORIDAD = '' THEN '-' ELSE PRIORIDAD END AS PRIORIDAD,cast([FINCALCULADO] as datetime) as FINCALCULADO,[FECHACAMBIOMAXIMO],[TIMETOGO],[REMARKS],MOL.Ubicacion AS UBICACION,MOL.Mano AS MANOROBOT,MAN.UBICACION AS UBIMANO," +
                            " CASE WHEN MAN.AREA = 1 THEN 'Obsoleto' WHEN MAN.AREA = 2 THEN 'Cuarto de manos' WHEN MAN.AREA = 3 THEN 'Máq. 34' WHEN MAN.AREA = 4 THEN 'Cub. estantería'  WHEN MAN.AREA = 5 THEN 'Máq. 32' WHEN MAN.AREA = 6 THEN 'Máq. 48' WHEN MAN.AREA = 7 THEN 'Máq. 43' ELSE '' END AS AREA" +
                            " FROM[SMARTH_DB].[dbo].[AUX_Planificacion_Prevision] PRV" +
                            " left join [SMARTH_DB].[dbo].[AUX_Lista_Moldes] MOL on PRV.NEXT_MOLDE = MOL.ReferenciaMolde" +
                            " left join SMARTH_DB.dbo.AUX_Lista_Manos_Robot MAN on MOL.Mano = MAN.MANO" +
                            " WHERE SEQNR >= 0 " + HORAINICIO + "" + HORAFIN + " ORDER BY FINCALCULADO ASC"; SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        } //revisa ubicacion va cambios

        //******************************************************\\KPI//************************************************\\
        //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\//\\

        ///////////\\\\\\\\\\\\\\\KPI PDCA/////////////////\\\\\\\\\\\\\\\
        public DataTable Devuelve_lista_acciones_pendientes_KPIPDCA(string WHERE)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT A.[FechaCierrePrev], O1.Nombre AS JEFE,O2.Nombre AS EJECUTA,P.IdPDCA as ID,CAST(P.IdPDCA AS VARCHAR) + '.' + CAST(A.IdReferencial AS VARCHAR) AS NUMACCION,LTRIM(A.AccionProducto) AS PRODUCTO,PROD.Descripcion,Desviacion AS PLANACCION,a.DesviacionEncontrada,A.ACCION AS REQUISITO" +
                               " FROM[SMARTH_DB].[dbo].[PDCA_Principal] P" +
                               " LEFT JOIN SMARTH_DB.dbo.PDCA_X_ListaAcciones A ON P.IdPDCA = A.IdPDCA" +
                               " LEFT JOIN SMARTH_DB.DBO.AUX_Personal_Mandos O1 ON P.Piloto = O1.Id" +
                               " LEFT JOIN SMARTH_DB.DBO.AUX_Personal_Mandos O2 ON A.Piloto = O2.Id" +
                               " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] PROD ON LTRIM(A.AccionProducto) = CAST(PROD.Referencia AS VARCHAR)" +
                               " WHERE A.FechaCierreReal IS NULL AND IdReferencial > 0  AND Eliminar <> 1" + WHERE+"" +
                               " ORDER BY O2.Nombre ASC";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;

            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_total_acciones_XOp_KPIPDCA(string WHERE)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT EJ.EJECUTA AS NOMBRE, EJ.ACCIONES AS ACCIONES, CASE WHEN DIR.ACCIONES IS NULL THEN 0 ELSE DIR.ACCIONES END AS PLANES"+
                                " FROM(SELECT CASE WHEN EJECUTA = '-' THEN 'Sin asignar' else EJECUTA end as EJECUTA, COUNT(PRODUCTO) AS ACCIONES" +
                                " FROM(SELECT O1.Nombre AS JEFE, O2.Nombre AS EJECUTA, P.IdPDCA as ID, CAST(P.IdPDCA AS VARCHAR) + '.' + CAST(A.IdReferencial AS VARCHAR) AS NUMACCION, LTRIM(A.AccionProducto) AS PRODUCTO, PROD.Descripcion, Desviacion AS PLANACCION, a.DesviacionEncontrada, A.ACCION AS REQUISITO" +
                                " FROM[SMARTH_DB].[dbo].[PDCA_Principal] P" +
                                " LEFT JOIN SMARTH_DB.dbo.PDCA_X_ListaAcciones A ON P.IdPDCA = A.IdPDCA" +
                                " LEFT JOIN SMARTH_DB.DBO.AUX_Personal_Mandos O1 ON P.Piloto = O1.Id" +
                                " LEFT JOIN SMARTH_DB.DBO.AUX_Personal_Mandos O2 ON A.Piloto = O2.Id" +
                                " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] PROD ON LTRIM(A.AccionProducto) = CAST(PROD.Referencia AS VARCHAR)" +
                                " WHERE IdReferencial > 0 AND Eliminar <> 1" + WHERE + ") A" +
                                " GROUP BY EJECUTA) EJ" +
                                " LEFT JOIN(SELECT CASE WHEN DIRIGE = '-' THEN 'Sin asignar' else DIRIGE end as DIRIGE, COUNT(PRODUCTO) AS ACCIONES" +
                                " FROM(SELECT O1.Nombre AS DIRIGE, O2.Nombre AS EJECUTA, P.IdPDCA as ID, CAST(P.IdPDCA AS VARCHAR) + '.' + CAST(A.IdReferencial AS VARCHAR) AS NUMACCION, LTRIM(A.AccionProducto) AS PRODUCTO, PROD.Descripcion, Desviacion AS PLANACCION, a.DesviacionEncontrada, A.ACCION AS REQUISITO" +
                                " FROM[SMARTH_DB].[dbo].[PDCA_Principal] P" +
                                " LEFT JOIN SMARTH_DB.dbo.PDCA_X_ListaAcciones A ON P.IdPDCA = A.IdPDCA" +
                                " LEFT JOIN SMARTH_DB.DBO.AUX_Personal_Mandos O1 ON P.Piloto = O1.Id" +
                                " LEFT JOIN SMARTH_DB.DBO.AUX_Personal_Mandos O2 ON A.Piloto = O2.Id" +
                                " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] PROD ON LTRIM(A.AccionProducto) = CAST(PROD.Referencia AS VARCHAR)" +
                                " WHERE IdReferencial > 0 AND Eliminar <> 1" + WHERE+") A" +
                                " GROUP BY DIRIGE) DIR ON EJ.EJECUTA = DIR.DIRIGE" +
                                " ORDER BY ACCIONES DESC";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;

            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_Resultados_MES_KPIPDCA(string WHERE)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = " SELECT * FROM(SELECT CASE WHEN AB.YEAR IS NULL THEN ITE.YEAR ELSE AB.YEAR END AS YEAR, CASE WHEN AB.MESAPERTURA IS NULL THEN ITE.MESAPERTURA ELSE AB.MESAPERTURA END AS MESAPERTURA, CASE WHEN AB.MESNUM IS NULL THEN ITE.MESNUM ELSE AB.MESNUM END AS MESNUM, ABIERTOS, CERRADOS, CASE WHEN REVISIONES IS NULL THEN 0 ELSE REVISIONES END AS ACTUALIZACIONES" +
                              " FROM(SELECT YEAR(FechaApertura) as YEAR, DATENAME(MONTH, (FechaApertura)) AS MESAPERTURA, MONTH(FechaApertura) AS MESNUM, COUNT([IdPDCA]) AS ABIERTOS FROM[SMARTH_DB].[dbo].[PDCA_X_ListaAcciones] GROUP BY YEAR(FechaApertura), DATENAME(MONTH, (FechaApertura)), MONTH(FechaApertura)) AB" +
                              " LEFT JOIN(SELECT DATENAME(MONTH, (FechaCierreReal)) AS MESAPERTURA, COUNT([IdPDCA]) AS CERRADOS FROM[SMARTH_DB].[dbo].[PDCA_X_ListaAcciones] GROUP BY YEAR(FechaApertura), DATENAME(MONTH, (FechaCierreReal)), MONTH(FechaCierreReal)) CR ON AB.MESAPERTURA = CR.MESAPERTURA" +
                              " FULL OUTER JOIN(SELECT YEAR(FechaApertura) AS YEAR, DATENAME(MONTH, (FechaApertura)) AS MESAPERTURA, MONTH(FechaApertura) AS MESNUM, COUNT([Revision]) AS REVISIONES FROM[SMARTH_DB].[dbo].[PDCA_X_Revisiones] GROUP BY YEAR(FechaApertura), DATENAME(MONTH, (FechaApertura)), MONTH(FechaApertura)) ITE ON AB.MESAPERTURA = ITE.MESAPERTURA) PDC" +
                              " WHERE YEAR = '"+WHERE+"'" +
                              " ORDER BY MESNUM ASC";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;

            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public string Devuelve_PDCAVencidos_KPIPDCA()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT COUNT ([FechaApertura]) AS VENCIDOS FROM[SMARTH_DB].[dbo].[PDCA_X_ListaAcciones] where FechaCierreReal IS NULL AND FechaCierrePrev<SYSDATETIME() AND IdReferencial > 0";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                if (ds.Tables[0].Rows[0]["VENCIDOS"].ToString() != "")
                {
                    return ds.Tables[0].Rows[0]["VENCIDOS"].ToString();
                }
                else
                {
                    return "";
                }

            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return "";
            }
        }

        public string Devuelve_PDCAPendientes_KPIPDCA()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT COUNT ([FechaApertura]) AS PENDIENTES FROM[SMARTH_DB].[dbo].[PDCA_X_ListaAcciones] where FechaCierreReal IS NULL AND IdReferencial > 0";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                if (ds.Tables[0].Rows[0]["PENDIENTES"].ToString() != "")
                {
                    return ds.Tables[0].Rows[0]["PENDIENTES"].ToString();
                }
                else
                {
                    return "";
                }

            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return "";
            }
        }

        public string Devuelve_PDCACerrados_KPIPDCA()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT COUNT ([FechaApertura]) AS CERRADOS FROM[SMARTH_DB].[dbo].[PDCA_X_ListaAcciones] where FechaCierreReal IS NOT NULL AND IdReferencial > 0";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                if (ds.Tables[0].Rows[0]["CERRADOS"].ToString() != "")
                {
                    return ds.Tables[0].Rows[0]["CERRADOS"].ToString();
                }
                else
                {
                    return "";
                }

            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return "";
            }
        }

        ///////////\\\\\\\\\\\\\\\KPI GP12/////////////////\\\\\\\\\\\\\\\
        public DataSet Devuelve_KPI_GP12_Mensual(string año, string DB)
        {
            try
            {
                string bd = "[SMARTH_DB].[dbo].[GP12_Historico_2021]";
                if (Convert.ToInt32(año) < 2021)
                {
                    bd = "[SMARTH_DB].[dbo].[GP12_Historico]";
                }
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT YEAR, DateName(month, DateAdd(month, S.MES, 0) - 1) AS 'MESTEXTO', HORAS, B.Cantidad, REVISADAS, (CAST(REVISADAS AS DECIMAL) / CAST(b.Cantidad AS DECIMAL)) AS PORCREVI, PIEZASOK, RETRABAJADAS, ((CAST(RETRABAJADAS AS decimal) / CAST(REVISADAS AS decimal)) * 1000000) AS PPMRETRABAJADAS, ((CAST(RETRABAJADAS AS decimal) / CAST(REVISADAS AS decimal))) AS PORCRETRABAJADAS, PIEZASNOK, ((CAST(PIEZASNOK AS decimal) / CAST(REVISADAS AS decimal)) * 1000000) AS PPMNOK, PIEZASNOK, ((CAST(PIEZASNOK AS decimal) / CAST(REVISADAS AS decimal))) AS PORCNOK, COSTEHORASREVISION, COSTESCRAP, COSTETOTAL" +
                            " FROM(SELECT YEAR([FechaInicio]) as YEAR, month([FechaInicio]) as Mes, SUM([HorasInspeccion]) AS HORAS, SUM([PiezasRevisadas]) AS REVISADAS, SUM([PiezasOK]) AS PIEZASOK, SUM([Retrabajadas]) AS RETRABAJADAS, SUM([PiezasNOK]) AS PIEZASNOK, SUM([CostePiezaRevision]) AS COSTEHORASREVISION, SUM([CosteScrapRevision]) AS COSTESCRAP, SUM([CosteRevision]) AS COSTETOTAL" +
                            " FROM "+bd+" group by YEAR([FechaInicio]), month([FechaInicio]) ) S" +
                            " LEFT JOIN[SMARTH_DB].[dbo].[AUX_BMSHistoricoProducidas] B ON S.Mes = B.Mes AND B.Periodo = '" + año + "'" +
                            " WHERE YEAR = '" + año + "' ORDER BY S.MES", cnn_SMARTH);

                cnn_SMARTH.Open();
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                cnn_SMARTH.Close();
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }   
        
        public string Devuelve_KPI_GP12_Mensual_CHARTPERC(string año, string DB)
        {
            try
            {
                string bd = "[SMARTH_DB].[dbo].[GP12_Historico_2021]";
                if (Convert.ToInt32(año) < 2021)
                {
                    bd = "[SMARTH_DB].[dbo].[GP12_Historico]";
                }

                string query = string.Format("SELECT YEAR, DateName(month, DateAdd(month, S.MES, 0) - 1) AS 'MESTEXTO', HORAS, B.Cantidad, REVISADAS, ((CAST(REVISADAS AS DECIMAL) / CAST(b.Cantidad AS DECIMAL))*100) AS PORCREVI, PIEZASOK, RETRABAJADAS, ((CAST(RETRABAJADAS AS decimal) / CAST(REVISADAS AS decimal)) * 1000000) AS PPMRETRABAJADAS, (((CAST(RETRABAJADAS AS decimal) / CAST(REVISADAS AS decimal)))*100) AS PORCRETRABAJADAS, PIEZASNOK, ((CAST(PIEZASNOK AS decimal) / CAST(REVISADAS AS decimal)) * 1000000) AS PPMNOK, PIEZASNOK, (((CAST(PIEZASNOK AS decimal) / CAST(REVISADAS AS decimal)))*100) AS PORCNOK, COSTEHORASREVISION, COSTESCRAP, COSTETOTAL" +
                            " FROM(SELECT YEAR([FechaInicio]) as YEAR, month([FechaInicio]) as Mes, SUM([HorasInspeccion]) AS HORAS, SUM([PiezasRevisadas]) AS REVISADAS, SUM([PiezasOK]) AS PIEZASOK, SUM([Retrabajadas]) AS RETRABAJADAS, SUM([PiezasNOK]) AS PIEZASNOK,  ROUND(SUM([CostePiezaRevision]),0) AS COSTEHORASREVISION, ROUND(SUM([CosteScrapRevision]),0) AS COSTESCRAP, ROUND(SUM([CosteRevision]),0) AS COSTETOTAL" +
                            " FROM "+bd+" group by YEAR([FechaInicio]), month([FechaInicio]) ) S" +
                            " LEFT JOIN[SMARTH_DB].[dbo].[AUX_BMSHistoricoProducidas] B ON S.Mes = B.Mes AND B.Periodo = '" + año + "'" +
                            " WHERE YEAR = '" + año + "' ORDER BY S.MES");

                    using (SqlConnection con = cnn_SMARTH)
                {
                    
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        con.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {

                            //STRING PRODUCIDAS
                            StringBuilder sb = new StringBuilder();
                            //Linea 1
                            sb.Append("[");
                            sb.Append("{label: \u0022% Revisadas\u0022,");
                            sb.Append("backgroundColor: [\u0022#6A0DAD\u0022],");
                            sb.Append("borderColor: [\u0022#6A0DAD\u0022],");
                            sb.Append("borderWidth: 3, ");
                            sb.Append("data: [");

                            //Linea 2
                            StringBuilder sb2 = new StringBuilder();
                            sb2.Append("{label: \u0022% Retrabajadas\u0022,");
                            sb2.Append("backgroundColor: [\u0022#0000FF\u0022],");
                            sb2.Append("borderColor: [\u0022#0000FF\u0022],");
                            sb2.Append("borderWidth: 3, ");
                            sb2.Append("data: [");

                            //Linea 3
                           
                            //Linea 5
                            StringBuilder sb5 = new StringBuilder();
                            sb5.Append("{label: \u0022% Malas\u0022,");
                            sb5.Append("backgroundColor: [\u0022#FFFF00\u0022],");
                            sb5.Append("borderColor: [\u0022#FFFF00\u0022],");
                            sb5.Append("borderWidth: 3, ");
                            sb5.Append("data: [");

                            StringBuilder sbcostesREVOP = new StringBuilder();
                            sbcostesREVOP.Append("¬[");
                            sbcostesREVOP.Append("{label: \u0022Coste de Operario\u0022,");
                            sbcostesREVOP.Append("backgroundColor: [\u0022#6A0DAD\u0022],");
                            //sbcostesREVOP.Append("borderColor: [\u0022#6A0DAD\u0022],");
                            // sbcostesREVOP.Append("borderWidth: 3, ");
                            sbcostesREVOP.Append("data: [");

                            StringBuilder sbcostesREVSCRAP = new StringBuilder();
                            sbcostesREVSCRAP.Append("{label: \u0022Coste de Scrap\u0022,");
                            sbcostesREVSCRAP.Append("backgroundColor: [\u0022#0000FF\u0022],");
                            //sbcostesREVSCRAP.Append("borderColor: [\u0022#0000FF\u0022],");
                            //sbcostesREVSCRAP.Append("borderWidth: 3, ");
                            sbcostesREVSCRAP.Append("data: [");

                            while (sdr.Read())
                            {
                                System.Threading.Thread.Sleep(50);
                                sb.Append(string.Format("{0},", sdr[5].ToString().Replace(',','.'))); //PORCREVI
                                sb2.Append(string.Format("{0},", sdr[9].ToString().Replace(',', '.'))); //PORCRETRAB
                                
                                sb5.Append(string.Format("{0},", sdr[13].ToString().Replace(',', '.')));//PORCNOK

                                sbcostesREVOP.Append(string.Format("{0},", sdr[14]));
                                sbcostesREVSCRAP.Append(string.Format("{0},", sdr[15]));

                            }

                            //Correcciones de final de linea
                            sb = sb.Remove(sb.Length - 1, 1);
                            sb.Append("]},");

                            sb2 = sb2.Remove(sb2.Length - 1, 1);
                            sb2.Append("]},");

                          

                            sb5 = sb5.Remove(sb5.Length - 1, 1);
                            sb5.Append("]}]");

                            sbcostesREVOP = sbcostesREVOP.Remove(sbcostesREVOP.Length - 1, 1);
                            sbcostesREVOP.Append("]},");

                            sbcostesREVSCRAP = sbcostesREVSCRAP.Remove(sbcostesREVSCRAP.Length - 1, 1);
                            sbcostesREVSCRAP.Append("]}]");

                            sb.Append(sb2);                           
                            sb.Append(sb5);
                            sb.Append(sbcostesREVOP);
                            sb.Append(sbcostesREVSCRAP);
                            return sb.ToString();

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return "";
            }

        }

        public List<string> Devuelve_KPI_GP12_Mensual_CHART3(string año, string DB)
        {
            try
            {
                string bd = "[SMARTH_DB].[dbo].[GP12_Historico_2021]";
                if (Convert.ToInt32(año) < 2021)
                {
                    bd = "[SMARTH_DB].[dbo].[GP12_Historico]";
                }

                string query = string.Format("SELECT YEAR, DateName(month, DateAdd(month, S.MES, 0) - 1) AS 'MESTEXTO', HORAS, B.Cantidad, REVISADAS, (CAST(REVISADAS AS DECIMAL) / CAST(b.Cantidad AS DECIMAL)) AS PORCREVI, PIEZASOK, RETRABAJADAS, ((CAST(RETRABAJADAS AS decimal) / CAST(REVISADAS AS decimal)) * 1000000) AS PPMRETRABAJADAS, ((CAST(RETRABAJADAS AS decimal) / CAST(REVISADAS AS decimal))) AS PORCRETRABAJADAS, PIEZASNOK, ((CAST(PIEZASNOK AS decimal) / CAST(REVISADAS AS decimal)) * 1000000) AS PPMNOK, PIEZASNOK, ((CAST(PIEZASNOK AS decimal) / CAST(REVISADAS AS decimal))) AS PORCNOK, COSTEHORASREVISION, COSTESCRAP, COSTETOTAL" +
                            " FROM(SELECT YEAR([FechaInicio]) as YEAR, month([FechaInicio]) as Mes, SUM([HorasInspeccion]) AS HORAS, SUM([PiezasRevisadas]) AS REVISADAS, SUM([PiezasOK]) AS PIEZASOK, SUM([Retrabajadas]) AS RETRABAJADAS, SUM([PiezasNOK]) AS PIEZASNOK, SUM([CostePiezaRevision]) AS COSTEHORASREVISION, SUM([CosteScrapRevision]) AS COSTESCRAP, SUM([CosteRevision]) AS COSTETOTAL" +
                            " FROM "+bd+" group by YEAR([FechaInicio]), month([FechaInicio]) ) S" +
                            " LEFT JOIN[SMARTH_DB].[dbo].[AUX_BMSHistoricoProducidas] B ON S.Mes = B.Mes AND B.Periodo = '" + año + "'" +
                            " WHERE YEAR = '" + año + "' ORDER BY S.MES");

                    using (SqlConnection con = cnn_SMARTH)
                {
                    
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        con.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {

                            //STRING PRODUCIDAS
                            StringBuilder sb = new StringBuilder();
                            //Linea 1
                            sb.Append("[");
                            sb.Append("{label: \u0022Producidas\u0022,");
                            sb.Append("backgroundColor: [\u0022#6A0DAD\u0022],");
                            sb.Append("borderColor: [\u0022#6A0DAD\u0022],");
                            sb.Append("borderWidth: 3, ");
                            sb.Append("data: [");

                            //Linea 2
                            StringBuilder sb2 = new StringBuilder();
                            sb2.Append("{label: \u0022Revisadas\u0022,");
                            sb2.Append("backgroundColor: [\u0022#0000FF\u0022],");
                            sb2.Append("borderColor: [\u0022#0000FF\u0022],");
                            sb2.Append("borderWidth: 3, ");
                            sb2.Append("data: [");

                            //Linea 3
                            StringBuilder sb3 = new StringBuilder();
                            sb3.Append("{label: \u0022Buenas\u0022,");
                            sb3.Append("backgroundColor: [\u0022#008000\u0022],");
                            sb3.Append("borderColor: [\u0022#008000\u0022],");
                            sb3.Append("borderWidth: 3, ");
                            sb3.Append("data: [");
                            //Linea 4
                            StringBuilder sb4 = new StringBuilder();
                            sb4.Append("{label: \u0022Malas\u0022,");
                            sb4.Append("backgroundColor: [\u0022#FFA500\u0022],");
                            sb4.Append("borderColor: [\u0022#FFA500\u0022],");
                            sb4.Append("borderWidth: 3, ");
                            sb4.Append("data: [");
                            //Linea 5
                            StringBuilder sb5 = new StringBuilder();
                            sb5.Append("{label: \u0022Retrabajadas\u0022,");
                            sb5.Append("backgroundColor: [\u0022#FFFF00\u0022],");
                            sb5.Append("borderColor: [\u0022#FFFF00\u0022],");
                            sb5.Append("borderWidth: 3, ");
                            sb5.Append("data: [");

                            StringBuilder sbcostesREVOP = new StringBuilder();
                            sbcostesREVOP.Append("¬[");
                            sbcostesREVOP.Append("{label: \u0022Coste operario\u0022,");
                            sbcostesREVOP.Append("backgroundColor: [\u0022#6A0DAD\u0022],");
                            sbcostesREVOP.Append("borderColor: [\u0022#6A0DAD\u0022],");
                            sbcostesREVOP.Append("borderWidth: 3, ");
                            sbcostesREVOP.Append("data: [");

                            StringBuilder sbcostesREVSCRAP = new StringBuilder();
                            sbcostesREVSCRAP.Append("{label: \u0022Revisadas\u0022,");
                            sbcostesREVSCRAP.Append("backgroundColor: [\u0022#0000FF\u0022],");
                            sbcostesREVSCRAP.Append("borderColor: [\u0022#0000FF\u0022],");
                            sbcostesREVSCRAP.Append("borderWidth: 3, ");
                            sbcostesREVSCRAP.Append("data: [");

                            while (sdr.Read())
                            {
                                System.Threading.Thread.Sleep(50);
                                sb.Append(string.Format("{0},", sdr[3]));
                                sb2.Append(string.Format("{0},", sdr[4]));
                                sb3.Append(string.Format("{0},", sdr[6]));
                                sb4.Append(string.Format("{0},", sdr[10]));
                                sb5.Append(string.Format("{0},", sdr[7]));

                                sbcostesREVOP.Append(string.Format("{0},", sdr[14]));
                                sbcostesREVSCRAP.Append(string.Format("{0},", sdr[15]));

                            }

                            //Correcciones de final de linea
                            sb = sb.Remove(sb.Length - 1, 1);
                            sb.Append("]},");

                            sb2 = sb2.Remove(sb2.Length - 1, 1);
                            sb2.Append("]},");

                            sb3 = sb3.Remove(sb3.Length - 1, 1);
                            sb3.Append("]},");

                            sb4 = sb4.Remove(sb4.Length - 1, 1);
                            sb4.Append("]},");

                            sb5 = sb5.Remove(sb5.Length - 1, 1);
                            sb5.Append("]}]");

                            sbcostesREVOP = sbcostesREVOP.Remove(sbcostesREVOP.Length - 1, 1);
                            sbcostesREVOP.Append("]},");

                            sbcostesREVSCRAP = sbcostesREVSCRAP.Remove(sb5.Length - 1, 1);
                            sbcostesREVSCRAP.Append("]}]");

                            sb.Append(sb2);
                            sb.Append(sb3);
                            sb.Append(sb4);
                            sb.Append(sb5);

                            sbcostesREVOP.Append(sbcostesREVSCRAP);

                            con.Close();

                            var Bloques = new List<string> { "set" };
                            Bloques.Add(sb.ToString());
                            Bloques.Add(sbcostesREVOP.ToString());
                            return Bloques;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }


        public DataSet Devuelve_detecciones_operarios(string DB, string mes, string año)
        {
            try
            {
                string bd = "[SMARTH_DB].[dbo].[GP12_Historico_2021]";
                if (Convert.ToInt32(DB) < 2021)
                {
                    bd = "[SMARTH_DB].[dbo].[GP12_Historico]";
                }

                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT TOP 7 IDE,  O.Operario, count(conteo) AS SUMA FROM (SELECT [NOp1] AS IDE, ([NOp1]) as conteo FROM " + bd + " WHERE NOp1 <> 0 " + mes + "" + año + "" +
                " union all SELECT [NOp2] AS IDE,([NOp2]) as conteo	FROM " + bd + "	WHERE NOp2 <> 0 " + mes + "" + año + "" +
                " union all SELECT [NOp3] AS IDE,([NOp3]) as conteo	FROM " + bd + "	WHERE NOp3 <> 0 " + mes + "" + año + "" +
                " union all SELECT [NOp4] AS IDE,([NOp4]) as conteo	FROM " + bd + "	WHERE NOp4 <> 0 " + mes + "" + año + ") T" +
                " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] O ON IDE = O.Id" + //ACTUALIZAR TABLA
                " group by IDE, Operario order by SUMA desc", cnn_SMARTH);

                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                cnn_SMARTH.Close();
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        //RETIRAR BD CALIDAD
       
        public DataSet Devuelve_TOP_PiezasRetrabajadas(string DB, string mes, string año)
        {
            try
            {
                string bd = "[SMARTH_DB].[dbo].[GP12_Historico_2021]";
                if (Convert.ToInt32(DB) < 2021)
                {
                    bd = "[SMARTH_DB].[dbo].[GP12_Historico]";
                }

                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT TOP 7 Referencia, Nombre, HORAS, RETRABAJADAS, ((CAST(RETRABAJADAS AS decimal)/CAST(REVISADAS AS decimal))*1000000) AS PPMRetrabajo, ((CAST(RETRABAJADAS AS decimal)/CAST(REVISADAS AS decimal))) AS PORCRetrabajo, COSTETOTAL FROM " +
                                                                "(SELECT [Referencia],[Nombre],SUM([HorasInspeccion]) AS HORAS,SUM([PiezasRevisadas]) AS REVISADAS,SUM([PiezasOK]) AS PIEZASOK,SUM([Retrabajadas]) AS RETRABAJADAS,SUM([PiezasNOK]) AS PIEZASNOK,SUM([CostePiezaRevision]) AS COSTEHORASREVISION" +
                                                                ",SUM([CosteScrapRevision]) AS COSTESCRAP,SUM([CosteRevision]) AS COSTETOTAL FROM " + bd + " WHERE RETRABAJADAS > 0 "+mes+""+año+" group by Referencia, Nombre ) S ORDER BY PPMRetrabajo desc", cnn_SMARTH);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                cnn_SMARTH.Close();
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataSet Devuelve_TOP_PiezasHoras(string DB, string mes, string año)
        {
            try
            {
                string bd = "[SMARTH_DB].[dbo].[GP12_Historico_2021]";
                if (Convert.ToInt32(DB) < 2021)
                {
                    bd = "[SMARTH_DB].[dbo].[GP12_Historico]";
                }
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT TOP 7 Referencia, Nombre, HORAS, COSTETOTAL FROM " +
                                                              "(SELECT [Referencia],[Nombre],SUM([HorasInspeccion]) AS HORAS,SUM([PiezasRevisadas]) AS REVISADAS,SUM([PiezasOK]) AS PIEZASOK,SUM([Retrabajadas]) AS RETRABAJADAS,SUM([PiezasNOK]) AS PIEZASNOK,SUM([CostePiezaRevision]) AS COSTEHORASREVISION" +
                                                               ",SUM([CosteScrapRevision]) AS COSTESCRAP,SUM([CosteRevision]) AS COSTETOTAL FROM "+ bd + " WHERE [Referencia] <> '' " + mes + "" + año + " group by Referencia, Nombre ) S ORDER BY HORAS desc", cnn_SMARTH);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                cnn_SMARTH.Close();
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataSet Devuelve_TOP_Costes(string DB, string mes, string año)
        {
            try
            {
                string bd = "[SMARTH_DB].[dbo].[GP12_Historico_2021]";
                if (Convert.ToInt32(DB) < 2021)
                {
                    bd = "[SMARTH_DB].[dbo].[GP12_Historico]";
                }
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT TOP 7 Referencia, Nombre, HORAS, COSTEHORASREVISION, COSTESCRAP, COSTETOTAL FROM " +
                                                              "(SELECT [Referencia],[Nombre],SUM([HorasInspeccion]) AS HORAS,SUM([PiezasRevisadas]) AS REVISADAS,SUM([PiezasOK]) AS PIEZASOK,SUM([Retrabajadas]) AS RETRABAJADAS,SUM([PiezasNOK]) AS PIEZASNOK,SUM([CostePiezaRevision]) AS COSTEHORASREVISION" +
                                                               ",SUM([CosteScrapRevision]) AS COSTESCRAP,SUM([CosteRevision]) AS COSTETOTAL FROM "+bd+" WHERE Referencia <> '' " + mes + "" + año + " group by Referencia, Nombre ) S ORDER BY COSTETOTAL desc", cnn_SMARTH);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                cnn_SMARTH.Close();
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataSet Devuelve_TOP_PiezasNOK(string DB, string mes, string año)
        {
            try
            {
                string bd = "[SMARTH_DB].[dbo].[GP12_Historico_2021]";
                if (Convert.ToInt32(DB) < 2021)
                {
                    bd = "[SMARTH_DB].[dbo].[GP12_Historico]";
                }
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT TOP 7 Referencia, Nombre, HORAS, PIEZASNOK, ((CAST(PIEZASNOK AS decimal)/CAST(REVISADAS AS decimal))*1000000) AS PPMNOK, ((CAST(PIEZASNOK AS decimal)/CAST(REVISADAS AS decimal))) AS PORCNOK, COSTETOTAL FROM " +
                                                              "(SELECT [Referencia],[Nombre],SUM([HorasInspeccion]) AS HORAS,SUM([PiezasRevisadas]) AS REVISADAS,SUM([PiezasOK]) AS PIEZASOK,SUM([Retrabajadas]) AS RETRABAJADAS,SUM([PiezasNOK]) AS PIEZASNOK,SUM([CostePiezaRevision]) AS COSTEHORASREVISION" +
                                                               ",SUM([CosteScrapRevision]) AS COSTESCRAP,SUM([CosteRevision]) AS COSTETOTAL FROM "+bd+" WHERE PIEZASNOK > 0 "+mes+""+año+" group by Referencia, Nombre ) S ORDER BY PPMNOK desc", cnn_SMARTH);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                cnn_SMARTH.Close();
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataSet Devuelve_totales_KPIGP12(string DB, string año, string mes, string mesanterior, string añoanterior)
        {
            try
            {
                string bd = "[SMARTH_DB].[dbo].[GP12_Historico_2021]";
                if (Convert.ToInt32(DB) < 2021)
                {
                    bd = "[SMARTH_DB].[dbo].[GP12_Historico]";
                }
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT MIN(FechaInicio) AS FECHAINICIO, ROUND(SUM([HorasInspeccion]),2) AS HORAS, SUM([PiezasRevisadas]) AS REVISADAS, SUM([PiezasOK]) AS PIEZASOK, SUM([Retrabajadas]) AS RETRABAJADAS, SUM([PiezasNOK]) AS PIEZASNOK, ROUND(SUM([CostePiezaRevision]),2) AS COSTEHORASREVISION, ROUND(SUM([CosteScrapRevision]),2) AS COSTESCRAP, ROUND(SUM([CosteRevision]),0) AS COSTETOTAL" +
                                                                " FROM "+bd+" WHERE PiezasOK >= 0  " + mes + "" + año + "" +
                                                                " UNION SELECT MIN(FechaInicio) AS FECHAINICIO, ROUND(SUM([HorasInspeccion]),2) AS HORAS, SUM([PiezasRevisadas]) AS REVISADAS, SUM([PiezasOK]) AS PIEZASOK, SUM([Retrabajadas]) AS RETRABAJADAS, SUM([PiezasNOK]) AS PIEZASNOK, ROUND(SUM([CostePiezaRevision]),2) AS COSTEHORASREVISION, ROUND(SUM([CosteScrapRevision]),2) AS COSTESCRAP, ROUND(SUM([CosteRevision]),0) AS COSTETOTAL" +
                                                                " FROM "+bd+" WHERE PiezasOK >= 0  " + mesanterior + ""+ añoanterior + " ORDER BY FECHAINICIO DESC", cnn_SMARTH);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                cnn_SMARTH.Close();
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataSet Devuelve_KPI_GP12_RazonRevision(string año, string where)
        {
            try
            {
                string bd = "[SMARTH_DB].[dbo].[GP12_Historico_2021]";
                if (Convert.ToInt32(año) < 2021)
                {
                    bd = "[SMARTH_DB].[dbo].[GP12_Historico]";
                }

                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT MES, CONTEO, a.Razon as RazonNUM, R.Razon, SCRAP, TOTAL, HORAS FROM(SELECT DATENAME(month, ([FechaInicio])) as MES, month([FechaInicio]) AS MESNUM, [RazonRevision] as Razon, COUNT([FechaInicio]) CONTEO, ROUND(SUM([CosteScrapRevision]),2) AS SCRAP, ROUND(SUM([CosteRevision]),2) AS TOTAL, ROUND(SUM([HorasInspeccion]),0) AS HORAS" +
                              " FROM "+bd+"" +
                              " where year([FechaInicio]) = '"+año+"' "+where+"" +
                              " GROUP BY  DATENAME(month, ([FechaInicio])),month([FechaInicio]), RazonRevision) A" +
                              " LEFT JOIN [SMARTH_DB].[dbo].[AUX_GP12_RazonRevision] R ON A.Razon = R.Id" +
                              " order by MESNUM ASC, TOTAL DESC";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataSet Devuelve_KPI_GP12_RazonRevision_DETALLE(string año, string mes, string tipo)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string bd = "[SMARTH_DB].[dbo].[GP12_Historico_2021]";
                if (Convert.ToInt32(año) < 2021)
                {
                    bd = "[SMARTH_DB].[dbo].[GP12_Historico]";
                }
                string sql = " SELECT MES, Cliente, CONTEO, SCRAP, TOTAL, HORAS" +
                            " FROM(SELECT DATENAME(month, ([FechaInicio])) as MES, month([FechaInicio]) AS MESNUM, prod.Cliente, COUNT([FechaInicio]) CONTEO, ROUND(SUM([CosteScrapRevision]),2) AS SCRAP, ROUND(SUM([CosteRevision]),2) AS TOTAL, ROUND(SUM([HorasInspeccion]),0) AS HORAS" +
                            " FROM "+bd+" a" +
                            " left join [SMARTH_DB].[dbo].[AUX_TablaProductos] prod on a.Referencia = prod.Referencia" +
                            " where year([FechaInicio]) = " + año + " and month([FechaInicio]) = " + mes + " and RazonRevision = " + tipo + "" +
                            " GROUP BY  DATENAME(month, ([FechaInicio])),month([FechaInicio]), cliente) A" +
                            " order by MESNUM ASC, TOTAL DESC";
          
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataSet Devuelve_KPI_GP12_RazonRevisionAÑO(string año)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string bd = "[SMARTH_DB].[dbo].[GP12_Historico_2021]";
                if (Convert.ToInt32(año) < 2021)
                {
                    bd = "[SMARTH_DB].[dbo].[GP12_Historico]";
                }

                string sql = " SELECT AÑO, a.Razon as RazonNUM, R.Razon, CONTEO, SCRAP, TOTAL, HORAS FROM(SELECT year([FechaInicio]) as AÑO, [RazonRevision] as Razon, COUNT([FechaInicio]) CONTEO, ROUND(SUM([CosteScrapRevision]),2) AS SCRAP, ROUND(SUM([CosteRevision]),2) AS TOTAL, ROUND(SUM([HorasInspeccion]),0) AS HORAS" +
                              " FROM "+bd+"" +
                              " where year([FechaInicio]) = '"+año+"'" +
                              " GROUP BY  year([FechaInicio]), RazonRevision) A" +
                              " LEFT JOIN [SMARTH_DB].[dbo].[AUX_GP12_RazonRevision] R ON A.Razon = R.Id" +
                              " order by TOTAL DESC";
               
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataSet Devuelve_KPI_GP12_RazonRevisionAÑO_DETALLE(string año, string tipo)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string bd = "[SMARTH_DB].[dbo].[GP12_Historico_2021]";
                if (Convert.ToInt32(año) < 2021)
                {
                    bd = "[SMARTH_DB].[dbo].[GP12_Historico]";
                }
                string sql = "SELECT MES, Cliente, CONTEO, SCRAP, TOTAL, HORAS" +
                             " FROM(SELECT DATENAME(year, ([FechaInicio])) as MES, prod.Cliente, COUNT([FechaInicio]) CONTEO, ROUND(SUM([CosteScrapRevision]),2) AS SCRAP, ROUND(SUM([CosteRevision]),2) AS TOTAL, ROUND(SUM([HorasInspeccion]),0) AS HORAS" +
                             " FROM "+bd+" a" +
                             " left join [SMARTH_DB].[dbo].[AUX_TablaProductos] prod on a.Referencia = prod.Referencia" +
                             " where year([FechaInicio]) = "+año+" and RazonRevision = "+tipo+"" +
                             " GROUP BY  DATENAME(year, ([FechaInicio])), cliente) A" +
                             " order by TOTAL DESC";
 
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataSet Devuelve_KPI_GP12xCliente(string año, string where)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string bd = "[SMARTH_DB].[dbo].[GP12_Historico_2021]";
                if (Convert.ToInt32(año) < 2021)
                {
                    bd = "[SMARTH_DB].[dbo].[GP12_Historico]";
                }
                string sql = " SELECT DATENAME(month, ([FechaInicio])) as MES, month([FechaInicio]) AS MESNUM, PRO.Cliente, COUNT([FechaInicio]) CONTEO, ROUND(SUM([CosteScrapRevision]),2) AS SCRAP, ROUND(SUM([CosteRevision]),2) AS TOTAL, ROUND(SUM(HorasInspeccion),2) AS HorasInspeccion" +
                                " FROM "+bd+" HIS" +
                                " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] PRO ON HIS.Referencia = PRO.Referencia" +
                                " where year([FechaInicio]) = '" + año + "' " + where + "" +
                                " GROUP BY  DATENAME(month, ([FechaInicio])),month([FechaInicio]), PRO.Cliente"+
                                " ORDER BY TOTAL DESC";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataSet Devuelve_KPI_GP12xClienteAÑO(string año)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string bd = "[SMARTH_DB].[dbo].[GP12_Historico_2021]";
                if (Convert.ToInt32(año) < 2021)
                {
                    bd = "[SMARTH_DB].[dbo].[GP12_Historico]";
                }
                string sql  = " SELECT YEAR([FechaInicio]) AS YEAR, PRO.Cliente, COUNT([FechaInicio]) CONTEO, ROUND(SUM([CosteScrapRevision]),2) AS SCRAP, ROUND(SUM([CosteRevision]),2) AS TOTAL, ROUND(SUM(HorasInspeccion),2) AS HorasInspeccion" +
                                " FROM "+bd+" HIS" +
                                " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] PRO ON HIS.Referencia = PRO.Referencia" +
                                " where year([FechaInicio]) = '" + año + "'" +
                                " GROUP BY  YEAR([FechaInicio]), PRO.Cliente" +
                                " ORDER BY TOTAL DESC";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataSet Devuelve_KPI_Incidencias_GP12(string año, string where)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                
                string bd = "[SMARTH_DB].[dbo].[GP12_Historico_2021]";
                if (Convert.ToInt32(año) < 2021)
                {
                    bd = "[SMARTH_DB].[dbo].[GP12_Historico]";
                }

                string sql = " SELECT MES, CONTEO, AlertaGP12 as AlertaGP12NUM, CASE WHEN AlertaGP12 = 0 THEN 'Sin incidencia' WHEN AlertaGP12 = 1 THEN 'Sin fecha definida'  WHEN AlertaGP12 = 2 THEN 'Plazo vencido' WHEN AlertaGP12 = 3 THEN 'Pieza sin revisión' END AS Razon, SCRAP, TOTAL FROM(SELECT DATENAME(month, ([FechaInicio])) as MES, month([FechaInicio]) AS MESNUM, AlertaGP12 as AlertaGP12, COUNT([FechaInicio]) CONTEO, ROUND(SUM([CosteScrapRevision]),2) AS SCRAP, ROUND(SUM([CosteRevision]),2) AS TOTAL" +
                               " FROM "+bd+"" +
                               " where year([FechaInicio]) = '" + año + "' " + where + "" +
                               " GROUP BY  DATENAME(month, ([FechaInicio])),month([FechaInicio]), AlertaGP12) A" +
                               " order by MESNUM ASC, TOTAL DESC";
             

                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }
      
        public DataSet Devuelve_KPI_Incidencias_GP12_DETALLE(string año, string mes, string tipo)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string bd = "[SMARTH_DB].[dbo].[GP12_Historico_2021]";
                if (Convert.ToInt32(año) < 2021)
                {
                    bd = "[SMARTH_DB].[dbo].[GP12_Historico]";
                }
                string sql = " SELECT DATENAME(MONTH, ([FechaInicio])) AS MES, PROD.Cliente,COUNT([AlertaGP12]) as INCIDENCIAS,ROUND(SUM([CosteRevision]),2) AS COSTE"+
                                " FROM "+bd+" R"+
                                " left join[SMARTH_DB].[dbo].[AUX_TablaProductos] PROD ON R.Referencia = PROD.Referencia" +
                                " WHERE YEAR([FechaInicio]) = "+año+" AND MONTH([FechaInicio]) = "+mes+" AND AlertaGP12 = "+tipo+"" +
                                " GROUP BY DATENAME(MONTH, ([FechaInicio])), PROD.Cliente" +
                                " ORDER BY INCIDENCIAS DESC";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataSet Devuelve_KPI_Incidencias_GP12AÑO(string año)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string bd = "[SMARTH_DB].[dbo].[GP12_Historico_2021]";
                if (Convert.ToInt32(año) < 2021)
                {
                    bd = "[SMARTH_DB].[dbo].[GP12_Historico]";
                }
                string sql  = " SELECT AÑO, AlertaGP12 as AlertaGP12NUM, CONTEO, CASE WHEN AlertaGP12 = 0 THEN 'Sin incidencia' WHEN AlertaGP12 = 1 THEN 'Sin fecha definida'  WHEN AlertaGP12 = 2 THEN 'Plazo vencido' WHEN AlertaGP12 = 3 THEN 'Pieza sin revisión' END AS Razon, SCRAP, TOTAL" +
                               " FROM(SELECT DATENAME(year, ([FechaInicio])) as AÑO, AlertaGP12 as AlertaGP12, COUNT([FechaInicio]) CONTEO, ROUND(SUM([CosteScrapRevision]),2) AS SCRAP, ROUND(SUM([CosteRevision]),2) AS TOTAL" +
                               " FROM "+bd+"" +
                               " where year([FechaInicio]) = '" + año + "'" +
                               " GROUP BY  DATENAME(year, ([FechaInicio])), AlertaGP12) A" +
                               " order by TOTAL DESC";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataSet Devuelve_KPI_Incidencias_GP12AÑO_DETALLE(string año, string tipo)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string bd = "[SMARTH_DB].[dbo].[GP12_Historico_2021]";
                if (Convert.ToInt32(año) < 2021)
                {
                    bd = "[SMARTH_DB].[dbo].[GP12_Historico]";
                }
                string sql = "SELECT YEAR([FechaInicio]) AS MES, PROD.Cliente,COUNT([AlertaGP12]) as INCIDENCIAS,ROUND(SUM([CosteRevision]),2) AS COSTE" +
                                 " FROM "+bd+" R" +
                                 " left join [SMARTH_DB].[dbo].[AUX_TablaProductos] PROD ON R.Referencia = PROD.Referencia" +
                                 " WHERE YEAR([FechaInicio]) = "+año+" AND AlertaGP12 = "+tipo+"" +
                                 " GROUP BY YEAR([FechaInicio]), PROD.Cliente" +
                                 " ORDER BY INCIDENCIAS DESC";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataSet Devuelve_KPI_ComunicacionesPDTExINFORMADOS_Totales(string año, string where)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string bd = "[SMARTH_DB].[dbo].[GP12_Historico_2021]";
                if (Convert.ToInt32(año) < 2021)
                {
                    bd = "[SMARTH_DB].[dbo].[GP12_Historico]";
                }
                string sql = "SELECT AÑO, COUNT(INFO) AS DETECCIONES, SUM(CASE WHEN INFO = 'Informado' THEN 1 ELSE 0 END) AS INFORMADO, SUM(CASE WHEN INFO = 'Pdte. informar' THEN 1 ELSE 0 END) AS PENDIENTE" +
                                       " FROM(SELECT YEAR(C.FechaInicio) AS AÑO, CASE WHEN NOp = 0 THEN ' ' WHEN NOp > 0 AND CAST(C.FechaInfo AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp > 0 AND CAST(FechaInfo AS varchar) <> '' THEN 'Informado' END AS INFO" +
                                 " FROM(SELECT[FechaInicio], [NOp1] as NOp," +
                                       " CASE WHEN NOp1 = 0 THEN ' ' WHEN NOp1 > 0 AND CAST(FechaInfo1 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp1 > 0 AND CAST(FechaInfo1 AS varchar) <> '' THEN CAST(FechaInfo1 AS varchar) END AS INFO," +
                                       " [FechaInfo1] AS FECHAINFO,[Informador1] AS INFORMADOR" +
                                       " FROM " + bd + "" +
                                       " WHERE NOp1 > 0 AND([PiezasNOK] > 0 OR[Retrabajadas] > 0)" +
                                 " UNION ALL SELECT[FechaInicio], [NOp2] as NOp," +
                                       " CASE WHEN NOp2 = 0 THEN ' ' WHEN NOp2 > 0 AND CAST(FechaInfo2 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp2 > 0 AND CAST(FechaInfo2 AS varchar) <> '' THEN CAST(FechaInfo2 AS varchar) END AS INFO," +
                                       " [FechaInfo2] AS FECHAINFO,[Informador2] AS INFORMADOR" +
                                       " FROM "+bd+"" +
                                       " WHERE NOp2 > 0 AND([PiezasNOK] > 0 OR[Retrabajadas] > 0)" +
                                 " UNION ALL SELECT[FechaInicio], [NOp3] as NOp," +
                                       " CASE WHEN NOp3 = 0 THEN ' ' WHEN NOp3 > 0 AND CAST(FechaInfo3 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp3 > 0 AND CAST(FechaInfo3 AS varchar) <> '' THEN CAST(FechaInfo3 AS varchar) END AS INFO," +
                                       " [FechaInfo3] AS FECHAINFO,[Informador3] AS INFORMADOR" +
                                       " FROM " + bd + "" +
                                       " WHERE NOp3 > 0 AND([PiezasNOK] > 0 OR[Retrabajadas] > 0)" +
                                 " UNION ALL SELECT[FechaInicio], [NOp4] as NOp," +
                                      "  CASE WHEN NOp4 = 0 THEN ' ' WHEN NOp4 > 0 AND CAST(FechaInfo4 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp4 > 0 AND CAST(FechaInfo3 AS varchar) <> '' THEN CAST(FechaInfo4 AS varchar) END AS INFO," +
                                       " [FechaInfo4] AS FECHAINFO,[Informador4] AS INFORMADOR" +
                                 " FROM " + bd + "" +
                                       " WHERE NOp4 > 0 AND([PiezasNOK] > 0 OR[Retrabajadas] > 0)" +
                                       " ) C" +
                                       " left join[SMARTH_DB].[dbo].[AUX_TablaOperarios] OP ON C.NOp = OP.Id" +
                               " WHERE YEAR(C.FechaInicio) = '" + año + "' AND OP.[OpActivo] = 1 " + where+") F" +
                               " GROUP BY AÑO";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataSet Devuelve_KPI_ComunicacionesRankingxINFORMADORES(string año)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string bd = "[SMARTH_DB].[dbo].[GP12_Historico_2021]";
                if (Convert.ToInt32(año) < 2021)
                {
                    bd = "[SMARTH_DB].[dbo].[GP12_Historico]";
                }
                string sql = "SELECT AÑO, INFORMADOR, COUNT(INFO) AS INFORMADO" +
                                " FROM(SELECT YEAR(C.FechaInicio) AS AÑO," +
                                     " CASE WHEN NOp = 0 THEN ' ' WHEN NOp > 0 AND CAST(C.FechaInfo AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp > 0 AND CAST(FechaInfo AS varchar) <> '' THEN 'Informado' END AS INFO, INFORMADOR" +
                           " FROM(SELECT[FechaInicio], [NOp1] as NOp," +
                                  " CASE WHEN NOp1 = 0 THEN ' ' WHEN NOp1 > 0 AND CAST(FechaInfo1 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp1 > 0 AND CAST(FechaInfo1 AS varchar) <> '' THEN CAST(FechaInfo1 AS varchar) END AS INFO," +
                                  " [FechaInfo1] AS FECHAINFO,[Informador1] AS INFORMADOR" +
                                  " FROM "+bd+"" +
                                  " WHERE NOp1 > 0 AND([PiezasNOK] > 0 OR[Retrabajadas] > 0) AND FechaInfo1 <> ''" +
                           " UNION ALL SELECT[FechaInicio], [NOp2] as NOp," +
                                  " CASE WHEN NOp2 = 0 THEN ' ' WHEN NOp2 > 0 AND CAST(FechaInfo2 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp2 > 0 AND CAST(FechaInfo2 AS varchar) <> '' THEN CAST(FechaInfo2 AS varchar) END AS INFO," +
                                  " [FechaInfo2] AS FECHAINFO,[Informador2] AS INFORMADOR" +
                                  " FROM " + bd + "" +
                                  " WHERE NOp2 > 0 AND([PiezasNOK] > 0 OR[Retrabajadas] > 0) AND FechaInfo2<> ''" +
                            " UNION ALL SELECT[FechaInicio], [NOp3] as NOp," +
                                  " CASE WHEN NOp3 = 0 THEN ' ' WHEN NOp3 > 0 AND CAST(FechaInfo3 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp3 > 0 AND CAST(FechaInfo3 AS varchar) <> '' THEN CAST(FechaInfo3 AS varchar) END AS INFO," +
                                  " [FechaInfo3] AS FECHAINFO,[Informador3] AS INFORMADOR" +
                                  " FROM " + bd + "" +
                                  " WHERE NOp3 > 0 AND([PiezasNOK] > 0 OR[Retrabajadas] > 0) AND FechaInfo3<> ''" +
                            " UNION ALL SELECT[FechaInicio], [NOp4] as NOp," +
                                  " CASE WHEN NOp4 = 0 THEN ' ' WHEN NOp4 > 0 AND CAST(FechaInfo4 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp4 > 0 AND CAST(FechaInfo3 AS varchar) <> '' THEN CAST(FechaInfo4 AS varchar) END AS INFO," +
                                  " [FechaInfo4] AS FECHAINFO,[Informador4] AS INFORMADOR" +
                                 "  FROM " + bd + "" +
                                  " WHERE NOp4 > 0 AND([PiezasNOK] > 0 OR[Retrabajadas] > 0) AND FechaInfo4<> ''" +
                                 "  ) C" +
                                 " left join[SMARTH_DB].[dbo].[AUX_TablaOperarios] OP ON C.NOp = OP.Id" +
                          " WHERE YEAR(C.FechaInicio) = '" + año + "' AND OP.[OpActivo] = 1) F" +
                          " GROUP BY AÑO, INFORMADOR" +
                          " ORDER BY INFORMADO DESC";
          
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataSet Devuelve_KPI_ComunicacionesRankingxINFORMADORES_MES(string año, string WHERE)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string bd = "[SMARTH_DB].[dbo].[GP12_Historico_2021]";
                if (Convert.ToInt32(año) < 2021)
                {
                    bd = "[SMARTH_DB].[dbo].[GP12_Historico]";
                }
                string sql = "SELECT MESTEXTO, MESNUM, INFORMADOR, COUNT(INFO) AS INFORMADO"+
                                " FROM(SELECT DATENAME(MONTH, (C.FechaInicio)) AS MESTEXTO, MONTH(C.FechaInicio) AS MESNUM,"+
                                     " CASE WHEN NOp = 0 THEN ' ' WHEN NOp > 0 AND CAST(C.FechaInfo AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp > 0 AND CAST(FechaInfo AS varchar) <> '' THEN 'Informado' END AS INFO, INFORMADOR"+
                           " FROM(SELECT[FechaInicio], [NOp1] as NOp,"+
                                  " CASE WHEN NOp1 = 0 THEN ' ' WHEN NOp1 > 0 AND CAST(FechaInfo1 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp1 > 0 AND CAST(FechaInfo1 AS varchar) <> '' THEN CAST(FechaInfo1 AS varchar) END AS INFO," +
                                  " [FechaInfo1] AS FECHAINFO,[Informador1] AS INFORMADOR" +
                                  " FROM "+ bd+"" +
                                  " WHERE NOp1 > 0 AND([PiezasNOK] > 0 OR[Retrabajadas] > 0) AND FechaInfo1 <> ''" +
                           " UNION ALL SELECT[FechaInicio], [NOp2] as NOp," +
                                  " CASE WHEN NOp2 = 0 THEN ' ' WHEN NOp2 > 0 AND CAST(FechaInfo2 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp2 > 0 AND CAST(FechaInfo2 AS varchar) <> '' THEN CAST(FechaInfo2 AS varchar) END AS INFO," +
                                  " [FechaInfo2] AS FECHAINFO,[Informador2] AS INFORMADOR" +
                                  " FROM" + bd + "" +
                                  " WHERE NOp2 > 0 AND([PiezasNOK] > 0 OR[Retrabajadas] > 0) AND FechaInfo2<> ''" +
                            " UNION ALL SELECT[FechaInicio], [NOp3] as NOp," +
                                  " CASE WHEN NOp3 = 0 THEN ' ' WHEN NOp3 > 0 AND CAST(FechaInfo3 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp3 > 0 AND CAST(FechaInfo3 AS varchar) <> '' THEN CAST(FechaInfo3 AS varchar) END AS INFO," +
                                  " [FechaInfo3] AS FECHAINFO,[Informador3] AS INFORMADOR" +
                                  " FROM" + bd + "" +
                                  " WHERE NOp3 > 0 AND([PiezasNOK] > 0 OR[Retrabajadas] > 0) AND FechaInfo3<> ''"+
                            " UNION ALL SELECT[FechaInicio], [NOp4] as NOp," +
                                  " CASE WHEN NOp4 = 0 THEN ' ' WHEN NOp4 > 0 AND CAST(FechaInfo4 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp4 > 0 AND CAST(FechaInfo3 AS varchar) <> '' THEN CAST(FechaInfo4 AS varchar) END AS INFO," +
                                  " [FechaInfo4] AS FECHAINFO,[Informador4] AS INFORMADOR" +
                                 "  FROM" + bd + "" +
                                  " WHERE NOp4 > 0 AND([PiezasNOK] > 0 OR[Retrabajadas] > 0) AND FechaInfo4<> ''" +
                                 "  ) C" +
                                 " left join[SMARTH_DB].[dbo].[AUX_TablaOperarios] OP ON C.NOp = OP.Id" +
                          " WHERE YEAR(C.FechaInicio) = '" + año + "' AND OP.[OpActivo] = 1 " + WHERE + ") F" +
                          " GROUP BY MESTEXTO, MESNUM, INFORMADOR" +
                          " ORDER BY MESNUM ASC, INFORMADO DESC";
      
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataSet Devuelve_KPI_ComunicacionesRankingOPERARIOS_INFORMADOS(string año)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string bd = "[SMARTH_DB].[dbo].[GP12_Historico_2021]";
                if (Convert.ToInt32(año) < 2021)
                {
                    bd = "[SMARTH_DB].[dbo].[GP12_Historico]";
                }
                string sql = " SELECT AÑO, NOp, Operario,SUM(CASE WHEN INFO = 'Informado' THEN 1 ELSE 0 END) AS INFORMADO, SUM(CASE WHEN INFO = 'Pdte. informar' THEN 1 ELSE 0 END) AS PENDIENTE" +
                                 " FROM(SELECT YEAR(C.FechaInicio) AS AÑO," +
                                 " CASE WHEN NOp = 0 THEN ' ' WHEN NOp > 0 AND CAST(C.FechaInfo AS varchar) IS NULL THEN 'Pdte. informar'" +
                                 " WHEN NOp > 0 AND CAST(FechaInfo AS varchar) <> '' THEN 'Informado' END AS INFO, INFORMADOR, Nop, OP.Operario" +
                           " FROM(SELECT[FechaInicio], [NOp1] as NOp," +
                                 " CASE WHEN NOp1 = 0 THEN ' ' WHEN NOp1 > 0 AND CAST(FechaInfo1 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp1 > 0 AND CAST(FechaInfo1 AS varchar) <> '' THEN CAST(FechaInfo1 AS varchar) END AS INFO," +
                                 " [FechaInfo1] AS FECHAINFO,[Informador1] AS INFORMADOR" +
                                 " FROM "+bd+"" +
                                 " WHERE NOp1 > 0 AND([PiezasNOK] > 0 OR[Retrabajadas] > 0)" +
                           " UNION ALL SELECT[FechaInicio], [NOp2] as NOp," +
                                 " CASE WHEN NOp2 = 0 THEN ' ' WHEN NOp2 > 0 AND CAST(FechaInfo2 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp2 > 0 AND CAST(FechaInfo2 AS varchar) <> '' THEN CAST(FechaInfo2 AS varchar) END AS INFO," +
                                 " [FechaInfo2] AS FECHAINFO,[Informador2] AS INFORMADOR" +
                                 " FROM " + bd + "" +
                                 " WHERE NOp2 > 0 AND([PiezasNOK] > 0 OR[Retrabajadas] > 0)" +
                            " UNION ALL SELECT[FechaInicio], [NOp3] as NOp," +
                                 " CASE WHEN NOp3 = 0 THEN ' ' WHEN NOp3 > 0 AND CAST(FechaInfo3 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp3 > 0 AND CAST(FechaInfo3 AS varchar) <> '' THEN CAST(FechaInfo3 AS varchar) END AS INFO," +
                                 " [FechaInfo3] AS FECHAINFO,[Informador3] AS INFORMADOR" +
                                 " FROM " + bd + "" +
                                 " WHERE NOp3 > 0 AND([PiezasNOK] > 0 OR[Retrabajadas] > 0) " +
                            " UNION ALL SELECT[FechaInicio], [NOp4] as NOp," +
                                 " CASE WHEN NOp4 = 0 THEN ' ' WHEN NOp4 > 0 AND CAST(FechaInfo4 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp4 > 0 AND CAST(FechaInfo3 AS varchar) <> '' THEN CAST(FechaInfo4 AS varchar) END AS INFO," +
                                 " [FechaInfo4] AS FECHAINFO,[Informador4] AS INFORMADOR" +
                                 " FROM " + bd + "" +
                                 " WHERE NOp4 > 0 AND([PiezasNOK] > 0 OR[Retrabajadas] > 0)" +
                                 " ) C" +
                          " left join[SMARTH_DB].[dbo].[AUX_TablaOperarios] OP ON C.NOp = OP.Id" +
                          " WHERE YEAR(C.FechaInicio) = '"+año+ "' AND OP.[OpActivo] = 1) F" +
                          " GROUP BY AÑO, NOp, Operario" +
                          " ORDER BY PENDIENTE DESC, INFORMADO DESC";
                
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataSet Devuelve_KPI_ComunicacionesRankingOPERARIOS_INFORMADOS_MES(string año, string WHERE)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string bd = "[SMARTH_DB].[dbo].[GP12_Historico_2021]";
                if (Convert.ToInt32(año) < 2021)
                {
                    bd = "[SMARTH_DB].[dbo].[GP12_Historico]";
                }
                string sql  = " SELECT MESTEXTO, MESNUM, NOp, Operario,SUM(CASE WHEN INFO = 'Informado' THEN 1 ELSE 0 END) AS INFORMADO, SUM(CASE WHEN INFO = 'Pdte. informar' THEN 1 ELSE 0 END) AS PENDIENTE" +
                                " FROM(SELECT DATENAME(MONTH, (C.FechaInicio)) AS MESTEXTO, MONTH(C.FechaInicio) AS MESNUM," +
                                 " CASE WHEN NOp = 0 THEN ' ' WHEN NOp > 0 AND CAST(C.FechaInfo AS varchar) IS NULL THEN 'Pdte. informar'" +
                                 " WHEN NOp > 0 AND CAST(FechaInfo AS varchar) <> '' THEN 'Informado' END AS INFO, INFORMADOR, Nop, OP.Operario" +
                           " FROM(SELECT[FechaInicio], [NOp1] as NOp," +
                                 " CASE WHEN NOp1 = 0 THEN ' ' WHEN NOp1 > 0 AND CAST(FechaInfo1 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp1 > 0 AND CAST(FechaInfo1 AS varchar) <> '' THEN CAST(FechaInfo1 AS varchar) END AS INFO," +
                                 " [FechaInfo1] AS FECHAINFO,[Informador1] AS INFORMADOR" +
                                 " FROM "+bd+"" +
                                 " WHERE NOp1 > 0 AND([PiezasNOK] > 0 OR[Retrabajadas] > 0)" +
                           " UNION ALL SELECT[FechaInicio], [NOp2] as NOp," +
                                 " CASE WHEN NOp2 = 0 THEN ' ' WHEN NOp2 > 0 AND CAST(FechaInfo2 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp2 > 0 AND CAST(FechaInfo2 AS varchar) <> '' THEN CAST(FechaInfo2 AS varchar) END AS INFO," +
                                 " [FechaInfo2] AS FECHAINFO,[Informador2] AS INFORMADOR" +
                                 " FROM " + bd + "" +
                                 " WHERE NOp2 > 0 AND([PiezasNOK] > 0 OR[Retrabajadas] > 0)" +
                            " UNION ALL SELECT[FechaInicio], [NOp3] as NOp," +
                                 " CASE WHEN NOp3 = 0 THEN ' ' WHEN NOp3 > 0 AND CAST(FechaInfo3 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp3 > 0 AND CAST(FechaInfo3 AS varchar) <> '' THEN CAST(FechaInfo3 AS varchar) END AS INFO," +
                                 " [FechaInfo3] AS FECHAINFO,[Informador3] AS INFORMADOR" +
                                 " FROM " + bd + "" +
                                 " WHERE NOp3 > 0 AND([PiezasNOK] > 0 OR[Retrabajadas] > 0) " +
                            " UNION ALL SELECT[FechaInicio], [NOp4] as NOp," +
                                 " CASE WHEN NOp4 = 0 THEN ' ' WHEN NOp4 > 0 AND CAST(FechaInfo4 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp4 > 0 AND CAST(FechaInfo3 AS varchar) <> '' THEN CAST(FechaInfo4 AS varchar) END AS INFO," +
                                 " [FechaInfo4] AS FECHAINFO,[Informador4] AS INFORMADOR" +
                                 " FROM " + bd + "" +
                                 " WHERE NOp4 > 0 AND([PiezasNOK] > 0 OR[Retrabajadas] > 0)" +
                                 " ) C" +
                          " left join[SMARTH_DB].[dbo].[AUX_TablaOperarios] OP ON C.NOp = OP.Id" +
                          " WHERE YEAR(C.FechaInicio) = '"+año+ "' AND OP.[OpActivo] = 1" + WHERE+") F" +
                          " GROUP BY MESTEXTO, MESNUM, NOp, Operario" +
                          " ORDER BY MESNUM ASC, PENDIENTE DESC, INFORMADO DESC";
                
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        ///////////\\\\\\\\\\\\\DASHBOARD KPI
        public DataTable Devuelve_Objetivos_KPI_Editor(string APP)
        {
            try
            {

                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT [Año],[" + APP + "SIMB] as SIMBOLO,[" + APP+"] AS KPI FROM[SMARTH_DB].[dbo].[KPI_Objetivos_Anuales] order by Año desc";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public bool Actualiza_Objetivos_KPI_Editor(string KPI, int año, string simbolo, string valor)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "UPDATE [SMARTH_DB].[dbo].[KPI_Objetivos_Anuales] SET ["+ KPI + "SIMB] = '"+simbolo+ "',[" + KPI + "] = " + valor+" WHERE [Año] = " + año + "";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }

        public DataTable Devuelve_Auxiliares_AudiovisualAPP(string APP, string disponible)
        {
            try
            {

                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT * FROM[SMARTH_DB].[dbo].[AUX_Apoyo_AudiovisualAPP] WHERE APP = '"+APP+"' "+disponible+" AND Eliminado = 0";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_Auxiliares_AudiovisualAPPxID(string Id)
        {
            try
            {

                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT * FROM[SMARTH_DB].[dbo].[AUX_Apoyo_AudiovisualAPP] WHERE Id = " + Id + "";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public bool Inserta_Auxiliares_AudiovisualAPP(string URL, string APP, string disponible, string descripcion)
        {
            try
            {

                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[AUX_Apoyo_AudiovisualAPP]" +
                               "([Tipo],[URL],[Disponible],[Eliminado],[APP],[Descripcion])"+
                               " VALUES (1, '"+URL+"', "+disponible+", 0, '"+APP+"','"+descripcion+"')";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }


        public bool Elimina_Auxiliares_AudiovisualAPP(string ID)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "UPDATE [SMARTH_DB].[dbo].[AUX_Apoyo_AudiovisualAPP] SET [Eliminado] = 1 WHERE Id = " + ID + "";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }

        public bool Actualiza_Auxiliares_AudiovisualAPP(string ID, string disponible, string descripcion)
        {
            try
            {
              
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "UPDATE [SMARTH_DB].[dbo].[AUX_Apoyo_AudiovisualAPP] SET [Descripcion] = '"+descripcion+ "', [Disponible] = "+disponible+"  WHERE Id = " + ID + "";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }

        public DataTable Devuelve_Auxiliares_Splash()
        {
            try
            {

                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT * FROM[SMARTH_DB].[dbo].[AUX_Apoyo_Audiovisual] WHERE SplashVisitasDisponible = 1 and SplashVisitasEliminado <> 1";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_Auxiliares_Audiovisual()
        {
            try
            {

                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT * FROM[SMARTH_DB].[dbo].[AUX_Apoyo_Audiovisual] WHERE VideoKPIDisponible = 1 and VideoKPIEliminado <> 1";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }


        public DataTable Devuelve_DashboardOEECALPLanta()
        {
            try
            {

                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT YAR.[EtiquetaAÑO] AS AÑO, MON.EtiquetaMES AS EtiquetaMES,CASE WHEN OEECalidad IS NULL THEN 0 ELSE OEECalidad END AS OEECalidad,CASE WHEN OEECalidadOBJ IS NULL THEN 0 ELSE OEECalidadOBJ END AS OEECalidadOBJ,CASE WHEN OEEPlanta IS NULL THEN 0 ELSE OEEPlanta END AS OEEPlanta,CASE WHEN OEEPlantaOBJ IS NULL THEN 0 ELSE OEEPlantaOBJ END AS OEEPlantaOBJ" +
                                " FROM[SMARTH_DB].[dbo].[KPI_Labels_Graficos] YAR" +
                                " CROSS JOIN[SMARTH_DB].[dbo].[KPI_Labels_Graficos] MON" +
                                " LEFT JOIN(SELECT KPI.[Año], KPI.[Mes], lbl.[EtiquetaMes],[OEECalidad], OBJ.[22_1] AS OEECalidadOBJ,[OEEPlanta], OBJ.[31_1] AS OEEPlantaOBJ" +
                                            " FROM(SELECT[Año],[Mes],[OEEPlanta],[OEECalidad]" +
                                                    " FROM[SMARTH_DB].[dbo].[KPI_Panel_General_Sandbox]" +
                                                    " UNION ALL" +
                                                    " SELECT[Año], 13 AS Mes, ROUND(AVG([OEEPlanta]), 2) AS OEEPlanta, ROUND(AVG([OEECalidad]), 2) AS OEECalidad" +
                                                    " FROM[SMARTH_DB].[dbo].[KPI_Panel_General_Sandbox]" +
                                                    " GROUP BY Año) KPI" +
                                                  " LEFT JOIN[SMARTH_DB].[dbo].[KPI_Objetivos_Anuales] OBJ ON OBJ.Año = CAST(YEAR(SYSDATETIME()) AS int)" +
                                                  " LEFT JOIN SMARTH_DB.dbo.KPI_Labels_Graficos LBL ON KPI.Mes = LBL.Id) QUR ON YAR.EtiquetaAÑO = QUR.Año AND MON.EtiquetaMES = QUR.EtiquetaMES" +
                              " WHERE YAR.EtiquetaAÑO IS NOT NULL" +
                              " ORDER BY Año ASC";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_DashboardOEECALPLantaTendencia()
        {
            try
            {

                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = " SELECT TOP (12) KPI.[Año],[Mes],[OEEPlanta],[OEECalidad], lbl.[EtiquetaMes], lbl.[EtiquetaMes] + ' '+  RIGHT(CAST(KPI.[Año] AS varchar),2) as EtiquetaGraph, [OEECalidad], OBJ.[22_1] AS OEECalidadOBJ,[OEEPlanta], OBJ.[31_1] AS OEEPlantaOBJ" +
                                " FROM[SMARTH_DB].[dbo].[KPI_Panel_General_Sandbox] KPI" +
                                " LEFT JOIN[SMARTH_DB].[dbo].[KPI_Objetivos_Anuales] OBJ ON OBJ.Año = CAST(YEAR(SYSDATETIME()) AS int)" +
                                " LEFT JOIN SMARTH_DB.dbo.KPI_Labels_Graficos LBL ON KPI.Mes = LBL.Id" +
                               " ORDER BY Año DESC, Mes DESC";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }


        public DataTable Devuelve_DashboardNoConformidades()
        {
            try
            {

                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT YAR.[EtiquetaAÑO] AS AÑO, MON.EtiquetaMES AS EtiquetaMES,KPI.[25_1] AS NCOBJETIVO, CASE WHEN NC.NOCONFORMIDADES IS NULL THEN 0 ELSE NC.NOCONFORMIDADES END AS NOCONFORMIDADES"+
                                    " FROM[SMARTH_DB].[dbo].[KPI_Labels_Graficos] YAR"+
                                " CROSS JOIN[SMARTH_DB].[dbo].[KPI_Labels_Graficos] MON"+
                                " LEFT JOIN[SMARTH_DB].[dbo].[KPI_Objetivos_Anuales] KPI ON KPI.Año = YEAR(SYSDATETIME())"+
                                " LEFT JOIN(SELECT YEAR([FechaOriginal]) AS YEAR, MONTH([FechaOriginal]) AS MESNUM, COUNT([TipoNoConformidad]) as NOCONFORMIDADES"+
                                            " FROM[SMARTH_DB].[dbo].[NC_Alerta_de_calidad]"+
                                            " WHERE EscaladoNoConformidad = 2 and TipoNoConformidad = 2" +
                                            " GROUP BY YEAR([FechaOriginal]), MONTH([FechaOriginal])" +
                                                " UNION SELECT YEAR, 13 AS MESNUM, ROUND((CAST(NOCONFORMIDADES AS real) / MONTHNUM), 2) AS NOCONFORMIDADES" +
                                                  " FROM(SELECT YEAR([FechaOriginal]) AS YEAR, COUNT([TipoNoConformidad]) as NOCONFORMIDADES" +
                                                  " FROM[SMARTH_DB].[dbo].[NC_Alerta_de_calidad]" +
                                                  " WHERE EscaladoNoConformidad = 2 and TipoNoConformidad = 2" +
                                                  " GROUP BY YEAR([FechaOriginal])) ALE" +
                                " LEFT JOIN(SELECT YEAR([FechaInicio]) AS AÑO, MAX(MONTH([FechaInicio])) AS MONTHNUM FROM[SMARTH_DB].[dbo].[GP12_Historico_2021] GROUP BY YEAR([FechaInicio])) GRP ON ALE.YEAR = GRP.AÑO) NC ON MON.Id = NC.MESNUM AND YAR.EtiquetaAÑO = NC.YEAR" +
                                " WHERE YAR.EtiquetaAÑO IS NOT NULL";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_DashboardNoConformidadesTendencia()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT TOP (12) ALE.AÑO, MESNUM, CASE WHEN NOCONFORMIDADES IS NULL THEN 0 ELSE NOCONFORMIDADES END AS NOCONFORMIDADES, KPI.[25_1] AS NCOBJETIVO, lbl.[EtiquetaMes] + ' '+  RIGHT(CAST(ALE.[Año] AS varchar),2) as EtiquetaGraph"+
                                    " FROM(SELECT YEAR([FechaOriginal]) AS AÑO, MONTH([FechaOriginal]) AS MESNUM, COUNT([TipoNoConformidad]) as NOCONFORMIDADES"+
                                             " FROM[SMARTH_DB].[dbo].[NC_Alerta_de_calidad]"+
                                             " WHERE EscaladoNoConformidad = 2 and TipoNoConformidad = 2"+
                                             " GROUP BY YEAR([FechaOriginal]), MONTH([FechaOriginal])) ALE"+
                                    " LEFT JOIN[SMARTH_DB].[dbo].[KPI_Objetivos_Anuales] KPI ON KPI.Año = YEAR(SYSDATETIME())"+
                                    " LEFT JOIN[SMARTH_DB].[dbo].[KPI_Labels_Graficos] lbl ON ALE.MESNUM = lbl.Id"+
                                    " ORDER BY ALE.AÑO DESC, MESNUM DESC";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }


        public DataTable Devuelve_DashboardAprovechamientoOPS()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT YAR.[EtiquetaAÑO] AS AÑO,MON.EtiquetaMES AS EtiquetaMES,KPI.XXPS1_1 AS OBJETIVO,CASE WHEN OPS.OPLIBRES IS NULL THEN 0 ELSE OPS.OPLIBRES END AS OPLIBRES"+
                                  " FROM[SMARTH_DB].[dbo].[KPI_Labels_Graficos] YAR" +
                                        " CROSS JOIN[SMARTH_DB].[dbo].[KPI_Labels_Graficos] MON" +
                                        " LEFT JOIN[SMARTH_DB].[dbo].[KPI_Objetivos_Anuales] KPI ON KPI.Año = YEAR(SYSDATETIME())" +
                                        " LEFT JOIN(SELECT YEAR([Fecha]) as YEAR, MONTH([Fecha]) as MES, ROUND(AVG([OPLIBRES]),2) AS OPLIBRES" +
                                                    " FROM[SMARTH_DB].[dbo].[KPI_Aprovechamiento]" +
                                                    " WHERE[OPLOGUEADOS] > 0" +
                                                    " GROUP BY YEAR([Fecha]), MONTH([Fecha]) " +
                                                    " UNION SELECT YEAR([Fecha]) as YEAR, 13 as MES, ROUND(AVG([OPLIBRES]),2) AS OPLIBRES" +
                                                    " FROM[SMARTH_DB].[dbo].[KPI_Aprovechamiento]" +
                                                    " WHERE[OPLOGUEADOS] > 0" +
                                                    " GROUP BY YEAR([Fecha]))OPS ON YAR.EtiquetaAÑO = OPS.YEAR AND MON.Id = OPS.MES" +
                                  " WHERE YAR.EtiquetaAÑO IS NOT NULL";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_DashboardAprovechamientoOPSTendencias()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT TOP (12) ALE.AÑO, MESNUM, CASE WHEN OPLIBRES IS NULL THEN 0 ELSE OPLIBRES END AS OPLIBRES, KPI.XXPS1_1 AS OBJETIVO, lbl.[EtiquetaMes] + ' '+  RIGHT(CAST(ALE.[Año] AS varchar),2) as EtiquetaGraph" +
                                    " FROM(SELECT YEAR([Fecha]) as AÑO, MONTH([Fecha]) as MESNUM, ROUND(AVG([OPLIBRES]),2) AS OPLIBRES" +
                                            " FROM[SMARTH_DB].[dbo].[KPI_Aprovechamiento]" +
                                            " WHERE[OPLOGUEADOS] > 0" +
                                            " GROUP BY YEAR([Fecha]), MONTH([Fecha])) ALE" +
                                     " LEFT JOIN[SMARTH_DB].[dbo].[KPI_Objetivos_Anuales] KPI ON KPI.Año = YEAR(SYSDATETIME())" +
                                     " LEFT JOIN[SMARTH_DB].[dbo].[KPI_Labels_Graficos] lbl ON ALE.MESNUM = lbl.Id" +
                                     " Order by AÑO DESC, MESNUM DESC";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_DashboardAbsentismo()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = " SELECT YAR.[EtiquetaAÑO] AS AÑO, MON.EtiquetaMES AS EtiquetaMES,KPI.[91_1] AS OBJETIVO, CASE WHEN OPS.PORCABS IS NULL THEN 0 ELSE OPS.PORCABS END AS PORCABS" +
                                   " FROM[SMARTH_DB].[dbo].[KPI_Labels_Graficos] YAR" +
                                         " CROSS JOIN[SMARTH_DB].[dbo].[KPI_Labels_Graficos] MON" +
                                         " LEFT JOIN[SMARTH_DB].[dbo].[KPI_Objetivos_Anuales] KPI ON KPI.Año = YEAR(SYSDATETIME())" +
                                         " LEFT JOIN(SELECT*, ROUND(CAST((LARGADURACION+ACCIDENTE + ENFERMEDAD + LICENCIAS + SINDICALES) AS real) *100 / CAST((MINTEORICO) AS real),2) AS PORCABS" +
                                                    " FROM(SELECT YEAR([Fecha]) AS YEAR, MONTH([Fecha]) AS MES, SUM([MINTEORICO])/ 60 AS MINTEORICO, SUM([ENFERMEDADLARGA])/ 60 as LARGADURACION,(SUM([ACCIDENTELAB]) + SUM(BAJALAB))/ 60 AS ACCIDENTE,(SUM([ENFERMEDAD]))/ 60 AS ENFERMEDAD" +
                                                             " ,(SUM([LACTANCIA30MIN]) +SUM([LACTANCIA60MIN]) +SUM([VISITAMEDICO]) +SUM([ENFERMEDADFAMILIAR]) +SUM([EXAMENES]) +SUM([DEBERINEXCUSABLE]) +SUM([NACIMIENTOHIJO]) +SUM([MATRIMONIOS]) +SUM([MATERNIDAD]) +SUM([LICENCIASVARIAS]) +SUM([PATERNIDAD]) +SUM([LACTANCIACOMPACTACION]) +SUM([FALLECIMIENTOFAM]) +SUM([CAMBIODOMICILIO]) +SUM([LICENCIABODA]) +SUM([PERMISORETRIBUIDO]) +SUM([PERMISONORETRIBUIDO]) +SUM([RETRASOAPROBADO]))/ 60 AS LICENCIAS, SUM([HORASSINDICALES])/ 60 AS SINDICALES" +
                                                    " FROM[SMARTH_DB].[dbo].[KPI_Absentismo]" +
                                                    " GROUP BY YEAR([Fecha]),MONTH([Fecha])) ABT" +
                                                    " UNION"+
                                                    " SELECT *, ROUND(CAST((LARGADURACION + ACCIDENTE + ENFERMEDAD + LICENCIAS + SINDICALES) AS real) * 100 / CAST(MINTEORICO AS real), 2) AS PORCABS"+
                                                    " FROM(SELECT YEAR([Fecha]) AS YEAR,13 AS MES, SUM([MINTEORICO])/ 60 AS MINTEORICO, SUM([ENFERMEDADLARGA])/ 60 as LARGADURACION,(SUM([ACCIDENTELAB]) + SUM(BAJALAB))/ 60 AS ACCIDENTE,(SUM([ENFERMEDAD]))/ 60 AS ENFERMEDAD"+
                                                                " ,(SUM([LACTANCIA30MIN]) +SUM([LACTANCIA60MIN]) +SUM([VISITAMEDICO]) +SUM([ENFERMEDADFAMILIAR]) +SUM([EXAMENES]) +SUM([DEBERINEXCUSABLE]) +SUM([NACIMIENTOHIJO]) +SUM([MATRIMONIOS]) +SUM([MATERNIDAD]) +SUM([LICENCIASVARIAS]) +SUM([PATERNIDAD]) +SUM([LACTANCIACOMPACTACION]) +SUM([FALLECIMIENTOFAM]) +SUM([CAMBIODOMICILIO]) +SUM([LICENCIABODA]) +SUM([PERMISORETRIBUIDO]) +SUM([PERMISONORETRIBUIDO]) +SUM([RETRASOAPROBADO]))/ 60 AS LICENCIAS, SUM([HORASSINDICALES])/ 60 AS SINDICALES"+
                                                      " FROM[SMARTH_DB].[dbo].[KPI_Absentismo]"+
                                                      " GROUP BY YEAR([Fecha])) ABT"+
                                    " )OPS ON YAR.EtiquetaAÑO = OPS.YEAR AND MON.Id = OPS.MES" +
                                    " WHERE YAR.EtiquetaAÑO IS NOT NULL";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_DashboardAbsentismoTendencias()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = " SELECT TOP(12) ALE.YEAR, ALE.MES, ROUND(CAST((LARGADURACION+ACCIDENTE + ENFERMEDAD + LICENCIAS + SINDICALES) AS real) *100 / CAST((MINTEORICO) AS real),2) AS PORCABS, KPI.[91_1] AS OBJETIVO, lbl.[EtiquetaMes] + ' '+  RIGHT(CAST(ALE.[YEAR] AS varchar),2) as EtiquetaGraph"+
                                                    " FROM(SELECT YEAR([Fecha]) AS YEAR, MONTH([Fecha]) AS MES, SUM([MINTEORICO])/ 60 AS MINTEORICO, SUM([ENFERMEDADLARGA])/ 60 as LARGADURACION,(SUM([ACCIDENTELAB]) + SUM(BAJALAB))/ 60 AS ACCIDENTE,(SUM([ENFERMEDAD]))/ 60 AS ENFERMEDAD" +
                                                             " ,(SUM([LACTANCIA30MIN]) +SUM([LACTANCIA60MIN]) +SUM([VISITAMEDICO]) +SUM([ENFERMEDADFAMILIAR]) +SUM([EXAMENES]) +SUM([DEBERINEXCUSABLE]) +SUM([NACIMIENTOHIJO]) +SUM([MATRIMONIOS]) +SUM([MATERNIDAD]) +SUM([LICENCIASVARIAS]) +SUM([PATERNIDAD]) +SUM([LACTANCIACOMPACTACION]) +SUM([FALLECIMIENTOFAM]) +SUM([CAMBIODOMICILIO]) +SUM([LICENCIABODA]) +SUM([PERMISORETRIBUIDO]) +SUM([PERMISONORETRIBUIDO]) +SUM([RETRASOAPROBADO]))/ 60 AS LICENCIAS, SUM([HORASSINDICALES])/ 60 AS SINDICALES" +
                                                    " FROM[SMARTH_DB].[dbo].[KPI_Absentismo]" +
                                                    " GROUP BY YEAR([Fecha]),MONTH([Fecha])) ALE" +
                                                    " LEFT JOIN[SMARTH_DB].[dbo].[KPI_Labels_Graficos] lbl ON ALE.MES = lbl.Id" +
                                                    " LEFT JOIN[SMARTH_DB].[dbo].[KPI_Objetivos_Anuales] KPI ON KPI.Año = YEAR(SYSDATETIME())" +
                                                    " ORDER BY YEAR DESC, MES DESC";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        ///////////\\\\\\\\\\\\\KPI NO CONFORMIDADES////////////\\\\\\\\\\\\\\\\

        public DataSet Devuelve_kpi_Calidad(string año, string sector, string tipo)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
               
                string sql = "SELECT YEAR, MES, MESNUM, SUM(NCOFICIALES) AS NCOFICIALES, SUM(QINFO) AS QINFO, SUM(PROVEEDOR) AS PROVEEDOR, SUM(CANTIDADOFI)+SUM(CANTIDADQINFO) AS PIEZASNOK, AVG(CantidadEnviada) AS CantidadEnviada, SUM(CANTIDADPPM) AS CANTIDADPPM,((CAST( SUM(CANTIDADOFI) AS decimal) + CAST(SUM(CANTIDADQINFO) AS decimal)) / CAST(AVG(CantidadEnviada) AS DECIMAL) * 1000000) AS PPM, SUM(REPITEDEFECTO) AS REPITEDEFECTO, SUM(REPITEREFERENCIA) AS REPITEREFERENCIA" +
                            " FROM(SELECT  year(NC.[FechaOriginal]) as YEAR, month(NC.[FechaOriginal]) AS MESNUM, DateName(month, NC.[FechaOriginal]) as MES,SUM(AL.CantidadPPM) AS CANTIDADPPM," +
                            //" CASE WHEN(AL.EscaladoNoConformidad = 2 AND AL.TipoNoConformidad = 2) THEN COUNT(month(NC.[FechaOriginal])) ELSE 0 END AS NCOFICIALES," +
                            //" CASE WHEN(AL.EscaladoNoConformidad = 2 AND AL.TipoNoConformidad = 2) THEN SUM(AL.Cantidad) ELSE 0 END AS CANTIDADOFI," +
                            //" CASE WHEN(AL.EscaladoNoConformidad = 1 AND AL.TipoNoConformidad = 2) THEN COUNT(month(NC.[FechaOriginal])) ELSE 0 END AS QINFO," +
                            //" CASE WHEN(AL.EscaladoNoConformidad = 1 AND AL.TipoNoConformidad = 2) THEN SUM(AL.Cantidad)  ELSE 0 END AS CANTIDADQINFO," +
                            //" CASE WHEN(AL.TipoNoConformidad = 1) THEN COUNT(month(NC.[FechaOriginal])) ELSE 0 END AS PROVEEDOR, ENV.CantidadEnviada" +
                            " CASE WHEN(AL.EscaladoNoConformidad = 2) THEN COUNT(month(NC.[FechaOriginal])) ELSE 0 END AS NCOFICIALES," +
                            " CASE WHEN(AL.EscaladoNoConformidad = 2) THEN SUM(AL.Cantidad) ELSE 0 END AS CANTIDADOFI," +
                            " CASE WHEN(AL.EscaladoNoConformidad = 1) THEN COUNT(month(NC.[FechaOriginal])) ELSE 0 END AS QINFO," +
                            " CASE WHEN(AL.EscaladoNoConformidad = 1) THEN SUM(AL.Cantidad)  ELSE 0 END AS CANTIDADQINFO," +
                            " CASE WHEN(AL.TipoNoConformidad = 1) THEN COUNT(month(NC.[FechaOriginal])) ELSE 0 END AS PROVEEDOR, " +
                            " SUM(cast([RepiteDefecto] as int)) AS REPITEDEFECTO, SUM(cast([RepiteReferencia] as int)) AS REPITEREFERENCIA, ENV.CantidadEnviada" +
                            " FROM[SMARTH_DB].[dbo].[NC_Estado_No_Conformidad] NC" +
                            " LEFT JOIN[SMARTH_DB].[dbo].[NC_Alerta_de_calidad] AL ON NC.IdNoConformidad = AL.IdNoConformidad" +
                            " LEFT JOIN[SMARTH_DB].[dbo].[AUX_TablaProductos] PRO ON AL.Referencia = PRO.Referencia"+
                            " LEFT JOIN[SMARTH_DB].[dbo].[AUX_Piezas_Pz_Enviadas_NAV] ENV ON year(NC.[FechaOriginal]) = ENV.Año AND month(NC.[FechaOriginal]) = ENV.Mes" +
                            " WHERE MES <> '' " + sector+ "" + tipo + "" +
                            " GROUP BY year(NC.[FechaOriginal]), DateName(month, NC.[FechaOriginal]), month(NC.[FechaOriginal]), AL.EscaladoNoConformidad, AL.TipoNoConformidad, CantidadEnviada) A" +
                            " WHERE YEAR = '"+año+"'" +
                            " GROUP BY YEAR, MESNUM, MES ORDER BY MESNUM";


                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataSet Devuelve_kpi_Costes_Calidad(string año, string sector)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT YEAR(NC.[FechaOriginal]) AS AÑO, datename(MONTH, NC.[FechaOriginal]) AS MES, MONTH(NC.[FechaOriginal]) AS MESNUM, SUM([CosteSeleccionEXT]) AS SELECCIONEXT, SUM([CostePiezasNOKEXT]) AS CHATARRAEXT, SUM([CosteCargosEXT]) AS CARGOSEXT, SUM([CosteAdmonEXT]) AS ADMONEXT, SUM([CosteOtrosINT]) AS OTROSINT, GP.COSTE AS GP12,CH.CosteChatarra AS CHATARRA,SUM([CosteSeleccionEXT] +[CostePiezasNOKEXT] +[CosteCargosEXT] +[CosteAdmonEXT] +[CosteOtrosINT])+GP.COSTE + CH.CosteChatarra AS CosteNoCalidad,"+
                              " CH.CosteArranque AS ARRANQUE, SUM([CosteSeleccionEXT] +[CostePiezasNOKEXT] +[CosteCargosEXT] +[CosteAdmonEXT] +[CosteOtrosINT])+GP.COSTE + CH.CosteChatarra + ch.CosteArranque as CosteTotal" +
                              " FROM[SMARTH_DB].[dbo].[NC_Estado_No_Conformidad] NC" +
                                " LEFT JOIN [SMARTH_DB].[dbo].[NC_Alerta_de_calidad] AL ON NC.IdNoConformidad = AL.IdNoConformidad"+
                                " LEFT JOIN[SMARTH_DB].[dbo].[AUX_TablaProductos] PRO ON AL.Referencia = PRO.Referencia"+
                              " LEFT JOIN(SELECT YEAR([FechaInicio]) AS AÑO, MONTH([FechaInicio]) AS MES, SUM([CosteRevision]) AS COSTE FROM[SMARTH_DB].[dbo].[GP12_Historico_2021]" +
                              " GROUP BY YEAR([FechaInicio]), MONTH([FechaInicio])) GP ON MONTH(NC.FechaOriginal) = GP.[MES] and YEAR(NC.FechaOriginal) = GP.Año" +
                              " LEFT JOIN[SMARTH_DB].[dbo].[AUX_Piezas_Chatarras_Costes_BMS] CH ON MONTH(NC.FechaOriginal) = CH.Mes and YEAR(NC.FechaOriginal) = CH.Año" +
                              " WHERE YEAR(NC.[FechaOriginal]) = '"+año+"' GROUP BY YEAR(NC.[FechaOriginal]), datename(MONTH, NC.[FechaOriginal]), MONTH(NC.[FechaOriginal]), GP.COSTE, CH.CosteChatarra, CH.CosteArranque ORDER BY MESNUM";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_GP12.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_GP12.Close();
                return null;
            }
        }


        ///////////\\\\\\\\\\\\\KPI  MANTENIMIENTO////////////\\\\\\\\\\\\\\\\

        public DataSet Devuelve_Resultados_Mantenimiento_Maquinas(int año)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql1 = "SELECT MAQ.AÑO, MAQ.MESTEXTO, ROUND(HOR.HorasProduciendo,0) AS HorasProduciendo, ESTIMADASMAQ, ROUND(REALESMAQ,2) AS REALESMAQ,  ROUND((REALESMAQ*100)/HOR.HorasProduciendo,2) as PORCCORRECTIVO, CASE WHEN KPI.[102_1] IS NULL THEN 0.0 ELSE KPI.[102_1] END AS KPICOR, ESTIMADASPREV, ROUND(REALESPREV,2) AS REALESPREV, ROUND((REALESPREV*100)/HOR.HorasProduciendo,2) as PORCPREVENTIVO, CASE WHEN KPI.[101_2] IS NULL THEN 0.0 ELSE KPI.[101_2] END AS KPIPREV, COSTESREPUESTOS, COSTESOPERARIOS, COSTESTOTALES, PARTESCORR, PARTESPREV, COSTESOPERARIOSCORR, COSTESREPUESTOSCORR, COSTESTOTALESCORR, COSTESOPERARIOSPREV, COSTESREPUESTOSPREV, COSTESTOTALESPREV" +
                                    " FROM(SELECT  YEAR(convert(datetime, FechaAveria, 103)) as AÑO, DATENAME(MONTH, (convert(datetime, FechaAveria, 103))) as MESTEXTO, MAX(MONTH(convert(datetime, FechaAveria, 103))) as MES, SUM([HorasEstimadasReparacion]) AS ESTIMADASMAQ, SUM([HorasRealesReparacion]) AS REALESMAQ, COUNT([HorasRealesReparacion]) AS PARTESCORR, SUM([ImporteEmpresa1]) AS COSTESREPUESTOSCORR, SUM([ImporteEmpresa2]) AS COSTESOPERARIOSCORR, SUM([ImporteEmpreza3]) AS COSTESTOTALESCORR  FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas]" +
                                         " where FechaAveria is not null and FechaAveria <> '' and YEAR(convert(datetime, FechaAveria, 103)) = '"+año+"' AND IdTipoMantenimiento <= 1 GROUP BY YEAR(convert(datetime, FechaAveria, 103)), DATENAME(MONTH, (convert(datetime, FechaAveria, 103)))) MAQ" +
                                    " LEFT JOIN(SELECT  YEAR(convert(datetime, FechaAveria, 103)) as AÑO, DATENAME(MONTH, (convert(datetime, FechaAveria, 103))) as MESTEXTO, MAX(MONTH(convert(datetime, FechaAveria, 103))) as MES, SUM([HorasEstimadasReparacion]) AS ESTIMADASPREV, SUM([HorasRealesReparacion]) AS REALESPREV, COUNT([HorasRealesReparacion]) AS PARTESPREV, SUM([ImporteEmpresa1]) AS COSTESREPUESTOSPREV, SUM([ImporteEmpresa2]) AS COSTESOPERARIOSPREV, SUM([ImporteEmpreza3]) AS COSTESTOTALESPREV   FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas]" +
                                         " where FechaAveria is not null and FechaAveria <> '' and YEAR(convert(datetime, FechaAveria, 103)) = '" + año + "' AND IdTipoMantenimiento > 1 GROUP BY YEAR(convert(datetime, FechaAveria, 103)), DATENAME(MONTH, (convert(datetime, FechaAveria, 103)))) PRE ON MAQ.AÑO = PRE.AÑO AND MAQ.MES = PRE.MES" +
                                    " LEFT JOIN(SELECT YEAR (convert(datetime, FechaAveria, 103)) as AÑO, DATENAME(MONTH, (convert(datetime, FechaAveria, 103))) as MESTEXTO, MAX(MONTH(convert(datetime, FechaAveria, 103))) as MES, SUM([ImporteEmpresa1]) AS COSTESREPUESTOS, SUM([ImporteEmpresa2]) AS COSTESOPERARIOS, SUM([ImporteEmpreza3]) AS COSTESTOTALES FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas]" +
                                         " where FechaAveria is not null and FechaAveria <> '' and YEAR(convert(datetime, FechaAveria, 103)) = '" + año + "' GROUP BY YEAR(convert(datetime, FechaAveria, 103)), DATENAME(MONTH, (convert(datetime, FechaAveria, 103)))) CST ON MAQ.AÑO = CST.AÑO AND MAQ.MES = CST.MES" +
                                    " LEFT JOIN [SMARTH_DB].[dbo].[AUX_Horas_Disponibles_Fabrica] HOR ON MAQ.AÑO = HOR.Año AND MAQ.MES = HOR.MES"+
                                    " LEFT JOIN[SMARTH_DB].[dbo].[KPI_Objetivos_Anuales] KPI ON MAQ.AÑO = KPI.AÑO"+
                                    " order by MAQ.MES";
                SqlDataAdapter adapter = new SqlDataAdapter(sql1, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql1);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataSet Devuelve_TOTAL_Resultados_Mantenimiento_Maquinas(int año)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql1 = "SELECT ROUND(REALESMAQ,2) AS REALESMAQ, ROUND(REALESPREV,2) AS REALESPREV, CAST(CAST(COSTESTOTALES AS INT) AS VARCHAR) + ' €' AS COSTETOTALES, PARTES" +
                                     " FROM(SELECT  YEAR(convert(datetime, FechaAveria, 103)) as AÑO, SUM([HorasEstimadasReparacion]) AS ESTIMADASMAQ, SUM([HorasRealesReparacion]) AS REALESMAQ, COUNT([HorasRealesReparacion]) AS PARTESCORR, SUM([ImporteEmpresa1]) AS COSTESREPUESTOSCORR, SUM([ImporteEmpresa2]) AS COSTESOPERARIOSCORR, SUM([ImporteEmpreza3]) AS COSTESTOTALESCORR  FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas]" +
                                          " where FechaAveria is not null and FechaAveria <> '' and YEAR(convert(datetime, FechaAveria, 103)) = '"+año+"' AND IdTipoMantenimiento <= 1 GROUP BY YEAR(convert(datetime, FechaAveria, 103))) MAQ" +
                                     " LEFT JOIN(SELECT  YEAR(convert(datetime, FechaAveria, 103)) as AÑO, SUM([HorasEstimadasReparacion]) AS ESTIMADASPREV, SUM([HorasRealesReparacion]) AS REALESPREV, COUNT([HorasRealesReparacion]) AS PARTESPREV, SUM([ImporteEmpresa1]) AS COSTESREPUESTOSPREV, SUM([ImporteEmpresa2]) AS COSTESOPERARIOSPREV, SUM([ImporteEmpreza3]) AS COSTESTOTALESPREV   FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas]" +
                                          " where FechaAveria is not null and FechaAveria <> '' and YEAR(convert(datetime, FechaAveria, 103)) = '" + año + "' AND IdTipoMantenimiento > 1 GROUP BY YEAR(convert(datetime, FechaAveria, 103))) PRE ON MAQ.AÑO = PRE.AÑO" +
                                     " LEFT JOIN(SELECT YEAR (convert(datetime, FechaAveria, 103)) as AÑO, SUM([ImporteEmpresa1]) AS COSTESREPUESTOS, SUM([ImporteEmpresa2]) AS COSTESOPERARIOS, SUM([ImporteEmpreza3]) AS COSTESTOTALES, COUNT([ImporteEmpresa1]) AS PARTES FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas]" +
                                          " where FechaAveria is not null and FechaAveria <> '' and YEAR(convert(datetime, FechaAveria, 103)) = '" + año + "' GROUP BY YEAR(convert(datetime, FechaAveria, 103))) CST ON MAQ.AÑO = CST.AÑO";
                SqlDataAdapter adapter = new SqlDataAdapter(sql1, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql1);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataSet Devuelve_Ranking_AperturaPartes_Maquinas(int año, string WHERE_MES, string WHERE_NOMBRE)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql1 = "SELECT AÑO, MESTEXTO, Nombre, PARTES" +
                                " FROM(SELECT YEAR(convert(datetime, FechaAveria, 103)) as AÑO, DATENAME(MONTH, (convert(datetime, FechaAveria, 103))) as MESTEXTO, MAX(MONTH(convert(datetime, FechaAveria, 103))) as MES, IdEncargado, COUNT(IdEncargado) AS PARTES" +
                                " FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas]" +
                                " Where FechaAveria is not null and FechaAveria <> '' and YEAR(convert(datetime, FechaAveria, 103)) = '" + año + "'" +
                                " GROUP BY YEAR(convert(datetime, FechaAveria, 103)), DATENAME(MONTH, (convert(datetime, FechaAveria, 103))), IdEncargado) NUM" +
                                " LEFT JOIN SMARTH_DB.DBO.AUX_Personal_Mandos PER ON NUM.IdEncargado = PER.Id" +
                                " WHERE AÑO = '" + año + "'" + WHERE_MES + "" + WHERE_NOMBRE + "" +
                                " order by MES ASC, PARTES DESC";
                SqlDataAdapter adapter = new SqlDataAdapter(sql1, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql1);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataSet Devuelve_Ranking_AperturaPartes_MaquinasAÑO(int año, string WHERE_MES, string WHERE_NOMBRE)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql1 = "SELECT AÑO, Nombre, PARTES" +
                                " FROM(SELECT YEAR(convert(datetime, FechaAveria, 103)) as AÑO, IdEncargado, COUNT(IdEncargado) AS PARTES" +
                                " FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas]" +
                                " Where FechaAveria is not null and FechaAveria <> '' and YEAR(convert(datetime, FechaAveria, 103)) = '" + año + "'" +
                                " GROUP BY YEAR(convert(datetime, FechaAveria, 103)), IdEncargado) NUM" +
                                " LEFT JOIN SMARTH_DB.DBO.AUX_Personal_Mandos PER ON NUM.IdEncargado = PER.Id" +
                                " WHERE AÑO = '" + año + "'" + WHERE_MES + "" + WHERE_NOMBRE + "" +
                                " order by PARTES DESC";
                SqlDataAdapter adapter = new SqlDataAdapter(sql1, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql1);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataSet Devuelve_Ranking_Maquinas_PartesAbiertos(int año, string WHERE_MES, string WHERE_NOMBRE)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql1 = " SELECT TOP(10) AÑO, MESTEXTO, PER.Maquina, PARTES, NUM.IdMaquina, PER.IdMaquinaCHAR, HORAS, ROUND(HORAS/PARTES,2) AS MTTR" +
                                 " FROM(SELECT YEAR(convert(datetime, FechaAveria, 103)) as AÑO, DATENAME(MONTH, (convert(datetime, FechaAveria, 103))) as MESTEXTO, MAX(MONTH(convert(datetime, FechaAveria, 103))) as MES, IDMaquina, COUNT(IDMaquina) AS PARTES, SUM(HorasRealesRepOP1+HorasRealesRepOP2+HorasRealesRepOP3) AS HORAS" +
                                 " FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas]" +
                                 " Where FechaAveria is not null and FechaAveria <> '' and YEAR(convert(datetime, FechaAveria, 103)) = '" + año + "'" +
                                 " GROUP BY YEAR(convert(datetime, FechaAveria, 103)), DATENAME(MONTH, (convert(datetime, FechaAveria, 103))), IDMaquina) NUM" +
                                 " LEFT JOIN SMARTH_DB.DBO.AUX_Lista_Máquinas PER ON NUM.IDMaquina = PER.IdMaquina"+
                                 " WHERE AÑO = '" + año + "'" + WHERE_MES + "" + WHERE_NOMBRE + "" +
                                 " order by MES ASC, MTTR DESC";

                SqlDataAdapter adapter = new SqlDataAdapter(sql1, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql1);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataSet Devuelve_Ranking_Maquinas_PartesAbiertosMTBF(int año, string WHERE_MES, string WHERE_NOMBRE)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql1 = " SELECT TOP(10) NUM.AÑO, MESTEXTO, PER.Maquina, PARTES, NUM.IdMaquina, PER.IdMaquinaCHAR, HORAS, MAQ.HorasProduciendo, CASE WHEN HorasProduciendo IS NULL THEN 0.0 ELSE ROUND(MAQ.HorasProduciendo/PARTES,2) END as MTBF, CASE WHEN HorasProduciendo IS NULL THEN 0.0 ELSE ROUND(1/(MAQ.HorasProduciendo/PARTES),2) END as MTBFINVERSO" +
                                 " FROM(SELECT YEAR(convert(datetime, FechaAveria, 103)) as AÑO, DATENAME(MONTH, (convert(datetime, FechaAveria, 103))) as MESTEXTO, MAX(MONTH(convert(datetime, FechaAveria, 103))) as MES, IDMaquina, COUNT(IDMaquina) AS PARTES, SUM(HorasRealesRepOP1+HorasRealesRepOP2+HorasRealesRepOP3) AS HORAS" +
                                 " FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas]" +
                                 " Where FechaAveria is not null and FechaAveria <> '' and YEAR(convert(datetime, FechaAveria, 103)) = '" + año + "'" +
                                 " GROUP BY YEAR(convert(datetime, FechaAveria, 103)), DATENAME(MONTH, (convert(datetime, FechaAveria, 103))), IDMaquina) NUM" +
                                 " LEFT JOIN SMARTH_DB.DBO.AUX_Lista_Máquinas PER ON NUM.IDMaquina = PER.IdMaquina" +
                                 " LEFT JOIN[SMARTH_DB].[dbo].[AUX_Horas_Disponibles_Maquina] MAQ ON PER.IdMaquinaCHAR = MAQ.Maquina AND NUM.AÑO = MAQ.Año AND NUM.MES = MAQ.Mes"+
                                 " WHERE HorasProduciendo IS NOT NULL AND NUM.AÑO = '" + año + "'" + WHERE_MES + "" + WHERE_NOMBRE + "" +
                                 " order by NUM.MES ASC, MTBF ASC";

                SqlDataAdapter adapter = new SqlDataAdapter(sql1, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql1);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }


        public DataSet Devuelve_Ranking_Maquinas_PartesAbiertosAÑO(int año, string WHERE_MES, string WHERE_NOMBRE)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql1 = " SELECT TOP(10) AÑO, PER.Maquina, PARTES, NUM.IdMaquina, PER.IdMaquinaCHAR, HORAS, ROUND(HORAS/PARTES,2) AS MTTR" +
                                 " FROM(SELECT YEAR(convert(datetime, FechaAveria, 103)) as AÑO, MAX(MONTH(convert(datetime, FechaAveria, 103))) as MES, IDMaquina, COUNT(IDMaquina) AS PARTES, SUM(HorasRealesRepOP1+HorasRealesRepOP2+HorasRealesRepOP3) AS HORAS" +
                                 " FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas]" +
                                 " Where FechaAveria is not null and FechaAveria <> '' and YEAR(convert(datetime, FechaAveria, 103)) = '" + año + "'" +
                                 " GROUP BY YEAR(convert(datetime, FechaAveria, 103)), IDMaquina) NUM" +
                                 " LEFT JOIN SMARTH_DB.DBO.AUX_Lista_Máquinas PER ON NUM.IDMaquina = PER.IdMaquina" +
                                 " WHERE AÑO = '" + año + "'" + WHERE_MES + "" + WHERE_NOMBRE + "" +
                                 " order by MTTR DESC";

                SqlDataAdapter adapter = new SqlDataAdapter(sql1, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql1);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataSet Devuelve_Ranking_Maquinas_PartesAbiertosAÑOMTBF(int año, string WHERE_MES, string WHERE_NOMBRE)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql1 = " SELECT TOP(10) NUM.AÑO, PER.Maquina, PARTES, NUM.IdMaquina, PER.IdMaquinaCHAR, HORAS, MAQ.HorasProduciendo, CASE WHEN HorasProduciendo IS NULL THEN 0.0 ELSE ROUND(MAQ.HorasProduciendo/PARTES,2) END as MTBF, CASE WHEN HorasProduciendo IS NULL THEN 0.0 ELSE ROUND(1/(MAQ.HorasProduciendo/PARTES),4) END as MTBFINVERSO" +
                                 " FROM(SELECT YEAR(convert(datetime, FechaAveria, 103)) as AÑO, MAX(MONTH(convert(datetime, FechaAveria, 103))) as MES, IDMaquina, COUNT(IDMaquina) AS PARTES, SUM(HorasRealesRepOP1+HorasRealesRepOP2+HorasRealesRepOP3) AS HORAS" +
                                 " FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas]" +
                                 " Where FechaAveria is not null and FechaAveria <> '' and YEAR(convert(datetime, FechaAveria, 103)) = '" + año + "'" +
                                 " GROUP BY YEAR(convert(datetime, FechaAveria, 103)), IDMaquina) NUM" +
                                 " LEFT JOIN SMARTH_DB.DBO.AUX_Lista_Máquinas PER ON NUM.IDMaquina = PER.IdMaquina" +
                                 " LEFT JOIN (SELECT [Año],[Maquina],SUM([HorasDisponibles]) AS HorasDisponibles,SUM([HorasProduciendo]) AS HorasProduciendo FROM [SMARTH_DB].[dbo].[AUX_Horas_Disponibles_Maquina] GROUP BY Año, Maquina) MAQ ON NUM.AÑO = MAQ.Año AND IdMaquinaCHAR = MAQ.Maquina"+
                                 " WHERE HorasProduciendo IS NOT NULL AND NUM.AÑO = '" + año + "'" + WHERE_MES + "" + WHERE_NOMBRE + "" +
                                 " order by MTBF ASC";

                SqlDataAdapter adapter = new SqlDataAdapter(sql1, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql1);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }
        /*
        public DataSet Devuelve_Ranking_Maquinas_PartesAbiertosAÑO(int año)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql1 = " SELECT TOP(10) AÑO, PER.Maquina, PARTES, NUM.IdMaquina, PER.IdMaquinaCHAR" +
                                    " FROM(SELECT YEAR(convert(datetime, FechaAveria, 103)) as AÑO, MAX(MONTH(convert(datetime, FechaAveria, 103))) as MES, IDMaquina, COUNT(IDMaquina) AS PARTES" +
                                    " FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas]" +
                                    " Where FechaAveria is not null and FechaAveria <> '' and YEAR(convert(datetime, FechaAveria, 103)) = '" + año + "'" +
                                    " GROUP BY YEAR(convert(datetime, FechaAveria, 103)), IDMaquina) NUM" +
                                    " LEFT JOIN SMARTH_DB.DBO.AUX_Lista_Máquinas PER ON NUM.IDMaquina = PER.IdMaquina " +
                                    " WHERE AÑO = '" + año + "' order by PARTES DESC";
                SqlDataAdapter adapter = new SqlDataAdapter(sql1, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql1);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }
        */
        public DataSet Devuelve_Resultados_Mantenimiento_Moldes(int año)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql1 = "SELECT MAQ.AÑO, MAQ.MESTEXTO, ROUND(HOR.HorasProduciendo,0) AS HorasProduciendo, ESTIMADASMAQ, ROUND(REALESMAQ,2) AS REALESMAQ,  ROUND((REALESMAQ*100)/HOR.HorasProduciendo,2) as PORCCORRECTIVO, CASE WHEN KPI.[102_1] IS NULL THEN 0.0 ELSE KPI.[102_1] END AS KPICOR, ESTIMADASPREV, ROUND(REALESPREV,2) AS REALESPREV, ROUND((REALESPREV*100)/HOR.HorasProduciendo,2) as PORCPREVENTIVO, CASE WHEN KPI.[101_3] IS NULL THEN 0.0 ELSE KPI.[101_3] END AS KPIPREV, COSTESREPUESTOS, COSTESOPERARIOS, COSTESTOTALES, PARTESCORR, PARTESPREV, COSTESOPERARIOSCORR, COSTESREPUESTOSCORR, COSTESTOTALESCORR, COSTESOPERARIOSPREV, COSTESREPUESTOSPREV, COSTESTOTALESPREV" +
                                    " FROM(SELECT  YEAR(convert(datetime, FechaAveria, 103)) as AÑO, DATENAME(MONTH, (convert(datetime, FechaAveria, 103))) as MESTEXTO, MAX(MONTH(convert(datetime, FechaAveria, 103))) as MES, SUM([HorasEstimadasReparacion]) AS ESTIMADASMAQ, SUM([HorasRealesReparacion]) AS REALESMAQ, COUNT([HorasRealesReparacion]) AS PARTESCORR, SUM([ImporteEmpresa1]) AS COSTESREPUESTOSCORR, SUM([ImporteEmpresa2]) AS COSTESOPERARIOSCORR, SUM([ImporteEmpreza3]) AS COSTESTOTALESCORR  FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes]" +
                                         " where FechaAveria is not null and FechaAveria <> '' and YEAR(convert(datetime, FechaAveria, 103)) = '" + año + "' AND IdTipoRevision <= 1 GROUP BY YEAR(convert(datetime, FechaAveria, 103)), DATENAME(MONTH, (convert(datetime, FechaAveria, 103)))) MAQ" +
                                    " LEFT JOIN(SELECT  YEAR(convert(datetime, FechaAveria, 103)) as AÑO, DATENAME(MONTH, (convert(datetime, FechaAveria, 103))) as MESTEXTO, MAX(MONTH(convert(datetime, FechaAveria, 103))) as MES, SUM([HorasEstimadasReparacion]) AS ESTIMADASPREV, SUM([HorasRealesReparacion]) AS REALESPREV, COUNT([HorasRealesReparacion]) AS PARTESPREV, SUM([ImporteEmpresa1]) AS COSTESREPUESTOSPREV, SUM([ImporteEmpresa2]) AS COSTESOPERARIOSPREV, SUM([ImporteEmpreza3]) AS COSTESTOTALESPREV   FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes]" +
                                         " where FechaAveria is not null and FechaAveria <> '' and YEAR(convert(datetime, FechaAveria, 103)) = '" + año + "' AND IdTipoRevision > 1 GROUP BY YEAR(convert(datetime, FechaAveria, 103)), DATENAME(MONTH, (convert(datetime, FechaAveria, 103)))) PRE ON MAQ.AÑO = PRE.AÑO AND MAQ.MES = PRE.MES" +
                                    " LEFT JOIN(SELECT YEAR (convert(datetime, FechaAveria, 103)) as AÑO, DATENAME(MONTH, (convert(datetime, FechaAveria, 103))) as MESTEXTO, MAX(MONTH(convert(datetime, FechaAveria, 103))) as MES, SUM([ImporteEmpresa1]) AS COSTESREPUESTOS, SUM([ImporteEmpresa2]) AS COSTESOPERARIOS, SUM([ImporteEmpreza3]) AS COSTESTOTALES FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes]" +
                                         " where FechaAveria is not null and FechaAveria <> '' and YEAR(convert(datetime, FechaAveria, 103)) = '" + año + "' GROUP BY YEAR(convert(datetime, FechaAveria, 103)), DATENAME(MONTH, (convert(datetime, FechaAveria, 103)))) CST ON MAQ.AÑO = CST.AÑO AND MAQ.MES = CST.MES" +
                                    " LEFT JOIN [SMARTH_DB].[dbo].[AUX_Horas_Disponibles_Fabrica] HOR ON MAQ.AÑO = HOR.Año AND MAQ.MES = HOR.MES" +
                                    " LEFT JOIN[SMARTH_DB].[dbo].[KPI_Objetivos_Anuales] KPI ON MAQ.AÑO = KPI.AÑO" +
                                    " order by MAQ.MES";
                SqlDataAdapter adapter = new SqlDataAdapter(sql1, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql1);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataSet Devuelve_TOTAL_Resultados_Mantenimiento_Moldes(int año)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql1 = "SELECT REALESMAQ, REALESPREV, CAST(CAST(COSTESTOTALES AS INT) AS VARCHAR) + ' €' AS COSTETOTALES, PARTES" +
                                     " FROM(SELECT  YEAR(convert(datetime, FechaAveria, 103)) as AÑO, SUM([HorasEstimadasReparacion]) AS ESTIMADASMAQ, SUM([HorasRealesReparacion]) AS REALESMAQ, COUNT([HorasRealesReparacion]) AS PARTESCORR, SUM([ImporteEmpresa1]) AS COSTESREPUESTOSCORR, SUM([ImporteEmpresa2]) AS COSTESOPERARIOSCORR, SUM([ImporteEmpreza3]) AS COSTESTOTALESCORR  FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes]" +
                                          " where FechaAveria is not null and FechaAveria <> '' and YEAR(convert(datetime, FechaAveria, 103)) = '" + año + "' AND IdTipoRevision <= 1 GROUP BY YEAR(convert(datetime, FechaAveria, 103))) MAQ" +
                                     " LEFT JOIN(SELECT  YEAR(convert(datetime, FechaAveria, 103)) as AÑO, SUM([HorasEstimadasReparacion]) AS ESTIMADASPREV, SUM([HorasRealesReparacion]) AS REALESPREV, COUNT([HorasRealesReparacion]) AS PARTESPREV, SUM([ImporteEmpresa1]) AS COSTESREPUESTOSPREV, SUM([ImporteEmpresa2]) AS COSTESOPERARIOSPREV, SUM([ImporteEmpreza3]) AS COSTESTOTALESPREV   FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes]" +
                                          " where FechaAveria is not null and FechaAveria <> '' and YEAR(convert(datetime, FechaAveria, 103)) = '" + año + "' AND IdTipoRevision > 1 GROUP BY YEAR(convert(datetime, FechaAveria, 103))) PRE ON MAQ.AÑO = PRE.AÑO" +
                                     " LEFT JOIN(SELECT YEAR (convert(datetime, FechaAveria, 103)) as AÑO, SUM([ImporteEmpresa1]) AS COSTESREPUESTOS, SUM([ImporteEmpresa2]) AS COSTESOPERARIOS, SUM([ImporteEmpreza3]) AS COSTESTOTALES, COUNT([ImporteEmpresa1]) AS PARTES FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes]" +
                                          " where FechaAveria is not null and FechaAveria <> '' and YEAR(convert(datetime, FechaAveria, 103)) = '" + año + "' GROUP BY YEAR(convert(datetime, FechaAveria, 103))) CST ON MAQ.AÑO = CST.AÑO";
                SqlDataAdapter adapter = new SqlDataAdapter(sql1, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql1);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataSet Devuelve_Resultados_Detallados_TipoMantenimiento_MOLDES(int año, string where)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql1 = "SELECT MAQ.AÑO, MAQ.MESTEXTO, CASE WHEN AUX.TipoMantenimientoMolde = '' THEN 'Sin definir' else  AUX.TipoMantenimientoMolde END AS TIPOMANT, REALESMAQ as HorasREP, PARTESCORR as NUMPARTES,COSTESOPERARIOSCORR, COSTESREPUESTOSCORR, COSTESTOTALESCORR"+
                                     " FROM(SELECT  YEAR(convert(datetime, FechaAveria, 103)) as AÑO, DATENAME(MONTH, (convert(datetime, FechaAveria, 103))) as MESTEXTO, MAX(MONTH(convert(datetime, FechaAveria, 103))) as MES, IdTipoRevision, SUM([HorasEstimadasReparacion]) AS ESTIMADASMAQ, SUM([HorasRealesReparacion]) AS REALESMAQ, COUNT([HorasRealesReparacion]) AS PARTESCORR, SUM([ImporteEmpresa1]) AS COSTESREPUESTOSCORR, SUM([ImporteEmpresa2]) AS COSTESOPERARIOSCORR, SUM([ImporteEmpreza3]) AS COSTESTOTALESCORR  FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes]" +
                                          " where FechaAveria is not null and FechaAveria <> '' and YEAR(convert(datetime, FechaAveria, 103)) = '"+año+"'"+where+" GROUP BY YEAR(convert(datetime, FechaAveria, 103)), DATENAME(MONTH, (convert(datetime, FechaAveria, 103))), IdTipoRevision) MAQ" +
                                      " LEFT JOIN SMARTH_DB.DBO.MANTENIMIENTO_Aux_Opciones AUX ON MAQ.IdTipoRevision = AUX.Id" +
                                      " order by MAQ.MES";
                SqlDataAdapter adapter = new SqlDataAdapter(sql1, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql1);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataSet Devuelve_Ranking_AperturaPartes_Moldes(int año, string WHERE_MES, string WHERE_NOMBRE)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql1 = "SELECT AÑO, MESTEXTO, Nombre, PARTES" +
                                " FROM(SELECT YEAR(convert(datetime, FechaAveria, 103)) as AÑO, DATENAME(MONTH, (convert(datetime, FechaAveria, 103))) as MESTEXTO, MAX(MONTH(convert(datetime, FechaAveria, 103))) as MES, IdEncargado, COUNT(IdEncargado) AS PARTES" +
                                " FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes]" +
                                " Where FechaAveria is not null and FechaAveria <> '' and YEAR(convert(datetime, FechaAveria, 103)) = '" + año + "'" +
                                " GROUP BY YEAR(convert(datetime, FechaAveria, 103)), DATENAME(MONTH, (convert(datetime, FechaAveria, 103))), IdEncargado) NUM" +
                                " LEFT JOIN SMARTH_DB.DBO.AUX_Personal_Mandos PER ON NUM.IdEncargado = PER.Id" +
                                " WHERE AÑO = '" + año + "'" + WHERE_MES + "" + WHERE_NOMBRE + "" +
                                " order by MES ASC, PARTES DESC";
                SqlDataAdapter adapter = new SqlDataAdapter(sql1, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql1);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataSet Devuelve_Ranking_AperturaPartes_MOLAÑO(int año, string WHERE_MES, string WHERE_NOMBRE)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql1 = "SELECT AÑO, Nombre, PARTES" +
                                " FROM(SELECT YEAR(convert(datetime, FechaAveria, 103)) as AÑO, IdEncargado, COUNT(IdEncargado) AS PARTES" +
                                " FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes]" +
                                " Where FechaAveria is not null and FechaAveria <> '' and YEAR(convert(datetime, FechaAveria, 103)) = '" + año + "'" +
                                " GROUP BY YEAR(convert(datetime, FechaAveria, 103)), IdEncargado) NUM" +
                                " LEFT JOIN SMARTH_DB.DBO.AUX_Personal_Mandos PER ON NUM.IdEncargado = PER.Id" +
                                " WHERE AÑO = '" + año + "'" + WHERE_MES + "" + WHERE_NOMBRE + "" +
                                " order by PARTES DESC";
                SqlDataAdapter adapter = new SqlDataAdapter(sql1, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql1);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataSet Devuelve_Ranking_Moldes_PartesAbiertosMTBF(int año, string WHERE_MES, string WHERE_NOMBRE)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql1 = "SELECT TOP(10) NUM.AÑO, MESTEXTO, CAST(MOLDES AS VARCHAR) + ' ' + PER.Descripcion AS Molde, MOLDES AS MOL, PARTES, MOL.HorasProduciendo, CASE WHEN HorasProduciendo IS NULL THEN 0.0 ELSE ROUND(MOL.HorasProduciendo/PARTES,2) END as MTBF, CASE WHEN HorasProduciendo IS NULL THEN 0.0 ELSE ROUND(1/(MOL.HorasProduciendo/PARTES),2) END as MTBFINVERSO " +
                                " FROM(SELECT YEAR(convert(datetime, FechaAveria, 103)) as AÑO, DATENAME(MONTH, (convert(datetime, FechaAveria, 103))) as MESTEXTO, MAX(MONTH(convert(datetime, FechaAveria, 103))) as MES, IDMoldes AS MOLDES, COUNT(IDMoldes) AS PARTES" +
                                " FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes]" +
                                " Where FechaAveria is not null and FechaAveria <> '' and YEAR(convert(datetime, FechaAveria, 103)) = '" + año + "'" +
                                " GROUP BY YEAR(convert(datetime, FechaAveria, 103)), DATENAME(MONTH, (convert(datetime, FechaAveria, 103))), IDMoldes) NUM" +
                                " LEFT JOIN SMARTH_DB.DBO.AUX_Lista_Moldes PER ON NUM.MOLDES = PER.ReferenciaMolde" +
                                " LEFT JOIN[SMARTH_DB].[dbo].[AUX_Horas_Disponibles_Molde] MOL ON PER.ReferenciaMolde = MOL.Molde AND NUM.AÑO = MOL.Año AND NUM.MES = MOL.Mes" +
                                " WHERE MOL.HorasProduciendo IS NOT NULL AND MOLDES <> 0 AND NUM.AÑO = '" + año + "'" + WHERE_MES + "" + WHERE_NOMBRE + "" +
                                " order by NUM.MES ASC, MTBF ASC";
                SqlDataAdapter adapter = new SqlDataAdapter(sql1, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql1);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataSet Devuelve_Ranking_Moldes_PartesAbiertos(int año, string WHERE_MES, string WHERE_NOMBRE)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql1 = "SELECT TOP(10) AÑO, MESTEXTO, CAST(MOLDES AS VARCHAR) + ' ' + PER.Descripcion AS Molde, MOLDES AS MOL, PARTES, HORAS, ROUND(HORAS/PARTES,2) AS MTTR" +
                                " FROM(SELECT YEAR(convert(datetime, FechaAveria, 103)) as AÑO, DATENAME(MONTH, (convert(datetime, FechaAveria, 103))) as MESTEXTO, MAX(MONTH(convert(datetime, FechaAveria, 103))) as MES, IDMoldes AS MOLDES, COUNT(IDMoldes) AS PARTES, SUM(HorasRealesRepOP1+HorasRealesRepOP2+HorasRealesRepOP3) AS HORAS" +
                                " FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes]" +
                                " Where FechaAveria is not null and FechaAveria <> '' and YEAR(convert(datetime, FechaAveria, 103)) = '" + año + "'" +
                                " GROUP BY YEAR(convert(datetime, FechaAveria, 103)), DATENAME(MONTH, (convert(datetime, FechaAveria, 103))), IDMoldes) NUM" +
                                " LEFT JOIN SMARTH_DB.DBO.AUX_Lista_Moldes PER ON NUM.MOLDES = PER.ReferenciaMolde" +

                                " WHERE AÑO = '" + año + "'" + WHERE_MES + "" + WHERE_NOMBRE + "" +
                                " order by MES ASC, MTTR DESC";
                SqlDataAdapter adapter = new SqlDataAdapter(sql1, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql1);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataSet Devuelve_Ranking_Moldes_PartesAbiertosAÑO(int año, string WHERE_MES, string WHERE_NOMBRE)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql1 = "SELECT TOP(10) AÑO, CAST(MOLDES AS VARCHAR) + ' ' + PER.Descripcion AS Molde,CAST(MOLDES AS VARCHAR) AS MOL, PARTES, HORAS, ROUND(HORAS/PARTES,2) AS MTTR" +
                                " FROM(SELECT YEAR(convert(datetime, FechaAveria, 103)) as AÑO, MAX(MONTH(convert(datetime, FechaAveria, 103))) as MES, IDMoldes AS MOLDES, COUNT(IDMoldes) AS PARTES, SUM(HorasRealesRepOP1+HorasRealesRepOP2+HorasRealesRepOP3) AS HORAS" +
                                " FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes]" +
                                " Where FechaAveria is not null and FechaAveria <> '' and YEAR(convert(datetime, FechaAveria, 103)) = '" + año + "'" +
                                " GROUP BY YEAR(convert(datetime, FechaAveria, 103)), IDMoldes) NUM" +
                                " LEFT JOIN SMARTH_DB.DBO.AUX_Lista_Moldes PER ON NUM.MOLDES = PER.ReferenciaMolde" +
                                " WHERE AÑO = '" + año + "'" + WHERE_MES + "" + WHERE_NOMBRE + "" +
                                " order by MTTR DESC";
                SqlDataAdapter adapter = new SqlDataAdapter(sql1, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql1);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataSet Devuelve_Ranking_Moldes_PartesAbiertosAÑOMTBF(int año, string WHERE_MES, string WHERE_NOMBRE)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql1 = "SELECT TOP(10) NUM.AÑO, CAST(MOLDES AS VARCHAR) + ' ' + PER.Descripcion AS Molde,CAST(MOLDES AS VARCHAR) AS MOL, PARTES, MOL.HorasProduciendo, CASE WHEN HorasProduciendo IS NULL THEN 0.0 ELSE ROUND(MOL.HorasProduciendo/PARTES,2) END as MTBF, CASE WHEN HorasProduciendo IS NULL THEN 0.0 ELSE ROUND(1/(MOL.HorasProduciendo/PARTES),2) END as MTBFINVERSO " +
                                " FROM(SELECT YEAR(convert(datetime, FechaAveria, 103)) as AÑO, MAX(MONTH(convert(datetime, FechaAveria, 103))) as MES, IDMoldes AS MOLDES, COUNT(IDMoldes) AS PARTES, SUM(HorasRealesRepOP1+HorasRealesRepOP2+HorasRealesRepOP3) AS HORAS" +
                                " FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes]" +
                                " Where FechaAveria is not null and FechaAveria <> '' and YEAR(convert(datetime, FechaAveria, 103)) = '" + año + "'" +
                                " GROUP BY YEAR(convert(datetime, FechaAveria, 103)), IDMoldes) NUM" +
                                " LEFT JOIN SMARTH_DB.DBO.AUX_Lista_Moldes PER ON NUM.MOLDES = PER.ReferenciaMolde" +
                                " LEFT JOIN (SELECT  [Año],[Molde],SUM([HorasProduciendo]) AS [HorasProduciendo] FROM [SMARTH_DB].[dbo].[AUX_Horas_Disponibles_Molde] WHERE MOLDE <> '' AND HorasProduciendo > 0 GROUP BY [Año],[Molde])  MOL ON PER.ReferenciaMolde = MOL.Molde AND NUM.AÑO = MOL.Año"+
                                " WHERE MOLDE <> 0 AND HorasProduciendo is not null and HorasProduciendo > 50 AND NUM.AÑO = '" + año + "'" + WHERE_MES + "" + WHERE_NOMBRE + "" +
                                " order by MTBF ASC";
                SqlDataAdapter adapter = new SqlDataAdapter(sql1, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql1);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        ///////////////////////////////////////////////////////PRODUCCIÓN\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

        //TAREAS DE CAMBIADOR
        public bool Actualizar_Molde_Al_Taller(string parte)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "UPDATE [SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes] SET[IdUbicacion] = 2 WHERE IdReparacionMolde = " + parte + "";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }

        public DataSet Moldes_Pendientes_Taller()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql1 = "SELECT IdReparacionMolde as PARTE, R.IDMoldes AS MOLDE, M.Descripcion AS DESCRIPCION, MotivoAveria AS REPARACION, FechaAveria AS FECHA, " +
                                 " CASE WHEN R.IdUbicacion = 1 THEN 'En planta' WHEN R.IdUbicacion = 2 THEN 'En taller' WHEN R.IdUbicacion = 3 THEN 'En prov. externo' END AS DONDE, Nombre AS ENCARGADO, M.Ubicacion AS UBICACION," +
                                 " CASE WHEN PN.SEQNR = 0 THEN 'EN MÁQUINA' WHEN PN.SEQNR > 0 THEN 'PLANIFICADO' WHEN PN.SEQNR IS NULL THEN 'LIBRE' END AS ESTADO" +
                                 " FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes] R" +
                                 " left join[SMARTH_DB].[dbo].[AUX_Personal_Mandos] P on r.IdEncargado = p.Id" +
                                 " left join[SMARTH_DB].[dbo].[AUX_Lista_Moldes] M on R.IDMoldes = M.ReferenciaMolde" +
                                 " left join(SELECT Molde, MIN(SEQNR) AS SEQNR FROM [SMARTH_DB].[dbo].[AUX_Planificacion] GROUP BY [Molde]) PN ON R.IDMoldes = PN.Molde" +
                                 " WHERE Terminado = 0 and R.IdUbicacion = 1" +
                                 " ORDER BY IdReparacionMolde DESC";
                SqlDataAdapter adapter = new SqlDataAdapter(sql1, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql1);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
               
                return null;
            }
        }

        //******************************************************\\RR.HH//************************************************\\
        //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\//\\

        ///////////\\\\\\\\\\\\\\\KPI RR.HH/////////////////\\\\\\\\\\\\\\\
        public DataTable Devuelve_dias_sin_accidente()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = " SELECT TOP(1000)[Id],[UltimoAccidente],[Imagen],ABS(DATEDIFF(day, SYSDATETIME(), [UltimoAccidente])) AS DIASINACC" +
                             " FROM [SMARTH_DB].[dbo].[RRHH_KPI_Dashboard]";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public void Actualizar_Fecha_Accidente(string fecha)

        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "UPDATE  [SMARTH_DB].[dbo].[RRHH_KPI_Dashboard] SET[UltimoAccidente] = '"+fecha+"'";
                
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
            }
        }




        ///////////\\\\\\\\\\\\\\\ENTREGA EPIS/////////////////\\\\\\\\\\\\

        public DataTable Devuelve_listado_Recursos_Entregados(string WHERE)
        {
            try
            {
                cnn_NAV.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_NAV;
                string sql = "SELECT ART.[timestamp], CAST([Employee No_] AS INT) AS [Employee No_], OP.[Search Name],[Misc_ Article Code],[Line No_],[Description],[From Date],[To Date],[In Use],[Serial No_]" +
                              " FROM[NAVDB].[dbo].[THERMO$Misc_ Article Information] ART"+
                              " LEFT JOIN [NAVDB].[dbo].[THERMO$Employee] OP ON ART. [Employee No_] = OP.No_"+
                              " WHERE [Employee No_] IS NOT NULL " + WHERE+""+
                              " order by [Employee No_] asc, [Line No_]";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_NAV);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_NAV.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_NAV.Close();
                return null;
            }
        }

        public DataTable Devuelve_EPIS_ListaEntrega(string WHERE)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT LIS.[Id],[IdNAV],CAST(LIS.[Operario] AS INT) AS[Operario],OP.[Operario] AS NOMBRE,[Descripcion],[NumSerie],[Talla],CAST([Cantidad] AS varchar) as [Cantidad],CAST([FechaEntrega] AS varchar) as [FechaEntrega],[FechaEntrega] as FechaEntregaSHOW, ENT.Nombre AS[EntregadoPOR],[Firma],[Documento],[Eliminar]" +
                                  " FROM[SMARTH_DB].[dbo].[RRHH_Entrega_Articulos_Diversos] LIS" +
                                  " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] OP ON LIS.Operario = OP.Id" +
                                  " LEFT JOIN [SMARTH_DB].[dbo].[AUX_Personal_Mandos] ENT ON LIS.EntregadoPOR = ENT.Id" +
                                  " where lis.[Id] is not null " + WHERE + "";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;

            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }


        /*
        public DataTable Devuelve_listado_Articulos()
        {
            try
            {
                cnn_NAV.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_NAV;
                string sql = "SELECT *  FROM [NAVDB].[dbo].[THERMO$Misc_ Article] order by code";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_NAV);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_NAV.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_NAV.Close();
                return null;
            }
        }
        public DataTable Devuelve_lista_EPIS()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT * FROM[SMARTH_DB].[dbo].[RRHH_EPIS] order by material asc";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;

            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }
        */
        public bool Insertar_EPI_Entregado(int idNAV, int Operario, string Codigo, string Descripcion, string NumSerie, string Talla, int cantidad, string fechaentrega, int entregadoPOR, string firma)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[RRHH_Entrega_Articulos_Diversos] ([IdNAV],[Operario],[Codigo],[Descripcion],[NumSerie],[Talla],[Cantidad],[FechaEntrega],[EntregadoPOR],[Firma],[Documento],[Eliminar] )" +
                               " VALUES ("+idNAV+"," + Operario + ",'" + Codigo + "', '" + Descripcion + "', '" + NumSerie + "', '"+Talla+"',"+cantidad+", '"+fechaentrega+"', "+ entregadoPOR + ", '"+firma+"','', 0)";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }


        //**********************************************\\MURO DE CALIDAD//**********************************************\\
        //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\//\\
        ///////////\\\\\\\\\\\\\\\CONSULTAS REGISTRO COMUNICACIONES/////////////////\\\\\\\\\\\\\\\
        ///

        public DataTable Devuelve_Lista_Estados_Referencias()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                
                string sql = "SELECT * FROM[SMARTH_DB].[dbo].[AUX_GP12_RazonRevision] where Razon <> ''";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public int Devuelve_ID_Estados_Referencias(string estado)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;

                string sql = "SELECT Id FROM [SMARTH_DB].[dbo].[AUX_GP12_RazonRevision] WHERE [Razon] = '" + estado + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return Convert.ToInt16(ds.Tables[0].Rows[0]["Id"].ToString());
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return 0;
            }
        }


        public DataTable Devuelve_Estado_Referencias(string referencia, string molde, string estadoREV, string cliente, string responsable, string vencido)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                
                string sql = "SELECT P.[Referencia] as REF,[Molde],[Descripcion], CASE WHEN LogotipoSM IS NULL THEN 'http://facts4-srv/thermogestion/SMARTH_docs/MANTENIMIENTO/sin_imagen.jpg' ELSE LogotipoSM END AS LogotipoSM, R.Razon as EstadoActual,D.Nombre as Responsable,[Fecharev],[Fechaprevsalida],[EstadoAnterior],[Fechaestanterior],[Observaciones],E.EstadoActual as IDEstactual " +
                    " FROM [SMARTH_DB].[dbo].[AUX_TablaProductos] P LEFT JOIN [SMARTH_DB].[dbo].[GP12_ProductosEstados] E ON P.Referencia = E.Referencia" +
                    " LEFT JOIN [SMARTH_DB].[dbo].[AUX_GP12_RazonRevision] R ON E.EstadoActual = R.Id " +
                    " LEFT JOIN SMARTH_DB.DBO.AUX_Personal_Mandos D ON E.Responsable = D.Id " +
                    " LEFT JOIN [SMARTH_DB].[dbo].[AUX_Lista_Clientes] CLI ON P.Cliente = CLI.Cliente" +
                    " where P.[Referencia] is not null " + referencia+ "" + molde + "" + estadoREV + "" + cliente + "" + responsable + "" + vencido + "" +
                    " ORDER BY P.Referencia";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_ultimas_revisiones_periodo(int año, string filtroaño, string referencia, string molde, string lote, string cliente, string responsable, string fechainicio, string fechafin, string firmado, string estado)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "";
                if (año < 2021)
                {
                    sql = "SELECT TOP(500) * FROM(" +
                          " SELECT [IDHistorico] AS ID, [FechaInicio],HorasInspeccion AS DoubleHoras, CAST([HorasInspeccion] AS varchar) + ' h' AS HORAS,P.Proveedor,[OperarioRevisionINT],[OperarioRevision],C.Referencia,C.[Nombre],J.[Nombre] as NombreRESP,C.Molde,[Nlote],[Ncaja],[PiezasRevisadas]," +
                          " [PiezasOK],[Retrabajadas],[PiezasNOK],[Def1],[Def2],[Def3],[Def4],[Def5],[Def6],[Def7],[Def8],[Def9],[Def10],[Def11],[Def12],[Def13],[Def14],[Def15],[Def16],[Def17]," +
                          " [Def18],[Def19],[Def20],[Def21],[Def22],[Def23],[Def24],[Def25],[Def26],[Def27],[Def28],[Def29],[Def30],[Incidencias],[ImagenDefecto1],[ImagenDefecto2]," +
                          " [ImagenDefecto3],[Notas],C.[NOp1],C.[NOp2],C.[NOp3],C.[NOp4],R.Razon,CAST([CosteRevision] AS varchar)+' €' AS CosteRevision,CAST([CostePiezaRevision] AS varchar)+' €' AS CostePiezaRevision,CAST([CosteScrapRevision] AS varchar)+' €' AS CosteScrapRevision, M.Cliente, CASE WHEN LogotipoSM IS NULL THEN 'http://facts4-srv/thermogestion/SMARTH_docs/MANTENIMIENTO/sin_imagen.jpg' ELSE LogotipoSM END AS Logotipo," +
                          " CASE WHEN NOp1 = 0 THEN ' ' WHEN NOp1 > 0 AND CAST(FechaInfo1 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp1 > 0 AND CAST(FechaInfo1 AS varchar) <> '' THEN CAST(FechaInfo1 AS varchar) END AS INFO1," +
                          " CASE WHEN NOp2 = 0 THEN ' ' WHEN NOp2 > 0 AND CAST(FechaInfo2 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp2 > 0 AND CAST(FechaInfo2 AS varchar) <> '' THEN CAST(FechaInfo2 AS varchar) END AS INFO2," +
                          " CASE WHEN NOp3 = 0 THEN ' ' WHEN NOp3 > 0 AND CAST(FechaInfo3 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp3 > 0 AND CAST(FechaInfo3 AS varchar) <> '' THEN CAST(FechaInfo3 AS varchar) END AS INFO3," +
                          " CASE WHEN NOp4 = 0 THEN ' ' WHEN NOp4 > 0 AND CAST(FechaInfo4 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp4 > 0 AND CAST(FechaInfo4 AS varchar) <> '' THEN CAST(FechaInfo4 AS varchar) END AS INFO4," +
                          " [FechaInfo1],[Informador1],[FechaInfo2],[Informador2],[FechaInfo3],[Informador3],[FechaInfo4],[Informador4],[FeedbackOPERARIOS], M.[FakeMode]," +
                          " CASE WHEN O.Operario IS NULL THEN '' WHEN O.Operario IS NOT NULL THEN O.Operario END AS NOMOP1, CASE WHEN O2.Operario IS NULL THEN '' WHEN O2.Operario IS NOT NULL THEN O2.Operario END AS NOMOP2," +
                          " CASE WHEN O3.Operario IS NULL THEN '' WHEN O3.Operario IS NOT NULL THEN O3.Operario END AS NOMOP3, CASE WHEN O4.Operario IS NULL THEN '' WHEN O4.Operario IS NOT NULL THEN O4.Operario END AS NOMOP4,ScrapAlmacen" +
                          " FROM [SMARTH_DB].[dbo].[GP12_Historico] C LEFT JOIN [SMARTH_DB].[dbo].[GP12_ProveedoresRevision] P ON C.ProveedorRevision = P.Id LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] M ON C.Referencia = M.Referencia" +
                          " LEFT JOIN [SMARTH_DB].[dbo].[GP12_ProductosEstados] EST ON C.Referencia = EST.Referencia"+
                          " LEFT JOIN SMARTH_DB.DBO.AUX_Personal_Mandos J ON EST.Responsable = J.Id"+
                          " LEFT JOIN [SMARTH_DB].[dbo].[AUX_Lista_Clientes] CLI ON M.Cliente = CLI.Cliente" +
                          " LEFT JOIN [SMARTH_DB].[dbo].[AUX_GP12_RazonRevision] R ON C.RazonRevision = R.Id" +
                          " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] O on C.NOp1 = O.Id LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] O2 on C.NOp2 = O2.Id" +
                          " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] O3 on C.NOp3 = O3.Id LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] O4 on C.NOp4 = O4.Id) F" +
                          " WHERE PiezasRevisadas > 0 " + filtroaño + "" + referencia + "" + molde + "" + lote + "" + cliente + "" + responsable + "" + fechainicio + "" + fechafin + "" + firmado + "" + estado + "" +
                          " ORDER BY FechaInicio DESC";
                }
                else
                {
                    sql = "SELECT TOP(500) * FROM(SELECT C.ID, [FechaInicio],HorasInspeccion AS DoubleHoras, CAST([HorasInspeccion] AS varchar) + ' h' AS HORAS,P.Proveedor,[OperarioRevisionINT],[OperarioRevision],C.Referencia,C.[Nombre],J.[Nombre] as NombreRESP,C.Molde,[Nlote],[Ncaja],[PiezasRevisadas]," + " [PiezasOK],[Retrabajadas],[PiezasNOK],[Def1],[Def2],[Def3],[Def4],[Def5],[Def6],[Def7],[Def8],[Def9],[Def10],[Def11],[Def12],[Def13],[Def14],[Def15],[Def16],[Def17]," + " [Def18],[Def19],[Def20],[Def21],[Def22],[Def23],[Def24],[Def25],[Def26],[Def27],[Def28],[Def29],[Def30],[Incidencias],[ImagenDefecto1],[ImagenDefecto2]," + " [ImagenDefecto3],[Notas],C.[NOp1],C.[NOp2],C.[NOp3],C.[NOp4],R.Razon,CAST([CosteRevision] AS varchar)+' €' AS CosteRevision,CAST([CostePiezaRevision] AS varchar)+' €' AS CostePiezaRevision,CAST([CosteScrapRevision] AS varchar)+' €' AS CosteScrapRevision, M.Cliente,CASE WHEN LogotipoSM IS NULL THEN 'http://facts4-srv/thermogestion/SMARTH_docs/MANTENIMIENTO/sin_imagen.jpg' ELSE LogotipoSM END AS Logotipo," + " CASE WHEN NOp1 = 0 THEN ' ' WHEN NOp1 > 0 AND CAST(FechaInfo1 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp1 > 0 AND CAST(FechaInfo1 AS varchar) <> '' THEN CAST(FechaInfo1 AS varchar) END AS INFO1," + " CASE WHEN NOp2 = 0 THEN ' ' WHEN NOp2 > 0 AND CAST(FechaInfo2 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp2 > 0 AND CAST(FechaInfo2 AS varchar) <> '' THEN CAST(FechaInfo2 AS varchar) END AS INFO2," + " CASE WHEN NOp3 = 0 THEN ' ' WHEN NOp3 > 0 AND CAST(FechaInfo3 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp3 > 0 AND CAST(FechaInfo3 AS varchar) <> '' THEN CAST(FechaInfo3 AS varchar) END AS INFO3," + " CASE WHEN NOp4 = 0 THEN ' ' WHEN NOp4 > 0 AND CAST(FechaInfo4 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp4 > 0 AND CAST(FechaInfo4 AS varchar) <> '' THEN CAST(FechaInfo4 AS varchar) END AS INFO4," + " [FechaInfo1],[Informador1],[FechaInfo2],[Informador2],[FechaInfo3],[Informador3],[FechaInfo4],[Informador4],[FeedbackOPERARIOS], M.[FakeMode]," + " CASE WHEN O.Operario IS NULL THEN '' WHEN O.Operario IS NOT NULL THEN O.Operario END AS NOMOP1, CASE WHEN O2.Operario IS NULL THEN '' WHEN O2.Operario IS NOT NULL THEN O2.Operario END AS NOMOP2," + " CASE WHEN O3.Operario IS NULL THEN '' WHEN O3.Operario IS NOT NULL THEN O3.Operario END AS NOMOP3, CASE WHEN O4.Operario IS NULL THEN '' WHEN O4.Operario IS NOT NULL THEN O4.Operario END AS NOMOP4,ScrapAlmacen" + " " +
                        " FROM [SMARTH_DB].[dbo].[GP12_Historico_2021] C" +                       
                            " LEFT JOIN[SMARTH_DB].[dbo].[GP12_ProductosEstados] EST ON C.Referencia = EST.Referencia"+
                            " LEFT JOIN SMARTH_DB.DBO.AUX_Personal_Mandos J ON EST.Responsable = J.Id" +
                            " LEFT JOIN [SMARTH_DB].[dbo].[GP12_ProveedoresRevision] P ON C.ProveedorRevision = P.Id" +
                            " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] M ON C.Referencia = M.Referencia" +
                            " LEFT JOIN [SMARTH_DB].[dbo].[AUX_Lista_Clientes] CLI ON M.Cliente = CLI.Cliente" +
                            " LEFT JOIN [SMARTH_DB].[dbo].[AUX_GP12_RazonRevision] R ON C.RazonRevision = R.Id" +
                            " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] O on C.NOp1 = O.Id LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] O2 on C.NOp2 = O2.Id" +
                            " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] O3 on C.NOp3 = O3.Id LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] O4 on C.NOp4 = O4.Id) F" +
                            " WHERE PiezasRevisadas > 0 " + filtroaño + "" + referencia + "" + molde + "" + lote + "" + cliente + "" + responsable + "" + fechainicio + "" + fechafin + "" + firmado + "" + estado + "" +
                            " ORDER BY FechaInicio DESC";
                }

                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        //Gestion Scrap contra NAV
        public DataTable Devuelve_ultimas_revisiones_periodoID(int año, int ID)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "";
                if (año < 2021)
                {
                    sql = "SELECT * FROM(" +
                          " SELECT [IDHistorico] AS ID, [FechaInicio],HorasInspeccion AS DoubleHoras, CAST([HorasInspeccion] AS varchar) + ' h' AS HORAS,P.Proveedor,[OperarioRevisionINT],[OperarioRevision],C.Referencia,C.[Nombre],J.[Nombre] as NombreRESP,C.Molde,[Nlote],[Ncaja],[PiezasRevisadas]," +
                          " [PiezasOK],[Retrabajadas],[PiezasNOK],[Def1],[Def2],[Def3],[Def4],[Def5],[Def6],[Def7],[Def8],[Def9],[Def10],[Def11],[Def12],[Def13],[Def14],[Def15],[Def16],[Def17]," +
                          " [Def18],[Def19],[Def20],[Def21],[Def22],[Def23],[Def24],[Def25],[Def26],[Def27],[Def28],[Def29],[Def30],[Incidencias],[ImagenDefecto1],[ImagenDefecto2]," +
                          " [ImagenDefecto3],[Notas],C.[NOp1],C.[NOp2],C.[NOp3],C.[NOp4],R.Razon,CAST([CosteRevision] AS varchar)+' €' AS CosteRevision,CAST([CostePiezaRevision] AS varchar)+' €' AS CostePiezaRevision,CAST([CosteScrapRevision] AS varchar)+' €' AS CosteScrapRevision, M.Cliente, CASE WHEN LogotipoSM IS NULL THEN 'http://facts4-srv/thermogestion/SMARTH_docs/MANTENIMIENTO/sin_imagen.jpg' ELSE LogotipoSM END AS Logotipo," +
                          " CASE WHEN NOp1 = 0 THEN ' ' WHEN NOp1 > 0 AND CAST(FechaInfo1 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp1 > 0 AND CAST(FechaInfo1 AS varchar) <> '' THEN CAST(FechaInfo1 AS varchar) END AS INFO1," +
                          " CASE WHEN NOp2 = 0 THEN ' ' WHEN NOp2 > 0 AND CAST(FechaInfo2 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp2 > 0 AND CAST(FechaInfo2 AS varchar) <> '' THEN CAST(FechaInfo2 AS varchar) END AS INFO2," +
                          " CASE WHEN NOp3 = 0 THEN ' ' WHEN NOp3 > 0 AND CAST(FechaInfo3 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp3 > 0 AND CAST(FechaInfo3 AS varchar) <> '' THEN CAST(FechaInfo3 AS varchar) END AS INFO3," +
                          " CASE WHEN NOp4 = 0 THEN ' ' WHEN NOp4 > 0 AND CAST(FechaInfo4 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp4 > 0 AND CAST(FechaInfo4 AS varchar) <> '' THEN CAST(FechaInfo4 AS varchar) END AS INFO4," +
                          " [FechaInfo1],[Informador1],[FechaInfo2],[Informador2],[FechaInfo3],[Informador3],[FechaInfo4],[Informador4],[FeedbackOPERARIOS], M.[FakeMode]," +
                          " CASE WHEN O.Operario IS NULL THEN '' WHEN O.Operario IS NOT NULL THEN O.Operario END AS NOMOP1, CASE WHEN O2.Operario IS NULL THEN '' WHEN O2.Operario IS NOT NULL THEN O2.Operario END AS NOMOP2," +
                          " CASE WHEN O3.Operario IS NULL THEN '' WHEN O3.Operario IS NOT NULL THEN O3.Operario END AS NOMOP3, CASE WHEN O4.Operario IS NULL THEN '' WHEN O4.Operario IS NOT NULL THEN O4.Operario END AS NOMOP4,ScrapAlmacen" +
                          " FROM [SMARTH_DB].[dbo].[GP12_Historico] C LEFT JOIN [SMARTH_DB].[dbo].[GP12_ProveedoresRevision] P ON C.ProveedorRevision = P.Id LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] M ON C.Referencia = M.Referencia" +
                          " LEFT JOIN [SMARTH_DB].[dbo].[GP12_ProductosEstados] EST ON C.Referencia = EST.Referencia" +
                          " LEFT JOIN SMARTH_DB.DBO.AUX_Personal_Mandos J ON EST.Responsable = J.Id" +
                          " LEFT JOIN [SMARTH_DB].[dbo].[AUX_Lista_Clientes] CLI ON M.Cliente = CLI.Cliente" +
                          " LEFT JOIN [SMARTH_DB].[dbo].[AUX_GP12_RazonRevision] R ON C.RazonRevision = R.Id" +
                          " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] O on C.NOp1 = O.Id LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] O2 on C.NOp2 = O2.Id" +
                          " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] O3 on C.NOp3 = O3.Id LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] O4 on C.NOp4 = O4.Id) F" +
                          " WHERE ID = "+ ID + "" +
                          " ORDER BY FechaInicio DESC";
                }
                else
                {
                    sql = "SELECT * FROM(SELECT C.ID, [FechaInicio],HorasInspeccion AS DoubleHoras, CAST([HorasInspeccion] AS varchar) + ' h' AS HORAS,P.Proveedor,[OperarioRevisionINT],[OperarioRevision],C.Referencia,C.[Nombre],J.[Nombre] as NombreRESP,C.Molde,[Nlote],[Ncaja],[PiezasRevisadas]," + " [PiezasOK],[Retrabajadas],[PiezasNOK],[Def1],[Def2],[Def3],[Def4],[Def5],[Def6],[Def7],[Def8],[Def9],[Def10],[Def11],[Def12],[Def13],[Def14],[Def15],[Def16],[Def17]," + " [Def18],[Def19],[Def20],[Def21],[Def22],[Def23],[Def24],[Def25],[Def26],[Def27],[Def28],[Def29],[Def30],[Incidencias],[ImagenDefecto1],[ImagenDefecto2]," + " [ImagenDefecto3],[Notas],C.[NOp1],C.[NOp2],C.[NOp3],C.[NOp4],R.Razon,CAST([CosteRevision] AS varchar)+' €' AS CosteRevision,CAST([CostePiezaRevision] AS varchar)+' €' AS CostePiezaRevision,CAST([CosteScrapRevision] AS varchar)+' €' AS CosteScrapRevision, M.Cliente,CASE WHEN LogotipoSM IS NULL THEN 'http://facts4-srv/thermogestion/SMARTH_docs/MANTENIMIENTO/sin_imagen.jpg' ELSE LogotipoSM END AS Logotipo," + " CASE WHEN NOp1 = 0 THEN ' ' WHEN NOp1 > 0 AND CAST(FechaInfo1 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp1 > 0 AND CAST(FechaInfo1 AS varchar) <> '' THEN CAST(FechaInfo1 AS varchar) END AS INFO1," + " CASE WHEN NOp2 = 0 THEN ' ' WHEN NOp2 > 0 AND CAST(FechaInfo2 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp2 > 0 AND CAST(FechaInfo2 AS varchar) <> '' THEN CAST(FechaInfo2 AS varchar) END AS INFO2," + " CASE WHEN NOp3 = 0 THEN ' ' WHEN NOp3 > 0 AND CAST(FechaInfo3 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp3 > 0 AND CAST(FechaInfo3 AS varchar) <> '' THEN CAST(FechaInfo3 AS varchar) END AS INFO3," + " CASE WHEN NOp4 = 0 THEN ' ' WHEN NOp4 > 0 AND CAST(FechaInfo4 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp4 > 0 AND CAST(FechaInfo4 AS varchar) <> '' THEN CAST(FechaInfo4 AS varchar) END AS INFO4," + " [FechaInfo1],[Informador1],[FechaInfo2],[Informador2],[FechaInfo3],[Informador3],[FechaInfo4],[Informador4],[FeedbackOPERARIOS], M.[FakeMode]," + " CASE WHEN O.Operario IS NULL THEN '' WHEN O.Operario IS NOT NULL THEN O.Operario END AS NOMOP1, CASE WHEN O2.Operario IS NULL THEN '' WHEN O2.Operario IS NOT NULL THEN O2.Operario END AS NOMOP2," + " CASE WHEN O3.Operario IS NULL THEN '' WHEN O3.Operario IS NOT NULL THEN O3.Operario END AS NOMOP3, CASE WHEN O4.Operario IS NULL THEN '' WHEN O4.Operario IS NOT NULL THEN O4.Operario END AS NOMOP4,ScrapAlmacen" + " " +
                        " FROM [SMARTH_DB].[dbo].[GP12_Historico_2021] C" +
                            " LEFT JOIN[SMARTH_DB].[dbo].[GP12_ProductosEstados] EST ON C.Referencia = EST.Referencia" +
                            " LEFT JOIN SMARTH_DB.DBO.AUX_Personal_Mandos J ON EST.Responsable = J.Id" +
                            " LEFT JOIN [SMARTH_DB].[dbo].[GP12_ProveedoresRevision] P ON C.ProveedorRevision = P.Id" +
                            " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] M ON C.Referencia = M.Referencia" +
                            " LEFT JOIN [SMARTH_DB].[dbo].[AUX_Lista_Clientes] CLI ON M.Cliente = CLI.Cliente" +
                            " LEFT JOIN [SMARTH_DB].[dbo].[AUX_GP12_RazonRevision] R ON C.RazonRevision = R.Id" +
                            " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] O on C.NOp1 = O.Id LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] O2 on C.NOp2 = O2.Id" +
                            " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] O3 on C.NOp3 = O3.Id LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] O4 on C.NOp4 = O4.Id) F" +
                            " WHERE ID = " + ID + "" +
                            " ORDER BY FechaInicio DESC";
                }

                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public void Actualizar_SCRAP_GP1XNAV(int año, int ID)

        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "";
                if (año < 2021)
                {
                    sql = "UPDATE [SMARTH_DB].[dbo].[GP12_Historico] SET [ScrapAlmacen] = 1 WHERE Id =" + ID;
                }
                else
                {
                    sql = "UPDATE [SMARTH_DB].[dbo].[GP12_Historico_2021] SET [ScrapAlmacen] = 1 WHERE Id =" + ID;
                }
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
            }
        }


        public DataTable Devuelve_Registro_COMUNICACIONES_ORDEN(int año, string filtroaño, string referencia, string molde, string lote, string cliente, string responsable, string operario, string firmado)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "";
                if (año < 2021)
                {
                    sql = "SELECT * FROM(" +
                          " SELECT [IDHistorico] AS ID, [FechaInicio],HorasInspeccion AS DoubleHoras, CAST([HorasInspeccion] AS varchar) + ' h' AS HORAS,P.Proveedor,[OperarioRevisionINT],[OperarioRevision],C.Referencia,[Nombre],C.Molde,[Nlote],[Ncaja],[PiezasRevisadas]," +
                          " [PiezasOK],[Retrabajadas],[PiezasNOK],[Def1],[Def2],[Def3],[Def4],[Def5],[Def6],[Def7],[Def8],[Def9],[Def10],[Def11],[Def12],[Def13],[Def14],[Def15],[Def16],[Def17]," +
                          " [Def18],[Def19],[Def20],[Def21],[Def22],[Def23],[Def24],[Def25],[Def26],[Def27],[Def28],[Def29],[Def30],[Incidencias],[ImagenDefecto1],[ImagenDefecto2]," +
                          " [ImagenDefecto3],[Notas],C.[NOp1],C.[NOp2],C.[NOp3],C.[NOp4],R.Razon,CAST([CosteRevision] AS varchar)+' €' AS CosteRevision,CAST([CostePiezaRevision] AS varchar)+' €' AS CostePiezaRevision,CAST([CosteScrapRevision] AS varchar)+' €' AS CosteScrapRevision, M.Cliente," +
                          " CASE WHEN NOp1 = 0 THEN ' ' WHEN NOp1 > 0 AND CAST(FechaInfo1 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp1 > 0 AND CAST(FechaInfo1 AS varchar) <> '' THEN CAST(FechaInfo1 AS varchar) END AS INFO1," +
                          " CASE WHEN NOp2 = 0 THEN ' ' WHEN NOp2 > 0 AND CAST(FechaInfo2 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp2 > 0 AND CAST(FechaInfo2 AS varchar) <> '' THEN CAST(FechaInfo2 AS varchar) END AS INFO2," +
                          " CASE WHEN NOp3 = 0 THEN ' ' WHEN NOp3 > 0 AND CAST(FechaInfo3 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp3 > 0 AND CAST(FechaInfo3 AS varchar) <> '' THEN CAST(FechaInfo3 AS varchar) END AS INFO3," +
                          " CASE WHEN NOp4 = 0 THEN ' ' WHEN NOp4 > 0 AND CAST(FechaInfo4 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp4 > 0 AND CAST(FechaInfo4 AS varchar) <> '' THEN CAST(FechaInfo4 AS varchar) END AS INFO4," +
                          " [FechaInfo1],[Informador1],[FechaInfo2],[Informador2],[FechaInfo3],[Informador3],[FechaInfo4],[Informador4],[FeedbackOPERARIOS], M.[FakeMode]," +
                          " CASE WHEN O.Operario IS NULL THEN '' WHEN O.Operario IS NOT NULL THEN O.Operario END AS NOMOP1, CASE WHEN O2.Operario IS NULL THEN '' WHEN O2.Operario IS NOT NULL THEN O2.Operario END AS NOMOP2," +
                          " CASE WHEN O3.Operario IS NULL THEN '' WHEN O3.Operario IS NOT NULL THEN O3.Operario END AS NOMOP3, CASE WHEN O4.Operario IS NULL THEN '' WHEN O4.Operario IS NOT NULL THEN O4.Operario END AS NOMOP4" +
                          " FROM [SMARTH_DB].[dbo].[GP12_Historico] C LEFT JOIN [SMARTH_DB].[dbo].[GP12_ProveedoresRevision] P ON C.ProveedorRevision = P.Id LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] M ON C.Referencia = M.Referencia" +
                          " LEFT JOIN [SMARTH_DB].[dbo].[AUX_GP12_RazonRevision] R ON C.RazonRevision = R.Id" +
                          " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] O on C.NOp1 = O.Id LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] O2 on C.NOp2 = O2.Id" +
                          " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] O3 on C.NOp3 = O3.Id LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] O4 on C.NOp4 = O4.Id) F" +
                          " WHERE NOp1 > 0 AND ([PiezasNOK] > 0 OR [Retrabajadas] > 0) " + filtroaño + "" + referencia + "" + molde + "" + lote + "" + cliente + "" + responsable + "" + operario + "" + firmado + "" +
                          " ORDER BY FechaInicio DESC";
                }
                else
                {
                    sql = "SELECT * FROM(" +
                            "SELECT C.ID, [FechaInicio],HorasInspeccion AS DoubleHoras, CAST([HorasInspeccion] AS varchar) + ' h' AS HORAS,P.Proveedor,[OperarioRevisionINT],[OperarioRevision],C.Referencia,[Nombre],C.Molde,[Nlote],[Ncaja],[PiezasRevisadas]," +
                            " [PiezasOK],[Retrabajadas],[PiezasNOK],[Def1],[Def2],[Def3],[Def4],[Def5],[Def6],[Def7],[Def8],[Def9],[Def10],[Def11],[Def12],[Def13],[Def14],[Def15],[Def16],[Def17]," +
                            " [Def18],[Def19],[Def20],[Def21],[Def22],[Def23],[Def24],[Def25],[Def26],[Def27],[Def28],[Def29],[Def30],[Incidencias],[ImagenDefecto1],[ImagenDefecto2]," +
                            " [ImagenDefecto3],[Notas],C.[NOp1],C.[NOp2],C.[NOp3],C.[NOp4],R.Razon,CAST([CosteRevision] AS varchar)+' €' AS CosteRevision,CAST([CostePiezaRevision] AS varchar)+' €' AS CostePiezaRevision,CAST([CosteScrapRevision] AS varchar)+' €' AS CosteScrapRevision, M.Cliente," +
                            " CASE WHEN NOp1 = 0 THEN ' ' WHEN NOp1 > 0 AND CAST(FechaInfo1 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp1 > 0 AND CAST(FechaInfo1 AS varchar) <> '' THEN CAST(FechaInfo1 AS varchar) END AS INFO1," +
                            " CASE WHEN NOp2 = 0 THEN ' ' WHEN NOp2 > 0 AND CAST(FechaInfo2 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp2 > 0 AND CAST(FechaInfo2 AS varchar) <> '' THEN CAST(FechaInfo2 AS varchar) END AS INFO2," +
                            " CASE WHEN NOp3 = 0 THEN ' ' WHEN NOp3 > 0 AND CAST(FechaInfo3 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp3 > 0 AND CAST(FechaInfo3 AS varchar) <> '' THEN CAST(FechaInfo3 AS varchar) END AS INFO3," +
                            " CASE WHEN NOp4 = 0 THEN ' ' WHEN NOp4 > 0 AND CAST(FechaInfo4 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp4 > 0 AND CAST(FechaInfo4 AS varchar) <> '' THEN CAST(FechaInfo4 AS varchar) END AS INFO4," +
                            " [FechaInfo1],[Informador1],[FechaInfo2],[Informador2],[FechaInfo3],[Informador3],[FechaInfo4],[Informador4],[FeedbackOPERARIOS], M.[FakeMode]," +
                            " CASE WHEN O.Operario IS NULL THEN '' WHEN O.Operario IS NOT NULL THEN O.Operario END AS NOMOP1, CASE WHEN O2.Operario IS NULL THEN '' WHEN O2.Operario IS NOT NULL THEN O2.Operario END AS NOMOP2," +
                            " CASE WHEN O3.Operario IS NULL THEN '' WHEN O3.Operario IS NOT NULL THEN O3.Operario END AS NOMOP3, CASE WHEN O4.Operario IS NULL THEN '' WHEN O4.Operario IS NOT NULL THEN O4.Operario END AS NOMOP4" +
                            " FROM [SMARTH_DB].[dbo].[GP12_Historico_2021] C" +
                            " LEFT JOIN [SMARTH_DB].[dbo].[GP12_ProveedoresRevision] P ON C.ProveedorRevision = P.Id" +
                            " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] M ON C.Referencia = M.Referencia" +
                            " LEFT JOIN [SMARTH_DB].[dbo].[AUX_GP12_RazonRevision] R ON C.RazonRevision = R.Id" +
                            " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] O on C.NOp1 = O.Id LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] O2 on C.NOp2 = O2.Id" +
                            " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] O3 on C.NOp3 = O3.Id LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] O4 on C.NOp4 = O4.Id) F" +
                            " WHERE NOp1 > 0 AND ([PiezasNOK] > 0 OR [Retrabajadas] > 0) " + filtroaño + "" + referencia + "" + molde + "" + lote + "" + cliente + "" + responsable + "" + operario + "" + firmado + "" +
                            " ORDER BY FechaInicio DESC";
                }



  
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_Registro_COMUNICACIONES_OPERARIO(int año, string filtroaño, string referencia, string molde, string lote, string cliente, string responsable, string operario, string firmado)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "";
                if (año < 2021)
                {
                    sql = "SELECT * FROM (SELECT C.Id, C.FechaInicio, M.Cliente, C.Referencia,C.Nombre, C.Nlote, C.PiezasRevisadas, C.PiezasOK, C.Retrabajadas, C.PiezasNOK, C.Incidencias, C.Notas, R.Razon, NOp," +
                                " CASE WHEN O.Operario IS NULL THEN '' WHEN O.Operario IS NOT NULL THEN O.Operario END AS NOMOP," +
                                " CASE WHEN NOp = 0 THEN ' ' WHEN NOp > 0 AND CAST(C.FechaInfo AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp > 0 AND CAST(FechaInfo AS varchar) <> '' THEN CAST(FechaInfo AS varchar) END AS INFO,FIRMANOP, Informador, FeedbackOPERARIOS" +
                          " FROM (SELECT [IDHistorico] AS ID, [FechaInicio], Referencia,[Nombre], Molde,[Nlote],[PiezasRevisadas],[PiezasOK],[Retrabajadas],[PiezasNOK],[Incidencias],[Notas],[NOp1] as NOp," +
                                 " CASE WHEN NOp1 = 0 THEN ' ' WHEN NOp1 > 0 AND CAST(FechaInfo1 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp1 > 0 AND CAST(FechaInfo1 AS varchar) <> '' THEN CAST(FechaInfo1 AS varchar) END AS INFO," +
                                 " [FechaInfo1] AS FECHAINFO,[Informador1] AS INFORMADOR, [FirmaNOp1] AS FIRMANOP, [FeedbackOPERARIOS], RazonRevision" +
                                 " FROM [SMARTH_DB].[dbo].[GP12_Historico]" +
                                 " WHERE NOp1 > 0 AND([PiezasNOK] > 0 OR[Retrabajadas] > 0)" +
                          " UNION ALL SELECT [IDHistorico] AS ID, [FechaInicio], Referencia,[Nombre], Molde,[Nlote],[PiezasRevisadas],[PiezasOK],[Retrabajadas],[PiezasNOK],[Incidencias],[Notas],[NOp2] as NOp," +
                                 " CASE WHEN NOp2 = 0 THEN ' ' WHEN NOp2 > 0 AND CAST(FechaInfo2 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp2 > 0 AND CAST(FechaInfo2 AS varchar) <> '' THEN CAST(FechaInfo2 AS varchar) END AS INFO," +
                                 " [FechaInfo2]  AS FECHAINFO,[Informador2]  AS INFORMADOR,[FirmaNOp2] AS FIRMANOP,[FeedbackOPERARIOS2]  AS [FeedbackOPERARIOS], RazonRevision" +
                                 " FROM [SMARTH_DB].[dbo].[GP12_Historico]" +
                                 " WHERE NOp2 > 0 AND([PiezasNOK] > 0 OR[Retrabajadas] > 0)" +
                          " UNION ALL SELECT [IDHistorico] AS ID, [FechaInicio], Referencia,[Nombre], Molde,[Nlote],[PiezasRevisadas],[PiezasOK],[Retrabajadas],[PiezasNOK],[Incidencias],[Notas],[NOp3] as NOp," +
                                 " CASE WHEN NOp3 = 0 THEN ' ' WHEN NOp3 > 0 AND CAST(FechaInfo3 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp3 > 0 AND CAST(FechaInfo3 AS varchar) <> '' THEN CAST(FechaInfo3 AS varchar) END AS INFO," +
                                 " [FechaInfo3]  AS FECHAINFO,[Informador3]  AS INFORMADOR,[FirmaNOp3] AS FIRMANOP,[FeedbackOPERARIOS3]  AS [FeedbackOPERARIOS], RazonRevision" +
                                 " FROM [SMARTH_DB].[dbo].[GP12_Historico]" +
                                 " WHERE NOp3 > 0 AND([PiezasNOK] > 0 OR[Retrabajadas] > 0)" +
                          " UNION ALL SELECT [IDHistorico] AS ID, [FechaInicio], Referencia,[Nombre], Molde,[Nlote],[PiezasRevisadas],[PiezasOK],[Retrabajadas],[PiezasNOK],[Incidencias],[Notas],[NOp4] as NOp," +
                                 " CASE WHEN NOp4 = 0 THEN ' ' WHEN NOp4 > 0 AND CAST(FechaInfo4 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp4 > 0 AND CAST(FechaInfo4 AS varchar) <> '' THEN CAST(FechaInfo4 AS varchar) END AS INFO," +
                                 " [FechaInfo4]  AS FECHAINFO,[Informador4]  AS INFORMADOR,[FirmaNOp4] AS FIRMANOP,[FeedbackOPERARIOS4]  AS [FeedbackOPERARIOS], RazonRevision" +
                                 " FROM [SMARTH_DB].[dbo].[GP12_Historico]" +
                                 " WHERE NOp4 > 0 AND([PiezasNOK] > 0 OR[Retrabajadas] > 0)) C" +
                         " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] M ON C.Referencia = M.Referencia" +
                         " LEFT JOIN [SMARTH_DB].[dbo].[AUX_GP12_RazonRevision] R ON C.RazonRevision = R.Id" +
                         " LEFT JOIN[SMARTH_DB].[dbo].[AUX_TablaOperarios] O on C.NOp = O.Id) F" +
                         " WHERE NOp IS NOT NULL " + filtroaño + "" + referencia + "" + molde + "" + lote + "" + cliente + "" + responsable + "" + operario + ""+firmado+"" +
                         " ORDER BY FechaInicio DESC";
                }
                else
                {
                    sql = "SELECT * FROM (SELECT C.Id, C.FechaInicio, M.Cliente, C.Referencia,C.Nombre, C.Nlote, C.PiezasRevisadas, C.PiezasOK, C.Retrabajadas, C.PiezasNOK, C.Incidencias, C.Notas, R.Razon, NOp," +
                                " CASE WHEN O.Operario IS NULL THEN '' WHEN O.Operario IS NOT NULL THEN O.Operario END AS NOMOP," +
                                " CASE WHEN NOp = 0 THEN ' ' WHEN NOp > 0 AND CAST(C.FechaInfo AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp > 0 AND CAST(FechaInfo AS varchar) <> '' THEN CAST(FechaInfo AS varchar) END AS INFO,FIRMANOP, Informador, FeedbackOPERARIOS" +
                          " FROM (SELECT ID, [FechaInicio], Referencia,[Nombre], Molde,[Nlote],[PiezasRevisadas],[PiezasOK],[Retrabajadas],[PiezasNOK],[Incidencias],[Notas],[NOp1] as NOp," +
                                 " CASE WHEN NOp1 = 0 THEN ' ' WHEN NOp1 > 0 AND CAST(FechaInfo1 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp1 > 0 AND CAST(FechaInfo1 AS varchar) <> '' THEN CAST(FechaInfo1 AS varchar) END AS INFO," +
                                 " [FechaInfo1] AS FECHAINFO,[Informador1] AS INFORMADOR, [FirmaNOp1] AS FIRMANOP, [FeedbackOPERARIOS], RazonRevision" +
                                 " FROM[SMARTH_DB].[dbo].[GP12_Historico_2021]" +
                                 " WHERE NOp1 > 0 AND([PiezasNOK] > 0 OR[Retrabajadas] > 0)" +
                          " UNION ALL SELECT ID, [FechaInicio], Referencia,[Nombre], Molde,[Nlote],[PiezasRevisadas],[PiezasOK],[Retrabajadas],[PiezasNOK],[Incidencias],[Notas],[NOp2] as NOp," +
                                 " CASE WHEN NOp2 = 0 THEN ' ' WHEN NOp2 > 0 AND CAST(FechaInfo2 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp2 > 0 AND CAST(FechaInfo2 AS varchar) <> '' THEN CAST(FechaInfo2 AS varchar) END AS INFO," +
                                 " [FechaInfo2]  AS FECHAINFO,[Informador2]  AS INFORMADOR,[FirmaNOp2] AS FIRMANOP,[FeedbackOPERARIOS2]  AS [FeedbackOPERARIOS], RazonRevision" +
                                 " FROM[SMARTH_DB].[dbo].[GP12_Historico_2021]" +
                                 " WHERE NOp2 > 0 AND([PiezasNOK] > 0 OR[Retrabajadas] > 0)" +
                          " UNION ALL SELECT ID, [FechaInicio], Referencia,[Nombre], Molde,[Nlote],[PiezasRevisadas],[PiezasOK],[Retrabajadas],[PiezasNOK],[Incidencias],[Notas],[NOp3] as NOp," +
                                 " CASE WHEN NOp3 = 0 THEN ' ' WHEN NOp3 > 0 AND CAST(FechaInfo3 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp3 > 0 AND CAST(FechaInfo3 AS varchar) <> '' THEN CAST(FechaInfo3 AS varchar) END AS INFO," +
                                 " [FechaInfo3]  AS FECHAINFO,[Informador3]  AS INFORMADOR,[FirmaNOp3] AS FIRMANOP,[FeedbackOPERARIOS3]  AS [FeedbackOPERARIOS], RazonRevision" +
                                 " FROM[SMARTH_DB].[dbo].[GP12_Historico_2021]" +
                                 " WHERE NOp3 > 0 AND([PiezasNOK] > 0 OR[Retrabajadas] > 0)" +
                          " UNION ALL SELECT ID, [FechaInicio], Referencia,[Nombre], Molde,[Nlote],[PiezasRevisadas],[PiezasOK],[Retrabajadas],[PiezasNOK],[Incidencias],[Notas],[NOp4] as NOp," +
                                 " CASE WHEN NOp4 = 0 THEN ' ' WHEN NOp4 > 0 AND CAST(FechaInfo4 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp4 > 0 AND CAST(FechaInfo4 AS varchar) <> '' THEN CAST(FechaInfo4 AS varchar) END AS INFO," +
                                 " [FechaInfo4]  AS FECHAINFO,[Informador4]  AS INFORMADOR,[FirmaNOp4] AS FIRMANOP,[FeedbackOPERARIOS4]  AS [FeedbackOPERARIOS], RazonRevision" +
                                 " FROM[SMARTH_DB].[dbo].[GP12_Historico_2021]" +
                                 " WHERE NOp4 > 0 AND([PiezasNOK] > 0 OR[Retrabajadas] > 0)) C" +
                         " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] M ON C.Referencia = M.Referencia" +
                         " LEFT JOIN [SMARTH_DB].[dbo].[AUX_GP12_RazonRevision] R ON C.RazonRevision = R.Id" +
                         " LEFT JOIN[SMARTH_DB].[dbo].[AUX_TablaOperarios] O on C.NOp = O.Id)F" +
                         " WHERE NOp IS NOT NULL " + filtroaño + "" + referencia + "" + molde + "" + lote + "" + cliente + "" + responsable + "" + operario + ""+firmado+"" +
                         " ORDER BY FechaInicio DESC";
                        }
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Modal_Detalles_Revision(string idrevision, string año)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
               
                string sql = "SELECT C.ID, HorasInspeccion AS DoubleHoras, [FechaInicio],CAST([HorasInspeccion] AS varchar) AS HORAS,P.Proveedor,[OperarioRevisionINT],[OperarioRevision],C.Referencia,[Nombre],C.Molde,[Nlote],[Ncaja],[PiezasRevisadas]," +
                "[PiezasOK],[Retrabajadas],[PiezasNOK],[Def1],[Def2],[Def3],[Def4],[Def5],[Def6],[Def7],[Def8],[Def9],[Def10],[Def11],[Def12],[Def13],[Def14],[Def15],[Def16],[Def17]," +
                "[Def18],[Def19],[Def20],[Def21],[Def22],[Def23],[Def24],[Def25],[Def26],[Def27],[Def28],[Def29],[Def30],[Incidencias],[ImagenDefecto1],[ImagenDefecto2],M.[FakeMode]," +
                "[ImagenDefecto3],[Notas],C.NOp1, O.Operario AS OPERARIO1,C.NOp2, O2.Operario AS OPERARIO2,C.NOp3, O3.Operario AS OPERARIO3,C.NOp4, O4.Operario AS OPERARIO4,R.Razon,CAST([CosteRevision] AS varchar)+' €' AS CosteRevision,CAST([CostePiezaRevision] AS varchar)+' €' AS CostePiezaRevision,CAST([CosteScrapRevision] AS varchar)+' €' AS CosteScrapRevision, M.Cliente," +
                "CASE WHEN NOp1 = 0 THEN ' ' WHEN NOp1 > 0 AND CAST(FechaInfo1 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp1 > 0 AND CAST(FechaInfo1 AS varchar) <> '' THEN CAST(FechaInfo1 AS varchar) END AS INFO1," +
                "CASE WHEN NOp2 = 0 THEN ' ' WHEN NOp2 > 0 AND CAST(FechaInfo2 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp2 > 0 AND CAST(FechaInfo2 AS varchar) <> '' THEN CAST(FechaInfo2 AS varchar) END AS INFO2," +
                "CASE WHEN NOp3 = 0 THEN ' ' WHEN NOp3 > 0 AND CAST(FechaInfo3 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp3 > 0 AND CAST(FechaInfo3 AS varchar) <> '' THEN CAST(FechaInfo3 AS varchar) END AS INFO3," +
                "CASE WHEN NOp4 = 0 THEN ' ' WHEN NOp4 > 0 AND CAST(FechaInfo4 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp4 > 0 AND CAST(FechaInfo4 AS varchar) <> '' THEN CAST(FechaInfo4 AS varchar) END AS INFO4," +
                "[FechaInfo1],[Informador1],[FechaInfo2],[Informador2],[FechaInfo3],[Informador3],[FechaInfo4],[Informador4],[FeedbackOPERARIOS],[FeedbackOPERARIOS2],[FeedbackOPERARIOS3],[FeedbackOPERARIOS4],[FirmaNOp1],[FirmaNOp2],[FirmaNOp3],[FirmaNOp4]" +
                " FROM [SMARTH_DB].[dbo].[GP12_Historico_2021] C LEFT JOIN [SMARTH_DB].[dbo].[GP12_ProveedoresRevision] P ON C.ProveedorRevision = P.Id LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] M ON C.Referencia = M.Referencia" +
                " LEFT JOIN [SMARTH_DB].[dbo].[AUX_GP12_RazonRevision] R ON C.RazonRevision = R.Id LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] O on C.NOp1 = O.Id" +
                " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] O2 on C.NOp2 = O2.Id LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] O3 on C.NOp3 = O3.Id" +
                " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] O4 on C.NOp4 = O4.Id where C.ID = " + idrevision + "" +
                " UNION ALL" +
                " SELECT C.[IDHistorico] AS ID, HorasInspeccion AS DoubleHoras, [FechaInicio],CAST([HorasInspeccion] AS varchar) AS HORAS,P.Proveedor,[OperarioRevisionINT],[OperarioRevision],C.Referencia,[Nombre],C.Molde,[Nlote],[Ncaja],[PiezasRevisadas]," +
                "[PiezasOK],[Retrabajadas],[PiezasNOK],[Def1],[Def2],[Def3],[Def4],[Def5],[Def6],[Def7],[Def8],[Def9],[Def10],[Def11],[Def12],[Def13],[Def14],[Def15],[Def16],[Def17]," +
                "[Def18],[Def19],[Def20],[Def21],[Def22],[Def23],[Def24],[Def25],[Def26],[Def27],[Def28],[Def29],[Def30],[Incidencias],[ImagenDefecto1],[ImagenDefecto2],M.[FakeMode]," +
                "[ImagenDefecto3],[Notas],C.NOp1, O.Operario AS OPERARIO1,C.NOp2, O2.Operario AS OPERARIO2,C.NOp3, O3.Operario AS OPERARIO3,C.NOp4, O4.Operario AS OPERARIO4,R.Razon,CAST([CosteRevision] AS varchar)+' €' AS CosteRevision,CAST([CostePiezaRevision] AS varchar)+' €' AS CostePiezaRevision,CAST([CosteScrapRevision] AS varchar)+' €' AS CosteScrapRevision, M.Cliente," +
                "CASE WHEN NOp1 = 0 THEN ' ' WHEN NOp1 > 0 AND CAST(FechaInfo1 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp1 > 0 AND CAST(FechaInfo1 AS varchar) <> '' THEN CAST(FechaInfo1 AS varchar) END AS INFO1," +
                "CASE WHEN NOp2 = 0 THEN ' ' WHEN NOp2 > 0 AND CAST(FechaInfo2 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp2 > 0 AND CAST(FechaInfo2 AS varchar) <> '' THEN CAST(FechaInfo2 AS varchar) END AS INFO2," +
                "CASE WHEN NOp3 = 0 THEN ' ' WHEN NOp3 > 0 AND CAST(FechaInfo3 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp3 > 0 AND CAST(FechaInfo3 AS varchar) <> '' THEN CAST(FechaInfo3 AS varchar) END AS INFO3," +
                "CASE WHEN NOp4 = 0 THEN ' ' WHEN NOp4 > 0 AND CAST(FechaInfo4 AS varchar) IS NULL THEN 'Pdte. informar' WHEN NOp4 > 0 AND CAST(FechaInfo4 AS varchar) <> '' THEN CAST(FechaInfo4 AS varchar) END AS INFO4," +
                "[FechaInfo1],[Informador1],[FechaInfo2],[Informador2],[FechaInfo3],[Informador3],[FechaInfo4],[Informador4],[FeedbackOPERARIOS],[FeedbackOPERARIOS2],[FeedbackOPERARIOS3],[FeedbackOPERARIOS4],[FirmaNOp1],[FirmaNOp2],[FirmaNOp3],[FirmaNOp4]" +
                " FROM [SMARTH_DB].[dbo].[GP12_Historico] C LEFT JOIN [SMARTH_DB].[dbo].[GP12_ProveedoresRevision] P ON C.ProveedorRevision = P.Id LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] M ON C.Referencia = M.Referencia" +
                " LEFT JOIN [SMARTH_DB].[dbo].[AUX_GP12_RazonRevision] R ON C.RazonRevision = R.Id LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] O on C.NOp1 = O.Id" +
                " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] O2 on C.NOp2 = O2.Id LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] O3 on C.NOp3 = O3.Id" +
                " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] O4 on C.NOp4 = O4.Id where C.[IDHistorico] = " + idrevision + "" +
                " ORDER BY FechaInicio DESC";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public void Actualizar_GP12_OPERARIOREVISION(int id, string queryinfo)

        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "";
                if (id > 900000000)
                {
                    sql = "UPDATE [SMARTH_DB].[dbo].[GP12_Historico]" + queryinfo + " WHERE Id =" + id;
                }
                else
                { 
                    sql = "UPDATE [SMARTH_DB].[dbo].[GP12_Historico_2021]" + queryinfo + " WHERE Id =" + id;
                }
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
            }
        }

        //**********************************************\\METROLOGIA//**********************************************\\
        //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\EQUIPOS\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\//\\
        public bool Insertar_Equipo_Metrologia(int equipo, string nombre, string clase, int FrecuenciaCal, string TipoCal, string MedioCal, int CriterioAcep, int MSA, string Ubicacion, int EstadoEquipo, string notas )
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql2 = "INSERT INTO [SMARTH_DB].[dbo].[METROLOGIA_Listados_Equipos]" +
                              "([NumEquipo],[NequipoORI],[Nombre],[Imagen],[Clase],[FechaAlta],[FrecuenciaCalibracion],[TipoCalibracion],[MedioCalibracion],[CriterioAceptacion],[Msa],[Ubicacion],[EstadoEquipo],[Eliminar],[NotasEquipo],[Departamento])" +
                    " VALUES ('" + equipo + "', '" + equipo + "','" + nombre + "','../SMARTH_docs/METROLOGIA/sin_imagen.jpg','" + clase + "',convert(varchar, getdate(), 103)," + FrecuenciaCal + ",'" + TipoCal + "','" + MedioCal + "'," + CriterioAcep + ",'" + MSA + "','" + Ubicacion + "'," + EstadoEquipo + ",0,'" + notas + "','-')";
                cmd.CommandText = sql2;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }

        public bool Existe_Equipo_Metrologia(string equipo)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT * FROM [SMARTH_DB].[dbo].[METROLOGIA_Listados_Equipos] WHERE NumEquipo = '" + equipo + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_GP12.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                cnn_GP12.Close();
                return false;
            }
        }


        public string Devuelve_MAX_Metrologia()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;

                string sql = "SELECT MAX([NumEquipo]) + 1 AS NUMEQUIPO  FROM [SMARTH_DB].[dbo].[METROLOGIA_Listados_Equipos]";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return ds.Tables[0].Rows[0]["NUMEQUIPO"].ToString();
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return "";
            }
        }

        public DataTable Devuelve_Pendientes_Metrologia_MSA()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;

                string sql = "SELECT EQU.ID,[NumEquipo],[Nombre]" +
                                " FROM[SMARTH_DB].[dbo].[METROLOGIA_Listados_Equipos] EQU" +
                                " FULL OUTER JOIN SMARTH_DB.DBO.METROLOGIA_ListadoMSA MSA ON EQU.NumEquipo = MSA.Equipo" +
                                " where Msa<> 3 AND Msa<> 2  AND EQU.EstadoEquipo <> 2 AND(EQU.NumEquipo IS NULL OR MSA.Equipo IS NULL)";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_Pendientes_Metrologia_Calibraciones(string where)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;

                string sql = "SELECT CASE WHEN CAL.FechaDoc IS NULL THEN '2000-01-01 00:00:00.000' ELSE DATEADD(year, FrecuenciaCalibracion, CAL.FechaDoc) END as VenceCalibracion,*"+
                                " FROM[SMARTH_DB].[dbo].[METROLOGIA_Listados_Equipos] EQU"+
                                " LEFT JOIN(SELECT[Equipo], MAX([FechaDoc]) AS FechaDoc FROM[SMARTH_DB].[dbo].[METROLOGIA_ListadoCalibraciones] GROUP BY[Equipo]) CAL ON EQU.NumEquipo = CAL.Equipo" +
                                " WHERE Eliminar<> 'true' and EstadoEquipo < 2 and TipoCalibracion <> 'N/A'"+where+""+
                                " ORDER BY VenceCalibracion DESC";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_Auxiliares_Metrologia()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
               
                string sql = "SELECT * FROM[SMARTH_DB].[dbo].[METROLOGIA_Auxiliares]";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_Listado_Equipos_Metrologia_SEPARADOR()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                
                string sql = "SELECT NumEquipo + ' ¬ ' + Nombre + ' ¬ ' + Clase as Equipo FROM[SMARTH_DB].[dbo].[METROLOGIA_Listados_Equipos] WHERE Eliminar <> 'true' ORDER BY NumEquipo asc";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_Listado_Ubicaciones_Metrologia()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;

                string sql = "SELECT DISTINCT UBICACION  FROM[SMARTH_DB].[dbo].[METROLOGIA_Listados_Equipos]";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_EquipoXProducto(string equipo)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;

                string sql = " SELECT leq.ID, EXP.[NUMEQUIPO], leq.Nombre, cast(exp.[REFERENCIA] as varchar) + ' ' + pro.Descripcion as Producto, cli.LogotipoSM" +
                                " FROM[SMARTH_DB].[dbo].[METROLOGIA_EquiposXProductos] EXP" +
                                " inner join [SMARTH_DB].[dbo].[AUX_TablaProductos] PRO on exp.REFERENCIA = pro.Referencia" +
                                " left join SMARTH_DB.dbo.AUX_Lista_Clientes CLI on pro.Cliente = cli.Cliente" +
                                " left join SMARTH_DB.dbo.METROLOGIA_Listados_Equipos LEQ ON exp.NUMEQUIPO = leq.NumEquipo" +
                                " where EXP.REFERENCIA = " + equipo+"";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }


        public bool Actualizar_equipo_Metrologia(string equipo, string nombre, string clase, string Nserie, string Rango, string DivisionEscala, string Departamento, string Propietario, string Fabricante, string FechaAlta, int FrecuenciaCalibracion, string TipoCalibracion, string MedioCalibracion, int CriterioAceptacion, string MSA, string InstMantenimiento, string InstUso, string Ubicacion, int EstadoEquipo, string Notas, string alternativo, string notasalternativo)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql2 = "UPDATE [SMARTH_DB].[dbo].[METROLOGIA_Listados_Equipos]"+
                              " SET [Nombre] = '"+nombre+ "',[Clase] = '" + clase + "',[Nserie] = '" + Nserie + "',[Rango]  = '" + Rango + "',[DivisionEscala] = '" + DivisionEscala + "',[Departamento] = '" + Departamento + "',[Propietario]  = '" + Propietario + "' ,[Fabricante]  = '" + Fabricante + "',[FechaAlta]  = '" + FechaAlta + "',[FrecuenciaCalibracion] = " + FrecuenciaCalibracion + ",[TipoCalibracion] = '" + TipoCalibracion + "',[MedioCalibracion] = '" + MedioCalibracion + "',[CriterioAceptacion] = " + CriterioAceptacion + ",[Msa] = '" + MSA + "',[InstruccionMantenimiento]  = '" + InstMantenimiento + "',[InstruccionUso]  = '" + InstUso + "',[Ubicacion]   = '" + Ubicacion + "',[EstadoEquipo]  = " + EstadoEquipo + ", [NotasEquipo] = '"+Notas+ "',[Alternativo] = '" + alternativo + "',[AlternativoInstruccion] = '" + notasalternativo + "' " +
                              " WHERE [NumEquipo] = '"+ equipo + "'";
                cmd.CommandText = sql2;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }

       
        public DataTable Devuelve_Listado_Equipos_Metrologia(string equipo, string tipos, string vencidos, string obsoletos, string orderby)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;

                string sql = " SELECT CASE WHEN CAL.FechaDoc IS NULL THEN '2000-01-01 00:00:00.000' ELSE DATEADD(year, FrecuenciaCalibracion, CAL.FechaDoc) END as VenceCalibracion," +
                               " CASE WHEN EQU.MSA = 1 and MSA.MSAVALIDADO IS NOT NULL THEN CAST(MSA.MSAVALIDADO AS VARCHAR) WHEN EQU.MSA = 1 and MSA.MSAVALIDADO IS NULL THEN 'Pendiente' WHEN EQU.MSA = 2 THEN   'Familia de equipos' WHEN EQU.MSA = 3 THEN   'N/A' END as EstadoMSA," +
                               " CASE WHEN EQU.EstadoEquipo = 0 THEN 'PEND. VALIDAR' WHEN EQU.EstadoEquipo = 1 THEN 'CALIBRADO' WHEN EQU.EstadoEquipo = 2 THEN 'FUERA DE USO' END AS EstadoEquipoTEXT,*" +
                               " FROM[SMARTH_DB].[dbo].[METROLOGIA_Listados_Equipos] EQU" +
                               //" LEFT JOIN[SMARTH_DB].[dbo].[METROLOGIA_ListadoCalibraciones] CAL ON EQU.NumEquipo = CAL.Equipo" +
                               " LEFT JOIN (SELECT [Equipo],MAX([FechaDoc]) AS FechaDoc FROM [SMARTH_DB].[dbo].[METROLOGIA_ListadoCalibraciones] GROUP BY [Equipo]) CAL ON EQU.NumEquipo = CAL.Equipo"+
                               " LEFT JOIN (SELECT [Equipo],MAX([FechaDoc]) AS MSAVALIDADO FROM [SMARTH_DB].[dbo].[METROLOGIA_ListadoMSA] GROUP BY Equipo) MSA ON EQU.NumEquipo = MSA.Equipo" +
                               " WHERE Eliminar <> 'true' " + equipo + "" + tipos + "" + vencidos + "" + obsoletos + "" + orderby + "";


                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_Listado_Equipos_MetrologiaETIQUETAS(string equipo)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;

                string sql = " SELECT CAST(CASE WHEN CAL.FechaDoc IS NULL THEN CAST('2000-01-01 00:00:00.000' AS DATE) ELSE CAST(DATEADD(year, FrecuenciaCalibracion, CAL.FechaDoc) AS DATE) END AS VARCHAR) as VenceCalibracion, CAST(CAST(CAL.FechaDoc as DATE) AS varchar) AS UltCalib," +
                               " CASE WHEN EQU.MSA = 1 and MSA.MSAVALIDADO IS NOT NULL THEN CAST(MSA.MSAVALIDADO AS VARCHAR) WHEN EQU.MSA = 1 and MSA.MSAVALIDADO IS NULL THEN 'Pendiente' WHEN EQU.MSA = 2 THEN   'Familia de equipos' WHEN EQU.MSA = 3 THEN   'N/A' END as EstadoMSA," +
                               " CASE WHEN EQU.EstadoEquipo = 0 THEN 'PEND. VALIDAR' WHEN EQU.EstadoEquipo = 1 THEN 'CALIBRADO' WHEN EQU.EstadoEquipo = 2 THEN 'FUERA DE USO' END AS EstadoEquipoTEXT,*" +
                               " FROM[SMARTH_DB].[dbo].[METROLOGIA_Listados_Equipos] EQU" +
                               //" LEFT JOIN[SMARTH_DB].[dbo].[METROLOGIA_ListadoCalibraciones] CAL ON EQU.NumEquipo = CAL.Equipo" +
                               " LEFT JOIN (SELECT [Equipo],MAX([FechaDoc]) AS FechaDoc FROM [SMARTH_DB].[dbo].[METROLOGIA_ListadoCalibraciones] GROUP BY [Equipo]) CAL ON EQU.NumEquipo = CAL.Equipo" +
                               " LEFT JOIN (SELECT [Equipo],MAX([FechaDoc]) AS MSAVALIDADO FROM [SMARTH_DB].[dbo].[METROLOGIA_ListadoMSA] GROUP BY Equipo) MSA ON EQU.NumEquipo = MSA.Equipo" +
                               "" + equipo + "";


                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_Listado_Propietarios_METROLOGIA()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT case when [Propietario] is null then '-' else [Propietario] END AS Propietario FROM[SMARTH_DB].[dbo].[METROLOGIA_Listados_Equipos] group by Propietario";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_Listado_Fabricantes_METROLOGIA()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT case when [Fabricante] is null then '-' else [Fabricante] END AS Fabricante FROM[SMARTH_DB].[dbo].[METROLOGIA_Listados_Equipos] group by Fabricante";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public bool Existe_MSA(string equipo)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT * FROM[SMARTH_DB].[dbo].[METROLOGIA_ListadoMSA] WHERE Equipo = '"+equipo+"'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_GP12.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                cnn_GP12.Close();
                return false;
            }
        }

        public DataTable Devuelve_Listado_MSA(string equipo)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT MSA.[Id],[Equipo],CAL.Clase, MSA.Descripcion, CASE WHEN[TipoMSA] = 0 THEN '' WHEN[TipoMSA] = 1 THEN 'R&R Atributos' WHEN[TipoMSA] = 2 THEN 'R&R Variables'  WHEN[TipoMSA] = 3 THEN 'Otros' END AS[TipoMSA], PER.Nombre,[FechaDoc],[URLDocumento],[Resultado]" +
                                " FROM[SMARTH_DB].[dbo].[METROLOGIA_ListadoMSA] MSA" +
                                " LEFT JOIN AUX_Personal_Mandos PER ON MSA.Responsable = PER.Id" +
                                " LEFT JOIN SMARTH_DB.DBO.METROLOGIA_Listados_Equipos CAL ON MSA.Equipo = CAL.NumEquipo"+
                                " WHERE MSA.ID > 0 " + equipo+ " order by MSA.[FechaDoc] desc";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public bool Insertar_MSA(string equipo, int tipomsa, int responsable, string Fechadoc, string Fechasubida, string URL, string descripcion, string resultado)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql2 = "INSERT INTO [SMARTH_DB].[dbo].[METROLOGIA_ListadoMSA] ([Equipo],[TipoMSA],[Responsable],[FechaDoc],[FechaSubida],[URLDocumento],[Descripcion],[Resultado]) VALUES" +
                                           "('" + equipo + "'," + tipomsa + "," + responsable + ",'" + Fechadoc + "','" + Fechasubida + "','" + URL + "','"+descripcion+ "','" + resultado + "')";
                cmd.CommandText = sql2;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }

        public DataTable Devuelve_Detalle_MSA(string urldocumento)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = " SELECT[Equipo],[TipoMSA],[Responsable], PER.Nombre,[FechaDoc],[FechaSubida],[URLDocumento],[Descripcion],[Resultado]" +
                                 " FROM[SMARTH_DB].[dbo].[METROLOGIA_ListadoMSA] MSA" +
                                 " LEFT JOIN SMARTH_DB.DBO.AUX_Personal_Mandos PER ON MSA.Responsable = PER.Id" +
                                 " where URLDocumento = '" + urldocumento + "'";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public bool Actualizar_MSA_BD(string URL, int TipoMSA, int Responsable, string Fechadoc, string descripcion, string resultadoobtenido)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql2 = " UPDATE [SMARTH_DB].[dbo].[METROLOGIA_ListadoMSA]" +
                                " SET [TipoMSA] = "+TipoMSA+ ",[Responsable] = " + Responsable + ",[FechaDoc] = '"+Fechadoc+"',[Descripcion] = '"+descripcion+"',[Resultado] = '"+resultadoobtenido+"'" +
                                " WHERE URLDocumento = '" + URL + "'";
                cmd.CommandText = sql2;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }

        public bool Eliminar_MSA(string documento)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql2 = "DELETE FROM [SMARTH_DB].[dbo].[METROLOGIA_ListadoMSA] WHERE [URLDocumento] like '%" + documento + "'"; 
                cmd.CommandText = sql2;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }

        public DataTable Devuelve_Listado_DOCAUX_Metrologia(string equipo)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = " SELECT DOC.[Id],[Equipo], DOC.Descripcion, CASE WHEN[TipoDOC] = 0 THEN '' WHEN[TipoDOC] = 1 THEN 'Manual de uso' WHEN[TipoDOC] = 2 THEN 'Manual de mantenimiento'  WHEN[TipoDOC] = 3 THEN 'Planos/CAD de equipo' WHEN[TipoDOC] = 5 THEN 'Proceso/equipo alternativo' ELSE 'Otros' END AS [TipoDOC], PER.Nombre,[FechaDoc],[URLDocumento]" +
                                " FROM[SMARTH_DB].[dbo].[METROLOGIA_ListadoDOCAUX] DOC" +
                                " LEFT JOIN AUX_Personal_Mandos PER ON DOC.Responsable = PER.Id" +
                                 " LEFT JOIN SMARTH_DB.DBO.METROLOGIA_Listados_Equipos CAL ON DOC.Equipo = CAL.NumEquipo" +
                                 " WHERE DOC.ID > 0" + equipo + ""+
                                 " order by DOC.[FechaDoc] desc";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public bool Insertar_DOCAUX_Metrologia(string equipo, int tipoDOC, int responsable, string Fechadoc, string Fechasubida, string URL, string descripcion)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql2 = "INSERT INTO [SMARTH_DB].[dbo].[METROLOGIA_ListadoDOCAUX] ([Equipo],[TipoDOC],[Responsable],[FechaDoc],[FechaSubida],[URLDocumento],[Descripcion]) VALUES" +
                                           "('" + equipo + "'," + tipoDOC + "," + responsable + ",'" + Fechadoc + "','" + Fechasubida + "','" + URL + "','" + descripcion + "')";
                cmd.CommandText = sql2;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception EX)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }

        public DataTable Devuelve_Detalle_DOCAUX_Metrologia(string urldocumento)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = " SELECT PER.Nombre,[Equipo],[TipoDOC],[Responsable],[FechaDoc],[FechaSubida],[URLDocumento],[Descripcion]" +
                                 " FROM [SMARTH_DB].[dbo].[METROLOGIA_ListadoDOCAUX] DOC" +
                                 " LEFT JOIN SMARTH_DB.DBO.AUX_Personal_Mandos PER ON DOC.Responsable = PER.Id" +
                                 " where URLDocumento = '" + urldocumento + "'";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public bool Actualizar_DOCAUX_Metrologia_BD(string URL, int tipoDOC, int Responsable, string Fechadoc, string descripcion)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql2 = " UPDATE [SMARTH_DB].[dbo].[METROLOGIA_ListadoDOCAUX]" +
                                " SET [TipoDOC] = " + tipoDOC + ",[Responsable] = " + Responsable + ",[FechaDoc] = '" + Fechadoc + "',[Descripcion] = '" + descripcion + "'" +
                                " WHERE URLDocumento = '" + URL + "'";
                cmd.CommandText = sql2;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }

        public bool Eliminar_DOCAUX_Metrologia(string documento)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql2 = "DELETE FROM [SMARTH_DB].[dbo].[METROLOGIA_ListadoDOCAUX] WHERE [URLDocumento] like '%" + documento + "'";
                cmd.CommandText = sql2;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }


        public bool Insertar_Imagen_Equipo_Metrologia(string equipo, string imagen)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql2 = "UPDATE [SMARTH_DB].[dbo].[METROLOGIA_Listados_Equipos]" +
                                " SET [Imagen] = '" + imagen + "'" +
                                " WHERE [NumEquipo] = '" + equipo + "'";
                cmd.CommandText = sql2;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }

        public DataTable Devuelve_Listado_EquipoXProducto(string equipo)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT EQ.Id, [NUMEQUIPO], EQ.[REFERENCIA],PRD.Descripcion, CASE WHEN CLI.Logotipo IS NULL THEN '' ELSE CLI.Logotipo END AS ClienteLogo" +
                             " FROM[SMARTH_DB].[dbo].[METROLOGIA_EquiposXProductos] EQ"+
                             " left join [SMARTH_DB].[dbo].[AUX_TablaProductos] PRD on EQ.REFERENCIA = PRD.Referencia"+
                             " LEFT JOIN SMARTH_DB.DBO.AUX_Lista_Clientes CLI ON PRD.Cliente = CLI.Cliente"+
                             " WHERE NUMEQUIPO = " +equipo+"";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public bool Insertar_Metrologia_ProductoXEquipo(string equipo, string producto)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql2 = "INSERT INTO [SMARTH_DB].[dbo].[METROLOGIA_EquiposXProductos] ([NUMEQUIPO],[REFERENCIA]) VALUES" +
                                           "('" + equipo + "'," + producto + ")";
                cmd.CommandText = sql2;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception EX)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }

        public bool Eliminar_Metrologia_ProductoXEquipo(string id)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql2 = "DELETE FROM [SMARTH_DB].[dbo].[METROLOGIA_EquiposXProductos] WHERE [Id] = "+ id + "";
                cmd.CommandText = sql2;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }


        public DataTable Devuelve_Listado_Calibracion(string equipo)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT MSA.[Id],[Equipo],[TipoCalibracion],[CriterioCalibracion],CASE WHEN[CriterioCalibracion] = 0 THEN '-' WHEN[CriterioCalibracion] = 1 THEN '1/10 < 2I/T < 1/3' WHEN[CriterioCalibracion] = 2 THEN '10 >T/2U > 3' END AS CriterioChar,PER.Nombre,[Responsable],[FechaDoc],[FechaSubida],[URLDocumento],[Descripcion],[ResultadoObtenido],[ResultadoCalibracion]" +
                                    " ,CASE WHEN [TipoCalibracion] = 0 THEN '-' WHEN [TipoCalibracion] = 1 THEN 'Certificado' WHEN [TipoCalibracion] = 2 THEN 'Dimensional' WHEN [TipoCalibracion] = 3 THEN 'Otros' END AS TipoCalibracionCHAR,[INT_EXT],[EntidadCertificadora]" +
                                    " FROM[SMARTH_DB].[dbo].[METROLOGIA_ListadoCalibraciones] MSA" +
                                    " LEFT JOIN AUX_Personal_Mandos PER ON MSA.Responsable = PER.Id" +
                                    " WHERE Equipo = '" + equipo + "' ORDER BY MSA.FechaDoc DESC";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_Detalle_Calibracion(string equipo)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT CAL.[Id],[Equipo],[CriterioCalibracion],PER.Nombre,[FechaDoc],[FechaSubida],[URLDocumento],[Descripcion],[ResultadoObtenido],[ResultadoCalibracion],[TipoCalibracion],[INT_EXT],[EntidadCertificadora]"+
                                " FROM[SMARTH_DB].[dbo].[METROLOGIA_ListadoCalibraciones] CAL"+
                                " LEFT JOIN SMARTH_DB.DBO.AUX_Personal_Mandos PER ON CAL.Responsable = PER.Id"+
                                " where URLDocumento = '" + equipo + "'";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public bool Insertar_Calibracion(string equipo, int criterio, int responsable, string Fechadoc, string Fechasubida, string URL, string descripcion, string resultadoobtenido, string resultadofinal, int tipocalibracion, string intext, string calibrador)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql2 = "INSERT INTO [SMARTH_DB].[dbo].[METROLOGIA_ListadoCalibraciones] ([Equipo],[CriterioCalibracion],[Responsable],[FechaDoc],[FechaSubida],[URLDocumento],[Descripcion],[ResultadoObtenido],[ResultadoCalibracion],[TipoCalibracion],[INT_EXT],[EntidadCertificadora]) VALUES" +
                                                           "('" + equipo + "'," + criterio + "," + responsable + ",'" + Fechadoc + "','" + Fechasubida + "','" + URL + "','" + descripcion + "'," + resultadoobtenido.Replace(",",".") + ",'" + resultadofinal + "',"+tipocalibracion+", '"+intext.Replace(",", ".") + "','"+calibrador+"')";
                cmd.CommandText = sql2;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }

        public bool Actualizar_Calibracion_BD(string URL, int criterio, int responsable, string Fechadoc, string descripcion, string resultadoobtenido, string resultadofinal, int tipocalibracion, string intext, string calibrador)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql2 = " UPDATE [SMARTH_DB].[dbo].[METROLOGIA_ListadoCalibraciones]"+
                                " SET[CriterioCalibracion] = "+criterio+",[Responsable] = "+responsable+ ",[FechaDoc] = '" + Fechadoc + "',[Descripcion] = '" + descripcion + "',[ResultadoObtenido] = "+ resultadoobtenido.Replace(",",".") + ",[ResultadoCalibracion] = '"+ resultadofinal + "',[TipoCalibracion] = "+tipocalibracion+",[INT_EXT] = '"+intext+"',[EntidadCertificadora] = '"+calibrador+"'" +
                                " WHERE URLDocumento = '"+URL+"'";
                cmd.CommandText = sql2;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }

        public bool Eliminar_Calibracion(string documento)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql2 = "DELETE FROM [SMARTH_DB].[dbo].[METROLOGIA_ListadoCalibraciones] WHERE [URLDocumento] like '%" + documento + "'";
                cmd.CommandText = sql2;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }

        public bool Actualizar_Prevision_Calibracion(string equipo, string fechaset)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "";
                if (fechaset == "")
                {
                    sql = "UPDATE [SMARTH_DB].[dbo].[METROLOGIA_Listados_Equipos]" +
                               " SET [CalibracionPlanificada] = NULL" +
                               " WHERE [NumEquipo] = '" + equipo + "'";
                }
                else
                {
                    sql = "UPDATE [SMARTH_DB].[dbo].[METROLOGIA_Listados_Equipos]" +
                               " SET [CalibracionPlanificada] = '" + fechaset + "'" +
                               " WHERE [NumEquipo] = '" + equipo + "'";
                }
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }


        //**********************************************\\MANTENIMIENTO//**********************************************\\
        //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\MOLDES\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\//\\

        public DataTable Devuelve_Auxiliares_Mantenimiento()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT *" +
                             " FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Aux_Opciones]";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public int Devuelve_IDTrabajo(string trabajo)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;

                string sql = "SELECT Id FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Aux_Opciones] WHERE [TipoMantenimientoMolde] = '" + trabajo + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return Convert.ToInt16(ds.Tables[0].Rows[0]["Id"].ToString());
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return 0;
            }
        }

        public string Devuelve_TipoMantenimiento(int trabajo)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;

                string sql = "SELECT TipoMantenimientoMolde FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Aux_Opciones] WHERE [Id] = '" + trabajo + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return ds.Tables[0].Rows[0]["TipoMantenimientoMolde"].ToString();
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return "";
            }
        }

        public string Devuelve_TipoReparacion(int trabajo)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;

                string sql = "SELECT TipoReparación FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Aux_Opciones] WHERE [Id] = '" + trabajo + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return ds.Tables[0].Rows[0]["TipoReparación"].ToString();
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return "";
            }
        }

        public string Devuelve_prioridad_parte(int id)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;

                string sql = "SELECT Prioridad FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Aux_Opciones] WHERE Id = " + id;
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return ds.Tables[0].Rows[0]["Prioridad"].ToString();
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return "";
            }
        }

        public int Devuelve_IDPrioridad(string prioridad)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;

                string sql = "SELECT Id FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Aux_Opciones] WHERE Prioridad = '" + prioridad + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return Convert.ToInt16(ds.Tables[0].Rows[0]["Id"].ToString());
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return 0;
            }
        }


        public string Devuelve_ubicacion(int id)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;

                string sql = "SELECT UbicacionMolde FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Aux_Opciones] WHERE Id = " + id;
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return ds.Tables[0].Rows[0]["UbicacionMolde"].ToString();
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return "En planta";
            }
        }

        public DataSet Devuelve_lista_moldes_numReparaciones(string WHERE)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;

                string sql = "SELECT distinct ReferenciaMolde, MOL.Descripcion, Cavidades, MOL.Ubicacion, UBI.Zona, MOL.FechaModificaUbicacion, CASE WHEN LogotipoSM IS NULL THEN 'http://facts4-srv/thermogestion/SMARTH_docs/MANTENIMIENTO/sin_imagen.jpg' ELSE LogotipoSM END AS Logotipo," +
                                " (SELECT Count(IDMoldes) FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes] WHERE IDMoldes = ReferenciaMolde) as NumReparaciones, CASE WHEN MAN.MANO IS NULL THEN '-' ELSE CAST(MAN.MANO AS varchar) END AS MANO, MAN.UBICACION AS MANUBICACION, MOL.Activo, MOL.FechaUltimaProduccion, UBI.Ubicacion as MOLUBICACION, MAN.AREA " +
                                " FROM[SMARTH_DB].[dbo].[AUX_Lista_Moldes] MOL" +
                                " LEFT JOIN[SMARTH_DB].[dbo].[AUX_Lista_Manos_Robot] MAN ON MOL.Mano = MAN.MANO" +
                                " LEFT JOIN[SMARTH_DB].DBO.AUX_Lista_Moldes_Ubicaciones UBI ON MOL.Ubicacion = UBI.Ubicacion" +
                                " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] PRO ON MOL.ReferenciaMolde = PRO.Molde"+
                                " LEFT JOIN [SMARTH_DB].[dbo].[AUX_Lista_Clientes] LG ON PRO.Cliente = LG.Cliente"+
                                " WHERE ReferenciaMolde > 30000000 " + WHERE + " order by ReferenciaMolde";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        ///
        //OBSOLETOS
        /*
        public DataSet PrevisionCambiosOrdenXMolde2(string HORAINICIO, string HORAFIN)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT MAX(TO_NUMBER(TO_CHAR(j.c_calcenddate,'HH24'))) as hora, j.c_machine_id AS MAQUINA, j.c_id AS ACT_ORDEN, max(n.c_id) AS NEXT_ORDEN, max(j.c_product_id) AS ACT_PRODUCTO, max(j.c_productdescr) AS ACT_PRODDESCRIPT, max(n.c_product_id) AS NEXT_PRODUCTO, max(SUBSTR (n.c_prodlongdescr,0,35)) AS NEXT_PRODDESCRIPT, max(DECODE(j.c_seqnr, 0, PCMS.FncJobBalance(j.c_machine_id), j.c_qtyrequired)) AS ACT_CANTPENDIENTE, j.c_tool_id AS ACT_MOLDE, max(n.c_tool_id) AS NEXT_MOLDE, max(n.c_PlnDueDate) as NEXT_ENTREGAPLANIFICADO, max(n.c_plnstartdate) as NEXT_INICIOPLANIFICADO, max(n.c_recipe_id) AS NEXT_RECETA, j.c_seqnr AS SEQNR, max(j.c_calcenddate) as FINCALCULADO, max((n.c_PlnDueDate - (n.c_MountTime / 1440))) AS FECHACAMBIOMAXIMO, max(round((j.c_calcenddate-SYSDATE),2)) AS TIMETOGO,  max(TRIM(a.C_CHARACTERISTICS)) AS UBICACION, max (n.c_remarks) AS REMARKS" +
                             " FROM (select m.c_id as c_machine_id, nvl(j.c_id, '-') as c_id, j.c_tool_id as c_tool_id, nvl(j.c_seqnr, 0) as c_seqnr, nvl(j.c_CalcStartDate, sysdate) as c_calcstartdate, nvl(j.c_CalcendDate, sysdate) as c_calcenddate, nvl(j.c_qtyrequired, 0) as c_qtyrequired, j.c_qtyrequired AS c_remaining, j.c_product_id as c_product_id, j.c_productdescr as c_productdescr, j.c_colour_id, j.c_remarks from pcms.t_machines m, pcms.t_jobs j  where m.c_id = j.c_machine_id(+) and nvl(j.c_seqnr(+), 0) = 0 and nvl(j.c_parindex(+), 1) = 1" +
                                    " union select j.c_machine_id, j.c_id, j.c_Tool_id,j.c_seqnr,j.c_CalcStartDate,j.c_CalcendDate,j.c_qtyrequired,j.c_qtyrequired,j.c_product_id,j.c_productdescr,j.c_colour_id,j.c_remarks from pcms.t_jobs j where j.c_parindex = 1) j, pcms.T_JOBS n, pcms.T_MACHINES m, pcms.T_TOOLS a" +
                             " WHERE j.C_SEQNR >= 0 AND n.C_MACHINE_ID(+) = j.C_MACHINE_ID AND n.C_SEQNR(+) = j.c_seqnr + 1 AND n.C_PARINDEX = 1 AND j.C_MACHINE_ID = m.C_ID AND(j.C_ID is not null) AND(n.C_ID is not null) AND((C_PLNSTARTDATE <= (SYSDATE + 0.16)) or(c_plnstartdate is null)) AND a.C_ID(+) = n.c_tool_id AND j.C_MACHINE_ID <> 'MONT' AND j.C_MACHINE_ID <> 'HOUS' AND j.C_MACHINE_ID <> 'RTRB' AND j.C_MACHINE_ID <> 'FOAM' and j.c_tool_id<> n.c_tool_id " + HORAINICIO + "" + HORAFIN + "" +
                             " GROUP BY j.C_MACHINE_ID, j.C_TOOL_ID, j.C_SEQNR, j.C_ID" +
                             " ORDER BY TIMETOGO";
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataSet acciones = new DataSet();
                query.Fill(acciones);
                cnn_bms.Close();
                return acciones;
            }
            catch (Exception ex)
            {
                cnn_bms.Close();
                return null;
            }
        }*/


        //***************************************\\IR REVISANDO Y ORGANIZANDO//***************************************\\
        //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\//\\
        public bool actualizar_productosBMS(int producto, int estado)
        {

            try
            {
                string GP12;
                if (estado == 0)// || estado == 7
                { GP12 = "."; }
                else
                { GP12 = "GP12"; }

                cnn_bms.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = cnn_bms;
                cmd.CommandText = "UPDATE PCMS.T_PRODUCTS SET C_CUSTOMSTRING3 = '" + GP12 + "'  WHERE C_ID = '" + producto + "'";
                cmd.ExecuteNonQuery();
                cnn_bms.Close();
                return true;

            }
            catch (Exception)
            {

                cnn_bms.Close();
                return false;
            }
        }

        //**\\BSH//**\\
        public DataSet KPI_Mensual_Uso_Maquina(string fecha)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT EXTRACT(month FROM C_STARTTIME) AS MES, C_CUSTOMER, G.C_SHORT_DESCR, CAST(SUM(C_RUNTIME)/60 AS DECIMAL(10,2)) AS TIEMPO FROM HIS.T_HISJOBS J" +
                             " LEFT JOIN PCMS.T_MACHINES M ON J.C_MACHINE_NR = M.C_ID LEFT JOIN PCMS.T_MCHGROUPS G ON M.C_MCHGRP_ID = G.C_ID WHERE C_STARTTIME > TO_DATE('"+fecha+"', 'DD/MM/YYYY')" +
                             " GROUP BY EXTRACT(month FROM C_STARTTIME), C_CUSTOMER, G.C_SHORT_DESCR, G.C_ID ORDER BY MES, C_CUSTOMER, G.C_ID";
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataSet dsprod = new DataSet();
                query.Fill(dsprod);
                cnn_bms.Close();
                return dsprod;
            }
            catch (Exception)
            {
                cnn_bms.Close();
                return null;
            }
        }

        public DataSet KPI_Mensual_Uso_Operario(string fecha)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT EXTRACT(month FROM C_STARTTIME) AS MES, C_CUSTOMER, CAST(SUM((C_ACTUALAVAILABLETIME / 60) * C_FACTORDIV2) AS DECIMAL(10, 2)) AS TIEMPOOP"+
                             " FROM HIS.T_HISOPERATORS WHERE C_STARTTIME > TO_DATE('"+fecha+"', 'DD/MM/YYYY') AND C_OPERATORTYPE = 1 GROUP BY EXTRACT(month FROM C_STARTTIME), C_CUSTOMER ORDER BY MES, C_CUSTOMER";
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataSet dsprod = new DataSet();
                query.Fill(dsprod);
                cnn_bms.Close();
                return dsprod;
            }
            catch (Exception)
            {
                cnn_bms.Close();
                return null;
            }
        }

        public DataSet KPI_Mensual_Uso_Cambiador(string fecha)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT EXTRACT(month FROM C_STARTTIME) AS MES, C_CUSTOMER, CAST(SUM((C_ACTUALAVAILABLETIME / 60) * C_FACTORDIV2) AS DECIMAL(10, 2)) AS TIEMPOOP" +
                             " FROM HIS.T_HISOPERATORS WHERE C_STARTTIME > TO_DATE('" + fecha + "', 'DD/MM/YYYY') AND C_OPERATORTYPE = 3 GROUP BY EXTRACT(month FROM C_STARTTIME), C_CUSTOMER ORDER BY MES, C_CUSTOMER";
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataSet dsprod = new DataSet();
                query.Fill(dsprod);
                cnn_bms.Close();
                return dsprod;
            }
            catch (Exception)
            {
                cnn_bms.Close();
                return null;
            }
        }

        public DataSet KPI_Mensual_KGTransformados(string fecha)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT EXTRACT(month FROM HS.C_STARTTIME) AS MES, HS.C_CUSTOMER, SUM((RE.C_UNITS * HS.C_PRODUCTIONCOUNT02)) AS KG FROM HIS.T_HISJOBS HS" +
                             " LEFT JOIN PCMS.T_RECIPE_X_MATERIAL RE ON HS.C_RECIPE = RE.C_RECIPE_ID LEFT JOIN PCMS.T_MATERIALS MAT ON RE.C_MATERIAL_ID = MAT.C_ID" +
                             " WHERE C_STARTTIME > TO_DATE('"+fecha+"', 'DD/MM/YYYY') AND(MAT.C_MATTYPE_ID = '15' OR MAT.C_MATTYPE_ID = '150' OR MAT.C_MATTYPE_ID = '215'  OR MAT.C_MATTYPE_ID = '216' OR MAT.C_MATTYPE_ID = '23')" +
                             " GROUP BY EXTRACT(month FROM C_STARTTIME), C_CUSTOMER ORDER BY MES, C_CUSTOMER";
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataSet dsprod = new DataSet();
                query.Fill(dsprod);
                cnn_bms.Close();
                return dsprod;
            }
            catch (Exception)
            {
                cnn_bms.Close();
                return null;
            }
        }

        public DataSet KPI_Mensual_KGTransformadosMOLIDO(string año, string where)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT to_char(C_STARTTIME, 'Month') AS MES, EXTRACT(month FROM HS.C_STARTTIME) AS MESNUM, SUM((RE.C_UNITS * HS.C_PRODUCTIONCOUNT02)) AS KG FROM HIS.T_HISJOBS HS" +
                              " LEFT JOIN PCMS.T_RECIPE_X_MATERIAL RE ON HS.C_RECIPE = RE.C_RECIPE_ID LEFT JOIN PCMS.T_MATERIALS MAT ON RE.C_MATERIAL_ID = MAT.C_ID" +
                              " WHERE EXTRACT(YEAR FROM C_STARTTIME) = "+año+" "+where+" AND" +
                              " (MAT.C_MATTYPE_ID = '15' OR MAT.C_MATTYPE_ID = '150' OR MAT.C_MATTYPE_ID = '215'  OR MAT.C_MATTYPE_ID = '216' OR MAT.C_MATTYPE_ID = '23')" +
                              " GROUP BY to_char(C_STARTTIME, 'Month'), EXTRACT(month FROM C_STARTTIME) ORDER BY MESNUM";
                 OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataSet dsprod = new DataSet();
                query.Fill(dsprod);
                cnn_bms.Close();
                return dsprod;
            }
            catch (Exception)
            {
                cnn_bms.Close();
                return null;
            }
        }

        public DataSet KPI_Mensual_BSH(string periodo, string mes )
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT OPERARIO, NOMBRE, PRODUCIDAS, TO_CHAR(HORAS,'FM9999999.90') AS HORAS, TO_CHAR((PRODUCIDAS/HORAS),'FM9999999.90') AS RATIO, CASE WHEN(PRODUCIDAS / HORAS) < 125 THEN 0 WHEN(PRODUCIDAS / HORAS) > 125 AND(PRODUCIDAS / HORAS) < 131 THEN 53 WHEN(PRODUCIDAS / HORAS) > 131 AND(PRODUCIDAS / HORAS) < 137 THEN 75 ELSE 86 END AS PRIMA" +
                             " FROM(SELECT SUM((C_RUNTIME / 60)) AS HORAS, SUM(C_PRODUCTIONCOUNT01) AS PRODUCIDAS, C_CLOCKNUMBER  AS OPERARIO, C_OPERATORNAME AS NOMBRE FROM HIS.T_HISOPERATORS WHERE to_number(to_CHAR(to_date(C_STARTTIME, 'DD/MM/YYYY'), 'MM'), 99) = " + mes+" AND to_char(to_date(C_STARTTIME, 'DD/MM/RRRR'), 'RRRR') = "+periodo+ " AND C_MACHINE_NR = 'BSH' AND C_OPERATORTYPE = 1  AND C_PRODUCTIONCOUNT01 > 0 GROUP BY C_CLOCKNUMBER, C_OPERATORNAME) ORDER BY OPERARIO";    
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataSet dsprod = new DataSet();
                query.Fill(dsprod);
                cnn_bms.Close();
                return dsprod;
            }
            catch (Exception)
            {
                cnn_bms.Close();
                return null;
            }
        }
        //**\\APROVECHAMIENTO//**\\
        public DataSet AprovechamientoOperario(string FechaInicio, string FechaFinal)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT [Fecha],[OPLOGUEADOS],[OPASIGNADOS],[OPAPROVECHADOS],[OPLIBRES], [CAL],[ENC1],[ENC2] FROM[SMARTH_DB].[dbo].[KPI_Aprovechamiento] where Fecha > CONVERT(DATETIME, '" + FechaInicio+"', 21) AND Fecha<CONVERT(DATETIME,'"+FechaFinal+ "',21) and [OPLOGUEADOS] > 0  order by fecha desc";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataSet AprovechamientoOperarioMedias(string FechaInicio, string FechaFinal)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT AVG([OPLOGUEADOS]) AS OPLOGUEADOS, AVG([OPASIGNADOS]) AS OPASIGNADOS, AVG([OPAPROVECHADOS]) AS OPAPROVECHADOS, AVG([OPLIBRES]) AS OPLIBRES FROM [SMARTH_DB].[dbo].[KPI_Aprovechamiento] where Fecha > CONVERT(DATETIME,'" + FechaInicio + "',21) AND Fecha < CONVERT(DATETIME,'" + FechaFinal + "',21) and [OPLOGUEADOS] > 0";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_GP12.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_GP12.Close();
                return null;
            }
        }
        /*
        public DataSet AprovechamientoOperario4HENCARGADOS()
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT TOP (8) [Fecha],[OPLOGUEADOS],[OPASIGNADOS],[OPAPROVECHADOS],[OPLIBRES], [CAL],[ENC1],[ENC2]  FROM[SMARTH_DB].[dbo].[KPI_Aprovechamiento] WHERE [OPLOGUEADOS] > 0 order by fecha desc";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_GP12.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_GP12.Close();
                return null;
            }
        }*/
        public DataSet AprovechamientoOperario4H()
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT TOP (8) [Fecha],[OPLOGUEADOS],[OPASIGNADOS],[OPAPROVECHADOS],[OPLIBRES],[OPLIBRES], [CAL],[ENC1],[ENC2] FROM[SMARTH_DB].[dbo].[KPI_Aprovechamiento] WHERE [OPLOGUEADOS] > 0 order by fecha desc";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_GP12.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_GP12.Close();
                return null;
            }
        }

        public DataSet AprovechamientoOperarioMedias4H()
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT AVG([OPLOGUEADOS]) AS OPLOGUEADOS, AVG([OPASIGNADOS]) AS OPASIGNADOS, AVG([OPAPROVECHADOS]) AS OPAPROVECHADOS, AVG([OPLIBRES]) AS OPLIBRES FROM (SELECT TOP (8) * FROM [SMARTH_DB].[dbo].[KPI_Aprovechamiento] WHERE [OPLOGUEADOS] > 0 ORDER BY Fecha desc) S";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_GP12.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_GP12.Close();
                return null;
            }
        }

        public DataSet AprovechamientoOperarioSemanas(string Fecha)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT DATEPART( wk, [Fecha]) as SEMANA, MIN([Fecha]) AS INICIO, AVG([OPLOGUEADOS]) AS OPLOGUEADOS, AVG([OPASIGNADOS]) AS OPASIGNADOS, AVG([OPAPROVECHADOS]) AS OPAPROVECHADOS, AVG([OPLIBRES]) AS OPLIBRES FROM[SMARTH_DB].[dbo].[KPI_Aprovechamiento] WHERE convert(varchar, Fecha, 23) like '" + Fecha + "%' and [OPLOGUEADOS] > 0 GROUP BY DATEPART(wk, [Fecha]) order by SEMANA desc";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_GP12.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_GP12.Close();
                return null;
            }
        }

        public DataSet AprovechamientoOperarioMes(string Fecha)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT DATEPART( m, [Fecha]) as MES, MIN([Fecha]) AS INICIO, AVG([OPLOGUEADOS]) AS OPLOGUEADOS, AVG([OPASIGNADOS]) AS OPASIGNADOS, AVG([OPAPROVECHADOS]) AS OPAPROVECHADOS, AVG([OPLIBRES]) AS OPLIBRES FROM [SMARTH_DB].[dbo].[KPI_Aprovechamiento] WHERE convert(varchar, Fecha, 23) like '" + Fecha + "%'  and [OPLOGUEADOS] > 0 GROUP BY DATEPART(m, [Fecha]) order by MES desc";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_GP12.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_GP12.Close();
                return null;
            }
        }

        public DataSet AprovechamientoOperarioMediaAño(string Fecha)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT  AVG([OPLOGUEADOS]) AS OPLOGUEADOS, AVG([OPASIGNADOS]) AS OPASIGNADOS, AVG([OPAPROVECHADOS]) AS OPAPROVECHADOS, AVG([OPLIBRES]) AS OPLIBRES FROM[SMARTH_DB].[dbo].[KPI_Aprovechamiento] WHERE convert(varchar, Fecha, 23) like '" + Fecha + "%'  and [OPLOGUEADOS] > 0";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_GP12.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_GP12.Close();
                return null;
            }
        }

        //**\\LIBERACIONES//**\\
        public DataSet KPILiberacionesWEEK(string año)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                //string sql = "SELECT DATEPART( wk, [Fecha]) as SEMANA, MIN([Fecha]) AS INICIO, AVG([OPLOGUEADOS]) AS OPLOGUEADOS, AVG([OPASIGNADOS]) AS OPASIGNADOS, AVG([OPAPROVECHADOS]) AS OPAPROVECHADOS, AVG([OPLIBRES]) AS OPLIBRES FROM[SMARTH_DB].[dbo].[KPI_Aprovechamiento] WHERE convert(varchar, Fecha, 23) like '" + Fecha + "%' and [OPLOGUEADOS] > 0 GROUP BY DATEPART(wk, [Fecha]) order by SEMANA desc";
                string sql = "SELECT S.YEAR, S.WEEK, S.INICIADAS, C.CONFORMES, NC.CONDICIONADAS"+
                             " FROM(SELECT DATEPART(yyyy,[FechaApertura]) AS YEAR, DATEPART(wk,[FechaApertura]) AS WEEK, COUNT([Orden]) AS INICIADAS FROM[SMARTH_DB].[dbo].[LIBERACION_Ficha] GROUP BY DATEPART(yyyy,[FechaApertura]), DATEPART(wk,[FechaApertura])) S" +
                             " LEFT JOIN(SELECT DATEPART(yyyy,[FechaApertura]) AS YEAR, DATEPART(wk,[FechaApertura]) AS WEEK, COUNT([Orden]) AS CONFORMES FROM[SMARTH_DB].[dbo].[LIBERACION_Ficha] WHERE CalidadLiberado = 2 AND ProduccionLiberado = 2 and CambiadorLiberado = 2 GROUP BY DATEPART(yyyy,[FechaApertura]), DATEPART(wk,[FechaApertura])) C ON S.YEAR = C.YEAR AND S.WEEK = C.WEEK" +
                             " LEFT JOIN(SELECT DATEPART(yyyy,[FechaApertura]) AS YEAR, DATEPART(wk,[FechaApertura]) AS WEEK, COUNT ([Orden]) AS CONDICIONADAS FROM [SMARTH_DB].[dbo].[LIBERACION_Ficha] WHERE CalidadLiberado < 2 or ProduccionLiberado< 2 or CambiadorLiberado< 2 GROUP BY DATEPART(yyyy,[FechaApertura]), DATEPART(wk,[FechaApertura])) NC ON S.YEAR = NC.YEAR AND S.WEEK = NC.WEEK" +
                             " WHERE S.YEAR = '" +año+"'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_GP12.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_GP12.Close();
                return null;
            }
        }

        public DataSet KPILiberacionesCAMBIADORTOTALES(string año)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "select Cambiador, Operario, NUMORDENES, CambiadorLIBERADO, CAST(CAST (CambiadorLIBERADO AS FLOAT)/NUMORDENES AS numeric(36,2)) AS PORCENTAJE" +
                             " from(SELECT Cambiador,count(Cambiador) AS NUMORDENES,SUM(CASE WHEN[CambiadorLiberado] = 0 THEN 0 ELSE 1 END) AS CambiadorLIBERADO FROM[SMARTH_DB].[dbo].[LIBERACION_Ficha] group by Cambiador) E"+
                             " LEFT JOIN[SMARTH_DB].[dbo].[AUX_TablaOperarios] A on E.Cambiador = A.Id where Cambiador<> 0 order by NUMORDENES desc";
                    
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_GP12.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_GP12.Close();
                return null;
            }
        }

        public DataSet KPILiberacionesPRODUCCIONTOTALES(string año)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "select Encargado, Operario, NUMORDENES, ProduccionLIBERADO, CAST(CAST (ProduccionLIBERADO AS FLOAT)/NUMORDENES AS numeric(36,2)) AS PORCENTAJE" +
                             " from(SELECT Encargado, count(Encargado) AS NUMORDENES, SUM(CASE WHEN[ProduccionLiberado] = 0 THEN 0 ELSE 1 END) AS ProduccionLIBERADO FROM[SMARTH_DB].[dbo].[LIBERACION_Ficha] group by Encargado) E" +
                             " LEFT JOIN[SMARTH_DB].[dbo].[AUX_TablaOperarios] A on E.Encargado = A.Id where Encargado<> 0 order by NUMORDENES desc";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_GP12.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_GP12.Close();
                return null;
            }
        }

        public DataSet KPILiberacionesCALIDADTOTALES(string año)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "select Calidad, Operario, NUMORDENES, CALIDADLIBERADO, CAST(CAST (CALIDADLIBERADO AS FLOAT)/NUMORDENES AS numeric(36,2)) AS PORCENTAJE" +
                             " from(SELECT Calidad, count(Calidad) AS NUMORDENES, SUM(CASE WHEN CalidadLiberado = 0 THEN 0 ELSE 1 END) AS CALIDADLIBERADO FROM[SMARTH_DB].[dbo].[LIBERACION_Ficha] group by Calidad) E" +
                             " LEFT JOIN[SMARTH_DB].[dbo].[AUX_TablaOperarios] A on E.Calidad = A.Id where Calidad <> 0 order by NUMORDENES desc";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_GP12.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_GP12.Close();
                return null;
            }
        }

        public DataSet Devuelve_Panel_Liberaciones(string where, string orderby)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT P.[Maquina],P.[Orden],[OrdenMaestra],[Linea],P.[Molde],P.[Referencia],R.Descripcion,R.Cliente,CASE WHEN CLI.LogotipoSM IS NULL THEN 'http://facts4-srv/thermogestion/SMARTH_docs/MANTENIMIENTO/sin_imagen.jpg' ELSE CLI.LogotipoSM END AS LogotipoSM, [HoraCarga],[EstadoMaquina],[AccionLiberado]," +
                             "CASE WHEN L.CambiadorLiberado IS NULL THEN 'Pendiente' WHEN L.CambiadorLiberado = 0 THEN 'Pendiente' WHEN L.CambiadorLiberado = 1 THEN 'OK Condicionada' WHEN L.CambiadorLiberado = 2 THEN 'Liberada OK' END AS LIBERARCAMBIO," +
                             "CASE WHEN L.ProduccionLiberado IS NULL THEN 'Pendiente' WHEN L.ProduccionLiberado = 0 THEN 'Pendiente' WHEN L.ProduccionLiberado = 1 THEN 'OK Condicionada' WHEN L.ProduccionLiberado = 2 THEN 'Liberada OK' END AS LIBERARPRODUCCION," +
                             "CASE WHEN L.CalidadLiberado IS NULL THEN 'Pendiente' WHEN L.CalidadLiberado = 0 THEN 'Pendiente' WHEN L.CalidadLiberado = 1 THEN 'OK Condicionada' WHEN L.CalidadLiberado = 2 THEN 'Liberada OK' END AS LIBERARCALIDAD, l.NotasLiberado, m.Ubicación," +
                             "CASE WHEN L.FechaCambiadorLiberado = '' THEN ORIFechaCambiadorLiberado ELSE FechaCambiadorLiberado END AS FECHACAMBIADORLIBERADO," +
                             "CASE WHEN L.FechaProduccionLiberado = '' THEN ORIFechaProduccionLiberado ELSE FechaProduccionLiberado END AS FECHAPRODUCCIONLIBERADO," +
                             "CASE WHEN L.FechaCalidadLiberado = '' THEN ORIFechaCalidadLiberado ELSE FechaCalidadLiberado END AS FECHACALIDADLIBERADO," +
                             " PRI.PRIORIDADDEC AS PRIORIDAD,CASE WHEN PRI.PRIORIDADDEC = '100' THEN 'N.P.' ELSE CAST(PRI.PRIORIDADDEC AS varchar) END AS PRIORIDADTEXT " +
                             " FROM [SMARTH_DB].[dbo].[AUX_Planificacion] P" +
                             " left join [SMARTH_DB].[dbo].[LIBERACION_Ficha] L ON P.Orden = L.Orden " +
                             " left join [SMARTH_DB].[dbo].[AUX_TablaProductos] R ON P.Referencia = R.Referencia" +
                             " left join [SMARTH_DB].[dbo].[AUX_Lista_Máquinas] M on P.Maquina = m.IdMaquinaCHAR" +
                             " left join [SMARTH_DB].[dbo].[AUX_Lista_Clientes] CLI on R.Cliente = CLI.Cliente" +
                             " left join (SELECT [Orden], MAX([PRIORIDADDEC]) AS PRIORIDADDEC FROM [SMARTH_DB].[dbo].[AUX_Planificacion_Prioridades] GROUP BY Orden) PRI on P.Orden = PRI.Orden" +
                             " WHERE P.Linea = 1 AND P.SEQNR = 0 " + where + " " + orderby + "";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataSet Devuelve_OrdenesProduciendoDESVIADAS()
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT BMS.EstadoMaquina, LIB.[Orden],LIB.[Referencia],LIB.[Maquina],[Operario1Nivel],[Operario1Notas],OP1.Operario AS Encargado,OP2.Operario AS Cambiador,OP3.Operario AS Calidad,[CambiadorLiberado],[ProduccionLiberado],[CalidadLiberado],[ResultadoLiberado],[AccionLiberado],[NotasLiberado],[ENCNoconformidad],[ENCDefectos],[CALNoconformidad],[CALDefectos],[FechaApertura]" +
                             ",[Q1E],[Q1C],[Q1ENC],[Q1CAL],[Q2E],[Q2C],[Q2ENC],[Q2CAL],[Q3E],[Q3C],[Q3ENC],[Q3CAL],[Q4E],[Q4C],[Q4ENC],[Q4CAL],[Q5E],[Q5C],[Q5ENC],[Q5CAL],[Q6E],[Q6C],[Q6ENC],[Q6CAL],[Q7E],[Q7C],[Q7ENC],[Q7CAL],[Q8E],[Q8C],[Q8ENC],[Q8CAL],[Q9E],[Q9C],[Q9ENC],[Q9CAL],[Q10E],[Q10C],[Q10ENC],[Q10CAL],[Q11E],[Q11C],[Q11ENC],[Q11CAL],[QXFeedbackCambiador],[QXFeedbackProduccion],[QXFeedbackCalidad]" +
                             ",ResultadoLOTES, CASE WHEN ResultadoLOTES = 1 THEN 'Lotes sin declarar' ELSE '' END AS ResultadosLOTESTEXT,ResultadoPARAM, CASE WHEN ResultadoPARAM = 1 THEN 'Desviación en parámetros' ELSE '' END AS ResultadoPARAMTEXT" +
                             ",CASE WHEN ENCNoconformidad = 0 and ProduccionLiberado<> 0 THEN 'NC sin consultar' ELSE '' END AS ENCNoconformidadTEXT, CASE WHEN ENCDefectos = 0 and ProduccionLiberado<> 0 THEN 'GP12 sin consultar' ELSE '' END AS ENCDefectosTEXT" +
                             ",CASE WHEN CALNoconformidad = 0 and CalidadLiberado<> 0 THEN 'NC sin consultar' ELSE '' END AS CALNoconformidadTEXT, CASE WHEN CALDefectos = 0  and CalidadLiberado<> 0 THEN 'GP12 sin consultar' ELSE '' END AS CALDefectosTEXT" +
                             " FROM [SMARTH_DB].[dbo].[AUX_Planificacion] BMS" +
                             " LEFT JOIN[SMARTH_DB].[dbo].[LIBERACION_Ficha] LIB ON BMS.Orden = LIB.Orden " +
                             " LEFT JOIN [SMARTH_DB].[dbo].[LIBERACION_Auditoria] AUD ON LIB.Orden = AUD.Orden" +
                             " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] OP1 ON LIB.Encargado = OP1.Id" +
                             " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] OP2 ON LIB.Cambiador = OP2.Id" +
                             " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] OP3 ON LIB.Calidad = OP3.Id" +
                             " WHERE (CambiadorLiberado = 1 OR ProduccionLiberado = 1 OR CalidadLiberado = 1) AND BMS.SEQNR = 0 AND BMS.Maquina <> 'FOAM' AND BMS.Linea = 1 ORDER BY BMS.Maquina ASC";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_GP12.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_GP12.Close();
                return null;
            }
        }

        //**\\CONECTORES ANTIGUOS //**\\
        //CONECTORES GP12.ASPX

        public DataSet devuelve_lista_ordenes(string producto)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT NULL AS ORDEN, NULL AS PRODUCTO FROM PCMS.T_PRODUCTS UNION ALL SELECT C_ID AS ORDEN, C_PRODUCT_ID AS PRODUCTO FROM PCMS.T_JOBS WHERE C_PRODUCT_ID = '" + producto + "' UNION SELECT C_JOB AS ORDEN, C_PRODUCT AS PRODUCTO FROM HIS.T_HISJOBS WHERE C_PRODUCT = '" + producto + "' ORDER BY ORDEN DESC";
                //string sql = "SELECT NULL AS ORDEN, NULL AS PRODUCTO FROM PCMS.T_PRODUCTS UNION ALL SELECT C_ID AS ORDEN, C_PRODUCT_ID AS PRODUCTO FROM PCMS.T_JOBS WHERE C_PRODUCT_ID = '" + producto + "' AND C_SEQNR < '1' UNION SELECT C_JOB AS ORDEN, C_PRODUCT AS PRODUCTO FROM HIS.T_HISJOBS WHERE C_PRODUCT = '" + producto + "' ORDER BY ORDEN DESC";
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataSet dsordenes = new DataSet();
                query.Fill(dsordenes, sql);
                cnn_bms.Close();
                return dsordenes;
            }
            catch (Exception)
            {
                cnn_bms.Close();
                return null;
            }
        }

        public DataSet devuelve_lista_razonesrevision()
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT Id, Razon FROM [SMARTH_DB].[dbo].[AUX_GP12_RazonRevision] where Razon <> '' order by Id";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_GP12.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_GP12.Close();
                return null;
            }
        }

        public DataTable devuelve_lista_responsables()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT [Id],[Nombre] AS PAprobado FROM [SMARTH_DB].[dbo].[AUX_Personal_Mandos] WHERE OPActivo = 1 ORDER BY PAprobado ASC", cnn_SMARTH);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                cnn_SMARTH.Close();
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                return dt;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public string devuelve_razonrevision(string razon)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT TipoAgua FROM [SMARTH_DB].[dbo].[PARAMETROS_AuxAtemperado] where IdAtemp = " + razon;
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_GP12.Close();
                if (ds.Tables[0].Rows[0]["TipoAgua"].ToString() != "")
                {
                    return ds.Tables[0].Rows[0]["TipoAgua"].ToString();
                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {
                cnn_GP12.Close();
                return "";
            }
        }

        public int devuelve_IDlista_razonesrevision(string razon)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT Id FROM [SMARTH_DB].[dbo].[AUX_GP12_RazonRevision] where Razon = '" + razon + "'"; 
                    
                    
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_GP12.Close();
                return Convert.ToInt16(ds.Tables[0].Rows[0]["Id"].ToString());
            }
            catch (Exception)
            {
                cnn_GP12.Close();
                return 1;
            }
        }

        public int Devuelve_IDproveedorrevision(string proveedor)
        {
            try
            {

                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT Id FROM [SMARTH_DB].[dbo].[GP12_ProveedoresRevision] where Proveedor = '" + proveedor + "'";


                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_GP12.Close();
                return Convert.ToInt16(ds.Tables[0].Rows[0]["Id"].ToString());
            }
            catch (Exception)
            {
                cnn_GP12.Close();
                return 0;
            }
        }

       
    
        //CONECTORES PREVISION

        public bool insertar_previsiones(int articulo, DateTime entrega, int cantidad)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql2 = "INSERT INTO [CALIDAD].[dbo].[PrevisionGP12] (Referencia, FechaEntrega, cantidad) VALUES " +
                                 "(" + articulo + ",convert(datetime,'" + entrega + "')," + cantidad + ")";
                cmd.CommandText = sql2;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();
                return true;
            }
            catch (Exception)
            {
                cnn_GP12.Close();
                return false;
            }
        }

        public DataSet lee_grid_prevision()
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT P.[Referencia],R.Descripcion,[FechaEntrega],[cantidad],R.Cliente, N.Razon FROM [CALIDAD].[dbo].[PrevisionGP12] P LEFT JOIN [SMARTH_DB].[dbo].[GP12_ProductosEstados] Q ON P.[Referencia] = Q.Referencia LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] R ON P.Referencia = R.Referencia LEFT JOIN [SMARTH_DB].[dbo].[AUX_GP12_RazonRevision] N ON EstadoActual = N.Id WHERE Q.EstadoActual > 0 and FechaEntrega >= DATEADD(DAY,-1,SYSDATETIME()) Group By P.[Referencia],R.Descripcion, [FechaEntrega], [cantidad], [Cliente], N.Razon ORDER BY FechaEntrega ASC";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_GP12.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_GP12.Close();
                return null;
            }
        }

        public DataSet lee_grid_prevision_filtrado(string fecha)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT P.[Referencia],R.Descripcion,[FechaEntrega],[cantidad],R.Cliente, N.Razon FROM [CALIDAD].[dbo].[PrevisionGP12] P LEFT JOIN [SMARTH_DB].[dbo].[GP12_ProductosEstados] Q ON P.[Referencia] = Q.Referencia LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] R ON P.Referencia = R.Referencia LEFT JOIN [SMARTH_DB].[dbo].[AUX_GP12_RazonRevision] N ON EstadoActual = N.Id WHERE Q.EstadoActual > 0 AND FechaEntrega like '" + fecha + "%' Group By P.[Referencia],R.Descripcion, [FechaEntrega], [cantidad], [Cliente], N.Razon ORDER BY FechaEntrega ASC";    
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_GP12.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_GP12.Close();
                return null;
            }
        }

        public DataSet lee_grid_prevision_atrasadas()
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT P.[Referencia],R.Descripcion,[FechaEntrega],[cantidad],R.Cliente, N.Razon FROM [CALIDAD].[dbo].[PrevisionGP12] P LEFT JOIN [SMARTH_DB].[dbo].[GP12_ProductosEstados] Q ON P.[Referencia] = Q.Referencia LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] R ON P.Referencia = R.Referencia LEFT JOIN [SMARTH_DB].[dbo].[AUX_GP12_RazonRevision] N ON EstadoActual = N.Id WHERE Q.EstadoActual > 0 and FechaEntrega >= DATEADD(DAY,-15,SYSDATETIME()) and FechaEntrega <= DATEADD(DAY,-1,SYSDATETIME()) Group By P.[Referencia],R.Descripcion, [FechaEntrega], [cantidad], [Cliente], N.Razon ORDER BY FechaEntrega ASC"; 
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_GP12.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_GP12.Close();
                return null;
            }
        }

        public DataSet leer_correos()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql1 = "SELECT DISTINCT Correo FROM [SMARTH_DB].[dbo].[AUX_Personal_Mandos] WHERE Mail_GP12 IS NOT NULL";
                SqlDataAdapter adapter = new SqlDataAdapter(sql1, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql1);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                //mandar_mail(ex.Message);
                return null;
            }
        }

       
        


        //bloque antiguo calidad //rehacer
        public void InsertarNoConformidad(int IdNoConformidad, string FechaOriginal, string FechaD3, string FechaD6, string FechaD8, int CheckCorte, int CheckD3, int CheckD6, int CheckD8)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[NC_Estado_No_Conformidad] ([IdNoConformidad], [FechaOriginal],[D3],[D6],[D8],[CheckCorte],[CheckD3],[CheckD6],[CheckD8])" +
                                                                   " VALUES (" + IdNoConformidad + ",'" + FechaOriginal + "','" + FechaD3 + "','" + FechaD6 + "','" + FechaD8 + "'," + CheckCorte + "," + CheckD3 + "," + CheckD6 + "," + CheckD8 + " )";

                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();

            }
            catch (Exception)
            {
                cnn_GP12.Close();
            }
        }

        public void GuardaFormacionOperario(int IdNoConformidad, int NumOperario, int Formador, string fecha, string firma)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "UPDATE [SMARTH_DB].[dbo].[NC_OperariosVinculados] SET [NumFormador] = " + Formador + ", [FechaOriginal] = '" + fecha + "', [Firma] = '" + firma + "' WHERE [IdNoConformidad] = " + IdNoConformidad + " and [NumOperario] = " + NumOperario + "";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();

            }
            catch (Exception)
            {
                cnn_GP12.Close();
            }
        }

        public bool existe_alerta(string NC)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT IdNoConformidad FROM[SMARTH_DB].[dbo].[NC_Alerta_de_calidad] WHERE IdNoConformidad = '" + NC + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_GP12.Close();
                if (Convert.ToInt32(ds.Tables[0].Rows[0]["IdNoConformidad"]) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                cnn_GP12.Close();
                return false;
            }
        }

        public int Devuelve_Ultima_AlertaCalidad()
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT MAX([IdNoConformidad]) AS ID FROM [SMARTH_DB].[dbo].[NC_Alerta_de_calidad]";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_GP12.Close();
                return Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString());
            }
            catch (Exception)
            {
                cnn_GP12.Close();
                return 0;
            }
        }

        public DataTable Devuelve_lista_almacenes_NAV()
        {
            try
            {             
                cnn_NAV.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT [Code],[Name] FROM [NAVDB].[dbo].[THERMO$Location]", cnn_NAV);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                cnn_NAV.Close();
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                return dt;
            }
            catch (Exception)
            {

                cnn_NAV.Close();
                return null;
            }
        }
        public DataSet Devuelve_stock_cuarentena(string referencia)
        {
            try
            {
                cnn_NAV.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_NAV;
                //OLD string sql = "SELECT [Item No_], sum([Item Ledger Entry Quantity]) AS CANTALM FROM[NAVDB].[dbo].[THERMO$Value Entry] WHERE[Item No_] = '" + referencia + "' group by[Item No_]";
                string sql = "SELECT [Location Code], ALM.Name, SUM(Quantity) AS CANTALM FROM NAVDB.DBO.[THERMO$Item Ledger Entry] STC" +
                                " LEFT JOIN[NAVDB].[dbo].[THERMO$Location] ALM ON STC.[Location Code] = ALM.CODE" +
                                " where[Item No_] = '" + referencia + "' and[Posting Date] <= SYSDATETIME()" +
                                " GROUP BY[Item No_], [Location Code], ALM.Name" +
                                " HAVING SUM(Quantity) > 0 order by CANTALM desc";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_NAV);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_NAV.Close();
                return ds;
            }
            catch (Exception)
            {

                cnn_NAV.Close();
                return null;
            }
        }

        public bool Devuelve_LEE_stock_cuarentena(int NoConformidad, int referencia, int responsable, string FechaNC)
        {

            try
            {
                string sql = "SELECT [Item No_] as REFERENCIA, [Location Code]  as CODIGO, ALM.Name as ALMACEN, SUM(Quantity) AS CANTALM FROM NAVDB.DBO.[THERMO$Item Ledger Entry] STC" +
                                " LEFT JOIN[NAVDB].[dbo].[THERMO$Location] ALM ON STC.[Location Code] = ALM.CODE" +
                                " where[Item No_] = '" + referencia + "' and[Posting Date] <= SYSDATETIME()" +
                                " GROUP BY[Item No_], [Location Code], ALM.Name" +
                                " HAVING SUM(Quantity) > 0 order by CANTALM desc";
                SqlCommand cmd = new SqlCommand(sql, cnn_NAV);
                cnn_NAV.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Insertar_Cuarentena_NC(Convert.ToInt32(reader["REFERENCIA"]), reader["CODIGO"].ToString(), reader["ALMACEN"].ToString(), Convert.ToInt32(reader["CANTALM"]), NoConformidad, responsable, FechaNC);

                    }
                }

                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_NAV);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    Insertar_Cuarentena_NC(referencia, "1", "Thermolympic", 0, NoConformidad, responsable, FechaNC);

                }

                cnn_NAV.Close();
                return true;
            }
            catch (Exception ex)
            {
                cnn_NAV.Close();
                return false;
            }
        }

        public bool Insertar_Cuarentena_NC(int referencia, string Codigo, string Almacen, int Cantidad, int NoConformidad, int responsable, string FechaNC) //SMARTH
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[NC_HojaContencion] ([Referencia],[IdNoConformidad],[Cantidad],[IdAlmacen],[NombreAlmacen],[FechaNC],[IdResponsable],[Notas],[PuntoLimpio]) VALUES " +
                                                                       "(" + referencia + "," + NoConformidad + "," + Cantidad + ",'" + Codigo + "','" + Almacen + "','" + FechaNC + "'," + responsable + ",'',0)";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();
                return true;
            }
            catch (Exception ex)
            {
                cnn_GP12.Close();
                return false;
            }
        }


        public DataTable devuelve_setlista_informadores()
        {
            try
            {
                cnn_GP12.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT [Id],[Nombre] AS PInformadores FROM [SMARTH_DB].[dbo].[AUX_Personal_Mandos] WHERE OPActivo = 1 ORDER BY PInformadores ASC", cnn_GP12);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                cnn_GP12.Close();
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                return dt;
            }
            catch (Exception)
            {
                cnn_GP12.Close();
                return null;
            }
        }

        public string devuelve_Pilotos_NoConformidad_SMARTH(int id)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT [Id],[Nombre] FROM[SMARTH_DB].[dbo].[AUX_Personal_Mandos] WHERE Id = " + id;
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_GP12.Close();
                if (ds.Tables[0].Rows[0]["Nombre"].ToString() != "")
                {
                    return ds.Tables[0].Rows[0]["Nombre"].ToString();
                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {
                cnn_GP12.Close();
                return "";
            }
        } //SMARTH

        public DataSet Devuelve_operarios_vinculados_NC_SMARTH(int NoConformidad)
        {

            try
            {
                //Lee resultados y devuelve el nuevo DataSet
                cnn_GP12.Open();
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = cnn_GP12;
                string sql2 = "SELECT [NumOperario] as OPNUMERO, OP.[Operario] AS OPNOMBRE, cal.Nombre AS FORMNOMBRE,[FechaOriginal] AS FECHA,CASE WHEN [Firma] IS NULL THEN 'http://facts4-srv/thermogestion/imagenes/null.png' ELSE [Firma] END AS FIRMA FROM [SMARTH_DB].[dbo].[NC_OperariosVinculados] NC" +
                              " left join[SMARTH_DB].[dbo].[AUX_Personal_Mandos] CAL on nc.NumFormador = cal.Id LEFT JOIN[SMARTH_DB].[dbo].[AUX_TablaOperarios] OP on nc.NumOperario = op.Id WHERE NC.IdNoConformidad = " + NoConformidad + "  AND OP.OpActivo <> 0";
                SqlDataAdapter adapter = new SqlDataAdapter(sql2, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql2);
                cnn_GP12.Close();
                return ds;

            }
            catch (Exception)
            {
                cnn_GP12.Close();
                return null;
            }
        } //SMARTH

        public DataSet Devuelve_moldesXreferencia(string molde)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT [Referencia],[Descripcion] FROM [SMARTH_DB].[dbo].[AUX_TablaProductos] where Molde = " + molde + " order by Referencia";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_GP12.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_GP12.Close();
                return null;
            }
        }

        public string Devuelve_referenciasXmolde(string referencia)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT Molde FROM [SMARTH_DB].[dbo].[AUX_TablaProductos] where [Referencia]  = " + referencia + " order by Referencia";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                if (ds.Tables[0].Rows[0]["Molde"].ToString() != "")
                {
                    return ds.Tables[0].Rows[0]["Molde"].ToString();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return "";
            }
        }

        public int devuelve_IDlista_Formadores_SMARTH(string responsable) //SMARTH
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT Id FROM [SMARTH_DB].[dbo].[AUX_Personal_Mandos] where Nombre = '" + responsable + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_GP12.Close();
                return Convert.ToInt16(ds.Tables[0].Rows[0]["Id"].ToString());
            }
            catch (Exception)
            {
                cnn_GP12.Close();
                return 1;
            }
        }

        public DataSet Devuelve_datos_referencia(string referencia)
        {
            try
            {
                cnn_NAV.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_NAV;
                string sql = "SELECT I.[No_] AS Referencia, I.[Description] AS Descripcion, I.[Vendor No_] as Proveedor, V.Name AS Nomproveedor FROM[NAVDB].[dbo].[THERMO$Item] I LEFT JOIN[NAVDB].[dbo].[THERMO$Vendor] V ON I.[Vendor No_] = V.No_ WHERE i.[No_] = '" + referencia + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_NAV);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_NAV.Close();
                return ds;
            }
            catch (Exception)
            {

                cnn_NAV.Close();
                return null;
            }
        }

        public DataSet Devuelve_datos_producto_BMS(string referencia)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                //string sql = "SELECT [Referencia],[Descripcion] FROM [SMARTH_DB].[dbo].[AUX_TablaProductos] where Molde = " + molde + " order by Referencia";
                string sql = "SELECT P.[Referencia],P.[Molde],[Descripcion],[Cliente], R.ImagenPieza FROM[SMARTH_DB].[dbo].[AUX_TablaProductos] P LEFT JOIN [SMARTH_DB].[dbo].[DOCUMENTAL_Base_DocumentosXReferencias] R ON P.Referencia = R.Referencia WHERE P.Referencia = '" + referencia + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_GP12.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_GP12.Close();
                return null;
            }
        }

        public DataSet Devuelve_datos_NOCONFORMIDAD_MOLDE(string MOLDE)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT *,CASE WHEN RepiteDefecto = 1 THEN '1' ELSE '0' END AS RepiteDefecto10, CASE WHEN RepiteReferencia = 1 THEN '1' ELSE '0' END AS RepiteReferencia10 FROM[SMARTH_DB].[dbo].[NC_Alerta_de_calidad] NC LEFT JOIN[SMARTH_DB].[dbo].[AUX_TablaProductos] P ON NC.Referencia = P.Referencia LEFT JOIN[SMARTH_DB].[dbo].[DOCUMENTAL_Base_DocumentosXReferencias] R ON NC.Referencia = R.Referencia WHERE P.Molde = '" + MOLDE + "' ORDER BY NC.ID DESC";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_GP12.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_GP12.Close();
                return null;
            }
        }

        public DataSet Devuelve_Cajas_AfectadasXMolde(string molde)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT RTRIM(P.C_SHORT_DESCR) AS C_SHORT_DESCR, H.C_PRODUCTTOOL FROM PCMS.T_JOB_PIECE P LEFT JOIN HIS.T_HISJOBS H ON P.C_JOB_ID = H.C_JOB WHERE C_PRODUCTTOOL = '" + molde + "'";
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataSet dsope = new DataSet();
                query.Fill(dsope);
                cnn_bms.Close();
                return dsope;
            }
            catch (Exception)
            {
                cnn_bms.Close();
                return null;
            }
        }

        public DataSet Devuelve_Afectados_MaterialesXreferencia(string referencia)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT P.C_ID AS Referencia, P.C_LONG_DESCR AS Descripcion FROM PCMS.T_RECIPE_X_MATERIAL M LEFT JOIN PCMS.T_PRODUCTS P ON M.C_RECIPE_ID = P.C_ID WHERE M.C_MATERIAL_ID = '" + referencia + "'";
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataSet dsope = new DataSet();
                query.Fill(dsope);
                cnn_bms.Close();
                return dsope;
            }
            catch (Exception)
            {
                cnn_bms.Close();
                return null;
            }
        }

        public DataSet DevuelveLEE_operarios_vinculados_NCBMS_SMARTH(string molde, int NoConformidad) //SMARTH
        {

            try
            {
                string sql = "SELECT DISTINCT C_CLOCKNUMBER, C_OPERATORNAME FROM HIS.T_HISJOBS WHERE C_PRODUCTTOOL = '" + molde + "' AND C_OPERATORNAME IS NOT NULL ORDER BY C_CLOCKNUMBER";
                OracleCommand cmd = new OracleCommand(sql, cnn_bms);
                cnn_bms.Open();

                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        insertar_operariosNC_SMARTH(Convert.ToInt32(reader["C_CLOCKNUMBER"]), NoConformidad);

                    }
                }
                cnn_bms.Close();
                //Lee resultados y devuelve el nuevo DataSet
                cnn_GP12.Open();
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = cnn_GP12;
                string sql2 = "SELECT [NumOperario] as OPNUMERO, OP.[Operario] AS OPNOMBRE, cal.Nombre AS FORMNOMBRE,[FechaOriginal] AS FECHA,[Firma] AS FIRMA FROM [SMARTH_DB].[dbo].[NC_OperariosVinculados] NC" +
                              " left join[SMARTH_DB].[dbo].[AUX_Personal_Mandos] CAL on nc.NumFormador = cal.Id LEFT JOIN[SMARTH_DB].[dbo].[AUX_TablaOperarios] OP on nc.NumOperario = op.Id WHERE NC.IdNoConformidad = " + NoConformidad + " AND OP.OpActivo <> 0";
                SqlDataAdapter adapter = new SqlDataAdapter(sql2, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql2);
                cnn_GP12.Close();
                return ds;

            }
            catch (Exception)
            {
                cnn_GP12.Close();
                cnn_bms.Close();
                return null;
            }
        }

        public bool insertar_operariosNC_SMARTH(int numoperario, int NoConformidad) //SMARTH
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[NC_OperariosVinculados] (IdNoConformidad, NumOperario, NumFormador) VALUES " +
                                 "(" + NoConformidad + "," + numoperario + ",2)";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();
                return true;
            }
            catch (Exception)
            {
                cnn_GP12.Close();
                return false;
            }
        }

        public DataSet Cargar_operarios_produccion(string molde)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT DISTINCT C_CLOCKNUMBER as OPNUMERO, C_OPERATORNAME AS OPNOMBRE, '' AS FORMNOMBRE, '' AS FECHA, '' AS FIRMA FROM HIS.T_HISJOBS WHERE C_PRODUCTTOOL = '" + molde + "' AND C_OPERATORNAME IS NOT NULL ORDER BY C_CLOCKNUMBER";
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataSet dsope = new DataSet();
                query.Fill(dsope);
                cnn_bms.Close();
                return dsope;
            }
            catch (Exception)
            {
                cnn_bms.Close();
                return null;
            }
        }
        //***************************************\\ALMACENES//***************************************\\
        /////////VINCULADOS A SERVICIOS NAV
        ///
        public DataTable Devuelve_StockAlmacen_SERVNAV(string almacen, string vacios, string entorno)
        {
            try
            {
                string response = "";
                
                if (entorno == "") //PRODUCCION
                {
                    var SNAV = new Serv_NAVISION.WSFunctionsAPP_PortClient();
                    SNAV.ClientCredentials.Windows.ClientCredential = new System.Net.NetworkCredential("Administrador", "eXtint0r");
                    response = SNAV.ReadStockLocation(almacen, vacios);
                    
                }
                else //ENTORNOPRUEBA
                {
                    var SNAVDEBUG = new Serv_NAVISION_DEBUG.WSFunctionsAPP_PortClient(); 
                    SNAVDEBUG.ClientCredentials.Windows.ClientCredential = new System.Net.NetworkCredential("Administrador", "eXtint0r");
                    response = SNAVDEBUG.ReadStockLocation(almacen, vacios);
                }
                
                DataTable StockAlmacen = new DataTable();
                StockAlmacen.Columns.Add("producto", typeof(string));
                StockAlmacen.Columns.Add("descripcion", typeof(string));
                StockAlmacen.Columns.Add("almacen", typeof(string));
                StockAlmacen.Columns.Add("stock", typeof(string));

                JObject jsonObject = JObject.Parse(response);
                JArray jsonArray = (JArray)jsonObject["arrayStock"];

                foreach (var item in jsonArray)
                {
                    DataRow newRow = StockAlmacen.NewRow();
                    newRow["producto"] = item["producto"];
                    newRow["descripcion"] = item["descripcion"];
                    newRow["almacen"] = item["almacen"];
                    newRow["stock"] = item["stock"].ToString().Replace(",", "").Replace(".", ",");
                    StockAlmacen.Rows.Add(newRow);
                }
                return StockAlmacen;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public DataTable Devuelve_StockAlmacen_SERVNAV_PATCHSTRING(string almacen, string vacios, string entorno)
        {
            try
            {
                string response = "";

                if (entorno == "") //PRODUCCION
                {
                    var SNAV = new Serv_NAVISION.WSFunctionsAPP_PortClient();
                    SNAV.ClientCredentials.Windows.ClientCredential = new System.Net.NetworkCredential("Administrador", "eXtint0r");
                    response = SNAV.ReadStockLocation(almacen, vacios);

                }
                else //ENTORNOPRUEBA
                {
                    var SNAVDEBUG = new Serv_NAVISION_DEBUG.WSFunctionsAPP_PortClient();
                    SNAVDEBUG.ClientCredentials.Windows.ClientCredential = new System.Net.NetworkCredential("Administrador", "eXtint0r");
                    response = SNAVDEBUG.ReadStockLocation(almacen, vacios);
                }

                DataTable StockAlmacen = new DataTable();
                StockAlmacen.Columns.Add("producto", typeof(string));
                StockAlmacen.Columns.Add("descripcion", typeof(string));
                StockAlmacen.Columns.Add("almacen", typeof(string));
                StockAlmacen.Columns.Add("stock", typeof(string));

                JObject jsonObject = JObject.Parse(response);
                JArray jsonArray = (JArray)jsonObject["arrayStock"];

                foreach (var item in jsonArray)
                {
                    DataRow newRow = StockAlmacen.NewRow();
                    newRow["producto"] = "P" + item["producto"];
                    newRow["descripcion"] = item["descripcion"];
                    newRow["almacen"] = item["almacen"];
                    newRow["stock"] = item["stock"];
                    StockAlmacen.Rows.Add(newRow);
                }
                return StockAlmacen;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public DataTable Devuelve_Stock_ProductoXAlmacen_SERVNAV(string producto, string almacen, string entorno)
        {
            try
            {
                string response = "";

                if (entorno == "") //PRODUCCION
                {
                    var SNAV = new Serv_NAVISION.WSFunctionsAPP_PortClient();
                    SNAV.ClientCredentials.Windows.ClientCredential = new System.Net.NetworkCredential("Administrador", "eXtint0r");
                    response = SNAV.ReadStock(producto, almacen);

                }
                else //ENTORNOPRUEBA
                {
                    var SNAVDEBUG = new Serv_NAVISION_DEBUG.WSFunctionsAPP_PortClient();
                    SNAVDEBUG.ClientCredentials.Windows.ClientCredential = new System.Net.NetworkCredential("Administrador", "eXtint0r");
                    response = SNAVDEBUG.ReadStock(producto, almacen);
                }

                DataTable StockProducto = new DataTable();
                StockProducto.Columns.Add("producto", typeof(string));
                StockProducto.Columns.Add("descripcion", typeof(string));
                StockProducto.Columns.Add("almacen", typeof(string));
                StockProducto.Columns.Add("stock", typeof(string));

                JObject jsonObject = JObject.Parse(response);
                JArray jsonArray = (JArray)jsonObject["arrayStock"];

                foreach (var item in jsonArray)
                {
                    DataRow newRow = StockProducto.NewRow();
                    newRow["producto"] = item["producto"];
                    newRow["descripcion"] = item["descripcion"];
                    newRow["almacen"] = item["almacen"];
                    newRow["stock"] = item["stock"].ToString().Replace(",", "").Replace(".", ",");
                    StockProducto.Rows.Add(newRow);
                }
                return StockProducto;
            }
            catch (Exception ex)
            {
                DataTable StockProducto = new DataTable();
                StockProducto.Columns.Add("producto", typeof(string));
                StockProducto.Columns.Add("descripcion", typeof(string));
                StockProducto.Columns.Add("almacen", typeof(string));
                StockProducto.Columns.Add("stock", typeof(string));
                DataRow newRow = StockProducto.NewRow();
                newRow["producto"] = "";
                newRow["descripcion"] = "";
                newRow["almacen"] = "0";
                newRow["stock"] = "0";
                StockProducto.Rows.Add(newRow);
                return StockProducto;
            }

        }

        public string Devuelve_Almacen_PorDefecto(string producto)
        {
            try
            {
                cnn_NAV.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_NAV;
                string sql = "SELECT [No_],[NEO Location Code] FROM [NAVDB].[dbo].[THERMO$Item$e8af12d5-328b-47d8-b2d8-158ecbed1ec3] WHERE  [No_] = '"+ producto + "'";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_NAV);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_NAV.Close();
                return dt.Rows[0]["NEO Location Code"].ToString();               
            }
            catch (Exception ex)
            {
                cnn_NAV.Close();
                return "";
            }

        }

        public string Devuelve_CantidadCaja_PorDefecto(string producto)
        {
            try
            {
                cnn_NAV.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_NAV;
                string sql = "SELECT rtrim(I.[No_]) AS PRODUCT, CASE WHEN E.[Quantity per Package] IS NULL THEN 0 ELSE CAST(E.[Quantity per Package] AS int) END AS PZCAJA" +
                                " FROM[NAVDB].[dbo].[THERMO$Item] I" +
                                " LEFT JOIN[NAVDB].[dbo].[THERMO$NEO Envases por producto$e8af12d5-328b-47d8-b2d8-158ecbed1ec3] E ON I.No_ = E.Item AND E.Predetermined = 1" +
                                " WHERE I.No_ = '"+producto+"'";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_NAV);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_NAV.Close();
                return dt.Rows[0]["PZCAJA"].ToString();
            }
            catch (Exception ex)
            {
                cnn_NAV.Close();
                return "";
            }

        }

        /////////AUXILIARES

        public bool APP_Almacenes_Activa()
        {
            try
            {
                cnn_SMARTH.Open();
                string sql = "SELECT * FROM[SMARTH_DB].[dbo].[AUX_APPs_Activas]";
                SqlCommand cmd = new SqlCommand(sql, cnn_SMARTH);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                if (Convert.ToInt32(dt.Rows[0]["AlmacenesNAV"]) == 0)
                { return false; }
                else
                { return true; }
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }

        public DataSet devuelve_lista_materiales(string where)
        {
            try
            {
                cnn_NAV.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_NAV;
                string sql = "SELECT I.[No_] + ' ¬ ' + [Description] as MATFILTRO, I.[No_] AS MATERIAL, [Description] AS LONG_DESCRIPTION, R.[NEO Shelf location] AS SHORT_DESCRIPTION, CONCAT (R.[NEO Drying temperature], ' | ', R.[NEO Drying time]) AS REMARKS, CAN.CANTALM, PUR.FECHA, PUR.QUANTITY" +
                             " FROM[NAVDB].[dbo].[THERMO$Item] I " +
                             " LEFT JOIN[NAVDB].[dbo].[THERMO$Item$e8af12d5-328b-47d8-b2d8-158ecbed1ec3] R ON I.No_ = R.No_ " +
                             " LEFT JOIN(SELECT MAT.[No_] AS MATERIAL, CASE WHEN CANTALM IS NULL THEN 0.0 ELSE CANTALM END AS CANTALM" +
                                        " FROM[NAVDB].[dbo].[THERMO$Item] MAT" +
                                        " LEFT JOIN(SELECT RTRIM([Item No_]) as MATERIAL, sum([Item Ledger Entry Quantity]) AS CANTALM" +
                                        " FROM[NAVDB].[dbo].[THERMO$Value Entry] where[Item No_] LIKE '2%' group by[Item No_]) CAN ON MAT.[No_] = CAN.MATERIAL" +
                                        " WHERE MAT.[No_] LIKE '2%') CAN ON I.No_ = CAN.MATERIAL" +
                             " LEFT JOIN(SELECT DAT.No_, DAT.FECHA, CAN.Quantity " +
                                         " FROM(SELECT [No_], MIN([Expected Receipt Date]) AS FECHA" +
                                              " FROM[NAVDB].[dbo].[THERMO$Purchase Line]" +
                                              " WHERE[Document No_] LIKE 'PC23%' AND[Outstanding Amount] > 0 AND[Posting Group] = 'MAT. PRIMA' AND[Line No_] LIKE '1%' GROUP BY No_) DAT" +
                                         " LEFT JOIN[NAVDB].[dbo].[THERMO$Purchase Line] CAN ON DAT.No_ = CAN.No_ AND DAT.FECHA = CAN.[Expected Receipt Date]) PUR ON I.No_ = PUR.No_" +
                             " WHERE " + where + " " +
                             " AND(I.[Item Category Code] = 8 OR I.[Item Category Code] = 15 OR I.[Item Category Code] = 215 OR I.[Item Category Code] = 221 OR I.[Item Category Code] = 215 OR I.[Item Category Code] = 155" +
                                    " OR I.[Item Category Code] = 150 OR I.[Item Category Code] = 012 OR I.[Item Category Code] = 10 OR I.[Item Category Code] = 12 " +
                                    " OR I.[Item Category Code] = 120 OR I.[Item Category Code] = 20 OR I.[Item Category Code] = 200 OR I.[Item Category Code] = 220" +
                                    " OR I.[Item Category Code] = 221 OR I.[Item Category Code] = 222)" +
                              " ORDER BY MATERIAL asc";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_NAV);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_NAV.Close();
                return ds;
            }
            catch (Exception ex)
            {
                cnn_NAV.Close();
                return null;
            }
        }

        ////////ALMACEN FINAL
        public bool Existe_Ubicacion_ALMFINAL(string estanteria, string modulo, string baldas)
        {
            try
            {
                cnn_SMARTH.Open();
                string sql = "SELECT * FROM[SMARTH_DB].[dbo].[UBICACIONES_Listado_Sphere] WHERE Estanteria = '" + estanteria + "' AND Modulo = '" + modulo + "' AND Balda = '" + baldas + "' AND UBIActiva = 1";
                SqlCommand cmd = new SqlCommand(sql, cnn_SMARTH);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                if (dt.Rows.Count > 0)
                { return true; }
                else
                { return false; }


            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }

        public DataTable Devuelve_Lista_Materiales_Ubicados_ALMFINAL()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT MBI.Id, [FechaEntrada],[Articulo], MAT.Descripcion,MBI.[Ubicacion],[FechaAuditoria],[Eliminado],UBI.Estanteria,UBI.Modulo,UBI.Balda" +
                                  " FROM[SMARTH_DB].[dbo].[UBICACIONES_Producto_Terminado] MBI" +
                                  " left join [SMARTH_DB].[dbo].[AUX_TablaProductos] MAT ON MBI.Articulo = MAT.Referencia" +
                                  " left join [SMARTH_DB].[dbo].[UBICACIONES_Listado_Sphere] UBI ON MBI.Ubicacion = UBI.NombreUbicacion" +
                                  " WHERE [Eliminado] = 0";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_GP12);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception EX)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_Lista_Materiales_Ubicados_Silos_ALMFINAL()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = " SELECT[NombreUbicacion],[Estanteria],[Modulo],[Balda]" +
                              " , CASE WHEN MAT.Articulo IS NULL THEN '-' ELSE CAST(MAT.Articulo AS varchar) END AS Articulo" +
                              " ,CASE WHEN MAN.Descripcion IS NULL THEN '' ELSE MAN.Descripcion END AS Descripcion" +
                              " , CASE WHEN MAT.FechaEntrada IS NULL THEN '2000-01-01 00:00:00.000' ELSE MAT.FechaEntrada END AS FechaEntrada" +
                              " FROM[SMARTH_DB].[dbo].[UBICACIONES_Listado_Sphere] UBI" +
                              " left join SMARTH_DB.dbo.[UBICACIONES_Producto_Terminado] MAT ON UBI.NombreUbicacion = MAT.Ubicacion AND MAT.[Eliminado] = 0" +
                              " left join [SMARTH_DB].[dbo].[AUX_TablaProductos] MAN ON MAT.Articulo = MAN.Referencia" +
                              " where NombreUbicacion like 'SIL%'" +
                              " ORDER BY NombreUbicacion";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception EX)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public bool Insertar_MaterialxUbicacion_ALMFINAL(string ubicacion, string referencia, string operario)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql2 = "INSERT INTO [SMARTH_DB].[dbo].[UBICACIONES_Producto_Terminado] " +
                                          " ([FechaEntrada],[Articulo],[Ubicacion],[FechaAuditoria],[Eliminado],[Operario]) VALUES " +
                                          " (CONVERT(VARCHAR(10), GETDATE(), 103)," + referencia.Trim() + ",'" + ubicacion + "',CONVERT(VARCHAR(10), GETDATE(), 103),0,'" + operario + "')";
                cmd.CommandText = sql2;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }

        public void Elimina_MaterialxUbicacion_ALMFINAL(string fecha, string referencia, string ubicacion)

        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "UPDATE [SMARTH_DB].[dbo].[UBICACIONES_Producto_Terminado] SET [Eliminado] = 1 where FechaEntrada = '" + fecha + "' and Articulo = " + referencia + " and Ubicacion = '" + ubicacion + "'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
            }

        }

        public DataTable Devuelve_Lista_MaterialesXUbicacion_ALMFINAL(string referencia)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT [FechaEntrada],[Articulo], UBI.[Ubicacion],[FechaAuditoria],[Eliminado],[Operario],MAT.Descripcion,Estanteria, Modulo, Balda" +
                                " FROM[SMARTH_DB].[dbo].[UBICACIONES_Producto_Terminado] UBI" +
                                " left join [SMARTH_DB].[dbo].[SMARTH_DB].[dbo].[AUX_TablaProductos] MAT ON UBI.Articulo = MAT.Referencia" +
                                " LEFT JOIN [SMARTH_DB].[dbo].[UBICACIONES_Listado_Sphere] UB2 on UBI.Ubicacion = UB2.NombreUbicacion" +
                                " WHERE Articulo = " + referencia + " and [Eliminado] = 0";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception EX)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        ////////MATERIAS PRIMAS
        public bool Existe_Ubicacion(string estanteria, string modulo, string baldas)
        {
            try
            {
                cnn_GP12.Open();
                string sql = "SELECT * FROM[SMARTH_DB].[dbo].[UBICACIONES_Listado] WHERE Estanteria = '" + estanteria + "' AND Modulo = '" + modulo + "' AND Balda = '" + baldas + "' AND UBIActiva = 1";
                SqlCommand cmd = new SqlCommand(sql, cnn_GP12);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_GP12);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_GP12.Close();
                if (dt.Rows.Count > 0)
                { return true; }
                else
                { return false; }


            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DataTable Devuelve_Lista_Materiales_Ubicados()
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT MBI.Id, [FechaEntrada],[Articulo], MAT.Descripcion,MBI.[Ubicacion],[FechaAuditoria],[Eliminado],UBI.Estanteria,UBI.Modulo,UBI.Balda" +
                                  " FROM[SMARTH_DB].[dbo].[UBICACIONES_Materias_Primas] MBI" +
                                  " left join SMARTH_DB.dbo.AUX_Lista_Materiales MAT ON MBI.Articulo = MAT.Referencia" +
                                  " left join SMARTH_DB.dbo.UBICACIONES_Listado UBI ON MBI.Ubicacion = UBI.NombreUbicacion" +
                                  " WHERE [Eliminado] = 0";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_GP12);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_GP12.Close();
                return dt;
            }
            catch (Exception EX)
            {
                cnn_GP12.Close();
                return null;
            }
        }

        public DataTable Devuelve_Lista_Materiales_Ubicados_Silos()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = " SELECT[NombreUbicacion],[Estanteria],[Modulo],[Balda]" +
                              " , CASE WHEN MAT.Articulo IS NULL THEN '-' ELSE CAST(MAT.Articulo AS varchar) END AS Articulo" +
                              " ,CASE WHEN MAN.Descripcion IS NULL THEN '' ELSE MAN.Descripcion END AS Descripcion" +
                              " , CASE WHEN MAT.FechaEntrada IS NULL THEN '2000-01-01 00:00:00.000' ELSE MAT.FechaEntrada END AS FechaEntrada" +
                              " FROM[SMARTH_DB].[dbo].[UBICACIONES_Listado] UBI" +
                              " left join SMARTH_DB.dbo.UBICACIONES_Materias_Primas MAT ON UBI.NombreUbicacion = MAT.Ubicacion AND MAT.[Eliminado] = 0" +
                              " left join SMARTH_DB.dbo.AUX_Lista_Materiales MAN ON MAT.Articulo = MAN.Referencia" +
                              " where NombreUbicacion like 'SIL%'" +
                              " ORDER BY NombreUbicacion";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception EX)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public bool Insertar_MaterialxUbicacion(string ubicacion, string referencia, string operario)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql2 = "INSERT INTO [SMARTH_DB].[dbo].[UBICACIONES_Materias_Primas] " +
                                          " ([FechaEntrada],[Articulo],[Ubicacion],[FechaAuditoria],[Eliminado],[Operario]) VALUES " +
                                          " (CONVERT(VARCHAR(10), GETDATE(), 103)," + referencia.Trim() + ",'" + ubicacion + "',CONVERT(VARCHAR(10), GETDATE(), 103),0,'" + operario + "')";
                cmd.CommandText = sql2;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }

        public void Elimina_MaterialxUbicacion(string fecha, string referencia, string ubicacion)

        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "UPDATE [SMARTH_DB].[dbo].[UBICACIONES_Materias_Primas] SET [Eliminado] = 1 where FechaEntrada = '" + fecha + "' and Articulo = " + referencia + " and Ubicacion = '" + ubicacion + "'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
            }

        }

        public void Elimina_MaterialxUbicacionFINAL(string fecha, string referencia, string ubicacion)

        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "UPDATE [SMARTH_DB].[dbo].[UBICACIONES_Producto_Terminado] SET [Eliminado] = 1 where FechaEntrada = '" + fecha + "' and Articulo = " + referencia + " and Ubicacion = '" + ubicacion + "'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
            }

        }

        public DataTable Devuelve_Lista_MaterialesXUbicacion(string referencia)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT [FechaEntrada],[Articulo], UBI.[Ubicacion],[FechaAuditoria],[Eliminado],[Operario],MAT.Descripcion,Estanteria, Modulo, Balda" +
                                " FROM[SMARTH_DB].[dbo].[UBICACIONES_Materias_Primas] UBI" +
                                " left join [SMARTH_DB].[dbo].AUX_Lista_Materiales MAT ON UBI.Articulo = MAT.Referencia" +
                                " LEFT JOIN [SMARTH_DB].[dbo].[UBICACIONES_Listado] UB2 on UBI.Ubicacion = UB2.NombreUbicacion" +
                                " WHERE Articulo = " + referencia + " and [Eliminado] = 0";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_SMARTH.Close();
                return dt;
            }
            catch (Exception EX)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_Datos_Material_NAV(string referencia)
        {
            try
            {
                cnn_NAV.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_NAV;
                string sql = "SELECT MAT.[No_] AS MATERIAL, CASE WHEN CANTALM IS NULL THEN 0.0 ELSE ROUND(CAST(CANTALM AS DECIMAL),0) END AS CANTALM, CAST(FORMAT (PUR.FECHA, 'dd/MM/yyyy ') AS VARCHAR) AS FECHA FROM [NAVDB].[dbo].[THERMO$Item] MAT" +
                             " LEFT JOIN(SELECT RTRIM([Item No_]) as MATERIAL, sum([Item Ledger Entry Quantity]) AS CANTALM FROM[NAVDB].[dbo].[THERMO$Value Entry]" +
                               " group by[Item No_]) CAN ON MAT.[No_] = CAN.MATERIAL" +
                             //" where[Item No_] LIKE '2%' group by[Item No_]) CAN ON MAT.[No_] = CAN.MATERIAL" +
                             " LEFT JOIN(SELECT DAT.No_, DAT.FECHA, CAN.Quantity " +
                                         " FROM(SELECT [No_], MIN([Expected Receipt Date]) AS FECHA" +
                                              " FROM[NAVDB].[dbo].[THERMO$Purchase Line]" +
                                              " WHERE[Document No_] LIKE 'PC23%' AND[Outstanding Amount] > 0 AND[Posting Group] = 'MAT. PRIMA' AND[Line No_] LIKE '1%' GROUP BY No_) DAT" +
                                         " LEFT JOIN[NAVDB].[dbo].[THERMO$Purchase Line] CAN ON DAT.No_ = CAN.No_ AND DAT.FECHA = CAN.[Expected Receipt Date]) PUR ON MAT.No_ = PUR.No_" +

                             " WHERE MAT.[No_] LIKE '" + referencia + "'";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_NAV);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_NAV.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_NAV.Close();
                return null;
            }
        }

        ////////ALMACENES

        //***************************************\\ETIQUETAS//***************************************\\

        public bool Insertar_Etiqueta_Auxiliar(string ETIQUETA, string TAREA, string REFERENCIA, string DESCRIPCION, string LOTE, string OBSERVACIONES, int CANTIDADORI, int CANTIDADREAL, int OPERARIO, int NUMETIQUETAS) //SMARTH
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[CALIDAD_Etiquetas_Auxiliares]" +
                                 " ([Etiqueta],[Tarea],[Referencia],[Descripcion],[Lote],[Observaciones],[CantidadORI],[CantidadREAL],[Operario],[NumEtiqueta],[FechaImpresion],[Eliminar]) " +
                                 " VALUES ('"+ETIQUETA+"','"+TAREA+ "','" + REFERENCIA + "','" + DESCRIPCION + "','" + LOTE + "','" + OBSERVACIONES + "'," + CANTIDADORI + "," + CANTIDADREAL + "," + OPERARIO + "," + NUMETIQUETAS + ",SYSDATETIME(),0)";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception EX)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }

        public DataTable Devuelve_Etiquetas_Auxiliares(string where)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT TOP(10) * FROM[SMARTH_DB].[dbo].[CALIDAD_Etiquetas_Auxiliares] "+where+" ORDER BY FechaImpresion DESC", cnn_SMARTH);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                cnn_SMARTH.Close();
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                return dt;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }








        //***************************************\\OBSOLETOS Y DESHABILITADOS//***************************************\\
        //private readonly SqlConnection cnn_RPS = new SqlConnection();
        //cnn_RPS.ConnectionString = ConfigurationManager.ConnectionStrings["RPS"].ToString();


        /*Consultas
            Entregas pendientes:
                SELECT [codart]
                    ,[fecentlin]
                    ,[descr]
                    ,(CANPED - canenv) AS CANPEND
            FROM [RPS].[dbo].[fapedl]
            where codemp = 1
                 and tipcont = 0
                 and (canped > canenv) 
                 and (fecentlin BETWEEN SYSDATETIME() AND DATEADD(DAY,+15,SYSDATETIME()))
            order by fecentlin ASC
        */
        /*
        // leer area de rechazo con filtro "activas"
        public DataSet leer_area_rechazo()
        {
            try
            {
                cnn_Area_Rechazo.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM [CALIDAD].[dbo].[AreaRechazo] WHERE FechaSalida is NULL", cnn_Area_Rechazo);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                cnn_Area_Rechazo.Close();
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                cnn_Area_Rechazo.Close();
                return null;
            }
        }

        // leer area de rechazo con filtro "todas"
        public DataSet leer_area_rechazo_todas()
        {
            try
            {
                cnn_Area_Rechazo.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM [CALIDAD].[dbo].[AreaRechazo]",cnn_Area_Rechazo);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                cnn_Area_Rechazo.Close();
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                cnn_Area_Rechazo.Close();
                return null;
            }
        }

        public void eliminar_area_rechazo(int id)
        {
            try
            {
                cnn_Area_Rechazo.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_Area_Rechazo;
                string sql = "DELETE FROM [CALIDAD].[dbo].[AreaRechazo] WHERE Id = " + id;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_Area_Rechazo.Close();
            }
            catch (Exception)
            {

            }
        }

        public void actualizar_area_rechazo(Int32 id, string referencia, string motivo, string responsableEntrada,
                                            Int32  cantidad, string fechaEntrada, string fechaSalida, string debeSalir,
                                            string decision, string responsableSalida, string observaciones)
        {
            try
            {
                cnn_Area_Rechazo.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_Area_Rechazo;
                string sql = "UPDATE dbo.AreaRechazo SET Referencia = " + referencia + ", Motivo = '" + motivo + "', ResponsableEntrada = '" + responsableEntrada + "', Cantidad = " + cantidad + ", FechaEntrada = CASE WHEN '" + fechaEntrada + "' = '' THEN NULL ELSE '" + fechaEntrada + "' END, FechaSalida = CASE WHEN '" + fechaSalida + "' = '' THEN NULL ELSE '" + fechaSalida + "' END, DebeSalir = CASE WHEN '" + debeSalir + "' = '' THEN NULL ELSE '" + debeSalir + "' END, Decision = '" + decision + "', ResponsableSalida = '" + responsableSalida + "', Observaciones = '" + observaciones + "' WHERE Id = " + id;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_Area_Rechazo.Close();
            }
            catch (Exception)
            {

            }
        }

        public void insertar_area_rechazo(string referencia, string motivo, string responsableEntrada,
                                            Int32 cantidad, string fechaEntrada, string fechaSalida, string debeSalir,
                                            string decision, string responsableSalida, string observaciones)
        {
            try
            {
                cnn_Area_Rechazo.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_Area_Rechazo;
                string sql = "INSERT INTO dbo.AreaRechazo (Referencia,Motivo,ResponsableEntrada,Cantidad,FechaEntrada,FechaSalida,DebeSalir,Decision,ResponsableSalida,Observaciones) VALUES ('" + referencia + "','" + motivo + "','" + responsableEntrada + "'," + cantidad + ",CASE WHEN '" + fechaEntrada + "' = '' THEN NULL ELSE '" + fechaEntrada + "' END, CASE WHEN '" + fechaSalida + "'= '' THEN NULL ELSE '" + fechaSalida + "' END, CASE WHEN '" + debeSalir + "'= '' THEN NULL ELSE '" + debeSalir + "' END,'" + decision +"','" + responsableSalida + "','" + observaciones + "')";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_Area_Rechazo.Close();
            }
            catch (Exception)
            {

            }
          
        }*/
        /*
       public bool leer_previsiones()
       {
           try
           {
               cnn_RPS.Open();
               SqlCommand cmd = new SqlCommand();
               SqlDataReader reader;
               cmd.Connection = cnn_RPS;
               //cmd.CommandText = "SELECT [codart], [fecentlin], SUM (CANPED - canenv) AS CANPEND FROM [RPS].[dbo].[fapedl] where codemp = 1 and tipcont = 0 and (canped > canenv) and (fecentlin BETWEEN SYSDATETIME() AND DATEADD(DAY,+15,SYSDATETIME())) and codart > 30000000 GROUP BY fecentlin, codart, canped order by fecentlin ASC";
               //cmd.CommandText = "SELECT codart as articulo, fecentlin as fecha, SUM (CANPED - canenv) AS cantidad FROM [RPS].[dbo].[fapedl] where codemp = 1 and tipcont = 0 and (canped > canenv) and (fecentlin BETWEEN SYSDATETIME() AND DATEADD(DAY,+15,SYSDATETIME())) and cumpli = 'N' and codart > 30000000 GROUP BY fecentlin, codart, canped order by fecentlin ASC";
               cmd.CommandText = "SELECT codart as articulo, fecentlin as fecha, SUM (CANPED - canenv) AS cantidad FROM [RPS].[dbo].[fapedl] where codemp = 1 and tipcont = 0 and (canped > canenv) and (fecentlin BETWEEN DATEADD(DAY,-15,SYSDATETIME()) AND DATEADD(DAY,+15,SYSDATETIME())) and cumpli = 'N' and codart > 30000000 GROUP BY fecentlin, codart, canped order by fecentlin ASC";
               //cmd.CommandText = "SELECT P.[Referencia],R.Descripcion,[FechaEntrega],[cantidad],R.Cliente, N.Razon FROM [CALIDAD].[dbo].[PrevisionGP12] P LEFT JOIN [SMARTH_DB].[dbo].[GP12_ProductosEstados] Q ON P.[Referencia] = Q.Referencia LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] R ON P.Referencia = R.Referencia LEFT JOIN [SMARTH_DB].[dbo].[AUX_GP12_RazonRevision] N ON EstadoActual = N.Id WHERE Q.EstadoActual > 0 AND q.EstadoActual < 7 and FechaEntrega >= DATEADD(DAY,-1,SYSDATETIME()) Group By P.[Referencia],R.Descripcion, [FechaEntrega], [cantidad], [Cliente], N.Razon ORDER BY FechaEntrega ASC";
               reader = cmd.ExecuteReader();
               while (reader.Read())
               {
                   insertar_previsiones(Convert.ToInt32(reader["articulo"]), Convert.ToDateTime(reader["fecha"]), Convert.ToInt32(reader["cantidad"]));
               }
               cnn_RPS.Close();
               return true;
           }
           catch (Exception)
           {

               cnn_RPS.Close();
               return false;
           }
       }
       */
        /*
       public bool leer_operariosBMS()
       {

           try
           {
               //string sql = "SELECT '' AS ID, C_LONG_DESCR AS DESCRIPCION, TRIM(C_ID) AS CODIGO, TRIM(C_ID) AS REFERENCIAMOLDE, TRIM(C_ORIGINALCAVITIES) AS CAVIDADES, C_CHARACTERISTICS AS UBICACION FROM PCMS.T_TOOLS WHERE C_ID LIKE '3333%' or C_ID LIKE '9999%'";
               string sql = "SELECT C_ID, C_NAME FROM PCMS.T_EMPLOYEES";
               OracleCommand cmd = new OracleCommand(sql, cnn_bms);
               cnn_bms.Open();

               using (OracleDataReader reader = cmd.ExecuteReader())
               {
                   while (reader.Read())
                   {
                       insertar_operario(Convert.ToInt32(reader["C_ID"]), reader["C_NAME"].ToString(), "1");

                   }
               }
               cnn_bms.Close();
               return true;
           }
           catch (Exception)
           {
               cnn_bms.Close();
               return false;
           }
       }
       */
    }
}