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

namespace ThermoWeb.GP12
{
    public class Conexion_GP12
    {
        private readonly SqlConnection cnn_GP12 = new SqlConnection();
        private readonly OracleConnection cnn_bms = new OracleConnection();
        private readonly SqlConnection cnn_RPS = new SqlConnection();
        private readonly SqlConnection cnn_GP122 = new SqlConnection();
        private readonly SqlConnection cnn_NAV = new SqlConnection();
        private readonly SqlConnection cnn_SMARTH = new SqlConnection();

        public Conexion_GP12()
        {
            cnn_GP12.ConnectionString = ConfigurationManager.ConnectionStrings["area_rechazo"].ToString();
            cnn_GP122.ConnectionString = ConfigurationManager.ConnectionStrings["area_rechazo"].ToString();
            cnn_bms.ConnectionString = ConfigurationManager.ConnectionStrings["BMS"].ToString();
            cnn_RPS.ConnectionString = ConfigurationManager.ConnectionStrings["RPS"].ToString();
            cnn_NAV.ConnectionString = ConfigurationManager.ConnectionStrings["NAV"].ToString();
            cnn_SMARTH.ConnectionString = ConfigurationManager.ConnectionStrings["SMARTH"].ToString();
        }


        //CONECTORES GP12.ASPX
        public void insertarRevision(string FechaInicio, string FechaFin, double HorasInspeccion, int ProveedorRevision, int OperarioRevisionINT, string OperarioRevision, int referencia, string nombre, int molde, int nlote, string ncaja, int piezasrevisadas, int piezasok, int retrabajadas, int piezasnok, int def01, int def02, int def03, int def04, int def05, int def06, int def07,
                                    int def08, int def09, int def10, int def11, int def12, int def13, int def14, int def15, int def16, int def17, int def18, int def19, int def20, int def21, int def22, int def23, int def24, int def25, int def26, int def27, int def28, int def29, int def30, string incidencias, string imagendef1, string imagendef2, string imagendef3, string notas,
                                    int op1, int op2, int op3, int op4, int razonrevision, double costerevision, double costescrap, double costeoperario, int alertaGP12)        
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "";
                sql = "INSERT INTO [SMARTH_DB].[dbo].[GP12_Historico_2021] (FechaInicio, FechaFin, HorasInspeccion, ProveedorRevision, OperarioRevisionINT, OperarioRevision, Referencia, Nombre, Molde, Nlote, Ncaja, PiezasRevisadas, PiezasOK, Retrabajadas, PiezasNOK, " +
                                         "Def1, Def2, Def3, Def4, Def5, Def6, Def7, Def8, Def9, Def10, Def11, Def12, Def13, Def14, Def15, Def16, Def17, Def18, Def19, Def20, Def21, Def22, Def23, Def24, Def25, Def26, Def27, Def28, Def29, Def30, "+
                                         "Incidencias, ImagenDefecto1, ImagenDefecto2, ImagenDefecto3, Notas, NOp1, NOp2, NOp3, NOp4, RazonRevision, CosteRevision, CosteScrapRevision, CostePiezaRevision, Informador1, Informador2, Informador3, Informador4, AlertaGP12, ScrapAlmacen) " +
                       "VALUES ('" + FechaInicio + "','" + FechaFin + "', " + HorasInspeccion.ToString().Replace(',', '.') + ", " + ProveedorRevision + "," + OperarioRevisionINT + ",'" + OperarioRevision + "'," + referencia + ",'" + nombre + "'," + molde + "," + nlote + ",'" + ncaja + "'," + piezasrevisadas + "," + piezasok + "," + retrabajadas + "," + piezasnok + "," + def01 + "," + def02 + "," + def03 + "," + def04 + "," + def05 + "," + def06 + "," + def07 + "," + 
                                def08 +"," + def09 +"," + def10 +"," + def11 +"," + def12 +"," + def13 +"," + def14 +"," + def15 +"," + def16 +"," + def17 +"," + def18 +"," + def19 +"," + def20 +"," + def21 +"," + def22 +"," + def23 +"," + def24 +"," + def25 +"," + def26 +"," + def27 +"," + def28 +"," + def29 +"," + def30 +",'" +
                                incidencias + "','" + imagendef1 + "','" + imagendef2 + "','" + imagendef3 + "','" + notas + "'," + op1 + "," + op2 + "," + op3 + "," + op4 + ",'" + razonrevision + "'," + costerevision.ToString().Replace(',', '.') + "," + costescrap.ToString().Replace(',', '.') + "," + costeoperario.ToString().Replace(',', '.') + ",'-','-','-','-', "+ alertaGP12 +",1)";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();
                
            }
            catch (Exception EX)
            {
                cnn_GP12.Close();
            }
        }

        public DataSet cargar_operarios_bms(int operario)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT TRIM(C_ID) as NUMOPERARIO, TRIM(C_NAME) AS NOMBRE, TRIM(C_DEPARTMENT) AS EMPRESA, C_USERVALUE1 AS COSTE FROM PCMS.T_EMPLOYEES WHERE C_ID = '" + operario + "'";
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

        public DataSet cargar_producto(string producto)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT TRIM(P.C_ID) as PRODUCTO, TRIM(P.C_CUSTOMER) AS CLIENTE, TRIM(P.C_LONG_DESCR) AS DESCRIPCION, TRIM(T.C_TOOL_ID) AS MOLDE, TRUNC(P.C_CUSTOMVALUE2,2) AS COSTEPIEZA FROM PCMS.T_PRODUCTS P LEFT JOIN  PCMS.T_TOOL_X_PRODUCT T ON (P.C_ID = T.C_PRODUCT_ID) WHERE P.C_ID = '" + producto + "'";
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

        public DataSet devuelve_documentos(string referencia)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT * FROM [SMARTH_DB].[dbo].[DOCUMENTAL_Base_DocumentosXReferencias] WHERE Referencia = "+referencia+"";
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

        public DataSet devuelve_lista_ordenes(int producto)
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

        public DataSet devuelve_lista_razonesrevision()  //LLEVAR A SMARTH EN [SMARTH_DB].[dbo].[AUX_GP12_RazonRevision]
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

        public int devuelve_IDproveedorrevision(string proveedor)
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
                return 1;
            }
        }


        //CONECTORES REFERENCIAS Y ESTADO
      

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

       
        public bool InsertaProductosReferenciaEstados() //SMARTH
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[GP12_ProductosEstados] SELECT P.Referencia, 0 as EstadoActual, 2 as Responsable, null  as Fecharev, null as Fechaprevsalida, null as EstadoAnterior, null as Fechaestanterior, null as Observaciones, null as IdNoConformidad  FROM [SMARTH_DB].[dbo].[AUX_TablaProductos] P FULL JOIN [SMARTH_DB].[dbo].[GP12_ProductosEstados] E ON P.Referencia = E.Referencia WHERE E.Referencia IS NULL"; 
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

        public bool InsertaProductosTablaDocumentos()
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[DOCUMENTAL_Base_DocumentosXReferencias] SELECT P.Referencia, P.Molde, null as ImagenPieza, null as PlanControl, null as PautaControl, null as PautaRecepcion1, null as PautaRecepcion2, null as OperacionEstandar, null as OperacionEstandar2, null as Defoteca, null as Embalaje, null as Gp12, null as [PautaRetrabajo], null as [VideoAuxiliar], null as [Observaciones], null as [NoConformidades], SYSDATETIME() as [FechaModificacion], 'Agregada referencia al sistema de documentación.' as [RazonModificacion]  FROM [SMARTH_DB].[dbo].[AUX_TablaProductos] P FULL JOIN [SMARTH_DB].[dbo].[DOCUMENTAL_Base_DocumentosXReferencias] E ON P.Referencia = E.Referencia WHERE E.Referencia IS NULL"; 
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
  
        public DataSet devuelve_EstadosFiltros_Referencia(string referencia) //SMARTH
        {
            try
            {
                cnn_GP12.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT P.[Referencia] as REF, R.Razon as EstadoActual, E.EstadoActual as IDEstactual, [Fechaprevsalida],[Observaciones],D.Nombre FROM [SMARTH_DB].[dbo].[AUX_TablaProductos] P INNER JOIN [SMARTH_DB].[dbo].[GP12_ProductosEstados] E ON P.Referencia = E.Referencia INNER JOIN [SMARTH_DB].[dbo].[AUX_GP12_RazonRevision] R ON E.EstadoActual = R.Id INNER JOIN SMARTH_DB.DBO.AUX_Personal_Mandos D ON E.Responsable = D.Id WHERE P.[Referencia] = '" + referencia + "'", cnn_GP12);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                cnn_GP12.Close();
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                cnn_GP12.Close();
                return null;
            }
        }


        public DataTable leer_tablalistaestados()
        {
            try
            {
                cnn_GP12.Open();
                //SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT P.[Referencia] as REF,[Molde],[Descripcion],[Cliente],[EstadoActual],[Responsable],[Fecharev],[Fechaprevsalida],[EstadoAnterior],[Fechaestanterior],[Observaciones]FROM [SMARTH_DB].[dbo].[AUX_TablaProductos] P LEFT JOIN [SMARTH_DB].[dbo].[GP12_ProductosEstados] E ON P.Referencia = E.Referencia ORDER BY P.Referencia", cnn_GP12);
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT Id, Razon FROM [SMARTH_DB].[dbo].[AUX_GP12_RazonRevision] order by Id", cnn_GP12);
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
        public void actualizar_estado(int referencia, int estadoactual, int responsable, string fecharevision, string fechaprevsal, string Estadoanterior, string Fechaestanterior, string observaciones)
                                    
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                //string sql = "UPDATE dbo.AreaRechazo SET Referencia = " + referencia + ", Motivo = '" + motivo + "', ResponsableEntrada = '" + responsableEntrada + "', Cantidad = " + cantidad + ", FechaEntrada = CASE WHEN '" + fechaEntrada + "' = '' THEN NULL ELSE '" + fechaEntrada + "' END, FechaSalida = CASE WHEN '" + fechaSalida + "' = '' THEN NULL ELSE '" + fechaSalida + "' END, DebeSalir = CASE WHEN '" + debeSalir + "' = '' THEN NULL ELSE '" + debeSalir + "' END, Decision = '" + decision + "', ResponsableSalida = '" + responsableSalida + "', Observaciones = '" + observaciones + "' WHERE Id = " + id;
                string sql = "UPDATE [SMARTH_DB].[dbo].[GP12_ProductosEstados] SET EstadoActual = " + estadoactual + ", Responsable = " + responsable + ", Fecharev  = CASE WHEN '" + fecharevision + "' = '' THEN NULL ELSE '" + fecharevision + "' END, Fechaprevsalida = CASE WHEN '" + fechaprevsal + "' = '' THEN NULL ELSE '" + fechaprevsal + "' END, EstadoAnterior = '" + Estadoanterior + "', Fechaestanterior = CASE WHEN '" + Fechaestanterior + "' = '' THEN NULL ELSE '" + Fechaestanterior + "' END, Observaciones  = '" + observaciones + "' WHERE Referencia = " + referencia;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();
            }
            catch (Exception)
            {

            }
        }

       
        //CONECTORES PREVISION

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

        //CONECTORES PREVISION NAV

        public bool LimpiarTablaPrevisionesNAV()
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "DELETE FROM [SMARTH_DB].[dbo].[AUX_PrevisionGP12NAV]";
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
        public bool leer_previsionesNAV()
        {
            try
            {
                cnn_NAV.Open();
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;
                cmd.Connection = cnn_NAV;
                //FECHAS planificadas
                /*cmd.CommandText = "SELECT [Planned Shipment Date] AS FECHAENVIO, A.[No_] AS REFERENCIA, DEST.PLANTA AS PLANTA,[Outstanding Quantity] AS PARAENVIAR, CASE WHEN ALM.CANTALM IS NULL THEN 0 ELSE ALM.CANTALM END AS ALMACENGRAL, CASE WHEN CA.CANTALM IS NULL THEN 0 ELSE CA.CANTALM END AS GP12, CASE WHEN CHA.CANTALM IS NULL THEN 0 ELSE CHA.CANTALM END AS CHATARREAR FROM[NAVDB].[dbo].[THERMO$Sales Line] A" +
                    " LEFT JOIN(SELECT[No_], MAX([Ship-to Name]) AS PLANTA FROM[NAVDB].[dbo].[THERMO$Sales Header] GROUP BY No_) DEST ON A.[Document No_] = DEST.[No_]" +
                    " LEFT JOIN(SELECT[Item No_], sum ([Item Ledger Entry Quantity]) AS CANTALM FROM [NAVDB].[dbo].[THERMO$Value Entry] where[Location Code] <> 'CH' and[Location Code] <> 'CA' group by[Item No_]) ALM ON A.[No_] = ALM.[Item No_]" +
                    " LEFT JOIN(SELECT[Item No_], sum ([Item Ledger Entry Quantity]) AS CANTALM FROM [NAVDB].[dbo].[THERMO$Value Entry] where[Location Code] = 'CH' group by [Item No_]) CHA ON A.[No_] = CHA.[Item No_]" +
                    " LEFT JOIN(SELECT[Item No_], sum ([Item Ledger Entry Quantity]) AS CANTALM FROM [NAVDB].[dbo].[THERMO$Value Entry] where[Location Code] = 'CA' group by [Item No_]) CA ON A.[No_] = CA.[Item No_]" +
                    " where[Planned Delivery Date]  BETWEEN DATEADD(DAY,-30,SYSDATETIME()) AND DATEADD(DAY,+30,SYSDATETIME()) AND[Outstanding Quantity] > 0" +
                    " order by A.[Planned Shipment Date] asc";*/

                //FECHAS requeridas
                /*
                cmd.CommandText = "SELECT [Planned Shipment Date] AS FECHAENVIO, A.[No_] AS REFERENCIA, DEST.PLANTA AS PLANTA,[Outstanding Quantity] AS PARAENVIAR, CASE WHEN ALM.CANTALM IS NULL THEN 0 ELSE ALM.CANTALM END AS ALMACENGRAL, CASE WHEN CA.CANTALM IS NULL THEN 0 ELSE CA.CANTALM END AS GP12, CASE WHEN CHA.CANTALM IS NULL THEN 0 ELSE CHA.CANTALM END AS CHATARREAR FROM[NAVDB].[dbo].[THERMO$Sales Line] A" +
                    " LEFT JOIN(SELECT[No_], MAX([Ship-to Name]) AS PLANTA FROM[NAVDB].[dbo].[THERMO$Sales Header] GROUP BY No_) DEST ON A.[Document No_] = DEST.[No_]" +
                    " LEFT JOIN(SELECT[Item No_], sum ([Item Ledger Entry Quantity]) AS CANTALM FROM [NAVDB].[dbo].[THERMO$Value Entry] where[Location Code] <> 'CH' and[Location Code] <> 'CA' group by[Item No_]) ALM ON A.[No_] = ALM.[Item No_]" +
                    " LEFT JOIN(SELECT[Item No_], sum ([Item Ledger Entry Quantity]) AS CANTALM FROM [NAVDB].[dbo].[THERMO$Value Entry] where[Location Code] = 'CH' group by [Item No_]) CHA ON A.[No_] = CHA.[Item No_]" +
                    " LEFT JOIN(SELECT[Item No_], sum ([Item Ledger Entry Quantity]) AS CANTALM FROM [NAVDB].[dbo].[THERMO$Value Entry] where[Location Code] = 'CA' group by [Item No_]) CA ON A.[No_] = CA.[Item No_]" +
                    " where [Document No_] LIKE 'PV%' AND  [Document No_] NOT LIKE '000%' AND A.[No_] <> '_'  AND [Requested Delivery Date]  BETWEEN DATEADD(DAY,-30,SYSDATETIME()) AND DATEADD(DAY,+30,SYSDATETIME()) AND[Outstanding Quantity] > 0" +
                    " order by A.[Planned Shipment Date] asc";*/


                cmd.CommandText = "SELECT [Shipment Date] AS FECHAENVIO, A.[No_] AS REFERENCIA, DEST.PLANTA AS PLANTA,[Outstanding Quantity] AS PARAENVIAR, CASE WHEN ALM.CANTALM IS NULL THEN 0 ELSE ALM.CANTALM END AS ALMACENGRAL, CASE WHEN CA.CANTALM IS NULL THEN 0 ELSE CA.CANTALM END AS GP12, CASE WHEN CHA.CANTALM IS NULL THEN 0 ELSE CHA.CANTALM END AS CHATARREAR FROM[NAVDB].[dbo].[THERMO$Sales Line] A" +
                   " LEFT JOIN(SELECT[No_], MAX([Ship-to Name]) AS PLANTA FROM[NAVDB].[dbo].[THERMO$Sales Header] GROUP BY No_) DEST ON A.[Document No_] = DEST.[No_]" +
                   " LEFT JOIN(SELECT[Item No_], sum ([Item Ledger Entry Quantity]) AS CANTALM FROM [NAVDB].[dbo].[THERMO$Value Entry] where[Location Code] <> 'CH' and[Location Code] <> 'CA' group by[Item No_]) ALM ON A.[No_] = ALM.[Item No_]" +
                   " LEFT JOIN(SELECT[Item No_], sum ([Item Ledger Entry Quantity]) AS CANTALM FROM [NAVDB].[dbo].[THERMO$Value Entry] where[Location Code] = 'CH' group by [Item No_]) CHA ON A.[No_] = CHA.[Item No_]" +
                   " LEFT JOIN(SELECT[Item No_], sum ([Item Ledger Entry Quantity]) AS CANTALM FROM [NAVDB].[dbo].[THERMO$Value Entry] where[Location Code] = 'CA' group by [Item No_]) CA ON A.[No_] = CA.[Item No_]" +
                   " where  A.[No_] LIKE '6%' AND A.[No_] <> '_'  AND [Requested Delivery Date]  BETWEEN DATEADD(DAY,-30,SYSDATETIME()) AND DATEADD(DAY,+30,SYSDATETIME()) AND[Outstanding Quantity] > 0" +
                   " order by A.[Shipment Date] asc";

                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    insertar_previsionesNAV(Convert.ToInt32(reader["REFERENCIA"]), Convert.ToDateTime(reader["FECHAENVIO"]), Convert.ToInt32(reader["PARAENVIAR"]), reader["PLANTA"].ToString(), Convert.ToInt32(reader["ALMACENGRAL"]), Convert.ToInt32(reader["GP12"]), Convert.ToInt32(reader["CHATARREAR"]));
                }
                cnn_NAV.Close();
                return true;
            }
            catch (Exception EX)
            {

                cnn_NAV.Close();
                return false;
            }
        }
        public bool insertar_previsionesNAV(int articulo, DateTime entrega, int cantidad, string planta, int almacengral, int almacengp12, int chatarra)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql2 = "INSERT INTO [SMARTH_DB].[dbo].[AUX_PrevisionGP12NAV] (Referencia, FechaEntrega, planta, cantidad, almacengral, gp12, chatarra) VALUES " +
                                 "(" + articulo + ",convert(datetime,'" + entrega + "'),'" + planta + "'," + cantidad + ","+almacengral+","+almacengp12+","+chatarra+" )";
                cmd.CommandText = sql2;
                cmd.ExecuteNonQuery();
                cnn_GP12.Close();
                return true;
            }
            catch (Exception EX)
            {
                cnn_GP12.Close();
                return false;
            }
        }

        public DataTable Lee_grid_previsionNAV_SMARTH(string revision, string fecha)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT *, CASE WHEN PZHORA = 0 THEN 0.0 ELSE CONVERT(DECIMAL(10,2),(PDTEREVISAR / PZHORA),2) END AS HORASNECESARIAS, CASE WHEN RequiereMontaje = 1 THEN 'MONTAJE | ' + Razon ELSE '' + Razon END AS RAZONMONT" +
                        " FROM(SELECT PRV.[Referencia], PRO.Descripcion, [FechaEntrega],[planta], RZN.Id, RZN.Razon, SUM([cantidad]) AS ENTREGAR, SUM([almacengral]) AS ALMACEN, SUM([GP12]) AS GP12, " +
                        " CASE WHEN (SUM([cantidad]) - SUM([almacengral])) < 0 THEN 0 ELSE (SUM([cantidad]) - SUM([almacengral])) END as PDTEREVISAR, " +
                        " CASE WHEN PZHORA IS NULL THEN 0 ELSE PZHORA END AS PZHORA, EST.[RequiereMontaje]" +
                        " FROM[SMARTH_DB].[dbo].[AUX_PrevisionGP12NAV] PRV" +
                        " LEFT JOIN[SMARTH_DB].[dbo].[AUX_TablaProductos] PRO ON PRV.Referencia = PRO.Referencia" +
                        " LEFT JOIN[SMARTH_DB].[dbo].[GP12_ProductosEstados] EST ON PRV.Referencia = EST.Referencia" +
                        " LEFT JOIN[SMARTH_DB].[dbo].[AUX_GP12_RazonRevision] RZN ON EST.EstadoActual = RZN.Id" +
                        " LEFT JOIN(SELECT HIS.Referencia, SUM(HorasInspeccion) AS HORAS, SUM(PiezasRevisadas) as Piezas, CAST(SUM(PiezasRevisadas) / SUM(HorasInspeccion) AS numeric(36, 2)) AS PZHORA" +
                    " FROM(SELECT[Referencia],[HorasInspeccion],[PiezasRevisadas] FROM[SMARTH_DB].[dbo].[GP12_Historico_2021] WHERE HorasInspeccion > 0.3) HIS" +
                    " where Referencia is not null and Referencia <> '1' and PiezasRevisadas is not null" +
                    " GROUP BY Referencia) PZH ON PRV.Referencia = PZH.Referencia" +
                    " GROUP BY FechaEntrega, PRV.Referencia, PRO.Descripcion, RZN.Id, RZN.Razon, planta, PZHORA, EST.[RequiereMontaje]) SQ" +
                    " WHERE referencia <> ''"+revision+" " + fecha+""; 
                    //id <> 0 sin revision
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

        public DataTable Lee_totales_grid_previsionNAV_SMARTH(string revision, string fecha)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT FechaEntrega, COUNT(Referencia) AS Productos, SUM(ENTREGAR) AS TotalEntregar, SUM(HORASNECESARIAS) AS TotalHoras FROM("+
                            " SELECT *, CASE WHEN PZHORA = 0 THEN 0.0 ELSE CONVERT(DECIMAL(10,2),(PDTEREVISAR / PZHORA),2) END AS HORASNECESARIAS" +
                                " FROM(SELECT PRV.[Referencia], PRO.Descripcion, [FechaEntrega],[planta], RZN.Id, RZN.Razon, SUM([cantidad]) AS ENTREGAR, SUM([almacengral]) AS ALMACEN, CASE WHEN (SUM([cantidad]) - SUM([almacengral])) < 0 THEN 0 ELSE (SUM([cantidad]) - SUM([almacengral])) END as PDTEREVISAR,"+ 
                                " SUM([GP12]) AS GP12, CASE WHEN PZHORA IS NULL THEN 0 ELSE PZHORA END AS PZHORA" +
                                " FROM[SMARTH_DB].[dbo].[AUX_PrevisionGP12NAV] PRV" +
                                " LEFT JOIN[SMARTH_DB].[dbo].[AUX_TablaProductos] PRO ON PRV.Referencia = PRO.Referencia" +
                                " LEFT JOIN[SMARTH_DB].[dbo].[GP12_ProductosEstados] EST ON PRV.Referencia = EST.Referencia" +
                                " LEFT JOIN[SMARTH_DB].[dbo].[AUX_GP12_RazonRevision] RZN ON EST.EstadoActual = RZN.Id" +
                                " LEFT JOIN(SELECT HIS.Referencia, SUM(HorasInspeccion) AS HORAS, SUM(PiezasRevisadas) as Piezas, CAST(SUM(PiezasRevisadas) / SUM(HorasInspeccion) AS numeric(36, 2)) AS PZHORA" +
                    " FROM(SELECT[Referencia],[HorasInspeccion],[PiezasRevisadas] FROM[SMARTH_DB].[dbo].[GP12_Historico_2021] WHERE HorasInspeccion > 0.3) HIS" +
                    " where Referencia is not null and Referencia <> '1' and PiezasRevisadas is not null" +
                    " GROUP BY Referencia) PZH ON PRV.Referencia = PZH.Referencia" +
                    " GROUP BY FechaEntrega, PRV.Referencia, PRO.Descripcion, RZN.Id, RZN.Razon, planta, PZHORA) SQ" +
                    " WHERE Referencia <> ''"+revision+"" + fecha+") GRL GROUP BY FechaEntrega ORDER BY FechaEntrega ASC"; 
                    //id <> 0 sin revision
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

        public DataTable Devuelve_horasGP12_X_Producto()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT CAST(HIS.Referencia as varchar) as Referencia, SUM(HorasInspeccion) AS HORAS, SUM(PiezasRevisadas) as Piezas, CAST(SUM(PiezasRevisadas) / SUM(HorasInspeccion) AS numeric(36, 2)) AS PZHORA"+
                                    " FROM(SELECT[Referencia],[HorasInspeccion],[PiezasRevisadas] FROM[SMARTH_DB].[dbo].[GP12_Historico_2021] WHERE HorasInspeccion > 0.3) HIS"+
                                    " where Referencia is not null and Referencia <> '1' and PiezasRevisadas is not null"+
                                    " GROUP BY Referencia";
                //id <> 0 sin revision
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


        public DataSet lee_grid_previsionNAV()
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT P.[Referencia],R.Descripcion,[FechaEntrega],[cantidad],P.planta, P.almacengral, P.gp12, P.chatarra, CASE WHEN Q.[RequiereMontaje] = 1 THEN 'MONTAJE | ' ELSE '' +  N.Razon END AS Razon " +
                                " FROM [SMARTH_DB].[dbo].[AUX_PrevisionGP12NAV] P " +
                                " LEFT JOIN [SMARTH_DB].[dbo].[GP12_ProductosEstados] Q ON P.[Referencia] = Q.Referencia " +
                                " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] R ON P.Referencia = R.Referencia " +
                                " LEFT JOIN [SMARTH_DB].[dbo].[AUX_GP12_RazonRevision] N ON EstadoActual = N.Id " +
                                " WHERE (Q.EstadoActual > 0 OR Q.[RequiereMontaje] = 1) and FechaEntrega >= DATEADD(DAY,-1,SYSDATETIME()) " +
                                //" WHERE FechaEntrega >= DATEADD(DAY,-1,SYSDATETIME()) " +

                                " ORDER BY FechaEntrega ASC";
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

        public DataSet lee_grid_prevision_filtradoNAV(string fecha)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT P.[Referencia],R.Descripcion,[FechaEntrega],[cantidad],P.planta, P.almacengral, P.gp12, P.chatarra, CASE WHEN Q.[RequiereMontaje] = 1 THEN 'MONTAJE | ' ELSE '' +  N.Razon END AS Razon" +
                             " FROM [SMARTH_DB].[dbo].[AUX_PrevisionGP12NAV] P " +
                             " LEFT JOIN [SMARTH_DB].[dbo].[GP12_ProductosEstados] Q ON P.[Referencia] = Q.Referencia" +
                             " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] R ON P.Referencia = R.Referencia" +
                             " LEFT JOIN [SMARTH_DB].[dbo].[AUX_GP12_RazonRevision] N ON EstadoActual = N.Id " +
                             " WHERE (Q.EstadoActual > 0 OR Q.[RequiereMontaje] = 1) AND FechaEntrega like '" + fecha + "%'" +
                             " ORDER BY FechaEntrega ASC";
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

        public DataSet lee_grid_prevision_atrasadasNAV()
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "SELECT P.[Referencia],R.Descripcion,[FechaEntrega],[cantidad],P.planta, P.almacengral, P.gp12, P.chatarra, CASE WHEN Q.[RequiereMontaje] = 1 THEN 'MONTAJE | ' ELSE '' +  N.Razon END AS Razon" +
                    " FROM [SMARTH_DB].[dbo].[AUX_PrevisionGP12NAV] P " +
                    " LEFT JOIN [SMARTH_DB].[dbo].[GP12_ProductosEstados] Q ON P.[Referencia] = Q.Referencia " +
                    " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] R ON P.Referencia = R.Referencia " +
                    " LEFT JOIN [SMARTH_DB].[dbo].[AUX_GP12_RazonRevision] N ON EstadoActual = N.Id " +
                    " WHERE (Q.EstadoActual > 0 OR Q.[RequiereMontaje] = 1) and FechaEntrega >= DATEADD(DAY,-30,SYSDATETIME()) and FechaEntrega <= DATEADD(DAY,-1,SYSDATETIME())" +
                    " ORDER BY FechaEntrega ASC";
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

        //CONECTORES DE INFORME
        public DataSet devuelve_ultimas_revisionesNAV()
        {
            try
            {
                cnn_GP12.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT TOP 600 C.ID, [FechaInicio],CAST([HorasInspeccion] AS varchar) + ' h' AS HORAS,P.Proveedor,[OperarioRevisionINT],[OperarioRevision],C.Referencia,[Nombre],C.Molde,[Nlote],[Ncaja],[PiezasRevisadas]," +
                "[PiezasOK],[Retrabajadas],[PiezasNOK],[Def1],[Def2],[Def3],[Def4],[Def5],[Def6],[Def7],[Def8],[Def9],[Def10],[Def11],[Def12],[Def13],[Def14],[Def15],[Def16],[Def17]," +
                "[Def18],[Def19],[Def20],[Def21],[Def22],[Def23],[Def24],[Def25],[Def26],[Def27],[Def28],[Def29],[Def30],[Incidencias],[ImagenDefecto1],[ImagenDefecto2]," +
                "[ImagenDefecto3],[Notas],[NOp1],[NOp2],[NOp3],[NOp4],R.Razon,CAST([CosteRevision] AS varchar)+' €' AS CosteRevision,CAST([CostePiezaRevision] AS varchar)+' €' AS CostePiezaRevision,CAST([CosteScrapRevision] AS varchar)+' €' AS CosteScrapRevision, M.Cliente, M.[FakeMode]" +
                " FROM [SMARTH_DB].[dbo].[GP12_Historico_2021] C LEFT JOIN [SMARTH_DB].[dbo].[GP12_ProveedoresRevision] P ON C.ProveedorRevision = P.Id LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] M ON C.Referencia = M.Referencia" +
                " LEFT JOIN [SMARTH_DB].[dbo].[AUX_GP12_RazonRevision] R ON C.RazonRevision = R.Id ORDER BY FechaInicio DESC", cnn_GP12);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                cnn_GP12.Close();
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                cnn_GP12.Close();
                return null;
            }
        }

        public DataSet devuelve_ultimas_revisiones_filtradas(string referencia, string molde, string lote, string cliente, int razon, string fecha)
        {
            try
            {
                cnn_GP12.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT TOP 600 C.ID, [FechaInicio],CAST([HorasInspeccion] AS varchar) + ' h' AS HORAS,P.Proveedor,[OperarioRevisionINT],[OperarioRevision],C.Referencia,[Nombre],C.Molde,[Nlote],[Ncaja],[PiezasRevisadas]," +
                "[PiezasOK],[Retrabajadas],[PiezasNOK],[Def1],[Def2],[Def3],[Def4],[Def5],[Def6],[Def7],[Def8],[Def9],[Def10],[Def11],[Def12],[Def13],[Def14],[Def15],[Def16],[Def17],"+
	            "[Def18],[Def19],[Def20],[Def21],[Def22],[Def23],[Def24],[Def25],[Def26],[Def27],[Def28],[Def29],[Def30],[Incidencias],[ImagenDefecto1],[ImagenDefecto2],"+
                "[ImagenDefecto3],[Notas],[NOp1],[NOp2],[NOp3],[NOp4],R.Razon,CAST([CosteRevision] AS varchar)+' €' AS CosteRevision,CAST([CostePiezaRevision] AS varchar)+' €' AS CostePiezaRevision,CAST([CosteScrapRevision] AS varchar)+' €' AS CosteScrapRevision, M.Cliente, M.[FakeMode]" +
                " FROM [SMARTH_DB].[dbo].[GP12_Historico_2021] C LEFT JOIN [SMARTH_DB].[dbo].[GP12_ProveedoresRevision] P ON C.ProveedorRevision = P.Id LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] M ON C.Referencia = M.Referencia AND M.Cliente LIKE '"+cliente+"%'"+
                " LEFT JOIN [SMARTH_DB].[dbo].[AUX_GP12_RazonRevision] R ON C.RazonRevision = R.Id and C.RazonRevision = '" + razon + "' WHERE C.Referencia like '" + referencia + "%' and M.Molde like '" + molde + "%' and C.Nlote like '" + lote + "%' and CONVERT(VARCHAR(25),FechaInicio,126) like '" + fecha + "%' ORDER BY FechaInicio DESC", cnn_GP12);  
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                cnn_GP12.Close();
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                cnn_GP12.Close();
                return null;
            }
        }

        public DataSet modal_detalles_ultimas_revisiones(string idrevision)
        {
            try
            {
                cnn_GP12.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT C.ID, [FechaInicio],CAST([HorasInspeccion] AS varchar) + ' h' AS HORAS,P.Proveedor,[OperarioRevisionINT],[OperarioRevision],C.Referencia,[Nombre],C.Molde,[Nlote],[Ncaja],[PiezasRevisadas]," +
                "[PiezasOK],[Retrabajadas],[PiezasNOK],[Def1],[Def2],[Def3],[Def4],[Def5],[Def6],[Def7],[Def8],[Def9],[Def10],[Def11],[Def12],[Def13],[Def14],[Def15],[Def16],[Def17],"+
	            "[Def18],[Def19],[Def20],[Def21],[Def22],[Def23],[Def24],[Def25],[Def26],[Def27],[Def28],[Def29],[Def30],[Incidencias],[ImagenDefecto1],[ImagenDefecto2],"+
                "[ImagenDefecto3],[Notas],C.NOp1, O.Operario AS OPERARIO1,C.NOp2, O2.Operario AS OPERARIO2,C.NOp3, O3.Operario AS OPERARIO3,C.NOp4, O4.Operario AS OPERARIO4,R.Razon,CAST([CosteRevision] AS varchar)+' €' AS CosteRevision,CAST([CostePiezaRevision] AS varchar)+' €' AS CostePiezaRevision,CAST([CosteScrapRevision] AS varchar)+' €' AS CosteScrapRevision, M.Cliente," +
                "[FechaInfo1],[Informador1],[FechaInfo2],[Informador2],[FechaInfo3],[Informador3],[FechaInfo4],[Informador4],[FeedbackOPERARIOS], M.[FakeMode]" +
                " FROM [SMARTH_DB].[dbo].[GP12_Historico_2021] C LEFT JOIN [SMARTH_DB].[dbo].[GP12_ProveedoresRevision] P ON C.ProveedorRevision = P.Id LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] M ON C.Referencia = M.Referencia"+
                " LEFT JOIN [SMARTH_DB].[dbo].[AUX_GP12_RazonRevision] R ON C.RazonRevision = R.Id LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] O on C.NOp1 = O.Id"+ 
				" LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] O2 on C.NOp2 = O2.Id LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] O3 on C.NOp3 = O3.Id"+
				" LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] O4 on C.NOp4 = O4.Id where C.ID = "+idrevision+" ORDER BY FechaInicio DESC", cnn_GP12);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                cnn_GP12.Close();
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                cnn_GP12.Close();
                return null;
            }
        }

       
        //CONECTORES DE HISTORICO

        //CONECTORES DE HISTORICO POR REFERENCIA
        public DataSet devuelve_detalle_revisiones_referencia(string referencia)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT C.ID AS ID, [FechaInicio], HorasInspeccion AS DoubleHoras, CAST([HorasInspeccion] AS varchar) + ' h' AS HORAS, P.Proveedor,[OperarioRevisionINT],[OperarioRevision],C.Referencia,[Nombre],C.Molde,[Nlote],[Ncaja],[PiezasRevisadas],[PiezasOK],[Retrabajadas],[PiezasNOK],[Def1],[Def2],[Def3],[Def4],[Def5],[Def6],[Def7],[Def8],[Def9],[Def10],[Def11],[Def12],[Def13],[Def14],[Def15],[Def16],[Def17],[Def18],[Def19],[Def20],[Def21],[Def22],[Def23],[Def24],[Def25],[Def26],[Def27],[Def28],[Def29],[Def30],[Incidencias],[ImagenDefecto1],[ImagenDefecto2],[ImagenDefecto3],[Notas],[NOp1],[NOp2],[NOp3],[NOp4],R.Razon,CosteRevision, CAST([CosteRevision] AS varchar)+' €' AS CosteRevisionCHAR, CostePiezaRevision, CAST([CostePiezaRevision] AS varchar)+' €' AS CostePiezaRevisionCHAR, CosteScrapRevision, CAST([CosteScrapRevision] AS varchar)+' €' AS CosteScrapRevisionCHAR, M.Cliente, M.Molde, M.FakeMode FROM[SMARTH_DB].[dbo].[GP12_Historico_2021] C LEFT JOIN[SMARTH_DB].[dbo].[GP12_ProveedoresRevision] P ON C.ProveedorRevision = P.Id LEFT JOIN[SMARTH_DB].[dbo].[AUX_TablaProductos] M ON C.Referencia = M.Referencia LEFT JOIN[SMARTH_DB].[dbo].[AUX_GP12_RazonRevision] R ON C.RazonRevision = R.Id  WHERE C.Referencia LIKE '" + referencia + "%'"+
                                                                " UNION ALL SELECT C.ID AS ID, [FechaInicio],HorasInspeccion AS DoubleHoras, CAST([HorasInspeccion] AS varchar) +' h' AS HORAS, P.Proveedor,[OperarioRevisionINT],[OperarioRevision],C.Referencia,[Nombre],C.Molde,[Nlote],[Ncaja],[PiezasRevisadas],[PiezasOK],[Retrabajadas],[PiezasNOK],[Def1],[Def2],[Def3],[Def4],[Def5],[Def6],[Def7],[Def8],[Def9],[Def10],[Def11],[Def12],[Def13],[Def14],[Def15],[Def16],[Def17],[Def18],[Def19],[Def20],[Def21],[Def22],[Def23],[Def24],[Def25],[Def26],[Def27],[Def28],[Def29],[Def30],[Incidencias],[ImagenDefecto1],[ImagenDefecto2],[ImagenDefecto3],[Notas],[NOp1],[NOp2],[NOp3],[NOp4],R.Razon,CosteRevision, CAST([CosteRevision] AS varchar)+' €' AS CosteRevisionCHAR, CostePiezaRevision, CAST([CostePiezaRevision] AS varchar)+' €' AS CostePiezaRevisionCHAR, CosteScrapRevision, CAST([CosteScrapRevision] AS varchar)+' €' AS CosteScrapRevisionCHAR, M.Cliente, M.Molde, M.FakeMode FROM[SMARTH_DB].[dbo].[GP12_Historico] C LEFT JOIN[SMARTH_DB].[dbo].[GP12_ProveedoresRevision] P ON C.ProveedorRevision = P.Id LEFT JOIN[SMARTH_DB].[dbo].[AUX_TablaProductos] M ON C.Referencia = M.Referencia LEFT JOIN[SMARTH_DB].[dbo].[AUX_GP12_RazonRevision] R ON C.RazonRevision = R.Id  WHERE C.Referencia LIKE '" + referencia + "%'"+
                                                                " ORDER BY FechaInicio DESC", cnn_SMARTH);
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

     
        public DataSet devuelve_detalle_revisiones_referencia_filtradasSMARTH(string referencia, string lote, string razon, string fecha, string cliente, string molde)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT Referencia,Nombre, Cliente, SUM(HORAS) as HORAS, SUM([PiezasRevisadas]) as PiezasRevisadas,SUM([PiezasOK]) as PiezasOK,SUM([Retrabajadas]) as Retrabajadas,SUM([PiezasNOK]) as PiezasNOK,CASE WHEN SUM([PiezasNOK]) <> 0 THEN CAST(SUM([PiezasNOK]) as decimal(9,2))/CAST(SUM([PiezasRevisadas]) as decimal(9,2)) ELSE 0 END AS PORCSCRAP,SUM([Def1]) as Def1,SUM([Def2]) as Def2,SUM([Def3]) as Def3,SUM([Def4])  as Def4,SUM([Def5])  as Def5,SUM([Def6])  as Def6,SUM([Def7])  as Def7,SUM([Def8])  as Def8,SUM([Def9])  as Def9,SUM([Def10])  as Def10,SUM([Def11]) as Def11,SUM([Def12])  as Def12,SUM([Def13])  as Def13,SUM([Def14]) as Def14,SUM([Def15]) as Def15,SUM([Def16]) as Def16,SUM([Def17]) as Def17,SUM([Def18]) as Def18,SUM([Def19]) as Def19,SUM([Def20]) as Def20,SUM([Def21]) as Def21,SUM([Def22]) as Def22,SUM([Def23]) as Def23,SUM([Def24]) as Def24,SUM([Def25]) as Def25,SUM([Def26]) as Def26,SUM([Def27]) as Def27,SUM([Def28]) as Def28,SUM([Def29]) as Def29,SUM([Def30]) as Def30,SUM(CosteRevision) as CosteRevision, SUM(CostePiezaRevision) as CostePiezaRevision, SUM(CosteScrapRevision) as CosteScrapRevision" +
                            " FROM(SELECT[HorasInspeccion] AS HORAS, C.Referencia,[Nombre],[PiezasRevisadas],[PiezasOK],[Retrabajadas],[PiezasNOK],[Def1],[Def2],[Def3],[Def4],[Def5],[Def6],[Def7],[Def8],[Def9],[Def10],[Def11],[Def12],[Def13],[Def14],[Def15],[Def16],[Def17],[Def18],[Def19],[Def20],[Def21],[Def22],[Def23],[Def24],[Def25],[Def26],[Def27],[Def28],[Def29],[Def30], CosteRevision, CostePiezaRevision, CosteScrapRevision, M.Cliente FROM[SMARTH_DB].[dbo].[GP12_Historico_2021] C LEFT JOIN[SMARTH_DB].[dbo].[GP12_ProveedoresRevision] P ON C.ProveedorRevision = P.Id LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] M ON C.Referencia = M.Referencia LEFT JOIN[SMARTH_DB].[dbo].[AUX_GP12_RazonRevision] R ON C.RazonRevision = R.Id WHERE M.Cliente LIKE '"+ cliente + "%' AND C.Referencia LIKE '" + referencia + "%' AND Nlote LIKE '" + lote + "%' AND Razon LIKE '" + razon + "%' AND CONVERT(VARCHAR(25), FechaInicio, 126) LIKE '" + fecha + "%' AND M.Molde LIKE '"+molde+"%'" +
                            " UNION ALL SELECT[HorasInspeccion] AS HORAS, C.Referencia,[Nombre],[PiezasRevisadas],[PiezasOK],[Retrabajadas],[PiezasNOK],[Def1],[Def2],[Def3],[Def4],[Def5],[Def6],[Def7],[Def8],[Def9],[Def10],[Def11],[Def12],[Def13],[Def14],[Def15],[Def16],[Def17],[Def18],[Def19],[Def20],[Def21],[Def22],[Def23],[Def24],[Def25],[Def26],[Def27],[Def28],[Def29],[Def30], CosteRevision, CostePiezaRevision, CosteScrapRevision, M.Cliente FROM[SMARTH_DB].[dbo].[GP12_Historico] C LEFT JOIN[SMARTH_DB].[dbo].[GP12_ProveedoresRevision] P ON C.ProveedorRevision = P.Id LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] M ON C.Referencia = M.Referencia LEFT JOIN[SMARTH_DB].[dbo].[AUX_GP12_RazonRevision] R ON C.RazonRevision = R.Id WHERE M.Cliente LIKE '"+cliente+"%' AND C.Referencia LIKE '" + referencia + "%' AND Nlote LIKE '" + lote + "%' AND Razon LIKE '" + razon + "%' AND CONVERT(VARCHAR(25), FechaInicio, 126) LIKE '" + fecha + "%'  AND M.Molde LIKE '"+molde+"%')M" +
                            " GROUP BY Referencia, Nombre, Cliente" +
                            " ORDER BY Referencia DESC", cnn_SMARTH);
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

        public DataSet modal_detalles_ultimas_revisiones_referencia(string idrevision)
        {
            try
            {
                cnn_GP12.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM"+
                            " (SELECT C.ID AS ID, HorasInspeccion AS DoubleHoras, [FechaInicio], CAST([HorasInspeccion] AS varchar) + ' h' AS HORAS, P.Proveedor,[OperarioRevisionINT],[OperarioRevision], C.Referencia,[Nombre], C.Molde,[Nlote],[Ncaja],[PiezasRevisadas], [PiezasOK],[Retrabajadas],[PiezasNOK],[Def1],[Def2],[Def3],[Def4],[Def5],[Def6],[Def7],[Def8],[Def9],[Def10],[Def11],[Def12],[Def13],[Def14],[Def15],[Def16],[Def17], [Def18],[Def19],[Def20],[Def21],[Def22],[Def23],[Def24],[Def25],[Def26],[Def27],[Def28],[Def29],[Def30],[Incidencias],[ImagenDefecto1],[ImagenDefecto2],"+
                            " [ImagenDefecto3],[Notas], C.NOp1, O.Operario AS OPERARIO1, C.NOp2, O2.Operario AS OPERARIO2, C.NOp3, O3.Operario AS OPERARIO3, C.NOp4, O4.Operario AS OPERARIO4, R.Razon, CAST([CosteRevision] AS varchar) + ' €' AS CosteRevision, CAST([CostePiezaRevision] AS varchar)+' €' AS CostePiezaRevision, CAST([CosteScrapRevision] AS varchar)+' €' AS CosteScrapRevision, M.Cliente, M.FakeMode FROM[SMARTH_DB].[dbo].[GP12_Historico_2021] C LEFT JOIN[SMARTH_DB].[dbo].[GP12_ProveedoresRevision] P ON C.ProveedorRevision = P.Id LEFT JOIN[SMARTH_DB].[dbo].[AUX_TablaProductos] M ON C.Referencia = M.Referencia LEFT JOIN[SMARTH_DB].[dbo].[AUX_GP12_RazonRevision] R ON C.RazonRevision = R.Id LEFT JOIN[SMARTH_DB].[dbo].[AUX_TablaOperarios] O on C.NOp1 = O.Id LEFT JOIN[SMARTH_DB].[dbo].[AUX_TablaOperarios] O2 on C.NOp2 = O2.Id LEFT JOIN[SMARTH_DB].[dbo].[AUX_TablaOperarios] O3 on C.NOp3 = O3.Id LEFT JOIN[SMARTH_DB].[dbo].[AUX_TablaOperarios] O4 on C.NOp4 = O4.Id"+
                            " UNION ALL SELECT C.ID AS ID, HorasInspeccion AS DoubleHoras, [FechaInicio],CAST([HorasInspeccion] AS varchar) +' h' AS HORAS, P.Proveedor,[OperarioRevisionINT],[OperarioRevision],C.Referencia,[Nombre],C.Molde,[Nlote],[Ncaja],[PiezasRevisadas], [PiezasOK],[Retrabajadas],[PiezasNOK],[Def1],[Def2],[Def3],[Def4],[Def5],[Def6],[Def7],[Def8],[Def9],[Def10],[Def11],[Def12],[Def13],[Def14],[Def15],[Def16],[Def17], [Def18],[Def19],[Def20],[Def21],[Def22],[Def23],[Def24],[Def25],[Def26],[Def27],[Def28],[Def29],[Def30],[Incidencias],[ImagenDefecto1],[ImagenDefecto2],"+
                            " [ImagenDefecto3],[Notas],C.NOp1, O.Operario AS OPERARIO1,C.NOp2, O2.Operario AS OPERARIO2,C.NOp3, O3.Operario AS OPERARIO3,C.NOp4, O4.Operario AS OPERARIO4,R.Razon,CAST([CosteRevision] AS varchar)+' €' AS CosteRevision, CAST([CostePiezaRevision] AS varchar)+' €' AS CostePiezaRevision, CAST([CosteScrapRevision] AS varchar)+' €' AS CosteScrapRevision, M.Cliente, M.FakeMode FROM[SMARTH_DB].[dbo].[GP12_Historico] C LEFT JOIN[SMARTH_DB].[dbo].[GP12_ProveedoresRevision] P ON C.ProveedorRevision = P.Id LEFT JOIN[SMARTH_DB].[dbo].[AUX_TablaProductos] M ON C.Referencia = M.Referencia LEFT JOIN[SMARTH_DB].[dbo].[AUX_GP12_RazonRevision] R ON C.RazonRevision = R.Id LEFT JOIN[SMARTH_DB].[dbo].[AUX_TablaOperarios] O on C.NOp1 = O.Id LEFT JOIN[SMARTH_DB].[dbo].[AUX_TablaOperarios] O2 on C.NOp2 = O2.Id LEFT JOIN[SMARTH_DB].[dbo].[AUX_TablaOperarios] O3 on C.NOp3 = O3.Id LEFT JOIN[SMARTH_DB].[dbo].[AUX_TablaOperarios] O4 on C.NOp4 = O4.Id) S"+
                            " where S.ID = " + idrevision+""+
				             " ORDER BY FechaInicio DESC", cnn_GP12);
                
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                cnn_GP12.Close();
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                cnn_GP12.Close();
                return null;
            }
        }

        public DataSet devuelve_historico_BMS(string referencia)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT S.C_PRODUCT,S.PRODUCIDAS,S.RECHAZADAS,TRUNC((S.TIEMPOMINUTOS/1440),2)  AS TIEMPOHORAS,S.PRECIOSCRAP,S.PRECIO"+
                             " FROM (SELECT C_PRODUCT, SUM(C_PRODUCTIONCOUNT02) AS PRODUCIDAS, SUM (C_COUNTTOTAL) AS RECHAZADAS, SUM(C_AVAILABLETIME) AS TIEMPOMINUTOS,"+
                             "SUM ((C_COUNTTOTAL*C_CUSTOMVALUE2)) AS PRECIOSCRAP, MAX(C_CUSTOMVALUE2) AS PRECIO FROM HIS.T_HISJOBS WHERE C_PRODUCT = '"+referencia+"' GROUP BY C_PRODUCT) S";                   
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

        public DataSet devuelve_detalle_revisiones_referencia_operario(int operario)
        {
            try
            {
                cnn_GP12.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT C.ID AS ID, [FechaInicio],HorasInspeccion AS DoubleHoras, CAST([HorasInspeccion] AS varchar) + ' h' AS HORAS,P.Proveedor,[OperarioRevisionINT],[OperarioRevision],C.Referencia,[Nombre],C.Molde,[Nlote],[Ncaja],[PiezasRevisadas],[PiezasOK],[Retrabajadas],[PiezasNOK],[Def1],[Def2],[Def3],[Def4],[Def5],[Def6],[Def7],[Def8],[Def9],[Def10],[Def11],[Def12],[Def13],[Def14],[Def15],[Def16],[Def17],[Def18],[Def19],[Def20],[Def21],[Def22],[Def23],[Def24],[Def25],[Def26],[Def27],[Def28],[Def29],[Def30],[Incidencias],[ImagenDefecto1],[ImagenDefecto2],[ImagenDefecto3],[Notas],[NOp1],[NOp2],[NOp3],[NOp4],R.Razon,CosteRevision, CAST([CosteRevision] AS varchar)+' €' AS CosteRevisionCHAR,CostePiezaRevision, CAST([CostePiezaRevision] AS varchar)+' €' AS CostePiezaRevisionCHAR,CosteScrapRevision, CAST([CosteScrapRevision] AS varchar)+' €' AS CosteScrapRevisionCHAR, M.Cliente, M.FakeMode FROM [SMARTH_DB].[dbo].[GP12_Historico_2021] C LEFT JOIN [SMARTH_DB].[dbo].[GP12_ProveedoresRevision] P ON C.ProveedorRevision = P.Id LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] M ON C.Referencia = M.Referencia LEFT JOIN [SMARTH_DB].[dbo].[AUX_GP12_RazonRevision] R ON C.RazonRevision = R.Id  WHERE NOp1 = " + operario + " OR NOp2 = " + operario + " OR NOp3 = " + operario + " OR NOp4 = " + operario + "" +
                                     " UNION ALL SELECT C.ID AS ID, [FechaInicio],HorasInspeccion AS DoubleHoras, CAST([HorasInspeccion] AS varchar) + ' h' AS HORAS,P.Proveedor,[OperarioRevisionINT],[OperarioRevision],C.Referencia,[Nombre],C.Molde,[Nlote],[Ncaja],[PiezasRevisadas],[PiezasOK],[Retrabajadas],[PiezasNOK],[Def1],[Def2],[Def3],[Def4],[Def5],[Def6],[Def7],[Def8],[Def9],[Def10],[Def11],[Def12],[Def13],[Def14],[Def15],[Def16],[Def17],[Def18],[Def19],[Def20],[Def21],[Def22],[Def23],[Def24],[Def25],[Def26],[Def27],[Def28],[Def29],[Def30],[Incidencias],[ImagenDefecto1],[ImagenDefecto2],[ImagenDefecto3],[Notas],[NOp1],[NOp2],[NOp3],[NOp4],R.Razon,CosteRevision, CAST([CosteRevision] AS varchar)+' €' AS CosteRevisionCHAR,CostePiezaRevision, CAST([CostePiezaRevision] AS varchar)+' €' AS CostePiezaRevisionCHAR,CosteScrapRevision, CAST([CosteScrapRevision] AS varchar)+' €' AS CosteScrapRevisionCHAR, M.Cliente, M.FakeMode FROM [SMARTH_DB].[dbo].[GP12_Historico] C LEFT JOIN [SMARTH_DB].[dbo].[GP12_ProveedoresRevision] P ON C.ProveedorRevision = P.Id LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaProductos] M ON C.Referencia = M.Referencia LEFT JOIN [SMARTH_DB].[dbo].[AUX_GP12_RazonRevision] R ON C.RazonRevision = R.Id  WHERE NOp1 = " + operario + " OR NOp2 = " + operario + " OR NOp3 = " + operario + " OR NOp4 = " + operario + "" +
                                     " ORDER BY FechaInicio DESC", cnn_GP12);

                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                cnn_GP12.Close();
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                cnn_GP12.Close();
                return null;
            }
        }

        //CONECTORES KPIS GP12

        public DataSet devuelve_KPI_GP12_Mensual(string año)
        {
            try
            {
                string db = "[SMARTH_DB].[dbo].[GP12_Historico_2021]";
                if (Convert.ToInt32(año) < 2021)
                {
                    db = "[SMARTH_DB].[dbo].[GP12_Historico]";
                }
                cnn_GP12.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT DateName( month , DateAdd( month , S.MES , 0 ) - 1 ) AS 'MESTEXTO', HORAS, B.Cantidad, REVISADAS, (CAST(REVISADAS AS DECIMAL)/CAST(b.Cantidad AS DECIMAL)) AS PORCREVI, PIEZASOK, RETRABAJADAS, ((CAST(RETRABAJADAS AS decimal)/CAST(REVISADAS AS decimal))*1000000) AS PPMRETRABAJADAS, ((CAST(RETRABAJADAS AS decimal)/CAST(REVISADAS AS decimal))) AS PORCRETRABAJADAS, PIEZASNOK, ((CAST(PIEZASNOK AS decimal)/CAST(REVISADAS AS decimal))*1000000) AS PPMNOK, PIEZASNOK, ((CAST(PIEZASNOK AS decimal)/CAST(REVISADAS AS decimal))) AS PORCNOK, COSTEHORASREVISION, COSTESCRAP, COSTETOTAL" +
                    " FROM (SELECT month ([FechaInicio]) as Mes,SUM([HorasInspeccion]) AS HORAS,SUM([PiezasRevisadas]) AS REVISADAS,SUM([PiezasOK]) AS PIEZASOK,SUM([Retrabajadas]) AS RETRABAJADAS,SUM([PiezasNOK]) AS PIEZASNOK,SUM([CostePiezaRevision]) AS COSTEHORASREVISION,SUM([CosteScrapRevision]) AS COSTESCRAP,SUM([CosteRevision]) AS COSTETOTAL"+
                    " FROM "+db+" group by month([FechaInicio]) ) S"+
                    " LEFT JOIN [SMARTH_DB].[dbo].[AUX_BMSHistoricoProducidas] B ON S.Mes = B.Mes AND B.Periodo = " + año+""+
					" ORDER BY S.MES", cnn_GP12);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                cnn_GP12.Close();
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                cnn_GP12.Close();
                return null;
            }
        }

        public DataSet devuelve_detecciones_operarios(string año, string mes)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "";
                if (Convert.ToInt32(año) > 2020)
                {
                    sql = "SELECT IDE,  O.Operario, count(conteo) AS SUMA FROM (SELECT [NOp1]  AS IDE, ([NOp1]) as conteo FROM [SMARTH_DB].[dbo].[GP12_Historico_2021]  WHERE year([FechaInicio]) = '" + año + "' and NOp1 <> 0" +
                    " union all SELECT [NOp2] AS IDE,([NOp2]) as conteo	FROM [SMARTH_DB].[dbo].[GP12_Historico_2021]	 WHERE year([FechaInicio]) = '" + año + "'  and NOp2 <> 0" +
                    " union all SELECT [NOp3] AS IDE,([NOp3]) as conteo	FROM [SMARTH_DB].[dbo].[GP12_Historico_2021]	 WHERE year([FechaInicio]) = '" + año + "'  and NOp3 <> 0" +
                    " union all SELECT [NOp4] AS IDE,([NOp4]) as conteo	FROM [SMARTH_DB].[dbo].[GP12_Historico_2021]	 WHERE year([FechaInicio]) = '" + año + "'  and NOp4 <> 0) T" +
                    " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] O ON IDE = O.Id" +
                    " group by IDE, Operario order by IDE asc";
                }
                else
                {
                    sql = "SELECT IDE,  O.Operario, count(conteo) AS SUMA FROM (SELECT [NOp1]  AS IDE, ([NOp1]) as conteo FROM [SMARTH_DB].[dbo].[GP12_Historico]  WHERE year([FechaInicio]) = '" + año + "' and NOp1 <> 0" +
                    " union all SELECT [NOp2] AS IDE,([NOp2]) as conteo	FROM [SMARTH_DB].[dbo].[GP12_Historico]	 WHERE year([FechaInicio]) = '" + año + "'  and NOp2 <> 0" +
                    " union all SELECT [NOp3] AS IDE,([NOp3]) as conteo	FROM [SMARTH_DB].[dbo].[GP12_Historico]	 WHERE year([FechaInicio]) = '" + año + "'  and NOp3 <> 0" +
                    " union all SELECT [NOp4] AS IDE,([NOp4]) as conteo	FROM [SMARTH_DB].[dbo].[GP12_Historico]	 WHERE year([FechaInicio]) = '" + año + "'  and NOp4 <> 0) T" +
                    " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] O ON IDE = O.Id" +
                    " group by IDE, Operario order by IDE asc";
                }
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

        public DataSet devuelve_detecciones_operarios_PRIMAS(string año, string mes)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "";
                if (Convert.ToInt32(año) > 2020)
                {
                    sql = "SELECT CAST(IDE AS VARCHAR(10)) AS IDE,  O.Operario, CAST(count(conteo) AS varchar) AS SUMA, CAST(sum(piezasNOK) AS varchar) as PIEZASNOK FROM (SELECT [NOp1]  AS IDE, ([NOp1]) as conteo, [PiezasNOK] FROM [SMARTH_DB].[dbo].[GP12_Historico_2021]  WHERE year([FechaInicio]) = '" + año + "' AND month ([FechaInicio]) = '" + mes + "' and NOp1 <> 0 and PiezasNOK > 0" +
                    " union all SELECT [NOp2] AS IDE,([NOp2]) as conteo, [PiezasNOK]	FROM [SMARTH_DB].[dbo].[GP12_Historico_2021]	 WHERE year([FechaInicio]) = '" + año + "' AND month ([FechaInicio]) = '" + mes + "' and NOp2 <> 0 and PiezasNOK > 0" +
                    " union all SELECT [NOp3] AS IDE,([NOp3]) as conteo, [PiezasNOK]	FROM [SMARTH_DB].[dbo].[GP12_Historico_2021]	 WHERE year([FechaInicio]) = '" + año + "' AND month ([FechaInicio]) = '" + mes + "' and NOp3 <> 0 and PiezasNOK > 0" +
                    " union all SELECT [NOp4] AS IDE,([NOp4]) as conteo, [PiezasNOK]	FROM [SMARTH_DB].[dbo].[GP12_Historico_2021]	 WHERE year([FechaInicio]) = '" + año + "' AND month ([FechaInicio]) = '" + mes + "' and NOp4 <> 0 and PiezasNOK > 0) T" +
                    " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] O ON IDE = O.Id" +
                    " group by IDE, Operario order by IDE asc";
                }
                else
                {
                    sql = "SELECT CAST(IDE AS VARCHAR(10)) AS IDE,  O.Operario, CAST(count(conteo) AS varchar) AS SUMA, CAST(sum(piezasNOK) AS varchar) as PIEZASNOK FROM (SELECT [NOp1]  AS IDE, ([NOp1]) as conteo, [PiezasNOK] FROM [SMARTH_DB].[dbo].[GP12_Historico]  WHERE year([FechaInicio]) = '" + año + "' AND month ([FechaInicio]) = '" + mes + "' and NOp1 <> 0 and PiezasNOK > 0" +
                    " union all SELECT [NOp2] AS IDE,([NOp2]) as conteo, [PiezasNOK]	FROM [SMARTH_DB].[dbo].[GP12_Historico]	 WHERE year([FechaInicio]) = '" + año + "' AND month ([FechaInicio]) = '" + mes + "' and NOp2 <> 0 and PiezasNOK > 0" +
                    " union all SELECT [NOp3] AS IDE,([NOp3]) as conteo, [PiezasNOK]	FROM [SMARTH_DB].[dbo].[GP12_Historico]	 WHERE year([FechaInicio]) = '" + año + "' AND month ([FechaInicio]) = '" + mes + "' and NOp3 <> 0 and PiezasNOK > 0" +
                    " union all SELECT [NOp4] AS IDE,([NOp4]) as conteo, [PiezasNOK]	FROM [SMARTH_DB].[dbo].[GP12_Historico]	 WHERE year([FechaInicio]) = '" + año + "' AND month ([FechaInicio]) = '" + mes + "' and NOp4 <> 0 and PiezasNOK > 0) T" +
                    " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] O ON IDE = O.Id" +
                    " group by IDE, Operario order by IDE asc";
                }
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

        public DataSet Devuelve_declaraciones_operarios_PRIMAS(string año, string mes)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT  CAST(C_OPERATOR AS VARCHAR(10)) AS IDE, C_OPERATORNAME, SUM(C_COUNTTOTAL-C_COUNT27 - C_COUNT29) AS PIEZASDETECTADAS, COUNT(C_SEQUENCE_NR) AS PRODUCCIONES FROM HIS.T_HISOPERATORS" +
                             " WHERE EXTRACT(YEAR FROM C_STARTTIME) = '"+año+"' AND EXTRACT(MONTH FROM C_STARTTIME) = '"+mes+ "' AND(C_COUNTTOTAL - C_COUNT27 - C_COUNT29) > 0 AND C_OPERATORTYPE = 1" +
                             " GROUP BY C_OPERATOR, C_OPERATORNAME"+
                             " ORDER BY C_OPERATOR ASC";
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


        public DataSet devuelve_detecciones_operarios_Año(string año)
        {
            try
            {
                cnn_GP12.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_GP12;
                string sql = "";
                if (Convert.ToInt32(año) > 2020)
                {
                    sql = "SELECT IDE,  O.Operario, count(conteo) AS SUMA FROM (SELECT [NOp1]  AS IDE, ([NOp1]) as conteo FROM [SMARTH_DB].[dbo].[GP12_Historico_2021]  WHERE year([FechaInicio]) = '" + año + "' and NOp1 <> 0 and PiezasNOK > 0" +
                    " union all SELECT [NOp2] AS IDE,([NOp2]) as conteo	FROM [SMARTH_DB].[dbo].[GP12_Historico_2021]	 WHERE year([FechaInicio]) = '" + año + "'  and NOp2 <> 0 and PiezasNOK > 0" +
                    " union all SELECT [NOp3] AS IDE,([NOp3]) as conteo	FROM [SMARTH_DB].[dbo].[GP12_Historico_2021]	 WHERE year([FechaInicio]) = '" + año + "'  and NOp3 <> 0 and PiezasNOK > 0" +
                    " union all SELECT [NOp4] AS IDE,([NOp4]) as conteo	FROM [SMARTH_DB].[dbo].[GP12_Historico_2021]	 WHERE year([FechaInicio]) = '" + año + "'  and NOp4 <> 0 and PiezasNOK > 0) T" +
                    " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] O ON IDE = O.Id" +
                    " group by IDE, Operario order by IDE asc";
                }
                else
                {
                    sql = "SELECT IDE,  O.Operario, count(conteo) AS SUMA FROM (SELECT [NOp1]  AS IDE, ([NOp1]) as conteo FROM [SMARTH_DB].[dbo].[GP12_Historico]  WHERE year([FechaInicio]) = '" + año + "' and NOp1 <> 0 and PiezasNOK > 0" +
                    " union all SELECT [NOp2] AS IDE,([NOp2]) as conteo	FROM [SMARTH_DB].[dbo].[GP12_Historico]	 WHERE year([FechaInicio]) = '" + año + "'  and NOp2 <> 0 and PiezasNOK > 0" +
                    " union all SELECT [NOp3] AS IDE,([NOp3]) as conteo	FROM [SMARTH_DB].[dbo].[GP12_Historico]	 WHERE year([FechaInicio]) = '" + año + "'  and NOp3 <> 0 and PiezasNOK > 0" +
                    " union all SELECT [NOp4] AS IDE,([NOp4]) as conteo	FROM [SMARTH_DB].[dbo].[GP12_Historico]	 WHERE year([FechaInicio]) = '" + año + "'  and NOp4 <> 0 and PiezasNOK > 0) T" +
                    " LEFT JOIN [SMARTH_DB].[dbo].[AUX_TablaOperarios] O ON IDE = O.Id" +
                    " group by IDE, Operario order by IDE asc";
                }
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


    }
}