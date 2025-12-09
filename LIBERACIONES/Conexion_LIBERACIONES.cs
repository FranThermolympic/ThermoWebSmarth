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

namespace ThermoWeb.LIBERACIONES
{
    public class Conexion_LIBERACIONES
    {
        private readonly SqlConnection cnn_GP12 = new SqlConnection();
        private readonly OracleConnection cnn_bms = new OracleConnection();
        private readonly SqlConnection cnn_RPS = new SqlConnection();
        private readonly SqlConnection cnn_SMARTH = new SqlConnection();

        public Conexion_LIBERACIONES()
        {
            cnn_GP12.ConnectionString = ConfigurationManager.ConnectionStrings["area_rechazo"].ToString();
            cnn_bms.ConnectionString = ConfigurationManager.ConnectionStrings["BMS"].ToString();
            cnn_RPS.ConnectionString = ConfigurationManager.ConnectionStrings["RPS"].ToString();
            cnn_SMARTH.ConnectionString = ConfigurationManager.ConnectionStrings["SMARTH"].ToString();
        }
        //**\\HISTORICO LIBERACION//**\\
        public DataSet Devuelve_Historico_Liberaciones(string orden, string referencia, string maquina, string cliente, string molde, string cambiadorlib, string produclib, string callib, string camblibnom, string prodlibnom, string callibnom, string libnok)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT TOP (500) [Orden],l.[Referencia],p.Descripcion,[Maquina],p.Cliente,[CodMolde],[AccionLiberado]," +
                             "CASE WHEN L.CambiadorLiberado IS NULL THEN 'Pendiente' WHEN L.CambiadorLiberado = 0 THEN 'Pendiente' WHEN L.CambiadorLiberado = 1 THEN 'OK Condicionada' WHEN L.CambiadorLiberado = 2 THEN 'Liberada OK' END AS LIBERARCAMBIO," +
                             "CASE WHEN L.ProduccionLiberado IS NULL THEN 'Pendiente' WHEN L.ProduccionLiberado = 0 THEN 'Pendiente' WHEN L.ProduccionLiberado = 1 THEN 'OK Condicionada' WHEN L.ProduccionLiberado = 2 THEN 'Liberada OK' END AS LIBERARPRODUCCION," +
                             "CASE WHEN L.CalidadLiberado IS NULL THEN 'Pendiente' WHEN L.CalidadLiberado = 0 THEN 'Pendiente' WHEN L.CalidadLiberado = 1 THEN 'OK Condicionada' WHEN L.CalidadLiberado = 2 THEN 'Liberada OK' END AS LIBERARCALIDAD, l.NotasLiberado," +
                             "CASE WHEN L.AccionLiberado = 1 THEN '-' WHEN L.AccionLiberado = 2 THEN 'Con inspección 100%' WHEN L.AccionLiberado = 3 THEN 'Identificado unitario' WHEN L.AccionLiberado = 4 THEN 'Producción retenida' WHEN L.AccionLiberado = 5 THEN 'Otros' END AS ACCIONES" +
                             " FROM[SMARTH_DB].[dbo].[LIBERACION_Ficha] L" +
                             " left join [SMARTH_DB].[dbo].[AUX_TablaProductos] P on l.Referencia = p.Referencia" +
                             " where orden like '"+orden+"%' and l.Referencia like '"+referencia+"%' and Maquina like '"+maquina+"%' and Cliente like '"+cliente+"%' and Molde LIKE '"+molde+"%' AND L.CambiadorLiberado LIKE '"+cambiadorlib+"%' AND L.ProduccionLiberado LIKE '"+produclib+"%' AND CalidadLiberado LIKE '"+callib+"%'"+callibnom+""+prodlibnom+""+callibnom+""+libnok+"" +
                             " order by Orden desc";
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

        public DataSet Devuelve_paros4H() //NUEVO PROBAR
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT C_MACHINE_ID, (SYSDATE - C_DATE)/ 0.044 AS STOPTIME, C_LOG_ID, C_FINAL_STOPCODE, S.C_SHORT_DESCR FROM ELOG2.T_ELOG_STOPINFO LEFT JOIN PCMS.T_STPTABLE_STPCODE S ON C_FINAL_STOPCODE = S.C_STOPCODE_NR WHERE C_NEXT_ID IS NULL AND C_FINAL_STOPCODE<> 0 AND S.C_STOPTABLE_ID = 2 AND S.C_STOPGROUPS_ID <> 'D001' AND S.C_STOPGROUPS_ID <> 'D002' AND((SYSDATE - C_DATE) / 0.044) > 4";
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
        public DataSet Devuelve_Maquinas_bms()
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT trim(C_ID) as C_ID FROM T_MACHINES  WHERE C_WORKCENTRE_ID <> 'VIRTUAL'  ORDER BY C_ID ASC";
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
        public DataSet Devuelve_setlista_responsablesSMARTH() //SMARTH
        {
            try
            {
                cnn_GP12.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT [Id],[Nombre] AS PAprobado FROM [SMARTH_DB].[dbo].[AUX_Personal_Mandos] WHERE (Departamento = '-' OR Departamento = 'INGENIERIA') AND OPActivo = 1", cnn_GP12);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                cnn_GP12.Close();
                DataSet dt = new DataSet();
                dataAdapter.Fill(dt);
                return dt;
            }
            catch (Exception)
            {
                cnn_GP12.Close();
                return null;
            }
        }


        public DataSet Devuelve_mandos_intermedios_SMARTH()
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                //string sql = "SELECT [Referencia],[Descripcion] FROM [SMARTH_DB].[dbo].[AUX_TablaProductos] where Molde = " + molde + " order by Referencia";
                string sql = "SELECT [Nombre],[OPActivo],[Departamento] FROM[SMARTH_DB].[dbo].[AUX_Personal_Mandos] where OPActivo = 1 order by Nombre asc";
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
        } //SMARTH

        public int devuelve_mandos_intermedios_SMARTH(string responsable)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT IdNUMOP FROM [SMARTH_DB].[dbo].[AUX_Personal_Mandos] where Nombre = '" + responsable + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_GP12.Close();
                return Convert.ToInt16(ds.Tables[0].Rows[0]["IdNUMOP"].ToString());
            }
            catch (Exception)
            {
                cnn_GP12.Close();
                return 1;
            }
        }

        //**\\CONECTORES ESTADO DE LIBERACIÓN//**\\
        public DataSet devuelve_OrdenesProduciendoBMS()
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT C_MACHINE_ID, C_ID, C_PARENT_ID, C_PARINDEX, C_TOOL_ID, C_PRODUCT_ID, C_PRODLONGDESCR, C_CUSTOMER, C_ACTSTARTDATE FROM PCMS.T_JOBS WHERE C_SEQNR = 0 ORDER BY C_MACHINE_ID, C_PARINDEX";
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataSet ordenes = new DataSet();
                query.Fill(ordenes);
                cnn_bms.Close();
                return ordenes;
            }
            catch (Exception)
            {
                cnn_bms.Close();
                return null;
            }
        }
        public bool LimpiarOrdenesProduciendoBMS()
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "DELETE FROM [CALIDAD].[dbo].[BMSEnProduccion]";
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
        public bool leer_OrdenesProduciendoBMS()
        {

            try
            {
                //string sql = "SELECT C_MACHINE_ID, C_ID, C_PARENT_ID, C_PARINDEX, C_TOOL_ID, C_PRODUCT_ID, C_ACTSTARTDATE FROM PCMS.T_JOBS WHERE C_SEQNR = 0 ORDER BY C_MACHINE_ID, C_PARINDEX";
                string sql = "SELECT J.C_MACHINE_ID, J.C_ID, J.C_PARENT_ID, J.C_PARINDEX, J.C_TOOL_ID, J.C_PRODUCT_ID, J.C_ACTSTARTDATE, S.ESTADO2 FROM PCMS.T_JOBS J"+
                             " LEFT JOIN(select C_MACHINE_ID AS MAQUINA, CASE CAST(E.C_FINAL_RUNSTATE AS VARCHAR2(30)) WHEN '1' THEN 'En marcha' ELSE CAST(S.C_SHORT_DESCR AS VARCHAR2(30)) END AS ESTADO2, S.C_STOPGROUPS_ID from ELOG2.T_ELOG_STOPINFO E LEFT JOIN PCMS.T_STPTABLE_STPCODE S ON E.C_FINAL_STOPCODE = S.C_STOPCODE_NR AND C_STOPTABLE_ID = 2"+
                             " where C_NEXT_ID is null order by c_machine_id asc) S ON J.C_MACHINE_ID = S.MAQUINA WHERE C_SEQNR = 0 ORDER BY C_MACHINE_ID, C_PARINDEX";
                    
                    
                OracleCommand cmd = new OracleCommand(sql, cnn_bms);
                cnn_bms.Open();
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        insertar_orden_produciendo(reader["C_MACHINE_ID"].ToString(), reader["C_ID"].ToString(), reader["C_PARENT_ID"].ToString(), Convert.ToInt32(reader["C_PARINDEX"]), reader["C_TOOL_ID"].ToString(), reader["C_PRODUCT_ID"].ToString(), reader["C_ACTSTARTDATE"].ToString(), reader["ESTADO2"].ToString());

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
        public bool insertar_orden_produciendo(string maquina, string orden, string ordenmaestra, int indice, string molde, string producto, string HoraCarga, string Estado)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "INSERT INTO [CALIDAD].[dbo].[BMSEnProduccion] (Maquina,Orden,OrdenMaestra,Linea,Molde,Referencia,HoraCarga,EstadoMaquina) VALUES " +
                                 "('" + maquina + "','" + orden + "','" + ordenmaestra + "'," + indice + ",'" + molde + "','" + producto + "', '" + HoraCarga + "', '"+Estado+"')";
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


        //**\\CONECTORES LIBERACION SERIE//**\\
        public DataSet devuelve_horasXReferenciaXOperario(int MOLDE, int operario)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT MAX (C_TOOL) AS MOLDE, MAX (C_PRODUCT) AS PRODUCTO, MAX (C_PRODUCTDESCR) as DESCRIPCION, C_CLOCKNUMBER, C_OPERATORNAME, SUM(TRUNC((C_ACTUALAVAILABLETIME/60),2)) AS TIEMPOHORAS, SUM (C_ACTUALAVAILABLETIME) AS HORAS" +
                             " FROM HIS.T_HISOPERATORS WHERE C_RUNTIME > 0 AND C_TOOL = "+MOLDE+" AND C_CLOCKNUMBER = "+ operario +" GROUP BY C_CLOCKNUMBER, C_OPERATORNAME ORDER BY TIEMPOHORAS DESC";
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

        public DataSet Devuelve_reparaciones_molde(int MOLDE)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                //string sql = "SELECT [IdReparacionMolde],[IDMoldes],[MotivoAveria],[Reparacion],[Terminado],[Revisado],[RevisadoNOK] FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes] where IDMoldes = " + MOLDE+" order by IdReparacionMolde desc";
                string sql = "SELECT[IdReparacionMolde],[IDMoldes],[MotivoAveria],[Reparacion], Terminado,[Revisado],[RevisadoNOK], A.Texto FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes] T LEFT JOIN [SMARTH_DB].[dbo].[MANTENIMIENTO_Aux_Estados_Reparacion] A ON CONVERT(VARCHAR,[Terminado]) + CONVERT(VARCHAR,[Revisado]) + CONVERT(VARCHAR,[RevisadoNOK]) = A.IdEstado where IDMoldes = " + MOLDE + " order by IdReparacionMolde desc";
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

        public DataSet Devuelve_reparaciones_maquina(string MAQUINA)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT [IdMantenimiento],T.IdMaquinaCHAR,[MotivoAveria],[Reparacion],[Terminado],[Revisado],[RevisadoNOK], a.Texto" +
                    " FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas] M" +
                    " LEFT JOIN [SMARTH_DB].[dbo].[AUX_Lista_Máquinas] T ON M.IDMaquina = T.IdMaquina" +
                    " LEFT JOIN [SMARTH_DB].[dbo].[MANTENIMIENTO_Aux_Estados_Reparacion] A ON CONVERT(VARCHAR, [Terminado]) +CONVERT(VARCHAR,[Revisado]) + CONVERT(VARCHAR,[RevisadoNOK]) = A.IdEstado WHERE IdMaquinaCHAR = '" + MAQUINA + "'" +
                    " order by IdMantenimiento desc";
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
        //CARGAINICIAL de campos en liberación de serie
        //LECTURA INICIAL DE OPERARIOS
        public DataSet devuelve_detalles_orden(string orden)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT C_MACHINE_ID, C_ID, C_PARENT_ID, C_PARINDEX, C_TOOL_ID, C_PRODUCT_ID, C_PRODLONGDESCR, C_CUSTOMER, C_ACTSTARTDATE FROM PCMS.T_JOBS WHERE C_ID = '"+orden+"'";
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataSet ordenes = new DataSet();
                query.Fill(ordenes);
                cnn_bms.Close();
                return ordenes;
            }
            catch (Exception)
            {
                cnn_bms.Close();
                return null;
            }
        }

        public DataSet devuelve_detalles_orden_HIST(string orden)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT [Orden],O.[Referencia],P.Descripcion,[Maquina],[CodMolde],[FechaApertura] FROM [SMARTH_DB].[dbo].[LIBERACION_Ficha] O left join [SMARTH_DB].[dbo].[AUX_TablaProductos] P ON O.Referencia = P.Referencia WHERE Orden = '" + orden + "'";
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

        public DataSet devuelve_hijos_orden(string orden)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT C_ID, C_PRODUCT_ID, C_LONG_DESCR FROM PCMS.T_JOBS WHERE C_PARENT_ID = '" + orden + "' ORDER BY C_PARINDEX ";
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataSet ordenes = new DataSet();
                query.Fill(ordenes);
                cnn_bms.Close();
                return ordenes;
            }
            catch (Exception)
            {
                cnn_bms.Close();
                return null;
            }
        }
      
        public DataSet devuelve_calidadplanta_logueadoXMaquina(string maquina)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT E.C_NAME, E.C_CLOCKNO, L.C_OPERATORTYPE FROM OPER.T_LOGS L, PCMS.T_EMPLOYEES E, PCMS.T_OPERATORS O, PCMS.T_OPERATORTYPES T, PCMS.T_MACHINES M, PCMS.T_WORKCENTRES W WHERE L.C_REFLOG_ID IS NULL AND (L.C_TYPE = 0 OR L.C_TYPE = 2) AND L.C_OPERATOR = E.C_ID AND E.C_ID = O.C_ID AND L.C_OPERATORTYPE  = T.C_ID AND L.C_MACHINE = M.C_ID AND L.C_MACHINE = '"+maquina+"' AND L.C_OPERATORTYPE = 'CALIDADPLANTA' AND M.C_WORKCENTRE_ID = W.C_ID (+) ORDER BY L.C_OPERATORTYPE";
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataSet ordenes = new DataSet();
                query.Fill(ordenes);
                cnn_bms.Close();
                return ordenes;
            }
            catch (Exception)
            {
                cnn_bms.Close();
                return null;
            }
        }

        public DataSet devuelve_encargado_logueadoXMaquina(string maquina)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT E.C_NAME, E.C_CLOCKNO, L.C_OPERATORTYPE FROM OPER.T_LOGS L, PCMS.T_EMPLOYEES E, PCMS.T_OPERATORS O, PCMS.T_OPERATORTYPES T, PCMS.T_MACHINES M, PCMS.T_WORKCENTRES W WHERE L.C_REFLOG_ID IS NULL AND (L.C_TYPE = 0 OR L.C_TYPE = 2) AND L.C_OPERATOR = E.C_ID AND E.C_ID = O.C_ID AND L.C_OPERATORTYPE  = T.C_ID AND L.C_MACHINE = M.C_ID AND L.C_MACHINE = '" + maquina + "' AND L.C_OPERATORTYPE = 'ENCARGADO' AND M.C_WORKCENTRE_ID = W.C_ID (+) ORDER BY L.C_OPERATORTYPE";
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataSet ordenes = new DataSet();
                query.Fill(ordenes);
                cnn_bms.Close();
                return ordenes;
            }
            catch (Exception)
            {
                cnn_bms.Close();
                return null;
            }
        }

        public DataSet devuelve_cambiador_logueadoXMaquina(string maquina)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT E.C_NAME, E.C_CLOCKNO, L.C_OPERATORTYPE FROM OPER.T_LOGS L, PCMS.T_EMPLOYEES E, PCMS.T_OPERATORS O, PCMS.T_OPERATORTYPES T, PCMS.T_MACHINES M, PCMS.T_WORKCENTRES W WHERE L.C_REFLOG_ID IS NULL AND (L.C_TYPE = 0 OR L.C_TYPE = 2) AND L.C_OPERATOR = E.C_ID AND E.C_ID = O.C_ID AND L.C_OPERATORTYPE  = T.C_ID AND L.C_MACHINE = M.C_ID AND L.C_MACHINE = '" + maquina + "' AND L.C_OPERATORTYPE = 'TOOLCHG' AND M.C_WORKCENTRE_ID = W.C_ID (+) ORDER BY L.C_OPERATORTYPE";
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataSet ordenes = new DataSet();
                query.Fill(ordenes);
                cnn_bms.Close();
                return ordenes;
            }
            catch (Exception)
            {
                cnn_bms.Close();
                return null;
            }
        }

        public DataSet devuelve_operario_logueadoXMaquina(string maquina)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT E.C_NAME, E.C_CLOCKNO, L.C_OPERATORTYPE FROM OPER.T_LOGS L, PCMS.T_EMPLOYEES E, PCMS.T_OPERATORS O, PCMS.T_OPERATORTYPES T, PCMS.T_MACHINES M, PCMS.T_WORKCENTRES W WHERE L.C_REFLOG_ID IS NULL AND (L.C_TYPE = 0 OR L.C_TYPE = 2) AND L.C_OPERATOR = E.C_ID AND E.C_ID = O.C_ID AND L.C_OPERATORTYPE  = T.C_ID AND L.C_MACHINE = M.C_ID AND L.C_MACHINE = '" + maquina + "' AND L.C_OPERATORTYPE = 'OPERATOR' AND M.C_WORKCENTRE_ID = W.C_ID (+) ORDER BY L.C_OPERATORTYPE";
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataSet ordenes = new DataSet();
                query.Fill(ordenes);
                cnn_bms.Close();
                return ordenes;
            }
            catch (Exception)
            {
                cnn_bms.Close();
                return null;
            }
        }
        //LECTURA INICIAL DE MATERIAS PRIMAS Y COMPONENTES
        public DataSet devuelve_materiasprimasXReferencias(string referencia)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT M.C_ID, M.C_MATTYPE_ID, M.C_USERVALUE1, M.C_USERVALUE2, M.C_SHORT_DESCR, M.C_LONG_DESCR, M.C_REMARKS FROM T_RECIPE_X_MATERIAL R LEFT JOIN T_MATERIALS M ON R.C_MATERIAL_ID = M.C_ID WHERE C_RECIPE_ID = " + referencia + " AND (C_MATTYPE_ID = '15' OR C_MATTYPE_ID = '215' OR C_MATTYPE_ID = '155') ORDER BY C_ID";
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataSet ordenes = new DataSet();
                query.Fill(ordenes);
                cnn_bms.Close();
                return ordenes;
            }
            catch (Exception)
            {
                cnn_bms.Close();
                return null;
            }
        }

        public string devuelve_remark_materiasprimasBMS(string referencia)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT C_REMARKS FROM T_MATERIALS WHERE C_ID = '"+referencia+"'";
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataSet material = new DataSet();
                query.Fill(material);

                cnn_bms.Close();
                cnn_GP12.Close();
                if (material.Tables[0].Rows[0]["C_REMARKS"].ToString() != "")
                {
                    return material.Tables[0].Rows[0]["C_REMARKS"].ToString();
                }
                else
                {
                    return "-";
                }
                
            }
            catch (Exception)
            {
                cnn_bms.Close();
                return null;
            }
        }

        public DataSet devuelve_componentesXReferencias(string referencia)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT M.C_ID, M.C_MATTYPE_ID, M.C_USERVALUE1, M.C_USERVALUE2, M.C_SHORT_DESCR, M.C_LONG_DESCR, M.C_REMARKS FROM T_RECIPE_X_MATERIAL R LEFT JOIN T_MATERIALS M ON R.C_MATERIAL_ID = M.C_ID WHERE C_RECIPE_ID = " + referencia + " AND (C_MATTYPE_ID = '10' or C_MATTYPE_ID = '8') ORDER BY C_ID";
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataSet ordenes = new DataSet();
                query.Fill(ordenes);
                cnn_bms.Close();
                return ordenes;
            }
            catch (Exception)
            {
                cnn_bms.Close();
                return null;
            }
        }


        //LECTURA INICIAL DE PARÁMETROS DESDE REFERENCIA
        public DataSet leerFicha(int referencia, int maquina)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT * FROM [SMARTH_DB].[dbo].[PARAMETROS_Ficha] a" +
                            " INNER JOIN (SELECT Referencia, Maquina, MAX(Version) AS Version from [SMARTH_DB].[dbo].[PARAMETROS_Ficha] GROUP BY Referencia, Maquina) b" +
                            " on a.Referencia = b.Referencia and a.Maquina = b.Maquina and a.Version = b.Version" +
                            " WHERE a.Referencia = " + referencia + " AND a.Maquina = " + maquina + "";
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

        public DataSet leerTempCilindro(int referencia, int maquina)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT * FROM [SMARTH_DB].[dbo].[PARAMETROS_TempCilindro] a" +
                    " INNER JOIN (SELECT Referencia, Maquina, MAX(Version) AS Version from [SMARTH_DB].[dbo].[PARAMETROS_TempCilindro] GROUP BY Referencia, Maquina) b" +
                    " on a.Referencia = b.Referencia and a.Maquina = b.Maquina and a.Version = b.Version" +
                    " WHERE a.Referencia = " + referencia + " AND a.Maquina = " + maquina + "";
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

        public DataSet leerTempCamCaliente(int referencia, int maquina)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT * FROM [SMARTH_DB].[dbo].[PARAMETROS_TempCamCaliente] a" +
                   " INNER JOIN (SELECT Referencia, Maquina, MAX(Version) AS Version from [SMARTH_DB].[dbo].[PARAMETROS_TempCamCaliente] GROUP BY Referencia, Maquina) b" +
                   " on a.Referencia = b.Referencia and a.Maquina = b.Maquina and a.Version = b.Version" +
                   " WHERE a.Referencia = " + referencia + " AND a.Maquina = " + maquina + "";
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

        public DataSet leerInyeccion(int referencia, int maquina)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT * FROM [SMARTH_DB].[dbo].[PARAMETROS_Inyeccion] a" +
                      " INNER JOIN (SELECT Referencia, Maquina, MAX(Version) AS Version from [SMARTH_DB].[dbo].[PARAMETROS_Inyeccion] GROUP BY Referencia, Maquina) b" +
                      " on a.Referencia = b.Referencia and a.Maquina = b.Maquina and a.Version = b.Version" +
                      " WHERE a.Referencia = " + referencia + " AND a.Maquina = " + maquina + "";    
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

        public DataSet leerPostpresion(int referencia, int maquina)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT * FROM [SMARTH_DB].[dbo].[PARAMETROS_Postpresion] a" +
                            " INNER JOIN (SELECT Referencia, Maquina, MAX(Version) AS Version from [SMARTH_DB].[dbo].[PARAMETROS_Postpresion] GROUP BY Referencia, Maquina) b" +
                            " on a.Referencia = b.Referencia and a.Maquina = b.Maquina and a.Version = b.Version" +
                            " WHERE a.Referencia = " + referencia + " AND a.Maquina = " + maquina + "";  
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

        public DataSet leerTolerancias(int referencia, int maquina)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT * FROM [SMARTH_DB].[dbo].[PARAMETROS_Tolerancias] a" +
                            " INNER JOIN (SELECT Referencia, Maquina, MAX(Version) AS Version from [SMARTH_DB].[dbo].[PARAMETROS_Tolerancias] GROUP BY Referencia, Maquina) b" +
                            " on a.Referencia = b.Referencia and a.Maquina = b.Maquina and a.Version = b.Version" +
                            " WHERE a.Referencia = " + referencia + " AND a.Maquina = " + maquina + "";  
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

        public DataSet leerAtemperado(int referencia, int maquina)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT * FROM [SMARTH_DB].[dbo].[PARAMETROS_AtemperadoV1] a" +
                            " INNER JOIN (SELECT Referencia, Maquina, MAX(Version) AS Version from [SMARTH_DB].[dbo].[PARAMETROS_AtemperadoV1] GROUP BY Referencia, Maquina) b" +
                            " on a.Referencia = b.Referencia and a.Maquina = b.Maquina and a.Version = b.Version" +
                            " WHERE a.Referencia = " + referencia + " AND a.Maquina = " + maquina + "";  
                
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

        public string devuelve_tipo_atemperado(int id)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT CircuitosABRV FROM [SMARTH_DB].[dbo].[PARAMETROS_AuxAtemperado] where IdAtemp = " + id;
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                if (ds.Tables[0].Rows[0]["CircuitosABRV"].ToString() != "")
                {
                    return ds.Tables[0].Rows[0]["CircuitosABRV"].ToString();
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
 
        //LECTURA INICIAL DE PARÁMETROS DESDE MOLDE
     
        public DataSet leerFichaMOLDE(int molde, int maquina)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT * FROM [SMARTH_DB].[dbo].[PARAMETROS_Ficha] a" +
                " INNER JOIN(SELECT CodMolde, Maquina, MAX(Version) AS Version"+
                " from [SMARTH_DB].[dbo].[PARAMETROS_Ficha]" +
                " where Maquina like '" + maquina + "%'and CodMolde like '" + molde + "%'" +
                " GROUP BY CodMolde, Maquina) b on a.CodMolde = b.CodMolde and a.Maquina = b.Maquina and a.Version = b.Version";
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

        /*public DataSet leerTempCilindroMOLDEOBSO(int molde, int maquina)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT * FROM PARAMETROS.dbo.TempCilindro a" +
                    " INNER JOIN (SELECT Referencia, Maquina, MAX(Version) AS Version from PARAMETROS.dbo.TempCilindro GROUP BY Referencia, Maquina) b" +
                    " on a.Referencia = b.Referencia and a.Maquina = b.Maquina and a.Version = b.Version" +
                    " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] P ON A.Referencia = P.Referencia" +
                    " WHERE P.Molde = " + molde + " AND a.Maquina = " + maquina + "";
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

        public DataSet leerTempCilindroMOLDE(int molde, int maquina)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT * FROM [SMARTH_DB].[dbo].[PARAMETROS_TempCilindro] a" +
                    " INNER JOIN (SELECT Referencia, Maquina, MAX(Version) AS Version" +
                    " from [SMARTH_DB].[dbo].[PARAMETROS_Ficha]" +
                    " where Maquina like '" + maquina + "%'and CodMolde like '" + molde + "%'"+
                    " GROUP BY Referencia, Maquina) b on a.Referencia = b.Referencia and a.Maquina = b.Maquina and a.Version = b.Version";
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

        public DataSet leerTempCamCalienteMOLDE(int molde, int maquina)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                //string sql = "SELECT * FROM PARAMETROS.dbo.TempCamCaliente a" +
                //   " INNER JOIN (SELECT Referencia, Maquina, MAX(Version) AS Version from PARAMETROS.dbo.TempCamCaliente GROUP BY Referencia, Maquina) b" +
                //   " on a.Referencia = b.Referencia and a.Maquina = b.Maquina and a.Version = b.Version" +
                //   " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] P ON A.Referencia = P.Referencia" +
                //   " WHERE P.Molde = " + molde + " AND a.Maquina = " + maquina + "";
                string sql = "SELECT * FROM [SMARTH_DB].[dbo].[PARAMETROS_TempCamCaliente] a" +
                    " INNER JOIN (SELECT Referencia, Maquina, MAX(Version) AS Version" +
                    " from [SMARTH_DB].[dbo].[PARAMETROS_Ficha]" +
                    " where Maquina like '" + maquina + "%'and CodMolde like '" + molde + "%'" +
                    " GROUP BY Referencia, Maquina) b on a.Referencia = b.Referencia and a.Maquina = b.Maquina and a.Version = b.Version";
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

        public DataSet leerInyeccionMOLDE(int molde, int maquina)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                /*string sql = "SELECT * FROM PARAMETROS.dbo.Inyeccion a" +
                      " INNER JOIN (SELECT Referencia, Maquina, MAX(Version) AS Version from PARAMETROS.dbo.Inyeccion GROUP BY Referencia, Maquina) b" +
                      " on a.Referencia = b.Referencia and a.Maquina = b.Maquina and a.Version = b.Version" +
                      " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] P ON A.Referencia = P.Referencia" +
                      " WHERE P.Molde = " + molde + " AND a.Maquina = " + maquina + "";*/
                string sql = "SELECT * FROM [SMARTH_DB].[dbo].[PARAMETROS_Inyeccion] a" +
                    " INNER JOIN (SELECT Referencia, Maquina, MAX(Version) AS Version" +
                    " from [SMARTH_DB].[dbo].[PARAMETROS_Ficha]" +
                    " where Maquina like '" + maquina + "%'and CodMolde like '" + molde + "%'" +
                    " GROUP BY Referencia, Maquina) b on a.Referencia = b.Referencia and a.Maquina = b.Maquina and a.Version = b.Version";
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

        public DataSet leerPostpresionMOLDE(int molde, int maquina)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                /*string sql = "SELECT * FROM PARAMETROS.dbo.Postpresion a" +
                            " INNER JOIN (SELECT Referencia, Maquina, MAX(Version) AS Version from PARAMETROS.dbo.Postpresion GROUP BY Referencia, Maquina) b" +
                            " on a.Referencia = b.Referencia and a.Maquina = b.Maquina and a.Version = b.Version" +
                            " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] P ON A.Referencia = P.Referencia" +
                            " WHERE P.Molde = " + molde + " AND a.Maquina = " + maquina + "";*/
                string sql = "SELECT * FROM [SMARTH_DB].[dbo].[PARAMETROS_Postpresion] a" +
                             " INNER JOIN (SELECT Referencia, Maquina, MAX(Version) AS Version" +
                             " from [SMARTH_DB].[dbo].[PARAMETROS_Ficha]" +
                             " where Maquina like '" + maquina + "%'and CodMolde like '" + molde + "%'" +
                             " GROUP BY Referencia, Maquina) b on a.Referencia = b.Referencia and a.Maquina = b.Maquina and a.Version = b.Version";
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

        public DataSet leerToleranciasMOLDE(int molde, int maquina)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                /*string sql = "SELECT * FROM PARAMETROS.dbo.Tolerancias a" +
                            " INNER JOIN (SELECT Referencia, Maquina, MAX(Version) AS Version from PARAMETROS.dbo.Tolerancias GROUP BY Referencia, Maquina) b" +
                            " on a.Referencia = b.Referencia and a.Maquina = b.Maquina and a.Version = b.Version" +
                            " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] P ON A.Referencia = P.Referencia" +
                            " WHERE P.Molde = " + molde + " AND a.Maquina = " + maquina + "";*/
                string sql = "SELECT * FROM [SMARTH_DB].[dbo].[PARAMETROS_Tolerancias] a" +
                             " INNER JOIN (SELECT Referencia, Maquina, MAX(Version) AS Version" +
                             " from [SMARTH_DB].[dbo].[PARAMETROS_Ficha]" +
                             " where Maquina like '" + maquina + "%'and CodMolde like '" + molde + "%'" +
                             " GROUP BY Referencia, Maquina) b on a.Referencia = b.Referencia and a.Maquina = b.Maquina and a.Version = b.Version";
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

        public DataSet leerAtemperadoMOLDE(int molde, int maquina)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                /*string sql = "SELECT * FROM PARAMETROS.dbo.Atemperado a" +
                            " INNER JOIN (SELECT Referencia, Maquina, MAX(Version) AS Version from PARAMETROS.dbo.Atemperado GROUP BY Referencia, Maquina) b" +
                            " on a.Referencia = b.Referencia and a.Maquina = b.Maquina and a.Version = b.Version" +
                            " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] P ON A.Referencia = P.Referencia" +
                            " WHERE P.Molde = " + molde + " AND a.Maquina = " + maquina + "";*/

                string sql = "SELECT * FROM [SMARTH_DB].[dbo].[PARAMETROS_AtemperadoV1] a" +
                             " INNER JOIN (SELECT Referencia, Maquina, MAX(Version) AS Version" +
                             " from [SMARTH_DB].[dbo].[PARAMETROS_Ficha]" +
                             " where Maquina like '" + maquina + "%'and CodMolde like '" + molde + "%'" +
                             " GROUP BY Referencia, Maquina) b on a.Referencia = b.Referencia and a.Maquina = b.Maquina and a.Version = b.Version";
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


        //ALERTA DE DESVIACION
        public bool Existe_Desviacion_Sin_Notificar(string orden)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT [ValidadoING],[AlertaDesviacion] FROM[SMARTH_DB].[dbo].[LIBERACION_Ficha] where[Orden] like '"+orden+"%' and(AlertaDesviacion is null or AlertaDesviacion = 0)";
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
            catch (Exception ex)
            {
                cnn_GP12.Close();
                return false;
            }
        }

        public void Marca_Mail_Enviado_Alerta_Desviacion(string orden)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "UPDATE [SMARTH_DB].[dbo].[LIBERACION_Ficha] SET[AlertaDesviacion] = 1 where[Orden] like '"+orden+"%'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();               
            }
            catch (Exception)
            {
                cnn_GP12.Close();
            }
        }

        public void Validar_Ingenieria(string orden, int operario)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "UPDATE [SMARTH_DB].[dbo].[LIBERACION_Ficha] SET [ValidadoING] = "+operario+" where[Orden] like '" + orden + "%'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();
            }
            catch (Exception)
            {
                cnn_GP12.Close();
            }
        }

        //CONECTORES GUARDADO INICIAL DE LIBERACION
        public DataSet Consulta_existencia_liberacion(string orden)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT * FROM [SMARTH_DB].[dbo].[LIBERACION_Ficha] WHERE Orden = '" + orden + "'";
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
        public void Crear_ficha_liberacion(string Orden, int Referencia, string Maquina, int Version, int CodMolde, int Operario1, double Operario1Horas, string Operario1Nivel, string Operario1UltREV, string Operario1Notas, int Operario2, double Operario2Horas, string Operario2Nivel, string Operario2UltREV, string Operario2Notas, int Encargado, double EncargadoHoras, int Cambiador,
                double CambiadorHoras, int Calidad, double CalidadHoras, int CambiadorLiberado, string FechaCambiadorLiberado, int ProduccionLiberado, string FechaProduccionLiberado, int CalidadLiberado, string FechaCalidadLiberado, int ResultadoLiberado, int AccionLiberado, string NotasLiberado, int ReLiberacion, int ENCNoconformidad, int ENCDefectos, int CALNoconformidad, int CALDefectos, string FechaApertura,
                //VALORES POSTPRESION
                double P1, double Tp1, double P2, double Tp2, double P3, double Tp3, double P4, double Tp4, double P5, double Tp5,
                double P6, double Ti6, double P7, double Tp7, double P8, double Tp8, double P9, double Tp9, double P10, double Tp10, string conmutacion, string tiempoPresion,
                //VALORES CAMARA CALIENTE
                double Z1, double Z2, double Z3, double Z4, double Z5, double Z6, double Z7, double Z8, double Z9, double Z10,
                double Z11, double Z12, double Z13, double Z14, double Z15, double Z16, double Z17, double Z18, double Z19, double Z20,
                //VALORES TEMPERATURA CILINDRO
                double boq, double T1, double T2, double T3, double T4,  double T5, double T6, double T7, double T8, double T9, double T10, string existeficha,
                //VALORES TOLERANCIAS
                double TiempoInyeccion, double LimitePresion, double VelocidadCarga, double Carga, double Descompresion, double Contrapresion, double Tiempo, double Enfriamiento, double Ciclo, double Cojin,
                double TiempoInyeccionNValDouble, double TiempoInyeccionMValDouble, double LimitePresionNValDouble, double LimitePresionMValDouble, double ConmuntaciontolNValDouble, double ConmuntaciontolMValDouble, double TiempoPresiontolNValDouble,
                double TiempoPresiontolMValDouble, double TNvcargavalDouble, double TMvcargavalDouble, double TNcargavalDouble, double TMcargavalDouble, double TNdescomvalDouble, double TMdescomvalDouble, double TNcontrapvalDouble,
                double TMcontrapvalDouble, double TNTiempdosvalDouble, double TMTiempdosvalDouble, double TNEnfriamvalDouble, double TMEnfriamvalDouble, double TNCiclovalDouble, double TMCiclovalDouble, double TNCojinvalDouble, double TMCojinvalDouble,
                //VALORES MATERIALES
                string MAT1REF, string MAT1NOM, string MAT1LOT, double MAT1TIEMP, double MAT1TIEMPREAL, double MAT1TEMP, double MAT1TEMPREAL, string MAT2REF, string MAT2NOM, string MAT2LOT, double MAT2TIEMP, double MAT2TIEMPREAL, double MAT2TEMP, double MAT2TEMPREAL, string MAT3REF, string MAT3NOM, string MAT3LOT, double MAT3TIEMP, double MAT3TIEMPREAL, double MAT3TEMP, double MAT3TEMPREAL,
                string COMP1REF, string COMP1NOM, string COMP1LOT, string COMP2REF, string COMP2NOM, string COMP2LOT, string COMP3REF, string COMP3NOM, string COMP3LOT, string COMP4REF, string COMP4NOM, string COMP4LOT, string COMP5REF, string COMP5NOM, string COMP5LOT, string COMP6REF, string COMP6NOM, string COMP6LOT, string COMP7REF, string COMP7NOM, string COMP7LOT,
                //VALORES ATEMPERADO
                string CircuitoF1, string CircuitoF2, string CircuitoF3, string CircuitoF4, string CircuitoF5, string CircuitoF6,
                string CaudalF1, string CaudalF2, string CaudalF3, string CaudalF4, string CaudalF5, string CaudalF6,
                string TemperaturaF1, string TemperaturaF2, string TemperaturaF3, string TemperaturaF4, string TemperaturaF5, string TemperaturaF6,  
                string CircuitoM1, string CircuitoM2, string CircuitoM3, string CircuitoM4, string CircuitoM5, string CircuitoM6,
                string CaudalM1, string CaudalM2, string CaudalM3, string CaudalM4, string CaudalM5, string CaudalM6,
                string TemperaturaM1, string TemperaturaM2, string TemperaturaM3, string TemperaturaM4, string TemperaturaM5, string TemperaturaM6,
                //VALORES AUDITORÍA
                int Q1E, string Q1ENC, int Q2E, string Q2ENC, int Q3E, string Q3ENC, int Q4E, string Q4ENC, int Q5E, string Q5ENC, 
                int Q6E, int Q6C, string Q6ENC, string Q6CAL, int Q7E, int Q7C, string Q7ENC, string Q7CAL, int Q8E, int Q8C, string Q8ENC, string Q8CAL, int Q9E, int Q9C, string Q9ENC, string Q9CAL, int Q10C, string Q10CAL, string QXFeedbackCambiador, string QXFeedbackProduccion
            )
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[LIBERACION_Ficha] ([Orden],[Referencia],[Maquina],[Version],[CodMolde],[Operario1],[Operario1Horas],[Operario1Nivel],[Operario1UltREV],[Operario1Notas],[Operario2],[Operario2Horas],[Operario2Nivel],[Operario2UltREV],[Operario2Notas],[Encargado],[EncargadoHoras],[Cambiador],[CambiadorHoras],[Calidad],[CalidadHoras],[CambiadorLiberado],[FechaCambiadorLiberado],[ProduccionLiberado]," +
                             "[FechaProduccionLiberado],[CalidadLiberado],[FechaCalidadLiberado],[ResultadoLiberado],[AccionLiberado],[NotasLiberado],[ReLiberacion],[ENCNoconformidad],[ENCDefectos],[CALNoconformidad],[CALDefectos],[FechaApertura],[ValidadoING])" +
                                                                "VALUES('" + Orden + "'," + Referencia + ",'" + Maquina + "'," + Version + "," + CodMolde + "," + Operario1 + "," + Operario1Horas.ToString().Replace(',', '.') + ",'" + Operario1Nivel + "','" + Operario1UltREV + "','" + Operario1Notas + "',"+Operario2+"," + Operario2Horas.ToString().Replace(',', '.') + ",'" + Operario2Nivel + "','" + Operario2UltREV + "','" + Operario2Notas + "'," + Encargado + "," + EncargadoHoras.ToString().Replace(',', '.') + "," + Cambiador + "," + CambiadorHoras.ToString().Replace(',', '.') + "," + Calidad + "," + CalidadHoras.ToString().Replace(',', '.') + "," + CambiadorLiberado + ",'" + FechaCambiadorLiberado + "'," + ProduccionLiberado + ",'" +
                            FechaProduccionLiberado + "'," + CalidadLiberado + ",'" + FechaCalidadLiberado + "'," + ResultadoLiberado + "," + AccionLiberado + ",'" + NotasLiberado + "'," + ReLiberacion + "," + ENCNoconformidad + "," + ENCDefectos + "," + CALNoconformidad + "," + CALDefectos + ",'"+FechaApertura+"',0)";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();
                    Crear_ficha_postpresion(Orden, Referencia, Maquina, Version, CodMolde, P1, Tp1, P2, Tp2, P3, Tp3, P4, Tp4, P5, Tp5, P6, Ti6, P7, Tp7, P8, Tp8, P9, Tp9, P10, Tp10, tiempoPresion, conmutacion);
                    Crear_ficha_TempCamCaliente(Orden, Referencia, Maquina, Version, Z1, Z2, Z3, Z4, Z5, Z6, Z7, Z8, Z9, Z10,Z11, Z12, Z13, Z14, Z15, Z16, Z17, Z18, Z19, Z20);
                    Crear_ficha_TempCilindro(Orden, Referencia, Maquina, Version, CodMolde, boq, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, existeficha);
                    Crear_ficha_Tolerancias(Orden, Referencia, Maquina, Version, TiempoInyeccion, LimitePresion, VelocidadCarga, Carga, Descompresion, Contrapresion, Tiempo, Enfriamiento, Ciclo, Cojin,
                                            TiempoInyeccionNValDouble, TiempoInyeccionMValDouble, LimitePresionNValDouble, LimitePresionMValDouble, ConmuntaciontolNValDouble, ConmuntaciontolMValDouble, TiempoPresiontolNValDouble,
                                            TiempoPresiontolMValDouble, TNvcargavalDouble, TMvcargavalDouble, TNcargavalDouble, TMcargavalDouble, TNdescomvalDouble, TMdescomvalDouble, TNcontrapvalDouble,
                                            TMcontrapvalDouble, TNTiempdosvalDouble, TMTiempdosvalDouble, TNEnfriamvalDouble, TMEnfriamvalDouble, TNCiclovalDouble, TMCiclovalDouble, TNCojinvalDouble, TMCojinvalDouble);
                    Crear_ficha_Materiales(Orden, Referencia, Maquina, Version, MAT1REF, MAT1NOM, MAT1LOT, MAT1TIEMP, MAT1TIEMPREAL, MAT1TEMP, MAT1TEMPREAL, MAT2REF, MAT2NOM, MAT2LOT, MAT2TIEMP, MAT2TIEMPREAL, MAT2TEMP, MAT2TEMPREAL, MAT3REF, MAT3NOM, MAT3LOT, MAT3TIEMP, MAT3TIEMPREAL, MAT3TEMP, MAT3TEMPREAL,
                                            COMP1REF, COMP1NOM, COMP1LOT, COMP2REF, COMP2NOM, COMP2LOT, COMP3REF, COMP3NOM, COMP3LOT, COMP4REF, COMP4NOM, COMP4LOT, COMP5REF, COMP5NOM, COMP5LOT, COMP6REF, COMP6NOM, COMP6LOT, COMP7REF, COMP7NOM, COMP7LOT);
                    Crear_ficha_Atemperado(Orden, Referencia, Maquina, Version, CircuitoF1, CircuitoF2, CircuitoF3, CircuitoF4, CircuitoF5, CircuitoF6, CaudalF1, CaudalF2, CaudalF3, CaudalF4, CaudalF5, CaudalF6, TemperaturaF1, TemperaturaF2, TemperaturaF3, TemperaturaF4, TemperaturaF5, TemperaturaF6,
                                            CircuitoM1, CircuitoM2, CircuitoM3, CircuitoM4, CircuitoM5, CircuitoM6, CaudalM1, CaudalM2, CaudalM3, CaudalM4, CaudalM5, CaudalM6, TemperaturaM1, TemperaturaM2, TemperaturaM3, TemperaturaM4, TemperaturaM5, TemperaturaM6);
                    Crear_ficha_Auditoria(Orden, Referencia, Maquina, Version, Q1E, Q1ENC, Q2E, Q2ENC, Q3E, Q3ENC, Q4E, Q4ENC, Q5E, Q5ENC, Q6E, Q6C, Q6ENC, Q6CAL, Q7E, Q7C, Q7ENC, Q7CAL, Q8E, Q8C, Q8ENC, Q8CAL, Q9E, Q9C, Q9ENC, Q9CAL, Q10C, Q10CAL, QXFeedbackCambiador, QXFeedbackProduccion);
            }
            catch (Exception)
            {
                cnn_GP12.Close();

            }
        }
        public void Crear_ficha_postpresion(string Orden, int referencia, string maquina, int version, int CodMolde, double P1, double T1, double P2, double T2, double P3, double T3, double P4, double T4, double P5, double T5,
                                          double P6, double T6, double P7, double T7, double P8, double T8, double P9, double T9, double P10, double T10, string tiempoPresion, string conmutacion)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                if (tiempoPresion == null)
                    tiempoPresion = "";
                if (conmutacion == null)
                    conmutacion = "";
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[LIBERACION_Postpresion] (Orden, Referencia, Maquina, Version, CodMolde, P1, T1, P2, T2, P3, T3, P4, T4, P5, T5, P6, T6, P7, T7, P8, T8, P9, T9, P10, T10, TiempoPresion, Conmutacion) " +
                                                      " VALUES ("+Orden+"," + referencia + ",'" + maquina + "'," + version + "," + CodMolde + "," + P1.ToString().Replace(',', '.') + "," + T1.ToString().Replace(',', '.') +
                                                      "," + P2.ToString().Replace(',', '.') + "," + T2.ToString().Replace(',', '.') + "," + P3.ToString().Replace(',', '.') +
                                                      "," + T3.ToString().Replace(',', '.') + "," + P4.ToString().Replace(',', '.') + "," + T4.ToString().Replace(',', '.') +
                                                      "," + P5.ToString().Replace(',', '.') + "," + T5.ToString().Replace(',', '.') + "," + P6.ToString().Replace(',', '.') +
                                                      "," + T6.ToString().Replace(',', '.') + "," + P7.ToString().Replace(',', '.') + "," + T7.ToString().Replace(',', '.') +
                                                      "," + P8.ToString().Replace(',', '.') + "," + T8.ToString().Replace(',', '.') + "," + P9.ToString().Replace(',', '.') +
                                                      "," + T9.ToString().Replace(',', '.') + "," + P10.ToString().Replace(',', '.') + "," + T10.ToString().Replace(',', '.') +
                                                      ",'" + tiempoPresion.ToString().Replace(',', '.') + "','" + conmutacion.ToString().Replace(',', '.') + "')";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();
            }
            catch (Exception)
            {
                cnn_GP12.Close();
            }
        }

        public void Crear_ficha_TempCamCaliente(string Orden, int referencia, string maquina, int version, double Z1, double Z2, double Z3, double Z4, double Z5, double Z6, double Z7, double Z8, double Z9, double Z10,
                                           double Z11, double Z12, double Z13, double Z14, double Z15, double Z16, double Z17, double Z18, double Z19, double Z20)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[LIBERACION_TempCamCaliente] (Orden, Referencia, Maquina, Version, Z1, Z2, Z3, Z4, Z5, Z6, Z7, Z8, Z9, Z10, " +
                                                       "Z11, Z12, Z13, Z14, Z15, Z16, Z17, Z18, Z19, Z20)" +
                                                      " VALUES ("+Orden+"," + referencia + ",'" + maquina + "'," + version + "," + Z1.ToString().Replace(',', '.') + "," + Z2.ToString().Replace(',', '.') + "," +
                                                      Z3.ToString().Replace(',', '.') + "," + Z4.ToString().Replace(',', '.') + "," + Z5.ToString().Replace(',', '.') + "," +
                                                      Z6.ToString().Replace(',', '.') + "," + Z7.ToString().Replace(',', '.') +
                                                      "," + Z8.ToString().Replace(',', '.') + "," + Z9.ToString().Replace(',', '.') +
                                                      "," + Z10.ToString().Replace(',', '.') + "," + Z11.ToString().Replace(',', '.') +
                                                      "," + Z12.ToString().Replace(',', '.') + "," + Z13.ToString().Replace(',', '.') +
                                                      "," + Z14.ToString().Replace(',', '.') + "," + Z15.ToString().Replace(',', '.') +
                                                      "," + Z16.ToString().Replace(',', '.') + "," + Z17.ToString().Replace(',', '.') +
                                                      "," + Z18.ToString().Replace(',', '.') + "," + Z19.ToString().Replace(',', '.') +
                                                      "," + Z20.ToString().Replace(',', '.') + ")";
                
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();
            }
            catch (Exception)
            {
                cnn_GP12.Close();
            }
        }

        public void Crear_ficha_TempCilindro(string Orden, int referencia, string maquina, int version, int CodMolde, double boq, double T1, double T2, double T3, double T4, double T5, double T6, double T7, double T8, double T9, double T10, string existeficha)
        
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[LIBERACION_TempCilindro] (Orden, Referencia, Maquina, Version, CodMolde, Boq, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, EXISTEFICHA) " +
                                                      " VALUES ('"+Orden+"'," + referencia + ",'" + maquina + "'," + version + ","+CodMolde+"," + boq.ToString().Replace(',', '.') + "," + T1.ToString().Replace(',', '.') + "," +
                                                      T2.ToString().Replace(',', '.') + "," + T3.ToString().Replace(',', '.') + "," + T4.ToString().Replace(',', '.') + "," +
                                                      T5.ToString().Replace(',', '.') + "," + T6.ToString().Replace(',', '.') + "," + T7.ToString().Replace(',', '.') + "," +
                                                      T8.ToString().Replace(',', '.') + "," + T9.ToString().Replace(',', '.') + "," + T10.ToString().Replace(',', '.') + ","+existeficha+")";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
            }
        }

        public void Crear_ficha_Tolerancias(string Orden, int referencia, string maquina, int version, double TiempoInyeccion, double LimitePresion, double VelocidadCarga, double Carga, double Descompresion, double Contrapresion, double Tiempo, double Enfriamiento, double Ciclo, double Cojin,
                    double TiempoInyeccionNValDouble, double TiempoInyeccionMValDouble, double LimitePresionNValDouble, double LimitePresionMValDouble, double ConmuntaciontolNValDouble, double ConmuntaciontolMValDouble, double TiempoPresiontolNValDouble,
                    double TiempoPresiontolMValDouble, double TNvcargavalDouble, double TMvcargavalDouble, double TNcargavalDouble, double TMcargavalDouble, double TNdescomvalDouble, double TMdescomvalDouble, double TNcontrapvalDouble,
                    double TMcontrapvalDouble, double TNTiempdosvalDouble, double TMTiempdosvalDouble, double TNEnfriamvalDouble, double TMEnfriamvalDouble, double TNCiclovalDouble, double TMCiclovalDouble, double TNCojinvalDouble, double TMCojinvalDouble)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[LIBERACION_Tolerancias] (Orden, Referencia, Maquina, Version,[TiempoInyeccion],[LimitePresion],[VelocidadCarga],[Carga],[Descompresion],[Contrapresion],[Tiempo],[Enfriamiento],[Ciclo],[Cojin], " +
                                  "TiempoInyeccionNVal, TiempoInyeccionMVal, LimitePresionNVal, LimitePresionMVal, ConmuntaciontolNVal, ConmuntaciontolMVal, TiempoPresiontolNVal, TiempoPresiontolMVal, TNvcargaval, TMvcargaval, TNcargaval, TMcargaval, TNdescomval, TMdescomval, TNcontrapval, TMcontrapval, TNTiempdosval, TMTiempdosval, TNEnfriamval, TMEnfriamval, TNCicloval, TMCicloval, TNCojinval, TMCojinval)" +
                                  " VALUES ('" + Orden + "'," + referencia + ",'" + maquina + "'," + version + "," + TiempoInyeccion.ToString().Replace(',', '.') + ","+ LimitePresion.ToString().Replace(',', '.') + ","+ VelocidadCarga.ToString().Replace(',', '.') + ","+Carga.ToString().Replace(',', '.') + ","+Descompresion.ToString().Replace(',', '.') + ","+Contrapresion.ToString().Replace(',', '.') + ","+Tiempo.ToString().Replace(',', '.') + ","+Enfriamiento.ToString().Replace(',', '.') + ","+Ciclo.ToString().Replace(',', '.') + ","+Cojin.ToString().Replace(',', '.') + ","+
                                  TiempoInyeccionNValDouble.ToString().Replace(',', '.') + "," + TiempoInyeccionMValDouble.ToString().Replace(',', '.') + "," + LimitePresionNValDouble.ToString().Replace(',', '.') + "," + LimitePresionMValDouble.ToString().Replace(',', '.') + "," +
                                  ConmuntaciontolNValDouble.ToString().Replace(',', '.') + "," + ConmuntaciontolMValDouble.ToString().Replace(',', '.') + "," + TiempoPresiontolNValDouble.ToString().Replace(',', '.') + "," + TiempoPresiontolMValDouble.ToString().Replace(',', '.') + "," + TNvcargavalDouble.ToString().Replace(',', '.') + "," + TMvcargavalDouble.ToString().Replace(',', '.') + "," + TNcargavalDouble.ToString().Replace(',', '.') + "," + TMcargavalDouble.ToString().Replace(',', '.') + "," +
                                  TNdescomvalDouble.ToString().Replace(',', '.') + "," + TMdescomvalDouble.ToString().Replace(',', '.') + "," + TNcontrapvalDouble.ToString().Replace(',', '.') + "," + TMcontrapvalDouble.ToString().Replace(',', '.') + "," + TNTiempdosvalDouble.ToString().Replace(',', '.') + "," + TMTiempdosvalDouble.ToString().Replace(',', '.') + "," + TNEnfriamvalDouble.ToString().Replace(',', '.') + "," + TMEnfriamvalDouble.ToString().Replace(',', '.') + "," +
                                  TNCiclovalDouble.ToString().Replace(',', '.') + "," + TMCiclovalDouble.ToString().Replace(',', '.') + "," + TNCojinvalDouble.ToString().Replace(',', '.') + "," + TMCojinvalDouble.ToString().Replace(',', '.') + ")";
                                  
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
            }
        }

        public void Crear_ficha_Materiales(string Orden, int referencia, string maquina, int version, string MAT1REF, string MAT1NOM, string MAT1LOT, double MAT1TIEMP, double MAT1TIEMPREAL, double MAT1TEMP, double MAT1TEMPREAL, string MAT2REF, string MAT2NOM, string MAT2LOT, double MAT2TIEMP, double MAT2TIEMPREAL, double MAT2TEMP, double MAT2TEMPREAL, string MAT3REF, string MAT3NOM, string MAT3LOT, double MAT3TIEMP, double MAT3TIEMPREAL, double MAT3TEMP, double MAT3TEMPREAL,
                string COMP1REF, string COMP1NOM, string COMP1LOT, string COMP2REF, string COMP2NOM, string COMP2LOT, string COMP3REF, string COMP3NOM, string COMP3LOT, string COMP4REF, string COMP4NOM, string COMP4LOT, string COMP5REF, string COMP5NOM, string COMP5LOT, string COMP6REF, string COMP6NOM, string COMP6LOT, string COMP7REF, string COMP7NOM, string COMP7LOT)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[LIBERACION_Estructura] ([Orden],[Referencia],[Maquina],[Version],[MAT1REF],[MAT1NOM],[MAT1LOT],[MAT1TIEMP],[MAT1TIEMPREAL],[MAT1TEMP],[MAT1TEMPREAL],[MAT2REF],[MAT2NOM],[MAT2LOT],[MAT2TIEMP],[MAT2TIEMPREAL],[MAT2TEMP],[MAT2TEMPREAL],[MAT3REF],[MAT3NOM],[MAT3LOT],[MAT3TIEMP],[MAT3TIEMPREAL],[MAT3TEMP],[MAT3TEMPREAL],[COMP1REF],[COMP1NOM],[COMP1LOT],[COMP2REF],[COMP2NOM],[COMP2LOT],[COMP3REF],[COMP3NOM],[COMP3LOT],[COMP4REF],[COMP4NOM],[COMP4LOT],[COMP5REF],[COMP5NOM],[COMP5LOT],[COMP6REF],[COMP6NOM],[COMP6LOT],[COMP7REF],[COMP7NOM],[COMP7LOT]) " +
                              " VALUES ('" + Orden + "'," + referencia + ",'" + maquina + "'," + version + ",'" + MAT1REF + "','" + MAT1NOM + "','" + MAT1LOT + "'," + MAT1TIEMP.ToString().Replace(',', '.') + "," + MAT1TIEMPREAL.ToString().Replace(',', '.') + "," + MAT1TEMP.ToString().Replace(',', '.') + "," + MAT1TEMPREAL.ToString().Replace(',', '.') + ",'" + MAT2REF + "','" + MAT2NOM + "','" + MAT2LOT + "'," + MAT2TIEMP.ToString().Replace(',', '.') + "," + MAT2TIEMPREAL.ToString().Replace(',', '.') + "," + MAT2TEMP.ToString().Replace(',', '.') + "," + MAT2TEMPREAL.ToString().Replace(',', '.') + ",'" + MAT3REF + "','" + MAT3NOM + "','" + MAT3LOT + "'," + MAT3TIEMP.ToString().Replace(',', '.') + "," + MAT3TIEMPREAL.ToString().Replace(',', '.') + "," + MAT3TEMP.ToString().Replace(',', '.') + "," + MAT3TEMPREAL.ToString().Replace(',', '.') + ",'" + COMP1REF + "','" + COMP1NOM + "','" + COMP1LOT + "','" + COMP2REF + "','" + COMP2NOM + "','" + COMP2LOT + "','" + COMP3REF + "','" + COMP3NOM + "','" + COMP3LOT + "','" + COMP4REF + "','" + COMP4NOM + "','" + COMP4LOT + "','" + COMP5REF + "','" + COMP5NOM + "','" + COMP5LOT + "','" + COMP6REF + "','" + COMP6NOM + "','" + COMP6LOT + "','" + COMP7REF + "','" + COMP7NOM + "','" + COMP7LOT + "')";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();
            }
            catch (Exception)
            {
                cnn_GP12.Close();
            }
        }

        public void Crear_ficha_Atemperado(string Orden, int referencia, string maquina, int version,
                                  string CircuitoF1, string CircuitoF2, string CircuitoF3, string CircuitoF4, string CircuitoF5, string CircuitoF6,
                                  string CaudalF1, string CaudalF2, string CaudalF3, string CaudalF4, string CaudalF5, string CaudalF6,
                                  string TemperaturaF1, string TemperaturaF2, string TemperaturaF3, string TemperaturaF4, string TemperaturaF5, string TemperaturaF6,
                                  string CircuitoM1, string CircuitoM2, string CircuitoM3, string CircuitoM4, string CircuitoM5, string CircuitoM6,
                                  string CaudalM1, string CaudalM2, string CaudalM3, string CaudalM4, string CaudalM5, string CaudalM6,
                                  string TemperaturaM1, string TemperaturaM2, string TemperaturaM3, string TemperaturaM4, string TemperaturaM5, string TemperaturaM6)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[LIBERACION_Atemperados] (Orden, Referencia, Maquina, Version," +
                                  "CircuitoF1, CircuitoF2, CircuitoF3, CircuitoF4, CircuitoF5, CircuitoF6," +
                                  "CaudalF1, CaudalF2, CaudalF3, CaudalF4, CaudalF5, CaudalF6," +
                                  "TemperaturaF1, TemperaturaF2, TemperaturaF3, TemperaturaF4, TemperaturaF5, TemperaturaF6," +
                                  "CircuitoM1, CircuitoM2, CircuitoM3, CircuitoM4, CircuitoM5, CircuitoM6," +
                                  "CaudalM1, CaudalM2, CaudalM3, CaudalM4, CaudalM5, CaudalM6," +
                                  "TemperaturaM1, TemperaturaM2, TemperaturaM3, TemperaturaM4, TemperaturaM5, TemperaturaM6)" +
                                  " VALUES ('" + Orden + "'," + referencia + ",'" + maquina + "'," + version + ",'" + CircuitoF1 + "','" + CircuitoF2 + "','" + CircuitoF3 + "','" + CircuitoF4 + "','" + CircuitoF5 + "','" + CircuitoF6 + "','" +
                                  CaudalF1 + "','" + CaudalF2 + "','" + CaudalF3 + "','" + CaudalF4 + "','" + CaudalF5 + "','" + CaudalF6 + "','" +
                                  TemperaturaF1 + "','" + TemperaturaF2 + "','" + TemperaturaF3 + "','" + TemperaturaF4 + "','" + TemperaturaF5 + "','" + TemperaturaF6 + "','" +
                                  CircuitoM1 + "','" + CircuitoM2 + "','" + CircuitoM3 + "','" + CircuitoM4 + "','" + CircuitoM5 + "','" + CircuitoM6 + "','" +
                                  CaudalM1 + "','" + CaudalM2 + "','" + CaudalM3 + "','" + CaudalM4 + "','" + CaudalM5 + "','" + CaudalM6 + "','" +
                                  TemperaturaM1 + "','" + TemperaturaM2 + "','" + TemperaturaM3 + "','" + TemperaturaM4 + "','" + TemperaturaM5 + "','" + TemperaturaM6 + "')";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
            }
        }

        public void Crear_ficha_Auditoria(string Orden, int referencia, string maquina, int version,
                                int Q1E, string Q1ENC, int Q2E, string Q2ENC, int Q3E, string Q3ENC, int Q4E, string Q4ENC, int Q5E, string Q5ENC,
                                int Q6E, int Q6C, string Q6ENC, string Q6CAL, int Q7E, int Q7C, string Q7ENC, string Q7CAL, int Q8E, int Q8C, string Q8ENC,
                                string Q8CAL, int Q9E, int Q9C, string Q9ENC, string Q9CAL, int Q10C, string Q10CAL, string QXFeedbackCambiador, string QXFeedbackProduccion)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[LIBERACION_Auditoria] (Orden, Referencia, Maquina, Version," +
                                 "[Q1E],[Q1ENC],[Q2E],[Q2ENC],[Q3E],[Q3ENC],[Q4E],[Q4ENC],[Q5E],[Q5ENC],[Q6E],[Q6C],[Q6ENC],[Q6CAL],[Q7E],[Q7C],[Q7ENC],[Q7CAL]," +
                                 "[Q8E],[Q8C],[Q8ENC],[Q8CAL],[Q9E],[Q9C],[Q9ENC],[Q9CAL],[Q10C],[Q10CAL],[QXFeedbackCambiador],[QXFeedbackProduccion])" +
                                  " VALUES ('" + Orden + "'," + referencia + ",'" + maquina + "'," + version + "," + Q1E + ",'" + Q1ENC + "'," + Q2E + ",'" + Q2ENC + "'," + Q3E + ",'" + Q3ENC + "'," + Q4E + ",'" + Q4ENC + "'," + Q5E + ",'" + Q5ENC + "'," +
                                  Q6E + "," + Q6C + ",'" + Q6ENC + "','" + Q6CAL + "'," + Q7E + "," + Q7E + ",'" + Q7ENC + "','" + Q7CAL + "'," + Q8E + "," + Q8C + ",'" + Q8ENC + "','" + Q8CAL + "'," + Q9E + "," + Q9C + ",'" + Q9ENC + "','" + Q9CAL + "'," + Q10C + ",'" + Q10CAL + "','"+QXFeedbackCambiador+"','" + QXFeedbackProduccion + "')";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();
            }
            catch (Exception)
            {
                cnn_GP12.Close();
            }
        }

        //LECTURA DE PARÁMETROS DESDE FICHA DE LIBERACION

        public DataSet CargaLiberacionFicha(string Orden)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT * FROM[SMARTH_DB].[dbo].[LIBERACION_Ficha] WHERE ORDEN = RTRIM('"+Orden+"') ORDER BY VERSION DESC";
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
        public DataSet LeerTempCilindroLIBERACION(int orden)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT * FROM [SMARTH_DB].[dbo].[LIBERACION_TempCilindro] WHERE ORDEN = RTRIM('" + orden + "')";
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
        public DataSet LeerTempCamCalienteLIBERACION(int orden)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT *  FROM [SMARTH_DB].[dbo].[LIBERACION_TempCamCaliente] WHERE ORDEN = RTRIM('" + orden + "')";
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
        public DataSet LeerPostpresionLIBERACION(int orden)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT *  FROM [SMARTH_DB].[dbo].[LIBERACION_Postpresion] WHERE ORDEN = RTRIM('" + orden + "')";
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
        public DataSet LeerAtemperadoLIBERACION(int orden)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT *  FROM [SMARTH_DB].[dbo].[LIBERACION_Atemperados] WHERE ORDEN = RTRIM('" + orden + "')";
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
        public DataSet LeerToleranciasLIBERACION(int orden)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT *  FROM [SMARTH_DB].[dbo].[LIBERACION_Tolerancias] WHERE ORDEN = RTRIM('" + orden + "')";
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
        public DataSet LeerAuditoriaLIBERACION(int orden)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT *  FROM [SMARTH_DB].[dbo].[LIBERACION_Auditoria] WHERE ORDEN = RTRIM('" + orden + "')";
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
        public DataSet LeerEstructuraLIBERACION(int orden)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT *  FROM [SMARTH_DB].[dbo].[LIBERACION_Estructura] WHERE ORDEN = RTRIM('" + orden + "')";
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
        public string Devuelve_Nombre_Operario(string operario)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT [Id], [Operario] FROM[SMARTH_DB].[dbo].[AUX_TablaOperarios] where Id = " + operario + "";


                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_GP12.Close();
                return ds.Tables[0].Rows[0]["Operario"].ToString();
            }
            catch
            {
                cnn_GP12.Close();
                return "";
            }
        }

        //ACTUALIZACIÓN DE PARÁMETROS AL LIBERAR O GUARDAR

        public void ActualizarLiberacionFicha(string Orden, int Referencia, string Maquina, int Version, int CodMolde, int Operario1, double Operario1Horas, string Operario1Nivel, string Operario1UltREV, string Operario1Notas, int Operario2, double Operario2Horas, string Operario2Nivel, string Operario2UltREV, string Operario2Notas, int Encargado, double EncargadoHoras, int Cambiador,
                double CambiadorHoras, int Calidad, double CalidadHoras, int CambiadorLiberado, string FechaCambiadorLiberado, int ProduccionLiberado, string FechaProduccionLiberado, int CalidadLiberado, string FechaCalidadLiberado, int ResultadoLiberado, int AccionLiberado, string NotasLiberado, int ReLiberacion, int ENCNoconformidad, int ENCDefectos, int CALNoconformidad, int CALDefectos, string QUERYLiberarCambiadorHoraORI, string QUERYLiberarEncargadoHoraORI, string QUERYLiberacionCalidadHoraORI, int ResultadoLOTES, int ResultadoPARAM,
                //VALORES POSTPRESION
                double P1, double Tp1, double P2, double Tp2, double P3, double Tp3, double P4, double Tp4, double P5, double Tp5,
                double P6, double Ti6, double P7, double Tp7, double P8, double Tp8, double P9, double Tp9, double P10, double Tp10, string conmutacion, string tiempoPresion,
                    //REALES POSTPRESION
                double P1R, double Tp1R, double P2R, double Tp2R, double P3R, double Tp3R, double P4R, double Tp4R, double P5R, double Tp5R,
                double P6R, double Ti6R, double P7R, double Tp7R, double P8R, double Tp8R, double P9R, double Tp9R, double P10R, double Tp10R, string conmutacionREAL, string tiempoPresionREAL,
                //VALORES CAMARA CALIENTE
                double Z1, double Z2, double Z3, double Z4, double Z5, double Z6, double Z7, double Z8, double Z9, double Z10,
                double Z11, double Z12, double Z13, double Z14, double Z15, double Z16, double Z17, double Z18, double Z19, double Z20,
                    //REALES CAMARA CALIENTE
                double Z1R, double Z2R, double Z3R, double Z4R, double Z5R, double Z6R, double Z7R, double Z8R, double Z9R, double Z10R,
                double Z11R, double Z12R, double Z13R, double Z14R, double Z15R, double Z16R, double Z17R, double Z18R, double Z19R, double Z20R,
                //VALORES TEMPERATURA CILINDRO
                double boq, double T1, double T2, double T3, double T4, double T5, double T6, double T7, double T8, double T9, double T10,
                    //REALES TEMPERATURA CILINDRO
                double boqR, double T1R, double T2R, double T3R, double T4R, double T5R, double T6R, double T7R, double T8R, double T9R, double T10R, string existeficha,
                //VALORES TOLERANCIAS
                double TiempoInyeccion, double LimitePresion, double VelocidadCarga, double Carga, double Descompresion, double Contrapresion, double Tiempo, double Enfriamiento, double Ciclo, double Cojin,
                double TiempoInyeccionNValDouble, double TiempoInyeccionMValDouble, double LimitePresionNValDouble, double LimitePresionMValDouble, double ConmuntaciontolNValDouble, double ConmuntaciontolMValDouble, double TiempoPresiontolNValDouble,
                double TiempoPresiontolMValDouble, double TNvcargavalDouble, double TMvcargavalDouble, double TNcargavalDouble, double TMcargavalDouble, double TNdescomvalDouble, double TMdescomvalDouble, double TNcontrapvalDouble,
                double TMcontrapvalDouble, double TNTiempdosvalDouble, double TMTiempdosvalDouble, double TNEnfriamvalDouble, double TMEnfriamvalDouble, double TNCiclovalDouble, double TMCiclovalDouble, double TNCojinvalDouble, double TMCojinvalDouble,
                    //REALES TOLERANCIAS
                double TiempoInyeccionR, double LimitePresionR, double VelocidadCargaR, double CargaR, double DescompresionR, double ContrapresionR, double TiempoR, double EnfriamientoR, double CicloR, double CojinR,
                //VALORES MATERIALES
                string MAT1REF, string MAT1NOM, string MAT1LOT, double MAT1TIEMP, double MAT1TIEMPREAL, double MAT1TEMP, double MAT1TEMPREAL, string MAT2REF, string MAT2NOM, string MAT2LOT, double MAT2TIEMP, double MAT2TIEMPREAL, double MAT2TEMP, double MAT2TEMPREAL, string MAT3REF, string MAT3NOM, string MAT3LOT, double MAT3TIEMP, double MAT3TIEMPREAL, double MAT3TEMP, double MAT3TEMPREAL,
                string COMP1REF, string COMP1NOM, string COMP1LOT, string COMP2REF, string COMP2NOM, string COMP2LOT, string COMP3REF, string COMP3NOM, string COMP3LOT, string COMP4REF, string COMP4NOM, string COMP4LOT, string COMP5REF, string COMP5NOM, string COMP5LOT, string COMP6REF, string COMP6NOM, string COMP6LOT, string COMP7REF, string COMP7NOM, string COMP7LOT,
                //VALORES ATEMPERADO
                string CircuitoF1, string CircuitoF2, string CircuitoF3, string CircuitoF4, string CircuitoF5, string CircuitoF6,
                string CaudalF1, string CaudalF2, string CaudalF3, string CaudalF4, string CaudalF5, string CaudalF6,
                string TemperaturaF1, string TemperaturaF2, string TemperaturaF3, string TemperaturaF4, string TemperaturaF5, string TemperaturaF6,
                string CircuitoM1, string CircuitoM2, string CircuitoM3, string CircuitoM4, string CircuitoM5, string CircuitoM6,
                string CaudalM1, string CaudalM2, string CaudalM3, string CaudalM4, string CaudalM5, string CaudalM6,
                string TemperaturaM1, string TemperaturaM2, string TemperaturaM3, string TemperaturaM4, string TemperaturaM5, string TemperaturaM6,
                    //REALES ATEMPERADO
                string CaudalF1R, string CaudalF2R, string CaudalF3R, string CaudalF4R, string CaudalF5R, string CaudalF6R,
                string TemperaturaF1R, string TemperaturaF2R, string TemperaturaF3R, string TemperaturaF4R, string TemperaturaF5R, string TemperaturaF6R,
                string CaudalM1R, string CaudalM2R, string CaudalM3R, string CaudalM4R, string CaudalM5R, string CaudalM6R,
                string TemperaturaM1R, string TemperaturaM2R, string TemperaturaM3R, string TemperaturaM4R, string TemperaturaM5R, string TemperaturaM6R,
                //AUDITORIA
                int Q1E, string Q1ENC, int Q2E, string Q2ENC, int Q3E, string Q3ENC, int Q4E, string Q4ENC, int Q5E, string Q5ENC, int Q6E, int Q6C, string Q6ENC, string Q6CAL,
                int Q7E, int Q7C, string Q7ENC, string Q7CAL, int Q8E, int Q8C, string Q8ENC, string Q8CAL, int Q9E, int Q9C, string Q9ENC, string Q9CAL, int Q10C, string Q10CAL,
                string QXFeedbackCambiador, string QXFeedbackProduccion, string QXFeedbackCalidad
            )

        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "UPDATE [SMARTH_DB].[dbo].[LIBERACION_Ficha] SET [Operario1]=" + Operario1 + ",[Operario1Horas]=" + Operario1Horas.ToString().Replace(',', '.') + ",[Operario1Nivel]='" + Operario1Nivel + "',[Operario1UltREV]='" + Operario1UltREV + "',[Operario1Notas]='" + Operario1Notas + "',[Operario2]=" + Operario2 + ",[Operario2Horas]=" + Operario2Horas.ToString().Replace(',', '.') + ",[Operario2Nivel]='" + Operario2Nivel + "',[Operario2UltREV]='" + Operario2UltREV + "',[Operario2Notas]='" + Operario2Notas + "'," +
                             "[Encargado]=" + Encargado + ",[EncargadoHoras]=" + EncargadoHoras.ToString().Replace(',', '.') + ",[Cambiador]=" + Cambiador + ",[CambiadorHoras]=" + CambiadorHoras.ToString().Replace(',', '.') + ",[Calidad]=" + Calidad + ",[CalidadHoras]=" + CalidadHoras.ToString().Replace(',', '.') + ",[CambiadorLiberado]=" + CambiadorLiberado + ",[FechaCambiadorLiberado]='" + FechaCambiadorLiberado + "',[ProduccionLiberado]=" + ProduccionLiberado + "," +
                             "[FechaProduccionLiberado]='" + FechaProduccionLiberado + "',[CalidadLiberado]=" + CalidadLiberado + ",[FechaCalidadLiberado]='" + FechaCalidadLiberado + "',[ResultadoLiberado]=" + ResultadoLiberado + ",[AccionLiberado]=" + AccionLiberado + ",[NotasLiberado]='" + NotasLiberado + "',[ReLiberacion]=" + ReLiberacion + ",[ENCNoconformidad]=" + ENCNoconformidad + ",[ENCDefectos]=" + ENCDefectos + ",[CALNoconformidad]=" + CALNoconformidad + ",[CALDefectos]=" + CALDefectos + ""+QUERYLiberarCambiadorHoraORI+""+QUERYLiberarEncargadoHoraORI+""+QUERYLiberacionCalidadHoraORI+",[ResultadoLOTES]="+ResultadoLOTES+ ",[ResultadoPARAM]="+ResultadoPARAM+"" +
                             " WHERE ORDEN = RTRIM('" + Orden + "')";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();
                Actualizar_ficha_postpresion(Orden, Referencia, Maquina, Version, CodMolde, P1, Tp1, P2, Tp2, P3, Tp3, P4, Tp4, P5, Tp5, P6, Ti6, P7, Tp7, P8, Tp8, P9, Tp9, P10, Tp10, tiempoPresion, conmutacion,
                                                                                            P1R, Tp1R, P2R, Tp2R, P3R, Tp3R, P4R, Tp4R, P5R, Tp5R, P6R, Ti6R, P7R, Tp7R, P8R, Tp8R, P9R, Tp9R, P10R, Tp10R, tiempoPresionREAL, conmutacionREAL);
                Actualizar_ficha_TempCamCaliente(Orden, Referencia, Maquina, Version, Z1, Z2, Z3, Z4, Z5, Z6, Z7, Z8, Z9, Z10, Z11, Z12, Z13, Z14, Z15, Z16, Z17, Z18, Z19, Z20,
                                                                                      Z1R, Z2R, Z3R, Z4R, Z5R, Z6R, Z7R, Z8R, Z9R, Z10R, Z11R, Z12R, Z13R, Z14R, Z15R, Z16R, Z17R, Z18R, Z19R, Z20R);
                Actualizar_ficha_TempCilindro(Orden, Referencia, Maquina, Version, CodMolde, boq, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, boqR, T1R, T2R, T3R, T4R, T5R, T6R, T7R, T8R, T9R, T10R, existeficha);

                Actualizar_ficha_Tolerancias(Orden, Referencia, Maquina, Version, TiempoInyeccion, LimitePresion, VelocidadCarga, Carga, Descompresion, Contrapresion, Tiempo, Enfriamiento, Ciclo, Cojin,
                                            TiempoInyeccionNValDouble, TiempoInyeccionMValDouble, LimitePresionNValDouble, LimitePresionMValDouble, ConmuntaciontolNValDouble, ConmuntaciontolMValDouble, TiempoPresiontolNValDouble,
                                            TiempoPresiontolMValDouble, TNvcargavalDouble, TMvcargavalDouble, TNcargavalDouble, TMcargavalDouble, TNdescomvalDouble, TMdescomvalDouble, TNcontrapvalDouble,
                                            TMcontrapvalDouble, TNTiempdosvalDouble, TMTiempdosvalDouble, TNEnfriamvalDouble, TMEnfriamvalDouble, TNCiclovalDouble, TMCiclovalDouble, TNCojinvalDouble, TMCojinvalDouble,
                                            TiempoInyeccionR, LimitePresionR, VelocidadCargaR, CargaR, DescompresionR, ContrapresionR, TiempoR, EnfriamientoR, CicloR, CojinR);
                Actualizar_ficha_Materiales(Orden, Referencia, Maquina, Version, MAT1REF, MAT1NOM, MAT1LOT, MAT1TIEMP, MAT1TIEMPREAL, MAT1TEMP, MAT1TEMPREAL, MAT2REF, MAT2NOM, MAT2LOT, MAT2TIEMP, MAT2TIEMPREAL, MAT2TEMP, MAT2TEMPREAL, MAT3REF, MAT3NOM, MAT3LOT, MAT3TIEMP, MAT3TIEMPREAL, MAT3TEMP, MAT3TEMPREAL,
                                            COMP1REF, COMP1NOM, COMP1LOT, COMP2REF, COMP2NOM, COMP2LOT, COMP3REF, COMP3NOM, COMP3LOT, COMP4REF, COMP4NOM, COMP4LOT, COMP5REF, COMP5NOM, COMP5LOT, COMP6REF, COMP6NOM, COMP6LOT, COMP7REF, COMP7NOM, COMP7LOT);
                Actualizar_ficha_Atemperado(Orden, Referencia, Maquina, Version, CircuitoF1, CircuitoF2, CircuitoF3, CircuitoF4, CircuitoF5, CircuitoF6, CaudalF1, CaudalF2, CaudalF3, CaudalF4, CaudalF5, CaudalF6, TemperaturaF1, TemperaturaF2, TemperaturaF3, TemperaturaF4, TemperaturaF5, TemperaturaF6,
                                            CircuitoM1, CircuitoM2, CircuitoM3, CircuitoM4, CircuitoM5, CircuitoM6, CaudalM1, CaudalM2, CaudalM3, CaudalM4, CaudalM5, CaudalM6, TemperaturaM1, TemperaturaM2, TemperaturaM3, TemperaturaM4, TemperaturaM5, TemperaturaM6,
                                            CaudalF1R, CaudalF2R, CaudalF3R, CaudalF4R, CaudalF5R, CaudalF6R, TemperaturaF1R, TemperaturaF2R, TemperaturaF3R, TemperaturaF4R, TemperaturaF5R, TemperaturaF6R,
                                            CaudalM1R, CaudalM2R, CaudalM3R, CaudalM4R, CaudalM5R, CaudalM6R, TemperaturaM1R, TemperaturaM2R, TemperaturaM3R, TemperaturaM4R, TemperaturaM5R, TemperaturaM6R);
                Actualizar_ficha_Auditoria(Orden, Referencia, Maquina, Version, Q1E, Q1ENC, Q2E, Q2ENC, Q3E, Q3ENC, Q4E, Q4ENC, Q5E, Q5ENC, Q6E, Q6C, Q6ENC, Q6CAL, Q7E, Q7C, Q7ENC, Q7CAL, Q8E, Q8C, Q8ENC, Q8CAL, Q9E, Q9C, Q9ENC, Q9CAL, Q10C, Q10CAL, QXFeedbackCambiador, QXFeedbackProduccion, QXFeedbackCalidad);


            }
            catch (Exception)
            {
                cnn_GP12.Close();
            }
        }

        public void ActualizarLiberacionFichaCambiador(string Orden, int Referencia, string Maquina, int Version, int CodMolde, int Operario1, double Operario1Horas, string Operario1Nivel, string Operario1UltREV, string Operario1Notas, int Operario2, double Operario2Horas, string Operario2Nivel, string Operario2UltREV, string Operario2Notas, int Encargado, double EncargadoHoras, int Cambiador,
        double CambiadorHoras, int Calidad, double CalidadHoras, int CambiadorLiberado, string FechaCambiadorLiberado, int ProduccionLiberado, string FechaProduccionLiberado, int CalidadLiberado, string FechaCalidadLiberado, int ResultadoLiberado, int AccionLiberado, string NotasLiberado, int ReLiberacion, int ENCNoconformidad, int ENCDefectos, int CALNoconformidad, int CALDefectos, string QUERYLiberarCambiadorHoraORI, int ResultadoLOTES, int ResultadoPARAM,
        //VALORES MATERIALES
        string MAT1REF, string MAT1NOM, string MAT1LOT, double MAT1TIEMP, double MAT1TIEMPREAL, double MAT1TEMP, double MAT1TEMPREAL, string MAT2REF, string MAT2NOM, string MAT2LOT, double MAT2TIEMP, double MAT2TIEMPREAL, double MAT2TEMP, double MAT2TEMPREAL, string MAT3REF, string MAT3NOM, string MAT3LOT, double MAT3TIEMP, double MAT3TIEMPREAL, double MAT3TEMP, double MAT3TEMPREAL,
        string COMP1REF, string COMP1NOM, string COMP1LOT, string COMP2REF, string COMP2NOM, string COMP2LOT, string COMP3REF, string COMP3NOM, string COMP3LOT, string COMP4REF, string COMP4NOM, string COMP4LOT, string COMP5REF, string COMP5NOM, string COMP5LOT, string COMP6REF, string COMP6NOM, string COMP6LOT, string COMP7REF, string COMP7NOM, string COMP7LOT,
        //AUDITORIA
        int Q1E, string Q1ENC, int Q2E, string Q2ENC, int Q3E, string Q3ENC, int Q4E, string Q4ENC, int Q5E, string Q5ENC, int Q6E, int Q6C, string Q6ENC, string Q6CAL,
        int Q7E, int Q7C, string Q7ENC, string Q7CAL, int Q8E, int Q8C, string Q8ENC, string Q8CAL, int Q9E, int Q9C, string Q9ENC, string Q9CAL, int Q10C, string Q10CAL,
        string QXFeedbackCambiador, string QXFeedbackProduccion, string QXFeedbackCalidad
        )

        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "UPDATE [SMARTH_DB].[dbo].[LIBERACION_Ficha] SET [Operario1]=" + Operario1 + ",[Operario1Horas]=" + Operario1Horas.ToString().Replace(',', '.') + ",[Operario1Nivel]='" + Operario1Nivel + "',[Operario1UltREV]='" + Operario1UltREV + "',[Operario1Notas]='" + Operario1Notas + "',[Operario2]=" + Operario2 + ",[Operario2Horas]=" + Operario2Horas.ToString().Replace(',', '.') + ",[Operario2Nivel]='" + Operario2Nivel + "',[Operario2UltREV]='" + Operario2UltREV + "',[Operario2Notas]='" + Operario2Notas + "'," +
                             "[Encargado]=" + Encargado + ",[EncargadoHoras]=" + EncargadoHoras.ToString().Replace(',', '.') + ",[Cambiador]=" + Cambiador + ",[CambiadorHoras]=" + CambiadorHoras.ToString().Replace(',', '.') + ",[Calidad]=" + Calidad + ",[CalidadHoras]=" + CalidadHoras.ToString().Replace(',', '.') + ",[CambiadorLiberado]=" + CambiadorLiberado + ",[FechaCambiadorLiberado]='" + FechaCambiadorLiberado + "',[ProduccionLiberado]=" + ProduccionLiberado + "," +
                             "[FechaProduccionLiberado]='" + FechaProduccionLiberado + "',[CalidadLiberado]=" + CalidadLiberado + ",[FechaCalidadLiberado]='" + FechaCalidadLiberado + "',[ResultadoLiberado]=" + ResultadoLiberado + ",[AccionLiberado]=" + AccionLiberado + ",[NotasLiberado]='" + NotasLiberado + "',[ReLiberacion]=" + ReLiberacion + ",[ENCNoconformidad]=" + ENCNoconformidad + ",[ENCDefectos]=" + ENCDefectos + ",[CALNoconformidad]=" + CALNoconformidad + ",[CALDefectos]=" + CALDefectos + ""+QUERYLiberarCambiadorHoraORI + ",[ResultadoLOTES]=" + ResultadoLOTES + ",[ResultadoPARAM]=" + ResultadoPARAM + "" +
                             " WHERE ORDEN = RTRIM('" + Orden + "')";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();
                Actualizar_ficha_Materiales(Orden, Referencia, Maquina, Version, MAT1REF, MAT1NOM, MAT1LOT, MAT1TIEMP, MAT1TIEMPREAL, MAT1TEMP, MAT1TEMPREAL, MAT2REF, MAT2NOM, MAT2LOT, MAT2TIEMP, MAT2TIEMPREAL, MAT2TEMP, MAT2TEMPREAL, MAT3REF, MAT3NOM, MAT3LOT, MAT3TIEMP, MAT3TIEMPREAL, MAT3TEMP, MAT3TEMPREAL,
                                            COMP1REF, COMP1NOM, COMP1LOT, COMP2REF, COMP2NOM, COMP2LOT, COMP3REF, COMP3NOM, COMP3LOT, COMP4REF, COMP4NOM, COMP4LOT, COMP5REF, COMP5NOM, COMP5LOT, COMP6REF, COMP6NOM, COMP6LOT, COMP7REF, COMP7NOM, COMP7LOT);
                Actualizar_ficha_Auditoria(Orden, Referencia, Maquina, Version, Q1E, Q1ENC, Q2E, Q2ENC, Q3E, Q3ENC, Q4E, Q4ENC, Q5E, Q5ENC, Q6E, Q6C, Q6ENC, Q6CAL, Q7E, Q7C, Q7ENC, Q7CAL, Q8E, Q8C, Q8ENC, Q8CAL, Q9E, Q9C, Q9ENC, Q9CAL, Q10C, Q10CAL, QXFeedbackCambiador, QXFeedbackProduccion, QXFeedbackCalidad);


            }
            catch (Exception)
            {
                cnn_GP12.Close();
            }
        }


        public void ActualizarLiberacionFichaCalidad(string Orden, int Referencia, string Maquina, int Version, int CodMolde, int Operario1, double Operario1Horas, string Operario1Nivel, string Operario1UltREV, string Operario1Notas, int Operario2, double Operario2Horas, string Operario2Nivel, string Operario2UltREV, string Operario2Notas, int Encargado, double EncargadoHoras, int Cambiador,
        double CambiadorHoras, int Calidad, double CalidadHoras, int CambiadorLiberado, string FechaCambiadorLiberado, int ProduccionLiberado, string FechaProduccionLiberado, int CalidadLiberado, string FechaCalidadLiberado, int ResultadoLiberado, int AccionLiberado, string NotasLiberado, int ReLiberacion, int ENCNoconformidad, int ENCDefectos, int CALNoconformidad, int CALDefectos, string QUERYLiberarCalidadHoraORI,
        //VALORES MATERIALES
        string MAT1REF, string MAT1NOM, string MAT1LOT, double MAT1TIEMP, double MAT1TIEMPREAL, double MAT1TEMP, double MAT1TEMPREAL, string MAT2REF, string MAT2NOM, string MAT2LOT, double MAT2TIEMP, double MAT2TIEMPREAL, double MAT2TEMP, double MAT2TEMPREAL, string MAT3REF, string MAT3NOM, string MAT3LOT, double MAT3TIEMP, double MAT3TIEMPREAL, double MAT3TEMP, double MAT3TEMPREAL,
        string COMP1REF, string COMP1NOM, string COMP1LOT, string COMP2REF, string COMP2NOM, string COMP2LOT, string COMP3REF, string COMP3NOM, string COMP3LOT, string COMP4REF, string COMP4NOM, string COMP4LOT, string COMP5REF, string COMP5NOM, string COMP5LOT, string COMP6REF, string COMP6NOM, string COMP6LOT, string COMP7REF, string COMP7NOM, string COMP7LOT,
        //AUDITORIA
        int Q1E, string Q1ENC, int Q2E, string Q2ENC, int Q3E, string Q3ENC, int Q4E, string Q4ENC, int Q5E, string Q5ENC, int Q6E, int Q6C, string Q6ENC, string Q6CAL,
        int Q7E, int Q7C, string Q7ENC, string Q7CAL, int Q8E, int Q8C, string Q8ENC, string Q8CAL, int Q9E, int Q9C, string Q9ENC, string Q9CAL, int Q10C, string Q10CAL,
        string QXFeedbackCambiador, string QXFeedbackProduccion, string QXFeedbackCalidad
        )

        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "UPDATE [SMARTH_DB].[dbo].[LIBERACION_Ficha] SET [Operario1]=" + Operario1 + ",[Operario1Horas]=" + Operario1Horas.ToString().Replace(',', '.') + ",[Operario1Nivel]='" + Operario1Nivel + "',[Operario1UltREV]='" + Operario1UltREV + "',[Operario1Notas]='" + Operario1Notas + "',[Operario2]=" + Operario2 + ",[Operario2Horas]=" + Operario2Horas.ToString().Replace(',', '.') + ",[Operario2Nivel]='" + Operario2Nivel + "',[Operario2UltREV]='" + Operario2UltREV + "',[Operario2Notas]='" + Operario2Notas + "'," +
                             "[Encargado]=" + Encargado + ",[EncargadoHoras]=" + EncargadoHoras.ToString().Replace(',', '.') + ",[Cambiador]=" + Cambiador + ",[CambiadorHoras]=" + CambiadorHoras.ToString().Replace(',', '.') + ",[Calidad]=" + Calidad + ",[CalidadHoras]=" + CalidadHoras.ToString().Replace(',', '.') + ",[CambiadorLiberado]=" + CambiadorLiberado + ",[FechaCambiadorLiberado]='" + FechaCambiadorLiberado + "',[ProduccionLiberado]=" + ProduccionLiberado + "," +
                             "[FechaProduccionLiberado]='" + FechaProduccionLiberado + "',[CalidadLiberado]=" + CalidadLiberado + ",[FechaCalidadLiberado]='" + FechaCalidadLiberado + "',[ResultadoLiberado]=" + ResultadoLiberado + ",[AccionLiberado]=" + AccionLiberado + ",[NotasLiberado]='" + NotasLiberado + "',[ReLiberacion]=" + ReLiberacion + ",[ENCNoconformidad]=" + ENCNoconformidad + ",[ENCDefectos]=" + ENCDefectos + ",[CALNoconformidad]=" + CALNoconformidad + ",[CALDefectos]=" + CALDefectos + "" + QUERYLiberarCalidadHoraORI + "" +
                             " WHERE ORDEN = RTRIM('" + Orden + ")";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();
                Actualizar_ficha_Materiales(Orden, Referencia, Maquina, Version, MAT1REF, MAT1NOM, MAT1LOT, MAT1TIEMP, MAT1TIEMPREAL, MAT1TEMP, MAT1TEMPREAL, MAT2REF, MAT2NOM, MAT2LOT, MAT2TIEMP, MAT2TIEMPREAL, MAT2TEMP, MAT2TEMPREAL, MAT3REF, MAT3NOM, MAT3LOT, MAT3TIEMP, MAT3TIEMPREAL, MAT3TEMP, MAT3TEMPREAL,
                                            COMP1REF, COMP1NOM, COMP1LOT, COMP2REF, COMP2NOM, COMP2LOT, COMP3REF, COMP3NOM, COMP3LOT, COMP4REF, COMP4NOM, COMP4LOT, COMP5REF, COMP5NOM, COMP5LOT, COMP6REF, COMP6NOM, COMP6LOT, COMP7REF, COMP7NOM, COMP7LOT);
                Actualizar_ficha_Auditoria(Orden, Referencia, Maquina, Version, Q1E, Q1ENC, Q2E, Q2ENC, Q3E, Q3ENC, Q4E, Q4ENC, Q5E, Q5ENC, Q6E, Q6C, Q6ENC, Q6CAL, Q7E, Q7C, Q7ENC, Q7CAL, Q8E, Q8C, Q8ENC, Q8CAL, Q9E, Q9C, Q9ENC, Q9CAL, Q10C, Q10CAL, QXFeedbackCambiador, QXFeedbackProduccion, QXFeedbackCalidad);


            }
            catch (Exception)
            {
                cnn_GP12.Close();
            }
        }

        public void Actualizar_ficha_postpresion(string Orden, int referencia, string maquina, int version, int CodMolde, double P1, double T1, double P2, double T2, double P3, double T3, double P4, double T4, double P5, double T5,
                                          double P6, double T6, double P7, double T7, double P8, double T8, double P9, double T9, double P10, double T10, string tiempoPresion, string conmutacion,
                                          double P1R, double T1R, double P2R, double T2R, double P3R, double T3R, double P4R, double T4R, double P5R, double T5R,
                                          double P6R, double T6R, double P7R, double T7R, double P8R, double T8R, double P9R, double T9R, double P10R, double T10R, string tiempoPresionREAL, string conmutacionREAL)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                if (tiempoPresion == null)
                    tiempoPresion = "";
                if (conmutacion == null)
                    conmutacion = "";
                string sql = "UPDATE [SMARTH_DB].[dbo].[LIBERACION_Postpresion] SET P1 = " + P1.ToString().Replace(',', '.') + " , T1 = " + T1.ToString().Replace(',', '.') + ", P2 = " + P2.ToString().Replace(',', '.') + ", T2 = " + T2.ToString().Replace(',', '.') + ", P3 = " + P3.ToString().Replace(',', '.') + ", T3 = " + T3.ToString().Replace(',', '.') + "," +
                                                                                    "P4 = " + P4.ToString().Replace(',', '.') + ", T4 = " + T4.ToString().Replace(',', '.') + ", P5 = " + P5.ToString().Replace(',', '.') + ", T5 = " + T5.ToString().Replace(',', '.') + ", P6 = " + P6.ToString().Replace(',', '.') + ", T6 = " + T6.ToString().Replace(',', '.') + "," +
                                                                                    "P7 = " + P7.ToString().Replace(',', '.') + ", T7 = " + T7.ToString().Replace(',', '.') + ", P8 = " + P8.ToString().Replace(',', '.') + ", T8 =" + T8.ToString().Replace(',', '.') + ", P9 = " + P9.ToString().Replace(',', '.') + ", T9 = " + T9.ToString().Replace(',', '.') + "," +
                                                                                    "P10 = " + P10.ToString().Replace(',', '.') + ", T10 = " + T10.ToString().Replace(',', '.') + ", TiempoPresion = '" + tiempoPresion.ToString().Replace(',', '.') + "', Conmutacion = '" + conmutacion.ToString().Replace(',', '.') + "'," +
                                                                                    "LIBP1 = " + P1R.ToString().Replace(',', '.') + ", LIBT1 = " + T1R.ToString().Replace(',', '.') + ", LIBP2 = " + P2R.ToString().Replace(',', '.') + ", LIBT2 = " + T2R.ToString().Replace(',', '.') + ", LIBP3 = " + P3R.ToString().Replace(',', '.') + ", LIBT3 = " + T3R.ToString().Replace(',', '.') + "," +
                                                                                    "LIBP4 = " + P4R.ToString().Replace(',', '.') + ", LIBT4 = " + T4R.ToString().Replace(',', '.') + ", LIBP5 = " + P5R.ToString().Replace(',', '.') + ", LIBT5 = " + T5R.ToString().Replace(',', '.') + ", LIBP6 = " + P6R.ToString().Replace(',', '.') + ", LIBT6 = " + T6R.ToString().Replace(',', '.') + "," +
                                                                                    "LIBP7 = " + P7R.ToString().Replace(',', '.') + ", LIBT7 = " + T7R.ToString().Replace(',', '.') + ", LIBP8 = " + P8R.ToString().Replace(',', '.') + ", LIBT8 =" + T8R.ToString().Replace(',', '.') + ", LIBP9 = " + P9R.ToString().Replace(',', '.') + ", LIBT9 = " + T9R.ToString().Replace(',', '.') + "," +
                                                                                    "LIBP10 = " + P10R.ToString().Replace(',', '.') + ", LIBT10 = " + T10R.ToString().Replace(',', '.') + ", LIBTiempoPresion = '" + tiempoPresionREAL.ToString().Replace(',', '.') + "', LIBConmutacion = '" + conmutacionREAL.ToString().Replace(',', '.') + "'" +
                                                                                    " WHERE ORDEN = RTRIM('" + Orden + "')";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();
            }
            catch (Exception)
            {
                cnn_GP12.Close();
            }
        }
        public void Actualizar_ficha_TempCamCaliente(string Orden, int referencia, string maquina, int version, double Z1, double Z2, double Z3, double Z4, double Z5, double Z6, double Z7, double Z8, double Z9, double Z10,
                                           double Z11, double Z12, double Z13, double Z14, double Z15, double Z16, double Z17, double Z18, double Z19, double Z20,
                                           double Z1R, double Z2R, double Z3R, double Z4R, double Z5R, double Z6R, double Z7R, double Z8R, double Z9R, double Z10R,
                                           double Z11R, double Z12R, double Z13R, double Z14R, double Z15R, double Z16R, double Z17R, double Z18R, double Z19R, double Z20R)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "UPDATE [SMARTH_DB].[dbo].[LIBERACION_TempCamCaliente] SET Z1 = " + Z1.ToString().Replace(',', '.') + ", Z2 = " + Z2.ToString().Replace(',', '.') + " , Z3 = " + Z3.ToString().Replace(',', '.') + ", Z4 = " + Z4.ToString().Replace(',', '.') + ", Z5 = " + Z5.ToString().Replace(',', '.') + ", Z6 = " + Z6.ToString().Replace(',', '.') + ", Z7 = " + Z7.ToString().Replace(',', '.') + ","+
                                                                                     "Z8 = " + Z8.ToString().Replace(',', '.') + ", Z9 = " + Z9.ToString().Replace(',', '.') + ", Z10 = " + Z10.ToString().Replace(',', '.') + ", Z11 = " + Z11.ToString().Replace(',', '.') + ", Z12 = " + Z12.ToString().Replace(',', '.') + ", Z13 = " + Z13.ToString().Replace(',', '.') + ", Z14 = " + Z14.ToString().Replace(',', '.') + ","+
                                                                                     "Z15 = " + Z15.ToString().Replace(',', '.') + ", Z16 = " + Z16.ToString().Replace(',', '.') + ", Z17 = " + Z17.ToString().Replace(',', '.') + " , Z18 = " + Z18.ToString().Replace(',', '.') + ", Z19 = " + Z19.ToString().Replace(',', '.') + ", Z20 = " + Z20.ToString().Replace(',', '.') + "," +
                                                                                     "LIBZ1 = " + Z1R.ToString().Replace(',', '.') + ", LIBZ2 = " + Z2R.ToString().Replace(',', '.') + ", LIBZ3 = " + Z3R.ToString().Replace(',', '.') + ", LIBZ4 = " + Z4R.ToString().Replace(',', '.') + ", LIBZ5 = " + Z5R.ToString().Replace(',', '.') + ", LIBZ6 = " + Z6R.ToString().Replace(',', '.') + ", LIBZ7 = " + Z7R.ToString().Replace(',', '.') + ","+
                                                                                     "LIBZ8 = " + Z8R.ToString().Replace(',', '.') + ", LIBZ9 = " + Z9R.ToString().Replace(',', '.') + ", LIBZ10 = " + Z10R.ToString().Replace(',', '.') + ", LIBZ11 = " + Z11R.ToString().Replace(',', '.') + ", LIBZ12 = " + Z12R.ToString().Replace(',', '.') + ", LIBZ13 = " + Z13R.ToString().Replace(',', '.') + ", LIBZ14 = " + Z14R.ToString().Replace(',', '.') + "," +
                                                                                     "LIBZ15 = " + Z15R.ToString().Replace(',', '.') + ", LIBZ16 = " + Z16R.ToString().Replace(',', '.') + ", LIBZ17 = " + Z17R.ToString().Replace(',', '.') + " , LIBZ18 = " + Z18R.ToString().Replace(',', '.') + ", LIBZ19 = " + Z19R.ToString().Replace(',', '.') + ", LIBZ20 = " + Z20R.ToString().Replace(',', '.') + " "+
                                                                                     " WHERE ORDEN = RTRIM('" + Orden + "')";

                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();
            }
            catch (Exception)
            {
                cnn_GP12.Close();
            }
        }
        public void Actualizar_ficha_TempCilindro(string Orden, int referencia, string maquina, int version, int CodMolde, double boq, double T1, double T2, double T3, double T4, double T5, double T6, double T7, double T8, double T9, double T10,
                                                                double boqR, double T1R, double T2R, double T3R, double T4R, double T5R, double T6R, double T7R, double T8R, double T9R, double T10R, string existeficha)

        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "UPDATE [SMARTH_DB].[dbo].[LIBERACION_TempCilindro] SET Boq = " + boq.ToString().Replace(',', '.') + ", T1 = " + T1.ToString().Replace(',', '.') + ", T2 = " + T2.ToString().Replace(',', '.') + ", T3 = " + T3.ToString().Replace(',', '.') + ", T4 = " + T4.ToString().Replace(',', '.') + ", T5 = " + T5.ToString().Replace(',', '.') + ", T6 = " + T6.ToString().Replace(',', '.') + ", T7 = " + T7.ToString().Replace(',', '.') + ", T8 = " + T8.ToString().Replace(',', '.') + ", T9 = " + T9.ToString().Replace(',', '.') + ", T10 = " + T10.ToString().Replace(',', '.') + "," +
                                                                                          "LIBBoq = " + boqR.ToString().Replace(',', '.') + ", LIBT1 = " + T1R.ToString().Replace(',', '.') + ", LIBT2 = " + T2R.ToString().Replace(',', '.') + ", LIBT3 = " + T3R.ToString().Replace(',', '.') + ", LIBT4 = " + T4R.ToString().Replace(',', '.') + ", LIBT5 = " + T5R.ToString().Replace(',', '.') + ", LIBT6 = " + T6R.ToString().Replace(',', '.') + ", LIBT7 = " + T7R.ToString().Replace(',', '.') + ", LIBT8 = " + T8R.ToString().Replace(',', '.') + ", LIBT9 = " + T9R.ToString().Replace(',', '.') + ", LIBT10 = " + T10R.ToString().Replace(',', '.') + ", EXISTEFICHA = "+ existeficha + " " +
                                                                                          " WHERE ORDEN = RTRIM('" + Orden + "')";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();
            }
            catch (Exception)
            {
                cnn_GP12.Close();
            }
        }
        public void Actualizar_ficha_Tolerancias(string Orden, int referencia, string maquina, int version, double TiempoInyeccion, double LimitePresion, double VelocidadCarga, double Carga, double Descompresion, double Contrapresion, double Tiempo, double Enfriamiento, double Ciclo, double Cojin,
                   double TiempoInyeccionNValDouble, double TiempoInyeccionMValDouble, double LimitePresionNValDouble, double LimitePresionMValDouble, double ConmuntaciontolNValDouble, double ConmuntaciontolMValDouble, double TiempoPresiontolNValDouble,
                   double TiempoPresiontolMValDouble, double TNvcargavalDouble, double TMvcargavalDouble, double TNcargavalDouble, double TMcargavalDouble, double TNdescomvalDouble, double TMdescomvalDouble, double TNcontrapvalDouble,
                   double TMcontrapvalDouble, double TNTiempdosvalDouble, double TMTiempdosvalDouble, double TNEnfriamvalDouble, double TMEnfriamvalDouble, double TNCiclovalDouble, double TMCiclovalDouble, double TNCojinvalDouble, double TMCojinvalDouble,
                   double TiempoInyeccionR, double LimitePresionR, double VelocidadCargaR, double CargaR, double DescompresionR, double ContrapresionR, double TiempoR, double EnfriamientoR, double CicloR, double CojinR)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "UPDATE [SMARTH_DB].[dbo].[LIBERACION_Tolerancias] SET [TiempoInyeccion] = " + TiempoInyeccion.ToString().Replace(',', '.') + ",[LimitePresion] = " + LimitePresion.ToString().Replace(',', '.') + ",[VelocidadCarga] = " + VelocidadCarga.ToString().Replace(',', '.') + ",[Carga]=" + Carga.ToString().Replace(',', '.') + ",[Descompresion]=" + Descompresion.ToString().Replace(',', '.') + ",[Contrapresion]=" + Contrapresion.ToString().Replace(',', '.') + ",[Tiempo]=" + Tiempo.ToString().Replace(',', '.') + ",[Enfriamiento]=" + Enfriamiento.ToString().Replace(',', '.') + ",[Ciclo]=" + Ciclo.ToString().Replace(',', '.') + ",[Cojin]=" + Cojin.ToString().Replace(',', '.') + "," +
                                  "TiempoInyeccionNVal = " + TiempoInyeccionNValDouble.ToString().Replace(',', '.') + ", TiempoInyeccionMVal = " + TiempoInyeccionMValDouble.ToString().Replace(',', '.') + ", LimitePresionNVal = " + LimitePresionNValDouble.ToString().Replace(',', '.') + ", LimitePresionMVal = " + LimitePresionMValDouble.ToString().Replace(',', '.') + ", ConmuntaciontolNVal = " + ConmuntaciontolNValDouble.ToString().Replace(',', '.') + ", ConmuntaciontolMVal = " + ConmuntaciontolMValDouble.ToString().Replace(',', '.') + ", TiempoPresiontolNVal=" + TiempoPresiontolNValDouble.ToString().Replace(',', '.') + ", TiempoPresiontolMVal=" + TiempoPresiontolMValDouble.ToString().Replace(',', '.') + ", TNvcargaval=" + TNvcargavalDouble.ToString().Replace(',', '.') + ", TMvcargaval=" + TMvcargavalDouble.ToString().Replace(',', '.') + ","+
                                  "TNcargaval=" + TNcargavalDouble.ToString().Replace(',', '.') + ", TMcargaval=" + TMcargavalDouble.ToString().Replace(',', '.') + ", TNdescomval=" + TNdescomvalDouble.ToString().Replace(',', '.') + ", TMdescomval=" + TMdescomvalDouble.ToString().Replace(',', '.') + ", TNcontrapval=" + TNcontrapvalDouble.ToString().Replace(',', '.') + ", TMcontrapval=" + TMcontrapvalDouble.ToString().Replace(',', '.') + ", TNTiempdosval=" + TNTiempdosvalDouble.ToString().Replace(',', '.') + ", TMTiempdosval=" + TMTiempdosvalDouble.ToString().Replace(',', '.') + ", TNEnfriamval=" + TNEnfriamvalDouble.ToString().Replace(',', '.') + ", TMEnfriamval=" + TMEnfriamvalDouble.ToString().Replace(',', '.') + ", TNCicloval=" + TNCiclovalDouble.ToString().Replace(',', '.') + ", TMCicloval=" + TMCiclovalDouble.ToString().Replace(',', '.') + ", TNCojinval=" + TNCojinvalDouble.ToString().Replace(',', '.') + ", TMCojinval=" + TMCojinvalDouble.ToString().Replace(',', '.') + "," +
                                  "[LIBTiempoInyeccion] = " + TiempoInyeccionR.ToString().Replace(',', '.') + ",[LIBLimitePresion] = " + LimitePresionR.ToString().Replace(',', '.') + ",[LIBVelocidadCarga] = " + VelocidadCargaR.ToString().Replace(',', '.') + ",[LIBCarga]=" + CargaR.ToString().Replace(',', '.') + ",[LIBDescompresion]=" + DescompresionR.ToString().Replace(',', '.') + ",[LIBContrapresion]=" + ContrapresionR.ToString().Replace(',', '.') + ",[LIBTiempo]=" + TiempoR.ToString().Replace(',', '.') + ",[LIBEnfriamiento]=" + EnfriamientoR.ToString().Replace(',', '.') + ",[LIBCiclo]=" + CicloR.ToString().Replace(',', '.') + ",[LIBCojin]=" + CojinR.ToString().Replace(',', '.') + "" +
                                  " WHERE ORDEN = RTRIM('" + Orden + "')";

                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();
            }
            catch (Exception)
            {
                cnn_GP12.Close();
            }
        }

        public void Actualizar_ficha_Materiales(string Orden, int referencia, string maquina, int version, string MAT1REF, string MAT1NOM, string MAT1LOT, double MAT1TIEMP, double MAT1TIEMPREAL, double MAT1TEMP, double MAT1TEMPREAL, string MAT2REF, string MAT2NOM, string MAT2LOT, double MAT2TIEMP, double MAT2TIEMPREAL, double MAT2TEMP, double MAT2TEMPREAL, string MAT3REF, string MAT3NOM, string MAT3LOT, double MAT3TIEMP, double MAT3TIEMPREAL, double MAT3TEMP, double MAT3TEMPREAL,
                string COMP1REF, string COMP1NOM, string COMP1LOT, string COMP2REF, string COMP2NOM, string COMP2LOT, string COMP3REF, string COMP3NOM, string COMP3LOT, string COMP4REF, string COMP4NOM, string COMP4LOT, string COMP5REF, string COMP5NOM, string COMP5LOT, string COMP6REF, string COMP6NOM, string COMP6LOT, string COMP7REF, string COMP7NOM, string COMP7LOT)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "UPDATE [SMARTH_DB].[dbo].[LIBERACION_Estructura] SET [MAT1REF] = '" + MAT1REF + "',[MAT1NOM] = '" + MAT1NOM + "',[MAT1LOT]='" + MAT1LOT + "',[MAT1TIEMP]=" + MAT1TIEMP.ToString().Replace(',', '.') + ",[MAT1TIEMPREAL]=" + MAT1TIEMPREAL.ToString().Replace(',', '.') + ",[MAT1TEMP]=" + MAT1TEMP.ToString().Replace(',', '.') + ",[MAT1TEMPREAL]=" + MAT1TEMPREAL.ToString().Replace(',', '.') + ",[MAT2REF]='" + MAT2REF + "',[MAT2NOM]='" + MAT2NOM + "',[MAT2LOT]='" + MAT2LOT + "',[MAT2TIEMP]=" + MAT2TIEMP.ToString().Replace(',', '.') + ",[MAT2TIEMPREAL]=" + MAT2TIEMPREAL.ToString().Replace(',', '.') + ",[MAT2TEMP]=" + MAT2TEMP.ToString().Replace(',', '.') + ",[MAT2TEMPREAL]=" + MAT2TEMPREAL.ToString().Replace(',', '.') + ","+
                                                                                  "[MAT3REF] = '" + MAT3REF + "',[MAT3NOM] = '" + MAT3NOM + "',[MAT3LOT]='" + MAT3LOT + "',[MAT3TIEMP]=" + MAT3TIEMP.ToString().Replace(',', '.') + ",[MAT3TIEMPREAL]=" + MAT3TIEMPREAL.ToString().Replace(',', '.') + ",[MAT3TEMP]=" + MAT3TEMP.ToString().Replace(',', '.') + ",[MAT3TEMPREAL]=" + MAT3TEMPREAL.ToString().Replace(',', '.') + ",[COMP1REF]='" + COMP1REF + "',[COMP1NOM]='" + COMP1NOM + "',[COMP1LOT]='" + COMP1LOT + "',[COMP2REF]='" + COMP2REF + "',[COMP2NOM]='" + COMP2NOM + "',[COMP2LOT]='" + COMP2LOT + "',[COMP3REF]='" + COMP3REF + "',[COMP3NOM]='" + COMP3NOM + "',[COMP3LOT]='" + COMP3LOT + "',"+
                                                                                  "[COMP4REF] = '" + COMP4REF + "',[COMP4NOM] = '" + COMP4NOM + "',[COMP4LOT] = '" + COMP4LOT + "',[COMP5REF] = '" + COMP5REF + "',[COMP5NOM]='" + COMP5NOM + "',[COMP5LOT]='" + COMP5LOT + "',[COMP6REF]='" + COMP6REF + "',[COMP6NOM]='" + COMP6NOM + "',[COMP6LOT]='" + COMP6LOT + "',[COMP7REF]='" + COMP7REF + "',[COMP7NOM]='" + COMP7NOM + "',[COMP7LOT]='" + COMP7LOT + "'" +
                                                                                  " WHERE ORDEN = RTRIM('" + Orden + "')";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();
            }
            catch (Exception)
            {
                cnn_GP12.Close();
            }
        }

        public void Actualizar_ficha_Atemperado(string Orden, int referencia, string maquina, int version,
                                  string CircuitoF1, string CircuitoF2, string CircuitoF3, string CircuitoF4, string CircuitoF5, string CircuitoF6,
                                  string CaudalF1, string CaudalF2, string CaudalF3, string CaudalF4, string CaudalF5, string CaudalF6,
                                  string TemperaturaF1, string TemperaturaF2, string TemperaturaF3, string TemperaturaF4, string TemperaturaF5, string TemperaturaF6,
                                  string CircuitoM1, string CircuitoM2, string CircuitoM3, string CircuitoM4, string CircuitoM5, string CircuitoM6,
                                  string CaudalM1, string CaudalM2, string CaudalM3, string CaudalM4, string CaudalM5, string CaudalM6,
                                  string TemperaturaM1, string TemperaturaM2, string TemperaturaM3, string TemperaturaM4, string TemperaturaM5, string TemperaturaM6,
                                  string CaudalF1R, string CaudalF2R, string CaudalF3R, string CaudalF4R, string CaudalF5R, string CaudalF6R,
                                  string TemperaturaF1R, string TemperaturaF2R, string TemperaturaF3R, string TemperaturaF4R, string TemperaturaF5R, string TemperaturaF6R,
                                  string CaudalM1R, string CaudalM2R, string CaudalM3R, string CaudalM4R, string CaudalM5R, string CaudalM6R,
                                  string TemperaturaM1R, string TemperaturaM2R, string TemperaturaM3R, string TemperaturaM4R, string TemperaturaM5R, string TemperaturaM6R)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "UPDATE [SMARTH_DB].[dbo].[LIBERACION_Atemperados] SET CircuitoF1='" + CircuitoF1 + "', CircuitoF2='" + CircuitoF2 + "', CircuitoF3='" + CircuitoF3 + "', CircuitoF4='" + CircuitoF4 + "', CircuitoF5='" + CircuitoF5 + "', CircuitoF6='" + CircuitoF6 + "'," +
                                                                                    "CaudalF1='" + CaudalF1 + "', CaudalF2='" + CaudalF2 + "', CaudalF3='" + CaudalF3 + "', CaudalF4='" + CaudalF4 + "', CaudalF5='" + CaudalF5 + "', CaudalF6='" + CaudalF6 + "'," +
                                                                                    "TemperaturaF1='" + TemperaturaF1 + "', TemperaturaF2='" + TemperaturaF2 + "', TemperaturaF3='" + TemperaturaF3 + "', TemperaturaF4='" + TemperaturaF4 + "', TemperaturaF5='" + TemperaturaF5 + "', TemperaturaF6='" + TemperaturaF6 + "'," +
                                                                                    "CircuitoM1 = '" + CircuitoM1 + "', CircuitoM2='" + CircuitoM2 + "', CircuitoM3='" + CircuitoM3 + "', CircuitoM4='" + CircuitoM4 + "', CircuitoM5='" + CircuitoM5 + "', CircuitoM6='" + CircuitoM6 + "'," +
                                                                                    "CaudalM1='" + CaudalM1 + "', CaudalM2='" + CaudalM2 + "', CaudalM3='" + CaudalM3 + "', CaudalM4='" + CaudalM4 + "', CaudalM5='" + CaudalM5 + "', CaudalM6='" + CaudalM6 + "'," +
                                                                                    "TemperaturaM1='" + TemperaturaM1 + "', TemperaturaM2='" + TemperaturaM2 + "', TemperaturaM3='" + TemperaturaM3 + "', TemperaturaM4='" + TemperaturaM4 + "', TemperaturaM5='" + TemperaturaM5 + "', TemperaturaM6='" + TemperaturaM6 + "'," +
                                                                                    "LIBCaudalF1='" + CaudalF1R + "', LIBCaudalF2='" + CaudalF2R + "', LIBCaudalF3='" + CaudalF3R + "', LIBCaudalF4='" + CaudalF4R + "', LIBCaudalF5='" + CaudalF5R + "', LIBCaudalF6='" + CaudalF6R + "'," +
                                                                                    "LIBTemperaturaF1='" + TemperaturaF1R + "', LIBTemperaturaF2='" + TemperaturaF2R + "', LIBTemperaturaF3='" + TemperaturaF3R + "', LIBTemperaturaF4='" + TemperaturaF4R + "', LIBTemperaturaF5='" + TemperaturaF5R + "', LIBTemperaturaF6='" + TemperaturaF6R + "'," +
                                                                                    "LIBCaudalM1='" + CaudalM1R + "', LIBCaudalM2='" + CaudalM2R + "', LIBCaudalM3='" + CaudalM3R + "', LIBCaudalM4='" + CaudalM4R + "', LIBCaudalM5='" + CaudalM5R + "', LIBCaudalM6='" + CaudalM6R + "'," +
                                                                                    "LIBTemperaturaM1='" + TemperaturaM1R + "', LIBTemperaturaM2='" + TemperaturaM2R + "', LIBTemperaturaM3='" + TemperaturaM3R + "', LIBTemperaturaM4='" + TemperaturaM4R + "', LIBTemperaturaM5='" + TemperaturaM5R + "', LIBTemperaturaM6='" + TemperaturaM6R + "'" +
                                                                                    " WHERE ORDEN = RTRIM('" + Orden + "')";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();
            }
            catch (Exception)
            {
                cnn_GP12.Close();
            }
        }

        public void Actualizar_ficha_Auditoria(string Orden, int referencia, string maquina, int version,
                                int Q1E, string Q1ENC, int Q2E, string Q2ENC, int Q3E, string Q3ENC, int Q4E, string Q4ENC, int Q5E, string Q5ENC, int Q6E, int Q6C, string Q6ENC, string Q6CAL,
                                int Q7E, int Q7C, string Q7ENC, string Q7CAL, int Q8E, int Q8C, string Q8ENC, string Q8CAL, int Q9E, int Q9C, string Q9ENC, string Q9CAL, int Q10C, string Q10CAL, string QXFeedbackCambiador, string QXFeedbackProduccion, string QXFeedbackCalidad)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "UPDATE [SMARTH_DB].[dbo].[LIBERACION_Auditoria] SET [Q1E]=" + Q1E + ",[Q1ENC]='" + Q1ENC + "',[Q2E]=" + Q2E + ",[Q2ENC]='" + Q2ENC + "',[Q3E]=" + Q3E + ",[Q3ENC]='" + Q3ENC + "',[Q4E]=" + Q4E + ",[Q4ENC]='" + Q4ENC + "',[Q5E]=" + Q5E + ",[Q5ENC]='" + Q5ENC + "',[Q6E]=" + Q6E + ",[Q6C]=" + Q6C + ",[Q6ENC]='" + Q6ENC + "',[Q6CAL]='" + Q6CAL + "',"+
                                                                                 "[Q7E]=" + Q7E + ",[Q7C]=" + Q7C + ",[Q7ENC]='" + Q7ENC + "',[Q7CAL]='" + Q7CAL + "',[Q8E]=" + Q8E + ",[Q8C]=" + Q8C + ",[Q8ENC]='" + Q8ENC + "',[Q8CAL]='" + Q8CAL + "',[Q9E]=" + Q9E + ",[Q9C]=" + Q9C + ",[Q9ENC]='" + Q9ENC + "',[Q9CAL]='" + Q9CAL + "',[Q10C]=" + Q10C + ",[Q10CAL]='" + Q10CAL + "',[QXFeedbackCambiador]='" + QXFeedbackCambiador + "',[QXFeedbackProduccion]='" + QXFeedbackProduccion + "',[QXFeedbackCalidad]='" + QXFeedbackCalidad + "'" +
                                                                                 " WHERE ORDEN = RTRIM('" + Orden + "')";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();
            }
            catch (Exception)
            {
                cnn_GP12.Close();
            }
        }


        //**\\INFORME DE FORMACION DE OPERARIOS //**\\

        public DataSet devuelve_operarios_NIVELI_trabajando()
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT J.C_MACHINE_ID, J.C_TOOL_ID, J.C_PRODUCT_ID, J.C_PRODLONGDESCR,S.ESTADO2, OP.C_CLOCKNO, OP.C_NAME, EX.TIEMPOHORAS, CASE WHEN EX.TIEMPOHORAS IS NULL THEN 'I' WHEN EX.TIEMPOHORAS < 10 THEN 'I' WHEN EX.TIEMPOHORAS > 10 AND EX.TIEMPOHORAS < 80 THEN 'L' ELSE 'U' END AS NIVEL" +
                             " FROM PCMS.T_JOBS J LEFT JOIN(SELECT C_MACHINE_ID AS MAQUINA, CASE CAST(E.C_FINAL_RUNSTATE AS VARCHAR2(30)) WHEN '1' THEN 'En marcha' ELSE CAST(S.C_SHORT_DESCR AS VARCHAR2(30)) END AS ESTADO2, S.C_STOPGROUPS_ID from ELOG2.T_ELOG_STOPINFO E LEFT JOIN PCMS.T_STPTABLE_STPCODE S ON E.C_FINAL_STOPCODE = S.C_STOPCODE_NR AND C_STOPTABLE_ID = 2 where C_NEXT_ID is null order by c_machine_id asc) S" +
                             " ON J.C_MACHINE_ID = S.MAQUINA" +
                             " LEFT JOIN(SELECT E.C_NAME, E.C_CLOCKNO, L.C_OPERATORTYPE, L.C_MACHINE FROM OPER.T_LOGS L, PCMS.T_EMPLOYEES E, PCMS.T_OPERATORS O, PCMS.T_OPERATORTYPES T, PCMS.T_MACHINES M, PCMS.T_WORKCENTRES W WHERE L.C_REFLOG_ID IS NULL AND (L.C_TYPE = 0 OR L.C_TYPE = 2) AND L.C_OPERATOR = E.C_ID AND E.C_ID = O.C_ID AND L.C_OPERATORTYPE = T.C_ID AND L.C_MACHINE = M.C_ID AND L.C_OPERATORTYPE = 'OPERATOR' AND M.C_WORKCENTRE_ID = W.C_ID(+) ORDER BY L.C_OPERATORTYPE) OP" +
                             " ON J.C_MACHINE_ID = OP.C_MACHINE" +
                             " LEFT JOIN(SELECT C_TOOL AS MOLDE, C_CLOCKNUMBER, C_OPERATORNAME, SUM(TRUNC((C_ACTUALAVAILABLETIME/ 60),2)) AS TIEMPOHORAS, SUM (C_ACTUALAVAILABLETIME) AS HORAS" +
                             " FROM HIS.T_HISOPERATORS WHERE C_RUNTIME > 0 GROUP BY C_TOOL, C_CLOCKNUMBER, C_OPERATORNAME ORDER BY TIEMPOHORAS DESC) EX" +
                             " ON J.C_TOOL_ID = EX.MOLDE AND C_CLOCKNO = EX.C_CLOCKNUMBER" +
                             " WHERE C_SEQNR = 0 AND C_PARINDEX = 1 AND C_CLOCKNO IS NOT NULL AND(TIEMPOHORAS< 10 OR TIEMPOHORAS IS NULL) order by c_machine_id asc";
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


        //CONECTORES DE HISTORICO POR REFERENCIA


    }
}