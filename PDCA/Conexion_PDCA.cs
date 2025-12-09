using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace ThermoWeb.PDCA
{
    public class Conexion_PDCA
    {
        private readonly SqlConnection cnn_GP12 = new SqlConnection();
        private readonly OracleConnection cnn_bms = new OracleConnection();
       
        private readonly SqlConnection cnn_SMARTH = new SqlConnection();

        public Conexion_PDCA()
        {
            cnn_GP12.ConnectionString = ConfigurationManager.ConnectionStrings["area_rechazo"].ToString();
            cnn_SMARTH.ConnectionString = ConfigurationManager.ConnectionStrings["SMARTH"].ToString();
            cnn_bms.ConnectionString = ConfigurationManager.ConnectionStrings["BMS"].ToString();
            
        }


        /////////////////////CONSULTAS PDCA\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        public DataTable Devuelve_datatable_PDCA(string FILTRO)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT DISTINCT P.[IdPDCA], [Tipo] as TipoNUM, CASE WHEN[Tipo] = 1 THEN 'ACTA'  WHEN [Tipo] = 2 THEN 'DE CLIENTE' WHEN [Tipo] = 3 THEN 'INTERNO' WHEN [Tipo] = 4 THEN 'PROYECTO'  WHEN [Tipo] = 5 THEN 'NO CONFORMIDAD' ELSE 'OTROS' END AS TIPO,[Referencia],[ReferenciaTEXT],[Molde],[Modo],[Desviacion],[Cliente], [Prioridad] as PRIORIDADNUM, CASE WHEN[Prioridad] = 1 THEN 'PRIORIDAD1.png' WHEN[Prioridad] = 2 THEN 'PRIORIDAD2.png' WHEN[Prioridad] = 3 THEN 'PRIORIDAD3.png' WHEN[Prioridad] = 4 THEN 'PRIORIDAD4.png' END AS Prioridad, CAST(AC.ACCIONES-(CASE WHEN AA.ACCIONES IS NULL THEN 0 ELSE AA.ACCIONES END) AS varchar) + ' / ' + CAST(AC.ACCIONES AS varchar) AS ACCIONES, OP.Nombre, [Estado] as ESTADONUM, CASE WHEN[Estado] = 0 THEN 'Circulo0b.png' WHEN[Estado] = 1 THEN 'Ciculo25b.png' WHEN[Estado] = 2 THEN 'Ciculo50b.png' WHEN[Estado] = 3 THEN 'Ciculo75b.png' WHEN[Estado] = 4 THEN 'Ciculo100b.png'  WHEN[Estado] = 5 THEN 'CirculoNA.png' END AS Estado,[Apertura],[Cierre], V.VENCIDO" +
                                                                " FROM[SMARTH_DB].[dbo].[PDCA_Principal] P"+
                                                                " LEFT JOIN[SMARTH_DB].[dbo].AUX_Personal_Mandos OP ON P.Piloto = OP.Id"+
                                                                " LEFT JOIN (SELECT IdPDCA, COUNT([IdPDCA]) as ACCIONES FROM[SMARTH_DB].[dbo].[PDCA_X_ListaAcciones] WHERE Eliminar = 0 and IdReferencial > 0 GROUP BY IdPDCA) AC ON P.IdPDCA = AC.IdPDCA" +
                                                                " LEFT JOIN (SELECT IdPDCA, 1 as VENCIDO FROM[SMARTH_DB].[dbo].[PDCA_X_ListaAcciones] WHERE Eliminar = 0 AND SYSDATETIME() > FechaCierrePrev and (FechaCierreReal is null or FechaCierreReal = '')) V ON P.IdPDCA = V.IdPDCA" +
                                                                " LEFT JOIN (SELECT IdPDCA, COUNT([IdPDCA]) as ACCIONES FROM[SMARTH_DB].[dbo].[PDCA_X_ListaAcciones] WHERE (FechaCierreReal = '' or FechaCierreReal is null) AND Eliminar = 0 and IdReferencial > 0  GROUP BY IdPDCA) AA ON P.IdPDCA = AA.IdPDCA" +
                                                                " WHERE P.Id > 0"+FILTRO+"", cnn_SMARTH);
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
  
        public DataTable Devuelve_datatable_PDCAXListaAcciones(string WHERE, string orderby)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT case when MIN(PL.SEQNR) is null then 100 else MIN(PL.SEQNR) end AS SEQNR, A.AccionMaquina, A.[Id], A.[IdPDCA], A.[AccionPrioridad], A.[AccionEstado], P.Desviacion, A.IdReferencial, A.APPVinculada AS APP, APPIdVinculada, CASE WHEN[APPEstado] = 0 THEN 'Circulo0b.png' WHEN[APPEstado] = 1 THEN 'Ciculo25b.png' WHEN[APPEstado] = 2 THEN 'Ciculo50b.png' WHEN[APPEstado] = 3 THEN 'Ciculo75b.png' WHEN[APPEstado] = 4 THEN 'Ciculo100b.png' END AS APPEstado, OP.Nombre AS APPPiloto, FechaCierrePrev, FechaCierrePrevback, DesviacionEncontrada, CASE WHEN A.[Tipo] = 0 THEN '' WHEN A.[Tipo] = 1 THEN 'CONTENCIÓN:' WHEN A.[Tipo] = 2 THEN 'DETECCIÓN:' WHEN A.Tipo = 3 THEN 'OCURRENCIA:' END AS Tipo, CausaRaiz, Accion, Efectividad, FechaApertura, FechaCierreReal, CASE WHEN SYSDATETIME() > FechaCierrePrev THEN 1 ELSE 0 END AS VENCIDO, PR.Cliente, PR.Descripcion, RTRIM(LTRIM(a.AccionProducto)) AS ProdDescrip, CASE WHEN CAST(PR.Molde as varchar) IS NULL THEN '' ELSE CAST(PR.Molde as varchar) END AS ProdMold, REV.FECHAREV, REV.Revision" +
                                                                " FROM[SMARTH_DB].[dbo].[PDCA_X_ListaAcciones] A LEFT JOIN SMARTH_DB.DBO.AUX_Personal_Mandos OP ON A.Piloto = OP.Id" +
                                                                " LEFT JOIN SMARTH_DB.DBO.PDCA_Principal P ON A.IdPDCA = P.IdPDCA" +
                                                                " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] PR ON RTRIM(LTRIM(a.AccionProducto)) = CAST((PR.Referencia) as nvarchar)" +
                                                                " LEFT JOIN[SMARTH_DB].[dbo].[AUX_Planificacion] PL ON PR.Molde = PL.Molde" +
                                                                " LEFT JOIN(SELECT Z.IdReferencial, T.Revision, T.FechaApertura AS FECHAREV FROM(SELECT[IdReferencial], MAX([FechaApertura]) AS FECHA FROM[SMARTH_DB].[dbo].[PDCA_X_Revisiones] GROUP BY IdReferencial) Z LEFT JOIN[SMARTH_DB].[dbo].[PDCA_X_Revisiones] T ON Z.IdReferencial = T.IdReferencial AND Z.FECHA = T.FechaApertura  and T.Eliminar = 0) REV ON A.Id = REV.IdReferencial" +
                                                                " " + WHERE + "" +
                                                                " GROUP BY A.AccionMaquina, A.[Id], A.[IdPDCA], A.[AccionPrioridad], A.[AccionEstado], P.Desviacion, A.IdReferencial, APPIdVinculada, APPEstado, Nombre, FechaCierrePrev, DesviacionEncontrada, A.[Tipo], CausaRaiz, Accion, Efectividad, A.FechaApertura, FechaCierreReal, FechaCierrePrevback, PR.Cliente, PR.Descripcion, RTRIM(LTRIM(a.AccionProducto)), PR.Molde, APPVinculada, REV.FECHAREV, REV.Revision" +
                                                                " " + orderby + "", cnn_SMARTH);
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

        public DataTable Devuelve_datatable_DetallesAcciones(string id)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT A.[Id],A.[AdjuntoEVIDENCIA1],A.[AdjuntoEVIDENCIA2],A.[AdjuntoEVIDENCIA3],A.[AdjuntoEVIDENCIA4], A.[AccionMaquina], A.[AccionPrioridad],A.[AccionEstado],[IdPDCA], IdReferencial, A.APPVinculada AS APP,APPIdVinculada,CASE WHEN [APPEstado] = 0 THEN 'Circulo0b.png' WHEN [APPEstado] = 1 THEN 'Ciculo25b.png' WHEN [APPEstado] = 2 THEN 'Ciculo50b.png' WHEN [APPEstado] = 3 THEN 'Ciculo75b.png' WHEN [APPEstado] = 4 THEN 'Ciculo100b.png' END AS APPEstado,OP.Nombre AS APPPiloto, FechaCierrePrev, FechaCierrePrevback, DesviacionEncontrada,CASE WHEN [Tipo] = 0 THEN '' WHEN [Tipo] = 1 THEN 'CONTENCIÓN:' WHEN [Tipo] = 2 THEN 'DETECCIÓN:' WHEN Tipo = 3 THEN 'OCURRENCIA:' END AS Tipo, [Tipo] AS TipoINT, CausaRaiz, Accion,Efectividad, FechaApertura, FechaCierreReal," +
                                                                "A.APPVinculada, CASE WHEN A.APPVinculada = 1 THEN 'ICONOCALIDAD.png' WHEN A.APPVinculada = 2 THEN 'ICONOMAQUINAS.png' WHEN A.APPVinculada = 3 THEN 'ICONOMOLDES.png'  WHEN A.APPVinculada = 5 THEN 'ICONOPARAMETROS.png' ELSE 'null.png' END AS APPVinculadaIMG, A.AccionProducto as ProdMOLDE, " +
                                                                "CASE WHEN A.OrigenCausa IS NULL THEN 0 ELSE A.OrigenCausa END AS OrigenCausa, CASE WHEN A.EstadoContencion IS NULL THEN 0 ELSE A.EstadoContencion END AS EstadoContencion, A.LeccionAprendida" +
                                                                " FROM[SMARTH_DB].[dbo].[PDCA_X_ListaAcciones] A LEFT JOIN SMARTH_DB.DBO.AUX_Personal_Mandos OP ON A.Piloto = OP.Id  WHERE A.[Id] = " + id + "  AND A.Eliminar = 0", cnn_SMARTH);
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

        public DataTable Devuelve_datatable_RevisionAcciones(string id)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM[SMARTH_DB].[dbo].[PDCA_X_Revisiones]" +
                                                                " WHERE IdReferencial = "+id+ " AND Eliminar = 0 order by FechaApertura desc", cnn_SMARTH);
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

        //Insertar acciones y planes
        //PLANES DE ACCIONES
        public int Devuelve_MAXID_PDCA_Principal()
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

        public bool Insertar_Nuevo_PlanAcciones(int IdPDCA, int Tipo, string Referencia, string ReferenciaTEXT, string molde, int Modo, string objetivo, string Cliente, int Prioridad, int Piloto, string FechaApertura, int Accionprioridad, int Accionestado)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "INSERT INTO[SMARTH_DB].[dbo].[PDCA_Principal] " +
                             " ([IdPDCA],[Tipo],[Referencia],[ReferenciaTEXT],[Modo],[Desviacion],[Cliente],[Prioridad],[Molde],[Piloto],[Estado],[Apertura])" +
                             " VALUES("+IdPDCA+", "+Tipo+",'"+Referencia+ "', '" + ReferenciaTEXT + "'," + Modo+", '"+objetivo+"', '"+Cliente+"',"+Prioridad+","+molde+","+Piloto+", 1, '"+FechaApertura+"')";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                Insertar_A_ListaAcciones(IdPDCA, 0, FechaApertura, "NULL", null, 0, "", "", "", 2, 0, 0, 0, 0, 0,"",0,0,"", "", "", "", "",0,0,0);
                return false;
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }
        
        public bool Actualizar_PlanAccion(int IdPDCA, int Tipo, string Referencia, int Modo, string Desviacion, string Cliente, int Prioridad, int Molde, int Piloto, int estado, string ReferenciaTEXT, string FechaCierre)
        {
            try
            {

                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "UPDATE [SMARTH_DB].[dbo].[PDCA_Principal]" +
                             " SET [Tipo] = "+Tipo+",[Referencia] = '"+Referencia+"',[Modo] = "+Modo+",[Desviacion] = '"+Desviacion+"',[Cliente] = '"+Cliente+"',[Prioridad] = "+Prioridad+",[Molde] = "+Molde+",[Piloto] = "+Piloto+",[Estado] = "+estado+", [ReferenciaTEXT] = '"+ReferenciaTEXT+"'"+FechaCierre+""+
                             " where IdPDCA = "+IdPDCA+"";
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

        public int Devuelve_INTAbiertas(string IdPDCA)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT Count ([Id]) as ABIERTAS FROM [SMARTH_DB].[dbo].[PDCA_X_ListaAcciones] where IdPDCA = "+IdPDCA+" and (FechaCierreReal IS NULL or FechaCierreReal = '') and Eliminar = 0 and IdReferencial > 0";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return Convert.ToInt16(ds.Tables[0].Rows[0]["ABIERTAS"].ToString());
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return 1;
            }
        }
        //ACCIONES DEFINIDAS
        public bool Marcar_Accion_Borrado_PDCA(string id)
        {
            try
            {

                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "UPDATE [SMARTH_DB].[dbo].[PDCA_X_ListaAcciones] SET Eliminar = 1 WHERE Id = " + id + "";
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

        public int Devuelve_IDReferencial_Asignado(int IdPDCA)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT (Count (IdPDCA)) AS IdReferencial FROM[SMARTH_DB].[dbo].[PDCA_X_ListaAcciones] where IdPDCA = "+IdPDCA+"";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                if (ds.Tables[0].Rows.Count == 0)
                { return 0; }
                else
                {
                return Convert.ToInt16(ds.Tables[0].Rows[0]["IdReferencial"].ToString());
                }
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return 0;
            }
        } 

        public bool Insertar_A_ListaAcciones(int IdPDCA, int IdReferencial, string FechaApertura, string FechaCierrePrev, string FechaCierreReal, int Tipo, string Desviacion, string CausaRaiz, string Accion, int Piloto, int APPEstado, int Efectividad, int APPVinculada, int APPIdVinculada, int Eliminar, string producto, int Accionprioridad, int Accionestado, string AccionMaquina, string Evidencia1, string Evidencia2, string Evidencia3, string Evidencia4, int EstadoContencion, int Origencausa, int LeccionAprendida)
        {
            try
            {

                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "INSERT INTO[SMARTH_DB].[dbo].[PDCA_X_ListaAcciones]"+
                             " ([IdPDCA],[IdReferencial],[FechaApertura],[FechaCierrePrev],[FechaCierrePrevback],[FechaCierreReal],[Tipo],[DesviacionEncontrada],[CausaRaiz],[Accion],[Piloto],[APPEstado],[Efectividad],[APPVinculada],[APPIdVinculada],[Eliminar],[AccionProducto],[AccionPrioridad],[AccionEstado],[AccionMaquina],[AdjuntoEVIDENCIA1],[AdjuntoEVIDENCIA2],[AdjuntoEVIDENCIA3],[AdjuntoEVIDENCIA4],[EstadoContencion],[OrigenCausa],[LeccionAprendida])" +
                             " VALUES("+IdPDCA+","+IdReferencial+",'"+FechaApertura+"',"+FechaCierrePrev+ "," + FechaCierrePrev + ",NULL," + Tipo+",'"+Desviacion+"','"+CausaRaiz+"','"+Accion+"',"+Piloto+","+APPEstado+","+Efectividad+","+APPVinculada+","+APPIdVinculada+","+Eliminar+",'"+producto+"', "+ Accionprioridad + ","+ Accionestado + ",'"+AccionMaquina+"','"+Evidencia1+ "','" + Evidencia2 + "','" + Evidencia3 + "','" + Evidencia4 + "', "+EstadoContencion+", "+Origencausa+","+LeccionAprendida+")";
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

        public bool Actualizar_A_ListaAcciones(int Id, string FechaCierrePrev, int Tipo, string Desviacion, string CausaRaiz, string Accion, int Piloto, int APPVinculada, int APPIdVinculada, string FechaCierreReal, string producto, int Accionprioridad, int Accionestado, string AccionMaquina, string Evidencia1, string Evidencia2, string Evidencia3, string Evidencia4, int EstadoContencion, int Origencausa, int LeccionAprendida)
        {
            try
            {

                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "UPDATE [SMARTH_DB].[dbo].[PDCA_X_ListaAcciones]" +
                             " SET [Tipo] = " + Tipo + ", [DesviacionEncontrada] = '" + Desviacion + "', [CausaRaiz] = '" + CausaRaiz + "',[Accion] = '" + Accion + "', [Piloto] = " + Piloto + ", [APPVinculada] = " + APPVinculada + ",[AccionProducto] = '"+producto+"', [APPIdVinculada] = " + APPIdVinculada + ", [AccionPrioridad] = "+Accionprioridad+ ", [AccionEstado] = "+Accionestado+ ",[AccionMaquina] ='" + AccionMaquina + "',[AdjuntoEVIDENCIA1]='" + Evidencia1+ "',[AdjuntoEVIDENCIA2]='" + Evidencia2 + "',[AdjuntoEVIDENCIA3]='" + Evidencia3 + "',[AdjuntoEVIDENCIA4]='" + Evidencia4 + "',[EstadoContencion] = " + EstadoContencion + " ,[OrigenCausa]= " + Origencausa + ",[LeccionAprendida]= " + LeccionAprendida + "" + FechaCierreReal+""+FechaCierrePrev+ "" +
                             " WHERE Id = "+Id+"";
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
        //REVISIONES
        public bool Insertar_A_AccionRevision(int IdReferencial, string Revision)
        {
            try
            {

                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[PDCA_X_Revisiones]" +
                             " ([IdReferencial],[FechaApertura],[Revision],[Eliminar])" +
                             " VALUES(" + IdReferencial + ",SYSDATETIME(),'" + Revision + "',0)";
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

        public bool Marcar_Borrado_Revision(string Id)
        {
            try
            {

                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "UPDATE [SMARTH_DB].[dbo].[PDCA_X_Revisiones]" +
                             " SET [Eliminar] = 1" +
                             " WHERE [Id] = " + Id+"";
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

        //Selecciones de otras APP
        public DataTable Devuelve_Lista_Productos_NUEVO_PDCA()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT CAST ([Referencia] AS varchar) + ' || ' + CAST([Molde] AS varchar) + ' || ' + [Descripcion]  + ' || ' + [Cliente] as PRODUCTO FROM [SMARTH_DB].[dbo].[AUX_TablaProductos] where Referencia > 10000000 order by Referencia asc", cnn_SMARTH);
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

        public DataTable Devuelve_Lista_Moldes_NUEVO_PDCA()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT DISTINCT CAST([ReferenciaMolde] AS VARCHAR) + ' || ' + MOL.[Descripcion] +' || '+ PR.Cliente AS MOLDE FROM [SMARTH_DB].[dbo].[AUX_Lista_Moldes] MOL LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] PR ON MOL.ReferenciaMolde = PR.Molde WHERE Molde IS NOT NULL ORDER BY MOLDE ASC", cnn_SMARTH);
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
        public DataTable Devuelve_Producto_Seleccionado_NUEVO_PDCA(string Referencia)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM [SMARTH_DB].[dbo].[AUX_TablaProductos] where Referencia = "+Referencia+"", cnn_SMARTH);
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

        public DataTable Devuelve_Molde_Seleccionado_NUEVO_PDCA(string Molde)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT DISTINCT ReferenciaMolde, MOL.[Descripcion], PR.Cliente FROM[SMARTH_DB].[dbo].[AUX_Lista_Moldes] MOL LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] PR ON MOL.ReferenciaMolde = PR.Molde WHERE Molde = "+Molde+"", cnn_SMARTH);
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
        public DataTable Devuelve_TOP100_Partes_Moldes()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT TOP (100) cast([IdReparacionMolde] as varchar) + ' | ' + cast ([IDMoldes] as varchar) +' | ' + replace([MotivoAveria],CHAR(13)+CHAR(10),'') as PARTE FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes] order by IdReparacionMolde desc", cnn_SMARTH);
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

        public DataTable Devuelve_Parte_Molde_Seleccionado(string ID)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT cast([IdReparacionMolde] as varchar) AS ID, cast ([IDMoldes] as varchar) AS PRODUCTO, [MotivoAveria] as MOTIVO FROM[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes] WHERE IdReparacionMolde = '"+ID+"'", cnn_SMARTH);
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

        public DataTable Devuelve_TOP100_Partes_Maquina()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT TOP (100) CAST([IdMantenimiento] AS varchar) + ' | ' + M.Maquina + ' | ' + replace([MotivoAveria],CHAR(13)+CHAR(10),' ') AS PARTE" +
                    " FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas] A" +
                    " LEFT JOIN [SMARTH_DB].[dbo].[AUX_Lista_Máquinas] M ON A.IDMaquina = M.IdMaquina" +
                    " order by IdMantenimiento desc ", cnn_SMARTH);
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

        public DataTable Devuelve_Parte_Maquina_Seleccionado(string ID)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT CAST([IdMantenimiento] AS varchar) as ID, M.Maquina as PRODUCTO, [MotivoAveria] as MOTIVO" +
                    " FROM [SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas] A" +
                    " LEFT JOIN [SMARTH_DB].[dbo].[AUX_Lista_Máquinas] M ON A.IDMaquina = M.IdMaquina" +
                    " WHERE IdMantenimiento = "+ID+"", cnn_SMARTH);
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

        public DataTable Devuelve_TOP100_NoConformidades()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT TOP (100) CAST([IdNoConformidad] AS varchar) + ' | ' + CAST([Referencia] AS varchar) + ' | ' + REPLACE(DescripcionProblema,CHAR(13)+CHAR(10),' ') as PARTE FROM [SMARTH_DB].[dbo].[NC_Alerta_de_calidad] order by IdNoConformidad desc", cnn_SMARTH);
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

        public DataTable Devuelve_NoConformidad_Seleccionado(string ID)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT CAST([IdNoConformidad] AS varchar) as ID, CAST([Referencia] AS varchar) AS PRODUCTO, [DescripcionProblema] AS MOTIVO FROM [SMARTH_DB].[dbo].[NC_Alerta_de_calidad] WHERE IdNoConformidad = "+ID+"", cnn_SMARTH);
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

        /////////////////////CONSULTAS MiPDCA\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        public DataTable Devuelve_MiPDCA_Pendientes_Reparacion_Molde(int operario)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("Select IdReparacionMolde, IDMoldes, MotivoAveria, p.Nombre"+
                                                                " FROM(select IdReparacionMolde, IdRealizadoPor, IDMoldes, MotivoAveria, substring([AsignadoA], 1, charindex(' ',[AsignadoA]) - 1) as Asignado from[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes] where Terminado = 0"+
                                                                " union all select IdReparacionMolde, IdRealizadoPor, IDMoldes, MotivoAveria,  substring([AsignadoA],charindex(' ',[AsignadoA]) + 1,charindex(' ',[AsignadoA]) - 1) as Asignado from[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes] where Terminado = 0"+
                                                                " union all select IdReparacionMolde, IdRealizadoPor, IDMoldes, MotivoAveria, reverse(substring(reverse([AsignadoA]), 0, charindex('-', reverse([AsignadoA])))) as Asignado from[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes] where Terminado = 0) A"+
                                                                " LEFT JOIN SMARTH_DB.DBO.AUX_Personal_Mandos P ON A.Asignado = P.Id WHERE a.Asignado = "+operario+"", cnn_SMARTH);
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

        public DataTable Devuelve_MiPDCA_Pendientes_Validacion_Molde(int operario)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("Select IdReparacionMolde, IDMoldes, MotivoAveria, p.Nombre"+
                                                                " FROM(select IdReparacionMolde, IdRealizadoPor, IDMoldes, MotivoAveria, [IdEncargado] from[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Moldes] where Terminado = 1 and Revisado = 0) A"+
                                                                " LEFT JOIN SMARTH_DB.DBO.AUX_Personal_Mandos P ON A.[IdEncargado] = P.Id WHERE a.[IdEncargado] = "+operario+"", cnn_SMARTH);
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

        public DataTable Devuelve_MiPDCA_Pendientes_Reparacion_Maquina(int operario)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("Select IdMantenimiento as PARTE, M.Maquina as Descripcion, MotivoAveria, p.Nombre"+
                                                                " FROM(select IdMantenimiento, IdRealizadoPor, IDMaquina, MotivoAveria, substring([AsignadoA], 1, charindex(' ',[AsignadoA]) - 1) as Asignado"+
                                                                " from[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas] where Terminado = 1 and Revisado = 0"+
                                                                " union all select IdMantenimiento, IdRealizadoPor, IDMaquina, MotivoAveria, substring([AsignadoA],charindex(' ',[AsignadoA]) + 1,charindex(' ',[AsignadoA]) - 1) as Asignado"+
                                                                " from[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas] where Terminado = 1 and Revisado = 0"+
                                                                " union all select IdMantenimiento, IdRealizadoPor, IDMaquina, MotivoAveria, reverse(substring(reverse([AsignadoA]), 0, charindex('-', reverse([AsignadoA])))) as Asignado"+
                                                                " from[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas] where Terminado = 1 and Revisado = 0) A"+
                                                                " LEFT JOIN SMARTH_DB.DBO.AUX_Personal_Mandos P ON A.Asignado = P.Id"+
                                                                " LEFT JOIN [SMARTH_DB].[dbo].[AUX_Lista_Máquinas] M ON A.IDMaquina = M.IdMaquina" +
                                                                " WHERE a.Asignado = "+operario+"", cnn_SMARTH);
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

        public DataTable Devuelve_MiPDCA_Pendientes_Validacion_Maquina(int operario)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("Select IdMantenimiento as Parte, M.Maquina as Descripcion, MotivoAveria, p.Nombre, Reparacion"+
                                                                " FROM(select IdMantenimiento, IdRealizadoPor, IDMaquina, MotivoAveria, Reparacion, [IdEncargado] from[SMARTH_DB].[dbo].[MANTENIMIENTO_Partes_Reparacion_Maquinas] where Terminado = 1 and Revisado = 0) A"+
                                                                " LEFT JOIN SMARTH_DB.DBO.AUX_Personal_Mandos P ON A.[IdEncargado] = P.Id"+
                                                                " LEFT JOIN [SMARTH_DB].[dbo].[AUX_Lista_Máquinas] M ON A.IDMaquina = M.IdMaquina" +
                                                                " WHERE a.[IdEncargado] = "+operario+"", cnn_SMARTH);
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

        public DataTable Devuelve_MiPDCA_Pendientes_No_ConformidadesEnCurso(int operario)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT A.ID, A.DESCRIPCION, CONTRAMEDIDA, CAST(PRODUCTO AS varchar) + '  ' + P.Descripcion AS PRODDESCRIPCION, Nombre" +
" FROM(SELECT A.[IdNoConformidad] AS ID,[Referencia] AS PRODUCTO,[DescripcionProblema] AS DESCRIPCION,[PilotoCalidad] AS PILOTO,[ContramedidaCalidad] AS CONTRAMEDIDA" +
" FROM[SMARTH_DB].[dbo].[NC_Alerta_de_calidad] A LEFT JOIN [SMARTH_DB].[dbo].[NC_Estado_No_Conformidad] E ON A.IdNoConformidad = E.IdNoConformidad  WHERE (D3CIERRE IS NULL OR D3CIERRE = '') OR ((D6CIERRE IS NULL OR D6CIERRE = '') AND CHECKD6 = 1) OR(D8CIERRE IS NULL OR D8CIERRE = '') AND CHECKD8 = 1" +
" UNION ALL SELECT A.[IdNoConformidad] AS ID,[Referencia] AS PRODUCTO,[DescripcionProblema] AS DESCRIPCION,[PilotoProduccion] AS PILOTO,[ContramedidaProduccion] AS CONTRAMEDIDA" +
" FROM[SMARTH_DB].[dbo].[NC_Alerta_de_calidad] A LEFT JOIN [SMARTH_DB].[dbo].[NC_Estado_No_Conformidad] E ON A.IdNoConformidad = E.IdNoConformidad WHERE (D3CIERRE IS NULL OR D3CIERRE = '') OR ((D6CIERRE IS NULL OR D6CIERRE = '') AND CHECKD6 = 1) OR(D8CIERRE IS NULL OR D8CIERRE = '') AND CHECKD8 = 1" +
" UNION ALL SELECT A.[IdNoConformidad] AS ID,[Referencia] AS PRODUCTO,[DescripcionProblema] AS DESCRIPCION,[PilotoIngenieria] AS PILOTO,[ContramedidaIngenieria] AS CONTRAMEDIDA" +
" FROM[SMARTH_DB].[dbo].[NC_Alerta_de_calidad] A LEFT JOIN [SMARTH_DB].[dbo].[NC_Estado_No_Conformidad] E ON A.IdNoConformidad = E.IdNoConformidad WHERE (D3CIERRE IS NULL OR D3CIERRE = '') OR ((D6CIERRE IS NULL OR D6CIERRE = '') AND CHECKD6 = 1) OR(D8CIERRE IS NULL OR D8CIERRE = '') AND CHECKD8 = 1) A" +
" LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] P ON A.PRODUCTO = P.Referencia LEFT JOIN SMARTH_DB.DBO.AUX_Personal_Mandos O ON PILOTO = O.Id" +
" WHERE PILOTO = " + operario + " ORDER BY ID DESC", cnn_SMARTH);

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

        public DataTable Devuelve_MiPDCA_Pendientes_No_ConformidadesVencidas(int operario)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT A.ID, A.DESCRIPCION, CONTRAMEDIDA, CAST(PRODUCTO AS varchar) + '  ' + P.Descripcion AS PRODDESCRIPCION, Nombre" +
                        " FROM(SELECT A.[IdNoConformidad] AS ID,[Referencia] AS PRODUCTO,[DescripcionProblema] AS DESCRIPCION,[PilotoCalidad] AS PILOTO,[ContramedidaCalidad] AS CONTRAMEDIDA" +
                        " FROM[SMARTH_DB].[dbo].[NC_Alerta_de_calidad] A LEFT JOIN [SMARTH_DB].[dbo].[NC_Estado_No_Conformidad] E ON A.IdNoConformidad = E.IdNoConformidad  WHERE ((D3 < SYSDATETIME() AND (D3CIERRE IS NULL OR D3CIERRE = ''))"+
                            " OR(CASE WHEN D6 = 'N/A' THEN '01/01/2900 0:00:00' WHEN D6 = '' THEN '01/01/1900 0:00:00' ELSE cast(d6 as datetime) end < SYSDATETIME() AND(D6CIERRE IS NULL OR D6CIERRE = '') AND CHECKD6 = 1)"+
                            " OR(CASE WHEN D8 = 'N/A' THEN '01/01/2900 0:00:00' WHEN D8 = '' THEN '01/01/1900 0:00:00' ELSE cast(d8 as datetime) end < SYSDATETIME() AND(D8CIERRE IS NULL OR D8CIERRE = '')) AND CHECKD8 = 1)"+
    
                        " UNION ALL SELECT A.[IdNoConformidad] AS ID,[Referencia] AS PRODUCTO,[DescripcionProblema] AS DESCRIPCION,[PilotoProduccion] AS PILOTO,[ContramedidaProduccion] AS CONTRAMEDIDA" +
                        " FROM[SMARTH_DB].[dbo].[NC_Alerta_de_calidad] A LEFT JOIN [SMARTH_DB].[dbo].[NC_Estado_No_Conformidad] E ON A.IdNoConformidad = E.IdNoConformidad WHERE ((D3 < SYSDATETIME() AND (D3CIERRE IS NULL OR D3CIERRE = ''))" +
                            " OR(CASE WHEN D6 = 'N/A' THEN '01/01/2900 0:00:00' WHEN D6 = '' THEN '01/01/1900 0:00:00' ELSE cast(d6 as datetime) end < SYSDATETIME() AND(D6CIERRE IS NULL OR D6CIERRE = '') AND CHECKD6 = 1)" +
                            " OR(CASE WHEN D8 = 'N/A' THEN '01/01/2900 0:00:00' WHEN D8 = '' THEN '01/01/1900 0:00:00' ELSE cast(d8 as datetime) end < SYSDATETIME() AND(D8CIERRE IS NULL OR D8CIERRE = '')) AND CHECKD8 = 1)" +
                        " UNION ALL SELECT A.[IdNoConformidad] AS ID,[Referencia] AS PRODUCTO,[DescripcionProblema] AS DESCRIPCION,[PilotoIngenieria] AS PILOTO,[ContramedidaIngenieria] AS CONTRAMEDIDA" +
                        " FROM[SMARTH_DB].[dbo].[NC_Alerta_de_calidad] A LEFT JOIN [SMARTH_DB].[dbo].[NC_Estado_No_Conformidad] E ON A.IdNoConformidad = E.IdNoConformidad WHERE ((D3 < SYSDATETIME() AND (D3CIERRE IS NULL OR D3CIERRE = ''))" +
                            " OR(CASE WHEN D6 = 'N/A' THEN '01/01/2900 0:00:00' WHEN D6 = '' THEN '01/01/1900 0:00:00' ELSE cast(d6 as datetime) end < SYSDATETIME() AND(D6CIERRE IS NULL OR D6CIERRE = '') AND CHECKD6 = 1)" +
                            " OR(CASE WHEN D8 = 'N/A' THEN '01/01/2900 0:00:00' WHEN D8 = '' THEN '01/01/1900 0:00:00' ELSE cast(d8 as datetime) end < SYSDATETIME() AND(D8CIERRE IS NULL OR D8CIERRE = '')) AND CHECKD8 = 1))A" +
                        " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] P ON A.PRODUCTO = P.Referencia LEFT JOIN SMARTH_DB.DBO.AUX_Personal_Mandos O ON PILOTO = O.Id" +
                        " WHERE PILOTO = " + operario + " ORDER BY ID DESC", cnn_SMARTH);

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

        public DataTable Devuelve_MiPDCA_Pendientes_Acciones(int operario)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT P.IdPDCA as ID, P.Molde as Molde, P.ReferenciaTEXT as Producto, CAST(P.IdPDCA AS VARCHAR) + '.' + CAST(A.IdReferencial AS VARCHAR) AS NUMACCION, Desviacion AS PLANACCION, a.DesviacionEncontrada, A.ACCION AS REQUISITO, O1.Nombre AS JEFE, O2.Nombre AS EJECUTA, CASE WHEN a.FechaCierrePrev IS NULL THEN a.FechaApertura ELSE A.FechaCierrePrev END AS FECHA" +
                                                                " FROM[SMARTH_DB].[dbo].[PDCA_Principal] P left join SMARTH_DB.dbo.PDCA_X_ListaAcciones A ON P.IdPDCA = A.IdPDCA LEFT JOIN SMARTH_DB.DBO.AUX_Personal_Mandos O1 ON P.Piloto = O1.Id LEFT JOIN SMARTH_DB.DBO.AUX_Personal_Mandos O2 ON A.Piloto = O2.Id"+
                                                                " WHERE A.FechaCierreReal IS NULL AND P.Estado <> 4 AND A.IdReferencial <> 0 AND (O1.ID = "+operario+ " or O2.ID = " + operario + ")" +
                                                                " order by CASE WHEN a.FechaCierrePrev IS NULL THEN a.FechaApertura ELSE A.FechaCierrePrev END asc", cnn_SMARTH);
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

    }
}