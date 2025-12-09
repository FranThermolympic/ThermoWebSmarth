<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="DocumentosGP12.aspx.cs" Inherits="ThermoWeb.DOCUMENTAL.DocumentosGP12"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Referencias y estado de revisión</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <script src="js/json2.js" type="text/javascript"></script>
    <link href="https://code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" rel="stylesheet" type="text/css">
    <script type="text/javascript" src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script type="text/javascript" src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
</head>
<body>
    <form id="form1" runat="server">

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
                        <li><a href="GestionDocumental.aspx">Gestión documental</a></li>
                        <li><a href="FichaReferencia.aspx">Fichas de referencia</a></li>
                    </ul>
                </div>
            </div>
        </nav>

        <ul class="nav nav-pills nav-justified">
            <li class="active"><a data-toggle="pill" href="#PAUTACONTROL">PAUTA DE CONTROL</a></li>
            <li><a data-toggle="pill" href="#ESTANDAR">ESTÁNDAR</a></li>
            <li><a data-toggle="pill" href="#DEFECTOLOGIA">DEFECTOLOGIA</a></li>
            <li><a data-toggle="pill" href="#IMAGENES">DEFECTOS GP12</a></li>
            <li><a data-toggle="pill" href="#EMBALAJE">EMBALAJE</a></li>
            <li><a data-toggle="pill" href="#NOCONFORMIDADES">NO CONFORMIDADES</a></li>
        </ul>
        <div class="tab-content">
            <div id="PAUTACONTROL" class="tab-pane fade in active">
                <div class="embed-responsive embed-responsive-16by9">
                    <iframe id="PautaControl" runat="server" class="embed-responsive-item" src="SinDocumentos.png" allowfullscreen></iframe>
                </div>
            </div>
            <div id="ESTANDAR" class="tab-pane fade">
                <div class="embed-responsive embed-responsive-16by9">
                    <iframe id="GP12" runat="server" class="embed-responsive-item" src="SinDocumentos.png" allowfullscreen></iframe>
                </div>
            </div>
            <div id="DEFECTOLOGIA" class="tab-pane fade">
                <div class="embed-responsive embed-responsive-16by9">
                    <iframe id="DEFECTOS" runat="server" class="embed-responsive-item" src="SinDocumentos.png"></iframe>
                </div>
            </div>
            <div id="IMAGENES" class="tab-pane fade">
                <div class="container">
                    <div id="myCarousel" class="carousel slide" data-ride="carousel">
                        <!-- Indicators -->
                        <ol class="carousel-indicators">
                            <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                            <li data-target="#myCarousel" data-slide-to="1"></li>
                            <li data-target="#myCarousel" data-slide-to="2"></li>
                            <li data-target="#myCarousel" data-slide-to="3"></li>
                            <li data-target="#myCarousel" data-slide-to="4"></li>
                            <li data-target="#myCarousel" data-slide-to="5"></li>
                            <li data-target="#myCarousel" data-slide-to="6"></li>
                            <li data-target="#myCarousel" data-slide-to="7"></li>
                            <li data-target="#myCarousel" data-slide-to="8"></li>
                            <li data-target="#myCarousel" data-slide-to="9"></li>
                        </ol>

                        <!-- Wrapper for slides -->
                        <div class="carousel-inner" id="ACTIVOS" runat="server">
                            <div class="item active">
                                <img src="http://facts4-srv/thermogestion/DOCUMENTAL/DOCUMENTOS/LAYOUTESTANDAR.png" style="width: 100%;">
                            </div>
                        </div>

                        <!-- Left and right controls -->
                        <a class="left carousel-control" href="#myCarousel" data-slide="prev">
                            <span class="glyphicon glyphicon-chevron-left"></span>
                            <span class="sr-only">Previous</span>
                        </a>
                        <a class="right carousel-control" href="#myCarousel" data-slide="next">
                            <span class="glyphicon glyphicon-chevron-right"></span>
                            <span class="sr-only">Next</span>
                        </a>
                    </div>
                </div>
            </div>


            <div id="EMBALAJE" class="tab-pane fade">
                <div class="embed-responsive embed-responsive-16by9">
                    <iframe id="PAUTAEMBALAJE" runat="server" class="embed-responsive-item" src="SinDocumentos.png" allowfullscreen></iframe>
                </div>
            </div>
            <div id="NOCONFORMIDADES" class="tab-pane fade">
                <div class="ROW">
                    <div class="col-lg-12">
                        <div class="col-lg-4">
                            <h1 style="color: red">&nbsp;&nbsp; Alerta de calidad</h1>
                            <h4>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Informe de incidencia de calidad</h4>
                        </div>
                        <div class="col-lg-5">
                        </div>
                        <div class="col-lg-3"></div>
                    </div>
                    <div class="col-lg-3"></div>
                    <div id="NOEXISTEALERTA" runat="server" class="col-lg-6">
                        <img src="http://facts4-srv/thermogestion/DOCUMENTAL/DOCUMENTOS/nc000.png" class="img-responsive" width="100%" />"
                    </div>
                    <div class="col-lg-3"></div>
                    <div id="EXISTEALERTA" class="tab-content" runat="server" visible="false">
                        <%-- Abro panel de parámetros--%>
                        <div id="INFO" class="tab-pane fade in active" runat="server">
                            <br />
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="col-lg-8">
                                            <div class="col-lg-12">
                                                <div id="Div4" class="panel panel-primary" runat="server">
                                                    <div class="panel-heading">
                                                        <h3 class="panel-title" style="color: white">
                                                            <i class="fa fa-list-ul fa-fw"></i>Detalles generales
                                <button id="Button1" type="button" runat="server" visible="false" class="btn btn-info btn-xs" onserverclick="CargaDatosReferencia" style="float: right">Cargar datos de referencia</button>
                                                        </h3>
                                                    </div>
                                                    <div id="cuerpo1" class="panel-body" runat="server">
                                                        <div class="row">
                                                            <div class="col-lg-12">
                                                                <div class="col-sm-3">
                                                                    <div class="thumbnail">
                                                                        <asp:HyperLink ID="hyperlink6" NavigateUrl="" ImageUrl="" Target="_new" runat="server" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-9">
                                                                    <div class="table-responsive">
                                                                        <table style="width: 100%">

                                                                            <tr>
                                                                                <th>
                                                                                    <asp:TextBox ID="tituloReferenciaCarga" runat="server" Style="text-align: center; width: 100%; background-color: orange" Enabled="false">Referencia</asp:TextBox>
                                                                                </th>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbReferenciaCarga" runat="server" Style="text-align: center; width: 100%" Enabled="false"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbMoldeCarga" runat="server" Style="text-align: center; width: 100%" Visible="false"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <asp:TextBox ID="tbDescripcionCarga" runat="server" Style="text-align: center; width: 100%" Enabled="false"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <th colspan="2">
                                                                                    <asp:TextBox ID="tbClienteCarga" runat="server" Style="text-align: center; width: 100%" Enabled="false"></asp:TextBox>
                                                                                </th>
                                                                                <th colspan="2">
                                                                                    <asp:TextBox ID="tbNumProveedor" runat="server" Style="text-align: center; width: 100%" Enabled="false" Visible="false"></asp:TextBox>
                                                                                </th>
                                                                            </tr>

                                                                        </table>
                                                                    </div>
                                                                    <br />
                                                                    <div class="table-responsive">
                                                                        <table style="width: 100%">
                                                                            <tr>
                                                                                <th colspan="3">
                                                                                    <asp:TextBox ID="tituloPILOTOS" runat="server" Style="text-align: center; width: 100%; background-color: orange" Enabled="false">PILOTOS ASIGNADOS</asp:TextBox>
                                                                                </th>
                                                                            </tr>
                                                                            <tr>
                                                                                <th>
                                                                                    <asp:TextBox ID="tituloPROD" runat="server" Style="text-align: center; width: 100%; background-color: lightgrey" Enabled="false">PRODUCCIÓN</asp:TextBox>
                                                                                </th>
                                                                                <th>
                                                                                    <asp:TextBox ID="tituloCAL" runat="server" Style="text-align: center; width: 100%; background-color: lightgrey" Enabled="false">CALIDAD</asp:TextBox>
                                                                                </th>
                                                                                <th>
                                                                                    <asp:TextBox ID="tituloING" runat="server" Style="text-align: center; width: 100%; background-color: lightgrey" Enabled="false">INGENIERÍA</asp:TextBox>
                                                                                </th>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:DropDownList ID="DropProduccion" runat="server" CssClass="form-control" Font-Size="Large" Enabled="false">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="DropCalidad" runat="server" CssClass="form-control" Font-Size="Large" Enabled="false">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="DropIngenieria" runat="server" CssClass="form-control" Font-Size="Large" Enabled="false">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>

                                                                        </table>
                                                                    </div>
                                                                    <br />
                                                                    <div class="table-responsive">
                                                                        <table style="width: 100%">
                                                                            <tr>
                                                                                <th>
                                                                                    <asp:TextBox ID="tituloFechaOriginal" runat="server" Style="text-align: center; width: 100%; background-color: orange" Enabled="false">Fecha original:</asp:TextBox>
                                                                                </th>

                                                                                <td>
                                                                                    <asp:TextBox ID="tbFechaOriginal" CssClass="textbox Add-text" runat="server" autocomplete="off" Style="text-align: center; width: 100%" Enabled="false"></asp:TextBox>
                                                                                </td>
                                                                                <th>
                                                                                    <asp:TextBox ID="tituloFechaRevision" runat="server" Style="text-align: center; width: 100%; background-color: orange" Enabled="false">Fecha revisión:</asp:TextBox>
                                                                                </th>

                                                                                <td>
                                                                                    <asp:TextBox ID="tbFechaRevision" runat="server" Style="text-align: center; width: 100%" Enabled="false"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <%--final de panel de descripción--%>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="col-lg-12">
                                                <div id="Div8" class="panel panel-danger" runat="server">
                                                    <div class="panel-heading" style="background-color: red">
                                                        <h3 class="panel-title" style="color: white">
                                                            <i class="fa fa-list-ul fa-fw"></i>No conformidad
                                <button id="Button2" type="button" runat="server" class="btn btn-info btn-xs" style="float: right" onserverclick="insertar_foto" visible="false">Subir imágenes</button>
                                                        </h3>
                                                    </div>

                                                    <div id="Div9" class="panel-body" runat="server" style="background-color: lightcoral; border-color: red">
                                                        <div class="col-lg-12">
                                                            <div class="col-sm-4" style="background-color: forestgreen">
                                                                <div class="col-sm-12">
                                                                    <label style="color: white; font-size: large; font-family: Calibri">CÓMO DEBERÍA SER:</label>
                                                                    <div class="thumbnail" style="border-color: forestgreen; border-width: thick">
                                                                        <asp:HyperLink ID="hyperlink1" NavigateUrl="" ImageUrl="" Target="_new" runat="server" />
                                                                    </div>
                                                                    <asp:FileUpload ID="FileUpload1" runat="server" Visible="false"></asp:FileUpload>
                                                                    <br />
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-8">
                                                                <div class="col-sm-6">
                                                                    <label style="color: white; font-size: large; font-family: Calibri">CÓMO ES 1:</label>
                                                                    <div class="thumbnail" style="border-color: red; border-width: thick">
                                                                        <asp:HyperLink ID="hyperlink2" NavigateUrl="" ImageUrl="" Target="_new" runat="server" />
                                                                    </div>
                                                                    <asp:FileUpload ID="FileUpload2" runat="server" Visible="false"></asp:FileUpload>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <label style="color: white; font-size: large; font-family: Calibri">CÓMO ES 2:</label>
                                                                    <div class="thumbnail" style="border-color: red; border-width: thick">
                                                                        <asp:HyperLink ID="hyperlink3" NavigateUrl="" ImageUrl="" Target="_new" runat="server" />
                                                                    </div>
                                                                    <asp:FileUpload ID="FileUpload3" runat="server" Visible="false"></asp:FileUpload>
                                                                </div>
                                                            </div>

                                                        </div>
                                                        <div class="col-lg-12">
                                                            <div class="col-lg-12">
                                                                <br />

                                                                <div class="col-sm-3">
                                                                    <label style="color: white; font-size: large; font-family: Calibri">ETIQUETA:</label>
                                                                    <div class="thumbnail">
                                                                        <asp:HyperLink ID="hyperlink4" NavigateUrl="" ImageUrl="" Target="_new" runat="server" />
                                                                    </div>
                                                                    <asp:FileUpload ID="FileUpload4" runat="server" Visible="false"></asp:FileUpload>
                                                                </div>
                                                                <div class="col-sm-1"></div>
                                                                <div class="col-sm-3">
                                                                    <label style="color: white; font-size: large; font-family: Calibri">ETIQUETA 2:</label>
                                                                    <div class="thumbnail">
                                                                        <asp:HyperLink ID="hyperlink5" NavigateUrl="" ImageUrl="" Target="_new" runat="server" />
                                                                    </div>
                                                                    <asp:FileUpload ID="FileUpload5" runat="server" Visible="false"></asp:FileUpload>
                                                                </div>
                                                                <div class="col-sm-1"></div>
                                                                <div class="col-sm-4">
                                                                    <label style="color: white; font-size: large; font-family: Calibri">TRAZABILIDAD:</label>
                                                                    <asp:DropDownList ID="DropCaja1" runat="server" CssClass="form-control" Font-Size="Large" AutoPostBack="True">
                                                                        <asp:ListItem Text="---" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:DropDownList ID="DropCaja2" runat="server" CssClass="form-control" Font-Size="Large" AutoPostBack="True">
                                                                        <asp:ListItem Text="---" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:DropDownList ID="DropCaja3" runat="server" CssClass="form-control" Font-Size="Large" AutoPostBack="True">
                                                                        <asp:ListItem Text="---" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:DropDownList ID="DropCaja4" runat="server" CssClass="form-control" Font-Size="Large" AutoPostBack="True">
                                                                        <asp:ListItem Text="---" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:TextBox ID="tbNotas" runat="server" Height="75px" Style="padding-left: 1em; text-align: left; width: 100%" TextMode="MultiLine"></asp:TextBox><br />
                                                                    <br />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <%--final de panel de descripción--%>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="col-lg-12">
                                                <div id="Div2" class="panel panel-primary" runat="server">
                                                    <div class="panel-heading">
                                                        <h3 class="panel-title">
                                                            <i class="fa fa-list-ul fa-fw"></i>Detalles reclamación</h3>


                                                    </div>
                                                    <div id="Div5" class="panel-body" runat="server">
                                                        <div class="row">
                                                            <div class="col-lg-12">
                                                                <div id="divsector" runat="server" visible="false">
                                                                    <asp:TextBox ID="tbSector" runat="server" BackColor="white" ForeColor="black" Font-Bold="TRUE" BorderColor="Black" BorderStyle="Groove" Enabled="false" Style="text-align: center; width: 100%"></asp:TextBox>
                                                                    <asp:TextBox ID="tbProdRepetitivo" runat="server" BackColor="Red" ForeColor="White" Font-Bold="TRUE" Enabled="false" Style="text-align: center; width: 100%" Visible="false">PRODUCTO RECURRENTE</asp:TextBox>
                                                                    <br />
                                                                    <br />
                                                                </div>
                                                                <div class="btn-group" style="width: 100%" runat="server" visible="false">
                                                                    <a href="http://facts4-srv/thermogestion/calidad/Alertas_Calidad.aspx" style="width: 33%" class="btn btn-primary btn-sm" role="button">Nueva alerta</a>
                                                                    <button type="button" style="width: 34%" class="btn btn-primary btn-sm" runat="server" onserverclick="GuardarAlerta">Guardar cambios</button>
                                                                    <button type="button" style="width: 33%" class="btn btn-primary btn-sm" runat="server" onserverclick="imprimir_alerta">Distribuir alerta</button>
                                                                </div>
                                                                <br />
                                                                <br />

                                                                <div>
                                                                    <asp:TextBox ID="tituloNumNoConformidad" runat="server" Font-Bold="true" Style="text-align: center; width: 35%; height: 30px; float: left; background-color: orange" Enabled="false">No conformidad:</asp:TextBox>
                                                                    <asp:TextBox ID="tbNoConformidad" runat="server" Font-Size="X-Large" Style="text-align: center; width: 65%; height: 30px" Enabled="false"></asp:TextBox>
                                                                    <br />
                                                                    <asp:TextBox ID="tituloNumNoConformidadCliente" runat="server" Font-Bold="true" Style="text-align: center; width: 35%; float: left; background-color: lightgray" Enabled="false">NC de cliente:</asp:TextBox>
                                                                    <asp:TextBox ID="tbNoConformidadCliente" runat="server" Style="text-align: center; width: 65%" Enabled="false"></asp:TextBox>



                                                                    <br />
                                                                    <br />
                                                                </div>
                                                                <div class="table-responsive">
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <th colspan="2">
                                                                                <asp:DropDownList ID="TipoAlerta" runat="server" CssClass="form-control" Font-Size="Large" Enabled="false" AutoPostBack="True">
                                                                                    <asp:ListItem Text="---" Value="0"></asp:ListItem>
                                                                                    <asp:ListItem Text="A proveedor" Value="1"></asp:ListItem>
                                                                                    <asp:ListItem Text="De cliente" Value="2"></asp:ListItem>
                                                                                    <asp:ListItem Text="Interna" Value="3"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </th>
                                                                        </tr>
                                                                        <tr>
                                                                            <th colspan="2">
                                                                                <asp:DropDownList ID="NivelAlerta" runat="server" CssClass="form-control" Font-Size="Large" Enabled="false" AutoPostBack="True">
                                                                                    <asp:ListItem Text="---" Value="0"></asp:ListItem>
                                                                                    <asp:ListItem Text="Q-Info" Value="1"></asp:ListItem>
                                                                                    <asp:ListItem Text="Reclamación oficial" Value="2"></asp:ListItem>
                                                                                    <asp:ListItem Text="No aceptada" Value="3"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </th>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                                <br />
                                                                <asp:TextBox ID="tituloProcesoAfectado" runat="server" Font-Bold="true" Style="text-align: center; width: 35%; height: 30px; float: left; background-color: lightgrey" Enabled="false">Proceso afectado:</asp:TextBox>
                                                                <asp:TextBox ID="tbProcesoAfectado" runat="server" Style="text-align: center; width: 65%; height: 30px" Enabled="false"></asp:TextBox>

                                                            </div>
                                                            <%--final de panel de descripción--%>
                                                            <div class="col-lg-12">
                                                                <br />
                                                                <div class="table-responsive">
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <th colspan="2">
                                                                                <asp:TextBox ID="TituloCantidadStock" runat="server" Style="text-align: center; width: 100%; background-color: orange; border-left-color: red; border-bottom-color: red; border-top-color: red" Enabled="false">Stock Thermolympic:</asp:TextBox>
                                                                            </th>
                                                                            <td colspan="2">
                                                                                <asp:TextBox ID="tbCantidadStock" runat="server" Style="text-align: center; width: 100%; border-right-color: red; border-bottom-color: red; border-top-color: red" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <th colspan="2">
                                                                                <asp:TextBox ID="TituloCantidad" runat="server" Style="text-align: center; width: 100%; background-color: lightgrey" Enabled="false">Cantidad reclamada:</asp:TextBox>
                                                                            </th>
                                                                            <td colspan="2">
                                                                                <asp:TextBox ID="tbCantidad" runat="server" Style="text-align: center; width: 100%" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <th colspan="2">
                                                                                <asp:TextBox ID="tituloPPM" runat="server" Style="text-align: center; width: 100%; background-color: lightgrey" Enabled="false">PPM de cliente:</asp:TextBox>
                                                                            </th>
                                                                            <td colspan="2">
                                                                                <asp:TextBox ID="tbPPM" runat="server" Style="text-align: center; width: 100%" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <th colspan="4">
                                                                                <asp:TextBox ID="TituloLotes" runat="server" Style="text-align: center; width: 100%; background-color: lightgrey" Enabled="false">Órdenes afectadas</asp:TextBox>
                                                                            </th>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:TextBox ID="tbLote1text" Visible="false" runat="server" Style="text-align: center; width: 100%" Enabled="false"></asp:TextBox>
                                                                                <asp:DropDownList ID="tbLote1" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="tbLote2text" Visible="false" runat="server" Style="text-align: center; width: 100%" Enabled="false"></asp:TextBox>
                                                                                <asp:DropDownList ID="tbLote2" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="tbLote3text" Visible="false" runat="server" Style="text-align: center; width: 100%" Enabled="false"></asp:TextBox>
                                                                                <asp:DropDownList ID="tbLote3" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="tbLote4text" Visible="false" runat="server" Style="text-align: center; width: 100%" Enabled="false"></asp:TextBox>
                                                                                <asp:DropDownList ID="tbLote4" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-12">
                                                <div id="Div6" class="panel panel-primary" runat="server">
                                                    <div class="panel-heading">
                                                        <h3 class="panel-title">
                                                            <i class="fa fa-list-ul fa-fw"></i>Informe de detección y contención</h3>
                                                    </div>
                                                    <div id="Div12" class="panel-body" runat="server">
                                                        <div class="row">
                                                            <div class="col-lg-12">


                                                                <label>Descripción del problema:</label>
                                                                <asp:TextBox ID="tbProblemaNC" runat="server" TextMode="MultiLine" Style="padding-left: 1em; width: 100%" Height="50px" Enabled="false"></asp:TextBox><br />
                                                                <br />
                                                                <label class="checkbox-inline">
                                                                    <input id="tbRepetitivoSN" runat="server" type="checkbox" value=""><b>Defecto recurrente (Marcar si aplica)</b></label>
                                                                <br />
                                                                <br />
                                                                <label>Contramedidas requerida:</label><br />
                                                                <asp:TextBox ID="tituloContramedidaPROD" runat="server" Font-Bold="true" Height="50px" Style="text-align: left; width: 25%; background-color: lightgrey; float: left" Enabled="false"> PRODUCCIÓN:</asp:TextBox>
                                                                <asp:TextBox ID="tbContramedidaPROD" runat="server" Height="50px" Style="padding-left: 1em; text-align: left; width: 75%" TextMode="MultiLine" Enabled="false"></asp:TextBox><br />
                                                                <asp:TextBox ID="tituloContramedidaCAL" runat="server" Font-Bold="true" Height="50px" Style="text-align: left; width: 25%; background-color: lightgrey; float: left" Enabled="false"> CALIDAD:</asp:TextBox>
                                                                <asp:TextBox ID="tbContramedidaCAL" runat="server" Height="50px" Style="padding-left: 1em; text-align: left; width: 75%" TextMode="MultiLine" Enabled="false"></asp:TextBox><br />
                                                                <asp:TextBox ID="tituloContramedidaING" runat="server" Font-Bold="true" Height="50px" Style="text-align: left; width: 25%; background-color: lightgrey; float: left" Enabled="false"> INGENIERÍA:</asp:TextBox>
                                                                <asp:TextBox ID="tbContramedidaING" runat="server" Height="50px" Style="padding-left: 1em; text-align: left; width: 75%" TextMode="MultiLine" Enabled="false"></asp:TextBox><br />
                                                                <br />
                                                                <label>Observaciones:</label>
                                                                <asp:TextBox ID="tbObservacionesNC" runat="server" Style="padding-left: 1em; text-align: left; width: 100%" Height="50px" TextMode="MultiLine" Enabled="false"></asp:TextBox>

                                                            </div>
                                                            <%--final de panel de descripción--%>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="col-lg-12">
                                                <div id="Div13" class="panel panel-primary" runat="server">
                                                    <div class="panel-heading">
                                                        <h3 class="panel-title">
                                                            <i class="fa fa-list-ul fa-fw"></i>Productos potencialmente afectados</h3>
                                                    </div>
                                                    <div id="Div14" class="panel-body" runat="server">
                                                        <div class="row">
                                                            <div class="col-lg-12">
                                                                <asp:GridView ID="dgv_afectadas" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                                                    Width="98.5%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false"
                                                                    EmptyDataText="There are no data records to display.">

                                                                    <EditRowStyle BackColor="#ffffcc" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Referencia" ItemStyle-Width="100px">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblReferenciaAFECTADA" runat="server" Text='<%#Eval("Referencia") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Descripcion" ItemStyle-Width="250px">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDescripcionAFECTADA" runat="server" Text='<%#Eval("Descripcion") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                            <%--final de panel de descripción--%>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div id="FORMACION" class="tab-pane fade" runat="server">
                            <br />
                            <input id="signatureJSON" type="hidden" name="signature" class="signature" value="" runat="server">
                            <div class="col-lg-12">
                                <div id="Div1" class="panel panel-primary" runat="server">
                                    <div class="panel-heading">
                                        <h3 class="panel-title">
                                            <i class="fa fa-list-ul fa-fw"></i>Distribución de alerta + Formación vinculada a Alerta</h3>
                                    </div>
                                    <div id="Div3" class="panel-body" runat="server">
                                        <asp:GridView ID="dgv_operarios_informados" runat="server" AllowSorting="True" margin="20px" Style="margin-left: 1%;"
                                            Width="98.5%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false"
                                            OnRowCancelingEdit="gridView_RowCancelingEdit" OnRowEditing="GridView_RowEditing" OnRowUpdating="GridView_RowUpdating" OnRowDataBound="GridView_RowDataBound"
                                            EmptyDataText="There are no data records to display.">
                                            <EditRowStyle BackColor="#ffffcc" />
                                            <HeaderStyle BackColor="#3366ff" ForeColor="White" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Nº" ItemStyle-Width="5%" ItemStyle-Font-Bold="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNOperario" runat="server" Text='<%#Eval("OPNUMERO") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="txtNOperario" runat="server" Text='<%#Eval("OPNUMERO") %>' />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nombre" ItemStyle-Width="35%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNombre" runat="server" Text='<%#Eval("OPNOMBRE") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="txtOperario" runat="server" Text='<%#Eval("OPNOMBRE") %>' />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Formador" ItemStyle-Width="25%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFormador" runat="server" Text='<%#Eval("FORMNOMBRE") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="txtFormador" runat="server" Style="text-align: center" CssClass="form-control"></asp:DropDownList>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fecha" ItemStyle-Width="25%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFechaFormador" runat="server" Text='<%#Eval("FECHA") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Firma" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Image ID="lblFirmaIMG" runat="server" Height="50px" ImageUrl='<%#Eval("FIRMA")  %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <div id="captureSignature"></div>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEdit2" runat="server" CssClass="btn btn-primary" Width="100%" CommandName="Edit">
                                                <span aria-hidden="true" class="glyphicon glyphicon-pencil"></span>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="btnUpdate" runat="server" CssClass="btn btn-success" Width="100%" CommandName="Update" OnClientClick="return confirm('¿Seguro que quieres modificar esta fila?');">
                                                <span aria-hidden="true" class="glyphicon glyphicon-ok"></span>
                                                        </asp:LinkButton>

                                                        <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-danger" Width="100%" CommandName="Cancel">
                                                <span aria-hidden="true" class="glyphicon glyphicon-remove"></span>
                                                        </asp:LinkButton>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="btnañadir" runat="server" CssClass="btn btn-primary" Width="100%" CommandName="AddNew">
                                                <span aria-hidden="true" class="glyphicon glyphicon-plus"></span>
                                                        </asp:LinkButton>
                                                    </FooterTemplate>

                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div id="EXISTEALERTATABS" runat="server" visible="false">
                        <ul class="nav nav-pills nav-justified">
                            <li class="active" id="tab0button" runat="server"><a data-toggle="pill" href="#INFO">Información principal</a></li>
                            <li id="tab1button" runat="server"><a data-toggle="pill" href="#FORMACION">Distribución y formación</a></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>




    </form>
</body>
</html>
