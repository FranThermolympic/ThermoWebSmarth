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

namespace ThermoWeb.MATERIALES
{
    public class Conexion_MATERIALES
    {
        private readonly SqlConnection cnn_GP12 = new SqlConnection();
        private readonly OracleConnection cnn_bms = new OracleConnection();

        
        private readonly SqlConnection cnn_NAV = new SqlConnection();

        public Conexion_MATERIALES()
        {
            cnn_GP12.ConnectionString = ConfigurationManager.ConnectionStrings["area_rechazo"].ToString();
            cnn_bms.ConnectionString = ConfigurationManager.ConnectionStrings["BMS"].ToString();
            
            cnn_NAV.ConnectionString = ConfigurationManager.ConnectionStrings["NAV"].ToString();
        }
        //**\\UBICACIONES//**\\
        public bool Existe_Ubicacion(string estanteria, string modulo, string baldas)
        {
            try
            {
                cnn_GP12.Open();
                string sql = "SELECT * FROM[SMARTH_DB].[dbo].[UBICACIONES_Listado] WHERE Estanteria = '"+estanteria+"' AND Modulo = '"+modulo+"' AND Balda = '"+baldas+ "' AND UBIActiva = 1";
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
                string sql = "SELECT MBI.Id, [FechaEntrada],[Articulo], MAT.Descripcion,MBI.[Ubicacion],[FechaAuditoria],[Eliminado],UBI.Estanteria,UBI.Modulo,UBI.Balda, MBI.EnMaquina, MBI.Lote"+
                                  " FROM[SMARTH_DB].[dbo].[UBICACIONES_Materias_Primas] MBI"+
                                  " left join SMARTH_DB.dbo.AUX_Lista_Materiales MAT ON MBI.Articulo = MAT.Referencia" +
                                  " left join SMARTH_DB.dbo.UBICACIONES_Listado UBI ON MBI.Ubicacion = UBI.NombreUbicacion"+
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
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = " SELECT[NombreUbicacion],[Estanteria],[Modulo],[Balda],MAT.Lote" +
                              " , CASE WHEN [EnMaquina] IS NULL THEN 0 ELSE [EnMaquina] END AS [EnMaquina]" +
                              " ,CASE WHEN MAT.Articulo IS NULL THEN '-' ELSE CAST(MAT.Articulo AS varchar) END AS Articulo" +
                              " ,CASE WHEN MAN.Descripcion IS NULL THEN '' ELSE MAN.Descripcion END AS Descripcion" +
                              " ,CASE WHEN MAT.FechaEntrada IS NULL THEN '2000-01-01 00:00:00.000' ELSE MAT.FechaEntrada END AS FechaEntrada" +
                              " FROM[SMARTH_DB].[dbo].[UBICACIONES_Listado] UBI" +
                              " left join SMARTH_DB.dbo.UBICACIONES_Materias_Primas MAT ON UBI.NombreUbicacion = MAT.Ubicacion AND MAT.[Eliminado] = 0" +
                              " left join SMARTH_DB.dbo.AUX_Lista_Materiales MAN ON MAT.Articulo = MAN.Referencia" +
                              " where NombreUbicacion like 'SIL%'" +
                              " ORDER BY NombreUbicacion";
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

        public bool Insertar_MaterialxUbicacion(string ubicacion, string referencia, string operario, string lote)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql2 = "INSERT INTO [SMARTH_DB].[dbo].[UBICACIONES_Materias_Primas] " +
                                          " ([FechaEntrada],[Articulo],[Ubicacion],[FechaAuditoria],[Eliminado],[Operario], [EnMaquina],[Cantidad],[Lote],[LoteProv],[Proveedor],[Pedido]) VALUES " +
                                          " (CONVERT(VARCHAR(10), GETDATE(), 103)," + referencia.Trim() + ",'" + ubicacion + "',CONVERT(VARCHAR(10), GETDATE(), 103),0,'" + operario + "', 0,0,'"+lote+"','','','')";
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

        public bool Insertar_MaterialxUbicacionxLote(string ubicacion, string referencia, string operario, int cantidad, string lote, string loteprov, string proveedor, string pedido)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql2 = "INSERT INTO [SMARTH_DB].[dbo].[UBICACIONES_Materias_Primas] " +
                                          " ([FechaEntrada],[Articulo],[Ubicacion],[FechaAuditoria],[Eliminado],[Operario], [EnMaquina] ,[Cantidad],[Lote],[LoteProv],[Proveedor],[Pedido]) VALUES " +
                                          " (CONVERT(VARCHAR(10), GETDATE(), 103)," + referencia.Trim() + ",'" + ubicacion + "',CONVERT(VARCHAR(10), GETDATE(), 103),0,'" + operario + "', 0, "+cantidad+", '"+lote+"', '"+loteprov+"', '"+proveedor+"', '"+pedido+"')";
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

        public void Elimina_MaterialxUbicacion(string fecha, string referencia, string ubicacion, string lote)

        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "UPDATE [SMARTH_DB].[dbo].[UBICACIONES_Materias_Primas] SET [Eliminado] = 1 where FechaEntrada = '"+ fecha + "' and Articulo = "+referencia+" and Ubicacion = '"+ubicacion+"' and Lote = '"+lote+"'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();
            }
            catch (Exception ex)
            {
                cnn_GP12.Close();
            }

        }

        public void BloquearProd_MaterialxUbicacion(string fecha, string referencia, string ubicacion, string ENMAQUINA, string LOTE)

        {
            try
            {

                if (ENMAQUINA == "True")
                {
                    ENMAQUINA = "0";
                }
                else
                {
                    ENMAQUINA = "1";
                }

                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "UPDATE [SMARTH_DB].[dbo].[UBICACIONES_Materias_Primas] SET [EnMaquina] = "+ENMAQUINA+" where FechaEntrada = '" + fecha + "' and Articulo = " + referencia + " and Ubicacion = '" + ubicacion + "' and Lote = '"+LOTE+"'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();
            }
            catch (Exception ex)
            {
                cnn_GP12.Close();
            }

        }

        public void Actualiza_Material(string material, int TEMPMIN, int TEMPMAX, int TIEMPMIN, int TIEMPMAX, int secado, string TipoMaterial)

        {
            try
            {

                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "UPDATE [SMARTH_DB].[dbo].[AUX_Lista_Materiales] SET [Secado] = "+secado+",[TipoMaterial] = '" + TipoMaterial.Trim() + "', [SecadoTempMIN] = " + TEMPMIN + ",[SecadoTempMAX] = " + TEMPMAX + ",[SecadoTiempoMIN] = " + TIEMPMIN + ",[SecadoTiempoMAX]  = " + TIEMPMAX + " Where [Referencia] = '" + material + "'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();
            }
            catch (Exception ex)
            {
                cnn_GP12.Close();
            }

        }

        public void Vincula_Material_Reciclado_a_Material(string material, string reciclado)

        {
            try
            {

                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "UPDATE [SMARTH_DB].[dbo].[AUX_Lista_Materiales] SET [ReferenciaReciclado] = " + reciclado + " where [Referencia] = '" + material + "'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();
            }
            catch (Exception ex)
            {
                cnn_GP12.Close();
            }

        }

        public void Edita_documento_Material(string material, string set, string ruta)

        {
            try
            {

                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "UPDATE [SMARTH_DB].[dbo].[AUX_Lista_Materiales] SET "+set+" = '" + ruta + "' where [Referencia] = '" + material + "'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();
            }
            catch (Exception ex)
            {
                cnn_GP12.Close();
            }

        }

        public DataTable Devuelve_Material_Reciclado_Insertar(string material)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT [Referencia], CAST([Referencia] AS varchar) + ' ' + [Descripcion] as MATDESC" +
                                " FROM[SMARTH_DB].[dbo].[AUX_Lista_Materiales]" +
                                " WHERE [Referencia] = '"+ material + "'";
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

        public DataTable Devuelve_Lista_MaterialesXUbicacion(string referencia)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT [FechaEntrada],[Articulo], UBI.[Ubicacion],[FechaAuditoria],[Eliminado],[Operario],MAT.Descripcion,Estanteria, Modulo, Balda,Lote" +
                                " FROM[SMARTH_DB].[dbo].[UBICACIONES_Materias_Primas] UBI"+
                                " left join [SMARTH_DB].[dbo].AUX_Lista_Materiales MAT ON UBI.Articulo = MAT.Referencia" +
                                " LEFT JOIN [SMARTH_DB].[dbo].[UBICACIONES_Listado] UB2 on UBI.Ubicacion = UB2.NombreUbicacion"+
                                " WHERE Articulo = " +referencia+" and [Eliminado] = 0 " +
                                " ORDER BY LOTE ASC";
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

        public DataTable Devuelve_Lista_MaterialesXUbicacionUltimas(string referencia)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = " SELECT TOP(3)  UBI.[Ubicacion], Estanteria, Modulo, Balda, CASE WHEN OCU.Ubicacion IS NULL THEN 'Ubicación libre' else 'Ubicación ocupada' end as OCUPADO" +
                                 " FROM[SMARTH_DB].[dbo].[UBICACIONES_Materias_Primas] UBI" +
                                 " LEFT JOIN(SELECT distinct[Ubicacion] FROM[SMARTH_DB].[dbo].[UBICACIONES_Materias_Primas] where Eliminado = 0) OCU ON UBI.Ubicacion = OCU.Ubicacion" +
                                 " LEFT JOIN[SMARTH_DB].[dbo].[UBICACIONES_Listado] UB2 on UBI.Ubicacion = UB2.NombreUbicacion" +
                                 " WHERE Articulo = '"+referencia+"'" +
                                 " ORDER BY FechaEntrada DESC";
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

                             " WHERE MAT.[No_] LIKE '"+referencia+"'";
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

        public string Devuelve_Datos_Proveedor_NAV(string referencia)
        {
            try
            {
                cnn_NAV.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_NAV;
                string sql = "SELECT [Name] FROM [NAVDB].[dbo].[THERMO$Vendor] where No_ = '"+ referencia + "'";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_NAV);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_NAV.Close();
                return dt.Rows[0]["Name"].ToString();
            }
            catch (Exception ex)
            {
                cnn_NAV.Close();
                return "";
            }
        }

        public DataTable Devuelve_Familias_Productos_NAV()
        {
            try
            {
                cnn_NAV.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_NAV;
                string sql = "SELECT[Code],[Description] FROM[NAVDB].[dbo].[THERMO$Item Category] order by cast(Code as int) asc";
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

        public string Devuelve_ID_Familias_Productos_NAV(string descripcion)
        {
            try
            {
                cnn_NAV.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_NAV;
                string sql = "SELECT[Code],[Description] FROM[NAVDB].[dbo].[THERMO$Item Category] WHERE [Description] = '"+ descripcion + "'";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_NAV);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_NAV.Close();
                return dt.Rows[0]["Code"].ToString();
            }
            catch (Exception ex)
            {
                cnn_NAV.Close();
                return "";
            }
        }
        //**\\RECICLADOS//**\\
        public DataTable Devuelve_Historico_Molidos()
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT TOP (500) MOL.Molino,' (' + MOL.Ubicacion + ')' AS UBICACION, MAT.[Referencia],MAT.Descripcion,[Cantidad],HIS.[Fecha]"+
                                " FROM[SMARTH_DB].[dbo].[RECICLADO_Historico_Molidos] HIS"+
                                " left join[SMARTH_DB].[dbo].[RECICLADO_Lista_Molinos] MOL ON HIS.Molino = MOL.Id"+
                                " left join SMARTH_DB.dbo.AUX_Lista_Materiales MAT on HIS.Referencia = MAT.Referencia"+
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
                string sql = " SELECT J.c_Machine_id AS MAQ, TRIM(M.C_ID) AS MATERIAL, RPAD(M.C_LONG_DESCR, 25) AS DESCRIPCION, MCH.C_WORKCENTRE_ID AS NAVE" +
                              " FROM PCMS.t_Jobs J, PCMS.T_RECIPE_X_MATERIAL R, PCMS.T_MATERIALS M, PCMS.T_MACHINES MCH" +
                                " WHERE J.C_SEQNR = 0 AND R.C_MATERIAL_ID = M.C_ID AND R.C_RECIPE_ID = J.c_Product_id AND J.c_Machine_id = MCH.C_ID" +
                                  " AND((M.C_MATTYPE_ID = '15') OR(M.C_MATTYPE_ID = '215') OR(M.C_MATTYPE_ID = '155')) AND J.C_MACHINE_ID <> 'FOAM' AND J.C_MACHINE_ID <> 'MONT' AND J.C_MACHINE_ID <> 'RTRB'" +
                               " GROUP BY J.c_Machine_id, MCH.C_WORKCENTRE_ID, TRIM(M.C_ID), M.C_LONG_DESCR" +
                               " ORDER BY NAVE ASC, MAQ ASC";
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
                string sql = "SELECT CASE WHEN [ReferenciaActiva] IS NULL THEN CAST('0' AS varchar) ELSE Cast([ReferenciaActiva] as varchar) END AS ReferenciaMOL ,[Molino], Ubicacion FROM[SMARTH_DB].[dbo].[RECICLADO_Lista_Molinos]";
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
                string sql = "SELECT *, MOL.Ubicacion AS UBIMOL  FROM [SMARTH_DB].[dbo].[RECICLADO_Lista_Molinos] MOL left join [SMARTH_DB].[dbo].AUX_Lista_Materiales MAT ON MOL.ReferenciaActiva = MAT.Referencia ORDER BY MOL.Id";
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

        public string Devuelve_Descripcion_Molino(string molino)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT * FROM [SMARTH_DB].[dbo].[RECICLADO_Lista_Molinos] where Id = '" + molino + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_GP12.Close();
                if (ds.Tables[0].Rows[0]["Molino"].ToString() != "")
                {
                    return ds.Tables[0].Rows[0]["Molino"].ToString();
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


        public DataTable Devuelve_ResumenMolidosMES(string where)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT MES, HIS.Referencia, MAT.Descripcion, CANTIDAD" + 
                                " FROM (SELECT month([Fecha]) AS MESNUM, datename(month, [Fecha]) as MES,[Referencia],SUM(cast([Cantidad] as float)) AS CANTIDAD FROM[SMARTH_DB].[dbo].[RECICLADO_Historico_Molidos]"+
                                " GROUP BY month([Fecha]), datename(month, [Fecha]),[Referencia],Molino) HIS"+
                                " LEFT JOIN[SMARTH_DB].[dbo].Aux_Lista_Materiales MAT ON HIS.Referencia = MAT.Referencia"+
                                " "+where+" ORDER BY MESNUM, HIS.Referencia";
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
                string sql = "SELECT CAST([Referencia] AS varchar) Material FROM[SMARTH_DB].[dbo].[AUX_Lista_Materiales] where[Referencia] = "+material+"";
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
                string sql = "  UPDATE [SMARTH_DB].[dbo].[RECICLADO_Lista_Molinos] SET ReferenciaActiva = "+ referencia + " where Id = "+ id + "";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();              
            }
            catch (Exception ex)
            {
                cnn_GP12.Close();
            }

        }

        //**\\MATERIALES//**\\
        public DataSet Devuelve_Prevision_Secado()
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT J.c_Machine_id AS MAQ, to_char((J.C_CALCSTARTDATE - (((M.C_USERVALUE1) / 1440))), 'HH24') || ':' || to_char((J.C_CALCSTARTDATE - (((M.C_USERVALUE1) / 1440))), 'MI')  AS INICIARSECADO, J.c_Id AS ORDEN, M.C_ID AS MATERIAL, RPAD(M.C_LONG_DESCR, 25) AS DESCRIPCION,"+
                    "RPAD(M.C_SHORT_DESCR, 16) AS UBICACION, TO_CHAR((((R.C_UNITS * J.C_QTYREQUIRED) * 1.05) + 5), '99999.99') || ' Kg'   AS PREPARAR, M.C_REMARKS AS NOTAS, to_char((J.C_CALCSTARTDATE-(((M.C_USERVALUE1)/1440))),'dd/mm/yyyy')  AS FECHA" +
                    " FROM PCMS.t_Jobs J, PCMS.T_RECIPE_X_MATERIAL R, PCMS.T_MATERIALS M WHERE J.C_SEQNR >= 1 AND R.C_MATERIAL_ID = M.C_ID AND R.C_RECIPE_ID = J.c_Product_id AND((M.C_MATTYPE_ID = '15') OR(M.C_MATTYPE_ID = '215') OR(M.C_MATTYPE_ID = '221') OR(M.C_MATTYPE_ID = '155'))"+
                    " AND J.C_MACHINE_ID <> 'FOAM' AND J.C_MACHINE_ID <> 'MONT' AND J.C_MACHINE_ID <> 'RTRB' ORDER BY (J.C_CALCSTARTDATE-(((M.C_USERVALUE1) / 1440))) ASC";
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

        public DataSet devuelve_lista_materiales(string where)
        {
            try
            {
                cnn_NAV.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_NAV;
                string sql = "SELECT I.[No_] + ' ¬ ' + [Description] as MATFILTRO, I.[No_] AS MATERIAL, [Description] AS LONG_DESCRIPTION, R.[NEO Shelf location] AS SHORT_DESCRIPTION, CONCAT (R.[NEO Drying temperature], ' | ', R.[NEO Drying time]) AS REMARKS, CAN.CANTALM, CAST(FORMAT (PUR.FECHA, 'dd/MM/yyyy ') AS VARCHAR) as FECHA, PUR.QUANTITY" +
                             " FROM[NAVDB].[dbo].[THERMO$Item] I " +
                             " LEFT JOIN[NAVDB].[dbo].[THERMO$Item$e8af12d5-328b-47d8-b2d8-158ecbed1ec3] R ON I.No_ = R.No_ " +
                             " LEFT JOIN(SELECT MAT.[No_] AS MATERIAL, CASE WHEN CANTALM IS NULL THEN 0.0 ELSE CANTALM END AS CANTALM"+
                                        " FROM[NAVDB].[dbo].[THERMO$Item] MAT"+
                                        " LEFT JOIN(SELECT RTRIM([Item No_]) as MATERIAL, sum([Item Ledger Entry Quantity]) AS CANTALM"+
                                        " FROM[NAVDB].[dbo].[THERMO$Value Entry] where[Item No_] LIKE '2%' group by[Item No_]) CAN ON MAT.[No_] = CAN.MATERIAL"+
                                        " WHERE MAT.[No_] LIKE '2%') CAN ON I.No_ = CAN.MATERIAL"+
                             " LEFT JOIN(SELECT DAT.No_, DAT.FECHA, CAN.Quantity " +
                                         " FROM(SELECT [No_], MIN([Expected Receipt Date]) AS FECHA"+
                                              " FROM[NAVDB].[dbo].[THERMO$Purchase Line]"+
                                              " WHERE[Document No_] LIKE 'PC23%' AND[Outstanding Amount] > 0 AND[Posting Group] = 'MAT. PRIMA' AND[Line No_] LIKE '1%' GROUP BY No_) DAT"+
                                         " LEFT JOIN[NAVDB].[dbo].[THERMO$Purchase Line] CAN ON DAT.No_ = CAN.No_ AND DAT.FECHA = CAN.[Expected Receipt Date]) PUR ON I.No_ = PUR.No_" +
                             " WHERE " +where+" " +
                             " AND(I.[Item Category Code] = 8 OR I.[Item Category Code] = 15 OR I.[Item Category Code] = 215 OR I.[Item Category Code] = 221 OR I.[Item Category Code] = 215 OR I.[Item Category Code] = 155" +
                                    " OR I.[Item Category Code] = 150 OR I.[Item Category Code] = 012 OR I.[Item Category Code] = 10 OR I.[Item Category Code] = 12 " +
                                    " OR I.[Item Category Code] = 120 OR I.[Item Category Code] = 20 OR I.[Item Category Code] = 200 OR I.[Item Category Code] = 220" +
                                    " OR I.[Item Category Code] = 221 OR I.[Item Category Code] = 222)"+ 
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

        public DataTable Devuelve_lista_materiales(string where)
        {
            try
            {
                cnn_NAV.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_NAV;
                string sql = "SELECT I.[No_] + ' ¬ ' + [Description] as MATFILTRO, I.[No_] AS MATERIAL, [Description] AS LONG_DESCRIPTION, R.[NEO Shelf location] AS SHORT_DESCRIPTION, CONCAT (R.[NEO Drying temperature], ' | ', R.[NEO Drying time]) AS REMARKS, CAN.CANTALM, CAST(FORMAT (PUR.FECHA, 'dd/MM/yyyy ') AS VARCHAR) as FECHA, CASE WHEN PUR.QUANTITY IS NULL THEN 0 ELSE PUR.QUANTITY END AS QUANTITY" +
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
        public DataTable Devuelve_Lista_Material_SMARTH_LEFTJOIN(string where)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT CAST(LM.[Referencia] AS VARCHAR) AS REFERENCIA,LM.[Descripcion],CAST(LM.[ReferenciaReciclado] AS VARCHAR) AS ReferenciaReciclado," +
                        "CASE WHEN LM.[Secado] = 1 THEN 'SI' ELSE 'NO' END AS SECADO, " +
                        "LM.[TipoMaterial]," +
                        "CASE WHEN[SecadoTempMIN] = [SecadoTempMAX] THEN 'Temperatura: ' + CAST([SecadoTempMAX] AS varchar) +' °C' ELSE 'Temperatura: ' + CAST([SecadoTempMIN] AS varchar) +' a ' + CAST([SecadoTempMAX] AS varchar) +' °C' END AS SECADOTEMP," +
                        "CASE WHEN[SecadoTiempoMIN] = [SecadoTiempoMAX] THEN  'Tiempo: ' + CAST([SecadoTiempoMAX] AS varchar) +' horas' ELSE 'Tiempo: ' + CAST([SecadoTiempoMIN] AS varchar) +' a ' + CAST([SecadoTiempoMAX] AS varchar) +' horas' END AS SECADOTIEMP," +
                        "CASE WHEN[SecadoTempMIN] = [SecadoTempMAX] THEN 'Temp.: ' + CAST([SecadoTempMAX] AS varchar) +' °C' ELSE 'Temp.: ' + CAST([SecadoTempMIN] AS varchar) +' a ' + CAST([SecadoTempMAX] AS varchar) +' °C' END AS SECADOTEMPSHORT," +
                        "CASE WHEN[SecadoTiempoMIN] = [SecadoTiempoMAX] THEN  'Tiempo: ' + CAST([SecadoTiempoMAX] AS varchar) +' H' ELSE 'Tiempo: ' + CAST([SecadoTiempoMIN] AS varchar) +' a ' + CAST([SecadoTiempoMAX] AS varchar) +' H' END AS SECADOTIEMPSHORT," +

                        "LM.[FichaMaterial],LM.[FichaSeguridad]" +
                        ",UBI.Ubicacion,[SecadoTempNOM],[SecadoTempMIN],[SecadoTempMAX],[SecadoTiempoMIN],[SecadoTiempoMAX]" +
                    " FROM[SMARTH_DB].[dbo].[AUX_Lista_Materiales] LM " +
                      "LEFT JOIN(SELECT CAST(ART.Articulo AS varchar) AS Articulo, UBI.Ubicacion FROM(SELECT min([id]) AS MINID,[Articulo], count([Ubicacion]) as UBIACTIVAS"+
                      " FROM[SMARTH_DB].[dbo].[UBICACIONES_Materias_Primas]"+
                      " where Eliminado = 0"+
                      " group by Articulo) ART"+
                      " LEFT JOIN[SMARTH_DB].[dbo].[UBICACIONES_Materias_Primas] UBI ON ART.MINID = UBI.ID) UBI ON LM.Referencia = UBI.Articulo"+
                                        " Where Referencia <> '' " +where+"";
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

        public DataTable Devuelve_Lista_Material_SMARTH_LEFTJOIN_V1(string where)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT CAST(LM.[Referencia] AS VARCHAR) AS REFERENCIA,LM.[Descripcion],CAST(LM.[ReferenciaReciclado] AS VARCHAR) AS ReferenciaReciclado," +
                                    " CASE WHEN LM.[Secado] = 1 THEN 'SI' ELSE 'NO' END AS SECADO, " +
                                    " LM.[TipoMaterial]," +
                                    " CASE WHEN[SecadoTempMIN] = [SecadoTempMAX] THEN 'Temperatura: ' + CAST([SecadoTempMAX] AS varchar) +' °C' ELSE 'Temperatura: ' + CAST([SecadoTempMIN] AS varchar) +' a ' + CAST([SecadoTempMAX] AS varchar) +' °C' END AS SECADOTEMP," +
                                    " CASE WHEN[SecadoTiempoMIN] = [SecadoTiempoMAX] THEN  'Tiempo: ' + CAST([SecadoTiempoMAX] AS varchar) +' horas' ELSE 'Tiempo: ' + CAST([SecadoTiempoMIN] AS varchar) +' a ' + CAST([SecadoTiempoMAX] AS varchar) +' horas' END AS SECADOTIEMP," +
                                    " CASE WHEN[SecadoTempMIN] = [SecadoTempMAX] THEN 'Temp.: ' + CAST([SecadoTempMAX] AS varchar) +' °C' ELSE 'Temp.: ' + CAST([SecadoTempMIN] AS varchar) +' a ' + CAST([SecadoTempMAX] AS varchar) +' °C' END AS SECADOTEMPSHORT," +
                                    " CASE WHEN[SecadoTiempoMIN] = [SecadoTiempoMAX] THEN  'Tiempo: ' + CAST([SecadoTiempoMAX] AS varchar) +' H' ELSE 'Tiempo: ' + CAST([SecadoTiempoMIN] AS varchar) +' a ' + CAST([SecadoTiempoMAX] AS varchar) +' H' END AS SECADOTIEMPSHORT," +
                                    " CASE WHEN LOTE IS NULL THEN '-' ELSE CAST(LOTE AS VARCHAR) END AS LOTE,"+
                                    " LM.[FichaMaterial],LM.[FichaSeguridad]" +
                                    " ,UBI.Ubicacion,[SecadoTempNOM],[SecadoTempMIN],[SecadoTempMAX],[SecadoTiempoMIN],[SecadoTiempoMAX]" +
                                " FROM[SMARTH_DB].[dbo].[AUX_Lista_Materiales] LM " +
                                " LEFT JOIN(Select  ART.[Articulo], ART.LOTE, MIN(UBI.Ubicacion) AS UBICACION"+
                                                    " FROM(SELECT[Articulo], min([Lote]) as LOTE FROM[SMARTH_DB].[dbo].[UBICACIONES_Materias_Primas]"+
                                                            " WHERE Eliminado = 0 group by Articulo) ART" +
                                                            " LEFT JOIN[SMARTH_DB].[dbo].[UBICACIONES_Materias_Primas] UBI ON ART.LOTE = UBI.Lote AND ART.Articulo = UBI.Articulo AND UBI.Eliminado = 0" +
                                                            " GROUP BY ART.[Articulo], ART.LOTE) UBI ON LM.Referencia = UBI.Articulo" +
                                " Where Referencia <> '' " + where + "";
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

        public DataSet devuelve_linea_material(string material)
        {
            try
            {
                cnn_NAV.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_NAV;
                string sql = "SELECT I.[No_] + ' ¬ ' + [Description] as MATFILTRO, I.[No_] AS MATERIAL, [Description] AS LONG_DESCRIPTION, R.[NEO Shelf location] AS SHORT_DESCRIPTION, CONCAT (R.[NEO Drying temperature], ' | ', R.[NEO Drying time]) AS REMARKS" +
                             " FROM[NAVDB].[dbo].[THERMO$Item] I LEFT JOIN[NAVDB].[dbo].[THERMO$Item$e8af12d5-328b-47d8-b2d8-158ecbed1ec3] R ON I.No_ = R.No_   " +
                             "WHERE I.[No_] LIKE '"+material+"%' " +
                             //" AND(I.[Item Category Code] = 15 OR I.[Item Category Code] = 150 OR I.[Item Category Code] = 155   OR I.[Item Category Code] = 215 OR I.[Item Category Code] = 221) " +
                             " ORDER BY MATERIAL asc";
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

        public DataTable Devuelve_Prevision_SecadoV2()
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT J.c_Machine_id AS MAQ, to_char((J.C_CALCSTARTDATE - (((M.C_USERVALUE1) / 1440))), 'HH24') || ':' || to_char((J.C_CALCSTARTDATE - (((M.C_USERVALUE1) / 1440))), 'MI')  AS INICIARSECADO, J.c_Id AS ORDEN, TRIM(M.C_ID) AS MATERIAL, RPAD(M.C_LONG_DESCR, 25) AS DESCRIPCION," +
                    "RPAD(M.C_SHORT_DESCR, 16) AS UBICACION, TO_CHAR((((R.C_UNITS * J.C_QTYREQUIRED) * 1.05) + 5), '99999.99') || ' Kg'   AS PREPARAR, M.C_REMARKS AS NOTAS, to_char((J.C_CALCSTARTDATE-(((M.C_USERVALUE1)/1440))),'dd/mm/yyyy')  AS FECHA" +
                    " FROM PCMS.t_Jobs J, PCMS.T_RECIPE_X_MATERIAL R, PCMS.T_MATERIALS M WHERE J.C_SEQNR >= 1 AND R.C_MATERIAL_ID = M.C_ID AND R.C_RECIPE_ID = J.c_Product_id AND((M.C_MATTYPE_ID = '15') OR(M.C_MATTYPE_ID = '215') OR(M.C_MATTYPE_ID = '221') OR(M.C_MATTYPE_ID = '155'))" +
                    " AND J.C_MACHINE_ID <> 'FOAM' AND J.C_MACHINE_ID <> 'MONT' AND J.C_MACHINE_ID <> 'RTRB' ORDER BY (J.C_CALCSTARTDATE-(((M.C_USERVALUE1) / 1440))) ASC";
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

        public DataTable Devuelve_Prevision_SecadoV3()
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT J.C_SEQNR AS SEQNR, CASE WHEN J.C_RUN_NR = 0 THEN 'V1' ELSE 'V' || to_char(J.C_RUN_NR) END AS JOBVER, J.c_Machine_id AS MAQ, to_char((J.C_CALCSTARTDATE - (((M.C_USERVALUE1) / 1440))), 'HH24') || ':' || to_char((J.C_CALCSTARTDATE - (((M.C_USERVALUE1) / 1440))), 'MI')  AS INICIARSECADO, TRIM(J.c_Id) AS ORDEN, TRIM(M.C_ID) AS MATERIAL, 'M'||TRIM(M.C_ID) AS MATJOIN,RPAD(M.C_LONG_DESCR, 25) AS DESCRIPCION,RTRIM(M.C_LONG_DESCR) AS DESCRIPCIONLONG," +
                             " RPAD(M.C_SHORT_DESCR, 16) AS UBICACION, TO_CHAR((((R.C_UNITS * J.C_QTYREQUIRED) * 1.05) + 5), '99999.99') || ' Kg'   AS PREPARAR, M.C_REMARKS AS NOTAS, to_char((J.C_CALCSTARTDATE - (((M.C_USERVALUE1) / 1440))), 'dd/mm/yyyy')  AS FECHA, CASE WHEN CT.REPETICIONES IS NULL THEN 1 ELSE CT.REPETICIONES END AS REPETICIONES, CASE WHEN CT.SUMAMATS IS NULL THEN 0 ELSE CT.SUMAMATS END AS SUMAMATS" +
                             " FROM PCMS.t_Jobs J, PCMS.T_RECIPE_X_MATERIAL R, PCMS.T_MATERIALS M"+
                             " LEFT JOIN(SELECT TRIM(M.C_ID) AS MATERIAL, COUNT(M.C_ID) AS REPETICIONES, SUM (((R.C_UNITS * J.C_QTYREQUIRED) * 1.05) + 5) AS SUMAMATS" +
                                         " FROM PCMS.t_Jobs J, PCMS.T_RECIPE_X_MATERIAL R, PCMS.T_MATERIALS M" +
                                         " WHERE J.C_SEQNR >= 1 AND R.C_MATERIAL_ID = M.C_ID AND R.C_RECIPE_ID = J.c_Product_id " +
                                         " AND((M.C_MATTYPE_ID = '15') OR(M.C_MATTYPE_ID = '215') OR(M.C_MATTYPE_ID = '221') OR(M.C_MATTYPE_ID = '155'))" +                  
                                         " AND J.C_MACHINE_ID <> 'FOAM' AND J.C_MACHINE_ID <> 'MONT' AND J.C_MACHINE_ID <> 'RTRB' AND J.C_CALCSTARTDATE - (((M.C_USERVALUE1) / 1440)) < (SYSDATE + 3)" +
                                         " GROUP BY TRIM(M.C_ID)) CT ON TRIM(M.C_ID) = CT.MATERIAL" +
                                    " WHERE J.C_SEQNR >= 1 AND R.C_MATERIAL_ID = M.C_ID AND R.C_RECIPE_ID = J.c_Product_id " +
                                         " AND((M.C_MATTYPE_ID = '15') OR(M.C_MATTYPE_ID = '215') OR(M.C_MATTYPE_ID = '221') OR(M.C_MATTYPE_ID = '155'))" +                                    
                                         " AND J.C_MACHINE_ID <> 'FOAM' AND J.C_MACHINE_ID <> 'MONT' AND J.C_MACHINE_ID <> 'RTRB'" +
                                    " ORDER BY(J.C_CALCSTARTDATE-(((M.C_USERVALUE1) / 1440))) ASC";
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

        public bool Insertar_Preparacion_Materiales(string orden, int version, string material, int operario)
        {
            try
            {
                string fecha = DateTime.Now.ToString();

                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql2 = "INSERT INTO [SMARTH_DB].[dbo].[MATERIALES_Entradas_Secado] ([Orden],[Material],[Version],[Operario],[Fecha])" +
                                "VALUES ('"+orden+ "', '" + material + "','" + version+"', '"+ operario + "', SYSDATETIME())";
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

        public DataTable Devuelve_Prevision_Secado_Pendientes()
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT TRIM(C_ID) AS ORDEN, 'M'||TRIM(MATERIAL) AS MATJOIN, CASE WHEN C_RUN_NR = 0 THEN 'V1' ELSE 'V' || to_char(C_RUN_NR) END AS JOBVER, TRIM(MAQ) AS MAQ, TRIM(MATERIAL) AS MATERIAL, DESCRIPCION, DESCRIPCIONLONG, UBICACION, SUM(KGS_RESTANTE) AS KGS_RESTANTE, SUM(PIEZASRESTANTES) AS PIEZASRESTANTES, COUNT(MAQ) AS ORDENES, C_REMARKS AS NOTAS" +
                                " FROM(SELECT J.C_ID, J.C_RUN_NR, J.c_Machine_id AS MAQ, TRIM(M.C_ID) AS MATERIAL, RPAD(M.C_LONG_DESCR, 25) AS DESCRIPCION, RTRIM(M.C_LONG_DESCR) AS DESCRIPCIONLONG," +
                                                          " RPAD(M.C_SHORT_DESCR, 16) AS UBICACION, ROUND((((R.C_UNITS * J.C_QTYREMAINING) * 1.05)), 2)   AS KGS_RESTANTE, J.C_QTYREMAINING AS PIEZASRESTANTES, M.C_REMARKS" +
                                                          " FROM PCMS.t_Jobs J, PCMS.T_RECIPE_X_MATERIAL R, PCMS.T_MATERIALS M" +
                                                          " WHERE J.C_SEQNR = 0 AND R.C_MATERIAL_ID = M.C_ID AND R.C_RECIPE_ID = J.c_Product_id" +
                                                                      " AND((M.C_MATTYPE_ID = '15') OR(M.C_MATTYPE_ID = '215') OR(M.C_MATTYPE_ID = '221') OR(M.C_MATTYPE_ID = '155'))" +
                                                                      " AND J.C_MACHINE_ID <> 'FOAM' AND J.C_MACHINE_ID <> 'MONT' AND J.C_MACHINE_ID <> 'RTRB' AND R.C_UNITS  <> 0) RST" +
                               " GROUP BY C_ID, C_RUN_NR, MAQ, MATERIAL, DESCRIPCION, DESCRIPCIONLONG, UBICACION, C_REMARKS" +
                               " ORDER BY MAQ ASC";
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

        public DataTable Devuelve_Prevision_Secado_Pendientes_V2()
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT TRIM(MAQ) AS MAQ, MATERIAL, DESCRIPCION, DESCRIPCIONLONG, UBICACION, KGS_RESTANTE, PIEZASRESTANTES, ORDENES, NOTAS"+
                                 " , TRIM(JOB.C_ID) AS ORDEN, 'M' || TRIM(MATERIAL) AS MATJOIN, CASE WHEN JOB.C_RUN_NR = 0 THEN 'V1' ELSE 'V' || to_char(JOB.C_RUN_NR) END AS JOBVER"+
                                 " FROM(SELECT MAQ, TRIM(MATERIAL) AS MATERIAL, DESCRIPCION, DESCRIPCIONLONG, UBICACION, SUM(KGS_RESTANTE) AS KGS_RESTANTE, SUM(PIEZASRESTANTES) AS PIEZASRESTANTES, COUNT(MAQ) AS ORDENES, C_REMARKS AS NOTAS" +
                                 " FROM(SELECT J.C_ID, J.C_RUN_NR, J.c_Machine_id AS MAQ, TRIM(M.C_ID) AS MATERIAL, RPAD(M.C_LONG_DESCR, 25) AS DESCRIPCION, RTRIM(M.C_LONG_DESCR) AS DESCRIPCIONLONG, " +
                                                           " RPAD(M.C_SHORT_DESCR, 16) AS UBICACION, ROUND((((R.C_UNITS * J.C_QTYREMAINING) * 1.05)), 2)   AS KGS_RESTANTE, J.C_QTYREMAINING AS PIEZASRESTANTES, M.C_REMARKS" +
                                                           " FROM PCMS.t_Jobs J, PCMS.T_RECIPE_X_MATERIAL R, PCMS.T_MATERIALS M" +
                                                           " WHERE J.C_SEQNR = 0 AND R.C_MATERIAL_ID = M.C_ID AND R.C_RECIPE_ID = J.c_Product_id" +
                                                                       " AND((M.C_MATTYPE_ID = '15') OR(M.C_MATTYPE_ID = '215') OR(M.C_MATTYPE_ID = '221') OR(M.C_MATTYPE_ID = '155'))" +
                                                                       " AND J.C_MACHINE_ID <> 'FOAM' AND J.C_MACHINE_ID <> 'MONT' AND J.C_MACHINE_ID <> 'RTRB' AND R.C_UNITS <> 0) RST" +
                                " GROUP BY MAQ, MATERIAL, DESCRIPCION, DESCRIPCIONLONG, UBICACION, C_REMARKS) ORI" +
                                " LEFT JOIN PCMS.T_JOBS JOB ON ORI.MAQ = JOB.c_Machine_id AND JOB.C_SEQNR = 0 AND JOB.C_PARINDEX = 1" +
                                " ORDER BY MAQ ASC";
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

        public DataTable Devuelve_Stock_Material_()
        {
            try
            {
                cnn_NAV.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_NAV;
                string sql = "SELECT RTRIM([Item No_]) as MATERIAL, sum ([Item Ledger Entry Quantity]) AS CANTALM FROM [NAVDB].[dbo].[THERMO$Value Entry] where [Item No_] LIKE '2%' group by[Item No_]";
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

        public DataTable Devuelve_Stock_MaterialV2()
        {
            try
            {
                cnn_NAV.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_NAV;
                string sql = "SELECT MAT.[No_] AS MATERIAL, CASE WHEN CANTALM IS NULL THEN 0.0 ELSE CANTALM END AS CANTALM, CAST(FORMAT (PUR.FECHA, 'dd/MM/yyyy ') AS VARCHAR) AS FECHA FROM [NAVDB].[dbo].[THERMO$Item] MAT" +
                             " LEFT JOIN(SELECT RTRIM([Item No_]) as MATERIAL, sum([Item Ledger Entry Quantity]) AS CANTALM FROM[NAVDB].[dbo].[THERMO$Value Entry]" +
                                        " where[Item No_] LIKE '2%' group by[Item No_]) CAN ON MAT.[No_] = CAN.MATERIAL"+
                             " LEFT JOIN(SELECT DAT.No_, DAT.FECHA, CAN.Quantity " +
                                         " FROM(SELECT [No_], MIN([Expected Receipt Date]) AS FECHA" +
                                              " FROM[NAVDB].[dbo].[THERMO$Purchase Line]" +
                                              " WHERE[Document No_] LIKE 'PC23%' AND[Outstanding Amount] > 0 AND[Posting Group] = 'MAT. PRIMA' AND[Line No_] LIKE '1%' GROUP BY No_) DAT" +
                                         " LEFT JOIN[NAVDB].[dbo].[THERMO$Purchase Line] CAN ON DAT.No_ = CAN.No_ AND DAT.FECHA = CAN.[Expected Receipt Date]) PUR ON MAT.No_ = PUR.No_" +

                             " WHERE MAT.[No_] LIKE '2%'";
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

        public DataTable Devuelve_Ubicacion_Material_LEFTJOIN()
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = " SELECT CAST(ART.Articulo AS varchar) AS Articulo, UBI.Ubicacion, UBIACTIVAS FROM(SELECT min([id]) AS MINID, [Articulo], count([Ubicacion]) as UBIACTIVAS" +
                                " FROM [SMARTH_DB].[dbo].[UBICACIONES_Materias_Primas]" +
                                  " where Eliminado = 0" +
                                  " group by Articulo) ART" +
                                  " LEFT JOIN[SMARTH_DB].[dbo].[UBICACIONES_Materias_Primas] UBI ON ART.MINID = UBI.ID" +
                                  " order by ART.Articulo asc";
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

        public DataTable Devuelve_Entradas_Secado()
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "Select ORD.ORDEN AS ORDEN, 'V' + CAST(ORD.VERSION AS varchar) AS JOBVER, 'M' + CAST(ORD.Material AS varchar) AS MATJOIN, FEC.Operario, FEC.Fecha, OP.Nombre" +
                                " FROM(SELECT[Orden],Material, MAX([Version]) AS VERSION FROM[SMARTH_DB].[dbo].[MATERIALES_Entradas_Secado] GROUP BY ORDEN,Material) ORD" +
                                " LEFT JOIN[SMARTH_DB].[dbo].[MATERIALES_Entradas_Secado] FEC ON ORD.Orden = FEC.Orden AND ORD.Material = FEC.Material AND ORD.VERSION = FEC.Version" +
                                " LEFT JOIN [SMARTH_DB].[dbo].[AUX_Personal_Mandos] OP ON FEC.Operario = OP.Id"+
                                " ORDER BY Fecha DESC";
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


        //**\\HISTORICO LIBERACION//**\\

        //DESCARTAR O APROVECHAR CONECTORES GP12.ASPX


    }
}