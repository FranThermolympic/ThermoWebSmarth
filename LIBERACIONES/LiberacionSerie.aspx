<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="LiberacionSerie.aspx.cs"
    Inherits="ThermoWeb.LIBERACIONES.LiberacionSerie" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <title>Liberación de serie</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <%-- <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css" integrity="sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh" crossorigin="anonymous"> --%>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js" integrity="sha384-ZMP7rVo3mIykV+2+9J3UJ46jBk0WLaUAdn689aCwoqbBJiSnjAK/l8WvCWPIPm49" crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <%-- <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js" integrity="sha384-wfSDF2E50Y2D1uUdj0O3uMBJnjuUD4Ih7YwaYd1iqfktj0Uod8GCExl3Og8ifwB6" crossorigin="anonymous"></script> --%>
    <link href="https://cdn.jsdelivr.net/css-toggle-switch/latest/toggle-switch.css" rel="stylesheet" />
    <script src="js/json2.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
        <script>
            $(document).ready(function () {
                $('[data-toggle="popover"]').popover();
            });
        </script>
        <script>
            $(document).ready(function () {
                $('[data-toggle="tooltip"]').tooltip();
            });
        </script>
        <script type="text/javascript">
            function liberar_encargado_NOK() {
                alert("¡Atención, aún no has rellenado el área de máquina y parámetros! Completa los datos faltantes y vuelve a intentarlo.");
            }
        </script>
        <nav class="navbar navbar-inverse">
            <div class="container-fluid">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <a class="navbar-brand" href="../index.aspx">Thermolympic S.L.</a>
                </div>
                <div class="collapse navbar-collapse" id="myNavbar">
                    <ul class="nav navbar-nav">
                        <li><a href="EstadoLiberacion.aspx">Ver máquinas y liberaciones</a></li>
                        <li><a href="HistoricoLiberacion.aspx">Consultar histórico</a></li>
                    </ul>
                    <ul class="nav navbar-nav navbar-right">
                        <li><a href="#"><span class="glyphicon glyphicon-question-sign">AYUDA</span></a></li>
                    </ul>
                </div>
            </div>
        </nav>
        <div class="container">
            <h2>Liberación de serie</h2>
            <div class="row">
                <div class="col-lg-12">
                    <div class="table-responsive">
                        <table style="table-layout: fixed">
                            <tr>
                                <th style="width: 10%">
                                    <asp:TextBox ID="tbOrdenTitulo" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange">Orden:</asp:TextBox>
                                </th>
                                <td style="width: 10%">
                                    <asp:TextBox ID="tbOrden" runat="server" Style="text-align: center; width: 100%" Enabled="false"></asp:TextBox>
                                </td>
                                <th style="width: 10%">
                                    <asp:TextBox ID="tbReferenciaTitulo" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange">Referencia:</asp:TextBox>
                                </th>
                                <td style="width: 15%">
                                    <asp:TextBox ID="tbReferencia" runat="server" Style="text-align: center; width: 100%" Enabled="false"></asp:TextBox>
                                </td>
                                <td style="width: 40%">
                                    <asp:TextBox ID="tbNombre" runat="server" Style="text-align: center; width: 100%" Enabled="false"></asp:TextBox>
                                </td>
                                <th style="width: 10%">
                                    <asp:TextBox ID="tbMaquinaTitulo" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange">Máquina:</asp:TextBox>
                                </th>
                                <td style="width: 5%">
                                    <asp:TextBox ID="tbMaquina" runat="server" Style="text-align: center; width: 100%" Enabled="false"></asp:TextBox>
                                </td>
                                <td style="width: 5%">
                                    <asp:TextBox ID="tbMolde" runat="server" Style="text-align: center; width: 100%" Enabled="false" Visible="false"></asp:TextBox>
                                </td>
                                <td style="width: 5%">
                                    <asp:TextBox ID="tbFechaCambio" runat="server" Style="text-align: center; width: 100%" Enabled="false" Visible="false"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="table-responsive">
                        <table style="table-layout: fixed">
                            <tr>
                                <th style="width: 10%">
                                    <asp:TextBox ID="tbOrdenTitulo2" runat="server" Style="text-align: center; width: 100%" Enabled="false" Visible="false" BackColor="Orange">Orden 2:</asp:TextBox>
                                </th>
                                <td style="width: 10%">
                                    <asp:TextBox ID="tbOrden2" runat="server" Style="text-align: center; width: 100%" Visible="false" Enabled="false"></asp:TextBox>
                                </td>
                                <th style="width: 10%">
                                    <asp:TextBox ID="tbReferenciaTitulo2" runat="server" Style="text-align: center; width: 100%" Visible="false" Enabled="false" BackColor="Orange">Referencia:</asp:TextBox>
                                </th>
                                <td style="width: 15%">
                                    <asp:TextBox ID="tbReferencia2" runat="server" Style="text-align: center; width: 100%" Visible="false" Enabled="false"></asp:TextBox>
                                </td>
                                <td style="width: 40%">
                                    <asp:TextBox ID="tbNombre2" runat="server" Style="text-align: center; width: 100%" Visible="false" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th style="width: 10%">
                                    <asp:TextBox ID="tbOrdenTitulo3" runat="server" Style="text-align: center; width: 100%" Visible="false" Enabled="false" BackColor="Orange">Orden 3:</asp:TextBox>
                                </th>
                                <td style="width: 10%">
                                    <asp:TextBox ID="tbOrden3" runat="server" Style="text-align: center; width: 100%" Visible="false" Enabled="false"></asp:TextBox>
                                </td>
                                <th style="width: 10%">
                                    <asp:TextBox ID="tbReferenciaTitulo3" runat="server" Style="text-align: center; width: 100%" Visible="false" Enabled="false" BackColor="Orange">Referencia:</asp:TextBox>
                                </th>
                                <td style="width: 15%">
                                    <asp:TextBox ID="tbReferencia3" runat="server" Style="text-align: center; width: 100%" Visible="false" Enabled="false"></asp:TextBox>
                                </td>
                                <td style="width: 40%">
                                    <asp:TextBox ID="tbNombre3" runat="server" Style="text-align: center; width: 100%" Visible="false" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th style="width: 10%">
                                    <asp:TextBox ID="tbOrdenTitulo4" runat="server" Style="text-align: center; width: 100%" Enabled="false" Visible="false" BackColor="Orange">Orden 4:</asp:TextBox>
                                </th>
                                <td style="width: 10%">
                                    <asp:TextBox ID="tbOrden4" runat="server" Style="text-align: center; width: 100%" Visible="false" Enabled="false"></asp:TextBox>
                                </td>
                                <th style="width: 10%">
                                    <asp:TextBox ID="tbReferenciaTitulo4" runat="server" Style="text-align: center; width: 100%" Visible="false" Enabled="false" BackColor="Orange">Referencia:</asp:TextBox>
                                </th>
                                <td style="width: 15%">
                                    <asp:TextBox ID="tbReferencia4" runat="server" Style="text-align: center; width: 100%" Visible="false" Enabled="false"></asp:TextBox>
                                </td>
                                <td style="width: 40%">
                                    <asp:TextBox ID="tbNombre4" runat="server" Style="text-align: center; width: 100%" Visible="false" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="progress">
                        <div id="estadocambiadorSN" class="progress-bar progress-bar-danger" runat="server" role="progressbar" style="width: 15%">
                            Cambiador (Sin Liberar)
                        </div>
                        <div id="estadocambiadorCOND" class="progress-bar progress-bar-warning" runat="server" role="progressbar" visible="false" style="width: 33%">
                            Cambiador (Liberado OK condicional)
                        </div>
                        <div id="estadocambiadorLIBOK" class="progress-bar progress-bar-success" runat="server" role="progressbar" visible="false" style="width: 33%">
                            Cambiador (Liberado OK)
                        </div>
                        <div id="estadoencargadoSN" class="progress-bar progress-bar-danger" runat="server" role="progressbar" style="width: 15%">
                            Encargado (Sin Liberar)
                        </div>
                        <div id="estadoencargadoCOND" class="progress-bar progress-bar-warning" runat="server" role="progressbar" visible="false" style="width: 33%">
                            Encargado (Liberado OK condicional)
                        </div>
                        <div id="estadoencargadoLIBOK" class="progress-bar progress-bar-success" runat="server" role="progressbar" visible="false" style="width: 33%">
                            Encargado (Liberado OK)
                        </div>
                        <div id="estadocalidadSN" class="progress-bar progress-bar-danger" runat="server" role="progressbar" style="width: 15%">
                            Calidad (Sin Liberar)
                        </div>
                        <div id="estadocalidadCOND" class="progress-bar progress-bar-warning" runat="server" role="progressbar" visible="false" style="width: 34%">
                            Calidad (Liberado OK condicional)
                        </div>
                        <div id="estadocalidadLIBOK" class="progress-bar progress-bar-success" runat="server" role="progressbar" visible="false" style="width: 34%">
                            Calidad (Liberado OK)
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div id="alertaoperario" runat="server" visible="false" class="alert alert-danger">
                    <strong>¡Atención!</strong> Los operarios trabajando en máquina no cuentan con suficiente experiencia. Bloquea la producción para revisión GP12 o asigna otro operario para la supervisión.
                </div>
                <div id="alertafichafabricacion" runat="server" visible="false" class="alert alert-danger alert-dismissible">
                    <strong>¡Atención!</strong> No existe una ficha de fabricación digital asignada a éste producto/molde. Créala desde <strong><a href="http://facts4-srv/thermogestion/FichasParametros_nuevo.aspx">la aplicación de gestión Thermolympic.</a></strong>
                </div>
            </div>

            <br />
            <div class="row">
                <div class="col-lg-12">

                    <%--Defino tabs --%>
                    <ul class="nav nav-pills nav-justified">
                        <li class="active" id="tab0button" runat="server"><a data-toggle="pill" href="#PORTADA">Portada</a></li>
                        <li id="tab1button" runat="server"><a data-toggle="pill" href="#CAMBIO">Materiales y cambio de molde</a></li>
                        <li id="tab2button" runat="server"><a data-toggle="pill" href="#PARAMETROS">Máquina y parámetros</a></li>
                        <li id="tab3button" runat="server"><a data-toggle="pill" href="#PROCESO">Proceso</a></li>
                        <li id="tab4button" runat="server"><a data-toggle="pill" href="#CALIDAD">Auditoría de calidad</a></li>
                    </ul>
                    <div class="tab-content">
                        <%-- Abro panel de cambio--%>
                        <div id="PORTADA" class="tab-pane fade in active" runat="server">

                            <div class="row">
                                <div class="col-lg-12">
                                    <h2>Personal implicado y formación&nbsp<a href="#" title="Consulta el tablero de polivalencia:" data-toggle="popover" data-placement="top" data-content="Recuerda que el personal asignado a la fabricación debe estar presente y validado en el tablero de polivalencia del producto."><span class="glyphicon glyphicon-question-sign"></span></a></h2>
                                    <div class="table-responsive">
                                        <table style="table-layout: fixed; width: 100%">
                                            <tr>
                                                <th style="width: 4%">
                                                    <asp:TextBox ID="TextBox2" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange"></asp:TextBox>
                                                </th>
                                                <th style="width: 15%">
                                                    <asp:TextBox ID="TituloPosicion" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange">Posición</asp:TextBox>
                                                </th>
                                                <th style="width: 5%">
                                                    <asp:TextBox ID="TituloNumOpe" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange">Nº</asp:TextBox>
                                                </th>
                                                <th style="width: 15%">
                                                    <asp:TextBox ID="TituloNomOpe" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange">Nombre</asp:TextBox>
                                                </th>
                                                <th style="width: 10%">
                                                    <asp:TextBox ID="TituloHorasAcumuladas" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange">Horas acum.</asp:TextBox>
                                                </th>
                                                <th style="width: 10%">
                                                    <asp:TextBox ID="TituloNivel" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange">Nivel</asp:TextBox>
                                                </th>
                                                <th style="width: 15%">
                                                    <asp:TextBox ID="TituloRevisionILUO" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange">Última revisión</asp:TextBox>
                                                </th>
                                                <th style="width: 30%">
                                                    <asp:TextBox ID="ILUONotas" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange">Notas</asp:TextBox>
                                                </th>
                                            </tr>
                                            <tr>
                                                <th style="width: 4%">
                                                    <a href="#" id="BtnRecargaOp1" class="btn btn-info" style="text-align: center; width: 100%" runat="server" onserverclick="ReCargarOperarios" data-toggle="tooltip" title="¡Recuerda que debe estar logueado en BMS!"><span class="glyphicon glyphicon-log-in"></span></a>
                                                </th>
                                                <th style="width: 15%">
                                                    <asp:TextBox ID="Operario1Posicion" runat="server" Style="text-align: center; width: 100%; height: 34px" Enabled="false">OPERARIO</asp:TextBox>
                                                </th>
                                                <td style="width: 5%">
                                                    <asp:TextBox ID="Operario1Numero" runat="server" Style="text-align: center; width: 100%; height: 34px" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 15%">
                                                    <asp:TextBox ID="Operario1Nombre" runat="server" Style="text-align: center; width: 100%; height: 34px" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 10%">
                                                    <asp:TextBox ID="Operario1Horas" runat="server" Style="text-align: center; width: 100%; height: 34px" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 10%">
                                                    <asp:DropDownList ID="Operario1Nivel" runat="server" CssClass="form-control" AutoPostBack="True">
                                                        <asp:ListItem Text="I" Value="I"></asp:ListItem>
                                                        <asp:ListItem Text="L" Value="L"></asp:ListItem>
                                                        <asp:ListItem Text="U" Value="U"></asp:ListItem>
                                                        <asp:ListItem Text="O" Value="O"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 15%">
                                                    <asp:TextBox ID="Operario1UltRevision" runat="server" Style="text-align: center; width: 100%; height: 34px" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 30%">
                                                    <asp:TextBox ID="Operario1Notas" runat="server" Style="text-align: center; width: 100%; height: 34px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th style="width: 4%">
                                                    <a href="#" id="btnRecargarOperario2" class="btn btn-info" runat="server" visible="false" style="text-align: center; width: 100%" onserverclick="ReCargarOperarios" data-toggle="tooltip" title="¡Recuerda que debe estar logueado en BMS!"><span class="glyphicon glyphicon-log-in"></span></a>
                                                </th>
                                                <th style="width: 15%">
                                                    <asp:TextBox ID="Operario2Posicion" runat="server" Style="text-align: center; width: 100%; height: 34px" Enabled="false" Visible="false">OPERARIO 2</asp:TextBox>
                                                </th>
                                                <td style="width: 5%">
                                                    <asp:TextBox ID="Operario2Numero" runat="server" Style="text-align: center; width: 100%; height: 34px" Enabled="false" Visible="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 15%">
                                                    <asp:TextBox ID="Operario2Nombre" runat="server" Style="text-align: center; width: 100%; height: 34px" Enabled="false" Visible="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 10%">
                                                    <asp:TextBox ID="Operario2Horas" runat="server" Style="text-align: center; width: 100%; height: 34px" Enabled="false" Visible="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 10%">
                                                    <asp:DropDownList ID="Operario2Nivel" runat="server" CssClass="form-control" AutoPostBack="True" Visible="false">
                                                        <asp:ListItem Text="I" Value="I"></asp:ListItem>
                                                        <asp:ListItem Text="L" Value="L"></asp:ListItem>
                                                        <asp:ListItem Text="U" Value="U"></asp:ListItem>
                                                        <asp:ListItem Text="O" Value="O"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 15%">
                                                    <asp:TextBox ID="Operario2UltRevision" runat="server" Style="text-align: center; width: 100%; height: 34px" Enabled="false" Visible="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 30%">
                                                    <asp:TextBox ID="Operario2Notas" runat="server" Style="text-align: center; width: 100%; height: 34px" Visible="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th style="width: 4%">
                                                    <a href="#" id="btnRecargarCambiador2" class="btn btn-info btn-sm" style="text-align: center; width: 100%" runat="server" onserverclick="ReCargarCambiador" data-toggle="tooltip" title="¡Recuerda que debe estar logueado en BMS!"><span class="glyphicon glyphicon-log-in"></span></a>
                                                </th>
                                                <th style="width: 15%">
                                                    <asp:TextBox ID="CambiadorPosicion" runat="server" Style="text-align: center; width: 100%" Enabled="false">CAMBIADOR</asp:TextBox>
                                                </th>
                                                <td style="width: 5%">
                                                    <asp:TextBox ID="CambiadorNumero" runat="server" Style="text-align: center; width: 100%" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 15%">
                                                    <asp:TextBox ID="CambiadorNombre" runat="server" Style="text-align: center; width: 100%" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 10%">
                                                    <asp:TextBox ID="CambiadorHoras" runat="server" Style="text-align: center; width: 100%" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 10%">
                                                    <asp:TextBox ID="CambiadorNivel" runat="server" Style="text-align: center; width: 100%" Enabled="false" Visible="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 15%">
                                                    <asp:TextBox ID="CambiadorUltRevision" runat="server" Style="text-align: center; width: 100%" Enabled="false" Visible="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 30%">
                                                    <asp:TextBox ID="CambiadorNotas" runat="server" Style="text-align: center; width: 100%" Enabled="false" Visible="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th style="width: 4%">
                                                    <a href="#" id="btnRecargarEncargado" class="btn btn-info btn-sm" style="text-align: center; width: 100%" runat="server" onserverclick="ReCargarEncargado" data-toggle="tooltip" title="¡Recuerda que debe estar logueado en BMS!"><span class="glyphicon glyphicon-log-in"></span></a>
                                                </th>
                                                <th style="width: 15%">
                                                    <asp:TextBox ID="EncargadoPosicion" runat="server" Style="text-align: center; width: 100%" Enabled="false">ENCARGADO</asp:TextBox>
                                                </th>
                                                <td style="width: 5%">
                                                    <asp:TextBox ID="EncargadoNumero" runat="server" Style="text-align: center; width: 100%" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 15%">
                                                    <asp:TextBox ID="EncargadoNombre" runat="server" Style="text-align: center; width: 100%" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 10%">
                                                    <asp:TextBox ID="EncargadoHoras" runat="server" Style="text-align: center; width: 100%" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 10%">
                                                    <asp:TextBox ID="EncargadoNivel" runat="server" Style="text-align: center; width: 100%" Enabled="false" Visible="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 15%">
                                                    <asp:TextBox ID="EncargadoUltRevision" runat="server" Style="text-align: center; width: 100%" Enabled="false" Visible="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 30%">
                                                    <asp:TextBox ID="EncargadoNotas" runat="server" Style="text-align: center; width: 100%" Enabled="false" Visible="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th style="width: 4%">
                                                    <a href="#" id="btnRecargarCalidad" class="btn btn-info btn-sm" style="text-align: center; width: 100%" runat="server" onserverclick="ReCargarCalidad" data-toggle="tooltip" title="¡Recuerda que debe estar logueado en BMS!"><span class="glyphicon glyphicon-log-in"></span></a>
                                                </th>
                                                <th style="width: 15%">
                                                    <asp:TextBox ID="CalidadPosicion" runat="server" Style="text-align: center; width: 100%" Enabled="false">CALIDAD</asp:TextBox>
                                                </th>
                                                <td style="width: 5%">
                                                    <asp:TextBox ID="CalidadNumero" runat="server" Style="text-align: center; width: 100%" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 15%">
                                                    <asp:TextBox ID="CalidadNombre" runat="server" Style="text-align: center; width: 100%" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 10%">
                                                    <asp:TextBox ID="CalidadHoras" runat="server" Style="text-align: center; width: 100%" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 10%">
                                                    <asp:TextBox ID="CalidadNivel" runat="server" Style="text-align: center; width: 100%" Enabled="false" Visible="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 15%">
                                                    <asp:TextBox ID="CalidadUltRevision" runat="server" Style="text-align: center; width: 100%" Enabled="false" Visible="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 30%">
                                                    <asp:TextBox ID="CalidadNotas" runat="server" Style="text-align: center; width: 100%" Enabled="false" Visible="false"></asp:TextBox>
                                                </td>
                                            </tr>

                                        </table>
                                    </div>
                                </div>
                            </div>
                            <br />

                            <div class="row">

                                <div class="col-lg-12">
                                    <h2>Resultados</h2>
                                    <div class="table-responsive">
                                        <table style="table-layout: fixed; width: 100%">
                                            <tr>
                                                <th style="width: 20%">
                                                    <asp:TextBox ID="TextBox22" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange">Estado</asp:TextBox>
                                                </th>
                                                <th style="width: 20%">
                                                    <asp:TextBox ID="TextBox23" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange">Proceso</asp:TextBox>
                                                </th>
                                                <th style="width: 15%">
                                                    <asp:TextBox ID="TextBox24" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange">Fecha de liberación</asp:TextBox>
                                                </th>
                                                <th style="width: 15%">
                                                    <asp:TextBox ID="TextBox25" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange">Fecha de modificación</asp:TextBox>
                                                </th>
                                                <th style="width: 30%">
                                                    <asp:TextBox ID="TextBox26" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange">Notas</asp:TextBox>
                                                </th>
                                            </tr>
                                            <tr>
                                                <th style="width: 20%">
                                                    <asp:TextBox ID="LiberacionCambiador" runat="server" Style="text-align: center; width: 100%" Enabled="false">Sin liberar</asp:TextBox>
                                                </th>
                                                <th style="width: 20%">
                                                    <asp:TextBox ID="LiberacionCambiadorDept" runat="server" Style="text-align: center; width: 100%" Enabled="false">Cambio de molde</asp:TextBox>
                                                </th>
                                                <th style="width: 15%">
                                                    <asp:TextBox ID="LiberacionCambiadorHoraORI" runat="server" Style="text-align: center; width: 100%" Enabled="false"></asp:TextBox>
                                                </th>
                                                <th style="width: 15%">
                                                    <asp:TextBox ID="LiberacionCambiadorHora" runat="server" Style="text-align: center; width: 100%" Enabled="false"></asp:TextBox>
                                                </th>
                                                <th style="width: 30%">
                                                    <asp:TextBox ID="LiberacionCambiadorNotas" runat="server" Style="text-align: center; width: 100%" Enabled="false"></asp:TextBox>
                                                </th>
                                            </tr>
                                            <tr>
                                                <th style="width: 20%">
                                                    <asp:TextBox ID="LiberacionEncargado" runat="server" Style="text-align: center; width: 100%" Enabled="false">Sin liberar</asp:TextBox>
                                                </th>
                                                <th style="width: 20%">
                                                    <asp:TextBox ID="LiberacionEncargadoDept" runat="server" Style="text-align: center; width: 100%" Enabled="false">Máquina, parámetros y proceso</asp:TextBox>
                                                </th>
                                                <th style="width: 15%">
                                                    <asp:TextBox ID="LiberacionEncargadoHoraORI" runat="server" Style="text-align: center; width: 100%" Enabled="false"></asp:TextBox>
                                                </th>
                                                <th style="width: 15%">
                                                    <asp:TextBox ID="LiberacionEncargadoHora" runat="server" Style="text-align: center; width: 100%" Enabled="false"></asp:TextBox>
                                                </th>
                                                <th style="width: 30%">
                                                    <asp:TextBox ID="LiberacionEncargadoNotas" runat="server" Style="text-align: center; width: 100%" Enabled="false"></asp:TextBox>
                                                </th>
                                            </tr>
                                            <tr>
                                                <th style="width: 25%">
                                                    <asp:TextBox ID="LiberacionCalidad" runat="server" Style="text-align: center; width: 100%" Enabled="false">Sin liberar</asp:TextBox>
                                                </th>
                                                <th style="width: 25%">
                                                    <asp:TextBox ID="LiberacionCalidadDept" runat="server" Style="text-align: center; width: 100%" Enabled="false">Auditoría de calidad</asp:TextBox>
                                                </th>
                                                <th style="width: 15%">
                                                    <asp:TextBox ID="LiberacionCalidadHoraORI" runat="server" Style="text-align: center; width: 100%" Enabled="false"></asp:TextBox>
                                                </th>
                                                <th style="width: 15%">
                                                    <asp:TextBox ID="LiberacionCalidadHora" runat="server" Style="text-align: center; width: 100%" Enabled="false"></asp:TextBox>
                                                </th>
                                                <th style="width: 30%">
                                                    <asp:TextBox ID="LiberacionCalidadNotas" runat="server" Style="text-align: center; width: 100%" Enabled="false"></asp:TextBox>
                                                </th>
                                            </tr>
                                        </table>
                                    </div>
                                    <label style="font:italic; font-weight:300; float:right">*Nota: En paradas superiores a 4 horas, se debe realizar una nueva auditoria Liberación de serie.</label>
                                    <br />


                                    <div id="condicionada" class="table-responsive" runat="server">
                                        <h4><strong>Liberación - Acciones en caso de desviación.</strong><a href="#" title="En caso de desviación:" data-toggle="popover" data-placement="top" data-content="Recuerda seleccionar una acción en el desplegable. Esto ayudará a trazar y escalar las acciones de contención posteriores.">&nbsp<span class="glyphicon glyphicon-question-sign"></span></a></h4>
                                        <table style="table-layout: fixed; width: 100%">
                                            <tr>
                                                <th>
                                                    <asp:DropDownList ID="LiberacionCondicionada" runat="server" CssClass="form-control" AutoPostBack="True">
                                                        <asp:ListItem Text="N/A" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Se retiene producción (Liberación NOK)" Value="4"></asp:ListItem>
                                                        <asp:ListItem Text="Con inspección 100%" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Con identificación unitaria" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="Otras actuaciones (Anotar abajo)" Value="5"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </th>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="NotaLiberacionCondicionada" runat="server" Style="text-align: center; width: 100%; height: 40px" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <br />
                                                    <asp:TextBox ID="lblValidadPORING" runat="server" Style="text-align: center; width: 100%; background-color: orange" Font-Bold="true" Enabled="false" Visible="false"> Desviación validada por:</asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="DropValidadJefeProyecto" runat="server" CssClass="form-control" Visible="false" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <button id="btnValidarING" runat="server" class="btn btn-info btn-sm" onserverclick="ValidarIngenieria" visible="false" style="width: 100%">Validar producción</button>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>


                                    <h2>Desviaciones encontradas</h2>
                                </div>

                                <div class="col-lg-12">
                                    <div id="SINDESVIACIONES" runat="server">
                                        <h5>Sin desviaciones</h5>
                                    </div>
                                    <div id="formaNC" runat="server" visible="false">
                                        <h4><strong>Formación y no conformidades</strong></h4>
                                    </div>
                                    <div class="table-responsive">
                                        <table style="table-layout: fixed; width: 100%">
                                            <tr>
                                                <td style="width: 50%">
                                                    <asp:TextBox ID="ResumenFORM1" runat="server" Style="width: 100%; border-color: transparent" Enabled="false" Visible="false">   ENCARGADO: Pendiente consultar no conformidades</asp:TextBox>
                                                </td>
                                                <td style="width: 50%">
                                                    <asp:TextBox ID="ResumenFORM2" runat="server" Style="width: 100%; border-color: transparent" Enabled="false" Visible="false">   ENCARGADO: Pendiente consultar detecciones Gp12</asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%">
                                                    <asp:TextBox ID="ResumenFORM3" runat="server" Style="width: 100%; border-color: transparent" Enabled="false" Visible="false">   CALIDAD: Pendiente consultar con operario no conformidades</asp:TextBox>
                                                </td>
                                                <td style="width: 50%">
                                                    <asp:TextBox ID="ResumenFORM4" runat="server" Style="width: 100%; border-color: transparent" Enabled="false" Visible="false">   CALIDAD: Pendiente consultar con operario detecciones Gp12</asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="width: 100%; border-color: transparent">
                                                    <asp:TextBox ID="ResumenFORM4ILUO" runat="server" Style="text-align: center; width: 100%; border-color: red" Enabled="false" Visible="false">Uno o más operarios asignados no cuentan con la formación suficiente.</asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="matparamNC" runat="server" visible="false">
                                        <br />
                                        <h4><strong>Materias primas y parámetros cargados</strong></h4>
                                    </div>
                                    <div class="table-responsive">
                                        <table style="table-layout: fixed; width: 100%">
                                            <tr>
                                                <td style="width: 50%">
                                                    <asp:TextBox ID="ResumenMAT1" runat="server" Style="width: 100%; border-color: transparent" Enabled="false" Visible="false">   MATERIALES: No se ha indicado el lote de algunas materias primas.</asp:TextBox>
                                                </td>
                                                <td style="width: 50%">
                                                    <asp:TextBox ID="ResumenMAT2" runat="server" Style="width: 100%; border-color: transparent" Enabled="false" Visible="false">   MATERIALES: No se ha indicado el tiempo de secado o la temperatura.</asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%">
                                                    <asp:TextBox ID="ResumenPROD1" runat="server" Style="width: 100%; border-color: transparent" Enabled="false" Visible="false">   PRODUCCIÓN: Existen parámetros fuera de especificación</asp:TextBox>
                                                </td>
                                                <td style="width: 50%">
                                                    <asp:TextBox ID="ResumenPROD2" runat="server" Style="width: 100%; border-color: transparent" Enabled="false" Visible="false">   No se han dado motivos</asp:TextBox>
                                                </td>
                                            </tr>
                                            <td colspan="2" style="width: 100%; border-color: transparent">
                                                <asp:TextBox ID="ResumenPROD3" runat="server" Style="text-align: center; width: 100%; border-color: red" Enabled="false" Visible="false">La ficha de producción no existe o no está digitalizada. (Limpiar y recargar ficha una vez creada)</asp:TextBox>
                                            </td>
                                            <tr>
                                            </tr>
                                        </table>
                                    </div>

                                    <div id="audinc" runat="server" visible="false">
                                        <br />
                                        <h4><strong>Auditoría de liberación</strong></h4>
                                    </div>
                                    <div class="table-responsive">
                                        <table style="table-layout: fixed; width: 100%">
                                            <tr>
                                                <th style="width: 25%">
                                                    <asp:TextBox ID="ResumenGRAL1" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange" Visible="false">Estándar no conforme</asp:TextBox>
                                                </th>
                                                <th style="width: 42%">
                                                    <asp:TextBox ID="ResumenGRAL2" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange" Visible="false">Comentarios producción</asp:TextBox>
                                                </th>
                                                <th style="width: 43%">
                                                    <asp:TextBox ID="ResumenGRAL3" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange" Visible="false">Comentarios calidad</asp:TextBox>
                                                </th>
                                            </tr>
                                            <tr>
                                                <th style="width: 25%">
                                                    <asp:TextBox ID="Resumen1" runat="server" Style="text-align: center; width: 100%; height: 45px; vertical-align: central" Enabled="false" Visible="false">1. Máquina y programas</asp:TextBox>
                                                </th>
                                                <td style="width: 42%">
                                                    <asp:TextBox ID="Resumen1ENC" runat="server" Style="text-align: center; width: 100%; height: 45px; vertical-align: central" Enabled="false" TextMode="MultiLine" Visible="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 43%">
                                                    <asp:TextBox ID="Resumen1CAL" runat="server" Style="text-align: center; width: 100%; height: 45px; vertical-align: central" Enabled="false" TextMode="MultiLine" Visible="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th style="width: 25%">
                                                    <asp:TextBox ID="Resumen2" runat="server" Style="text-align: center; width: 100%; height: 45px; vertical-align: central" Enabled="false" Visible="false">2. Conexiones de agua</asp:TextBox>
                                                </th>
                                                <td style="width: 42%">
                                                    <asp:TextBox ID="Resumen2ENC" runat="server" Style="text-align: center; width: 100%; height: 45px; vertical-align: central" Enabled="false" TextMode="MultiLine" Visible="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 43%">
                                                    <asp:TextBox ID="Resumen2CAL" runat="server" Style="text-align: center; width: 100%; height: 45px; vertical-align: central" Enabled="false" TextMode="MultiLine" Visible="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th style="width: 25%">
                                                    <asp:TextBox ID="Resumen3" runat="server" Style="text-align: center; width: 100%; height: 45px; vertical-align: central" Enabled="false" Visible="false">3. Periféricos y ajuste mecánico</asp:TextBox>
                                                </th>
                                                <td style="width: 42%">
                                                    <asp:TextBox ID="Resumen3ENC" runat="server" Style="text-align: center; width: 100%; height: 45px; vertical-align: central" Enabled="false" TextMode="MultiLine" Visible="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 43%">
                                                    <asp:TextBox ID="Resumen3CAL" runat="server" Style="text-align: center; width: 100%; height: 45px; vertical-align: central" Enabled="false" TextMode="MultiLine" Visible="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th style="width: 25%">
                                                    <asp:TextBox ID="Resumen4" runat="server" Style="text-align: center; width: 100%; height: 45px; vertical-align: central" Enabled="false" Visible="false">4. Condiciones iniciales</asp:TextBox>
                                                </th>
                                                <td style="width: 42%">
                                                    <asp:TextBox ID="Resumen4ENC" runat="server" Style="text-align: center; width: 100%" Enabled="false" TextMode="MultiLine" Visible="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 43%">
                                                    <asp:TextBox ID="Resumen4CAL" runat="server" Style="text-align: center; width: 100%" Enabled="false" TextMode="MultiLine" Visible="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th style="width: 25%">
                                                    <asp:TextBox ID="Resumen5" runat="server" Style="text-align: center; width: 100%; height: 45px; vertical-align: central" Enabled="false" Visible="false">5. Primeras inyectadas</asp:TextBox>
                                                </th>
                                                <td style="width: 42%">
                                                    <asp:TextBox ID="Resumen5ENC" runat="server" Style="text-align: center; width: 100%" Enabled="false" TextMode="MultiLine" Visible="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 43%">
                                                    <asp:TextBox ID="Resumen5CAL" runat="server" Style="text-align: center; width: 100%" Enabled="false" TextMode="MultiLine" Visible="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th style="width: 25%">
                                                    <asp:TextBox ID="Resumen6" runat="server" Style="text-align: center; width: 100%; height: 45px; vertical-align: central" Enabled="false" Visible="false">6. Pokayokes, galgas de control y máquinas periféricas</asp:TextBox>
                                                </th>
                                                <td style="width: 42%">
                                                    <asp:TextBox ID="Resumen6ENC" runat="server" Style="text-align: center; width: 100%" Enabled="false" TextMode="MultiLine" Visible="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 43%">
                                                    <asp:TextBox ID="Resumen6CAL" runat="server" Style="text-align: center; width: 100%" Enabled="false" TextMode="MultiLine" Visible="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th style="width: 25%">
                                                    <asp:TextBox ID="Resumen7" runat="server" Style="text-align: center; width: 100%; height: 45px; vertical-align: central" Enabled="false" Visible="false">7. Puesto de trabajo</asp:TextBox>
                                                </th>
                                                <td style="width: 42%">
                                                    <asp:TextBox ID="Resumen7ENC" runat="server" Style="text-align: center; width: 100%" Enabled="false" TextMode="MultiLine" Visible="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 43%">
                                                    <asp:TextBox ID="Resumen7CAL" runat="server" Style="text-align: center; width: 100%" Enabled="false" TextMode="MultiLine" Visible="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th style="width: 25%">
                                                    <asp:TextBox ID="Resumen8" runat="server" Style="text-align: center; width: 100%; height: 45px; vertical-align: central" Enabled="false" Visible="false">8. Anti mezclas</asp:TextBox>
                                                </th>
                                                <td style="width: 42%">
                                                    <asp:TextBox ID="Resumen8ENC" runat="server" Style="text-align: center; width: 100%" Enabled="false" TextMode="MultiLine" Visible="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 43%">
                                                    <asp:TextBox ID="Resumen8CAL" runat="server" Style="text-align: center; width: 100%" Enabled="false" TextMode="MultiLine" Visible="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th style="width: 25%">
                                                    <asp:TextBox ID="Resumen9" runat="server" Style="text-align: center; width: 100%; height: 45px; vertical-align: central" Enabled="false" Visible="false">9. Elementos auxiliares</asp:TextBox>
                                                </th>
                                                <td style="width: 42%">
                                                    <asp:TextBox ID="Resumen9ENC" runat="server" Style="text-align: center; width: 100%" Enabled="false" TextMode="MultiLine" Visible="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 43%">
                                                    <asp:TextBox ID="Resumen9CAL" runat="server" Style="text-align: center; width: 100%" Enabled="false" TextMode="MultiLine" Visible="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th style="width: 25%">
                                                    <asp:TextBox ID="Resumen10" runat="server" Style="text-align: center; width: 100%; height: 45px; vertical-align: central" Enabled="false" Visible="false">10. Control de atributos</asp:TextBox>
                                                </th>
                                                <td style="width: 42%">
                                                    <asp:TextBox ID="Resumen10ENC" runat="server" Style="text-align: center; width: 100%" Enabled="false" TextMode="MultiLine" Visible="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 43%">
                                                    <asp:TextBox ID="Resumen10CAL" runat="server" Style="text-align: center; width: 100%" Enabled="false" TextMode="MultiLine" Visible="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>

                                    <br />
                                    <br />
                                </div>

                            </div>

                        </div>
                        <%-- Abro panel de cambio--%>
                        <div id="CAMBIO" class="tab-pane fade" runat="server">
                            <div class="row">
                                <div class="col-lg-12">
                                    <h2>Materias primas y lotes</h2>
                                    <div class="table-responsive">
                                        <table style="table-layout: fixed; width: 100%">
                                            <tr>
                                                <th style="width: 10%">
                                                    <asp:TextBox ID="tituloEnstock" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange"></asp:TextBox>
                                                </th>
                                                <th style="width: 15%">
                                                    <asp:TextBox ID="tituloMATREF" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange">Referencia</asp:TextBox>
                                                </th>
                                                <th style="width: 25%">
                                                    <asp:TextBox ID="tituloMATNOM" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange">Nombre</asp:TextBox>
                                                </th>
                                                <th style="width: 9%">
                                                    <asp:TextBox ID="tituloMATLOT" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange">Lote</asp:TextBox>
                                                </th>
                                                <th style="width: 9%">
                                                    <asp:TextBox ID="tituloMATLOT2" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange">Lote 2</asp:TextBox>
                                                </th>
                                                <th style="width: 22%">
                                                    <asp:TextBox ID="tituloREMARK2" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange">Espec.</asp:TextBox>
                                                </th>
                                                <th style="width: 10%">
                                                    <asp:TextBox ID="tituloMATTEMPREAL" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange">TªSecadora:</asp:TextBox>
                                                </th>
                                                <th style="width: 10%">
                                                    <asp:TextBox ID="tituloMATTIEMPREAL" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange">Tiempo sec.</asp:TextBox>
                                                </th>
                                                <th style="width: 11%" runat="server" visible="false">
                                                    <asp:TextBox ID="tituloMATTEMP" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange">Tiempo sec.</asp:TextBox>
                                                </th>
                                                <th style="width: 11%" runat="server" visible="false">
                                                    <asp:TextBox ID="tituloMATTIEMP" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange">Tiempo sec.:</asp:TextBox>
                                                </th>





                                            </tr>
                                            <tr>
                                                <th style="width: 10%">
                                                    <%-- <asp:TextBox ID="MAT1STOCK" runat="server" Style="text-align:center; width:100%" Enabled="false" Visible="false"></asp:TextBox>--%>
                                                    <a href="#" id="MATSAVE1" class="btn btn-primary btn-sm" style="text-align: center; width: 100%" runat="server" onserverclick="GuardaMaterial" visible="false"><span class="glyphicon glyphicon-floppy-disk"></span></a>

                                                </th>
                                                <th style="width: 15%">
                                                    <asp:TextBox ID="MAT1REF" runat="server" Style="text-align: center; width: 100%" Enabled="false" Visible="false"></asp:TextBox>
                                                </th>
                                                <th style="width: 25%">
                                                    <asp:TextBox ID="MAT1NOM" runat="server" Style="text-align: center; width: 100%" Enabled="false" Visible="false"></asp:TextBox>
                                                </th>
                                                <td style="width: 9%">
                                                    <asp:TextBox ID="MAT1LOT" runat="server" Style="text-align: center; width: 100%" Visible="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 9%">
                                                    <asp:TextBox ID="MAT1LOT2" runat="server" Style="text-align: center; width: 100%" Visible="false"></asp:TextBox>
                                                </td>
                                                <th style="width: 22%">
                                                    <asp:TextBox ID="MAT1REMARK2" runat="server" Style="text-align: center; width: 100%" Enabled="false" Visible="false"></asp:TextBox>
                                                </th>
                                                <td style="width: 10%">
                                                    <asp:TextBox ID="MAT1TEMPREAL" runat="server" Style="text-align: center; width: 100%" Visible="false"></asp:TextBox>
                                                </td>
                                                <th style="width: 11%" runat="server" visible="FALSE">
                                                    <asp:TextBox ID="MAT1TIEMP" runat="server" Style="text-align: center; width: 100%" Enabled="false" Visible="false"></asp:TextBox>
                                                </th>

                                                <td style="width: 10%">
                                                    <asp:TextBox ID="MAT1TIEMPREAL" runat="server" Style="text-align: center; width: 100%" Visible="false"></asp:TextBox>
                                                </td>
                                                <th style="width: 11%" runat="server" visible="FALSE">
                                                    <asp:TextBox ID="MAT1TEMP" runat="server" Style="text-align: center; width: 100%" Enabled="false" Visible="false"></asp:TextBox>
                                                </th>

                                            </tr>
                                            <tr>
                                                <th style="width: 10%">
                                                    <%-- <asp:TextBox ID="MAT2STOCK" runat="server" Style="text-align:center; width:100%" Enabled="false" Visible="false"></asp:TextBox>--%>
                                                    <a href="#" id="MATSAVE2" class="btn btn-primary btn-sm" style="text-align: center; width: 100%" runat="server" onserverclick="GuardaMaterial" visible="false"><span class="glyphicon glyphicon-floppy-disk"></span></a>
                                                </th>
                                                <th style="width: 15%">
                                                    <asp:TextBox ID="MAT2REF" runat="server" Style="text-align: center; width: 100%" Enabled="false" Visible="false"></asp:TextBox>
                                                </th>
                                                <th style="width: 25%">
                                                    <asp:TextBox ID="MAT2NOM" runat="server" Style="text-align: center; width: 100%" Enabled="false" Visible="false"></asp:TextBox>
                                                </th>
                                                <td style="width: 9%">
                                                    <asp:TextBox ID="MAT2LOT" runat="server" Style="text-align: center; width: 100%" Visible="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 9%">
                                                    <asp:TextBox ID="MAT2LOT2" runat="server" Style="text-align: center; width: 100%" Visible="false"></asp:TextBox>
                                                </td>
                                                <th style="width: 22%">
                                                    <asp:TextBox ID="MAT2REMARK2" runat="server" Style="text-align: center; width: 100%" Enabled="false" Visible="false"></asp:TextBox>
                                                </th>
                                                <td style="width: 5%">
                                                    <asp:TextBox ID="MAT2TEMPREAL" runat="server" Style="text-align: center; width: 100%" Visible="false"></asp:TextBox>
                                                </td>
                                                <th style="width: 11%" runat="server" visible="FALSE">
                                                    <asp:TextBox ID="MAT2TIEMP" runat="server" Style="text-align: center; width: 100%" Enabled="false" Visible="false"></asp:TextBox>
                                                </th>
                                                <td style="width: 5%">
                                                    <asp:TextBox ID="MAT2TIEMPREAL" runat="server" Style="text-align: center; width: 100%" Visible="false"></asp:TextBox>
                                                </td>
                                                <th style="width: 11%" runat="server" visible="FALSE">
                                                    <asp:TextBox ID="MAT2TEMP" runat="server" Style="text-align: center; width: 100%" Enabled="false" Visible="false"></asp:TextBox>
                                                </th>

                                            </tr>
                                            <tr>
                                                <th style="width: 10%">
                                                    <%-- <asp:TextBox ID="MAT3STOCK" runat="server" Style="text-align:center; width:100%" Enabled="false" Visible="false"></asp:TextBox>--%>
                                                    <a href="#" id="MATSAVE3" class="btn btn-primary btn-sm" style="text-align: center; width: 100%" runat="server" onserverclick="GuardaMaterial" visible="false"><span class="glyphicon glyphicon-floppy-disk"></span></a>

                                                </th>
                                                <th style="width: 15%">
                                                    <asp:TextBox ID="MAT3REF" runat="server" Style="text-align: center; width: 100%" Enabled="false" Visible="false"></asp:TextBox>
                                                </th>
                                                <th style="width: 25%">
                                                    <asp:TextBox ID="MAT3NOM" runat="server" Style="text-align: center; width: 100%" Enabled="false" Visible="false"></asp:TextBox>
                                                </th>
                                                <td style="width: 9%">
                                                    <asp:TextBox ID="MAT3LOT" runat="server" Style="text-align: center; width: 100%" Visible="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 9%">
                                                    <asp:TextBox ID="MAT3LOT2" runat="server" Style="text-align: center; width: 100%" Visible="false"></asp:TextBox>
                                                </td>
                                                <th style="width: 22%">
                                                    <asp:TextBox ID="MAT3REMARK2" runat="server" Style="text-align: center; width: 100%" Enabled="false" Visible="false"></asp:TextBox>
                                                </th>
                                                <td style="width: 5%">
                                                    <asp:TextBox ID="MAT3TEMPREAL" runat="server" Style="text-align: center; width: 100%" Visible="false"></asp:TextBox>
                                                </td>
                                                <th style="width: 11%" runat="server" visible="FALSE">
                                                    <asp:TextBox ID="MAT3TIEMP" runat="server" Style="text-align: center; width: 100%" Enabled="false" Visible="false"></asp:TextBox>
                                                </th>
                                                <td style="width: 5%">
                                                    <asp:TextBox ID="MAT3TIEMPREAL" runat="server" Style="text-align: center; width: 100%" Visible="false"></asp:TextBox>
                                                </td>
                                                <th style="width: 11%" runat="server" visible="FALSE">
                                                    <asp:TextBox ID="MAT3TEMP" runat="server" Style="text-align: center; width: 100%" Enabled="false" Visible="false"></asp:TextBox>
                                                </th>

                                            </tr>
                                            <tr>
                                                <th style="width: 10%">
                                                    <%-- <asp:TextBox ID="COMP1STOCK" runat="server" Style="text-align:center; width:100%" Enabled="false" Visible="false"></asp:TextBox>--%>
                                                    <a href="#" id="COMPSAVE1" class="btn btn-primary btn-sm" style="text-align: center; width: 100%" runat="server" onserverclick="GuardaMaterial" visible="false"><span class="glyphicon glyphicon-floppy-disk"></span></a>
                                                </th>
                                                <th style="width: 15%">
                                                    <asp:TextBox ID="COMP1REF" runat="server" Style="text-align: center; width: 100%" Enabled="false" Visible="false"></asp:TextBox>
                                                </th>
                                                <th style="width: 25%">
                                                    <asp:TextBox ID="COMP1NOM" runat="server" Style="text-align: center; width: 100%" Enabled="false" Visible="false"></asp:TextBox>
                                                </th>
                                                <td style="width: 9%">
                                                    <asp:TextBox ID="COMP1LOT" runat="server" Style="text-align: center; width: 100%" Visible="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 9%">
                                                    <asp:TextBox ID="COMP1LOT2" runat="server" Style="text-align: center; width: 100%" Visible="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th style="width: 10%">
                                                    <a href="#" id="COMPSAVE2" class="btn btn-primary btn-sm" style="text-align: center; width: 100%" runat="server" onserverclick="GuardaMaterial" visible="false"><span class="glyphicon glyphicon-floppy-disk"></span></a>

                                                    <%-- <asp:TextBox ID="COMP2STOCK" runat="server" Style="text-align:center; width:100%" Enabled="false" Visible="false"></asp:TextBox>--%>
                                                </th>
                                                <th style="width: 15%">
                                                    <asp:TextBox ID="COMP2REF" runat="server" Style="text-align: center; width: 100%" Enabled="false" Visible="false"></asp:TextBox>
                                                </th>
                                                <th style="width: 25%">
                                                    <asp:TextBox ID="COMP2NOM" runat="server" Style="text-align: center; width: 100%" Enabled="false" Visible="false"></asp:TextBox>
                                                </th>
                                                <td style="width: 9%">
                                                    <asp:TextBox ID="COMP2LOT" runat="server" Style="text-align: center; width: 100%" Visible="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 9%">
                                                    <asp:TextBox ID="COMP2LOT2" runat="server" Style="text-align: center; width: 100%" Visible="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th style="width: 10%">
                                                    <a href="#" id="COMPSAVE3" class="btn btn-primary btn-sm" style="text-align: center; width: 100%" runat="server" onserverclick="GuardaMaterial" visible="false"><span class="glyphicon glyphicon-floppy-disk"></span></a>

                                                    <%-- <asp:TextBox ID="COMP3STOCK" runat="server" Style="text-align:center; width:100%" Enabled="false" Visible="false"></asp:TextBox>--%>
                                                </th>
                                                <th style="width: 15%">
                                                    <asp:TextBox ID="COMP3REF" runat="server" Style="text-align: center; width: 100%" Enabled="false" Visible="false"></asp:TextBox>
                                                </th>
                                                <th style="width: 25%">
                                                    <asp:TextBox ID="COMP3NOM" runat="server" Style="text-align: center; width: 100%" Enabled="false" Visible="false"></asp:TextBox>
                                                </th>
                                                <td style="width: 9%">
                                                    <asp:TextBox ID="COMP3LOT" runat="server" Style="text-align: center; width: 100%" Visible="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 9%">
                                                    <asp:TextBox ID="COMP3LOT2" runat="server" Style="text-align: center; width: 100%" Visible="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th style="width: 10%">
                                                    <a href="#" id="COMPSAVE4" class="btn btn-primary btn-sm" style="text-align: center; width: 100%" runat="server" onserverclick="GuardaMaterial" visible="false"><span class="glyphicon glyphicon-floppy-disk"></span></a>

                                                    <%-- <asp:TextBox ID="COMP4STOCK" runat="server" Style="text-align:center; width:100%" Enabled="false" Visible="false"></asp:TextBox>--%>
                                                </th>
                                                <th style="width: 15%">
                                                    <asp:TextBox ID="COMP4REF" runat="server" Style="text-align: center; width: 100%" Enabled="false" Visible="false"></asp:TextBox>
                                                </th>
                                                <th style="width: 25%">
                                                    <asp:TextBox ID="COMP4NOM" runat="server" Style="text-align: center; width: 100%" Enabled="false" Visible="false"></asp:TextBox>
                                                </th>
                                                <td style="width: 9%">
                                                    <asp:TextBox ID="COMP4LOT" runat="server" Style="text-align: center; width: 100%" Visible="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 9%">
                                                    <asp:TextBox ID="COMP4LOT2" runat="server" Style="text-align: center; width: 100%" Visible="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th style="width: 10%">
                                                    <a href="#" id="COMPSAVE5" class="btn btn-primary btn-sm" style="text-align: center; width: 100%" runat="server" onserverclick="GuardaMaterial" visible="false"><span class="glyphicon glyphicon-floppy-disk"></span></a>

                                                    <%--<asp:TextBox ID="COMP5STOCK" runat="server" Style="text-align:center; width:100%" Enabled="false" Visible="false"></asp:TextBox>--%>
                                                </th>
                                                <th style="width: 15%">
                                                    <asp:TextBox ID="COMP5REF" runat="server" Style="text-align: center; width: 100%" Enabled="false" Visible="false"></asp:TextBox>
                                                </th>
                                                <th style="width: 25%">
                                                    <asp:TextBox ID="COMP5NOM" runat="server" Style="text-align: center; width: 100%" Enabled="false" Visible="false"></asp:TextBox>
                                                </th>
                                                <td style="width: 9%">
                                                    <asp:TextBox ID="COMP5LOT" runat="server" Style="text-align: center; width: 100%" Visible="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 9%">
                                                    <asp:TextBox ID="COMP5LOT2" runat="server" Style="text-align: center; width: 100%" Visible="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th style="width: 10%">
                                                    <a href="#" id="COMPSAVE6" class="btn btn-primary btn-sm" style="text-align: center; width: 100%" runat="server" onserverclick="GuardaMaterial" visible="false"><span class="glyphicon glyphicon-floppy-disk"></span></a>
                                                    <%-- <asp:TextBox ID="COMP6STOCK" runat="server" Style="text-align:center; width:100%" Enabled="false" Visible="false"></asp:TextBox>--%>
                                                </th>
                                                <th style="width: 15%">
                                                    <asp:TextBox ID="COMP6REF" runat="server" Style="text-align: center; width: 100%" Enabled="false" Visible="false"></asp:TextBox>
                                                </th>
                                                <th style="width: 25%">
                                                    <asp:TextBox ID="COMP6NOM" runat="server" Style="text-align: center; width: 100%" Enabled="false" Visible="false"></asp:TextBox>
                                                </th>
                                                <td style="width: 9%">
                                                    <asp:TextBox ID="COMP6LOT" runat="server" Style="text-align: center; width: 100%" Visible="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 9%">
                                                    <asp:TextBox ID="COMP6LOT2" runat="server" Style="text-align: center; width: 100%" Visible="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th style="width: 10%">
                                                    <a href="#" id="COMPSAVE7" class="btn btn-primary btn-sm" style="text-align: center; width: 100%" runat="server" onserverclick="GuardaMaterial" visible="false"><span class="glyphicon glyphicon-floppy-disk"></span></a>

                                                    <%-- <asp:TextBox ID="COMP7STOCK" runat="server" Style="text-align:center; width:100%" Enabled="false" Visible="false"></asp:TextBox>--%>
                                                </th>
                                                <th style="width: 15%">
                                                    <asp:TextBox ID="COMP7REF" runat="server" Style="text-align: center; width: 100%" Enabled="false" Visible="false"></asp:TextBox>
                                                </th>
                                                <th style="width: 25%">
                                                    <asp:TextBox ID="COMP7NOM" runat="server" Style="text-align: center; width: 100%" Enabled="false" Visible="false"></asp:TextBox>
                                                </th>
                                                <td style="width: 9%">
                                                    <asp:TextBox ID="COMP7LOT" runat="server" Style="text-align: center; width: 100%" Visible="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 9%">
                                                    <asp:TextBox ID="COMP7LOT2" runat="server" Style="text-align: center; width: 100%" Visible="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>

                            <label>
                                <h2>Cambio de molde</h2>
                            </label>
                            &nbsp <a href="http://facts4-srv/thermogestion/DOCUMENTAL/DOCUMENTOS/ITGs-01%20Ed.05%20Cambios%20de%20Molde%20y%20Limpieza%20de%20husillos.pdf" style="font-size: small" target="_blank"><span class="glyphicon glyphicon-file"></span>ITGs-01: Cambios de Molde y Limpieza de husillos  </a>

                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="table-responsive">
                                        <table style="table-layout: fixed; width: 100%">
                                            <tr>
                                                <th style="width: 18%">
                                                    <asp:TextBox ID="TextBox12" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange">Evaluación</asp:TextBox>
                                                </th>
                                                <th style="width: 17%">
                                                    <asp:TextBox ID="TextBox13" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange">Estándar</asp:TextBox>
                                                </th>
                                                <th style="width: 32%">
                                                    <asp:TextBox ID="TextBox14" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange">¿Qué comprobar?</asp:TextBox>
                                                </th>
                                                <th style="width: 33%">
                                                    <asp:TextBox ID="TextBox15" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange">Comentarios</asp:TextBox>
                                                </th>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="container">
                                        <div class="col-lg-2">
                                            <div class="o-switch btn-group" data-toggle="buttons" role="group">
                                                <label id="Q1_OK" class="btn btn-lg btn-primary" runat="server">
                                                    <input type="radio" name="options15" id="Q1_OKOPT" runat="server" autocomplete="off"><span class="glyphicon glyphicon-thumbs-up"></span>
                                                </label>
                                                <label id="Q1_NOK" class="btn btn-lg btn-primary" runat="server">
                                                    <input type="radio" name="options15" id="Q1_NOKOPT" runat="server" autocomplete="off"><span class="glyphicon glyphicon-thumbs-down"></span>
                                                </label>
                                                <label id="Q1_NA" class="btn btn-lg btn-primary active" runat="server">
                                                    <input type="radio" name="options15" id="Q1_NAOPT" runat="server" autocomplete="off" checked><span class="glyphicon glyphicon-ban-circle"></span>
                                                </label>
                                            </div>
                                        </div>
                                        <div class="col-lg-2">
                                            <p><strong>1. Máquina y programas</strong></p>
                                        </div>
                                        <div class="col-lg-4">
                                            <p>- Programa de máquina y robot correctamente cargado y funcional.</p>
                                        </div>
                                        <div class="col-lg-4">
                                            <asp:TextBox ID="TbQ1ENC" runat="server" Style="text-align: center; width: 100%; height: 45px" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="container">
                                        <div class="col-lg-2">
                                            <div class="o-switch btn-group" data-toggle="buttons" role="group">
                                                <label id="Q2_OK" class="btn btn-lg btn-primary " runat="server">
                                                    <input type="radio" name="options16" id="Q2_OKOPT" runat="server" autocomplete="off"><span class="glyphicon glyphicon-thumbs-up"></span>
                                                </label>
                                                <label id="Q2_NOK" class="btn btn-lg btn-primary" runat="server">
                                                    <input type="radio" name="options16" id="Q2_NOKOPT" runat="server" autocomplete="off"><span class="glyphicon glyphicon-thumbs-down"></span>
                                                </label>
                                                <label id="Q2_NA" class="btn btn-lg btn-primary active" runat="server">
                                                    <input type="radio" name="options16" id="Q2_NAOPT" runat="server" autocomplete="off" checked><span class="glyphicon glyphicon-ban-circle"></span>
                                                </label>
                                            </div>
                                        </div>
                                        <div class="col-lg-2">
                                            <p><strong>2. Conexiones de agua</strong></p>
                                        </div>
                                        <div class="col-lg-4">
                                            <p>- Todos los elementos de atemperado conectados según la especificación.</p>
                                        </div>
                                        <div class="col-lg-4">
                                            <asp:TextBox ID="TbQ2ENC" runat="server" Style="text-align: center; width: 100%; height: 45px" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="container">
                                        <div class="col-lg-2">
                                            <div class="o-switch btn-group" data-toggle="buttons" role="group">
                                                <label id="Q3_OK" class="btn btn-lg btn-primary" runat="server">
                                                    <input type="radio" name="options3" id="Q3_OKOPT" runat="server" autocomplete="off"><span class="glyphicon glyphicon-thumbs-up"></span>
                                                </label>
                                                <label id="Q3_NOK" class="btn btn-lg btn-primary" runat="server">
                                                    <input type="radio" name="options3" id="Q3_NOKOPT" runat="server" autocomplete="off"><span class="glyphicon glyphicon-thumbs-down"></span>
                                                </label>
                                                <label id="Q3_NA" class="btn btn-lg btn-primary active" runat="server">
                                                    <input type="radio" name="options3" id="Q3_NAOPT" runat="server" autocomplete="off" checked><span class="glyphicon glyphicon-ban-circle"></span>
                                                </label>
                                            </div>
                                        </div>
                                        <div class="col-lg-2">
                                            <p><strong>3. Periféricos y ajuste mecánico</strong></p>
                                        </div>
                                        <div class="col-lg-4">
                                            <p>- Regulador de cámara caliente conectado y funcional.</p>
                                            <p>- Ajustes de carro, expulsor, noyos... según especificación.</p>
                                            <p>- Secuencia en manual y verificación paso a paso correcta.</p>

                                        </div>
                                        <div class="col-lg-4">
                                            <asp:TextBox ID="TbQ3ENC" runat="server" Style="text-align: center; width: 100%; height: 100px" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="table-responsive">
                                        <table style="table-layout: fixed; width: 100%">
                                            <tr>
                                                <th style="width: 15%">
                                                    <asp:TextBox ID="TituloMantMolde" runat="server" Style="text-align: center; width: 100%; height: 60px" Enabled="false" BackColor="Orange">Últ. aviso de molde:</asp:TextBox>
                                                </th>
                                                <th style="width: 5%">
                                                    <asp:TextBox ID="tbParteMolde" runat="server" Style="text-align: center; width: 100%; height: 60px" Enabled="false"></asp:TextBox>
                                                </th>
                                                <th style="width: 27%">
                                                    <asp:TextBox ID="TbMantMolde" runat="server" Style="text-align: center; width: 100%; height: 60px; vertical-align: middle" Enabled="false" TextMode="MultiLine"></asp:TextBox>
                                                </th>
                                                <th style="width: 21%">
                                                    <asp:TextBox ID="TbRepaMolde" runat="server" Style="text-align: center; width: 100%; height: 60px; vertical-align: middle" Enabled="false" TextMode="MultiLine"></asp:TextBox>
                                                </th>
                                                <th style="width: 14%">
                                                    <asp:TextBox ID="TbEstadoRepMolde" runat="server" Style="text-align: center; width: 100%; height: 60px; vertical-align: middle" Enabled="false" TextMode="MultiLine"></asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <button type="button" id="BotonAbrirParte" class="btn btn-info" style="text-align: center; width: 100%; height: 60px; vertical-align: middle" runat="server" onserverclick="redireccionaGRAL" target="_blank">Ver parte</button>
                                                </th>
                                                <th style="width: 8%">
                                                    <button type="button" id="BotonCrearParte" class="btn btn-danger" style="text-align: center; width: 100%; height: 60px; vertical-align: middle" runat="server" onserverclick="redireccionaGRAL" target="_blank">Crear parte</button>
                                                </th>
                                            </tr>
                                        </table>
                                    </div>
                                    <button type="button" class="btn btn-xs btn-default" data-toggle="collapse" data-target="#demo" style="width: 100%"><span class="glyphicon glyphicon-th-list"></span>Ver todos</button>
                                </div>
                                <div class="col-lg-12">
                                    <div class="col-lg-12">
                                        <div id="demo" class="collapse">
                                            <div class="table-responsive">
                                                <asp:GridView ID="dgv_mantmolde" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" OnRowCommand="RedireccionaGridView"
                                                    EmptyDataText="Sin datos para mostrar.">
                                                    <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Parte" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblParte" runat="server" Text='<%#Eval("IdReparacionMolde") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Avería" ItemStyle-Width="35%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblReferencia" runat="server" Text='<%#Eval("MotivoAveria") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Reparación" ItemStyle-Width="35%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblReparacion" runat="server" Text='<%#Eval("Reparacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Estado" ItemStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEstado" runat="server" Text='<%#Eval("Texto") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="" ItemStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:Button ID="BotonAbrirParteMolde2" class="btn btn-info btn-sm" Style="text-align: center; width: 100%; vertical-align: middle" CommandName="RedirectMOLDE" CommandArgument='<%#Eval("IdReparacionMolde")%>' runat="server" Text="Ver parte" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-lg-12">
                                    <h3>Notas de liberación&nbsp<a href="#" title="Anota tus consideraciones:" data-toggle="popover" data-placement="top" data-content="Anota aquello que sea reseñable a la liberación, como si falta un formato, un elemento está degradado o si existe una oportunidad de mejora en el proceso."><span class="glyphicon glyphicon-question-sign"></span></a></h3>
                                    <div class="table-responsive">
                                        <table style="table-layout: fixed; width: 100%">
                                            <tr>
                                                <th style="width: 20%">
                                                    <asp:TextBox ID="TextBox21" runat="server" Style="text-align: center; width: 100%; height: 35px" Enabled="false" BackColor="Orange">Notas cambiador:</asp:TextBox>
                                                </th>
                                                <th style="width: 60%">
                                                    <asp:TextBox ID="QXFeedbackCambiador" runat="server" Style="text-align: center; width: 100%; height: 35px" TextMode="MultiLine"></asp:TextBox>
                                                </th>
                                                <th style="width: 20%">
                                                    <button id="LiberarCambio" runat="server" type="button" onserverclick="LiberarCambiador" class="btn btn-success btn-md" style="text-align: center; width: 100%; height: 35px">Liberar (Cambiador)</button>
                                                </th>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <br />
                        </div>
                        <%-- Cierro panel de atemperado--%>
                        <%-- Abro panel de parámetros--%>
                        <div id="PARAMETROS" class="tab-pane fade" runat="server">
                            <div class="row">
                                <div class="col-lg-12">
                                    <h2>Proceso de arranque</h2>
                                    <div class="table-responsive">
                                        <table style="table-layout: fixed; width: 100%">
                                            <tr>
                                                <th style="width: 18%">
                                                    <asp:TextBox ID="TextBox16" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange">Evaluación</asp:TextBox>
                                                </th>
                                                <th style="width: 17%">
                                                    <asp:TextBox ID="TextBox17" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange">Estándar</asp:TextBox>
                                                </th>
                                                <th style="width: 32%">
                                                    <asp:TextBox ID="TextBox18" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange">¿Qué comprobar?</asp:TextBox>
                                                </th>
                                                <th style="width: 33%">
                                                    <asp:TextBox ID="TextBox19" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange">Comentarios</asp:TextBox>
                                                </th>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="container">
                                        <div class="col-lg-2">
                                            <div class="o-switch btn-group" data-toggle="buttons" role="group">
                                                <label id="Q4_OK" class="btn btn-lg btn-primary " runat="server">
                                                    <input type="radio" name="options4" id="Q4_OKOPT" runat="server" autocomplete="off"><span class="glyphicon glyphicon-thumbs-up"></span>
                                                </label>
                                                <label id="Q4_NOK" class="btn btn-lg btn-primary" runat="server">
                                                    <input type="radio" name="options4" id="Q4_NOKOPT" runat="server" autocomplete="off"><span class="glyphicon glyphicon-thumbs-down"></span>
                                                </label>
                                                <label id="Q4_NA" class="btn btn-lg btn-primary active" runat="server">
                                                    <input type="radio" name="options4" id="Q4_NAOPT" runat="server" autocomplete="off" checked><span class="glyphicon glyphicon-ban-circle"></span>
                                                </label>
                                            </div>
                                        </div>
                                        <div class="col-lg-2">
                                            <p><strong>4. Condiciones iniciales</strong></p>
                                        </div>
                                        <div class="col-lg-4">
                                            <p><i>- Proceso de cambio realizado correctamente.</i> <a href="#" title="Audita a tus compañeros:" data-toggle="popover" data-placement="top" data-content="Verifica que el proceso de cambio de moldes se ha realizado correctamente y anota cualquier desviación o mejora que encuentres oportuna en esta sección."><span class="glyphicon glyphicon-question-sign"></span></a></p>
                                            <p>- Circuito de refrigeración abierto.</p>
                                            <p>- Husillo purgado y tolva limpia.</p>
                                            <p>- Parámetros de robot adecuados</p>
                                            <p style="font-size:smaller; font: italic"> &nbsp&nbsp&nbsp&nbsp-Segregado de 3 inyectadas tras paro.</p>
                                            <p style="font-size:smaller; font: italic"> &nbsp&nbsp&nbsp&nbsp-Velocidad de robot y cotas.</p>
                                            
                                            <p>- Elementos de seguridad del proceso (si aplica).</p>
                                        </div>
                                        <div class="col-lg-4">
                                            <asp:TextBox ID="TbQ4ENC" runat="server" Style="text-align: center; width: 100%; height: 75px" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="container">
                                        <div class="col-lg-2">
                                            <div class="o-switch btn-group" data-toggle="buttons" role="group">
                                                <label id="Q5_OK" class="btn btn-lg btn-primary" runat="server">
                                                    <input type="radio" name="options5" id="Q5_OKOPT" runat="server" autocomplete="off"><span class="glyphicon glyphicon-thumbs-up"></span>
                                                </label>
                                                <label id="Q5_NOK" class="btn btn-lg btn-primary" runat="server">
                                                    <input type="radio" name="options5" id="Q5_NOKOPT" runat="server" autocomplete="off"><span class="glyphicon glyphicon-thumbs-down"></span>
                                                </label>
                                                <label id="Q5_NA" class="btn btn-lg btn-primary active" runat="server">
                                                    <input type="radio" name="options5" id="Q5_NAOPT" runat="server" autocomplete="off" checked><span class="glyphicon glyphicon-ban-circle"></span>
                                                </label>
                                            </div>
                                        </div>
                                        <div class="col-lg-2">
                                            <p><strong>5. Primeras inyectadas</strong></p>
                                        </div>
                                        <div class="col-lg-4">
                                            <p>- Aspecto visual de la pieza según características definidas.</p>
                                            <p>- Fechador actualizado (año/mes/día).</p>
                                        </div>
                                        <div class="col-lg-4">
                                            <asp:TextBox ID="TbQ5ENC" runat="server" Style="text-align: center; width: 100%; height: 75px" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="container">
                                        <div class="col-lg-2">
                                            <div class="o-switch btn-group" data-toggle="buttons" role="group">
                                                <label id="Q6_OK" class="btn btn-lg btn-primary" runat="server">
                                                    <input type="radio" name="options6" id="Q6_OKOPT" runat="server" autocomplete="off"><span class="glyphicon glyphicon-thumbs-up"></span>
                                                </label>
                                                <label id="Q6_NOK" class="btn btn-lg btn-primary" runat="server">
                                                    <input type="radio" name="options6" id="Q6_NOKOPT" runat="server" autocomplete="off"><span class="glyphicon glyphicon-thumbs-down"></span>
                                                </label>
                                                <label id="Q6_NA" class="btn btn-lg btn-primary active" runat="server">
                                                    <input type="radio" name="options6" id="Q6_NAOPT" runat="server" autocomplete="off" checked><span class="glyphicon glyphicon-ban-circle"></span>
                                                </label>
                                            </div>
                                        </div>
                                        <div class="col-lg-2">
                                            <p><strong>6. Pokayokes, galgas de control y máquinas periféricas</strong></p>
                                        </div>
                                        <div class="col-lg-4">
                                            <p>
                                                - Muestras límite de liberación disponibles.</p>
                                            
                                            <p>- Validación de arranque y funcionamiento.</p>
                                            <p>- Documentación vinculada disponible y actualizada.</p>
                                        </div>
                                        <div class="col-lg-4">
                                            <asp:TextBox ID="TbQ6ENC" runat="server" Style="text-align: center; width: 100%; height: 50px" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />

                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="table-responsive">
                                        <table style="table-layout: fixed; width: 100%">
                                            <tr>
                                                <th style="width: 15%">
                                                    <asp:TextBox ID="TituloMantMaq" runat="server" Style="text-align: center; width: 100%; height: 60px" Enabled="false" BackColor="Orange">Últ. aviso de máquina:</asp:TextBox>
                                                </th>
                                                <th style="width: 5%">
                                                    <asp:TextBox ID="tbParteMaq" runat="server" Style="text-align: center; width: 100%; height: 60px" Enabled="false"></asp:TextBox>
                                                </th>
                                                <th style="width: 27%">
                                                    <asp:TextBox ID="TbMantMaq" runat="server" Style="text-align: center; width: 100%; height: 60px" Enabled="false" TextMode="MultiLine"></asp:TextBox>
                                                </th>
                                                <th style="width: 21%">
                                                    <asp:TextBox ID="TbRepaMaq" runat="server" Style="text-align: center; width: 100%; height: 60px" Enabled="false" TextMode="MultiLine"></asp:TextBox>
                                                </th>
                                                <th style="width: 14%">
                                                    <asp:TextBox ID="TbEstadoRepMaq" runat="server" Style="text-align: center; width: 100%; height: 60px" Enabled="false" TextMode="MultiLine"></asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <button type="button" id="BotonAbrirParteMaq" class="btn btn-info" style="text-align: center; width: 100%; height: 60px; vertical-align: middle" runat="server" onserverclick="redireccionaGRAL" target="_blank">Ver parte</button>
                                                </th>
                                                <th style="width: 8%">
                                                    <button type="button" id="BotonCrearParteMaq" class="btn btn-danger" style="text-align: center; width: 100%; height: 60px; vertical-align: middle" runat="server" onserverclick="redireccionaGRAL" target="_blank">Crear parte</button>
                                                </th>
                                            </tr>
                                        </table>
                                    </div>
                                    <button type="button" class="btn btn-xs btn-default" data-toggle="collapse" data-target="#demo2" style="width: 100%"><span class="glyphicon glyphicon-th-list"></span>Ver todos</button>
                                </div>
                                <div class="col-lg-12">
                                    <div class="col-lg-12">
                                        <div id="demo2" class="collapse">
                                            <div class="table-responsive">
                                                <asp:GridView ID="dgv_mantmaq" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                    Width="98.5%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" OnRowCommand="RedireccionaGridView"
                                                    EmptyDataText="Sin datos para mostrar.">
                                                    <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                    <EditRowStyle BackColor="#ffffcc" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Parte" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPartemaq" runat="server" Text='<%#Eval("IdMantenimiento") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Avería" ItemStyle-Width="40%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblReferenciamaq" runat="server" Text='<%#Eval("MotivoAveria") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Reparación" ItemStyle-Width="40%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblReparacionmaq" runat="server" Text='<%#Eval("Reparacion") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Estado" ItemStyle-Width="40%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEstadomaq" runat="server" Text='<%#Eval("Texto") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="" ItemStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:Button ID="BotonAbrirParteMaq2" class="btn btn-info btn-sm" Style="text-align: center; width: 100%; vertical-align: middle" CommandName="RedirectMAQ" CommandArgument='<%#Eval("IdMantenimiento")%>' runat="server" Text="Ver parte" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-lg-12">
                                    <h2>Configuración de máquina&nbsp<a href="#" title="Verifica los parámetros:" data-toggle="popover" data-placement="top" data-content="Anota únicamente aquellos parámetros que no se correspondan y pulsa en guardar parámetros. En caso de requerir modificación, anota los motivos del cambio."><span class="glyphicon glyphicon-question-sign"></span></a></h2>
                                    <asp:TextBox ID="PARAMSESTADO" runat="server" Style="text-align: center; width: 100%; height: 50%" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="EXISTEFICHA" runat="server" Style="text-align: center; width: 100%; height: 50%" Visible="false">0</asp:TextBox>
                                    <p><strong>TEMPERATURAS (CILINDRO)</strong></p>
                                    <div class="table-responsive">
                                        <table style="table-layout: fixed; width: 100%">
                                            <tr>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="tbcarcilindro" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">CÁRACT.</asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="tbBoq" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">BOQ</asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="tbT1" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">T1</asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="tbT2" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">T2</asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="tbT3" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">T3</asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="tbT4" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">T4</asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="tbT5" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">T5</asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="tbT6" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">T6</asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="tbT7" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">T7</asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="tbT8" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">T8</asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="tbT9" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">T9</asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="tbT10" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">T10</asp:TextBox>
                                                </th>
                                            </tr>
                                            <tr>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="tbNOMcilindro" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false">NOM.</asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="thBoq" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="thT1" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="thT2" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="thT3" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="thT4" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="thT5" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="thT6" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="thT7" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="thT8" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="thT9" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="thT10" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                </th>
                                            </tr>
                                            <tr>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="TextBox1" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false">REAL</asp:TextBox>
                                                </th>
                                                <td style="width: 8%">
                                                    <asp:TextBox ID="thBoqREAL" runat="server" Style="text-align: center; width: 100%; height: 50%" content="1"></asp:TextBox>
                                                </td>
                                                <td style="width: 8%">
                                                    <asp:TextBox ID="thT1REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                </td>
                                                <td style="width: 8%">
                                                    <asp:TextBox ID="thT2REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                </td>
                                                <td style="width: 8%">
                                                    <asp:TextBox ID="thT3REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thT4REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                </td>
                                                <td style="width: 8%">
                                                    <asp:TextBox ID="thT5REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                </td>
                                                <td style="width: 8%">
                                                    <asp:TextBox ID="thT6REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                </td>
                                                <td style="width: 8%">
                                                    <asp:TextBox ID="thT7REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                </td>
                                                <td style="width: 8%">
                                                    <asp:TextBox ID="thT8REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                </td>
                                                <td style="width: 8%">
                                                    <asp:TextBox ID="thT9REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                </td>
                                                <td style="width: 8%">
                                                    <asp:TextBox ID="thT10REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                </td>
                                            </tr>

                                        </table>
                                    </div>
                                    <br />
                                    <p><strong>TEMPERATURAS (CÁMARA CALIENTE)</strong></p>
                                    <div class="table-responsive">
                                        <table style="table-layout: fixed; width: 100%">
                                            <tr>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="CARACCAMCALIENTE" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">CÁRACT.</asp:TextBox>
                                                </th>

                                                <th style="width: 8%">
                                                    <asp:TextBox ID="TBZ1" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Z1</asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="TBZ2" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Z2</asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="TBZ3" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Z3</asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="TBZ4" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Z4</asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="TBZ5" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Z5</asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="TBZ6" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Z6</asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="TBZ7" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Z7</asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="TBZ8" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Z8</asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="TBZ9" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Z9</asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="TBZ10" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Z10</asp:TextBox>
                                                </th>
                                            </tr>
                                            <tr>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="tbNOMCamCaliente" runat="server" Style="text-align: center; width: 100%; height: 50%; height: 50%" Enabled="false">NOM.</asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="thZ1" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="thZ2" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="thZ3" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="thZ4" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="thZ5" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="thZ6" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="thZ7" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="thZ8" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="thZ9" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="thZ10" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                </th>
                                            </tr>
                                            <tr>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="tbREALCamCaliente" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false">REAL</asp:TextBox>
                                                </th>
                                                <td style="width: 8%">
                                                    <asp:TextBox ID="thZ1REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                </td>
                                                <td style="width: 8%">
                                                    <asp:TextBox ID="thZ2REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                </td>
                                                <td style="width: 8%">
                                                    <asp:TextBox ID="thZ3REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thZ4REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                </td>
                                                <td style="width: 8%">
                                                    <asp:TextBox ID="thZ5REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                </td>
                                                <td style="width: 8%">
                                                    <asp:TextBox ID="thZ6REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                </td>
                                                <td style="width: 8%">
                                                    <asp:TextBox ID="thZ7REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                </td>
                                                <td style="width: 8%">
                                                    <asp:TextBox ID="thZ8REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                </td>
                                                <td style="width: 8%">
                                                    <asp:TextBox ID="thZ9REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                </td>
                                                <td style="width: 8%">
                                                    <asp:TextBox ID="thZ10REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="tbREALCamCaliente2" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false">REAL</asp:TextBox>
                                                </th>

                                                <td style="width: 8%">
                                                    <asp:TextBox ID="thZ11REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                </td>
                                                <td style="width: 8%">
                                                    <asp:TextBox ID="thZ12REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                </td>
                                                <td style="width: 8%">
                                                    <asp:TextBox ID="thZ13REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="thZ14REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                </td>
                                                <td style="width: 8%">
                                                    <asp:TextBox ID="thZ15REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                </td>
                                                <td style="width: 8%">
                                                    <asp:TextBox ID="thZ16REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                </td>
                                                <td style="width: 8%">
                                                    <asp:TextBox ID="thZ17REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                </td>
                                                <td style="width: 8%">
                                                    <asp:TextBox ID="thZ18REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                </td>
                                                <td style="width: 8%">
                                                    <asp:TextBox ID="thZ19REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                </td>
                                                <td style="width: 8%">
                                                    <asp:TextBox ID="thZ20REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="tbNOMCamCaliente2" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false">NOM.</asp:TextBox>
                                                </th>

                                                <th style="width: 8%">
                                                    <asp:TextBox ID="thZ11" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="thZ12" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="thZ13" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="thZ14" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="thZ15" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="thZ16" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="thZ17" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="thZ18" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="thZ19" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="thZ20" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                </th>
                                            </tr>
                                            <tr>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="tbcarcilindro2" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">CÁRACT.</asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="tbZ11" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Z11</asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="tbZ12" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Z12</asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="tbZ13" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Z13</asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="tbZ14" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Z14</asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="tbZ15" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Z15</asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="tbZ16" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Z16</asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="tbZ17" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Z17</asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="tbZ18" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Z18</asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="tbZ19" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Z19</asp:TextBox>
                                                </th>
                                                <th style="width: 8%">
                                                    <asp:TextBox ID="tbZ20" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Z20</asp:TextBox>
                                                </th>
                                            </tr>
                                        </table>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <p><strong>ATEMPERADO: PARTE FIJA</strong></p>
                                            <div class="table-responsive">
                                                <table style="table-layout: fixed; width: 100%">
                                                    <tr>
                                                        <th style="width: 14%">
                                                            <asp:TextBox ID="ThCircuitoF" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Circuito:</asp:TextBox>
                                                        </th>
                                                        <th style="width: 12%">
                                                            <asp:TextBox ID="TbCircuitoF1" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 12%">
                                                            <asp:TextBox ID="TbCircuitoF2" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 12%">
                                                            <asp:TextBox ID="TbCircuitoF3" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 12%">
                                                            <asp:TextBox ID="TbCircuitoF4" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 12%">
                                                            <asp:TextBox ID="TbCircuitoF5" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 12%">
                                                            <asp:TextBox ID="TbCircuitoF6" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 14%">
                                                            <asp:TextBox ID="TextBox2REAL" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">CARACT.</asp:TextBox>
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <th style="width: 14%">
                                                            <asp:TextBox ID="ThCaudalF" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false">Caudal:</asp:TextBox>
                                                        </th>
                                                        <th style="width: 12%">
                                                            <asp:TextBox ID="TbCaudalF1" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 12%">
                                                            <asp:TextBox ID="TbCaudalF2" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 12%">
                                                            <asp:TextBox ID="TbCaudalF3" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 12%">
                                                            <asp:TextBox ID="TbCaudalF4" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 12%">
                                                            <asp:TextBox ID="TbCaudalF5" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 12%">
                                                            <asp:TextBox ID="TbCaudalF6" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 14%">
                                                            <asp:TextBox ID="TextBox3" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false">NOM.</asp:TextBox>
                                                        </th>
                                                    </tr>
                                                    <tr>

                                                        <th style="width: 14%">
                                                            <asp:TextBox ID="TextBox4" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false">Caudal:</asp:TextBox>
                                                        </th>
                                                        <td style="width: 12%">
                                                            <asp:TextBox ID="TbCaudalF1REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 12%">
                                                            <asp:TextBox ID="TbCaudalF2REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 12%">
                                                            <asp:TextBox ID="TbCaudalF3REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 12%">
                                                            <asp:TextBox ID="TbCaudalF4REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 12%">
                                                            <asp:TextBox ID="TbCaudalF5REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 12%">
                                                            <asp:TextBox ID="TbCaudalF6REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <th style="width: 14%">
                                                            <asp:TextBox ID="ThCaudalFREAL" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false">REAL</asp:TextBox>
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <th style="width: 14%">
                                                            <asp:TextBox ID="TextBox5" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false">Temp.:</asp:TextBox>
                                                        </th>
                                                        <th style="width: 12%">
                                                            <asp:TextBox ID="TbTemperaturaF1" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 12%">
                                                            <asp:TextBox ID="TbTemperaturaF2" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 12%">
                                                            <asp:TextBox ID="TbTemperaturaF3" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 12%">
                                                            <asp:TextBox ID="TbTemperaturaF4" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 12%">
                                                            <asp:TextBox ID="TbTemperaturaF5" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 12%">
                                                            <asp:TextBox ID="TbTemperaturaF6" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 14%">
                                                            <asp:TextBox ID="ThTemperaturaF" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false">NOM.</asp:TextBox>
                                                        </th>
                                                    </tr>
                                                    <tr>

                                                        <th style="width: 14%">
                                                            <asp:TextBox ID="ThTemperaturaFREAL" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false">Temp.:</asp:TextBox>
                                                        </th>
                                                        <td style="width: 12%">
                                                            <asp:TextBox ID="TbTemperaturaF1REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 12%">
                                                            <asp:TextBox ID="TbTemperaturaF2REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 12%">
                                                            <asp:TextBox ID="TbTemperaturaF3REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 12%">
                                                            <asp:TextBox ID="TbTemperaturaF4REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 12%">
                                                            <asp:TextBox ID="TbTemperaturaF5REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 12%">
                                                            <asp:TextBox ID="TbTemperaturaF6REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <th style="width: 14%">
                                                            <asp:TextBox ID="ThTemperaturaFRREAL" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false">REAL</asp:TextBox>
                                                        </th>
                                                    </tr>
                                                </table>

                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <p><strong>ATEMPERADO: PARTE MÓVIL</strong></p>
                                            <div class="table-responsive">
                                                <table style="table-layout: fixed; width: 100%">
                                                    <tr>
                                                        <th style="width: 14%">
                                                            <asp:TextBox ID="ThCircuitoM" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Circuito:</asp:TextBox>
                                                        </th>
                                                        <th style="width: 12%">
                                                            <asp:TextBox ID="TbCircuitoM1" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 12%">
                                                            <asp:TextBox ID="TbCircuitoM2" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 12%">
                                                            <asp:TextBox ID="TbCircuitoM3" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 12%">
                                                            <asp:TextBox ID="TbCircuitoM4" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 12%">
                                                            <asp:TextBox ID="TbCircuitoM5" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 12%">
                                                            <asp:TextBox ID="TbCircuitoM6" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 14%">
                                                            <asp:TextBox ID="TextBox6" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">CARACT.</asp:TextBox>
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <th style="width: 14%">
                                                            <asp:TextBox ID="ThCaudalM" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false">Caudal:</asp:TextBox>
                                                        </th>
                                                        <th style="width: 12%">
                                                            <asp:TextBox ID="TbCaudalM1" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 12%">
                                                            <asp:TextBox ID="TbCaudalM2" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 12%">
                                                            <asp:TextBox ID="TbCaudalM3" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 12%">
                                                            <asp:TextBox ID="TbCaudalM4" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 12%">
                                                            <asp:TextBox ID="TbCaudalM5" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 12%">
                                                            <asp:TextBox ID="TbCaudalM6" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 14%">
                                                            <asp:TextBox ID="TextBox7" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false">NOM.</asp:TextBox>
                                                        </th>
                                                    </tr>
                                                    <tr>

                                                        <th style="width: 14%">
                                                            <asp:TextBox ID="TextBox8" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false">Caudal:</asp:TextBox>
                                                        </th>
                                                        <td style="width: 12%">
                                                            <asp:TextBox ID="TbCaudalM1REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 12%">
                                                            <asp:TextBox ID="TbCaudalM2REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 12%">
                                                            <asp:TextBox ID="TbCaudalM3REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 12%">
                                                            <asp:TextBox ID="TbCaudalM4REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 12%">
                                                            <asp:TextBox ID="TbCaudalM5REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 12%">
                                                            <asp:TextBox ID="TbCaudalM6REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <th style="width: 14%">
                                                            <asp:TextBox ID="ThCaudalMREAL" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false">REAL</asp:TextBox>
                                                        </th>
                                                    </tr>
                                                    <tr>

                                                        <th style="width: 14%">
                                                            <asp:TextBox ID="TextBox9" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false">Temp.:</asp:TextBox>
                                                        </th>
                                                        <th style="width: 12%">
                                                            <asp:TextBox ID="TbTemperaturaM1" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 12%">
                                                            <asp:TextBox ID="TbTemperaturaM2" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 12%">
                                                            <asp:TextBox ID="TbTemperaturaM3" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 12%">
                                                            <asp:TextBox ID="TbTemperaturaM4" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 12%">
                                                            <asp:TextBox ID="TbTemperaturaM5" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 12%">
                                                            <asp:TextBox ID="TbTemperaturaM6" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 14%">
                                                            <asp:TextBox ID="ThTemperaturaM" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false">NOM.</asp:TextBox>
                                                        </th>
                                                    </tr>
                                                    <tr>

                                                        <th style="width: 14%">
                                                            <asp:TextBox ID="ThTemperaturaMTITULOREAL" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false">Temp.:</asp:TextBox>
                                                        </th>
                                                        <td style="width: 12%">
                                                            <asp:TextBox ID="TbTemperaturaM1REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 12%">
                                                            <asp:TextBox ID="TbTemperaturaM2REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 12%">
                                                            <asp:TextBox ID="TbTemperaturaM3REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 12%">
                                                            <asp:TextBox ID="TbTemperaturaM4REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 12%">
                                                            <asp:TextBox ID="TbTemperaturaM5REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 12%">
                                                            <asp:TextBox ID="TbTemperaturaM6REAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <th style="width: 14%">
                                                            <asp:TextBox ID="ThTemperaturaMREAL" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false">REAL</asp:TextBox>
                                                        </th>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-lg-8">
                                            <p><strong>POSTPRESIÓN</strong></p>
                                            <div class="table-responsive">
                                                <table style="table-layout: fixed; width: 100%">
                                                    <tr>
                                                        <th>
                                                            <asp:TextBox ID="tbPasoPresion" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Paso</asp:TextBox>
                                                        </th>
                                                        <th>
                                                            <asp:TextBox ID="tbP1" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">1</asp:TextBox>
                                                        </th>
                                                        <th>
                                                            <asp:TextBox ID="tbP2" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">2</asp:TextBox>
                                                        </th>
                                                        <th>
                                                            <asp:TextBox ID="tbP3" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">3</asp:TextBox>
                                                        </th>
                                                        <th>
                                                            <asp:TextBox ID="tbP4" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">4</asp:TextBox>
                                                        </th>
                                                        <th>
                                                            <asp:TextBox ID="tbP5" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">5</asp:TextBox>
                                                        </th>
                                                        <th>
                                                            <asp:TextBox ID="tbP6" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">6</asp:TextBox>
                                                        </th>
                                                        <th>
                                                            <asp:TextBox ID="tbP7" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">7</asp:TextBox>
                                                        </th>
                                                        <th>
                                                            <asp:TextBox ID="tbP8" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">8</asp:TextBox>
                                                        </th>
                                                        <th>
                                                            <asp:TextBox ID="tbP9" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">9</asp:TextBox>
                                                        </th>
                                                        <th>
                                                            <asp:TextBox ID="tbP10" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">10</asp:TextBox>
                                                        </th>
                                                        <th>
                                                            <asp:TextBox ID="TextBox10" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">CAR.</asp:TextBox>
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <th>
                                                            <asp:TextBox ID="thPresion" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false">Pres.:</asp:TextBox>
                                                        </th>
                                                        <th>
                                                            <asp:TextBox ID="thP1" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th>
                                                            <asp:TextBox ID="thP2" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th>
                                                            <asp:TextBox ID="thP3" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th>
                                                            <asp:TextBox ID="thP4" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th>
                                                            <asp:TextBox ID="thP5" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th>
                                                            <asp:TextBox ID="thP6" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th>
                                                            <asp:TextBox ID="thP7" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th>
                                                            <asp:TextBox ID="thP8" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th>
                                                            <asp:TextBox ID="thP9" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th>
                                                            <asp:TextBox ID="thP10" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th>
                                                            <asp:TextBox ID="thP10NOM" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false">NOM.</asp:TextBox>
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <th>
                                                            <asp:TextBox ID="thPresionR" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false">Pres.:</asp:TextBox>
                                                        </th>
                                                        <td>
                                                            <asp:TextBox ID="thP1R" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="thP2R" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="thP3R" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="thP4R" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="thP5R" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="thP6R" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="thP7R" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="thP8R" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="thP9R" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="thP10R" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <th>
                                                            <asp:TextBox ID="thP10REAL" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false">REAL</asp:TextBox>
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <th>
                                                            <asp:TextBox ID="thTPtiempo" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false">Tiem.:</asp:TextBox>
                                                        </th>
                                                        <th>
                                                            <asp:TextBox ID="thTP1" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th>
                                                            <asp:TextBox ID="thTP2" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th>
                                                            <asp:TextBox ID="thTP3" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th>
                                                            <asp:TextBox ID="thTP4" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th>
                                                            <asp:TextBox ID="thTP5" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th>
                                                            <asp:TextBox ID="thTP6" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th>
                                                            <asp:TextBox ID="thTP7" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th>
                                                            <asp:TextBox ID="thTP8" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th>
                                                            <asp:TextBox ID="thTP9" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th>
                                                            <asp:TextBox ID="thTP10" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th>
                                                            <asp:TextBox ID="thTPNOM" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false">NOM.</asp:TextBox>
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <th>
                                                            <asp:TextBox ID="thTPtiempoR" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false">Tiem.:</asp:TextBox>
                                                        </th>
                                                        <td>
                                                            <asp:TextBox ID="thTP1R" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="thTP2R" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="thTP3R" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="thTP4R" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="thTP5R" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="thTP6R" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="thTP7R" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="thTP8R" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="thTP9R" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="thTP10R" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <th>
                                                            <asp:TextBox ID="thTPREAL" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false">REAL</asp:TextBox>
                                                        </th>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <p><strong>CONMUTACIÓN</strong></p>
                                            <div class="table-responsive">
                                                <table style="table-layout: fixed; width: 100%">
                                                    <tr>
                                                        <th style="width: 50%" colspan="4">
                                                            <asp:TextBox ID="tbConmutacionTitulo" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Conmutación</asp:TextBox>
                                                        </th>
                                                        <th style="width: 50%" colspan="4">
                                                            <asp:TextBox ID="tbTiempoPresionTitulo" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Tiempo presión</asp:TextBox>
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <th style="width: 50%" colspan="4">
                                                            <asp:TextBox ID="tbConmutacion" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 50%" colspan="4">
                                                            <asp:TextBox ID="tbTiempoPresion" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 50%" colspan="4">
                                                            <asp:TextBox ID="tbConmutacionREAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 50%" colspan="4">
                                                            <asp:TextBox ID="tbTiempoPresionREAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th style="width: 25%">
                                                            <asp:TextBox ID="thConmuntaciontolN" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Mín.</asp:TextBox>
                                                        </th>
                                                        <th style="width: 25%">
                                                            <asp:TextBox ID="thConmuntaciontolNVal" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 25%">
                                                            <asp:TextBox ID="thConmuntaciontolM" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Máx.</asp:TextBox>
                                                        </th>
                                                        <th style="width: 25%">
                                                            <asp:TextBox ID="thConmuntaciontolMVal" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 25%">
                                                            <asp:TextBox ID="tbTiempoPresiontolN" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Mín.</asp:TextBox>
                                                        </th>
                                                        <th style="width: 25%">
                                                            <asp:TextBox ID="tbTiempoPresiontolNVal" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 25%">
                                                            <asp:TextBox ID="tbTiempoPresiontolM" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Máx.</asp:TextBox>
                                                        </th>
                                                        <th style="width: 25%">
                                                            <asp:TextBox ID="tbTiempoPresiontolMVal" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-lg-3">
                                            <p><strong>INYECCIÓN</strong></p>
                                            <div class="table-responsive">
                                                <table id="Table2" style="table-layout: fixed; width: 100%" runat="server">
                                                    <tr>
                                                        <th colspan="2" style="width: 50%">
                                                            <asp:TextBox ID="tbTiempoInyeccionTitulo" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Tiempo</asp:TextBox>
                                                        </th>
                                                        <th colspan="2" style="width: 50%">
                                                            <asp:TextBox ID="tbLimitePresionTitulo" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Lim. Presión</asp:TextBox>
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <th colspan="2" style="width: 50%">
                                                            <asp:TextBox ID="tbTiempoInyeccion" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th colspan="2" style="width: 50%">
                                                            <asp:TextBox ID="tbLimitePresion" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="width: 50%">
                                                            <asp:TextBox ID="tbTiempoInyeccionREAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <td colspan="2" style="width: 50%">
                                                            <asp:TextBox ID="tbLimitePresionREAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th style="width: 50%">
                                                            <asp:TextBox ID="tbTiempoInyeccionNVal" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 50%">
                                                            <asp:TextBox ID="tbTiempoInyeccionMVal" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 50%">
                                                            <asp:TextBox ID="tbLimitePresionNVal" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 50%">
                                                            <asp:TextBox ID="tbLimitePresionMVal" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>

                                                    </tr>
                                                    <tr>
                                                        <th style="width: 50%">
                                                            <asp:TextBox ID="tbTiempoInyeccionN" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Mín.</asp:TextBox>
                                                        </th>
                                                        <th style="width: 50%">
                                                            <asp:TextBox ID="tbTiempoInyeccionM" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Máx.</asp:TextBox>
                                                        </th>

                                                        <th style="width: 50%">
                                                            <asp:TextBox ID="tbLimitePresionN" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Mín.</asp:TextBox>
                                                        </th>

                                                        <th style="width: 50%">
                                                            <asp:TextBox ID="tbLimitePresionM" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Máx.</asp:TextBox>
                                                        </th>


                                                    </tr>
                                                </table>
                                            </div>
                                        </div>

                                        <div class="col-lg-6">
                                            <p><strong>DOSIFICADO</strong></p>

                                            <div class="table-responsive">
                                                <table id="Tabledosificado" style="table-layout: fixed; width: 100%" runat="server">
                                                    <tr>
                                                        <th colspan="2" style="width: 16%">
                                                            <asp:TextBox ID="tbVCarga" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">V. Carga</asp:TextBox>
                                                        </th>
                                                        <th colspan="2" style="width: 16%">
                                                            <asp:TextBox ID="tbCarga" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Carga</asp:TextBox>
                                                        </th>
                                                        <th colspan="2" style="width: 16%">
                                                            <asp:TextBox ID="tbDescom" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Descom.</asp:TextBox>
                                                        </th>
                                                        <th colspan="2" style="width: 16%">
                                                            <asp:TextBox ID="tbContra" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Contrapr.</asp:TextBox>
                                                        </th>
                                                        <th colspan="2" style="width: 16%">
                                                            <asp:TextBox ID="tbTiempoDos" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Tiempo</asp:TextBox>
                                                        </th>

                                                        <th colspan="2" style="width: 16%">
                                                            <asp:TextBox ID="tbCojin" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Cojín</asp:TextBox>
                                                        </th>
                                                    </tr>
                                                    <tr id="tr1" runat="server">
                                                        <th colspan="2" style="width: 16%">
                                                            <asp:TextBox ID="thVCarga" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th colspan="2" style="width: 16%">
                                                            <asp:TextBox ID="thCarga" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th colspan="2" style="width: 16%">
                                                            <asp:TextBox ID="thDescomp" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th colspan="2" style="width: 16%">
                                                            <asp:TextBox ID="thContrapr" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th colspan="2" style="width: 16%">
                                                            <asp:TextBox ID="thTiempo" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th colspan="2" style="width: 16%">
                                                            <asp:TextBox ID="thCojin" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                    </tr>
                                                    <tr id="tr4" runat="server">
                                                        <td colspan="2" style="width: 16%">
                                                            <asp:TextBox ID="thVCargaREAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <td colspan="2" style="width: 16%">
                                                            <asp:TextBox ID="thCargaREAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <td colspan="2" style="width: 16%">
                                                            <asp:TextBox ID="thDescompREAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <td colspan="2" style="width: 16%">
                                                            <asp:TextBox ID="thContraprREAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <td colspan="2" style="width: 16%">
                                                            <asp:TextBox ID="thTiempoREAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <td colspan="2" style="width: 16%">
                                                            <asp:TextBox ID="thCojinREAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                    </tr>

                                                    <tr id="tr2" runat="server">
                                                        <th style="width: 8%">
                                                            <asp:TextBox ID="TNvcargaval" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 8%">
                                                            <asp:TextBox ID="TMvcargaval" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 8%">
                                                            <asp:TextBox ID="TNcargaval" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 8%">
                                                            <asp:TextBox ID="TMcargaval" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 8%">
                                                            <asp:TextBox ID="TNdescomval" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 8%">
                                                            <asp:TextBox ID="TMdescomval" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 8%">
                                                            <asp:TextBox ID="TNcontrapval" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 8%">
                                                            <asp:TextBox ID="TMcontrapval" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 8%">
                                                            <asp:TextBox ID="TNTiempdosval" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 8%">
                                                            <asp:TextBox ID="TMTiempdosval" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 8%">
                                                            <asp:TextBox ID="TNCojinval" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 8%">
                                                            <asp:TextBox ID="TMCojinval" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                    </tr>
                                                    <tr id="tr3" runat="server">
                                                        <th style="width: 8%">
                                                            <asp:TextBox ID="TNvcargavalMIN" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Mín.</asp:TextBox>
                                                        </th>
                                                        <th style="width: 8%">
                                                            <asp:TextBox ID="TMvcargavalMAX" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Máx.</asp:TextBox>
                                                        </th>
                                                        <th style="width: 8%">
                                                            <asp:TextBox ID="TNcargavalMINMAX" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Mín.</asp:TextBox>
                                                        </th>
                                                        <th style="width: 8%">
                                                            <asp:TextBox ID="TMcargavalMAX" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Máx.</asp:TextBox>
                                                        </th>
                                                        <th style="width: 8%">
                                                            <asp:TextBox ID="TNdescomvalMIN" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Mín.</asp:TextBox>
                                                        </th>
                                                        <th style="width: 8%">
                                                            <asp:TextBox ID="TMdescomvalMAX" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Máx.</asp:TextBox>
                                                        </th>
                                                        <th style="width: 8%">
                                                            <asp:TextBox ID="TNcontrapvalMIN" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Mín.</asp:TextBox>
                                                        </th>
                                                        <th style="width: 8%">
                                                            <asp:TextBox ID="TMcontrapvalMAX" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Máx.</asp:TextBox>
                                                        </th>
                                                        <th style="width: 8%">
                                                            <asp:TextBox ID="TNTiempdosvalMIN" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Mín.</asp:TextBox>
                                                        </th>
                                                        <th style="width: 8%">
                                                            <asp:TextBox ID="TMTiempdosvalMAX" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Máx.</asp:TextBox>
                                                        </th>
                                                        <th style="width: 8%">
                                                            <asp:TextBox ID="TNCojinvalMIN" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Mín.</asp:TextBox>
                                                        </th>
                                                        <th style="width: 8%">
                                                            <asp:TextBox ID="TMCojinvalMAX" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Máx.</asp:TextBox>
                                                        </th>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <p><strong>CICLO</strong></p>
                                            <div class="table-responsive">
                                                <table id="TableCiclo" style="table-layout: fixed; width: 100%" runat="server">
                                                    <tr>
                                                        <th colspan="2" style="width: 50%">
                                                            <asp:TextBox ID="tbEnfriamiento" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Enfriam.</asp:TextBox>
                                                        </th>
                                                        <th colspan="2" style="width: 50%">
                                                            <asp:TextBox ID="tbCiclo" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Ciclo</asp:TextBox>
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <th colspan="2" style="width: 50%">
                                                            <asp:TextBox ID="thEnfriamiento" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th colspan="2" style="width: 50%">
                                                            <asp:TextBox ID="thCiclo" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="width: 50%">
                                                            <asp:TextBox ID="thEnfriamientoREAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                        <td colspan="2" style="width: 50%">
                                                            <asp:TextBox ID="thCicloREAL" runat="server" Style="text-align: center; width: 100%; height: 50%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th style="width: 50%">
                                                            <asp:TextBox ID="TNEnfriamval" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 50%">
                                                            <asp:TextBox ID="TMEnfriamval" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 50%">
                                                            <asp:TextBox ID="TNCicloval" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                        <th style="width: 50%">
                                                            <asp:TextBox ID="TMCicloval" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false"></asp:TextBox>
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <th style="width: 50%">
                                                            <asp:TextBox ID="TNEnfriam" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Mín.</asp:TextBox>
                                                        </th>
                                                        <th style="width: 50%">
                                                            <asp:TextBox ID="TMEnfriam" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Máx.</asp:TextBox>
                                                        </th>

                                                        <th style="width: 50%">
                                                            <asp:TextBox ID="TNCiclo" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Mín.</asp:TextBox>
                                                        </th>

                                                        <th style="width: 50%">
                                                            <asp:TextBox ID="TMCiclo" runat="server" Style="text-align: center; width: 100%; height: 50%" Enabled="false" BackColor="Orange">Máx.</asp:TextBox>
                                                        </th>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>

                            <div class="row">
                                <br />
                                <div class="col-lg-12">
                                    <div class="table-responsive">
                                        <table style="table-layout: fixed; width: 100%">
                                            <tr>
                                                <th style="width: 20%">
                                                    <asp:TextBox ID="TextBox45" runat="server" Style="text-align: center; width: 100%; height: 35px" Enabled="false" BackColor="Orange">Motivo de cambios:</asp:TextBox>
                                                </th>
                                                <th style="width: 50%">
                                                    <asp:TextBox ID="MotivoCambioParam" runat="server" Style="text-align: center; width: 100%; height: 35px" TextMode="MultiLine" Enabled="true"></asp:TextBox>
                                                </th>

                                            </tr>

                                        </table>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <br />

                                <div class="col-lg-6">
                                    <button id="LIMPIAYRECARGA" class="btn btn-info btn-lg" style="text-align: center; width: 100%; height: 45px" runat="server" onserverclick="RecargayLimpiaParametros">Limpiar y recargar ficha</button>
                                </div>
                                <div class="col-lg-6">
                                    <button id="PARACOMPARA" class="btn btn-success btn-lg" style="text-align: center; width: 100%; height: 45px" runat="server" onserverclick="LiberarParametros">Guardar parámetros</button>
                                </div>

                            </div>
                            <br />
                            <br />

                        </div>
                        <%-- Cierro panel de parámetros--%>
                        <div id="PROCESO" class="tab-pane fade" runat="server">
                            <h2>Proceso de arranque (Operativa)</h2>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="table-responsive">
                                        <table style="table-layout: fixed; width: 100%">
                                            <tr>
                                                <th style="width: 18%">
                                                    <asp:TextBox ID="TextBox27" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange">Evaluación</asp:TextBox>
                                                </th>
                                                <th style="width: 17%">
                                                    <asp:TextBox ID="TextBox28" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange">Estándar</asp:TextBox>
                                                </th>
                                                <th style="width: 32%">
                                                    <asp:TextBox ID="TextBox29" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange">¿Qué comprobar?</asp:TextBox>
                                                </th>
                                                <th style="width: 33%">
                                                    <asp:TextBox ID="TextBox30" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange">Comentarios</asp:TextBox>
                                                </th>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <br />

                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="container">
                                        <div class="col-lg-2">
                                            <div class="o-switch btn-group" data-toggle="buttons" role="group">
                                                <label id="Q7_OK" class="btn btn-lg btn-primary " runat="server">
                                                    <input type="radio" name="options7" id="Q7_OKOPT" runat="server" autocomplete="off"><span class="glyphicon glyphicon-thumbs-up"></span>
                                                </label>
                                                <label id="Q7_NOK" class="btn btn-lg btn-primary" runat="server">
                                                    <input type="radio" name="options7" id="Q7_NOKOPT" runat="server" autocomplete="off"><span class="glyphicon glyphicon-thumbs-down"></span>
                                                </label>
                                                <label id="Q7_NA" class="btn btn-lg btn-primary active" runat="server">
                                                    <input type="radio" name="options7" id="Q7_NAOPT" runat="server" autocomplete="off" checked><span class="glyphicon glyphicon-ban-circle"></span>
                                                </label>
                                            </div>
                                        </div>
                                        <div class="col-lg-2">
                                            <p><strong>7. Puesto de trabajo</strong></p>
                                        </div>
                                        <div class="col-lg-4">
                                            <p>- Pieza de muestra de última producción disponible.</p>
                                            <p>- Puesto de trabajo distribuido según layout.</p>
                                            <p>- Sin elementos de otros productos en el puesto.</p>
                                            <p>- Documentación disponible y actualizada.</p>
                                        </div>
                                        <div class="col-lg-4">
                                            <asp:TextBox ID="TbQ7ENC" runat="server" Style="text-align: center; width: 100%; height: 100px" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="container">
                                        <div class="col-lg-2">
                                            <div class="o-switch btn-group" data-toggle="buttons" role="group">
                                                <label id="Q8_OK" class="btn btn-lg btn-primary" runat="server">
                                                    <input type="radio" name="options8" id="Q8_OKOPT" runat="server" autocomplete="off"><span class="glyphicon glyphicon-thumbs-up"></span>
                                                </label>
                                                <label id="Q8_NOK" class="btn btn-lg btn-primary" runat="server">
                                                    <input type="radio" name="options8" id="Q8_NOKOPT" runat="server" autocomplete="off"><span class="glyphicon glyphicon-thumbs-down"></span>
                                                </label>
                                                <label id="Q8_NA" class="btn btn-lg btn-primary active" runat="server">
                                                    <input type="radio" name="options8" id="Q8_NAOPT" runat="server" autocomplete="off" checked><span class="glyphicon glyphicon-ban-circle"></span>
                                                </label>
                                            </div>
                                        </div>
                                        <div class="col-lg-2">
                                            <p><strong>8. Anti mezclas</strong></p>
                                        </div>
                                        <div class="col-lg-4">
                                            <p>- Separador de caja pulmón disponible e instalado.</p>
                                            <p>- Correlación de etiquetado (posición RH/LH, tipo y color de papel).</p>
                                            <p>- Primeras etiquetas verificadas (producto fabricado y descripción).</p>
                                            <p>- Sin etiquetas de otras producciones en el puesto.</p>
                                        </div>
                                        <div class="col-lg-4">
                                            <asp:TextBox ID="TbQ8ENC" runat="server" Style="text-align: center; width: 100%; height: 90px" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="container">
                                        <div class="col-lg-2">
                                            <div class="o-switch btn-group" data-toggle="buttons" role="group">
                                                <label id="Q9_OK" class="btn btn-lg btn-primary" runat="server">
                                                    <input type="radio" name="options9" id="Q9_OKOPT" runat="server" autocomplete="off"><span class="glyphicon glyphicon-thumbs-up"></span>
                                                </label>
                                                <label id="Q9_NOK" class="btn btn-lg btn-primary" runat="server">
                                                    <input type="radio" name="options9" id="Q9_NOKOPT" runat="server" autocomplete="off"><span class="glyphicon glyphicon-thumbs-down"></span>
                                                </label>
                                                <label id="Q9_NA" class="btn btn-lg btn-primary active" runat="server">
                                                    <input type="radio" name="options9" id="Q9_NAOPT" runat="server" autocomplete="off" checked><span class="glyphicon glyphicon-ban-circle"></span>
                                                </label>
                                            </div>
                                        </div>
                                        <div class="col-lg-2">
                                            <p><strong>9. Elementos auxiliares</strong></p>
                                        </div>
                                        <div class="col-lg-4">
                                            <p>- Cubeta roja y amarillas presentes e identificadas.</p>
                                            <p>- Luz suficiente en el puesto.</p>
                                            <p>- Orden y limpieza general en el puesto.</p>
                                        </div>
                                        <div class="col-lg-4">
                                            <asp:TextBox ID="TbQ9ENC" runat="server" Style="text-align: center; width: 100%; height: 75px" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-lg-12">
                                    <h2>Alertas de calidad</h2>
                                    <div class="col-lg-1">
                                        <button id="A3" class="btn btn-danger btn-lg" style="text-align: center; width: 100%; height: 45px" runat="server" onserverclick="redireccionadetalleNC"></button>
                                        <button id="A3OK" type="button" class="btn btn-success btn-lg" style="text-align: center; width: 100%; height: 45px" runat="server" visible="false" onserverclick="redireccionaGRAL"><span class="glyphicon glyphicon-ok"></span></button>
                                        <%-- <a href="#" ID="A3" class="btn btn-danger btn-lg" value="0" runat="server" Style="text-align:center; width:100%; height:45px" target="_blank"></a>
                                <script>
                                    $("#A3").click(function () {
                                        $(this).toggleClass('btn-danger btn-success');
                                        $(this).value = '1';
                                    });
                                </script>   --%>
                                    </div>
                                    <div class="col-lg-5">
                                        <h4>Encargado - Conoce la última no conformidad denunciada.</h4>
                                    </div>
                                    <div class="col-lg-1">
                                        <button id="A5" class="btn btn-danger btn-lg" style="text-align: center; width: 100%; height: 45px" runat="server" onserverclick="redireccionadetalleGP12"></button>
                                        <button id="A5OK" type="button" class="btn btn-success btn-lg" style="text-align: center; width: 100%; height: 45px" runat="server" visible="false" onserverclick="redireccionaGRAL"><span class="glyphicon glyphicon-ok"></span></button>
                                    </div>
                                    <div class="col-lg-5">
                                        <h4>Encargado - Conoce los defectos encontrados en GP12.</h4>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-lg-12">
                                    <h3>Notas de liberación&nbsp<a href="#" title="Anota tus consideraciones:" data-toggle="popover" data-placement="top" data-content="Anota aquello que sea reseñable a la liberación, como si es un arranque tras parada larga, se deja funcionando en degradado o si existe una oportunidad de mejora en el proceso."><span class="glyphicon glyphicon-question-sign"></span></a></h3>

                                    <div class="table-responsive">
                                        <table style="table-layout: fixed; width: 100%">
                                            <tr>
                                                <th style="width: 20%">
                                                    <asp:TextBox ID="TextBox20" runat="server" Style="text-align: center; width: 100%; height: 35px" Enabled="false" BackColor="Orange">Notas producción:</asp:TextBox>
                                                </th>
                                                <th style="width: 60%">
                                                    <asp:TextBox ID="QXFeedbackProduccion" runat="server" Style="text-align: center; width: 100%; height: 35px" TextMode="MultiLine"></asp:TextBox>
                                                </th>
                                                <th style="width: 20%">
                                                    <button id="LiberarProduccion" runat="server" type="button" onserverclick="LiberarEncargado" class="btn btn-success btn-md" style="text-align: center; width: 100%; height: 35px">Liberar (Producción)</button>
                                                </th>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <br />
                        </div>
                        <%-- Abro panel de calidad--%>
                        <div id="CALIDAD" class="tab-pane fade" runat="server">

                            <div class="row">

                                <div class="col-lg-12">
                                    <h2>Control de atributos</h2>
                                    <div class="table-responsive">
                                        <table style="table-layout: fixed; width: 100%">
                                            <tr>
                                                <th style="width: 18%">
                                                    <asp:TextBox ID="TextBox32" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange">Evaluación</asp:TextBox>
                                                </th>
                                                <th style="width: 17%">
                                                    <asp:TextBox ID="TextBox33" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange">Estándar</asp:TextBox>
                                                </th>
                                                <th style="width: 32%">
                                                    <asp:TextBox ID="TextBox34" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange">¿Qué comprobar?</asp:TextBox>
                                                </th>
                                                <th style="width: 33%">
                                                    <asp:TextBox ID="TextBox35" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange">Hallazgos</asp:TextBox>
                                                </th>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="container">
                                        <div class="col-lg-2">
                                            <div class="o-switch btn-group" data-toggle="buttons" role="group">
                                                <label id="Q10C_OK" class="btn btn-lg btn-primary" runat="server">
                                                    <input type="radio" name="options10" id="Q10C_OKOPT" runat="server" autocomplete="off"><span class="glyphicon glyphicon-thumbs-up"></span>
                                                </label>
                                                <label id="Q10C_NOK" class="btn btn-lg btn-primary" runat="server">
                                                    <input type="radio" name="options10" id="Q10C_NOKOPT" runat="server" autocomplete="off"><span class="glyphicon glyphicon-thumbs-down"></span>
                                                </label>
                                                <label id="Q10C_NA" class="btn btn-lg btn-primary active" runat="server">
                                                    <input type="radio" name="options10" id="Q10C_NAOPT" runat="server" autocomplete="off" checked><span class="glyphicon glyphicon-ban-circle"></span>
                                                </label>
                                            </div>
                                        </div>
                                        <div class="col-lg-2">
                                            <p><strong>10. Control de atributos</strong></p>
                                        </div>
                                        <div class="col-lg-4">
                                            <p>- Verificación muestra de producción con piezas del último lote y correlación con referencia liberada.</p>
                                            <p>- Atributos medidos en Q-Master y conformes a la especificación.</p>
                                            <p>- Aspecto visual de la pieza según características definidas.</p>
                                            <p>- Fechador actualizado (año/mes/día).</p>
                                        </div>
                                        <div class="col-lg-4">
                                            <asp:TextBox ID="TbQ10CAL" runat="server" Style="text-align: center; width: 100%; height: 170px" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-12">
                                    <h2>Alertas de calidad</h2>
                                    <div class="col-lg-1">
                                        <button id="A7" class="btn btn-danger btn-lg" style="text-align: center; width: 100%; height: 45px" runat="server" onserverclick="redireccionadetalleNC"></button>
                                        <button id="A7OK" type="button" class="btn btn-success btn-lg" style="text-align: center; width: 100%; height: 45px" runat="server" visible="false" onserverclick="redireccionaGRAL"><span class="glyphicon glyphicon-ok"></span></button>
                                    </div>
                                    <div class="col-lg-5">
                                        <h4>Operario informado la última no conformidad denunciada.</h4>
                                    </div>
                                    <div class="col-lg-1">
                                        <button id="A8" class="btn btn-danger btn-lg" style="text-align: center; width: 100%; height: 45px" runat="server" onserverclick="redireccionadetalleGP12"></button>
                                        <button id="A8OK" type="button" class="btn btn-success btn-lg" style="text-align: center; width: 100%; height: 45px" runat="server" visible="false" onserverclick="redireccionaGRAL"><span class="glyphicon glyphicon-ok"></span></button>
                                    </div>

                                    <div class="col-lg-5">
                                        <h4>Operario informado de los defectos encontrados en GP12.</h4>
                                    </div>
                                    <h6 style="text-align: right">*Deben mostrarse la última no conformidad y los defectos al operario, verificando que los conoce.</h6>

                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-lg-12">
                                    <h2>Auditoría de liberación de serie</h2>
                                    <div class="table-responsive">
                                        <table style="table-layout: fixed; width: 100%">
                                            <tr>
                                                <th style="width: 18%">
                                                    <asp:TextBox ID="TextBox36" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange">Evaluación</asp:TextBox>
                                                </th>
                                                <th style="width: 17%">
                                                    <asp:TextBox ID="TextBox38" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange">Estándar</asp:TextBox>
                                                </th>
                                                <th style="width: 32%">
                                                    <asp:TextBox ID="TextBox39" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange">¿Qué comprobar?</asp:TextBox>
                                                </th>
                                                <th style="width: 33%">
                                                    <asp:TextBox ID="TextBox40" runat="server" Style="text-align: center; width: 100%" Enabled="false" BackColor="Orange">Hallazgos</asp:TextBox>
                                                </th>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="container">
                                        <div class="col-lg-2">
                                            <div class="o-switch btn-group" data-toggle="buttons" role="group">
                                                <label id="Q6C_OK" class="btn btn-lg btn-primary " runat="server">
                                                    <input type="radio" name="options11" id="Q6C_OKOPT" runat="server" autocomplete="off"><span class="glyphicon glyphicon-thumbs-up"></span>
                                                </label>
                                                <label id="Q6C_NOK" class="btn btn-lg btn-primary" runat="server">
                                                    <input type="radio" name="options11" id="Q6C_NOKOPT" runat="server" autocomplete="off"><span class="glyphicon glyphicon-thumbs-down"></span>
                                                </label>
                                                <label id="Q6C_NA" class="btn btn-lg btn-primary active" runat="server">
                                                    <input type="radio" name="options11" id="Q6C_NAOPT" runat="server" autocomplete="off" checked><span class="glyphicon glyphicon-ban-circle"></span>
                                                </label>
                                            </div>
                                        </div>
                                        <div class="col-lg-2">
                                            <p><strong>A6. Pokayokes y máquinas periféricas</strong></p>
                                        </div>
                                        <div class="col-lg-4">
                                            <p> - Muestras límite de pokayoke disponibles.</p>
                                                <p>- Validación de funcionamiento.</p>
                                                <p>- Documentación vinculada disponible y actualizada.</p>
                                        </div>
                                        <div class="col-lg-4">
                                            <asp:TextBox ID="TbQ6CAL" runat="server" Style="text-align: center; width: 100%; height: 70px" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <br />
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="container">
                                        <div class="col-lg-2">
                                            <div class="o-switch btn-group" data-toggle="buttons" role="group">
                                                <label id="Q7C_OK" class="btn btn-lg btn-primary " runat="server">
                                                    <input type="radio" name="options12" id="Q7C_OKOPT" runat="server" autocomplete="off" checked><span class="glyphicon glyphicon-thumbs-up"></span>
                                                </label>
                                                <label id="Q7C_NOK" class="btn btn-lg btn-primary" runat="server">
                                                    <input type="radio" name="options12" id="Q7C_NOKOPT" runat="server" autocomplete="off"><span class="glyphicon glyphicon-thumbs-down"></span>
                                                </label>
                                                <label id="Q7C_NA" class="btn btn-lg btn-primary active" runat="server">
                                                    <input type="radio" name="options12" id="Q7C_NAOPT" runat="server" autocomplete="off" checked><span class="glyphicon glyphicon-ban-circle"></span>
                                                </label>
                                            </div>
                                        </div>
                                        <div class="col-lg-2">
                                            <p><strong>A7. Puesto de trabajo</strong></p>
                                        </div>
                                        <div class="col-lg-4">
                                            <p>- Pieza de muestra de última producción disponible.</p>
                                            <p>- Puesto de trabajo distribuido según layout.</p>
                                            <p>- Sin elementos de otros productos en el puesto.</p>
                                            <p>- Documentación disponible y actualizada.</p>
                                        </div>
                                        <div class="col-lg-4">
                                            <asp:TextBox ID="TbQ7CAL" runat="server" Style="text-align: center; width: 100%; height: 100px" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="container">
                                        <div class="col-lg-2">
                                            <div class="o-switch btn-group" data-toggle="buttons" role="group">
                                                <label id="Q8C_OK" class="btn btn-lg btn-primary" runat="server">
                                                    <input type="radio" name="options13" id="Q8C_OKOPT" runat="server" autocomplete="off" checked><span class="glyphicon glyphicon-thumbs-up"></span>
                                                </label>
                                                <label id="Q8C_NOK" class="btn btn-lg btn-primary" runat="server">
                                                    <input type="radio" name="options13" id="Q8C_NOKOPT" runat="server" autocomplete="off"><span class="glyphicon glyphicon-thumbs-down"></span>
                                                </label>
                                                <label id="Q8C_NA" class="btn btn-lg btn-primary active" runat="server">
                                                    <input type="radio" name="options13" id="Q8C_NAOPT" runat="server" autocomplete="off" checked><span class="glyphicon glyphicon-ban-circle"></span>
                                                </label>
                                            </div>
                                        </div>
                                        <div class="col-lg-2">
                                            <p><strong>A8. Anti mezclas</strong></p>
                                        </div>
                                        <div class="col-lg-4">
                                            <p>- Separador de caja pulmón disponible e instalado.</p>
                                            <p>- Correlación de etiquetado (posición RH/LH, tipo y color de papel).</p>
                                            <p>- Primeras etiquetas verificadas (producto fabricado y descripción).</p>
                                            <p>- Sin etiquetas de otras producciones en el puesto.</p>
                                        </div>
                                        <div class="col-lg-4">
                                            <asp:TextBox ID="TbQ8CAL" runat="server" Style="text-align: center; width: 100%; height: 75px" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="container">
                                        <div class="col-lg-2">
                                            <div class="o-switch btn-group" data-toggle="buttons" role="group">
                                                <label id="Q9C_OK" class="btn btn-lg btn-primary" runat="server">
                                                    <input type="radio" name="options14" id="Q9C_OKOPT" runat="server" autocomplete="off"><span class="glyphicon glyphicon-thumbs-up"></span>
                                                </label>
                                                <label id="Q9C_NOK" class="btn btn-lg btn-primary" runat="server">
                                                    <input type="radio" name="options14" id="Q9C_NOKOPT" runat="server" autocomplete="off"><span class="glyphicon glyphicon-thumbs-down"></span>
                                                </label>
                                                <label id="Q9C_NA" class="btn btn-lg btn-primary active" runat="server">
                                                    <input type="radio" name="options14" id="Q9C_NAOPT" runat="server" autocomplete="off" checked><span class="glyphicon glyphicon-ban-circle"></span>
                                                </label>
                                            </div>
                                        </div>
                                        <div class="col-lg-2">
                                            <p><strong>A9. Elementos auxiliares</strong></p>
                                        </div>
                                        <div class="col-lg-4">
                                            <p>- Cubeta roja y amarillas presentes e identificadas.</p>
                                            <p>- Luz suficiente en el puesto.</p>
                                            <p>- Orden y limpieza general en el puesto.</p>
                                        </div>
                                        <div class="col-lg-4">
                                            <asp:TextBox ID="TbQ9CAL" runat="server" Style="text-align: center; width: 100%; height: 75px" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-lg-12">
                                    <h3>Notas de liberación&nbsp<a href="#" title="Anota tus consideraciones:" data-toggle="popover" data-placement="top" data-content="Anota aquello que sea reseñable a la liberación, como si es una liberación tras parada larga, un elemento está degradado o si existe una oportunidad de mejora en el proceso."><span class="glyphicon glyphicon-question-sign"></span></a></h3>
                                    <div class="table-responsive">
                                        <table style="table-layout: fixed; width: 100%">
                                            <tr>
                                                <th style="width: 20%">
                                                    <asp:TextBox ID="TextBox11" runat="server" Style="text-align: center; width: 100%; height: 35px" Enabled="false" BackColor="Orange">Notas auditor:</asp:TextBox>
                                                </th>
                                                <th style="width: 60%">
                                                    <asp:TextBox ID="QXFeedbackCalidad" runat="server" Style="text-align: center; width: 100%; height: 35px" TextMode="MultiLine"></asp:TextBox>
                                                </th>
                                                <th style="width: 20%">
                                                    <button id="LiberarCalidad" runat="server" type="button" onserverclick="LiberarAUDCalidad" class="btn btn-success btn-md" style="text-align: center; width: 100%; height: 35px">Liberar (Calidad)</button>
                                                </th>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <br />




                        </div>

                    </div>
                    <%-- Cierro panel de calidad--%>
                    <%-- Panel de histórico--%>
                </div>
                <%-- Cierro panel de aguas--%>
            </div>

            <%--Cierro definición de tabs --%>
            <%--Pie de página común --%>
    </form>
</body>
</html>
