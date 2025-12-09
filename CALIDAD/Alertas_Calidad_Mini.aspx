<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Alertas_Calidad_Mini.aspx.cs" Inherits="ThermoWeb.CALIDAD.ALERTASCALIDADMINI" MasterPageFile="~/SMARTHLite.Master"
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
                <li>
                    <hr class="dropdown-divider">
                </li>
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
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <asp:Timer ID="TempRecarga" runat="server" Interval="30000" ontick="Page_Load">
            
    </asp:Timer>
        <div class="container-fluid">
            <div class="row" style="width: 100%">
                <div class="col-lg-12">

                    <div class="card mt-2 border border-dark  shadow shadow-lg">
                        <div class="card-header text-bg-primary border-bottom border-dark">
                            <div class="row">
                                <div class="col-lg-1">
                                    <asp:Image ID="IMGliente" runat="server" Width="100px" ImageUrl='../SMARTH_docs/NOCONFORMIDADES/sin_imagen.jpg' CssClass="rounded rounded-1" />
                                </div>
                                <div class="col-lg-3">
                                    <asp:Label ID="tbNoConformidadTEXT" CssClass="h3" Font-Bold="true" runat="server" ForeColor="White">NC12345</asp:Label><br />
                                    <asp:Label ID="tbFechaOriginal" class="h4" Font-Italic="true" runat="server"></asp:Label>
                                    <asp:TextBox ID="tbNoConformidad" runat="server" Font-Size="XX-Large" Style="text-align: center; width: 100%; height: 40px" Visible="false"></asp:TextBox>
                                </div>
                                <div class="col-lg-4">                                  
                                    <h1 class="text-white" style="text-align:center">ALERTA DE CALIDAD</h1>                                
                                </div>
                                 <div class="col-lg-2">
                                     </div>
                                <div class="col-lg-2">
                                    <asp:DropDownList ID="NivelAlerta" runat="server" class="form-control border border-secondary border-1 rounded rounded-4" BackColor="Yellow" Font-Bold="true" Font-Size="Large">
                                        <asp:ListItem Text="---" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Q-INFO" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="RECLAMACION OFICIAL" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="NO ACEPTADA" Value="3"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg-6">
                                    <asp:Label ID="tbReferenciaCarga" Width="100%" runat="server" Font-Size="XX-Large" Font-Bold="true" ForeColor="Blue" Font-Italic="true" Style="text-align: center; border-color: transparent; background-color: transparent"></asp:Label>
                                    <asp:Label ID="tbDescripcionCarga" Width="100%" Font-Italic="true" Font-Size="X-Large" Font-Bold="true" ForeColor="Blue" runat="server" Style="text-align: center; border-color: transparent; background-color: transparent" Text="-" />
                                </div>

                                <div class="col-lg-6 border-start border-secondary">
                                    <asp:TextBox ID="tbProblemaNC" runat="server" CssClass="form-control border border-0" Font-Bold="true" Font-Size="X-Large" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                </div>
                            </div>
                    </div>
                    <div class="card-header text-bg-danger border-top border-dark border-bottom border-dark">
                        <label class="h4"><i class="bi bi-shield-exclamation me-2"></i>DETALLE DEL DEFECTO</label>
                    </div>
                    <div class="card-body" style="background-color:lightcoral">
                        <div class="row">
                            <div class="col-lg-6  mb-2 ">
                                <asp:HyperLink ID="hyperlink2" NavigateUrl="" Width="100%" ImageHeight="750px" ImageWidth="100%" ImageUrl="http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg" Target="_new" runat="server" class="rounded border border-dark img-thumbnail img-fluid shadow ms-2 me-2" />
                            </div>
                            <div class="col-lg-6 mb-2">
                                <asp:HyperLink ID="hyperlink3" NavigateUrl="" Width="100%" ImageHeight="750px" ImageWidth="100%" ImageUrl="http://facts4-srv/thermogestion/Imagenes/GP12/sin_imagen.jpg" Target="_new" runat="server" class="rounded border border-dark img-thumbnail img-fluid shadow ms-2 me-2" />
                            </div>
                        </div>

                    </div>
                </div>

            </div>

        </div>

    </div>
    </div>
</asp:Content>







