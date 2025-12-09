using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;


namespace ThermoWeb.MANTENIMIENTO
{
    public class Conexion_MANTENIMIENTO
    {        
        private readonly OracleConnection cnn_bms = new OracleConnection();
        private readonly SqlConnection cnn_SMARTH = new SqlConnection();

        public Conexion_MANTENIMIENTO()
        {
            cnn_SMARTH.ConnectionString = ConfigurationManager.ConnectionStrings["SMARTH"].ToString();
            cnn_bms.ConnectionString = ConfigurationManager.ConnectionStrings["BMS"].ToString();
        }

        //########################################## COMUNES MANTENIMIENTO ################################################################\\
        public void mandar_mail2(string mensaje, string subject)
        {
            //Create MailMessage
            MailMessage email = new MailMessage();
            email.To.Add(new MailAddress("pedro@thermolympic.es"));
            email.From = new MailAddress("bms@thermolympic.es");
            email.Subject = subject + DateTime.Now.ToString("dd/MMM/yyy hh:mm:ss");
            email.Body = mensaje;
            email.IsBodyHtml = true;
            email.Priority = MailPriority.Normal;

            //Definir objeto SmtpClient
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.thermolympic.es";
            smtp.Port = 25;
            smtp.EnableSsl = false;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("bms@thermolympic.es", "010477Bms");

            //Enviar email
            try
            {
                smtp.Send(email);
                email.Dispose();
            }
            catch (Exception ex)
            {
            }
        }

        public int Devuelve_IDmaquina(string maquina)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;

                string sql = "SELECT IdMaquina " +
                             "FROM [SMARTH_DB].[dbo].[AUX_Lista_Máquinas] " +
                              "WHERE Maquina = '" + maquina + "'";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return Convert.ToInt16(ds.Tables[0].Rows[0]["IdMaquina"].ToString());
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return 0;
            }
        }

        public int Devuelve_IDperiferico(string nombre)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;

                string sql = "SELECT Id " +
                             "FROM [SMARTH_DB].[dbo].[AUX_Lista_MaquinasPeriferico] " +
                             "WHERE Máquina = '" + nombre + "'";

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

        public int Devuelve_IDTrabajo(string trabajo)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;

                string sql = "SELECT Id FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Aux_Opciones] WHERE TipoMantenimientoMolde = '" + trabajo + "'";
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

        public DataSet Tipos_trabajo()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;                
                string sql = "SELECT Id, TipoMantenimientoMolde FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Aux_Opciones] Where TipoMantenimientoMolde is not null order by Id asc";
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

        public DataSet Leer_correosMANTMOL()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql1 = "SELECT DISTINCT [Correo] FROM [SMARTH_DB].[dbo].[AUX_Personal_Mandos] WHERE OPActivo = 1 AND Correo IS NOT NULL AND Mail_MantMOL = 1";
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

        public DataSet Leer_correosMANTMAQ()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql1 = "SELECT DISTINCT [Correo] FROM [SMARTH_DB].[dbo].[AUX_Personal_Mandos] WHERE OPActivo = 1 AND Correo IS NOT NULL AND Mail_MantMAQ = 1";
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

        public DataSet Leer_correosADMIN()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql1 = "SELECT DISTINCT [Correo] FROM [SMARTH_DB].[dbo].[AUX_Personal_Mandos] WHERE OPActivo = 1 AND Correo IS NOT NULL AND Departamento = 'ADMINISTRADOR'";
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

        //########################################## ÍNDICE ################################################################\\

        public DataSet Comprobar_contadores_moldesV2()
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT RTRIM(R.C_ID) AS C_RESOURCE_ID, R.C_MAINTCOUNTER_ID, R.C_MAINTCONFIG_ID, R.C_RESOURCECOUNTER, R.C_THRESHOLDVALUE, R.C_TIMELASTUPDATE, R.C_TIMELASTMAINTENANCE, C.C_LONG_DESCR FROM PCMS.T_MAINTENANCERESOURCES R LEFT JOIN PCMS.T_MAINTENANCECONFIGCOUNTERS C ON R.C_MAINTCOUNTER_ID = C.C_MAINTCOUNTER_ID AND R.C_MAINTCONFIG_ID = C.C_MAINTCONFIG_ID WHERE R.C_RESOURCECOUNTER is not null AND R.C_RESOURCE_TYPE = 2 and R.C_RESOURCECOUNTER > R.C_THRESHOLDVALUE AND C_COUNTENABLED = 1";
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataSet ds = new DataSet();
                query.Fill(ds);
                cnn_bms.Close();
                return ds;
            }
            catch (Exception ex)
            {
                cnn_bms.Close();
                mandar_mail(ex.Message);
                return null;
            }
        }

        public DataSet Comprobar_contadores_maquinasV2()
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT RTRIM(R.C_ID) AS C_RESOURCE_ID, R.C_MAINTCOUNTER_ID,R.C_MAINTCONFIG_ID, R.C_RESOURCECOUNTER, R.C_THRESHOLDVALUE, R.C_TIMELASTUPDATE, R.C_TIMELASTMAINTENANCE, C.C_LONG_DESCR FROM PCMS.T_MAINTENANCERESOURCES R LEFT JOIN PCMS.T_MAINTENANCECONFIGCOUNTERS C ON R.C_MAINTCOUNTER_ID = C.C_MAINTCOUNTER_ID AND R.C_MAINTCONFIG_ID = C.C_MAINTCONFIG_ID WHERE R.C_RESOURCECOUNTER is not null AND R.C_RESOURCE_TYPE = 1 and R.C_RESOURCECOUNTER > R.C_THRESHOLDVALUE AND C_COUNTENABLED = 1";

                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataSet ds = new DataSet();
                query.Fill(ds);
                cnn_bms.Close();
                return ds;
            }
            catch (Exception ex)
            {
                cnn_bms.Close();
                mandar_mail(ex.Message);
                return null;
            }
        }

        //comprueba si hay un registro en MantenimientoPreventivo

        public int leer_preventivoV2(string equipo, int IdMantenimiento, string fecha)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT Count(*) as Contador FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_AUX_Preventivo] where Equipo = '" + equipo + "' and IdContador = " + IdMantenimiento + " and FechaReseteo = '" + fecha + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return Convert.ToInt32(ds.Tables[0].Rows[0]["Contador"].ToString());
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                mandar_mail(ex.Message);
                return 0;
            }
        }

        //inserta una nueva linea de molde/máquina y contador en mantenimiento preventivo 
        public void insertar_preventivoV2(string equipo, int IdContador, string fecha)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[MANTENIMIENTO_AUX_Preventivo] (Equipo, IdContador, FechaReseteo) VALUES ('" + equipo + "'," + IdContador + ",'" + fecha + "')";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                mandar_mail(ex.Message);
            }
        }

        //reinicia el contador de mantenimiento preventivo de un molde
        public void Reiniciar_contador_MoldeMaq(int ID, int limite)
        {
            try
            {
                cnn_bms.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = cnn_bms;
                //cmd.CommandText = "UPDATE PCMS.T_MAINTENANCERESOURCES SET C_RESOURCECOUNTER = 0, C_TIMELASTMAINTENANCE = TO_DATE('" + fecha + "', 'DD/MM/YYYY HH24:MI:SS')";
                //C_TIMELASTMAINTENAN = 26/01/2021 8:42:06
                //C_LIMITREACHED = 0
                //C_EVENTALERTHANDLE = 0
                //C_RESOURCECOUNTER = 0
                cmd.CommandText = "UPDATE PCMS.T_MAINTENANCERESOURCES SET C_RESOURCECOUNTER = 0, C_LIMITREACHED = 0, C_EVENTALERTHANDLED = 0, C_TIMELASTMAINTENANCE = TO_DATE(SYSDATE, 'DD/MM/rrrr HH24:MI:SS') WHERE C_ID = '" + ID + "' and C_THRESHOLDVALUE = '" + limite + "'";
                cmd.ExecuteNonQuery();
                cnn_bms.Close();
            }
            catch (Exception ex)
            {
                cnn_bms.Close();
                mandar_mail(ex.Message);
            }
        }

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

        public DataSet Comprobar_contadores_molde_Seleccionado(int molde, int threshold)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT RTRIM(R.C_ID) AS C_RESOURCE_ID, R.C_MAINTCOUNTER_ID, R.C_MAINTCONFIG_ID, R.C_RESOURCECOUNTER, R.C_THRESHOLDVALUE, R.C_TIMELASTUPDATE, R.C_TIMELASTMAINTENANCE, C.C_LONG_DESCR FROM PCMS.T_MAINTENANCERESOURCES R LEFT JOIN PCMS.T_MAINTENANCECONFIGCOUNTERS C ON R.C_MAINTCOUNTER_ID = C.C_MAINTCOUNTER_ID AND R.C_MAINTCONFIG_ID = C.C_MAINTCONFIG_ID" +
                                " WHERE R.C_ID = '" + molde + "' AND R.C_THRESHOLDVALUE = '" + threshold + "' AND C_COUNTENABLED = 1";
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataSet ds = new DataSet();
                query.Fill(ds);
                cnn_bms.Close();
                return ds;
            }
            catch (Exception ex)
            {
                cnn_bms.Close();
                mandar_mail(ex.Message);
                return null;
            }
        }


        //TABlEROS DEL ÍNDICE
        public DataSet Devuelve_TOP7_pendientes_moldes(string FILTRO)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql1 = "SELECT TOP (7) R.IdReparacionMolde AS PARTE,R.IDMoldes AS MOLDE,M.Descripcion AS DESCRIPCION,R.MotivoAveria AS AVERIA,R.FechaAveria AS FECHAVERIA,R.IdPrioridad AS PRIORIDAD,R.FechaInicioReparacion AS FECHINICIOREP,R.Terminado,R.Revisado,R.RevisadoNOK FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes] R left join [SMARTH_DB].[dbo].[AUX_Lista_Moldes] M on R.IDMoldes = M.ReferenciaMolde where R.Terminado <> 1 " + FILTRO+"  order by IdReparacionMolde DESC";
                SqlDataAdapter adapter = new SqlDataAdapter(sql1, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql1);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                mandar_mail(ex.Message);
                return null;
            }
        }

        public DataSet Devuelve_TOP7_pendientes_maquina(string FILTRO)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql1 = "SELECT TOP(8) M.IdMantenimiento AS PARTE, CASE WHEN M.IdEstadoReparacion = 0 THEN '' WHEN M.IdEstadoReparacion = 1 THEN 'Rep. iniciada' ELSE 'En curso' END AS IdEstadoReparacion, C.Maquina AS MAQUINA, M.MotivoAveria AS AVERIA, substring(convert(varchar, M.FechaAveria, 100), 1, 19) as FECHAVERIA, substring(convert(varchar, M.FechaInicioReparacion, 100), 1, 19) AS FECHINICIOREP, M.IdPrioridad, M.Terminado, M.Revisado, M.RevisadoNOK, P.Máquina AS PERIFERICO, '' AS AREPARAR, M.HorasEstimadasReparacion, R.TipoMantenimientoMolde as TIPOMANTENIMIENTO" +
                              " FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas] M" +
                              " LEFT JOIN [SMARTH_DB].[dbo].[AUX_Lista_Máquinas] C ON(M.IDMaquina = C.IdMaquina)" +
                              " LEFT JOIN [SMARTH_DB].[dbo].[AUX_Lista_MaquinasPeriferico] P ON(M.IDPeriferico = P.ID)" +                        
                              " LEFT JOIN [SMARTH_DB].[dbo].[MANTENIMIENTO_Aux_Opciones] R ON M.IdTipoMantenimiento = R.Id" +
                              " WHERE M.Terminado <> 1" + FILTRO+" order by IdMantenimiento desc";
                SqlDataAdapter adapter = new SqlDataAdapter(sql1, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql1);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                mandar_mail(ex.Message);
                return null;
            }
        }

        public DataSet Avisos_moldes()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT Count(IdReparacionMolde) as num_avisos_moldes FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes] where Terminado = 0";
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

        public DataSet Avisos_maquinas()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT Count(IdMantenimiento) as num_avisos_maquinas FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas] where Terminado = 0";
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


        //########################################## INFORME DE MOLDES ##################################################################\\
        public DataSet Devuelve_lista_moldes_numReparaciones(string WHERE)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;

                string sql = "SELECT distinct ReferenciaMolde,'M' + cast(ReferenciaMolde as varchar) as MOLJOIN, MOL.Descripcion, Cavidades, MOL.Ubicacion, UBI.Zona," +
                                " (SELECT Count(IDMoldes) FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes] WHERE IDMoldes = ReferenciaMolde) as NumReparaciones," +
                                " CASE WHEN MAN.MANO IS NULL THEN 'Sin mano' else CAST(MAN.MANO AS varchar) end as MANO,"+ 
                                " CASE WHEN MAN.UBICACION IS NULL THEN 'Sin ubicar' else CAST(MAN.UBICACION AS varchar) end as MANUBICACION" +
                                " FROM[SMARTH_DB].[dbo].[AUX_Lista_Moldes] MOL" +
                                " LEFT JOIN[SMARTH_DB].[dbo].[AUX_Lista_Manos_Robot] MAN ON MOL.Mano = MAN.MANO" +
                                " LEFT JOIN[SMARTH_DB].DBO.AUX_Lista_Moldes_Ubicaciones UBI ON MOL.Ubicacion = UBI.Ubicacion"+
                                " WHERE ReferenciaMolde > 30000000 " +WHERE+" order by NumReparaciones desc";

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

        
        public DataSet Leer_contador_mantenimiento_Molde(string WHERE)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT TRIM(R.C_ID) AS C_ID, T.C_LONG_DESCR as descripcion_tool, R.C_RESOURCECOUNTER, R.C_TOTALCOUNTER, R.C_THRESHOLDVALUE, R.C_TIMELASTUPDATE, C.C_LONG_DESCR, CAST((R.C_RESOURCECOUNTER/R.C_THRESHOLDVALUE)*100 AS INT) AS PORCENTAJE, CONCAT ((CAST (((R.C_RESOURCECOUNTER/R.C_THRESHOLDVALUE)*100) AS INT)), '%') as porc FROM PCMS.T_MAINTENANCECONFIGS_TOOL CT" +
                                    " LEFT JOIN PCMS.T_MAINTENANCERESOURCES R ON (CT.C_RESOURCE_ID=R.C_ID)" +
                                    " INNER JOIN PCMS.T_MAINTENANCECONFIGCOUNTERS C ON (R.C_MAINTCONFIG_ID= C.C_MAINTCONFIG_ID)" +
                                    " INNER JOIN PCMS.T_TOOLS T ON (R.C_ID=T.C_ID)" +
                                " WHERE CT.C_MAINTCONFIG_ID=R.C_MAINTCONFIG_ID AND R.C_RESOURCECOUNTER is not null"+WHERE+"" +
                                " order by PORCENTAJE DESC";
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataSet ds = new DataSet();
                query.Fill(ds);
                cnn_bms.Close();
                return ds;
            }
            catch (Exception ex)
            {
                cnn_bms.Close();
                mandar_mail(ex.Message);
                return null;
            }
        }

        public DataTable Devuelve_Molde_Tipos_Mantenimiento_Asignados()
        {
            try
            {
                cnn_bms.Open();
                /*
                string sql = " SELECT TRIM(R.C_ID) AS C_ID, 'M' || TRIM(R.C_ID) AS MOLJOIN, C.C_LONG_DESCR" +
                                 " FROM PCMS.T_MAINTENANCECONFIGS_TOOL CT" +
                                       " LEFT JOIN PCMS.T_MAINTENANCERESOURCES R ON(CT.C_RESOURCE_ID = R.C_ID)" +
                                       " INNER JOIN PCMS.T_MAINTENANCECONFIGCOUNTERS C ON(R.C_MAINTCONFIG_ID = C.C_MAINTCONFIG_ID)" +
                                  " WHERE CT.C_MAINTCONFIG_ID = R.C_MAINTCONFIG_ID AND R.C_RESOURCECOUNTER is not null";
                */
                string sql = "SELECT 'M' || TRIM(R.C_ID) AS MOLJOIN, C.C_LONG_DESCR" +
                             " FROM PCMS.T_MAINTENANCERESOURCES R" +
                                   " INNER JOIN PCMS.T_MAINTENANCECONFIGCOUNTERS C ON(R.C_MAINTCONFIG_ID = C.C_MAINTCONFIG_ID)" +
                              " WHERE R.C_ENABLED = 1  AND C.C_COUNTENABLED = 1 AND R.C_RESOURCE_TYPE = 2" +
                              " ORDER BY MOLJOIN ASC";
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataTable ds = new DataTable();
                query.Fill(ds);
                cnn_bms.Close();
                return ds;
            }
            catch (Exception ex)
            {
                cnn_bms.Close();
                mandar_mail(ex.Message);
                return null;
            }
        }
        public DataSet Devuelve_historico_Molde(string WHERE)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;

                string sql = "SELECT * FROM(SELECT IDMoldes,IdReparacionMolde,MotivoAveria,FechaAveria,Reparacion,SUM([ImporteEmpresa1]+([HorasRealesReparacion]*21.5)) AS ImporteEmpresa1, CASE WHEN (FechaFinalizacionReparacion IS NULL or FechaFinalizacionReparacion = '') THEN 'Pendiente' else FechaFinalizacionReparacion end as FechaFinalizacionReparacion " +
                             " FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes] " +
                             " WHERE IDMoldes > 1000 "+WHERE+ " GROUP BY IDMoldes,IdReparacionMolde,MotivoAveria,FechaAveria,Reparacion,FechaFinalizacionReparacion) REP" +
                             " LEFT JOIN SMARTH_DB.DBO.AUX_Lista_Moldes MOL ON REP.IDMoldes = MOL.ReferenciaMolde"+
                             " order by IdReparacionMolde desc";

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

        //########################################## INFORME DE MÁQUINAS ################################################################\\
        public DataSet Devuelve_lista_maquinas_numReparaciones(string WHERE)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;

                string sql = "SELECT distinct IdMaquinaCHAR, NumSerie, Maquina, IdMaquina," +
                             " (SELECT Count(IDMaquina) FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas]" +
                             " WHERE IDMaquina = AUX_Lista_Máquinas.IdMaquina) as NumReparaciones" +
                             " FROM SMARTH_DB.DBO.AUX_Lista_Máquinas" +
                             " Where IdMaquina > 0 "+WHERE+"";
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

        public DataSet Leer_contador_mantenimientomaquina(string WHERE)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT R.C_ID as MAQUINA, CONCAT(R.C_THRESHOLDVALUE, ' Horas') as THRESHOLD, CAST(R.C_RESOURCECOUNTER AS INT) AS CONTADOR, CAST (R.C_TOTALCOUNTER AS INT) AS HISTORICO, R.C_TIMELASTUPDATE, CAST((R.C_RESOURCECOUNTER / R.C_THRESHOLDVALUE) * 100 AS INT) AS PORCENTAJE, CONCAT ((CAST(((R.C_RESOURCECOUNTER / R.C_THRESHOLDVALUE) * 100) AS INT)), '%') as porc,  R.C_THRESHOLDVALUE, C.C_MAINTCONFIG_ID" +
                             " FROM PCMS.T_MAINTENANCERESOURCES R LEFT JOIN PCMS.T_MAINTENANCECONFIGCOUNTERS C ON R.C_MAINTCOUNTER_ID = C.C_MAINTCOUNTER_ID AND R.C_MAINTCONFIG_ID = C.C_MAINTCONFIG_ID WHERE R.C_RESOURCECOUNTER is not null AND R.C_RESOURCE_TYPE = 1 AND C_COUNTENABLED = 1"+WHERE+" order by PORCENTAJE DESC";
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataSet ds = new DataSet();
                query.Fill(ds);
                cnn_bms.Close();
                return ds;
            }
            catch (Exception ex)
            {
                cnn_bms.Close();
                mandar_mail(ex.Message);
                return null;
            }
        }

        public DataSet Devuelve_historico_maquinas(string WHERE)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql1 = "SELECT C.Maquina,C.IdMaquinaCHAR,M.IdMantenimiento,M.MotivoAveria,substring(convert(varchar, M.FechaAveria, 100),1,19) as Fecha,M.Reparacion, SUM(M.ImporteEmpresa1+(HorasRealesReparacion*21.5)) AS ImporteEmpresa1, CASE WHEN (FechaFinalizacionReparacion IS NULL or FechaFinalizacionReparacion = '') THEN 'Pendiente' else FechaFinalizacionReparacion end as FechaFinalizacionReparacion" +
                                " FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas] M" +
                                " INNER JOIN SMARTH_DB.DBO.AUX_Lista_Máquinas C ON(M.IDMaquina = C.IdMaquina)  WHERE C.Maquina <> '' "+ WHERE+ " GROUP BY Maquina, IdMaquinaCHAR, IdMantenimiento, MotivoAveria, substring(convert(varchar, M.FechaAveria, 100), 1, 19), Reparacion, FechaFinalizacionReparacion" +
                                " order by M.IdMantenimiento desc";
                SqlDataAdapter adapter = new SqlDataAdapter(sql1, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql1);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        public DataSet Devuelve_historico_maquinas_preventivo(string WHERE)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;

                string sql1 = "SELECT C.Maquina,C.IdMaquinaCHAR,M.IdMantenimiento,M.MotivoAveria,substring(convert(varchar, M.FechaAveria, 100),1,19) as Fecha,M.Reparacion, SUM(M.ImporteEmpresa1+(HorasRealesReparacion*21.5)) AS ImporteEmpresa1,  CASE WHEN (FechaFinalizacionReparacion IS NULL or FechaFinalizacionReparacion = '') THEN 'Pendiente' else FechaFinalizacionReparacion end as FechaFinalizacionReparacion" +
                               " FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas] M" +
                               " INNER JOIN [SMARTH_DB].[dbo].[AUX_Lista_Máquinas] C ON(M.IDMaquina = C.IdMaquina)  WHERE IdTipoMantenimiento = 4" + WHERE+ "  GROUP BY Maquina, IdMaquinaCHAR, IdMantenimiento, MotivoAveria, substring(convert(varchar, M.FechaAveria, 100), 1, 19), Reparacion,FechaFinalizacionReparacion order by M.IdMantenimiento desc";               
                SqlDataAdapter adapter = new SqlDataAdapter(sql1, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql1);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        //########################################## INFORME DE PERIFÉRICOS #############################################################\\
        public DataSet Devuelve_lista_perifericos_numReparaciones(string WHERE)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT P.[Id], [Máquina],P.Familia, CASE WHEN P.Familia = '1' THEN 'Pokayokes y útiles'  WHEN P.Familia = '2' THEN 'Atemperadores' WHEN P.Familia = '3' THEN 'Manos' ELSE 'OTROS' END AS Descripcion,[Vinculado], COUNT(R.IdMantenimiento) AS REPARACIONES"+
                                " FROM[SMARTH_DB].[dbo].[AUX_Lista_MaquinasPeriferico] P"+
                                " LEFT JOIN[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas] R ON P.Id = R.IDPeriferico"+
                                " WHERE P.ID > 0 "+ WHERE +" GROUP BY P.Id, Máquina, P.Familia, Vinculado ORDER BY P.Familia, Máquina";
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

        public DataSet Devuelve_historico_perifericos(string WHERE)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql1 = " SELECT [Id],[Máquina],[Vinculado],R.IdMantenimiento AS PARTE,R.MotivoAveria AS AVERIA,R.FechaAveria AS FECHA,R.Reparacion AS REPARACION,R.ImporteEmpresa2"+
                              " FROM[SMARTH_DB].[dbo].[AUX_Lista_MaquinasPeriferico] P LEFT JOIN[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas] R ON P.Id = R.IDPeriferico"+
                              " WHERE ID > 0 AND R.IdMantenimiento IS NOT NULL " + WHERE + " ORDER BY PARTE DESC";
                SqlDataAdapter adapter = new SqlDataAdapter(sql1, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql1);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return null;
            }
        }

        //########################################## PENDIENTES MÁQUINAS #################################################################\\

        public DataSet Devuelve_pendientes_maquina(string maquina, string periferico, string TipoMantenimiento, string PreventivoVisible)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql1 = "SELECT PARTE, MAQUINA, AVERIA, FECHAVERIA, FECHINICIOREP, TIPOMANTENIMIENTO, IdEstadoReparacion, IdPrioridad, Terminado, Revisado, RevisadoNOK, PERIFERICO, HorasEstimadasReparacion,  Case WHEN P1.Nombre = '-' THEN '' ELSE P1.Nombre END AS AREPARAR1,  Case WHEN P2.Nombre = '-' THEN '' ELSE P2.Nombre END AS AREPARAR2,  Case WHEN P3.Nombre = '-' THEN '' ELSE P3.Nombre END AS AREPARAR3, REPARACIONESTADONUM" +
                                " FROM(SELECT M.IdMantenimiento AS PARTE, C.Maquina AS MAQUINA, M.MotivoAveria AS AVERIA, substring(convert(varchar, M.FechaAveria, 100), 1, 19) as FECHAVERIA, substring(convert(varchar, M.FechaInicioReparacion, 100), 1, 19) AS FECHINICIOREP, M.IdPrioridad, M.Terminado, M.Revisado, M.RevisadoNOK, P.Máquina AS PERIFERICO, '' AS AREPARAR, M.HorasEstimadasReparacion, M.IdEstadoReparacion as REPARACIONESTADONUM, CASE WHEN M.IdEstadoReparacion = 0 THEN '' WHEN M.IdEstadoReparacion = 1 THEN 'Rep. iniciada' ELSE 'En curso' END AS IdEstadoReparacion, R.TipoMantenimientoMolde as TIPOMANTENIMIENTO, substring([AsignadoA], 1, charindex(' ',[AsignadoA]) - 1) as Asignado1, substring([AsignadoA],charindex(' ',[AsignadoA]) + 1,charindex(' ',[AsignadoA]) - 1) as Asignado2, reverse(substring(reverse([AsignadoA]), 0, charindex(' ', reverse([AsignadoA])))) as Asignado3" +
                                " FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas] M" +
                                    " LEFT JOIN[SMARTH_DB].[dbo].[AUX_Lista_Máquinas] C ON(M.IDMaquina = C.IdMaquina)" +
                                    " LEFT JOIN[SMARTH_DB].[dbo].[AUX_Lista_MaquinasPeriferico] P ON(M.IDPeriferico = P.ID)" +
                                   
                                    " LEFT JOIN [SMARTH_DB].[dbo].[MANTENIMIENTO_Aux_Opciones] R ON M.IdTipoMantenimiento = R.Id" +
                                " WHERE M.Terminado <> 1" + maquina + "" + periferico + "" + TipoMantenimiento + "" + PreventivoVisible + ") CUT" +
                                    " LEFT JOIN SMARTH_DB.DBO.AUX_Personal_Mandos P1 on CUT.Asignado1 = P1.Id" +
                                    " LEFT JOIN SMARTH_DB.DBO.AUX_Personal_Mandos P2 on CUT.Asignado2 = P2.Id" +
                                    " LEFT JOIN SMARTH_DB.DBO.AUX_Personal_Mandos P3 on CUT.Asignado3 = P3.Id" +
                                " order by REPARACIONESTADONUM desc, PARTE desc";
                SqlDataAdapter adapter = new SqlDataAdapter(sql1, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql1);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                mandar_mail(ex.Message);
                return null;
            }
        }

        public DataSet Devuelve_pendientes_validarmaquina(string maquina, string periferico, string TipoMantenimiento, string PreventivoVisible, string AbiertoPor)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql1 = "SELECT M.IdMantenimiento AS PARTE, C.Maquina AS MAQUINA, M.MotivoAveria AS AVERIA, substring(convert(varchar, M.FechaAveria, 100), 1, 19) as FECHAVERIA, substring(convert(varchar, M.FechaInicioReparacion, 100), 1, 19) AS FECHINICIOREP, M.IdPrioridad, M.Terminado, M.Revisado, M.RevisadoNOK, P.Máquina AS PERIFERICO, OP.Nombre AS AREPARAR, M.HorasEstimadasReparacion, R.TipoMantenimientoMolde as TIPOMANTENIMIENTO" +
                               " FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas] M" +
                               " LEFT JOIN[SMARTH_DB].[dbo].[AUX_Lista_Máquinas] C ON(M.IDMaquina = C.IdMaquina)" +
                               " LEFT JOIN[SMARTH_DB].[dbo].[AUX_Lista_MaquinasPeriferico] P ON(M.IDPeriferico = P.ID)" +
                               " LEFT JOIN[SMARTH_DB].[dbo].[AUX_Personal_Mandos] OP ON IdEncargado = OP.Id" +
                               " LEFT JOIN[SMARTH_DB].[dbo].[MANTENIMIENTO_Aux_Opciones] R ON M.IdTipoMantenimiento = R.Id" +
                               " WHERE M.Revisado <> 1 and M.Terminado <> 0 " + maquina + "" + periferico + "" + TipoMantenimiento + "" + PreventivoVisible + "" + AbiertoPor + " order by IdMantenimiento desc";
                SqlDataAdapter adapter = new SqlDataAdapter(sql1, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql1);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                mandar_mail(ex.Message);
                return null;
            }
        }

        public bool Modificar_Asignacion_Reparacion_Maquina(int parte, string fechareparacion, int responsable, string horas)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "UPDATE [SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas] SET FechaInicioReparacion = '" + fechareparacion + "', HorasEstimadasReparacion = " + horas.ToString().Replace(',', '.') + ", IdOperario = " + responsable + "" +
                              " WHERE IdMantenimiento = " + parte;
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

        //########################################## PENDIENTES MOLDES #################################################################\\

        public DataSet Devuelve_pendientes_Moldes(string molde, string TipoMantenimiento, string PreventivoVisible)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                
                string sql1 = "SELECT R.IdReparacionMolde AS PARTE,R.IDMoldes AS MOLDE,M.Descripcion AS DESCRIPCION,R.MotivoAveria AS AVERIA, T.TipoMantenimientoMolde as TIPOMANTENIMIENTO, substring(convert(varchar, R.FechaAveria, 100), 1, 19) as FECHAVERIA, substring(convert(varchar, R.FechaInicioReparacion, 100), 1, 19) AS FECHINICIOREP, R.HorasEstimadasReparacion," +
                              " R.IdPrioridad AS PRIORIDAD,R.Terminado,R.Revisado,R.RevisadoNOK, CASE WHEN FECHAPREV IS NULL THEN 'Sin planificar' else substring(convert(varchar,FECHAPREV, 100), 1, 10) end as FECHAPREV " +
                              "FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes] R " +
                                    " LEFT JOIN [SMARTH_DB].[dbo].[AUX_Lista_Moldes] M on R.IDMoldes = M.ReferenciaMolde" +
                               
                                    " LEFT JOIN [SMARTH_DB].[dbo].[MANTENIMIENTO_Aux_Opciones] T ON R.IdTipoRevision = T.Id" +
                                    " LEFT JOIN(SELECT MOLDE AS MOLDEPREV, CASE WHEN MIN(FECHA) = '01/01/2000 0:0:00' THEN 'En máquina' ELSE MIN(FECHA) END AS FECHAPREV"+
                                            " FROM(SELECT DISTINCT[MOLDE], '01/01/2000 0:0:00' AS FECHA FROM[SMARTH_DB].[dbo].[AUX_Planificacion_Prioridades] WHERE SEQNR = 0 AND MOLDE <> '' UNION" +
                                            " SELECT[NEXT_MOLDE] AS MOLDE, MIN([FINCALCULADO]) AS FECHA  FROM[SMARTH_DB].[dbo].[AUX_Planificacion_Prevision]  GROUP BY NEXT_MOLDE) A GROUP BY MOLDE)PRV ON R.IDMoldes = PRV.MOLDEPREV" +
                              " where R.Terminado < 1 " + molde + "" + TipoMantenimiento + "" + PreventivoVisible + " order by IdReparacionMolde desc ";



                SqlDataAdapter adapter = new SqlDataAdapter(sql1, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql1);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                mandar_mail(ex.Message);
                return null;
            }
        }

        public DataSet Devuelve_pendientes_validar_Moldes(string molde, string TipoMantenimiento, string PreventivoVisible, string AbiertoPor)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql1 = "SELECT R.IdReparacionMolde AS PARTE,R.IDMoldes AS MOLDE,M.Descripcion AS DESCRIPCION,R.MotivoAveria AS AVERIA, T.TipoMantenimientoMolde  as TIPOMANTENIMIENTO, substring(convert(varchar, R.FechaAveria, 100), 1, 19) as FECHAVERIA, substring(convert(varchar, R.FechaInicioReparacion, 100), 1, 19) AS FECHINICIOREP, R.HorasEstimadasReparacion, op.Nombre as AREPARAR, R.IdPrioridad AS PRIORIDAD,R.Terminado,R.Revisado,R.RevisadoNOK" +
                              " FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes] R" +
                              " LEFT JOIN[SMARTH_DB].[dbo].[AUX_Lista_Moldes] M on R.IDMoldes = M.ReferenciaMolde" +
                              " LEFT JOIN[SMARTH_DB].[dbo].[AUX_Personal_Mandos] OP ON IdEncargado = OP.Id" +
                              " LEFT JOIN[SMARTH_DB].[dbo].[MANTENIMIENTO_Aux_Opciones] T ON R.IdTipoRevision = T.Id" +
                              " where R.Revisado <> 1 and R.Terminado <> 0 " + molde + "" + TipoMantenimiento + "" + PreventivoVisible + "" + AbiertoPor + "  order by IdReparacionMolde desc";
                SqlDataAdapter adapter = new SqlDataAdapter(sql1, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql1);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                mandar_mail(ex.Message);
                return null;
            }
        }

        //########################################## GESTION PREVENTIVOS #################################################################\\

        public bool Actualizar_Preguntas_PREVMAQ(string idMantenimiento, string ArrayPreguntas)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "UPDATE[SMARTH_DB].[dbo].[MANTENIMIENTO_Prev_Maquina_TiposXPreguntas] SET ArrayPreguntas = '" + ArrayPreguntas + "' WHERE IdMantenimiento = " + idMantenimiento + "";
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

        public DataSet Devuelve_Listado_TiposMantenimiento()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT TOP (1000) [IdMantenimiento],[IdTipoMantenimiento],[Descripcion],[TipoFrecuencia],[Frecuencia],[ArrayPreguntas] FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Prev_Maquina_TiposXPreguntas] order by Descripcion, IdTipoMantenimiento";
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

        public DataSet Devuelve_Listado_Preguntas_PREVMAQ()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT [IdCuestionario], Accion + ' - ' + Pregunta as Accion FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Prev_Maquina_ListaPreguntas] ORDER BY Accion, Pregunta";
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

        public DataSet Devuelve_Tipo_Mantenimiento_Seleccionado_PREVMAQ(string tipo)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT TOP (1000) [IdMantenimiento],[IdTipoMantenimiento],[Descripcion],[TipoFrecuencia],'('+CAST([Frecuencia] AS varchar) +' horas)' as Frecuencia,[ArrayPreguntas] FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Prev_Maquina_TiposXPreguntas]  WHERE TipoFrecuencia = '" + tipo + "'";
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

        public DataSet Devuelve_Listado_MAQ_TIPO_PREVMAQ(string tipo)
        {
            try
            {
                cnn_bms.Open();
                //string sql = "SELECT R.C_ID, R.C_RESOURCECOUNTER, R.C_TOTALCOUNTER, CONCAT (R.C_THRESHOLDVALUE, ' Horas')  as THRESHOLD, R.C_TIMELASTUPDATE, C.C_LONG_DESCR, CAST((R.C_RESOURCECOUNTER/R.C_THRESHOLDVALUE)*100 AS INT) AS PORCENTAJE, CONCAT ((CAST (((R.C_RESOURCECOUNTER/R.C_THRESHOLDVALUE)*100) AS INT)), '%') as porc FROM PCMS.T_MAINTENANCECONFIGS_MACH CT LEFT JOIN PCMS.T_MAINTENANCERESOURCES R ON (CT.C_RESOURCE_ID=R.C_ID) INNER JOIN PCMS.T_MAINTENANCECONFIGCOUNTERS C ON (R.C_MAINTCONFIG_ID= C.C_MAINTCONFIG_ID) WHERE CT.C_MAINTCONFIG_ID=R.C_MAINTCONFIG_ID AND R.C_RESOURCECOUNTER is not null AND R.C_RESOURCE_TYPE = 1 order by PORCENTAJE DESC";
                string sql = "SELECT C_LONG_DESCR FROM PCMS.T_MAINTENANCECONFIGS_MACH WHERE C_MAINTCONFIG_ID = " + tipo + " ORDER BY C_RESOURCE_ID";
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataSet ds = new DataSet();
                query.Fill(ds);
                cnn_bms.Close();
                return ds;
            }
            catch (Exception ex)
            {
                cnn_bms.Close();
                return null;
            }
        }
        //########################################## REPARACION DE MÁQUINAS ##################################################################\\
        public DataTable Devuelve_TOP300_Partes_Máquinas()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT TOP (300) cast([IdMantenimiento] as varchar) + ' | ' + cast (MAQ.Maquina as varchar) +' | ' + replace([MotivoAveria],CHAR(13)+CHAR(10),'') as PARTE"+ 
                                                                    " FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas] MAN"+
                                                                    " LEFT JOIN[SMARTH_DB].[dbo].[AUX_Lista_Máquinas] MAQ ON MAN.IDMaquina = MAQ.IdMaquina" +
                                                                    " order by[IdMantenimiento] desc", cnn_SMARTH);
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

        //########################################## REPARACION DE MOLDES ##################################################################\\
        public DataTable Devuelve_TOP300_Partes_Moldes()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT TOP (300) cast([IdReparacionMolde] as varchar) + ' | ' + cast ([IDMoldes] as varchar) +' | ' + replace([MotivoAveria],CHAR(13)+CHAR(10),'') as PARTE FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes] order by IdReparacionMolde desc", cnn_SMARTH);
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

        public DataSet Devuelve_Proxima_Produccion_Molde(string molde)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT C_SEQNR, C_ACTSTARTDATE, C_CALCSTARTDATE FROM T_JOBS WHERE C_SEQNR >= 0 AND C_PARINDEX = 1 AND C_TOOL_ID = '"+molde+"' ORDER BY C_SEQNR ASC";
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataSet ds = new DataSet();
                query.Fill(ds);
                cnn_bms.Close();
                return ds;
            }
            catch (Exception ex)
            {
                cnn_bms.Close();
                return null;
            }
        }
        /*
        public bool Añadir_operacion_molde(int parte, int idMolde, int reparado, int revisado, int revisadoNOK)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string reparado2 = "NOK";
                string revisado2 = "";
                if (reparado == 1)
                {
                    reparado2 = "OK";
                }
                if (revisado == 1)
                {
                    revisado2 = "OK";
                }
                if (revisadoNOK == 1)
                {
                    revisado2 = "NOK";
                }
                string sql = "INSERT INTO OperacionesMoldes (Parte, IdMolde, Fecha, Reparacion, Revision) VALUES (" + parte + "," + idMolde + ", CONVERT(VARCHAR(24),GETDATE(),113),'" + reparado2 + "','" + revisado2 + "')";
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
        */
        public bool Existe_parte(string parte)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT IdReparacionMolde FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes] where IdReparacionMolde = '" + parte + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                if (Convert.ToInt16(ds.Tables[0].Rows[0]["IdReparacionMolde"]) > 0)
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
                cnn_SMARTH.Close();
                return false;
            }
        }

        public DataSet TablaReparacionMoldes()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;

                string sql = "SELECT * " +
                             "FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes] " +
                             "order by idreparacionmolde asc";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);

                DataSet refs = new DataSet();
                adapter.Fill(refs, sql);
                cnn_SMARTH.Close();
                return refs;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                mandar_mail(ex.Message);
                return null;
            }
        }

        public DataSet Devuelve_lista_moldes()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;

                string sql = "SELECT  distinct ReferenciaMolde, Descripcion, Cavidades " +
                             "FROM [SMARTH_DB].[dbo].[AUX_Lista_Moldes] " +
                             "WHERE ReferenciaMolde > 30000000 order by ReferenciaMolde";

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

        public DataSet Devuelve_molde(int id)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;

                string sql = "SELECT ReferenciaMolde, Descripcion " +
                             "FROM [SMARTH_DB].[dbo].[AUX_Lista_Moldes] " +
                             "WHERE ReferenciaMolde = " + id;
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

        public int Devuelve_IDMolde(string molde)
        {
            try
            {
                string[] nombreMolde = molde.Split('-');
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;

                string sql = "SELECT ReferenciaMolde " +
                             "FROM [SMARTH_DB].[dbo].[AUX_Lista_Moldes] " +
                             "WHERE ReferenciaMolde = '" + nombreMolde[0] + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return Convert.ToInt32(ds.Tables[0].Rows[0]["ReferenciaMolde"].ToString());
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return 0;
            }
        }

        public string Devuelve_imagenes(string id, int num_img)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "";
                switch (num_img)
                {
                    case 1:
                        sql = "SELECT Imagen1 FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes] where IdReparacionMolde = '" + id + "'";
                        break;
                    case 2:
                        sql = "SELECT Imagen2 FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes] where IdReparacionMolde = '" + id + "'";
                        break;
                    case 3:
                        sql = "SELECT Imagen3 FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes] where IdReparacionMolde = '" + id + "'";
                        break;
                    default: break;
                }
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                switch (num_img)
                {
                    case 1:
                        return ds.Tables[0].Rows[0]["Imagen1"].ToString();
                        break;
                    case 2:
                        return ds.Tables[0].Rows[0]["Imagen2"].ToString();
                        break;
                    case 3:
                        return ds.Tables[0].Rows[0]["Imagen3"].ToString();
                        break;
                    default:
                        return null;
                        break;
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public DataSet Devuelve_parte_Molde(string parte)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql1 = "SELECT * " +
                             "FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes] " +
                             "WHERE IdReparacionMolde = '" + parte + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql1, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql1);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                mandar_mail(ex.Message);
                return null;
            }
        }

        public DataSet Devuelve_parte_Maquina(string parte)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql1 = "SELECT * " +
                             "FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas] " +
                             "WHERE [IdMantenimiento] = '" + parte + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql1, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql1);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                mandar_mail(ex.Message);
                return null;
            }
        }
        public DataSet TablaReparacionMoldes_pendientes()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;

                string sql1 = "SELECT * " +
                             "FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes] " +
                             "WHERE Terminado = 0 AND IdPrioridad <> 0 order by IdPrioridad asc, FechaAveria desc";

                string sql2 = "SELECT * " +
                             "FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes] " +
                             "WHERE Terminado = 0 AND IdPrioridad = 0";

                SqlDataAdapter adapter = new SqlDataAdapter(sql1, cnn_SMARTH);
                SqlDataAdapter adapter2 = new SqlDataAdapter(sql2, cnn_SMARTH);
                DataSet ds = new DataSet();
                DataSet ds2 = new DataSet();
                adapter.Fill(ds, sql1);
                adapter2.Fill(ds2, sql2);
                ds.Tables[0].Merge(ds.Tables[0]);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                mandar_mail(ex.Message);
                return null;
            }
        }

        public bool Modificar_parte(int parte, int molde, int trabajo, int encargado, int ubicacion, int prioridad,
                                    int turno, string averia, string fecha_apertura, int terminado, string imagen1, string imagen2, string imagen3,
                                    string fecha_inicio, string fecha_rep, int tipo_rep, string reparacion, string observaciones, int realizado_por,
                                    double horasestimadas, double horas, string fecha_revision, int revisado_por, string observaciones_revision, int revisado, int revisadoNOK, string coste, double costehoras, double costetotal, string AsignadoA, string ReparadoPor, int estadoreparacion, double horasrep1, double horasrep2, double horasrep3, int IdTipoPreventivo, string PreventivoAcciones, string PreventivoEstado)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "UPDATE [SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes] SET IDMoldes = " + molde + ", IdTipoRevision = " + trabajo + ", IdEncargado = " + encargado + ", IdUbicacion = " +
                              ubicacion + ", IdPrioridad = " + prioridad + ", IdTurno = " + turno + ", MotivoAveria = '" + averia + "', FechaAveria = '" + fecha_apertura + "', Terminado = " +
                              terminado + ", Imagen1 = '" + imagen1 + "', Imagen2 = '" + imagen2 + "', Imagen3 = '" + imagen3 + "'," +
                              " FechaInicioReparacion = '" + fecha_inicio + "', FechaFinalizacionReparacion = '" + fecha_rep + "', HorasEstimadasReparacion = " + horasestimadas.ToString().Replace(',', '.') + ", HorasRealesReparacion = " + horas.ToString().Replace(',', '.') + ", Reparacion = '" + reparacion +
                              "', IdRealizadoPor = " + realizado_por + ", Observaciones = '" + observaciones + "', IdTiposReparacion = " + tipo_rep + ", FechaRevision = '" + fecha_revision + "', " +
                              "RevisadoPor = " + revisado_por + ", ObservacionesRevision = '" + observaciones_revision + "', Revisado = " + revisado + ", RevisadoNOK = " + revisadoNOK + ", ImporteEmpresa1 = " + coste.ToString().Replace(',', '.') + ", ImporteEmpresa2 = " + costehoras.ToString().Replace(',', '.') + " " +
                               ", AsignadoA = '" + AsignadoA + "', ReparadoPorOP = '" + ReparadoPor + "', IdEstadoReparacion = " + estadoreparacion + ", HorasRealesRepOP1=" + horasrep1.ToString().Replace(',', '.') + ", HorasRealesRepOP2=" + horasrep2.ToString().Replace(',', '.') + ", HorasRealesRepOP3=" + horasrep3.ToString().Replace(',', '.') + ", ImporteEmpreza3 = " + costetotal.ToString().Replace(',', '.') + ", [IDTipoMantPreventivo] = " + IdTipoPreventivo + ", [PreventivoAcciones] = '" + PreventivoAcciones + "',[PreventivoEstado] = '" + PreventivoEstado + "'" +
                              " WHERE IdReparacionMolde = " + parte;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception ex)
            {
                
                cnn_SMARTH.Close();
                mandar_mail2(ex.Message + ex.StackTrace, "Error en la aplicación de mantenimiento");
                return false;
            }
        }

        
        //PREVENTIVOS MOLDE Y MÁQUINA
        public int Devuelve_IDMantenimiento_BMSMOLDE(string nombremaq)
        {
            try
            {
                cnn_bms.Open();
                //string sql = "SELECT R.C_ID, R.C_RESOURCECOUNTER, R.C_TOTALCOUNTER, CONCAT (R.C_THRESHOLDVALUE, ' Horas')  as THRESHOLD, R.C_TIMELASTUPDATE, C.C_LONG_DESCR, CAST((R.C_RESOURCECOUNTER/R.C_THRESHOLDVALUE)*100 AS INT) AS PORCENTAJE, CONCAT ((CAST (((R.C_RESOURCECOUNTER/R.C_THRESHOLDVALUE)*100) AS INT)), '%') as porc FROM PCMS.T_MAINTENANCECONFIGS_MACH CT LEFT JOIN PCMS.T_MAINTENANCERESOURCES R ON (CT.C_RESOURCE_ID=R.C_ID) INNER JOIN PCMS.T_MAINTENANCECONFIGCOUNTERS C ON (R.C_MAINTCONFIG_ID= C.C_MAINTCONFIG_ID) WHERE CT.C_MAINTCONFIG_ID=R.C_MAINTCONFIG_ID AND R.C_RESOURCECOUNTER is not null AND R.C_RESOURCE_TYPE = 1 order by PORCENTAJE DESC";
                string sql = "SELECT MAX(C_MAINTCONFIG_ID) AS ID_MAINTCONFIG FROM PCMS.T_MAINTENANCECONFIGS_TOOL WHERE C_RESOURCE_ID = '" + nombremaq + "'";
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataSet ds = new DataSet();
                query.Fill(ds);
                cnn_bms.Close();
                return Convert.ToInt32(ds.Tables[0].Rows[0]["ID_MAINTCONFIG"].ToString());
            }
            catch (Exception ex)
            {
                cnn_bms.Close();
                return 0;
            }
        }

        public DataSet Devuelve_Listado_PreventivosXMolde(int IDPreventivo)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                //string sql = "SELECT [TipoFrecuencia] FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Prev_Molde_TiposXPreguntas]  WHERE[IdTipoMantenimiento] = '" + IDPreventivo + "'  and TipoFrecuencia <> ''";
                string sql = "SELECT [TipoFrecuencia] FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Prev_Molde_TiposXPreguntas]  WHERE[IdTipoMantenimiento] IS NOT NULL  and TipoFrecuencia <> ''";

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

        public DataSet Devuelve_Tipo_Mantenimiento_Seleccionado_PREVMolde(string tipo)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT TOP (1000) [IdMantenimiento],[IdTipoMantenimiento],[Descripcion],[TipoFrecuencia],'('+CAST([Frecuencia] AS varchar) +' horas)' as Frecuencia,[ArrayPreguntas] FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Prev_Molde_TiposXPreguntas]  WHERE TipoFrecuencia = '" + tipo + "'";
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

        public string Devuelve_Tipo_Mantenimiento_Seleccionado_PREVMOLDE_DS(string IdMantenimiento)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT [TipoFrecuencia] FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Prev_Molde_TiposXPreguntas]  WHERE [IdMantenimiento] = '" + IdMantenimiento + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return ds.Tables[0].Rows[0]["TipoFrecuencia"].ToString(); ;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return "";
            }
        }


        //////////////////////////////////////////////////////////ANTIGUAS REVISAR Y ELIMINAR\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        //########################################## MOLDES PARA TALLER ##################################################################\\

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
                mandar_mail(ex.Message);
                return null;
            }
        }

        //######### PREVENTIVOS #########\\

        public int Devuelve_IDMantenimiento_BMS(string nombremaq)
        {
            try
            {
                cnn_bms.Open();
                //string sql = "SELECT R.C_ID, R.C_RESOURCECOUNTER, R.C_TOTALCOUNTER, CONCAT (R.C_THRESHOLDVALUE, ' Horas')  as THRESHOLD, R.C_TIMELASTUPDATE, C.C_LONG_DESCR, CAST((R.C_RESOURCECOUNTER/R.C_THRESHOLDVALUE)*100 AS INT) AS PORCENTAJE, CONCAT ((CAST (((R.C_RESOURCECOUNTER/R.C_THRESHOLDVALUE)*100) AS INT)), '%') as porc FROM PCMS.T_MAINTENANCECONFIGS_MACH CT LEFT JOIN PCMS.T_MAINTENANCERESOURCES R ON (CT.C_RESOURCE_ID=R.C_ID) INNER JOIN PCMS.T_MAINTENANCECONFIGCOUNTERS C ON (R.C_MAINTCONFIG_ID= C.C_MAINTCONFIG_ID) WHERE CT.C_MAINTCONFIG_ID=R.C_MAINTCONFIG_ID AND R.C_RESOURCECOUNTER is not null AND R.C_RESOURCE_TYPE = 1 order by PORCENTAJE DESC";
                string sql = "SELECT MAX(C_MAINTCONFIG_ID) AS ID_MAINTCONFIG FROM PCMS.T_MAINTENANCECONFIGS_MACH WHERE C_RESOURCE_ID = '" + nombremaq + "'";
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataSet ds = new DataSet();
                query.Fill(ds);
                cnn_bms.Close();
                return Convert.ToInt32(ds.Tables[0].Rows[0]["ID_MAINTCONFIG"].ToString());
            }
            catch (Exception ex)
            {
                cnn_bms.Close();
                return 0;
            }
        }

        //PARTES DE MAQUINA
        public DataSet Devuelve_Listado_PreventivosXMaquina(int IDPreventivo)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT [TipoFrecuencia] FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Prev_Maquina_TiposXPreguntas]  WHERE[IdTipoMantenimiento] = '" + IDPreventivo + "'";
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

        public string Devuelve_Tipo_Mantenimiento_Seleccionado_PREVMAQ_DS(string IdMantenimiento)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT [TipoFrecuencia] FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Prev_Maquina_TiposXPreguntas]  WHERE [IdMantenimiento] = '" + IdMantenimiento + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return ds.Tables[0].Rows[0]["TipoFrecuencia"].ToString(); ;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return "";
            }
        }

        public int Devuelve_ID_Tipo_Mantenimiento_Seleccionado_PREVMAQ_DS(string TipoFrecuencia)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT [IdMantenimiento] FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Prev_Maquina_TiposXPreguntas]  WHERE [TipoFrecuencia]  = '" + TipoFrecuencia + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return Convert.ToInt32(ds.Tables[0].Rows[0]["IdMantenimiento"].ToString()) ;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return 0;
            }
        }

        public int Devuelve_Frecuencia_Tipo_Mantenimiento_MAQ(string TipoMant)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT[Frecuencia]  FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Prev_Maquina_TiposXPreguntas] WHERE TipoFrecuencia = '"+ TipoMant + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return Convert.ToInt32(ds.Tables[0].Rows[0]["Frecuencia"].ToString());
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return 0;
            }
        }


        //PARTES MOLDES


        public int Devuelve_Frecuencia_Tipo_Mantenimiento_MOLDE(string TipoMant)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT[Frecuencia]  FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Prev_Molde_TiposXPreguntas] WHERE TipoFrecuencia = '" + TipoMant + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return Convert.ToInt32(ds.Tables[0].Rows[0]["Frecuencia"].ToString());
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return 0;
            }
        }

        public int Devuelve_ID_Tipo_Mantenimiento_Seleccionado_PREVMOLDE_DS(string TipoFrecuencia)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT [IdMantenimiento] FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Prev_Molde_TiposXPreguntas]  WHERE [TipoFrecuencia]  = '" + TipoFrecuencia + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return Convert.ToInt32(ds.Tables[0].Rows[0]["IdMantenimiento"].ToString());
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return 0;
            }
        }
        //######### REPARACION DE MOLDES #########

        public int Devuelve_IDtipo_reparacion(string reparacion)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;

                string sql = "SELECT Id FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Aux_Opciones] WHERE TipoReparación = '" + reparacion + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return Convert.ToInt16(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return 0;
            }
        }

        
        public int Devuelve_IDUbicacion(string ubicacion)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;

                string sql = "SELECT Id FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Aux_Opciones] WHERE UbicacionMolde = '" + ubicacion + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return Convert.ToInt16(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                else
                {
                    return 1;
                }
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return 0;
            }
        }

        
        public int max_idParte()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT max(IdReparacionMolde) as max FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes]";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return Convert.ToInt16(ds.Tables[0].Rows[0]["max"].ToString());
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return 0;
            }
        }

        public bool insertar_parte(int parte, int molde, int trabajo, int encargado, int ubicacion, int prioridad, 
                                   int turno, string averia, string fecha_apertura, int terminado, string imagen1, string imagen2, string imagen3,
                                   string fecha_inicio, string fecha_rep, int tipo_rep, string reparacion, string observaciones, int realizado_por, double horasestimadas,
                                   double horas, string fecha_revision, int revisado_por, string observaciones_revision, int revisado, int revisadoNOK, int automatico, double coste, double costehora, double costetotal, string AsignadoA, string ReparadoPor, int estadoreparacion, double horasrep1, double horasrep2, double horasrep3, int IdTipoPreventivo, string PreventivoAcciones, string PreventivoEstado)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes] (IdReparacionMolde, IDMoldes, IdTipoRevision, " + 
                             "IdEncargado, IdUbicacion, IdPrioridad, IdTurno, MotivoAveria, Terminado, FechaAveria, ComprasExternas, " + 
                             " ReparacionExterna, ReparacionInterna, Mantenimiento, Imagen1, Imagen2, Imagen3, " +
                             " FechaInicioReparacion, FechaFinalizacionReparacion, HorasEstimadasReparacion, HorasRealesReparacion, Reparacion, IdRealizadoPor, Observaciones, IdTiposReparacion, FechaRevision, RevisadoPor, ObservacionesRevision, Revisado, RevisadoNOK, Automatico, ImporteEmpresa1, ImporteEmpresa2, ImporteEmpreza3,AsignadoA,ReparadoPorOP,IdEstadoReparacion,HorasRealesRepOP1,HorasRealesRepOP2,HorasRealesRepOP3,IDTipoMantPreventivo,PreventivoAcciones,PreventivoEstado) VALUES " + 
                             "(" + parte + "," + molde + "," + trabajo + "," + encargado + "," + ubicacion + "," + prioridad + "," + 
                                turno + ",'" + averia + "'," + terminado + ",'" + fecha_apertura + "',0,0,0,0,'" + imagen1 + "','" + imagen2 + "','" + imagen3 + "'," +
                               "'" + fecha_inicio + "','" + fecha_rep + "'," + horasestimadas.ToString().Replace(',', '.') + ", " + horas.ToString().Replace(',', '.') + ",'" + reparacion + "'," + realizado_por + ",'" + observaciones + "'," + tipo_rep + ",'" + fecha_revision + "'," + revisado_por + ",'" + observaciones_revision + "'," + revisado + "," + revisadoNOK + "," + automatico + "," + coste.ToString().Replace(',', '.') + ","+costehora.ToString().Replace(',', '.') + ","+costetotal.ToString().Replace(',', '.') + ",'"+AsignadoA+"','"+ReparadoPor+"',"+estadoreparacion+","+horasrep1.ToString().Replace(',', '.') + ","+horasrep2.ToString().Replace(',', '.') + ","+horasrep3.ToString().Replace(',', '.') + ","+IdTipoPreventivo+",'"+PreventivoAcciones+"','"+PreventivoEstado+"')";
                cmd.CommandText = sql;                
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception ex)
            {
               
                cnn_SMARTH.Close();
                mandar_mail2(ex.Message + ex.StackTrace, "Error en la aplicación de mantenimiento");
                return false;
            }
        }

        

        // Comprobación de login
       
        //public string devuelve_fecha(string id)
        //{
        //    try
        //    {
        //        cnn_SMARTH.Open();
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Connection = cnn_SMARTH;
        //        string sql = "SELECT FechaAveria FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes] where IdReparacionMolde = '" + id + "'";
        //        SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
        //        DataSet ds = new DataSet();
        //        adapter.Fill(ds, sql);
        //        cnn_SMARTH.Close();
        //        return ds.Tables[0].Rows[0]["FechaAveria"].ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        return "";
        //    }
        //}

        //public string devuelve_fecha_inicioReparacion(string id)
        //{
        //    try
        //    {
        //        cnn_SMARTH.Open();
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Connection = cnn_SMARTH;
        //        string sql = "SELECT FechaInicioReparacion FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes] where IdReparacionMolde = '" + id + "'";
        //        SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
        //        DataSet ds = new DataSet();
        //        adapter.Fill(ds, sql);
        //        cnn_SMARTH.Close();
        //        return ds.Tables[0].Rows[0]["FechaInicioReparacion"].ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        return "";
        //    }
        //}

        //public string devuelve_fecha_finalReparacion(string id)
        //{
        //    try
        //    {
        //        cnn_SMARTH.Open();
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Connection = cnn_SMARTH;
        //        string sql = "SELECT FechaFinalizacionReparacion FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes] where IdReparacionMolde = '" + id + "'";
        //        SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
        //        DataSet ds = new DataSet();
        //        adapter.Fill(ds, sql);
        //        cnn_SMARTH.Close();
        //        return ds.Tables[0].Rows[0]["FechaFinalizacionReparacion"].ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        return "";
        //    }
        //}

       

        //##########################################################################
        //######################PARTES PREVENTIVOS AUTOMÁTICOS######################

        //AUXILIARES
        public string devuelve_ArrayPregunta(int IdMantenimiento)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;

                string sql = "SELECT[ArrayPreguntas] FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Prev_Maquina_TiposXPreguntas] WHERE IdMantenimiento = " + IdMantenimiento + "";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return ds.Tables[0].Rows[0]["ArrayPreguntas"].ToString();
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return "";
            }
        }

        public string devuelve_ArrayPreguntaMolde(int IdMantenimiento)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;

                string sql = "SELECT[ArrayPreguntas] FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Prev_Molde_TiposXPreguntas] WHERE IdMantenimiento = " + IdMantenimiento + "";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return ds.Tables[0].Rows[0]["ArrayPreguntas"].ToString();
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return "";
            }
        }



        //#####################################################################################
        //######### REPARACION DE MAQUINAS #########
        public DataSet Devuelve_lista_maquinas()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;

                string sql = "SELECT  distinct IdMaquina, NumSerie, Maquina, IdMaquinaCHAR, Observaciones " +
                             " FROM [SMARTH_DB].[dbo].[AUX_Lista_Máquinas] " +
                              " ORDER BY Observaciones ASC";
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

        public DataSet Devuelve_maquina(int id)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;

                string sql = "SELECT * " +
                             "FROM [SMARTH_DB].[dbo].[AUX_Lista_Máquinas] " +
                              "WHERE IdMaquina = " + id;

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
       
        public string Devuelve_IDmaquinaCHAR(string maquina)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;

                string sql = "SELECT IdMaquinaCHAR " +
                             "FROM [SMARTH_DB].[dbo].[AUX_Lista_Máquinas] " +
                              "WHERE Maquina = '" + maquina + "'";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return ds.Tables[0].Rows[0]["IdMaquinaCHAR"].ToString();
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return "";
            }
        }

        public string Devuelve_IDmaquinaXCHAR(string IdMaquinaCHAR)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;

                string sql = "SELECT IdMaquina " +
                             "FROM [SMARTH_DB].[dbo].[AUX_Lista_Máquinas] " +
                              "WHERE IdMaquinaCHAR = '" + IdMaquinaCHAR + "'";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return ds.Tables[0].Rows[0]["IdMaquina"].ToString();
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return "";
            }
        }

        public DataSet Devuelve_lista_perifericos()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;

                string sql = "SELECT Id, Máquina " +
                             "FROM [SMARTH_DB].[dbo].[AUX_Lista_MaquinasPeriferico] " +
                              "order by Familia, Máquina asc";

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

        public string Devuelve_periferico(int id)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;

                string sql = "SELECT Máquina " +
                             "FROM [SMARTH_DB].[dbo].[AUX_Lista_MaquinasPeriferico] " +
                              "where Id = " + id;

                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                if (ds.Tables[0].Rows[0]["Máquina"].ToString() != "")
                {
                    return ds.Tables[0].Rows[0]["Máquina"].ToString();    
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

       
        public DataSet Devuelve_lista_instalaciones()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;

                string sql = "SELECT Id, Instalacion " +
                                " FROM [SMARTH_DB].[dbo].[AUX_Lista_Instalaciones_MANT] " +
                                " order by Id asc";

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

        public string Devuelve_Instalacion_Empresa(string id)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;

                string sql = "SELECT Instalacion " +
                             "FROM [SMARTH_DB].[dbo].[AUX_Lista_Instalaciones_MANT] " +
                             "WHERE Id = " + Convert.ToInt16(id);

                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return ds.Tables[0].Rows[0]["Instalacion"].ToString();
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return "";
            }
        }

        public int Devuelve_IDinstalacion_Empresa(string nombre)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;

                string sql = "SELECT Id " +
                             "FROM [SMARTH_DB].[dbo].[AUX_Lista_Instalaciones_MANT] " +
                             "WHERE Instalacion = '" + nombre + "'";

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


        //GESTION DE PARTES
        public bool existe_parte_maquina(string parte)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT IdMantenimiento FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas] where IdMantenimiento = '" + parte + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                if (Convert.ToInt16(ds.Tables[0].Rows[0]["IdMantenimiento"]) > 0)
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
                cnn_SMARTH.Close();
                return false;
            }
        }

        public DataSet TablaReparacionMaquinas()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;

                string sql = "SELECT * " +
                             "FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas] " +
                             "order by IdMantenimiento asc";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);

                DataSet refs = new DataSet();
                adapter.Fill(refs, sql);
                cnn_SMARTH.Close();
                return refs;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                mandar_mail(ex.Message);
                return null;
            }
        }


        public bool insertar_parte_maquina(int parte, int maquina, int periferico, int trabajo, int encargado, int prioridad, int instalacion, int reparar_por,
                                   int turno, string averia, string fecha_apertura, string fecha_prox, int terminado, string imagen1, string imagen2, string imagen3,
                                   string fecha_inicio, string fecha_rep, string reparacion, string observaciones, int realizado_por, double Horasprevistas, 
                                   double horas, string fecha_revision, int revisado_por, string observaciones_revision, int revisado, int revisadoNOK, double CosteRepuesto, double CosteHora, string AsignadoA, string ReparadoPor, int estadoreparacion, double horasrep1, double horasrep2, double horasrep3, double CosteTotales, int IdTipoPreventivo, string PreventivoAcciones, string PreventivoEstado )
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas] (IdMantenimiento, IDMaquina, IDPeriferico, IdTipoMantenimiento, " +
                             "IdEncargado, IdPrioridad, IDInstalacion, IdOperario, IdTurno, MotivoAveria, Terminado, FechaAveria, FechaProximaProduccion, ComprasExternas, " +
                             " Imagen1, Imagen2, Imagen3, " +
                             " FechaInicioReparacion, FechaFinalizacionReparacion, HorasEstimadasReparacion, HorasRealesReparacion, Reparacion, IdRealizadoPor, Observaciones, FechaRevision, RevisadoPor, ObservacionesRevision, Revisado, RevisadoNOK, ImporteEmpresa1, ImporteEmpresa2, AsignadoA, ReparadoPorOP, IdEstadoReparacion, HorasRealesRepOP1, HorasRealesRepOP2, HorasRealesRepOP3, ImporteEmpreza3, IDTipoMantPreventivo, PreventivoAcciones,PreventivoEstado) VALUES " +
                             "(" + parte + "," + maquina + "," + periferico + "," + trabajo + "," + encargado + "," + prioridad + "," + instalacion + "," + reparar_por + "," +
                                turno + ",'" + averia + "'," + terminado + ",'" + fecha_apertura + "','" + fecha_prox + "',0,'" + imagen1 + "','" + imagen2 + "','" + imagen3 + "'," +
                               "'" + fecha_inicio + "','" + fecha_rep + "'," + Horasprevistas.ToString().Replace(',', '.') + "," + horas.ToString().Replace(',', '.') + ",'" + reparacion + "'," + realizado_por + ",'" + observaciones + "','" + fecha_revision + "'," + revisado_por + ",'" + observaciones_revision + "'," + revisado + ","+revisadoNOK+","+CosteRepuesto.ToString().Replace(',', '.') + ","+CosteHora.ToString().Replace(',', '.') + ",'"+AsignadoA+"', '"+ReparadoPor+"',0,"+horasrep1.ToString().Replace(',', '.') + ","+horasrep2.ToString().Replace(',', '.') + ","+horasrep3.ToString().Replace(',', '.') + "," + CosteTotales.ToString().Replace(',', '.') + ","+IdTipoPreventivo+",'"+PreventivoAcciones+"','"+PreventivoEstado+"')";
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

        public bool modificar_parte_maquina(int parte, int maquina, int periferico, int trabajo, int encargado, int prioridad, int instalacion, int reparar_por,
                                    int turno, string averia, string fecha_apertura, string fecha_prox, int terminado, string imagen1, string imagen2, string imagen3,
                                    string fecha_inicio, string fecha_rep, string reparacion, string observaciones, int realizado_por, double Horasprevistas,
                                    double horas, string fecha_revision, int revisado_por, string observaciones_revision, int revisado, int revisadoNOK, double CosteRepuestos, double CosteHoras, string AsignadoA, string ReparadoPor, int estadoreparacion, double horasrep1, double horasrep2, double horasrep3, double CosteTotales, int IDTipoMantPreventivo, string PreventivoAcciones, string PreventivoEstado)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "UPDATE [SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas] SET IDMaquina = " + maquina + ", IDPeriferico = " + periferico + ", IdTipoMantenimiento = " + trabajo + ", IdEncargado = " + encargado + ", IdPrioridad = " + prioridad +
                              ", IDInstalacion = " + instalacion + ", IdOperario = " + reparar_por + ", IdTurno = " + turno + ", MotivoAveria = '" + averia + "', FechaAveria = '" + fecha_apertura + "', FechaProximaProduccion = '" + fecha_prox  + "', Terminado = " + 
                              terminado + ", Imagen1 = '" + imagen1 + "', Imagen2 = '" + imagen2 + "', Imagen3 = '" + imagen3 + "'," +
                              " FechaInicioReparacion = '" + fecha_inicio + "', FechaFinalizacionReparacion = '" + fecha_rep + "', HorasEstimadasReparacion = " + Horasprevistas.ToString().Replace(',', '.') + ", HorasRealesReparacion = " + horas.ToString().Replace(',', '.') + ", Reparacion = '" + reparacion +
                              "', IdRealizadoPor = " + realizado_por + ", Observaciones = '" + observaciones + "', FechaRevision = '" + fecha_revision + "', " +
                              "RevisadoPor = " + revisado_por + ", ObservacionesRevision = '" + observaciones_revision + "', Revisado = " + revisado + ", RevisadoNOK = " + revisadoNOK + ", ImporteEmpresa1 = " + CosteRepuestos.ToString().Replace(',', '.') +", ImporteEmpresa2 = "+CosteHoras.ToString().Replace(',', '.') + ", AsignadoA = '"+AsignadoA+ "', ReparadoPorOP = '" + ReparadoPor + "', IdEstadoReparacion = "+estadoreparacion+ ", HorasRealesRepOP1="+horasrep1.ToString().Replace(',', '.') + ", HorasRealesRepOP2=" + horasrep2.ToString().Replace(',', '.') + ", HorasRealesRepOP3=" + horasrep3.ToString().Replace(',', '.') + ", ImporteEmpreza3 = " + CosteTotales.ToString().Replace(',', '.') + ", [IDTipoMantPreventivo] = "+ IDTipoMantPreventivo + ", [PreventivoAcciones] = '"+PreventivoAcciones+"',[PreventivoEstado] = '"+PreventivoEstado+"'" +
                              " WHERE IdMantenimiento = " + parte;
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

        
        public string devuelve_imagenes_maquinas(string id, int num_img)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "";
                switch (num_img)
                {
                    case 1:
                        sql = "SELECT Imagen1 FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas] where IdMantenimiento = '" + id + "'";
                        break;
                    case 2:
                        sql = "SELECT Imagen2 FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas] where IdMantenimiento = '" + id + "'";
                        break;
                    case 3:
                        sql = "SELECT Imagen3 FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas] where IdMantenimiento = '" + id + "'";
                        break;
                    default: break;
                }
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                switch (num_img)
                {
                    case 1:
                        return ds.Tables[0].Rows[0]["Imagen1"].ToString();
                        break;
                    case 2:
                        return ds.Tables[0].Rows[0]["Imagen2"].ToString();
                        break;
                    case 3:
                        return ds.Tables[0].Rows[0]["Imagen3"].ToString();
                        break;
                    default:
                        return null;
                        break;
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public int max_idParte_maquina()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT max(IdMantenimiento) as max FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas]";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return Convert.ToInt16(ds.Tables[0].Rows[0]["max"].ToString());
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return 0;
            }
        }

        public DataSet Reparaciones_pendientes_ORDPRIOD()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;

                string sql1 = "SELECT * " +
                             "FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas] " +
                             "WHERE Terminado = 0 AND IdPrioridad <> 0 order by IdPrioridad asc";

                string sql2 = "SELECT * " +
                             "FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas] " +
                             "WHERE Terminado = 0 AND IdPrioridad = 0";

                SqlDataAdapter adapter = new SqlDataAdapter(sql1, cnn_SMARTH);
                SqlDataAdapter adapter2 = new SqlDataAdapter(sql2, cnn_SMARTH);
                DataSet ds = new DataSet();
                DataSet ds2 = new DataSet();
                adapter.Fill(ds, sql1);
                adapter2.Fill(ds2, sql2);
                ds.Tables[0].Merge(ds2.Tables[0]);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                mandar_mail(ex.Message);
                return null;
            }
        }

        public DataSet devuelve_parte_maquina(string parte)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql1 = "SELECT * " +
                             "FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas] " +
                             "WHERE IdMantenimiento = '" + parte + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql1, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql1);
                cnn_SMARTH.Close();
                return ds;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                mandar_mail(ex.Message);
                return null;
            }
        }

        // añadir un nuevo registro de un usuario

        // añadir nueva operación en mantenimiento de moldes
       
        // añadir nueva operación en mantenimiento de máquinas
        public bool añadir_operacion_maquina(int parte, int idMolde, int reparado, int revisado)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string reparado2 = "NOK";
                string revisado2 = "NOK";
                if (reparado == 1)
                {
                    reparado2 = "OK";
                }
                if (revisado == 1)
                {
                    revisado2 = "OK";
                }
                string sql = "INSERT INTO OperacionesMaquinas (Parte, IdMaquina, Fecha, Reparacion, Revision) VALUES (" + parte + "," + idMolde + ", CONVERT(VARCHAR(24),GETDATE(),113),'" + reparado2 + "','" + revisado2 + "')";
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

        // devuelve la lista de correos de los usuarios.
      
        // devuelve la lista de correos de los usuarios.

        public void importacion()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql1 = "SELECT R.IdReparacionMolde as idReparacion, R.IDMoldes as idMolde, P.ReferenciaMolde as referencia FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes] as R JOIN [OFTECNICA].[dbo].TablaPrincipalMoldes as P ON (R.IDMoldes=P.IDMoldes) ";
                SqlDataAdapter adapter = new SqlDataAdapter(sql1, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql1);
                cnn_SMARTH.Close();

                foreach(DataRow row in ds.Tables[0].Rows)
                {
                    cnn_SMARTH.Open();
                    cmd = new SqlCommand();
                    cmd.Connection = cnn_SMARTH;
                    string sql = "Update [SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes] SET IDMoldes = " + row["referencia"].ToString() +" WHERE IdReparacionMolde = " + row["idReparacion"].ToString();
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    cnn_SMARTH.Close();
                }
                
 
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                mandar_mail(ex.Message);
            }
        }

        public void insertar_nuevos_partes()
        { 
             try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql1 = "SELECT IdReparacionMolde, IDMoldes, MotivoAveria, HorasEstimadasReparacion, HorasRealesReparacion, Reparacion, IdRealizadoPor, IdAutorizadoPor, Observaciones, IdPrioridad, Terminado, IdOperario, IdTurno, IdEncargado, IdTipoRevision, IdTiposReparacion, IdUbicacion, ComprasExternas, ReparacionExterna, ReparacionInterna, Mantenimiento FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes] where IdReparacionMolde > 1865 ";
                SqlDataAdapter adapter = new SqlDataAdapter(sql1, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql1);
                cnn_SMARTH.Close();

                foreach(DataRow row in ds.Tables[0].Rows)
                {
                    int id_parte = max_idParte() + 1;
                    cnn_SMARTH.Open();
                    cmd = new SqlCommand();
                    cmd.Connection = cnn_SMARTH;
                    string sql = "insert into [SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes] (IdReparacionMolde, IDMoldes, MotivoAveria, HorasEstimadasReparacion, HorasRealesReparacion, Reparacion, IdRealizadoPor, IdAutorizadoPor, Observaciones, IdPrioridad, Terminado, IdOperario, IdTurno, IdEncargado, IdTipoRevision, IdTiposReparacion, IdUbicacion, ComprasExternas, ReparacionExterna, ReparacionInterna, Mantenimiento) " +
                        "values ('" + row["IdReparacionMolde"].ToString() + "','" + row["IDMoldes"].ToString() + "','" + row["MotivoAveria"] + "','" + row["HorasEstimadasReparacion"].ToString() + "','" + row["HorasRealesReparacion"].ToString() + "','" + row["Reparacion"].ToString() + "','" + row["IdRealizadoPor"].ToString() + "','" + row["IdAutorizadoPor"].ToString() + "','" + row["Observaciones"].ToString() + "','" + row["IdPrioridad"].ToString() + "','" + row["Terminado"].ToString() + "','" + row["IdOperario"].ToString() + "','" + row["IdTurno"].ToString() + "','" + row["IdEncargado"].ToString() + "','" + row["IdTipoRevision"].ToString() + "','" + row["IdTiposReparacion"].ToString() + "','" + row["IdUbicacion"].ToString() + "',0,0,0,0)";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    cnn_SMARTH.Close();
                }
                
 
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                mandar_mail(ex.Message);
            }
        }

        // Insertar una accion pendiente en molde.
        public void insertar_accion_pendiente(int idMolde, string texto, int parte)
        {
            try
            {
                cnn_bms.Open();
                string sql = "update PCMS.T_TOOLS set C_REMARKS = '" + texto + "', C_STDNUMVALUES01 = '"+parte+"' WHERE C_ID = '" + idMolde + "'";
                OracleCommand insert = new OracleCommand(sql, cnn_bms);
                insert.ExecuteNonQuery();               
                cnn_bms.Close();
            }
            catch (Exception ex)
            {
            }
        }

        // Elimina una acción pendiente
        public void eliminar_accion_pendiente(int idMolde)
        {
            try
            {
                cnn_bms.Open();
                string sql = "update PCMS.T_TOOLS set C_REMARKS = '', C_STDNUMVALUES01 = 0 WHERE C_ID = '" + idMolde + "'";
                OracleCommand delete = new OracleCommand(sql, cnn_bms);
                delete.ExecuteNonQuery();
                cnn_bms.Close();
            }
            catch (Exception ex)
            {
            }
        }

     
        public void mandar_mail(string mensaje)
        {
            //Create MailMessage
            MailMessage email = new MailMessage();
            email.To.Add(new MailAddress("pedro@thermolympic.es"));
            email.From = new MailAddress("bms@thermolympic.es");
            email.Subject = "Error aplicación mantenimiento (" + DateTime.Now.ToString("dd/MMM/yyy hh:mm:ss") + ")";
            email.Body = mensaje;
            email.IsBodyHtml = true;
            email.Priority = MailPriority.Normal;

            //Definir objeto SmtpClient
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.thermolympic.es";
            smtp.Port = 25;
            smtp.EnableSsl = false;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("bms@thermolympic.es", "010477Bms");

            //Enviar email
            try
            {
                smtp.Send(email);
                email.Dispose();
            }
            catch (Exception ex)
            {
            }
        }


    }
}