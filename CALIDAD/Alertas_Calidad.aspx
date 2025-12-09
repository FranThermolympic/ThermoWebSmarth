<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Alertas_Calidad.aspx.cs" Inherits="ThermoWeb.CALIDAD.ALERTASCALIDAD" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Alerta de calidad</title>
    <link rel="shortcut icon" type="image/x-icon" href="FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Alerta de calidad
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
    <ul class="navbar-nav me-auto mb-2 mb-lg-0">
        <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown2" role="button" data-bs-toggle="dropdown" aria-expanded="false">Consultas
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown2">
                <li><a class="dropdown-item" href="../CALIDAD/DashboardAlertasCalidad.aspx">Dashboard</a></li>
                <li><a class="dropdown-item" href="../GP12/GP12ReferenciasEstado.aspx">Estado de referencias en muro</a></li>
                <li><a class="dropdown-item" href="../KPI/KPI_NoConformidades.aspx">Indicadores</a></li>
                <li><hr class="dropdown-divider"></li>
                <li><a class="dropdown-item" href="../CALIDAD/ListaAlertasCalidad.aspx">Listado de alertas</a></li>
            </ul>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function ShowPopup() {
            document.getElementById("AUXMODALACCION").click();
        }
        function AbreContencion() {
            document.getElementById("BTNCONTENCION").click();
        }
        function AbreEvidencia() {
            document.getElementById("AUXMODALMSA2").click();
        }
        function AbreModalContencion() {
            document.getElementById("btnModalContencionAlm").click();
        }
    </script>

    <script type="text/javascript">
        function creado_OK() {
            alert('Se ha creado la alerta de calidad correctamente.');
        }
    </script>
    <script type="text/javascript">
        function falta_referencia() {
            alert("Error al guardar. ¡No hay ninguna referencia cargada!");
        }
    </script>
    <script type="text/javascript">
        function error_cargos() {
            alert("Error al guardar. Existen datos erróneos en la sección de cargos.");
        }
    </script>
    <script type="text/javascript">
        function DistribuirMOD() {
            if (confirm('Alerta guardada correctamente. ¿Quieres distribuirla por e-mail?')) {
                jQuery("[ID$=btnDistAlarma]").click();
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.Add-text').datepicker({

                dateFormat: 'dd/mm/yy',
                inline: true,
                showOtherMonths: true,
                changeMonth: true,
                changeYear: true,
                constrainInput: true,
                firstDay: 1,
                navigationAsDateFormat: true,

                yearRange: "c-20:c+10",
                dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sa']
            });
        });
    </script>
    <style>
        .tab-content {
            width: 100%;
        }
    </style>
    <div style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-4">
                    <label class="ms-3 h1" style="color: red">Alerta de calidad</label>
                    <h5 class="ms-4">Informe de incidencia de calidad</h5>
                </div>
                <div class="col-lg-5">
                    <button type="button" style="width: 25%" class="btn btn-secondary btn-lg" runat="server" onserverclick="ForzarOperarios" visible="FALSE">ForzarOperarios</button>
                            
                </div>
                <div class="col-lg-3">
                    <div class="card-header mt-2 me-4 bg-white shadow">
                        <asp:TextBox ID="tbNoConformidadTEXT" CssClass="form-control rounded rounded-2 border-secondary border-1 bg-transparent" runat="server" Font-Size="XX-Large" Font-Bold="true" Font-Italic="true" Style="text-align: center; width: 100%; height: 40px" Enabled="false"></asp:TextBox>
                        <asp:TextBox ID="tbNoConformidad" runat="server" Font-Size="XX-Large" Style="text-align: center; width: 100%; height: 40px" Visible="false"></asp:TextBox>
                        <div class="btn-group" style="width: 100%">
                            <a href="http://facts4-srv/thermogestion/calidad/Alertas_Calidad.aspx" style="width: 25%" class="btn btn-primary btn-lg" role="button"><i class="bi bi-file-earmark-plus"></i></a>
                            <button type="button" style="width: 25%" class="btn btn-secondary btn-lg" runat="server" onserverclick="Imprimir_alerta"><i class="bi bi-printer"></i></button>
                            <button id="btnDistAlarma" type="button" style="width: 25%" class="btn btn-orange btn-lg" runat="server" onserverclick="MandarMail"><i class="bi bi-envelope-check"></i></button>
                            <button type="button" style="width: 25%" class="btn btn-success btn-lg" runat="server" onserverclick="GuardarAlerta"><i class="bi bi-sd-card"></i></button>
                        </div>
                    </div>

                </div>
            </div>
            <div class="row">
                <div class="d-flex align-items-start">
                    <div class="nav flex-column nav-pills me-3 mt-1 " id="pills-tab" role="tablist">
                        <button class="nav-link shadow  border border-secondary active" id="BTNALERTA" runat="server" data-bs-toggle="pill" data-bs-target="#TABALERTA" type="button" role="tab" aria-controls="pills-home" aria-selected="true" style="font-weight: bold; width: 100%">ALERTA</button>
                        <label enabled="false" class="mt-2" type="button" style="font-weight: bold; width: 100%"><i class="bi bi-bookmarks">DOCUMENTOS</i></label>
                        <button class="nav-link shadow  border border-secondary ms-1 me-1" style="font-weight: bold; width: 95%" id="BTNCONTENCION" runat="server" data-bs-toggle="pill" data-bs-target="#TABCONTENCION" type="button" role="tab" aria-controls="BTNCONTENCION" aria-selected="false">CONTENCIÓN</button>
                        <button class="nav-link shadow  border border-secondary ms-1 me-1" style="font-weight: bold; width: 95%" id="BTNACCIONES" runat="server" data-bs-toggle="pill" data-bs-target="#TABPDCA" type="button" role="tab" aria-controls="BTNACCIONES" aria-selected="false">ACCIONES</button>
                        <button class="nav-link  shadow  border border-secondary ms-1 me-1" style="font-weight: bold; width: 95%" id="BTNDISTRIBUCION" runat="server" data-bs-toggle="pill" data-bs-target="#TABDISTRIBUCION" type="button" role="tab" aria-controls="TABDISTRIBUCION" aria-selected="true">DISTRIBUCIÓN</button>
                        <asp:Image ID="CLIENTE" runat="server" CssClass="mt-2" ImageUrl="" Width="130px" />
                    </div>
                    <div class="tab-content">
                        <div id="TABALERTA" class="tab-pane fade show active" runat="server">
                             <button id="Button8" type="button" runat="server" class="btn btn-secondary btn-sm border border-dark" onserverclick="AuxiliarCuarentena" style="float: right" visible="false">cuarentena</button>
                            <button id="Button9" type="button" runat="server" class="btn btn-secondary btn-sm border border-dark" onserverclick="AuxiliarGP12" style="float: right" visible="false" >GP12 estado</button>
                                        
                            <div class="row" style="width: 100%">
                                <div class="col-lg-8">
                                    <div class="card mt-2 border border-dark  shadow shadow-lg">
                                        <div class="card-header text-bg-primary">
                                            <label class="h5"><i class="bi bi-journals me-2"></i>Detalles generales</label>
                                            <button id="Button3" type="button" runat="server" class="btn btn-secondary btn-sm border border-dark" onserverclick="CargaDatosReferencia" style="float: right">Cargar datos de referencia</button>
                                        </div>
                                        <div class="card-body" style="background-color: #eeeeee">
                                            <div class="row">
                                                <div class="col-lg-3">
                                                    <asp:HyperLink ID="hyperlink6" NavigateUrl="" Width="100%" ImageWidth="100%" ImageUrl="http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg" Target="_new" runat="server" class="rounded img-thumbnail img-fluid shadow" />
                                                </div>
                                                <div class="col-lg-9">
                                                    <asp:TextBox ID="tbMoldeCarga" runat="server" Style="text-align: center; width: 100%" Visible="false"></asp:TextBox>
                                                    <div class="table table-responsive rounded rounded-2 shadow">
                                                        <table width="100%">
                                                            <tr style="background-color: orange; border-color: orange">
                                                                <td class="border border-secondary">
                                                                    <asp:Label ID="tituloReferenciaCarga" Width="100%" Font-Bold="true" runat="server" Style="text-align: center; border-color: transparent; background-color: transparent" Enabled="false" Text="Referencia:" />
                                                                </td>
                                                                <td class="border border-secondary bg-white">
                                                                    <asp:TextBox ID="tbReferenciaCarga" Width="100%" runat="server" Style="text-align: center; border-color: transparent; background-color: transparent"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr style="background-color: white; border-color: orange">
                                                                <td colspan="2" class="border border-secondary border-opacity-50 bg-white">
                                                                    <asp:Label ID="tbDescripcionCarga" Width="100%" Font-Italic="true" Font-Bold="true" runat="server" Style="text-align: center; border-color: transparent; background-color: transparent" Text="-" />
                                                                </td>
                                                            </tr>
                                                            <tr style="background-color: white; border-color: orange">
                                                                <td colspan="2" class="border border-secondary border-opacity-50 bg-white">
                                                                    <asp:Label ID="tbClienteCarga" Width="100%" runat="server" Font-Bold="true" Style="text-align: center; border-color: transparent; background-color: transparent" Text="-"></asp:Label>
                                                                    <asp:Label ID="tbNumProveedor" Width="100%" runat="server" Font-Bold="true" Style="text-align: center; border-color: transparent; background-color: transparent" Text="-" Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>

                                                        </table>
                                                    </div>
                                                    <div class="table table-responsive rounded rounded-2 shadow">
                                                        <table width="100%">
                                                            <tr style="background-color: orange; border-color: orange">
                                                                <th colspan="3" class="border border-secondary">
                                                                    <asp:Label ID="tituloPILOTOS" runat="server" Width="100%" Font-Bold="true" Style="text-align: center; border-color: transparent; background-color: transparent" Enabled="false">PILOTOS ASIGNADOS</asp:Label>
                                                                </th>
                                                            </tr>
                                                            <tr style="background-color: lightgrey; border-color: lightgrey">
                                                                <th class="border border-secondary">
                                                                    <asp:Label ID="tituloPROD" runat="server" Width="100%" Style="text-align: center; border-color: transparent; background-color: transparent">PRODUCCIÓN</asp:Label>
                                                                </th>
                                                                <th class="border border-secondary">
                                                                    <asp:Label ID="tituloCAL" runat="server" Width="100%" Style="text-align: center; border-color: transparent; background-color: transparent">CALIDAD</asp:Label>
                                                                </th>
                                                                <th class="border border-secondary">
                                                                    <asp:Label ID="tituloING" runat="server" Width="100%" Style="text-align: center; border-color: transparent; background-color: transparent">INGENIERÍA</asp:Label>
                                                                </th>
                                                            </tr>
                                                            <tr style="background-color: white; border-color: white">
                                                                <th class="border border-secondary">
                                                                    <asp:DropDownList ID="DropProduccion" runat="server" CssClass="form-select border border-0" Font-Bold="true">
                                                                    </asp:DropDownList>
                                                                </th>
                                                                <th class="border border-secondary">
                                                                    <asp:DropDownList ID="DropCalidad" runat="server" CssClass="form-select border border-0" Font-Bold="true">
                                                                    </asp:DropDownList>
                                                                </th>
                                                                <th class="border border-secondary">
                                                                    <asp:DropDownList ID="DropIngenieria" runat="server" CssClass="form-select border border-0" Font-Bold="true">
                                                                    </asp:DropDownList>
                                                                </th>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div class="table table-responsive rounded rounded-2 shadow">
                                                        <table width="100%">
                                                            <tr style="background-color: orange; border-color: orange">
                                                                <td class="border border-secondary">
                                                                    <asp:Label ID="tituloFechaOriginal" Width="100%" Font-Bold="true" runat="server" Style="text-align: center; border-color: transparent; background-color: transparent" Enabled="false">Fecha original:</asp:Label>
                                                                </td>
                                                                <td class="border border-secondary bg-white">
                                                                    <asp:TextBox ID="tbFechaOriginal" Width="100%" CssClass="textbox Add-text" runat="server" autocomplete="off" Style="text-align: center; border-color: transparent; background-color: transparent"></asp:TextBox>
                                                                </td>
                                                                <td class="border border-secondary">
                                                                    <asp:Label ID="tituloFechaRevision" Width="100%" Font-Bold="true" runat="server" Style="text-align: center; border-color: transparent; background-color: transparent" Enabled="false">Fecha revisión:</asp:Label>
                                                                </td>
                                                                <td class="border border-secondary border-opacity-50 bg-white">
                                                                    <asp:TextBox ID="tbFechaRevision" Width="100%" runat="server" Style="text-align: center; border-color: transparent; background-color: #F2F3F4" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card mt-2 border border-dark  shadow shadow-lg">
                                        <div class="card-header text-bg-danger">
                                            <label class="h5"><i class="bi bi-shield-exclamation me-2"></i>No conformidad</label>
                                        </div>
                                        <ul class="nav nav-tabs shadow" style="background-color: lightgrey" id="myTab" role="tablist">
                                            <li class="nav-item" role="presentation">
                                                <button class="nav-link active border border-secondary" id="home-tab" data-bs-toggle="tab" data-bs-target="#home-tab-pane" type="button" role="tab" aria-controls="home-tab-pane" aria-selected="true" style="font-weight: bold">AYUDA VISUAL</button>
                                            </li>
                                            <li class="nav-item" role="presentation">
                                                <button class="nav-link border border-secondary" id="profile-tab" data-bs-toggle="tab" data-bs-target="#profile-tab-pane" type="button" role="tab" aria-controls="profile-tab-pane" aria-selected="false" style="font-weight: bold">TRAZABILIDAD</button>
                                            </li>
                                            <li class="nav-item" role="presentation">
                                                <button class="nav-link border border-secondary" id="profile-tab2" data-bs-toggle="tab" data-bs-target="#profile-tab-pane2" type="button" role="tab" aria-controls="profile-tab-pane" aria-selected="false" style="font-weight: bold">HITOS Y EVIDENCIAS</button>
                                            </li>
                                            <li class="nav-item" role="presentation">
                                                <button class="nav-link border border-secondary" id="profile-tab3" data-bs-toggle="tab" data-bs-target="#profile-tab-pane3" type="button" role="tab" aria-controls="profile-tab-pane" aria-selected="false" style="font-weight: bold">CARGOS Y COSTES</button>
                                            </li>
                                        </ul>
                                        <div class="tab-content" id="myTabContent">
                                            <div class="tab-pane fade show active border border-secondary" id="home-tab-pane" role="tabpanel" aria-labelledby="home-tab" tabindex="0" style="background-color: #eeeeee">
                                                <div class="card-body">
                                                    <div class="row">
                                                        <div class="col-lg-4  rounded rounded-2" style="background-color: lightgreen">
                                                            <div class="bg-success border border-1 border-white rounded mt-3 ms-2 me-2 shadow">
                                                                <div>
                                                                    <label class="ms-2 mt-1" style="color: white; font-family: Calibri; font-size: x-large; font-weight: bold">DEBERÍA SER:</label>
                                                                    <button id="Button2" type="button" runat="server" class="btn btn-secondary border border-dark me-2 mt-1" style="float: right" onserverclick="Insertar_documento"><i class="bi bi-upload"></i></button>
                                                                </div>
                                                                <div class="me-2 ms-2">
                                                                    <asp:HyperLink ID="hyperlink1" NavigateUrl="" Width="100%" ImageWidth="100%" ImageUrl="http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg" Target="_new" runat="server" class="rounded img-thumbnail img-fluid shadow" />
                                                                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control mb-3"></asp:FileUpload>
                                                                </div>
                                                            </div>
                                                            <br />
                                                        </div>
                                                        <div class="col-lg-4" style="background-color: indianred">
                                                            <div class="bg-danger border border-1 border-white rounded mt-3 ms-2 me-2 shadow">
                                                                <div>
                                                                    <label class="ms-2 mt-1" style="color: white; font-family: Calibri; font-size: x-large; font-weight: bold">CÓMO ES:</label>
                                                                    <button id="Button1" type="button" runat="server" class="btn btn-secondary border border-dark me-2 mt-1" style="float: right" onserverclick="Insertar_documento"><i class="bi bi-upload"></i></button>
                                                                </div>
                                                                <div class="me-2 ms-2">
                                                                    <asp:HyperLink ID="hyperlink2" NavigateUrl="" Width="100%" ImageWidth="100%" ImageUrl="http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg" Target="_new" runat="server" class="rounded img-thumbnail img-fluid shadow" />
                                                                    <asp:FileUpload ID="FileUpload2" runat="server" CssClass="form-control mb-3"></asp:FileUpload>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4" style="background-color: indianred">
                                                            <div class="bg-danger border border-1 border-white rounded mt-3 ms-2 me-2 shadow">
                                                                <div>
                                                                    <label class="ms-2 mt-1" style="color: white; font-family: Calibri; font-size: x-large; font-weight: bold">CÓMO ES:</label>
                                                                    <button id="Button4" type="button" runat="server" class="btn btn-secondary border border-dark me-2 mt-1" style="float: right" onserverclick="Insertar_documento"><i class="bi bi-upload"></i></button>
                                                                </div>
                                                                <div class="me-2 ms-2">
                                                                    <asp:HyperLink ID="hyperlink3" NavigateUrl="" Width="100%" ImageWidth="100%" ImageUrl="http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg" Target="_new" runat="server" class="rounded img-thumbnail img-fluid shadow" />
                                                                    <asp:FileUpload ID="FileUpload3" runat="server" CssClass="form-control mb-3"></asp:FileUpload>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="tab-pane fade border border-secondary" id="profile-tab-pane" role="tabpanel" aria-labelledby="profile-tab" tabindex="0" style="background-color: #eeeeee">
                                                <div class="card-body">
                                                    <div class="row">
                                                        <div class="col-sm-4">
                                                            <div>
                                                                <label class="ms-2 mt-1" style="font-family: Calibri; font-size: x-large; font-weight: bold">ETIQUETA:</label>
                                                                <button id="Button5" type="button" runat="server" class="btn btn-secondary border border-dark me-2 mt-1" style="float: right" onserverclick="Insertar_documento"><i class="bi bi-upload"></i></button>
                                                            </div>
                                                            <div>
                                                                <asp:HyperLink ID="hyperlink4" NavigateUrl="" Width="100%" ImageWidth="100%" ImageUrl="http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg" Target="_new" runat="server" class="rounded img-thumbnail img-fluid shadow" />
                                                                <asp:FileUpload ID="FileUpload4" runat="server" CssClass="form-control mb-3"></asp:FileUpload>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <div>
                                                                <label class="ms-2 mt-1" style="font-family: Calibri; font-size: x-large; font-weight: bold">ETIQUETA:</label>
                                                                <button id="Button6" type="button" runat="server" class="btn btn-secondary border border-dark me-2 mt-1" style="float: right" onserverclick="Insertar_documento"><i class="bi bi-upload"></i></button>
                                                            </div>
                                                            <div>
                                                                <asp:HyperLink ID="hyperlink5" NavigateUrl="" Width="100%" ImageWidth="100%" ImageUrl="http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg" Target="_new" runat="server" class="rounded img-thumbnail img-fluid shadow" />
                                                                <asp:FileUpload ID="FileUpload5" runat="server" CssClass="form-control mb-3"></asp:FileUpload>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <label class="ms-2 mt-1" style="font-family: Calibri; font-size: x-large; font-weight: bold">TRAZABILIDAD:</label>
                                                            <asp:DropDownList ID="DropCaja1" runat="server" CssClass="form-select border border-secondary shadow" Font-Size="Large" Font-Bold="true" >
                                                                <asp:ListItem Text="---" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="DropCaja2" runat="server" CssClass="form-select border border-secondary shadow" Font-Size="Large" Font-Bold="true">
                                                                <asp:ListItem Text="---" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="DropCaja3" runat="server" CssClass="form-select border border-secondary shadow" Font-Size="Large" Font-Bold="true" >
                                                                <asp:ListItem Text="---" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="DropCaja4" runat="server" CssClass="form-select border border-secondary shadow" Font-Size="Large" Font-Bold="true" >
                                                                <asp:ListItem Text="---" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="tbNotas" CssClass="form-control  border border-secondary shadow" runat="server" Height="75px" Style="padding-left: 1em; text-align: left; width: 100%" TextMode="MultiLine"></asp:TextBox><br />
                                                            <br />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="tab-pane fade border border-secondary" id="profile-tab-pane2" role="tabpanel" aria-labelledby="profile-tab" tabindex="0" style="background-color: #eeeeee">
                                                <div class="container-fluid">
                                                    <div class="row">
                                                        <div class="col-lg-6">
                                                            <h5 class="mt-2 fw-bold"><i class="bi bi-calendar2-date me-2"></i>Hitos con cliente:</h5>
                                                            <div class="form-check">
                                                                <input class="form-check-input" type="checkbox" value="" id="flexCheckChecked" checked disabled="disabled">
                                                                <label style="font-weight: bold">
                                                                    D3 / Punto de corte:
                                                                </label>
                                                            </div>
                                                            <div class="input-group shadow shadow-sm mb-3">
                                                                <div class="form-floating">
                                                                    <input type="text" class="form-control Add-text" id="D3Prevision" runat="server" value="-" disabled>
                                                                    <label for="floatingInput">Fecha prevista</label>
                                                                </div>
                                                                <div class="form-floating ">
                                                                    <input type="text" class="form-control Add-text" id="D3FechaReal" runat="server" value="-" disabled>
                                                                    <label for="floatingInput">Fecha real</label>
                                                                </div>
                                                            </div>
                                                            <div class="form-check">
                                                                <input class="form-check-input" type="checkbox" value="" id="Check_D6" runat="server" checked>
                                                                <label style="font-weight: bold">
                                                                    D6:
                                                                </label>
                                                            </div>
                                                            <div class="input-group shadow shadow-sm mb-3">
                                                                <div class="form-floating">
                                                                    <input type="text" class="form-control Add-text" id="D6Prevision" runat="server" value="-">

                                                                    <label for="floatingInput">Fecha prevista</label>
                                                                </div>
                                                                <div class="form-floating">
                                                                    <input type="text" class="form-control Add-text" id="D6FechaReal" runat="server" value="-" disabled>
                                                                    <label for="floatingInput">Fecha real</label>
                                                                </div>
                                                            </div>
                                                            <div class="form-check">
                                                                <input class="form-check-input" type="checkbox" value="" id="Check_D8" runat="server" checked>
                                                                <label style="font-weight: bold">
                                                                    D8:
                                                                </label>
                                                            </div>
                                                            <div class="input-group shadow shadow-sm mb-3">
                                                                <div class="form-floating">
                                                                    <input type="text" class="form-control Add-text" id="D8Prevision" runat="server" value="-">
                                                                    <label for="floatingInput">Fecha prevista</label>
                                                                </div>
                                                                <div class="form-floating">
                                                                    <input type="text" class="form-control Add-text" id="D8FechaReal" runat="server" value="-" disabled>
                                                                    <label for="floatingInput">Fecha real</label>
                                                                </div>
                                                            </div>

                                                        </div>
                                                        <div class="col-lg-6">
                                                            <h5 class="mt-2 fw-bold"><i class="bi bi-card-checklist me-2"></i>Evidencias:</h5>
                                                            <button id="AUXMODALMSA" runat="server" type="button" class="btn btn-primary btn-sm" data-bs-toggle="modal" data-bs-target="#ModalMSAGestion" style="width: 100%" visible="false">Agregar documento</button>
                                                            <button id="AUXMODALMSA2" runat="server" type="button" class="btn btn-primary btn-sm" data-bs-toggle="modal" data-bs-target="#ModalMSAGestion" style="width: 100%" hidden="hidden">Agregar documento</button>

                                                            <asp:GridView ID="dgvEvidencias" Width="100%" OnRowCommand="GridView_RowCommand"
                                                                runat="server" AutoGenerateColumns="false" CssClass="table table-responsive shadow p-3 rounded border-top-0" BorderColor="black">
                                                                <HeaderStyle CssClass="card-header" BackColor="#0d6efd" Font-Bold="True" ForeColor="White" HorizontalAlign="left" Height="40px" />
                                                                <RowStyle BackColor="White" />
                                                                <AlternatingRowStyle BackColor="#eeeeee" />
                                                                <EditRowStyle BackColor="#ffffcc" />
                                                                <Columns>
                                                                    <asp:TemplateField ItemStyle-VerticalAlign="Middle" ItemStyle-Font-Bold="true" ItemStyle-BackColor="#e6e6e6">
                                                                        <HeaderTemplate>
                                                                            <asp:LinkButton ID="btnañadirEvidencia" runat="server" CssClass="btn btn-outline-dark btn-sm bg-white shadow" CommandName="NuevoEvidencia">
                                                                            <i class="bi bi-plus-square"></i>
                                                                            </asp:LinkButton>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-outline-dark btn-sm shadow" CommandName="Bajar_evidencia" CommandArgument='<%#Eval("URLDocumento") %>'>
                                                                <i class="bi bi-file-post"></i>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField ItemStyle-BackColor="#e6e6e6">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTipoEvidencia" runat="server" Font-Bold="true" Text='<%#Eval("TipoEvidencia") %>' />
                                                                            <asp:Label ID="IdRev" runat="server" Visible="false" Text='<%#Eval("Id") %>' />
                                                                            <asp:Label ID="IdNConformidad" runat="server" Visible="false" Text='<%#Eval("NC") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDescEvidencia" runat="server" Text='<%#Eval("Descripcion")  %>' />
                                                                            <asp:Label ID="lblFormador" CssClass="ms-1" Font-Size="Small" Font-Italic="true" runat="server" Text='<%#"(" + Eval("FechaSubida","{0:dd/MM/yyyy}") + ")" %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="Middle">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="btnEdit2" runat="server" CssClass="btn btn-danger btn-sm shadow" CommandName="Eliminar_evidencia" CommandArgument='<%#Eval("Id") %>'>
                                                                            <i class="bi bi-trash"></i>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="tab-pane fade border border-secondary" id="profile-tab-pane3" role="tabpanel" aria-labelledby="profile-tab" tabindex="0" style="background-color: #eeeeee">
                                                <div class="container-fluid">
                                                    <div class="row">
                                                        <div class="col-lg-6">
                                                            <h5 for="CostesExternos" class="mt-2 fw-bold"><i class="bi bi-currency-euro me-2"></i>Costes externos:</h5>
                                                            <div class="input-group rounded rounded-1 shadow shadow-sm ">
                                                                <span class="input-group-text fw-bold fst-italic " style="width: 45%">Selección:</span>
                                                                <input type="text" runat="server" id="TbSeleccionExterno" class="form-control">
                                                                <span class="input-group-text">€</span>
                                                            </div>
                                                            <div class="input-group shadow shadow-sm">
                                                                <span class="input-group-text fw-bold fst-italic " style="width: 45%">Piezas NOK:</span>
                                                                <input type="text" runat="server" id="TbPiezasNOKExterno" class="form-control">
                                                                <span class="input-group-text">€</span>
                                                            </div>
                                                            <div class="input-group shadow shadow-sm">
                                                                <span class="input-group-text fw-bold fst-italic " style="width: 45%">Cargos:</span>
                                                                <input type="text" runat="server" id="TbCargosExterno" class="form-control">
                                                                <span class="input-group-text">€</span>
                                                            </div>
                                                            <div class="input-group shadow shadow-sm mb-4 ">
                                                                <span class="input-group-text fw-bold fst-italic " style="width: 45%">Administrativos:</span>
                                                                <input type="text" runat="server" id="TbAdministrativosExterno" class="form-control">
                                                                <span class="input-group-text">€</span>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6">
                                                            <h5 for="CostesSeleccion" class="mt-2 fw-bold"><i class="bi bi-currency-euro me-2"></i>Costes internos:</h5>
                                                            <div class="input-group shadow shadow-sm">
                                                                <span class="input-group-text fw-bold fst-italic" style="width: 45%">Selección:</span>
                                                                <input type="text" runat="server" id="tbSeleccionInterno" class="form-control">
                                                                <span class="input-group-text">€</span>
                                                            </div>
                                                            <div class="input-group shadow shadow-sm">
                                                                <span class="input-group-text fw-bold fst-italic" style="width: 45%">Otros:</span>
                                                                <input type="text" runat="server" id="tbOtrosInterno" class="form-control">
                                                                <span class="input-group-text">€</span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-lg-4">
                                    <div class="card mt-2 border border-dark shadow shadow-lg">
                                        <h5 class="card-header text-bg-primary"><i class="bi bi-journals me-2"></i>Detalles reclamación</h5>
                                        <div class="card-body " style="background-color: #eeeeee">
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <div id="divsector" class="mb-3" runat="server" visible="false">
                                                        <asp:TextBox ID="tbSector" CssClass="rounded rounded-1" runat="server" BackColor="white" ForeColor="black" Font-Bold="TRUE" BorderColor="Black" BorderStyle="Groove" Enabled="false" Style="text-align: center; width: 100%"></asp:TextBox>
                                                        <asp:TextBox ID="tbProdRepetitivo" CssClass="rounded rounded-1" runat="server" BackColor="Red" ForeColor="White" Font-Bold="TRUE" Enabled="false" Style="text-align: center; width: 100%" Visible="false">PRODUCTO RECURRENTE</asp:TextBox>
                                                    </div>

                                                    <div class="input-group mb-3 rounded rounded-2 border border-secondary border-1 shadow" style="background: lightgrey">
                                                        <asp:TextBox ID="tituloNumNoConformidadCliente" runat="server" Width="35%" Font-Bold="true" ForeColor="Black" Style="text-align: center; vertical-align: middle; border-color: transparent; background-color: transparent" Enabled="false">NC de cliente:</asp:TextBox>
                                                        <asp:TextBox ID="tbNoConformidadCliente" CssClass="form-control" runat="server" Style="text-align: center; width: 65%; height: 30px"></asp:TextBox>
                                                    </div>
                                                    <div class="input-group mt-3  shadow">
                                                        <div class="form-floating">
                                                            <asp:DropDownList ID="TipoAlerta" runat="server" class="form-select border border-secondary border-1" Font-Size="Large" Font-Bold="true" >
                                                                <asp:ListItem Text="---" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="A proveedor" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="De cliente" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="Interna" Value="3"></asp:ListItem>
                                                                <asp:ListItem Text="Logística" Value="4"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <label for="TipoAlerta" style="font-size: small">Tipo de Alerta</label>
                                                        </div>
                                                        <div class="form-floating">
                                                            <asp:DropDownList ID="NivelAlerta" runat="server" class="form-select border border-secondary border-1" Font-Bold="true" Font-Size="Large" >
                                                                <asp:ListItem Text="---" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="Q-Info" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Reclamación oficial" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="No aceptada" Value="3"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <label for="NivelAlerta" style="font-size: small">Escalado</label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-12 mt-3">
                                                    <div class="table table-responsive rounded rounded-2 shadow">
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td class="border border-secondary" colspan="4">
                                                                    <div class="input-group" style="background: lightgrey">
                                                                        <asp:TextBox ID="tituloProcesoAfectado" runat="server" Width="35%" Font-Bold="true" ForeColor="Black" Style="text-align: center; vertical-align: middle; border-color: transparent; background-color: transparent" Enabled="false">Proceso afectado:</asp:TextBox>
                                                                        <asp:TextBox ID="tbProcesoAfectado" CssClass="form-control border border-white" runat="server" Style="text-align: center; width: 65%; height: 30px"></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="border border-secondary" colspan="4" style="background: lightgrey">
                                                                    <asp:TextBox ID="TituloLotes" runat="server" Width="100%" Font-Bold="true" ForeColor="Black" Style="text-align: center; vertical-align: middle; border-color: transparent; background-color: transparent" Enabled="false">Órdenes afectadas</asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr style="background-color: white; border-color: white">
                                                                <th class="border border-secondary">
                                                                    <asp:TextBox ID="tbLote1text" Visible="false" CssClass="form-control border border-0" runat="server" Style="text-align: center; width: 100%"></asp:TextBox>
                                                                    <asp:DropDownList ID="tbLote1" runat="server" CssClass="form-select border border-0" Font-Bold="true"></asp:DropDownList>
                                                                </th>
                                                                <th class="border border-secondary">
                                                                    <asp:TextBox ID="tbLote2text" Visible="false" CssClass="form-control border border-0" runat="server" Style="text-align: center; width: 100%"></asp:TextBox>
                                                                    <asp:DropDownList ID="tbLote2" runat="server" CssClass="form-select border border-0" Font-Bold="true"></asp:DropDownList>
                                                                </th>
                                                                <th class="border border-secondary">
                                                                    <asp:TextBox ID="tbLote3text" Visible="false" CssClass="form-control border border-0" runat="server" Style="text-align: center; width: 100%"></asp:TextBox>
                                                                    <asp:DropDownList ID="tbLote3" runat="server" CssClass="form-select border border-0" Font-Bold="true"></asp:DropDownList>
                                                                </th>
                                                                <th class="border border-secondary">
                                                                    <asp:TextBox ID="tbLote4text" Visible="false" CssClass="form-control border border-0" runat="server" Style="text-align: center; width: 100%"></asp:TextBox>
                                                                    <asp:DropDownList ID="tbLote4" runat="server" CssClass="form-select border border-0" Font-Bold="true"></asp:DropDownList>
                                                                </th>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div class="input-group  rounded rounded-2 border border-danger border-2 shadow" hidden="hidden" style="background-color: orange; vertical-align: middle">
                                                        <asp:TextBox ID="TituloCantidadStock" Width="50%" ForeColor="Black" Font-Bold="true" runat="server" Style="text-align: center; vertical-align: middle; border-color: transparent; background-color: transparent" Enabled="false" Text="Stock Thermolympic:"></asp:TextBox>
                                                        <asp:TextBox ID="tbCantidadStock" CssClass="form-control" runat="server" Font-Size="X-Large" Style="text-align: center; width: 50%; height: 30px" Enabled="false"></asp:TextBox>

                                                    </div>
                                                    <div class="input-group rounded rounded-2 border border-secondary border-1 shadow" style="background: lightgrey">
                                                        <asp:TextBox ID="TituloCantidad" runat="server" Width="50%" Font-Bold="true" ForeColor="Black" Style="text-align: center; vertical-align: middle; border-color: transparent; background-color: transparent" Enabled="false">Cantidad reclamada:</asp:TextBox>
                                                        <asp:TextBox ID="tbCantidad" CssClass="form-control" runat="server" Style="text-align: center; width: 50%; height: 30px"></asp:TextBox>
                                                    </div>
                                                    <div class="input-group rounded rounded-2 border border-secondary border-1 shadow" style="background: lightgrey">
                                                        <asp:TextBox ID="tituloPPM" runat="server" Width="50%" Font-Bold="true" ForeColor="Black" Style="text-align: center; vertical-align: middle; border-color: transparent; background-color: transparent" Enabled="false">PPM de cliente:</asp:TextBox>
                                                        <asp:TextBox ID="tbPPM" CssClass="form-control" runat="server" Style="text-align: center; width: 50%; height: 30px"></asp:TextBox>
                                                    </div>
                                                    <div class="input-group rounded rounded-2 border border-secondary border-1 shadow" style="background: orange">
                                                        <button class="btn btn-sm btn-outline-dark border border-0" style="font-weight: bold; width: 100%; color: black" id="Button7" type="button" onclick="AbreContencion()">Consultar Stock</button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card mt-3 border border-dark  shadow shadow-lg">
                                        <h5 class="card-header text-bg-primary"><i class="bi bi-bricks me-2"></i>Informe de detección y contención</h5>
                                        <div class="card-body" style="background-color: #eeeeee">
                                            <div class="col-lg-12">
                                                <h5>Descripción del problema:</h5>
                                                <asp:TextBox ID="tbProblemaNC" runat="server" CssClass="form-control border border-secondary shadow" TextMode="MultiLine" Style="padding-left: 1em; width: 100%" Height="50px"></asp:TextBox>
                                                <div class="form-check ms-2 mt-1 mb-1">
                                                    <input class="form-check-input" id="tbRepetitivoSN" runat="server" type="checkbox" value="">
                                                    <label class="form-check-label" style="font-weight: bold" for="tbRepetitivoSN">
                                                        Defecto recurrente (Marcar si aplica)
                                                    </label>
                                                </div>
                                                <h5>Contramedidas requerida:</h5>
                                                <div class="table table-responsive rounded rounded-2 shadow mb-2">
                                                    <table style="width: 100%">
                                                        <tr style="background-color: lightgrey; border-color: lightgrey">
                                                            <td class="border border-secondary">
                                                                <asp:TextBox ID="tituloContramedidaPROD" runat="server" ForeColor="Black" Font-Bold="true" Height="55px" Style="text-align: left; width: 30%; background-color: transparent; border-color: transparent; float: left" Enabled="false"> PRODUCCIÓN:</asp:TextBox>
                                                                <asp:TextBox ID="tbContramedidaPROD" CssClass="form-control" runat="server" Height="55px" Style="padding-left: 1em; text-align: left; width: 70%; border-color: white;" TextMode="MultiLine"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr style="background-color: lightgrey; border-color: lightgrey">
                                                            <td class="border border-secondary">
                                                                <asp:TextBox ID="tituloContramedidaCAL" runat="server" ForeColor="Black" Font-Bold="true" Height="55px" Style="text-align: left; width: 30%; background-color: transparent; border-color: transparent; float: left" Enabled="false"> CALIDAD:</asp:TextBox>
                                                                <asp:TextBox ID="tbContramedidaCAL" CssClass="form-control" runat="server" Height="55px" Style="padding-left: 1em; text-align: left; width: 70%; border-color: white;" TextMode="MultiLine"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr style="background-color: lightgrey; border-color: lightgrey">
                                                            <td class="border border-secondary">
                                                                <asp:TextBox ID="tituloContramedidaING" runat="server" ForeColor="Black" Font-Bold="true" Height="55px" Style="text-align: left; width: 30%; background-color: transparent; border-color: transparent; float: left" Enabled="false"> INGENIERÍA:</asp:TextBox>
                                                                <asp:TextBox ID="tbContramedidaING" CssClass="form-control" runat="server" Height="55px" Style="padding-left: 1em; text-align: left; width: 70%; border-color: white;" TextMode="MultiLine"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <h5>Observaciones:</h5>
                                                <asp:TextBox ID="tbObservacionesNC" runat="server" CssClass="form-control border border-secondary shadow" TextMode="MultiLine" Style="padding-left: 1em; width: 100%" Height="55px"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="TABCONTENCION" class="tab-pane fade " runat="server">
                            <div class="row">
                                <div class="col-lg-9">
                                    <div class="card mt-2 border border-dark shadow shadow-lg" style="background-color:lightgray">
                                        <h5 class="card-header text-bg-primary" ><i class="bi bi-question-circle me-2"></i>Producto sospechoso</h5>
                                        <asp:GridView ID="DgvContencion" Width="100%" OnRowCommand="GridView_RowCommand"
                                            runat="server" AutoGenerateColumns="false" CssClass="table table-responsive bg-secondary shadow p-3 rounded border-top-1 border-bottom-1 border-start-0 border-end-0" BorderColor="black">
                                            <HeaderStyle CssClass="card-header border-1 border-dark" BackColor="lightgrey" Font-Bold="True" ForeColor="black" HorizontalAlign="Center" />
                                            <RowStyle BackColor="White" />
                                            <AlternatingRowStyle BackColor="#eeeeee" />
                                            <EditRowStyle BackColor="#ffffcc" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Almacén" ItemStyle-Width="5%" ItemStyle-VerticalAlign="Middle" ItemStyle-Font-Bold="true" ItemStyle-BackColor="#e6e6e6">
                                                    <ItemTemplate>
                                                        <asp:Label ID="IdRev" runat="server" Visible="false" Text='<%#Eval("Id") %>' />
                                                        <asp:Label ID="lblIdAlmacen" runat="server" Visible="false" Text='<%#Eval("IdAlmacen") %>' />
                                                        <asp:Label ID="lblNombreAlmacen" runat="server" Text='<%#Eval("NombreAlmacen") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Stock" ItemStyle-Width="15%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e6e6e6" ItemStyle-Font-Bold="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNombre" runat="server" Text='<%#Eval("Cantidad") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Revisadas" ItemStyle-Width="10%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRevisadas" runat="server" Text='<%#Eval("PiezasRevisadas") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NOK" ItemStyle-Width="10%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPiezasNOK" runat="server" Text='<%#Eval("PiezasNOK") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Lotes" ItemStyle-Width="10%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLotes" runat="server" Text='<%#Eval("Lotes") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Responsable" ItemStyle-Width="15%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblResponsable" runat="server" Text='<%#Eval("Nombre") %>' />
                                                        <%-- <asp:DropDownList ID="lblResponsable" runat="server" />--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Notas" ItemStyle-Width="20%" ItemStyle-VerticalAlign="Middle">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtNotas" Rows="1" TextMode="MultiLine" runat="server" Text='<%#Eval("Notas") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="¿Punto limpio?" ItemStyle-Width="20%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <input class="form-check-input" id="CheckPtLimpio" runat="server" type="checkbox" checked='<%#Convert.ToBoolean(Eval("PuntoLimpio")) %>'>
                                                        <asp:Label ID="lblFormador" Font-Size="Small" Font-Italic="true" runat="server" Text='<%#"(" + Eval("FechaPuntoLimpio","{0:dd/MM/yyyy}") + ")" %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="Middle">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="btnañadir" runat="server" class="btn bg-white btn-sm border border-dark" Width="100%" CommandName="NuevoStock">
                                                                <i class="bi bi-plus-square"></i>
                                                        </asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEdit2" runat="server" CssClass="btn btn-success btn-sm shadow" Width="100%" CommandName="GuardarPTLimpio">
                                                                <i class="bi bi-sd-card"></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                   
                                    </div>
                                    <div class="card mt-2 border border-dark shadow shadow-lg" style="background-color:lightgray">
                                        <h5 class="card-header text-bg-primary" ><i class="bi bi-search  me-2"></i>Últimas revisiones</h5>
                                        <div class='embed-container'>
                                            <iframe id="FRAMERESULTADOS" runat="server" style="height: 700px; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%" height="100%"></iframe>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="card mt-2 shadow shadow-lg" style="background-color:lightgray">
                                        <h5 class="card-header text-bg-primary border border-dark"><i class="bi bi-boxes me-2"></i>Estado del producto</h5>
                                        <div class="card-body border border-secondary" style="background-color: #eeeeee">
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <h3 id="lblEstadoGP12" runat="server">---</h3>
                                                    <h5 class="ms-3" id="lblEstadoGP12Desde" runat="server"></h5>
                                                    <h5 class="ms-3" id="lblEstadoGP12Hasta" runat="server"></h5>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="card mt-2 border border-dark shadow shadow-lg" style="background-color:lightgray">
                                        <h5 class="card-header text-bg-primary"><i class="bi bi-exclamation-triangle me-2"></i>Potencialmente afectados</h5>
                                        <asp:GridView ID="dgv_afectadas" runat="server" AllowSorting="True"
                                            Width="100%" ShowHeader="false" CssClass="table table-responsive bg-secondary shadow p-3 rounded border-top-1 border-bottom-1 border-start-0 border-end-0" AutoGenerateColumns="false"
                                            EmptyDataText="Sin referencias a mostrar.">
                                            <HeaderStyle CssClass="card-header border-1 border-dark" BackColor="#666666" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Height="40px" />
                                            <RowStyle BackColor="White" />
                                            <AlternatingRowStyle BackColor="#eeeeee" />
                                            <EditRowStyle BackColor="#ffffcc" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Producto" HeaderStyle-ForeColor="White">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblReferenciaAFECTADA" runat="server" Font-Bold="true" Font-Size="Large" Text='<%#Eval("Referencia") %>' />
                                                        <asp:Label ID="lblDescripcionAFECTADA" runat="server" Text='<%#Eval("Descripcion") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>


                                    </div>

                                </div>
                            </div>
                        </div>
                        <div id="TABPDCA" class="tab-pane fade " runat="server">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="card mt-2  border border-dark shadow shadow-lg">
                                        <h5 class="card-header text-bg-primary"><i class="bi bi-tools me-2"></i>Análisis de causa y acciones correctoras</h5>
                                        <div class='embed-container'>
                                            <iframe id="FRAMEPDCA" runat="server" style="height: 700px; width: 100%; top: 0px; left: 0px; right: 0px; bottom: 0px" width="100%" height="100%"></iframe>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="TABDISTRIBUCION" class="tab-pane fade" runat="server">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="card mt-2 border border-dark shadow shadow-lg" style="background-color:lightgray">
                                        <h5 class="card-header text-bg-primary"><i class="bi bi-person-workspace me-2"></i>Distribución de alerta + Formación vinculada a Alerta</h5>
                                        <asp:GridView ID="dgv_operarios_informados" ShowFooter="true" Width="100%"
                                            OnRowCancelingEdit="GridView_RowCancelingEdit" OnRowEditing="GridView_RowEditing" OnRowUpdating="GridView_RowUpdating" OnRowDataBound="GridView_RowDataBound" OnRowCommand="GridView_RowCommand"
                                            runat="server" AutoGenerateColumns="false" CssClass="table table-responsive bg-secondary shadow p-3 rounded border-top-1 border-bottom-1 border-start-0 border-end-0" BorderColor="black">
                                            <HeaderStyle CssClass="card-header border-1 border-dark" BackColor="lightgrey" Font-Bold="True" ForeColor="black" HorizontalAlign="Center" />
                                            <RowStyle BackColor="White" />
                                            <AlternatingRowStyle BackColor="#eeeeee" />
                                            <EditRowStyle BackColor="#ffffcc" />
                                            <FooterStyle BackColor="lightgrey" />

                                            <Columns>
                                                <asp:TemplateField HeaderText="Nombre" ItemStyle-CssClass="shadow-sm" ItemStyle-Width="25%" ItemStyle-BackColor="#e6e6e6" ItemStyle-Font-Bold="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNOperario" runat="server" Font-Size="X-Large" Text='<%#Eval("OPNUMERO") %>' />
                                                        <asp:Label ID="lblNombre" CssClass="ms-1" runat="server" Text='<%#Eval("OPNOMBRE") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="txtNOperario" runat="server" Font-Size="X-Large" Text='<%#Eval("OPNUMERO") %>' />
                                                        <asp:Label ID="txtOperario" CssClass="ms-1" runat="server" Text='<%#Eval("OPNOMBRE") %>' />
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:DropDownList ID="newOperario" runat="server" Style="text-align: center" CssClass="form-select shadow-sm border border-dark"></asp:DropDownList>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Formador" ItemStyle-Width="35%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFormador" runat="server" Text='<%#Eval("FORMNOMBRE") %>' /><br />
                                                        <asp:Label ID="lblFechaFormador" runat="server" Text='<%#Eval("FECHA") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="txtFormador" runat="server" Style="text-align: center" CssClass="form-select border border-dark shadow-sm"></asp:DropDownList>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Firma" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Image ID="lblFirmaIMG" runat="server" Height="50px" ImageUrl='<%#Eval("FIRMA")  %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <div id="captureSignature" class="border border-dark shado-sm"></div>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEdit2" runat="server" CssClass="btn btn-primary shadow" Width="100%" CommandName="Edit">
                                                        <i class="bi bi-pencil-square"></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="btnUpdate" runat="server" CssClass="btn btn-success shadow" Width="100%" CommandName="Update" OnClientClick="return confirm('¿Seguro que quieres modificar esta fila?');">
                                                <i class="bi bi-hand-thumbs-up"></i>
                                                        </asp:LinkButton>

                                                        <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-danger shadow" Width="100%" CommandName="Cancel">
                                                <i class="bi bi-caret-left-fill"></i>
                                                        </asp:LinkButton>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="btnañadir" runat="server" CssClass="btn btn-primary shadow" Width="100%" CommandName="AddNew">
                                                <i class="bi bi-plus-square"></i>
                                                        </asp:LinkButton>
                                                    </FooterTemplate>

                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <input id="signatureJSON" type="hidden" name="signature" class="signature" value="" runat="server">
                                    </div>
                                </div>
                            </div>


                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="ModalMSAGestion" runat="server" data-bs-keyboard="false" tabindex="-1" aria-labelledby="ModalMSAGestion" aria-hidden="false">
                <div class="modal-dialog modal-xl">
                    <div class="modal-content">
                        <div class="modal-header bg-primary shadow">
                            <h5 class="modal-title text-white" id="H1MSA" runat="server">Añadir evidencia:</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body" runat="server">
                            <div>
                                <div class="row" style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
                                    <div class="d-flex align-items-start">
                                        <div class="nav flex-column nav-pills me-2" id="v-pills-tabMSA" role="tablist" aria-orientation="vertical">
                                            <button id="TABACCIONMSA" class="nav-link active" data-bs-toggle="pill" data-bs-target="#v-pills-home" type="button" role="tab" aria-controls="v-pills-homeMSA" aria-selected="true"><i class="bi bi-kanban"></i></button>
                                        </div>
                                        <div class="tab-content" id="v-pills-tabContentMSA">
                                            <div class="tab-pane fade show active" id="v-pills-homeMSA" role="tabpanel" aria-labelledby="v-pills-home-tab">
                                                <div class="container-fluid">
                                                    <div class="row">
                                                        <div class="row mt-2 mb-1 ms-2 shadow rounded-2 border border-dark bg-white">
                                                            <div class="col-sm-12">
                                                                <h5 class="mt-1"><i class="bi bi-info-square me-2"></i>
                                                                    <label>Datos del documento</label>
                                                                    <label></label>
                                                                </h5>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-4">
                                                                <h6>Tipo de documento:</h6>
                                                                <asp:DropDownList ID="DropTipoEvidencia" runat="server" class="form-select">
                                                                    <asp:ListItem Value="0">-</asp:ListItem>
                                                                    <asp:ListItem Value="7">Cargos</asp:ListItem>
                                                                    <asp:ListItem Value="8">Comunicación con cliente</asp:ListItem>
                                                                    <asp:ListItem Value="2">D3</asp:ListItem>
                                                                    <asp:ListItem Value="3">D6</asp:ListItem>
                                                                    <asp:ListItem Value="4">D8</asp:ListItem>
                                                                    <asp:ListItem Value="6">Pauta de retrabajo</asp:ListItem>
                                                                    <asp:ListItem Value="1">Punto de corte</asp:ListItem>
                                                                    <asp:ListItem Value="5">Otros</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="col-lg-8">
                                                                <h6>Descripción:</h6>
                                                                <input type="text" id="InputDescripcionEvidencia" class="form-control" maxlength="49" autocomplete="off" runat="server">
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-4">
                                                                <h6>Fecha del documento:</h6>
                                                                <input type="text" id="InputFechaEvidencia" class="form-control Add-text" autocomplete="off" runat="server">
                                                            </div>
                                                            <div class="col-lg-8">
                                                                <h6>Adjunto:</h6>
                                                                <div class="input-group bg-white">
                                                                    <asp:FileUpload ID="UploadEvidencia" runat="server" class="form-control"></asp:FileUpload>
                                                                    <button class="btn btn-outline-secondary" type="button" runat="server" id="BTNUploadEvidencia" onserverclick="Insertar_documento">Subir</button>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer" style="background: #e6e6e6">
                            <button type="button" class="btn btn-danger" data-bs-dismiss="modal"><i class="bi bi-caret-left-fill"></i></button>
                        </div>
                    </div>
                </div>
            </div>
            <button id="btnModalContencionAlm" runat="server" type="button" class="btn btn-primary btn-sm" data-bs-toggle="modal" data-bs-target="#ModalContencionAlm" style="width: 100%" hidden="hidden">Agregar documento</button>

            <div class="modal fade" id="ModalContencionAlm" runat="server" data-bs-keyboard="false" tabindex="-1" aria-labelledby="ModalMSAGestion" aria-hidden="false">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header bg-primary shadow">
                            <h5 class="modal-title text-white" id="H1" runat="server">Añadir stock:</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>

                        </div>
                        <div class="modal-body" runat="server">
                            <div style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
                                <div class="row">
                                    <div class="container-fluid">
                                        <h5>Almacén y cantidad:</h5>
                                        <div class="input-group mb-3 bg-white shadow">

                                            <asp:DropDownList runat="server" CssClass="form-select border-dark" ID="DropAlmacenContencion">
                                                <asp:ListItem>-</asp:ListItem>

                                            </asp:DropDownList>
                                            <input type="number" min="0" class="form-control border-dark" id="tbCantidadContencion" runat="server">

                                            <button type="button" id="GuardarCuarentena" runat="server" onserverclick="InsertarStockCuarentena" class="btn btn-outline-secondary border-dark">Enviar</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer" style="background: #e6e6e6">
                            <button type="button" class="btn btn-danger" data-bs-dismiss="modal"><i class="bi bi-caret-left-fill"></i></button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>







