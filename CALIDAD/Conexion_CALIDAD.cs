using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
//using System.Data.OracleClient;

namespace ThermoWeb.CALIDAD
{
    public class Conexion_CALIDAD
    {
        private readonly SqlConnection cnn_GP12 = new SqlConnection();
        private readonly OracleConnection cnn_bms = new OracleConnection();
        private readonly SqlConnection cnn_RPS = new SqlConnection();
        private readonly SqlConnection cnn_GP122 = new SqlConnection();
        private readonly SqlConnection cnn_NAV = new SqlConnection();

        public Conexion_CALIDAD()
        {
            cnn_GP12.ConnectionString = ConfigurationManager.ConnectionStrings["area_rechazo"].ToString();
            cnn_GP122.ConnectionString = ConfigurationManager.ConnectionStrings["area_rechazo"].ToString();
            cnn_bms.ConnectionString = ConfigurationManager.ConnectionStrings["BMS"].ToString();
            cnn_RPS.ConnectionString = ConfigurationManager.ConnectionStrings["RPS"].ToString();
            cnn_NAV.ConnectionString = ConfigurationManager.ConnectionStrings["NAV"].ToString();
        }
       

        //LISTA ALERTAS DE CALIDAD

        public DataSet Devuelve_Listado_NoConformidadesSMARTH(string año) //SMARTH
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = " SELECT *,CASE WHEN LogotipoSM IS NULL  AND TipoNoConformidad <> 1 THEN 'http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg' WHEN LogotipoSM IS NULL AND TipoNoConformidad = 1 THEN '../SMARTH_docs/CLIENTES/THERMOLYMPICICON.jpg' ELSE LogotipoSM END AS LOGOCLIENTE,CASE WHEN RepiteDefecto = 1 THEN '1' ELSE '0' END AS RepiteDefecto10, CASE WHEN RepiteReferencia = 1 THEN '1' ELSE '0' END AS RepiteReferencia10, CASE WHEN CheckD3 = 0 THEN 'N/A' ELSE SUBSTRING(D3,1,10) END AS CD3, CASE WHEN CheckD3 = 0 THEN 'N/A' ELSE SUBSTRING(D3CIERRE,1,10) END AS CD3CIERRE,CASE WHEN CheckD6 = 0 THEN 'N/A' ELSE SUBSTRING(D6,1,10) END AS CD6, CASE WHEN CheckD6 = 0 THEN 'N/A' ELSE SUBSTRING(D6CIERRE,1,10) END AS CD6CIERRE, CASE WHEN CheckD8 = 0 THEN 'N/A' ELSE SUBSTRING(D8,1,10) END AS CD8, CASE WHEN CheckD8 = 0 THEN 'N/A' ELSE SUBSTRING(D8CIERRE,1,10) END AS CD8CIERRE, C.Observaciones as NCObservaciones, CASE WHEN NC.TipoNoConformidad = 1 THEN 'A proveedor' WHEN NC.TipoNoConformidad = 2 THEN 'De cliente' WHEN NC.TipoNoConformidad = 3 THEN 'Interna' WHEN NC.TipoNoConformidad = 4 THEN 'Logística' END AS TipoTEXT, CASE WHEN NC.EscaladoNoConformidad = 1 THEN 'Q-Info' WHEN NC.EscaladoNoConformidad = 2 THEN 'Oficial' WHEN NC.EscaladoNoConformidad = 3 THEN 'Rechazada' END AS NCTEXT, ENC.Nombre AS ENCARGADOTEXT, CAL.Nombre AS CALIDADTEXT, ING.Nombre AS INGENIERIATEXT" +
                                " FROM[SMARTH_DB].[dbo].[NC_Alerta_de_calidad] NC" +
                                " LEFT JOIN[SMARTH_DB].[dbo].[AUX_TablaProductos] P ON NC.Referencia = P.Referencia" +
                                " LEFT JOIN[SMARTH_DB].[dbo].[DOCUMENTAL_Base_DocumentosXReferencias] R ON NC.Referencia = R.Referencia" +
                                " LEFT JOIN[SMARTH_DB].[dbo].[AUX_Personal_Mandos] ENC ON NC.PilotoProduccion = ENC.Id" +
                                " LEFT JOIN[SMARTH_DB].[dbo].[AUX_Personal_Mandos] CAL ON NC.PilotoCalidad = CAL.Id" +
                                " LEFT JOIN[SMARTH_DB].[dbo].[AUX_Personal_Mandos] ING ON NC.PilotoIngenieria = ING.Id" +
                                " LEFT JOIN[SMARTH_DB].[dbo].[NC_Estado_No_Conformidad] C ON NC.IdNoConformidad = C.IdNoConformidad" +
                                " LEFT JOIN SMARTH_DB.DBO.AUX_Lista_Clientes CLI ON P.Cliente = CLI.Cliente"+
                                " WHERE YEAR(NC.FechaOriginal) = " +año+ " AND NC.IdNoConformidad <> 23000  AND NC.IdNoConformidad <> 24000 order by nc.IdNoConformidad desc ";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_GP12.Close();
                return ds;
            }
            catch (Exception EX)
            {
                cnn_GP12.Close();
                return null;
            }
        }

        public DataSet Devuelve_Listado_NoConformidadesFiltradosSMARTH(string estado, string cliente, string piloto, string escalado, string tipoNC, string año, string sector, string producto, string molde)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = " SELECT *,CASE WHEN LogotipoSM IS NULL  AND TipoNoConformidad <> 1 THEN 'http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg' WHEN LogotipoSM IS NULL AND TipoNoConformidad = 1 THEN '../SMARTH_docs/CLIENTES/THERMOLYMPICICON.jpg' ELSE LogotipoSM END AS LOGOCLIENTE,CASE WHEN RepiteDefecto = 1 THEN '1' ELSE '0' END AS RepiteDefecto10, CASE WHEN RepiteReferencia = 1 THEN '1' ELSE '0' END AS RepiteReferencia10, CASE WHEN CheckD3 = 0 THEN 'N/A' ELSE SUBSTRING(D3,1,10) END AS CD3, CASE WHEN CheckD3 = 0 THEN 'N/A' ELSE SUBSTRING(D3CIERRE,1,10) END AS CD3CIERRE,CASE WHEN CheckD6 = 0 THEN 'N/A' ELSE SUBSTRING(D6,1,10) END AS CD6, CASE WHEN CheckD6 = 0 THEN 'N/A' ELSE SUBSTRING(D6CIERRE,1,10) END AS CD6CIERRE, CASE WHEN CheckD8 = 0 THEN 'N/A' ELSE SUBSTRING(D8,1,10) END AS CD8, CASE WHEN CheckD8 = 0 THEN 'N/A' ELSE SUBSTRING(D8CIERRE,1,10) END AS CD8CIERRE, C.Observaciones as NCObservaciones, CASE WHEN NC.TipoNoConformidad = 1 THEN 'A proveedor' WHEN NC.TipoNoConformidad = 2 THEN 'De cliente' WHEN NC.TipoNoConformidad = 3 THEN 'Interna' WHEN NC.TipoNoConformidad = 4 THEN 'Logística' END AS TipoTEXT, CASE WHEN NC.EscaladoNoConformidad = 1 THEN 'Q-Info' WHEN NC.EscaladoNoConformidad = 2 THEN 'Oficial' WHEN NC.EscaladoNoConformidad = 3 THEN 'Rechazada' END AS NCTEXT, ENC.Nombre AS ENCARGADOTEXT, CAL.Nombre AS CALIDADTEXT, ING.Nombre AS INGENIERIATEXT" +
                                " FROM[SMARTH_DB].[dbo].[NC_Alerta_de_calidad] NC" +
                                " LEFT JOIN[SMARTH_DB].[dbo].[AUX_TablaProductos] P ON NC.Referencia = P.Referencia" +
                                " LEFT JOIN[SMARTH_DB].[dbo].[DOCUMENTAL_Base_DocumentosXReferencias] R ON NC.Referencia = R.Referencia" +
                                " LEFT JOIN[SMARTH_DB].[dbo].[AUX_Personal_Mandos] ENC ON NC.PilotoProduccion = ENC.Id" +
                                " LEFT JOIN[SMARTH_DB].[dbo].[AUX_Personal_Mandos] CAL ON NC.PilotoCalidad = CAL.Id" +
                                " LEFT JOIN[SMARTH_DB].[dbo].[AUX_Personal_Mandos] ING ON NC.PilotoIngenieria = ING.Id" +
                                " LEFT JOIN[SMARTH_DB].[dbo].[NC_Estado_No_Conformidad] C ON NC.IdNoConformidad = C.IdNoConformidad" +
                                " LEFT JOIN SMARTH_DB.DBO.AUX_Lista_Clientes CLI ON P.Cliente = CLI.Cliente" +
                                " WHERE YEAR(NC.FechaOriginal) = " +año+ " AND NC.IdNoConformidad <> 23000  AND NC.IdNoConformidad <> 24000 " + estado + "" + cliente + "" + piloto + "" + escalado + "" + tipoNC + "" + sector + ""+producto+""+molde+"" +
                                " order by nc.IdNoConformidad desc ";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_GP12.Close();
                return ds;
            }
            catch (Exception EX)
            {
                cnn_GP12.Close();
                return null;
            }
        } //SMARTH

        public void elimina_alerta_NoConformidad(string NoConformidad)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "DELETE FROM [SMARTH_DB].[dbo].[NC_Estado_No_Conformidad] Where IdNoConformidad = '"+NoConformidad+"'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "DELETE FROM [SMARTH_DB].[dbo].[NC_OperariosVinculados] Where IdNoConformidad = '" + NoConformidad + "'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "DELETE FROM [SMARTH_DB].[dbo].[NC_Alerta_de_calidad] Where IdNoConformidad = '" + NoConformidad + "'";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                cnn_GP12.Close();

            }
            catch (Exception)
            {
                cnn_GP12.Close();
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

        public DataSet devuelve_lista_clientes()
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT DISTINCT RTRIM(P.Cliente) AS Cliente FROM[SMARTH_DB].[dbo].[NC_Alerta_de_calidad] C LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] P ON C.Referencia = P.Referencia WHERE YEAR(C.FechaOriginal) = '2021' ORDER BY Cliente ASC";
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

        public int Devuelve_IDlista_responsablesSMARTH(string responsable)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT [Id] FROM [SMARTH_DB].[dbo].[AUX_Personal_Mandos] WHERE [Nombre] = '" + responsable + "'";
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
        } //SMARTH


        //CONECTORES CALIDAD

        public DataTable Devuelve_Lineas_Contencion_NC(int NoConformidad, string FechaOri)
        {
            try
            {
                cnn_GP12.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT NC.[Id], NC.[IdNoConformidad],[Cantidad],[IdAlmacen],[NombreAlmacen],[FechaNC],LOT.Lotes, PER.Nombre, NC.[Notas],[PuntoLimpio],[FechaPuntoLimpio], NC.[Referencia], SUM(REV.PiezasRevisadas) AS PiezasRevisadas, SUM(REV.PiezasOK) AS PiezasOK, SUM(REV.PiezasNOK) AS PiezasNOK, SUM(REV.Retrabajadas) AS Retrabajadas" +
                                                                " FROM[SMARTH_DB].[dbo].[NC_HojaContencion] NC"+
                                                                " left join[SMARTH_DB].[dbo].[GP12_Historico_2021] REV ON NC.Referencia = REV.Referencia AND REV.FechaInicio >= NC.FechaNC"+
                                                                " left join SMARTH_DB.dbo.AUX_Personal_Mandos PER ON NC.IdResponsable = PER.Id"+
                                                                " left join (SELECT Referencia, Count(DISTINCT Nlote) AS Lotes FROM[SMARTH_DB].[dbo].[GP12_Historico_2021] WHERE FechaInicio >= '"+ FechaOri+"' Group by Referencia) LOT ON NC.Referencia = LOT.Referencia"+
                                                                " WHERE NC.IdNoConformidad = " + NoConformidad+""+
                                                                " GROUP BY NC.[Id], NC.[IdNoConformidad],[Cantidad],[IdAlmacen],[NombreAlmacen],[FechaNC],PER.Nombre, NC.[Notas],[PuntoLimpio],[FechaPuntoLimpio], NC.[Referencia], LOT.Lotes", cnn_GP12);
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

        public void Actualiza_Lineas_Contencion_NC(int id, int checkestado, string Notas)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "UPDATE [SMARTH_DB].[dbo].[NC_HojaContencion] SET[Notas] = '"+Notas+"',[PuntoLimpio] = '"+checkestado+ "',[FechaPuntoLimpio] = SYSDATETIME() where id = "+id+" ";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();

            }
            catch (Exception)
            {
                cnn_GP12.Close();
            }
        }

        public void Actualiza_Fecha_Inicial_Contencion(int NC, string fecha)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "UPDATE [SMARTH_DB].[dbo].[NC_HojaContencion] SET [FechaNC] = '"+fecha+"' where [IdNoConformidad] = " + NC + " ";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();

            }
            catch (Exception ex)
            {
                cnn_GP12.Close();
            }
        }

        public bool Insertar_EvidenciasNCBD(string NC, int tipoevidencia, string Fechadoc, string Fechasubida, string URL, string descripcion)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql2 = "INSERT INTO [SMARTH_DB].[dbo].[NC_Evidencias] ([NC],[TipoEvidencia],[FechaDoc],[FechaSubida],[URLDocumento],[Descripcion],[Eliminar]) VALUES" +
                                                           "(" + NC + "," + tipoevidencia + ",'" + Fechadoc + "','" + Fechasubida + "','" + URL + "','" + descripcion + "',0)";
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

        public bool Eliminar_EvidenciasNCBD(string id)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql2 = "UPDATE [SMARTH_DB].[dbo].[NC_Evidencias] SET Eliminar = 1 WHERE Id = "+id+"";
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

        public void Actualiza_D3D6D8_NoConformidades(int IdNoConformidad, string SQL)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "UPDATE [SMARTH_DB].[dbo].[NC_Estado_No_Conformidad] SET "+SQL+" WHERE [IdNoConformidad] = " + IdNoConformidad + "";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();

            }
            catch (Exception ex)
            {
                cnn_GP12.Close();
            }
        }

        public DataTable Devuelve_estado_documentoNC(int noconformidad)
        {
            try
            {
                cnn_GP12.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT [DOCPuntoCorte],[DOCD3],[DOCD6],[DOCD8] FROM[SMARTH_DB].[dbo].[NC_Estado_No_Conformidad] where IdNoConformidad = "+noconformidad+"", cnn_GP12);
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

        public DataTable Devuelve_evidencias_documentoNC(int noconformidad)
        {
            try
            {
                cnn_GP12.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT [Id],[NC], CASE WHEN[TipoEvidencia] = 0 THEN '-' WHEN[TipoEvidencia] = 1 THEN 'Punto de corte' WHEN[TipoEvidencia] = 2 THEN 'D3' WHEN[TipoEvidencia] = 3 THEN 'D6' WHEN[TipoEvidencia] = 4 THEN 'D8' WHEN[TipoEvidencia] = 5 THEN 'Otros' WHEN[TipoEvidencia] = 6 THEN 'P.Retrabajo' WHEN[TipoEvidencia] = 7 THEN 'Cargos' WHEN [TipoEvidencia] = 8 THEN 'Comunicaciones' END AS TipoEvidencia" +
                                                                " ,[FechaDoc],[FechaSubida],[URLDocumento],[Descripcion] FROM[SMARTH_DB].[dbo].[NC_Evidencias]"+
                                                                " WHERE Eliminar = 0 and NC = "+noconformidad+ " ORDER BY TipoEvidencia ASC, [FechaSubida] ASC", cnn_GP12);
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

        //bloque gp12
        public DataTable Devuelve_estadoProductosGP12(string producto)
        {
            try
            {
                cnn_GP12.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM[SMARTH_DB].[dbo].[GP12_ProductosEstados] where Referencia = '"+producto+"'", cnn_GP12);
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

        public void Actualiza_Estado_Producto_NCGP12(string producto, string NoConformidad, string EstadoAnterior, string FechaEstadoAnterior)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = " UPDATE [SMARTH_DB].[dbo].[GP12_ProductosEstados]" +
                              " SET [EstadoActual] = 1, [Fecharev] = SYSDATETIME(), [Fechaprevsalida] = DATEADD(day, 45, SYSDATETIME()),[EstadoAnterior] = '"+EstadoAnterior+"',[Fechaestanterior] = '"+FechaEstadoAnterior+"',[IdNoConformidad] = '"+NoConformidad+"'" +
                              " WHERE Referencia = '"+producto+"' ";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();

            }
            catch (Exception ex)
            {
                cnn_GP12.Close();
            }
        }

        public string Devuelve_RazonRevisionGP12(string razon)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT [Razon] FROM [SMARTH_DB].[dbo].[AUX_GP12_RazonRevision] where [Id] = " + razon;
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_GP12.Close();
                if (ds.Tables[0].Rows[0]["Razon"].ToString() != "")
                {
                    return ds.Tables[0].Rows[0]["Razon"].ToString();
                }
                else
                {
                    return "";
                }

            }
            catch (Exception)
            {
                cnn_GP12.Close();
                return "";
            }
        }

        public DataSet Devuelve_datos_NOCONFORMIDAD(string referencia)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT *,CASE WHEN RepiteDefecto = 1 THEN '1' ELSE '0' END AS RepiteDefecto10, CASE WHEN RepiteReferencia = 1 THEN '1' ELSE '0' END AS RepiteReferencia10" +
                                " FROM[SMARTH_DB].[dbo].[NC_Alerta_de_calidad] NC " +
                                " LEFT JOIN[SMARTH_DB].[dbo].[AUX_TablaProductos] P ON NC.Referencia = P.Referencia" +
                                " LEFT JOIN[SMARTH_DB].[dbo].[DOCUMENTAL_Base_DocumentosXReferencias] R ON NC.Referencia = R.Referencia" +
                                " LEFT JOIN [SMARTH_DB].[dbo].[NC_Estado_No_Conformidad] NCS ON NC.IdNoConformidad = NCS.IdNoConformidad"+
                                " WHERE NC.IdNoConformidad = '" + referencia+"'";
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
                string sql = "SELECT *,CASE WHEN RepiteDefecto = 1 THEN '1' ELSE '0' END AS RepiteDefecto10, CASE WHEN RepiteReferencia = 1 THEN '1' ELSE '0' END AS RepiteReferencia10 FROM[SMARTH_DB].[dbo].[NC_Alerta_de_calidad] NC LEFT JOIN[SMARTH_DB].[dbo].[AUX_TablaProductos] P ON NC.Referencia = P.Referencia LEFT JOIN[SMARTH_DB].[dbo].[DOCUMENTAL_Base_DocumentosXReferencias] R ON NC.Referencia = R.Referencia WHERE P.Molde = '" + MOLDE+"' ORDER BY NC.ID DESC";
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

        //SEGUIR AQUÍ
        public DataSet Devuelve_datos_producto_SMARTH(string PRODUCTO)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT [Referencia],[Molde],[Descripcion],[Sector], CASE WHEN CLI.LogotipoSM IS NULL THEN '..\\SMARTH_docs\\NOCONFORMIDADES\\sin_imagen.jpg' ELSE CLI.LogotipoSM END AS LogotipoSM" +
                               " FROM[SMARTH_DB].[dbo].[AUX_TablaProductos] A left join[SMARTH_DB].[dbo].[AUX_Lista_Clientes] CLI ON A.Cliente = CLI.Cliente" +
                               " WHERE Referencia = '"+PRODUCTO+"'";
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
        public DataSet Devuelve_stock_cuarentena(string referencia)
        {
            try
            {
                cnn_NAV.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_NAV;
               //OLD string sql = "SELECT [Item No_], sum([Item Ledger Entry Quantity]) AS CANTALM FROM[NAVDB].[dbo].[THERMO$Value Entry] WHERE[Item No_] = '" + referencia + "' group by[Item No_]";
                string sql = "SELECT [Location Code], ALM.Name, SUM(Quantity) AS CANTALM FROM NAVDB.DBO.[THERMO$Item Ledger Entry] STC" +
                                " LEFT JOIN[NAVDB].[dbo].[THERMO$Location] ALM ON STC.[Location Code] = ALM.CODE"+
                                " where[Item No_] = '"+referencia+"' and[Posting Date] <= SYSDATETIME()" +
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

        public DataTable Devuelve_Listado_AlmacenesNAV()
        {
            try
            {

                cnn_NAV.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM[NAVDB].[dbo].[THERMO$Location]", cnn_NAV);
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

        public string Devuelve_Codigo_Almacen(string almacen)
        {
            try
            {
                cnn_NAV.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_NAV;
                string sql = "SELECT [code] FROM[NAVDB].[dbo].[THERMO$Location] WHERE Name = '" + almacen + "'";   
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_NAV.Close();
                if (ds.Tables[0].Rows[0]["code"].ToString() != "")
                {
                    return ds.Tables[0].Rows[0]["code"].ToString();
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


       
        public bool Insertar_Cuarentena_NC(int referencia, string Codigo, string Almacen, int Cantidad, int NoConformidad, int responsable, string FechaNC) //SMARTH
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[NC_HojaContencion] ([Referencia],[IdNoConformidad],[Cantidad],[IdAlmacen],[NombreAlmacen],[FechaNC],[IdResponsable],[Notas],[PuntoLimpio]) VALUES " +
                                                                       "(" + referencia + ","+NoConformidad+","+Cantidad+",'"+Codigo+"','"+Almacen+"','"+FechaNC+"',"+responsable+",'',0)";
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

        public int devuelve_ID_Piloto_SMARTH(string nombre)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT Id FROM [SMARTH_DB].[dbo].[AUX_Personal_Mandos] where [Nombre] = '" + nombre + "'";
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
        } //SMARTH

        public DataSet leer_correosCALIDAD()
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql1 = "SELECT DISTINCT [Correo] FROM [SMARTH_DB].[dbo].[AUX_Personal_Mandos] WHERE OPActivo = 1 AND Correo IS NOT NULL AND Mail_AlertasCalidad = 1";
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

        //BLOQUE DE OPERARIOS VINCULADOS A LA NO CONFORMIDAD
        public DataSet Cargar_operarios_produccion(string molde)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT DISTINCT C_CLOCKNUMBER as OPNUMERO, C_OPERATORNAME AS OPNOMBRE, '' AS FORMNOMBRE, '' AS FECHA, '' AS FIRMA FROM HIS.T_HISJOBS WHERE C_PRODUCTTOOL = '" + molde+"' AND C_OPERATORNAME IS NOT NULL ORDER BY C_CLOCKNUMBER";
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
                string sql = "SELECT DISTINCT C_CLOCKNUMBER, C_OPERATORNAME FROM HIS.T_HISOPERATORS WHERE C_PRODUCTTOOL = '" + molde + "' AND C_OPERATORNAME IS NOT NULL AND C_OPERATORTYPE = 1 ORDER BY C_CLOCKNUMBER";
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

        public void GuardaFormacionOperario(int IdNoConformidad, int NumOperario, int Formador, string fecha, string firma)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "UPDATE [SMARTH_DB].[dbo].[NC_OperariosVinculados] SET [NumFormador] = " + Formador + ", [FechaOriginal] = '" + fecha + "', [Firma] = '"+firma+"' WHERE [IdNoConformidad] = " + IdNoConformidad + " and [NumOperario] = " + NumOperario + "";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();

            }
            catch (Exception)
            {
                cnn_GP12.Close();
            }
        }

        public DataSet devuelve_Set_Operarios_NAV()
        {

            try
            {
                //Lee resultados y devuelve el nuevo DataSet
                cnn_NAV.Open();
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = cnn_NAV;
                string sql2 = "SELECT CAST(CAST (No_ as INT) AS VARCHAR) AS NUMERO,[Search Name],[Status] FROM[NAVDB].[dbo].[THERMO$Employee] where Status = 0 and[Job Title] <> 'Administración' and No_<> 999 order by[Search Name]";
                SqlDataAdapter adapter = new SqlDataAdapter(sql2, cnn_NAV);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql2);
                cnn_NAV.Close();
                return ds;

            }
            catch (Exception)
            {
                cnn_NAV.Close();
                return null;
            }
        } //SMARTH-NAV

        public int devuelve_IDOperario_NAV(string operario) //SMARTH-NAV
        {
            try
            {
                cnn_NAV.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_NAV;
                string sql = "SELECT CAST(CAST(No_ as INT) AS VARCHAR) AS NUMERO,[Search Name],[Status] FROM[NAVDB].[dbo].[THERMO$Employee] where[Search Name] = '"+operario+"' order by[Search Name]";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_NAV);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_NAV.Close();
                return Convert.ToInt16(ds.Tables[0].Rows[0]["NUMERO"].ToString());
            }
            catch (Exception)
            {
                cnn_NAV.Close();
                return 0;
            }
        }

        public void InsertarOperarioNoConformidad(int IdNoConformidad, int NumOperario)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[NC_OperariosVinculados] ([IdNoConformidad], [NumOperario])" +
                                                                   " VALUES (" + IdNoConformidad + "," + NumOperario + ")";

                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();

            }
            catch (Exception)
            {
                cnn_GP12.Close();
            }
        }

        public bool existeNCXOperario(int IdNoConformidad, int NumOperario)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT Count(*) as count FROM[SMARTH_DB].[dbo].[NC_OperariosVinculados] where IdNoConformidad = "+ IdNoConformidad + " and NumOperario = "+ NumOperario + "";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_GP12.Close();
                if (Convert.ToInt32(ds.Tables[0].Rows[0]["count"].ToString()) == 0)
                {
                    return false;
                }
                else
                    return true;
            }
            catch (Exception)
            {
                cnn_GP12.Close();
                return false;
            }
        }


        //BLOQUE AUXILIAR DE ALERTA Y GUARDADO

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

        public void GuardaAlertaCalidad(int IdNoConformidad, int TipoNoConformidad, int EscaladoNoConformidad, int Cantidad_Stock, int Cantidad_Reclamada, int Cantidad_PPM, string Lote1, string Lote2, string Lote3, string Lote4, string DescripcionNC, string PRODContramedida, string CALContramedida, string INGContramedida, string Observaciones, int referencia, int PRODPiloto, int CALPilot, int INGPiloto,
                                        string FechaOriginal, string FechaRevision, string ImagenOK, string ImagenDEF1, string ImagenDEF2, string ImagenTRAZ1, string ImagenTRAZ2, string ProcesoAfectado, string NotasExtra, string idNCcliente, string caja1, string caja2, string caja3, string caja4, int defrepetitivo, int prodrepetitivo)
            {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "UPDATE [SMARTH_DB].[dbo].[NC_Alerta_de_calidad] SET [TipoNoConformidad] = "+TipoNoConformidad+ ",[EscaladoNoConformidad]="+EscaladoNoConformidad+ ", [StockBloquear]="+Cantidad_Stock+ ", [Cantidad]="+Cantidad_Reclamada+ ",[CantidadPPM]="+Cantidad_PPM+ ",[Lote1]='"+Lote1+ "',[Lote2]='"+Lote2+ "',[Lote3]='"+Lote3+ "',[Lote4]='"+Lote4+"'"+
                              ",[DescripcionProblema]= '"+DescripcionNC+ "',[ContramedidaProduccion]='"+PRODContramedida+ "',[ContramedidaCalidad]='"+CALContramedida+ "',[ContramedidaIngenieria]='"+INGContramedida+ "',[Observaciones]='"+Observaciones+ "',[Referencia]='"+referencia+ "',[PilotoProduccion]='"+PRODPiloto+ "',[PilotoCalidad]='"+CALPilot+ "',[PilotoIngenieria]='"+INGPiloto+"'"+
                              ",[FechaOriginal]='"+FechaOriginal+ "',[FechaRevision]='"+FechaRevision+ "',[ImagenNODefecto]='"+ImagenOK+ "',[ImagenDefecto1]='"+ImagenDEF1+ "',[ImagenDefecto2]='"+ImagenDEF2+ "',[ImagenTrazabilidad1]='"+ImagenTRAZ1+ "',[ImagenTrazabilidad2]='"+ImagenTRAZ2+ "',[ProcesoAfectado]='"+ProcesoAfectado+ "',[NotasAdicionales]='"+NotasExtra+ "',[IdNoConformidadCliente]='"+idNCcliente+ "',[Caja1]=RTRIM('" + caja1 + "'),[Caja2]='" + caja2 + "',[Caja3]='" + caja3 + "',[Caja4]='" + caja4 + "',[RepiteDefecto]=" + defrepetitivo + ",[RepiteReferencia]=" + prodrepetitivo + "" +
                              " WHERE [IdNoConformidad] = " + IdNoConformidad + "";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();

            }
            catch (Exception)
            {
                cnn_GP12.Close();
            }
        }

        public void InsertarAlertaCalidad(int IdNoConformidad, int TipoNoConformidad, int EscaladoNoConformidad, int Cantidad_Stock, int Cantidad_Reclamada, int Cantidad_PPM, string Lote1, string Lote2, string Lote3, string Lote4, string DescripcionNC, string PRODContramedida, string CALContramedida, string INGContramedida, string Observaciones, int referencia, int PRODPiloto, int CALPilot, int INGPiloto,
                                        string FechaOriginal, string FechaRevision, string ImagenOK, string ImagenDEF1, string ImagenDEF2, string ImagenTRAZ1, string ImagenTRAZ2, string ProcesoAfectado, string NotasExtra, string idNCcliente, string caja1, string caja2, string caja3, string caja4, int defrepetitivo, int prodrepetitivo)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[NC_Alerta_de_calidad] ([IdNoConformidad], [TipoNoConformidad], [EscaladoNoConformidad], [StockBloquear], [Cantidad],[CantidadPPM],[Lote1],[Lote2],[Lote3],[Lote4],[DescripcionProblema],[ContramedidaProduccion],[ContramedidaCalidad],[ContramedidaIngenieria],[Observaciones],[Referencia],[PilotoProduccion],[PilotoCalidad],[PilotoIngenieria],[FechaOriginal],[FechaRevision],[ImagenNODefecto],[ImagenDefecto1],[ImagenDefecto2],[ImagenTrazabilidad1],[ImagenTrazabilidad2],[ProcesoAfectado],[NotasAdicionales],[IdNoConformidadCliente],[Caja1],[Caja2],[Caja3],[Caja4],[RepiteDefecto],[RepiteReferencia])" +
                                                                   "VALUES (" + IdNoConformidad + "," + TipoNoConformidad + "," + EscaladoNoConformidad + "," + Cantidad_Stock + "," + Cantidad_Reclamada + "," + Cantidad_PPM + ",'" + Lote1 + "','" + Lote2 + "','" + Lote3 + "','" + Lote4 + "','" + DescripcionNC + "','" + PRODContramedida + "','" + CALContramedida + "','" + INGContramedida + "','" + Observaciones + "','" + referencia + "','" + PRODPiloto + "','" + CALPilot + "','" + INGPiloto + "','" + FechaOriginal + "','" + FechaRevision + "','" + ImagenOK + "','" + ImagenDEF1 + "','" + ImagenDEF2 + "','" + ImagenTRAZ1 + "','" + ImagenTRAZ2 + "','" + ProcesoAfectado + "','" + NotasExtra + "', '"+idNCcliente+"','"+caja1+ "','" + caja2 + "','" + caja3 + "','" + caja4 + "', "+defrepetitivo+","+prodrepetitivo+")";
                            
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();

            }
            catch (Exception)
            {
                cnn_GP12.Close();
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

        public bool existe_alerta(string NC)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT IdNoConformidad FROM[SMARTH_DB].[dbo].[NC_Alerta_de_calidad] WHERE IdNoConformidad = '"+NC+"'";
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

        public bool existe_alertaOP(string NC)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT IdNoConformidad FROM[SMARTH_DB].[dbo].[NC_OperariosVinculados] WHERE IdNoConformidad = '" + NC + "'";
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

        public int Devuelve_Recurrencia_Producto(string referencia, string fecha)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql1 = "SELECT Molde FROM [SMARTH_DB].[dbo].[AUX_TablaProductos] WHERE Referencia = "+referencia+"";
                SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, cnn_GP12);
                DataSet dsmolde = new DataSet();
                adapter1.Fill(dsmolde, sql1);

                string sql = "SELECT COUNT([FechaOriginal]) AS NUM FROM[SMARTH_DB].[dbo].[NC_Alerta_de_calidad] CAL"+
                              " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] PRD ON CAL.Referencia = PRD.Referencia"+
                              " where PRD.Molde = '"+ dsmolde.Tables[0].Rows[0]["Molde"].ToString() + "' and FechaOriginal > '"+fecha+"'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_GP12.Close();
                return Convert.ToInt32(ds.Tables[0].Rows[0]["NUM"].ToString());
            }
            catch (Exception)
            {
                cnn_GP12.Close();
                return 0;
            }
        }

        //NO CONFORMIDADES
        public void InsertarNoConformidad(int IdNoConformidad, string FechaOriginal, string FechaD3, string FechaD6, string FechaD8, int CheckCorte, int CheckD3, int CheckD6, int CheckD8)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[NC_Estado_No_Conformidad] ([IdNoConformidad], [FechaOriginal],[D3],[D6],[D8],[CheckCorte],[CheckD3],[CheckD6],[CheckD8] ,[CosteSeleccionEXT],[CostePiezasNOKEXT],[CosteCargosEXT],[CosteAdmonEXT],[CosteSeleccionINT],[CosteOtrosINT])" +
                                                                   " VALUES (" + IdNoConformidad + ",'" + FechaOriginal + "','" + FechaD3 + "','" + FechaD6 + "','" + FechaD8 + "',"+CheckCorte+ "," + CheckD3 + "," + CheckD6 + "," + CheckD8 + ",0,0,0,0,0,0)";

                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();

            }
            catch (Exception)
            {
                cnn_GP12.Close();
            }
        }

        //KPI

        //**\\OBSOLETOS//**\\

        //**\\ANTIGUO//**\\
        //CONECTORES GP12.ASPX

        public DataTable devuelve_setlista_informadores()
        {
            try
            {
                cnn_GP12.Open();
                //SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT P.[Referencia] as REF,[Molde],[Descripcion],[Cliente],[EstadoActual],[Responsable],[Fecharev],[Fechaprevsalida],[EstadoAnterior],[Fechaestanterior],[Observaciones]FROM [SMARTH_DB].[dbo].[AUX_TablaProductos] P LEFT JOIN [SMARTH_DB].[dbo].[GP12_ProductosEstados] E ON P.Referencia = E.Referencia ORDER BY P.Referencia", cnn_GP12);
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

        public bool Actualizar_productosBMS(int producto, int estado)
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

        public DataSet leer_correos()
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql1 = "SELECT DISTINCT Correo FROM [SMARTH_DB].[dbo].[AUX_Personal_Mandos] WHERE Mail_GP12 IS NOT NULL";
                SqlDataAdapter adapter = new SqlDataAdapter(sql1, cnn_GP12);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql1);
                cnn_GP12.Close();
                return ds;
            }
            catch (Exception)
            {
                cnn_GP12.Close();
                //mandar_mail(ex.Message);
                return null;
            }
        }


    }
}