using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Web.Script.Services;
using Excel = Microsoft.Office.Interop.Excel;

using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using System.Net.Mail;
using System.Net;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Extensions;
using OfficeOpenXml;



namespace ThermoWeb.LIBERACIONES
{
    public partial class LiberacionSerieNew : System.Web.UI.Page
    {
        //private string selectedValueMaquina = "";
        //private int maquina = 0;
        //private string orden = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack || Request.QueryString["ORDEN"] == null)
            {
                return;
            }

            CargarDrops();
            CargarCabecera();

            Conexion_LIBERACIONES conexion = new Conexion_LIBERACIONES();
            DataSet existeorden = conexion.Consulta_existencia_liberacion(tbOrden.Text);
            if (existeorden.Tables[0].Rows.Count > 0)
            {
                CargarFichaLiberacion();
                CargarParametrosLiberacion();
                CargarCuestionariosLiberacion();
                CargarMateriasPrimasLiberacion();
                PintarCeldasDevacion();
                CargarResultados();
                CargarMantenimiento();
            }
            else
            {
                CargarParametros();
                CargarMateriasPrimas();
                //CargarTrabajadores(); prueba
                CargarMantenimiento();
                CrearLiberacion();
            }
        }

        private static bool HasRows(DataSet dataSet)
        {
            return dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0;
        }

        private void AsignarCabecera(DataRow row)
        {
            tbMaquina.Text = row["C_MACHINE_ID"].ToString();
            tbReferencia.Text = row["C_PRODUCT_ID"].ToString();
            tbNombre.Text = row["C_PRODLONGDESCR"].ToString();
            tbOrden.Text = row["C_ID"].ToString();
            tbMolde.Text = row["C_TOOL_ID"].ToString();
            tbFechaCambio.Text = row["C_ACTSTARTDATE"].ToString();
        }

        private void AsignarCabeceraHistorico(DataRow row)
        {
            tbMaquina.Text = row["Maquina"].ToString();
            tbReferencia.Text = row["Referencia"].ToString();
            tbNombre.Text = row["Descripcion"].ToString();
            tbOrden.Text = row["Orden"].ToString();
            tbMolde.Text = row["CodMolde"].ToString();
            tbFechaCambio.Text = row["FechaApertura"].ToString();
        }

        private void MostrarHijos(DataSet hijos)
        {
            MostrarHijo(hijos, 0, tbOrden2, tbReferencia2, tbNombre2, tbOrdenTitulo2, tbReferenciaTitulo2);
            MostrarHijo(hijos, 1, tbOrden3, tbReferencia3, tbNombre3, tbOrdenTitulo3, tbReferenciaTitulo3);
            MostrarHijo(hijos, 2, tbOrden4, tbReferencia4, tbNombre4, tbOrdenTitulo4, tbReferenciaTitulo4);
        }

        private static void MostrarHijo(DataSet hijos, int index, TextBox orden, TextBox referencia, TextBox nombre, TextBox tituloOrden, TextBox tituloReferencia)
        {
            if (!HasRows(hijos) || hijos.Tables[0].Rows.Count <= index)
            {
                return;
            }

            DataRow row = hijos.Tables[0].Rows[index];
            orden.Text = row["C_ID"].ToString();
            referencia.Text = row["C_PRODUCT_ID"].ToString();
            nombre.Text = row["C_LONG_DESCR"].ToString();
            orden.Visible = true;
            referencia.Visible = true;
            nombre.Visible = true;
            tituloOrden.Visible = true;
            tituloReferencia.Visible = true;
        }
        public void CargarDrops()
        {
            try
            {
                var conexion = new Conexion_LIBERACIONES();
                var ds = conexion.Devuelve_setlista_responsablesSMARTH();
                DropValidadJefeProyecto.Items.Clear();

                foreach (DataRow row in ds.Tables[0].Rows)
                    DropValidadJefeProyecto.Items.Add(row["PAprobado"].ToString());

                DropValidadJefeProyecto.ClearSelection();
                DropValidadJefeProyecto.SelectedValue = "";
            }
            catch { }
        }

        // --- CABECERA Y SUBÓRDENES ---
        public void CargarCabecera()
        {
            Conexion_LIBERACIONES conexion = new Conexion_LIBERACIONES();

            try
            {
                DataSet ds = conexion.devuelve_detalles_orden(Request.QueryString["ORDEN"]);
                if (HasRows(ds))
                {
                    AsignarCabecera(ds.Tables[0].Rows[0]);
                    MostrarHijos(conexion.devuelve_hijos_orden(Request.QueryString["ORDEN"]));
                }
                else
                {
                    ds = conexion.devuelve_detalles_orden_HIST(Request.QueryString["ORDEN"]);
                    if (HasRows(ds))
                    {
                        AsignarCabeceraHistorico(ds.Tables[0].Rows[0]);
                    }

                    MostrarHijos(conexion.devuelve_hijos_orden(Request.QueryString["ORDEN"]));
                }

                //CambiadorHoras.Text = cambiador.Tables[0].Rows[0]["C_MACHINE_ID"].ToString();

            }
            catch (Exception)
            {
            }
        }

        public void CargarTrabajadores()
        {
            try
            {
                Conexion_LIBERACIONES conexion = new Conexion_LIBERACIONES();
                DataSet calidadplanta = conexion.devuelve_calidadplanta_logueadoXMaquina(tbMaquina.Text);
                if (HasRows(calidadplanta))
                {
                    CalidadNumero.Text = calidadplanta.Tables[0].Rows[0]["C_CLOCKNO"].ToString();
                    CalidadNombre.Text = calidadplanta.Tables[0].Rows[0]["C_NAME"].ToString();
                    calidadplanta = conexion.devuelve_horasXReferenciaXOperario(Convert.ToInt32(tbMolde.Text), Convert.ToInt32(CalidadNumero.Text));
                    CalidadHoras.Text = HasRows(calidadplanta) ? calidadplanta.Tables[0].Rows[0]["TIEMPOHORAS"].ToString() : "0";
                }

                DataSet encargado = conexion.devuelve_encargado_logueadoXMaquina(tbMaquina.Text);
                if (HasRows(encargado))
                {
                    EncargadoNumero.Text = encargado.Tables[0].Rows[0]["C_CLOCKNO"].ToString();
                    EncargadoNombre.Text = encargado.Tables[0].Rows[0]["C_NAME"].ToString();
                    encargado = conexion.devuelve_horasXReferenciaXOperario(Convert.ToInt32(tbMolde.Text), Convert.ToInt32(EncargadoNumero.Text));
                    EncargadoHoras.Text = HasRows(encargado) ? encargado.Tables[0].Rows[0]["TIEMPOHORAS"].ToString() : "0";
                }

                DataSet operario = conexion.devuelve_operario_logueadoXMaquina(tbMaquina.Text);
                if (HasRows(operario))
                {
                    Operario1Numero.Text = operario.Tables[0].Rows[0]["C_CLOCKNO"].ToString();
                    Operario1Nombre.Text = operario.Tables[0].Rows[0]["C_NAME"].ToString();
                    if (operario.Tables[0].Rows.Count > 1)
                    {
                        Operario2Numero.Text = operario.Tables[0].Rows[1]["C_CLOCKNO"].ToString();
                        Operario2Nombre.Text = operario.Tables[0].Rows[1]["C_NAME"].ToString();
                        Operario2Posicion.Visible = true;
                        Operario2Nivel.Visible = true;
                        Operario2Horas.Visible = true;
                        Operario2Nombre.Visible = true;
                        Operario2UltRevision.Visible = true;
                        Operario2Numero.Visible = true;
                        Operario2Notas.Visible = true;
                    }

                    operario = conexion.devuelve_horasXReferenciaXOperario(Convert.ToInt32(tbMolde.Text), Convert.ToInt32(Operario1Numero.Text));
                    if (HasRows(operario))
                    {
                        Operario1Horas.Text = operario.Tables[0].Rows[0]["TIEMPOHORAS"].ToString();
                        //compruebo horas y asigno valor (revincular a aplicación)
                        double DoubleOperarioHoras = Convert.ToDouble(Operario1Horas.Text);
                        if (DoubleOperarioHoras < 10)
                        {
                            Operario1Nivel.SelectedValue = "I";
                            alertaoperario.Visible = true;
                        }
                        if (DoubleOperarioHoras > 10 && DoubleOperarioHoras < 80)
                        { Operario1Nivel.SelectedValue = "L"; }
                        if (DoubleOperarioHoras > 80)
                        { Operario1Nivel.SelectedValue = "U"; }
                    }
                    else
                    {
                        Operario1Horas.Text = "0";
                        alertaoperario.Visible = true;
                    }

            if (tipo == "calidad") ds = conexion.devuelve_calidadplanta_logueadoXMaquina(maquina);
            else if (tipo == "encargado") ds = conexion.devuelve_encargado_logueadoXMaquina(maquina);
            else if (tipo == "cambiador") ds = conexion.devuelve_cambiador_logueadoXMaquina(maquina);

            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0) return;

            var row = ds.Tables[0].Rows[0];
            tbNumero.Text = row["C_CLOCKNO"].ToString();
            tbNombre.Text = row["C_NAME"].ToString();

            var horas = conexion.devuelve_horasXReferenciaXOperario(Convert.ToInt32(molde), Convert.ToInt32(tbNumero.Text));
            tbHoras.Text = horas.Tables[0].Rows.Count > 0 ? horas.Tables[0].Rows[0]["TIEMPOHORAS"].ToString() : "0";
        }

        private void CargarOperarios(Conexion_LIBERACIONES conexion, string maquina, string molde)
        {
            var ds = conexion.devuelve_operario_logueadoXMaquina(maquina);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0) return;

            var tabla = ds.Tables[0];
            var row1 = tabla.Rows[0];
            Operario1Numero.Text = row1["C_CLOCKNO"].ToString();
            Operario1Nombre.Text = row1["C_NAME"].ToString();
            AsignaHorasYNivel(conexion, molde, Operario1Numero, Operario1Horas, Operario1Nivel);

            if (tabla.Rows.Count > 1)
            {
                var row2 = tabla.Rows[1];
                Operario2Numero.Text = row2["C_CLOCKNO"].ToString();
                Operario2Nombre.Text = row2["C_NAME"].ToString();
                MostrarControlesOperario2(true);
                AsignaHorasYNivel(conexion, molde, Operario2Numero, Operario2Horas, Operario2Nivel);
            }
        }

        private void AsignaHorasYNivel(Conexion_LIBERACIONES conexion, string molde,
                                       TextBox tbNumero, TextBox tbHoras, DropDownList ddlNivel)
        {
            var ds = conexion.devuelve_horasXReferenciaXOperario(Convert.ToInt32(molde), Convert.ToInt32(tbNumero.Text));
            double horas = 0;

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                horas = Convert.ToDouble(ds.Tables[0].Rows[0]["TIEMPOHORAS"]);

            tbHoras.Text = horas.ToString();

            if (horas < 10)
            {
                ddlNivel.SelectedValue = "I";
                alertaoperario.Visible = true;
            }
            else ddlNivel.SelectedValue = (horas < 80) ? "L" : "U";
        }

        private void MostrarControlesOperario2(bool visible)
        {
            Control[] controles = {
        Operario2Posicion, Operario2Nivel, Operario2Horas, Operario2Nombre,
        Operario2UltRevision, Operario2Numero, Operario2Notas
    };
            foreach (var c in controles)
                if (c != null) c.Visible = visible;
        }


        public void CargarParametros()
        {
            try
            {
                var conexion = new Conexion_LIBERACIONES();
                int refId = Convert.ToInt32(tbReferencia.Text);
                int maqId = Convert.ToInt32(tbMaquina.Text);
                int moldeId = Convert.ToInt32(tbMolde.Text);

                EXISTEFICHA.Text = "0";
                bool existeFicha = conexion.leerFicha(refId, maqId).Tables[0].Rows.Count > 0;

                if (existeFicha)
                    CargarDatos(conexion, refId, maqId, false);
                else
                    CargarDatos(conexion, moldeId, maqId, true);
            }
            catch
            {
                alertafichafabricacion.Visible = true;
                EXISTEFICHA.Text = "1";
            }
        }

        private void CargarDatos(Conexion_LIBERACIONES conexion, int id, int maqId, bool esMolde)
        {
            string sufijo = esMolde ? "MOLDE" : "";
            DataSet ds;
            DataRow row;

            ds = Llama(conexion, $"leerFicha{sufijo}", id, maqId);
            row = ds.Tables[0].Rows[0];
            Asigna(row, "VelocidadCarga", thVCarga);
            Asigna(row, "Carga", thCarga);
            Asigna(row, "Descompresion", thDescomp);
            Asigna(row, "Contrapresion", thContrapr);
            Asigna(row, "Tiempo", thTiempo);
            Asigna(row, "Enfriamiento", thEnfriamiento);
            Asigna(row, "Ciclo", thCiclo);
            Asigna(row, "Cojin", thCojin);

            ds = Llama(conexion, $"leerTempCilindro{sufijo}", id, maqId);
            AsignaSecuencia(ds, "T", 10, "thT");
            Asigna(ds, "Boq", thBoq);

            ds = Llama(conexion, $"leerTempCamCaliente{sufijo}", id, maqId);
            AsignaSecuencia(ds, "Z", 20, "thZ");

            ds = Llama(conexion, $"leerInyeccion{sufijo}", id, maqId);
            Asigna(ds, "Tiempo", tbTiempoInyeccion);
            Asigna(ds, "LimitePresion", tbLimitePresion);

            ds = Llama(conexion, $"leerPostpresion{sufijo}", id, maqId);
            AsignaSecuencia(ds, "P", 10, "thP");
            AsignaSecuencia(ds, "T", 10, "thTP");
            Asigna(ds, "Conmutacion", tbConmutacion);
            Asigna(ds, "TiempoPresion", tbTiempoPresion);

            ds = Llama(conexion, $"leerAtemperado{sufijo}", id, maqId);
            AsignaCircuitos(conexion, ds, "M", 6);
            AsignaCircuitos(conexion, ds, "F", 6);
            AsignaSecuencia(ds, "CaudalF", 6, "TbCaudalF");
            AsignaSecuencia(ds, "CaudalM", 6, "TbCaudalM");
            AsignaSecuencia(ds, "TemperaturaF", 6, "TbTemperaturaF");
            AsignaSecuencia(ds, "TemperaturaM", 6, "TbTemperaturaM");

            ds = Llama(conexion, $"leerTolerancias{sufijo}", id, maqId);
            AsignaCamposTolerancia(ds);
        }

        private DataSet Llama(Conexion_LIBERACIONES c, string metodo, int refId, int maqId)
        {
            var m = c.GetType().GetMethod(metodo);
            return (DataSet)m.Invoke(c, new object[] { refId, maqId });
        }

        private void Asigna(DataSet ds, string campo, TextBox destino)
        {
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Columns.Contains(campo))
                destino.Text = ds.Tables[0].Rows[0][campo].ToString();
        }

        private void Asigna(DataRow row, string campo, TextBox destino)
        {
            if (row.Table.Columns.Contains(campo))
                destino.Text = row[campo].ToString();
        }

        private void AsignaSecuencia(DataSet ds, string prefijo, int cantidad, string controlPrefijo)
        {
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0) return;
            var row = ds.Tables[0].Rows[0];

            for (int i = 1; i <= cantidad; i++)
            {
                var ctrl = FindControl($"{controlPrefijo}{i}") as TextBox;
                if (ctrl != null && row.Table.Columns.Contains($"{prefijo}{i}"))
                    ctrl.Text = row[$"{prefijo}{i}"].ToString();
            }
        }

        private void AsignaCircuitos(Conexion_LIBERACIONES c, DataSet ds, string tipo, int cantidad)
        {
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0) return;
            var row = ds.Tables[0].Rows[0];

            for (int i = 1; i <= cantidad; i++)
            {
                var ctrl = FindControl($"TbCircuito{tipo}{i}") as TextBox;
                if (ctrl == null) continue;
                string val = row.Table.Columns.Contains($"Circuito{tipo}{i}") ? row[$"Circuito{tipo}{i}"].ToString() : "";
                ctrl.Text = val != "1" && !string.IsNullOrEmpty(val)
                    ? c.devuelve_tipo_atemperado(Convert.ToInt16(val)).ToString()
                    : "";
            }
        }

        private void AsignaCamposTolerancia(DataSet ds)
        {
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0) return;
            var row = ds.Tables[0].Rows[0];

            foreach (DataColumn col in row.Table.Columns)
            {
                var ctrl = FindControl(col.ColumnName) as TextBox;
                if (ctrl != null) ctrl.Text = row[col.ColumnName].ToString();
            }
        }

        public void CargarMateriasPrimas()
        {
            try
            {
                var conexion = new Conexion_LIBERACIONES();

                var materiales = conexion.devuelve_materiasprimasXReferencias(tbReferencia.Text);
                if (materiales != null && materiales.Tables.Count > 0)
                    CargarBloqueMaterial("MAT", materiales, 3);

                var componentes = conexion.devuelve_componentesXReferencias(tbReferencia.Text);
                if (componentes != null && componentes.Tables.Count > 0)
                    CargarBloqueMaterial("COMP", componentes, 5);
            }
            catch { }
        }

        private void CargarBloqueMaterial(string prefijo, DataSet ds, int maxItems)
        {
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0) return;
            var tabla = ds.Tables[0];

            for (int i = 0; i < Math.Min(tabla.Rows.Count, maxItems); i++)
            {
                var row = tabla.Rows[i];
                int index = i + 1;

                MostrarControles(prefijo, index, true);
                AsignaTexto($"{prefijo}{index}REF", row["C_ID"]);
                AsignaTexto($"{prefijo}{index}NOM", row["C_LONG_DESCR"]);

                if (prefijo == "MAT" && row.Table.Columns.Contains("C_USERVALUE1"))
                    AsignaTexto($"{prefijo}{index}TIEMP", row["C_USERVALUE1"]);
            }
        }

        private void MostrarControles(string prefijo, int index, bool visible)
        {
            string[] visibles = {
        $"SAVE{index}", $"LOT{index}", $"LOT2{index}", $"NOM{index}", $"REF{index}",
        $"TEMP{index}", $"TEMPREAL{index}", $"TIEMP{index}", $"TIEMPREAL{index}"
    };

            foreach (string sufijo in visibles)
            {
                var ctrl = FindControl($"{prefijo}{sufijo}");
                if (ctrl != null) ctrl.Visible = visible;
            }
        }

        private void AsignaTexto(string controlId, object valor)
        {
            var ctrl = FindControl(controlId) as TextBox;
            if (ctrl != null)
                ctrl.Text = valor != null ? valor.ToString() : "";
        }

        // --- MANTENIMIENTO ---

        public void CargarMantenimiento()
        {
            try
            {
                var conexion = new Conexion_LIBERACIONES();

                // Molde
                CargarBloqueMantenimiento(
                    conexion.Devuelve_reparaciones_molde(Convert.ToInt32(tbMolde.Text)),
                    tbParteMolde, TbMantMolde, TbRepaMolde, TbEstadoRepMolde, dgv_mantmolde
                );

                // Máquina
                CargarBloqueMantenimiento(
                    conexion.Devuelve_reparaciones_maquina(tbMaquina.Text),
                    tbParteMaq, TbMantMaq, TbRepaMaq, TbEstadoRepMaq, dgv_mantmaq
                );
            }
            catch { }
        }

        private void CargarBloqueMantenimiento(
            DataSet ds,
            TextBox tbParte, TextBox tbMotivo, TextBox tbReparacion, TextBox tbEstado,
            GridView grid)
        {
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0) return;

            var row = ds.Tables[0].Rows[0];

            if (row.Table.Columns.Contains("IdReparacionMolde"))
                tbParte.Text = row["IdReparacionMolde"].ToString();
            else if (row.Table.Columns.Contains("IdMantenimiento"))
                tbParte.Text = row["IdMantenimiento"].ToString();
            else
                tbParte.Text = "";

            tbMotivo.Text = row["MotivoAveria"].ToString();
            tbReparacion.Text = row["Reparacion"].ToString();
            tbEstado.Text = row["Texto"].ToString();

            grid.DataSource = ds;
            grid.DataBind();
        }

        public void CrearLiberacion()
        {
            try
            {
                var conexion = new Conexion_LIBERACIONES();

                // --- Variables base ---
                int version = 0;
                int CambiadorLiberado = 0, ProduccionLiberado = 0, CalidadLiberado = 0;
                int ResultadoLiberacion = 0, Reliberacion = 0;
                int NCEncargado = 0, GP12Encargado = 0, NCCalidad = 0, GP12Calidad = 0;

                // --- Datos de operarios ---
                int op1Num = ToInt(Operario1Numero.Text);
                double op1Horas = ToDouble(Operario1Horas.Text);
                int op2Num = ToInt(Operario2Numero.Text);
                double op2Horas = ToDouble(Operario2Horas.Text);
                int encNum = ToInt(EncargadoNumero.Text);
                double encHoras = ToDouble(EncargadoHoras.Text);
                int cambNum = ToInt(CambiadorNumero.Text);
                double cambHoras = ToDouble(CambiadorHoras.Text);
                int calNum = ToInt(CalidadNumero.Text);
                double calHoras = ToDouble(CalidadHoras.Text);

                // --- Bloques numéricos ---
                double[] thP = LeerBloqueDouble("thP", 10);
                double[] thTP = LeerBloqueDouble("thTP", 10);
                double[] thZ = LeerBloqueDouble("thZ", 20);

                double[] thT = new double[11];
                thT[0] = ToDouble(thBoq.Text);
                for (int i = 1; i <= 10; i++)
                    thT[i] = ToDouble(((TextBox)FindControl($"thT{i}")).Text);

                // --- Parámetros ---
                double tIny = ToDouble(tbTiempoInyeccion.Text);
                double limPres = ToDouble(tbLimitePresion.Text);
                double vCarga = ToDouble(thVCarga.Text);
                double carga = ToDouble(thCarga.Text);
                double descomp = ToDouble(thDescomp.Text);
                double contrap = ToDouble(thContrapr.Text);
                double tiempo = ToDouble(thTiempo.Text);
                double enfriamiento = ToDouble(thEnfriamiento.Text);
                double ciclo = ToDouble(thCiclo.Text);
                double cojin = ToDouble(thCojin.Text);

                // --- Tolerancias ---
                double[] tol = {
            ToDouble(tbTiempoInyeccionNVal.Text), ToDouble(tbTiempoInyeccionMVal.Text),
            ToDouble(tbLimitePresionNVal.Text), ToDouble(tbLimitePresionMVal.Text),
            ToDouble(thConmuntaciontolNVal.Text), ToDouble(thConmuntaciontolMVal.Text),
            ToDouble(tbTiempoPresiontolNVal.Text), ToDouble(tbTiempoPresiontolMVal.Text),
            ToDouble(TNvcargaval.Text), ToDouble(TMvcargaval.Text),
            ToDouble(TNcargaval.Text), ToDouble(TMcargaval.Text),
            ToDouble(TNdescomval.Text), ToDouble(TMdescomval.Text),
            ToDouble(TNcontrapval.Text), ToDouble(TMcontrapval.Text),
            ToDouble(TNTiempdosval.Text), ToDouble(TMTiempdosval.Text),
            ToDouble(TNEnfriamval.Text), ToDouble(TMEnfriamval.Text),
            ToDouble(TNCicloval.Text), ToDouble(TMCicloval.Text),
            ToDouble(TNCojinval.Text), ToDouble(TMCojinval.Text)
        };

                // --- Materiales (3 bloques) ---
                double[,] mat = new double[3, 4];
                for (int i = 1; i <= 3; i++)
                {
                    mat[i - 1, 0] = ToDouble(((TextBox)FindControl($"MAT{i}TIEMP")).Text);
                    mat[i - 1, 1] = ToDouble(((TextBox)FindControl($"MAT{i}TIEMPREAL")).Text);
                    mat[i - 1, 2] = ToDouble(((TextBox)FindControl($"MAT{i}TEMP")).Text);
                    mat[i - 1, 3] = ToDouble(((TextBox)FindControl($"MAT{i}TEMPREAL")).Text);
                }

                // --- Auditoría ---
                int[] Q_E = new int[10];
                int[] Q_C = new int[10];

                // --- Guardado ---
                conexion.Crear_ficha_liberacion(
                    tbOrden.Text, ToInt(tbReferencia.Text), tbMaquina.Text, version,
                    ToInt(tbMolde.Text), op1Num, op1Horas, Operario1Nivel.SelectedValue,
                    Operario1UltRevision.Text, Operario1Notas.Text, op2Num, op2Horas,
                    Operario2Nivel.SelectedValue, Operario2UltRevision.Text, Operario2Notas.Text,
                    encNum, encHoras, cambNum, cambHoras, calNum, calHoras,
                    CambiadorLiberado, LiberacionCambiadorHora.Text,
                    ProduccionLiberado, LiberacionEncargadoHora.Text,
                    CalidadLiberado, LiberacionCalidadHora.Text,
                    ResultadoLiberacion, ToInt(LiberacionCondicionada.SelectedValue),
                    NotaLiberacionCondicionada.Text, Reliberacion, NCEncargado,
                    GP12Encargado, NCCalidad, GP12Calidad,
                    Convert.ToDateTime(tbFechaCambio.Text).ToString("dd/MM/yyyy HH:mm"),

                    // --- POSTPRESION ---
                    thP[0], thTP[0], thP[1], thTP[1], thP[2], thTP[2], thP[3], thTP[3],
                    thP[4], thTP[4], thP[5], thTP[5], thP[6], thTP[6], thP[7], thTP[7],
                    thP[8], thTP[8], thP[9], thTP[9], tbConmutacion.Text, tbTiempoPresion.Text,

                    // --- CAMARA CALIENTE ---
                    thZ[0], thZ[1], thZ[2], thZ[3], thZ[4], thZ[5], thZ[6], thZ[7], thZ[8], thZ[9],
                    thZ[10], thZ[11], thZ[12], thZ[13], thZ[14], thZ[15], thZ[16], thZ[17], thZ[18], thZ[19],

                    // --- TEMPERATURA CILINDRO ---
                    thT[0], thT[1], thT[2], thT[3], thT[4], thT[5], thT[6], thT[7], thT[8], thT[9], thT[10], EXISTEFICHA.Text,

                    // --- TOLERANCIAS ---
                    tIny, limPres, vCarga, carga, descomp, contrap, tiempo, enfriamiento, ciclo, cojin,
                    tol[0], tol[1], tol[2], tol[3], tol[4], tol[5], tol[6], tol[7],
                    tol[8], tol[9], tol[10], tol[11], tol[12], tol[13], tol[14], tol[15],
                    tol[16], tol[17], tol[18], tol[19], tol[20], tol[21], tol[22], tol[23],

                    // --- MATERIALES ---
                    MAT1REF.Text, MAT1NOM.Text, MAT1LOT.Text, mat[0, 0], mat[0, 1], mat[0, 2], mat[0, 3],
                    MAT2REF.Text, MAT2NOM.Text, MAT2LOT.Text, mat[1, 0], mat[1, 1], mat[1, 2], mat[1, 3],
                    MAT3REF.Text, MAT3NOM.Text, MAT3LOT.Text, mat[2, 0], mat[2, 1], mat[2, 2], mat[2, 3],

                    // --- COMPONENTES ---
                    COMP1REF.Text, COMP1NOM.Text, COMP1LOT.Text,
                    COMP2REF.Text, COMP2NOM.Text, COMP2LOT.Text,
                    COMP3REF.Text, COMP3NOM.Text, COMP3LOT.Text,
                    COMP4REF.Text, COMP4NOM.Text, COMP4LOT.Text,
                    COMP5REF.Text, COMP5NOM.Text, COMP5LOT.Text,
                    COMP6REF.Text, COMP6NOM.Text, COMP6LOT.Text,
                    COMP7REF.Text, COMP7NOM.Text, COMP7LOT.Text,

                    // --- ATEMPERADO ---
                    TbCircuitoF1.Text, TbCircuitoF2.Text, TbCircuitoF3.Text, TbCircuitoF4.Text, TbCircuitoF5.Text, TbCircuitoF6.Text,
                    TbCaudalF1.Text, TbCaudalF2.Text, TbCaudalF3.Text, TbCaudalF4.Text, TbCaudalF5.Text, TbCaudalF6.Text,
                    TbTemperaturaF1.Text, TbTemperaturaF2.Text, TbTemperaturaF3.Text, TbTemperaturaF4.Text, TbTemperaturaF5.Text, TbTemperaturaF6.Text,
                    TbCircuitoM1.Text, TbCircuitoM2.Text, TbCircuitoM3.Text, TbCircuitoM4.Text, TbCircuitoM5.Text, TbCircuitoM6.Text,
                    TbCaudalM1.Text, TbCaudalM2.Text, TbCaudalM3.Text, TbCaudalM4.Text, TbCaudalM5.Text, TbCaudalM6.Text,
                    TbTemperaturaM1.Text, TbTemperaturaM2.Text, TbTemperaturaM3.Text, TbTemperaturaM4.Text, TbTemperaturaM5.Text, TbTemperaturaM6.Text,

                    // --- AUDITORÍA ---
                    Q_E[0], TbQ1ENC.Text, Q_E[1], TbQ2ENC.Text, Q_E[2], TbQ3ENC.Text, Q_E[3], TbQ4ENC.Text,
                    Q_E[4], TbQ5ENC.Text, Q_E[5], Q_C[5], TbQ6ENC.Text, TbQ6CAL.Text, Q_E[6], Q_C[6], TbQ7ENC.Text, TbQ7CAL.Text,
                    Q_E[7], Q_C[7], TbQ8ENC.Text, TbQ8CAL.Text, Q_E[8], Q_C[8], TbQ9ENC.Text, TbQ9CAL.Text, Q_C[9], TbQ10CAL.Text,
                    QXFeedbackCambiador.Text, QXFeedbackProduccion.Text
                );
            }
            catch { }
        }

        // --- Helpers numéricos ---

        private int ToInt(string text)
        {
            int val;
            return int.TryParse(text, out val) ? val : 0;
        }

        private double ToDouble(string text)
        {
            double val;
            return double.TryParse(text.Replace('.', ','), out val) ? val : 0.0;
        }

        private double[] LeerBloqueDouble(string prefijo, int cantidad)
        {
            double[] valores = new double[cantidad];
            for (int i = 1; i <= cantidad; i++)
            {
                var ctrl = FindControl($"{prefijo}{i}") as TextBox;
                valores[i - 1] = ctrl != null ? ToDouble(ctrl.Text) : 0.0;
            }
            return valores;
        }


        public void CargarFichaLiberacion()
        {
            try
            {
                alertaoperario.Visible = false;
                var conexion = new Conexion_LIBERACIONES();
                var ds = conexion.CargaLiberacionFicha(tbOrden.Text);
                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0) return;

                var row = ds.Tables[0].Rows[0];

                // --- Personal ---
                CargarPersona(conexion, "Calidad", CalidadNumero, CalidadNombre, CalidadHoras, row);
                CargarPersona(conexion, "Encargado", EncargadoNumero, EncargadoNombre, EncargadoHoras, row);
                CargarPersona(conexion, "Cambiador", CambiadorNumero, CambiadorNombre, CambiadorHoras, row);
                CargarOperario(conexion, "Operario1", Operario1Numero, Operario1Nombre, Operario1Horas, Operario1Nivel, Operario1Notas, row, true);

                // --- Operario 2 ---
                Operario2Numero.Text = row["Operario2"].ToString();
                if (!string.IsNullOrEmpty(Operario2Numero.Text) && Operario2Numero.Text != "0")
                {
                    Operario2Nombre.Text = conexion.Devuelve_Nombre_Operario(Operario2Numero.Text);
                    Operario2Horas.Text = row["Operario2Horas"].ToString();
                    Operario2Nivel.SelectedValue = row["Operario2Nivel"].ToString();
                    Operario2Notas.Text = row["Operario2Notas"].ToString();

                    foreach (var c in new Control[] { Operario2Posicion, Operario2Nivel, Operario2Horas, Operario2Nombre, Operario2UltRevision, Operario2Numero, Operario2Notas })
                        if (c != null) c.Visible = true;

                    if (Operario2Nivel.SelectedValue == "I")
                        alertaoperario.Visible = true;
                }

                // --- Liberación condicionada ---
                LiberacionCondicionada.SelectedValue = row["AccionLiberado"].ToString();
                NotaLiberacionCondicionada.Text = row["NotasLiberado"].ToString();

                // --- Estados de liberación ---
                MostrarEstadoLiberacion("Cambiador", row, LiberacionCambiador, LiberacionCambiadorHoraORI, LiberacionCambiadorHora,
                                        estadocambiadorSN, estadocambiadorCOND, estadocambiadorLIBOK);
                MostrarEstadoLiberacion("Produccion", row, LiberacionEncargado, LiberacionEncargadoHoraORI, LiberacionEncargadoHora,
                                        estadoencargadoSN, estadoencargadoCOND, estadoencargadoLIBOK);
                MostrarEstadoLiberacion("Calidad", row, LiberacionCalidad, LiberacionCalidadHoraORI, LiberacionCalidadHora,
                                        estadocalidadSN, estadocalidadCOND, estadocalidadLIBOK);

                // --- Indicadores ---
                MostrarIndicador(row, "ENCNoconformidad", A3, A3OK);
                MostrarIndicador(row, "ENCDefectos", A5, A5OK);
                MostrarIndicador(row, "CALNoconformidad", A7, A7OK);
                MostrarIndicador(row, "CALDefectos", A8, A8OK);

                // --- Validación ---
                if (int.TryParse(row["ValidadoING"].ToString(), out int validadoPor) && validadoPor > 0)
                {
                    var sh = new Conexion_SMARTH();
                    DropValidadJefeProyecto.SelectedValue = sh.Devuelve_Pilotos_SMARTH(validadoPor);
                }
            }
            catch { }
        }

        private void CargarPersona(Conexion_LIBERACIONES c, string prefijo, TextBox numero, TextBox nombre, TextBox horas, DataRow row)
        {
            numero.Text = row[prefijo].ToString();
            nombre.Text = c.Devuelve_Nombre_Operario(numero.Text);
            horas.Text = row[$"{prefijo}Horas"].ToString();
        }

        private void CargarOperario(Conexion_LIBERACIONES c, string prefijo, TextBox numero, TextBox nombre, TextBox horas,
                                    DropDownList nivel, TextBox notas, DataRow row, bool alerta)
        {
            numero.Text = row[prefijo].ToString();
            nombre.Text = c.Devuelve_Nombre_Operario(numero.Text);
            horas.Text = row[$"{prefijo}Horas"].ToString();
            nivel.SelectedValue = row[$"{prefijo}Nivel"].ToString();
            notas.Text = row[$"{prefijo}Notas"].ToString();

            if (alerta && nivel.SelectedValue == "I")
                alertaoperario.Visible = true;
        }

        private void MostrarEstadoLiberacion(string tipo, DataRow row, TextBox lbl, TextBox horaORI, TextBox hora,
                                             Control sn, Control cond, Control ok)
        {
            int estado = ToInt(row[$"{tipo}Liberado"].ToString());
            horaORI.Text = row[$"ORIFecha{tipo}Liberado"].ToString();
            hora.Text = horaORI.Text == row[$"Fecha{tipo}Liberado"].ToString() ? "" : row[$"Fecha{tipo}Liberado"].ToString();

            sn.Visible = cond.Visible = ok.Visible = false;

            switch (estado)
            {
                case 0: lbl.Text = "Sin liberar"; sn.Visible = true; break;
                case 1: lbl.Text = "Liberada OK condicionada"; cond.Visible = true; break;
                case 2: lbl.Text = "Liberada OK"; ok.Visible = true; break;
            }
        }

        private void MostrarIndicador(DataRow row, string campo, Control normal, Control ok)
        {
            if (int.TryParse(row[campo].ToString(), out int valor) && valor == 1)
            {
                normal.Visible = false;
                ok.Visible = true;
            }
        }

        // --- PARÁMETROS LIBERACIÓN ---


        private void AsignarCamposTolerancia(DataSet ds)
        {
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0) return;

            var row = ds.Tables[0].Rows[0];
            foreach (DataColumn col in row.Table.Columns)
            {
                // Busca un TextBox cuyo ID coincida con el nombre de la columna
                var ctrl = FindControl(col.ColumnName) as TextBox;
                if (ctrl != null)
                    ctrl.Text = row[col].ToString();
            }
        }

        public void CargarParametrosLiberacion()
        {
            try
            {
                var conexion = new Conexion_LIBERACIONES();
                int orden = ToInt(tbOrden.Text);

                // --- Temperatura cilindro ---
                var ds = conexion.LeerTempCilindroLIBERACION(orden);
                AsignarValores(ds, new[] { "Boq" }, "th", "", "", 1);
                AsignarValores(ds, new[] { "T" }, "th", "", "", 10);
                AsignarValores(ds, new[] { "LIBBoq" }, "th", "REAL", "", 1);
                AsignarValores(ds, new[] { "LIBT" }, "th", "REAL", "", 10);

                EXISTEFICHA.Text = Valor(ds, "EXISTEFICHA");
                alertafichafabricacion.Visible = EXISTEFICHA.Text == "1";

                // --- Cámara caliente ---
                ds = conexion.LeerTempCamCalienteLIBERACION(orden);
                AsignarValores(ds, new[] { "Z" }, "th", "", "", 20);
                AsignarValores(ds, new[] { "LIBZ" }, "th", "REAL", "", 20);

                // --- Postpresión ---
                ds = conexion.LeerPostpresionLIBERACION(orden);
                AsignarValores(ds, new[] { "P" }, "th", "", "", 10);
                AsignarValores(ds, new[] { "T" }, "thTP", "", "", 10);
                tbConmutacion.Text = Valor(ds, "Conmutacion");
                tbTiempoPresion.Text = Valor(ds, "TiempoPresion");
                AsignarValores(ds, new[] { "LIBP" }, "th", "R", "", 10);
                AsignarValores(ds, new[] { "LIBT" }, "thTP", "R", "", 10);
                tbConmutacionREAL.Text = Valor(ds, "LIBConmutacion");
                tbTiempoPresionREAL.Text = Valor(ds, "LIBTiempoPresion");

                // --- Atemperado ---
                ds = conexion.LeerAtemperadoLIBERACION(orden);
                AsignarBloque(ds, "CircuitoM", TbCircuitoM1, 6);
                AsignarBloque(ds, "CircuitoF", TbCircuitoF1, 6);
                AsignarBloque(ds, "CaudalF", TbCaudalF1, 6);
                AsignarBloque(ds, "CaudalM", TbCaudalM1, 6);
                AsignarBloque(ds, "TemperaturaF", TbTemperaturaF1, 6);
                AsignarBloque(ds, "TemperaturaM", TbTemperaturaM1, 6);
                AsignarBloque(ds, "LIBCaudalF", TbCaudalF1REAL, 6);
                AsignarBloque(ds, "LIBCaudalM", TbCaudalM1REAL, 6);
                AsignarBloque(ds, "LIBTemperaturaF", TbTemperaturaF1REAL, 6);
                AsignarBloque(ds, "LIBTemperaturaM", TbTemperaturaM1REAL, 6);

                // --- Tolerancias y parámetros ---
                ds = conexion.LeerToleranciasLIBERACION(orden);
                AsignarCamposTolerancia(ds);
                AsignarCampo(ds, tbTiempoInyeccion, "TiempoInyeccion");
                AsignarCampo(ds, tbLimitePresion, "LimitePresion");
                AsignarCampo(ds, thVCarga, "VelocidadCarga");
                AsignarCampo(ds, thCarga, "Carga");
                AsignarCampo(ds, thDescomp, "Descompresion");
                AsignarCampo(ds, thContrapr, "Contrapresion");
                AsignarCampo(ds, thTiempo, "Tiempo");
                AsignarCampo(ds, thEnfriamiento, "Enfriamiento");
                AsignarCampo(ds, thCiclo, "Ciclo");
                AsignarCampo(ds, thCojin, "Cojin");

                AsignarCampo(ds, tbTiempoInyeccionREAL, "LIBTiempoInyeccion");
                AsignarCampo(ds, tbLimitePresionREAL, "LIBLimitePresion");
                AsignarCampo(ds, thVCargaREAL, "LIBVelocidadCarga");
                AsignarCampo(ds, thCargaREAL, "LIBCarga");
                AsignarCampo(ds, thDescompREAL, "LIBDescompresion");
                AsignarCampo(ds, thContraprREAL, "LIBContrapresion");
                AsignarCampo(ds, thTiempoREAL, "LIBTiempo");
                AsignarCampo(ds, thEnfriamientoREAL, "LIBEnfriamiento");
                AsignarCampo(ds, thCicloREAL, "LIBCiclo");
                AsignarCampo(ds, thCojinREAL, "LIBCojin");
            }
            catch { }
        }

        private string Valor(DataSet ds, string col)
        {
            return (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Columns.Contains(col))
                ? ds.Tables[0].Rows[0][col].ToString() : "";
        }

        private void AsignarCampo(DataSet ds, TextBox control, string columna)
        {
            if (control != null) control.Text = Valor(ds, columna);
        }

        private void AsignarBloque(DataSet ds, string prefijo, TextBox inicio, int cantidad)
        {
            for (int i = 1; i <= cantidad; i++)
            {
                var ctrl = inicio.Parent.FindControl(inicio.ID.Replace("1", i.ToString())) as TextBox;
                if (ctrl != null) ctrl.Text = Valor(ds, $"{prefijo}{i}");
            }
        }

        private void AsignarValores(DataSet ds, string[] columnas, string prefijo, string sufijo, string tabla = "", int cantidad = 1)
        {
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0) return;

            foreach (string baseCol in columnas)
            {
                for (int i = 1; i <= cantidad; i++)
                {
                    string col = baseCol + (cantidad > 1 ? i.ToString() : "");
                    string ctrlId = $"{prefijo}{(cantidad > 1 ? i.ToString() : "")}{sufijo}";
                    var ctrl = FindControl(ctrlId) as TextBox;
                    if (ctrl != null && ds.Tables[0].Columns.Contains(col))
                        ctrl.Text = ds.Tables[0].Rows[0][col].ToString();
                }
            }
        }

        // --- CUESTIONARIOS ---

        public void CargarCuestionariosLiberacion()
        {
            try
            {
                var conexion = new Conexion_LIBERACIONES();
                var ds = conexion.LeerAuditoriaLIBERACION(ToInt(tbOrden.Text));
                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0) return;

                var row = ds.Tables[0].Rows[0];
                QXFeedbackCambiador.Text = Valor(row, "QXFeedbackCambiador");
                LiberacionCambiadorNotas.Text = Valor(row, "QXFeedbackCambiador");
                QXFeedbackProduccion.Text = Valor(row, "QXFeedbackProduccion");
                LiberacionEncargadoNotas.Text = Valor(row, "QXFeedbackProduccion");
                QXFeedbackCalidad.Text = Valor(row, "QXFeedbackCalidad");
                LiberacionCalidadNotas.Text = Valor(row, "QXFeedbackCalidad");

                for (int i = 1; i <= 9; i++) ProcesarPregunta(row, "E", i);
                for (int i = 6; i <= 10; i++) ProcesarPregunta(row, "C", i);
            }
            catch { }
        }

        public void CargarMateriasPrimasLiberacion()
        {
            try
            {
                var conexion = new Conexion_LIBERACIONES();
                var ds = conexion.LeerEstructuraLIBERACION(ToInt(tbOrden.Text));
                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0) return;

                var row = ds.Tables[0].Rows[0];

                // --- Helpers locales para evitar dependencias/duplicados ---
                string Get(DataRow r, string col) => r.Table.Columns.Contains(col) ? r[col]?.ToString() ?? "" : "";
                void SetText(string id, string val) { var c = FindControl(id) as TextBox; if (c != null) c.Text = val ?? ""; }
                void SetVisible(string id, bool v) { var c = FindControl(id); if (c != null) c.Visible = v; }

                // --- MATERIALES (1..3) ---
                for (int i = 1; i <= 3; i++)
                {
                    string pref = $"MAT{i}";
                    string referencia = Get(row, $"{pref}REF");
                    if (string.IsNullOrWhiteSpace(referencia)) continue;

                    // Mostrar controles necesarios
                    SetVisible($"{pref}REF", true);
                    SetVisible($"{pref}NOM", true);
                    SetVisible($"{pref}LOT", true);
                    SetVisible($"{pref}LOT2", true);
                    SetVisible($"{pref}REMARK2", true);
                    SetVisible($"{pref}TIEMPREAL", true);
                    SetVisible($"{pref}TEMPREAL", true);
                    SetVisible($"MATSAVE{i}", true);

                    // Asignar textos
                    SetText($"{pref}REF", referencia);
                    SetText($"{pref}NOM", Get(row, $"{pref}NOM"));

                    // Lote con posible sublote separado por '|'
                    var lot = Get(row, $"{pref}LOT");
                    var partes = (lot ?? "").Split('|');
                    SetText($"{pref}LOT", partes.Length > 0 ? partes[0] : "");
                    SetText($"{pref}LOT2", partes.Length > 1 ? partes[1] : "");

                    // Tiempos y temperaturas
                    SetText($"{pref}TIEMP", Get(row, $"{pref}TIEMP"));
                    SetText($"{pref}TIEMPREAL", Get(row, $"{pref}TIEMPREAL"));
                    SetText($"{pref}TEMP", Get(row, $"{pref}TEMP"));
                    SetText($"{pref}TEMPREAL", Get(row, $"{pref}TEMPREAL"));

                    // Remark desde BMS
                    SetText($"{pref}REMARK2", conexion.devuelve_remark_materiasprimasBMS(referencia));
                }

                // --- COMPONENTES (1..7) ---
                for (int i = 1; i <= 7; i++)
                {
                    string pref = $"COMP{i}";
                    string referencia = Get(row, $"{pref}REF");
                    if (string.IsNullOrWhiteSpace(referencia)) continue;

                    // Mostrar controles necesarios
                    SetVisible($"{pref}REF", true);
                    SetVisible($"{pref}NOM", true);
                    SetVisible($"{pref}LOT", true);
                    SetVisible($"{pref}LOT2", true);
                    SetVisible($"COMPSAVE{i}", true);

                    // Asignar textos
                    SetText($"{pref}REF", referencia);
                    SetText($"{pref}NOM", Get(row, $"{pref}NOM"));

                    // Lote con posible sublote separado por '|'
                    var lot = Get(row, $"{pref}LOT");
                    var partes = (lot ?? "").Split('|');
                    SetText($"{pref}LOT", partes.Length > 0 ? partes[0] : "");
                    SetText($"{pref}LOT2", partes.Length > 1 ? partes[1] : "");
                }
            }
            catch { }
        }


        private string Valor(DataRow row, string col)
        {
            return row.Table.Columns.Contains(col) ? row[col].ToString() : "";
        }

        private void ProcesarPregunta(DataRow row, string tipo, int numero)
        {
            string colValor = $"Q{numero}{tipo}";
            string colTexto = tipo == "E" ? $"Q{numero}ENC" : $"Q{numero}CAL";

            int valor = ToInt(Valor(row, colValor));
            TextBox txt = FindControl($"TbQ{numero}{(tipo == "E" ? "ENC" : "CAL")}") as TextBox;
            if (txt != null) txt.Text = Valor(row, colTexto);

            var okBtn = FindControl($"Q{numero}{tipo}_OK") as WebControl;
            var nokBtn = FindControl($"Q{numero}{tipo}_NOK") as WebControl;
            var naBtn = FindControl($"Q{numero}{tipo}_NA") as WebControl;

            var okOpt = FindControl($"Q{numero}{tipo}_OKOPT") as CheckBox;
            var nokOpt = FindControl($"Q{numero}{tipo}_NOKOPT") as CheckBox;
            var naOpt = FindControl($"Q{numero}{tipo}_NAOPT") as CheckBox;

            if (okBtn == null || nokBtn == null || naBtn == null) return;

            okBtn.Attributes["Class"] = nokBtn.Attributes["Class"] = naBtn.Attributes["Class"] = "btn btn-lg btn-primary";
            okBtn.Attributes.Remove("style"); nokBtn.Attributes.Remove("style"); naBtn.Attributes.Remove("style");
            if (okOpt != null) okOpt.Checked = false;
            if (nokOpt != null) nokOpt.Checked = false;
            if (naOpt != null) naOpt.Checked = false;

            switch (valor)
            {
                case 0:
                    naBtn.Attributes["Class"] += " active";
                    if (naOpt != null) naOpt.Checked = true;
                    break;
                case 1:
                    nokBtn.Attributes["Class"] += " active";
                    nokBtn.Attributes["style"] = "background-color:red";
                    if (nokOpt != null) nokOpt.Checked = true;
                    break;
                case 2:
                    okBtn.Attributes["Class"] += " active";
                    okBtn.Attributes["style"] = "background-color:forestgreen";
                    if (okOpt != null) okOpt.Checked = true;
                    break;
            }
        }

        private void PintarCeldasDevacion()
        {
            try
            {
                ResumenPROD1.Visible = false;
                ResumenPROD2.Visible = false;
                ResumenPROD3.Visible = false;
                matparamNC.Visible = false;

                // --- CILINDRO ---
                VerificarDesviacion(new[] { "thBoq" });
                VerificarDesviacion(Enumerable.Range(1, 10).Select(i => $"thT{i}").ToArray());

                // --- CÁMARA CALIENTE ---
                VerificarDesviacion(Enumerable.Range(1, 20).Select(i => $"thZ{i}").ToArray());

                // --- POSTPRESIÓN ---
                VerificarDesviacion(Enumerable.Range(1, 10).Select(i => $"thP{i}").ToArray(), "R");
                VerificarDesviacion(Enumerable.Range(1, 10).Select(i => $"thTP{i}").ToArray(), "R");

                // --- LÍMITES / OTROS CAMPOS ---
                VerificarRango("tbConmutacionREAL", "thConmuntaciontolNVal", "thConmuntaciontolMVal");
                VerificarRango("tbTiempoPresionREAL", "tbTiempoPresiontolNVal", "tbTiempoPresiontolMVal");
                VerificarRango("tbTiempoInyeccionREAL", "tbTiempoInyeccionNVal", "tbTiempoInyeccionMVal");
                VerificarRango("tbLimitePresionREAL", "tbLimitePresionNVal", "tbLimitePresionMVal");
                VerificarRango("thVCargaREAL", "TNvcargaval", "TMvcargaval");
                VerificarRango("thCargaREAL", "TNcargaval", "TMcargaval");
                VerificarRango("thDescompREAL", "TNdescomval", "TMdescomval");
                VerificarRango("thContraprREAL", "TNcontrapval", "TMcontrapval");
                VerificarRango("thTiempoREAL", "TNTiempdosval", "TMTiempdosval");
                VerificarRango("thEnfriamientoREAL", "TNEnfriamval", "TMEnfriamval");
                VerificarRango("thCicloREAL", "TNCicloval", "TMCicloval");
                VerificarRango("thCojinREAL", "TNCojinval", "TMCojinval");
            }
            catch (Exception) { }
        }

        // ---------------- MÉTODOS AUXILIARES ----------------

        private void VerificarDesviacion(string[] nombres, string sufijoReal = "REAL")
        {
            foreach (var nombre in nombres)
            {
                var txt = FindControl(nombre) as TextBox;
                var txtReal = FindControl(nombre + sufijoReal) as TextBox;
                if (txt == null || txtReal == null) continue;

                if (!double.TryParse(txt.Text.Replace('.', ','), out double valor) ||
                    !double.TryParse(txtReal.Text.Replace('.', ','), out double real))
                    continue;

                bool fueraRango = real > valor * 1.1 || real < valor * 0.9;
                PintarCelda(txtReal, fueraRango);
            }
        }

        private void VerificarRango(string campoReal, string campoMin, string campoMax)
        {
            var realCtrl = FindControl(campoReal) as TextBox;
            var minCtrl = FindControl(campoMin) as TextBox;
            var maxCtrl = FindControl(campoMax) as TextBox;

            if (realCtrl == null || minCtrl == null || maxCtrl == null) return;
            if (!double.TryParse(realCtrl.Text.Replace('.', ','), out double real) ||
                !double.TryParse(minCtrl.Text.Replace('.', ','), out double min) ||
                !double.TryParse(maxCtrl.Text.Replace('.', ','), out double max))
                return;

            bool fueraRango = real > max || real < min;
            PintarCelda(realCtrl, fueraRango);
        }

        private void PintarCelda(TextBox control, bool fueraRango)
        {
            if (fueraRango)
            {
                control.BackColor = System.Drawing.Color.Red;
                control.ForeColor = System.Drawing.Color.White;
                ResumenPROD1.Visible = true;
                ResumenPROD2.Visible = true;
                if (!string.IsNullOrEmpty(MotivoCambioParam.Text))
                    ResumenPROD2.Text = MotivoCambioParam.Text;
                matparamNC.Visible = true;
            }
            else
            {
                control.BackColor = System.Drawing.Color.Empty;
                control.ForeColor = System.Drawing.Color.Black;
            }
        }

        public void EvaluaParam()
        {
            try
            {
                PARAMSESTADO.Text = "";

                // --- CILINDRO ---
                if (EvaluaDesviacion(new[] { "thBoq" })) PARAMSESTADO.Text = "1";
                if (EvaluaDesviacion(Enumerable.Range(1, 10).Select(i => $"thT{i}").ToArray())) PARAMSESTADO.Text = "1";

                // --- CÁMARA CALIENTE ---
                if (EvaluaDesviacion(Enumerable.Range(1, 20).Select(i => $"thZ{i}").ToArray())) PARAMSESTADO.Text = "1";

                // --- POSTPRESIÓN ---
                if (EvaluaDesviacion(Enumerable.Range(1, 10).Select(i => $"thP{i}").ToArray(), "R")) PARAMSESTADO.Text = "1";
                if (EvaluaDesviacion(Enumerable.Range(1, 10).Select(i => $"thTP{i}").ToArray(), "R")) PARAMSESTADO.Text = "1";

                // --- LÍMITES ---
                if (EvaluaRango("tbConmutacionREAL", "thConmuntaciontolNVal", "thConmuntaciontolMVal")) PARAMSESTADO.Text = "1";
                if (EvaluaRango("tbTiempoPresionREAL", "tbTiempoPresiontolNVal", "tbTiempoPresiontolMVal")) PARAMSESTADO.Text = "1";
                if (EvaluaRango("tbTiempoInyeccionREAL", "tbTiempoInyeccionNVal", "tbTiempoInyeccionMVal")) PARAMSESTADO.Text = "1";
                if (EvaluaRango("tbLimitePresionREAL", "tbLimitePresionNVal", "tbLimitePresionMVal")) PARAMSESTADO.Text = "1";
                if (EvaluaRango("thVCargaREAL", "TNvcargaval", "TMvcargaval")) PARAMSESTADO.Text = "1";
                if (EvaluaRango("thCargaREAL", "TNcargaval", "TMcargaval")) PARAMSESTADO.Text = "1";
                if (EvaluaRango("thDescompREAL", "TNdescomval", "TMdescomval")) PARAMSESTADO.Text = "1";
                if (EvaluaRango("thContraprREAL", "TNcontrapval", "TMcontrapval")) PARAMSESTADO.Text = "1";
                if (EvaluaRango("thTiempoREAL", "TNTiempdosval", "TMTiempdosval")) PARAMSESTADO.Text = "1";
                if (EvaluaRango("thEnfriamientoREAL", "TNEnfriamval", "TMEnfriamval")) PARAMSESTADO.Text = "1";
                if (EvaluaRango("thCicloREAL", "TNCicloval", "TMCicloval")) PARAMSESTADO.Text = "1";
                if (EvaluaRango("thCojinREAL", "TNCojinval", "TMCojinval")) PARAMSESTADO.Text = "1";
            }
            catch (Exception)
            {
                // Silencio de errores, como en el original
            }
        }

        // ---------------- MÉTODOS AUXILIARES ----------------

        private bool EvaluaDesviacion(string[] nombres, string sufijoReal = "REAL")
        {
            foreach (var nombre in nombres)
            {
                var txt = FindControl(nombre) as TextBox;
                var txtReal = FindControl(nombre + sufijoReal) as TextBox;
                if (txt == null || txtReal == null) continue;

                if (!double.TryParse(txt.Text.Replace('.', ','), out double valor) ||
                    !double.TryParse(txtReal.Text.Replace('.', ','), out double real))
                    continue;

                if (real > valor * 1.1 || real < valor * 0.9)
                    return true;
            }
            return false;
        }

        private bool EvaluaRango(string campoReal, string campoMin, string campoMax)
        {
            var realCtrl = FindControl(campoReal) as TextBox;
            var minCtrl = FindControl(campoMin) as TextBox;
            var maxCtrl = FindControl(campoMax) as TextBox;

            if (realCtrl == null || minCtrl == null || maxCtrl == null)
                return false;

            if (!double.TryParse(realCtrl.Text.Replace('.', ','), out double real) ||
                !double.TryParse(minCtrl.Text.Replace('.', ','), out double min) ||
                !double.TryParse(maxCtrl.Text.Replace('.', ','), out double max))
                return false;

            return (real > max || real < min);
        }


        public void RellenarVacíos()
        {
            try
            {
                // --- CILINDRO ---
                RellenarBloque(new[] { "thBoq" });
                RellenarBloque(Enumerable.Range(1, 10).Select(i => $"thT{i}").ToArray());

                // --- CÁMARA CALIENTE ---
                RellenarBloque(Enumerable.Range(1, 20).Select(i => $"thZ{i}").ToArray());

                // --- POSTPRESIÓN ---
                RellenarBloque(Enumerable.Range(1, 10).Select(i => $"thP{i}").ToArray(), "R");
                RellenarBloque(Enumerable.Range(1, 10).Select(i => $"thTP{i}").ToArray(), "R");

                // --- ATEMPERADO ---
                RellenarBloque(Enumerable.Range(1, 6).Select(i => $"TbCaudalF{i}").ToArray());
                RellenarBloque(Enumerable.Range(1, 6).Select(i => $"TbCaudalM{i}").ToArray());
                RellenarBloque(Enumerable.Range(1, 6).Select(i => $"TbTemperaturaF{i}").ToArray());
                RellenarBloque(Enumerable.Range(1, 6).Select(i => $"TbTemperaturaM{i}").ToArray());

                // --- LÍMITES ---
                RellenarSiVacio("tbConmutacionREAL", "tbConmutacion");
                RellenarSiVacio("tbTiempoPresionREAL", "tbTiempoPresion");
                RellenarSiVacio("tbLimitePresionREAL", "tbLimitePresion");
                RellenarSiVacio("thVCargaREAL", "thVCarga");
                RellenarSiVacio("thCargaREAL", "thCarga");
                RellenarSiVacio("thDescompREAL", "thDescomp");
                RellenarSiVacio("thContraprREAL", "thContrapr");
                RellenarSiVacio("thTiempoREAL", "thTiempo");
                RellenarSiVacio("thEnfriamientoREAL", "thEnfriamiento");
                RellenarSiVacio("thCicloREAL", "thCiclo");
            }
            catch (Exception)
            {
                // Igual que en el original, ignoramos excepciones
            }
        }

        // ---------------- MÉTODOS AUXILIARES ----------------

        private void RellenarBloque(IEnumerable<string> nombres, string sufijoReal = "REAL")
        {
            foreach (var nombre in nombres)
            {
                RellenarSiVacio(nombre + sufijoReal, nombre);
            }
        }

        private void RellenarSiVacio(string campoReal, string campoBase)
        {
            var realCtrl = FindControl(campoReal) as TextBox;
            var baseCtrl = FindControl(campoBase) as TextBox;

            if (realCtrl != null && baseCtrl != null && string.IsNullOrWhiteSpace(realCtrl.Text))
                realCtrl.Text = baseCtrl.Text;
        }

        public void CargarResultados()
        {
            try
            {
                // --- REINICIO DE ESTADO ---
                formaNC.Visible = false;
                audinc.Visible = false;
                SINDESVIACIONES.Visible = true;

                OcultarBloques(new[]
                {
            "ResumenFORM1","ResumenFORM2","ResumenFORM3","ResumenFORM4","ResumenFORM4ILUO",
            "ResumenMAT1","ResumenGRAL1","ResumenGRAL2","ResumenGRAL3",
            "Resumen1","Resumen1ENC","Resumen2","Resumen2ENC","Resumen3","Resumen3ENC",
            "Resumen4","Resumen4ENC","Resumen5","Resumen5ENC","Resumen6","Resumen6ENC",
            "Resumen6CAL","Resumen7","Resumen7ENC","Resumen7CAL","Resumen8","Resumen8ENC",
            "Resumen8CAL","Resumen9","Resumen9ENC","Resumen9CAL","Resumen10","Resumen10CAL"
        });

                // --- DESVIACIONES DE FICHA ---
                if (EXISTEFICHA.Text == "1")
                {
                    ResumenPROD3.Visible = true;
                    matparamNC.Visible = true;
                    SINDESVIACIONES.Visible = false;
                }

                // --- VALIDACIÓN DE FORMULARIOS Y CONDICIONES ---
                if (LiberacionEncargadoHoraORI.Text != "" || LiberacionCalidadHoraORI.Text != "")
                {
                    EvaluarFormularios();
                    EvaluarOperarios();
                    EvaluarMateriales();
                    EvaluarCuestionarios();
                    EvaluarValidacionIngenieria();
                }
            }
            catch (Exception) { }
        }

        // ---------------- MÉTODOS AUXILIARES ----------------

        private void OcultarBloques(IEnumerable<string> nombres)
        {
            foreach (var nombre in nombres)
            {
                var ctrl = FindControl(nombre) as Control;
                if (ctrl != null) ctrl.Visible = false;
            }
        }

        private void EvaluarFormularios()
        {
            var condiciones = new (bool visible, string resumen)[]
            {
        (A3.Visible, "ResumenFORM1"),
        (A5.Visible, "ResumenFORM2"),
        (A7.Visible, "ResumenFORM3"),
        (A8.Visible, "ResumenFORM4")
            };

            foreach (var (visible, resumen) in condiciones)
            {
                if (visible)
                {
                    ActivarDesviacion(resumen);
                }
            }
        }

        private void EvaluarOperarios()
        {
            bool iluOperario1 = Operario1Nivel.SelectedValue.ToString() == "I";
            bool iluOperario2 = Operario2Nivel.Visible && Operario2Nivel.SelectedValue.ToString() == "I";

            if (iluOperario1 || iluOperario2)
                ActivarDesviacion("ResumenFORM4ILUO");
        }

        private void EvaluarMateriales()
        {
            var materiales = new (Control Ref, TextBox Lote)[]
            {
        (MAT1REF, MAT1LOT), (MAT2REF, MAT2LOT), (MAT3REF, MAT3LOT),
        (COMP1REF, COMP1LOT), (COMP2REF, COMP2LOT), (COMP3REF, COMP3LOT),
        (COMP4REF, COMP4LOT), (COMP5REF, COMP5LOT), (COMP6REF, COMP6LOT), (COMP7REF, COMP7LOT)
            };

            if (materiales.Any(m => m.Ref.Visible && string.IsNullOrEmpty(m.Lote.Text)))
            {
                ResumenMAT1.Visible = true;
                matparamNC.Visible = true;
            }
        }

        private void EvaluarCuestionarios()
        {
            var cuestionarios = new[]
            {
        new { NOK = Q1_NOKOPT, Enc = TbQ1ENC, Cal = (TextBox)null, Prefix = "Resumen1" },
        new { NOK = Q2_NOKOPT, Enc = TbQ2ENC, Cal = (TextBox)null, Prefix = "Resumen2" },
        new { NOK = Q3_NOKOPT, Enc = TbQ3ENC, Cal = (TextBox)null, Prefix = "Resumen3" },
        new { NOK = Q4_NOKOPT, Enc = TbQ4ENC, Cal = (TextBox)null, Prefix = "Resumen4" },
        new { NOK = Q5_NOKOPT, Enc = TbQ5ENC, Cal = (TextBox)null, Prefix = "Resumen5" },
        new { NOK = Q6_NOKOPT, Enc = TbQ6ENC, Cal = TbQ6CAL, Prefix = "Resumen6" },
        new { NOK = Q6C_NOKOPT, Enc = TbQ6ENC, Cal = TbQ6CAL, Prefix = "Resumen6" },
        new { NOK = Q7_NOKOPT, Enc = TbQ7ENC, Cal = TbQ7CAL, Prefix = "Resumen7" },
        new { NOK = Q7C_NOKOPT, Enc = TbQ7ENC, Cal = TbQ7CAL, Prefix = "Resumen7" },
        new { NOK = Q8_NOKOPT, Enc = TbQ8ENC, Cal = TbQ8CAL, Prefix = "Resumen8" },
        new { NOK = Q8C_NOKOPT, Enc = TbQ8ENC, Cal = TbQ8CAL, Prefix = "Resumen8" },
        new { NOK = Q9_NOKOPT, Enc = TbQ9ENC, Cal = TbQ9CAL, Prefix = "Resumen9" },
        new { NOK = Q9C_NOKOPT, Enc = TbQ9ENC, Cal = TbQ9CAL, Prefix = "Resumen9" },
        new { NOK = Q10C_NOKOPT, Enc = (TextBox)null, Cal = TbQ10CAL, Prefix = "Resumen10" }
    };

            foreach (var q in cuestionarios)
            {
                if (q.NOK.Checked)
                {
                    ResumenGRAL1.Visible = true;
                    ResumenGRAL2.Visible = true;
                    ResumenGRAL3.Visible = true;

                    ActivarDesviacion(q.Prefix);
                    ActivarDesviacion(q.Prefix + "ENC", q.Enc?.Text);
                    ActivarDesviacion(q.Prefix + "CAL", q.Cal?.Text);

                    audinc.Visible = true;
                }
            }
        }

        private void EvaluarValidacionIngenieria()
        {
            bool necesitaValidacion = Convert.ToInt32(LiberacionCondicionada.SelectedValue) == 4 || ResumenPROD1.Visible || ResumenPROD3.Visible;
            if (!necesitaValidacion) return;

            lblValidadPORING.Visible = true;
            DropValidadJefeProyecto.Visible = true;
            btnValidarING.Visible = true;

            Conexion_LIBERACIONES conexion = new Conexion_LIBERACIONES();
            if (conexion.Existe_Desviacion_Sin_Notificar(tbOrden.Text.Trim()))
            {
                string producto = tbReferencia.Text.Trim() + " " + tbNombre.Text.Trim();
                string parametros = ResumenPROD1.Visible ? "<br><strong>-</strong> Parámetros fuera de especificación." : "";
                string retenido = Convert.ToInt32(LiberacionCondicionada.SelectedValue) == 4 ? "<br><strong>-</strong> Producción retenida por liberación NoK." : "";

                mandar_mail(tbOrden.Text.Trim(), producto, parametros, retenido);
                conexion.Marca_Mail_Enviado_Alerta_Desviacion(tbOrden.Text);
            }
        }

        private void ActivarDesviacion(string controlId, string texto = null)
        {
            var ctrl = FindControl(controlId) as Control;
            if (ctrl != null)
            {
                ctrl.Visible = true;
                if (ctrl is TextBox txt && texto != null)
                    txt.Text = texto;
            }

            formaNC.Visible = true;
            SINDESVIACIONES.Visible = false;
        }





























        public void ValidarIngenieria(Object sender, EventArgs e)
        {
            Conexion_SMARTH SHConexion = new Conexion_SMARTH();
            Conexion_LIBERACIONES conexion = new Conexion_LIBERACIONES();

            conexion.Validar_Ingenieria(tbOrden.Text.Trim(), SHConexion.Devuelve_ID_Piloto_SMARTH(DropValidadJefeProyecto.SelectedValue.ToString()));

        }
        public void LiberarCambiador(Object sender, EventArgs e)
        {
            try
            {
                string QUERYLiberarCambiadorHoraORI = "";
                if (LiberacionCambiadorHoraORI.Text == "")
                {
                    QUERYLiberarCambiadorHoraORI = ", ORIFechaCambiadorLiberado = '" + DateTime.Now.ToString("dd/MM/yyy HH:mm") + "'";
                }
                LiberacionCambiadorHoraORI.Text = DateTime.Now.ToString("dd/MM/yyy HH:mm");
                LiberacionCambiadorHora.Text = DateTime.Now.ToString("dd/MM/yyy HH:mm");
                if (CambiadorNombre.Text == "")
                {
                    ReCargarCambiador(null, null);
                }
                LiberarCambiadorFunction(QUERYLiberarCambiadorHoraORI);
                CargarFichaLiberacion();
                CargarParametrosLiberacion();
                CargarCuestionariosLiberacion();
                CargarMateriasPrimasLiberacion();
                CargarResultados();
                lkb_Sort_Click("CAMBIO");
            }
            catch (Exception)
            {
            }
        }
        public void LiberarEncargado(Object sender, EventArgs e)
        {
            try
            {
                if (Q4_NAOPT.Checked == true && Q5_NAOPT.Checked == true && Q6_NAOPT.Checked == true && LiberacionEncargadoHoraORI.Text == ""
                    || ComprobarParametrosLiberacion() == false)
                {
                    lkb_Sort_Click("PARAMETROS");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "liberar_encargado_NOK();", true);

                }

                else
                {
                    string QUERYLiberarEncargadoHoraORI = "";
                    if (LiberacionEncargadoHoraORI.Text == "")
                    {
                        QUERYLiberarEncargadoHoraORI = ", ORIFechaProduccionLiberado = '" + DateTime.Now.ToString("dd/MM/yyy HH:mm") + "'";
                    }
                    LiberacionEncargadoHoraORI.Text = DateTime.Now.ToString("dd/MM/yyy HH:mm");
                    LiberacionEncargadoHora.Text = DateTime.Now.ToString("dd/MM/yyy HH:mm");
                    if (EncargadoNombre.Text == "")
                    {
                        ReCargarEncargado(null, null);
                    }
                    if (Operario1Nombre.Text == "")
                    {
                        ReCargarOperarios(null, null);
                    }
                    RellenarVacíos();
                    EvaluaParam();
                    LiberarFunction("", QUERYLiberarEncargadoHoraORI, "");
                    CargarFichaLiberacion();
                    CargarParametrosLiberacion();
                    CargarCuestionariosLiberacion();
                    CargarMateriasPrimasLiberacion();
                    PintarCeldasDevacion();
                    CargarResultados();
                    lkb_Sort_Click("PROCESO");
                }


            }
            catch (Exception) { }
        }
        public bool ComprobarParametrosLiberacion()
        {
            try
            {
                Conexion_LIBERACIONES conexion = new Conexion_LIBERACIONES();
                DataSet ds = conexion.ComprobarParametrosLiberacion(Convert.ToInt32(tbOrden.Text));

                if (ds.Tables[0].Rows[0]["LIBTiempoInyeccion"].ToString() == "" && ds.Tables[0].Rows[0]["LIBCojin"].ToString() == "") { return false; }
                else { return true; }

            }
            catch (Exception) { }
            return false;


        }

        public void LiberarAUDCalidad(Object sender, EventArgs e)
        {
            try
            {
                string QUERYLiberacionCalidadHoraORI = "";
                if (LiberacionCalidadHoraORI.Text == "")
                {
                    QUERYLiberacionCalidadHoraORI = ", ORIFechaCalidadLiberado = '" + DateTime.Now.ToString("dd/MM/yyy HH:mm") + "'";
                }
                LiberacionCalidadHoraORI.Text = DateTime.Now.ToString("dd/MM/yyy HH:mm");
                LiberacionCalidadHora.Text = DateTime.Now.ToString("dd/MM/yyy HH:mm");
                if (EncargadoNombre.Text == "")
                {
                    ReCargarEncargado(null, null);
                }
                if (Operario1Nombre.Text == "")
                {
                    ReCargarOperarios(null, null);
                }
                if (CalidadNombre.Text == "")
                {
                    ReCargarCalidad(null, null);
                }

                EvaluaParam();
                //LiberarFunction("", "",QUERYLiberacionCalidadHoraORI);
                LiberarCalidadFunction(QUERYLiberacionCalidadHoraORI);
                CargarFichaLiberacion();
                CargarParametrosLiberacion();
                CargarCuestionariosLiberacion();
                CargarMateriasPrimasLiberacion();
                PintarCeldasDevacion();
                CargarResultados();
                lkb_Sort_Click("CALIDAD");
            }
            catch (Exception) { }
        }
        public void LiberarParametros(object sender, EventArgs e)
        {
            try
            {
                // --- RELLENAR CAMPOS VACÍOS ---
                try
                {
                    RellenarParametrosVacios();
                }
                catch (Exception)
                {
                    // Igual que en el original: ignoramos errores
                }

                // --- EVALUACIÓN Y REFRESCO DE LA PÁGINA ---
                EvaluaParam();
                LiberarFunction("", "", "");
                CargarFichaLiberacion();
                CargarParametrosLiberacion();
                CargarCuestionariosLiberacion();
                CargarMateriasPrimasLiberacion();
                PintarCeldasDevacion();
                CargarResultados();
                lkb_Sort_Click("PARAMETROS");
            }
            catch (Exception)
            {
                // Silencio de excepciones como en el original
            }
        }

        // ---------------- MÉTODO AUXILIAR ----------------

        private void RellenarParametrosVacios()
        {
            // --- CILINDRO ---
            RellenarBloque(new[] { "thBoq" });
            RellenarBloque(Enumerable.Range(1, 10).Select(i => $"thT{i}").ToArray());

            // --- CÁMARA CALIENTE ---
            RellenarBloque(Enumerable.Range(1, 20).Select(i => $"thZ{i}").ToArray());

            // --- POSTPRESIÓN ---
            RellenarBloque(Enumerable.Range(1, 10).Select(i => $"thP{i}").ToArray(), "R");
            RellenarBloque(Enumerable.Range(1, 10).Select(i => $"thTP{i}").ToArray(), "R");

            // --- ATEMPERADO ---
            RellenarBloque(Enumerable.Range(1, 6).Select(i => $"TbCaudalF{i}").ToArray());
            RellenarBloque(Enumerable.Range(1, 6).Select(i => $"TbCaudalM{i}").ToArray());
            RellenarBloque(Enumerable.Range(1, 6).Select(i => $"TbTemperaturaF{i}").ToArray());
            RellenarBloque(Enumerable.Range(1, 6).Select(i => $"TbTemperaturaM{i}").ToArray());

            // --- LÍMITES ---
            RellenarSiVacio("tbConmutacionREAL", "tbConmutacion");
            RellenarSiVacio("tbTiempoPresionREAL", "tbTiempoPresion");
            RellenarSiVacio("tbTiempoInyeccionREAL", "tbTiempoInyeccion");
            RellenarSiVacio("tbLimitePresionREAL", "tbLimitePresion");
            RellenarSiVacio("thVCargaREAL", "thVCarga");
            RellenarSiVacio("thCargaREAL", "thCarga");
            RellenarSiVacio("thDescompREAL", "thDescomp");
            RellenarSiVacio("thContraprREAL", "thContrapr");
            RellenarSiVacio("thTiempoREAL", "thTiempo");
            RellenarSiVacio("thEnfriamientoREAL", "thEnfriamiento");
            RellenarSiVacio("thCicloREAL", "thCiclo");
            RellenarSiVacio("thCojinREAL", "thCojin");
        }

        public void LiberarFunction(string QUERYLiberarCambiadorHoraORI, string QUERYLiberarEncargadoHoraORI, string QUERYLiberacionCalidadHoraORI)
        {
            try
            {
                int version = 0;

                // === DATOS DE OPERARIOS ===
                int operario1numero = ParseInt(Operario1Numero.Text);
                double operario1horas = ParseDouble(Operario1Horas.Text);

                int operario2numero = ParseInt(Operario2Numero.Text);
                double operario2horas = ParseDouble(Operario2Horas.Text);

                int encargadonumero = ParseInt(EncargadoNumero.Text);
                double encargadohoras = ParseDouble(EncargadoHoras.Text);

                int cambiadornumero = ParseInt(CambiadorNumero.Text);
                double cambiadorhoras = ParseDouble(CambiadorHoras.Text);

                int calidadnumero = ParseInt(CalidadNumero.Text);
                double calidadhoras = ParseDouble(CalidadHoras.Text);

                // === ALERTAS DE NIVEL DE OPERARIO ===
                alertaoperario.Visible = false;
                if (Operario1Nivel.SelectedValue.ToString() == "I" ||
                   (Operario2Numero.Text != "0" && Operario2Nivel.SelectedValue.ToString() == "I"))
                {
                    alertaoperario.Visible = true;
                }

                // === ESTADOS DE LIBERACIÓN ===
                int CambiadorLiberado = 0;
                if (LiberacionCambiadorHoraORI.Text != "" && (
                    Q1_NOKOPT.Checked || Q2_NOKOPT.Checked || Q3_NOKOPT.Checked ||
                    FaltaAlgúnLote()))
                {
                    CambiadorLiberado = 1;
                }
                else if (LiberacionCambiadorHoraORI.Text != "" &&
                         !(Q1_NOKOPT.Checked || Q2_NOKOPT.Checked || Q3_NOKOPT.Checked))
                {
                    CambiadorLiberado = 2;
                }

                int ProduccionLiberado = 0;
                if (LiberacionEncargadoHoraORI.Text != "" && (
                    Q4_NOKOPT.Checked || Q5_NOKOPT.Checked || Q6_NOKOPT.Checked || Q7_NOKOPT.Checked ||
                    Q8_NOKOPT.Checked || Q9_NOKOPT.Checked || A3.Visible || A5.Visible ||
                    PARAMSESTADO.Text != "" || alertaoperario.Visible ||
                    Convert.ToInt32(EXISTEFICHA.Text) == 1))
                {
                    ProduccionLiberado = 1;
                }
                else if (LiberacionEncargadoHoraORI.Text != "" && !(
                    Q4_NOKOPT.Checked || Q5_NOKOPT.Checked || Q6_NOKOPT.Checked ||
                    Q7_NOKOPT.Checked || Q8_NOKOPT.Checked || Q9_NOKOPT.Checked))
                {
                    ProduccionLiberado = 2;
                }

                int CalidadLiberado = 0;
                if (LiberacionCalidadHoraORI.Text != "" && (
                    Q10C_NOKOPT.Checked || Q6C_NOKOPT.Checked || Q7C_NOKOPT.Checked ||
                    Q8C_NOKOPT.Checked || Q9C_NOKOPT.Checked || A7.Visible ||
                    alertaoperario.Visible || Convert.ToInt32(EXISTEFICHA.Text) == 1))
                {
                    CalidadLiberado = 1;
                }
                else if (LiberacionCalidadHoraORI.Text != "" && !(
                    Q10C_NOKOPT.Checked || Q6C_NOKOPT.Checked || Q7C_NOKOPT.Checked ||
                    Q8C_NOKOPT.Checked || Q9C_NOKOPT.Checked))
                {
                    CalidadLiberado = 2;
                }

                int ResultadoLiberacion = 0;
                int Reliberacion = 0;

                int NCEncargado = A3OK.Visible ? 1 : 0;
                int GP12Encargado = A5OK.Visible ? 1 : 0;
                int NCCalidad = A7OK.Visible ? 1 : 0;
                int GP12Calidad = A8OK.Visible ? 1 : 0;

                // === PARÁMETROS NUMÉRICOS ===
                var thP = LeerBloqueNumerico("thP", 10);
                var thTP = LeerBloqueNumerico("thTP", 10);
                var thP_R = LeerBloqueNumerico("thP", 10, "R");
                var thTP_R = LeerBloqueNumerico("thTP", 10, "R");

                var thZ = LeerBloqueNumerico("thZ", 20);
                var thZ_REAL = LeerBloqueNumerico("thZ", 20, "REAL");

                var thT = LeerBloqueNumerico("thT", 10);
                double thBoq_double = ParseDouble(thBoq.Text);
                double thBoq_REAL_double = ParseDouble(thBoqREAL.Text);
                var thT_REAL = LeerBloqueNumerico("thT", 10, "REAL");

                // === TOLERANCIAS ===
                double tbTiempoInyeccion_double = ParseDouble(tbTiempoInyeccion.Text);
                double tbLimitePresion_double = ParseDouble(tbLimitePresion.Text);
                double thVCarga_double = ParseDouble(thVCarga.Text);
                double thCarga_double = ParseDouble(thCarga.Text);
                double thDescomp_double = ParseDouble(thDescomp.Text);
                double thContrapr_double = ParseDouble(thContrapr.Text);
                double thTiempo_double = ParseDouble(thTiempo.Text);
                double thEnfriamiento_double = ParseDouble(thEnfriamiento.Text);
                double thCiclo_double = ParseDouble(thCiclo.Text);
                double thCojin_double = ParseDouble(thCojin.Text);

                double tbTiempoInyeccion_double_REAL = ParseDouble(tbTiempoInyeccionREAL.Text);
                double tbLimitePresion_double_REAL = ParseDouble(tbLimitePresionREAL.Text);
                double thVCarga_double_REAL = ParseDouble(thVCargaREAL.Text);
                double thCarga_double_REAL = ParseDouble(thCargaREAL.Text);
                double thDescomp_double_REAL = ParseDouble(thDescompREAL.Text);
                double thContrapr_double_REAL = ParseDouble(thContraprREAL.Text);
                double thTiempo_double_REAL = ParseDouble(thTiempoREAL.Text);
                double thEnfriamiento_double_REAL = ParseDouble(thEnfriamientoREAL.Text);
                double thCiclo_double_REAL = ParseDouble(thCicloREAL.Text);
                double thCojin_double_REAL = ParseDouble(thCojinREAL.Text);

                // === MATERIALES ===
                double MAT1TIEMP_double = ParseDouble(MAT1TIEMP.Text);
                double MAT1TIEMPREAL_double = ParseDouble(MAT1TIEMPREAL.Text);
                double MAT1TEMP_double = ParseDouble(MAT1TEMP.Text);
                double MAT1TEMPREAL_double = ParseDouble(MAT1TEMPREAL.Text);

                double MAT2TIEMP_double = ParseDouble(MAT2TIEMP.Text);
                double MAT2TIEMPREAL_double = ParseDouble(MAT2TIEMPREAL.Text);
                double MAT2TEMP_double = ParseDouble(MAT2TEMP.Text);
                double MAT2TEMPREAL_double = ParseDouble(MAT2TEMPREAL.Text);

                double MAT3TIEMP_double = ParseDouble(MAT3TIEMP.Text);
                double MAT3TIEMPREAL_double = ParseDouble(MAT3TIEMPREAL.Text);
                double MAT3TEMP_double = ParseDouble(MAT3TEMP.Text);
                double MAT3TEMPREAL_double = ParseDouble(MAT3TEMPREAL.Text);

                // === CUESTIONARIOS ===
                int Q1E = ValorCheck(Q1_OKOPT, Q1_NOKOPT);
                int Q2E = ValorCheck(Q2_OKOPT, Q2_NOKOPT);
                int Q3E = ValorCheck(Q3_OKOPT, Q3_NOKOPT);
                int Q4E = ValorCheck(Q4_OKOPT, Q4_NOKOPT);
                int Q5E = ValorCheck(Q5_OKOPT, Q5_NOKOPT);
                int Q6E = ValorCheck(Q6_OKOPT, Q6_NOKOPT);
                int Q7E = ValorCheck(Q7_OKOPT, Q7_NOKOPT);
                int Q8E = ValorCheck(Q8_OKOPT, Q8_NOKOPT);
                int Q9E = ValorCheck(Q9_OKOPT, Q9_NOKOPT);

                int Q6C = ValorCheck(Q6C_OKOPT, Q6C_NOKOPT);
                int Q7C = ValorCheck(Q7C_OKOPT, Q7C_NOKOPT);
                int Q8C = ValorCheck(Q8C_OKOPT, Q8C_NOKOPT);
                int Q9C = ValorCheck(Q9C_OKOPT, Q9C_NOKOPT);
                int Q10C = ValorCheck(Q10C_OKOPT, Q10C_NOKOPT);

                // === RESULTADOS LOTES/PARAM ===
                int ResultadoLOTES = FaltaAlgúnLote() ? 1 : 0;
                int ResultadoPARAM = (PARAMSESTADO.Text != "") ? 1 : 0;

                string MAT1LOTE = MAT1LOT.Text + "|" + MAT1LOT2.Text;
                string MAT2LOTE = MAT2LOT.Text + "|" + MAT2LOT2.Text;
                string MAT3LOTE = MAT3LOT.Text + "|" + MAT3LOT2.Text;
                string COMP1LOTE = COMP1LOT.Text + "|" + COMP1LOT2.Text;
                string COMP2LOTE = COMP2LOT.Text + "|" + COMP2LOT2.Text;
                string COMP3LOTE = COMP3LOT.Text + "|" + COMP3LOT2.Text;
                string COMP4LOTE = COMP4LOT.Text + "|" + COMP4LOT2.Text;
                string COMP5LOTE = COMP5LOT.Text + "|" + COMP5LOT2.Text;
                string COMP6LOTE = COMP6LOT.Text + "|" + COMP6LOT2.Text;
                string COMP7LOTE = COMP7LOT.Text + "|" + COMP7LOT2.Text;

                // === GUARDAR EN BD ===
                Conexion_LIBERACIONES conexion = new Conexion_LIBERACIONES();

                conexion.ActualizarLiberacionFichaCambiador(
                 tbOrden.Text, Convert.ToInt32(tbReferencia.Text), tbMaquina.Text, version, Convert.ToInt32(tbMolde.Text),
                 operario1numero, operario1horas, Convert.ToString(Operario1Nivel.SelectedValue),
                 Operario1UltRevision.Text, Operario1Notas.Text,
                 operario2numero, operario2horas, Convert.ToString(Operario2Nivel.SelectedValue),
                 Operario2UltRevision.Text, Operario2Notas.Text,
                 encargadonumero, encargadohoras, cambiadornumero, cambiadorhoras,
                 calidadnumero, calidadhoras, CambiadorLiberado, LiberacionCambiadorHora.Text,
                 ProduccionLiberado, LiberacionEncargadoHora.Text,
                 CalidadLiberado, LiberacionCalidadHora.Text, ResultadoLiberacion,
                 Convert.ToInt32(LiberacionCondicionada.SelectedValue), NotaLiberacionCondicionada.Text,
                 Reliberacion, NCEncargado, GP12Encargado, NCCalidad, GP12Calidad,
                 QUERYLiberarCambiadorHoraORI, ResultadoLOTES, ResultadoPARAM,
                 // Materiales
                 MAT1REF.Text, MAT1NOM.Text, MAT1LOTE, MAT1TIEMP_double, MAT1TIEMPREAL_double, MAT1TEMP_double, MAT1TEMPREAL_double,
                 MAT2REF.Text, MAT2NOM.Text, MAT2LOTE, MAT2TIEMP_double, MAT2TIEMPREAL_double, MAT2TEMP_double, MAT2TEMPREAL_double,
                 MAT3REF.Text, MAT3NOM.Text, MAT3LOTE, MAT3TIEMP_double, MAT3TIEMPREAL_double, MAT3TEMP_double, MAT3TEMPREAL_double,
                 COMP1REF.Text, COMP1NOM.Text, COMP1LOTE, COMP2REF.Text, COMP2NOM.Text, COMP2LOTE, COMP3REF.Text, COMP3NOM.Text, COMP3LOTE,
                 COMP4REF.Text, COMP4NOM.Text, COMP4LOTE, COMP5REF.Text, COMP5NOM.Text, COMP5LOTE,
                 COMP6REF.Text, COMP6NOM.Text, COMP6LOTE, COMP7REF.Text, COMP7NOM.Text, COMP7LOTE,
                 // Auditoría
                 Q1E, TbQ1ENC.Text, Q2E, TbQ2ENC.Text, Q3E, TbQ3ENC.Text, Q4E, TbQ4ENC.Text, Q5E, TbQ5ENC.Text,
                 Q6E, Q6C, TbQ6ENC.Text, TbQ6CAL.Text, Q7E, Q7C, TbQ7ENC.Text, TbQ7CAL.Text,
                 Q8E, Q8C, TbQ8ENC.Text, TbQ8CAL.Text, Q9E, Q9C, TbQ9ENC.Text, TbQ9CAL.Text, Q10C, TbQ10CAL.Text,
                 QXFeedbackCambiador.Text, QXFeedbackProduccion.Text, QXFeedbackCalidad.Text
             );

            }
            catch (Exception)
            { }
        }

        // === FUNCIONES AUXILIARES ===
        private double ParseDouble(string text)
        {
            return double.TryParse(text.Replace('.', ','), out var val) ? val : 0.0;
        }

        private int ParseInt(string text)
        {
            return int.TryParse(text, out var val) ? val : 0;
        }

        private bool FaltaAlgúnLote()
        {
            return (MAT1REF.Visible && MAT1LOT.Text == "") ||
                   (MAT2REF.Visible && MAT2LOT.Text == "") ||
                   (MAT3REF.Visible && MAT3LOT.Text == "") ||
                   (COMP1REF.Visible && COMP1LOT.Text == "") ||
                   (COMP2REF.Visible && COMP2LOT.Text == "") ||
                   (COMP3REF.Visible && COMP3LOT.Text == "") ||
                   (COMP4REF.Visible && COMP4LOT.Text == "") ||
                   (COMP5REF.Visible && COMP5LOT.Text == "") ||
                   (COMP6REF.Visible && COMP6LOT.Text == "") ||
                   (COMP7REF.Visible && COMP7LOT.Text == "");
        }

        private Dictionary<string, double> LeerBloqueNumerico(string prefijo, int cantidad, string sufijo = "")
        {
            var dic = new Dictionary<string, double>();
            for (int i = 1; i <= cantidad; i++)
            {
                var ctrl = FindControl($"{prefijo}{i}{sufijo}") as TextBox;
                dic[$"{prefijo}{i}{sufijo}"] = ctrl != null ? ParseDouble(ctrl.Text) : 0.0;
            }
            return dic;
        }

        private int ValorCheck(object ok, object nok)
        {
            bool okChecked = false;
            bool nokChecked = false;

            if (ok is System.Web.UI.WebControls.CheckBox cb1)
                okChecked = cb1.Checked;
            else if (ok is System.Web.UI.HtmlControls.HtmlInputRadioButton rb1)
                okChecked = rb1.Checked;

            if (nok is System.Web.UI.WebControls.CheckBox cb2)
                nokChecked = cb2.Checked;
            else if (nok is System.Web.UI.HtmlControls.HtmlInputRadioButton rb2)
                nokChecked = rb2.Checked;

            if (okChecked) return 2;
            if (nokChecked) return 1;
            return 0;
        }

        public void LiberarCambiadorFunction(string QUERYLiberarCambiadorHoraORI)
        {
            try
            {
                int version = 0;

                int ParseInt(string txt) => int.TryParse(txt, out var v) ? v : 0;
                double ParseDouble(string txt) => double.TryParse(txt.Replace('.', ','), out var v) ? v : 0.0;
                int ValorCheck(object ok, object nok)
                {
                    bool okChecked = false;
                    bool nokChecked = false;

                    if (ok is System.Web.UI.WebControls.CheckBox cbOk)
                        okChecked = cbOk.Checked;
                    else if (ok is System.Web.UI.HtmlControls.HtmlInputRadioButton rbOk)
                        okChecked = rbOk.Checked;

                    if (nok is System.Web.UI.WebControls.CheckBox cbNok)
                        nokChecked = cbNok.Checked;
                    else if (nok is System.Web.UI.HtmlControls.HtmlInputRadioButton rbNok)
                        nokChecked = rbNok.Checked;

                    if (okChecked) return 2;
                    if (nokChecked) return 1;
                    return 0;
                }



                // === Datos de personal ===
                int operario1numero = ParseInt(Operario1Numero.Text);
                double operario1horas = ParseDouble(Operario1Horas.Text);
                int operario2numero = ParseInt(Operario2Numero.Text);
                double operario2horas = ParseDouble(Operario2Horas.Text);
                int encargadonumero = ParseInt(EncargadoNumero.Text);
                double encargadohoras = ParseDouble(EncargadoHoras.Text);
                int cambiadornumero = ParseInt(CambiadorNumero.Text);
                double cambiadorhoras = ParseDouble(CambiadorHoras.Text);
                int calidadnumero = ParseInt(CalidadNumero.Text);
                double calidadhoras = ParseDouble(CalidadHoras.Text);

                // === Estados de liberación ===
                int CambiadorLiberado = 0;
                if (!string.IsNullOrEmpty(LiberacionCambiadorHoraORI.Text) &&
                    (Q1_NOKOPT.Checked || Q2_NOKOPT.Checked || Q3_NOKOPT.Checked ||
                     (MAT1REF.Visible && MAT1LOT.Text == "") || (MAT2REF.Visible && MAT2LOT.Text == "") ||
                     (MAT3REF.Visible && MAT3LOT.Text == "") || (COMP1REF.Visible && COMP1LOT.Text == "") ||
                     (COMP2REF.Visible && COMP2LOT.Text == "") || (COMP3REF.Visible && COMP3LOT.Text == "") ||
                     (COMP4REF.Visible && COMP4LOT.Text == "") || (COMP5REF.Visible && COMP5LOT.Text == "") ||
                     (COMP6REF.Visible && COMP6LOT.Text == "") || (COMP7REF.Visible && COMP7LOT.Text == "")))
                {
                    CambiadorLiberado = 1;
                }
                else if (!string.IsNullOrEmpty(LiberacionCambiadorHoraORI.Text))
                {
                    CambiadorLiberado = 2;
                }

                int ProduccionLiberado = estadoencargadoCOND.Visible ? 1 : estadoencargadoLIBOK.Visible ? 2 : 0;
                int CalidadLiberado = estadocalidadCOND.Visible ? 1 : estadocalidadLIBOK.Visible ? 2 : 0;

                int ResultadoLiberacion = 0;
                int Reliberacion = 0;

                // === No conformidades y GP12 ===
                int NCEncargado = A3OK.Visible ? 1 : 0;
                int GP12Encargado = A5OK.Visible ? 1 : 0;
                int NCCalidad = A7OK.Visible ? 1 : 0;
                int GP12Calidad = A8OK.Visible ? 1 : 0;

                // === Materiales ===
                double MAT1TIEMP_double = ParseDouble(MAT1TIEMP.Text);
                double MAT1TIEMPREAL_double = ParseDouble(MAT1TIEMPREAL.Text);
                double MAT1TEMP_double = ParseDouble(MAT1TEMP.Text);
                double MAT1TEMPREAL_double = ParseDouble(MAT1TEMPREAL.Text);

                double MAT2TIEMP_double = ParseDouble(MAT2TIEMP.Text);
                double MAT2TIEMPREAL_double = ParseDouble(MAT2TIEMPREAL.Text);
                double MAT2TEMP_double = ParseDouble(MAT2TEMP.Text);
                double MAT2TEMPREAL_double = ParseDouble(MAT2TEMPREAL.Text);

                double MAT3TIEMP_double = ParseDouble(MAT3TIEMP.Text);
                double MAT3TIEMPREAL_double = ParseDouble(MAT3TIEMPREAL.Text);
                double MAT3TEMP_double = ParseDouble(MAT3TEMP.Text);
                double MAT3TEMPREAL_double = ParseDouble(MAT3TEMPREAL.Text);

                // === Auditoría ===
                int Q1E = ValorCheck(Q1_OKOPT, Q1_NOKOPT);
                int Q2E = ValorCheck(Q2_OKOPT, Q2_NOKOPT);
                int Q3E = ValorCheck(Q3_OKOPT, Q3_NOKOPT);
                int Q4E = ValorCheck(Q4_OKOPT, Q4_NOKOPT);
                int Q5E = ValorCheck(Q5_OKOPT, Q5_NOKOPT);
                int Q6E = ValorCheck(Q6_OKOPT, Q6_NOKOPT);
                int Q7E = ValorCheck(Q7_OKOPT, Q7_NOKOPT);
                int Q8E = ValorCheck(Q8_OKOPT, Q8_NOKOPT);
                int Q9E = ValorCheck(Q9_OKOPT, Q9_NOKOPT);

                int Q6C = ValorCheck(Q6C_OKOPT, Q6C_NOKOPT);
                int Q7C = ValorCheck(Q7C_OKOPT, Q7C_NOKOPT);
                int Q8C = ValorCheck(Q8C_OKOPT, Q8C_NOKOPT);
                int Q9C = ValorCheck(Q9C_OKOPT, Q9C_NOKOPT);
                int Q10C = ValorCheck(Q10C_OKOPT, Q10C_NOKOPT);

                // === Lotes y parámetros ===
                int ResultadoLOTES = (MAT1REF.Visible && MAT1LOT.Text == "") ||
                                     (MAT2REF.Visible && MAT2LOT.Text == "") ||
                                     (MAT3REF.Visible && MAT3LOT.Text == "") ||
                                     (COMP1REF.Visible && COMP1LOT.Text == "") ||
                                     (COMP2REF.Visible && COMP2LOT.Text == "") ||
                                     (COMP3REF.Visible && COMP3LOT.Text == "") ||
                                     (COMP4REF.Visible && COMP4LOT.Text == "") ||
                                     (COMP5REF.Visible && COMP5LOT.Text == "") ||
                                     (COMP6REF.Visible && COMP6LOT.Text == "") ||
                                     (COMP7REF.Visible && COMP7LOT.Text == "")
                                     ? 1 : 0;

                int ResultadoPARAM = 0;

                // === Concatenar lotes ===
                string Lote(string a, string b) => $"{a}|{b}";
                string MAT1LOTE = Lote(MAT1LOT.Text, MAT1LOT2.Text);
                string MAT2LOTE = Lote(MAT2LOT.Text, MAT2LOT2.Text);
                string MAT3LOTE = Lote(MAT3LOT.Text, MAT3LOT2.Text);
                string COMP1LOTE = Lote(COMP1LOT.Text, COMP1LOT2.Text);
                string COMP2LOTE = Lote(COMP2LOT.Text, COMP2LOT2.Text);
                string COMP3LOTE = Lote(COMP3LOT.Text, COMP3LOT2.Text);
                string COMP4LOTE = Lote(COMP4LOT.Text, COMP4LOT2.Text);
                string COMP5LOTE = Lote(COMP5LOT.Text, COMP5LOT2.Text);
                string COMP6LOTE = Lote(COMP6LOT.Text, COMP6LOT2.Text);
                string COMP7LOTE = Lote(COMP7LOT.Text, COMP7LOT2.Text);

                // === Actualización en base de datos ===
                Conexion_LIBERACIONES conexion = new Conexion_LIBERACIONES();
                conexion.ActualizarLiberacionFichaCambiador(
                    tbOrden.Text, Convert.ToInt32(tbReferencia.Text), tbMaquina.Text, version, Convert.ToInt32(tbMolde.Text),
                    operario1numero, operario1horas, Convert.ToString(Operario1Nivel.SelectedValue),
                    Operario1UltRevision.Text, Operario1Notas.Text,
                    operario2numero, operario2horas, Convert.ToString(Operario2Nivel.SelectedValue),
                    Operario2UltRevision.Text, Operario2Notas.Text,
                    encargadonumero, encargadohoras, cambiadornumero, cambiadorhoras,
                    calidadnumero, calidadhoras, CambiadorLiberado, LiberacionCambiadorHora.Text,
                    ProduccionLiberado, LiberacionEncargadoHora.Text,
                    CalidadLiberado, LiberacionCalidadHora.Text, ResultadoLiberacion,
                    Convert.ToInt32(LiberacionCondicionada.SelectedValue), NotaLiberacionCondicionada.Text,
                    Reliberacion, NCEncargado, GP12Encargado, NCCalidad, GP12Calidad,
                    QUERYLiberarCambiadorHoraORI, ResultadoLOTES, ResultadoPARAM,
                    // === Materiales ===
                    MAT1REF.Text, MAT1NOM.Text, MAT1LOTE, MAT1TIEMP_double, MAT1TIEMPREAL_double, MAT1TEMP_double, MAT1TEMPREAL_double,
                    MAT2REF.Text, MAT2NOM.Text, MAT2LOTE, MAT2TIEMP_double, MAT2TIEMPREAL_double, MAT2TEMP_double, MAT2TEMPREAL_double,
                    MAT3REF.Text, MAT3NOM.Text, MAT3LOTE, MAT3TIEMP_double, MAT3TIEMPREAL_double, MAT3TEMP_double, MAT3TEMPREAL_double,
                    COMP1REF.Text, COMP1NOM.Text, COMP1LOTE, COMP2REF.Text, COMP2NOM.Text, COMP2LOTE, COMP3REF.Text, COMP3NOM.Text, COMP3LOTE,
                    COMP4REF.Text, COMP4NOM.Text, COMP4LOTE, COMP5REF.Text, COMP5NOM.Text, COMP5LOTE,
                    COMP6REF.Text, COMP6NOM.Text, COMP6LOTE, COMP7REF.Text, COMP7NOM.Text, COMP7LOTE,
                    // === Auditoría ===
                    Q1E, TbQ1ENC.Text, Q2E, TbQ2ENC.Text, Q3E, TbQ3ENC.Text, Q4E, TbQ4ENC.Text, Q5E, TbQ5ENC.Text,
                    Q6E, Q6C, TbQ6ENC.Text, TbQ6CAL.Text, Q7E, Q7C, TbQ7ENC.Text, TbQ7CAL.Text,
                    Q8E, Q8C, TbQ8ENC.Text, TbQ8CAL.Text, Q9E, Q9C, TbQ9ENC.Text, TbQ9CAL.Text, Q10C, TbQ10CAL.Text,
                    QXFeedbackCambiador.Text, QXFeedbackProduccion.Text, QXFeedbackCalidad.Text
                );
            }
            catch (Exception ex)
            {
                // Puedes loguearlo si lo necesitas
                Console.WriteLine("Error en LiberarCambiadorFunction: " + ex.Message);
            }
        }

        public void LiberarCalidadFunction(string QUERYLiberarCalidadHoraORI)
        {
            try
            {
                int version = 0;

                // === Helpers locales ===
                int ParseInt(string txt) => int.TryParse(txt, out var v) ? v : 0;
                double ParseDouble(string txt) => double.TryParse(txt.Replace('.', ','), out var v) ? v : 0.0;
                string Lote(string a, string b) => $"{a}|{b}";

                int ValorCheck(object ok, object nok)
                {
                    bool okChecked = false, nokChecked = false;

                    if (ok is System.Web.UI.WebControls.CheckBox cbOk) okChecked = cbOk.Checked;
                    else if (ok is System.Web.UI.HtmlControls.HtmlInputRadioButton rbOk) okChecked = rbOk.Checked;

                    if (nok is System.Web.UI.WebControls.CheckBox cbNok) nokChecked = cbNok.Checked;
                    else if (nok is System.Web.UI.HtmlControls.HtmlInputRadioButton rbNok) nokChecked = rbNok.Checked;

                    if (okChecked) return 2;
                    if (nokChecked) return 1;
                    return 0;
                }

                // === Datos de personal ===
                int operario1numero = ParseInt(Operario1Numero.Text);
                double operario1horas = ParseDouble(Operario1Horas.Text);
                int operario2numero = ParseInt(Operario2Numero.Text);
                double operario2horas = ParseDouble(Operario2Horas.Text);
                int encargadonumero = ParseInt(EncargadoNumero.Text);
                double encargadohoras = ParseDouble(EncargadoHoras.Text);
                int cambiadornumero = ParseInt(CambiadorNumero.Text);
                double cambiadorhoras = ParseDouble(CambiadorHoras.Text);
                int calidadnumero = ParseInt(CalidadNumero.Text);
                double calidadhoras = ParseDouble(CalidadHoras.Text);

                // === Estados de liberación ===
                int CambiadorLiberado =
                    !string.IsNullOrEmpty(LiberacionCambiadorHoraORI.Text) &&
                    (Q1_NOKOPT.Checked || Q2_NOKOPT.Checked || Q3_NOKOPT.Checked ||
                     (MAT1REF.Visible && MAT1LOT.Text == "") ||
                     (MAT2REF.Visible && MAT2LOT.Text == "") ||
                     (MAT3REF.Visible && MAT3LOT.Text == "") ||
                     (COMP1REF.Visible && COMP1LOT.Text == "") ||
                     (COMP2REF.Visible && COMP2LOT.Text == "") ||
                     (COMP3REF.Visible && COMP3LOT.Text == "") ||
                     (COMP4REF.Visible && COMP4LOT.Text == "") ||
                     (COMP5REF.Visible && COMP5LOT.Text == "") ||
                     (COMP6REF.Visible && COMP6LOT.Text == "") ||
                     (COMP7REF.Visible && COMP7LOT.Text == ""))
                    ? 1 :
                    (!string.IsNullOrEmpty(LiberacionCambiadorHoraORI.Text) ? 2 : 0);

                int ProduccionLiberado = estadoencargadoCOND.Visible ? 1 :
                                         estadoencargadoLIBOK.Visible ? 2 : 0;

                int CalidadLiberado =
                    !string.IsNullOrEmpty(LiberacionCalidadHoraORI.Text) &&
                    (Q10C_NOKOPT.Checked || Q6C_NOKOPT.Checked || Q7C_NOKOPT.Checked ||
                     Q8C_NOKOPT.Checked || Q9C_NOKOPT.Checked || A7.Visible ||
                     alertaoperario.Visible || Convert.ToInt32(EXISTEFICHA.Text) == 1)
                    ? 1 :
                    (!string.IsNullOrEmpty(LiberacionCalidadHoraORI.Text) &&
                     !Q10C_NOKOPT.Checked && !Q6C_NOKOPT.Checked &&
                     !Q7C_NOKOPT.Checked && !Q8C_NOKOPT.Checked && !Q9C_NOKOPT.Checked ? 2 : 0);

                int ResultadoLiberacion = 0;
                int Reliberacion = 0;

                // === NC y GP12 ===
                int NCEncargado = A3OK.Visible ? 1 : 0;
                int GP12Encargado = A5OK.Visible ? 1 : 0;
                int NCCalidad = A7OK.Visible ? 1 : 0;
                int GP12Calidad = A8OK.Visible ? 1 : 0;

                // === Materiales ===
                double MAT1TIEMP_double = ParseDouble(MAT1TIEMP.Text);
                double MAT1TIEMPREAL_double = ParseDouble(MAT1TIEMPREAL.Text);
                double MAT1TEMP_double = ParseDouble(MAT1TEMP.Text);
                double MAT1TEMPREAL_double = ParseDouble(MAT1TEMPREAL.Text);

                double MAT2TIEMP_double = ParseDouble(MAT2TIEMP.Text);
                double MAT2TIEMPREAL_double = ParseDouble(MAT2TIEMPREAL.Text);
                double MAT2TEMP_double = ParseDouble(MAT2TEMP.Text);
                double MAT2TEMPREAL_double = ParseDouble(MAT2TEMPREAL.Text);

                double MAT3TIEMP_double = ParseDouble(MAT3TIEMP.Text);
                double MAT3TIEMPREAL_double = ParseDouble(MAT3TIEMPREAL.Text);
                double MAT3TEMP_double = ParseDouble(MAT3TEMP.Text);
                double MAT3TEMPREAL_double = ParseDouble(MAT3TEMPREAL.Text);

                // === Cuestionarios ===
                int Q1E = ValorCheck(Q1_OKOPT, Q1_NOKOPT);
                int Q2E = ValorCheck(Q2_OKOPT, Q2_NOKOPT);
                int Q3E = ValorCheck(Q3_OKOPT, Q3_NOKOPT);
                int Q4E = ValorCheck(Q4_OKOPT, Q4_NOKOPT);
                int Q5E = ValorCheck(Q5_OKOPT, Q5_NOKOPT);
                int Q6E = ValorCheck(Q6_OKOPT, Q6_NOKOPT);
                int Q7E = ValorCheck(Q7_OKOPT, Q7_NOKOPT);
                int Q8E = ValorCheck(Q8_OKOPT, Q8_NOKOPT);
                int Q9E = ValorCheck(Q9_OKOPT, Q9_NOKOPT);

                int Q6C = ValorCheck(Q6C_OKOPT, Q6C_NOKOPT);
                int Q7C = ValorCheck(Q7C_OKOPT, Q7C_NOKOPT);
                int Q8C = ValorCheck(Q8C_OKOPT, Q8C_NOKOPT);
                int Q9C = ValorCheck(Q9C_OKOPT, Q9C_NOKOPT);
                int Q10C = ValorCheck(Q10C_OKOPT, Q10C_NOKOPT);

                // === Lotes y parámetros ===
                int ResultadoLOTES =
                    (MAT1REF.Visible && MAT1LOT.Text == "") ||
                    (MAT2REF.Visible && MAT2LOT.Text == "") ||
                    (MAT3REF.Visible && MAT3LOT.Text == "") ||
                    (COMP1REF.Visible && COMP1LOT.Text == "") ||
                    (COMP2REF.Visible && COMP2LOT.Text == "") ||
                    (COMP3REF.Visible && COMP3LOT.Text == "") ||
                    (COMP4REF.Visible && COMP4LOT.Text == "") ||
                    (COMP5REF.Visible && COMP5LOT.Text == "") ||
                    (COMP6REF.Visible && COMP6LOT.Text == "") ||
                    (COMP7REF.Visible && COMP7LOT.Text == "") ? 1 : 0;

                int ResultadoPARAM = !string.IsNullOrEmpty(PARAMSESTADO.Text) ? 1 : 0;

                // === Concatenar lotes ===
                string MAT1LOTE = Lote(MAT1LOT.Text, MAT1LOT2.Text);
                string MAT2LOTE = Lote(MAT2LOT.Text, MAT2LOT2.Text);
                string MAT3LOTE = Lote(MAT3LOT.Text, MAT3LOT2.Text);
                string COMP1LOTE = Lote(COMP1LOT.Text, COMP1LOT2.Text);
                string COMP2LOTE = Lote(COMP2LOT.Text, COMP2LOT2.Text);
                string COMP3LOTE = Lote(COMP3LOT.Text, COMP3LOT2.Text);
                string COMP4LOTE = Lote(COMP4LOT.Text, COMP4LOT2.Text);
                string COMP5LOTE = Lote(COMP5LOT.Text, COMP5LOT2.Text);
                string COMP6LOTE = Lote(COMP6LOT.Text, COMP6LOT2.Text);
                string COMP7LOTE = Lote(COMP7LOT.Text, COMP7LOT2.Text);

                // === Guardar en BD ===
                var conexion = new Conexion_LIBERACIONES();
                conexion.ActualizarLiberacionFichaCambiador(
                    tbOrden.Text, Convert.ToInt32(tbReferencia.Text), tbMaquina.Text, version,
                    Convert.ToInt32(tbMolde.Text), operario1numero, operario1horas,
                    Operario1Nivel.SelectedValue.ToString(), Operario1UltRevision.Text, Operario1Notas.Text,
                    operario2numero, operario2horas, Operario2Nivel.SelectedValue.ToString(),
                    Operario2UltRevision.Text, Operario2Notas.Text,
                    encargadonumero, encargadohoras, cambiadornumero, cambiadorhoras,
                    calidadnumero, calidadhoras, CambiadorLiberado, LiberacionCambiadorHora.Text,
                    ProduccionLiberado, LiberacionEncargadoHora.Text,
                    CalidadLiberado, LiberacionCalidadHora.Text, ResultadoLiberacion,
                    Convert.ToInt32(LiberacionCondicionada.SelectedValue), NotaLiberacionCondicionada.Text,
                    Reliberacion, NCEncargado, GP12Encargado, NCCalidad, GP12Calidad,
                    QUERYLiberarCalidadHoraORI, ResultadoLOTES, ResultadoPARAM,
                    // Materiales
                    MAT1REF.Text, MAT1NOM.Text, MAT1LOTE, MAT1TIEMP_double, MAT1TIEMPREAL_double, MAT1TEMP_double, MAT1TEMPREAL_double,
                    MAT2REF.Text, MAT2NOM.Text, MAT2LOTE, MAT2TIEMP_double, MAT2TIEMPREAL_double, MAT2TEMP_double, MAT2TEMPREAL_double,
                    MAT3REF.Text, MAT3NOM.Text, MAT3LOTE, MAT3TIEMP_double, MAT3TIEMPREAL_double, MAT3TEMP_double, MAT3TEMPREAL_double,
                    COMP1REF.Text, COMP1NOM.Text, COMP1LOTE, COMP2REF.Text, COMP2NOM.Text, COMP2LOTE, COMP3REF.Text, COMP3NOM.Text, COMP3LOTE,
                    COMP4REF.Text, COMP4NOM.Text, COMP4LOTE, COMP5REF.Text, COMP5NOM.Text, COMP5LOTE,
                    COMP6REF.Text, COMP6NOM.Text, COMP6LOTE, COMP7REF.Text, COMP7NOM.Text, COMP7LOTE,
                    // Auditoría
                    Q1E, TbQ1ENC.Text, Q2E, TbQ2ENC.Text, Q3E, TbQ3ENC.Text, Q4E, TbQ4ENC.Text, Q5E, TbQ5ENC.Text,
                    Q6E, Q6C, TbQ6ENC.Text, TbQ6CAL.Text, Q7E, Q7C, TbQ7ENC.Text, TbQ7CAL.Text,
                    Q8E, Q8C, TbQ8ENC.Text, TbQ8CAL.Text, Q9E, Q9C, TbQ9ENC.Text, TbQ9CAL.Text, Q10C, TbQ10CAL.Text,
                    QXFeedbackCambiador.Text, QXFeedbackProduccion.Text, QXFeedbackCalidad.Text
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en LiberarCalidadFunction: {ex.Message}");
            }
        }


        public void GuardaMaterial(object sender, EventArgs e)
        {
            try
            {
                int version = 0;
                var conexion = new Conexion_LIBERACIONES();

                // === Helpers ===
                double ParseDouble(string txt)
                    => double.TryParse(txt.Replace('.', ','), out var v) ? v : 0.0;

                string Lote(string a, string b) => $"{a}|{b}";

                // === Materiales ===
                double MAT1TIEMP_double = ParseDouble(MAT1TIEMP.Text);
                double MAT1TIEMPREAL_double = ParseDouble(MAT1TIEMPREAL.Text);
                double MAT1TEMP_double = ParseDouble(MAT1TEMP.Text);
                double MAT1TEMPREAL_double = ParseDouble(MAT1TEMPREAL.Text);

                double MAT2TIEMP_double = ParseDouble(MAT2TIEMP.Text);
                double MAT2TIEMPREAL_double = ParseDouble(MAT2TIEMPREAL.Text);
                double MAT2TEMP_double = ParseDouble(MAT2TEMP.Text);
                double MAT2TEMPREAL_double = ParseDouble(MAT2TEMPREAL.Text);

                double MAT3TIEMP_double = ParseDouble(MAT3TIEMP.Text);
                double MAT3TIEMPREAL_double = ParseDouble(MAT3TIEMPREAL.Text);
                double MAT3TEMP_double = ParseDouble(MAT3TEMP.Text);
                double MAT3TEMPREAL_double = ParseDouble(MAT3TEMPREAL.Text);

                // === Concatenar lotes ===
                string MAT1LOTE = Lote(MAT1LOT.Text, MAT1LOT2.Text);
                string MAT2LOTE = Lote(MAT2LOT.Text, MAT2LOT2.Text);
                string MAT3LOTE = Lote(MAT3LOT.Text, MAT3LOT2.Text);
                string COMP1LOTE = Lote(COMP1LOT.Text, COMP1LOT2.Text);
                string COMP2LOTE = Lote(COMP2LOT.Text, COMP2LOT2.Text);
                string COMP3LOTE = Lote(COMP3LOT.Text, COMP3LOT2.Text);
                string COMP4LOTE = Lote(COMP4LOT.Text, COMP4LOT2.Text);
                string COMP5LOTE = Lote(COMP5LOT.Text, COMP5LOT2.Text);
                string COMP6LOTE = Lote(COMP6LOT.Text, COMP6LOT2.Text);
                string COMP7LOTE = Lote(COMP7LOT.Text, COMP7LOT2.Text);

                // === Guardar en base de datos ===
                conexion.Actualizar_ficha_Materiales(
                    tbOrden.Text, Convert.ToInt32(tbReferencia.Text), tbMaquina.Text, version,
                    // Material 1
                    MAT1REF.Text, MAT1NOM.Text, MAT1LOTE, MAT1TIEMP_double, MAT1TIEMPREAL_double, MAT1TEMP_double, MAT1TEMPREAL_double,
                    // Material 2
                    MAT2REF.Text, MAT2NOM.Text, MAT2LOTE, MAT2TIEMP_double, MAT2TIEMPREAL_double, MAT2TEMP_double, MAT2TEMPREAL_double,
                    // Material 3
                    MAT3REF.Text, MAT3NOM.Text, MAT3LOTE, MAT3TIEMP_double, MAT3TIEMPREAL_double, MAT3TEMP_double, MAT3TEMPREAL_double,
                    // Componentes
                    COMP1REF.Text, COMP1NOM.Text, COMP1LOTE,
                    COMP2REF.Text, COMP2NOM.Text, COMP2LOTE,
                    COMP3REF.Text, COMP3NOM.Text, COMP3LOTE,
                    COMP4REF.Text, COMP4NOM.Text, COMP4LOTE,
                    COMP5REF.Text, COMP5NOM.Text, COMP5LOTE,
                    COMP6REF.Text, COMP6NOM.Text, COMP6LOTE,
                    COMP7REF.Text, COMP7NOM.Text, COMP7LOTE
                );

                // === Recargar vistas ===
                CargarFichaLiberacion();
                CargarParametrosLiberacion();
                CargarCuestionariosLiberacion();
                CargarMateriasPrimasLiberacion();
                CargarResultados();
                lkb_Sort_Click("CAMBIO");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GuardaMaterial: {ex.Message}");
            }
        }


        //OPCIONES DE RECARGA DE DATOS ACTUALIZADOS

        public void RecargayLimpiaParametros(object sender, EventArgs e)
        {
            try
            {
                // === Helper para limpiar cualquier TextBox ===
                void Limpiar(params System.Web.UI.WebControls.TextBox[] cajas)
                {
                    foreach (var txt in cajas)
                    {
                        if (txt == null) continue;
                        txt.Text = "";
                        txt.BackColor = System.Drawing.Color.Empty;
                        txt.ForeColor = System.Drawing.Color.Black;
                    }
                }

                // === CILINDRO ===
                Limpiar(thBoqREAL, thT1REAL, thT2REAL, thT3REAL, thT4REAL, thT5REAL, thT6REAL, thT7REAL, thT8REAL, thT9REAL, thT10REAL);

                // === CÁMARA CALIENTE ===
                Limpiar(thZ1REAL, thZ2REAL, thZ3REAL, thZ4REAL, thZ5REAL, thZ6REAL, thZ7REAL, thZ8REAL, thZ9REAL, thZ10REAL,
                        thZ11REAL, thZ12REAL, thZ13REAL, thZ14REAL, thZ15REAL, thZ16REAL, thZ17REAL, thZ18REAL, thZ19REAL, thZ20REAL);

                // === POSTPRESIÓN (presiones y tiempos) ===
                Limpiar(thP1R, thP2R, thP3R, thP4R, thP5R, thP6R, thP7R, thP8R, thP9R, thP10R,
                        thTP1R, thTP2R, thTP3R, thTP4R, thTP5R, thTP6R, thTP7R, thTP8R, thTP9R, thTP10R,
                        tbConmutacionREAL, tbTiempoPresionREAL);

                // === ATEMPERADO ===
                Limpiar(TbCaudalF1REAL, TbCaudalF2REAL, TbCaudalF3REAL, TbCaudalF4REAL, TbCaudalF5REAL, TbCaudalF6REAL,
                        TbCaudalM1REAL, TbCaudalM2REAL, TbCaudalM3REAL, TbCaudalM4REAL, TbCaudalM5REAL, TbCaudalM6REAL,
                        TbTemperaturaF1REAL, TbTemperaturaF2REAL, TbTemperaturaF3REAL, TbTemperaturaF4REAL, TbTemperaturaF5REAL, TbTemperaturaF6REAL,
                        TbTemperaturaM1REAL, TbTemperaturaM2REAL, TbTemperaturaM3REAL, TbTemperaturaM4REAL, TbTemperaturaM5REAL, TbTemperaturaM6REAL);

                // === TOLERANCIAS ===
                Limpiar(tbTiempoInyeccionREAL, tbLimitePresionREAL, thVCargaREAL, thCargaREAL, thDescompREAL,
                        thContraprREAL, thTiempoREAL, thEnfriamientoREAL, thCicloREAL, thCojinREAL);

                // === Recargar datos actualizados ===
                CargarParametros();
                lkb_Sort_Click("PARAMETROS");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en RecargayLimpiaParametros: {ex.Message}");
            }
        }




















        // ==== HELPERS ====

        private Conexion_LIBERACIONES NuevaConexion() => new Conexion_LIBERACIONES();

        private void CargarOperario(
            Func<string, DataSet> metodoLogueado,
            TextBox txtNumero,
            TextBox txtNombre,
            TextBox txtHoras)
        {
            var conexion = NuevaConexion();
            var data = metodoLogueado(tbMaquina.Text);
            if (data.Tables[0].Rows.Count == 0) return;

            txtNumero.Text = data.Tables[0].Rows[0]["C_CLOCKNO"].ToString();
            txtNombre.Text = data.Tables[0].Rows[0]["C_NAME"].ToString();

            var horas = conexion.devuelve_horasXReferenciaXOperario(Convert.ToInt32(tbMolde.Text), Convert.ToInt32(txtNumero.Text));
            txtHoras.Text = horas.Tables[0].Rows.Count > 0
                ? horas.Tables[0].Rows[0]["TIEMPOHORAS"].ToString()
                : "0";

            lkb_Sort_Click("PORTADA");
        }

        // ==== MÉTODOS DE RECARGA ====

        public void ReCargarCambiador(object sender, EventArgs e)
        {
            try
            {
                CargarOperario(
                    (maq) => NuevaConexion().devuelve_cambiador_logueadoXMaquina(maq),
                    CambiadorNumero, CambiadorNombre, CambiadorHoras
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ReCargarCambiador: {ex.Message}");
            }
        }

        public void ReCargarCalidad(object sender, EventArgs e)
        {
            try
            {
                CargarOperario(
                    (maq) => NuevaConexion().devuelve_calidadplanta_logueadoXMaquina(maq),
                    CalidadNumero, CalidadNombre, CalidadHoras
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ReCargarCalidad: {ex.Message}");
            }
        }

        public void ReCargarEncargado(object sender, EventArgs e)
        {
            try
            {
                CargarOperario(
                    (maq) => NuevaConexion().devuelve_encargado_logueadoXMaquina(maq),
                    EncargadoNumero, EncargadoNombre, EncargadoHoras
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ReCargarEncargado: {ex.Message}");
            }
        }

        public void ReCargarOperarios(object sender, EventArgs e)
        {
            try
            {
                alertaoperario.Visible = false;

                var ocultar = new Control[] {
            Operario2Posicion, Operario2Nivel, Operario2Horas, Operario2Nombre,
            Operario2UltRevision, Operario2Numero, Operario2Notas
        };
                foreach (var c in ocultar) c.Visible = false;

                Operario2Numero.Text = Operario2Nombre.Text = Operario2Horas.Text = "";

                var conexion = NuevaConexion();
                var operarios = conexion.devuelve_operario_logueadoXMaquina(tbMaquina.Text);
                if (operarios.Tables[0].Rows.Count == 0) return;

                // Operario principal
                Operario1Numero.Text = operarios.Tables[0].Rows[0]["C_CLOCKNO"].ToString();
                Operario1Nombre.Text = operarios.Tables[0].Rows[0]["C_NAME"].ToString();

                // Segundo operario si existe
                if (operarios.Tables[0].Rows.Count > 1)
                {
                    Operario2Numero.Text = operarios.Tables[0].Rows[1]["C_CLOCKNO"].ToString();
                    Operario2Nombre.Text = operarios.Tables[0].Rows[1]["C_NAME"].ToString();
                    foreach (var c in ocultar) c.Visible = true;
                }

                // Calcular horas y nivel de operario 1
                var horas1 = conexion.devuelve_horasXReferenciaXOperario(Convert.ToInt32(tbMolde.Text), Convert.ToInt32(Operario1Numero.Text));
                if (horas1.Tables[0].Rows.Count > 0)
                {
                    double h = Convert.ToDouble(horas1.Tables[0].Rows[0]["TIEMPOHORAS"]);
                    Operario1Horas.Text = h.ToString();
                    Operario1Nivel.SelectedValue = h < 10 ? "I" : (h < 80 ? "L" : "U");
                    alertaoperario.Visible = h < 10;
                }
                else
                {
                    Operario1Horas.Text = "0";
                    alertaoperario.Visible = true;
                }

                // Calcular horas y nivel de operario 2 si visible
                if (Operario2Posicion.Visible)
                {
                    var horas2 = conexion.devuelve_horasXReferenciaXOperario(Convert.ToInt32(tbMolde.Text), Convert.ToInt32(Operario2Numero.Text));
                    if (horas2.Tables[0].Rows.Count > 0)
                    {
                        double h2 = Convert.ToDouble(horas2.Tables[0].Rows[0]["TIEMPOHORAS"]);
                        Operario2Horas.Text = h2.ToString();
                        Operario2Nivel.SelectedValue = h2 < 10 ? "I" : (h2 < 80 ? "L" : "U");
                        alertaoperario.Visible |= h2 < 10;
                    }
                    else
                    {
                        Operario2Horas.Text = "0";
                    }
                }

                lkb_Sort_Click("PORTADA");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ReCargarOperarios: {ex.Message}");
            }
        }

        // ==== REDIRECCIONES ====

        private void AbrirVentana(string url) =>
            Response.Write($"<script>window.open('{url}','window','HEIGHT=600,WIDTH=820,top=50,left=50,toolbar=yes,scrollbars=yes,resizable=yes');</script>");

        private void ProcesarBoton(HtmlButton button, string tipo)
        {
            button.Visible = false;
            string tab = tipo == "CALIDAD" ? "CALIDAD" : "PROCESO";

            LiberarCambiadorFunction("");
            CargarFichaLiberacion();
            CargarParametrosLiberacion();
            CargarCuestionariosLiberacion();
            CargarMateriasPrimasLiberacion();
            PintarCeldasDevacion();
            CargarResultados();
            lkb_Sort_Click(tab);
        }

        public void redireccionadetalleGP12(object sender, EventArgs e)
        {
            try
            {
                var button = (HtmlButton)sender;
                string url = $"http://facts4-srv/thermogestion/DOCUMENTAL/DocumentosLiberacionGP12.aspx?REFERENCIA={tbReferencia.Text}";

                switch (button.ClientID)
                {
                    case "A3": A3OK.Visible = true; ProcesarBoton(button, "PROCESO"); break;
                    case "A5": A5OK.Visible = true; ProcesarBoton(button, "PROCESO"); break;
                    case "A7": A7OK.Visible = true; ProcesarBoton(button, "CALIDAD"); break;
                    case "A8": A8OK.Visible = true; ProcesarBoton(button, "CALIDAD"); break;
                }

                AbrirVentana(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en redireccionadetalleGP12: {ex.Message}");
            }
        }

        public void redireccionadetalleNC(object sender, EventArgs e)
        {
            try
            {
                var button = (HtmlButton)sender;
                string url = $"http://facts4-srv/thermogestion/calidad/Alertas_Calidad_LIBERACION.aspx?Molde={tbMolde.Text}";

                switch (button.ClientID)
                {
                    case "A3": A3OK.Visible = true; ProcesarBoton(button, "PROCESO"); break;
                    case "A5": A5OK.Visible = true; ProcesarBoton(button, "PROCESO"); break;
                    case "A7": A7OK.Visible = true; ProcesarBoton(button, "CALIDAD"); break;
                    case "A8": A8OK.Visible = true; ProcesarBoton(button, "CALIDAD"); break;
                }

                AbrirVentana(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en redireccionadetalleNC: {ex.Message}");
            }
        }

        public void redireccionaGRAL(object sender, EventArgs e)
        {
            try
            {
                var button = (HtmlButton)sender;
                string id = button.ClientID;
                string tab = "";
                string url = "";

                switch (id)
                {
                    case "BotonAbrirParte":
                        url = $"http://facts4-srv/thermogestion/MANTENIMIENTO/ReparacionMoldes.aspx?PARTE={tbParteMolde.Text}";
                        tab = "CAMBIO"; break;
                    case "BotonCrearParte":
                        url = "http://facts4-srv/thermogestion/MANTENIMIENTO/ReparacionMoldes.aspx";
                        tab = "CAMBIO"; break;
                    case "BotonAbrirParteMaq":
                        url = $"../MANTENIMIENTO/ReparacionMaquinas.aspx?PARTE={tbParteMaq.Text}";
                        tab = "PARAMETROS"; break;
                    case "BotonCrearParteMaq":
                        url = "../MANTENIMIENTO/ReparacionMaquinas.aspx";
                        tab = "PARAMETROS"; break;
                    case "A3OK":
                        url = $"http://facts4-srv/thermogestion/calidad/Alertas_Calidad_LIBERACION.aspx?Molde={tbMolde.Text}";
                        tab = "PROCESO"; break;
                    case "A5OK":
                        url = $"http://facts4-srv/thermogestion/DOCUMENTAL/DocumentosLiberacionGP12.aspx?REFERENCIA={tbReferencia.Text}";
                        tab = "PROCESO"; break;
                    case "A7OK":
                        url = $"http://facts4-srv/thermogestion/calidad/Alertas_Calidad_LIBERACION.aspx?Molde={tbMolde.Text}";
                        tab = "CALIDAD"; break;
                    case "A8OK":
                        url = $"http://facts4-srv/thermogestion/DOCUMENTAL/DocumentosLiberacionGP12.aspx?REFERENCIA={tbReferencia.Text}";
                        tab = "CALIDAD"; break;
                }

                if (!string.IsNullOrEmpty(url)) AbrirVentana(url);
                lkb_Sort_Click(tab);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en redireccionaGRAL: {ex.Message}");
            }
        }

        public void RedireccionaGridView(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string tab = "";
                string url = "";

                if (e.CommandName == "RedirectMOLDE")
                {
                    url = $"http://facts4-srv/thermogestion/MANTENIMIENTO/ReparacionMoldes.aspx?PARTE={e.CommandArgument}";
                    tab = "CAMBIO";
                }
                else if (e.CommandName == "RedirectMAQ")
                {
                    url = $"../MANTENIMIENTO/ReparacionMaquinas.aspx?PARTE={e.CommandArgument}";
                    tab = "PARAMETROS";
                }

                if (!string.IsNullOrEmpty(url))
                    Response.Write($"<script>window.open('{url}','_blank');</script>");

                lkb_Sort_Click(tab);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en RedireccionaGridView: {ex.Message}");
            }
        }

        // ==== GESTIÓN DE TABS ====

        protected void lkb_Sort_Click(string e)
        {
            ManageTabsPostBack(PORTADA, CAMBIO, PARAMETROS, PROCESO, CALIDAD,
                               tab0button, tab1button, tab2button, tab3button, tab4button, e);
        }

        private void ManageTabsPostBack(HtmlGenericControl PORTADA, HtmlGenericControl CAMBIO, HtmlGenericControl PARAMETROS,
                                        HtmlGenericControl PROCESO, HtmlGenericControl CALIDAD,
                                        HtmlGenericControl tab0button, HtmlGenericControl tab1button, HtmlGenericControl tab2button,
                                        HtmlGenericControl tab3button, HtmlGenericControl tab4button, string grid)
        {
            var tabs = new (HtmlGenericControl pane, HtmlGenericControl button, string id)[]
            {
        (PORTADA, tab0button, "PORTADA"),
        (CAMBIO, tab1button, "CAMBIO"),
        (PARAMETROS, tab2button, "PARAMETROS"),
        (PROCESO, tab3button, "PROCESO"),
        (CALIDAD, tab4button, "CALIDAD")
            };

            foreach (var (pane, btn, _) in tabs)
            {
                btn.Attributes["class"] = "";
                pane.Attributes["class"] = "tab-pane";
            }

            var active = tabs.FirstOrDefault(t => t.id == grid);
            if (active.pane != null)
            {
                active.button.Attributes["class"] = "active";
                active.pane.Attributes["class"] = "tab-pane active";
            }
        }

        // ==== ENVÍO DE EMAILS ====

        public void mandar_mail(string orden, string producto, string parametros, string retenido)
        {
            try
            {
                var email = new MailMessage
                {
                    From = new MailAddress("calidadplanta@thermolympic.es"),
                    Subject = "Desviación crítica en liberación " + orden + " del producto " + producto + ".",
                    Body = "Se han detectado las siguientes desviaciones críticas durante el proceso de liberación:" +
                           parametros + retenido +
                           "<br><a href='http://facts4-srv/thermogestion/LIBERACIONES/LiberacionSerieNew.aspx?ORDEN=" + orden + "'>Consulta las desviaciones aquí.</a>",
                    IsBodyHtml = true,
                    Priority = MailPriority.Normal
                };

                string[] destinatarios = {
            "ruben@thermolympic.es",
            "j.murillo@thermolympic.es",
            "paco@thermolympic.es",
            "jorge@thermolympic.es"
        };

                foreach (var correo in destinatarios)
                    email.To.Add(correo);

                email.Bcc.Add("pedro@thermolympic.es");

                using (var smtp = new SmtpClient("smtp.thermolympic.es", 25))
                {
                    smtp.EnableSsl = false;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("calidadplanta@thermolympic.es", "010477Cp");
                    smtp.Send(email);
                }

                email.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar correo: " + ex.Message);
            }
        }


    }

}