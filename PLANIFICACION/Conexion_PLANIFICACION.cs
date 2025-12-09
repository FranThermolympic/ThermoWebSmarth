using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace ThermoWeb.PLANIFICACION
{
    public class Conexion_PLANIFICACION
    {
        private readonly SqlConnection cnn_GP12 = new SqlConnection();
        private readonly OracleConnection cnn_bms = new OracleConnection();
        private readonly SqlConnection cnn_NAV = new SqlConnection();

        public Conexion_PLANIFICACION()
        {
            cnn_GP12.ConnectionString = ConfigurationManager.ConnectionStrings["area_rechazo"].ToString();          
            cnn_bms.ConnectionString = ConfigurationManager.ConnectionStrings["BMS"].ToString();         
            cnn_NAV.ConnectionString = ConfigurationManager.ConnectionStrings["NAV"].ToString();
        }
        //SMARTH
        
        public DataTable Devuelve_Planificacion_SMARTH(string orderby, int seqnr)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT * FROM(SELECT[Maquina], 'R' + [Maquina] AS ROBOT,[Orden],[Producto],[ProdDescript],[MOLDE],[TiempoMINUTOS],[Tiempo],[SEQNR],[REMARKPRODUCTO],[REMARKORDEN],[REMARKRECETA],[PRIORIDAD],[PRIORIDADDEC]"+
                                                " FROM[SMARTH_DB].[dbo].[AUX_Planificacion_Prioridades] WHERE SEQNR <= " + seqnr + ") PREV" +

                    //"SELECT * FROM(SELECT [Hora],[Maquina],'R' + [Maquina] AS ROBOT, [ACT_Orden],[NEXT_Orden],[ACT_Producto],[ACT_ProdDescript],[NEXT_Producto],[NEXT_ProdDescript],[ACT_CantPendiente],[ACT_MOLDE],[NEXT_MOLDE],[NEXT_ENTREGAPLANIFICADO],[NEXT_INICIOPLANIFICADO],[NEXT_RECETA],[SEQNR], CASE WHEN PRIORIDAD = '' THEN '-' ELSE PRIORIDAD END AS PRIORIDAD,cast([FINCALCULADO] as datetime) as FINCALCULADO,[FECHACAMBIOMAXIMO],[TIMETOGO],[REMARKS]" +
                    //                          " FROM[SMARTH_DB].[dbo].[AUX_Planificacion_Prevision] PRV WHERE SEQNR >= 0  AND SEQNR <= " + seqnr + ") PREV" +
                                                " LEFT JOIN(SELECT PARTEMAQ, IdMaquinaCHAR, MotivoAveria as MAQAVERIA, Reparacion as MAQREPARACION FROM" +
                                                            " (SELECT MAX([IdMantenimiento]) AS PARTEMAQ, [IDMaquina] AS MAQ" +
                                                                " FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas]" +
                                                                " where Revisado = 0 AND IdTipoMantenimiento <> 4" +
                                                                " GROUP BY[IDMaquina]) PAR" +
                                                " LEFT JOIN[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas] DOC ON PAR.PARTEMAQ = DOC.IdMantenimiento" +
                                                " LEFT JOIN SMARTH_DB.DBO.AUX_Lista_Máquinas MAQ ON CAST(PAR.MAQ AS varchar) = MAQ.IdMaquina" +
                                                " WHERE IdMaquinaCHAR<> '-' AND IdMaquinaCHAR NOT LIKE 'R%') MAQ ON PREV.Maquina = MAQ.IdMaquinaCHAR" +

                                        " LEFT JOIN(SELECT PARTEROB, MAQ.IdMaquinaCHAR AS ROBOMAQ, MotivoAveria as ROBAVERIA, Reparacion as ROBREPARACION FROM" +
                                                " (SELECT MAX([IdMantenimiento]) AS PARTEROB, [IDMaquina] AS MAQ" +
                                                " FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas]" +
                                                " where Revisado = 0 AND IdTipoMantenimiento<> 4" +
                                                " GROUP BY[IDMaquina]) PAR" +
                                                " LEFT JOIN[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas] DOC ON PAR.PARTEROB = DOC.IdMantenimiento" +
                                                " LEFT JOIN SMARTH_DB.DBO.AUX_Lista_Máquinas MAQ ON CAST(PAR.MAQ AS varchar) = MAQ.IdMaquina" +
                                                "  WHERE IdMaquinaCHAR<> '-' AND IdMaquinaCHAR LIKE 'R%') ROB ON PREV.ROBOT = ROB.ROBOMAQ" +

                                        " LEFT JOIN(SELECT PARTE AS PARTEMOL, IDMoldes AS NUMMOLDE , MotivoAveria as MOLAVERIA, Reparacion as MOLREPARACION FROM" +
                                                " (SELECT  MAX(IdReparacionMolde) AS PARTE" +
                                                " FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes]" +
                                                " where Revisado = 0 AND IdTipoRevision <> 4 AND IDMoldes > 0" +
                                                " GROUP BY[IDMoldes]) PAR" +
                                                " LEFT JOIN[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes] DOC ON PAR.PARTE = DOC.IdReparacionMolde) MOL ON PREV.MOLDE = MOL.NUMMOLDE" +

                                        " LEFT JOIN(SELECT NC.[IdNoConformidad], AL.Referencia AS NCPRODUCTO, AL.DescripcionProblema" +
                                                " ,CASE WHEN D3CIERRE = '' OR D3CIERRE IS NULL THEN 'D3 Pendiente (' + LEFT(D3, 10) + ')' ELSE '' END AS D3ESTADO" +
                                                " ,CASE WHEN (D6CIERRE = '' OR D6CIERRE IS NULL) and CheckD6 = 1 THEN 'D6 Pendiente (' + LEFT(D6, 10) + ')' ELSE '' END AS D6ESTADO" +
                                                " ,CASE WHEN (D8CIERRE = '' OR D8CIERRE IS NULL) and CheckD8 = 1 THEN 'D8 Pendiente (' + LEFT(D8, 10) + ')' ELSE '' END AS D8ESTADO" +
                                                " FROM[SMARTH_DB].[dbo].[NC_Estado_No_Conformidad] NC" +
                                                " LEFT JOIN[SMARTH_DB].[dbo].[NC_Alerta_de_calidad] AL ON NC.IdNoConformidad = AL.IdNoConformidad" +
                                                " WHERE (NC.FechaOriginal > GETDATE() - 180) OR ((CheckD3 = 1 AND(D3CIERRE = '' OR D3CIERRE IS NULL))" +
                                                " OR(CheckD6 = 1 AND(D6CIERRE = '' OR D6CIERRE IS NULL))" +
                                                " OR(CheckD8 = 1 AND(D8CIERRE = '' OR D8CIERRE IS NULL)))) NC ON PREV.Producto = NC.NCPRODUCTO" +
                                        " LEFT JOIN(SELECT[Referencia], 'GP12' AS GP12 FROM[SMARTH_DB].[dbo].[GP12_ProductosEstados] where EstadoActual <> 0) GP12 ON PREV.Producto = GP12.Referencia"+
                                                " " +orderby+"";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_GP12);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_GP12.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_GP12.Close();
                return null;
            }
        }

        public DataTable Devuelve_Linea_Planificacion(string orden)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT * FROM(SELECT[Maquina], 'R' + [Maquina] AS ROBOT,[Orden],[Producto],[ProdDescript],[MOLDE],[TiempoMINUTOS],[Tiempo],[SEQNR],[REMARKPRODUCTO],[REMARKORDEN],[REMARKRECETA],[PRIORIDAD],[PRIORIDADDEC]" +
                                                " FROM[SMARTH_DB].[dbo].[AUX_Planificacion_Prioridades] WHERE Orden = " + orden + ") PREV" +

                                                //"SELECT * FROM(SELECT [Hora],[Maquina],'R' + [Maquina] AS ROBOT, [ACT_Orden],[NEXT_Orden],[ACT_Producto],[ACT_ProdDescript],[NEXT_Producto],[NEXT_ProdDescript],[ACT_CantPendiente],[ACT_MOLDE],[NEXT_MOLDE],[NEXT_ENTREGAPLANIFICADO],[NEXT_INICIOPLANIFICADO],[NEXT_RECETA],[SEQNR], CASE WHEN PRIORIDAD = '' THEN '-' ELSE PRIORIDAD END AS PRIORIDAD,cast([FINCALCULADO] as datetime) as FINCALCULADO,[FECHACAMBIOMAXIMO],[TIMETOGO],[REMARKS]" +
                                                //                          " FROM[SMARTH_DB].[dbo].[AUX_Planificacion_Prevision] PRV WHERE SEQNR >= 0  AND SEQNR <= " + seqnr + ") PREV" +
                                                " LEFT JOIN(SELECT PARTEMAQ, IdMaquinaCHAR, MotivoAveria as MAQAVERIA, Reparacion as MAQREPARACION FROM" +
                                                            " (SELECT MAX([IdMantenimiento]) AS PARTEMAQ, [IDMaquina] AS MAQ" +
                                                                " FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas]" +
                                                                " where Revisado = 0 AND IdTipoMantenimiento <> 4" +
                                                                " GROUP BY[IDMaquina]) PAR" +
                                                " LEFT JOIN[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas] DOC ON PAR.MAQ = DOC.IdMantenimiento" +
                                                " LEFT JOIN SMARTH_DB.DBO.AUX_Lista_Máquinas MAQ ON CAST(PAR.MAQ AS varchar) = MAQ.IdMaquina" +
                                                " WHERE IdMaquinaCHAR<> '-' AND IdMaquinaCHAR NOT LIKE 'R%') MAQ ON PREV.Maquina = MAQ.IdMaquinaCHAR" +

                                        " LEFT JOIN(SELECT PARTEROB, MAQ.IdMaquinaCHAR AS ROBOMAQ, MotivoAveria as ROBAVERIA, Reparacion as ROBREPARACION FROM" +
                                                " (SELECT MAX([IdMantenimiento]) AS PARTEROB, [IDMaquina] AS MAQ" +
                                                " FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas]" +
                                                " where Revisado = 0 AND IdTipoMantenimiento<> 4" +
                                                " GROUP BY[IDMaquina]) PAR" +
                                                " LEFT JOIN[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas] DOC ON PAR.MAQ = DOC.IdMantenimiento" +
                                                " LEFT JOIN SMARTH_DB.DBO.AUX_Lista_Máquinas MAQ ON CAST(PAR.MAQ AS varchar) = MAQ.IdMaquina" +
                                                "  WHERE IdMaquinaCHAR<> '-' AND IdMaquinaCHAR LIKE 'R%') ROB ON PREV.ROBOT = ROB.ROBOMAQ" +

                                        " LEFT JOIN(SELECT PARTE AS PARTEMOL, IDMoldes AS NUMMOLDE , MotivoAveria as MOLAVERIA, Reparacion as MOLREPARACION FROM" +
                                                " (SELECT  MAX(IdReparacionMolde) AS PARTE" +
                                                " FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes]" +
                                                " where Revisado = 0 AND IdTipoRevision <> 4 AND IDMoldes > 0" +
                                                " GROUP BY[IDMoldes]) PAR" +
                                                " LEFT JOIN[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes] DOC ON PAR.PARTE = DOC.IdReparacionMolde) MOL ON PREV.MOLDE = MOL.NUMMOLDE" +

                                        " LEFT JOIN(SELECT NC.[IdNoConformidad], AL.Referencia AS NCPRODUCTO, AL.DescripcionProblema" +
                                                " ,CASE WHEN D3CIERRE = '' OR D3CIERRE IS NULL THEN 'D3 Pendiente (' + LEFT(D3, 10) + ')' ELSE '' END AS D3ESTADO" +
                                                " ,CASE WHEN D6CIERRE = '' OR D6CIERRE IS NULL THEN 'D6 Pendiente (' + LEFT(D6, 10) + ')' ELSE '' END AS D6ESTADO" +
                                                " ,CASE WHEN D8CIERRE = '' OR D8CIERRE IS NULL THEN 'D8 Pendiente (' + LEFT(D8, 10) + ')' ELSE '' END AS D8ESTADO" +
                                                " FROM[SMARTH_DB].[dbo].[NC_Estado_No_Conformidad] NC" +
                                                " LEFT JOIN[SMARTH_DB].[dbo].[NC_Alerta_de_calidad] AL ON NC.IdNoConformidad = AL.IdNoConformidad" +
                                                " WHERE((CheckD3 = 1 AND(D3CIERRE = '' OR D3CIERRE IS NULL))" +
                                                " OR(CheckD6 = 1 AND(D6CIERRE = '' OR D6CIERRE IS NULL))" +
                                                " OR(CheckD8 = 1 AND(D8CIERRE = '' OR D8CIERRE IS NULL)))) NC ON PREV.Producto = NC.NCPRODUCTO" +
                                        " LEFT JOIN(SELECT[Referencia], 'GP12' AS GP12 FROM[SMARTH_DB].[dbo].[GP12_ProductosEstados] where EstadoActual <> 0) GP12 ON PREV.Producto = GP12.Referencia";
                                              
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn_GP12);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                cnn_GP12.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cnn_GP12.Close();
                return null;
            }
        }

        public DataTable Devuelve_lista_reparaciones_molde(int MOLDE)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT [IdReparacionMolde],[IDMoldes],[MotivoAveria],[Reparacion], Terminado,[Revisado],[RevisadoNOK], A.Texto " +
                    " FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes] T" +
                    " LEFT JOIN [SMARTH_DB].[dbo].[MANTENIMIENTO_Aux_Estados_Reparacion] A ON CONVERT(VARCHAR,[Terminado]) + CONVERT(VARCHAR,[Revisado]) + CONVERT(VARCHAR,[RevisadoNOK]) = A.IdEstado" +
                    " where Texto <> 'Reparación OK' AND IDMoldes = " + MOLDE + " order by IdReparacionMolde desc";
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

        public DataTable Devuelve_lista_reparaciones_maquina(string MAQUINA)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT [IdMantenimiento],T.IdMaquinaCHAR,[MotivoAveria],[Reparacion],[Terminado],[Revisado],[RevisadoNOK], a.Texto" +
                                    " FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas] M " +
                                    " LEFT JOIN [SMARTH_DB].[dbo].[AUX_Lista_Máquinas] T ON M.IDMaquina = T.IdMaquina " +
                                    " LEFT JOIN [SMARTH_DB].[dbo].[MANTENIMIENTO_Aux_Estados_Reparacion] A ON CONVERT(VARCHAR, [Terminado]) +CONVERT(VARCHAR,[Revisado]) + CONVERT(VARCHAR,[RevisadoNOK]) = A.IdEstado" +
                                    " WHERE Texto <> 'Reparación OK' AND (IdMaquinaCHAR = '" + MAQUINA + "' OR IdMaquinaCHAR = 'R" + MAQUINA + "') order by IdMantenimiento desc";
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

        public DataSet cargar_acciones_abiertas_ordenadas(string orderby, int seqnr)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT Maquina, Orden, REF, CONCAT(Referencia, Descripcion) AS REFERENCIA, Molde, Descripcion, SDescripcion, CASE WHEN Posicion > 0 THEN 'En cola' WHEN Posicion = 0 THEN to_char(trunc(Tiempo_restante / 60), '999999') || ' H' || to_char(round(Tiempo_restante - (hh * 60)), '99') || ' MIN' END as Tiempo, Posicion, ParteMolde, RemarkMolde, RemarkProducto, RemarkOrden, ParteMaquina, RemarkMaquina, Prioridad,Prioridaddec,RemarksReceta" +
                              " FROM(SELECT distinct j1.c_machine_id AS Maquina, TRIM(j1.C_ID) AS Orden, j1.c_tool_id as Molde, j1.c_prodlongdescr AS Descripcion, j1.C_PRODUCTDESCR AS SDescripcion, TRIM(j1.c_product_id) as REF, CONCAT(TRIM(j1.c_product_id), ' ') AS Referencia, j1.C_CALCENDDATE, j1.C_SEQNR AS Posicion, j1.C_PRIORITY AS Prioridad, PCMS.fncCstrm_getnumData(m.c_id, m.c_rownr, 19935, 14) AS Tiempo_restante, trunc((PCMS.fncCstrm_getnumData(m.c_id, m.c_rownr, 19935, 14)) / 60)  AS hh," +
                              " PCMS.fncCstrm_getnumData(m.c_id, m.c_rownr, 19968, 14) AS Completo, t.C_STDNUMVALUES01 as ParteMolde, t.c_remarks as RemarkMolde, p.c_remarks AS RemarkProducto, j1.C_REMARKS as RemarkOrden, m.C_CUSTOMSTRING1 as ParteMaquina, DECODE(j1.C_PRIORITY, 100, NULL, j1.C_PRIORITY) AS Prioridaddec, m.C_REMARKS as RemarkMaquina, r.C_REMARKS as RemarksReceta FROM   PCMS.T_JOBS j1, PCMS.t_machines m, PCMS.T_products p, PCMS.t_tools t, PCMS.t_recipes R" +
                              " WHERE p.C_ID(+) = j1.C_PRODUCT_ID AND j1.c_seqnr >= 0 AND j1.c_seqnr <= "+seqnr+" AND j1.c_machine_id = m.c_id AND j1.C_TOOL_ID = t.c_id(+) AND p.C_ID = r.C_ID" +
                               " "+orderby+")";
               // "SELECT Maquina, Orden, REF, CONCAT(Referencia, Descripcion) AS REFERENCIA, Molde, CASE WHEN Posicion > 0 THEN 'En cola' WHEN Posicion = 0 THEN to_char(trunc(Tiempo_restante / 60), '999999') || ' H' || to_char(round(Tiempo_restante - (hh * 60)), '99') || ' MIN' END as Tiempo, Posicion, ParteMolde, RemarkMolde, RemarkProducto, RemarkOrden, ParteMaquina, RemarkMaquina, Prioridad, Prioridaddec" +
               //              " FROM (SELECT distinct j1.c_machine_id AS Maquina,TRIM(j1.C_ID) AS Orden, j1.c_tool_id as Molde, j1.c_prodlongdescr AS Descripcion, TRIM(j1.c_product_id) as REF, CONCAT(TRIM(j1.c_product_id), ' ') AS Referencia, j1.C_CALCENDDATE, j1.C_SEQNR AS Posicion, j1.C_PRIORITY AS Prioridad, PCMS.fncCstrm_getnumData(m.c_id, m.c_rownr, 19935, 14) AS Tiempo_restante, trunc((PCMS.fncCstrm_getnumData(m.c_id, m.c_rownr, 19935, 14)) / 60)  AS hh," +
               //              " PCMS.fncCstrm_getnumData(m.c_id, m.c_rownr, 19968, 14) AS Completo, t.C_STDNUMVALUES01 as ParteMolde, t.c_remarks as RemarkMolde, p.c_remarks AS RemarkProducto, DECODE(j1.C_PRIORITY, 100, NULL,j1.C_PRIORITY) AS Prioridaddec, j1.C_REMARKS as RemarkOrden, m.C_CUSTOMSTRING1 as ParteMaquina, m.C_REMARKS as RemarkMaquina FROM   PCMS.T_JOBS j1, PCMS.t_machines m, PCMS.T_products p, PCMS.t_tools t" +
               //              " WHERE p.C_ID(+) = j1.C_PRODUCT_ID AND j1.c_seqnr >= 0 AND j1.c_seqnr <= 3 AND j1.c_machine_id = m.c_id  AND j1.C_TOOL_ID = t.c_id(+) " +orderby+")";
                            //AND(p.c_remarks <> ' ' OR t.c_remarks <> ' ' OR j1.C_REMARKS <> ' ')
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataSet acciones = new DataSet();
                query.Fill(acciones);
                cnn_bms.Close();
                return acciones;
            }
            catch (Exception)
            {
                cnn_bms.Close();
                DataSet acciones = new DataSet();
                acciones = null;
                return acciones;
            }
        }
        public DataSet cargar_acciones_abiertas_ordenadasSEQ0(string orderby)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT Maquina, Prioridad, Prioridaddec, Orden, REF, Molde, Descripcion, SDescripcion, CASE WHEN Posicion > 0 THEN 'En cola' WHEN Posicion = 0 THEN to_char(trunc(Tiempo_restante / 60), '999999') || ' H' || to_char(round(Tiempo_restante - (hh * 60)), '99') || ' MIN' END as Tiempo, Posicion, RemarkMolde, RemarkProducto, RemarkOrden, RemarkMaquina, RemarksReceta" +
                             " FROM(SELECT  j1.C_PRIORITY AS Prioridad,j1.c_machine_id AS Maquina,TRIM(j1.C_ID) AS Orden,j1.c_tool_id as Molde,j1.c_prodlongdescr AS Descripcion,j1.C_PRODUCTDESCR AS SDescripcion,TRIM(j1.c_product_id) as REF,j1.C_CALCENDDATE,j1.C_SEQNR AS Posicion,Min(j1.C_SEQNR) OVER(Partition by j1.C_PRIORITY) SELECTOR,PCMS.fncCstrm_getnumData(m.c_id, m.c_rownr, 19935, 14) AS Tiempo_restante," +
                             " trunc((PCMS.fncCstrm_getnumData(m.c_id, m.c_rownr, 19935, 14)) / 60)  AS hh,t.c_remarks as RemarkMolde,p.c_remarks AS RemarkProducto,j1.C_REMARKS as RemarkOrden,DECODE(j1.C_PRIORITY, 100, NULL, j1.C_PRIORITY) AS Prioridaddec,m.C_REMARKS as RemarkMaquina,r.C_REMARKS as RemarksReceta" +
                             " FROM PCMS.T_JOBS j1, PCMS.t_machines m, PCMS.T_products p, PCMS.t_tools t, PCMS.t_recipes R WHERE p.C_ID(+) = j1.C_PRODUCT_ID AND j1.c_seqnr >= 0 AND j1.c_seqnr <= 4 AND j1.c_machine_id = m.c_id AND j1.C_TOOL_ID = t.c_id(+) AND p.C_ID = r.C_ID  AND j1.C_PARINDEX = 1  ORDER BY Prioridad ASC) A" +
                            " WHERE A.Posicion = A.SELECTOR order by Prioridad ASC";
                //string sql = "SELECT Maquina, Orden, REF, CONCAT(Referencia, Descripcion) AS REFERENCIA, Molde, Descripcion, SDescripcion, CASE WHEN Posicion > 0 THEN 'En cola' WHEN Posicion = 0 THEN to_char(trunc(Tiempo_restante / 60), '999999') || ' H' || to_char(round(Tiempo_restante - (hh * 60)), '99') || ' MIN' END as Tiempo, Posicion, ParteMolde, RemarkMolde, RemarkProducto, RemarkOrden, ParteMaquina, RemarkMaquina, DISTINCT (Prioridad),Prioridaddec,RemarksReceta" +
                //              " FROM(SELECT distinct j1.c_machine_id AS Maquina, TRIM(j1.C_ID) AS Orden, j1.c_tool_id as Molde, j1.c_prodlongdescr AS Descripcion, j1.C_PRODUCTDESCR AS SDescripcion, TRIM(j1.c_product_id) as REF, CONCAT(TRIM(j1.c_product_id), ' ') AS Referencia, j1.C_CALCENDDATE, j1.C_SEQNR AS Posicion, j1.C_PRIORITY AS Prioridad, PCMS.fncCstrm_getnumData(m.c_id, m.c_rownr, 19935, 14) AS Tiempo_restante, trunc((PCMS.fncCstrm_getnumData(m.c_id, m.c_rownr, 19935, 14)) / 60)  AS hh," +
                //              " PCMS.fncCstrm_getnumData(m.c_id, m.c_rownr, 19968, 14) AS Completo, t.C_STDNUMVALUES01 as ParteMolde, t.c_remarks as RemarkMolde, p.c_remarks AS RemarkProducto, j1.C_REMARKS as RemarkOrden, m.C_CUSTOMSTRING1 as ParteMaquina, DECODE(j1.C_PRIORITY, 100, NULL, j1.C_PRIORITY) AS Prioridaddec, m.C_REMARKS as RemarkMaquina, r.C_REMARKS as RemarksReceta FROM   PCMS.T_JOBS j1, PCMS.t_machines m, PCMS.T_products p, PCMS.t_tools t, PCMS.t_recipes R" +
                //              " WHERE p.C_ID(+) = j1.C_PRODUCT_ID AND j1.c_seqnr >= 0 AND j1.c_seqnr <= 1 AND j1.c_machine_id = m.c_id AND j1.C_TOOL_ID = t.c_id(+) AND p.C_ID = r.C_ID" +
                //               " " + orderby + ")";
                // "SELECT Maquina, Orden, REF, CONCAT(Referencia, Descripcion) AS REFERENCIA, Molde, CASE WHEN Posicion > 0 THEN 'En cola' WHEN Posicion = 0 THEN to_char(trunc(Tiempo_restante / 60), '999999') || ' H' || to_char(round(Tiempo_restante - (hh * 60)), '99') || ' MIN' END as Tiempo, Posicion, ParteMolde, RemarkMolde, RemarkProducto, RemarkOrden, ParteMaquina, RemarkMaquina, Prioridad, Prioridaddec" +
                //              " FROM (SELECT distinct j1.c_machine_id AS Maquina,TRIM(j1.C_ID) AS Orden, j1.c_tool_id as Molde, j1.c_prodlongdescr AS Descripcion, TRIM(j1.c_product_id) as REF, CONCAT(TRIM(j1.c_product_id), ' ') AS Referencia, j1.C_CALCENDDATE, j1.C_SEQNR AS Posicion, j1.C_PRIORITY AS Prioridad, PCMS.fncCstrm_getnumData(m.c_id, m.c_rownr, 19935, 14) AS Tiempo_restante, trunc((PCMS.fncCstrm_getnumData(m.c_id, m.c_rownr, 19935, 14)) / 60)  AS hh," +
                //              " PCMS.fncCstrm_getnumData(m.c_id, m.c_rownr, 19968, 14) AS Completo, t.C_STDNUMVALUES01 as ParteMolde, t.c_remarks as RemarkMolde, p.c_remarks AS RemarkProducto, DECODE(j1.C_PRIORITY, 100, NULL,j1.C_PRIORITY) AS Prioridaddec, j1.C_REMARKS as RemarkOrden, m.C_CUSTOMSTRING1 as ParteMaquina, m.C_REMARKS as RemarkMaquina FROM   PCMS.T_JOBS j1, PCMS.t_machines m, PCMS.T_products p, PCMS.t_tools t" +
                //              " WHERE p.C_ID(+) = j1.C_PRODUCT_ID AND j1.c_seqnr >= 0 AND j1.c_seqnr <= 3 AND j1.c_machine_id = m.c_id  AND j1.C_TOOL_ID = t.c_id(+) " +orderby+")";
                //AND(p.c_remarks <> ' ' OR t.c_remarks <> ' ' OR j1.C_REMARKS <> ' ')
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataSet acciones = new DataSet();
                query.Fill(acciones);
                cnn_bms.Close();
                return acciones;
            }
            catch (Exception)
            {
                cnn_bms.Close();
                return null;
            }
        }
        public void Actualizar_Remarks_ProductoBMS(string referencia, string accionproducto)
        {
            try
            {
                cnn_bms.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = cnn_bms;
                cmd.CommandText = "UPDATE PCMS.T_PRODUCTS SET C_REMARKS = '"+accionproducto+"' WHERE C_ID = '"+referencia+"'";
                cmd.ExecuteNonQuery();
                cnn_bms.Close();
            }
            catch (Exception)
            {

                cnn_bms.Close();
            }
        }
        public void Actualizar_Remarks_OrdenesBMS(int prioridad, string orden, string accionorden)
        {
            try
            {
                cnn_bms.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = cnn_bms;
                cmd.CommandText = "UPDATE PCMS.T_JOBS SET C_PRIORITY = "+prioridad+", C_REMARKS = '" + accionorden + "' WHERE C_ID = '" + orden + "'";
                cmd.ExecuteNonQuery();
                cnn_bms.Close();
            }
            catch (Exception)
            {

                cnn_bms.Close();
            }
        }
        public void Borrar_Prioridades_OrdenesBMS()
        {
            try
            {
                cnn_bms.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = cnn_bms;
                cmd.CommandText = "UPDATE PCMS.T_JOBS SET C_PRIORITY = NULL WHERE C_PRIORITY >= 0";
                cmd.ExecuteNonQuery();
                cnn_bms.Close();
            }
            catch (Exception)
            {

                cnn_bms.Close();
            }
        }
        public void Actualizar_Parte_Molde(string parte, int terminado, int revisado_OK, int revisado_NOK)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "UPDATE [SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes] SET [Terminado] = "+terminado+", [Revisado] = "+revisado_OK+", [RevisadoNOK] = "+revisado_NOK+" WHERE IdReparacionMolde = '"+parte+"'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();

            }
            catch (Exception)
            {
                cnn_GP12.Close();
            }
        }
        public void Actualizar_Parte_Maquina(string parte, int terminado, int revisado_OK, int revisado_NOK)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "UPDATE [SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas] SET [Terminado] = " + terminado + ", [Revisado] = " + revisado_OK + ", [RevisadoNOK] = " + revisado_NOK + " WHERE IdMantenimiento = '" + parte + "'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();

            }
            catch (Exception)
            {
                cnn_GP12.Close();
            }
        }
        public int Devuelve_Molde_X_Parte(int parte)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT [IDMoldes] FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes] WHERE IdReparacionMolde = '" + parte + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_GP12.Close();
                return Convert.ToInt32(ds.Tables[0].Rows[0]["IDMoldes"].ToString());
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public void eliminar_accion_pendienteBMSMOLDE(int idMolde)
        {
            try
            {
                if (idMolde > 0)
                {
                    cnn_bms.Open();
                    string sql = "update PCMS.T_TOOLS set C_REMARKS = '', C_STDNUMVALUES01 = '' WHERE C_ID = '" + idMolde + "'";
                    OracleCommand delete = new OracleCommand(sql, cnn_bms);
                    delete.ExecuteNonQuery();
                    cnn_bms.Close();
                }
            }
            catch (Exception)
            {
            }
        }

        public DataSet leer_correosPrioridades()
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql1 = "SELECT DISTINCT Correo FROM [SMARTH_DB].[dbo].[AUX_Personal_Mandos] WHERE Mail_Prioridades = 1";
                SqlDataAdapter adapter = new SqlDataAdapter(sql1, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql1);
                cnn_GP12.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_GP12.Close();
                
                return null;
            }
        }

    }
}