<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="FichasParametros_nuevo.aspx.cs" MasterPageFile="~/SMARTH.Master"
    Inherits="ThermoWeb.PRODUCCION.FichasParametros_nuevo" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>
<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Ficha de parámetros</title>
    <link rel="shortcut icon" type="image/x-icon" href="ICONOS/FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Ficha de parámetros             
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
   <ul class="navbar-nav me-auto mb-2 mb-lg-0">
        <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">Fichas de parámetros
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown">
                <li><a class="dropdown-item" href="FichasParametros_nuevo.aspx">Crear una ficha de parámetros</a></li>
                <li><a class="dropdown-item" href="FichasParametros.aspx">Consultar una ficha de parámetros</a></li>
            </ul>
        </li>
        <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown2" role="button" data-bs-toggle="dropdown" aria-expanded="false">Consultas
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown2">
                <li><a class="dropdown-item" href="FichasParametrosListado.aspx">Listado de fichas de parámetros</a></li>
                <li><a class="dropdown-item" href="Listado_Manos_Robot.aspx">Listado de manos</a></li>
                <li><a class="dropdown-item" href="Tareas_Cambiador.aspx?TAB=LISTAMOLDES">Listado de moldes</a></li>  
                <li>
                    <hr class="dropdown-divider">
                </li>
                <li><a class="dropdown-item" href="Gestion_Ubicaciones_Moldes.aspx">Gestionar ubicaciones de moldes</a></li>       
            </ul>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <%--Scripts de botones --%>
    <style>
        th {
            background: #0d6efd !important;
            color: white !important;
            position: sticky !important;
            top: 0;
            box-shadow: 0 2px 2px -1px rgba(0, 0, 0, 0.4);
        }

        th, td {
            padding: 0.25rem;
        }

        .nav .nav-link {
            color: black !important;
        }

        .nav .active {
            color: whitesmoke !important;
            text-shadow: 1px 2px 2px darkslategray;
            background-color: darkorange !important;
            border: 1px solid gray;
        }

        .cabecera {
            background-color: lightslategray !important;
            text-shadow: 1px 1px 1px darkslategray;
            text-align: center;
            color: white;
            border: 1px solid black;
            padding: 0;
            margin: 0;
        }

        .cabeceraLeft {
            background-color: lightslategray !important;
            text-shadow: 1px 1px 1px darkslategray;
            text-align: left;
            color: white;
            border: 1px solid black;
            padding: 0;
            margin: 0;
            border-bottom-left-radius: 5px;
            border-top-left-radius: 5px;
            padding: 0.375rem 0.75rem;
            line-height: 1.500;
            font-weight: bold;
        }

        .cabeceraRight {
            background-color: lightslategray !important;
            text-shadow: 1px 1px 1px darkslategray;
            text-align: left;
            color: white;
            border: 1px solid black;
            padding: 0;
            margin: 0;
            border-bottom-right-radius: 5px;
            border-top-right-radius: 5px;
            padding: 0.375rem 0.75rem;
            line-height: 1.500;
            font-weight: bold;
        }

        .cuerpo {
            background-color: lavender !important;
            border: 1px solid black;
            padding: 0;
            margin: 0;
            font-style: italic !important;
            color: black !important;
        }

        .cuerpo-editable {
            background-color: white !important;
            border-radius: 5px !important;
            border: 2px solid black;
            padding: 0;
            margin: 0;
        }

        .table-responsive .table td input[type="text"] {
            padding: 0; /* Elimina el espaciado alrededor de los TextBox */
            margin: 0; /* Elimina los márgenes */
        }

        .ps-25 {
            padding-left: 0.75rem !important;
            padding-right: 0.75rem !important;
        }
    </style>
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
    <script type="text/javascript">
        function ShowPopup1() {
            document.getElementById("AUXMODALACCION").click();
        }
        function ShowPopupFirma() {
            document.getElementById("AUXMODALACCIONFIRMA").click();
        }
        function ClosePopupFirma() {
            document.getElementById("AUXCIERRAMODALFIRMA").click();
        }
        function guardado_OK() {
            alert("Ficha de parámetros guardada correctamente.");
            location.href = 'FichasParametros.aspx?REFERENCIA=' + document.getElementById('tbFicha').value + "&MAQUINA=" + document.getElementById('tbMaquina').value + "&VERSION=1";
        }
        function guardado_NOK() {
            alert("Se ha producido un error al guardar la ficha de parámetros. Revise que los datos estén bien y consulte con el administrador del sistema.");
        }
        function guardado_NOKEXISTE() {
            alert("Se ha producido un error. La ficha a generar ya existe en el sistema.");
        }
        function ErrorRecalculando() {
            alert("Se ha producido un error al recalcular. Existen campos con datos erróneos. Revisa las celdas marcadas y vuelve a intentarlo");
        }
    </script>   
    <div class="container-fluid" style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
        <div class="container">
            <div class="row">
                <div class="card border-0">
                    <div class="card-header mt-2 border border-dark shadow" style="background-color: darkorange">
                        <label class="h5" style="color: whitesmoke; text-shadow: 1px 2px 2px darkslategray"><i class="bi bi-info-circle">&nbsp Ficha de parámetros</i></label>
                        <div class="btn-group float-end shadow">
                               <button id="btnGuardar" runat="server" onserverclick="GuardarFicha" type="button" class="btn btn-sm btn-success border border-dark shadow"><i class="bi bi-sd-card">&nbsp Guardar nueva ficha</i></button>

                        </div>
                    </div>
                    <div class="card-body border border-dark border-top-0 shadow bg-white" style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
                        <div class="row">
                            <div class="col-lg-6">
                                <div class="row ps-25" style="background-color: transparent">
                                    <div class="col-lg-6 ps-0 pe-0 shadow">
                                        <div class="input-group " style="padding: 0; margin: 0">
                                            <span class="cabeceraLeft" style="width: 50%"><i>&nbsp Referencia</i></span>
                                            <input type="text" class="form-control border border-dark" id="tbFicha" style="text-align: left; width: 50%; overflow: hidden; border-top-right-radius:0; border-bottom-right-radius:0" runat="server">
                                          
                                        </div>
                                    </div>
                                    <div class="col-lg-6 ps-0 pe-0 shadow">
                                        <asp:TextBox ID="tbNombre" runat="server" CssClass="form-control border border-dark" Style="text-align: center; width: 100%; border-bottom-left-radius: 0px; border-top-left-radius: 0px" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <div class="input-group shadow">
                                    <span class="cabeceraLeft" style="width: 65%"><i>&nbsp Máquina</i></span>
                                    <asp:TextBox ID="tbMaquina" runat="server" Width="35%" CssClass="form-control border border-dark" Style="text-align: center; border-top-right-radius: 5px; border-bottom-right-radius: 5px" ></asp:TextBox>
                                    
                                </div>
                            </div>
                          
                            <div class="col-lg-3">
                                 <button id="btnImportarMaquina" runat="server" type="button" onserverclick="CargarDatosBms"  class="btn btn-primary border border-dark shadow float-end" style="width:100%"><i class="bi bi-arrow-down-square">&nbsp Importar datos de BMS</i></button>
                            </div>

                           
                        </div>
                    </div>
                    <div class="card-body border border-dark border-top-0 shadow" style="background-color: lightgrey">
                        <div class="row">
                            <div class="col-lg-2">
                                <asp:Image ID="ImgPieza" runat="server" CssClass="rounded border border-dark shadow" Width="100%" ImageUrl='http://facts4-srv/thermogestion/Imagenes/directorioparam/sin_imagen.jpg' />
                                <asp:TextBox ID="tbCliente" runat="server" Style="text-align: center" Enabled="false" Visible="false"></asp:TextBox>
                            </div>
                            <div class="col-lg-5">
                                <div class="input-group shadow">
                                    <span class="cabeceraLeft" style="width: 45%"><i class="bi bi-grid-1x2">&nbsp Código molde</i></span>
                                    <asp:TextBox ID="tbCodigoMolde" runat="server" CssClass="cuerpo-editable" Style="text-align: center; width: 25%"></asp:TextBox>
                                    <asp:TextBox ID="tbCavidades" runat="server" CssClass="form-control e border border-dark" Style="text-align: center; width: 15%" Enabled="false"></asp:TextBox>
                                    <span class="cabeceraRight" style="width: 15%">cav.</span>
                                </div>
                                <div class="input-group shadow">
                                    <span class="cabeceraLeft" style="width: 45%"><i class="bi bi-grid-1x2">&nbsp Ubicación</i></span>
                                    <asp:TextBox ID="tbUbicacionMolde" runat="server" CssClass="form-control e border border-dark" Style="text-align: center; width: 40%" Enabled="false"></asp:TextBox>
                                    <button type="button" id="BTNModalUbicacion" runat="server" class="btn btn-primary border border-dark" data-bs-toggle="modal" data-bs-target="#ModalUbicacion" style="text-align: center; width: 15%" disabled="disabled"><i class="bi bi-geo-alt"></i></button>
                                </div>
                                <div class="input-group shadow">
                                    <span class="cabeceraLeft" style="width: 45%"><i class="bi bi-calculator">&nbsp Programa inyección</i></span>
                                    <asp:TextBox ID="tbProgramaMolde" runat="server" CssClass="cuerpo-editable" Style="text-align: center; width: 55%" ></asp:TextBox>
                                </div>
                                <div class="input-group shadow">
                                    <span class="cabeceraLeft" style="width: 45%"><i class="bi bi-calculator">&nbsp Apertura máquina</i></span>
                                    <asp:TextBox ID="tbAperturaMaquina" runat="server" CssClass="cuerpo-editable" Style="text-align: center; width: 40%" ></asp:TextBox>
                                    <span class="cabeceraRight" style="width: 15%">mm</span>
                                </div>
                                <div class="input-group shadow">
                                    <span class="cabeceraLeft" style="width: 45%"><i class="bi bi-calculator">&nbsp Fuerza cierre</i></span>
                                    <asp:TextBox ID="tbFuerzaCierre" runat="server" CssClass="cuerpo-editable" Style="text-align: center; width: 40%" ></asp:TextBox>
                                    <span class="cabeceraRight" style="width: 15%">kN</span>
                                </div>
                            </div>
                            <div class="col-lg-5">
                                <div class="input-group shadow">
                                    <span class="cabeceraLeft" style="width: 45%"><i class="bi bi-gear-wide-connected">&nbsp Modo</i></span>
                                    <asp:DropDownList ID="DropModoSelect" runat="server" CssClass="cuerpo-editable" Style="text-align: center; width: 55%">
                                        <asp:ListItem Value="0">-</asp:ListItem>
                                        <asp:ListItem Value="1">Manual</asp:ListItem>
                                        <asp:ListItem Value="2">Automático</asp:ListItem>
                                        <asp:ListItem Value="3">Semi-Automático</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="input-group shadow">
                                    <span class="cabeceraLeft" style="width: 45%"><i class="bi bi-gear-wide-connected">&nbsp Nº programa robot</i></span>
                                    <asp:TextBox ID="tbProgramaRobot" runat="server" CssClass="cuerpo-editable" Style="text-align: center; width: 55%"></asp:TextBox>
                                </div>
                                <div class="input-group shadow">
                                    <span class="cabeceraLeft" style="width: 45%"><i class="bi bi-gear-wide-connected">&nbsp Nº Mano</i></span>
                                    <asp:TextBox ID="tbManoRobot" runat="server" CssClass="form-control border border-dark" Style="text-align: center; width: 40%" Enabled="false"></asp:TextBox>
                                    <button id="BTNManoUbi" runat="server" type="button" class="btn btn-primary border border-dark" style="width: 15%"  data-bs-toggle="modal" data-bs-target="#ModalAsignaMano" disabled="disabled"><i class="bi bi-pencil-fill"></i></button>
                                </div>
                                <div class="input-group shadow">
                                    <span class="cabeceraLeft" style="width: 45%"><i class="bi bi-gear-wide-connected">&nbsp Ubicación</i></span>
                                    <asp:TextBox ID="tbUbicacionMano" runat="server" CssClass="form-control e border border-dark" Style="text-align: center; width: 55%" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="input-group shadow">
                                    <span class="cabeceraLeft" style="width: 45%"><i class="bi bi-grid-1x2">&nbsp Operarios</i></span>
                                    <asp:TextBox ID="tbPersonal" runat="server" CssClass="form-control border border-dark" Style="text-align: center; width: 40%" Enabled="false"></asp:TextBox>
                                    <span class="cabeceraRight" style="width: 15%; text-align: center"><i class="bi bi-person-fill">&nbsp</i></span>
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="card-footer border border-dark border-top-0 shadow" style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
                        <div class="row">
                            <div class="col-lg-4">
                                <div class="input-group shadow">
                                    <span class="cabeceraLeft" style="width: 45%"><i class="bi bi-box">&nbsp Peso pieza</i></span>
                                    <asp:TextBox ID="tbPesoPieza" runat="server" CssClass="cuerpo-editable" Style="text-align: center; width: 35%" ></asp:TextBox>
                                    <span class="cabeceraRight" style="width: 20%">gr.</span>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="input-group shadow">
                                    <span class="cabeceraLeft" style="width: 45%"><i class="bi bi-box">&nbsp Peso coladas</i></span>
                                    <asp:TextBox ID="tbPesoColada" runat="server" CssClass="cuerpo-editable" Style="text-align: center; width: 35%" ></asp:TextBox>
                                    <span class="cabeceraRight" style="width: 20%">gr.</span>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="input-group shadow">
                                    <span class="cabeceraLeft" style="width: 45%"><i class="bi bi-box">&nbsp Peso inyectada</i></span>
                                    <asp:TextBox ID="tbPesoTotal" runat="server" CssClass="cuerpo-editable" Style="text-align: center; width: 35%" ></asp:TextBox>
                                    <span class="cabeceraRight" style="width: 20%">gr.</span>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <div class="row mt-3">
                <div class="col-lg-12">
                    <ul class="nav nav-pills nav-fill rounded rounded-2 border border-dark" id="pills-tab" role="tablist" style="background-color: gainsboro">
                        <li class="nav-item" role="presentation">
                            <button class="nav-link active" id="BTN_PARAMETROS" runat="server" data-bs-toggle="pill" data-bs-target="#pills_PARAMETROS" type="button" role="tab" aria-controls="pills-home" aria-selected="true" style="font-weight: bold"><i class="bi bi-ui-radios-grid">&nbsp Parámetros</i></button>
                        </li>
                        <li class="nav-item" role="presentation">
                            <button class="nav-link" id="BTN_ATEMPERADO" runat="server" data-bs-toggle="pill" data-bs-target="#pills_ATEMPERADO" type="button" role="tab" aria-controls="pills-profile" aria-selected="false" style="font-weight: bold"><i class="bi bi-thermometer-snow">&nbsp Atemperado</i></button>
                        </li>
                        
                    </ul>

                    <div class="tab-content" id="pills-tabContent">
                        <div class="tab-pane fade show active" id="pills_PARAMETROS" runat="server" role="tabpanel" aria-labelledby="pills-home-tab">
                            <div class="row">
                                <div class="card border-0">
                                    <div class="card-footer border border-dark border-top-0" style="background-color: whitesmoke">
                                        <div class="row">
                                            <h5><i class="bi bi-thermometer-half">Temperaturas</i></h5>
                                            <div class="col-lg-6">
                                                <div class="card border-0 mt-1 shadow">
                                                    <div class="card-header  border border-dark shadow" style="background-color: orange">
                                                        <div class="row">
                                                            <div class="col-lg-9">
                                                                <label style="font-size: large; font-weight: 600; width: 75%"><i>&nbsp Cilindro</i></label>
                                                            </div>
                                                            <div class="col-lg-3 pe-0">
                                                                <div class="input-group input-group-sm float-end" style="width: 100%">
                                                                    <asp:Label runat="server" class="input-group-text border border-dark" BackColor="lavender" Font-Bold="true" Width="20%">± &nbsp</asp:Label>
                                                                    <asp:Label type="number" runat="server" ID="TbTOLCilindro" class="cuerpo" Style="width: 50%; margin: 0; text-align: center" Font-Italic="true" BackColor="Lavender">10</asp:Label>
                                                                    <asp:Label runat="server" class="input-group-text border border-dark" BackColor="lavender" Font-Bold="true" Width="30%">%</asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="card-body p-0">
                                                        <div class="row ps-25">
                                                            <div class="col-lg-3  ps-0 pe-0">
                                                                <div class="input-group input-group-lg ps-0 pe-0" style="width: 100%; background: darkslategray">
                                                                    <asp:Label ID="tbBoq" CssClass="cabecera" Font-Bold="true" Width="40%" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">BOQ</asp:Label><br />
                                                                    <asp:Label ID="tbT1" CssClass="cabecera" Font-Bold="true" Width="30%" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">T1</asp:Label>
                                                                    <asp:Label ID="tbT2" CssClass="cabecera" Font-Bold="true" Width="30%" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">T2</asp:Label>
                                                                </div>
                                                                <div class="input-group  input-group-lg ps-0 pe-0" style="width: 100%">
                                                                    <asp:TextBox ID="thBoq" runat="server" Width="40%" CssClass="cuerpo-editable" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                    <asp:TextBox ID="thT1" runat="server" Width="30%" CssClass="cuerpo-editable" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                    <asp:TextBox ID="thT2" runat="server" Width="30%" CssClass="cuerpo-editable" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-3 ps-0 pe-0">
                                                                <div class="input-group input-group-lg ps-0 pe-0" style="width: 100%; background: darkslategray">
                                                                    <asp:Label ID="tbT3" CssClass="cabecera" Width="34%" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">T3</asp:Label>
                                                                    <asp:Label ID="tbT4" CssClass="cabecera" Width="33%" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">T4</asp:Label>
                                                                    <asp:Label ID="tbT5" CssClass="cabecera" Width="33%" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">T5</asp:Label>
                                                                </div>
                                                                <div class="input-group input-group-lg ps-0 pe-0" style="width: 100%">
                                                                    <asp:TextBox ID="thT3" CssClass="cuerpo-editable" Width="34%" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                    <asp:TextBox ID="thT4" CssClass="cuerpo-editable" Width="33%" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                    <asp:TextBox ID="thT5" CssClass="cuerpo-editable" Width="33%" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>

                                                                </div>
                                                            </div>
                                                            <div class="col-lg-3 ps-0 pe-0">
                                                                <div class="input-group input-group-lg bg-secondary ps-0 pe-0" style="width: 100%; background: darkslategray">
                                                                    <asp:Label ID="tbT6" CssClass="cabecera" Width="34%" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">T6</asp:Label>
                                                                    <asp:Label ID="tbT7" CssClass="cabecera" Width="33%" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">T7</asp:Label>
                                                                    <asp:Label ID="tbT8" CssClass="cabecera" Width="33%" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">T8</asp:Label>
                                                                </div>
                                                                <div class="input-group  input-group-lg ps-0 pe-0" style="width: 100%">
                                                                    <asp:TextBox ID="thT6" CssClass="cuerpo-editable" Width="34%" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                    <asp:TextBox ID="thT7" CssClass="cuerpo-editable" Width="33%" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                    <asp:TextBox ID="thT8" CssClass="cuerpo-editable" Width="33%" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-3 ps-0 pe-0">
                                                                <div class="input-group  input-group-lg ps-0 pe-0" style="width: 100%; background: darkslategray">
                                                                    <asp:Label ID="tbT9" CssClass="cabecera" Width="34%" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">T9</asp:Label>
                                                                    <asp:Label ID="tbT10" CssClass="cabecera" Width="33%" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">T10</asp:Label>
                                                                    <asp:Label ID="tbTUNIT" CssClass="cabecera" Width="33%" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">-</asp:Label>
                                                                </div>
                                                                <div class="input-group  input-group-lg ps-0 pe-0" style="width: 100%">
                                                                    <asp:TextBox ID="thT9" CssClass="cuerpo-editable" Width="34%" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                    <asp:TextBox ID="thT10" CssClass="cuerpo-editable" Width="33%" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                    <asp:Label ID="thTUNIT" CssClass="cabecera" Width="33%" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">ºC</asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    
                                                </div>
                                            </div>
                                            <div class="col-lg-6">
                                                <div class="card border-0 mt-1 shadow">
                                                    <div class="card-header border border-dark shadow" style="background-color: orange">
                                                        <div class="row">
                                                            <div class="col-lg-9">
                                                                <label style="font-size: large; font-weight: 600; width: 75%"><i>&nbsp Cámara caliente</i></label>
                                                            </div>
                                                            <div class="col-lg-3 pe-0">
                                                                <div class="input-group input-group-sm float-end" style="width: 100%">
                                                                    <asp:Label runat="server" class="input-group-text border border-dark" BackColor="lavender" Font-Bold="true" Width="20%">± &nbsp</asp:Label>
                                                                    <asp:Label type="number" runat="server" ID="tbTOLCamCaliente" class="cuerpo" Style="width: 50%; margin: 0; text-align: center" Font-Italic="true" BackColor="Lavender">10</asp:Label>
                                                                    <asp:Label runat="server" class="input-group-text border border-dark" BackColor="lavender" Font-Bold="true" Width="30%">%</asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="card-body p-0">
                                                        <div class="row ps-25">
                                                            <div class="col-lg-4 ps-0 pe-0">
                                                                <div class="input-group input-group-lg ps-0 pe-0" style="width: 100%; background: darkslategray">
                                                                    <asp:Label ID="tbZ1" CssClass="cabecera" Width="34%" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">Z1</asp:Label>
                                                                    <asp:Label ID="tbZ2" CssClass="cabecera" Width="33%" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">Z2</asp:Label>
                                                                    <asp:Label ID="tbZ3" CssClass="cabecera" Width="33%" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">Z3</asp:Label>
                                                                </div>
                                                                <div class="input-group input-group-lg ps-0 pe-0" style="width: 100%">
                                                                    <asp:TextBox ID="thZ1" CssClass="cuerpo-editable" Width="34%" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                    <asp:TextBox ID="thZ2" CssClass="cuerpo-editable" Width="33%" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                    <asp:TextBox ID="thZ3" CssClass="cuerpo-editable" Width="33%" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 ps-0 pe-0">
                                                                <div class="input-group input-group-lg ps-0 pe-0" style="width: 100%; background: darkslategray">
                                                                    <asp:Label ID="tbZ4" CssClass="cabecera" Width="25%" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">Z4</asp:Label>
                                                                    <asp:Label ID="tbZ5" CssClass="cabecera" Width="25%" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">Z5</asp:Label>
                                                                    <asp:Label ID="tbZ6" CssClass="cabecera" Width="25%" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">Z6</asp:Label>
                                                                    <asp:Label ID="tbZ7" CssClass="cabecera" Width="25%" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">Z7</asp:Label>
                                                                </div>
                                                                <div class="input-group input-group-lg ps-0 pe-0" style="width: 100%">
                                                                    <asp:TextBox ID="thZ4" CssClass="cuerpo-editable" Width="25%" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                    <asp:TextBox ID="thZ5" CssClass="cuerpo-editable" Width="25%" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                    <asp:TextBox ID="thZ6" CssClass="cuerpo-editable" Width="25%" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                    <asp:TextBox ID="thZ7" CssClass="cuerpo-editable" Width="25%" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 ps-0 pe-0">
                                                                <div class="input-group input-group-lg ps-0 pe-0" style="width: 100%; background: darkslategray">
                                                                    <asp:Label ID="tbZ8" CssClass="cabecera" Width="25%" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">Z8</asp:Label>
                                                                    <asp:Label ID="tbZ9" CssClass="cabecera" Width="25%" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">Z9</asp:Label>
                                                                    <asp:Label ID="tbZ10" CssClass="cabecera" Width="25%" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">Z10</asp:Label>
                                                                    <asp:Label ID="tbZ1UNIT" CssClass="cabecera" Width="25%" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">-</asp:Label>
                                                                </div>
                                                                <div class="input-group input-group-lg ps-0 pe-0" style="width: 100%">
                                                                    <asp:TextBox ID="thZ8" CssClass="cuerpo-editable" Width="25%" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                    <asp:TextBox ID="thZ9" CssClass="cuerpo-editable" Width="25%" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                    <asp:TextBox ID="thZ10" CssClass="cuerpo-editable" Width="25%" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                    <asp:Label ID="thZ1UNIT" CssClass="cabecera" Width="25%" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">ºC</asp:Label>
                                                                </div>
                                                            </div>

                                                        </div>
                                                        <div class="row ps-25">
                                                            <div class="col-lg-4 ps-0 pe-0">
                                                                <div class="input-group  input-group-lg ps-0 pe-0" style="width: 100%; background: darkslategray">
                                                                    <asp:Label ID="tbZ11" CssClass="cabecera" Width="34%" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">Z11</asp:Label>
                                                                    <asp:Label ID="tbZ12" CssClass="cabecera" Width="33%" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">Z12</asp:Label>
                                                                    <asp:Label ID="tbZ13" CssClass="cabecera" Width="33%" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">Z13</asp:Label>
                                                                </div>
                                                                <div class="input-group  input-group-lg ps-0 pe-0" style="width: 100%">
                                                                    <asp:TextBox ID="thZ11" CssClass="cuerpo-editable" Width="34%" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible; border-bottom-left-radius: 5px"></asp:TextBox>
                                                                    <asp:TextBox ID="thZ12" CssClass="cuerpo-editable" Width="33%" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                    <asp:TextBox ID="thZ13" CssClass="cuerpo-editable" Width="33%" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 ps-0 pe-0">
                                                                <div class="input-group  input-group-lg ps-0 pe-0" style="width: 100%; background: darkslategray">
                                                                    <asp:Label ID="tbZ14" CssClass="cabecera" Width="25%" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">Z14</asp:Label>
                                                                    <asp:Label ID="tbZ15" CssClass="cabecera" Width="25%" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">Z15</asp:Label>
                                                                    <asp:Label ID="tbZ16" CssClass="cabecera" Width="25%" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">Z16</asp:Label>
                                                                    <asp:Label ID="tbZ17" CssClass="cabecera" Width="25%" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">Z17</asp:Label>
                                                                </div>
                                                                <div class="input-group  input-group-lg ps-0 pe-0" style="width: 100%">
                                                                    <asp:TextBox ID="thZ14" CssClass="cuerpo-editable" Width="25%" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                    <asp:TextBox ID="thZ15" CssClass="cuerpo-editable" Width="25%" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                    <asp:TextBox ID="thZ16" CssClass="cuerpo-editable" Width="25%" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                    <asp:TextBox ID="thZ17" CssClass="cuerpo-editable" Width="25%" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>

                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 ps-0 pe-0">
                                                                <div class="input-group  input-group-lg ps-0 pe-0" style="width: 100%; background: darkslategray">
                                                                    <asp:Label ID="tbZ18" CssClass="cabecera" Width="25%" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">Z18</asp:Label>
                                                                    <asp:Label ID="tbZ19" CssClass="cabecera" Width="25%" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">Z19</asp:Label>
                                                                    <asp:Label ID="tbZ20" CssClass="cabecera" Width="25%" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">Z20</asp:Label>
                                                                    <asp:Label ID="tbZ2UNIT" CssClass="cabecera" Width="25%" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">-</asp:Label>
                                                                </div>
                                                                <div class="input-group  input-group-lg ps-0 pe-0" style="width: 100%">
                                                                    <asp:TextBox ID="thZ18" CssClass="cuerpo-editable" Width="25%" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                    <asp:TextBox ID="thZ19" CssClass="cuerpo-editable" Width="25%" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                    <asp:TextBox ID="thZ20" CssClass="cuerpo-editable" Width="25%" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                    <asp:Label ID="thZ2UNIT" CssClass="cabecera" Width="25%" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible; border-bottom-right-radius: 5px">ºC</asp:Label>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <%--TEMPERATURAS --%>
                                        <%--TABS DE SECUENCIAL --%>
                                        <h5><i class="bi bi-file-spreadsheet">&nbsp Inyección</i></h5>
                                        <ul class="nav nav-pills nav-fill mt-2 border border-dark rounded rounded-2" id="pills_tabSECUSELECT" style="background-color: gainsboro" role="tablist">
                                            <li class="nav-item" role="presentation">
                                                <button class="nav-link active" id="pills_tabINYECCION" runat="server" data-bs-toggle="pill" data-bs-target="#pills_INYECCION" type="button" role="tab" aria-selected="true" style="font-weight: bold"><i class="bi bi-vinyl-fill me-2">&nbsp Inyección</i></button>
                                            </li>
                                            <li class="nav-item" role="presentation">
                                                <button class="nav-link" id="pills_tabSECUENCIAL" runat="server" data-bs-toggle="pill" data-bs-target="#pills_SECUENCIAL" type="button" role="tab" aria-selected="false" style="font-weight: bold"><i class="bi bi-ui-radios me-2">&nbsp Secuencial</i></button>
                                            </li>
                                        </ul>
                                        <div class="tab-content" id="pills-tabContentSECUENCIAL">
                                            <div class="tab-pane fade  border border-secondary shadow rounded-bottom border-top-0 show active" id="pills_INYECCION" runat="server" role="tabpanel">
                                                <div class="row ms-1 me-1  mt-1 mb-1">
                                                    <div class="col-lg-8">
                                                        <%--Carrera de inyección--%>
                                                        <div class="card border-0 shadow mt-2">
                                                            <div class="card-header border border-dark shadow" style="background-color: orange">
                                                                <div class="row">
                                                                    <div class="col-lg-10">
                                                                        <label style="font-size: large; font-weight: 600; width: 75%"><i>&nbsp Carrera de inyección</i></label>
                                                                    </div>
                                                                    <div class="col-lg-2 pe-0">
                                                                        <div class="input-group input-group-sm float-end" style="width: 100%">
                                                                            <asp:Label runat="server" class="input-group-text border border-dark" BackColor="lavender" Font-Bold="true" Width="20%">± &nbsp</asp:Label>
                                                                            <asp:Label type="number" runat="server" ID="tbTOLCarInyeccion" class="cuerpo" Style="width: 50%; margin: 0; text-align: center" Font-Italic="true" BackColor="Lavender">10</asp:Label>
                                                                            <asp:Label runat="server" class="input-group-text border border-dark" BackColor="lavender" Font-Bold="true" Width="30%">%</asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body p-0">
                                                                <div class="row ps-25">
                                                                    <div class="col-lg-3 p-0">
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%; background: darkslategray">
                                                                            <asp:Label ID="tbPaso" class="cabecera" runat="server" Font-Bold="true" Style="text-align: center; margin: 0; padding: 0; overflow: visible" Width="67%">PASO</asp:Label>
                                                                            <asp:Label ID="tb1" class="cabecera" runat="server" Font-Bold="true" Style="text-align: center; margin: 0; padding: 0; overflow: visible" Width="33%">1</asp:Label>
                                                                        </div>
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%">
                                                                            <asp:Label ID="tbVelocidad" runat="server" class="cabecera" Style="text-align: center; margin: 0; padding: 0; overflow: visible" Font-Bold="true" Width="67%">VELOCIDAD</asp:Label>
                                                                            <asp:TextBox ID="thV1" runat="server" class="cuerpo-editable" Font-Italic="true" Style="text-align: center; margin: 0; padding: 0; overflow: visible" Width="33%"></asp:TextBox>
                                                                        </div>
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%">
                                                                            <asp:Label ID="tbCarrera" runat="server" class="cabecera" Style="text-align: center; margin: 0; padding: 0; overflow: visible" Font-Bold="true" Width="67%">CARRERA</asp:Label>
                                                                            <asp:TextBox ID="thC1" runat="server" class="cuerpo-editable" Style="text-align: center; margin: 0; padding: 0; overflow: visible" Width="33%"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-2 p-0">
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%; background: darkslategray">
                                                                            <asp:Label ID="tb2" class="cabecera" runat="server" Font-Bold="true" Width="34%" Style="text-align: center; margin: 0; padding: 0; overflow: visible">2</asp:Label>
                                                                            <asp:Label ID="tb3" class="cabecera" runat="server" Font-Bold="true" Width="33%" Style="text-align: center; margin: 0; padding: 0; overflow: visible">3</asp:Label>
                                                                            <asp:Label ID="tb4" class="cabecera" runat="server" Font-Bold="true" Width="33%" Style="text-align: center; margin: 0; padding: 0; overflow: visible">4</asp:Label>
                                                                        </div>
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%">
                                                                            <asp:TextBox ID="thV2" runat="server" class="cuerpo-editable" Font-Italic="true" Width="34%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="thV3" runat="server" class="cuerpo-editable" Font-Italic="true" Width="33%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="thV4" runat="server" class="cuerpo-editable" Font-Italic="true" Width="33%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                        </div>
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%">
                                                                            <asp:TextBox ID="thC2" runat="server" class="cuerpo-editable" Width="34%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="thC3" runat="server" class="cuerpo-editable" Width="33%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="thC4" runat="server" class="cuerpo-editable" Width="33%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-2 p-0">
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%; background: darkslategray">
                                                                            <asp:Label ID="tb5" class="cabecera" runat="server" Font-Bold="true" Width="34%" Style="text-align: center; margin: 0; padding: 0; overflow: visible">5</asp:Label>
                                                                            <asp:Label ID="tb6" class="cabecera" runat="server" Font-Bold="true" Width="33%" Style="text-align: center; margin: 0; padding: 0; overflow: visible">6</asp:Label>
                                                                            <asp:Label ID="tb7" class="cabecera" runat="server" Font-Bold="true" Width="33%" Style="text-align: center; margin: 0; padding: 0; overflow: visible">7</asp:Label>
                                                                        </div>
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%">
                                                                            <asp:TextBox ID="thV5" runat="server" class="cuerpo-editable" Width="34%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="thV6" runat="server" class="cuerpo-editable" Width="33%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="thV7" runat="server" class="cuerpo-editable" Width="33%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                        </div>
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%">
                                                                            <asp:TextBox ID="thC5" runat="server" class="cuerpo-editable" Width="34%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="thC6" class="cuerpo-editable" runat="server" Width="33%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="thC7" class="cuerpo-editable" runat="server" Width="33%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-2 p-0">
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%; background: darkslategray">
                                                                            <asp:Label ID="tb8" class="cabecera" runat="server" Font-Bold="true" Width="34%" Style="text-align: center; margin: 0; padding: 0; overflow: visible">8</asp:Label>
                                                                            <asp:Label ID="tb9" class="cabecera" runat="server" Font-Bold="true" Width="33%" Style="text-align: center; margin: 0; padding: 0; overflow: visible">9</asp:Label>
                                                                            <asp:Label ID="tb10" class="cabecera" runat="server" Font-Bold="true" Width="33%" Style="text-align: center; margin: 0; padding: 0; overflow: visible">10</asp:Label>
                                                                        </div>
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%">
                                                                            <asp:TextBox ID="thV8" runat="server" class="cuerpo-editable" Width="34%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="thV9" runat="server" class="cuerpo-editable" Width="33%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="thV10" runat="server" class="cuerpo-editable" Width="33%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                        </div>
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%">
                                                                            <asp:TextBox ID="thC8" class="cuerpo-editable" runat="server" Width="34%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="thC9" class="cuerpo-editable" runat="server" Width="33%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="thC10" class="cuerpo-editable" runat="server" Width="33%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-3 p-0">
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%; background: darkslategray">
                                                                            <asp:Label ID="tb11" class="cabecera" runat="server" Width="25%" Font-Bold="true" Style="text-align: center; margin: 0; padding: 0; overflow: visible">11</asp:Label>
                                                                            <asp:Label ID="tb12" class="cabecera" runat="server" Width="25%" Font-Bold="true" Style="text-align: center; margin: 0; padding: 0; overflow: visible">12</asp:Label>
                                                                            <asp:Label ID="tbVelUnit" class="cabecera" runat="server" Width="50%" Font-Bold="true" Style="text-align: center; margin: 0; padding: 0; overflow: visible">-</asp:Label>
                                                                        </div>
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%">
                                                                            <asp:TextBox ID="thV11" runat="server" class="cuerpo-editable" Width="25%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="thV12" runat="server" class="cuerpo-editable" Width="25%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:Label ID="thVelUnit" class="cabecera" runat="server" Width="50%" Font-Bold="true" Style="text-align: center; margin: 0; padding: 0; overflow: visible">mm/s</asp:Label>
                                                                        </div>
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%">
                                                                            <asp:TextBox ID="thC11" class="cuerpo-editable" runat="server" Width="25%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="thC12" class="cuerpo-editable" runat="server" Width="25%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:Label ID="thCarrUnit" class="cabecera" runat="server" Width="50%" Font-Bold="true" Style="text-align: center; margin: 0; padding: 0; overflow: visible">mm</asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <div class="card border-0 shadow mt-2">
                                                            <div class="card-header border border-dark shadow" style="background-color: orange">
                                                                <label class="h6"><i>&nbsp Tiempo</i></label>
                                                            </div>
                                                            <div class="card-body rounded-bottom p-0">
                                                                <div class="input-group" style="width: 100%">
                                                                    <asp:TextBox ID="tbTiempoInyeccion" runat="server" class="cuerpo-editable p-0" Style="text-align: center; width: 70%"></asp:TextBox>
                                                                    <asp:Label ID="tbTiempoInyeccionUNIT" runat="server" class="cuerpo p-0" Style="text-align: center; width: 30%; margin: 0; padding: 0; border-left: 0">s</asp:Label>
                                                                </div>
                                                                <div class="input-group" style="width: 100%">
                                                                    <asp:Label ID="tbTiempoInyeccionN" runat="server" class="cabecera p-0" Font-Bold="true" Style="text-align: center; width: 25%; margin: 0; padding: 0; width: 25%; border-bottom-left-radius: 5px">MIN</asp:Label>
                                                                    <asp:Label ID="tbTiempoInyeccionNVal" runat="server" class="cuerpo p-0" Style="text-align: center; width: 25%; margin: 0; padding: 0">10%</asp:Label>
                                                                    <asp:Label ID="tbTiempoInyeccionM" runat="server" class="cabecera p-0" Font-Bold="true" Style="text-align: center; width: 25%; margin: 0; padding: 0">MAX</asp:Label>
                                                                    <asp:Label ID="tbTiempoInyeccionMVal" runat="server" class="cuerpo p-0" Style="text-align: center; width: 25%; margin: 0; padding: 0; width: 25%; border-bottom-right-radius: 5px">10%</asp:Label>
                                                                </div>
                                                                    </div>
                                                        </div>
                                                        <%--Tiempo de inyección--%>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <%--Tiempo de presión--%>
                                                        <div class="card border-0 shadow mt-2">
                                                            <div class="card-header border border-dark shadow" style="background-color: orange">
                                                                <label class="h6"><i>&nbsp Lím. presión</i></label>
                                                            </div>
                                                            <div class="card-body rounded-bottom p-0">
                                                                <div class="input-group" style="width: 100%">
                                                                    <asp:TextBox ID="tbLimitePresion" runat="server" class="cuerpo-editable p-0" Style="text-align: center; width: 70%; margin: 0; padding: 0"></asp:TextBox>
                                                                    <asp:Label ID="tbLimitePresionUNIT" runat="server" class="cuerpo p-0" Style="text-align: center; width: 30%; margin: 0; padding: 0; border-left: 0">bar</asp:Label>
                                                                </div>
                                                                <div class="input-group" style="width: 100%">
                                                                    <asp:Label ID="tbLimitePresionN" runat="server" class="cabecera p-0" Font-Bold="true" Style="text-align: center; width: 25%; margin: 0; padding: 0">MIN</asp:Label>
                                                                    <asp:Label ID="tbLimitePresionNVal" runat="server" class="cuerpo p-0" Style="text-align: center; width: 25%; margin: 0; padding: 0" EnableTheming="false">10%</asp:Label>
                                                                    <asp:Label ID="tbLimitePresionM" runat="server" class="cabecera p-0" Font-Bold="true" Style="text-align: center; width: 25%; margin: 0; padding: 0">MAX</asp:Label>
                                                                    <asp:Label ID="tbLimitePresionMval" runat="server" class="cuerpo p-0" Style="text-align: center; width: 25%; margin: 0; padding: 0"  EnableTheming="false">10%</asp:Label>
                                                                </div>
                                                                <div class="input-group border border-dark" style="width: 100%; background-color: orange">
                                                                    <label class="h6  mb-1 mt-1" style="text-align: center; width: 100%"><i>&nbsp Presión real</i></label>
                                                                </div>
                                                                <div class="input-group" style="width: 100%">
                                                                    <asp:TextBox ID="tbLimitePresionReal" runat="server" class="cuerpo-editable p-0" Style="text-align: center; width: 70%"></asp:TextBox>
                                                                    <asp:Label ID="tbLimitePresionRealUNIT" runat="server" class="cuerpo p-0" Style="text-align: center; width: 30%; margin: 0; padding: 0; border-left: 0">bar</asp:Label>
                                                                </div>
                                                                <div class="input-group" style="width: 100%">
                                                                    <asp:Label ID="tbLimitePresionNReal" runat="server" class="cabecera p-0" Font-Bold="true" Style="text-align: center; width: 25%; margin: 0; padding: 0; width: 25%; border-bottom-left-radius: 5px" >MIN</asp:Label>
                                                                    <asp:Label ID="tbLimitePresionNRealVal" runat="server" class="cuerpo p-0" Style="text-align: center; width: 25%; margin: 0; padding: 0; width: 25%" >10%</asp:Label>
                                                                    <asp:Label ID="tbLimitePresionMReal" runat="server" class="cabecera p-0" Font-Bold="true" Style="text-align: center; width: 25%; margin: 0; padding: 0; width: 25%" >MAX</asp:Label>
                                                                    <asp:Label ID="tbLimitePresionMRealVal" runat="server" class="cuerpo p-0" Style="text-align: center; width: 25%; margin: 0; padding: 0; width: 25%; border-bottom-right-radius: 5px">10%</asp:Label>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="tab-pane fade border border-secondary shadow rounded" id="pills_SECUENCIAL" runat="server" role="tabpanel">
                                                <div class="row ms-1 me-1  mt-1 mb-1">
                                                    <div class="col-lg-6">
                                                        <div class="card border-0 shadow  mt-2">
                                                            <div class="card-header border border-dark shadow" style="background-color: orange">
                                                                <div class="row">
                                                                    <div class="col-lg-9">
                                                                        <label style="font-size: large; font-weight: 600; width: 75%"><i>&nbsp Por cota (mm)</i></label>
                                                                    </div>
                                                                    <div class="col-lg-3 pe-0">
                                                                        <div class="input-group input-group-sm float-end" style="width: 100%">
                                                                            <asp:Label runat="server" class="input-group-text border border-dark" BackColor="lavender" Font-Bold="true" Width="20%">± &nbsp</asp:Label>
                                                                            <asp:Label type="number" runat="server" ID="tbTOLSecCota" class="cuerpo" Style="width: 50%; margin: 0; text-align: center" Font-Italic="true" BackColor="Lavender">10</asp:Label>
                                                                            <asp:Label runat="server" class="input-group-text border border-dark" BackColor="lavender" Font-Bold="true" Width="30%">%</asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <div class="card-body p-0">
                                                                <div class="row ps-25">
                                                                    <div class="col-lg-3 p-0">
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%; background: darkslategray">
                                                                            <asp:Label ID="tituloBoquilla" runat="server" Font-Bold="true" Width="67%" CssClass="cabecera" Style="text-align: center; margin: 0; padding: 0; overflow: visible">BOQUILLA</asp:Label>
                                                                            <asp:Label ID="titulosecuencial1" runat="server" Font-Bold="true" Width="33%" CssClass="cabecera" Style="text-align: center; margin: 0; padding: 0; overflow: visible">1</asp:Label>
                                                                        </div>
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%">
                                                                            <asp:Label ID="lineabrir1secu" runat="server" Font-Bold="true" Width="67%" CssClass="cabecera" Style="text-align: center; margin: 0; padding: 0; overflow: visible">ABRIR 1</asp:Label>
                                                                            <asp:TextBox ID="seqAbrir1_1" runat="server" Width="33%" CssClass="cuerpo-editable" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                        </div>
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%">
                                                                            <asp:Label ID="linecerrar1secu" runat="server" Font-Bold="true" Width="67%" CssClass="cabecera" Style="text-align: center; margin: 0; padding: 0; overflow: visible">CERRAR 1</asp:Label>
                                                                            <asp:TextBox ID="seqCerrar1_1" runat="server" Width="33%" CssClass="cuerpo-editable" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                        </div>
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%">
                                                                            <asp:Label ID="lineabrir2secu" runat="server" Font-Bold="true" Width="67%" CssClass="cabecera" Style="text-align: center; margin: 0; padding: 0; overflow: visible">ABRIR 2</asp:Label>
                                                                            <asp:TextBox ID="seqAbrir2_1" runat="server" Width="33%" CssClass="cuerpo-editable" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                        </div>
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%">
                                                                            <asp:Label ID="linecerrar2secu" runat="server" Font-Bold="true" Width="67%" CssClass="cabecera" Style="text-align: center; margin: 0; padding: 0; overflow: visible">CERRAR 2</asp:Label>
                                                                            <asp:TextBox ID="seqCerrar2_1" runat="server" Width="33%" CssClass="cuerpo-editable" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                        </div>
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%">
                                                                            <asp:Label ID="lineTPresPost" runat="server" Font-Bold="true" Width="67%" CssClass="cabecera" Style="text-align: center; margin: 0; padding: 0; overflow: visible; border-bottom-left-radius: 5px">T.POST.</asp:Label>
                                                                            <asp:TextBox ID="seqTPresPost1" runat="server" Width="33%" CssClass="cuerpo-editable" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-3 p-0">
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%; background: darkslategray">
                                                                            <asp:Label ID="titulosecuencial2" runat="server" Width="34%" CssClass="cabecera" Font-Bold="true" Style="text-align: center; margin: 0; padding: 0; overflow: visible">2</asp:Label>
                                                                            <asp:Label ID="titulosecuencial3" runat="server" Width="33%" CssClass="cabecera" Font-Bold="true" Style="text-align: center; margin: 0; padding: 0; overflow: visible">3</asp:Label>
                                                                            <asp:Label ID="titulosecuencial4" runat="server" Width="33%" CssClass="cabecera" Font-Bold="true" Style="text-align: center; margin: 0; padding: 0; overflow: visible">4</asp:Label>
                                                                        </div>
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%">
                                                                            <asp:TextBox ID="seqAbrir1_2" runat="server" Width="34%" CssClass="cuerpo-editable" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="seqAbrir1_3" runat="server" Width="33%" CssClass="cuerpo-editable" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="seqAbrir1_4" runat="server" Width="33%" CssClass="cuerpo-editable" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                        </div>
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%">
                                                                            <asp:TextBox ID="seqAbrir2_2" runat="server" Width="34%" CssClass="cuerpo-editable" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="seqAbrir2_3" runat="server" Width="33%" CssClass="cuerpo-editable" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="seqAbrir2_4" runat="server" Width="33%" CssClass="cuerpo-editable" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                        </div>
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%">
                                                                            <asp:TextBox ID="seqCerrar1_2" runat="server" Width="34%" CssClass="cuerpo-editable" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="seqCerrar1_3" runat="server" Width="33%" CssClass="cuerpo-editable" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="seqCerrar1_4" runat="server" Width="33%" CssClass="cuerpo-editable" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                        </div>
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%">
                                                                            <asp:TextBox ID="seqCerrar2_2" runat="server" Width="34%" CssClass="cuerpo-editable" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="seqCerrar2_3" runat="server" Width="33%" CssClass="cuerpo-editable" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="seqCerrar2_4" runat="server" Width="33%" CssClass="cuerpo-editable" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                        </div>
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%">
                                                                            <asp:TextBox ID="seqTPresPost2" runat="server" Width="34%" CssClass="cuerpo-editable" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="seqTPresPost3" runat="server" Width="33%" CssClass="cuerpo-editable" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="seqTPresPost4" runat="server" Width="33%" CssClass="cuerpo-editable" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-3 p-0">
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%; background: darkslategray">
                                                                            <asp:Label ID="titulosecuencial5" CssClass="cabecera" runat="server" Width="34%" Font-Bold="true" Style="text-align: center; margin: 0; padding: 0; overflow: visible">5</asp:Label>
                                                                            <asp:Label ID="titulosecuencial6" CssClass="cabecera" runat="server" Width="33%" Font-Bold="true" Style="text-align: center; margin: 0; padding: 0; overflow: visible">6</asp:Label>
                                                                            <asp:Label ID="titulosecuencial7" CssClass="cabecera" runat="server" Width="33%" Font-Bold="true" Style="text-align: center; margin: 0; padding: 0; overflow: visible">7</asp:Label>
                                                                        </div>
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%">
                                                                            <asp:TextBox ID="seqAbrir1_5" CssClass="cuerpo-editable" runat="server" Width="34%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="seqAbrir1_6" CssClass="cuerpo-editable" runat="server" Width="33%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="seqAbrir1_7" CssClass="cuerpo-editable" runat="server" Width="33%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                        </div>
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%">
                                                                            <asp:TextBox ID="seqCerrar1_5" CssClass="cuerpo-editable" runat="server" Width="34%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="seqCerrar1_6" CssClass="cuerpo-editable" runat="server" Width="33%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="seqCerrar1_7" CssClass="cuerpo-editable" runat="server" Width="33%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                        </div>
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%">
                                                                            <asp:TextBox ID="seqAbrir2_5" CssClass="cuerpo-editable" runat="server" Width="34%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="seqAbrir2_6" CssClass="cuerpo-editable" runat="server" Width="33%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="seqAbrir2_7" CssClass="cuerpo-editable" runat="server" Width="33%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                        </div>
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%">
                                                                            <asp:TextBox ID="seqCerrar2_5" CssClass="cuerpo-editable" runat="server" Width="34%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="seqCerrar2_6" CssClass="cuerpo-editable" runat="server" Width="33%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="seqCerrar2_7" CssClass="cuerpo-editable" runat="server" Width="33%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                        </div>
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%">
                                                                            <asp:TextBox ID="seqTPresPost5" CssClass="cuerpo-editable" runat="server" Width="34%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="seqTPresPost6" CssClass="cuerpo-editable" runat="server" Width="33%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="seqTPresPost7" CssClass="cuerpo-editable" runat="server" Width="33%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-3 p-0">
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%; background: darkslategray">
                                                                            <asp:Label ID="titulosecuencial8" Width="34%" class="cabecera" runat="server" Font-Bold="true" Style="text-align: center; margin: 0; padding: 0; overflow: visible">8</asp:Label>
                                                                            <asp:Label ID="titulosecuencial9" Width="33%" class="cabecera" runat="server" Font-Bold="true" Style="text-align: center; margin: 0; padding: 0; overflow: visible">9</asp:Label>
                                                                            <asp:Label ID="titulosecuencial10" Width="33%" class="cabecera" runat="server" Font-Bold="true" Style="text-align: center; margin: 0; padding: 0; overflow: visible">10</asp:Label>
                                                                        </div>
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%">
                                                                            <asp:TextBox ID="seqAbrir1_8" Width="34%" runat="server" class="cuerpo-editable" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="seqAbrir1_9" Width="33%" runat="server" class="cuerpo-editable" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="seqAbrir1_10" Width="33%" runat="server" class="cuerpo-editable" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                        </div>
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%">
                                                                            <asp:TextBox ID="seqCerrar1_8" Width="34%" runat="server" class="cuerpo-editable" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="seqCerrar1_9" Width="33%" runat="server" class="cuerpo-editable" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="seqCerrar1_10" Width="33%" runat="server" class="cuerpo-editable" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                        </div>
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%">
                                                                            <asp:TextBox ID="seqAbrir2_8" Width="34%" runat="server" class="cuerpo-editable" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="seqAbrir2_9" Width="33%" runat="server" class="cuerpo-editable" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="seqAbrir2_10" Width="33%" runat="server" class="cuerpo-editable" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>

                                                                        </div>
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%">
                                                                            <asp:TextBox ID="seqCerrar2_8" Width="34%" runat="server" class="cuerpo-editable" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="seqCerrar2_9" Width="33%" runat="server" class="cuerpo-editable" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="seqCerrar2_10" Width="33%" runat="server" class="cuerpo-editable" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                        </div>
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%">
                                                                            <asp:TextBox ID="seqTPresPost8" Width="34%" runat="server" class="cuerpo-editable" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="seqTPresPost9" Width="33%" runat="server" class="cuerpo-editable" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="seqTPresPost10" Width="33%" runat="server" class="cuerpo-editable" Style="text-align: center; margin: 0; padding: 0; overflow: visible; border-bottom-right-radius: 5px"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="card border-0 shadow mt-2">
                                                            <div class="card-header border border-dark shadow" style="background-color: orange">
                                                                <div class="row">
                                                                    <div class="col-lg-9">
                                                                        <label style="font-size: large; font-weight: 600; width: 75%"><i>&nbsp Por tiempo (s)</i></label>
                                                                    </div>
                                                                    <div class="col-lg-3 pe-0">
                                                                        <div class="input-group input-group-sm float-end" style="width: 100%">
                                                                            <asp:Label runat="server" class="input-group-text border border-dark" BackColor="lavender" Font-Bold="true" Width="20%">± &nbsp</asp:Label>
                                                                            <asp:Label type="number" runat="server" ID="tbTOLSecTiempo" class="cuerpo" Style="width: 50%; margin: 0; text-align: center" Font-Italic="true" BackColor="Lavender">10</asp:Label>
                                                                            <asp:Label runat="server" class="input-group-text border border-dark" BackColor="lavender" Font-Bold="true" Width="30%">%</asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="card-body p-0">
                                                                <div class="row ps-25">
                                                                    <div class="col-lg-3 p-0">
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%; background: darkslategray">
                                                                            <asp:Label ID="tituloBoquillaTiempo" runat="server" Font-Bold="true" Width="67%" CssClass="cabecera" Style="text-align: center; margin: 0; padding: 0; overflow: visible">BOQUILLA</asp:Label>
                                                                            <asp:Label ID="titulosecuencialTiempo1" runat="server" Font-Bold="true" Width="33%" CssClass="cabecera" Style="text-align: center; margin: 0; padding: 0; overflow: visible">1</asp:Label>
                                                                        </div>
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%">
                                                                            <asp:Label ID="lineTiempoRetardo" runat="server" Font-Bold="true" Width="67%" CssClass="cabecera" Style="text-align: center; margin: 0; padding: 0; overflow: visible; border-bottom-left-radius: 5px">RETARDO</asp:Label>
                                                                            <asp:TextBox ID="seqTiempoRetardo_1" runat="server" CssClass="cuerpo-editable" Width="33%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-3 p-0">
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%; background: darkslategray">
                                                                            <asp:Label ID="titulosecuencialTiempo2" runat="server" Font-Bold="true" Width="34%" CssClass="cabecera" Style="text-align: center; margin: 0; padding: 0; overflow: visible">2</asp:Label>
                                                                            <asp:Label ID="titulosecuencialTiempo3" runat="server" Font-Bold="true" Width="33%" CssClass="cabecera" Style="text-align: center; margin: 0; padding: 0; overflow: visible">3</asp:Label>
                                                                            <asp:Label ID="titulosecuencialTiempo4" runat="server" Font-Bold="true" Width="33%" CssClass="cabecera" Style="text-align: center; margin: 0; padding: 0; overflow: visible">4</asp:Label>
                                                                        </div>
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%">
                                                                            <asp:TextBox ID="seqTiempoRetardo_2" runat="server" CssClass="cuerpo-editable" Width="34%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="seqTiempoRetardo_3" runat="server" CssClass="cuerpo-editable" Width="33%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="seqTiempoRetardo_4" runat="server" CssClass="cuerpo-editable" Width="33%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-3 p-0">
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%; background: darkslategray">
                                                                            <asp:Label ID="titulosecuencialTiempo5" CssClass="cabecera" runat="server" Font-Bold="true" Width="34%" Style="text-align: center; margin: 0; padding: 0; overflow: visible">5</asp:Label>
                                                                            <asp:Label ID="titulosecuencialTiempo6" CssClass="cabecera" runat="server" Font-Bold="true" Width="33%" Style="text-align: center; margin: 0; padding: 0; overflow: visible">6</asp:Label>
                                                                            <asp:Label ID="titulosecuencialTiempo7" CssClass="cabecera" runat="server" Font-Bold="true" Width="33%" Style="text-align: center; margin: 0; padding: 0; overflow: visible">7</asp:Label>
                                                                        </div>
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%">
                                                                            <asp:TextBox ID="seqTiempoRetardo_5" CssClass="cuerpo-editable" runat="server" Width="34%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="seqTiempoRetardo_6" CssClass="cuerpo-editable" runat="server" Width="33%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="seqTiempoRetardo_7" CssClass="cuerpo-editable" runat="server" Width="33%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-3 p-0">
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%; background: darkslategray">
                                                                            <asp:Label ID="titulosecuencialTiempo8" CssClass="cabecera" runat="server" Font-Bold="true" Width="34%" Style="text-align: center; margin: 0; padding: 0; overflow: visible">8</asp:Label>
                                                                            <asp:Label ID="titulosecuencialTiempo9" CssClass="cabecera" runat="server" Font-Bold="true" Width="33%" Style="text-align: center; margin: 0; padding: 0; overflow: visible">9</asp:Label>
                                                                            <asp:Label ID="titulosecuencialTiempo10" CssClass="cabecera" runat="server" Font-Bold="true" Width="33%" Style="text-align: center; margin: 0; padding: 0; overflow: visible">10</asp:Label>
                                                                        </div>
                                                                        <div class="input-group input-group-lg p-0" style="width: 100%">
                                                                            <asp:TextBox ID="seqTiempoRetardo_8" CssClass="cuerpo-editable" runat="server" Width="34%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="seqTiempoRetardo_9" CssClass="cuerpo-editable" runat="server" Width="33%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                            <asp:TextBox ID="seqTiempoRetardo_10" CssClass="cuerpo-editable" runat="server" Width="33%" Style="text-align: center; margin: 0; padding: 0; overflow: visible; border-bottom-right-radius: 5px"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row ms-1 me-1 mt-1 mb-1">
                                                    <div class="col-lg-12 mt-1 mb-2">
                                                        <asp:Label ID="SeqAnotacionesTitulo" runat="server" CssClass="h5 mt-3 mb-1" Enabled="false">Anotaciones</asp:Label>
                                                        <asp:TextBox ID="seqAnotaciones" CssClass="form-control border border-secondary shadow" Width="100%" TextMode="MultiLine" Rows="2" runat="server" Style="text-align: center"></asp:TextBox>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="row mt-2">
                                            <h5><i class="bi bi-file-spreadsheet">&nbsp Postpresión</i></h5>
                                            <div class="col-lg-8">
                                                <div class="card border-0 shadow mt-2">
                                                    <div class="card-header border border-dark shadow" style="background-color: orange">
                                                        <div class="row">
                                                            <div class="col-lg-10">
                                                                <label style="font-size: large; font-weight: 600; width: 75%"><i>&nbsp Postpresión</i></label>
                                                            </div>
                                                            <div class="col-lg-2 pe-0">
                                                                <div class="input-group input-group-sm float-end" style="width: 100%">
                                                                    <asp:Label runat="server" class="input-group-text border border-dark" BackColor="lavender" Font-Bold="true" Width="20%">± &nbsp</asp:Label>
                                                                    <asp:Label type="number" runat="server" ID="tbTOLPostPresion" class="cuerpo" Style="width: 50%; margin: 0; text-align: center" Font-Italic="true" BackColor="Lavender">10</asp:Label>
                                                                    <asp:Label runat="server" class="input-group-text border border-dark" BackColor="lavender" Font-Bold="true" Width="30%">%</asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="card-body border border-dark p-0">
                                                        <div class="card-body p-0">
                                                            <div class="row ps-25">
                                                                <div class="col-lg-3 p-0">
                                                                    <div class="input-group input-group-lg p-0" style="width: 100%; background: darkslategray">
                                                                        <asp:Label ID="tbPasoPresion" Width="50%" CssClass="cabecera" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">PASO</asp:Label>
                                                                        <asp:Label ID="tbP1" Width="25%" CssClass="cabecera" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">1</asp:Label>
                                                                        <asp:Label ID="tbP2" Width="25%" CssClass="cabecera" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">2</asp:Label>
                                                                    </div>
                                                                    <div class="input-group input-group-lg p-0" style="width: 100%">
                                                                        <asp:Label ID="thPresion" Width="50%" CssClass="cabecera" runat="server" Font-Bold="true" Style="text-align: center; margin: 0; padding: 0; overflow: visible">PRESIÓN</asp:Label>
                                                                        <asp:TextBox ID="thP1" Width="25%" CssClass="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                        <asp:TextBox ID="thP2" Width="25%" CssClass="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                    </div>
                                                                    <div class="input-group input-group-lg p-0" style="width: 100%">
                                                                        <asp:Label ID="thTPtiempo" Width="50%" CssClass="cabecera" runat="server" Font-Bold="true" Style="text-align: center; margin: 0; padding: 0; overflow: visible">TIEMPO</asp:Label>
                                                                        <asp:TextBox ID="thTP1" Width="25%" CssClass="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                        <asp:TextBox ID="thTP2" Width="25%" CssClass="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-3 p-0">
                                                                    <div class="input-group input-group-lg p-0" style="width: 100%; background: darkslategray">
                                                                        <asp:Label ID="tbP3" Width="34%" CssClass="cabecera" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">3</asp:Label>
                                                                        <asp:Label ID="tbP4" Width="33%" CssClass="cabecera" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">4</asp:Label>
                                                                        <asp:Label ID="tbP5" Width="33%" CssClass="cabecera" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">5</asp:Label>
                                                                    </div>
                                                                    <div class="input-group input-group-lg p-0" style="width: 100%">
                                                                        <asp:TextBox ID="thP3" Width="34%" CssClass="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                        <asp:TextBox ID="thP4" Width="33%" CssClass="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                        <asp:TextBox ID="thP5" Width="33%" CssClass="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                    </div>
                                                                    <div class="input-group input-group-lg p-0" style="width: 100%">
                                                                        <asp:TextBox ID="thTP3" Width="34%" CssClass="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                        <asp:TextBox ID="thTP4" Width="33%" CssClass="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                        <asp:TextBox ID="thTP5" Width="33%" CssClass="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-3 p-0">
                                                                    <div class="input-group input-group-lg p-0" style="width: 100%; background: darkslategray">
                                                                        <asp:Label ID="tbP6" Width="34%" CssClass="cabecera" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">6</asp:Label>
                                                                        <asp:Label ID="tbP7" Width="33%" CssClass="cabecera" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">7</asp:Label>
                                                                        <asp:Label ID="tbP8" Width="33%" CssClass="cabecera" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">8</asp:Label>
                                                                    </div>
                                                                    <div class="input-group input-group-lg p-0" style="width: 100%">
                                                                        <asp:TextBox ID="thP6" Width="34%" CssClass="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                        <asp:TextBox ID="thP7" Width="33%" CssClass="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                        <asp:TextBox ID="thP8" Width="33%" CssClass="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                    </div>
                                                                    <div class="input-group input-group-lg p-0" style="width: 100%">
                                                                        <asp:TextBox ID="thTP6" Width="34%" CssClass="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                        <asp:TextBox ID="thTP7" Width="33%" CssClass="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                        <asp:TextBox ID="thTP8" Width="33%" CssClass="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-3 p-0">
                                                                    <div class="input-group input-group-lg p-0" style="width: 100%; background: darkslategray">
                                                                        <asp:Label ID="tbP9" Width="30%" CssClass="cabecera" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">9</asp:Label>
                                                                        <asp:Label ID="tbP10" Width="30%" CssClass="cabecera" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">10</asp:Label>
                                                                        <asp:Label ID="tbPUNIT" Width="40%" CssClass="cabecera" runat="server" Font-Bold="true" Style="text-align: center; margin: 0; padding: 0; overflow: visible">-</asp:Label>
                                                                    </div>
                                                                    <div class="input-group input-group-lg p-0" style="width: 100%">
                                                                        <asp:TextBox ID="thP9" Width="30%" CssClass="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                        <asp:TextBox ID="thP10" Width="30%" CssClass="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                        <asp:Label ID="thPresionUNIDAD" Width="40%" CssClass="cabecera" runat="server" Font-Bold="true" Style="text-align: center; margin: 0; padding: 0; overflow: visible">Bar</asp:Label>
                                                                    </div>
                                                                    <div class="input-group input-group-lg p-0" style="width: 100%">
                                                                        <asp:TextBox ID="thTP9" Width="30%" CssClass="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                        <asp:TextBox ID="thTP10" Width="30%" CssClass="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                                        <asp:Label ID="thTPtiempoUnidad" Width="40%" CssClass="cabecera" runat="server" Font-Bold="true" Style="text-align: center; margin: 0; padding: 0; overflow: visible">s</asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                
                                                </div>
                                            </div>
                                            <div class="col-lg-2">
                                                <div class="card border-0 shadow mt-2">
                                                    <div class="card-header border border-dark shadow" style="background-color: orange">
                                                        <label class="h6"><i>Conmutación</i></label>
                                                    </div>
                                                    <div class="card-body rounded-bottom p-0">
                                                        <div class="input-group" style="width: 100%">
                                                            <asp:TextBox ID="tbConmutacion" runat="server" class="cuerpo-editable p-0" Style="text-align: center; width: 70%"></asp:TextBox>
                                                            <asp:Label ID="tbConmutacionUnit" runat="server" class="cuerpo p-0" Style="text-align: center; width: 30%; margin: 0; padding: 0; border-left: 0">mm</asp:Label>
                                                        </div>
                                                        <div class="input-group" style="width: 100%">
                                                            <asp:Label ID="thConmuntaciontolN" runat="server" class="cabecera p-0" Font-Bold="true" Style="text-align: center; width: 25%; margin: 0; padding: 0; width: 25%; border-bottom-left-radius: 5px">MIN</asp:Label>
                                                            <asp:TextBox ID="thConmuntaciontolNVal" class="cuerpo p-0" runat="server" Style="text-align: center; width: 25%; margin: 0; padding: 0; width: 25%; width: 25%">10%</asp:TextBox>
                                                            <asp:Label ID="thConmuntaciontolM" class="cabecera p-0" runat="server" Font-Bold="true" Style="text-align: center; width: 25%; margin: 0; padding: 0; width: 25%; width: 25%">MAX</asp:Label>
                                                            <asp:TextBox ID="thConmuntaciontolMVal" class="cuerpo p-0" runat="server" Style="text-align: center; width: 25%; margin: 0; padding: 0; width: 25%; border-bottom-right-radius: 5px">10%</asp:TextBox>
                                                        </div>
                                                         </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-2">
                                                <div class="card border-0 shadow mt-2">
                                                    <div class="card-header border border-dark shadow" style="background-color: orange">
                                                        <label class="h6"><i>Tiempo presión</i></label>
                                                    </div>
                                                    <div class="card-body rounded-bottom p-0">
                                                        <div class="input-group" style="width: 100%">
                                                            <asp:TextBox ID="tbTiempoPresion" runat="server" class="cuerpo-editable p-0" Style="text-align: center; width: 70%"></asp:TextBox>
                                                            <asp:Label ID="tbTiempoPresionUNIT" runat="server" class="cuerpo p-0" Style="text-align: center; width: 30%; margin: 0; padding: 0; border-left: 0">s</asp:Label>
                                                        </div>
                                                        <div class="input-group" style="width: 100%">
                                                            <asp:Label ID="tbTiempoPresiontolN" runat="server" class="cabecera p-0" Font-Bold="true" Style="text-align: center; width: 25%; margin: 0; padding: 0; width: 25%; border-bottom-left-radius: 5px">MIN</asp:Label>
                                                            <asp:Label ID="tbTiempoPresiontolNVal" runat="server" class="cuerpo p-0" Style="text-align: center; width: 25%; margin: 0; padding: 0; width: 25%; width: 25%">10%</asp:Label>
                                                            <asp:Label ID="tbTiempoPresiontolM" runat="server" class="cabecera p-0" Font-Bold="true" Style="text-align: center; width: 25%; margin: 0; padding: 0; width: 25%">MAX</asp:Label>
                                                            <asp:Label ID="tbTiempoPresiontolMVal" runat="server" class="cuerpo p-0" Style="text-align: center; width: 25%; margin: 0; padding: 0; width: 25%; border-bottom-right-radius: 5px">10%</asp:Label>
                                                        </div>
                                                       </div>
                                                </div>
                                            </div>
                                        </div>
                                        <%--PRESIONES --%>
                                        <div class="row mt-3">
                                            <h5><i class="bi bi-file-spreadsheet">&nbsp Dosificación</i></h5>
                                            <div class="row">
                                                <div class="col-lg-2">
                                                    <div class="card shadow mt-2">
                                                        <div class="card-header border border-dark shadow" style="background-color: orange">
                                                            <label class="h6"><i>Cojín</i></label>
                                                        </div>
                                                        <div class="card-body rounded-bottom p-0">
                                                            <div class="input-group p-0" style="width: 100%">
                                                                <asp:TextBox ID="thCojin" runat="server" class="cuerpo-editable" Style="text-align: center; width: 70%"></asp:TextBox>
                                                                <asp:Label ID="thCojinUnit" runat="server" class="cuerpo p-0" Style="text-align: center; width: 30%; margin: 0; padding: 0; border-left: 0">mm</asp:Label>
                                                            </div>
                                                            <div class="input-group p-0" style="width: 100%">
                                                                <asp:Label ID="TNCojin" runat="server" class="cabecera p-0" Font-Bold="true" Style="text-align: center; width: 25%; margin: 0; padding: 0; border-bottom-left-radius: 5px">MIN</asp:Label>
                                                                <asp:Label ID="TNCojinval" runat="server" class="cuerpo p-0" Style="text-align: center; width: 25%; margin: 0; padding: 0">10%</asp:Label>
                                                                <asp:Label ID="TMCojin" runat="server" class="cabecera p-0" Font-Bold="true" Style="text-align: center; width: 25%; margin: 0; padding: 0">MAX</asp:Label>
                                                                <asp:Label ID="TMCojinval" runat="server" class="cuerpo p-0" Style="text-align: center; width: 25%; margin: 0; padding: 0; border-bottom-right-radius: 5px">10%</asp:Label>
                                                            </div>
                                                           </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-2">
                                                    <div class="card   shadow mt-2">
                                                        <div class="card-header border border-dark shadow" style="background-color: orange">
                                                            <label class="h6"><i>Carga</i></label>
                                                        </div>
                                                        <div class="card-body rounded-bottom p-0">
                                                            <div class="input-group p-0" style="width: 100%">
                                                                <asp:TextBox ID="thCarga" runat="server" class="cuerpo-editable" Style="text-align: center; width: 70%"></asp:TextBox>
                                                                <asp:Label ID="thCargaUnit" runat="server" class="cuerpo p-0" Style="text-align: center; width: 30%; margin: 0; padding: 0; border-left: 0">mm</asp:Label>
                                                            </div>

                                                            <div class="input-group" style="width: 100%">
                                                                <asp:Label ID="TNcarga" runat="server" class="cabecera p-0" Font-Bold="true" Style="text-align: center; width: 25%; margin: 0; padding: 0; width: 25%; border-bottom-left-radius: 5px">MIN</asp:Label>
                                                                <asp:Label ID="TNcargaval" runat="server" class="cuerpo p-0" Style="text-align: center; width: 25%; margin: 0; padding: 0; width: 25%">10%</asp:Label>
                                                                <asp:Label ID="TMcarga" runat="server" class="cabecera p-0" Font-Bold="true" Style="text-align: center; width: 25%; margin: 0; padding: 0; width: 25%">MAX</asp:Label>
                                                                <asp:Label ID="TMcargaval" runat="server" class="cuerpo p-0" Style="text-align: center; width: 25%; margin: 0; padding: 0; width: 25%; border-bottom-right-radius: 5px">10%</asp:Label>
                                                            </div>
                                                            </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-2">
                                                    <div class="card   shadow mt-2">
                                                        <div class="card-header border border-dark shadow" style="background-color: orange">
                                                            <label class="h6"><i>V. Carga</i></label>
                                                        </div>
                                                        <div class="card-body rounded-bottom p-0">
                                                            <div class="input-group p-0" style="width: 100%">
                                                                <asp:TextBox ID="thVCarga" runat="server" class="cuerpo-editable" Style="text-align: center; width: 70%"></asp:TextBox>
                                                                <asp:Label ID="thVCargaUnit" runat="server" class="cuerpo p-0" Style="text-align: center; width: 30%; margin: 0; padding: 0; border-left: 0">%</asp:Label>
                                                            </div>
                                                            <div class="input-group" style="width: 100%">
                                                                <asp:Label ID="TNvcarga" runat="server" class="cabecera p-0" Font-Bold="true" Style="text-align: center; width: 25%; margin: 0; padding: 0; width: 25%; border-bottom-left-radius: 5px">MIN</asp:Label>
                                                                <asp:Label ID="TNvcargaval" runat="server" class="cuerpo p-0" Style="text-align: center; width: 25%; margin: 0; padding: 0; width: 25%">10%</asp:Label>
                                                                <asp:Label ID="TMvcarga" runat="server" class="cabecera p-0" Font-Bold="true" Style="text-align: center; width: 25%; margin: 0; padding: 0; width: 25%">MAX</asp:Label>
                                                                <asp:Label ID="TMvcargaval" runat="server" class="cuerpo p-0" Style="text-align: center; width: 25%; margin: 0; padding: 0; width: 25%; border-bottom-right-radius: 5px">10%</asp:Label>
                                                            </div>
                                                          </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-2">
                                                    <div class="card shadow mt-2">
                                                        <div class="card-header border border-dark shadow" style="background-color: orange">
                                                            <label class="h6"><i>Descompresión</i></label>
                                                        </div>
                                                        <div class="card-body rounded-bottom p-0">
                                                            <div class="input-group" style="width: 100%">
                                                                <asp:TextBox ID="thDescomp" runat="server" class="cuerpo-editable" Style="text-align: center; width: 70%"></asp:TextBox>
                                                                <asp:Label ID="thDescompUNIT" runat="server" class="cuerpo p-0" Style="text-align: center; width: 30%; margin: 0; padding: 0; border-left: 0">mm</asp:Label>
                                                            </div>
                                                            <div class="input-group" style="width: 100%">
                                                                <asp:Label ID="TNdescom" runat="server" class="cabecera p-0" Font-Bold="true" Style="text-align: center; width: 25%; margin: 0; padding: 0; width: 25%; border-bottom-left-radius: 5px">MIN</asp:Label>
                                                                <asp:Label ID="TNdescomval" runat="server" class="cuerpo p-0" Style="text-align: center; width: 25%; margin: 0; padding: 0; width: 25%">10%</asp:Label>
                                                                <asp:Label ID="TMdescom" runat="server" class="cabecera p-0" Font-Bold="true" Style="text-align: center; width: 25%; margin: 0; padding: 0; width: 25%">MAX</asp:Label>
                                                                <asp:Label ID="TMdescomval" runat="server" class="cuerpo p-0" Style="text-align: center; width: 25%; margin: 0; padding: 0; width: 25%; border-bottom-right-radius: 5px">10%</asp:Label>
                                                            </div>
                                                            </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-2">
                                                    <div class="card shadow mt-2">
                                                        <div class="card-header border border-dark shadow" style="background-color: orange">
                                                            <label class="h6"><i>Contrapresión</i></label>
                                                        </div>
                                                        <div class="card-body rounded-bottom p-0">
                                                            <div class="input-group" style="width: 100%">
                                                                <asp:TextBox ID="thContrapr" runat="server" class="cuerpo-editable" Style="text-align: center; width: 70%"></asp:TextBox>
                                                                <asp:Label ID="thContraprUNIT" runat="server" class="cuerpo p-0" Style="text-align: center; width: 30%; margin: 0; padding: 0; border-left: 0">bar</asp:Label>
                                                            </div>
                                                            <div class="input-group" style="width: 100%">
                                                                <asp:Label ID="TNcontrap" runat="server" class="cabecera p-0" Font-Bold="true" Style="text-align: center; width: 25%; margin: 0; padding: 0; width: 25%; border-bottom-left-radius: 5px">MIN</asp:Label>
                                                                <asp:Label ID="TNcontrapval" runat="server" class="cuerpo p-0" Style="text-align: center; width: 25%; margin: 0; padding: 0; width: 25%">10%</asp:Label>
                                                                <asp:Label ID="TMcontrap" runat="server" class="cabecera p-0" Font-Bold="true" Style="text-align: center; width: 25%; margin: 0; padding: 0; width: 25%">MAX</asp:Label>
                                                                <asp:Label ID="TMcontrapval" runat="server" class="cuerpo p-0" Style="text-align: center; width: 25%; margin: 0; padding: 0; width: 25%; border-bottom-right-radius: 5px">10%</asp:Label>
                                                            </div>
                                                             </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <h5><i class="bi bi-hourglass-split">&nbsp Tiempos</i></h5>
                                                <div class="col-lg-2">
                                                    <div class="card border-0 shadow mt-2">
                                                        <div class="card-header border border-dark shadow" style="background-color: orange">
                                                            <label class="h6"><i>Dosificacion</i></label>
                                                        </div>
                                                        <div class="card-body rounded-bottom p-0">
                                                            <div class="input-group" style="width: 100%">
                                                                <asp:TextBox ID="thTiempo" runat="server" class="cuerpo-editable" Style="text-align: center; width: 70%"></asp:TextBox>
                                                                <asp:Label ID="thTiempoUNIT" runat="server" class="cuerpo p-0" Style="text-align: center; width: 30%; margin: 0; padding: 0; border-left: 0">s</asp:Label>
                                                            </div>
                                                            <div class="input-group" style="width: 100%">
                                                                <asp:Label ID="TNTiempdos" runat="server" class="cabecera p-0" Font-Bold="true" Style="text-align: center; width: 25%; margin: 0; padding: 0; width: 25%; border-bottom-left-radius: 5px">MIN</asp:Label>
                                                                <asp:Label ID="TNTiempdosval" runat="server" class="cuerpo p-0" Style="text-align: center; width: 25%; margin: 0; padding: 0; width: 25%">10%</asp:Label>
                                                                <asp:Label ID="TMTiempdos" runat="server" class="cabecera p-0" Font-Bold="true" Style="text-align: center; width: 25%; margin: 0; padding: 0; width: 25%">MAX</asp:Label>
                                                                <asp:Label ID="TMTiempdosval" runat="server" class="cuerpo p-0" Style="text-align: center; width: 25%; margin: 0; padding: 0; width: 25%; border-bottom-right-radius: 5px">10%</asp:Label>
                                                            </div>
                                                            </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-2">
                                                    <div class="card border-0 shadow mt-2">
                                                        <div class="card-header border border-dark shadow" style="background-color: orange">
                                                            <label class="h6"><i>Enfriamiento</i></label>
                                                        </div>
                                                        <div class="card-body rounded-bottom p-0">
                                                            <div class="input-group" style="width: 100%">
                                                                <asp:TextBox ID="thEnfriamiento" runat="server" class="cuerpo-editable" Style="text-align: center; width: 70%"></asp:TextBox>
                                                                <asp:Label ID="thEnfriamientoUNIT" runat="server" class="cuerpo p-0" Style="text-align: center; width: 30%; margin: 0; padding: 0; border-left: 0">s</asp:Label>
                                                            </div>
                                                            <div class="input-group" style="width: 100%">
                                                                <asp:Label ID="TNEnfriam" runat="server" class="cabecera p-0" Font-Bold="true" Style="text-align: center; width: 25%; margin: 0; padding: 0; width: 25%; border-bottom-left-radius: 5px">MIN</asp:Label>
                                                                <asp:Label ID="TNEnfriamval" runat="server" class="cuerpo p-0" Style="text-align: center; width: 25%; margin: 0; padding: 0; width: 25%">10%</asp:Label>
                                                                <asp:Label ID="TMEnfriam" runat="server" class="cabecera p-0" Font-Bold="true" Style="text-align: center; width: 25%; margin: 0; padding: 0; width: 25%">MAX</asp:Label>
                                                                <asp:Label ID="TMEnfriamval" runat="server" class="cuerpo p-0" Style="text-align: center; width: 25%; margin: 0; padding: 0; width: 25%; border-bottom-right-radius: 5px">10%</asp:Label>
                                                            </div>
                                                          </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-2">
                                                    <div class="card border-0 shadow mt-2">
                                                        <div class="card-header border border-dark shadow" style="background-color: orange">
                                                            <label class="h6"><i>Ciclo</i></label>
                                                        </div>
                                                        <div class="card-body rounded-bottom p-0">
                                                            <div class="input-group" style="width: 100%">
                                                                <asp:TextBox ID="thCiclo" runat="server" class="cuerpo-editable p-0" Style="text-align: center; width: 70%"></asp:TextBox>
                                                                <asp:Label ID="thCicloUnit" runat="server" class="cuerpo p-0" Style="text-align: center; width: 30%; margin: 0; padding: 0; border-left: 0">s</asp:Label>
                                                            </div>
                                                            <div class="input-group" style="width: 100%">
                                                                <asp:Label ID="TNCiclo" runat="server" class="cabecera p-0" Font-Bold="true" Style="text-align: center; width: 25%; margin: 0; padding: 0; width: 25%; border-bottom-left-radius: 5px">MIN</asp:Label>
                                                                <asp:Label ID="TNCicloval" runat="server" class="cuerpo p-0" Style="text-align: center; width: 25%; margin: 0; padding: 0; width: 25%">10%</asp:Label>
                                                                <asp:Label ID="TMCiclo" runat="server" class="cabecera p-0" Font-Bold="true" Style="text-align: center; width: 25%; margin: 0; padding: 0; width: 25%">MAX</asp:Label>
                                                                <asp:Label ID="TMCicloval" runat="server" class="cuerpo p-0" Style="text-align: center; width: 25%; margin: 0; padding: 0; width: 25%; border-bottom-right-radius: 5px">10%</asp:Label>
                                                            </div>
                                                           </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row mt-3">
                                            <div class="col-lg-9">
                                                <h5><i class="bi bi-check-square">&nbsp Marcad lo que corresponda</i></h5>
                                                <div class="row">
                                                    <div class="col-lg-4">
                                                        <div class="form-check">
                                                            <input class="form-check-input border border-dark" type="checkbox" runat="server" value="" id="cbNoyos">
                                                            <label style="font-style: italic">Noyos</label>
                                                        </div>
                                                        <div class="form-check">
                                                            <input class="form-check-input border border-dark" type="checkbox" runat="server" value="" id="cbHembra">
                                                            <label style="font-style: italic">Hembra dos</label>
                                                        </div>
                                                        <div class="form-check">
                                                            <input class="form-check-input border border-dark" type="checkbox" runat="server" value="" id="cbMacho">
                                                            <label style="font-style: italic">Macho</label>
                                                        </div>
                                                        <div class="form-check">
                                                            <input class="form-check-input border border-dark" type="checkbox" runat="server" value="" id="cbAntesExpul">
                                                            <label style="font-style: italic">Antes expuls.</label>
                                                        </div>
                                                        <div class="form-check">
                                                            <input class="form-check-input border border-dark" type="checkbox" runat="server" value="" id="cbAntesApert">
                                                            <label style="font-style: italic">Antes apert.</label>
                                                        </div>
                                                        <div class="form-check">
                                                            <input class="form-check-input border border-dark" type="checkbox" runat="server" value="" id="cbAntesCierre">
                                                            <label style="font-style: italic">Recoger antes cierre</label>
                                                        </div>
                                                        <div class="form-check">
                                                            <input class="form-check-input border border-dark" type="checkbox" runat="server" value="" id="cbDespuesCierre">
                                                            <label style="font-style: italic">Recoger después cierre</label>
                                                        </div>
                                                        <div class="form-check">
                                                            <input class="form-check-input border border-dark" type="checkbox" runat="server" value="" id="cbOtros1">
                                                            <label style="font-style: italic">Otros</label>
                                                        </div>

                                                    </div>
                                                    <div class="col-lg-4">
                                                        <div class="form-check">
                                                            <input class="form-check-input border border-dark" type="checkbox" runat="server" value="" id="cbBoquilla">
                                                            <label style="font-style: italic">Boquilla</label>
                                                        </div>
                                                        <div class="form-check">
                                                            <input class="form-check-input border border-dark" type="checkbox" runat="server" value="" id="cbCono">
                                                            <label style="font-style: italic">Cono</label>
                                                        </div>
                                                        <div class="form-check">
                                                            <input class="form-check-input border border-dark" type="checkbox" runat="server" value="" id="cbRadioLarga">
                                                            <label style="font-style: italic">Radio larga</label>

                                                        </div>
                                                        <div class="form-check">
                                                            <input class="form-check-input border border-dark" type="checkbox" runat="server" value="" id="cbLibre">
                                                            <label style="font-style: italic">Libre</label>
                                                        </div>
                                                        <div class="form-check">
                                                            <input class="form-check-input border border-dark" type="checkbox" runat="server" value="" id="cbValvula">
                                                            <label style="font-style: italic">Con válvula</label>

                                                        </div>
                                                        <div class="form-check">
                                                            <input class="form-check-input border border-dark" type="checkbox" runat="server" value="" id="cbResistencia">
                                                            <label style="font-style: italic">Con resistencia</label>
                                                        </div>
                                                        <div class="form-check">
                                                            <input class="form-check-input border border-dark" type="checkbox" runat="server" value="" id="cbOtros2">
                                                            <label style="font-style: italic">Otros</label>
                                                        </div>

                                                    </div>
                                                    <div class="col-lg-4">
                                                        <div class="form-check">
                                                            <input class="form-check-input border border-dark" type="checkbox" runat="server" value="" id="cbExpulsion">
                                                            <label style="font-style: italic">Expulsión</label>
                                                        </div>
                                                        <div class="form-check">
                                                            <input class="form-check-input border border-dark" type="checkbox" runat="server" value="" id="cbHidraulica">
                                                            <label style="font-style: italic">Hidráulica</label>
                                                        </div>
                                                        <div class="form-check">
                                                            <input class="form-check-input border border-dark" type="checkbox" runat="server" value="" id="cbNeumatica">
                                                            <label style="font-style: italic">Neumática</label>

                                                        </div>
                                                        <div class="form-check">
                                                            <input class="form-check-input border border-dark" type="checkbox" runat="server" value="" id="cbNormal">
                                                            <label style="font-style: italic">Normal(choque)</label>
                                                        </div>
                                                        <div class="form-check">
                                                            <input class="form-check-input border border-dark" type="checkbox" runat="server" value="" id="cbArandela125">
                                                            <label style="font-style: italic">Arandela centr. 125</label>
                                                        </div>
                                                        <div class="form-check">
                                                            <input class="form-check-input border border-dark" type="checkbox" runat="server" value="" id="cbArandela160">
                                                            <label style="font-style: italic">Arandela centr. 160</label>
                                                        </div>
                                                        <div class="form-check">
                                                            <input class="form-check-input border border-dark" type="checkbox" runat="server" value="" id="cbArandela200">
                                                            <label style="font-style: italic">Arandela centr. 200</label>
                                                        </div>
                                                        <div class="form-check">
                                                            <input class="form-check-input border border-dark" type="checkbox" runat="server" value="" id="cbArandela250">
                                                            <label style="font-style: italic">Arandela centr. 250</label>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3">
                                                <h5><i class="bi bi-journal">&nbsp Notas:</i></h5>
                                                <asp:TextBox ID="MarcasOtrosText" CssClass="cuerpo-editable shadow" runat="server" Rows="7" Width="100%" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row mt-3">
                                            <h5><i class="bi bi-card-list">&nbsp Instrucciones de arranque</i></h5>
                                            <div class="col-lg-2"></div>
                                            <div class="col-lg-8">
                                                <div class="input-group input-group-sm shadow">
                                                    <span class="cabecera" style="width: 10%; font-weight: bold; border-bottom-left-radius: 5px; border-top-left-radius: 5px">1</span>
                                                    <asp:TextBox ID="TbOperacionText1" runat="server" CssClass="cuerpo-editable" Style="text-align: center; width: 90%" ></asp:TextBox>
                                                </div>
                                                <div class="input-group input-group-sm shadow">
                                                    <span class="cabecera" style="width: 10%; font-weight: bold; border-bottom-left-radius: 5px; border-top-left-radius: 5px">2</span>
                                                    <asp:TextBox ID="TbOperacionText2" runat="server" CssClass="cuerpo-editable" Style="text-align: center; width: 90%" ></asp:TextBox>
                                                </div>
                                                <div class="input-group input-group-sm shadow">
                                                    <span class="cabecera" style="width: 10%; font-weight: bold; border-bottom-left-radius: 5px; border-top-left-radius: 5px">3</span>
                                                    <asp:TextBox ID="TbOperacionText3" runat="server" CssClass="cuerpo-editable" Style="text-align: center; width: 90%" ></asp:TextBox>
                                                </div>
                                                <div class="input-group input-group-sm shadow">
                                                    <span class="cabecera" style="width: 10%; font-weight: bold; border-bottom-left-radius: 5px; border-top-left-radius: 5px">4</span>
                                                    <asp:TextBox ID="TbOperacionText4" runat="server" CssClass="cuerpo-editable" Style="text-align: center; width: 90%"></asp:TextBox>
                                                </div>
                                                <div class="input-group input-group-sm shadow">
                                                    <span class="cabecera" style="width: 10%; font-weight: bold; border-bottom-left-radius: 5px; border-top-left-radius: 5px">5</span>
                                                    <asp:TextBox ID="TbOperacionText5" runat="server" CssClass="cuerpo-editable" Style="text-align: center; width: 90%"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-2"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="pills_ATEMPERADO" runat="server" role="tabpanel" aria-labelledby="pills-profile-tab">
                            <div class="card border-0">
                                <div class="card-footer border border-dark border-top-0" style="background-color: whitesmoke">
                                    <div class="row">
                                        <div class="col-lg-6 pe-0">
                                            <div class="card border-0 shadow mt-2">
                                                <div class="card-header border border-dark shadow" style="background-color: darkorange">
                                                    <label class="h6"><i>PARTE FIJA</i></label>
                                                </div>
                                                <div class="card-body border border-dark rounded-bottom p-0">
                                                    <div class="row">
                                                        <div class="col-md-3"></div>
                                                        <div class="col-md-6">
                                                            <asp:HyperLink ID="hyperlink1" NavigateUrl="" ImageUrl="" Width="100%" ImageWidth="100%" Text="Lado opuesto" Target="_new" runat="server" class="rounded rounded-2 border border-dark shadow mt-2" />
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="FileUpload1" runat="server" class="form-control form-control-sm border border-dark"></asp:FileUpload>
                                                                <button id="Botonsubida1" runat="server" onserverclick="Insertar_foto" type="button" class="btn btn-sm border border-dark">Subir</button>
                                                            </div>
                                                            <h6>Vista general</h6>
                                                        </div>
                                                        <div class="col-md-3"></div>

                                                    </div>
                                                    <div class="row ps-25">
                                                        <div class="input-group input-group-sm" style="margin: 0; padding: 0; overflow: visible; width: 100%">
                                                            <span class="cabecera" style="text-align: center; overflow: visible; width: 25%; border-bottom-left-radius: 0; border-top-left-radius: 5px; font-weight: bold">TIPO DE AGUA</span>
                                                            <asp:DropDownList ID="AtempTipoF" runat="server" Width="75%" Style="text-align: center; border-bottom-left-radius: 0; border-bottom-right-radius: 0; font-weight: bold" CssClass="form-select border border-dark"></asp:DropDownList>
                                                        </div>
                                                        <div class="col-lg-3 ps-0 pe-0">
                                                            <asp:TextBox ID="ThCircuitoF" Width="100%" class="cabecera" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">Nº CIRCUITO</asp:TextBox>
                                                            <asp:DropDownList ID="TbCircuitoF1" Width="100%" Height="26px" class="cuerpo-editable" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:DropDownList>
                                                            <asp:DropDownList ID="TbCircuitoF2" Width="100%" Height="26px" class="cuerpo-editable" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:DropDownList>
                                                            <asp:DropDownList ID="TbCircuitoF3" Width="100%" Height="26px" class="cuerpo-editable" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:DropDownList>
                                                            <asp:DropDownList ID="TbCircuitoF4" Width="100%" Height="26px" class="cuerpo-editable" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:DropDownList>
                                                            <asp:DropDownList ID="TbCircuitoF5" Width="100%" Height="26px" class="cuerpo-editable" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:DropDownList>
                                                            <asp:DropDownList ID="TbCircuitoF6" Width="100%" Height="26px" class="cuerpo-editable" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible; border-bottom-left-radius: 5px"></asp:DropDownList>
                                                        </div>
                                                        <div class="col-lg-3 ps-0 pe-0">
                                                            <asp:TextBox ID="ThCaudalF" Width="100%" class="cabecera" runat="server" Font-Bold="true" Style="text-align: center; margin: 0; padding: 0; overflow: visible">CAUDAL</asp:TextBox>
                                                            <asp:TextBox ID="TbCaudalF1" Width="100%" Height="26px" class="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                            <asp:TextBox ID="TbCaudalF2" Width="100%" Height="26px" class="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                            <asp:TextBox ID="TbCaudalF3" Width="100%" Height="26px" class="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                            <asp:TextBox ID="TbCaudalF4" Width="100%" Height="26px" class="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                            <asp:TextBox ID="TbCaudalF5" Width="100%" Height="26px" class="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                            <asp:TextBox ID="TbCaudalF6" Width="100%" Height="26px" class="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                        </div>
                                                        <div class="col-lg-3 ps-0 pe-0">
                                                            <asp:TextBox ID="ThTemperaturaF" Width="100%" class="cabecera" runat="server" Font-Bold="true" Style="text-align: center; margin: 0; padding: 0; overflow: visible">TEMPERATURA</asp:TextBox>
                                                            <asp:TextBox ID="TbTemperaturaF1" Width="100%" Height="26px" class="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                            <asp:TextBox ID="TbTemperaturaF2" Width="100%" Height="26px" class="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                            <asp:TextBox ID="TbTemperaturaF3" Width="100%" Height="26px" class="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                            <asp:TextBox ID="TbTemperaturaF4" Width="100%" Height="26px" class="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                            <asp:TextBox ID="TbTemperaturaF5" Width="100%" Height="26px" class="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                            <asp:TextBox ID="TbTemperaturaF6" Width="100%" Height="26px" class="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                        </div>
                                                        <div class="col-lg-3 ps-0 pe-0">
                                                            <asp:TextBox ID="ThEntradaF" Width="100%" class="cabecera" runat="server" Font-Bold="true" Style="text-align: center; margin: 0; padding: 0; overflow: visible">ENTRADA</asp:TextBox>
                                                            <asp:DropDownList ID="TbEntradaF1" Width="100%" Height="26px" class="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:DropDownList>
                                                            <asp:DropDownList ID="TbEntradaF2" Width="100%" Height="26px" class="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:DropDownList>
                                                            <asp:DropDownList ID="TbEntradaF3" Width="100%" Height="26px" class="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:DropDownList>
                                                            <asp:DropDownList ID="TbEntradaF4" Width="100%" Height="26px" class="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:DropDownList>
                                                            <asp:DropDownList ID="TbEntradaF5" Width="100%" Height="26px" class="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:DropDownList>
                                                            <asp:DropDownList ID="TbEntradaF6" Width="100%" Height="26px" class="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible; border-bottom-right-radius: 5px"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-4">
                                                            <asp:HyperLink ID="hyperlink2" NavigateUrl="" ImageUrl="" Width="100%" ImageWidth="100%" Text="Lado opuesto" Target="_new" runat="server" class="rounded rounded-2 border border-dark shadow mt-2" />
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="FileUpload2" runat="server" class="form-control form-control-sm border border-dark"></asp:FileUpload>
                                                                <button id="Botonsubida2" runat="server" onserverclick="Insertar_foto" type="button" class="btn btn-sm border border-dark">Subir</button>
                                                            </div>
                                                            <h6>Vista general</h6>
                                                        </div>
                                                        <div class="col-lg-4">
                                                            <asp:HyperLink ID="hyperlink3" NavigateUrl="" ImageUrl="" Width="100%" ImageWidth="100%" Text="Lado opuesto" Target="_new" runat="server" class="rounded rounded-2 border border-dark shadow mt-2" />
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="FileUpload3" runat="server" class="form-control form-control-sm border border-dark"></asp:FileUpload>
                                                                <button id="Botonsubida3" runat="server" onserverclick="Insertar_foto" type="button" class="btn btn-sm border border-dark">Subir</button>
                                                            </div>
                                                            <h6>Lado operario</h6>
                                                        </div>
                                                        <div class="col-lg-4">
                                                            <asp:HyperLink ID="hyperlink4" NavigateUrl="" ImageUrl="" Width="100%" ImageWidth="100%" Text="Lado opuesto" Target="_new" runat="server" class="rounded rounded-2 border border-dark shadow mt-2" />
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="FileUpload4" runat="server" class="form-control form-control-sm border border-dark"></asp:FileUpload>
                                                                <button id="Botonsubida4" runat="server" onserverclick="Insertar_foto" type="button" class="btn btn-sm border border-dark">Subir</button>
                                                            </div>
                                                            <h6>Lado opuesto</h6>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <asp:Label ID="UploadStatusLabel" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6 pe-0">
                                            <div class="card border-0 shadow mt-2">
                                                <div class="card-header border border-dark shadow" style="background-color: darkorange">
                                                    <label class="h6"><i>PARTE MÓVIL</i></label>
                                                </div>
                                                <div class="card-body border border-dark rounded-bottom p-0">
                                                    <div class="row">
                                                        <div class="col-md-3"></div>
                                                        <div class="col-md-6">
                                                            <asp:HyperLink ID="hyperlink5" NavigateUrl="" ImageUrl="" Width="100%" ImageWidth="100%" Text="Lado opuesto" Target="_new" runat="server" class="rounded rounded-2 border border-dark shadow mt-2" />
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="FileUpload5" runat="server" class="form-control form-control-sm border border-dark"></asp:FileUpload>
                                                                <button id="Botonsubida5" runat="server" onserverclick="Insertar_foto" type="button" class="btn btn-sm border border-dark">Subir</button>
                                                            </div>
                                                            <h6>Vista general</h6>
                                                        </div>
                                                        <div class="col-md-3"></div>
                                                    </div>
                                                    <div class="row ps-25">
                                                        <div class="input-group input-group-sm" style="margin: 0; padding: 0; overflow: visible; width: 100%">
                                                            <span class="cabecera" style="text-align: center; overflow: visible; width: 25%; border-bottom-left-radius: 0; border-top-left-radius: 5px; font-weight: bold">TIPO DE AGUA</span>
                                                            <asp:DropDownList ID="AtempTipoM" runat="server" Width="75%" Style="text-align: center; border-bottom-left-radius: 0; border-bottom-right-radius: 0; font-weight: bold" CssClass="form-select border border-dark"></asp:DropDownList>
                                                        </div>

                                                        <div class="col-lg-3 ps-0 pe-0">
                                                            <asp:Label ID="ThCircuitoM" Width="100%" class="cabecera" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible">Nº CIRCUITO</asp:Label>
                                                            <asp:DropDownList ID="TbCircuitoM1" Width="100%" Height="26px" class="cuerpo-editable" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:DropDownList>
                                                            <asp:DropDownList ID="TbCircuitoM2" Width="100%" Height="26px" class="cuerpo-editable" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:DropDownList>
                                                            <asp:DropDownList ID="TbCircuitoM3" Width="100%" Height="26px" class="cuerpo-editable" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:DropDownList>
                                                            <asp:DropDownList ID="TbCircuitoM4" Width="100%" Height="26px" class="cuerpo-editable" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:DropDownList>
                                                            <asp:DropDownList ID="TbCircuitoM5" Width="100%" Height="26px" class="cuerpo-editable" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:DropDownList>
                                                            <asp:DropDownList ID="TbCircuitoM6" Width="100%" Height="26px" class="cuerpo-editable" Font-Bold="true" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible; border-bottom-left-radius: 5px"></asp:DropDownList>
                                                        </div>
                                                        <div class="col-lg-3 ps-0 pe-0">
                                                            <asp:Label ID="ThCaudalM" Width="100%" class="cabecera" runat="server" Font-Bold="true" Style="text-align: center; margin: 0; padding: 0; overflow: visible">CAUDAL</asp:Label>
                                                            <asp:TextBox ID="TbCaudalM1" Width="100%" Height="26px" class="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                            <asp:TextBox ID="TbCaudalM2" Width="100%" Height="26px" class="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                            <asp:TextBox ID="TbCaudalM3" Width="100%" Height="26px" class="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                            <asp:TextBox ID="TbCaudalM4" Width="100%" Height="26px" class="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                            <asp:TextBox ID="TbCaudalM5" Width="100%" Height="26px" class="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                            <asp:TextBox ID="TbCaudalM6" Width="100%" Height="26px" class="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                        </div>
                                                        <div class="col-lg-3 ps-0 pe-0">
                                                            <asp:Label ID="ThTemperaturaM" Width="100%" class="cabecera" runat="server" Font-Bold="true" Style="text-align: center; margin: 0; padding: 0; overflow: visible">TEMPERATURA</asp:Label>
                                                            <asp:TextBox ID="TbTemperaturaM1" Width="100%" Height="26px" class="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                            <asp:TextBox ID="TbTemperaturaM2" Width="100%" Height="26px" class="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                            <asp:TextBox ID="TbTemperaturaM3" Width="100%" Height="26px" class="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                            <asp:TextBox ID="TbTemperaturaM4" Width="100%" Height="26px" class="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                            <asp:TextBox ID="TbTemperaturaM5" Width="100%" Height="26px" class="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                            <asp:TextBox ID="TbTemperaturaM6" Width="100%" Height="26px" class="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                                                        </div>
                                                        <div class="col-lg-3 ps-0 pe-0">
                                                            <asp:TextBox ID="ThEntradaM" Width="100%" class="cabecera" runat="server" Font-Bold="true" Style="text-align: center; margin: 0; padding: 0; overflow: visible">ENTRADA</asp:TextBox>
                                                            <asp:DropDownList ID="TbEntradaM1" Width="100%" Height="26px" class="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:DropDownList>
                                                            <asp:DropDownList ID="TbEntradaM2" Width="100%" Height="26px" class="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:DropDownList>
                                                            <asp:DropDownList ID="TbEntradaM3" Width="100%" Height="26px" class="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:DropDownList>
                                                            <asp:DropDownList ID="TbEntradaM4" Width="100%" Height="26px" class="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:DropDownList>
                                                            <asp:DropDownList ID="TbEntradaM5" Width="100%" Height="26px" class="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:DropDownList>
                                                            <asp:DropDownList ID="TbEntradaM6" Width="100%" Height="26px" class="cuerpo-editable" runat="server" Style="text-align: center; margin: 0; padding: 0; overflow: visible; border-bottom-right-radius: 5px"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-4">
                                                            <asp:HyperLink ID="hyperlink6" NavigateUrl="" ImageUrl="" Width="100%" ImageWidth="100%" Text="Lado opuesto" Target="_new" runat="server" class="rounded rounded-2 border border-dark shadow mt-2" />
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="FileUpload6" runat="server" class="form-control form-control-sm border border-dark"></asp:FileUpload>
                                                                <button id="Botonsubida6" runat="server" onserverclick="Insertar_foto" type="button" class="btn btn-sm border border-dark">Subir</button>
                                                            </div>
                                                            <h6>Vista general</h6>
                                                        </div>
                                                        <div class="col-lg-4">
                                                            <asp:HyperLink ID="hyperlink7" NavigateUrl="" ImageUrl="" Width="100%" ImageWidth="100%" Text="Lado opuesto" Target="_new" runat="server" class="rounded rounded-2 border border-dark shadow mt-2" />
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="FileUpload7" runat="server" class="form-control form-control-sm border border-dark"></asp:FileUpload>
                                                                <button id="Botonsubida7" runat="server" onserverclick="Insertar_foto" type="button" class="btn btn-sm border border-dark">Subir</button>
                                                            </div>
                                                            <h6>Lado operario</h6>
                                                        </div>
                                                        <div class="col-lg-4">
                                                            <asp:HyperLink ID="hyperlink8" NavigateUrl="" ImageUrl="" Width="100%" ImageWidth="100%" Text="Lado opuesto" Target="_new" runat="server" class="rounded rounded-2 border border-dark shadow mt-2" />
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="FileUpload8" runat="server" class="form-control form-control-sm border border-dark"></asp:FileUpload>
                                                                <button id="Botonsubida8" runat="server" onserverclick="Insertar_foto" type="button" class="btn btn-sm border border-dark">Subir</button>
                                                            </div>
                                                            <h6>Lado opuesto</h6>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <asp:Label ID="UploadStatusLabel2" runat="server"></asp:Label>
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
            <div class="row mt-2">
                <div class="card border border-0">
                    <div class="card-header mt-2 border border-dark shadow" style="background-color: darkorange">
                        <label class="h5" style="color: whitesmoke; text-shadow: 1px 2px 2px darkslategray"><i class="bi bi-info-circle">&nbsp Datos de la ficha</i></label>
                        </div>
                    <div class="card-body shadow p-0">
                        <div class="row ps-25">
                            <div class="col-lg-6 ps-0 pe-0">
                                <asp:Label ID="tbObservacionesTitulo" runat="server" CssClass="cabecera" Width="100%" Style="text-align: center; margin: 0; padding: 0; overflow: visible; font-weight: bold">CAMBIOS RESPECTO A LA FICHA ANTERIOR</asp:Label>
                                <asp:TextBox ID="tbObservaciones" TextMode="MultiLine" Rows="2" runat="server" CssClass="cuerpo-editable" Width="100%" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                            </div>
                            <div class="col-lg-6 ps-0 pe-0">
                                <asp:Label ID="tbRazonesTitulo" runat="server" Width="100%" CssClass="cabecera" Style="text-align: center; margin: 0; padding: 0; overflow: visible; font-weight: bold">MOTIVO DEL CAMBIO</asp:Label>
                                <asp:TextBox ID="tbRazones" TextMode="MultiLine" Rows="2" runat="server" Width="100%" CssClass="cuerpo-editable" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row ps-25">
                            <div class="col-lg-4 ps-0 pe-0">
                                <asp:Label ID="cbElaboradoPorTitulo" runat="server" CssClass="cabecera" Width="100%" Style="text-align: center; margin: 0; padding: 0; overflow: visible; font-weight: bold">ELABORADO</asp:Label>
                                <asp:DropDownList ID="cbElaboradoPor" runat="server" Width="100%" Height="35px" CssClass="cuerpo-editable" Style="text-align: center; margin: 0; padding: 0; overflow: visible; border-bottom-left-radius: 5px"></asp:DropDownList>
                            </div>
                            <div class="col-lg-4 ps-0 pe-0">
                                <asp:Label ID="cbRevisadoPorTitulo" runat="server" CssClass="cabecera" Width="100%" Style="text-align: center; margin: 0; padding: 0; overflow: visible; font-weight: bold">REVISADO</asp:Label>
                                <asp:DropDownList ID="cbRevisadoPor" runat="server" Width="100%" Height="35px" CssClass="cuerpo-editable" Style="text-align: center; margin: 0; padding: 0; overflow: visible"></asp:DropDownList>
                            </div>
                            <div class="col-lg-4 ps-0 pe-0">
                                <asp:Label ID="cbAprobadoPorTitulo" runat="server" CssClass="cabecera" Width="100%" Style="text-align: center; margin: 0; padding: 0; overflow: visible; font-weight: bold">APROBADO</asp:Label>
                                <asp:DropDownList ID="cbAprobadoPor" runat="server" Width="100%" Height="35px" CssClass="cuerpo-editable" Style="text-align: center; margin: 0; padding: 0; overflow: visible; border-bottom-right-radius: 5px"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--MODALES DE EDICION --%>
    <div class="modal fade" id="ModalUbicacion" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header border border-dark shadow" style="background-color: darkorange">

                    <label class="h4" id="UbicaMolde" style="color: whitesmoke; text-shadow: 1px 2px 2px darkslategray" runat="server">3546</label>
                    <label class="h4" id="UbicaMoldeNombre" style="color: whitesmoke; text-shadow: 1px 2px 2px darkslategray" runat="server">nombre de molde</label>

                </div>
                <div class="modal-body border border-dark">
                    <div class="row">
                        <div class="col-lg-6">
                            <h5>Ubicación</h5>
                            <div class="input-group ">
                                <span class="input-group-text border border-dark shadow" id="basic-addon1"><i class="bi bi-geo-alt"></i></span>
                                <asp:Label ID="UbicacionMolde" runat="server" class="input-group-text border border-dark shadow">
                                </asp:Label>
                                <%-- 
                                        <input type="text" id="UbicacionMolde" runat="server" class="form-control" placeholder="Ubicación" aria-label="Username" aria-describedby="basic-addon1">
                                --%>
                            </div>

                        </div>
                        <div class="col-lg-6">
                            <h5>&nbsp</h5>
                            <div class="form-check">
                                <input class="form-check-input" runat="server" type="checkbox" value="" id="flexCheckDefault" disabled="disabled">
                                <label style="color: black">
                                    Molde activo
                                </label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-6">
                            <asp:Label ID="LblModificado" CssClass="mb-3" runat="server" Font-Size="Small"></asp:Label>
                        </div>
                        <div class="col-lg-6">
                        </div>
                    </div>
                    <div class="row">

                        <div class="col-lg-12">
                            <img id="ImgUbicacion" runat="server" class="img-fluid border border-1 rounded rounded-2 mt-2" src="http://facts4-srv/thermogestion/SMARTH_docs/MANTENIMIENTO/sin_imagen.jpg" />
                        </div>

                    </div>
                </div>
                <div class="modal-footer border border-dark shadow" style="background-color: darkorange">
                    <button type="button" class="btn btn-danger border border-dark shadow" data-bs-dismiss="modal"><i class="bi bi-caret-left-fill"></i></button>

                </div>
            </div>
        </div>
    </div>
     <div class="modal fade" id="ModalAsignaMano" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header border border-dark shadow" style="background-color: darkorange">
                            <label class="h4" id="AsignaMano" runat="server">3546</label>
                            <label class="h4 ms-2" id="AsignaManoNombre" runat="server">nombre de molde</label>
                        </div>
                        <div class="modal-body border border-dark">
                            <div class="row">
                                <div class="col-lg-12">
                                    <h5>Mano de robot actual:</h5>
                                    <div class="input-group mb-3">
                                        <span class="input-group-text"><i class="bi bi-link-45deg"></i></span>
                                        <input type="text" disabled="disabled" id="InputManoActual" runat="server" class="form-control" placeholder="Username" aria-label="Username" aria-describedby="basic-addon1">
                                    </div>
                                    <h5>Nueva mano a asignar:</h5>
                                    <div class="input-group mb-3">
                                        <span class="input-group-text"><i class="bi bi-save"></i></span>
                                        <input class="form-control" list="ListaManosAsignacion" id="InputAsignaNuevaMano" runat="server" placeholder="Escribe una mano...">
                                        <datalist id="ListaManosAsignacion" runat="server">
                                        </datalist>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer border border-dark shadow" style="background-color: darkorange">
                            <button type="button" class="btn btn-danger" data-bs-dismiss="modal"><i class="bi bi-caret-left-fill"></i></button>
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="Actualizar_Mano" class="btn btn-success"><i class="bi bi-sd-card"></i></asp:LinkButton>

                        </div>
                    </div>
                </div>
            </div>



    <div class="modal fade" id="ModalEditaAccion" runat="server" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="EditaAccionLabel" aria-hidden="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-primary shadow">
                    <h5 class="modal-title text-white" id="staticBackdropLabel" runat="server">Detalles de revisión</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    <asp:TextBox ID="IDINSPECCION" runat="server" Style="text-align: center" Width="100%" Enabled="false" Visible="false"></asp:TextBox>
                </div>
                <div class="modal-body" runat="server">
                    <div>
                        <div class="hidden">
                            <asp:TextBox ID="tbManual" runat="server" Style="text-align: center"></asp:TextBox>
                            <asp:TextBox ID="tbAutomatico" runat="server" Style="text-align: center"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer" style="background: #e6e6e6">
                    <button type="button" class="btn btn-danger" data-bs-dismiss="modal"><i class="bi bi-caret-left-fill"></i></button>
                </div>
            </div>
        </div>
    </div>


    <%--OFFCANVAS DE FILTROS --%>
    <div class="offcanvas offcanvas-end" tabindex="-1" id="offcanvasExample" aria-labelledby="offcanvasExampleLabel">
        <div class="offcanvas-header">
            <h5 class="offcanvas-title" id="offcanvasExampleLabel">Filtros</h5>
            <button type="button" class="btn-close text-reset" data-bs-dismiss="offcanvas" aria-label="Close"></button>
        </div>
        <div class="offcanvas-body">
            <div class="row">
                <br />
                <h6>Estado:</h6>
                <div class="form-check form-switch ms-3 mb-3">
                    <input class="form-check-input" type="checkbox" runat="server" id="SwitchActivas">
                    <label class="form-check-label" for="SwitchActivas">Mostrar sólo defetuosas</label>
                </div>
                <br />
                <h6>Periodo de revisión:</h6>
                <asp:DropDownList ID="Selecaño" runat="server" CssClass="form-select shadow-sm ms-2 mb-3" Style="width: 95%" Font-Size="Large">
                    <asp:ListItem Text="2024" Value="2024"></asp:ListItem>
<asp:ListItem Text="2023" Value="2023"></asp:ListItem>
                    <asp:ListItem Text="2022" Value="2022"></asp:ListItem>
                    <asp:ListItem Text="2021" Value="2021"></asp:ListItem>
                    <asp:ListItem Text="2020" Value="2020"></asp:ListItem>
                    <asp:ListItem Text="2019" Value="2019"></asp:ListItem>
                    <asp:ListItem Text="2018" Value="2018"></asp:ListItem>
                </asp:DropDownList>
                <br />
                <h6>Desde:</h6>
                <input type="text" id="InputFechaDesde" class="form-control ms-2 mb-3 Add-text" style="width: 95%" autocomplete="off" runat="server">
                <br />
                <h6>Hasta:</h6>
                <input type="text" id="InputFechaHasta" class="form-control ms-2 mb-3 Add-text" style="width: 95%" autocomplete="off" runat="server">
                <br />


                <h6>Referencia:</h6>
                <div class="input-group mb-3">
                    <input class="form-control" list="DatalistReferencias" id="selectReferencia" runat="server" placeholder="Escribe un referencia...">
                    <datalist id="DatalistReferencias" runat="server">
                    </datalist>
                </div>
                <br />
                <h6>Molde:</h6>
                <div class="input-group mb-3">
                    <input class="form-control" list="DatalistMoldes" id="selectMolde" runat="server" placeholder="Escribe un molde...">
                    <datalist id="DatalistMoldes" runat="server">
                    </datalist>
                </div>
                <br />
                <h6>Lote:</h6>
                <div class="input-group mb-3">
                    <input class="form-control" list="DatalistLotes" id="selectLote" runat="server" placeholder="Escribe un lote...">
                    <datalist id="DatalistLotes" runat="server">
                    </datalist>
                </div>
                <br />
                <h6>Cliente:</h6>
                <div class="input-group mb-3">
                    <asp:DropDownList ID="lista_clientes" runat="server" class="form-select">
                    </asp:DropDownList>
                </div>
                <br />
                <h6>Estado de revisión:</h6>
                <div class="input-group mb-3">
                    <asp:DropDownList ID="lista_estado_revision" runat="server" class="form-select">
                    </asp:DropDownList>
                </div>
                <br />
                <h6>Responsable:</h6>
                <div class="input-group mb-3">
                    <asp:DropDownList ID="lista_responsable" runat="server" class="form-select">
                    </asp:DropDownList>
                </div>


                <div class="input-group mb-3">
                    <button id="Button1" runat="server" type="button" class="btn btn-secondary" style="width: 100%; text-align: left">
                        <i class="bi bi-search me-2"></i>Filtrar</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
