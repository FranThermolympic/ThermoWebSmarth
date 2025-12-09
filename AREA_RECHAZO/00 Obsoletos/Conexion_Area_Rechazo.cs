using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;

namespace ThermoWeb.AREA_RECHAZO
{
    public class Conexion_Area_Rechazo
    {
       
        private readonly SqlConnection cnn_SMARTH = new SqlConnection();

        public Conexion_Area_Rechazo()
        {
            
            cnn_SMARTH.ConnectionString = ConfigurationManager.ConnectionStrings["SMARTH"].ToString();
        }

        // leer area de rechazo con filtro "activas"
        
        
        
        
        public DataSet Leer_area_rechazo()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM [CALIDAD].[dbo].[AreaRechazo] WHERE FechaSalida is NULL", cnn_SMARTH);
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

        // leer area de rechazo con filtro "todas"
        public DataSet Leer_area_rechazo_todas()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM [CALIDAD].[dbo].[AreaRechazo]", cnn_SMARTH);
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

        public void Eliminar_area_rechazo(int id)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand { Connection = cnn_SMARTH };
                string sql = "DELETE FROM [CALIDAD].[dbo].[AreaRechazo] WHERE Id = " + id;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
            }
        }

        public void Actualizar_area_rechazo(Int32 id, string referencia, string motivo, string responsableEntrada,
                                            Int32  cantidad, string fechaEntrada, string fechaSalida, string debeSalir,
                                            string decision, string responsableSalida, string observaciones)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand { Connection = cnn_SMARTH };
                
                string sql = "UPDATE dbo.AreaRechazo SET Referencia = " + referencia + ", Motivo = '" + motivo + "', ResponsableEntrada = '" + responsableEntrada + "', Cantidad = " + cantidad + ", FechaEntrada = CASE WHEN '" + fechaEntrada + "' = '' THEN NULL ELSE '" + fechaEntrada + "' END, FechaSalida = CASE WHEN '" + fechaSalida + "' = '' THEN NULL ELSE '" + fechaSalida + "' END, DebeSalir = CASE WHEN '" + debeSalir + "' = '' THEN NULL ELSE '" + debeSalir + "' END, Decision = '" + decision + "', ResponsableSalida = '" + responsableSalida + "', Observaciones = '" + observaciones + "' WHERE Id = " + id;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
            }
        }

        public void Insertar_area_rechazo(string referencia, string motivo, string responsableEntrada,
                                            Int32 cantidad, string fechaEntrada, string fechaSalida, string debeSalir,
                                            string decision, string responsableSalida, string observaciones)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand { Connection = cnn_SMARTH };
                string sql = "INSERT INTO dbo.AreaRechazo (Referencia,Motivo,ResponsableEntrada,Cantidad,FechaEntrada,FechaSalida,DebeSalir,Decision,ResponsableSalida,Observaciones) VALUES ('" + referencia + "','" + motivo + "','" + responsableEntrada + "'," + cantidad + ",CASE WHEN '" + fechaEntrada + "' = '' THEN NULL ELSE '" + fechaEntrada + "' END, CASE WHEN '" + fechaSalida + "'= '' THEN NULL ELSE '" + fechaSalida + "' END, CASE WHEN '" + debeSalir + "'= '' THEN NULL ELSE '" + debeSalir + "' END,'" + decision +"','" + responsableSalida + "','" + observaciones + "')";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
            }
        }
    }
}