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

namespace ThermoWeb.DOCUMENTAL
{
    public class Conexion_DOCUMENTAL
    {
        
        private readonly OracleConnection cnn_bms = new OracleConnection();
        private readonly SqlConnection cnn_SMARTH = new SqlConnection();

        public Conexion_DOCUMENTAL()
        {
            cnn_bms.ConnectionString = ConfigurationManager.ConnectionStrings["BMS"].ToString();
            cnn_SMARTH.ConnectionString = ConfigurationManager.ConnectionStrings["SMARTH"].ToString();
        }

        //CONECTORES DOCUMENTAL
        public DataSet devuelve_dataset_referenciasYN()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT P.[Referencia] as REF,P.[Molde],[Descripcion],"+
                                "CASE WHEN PlanControl IS NULL THEN 'NO' WHEN PlanControl = '' THEN 'NO' ELSE 'SI' END AS PlanControl,"+
                                "CASE WHEN PautaControl IS NULL THEN 'NO' WHEN PautaControl = '' THEN 'NO' ELSE 'SI' END AS PautaControl,"+
                                "CASE WHEN PautaRecepcion1 IS NULL THEN 'NO' WHEN PautaRecepcion1 = '' THEN 'NO' ELSE 'SI' END AS PautaRecepcion1,"+
                                "CASE WHEN PautaRecepcion2 IS NULL THEN 'NO' WHEN PautaRecepcion2 = '' THEN 'NO' ELSE 'SI' END AS PautaRecepcion2,"+
                                "CASE WHEN OperacionEstandar IS NULL THEN 'NO' WHEN OperacionEstandar = '' THEN 'NO' ELSE 'SI' END AS OperacionEstandar,"+
                                "CASE WHEN OperacionEstandar2 IS NULL THEN 'NO' WHEN OperacionEstandar2 = '' THEN 'NO' ELSE 'SI' END AS OperacionEstandar2,"+
                                "CASE WHEN Defoteca IS NULL THEN 'NO' WHEN Defoteca = '' THEN 'NO' ELSE 'SI' END AS Defoteca,"+
                                "CASE WHEN Embalaje IS NULL THEN 'NO' WHEN Embalaje = '' THEN 'NO' ELSE 'SI' END AS Embalaje,"+
                                "CASE WHEN Gp12 IS NULL THEN 'NO' WHEN Gp12 = '' THEN 'NO' ELSE 'SI' END AS Gp12,"+
                                "CASE WHEN ImagenPieza IS NULL THEN 'NO' WHEN ImagenPieza = '' THEN 'NO' ELSE 'SI' END AS ImagenPieza,"+
                                "CASE WHEN PautaRetrabajo IS NULL THEN 'NO' WHEN PautaRetrabajo = '' THEN 'NO' ELSE 'SI' END AS PautaRetrabajo," +
                                "CASE WHEN VideoAuxiliar IS NULL THEN 'NO' WHEN VideoAuxiliar = '' THEN 'NO' ELSE 'SI' END AS VideoAuxiliar" +
                                " FROM [SMARTH_DB].[dbo].[AUX_TablaProductos] P LEFT JOIN [SMARTH_DB].[dbo].[DOCUMENTAL_Base_DocumentosXReferencias] R ON P.Referencia = R.Referencia"+
                                " ORDER BY P.Referencia", cnn_SMARTH);
                //"SELECT P.[Referencia] as REF,P.[Molde],[Descripcion],[PlanControl],[PautaControl],[PautaRecepcion1],[PautaRecepcion2],[OperacionEstandar],[OperacionEstandar2],[Defoteca],[Embalaje],[Gp12], ImagenPieza FROM [SMARTH_DB].[dbo].[AUX_TablaProductos] P LEFT JOIN [SMARTH_DB].[dbo].[DOCUMENTAL_Base_DocumentosXReferencias] R ON P.Referencia = R.Referencia ORDER BY P.Referencia", cnn_SMARTH);
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

        public DataSet devuelve_dataset_filtroreferenciasYN(string referencia, string molde)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT P.[Referencia] as REF,P.[Molde],[Descripcion],"+
                                "CASE WHEN PlanControl IS NULL THEN 'NO' WHEN PlanControl = '' THEN 'NO' ELSE 'SI' END AS PlanControl,"+
                                "CASE WHEN PautaControl IS NULL THEN 'NO' WHEN PautaControl = '' THEN 'NO' ELSE 'SI' END AS PautaControl,"+
                                "CASE WHEN PautaRecepcion1 IS NULL THEN 'NO' WHEN PautaRecepcion1 = '' THEN 'NO' ELSE 'SI' END AS PautaRecepcion1,"+
                                "CASE WHEN PautaRecepcion2 IS NULL THEN 'NO' WHEN PautaRecepcion2 = '' THEN 'NO' ELSE 'SI' END AS PautaRecepcion2,"+
                                "CASE WHEN OperacionEstandar IS NULL THEN 'NO' WHEN OperacionEstandar = '' THEN 'NO' ELSE 'SI' END AS OperacionEstandar,"+
                                "CASE WHEN OperacionEstandar2 IS NULL THEN 'NO' WHEN OperacionEstandar2 = '' THEN 'NO' ELSE 'SI' END AS OperacionEstandar2,"+
                                "CASE WHEN Defoteca IS NULL THEN 'NO' WHEN Defoteca = '' THEN 'NO' ELSE 'SI' END AS Defoteca,"+
                                "CASE WHEN Embalaje IS NULL THEN 'NO' WHEN Embalaje = '' THEN 'NO' ELSE 'SI' END AS Embalaje,"+
                                "CASE WHEN Gp12 IS NULL THEN 'NO' WHEN Gp12 = '' THEN 'NO' ELSE 'SI' END AS Gp12,"+
                                "CASE WHEN ImagenPieza IS NULL THEN 'NO' WHEN ImagenPieza = '' THEN 'NO' ELSE 'SI' END AS ImagenPieza,"+
                                "CASE WHEN PautaRetrabajo IS NULL THEN 'NO' WHEN PautaRetrabajo = '' THEN 'NO' ELSE 'SI' END AS PautaRetrabajo," +
                                "CASE WHEN VideoAuxiliar IS NULL THEN 'NO' WHEN VideoAuxiliar = '' THEN 'NO' ELSE 'SI' END AS VideoAuxiliar,[FechaModificacion]" +
                                " FROM [SMARTH_DB].[dbo].[AUX_TablaProductos] P LEFT JOIN [SMARTH_DB].[dbo].[DOCUMENTAL_Base_DocumentosXReferencias] R ON P.Referencia = R.Referencia"+
                                " WHERE P.Referencia LIKE '"+referencia+"%' AND P.Molde LIKE '"+molde+"%'"+
                                " ORDER BY P.Referencia", cnn_SMARTH);
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

        public DataSet Devuelve_dataset_filtroreferenciasSMARTH(string referencia1, string referencia2, string referencia3, string referencia4, string referencia5)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT P.[Referencia] as REF,P.[Molde],[Descripcion],[PlanControl],[PautaControl],[PautaRecepcion1],[PautaRecepcion2],[OperacionEstandar],[OperacionEstandar2],[Defoteca],[Embalaje],[Gp12],ImagenPieza,[PautaRetrabajo],[VideoAuxiliar], [Observaciones],[NoConformidades],P.Cliente as CLINO, IMG.Logotipo,[FechaModificacion] FROM [SMARTH_DB].[dbo].[AUX_TablaProductos] P LEFT JOIN [SMARTH_DB].[dbo].[DOCUMENTAL_Base_DocumentosXReferencias] R ON P.Referencia = R.Referencia LEFT JOIN SMARTH_DB.DBO.[AUX_Lista_Clientes] IMG ON P.Cliente = IMG.Cliente WHERE P.Referencia = '" + referencia1 + "'"+referencia2+ "" + referencia3 + "" + referencia4 + "" + referencia5 + " ORDER BY P.Referencia", cnn_SMARTH);
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

        



        public DataTable Devuelve_Documentos_Vinculados_SMARTH(string referencia, string molde)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT DOC.Id, [Referencia],[Molde], UPPER(AUX.TipoDocumento) AS TipoDocumento,[DescripcionDOC],[URL],[Fecha],[Edicion], PER.Nombre,[MultiReferencia], CASE WHEN [MultiReferencia] = 1 THEN '(Común al molde)' ELSE '(Sólo producto)' END AS MULTIREFTEXT " +
                                                                " FROM [SMARTH_DB].[dbo].[DOCUMENTAL_DocumentosXReferenciasV2] DOC"+
                                                                " LEFT JOIN [SMARTH_DB].[dbo].DOCUMENTAL_Auxiliares AUX ON DOC.TipoDocumento = AUX.Id" +
                                                                " LEFT JOIN [SMARTH_DB].[dbo].AUX_Personal_Mandos PER ON DOC.Responsable = PER.Id" +
                                                                " WHERE (Referencia = '"+referencia+ "' or(Molde = '" + molde + "' and MultiReferencia = 1 )) and Eliminar = 0", cnn_SMARTH);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                cnn_SMARTH.Close();
                DataTable ds = new DataTable();
                dataAdapter.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_Tipos_Documentos_SMARTH()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM[SMARTH_DB].[dbo].[DOCUMENTAL_Auxiliares] Order by TipoDocumento asc", cnn_SMARTH);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                cnn_SMARTH.Close();
                DataTable ds = new DataTable();
                dataAdapter.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataSet devuelve_moldesXreferencia(string referencia)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT [Referencia],[Descripcion] FROM [SMARTH_DB].[dbo].[AUX_TablaProductos] where Molde = " + referencia + " order by Referencia";
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
        /*
        public bool insertar_documentos_referencia_molde(string referencia, string molde, string PLANCONTROL, string PAUTACONTROL, string RECEPCION, string HOS, string GP12, string HOS2, string DEF, string EMB, string IMG, string RETRABAJO, string VIDEO, string COMENTARIO, string NOCONFORMIDAD, string fecha, string cambio)
        { 
            try
            {

                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "UPDATE [SMARTH_DB].[dbo].[DOCUMENTAL_Base_DocumentosXReferencias] SET [PlanControl] = '" + PLANCONTROL + "',[PautaControl] = '" + PAUTACONTROL + "',[PautaRecepcion2] = '" + RECEPCION + "',[PautaRecepcion1] = '',[OperacionEstandar] = '" + HOS + "',[OperacionEstandar2] = '" + HOS2 + "',[Defoteca] = '" + DEF + "',[Embalaje] = '" + EMB + "',[Gp12] = '" + GP12 + "',[ImagenPieza] = '" + IMG + "',[PautaRetrabajo] = '" + RETRABAJO + "', [VideoAuxiliar] = '" + VIDEO + "', [Observaciones] = '" + COMENTARIO + "', [NoConformidades] = '" + NOCONFORMIDAD + "', [FechaModificacion] = '"+fecha+ "', [RazonModificacion] = '"+cambio+"'" +
                             " WHERE Referencia ='" + referencia + "' and Molde = '" + molde + "'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return false;
            }
            catch (Exception)
                {
                    cnn_SMARTH.Close();
                    return false;
                }
        }

        
        public bool insertar_documentos_moldeBOTON(string molde, string PLANCONTROL, string PAUTACONTROL, string RECEPCION, string HOS, string GP12, string HOS2, string DEF, string EMB, string RETRABAJO, string VIDEO, string COMENTARIO, string NOCONFORMIDAD, string IMG, string fecha, string cambio)
        {
            try
            {

                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "UPDATE [SMARTH_DB].[dbo].[DOCUMENTAL_Base_DocumentosXReferencias] SET [FechaModificacion] = '" + fecha + "',"+cambio+"" + PLANCONTROL + "" + PAUTACONTROL + "" + RECEPCION + "" + HOS + "" + HOS2 + "" + DEF + "" + EMB + "" + GP12 + "" + RETRABAJO + "" + VIDEO + "" + COMENTARIO + "" + NOCONFORMIDAD + ""+IMG+"[PautaRecepcion2] = '' " +
                             " WHERE Molde = '" + molde + "'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return false;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }

        */
        public bool Insertar_documentos_moldeBOTONV2(string molde, string PAUTACONTROL, string RECEPCION, string HOS, string GP12, string DEF, string EMB, string COMENTARIO, string IMG, string fecha, string cambio)
        {
            try
            {

                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "UPDATE [SMARTH_DB].[dbo].[DOCUMENTAL_Base_DocumentosXReferencias] SET [FechaModificacion] = '" + fecha + "'," + cambio + "" + PAUTACONTROL + "" + RECEPCION + "" + HOS + "" + DEF + "" + EMB + "" + GP12 + "" + COMENTARIO + "" + IMG + "[PautaRecepcion2] = '' " +
                             " WHERE Molde = '" + molde + "'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return false;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }

        public bool Insertar_documentos_referencia_moldeV2(string referencia, string molde, string PAUTACONTROL, string RECEPCION, string HOS, string GP12, string DEF, string EMB, string IMG, string COMENTARIO, string fecha, string cambio)
        {
            try
            {

                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "UPDATE [SMARTH_DB].[dbo].[DOCUMENTAL_Base_DocumentosXReferencias] SET [PautaControl] = '" + PAUTACONTROL + "',[PautaRecepcion2] = '" + RECEPCION + "',[PautaRecepcion1] = '',[OperacionEstandar] = '" + HOS + "',[Defoteca] = '" + DEF + "',[Embalaje] = '" + EMB + "',[Gp12] = '" + GP12 + "',[ImagenPieza] = '" + IMG + "', [Observaciones] = '" + COMENTARIO + "', [FechaModificacion] = '" + fecha + "', [RazonModificacion] = '" + cambio + "'" +
                             " WHERE Referencia ='" + referencia + "' and Molde = '" + molde + "'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return false;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }

        //Nuevos documentos

        public DataTable Devuelve_Produciendo_Pendientes_Documentos()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT Maquina, PRD.Referencia, DC.Documento, CASE WHEN OP.Nombre = '-' then 'Sin jefe asignado' else op.Nombre end as Nombre"+
                                                                " FROM[SMARTH_DB].[dbo].[AUX_Planificacion] PRD" +
                                                                " LEFT JOIN(SELECT * FROM(SELECT Referencia, Molde, 'Pauta control' as Documento FROM[SMARTH_DB].[dbo].[DOCUMENTAL_Base_DocumentosXReferencias] where PautaControl is null or PautaControl = ''"+
                                                                  " UNION ALL SELECT Referencia, Molde, 'Operacion Estándar' as Documento FROM[SMARTH_DB].[dbo].[DOCUMENTAL_Base_DocumentosXReferencias] where OperacionEstandar is null or OperacionEstandar = ''"+
                                                                  " UNION ALL SELECT Referencia, Molde, 'Defoteca' as Documento FROM[SMARTH_DB].[dbo].[DOCUMENTAL_Base_DocumentosXReferencias] where Defoteca is null or Defoteca = ''"+
                                                                  " UNION ALL SELECT Referencia, Molde, 'Embalaje' as Documento FROM[SMARTH_DB].[dbo].[DOCUMENTAL_Base_DocumentosXReferencias] where Embalaje is null or Embalaje = ''"+
                                                                  " UNION ALL SELECT Referencia, Molde, 'Embalaje Alternativo' as Documento FROM[SMARTH_DB].[dbo].[DOCUMENTAL_Base_DocumentosXReferencias] where PautaRecepcion2 is null or PautaRecepcion2 = ''"+
                                                                  " UNION ALL SELECT Referencia, Molde, 'Pauta GP12' as Documento FROM[SMARTH_DB].[dbo].[DOCUMENTAL_Base_DocumentosXReferencias] where Gp12 is null or Gp12 = '') DOC) DC ON PRD.Referencia = DC.Referencia"+
                                                                " LEFT JOIN(SELECT[Referencia], MAN.Nombre FROM[SMARTH_DB].[dbo].[GP12_ProductosEstados] PRO  left join[SMARTH_DB].[dbo].[AUX_Personal_Mandos] MAN ON PRO.Responsable = MAN.Id) OP ON PRD.Referencia = OP.Referencia"+
                                                                " WHERE PRD.SEQNR = 0"+
                                                                " ORDER BY Referencia", cnn_SMARTH);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                cnn_SMARTH.Close();
                DataTable ds = new DataTable();
                dataAdapter.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataTable Devuelve_Notas_Documentales_Operarios(string where)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT DOC.Id, [NumOperario],OP.Operario,DOC.[Molde],DOC.[Referencia],PRO.Descripcion,[Fecha],[FeedbackDocumento],[Estado],CASE WHEN Estado = 1 THEN 'Cerrado' else 'Abierto' END AS EstadoDOC" +
                                                              " FROM[SMARTH_DB].[dbo].[DOCUMENTAL_Operarios_Feedback] DOC"+
                                                              " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] PRO ON DOC.Referencia = PRO.Referencia" +
                                                              " LEFT JOIN[SMARTH_DB].[dbo].[AUX_TablaOperarios] OP ON DOC.NumOperario = OP.Id" +
                                                              " WHERE PRO.Referencia IS NOT NULL" +
                                                              " order by Estado asc, Fecha desc", cnn_SMARTH);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                cnn_SMARTH.Close();
                DataTable ds = new DataTable();
                dataAdapter.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public bool Actualizar_Operarios_Feedback(string id, string estado)
        {
            try
            {

                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "Update[SMARTH_DB].[dbo].[DOCUMENTAL_Operarios_Feedback] SET Estado = " + estado + " WHERE Id = "+id+"";
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


        public bool Insertar_documentos_V3(string referencia, string molde, int tipodoc, string descripciondoc, string URL, string Fecha, int edicion, int responsable, int Multireferencia)
        {
            try
            {

                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[DOCUMENTAL_DocumentosXReferenciasV2]" +
                    "([Referencia],[Molde],[TipoDocumento],[DescripcionDOC],[URL],[Fecha],[Edicion],[Responsable],[MultiReferencia],[FechaSubida],[Eliminar])" +
                    "VALUES (" + referencia + ",'" + molde + "', " + tipodoc + ", '" + descripciondoc + "','" + URL + "','" + Fecha + "'," + edicion + ", " + responsable + ", " + Multireferencia + ", SYSDATETIME(), 0 )";
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

        public bool Eliminar_documentos_V3(string URL)
        {
            try
            {

                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "UPDATE [SMARTH_DB].[dbo].[DOCUMENTAL_DocumentosXReferenciasV2]" +
                    " SET [Eliminar] = 1" +
                    " WHERE [URL] = '" + URL + "'";
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



        public int Devuelve_ID_TipoDocumento_SMARTH(string nombre)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT [Id] FROM[SMARTH_DB].[dbo].[DOCUMENTAL_Auxiliares] WHERE TipoDocumento = '"+nombre+"'";
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

        public DataSet DevuelveRemarkProducto(string producto)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT C_REMARKS FROM PCMS.T_PRODUCTS WHERE C_ID = '" + producto + "'";
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

        public DataSet DevuelveEstructuraMaquina(string maquina)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT J.c_Machine_id AS MAQ, J.C_PRODLONGDESCR, J.C_PRODUCT_ID AS PRODUCTO, J.c_Id AS ORDEN, TRIM(M.C_ID) AS MATERIAL, RPAD(M.C_LONG_DESCR, 25) AS DESCRIPCION, RPAD(M.C_SHORT_DESCR, 16) AS UBICACION, ROUND((((R.C_UNITS * J.C_QTYREQUIRED) * 1.05) + 5), 2)  AS CONSUMOORDEN, M.C_REMARKS AS NOTAS" +
                                " FROM PCMS.t_Jobs J, PCMS.T_RECIPE_X_MATERIAL R, PCMS.T_MATERIALS M " +
                                " WHERE J.C_SEQNR = 0 AND J.c_Machine_id = '"+maquina+"' AND R.C_MATERIAL_ID = M.C_ID AND R.C_RECIPE_ID = J.c_Product_id" +
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
                string sql = "SELECT TRIM(M.C_ID) AS MATERIAL, RPAD(M.C_LONG_DESCR, 25) AS DESCRIPCION, RPAD(M.C_SHORT_DESCR, 16) AS UBICACION, R.C_UNITS  AS CONSUMOUNIDAD, M.C_REMARKS AS NOTAS"+
                                 " FROM PCMS.T_RECIPE_X_MATERIAL R" +
                                 " LEFT JOIN PCMS.T_MATERIALS M ON R.C_MATERIAL_ID = M.C_ID" +
                                 " WHERE R.C_RECIPE_ID = '"+producto+"'" +
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

        public bool Actualizar_NotaProductosBMSxFicha(int producto, string nota)
        {

            try
            {
                cnn_bms.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = cnn_bms;
                cmd.CommandText = "UPDATE PCMS.T_PRODUCTS SET C_REMARKS = '" + nota + "'  WHERE C_ID = '" + producto + "'";
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

        //Documentación en linea
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
        }

        //Documentación en planta
        public DataSet DevuelveOrdenProducciendo(string Maquina)
        {
            try 
            {
                cnn_bms.Open();
                string sql = "SELECT C_ID, trim(C_PRODUCT_ID) as C_PRODUCT_ID, C_PRODLONGDESCR, C_CUSTOMER, C_MACHINE_ID, C_TOOL_ID FROM T_JOBS WHERE C_SEQNR = 0 AND C_MACHINE_ID = '" + Maquina+ "' ORDER BY C_PRODUCT_ID";
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
        /*
        public DataSet DevuelveDetallesOrden (string Orden)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT J.c_Machine_id AS MAQUINA,J.c_Id AS ORDEN,J.c_CalcEndDate AS FINCALCULADO,J.c_PlnDueDate AS FINPLANEADO,J.c_Product_id AS PRODUCTO,J.c_productdescr AS DESCRIPRODUCTO,J.c_prodlongdescr AS LDESCRIPRODUCTO,(J.c_CalcEndDate - J.c_PlnDueDate) AS DelayTime,J.C_RECIPE_ID AS IDESCANDALLO,J.C_CUSTOMER AS CLIENTE,J.C_QTYREQUIRED AS CANTIDADREQ,J.C_QTYREMAINING AS CANTIDADPEND," +
                             " (J.C_QTYREQUIRED - J.C_QTYREMAINING) AS CANTIDADFAB,J.C_PCTUNDERPROD,J.C_LOADTIME,J.C_TOOL_ID AS MOLDE,J.C_PERIPHERY1_ID,J.C_PERIPHERY2_ID,J.C_FACTOR_MUL1,J.C_FACTOR_MUL2,J.C_FACTOR_MUL3,J.C_FACTOR_DIV1,J.C_FACTOR_DIV2 AS OPERARIO,J.C_FACTOR_DIV3,J.C_LOCATION,J.C_STDSPEED,J.C_PLNSPEED AS CICLOPLAN,J.C_ACTSPEED AS CICLOREAL,J.C_MOUNTTIME,J.C_DISMOUNTTIME,J.C_SEQNR,J.C_PARINDEX," +
                             " J.C_PRIORITY,J.C_GRPINDEX,J.C_PLNSTARTDATE,J.C_PLNDUEDATE,J.C_ESTSTARTDATE,J.C_ESTENDDATE,J.C_REQUIREDBYDATE,J.C_CALCSTARTDATE,J.C_CALCENDDATE,J.C_ACTSTARTDATE AS INIORDEN,J.C_ACTENDDATE AS FINORDEN,J.C_CALCPRODTIME,J.C_CALCSETUPTIME,J.C_CALCRESETTIME,J.C_CUSTOMSTRING2,J.C_CUSTOMSTRING3,J.C_CUSTOMVALUE1,J.C_CUSTOMVALUE2,J.C_CUSTOMVALUE3,J.C_PARENT_ID,J.C_MASTERJOB,J.C_SHORT_DESCR,J.C_LONG_DESCR,J.C_REMARKS AS NOTAORD," +
                             " T.C_TYPE AS UBIC,T.C_CHARACTERISTICS AS UBIC2,T.C_LONG_DESCR AS MOLDESCRIP,T.C_REMARKS AS NOTAMOL,T.C_MAXIMUMCAVITIES AS CAVIDADES,P.C_REMARKS AS NOTAPROD,M.C_ID as MATREFERENCIA,M.C_MATTYPE_ID as TIPOMAT,M.C_LONG_DESCR AS DESCRIP,M.C_SHORT_DESCR AS UBICACION,M.C_REMARKS AS NOTAS,R.C_UNITS AS CONSUMO,R.C_REMARKS AS NOTASREC,Y.C_REMARKS AS NOTASREC2,(J.C_QTYREQUIRED * R.C_UNITS) AS MATREQ,J.C_CUSTOMSTRING3 as CALI" +
                             " FROM PCMS.t_Jobs J,PCMS.t_PRODUCTS P,PCMS.t_Tools T,PCMS.t_materials M,PCMS.T_RECIPES Y,PCMS.t_recipe_x_material R WHERE ((NVL(J.c_Id, ' ') LIKE '66059' OR j.c_Id = '"+Orden+"')) AND J.C_TOOL_ID = T.C_ID AND J.c_Product_id = P.C_ID AND R.C_RECIPE_ID = J.c_Product_id AND R.C_RECIPE_ID = Y.C_ID AND M.C_ID = R.C_MATERIAL_ID ORDER BY J.c_Id ASC, M.C_ID DESC";
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
        */
        /*

        
        public DataSet devuelve_horasXReferenciaXOperario(int MOLDE, int operario)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT MAX (C_TOOL) AS MOLDE, MAX (C_PRODUCT) AS PRODUCTO, MAX (C_PRODUCTDESCR) as DESCRIPCION, C_CLOCKNUMBER, C_OPERATORNAME, SUM(TRUNC((C_ACTUALAVAILABLETIME/60),2)) AS TIEMPOHORAS, SUM (C_ACTUALAVAILABLETIME) AS HORAS" +
                             " FROM HIS.T_HISOPERATORS WHERE C_RUNTIME > 0 AND C_TOOL = " + MOLDE + " AND C_CLOCKNUMBER = " + operario + " GROUP BY C_CLOCKNUMBER, C_OPERATORNAME ORDER BY TIEMPOHORAS DESC";
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

        public DataSet devuelve_calidadplanta_logueadoXMaquina(string maquina)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT E.C_NAME, E.C_CLOCKNO, L.C_OPERATORTYPE FROM OPER.T_LOGS L, PCMS.T_EMPLOYEES E, PCMS.T_OPERATORS O, PCMS.T_OPERATORTYPES T, PCMS.T_MACHINES M, PCMS.T_WORKCENTRES W WHERE L.C_REFLOG_ID IS NULL AND (L.C_TYPE = 0 OR L.C_TYPE = 2) AND L.C_OPERATOR = E.C_ID AND E.C_ID = O.C_ID AND L.C_OPERATORTYPE  = T.C_ID AND L.C_MACHINE = M.C_ID AND L.C_MACHINE = '" + maquina + "' AND L.C_OPERATORTYPE = 'CALIDADPLANTA' AND M.C_WORKCENTRE_ID = W.C_ID (+) ORDER BY L.C_OPERATORTYPE";
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
        */
        public DataSet Devuelve_LogueadoXMaquina(string maquina)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT E.C_NAME, E.C_CLOCKNO, CASE WHEN L.C_OPERATORTYPE = 'OPERATOR' THEN 'OPERARIO' ELSE CAST(L.C_OPERATORTYPE AS VARCHAR(20)) END AS C_OPERATORTYPE       FROM OPER.T_LOGS L, PCMS.T_EMPLOYEES E, PCMS.T_OPERATORS O, PCMS.T_OPERATORTYPES T, PCMS.T_MACHINES M WHERE L.C_REFLOG_ID IS NULL AND (L.C_TYPE = 0 OR L.C_TYPE = 2) AND L.C_OPERATOR = E.C_ID AND E.C_ID = O.C_ID AND L.C_OPERATORTYPE  = T.C_ID AND L.C_MACHINE = M.C_ID AND L.C_MACHINE = '" + maquina + "' ORDER BY L.C_OPERATORTYPE desc";
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
        
        public DataSet Devuelve_HorasXReferenciaSMARTH(string where)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT C_CLOCKNUMBER, C_OPERATORNAME, SUM(TRUNC((C_ACTUALAVAILABLETIME/60),2)) AS TIEMPOHORAS, CASE WHEN SUM(TRUNC((C_ACTUALAVAILABLETIME/60),2)) < 10 THEN 'I'" +
                                " WHEN C_OPERATORTYPE = '5' THEN 'O'  WHEN C_OPERATORTYPE = '4' THEN 'O' WHEN SUM(TRUNC((C_ACTUALAVAILABLETIME/ 60),2)) > 10 and SUM(TRUNC((C_ACTUALAVAILABLETIME/ 60),2)) < 80 THEN 'L' ELSE 'U'  END AS NIVEL, MAX(C_STARTTIME) AS REVISION," +
                                " CASE WHEN C_OPERATORTYPE = 1 THEN 'OPERARIO'  WHEN C_OPERATORTYPE = '4' THEN 'CALIDAD PLANTA'  WHEN C_OPERATORTYPE = '3' THEN 'CAMBIADOR' WHEN C_OPERATORTYPE = '5' THEN 'ENCARGADO' ELSE 'OTROS' END AS TIPOOPERARIO"+
                                
                                " FROM HIS.T_HISOPERATORS WHERE C_RUNTIME > 0 " + where + " GROUP BY C_CLOCKNUMBER, C_OPERATORNAME, C_OPERATORTYPE ORDER BY TIEMPOHORAS DESC";                    
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

        public DataSet devuelve_estado_GP12(string referencia)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT [Referencia],[EstadoActual], Razon, Observaciones FROM[SMARTH_DB].[dbo].[GP12_ProductosEstados] E LEFT JOIN [SMARTH_DB].[dbo].[AUX_GP12_RazonRevision] R ON E.EstadoActual = R.Id WHERE Referencia = '" + referencia+"'";
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

        public DataSet devuelve_fechaModDocumentos(string MOLDE)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT TOP 1 [Molde],[FechaModificacion], RazonModificacion FROM[SMARTH_DB].[dbo].[DOCUMENTAL_Base_DocumentosXReferencias] WHERE MOLDE = '"+MOLDE+"' order by FechaModificacion desc";
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

        public bool existe_validacion_operario(string numoperario, string molde)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT [NumOperario],[Molde],[Referencia],[Fecha] FROM[SMARTH_DB].[dbo].[DOCUMENTAL_Operarios_Validacion] WHERE NumOperario = '"+numoperario+"' AND Molde = '"+molde+"'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                if(ds.Tables[0].Rows.Count > 0)
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
                cnn_SMARTH.Close();
                return false;
            }
        }

        public bool existe_validacion_operario_enfecha(string numoperario, string molde, string fecha)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT * FROM [SMARTH_DB].[dbo].[DOCUMENTAL_Operarios_Validacion] WHERE(FECHA < '" + fecha + "' OR FECHA IS NULL) AND NumOperario = '" + numoperario + "' AND Molde = RTRIM('" + molde + "')";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
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
                cnn_SMARTH.Close();
                return false;
            }
        }

        public void insertar_validacion_operario(string numoperario, string molde)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "";
                sql = "INSERT INTO [SMARTH_DB].[dbo].[DOCUMENTAL_Operarios_Validacion] ([NumOperario], [Molde], [EstadoAviso]) " +
                       "VALUES ('" + numoperario + "','" + molde + "', 0)";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception)
            {
            }
        }

        public void actualizar_validacion_operario(string numoperario, string molde)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "";
                sql = "UPDATE [SMARTH_DB].[dbo].[DOCUMENTAL_Operarios_Validacion] SET [EstadoAviso] = 1, [Fecha] = SYSDATETIME() WHERE NumOperario = '" + numoperario + "' AND Molde = '" + molde + "'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception)
            {
            }
        }

        public void resetea_alarma_validacion_operario(string numoperario, string molde)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "";
                sql = "UPDATE [SMARTH_DB].[dbo].[DOCUMENTAL_Operarios_Validacion] SET [EstadoAviso] = 0 WHERE NumOperario = '" + numoperario + "' AND Molde = '" + molde + "'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception)
            {
            }
        }
        public void insertar_feedback_operario(string numoperario, string numoperario2, string molde, string feedback)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "";
                sql = "INSERT INTO[SMARTH_DB].[dbo].[DOCUMENTAL_Operarios_Feedback]"+
                        " ([NumOperario],[NumOperario2],[Molde],[Fecha],[FeedbackDocumento],[Estado])"+
                        " VALUES("+numoperario+ "," + numoperario2 + ", RTRIM('"+molde+ "'), SYSDATETIME(), '" + feedback + "', 0)";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception)
            {
            }
        }

        public void Insertar_feedback_operario_V2(string numoperario, string numoperario2, string molde, string referencia, string feedback)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "";
                sql = "INSERT INTO[SMARTH_DB].[dbo].[DOCUMENTAL_Operarios_Feedback]" +
                        " ([NumOperario],[NumOperario2],[Referencia],[Molde],[Fecha],[FeedbackDocumento],[Estado])" +
                        " VALUES(" + numoperario + "," + numoperario2 + ",RTRIM('"+ referencia + "'), RTRIM('" + molde + "'), SYSDATETIME(), '" + feedback + "', 0)";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception)
            {
            }
        }


    }
}