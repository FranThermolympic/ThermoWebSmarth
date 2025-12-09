using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
//using System.Data.OracleClient;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace ThermoWeb.KPI
{
    public class Conexion_KPI
    {
        private readonly SqlConnection cnn_SMARTH = new SqlConnection();
        private readonly SqlConnection cnn_GP12 = new SqlConnection();
        private readonly OracleConnection cnn_bms = new OracleConnection();
        private readonly SqlConnection cnn_RPS = new SqlConnection();
        
        private readonly SqlConnection cnn_NAV = new SqlConnection();

        public Conexion_KPI()
        {
            cnn_GP12.ConnectionString = ConfigurationManager.ConnectionStrings["area_rechazo"].ToString();
            cnn_SMARTH.ConnectionString = ConfigurationManager.ConnectionStrings["SMARTH"].ToString();
            cnn_bms.ConnectionString = ConfigurationManager.ConnectionStrings["BMS"].ToString();
            cnn_RPS.ConnectionString = ConfigurationManager.ConnectionStrings["RPS"].ToString();
            cnn_NAV.ConnectionString = ConfigurationManager.ConnectionStrings["NAV"].ToString();
        }
        //CONECTORES KPI
        //APP WORKTOP
        public DataSet KPI_PiezasXTrend(string MAQUINA, string FECHAINICIO, string FECHAFIN)
        {
            try
            {
                cnn_bms.Open();
                string sql = " SELECT SUM(C_PRODUCTIONCOUNT02) AS PIEZAS, ROUND(SUM(C_RUNTIME)/60,2) AS PRODUCIENDO   " +
                                " FROM HIS.T_HISTRENDS"+
                                " WHERE C_MACHINE_NR = '"+MAQUINA+"' AND C_STARTTIME >= TO_DATE('"+ FECHAINICIO + "', 'dd/mm/yyyy HH24:MI:SS') AND C_ENDTIME <= TO_DATE('" + FECHAFIN+"', 'dd/mm/yyyy HH24:MI:SS')";
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataSet dsprod = new DataSet();
                query.Fill(dsprod);
                cnn_bms.Close();
                return dsprod;
            }
            catch (Exception ex)
            {
                cnn_bms.Close();
                return null;
            }
        }

        public DataSet KPI_TiempoEntreParo()
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT C_ID, C_FROMDATE AS INICIO, C_FROMDATE + ((C_DURATION + C_RETROTIME) / 1440) AS HORAFIN, ROUND((C_DURATION+C_RETROTIME)/100,2) AS TIEMPO_CAMBIO, '0' AS HORASBIDON, '0' AS PIEZASBIDON" +
                             " FROM HIS.T_HISELOGSTOPS STP"+
                             " WHERE C_MACHINE = 'WTOP' AND C_STOPCODE = 42"+
                             " ORDER BY C_FROMDATE DESC";
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataSet dsprod = new DataSet();
                query.Fill(dsprod);
                cnn_bms.Close();
                return dsprod;
            }
            catch (Exception ex)
            {
                cnn_bms.Close();
                return null;
            }
        }
        //CONECTORES REFERENCIAS Y ESTADO

        //**\\BSH//**\\
        public DataSet KPI_Mensual_Uso_Maquina(string año, string mes, string cliente, string grupo)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT EXTRACT(YEAR FROM C_STARTTIME) AS YEAR, EXTRACT(month FROM C_STARTTIME) AS MESNUM, to_char(to_date(C_STARTTIME, 'DD-MM-YYYY'), 'Month', 'NLS_DATE_LANGUAGE = spanish') AS MES, C_CUSTOMER, G.C_SHORT_DESCR, CAST(SUM(C_RUNTIME) / 60 AS DECIMAL(10, 2)) AS TIEMPO" +
                             " FROM HIS.T_HISJOBS J LEFT JOIN PCMS.T_MACHINES M ON J.C_MACHINE_NR = M.C_ID LEFT JOIN PCMS.T_MCHGROUPS G ON M.C_MCHGRP_ID = G.C_ID" +
                             " WHERE EXTRACT(YEAR FROM C_STARTTIME) LIKE '"+año+"%' "+mes+" AND C_CUSTOMER LIKE '"+cliente+"%'" +
                             " GROUP BY EXTRACT(YEAR FROM C_STARTTIME), EXTRACT(month FROM C_STARTTIME),to_char(to_date(C_STARTTIME, 'DD-MM-YYYY'), 'Month', 'NLS_DATE_LANGUAGE = spanish'), C_CUSTOMER, G.C_SHORT_DESCR, G.C_ID ORDER BY YEAR, MESNUM, C_CUSTOMER, G.C_ID";
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

        public DataSet KPI_Mensual_Uso_Operario(string año, string mes, string cliente)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT EXTRACT(YEAR FROM C_STARTTIME) AS YEAR,EXTRACT(month FROM C_STARTTIME) AS MESNUM,to_char(to_date(C_STARTTIME, 'DD-MM-YYYY'), 'Month', 'NLS_DATE_LANGUAGE = spanish') AS MES, C_CUSTOMER, CAST(SUM((C_ACTUALAVAILABLETIME / 60) * C_FACTORDIV2) AS DECIMAL(10, 2)) AS TIEMPOOP" +
                                " FROM HIS.T_HISOPERATORS" +
                                " WHERE EXTRACT(YEAR FROM C_STARTTIME) LIKE '"+año+ "%' " + mes + "  AND C_CUSTOMER LIKE '" + cliente + "%' AND C_OPERATORTYPE = 1" +
                                " GROUP BY EXTRACT(YEAR FROM C_STARTTIME), EXTRACT(month FROM C_STARTTIME),to_char(to_date(C_STARTTIME, 'DD-MM-YYYY'), 'Month', 'NLS_DATE_LANGUAGE = spanish'), C_CUSTOMER" +
                                " ORDER BY year, MESNUM, C_CUSTOMER";
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

        public DataSet KPI_Mensual_Uso_Cambiador(string año, string mes, string cliente)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT EXTRACT(YEAR FROM C_STARTTIME) AS YEAR, EXTRACT(month FROM C_STARTTIME) AS MESNUM, to_char(to_date(C_STARTTIME, 'DD-MM-YYYY'), 'Month', 'NLS_DATE_LANGUAGE = spanish') AS MES, C_CUSTOMER, CAST(SUM((C_ACTUALAVAILABLETIME / 60) * C_FACTORDIV2) AS DECIMAL(10, 2)) AS TIEMPOOP" +
                                " FROM HIS.T_HISOPERATORS" +
                                " WHERE EXTRACT(YEAR FROM C_STARTTIME) LIKE '"+ año + "%' " + mes + "  AND C_CUSTOMER LIKE '" + cliente + "%' AND C_OPERATORTYPE = 3" +
                                " GROUP BY EXTRACT(YEAR FROM C_STARTTIME), EXTRACT(month FROM C_STARTTIME),to_char(to_date(C_STARTTIME, 'DD-MM-YYYY'), 'Month', 'NLS_DATE_LANGUAGE = spanish'), C_CUSTOMER ORDER BY YEAR, MESNUM, C_CUSTOMER";
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

        public DataSet KPI_Mensual_KGTransformados(string año, string mes, string cliente)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT EXTRACT(YEAR FROM C_STARTTIME) AS YEAR, EXTRACT(month FROM C_STARTTIME) AS MESNUM, to_char(to_date(C_STARTTIME, 'DD-MM-YYYY'), 'Month', 'NLS_DATE_LANGUAGE = spanish') AS MES, HS.C_CUSTOMER, SUM((RE.C_UNITS * HS.C_PRODUCTIONCOUNT02)) AS KG"+ 
                             " FROM HIS.T_HISJOBS HS LEFT JOIN PCMS.T_RECIPE_X_MATERIAL RE ON HS.C_RECIPE = RE.C_RECIPE_ID LEFT JOIN PCMS.T_MATERIALS MAT ON RE.C_MATERIAL_ID = MAT.C_ID"+
                             " WHERE EXTRACT(YEAR FROM C_STARTTIME) LIKE '"+año+ "%' " + mes + "  AND C_CUSTOMER LIKE '" + cliente+"%' AND(MAT.C_MATTYPE_ID = '15' OR MAT.C_MATTYPE_ID = '150' OR MAT.C_MATTYPE_ID = '215'  OR MAT.C_MATTYPE_ID = '216' OR MAT.C_MATTYPE_ID = '23')"+
                             " GROUP BY EXTRACT(YEAR FROM C_STARTTIME), EXTRACT(month FROM C_STARTTIME),to_char(to_date(C_STARTTIME, 'DD-MM-YYYY'), 'Month', 'NLS_DATE_LANGUAGE = spanish'), C_CUSTOMER ORDER BY YEAR, MESNUM, C_CUSTOMER";
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
        //**\\KPIS DIMENSIONES //**\\
        public DataSet Devuelve_OperarioXCliente(string fechainicio, string fechafin)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT CAST(C_OPERATOR AS VARCHAR(4)) AS OPERARIO, C_CUSTOMER,  C_OPERATORNAME, ROUND(SUM((C_AVAILABLETIME / 60)*CASE WHEN C_FACTORDIV2 = 0 THEN 1 ELSE C_FACTORDIV2 END), 2) AS TIEMPODISP, ROUND(SUM((C_RUNTIME / 60)*CASE WHEN C_FACTORDIV2 = 0 THEN 1 ELSE C_FACTORDIV2 END), 2) AS TIEMPOFUNC"+
                                " FROM HIS.T_HISOPERATORS" +
                                " WHERE C_PARINDEX = 1 AND C_CUSTOMER<> '-' AND C_AVAILABLETIME > 0 AND C_STARTTIME BETWEEN TO_DATE('"+fechainicio+"', 'DD/MM/YYYY') AND TO_DATE('"+fechafin+"', 'DD/MM/YYYY')" +
                                " GROUP BY C_CUSTOMER, C_OPERATOR,C_OPERATORNAME ORDER BY C_CUSTOMER, OPERARIO";
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataSet dsope = new DataSet();
                query.Fill(dsope);
                cnn_bms.Close();
                return dsope;
            }
            catch (Exception ex)
            {
                cnn_bms.Close();
                return null;
            }
        }

        public DataTable Devuelve_OperarioXEmpresa()
        {
            try
            {
                cnn_NAV.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_NAV;
                string sql = "SELECT CAST([No_] AS VARCHAR(4)) AS NUM, CASE WHEN[Job Title] IS NULL THEN 'SIN DEFINIR' ELSE[Job Title] END AS PUESTO" +
                                " ,[Search Name] AS NOMBRE, CASE WHEN COD.Description IS NULL THEN 'SIN DEFINIR' ELSE COD.Description END AS DESCRIPCION"+
                                " FROM[NAVDB].[dbo].[THERMO$Employee] EMP" +
                                " LEFT JOIN[NAVDB].[dbo].[THERMO$Employment Contract] COD ON EMP.[Emplymt_ Contract Code] = COD.Code" +
                                " ORDER BY NUM";
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




        //**\\APROVECHAMIENTO//**\\
        public DataSet AprovechamientoOperario(string FechaInicio, string FechaFinal)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT [Fecha],[OPLOGUEADOS],[OPASIGNADOS],[OPAPROVECHADOS],[OPLIBRES], [CAL],[ENC1],[ENC2] FROM[SMARTH_DB].[dbo].[KPI_Aprovechamiento] where Fecha > CONVERT(DATETIME, '" + FechaInicio+"', 21) AND Fecha<CONVERT(DATETIME,'"+FechaFinal+ "',21) and [OPLOGUEADOS] > 0  order by fecha desc";
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
        public DataSet Aprovechamiento_Operario_4H_NEW()
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT Fecha, BMS.TURNO, OPE AS FICHADOS, OPLOGUEADOS,OPASIGNADOS, OPAPROVECHADOS, OPLIBRES, CAL, ENC1, ENC2" +
                    " FROM (SELECT TOP(8) CONVERT(DATE, [Fecha]) AS FechaPart, [Fecha]" +
                            " ,CASE WHEN CONVERT(VARCHAR(8), [Fecha], 108) BETWEEN  '05:00:00' and  '13:00:00' THEN 'M'" +
                            " WHEN CONVERT(VARCHAR(8), [Fecha], 108) BETWEEN '13:00:00' and '21:00:00' THEN 'T'" +
                            " ELSE 'N' END AS TURNO, [OPLOGUEADOS],[OPASIGNADOS],[OPAPROVECHADOS],[OPLIBRES], [CAL],[ENC1],[ENC2]" +
                            " FROM[SMARTH_DB].[dbo].[KPI_Aprovechamiento] WHERE[OPLOGUEADOS] > 0 order by fecha desc) BMS" +
                    " LEFT JOIN(SELECT FechaPart, TURNO, COUNT(NoP) as OPE" +
                    " FROM(SELECT[NoP], CONVERT(DATE, [Fecha]) AS FechaPart" +
                          " ,CASE WHEN CONVERT(VARCHAR(8), [Fecha], 108) BETWEEN  '05:00:00' and  '13:00:00' THEN 'M'" +
                          " WHEN CONVERT(VARCHAR(8), [Fecha], 108) BETWEEN '13:00:00' and '21:00:00' THEN 'T'" +
                          " ELSE 'N' END AS TURNO, TipoOperario, OP.Operario" +
                          " FROM[SMARTH_DB].[dbo].[AUX_Marcajes_Reloj] MC" +
                          " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] OP ON MC.NoP = OP.Id" +
                          " where Fichaje = 'Entrando' AND TipoOperario LIKE 'OPERAR%') FIC" +
                          " GROUP BY FechaPart, TURNO) RLJ ON BMS.FechaPart = RLJ.FechaPart and BMS.TURNO = RLJ.TURNO order by Fecha desc";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_GP12.Close();
                return ds;
            }
            catch (Exception ex)
            {
                cnn_GP12.Close();
                return null;
            }
        }

        public DataSet Aprovechamiento_Operario_Semanas_NEW(string Fecha)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT DATEPART(wk, [Fecha]) as SEMANA, MIN([Fecha]) AS INICIO, AVG(OPE) AS FICHADOS, AVG(OPLOGUEADOS) AS OPLOGUEADOS, AVG(OPASIGNADOS) AS OPASIGNADOS, AVG(OPAPROVECHADOS) AS OPAPROVECHADOS, AVG(OPLIBRES) AS OPLIBRES FROM" +
                                " (SELECT CONVERT(DATE, [Fecha]) AS FechaPart, [Fecha]" +
                                " , CASE WHEN CONVERT(VARCHAR(8), [Fecha], 108) BETWEEN  '05:00:00' and  '13:00:00' THEN 'M'" +
                                " WHEN CONVERT(VARCHAR(8), [Fecha], 108) BETWEEN '13:00:00' and '21:00:00' THEN 'T'" +
                                " ELSE 'N' END AS TURNO, [OPLOGUEADOS],[OPASIGNADOS],[OPAPROVECHADOS],[OPLIBRES], [CAL],[ENC1],[ENC2] FROM[SMARTH_DB].[dbo].[KPI_Aprovechamiento]" +
                                " WHERE[OPLOGUEADOS] > 0 and convert(varchar, [Fecha], 23) like '"+Fecha+"%'" +
                                " ) BMS" +
                            " LEFT JOIN(SELECT FechaPart, TURNO, COUNT(NoP) as OPE" +
                                " FROM(SELECT[NoP], CONVERT(DATE, [Fecha]) AS FechaPart" +
                                " , CASE WHEN CONVERT(VARCHAR(8), [Fecha], 108) BETWEEN  '05:00:00' and  '13:00:00' THEN 'M'" +
                                " WHEN CONVERT(VARCHAR(8), [Fecha], 108) BETWEEN '13:00:00' and '21:00:00' THEN 'T'" +
                                " ELSE 'N' END AS TURNO, TipoOperario, OP.Operario" +
                                " FROM[SMARTH_DB].[dbo].[AUX_Marcajes_Reloj] MC" +
                            " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] OP ON MC.NoP = OP.Id" +
                            " where Fichaje = 'Entrando' AND TipoOperario LIKE 'OPERAR%') FIC" +
                            " GROUP BY FechaPart, TURNO) RLJ ON BMS.FechaPart = RLJ.FechaPart and BMS.TURNO = RLJ.TURNO" +
                            " GROUP BY DATEPART(wk, [Fecha])" +
                            " order by SEMANA desc";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_GP12.Close();
                return ds;
            }
            catch (Exception ex)
            {
                cnn_GP12.Close();
                return null;
            }
        }



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
        } //calcular en bound

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

        public DataSet AprovechamientoOperarioSemanasMTN(string Fecha)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT *"+ 
                             " FROM(SELECT DATEPART(wk, [Fecha]) as SEMANA"+
                                        " , MIN([Fecha]) AS INICIO" +
                                        " , AVG([OPLOGUEADOS]) AS MANOPLOGUEADOS" +
                                        " , AVG([OPASIGNADOS]) AS MANOPASIGNADOS" +
                                        " , AVG([OPAPROVECHADOS]) AS MANOPAPROVECHADOS" +
                                        " , AVG([OPLIBRES]) AS MANOPLIBRES" +
                                    " FROM[SMARTH_DB].[dbo].[KPI_Aprovechamiento]" +
                                    " WHERE CONVERT(VARCHAR(8), [Fecha], 108) BETWEEN  '05:50:00' and  '13:50:00' AND convert(varchar, Fecha, 23) like '"+Fecha+ "%'  AND OPLOGUEADOS > 0" +
                                    " GROUP BY DATEPART(wk, [Fecha])) A" +
                             " LEFT JOIN(SELECT DATEPART(wk, [Fecha]) as SEMANA" +
                                    " , AVG([OPLOGUEADOS]) AS TAROPLOGUEADOS" +
                                    " , AVG([OPASIGNADOS]) AS TAROPASIGNADOS" +
                                    " , AVG([OPAPROVECHADOS]) AS TAROPAPROVECHADOS" +
                                    " , AVG([OPLIBRES]) AS TAROPLIBRES" +
                                  " FROM[SMARTH_DB].[dbo].[KPI_Aprovechamiento]" +
                                  " WHERE CONVERT(VARCHAR(8), [Fecha], 108) BETWEEN  '13:55:00' and  '21:55:00' AND convert(varchar, Fecha, 23) like '" + Fecha + "%'  AND OPLOGUEADOS > 0" +
                                  " GROUP BY DATEPART(wk, [Fecha])) B ON A.SEMANA = B.SEMANA" +
                      " LEFT JOIN(SELECT DATEPART(wk, [Fecha]) as SEMANA" +
                                  " , AVG([OPLOGUEADOS]) AS NOCOPLOGUEADOS" +
                                  " , AVG([OPASIGNADOS]) AS NOCOPASIGNADOS" +
                                  " , AVG([OPAPROVECHADOS]) AS NOCOPAPROVECHADOS" +
                                  " , AVG([OPLIBRES]) AS NOCOPLIBRES" +
                              " FROM[SMARTH_DB].[dbo].[KPI_Aprovechamiento]" +
                              " WHERE((CONVERT(VARCHAR(8), [Fecha], 108) BETWEEN  '22:00:00' and  '23:55:00') OR(CONVERT(VARCHAR(8), [Fecha], 108) BETWEEN  '00:00:00' and  '05:55:00')) AND convert(varchar, Fecha, 23) like '" + Fecha + "%'  AND OPLOGUEADOS > 0" +
                              " GROUP BY DATEPART(wk, [Fecha]))C ON A.SEMANA = C.SEMANA" +
                              " ORDER BY A.SEMANA DESC";
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
                string sql = "SELECT S.YEAR, S.WEEK, CASE WHEN S.INICIADAS='' THEN 0 ELSE S.INICIADAS END AS INICIADAS, CASE WHEN C.CONFORMES='' THEN 0 ELSE C.CONFORMES END AS CONFORMES, CASE WHEN NC.CONDICIONADAS='' THEN 0 ELSE NC.CONDICIONADAS END AS CONDICIONADAS" +
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
                string sql = "select AÑO, Cambiador, Operario, NUMORDENES, CambiadorLiberado, CAST(CAST (CambiadorLiberado AS FLOAT)/NUMORDENES AS numeric(36,2)) AS PORCENTAJE" +
                                   " from(SELECT Cambiador, count(Calidad) AS NUMORDENES, SUM(CASE WHEN CambiadorLiberado = 0 THEN 0 ELSE 1 END) AS CambiadorLiberado, DATENAME(YEAR,[FechaApertura]) AS AÑO" +
                                   " FROM[SMARTH_DB].[dbo].[LIBERACION_Ficha]" +
                                   " where Maquina <> 'FOAM' AND Maquina<> 'BSH' AND Maquina<> 'HOUS' AND Maquina<> 'WTOP'" +
                                   " group by Cambiador, DATENAME(YEAR,[FechaApertura])) E" +
                                   " LEFT JOIN[SMARTH_DB].[dbo].[AUX_TablaOperarios] A on E.Cambiador = A.Id" +
                                   " where Cambiador<> 0 AND AÑO = '"+año+"' order by PORCENTAJE desc";
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
                string sql = "select AÑO, Encargado, Operario, NUMORDENES, ProduccionLIBERADO, CAST(CAST (ProduccionLIBERADO AS FLOAT)/NUMORDENES AS numeric(36,2)) AS PORCENTAJE" +
                                " from(SELECT Encargado, count(Calidad) AS NUMORDENES, SUM(CASE WHEN ProduccionLIBERADO = 0 THEN 0 ELSE 1 END) AS ProduccionLIBERADO, DATENAME(YEAR,[FechaApertura]) AS AÑO" +
                                " FROM[SMARTH_DB].[dbo].[LIBERACION_Ficha]" +
                                 " where Maquina <> 'FOAM' AND Maquina<> 'BSH' AND Maquina<> 'HOUS' AND Maquina<> 'WTOP'" +
                                " group by Encargado, DATENAME(YEAR,[FechaApertura])) E" +
                                " LEFT JOIN[SMARTH_DB].[dbo].[AUX_TablaOperarios] A on E.Encargado = A.Id" +
                                " where Encargado<> 0 AND AÑO = '"+año+"' order by PORCENTAJE desc";
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
                string sql = "select AÑO, Calidad, Operario, NUMORDENES, CALIDADLIBERADO, CAST(CAST (CALIDADLIBERADO AS FLOAT)/NUMORDENES AS numeric(36,2)) AS PORCENTAJE"+
                                   " from(SELECT Calidad, count(Calidad) AS NUMORDENES, SUM(CASE WHEN CalidadLiberado = 0 THEN 0 ELSE 1 END) AS CALIDADLIBERADO, DATENAME(YEAR,[FechaApertura]) AS AÑO" +
                                   " FROM[SMARTH_DB].[dbo].[LIBERACION_Ficha]" +
                                    " where Maquina <> 'FOAM' AND Maquina<> 'BSH' AND Maquina<> 'HOUS' AND Maquina<> 'WTOP'" +
                                   " group by Calidad, DATENAME(YEAR,[FechaApertura])) E" +
                                   " LEFT JOIN[SMARTH_DB].[dbo].[AUX_TablaOperarios] A on E.Calidad = A.Id" +
                                   " where Calidad<> 0 AND AÑO = '"+año+"' order by PORCENTAJE desc";
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

        public DataSet KPILiberacionesDepartamentoPRODUCCION(string año)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT AB.AÑO, AB.mes, CASE WHEN AB.ABIERTAS IS NULL THEN 0 ELSE AB.ABIERTAS END AS ABIERTAS, CASE WHEN OK.OK IS NULL THEN 0 ELSE OK.OK END AS OK, CASE WHEN CON.CONDICIONADAS IS NULL THEN 0 ELSE CON.CONDICIONADAS END AS CONDICIONADAS, CASE WHEN NOL.SINLIBERAR IS NULL THEN 0 ELSE NOL.SINLIBERAR END AS SINLIBERAR" +
                                        " FROM(SELECT DATENAME(YEAR,[FechaApertura]) AS AÑO, DATENAME(month,[FechaApertura]) as mes, MONTH([FechaApertura]) AS MESNUM, COUNT([ProduccionLiberado]) as ABIERTAS" +
                                        " FROM[SMARTH_DB].[dbo].[LIBERACION_Ficha]" +
                                         " where Maquina <> 'FOAM' AND Maquina<> 'BSH' AND Maquina<> 'HOUS' AND Maquina<> 'WTOP'" +
                                        " GROUP BY DATENAME(YEAR,[FechaApertura]), DATENAME(month,[FechaApertura]), MONTH([FechaApertura])) AB" +
                                " LEFT JOIN(SELECT DATENAME(YEAR,[FechaApertura]) AS AÑO, DATENAME(month,[FechaApertura]) as mes, MONTH([FechaApertura]) AS MESNUM, COUNT([ProduccionLiberado]) as SINLIBERAR" +
                                        " FROM[SMARTH_DB].[dbo].[LIBERACION_Ficha] WHERE[ProduccionLiberado] = 0  AND Maquina<> 'FOAM' AND Maquina<> 'BSH' AND Maquina<> 'HOUS' AND Maquina<> 'WTOP'" +
                                        " GROUP BY DATENAME(YEAR,[FechaApertura]), DATENAME(month,[FechaApertura]), MONTH([FechaApertura])) NOL ON AB.AÑO = NOL.AÑO AND AB.MESNUM = NOL.MESNUM" +
                                " LEFT JOIN(SELECT DATENAME(YEAR,[FechaApertura]) AS AÑO, DATENAME(month,[FechaApertura]) as mes, MONTH([FechaApertura]) AS MESNUM, COUNT([ProduccionLiberado]) as CONDICIONADAS" +
                                        " FROM[SMARTH_DB].[dbo].[LIBERACION_Ficha] WHERE[ProduccionLiberado] = 1  AND Maquina<> 'FOAM' AND Maquina<> 'BSH' AND Maquina<> 'HOUS' AND Maquina<> 'WTOP'" +
                                        " GROUP BY DATENAME(YEAR,[FechaApertura]), DATENAME(month,[FechaApertura]), MONTH([FechaApertura])) CON ON AB.AÑO = CON.AÑO AND AB.MESNUM = CON.MESNUM" +
                                " LEFT JOIN(SELECT DATENAME(YEAR,[FechaApertura]) AS AÑO, DATENAME(month,[FechaApertura]) as mes, MONTH([FechaApertura]) AS MESNUM, COUNT([ProduccionLiberado]) as OK" +
                                        " FROM[SMARTH_DB].[dbo].[LIBERACION_Ficha] WHERE[ProduccionLiberado] = 2  AND Maquina<> 'FOAM' AND Maquina<> 'BSH' AND Maquina<> 'HOUS' AND Maquina<> 'WTOP'" +
                                        " GROUP BY DATENAME(YEAR,[FechaApertura]), DATENAME(month,[FechaApertura]), MONTH([FechaApertura])) OK ON AB.AÑO = OK.AÑO AND AB.MESNUM = OK.MESNUM" +
                                " WHERE AB.AÑO = '" + año + "'" +
                                " order by AB.MESNUM";
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

        public DataSet KPILiberacionesDepartamentoCAMBIADOR(string año)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT AB.AÑO, AB.mes, CASE WHEN AB.ABIERTAS IS NULL THEN 0 ELSE AB.ABIERTAS END AS ABIERTAS, CASE WHEN OK.OK IS NULL THEN 0 ELSE OK.OK END AS OK, CASE WHEN CON.CONDICIONADAS IS NULL THEN 0 ELSE CON.CONDICIONADAS END AS CONDICIONADAS, CASE WHEN NOL.SINLIBERAR IS NULL THEN 0 ELSE NOL.SINLIBERAR END AS SINLIBERAR" + " FROM(SELECT DATENAME(YEAR,[FechaApertura]) AS AÑO, DATENAME(month,[FechaApertura]) as mes, MONTH([FechaApertura]) AS MESNUM, COUNT([CambiadorLiberado]) as ABIERTAS" +
                                        " FROM[SMARTH_DB].[dbo].[LIBERACION_Ficha]" +
                                         " where Maquina <> 'FOAM' AND Maquina<> 'BSH' AND Maquina<> 'HOUS' AND Maquina<> 'WTOP'" +
                                        " GROUP BY DATENAME(YEAR,[FechaApertura]), DATENAME(month,[FechaApertura]), MONTH([FechaApertura])) AB" +
                                " LEFT JOIN(SELECT DATENAME(YEAR,[FechaApertura]) AS AÑO, DATENAME(month,[FechaApertura]) as mes, MONTH([FechaApertura]) AS MESNUM, COUNT([CambiadorLiberado]) as SINLIBERAR" +
                                        " FROM[SMARTH_DB].[dbo].[LIBERACION_Ficha] WHERE[CambiadorLiberado] = 0  AND Maquina<> 'FOAM' AND Maquina<> 'BSH' AND Maquina<> 'HOUS' AND Maquina<> 'WTOP'" +
                                        " GROUP BY DATENAME(YEAR,[FechaApertura]), DATENAME(month,[FechaApertura]), MONTH([FechaApertura])) NOL ON AB.AÑO = NOL.AÑO AND AB.MESNUM = NOL.MESNUM" +
                                " LEFT JOIN(SELECT DATENAME(YEAR,[FechaApertura]) AS AÑO, DATENAME(month,[FechaApertura]) as mes, MONTH([FechaApertura]) AS MESNUM, COUNT([CambiadorLiberado]) as CONDICIONADAS" +
                                        " FROM[SMARTH_DB].[dbo].[LIBERACION_Ficha] WHERE[CambiadorLiberado] = 1  AND Maquina<> 'FOAM' AND Maquina<> 'BSH' AND Maquina<> 'HOUS' AND Maquina<> 'WTOP'" +
                                        " GROUP BY DATENAME(YEAR,[FechaApertura]), DATENAME(month,[FechaApertura]), MONTH([FechaApertura])) CON ON AB.AÑO = CON.AÑO AND AB.MESNUM = CON.MESNUM" +
                                " LEFT JOIN(SELECT DATENAME(YEAR,[FechaApertura]) AS AÑO, DATENAME(month,[FechaApertura]) as mes, MONTH([FechaApertura]) AS MESNUM, COUNT([CambiadorLiberado]) as OK" +
                                        " FROM[SMARTH_DB].[dbo].[LIBERACION_Ficha] WHERE[CambiadorLiberado] = 2  AND Maquina<> 'FOAM' AND Maquina<> 'BSH' AND Maquina<> 'HOUS' AND Maquina<> 'WTOP'" +
                                        " GROUP BY DATENAME(YEAR,[FechaApertura]), DATENAME(month,[FechaApertura]), MONTH([FechaApertura])) OK ON AB.AÑO = OK.AÑO AND AB.MESNUM = OK.MESNUM" +
                                " WHERE AB.AÑO = '"+ año + "'" +
                                " order by AB.MESNUM";
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

        public DataSet KPILiberacionesDepartamentoCALIDAD(string año)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT AB.AÑO, AB.mes, CASE WHEN AB.ABIERTAS IS NULL THEN 0 ELSE AB.ABIERTAS END AS ABIERTAS, CASE WHEN OK.OK IS NULL THEN 0 ELSE OK.OK END AS OK, CASE WHEN CON.CONDICIONADAS IS NULL THEN 0 ELSE CON.CONDICIONADAS END AS CONDICIONADAS, CASE WHEN NOL.SINLIBERAR IS NULL THEN 0 ELSE NOL.SINLIBERAR END AS SINLIBERAR" + " FROM(SELECT DATENAME(YEAR,[FechaApertura]) AS AÑO, DATENAME(month,[FechaApertura]) as mes, MONTH([FechaApertura]) AS MESNUM, COUNT([CalidadLiberado]) as ABIERTAS" +
                                        " FROM[SMARTH_DB].[dbo].[LIBERACION_Ficha]" +
                                         " where Maquina <> 'FOAM' AND Maquina<> 'BSH' AND Maquina<> 'HOUS' AND Maquina<> 'WTOP'" +
                                        " GROUP BY DATENAME(YEAR,[FechaApertura]), DATENAME(month,[FechaApertura]), MONTH([FechaApertura])) AB" +
                                " LEFT JOIN(SELECT DATENAME(YEAR,[FechaApertura]) AS AÑO, DATENAME(month,[FechaApertura]) as mes, MONTH([FechaApertura]) AS MESNUM, COUNT([CalidadLiberado]) as SINLIBERAR" +
                                        " FROM[SMARTH_DB].[dbo].[LIBERACION_Ficha] WHERE[CalidadLiberado] = 0  AND Maquina<> 'FOAM' AND Maquina<> 'BSH' AND Maquina<> 'HOUS' AND Maquina<> 'WTOP'" +
                                        " GROUP BY DATENAME(YEAR,[FechaApertura]), DATENAME(month,[FechaApertura]), MONTH([FechaApertura])) NOL ON AB.AÑO = NOL.AÑO AND AB.MESNUM = NOL.MESNUM" +
                                " LEFT JOIN(SELECT DATENAME(YEAR,[FechaApertura]) AS AÑO, DATENAME(month,[FechaApertura]) as mes, MONTH([FechaApertura]) AS MESNUM, COUNT([CalidadLiberado]) as CONDICIONADAS" +
                                        " FROM[SMARTH_DB].[dbo].[LIBERACION_Ficha] WHERE[CalidadLiberado] = 1  AND Maquina<> 'FOAM' AND Maquina<> 'BSH' AND Maquina<> 'HOUS' AND Maquina<> 'WTOP'" +
                                        " GROUP BY DATENAME(YEAR,[FechaApertura]), DATENAME(month,[FechaApertura]), MONTH([FechaApertura])) CON ON AB.AÑO = CON.AÑO AND AB.MESNUM = CON.MESNUM" +
                                " LEFT JOIN(SELECT DATENAME(YEAR,[FechaApertura]) AS AÑO, DATENAME(month,[FechaApertura]) as mes, MONTH([FechaApertura]) AS MESNUM, COUNT([CalidadLiberado]) as OK" +
                                        " FROM[SMARTH_DB].[dbo].[LIBERACION_Ficha] WHERE[CalidadLiberado] = 2  AND Maquina<> 'FOAM' AND Maquina<> 'BSH' AND Maquina<> 'HOUS' AND Maquina<> 'WTOP'" +
                                        " GROUP BY DATENAME(YEAR,[FechaApertura]), DATENAME(month,[FechaApertura]), MONTH([FechaApertura])) OK ON AB.AÑO = OK.AÑO AND AB.MESNUM = OK.MESNUM" +
                                " WHERE AB.AÑO = '" + año + "'" +
                                " order by AB.MESNUM";
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

        public DataSet KPILiberacionesDevuelveResultadosAuditorias(string cuestiones, string where)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT TOP (1000) aud.[Orden], aud.[Referencia],pro.Descripcion,aud.[Maquina],case when op.Operario is null then '-' else op.Operario end as CALIDAD, case when op2.Operario is null then '-' else op2.Operario end as CAMBIADOR, case when op3.Operario is null then '-' else op3.Operario end as ENCARGADO"+
                              ""+cuestiones+",[QXFeedbackCambiador],[QXFeedbackProduccion],[QXFeedbackCalidad]"+
                              " FROM [SMARTH_DB].[dbo].[LIBERACION_Auditoria] aud" +
                              " left join [SMARTH_DB].[dbo].[LIBERACION_Ficha] lib on aud.Orden = lib.Orden" +
                              " left join [SMARTH_DB].[dbo].[AUX_TablaOperarios] op on lib.Calidad = op.Id" +
                              " left join [SMARTH_DB].[dbo].[AUX_TablaOperarios] op2 on lib.Cambiador = op2.Id" +
                              " left join [SMARTH_DB].[dbo].[AUX_TablaOperarios] op3 on lib.Encargado = op3.Id" +
                              " left join [SMARTH_DB].[dbo].[AUX_TablaProductos] PRO on aud.Referencia = pro.Referencia" +
                              " where lib.Orden <> '' " + where + "" +
                              " order by orden desc";
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



        //**\\CONECTORES ANTIGUOS //**\\
        //CONECTORES GP12.ASPX

        public DataSet devuelve_lista_clientes()
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT RTRIM(Cliente) AS Cliente FROM [SMARTH_DB].[dbo].[AUX_TablaProductos] WHERE CLIENTE <> '' GROUP BY Cliente ORDER BY Cliente";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_GP12.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_bms.Close();
                return null;
            }
        }



        //CONECTORES KPIS GP12

        public bool LimpiarTablaHistoria()
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "DELETE FROM [CALIDAD].[dbo].[BMSHistoricoProducidas]";
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

        public bool lee_produccionhistorica_BMS()
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
                        insertar_historiaBMS(Convert.ToInt32(reader["YEA"]), Convert.ToInt32(reader["MES"]), Convert.ToDouble(reader["CANTIDAD"]));

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

        public bool insertar_historiaBMS(int periodo, int mes, double cantidad)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "INSERT INTO [CALIDAD].[dbo].[BMSHistoricoProducidas] (Periodo, Mes, Cantidad) VALUES " +
                                 "(" + periodo + "," + mes + "," + cantidad + ")";
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


    }
}