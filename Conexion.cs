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

namespace ThermoWeb
{
    public class Conexion
    {
        //private readonly SqlConnection cnn_PARAMETROS = new SqlConnection();
        private readonly OracleConnection cnn_bms = new OracleConnection();
        private readonly SqlConnection cnn_SMARTH = new SqlConnection();


        public Conexion()
        {
            //cnn_PARAMETROS.ConnectionString = ConfigurationManager.ConnectionStrings["parametros"].ToString();
            cnn_bms.ConnectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=thermoBMS)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME= barcodb)));User Id= pcms;Password=smcp;Persist Security Info=True;";
            cnn_SMARTH.ConnectionString = ConfigurationManager.ConnectionStrings["SMARTH"].ToString();
        }

        public void InsertarFichaV2(int referencia, int maquina, int version, string descripcion, string cliente, string codMolde, string nMaquina, int automatico, string manual,
                          string personalAsignado, string programaMolde, string nProgramaRobot, string nManoRobot, string aperturaMaquina,
                          string nCavidades, double pesoPieza, double pesoColada, double pesoTotal, string velocidadCarga, string carga, string descompresion,
                          string contrapresion, string tiempo, string enfriamiento, string ciclo, string cojin, string razones, /*int codigo, string material, string codColorante, string colorante,
                                  string color, string tempSecado, string tiempoSecado,*/ double boq, double T1, double T2, double T3, double T4,
                          double T5, double T6, double T7, double T8, double T9, double T10, /*string refrigeracionCircuito, string atempCircuito, string atempTemperatura,*/
                          double Z1, double Z2, double Z3, double Z4, double Z5, double Z6, double Z7, double Z8, double Z9, double Z10,
                          double Z11, double Z12, double Z13, double Z14, double Z15, double Z16, double Z17, double Z18, double Z19, double Z20,
                          /*string refrigeracionCircuito2, string atempCircuito2, string atempTemperatura2,*/
                          double V1, double C1, double V2, double C2, double V3, double C3, double V4, double C4, double V5, double C5,
                          double V6, double C6, double V7, double C7, double V8, double C8, double V9, double C9, double V10, double C10,
                          double V11, double C11, double V12, double C12, string Tiempo, string limitePresion, string limitePresionReal,
                          double P1, double Tp1, double P2, double Tp2, double P3, double Tp3, double P4, double Tp4, double P5, double Tp5,
                          double P6, double Ti6, double P7, double Tp7, double P8, double Tp8, double P9, double Tp9, double P10, double Tp10, string conmutacion, string tiempoPresion,
                          //Pet declarosecu corregir a base de datos
                          double abrir1_1, double abrir1_2, double abrir1_3, double abrir1_4, double abrir1_5, double abrir1_6, double abrir1_7, double abrir1_8, double abrir1_9, double abrir1_10,
                          double cerrar1_1, double cerrar1_2, double cerrar1_3, double cerrar1_4, double cerrar1_5, double cerrar1_6, double cerrar1_7, double cerrar1_8, double cerrar1_9, double cerrar1_10,
                          double abrir2_1, double abrir2_2, double abrir2_3, double abrir2_4, double abrir2_5, double abrir2_6, double abrir2_7, double abrir2_8, double abrir2_9, double abrir2_10,
                          double cerrar2_1, double cerrar2_2, double cerrar2_3, double cerrar2_4, double cerrar2_5, double cerrar2_6, double cerrar2_7, double cerrar2_8, double cerrar2_9, double cerrar2_10,
                          double TprestPost_1, double TprestPost_2, double TprestPost_3, double TprestPost_4, double TprestPost_5, double TprestPost_6, double TprestPost_7, double TprestPost_8, double TprestPost_9, double TprestPost_10,
                          double TiempoRetardo_1, double TiempoRetardo_2, double TiempoRetardo_3, double TiempoRetardo_4, double TiempoRetardo_5, double TiempoRetardo_6, double TiempoRetardo_7, double TiempoRetardo_8, double TiempoRetardo_9, double TiempoRetardo_10, string Anotaciones,
                          //termino secu
                          //declaro tolerancias
                          double TiempoInyeccionNValDouble, double TiempoInyeccionMValDouble, double LimitePresionNValDouble, double LimitePresionMValDouble, double ConmuntaciontolNValDouble, double ConmuntaciontolMValDouble, double TiempoPresiontolNValDouble,
                          double TiempoPresiontolMValDouble, double TNvcargavalDouble, double TMvcargavalDouble, double TNcargavalDouble, double TMcargavalDouble, double TNdescomvalDouble, double TMdescomvalDouble, double TNcontrapvalDouble,
                          double TMcontrapvalDouble, double TNTiempdosvalDouble, double TMTiempdosvalDouble, double TNEnfriamvalDouble, double TMEnfriamvalDouble, double TNCiclovalDouble, double TMCiclovalDouble, double TNCojinvalDouble, double TMCojinvalDouble,
                          //tolerancias nuevas
                          double tbTOLCarInyeccion,double tbTOLCamCaliente,double TbTOLCilindro,double tbTOLPostPresion,double tbTOLSecCota,double tbTOLSecTiempo, double LimitePresionNRealValDouble, double LimitePresionMRealValDouble,
                          //fin tolernacias nuevas
                          string TbOperacionText1, string TbOperacionText2, string TbOperacionText3, string TbOperacionText4, string TbOperacionText5,
                          //termino tolerancias
                          //declaro atemperado
                          int AtempTipoF, int CircuitoF1, int CircuitoF2, int CircuitoF3, int CircuitoF4, int CircuitoF5, int CircuitoF6,
                          string CaudalF1, string CaudalF2, string CaudalF3, string CaudalF4, string CaudalF5, string CaudalF6,
                          string TemperaturaF1, string TemperaturaF2, string TemperaturaF3, string TemperaturaF4, string TemperaturaF5, string TemperaturaF6,
                          int EntradaF1, int EntradaF2, int EntradaF3, int EntradaF4, int EntradaF5, int EntradaF6,
                          int AtempTipoM, int CircuitoM1, int CircuitoM2, int CircuitoM3, int CircuitoM4, int CircuitoM5, int CircuitoM6,
                          string CaudalM1, string CaudalM2, string CaudalM3, string CaudalM4, string CaudalM5, string CaudalM6,
                          string TemperaturaM1, string TemperaturaM2, string TemperaturaM3, string TemperaturaM4, string TemperaturaM5, string TemperaturaM6,
                          int EntradaM1, int EntradaM2, int EntradaM3, int EntradaM4, int EntradaM5, int EntradaM6,
                          //termino atemperado

                          //declaro imagenes
                          string img1, string hyperlink2, string hyperlink3, string hyperlink4, string img5, string hyperlink6, string hyperlink7, string hyperlink8,
                          //termino imagenes
                          int noyos, int hembra, int macho, int antesExpuls, int antesAper, int antesCierre,
                          int despuesCierre, int otros, int boquilla, int cono, int radioLarga, int libre, int valvula, int resistencia,
                          int otros2, int expulsion, int hidraulica, int neumatica, int normal, int arandela125, int arandela160, int arandela200, int arandela250, string MarcasOtrosText,
                          string edicion, string fecha, int elaborado, int revisado, int aprobado, string observaciones, string FuerzaCierre)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "";
                sql = "INSERT INTO [SMARTH_DB].[dbo].[PARAMETROS_Ficha] (Referencia, Maquina, Version, Descripcion, Cliente, CodMolde, NMaquina, ModoAutoMan, Manual, PersonalAsignado, " +
                                       "ProgramaMolde, NProgramaRobot, NManoRobot, AperturaMaquina, NCavidades, PesoPiezas, " +
                                       "PesoColadas, PesoTotales, VelocidadCarga, Carga, Descompresion, Contrapresion, Tiempo, Enfriamiento, Ciclo, Cojin, Razones, " +
                                       "Edicion, Fecha, INTElaborado, INTRevisado, INTAprobado, Observaciones, FuerzaCierre) " +
                                       "VALUES (" + referencia + "," + maquina + "," + version + ",'" + descripcion.ToString() + "','" + cliente.ToString() + "','" + codMolde.ToString() + "','" + nMaquina.ToString() + "'," + automatico + ",'" + manual.ToString() + "','" +
                                        personalAsignado.ToString().Replace(',', '.') + "','" + programaMolde.ToString() + "','" + nProgramaRobot.ToString() + "','" + nManoRobot.ToString() + "','" + aperturaMaquina.ToString() + "','" +
                                        nCavidades.ToString() + "'," + pesoPieza.ToString().Replace(',', '.') + "," + pesoColada.ToString().Replace(',', '.') + "," + pesoTotal.ToString().Replace(',', '.') + ",'" + velocidadCarga.ToString().Replace(',', '.') + "','" + carga.ToString().Replace(',', '.') + "','" +
                                        descompresion.ToString().Replace(',', '.') + "','" + contrapresion.ToString() + "','" + tiempo.ToString().Replace(',', '.') + "','" + enfriamiento.ToString().Replace(',', '.') + "','" + ciclo.ToString().Replace(',', '.') + "','" + cojin.ToString().Replace(',', '.') + "','" + razones.ToString() + "','" +
                                        edicion + "',CURRENT_TIMESTAMP,'" + elaborado + "','" + revisado + "','" + aprobado + "','" + observaciones + "','" + FuerzaCierre + "')";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                //insertarMaterial(referencia, maquina, version, codigo, material, codColorante, colorante,
                //                       color, tempSecado, tiempoSecado);
                insertarTempCilindro(referencia, maquina, version, boq, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10); //refrigeracionCircuito, atempCircuito, atempTemperatura);
                insertarTempCamCaliente(referencia, maquina, version, Z1, Z2, Z3, Z4, Z5, Z6, Z7, Z8, Z9, Z10,
                                        Z11, Z12, Z13, Z14, Z15, Z16, Z17, Z18, Z19, Z20);
                //refrigeracionCircuito2, atempCircuito2, atempTemperatura2);
                InsertarInyeccionV2(referencia, maquina, version, V1, C1, V2, C2, V3, C3, V4, C4, V5, C5, V6, C6, V7, C7, V8, C8, V9, C9, V10, C10, V11, C11, V12, C12, Tiempo, limitePresion, limitePresionReal);
                insertarPostpresion(referencia, maquina, version, P1, Tp1, P2, Tp2, P3, Tp3, P4, Tp4, P5, Tp5, P6, Ti6, P7, Tp7, P8, Tp8, P9, Tp9, P10, Tp10, tiempoPresion, conmutacion);
                insertarInyeccionSecuencial(referencia, maquina, version, abrir1_1, abrir1_2, abrir1_3, abrir1_4, abrir1_5, abrir1_6, abrir1_7, abrir1_8, abrir1_9, abrir1_10,
                                                    cerrar1_1, cerrar1_2, cerrar1_3, cerrar1_4, cerrar1_5, cerrar1_6, cerrar1_7, cerrar1_8, cerrar1_9, cerrar1_10,
                                                    abrir2_1, abrir2_2, abrir2_3, abrir2_4, abrir2_5, abrir2_6, abrir2_7, abrir2_8, abrir2_9, abrir2_10,
                                                    cerrar2_1, cerrar2_2, cerrar2_3, cerrar2_4, cerrar2_5, cerrar2_6, cerrar2_7, cerrar2_8, cerrar2_9, cerrar2_10,
                                                    TprestPost_1, TprestPost_2, TprestPost_3, TprestPost_4, TprestPost_5, TprestPost_6, TprestPost_7, TprestPost_8, TprestPost_9, TprestPost_10,
                                                    TiempoRetardo_1, TiempoRetardo_2, TiempoRetardo_3, TiempoRetardo_4, TiempoRetardo_5, TiempoRetardo_6, TiempoRetardo_7, TiempoRetardo_8, TiempoRetardo_9, TiempoRetardo_10, Anotaciones);
                InsertarToleranciasV2(referencia, maquina, version, TiempoInyeccionNValDouble, TiempoInyeccionMValDouble, LimitePresionNValDouble, LimitePresionMValDouble, ConmuntaciontolNValDouble, ConmuntaciontolMValDouble, TiempoPresiontolNValDouble,
                                                     TiempoPresiontolMValDouble, TNvcargavalDouble, TMvcargavalDouble, TNcargavalDouble, TMcargavalDouble, TNdescomvalDouble, TMdescomvalDouble, TNcontrapvalDouble,
                                                     TMcontrapvalDouble, TNTiempdosvalDouble, TMTiempdosvalDouble, TNEnfriamvalDouble, TMEnfriamvalDouble, TNCiclovalDouble, TMCiclovalDouble, TNCojinvalDouble, TMCojinvalDouble,
                                                     TbOperacionText1, TbOperacionText2, TbOperacionText3, TbOperacionText4, TbOperacionText5,
                                                     tbTOLCarInyeccion,tbTOLCamCaliente,TbTOLCilindro,tbTOLPostPresion,tbTOLSecCota,tbTOLSecTiempo, LimitePresionNRealValDouble, LimitePresionMRealValDouble);
                insertarAtemperado(referencia, maquina, version, AtempTipoF, CircuitoF1, CircuitoF2, CircuitoF3, CircuitoF4, CircuitoF5, CircuitoF6,
                                                    CaudalF1, CaudalF2, CaudalF3, CaudalF4, CaudalF5, CaudalF6,
                                                    TemperaturaF1, TemperaturaF2, TemperaturaF3, TemperaturaF4, TemperaturaF5, TemperaturaF6,
                                                    EntradaF1, EntradaF2, EntradaF3, EntradaF4, EntradaF5, EntradaF6,
                                                    AtempTipoM, CircuitoM1, CircuitoM2, CircuitoM3, CircuitoM4, CircuitoM5, CircuitoM6,
                                                    CaudalM1, CaudalM2, CaudalM3, CaudalM4, CaudalM5, CaudalM6,
                                                    TemperaturaM1, TemperaturaM2, TemperaturaM3, TemperaturaM4, TemperaturaM5, TemperaturaM6,
                                                    EntradaM1, EntradaM2, EntradaM3, EntradaM4, EntradaM5, EntradaM6);
                insertarImagenes(referencia, maquina, version, img1, hyperlink2, hyperlink3, hyperlink4, img5, hyperlink6, hyperlink7, hyperlink8);
                insertarMarcaCruz(referencia, maquina, version, noyos, hembra, macho, antesExpuls, antesAper, antesCierre,
                            despuesCierre, otros, boquilla, cono, radioLarga, libre, valvula, resistencia,
                            otros2, expulsion, hidraulica, neumatica, normal, arandela125, arandela160, arandela200, arandela250, MarcasOtrosText);
            }
            catch (Exception ex)
            {

            }
        }





        public void insertarFicha(int referencia, int maquina, int version, string descripcion, string cliente, string codMolde, string nMaquina, string automatico, string manual,
                                  string personalAsignado, string programaMolde, string nProgramaRobot, string nManoRobot, string aperturaMaquina,
                                  string nCavidades, string pesoPieza, string pesoColada, string pesoTotal, string velocidadCarga, string carga, string descompresion,
                                  string contrapresion, string tiempo, string enfriamiento, string ciclo, string cojin, string razones, /*int codigo, string material, string codColorante, string colorante,
                                  string color, string tempSecado, string tiempoSecado,*/ double boq, double T1, double T2, double T3, double T4,
                                  double T5, double T6, double T7, double T8, double T9, double T10, /*string refrigeracionCircuito, string atempCircuito, string atempTemperatura,*/
                                  double Z1, double Z2, double Z3, double Z4, double Z5, double Z6, double Z7, double Z8, double Z9, double Z10,
                                  double Z11, double Z12, double Z13, double Z14, double Z15, double Z16, double Z17, double Z18, double Z19, double Z20,
                                  /*string refrigeracionCircuito2, string atempCircuito2, string atempTemperatura2,*/
                                  double V1, double C1, double V2, double C2, double V3, double C3, double V4, double C4, double V5, double C5,
                                  double V6, double C6, double V7, double C7, double V8, double C8, double V9, double C9, double V10, double C10,
                                  double V11, double C11, double V12, double C12, string Tiempo, string limitePresion,
                                  double P1, double Tp1, double P2, double Tp2, double P3, double Tp3, double P4, double Tp4, double P5, double Tp5,
                                  double P6, double Ti6, double P7, double Tp7, double P8, double Tp8, double P9, double Tp9, double P10, double Tp10, string conmutacion, string tiempoPresion,
                                  //Pet declarosecu corregir a base de datos
                                  double abrir1_1, double abrir1_2, double abrir1_3, double abrir1_4, double abrir1_5, double abrir1_6, double abrir1_7, double abrir1_8, double abrir1_9, double abrir1_10,
                                  double cerrar1_1, double cerrar1_2, double cerrar1_3, double cerrar1_4, double cerrar1_5, double cerrar1_6, double cerrar1_7, double cerrar1_8, double cerrar1_9, double cerrar1_10,
                                  double abrir2_1, double abrir2_2, double abrir2_3, double abrir2_4, double abrir2_5, double abrir2_6, double abrir2_7, double abrir2_8, double abrir2_9, double abrir2_10,
                                  double cerrar2_1, double cerrar2_2, double cerrar2_3, double cerrar2_4, double cerrar2_5, double cerrar2_6, double cerrar2_7, double cerrar2_8, double cerrar2_9, double cerrar2_10,
                                  double TprestPost_1, double TprestPost_2, double TprestPost_3, double TprestPost_4, double TprestPost_5, double TprestPost_6, double TprestPost_7, double TprestPost_8, double TprestPost_9, double TprestPost_10,
                                  double TiempoRetardo_1, double TiempoRetardo_2, double TiempoRetardo_3, double TiempoRetardo_4, double TiempoRetardo_5, double TiempoRetardo_6, double TiempoRetardo_7, double TiempoRetardo_8, double TiempoRetardo_9, double TiempoRetardo_10, string Anotaciones, 
                                  //termino secu
                                  //declaro tolerancias
                                  double TiempoInyeccionNValDouble, double TiempoInyeccionMValDouble, double LimitePresionNValDouble, double LimitePresionMValDouble, double ConmuntaciontolNValDouble, double ConmuntaciontolMValDouble, double TiempoPresiontolNValDouble,
                                  double TiempoPresiontolMValDouble, double TNvcargavalDouble, double TMvcargavalDouble, double TNcargavalDouble, double TMcargavalDouble, double TNdescomvalDouble, double TMdescomvalDouble, double TNcontrapvalDouble,
                                  double TMcontrapvalDouble, double TNTiempdosvalDouble, double TMTiempdosvalDouble, double TNEnfriamvalDouble, double TMEnfriamvalDouble, double TNCiclovalDouble, double TMCiclovalDouble, double TNCojinvalDouble, double TMCojinvalDouble,
                                  string TbOperacionText1,string TbOperacionText2,string TbOperacionText3,string TbOperacionText4, string TbOperacionText5,
                                  //termino tolerancias
                                  //declaro atemperado
                                  int AtempTipoF, int CircuitoF1, int CircuitoF2, int CircuitoF3, int CircuitoF4, int CircuitoF5, int CircuitoF6,
                                  string CaudalF1, string CaudalF2, string CaudalF3, string CaudalF4, string CaudalF5, string CaudalF6,
                                  string TemperaturaF1, string TemperaturaF2, string TemperaturaF3, string TemperaturaF4, string TemperaturaF5, string TemperaturaF6,
                                  int EntradaF1, int EntradaF2, int EntradaF3, int EntradaF4, int EntradaF5, int EntradaF6,
                                  int AtempTipoM, int CircuitoM1, int CircuitoM2, int CircuitoM3, int CircuitoM4, int CircuitoM5, int CircuitoM6,
                                  string CaudalM1, string CaudalM2, string CaudalM3, string CaudalM4, string CaudalM5, string CaudalM6,
                                  string TemperaturaM1, string TemperaturaM2, string TemperaturaM3, string TemperaturaM4, string TemperaturaM5, string TemperaturaM6,
                                  int EntradaM1, int EntradaM2, int EntradaM3, int EntradaM4, int EntradaM5, int EntradaM6,
                                  //termino atemperado
                                  
                                  //declaro imagenes
                                  string img1, string hyperlink2, string hyperlink3, string hyperlink4, string img5, string hyperlink6, string hyperlink7, string hyperlink8,
                                  //termino imagenes
                                  int noyos, int hembra, int macho, int antesExpuls, int antesAper, int antesCierre,
                                  int despuesCierre, int otros, int boquilla, int cono, int radioLarga, int libre, int valvula, int resistencia,
                                  int otros2, int expulsion, int hidraulica, int neumatica, int normal, int arandela125, int arandela160, int arandela200, int arandela250, string MarcasOtrosText,
                                  string edicion, string fecha, int elaborado, int revisado, int aprobado, string observaciones, string FuerzaCierre)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "";
                sql = "INSERT INTO [SMARTH_DB].[dbo].[PARAMETROS_Ficha] (Referencia, Maquina, Version, Descripcion, Cliente, CodMolde, NMaquina, Automatico, Manual, PersonalAsignado, " +
                                       "ProgramaMolde, NProgramaRobot, NManoRobot, AperturaMaquina, NCavidades, PesoPieza, " +
                                       "PesoColada, PesoTotal, VelocidadCarga, Carga, Descompresion, Contrapresion, Tiempo, Enfriamiento, Ciclo, Cojin, Razones, " +
                                       "Edicion, Fecha, INTElaborado, INTRevisado, INTAprobado, Observaciones, FuerzaCierre) " +
                                       "VALUES (" + referencia + "," + maquina + "," + version + ",'" + descripcion.ToString() + "','" + cliente.ToString() + "','" + codMolde.ToString() + "','" + nMaquina.ToString() + "','" + automatico.ToString() + "','" + manual.ToString() + "','" +
                                        personalAsignado.ToString().Replace(',', '.') + "','" + programaMolde.ToString() + "','" + nProgramaRobot.ToString() + "','" + nManoRobot.ToString() + "','" + aperturaMaquina.ToString() + "','" +
                                        nCavidades.ToString() + "','" + pesoPieza.ToString() + "','" + pesoColada.ToString() + "','" + pesoTotal.ToString() + "','" + velocidadCarga.ToString().Replace(',', '.') + "','" + carga.ToString().Replace(',', '.') + "','" +
                                        descompresion.ToString().Replace(',', '.') + "','" + contrapresion.ToString() + "','" + tiempo.ToString().Replace(',', '.') + "','" + enfriamiento.ToString().Replace(',', '.') + "','" + ciclo.ToString().Replace(',', '.') + "','" + cojin.ToString().Replace(',', '.') + "','" + razones.ToString() + "','" +
                                        edicion + "',CURRENT_TIMESTAMP,'" + elaborado + "','" + revisado + "','" + aprobado + "','" + observaciones + "','"+FuerzaCierre+"')";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                //insertarMaterial(referencia, maquina, version, codigo, material, codColorante, colorante,
                //                       color, tempSecado, tiempoSecado);
                insertarTempCilindro(referencia, maquina, version, boq, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10); //refrigeracionCircuito, atempCircuito, atempTemperatura);
                insertarTempCamCaliente(referencia, maquina, version, Z1, Z2, Z3, Z4, Z5, Z6, Z7, Z8, Z9, Z10,
                                        Z11, Z12, Z13, Z14, Z15, Z16, Z17, Z18, Z19, Z20);
                                        //refrigeracionCircuito2, atempCircuito2, atempTemperatura2);
                insertarInyeccion(referencia, maquina, version, V1, C1, V2, C2, V3, C3, V4, C4, V5, C5, V6, C6, V7, C7, V8, C8, V9, C9, V10, C10, V11, C11, V12, C12, Tiempo, limitePresion);
                insertarPostpresion(referencia, maquina, version, P1, Tp1, P2, Tp2, P3, Tp3, P4, Tp4, P5, Tp5, P6, Ti6, P7, Tp7, P8, Tp8, P9, Tp9, P10, Tp10, tiempoPresion, conmutacion);
                insertarInyeccionSecuencial(referencia, maquina, version, abrir1_1, abrir1_2, abrir1_3, abrir1_4, abrir1_5, abrir1_6, abrir1_7, abrir1_8, abrir1_9, abrir1_10,
                                                    cerrar1_1, cerrar1_2, cerrar1_3, cerrar1_4, cerrar1_5, cerrar1_6, cerrar1_7, cerrar1_8, cerrar1_9, cerrar1_10,
                                                    abrir2_1, abrir2_2, abrir2_3, abrir2_4, abrir2_5, abrir2_6, abrir2_7, abrir2_8, abrir2_9, abrir2_10,
                                                    cerrar2_1, cerrar2_2, cerrar2_3, cerrar2_4, cerrar2_5, cerrar2_6, cerrar2_7, cerrar2_8, cerrar2_9, cerrar2_10,
                                                    TprestPost_1, TprestPost_2, TprestPost_3, TprestPost_4, TprestPost_5, TprestPost_6, TprestPost_7, TprestPost_8, TprestPost_9, TprestPost_10,
                                                    TiempoRetardo_1, TiempoRetardo_2, TiempoRetardo_3, TiempoRetardo_4, TiempoRetardo_5, TiempoRetardo_6, TiempoRetardo_7, TiempoRetardo_8, TiempoRetardo_9, TiempoRetardo_10, Anotaciones);
                insertarTolerancias(referencia, maquina, version, TiempoInyeccionNValDouble, TiempoInyeccionMValDouble, LimitePresionNValDouble,  LimitePresionMValDouble,  ConmuntaciontolNValDouble,  ConmuntaciontolMValDouble,  TiempoPresiontolNValDouble,
                                                     TiempoPresiontolMValDouble, TNvcargavalDouble, TMvcargavalDouble,  TNcargavalDouble,  TMcargavalDouble,  TNdescomvalDouble,  TMdescomvalDouble,  TNcontrapvalDouble,
                                                     TMcontrapvalDouble, TNTiempdosvalDouble,  TMTiempdosvalDouble,  TNEnfriamvalDouble,  TMEnfriamvalDouble,  TNCiclovalDouble,  TMCiclovalDouble,  TNCojinvalDouble,  TMCojinvalDouble,
                                                     TbOperacionText1, TbOperacionText2, TbOperacionText3, TbOperacionText4, TbOperacionText5);
                insertarAtemperado(referencia, maquina, version, AtempTipoF, CircuitoF1, CircuitoF2, CircuitoF3, CircuitoF4, CircuitoF5, CircuitoF6,
                                                    CaudalF1, CaudalF2, CaudalF3, CaudalF4, CaudalF5, CaudalF6,
                                                    TemperaturaF1, TemperaturaF2, TemperaturaF3, TemperaturaF4, TemperaturaF5, TemperaturaF6,
                                                    EntradaF1, EntradaF2, EntradaF3, EntradaF4, EntradaF5, EntradaF6,
                                                    AtempTipoM, CircuitoM1, CircuitoM2, CircuitoM3, CircuitoM4, CircuitoM5, CircuitoM6,
                                                    CaudalM1, CaudalM2, CaudalM3, CaudalM4, CaudalM5, CaudalM6,
                                                    TemperaturaM1, TemperaturaM2, TemperaturaM3, TemperaturaM4, TemperaturaM5, TemperaturaM6,
                                                    EntradaM1, EntradaM2, EntradaM3, EntradaM4, EntradaM5, EntradaM6);
                insertarImagenes(referencia, maquina, version, img1, hyperlink2, hyperlink3, hyperlink4, img5, hyperlink6, hyperlink7, hyperlink8);
                insertarMarcaCruz(referencia, maquina, version, noyos, hembra, macho, antesExpuls, antesAper, antesCierre,
                            despuesCierre, otros, boquilla, cono, radioLarga, libre, valvula, resistencia,
                            otros2, expulsion, hidraulica, neumatica, normal, arandela125, arandela160, arandela200, arandela250, MarcasOtrosText); 
            }
            catch (Exception)
            {

            }
        }

        public void actualizarFicha(int referencia, int maquina, int version, string descripcion, string cliente, string codMolde, string nMaquina, string automatico, string manual,
                                  string personalAsignado, string programaMolde, string nProgramaRobot, string nManoRobot, string aperturaMaquina,
                                  string nCavidades, string pesoPieza, string pesoColada, string pesoTotal, string velocidadCarga, string carga, string descompresion,
                                  string contrapresion, string tiempo, string enfriamiento, string ciclo, string cojin, int codigo, string material, string codColorante, string colorante,
                                  string color, string tempSecado, string tiempoSecado, double boq, double T1, double T2, double T3, double T4,
                                  double T5, double T6, double T7, double T8, double T9, double T10, string refrigeracionCircuito, string atempCircuito, string atempTemperatura,
                                  double Z1, double Z2, double Z3, double Z4, double Z5, double Z6, double Z7, double Z8, double Z9, double Z10,
                                  double Z11, double Z12, double Z13, double Z14, double Z15, double Z16, double Z17, double Z18, double Z19, double Z20,
                                  string refrigeracionCircuito2, string atempCircuito2, string atempTemperatura2,
                                  double V1, double C1, double V2, double C2, double V3, double C3, double V4, double C4, double V5, double C5,
                                  double V6, double C6, double V7, double C7, double V8, double C8, double V9, double C9, double V10, double C10,
                                  double V11, double C11, double V12, double C12, string Tiempo, string limitePresion,
                                  double P1, double Tp1, double P2, double Tp2, double P3, double Tp3, double P4, double Tp4, double P5, double Tp5,
                                  double P6, double Ti6, double P7, double Tp7, double P8, double Tp8, double P9, double Tp9, double P10, double Tp10, string conmutacion, string tiempoPresion,
                                  int noyos, int hembra, int macho, int antesExpuls, int antesAper, int antesCierre,
                                  int despuesCierre, int otros, int boquilla, int cono, int radioLarga, int libre, int valvula, int resistencia,
                                  int otros2, int expulsion, int hidraulica, int neumatica, int normal, int arandela125, int arandela160, int arandela200, int arandela250)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "";
                sql = "UPDATE [SMARTH_DB].[dbo].[PARAMETROS_Ficha] SET Referencia = " + referencia + ", Descripcion = '" + descripcion + "', Cliente = '" + cliente + "', CodMolde = '" + codMolde + "', NMaquina = '" + nMaquina +
                                         "', Automatico = '" + automatico + "', Manual = '" + manual + "', PersonalAsignado = '" + personalAsignado.ToString().Replace(',', '.') +
                                         "', ProgramaMolde = '" + programaMolde + "', NProgramaRobot = '" + nProgramaRobot + "', NManoRobot = '" + nManoRobot + "', AperturaMaquina = '" + aperturaMaquina +
                                         "', NCavidades = '" + nCavidades + "', PesoPieza = '" + pesoPieza + "', PesoColada = '" + pesoColada + "', PesoTotal = '" + pesoTotal +
                                         "', VelocidadCarga = '" + velocidadCarga.ToString().Replace(',', '.') + "', Carga = '" + carga.ToString().Replace(',', '.') + "', Descompresion = '" + descompresion.ToString().Replace(',', '.') + "', Contrapresion = '" + contrapresion +
                                         "', Tiempo = '" + tiempo.ToString().Replace(',', '.') + "', Enfriamiento = '" + enfriamiento.ToString().Replace(',', '.') + "', Ciclo = '" + ciclo.ToString().Replace(',', '.') + "', Cojin = '" + cojin.ToString().Replace(',', '.') + "' WHERE Referencia = " + referencia + " AND Maquina = " + maquina + " AND Version = " + version;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                actualizarMaterial(referencia, maquina, version, codigo, material, codColorante, colorante,
                                    color, tempSecado, tiempoSecado);
                actualizarTempCilindro(referencia, maquina, version, boq, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, refrigeracionCircuito, atempCircuito, atempTemperatura);
                actualizarTempCamCaliente(referencia, maquina, version, Z1, Z2, Z3, Z4, Z5, Z6, Z7, Z8, Z9, Z10,
                                       Z11, Z12, Z13, Z14, Z15, Z16, Z17, Z18, Z19, Z20,
                                       refrigeracionCircuito2, atempCircuito2, atempTemperatura2);
                actualizarInyeccion(referencia, maquina, version, V1, C1, V2, C2, V3, C3, V4, C4, V5, C5,
                                      V6, C6, V7, C7, V8, C8, V9, C9, V10, C10,
                                      V11, C11, V12, C12, Tiempo, limitePresion);
                actualizarPostpresion(referencia, maquina, version, P1, Tp1, P2, Tp2, P3, Tp3, P4, Tp4, P5, Tp5, P6, Ti6, P7, Tp7, P8, Tp8, P9, Tp9, P10, Tp10, tiempoPresion, conmutacion);
                actualizarMarcaCruz(referencia, maquina, version, noyos, hembra, macho, antesExpuls, antesAper, antesCierre,
                            despuesCierre, otros, boquilla, cono, radioLarga, libre, valvula, resistencia,
                            otros2, expulsion, hidraulica, neumatica, normal, arandela125, arandela160, arandela200, arandela250);
            }
            catch (Exception)
            {

            }
        }

        public void ValidarFicha(int referencia, int maquina, int version, string observaciones, string razones, int elaborado, int revisado, int aprobado)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "UPDATE [SMARTH_DB].[dbo].[PARAMETROS_Ficha] SET [Observaciones] = '" + observaciones+"', [Razones]  = '"+razones+"',[INTElaborado] = "+elaborado+",[INTRevisado] = "+revisado+",[INTAprobado]  = "+aprobado+" WHERE[Referencia] = "+ referencia + " AND [Maquina] = "+ maquina + " AND [Version]= "+ version + "";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();

            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
            }
                }

        public void insertarMaterial(int referencia, int maquina, int version, int codigo, string material, string codColorante, string colorante,
                                       string color, string tempSecado, string tiempoSecado)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "INSERT INTO Material (Referencia, Maquina, Version, Codigo, Material, CodColorante, Colorante, Color, TempSecado, TiempoSecado) " +
                                                      " VALUES (" + referencia + "," + maquina + "," + version + "," + codigo + ",'" + material + "','" + codColorante + "','" +
                                                      colorante + "','" + color + "','" + tempSecado + "','" + tiempoSecado + "')";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
            }
        }

        public void actualizarMaterial(int referencia, int maquina, int version, int codigo, string material, string codColorante, string colorante,
                                       string color, string tempSecado, string tiempoSecado)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "UPDATE Material SET Referencia = " + referencia + ", Codigo = " + codigo + ", Material = '" + material + "', CodColorante = '" + codColorante + "', Colorante = '" +
                                                      colorante + "', Color = '" + color + "', TempSecado = '" + tempSecado + "', TiempoSecado = '" + tiempoSecado + "' WHERE Referencia = " + referencia + " AND maquina = " + maquina + " AND version = " + version;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
            }
        }

        public void insertarTempCilindro(int referencia, int maquina, int version, double boq, double T1, double T2, double T3, double T4,
                                         double T5, double T6, double T7, double T8, double T9, double T10)
                                         //string refrigeracionCircuito, string atempCircuito, string atempTemperatura)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[PARAMETROS_TempCilindro] (Referencia, Maquina, Version, Boq, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10) " +
                                                      " VALUES (" + referencia + "," + maquina + "," + version + "," + boq.ToString().Replace(',', '.') + "," + T1.ToString().Replace(',', '.') + "," +
                                                      T2.ToString().Replace(',', '.') + "," + T3.ToString().Replace(',', '.') + "," + T4.ToString().Replace(',', '.') + "," +
                                                      T5.ToString().Replace(',', '.') + "," + T6.ToString().Replace(',', '.') + "," + T7.ToString().Replace(',', '.') + "," +
                                                      T8.ToString().Replace(',', '.') + "," + T9.ToString().Replace(',', '.') + "," + T10.ToString().Replace(',', '.') + ")";
                                                      //,'" + refrigeracionCircuito + "','" +  atempCircuito + "','" + atempTemperatura + "')";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
            }
        }

        public void actualizarTempCilindro(int referencia, int maquina, int version, double boq, double T1, double T2, double T3, double T4,
                                         double T5, double T6, double T7, double T8, double T9, double T10,
                                         string refrigeracionCircuito, string atempCircuito, string atempTemperatura)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "UPDATE [SMARTH_DB].[dbo].[PARAMETROS_TempCilindro] SET Referencia = " + referencia + ", Boq = " + boq.ToString().Replace(',', '.') + ", T1 = " + T1.ToString().Replace(',', '.') + ", T2 = " + T2.ToString().Replace(',', '.') + ", T3 = " +
                                                                     T3.ToString().Replace(',', '.') + ", T4 = " + T4.ToString().Replace(',', '.') + ", T5 = " + T5.ToString().Replace(',', '.') + ", T6 = " + T6.ToString().Replace(',', '.') + ", T7 = " + T7.ToString().Replace(',', '.') + ", T8 = " +
                                                                     T8.ToString().Replace(',', '.') + ", T9 = " + T9.ToString().Replace(',', '.') + ", T10 = " + T10.ToString().Replace(',', '.') + ", RefrigeracionCircuito = '" + refrigeracionCircuito +
                                                                    "', AtempCircuito = '" + atempCircuito + "', AtempTemperatura = '" + atempTemperatura + "' WHERE Referencia = " + referencia + " AND Maquina = " + maquina + " AND Version = " + version;

                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
            }
        }

        public void insertarTempCamCaliente(int referencia, int maquina, int version, double Z1, double Z2, double Z3, double Z4, double Z5, double Z6, double Z7, double Z8, double Z9, double Z10,
                                            double Z11, double Z12, double Z13, double Z14, double Z15, double Z16, double Z17, double Z18, double Z19, double Z20)
                                            //string refrigeracionCircuito, string atempCircuito, string atempTemperatura)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[PARAMETROS_TempCamCaliente] (Referencia, Maquina, Version, Z1, Z2, Z3, Z4, Z5, Z6, Z7, Z8, Z9, Z10, " +
                                                       "Z11, Z12, Z13, Z14, Z15, Z16, Z17, Z18, Z19, Z20)" +
                                                      " VALUES (" + referencia + "," + maquina + "," + version + "," + Z1.ToString().Replace(',', '.') + "," + Z2.ToString().Replace(',', '.') + "," +
                                                      Z3.ToString().Replace(',', '.') + "," + Z4.ToString().Replace(',', '.') + "," + Z5.ToString().Replace(',', '.') + "," +
                                                      Z6.ToString().Replace(',', '.') + "," + Z7.ToString().Replace(',', '.') +
                                                      "," + Z8.ToString().Replace(',', '.') + "," + Z9.ToString().Replace(',', '.') +
                                                      "," + Z10.ToString().Replace(',', '.') + "," + Z11.ToString().Replace(',', '.') +
                                                      "," + Z12.ToString().Replace(',', '.') + "," + Z13.ToString().Replace(',', '.') +
                                                      "," + Z14.ToString().Replace(',', '.') + "," + Z15.ToString().Replace(',', '.') +
                                                      "," + Z16.ToString().Replace(',', '.') + "," + Z17.ToString().Replace(',', '.') +
                                                      "," + Z18.ToString().Replace(',', '.') + "," + Z19.ToString().Replace(',', '.') +
                                                      "," + Z20.ToString().Replace(',', '.') + ")";
                                                      //,'" + refrigeracionCircuito + "','" + atempCircuito + "','" + atempTemperatura + "')";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
            }
        }


        public void actualizarTempCamCaliente(int referencia, int maquina, int version, double Z1, double Z2, double Z3, double Z4, double Z5, double Z6, double Z7, double Z8, double Z9, double Z10,
                                            double Z11, double Z12, double Z13, double Z14, double Z15, double Z16, double Z17, double Z18, double Z19, double Z20,
                                            string refrigeracionCircuito, string atempCircuito, string atempTemperatura)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "UPDATE [SMARTH_DB].[dbo].[PARAMETROS_TempCamCaliente] SET referencia = " + referencia + ", Z1 = " + Z1.ToString().Replace(',', '.') + ", Z2 = " + Z2.ToString().Replace(',', '.') +
                                                                ", Z3 = " + Z3.ToString().Replace(',', '.') + ", Z4 = " + Z4.ToString().Replace(',', '.') + ", Z5 = " + Z5.ToString().Replace(',', '.') +
                                                                ", Z6 = " + Z6.ToString().Replace(',', '.') + ", Z7 = " + Z7.ToString().Replace(',', '.') +
                                                                ", Z8 = " + Z8.ToString().Replace(',', '.') + ", Z9 = " + Z9.ToString().Replace(',', '.') +
                                                                ", Z10 = " + Z10.ToString().Replace(',', '.') + ", Z11 = " + Z11.ToString().Replace(',', '.') +
                                                                ", Z12 = " + Z12.ToString().Replace(',', '.') + ", Z13 = " + Z13.ToString().Replace(',', '.') +
                                                                ", Z14 = " + Z14.ToString().Replace(',', '.') + ", Z15 = " + Z15.ToString().Replace(',', '.') +
                                                                ", Z16 = " + Z16.ToString().Replace(',', '.') + ", Z17 = " + Z17.ToString().Replace(',', '.') +
                                                                ", Z18 = " + Z18.ToString().Replace(',', '.') + ", Z19 = " + Z19.ToString().Replace(',', '.') +
                                                                ", Z20 = " + Z20.ToString().Replace(',', '.') + ", RefrigeracionCircuito = '" + refrigeracionCircuito +
                                                                "', AtempCircuito = '" + atempCircuito + "', AtempTemperatura = '" + atempTemperatura + "' WHERE Referencia = " + referencia + " AND maquina = " + maquina + " AND version = " + version;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
            }
        }

        public void InsertarInyeccionV2(int referencia, int maquina, int version, double V1, double C1, double V2, double C2, double V3, double C3, double V4, double C4, double V5, double C5,
                                           double V6, double C6, double V7, double C7, double V8, double C8, double V9, double C9, double V10, double C10,
                                           double V11, double C11, double V12, double C12, string tiempo, string limitePresion, string limitepresionReal)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                if (tiempo == null)
                    tiempo = "";
                if (limitePresion == null)
                    limitePresion = "";
                if (limitepresionReal == null)
                    limitepresionReal = "";
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[PARAMETROS_Inyeccion] (referencia, Maquina, Version, V1, C1, V2, C2, V3, C3, V4, C4, V5, C5, V6, C6, V7, C7, V8, C8, V9, C9, V10, C10, V11, C11, V12,C12, Tiempo, LimitePresion,LimitePresionReal) " +
                                                      " VALUES (" + referencia + "," + maquina + "," + version + "," + V1.ToString().Replace(',', '.') + "," + C1.ToString().Replace(',', '.') + "," + V2.ToString().Replace(',', '.') +
                                                      "," + C2.ToString().Replace(',', '.') + "," + V3.ToString().Replace(',', '.') + "," + C3.ToString().Replace(',', '.') + "," + V4.ToString().Replace(',', '.') +
                                                      "," + C4.ToString().Replace(',', '.') + "," + V5.ToString().Replace(',', '.') + "," + C5.ToString().Replace(',', '.') + "," + V6.ToString().Replace(',', '.') +
                                                      "," + C6.ToString().Replace(',', '.') + "," + V7.ToString().Replace(',', '.') + "," + C7.ToString().Replace(',', '.') + "," + V8.ToString().Replace(',', '.') +
                                                      "," + C8.ToString().Replace(',', '.') + "," + V9.ToString().Replace(',', '.') + "," + C9.ToString().Replace(',', '.') + "," + V10.ToString().Replace(',', '.') +
                                                      "," + C10.ToString().Replace(',', '.') + "," + V11.ToString().Replace(',', '.') + "," + C11.ToString().Replace(',', '.') + "," + V12.ToString().Replace(',', '.') +
                                                      "," + C12.ToString().Replace(',', '.') + ",'" + tiempo.ToString().Replace(',', '.') + "','" + limitePresion.ToString().Replace(',', '.') + "','" + limitepresionReal.ToString().Replace(',', '.') + "')";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
            }
        }

        public void insertarInyeccion(int referencia, int maquina, int version, double V1, double C1, double V2, double C2, double V3, double C3, double V4, double C4, double V5, double C5,
                                           double V6, double C6, double V7, double C7, double V8, double C8, double V9, double C9, double V10, double C10,
                                           double V11, double C11, double V12, double C12, string tiempo, string limitePresion)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                if (tiempo == null)
                    tiempo = "";
                if (limitePresion == null)
                    limitePresion = "";
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[PARAMETROS_Inyeccion] (referencia, Maquina, Version, V1, C1, V2, C2, V3, C3, V4, C4, V5, C5, V6, C6, V7, C7, V8, C8, V9, C9, V10, C10, V11, C11, V12,C12, Tiempo, LimitePresion) " +
                                                      " VALUES (" + referencia + "," + maquina +  "," + version + "," + V1.ToString().Replace(',', '.') + "," + C1.ToString().Replace(',', '.') + "," + V2.ToString().Replace(',', '.') +
                                                      "," + C2.ToString().Replace(',', '.') + "," + V3.ToString().Replace(',', '.') + "," + C3.ToString().Replace(',', '.') + "," + V4.ToString().Replace(',', '.') +
                                                      "," + C4.ToString().Replace(',', '.') + "," + V5.ToString().Replace(',', '.') + "," + C5.ToString().Replace(',', '.') + "," + V6.ToString().Replace(',', '.') +
                                                      "," + C6.ToString().Replace(',', '.') + "," + V7.ToString().Replace(',', '.') + "," + C7.ToString().Replace(',', '.') + "," + V8.ToString().Replace(',', '.') +
                                                      "," + C8.ToString().Replace(',', '.') + "," + V9.ToString().Replace(',', '.') + "," + C9.ToString().Replace(',', '.') + "," + V10.ToString().Replace(',', '.') +
                                                      "," + C10.ToString().Replace(',', '.') + "," + V11.ToString().Replace(',', '.') + "," + C11.ToString().Replace(',', '.') + "," + V12.ToString().Replace(',', '.') +
                                                      "," + C12.ToString().Replace(',', '.') + ",'" + tiempo.ToString().Replace(',', '.') + "','" + limitePresion.ToString().Replace(',', '.') + "')";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
            }
        }

        public void actualizarInyeccion(int referencia, int maquina, int version, double V1, double C1, double V2, double C2, double V3, double C3, double V4, double C4, double V5, double C5,
                                           double V6, double C6, double V7, double C7, double V8, double C8, double V9, double C9, double V10, double C10,
                                           double V11, double C11, double V12, double C12, string tiempo, string limitePresion)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                if (tiempo == null)
                    tiempo = "";
                if (limitePresion == "")
                    limitePresion = "";
                string sql = "UPDATE [SMARTH_DB].[dbo].[PARAMETROS_Inyeccion] SET Referencia = " + referencia + ", V1 = " + V1.ToString().Replace(',', '.') + ", C1 = " + C1.ToString().Replace(',', '.') +
                                                    ", V2 = " + V2.ToString().Replace(',', '.') + ", C2 = " + C2.ToString().Replace(',', '.') + ", V3 = " + V3.ToString().Replace(',', '.') +
                                                    ", C3 = " + C3.ToString().Replace(',', '.') + ", V4 = " + V4.ToString().Replace(',', '.') +
                                                      ", C4 = " + C4.ToString().Replace(',', '.') + ", V5 = " + V5.ToString().Replace(',', '.') +
                                                      ", C5 = " + C5.ToString().Replace(',', '.') + ", V6 = " + V6.ToString().Replace(',', '.') +
                                                      ", C6 = " + C6.ToString().Replace(',', '.') + ", V7 = " + V7.ToString().Replace(',', '.') +
                                                      ", C7 = " + C7.ToString().Replace(',', '.') + ", V8 = " + V8.ToString().Replace(',', '.') +
                                                      ", C8 = " + C8.ToString().Replace(',', '.') + ", V9 = " + V9.ToString().Replace(',', '.') +
                                                      ", C9 = " + C9.ToString().Replace(',', '.') + ", V10 = " + V10.ToString().Replace(',', '.') +
                                                      ", C10 = " + C10.ToString().Replace(',', '.') + ", V11 = " + V11.ToString().Replace(',', '.') +
                                                      ", C11 = " + C11.ToString().Replace(',', '.') + ", V12 = " + V12.ToString().Replace(',', '.') +
                                                      ", C12 = " + C12.ToString().Replace(',', '.') + ", Tiempo = '" + tiempo.ToString().Replace(',', '.') + "', LimitePresion = '" + limitePresion + "' WHERE Referencia = " + referencia + " AND Maquina = " + maquina + " AND Version = " + version;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
            }
        }

        public void insertarPostpresion(int referencia, int maquina, int version, double P1, double T1, double P2, double T2, double P3, double T3, double P4, double T4, double P5, double T5,
                                          double P6, double T6, double P7, double T7, double P8, double T8, double P9, double T9, double P10, double T10, string tiempoPresion, string conmutacion)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                if (tiempoPresion == null)
                    tiempoPresion = "";
                if (conmutacion == null)
                    conmutacion = "";
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[PARAMETROS_Postpresion] (Referencia, Maquina, Version, P1, T1, P2, T2, P3, T3, P4, T4, P5, T5, P6, T6, P7, T7, P8, T8, P9, T9, P10, T10, TiempoPresion, Conmutacion) " +
                                                      " VALUES (" + referencia + "," + maquina + "," + version + "," + P1.ToString().Replace(',', '.') + "," + T1.ToString().Replace(',', '.') +
                                                      "," + P2.ToString().Replace(',', '.') + "," + T2.ToString().Replace(',', '.') + "," + P3.ToString().Replace(',', '.') +
                                                      "," + T3.ToString().Replace(',', '.') + "," + P4.ToString().Replace(',', '.') + "," + T4.ToString().Replace(',', '.') +
                                                      "," + P5.ToString().Replace(',', '.') + "," + T5.ToString().Replace(',', '.') + "," + P6.ToString().Replace(',', '.') +
                                                      "," + T6.ToString().Replace(',', '.') + "," + P7.ToString().Replace(',', '.') + "," + T7.ToString().Replace(',', '.') +
                                                      "," + P8.ToString().Replace(',', '.') + "," + T8.ToString().Replace(',', '.') + "," + P9.ToString().Replace(',', '.') +
                                                      "," + T9.ToString().Replace(',', '.') + "," + P10.ToString().Replace(',', '.') + "," + T10.ToString().Replace(',', '.') +
                                                      ",'" + tiempoPresion.ToString().Replace(',', '.') + "','" + conmutacion.ToString().Replace(',', '.') + "')";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
            }
        }

        public void actualizarPostpresion(int referencia, int maquina, int version, double P1, double T1, double P2, double T2, double P3, double T3, double P4, double T4, double P5, double T5,
                                          double P6, double T6, double P7, double T7, double P8, double T8, double P9, double T9, double P10, double T10, string tiempoPresion, string conmutacion)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                if (tiempoPresion == null)
                    tiempoPresion = "";
                if (conmutacion == null)
                    conmutacion = "";
                string sql = "UPDATE [SMARTH_DB].[dbo].[PARAMETROS_Postpresion] SET Referencia = " + referencia + ", P1 = " + P1.ToString().Replace(',', '.') + ", T1 = " + T1.ToString().Replace(',', '.') +
                                                                ", P2 = " + P2.ToString().Replace(',', '.') + ", T2 = " + T2.ToString().Replace(',', '.') + ", P3 = " + P3.ToString().Replace(',', '.') +
                                                                ", T3 = " + T3.ToString().Replace(',', '.') + ", P4 = " + P4.ToString().Replace(',', '.') + ", T4 = " + T4.ToString().Replace(',', '.') +
                                                                ", P5 = " + P5.ToString().Replace(',', '.') + ", T5 = " + T5.ToString().Replace(',', '.') + ", P6 = " + P6.ToString().Replace(',', '.') +
                                                                ", T6 = " + T6.ToString().Replace(',', '.') + ", P7 = " + P7.ToString().Replace(',', '.') + ", T7 = " + T7.ToString().Replace(',', '.') +
                                                                ", P8 = " + P8.ToString().Replace(',', '.') + ", T8 = " + T8.ToString().Replace(',', '.') + ", P9 = " + P9.ToString().Replace(',', '.') +
                                                                ", T9 = " + T9.ToString().Replace(',', '.') + ", P10 = " + P10.ToString().Replace(',', '.') + ", T10 = " + T10.ToString().Replace(',', '.') +
                                                                ", TiempoPresion = '" + tiempoPresion.ToString().Replace(',', '.') + "', Conmutacion = '" + conmutacion.ToString().Replace(',', '.') +
                                                                "' WHERE Referencia = " + referencia + " AND Maquina = " + maquina + " AND Version = " + version;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
            }
        }

        public void insertarMarcaCruz(int referencia, int maquina, int version, int noyos, int hembra, int macho, int antesExpuls, int antesAper, int antesCierre,
                                int despuesCierre, int otros, int boquilla, int cono, int radioLarga, int libre, int valvula, int resistencia,
                                int otros2, int expulsion, int hidraulica, int neumatica, int normal, int arandela125, int arandela160, int arandela200, int arandela250, string MarcasOtrosText)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[PARAMETROS_MarcaCruz] (Referencia, Maquina, Version, Noyos, Hembra, Macho, AntesExpuls, AntesApert, AntesCierre, DespuesCierre, Otros, Boquilla," +
                                                " Cono, RadioLarga, Libre, Valvula, Resistencia, Otros2, Expulsion, Hidraulica, Neumatica, Normal, Arandela125, Arandela160, Arandela200, Arandela250, NotasMarcaCruz) " +
                                                      " VALUES (" + referencia + "," + maquina + "," + version + "," + noyos + "," + hembra + "," + macho +
                                                      "," + antesExpuls + "," + antesAper + "," + antesCierre + "," + despuesCierre +
                                                      "," + otros + "," + boquilla + "," + cono + "," + radioLarga + "," + libre + "," + valvula + "," + resistencia + "," + otros2 +
                                                      "," + expulsion + "," + hidraulica + "," + neumatica + "," + normal + "," + arandela125 + "," + arandela160 + "," + arandela200 + "," + arandela250 + ",'"+ MarcasOtrosText +"')";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
            }
        }

        public void actualizarMarcaCruz(int referencia, int maquina, int version, int noyos, int hembra, int macho, int antesExpuls, int antesAper, int antesCierre,
                                int despuesCierre, int otros, int boquilla, int cono, int radioLarga, int libre, int valvula, int resistencia,
                                int otros2, int expulsion, int hidraulica, int neumatica, int normal, int arandela125, int arandela160, int arandela200, int arandela250)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "UPDATE [SMARTH_DB].[dbo].[PARAMETROS_MarcaCruz] SET Referencia = " + referencia + ", Noyos = " + noyos + ", Hembra = " + hembra + ", Macho = " + macho + ", AntesExpuls = " + antesExpuls +
                                                    ", AntesApert = " + antesAper + ", AntesCierre = " + antesCierre + ", DespuesCierre = " + despuesCierre +
                                                      ", Otros = " + otros + ", Boquilla = " + boquilla + ", Cono = " + cono + ", RadioLarga = " + radioLarga + ", Libre = " + libre +
                                                      ", Valvula = " + valvula + ", Resistencia = " + resistencia + ", Otros2 = " + otros2 + ", Expulsion = " + expulsion + ", Hidraulica = " + hidraulica +
                                                      ", Neumatica = " + neumatica + ", Normal = " + normal + ", Arandela125 = " + arandela125 + ", Arandela160 = " + arandela160 + ", Arandela200 = " + arandela200 + ", Arandela250 = " + arandela250 +
                                                      " WHERE Referencia = " + referencia + " AND Maquina = " + maquina + " AND Version = " + version;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
            }
        }

        public void insertarInyeccionSecuencial(int referencia, int maquina, int version, double abrir1_1, double abrir1_2, double abrir1_3, double abrir1_4,
                                  double abrir1_5, double abrir1_6, double abrir1_7, double abrir1_8, double abrir1_9, double abrir1_10, double cerrar1_1, double cerrar1_2,
                                  double cerrar1_3, double cerrar1_4, double cerrar1_5, double cerrar1_6, double cerrar1_7, double cerrar1_8, double cerrar1_9, double cerrar1_10,
                                  double abrir2_1, double abrir2_2, double abrir2_3, double abrir2_4, double abrir2_5, double abrir2_6, double abrir2_7, double abrir2_8, double abrir2_9, double abrir2_10,
                                  double cerrar2_1, double cerrar2_2, double cerrar2_3, double cerrar2_4, double cerrar2_5, double cerrar2_6, double cerrar2_7, double cerrar2_8, double cerrar2_9, double cerrar2_10,
                                  double TprestPost_1, double TprestPost_2, double TprestPost_3, double TprestPost_4, double TprestPost_5, double TprestPost_6, double TprestPost_7, double TprestPost_8, double TprestPost_9, double TprestPost_10,
                                  double TiempoRetardo_1, double TiempoRetardo_2, double TiempoRetardo_3, double TiempoRetardo_4, double TiempoRetardo_5, double TiempoRetardo_6, double TiempoRetardo_7, double TiempoRetardo_8, double TiempoRetardo_9, double TiempoRetardo_10, string Anotaciones)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[PARAMETROS_InyeccionSecuencial] (Referencia, Maquina, Version, abrir1_1, abrir1_2, abrir1_3, abrir1_4, " +
                                  "abrir1_5, abrir1_6, abrir1_7, abrir1_8, abrir1_9, abrir1_10, cerrar1_1, cerrar1_2, " +
                                  "cerrar1_3, cerrar1_4, cerrar1_5, cerrar1_6, cerrar1_7, cerrar1_8, cerrar1_9, cerrar1_10, " +
                                  "abrir2_1, abrir2_2, abrir2_3, abrir2_4, abrir2_5, abrir2_6, abrir2_7, abrir2_8, abrir2_9, abrir2_10, " +
                                  "cerrar2_1, cerrar2_2, cerrar2_3, cerrar2_4, cerrar2_5, cerrar2_6, cerrar2_7, cerrar2_8, cerrar2_9, cerrar2_10, " +
                                  "TprestPost_1, TprestPost_2, TprestPost_3, TprestPost_4, TprestPost_5, TprestPost_6, TprestPost_7, TprestPost_8, TprestPost_9, TprestPost_10, " +
                                  "TiempoRetardo_1, TiempoRetardo_2, TiempoRetardo_3, TiempoRetardo_4, TiempoRetardo_5, TiempoRetardo_6, TiempoRetardo_7, TiempoRetardo_8, TiempoRetardo_9, TiempoRetardo_10, Anotaciones) " +
                                  " VALUES (" + referencia + "," + maquina + "," + version + "," + abrir1_1.ToString().Replace(',', '.') + "," + abrir1_2.ToString().Replace(',', '.') + "," + abrir1_3.ToString().Replace(',', '.') + "," + abrir1_4.ToString().Replace(',', '.') + "," +
                                  abrir1_5.ToString().Replace(',', '.') + "," + abrir1_6.ToString().Replace(',', '.') + "," + abrir1_7.ToString().Replace(',', '.') + "," + abrir1_8.ToString().Replace(',', '.') + "," + abrir1_9.ToString().Replace(',', '.') + "," + abrir1_10.ToString().Replace(',', '.') + "," + cerrar1_1.ToString().Replace(',', '.') + "," + cerrar1_2.ToString().Replace(',', '.') + "," +
                                  cerrar1_3.ToString().Replace(',', '.') + "," + cerrar1_4.ToString().Replace(',', '.') + "," + cerrar1_5.ToString().Replace(',', '.') + "," + cerrar1_6.ToString().Replace(',', '.') + "," + cerrar1_7.ToString().Replace(',', '.') + "," + cerrar1_8.ToString().Replace(',', '.') + "," + cerrar1_9.ToString().Replace(',', '.') + "," + cerrar1_10.ToString().Replace(',', '.') + "," +
                                  abrir2_1.ToString().Replace(',', '.') + "," + abrir2_2.ToString().Replace(',', '.') + "," + abrir2_3.ToString().Replace(',', '.') + "," + abrir2_4.ToString().Replace(',', '.') + "," + abrir2_5.ToString().Replace(',', '.') + "," + abrir2_6.ToString().Replace(',', '.') + "," + abrir2_7.ToString().Replace(',', '.') + "," + abrir2_8.ToString().Replace(',', '.') + "," + abrir2_9.ToString().Replace(',', '.') + "," + abrir2_10.ToString().Replace(',', '.') + "," +
                                  cerrar2_1.ToString().Replace(',', '.') + "," + cerrar2_2.ToString().Replace(',', '.') + "," + cerrar2_3.ToString().Replace(',', '.') + "," + cerrar2_4.ToString().Replace(',', '.') + "," + cerrar2_5.ToString().Replace(',', '.') + "," + cerrar2_6.ToString().Replace(',', '.') + "," + cerrar2_7.ToString().Replace(',', '.') + "," + cerrar2_8.ToString().Replace(',', '.') + "," + cerrar2_9.ToString().Replace(',', '.') + "," + cerrar2_10.ToString().Replace(',', '.') + "," +
                                  TprestPost_1.ToString().Replace(',', '.') + "," + TprestPost_2.ToString().Replace(',', '.') + "," + TprestPost_3.ToString().Replace(',', '.') + "," + TprestPost_4.ToString().Replace(',', '.') + "," + TprestPost_5.ToString().Replace(',', '.') + "," + TprestPost_6.ToString().Replace(',', '.') + "," + TprestPost_7.ToString().Replace(',', '.') + "," + TprestPost_8.ToString().Replace(',', '.') + "," + TprestPost_9.ToString().Replace(',', '.') + "," + TprestPost_10.ToString().Replace(',', '.') + "," +
                                  TiempoRetardo_1.ToString().Replace(',', '.') + "," + TiempoRetardo_2.ToString().Replace(',', '.') + "," + TiempoRetardo_3.ToString().Replace(',', '.') + "," + TiempoRetardo_4.ToString().Replace(',', '.') + "," + TiempoRetardo_5.ToString().Replace(',', '.') + "," + TiempoRetardo_6.ToString().Replace(',', '.') + "," + TiempoRetardo_7.ToString().Replace(',', '.') + "," + TiempoRetardo_8.ToString().Replace(',', '.') + "," + TiempoRetardo_9.ToString().Replace(',', '.') + "," + TiempoRetardo_10.ToString().Replace(',', '.') + ", '" + Anotaciones + "')";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
            }
        }

        public void InsertarToleranciasV2(int referencia, int maquina, int version, double TiempoInyeccionNValDouble, double TiempoInyeccionMValDouble, double LimitePresionNValDouble, double LimitePresionMValDouble, double ConmuntaciontolNValDouble, double ConmuntaciontolMValDouble, double TiempoPresiontolNValDouble,
            double TiempoPresiontolMValDouble, double TNvcargavalDouble, double TMvcargavalDouble, double TNcargavalDouble, double TMcargavalDouble, double TNdescomvalDouble, double TMdescomvalDouble, double TNcontrapvalDouble,
            double TMcontrapvalDouble, double TNTiempdosvalDouble, double TMTiempdosvalDouble, double TNEnfriamvalDouble, double TMEnfriamvalDouble, double TNCiclovalDouble, double TMCiclovalDouble, double TNCojinvalDouble, double TMCojinvalDouble,
            string TbOperacionText1, string TbOperacionText2, string TbOperacionText3, string TbOperacionText4, string TbOperacionText5,
            double tbTOLCarInyeccion, double tbTOLCamCaliente, double TbTOLCilindro, double tbTOLPostPresion, double tbTOLSecCota, double tbTOLSecTiempo, double LimitePresionNRealValDouble, double LimitePresionMRealValDouble)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[PARAMETROS_Tolerancias] (Referencia, Maquina, Version, TiempoInyeccionNVal, TiempoInyeccionMVal, LimitePresionNVal, LimitePresionMVal, ConmuntaciontolNVal, ConmuntaciontolMVal, TiempoPresiontolNVal, TiempoPresiontolMVal, TNvcargaval, TMvcargaval, TNcargaval, TMcargaval, TNdescomval, TMdescomval, TNcontrapval, TMcontrapval, TNTiempdosval, TMTiempdosval, TNEnfriamval, TMEnfriamval, TNCicloval, TMCicloval, TNCojinval, TMCojinval, ArranqueLIN1, ArranqueLIN2, ArranqueLIN3, ArranqueLIN4, ArranqueLIN5" +
                                  ",[TOLCilindro],[TOLCamCaliente],[TOLCarInyeccion],[TOLPostPresion],[TOLSecCota],[TOLSecTiempo],[LimitePresionRealNVal],[LimitePresionRealMVal])" +
                                  " VALUES (" + referencia + "," + maquina + "," + version + "," + TiempoInyeccionNValDouble.ToString().Replace(',', '.') + "," + TiempoInyeccionMValDouble.ToString().Replace(',', '.') + "," + LimitePresionNValDouble.ToString().Replace(',', '.') + "," + LimitePresionMValDouble.ToString().Replace(',', '.') + "," +
                                  ConmuntaciontolNValDouble.ToString().Replace(',', '.') + "," + ConmuntaciontolMValDouble.ToString().Replace(',', '.') + "," + TiempoPresiontolNValDouble.ToString().Replace(',', '.') + "," + TiempoPresiontolMValDouble.ToString().Replace(',', '.') + "," + TNvcargavalDouble.ToString().Replace(',', '.') + "," + TMvcargavalDouble.ToString().Replace(',', '.') + "," + TNcargavalDouble.ToString().Replace(',', '.') + "," + TMcargavalDouble.ToString().Replace(',', '.') + "," +
                                  TNdescomvalDouble.ToString().Replace(',', '.') + "," + TMdescomvalDouble.ToString().Replace(',', '.') + "," + TNcontrapvalDouble.ToString().Replace(',', '.') + "," + TMcontrapvalDouble.ToString().Replace(',', '.') + "," + TNTiempdosvalDouble.ToString().Replace(',', '.') + "," + TMTiempdosvalDouble.ToString().Replace(',', '.') + "," + TNEnfriamvalDouble.ToString().Replace(',', '.') + "," + TMEnfriamvalDouble.ToString().Replace(',', '.') + "," +
                                  TNCiclovalDouble.ToString().Replace(',', '.') + "," + TMCiclovalDouble.ToString().Replace(',', '.') + "," + TNCojinvalDouble.ToString().Replace(',', '.') + "," + TMCojinvalDouble.ToString().Replace(',', '.') +
                                  ",'" + TbOperacionText1 + "','" + TbOperacionText2 + "','" + TbOperacionText3 + "','" + TbOperacionText4 + "','" + TbOperacionText5 + "'," +
                                  TbTOLCilindro.ToString().Replace(',', '.') + "," +tbTOLCamCaliente.ToString().Replace(',', '.') + ","+tbTOLCarInyeccion.ToString().Replace(',', '.') + ","+tbTOLPostPresion.ToString().Replace(',', '.') + ","+tbTOLSecCota.ToString().Replace(',', '.') + ","+tbTOLSecTiempo.ToString().Replace(',', '.') + ","+LimitePresionNRealValDouble.ToString().Replace(',', '.') + ","+LimitePresionMRealValDouble.ToString().Replace(',', '.') + ")";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
            }
        }


        public void insertarTolerancias(int referencia, int maquina, int version, double TiempoInyeccionNValDouble, double TiempoInyeccionMValDouble, double LimitePresionNValDouble, double LimitePresionMValDouble, double ConmuntaciontolNValDouble, double ConmuntaciontolMValDouble, double TiempoPresiontolNValDouble,
                    double TiempoPresiontolMValDouble, double TNvcargavalDouble, double TMvcargavalDouble, double TNcargavalDouble, double TMcargavalDouble, double TNdescomvalDouble, double TMdescomvalDouble, double TNcontrapvalDouble,
                    double TMcontrapvalDouble, double TNTiempdosvalDouble, double TMTiempdosvalDouble, double TNEnfriamvalDouble, double TMEnfriamvalDouble, double TNCiclovalDouble, double TMCiclovalDouble, double TNCojinvalDouble, double TMCojinvalDouble,
                    string TbOperacionText1,string TbOperacionText2,string TbOperacionText3,string TbOperacionText4, string TbOperacionText5)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[PARAMETROS_Tolerancias] (Referencia, Maquina, Version, TiempoInyeccionNVal, TiempoInyeccionMVal, LimitePresionNVal, LimitePresionMVal, ConmuntaciontolNVal, ConmuntaciontolMVal, TiempoPresiontolNVal, TiempoPresiontolMVal, TNvcargaval, TMvcargaval, TNcargaval, TMcargaval, TNdescomval, TMdescomval, TNcontrapval, TMcontrapval, TNTiempdosval, TMTiempdosval, TNEnfriamval, TMEnfriamval, TNCicloval, TMCicloval, TNCojinval, TMCojinval, ArranqueLIN1, ArranqueLIN2, ArranqueLIN3, ArranqueLIN4, ArranqueLIN5)" +
                                  " VALUES (" + referencia + "," + maquina + "," + version + "," + TiempoInyeccionNValDouble.ToString().Replace(',', '.') + "," + TiempoInyeccionMValDouble.ToString().Replace(',', '.') + "," + LimitePresionNValDouble.ToString().Replace(',', '.') + "," + LimitePresionMValDouble.ToString().Replace(',', '.') + "," +
                                  ConmuntaciontolNValDouble.ToString().Replace(',', '.') + "," + ConmuntaciontolMValDouble.ToString().Replace(',', '.') + "," + TiempoPresiontolNValDouble.ToString().Replace(',', '.') + "," + TiempoPresiontolMValDouble.ToString().Replace(',', '.') + "," + TNvcargavalDouble.ToString().Replace(',', '.') + "," + TMvcargavalDouble.ToString().Replace(',', '.') + "," + TNcargavalDouble.ToString().Replace(',', '.') + "," + TMcargavalDouble.ToString().Replace(',', '.') + "," +
                                  TNdescomvalDouble.ToString().Replace(',', '.') + "," + TMdescomvalDouble.ToString().Replace(',', '.') + "," + TNcontrapvalDouble.ToString().Replace(',', '.') + "," + TMcontrapvalDouble.ToString().Replace(',', '.') + "," + TNTiempdosvalDouble.ToString().Replace(',', '.') + "," + TMTiempdosvalDouble.ToString().Replace(',', '.') + "," + TNEnfriamvalDouble.ToString().Replace(',', '.') + "," + TMEnfriamvalDouble.ToString().Replace(',', '.') + "," +
                                  TNCiclovalDouble.ToString().Replace(',', '.') + "," + TMCiclovalDouble.ToString().Replace(',', '.') + "," + TNCojinvalDouble.ToString().Replace(',', '.') + "," + TMCojinvalDouble.ToString().Replace(',', '.') + 
                                  ",'"+ TbOperacionText1 +"','" + TbOperacionText2 +"','" + TbOperacionText3 + "','" + TbOperacionText4 + "','" + TbOperacionText5 + "')";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
            }
        }

        public void actualizarInyeccionSecuencial(int referencia, int maquina, int version, double abrir1_1, double abrir1_2, double abrir1_3, double abrir1_4,
                                  double abrir1_5, double abrir1_6, double abrir1_7, double abrir1_8, double abrir1_9, double abrir1_10, double cerrar1_1, double cerrar1_2,
                                  double cerrar1_3, double cerrar1_4, double cerrar1_5, double cerrar1_6, double cerrar1_7, double cerrar1_8, double cerrar1_9, double cerrar1_10,
                                  double abrir2_1, double abrir2_2, double abrir2_3, double abrir2_4, double abrir2_5, double abrir2_6, double abrir2_7, double abrir2_8, double abrir2_9, double abrir2_10,
                                  double cerrar2_1, double cerrar2_2, double cerrar2_3, double cerrar2_4, double cerrar2_5, double cerrar2_6, double cerrar2_7, double cerrar2_8, double cerrar2_9, double cerrar2_10,
                                  double TprestPost_1, double TprestPost_2, double TprestPost_3, double TprestPost_4, double TprestPost_5, double TprestPost_6, double TprestPost_7, double TprestPost_8, double TprestPost_9, double TprestPost_10,
                                  double TiempoRetardo_1, double TiempoRetardo_2, double TiempoRetardo_3, double TiempoRetardo_4, double TiempoRetardo_5, double TiempoRetardo_6, double TiempoRetardo_7, double TiempoRetardo_8, double TiempoRetardo_9, double TiempoRetardo_10)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "UPDATE [SMARTH_DB].[dbo].[PARAMETROS_InyeccionSecuencial] SET referencia = " + referencia + ", abrir1_1 = " + abrir1_1 + ", abrir1_2 =" + abrir1_2 + ", abrir1_3 = " + abrir1_3 + ", abrir1_4 = " + abrir1_4 + ", abrir1_5 = " +
                                  abrir1_5 + ", abrir1_6 = " + abrir1_6 + ", abrir1_7 = " + abrir1_7 + ", abrir1_8 = " + abrir1_8 + ", abrir1_9 = " + abrir1_9 + ", abrir1_10 = " + abrir1_10 + ", cerrar1_1 = " + cerrar1_1 + ", cerrar1_2 = " + cerrar1_2 + ", cerrar1_3 = " +
                                  cerrar1_3 + ", cerrar1_4 = " + cerrar1_4 + ", cerrar1_5 " + cerrar1_5 + ", cerrar1_6 =" + cerrar1_6 + ", cerrar1_7 = " + cerrar1_7 + ", cerrar1_8 = " + cerrar1_8 + ", cerrar1_9 = " + cerrar1_9 + ", cerrar1_10 = " + cerrar1_10 + ", abrir2_1 = " +
                                  abrir2_1 + ", abrir2_2 = " + abrir2_2 + ", abrir2_3 = " + abrir2_3 + ", abrir2_4 = " + abrir2_4 + ", abrir2_5 = " + abrir2_5 + ", abrir2_6 = " + abrir2_6 + ", abrir2_7 = " + abrir2_7 + ", abrir2_8 = " + abrir2_8 + ", abrir2_9 = " + abrir2_9 + ", abrir2_10 = " + abrir2_10 + ", cerrar2_1 = " +
                                  cerrar2_1 + ", cerrar2_2 = " + cerrar2_2 + ", cerrar2_3 = " + cerrar2_3 + ", cerrar2_4 = " + cerrar2_4 + ", cerrar2_5 = " + cerrar2_5 + ", cerrar2_6 = " + cerrar2_6 + ", cerrar2_7 = " + cerrar2_7 + ", cerrar2_8 = " + cerrar2_8 + ", cerrar2_9 = " + cerrar2_9 + ", cerrar2_10 = " + cerrar2_10 + ", TprestPost_1 = " +
                                  TprestPost_1 + ", TprestPost_2 = " + TprestPost_2 + ", TprestPost_3 = " + TprestPost_3 + ", TprestPost_4 = " + TprestPost_4 + ", TprestPost_5 = " + TprestPost_5 + ", TprestPost_6 = " + TprestPost_6 + ", TprestPost_7 = " + TprestPost_7 + ", TprestPost_8 = " + TprestPost_8 +
                                  ", TprestPost_9 = " + TprestPost_9 + ", TprestPost_10 = " + TprestPost_10 + ", TiempoRetardo_1 = " + TiempoRetardo_1 + ", TiempoRetardo_2 = " + TiempoRetardo_2 + ", TiempoRetardo_3 = " + TiempoRetardo_3 + ", TiempoRetardo_4 = " +
                                  TiempoRetardo_4 + ", TiempoRetardo_5 = " + TiempoRetardo_5 + ", TiempoRetardo_6 = " + TiempoRetardo_6 + ", TiempoRetardo_7 = " + TiempoRetardo_7 +
                                  ", TiempoRetardo_8 = " + TiempoRetardo_8 + ", TiempoRetardo_9 = " + TiempoRetardo_9 + ", TiempoRetardo_10 = " + TiempoRetardo_10 + " WHERE Referencia = " + referencia + " AND Maquina = " + maquina + " AND Version = " + version;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
            }
        }

        public void insertarAtemperado(int referencia, int maquina, int version,
                                  int AtempTipoF, int CircuitoF1, int CircuitoF2, int CircuitoF3, int CircuitoF4, int CircuitoF5, int CircuitoF6,
                                  string CaudalF1, string CaudalF2, string CaudalF3, string CaudalF4, string CaudalF5, string CaudalF6,
                                  string TemperaturaF1, string TemperaturaF2, string TemperaturaF3, string TemperaturaF4, string TemperaturaF5, string TemperaturaF6,
                                  int EntradaF1, int EntradaF2, int EntradaF3, int EntradaF4, int EntradaF5, int EntradaF6,
                                  int AtempTipoM, int CircuitoM1, int CircuitoM2, int CircuitoM3, int CircuitoM4, int CircuitoM5, int CircuitoM6,
                                  string CaudalM1, string CaudalM2, string CaudalM3, string CaudalM4, string CaudalM5, string CaudalM6,
                                  string TemperaturaM1, string TemperaturaM2, string TemperaturaM3, string TemperaturaM4, string TemperaturaM5, string TemperaturaM6,
                                  int EntradaM1, int EntradaM2, int EntradaM3, int EntradaM4, int EntradaM5, int EntradaM6) 
           {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[PARAMETROS_AtemperadoV1] (Referencia, Maquina, Version," +
                                  "AtempTipoF, CircuitoF1, CircuitoF2, CircuitoF3, CircuitoF4, CircuitoF5, CircuitoF6," +
                                  "CaudalF1, CaudalF2, CaudalF3, CaudalF4, CaudalF5, CaudalF6," +
                                  "TemperaturaF1, TemperaturaF2, TemperaturaF3, TemperaturaF4, TemperaturaF5, TemperaturaF6," +
                                  "EntradaF1, EntradaF2, EntradaF3, EntradaF4, EntradaF5, EntradaF6," +
                                  "AtempTipoM, CircuitoM1, CircuitoM2, CircuitoM3, CircuitoM4, CircuitoM5, CircuitoM6," +
                                  "CaudalM1, CaudalM2, CaudalM3, CaudalM4, CaudalM5, CaudalM6," +
                                  "TemperaturaM1, TemperaturaM2, TemperaturaM3, TemperaturaM4, TemperaturaM5, TemperaturaM6," +
                                  "EntradaM1, EntradaM2, EntradaM3, EntradaM4, EntradaM5, EntradaM6) " +
                                  " VALUES (" + referencia + "," + maquina + "," + version + "," + AtempTipoF + "," + CircuitoF1 + "," + CircuitoF2 + "," + CircuitoF3 + "," + CircuitoF4 + "," + CircuitoF5 + "," + CircuitoF6 + ",'" +
                                  CaudalF1 + "','" + CaudalF2 + "','" + CaudalF3 + "','" + CaudalF4 + "','" + CaudalF5 + "','" + CaudalF6 + "','" +
                                  TemperaturaF1 + "','" + TemperaturaF2 + "','" + TemperaturaF3 + "','" + TemperaturaF4 + "','" + TemperaturaF5 + "','" + TemperaturaF6 + "'," +
                                  EntradaF1 + "," + EntradaF2 + "," + EntradaF3 + "," + EntradaF4 + "," + EntradaF5 + "," + EntradaF6 + "," +
                                  AtempTipoM + "," + CircuitoM1 + "," + CircuitoM2 + "," + CircuitoM3 + "," + CircuitoM4 + "," + CircuitoM5 + "," + CircuitoM6 + ",'" +
                                  CaudalM1 + "','" + CaudalM2 + "','" + CaudalM3 + "','" + CaudalM4 + "','" + CaudalM5 + "','" + CaudalM6 + "','" +
                                  TemperaturaM1 + "','" + TemperaturaM2 + "','" + TemperaturaM3 + "','" + TemperaturaM4 + "','" + TemperaturaM5 + "','" + TemperaturaM6 + "'," +
                                  EntradaM1 + "," + EntradaM2 + "," + EntradaM3 + "," + EntradaM4 + "," + EntradaM5 + "," + EntradaM6 + ")";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
            }
        }

        public void insertarImagenes(int referencia, int maquina, int version, string img1, string hyperlink2, string hyperlink3, string hyperlink4, string img5, string hyperlink6, string hyperlink7, string hyperlink8)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "INSERT INTO [SMARTH_DB].[dbo].[PARAMETROS_ImagenesParam] (Referencia, Maquina, Version," +
                                  "AtempEsquemaF, AtempGeneralF, AtempOpeF, AtempOpuF, AtempEsquemaM, AtempGeneralM, AtempOpeM, AtempOpuM)" +
                                  " VALUES (" + referencia + "," + maquina + "," + version + ",'" +
                                  img1 + "','" + hyperlink2 + "','" + hyperlink3 + "','" + hyperlink4 + "','" + img5 + "','" + hyperlink6 + "','" + hyperlink7 + "','" + hyperlink8 + "')";          
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
            }
        }


        public bool existeFicha(int referencia, int maquina, int version)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT Count(*) as count FROM [SMARTH_DB].[dbo].[PARAMETROS_Ficha] where Referencia = " + referencia + " AND Maquina = " + maquina + " AND Version = " + version;
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                if (Convert.ToInt32(ds.Tables[0].Rows[0]["count"].ToString()) == 0)
                {
                    return false;
                }
                else
                    return true;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }

     // LECTURA DE PARÁMETROS
        public DataSet leerFicha(int referencia, int maquina, int version)
        {
            try
            {                
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT * FROM [SMARTH_DB].[dbo].[PARAMETROS_Ficha] WHERE Referencia = " + referencia + " AND Maquina = " + maquina + " AND Version = " + version;
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

        public int leer_maxima_version(int referencia, int maquina)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT max(version) as version FROM [SMARTH_DB].[dbo].[PARAMETROS_Ficha] WHERE Referencia = " + referencia + " AND Maquina = " + maquina;
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return 0;
            }
        }

        public DataSet leerMaterial(int referencia, int maquina, int version)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT * FROM Material WHERE Referencia = " + referencia + " AND Maquina = " + maquina + " AND Version = " + version;
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

        public DataSet leerTempCilindro(int referencia, int maquina, int version)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT * FROM [SMARTH_DB].[dbo].[PARAMETROS_TempCilindro] WHERE Referencia = " + referencia + " AND Maquina = " + maquina + "AND Version = " + version;
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

        public DataSet leerTempCamCaliente(int referencia, int maquina, int version)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT * FROM [SMARTH_DB].[dbo].[PARAMETROS_TempCamCaliente] WHERE Referencia = " + referencia + " AND Maquina = " + maquina + " AND Version = " + version;
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

        public DataSet leerInyeccion(int referencia, int maquina, int version)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT * FROM [SMARTH_DB].[dbo].[PARAMETROS_Inyeccion] WHERE Referencia = " + referencia + " AND Maquina = " + maquina + " AND Version = " + version;
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

        public DataSet leerPostpresion(int referencia, int maquina, int version)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT * FROM [SMARTH_DB].[dbo].[PARAMETROS_Postpresion] WHERE Referencia = " + referencia + " AND Maquina = " + maquina + " AND Version = " + version;
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

        public DataSet leerSecuencial(int referencia, int maquina, int version)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT * FROM [SMARTH_DB].[dbo].[PARAMETROS_InyeccionSecuencial] WHERE Referencia = " + referencia + " AND Maquina = " + maquina + " AND Version = " + version;
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

        public DataSet leerTolerancias(int referencia, int maquina, int version)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT * FROM [SMARTH_DB].[dbo].[PARAMETROS_Tolerancias] WHERE Referencia = " + referencia + " AND Maquina = " + maquina + " AND Version = " + version;
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

        public DataSet leerMarcaCruz(int referencia, int maquina, int version)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT * FROM [SMARTH_DB].[dbo].[PARAMETROS_MarcaCruz] WHERE Referencia = " + referencia + " AND Maquina = " + maquina + " AND Version = " + version;
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

        public DataSet leer_maquinas_byRef(int referencia)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT distinct Maquina FROM [SMARTH_DB].[dbo].[PARAMETROS_Ficha] WHERE Referencia = " + referencia;
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

        public DataSet leer_versiones_byRefXMaq(int referencia, string maquina)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT distinct Version FROM [SMARTH_DB].[dbo].[PARAMETROS_Ficha] WHERE Referencia = " + referencia+ " and Maquina = '" + maquina + "'";
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


        public DataSet leerAtemperado(int referencia, int maquina, int version)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT * FROM [SMARTH_DB].[dbo].[PARAMETROS_AtemperadoV1] WHERE Referencia = " + referencia + " AND Maquina = " + maquina + " AND Version = " + version;
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

        public DataSet leer_versiones(int referencia, int maquina)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT distinct Version FROM [SMARTH_DB].[dbo].[PARAMETROS_Ficha] WHERE Referencia = " + referencia + " AND Maquina = " + maquina;
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

        public DataSet leerHistorico(int referencia, int maquina)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT * FROM [SMARTH_DB].[dbo].[PARAMETROS_Ficha] WHERE Referencia = " + referencia + " AND Maquina = " + maquina;
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

        public DataSet LeerHistoricoFichasFabricacion(int referencia, int maquina)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT *  FROM [SMARTH_DB].[dbo].[PARAMETROS_Ficha] F" +
                            " LEFT JOIN [SMARTH_DB].[dbo].[AUX_Personal_Mandos] P ON F.INTElaborado = P.Id" +
                            " WHERE Referencia = " + referencia + " AND Maquina = " + maquina;
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

        public DataSet LeerHistoricoFichasFabricacionV2(int referencia)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT *  FROM [SMARTH_DB].[dbo].[PARAMETROS_Ficha] F" +
                            " LEFT JOIN [SMARTH_DB].[dbo].[AUX_Personal_Mandos] P ON F.INTElaborado = P.Id" +
                            " WHERE Referencia = " + referencia + "" +
                            " ORDER BY Referencia, Maquina, Version";
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


        public string leerImagenes(int referencia, int maquina, int version, int num_img)
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
                        sql = "SELECT AtempEsquemaF FROM [SMARTH_DB].[dbo].[PARAMETROS_ImagenesParam] WHERE Referencia = " + referencia + " AND Maquina = " + maquina + " AND Version = " + version;
                        break;
                    case 2:
                        sql = "SELECT AtempGeneralF FROM [SMARTH_DB].[dbo].[PARAMETROS_ImagenesParam] WHERE Referencia = " + referencia + " AND Maquina = " + maquina + " AND Version = " + version;
                        break;
                    case 3:
                        sql = "SELECT AtempOpeF FROM [SMARTH_DB].[dbo].[PARAMETROS_ImagenesParam] WHERE Referencia = " + referencia + " AND Maquina = " + maquina + " AND Version = " + version;
                        break;
                    case 4:
                        sql = "SELECT AtempOpuF FROM [SMARTH_DB].[dbo].[PARAMETROS_ImagenesParam] WHERE Referencia = " + referencia + " AND Maquina = " + maquina + " AND Version = " + version;
                        break;
                    case 5:
                        sql = "SELECT AtempEsquemaM FROM [SMARTH_DB].[dbo].[PARAMETROS_ImagenesParam] WHERE Referencia = " + referencia + " AND Maquina = " + maquina + " AND Version = " + version;
                        break;
                    case 6:
                        sql = "SELECT AtempGeneralM FROM [SMARTH_DB].[dbo].[PARAMETROS_ImagenesParam] WHERE Referencia = " + referencia + " AND Maquina = " + maquina + " AND Version = " + version;
                        break;
                    case 7:
                        sql = "SELECT AtempOpeM FROM [SMARTH_DB].[dbo].[PARAMETROS_ImagenesParam] WHERE Referencia = " + referencia + " AND Maquina = " + maquina + " AND Version = " + version;
                        break;
                    case 8:
                        sql = "SELECT AtempOpuM FROM [SMARTH_DB].[dbo].[PARAMETROS_ImagenesParam] WHERE Referencia = " + referencia + " AND Maquina = " + maquina + " AND Version = " + version;
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
                        return ds.Tables[0].Rows[0]["AtempEsquemaF"].ToString();
                       // break;
                    case 2:
                        return ds.Tables[0].Rows[0]["AtempGeneralF"].ToString();
                        //break;
                    case 3:
                        return ds.Tables[0].Rows[0]["AtempOpeF"].ToString();
                        //break;
                    case 4:
                        return ds.Tables[0].Rows[0]["AtempOpuF"].ToString();
                        //break;
                    case 5:
                        return ds.Tables[0].Rows[0]["AtempEsquemaM"].ToString();
                        //break;
                    case 6:
                        return ds.Tables[0].Rows[0]["AtempGeneralM"].ToString();
                        //break;
                    case 7:
                        return ds.Tables[0].Rows[0]["AtempOpeM"].ToString();
                        //break;
                    case 8:
                        return ds.Tables[0].Rows[0]["AtempOpuM"].ToString();
                        //break;
                    default:
                        return null;
                        //break;
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

   // CONEXION CON BMS
        public bool comprobarMaquina(int referencia)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT O.c_machineid as m FROM SPC.T_INSPECTIONORDERS O INNER JOIN SPC.T_PRODUCTS P ON (O.C_PRODUCTID=P.C_ID) WHERE P.C_NAME = '" + referencia + "'";
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataSet ds = new DataSet();
                query.Fill(ds);
                cnn_bms.Close();
                if (ds.Tables[0].Rows[0]["m"].ToString() == "29" || ds.Tables[0].Rows[0]["m"].ToString() == "31" || ds.Tables[0].Rows[0]["m"].ToString() == "44")
                {
                    return true;
                }
                else 
                    return false;            
            }
            catch (Exception)
            {
                return false;
            }
        }

        public DataSet cargar_datos_bms(int referencia)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT P.C_ID, RTRIM (P.C_CUSTOMER) as Cliente, P.C_FACTOR_MUL1 as Cavidades, P.C_FACTOR_DIV2 as Operarios, UPPER(P.C_LONG_DESCR) as Descripcion, RTRIM (M.C_TOOL_ID) as Molde FROM PCMS.T_PRODUCTS P LEFT JOIN T_TOOL_X_PRODUCT M ON P.C_ID = M.C_PRODUCT_ID WHERE P.C_ID = '" + referencia + "' ORDER BY P.C_ID";    
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

        public DataSet cargar_datos_estructura(int referencia)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT M.C_ID AS MATERIAL, M.C_LONG_DESCR AS DESCRIPCION, M.C_SHORT_DESCR AS UBICACION, M.C_REMARKS AS SECADO, Y.C_REMARKS AS NOTAS, Y.C_ID AS G," +
                    " CASE WHEN M.C_MATTYPE_ID = '221' THEN M.C_REMARKS WHEN M.C_MATTYPE_ID = '15' THEN M.C_REMARKS WHEN M.C_MATTYPE_ID = '18' THEN M.C_REMARKS END AS SECADOV2,"+
                    " CASE WHEN M.C_MATTYPE_ID = '221' THEN Y.C_REMARKS WHEN M.C_MATTYPE_ID = '15' THEN Y.C_REMARKS WHEN M.C_MATTYPE_ID = '18' THEN Y.C_REMARKS END AS NOTASV2"+
                    " FROM PCMS.T_MATERIALS M, PCMS.T_RECIPES Y, PCMS.T_RECIPE_X_MATERIAL R WHERE R.C_RECIPE_ID = Y.C_ID AND M.C_ID = R.C_MATERIAL_ID AND Y.C_ID = '" + referencia + "'";
                //Y.C_ID AS Receta, M.C_MATTYPE_ID as Tipomat, R.C_UNITS AS Consumo,R.C_REMARKS AS E,
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

        public DataSet cargar_datos_materialinyeccion(int referencia)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT M.C_ID AS MATERIAL, M.C_LONG_DESCR AS DESCRIPCION, M.C_SHORT_DESCR AS UBICACION, M.C_REMARKS AS SECADO, Y.C_REMARKS AS NOTAS, Y.C_ID AS G FROM PCMS.T_MATERIALS M, PCMS.T_RECIPES Y, PCMS.T_RECIPE_X_MATERIAL R WHERE R.C_RECIPE_ID = Y.C_ID AND M.C_ID = R.C_MATERIAL_ID AND (M.C_MATTYPE_ID = '15' or M.C_MATTYPE_ID = '155' or M.C_MATTYPE_ID = '215') AND Y.C_ID = '" + referencia + "'";
                //Y.C_ID AS Receta, M.C_MATTYPE_ID as Tipomat, R.C_UNITS AS Consumo,R.C_REMARKS AS E,
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

        public DataSet cargar_datos_materialconcentrado(int referencia)
        {
            try
            {
                cnn_bms.Open();
                string sql = "SELECT M.C_ID AS MATERIAL, M.C_LONG_DESCR AS DESCRIPCION, M.C_SHORT_DESCR AS UBICACION, M.C_REMARKS AS SECADO, Y.C_REMARKS AS NOTAS, Y.C_ID AS G FROM PCMS.T_MATERIALS M, PCMS.T_RECIPES Y, PCMS.T_RECIPE_X_MATERIAL R WHERE R.C_RECIPE_ID = Y.C_ID AND M.C_ID = R.C_MATERIAL_ID AND M.C_MATTYPE_ID = '150' AND Y.C_ID = '" + referencia + "'";
                //Y.C_ID AS Receta, M.C_MATTYPE_ID as Tipomat, R.C_UNITS AS Consumo,R.C_REMARKS AS E,
                OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
                DataSet dsc = new DataSet();
                query.Fill(dsc);
                cnn_bms.Close();
                return dsc;
            }
            catch (Exception)
            {
                cnn_bms.Close();
                return null;
            }
        }

   //DATASETS Y CONECTORES DE ATEMPERADO
        public DataSet devuelve_lista_tipo_atemperado()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT IdAtemp, TipoAgua FROM [SMARTH_DB].[dbo].[PARAMETROS_AuxAtemperado] where TipoAgua <> '' order by IdAtemp asc";
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
                string sql = "SELECT TipoAgua FROM [SMARTH_DB].[dbo].[PARAMETROS_AuxAtemperado] where IdAtemp = " + id;
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
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
                cnn_SMARTH.Close();
                return "";
            }
        }

        public int devuelve_IDtipo_atemperado(string nombre)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT IdAtemp FROM [SMARTH_DB].[dbo].[PARAMETROS_AuxAtemperado] where TipoAgua = '" + nombre + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return Convert.ToInt16(ds.Tables[0].Rows[0]["IdAtemp"].ToString());
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return 1;
            }
        }

        public DataSet devuelve_lista_circuitos()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT IdAtemp, Circuitos FROM [SMARTH_DB].[dbo].[PARAMETROS_AuxAtemperado] where Circuitos <> '' order by IdAtemp asc";
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

        public string devuelve_tipo_circuito(int id)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT Circuitos FROM [SMARTH_DB].[dbo].[PARAMETROS_AuxAtemperado] where IdAtemp = " + id;
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                if (ds.Tables[0].Rows[0]["Circuitos"].ToString() != "")
                {
                    return ds.Tables[0].Rows[0]["Circuitos"].ToString();
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

        public string Devuelve_ManoVinculada(string molde)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT Mano FROM[SMARTH_DB].[dbo].[AUX_Lista_Moldes] WHERE ReferenciaMolde = "+molde+"";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                if (ds.Tables[0].Rows[0]["Mano"].ToString() != "")
                {
                    return ds.Tables[0].Rows[0]["Mano"].ToString();
                }
                else
                {
                    return "Pdte. asignar en app";
                }

            }
            catch (Exception ex)
            {
                cnn_SMARTH.Close();
                return "Pdte. asignar en app";
            }
        }

        public int devuelve_IDtipo_circuito(string nombre)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT IdAtemp FROM [SMARTH_DB].[dbo].[PARAMETROS_AuxAtemperado] where Circuitos = '" + nombre + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return Convert.ToInt16(ds.Tables[0].Rows[0]["IdAtemp"].ToString());
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return 1;
            }
        }

        public DataSet devuelve_lista_entradasAtemp()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT IdAtemp, AtempIN FROM [SMARTH_DB].[dbo].[PARAMETROS_AuxAtemperado] where AtempIN <> '' order by IdAtemp asc";
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

        public string devuelve_tipo_entradasAtemp(int id)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT AtempIN FROM [SMARTH_DB].[dbo].[PARAMETROS_AuxAtemperado] where IdAtemp = " + id;
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                if (ds.Tables[0].Rows[0]["AtempIN"].ToString() != "")
                {
                    return ds.Tables[0].Rows[0]["AtempIN"].ToString();
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

        public int devuelve_IDtipo_entradasAtemp(string nombre)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "SELECT IdAtemp FROM [SMARTH_DB].[dbo].[PARAMETROS_AuxAtemperado] where AtempIN = '" + nombre + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, cnn_SMARTH);
                DataSet ds = new DataSet();
                adapter.Fill(ds, sql);
                cnn_SMARTH.Close();
                return Convert.ToInt16(ds.Tables[0].Rows[0]["IdAtemp"].ToString());
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return 1;
            }
        }


   //CONECTORES DE LISTA FICHA PARAMETROS
        public DataSet lista_fichas_parametros()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT F.Referencia, F.CodMolde, F.Descripcion, F.Maquina, MAX (F.Version) as Version, MAX (F.Cliente) as Cliente, MAX (F.Fecha) as Fecha, MAX (CONVERT(datetime2, F.Fecha, 126))  as Fechaord, P.Nombre as Autor " +
                                                                 " FROM [SMARTH_DB].[dbo].[PARAMETROS_Ficha] F left join [SMARTH_DB].[dbo].[AUX_Personal_Mandos] P on (F.INTElaborado = P.Id)" +
                                                                 " GROUP BY Referencia, CodMolde, Descripcion, Maquina, Cliente, Nombre order by Referencia", cnn_SMARTH);
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

        public DataSet Lista_fichas_parametros_V2(string referencia, string molde, string orderby)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT FM.Referencia, FM.Maquina, FM.Version, FIC.CodMolde, FIC.Descripcion, FIC.Cliente, FIC.Fecha, PER.Nombre AS Autor"+
                                                                  " FROM(SELECT[Referencia],[Maquina], MAX([Version]) AS Version FROM[SMARTH_DB].[dbo].[PARAMETROS_Ficha] GROUP BY Referencia, Maquina) FM"+
                                                                  " LEFT JOIN[SMARTH_DB].[dbo].[PARAMETROS_Ficha] FIC ON FM.Referencia = FIC.Referencia AND FM.Maquina = FIC.Maquina AND FM.Version = FIC.Version" +
                                                                  " LEFT JOIN[SMARTH_DB].[dbo].[AUX_Personal_Mandos] PER ON FIC.INTElaborado = PER.Id" +
                                                                  " WHERE FM.Version > 0"+referencia+" "+molde+""+orderby+"", cnn_SMARTH);
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

        public DataSet lista_fichas_parametros_fecha()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT F.Referencia, F.CodMolde, F.Descripcion, F.Maquina, MAX (F.Version) as Version, MAX (F.Fecha) as Fecha,MAX (F.Cliente) as Cliente, MAX (CONVERT(datetime2, F.Fecha, 126))  as Fechaord, P.Nombre as Autor " +
                    " FROM [SMARTH_DB].[dbo].[PARAMETROS_Ficha] F left join [SMARTH_DB].[dbo].[AUX_Personal_Mandos] P on (F.INTElaborado = P.Id)" +
                    " GROUP BY Referencia, CodMolde, Descripcion, Maquina, Cliente, Nombre " +
                    " ORDER BY Fechaord DESC ", cnn_SMARTH);
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

        public DataSet busca_fichas_parametros(int numficha)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT F.Referencia, F.CodMolde, F.Descripcion, F.Maquina, MAX (F.Version) as Version, MAX (F.Cliente) as Cliente, MAX (F.Fecha) as Fecha, MAX (CONVERT(datetime2, F.Fecha, 126))  as Fechaord, P.Nombre as Autor" +
                    " FROM [SMARTH_DB].[dbo].[PARAMETROS_Ficha] F left join [SMARTH_DB].[dbo].[AUX_Personal_Mandos] P on (F.INTElaborado = P.Id) WHERE Referencia LIKE '" + numficha + "%'" +
                    " GROUP BY Referencia, CodMolde, Descripcion, Maquina, Cliente, Nombre order by Referencia", cnn_SMARTH);
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

        public DataSet busca_fichas_parametrosMolde(int nummolde)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT F.Referencia, F.CodMolde, F.Descripcion, F.Maquina, MAX (F.Version) as Version, MAX (F.Cliente) as Cliente, MAX (F.Fecha) as Fecha, MAX (CONVERT(datetime2, F.Fecha, 126))  as Fechaord, P.Nombre as Autor" +
                    " FROM [SMARTH_DB].[dbo].[PARAMETROS_Ficha] F left join [SMARTH_DB].[dbo].[AUX_Personal_Mandos] P on (F.INTElaborado = P.Id)" +
                    " WHERE CodMolde LIKE '" + nummolde + "%' GROUP BY Referencia, CodMolde, Descripcion, Maquina, Cliente, Nombre" +
                    " order by Referencia", cnn_SMARTH);
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
        //ELIMINAR V2
        public void eliminar_ficha_lista_V2(int referencia, int maquina, int version)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "DELETE FROM [SMARTH_DB].[dbo].[PARAMETROS_Ficha] WHERE Referencia = " + referencia + " AND Maquina = " + maquina + " AND Version =" +version;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                eliminar_inyeccion_lista_V2(referencia, maquina,version);
                eliminar_inyeccionsecuencial_lista_V2(referencia, maquina, version);
                eliminar_MarcaCruz_lista_V2(referencia, maquina, version);
                eliminar_Material_lista_V2(referencia, maquina, version);
                eliminar_Postpresion_lista_V2(referencia, maquina, version);
                eliminar_TempCamCaliente_lista_V2(referencia, maquina, version);
                eliminar_TempCilindro_lista_V2(referencia, maquina, version);
                eliminar_Atemperado_lista_V2(referencia, maquina, version);
                eliminar_tolerancias_lista_V2(referencia, maquina, version);
                eliminar_imagenes_lista_V2(referencia, maquina, version);
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
            }
        }

        public void eliminar_inyeccion_lista_V2(int referencia, int maquina, int version)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "DELETE FROM [SMARTH_DB].[dbo].[PARAMETROS_Inyeccion] WHERE Referencia = " + referencia + " AND Maquina = " + maquina + " AND Version =" + version;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
            }
        }

        public void eliminar_inyeccionsecuencial_lista_V2(int referencia, int maquina, int version)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "DELETE FROM [SMARTH_DB].[dbo].[PARAMETROS_InyeccionSecuencial] WHERE Referencia = " + referencia + " AND Maquina = " + maquina + " AND Version =" + version;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
            }
        }

        public void eliminar_MarcaCruz_lista_V2(int referencia, int maquina, int version)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "DELETE FROM [SMARTH_DB].[dbo].[PARAMETROS_MarcaCruz] WHERE Referencia = " + referencia + " AND Maquina = " + maquina + " AND Version =" + version;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
            }
        }

        public void eliminar_Material_lista_V2(int referencia, int maquina, int version)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "DELETE FROM Material WHERE Referencia = " + referencia + " AND Maquina = " + maquina + " AND Version =" + version;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
            }
        }

        public void eliminar_TempCamCaliente_lista_V2(int referencia, int maquina, int version)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "DELETE FROM [SMARTH_DB].[dbo].[PARAMETROS_TempCamCaliente] WHERE Referencia = " + referencia + " AND Maquina = " + maquina + " AND Version =" + version;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
            }
        }

        public void eliminar_Postpresion_lista_V2(int referencia, int maquina, int version)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "DELETE FROM [SMARTH_DB].[dbo].[PARAMETROS_Postpresion] WHERE Referencia = " + referencia + " AND Maquina = " + maquina + " AND Version =" + version;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
            }
        }

        public void eliminar_TempCilindro_lista_V2(int referencia, int maquina, int version)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "DELETE FROM [SMARTH_DB].[dbo].[PARAMETROS_TempCilindro] WHERE Referencia = " + referencia + " AND Maquina = " + maquina + " AND Version =" + version;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
            }
        }

        public void eliminar_Atemperado_lista_V2(int referencia, int maquina, int version)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "DELETE FROM [SMARTH_DB].[dbo].[PARAMETROS_AtemperadoV1] WHERE Referencia = " + referencia + " AND Maquina = " + maquina + " AND Version =" + version;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
            }
        }

        public void eliminar_tolerancias_lista_V2(int referencia, int maquina, int version)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "DELETE FROM [SMARTH_DB].[dbo].[PARAMETROS_Tolerancias] WHERE Referencia = " + referencia + " AND Maquina = " + maquina + " AND Version =" + version;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
            }
        }

        public void eliminar_imagenes_lista_V2(int referencia, int maquina, int version)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "DELETE FROM [SMARTH_DB].[dbo].[PARAMETROS_ImagenesParam] WHERE Referencia = " + referencia + " AND Maquina = " + maquina + " AND Version =" + version;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
            }
        }

        //ELIMINAR ANTIGUO

        public void eliminar_ficha_lista(int referencia, int maquina)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "DELETE FROM [SMARTH_DB].[dbo].[PARAMETROS_Ficha] WHERE Referencia = " + referencia +" AND Maquina = " + maquina;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                eliminar_inyeccion_lista(referencia, maquina);
                eliminar_inyeccionsecuencial_lista(referencia, maquina);
                eliminar_MarcaCruz_lista(referencia, maquina);
                eliminar_Material_lista(referencia, maquina);
                eliminar_Postpresion_lista(referencia, maquina);
                eliminar_TempCamCaliente_lista(referencia, maquina);
                eliminar_TempCilindro_lista(referencia, maquina);
                eliminar_Atemperado_lista(referencia, maquina);
                eliminar_tolerancias_lista(referencia, maquina);
                eliminar_imagenes_lista(referencia, maquina);
            }
            catch (Exception)
            {

            }
        }

        public void eliminar_inyeccion_lista(int referencia, int maquina)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "DELETE FROM [SMARTH_DB].[dbo].[PARAMETROS_Inyeccion] WHERE Referencia = " + referencia + " AND Maquina = " + maquina;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
            }
        }

        public void eliminar_inyeccionsecuencial_lista(int referencia, int maquina)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "DELETE FROM [SMARTH_DB].[dbo].[PARAMETROS_InyeccionSecuencial] WHERE Referencia = " + referencia + " AND Maquina = " + maquina;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
            }
        }

        public void eliminar_MarcaCruz_lista(int referencia, int maquina)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "DELETE FROM [SMARTH_DB].[dbo].[PARAMETROS_MarcaCruz] WHERE Referencia = " + referencia + " AND Maquina = " + maquina;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
            }
        }

        public void eliminar_Material_lista(int referencia, int maquina)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "DELETE FROM Material WHERE Referencia = " + referencia + " AND Maquina = " + maquina;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception)
            {

            }
        }

        public void eliminar_TempCamCaliente_lista(int referencia, int maquina)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "DELETE FROM [SMARTH_DB].[dbo].[PARAMETROS_TempCamCaliente] WHERE Referencia = " + referencia + " AND Maquina = " + maquina;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
            }
        }

        public void eliminar_Postpresion_lista(int referencia, int maquina)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "DELETE FROM [SMARTH_DB].[dbo].[PARAMETROS_Postpresion] WHERE Referencia = " + referencia + " AND Maquina = " + maquina;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
            }
        }

        public void eliminar_TempCilindro_lista(int referencia, int maquina)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "DELETE FROM [SMARTH_DB].[dbo].[PARAMETROS_TempCilindro] WHERE Referencia = " + referencia + " AND Maquina = " + maquina;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception)
            {

            }
        }

        public void eliminar_Atemperado_lista(int referencia, int maquina)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "DELETE FROM [SMARTH_DB].[dbo].[PARAMETROS_AtemperadoV1] WHERE Referencia = " + referencia + " AND Maquina = " + maquina;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
            }
        }

        public void eliminar_tolerancias_lista(int referencia, int maquina)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "DELETE FROM [SMARTH_DB].[dbo].[PARAMETROS_Tolerancias] WHERE Referencia = " + referencia + " AND Maquina = " + maquina;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
            }
        }

        public void eliminar_imagenes_lista(int referencia, int maquina)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "DELETE FROM [SMARTH_DB].[dbo].[PARAMETROS_ImagenesParam] WHERE Referencia = " + referencia + " AND Maquina = " + maquina;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
            }
        }

        //KPISFichasParametros
        public DataSet devuelveKPIEncargadoXFicha()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT P.Nombre as PElaborado, COUNT(S.INTElaborado) AS FICHAS" +
                                                                    " FROM (SELECT [INTElaborado],Referencia,MAX([Version]) AS VERSION,MAX ([Fecha]) as FECHA" +
                                                                    " FROM [SMARTH_DB].[dbo].[PARAMETROS_Ficha] " +
                                                                    " GROUP BY [INTElaborado], Referencia) S " +
                                                                    " LEFT JOIN [SMARTH_DB].[dbo].[AUX_Personal_Mandos] P ON S.INTElaborado=P.Id" +
                                                                    " GROUP BY Nombre ORDER BY FICHAS DESC", cnn_SMARTH);      
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

        public DataSet devuelveKPIFichasFaltantes()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT B.[Maquina],B.[Orden],B.[OrdenMaestra],B.[Linea], B.[Molde],B.[Referencia],P.Descripcion, B.[HoraCarga],MAX(F.Version) as VERSION"+
                                     " FROM[SMARTH_DB].[dbo].[AUX_Planificacion] B"+
                                    " LEFT JOIN[SMARTH_DB].[dbo].[PARAMETROS_Ficha] F ON B.Molde = f.CodMolde AND B.Maquina = CAST(F.Maquina AS nvarchar)" +
                                    " LEFT JOIN[SMARTH_DB].[dbo].[AUX_TablaProductos] P ON B.Referencia = P.Referencia WHERE VERSION IS NULL AND B.Maquina <> 'BSH ' AND B.Maquina <> 'FOAM' AND B.Maquina <> 'HOUS' AND SEQNR = 0 AND Linea = 1 AND B.Maquina <> 'WTOP'"+
                                    " GROUP BY B.Maquina, b.Orden, b.OrdenMaestra, b.Linea, b.Molde, b.Referencia, b.HoraCarga, P.Descripcion", cnn_SMARTH);
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


        public bool LimpiarOrdenesProduciendoBMS()
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "DELETE FROM [CALIDAD].[dbo].[BMSEnProduccion]";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }

        public bool leer_OrdenesProduciendoBMS()
        {

            try
            {
                string sql = "SELECT C_MACHINE_ID, C_ID, C_PARENT_ID, C_PARINDEX, C_TOOL_ID, C_PRODUCT_ID, C_ACTSTARTDATE FROM PCMS.T_JOBS WHERE C_SEQNR = 0 ORDER BY C_MACHINE_ID, C_PARINDEX";
                OracleCommand cmd = new OracleCommand(sql, cnn_bms);
                cnn_bms.Open();
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        insertar_orden_produciendo(reader["C_MACHINE_ID"].ToString(), reader["C_ID"].ToString(), reader["C_PARENT_ID"].ToString(), Convert.ToInt32(reader["C_PARINDEX"]), reader["C_TOOL_ID"].ToString(), reader["C_PRODUCT_ID"].ToString(), reader["C_ACTSTARTDATE"].ToString());

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

        public bool insertar_orden_produciendo(string maquina, string orden, string ordenmaestra, int indice, string molde, string producto, string HoraCarga)
        {
            try
            {
                cnn_SMARTH.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn_SMARTH;
                string sql = "INSERT INTO [CALIDAD].[dbo].[BMSEnProduccion] (Maquina,Orden,OrdenMaestra,Linea,Molde,Referencia,HoraCarga) VALUES " +
                                 "('" + maquina + "','" + orden + "','" + ordenmaestra + "'," + indice + ",'" + molde + "','" + producto + "', '" + HoraCarga + "')";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cnn_SMARTH.Close();
                return true;
            }
            catch (Exception)
            {
                cnn_SMARTH.Close();
                return false;
            }
        }
        // CONSUlTA BUENA
        //select S.C_INSPECTIONID,O.C_NAME, CH.C_NAME, S.C_XBAR FROM SPC.T_INSPECTIONSAMPLES S INNER JOIN
        //             SPC.T_INSPECTIONS I ON(S.C_INSPECTIONID=I.C_ID) INNER JOIN
        //             SPC.T_INSPECTIONORDERS O ON(I.C_INSPECTIONORDERID=O.C_ID) INNER JOIN
        //             SPC.T_INSPECTIONCHARPARAMS C ON (S.C_INSPECTIONCHARPARAMID=C.C_ID) INNER JOIN
        //             SPC.T_CHARACTERISTICS CH ON(C_CHARACTERISTICID=CH.C_ID)
        //             WHERE S.C_INSPECTOR LIKE '%Euromap%' AND O.C_NAME LIKE '%62095001%'
        //             ORDER BY S.C_INSPECTIONID DESC, CH.C_ID

        //public bool leerDatos(int referencia)
        //{
        //    try
        //    {
        //        cnn_bms.Open();
        //        string sql = "select S.C_XBAR FROM SPC.T_INSPECTIONSAMPLES as S INNER JOIN SPC.T_INSPECTIONS as I INNE JOIN SPC.T_INSPECTIONCREATIONRULES as R INNER JOIN SPC.T_PRODUCTS as P WHERE S.C_INSPECTOR LIKE '%Euromap%' AND P.C_NAME = '" + referencia + "'";
        //        OracleDataAdapter query = new OracleDataAdapter(sql, cnn_bms);
        //        DataSet ds = new DataSet();
        //        query.Fill(ds);
        //        cnn_bms.Close();
               
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

    }
}